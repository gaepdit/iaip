Imports Oracle.ManagedDataAccess.Client


Public Class ISMPMonitoringLog
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim dsTestReportViewer As DataSet
    Dim daTestReportViewer As OracleDataAdapter
    Dim dsNotificationViewer As DataSet
    Dim daNotificationViewer As OracleDataAdapter
    Dim dsTestFirmComments As DataSet
    Dim daTestFirmComments As OracleDataAdapter
    Dim dsEngineer As DataSet
    Dim daEngineer As OracleDataAdapter
    Dim dsPollutants As DataSet
    Dim daPollutants As OracleDataAdapter

    Private Sub ISMPMonitoringLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            DTPStartDate.Text = Format(Date.Today.AddDays(-30), "dd-MMM-yyyy")
            DTPEndDate.Text = OracleDate

            Panel1.Text = "Select a Function..."
            Panel2.Text = CurrentUser.AlphaName
            Panel3.Text = OracleDate

            chbReviewingEngineer.Text = CurrentUser.AlphaName
            chbWitnessingEngineer.Text = CurrentUser.AlphaName

            LoadEngineerDataSet()
            LoadComboBoxes()
            LoadDataSet()

            SCMonitoringLog.SanelySetSplitterDistance(500)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Page Load Functions"
    Sub LoadEngineerDataSet()
        Dim SQL As String

        Try

            SQL = "select " & _
            "distinct(strLastName|| ', ' ||strFirstName) as UserName, " & _
            "numUserID " & _
            "from AIrbranch.epduserprofiles, " & _
            "(select distinct(strStaffResponsible) as NotificationStaff " & _
            "from AIRBranch.ISMPTestNotification) Notification " & _
            "where to_char(AIRBranch.EPDUserProfiles.numUserId) = Notification.NotificationStaff " & _
            "Union " & _
            "Select " & _
            "distinct(strLastName|| ', ' ||strFirstName) as UserName, " & _
            "numUserID " & _
            "from AIRBranch.EPDUserProfiles, " & _
            "(select distinct(strReviewingEngineer) " & _
            "from AIRBranch.ISMPReportInformation) ISMPUsers " & _
            "where to_char(AIRBranch.EPDUserProfiles.numUserID) = ISMPUsers.strReviewingEngineer  "

            dsEngineer = New DataSet

            daEngineer = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daEngineer.Fill(dsEngineer, "Engineers")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadComboBoxes()
        Dim dtStaff As New DataTable
        Dim dtWitnessingEng As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow
        Dim drNewRow2 As DataRow

        Try

            dtStaff.Columns.Add("UserName", GetType(System.String))
            dtStaff.Columns.Add("numUserID", GetType(System.String))

            For Each drDSRow In dsEngineer.Tables("Engineers").Rows()
                drNewRow = dtStaff.NewRow
                drNewRow("UserName") = drDSRow("UserName")
                drNewRow("numUserID") = drDSRow("numUserID")
                dtStaff.Rows.Add(drNewRow)
            Next

            With clbEngineer
                .DataSource = dtStaff
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
            End With

            dtWitnessingEng.Columns.Add("UserName", GetType(System.String))
            dtWitnessingEng.Columns.Add("numUserID", GetType(System.String))

            For Each drDSRow In dsEngineer.Tables("Engineers").Rows()
                drNewRow2 = dtWitnessingEng.NewRow
                drNewRow2("UserName") = drDSRow("UserName")
                drNewRow2("numUserID") = drDSRow("numUserID")
                dtWitnessingEng.Rows.Add(drNewRow2)
            Next

            With clbWitnessingStaff
                .DataSource = dtWitnessingEng
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadPollutants()
        Dim dtPollutants As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try

            SQL = "Select strPollutantCode, strPollutantDescription " & _
                "from AIRBRANCH.LookUPPollutants " & _
                "order by strPollutantDescription"

            dsPollutants = New DataSet

            daPollutants = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daPollutants.Fill(dsPollutants, "Pollutants")

            dtPollutants.Columns.Add("strPOllutantDescription", GetType(System.String))
            dtPollutants.Columns.Add("strPollutantCode", GetType(System.String))

            drNewRow = dtPollutants.NewRow()
            drNewRow("strPOllutantDescription") = " "
            drNewRow("strPollutantCode") = " "
            dtPollutants.Rows.Add(drNewRow)

            For Each drDSRow In dsPollutants.Tables("Pollutants").Rows()
                drNewRow = dtPollutants.NewRow()
                drNewRow("strPOllutantDescription") = drDSRow("strPOllutantDescription")
                drNewRow("strPollutantCode") = drDSRow("strPollutantCode")
                dtPollutants.Rows.Add(drNewRow)
            Next

            'With cboPollutants
            '    .DataSource = dtPollutants
            '    .DisplayMember = "strPOllutantDescription"
            '    .ValueMember = "strPollutantCode"
            '    .SelectedIndex = 0
            'End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "Functions & Subs"
#Region "DataSet(s)"
    Sub LoadDataSet()
        Dim SQLWhere As String = " "
        Try

            If chbTestReports.Checked = True Then
                SQLWhere = ""
                SQL = "select " & _
                "AIRBRANCH.ISMPMaster.strReferenceNumber,  " & _
                "substr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5) as strAIRSNumber,  " & _
                "AIRBRANCH.APBFacilityInformation.strFacilityName,  " & _
                "AIRBRANCH.APBFacilityInformation.strFacilityCity,  " & _
                "AIRBRANCH.LookupCountyInformation.strCountyName,  " & _
                "AIRBRANCH.ISMPReportInformation.strEmissionSource,  " & _
                "AIRBRANCH.LookUpPollutants.strPollutantDescription,  " & _
                "AIRBRANCH.ISMPReportType.strReportType,  " & _
                "AIRBRANCH.ISMPDocumentType.strDocumentType,  " & _
                "(strLastName|| ', ' ||strFirstName) as StaffResponsible,  " & _
                "AIRBRANCH.ISMPREportInformation.datTestDateStart,  " & _
                "AIRBRANCH.ISMPREportInformation.datReceivedDate,  " & _
                "(AIRBRANCH.ISMPREportINformation.datReceivedDate - " & _
                "AIRBRANCH.ISMPReportInformation.datTestDateEnd) as daysToReceived, " & _
                "case " & _
                "when strClosed <> 'True' then trunc(abs(sysdate - AIRBRANCH.ISMPREportInformation.datReceivedDate), 0) " & _
                "else trunc(abs(AIRBranch.ISMPReportInformation.datCompleteDate - AIRBranch.ISMPReportInformation.datReceivedDate)) " & _
                "End daysInAPB, " & _
                "case " & _
                "when AIRBRANCH.ISMPReportInformation.datCompleteDate = '04-Jul-1776' then Null " & _
                "else AIRBRANCH.ISMPReportInformation.datCompleteDate " & _
                "end datCompleteDate, " & _
                "AIRBRANCH.ISMPREportInformation.strClosed,  " & _
                "AIRBRANCH.LookUpISMPComplianceStatus.strComplianceStatus,  " & _
                "AIRBRANCH.ISMPREportInformation.mmoCommentArea,  " & _
                "AIRBRANCH.LookUpEPDUnits.strUnitDesc, " & _
                "AIRBRANCH.ISMPTestLogLink.strTestLogNumber, " & _
                "strPreComplianceStatus " & _
                "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
                "AIRBRANCH.LookUpCountyInformation, AIRBRANCH.ISMPReportInformation, " & _
                "AIRBRANCH.LookUpPollutants, AIRBRANCH.ISMPReportType,  " & _
                "AIRBRANCH.ISMPDocumentType, AIRBRANCH.EPDUSerProfiles,  " & _
                "AIRBRANCH.LookUpISMPComplianceStatus,  " & _
                "AIRBRANCH.ISMPTestLogLink, AIRBRANCH.ISMPWitnessingEng, " & _
                "AIRBRANCH.LookUpEPDUnits " & _
                "where AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPREportInformation.strReferenceNumber  " & _
                "and AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber " & _
                "and AIRBRANCH.ISMPREportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID (+)  " & _
                "and AIRBRANCH.ISMPREportINformation.strReportType = AIRBRANCH.ISMPREportType.strKey " & _
                "and substr(AIRBRANCH.ISMPMaster.strAIRSNumber, 5, 3) = AIRBRANCH.LookupCountyInformation.strCountyCode  " & _
                "and AIRBRANCH.ISMPReportInformation.strpollutant = AIRBRANCH.LookUpPollutants.strPollutantCode (+)  " & _
                "and AIRBRANCH.ISMPREportINformation.strDocumentType = AIRBRANCH.ISMPDocumentType.strKey (+)  " & _
                "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey (+) " & _
                "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPTestLogLink.strReferenceNumber (+) " & _
                "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPWitnessingEng.strReferenceNumber (+) " & _
                "and AIRBRANCH.EPDUserProfiles.numUnit = AIRBRANCH.LookUpEPDUnits.numUnitCode " & _
                "and (strDelete is Null or strDelete <> 'DELETE') "

                If chbReviewingEngineer.Checked = True Then
                    SQLWhere = SQLWhere & " and ( "
                    If clbEngineer.CheckedItems.Count > 0 Then
                        For x As Integer = 0 To clbEngineer.Items.Count - 1
                            If clbEngineer.GetItemChecked(x) = True Then
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
                            If clbEngineer.GetItemChecked(x) = True Then
                                clbEngineer.SelectedIndex = x
                                SQLWhere = SQLWhere & " strReviewingEngineer = '" & clbEngineer.SelectedValue & "' Or "
                            End If
                        Next
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3))
                        SQLWhere = SQLWhere & " ) "
                    End If
                End If

                If chbWitnessingEngineer.Checked = True Then
                    SQLWhere = SQLWhere & " and ( "
                    If clbWitnessingStaff.CheckedItems.Count > 0 Then
                        For x As Integer = 0 To clbWitnessingStaff.Items.Count - 1
                            If clbWitnessingStaff.GetItemChecked(x) = True Then
                                clbWitnessingStaff.SelectedIndex = x
                                SQLWhere = SQLWhere & " AIRBRANCH.ISMPReportInformation.strWitnessingengineer = '" & clbWitnessingStaff.SelectedValue & "' Or " & _
                                                            " AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = '" & clbWitnessingStaff.SelectedValue & "' or "
                            End If
                        Next
                    End If
                    SQLWhere = SQLWhere & " AIRBRANCH.ISMPReportInformation.strWitnessingengineer = '" & CurrentUser.UserID & "' or strWitnessingengineer2 = '" & CurrentUser.UserID & "' ) "
                Else
                    If clbWitnessingStaff.CheckedItems.Count > 0 Then
                        SQLWhere = SQLWhere & " and ( "
                        For x As Integer = 0 To clbWitnessingStaff.Items.Count - 1
                            If clbWitnessingStaff.GetItemChecked(x) = True Then
                                clbWitnessingStaff.SelectedIndex = x
                                SQLWhere = SQLWhere & "  AIRBRANCH.ISMPReportInformation.strWitnessingengineer = '" & clbWitnessingStaff.SelectedValue & "' Or " & _
                                                            " AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = '" & clbWitnessingStaff.SelectedValue & "' or "
                            End If
                        Next
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3))
                        SQLWhere = SQLWhere & " ) "
                    End If
                End If

                If chbOpen.Checked = True And chbClosed.Checked = False Then
                    SQLWhere = SQLWhere & " and strClosed = 'False' "
                End If
                If chbOpen.Checked = False And chbClosed.Checked = True Then
                    SQLWhere = SQLWhere & " and strClosed = 'True' "
                End If

                If chbAll.Checked = True Then

                Else
                    If chbUnassigned.Checked = True Or chbOneStackTwoRun.Checked = True Or chbOneStackThreeRun.Checked = True Or _
                          chbOneStackFourRun.Checked = True Or chbTwoStackStandard.Checked = True Or chbTwoStackDRE.Checked = True Or _
                            chbLoadingRack.Checked = True Or chbPondTreatment.Checked = True Or chbGasConcentration.Checked = True Or _
                              chbFlare.Checked = True Or chbRata.Checked = True Or chbMemorandumStandard.Checked = True Or _
                                 chbMemorandumToFile.Checked = True Or chbMethod9Single.Checked = True Or chbMethod9Multi.Checked = True Or _
                                 chbMethod22.Checked = True Or chbPEMS.Checked = True Or chbPTE.Checked = True Then
                        SQLWhere = SQLWhere & " and ( "

                        If chbUnassigned.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '001' Or "
                        End If
                        If chbOneStackTwoRun.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '002' Or "
                        End If
                        If chbOneStackThreeRun.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '003' Or "
                        End If
                        If chbOneStackFourRun.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '004' Or "
                        End If
                        If chbTwoStackStandard.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '005' Or "
                        End If
                        If chbTwoStackDRE.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '006' Or "
                        End If
                        If chbLoadingRack.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '007' Or "
                        End If
                        If chbPondTreatment.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '008' Or "
                        End If
                        If chbGasConcentration.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '009' Or "
                        End If
                        If chbFlare.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '010' Or "
                        End If
                        If chbRata.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '011' Or "
                        End If
                        If chbMemorandumStandard.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '012' Or "
                        End If
                        If chbMemorandumToFile.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '013' Or "
                        End If
                        If chbMethod9Single.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '016' Or "
                        End If
                        If chbMethod9Multi.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '014' Or "
                        End If
                        If chbMethod22.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '015' Or "
                        End If
                        If chbPEMS.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '017' Or "
                        End If
                        If chbPTE.Checked = True Then
                            SQLWhere = SQLWhere & " AIRBRANCH.ISMPDocumentType.strKey = '018' Or "
                        End If
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3)) & " ) "
                    End If
                End If

                If chbMonitorCertification.Checked = True Or chbPEMSDevelopment.Checked = True Or chbRATAandCEMS.Checked = True Or _
                     chbSourceTest.Checked = True Or chbOther.Checked = True Then

                    SQLWhere = SQLWhere & " and ( "

                    If chbMonitorCertification.Checked = True Then
                        SQLWhere = SQLWhere & " AIRBRANCH.ISMPReportInformation. strReportType = '001' Or "
                    End If
                    If chbPEMSDevelopment.Checked = True Then
                        SQLWhere = SQLWhere & " AIRBRANCH.ISMPReportInformation. strReportType = '002' Or "
                    End If
                    If chbRATAandCEMS.Checked = True Then
                        SQLWhere = SQLWhere & " AIRBRANCH.ISMPReportInformation. strReportType = '003' Or "
                    End If
                    If chbSourceTest.Checked = True Then
                        SQLWhere = SQLWhere & " AIRBRANCH.ISMPReportInformation. strReportType = '004' Or "
                    End If
                    If chbOther.Checked = True Then
                        SQLWhere = SQLWhere & " AIRBRANCH.ISMPReportInformation. strReportType = '005' Or "
                    End If
                    SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3)) & " ) "

                End If

                If chbComplianceStatus1.Checked = True Or chbComplianceStatus2.Checked = True Or _
                    chbComplianceStatus3.Checked = True Or chbComplianceStatus4.Checked = True Or _
                        chbComplianceStatus5.Checked = True Then

                    SQLWhere = SQLWhere & " and ( "

                    If chbComplianceStatus1.Checked = True Then
                        SQLWhere = SQLWhere & " AIRBRANCH.ISMPReportInformation.strComplianceStatus = '01' Or "
                    End If
                    If chbComplianceStatus2.Checked = True Then
                        SQLWhere = SQLWhere & " AIRBRANCH.ISMPReportInformation.strComplianceStatus = '02' Or "
                    End If
                    If chbComplianceStatus3.Checked = True Then
                        SQLWhere = SQLWhere & " AIRBRANCH.ISMPReportInformation.strComplianceStatus = '03' Or "
                    End If
                    If chbComplianceStatus4.Checked = True Then
                        SQLWhere = SQLWhere & " AIRBRANCH.ISMPReportInformation.strComplianceStatus = '04' Or "
                    End If
                    If chbComplianceStatus5.Checked = True Then
                        SQLWhere = SQLWhere & " AIRBRANCH.ISMPReportInformation.strComplianceStatus = '05' Or "
                    End If
                    SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3)) & " ) "
                End If

                If rdbNA.Checked = True Then

                Else
                    If rdbFacilityDateReceived.Checked = True Then
                        SQLWhere = SQLWhere & "AND datReceivedDate between '" & DTPStartDate.Text & "' and '" & DTPEndDate.Text & "' "
                    End If
                    If rdbFacilityDateTestStarted.Checked = True Then
                        SQLWhere = SQLWhere & "AND datTestDateStart between '" & DTPStartDate.Text & "' and '" & DTPEndDate.Text & "' "
                    End If
                    If rdbFacilityDateCompleted.Checked = True Then
                        SQLWhere = SQLWhere & "And datCompleteDate between '" & DTPStartDate.Text & "' and '" & DTPEndDate.Text & "' "
                    End If
                End If

                If txtAIRSNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and AIRBRANCH.ISMPMaster.strAIRSnumber like '%" & txtAIRSNumberFilter.Text & "%' "
                End If
                If txtFacilityNameFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(AIRBRANCH.APBFacilityInformation.strFacilityName) like Upper('%" & txtFacilityNameFilter.Text & "%') "
                End If
                If txtReferenceNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and AIRBRANCH.ISMPMaster.strReferenceNumber like '%" & txtReferenceNumberFilter.Text & "%' "
                End If
                If txtNotificationNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and AIRBRANCH.ISMPTestLogLink.strTestLogNumber like '%" & txtNotificationNumberFilter.Text & "%' "
                End If
                If txtCityFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) like Upper('%" & txtCityFilter.Text & "%') "
                End If
                If txtCountyFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(AIRBRANCH.LookUpCountyInformation.strCountyName) like Upper('%" & txtCountyFilter.Text & "%') "
                End If
                If txtEmissionSourceTestedFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(AIRBRANCH.ISMPReportInformation.strEmissionSource) " & _
                                               "like upper('%" & txtEmissionSourceTestedFilter.Text & "%') "
                End If
                If txtCommentFieldFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(AIRBRANCH.ISMPReportInformation.mmoCommentArea) " & _
                                               "like Upper('%" & txtCommentFieldFilter.Text & "%') "
                End If

                If txtPollutantFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(AIRBRANCH.LookUpPollutants.strPollutantDescription) " & _
                                               "like Upper('%" & txtPollutantFilter.Text & "%') "
                End If

                If chbUnitOption1.Checked = True And chbUnitOption2.Checked = True Then
                    SQLWhere = SQLWhere & " And (strReviewingUnit = '12' or strReviewingUnit = '13') "
                End If
                If chbUnitOption1.Checked = True And chbUnitOption2.Checked = False Then
                    SQLWhere = SQLWhere & " And strReviewingUnit = '12' "
                End If
                If chbUnitOption1.Checked = False And chbUnitOption2.Checked = True Then
                    SQLWhere = SQLWhere & " And strReviewingUnit = '13' "
                End If

                SQL = SQL & SQLWhere

                dsTestReportViewer = New DataSet
                daTestReportViewer = New OracleDataAdapter(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                daTestReportViewer.Fill(dsTestReportViewer, "TestReportViewer")
                dgvTestReportViewer.DataSource = dsTestReportViewer
                dgvTestReportViewer.DataMember = "TestReportViewer"

                dgvTestReportViewer.RowHeadersVisible = False
                dgvTestReportViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvTestReportViewer.AllowUserToResizeColumns = True
                dgvTestReportViewer.AllowUserToAddRows = False
                dgvTestReportViewer.AllowUserToDeleteRows = False
                dgvTestReportViewer.AllowUserToOrderColumns = True
                dgvTestReportViewer.AllowUserToResizeRows = True
                dgvTestReportViewer.ColumnHeadersHeight = "35"
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
                dgvTestReportViewer.Columns("strDocumentType").HeaderText = "Report Type"
                dgvTestReportViewer.Columns("strDocumentType").DisplayIndex = 8
                dgvTestReportViewer.Columns("StaffResponsible").HeaderText = "Staff Responsible"
                dgvTestReportViewer.Columns("StaffResponsible").DisplayIndex = 9
                dgvTestReportViewer.Columns("datTestDateStart").HeaderText = "Test Date"
                dgvTestReportViewer.Columns("datTestDateStart").DisplayIndex = 10
                dgvTestReportViewer.Columns("datTestDateStart").DefaultCellStyle.Format = "dd-MMM-yyyy"
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
                txtReportCount.Text = dgvTestReportViewer.RowCount

                LoadCompliaceColor()

            End If  'Test Reports

            If Me.chbNotifications.Checked = True Then
                SQLWhere = ""
                SQL = "select distinct " & _
               "substr(AIRBRANCH.ISMPTEstNotification.strAIRSNumber, 5) as AIRSNumber, " & _
               "AIRBRANCH.APBFacilityInformation.strFacilityName,  " & _
               "strFacilityCity,  " & _
               "strCountyName,  " & _
               "to_number(AIRBRANCH.ISMPTestNotification.strTestLogNumber) as strTestLogNumber,    " & _
               "strEmissionUnit,  datProposedStartDate,    " & _
               "(strLastName|| ', ' ||strFirstName) as StaffResponsible,   " & _
               "strUnitDesc, " & _
               "AIRBRANCH.ISMPTestNotification.strcomments,  " & _
               "strReferenceNumber " & _
               "from AIRBRANCH.ISMPTestNotification, AIRBRANCH.ISMPTestLogLink,  " & _
               "AIRBRANCH.EPDUserProfiles, AIRBRANCH.APBFacilityINformation,  " & _
               "AIRBRANCH.LookUpCountyInformation, AIRBRANCH.LookUpEPDUnits  " & _
               "where  AIRBRANCH.ISMPTestNotification.strTestLogNumber = AIRBRANCH.ISMPTestLogLink.strTestLognumber (+)    " & _
               "and AIRBRANCH.ISMPTestNotification.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber (+) " & _
               "and substr(AIRBRANCH.ISMPTestNotification.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strcountycode (+)  " & _
               "and AIRBRANCH.EPDUserProfiles.numUnit = AIRBRANCH.LookUpEPDUnits.numUnitCode (+) " & _
               "and strStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID (+)  "

                If chbReviewingEngineer.Checked = True Then
                    SQLWhere = SQLWhere & " and ( "
                    If clbEngineer.CheckedItems.Count > 0 Then
                        For x As Integer = 0 To clbEngineer.Items.Count - 1
                            If clbEngineer.GetItemChecked(x) = True Then
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
                            If clbEngineer.GetItemChecked(x) = True Then
                                clbEngineer.SelectedIndex = x
                                SQLWhere = SQLWhere & " strStaffResponsible = '" & clbEngineer.SelectedValue & "' Or "
                            End If
                        Next
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3))
                        SQLWhere = SQLWhere & " ) "
                    End If
                End If

                If chbOpen.Checked = True Then
                    SQLWhere = SQLWhere & " and to_date(datproposedstartdate) > to_date(sysdate - 90) "
                End If
                If txtAIRSNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and AIRBRANCH.ISMPTestNotification.strAIRSnumber like '%" & txtAIRSNumberFilter.Text & "%' "
                End If
                If txtFacilityNameFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(AIRBRANCH.APBFacilityInformation.strFacilityName) like Upper('%" & txtFacilityNameFilter.Text & "%') "
                End If
                If txtReferenceNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and AIRBRANCH.ISMPTestLogLink.strReferenceNumber like '%" & txtReferenceNumberFilter.Text & "%' "
                End If
                If txtNotificationNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and AIRBRANCH.ISMPTestNotification.strTestLogNumber like '%" & txtNotificationNumberFilter.Text & "%' "
                End If
                If txtCityFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) like Upper('%" & txtCityFilter.Text & "%') "
                End If
                If txtCountyFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(AIRBRANCH.LookUpCountyInformation.strCountyName) like Upper('%" & txtCountyFilter.Text & "%') "
                End If
                If txtEmissionSourceTestedFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(AIRBRANCH.ISMPTestNotification.strEmissionUnit) " & _
                                               "like upper('%" & txtEmissionSourceTestedFilter.Text & "%') "
                End If
                If txtCommentFieldFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(AIRBRANCH.ISMPTestNotification.mmoComments) " & _
                                               "like Upper('%" & txtCommentFieldFilter.Text & "%') "
                End If
                If txtPollutantFilter.Text <> "" Then

                End If
                If chbUnitOption1.Checked = True And chbUnitOption2.Checked = True Then
                    SQLWhere = SQLWhere & " And (strUserUnit = 'Chemical & VOC' or strUserUnit = 'Combustion & Mineral') "
                End If
                If chbUnitOption1.Checked = True And chbUnitOption2.Checked = False Then
                    SQLWhere = SQLWhere & " And strUserUnit = 'Chemical & VOC' "
                End If
                If chbUnitOption1.Checked = False And chbUnitOption2.Checked = True Then
                    SQLWhere = SQLWhere & " And strUserUnit = 'Combustion & Mineral' "
                End If
                If chbNotificationLinked.Checked = True Then
                    SQLWhere = SQLWhere & " and strReferenceNumber is Not Null "
                End If
                If chbNotificationUnlinked.Checked = True Then
                    SQLWhere = SQLWhere & " and strReferenceNumber is Null "
                End If

                SQL = SQL & SQLWhere

                dsNotificationViewer = New DataSet
                daNotificationViewer = New OracleDataAdapter(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                daNotificationViewer.Fill(dsNotificationViewer, "NotificationViewer")
                dgvNotificationLog.DataSource = dsNotificationViewer
                dgvNotificationLog.DataMember = "NotificationViewer"

                dgvNotificationLog.RowHeadersVisible = False
                dgvNotificationLog.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvNotificationLog.AllowUserToResizeColumns = True
                dgvNotificationLog.AllowUserToAddRows = False
                dgvNotificationLog.AllowUserToDeleteRows = False
                dgvNotificationLog.AllowUserToOrderColumns = True
                dgvNotificationLog.AllowUserToResizeRows = True
                dgvNotificationLog.ColumnHeadersHeight = "35"
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
                '   dgvNotificationLog.Columns("strTestLogNumber").DefaultCellStyle.Format = 


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
                txtNotificationCount.Text = dgvNotificationLog.RowCount
            End If

            If chbTestFirmComments.Checked = True Then
                SQL = ""
                SQLWhere = ""

                SQL = "select " & _
               "numcommentsID, strTestingFirm, " & _
               "substr(AIRBRANCH.ISMPTestFirmComments.strAIRSNumber, 5) as AIRSNumber, " & _
               "strFacilityName, " & _
               "AIRBRANCH.ISMPTestFirmComments.strTestLogNumber, strReferenceNumber, " & _
               "case " & _
               "when strCommentType is Null then 'Unknown' " & _
               "when strCommentType = '1' then 'Pre-test Comment' " & _
               "when strCommentType = '2' then 'Day of test Comment' " & _
               "when strCommentType = '3' then 'Post-test Comment' " & _
               "else 'Unknown' " & _
               "end CommentType, " & _
               "(strLastName|| ', ' ||strFirstName) as StaffResponsible, " & _
               "datCommentDate, strComment, " & _
               "strFacilityCity, " & _
               "AIRBRANCH.LookUpCountyInformation.strCountyName " & _
               "from AIRBRANCH.ISMPTestFirmComments, AIRBRANCH.LookUpTestingFirms, " & _
               "AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles,  " & _
               "AIRBRANCH.ISMPTestNotification, AIRBRANCH.LookUpCountyInformation " & _
               "where AIRBRANCH.ISMPTestFirmComments.strTestingFirmKey = AIRBRANCH.LooKUpTestingFirms.strTestingFirmKey " & _
               "and AIRBRANCH.ISMPTestFirmComments.strAIRSnumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber (+) " & _
               "and AIRBRANCH.ISMPTestFirmComments.strStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID (+) " & _
               "and substr(AIRBRANCH.ISMPTestFirmComments.strAIRSNUmber, 5, 3)  = AIRBRANCH.LookUpCountyInformation.strCountycode (+) " & _
               "and AIRBRANCH.ismptestfirmcomments.strtestlognumber = AIRBRANCH.ismptestnotification.strtestlognumber (+) "


                If chbReviewingEngineer.Checked = True Then
                    SQLWhere = SQLWhere & " and ( "
                    If clbEngineer.CheckedItems.Count > 0 Then
                        For x As Integer = 0 To clbEngineer.Items.Count - 1
                            If clbEngineer.GetItemChecked(x) = True Then
                                clbEngineer.SelectedIndex = x
                                SQLWhere = SQLWhere & " AIRBRANCH.ISMPTestFirmComments.strStaffResponsible = '" & clbEngineer.SelectedValue & "' Or "
                            End If
                        Next
                    End If
                    SQLWhere = SQLWhere & " AIRBRANCH.ISMPTestFirmComments.strStaffResponsible = '" & CurrentUser.UserID & "' ) "
                Else
                    If clbEngineer.CheckedItems.Count > 0 Then
                        SQLWhere = SQLWhere & " and ( "
                        For x As Integer = 0 To clbEngineer.Items.Count - 1
                            If clbEngineer.GetItemChecked(x) = True Then
                                clbEngineer.SelectedIndex = x
                                SQLWhere = SQLWhere & " AIRBRANCH.ISMPTestFirmComments.strStaffResponsible = '" & clbEngineer.SelectedValue & "' Or "
                            End If
                        Next
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3))
                        SQLWhere = SQLWhere & " ) "
                    End If
                End If

                If txtAIRSNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and AIRBRANCH.ISMPTestFirmComments.strAIRSnumber like '%" & txtAIRSNumberFilter.Text & "%' "
                End If
                If txtFacilityNameFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(AIRBRANCH.APBFacilityInformation.strFacilityName) like Upper('%" & txtFacilityNameFilter.Text & "%') "
                End If
                If txtReferenceNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and AIRBRANCH.ISMPTestFirmComments.strReferenceNumber like '%" & txtReferenceNumberFilter.Text & "%' "
                End If
                If txtNotificationNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and AIRBRANCH.ISMPTestFirmComments.strTestLogNumber like '%" & txtNotificationNumberFilter.Text & "%' "
                End If
                If txtCityFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and upper(AIRBRANCH.APBFacilityInformation.strFacilityCity) like Upper('%" & txtCityFilter.Text & "%') "
                End If
                If txtCountyFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(AIRBRANCH.LookUpCountyInformation.strCountyName) like Upper('%" & txtCountyFilter.Text & "%') "
                End If
                If txtEmissionSourceTestedFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(AIRBRANCH.ISMPTestNotification.strEmissionUnit) " & _
                                               "like upper('%" & txtEmissionSourceTestedFilter.Text & "%') "
                End If
                If txtCommentFieldFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(AIRBRANCH.ISMPTestFirmComments.strComments) " & _
                                               "like Upper('%" & Replace(txtCommentFieldFilter.Text, "'", "''") & "%') "
                End If

                If txtTestingFirm.Text <> "" Then
                    SQLWhere = SQLWhere & "and Upper(AIRBRANCH.LookUpTestingFirms.strTestingFirm) " & _
                                                "like Upper('%" & Replace(txtTestingFirm.Text, "'", "''") & "%') "
                End If

                SQL = SQL & SQLWhere

                dsTestFirmComments = New DataSet
                daTestFirmComments = New OracleDataAdapter(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                daTestFirmComments.Fill(dsTestFirmComments, "TestFirmComments")
                dgvTestFirmComments.DataSource = dsTestFirmComments
                dgvTestFirmComments.DataMember = "TestFirmComments"

                dgvTestFirmComments.RowHeadersVisible = False
                dgvTestFirmComments.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvTestFirmComments.AllowUserToResizeColumns = True
                dgvTestFirmComments.AllowUserToAddRows = False
                dgvTestFirmComments.AllowUserToDeleteRows = False
                dgvTestFirmComments.AllowUserToOrderColumns = True
                dgvTestFirmComments.AllowUserToResizeRows = True
                dgvTestFirmComments.ColumnHeadersHeight = "35"
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

                txtTestFirmCommentsCount.Text = dgvTestFirmComments.RowCount
            End If

        Catch ex As Exception
            ErrorReport(ex, SQL, "ISMPTestReportViewer.LoadDataSet")
        Finally

        End Try


    End Sub
#End Region
    Sub LoadTestLogData()
        Try

            If txtTestLogNumber.Text <> "" Then
                SQL = "Select " & _
                "strTestLogNumber,  " & _
                "AIRBRANCH.APBFacilityInformation.strFacilityName,  " & _
                "substr(AIRBRANCH.APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,  " & _
                "strEmissionUnit,  " & _
                "AIRBRANCH.APBFacilityInformation.strFacilityCity,  " & _
                "strCountyName,  " & _
                "datProposedStartDate  " & _
                "from AIRBRANCH.APBFacilityINformation, AIRBRANCH.ISMPTestNotification,  " & _
                "AIRBRANCH.LookUpCountyInformation  " & _
                "where '0413'||AIRBRANCH.ISMPTestNotification.strAIRSnumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber (+) " & _
                "and substr(AIRBRANCH.ISMPTestNotification.strAIRSNumber, 1, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode (+)  " & _
                "and strTestLogNumber = '" & txtTestLogNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub SelectTestReport()
        Try
            Dim id As String = txtReferenceNumber.Text
            If id = "" Then Exit Sub

            If DAL.Ismp.StackTestExists(id) Then
                If CurrentUser.ProgramID = 3 Then
                    OpenMultiForm("ISMPTestReports", id)
                Else
                    If DAL.Ismp.StackTestIsClosedOut(id) Then
                        If PrintOut IsNot Nothing AndAlso Not PrintOut.IsDisposed Then
                            PrintOut.Dispose()
                        End If
                        PrintOut = New IAIPPrintOut
                        PrintOut.txtReferenceNumber.Text = txtReferenceNumber.Text
                        PrintOut.txtPrintType.Text = "SSCP"
                        PrintOut.Show()
                    Else
                        MsgBox("This test has not been completely reviewed by ISMP.", MsgBoxStyle.Information, "Facility Summary")
                    End If
                End If
            Else
                MsgBox("Reference number is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ResetOptions()
        Try

            rdbFacilityDateReceived.Checked = True
            DTPStartDate.Text = Format(Date.Today.AddDays(-30), "dd-MMM-yyyy")
            DTPEndDate.Text = OracleDate
            chbUnitOption1.Checked = False
            chbUnitOption2.Checked = False
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
    Private Sub ISMPTestReportViewer_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LLSelectReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LLSelectReport.LinkClicked
        Try

            SelectTestReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnRunFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunFilter.Click
        Try

            LoadDataSet()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiReset.Click
        Try

            ResetOptions()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            SendKeys.Send("^(x)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            SendKeys.Send("^(v)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            SendKeys.Send("^(c)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiBack.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgvTestReportViewer_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvTestReportViewer.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvTestReportViewer.HitTest(e.X, e.Y)

        Try


            If dgvTestReportViewer.RowCount > 0 And hti.RowIndex <> -1 Then
                txtReferenceNumber.Text = dgvTestReportViewer(0, hti.RowIndex).Value
                If IsDBNull(dgvTestReportViewer(2, hti.RowIndex).Value) Then
                    txtFacilityName.Text = ""
                Else
                    txtFacilityName.Text = dgvTestReportViewer(2, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTestReportViewer(1, hti.RowIndex).Value) Then
                    txtAIRSNumber.Text = ""
                Else
                    txtAIRSNumber.Text = dgvTestReportViewer(1, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTestReportViewer(7, hti.RowIndex).Value) Then
                    txtTestType.Text = ""
                Else
                    txtTestType.Text = dgvTestReportViewer(7, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTestReportViewer(3, hti.RowIndex).Value) Then
                    txtFacilityCity.Text = ""
                Else
                    txtFacilityCity.Text = dgvTestReportViewer(3, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTestReportViewer(4, hti.RowIndex).Value) Then
                    txtFacilityCounty.Text = ""
                Else
                    txtFacilityCounty.Text = dgvTestReportViewer(4, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTestReportViewer(6, hti.RowIndex).Value) Then
                    txtPollutant.Text = ""
                Else
                    txtPollutant.Text = dgvTestReportViewer(6, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTestReportViewer(19, hti.RowIndex).Value) Then
                Else
                    txtTestLogNumber.Text = dgvTestReportViewer(19, hti.RowIndex).Value
                    LoadTestLogData()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgvTestFirmComments_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvTestFirmComments.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvTestFirmComments.HitTest(e.X, e.Y)

        Try
            If dgvTestFirmComments.RowCount > 0 And hti.RowIndex <> -1 Then
                txtCommentNumber.Text = dgvTestFirmComments(0, hti.RowIndex).Value

                If IsDBNull(dgvTestFirmComments(3, hti.RowIndex).Value) Then
                    txtTestFirmFacilityName.Text = ""
                Else
                    txtTestFirmFacilityName.Text = dgvTestFirmComments(3, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTestFirmComments(2, hti.RowIndex).Value) Then
                    txtTestFirmAirsNumber.Text = ""
                Else
                    txtTestFirmAirsNumber.Text = dgvTestFirmComments(2, hti.RowIndex).Value
                End If

                If IsDBNull(dgvTestFirmComments(1, hti.RowIndex).Value) Then
                    txtTestFirmName.Text = ""
                Else
                    txtTestFirmName.Text = dgvTestFirmComments(1, hti.RowIndex).Value
                End If

                If IsDBNull(dgvTestFirmComments(6, hti.RowIndex).Value) Then
                    txtTestFirmCommentType.Text = ""
                Else
                    txtTestFirmCommentType.Text = dgvTestFirmComments(6, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTestFirmComments(10, hti.RowIndex).Value) Then
                    txtTestFirmFacilityCity.Text = ""
                Else
                    txtTestFirmFacilityCity.Text = dgvTestFirmComments(10, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTestFirmComments(11, hti.RowIndex).Value) Then
                    txtTestFirmCounty.Text = ""
                Else
                    txtTestFirmCounty.Text = dgvTestFirmComments(11, hti.RowIndex).Value
                End If

                If IsDBNull(dgvTestFirmComments(5, hti.RowIndex).Value) Then
                    txtTestFirmReferenceNumber.Text = ""
                Else
                    txtTestFirmReferenceNumber.Text = dgvTestFirmComments(5, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTestFirmComments(4, hti.RowIndex).Value) Then
                    txtTestFirmTestLogNumber.Text = ""
                Else
                    txtTestFirmTestLogNumber.Text = dgvTestFirmComments(4, hti.RowIndex).Value
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub tsbResize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbResize.Click
        Try
            SCMonitoringLog.ToggleSplitterDistance(235, 500)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvNotificationLog_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvNotificationLog.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvNotificationLog.HitTest(e.X, e.Y)

        Try


            If dgvNotificationLog.RowCount > 0 And hti.RowIndex <> -1 Then
                txtTestLogNumber.Text = dgvNotificationLog(4, hti.RowIndex).Value
                If IsDBNull(dgvNotificationLog(1, hti.RowIndex).Value) Then
                    txtNotificationFacilityName.Text = ""
                Else
                    txtNotificationFacilityName.Text = dgvNotificationLog(1, hti.RowIndex).Value
                End If
                If IsDBNull(dgvNotificationLog(0, hti.RowIndex).Value) Then
                    txtNotificationAIRSNumber.Text = ""
                Else
                    txtNotificationAIRSNumber.Text = dgvNotificationLog(0, hti.RowIndex).Value
                End If
                If IsDBNull(dgvNotificationLog(5, hti.RowIndex).Value) Then
                    txtNotificationEmissionUnit.Text = ""
                Else
                    txtNotificationEmissionUnit.Text = dgvNotificationLog(5, hti.RowIndex).Value
                End If
                If IsDBNull(dgvNotificationLog(2, hti.RowIndex).Value) Then
                    txtNotificationCity.Text = ""
                Else
                    txtNotificationCity.Text = dgvNotificationLog(2, hti.RowIndex).Value
                End If
                If IsDBNull(dgvNotificationLog(3, hti.RowIndex).Value) Then
                    txtNotificationCounty.Text = ""
                Else
                    txtNotificationCounty.Text = dgvNotificationLog(3, hti.RowIndex).Value
                End If
                If IsDBNull(dgvNotificationLog(6, hti.RowIndex).Value) Then
                    txtNotificationTestStart.Text = ""
                Else
                    txtNotificationTestStart.Text = Format(dgvNotificationLog(6, hti.RowIndex).Value, "dd-MMM-yyyy")
                End If
                If IsDBNull(dgvNotificationLog(10, hti.RowIndex).Value) Then

                Else
                    txtReferenceNumber.Text = dgvNotificationLog(10, hti.RowIndex).Value
                    'txtTestFirmReferenceNumber.Text = dgvNotificationLog(10, hti.RowIndex).Value
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbSelectTestLog_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbSelectTestLog.LinkClicked
        Try
            If Not IsNothing(ISMPNotificationLogForm) Then
                ISMPNotificationLogForm.txtTestNotificationNumber.Text = txtTestLogNumber.Text
                ISMPNotificationLogForm.Show()
            Else
                ISMPNotificationLogForm = Nothing
                If ISMPNotificationLogForm Is Nothing Then ISMPNotificationLogForm = New ISMPNotificationLog
                ISMPNotificationLogForm.txtTestNotificationNumber.Text = txtTestLogNumber.Text
                ISMPNotificationLogForm.Show()
            End If
            ISMPNotificationLogForm.LoadTestNotification()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbOpenComments_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbOpenComments.LinkClicked
        Try

            If TestFirmComments Is Nothing Then
                If TestFirmComments Is Nothing Then TestFirmComments = New ISMPTestFirmComments
            Else
                TestFirmComments.Dispose()
                TestFirmComments = New ISMPTestFirmComments
            End If
            TestFirmComments.Show()
            TestFirmComments.txtAIRSNumber.Text = txtTestFirmAirsNumber.Text
            TestFirmComments.txtFacilityTested.Text = txtTestFirmFacilityName.Text
            TestFirmComments.cboTestingFirm.Text = txtTestFirmName.Text

            If txtTestFirmReferenceNumber.Text <> "" Then
                TestFirmComments.txtTestReportNumber.Text = txtTestFirmReferenceNumber.Text
            Else
                TestFirmComments.txtTestReportNumber.Text = ""
            End If
            If txtTestFirmTestLogNumber.Text <> "" Then
                TestFirmComments.txtTestNotificationNumber.Text = txtTestFirmTestLogNumber.Text
            Else
                TestFirmComments.txtTestNotificationNumber.Text = ""
            End If
            TestFirmComments.txtCommentID.Text = txtCommentNumber.Text
            TestFirmComments.LoadTestFirmComments()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Try

            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Public WriteOnly Property ValueFromFacilityLookUp() As String
        Set(ByVal Value As String)
            txtAIRSNumberFilter.Text = Value
        End Set
    End Property

    Private Sub OpenFacilityLookupTool()
        Try
            Dim facilityLookupDialog As New IAIPFacilityLookUpTool
            facilityLookupDialog.ShowDialog()
            If facilityLookupDialog.DialogResult = DialogResult.OK _
            AndAlso facilityLookupDialog.SelectedAirsNumber <> "" Then
                Me.ValueFromFacilityLookUp = facilityLookupDialog.SelectedAirsNumber
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub tsbFacilitySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbFacilitySearch.Click
        OpenFacilityLookupTool()
    End Sub

    Private Sub tsbExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExportToExcel.Click
        dgvTestReportViewer.ExportToExcel(Me)
    End Sub
    Sub LoadCompliaceColor()
        Try
            For Each row As DataGridViewRow In dgvTestReportViewer.Rows
                If Not row.IsNewRow Then
                    If Not row.Cells(20).Value Is DBNull.Value Then
                        temp = row.Cells(20).Value
                        If row.Cells(20).Value = "True" Then
                            row.DefaultCellStyle.BackColor = Color.Pink
                        End If
                    End If
                    If Not row.Cells(16).Value Is DBNull.Value Then
                        temp = row.Cells(16).Value
                        If row.Cells(16).Value = "Not In Compliance" Then
                            row.DefaultCellStyle.BackColor = Color.Tomato
                        End If
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvTestReportViewer_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTestReportViewer.Sorted
        Try
            LoadCompliaceColor()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mmiReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiReports.Click
        Try
            If StaffReports Is Nothing Then
                If StaffReports Is Nothing Then StaffReports = New ISMPStaffReports
            Else
                StaffReports.Dispose()
                StaffReports = New ISMPStaffReports
                If StaffReports Is Nothing Then StaffReports = New ISMPStaffReports
            End If
            StaffReports.Show()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

   
End Class