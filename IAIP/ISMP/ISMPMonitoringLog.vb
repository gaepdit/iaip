Imports System.Data.SqlClient

Public Class ISMPMonitoringLog

    Private Sub ISMPMonitoringLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            DTPStartDate.Value = Today.AddDays(-30)
            DTPEndDate.Value = Today

            chbReviewingEngineer.Text = CurrentUser.AlphaName
            chbWitnessingEngineer.Text = CurrentUser.AlphaName

            LoadComboBoxes()
            LoadDataSet()

            SCMonitoringLog.SanelySetSplitterDistance(500)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadComboBoxes()
        Try

            Dim query As String = "select " &
            "concat(strLastName, ', ' ,strFirstName) as UserName, " &
            "numUserID " &
            "from epduserprofiles, " &
            "(select distinct(strStaffResponsible) as NotificationStaff " &
            "from ISMPTestNotification) Notification " &
            "where convert(varchar(3),EPDUserProfiles.numUserId) = Notification.NotificationStaff " &
            "Union " &
            "Select " &
            "concat(strLastName, ', ' ,strFirstName) as UserName, " &
            "numUserID " &
            "from EPDUserProfiles, " &
            "(select distinct(strReviewingEngineer) " &
            "from ISMPReportInformation) ISMPUsers " &
            "where convert(varchar(3),EPDUserProfiles.numUserID) = ISMPUsers.strReviewingEngineer  "

            With clbEngineer
                .DataSource = DB.GetDataTable(query)
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
            End With

            With clbWitnessingStaff
                .DataSource = CType(clbEngineer.DataSource, DataTable).Copy
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadDataSet()
        Dim SQLWhere As String
        Dim query As String = ""

        Try

            If chbTestReports.Checked Then
                SQLWhere = ""

                query = "select " &
                "ISMPMaster.strReferenceNumber,  " &
                "SUBSTRING(ISMPMaster.strAIRSNumber, 5,8) as strAIRSNumber,  " &
                "APBFacilityInformation.strFacilityName,  " &
                "APBFacilityInformation.strFacilityCity,  " &
                "LookupCountyInformation.strCountyName,  " &
                "ISMPReportInformation.strEmissionSource,  " &
                "LookUpPollutants.strPollutantDescription,  " &
                "ISMPReportType.strReportType,  " &
                "ISMPDocumentType.strDocumentType,  " &
                "concat(strLastName, ', ' , strFirstName) as StaffResponsible,  " &
                " case " &
                " when DATTESTDATESTART = DATTESTDATEEND " &
                " then format(DATTESTDATESTART, 'dd-MMM-yyyy') " &
                " else concat(format(DATTESTDATESTART, 'dd-MMM-yyyy'), ' — ', format(DATTESTDATEEND, 'dd-MMM-yyyy')) " &
                " end as [TestDateRange], " &
                "ISMPREportInformation.datReceivedDate,  " &
                "DATEDIFF(day, ISMPREportINformation.datTestDateEnd, " &
                "ISMPReportInformation.datReceivedDate) as daysToReceived, " &
                "case " &
                "when strClosed <> 'True' then ROUND( DATEDIFF(day, ISMPREportInformation.datReceivedDate, GETDATE() ), 0) " &
                "else DATEDIFF(day, ISMPReportInformation.datCompleteDate, ISMPReportInformation.datReceivedDate) " &
                "End daysInAPB, " &
                "case " &
                "when ISMPReportInformation.datCompleteDate = '04-Jul-1776' then Null " &
                "else ISMPReportInformation.datCompleteDate " &
                "end datCompleteDate, " &
                "ISMPREportInformation.strClosed,  " &
                "LookUpISMPComplianceStatus.strComplianceStatus,  " &
                "ISMPREportInformation.mmoCommentArea,  " &
                "LookUpEPDUnits.strUnitDesc, " &
                "ISMPTestLogLink.strTestLogNumber, " &
                "strPreComplianceStatus, " &
                "LOOKUPTESTINGFIRMS.STRTESTINGFIRM as [Testing Firm] " &
                "FROM ISMPMaster " &
                " INNER JOIN APBFacilityInformation  " &
                " ON ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSnumber " &
                " INNER JOIN LookUpCountyInformation " &
                " ON SUBSTRING(ISMPMaster.strAIRSNumber, 5, 3) = LookupCountyInformation.strCountyCode  " &
                " INNER JOIN ISMPReportInformation " &
                " ON ISMPMaster.strReferenceNumber = ISMPREportInformation.strReferenceNumber  " &
                " LEFT JOIN LookUpPollutants " &
                " ON ISMPReportInformation.strpollutant = LookUpPollutants.strPollutantCode " &
                " INNER JOIN ISMPReportType  " &
                " ON ISMPREportINformation.strReportType = ISMPREportType.strKey " &
                " LEFT JOIN ISMPDocumentType " &
                " ON ISMPREportINformation.strDocumentType = ISMPDocumentType.strKey " &
                " LEFT JOIN EPDUSerProfiles  " &
                " ON ISMPREportInformation.strReviewingEngineer = EPDUserProfiles.numUserID " &
                " LEFT JOIN LookUpISMPComplianceStatus  " &
                " ON ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey " &
                " LEFT JOIN ISMPTestLogLink " &
                " ON ISMPMaster.strReferenceNumber = ISMPTestLogLink.strReferenceNumber " &
                " LEFT JOIN ISMPWitnessingEng " &
                " ON ISMPMaster.strReferenceNumber = ISMPWitnessingEng.strReferenceNumber " &
                " INNER JOIN LookUpEPDUnits " &
                " ON EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode " &
                " left join LOOKUPTESTINGFIRMS on ISMPREPORTINFORMATION.STRTESTINGFIRM = LOOKUPTESTINGFIRMS.STRTESTINGFIRMKEY " &
                "WHERE (strDelete is Null or strDelete <> 'DELETE') "

                If chbReviewingEngineer.Checked Then
                    SQLWhere = SQLWhere & " and ( "
                    If clbEngineer.CheckedItems.Count > 0 Then
                        For x As Integer = 0 To clbEngineer.Items.Count - 1
                            If clbEngineer.GetItemChecked(x) Then
                                clbEngineer.SelectedIndex = x
                                SQLWhere = SQLWhere & " strReviewingEngineer = '" & clbEngineer.SelectedValue & "' Or "
                            End If
                        Next
                    End If
                    SQLWhere = SQLWhere & " strReviewingEngineer = '" & CurrentUser.UserID & "' ) "
                Else
                    If clbEngineer.CheckedItems.Count > 0 Then
                        SQLWhere = SQLWhere & " and ( "
                        For x As Integer = 0 To clbEngineer.Items.Count - 1
                            If clbEngineer.GetItemChecked(x) Then
                                clbEngineer.SelectedIndex = x
                                SQLWhere = SQLWhere & " strReviewingEngineer = '" & clbEngineer.SelectedValue & "' Or "
                            End If
                        Next
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3))
                        SQLWhere = SQLWhere & " ) "
                    End If
                End If

                If chbWitnessingEngineer.Checked Then
                    SQLWhere = SQLWhere & " and ( "
                    If clbWitnessingStaff.CheckedItems.Count > 0 Then
                        For x As Integer = 0 To clbWitnessingStaff.Items.Count - 1
                            If clbWitnessingStaff.GetItemChecked(x) Then
                                clbWitnessingStaff.SelectedIndex = x
                                SQLWhere = SQLWhere & " ISMPReportInformation.strWitnessingengineer = '" & clbWitnessingStaff.SelectedValue & "' Or " &
                                                            " ISMPWitnessingEng.strWitnessingEngineer = '" & clbWitnessingStaff.SelectedValue & "' or "
                            End If
                        Next
                    End If
                    SQLWhere = SQLWhere & " ISMPReportInformation.strWitnessingengineer = '" & CurrentUser.UserID & "' or strWitnessingengineer2 = '" & CurrentUser.UserID & "' ) "
                Else
                    If clbWitnessingStaff.CheckedItems.Count > 0 Then
                        SQLWhere = SQLWhere & " and ( "
                        For x As Integer = 0 To clbWitnessingStaff.Items.Count - 1
                            If clbWitnessingStaff.GetItemChecked(x) Then
                                clbWitnessingStaff.SelectedIndex = x
                                SQLWhere = SQLWhere & "  ISMPReportInformation.strWitnessingengineer = '" & clbWitnessingStaff.SelectedValue & "' Or " &
                                                            " ISMPWitnessingEng.strWitnessingEngineer = '" & clbWitnessingStaff.SelectedValue & "' or "
                            End If
                        Next
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3))
                        SQLWhere = SQLWhere & " ) "
                    End If
                End If

                If chbOpen.Checked AndAlso Not chbClosed.Checked Then
                    SQLWhere = SQLWhere & " and strClosed = 'False' "
                End If
                If Not chbOpen.Checked AndAlso chbClosed.Checked Then
                    SQLWhere = SQLWhere & " and strClosed = 'True' "
                End If

                If chbAll.Checked Then

                Else
                    If chbUnassigned.Checked OrElse
                        chbOneStackTwoRun.Checked OrElse
                        chbOneStackThreeRun.Checked OrElse
                        chbOneStackFourRun.Checked OrElse
                        chbTwoStackStandard.Checked OrElse
                        chbTwoStackDRE.Checked OrElse
                        chbLoadingRack.Checked OrElse
                        chbPondTreatment.Checked OrElse
                        chbGasConcentration.Checked OrElse
                        chbFlare.Checked OrElse
                        chbRata.Checked OrElse
                        chbMemorandumStandard.Checked OrElse
                        chbMemorandumToFile.Checked OrElse
                        chbMethod9Single.Checked OrElse
                        chbMethod9Multi.Checked OrElse
                        chbMethod22.Checked OrElse
                        chbPTE.Checked Then

                        SQLWhere = SQLWhere & " and ( "

                        If chbUnassigned.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '001' Or "
                        End If
                        If chbOneStackTwoRun.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '002' Or "
                        End If
                        If chbOneStackThreeRun.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '003' Or "
                        End If
                        If chbOneStackFourRun.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '004' Or "
                        End If
                        If chbTwoStackStandard.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '005' Or "
                        End If
                        If chbTwoStackDRE.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '006' Or "
                        End If
                        If chbLoadingRack.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '007' Or "
                        End If
                        If chbPondTreatment.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '008' Or "
                        End If
                        If chbGasConcentration.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '009' Or "
                        End If
                        If chbFlare.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '010' Or "
                        End If
                        If chbRata.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '011' Or "
                        End If
                        If chbMemorandumStandard.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '012' Or "
                        End If
                        If chbMemorandumToFile.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '013' Or "
                        End If
                        If chbMethod9Single.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '016' Or "
                        End If
                        If chbMethod9Multi.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '014' Or "
                        End If
                        If chbMethod22.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '015' Or "
                        End If
                        If chbPTE.Checked Then
                            SQLWhere = SQLWhere & " ISMPDocumentType.strKey = '018' Or "
                        End If
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3)) & " ) "
                    End If
                End If

                If chbMonitorCertification.Checked OrElse
                    chbPEMSDevelopment.Checked OrElse
                    chbRATAandCEMS.Checked OrElse
                    chbSourceTest.Checked OrElse
                    chbOther.Checked Then

                    SQLWhere = SQLWhere & " and ( "

                    If chbMonitorCertification.Checked Then
                        SQLWhere = SQLWhere & " ISMPReportInformation.strReportType = '001' Or "
                    End If
                    If chbPEMSDevelopment.Checked Then
                        SQLWhere = SQLWhere & " ISMPReportInformation.strReportType = '002' Or "
                    End If
                    If chbRATAandCEMS.Checked Then
                        SQLWhere = SQLWhere & " ISMPReportInformation.strReportType = '003' Or "
                    End If
                    If chbSourceTest.Checked Then
                        SQLWhere = SQLWhere & " ISMPReportInformation.strReportType = '004' Or "
                    End If
                    If chbOther.Checked Then
                        SQLWhere = SQLWhere & " ISMPReportInformation.strReportType = '005' Or "
                    End If
                    SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3)) & " ) "

                End If

                If chbComplianceStatus1.Checked OrElse chbComplianceStatus2.Checked OrElse
                    chbComplianceStatus3.Checked OrElse chbComplianceStatus4.Checked OrElse
                    chbComplianceStatus5.Checked Then

                    SQLWhere = SQLWhere & " and ( "

                    If chbComplianceStatus1.Checked Then
                        SQLWhere = SQLWhere & " ISMPReportInformation.strComplianceStatus = '01' Or "
                    End If
                    If chbComplianceStatus2.Checked Then
                        SQLWhere = SQLWhere & " ISMPReportInformation.strComplianceStatus = '02' Or "
                    End If
                    If chbComplianceStatus3.Checked Then
                        SQLWhere = SQLWhere & " ISMPReportInformation.strComplianceStatus = '03' Or "
                    End If
                    If chbComplianceStatus4.Checked Then
                        SQLWhere = SQLWhere & " ISMPReportInformation.strComplianceStatus = '04' Or "
                    End If
                    If chbComplianceStatus5.Checked Then
                        SQLWhere = SQLWhere & " ISMPReportInformation.strComplianceStatus = '05' Or "
                    End If
                    SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3)) & " ) "
                End If

                If rdbNA.Checked Then

                Else
                    If rdbFacilityDateReceived.Checked Then
                        SQLWhere = SQLWhere & "AND datReceivedDate between '" & DTPStartDate.Text & "' and '" & DTPEndDate.Text & "' "
                    End If
                    If rdbFacilityDateTested.Checked Then
                        SQLWhere = SQLWhere & "AND (datTestDateStart <= '" & DTPEndDate.Text & "' AND DATTESTDATEEND >= '" & DTPStartDate.Text & "') "
                    End If
                    If rdbFacilityDateCompleted.Checked Then
                        SQLWhere = SQLWhere & "And datCompleteDate between '" & DTPStartDate.Text & "' and '" & DTPEndDate.Text & "' "
                    End If
                End If

                If txtAIRSNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPMaster.strAIRSnumber like '%" & SqlQuote(txtAIRSNumberFilter.Text) & "%' "
                End If
                If txtFacilityNameFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and APBFacilityInformation.strFacilityName like '%" & SqlQuote(txtFacilityNameFilter.Text) & "%' "
                End If
                If txtReferenceNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPMaster.strReferenceNumber like '%" & SqlQuote(txtReferenceNumberFilter.Text) & "%' "
                End If
                If txtNotificationNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPTestLogLink.strTestLogNumber like '%" & SqlQuote(txtNotificationNumberFilter.Text) & "%' "
                End If
                If txtCityFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and APBFacilityInformation.strFacilityCity like '%" & SqlQuote(txtCityFilter.Text) & "%' "
                End If
                If txtCountyFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and LookUpCountyInformation.strCountyName like '%" & SqlQuote(txtCountyFilter.Text) & "%' "
                End If
                If txtEmissionSourceTestedFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPReportInformation.strEmissionSource " &
                                               "like '%" & SqlQuote(txtEmissionSourceTestedFilter.Text) & "%' "
                End If
                If txtCommentFieldFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPReportInformation.mmoCommentArea " &
                                               "like '%" & SqlQuote(txtCommentFieldFilter.Text) & "%' "
                End If

                If txtPollutantFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and LookUpPollutants.strPollutantDescription " &
                                               "like '%" & SqlQuote(txtPollutantFilter.Text) & "%' "
                End If

                If txtTestingFirm.Text <> "" Then
                    SQLWhere = SQLWhere & "and LOOKUPTESTINGFIRMS.STRTESTINGFIRM " &
                        "like '%" & SqlQuote(txtTestingFirm.Text) & "%' "
                End If

                query = query & SQLWhere

                dgvTestReportViewer.DataSource = DB.GetDataTable(query)

                dgvTestReportViewer.Columns("strReferenceNumber").HeaderText = "Reference #"
                dgvTestReportViewer.Columns("strReferenceNumber").DisplayIndex = 0
                dgvTestReportViewer.Columns("strAIRSNumber").HeaderText = "AIRS #"
                dgvTestReportViewer.Columns("strAIRSNumber").DisplayIndex = 1
                dgvTestReportViewer.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvTestReportViewer.Columns("strFacilityName").DisplayIndex = 2
                dgvTestReportViewer.Columns("strFacilityCity").HeaderText = "City"
                dgvTestReportViewer.Columns("strFacilityCity").DisplayIndex = 3
                dgvTestReportViewer.Columns("strCountyName").HeaderText = "County"
                dgvTestReportViewer.Columns("strCountyName").DisplayIndex = 4
                dgvTestReportViewer.Columns("strEmissionSource").HeaderText = "Unit Tested"
                dgvTestReportViewer.Columns("strEmissionSource").DisplayIndex = 5
                dgvTestReportViewer.Columns("strPollutantDescription").HeaderText = "Pollutant"
                dgvTestReportViewer.Columns("strPollutantDescription").DisplayIndex = 6
                dgvTestReportViewer.Columns("strReportType").HeaderText = "Report Type"
                dgvTestReportViewer.Columns("strReportType").DisplayIndex = 7
                dgvTestReportViewer.Columns("strDocumentType").HeaderText = "Document Type"
                dgvTestReportViewer.Columns("strDocumentType").DisplayIndex = 8
                dgvTestReportViewer.Columns("StaffResponsible").HeaderText = "Staff Responsible"
                dgvTestReportViewer.Columns("StaffResponsible").DisplayIndex = 9
                dgvTestReportViewer.Columns("TestDateRange").HeaderText = "Test Dates"
                dgvTestReportViewer.Columns("TestDateRange").DisplayIndex = 10
                dgvTestReportViewer.Columns("TestDateRange").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvTestReportViewer.Columns("daysToReceived").HeaderText = "Days From Test to APB"
                dgvTestReportViewer.Columns("daysToReceived").DisplayIndex = 11
                dgvTestReportViewer.Columns("datReceivedDate").HeaderText = "Received Date"
                dgvTestReportViewer.Columns("datReceivedDate").DisplayIndex = 12
                dgvTestReportViewer.Columns("datReceivedDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvTestReportViewer.Columns("daysinAPB").HeaderText = "Days in APB"
                dgvTestReportViewer.Columns("daysinAPB").DisplayIndex = 13
                dgvTestReportViewer.Columns("datCompleteDate").HeaderText = "Report Completed"
                dgvTestReportViewer.Columns("datCompleteDate").DisplayIndex = 14
                dgvTestReportViewer.Columns("datCompleteDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvTestReportViewer.Columns("strClosed").HeaderText = "Open/Closed"
                dgvTestReportViewer.Columns("strClosed").DisplayIndex = 15
                dgvTestReportViewer.Columns("strComplianceStatus").HeaderText = "Compliance Status"
                dgvTestReportViewer.Columns("strComplianceStatus").DisplayIndex = 16
                dgvTestReportViewer.Columns("mmoCommentArea").HeaderText = "Comments"
                dgvTestReportViewer.Columns("mmoCommentArea").DisplayIndex = 17
                dgvTestReportViewer.Columns("strUnitDesc").HeaderText = "User Unit"
                dgvTestReportViewer.Columns("strUnitDesc").DisplayIndex = 18
                dgvTestReportViewer.Columns("strTestLogNumber").HeaderText = "Test Log Number"
                dgvTestReportViewer.Columns("strTestLogNumber").DisplayIndex = 19
                dgvTestReportViewer.Columns("strPreComplianceStatus").HeaderText = "PreCompliance Status"
                dgvTestReportViewer.Columns("strPreComplianceStatus").DisplayIndex = 20

                dgvTestReportViewer.SanelyResizeColumns()
                txtReportCount.Text = dgvTestReportViewer.RowCount.ToString()

                LoadComplianceColor()

            End If  'Test Reports

            If Me.chbNotifications.Checked Then
                SQLWhere = ""
                query = "select distinct " &
               "SUBSTRING(ISMPTEstNotification.strAIRSNumber, 5,8) as AIRSNumber, " &
               "APBFacilityInformation.strFacilityName,  " &
               "strFacilityCity,  " &
               "strCountyName,  " &
               "convert(int, ISMPTestNotification.strTestLogNumber) as strTestLogNumber,    " &
               "strEmissionUnit,  datProposedStartDate,    " &
               "concat(strLastName, ', ' , strFirstName) as StaffResponsible,   " &
               "strUnitDesc, " &
               "ISMPTestNotification.strcomments,  " &
               "strReferenceNumber " &
               "from ISMPTestNotification " &
               " LEFT JOIN ISMPTestLogLink  " &
               "ON ISMPTestNotification.strTestLogNumber = ISMPTestLogLink.strTestLognumber " &
               " LEFT JOIN EPDUserProfiles " &
               "ON strStaffResponsible = EPDUserProfiles.numUserID " &
               " LEFT JOIN APBFacilityINformation  " &
               "ON ISMPTestNotification.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
               " LEFT JOIN LookUpCountyInformation  " &
               "ON SUBSTRING(ISMPTestNotification.strAIRSNumber, 5, 3) = LookUpCountyInformation.strcountycode " &
               " LEFT JOIN LookUpEPDUnits  " &
               "ON EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode " &
               " where 1=1 "

                If chbReviewingEngineer.Checked Then
                    SQLWhere = SQLWhere & " and ( "
                    If clbEngineer.CheckedItems.Count > 0 Then
                        For x As Integer = 0 To clbEngineer.Items.Count - 1
                            If clbEngineer.GetItemChecked(x) Then
                                clbEngineer.SelectedIndex = x
                                SQLWhere = SQLWhere & " strStaffResponsible = '" & clbEngineer.SelectedValue & "' Or "
                            End If
                        Next
                    End If
                    SQLWhere = SQLWhere & " strStaffResponsible = '" & CurrentUser.UserID & "' ) "
                Else
                    If clbEngineer.CheckedItems.Count > 0 Then
                        SQLWhere = SQLWhere & " and ( "
                        For x As Integer = 0 To clbEngineer.Items.Count - 1
                            If clbEngineer.GetItemChecked(x) Then
                                clbEngineer.SelectedIndex = x
                                SQLWhere = SQLWhere & " strStaffResponsible = '" & clbEngineer.SelectedValue & "' Or "
                            End If
                        Next
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3))
                        SQLWhere = SQLWhere & " ) "
                    End If
                End If

                If chbOpen.Checked Then
                    SQLWhere = SQLWhere & " and datproposedstartdate > DATEADD(day, -90, GETDATE()) "
                End If
                If txtAIRSNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPTestNotification.strAIRSnumber like '%" & SqlQuote(txtAIRSNumberFilter.Text) & "%' "
                End If
                If txtFacilityNameFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and APBFacilityInformation.strFacilityName like '%" & SqlQuote(txtFacilityNameFilter.Text) & "%' "
                End If
                If txtReferenceNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPTestLogLink.strReferenceNumber like '%" & SqlQuote(txtReferenceNumberFilter.Text) & "%' "
                End If
                If txtNotificationNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPTestNotification.strTestLogNumber like '%" & SqlQuote(txtNotificationNumberFilter.Text) & "%' "
                End If
                If txtCityFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and APBFacilityInformation.strFacilityCity like '%" & SqlQuote(txtCityFilter.Text) & "%' "
                End If
                If txtCountyFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and LookUpCountyInformation.strCountyName like '%" & SqlQuote(txtCountyFilter.Text) & "%' "
                End If
                If txtEmissionSourceTestedFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPTestNotification.strEmissionUnit " &
                                               "like '%" & SqlQuote(txtEmissionSourceTestedFilter.Text) & "%' "
                End If
                If txtCommentFieldFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPTestNotification.strComments " &
                                               "like '%" & SqlQuote(txtCommentFieldFilter.Text) & "%' "
                End If
                If txtPollutantFilter.Text <> "" Then

                End If
                If chbNotificationLinked.Checked Then
                    SQLWhere = SQLWhere & " and strReferenceNumber is Not Null "
                End If
                If chbNotificationUnlinked.Checked Then
                    SQLWhere = SQLWhere & " and strReferenceNumber is Null "
                End If

                query = query & SQLWhere

                dgvNotificationLog.DataSource = DB.GetDataTable(query)

                dgvNotificationLog.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvNotificationLog.Columns("AIRSNumber").DisplayIndex = 2
                dgvNotificationLog.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvNotificationLog.Columns("strFacilityName").DisplayIndex = 3
                dgvNotificationLog.Columns("strFacilityCity").HeaderText = "City"
                dgvNotificationLog.Columns("strFacilityCity").DisplayIndex = 4
                dgvNotificationLog.Columns("strCountyName").HeaderText = "County"
                dgvNotificationLog.Columns("strCountyName").DisplayIndex = 5
                dgvNotificationLog.Columns("strTestLogNumber").HeaderText = "Test Log #"
                dgvNotificationLog.Columns("strTestLogNumber").DisplayIndex = 0
                dgvNotificationLog.Columns("strEmissionUnit").HeaderText = "Unit Tested"
                dgvNotificationLog.Columns("strEmissionUnit").DisplayIndex = 6
                dgvNotificationLog.Columns("datProposedStartDate").HeaderText = "Purposed Test Date"
                dgvNotificationLog.Columns("datProposedStartDate").DisplayIndex = 8
                dgvNotificationLog.Columns("datProposedStartDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvNotificationLog.Columns("staffResponsible").HeaderText = "Responsible Staff"
                dgvNotificationLog.Columns("staffResponsible").DisplayIndex = 9
                dgvNotificationLog.Columns("strUnitDesc").HeaderText = "User Unit"
                dgvNotificationLog.Columns("strUnitDesc").DisplayIndex = 10
                dgvNotificationLog.Columns("strComments").HeaderText = "Comments"
                dgvNotificationLog.Columns("strComments").DisplayIndex = 7
                dgvNotificationLog.Columns("strReferenceNumber").HeaderText = "Reference #"
                dgvNotificationLog.Columns("strReferenceNumber").DisplayIndex = 1

                dgvNotificationLog.SanelyResizeColumns()
                txtNotificationCount.Text = dgvNotificationLog.RowCount
            End If

            If chbTestFirmComments.Checked Then
                query = ""
                SQLWhere = ""

                query = "select " &
               "numcommentsID, strTestingFirm, " &
               "SUBSTRING(ISMPTestFirmComments.strAIRSNumber, 5,8) as AIRSNumber, " &
               "strFacilityName, " &
               "ISMPTestFirmComments.strTestLogNumber, strReferenceNumber, " &
               "case " &
               "when strCommentType is Null then 'Unknown' " &
               "when strCommentType = '1' then 'Pre-test Comment' " &
               "when strCommentType = '2' then 'Day of test Comment' " &
               "when strCommentType = '3' then 'Post-test Comment' " &
               "else 'Unknown' " &
               "end CommentType, " &
               "concat(strLastName, ', ' , strFirstName) as StaffResponsible, " &
               "datCommentDate, strComment, " &
               "strFacilityCity, " &
               "LookUpCountyInformation.strCountyName " &
               "from ISMPTestFirmComments " &
               " INNER JOIN LookUpTestingFirms " &
               "ON ISMPTestFirmComments.strTestingFirmKey = LooKUpTestingFirms.strTestingFirmKey " &
               " LEFT JOIN APBFacilityInformation " &
               "ON ISMPTestFirmComments.strAIRSnumber = APBFacilityInformation.strAIRSNumber " &
               " LEFT JOIN EPDUserProfiles  " &
               "ON ISMPTestFirmComments.strStaffResponsible = EPDUserProfiles.numUserID " &
               " LEFT JOIN ISMPTestNotification " &
               "ON ismptestfirmcomments.strtestlognumber = ismptestnotification.strtestlognumber " &
               " LEFT JOIN LookUpCountyInformation " &
               "ON SUBSTRING(ISMPTestFirmComments.strAIRSNUmber, 5, 3)  = LookUpCountyInformation.strCountycode " &
               " where 1=1 "

                If chbReviewingEngineer.Checked Then
                    SQLWhere = SQLWhere & " and ( "
                    If clbEngineer.CheckedItems.Count > 0 Then
                        For x As Integer = 0 To clbEngineer.Items.Count - 1
                            If clbEngineer.GetItemChecked(x) Then
                                clbEngineer.SelectedIndex = x
                                SQLWhere = SQLWhere & " ISMPTestFirmComments.strStaffResponsible = '" & clbEngineer.SelectedValue & "' Or "
                            End If
                        Next
                    End If
                    SQLWhere = SQLWhere & " ISMPTestFirmComments.strStaffResponsible = '" & CurrentUser.UserID & "' ) "
                Else
                    If clbEngineer.CheckedItems.Count > 0 Then
                        SQLWhere = SQLWhere & " and ( "
                        For x As Integer = 0 To clbEngineer.Items.Count - 1
                            If clbEngineer.GetItemChecked(x) Then
                                clbEngineer.SelectedIndex = x
                                SQLWhere = SQLWhere & " ISMPTestFirmComments.strStaffResponsible = '" & clbEngineer.SelectedValue & "' Or "
                            End If
                        Next
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3))
                        SQLWhere = SQLWhere & " ) "
                    End If
                End If

                If txtAIRSNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPTestFirmComments.strAIRSnumber like '%" & SqlQuote(txtAIRSNumberFilter.Text) & "%' "
                End If
                If txtFacilityNameFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and APBFacilityInformation.strFacilityName like '%" & SqlQuote(txtFacilityNameFilter.Text) & "%' "
                End If
                If txtReferenceNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPTestFirmComments.strReferenceNumber like '%" & SqlQuote(txtReferenceNumberFilter.Text) & "%' "
                End If
                If txtNotificationNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPTestFirmComments.strTestLogNumber like '%" & SqlQuote(txtNotificationNumberFilter.Text) & "%' "
                End If
                If txtCityFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and APBFacilityInformation.strFacilityCity like '%" & SqlQuote(txtCityFilter.Text) & "%' "
                End If
                If txtCountyFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and LookUpCountyInformation.strCountyName like '%" & SqlQuote(txtCountyFilter.Text) & "%' "
                End If
                If txtEmissionSourceTestedFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPTestNotification.strEmissionUnit " &
                                               "like '%" & SqlQuote(txtEmissionSourceTestedFilter.Text) & "%' "
                End If
                If txtCommentFieldFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and ISMPTestFirmComments.strComments " &
                                               "like '%" & SqlQuote(txtCommentFieldFilter.Text) & "%' "
                End If

                If txtTestingFirm.Text <> "" Then
                    SQLWhere = SQLWhere & "and LookUpTestingFirms.strTestingFirm " &
                                                "like '%" & SqlQuote(txtTestingFirm.Text) & "%' "
                End If

                query = query & SQLWhere

                dgvTestFirmComments.DataSource = DB.GetDataTable(query)

                dgvTestFirmComments.Columns("numCommentsID").HeaderText = "Comment #"
                dgvTestFirmComments.Columns("numcommentsID").DisplayIndex = 0
                dgvTestFirmComments.Columns("strTestingFirm").HeaderText = "Testing Firm Name"
                dgvTestFirmComments.Columns("strTestingFirm").DisplayIndex = 1
                dgvTestFirmComments.Columns("AIRSNumber").HeaderText = "AIRS Number"
                dgvTestFirmComments.Columns("AIRSNumber").DisplayIndex = 2
                dgvTestFirmComments.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvTestFirmComments.Columns("strFacilityName").DisplayIndex = 3
                dgvTestFirmComments.Columns("strTestLogNumber").HeaderText = "Test Log #"
                dgvTestFirmComments.Columns("strTestLogNumber").DisplayIndex = 4
                dgvTestFirmComments.Columns("strReferenceNumber").HeaderText = "Reference #"
                dgvTestFirmComments.Columns("strReferenceNumber").DisplayIndex = 5
                dgvTestFirmComments.Columns("staffResponsible").HeaderText = "Responsible Staff"
                dgvTestFirmComments.Columns("staffResponsible").DisplayIndex = 6
                dgvTestFirmComments.Columns("datCommentDate").HeaderText = "Date of Comment"
                dgvTestFirmComments.Columns("datCommentDate").DisplayIndex = 7
                dgvTestFirmComments.Columns("datCommentDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvTestFirmComments.Columns("CommentType").HeaderText = "Comment Type"
                dgvTestFirmComments.Columns("CommentType").DisplayIndex = 8
                dgvTestFirmComments.Columns("strComment").HeaderText = "Comments"
                dgvTestFirmComments.Columns("strComment").DisplayIndex = 9
                dgvTestFirmComments.Columns("strFacilityCity").HeaderText = "Facility City"
                dgvTestFirmComments.Columns("strFacilityCity").DisplayIndex = 10
                dgvTestFirmComments.Columns("strCountyName").HeaderText = "County Name"
                dgvTestFirmComments.Columns("strCountyName").DisplayIndex = 11

                dgvTestFirmComments.SanelyResizeColumns()
                txtTestFirmCommentsCount.Text = dgvTestFirmComments.RowCount
            End If

        Catch ex As Exception
            ErrorReport(ex, query, "ISMPTestReportViewer.LoadDataSet")
        End Try
    End Sub

    Private Sub LoadTestLogData()
        Try

            If txtTestLogNumber.Text <> "" Then
                Dim query As String = "Select " &
                    "strTestLogNumber,  " &
                    "APBFacilityInformation.strFacilityName,  " &
                    "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as AIRSNumber,  " &
                    "strEmissionUnit,  " &
                    "APBFacilityInformation.strFacilityCity,  " &
                    "strCountyName,  " &
                    "datProposedStartDate  " &
                    " FROM ISMPTestNotification " &
                    " LEFT JOIN APBFacilityINformation " &
                    "ON concat('0413',ISMPTestNotification.strAIRSnumber) = APBFacilityInformation.strAIRSNumber " &
                    " LEFT JOIN LookUpCountyInformation  " &
                    "ON SUBSTRING(ISMPTestNotification.strAIRSNumber, 1, 3) = LookUpCountyInformation.strCountyCode " &
                    "WHERE strTestLogNumber = @log "

                Dim p As New SqlParameter("@log", txtTestLogNumber.Text)

                Dim dr As DataRow = DB.GetDataRow(query, p)

                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        txtNotificationFacilityName.Text = " "
                    Else
                        txtNotificationFacilityName.Text = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("AIRSNumber")) Then
                        txtNotificationAIRSNumber.Text = " "
                    Else
                        txtNotificationAIRSNumber.Text = dr.Item("AIRSNumber")
                    End If
                    If IsDBNull(dr.Item("strEmissionUnit")) Then
                        txtNotificationEmissionUnit.Text = " "
                    Else
                        txtNotificationEmissionUnit.Text = dr.Item("strEmissionUnit")
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        txtNotificationCity.Text = " "
                    Else
                        txtNotificationCity.Text = dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strCountyName")) Then
                        txtNotificationCounty.Text = " "
                    Else
                        txtNotificationCounty.Text = dr.Item("strCountyName")
                    End If
                    If IsDBNull(dr.Item("datProposedStartDate")) Then
                        txtNotificationTestStart.Text = " "
                    Else
                        txtNotificationTestStart.Text = dr.Item("datProposedStartDate")
                    End If
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub SelectTestReport()
        OpenFormTestReport(txtReferenceNumber.Text)
    End Sub
    Private Sub ResetOptions()
        Try

            rdbFacilityDateReceived.Checked = True
            DTPStartDate.Value = Today.AddDays(-30)
            DTPEndDate.Value = Today
            chbOpen.Checked = False
            chbClosed.Checked = False
            chbComplianceStatus1.Checked = False
            chbComplianceStatus2.Checked = False
            chbComplianceStatus3.Checked = False
            chbComplianceStatus4.Checked = False
            chbComplianceStatus5.Checked = False
            chbMonitorCertification.Checked = False
            chbPEMSDevelopment.Checked = False
            chbRATAandCEMS.Checked = False
            chbSourceTest.Checked = False
            txtReferenceNumber.Clear()
            txtFacilityName.Clear()
            txtAIRSNumber.Clear()
            txtTestType.Clear()
            txtFacilityCity.Clear()
            txtFacilityCounty.Clear()
            txtPollutant.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LLSelectReport_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LLSelectReport.LinkClicked
        SelectTestReport()
    End Sub
    Private Sub btnRunFilter_Click(sender As Object, e As EventArgs) Handles btnRunFilter.Click
        LoadDataSet()
    End Sub
    Private Sub MmiReset_Click(sender As Object, e As EventArgs) Handles mmiReset.Click
        ResetOptions()
    End Sub

    Private Sub tsbResize_Click(sender As Object, e As EventArgs) Handles tsbResize.Click
        Try
            SCMonitoringLog.ToggleSplitterDistance(235, 500)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbSelectTestLog_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSelectTestLog.LinkClicked
        Try
            OpenFormTestNotification(txtTestLogNumber.Text)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbOpenComments_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbOpenComments.LinkClicked
        Try

            Dim TestFirmComments As New ISMPTestFirmComments

            If TestFirmComments IsNot Nothing AndAlso Not TestFirmComments.IsDisposed Then
                TestFirmComments.txtAIRSNumber.Text = txtTestFirmAirsNumber.Text
                TestFirmComments.txtFacilityTested.Text = txtTestFirmFacilityName.Text
                TestFirmComments.cboTestingFirm.Text = txtTestFirmName.Text
                TestFirmComments.txtTestReportNumber.Text = txtTestFirmReferenceNumber.Text
                TestFirmComments.txtTestNotificationNumber.Text = txtTestFirmTestLogNumber.Text
                TestFirmComments.txtCommentID.Text = txtCommentNumber.Text

                TestFirmComments.Show()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub OpenFacilityLookupTool()
        Try
            Using facilityLookupDialog As New IAIPFacilityLookUpTool
                If facilityLookupDialog.ShowDialog() = DialogResult.OK AndAlso facilityLookupDialog.SelectedAirsNumber <> "" Then
                    txtAIRSNumberFilter.Text = facilityLookupDialog.SelectedAirsNumber
                End If
            End Using
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub tsbFacilitySearch_Click(sender As Object, e As EventArgs) Handles tsbFacilitySearch.Click
        OpenFacilityLookupTool()
    End Sub

    Private Sub tsbExportToExcel_Click(sender As Object, e As EventArgs) Handles tsbExportToExcel.Click
        ExportGridToExcel()
    End Sub

    Private Sub ExportGridToExcel()
        dgvTestReportViewer.ExportToExcel(Me)
    End Sub

    Private Sub LoadComplianceColor()
        Try
            For Each row As DataGridViewRow In dgvTestReportViewer.Rows
                If Not row.IsNewRow Then
                    If row.Cells(20).Value IsNot DBNull.Value AndAlso row.Cells(20).Value.ToString = "True" Then
                        row.DefaultCellStyle.BackColor = Color.Pink
                    End If
                    If row.Cells(16).Value IsNot DBNull.Value AndAlso
                        row.Cells(16).Value.ToString = "Not In Compliance" Then
                        row.DefaultCellStyle.BackColor = Color.Tomato
                    End If
                End If
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name, False)
        End Try
    End Sub

    Private Sub dgvTestReportViewer_Sorted(sender As Object, e As EventArgs) Handles dgvTestReportViewer.Sorted
        Try
            LoadComplianceColor()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mmiReports_Click(sender As Object, e As EventArgs) Handles mmiReports.Click
        Dim StaffReports As New ISMPStaffReports

        If StaffReports IsNot Nothing AndAlso Not StaffReports.IsDisposed Then
            StaffReports.Show()
        End If
    End Sub

    Private Sub tsbClear_Click(sender As Object, e As EventArgs) Handles tsbClear.Click
        ResetOptions()
    End Sub

    Private Sub ExportToExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToExcelToolStripMenuItem.Click
        ExportGridToExcel()
    End Sub

    Private Sub dgvTestReportViewer_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTestReportViewer.CellEnter
        If e.RowIndex = -1 OrElse e.RowIndex >= dgvTestReportViewer.RowCount Then Return

        txtReferenceNumber.Text = ToStringIfNotNull(dgvTestReportViewer(0, e.RowIndex).Value, "")
        txtAIRSNumber.Text = ToStringIfNotNull(dgvTestReportViewer(1, e.RowIndex).Value, "")
        txtFacilityName.Text = ToStringIfNotNull(dgvTestReportViewer(2, e.RowIndex).Value, "")
        txtTestType.Text = ToStringIfNotNull(dgvTestReportViewer(7, e.RowIndex).Value, "")
        txtFacilityCity.Text = ToStringIfNotNull(dgvTestReportViewer(3, e.RowIndex).Value, "")
        txtFacilityCounty.Text = ToStringIfNotNull(dgvTestReportViewer(4, e.RowIndex).Value, "")
        txtPollutant.Text = ToStringIfNotNull(dgvTestReportViewer(6, e.RowIndex).Value, "")
        txtTestLogNumber.Text = ToStringIfNotNull(dgvTestReportViewer(19, e.RowIndex).Value, "")

        LoadTestLogData()
    End Sub

    Private Sub dgvTestReportViewer_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvTestReportViewer.CellLinkActivated
        OpenFormTestReport(e.LinkValue.ToString)
    End Sub

    Private Sub dgvNotificationLog_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvNotificationLog.CellEnter
        If e.RowIndex = -1 OrElse e.RowIndex >= dgvNotificationLog.RowCount Then Return

        txtTestLogNumber.Text = ToStringIfNotNull(dgvNotificationLog(4, e.RowIndex).Value, "")
        txtNotificationFacilityName.Text = ToStringIfNotNull(dgvNotificationLog(1, e.RowIndex).Value, "")
        txtNotificationAIRSNumber.Text = ToStringIfNotNull(dgvNotificationLog(0, e.RowIndex).Value, "")
        txtNotificationEmissionUnit.Text = ToStringIfNotNull(dgvNotificationLog(5, e.RowIndex).Value, "")
        txtNotificationCity.Text = ToStringIfNotNull(dgvNotificationLog(2, e.RowIndex).Value, "")
        txtNotificationCounty.Text = ToStringIfNotNull(dgvNotificationLog(3, e.RowIndex).Value, "")
        txtNotificationTestStart.Text = ToStringIfNotNull(dgvNotificationLog(6, e.RowIndex).Value, "")

        If dgvNotificationLog(10, e.RowIndex).Value IsNot Nothing AndAlso dgvNotificationLog(10, e.RowIndex).Value IsNot DBNull.Value Then
            txtReferenceNumber.Text = dgvNotificationLog(10, e.RowIndex).Value
        End If
    End Sub

    Private Sub dgvNotificationLog_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvNotificationLog.CellLinkActivated
        OpenFormTestNotification(e.LinkValue.ToString)
    End Sub

    Private Sub dgvTestFirmComments_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTestFirmComments.CellEnter
        If e.RowIndex = -1 OrElse e.RowIndex >= dgvTestFirmComments.RowCount Then Return

        txtCommentNumber.Text = ToStringIfNotNull(dgvTestFirmComments(0, e.RowIndex).Value, "")
        txtTestFirmFacilityName.Text = ToStringIfNotNull(dgvTestFirmComments(3, e.RowIndex).Value, "")
        txtTestFirmAirsNumber.Text = ToStringIfNotNull(dgvTestFirmComments(2, e.RowIndex).Value, "")
        txtTestFirmName.Text = ToStringIfNotNull(dgvTestFirmComments(1, e.RowIndex).Value, "")
        txtTestFirmCommentType.Text = ToStringIfNotNull(dgvTestFirmComments(6, e.RowIndex).Value, "")
        txtTestFirmFacilityCity.Text = ToStringIfNotNull(dgvTestFirmComments(10, e.RowIndex).Value, "")
        txtTestFirmCounty.Text = ToStringIfNotNull(dgvTestFirmComments(11, e.RowIndex).Value, "")
        txtTestFirmReferenceNumber.Text = ToStringIfNotNull(dgvTestFirmComments(5, e.RowIndex).Value, "")
        txtTestFirmTestLogNumber.Text = ToStringIfNotNull(dgvTestFirmComments(4, e.RowIndex).Value, "")
    End Sub

    Private Sub dgvTestFirmComments_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvTestFirmComments.CellLinkActivated
        Dim rowIndex As Integer = dgvTestFirmComments.CurrentRow.Index

        Dim TestFirmComments As New ISMPTestFirmComments

        If TestFirmComments IsNot Nothing AndAlso Not TestFirmComments.IsDisposed Then
            TestFirmComments.txtAIRSNumber.Text = ToStringIfNotNull(dgvTestFirmComments(2, rowIndex).Value, "")
            TestFirmComments.txtFacilityTested.Text = ToStringIfNotNull(dgvTestFirmComments(3, rowIndex).Value, "")
            TestFirmComments.cboTestingFirm.Text = ToStringIfNotNull(dgvTestFirmComments(1, rowIndex).Value, "")
            TestFirmComments.txtTestReportNumber.Text = ToStringIfNotNull(dgvTestFirmComments(5, rowIndex).Value, "")
            TestFirmComments.txtTestNotificationNumber.Text = ToStringIfNotNull(dgvTestFirmComments(4, rowIndex).Value, "")
            TestFirmComments.txtCommentID.Text = ToStringIfNotNull(dgvTestFirmComments(0, rowIndex).Value, "")

            TestFirmComments.Show()
        End If
    End Sub

End Class