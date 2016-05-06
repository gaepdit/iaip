Imports Oracle.ManagedDataAccess.Client
Imports Iaip.Apb.Facilities

Public Class IAIPFacilityCreator
    Dim ds As New DataSet
    Dim da As OracleDataAdapter
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader

    Private Sub IAIPFacilityCreator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            LoadCounty()
            TCFacilityTools.TabPages.Remove(TPApproveNewFacility)
            TCFacilityTools.TabPages.Remove(TPDeleteFacility)

            If AccountFormAccess(138, 0) Is Nothing Then
            Else
                If AccountFormAccess(138, 0) = "138" Then
                    If AccountFormAccess(138, 3) = "1" Or AccountFormAccess(138, 4) = "1" Then
                        TCFacilityTools.TabPages.Add(TPApproveNewFacility)
                        dtpStartFilter.Text = OracleDate
                        dtpEndFilter.Text = OracleDate
                        DTPSSCPApproveDate.Text = OracleDate
                        DTPSSPPApproveDate.Text = OracleDate

                        TCFacilityTools.TabPages.Add(TPDeleteFacility)

                        LoadPendingFacilities()
                    End If
                End If
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadCounty()
        Try
            Dim dtCounty As New DataTable
            Dim drNewRow As DataRow
            Dim drDSRow As DataRow

            SQL = "Select " &
            "strCountyCode, strCountyName " &
            "from AIRBRANCH.lookUpCountyInformation " &
            "order by strCountyName "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "CountyData")

            dtCounty.Columns.Add("strCountyCode", GetType(System.String))
            dtCounty.Columns.Add("strCountyName", GetType(System.String))

            drNewRow = dtCounty.NewRow()
            drNewRow("strCountyCode") = " "
            drNewRow("strCountyName") = ""
            dtCounty.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("CountyData").Rows()
                drNewRow = dtCounty.NewRow
                drNewRow("strCountyCode") = drDSRow("strCountyCode")
                drNewRow("strCountyName") = drDSRow("strCountyName")
                dtCounty.Rows.Add(drNewRow)
            Next

            With cboCounty
                .DataSource = dtCounty
                .DisplayMember = "strCountyName"
                .ValueMember = "strCountyCode"
                .SelectedIndex = 0
            End With

            cboCDSOperationalStatus.Items.Add("O - Operating")
            cboCDSOperationalStatus.Items.Add("P - Planned")
            cboCDSOperationalStatus.Items.Add("C - Under Construction")
            cboCDSOperationalStatus.Items.Add("T - Temporarily Closed")
            cboCDSOperationalStatus.Items.Add("X - Permanently Closed")
            cboCDSOperationalStatus.Items.Add("I - Seasonal Operation")

            cboCDSClassCode.Items.Add("A - MAJOR")
            cboCDSClassCode.Items.Add("B - MINOR")
            cboCDSClassCode.Items.Add("C - UNKNOWN")
            cboCDSClassCode.Items.Add("SM - SYNTHETIC MINOR")
            cboCDSClassCode.Items.Add("PR - PERMIT BY RULE")
            'cboCDSClassCode.Items.Add("U - UNDEFINED")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub LoadPendingFacilities()
        Try
            If chbIncludeApproved.Checked = True Then
                SQL = "select " &
         "FUllData.AIRSNumber, strFacilityName, " &
         "DateCreated, strComments, " &
         "SSCPApprover, datApproveDateSSCP, strCommentSSCP, " &
         "SSPPApprover, datApproveDateSSPP, strCommentSSPP, " &
         "strfacilityStreet1 " &
         "from " &
         "(select substr(AIRBRANCH.APBMasterAIRS.strAIRSNumber, 5) as AIRSNumber, " &
         "strFacilityName, AIRBRANCH.AFSFacilityData.datModifingDate as dateCreated, " &
         "AIRBRANCH.APBHeaderData.strComments,  " &
         "datApproveDateSSCP, strCommentSSCP, " &
         "datApproveDateSSPP, strCommentSSPP, " &
         "strfacilityStreet1 " &
         "from AIRBRANCH.AFSFacilityData, AIRBRANCH.APBFacilityInformation,  " &
         "AIRBRANCH.APBMasterAIRS, AIRBRANCH.APBHeaderData, AIRBRANCH.APBSupplamentalData " &
         "where AIRBRANCH.AFSFacilityData.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber  " &
         "and AIRBRANCH.AFSFacilityData.strAIRSNumber = AIRBRANCH.APBMasterAIRS.strAIRSnumber  " &
         "and AIRBRANCH.AFSFacilityData.strAIRSnumber = AIRBRANCH.APBHeaderData.strAIRSNumber " &
         "and AIRBRANCH.AFSFacilityData.strAIRSNumber = AIRBRANCH.APBSupplamentalData.strAIRSNumber " &
         " ) FullData,   " &
         "(select substr(AIRBRANCH.AFSFacilityData.strAIRSNumber, 5) as AIRSNumber, " &
         "case " &
         "when numApprovingSSCP is not null then (strLastName||', '||strFirstName) " &
         "else '' " &
         "end SSCPApprover " &
         "from AIRBRANCH.AFSFacilityData, AIRBRANCH.APBSupplamentalData, " &
         "AIRBRANCH.EPDUserProfiles " &
         "where  AIRBRANCH.AFSFacilityData.strAIRSNumber = AIRBRANCH.APBSupplamentalData.strAIRSNumber " &
         "and AIRBRANCH.APBSupplamentalData.numApprovingSSCP = AIRBRANCH.EPDUserProfiles.numUserID (+) " &
         " )SSCPStaff, " &
         "(select substr(AIRBRANCH.AFSFacilityData.strAIRSNumber, 5) as AIRSNumber, " &
         "case " &
         "when numApprovingSSPP is not null then (strLastName||', '||strFirstName) " &
         "else '' " &
         "end SSPPApprover " &
         "from AIRBRANCH.AFSFacilityData, AIRBRANCH.APBSupplamentalData, " &
         "AIRBRANCH.EPDUserProfiles " &
         "where  AIRBRANCH.AFSFacilityData.strAIRSNumber = AIRBRANCH.APBSupplamentalData.strAIRSNumber " &
         "and AIRBRANCH.APBSupplamentalData.numApprovingSSPP = AIRBRANCH.EPDUserProfiles.numUserID (+) " &
         " )SSPPStaff " &
         "where FullData.AIRSNumber = SSCPStaff.AIRSNumber (+) " &
         "and FullData.AIRSNumber = SSPPStaff.AIRSNumber (+) "

            Else
                SQL = "select " &
         "FUllData.AIRSNumber, strFacilityName, " &
         "DateCreated, strComments, " &
         "SSCPApprover, datApproveDateSSCP, strCommentSSCP, " &
         "SSPPApprover, datApproveDateSSPP, strCommentSSPP, " &
         "strfacilityStreet1 " &
         "from " &
         "(select substr(AIRBRANCH.APBMasterAIRS.strAIRSNumber, 5) as AIRSNumber, " &
         "strFacilityName, AIRBRANCH.AFSFacilityData.datModifingDate as dateCreated, " &
         "AIRBRANCH.APBHeaderData.strComments,  " &
         "datApproveDateSSCP, strCommentSSCP, " &
         "datApproveDateSSPP, strCommentSSPP, " &
         "strfacilityStreet1 " &
         "from AIRBRANCH.AFSFacilityData, AIRBRANCH.APBFacilityInformation,  " &
         "AIRBRANCH.APBMasterAIRS, AIRBRANCH.APBHeaderData, AIRBRANCH.APBSupplamentalData " &
         "where AIRBRANCH.AFSFacilityData.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber  " &
         "and AIRBRANCH.AFSFacilityData.strAIRSNumber = AIRBRANCH.APBMasterAIRS.strAIRSnumber  " &
         "and AIRBRANCH.AFSFacilityData.strAIRSnumber = AIRBRANCH.APBHeaderData.strAIRSNumber " &
         "and AIRBRANCH.AFSFacilityData.strAIRSNumber = AIRBRANCH.APBSupplamentalData.strAIRSNumber " &
         "and strUpdateStatus = 'H') FullData,   " &
         "(select substr(AIRBRANCH.AFSFacilityData.strAIRSNumber, 5) as AIRSNumber, " &
         "case " &
         "when numApprovingSSCP is not null then (strLastName||', '||strFirstName) " &
         "else '' " &
         "end SSCPApprover " &
         "from AIRBRANCH.AFSFacilityData, AIRBRANCH.APBSupplamentalData, " &
         "AIRBRANCH.EPDUserProfiles " &
         "where  AIRBRANCH.AFSFacilityData.strAIRSNumber = AIRBRANCH.APBSupplamentalData.strAIRSNumber " &
         "and AIRBRANCH.APBSupplamentalData.numApprovingSSCP = AIRBRANCH.EPDUserProfiles.numUserID (+) " &
         "and strUpdateStatus = 'H')SSCPStaff, " &
         "(select substr(AIRBRANCH.AFSFacilityData.strAIRSNumber, 5) as AIRSNumber, " &
         "case " &
         "when numApprovingSSPP is not null then (strLastName||', '||strFirstName) " &
         "else '' " &
         "end SSPPApprover " &
         "from AIRBRANCH.AFSFacilityData, AIRBRANCH.APBSupplamentalData, " &
         "AIRBRANCH.EPDUserProfiles " &
         "where  AIRBRANCH.AFSFacilityData.strAIRSNumber = AIRBRANCH.APBSupplamentalData.strAIRSNumber " &
         "and AIRBRANCH.APBSupplamentalData.numApprovingSSPP = AIRBRANCH.EPDUserProfiles.numUserID (+) " &
         "and strUpdateStatus = 'H')SSPPStaff " &
         "where FullData.AIRSNumber = SSCPStaff.AIRSNumber (+) " &
         "and FullData.AIRSNumber = SSPPStaff.AIRSNumber (+) "
            End If

            If chbFilterNewFacilities.Checked = True Then
                SQL = SQL & "and dateCreated between '" & dtpStartFilter.Text & "' and '" & dtpEndFilter.Text & "' "
            End If

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "PendingAIRS")
            dgvVerifyNewFacilities.DataSource = ds
            dgvVerifyNewFacilities.DataMember = "PendingAIRS"

            dgvVerifyNewFacilities.RowHeadersVisible = False
            dgvVerifyNewFacilities.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvVerifyNewFacilities.AllowUserToResizeColumns = True
            dgvVerifyNewFacilities.AllowUserToAddRows = False
            dgvVerifyNewFacilities.AllowUserToDeleteRows = False
            dgvVerifyNewFacilities.AllowUserToOrderColumns = True
            dgvVerifyNewFacilities.AllowUserToResizeRows = True

            dgvVerifyNewFacilities.Columns("AIRSNumber").HeaderText = "AIRS Number"
            dgvVerifyNewFacilities.Columns("AIRSNumber").DisplayIndex = 0
            dgvVerifyNewFacilities.Columns("AIRSNumber").Width = dgvVerifyNewFacilities.Width * 0.1
            dgvVerifyNewFacilities.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvVerifyNewFacilities.Columns("strFacilityName").DisplayIndex = 1
            dgvVerifyNewFacilities.Columns("strFacilityName").Width = dgvVerifyNewFacilities.Width * 0.3
            dgvVerifyNewFacilities.Columns("dateCreated").HeaderText = "Date Created"
            dgvVerifyNewFacilities.Columns("dateCreated").DisplayIndex = 2
            dgvVerifyNewFacilities.Columns("dateCreated").Width = dgvVerifyNewFacilities.Width * 0.15
            dgvVerifyNewFacilities.Columns("dateCreated").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvVerifyNewFacilities.Columns("strComments").HeaderText = "Comments"
            dgvVerifyNewFacilities.Columns("strComments").DisplayIndex = 3
            dgvVerifyNewFacilities.Columns("strComments").Width = dgvVerifyNewFacilities.Width * 0.4

            dgvVerifyNewFacilities.Columns("SSCPApprover").HeaderText = "SSCP Approver"
            dgvVerifyNewFacilities.Columns("SSCPApprover").DisplayIndex = 4
            dgvVerifyNewFacilities.Columns("SSCPApprover").Width = dgvVerifyNewFacilities.Width * 0.2
            dgvVerifyNewFacilities.Columns("datApproveDateSSCP").HeaderText = "Date SSCP Approved"
            dgvVerifyNewFacilities.Columns("datApproveDateSSCP").DisplayIndex = 5
            dgvVerifyNewFacilities.Columns("datApproveDateSSCP").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvVerifyNewFacilities.Columns("datApproveDateSSCP").Width = dgvVerifyNewFacilities.Width * 0.2
            dgvVerifyNewFacilities.Columns("strCommentSSCP").HeaderText = "SSCP Comments"
            dgvVerifyNewFacilities.Columns("strCommentSSCP").DisplayIndex = 6
            dgvVerifyNewFacilities.Columns("strCommentSSCP").Width = dgvVerifyNewFacilities.Width * 0.2
            dgvVerifyNewFacilities.Columns("SSPPApprover").HeaderText = "SSPP Approver"
            dgvVerifyNewFacilities.Columns("SSPPApprover").DisplayIndex = 7
            dgvVerifyNewFacilities.Columns("SSPPApprover").Width = dgvVerifyNewFacilities.Width * 0.2
            dgvVerifyNewFacilities.Columns("datApproveDateSSPP").HeaderText = "Date SSPP Approved"
            dgvVerifyNewFacilities.Columns("datApproveDateSSPP").DisplayIndex = 8
            dgvVerifyNewFacilities.Columns("datApproveDateSSPP").Width = dgvVerifyNewFacilities.Width * 0.2
            dgvVerifyNewFacilities.Columns("datApproveDateSSPP").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvVerifyNewFacilities.Columns("strCommentSSPP").HeaderText = "SSPP Comments"
            dgvVerifyNewFacilities.Columns("strCommentSSPP").DisplayIndex = 9
            dgvVerifyNewFacilities.Columns("strCommentSSPP").Width = dgvVerifyNewFacilities.Width * 0.2
            dgvVerifyNewFacilities.Columns("strFacilityStreet1").HeaderText = "Street Address"
            dgvVerifyNewFacilities.Columns("strFacilityStreet1").DisplayIndex = 10
            dgvVerifyNewFacilities.Columns("strFacilityStreet1").Width = dgvVerifyNewFacilities.Width * 0.2
            dgvVerifyNewFacilities.Columns("strFacilityStreet1").Visible = False

            txtCountFacilities.Text = dgvVerifyNewFacilities.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub FindRegion(ByVal Region As String, ByVal AIRSNumber As String)
        Try

            If Len(AIRSNumber) = 8 And IsNumeric(AIRSNumber) Then
                SQL = "Select (AIRBRANCH.LookUPDistricts.strDistrictcode|| '-'||strDistrictName) as District " &
                "from AIRBRANCH.LookUPDistricts, AIRBRANCH.LookUPDistrictInformation " &
                "where AIRBRANCH.LookUPDistricts.strDistrictCode = AIRBRANCH.LookUPDistrictInformation.strDistrictCode " &
                "and strDistrictCounty = '" & Mid(AIRSNumber, 1, 3) & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read

                If recExist Then
                    Region = dr.Item("District")
                Else
                    Region = "WARNING"
                End If
            Else
                Region = "WARNING"
            End If

            txtCDSRegionCode.Text = Region

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub btnSaveNewFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveNewFacility.Click
        Try
            Dim AIRSNumber As String = ""
            Dim FacilityName As String = ""
            Dim FacilityStreet As String = ""
            Dim FacilityCity As String = ""
            Dim FacilityZipCode As String = ""
            Dim FacilityLongitude As String = ""
            Dim FacilityLatitude As String = ""
            Dim MailingStreet As String = ""
            Dim MailingCity As String = ""
            Dim MailingState As String = ""
            Dim MailingZipCode As String = ""
            Dim OperatingStatus As String = ""
            Dim Classification As String = ""
            Dim AirProgramCode As String = ""
            Dim SICCode As String = ""
            Dim NAICSCode As String = ""
            Dim DistrictOffice As String = ""
            Dim PlantDesc As String = ""
            Dim ContactFirstName As String = ""
            Dim ContactLastName As String = ""
            Dim ContactPrefix As String = ""
            Dim ContactSuffix As String = ""
            Dim ContactTitle As String = ""
            Dim ContactPhoneNumber As String = ""
            Dim Comments As String = ""
            Dim RMPNumber As String = ""

            If IsDBNull(cboCounty.SelectedValue) Or cboCounty.SelectedValue Is Nothing Then
                MsgBox("Invalid County Selected." & vbCrLf & "No Data Saved", MsgBoxStyle.Information, Me.Name)
                Exit Sub
            End If
            If txtCDSAIRSNumber.Text <> "" Then
                MsgBox("There is an existing AIRS # associated with this data." &
                    "Either use the Edit button or clear the AIRS # before Saving a new Facility.", MsgBoxStyle.Information, Me.Name)
                Exit Sub
            End If
            temp = mtbFacilityLatitude.Text
            temp = mtbFacilityLongitude.Text
            If mtbFacilityLatitude.Text = "  ." Then
                MsgBox("The Latitude field needs to be addressed." &
                 "No Data saved.", MsgBoxStyle.Information, Me.Name)
                Exit Sub
            End If
            If mtbFacilityLongitude.Text = "-  ." Then
                MsgBox("The Longitude field needs to be addressed." &
                "No Data saved.", MsgBoxStyle.Information, Me.Name)
                Exit Sub
            End If
            If Not DAL.FacilityHeaderDataData.NaicsCodeIsValid(mtbCDSNAICSCode.Text) Then
                MsgBox("The NAICS Code is not valid and must be fixed before proceeding." &
                  "No Data saved.", MsgBoxStyle.Information, Me.Name)
                Exit Sub
            End If
            If Not DAL.FacilityHeaderDataData.SicCodeIsValid(mtbCDSSICCode.Text) Then
                MsgBox("The SIC Code is not valid and must be fixed before proceeding." &
                "No Data saved.", MsgBoxStyle.Information, Me.Name)
                Exit Sub
            End If

            SQL = "Insert into AIRBRANCH.APBMasterAIRS " &
            "values " &
            "((select '0413'||substr((max(strAIRSNumber) + 1), 4) " &
            "from AIRBRANCH.APBMasterAIRS " &
            "where substr(strAIRSNumber, 1, 7) = '0413" & cboCounty.SelectedValue & "'), " &
            "'" & CurrentUser.UserID & "', '" & OracleDate & "' ) "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "select (substr((max(strAIRSNumber)), 5)) as AIRSNumber " &
            "from AIRBRANCH.APBMasterAIRS " &
            "where substr(strAIRSNumber, 1, 7) = '0413" & cboCounty.SelectedValue & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("AIRSNumber")) Then
                    txtCDSAIRSNumber.Text = ""
                Else
                    txtCDSAIRSNumber.Text = dr.Item("AIRSNumber")
                End If
            End While
            dr.Close()
            FindRegion(txtCDSRegionCode.Text, txtCDSAIRSNumber.Text)

            If txtCDSAIRSNumber.Text = "" Then
                MsgBox("There was an error in creating Facility." & "Contact EPD IT before proceding", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            Else
                AIRSNumber = "0413" & txtCDSAIRSNumber.Text
            End If
            If txtCDSFacilityName.Text = "" Then
                FacilityName = "N/A"
            Else
                txtCDSFacilityName.Text = Apb.Facilities.Facility.SanitizeFacilityNameForDb(txtCDSFacilityName.Text)
                FacilityName = txtCDSFacilityName.Text
            End If
            If txtCDSStreetAddress.Text <> "" Then
                FacilityStreet = txtCDSStreetAddress.Text
            Else
                FacilityStreet = "N/A"
            End If
            If txtCDSCity.Text <> "" Then
                FacilityCity = txtCDSCity.Text
            Else
                FacilityCity = "N/A"
            End If
            If mtbCDSZipCode.Text <> "" Then
                FacilityZipCode = mtbCDSZipCode.Text
            Else
                FacilityZipCode = "00000"
            End If
            If mtbFacilityLongitude.Text <> "" And mtbFacilityLongitude.Text.Contains("-  .") = False Then
                FacilityLongitude = mtbFacilityLongitude.Text
            Else
                FacilityLongitude = "00.000000"
            End If
            If mtbFacilityLatitude.Text <> "" And mtbFacilityLatitude.Text.Contains("  .") = False Then
                FacilityLatitude = mtbFacilityLatitude.Text
            Else
                FacilityLatitude = "-00.000000"
            End If
            If cboCDSOperationalStatus.Items.Contains(cboCDSOperationalStatus.Text) Then
                OperatingStatus = Mid(cboCDSOperationalStatus.Text, 1, 1)
            Else
                OperatingStatus = "O"
            End If
            If cboCDSClassCode.Items.Contains(cboCDSClassCode.Text) Then
                Classification = Mid(cboCDSClassCode.Text, 1, (InStr(1, cboCDSClassCode.Text, "-", CompareMethod.Text)) - 2)
            Else
                Classification = "C"
            End If

            AirProgramCode = "000000000000000"

            If chbCDS_1.Checked = True Then
                AirProgramCode = "1" & Mid(AirProgramCode, 2)
            End If
            If chbCDS_2.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 1) & "1" & Mid(AirProgramCode, 3)
            End If
            If chbCDS_3.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 2) & "1" & Mid(AirProgramCode, 4)
            End If
            If chbCDS_4.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 3) & "1" & Mid(AirProgramCode, 5)
            End If
            If chbCDS_5.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 4) & "1" & Mid(AirProgramCode, 6)
            End If
            If chbCDS_6.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 5) & "1" & Mid(AirProgramCode, 7)
            End If
            If chbCDS_7.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 6) & "1" & Mid(AirProgramCode, 8)
            End If
            If chbCDS_8.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 7) & "1" & Mid(AirProgramCode, 9)
            End If
            If chbCDS_9.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 8) & "1" & Mid(AirProgramCode, 10)
            End If
            If chbCDS_10.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 9) & "1" & Mid(AirProgramCode, 11)
            End If
            If chbCDS_11.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 10) & "1" & Mid(AirProgramCode, 12)
            End If
            If chbCDS_12.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 11) & "1" & Mid(AirProgramCode, 13)
            End If
            If chbCDS_13.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 12) & "1" & Mid(AirProgramCode, 14)
            End If
            If chbCDS_14.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 13) & "1" & Mid(AirProgramCode, 15)
            End If

            If AirProgramCode.Length <> 15 Then
                AirProgramCode = "100000000000000"
            Else
                AirProgramCode = AirProgramCode
            End If
            SICCode = mtbCDSSICCode.Text
            NAICSCode = mtbCDSNAICSCode.Text
            If txtCDSRegionCode.Text <> "" Then
                DistrictOffice = Mid(txtCDSRegionCode.Text, 1, 1)
            Else
                DistrictOffice = "A"
            End If
            If txtFacilityDescription.Text <> "" Then
                PlantDesc = txtFacilityDescription.Text
            Else
                PlantDesc = "N/A"
            End If
            If txtContactSocialTitle.Text <> "" Then
                ContactPrefix = Replace(txtContactSocialTitle.Text, "'", "''")
            Else
                ContactPrefix = ""
            End If
            If txtContactPedigree.Text <> "" Then
                ContactSuffix = Replace(txtContactPedigree.Text, "'", "''")
            Else
                ContactSuffix = ""
            End If
            If txtContactFirstName.Text <> "" Then
                ContactFirstName = Replace(txtContactFirstName.Text, "'", "''")
            Else
                ContactFirstName = "N/A"
            End If
            If txtContactLastName.Text <> "" Then
                ContactLastName = Replace(txtContactLastName.Text, "'", "''")
            Else
                ContactLastName = "N/A"
            End If
            If txtContactTitle.Text <> "" Then
                ContactTitle = txtContactTitle.Text
            Else
                ContactTitle = "N/A"
            End If
            If mtbContactPhoneNumber.Text <> "" Then
                ContactPhoneNumber = Replace(Replace(Replace(Replace(mtbContactPhoneNumber.Text, "(", ""), ")", ""), " ", ""), "-", "")
            Else
                ContactPhoneNumber = "0000000000"
            End If
            If mtbContactNumberExtension.Text <> "" Then
                ContactPhoneNumber = ContactPhoneNumber & mtbContactNumberExtension.Text
            End If
            If txtMailingAddress.Text <> "" Then
                MailingStreet = txtMailingAddress.Text
            Else
                MailingStreet = "N/A"
            End If
            If txtMailingCity.Text <> "" Then
                MailingCity = txtMailingCity.Text
            Else
                MailingCity = "N/A"
            End If
            If txtMailingState.Text <> "" Then
                MailingState = txtMailingState.Text
            Else
                MailingState = "GA"
            End If
            If mtbMailingZipCode.Text <> "" Then
                MailingZipCode = mtbMailingZipCode.Text
            Else
                MailingZipCode = "00000"
            End If
            If txtFacilityComments.Text = "" Then
                Comments = "Created with Facility Creator tool by " & CurrentUser.AlphaName & " on " & OracleDate & vbCrLf
            End If

            If txtFacilityComments.Text.Contains("Created by Facility Creator by " & CurrentUser.AlphaName & " on " & OracleDate) Then
            Else
                Comments = "Created with Facility Creator tool by " & CurrentUser.AlphaName & " on " & OracleDate &
                               vbCrLf & txtFacilityComments.Text & vbCrLf
            End If
            If txtApplicationNumber.Text <> "App No." And
                        txtFacilityComments.Text.Contains(txtApplicationNumber.Text) = False Then
                Comments = Comments & "Pre-loaded with Application " & txtApplicationNumber.Text
            End If
            If mtbRiskManagementNumber.Text <> "" Then
                RMPNumber = mtbRiskManagementNumber.Text
            Else
                RMPNumber = ""
            End If

            SQL = "Insert into AIRBRANCH.APBFacilityInformation " &
            "(strAIRSNumber, strFacilityName, " &
            "strFacilityStreet1, strFacilityStreet2, " &
            "strFacilityCity, strFacilityState, " &
            "strFacilityZipCode, strModifingPerson, " &
            "datModifingDate, numFacilityLongitude, " &
            "numFacilityLatitude, strHorizontalCollectionCode, " &
            "strHorizontalAccuracyMeasure, strHorizontalReferenceCode, " &
            "strModifingLocation ) " &
            "values " &
            "('" & AIRSNumber & "', '" & Replace(FacilityName, "'", "''") & "', " &
            "'" & Replace(FacilityStreet, "'", "''") & "', 'N/A', " &
            "'" & Replace(FacilityCity, "'", "''") & "', 'GA', " &
            "'" & Replace(FacilityZipCode, "-", "") & "', '" & CurrentUser.UserID & "', " &
            "'" & OracleDate & "', " & FacilityLongitude & ", " &
            "" & FacilityLatitude & ", '007', " &
            "'25', '002', '4' ) "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            Dim AttainmentStatus As String = ""

            SQL = "select " &
            "strNonAttainment " &
            "from AIRBRANCH.LookUpCountyInformation " &
            "where strCountyCode = '" & Mid(AIRSNumber, 5, 3) & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strNonAttainment")) Then
                    AttainmentStatus = "00000"
                Else
                    AttainmentStatus = dr.Item("strNonAttainment")
                End If
            End While
            dr.Close()

            SQL = "Insert into AIRBRANCH.APBHeaderData " &
            "(strAIRSNumber, strOperationalStatus, " &
            "strClass, " &
            "strAIRProgramCodes, strSICCode, " &
            "strFEINumber, strModifingPerson, " &
            "datModifingDate, datStartUpDate, " &
            "datShutDownDate, strComments, " &
            "strPlantDescription, strAttainmentStatus, " &
            "strNAICSCode, strModifingLocation) " &
            "values " &
            "('" & AIRSNumber & "', '" & OperatingStatus & "', " &
            "'" & Classification & "', " &
            "'" & AirProgramCode & "', '" & SICCode & "', " &
            "'N/A', '" & CurrentUser.UserID & "', " &
            "'" & OracleDate & "', '', '', " &
            "'" & Replace(Comments, "'", "''") & "', " &
            "'" & Replace(PlantDesc, "'", "''") & "', '" & AttainmentStatus & "', " &
            "'" & Replace(NAICSCode, "'", "''") & "', '4' ) "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Insert into AIRBRANCH.APBSupplamentalData " &
            "(strAIRSNumber, datSSCPTestReportDue, " &
            "strModifingPerson, DatModifingDate, " &
            "strDistrictOffice, strCMSMember, " &
            "strAFSActionNumber, STRRMPID) " &
            "values " &
             "('" & AIRSNumber & "', '', " &
             "'" & CurrentUser.UserID & "', '" & OracleDate & "', " &
             "'" & DistrictOffice & "', '', " &
             "'00001', '" & Replace(RMPNumber, "'", "''") & "' ) "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "insert into AIRBRANCH.APBContactInformation " &
            "(strContactKey, strAIRSNumber, strKey, " &
            "strContactFirstName, strContactLastName, " &
            "strContactPrefix, strContactSuffix, " &
            "strContactTitle, strContactCompanyName, " &
            "strContactPhoneNumber1, strContactPhoneNumber2, " &
            "strContactFaxNumber, strContactEmail, " &
            "strContactAddress1, strContactAddress2, " &
            "strContactCity, strContactState, " &
            "strContactZipCode, strModifingPerson, " &
            "datModifingDate) " &
            "values " &
            "('" & AIRSNumber & "30', '" & AIRSNumber & "', '30', " &
            "'" & Replace(ContactFirstName, "'", "''") & "', '" & Replace(ContactLastName, "'", "''") & "', " &
            "'" & ContactPrefix & "', '" & ContactSuffix & "', " &
            "'" & Replace(ContactTitle, "'", "''") & "', 'N/A', " &
            "'" & ContactPhoneNumber & "', 'N/A', " &
            "'N/A', 'N/A', " &
            "'" & Replace(MailingStreet, "'", "''") & "', 'N/A', " &
            "'" & Replace(MailingCity, "'", "''") & "', '" & MailingState & "', " &
            "'" & Replace(MailingZipCode, "-", "") & "', '" & CurrentUser.UserID & "', " &
            "'" & OracleDate & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            dr.Close()

            Dim os As FacilityOperationalStatus = [Enum].Parse(GetType(FacilityOperationalStatus), OperatingStatus)

            If chbCDS_1.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(AIRSNumber, AirProgram.SIP, "OT", os)
            End If
            If chbCDS_2.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(AIRSNumber, AirProgram.FederalSIP, "OT", os)
            End If
            If chbCDS_3.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(AIRSNumber, AirProgram.NonFederalSIP, "OT", os)
            End If
            If chbCDS_4.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(AIRSNumber, AirProgram.CfcTracking, "OT", os)
            End If
            If chbCDS_5.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(AIRSNumber, AirProgram.PSD, "OT", os)
            End If
            If chbCDS_6.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(AIRSNumber, AirProgram.NSR, "OT", os)
            End If
            If chbCDS_7.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(AIRSNumber, AirProgram.NESHAP, "OT", os)
            End If
            If chbCDS_8.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(AIRSNumber, AirProgram.NSPS, "OT", os)
            End If
            If chbCDS_9.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(AIRSNumber, AirProgram.FESOP, "OT", os)
            End If
            If chbCDS_10.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(AIRSNumber, AirProgram.AcidPrecipitation, "OT", os)
            End If
            If chbCDS_11.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(AIRSNumber, AirProgram.NativeAmerican, "OT", os)
            End If
            If chbCDS_12.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(AIRSNumber, AirProgram.MACT, "OT", os)
            End If
            If chbCDS_13.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(AIRSNumber, AirProgram.TitleV, "OT", os)
            End If

            SQL = "Insert into AIRBRANCH.SSCPDistrictResponsible " &
            "values " &
            "('" & AIRSNumber & "', 'False', " &
            "'1', sysdate) "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            MsgBox("Facility Added to Integrated Air Information Platform", MsgBoxStyle.Information, Me.Text)

            If TCFacilityTools.TabPages.Contains(TPApproveNewFacility) Then
                LoadPendingFacilities()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnPreLoadNewFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreLoadNewFacility.Click
        Try
            If txtApplicationNumber.Text <> "App No." Then
                SQL = "select " &
                "strFacilityName, strFacilityStreet1, " &
                "strFacilityCity, strFacilityZipCode, " &
                "strOperationalStatus, strClass, " &
                "strAirProgramCodes, strSICCode, " &
                "strNAICSCode, " &
                "strPlantDescription, strContactFirstName, " &
                "strContactLastName, strContactpreFix, " &
                "strContactSuffix, strContactTitle, " &
                "strContactPhoneNumber1 " &
                "from AIRBRANCH.SSPPApplicationdata, " &
                "AIRBRANCH.SSPPApplicationContact " &
                "where AIRBRANCH.SSPPApplicationData.strApplicationNumber = AIRBRANCH.SSPPApplicationContact.strApplicationNumber " &
                "and AIRBRANCH.SSPPApplicationData.strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        txtCDSFacilityName.Clear()
                    Else
                        txtCDSFacilityName.Text = dr.Item("strFacilityname")
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        txtCDSStreetAddress.Clear()
                        txtMailingAddress.Clear()
                    Else
                        txtCDSStreetAddress.Text = dr.Item("strFacilityStreet1")
                        txtMailingAddress.Text = dr.Item("strFacilityStreet1")
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        txtCDSCity.Clear()
                        txtMailingCity.Clear()
                    Else
                        txtCDSCity.Text = dr.Item("strFacilityCity")
                        txtMailingCity.Text = dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        mtbCDSZipCode.Clear()
                        mtbMailingZipCode.Clear()
                    Else
                        mtbCDSZipCode.Text = dr.Item("strFacilityZipCode")
                        mtbMailingZipCode.Text = dr.Item("strFacilityZipCode")
                    End If
                    If IsDBNull(dr.Item("strOperationalStatus")) Then
                        cboCDSOperationalStatus.Text = ""
                    Else
                        temp = dr.Item("strOperationalStatus")
                        Select Case temp.ToString
                            Case "O"
                                cboCDSOperationalStatus.Text = "O - Operating"
                            Case "P"
                                cboCDSOperationalStatus.Text = "P - Planned"
                            Case "C"
                                cboCDSOperationalStatus.Text = "C - Under Construction"
                            Case "T"
                                cboCDSOperationalStatus.Text = "T - Temporarily Closed"
                            Case "X"
                                cboCDSOperationalStatus.Text = "X - Permanently Closed"
                            Case "I"
                                cboCDSOperationalStatus.Text = "I - Seasonal Operation"
                            Case Else
                                cboCDSOperationalStatus.Text = " "
                        End Select
                        '  cboCDSOperationalStatus.SelectedText = dr.Item("stroperationalStatus")
                    End If
                    'If IsDBNull(dr.Item("strClass")) Then
                    '    cboCDSClassCode.Text = ""
                    'Else
                    '    cboCDSClassCode.Text = dr.Item("strClass")
                    'End If
                    If IsDBNull(dr.Item("strClass")) Then
                        cboCDSClassCode.Text = ""
                    Else
                        temp = dr.Item("strClass")
                        Select Case temp.ToString
                            Case "A"
                                cboCDSClassCode.Text = "A - MAJOR"
                            Case "B"
                                cboCDSClassCode.Text = "B - MINOR"
                            Case "C"
                                cboCDSClassCode.Text = "C - UNKNOWN"
                            Case "SM"
                                cboCDSClassCode.Text = "SM - SYNTHETIC MINOR"
                            Case "PR"
                                cboCDSClassCode.Text = "PR - PERMIT BY RULE"
                            Case Else
                                cboCDSClassCode.Text = "C - UNKNOWN"
                        End Select
                        'cboCDSClassCode.Text = dr.Item("strClass")
                    End If
                    If IsDBNull(dr.Item("strAirProgramCodes")) Then
                        chbCDS_1.Checked = False
                        chbCDS_2.Checked = False
                        chbCDS_3.Checked = False
                        chbCDS_4.Checked = False
                        chbCDS_5.Checked = False
                        chbCDS_6.Checked = False
                        chbCDS_7.Checked = False
                        chbCDS_8.Checked = False
                        chbCDS_9.Checked = False
                        chbCDS_10.Checked = False
                        chbCDS_11.Checked = False
                        chbCDS_12.Checked = False
                        chbCDS_13.Checked = False
                        chbCDS_14.Checked = False

                    Else
                        temp = dr.Item("strAirProgramCodes")
                        If Mid(temp, 1, 1) = "0" Then
                            chbCDS_1.Checked = False
                        Else
                            chbCDS_1.Checked = True
                        End If
                        If Mid(temp, 2, 1) = "0" Then
                            chbCDS_2.Checked = False
                        Else
                            chbCDS_2.Checked = True
                        End If
                        If Mid(temp, 3, 1) = "0" Then
                            chbCDS_3.Checked = False
                        Else
                            chbCDS_3.Checked = True
                        End If
                        If Mid(temp, 4, 1) = "0" Then
                            chbCDS_4.Checked = False
                        Else
                            chbCDS_4.Checked = True
                        End If
                        If Mid(temp, 5, 1) = "0" Then
                            chbCDS_5.Checked = False
                        Else
                            chbCDS_5.Checked = True
                        End If
                        If Mid(temp, 6, 1) = "0" Then
                            chbCDS_6.Checked = False
                        Else
                            chbCDS_6.Checked = True
                        End If
                        If Mid(temp, 7, 1) = "0" Then
                            chbCDS_7.Checked = False
                        Else
                            chbCDS_7.Checked = True
                        End If
                        If Mid(temp, 8, 1) = "0" Then
                            chbCDS_8.Checked = False
                        Else
                            chbCDS_8.Checked = True
                        End If
                        If Mid(temp, 9, 1) = "0" Then
                            chbCDS_9.Checked = False
                        Else
                            chbCDS_9.Checked = True
                        End If
                        If Mid(temp, 10, 1) = "0" Then
                            chbCDS_10.Checked = False
                        Else
                            chbCDS_10.Checked = True
                        End If
                        If Mid(temp, 11, 1) = "0" Then
                            chbCDS_11.Checked = False
                        Else
                            chbCDS_11.Checked = True
                        End If
                        If Mid(temp, 12, 1) = "0" Then
                            chbCDS_12.Checked = False
                        Else
                            chbCDS_12.Checked = True
                        End If
                        If Mid(temp, 13, 1) = "0" Then
                            chbCDS_13.Checked = False
                        Else
                            chbCDS_13.Checked = True
                        End If
                        If Mid(temp, 14, 1) = "0" Then
                            chbCDS_14.Checked = False
                        Else
                            chbCDS_14.Checked = True
                        End If
                    End If
                    If IsDBNull(dr.Item("strSICCode")) Then
                        mtbCDSSICCode.Clear()
                    Else
                        mtbCDSSICCode.Text = dr.Item("strSICCode")
                    End If
                    If IsDBNull(dr.Item("strNAICSCode")) Then
                        mtbCDSNAICSCode.Clear()
                    Else
                        mtbCDSNAICSCode.Text = dr.Item("strNAICScode")
                    End If
                    If IsDBNull(dr.Item("strPlantDescription")) Then
                        txtFacilityDescription.Clear()
                    Else
                        txtFacilityDescription.Text = dr.Item("strPlantDescription")
                    End If
                    If IsDBNull(dr.Item("strContactFirstName")) Then
                        txtContactFirstName.Clear()
                    Else
                        txtContactFirstName.Text = dr.Item("strContactFirstName")
                    End If
                    If IsDBNull(dr.Item("strContactLastName")) Then
                        txtContactLastName.Clear()
                    Else
                        txtContactLastName.Text = dr.Item("strContactLastName")
                    End If
                    If IsDBNull(dr.Item("strContactPreFix")) Then
                        txtContactSocialTitle.Clear()
                    Else
                        txtContactSocialTitle.Text = dr.Item("strContactPrefix")
                    End If
                    If IsDBNull(dr.Item("strContactSuffix")) Then
                        txtContactPedigree.Clear()
                    Else
                        txtContactPedigree.Text = dr.Item("strContactSuffix")
                    End If
                    If IsDBNull(dr.Item("strContactTitle")) Then
                        txtContactTitle.Clear()
                    Else
                        txtContactTitle.Text = dr.Item("strContactTitle")
                    End If
                    If IsDBNull(dr.Item("strCOntactphoneNumber1")) Then
                        mtbContactPhoneNumber.Clear()
                        mtbContactNumberExtension.Clear()
                    Else
                        mtbContactPhoneNumber.Text = Mid(dr.Item("strContactPhoneNumber1"), 1, 10)
                        mtbContactNumberExtension.Text = Mid(dr.Item("strcontactPhoneNumber1"), 11)
                    End If
                End If
                dr.Close()
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbOpenWebpage_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbOpenWebpage.LinkClicked
        Try
            Dim MappingAddress As String = txtCDSStreetAddress.Text & ", " & txtCDSCity.Text & ", GA," & mtbCDSZipCode.Text
            Clipboard.SetDataObject(MappingAddress, True)

            OpenUri(New Uri("http://mapper.acme.com/"), Me)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvVerifyNewFacilities_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvVerifyNewFacilities.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvVerifyNewFacilities.HitTest(e.X, e.Y)
        Try
            txtNewFacilityName.Clear()
            txtNewAIRSNumber.Clear()
            txtSSCPApprover.Clear()
            DTPSSCPApproveDate.Text = OracleDate
            txtSSCPComments.Clear()
            txtSSPPApprover.Clear()
            DTPSSPPApproveDate.Text = OracleDate
            txtSSPPComments.Clear()
            chbSSCPSignOff.Checked = False
            chbSSPPSignOff.Checked = False

            If dgvVerifyNewFacilities.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvVerifyNewFacilities.Columns(0).HeaderText = "AIRS Number" Then
                    If IsDBNull(dgvVerifyNewFacilities(0, hti.RowIndex).Value) Then
                        txtNewAIRSNumber.Clear()
                    Else
                        txtNewAIRSNumber.Text = dgvVerifyNewFacilities(0, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvVerifyNewFacilities(1, hti.RowIndex).Value) Then
                        txtNewFacilityName.Clear()
                    Else
                        txtNewFacilityName.Text = dgvVerifyNewFacilities(1, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvVerifyNewFacilities(3, hti.RowIndex).Value) Then
                        txtApprovialComments.Clear()
                    Else
                        txtApprovialComments.Text = dgvVerifyNewFacilities(3, hti.RowIndex).Value
                    End If

                    If IsDBNull(dgvVerifyNewFacilities(4, hti.RowIndex).Value) Then
                        txtSSCPApprover.Clear()
                    Else
                        chbSSCPSignOff.Checked = True
                        txtSSCPApprover.Text = dgvVerifyNewFacilities(4, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvVerifyNewFacilities(5, hti.RowIndex).Value) Then
                        DTPSSCPApproveDate.Text = OracleDate
                    Else
                        DTPSSCPApproveDate.Text = dgvVerifyNewFacilities(5, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvVerifyNewFacilities(6, hti.RowIndex).Value) Then
                        txtSSCPComments.Clear()
                    Else
                        txtSSCPComments.Text = dgvVerifyNewFacilities(6, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvVerifyNewFacilities(7, hti.RowIndex).Value) Then
                        txtSSPPApprover.Clear()
                    Else
                        chbSSPPSignOff.Checked = True
                        txtSSPPApprover.Text = dgvVerifyNewFacilities(7, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvVerifyNewFacilities(8, hti.RowIndex).Value) Then
                        DTPSSPPApproveDate.Text = OracleDate
                    Else
                        DTPSSPPApproveDate.Text = dgvVerifyNewFacilities(8, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvVerifyNewFacilities(9, hti.RowIndex).Value) Then
                        txtSSPPComments.Clear()
                    Else
                        txtSSPPComments.Text = dgvVerifyNewFacilities(9, hti.RowIndex).Value
                    End If
                    'txtStreetAddress
                    If IsDBNull(dgvVerifyNewFacilities(10, hti.RowIndex).Value) Then
                        txtStreetAddress.Clear()
                    Else
                        txtStreetAddress.Text = dgvVerifyNewFacilities(10, hti.RowIndex).Value
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnViewFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewFacility.Click
        Try
            If txtNewAIRSNumber.Text = "" Then
                Exit Sub
            End If

            txtCDSAIRSNumber.Text = txtNewAIRSNumber.Text
            FindRegion(txtCDSRegionCode.Text, txtCDSAIRSNumber.Text)

            SQL = "select " &
            "strFacilityName, strFacilityStreet1, " &
            "strFacilityCity, strFacilityState, " &
            "strFacilityZipCode, " &
            "numFacilityLongitude, numFacilityLatitude, " &
            "strContactFirstName, strContactLastname, " &
            "strContactPrefix, strContactSuffix, " &
            "strContactTitle, strContactPhoneNumber1, " &
            "strContactAddress1, strContactCity, " &
            "strContactState, strContactZipCode, " &
            "strSICCode, strOperationalStatus, " &
            "strClass, strAirProgramCodes, " &
            "strPlantDescription, " &
            "AIRBRANCH.APBHeaderData.strComments, " &
            "strNAICSCode, strRMPID " &
            "from AIRBRANCH.APBFacilityInformation, AIRBRANCH.APBHeaderData, " &
            "AIRBRANCH.APBContactInformation, AIRBRANCH.APBSupplamentalData  " &
            "where AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.APBHeaderData.strAIRSNumber " &
            "and AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.APBSupplamentalData.strAIRSNumber " &
            "and AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.APBContactInformation.strAIRSNumber " &
            "and strkey = '30' " &
            "and AIRBRANCH.APBFacilityInformation.strAIRSNumber = '0413" & txtNewAIRSNumber.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("strComments")) Then
                    txtFacilityComments.Clear()
                Else
                    txtFacilityComments.Text = dr.Item("strComments")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtCDSFacilityName.Clear()
                Else
                    txtCDSFacilityName.Text = dr.Item("strFacilityname")
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    txtCDSStreetAddress.Clear()
                    txtMailingAddress.Clear()
                Else
                    txtCDSStreetAddress.Text = dr.Item("strFacilityStreet1")
                    txtMailingAddress.Text = dr.Item("strFacilityStreet1")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    txtCDSCity.Clear()
                    txtMailingCity.Clear()
                Else
                    txtCDSCity.Text = dr.Item("strFacilityCity")
                    txtMailingCity.Text = dr.Item("strFacilityCity")
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    mtbCDSZipCode.Clear()
                    mtbMailingZipCode.Clear()
                Else
                    mtbCDSZipCode.Text = dr.Item("strFacilityZipCode")
                    mtbMailingZipCode.Text = dr.Item("strFacilityZipCode")
                End If
                If IsDBNull(dr.Item("strOperationalStatus")) Then
                    cboCDSOperationalStatus.Text = ""
                Else
                    temp = dr.Item("strOperationalStatus")
                    Select Case temp.ToString
                        Case "O"
                            cboCDSOperationalStatus.Text = "O - Operating"
                        Case "P"
                            cboCDSOperationalStatus.Text = "P - Planned"
                        Case "C"
                            cboCDSOperationalStatus.Text = "C - Under Construction"
                        Case "T"
                            cboCDSOperationalStatus.Text = "T - Temporarily Closed"
                        Case "X"
                            cboCDSOperationalStatus.Text = "X - Permanently Closed"
                        Case "I"
                            cboCDSOperationalStatus.Text = "I - Seasonal Operation"
                        Case Else
                            cboCDSOperationalStatus.Text = " "
                    End Select
                    '  cboCDSOperationalStatus.SelectedText = dr.Item("stroperationalStatus")
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    cboCDSClassCode.Text = ""
                Else
                    temp = dr.Item("strClass")
                    Select Case temp.ToString
                        Case "A"
                            cboCDSClassCode.Text = "A - MAJOR"
                        Case "B"
                            cboCDSClassCode.Text = "B - MINOR"
                        Case "C"
                            cboCDSClassCode.Text = "C - UNKNOWN"
                        Case "SM"
                            cboCDSClassCode.Text = "SM - SYNTHETIC MINOR"
                        Case "PR"
                            cboCDSClassCode.Text = "PR - PERMIT BY RULE"
                        Case Else
                            cboCDSClassCode.Text = "C - UNKNOWN"
                    End Select
                    'cboCDSClassCode.Text = dr.Item("strClass")
                End If
                If IsDBNull(dr.Item("strAirProgramCodes")) Then
                    chbCDS_1.Checked = False
                    chbCDS_2.Checked = False
                    chbCDS_3.Checked = False
                    chbCDS_4.Checked = False
                    chbCDS_5.Checked = False
                    chbCDS_6.Checked = False
                    chbCDS_7.Checked = False
                    chbCDS_8.Checked = False
                    chbCDS_9.Checked = False
                    chbCDS_10.Checked = False
                    chbCDS_11.Checked = False
                    chbCDS_12.Checked = False
                    chbCDS_13.Checked = False
                    chbCDS_14.Checked = False

                Else
                    temp = dr.Item("strAirProgramCodes")
                    If Mid(temp, 1, 1) = "0" Then
                        chbCDS_1.Checked = False
                    Else
                        chbCDS_1.Checked = True
                    End If
                    If Mid(temp, 2, 1) = "0" Then
                        chbCDS_2.Checked = False
                    Else
                        chbCDS_2.Checked = True
                    End If
                    If Mid(temp, 3, 1) = "0" Then
                        chbCDS_3.Checked = False
                    Else
                        chbCDS_3.Checked = True
                    End If
                    If Mid(temp, 4, 1) = "0" Then
                        chbCDS_4.Checked = False
                    Else
                        chbCDS_4.Checked = True
                    End If
                    If Mid(temp, 5, 1) = "0" Then
                        chbCDS_5.Checked = False
                    Else
                        chbCDS_5.Checked = True
                    End If
                    If Mid(temp, 6, 1) = "0" Then
                        chbCDS_6.Checked = False
                    Else
                        chbCDS_6.Checked = True
                    End If
                    If Mid(temp, 7, 1) = "0" Then
                        chbCDS_7.Checked = False
                    Else
                        chbCDS_7.Checked = True
                    End If
                    If Mid(temp, 8, 1) = "0" Then
                        chbCDS_8.Checked = False
                    Else
                        chbCDS_8.Checked = True
                    End If
                    If Mid(temp, 9, 1) = "0" Then
                        chbCDS_9.Checked = False
                    Else
                        chbCDS_9.Checked = True
                    End If
                    If Mid(temp, 10, 1) = "0" Then
                        chbCDS_10.Checked = False
                    Else
                        chbCDS_10.Checked = True
                    End If
                    If Mid(temp, 11, 1) = "0" Then
                        chbCDS_11.Checked = False
                    Else
                        chbCDS_11.Checked = True
                    End If
                    If Mid(temp, 12, 1) = "0" Then
                        chbCDS_12.Checked = False
                    Else
                        chbCDS_12.Checked = True
                    End If
                    If Mid(temp, 13, 1) = "0" Then
                        chbCDS_13.Checked = False
                    Else
                        chbCDS_13.Checked = True
                    End If
                    If Mid(temp, 14, 1) = "0" Then
                        chbCDS_14.Checked = False
                    Else
                        chbCDS_14.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strSICCode")) Then
                    mtbCDSSICCode.Clear()
                Else
                    mtbCDSSICCode.Text = dr.Item("strSICCode")
                End If
                If IsDBNull(dr.Item("strPlantDescription")) Then
                    txtFacilityDescription.Clear()
                Else
                    txtFacilityDescription.Text = dr.Item("strPlantDescription")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    txtContactFirstName.Clear()
                Else
                    txtContactFirstName.Text = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtContactLastName.Clear()
                Else
                    txtContactLastName.Text = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactPreFix")) Then
                    txtContactSocialTitle.Clear()
                Else
                    txtContactSocialTitle.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactSuffix")) Then
                    txtContactPedigree.Clear()
                Else
                    txtContactPedigree.Text = dr.Item("strContactSuffix")
                End If
                If IsDBNull(dr.Item("strContactTitle")) Then
                    txtContactTitle.Clear()
                Else
                    txtContactTitle.Text = dr.Item("strContactTitle")
                End If
                If IsDBNull(dr.Item("strCOntactphoneNumber1")) Then
                    mtbContactPhoneNumber.Clear()
                    mtbContactNumberExtension.Clear()
                Else
                    mtbContactPhoneNumber.Text = Mid(dr.Item("strContactPhoneNumber1"), 1, 10)
                    mtbContactNumberExtension.Text = Mid(dr.Item("strcontactPhoneNumber1"), 11)
                End If
                If IsDBNull(dr.Item("numFacilityLongitude")) Then
                    mtbFacilityLongitude.Clear()
                Else
                    mtbFacilityLongitude.Text = dr.Item("numFacilityLongitude")
                End If
                If IsDBNull(dr.Item("numFacilityLatitude")) Then
                    mtbFacilityLatitude.Clear()
                Else
                    mtbFacilityLatitude.Text = dr.Item("numFacilityLatitude")
                End If
                If IsDBNull(dr.Item("strNAICSCode")) Then
                    mtbCDSNAICSCode.Clear()
                Else
                    mtbCDSNAICSCode.Text = dr.Item("strNAICSCode")
                End If
                If IsDBNull(dr.Item("strRMPID")) Then
                    mtbRiskManagementNumber.Clear()
                Else
                    mtbRiskManagementNumber.Text = dr.Item("strRMPID")
                End If
            End If
            dr.Close()

            cboCounty.SelectedValue = Mid(txtCDSAIRSNumber.Text, 1, 3)

            If TCFacilityTools.TabPages.Contains(TPCreateNewFacility) Then
                '                TPCreateNewFacility.Focus = True
                TCFacilityTools.SelectedIndex = 1
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSubmitFacilityToAFS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmitFacilityToAFS.Click
        Try
            Dim SSCPSignOff As String = ""
            Dim SSPPSignOff As String = ""

            If chbSSCPSignOff.Checked = True And chbSSPPSignOff.Checked = True Then
                SQL = "Select " &
                "numApprovingSSCP, numApprovingSSPP " &
                "from AIRBRANCH.APBSupplamentalData " &
                "where strAIRSNumber = '0413" & txtNewAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("numApprovingSSCP")) Then
                        SSCPSignOff = ""
                    Else
                        SSCPSignOff = dr.Item("numApprovingSSCP")
                    End If
                    If IsDBNull(dr.Item("numApprovingSSPP")) Then
                        SSPPSignOff = ""
                    Else
                        SSPPSignOff = dr.Item("numApprovingSSPP")
                    End If
                End While
                dr.Close()

                If SSCPSignOff = "" And SSPPSignOff = "" Then
                    SQL = "Update AIRBRANCH.APBSupplamentalData set " &
                    "numApprovingSSCP = '" & CurrentUser.UserID & "', " &
                    "datApproveDateSSCP = '" & DTPSSCPApproveDate.Text & "', " &
                    "strCommentSSCP = '" & txtSSCPComments.Text & "', " &
                     "numApprovingSSPP = '" & CurrentUser.UserID & "', " &
                    "datApproveDateSSPP = '" & DTPSSCPApproveDate.Text & "', " &
                    "strCommentSSPP = '" & txtSSCPComments.Text & "' " &
                    "where strAIRSnumber = '0413" & txtNewAIRSNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    If SSCPSignOff = "" Then
                        SQL = "Update AIRBRANCH.APBSupplamentalData set " &
                         "numApprovingSSCP = '" & CurrentUser.UserID & "', " &
                         "datApproveDateSSCP = '" & DTPSSCPApproveDate.Text & "', " &
                         "strCommentSSCP = '" & txtSSCPComments.Text & "' " &
                         "where strAIRSnumber = '0413" & txtNewAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If

                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If
                    If SSPPSignOff = "" Then
                        SQL = "Update AIRBRANCH.APBSupplamentalData set " &
                        "numApprovingSSPP = '" & CurrentUser.UserID & "', " &
                        "datApproveDateSSPP = '" & DTPSSCPApproveDate.Text & "', " &
                        "strCommentSSPP = '" & txtSSCPComments.Text & "' " &
                        "where strAIRSnumber = '0413" & txtNewAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If

                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If
                End If

                SQL = "Update AIRBRANCH.AFSFacilityData set " &
                "strUpdateStatus = 'A' " &
                "where strAIRSNumber = '0413" & txtNewAIRSNumber.Text & "' " &
                "and strUpdateStatus = 'H' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                MsgBox(txtNewFacilityName.Text & " (" & txtNewAIRSNumber.Text &
                       ") has been approved", MsgBoxStyle.Information, Me.Text)
                LoadPendingFacilities()
                ClearValidator()
                ClearNewFacility()
            Else
                MsgBox("Both SSCP and SSPP have to sign off on the new facility before it can be sent to EPA.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRemoveFromPlatform_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFromPlatform.Click
        Try
            If Not Apb.ApbFacilityId.IsValidAirsNumberFormat(txtNewAIRSNumber.Text) Then
                MessageBox.Show("AIRS number is not valid", "Invalid AIRS number", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If DAL.FacilityData.FacilityHasBeenApproved(txtNewAIRSNumber.Text) Then
                MessageBox.Show("Facility has already been approved.", "Can't delete", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim result As DialogResult
            result = MessageBox.Show("Are you sure you want to completely remove this facility from the database? The data will not be recoverable.", "Confirm facility deletion",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
            If result = DialogResult.No Then
                Exit Sub
            End If

            If DAL.FacilityData.DeleteFacility(txtNewAIRSNumber.Text) Then
                MessageBox.Show("Facility removed from the database", "Gone", MessageBoxButtons.OK)
            Else
                MessageBox.Show("There was an error when attempting to remove the facility from the database." & vbNewLine & vbNewLine & "Facility has not been removed.", "Error", MessageBoxButtons.OK)
            End If

            LoadPendingFacilities()
            ClearValidator()
            ClearNewFacility()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveSSCPApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSSCPApproval.Click
        Try
            If chbSSCPSignOff.Checked = False Then
                MsgBox("Please check the SSCP Approve box.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            SQL = "Update AIRBRANCH.APBSupplamentalData set " &
            "numApprovingSSCP = '" & CurrentUser.UserID & "', " &
            "datApproveDateSSCP = '" & DTPSSCPApproveDate.Text & "', " &
            "strCommentSSCP = '" & txtSSCPComments.Text & "' " &
            "where strAIRSnumber = '0413" & txtNewAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            dr.Close()
            LoadPendingFacilities()
            txtSSCPApprover.Text = CurrentUser.AlphaName
            MsgBox("Approval Saved.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveSSPPApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSSPPApproval.Click
        Try
            If chbSSPPSignOff.Checked = False Then
                MsgBox("Please check the SSPP Approve box.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            SQL = "Update AIRBRANCH.APBSupplamentalData set " &
            "numApprovingSSpp = '" & CurrentUser.UserID & "', " &
            "datApproveDateSSpP = '" & DTPSSPPApproveDate.Text & "', " &
            "strCommentSSpP = '" & txtSSPPComments.Text & "' " &
            "where strAIRSnumber = '0413" & txtNewAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            dr.Close()
            LoadPendingFacilities()
            txtSSPPApprover.Text = CurrentUser.AlphaName
            MsgBox("Approval Saved.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnValidateFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidateFacility.Click
        Try
            Dim FacilityName As String
            Dim FacilityAddress As String

            If txtNewAIRSNumber.Text <> "" Then
                FacilityName = txtNewFacilityName.Text
            Else
                Exit Sub
            End If
            If txtStreetAddress.Text <> "" Then
                FacilityAddress = txtStreetAddress.Text
            Else
                FacilityAddress = ""
            End If

            SQL = "Select " &
            "strFacilityName, " &
            "substr(strAIRSNumber, 5) as AIRSNumber, " &
            "strFacilityStreet1, strFacilityCity, " &
            "strFacilityZipCode " &
            "from AIRBRANCH.APBFacilityInformation " &
            "where Upper(strFacilityName) Like Upper('%" & Replace(FacilityName.ToUpper, "'", "''") & "%')" &
            "or upper(strFacilityStreet1) like '%" & FacilityAddress.ToUpper & "%' " &
            "Union " &
            "Select " &
            "distinct(strFacilityName) as strFacilityName, " &
            "substr(strAIRSNumber, 5) as shortAIRS, " &
            "strFacilityStreet1, strFacilityCity,  strFacilityZipCode " &
            "from AIRBRANCH.HB_APBFacilityInformation " &
            "where Upper(strFacilityName) Like Upper('%" & Replace(FacilityName.ToUpper, "'", "''") & "%')" &
            "or upper(strFacilityStreet1) like '%" & FacilityAddress.ToUpper & "%' " &
            "Union " &
            "select " &
            "Distinct(strFacilityname) as strFacilityname,  " &
            "substr(strAIRSNumber, 5) as shortAIRS,  " &
            "strFacilityStreet1, strFacilityCity, strFacilityZipCode  " &
            "from AIRBRANCH.SSPPApplicationData, AIRBRANCH.SSPPApplicationMaster   " &
            "where AIRBRANCH.SSPPApplicationData.strApplicationNumber = AIRBRANCH.SSPPApplicationMaster.strApplicationNumber " &
            "and (upper(strFacilityname) like Upper('%" & Replace(FacilityName.ToUpper, "'", "''") & "%') " &
            "or upper(strFacilityStreet1) like '%" & FacilityAddress.ToUpper & "%') "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "Validate")
            dgvValidatingAIRS.DataSource = ds
            dgvValidatingAIRS.DataMember = "Validate"

            dgvValidatingAIRS.RowHeadersVisible = False
            dgvValidatingAIRS.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvValidatingAIRS.AllowUserToResizeColumns = True
            dgvValidatingAIRS.AllowUserToAddRows = False
            dgvValidatingAIRS.AllowUserToDeleteRows = False
            dgvValidatingAIRS.AllowUserToOrderColumns = True
            dgvValidatingAIRS.AllowUserToResizeRows = True

            dgvValidatingAIRS.Columns("AIRSNumber").HeaderText = "AIRS Number"
            dgvValidatingAIRS.Columns("AIRSNumber").DisplayIndex = 0
            dgvValidatingAIRS.Columns("AIRSNumber").Width = dgvValidatingAIRS.Width * 0.1
            dgvValidatingAIRS.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvValidatingAIRS.Columns("strFacilityName").DisplayIndex = 1
            dgvValidatingAIRS.Columns("strFacilityName").Width = dgvValidatingAIRS.Width * 0.3
            dgvValidatingAIRS.Columns("strFacilityStreet1").HeaderText = "Street Address"
            dgvValidatingAIRS.Columns("strFacilityStreet1").DisplayIndex = 2
            dgvValidatingAIRS.Columns("strFacilityStreet1").Width = dgvValidatingAIRS.Width * 0.3
            dgvValidatingAIRS.Columns("strFacilityCity").HeaderText = "City"
            dgvValidatingAIRS.Columns("strFacilityCity").DisplayIndex = 3
            dgvValidatingAIRS.Columns("strFacilityCity").Width = dgvValidatingAIRS.Width * 0.2
            dgvValidatingAIRS.Columns("strFacilityZipCode").HeaderText = "Zip Code"
            dgvValidatingAIRS.Columns("strFacilityZipCode").DisplayIndex = 4
            dgvValidatingAIRS.Columns("strFacilityZipCode").Width = dgvValidatingAIRS.Width * 0.15

            txtValidationCount.Text = dgvValidatingAIRS.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tspClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tspClear.Click
        Try
            ClearValidator()
            ClearNewFacility()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearNewFacility()
        Try
            cboCounty.SelectedValue = 0
            txtApplicationNumber.Text = "App No."
            txtCDSFacilityName.Clear()
            txtCDSStreetAddress.Clear()
            txtCDSCity.Clear()
            mtbCDSZipCode.Clear()
            mtbFacilityLatitude.Clear()
            mtbFacilityLongitude.Clear()
            txtMailingAddress.Clear()
            txtMailingCity.Clear()
            txtMailingState.Text = "GA"
            mtbMailingZipCode.Clear()
            mtbCDSSICCode.Clear()
            cboCDSOperationalStatus.Text = ""
            cboCDSClassCode.Text = ""
            txtFacilityDescription.Clear()
            txtCDSRegionCode.Clear()
            chbCDS_1.Checked = False
            chbCDS_2.Checked = False
            chbCDS_3.Checked = False
            chbCDS_4.Checked = False
            chbCDS_5.Checked = False
            chbCDS_6.Checked = False
            chbCDS_7.Checked = False
            chbCDS_8.Checked = False
            chbCDS_9.Checked = False
            chbCDS_10.Checked = False
            chbCDS_11.Checked = False
            chbCDS_12.Checked = False
            chbCDS_13.Checked = False
            chbCDS_14.Checked = False
            chbCDS_15.Checked = False
            txtContactSocialTitle.Clear()
            txtContactFirstName.Clear()
            txtContactLastName.Clear()
            txtContactPedigree.Clear()
            txtContactTitle.Clear()
            mtbContactPhoneNumber.Clear()
            mtbContactNumberExtension.Clear()
            txtFacilityComments.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearValidator()
        Try

            txtNewAIRSNumber.Clear()
            txtNewFacilityName.Clear()
            txtStreetAddress.Clear()
            txtApprovialComments.Clear()
            chbSSCPSignOff.Checked = False
            txtSSCPApprover.Clear()
            DTPSSCPApproveDate.Text = OracleDate
            txtSSCPComments.Clear()
            chbSSPPSignOff.Checked = False
            txtSSPPApprover.Clear()
            DTPSSPPApproveDate.Text = OracleDate
            txtSSPPComments.Clear()
            ds = New DataSet
            dgvValidatingAIRS.DataSource = ds
            txtValidationCount.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearAIRSNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAIRSNumber.Click
        Try
            txtCDSAIRSNumber.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditFacilityData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditFacilityData.Click
        Try
            Dim AIRSNumber As String = ""
            Dim FacilityName As String = ""
            Dim FacilityStreet As String = ""
            Dim FacilityCity As String = ""
            Dim FacilityZipCode As String = ""
            Dim FacilityLongitude As String = ""
            Dim FacilityLatitude As String = ""
            Dim MailingStreet As String = ""
            Dim MailingCity As String = ""
            Dim MailingState As String = ""
            Dim MailingZipCode As String = ""
            Dim OperatingStatus As String = ""
            Dim Classification As String = ""
            Dim AirProgramCode As String = ""
            Dim SICCode As String = ""
            Dim NAICSCode As String = ""
            Dim DistrictOffice As String = ""
            Dim PlantDesc As String = ""
            Dim ContactFirstName As String = ""
            Dim ContactLastName As String = ""
            Dim ContactPrefix As String = ""
            Dim ContactSuffix As String = ""
            Dim ContactTitle As String = ""
            Dim ContactPhoneNumber As String = ""
            Dim Comments As String = ""

            If txtCDSAIRSNumber.Text = "" Then
                MsgBox("There is no AIRS # selected. You will need to select a facility from the Approvial Tab first.",
                       MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
            If DAL.FacilityHeaderDataData.NaicsCodeIsValid(mtbCDSNAICSCode.Text) = False Then
                MsgBox("The NACIS Code is not valid." &
                  "No Data saved.", MsgBoxStyle.Information, Me.Name)
                Exit Sub
            End If

            AIRSNumber = "0413" & txtCDSAIRSNumber.Text

            If txtCDSFacilityName.Text = "" Then
                FacilityName = "N/A"
            Else
                txtCDSFacilityName.Text = Apb.Facilities.Facility.SanitizeFacilityNameForDb(txtCDSFacilityName.Text)
                FacilityName = txtCDSFacilityName.Text
            End If

            If txtCDSStreetAddress.Text <> "" Then
                FacilityStreet = txtCDSStreetAddress.Text
            Else
                FacilityStreet = "N/A"
            End If
            If txtCDSCity.Text <> "" Then
                FacilityCity = txtCDSCity.Text
            Else
                FacilityCity = "N/A"
            End If
            If mtbCDSZipCode.Text <> "" Then
                FacilityZipCode = mtbCDSZipCode.Text
            Else
                FacilityZipCode = "00000"
            End If
            If mtbFacilityLongitude.Text <> "" And mtbFacilityLongitude.Text.Contains("-  .") = False Then
                FacilityLongitude = mtbFacilityLongitude.Text
            Else
                FacilityLongitude = "00.000000"
            End If
            If mtbFacilityLatitude.Text <> "" And mtbFacilityLatitude.Text.Contains("  .") = False Then
                FacilityLatitude = mtbFacilityLatitude.Text
            Else
                FacilityLatitude = "-00.000000"
            End If
            If cboCDSOperationalStatus.Items.Contains(cboCDSOperationalStatus.Text) Then
                OperatingStatus = Mid(cboCDSOperationalStatus.Text, 1, 1)
            Else
                OperatingStatus = "O"
            End If
            If cboCDSClassCode.Items.Contains(cboCDSClassCode.Text) Then
                Classification = Mid(cboCDSClassCode.Text, 1, (InStr(1, cboCDSClassCode.Text, "-", CompareMethod.Text)) - 2)
            Else
                Classification = "C"
            End If

            AirProgramCode = "000000000000000"

            If chbCDS_1.Checked = True Then
                AirProgramCode = "1" & Mid(AirProgramCode, 2)
            End If
            If chbCDS_2.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 1) & "1" & Mid(AirProgramCode, 3)
            End If
            If chbCDS_3.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 2) & "1" & Mid(AirProgramCode, 4)
            End If
            If chbCDS_4.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 3) & "1" & Mid(AirProgramCode, 5)
            End If
            If chbCDS_5.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 4) & "1" & Mid(AirProgramCode, 6)
            End If
            If chbCDS_6.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 5) & "1" & Mid(AirProgramCode, 7)
            End If
            If chbCDS_7.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 6) & "1" & Mid(AirProgramCode, 8)
            End If
            If chbCDS_8.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 7) & "1" & Mid(AirProgramCode, 9)
            End If
            If chbCDS_9.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 8) & "1" & Mid(AirProgramCode, 10)
            End If
            If chbCDS_10.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 9) & "1" & Mid(AirProgramCode, 11)
            End If
            If chbCDS_11.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 10) & "1" & Mid(AirProgramCode, 12)
            End If
            If chbCDS_12.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 11) & "1" & Mid(AirProgramCode, 13)
            End If
            If chbCDS_13.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 12) & "1" & Mid(AirProgramCode, 14)
            End If
            If chbCDS_14.Checked = True Then
                AirProgramCode = Mid(AirProgramCode, 1, 13) & "1" & Mid(AirProgramCode, 15)
            End If

            If AirProgramCode.Length <> 15 Then
                AirProgramCode = "100000000000000"
            Else
                AirProgramCode = AirProgramCode
            End If
            If mtbCDSSICCode.Text <> "" Then
                SICCode = mtbCDSSICCode.Text
            Else
                SICCode = "0000"
            End If
            If mtbCDSNAICSCode.Text <> "" Then
                NAICSCode = mtbCDSNAICSCode.Text
            Else
                NAICSCode = "000000"
            End If
            If txtCDSRegionCode.Text <> "" Then
                DistrictOffice = Mid(txtCDSRegionCode.Text, 1, 1)
            Else
                DistrictOffice = "A"
            End If
            If txtFacilityDescription.Text <> "" Then
                PlantDesc = txtFacilityDescription.Text
            Else
                PlantDesc = "N/A"
            End If
            If txtContactSocialTitle.Text <> "" Then
                ContactPrefix = txtContactSocialTitle.Text
            Else
                ContactPrefix = ""
            End If
            If txtContactPedigree.Text <> "" Then
                ContactSuffix = txtContactPedigree.Text
            Else
                ContactSuffix = ""
            End If
            If txtContactFirstName.Text <> "" Then
                ContactFirstName = txtContactFirstName.Text
            Else
                ContactFirstName = "N/A"
            End If
            If txtContactLastName.Text <> "" Then
                ContactLastName = txtContactLastName.Text
            Else
                ContactLastName = "N/A"
            End If
            If txtContactTitle.Text <> "" Then
                ContactTitle = txtContactTitle.Text
            Else
                ContactTitle = "N/A"
            End If
            If mtbContactPhoneNumber.Text <> "" Then
                ContactPhoneNumber = Replace(Replace(Replace(Replace(mtbContactPhoneNumber.Text, "(", ""), ")", ""), " ", ""), "-", "")
            Else
                ContactPhoneNumber = "0000000000"
            End If
            If mtbContactNumberExtension.Text <> "" Then
                ContactPhoneNumber = ContactPhoneNumber & mtbContactNumberExtension.Text
            End If
            If txtMailingAddress.Text <> "" Then
                MailingStreet = txtMailingAddress.Text
            Else
                MailingStreet = "N/A"
            End If
            If txtMailingCity.Text <> "" Then
                MailingCity = txtMailingCity.Text
            Else
                MailingCity = "N/A"
            End If
            If txtMailingState.Text <> "" Then
                MailingState = txtMailingState.Text
            Else
                MailingState = "GA"
            End If
            If mtbMailingZipCode.Text <> "" Then
                MailingZipCode = mtbMailingZipCode.Text
            Else
                MailingZipCode = "00000"
            End If
            If txtFacilityComments.Text = "" Then
                Comments = "Created with Facility Creator tool by " & CurrentUser.AlphaName & " on " & OracleDate & vbCrLf
            End If

            If txtFacilityComments.Text.Contains("Created by Facility Creator by " & CurrentUser.AlphaName & " on " & OracleDate) Then
            Else
                Comments = "Created with Facility Creator tool by " & CurrentUser.AlphaName & " on " & OracleDate &
                               vbCrLf & txtFacilityComments.Text & vbCrLf
            End If
            If txtApplicationNumber.Text <> "App No." And
                        txtFacilityComments.Text.Contains(txtApplicationNumber.Text) = False Then
                Comments = Comments & "Pre-loaded with Application " & txtApplicationNumber.Text
            End If

            SQL = "update AIRBRANCH.APBFacilityInformation set " &
            "strFacilityName = '" & Replace(FacilityName, "'", "''") & "', " &
            "strFacilityStreet1 = '" & Replace(FacilityStreet, "'", "''") & "', " &
            "strFacilityCity = '" & Replace(FacilityCity, "'", "''") & "', " &
            "strFacilityZipCode = '" & Replace(FacilityZipCode, "'", "''") & "', " &
            "strModifingPerson = '" & CurrentUser.UserID & "', " &
            "datModifingdate = '" & OracleDate & "', " &
            "numfacilitylongitude = '" & Replace(FacilityLongitude, "'", "''") & "', " &
            "numFacilityLatitude = '" & Replace(FacilityLatitude, "'", "''") & "' " &
            "where strAirsnumber = '" & AIRSNumber & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Update AIRBRANCH.APBHeaderData set " &
            "strOperationalStatus = '" & Replace(OperatingStatus, "'", "''") & "', " &
            "strClass = '" & Replace(Classification, "'", "''") & "', " &
            "strAIRProgramCodes = '" & Replace(AirProgramCode, "'", "''") & "', " &
            "strSICCode = '" & Replace(SICCode, "'", "''") & "', " &
            "strNAICSCode = '" & Replace(NAICSCode, "'", "''") & "', " &
            "strPlantDescription = '" & Replace(PlantDesc, "'", "''") & "', " &
            "strComments = '" & Replace(Comments, "'", "''") & "', " &
            "strModifingPerson = '" & CurrentUser.UserID & "', " &
            "datModifingDate = '" & OracleDate & "' " &
            "where strAIRSNumber = '" & AIRSNumber & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Update AIRBRANCH.APBSupplamentalData set " &
            "strDistrictOffice = '" & Replace(DistrictOffice, "'", "''") & "', " &
            "strModifingPerson = '" & CurrentUser.UserID & "', " &
            "datModifingDate = '" & OracleDate & "' " &
            "where strAIRSNumber = '" & AIRSNumber & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "update AIRBRANCH.APBContactInformation set " &
            "strContactAddress1 = '" & Replace(MailingStreet, "'", "''") & "', " &
            "strContactCity = '" & Replace(MailingCity, "'", "''") & "', " &
            "strContactState = '" & Replace(MailingState, "'", "''") & "', " &
            "strContactZipCode = '" & Replace(MailingZipCode, "'", "''") & "', " &
            "strContactFirstName = '" & Replace(ContactFirstName, "'", "''") & "', " &
            "strContactLastName = '" & Replace(ContactLastName, "'", "''") & "', " &
            "strContactPrefix = '" & Replace(ContactPrefix, "'", "''") & "', " &
            "strContactSuffix = '" & Replace(ContactSuffix, "'", "''") & "', " &
            "strContactTitle = '" & Replace(ContactTitle, "'", "''") & "', " &
            "strContactPhoneNumber1 = '" & Replace(ContactPhoneNumber, "'", "''") & "', " &
            "strModifingPerson = '" & CurrentUser.UserID & "', " &
            "datModifingDate = '" & OracleDate & "' " &
            "where strAIRSNumber = '" & AIRSNumber & "' " &
            "and strContactKey = '" & AIRSNumber & "30' " &
            "and strKey = '30' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            MsgBox("Facility has been updated", MsgBoxStyle.Information, Me.Text)

            If TCFacilityTools.TabPages.Contains(TPApproveNewFacility) Then
                LoadPendingFacilities()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbFilterNewFacilities_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbFilterNewFacilities.CheckedChanged
        Try
            If chbFilterNewFacilities.Checked = True Then
                dtpStartFilter.Enabled = True
                dtpEndFilter.Enabled = True
                chbIncludeApproved.Enabled = True
            Else
                dtpStartFilter.Enabled = False
                dtpEndFilter.Enabled = False
                chbIncludeApproved.Enabled = False
                chbIncludeApproved.Checked = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnFilterNewFacilities_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterNewFacilities.Click
        Try

            LoadPendingFacilities()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DeleteAirsNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteAirsNumber.Click
        Try
            If Not Apb.ApbFacilityId.IsValidAirsNumberFormat(AirsNumberToDelete.Text) Then
                MessageBox.Show("AIRS number is not valid", "Invalid AIRS number", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim airsNumberDeleting As New Apb.ApbFacilityId(AirsNumberToDelete.Text)

            If Not DAL.FacilityData.FacilityHasBeenApproved(airsNumberDeleting) Then
                MessageBox.Show("Facility has not been approved yet. Remove facility using the ""Approve New Facilities"" tab.", "Can't delete", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim result As DialogResult
            result = MessageBox.Show("Are you sure you want to completely remove this facility from the database? The data will not be recoverable.", "Confirm facility deletion",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
            If result = DialogResult.No Then
                Exit Sub
            End If

            If DAL.FacilityData.DeleteFacility(airsNumberDeleting) Then
                MessageBox.Show("Facility removed from the database", "Gone", MessageBoxButtons.OK)
            Else
                MessageBox.Show("There was an error when attempting to remove the facility from the database." & vbNewLine & vbNewLine & "Facility has not been removed.", "Error", MessageBoxButtons.OK)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub AirsNumberToDelete_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AirsNumberToDelete.TextChanged
        FacilityLongDisplay.Text = ""
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(AirsNumberToDelete.Text) Then
            Dim fac As Apb.Facilities.Facility = DAL.FacilityData.GetFacility(AirsNumberToDelete.Text)
            If fac IsNot Nothing Then
                fac.HeaderData = DAL.FacilityHeaderDataData.GetFacilityHeaderData(AirsNumberToDelete.Text)
                If fac.HeaderData IsNot Nothing Then FacilityLongDisplay.Text = fac.LongDisplay
            End If
        End If
    End Sub

End Class