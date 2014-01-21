Imports Oracle.DataAccess.Client


Public Class ISMPMonitoringLog
    Dim SQL, SQL2 As String
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
            Panel2.Text = UserName
            Panel3.Text = OracleDate

            chbReviewingEngineer.Text = UserName
            chbWitnessingEngineer.Text = UserName

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

            daEngineer = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
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
                "from " & DBNameSpace & ".LookUPPollutants " & _
                "order by strPollutantDescription"

            dsPollutants = New DataSet

            daPollutants = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
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
                "" & DBNameSpace & ".ISMPMaster.strReferenceNumber,  " & _
                "substr(" & DBNameSpace & ".ISMPMaster.strAIRSNumber, 5) as strAIRSNumber,  " & _
                "" & DBNameSpace & ".APBFacilityInformation.strFacilityName,  " & _
                "" & DBNameSpace & ".APBFacilityInformation.strFacilityCity,  " & _
                "" & DBNameSpace & ".LookupCountyInformation.strCountyName,  " & _
                "" & DBNameSpace & ".ISMPReportInformation.strEmissionSource,  " & _
                "" & DBNameSpace & ".LookUpPollutants.strPollutantDescription,  " & _
                "" & DBNameSpace & ".ISMPReportType.strReportType,  " & _
                "" & DBNameSpace & ".ISMPDocumentType.strDocumentType,  " & _
                "(strLastName|| ', ' ||strFirstName) as StaffResponsible,  " & _
                "" & DBNameSpace & ".ISMPREportInformation.datTestDateStart,  " & _
                "" & DBNameSpace & ".ISMPREportInformation.datReceivedDate,  " & _
                "(" & DBNameSpace & ".ISMPREportINformation.datReceivedDate - " & _
                "" & DBNameSpace & ".ISMPReportInformation.datTestDateEnd) as daysToReceived, " & _
                "case " & _
                "when strClosed <> 'True' then trunc(abs(sysdate - AIRBRANCH.ISMPREportInformation.datReceivedDate), 0) " & _
                "else trunc(abs(AIRBranch.ISMPReportInformation.datCompleteDate - AIRBranch.ISMPReportInformation.datReceivedDate)) " & _
                "End daysInAPB, " & _
                "case " & _
                "when " & DBNameSpace & ".ISMPReportInformation.datCompleteDate = '04-Jul-1776' then Null " & _
                "else " & DBNameSpace & ".ISMPReportInformation.datCompleteDate " & _
                "end datCompleteDate, " & _
                "" & DBNameSpace & ".ISMPREportInformation.strClosed,  " & _
                "" & DBNameSpace & ".LookUpISMPComplianceStatus.strComplianceStatus,  " & _
                "" & DBNameSpace & ".ISMPREportInformation.mmoCommentArea,  " & _
                "" & DBNameSpace & ".LookUpEPDUnits.strUnitDesc, " & _
                "" & DBNameSpace & ".ISMPTestLogLink.strTestLogNumber, " & _
                "strPreComplianceStatus " & _
                "From " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation,  " & _
                "" & DBNameSpace & ".LookUpCountyInformation, " & DBNameSpace & ".ISMPReportInformation, " & _
                "" & DBNameSpace & ".LookUpPollutants, " & DBNameSpace & ".ISMPReportType,  " & _
                "" & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".EPDUSerProfiles,  " & _
                "" & DBNameSpace & ".LookUpISMPComplianceStatus,  " & _
                "" & DBNameSpace & ".ISMPTestLogLink, " & DBNameSpace & ".ISMPWitnessingEng, " & _
                "" & DBNameSpace & ".LookUpEPDUnits " & _
                "where " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPREportInformation.strReferenceNumber  " & _
                "and " & DBNameSpace & ".ISMPMaster.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber " & _
                "and " & DBNameSpace & ".ISMPREportInformation.strReviewingEngineer = " & DBNameSpace & ".EPDUserProfiles.numUserID (+)  " & _
                "and " & DBNameSpace & ".ISMPREportINformation.strReportType = " & DBNameSpace & ".ISMPREportType.strKey " & _
                "and substr(" & DBNameSpace & ".ISMPMaster.strAIRSNumber, 5, 3) = " & DBNameSpace & ".LookupCountyInformation.strCountyCode  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strpollutant = " & DBNameSpace & ".LookUpPollutants.strPollutantCode (+)  " & _
                "and " & DBNameSpace & ".ISMPREportINformation.strDocumentType = " & DBNameSpace & ".ISMPDocumentType.strKey (+)  " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strComplianceStatus = " & DBNameSpace & ".LookUpISMPComplianceStatus.strComplianceKey (+) " & _
                "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPTestLogLink.strReferenceNumber (+) " & _
                "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPWitnessingEng.strReferenceNumber (+) " & _
                "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode " & _
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
                    SQLWhere = SQLWhere & " strReviewingEngineer = '" & UserGCode & "' ) "
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
                                SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPReportInformation.strWitnessingengineer = '" & clbWitnessingStaff.SelectedValue & "' Or " & _
                                                            " " & DBNameSpace & ".ISMPWitnessingEng.strWitnessingEngineer = '" & clbWitnessingStaff.SelectedValue & "' or "
                            End If
                        Next
                    End If
                    SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPReportInformation.strWitnessingengineer = '" & UserGCode & "' or strWitnessingengineer2 = '" & UserGCode & "' ) "
                Else
                    If clbWitnessingStaff.CheckedItems.Count > 0 Then
                        SQLWhere = SQLWhere & " and ( "
                        For x As Integer = 0 To clbWitnessingStaff.Items.Count - 1
                            If clbWitnessingStaff.GetItemChecked(x) = True Then
                                clbWitnessingStaff.SelectedIndex = x
                                SQLWhere = SQLWhere & "  " & DBNameSpace & ".ISMPReportInformation.strWitnessingengineer = '" & clbWitnessingStaff.SelectedValue & "' Or " & _
                                                            " " & DBNameSpace & ".ISMPWitnessingEng.strWitnessingEngineer = '" & clbWitnessingStaff.SelectedValue & "' or "
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
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '001' Or "
                        End If
                        If chbOneStackTwoRun.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '002' Or "
                        End If
                        If chbOneStackThreeRun.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '003' Or "
                        End If
                        If chbOneStackFourRun.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '004' Or "
                        End If
                        If chbTwoStackStandard.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '005' Or "
                        End If
                        If chbTwoStackDRE.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '006' Or "
                        End If
                        If chbLoadingRack.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '007' Or "
                        End If
                        If chbPondTreatment.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '008' Or "
                        End If
                        If chbGasConcentration.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '009' Or "
                        End If
                        If chbFlare.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '010' Or "
                        End If
                        If chbRata.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '011' Or "
                        End If
                        If chbMemorandumStandard.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '012' Or "
                        End If
                        If chbMemorandumToFile.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '013' Or "
                        End If
                        If chbMethod9Single.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '016' Or "
                        End If
                        If chbMethod9Multi.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '014' Or "
                        End If
                        If chbMethod22.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '015' Or "
                        End If
                        If chbPEMS.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '017' Or "
                        End If
                        If chbPTE.Checked = True Then
                            SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPDocumentType.strKey = '018' Or "
                        End If
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3)) & " ) "
                    End If
                End If

                If chbMonitorCertification.Checked = True Or chbPEMSDevelopment.Checked = True Or chbRATAandCEMS.Checked = True Or _
                     chbSourceTest.Checked = True Or chbOther.Checked = True Then

                    SQLWhere = SQLWhere & " and ( "

                    If chbMonitorCertification.Checked = True Then
                        SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPReportInformation. strReportType = '001' Or "
                    End If
                    If chbPEMSDevelopment.Checked = True Then
                        SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPReportInformation. strReportType = '002' Or "
                    End If
                    If chbRATAandCEMS.Checked = True Then
                        SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPReportInformation. strReportType = '003' Or "
                    End If
                    If chbSourceTest.Checked = True Then
                        SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPReportInformation. strReportType = '004' Or "
                    End If
                    If chbOther.Checked = True Then
                        SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPReportInformation. strReportType = '005' Or "
                    End If
                    SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3)) & " ) "

                End If

                If chbComplianceStatus1.Checked = True Or chbComplianceStatus2.Checked = True Or _
                    chbComplianceStatus3.Checked = True Or chbComplianceStatus4.Checked = True Or _
                        chbComplianceStatus5.Checked = True Then

                    SQLWhere = SQLWhere & " and ( "

                    If chbComplianceStatus1.Checked = True Then
                        SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPReportInformation.strComplianceStatus = '01' Or "
                    End If
                    If chbComplianceStatus2.Checked = True Then
                        SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPReportInformation.strComplianceStatus = '02' Or "
                    End If
                    If chbComplianceStatus3.Checked = True Then
                        SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPReportInformation.strComplianceStatus = '03' Or "
                    End If
                    If chbComplianceStatus4.Checked = True Then
                        SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPReportInformation.strComplianceStatus = '04' Or "
                    End If
                    If chbComplianceStatus5.Checked = True Then
                        SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPReportInformation.strComplianceStatus = '05' Or "
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
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".ISMPMaster.strAIRSnumber like '%" & txtAIRSNumberFilter.Text & "%' "
                End If
                If txtFacilityNameFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(" & DBNameSpace & ".APBFacilityInformation.strFacilityName) like Upper('%" & txtFacilityNameFilter.Text & "%') "
                End If
                If txtReferenceNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".ISMPMaster.strReferenceNumber like '%" & txtReferenceNumberFilter.Text & "%' "
                End If
                If txtNotificationNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".ISMPTestLogLink.strTestLogNumber like '%" & txtNotificationNumberFilter.Text & "%' "
                End If
                If txtCityFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and upper(" & DBNameSpace & ".APBFacilityInformation.strFacilityCity) like Upper('%" & txtCityFilter.Text & "%') "
                End If
                If txtCountyFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(" & DBNameSpace & ".LookUpCountyInformation.strCountyName) like Upper('%" & txtCountyFilter.Text & "%') "
                End If
                If txtEmissionSourceTestedFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(" & DBNameSpace & ".ISMPReportInformation.strEmissionSource) " & _
                                               "like upper('%" & txtEmissionSourceTestedFilter.Text & "%') "
                End If
                If txtCommentFieldFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(" & DBNameSpace & ".ISMPReportInformation.mmoCommentArea) " & _
                                               "like Upper('%" & txtCommentFieldFilter.Text & "%') "
                End If

                If txtPollutantFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(" & DBNameSpace & ".LookUpPollutants.strPollutantDescription) " & _
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
                daTestReportViewer = New OracleDataAdapter(SQL, Conn)

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
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
               "substr(" & DBNameSpace & ".ISMPTEstNotification.strAIRSNumber, 5) as AIRSNumber, " & _
               "" & DBNameSpace & ".APBFacilityInformation.strFacilityName,  " & _
               "strFacilityCity,  " & _
               "strCountyName,  " & _
               "to_number(" & DBNameSpace & ".ISMPTestNotification.strTestLogNumber) as strTestLogNumber,    " & _
               "strEmissionUnit,  datProposedStartDate,    " & _
               "(strLastName|| ', ' ||strFirstName) as StaffResponsible,   " & _
               "strUnitDesc, " & _
               "" & DBNameSpace & ".ISMPTestNotification.strcomments,  " & _
               "strReferenceNumber " & _
               "from " & DBNameSpace & ".ISMPTestNotification, " & DBNameSpace & ".ISMPTestLogLink,  " & _
               "" & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".APBFacilityINformation,  " & _
               "" & DBNameSpace & ".LookUpCountyInformation, " & DBNameSpace & ".LookUpEPDUnits  " & _
               "where  " & DBNameSpace & ".ISMPTestNotification.strTestLogNumber = " & DBNameSpace & ".ISMPTestLogLink.strTestLognumber (+)    " & _
               "and " & DBNameSpace & ".ISMPTestNotification.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+) " & _
               "and substr(" & DBNameSpace & ".ISMPTestNotification.strAIRSNumber, 5, 3) = " & DBNameSpace & ".LookUpCountyInformation.strcountycode (+)  " & _
               "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
               "and strStaffResponsible = " & DBNameSpace & ".EPDUserProfiles.numUserID (+)  "

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
                    SQLWhere = SQLWhere & " strStaffResponsible = '" & UserGCode & "' ) "
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
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".ISMPTestNotification.strAIRSnumber like '%" & txtAIRSNumberFilter.Text & "%' "
                End If
                If txtFacilityNameFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(" & DBNameSpace & ".APBFacilityInformation.strFacilityName) like Upper('%" & txtFacilityNameFilter.Text & "%') "
                End If
                If txtReferenceNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".ISMPTestLogLink.strReferenceNumber like '%" & txtReferenceNumberFilter.Text & "%' "
                End If
                If txtNotificationNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".ISMPTestNotification.strTestLogNumber like '%" & txtNotificationNumberFilter.Text & "%' "
                End If
                If txtCityFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and upper(" & DBNameSpace & ".APBFacilityInformation.strFacilityCity) like Upper('%" & txtCityFilter.Text & "%') "
                End If
                If txtCountyFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(" & DBNameSpace & ".LookUpCountyInformation.strCountyName) like Upper('%" & txtCountyFilter.Text & "%') "
                End If
                If txtEmissionSourceTestedFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(" & DBNameSpace & ".ISMPTestNotification.strEmissionUnit) " & _
                                               "like upper('%" & txtEmissionSourceTestedFilter.Text & "%') "
                End If
                If txtCommentFieldFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(" & DBNameSpace & ".ISMPTestNotification.mmoComments) " & _
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
                daNotificationViewer = New OracleDataAdapter(SQL, Conn)

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
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
               "substr(" & DBNameSpace & ".ISMPTestFirmComments.strAIRSNumber, 5) as AIRSNumber, " & _
               "strFacilityName, " & _
               "" & DBNameSpace & ".ISMPTestFirmComments.strTestLogNumber, strReferenceNumber, " & _
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
               "" & DBNameSpace & ".LookUpCountyInformation.strCountyName " & _
               "from " & DBNameSpace & ".ISMPTestFirmComments, " & DBNameSpace & ".LookUpTestingFirms, " & _
               "" & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".EPDUserProfiles,  " & _
               "" & DBNameSpace & ".ISMPTestNotification, " & DBNameSpace & ".LookUpCountyInformation " & _
               "where " & DBNameSpace & ".ISMPTestFirmComments.strTestingFirmKey = " & DBNameSpace & ".LooKUpTestingFirms.strTestingFirmKey " & _
               "and " & DBNameSpace & ".ISMPTestFirmComments.strAIRSnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+) " & _
               "and " & DBNameSpace & ".ISMPTestFirmComments.strStaffResponsible = " & DBNameSpace & ".EPDUserProfiles.numUserID (+) " & _
               "and substr(" & DBNameSpace & ".ISMPTestFirmComments.strAIRSNUmber, 5, 3)  = " & DBNameSpace & ".LookUpCountyInformation.strCountycode (+) " & _
               "and " & DBNameSpace & ".ismptestfirmcomments.strtestlognumber = " & DBNameSpace & ".ismptestnotification.strtestlognumber (+) "


                If chbReviewingEngineer.Checked = True Then
                    SQLWhere = SQLWhere & " and ( "
                    If clbEngineer.CheckedItems.Count > 0 Then
                        For x As Integer = 0 To clbEngineer.Items.Count - 1
                            If clbEngineer.GetItemChecked(x) = True Then
                                clbEngineer.SelectedIndex = x
                                SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPTestFirmComments.strStaffResponsible = '" & clbEngineer.SelectedValue & "' Or "
                            End If
                        Next
                    End If
                    SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPTestFirmComments.strStaffResponsible = '" & UserGCode & "' ) "
                Else
                    If clbEngineer.CheckedItems.Count > 0 Then
                        SQLWhere = SQLWhere & " and ( "
                        For x As Integer = 0 To clbEngineer.Items.Count - 1
                            If clbEngineer.GetItemChecked(x) = True Then
                                clbEngineer.SelectedIndex = x
                                SQLWhere = SQLWhere & " " & DBNameSpace & ".ISMPTestFirmComments.strStaffResponsible = '" & clbEngineer.SelectedValue & "' Or "
                            End If
                        Next
                        SQLWhere = Mid(SQLWhere, 1, (SQLWhere.Length - 3))
                        SQLWhere = SQLWhere & " ) "
                    End If
                End If

                If txtAIRSNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".ISMPTestFirmComments.strAIRSnumber like '%" & txtAIRSNumberFilter.Text & "%' "
                End If
                If txtFacilityNameFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(" & DBNameSpace & ".APBFacilityInformation.strFacilityName) like Upper('%" & txtFacilityNameFilter.Text & "%') "
                End If
                If txtReferenceNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".ISMPTestFirmComments.strReferenceNumber like '%" & txtReferenceNumberFilter.Text & "%' "
                End If
                If txtNotificationNumberFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and " & DBNameSpace & ".ISMPTestFirmComments.strTestLogNumber like '%" & txtNotificationNumberFilter.Text & "%' "
                End If
                If txtCityFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and upper(" & DBNameSpace & ".APBFacilityInformation.strFacilityCity) like Upper('%" & txtCityFilter.Text & "%') "
                End If
                If txtCountyFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(" & DBNameSpace & ".LookUpCountyInformation.strCountyName) like Upper('%" & txtCountyFilter.Text & "%') "
                End If
                If txtEmissionSourceTestedFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(" & DBNameSpace & ".ISMPTestNotification.strEmissionUnit) " & _
                                               "like upper('%" & txtEmissionSourceTestedFilter.Text & "%') "
                End If
                If txtCommentFieldFilter.Text <> "" Then
                    SQLWhere = SQLWhere & " and Upper(" & DBNameSpace & ".ISMPTestFirmComments.strComments) " & _
                                               "like Upper('%" & Replace(txtCommentFieldFilter.Text, "'", "''") & "%') "
                End If

                If txtTestingFirm.Text <> "" Then
                    SQLWhere = SQLWhere & "and Upper(" & DBNameSpace & ".LookUpTestingFirms.strTestingFirm) " & _
                                                "like Upper('%" & Replace(txtTestingFirm.Text, "'", "''") & "%') "
                End If

                SQL = SQL & SQLWhere

                dsTestFirmComments = New DataSet
                daTestFirmComments = New OracleDataAdapter(SQL, Conn)

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
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
            ErrorReport(SQL & vbCrLf & ex.ToString(), "ISMPTestReportViewer.LoadDataSet")
        Finally

        End Try


    End Sub
#End Region
    Sub LoadTestLogData()
        Try

            If txtTestLogNumber.Text <> "" Then
                SQL = "Select " & _
                "strTestLogNumber,  " & _
                "" & DBNameSpace & ".APBFacilityInformation.strFacilityName,  " & _
                "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,  " & _
                "strEmissionUnit,  " & _
                "" & DBNameSpace & ".APBFacilityInformation.strFacilityCity,  " & _
                "strCountyName,  " & _
                "datProposedStartDate  " & _
                "from " & DBNameSpace & ".APBFacilityINformation, " & DBNameSpace & ".ISMPTestNotification,  " & _
                "" & DBNameSpace & ".LookUpCountyInformation  " & _
                "where '0413'||" & DBNameSpace & ".ISMPTestNotification.strAIRSnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+) " & _
                "and substr(" & DBNameSpace & ".ISMPTestNotification.strAIRSNumber, 1, 3) = " & DBNameSpace & ".LookUpCountyInformation.strCountyCode (+)  " & _
                "and strTestLogNumber = '" & txtTestLogNumber.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
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

            'If txtReferenceNumber.Text <> "" Then
            '    SQL = "select " & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
            '     "from " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPReportInformation " & _
            '     "where " & DBNameSpace & ".ISMPReportInformation.strDocumentType = " & DBNameSpace & ".ISMPDocumentType.strKey and " & _
            '     "strReferenceNumber = '" & txtReferenceNumber.Text & "'"
            '    Dim cmd As New OracleCommand(SQL, conn)
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If
            '    Dim dr As OracleDataReader = cmd.ExecuteReader
            '    Dim recExist As Boolean = dr.Read
            '    If recExist = True Then
            '        ISMPTestReportsEntry = Nothing
            '        If ISMPTestReportsEntry Is Nothing Then ISMPTestReportsEntry = New ISMPTestReports
            '        ISMPTestReportsEntry.txtReferenceNumber.Text = txtReferenceNumber.Text
            '        ISMPTestReportsEntry.Show()
            '        ISMPTestReportsEntry.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            '    End If
            'End If
            If txtReferenceNumber.Text <> "" Then
                If UserProgram = "3" Then
                    SQL = "select " & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                    "from " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPReportInformation " & _
                    "where " & DBNameSpace & ".ISMPReportInformation.strDocumentType = " & DBNameSpace & ".ISMPDocumentType.strKey and " & _
                    "strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        ISMPTestReportsEntry = Nothing
                        If ISMPTestReportsEntry Is Nothing Then ISMPTestReportsEntry = New ISMPTestReports
                        ISMPTestReportsEntry.txtReferenceNumber.Text = txtReferenceNumber.Text
                        ISMPTestReportsEntry.Show()
                        'ISMPTestReportsEntry.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    End If
                Else
                    SQL = "Select strClosed " & _
                    "from " & DBNameSpace & ".ISMPReportInformation " & _
                    "where strReferenceNumber = '" & txtReferenceNumber.Text & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        temp = dr.Item("strClosed")
                    End While
                    If temp = "True" Then
                        PrintOut = Nothing
                        If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
                        PrintOut.txtReferenceNumber.Text = txtReferenceNumber.Text
                        PrintOut.txtPrintType.Text = "SSCP"
                        PrintOut.Show()
                        'PrintOut.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    Else
                        MsgBox("This Test Summary has not been completely reviewed by ISMP Engineer", MsgBoxStyle.Information, "Facility Summary")
                    End If

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

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

            If NavigationScreen Is Nothing Then
                NavigationScreen = New IAIPNavigation
            End If
            NavigationScreen.Show()
            ISMPReportViewer = Nothing
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
        Try

            Help.ShowHelp(Label1, HelpUrl)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

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
            'DevTestLog.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
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
            'TestFirmComments.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Try

            If NavigationScreen Is Nothing Then
                NavigationScreen = New IAIPNavigation
            End If
            NavigationScreen.Show()
            ISMPReportViewer = Nothing
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
            If facilityLookupDialog.DialogResult = Windows.Forms.DialogResult.OK _
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
        Try
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim i, j As Integer


            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvTestReportViewer.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvTestReportViewer.ColumnCount - 1
                        .Cells(1, i + 1) = dgvTestReportViewer.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvTestReportViewer.ColumnCount - 1
                        For j = 0 To dgvTestReportViewer.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvTestReportViewer.Item(i, j).Value.ToString
                        Next
                    Next

                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
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
            'StaffReports.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

   
End Class