Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports Iaip.Apb.Facilities
Imports Jil

Public Class IAIPQueryGenerator
    Dim query As String

    Dim dtcboCountySearch1 As DataTable
    Dim dtcboCountySearch2 As DataTable
    Dim dtcboDistrictSearch1 As DataTable
    Dim dtcboDistrictSearch2 As DataTable
    Dim dtcboSIPSearch1 As DataTable
    Dim dtcboSIPSearch2 As DataTable
    Dim dtcboPart61Search1 As DataTable
    Dim dtcboPart61Search2 As DataTable
    Dim dtcboPart60Search1 As DataTable
    Dim dtcboPart60Search2 As DataTable
    Dim dtcboPart63Search1 As DataTable
    Dim dtcboPart63Search2 As DataTable
    Dim dtcboSSCPEngineerSearch1 As DataTable
    Dim dtcboSSCPEngineerSearch2 As DataTable
    Dim dtcboSSCPUnitSearch1 As DataTable
    Dim dtcboSSCPUnitSearch2 As DataTable

    Protected Overrides Sub OnLoad(e As EventArgs)
        lblQueryCount.Text = ""
        lblCannedQueryCount.Text = ""

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

        If bgwQueryGenerator.IsBusy Then
            bgwQueryGenerator.CancelAsync()
        Else
            bgwQueryGenerator.WorkerReportsProgress = True
            bgwQueryGenerator.WorkerSupportsCancellation = True
            bgwQueryGenerator.RunWorkerAsync()
        End If

        DTPStartUpDateSearch1.Value = Today
        DTPStartUpDateSearch1.Checked = False
        DTPStartUpDateSearch2.Value = Today
        DTPStartUpDateSearch2.Checked = False
        DTPShutDownDateSearch1.Value = Today
        DTPShutDownDateSearch1.Checked = False
        DTPShutDownDateSearch2.Value = Today
        DTPShutDownDateSearch2.Checked = False
        DTPLastFCESearch1.Value = Today
        DTPLastFCESearch1.Checked = False
        DTPLastFCESearch2.Value = Today
        DTPLastFCESearch2.Checked = False

        tcQueryOptions.Size = New Size(tcQueryOptions.Size.Width, 389)

        dtpCannedEndDate.Value = DatePriorToDate(9, 30, Today)
        dtpCannedStartDate.Value = DatePriorToDate(10, 1, dtpCannedEndDate.Value)
        cboCannedSelection.BindToEnum(Of FacilityClassification)()
        cboCannedSelection.SelectedIndex = 1

        MyBase.OnLoad(e)
    End Sub

#Region "Page Load Functions"

    Private Sub LoadDataSets()
        Try

            query = "Select strCountyCode, strCountyName " &
            "from LookUpCountyInformation " &
            "order by strCountyName "

            dtcboCountySearch1 = DB.GetDataTable(query)
            dtcboCountySearch2 = dtcboCountySearch1.Copy

            query = "select strDistrictCode, strDistrictName " &
            "from LookUPDistricts " &
            "order by strDistrictName "

            dtcboDistrictSearch1 = DB.GetDataTable(query)
            dtcboDistrictSearch2 = dtcboDistrictSearch1.Copy

            query = "select " &
            "strSubPart " &
            "from LookUpSubPartSIP " &
            "order by strSubPart "

            dtcboSIPSearch1 = DB.GetDataTable(query)
            dtcboSIPSearch2 = dtcboSIPSearch1.Copy

            query = "select LK_SUBPART_CODE as strSubPart
                from LK_ICIS_PROGRAM_SUBPART
                where LGCY_PROGRAM_CODE = '8'
                      and ICIS_STATUS_FLAG = 'A'
                order by LK_SUBPART_CODE "

            dtcboPart61Search1 = DB.GetDataTable(query)
            dtcboPart61Search2 = dtcboPart61Search1.Copy

            query = "select LK_SUBPART_CODE as strSubPart
                from LK_ICIS_PROGRAM_SUBPART
                where LGCY_PROGRAM_CODE = '9'
                      and ICIS_STATUS_FLAG = 'A'
                order by LK_SUBPART_CODE "

            dtcboPart60Search1 = DB.GetDataTable(query)
            dtcboPart60Search2 = dtcboPart60Search1.Copy

            query = "select LK_SUBPART_CODE as strSubPart
                from LK_ICIS_PROGRAM_SUBPART
                where LGCY_PROGRAM_CODE = 'M'
                      and ICIS_STATUS_FLAG = 'A'
                order by LK_SUBPART_CODE "

            dtcboPart63Search1 = DB.GetDataTable(query)
            dtcboPart63Search2 = dtcboPart63Search1.Copy

            query = "SELECT numUserID AS numUserID, CONCAT(StrLastName, ', ', strFirstname) AS Staff
                FROM EPDUserProfiles
                WHERE numProgram = '4'
                UNION
                SELECT numUserID AS numUserID, CONCAT(StrLastName, ', ', strFirstname) AS Staff
                FROM EPDUserProfiles, SSCPItemMaster
                WHERE EPDUserProfiles.numuserID = SSCPItemMaster.strResponsibleStaff
                ORDER BY Staff"

            dtcboSSCPEngineerSearch1 = DB.GetDataTable(query)
            dtcboSSCPEngineerSearch2 = dtcboSSCPEngineerSearch1.Copy

            query = "select " &
            "numUnitCode, strUnitDesc " &
            "from LookUpEPDUnits " &
            "where numProgramcode = 4 and Active = 1 "

            dtcboSSCPUnitSearch1 = DB.GetDataTable(query)
            dtcboSSCPUnitSearch2 = dtcboSSCPUnitSearch1.Copy

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ShowComboBoxes()

        With cboCountySearch1
            .DataSource = dtcboCountySearch1
            .DisplayMember = "strCountyName"
            .ValueMember = "strCountyCode"
            .SelectedIndex = -1
        End With

        With cboCountySearch2
            .DataSource = dtcboCountySearch2
            .DisplayMember = "strCountyName"
            .ValueMember = "strCountyCode"
            .SelectedIndex = -1
        End With

        With cboDistrictSearch1
            .DataSource = dtcboDistrictSearch1
            .DisplayMember = "strDistrictName"
            .ValueMember = "strDistrictCode"
            .SelectedIndex = -1
        End With

        With cboDistrictSearch2
            .DataSource = dtcboDistrictSearch2
            .DisplayMember = "strDistrictName"
            .ValueMember = "strDistrictCode"
            .SelectedIndex = -1
        End With

        With cboSIPSearch1
            .DataSource = dtcboSIPSearch1
            .DisplayMember = "strSubPart"
            .SelectedIndex = -1
        End With

        With cboSIPSearch2
            .DataSource = dtcboSIPSearch2
            .DisplayMember = "strSubPart"
            .SelectedIndex = -1
        End With

        With cboPart61Search1
            .DataSource = dtcboPart61Search1
            .DisplayMember = "strSubPart"
            .SelectedIndex = -1
        End With

        With cboPart61Search2
            .DataSource = dtcboPart61Search2
            .DisplayMember = "strSubPart"
            .SelectedIndex = -1
        End With

        With cboPart60Search1
            .DataSource = dtcboPart60Search1
            .DisplayMember = "strSubPart"
            .SelectedIndex = -1
        End With

        With cboPart60Search2
            .DataSource = dtcboPart60Search2
            .DisplayMember = "strSubPart"
            .SelectedIndex = -1
        End With

        With cboPart63Search1
            .DataSource = dtcboPart63Search1
            .DisplayMember = "strSubPart"
            .SelectedIndex = -1
        End With

        With cboPart63Search2
            .DataSource = dtcboPart63Search2
            .DisplayMember = "strSubPart"
            .SelectedIndex = -1
        End With

        With cboSSCPEngineerSearch1
            .DataSource = dtcboSSCPEngineerSearch1
            .DisplayMember = "Staff"
            .ValueMember = "numUserID"
            .SelectedIndex = -1
        End With

        With cboSSCPEngineerSearch2
            .DataSource = dtcboSSCPEngineerSearch2
            .DisplayMember = "Staff"
            .ValueMember = "numUserID"
            .SelectedIndex = -1
        End With

        With cboSSCPUnitSearch1
            .DataSource = dtcboSSCPUnitSearch1
            .DisplayMember = "strUnitDesc"
            .ValueMember = "numUnitCode"
            .SelectedIndex = -1
        End With

        With cboSSCPUnitSearch2
            .DataSource = dtcboSSCPUnitSearch2
            .DisplayMember = "strUnitDesc"
            .ValueMember = "numUnitCode"
            .SelectedIndex = -1
        End With

        cboOperationStatusSearch1.Text = " "
        cboOperationStatusSearch1.Items.Add(" ")
        cboOperationStatusSearch1.Items.Add("O - Operational")
        cboOperationStatusSearch1.Items.Add("P - Planned")
        cboOperationStatusSearch1.Items.Add("C - Under Construction")
        cboOperationStatusSearch1.Items.Add("T - Temporarily Closed")
        cboOperationStatusSearch1.Items.Add("X - Closed/Dismantled")
        cboOperationStatusSearch1.Items.Add("I - Seasonal Operation")

        cboOperationStatusSearch2.Text = " "
        cboOperationStatusSearch2.Items.Add(" ")
        cboOperationStatusSearch2.Items.Add("O - Operational")
        cboOperationStatusSearch2.Items.Add("P - Planned")
        cboOperationStatusSearch2.Items.Add("C - Under Construction")
        cboOperationStatusSearch2.Items.Add("T - Temporarily Closed")
        cboOperationStatusSearch2.Items.Add("X - Closed/Dismantled")
        cboOperationStatusSearch2.Items.Add("I - Seasonal Operation")

        cboClassificationSearch1.Text = " "
        cboClassificationSearch1.Items.Add(" ")
        cboClassificationSearch1.Items.Add("A")
        cboClassificationSearch1.Items.Add("B")
        cboClassificationSearch1.Items.Add("SM")
        cboClassificationSearch1.Items.Add("PR")
        cboClassificationSearch1.Items.Add("C")


        cboClassificationSearch2.Text = " "
        cboClassificationSearch2.Items.Add(" ")
        cboClassificationSearch2.Items.Add("A")
        cboClassificationSearch2.Items.Add("B")
        cboClassificationSearch2.Items.Add("SM")
        cboClassificationSearch2.Items.Add("PR")
        cboClassificationSearch2.Items.Add("C")

        cboCMSUniverseSearch1.Text = " "
        cboCMSUniverseSearch1.Items.Add(" ")
        cboCMSUniverseSearch1.Items.Add("A")
        cboCMSUniverseSearch1.Items.Add("S")
        cboCMSUniverseSearch1.Items.Add("M")

        cboCMSUniverseSearch2.Text = " "
        cboCMSUniverseSearch2.Items.Add(" ")
        cboCMSUniverseSearch2.Items.Add("A")
        cboCMSUniverseSearch2.Items.Add("S")
        cboCMSUniverseSearch2.Items.Add("M")

        cboCountySearch1.Visible = True
        cboCountySearch2.Visible = True
        cboDistrictSearch1.Visible = True
        cboDistrictSearch2.Visible = True
        cboSIPSearch1.Visible = True
        cboSIPSearch2.Visible = True
        cboPart61Search1.Visible = True
        cboPart61Search2.Visible = True
        cboPart60Search1.Visible = True
        cboPart60Search2.Visible = True
        cboPart63Search1.Visible = True
        cboPart63Search2.Visible = True
        cboOperationStatusSearch1.Visible = True
        cboOperationStatusSearch2.Visible = True
        cboClassificationSearch1.Visible = True
        cboClassificationSearch2.Visible = True
        cboCMSUniverseSearch1.Visible = True
        cboCMSUniverseSearch2.Visible = True
    End Sub

#End Region

    Private Sub GenerateSQL2()
        Dim MasterSQL As String = ""
        Dim SQLSelect As String = ""
        Dim SQLFrom As String = ""
        Dim SQLWhere As String = ""
        Dim SQLOrder As String = ""
        Dim SQLWhereCase1 As String = ""
        Dim SQLWhereCase2 As String = ""
        Dim params As New List(Of SqlParameter)

        Try
            Dim temp As String = ""
            Dim i As Integer = 0
            Dim j As Integer = 0

            SQLSelect = "Select " &
            "substring(APBFacilityInformation.STRAIRSNUMBER, 5, 3) + '-' + substring(APBFacilityInformation.STRAIRSNUMBER, 8, 5) as AIRSNumber, " &
            "APBFacilityInformation.strFacilityName, "

            SQLFrom = " From APBMasterAIRS, APBFacilityInformation, "

            SQLWhere = " Where APBMasterAIRS.strAIRSNumber = APBFacilityInformation.strAIRSNumber "

            'Adding Select/From to SQL statements
            If chbFacilityStreet1.Checked Then
                SQLSelect = SQLSelect &
                "APBFacilityInformation.strFacilityStreet1, "

                If SQLFrom.IndexOf("APBFacilityInformation") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBFacilityInformation, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = APBFacilityInformation.strAIRSNumber "
                End If
            End If
            If chbFacilityStreet2.Checked Then
                SQLSelect = SQLSelect &
                "APBFacilityInformation.strFacilityStreet2, "

                If SQLFrom.IndexOf("APBFacilityInformation") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBFacilityInformation, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = APBFacilityInformation.strAIRSNumber "
                End If
            End If

            If chbFacilityCity.Checked Then
                SQLSelect = SQLSelect &
                "APBFacilityInformation.strFacilityCity, "

                If SQLFrom.IndexOf("APBFacilityInformation") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBFacilityInformation, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = APBFacilityInformation.strAIRSNumber "
                End If
            End If

            If chbFacilityZipCode.Checked Then
                SQLSelect = SQLSelect &
                "APBFacilityInformation.strFacilityZipCode, "

                If SQLFrom.IndexOf("APBFacilityInformation") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBFacilityInformation, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = APBFacilityInformation.strAIRSNumber "
                End If
            End If

            If chbFacilityLatitude.Checked Then
                SQLSelect = SQLSelect &
                "APBFacilityInformation.numFacilityLatitude, "

                If SQLFrom.IndexOf("APBFacilityInformation") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBFacilityInformation, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = APBFacilityInformation.strAIRSNumber "
                End If
            End If

            If chbSSCPEngineer.Checked Then
                SQLSelect = SQLSelect &
                " concat(StrLastName, ', ', strFirstname) as SSCPEngineer, "

                If SQLFrom.IndexOf("VW_SSCP_MOSTRECENTASSIGNMENT") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " VW_SSCP_MOSTRECENTASSIGNMENT, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSnumber = VW_SSCP_MOSTRECENTASSIGNMENT.strAIRSNumber "
                End If
                If SQLFrom.IndexOf("EPDUserProflies") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " EPDUserProfiles, "
                    SQLWhere = SQLWhere & " and EPDUserProfiles.numUserID = VW_SSCP_MOSTRECENTASSIGNMENT.numSSCPEngineer "
                End If
            End If

            If chbFacilityLongitude.Checked Then
                SQLSelect = SQLSelect &
                "APBFacilityInformation.numFacilityLongitude, "

                If SQLFrom.IndexOf("APBFacilityInformation") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBFacilityInformation, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = APBFacilityInformation.strAIRSNumber "
                End If
            End If

            If chbCounty.Checked Then
                SQLSelect = SQLSelect &
                "LookUpCountyInformation.strCountyName, "

                If SQLFrom.IndexOf("LookUpCountyInformation") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " LookUpCountyInformation, "
                    SQLWhere = SQLWhere & " and SUBSTRING(APBMasterAIRS.strAIRSNumber, 5, 3) = " &
                    "LookUpCountyInformation.strCountyCode "
                End If
            End If

            If chbDistrict.Checked Then
                SQLSelect = SQLSelect &
                "LookUpDistricts.strDistrictName, "

                If SQLFrom.IndexOf("LookUpDistricts") <> -1 Then
                    'SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " LookUpDistricts, LookUpDistrictInformation, "
                    SQLWhere = SQLWhere & " and SUBSTRING(APBMasterAIRS.strAIRSNumber, 5, 3) = " &
                    "LookUpDistrictInformation.strDistrictCounty " &
                    " and LookUpDistrictInformation.strDistrictCode = LookUpDistricts.strDistrictCode "
                End If
            End If

            If chbDistrictResponsible.Checked AndAlso SQLFrom.IndexOf("SSCPDistrictResponsible") = -1 Then
                SQLFrom = SQLFrom & " SSCPDistrictResponsible, "
                SQLWhere = SQLWhere & " AND APBMasterAIRS.strAIRSnumber = SSCPDistrictResponsible.strAIRSNumber "
                If rdbDistrictResponsibleTrue.Checked Then
                    SQLWhere = SQLWhere & " and SSCPDistrictResponsible.strDistrictResponsible = 'True' "
                Else
                    SQLWhere = SQLWhere & " and SSCPDistrictResponsible.strDistrictResponsible = 'False' "
                End If
            End If


            If chbOperationStatus.Checked Then
                SQLSelect = SQLSelect &
                "APBHeaderData.strOperationalStatus, "

                If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBHeaderData, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbClassification.Checked Then
                SQLSelect = SQLSelect &
                "APBHeaderData.strClass, "

                If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBHeaderData, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbSICCode.Checked Then
                SQLSelect = SQLSelect &
                "APBHeaderData.strSICCode, "

                If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                    'SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBHeaderData, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbNAICSCode.Checked Then
                SQLSelect = SQLSelect &
                "APBHeaderData.strNAICSCode, "

                If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBHeaderData, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbStartUpDate.Checked Then
                SQLSelect = SQLSelect &
                "APBHeaderData.datStartUpDate, "

                If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBHeaderData, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbShutDownDate.Checked Then
                SQLSelect = SQLSelect &
                "APBHeaderData.datShutDownDate, "

                If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBHeaderData, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbCMSUniverse.Checked Then
                SQLSelect = SQLSelect &
                "APBSupplamentalData.strCMSMember, "

                If SQLFrom.IndexOf("APBSupplamentalData") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBSupplamentalData, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = APBSupplamentalData.strAIRSNumber "
                End If
            End If

            If chbPlantDescription.Checked Then
                SQLSelect = SQLSelect &
                "APBHeaderData.strPlantDescription, "

                If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                    '  SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBHeaderData, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = APBHeaderData.strAIRSNumber "
                End If
            End If

            If chbAttainmentStatus.Checked Then
                SQLSelect = SQLSelect & "case " &
                "when SUBSTRING(APBHeaderData.strAttainmentStatus, 2, 1) = '1' then '1-Hr Yes' " &
                "Else null end OneHrYes, " &
                "case " &
                "when SUBSTRING(APBHeaderData.strAttainmentStatus, 2, 1) = '2' then '1-Hr Contribute' " &
                "Else null end OneHrContribute, " &
                "case " &
                "when SUBSTRING(APBHeaderData.strAttainmentStatus, 2, 1) = '0' then '1-Hr No' " &
                "Else null End OneHrNo, " &
                "case " &
                "when SUBSTRING(APBHeaderData.strAttainmentStatus, 3, 1) = '1' then '8-Hr Atlanta' " &
                "else null end EightHrAtlanta, " &
                "case " &
                "when SUBSTRING(APBHeaderData.strAttainmentStatus, 3, 1) = '2' then '8-Hr Macon' " &
                "else null end EightHrMacon, " &
                "case " &
                "when SUBSTRING(APBHeaderData.strAttainmentStatus, 3, 1) = '0' then '8-Hr No' " &
                "else null end EightHrNo, " &
                "case " &
                "when SUBSTRING(APBHeaderData.strAttainmentStatus, 4, 1) = '1' then 'PM-2.5 Atlanta' " &
                "else null end PMAtlanta, " &
                "case " &
                "when SUBSTRING(APBHeaderData.strAttainmentStatus, 4, 1) = '2' then 'PM-2.5 Chattanooga' " &
                "else null end PMChattanooga, " &
                "case " &
                "when SUBSTRING(APBHeaderData.strAttainmentStatus, 4, 1) = '3' then 'PM-2.5 Floyd' " &
                "else null end PMFloyd, " &
                "case " &
                "when SUBSTRING(APBHeaderData.strAttainmentStatus, 4, 1) = '4' then 'PM-2.5 Macon' " &
                "else null end PMMacon, " &
                "case " &
                "when SUBSTRING(APBHeaderData.strAttainmentStatus, 4, 1) = '0' then 'PM-2.5 No' " &
                "else null end PMNo, "

                If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBHeaderData, "
                    SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                End If
            Else
                If chb1HrYes.Checked Then
                    SQLSelect = SQLSelect & "case " &
                    "when SUBSTRING(APBHeaderData.strAttainmentStatus, 2, 1) = '1' then '1-Hr Yes' " &
                    "Else null end OneHrYes, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        ' SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chb1HrNo.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(APBHeaderData.strAttainmentStatus, 2, 1) = '0' then '1-Hr No' " &
                    "Else null End OneHrNo, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chb1HrContribute.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(APBHeaderData.strAttainmentStatus, 2, 1) = '2' then '1-Hr Contribute' " &
                    "Else null end OneHrContribute, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chb8HrAtlanta.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(APBHeaderData.strAttainmentStatus, 3, 1) = '1' then '8-Hr Atlanta' " &
                    "else null end EightHrAtlanta, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chb8HrMacon.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(APBHeaderData.strAttainmentStatus, 3, 1) = '2' then '8-Hr Macon' " &
                    "else null end EightHrMacon, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chb8HrNo.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(APBHeaderData.strAttainmentStatus, 3, 1) = '0' then '8-Hr No' " &
                    "else null end EightHrNo, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbPMAtlanta.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(APBHeaderData.strAttainmentStatus, 4, 1) = '1' then 'PM-2.5 Atlanta' " &
                    "else null end PMAtlanta, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        ' SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbPMChattanooga.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(APBHeaderData.strAttainmentStatus, 4, 1) = '2' then 'PM-2.5 Chattanooga' " &
                    "else null end PMChattanooga, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '   SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbPMFloyd.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(APBHeaderData.strAttainmentStatus, 4, 1) = '3' then 'PM-2.5 Floyd' " &
                    "else null end PMFloyd, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbPMMacon.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(APBHeaderData.strAttainmentStatus, 4, 1) = '4' then 'PM-2.5 Macon' " &
                    "else null end PMMacon, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbPMNo.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(APBHeaderData.strAttainmentStatus, 4, 1) = '0' then 'PM-2.5 No' " &
                    "else null end PMNo, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
            End If
            If chbStateProgramCodes.Checked Then
                SQLSelect = SQLSelect & "case " &
                "when SUBSTRING(strStateProgramCodes, 1, 1) = '1' then 'NSR/PSD Major' " &
                "Else null end NSRPSD, " &
                "case " &
                "when SUBSTRING(strStateProgramCodes, 2, 1) = '1' then 'HAPs Major' " &
                "Else null end HAP, "

                If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBHeaderData, "
                    SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                End If
            Else
                If chbNSRPSDMajor.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strStateProgramCodes, 1, 1) = '1' then 'NSR/PSD Major' " &
                    "Else null end NSRPSD, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If

                If chbHAPMajor.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strStateProgramCodes, 2, 1) = '1' then 'HAPs Major' " &
                    "Else null end HAP, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '   SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
            End If

            If chbViewAirPrograms.Checked Then
                SQLSelect = SQLSelect &
                "case " &
                "when SUBSTRING(strAirProgramCodes, 1, 1) = '1' then '0 - SIP' " &
                "Else null end APC0, " &
                "case " &
                "when SUBSTRING(strAirProgramCodes, 2, 1) = '1' then '1 - Federal SIP' " &
                "Else null end APC1, " &
                "case " &
                "when SUBSTRING(strAirProgramCodes, 3, 1) = '1' then '3 - Non-Fed' " &
                "Else null end APC3, " &
                "case " &
                "when SUBSTRING(strAirProgramCodes, 4, 1) = '1' then '4 - CFC Tracking' " &
                "Else null end APC4, " &
                "case " &
                "when SUBSTRING(strAirProgramCodes, 5, 1) = '1' then '6 - PSD' " &
                "Else null end APC6, " &
                "case " &
                "when SUBSTRING(strAirProgramCodes, 6, 1) = '1' then '7 - NSR' " &
                "Else null end APC7, " &
                "case " &
                "when SUBSTRING(strAirProgramCodes, 7, 1) = '1' then '8 - NESHAP' " &
                "Else null end APC8, " &
                "case " &
                "when SUBSTRING(strAirProgramCodes, 8, 1) = '1' then '9 - NSPS' " &
                "Else null end APC9, " &
                "case " &
                "when SUBSTRING(strAirProgramCodes, 9, 1) = '1' then 'A - Acid Precipitation' " &
                "Else null end APCA, " &
                "case " &
                "when SUBSTRING(strAirProgramCodes, 10, 1) = '1' then 'F - FESHOP' " &
                "Else null end APCF, " &
                "case " &
                "when SUBSTRING(strAirProgramCodes, 11, 1) = '1' then 'I - Native American' " &
                "Else null end APCI, " &
                "case " &
                "when SUBSTRING(strAirProgramCodes, 12, 1) = '1' then 'M - MACT' " &
                "Else null end APCM, " &
                "case " &
                "when SUBSTRING(strAirProgramCodes, 13, 1) = '1' then 'V - Title V' " &
                "Else null end APCV, "

                If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                    ' SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBHeaderData, "
                    SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                End If
            Else
                If chbAPC0.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strAirProgramCodes, 1, 1) = '1' then '0 - SIP' " &
                    "Else null end APC0, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '    SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPC1.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strAirProgramCodes, 2, 1) = '1' then '1 - Federal SIP' " &
                    "Else null end APC1, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '   SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPC3.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strAirProgramCodes, 3, 1) = '1' then '3 - Non-Fed' " &
                    "Else null end APC3, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        ' SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPC4.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strAirProgramCodes, 4, 1) = '1' then '4 - CFC Tracking' " &
                    "Else null end APC4, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '   SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPC6.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strAirProgramCodes, 5, 1) = '1' then '6 - PSD' " &
                    "Else null end APC6, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '  SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPC7.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strAirProgramCodes, 6, 1) = '1' then '7 - NSR' " &
                    "Else null end APC7, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        ' SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPC8.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strAirProgramCodes, 7, 1) = '1' then '8 - NESHAP' " &
                    "Else null end APC8, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '      SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPC9.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strAirProgramCodes, 8, 1) = '1' then '9 - NSPS' " &
                    "Else null end APC9, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '      SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPCA.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strAirProgramCodes, 9, 1) = '1' then 'A - Acid Precipitation' " &
                    "Else null end APCA, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '         SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPCF.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strAirProgramCodes, 10, 1) = '1' then 'F - FESHOP' " &
                    "Else null end APCF, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '      SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPCI.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strAirProgramCodes, 11, 1) = '1' then 'I - Native American' " &
                    "Else null end APCI, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '        SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPCM.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strAirProgramCodes, 12, 1) = '1' then 'M - MACT' " &
                    "Else null end APCM, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '   SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
                If chbAPCV.Checked Then
                    SQLSelect = SQLSelect &
                    "case " &
                    "when SUBSTRING(strAirProgramCodes, 13, 1) = '1' then 'V - Title V' " &
                    "Else null end APCV, "

                    If SQLFrom.IndexOf("APBHeaderData") <> -1 Then
                        '   SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBHeaderData, "
                        SQLWhere = SQLWhere & " and APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber "
                    End If
                End If
            End If

            If chbAllSubparts.Checked Then
                SQLSelect = SQLSelect &
                   " case " &
                   "when SUBSTRING(strSubPartKey, 13, 1) = '0' and APBSUBPARTDATA.ACTIVE = 1 then strSubPart " &
                   "end GASIP, " &
                   "case " &
                   "when SUBSTRING(strSubPartKey, 13, 1) = '8' and APBSUBPARTDATA.ACTIVE = 1 then strSubPart " &
                   "end Part61, " &
                   "case " &
                   "when SUBSTRING(strSubPartKey, 13, 1) = '9' and APBSUBPARTDATA.ACTIVE = 1 then strSubPart " &
                   "end Part60, " &
                   "case " &
                   "when SUBSTRING(strSubPartKey, 13, 1) = 'M' and APBSUBPARTDATA.ACTIVE = 1 then strSubPart " &
                   "End Part63, "

                If SQLFrom.IndexOf("APBSubPartData") <> -1 Then
                    '      SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " APBSubPartData, "
                    SQLWhere = SQLWhere &
                    " and APBFacilityInformation.strAIRSNumber = APBSubPartData.strAIRSNumber " &
                    " and strSubPart Is not Null and apbsubpartdata.active = '1' "
                End If
            Else
                If chbSIP.Checked Then
                    SQLSelect = SQLSelect &
                    " case " &
                    "when SUBSTRING(strSubPartKey, 13, 1) = '0' and APBSUBPARTDATA.ACTIVE = 1 then strSubPart " &
                    "end GASIP, "

                    If SQLFrom.IndexOf("APBSubPartData") <> -1 Then
                        '          SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBSubPartData, "
                        SQLWhere = SQLWhere &
                        " and APBFacilityInformation.strAIRSNumber = APBSubPartData.strAIRSNumber " &
                        " and strSubPart Is not Null "
                    End If
                End If
                If chbPart61Subpart.Checked Then
                    SQLSelect = SQLSelect &
                    " case " &
                    "when SUBSTRING(strSubPartKey, 13, 1) = '8' and APBSUBPARTDATA.ACTIVE = 1 then strSubPart " &
                    "end Part61, "

                    If SQLFrom.IndexOf("APBSubPartData") <> -1 Then
                        '         SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBSubPartData, "
                        SQLWhere = SQLWhere &
                        " and APBFacilityInformation.strAIRSNumber = APBSubPartData.strAIRSNumber " &
                        " and strSubPart Is not Null and apbsubpartdata.active = '1' "
                    End If
                End If
                If chbPart60Subpart.Checked Then
                    SQLSelect = SQLSelect &
                    " case " &
                    "when SUBSTRING(strSubPartKey, 13, 1) = '9' and APBSUBPARTDATA.ACTIVE = 1 then strSubPart " &
                    "end Part60, "

                    If SQLFrom.IndexOf("APBSubPartData") <> -1 Then
                        '       SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBSubPartData, "
                        SQLWhere = SQLWhere &
                        " and APBFacilityInformation.strAIRSNumber = APBSubPartData.strAIRSNumber " &
                        " and strSubPart Is not Null and apbsubpartdata.active = '1' "
                    End If
                End If
                If chbPart63Subpart.Checked Then
                    SQLSelect = SQLSelect &
                    " case " &
                    "when SUBSTRING(strSubPartKey, 13, 1) = 'M' and APBSUBPARTDATA.ACTIVE = 1 then strSubPart " &
                    "end Part63, "

                    If SQLFrom.IndexOf("APBSubPartData") <> -1 Then
                        '       SQLFrom = SQLFrom
                    Else
                        SQLFrom = SQLFrom & " APBSubPartData, "
                        SQLWhere = SQLWhere &
                        " and APBFacilityInformation.strAIRSNumber = APBSubPartData.strAIRSNumber " &
                        " and strSubPart Is not Null and apbsubpartdata.active = '1' "
                    End If
                End If
            End If

            If chbLastFCE.Checked Then
                SQLSelect = SQLSelect &
                "LastFCE, "

                If SQLFrom.IndexOf("VW_SSCP_MT_FacilityAssignment") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " VW_SSCP_MT_FacilityAssignment, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = VW_SSCP_MT_FacilityAssignment.strAIRSNumber "
                End If
            End If

            If chbSSCPUnit.Checked Then
                SQLSelect = SQLSelect &
                "strUnitDesc, "

                If SQLFrom.IndexOf("VW_SSCP_MOSTRECENTASSIGNMENT") <> -1 Then
                    '     SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " VW_SSCP_MOSTRECENTASSIGNMENT, "
                    SQLWhere = SQLWhere & " and APBMasterAIRS.strAIRSNumber = VW_SSCP_MOSTRECENTASSIGNMENT.strAIRSNumber "
                End If
                If SQLFrom.IndexOf("LookUpEPDUnits") <> -1 Then
                    '   SQLFrom = SQLFrom
                Else
                    SQLFrom = SQLFrom & " LookUpEPDUnits, "
                    SQLWhere = SQLWhere & " and VW_SSCP_MOSTRECENTASSIGNMENT.numSSCPUnit = LookUpEPDUnits.nuMUnitCode "
                End If
            End If




            'Adding Where to SQL Statement
            If rdbAIRSNumberOr.Checked Then
                SQLWhereCase1 = " OR "
            Else
                SQLWhereCase1 = " AND "
            End If
            If rdbAIRSNumberEqual.Checked Then
                SQLWhereCase2 = " Like "
            Else
                SQLWhereCase2 = " Not Like "
            End If

            If txtAIRSNumberSearch1.Text <> "" Then
                SQLWhere = SQLWhere & " and (APBMasterAIRS.strairsnumber " & SQLWhereCase2 & " @airs1) "
                params.Add(New SqlParameter("@airs1", "0413%" & txtAIRSNumberSearch1.Text & "%"))
            End If
            If txtAIRSNumberSearch2.Text <> "" Then
                If txtAIRSNumberSearch1.Text <> "" Then
                    SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                    " " & SQLWhereCase1 & " APBMasterAIRS.strairsnumber " & SQLWhereCase2 & " @airs2 ) "
                Else
                    SQLWhere = SQLWhere & " and (APBMasterAIRS.strairsNumber " & SQLWhereCase2 & " @airs2 ) "
                End If
                params.Add(New SqlParameter("@airs2", "0413%" & txtAIRSNumberSearch2.Text & "%"))
            End If

            If rdbFacilityNameOr.Checked Then
                SQLWhereCase1 = " OR "
            Else
                SQLWhereCase1 = " AND "
            End If
            If rdbFacilityNameEqual.Checked Then
                SQLWhereCase2 = " Like "
            Else
                SQLWhereCase2 = " Not Like "
            End If
            If txtFacilityNameSearch1.Text <> "" Then
                SQLWhere = SQLWhere & " and (strFacilityName " & SQLWhereCase2 & " @name1 ) "
                params.Add(New SqlParameter("@name1", "%" & txtFacilityNameSearch1.Text & "%"))
            End If
            If txtFacilityNameSearch2.Text <> "" Then
                If txtFacilityNameSearch1.Text <> "" Then
                    SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                    " " & SQLWhereCase1 & " strFacilityName " & SQLWhereCase2 & " @name2 ) "
                Else
                    SQLWhere = SQLWhere & " and (strFacilityName " & SQLWhereCase2 & " @name2) "
                End If
                params.Add(New SqlParameter("@name2", "%" & txtFacilityNameSearch2.Text & "%"))
            End If

            If chbFacilityStreet1.Checked Then
                If rdbFacilityStreet1Or.Checked Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbFacilityStreet1Equal.Checked Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If txtFacilityStreet1Search1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (strFacilityStreet1 " & SQLWhereCase2 & " @street1 ) "
                    params.Add(New SqlParameter("@street1", "%" & txtFacilityStreet1Search1.Text & "%"))
                End If
                If txtFacilityStreet1Search2.Text <> "" Then
                    If txtFacilityStreet1Search1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                        " " & SQLWhereCase1 & " strFacilityStreet1 " & SQLWhereCase2 & " @street2 ) "
                    Else
                        SQLWhere = SQLWhere & " and (strFacilityStreet1 " & SQLWhereCase2 & " @street2) "
                    End If
                    params.Add(New SqlParameter("@street2", "%" & txtFacilityStreet1Search2.Text & "%"))
                End If
            End If

            If chbFacilityStreet2.Checked Then
                If rdbFacilityStreet2Or.Checked Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbFacilityStreet2Equal.Checked Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If txtFacilityStreet2Search1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (strFacilityStreet2 " & SQLWhereCase2 & " @streetB1) "
                    params.Add(New SqlParameter("@streetB1", "%" & txtFacilityStreet2Search1.Text & "%"))
                End If
                If txtFacilityStreet2Search2.Text <> "" Then
                    If txtFacilityStreet2Search1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                        " " & SQLWhereCase1 & " strFacilityStreet2 " & SQLWhereCase2 & " @streetB2 ) "
                    Else
                        SQLWhere = SQLWhere & " and (strFacilityStreet2 " & SQLWhereCase2 & " @streetB2) "
                    End If
                    params.Add(New SqlParameter("@streetB2", "%" & txtFacilityStreet2Search2.Text & "%"))
                End If
            End If

            If chbFacilityCity.Checked Then
                If rdbFacilityCityOr.Checked Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbFacilityCityEqual.Checked Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If txtFacilityCitySearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (strFacilityCity " & SQLWhereCase2 & " @city1) "
                    params.Add(New SqlParameter("@city1", "%" & txtFacilityCitySearch1.Text & "%"))
                End If
                If txtFacilityCitySearch2.Text <> "" Then
                    If txtFacilityCitySearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                        " " & SQLWhereCase1 & " strFacilityCity " & SQLWhereCase2 & " @city2 ) "
                    Else
                        SQLWhere = SQLWhere & " and (strFacilityCity " & SQLWhereCase2 & " @city2) "
                    End If
                    params.Add(New SqlParameter("@city2", "%" & txtFacilityCitySearch2.Text & "%"))
                End If
            End If

            If chbFacilityZipCode.Checked Then
                If rdbFacilityZipCodeOr.Checked Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbFacilityZipCodeEqual.Checked Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If txtFacilityZipCodeSearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (strFacilityZipCode " & SQLWhereCase2 & " @zip1) "
                    params.Add(New SqlParameter("@zip1", "%" & txtFacilityZipCodeSearch1.Text & "%"))
                End If
                If txtFacilityZipCodeSearch2.Text <> "" Then
                    If txtFacilityZipCodeSearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                        " " & SQLWhereCase1 & " strFacilityZipCode " & SQLWhereCase2 & " @zip2 ) "
                    Else
                        SQLWhere = SQLWhere & " and (strFacilityzipcode " & SQLWhereCase2 & " @zip2) "
                    End If
                    params.Add(New SqlParameter("@zip2", "%" & txtFacilityZipCodeSearch2.Text & "%"))
                End If
            End If

            If chbFacilityLatitude.Checked AndAlso
                (txtFacilityLatitudeSearch1.Text <> "" OrElse txtFacilityLatitudeSearch2.Text <> "") Then
                If txtFacilityLatitudeSearch1.Text <> "" AndAlso txtFacilityLatitudeSearch2.Text = "" Then
                    params.Add(New SqlParameter("@lat1", txtFacilityLatitudeSearch1.Text))
                    params.Add(New SqlParameter("@lat2", txtFacilityLatitudeSearch1.Text))
                End If
                If txtFacilityLatitudeSearch1.Text = "" AndAlso txtFacilityLatitudeSearch2.Text <> "" Then
                    params.Add(New SqlParameter("@lat1", txtFacilityLatitudeSearch2.Text))
                    params.Add(New SqlParameter("@lat2", txtFacilityLatitudeSearch2.Text))
                    SQLWhereCase1 = txtFacilityLatitudeSearch2.Text
                    SQLWhereCase2 = txtFacilityLatitudeSearch2.Text
                End If
                If txtFacilityLatitudeSearch1.Text <> "" AndAlso txtFacilityLatitudeSearch2.Text <> "" Then
                    params.Add(New SqlParameter("@lat1", txtFacilityLatitudeSearch1.Text))
                    params.Add(New SqlParameter("@lat2", txtFacilityLatitudeSearch2.Text))
                End If
                SQLWhere = SQLWhere & " and (numFacilityLatitude between @lat1 and @lat2 or " &
                    " numFacilityLatitude between @lat2 and @lat1 ) "
            End If

            If chbFacilityLongitude.Checked AndAlso
                ((txtFacilityLongitudeSearch1.Text <> "" AndAlso IsNumeric(txtFacilityLongitudeSearch1.Text)) OrElse
                (txtFacilityLongitudeSearch2.Text <> "" AndAlso IsNumeric(txtFacilityLongitudeSearch2.Text))) Then

                If (txtFacilityLongitudeSearch1.Text <> "" AndAlso IsNumeric(txtFacilityLongitudeSearch1.Text)) Then
                    params.Add(New SqlParameter("@long1", -Math.Abs(CType(txtFacilityLongitudeSearch1.Text, Decimal))))
                Else
                    params.Add(New SqlParameter("@long1", 0))
                End If

                If (txtFacilityLongitudeSearch2.Text <> "" AndAlso IsNumeric(txtFacilityLongitudeSearch2.Text)) Then
                    params.Add(New SqlParameter("@long2", -Math.Abs(CType(txtFacilityLongitudeSearch2.Text, Decimal))))
                Else
                    params.Add(New SqlParameter("@long2", 0))
                End If

                SQLWhere = SQLWhere & " and (numFacilityLongitude between @long1 and @long2 or " &
                    " numFacilityLongitude between @long2 and @long1 ) "
            End If

            If chbCounty.Checked Then
                If rdbCountyOr.Checked Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbCountyEqual.Checked Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If cboCountySearch1.SelectedIndex > -1 Then
                    SQLWhere = SQLWhere & " and (SUBSTRING(APBMasterAIRS.strAIRSNumber, 5, 3) " & SQLWhereCase2 & " @county1 ) "
                    params.Add(New SqlParameter("@county1", cboCountySearch1.SelectedValue))
                End If
                If cboCountySearch2.SelectedIndex > -1 Then
                    If cboCountySearch1.SelectedIndex > -1 Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                        " " & SQLWhereCase1 & " SUBSTRING(APBMasterAIRS.strAIRSNumber, 5, 3) " & SQLWhereCase2 & " @county2 ) "
                    Else
                        SQLWhere = SQLWhere & " and (SUBSTRING(APBMasterAIRS.strAIRSNumber, 5, 3) " & SQLWhereCase2 & " @county2) "
                    End If
                    params.Add(New SqlParameter("@county2", cboCountySearch2.SelectedValue))
                End If
            End If

            If chbSSCPEngineer.Checked Then
                If rdbSSCPEngineerOr.Checked Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbSSCPEngineerEqual.Checked Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If cboSSCPEngineerSearch1.SelectedIndex > -1 Then
                    SQLWhere = SQLWhere & " and (VW_SSCP_MOSTRECENTASSIGNMENT.numSSCPEngineer " & SQLWhereCase2 & " @eng1) "
                    params.Add(New SqlParameter("@eng1", cboSSCPEngineerSearch1.SelectedValue))
                End If
                If cboSSCPEngineerSearch2.SelectedIndex > -1 Then
                    If cboSSCPEngineerSearch1.SelectedIndex > -1 Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                        " " & SQLWhereCase1 & " VW_SSCP_MOSTRECENTASSIGNMENT.numSSCPEngineer " & SQLWhereCase2 & " @eng2 ) "
                    Else
                        SQLWhere = SQLWhere & " and (VW_SSCP_MOSTRECENTASSIGNMENT.numSSCPEngineer " & SQLWhereCase2 & " @eng2) "
                    End If
                    params.Add(New SqlParameter("@eng2", cboSSCPEngineerSearch2.SelectedValue))
                End If
            End If

            If chbDistrict.Checked Then
                If rdbDistrictOr.Checked Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbDistrictEqual.Checked Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If cboDistrictSearch1.SelectedIndex > -1 Then
                    SQLWhere = SQLWhere & " and (LookUpDistrictInformation.strDistrictCode " & SQLWhereCase2 & " @dist1) "
                    params.Add(New SqlParameter("@dist1", cboDistrictSearch1.SelectedValue))
                End If
                If cboDistrictSearch2.SelectedIndex > -1 Then
                    If cboDistrictSearch1.SelectedIndex > -1 Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                        " " & SQLWhereCase1 & " LookUpDistrictInformation.strDistrictCode " & SQLWhereCase2 & " @dist2 ) "
                    Else
                        SQLWhere = MasterSQL & " and (LookUpDistrictInformation.strDistrictCode " & SQLWhereCase2 & " @dist2) "
                    End If
                    params.Add(New SqlParameter("@dist2", cboDistrictSearch2.SelectedValue))
                End If
            End If

            If chbOperationStatus.Checked Then
                If rdbOperationalStatusOr.Checked Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbOperationStatusEqual.Checked Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If cboOperationStatusSearch1.Text <> "" AndAlso cboOperationStatusSearch1.Text <> " " Then
                    SQLWhere = SQLWhere & " and (APBHeaderdata.strOperationalStatus " & SQLWhereCase2 & " @op1) "
                    params.Add(New SqlParameter("@op1", "%" & Mid(cboOperationStatusSearch1.Text, 1, 1) & "%"))
                End If
                If cboOperationStatusSearch2.Text <> "" AndAlso cboOperationStatusSearch2.Text <> " " Then
                    If cboOperationStatusSearch1.Text <> "" AndAlso cboOperationStatusSearch1.Text <> " " Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                        " " & SQLWhereCase1 & " APBHeaderdata.strOperationalStatus " & SQLWhereCase2 & " @op2 ) "
                    Else
                        SQLWhere = SQLWhere & " and (APBHeaderdata.strOperationalStatus " & SQLWhereCase2 & " @op2) "
                    End If
                    params.Add(New SqlParameter("@op2", "%" & Mid(cboOperationStatusSearch2.Text, 1, 1) & "%"))
                End If
            End If

            If chbClassification.Checked Then
                If rdbClassificationOr.Checked Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbClassificationEqual.Checked Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If cboClassificationSearch1.Text <> "" AndAlso cboClassificationSearch1.Text <> " " Then
                    SQLWhere = SQLWhere & " and (APBHeaderdata.strClass " & SQLWhereCase2 & " @class1) "
                    params.Add(New SqlParameter("@class1", "%" & Mid(cboClassificationSearch1.Text, 1, 1) & "%"))
                End If
                If cboClassificationSearch2.Text <> "" AndAlso cboClassificationSearch2.Text <> " " Then
                    If cboClassificationSearch1.Text <> "" AndAlso cboClassificationSearch1.Text <> " " Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                        " " & SQLWhereCase1 & " APBHeaderdata.strClass " & SQLWhereCase2 & " @class2 ) "
                    Else
                        SQLWhere = SQLWhere & " and (APBHeaderdata.strClass " & SQLWhereCase2 & " @class2) "
                    End If
                    params.Add(New SqlParameter("@class2", "%" & Mid(cboClassificationSearch2.Text, 1, 1) & "%"))
                End If
            End If

            If chbSICCode.Checked Then
                If rdbSICCodeOr.Checked Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbSICCodeEqual.Checked Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If txtSICCodeSearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (APBHeaderdata.strSICCode " & SQLWhereCase2 & " @sic1) "
                    params.Add(New SqlParameter("@sic1", "%" & txtSICCodeSearch1.Text & "%"))
                End If
                If txtSICCodeSearch2.Text <> "" Then
                    If txtSICCodeSearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                        " " & SQLWhereCase1 & " APBHeaderdata.strSICCode " & SQLWhereCase2 & " @sic2 ) "
                    Else
                        SQLWhere = SQLWhere & " and (APBHeaderdata.strSICCode " & SQLWhereCase2 & " @sic2) "
                    End If
                    params.Add(New SqlParameter("@sic2", "%" & txtSICCodeSearch2.Text & "%"))
                End If
            End If

            If chbNAICSCode.Checked Then
                If rdbNAICSCodeOr.Checked Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbNAICSCodeEqual.Checked Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If txtNAICSCodeSearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (APBHeaderdata.strNAICSCode " & SQLWhereCase2 & " @naics1) "
                    params.Add(New SqlParameter("@naics1", "%" & txtNAICSCodeSearch1.Text & "%"))
                End If
                If txtNAICSCodeSearch2.Text <> "" Then
                    If txtNAICSCodeSearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                        " " & SQLWhereCase1 & " APBHeaderdata.strNAICSCode " & SQLWhereCase2 & " @naics2 ) "
                    Else
                        SQLWhere = SQLWhere & " and (APBHeaderdata.strNAICSCode " & SQLWhereCase2 & " @naics2) "
                    End If
                    params.Add(New SqlParameter("@naics2", "%" & txtNAICSCodeSearch2.Text & "%"))
                End If
            End If

            If chbStartUpDate.Checked AndAlso
                (DTPStartUpDateSearch1.Checked OrElse DTPStartUpDateSearch2.Checked) Then
                If DTPStartUpDateSearch1.Checked AndAlso Not DTPStartUpDateSearch2.Checked Then
                    params.Add(New SqlParameter("@stdate1", DTPStartUpDateSearch1.Value))
                    params.Add(New SqlParameter("@stdate2", DTPStartUpDateSearch1.Value))
                End If
                If Not DTPStartUpDateSearch1.Checked AndAlso DTPStartUpDateSearch2.Checked Then
                    params.Add(New SqlParameter("@stdate1", DTPStartUpDateSearch2.Value))
                    params.Add(New SqlParameter("@stdate2", DTPStartUpDateSearch2.Value))
                End If
                If DTPStartUpDateSearch1.Checked AndAlso DTPStartUpDateSearch2.Checked Then
                    params.Add(New SqlParameter("@stdate1", DTPStartUpDateSearch1.Value))
                    params.Add(New SqlParameter("@stdate2", DTPStartUpDateSearch2.Value))
                End If
                SQLWhere = SQLWhere & " and datStartUpDate between @stdate1 and @stdate2 "
            End If

            If chbShutDownDate.Checked AndAlso
                (DTPShutDownDateSearch1.Checked OrElse DTPShutDownDateSearch2.Checked) Then

                If DTPShutDownDateSearch1.Checked AndAlso Not DTPShutDownDateSearch2.Checked Then
                    params.Add(New SqlParameter("@shdate1", DTPShutDownDateSearch1.Value))
                    params.Add(New SqlParameter("@shdate2", DTPShutDownDateSearch1.Value))
                End If
                If Not DTPShutDownDateSearch1.Checked AndAlso DTPShutDownDateSearch2.Checked Then
                    params.Add(New SqlParameter("@shdate1", DTPShutDownDateSearch2.Value))
                    params.Add(New SqlParameter("@shdate2", DTPShutDownDateSearch2.Value))
                End If
                If DTPShutDownDateSearch1.Checked AndAlso DTPShutDownDateSearch2.Checked Then
                    params.Add(New SqlParameter("@shdate1", DTPShutDownDateSearch1.Value))
                    params.Add(New SqlParameter("@shdate2", DTPShutDownDateSearch2.Value))
                End If
                SQLWhere = SQLWhere & " and datShutdownDate between @shdate1 and @shdate2 "
            End If

            If chbLastFCE.Checked AndAlso
                DTPLastFCESearch1.Checked OrElse DTPLastFCESearch2.Checked Then
                If DTPLastFCESearch1.Checked AndAlso Not DTPLastFCESearch2.Checked Then
                    params.Add(New SqlParameter("@fcedate1", DTPLastFCESearch1.Value))
                    params.Add(New SqlParameter("@fcedate2", DTPLastFCESearch1.Value))
                End If
                If Not DTPLastFCESearch1.Checked AndAlso DTPLastFCESearch2.Checked Then
                    params.Add(New SqlParameter("@fcedate1", DTPLastFCESearch2.Value))
                    params.Add(New SqlParameter("@fcedate2", DTPLastFCESearch2.Value))
                End If
                If DTPLastFCESearch1.Checked AndAlso DTPLastFCESearch2.Checked Then
                    params.Add(New SqlParameter("@fcedate1", DTPLastFCESearch1.Value))
                    params.Add(New SqlParameter("@fcedate2", DTPLastFCESearch2.Value))
                End If
                SQLWhere = SQLWhere & " and LastFCE between @fcedate1 and @fcedate2 "
            End If

            If chbCMSUniverse.Checked Then
                If rdbCMSUniverseOR.Checked Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbCMSUniverseEqual.Checked Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If cboCMSUniverseSearch1.SelectedIndex > -1 Then
                    SQLWhere = SQLWhere & " and (APBSupplamentalData.strCMSMember " & SQLWhereCase2 & " @cms1) "
                    params.Add(New SqlParameter("@cms1", cboCMSUniverseSearch1.Text))
                End If
                If cboCMSUniverseSearch2.SelectedIndex > -1 Then
                    If cboCMSUniverseSearch1.SelectedIndex > -1 Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                        " " & SQLWhereCase1 & " APBSupplamentalData.strCMSMember " & SQLWhereCase2 & " @cms2 ) "
                    Else
                        SQLWhere = SQLWhere & " and (APBSupplamentalData.strCMSMember " & SQLWhereCase2 & " @cms2) "
                    End If
                    params.Add(New SqlParameter("@cms2", cboCMSUniverseSearch2.Text))
                End If
            End If

            If chbPlantDescription.Checked Then
                If rdbPlantDescriptionOR.Checked Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbPlantDescriptionEqual.Checked Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If txtPlantDescriptionSearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (APBHeaderData.strPlantDescription " & SQLWhereCase2 & " @desc1) "
                    params.Add(New SqlParameter("@desc1", "%" & txtPlantDescriptionSearch1.Text & "%"))
                End If
                If txtPlantDescriptionSearch2.Text <> "" Then
                    If txtPlantDescriptionSearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                        " " & SQLWhereCase1 & " APBHeaderData.strPlantDescription " & SQLWhereCase2 & " @desc2 ) "
                    Else
                        SQLWhere = SQLWhere & " and (APBHeaderData.strPlantDescription " & SQLWhereCase2 & " @desc2) "
                    End If
                    params.Add(New SqlParameter("@desc2", "%" & txtPlantDescriptionSearch2.Text & "%"))
                End If
            End If


            If chbSSCPUnit.Checked Then
                If rdbSSCPUnitOr.Checked Then
                    SQLWhereCase1 = " OR "
                Else
                    SQLWhereCase1 = " AND "
                End If
                If rdbSSCPUnitEqual.Checked Then
                    SQLWhereCase2 = " Like "
                Else
                    SQLWhereCase2 = " Not Like "
                End If
                If cboSSCPUnitSearch1.Text <> "" Then
                    SQLWhere = SQLWhere & " and (strUnitDesc " & SQLWhereCase2 & " @sscpunit1) "
                    params.Add(New SqlParameter("@sscpunit1", cboSSCPUnitSearch1.Text))
                End If
                If cboSSCPUnitSearch2.Text <> "" Then
                    If cboSSCPUnitSearch1.Text <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 2)) &
                        " " & SQLWhereCase1 & " strUnitDesc " & SQLWhereCase2 & " @sscpunit2 ) "
                    Else
                        SQLWhere = SQLWhere & " and (strUnitDesc " & SQLWhereCase2 & " @sscpunit2) "
                    End If
                    params.Add(New SqlParameter("@sscpunit2", cboSSCPUnitSearch2.Text))
                End If
            End If


            If txtFacilityAIRSNumberOrder.Text <> "" OrElse txtFacilityNameOrder.Text <> "" _
                     OrElse txtFacilityStreet1Order.Text <> "" OrElse txtFacilityStreet2Order.Text <> "" _
                     OrElse txtFacilityCityOrder.Text <> "" OrElse txtFacilityZipCodeOrder.Text <> "" _
                     OrElse txtFacilityLatitudeOrder.Text <> "" OrElse txtFacilityLongitudeOrder.Text <> "" _
                     OrElse txtCountyOrder.Text <> "" OrElse txtDistrictOrder.Text <> "" _
                     OrElse txtOperationStatusOrder.Text <> "" OrElse txtClassificationOrder.Text <> "" _
                     OrElse txtSICCodeOrder.Text <> "" OrElse txtStartUpDateOrder.Text <> "" _
                     OrElse txtShutDownDateOrder.Text <> "" OrElse txtCMSUniverseOrder.Text <> "" _
                     OrElse txtPlantDescriptionOrder.Text <> "" OrElse txtAPC0Order.Text <> "" _
                     OrElse txtAPC1Order.Text <> "" OrElse txtAPC3Order.Text <> "" _
                     OrElse txtAPC4Order.Text <> "" OrElse txtAPC6Order.Text <> "" _
                     OrElse txtAPC7Order.Text <> "" OrElse txtAPC8Order.Text <> "" _
                     OrElse txtAPC9Order.Text <> "" OrElse txtAPCAOrder.Text <> "" _
                     OrElse txtAPCFOrder.Text <> "" OrElse txtAPCIOrder.Text <> "" _
                     OrElse txtAPCMOrder.Text <> "" OrElse txtAPCVOrder.Text <> "" Then
                i = 1
                If txtFacilityAIRSNumberOrder.Text <> "" AndAlso chbAIRSNumber.Checked Then
                    temp = temp & txtFacilityAIRSNumberOrder.Text & "-AIRSNumber, "
                    i += 1
                End If
                If txtFacilityNameOrder.Text <> "" AndAlso chbFacilityName.Checked Then
                    temp = temp & txtFacilityNameOrder.Text & "-strFacilityName, "
                    i += 1
                End If
                If txtFacilityStreet1Order.Text <> "" AndAlso chbFacilityStreet1.Checked Then
                    temp = temp & txtFacilityStreet1Order.Text & "-strFacilityStreet1, "
                    i += 1
                End If
                If txtFacilityStreet2Order.Text <> "" AndAlso chbFacilityStreet2.Checked Then
                    temp = temp & txtFacilityStreet2Order.Text & "-strFacilityStreet2, "
                    i += 1
                End If
                If txtFacilityCityOrder.Text <> "" AndAlso chbFacilityCity.Checked Then
                    temp = temp & txtFacilityCityOrder.Text & "-strFacilityCity, "
                    i += 1
                End If
                If txtFacilityZipCodeOrder.Text <> "" AndAlso chbFacilityZipCode.Checked Then
                    temp = temp & txtFacilityZipCodeOrder.Text & "-strFacilityZipCode, "
                    i += 1
                End If
                If txtFacilityLatitudeOrder.Text <> "" AndAlso chbFacilityLatitude.Checked Then
                    temp = temp & txtFacilityLatitudeOrder.Text & "-numFacilityLatitude, "
                    i += 1
                End If
                If txtFacilityLongitudeOrder.Text <> "" AndAlso chbFacilityLongitude.Checked Then
                    temp = temp & txtFacilityLongitudeOrder.Text & "-numFacilityLongitude, "
                    i += 1
                End If
                If txtCountyOrder.Text <> "" AndAlso chbCounty.Checked Then
                    temp = temp & txtCountyOrder.Text & "-strCountyName, "
                    i += 1
                End If
                If txtDistrictOrder.Text <> "" AndAlso chbDistrict.Checked Then
                    temp = temp & txtDistrictOrder.Text & "-strDistrictName, "
                    i += 1
                End If
                If txtOperationStatusOrder.Text <> "" AndAlso chbOperationStatus.Checked Then
                    temp = temp & txtOperationStatusOrder.Text & "-strOperationalStatus, "
                    i += 1
                End If
                If txtClassificationOrder.Text <> "" AndAlso chbClassification.Checked Then
                    temp = temp & txtClassificationOrder.Text & "-strClass, "
                    i += 1
                End If
                If txtSICCodeOrder.Text <> "" AndAlso chbSICCode.Checked Then
                    temp = temp & txtSICCodeOrder.Text & "-strSICCode, "
                    i += 1
                End If
                If txtNAICSCodeOrder.Text <> "" AndAlso chbNAICSCode.Checked Then
                    temp = temp & txtNAICSCodeOrder.Text & "-strNAICSCode, "
                    i += 1
                End If
                If txtStartUpDateOrder.Text <> "" AndAlso chbStartUpDate.Checked Then
                    temp = temp & txtStartUpDateOrder.Text & "-datStartUpDate, "
                    i += 1
                End If
                If txtShutDownDateOrder.Text <> "" AndAlso chbShutDownDate.Checked Then
                    temp = temp & txtShutDownDateOrder.Text & "-datShutDownDate, "
                    i += 1
                End If
                If txtLastFCEOrder.Text <> "" AndAlso chbLastFCE.Checked Then
                    temp = temp & txtLastFCEOrder.Text & "-LastFCE, "
                    i += 1
                End If
                If txtCMSUniverseOrder.Text <> "" AndAlso chbCMSUniverse.Checked Then
                    temp = temp & txtCMSUniverseOrder.Text & "-strCMSmember, "
                    i += 1
                End If
                If txtPlantDescriptionOrder.Text <> "" AndAlso chbPlantDescription.Checked Then
                    temp = temp & txtPlantDescriptionOrder.Text & "-strPlantDescription, "
                    i += 1
                End If
                If txtAPC0Order.Text <> "" AndAlso chbAPC0.Checked Then
                    temp = temp & txtAPC0Order.Text & "-APC0, "
                    i += 1
                End If
                If txtAPC1Order.Text <> "" AndAlso chbAPC1.Checked Then
                    temp = temp & txtAPC1Order.Text & "-APC1, "
                    i += 1
                End If
                If txtAPC3Order.Text <> "" AndAlso chbAPC3.Checked Then
                    temp = temp & txtAPC3Order.Text & "-APC3, "
                    i += 1
                End If
                If txtAPC4Order.Text <> "" AndAlso chbAPC4.Checked Then
                    temp = temp & txtAPC4Order.Text & "-APC4, "
                    i += 1
                End If
                If txtAPC6Order.Text <> "" AndAlso chbAPC6.Checked Then
                    temp = temp & txtAPC6Order.Text & "-APC6, "
                    i += 1
                End If
                If txtAPC7Order.Text <> "" AndAlso chbAPC7.Checked Then
                    temp = temp & txtAPC7Order.Text & "-APC7, "
                    i += 1
                End If
                If txtAPC8Order.Text <> "" AndAlso chbAPC8.Checked Then
                    temp = temp & txtAPC8Order.Text & "-APC8, "
                    i += 1
                End If
                If txtAPC9Order.Text <> "" AndAlso chbAPC9.Checked Then
                    temp = temp & txtAPC9Order.Text & "-APC9, "
                    i += 1
                End If
                If txtAPCAOrder.Text <> "" AndAlso chbAPCA.Checked Then
                    temp = temp & txtAPCAOrder.Text & "-APCA, "
                    i += 1
                End If
                If txtAPCFOrder.Text <> "" AndAlso chbAPCF.Checked Then
                    temp = temp & txtAPCFOrder.Text & "-APCF, "
                    i += 1
                End If
                If txtAPCIOrder.Text <> "" AndAlso chbAPCI.Checked Then
                    temp = temp & txtAPCIOrder.Text & "-APCI, "
                    i += 1
                End If
                If txtAPCMOrder.Text <> "" AndAlso chbAPCM.Checked Then
                    temp = temp & txtAPCMOrder.Text & "-APCM, "
                    i += 1
                End If
                If txtAPCVOrder.Text <> "" AndAlso chbAPCV.Checked Then
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

            query = Mid(SQLSelect, 1, (SQLSelect.Length - 2)) &
            Mid(SQLFrom, 1, (SQLFrom.Length - 2)) &
            SQLWhere

            MasterSQL = "Select distinct * " &
            "from (" & query & ") MasterSQL " &
            "Where AIRSNumber is Not Null "

            If chb1HrYes.Checked Then
                If rdb1HrYesEqual.Checked Then
                    MasterSQL = MasterSQL & " and OneHRYes is not null "
                Else
                    MasterSQL = MasterSQL & " and OneHRYes is null "
                End If
            End If

            If chb1HrNo.Checked Then
                If rdb1HrNoEqual.Checked Then
                    MasterSQL = MasterSQL & " and OneHRNo is not null "
                Else
                    MasterSQL = MasterSQL & " and OneHRNo is null "
                End If
            End If

            If chb1HrContribute.Checked Then
                If rdb1HrContributeEqual.Checked Then
                    MasterSQL = MasterSQL & " and OneHRContribute is not null "
                Else
                    MasterSQL = MasterSQL & " and OneHRContribute is null "
                End If
            End If

            If chb8HrAtlanta.Checked Then
                If rdb8HrAtlantaEqual.Checked Then
                    MasterSQL = MasterSQL & " and EightHRAtlanta is not null "
                Else
                    MasterSQL = MasterSQL & " and EightHRAtlanta is null "
                End If
            End If

            If chb8HrMacon.Checked Then
                If rdb8HrMaconEqual.Checked Then
                    MasterSQL = MasterSQL & " and EightHRMacon is not null "
                Else
                    MasterSQL = MasterSQL & " and EightHRMacon is null "
                End If
            End If

            If chb8HrNo.Checked Then
                If rdb8HrNoEqual.Checked Then
                    MasterSQL = MasterSQL & " and EightHRNo is not null "
                Else
                    MasterSQL = MasterSQL & " and EightHRNo is null "
                End If
            End If

            If chbPMAtlanta.Checked Then
                If rdbPMAtlantaEqual.Checked Then
                    MasterSQL = MasterSQL & " and PMAtlanta is not null "
                Else
                    MasterSQL = MasterSQL & " and PMAtlanta is null "
                End If
            End If

            If chbPMChattanooga.Checked Then
                If rdbPMChattanoogaEqual.Checked Then
                    MasterSQL = MasterSQL & " and PMChattanooga is not null "
                Else
                    MasterSQL = MasterSQL & " and PMChattanooga is null "
                End If
            End If

            If chbPMFloyd.Checked Then
                If rdbPMFloydEqual.Checked Then
                    MasterSQL = MasterSQL & " and PMFloyd is not null "
                Else
                    MasterSQL = MasterSQL & " and PMFloyd is null "
                End If
            End If

            If chbPMMacon.Checked Then
                If rdbPMMaconEqual.Checked Then
                    MasterSQL = MasterSQL & " and PMMacon is not null "
                Else
                    MasterSQL = MasterSQL & " and PMMacon is null "
                End If
            End If

            If chbPMNo.Checked Then
                If rdbPMNoEqual.Checked Then
                    MasterSQL = MasterSQL & " and PMNo is not null "
                Else
                    MasterSQL = MasterSQL & " and PMNo is null "
                End If
            End If

            If chbNSRPSDMajor.Checked Then
                If rdbNSRPSDMajorEqual.Checked Then
                    MasterSQL = MasterSQL & " and NSRPSD is not Null "
                Else
                    MasterSQL = MasterSQL & " and NSRPSD is Null "
                End If
            End If

            If chbHAPMajor.Checked Then
                If rdbHAPMajorEqual.Checked Then
                    MasterSQL = MasterSQL & " and HAP is not Null "
                Else
                    MasterSQL = MasterSQL & " and HAP is Null "
                End If
            End If

            If rdbAPCAnd.Checked Then
                If chbAPC0.Checked Then
                    If rdbAPC0Equal.Checked Then
                        MasterSQL = MasterSQL & " and APC0 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC0 is null "
                    End If
                End If
                If chbAPC1.Checked Then
                    If rdbAPC1Equal.Checked Then
                        MasterSQL = MasterSQL & " and APC1 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC1 is null "
                    End If
                End If
                If chbAPC3.Checked Then
                    If rdbAPC3Equal.Checked Then
                        MasterSQL = MasterSQL & " and APC3 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC3 is null "
                    End If
                End If
                If chbAPC4.Checked Then
                    If rdbAPC4Equal.Checked Then
                        MasterSQL = MasterSQL & " and APC4 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC4 is null "
                    End If
                End If
                If chbAPC6.Checked Then
                    If rdbAPC6Equal.Checked Then
                        MasterSQL = MasterSQL & " and APC6 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC6 is null "
                    End If
                End If
                If chbAPC7.Checked Then
                    If rdbAPC7Equal.Checked Then
                        MasterSQL = MasterSQL & " and APC7 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC7 is null "
                    End If
                End If
                If chbAPC8.Checked Then
                    If rdbAPC8Equal.Checked Then
                        MasterSQL = MasterSQL & " and APC8 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC8 is null "
                    End If
                End If
                If chbAPC9.Checked Then
                    If rdbAPC9Equal.Checked Then
                        MasterSQL = MasterSQL & " and APC9 is not null "
                    Else
                        MasterSQL = MasterSQL & " and APC9 is null "
                    End If
                End If
                If chbAPCA.Checked Then
                    If rdbAPCAEqual.Checked Then
                        MasterSQL = MasterSQL & " and APCA is not null "
                    Else
                        MasterSQL = MasterSQL & " and APCA is null "
                    End If
                End If
                If chbAPCF.Checked Then
                    If rdbAPCFEqual.Checked Then
                        MasterSQL = MasterSQL & " and APCF is not null "
                    Else
                        MasterSQL = MasterSQL & " and APCF is null "
                    End If
                End If
                If chbAPCI.Checked Then
                    If rdbAPCIEqual.Checked Then
                        MasterSQL = MasterSQL & " and APCI is not null "
                    Else
                        MasterSQL = MasterSQL & " and APCI is null "
                    End If
                End If
                If chbAPCM.Checked Then
                    If rdbAPCMEqual.Checked Then
                        MasterSQL = MasterSQL & " and APCM is not null "
                    Else
                        MasterSQL = MasterSQL & " and APCM is null "
                    End If
                End If
                If chbAPCV.Checked Then
                    If rdbAPCVEqual.Checked Then
                        MasterSQL = MasterSQL & " and APCV is not null "
                    Else
                        MasterSQL = MasterSQL & " and APCV is null "
                    End If
                End If
            Else
                If chbAPC0.Checked OrElse chbAPC1.Checked OrElse chbAPC3.Checked OrElse chbAPC4.Checked _
                  OrElse chbAPC6.Checked OrElse chbAPC7.Checked OrElse chbAPC8.Checked OrElse chbAPC9.Checked _
                    OrElse chbAPCA.Checked OrElse chbAPCF.Checked OrElse chbAPCI.Checked OrElse chbAPCM.Checked _
                      OrElse chbAPCV.Checked Then
                    MasterSQL = MasterSQL & " and ("

                    If chbAPC0.Checked Then
                        If rdbAPC0Equal.Checked Then
                            MasterSQL = MasterSQL & " APC0 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC0 is null or "
                        End If
                    End If
                    If chbAPC1.Checked Then
                        If rdbAPC1Equal.Checked Then
                            MasterSQL = MasterSQL & " APC1 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC1 is null or "
                        End If
                    End If
                    If chbAPC3.Checked Then
                        If rdbAPC3Equal.Checked Then
                            MasterSQL = MasterSQL & " APC3 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC3 is null or "
                        End If
                    End If
                    If chbAPC4.Checked Then
                        If rdbAPC4Equal.Checked Then
                            MasterSQL = MasterSQL & " APC4 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC4 is null or "
                        End If
                    End If
                    If chbAPC6.Checked Then
                        If rdbAPC6Equal.Checked Then
                            MasterSQL = MasterSQL & " APC6 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC6 is null or "
                        End If
                    End If
                    If chbAPC7.Checked Then
                        If rdbAPC7Equal.Checked Then
                            MasterSQL = MasterSQL & " APC7 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC7 is null or "
                        End If
                    End If
                    If chbAPC8.Checked Then
                        If rdbAPC8Equal.Checked Then
                            MasterSQL = MasterSQL & " APC8 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC8 is null or "
                        End If
                    End If
                    If chbAPC9.Checked Then
                        If rdbAPC9Equal.Checked Then
                            MasterSQL = MasterSQL & " APC9 is not null or "
                        Else
                            MasterSQL = MasterSQL & " APC9 is null or "
                        End If
                    End If
                    If chbAPCA.Checked Then
                        If rdbAPCAEqual.Checked Then
                            MasterSQL = MasterSQL & " APCA is not null or "
                        Else
                            MasterSQL = MasterSQL & " APCA is null or "
                        End If
                    End If
                    If chbAPCF.Checked Then
                        If rdbAPCFEqual.Checked Then
                            MasterSQL = MasterSQL & " APCF is not null or "
                        Else
                            MasterSQL = MasterSQL & " APCF is null or "
                        End If
                    End If
                    If chbAPCI.Checked Then
                        If rdbAPCIEqual.Checked Then
                            MasterSQL = MasterSQL & " APCI is not null or "
                        Else
                            MasterSQL = MasterSQL & " APCI is null or "
                        End If
                    End If
                    If chbAPCM.Checked Then
                        If rdbAPCMEqual.Checked Then
                            MasterSQL = MasterSQL & " APCM is not null or "
                        Else
                            MasterSQL = MasterSQL & " APCM is null or "
                        End If
                    End If
                    If chbAPCV.Checked Then
                        If rdbAPCVEqual.Checked Then
                            MasterSQL = MasterSQL & " APCV is not null or "
                        Else
                            MasterSQL = MasterSQL & " APCV is null or "
                        End If
                    End If
                    MasterSQL = Mid(MasterSQL, 1, (MasterSQL.Length - 3)) & " ) "
                End If
            End If

            If chbAllSubparts.Checked Then
                If chbSIP.Checked Then
                    If rdbSIPEqual.Checked Then
                        MasterSQL = MasterSQL & " and GASIP is not null "
                    Else
                        MasterSQL = MasterSQL & " and GASIP is null "
                    End If
                End If

                If chbPart61Subpart.Checked Then
                    If rdbPart61Equal.Checked Then
                        MasterSQL = MasterSQL & " and Part61 is not null "
                    Else
                        MasterSQL = MasterSQL & " and Part61 is null "
                    End If
                End If

                If chbPart60Subpart.Checked Then
                    If rdbPart60Equal.Checked Then
                        MasterSQL = MasterSQL & " and Part60 is not null "
                    Else
                        MasterSQL = MasterSQL & " and Part60 is null "
                    End If
                End If

                If chbPart63Subpart.Checked Then
                    If rdbPart63Equal.Checked Then
                        MasterSQL = MasterSQL & " and Part63 is not null "
                    Else
                        MasterSQL = MasterSQL & " and Part63 is null "
                    End If
                End If
            Else
                If chbSIP.Checked Then
                    If rdbSIPSubPartOr.Checked Then
                        SQLWhereCase1 = " OR "
                    Else
                        SQLWhereCase1 = " AND "
                    End If
                    If rdbSIPEqual.Checked Then
                        SQLWhereCase2 = " = "
                    Else
                        SQLWhereCase2 = " <> "
                    End If
                    If cboSIPSearch1.Text <> "" AndAlso cboSIPSearch1.Text <> " " Then
                        MasterSQL = MasterSQL & " and (GASIP " & SQLWhereCase2 & " @sip1 ) "
                        params.Add(New SqlParameter("@sip1", cboSIPSearch1.Text))
                    End If
                    If cboSIPSearch2.Text <> "" AndAlso cboSIPSearch2.Text <> " " Then
                        If cboSIPSearch1.Text <> "" AndAlso cboSIPSearch1.Text <> " " Then
                            MasterSQL = Mid(MasterSQL, 1, (MasterSQL.Length - 2)) &
                            " " & SQLWhereCase1 & " GASIP " & SQLWhereCase2 & " @sip2 ) "
                        Else
                            MasterSQL = MasterSQL & " and (GASIP " & SQLWhereCase2 & " @sip2) "
                        End If
                        params.Add(New SqlParameter("@sip2", cboSIPSearch2.Text))
                    End If
                End If

                If chbPart61Subpart.Checked Then
                    If rdbPart61SubPartOr.Checked Then
                        SQLWhereCase1 = " OR "
                    Else
                        SQLWhereCase1 = " AND "
                    End If
                    If rdbPart61Equal.Checked Then
                        SQLWhereCase2 = " = "
                    Else
                        SQLWhereCase2 = " <> "
                    End If
                    If cboPart61Search1.Text <> "" AndAlso cboPart61Search1.Text <> " " Then
                        MasterSQL = MasterSQL & " and (Part61 " & SQLWhereCase2 & " @p61a ) "
                        params.Add(New SqlParameter("@p61a", cboPart61Search1.Text))
                    End If
                    If cboPart61Search2.Text <> "" AndAlso cboPart61Search2.Text <> " " Then
                        If cboPart61Search1.Text <> "" AndAlso cboPart61Search1.Text <> " " Then
                            MasterSQL = Mid(MasterSQL, 1, (MasterSQL.Length - 2)) &
                            " " & SQLWhereCase1 & " Part61 " & SQLWhereCase2 & " @p61b ) "
                        Else
                            MasterSQL = MasterSQL & " and (Part61 " & SQLWhereCase2 & " @p61b) "
                        End If
                        params.Add(New SqlParameter("@p61b", cboPart61Search2.Text))
                    End If
                End If

                If chbPart60Subpart.Checked Then
                    If rdbPart60SubPartOr.Checked Then
                        SQLWhereCase1 = " OR "
                    Else
                        SQLWhereCase1 = " AND "
                    End If
                    If rdbPart60Equal.Checked Then
                        SQLWhereCase2 = " = "
                    Else
                        SQLWhereCase2 = " <> "
                    End If
                    If cboPart60Search1.Text <> "" AndAlso cboPart60Search1.Text <> " " Then
                        MasterSQL = MasterSQL & " and (Part60 " & SQLWhereCase2 & " '" & cboPart60Search1.Text & "' ) "
                        params.Add(New SqlParameter("@p60a", cboPart60Search1.Text))
                    End If
                    If cboPart60Search2.Text <> "" AndAlso cboPart60Search2.Text <> " " Then
                        If cboPart60Search1.Text <> "" AndAlso cboPart60Search1.Text <> " " Then
                            MasterSQL = Mid(MasterSQL, 1, (MasterSQL.Length - 2)) &
                            " " & SQLWhereCase1 & " Part60 " & SQLWhereCase2 & " '" & cboPart60Search2.Text & "' ) "
                        Else
                            MasterSQL = MasterSQL & " and (Part60 " & SQLWhereCase2 & " '" & cboPart60Search2.Text & "') "
                        End If
                        params.Add(New SqlParameter("@p60b", cboPart60Search2.Text))
                    End If
                End If

                If chbPart63Subpart.Checked Then
                    If rdbPart63SubPartOR.Checked Then
                        SQLWhereCase1 = " OR "
                    Else
                        SQLWhereCase1 = " AND "
                    End If
                    If rdbPart63Equal.Checked Then
                        SQLWhereCase2 = " = "
                    Else
                        SQLWhereCase2 = " <> "
                    End If
                    If cboPart63Search1.Text <> "" AndAlso cboPart63Search1.Text <> " " Then
                        MasterSQL = MasterSQL & " and (Part63 " & SQLWhereCase2 & " '" & cboPart63Search1.Text & "' ) "
                        params.Add(New SqlParameter("@p63a", cboPart63Search1.Text))
                    End If
                    If cboPart63Search2.Text <> "" AndAlso cboPart63Search2.Text <> " " Then
                        If cboPart63Search1.Text <> "" AndAlso cboPart63Search1.Text <> " " Then
                            MasterSQL = Mid(MasterSQL, 1, (MasterSQL.Length - 2)) &
                            " " & SQLWhereCase1 & " Part63 " & SQLWhereCase2 & " '" & cboPart63Search2.Text & "' ) "
                        Else
                            MasterSQL = MasterSQL & " and (Part63 " & SQLWhereCase2 & " '" & cboPart63Search2.Text & "') "
                        End If
                        params.Add(New SqlParameter("@p63b", cboPart63Search2.Text))
                    End If
                End If
                If chbSIP.Checked OrElse chbPart60Subpart.Checked OrElse chbPart61Subpart.Checked OrElse chbPart63Subpart.Checked Then
                    If chbSIP.Checked AndAlso chbPart60Subpart.Checked AndAlso chbPart61Subpart.Checked AndAlso chbPart63Subpart.Checked Then
                        MasterSQL = MasterSQL & " and (Part60 is not null or GASIP is not null or Part61 is not null or Part63 is not null) "
                    Else
                        MasterSQL = MasterSQL & " and ( "
                        If chbSIP.Checked Then
                            MasterSQL = MasterSQL & " GASIP is not Null or "
                        End If
                        If chbPart60Subpart.Checked Then
                            MasterSQL = MasterSQL & " Part60 is not Null or "
                        End If
                        If chbPart61Subpart.Checked Then
                            MasterSQL = MasterSQL & " Part61 is not Null or "
                        End If
                        If chbPart63Subpart.Checked Then
                            MasterSQL = MasterSQL & " Part63 is not Null or "
                        End If
                        MasterSQL = Mid(MasterSQL, 1, (MasterSQL.Length - 3)) & " ) "
                    End If
                End If
            End If

            MasterSQL = MasterSQL & Mid(SQLOrder, 1, (SQLOrder.Length - 2))

            dgvQueryGenerator.DataSource = DB.GetDataTable(MasterSQL, params.ToArray)

            Dim qParams As QueryGeneratorParameter() = params.Select(
                Function(p) New QueryGeneratorParameter() With {
                    .ParameterName = p.ParameterName,
                    .Value = p.Value.ToString()
                }).ToArray()

            Dim queryInfo As String = JSON.Serialize(
                New QueryGeneratorValues() With {
                    .Parameters = qParams,
                    .MasterSQL = MasterSQL
                })

            DAL.LogQuery(queryInfo, dgvQueryGenerator.Rows.Count)

            i = 0
            dgvQueryGenerator.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvQueryGenerator.Columns("AIRSNumber").DisplayIndex = i

            i += 1
            dgvQueryGenerator.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvQueryGenerator.Columns("strFacilityName").DisplayIndex = i

            i += 1

            If chbFacilityStreet1.Checked Then
                dgvQueryGenerator.Columns("strFacilityStreet1").HeaderText = "Street Address 1"
                dgvQueryGenerator.Columns("strFacilityStreet1").DisplayIndex = i
                i += 1
            End If
            If chbFacilityStreet2.Checked Then
                dgvQueryGenerator.Columns("strFacilityStreet2").HeaderText = "Street Address 2"
                dgvQueryGenerator.Columns("strFacilityStreet2").DisplayIndex = i
                i += 1
            End If
            If chbFacilityCity.Checked Then
                dgvQueryGenerator.Columns("strFacilityCity").HeaderText = "City"
                dgvQueryGenerator.Columns("strFacilityCity").DisplayIndex = i
                i += 1
            End If
            If chbFacilityZipCode.Checked Then
                dgvQueryGenerator.Columns("strFacilityZipCode").HeaderText = "Zip Code"
                dgvQueryGenerator.Columns("strFacilityZipCode").DisplayIndex = i
                i += 1
            End If
            If chbFacilityLatitude.Checked Then
                dgvQueryGenerator.Columns("numFacilityLatitude").HeaderText = "Latitude"
                dgvQueryGenerator.Columns("numFacilityLatitude").DisplayIndex = i
                i += 1
            End If
            If chbFacilityLongitude.Checked Then
                dgvQueryGenerator.Columns("numFacilityLongitude").HeaderText = "Longitude"
                dgvQueryGenerator.Columns("numFacilityLongitude").DisplayIndex = i
                i += 1
            End If
            If chbCounty.Checked Then
                dgvQueryGenerator.Columns("strCountyName").HeaderText = "County"
                dgvQueryGenerator.Columns("strCountyName").DisplayIndex = i
                i += 1
            End If
            If chbSSCPEngineer.Checked Then
                dgvQueryGenerator.Columns("SSCPEngineer").HeaderText = "Compliance Engineer"
                dgvQueryGenerator.Columns("SSCPEngineer").DisplayIndex = i
                i += 1
            End If
            If chbSSCPUnit.Checked Then
                dgvQueryGenerator.Columns("strUnitDesc").HeaderText = "Compliance Unit"
                dgvQueryGenerator.Columns("strUnitDesc").DisplayIndex = i
                i += 1
            End If
            If chbDistrict.Checked Then
                dgvQueryGenerator.Columns("strDistrictName").HeaderText = "District"
                dgvQueryGenerator.Columns("strDistrictName").DisplayIndex = i
                i += 1
            End If
            If chbOperationStatus.Checked Then
                dgvQueryGenerator.Columns("strOperationalStatus").HeaderText = "Operation Status"
                dgvQueryGenerator.Columns("strOperationalStatus").DisplayIndex = i
                i += 1
            End If
            If chbClassification.Checked Then
                dgvQueryGenerator.Columns("strClass").HeaderText = "Classification"
                dgvQueryGenerator.Columns("strClass").DisplayIndex = i
                i += 1
            End If
            If chbSICCode.Checked Then
                dgvQueryGenerator.Columns("strSICCode").HeaderText = "SIC"
                dgvQueryGenerator.Columns("strSICCode").DisplayIndex = i
                i += 1
            End If
            If chbNAICSCode.Checked Then
                dgvQueryGenerator.Columns("strNAICSCode").HeaderText = "NAICS"
                dgvQueryGenerator.Columns("strNAICSCode").DisplayIndex = i
                i += 1
            End If
            If chbStartUpDate.Checked Then
                dgvQueryGenerator.Columns("datStartUpDate").HeaderText = "Startup Date"
                dgvQueryGenerator.Columns("datStartUpDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvQueryGenerator.Columns("datStartUpDate").DisplayIndex = i
                i += 1
            End If
            If chbShutDownDate.Checked Then
                dgvQueryGenerator.Columns("datShutDownDate").HeaderText = "Permit Revocation Date"
                dgvQueryGenerator.Columns("datShutDownDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvQueryGenerator.Columns("datShutDownDate").DisplayIndex = i
                i += 1
            End If
            If chbLastFCE.Checked Then
                dgvQueryGenerator.Columns("LastFCE").HeaderText = "Last FCE"
                dgvQueryGenerator.Columns("LastFCE").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvQueryGenerator.Columns("LastFCE").DisplayIndex = i
                i += 1
            End If
            If chbCMSUniverse.Checked Then
                dgvQueryGenerator.Columns("strCMSMember").HeaderText = "CMS"
                dgvQueryGenerator.Columns("strCMSMember").DisplayIndex = i
                i += 1
            End If
            If chbPlantDescription.Checked Then
                dgvQueryGenerator.Columns("strPlantDescription").HeaderText = "Plant Description"
                dgvQueryGenerator.Columns("strPlantDescription").DisplayIndex = i
                i += 1
            End If
            If chbAttainmentStatus.Checked Then
                dgvQueryGenerator.Columns("OneHRYes").HeaderText = "One HR Yes"
                dgvQueryGenerator.Columns("OneHRYes").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("OneHRContribute").HeaderText = "One Hr Contributing"
                dgvQueryGenerator.Columns("OneHRContribute").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("OneHRNo").HeaderText = "One Hr No"
                dgvQueryGenerator.Columns("OneHRNo").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("EightHRAtlanta").HeaderText = "8-Hr Atlanta"
                dgvQueryGenerator.Columns("EightHRAtlanta").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("EightHRMacon").HeaderText = "8-Hr Macon"
                dgvQueryGenerator.Columns("EightHRMacon").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("EightHRNo").HeaderText = "8-Hr No"
                dgvQueryGenerator.Columns("EightHRNo").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("PMAtlanta").HeaderText = "PM Atlanta"
                dgvQueryGenerator.Columns("PMAtlanta").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("PMChattanooga").HeaderText = "PM Chattanooga"
                dgvQueryGenerator.Columns("PMChattanooga").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("PMMacon").HeaderText = "PM Macon"
                dgvQueryGenerator.Columns("PMMacon").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("PMFloyd").HeaderText = "PM Floyd"
                dgvQueryGenerator.Columns("PMFloyd").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("PMNo").HeaderText = "PM No"
                dgvQueryGenerator.Columns("PMNo").DisplayIndex = i
                i += 1
            Else
                If chb1HrYes.Checked Then
                    dgvQueryGenerator.Columns("OneHRYes").HeaderText = "One HR Yes"
                    dgvQueryGenerator.Columns("OneHRYes").DisplayIndex = i
                    i += 1
                End If
                If chb1HrContribute.Checked Then
                    dgvQueryGenerator.Columns("OneHRContribute").HeaderText = "One Hr Contributing"
                    dgvQueryGenerator.Columns("OneHRContribute").DisplayIndex = i
                    i += 1
                End If
                If chb1HrNo.Checked Then
                    dgvQueryGenerator.Columns("OneHRNo").HeaderText = "One Hr No"
                    dgvQueryGenerator.Columns("OneHRNo").DisplayIndex = i
                    i += 1
                End If
                If chb8HrAtlanta.Checked Then
                    dgvQueryGenerator.Columns("EightHRAtlanta").HeaderText = "8-Hr Atlanta"
                    dgvQueryGenerator.Columns("EightHRAtlanta").DisplayIndex = i
                    i += 1
                End If
                If chb8HrMacon.Checked Then
                    dgvQueryGenerator.Columns("EightHRMacon").HeaderText = "8-Hr Macon"
                    dgvQueryGenerator.Columns("EightHRMacon").DisplayIndex = i
                    i += 1
                End If
                If chb8HrNo.Checked Then
                    dgvQueryGenerator.Columns("EightHRNo").HeaderText = "8-Hr No"
                    dgvQueryGenerator.Columns("EightHRNo").DisplayIndex = i
                    i += 1
                End If
                If chbPMAtlanta.Checked Then
                    dgvQueryGenerator.Columns("PMAtlanta").HeaderText = "PM Atlanta"
                    dgvQueryGenerator.Columns("PMAtlanta").DisplayIndex = i
                    i += 1
                End If
                If chbPMChattanooga.Checked Then
                    dgvQueryGenerator.Columns("PMChattanooga").HeaderText = "PM Chattanooga"
                    dgvQueryGenerator.Columns("PMChattanooga").DisplayIndex = i
                    i += 1
                End If
                If chbPMFloyd.Checked Then
                    dgvQueryGenerator.Columns("PMFloyd").HeaderText = "PM Floyd"
                    dgvQueryGenerator.Columns("PMFloyd").DisplayIndex = i
                    i += 1
                End If
                If chbPMMacon.Checked Then
                    dgvQueryGenerator.Columns("PMMacon").HeaderText = "PM Macon"
                    dgvQueryGenerator.Columns("PMMacon").DisplayIndex = i
                    i += 1
                End If
                If chbPMNo.Checked Then
                    dgvQueryGenerator.Columns("PMNo").HeaderText = "PM No"
                    dgvQueryGenerator.Columns("PMNo").DisplayIndex = i
                    i += 1
                End If
            End If
            If chbStateProgramCodes.Checked Then
                dgvQueryGenerator.Columns("NSRPSD").HeaderText = "NSR/PSD"
                dgvQueryGenerator.Columns("NSRPSD").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("HAP").HeaderText = "HAPs"
                dgvQueryGenerator.Columns("HAP").DisplayIndex = i
                i += 1
            Else
                If chbNSRPSDMajor.Checked Then
                    dgvQueryGenerator.Columns("NSRPSD").HeaderText = "NSR/PSD"
                    dgvQueryGenerator.Columns("NSRPSD").DisplayIndex = i
                    i += 1
                End If
                If chbHAPMajor.Checked Then
                    dgvQueryGenerator.Columns("HAP").HeaderText = "HAPs"
                    dgvQueryGenerator.Columns("HAP").DisplayIndex = i
                    i += 1
                End If
            End If
            If chbViewAirPrograms.Checked Then
                dgvQueryGenerator.Columns("APC0").HeaderText = "0 - SIP"
                dgvQueryGenerator.Columns("APC0").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("APC1").HeaderText = "1 - Federal SIP"
                dgvQueryGenerator.Columns("APC1").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("APC3").HeaderText = "3 - Non-Federal SIP"
                dgvQueryGenerator.Columns("APC3").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("APC4").HeaderText = "4 - CFC Tracking"
                dgvQueryGenerator.Columns("APC4").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("APC6").HeaderText = "6 - PSD"
                dgvQueryGenerator.Columns("APC6").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("APC7").HeaderText = "7 - NSR"
                dgvQueryGenerator.Columns("APC7").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("APC8").HeaderText = "8 - NESHAP"
                dgvQueryGenerator.Columns("APC8").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("APC9").HeaderText = "9 - NSPS"
                dgvQueryGenerator.Columns("APC9").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("APCA").HeaderText = "A - Acid Precipitation"
                dgvQueryGenerator.Columns("APCA").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("APCF").HeaderText = "F - FESOP"
                dgvQueryGenerator.Columns("APCF").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("APCI").HeaderText = "I - Native American"
                dgvQueryGenerator.Columns("APCI").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("APCM").HeaderText = "M - MACT"
                dgvQueryGenerator.Columns("APCM").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("APCV").HeaderText = "V - Title V"
                dgvQueryGenerator.Columns("APCV").DisplayIndex = i
                i += 1
            Else
                If chbAPC0.Checked Then
                    dgvQueryGenerator.Columns("APC0").HeaderText = "0 - SIP"
                    dgvQueryGenerator.Columns("APC0").DisplayIndex = i
                    i += 1
                End If
                If chbAPC1.Checked Then
                    dgvQueryGenerator.Columns("APC1").HeaderText = "1 - Federal SIP"
                    dgvQueryGenerator.Columns("APC1").DisplayIndex = i
                    i += 1
                End If
                If chbAPC3.Checked Then
                    dgvQueryGenerator.Columns("APC3").HeaderText = "3 - Non-Federal SIP"
                    dgvQueryGenerator.Columns("APC3").DisplayIndex = i
                    i += 1
                End If
                If chbAPC4.Checked Then
                    dgvQueryGenerator.Columns("APC4").HeaderText = "4 - CFC Tracking"
                    dgvQueryGenerator.Columns("APC4").DisplayIndex = i
                    i += 1
                End If
                If chbAPC6.Checked Then
                    dgvQueryGenerator.Columns("APC6").HeaderText = "6 - PSD"
                    dgvQueryGenerator.Columns("APC6").DisplayIndex = i
                    i += 1
                End If
                If chbAPC7.Checked Then
                    dgvQueryGenerator.Columns("APC7").HeaderText = "7 - NSR"
                    dgvQueryGenerator.Columns("APC7").DisplayIndex = i
                    i += 1
                End If
                If chbAPC8.Checked Then
                    dgvQueryGenerator.Columns("APC8").HeaderText = "8 - NESHAP"
                    dgvQueryGenerator.Columns("APC8").DisplayIndex = i
                    i += 1
                End If
                If chbAPC9.Checked Then
                    dgvQueryGenerator.Columns("APC9").HeaderText = "9 - NSPS"
                    dgvQueryGenerator.Columns("APC9").DisplayIndex = i
                    i += 1
                End If
                If chbAPCA.Checked Then
                    dgvQueryGenerator.Columns("APCA").HeaderText = "A - Acid Precipitation"
                    dgvQueryGenerator.Columns("APCA").DisplayIndex = i
                    i += 1
                End If
                If chbAPCF.Checked Then
                    dgvQueryGenerator.Columns("APCF").HeaderText = "F - FESOP"
                    dgvQueryGenerator.Columns("APCF").DisplayIndex = i
                    i += 1
                End If
                If chbAPCI.Checked Then
                    dgvQueryGenerator.Columns("APCI").HeaderText = "I - Native American"
                    dgvQueryGenerator.Columns("APCI").DisplayIndex = i
                    i += 1
                End If
                If chbAPCM.Checked Then
                    dgvQueryGenerator.Columns("APCM").HeaderText = "M - MACT"
                    dgvQueryGenerator.Columns("APCM").DisplayIndex = i
                    i += 1
                End If
                If chbAPCV.Checked Then
                    dgvQueryGenerator.Columns("APCV").HeaderText = "V - Title V"
                    dgvQueryGenerator.Columns("APCV").DisplayIndex = i
                    i += 1
                End If
            End If
            If chbAllSubparts.Checked Then
                dgvQueryGenerator.Columns("GASIP").HeaderText = "GA SIP"
                dgvQueryGenerator.Columns("GASIP").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("Part61").HeaderText = "NESHAP"
                dgvQueryGenerator.Columns("Part61").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("Part60").HeaderText = "NSPS"
                dgvQueryGenerator.Columns("Part60").DisplayIndex = i
                i += 1
                dgvQueryGenerator.Columns("Part63").HeaderText = "MACT"
                dgvQueryGenerator.Columns("Part63").DisplayIndex = i
                i += 1
            Else
                If chbSIP.Checked Then
                    dgvQueryGenerator.Columns("GASIP").HeaderText = "GA SIP"
                    dgvQueryGenerator.Columns("GASIP").DisplayIndex = i
                    i += 1
                End If
                If chbPart61Subpart.Checked Then
                    dgvQueryGenerator.Columns("Part61").HeaderText = "NESHAP"
                    dgvQueryGenerator.Columns("Part61").DisplayIndex = i
                    i += 1
                End If
                If chbPart60Subpart.Checked Then
                    dgvQueryGenerator.Columns("Part60").HeaderText = "NSPS"
                    dgvQueryGenerator.Columns("Part60").DisplayIndex = i
                    i += 1
                End If
                If chbPart63Subpart.Checked Then
                    dgvQueryGenerator.Columns("Part63").HeaderText = "MACT"
                    dgvQueryGenerator.Columns("Part63").DisplayIndex = i
                    i += 1
                End If
            End If

        Catch ex As Exception
            Dim qParams As QueryGeneratorParameter() = params.Select(
                Function(p) New QueryGeneratorParameter() With {
                    .ParameterName = p.ParameterName,
                    .Value = p.Value.ToString()
                }).ToArray()

            Dim queryInfo As String = JSON.Serialize(
                New QueryGeneratorValues() With {
                    .Parameters = qParams,
                    .MasterSQL = MasterSQL
                })

            ErrorReport(ex, queryInfo, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Class QueryGeneratorValues
        Public Parameters As QueryGeneratorParameter()
        Public MasterSQL As String
    End Class

    Private Class QueryGeneratorParameter
        Public Value As String
        Public ParameterName As String
    End Class

    Sub ExportToExcel()
        dgvQueryGenerator.ExportToExcel(Me)
    End Sub

    Private Sub ResetForm()
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
            rdbCountyOr.Checked = True
            rdbCountyEqual.Checked = True
            txtCountyOrder.Clear()

            chbDistrict.Checked = False
            rdbDistrictOr.Checked = True
            rdbDistrictEqual.Checked = True
            txtDistrictOrder.Clear()

            chbOperationStatus.Checked = False
            rdbOperationalStatusOr.Checked = True
            rdbOperationStatusEqual.Checked = True
            txtOperationStatusOrder.Clear()

            chbClassification.Checked = False
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
            DTPStartUpDateSearch1.Value = Today
            DTPStartUpDateSearch2.Checked = False
            DTPStartUpDateSearch2.Value = Today
            rdbStartUpDateBetween.Checked = True
            txtStartUpDateOrder.Clear()

            chbShutDownDate.Checked = False
            DTPShutDownDateSearch1.Checked = False
            DTPShutDownDateSearch1.Value = Today
            DTPShutDownDateSearch2.Checked = False
            DTPShutDownDateSearch2.Value = Today
            rdbShutDownDateBetween.Checked = True
            txtShutDownDateOrder.Clear()

            chbCMSUniverse.Checked = False
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

            cboCountySearch1.SelectedIndex = -1
            cboCountySearch2.SelectedIndex = -1
            cboDistrictSearch1.SelectedIndex = -1
            cboDistrictSearch2.SelectedIndex = -1
            cboSIPSearch1.SelectedIndex = -1
            cboSIPSearch2.SelectedIndex = -1
            cboPart61Search1.SelectedIndex = -1
            cboPart61Search2.SelectedIndex = -1
            cboPart60Search1.SelectedIndex = -1
            cboPart60Search2.SelectedIndex = -1
            cboPart63Search1.SelectedIndex = -1
            cboPart63Search2.SelectedIndex = -1
            cboOperationStatusSearch1.SelectedIndex = -1
            cboOperationStatusSearch2.SelectedIndex = -1
            cboClassificationSearch1.SelectedIndex = -1
            cboClassificationSearch2.SelectedIndex = -1
            cboCMSUniverseSearch1.SelectedIndex = -1
            cboCMSUniverseSearch2.SelectedIndex = -1

            ' This repetition is intentional. If a ComboBox has an item selected, the above statements set
            ' the SelectedIndex to 0 instead of -1! Setting it a second time below forces the SelectedIndex
            ' to set to -1.

            cboCountySearch1.SelectedIndex = -1
            cboCountySearch2.SelectedIndex = -1
            cboDistrictSearch1.SelectedIndex = -1
            cboDistrictSearch2.SelectedIndex = -1
            cboSIPSearch1.SelectedIndex = -1
            cboSIPSearch2.SelectedIndex = -1
            cboPart61Search1.SelectedIndex = -1
            cboPart61Search2.SelectedIndex = -1
            cboPart60Search1.SelectedIndex = -1
            cboPart60Search2.SelectedIndex = -1
            cboPart63Search1.SelectedIndex = -1
            cboPart63Search2.SelectedIndex = -1
            cboOperationStatusSearch1.SelectedIndex = -1
            cboOperationStatusSearch2.SelectedIndex = -1
            cboClassificationSearch1.SelectedIndex = -1
            cboClassificationSearch2.SelectedIndex = -1
            cboCMSUniverseSearch1.SelectedIndex = -1
            cboCMSUniverseSearch2.SelectedIndex = -1

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub ResizeFilter()
        Try

            If tcQueryOptions.Size.Height > 27 Then
                tcQueryOptions.Size = New Size(tcQueryOptions.Size.Width, 27)
            Else
                tcQueryOptions.Size = New Size(tcQueryOptions.Size.Width, 389)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub UpdateDefaultSearch()
        Dim DefaultsText As String = ""

        Try

            If Me.chbAIRSNumber.Checked Then
                DefaultsText = DefaultsText & "AIRSNumber"
                If txtAIRSNumberSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtAIRSNumberSearch1.Text & "-#"
                End If
                If txtAIRSNumberSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtAIRSNumberSearch2.Text & "-%"
                End If
                If rdbAIRSNumberAnd.Checked Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbAIRSNumberEqual.Checked Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtFacilityAIRSNumberOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtFacilityAIRSNumberOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "rebmuNSRIA"
            End If
            If Me.chbFacilityName.Checked Then
                DefaultsText = DefaultsText & "FacilityName"
                If txtFacilityNameSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtFacilityNameSearch1.Text & "-#"
                End If
                If txtFacilityNameSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtFacilityNameSearch2.Text & "-%"
                End If
                If rdbFacilityNameAnd.Checked Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbFacilityNameEqual.Checked Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtFacilityNameOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtFacilityNameOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "emaNytilicaF"
            End If
            If Me.chbFacilityStreet1.Checked Then
                DefaultsText = DefaultsText & "Street1"
                If txtFacilityStreet1Search1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtFacilityStreet1Search1.Text & "-#"
                End If
                If txtFacilityStreet1Search2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtFacilityStreet1Search2.Text & "-%"
                End If
                If rdbFacilityStreet1And.Checked Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbFacilityStreet1Equal.Checked Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtFacilityStreet1Order.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtFacilityStreet1Order.Text & "-^"
                End If
                DefaultsText = DefaultsText & "1teertS"
            End If
            If Me.chbFacilityStreet2.Checked Then
                DefaultsText = DefaultsText & "Street2"
                If txtFacilityStreet2Search1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtFacilityStreet2Search1.Text & "-#"
                End If
                If txtFacilityStreet2Search2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtFacilityStreet2Search2.Text & "-%"
                End If
                If rdbFacilityStreet2And.Checked Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbFacilityStreet2Equal.Checked Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtFacilityStreet2Order.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtFacilityStreet2Order.Text & "-^"
                End If
                DefaultsText = DefaultsText & "2teertS"
            End If
            If Me.chbFacilityCity.Checked Then
                DefaultsText = DefaultsText & "City"
                If txtFacilityCitySearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtFacilityCitySearch1.Text & "-#"
                End If
                If txtFacilityCitySearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtFacilityCitySearch2.Text & "-%"
                End If
                If rdbFacilityCityAnd.Checked Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbFacilityCityEqual.Checked Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtFacilityCityOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtFacilityCityOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "ytiC"
            End If
            If Me.chbFacilityZipCode.Checked Then
                DefaultsText = DefaultsText & "ZipCode"
                If txtFacilityZipCodeSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtFacilityZipCodeSearch1.Text & "-#"
                End If
                If txtFacilityZipCodeSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtFacilityZipCodeSearch2.Text & "-%"
                End If
                If rdbFacilityZipCodeAnd.Checked Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbFacilityZipCodeEqual.Checked Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtFacilityZipCodeOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtFacilityZipCodeOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "edoCpiZ"
            End If
            If Me.chbFacilityLongitude.Checked Then
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
            If Me.chbFacilityLatitude.Checked Then
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
            If Me.chbCounty.Checked Then
                DefaultsText = DefaultsText & "County"
                If cboCountySearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & cboCountySearch1.Text & "-#"
                End If
                If cboCountySearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & cboCountySearch2.Text & "-%"
                End If
                If rdbCountyAnd.Checked Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbCountyEqual.Checked Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtCountyOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtCountyOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "ytnuoC"
            End If
            If Me.chbDistrict.Checked Then
                DefaultsText = DefaultsText & "District"
                If cboDistrictSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & cboDistrictSearch1.Text & "-#"
                End If
                If cboDistrictSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & cboDistrictSearch2.Text & "-%"
                End If
                If rdbDistrictAnd.Checked Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbDistrictEqual.Checked Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtDistrictOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtDistrictOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "tcirtsiD"
            End If
            If Me.chbOperationStatus.Checked Then
                DefaultsText = DefaultsText & "OpStatus"
                If cboOperationStatusSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & cboOperationStatusSearch1.Text & "-#"
                End If
                If cboOperationStatusSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & cboOperationStatusSearch2.Text & "-%"
                End If
                If rdbOperationalStatusAnd.Checked Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbOperationStatusEqual.Checked Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtOperationStatusOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtOperationStatusOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "sutatSpO"
            End If
            If Me.chbClassification.Checked Then
                DefaultsText = DefaultsText & "Classification"
                If cboClassificationSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & cboClassificationSearch1.Text & "-#"
                End If
                If cboClassificationSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & cboClassificationSearch2.Text & "-%"
                End If
                If rdbClassificationAnd.Checked Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbClassificationEqual.Checked Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtClassificationOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtClassificationOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "noitacifissalC"
            End If
            If Me.chbSICCode.Checked Then
                DefaultsText = DefaultsText & "SIC"
                If txtSICCodeSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtSICCodeSearch1.Text & "-#"
                End If
                If txtSICCodeSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtSICCodeSearch2.Text & "-%"
                End If
                If rdbSICCodeAnd.Checked Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbSICCodeEqual.Checked Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtSICCodeOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtSICCodeOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "CIS"
            End If
            If Me.chbNAICSCode.Checked Then
                DefaultsText = DefaultsText & "NAICS"
                If txtNAICSCodeSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtNAICSCodeSearch1.Text & "-#"
                End If
                If txtNAICSCodeSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtNAICSCodeSearch2.Text & "-%"
                End If
                If rdbNAICSCodeAnd.Checked Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbNAICSCodeEqual.Checked Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtNAICSCodeOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtNAICSCodeOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "SCIAN"
            End If
            If Me.chbStartUpDate.Checked Then
                DefaultsText = DefaultsText & "StartUp"
                If DTPStartUpDateSearch1.Checked Then
                    DefaultsText = DefaultsText & "#-" & DTPStartUpDateSearch1.Text & "-#"
                End If
                If DTPStartUpDateSearch2.Checked Then
                    DefaultsText = DefaultsText & "%-" & DTPStartUpDateSearch2.Text & "-%"
                End If
                If rdbStartUpDateBetween.Checked Then
                    DefaultsText = DefaultsText & "*-BETWEEN-*"
                End If
                If txtStartUpDateOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtStartUpDateOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "pUtratS"
            End If
            If Me.chbShutDownDate.Checked Then
                DefaultsText = DefaultsText & "ShutDown"
                If DTPShutDownDateSearch1.Checked Then
                    DefaultsText = DefaultsText & "#-" & DTPShutDownDateSearch1.Text & "-#"
                End If
                If DTPShutDownDateSearch2.Checked Then
                    DefaultsText = DefaultsText & "%-" & DTPShutDownDateSearch2.Text & "-%"
                End If
                If rdbShutDownDateBetween.Checked Then
                    DefaultsText = DefaultsText & "*-BETWEEN-*"
                End If
                If txtShutDownDateOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtShutDownDateOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "nwoDtuhS"
            End If
            If Me.chbCMSUniverse.Checked Then
                DefaultsText = DefaultsText & "CMS"
                If cboCMSUniverseSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & cboCMSUniverseSearch1.Text & "-#"
                End If
                If cboCMSUniverseSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & cboCMSUniverseSearch2.Text & "-%"
                End If
                If rdbCMSUniverseAnd.Checked Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbCMSUniverseEqual.Checked Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtCMSUniverseOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtCMSUniverseOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "SMC"
            End If
            If Me.chbPlantDescription.Checked Then
                DefaultsText = DefaultsText & "Plant"
                If txtPlantDescriptionSearch1.Text <> "" Then
                    DefaultsText = DefaultsText & "#-" & txtPlantDescriptionSearch1.Text & "-#"
                End If
                If txtPlantDescriptionSearch2.Text <> "" Then
                    DefaultsText = DefaultsText & "%-" & txtPlantDescriptionSearch2.Text & "-%"
                End If
                If rdbPlantDescriptionAND.Checked Then
                    DefaultsText = DefaultsText & "*-AND-*"
                Else
                    DefaultsText = DefaultsText & "*-OR-*"
                End If
                If rdbPlantDescriptionEqual.Checked Then
                    DefaultsText = DefaultsText & "@-EQUAL-@"
                Else
                    DefaultsText = DefaultsText & "@-NOTEQUAL-@"
                End If
                If txtPlantDescriptionOrder.Text <> "" Then
                    DefaultsText = DefaultsText & "^-" & txtPlantDescriptionOrder.Text & "-^"
                End If
                DefaultsText = DefaultsText & "tnalP"
            End If

            Using path As New SaveFileDialog With {
                .InitialDirectory = GetUserSetting(UserSetting.FileDownloadLocation),
                .DefaultExt = ".txt"
            }

                If path.ShowDialog() = DialogResult.OK Then
                    Dim DestFilePath As String = path.FileName.ToString

                    If IO.Path.GetDirectoryName(path.FileName) <> path.InitialDirectory Then
                        SaveUserSetting(UserSetting.FileDownloadLocation, IO.Path.GetDirectoryName(path.FileName))
                    End If

                    Dim Encoder As New Text.ASCIIEncoding

                    Dim bytedata As Byte() = Encoder.GetBytes(DefaultsText)

                    Using fs As New FileStream(DestFilePath, FileMode.Create, FileAccess.Write)
                        fs.Write(bytedata, 0, bytedata.Length)
                        fs.Close()
                    End Using
                End If
            End Using

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadDefaults()
        Dim DefaultsText As String = ""
        Dim AIRSNumber As String
        Dim FacilityName As String
        Dim FacilityStreet1 As String
        Dim FacilityStreet2 As String
        Dim FacilityCity As String
        Dim FacilityZipCode As String
        Dim Longitude As String
        Dim Latitude As String
        Dim County As String
        Dim District As String
        Dim OperationStatus As String
        Dim Classification As String
        Dim SICCode As String
        Dim NAICSCode As String
        Dim StartUpDate As String
        Dim ShutDownDate As String
        Dim CMSUniverse As String
        Dim PlantDesc As String

        Try
            Using path As New OpenFileDialog With {
                .InitialDirectory = GetUserSetting(UserSetting.FileDownloadLocation),
                .DefaultExt = ".txt"
            }

                If path.ShowDialog() = DialogResult.OK Then
                    Dim DestFilePath As String = path.FileName.ToString

                    If IO.Path.GetDirectoryName(path.FileName) <> path.InitialDirectory Then
                        SaveUserSetting(UserSetting.FileDownloadLocation, IO.Path.GetDirectoryName(path.FileName))
                    End If

                    If File.Exists(DestFilePath) Then
                        Dim reader As New StreamReader(DestFilePath)
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
                            If StartUpDate.IndexOf("*-") <> -1 AndAlso
                                    Mid(StartUpDate, StartUpDate.IndexOf("*-") + 3, (StartUpDate.IndexOf("-*") - (StartUpDate.IndexOf("*-") + 2))) = "Between" Then
                                rdbStartUpDateBetween.Checked = True
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
                            If ShutDownDate.IndexOf("*-") <> -1 AndAlso
                                Mid(ShutDownDate, ShutDownDate.IndexOf("*-") + 3, (ShutDownDate.IndexOf("-*") - (ShutDownDate.IndexOf("*-") + 2))) = "Between" Then
                                rdbShutDownDateBetween.Checked = True
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
            End Using

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRunSearch_Click(sender As Object, e As EventArgs) Handles btnRunSearch.Click
        Cursor = Cursors.WaitCursor
        GenerateSQL2()
        Cursor = Cursors.Default
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ResetForm()
    End Sub

    Private Sub tsbReSizeFilterOptions_Click(sender As Object, e As EventArgs) Handles tsbReSizeFilterOptions.Click
        ResizeFilter()
    End Sub

    Private Sub bgwQueryGenerator_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwQueryGenerator.DoWork
        LoadDataSets()
    End Sub

    Private Sub bgwQueryGenerator_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwQueryGenerator.RunWorkerCompleted
        ShowComboBoxes()
    End Sub

    Private Sub tsbExport_Click(sender As Object, e As EventArgs) Handles tsbExport.Click
        ExportToExcel()
    End Sub

    Private Sub tsbSearchQuery_Click(sender As Object, e As EventArgs) Handles tsbSearchQuery.Click, OpenSavedSearchToolStripMenuItem.Click
        LoadDefaults()
    End Sub

    Private Sub tsbSaveQuery_Click(sender As Object, e As EventArgs) Handles tsbSaveQuery.Click, SaveSearchQueryToolStripMenuItem.Click
        UpdateDefaultSearch()
    End Sub

    Private Sub RunCannedPermitContact()
        Try
            query = "select " &
            "distinct substring(STRAIRSNUMBER, 5, 3) + '-' + substring(STRAIRSNUMBER, 8, 5) as AIRSNumber, " &
            "strFacilityName, strFacilityStreet1, " &
            "strFacilityStreet2, strFacilityCity, " &
            "strFacilityState, strFacilityZipCode, " &
            "numFacilityLongitude, numFacilityLatitude, " &
            "strOperationalStatus, strClass, " &
            "strSICCode, strNAICSCode, " &
            "strPlantDescription, ContactType, " &
            "strContactFirstName, strContactLastName, " &
            "strContactAddress1, strContactCity, " &
            "strContactState, strContactZiPCode, " &
            "strContactPhoneNumber1, strContactEmail, " &
            "PermitNumber, IssuanceDate " &
            "from VW_Permit_Contact_Data " &
            "order by AIRSNumber "

            dgvQueryGenerator.DataSource = DB.GetDataTable(query)

            dgvQueryGenerator.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvQueryGenerator.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvQueryGenerator.Columns("strFacilityStreet1").HeaderText = "Facility Street"
            dgvQueryGenerator.Columns("strFacilityStreet2").HeaderText = "Facility Street 2"
            dgvQueryGenerator.Columns("strFacilityCity").HeaderText = "Facility City"
            dgvQueryGenerator.Columns("strFacilityState").HeaderText = "Facility State"
            dgvQueryGenerator.Columns("strFacilityZipCode").HeaderText = "Facility Zip Code"
            dgvQueryGenerator.Columns("numFacilityLongitude").HeaderText = "Longitude"
            dgvQueryGenerator.Columns("numFacilityLatitude").HeaderText = "Latitude"
            dgvQueryGenerator.Columns("strOperationalStatus").HeaderText = "Status"
            dgvQueryGenerator.Columns("strClass").HeaderText = "Classification"
            dgvQueryGenerator.Columns("strSICCode").HeaderText = "SIC"
            dgvQueryGenerator.Columns("strNAICSCode").HeaderText = "NAICS"
            dgvQueryGenerator.Columns("strPlantDescription").HeaderText = "Plant Desc."
            dgvQueryGenerator.Columns("ContactType").HeaderText = "Contact Type"
            dgvQueryGenerator.Columns("strContactFirstName").HeaderText = "First Name"
            dgvQueryGenerator.Columns("strContactLastName").HeaderText = "Last Name"
            dgvQueryGenerator.Columns("strContactAddress1").HeaderText = "City Address"
            dgvQueryGenerator.Columns("strContactCity").HeaderText = "Contact City"
            dgvQueryGenerator.Columns("strContactState").HeaderText = "Contact State"
            dgvQueryGenerator.Columns("strContactZiPCode").HeaderText = "Contact Zip Code"
            dgvQueryGenerator.Columns("strContactPhoneNumber1").HeaderText = "Phone #"
            dgvQueryGenerator.Columns("strContactEmail").HeaderText = "Email"
            dgvQueryGenerator.Columns("PermitNumber").HeaderText = "Permit #"
            dgvQueryGenerator.Columns("IssuanceDate").HeaderText = "Issued Date"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mmiExport_Click(sender As Object, e As EventArgs) Handles mmiExport.Click
        ExportToExcel()
    End Sub

    Private Sub RunCannedHistoryAirProgram()
        If dtpCannedEndDate.Value >= dtpCannedStartDate.Value Then
            dgvQueryGenerator.DataSource = DAL.SearchHistoricalAirProgramStatus(
                dtpCannedStartDate.Value,
                dtpCannedEndDate.Value,
                CType(cboCannedSelection.SelectedValue, AirPrograms))
        End If
    End Sub

    Private Sub RunCannedHistoryClass()
        If dtpCannedEndDate.Value >= dtpCannedStartDate.Value Then
            dgvQueryGenerator.DataSource = DAL.SearchHistoricalFacilityClassificationStatus(
                dtpCannedStartDate.Value,
                dtpCannedEndDate.Value,
                CType(cboCannedSelection.SelectedValue, FacilityClassification))
        End If
    End Sub

    Private Sub btnRunCannedReport_Click(sender As Object, e As EventArgs) Handles btnRunCannedReport.Click
        Cursor = Cursors.WaitCursor

        If rdbCannedHistoryClass.Checked Then
            RunCannedHistoryClass()
        ElseIf rdbCannedHistoryAirProgram.Checked Then
            RunCannedHistoryAirProgram()
        Else
            RunCannedPermitContact()
        End If

        Cursor = Cursors.Default
    End Sub

    Private Sub CannedSelection_CheckedChanged(sender As Object, e As EventArgs) _
        Handles rdbCannedHistoryClass.CheckedChanged, rdbCannedHistoryAirProgram.CheckedChanged, rdbCannedPermitContactData.CheckedChanged

        If rdbCannedHistoryClass.Checked Then
            HideControls({lblCannedHistoryAirProgram, lblCannedPermitContactData})
            ShowControls({lblCannedHistoryClass, lblCannedStartDate, dtpCannedStartDate, lblCannedEndDate, dtpCannedEndDate, lblCannedSelection, cboCannedSelection})
            cboCannedSelection.BindToEnum(Of FacilityClassification)()

            If cboCannedSelection.Items.Count > 1 Then
                cboCannedSelection.SelectedIndex = 1
            End If
        ElseIf rdbCannedHistoryAirProgram.Checked Then
            HideControls({lblCannedHistoryClass, lblCannedPermitContactData})
            ShowControls({lblCannedHistoryAirProgram, lblCannedStartDate, dtpCannedStartDate, lblCannedEndDate, dtpCannedEndDate, lblCannedSelection, cboCannedSelection})
            cboCannedSelection.BindToEnum(Of AirPrograms)()

            If cboCannedSelection.Items.Count > 1 Then
                cboCannedSelection.SelectedIndex = 1
            End If
        Else
            HideControls({lblCannedHistoryClass, lblCannedHistoryAirProgram, lblCannedStartDate, dtpCannedStartDate, lblCannedEndDate, dtpCannedEndDate, lblCannedSelection, cboCannedSelection})
            ShowControls({lblCannedPermitContactData})
        End If

    End Sub

    Private Sub tcQueryOptions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tcQueryOptions.SelectedIndexChanged
        If tcQueryOptions.SelectedTab Is TPCannedReports Then
            BasicSearchGroup.Enabled = False
            AcceptButton = btnRunCannedReport
        Else
            BasicSearchGroup.Enabled = True
            AcceptButton = btnRunSearch
        End If
    End Sub

    ' DataGridView events

    Private Sub dgvQueryGenerator_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvQueryGenerator.CellLinkActivated
        OpenFormFacilitySummary(e.LinkValue.ToString)
    End Sub

    'Form overrides dispose to clean up the component list. 
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If dtcboCountySearch1 IsNot Nothing Then dtcboCountySearch1.Dispose()
                If dtcboCountySearch2 IsNot Nothing Then dtcboCountySearch2.Dispose()
                If dtcboDistrictSearch1 IsNot Nothing Then dtcboDistrictSearch1.Dispose()
                If dtcboDistrictSearch2 IsNot Nothing Then dtcboDistrictSearch2.Dispose()
                If dtcboSIPSearch1 IsNot Nothing Then dtcboSIPSearch1.Dispose()
                If dtcboSIPSearch2 IsNot Nothing Then dtcboSIPSearch2.Dispose()
                If dtcboPart61Search1 IsNot Nothing Then dtcboPart61Search1.Dispose()
                If dtcboPart61Search2 IsNot Nothing Then dtcboPart61Search2.Dispose()
                If dtcboPart60Search1 IsNot Nothing Then dtcboPart60Search1.Dispose()
                If dtcboPart60Search2 IsNot Nothing Then dtcboPart60Search2.Dispose()
                If dtcboPart63Search1 IsNot Nothing Then dtcboPart63Search1.Dispose()
                If dtcboPart63Search2 IsNot Nothing Then dtcboPart63Search2.Dispose()
                If dtcboSSCPEngineerSearch1 IsNot Nothing Then dtcboSSCPEngineerSearch1.Dispose()
                If dtcboSSCPEngineerSearch2 IsNot Nothing Then dtcboSSCPEngineerSearch2.Dispose()
                If dtcboSSCPUnitSearch1 IsNot Nothing Then dtcboSSCPUnitSearch1.Dispose()
                If dtcboSSCPUnitSearch2 IsNot Nothing Then dtcboSSCPUnitSearch2.Dispose()
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

End Class