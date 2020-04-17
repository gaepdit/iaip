Imports System.Data.SqlClient
Imports Iaip.Apb
Imports Iaip.Apb.Facilities

Public Class IAIPFacilityCreator

    Private Sub IAIPFacilityCreator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadCounty()
            TCFacilityTools.TabPages.Remove(TPApproveNewFacility)
            TCFacilityTools.TabPages.Remove(TPDeleteFacility)

            If AccountFormAccess(138, 0) IsNot Nothing AndAlso
                AccountFormAccess(138, 0) = "138" AndAlso
                (AccountFormAccess(138, 3) = "1" OrElse AccountFormAccess(138, 4) = "1") Then

                TCFacilityTools.TabPages.Add(TPApproveNewFacility)
                dtpStartFilter.Value = Today
                dtpEndFilter.Value = Today
                DTPSSCPApproveDate.Value = Today
                DTPSSPPApproveDate.Value = Today

                TCFacilityTools.TabPages.Add(TPDeleteFacility)

                LoadPendingFacilities()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadCounty()
        Try
            With cboCounty
                .DataSource = GetSharedData(SharedTable.Counties)
                .DisplayMember = "County"
                .ValueMember = "CountyCode"
                .SelectedIndex = -1
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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadPendingFacilities()
        Dim SQL As String
        Try
            If chbIncludeApproved.Checked = True Then
                SQL = "select " &
                    "FUllData.AIRSNumber, strFacilityName, " &
                    "DateCreated, strComments, " &
                    "SSCPApprover, datApproveDateSSCP, strCommentSSCP, " &
                    "SSPPApprover, datApproveDateSSPP, strCommentSSPP, " &
                    "strfacilityStreet1 " &
                    "from " &
                    "(select substring(APBMasterAIRS.strAIRSNumber, 5, 8) as AIRSNumber, " &
                    "strFacilityName, AFSFacilityData.datModifingDate as dateCreated, " &
                    "APBHeaderData.strComments,  " &
                    "datApproveDateSSCP, strCommentSSCP, " &
                    "datApproveDateSSPP, strCommentSSPP, " &
                    "strfacilityStreet1 " &
                    "from AFSFacilityData " &
                    "inner join APBFacilityInformation " &
                    "on AFSFacilityData.strAIRSNumber = APBFacilityInformation.strAIRSnumber  " &
                    "inner join APBMasterAIRS " &
                    "on AFSFacilityData.strAIRSNumber = APBMasterAIRS.strAIRSnumber  " &
                    "inner join APBHeaderData " &
                    "on AFSFacilityData.strAIRSnumber = APBHeaderData.strAIRSNumber " &
                    "inner join APBSupplamentalData " &
                    "on AFSFacilityData.strAIRSNumber = APBSupplamentalData.strAIRSNumber " &
                    " ) FullData " &
                    "left join " &
                    "(select substring(AFSFacilityData.strAIRSNumber, 5, 8) as AIRSNumber, " &
                    "case " &
                    "when numApprovingSSCP is not null then concat(strLastName, ', ', strFirstName) " &
                    "else null " &
                    "end SSCPApprover " &
                    "from AFSFacilityData " &
                    "inner join APBSupplamentalData " &
                    "on AFSFacilityData.strAIRSNumber = APBSupplamentalData.strAIRSNumber " &
                    "left join EPDUserProfiles " &
                    "on APBSupplamentalData.numApprovingSSCP = EPDUserProfiles.numUserID " &
                    " ) SSCPStaff " &
                    "on FullData.AIRSNumber = SSCPStaff.AIRSNumber " &
                    " left join " &
                    "(select substring(AFSFacilityData.strAIRSNumber, 5, 8) as AIRSNumber, " &
                    "case " &
                    "when numApprovingSSPP is not null then concat(strLastName, ', ', strFirstName) " &
                    "else null " &
                    "end SSPPApprover " &
                    "from AFSFacilityData, APBSupplamentalData, " &
                    "EPDUserProfiles " &
                    "where  AFSFacilityData.strAIRSNumber = APBSupplamentalData.strAIRSNumber " &
                    "and APBSupplamentalData.numApprovingSSPP = EPDUserProfiles.numUserID " &
                    " ) SSPPStaff " &
                    "on FullData.AIRSNumber = SSPPStaff.AIRSNumber "

            Else
                SQL = "select " &
                    "FUllData.AIRSNumber, strFacilityName, " &
                    "DateCreated, strComments, " &
                    "SSCPApprover, datApproveDateSSCP, strCommentSSCP, " &
                    "SSPPApprover, datApproveDateSSPP, strCommentSSPP, " &
                    "strfacilityStreet1 " &
                    "from " &
                    "(select substring(APBMasterAIRS.strAIRSNumber, 5, 8) as AIRSNumber, " &
                    "strFacilityName, AFSFacilityData.datModifingDate as dateCreated, " &
                    "APBHeaderData.strComments,  " &
                    "datApproveDateSSCP, strCommentSSCP, " &
                    "datApproveDateSSPP, strCommentSSPP, " &
                    "strfacilityStreet1 " &
                    "from AFSFacilityData " &
                    "inner join APBFacilityInformation " &
                    "on AFSFacilityData.strAIRSNumber = APBFacilityInformation.strAIRSnumber  " &
                    "inner join APBMasterAIRS " &
                    "on AFSFacilityData.strAIRSNumber = APBMasterAIRS.strAIRSnumber  " &
                    "inner join APBHeaderData " &
                    "on AFSFacilityData.strAIRSnumber = APBHeaderData.strAIRSNumber " &
                    "inner join APBSupplamentalData " &
                    "on AFSFacilityData.strAIRSNumber = APBSupplamentalData.strAIRSNumber " &
                    "where strUpdateStatus = 'H') FullData " &
                    " left join " &
                    "(select substring(AFSFacilityData.strAIRSNumber, 5, 8) as AIRSNumber, " &
                    "case " &
                    "when numApprovingSSCP is not null then concat(strLastName, ', ', strFirstName) " &
                    "else null " &
                    "end SSCPApprover " &
                    "from AFSFacilityData " &
                    "inner join APBSupplamentalData " &
                    "on AFSFacilityData.strAIRSNumber = APBSupplamentalData.strAIRSNumber " &
                    "left join EPDUserProfiles " &
                    "on APBSupplamentalData.numApprovingSSCP = EPDUserProfiles.numUserID " &
                    "where strUpdateStatus = 'H') SSCPStaff " &
                    "on FullData.AIRSNumber = SSCPStaff.AIRSNumber " &
                    " left join " &
                    "(select substring(AFSFacilityData.strAIRSNumber, 5, 8) as AIRSNumber, " &
                    "case " &
                    "when numApprovingSSPP is not null then concat(strLastName, ', ', strFirstName) " &
                    "else null " &
                    "end SSPPApprover " &
                    "from AFSFacilityData " &
                    "inner join APBSupplamentalData " &
                    "on AFSFacilityData.strAIRSNumber = APBSupplamentalData.strAIRSNumber " &
                    "left join EPDUserProfiles " &
                    "on APBSupplamentalData.numApprovingSSPP = EPDUserProfiles.numUserID " &
                    "where strUpdateStatus = 'H') SSPPStaff " &
                    "on FullData.AIRSNumber = SSPPStaff.AIRSNumber "
            End If

            If chbFilterNewFacilities.Checked = True Then
                SQL = SQL & " where dateCreated between @datestart and @dateend"
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@datestart", dtpStartFilter.Value),
                New SqlParameter("@dateend", dtpEndFilter.Value)
            }

            dgvVerifyNewFacilities.DataSource = DB.GetDataTable(SQL, p)

            dgvVerifyNewFacilities.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke

            dgvVerifyNewFacilities.Columns("AIRSNumber").HeaderText = "AIRS Number"
            dgvVerifyNewFacilities.Columns("AIRSNumber").DisplayIndex = 0
            dgvVerifyNewFacilities.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvVerifyNewFacilities.Columns("strFacilityName").DisplayIndex = 1
            dgvVerifyNewFacilities.Columns("dateCreated").HeaderText = "Date Created"
            dgvVerifyNewFacilities.Columns("dateCreated").DisplayIndex = 2
            dgvVerifyNewFacilities.Columns("dateCreated").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvVerifyNewFacilities.Columns("strComments").HeaderText = "Comments"
            dgvVerifyNewFacilities.Columns("strComments").DisplayIndex = 3

            dgvVerifyNewFacilities.Columns("SSCPApprover").HeaderText = "SSCP Approver"
            dgvVerifyNewFacilities.Columns("SSCPApprover").DisplayIndex = 4
            dgvVerifyNewFacilities.Columns("datApproveDateSSCP").HeaderText = "Date SSCP Approved"
            dgvVerifyNewFacilities.Columns("datApproveDateSSCP").DisplayIndex = 5
            dgvVerifyNewFacilities.Columns("datApproveDateSSCP").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvVerifyNewFacilities.Columns("strCommentSSCP").HeaderText = "SSCP Comments"
            dgvVerifyNewFacilities.Columns("strCommentSSCP").DisplayIndex = 6
            dgvVerifyNewFacilities.Columns("SSPPApprover").HeaderText = "SSPP Approver"
            dgvVerifyNewFacilities.Columns("SSPPApprover").DisplayIndex = 7
            dgvVerifyNewFacilities.Columns("datApproveDateSSPP").HeaderText = "Date SSPP Approved"
            dgvVerifyNewFacilities.Columns("datApproveDateSSPP").DisplayIndex = 8
            dgvVerifyNewFacilities.Columns("datApproveDateSSPP").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvVerifyNewFacilities.Columns("strCommentSSPP").HeaderText = "SSPP Comments"
            dgvVerifyNewFacilities.Columns("strCommentSSPP").DisplayIndex = 9
            dgvVerifyNewFacilities.Columns("strFacilityStreet1").HeaderText = "Street Address"
            dgvVerifyNewFacilities.Columns("strFacilityStreet1").DisplayIndex = 10
            dgvVerifyNewFacilities.Columns("strFacilityStreet1").Visible = False

            dgvVerifyNewFacilities.SanelyResizeColumns()

            txtCountFacilities.Text = dgvVerifyNewFacilities.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FindRegion(AIRSNumber As String)
        Dim Region As String

        Try
            If ApbFacilityId.IsValidAirsNumberFormat(AIRSNumber) Then
                Dim SQL As String = "Select concat(LookUPDistricts.strDistrictcode, '-', strDistrictName) as District " &
                "from LookUPDistricts inner join LookUPDistrictInformation " &
                "on LookUPDistricts.strDistrictCode = LookUPDistrictInformation.strDistrictCode " &
                "where strDistrictCounty = @county "

                Dim p As New SqlParameter("@county", Mid(AIRSNumber, 1, 3))

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
                    Region = dr.Item("District").ToString
                Else
                    Region = "WARNING"
                End If
            Else
                Region = "WARNING"
            End If

            txtCDSRegionCode.Text = Region

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveNewFacility_Click(sender As Object, e As EventArgs) Handles btnSaveNewFacility.Click
        SaveNewFacility()
    End Sub

    Private Sub SaveNewFacility()
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

            If IsDBNull(cboCounty.SelectedValue) OrElse cboCounty.SelectedValue Is Nothing OrElse cboCounty.SelectedIndex = -1 Then
                MsgBox("Invalid County Selected." & vbCrLf & "No Data Saved", MsgBoxStyle.Information, Me.Name)
                Return
            End If
            If txtCDSAIRSNumber.Text <> "" Then
                MsgBox("There is an existing AIRS # associated with this data." &
                    "Either use the Edit button or clear the AIRS # before Saving a new Facility.", MsgBoxStyle.Information, Me.Name)
                Return
            End If
            If mtbFacilityLatitude.Text = "  ." Then
                MsgBox("The Latitude field needs to be addressed." &
                 "No Data saved.", MsgBoxStyle.Information, Me.Name)
                Return
            End If
            If mtbFacilityLongitude.Text = "-  ." Then
                MsgBox("The Longitude field needs to be addressed." &
                "No Data saved.", MsgBoxStyle.Information, Me.Name)
                Return
            End If
            If Not DAL.NaicsCodeIsValid(mtbCDSNAICSCode.Text, False) Then
                MsgBox("The NAICS Code is not valid and must be fixed before proceeding." &
                  "No Data saved.", MsgBoxStyle.Information, Me.Name)
                Return
            End If
            If Not DAL.SicCodeIsValid(mtbCDSSICCode.Text) Then
                MsgBox("The SIC Code is not valid and must be fixed before proceeding." &
                "No Data saved.", MsgBoxStyle.Information, Me.Name)
                Return
            End If

            Dim SQL As String = "INSERT INTO APBMASTERAIRS (
                STRAIRSNUMBER,
                STRMODIFINGPERSON,
                DATMODIFINGDATE
            )
                SELECT
                    CONCAT(
                        '0413',
                        @cty,
                        Replace(Str(isnull(MAX(CONVERT(int, right(STRAIRSNUMBER, 5))), 0) + 1, 5), ' ', '0')
                    ),
                    @user,
                    GETDATE()
                FROM APBMASTERAIRS
                WHERE SUBSTRING(STRAIRSNUMBER, 5, 3) = @cty "

            Dim p As SqlParameter() = {
                New SqlParameter("@cty", cboCounty.SelectedValue),
                New SqlParameter("@user", CurrentUser.UserID)
            }

            DB.RunCommand(SQL, p)

            SQL = "SELECT SUBSTRING(MAX(strAIRSNumber), 5, 8) AS AIRSNumber " &
                "FROM   APBMasterAIRS " &
                "WHERE  SUBSTRING(strAIRSNumber, 5, 3) = @cty "

            txtCDSAIRSNumber.Text = DB.GetString(SQL, p)

            If txtCDSAIRSNumber.Text = "" Then
                MsgBox("There was an error in creating Facility." & "Contact EPD IT before proceeding", MsgBoxStyle.Information, Me.Text)
                Return
            Else
                AIRSNumber = "0413" & txtCDSAIRSNumber.Text
            End If

            FindRegion(txtCDSAIRSNumber.Text)

            If txtCDSFacilityName.Text = "" Then
                FacilityName = "N/A"
            Else
                txtCDSFacilityName.Text = Facility.SanitizeFacilityNameForDb(txtCDSFacilityName.Text)
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
            If String.IsNullOrEmpty(txtContactPhoneNumber.Text) Then
                ContactPhoneNumber = txtContactPhoneNumber.Text
            Else
                ContactPhoneNumber = "0000000000"
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
                Comments = "Created with Facility Creator tool by " & CurrentUser.AlphaName & " on " & TodayFormatted & vbCrLf
            End If

            If txtFacilityComments.Text.Contains("Created by Facility Creator by " & CurrentUser.AlphaName & " on " & TodayFormatted) Then
            Else
                Comments = "Created with Facility Creator tool by " & CurrentUser.AlphaName & " on " & TodayFormatted &
                               vbCrLf & txtFacilityComments.Text & vbCrLf
            End If
            If String.IsNullOrWhiteSpace(txtApplicationNumber.Text) And
                txtFacilityComments.Text.Contains(txtApplicationNumber.Text) = False Then
                Comments = Comments & "Pre-loaded with Application " & txtApplicationNumber.Text
            End If
            If mtbRiskManagementNumber.Text <> "" Then
                RMPNumber = mtbRiskManagementNumber.Text
            Else
                RMPNumber = ""
            End If

            SQL = "Insert into APBFacilityInformation " &
                "(strAIRSNumber, strFacilityName, " &
                "strFacilityStreet1, strFacilityStreet2, " &
                "strFacilityCity, strFacilityState, " &
                "strFacilityZipCode, strModifingPerson, " &
                "datModifingDate, numFacilityLongitude, " &
                "numFacilityLatitude, strHorizontalCollectionCode, " &
                "strHorizontalAccuracyMeasure, strHorizontalReferenceCode, " &
                "strModifingLocation ) " &
                "values " &
                "(@strAIRSNumber, @strFacilityName, " &
                "@strFacilityStreet1, @strFacilityStreet2, " &
                "@strFacilityCity, @strFacilityState, " &
                "@strFacilityZipCode, @strModifingPerson, " &
                "getdate(), @numFacilityLongitude, " &
                "@numFacilityLatitude, @strHorizontalCollectionCode, " &
                "@strHorizontalAccuracyMeasure, @strHorizontalReferenceCode, " &
                "@strModifingLocation ) "

            Dim p2 As SqlParameter() = {
                New SqlParameter("@strAIRSNumber", AIRSNumber),
                New SqlParameter("@strFacilityName", FacilityName),
                New SqlParameter("@strFacilityStreet1", FacilityStreet),
                New SqlParameter("@strFacilityStreet2", "N/A"),
                New SqlParameter("@strFacilityCity", FacilityCity),
                New SqlParameter("@strFacilityState", "GA"),
                New SqlParameter("@strFacilityZipCode", FacilityZipCode),
                New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                New SqlParameter("@numFacilityLongitude", FacilityLongitude),
                New SqlParameter("@numFacilityLatitude", FacilityLatitude),
                New SqlParameter("@strHorizontalCollectionCode", "007"),
                New SqlParameter("@strHorizontalAccuracyMeasure", "25"),
                New SqlParameter("@strHorizontalReferenceCode", "002"),
                New SqlParameter("@strModifingLocation", "4")
            }

            DB.RunCommand(SQL, p2)

            Dim AttainmentStatus As String = "00000"

            SQL = "select " &
                "strNonAttainment " &
                "from LookUpCountyInformation " &
                "where strCountyCode = @cty "

            Dim p3 As New SqlParameter("@cty", Mid(AIRSNumber, 5, 3))

            Dim dr As DataRow = DB.GetDataRow(SQL, p3)
            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strNonAttainment")) Then
                    AttainmentStatus = "00000"
                Else
                    AttainmentStatus = dr.Item("strNonAttainment").ToString
                End If
            End If

            SQL = "INSERT INTO APBHEADERDATA " &
                "( STRAIRSNUMBER, STROPERATIONALSTATUS, STRCLASS, STRAIRPROGRAMCODES " &
                ", STRSICCODE, STRFEINUMBER, STRMODIFINGPERSON, DATMODIFINGDATE " &
                ", STRCOMMENTS, STRPLANTDESCRIPTION " &
                ", STRATTAINMENTSTATUS, STRMODIFINGLOCATION, STRNAICSCODE) " &
                "VALUES " &
                "( @STRAIRSNUMBER, @STROPERATIONALSTATUS, @STRCLASS, @STRAIRPROGRAMCODES " &
                ", @STRSICCODE, @STRFEINUMBER, @STRMODIFINGPERSON, getdate() " &
                ", @STRCOMMENTS, @STRPLANTDESCRIPTION " &
                ", @STRATTAINMENTSTATUS, @STRMODIFINGLOCATION, @STRNAICSCODE) "

            Dim p4 As SqlParameter() = {
                New SqlParameter("@STRAIRSNUMBER", AIRSNumber),
                New SqlParameter("@STROPERATIONALSTATUS", OperatingStatus),
                New SqlParameter("@STRCLASS", Classification),
                New SqlParameter("@STRAIRPROGRAMCODES", AirProgramCode),
                New SqlParameter("@STRSICCODE", RealStringOrNothing(SICCode)),
                New SqlParameter("@STRFEINUMBER", "N/A"),
                New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID),
                New SqlParameter("@STRCOMMENTS", Comments),
                New SqlParameter("@STRPLANTDESCRIPTION", PlantDesc),
                New SqlParameter("@STRATTAINMENTSTATUS", AttainmentStatus),
                New SqlParameter("@STRMODIFINGLOCATION", "4"),
                New SqlParameter("@STRNAICSCODE", RealStringOrNothing(NAICSCode))
            }

            DB.RunCommand(SQL, p4)

            SQL = "INSERT INTO APBSUPPLAMENTALDATA " &
                "( STRAIRSNUMBER, STRMODIFINGPERSON, DATMODIFINGDATE " &
                ", STRDISTRICTOFFICE, STRAFSACTIONNUMBER, STRRMPID) " &
                "VALUES " &
                "( @STRAIRSNUMBER, @STRMODIFINGPERSON, getdate() " &
                ", @STRDISTRICTOFFICE, @STRAFSACTIONNUMBER, @STRRMPID) "

            Dim p5 As SqlParameter() = {
                New SqlParameter("@STRAIRSNUMBER", AIRSNumber),
                New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID),
                New SqlParameter("@STRDISTRICTOFFICE", DistrictOffice),
                New SqlParameter("@STRAFSACTIONNUMBER", "00001"),
                New SqlParameter("@STRRMPID", RMPNumber)
            }

            DB.RunCommand(SQL, p5)

            SQL = "insert into APBContactInformation " &
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
            "(@strContactKey, @strAIRSNumber, @strKey, " &
            "@strContactFirstName, @strContactLastName, " &
            "@strContactPrefix, @strContactSuffix, " &
            "@strContactTitle, @strContactCompanyName, " &
            "@strContactPhoneNumber1, @strContactPhoneNumber2, " &
            "@strContactFaxNumber, @strContactEmail, " &
            "@strContactAddress1, @strContactAddress2, " &
            "@strContactCity, @strContactState, " &
            "@strContactZipCode, @strModifingPerson, " &
            "getdate() ) "

            Dim p6 As SqlParameter() = {
                    New SqlParameter("@strContactKey", AIRSNumber & "30"),
                    New SqlParameter("@strAIRSNumber", AIRSNumber),
                    New SqlParameter("@strKey", "30"),
                    New SqlParameter("@strContactFirstName", ContactFirstName),
                    New SqlParameter("@strContactLastName", ContactLastName),
                    New SqlParameter("@strContactPrefix", ContactPrefix),
                    New SqlParameter("@strContactSuffix", ContactSuffix),
                    New SqlParameter("@strContactTitle", ContactTitle),
                    New SqlParameter("@strContactCompanyName", "N/A"),
                    New SqlParameter("@strContactPhoneNumber1", ContactPhoneNumber),
                    New SqlParameter("@strContactPhoneNumber2", "N/A"),
                    New SqlParameter("@strContactFaxNumber", "N/A"),
                    New SqlParameter("@strContactEmail", "N/A"),
                    New SqlParameter("@strContactAddress1", MailingStreet),
                    New SqlParameter("@strContactAddress2", "N/A"),
                    New SqlParameter("@strContactCity", MailingCity),
                    New SqlParameter("@strContactState", MailingState),
                    New SqlParameter("@strContactZipCode", MailingZipCode),
                    New SqlParameter("@strModifingPerson", CurrentUser.UserID)
            }

            DB.RunCommand(SQL, p6)

            Dim os As FacilityOperationalStatus = CType([Enum].Parse(GetType(FacilityOperationalStatus), OperatingStatus), FacilityOperationalStatus)

            If chbCDS_1.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(New ApbFacilityId(AIRSNumber), AirPrograms.SIP, "OT", os)
            End If
            If chbCDS_2.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(New ApbFacilityId(AIRSNumber), AirPrograms.FederalSIP, "OT", os)
            End If
            If chbCDS_3.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(New ApbFacilityId(AIRSNumber), AirPrograms.NonFederalSIP, "OT", os)
            End If
            If chbCDS_4.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(New ApbFacilityId(AIRSNumber), AirPrograms.CfcTracking, "OT", os)
            End If
            If chbCDS_5.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(New ApbFacilityId(AIRSNumber), AirPrograms.PSD, "OT", os)
            End If
            If chbCDS_6.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(New ApbFacilityId(AIRSNumber), AirPrograms.NSR, "OT", os)
            End If
            If chbCDS_7.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(New ApbFacilityId(AIRSNumber), AirPrograms.NESHAP, "OT", os)
            End If
            If chbCDS_8.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(New ApbFacilityId(AIRSNumber), AirPrograms.NSPS, "OT", os)
            End If
            If chbCDS_9.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(New ApbFacilityId(AIRSNumber), AirPrograms.FESOP, "OT", os)
            End If
            If chbCDS_10.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(New ApbFacilityId(AIRSNumber), AirPrograms.AcidPrecipitation, "OT", os)
            End If
            If chbCDS_11.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(New ApbFacilityId(AIRSNumber), AirPrograms.NativeAmerican, "OT", os)
            End If
            If chbCDS_12.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(New ApbFacilityId(AIRSNumber), AirPrograms.MACT, "OT", os)
            End If
            If chbCDS_13.Checked = True Then
                DAL.InsertFacilityAirProgramPollutant(New ApbFacilityId(AIRSNumber), AirPrograms.TitleV, "OT", os)
            End If

            SQL = "Insert into SSCPDistrictResponsible " &
                "( STRAIRSNUMBER, STRDISTRICTRESPONSIBLE, STRASSIGNINGMANAGER, DATASSIGNINGDATE) " &
                "values " &
                "(@airs, 'False', '1', getdate()) "

            Dim p7 As New SqlParameter("@airs", AIRSNumber)

            DB.RunCommand(SQL, p7)

            MsgBox("Facility Added to Integrated Air Information Platform", MsgBoxStyle.Information, Me.Text)

            If TCFacilityTools.TabPages.Contains(TPApproveNewFacility) Then
                LoadPendingFacilities()
            End If

            btnEditFacilityData.Visible = True
            btnSaveNewFacility.Visible = False
            cboCounty.Enabled = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnPreLoadNewFacility_Click(sender As Object, e As EventArgs) Handles btnPreLoadNewFacility.Click
        Try
            Dim appnum As String = txtApplicationNumber.Text
            ClearNewFacility()
            txtApplicationNumber.Text = appnum

            If Not String.IsNullOrWhiteSpace(txtApplicationNumber.Text) Then

                Dim SQL As String = "select
                    right(strairsnumber, 8) as airs,
                    strFacilityName,
                    strFacilityStreet1,
                    strFacilityCity,
                    strFacilityZipCode,
                    strOperationalStatus,
                    strClass,
                    strAirProgramCodes,
                    strSICCode,
                    strNAICSCode,
                    strPlantDescription,
                    strContactFirstName,
                    strContactLastName,
                    strContactpreFix,
                    strContactSuffix,
                    strContactTitle,
                    strContactPhoneNumber1
                from SSPPApplicationdata d
                    inner join SSPPApplicationContact c
                        on d.strApplicationNumber = c.strApplicationNumber
                    inner join SSPPAPPLICATIONMASTER m
                        on m.STRAPPLICATIONNUMBER = d.STRAPPLICATIONNUMBER
                where d.strApplicationNumber = @app"

                Dim p As New SqlParameter("@app", txtApplicationNumber.Text)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
                    If Not IsDBNull(dr.Item("airs")) AndAlso ApbFacilityId.IsValidAirsNumberFormat(dr.Item("airs").ToString) Then
                        txtCDSAIRSNumber.Text = dr.Item("airs").ToString
                        btnSaveNewFacility.Visible = False
                        btnEditFacilityData.Visible = True
                        cboCounty.Enabled = False
                        cboCounty.SelectedValue = Mid(txtCDSAIRSNumber.Text, 1, 3)
                    Else
                        txtCDSAIRSNumber.Text = ""
                        btnSaveNewFacility.Visible = True
                        btnEditFacilityData.Visible = False
                        cboCounty.Enabled = True
                        cboCounty.SelectedIndex = -1
                    End If

                    If IsDBNull(dr.Item("strFacilityName")) Then
                        txtCDSFacilityName.Clear()
                    Else
                        txtCDSFacilityName.Text = dr.Item("strFacilityname").ToString
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        txtCDSStreetAddress.Clear()
                        txtMailingAddress.Clear()
                    Else
                        txtCDSStreetAddress.Text = dr.Item("strFacilityStreet1").ToString
                        txtMailingAddress.Text = dr.Item("strFacilityStreet1").ToString
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        txtCDSCity.Clear()
                        txtMailingCity.Clear()
                    Else
                        txtCDSCity.Text = dr.Item("strFacilityCity").ToString
                        txtMailingCity.Text = dr.Item("strFacilityCity").ToString
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        mtbCDSZipCode.Clear()
                        mtbMailingZipCode.Clear()
                    Else
                        mtbCDSZipCode.Text = dr.Item("strFacilityZipCode").ToString
                        mtbMailingZipCode.Text = dr.Item("strFacilityZipCode").ToString
                    End If

                    If IsDBNull(dr.Item("strOperationalStatus")) Then
                        cboCDSOperationalStatus.Text = ""
                    Else
                        Select Case dr.Item("strOperationalStatus").ToString
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
                    End If

                    If IsDBNull(dr.Item("strClass")) Then
                        cboCDSClassCode.Text = ""
                    Else
                        Select Case dr.Item("strClass").ToString
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
                        Dim temp As String = dr.Item("strAirProgramCodes").ToString

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
                        mtbCDSSICCode.Text = dr.Item("strSICCode").ToString
                    End If
                    If IsDBNull(dr.Item("strNAICSCode")) Then
                        mtbCDSNAICSCode.Clear()
                    Else
                        mtbCDSNAICSCode.Text = dr.Item("strNAICScode").ToString
                    End If
                    If IsDBNull(dr.Item("strPlantDescription")) Then
                        txtFacilityDescription.Clear()
                    Else
                        txtFacilityDescription.Text = dr.Item("strPlantDescription").ToString
                    End If
                    If IsDBNull(dr.Item("strContactFirstName")) Then
                        txtContactFirstName.Clear()
                    Else
                        txtContactFirstName.Text = dr.Item("strContactFirstName").ToString
                    End If
                    If IsDBNull(dr.Item("strContactLastName")) Then
                        txtContactLastName.Clear()
                    Else
                        txtContactLastName.Text = dr.Item("strContactLastName").ToString
                    End If
                    If IsDBNull(dr.Item("strContactPreFix")) Then
                        txtContactSocialTitle.Clear()
                    Else
                        txtContactSocialTitle.Text = dr.Item("strContactPrefix").ToString
                    End If
                    If IsDBNull(dr.Item("strContactSuffix")) Then
                        txtContactPedigree.Clear()
                    Else
                        txtContactPedigree.Text = dr.Item("strContactSuffix").ToString
                    End If
                    If IsDBNull(dr.Item("strContactTitle")) Then
                        txtContactTitle.Clear()
                    Else
                        txtContactTitle.Text = dr.Item("strContactTitle").ToString
                    End If
                    If IsDBNull(dr.Item("strCOntactphoneNumber1")) Then
                        txtContactPhoneNumber.Clear()
                    Else
                        txtContactPhoneNumber.Text = dr.Item("strContactPhoneNumber1").ToString
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbOpenWebpage_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbOpenWebpage.LinkClicked
        Try
            Dim MappingAddress As String = txtCDSStreetAddress.Text & ", " & txtCDSCity.Text & ", GA," & mtbCDSZipCode.Text
            Clipboard.SetDataObject(MappingAddress, True)

            OpenUri(New Uri("https://mapper.acme.com/"), Me)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvVerifyNewFacilities_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvVerifyNewFacilities.CellEnter
        Try
            txtNewFacilityName.Clear()
            txtNewAIRSNumber.Clear()
            txtSSCPApprover.Clear()
            DTPSSCPApproveDate.Value = Today
            txtSSCPComments.Clear()
            txtSSPPApprover.Clear()
            DTPSSPPApproveDate.Value = Today
            txtSSPPComments.Clear()
            chbSSCPSignOff.Checked = False
            chbSSPPSignOff.Checked = False

            If e.RowIndex <> -1 AndAlso e.RowIndex < dgvVerifyNewFacilities.RowCount Then
                dgvVerifyNewFacilities(0, e.RowIndex).Value.ToString()

                If Not IsDBNull(dgvVerifyNewFacilities(0, e.RowIndex)) Then
                    txtNewAIRSNumber.Text = dgvVerifyNewFacilities(0, e.RowIndex).Value.ToString
                End If

                If Not IsDBNull(dgvVerifyNewFacilities(1, e.RowIndex)) Then
                    txtNewFacilityName.Text = dgvVerifyNewFacilities(1, e.RowIndex).Value.ToString()
                End If

                If Not IsDBNull(dgvVerifyNewFacilities(3, e.RowIndex).Value) Then
                    txtApprovialComments.Text = dgvVerifyNewFacilities(3, e.RowIndex).Value.ToString()
                End If

                If Not IsDBNull(dgvVerifyNewFacilities(4, e.RowIndex).Value) Then
                    chbSSCPSignOff.Checked = True
                    txtSSCPApprover.Text = dgvVerifyNewFacilities(4, e.RowIndex).Value.ToString()
                End If

                If Not IsDBNull(dgvVerifyNewFacilities(5, e.RowIndex).Value) Then
                    DTPSSCPApproveDate.Text = dgvVerifyNewFacilities(5, e.RowIndex).Value.ToString()
                End If

                If Not IsDBNull(dgvVerifyNewFacilities(6, e.RowIndex).Value) Then
                    txtSSCPComments.Text = dgvVerifyNewFacilities(6, e.RowIndex).Value.ToString()
                End If

                If Not IsDBNull(dgvVerifyNewFacilities(7, e.RowIndex).Value) Then
                    chbSSPPSignOff.Checked = True
                    txtSSPPApprover.Text = dgvVerifyNewFacilities(7, e.RowIndex).Value.ToString()
                End If

                If Not IsDBNull(dgvVerifyNewFacilities(8, e.RowIndex).Value) Then
                    DTPSSPPApproveDate.Text = dgvVerifyNewFacilities(8, e.RowIndex).Value.ToString()
                End If

                If Not IsDBNull(dgvVerifyNewFacilities(9, e.RowIndex).Value) Then
                    txtSSPPComments.Text = dgvVerifyNewFacilities(9, e.RowIndex).Value.ToString()
                End If

                If Not IsDBNull(dgvVerifyNewFacilities(10, e.RowIndex).Value) Then
                    txtStreetAddress.Text = dgvVerifyNewFacilities(10, e.RowIndex).Value.ToString()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewFacility_Click(sender As Object, e As EventArgs) Handles btnViewFacility.Click
        Try
            Dim temp As String

            If txtNewAIRSNumber.Text = "" Then
                Return
            End If

            txtCDSAIRSNumber.Text = txtNewAIRSNumber.Text
            FindRegion(txtCDSAIRSNumber.Text)

            Dim SQL As String = "select " &
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
            "APBHeaderData.strComments, " &
            "strNAICSCode, strRMPID " &
            "from APBFacilityInformation " &
            "inner join APBHeaderData " &
            "on APBFacilityInformation.strAIRSNumber = APBHeaderData.strAIRSNumber " &
            "inner join APBContactInformation " &
            "on APBFacilityInformation.strAIRSNumber = APBContactInformation.strAIRSNumber " &
            "inner join APBSupplamentalData  " &
            "on APBFacilityInformation.strAIRSNumber = APBSupplamentalData.strAIRSNumber " &
            "where strkey = '30' " &
            "and APBFacilityInformation.strAIRSNumber = @airs "

            Dim p As New SqlParameter("@airs", "0413" & txtNewAIRSNumber.Text)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strComments")) Then
                    txtFacilityComments.Clear()
                Else
                    txtFacilityComments.Text = dr.Item("strComments").ToString
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtCDSFacilityName.Clear()
                Else
                    txtCDSFacilityName.Text = dr.Item("strFacilityname").ToString
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    txtCDSStreetAddress.Clear()
                    txtMailingAddress.Clear()
                Else
                    txtCDSStreetAddress.Text = dr.Item("strFacilityStreet1").ToString
                    txtMailingAddress.Text = dr.Item("strFacilityStreet1").ToString
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    txtCDSCity.Clear()
                    txtMailingCity.Clear()
                Else
                    txtCDSCity.Text = dr.Item("strFacilityCity").ToString
                    txtMailingCity.Text = dr.Item("strFacilityCity").ToString
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    mtbCDSZipCode.Clear()
                    mtbMailingZipCode.Clear()
                Else
                    mtbCDSZipCode.Text = dr.Item("strFacilityZipCode").ToString
                    mtbMailingZipCode.Text = dr.Item("strFacilityZipCode").ToString
                End If
                If IsDBNull(dr.Item("strOperationalStatus")) Then
                    cboCDSOperationalStatus.Text = ""
                Else
                    temp = dr.Item("strOperationalStatus").ToString
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
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    cboCDSClassCode.Text = ""
                Else
                    temp = dr.Item("strClass").ToString
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
                    temp = dr.Item("strAirProgramCodes").ToString
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
                    mtbCDSSICCode.Text = dr.Item("strSICCode").ToString
                End If
                If IsDBNull(dr.Item("strPlantDescription")) Then
                    txtFacilityDescription.Clear()
                Else
                    txtFacilityDescription.Text = dr.Item("strPlantDescription").ToString
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    txtContactFirstName.Clear()
                Else
                    txtContactFirstName.Text = dr.Item("strContactFirstName").ToString
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtContactLastName.Clear()
                Else
                    txtContactLastName.Text = dr.Item("strContactLastName").ToString
                End If
                If IsDBNull(dr.Item("strContactPreFix")) Then
                    txtContactSocialTitle.Clear()
                Else
                    txtContactSocialTitle.Text = dr.Item("strContactPrefix").ToString
                End If
                If IsDBNull(dr.Item("strContactSuffix")) Then
                    txtContactPedigree.Clear()
                Else
                    txtContactPedigree.Text = dr.Item("strContactSuffix").ToString
                End If
                If IsDBNull(dr.Item("strContactTitle")) Then
                    txtContactTitle.Clear()
                Else
                    txtContactTitle.Text = dr.Item("strContactTitle").ToString
                End If
                If IsDBNull(dr.Item("strCOntactphoneNumber1")) Then
                    txtContactPhoneNumber.Clear()
                Else
                    txtContactPhoneNumber.Text = dr.Item("strContactPhoneNumber1").ToString
                End If
                If IsDBNull(dr.Item("numFacilityLongitude")) Then
                    mtbFacilityLongitude.Clear()
                Else
                    mtbFacilityLongitude.Text = dr.Item("numFacilityLongitude").ToString
                End If
                If IsDBNull(dr.Item("numFacilityLatitude")) Then
                    mtbFacilityLatitude.Clear()
                Else
                    mtbFacilityLatitude.Text = dr.Item("numFacilityLatitude").ToString
                End If
                If IsDBNull(dr.Item("strNAICSCode")) Then
                    mtbCDSNAICSCode.Clear()
                Else
                    mtbCDSNAICSCode.Text = dr.Item("strNAICSCode").ToString
                End If
                If IsDBNull(dr.Item("strRMPID")) Then
                    mtbRiskManagementNumber.Clear()
                Else
                    mtbRiskManagementNumber.Text = dr.Item("strRMPID").ToString
                End If
            End If

            cboCounty.SelectedValue = Mid(txtCDSAIRSNumber.Text, 1, 3)

            If TCFacilityTools.TabPages.Contains(TPCreateNewFacility) Then
                TCFacilityTools.SelectedIndex = 0
                btnEditFacilityData.Visible = True
                btnSaveNewFacility.Visible = False
                cboCounty.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSubmitFacilityToAFS_Click(sender As Object, e As EventArgs) Handles btnSubmitFacilityToAFS.Click
        Try
            Dim SSCPSignOff As String = ""
            Dim SSPPSignOff As String = ""
            Dim SQL As String

            If chbSSCPSignOff.Checked = True And chbSSPPSignOff.Checked = True Then
                SQL = "Select " &
                "numApprovingSSCP, numApprovingSSPP " &
                "from APBSupplamentalData " &
                "where strAIRSNumber = @airs "

                Dim p As New SqlParameter("@airs", "0413" & txtNewAIRSNumber.Text)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("numApprovingSSCP")) Then
                        SSCPSignOff = ""
                    Else
                        SSCPSignOff = dr.Item("numApprovingSSCP").ToString
                    End If
                    If IsDBNull(dr.Item("numApprovingSSPP")) Then
                        SSPPSignOff = ""
                    Else
                        SSPPSignOff = dr.Item("numApprovingSSPP").ToString
                    End If
                End If

                If SSCPSignOff = "" And SSPPSignOff = "" Then
                    SQL = "Update APBSupplamentalData set " &
                        "numApprovingSSCP = @numApprovingSSCP " &
                        ", datApproveDateSSCP = @datApproveDateSSCP " &
                        ", strCommentSSCP = @strCommentSSCP " &
                        ", numApprovingSSPP = @numApprovingSSPP " &
                        ", datApproveDateSSPP = @datApproveDateSSPP " &
                        ", strCommentSSPP = @strCommentSSPP " &
                        "where strAIRSnumber = @strAIRSnumber "

                    Dim p2 As SqlParameter() = {
                        New SqlParameter("@numApprovingSSCP", CurrentUser.UserID),
                        New SqlParameter("@datApproveDateSSCP", DTPSSCPApproveDate.Text),
                        New SqlParameter("@strCommentSSCP", txtSSCPComments.Text),
                        New SqlParameter("@numApprovingSSPP", CurrentUser.UserID),
                        New SqlParameter("@datApproveDateSSPP", DTPSSCPApproveDate.Text),
                        New SqlParameter("@strCommentSSPP", txtSSCPComments.Text),
                        New SqlParameter("@strAIRSnumber", "0413" & txtNewAIRSNumber.Text)
                    }

                    DB.RunCommand(SQL, p2)
                Else
                    If SSCPSignOff = "" Then
                        SQL = "Update APBSupplamentalData set " &
                            "numApprovingSSCP = @numApprovingSSCP " &
                            ", datApproveDateSSCP = @datApproveDateSSCP " &
                            ", strCommentSSCP = @strCommentSSCP " &
                            "where strAIRSnumber = @strAIRSnumber "

                        Dim p3 As SqlParameter() = {
                            New SqlParameter("@numApprovingSSCP", CurrentUser.UserID),
                            New SqlParameter("@datApproveDateSSCP", DTPSSCPApproveDate.Text),
                            New SqlParameter("@strCommentSSCP", txtSSCPComments.Text),
                            New SqlParameter("@strAIRSnumber", "0413" & txtNewAIRSNumber.Text)
                        }

                        DB.RunCommand(SQL, p3)
                    End If
                    If SSPPSignOff = "" Then
                        SQL = "Update APBSupplamentalData set " &
                            "numApprovingSSPP = @numApprovingSSPP " &
                            ", datApproveDateSSPP = @datApproveDateSSPP " &
                            ", strCommentSSPP = @strCommentSSPP " &
                            "where strAIRSnumber = @strAIRSnumber "

                        Dim p3 As SqlParameter() = {
                            New SqlParameter("@numApprovingSSPP", CurrentUser.UserID),
                            New SqlParameter("@datApproveDateSSPP", DTPSSCPApproveDate.Text),
                            New SqlParameter("@strCommentSSPP", txtSSCPComments.Text),
                            New SqlParameter("@strAIRSnumber", "0413" & txtNewAIRSNumber.Text)
                        }

                        DB.RunCommand(SQL, p3)
                    End If
                End If

                SQL = "Update AFSFacilityData set " &
                    "strUpdateStatus = 'A' " &
                    "where strAIRSNumber = @airs " &
                    "and strUpdateStatus = 'H' "

                Dim p4 As New SqlParameter("@airs", "0413" & txtNewAIRSNumber.Text)

                DB.RunCommand(SQL, p4)

                MsgBox(txtNewFacilityName.Text & " (" & txtNewAIRSNumber.Text & ") has been approved", MsgBoxStyle.Information, Me.Text)

                LoadPendingFacilities()

                ClearNewFacility()
            Else
                MsgBox("Both SSCP and SSPP have to sign off on the new facility before it can be sent to EPA.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRemoveFromPlatform_Click(sender As Object, e As EventArgs) Handles btnRemoveFromPlatform.Click
        Try
            If Not ApbFacilityId.IsValidAirsNumberFormat(txtNewAIRSNumber.Text) Then
                MessageBox.Show("AIRS number is not valid", "Invalid AIRS number", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim airsNumberDeleting As New ApbFacilityId(txtNewAIRSNumber.Text)

            If DAL.FacilityHasBeenApproved(airsNumberDeleting) Then
                MessageBox.Show("Facility has already been approved.", "Can't delete", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If DAL.Finance.FacilityHasFeesData(airsNumberDeleting) Then
                MessageBox.Show("Facility has permit fees data. Unable to delete unless the fees data is removed first.", "Can't delete", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim result As DialogResult
            result = MessageBox.Show("Are you sure you want to completely remove this facility from the database? The data will not be recoverable.", "Confirm facility deletion",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
            If result = DialogResult.No Then
                Return
            End If

            If DAL.DeleteFacility(airsNumberDeleting) Then
                MessageBox.Show("Facility removed from the database", "Gone", MessageBoxButtons.OK)
            Else
                MessageBox.Show("There was an error when attempting to remove the facility from the database." & vbNewLine & vbNewLine & "Facility has not been removed.", "Error", MessageBoxButtons.OK)
            End If

            LoadPendingFacilities()

            ClearNewFacility()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveSSCPApproval_Click(sender As Object, e As EventArgs) Handles btnSaveSSCPApproval.Click
        Try
            If chbSSCPSignOff.Checked = False Then
                MsgBox("Please check the SSCP Approve box.", MsgBoxStyle.Information, Me.Text)
                Return
            End If

            Dim SQL As String = "Update APBSupplamentalData set " &
                "numApprovingSSCP = @numApprovingSSCP " &
                ", datApproveDateSSCP = @datApproveDateSSCP " &
                ", strCommentSSCP = @strCommentSSCP " &
                "where strAIRSnumber = @strAIRSnumber "

            Dim p3 As SqlParameter() = {
                New SqlParameter("@numApprovingSSCP", CurrentUser.UserID),
                New SqlParameter("@datApproveDateSSCP", DTPSSCPApproveDate.Text),
                New SqlParameter("@strCommentSSCP", txtSSCPComments.Text),
                New SqlParameter("@strAIRSnumber", "0413" & txtNewAIRSNumber.Text)
            }

            DB.RunCommand(SQL, p3)

            LoadPendingFacilities()
            txtSSCPApprover.Text = CurrentUser.AlphaName
            MsgBox("Approval Saved.", MsgBoxStyle.Information, Me.Text)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveSSPPApproval_Click(sender As Object, e As EventArgs) Handles btnSaveSSPPApproval.Click
        Try
            If chbSSPPSignOff.Checked = False Then
                MsgBox("Please check the SSPP Approve box.", MsgBoxStyle.Information, Me.Text)
                Return
            End If

            Dim SQL As String = "Update APBSupplamentalData set " &
                "numApprovingSSPP = @numApprovingSSPP " &
                ", datApproveDateSSPP = @datApproveDateSSPP " &
                ", strCommentSSPP = @strCommentSSPP " &
                "where strAIRSnumber = @strAIRSnumber "

            Dim p3 As SqlParameter() = {
                New SqlParameter("@numApprovingSSPP", CurrentUser.UserID),
                New SqlParameter("@datApproveDateSSPP", DTPSSCPApproveDate.Text),
                New SqlParameter("@strCommentSSPP", txtSSCPComments.Text),
                New SqlParameter("@strAIRSnumber", "0413" & txtNewAIRSNumber.Text)
            }

            DB.RunCommand(SQL, p3)

            LoadPendingFacilities()
            txtSSPPApprover.Text = CurrentUser.AlphaName
            MsgBox("Approval Saved.", MsgBoxStyle.Information, Me.Text)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnValidateFacility_Click(sender As Object, e As EventArgs) Handles btnValidateFacility.Click
        Try
            If Not ApbFacilityId.IsValidAirsNumberFormat(txtNewAIRSNumber.Text) Then
                dgvValidatingAIRS.DataSource = Nothing
                lblValidationCount.Text = ""
                Return
            End If

            Dim FacilityName As String = txtNewFacilityName.Text
            Dim FacilityAddress As String = txtStreetAddress.Text

            Dim SQL As String = "select
                strFacilityName,
                right(strAIRSNumber, 8) as AIRSNumber,
                strFacilityStreet1,
                strFacilityCity,
                strFacilityZipCode
            from APBFACILITYINFORMATION
            where strFacilityName Like @name or strFacilityStreet1 like @address
            union
            select
                strFacilityName,
                right(strAIRSNumber, 8),
                strFacilityStreet1,
                strFacilityCity,
                strFacilityZipCode
            from HB_APBFACILITYINFORMATION
            where strFacilityName Like @name or strFacilityStreet1 like @address
            union
            select
                strFacilityname,
                right(strAIRSNumber, 8),
                strFacilityStreet1,
                strFacilityCity,
                strFacilityZipCode
            from SSPPAPPLICATIONDATA d
                inner join SSPPAPPLICATIONMASTER m
                    on d.strApplicationNumber = m.strApplicationNumber
            where strFacilityname like @name or strFacilityStreet1 like @address "

            Dim p As SqlParameter() = {
                New SqlParameter("@name", "%" & FacilityName & "%"),
                New SqlParameter("@address", "%" & FacilityAddress & "%")
            }

            dgvValidatingAIRS.DataSource = DB.GetDataTable(SQL, p)

            dgvValidatingAIRS.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke

            dgvValidatingAIRS.Columns("AIRSNumber").HeaderText = "AIRS"
            dgvValidatingAIRS.Columns("AIRSNumber").DisplayIndex = 0
            dgvValidatingAIRS.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvValidatingAIRS.Columns("strFacilityName").DisplayIndex = 1
            dgvValidatingAIRS.Columns("strFacilityStreet1").HeaderText = "Street Address"
            dgvValidatingAIRS.Columns("strFacilityStreet1").DisplayIndex = 2
            dgvValidatingAIRS.Columns("strFacilityCity").HeaderText = "City"
            dgvValidatingAIRS.Columns("strFacilityCity").DisplayIndex = 3
            dgvValidatingAIRS.Columns("strFacilityZipCode").HeaderText = "Zip Code"
            dgvValidatingAIRS.Columns("strFacilityZipCode").DisplayIndex = 4

            dgvValidatingAIRS.SanelyResizeColumns()

            lblValidationCount.Text = dgvValidatingAIRS.RowCount.ToString & " found"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ClearNewFacility()
        txtApplicationNumber.Text = ""
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
        mtbCDSNAICSCode.Clear()
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
        txtContactPhoneNumber.Clear()
        txtFacilityComments.Clear()

        txtNewAIRSNumber.Clear()
        txtNewFacilityName.Clear()
        txtStreetAddress.Clear()
        txtApprovialComments.Clear()
        chbSSCPSignOff.Checked = False
        txtSSCPApprover.Clear()
        DTPSSCPApproveDate.Value = Today
        txtSSCPComments.Clear()
        chbSSPPSignOff.Checked = False
        txtSSPPApprover.Clear()
        DTPSSPPApproveDate.Value = Today
        txtSSPPComments.Clear()
        dgvValidatingAIRS.DataSource = Nothing
        lblValidationCount.Text = ""

        btnEditFacilityData.Visible = False
        btnSaveNewFacility.Visible = True
        cboCounty.Enabled = True
        cboCounty.SelectedIndex = -1
    End Sub

    Private Sub btnEditFacilityData_Click(sender As Object, e As EventArgs) Handles btnEditFacilityData.Click
        EditFacility()
    End Sub

    Private Sub EditFacility()
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

            If Not ApbFacilityId.IsValidAirsNumberFormat(txtCDSAIRSNumber.Text) Then
                MsgBox("There is no AIRS # selected. You will need to select a facility from the Approval Tab first.",
                       MsgBoxStyle.Information, Me.Text)
                Return
            End If

            If Not DAL.NaicsCodeIsValid(mtbCDSNAICSCode.Text, False) Then
                MsgBox("The NAICS Code is not valid." &
                  "No Data saved.", MsgBoxStyle.Information, Me.Name)
                Return
            End If

            AIRSNumber = "0413" & txtCDSAIRSNumber.Text

            If txtCDSFacilityName.Text = "" Then
                FacilityName = "N/A"
            Else
                txtCDSFacilityName.Text = Facility.SanitizeFacilityNameForDb(txtCDSFacilityName.Text)
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
            If String.IsNullOrEmpty(txtContactPhoneNumber.Text) Then
                ContactPhoneNumber = txtContactPhoneNumber.Text
            Else
                ContactPhoneNumber = "0000000000"
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
                Comments = "Created with Facility Creator tool by " & CurrentUser.AlphaName & " on " & Format(Today, DateFormat) & vbCrLf
            End If

            If txtFacilityComments.Text.Contains("Created by Facility Creator by " & CurrentUser.AlphaName & " on " & Format(Today, DateFormat)) Then
            Else
                Comments = "Created with Facility Creator tool by " & CurrentUser.AlphaName & " on " & Format(Today, DateFormat) &
                               vbCrLf & txtFacilityComments.Text & vbCrLf
            End If
            If Not String.IsNullOrWhiteSpace(txtApplicationNumber.Text) And
                txtFacilityComments.Text.Contains(txtApplicationNumber.Text) = False Then
                Comments = Comments & "Pre-loaded with Application " & txtApplicationNumber.Text
            End If

            Dim SQL As String = "update APBFacilityInformation set " &
                "strFacilityName = @strFacilityName, " &
                "strFacilityStreet1 = @strFacilityStreet1, " &
                "strFacilityCity = @strFacilityCity, " &
                "strFacilityZipCode = @strFacilityZipCode, " &
                "strModifingPerson = @strModifingPerson, " &
                "datModifingdate = getdate(), " &
                "numfacilitylongitude = @numfacilitylongitude, " &
                "numFacilityLatitude = @numFacilityLatitude " &
                "where strAirsnumber = @strAirsnumber "

            Dim p As SqlParameter() = {
                New SqlParameter("@strFacilityName", FacilityName),
                New SqlParameter("@strFacilityStreet1", FacilityStreet),
                New SqlParameter("@strFacilityCity", FacilityCity),
                New SqlParameter("@strFacilityZipCode", FacilityZipCode),
                New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                New SqlParameter("@numfacilitylongitude", FacilityLongitude),
                New SqlParameter("@numFacilityLatitude", FacilityLatitude),
                New SqlParameter("@strAirsnumber", AIRSNumber)
            }

            DB.RunCommand(SQL, p)

            SQL = "Update APBHeaderData set " &
                "strOperationalStatus = @strOperationalStatus, " &
                "strClass = @strClass, " &
                "strAIRProgramCodes = @strAIRProgramCodes, " &
                "strSICCode = @strSICCode, " &
                "strNAICSCode = @strNAICSCode, " &
                "strPlantDescription = @strPlantDescription, " &
                "strComments = @strComments, " &
                "strModifingPerson = @strModifingPerson, " &
                "datModifingDate = getdate() " &
                "where strAIRSNumber = @strAIRSNumber "

            Dim p2 As SqlParameter() = {
                New SqlParameter("@strOperationalStatus", OperatingStatus),
                New SqlParameter("@strClass", Classification),
                New SqlParameter("@strAIRProgramCodes", AirProgramCode),
                New SqlParameter("@strSICCode", RealStringOrNothing(SICCode)),
                New SqlParameter("@strNAICSCode", RealStringOrNothing(NAICSCode)),
                New SqlParameter("@strPlantDescription", PlantDesc),
                New SqlParameter("@strComments", Comments),
                New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                New SqlParameter("@strAIRSNumber", AIRSNumber)
            }

            DB.RunCommand(SQL, p2)

            SQL = "Update APBSupplamentalData set " &
                "strDistrictOffice = @strDistrictOffice, " &
                "strModifingPerson = @strModifingPerson, " &
                "datModifingDate = getdate() " &
                "where strAIRSNumber = @strAIRSNumber "

            Dim p3 As SqlParameter() = {
                New SqlParameter("@strDistrictOffice", DistrictOffice),
                New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                New SqlParameter("@strAIRSNumber", AIRSNumber)
            }

            DB.RunCommand(SQL, p3)

            SQL = "update APBContactInformation set " &
                "strContactAddress1 = @strContactAddress1, " &
                "strContactCity = @strContactCity, " &
                "strContactState = @strContactState, " &
                "strContactZipCode = @strContactZipCode, " &
                "strContactFirstName = @strContactFirstName, " &
                "strContactLastName = @strContactLastName, " &
                "strContactPrefix = @strContactPrefix, " &
                "strContactSuffix = @strContactSuffix, " &
                "strContactTitle = @strContactTitle, " &
                "strContactPhoneNumber1 = @strContactPhoneNumber1, " &
                "strModifingPerson = @strModifingPerson, " &
                "datModifingDate = getdate() " &
                "where strAIRSNumber = @strAIRSNumber " &
                "and strKey = '30' "

            Dim p4 As SqlParameter() = {
                New SqlParameter("@strContactAddress1", MailingStreet),
                New SqlParameter("@strContactCity", MailingCity),
                New SqlParameter("@strContactState", MailingState),
                New SqlParameter("@strContactZipCode", MailingZipCode),
                New SqlParameter("@strContactFirstName", ContactFirstName),
                New SqlParameter("@strContactLastName", ContactLastName),
                New SqlParameter("@strContactPrefix", ContactPrefix),
                New SqlParameter("@strContactSuffix", ContactSuffix),
                New SqlParameter("@strContactTitle", ContactTitle),
                New SqlParameter("@strContactPhoneNumber1", ContactPhoneNumber),
                New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                New SqlParameter("@strAIRSNumber", AIRSNumber)
            }

            DB.RunCommand(SQL, p4)

            MsgBox("Facility has been updated", MsgBoxStyle.Information, Me.Text)

            If TCFacilityTools.TabPages.Contains(TPApproveNewFacility) Then
                LoadPendingFacilities()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbFilterNewFacilities_CheckedChanged(sender As Object, e As EventArgs) Handles chbFilterNewFacilities.CheckedChanged
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnFilterNewFacilities_Click(sender As Object, e As EventArgs) Handles btnFilterNewFacilities.Click
        LoadPendingFacilities()
    End Sub

    Private Sub DeleteAirsNumber_Click(sender As Object, e As EventArgs) Handles DeleteAirsNumber.Click
        Try
            If Not ApbFacilityId.IsValidAirsNumberFormat(AirsNumberToDelete.Text) Then
                MessageBox.Show("AIRS number is not valid", "Invalid AIRS number", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim airsNumberDeleting As New ApbFacilityId(AirsNumberToDelete.Text)

            If Not DAL.FacilityHasBeenApproved(airsNumberDeleting) Then
                MessageBox.Show("Facility has not been approved yet. Remove facility using the ""Approve New Facilities"" tab.", "Can't delete", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If DAL.Finance.FacilityHasFeesData(airsNumberDeleting) Then
                MessageBox.Show("Facility has permit fees data. Unable to delete unless the fees data is removed first.", "Can't delete", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim result As DialogResult
            result = MessageBox.Show("Are you sure you want to completely remove this facility from the database? The data will not be recoverable.", "Confirm facility deletion",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
            If result = DialogResult.No Then
                Return
            End If

            If DAL.DeleteFacility(airsNumberDeleting) Then
                MessageBox.Show("Facility removed from the database", "Gone", MessageBoxButtons.OK)
            Else
                MessageBox.Show("There was an error when attempting to remove the facility from the database." & vbNewLine & vbNewLine & "Facility has not been removed.", "Error", MessageBoxButtons.OK)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub AirsNumberToDelete_TextChanged(sender As Object, e As EventArgs) Handles AirsNumberToDelete.TextChanged
        FacilityLongDisplay.Text = ""
        If ApbFacilityId.IsValidAirsNumberFormat(AirsNumberToDelete.Text) Then
            Dim fac As Facility = DAL.GetFacility(New ApbFacilityId(AirsNumberToDelete.Text))
            If fac IsNot Nothing Then
                fac.HeaderData = DAL.GetFacilityHeaderData(New ApbFacilityId(AirsNumberToDelete.Text))
                If fac.HeaderData IsNot Nothing Then FacilityLongDisplay.Text = fac.LongDisplay
            End If
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearNewFacility()
    End Sub

    Private Sub txtApplicationNumber_Enter(sender As Object, e As EventArgs) Handles txtApplicationNumber.Enter
        AcceptButton = btnPreLoadNewFacility
    End Sub

    Private Sub txtApplicationNumber_Leave(sender As Object, e As EventArgs) Handles txtApplicationNumber.Leave
        AcceptButton = Nothing
    End Sub

End Class