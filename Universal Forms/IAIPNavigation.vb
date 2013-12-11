Imports Oracle.DataAccess.Client
Imports System.IO
Imports System.Collections.Generic

Public Class IAIPNavigation
    Dim Paneltemp1 As String
    Public UserSource As String
    Public UserRequest As String
    Public UserGridStyle As String
    Dim dsOpenWork As DataSet
    Dim daOpenWork As OracleDataAdapter
    Dim AccountAccess As String = ""
    Dim WorkBranch As String
    Dim WorkProgram As String
    Dim WorkUnit As String

    Private Sub IAIPNavigation_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        monitor.TrackFeatureStop("Startup.LoggingIn")
    End Sub
    Private Sub APBNavigation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Main." & Me.Name)
        Try
            pnl3.Text = OracleDate
            pnl2.Text = UserName
            Paneltemp1 = pnl1.Text
            ProgressBar.PerformStep()

            WorkBranch = UserBranch
            WorkProgram = UserProgram
            WorkUnit = UserUnit

            bgrFormLoad.WorkerReportsProgress = True
            bgrFormLoad.WorkerSupportsCancellation = True
            bgrFormLoad.RunWorkerAsync()

            LoadShortCutData()

            ProgressBar.Value = 0
            IAIPLogIn.Hide()

            'GetDefaultLocation()

            cboIAIPList.Items.Add("Compliance Facilities Assigned")
            cboIAIPList.Items.Add("Compliance Work")
            cboIAIPList.Items.Add("Full Compliance Evaluations - Delinquent")
            cboIAIPList.Items.Add("Enforcement")
            cboIAIPList.Items.Add("Facilities with Subparts")
            cboIAIPList.Items.Add("Facilities missing Subparts")
            cboIAIPList.Items.Add("Monitoring Test Reports")
            cboIAIPList.Items.Add("Monitoring Test Notifications")
            cboIAIPList.Items.Add("Permit Applications")

            If TestingEnvironment Then
                mmiTesting.Visible = True
                mmiTesting.Enabled = True
            Else
                mmiTesting.Visible = False
                mmiTesting.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#Region "Page Load Subs and Funcations"
    Sub LoadShortCutData()
        Try

            ProgressBar.PerformStep()

            llbPrimaryList.Visible = False
            llbSecondaryList.Visible = False
            llbTertiaryList.Visible = False
            llbQuaternaryList.Visible = False

            ProgressBar.PerformStep()
            If WorkProgram <> "---" Then
                SQL2 = "Select strProgramDesc " & _
                "from AIRBranch.LookUpEPDPrograms " & _
                "where numProgramCode = '" & WorkProgram & "' "
                cmd2 = New OracleCommand(SQL2, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd2.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("strProgramDesc")) Then
                        pnl1.Text = "---"
                    Else
                        pnl1.Text = dr2.Item("strProgramDesc")
                    End If
                End While
                dr2.Close()
            Else
                pnl1.Text = "---"
            End If

            llbPrimaryList.Visible = False
            llbSecondaryList.Visible = False
            llbTertiaryList.Visible = False
            llbQuaternaryList.Visible = False

            Select Case WorkBranch
                Case "1"
                    Select Case WorkProgram
                        Case "1"

                        Case "2"

                        Case "3"    'ISMP 
                            llbPrimaryList.Visible = True
                            llbSecondaryList.Visible = True
                            llbTertiaryList.Visible = True
                            llbPrimaryList.Text = "Test Reports"
                            llbSecondaryList.Text = "Permit Applications"
                            llbTertiaryList.Text = "Test Notifications"

                        Case "4"    'SSCP 
                            llbPrimaryList.Visible = True
                            llbSecondaryList.Visible = True
                            llbTertiaryList.Visible = True
                            llbQuaternaryList.Visible = True
                            llbPrimaryList.Text = "Open Enforcement"
                            llbSecondaryList.Text = "Permit Applications"
                            llbTertiaryList.Text = "Open Compliance Work"
                            llbQuaternaryList.Text = "MACT Sub Parts"
                        Case "5"     'SSPP
                            llbPrimaryList.Visible = True
                            llbPrimaryList.Text = "Open Applications"
                        Case "6"
                        Case Else
                            If Permissions.Contains("(27)") Then
                                llbPrimaryList.Visible = True
                                llbSecondaryList.Visible = True
                                llbTertiaryList.Visible = True
                                llbQuaternaryList.Visible = True
                                llbPrimaryList.Text = "Open Enforcement"
                                llbSecondaryList.Text = "Permit Applications"
                                llbTertiaryList.Text = "Open Compliance Work"
                                llbQuaternaryList.Text = "MACT Sub Parts"
                            End If

                    End Select
                Case "5"
                    llbPrimaryList.Visible = True
                    llbSecondaryList.Visible = True
                    llbPrimaryList.Text = "Open Enforcement"
                    llbSecondaryList.Text = "Open Compliance Work"
            End Select

            ProgressBar.Value = 0
            lblMessageLabel.Text = "Loading...."
            dgvWorkViewer.Visible = False
            UserRequest = "Primary"

            If bgrLongProcess.IsBusy = True Then
                bgrLongProcess.CancelAsync()
            Else
                bgrLongProcess.WorkerReportsProgress = True
                bgrLongProcess.WorkerSupportsCancellation = True
                bgrLongProcess.RunWorkerAsync()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadISMPTestReports()
        If SQL <> "" Then
            dgvWorkViewer.DataSource = dsOpenWork
            dgvWorkViewer.DataMember = "OpenWork"

            dgvWorkViewer.RowHeadersVisible = False
            dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvWorkViewer.AllowUserToResizeColumns = True
            dgvWorkViewer.AllowUserToAddRows = False
            dgvWorkViewer.AllowUserToDeleteRows = False
            dgvWorkViewer.AllowUserToOrderColumns = True
            dgvWorkViewer.AllowUserToResizeRows = True
            dgvWorkViewer.ColumnHeadersHeight = "35"
            dgvWorkViewer.Columns("strReferenceNumber").HeaderText = "Reference #"
            dgvWorkViewer.Columns("strReferenceNumber").DisplayIndex = 0
            dgvWorkViewer.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvWorkViewer.Columns("AIRSNumber").DisplayIndex = 1
            dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 2
            dgvWorkViewer.Columns("strFacilityCity").HeaderText = "City"
            dgvWorkViewer.Columns("strFacilityCity").DisplayIndex = 3
            dgvWorkViewer.Columns("strCountyName").HeaderText = "County"
            dgvWorkViewer.Columns("strCountyName").DisplayIndex = 4
            dgvWorkViewer.Columns("strEmissionSource").HeaderText = "Emission Source"
            dgvWorkViewer.Columns("strEmissionSource").DisplayIndex = 5
            dgvWorkViewer.Columns("strPollutantDescription").HeaderText = "Pollutant"
            dgvWorkViewer.Columns("strPollutantDescription").DisplayIndex = 6
            dgvWorkViewer.Columns("strReportType").HeaderText = "Report Type"
            dgvWorkViewer.Columns("strReportType").DisplayIndex = 7
            dgvWorkViewer.Columns("strDocumentType").HeaderText = "Document Type"
            dgvWorkViewer.Columns("strDocumentType").DisplayIndex = 8
            dgvWorkViewer.Columns("ReviewingEngineer").HeaderText = "Reviewing Engineer"
            dgvWorkViewer.Columns("ReviewingEngineer").DisplayIndex = 9
            dgvWorkViewer.Columns("TestDateStart").HeaderText = "Test Date"
            dgvWorkViewer.Columns("TestDateStart").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWorkViewer.Columns("TestDateStart").DisplayIndex = 10
            dgvWorkViewer.Columns("ReceivedDate").HeaderText = "Received Date"
            dgvWorkViewer.Columns("ReceivedDate").DisplayIndex = 11
            dgvWorkViewer.Columns("ReceivedDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWorkViewer.Columns("CompleteDate").HeaderText = "Complete Date"
            dgvWorkViewer.Columns("CompleteDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWorkViewer.Columns("CompleteDate").DisplayIndex = 12
            dgvWorkViewer.Columns("Status").HeaderText = "Report Open/Closed"
            dgvWorkViewer.Columns("Status").DisplayIndex = 13
            dgvWorkViewer.Columns("strComplianceStatus").HeaderText = "Compliance Status"
            dgvWorkViewer.Columns("strComplianceStatus").DisplayIndex = 14
            dgvWorkViewer.Columns("mmoCommentAREA").HeaderText = "Comment Field"
            dgvWorkViewer.Columns("mmoCommentAREA").DisplayIndex = 15
            dgvWorkViewer.Columns("strPreComplianceStatus").HeaderText = "Precompliance Status"
            dgvWorkViewer.Columns("strPreComplianceStatus").DisplayIndex = 16
            dgvWorkViewer.Columns("strWitnessingEngineer").Visible = False
            dgvWorkViewer.Columns("strWitnessingEngineer2").Visible = False
            dgvWorkViewer.Columns("strUserUnit").Visible = False

            LoadCompliaceColor()
        End If
    End Sub
    Sub LoadCompliaceColor()
        Try
            For Each row As DataGridViewRow In dgvWorkViewer.Rows
                If Not row.IsNewRow Then
                    If Not row.Cells(19).Value Is DBNull.Value Then
                        temp = row.Cells(19).Value
                        If row.Cells(19).Value = "True" Then
                            row.DefaultCellStyle.BackColor = Color.Pink
                        End If
                    End If
                    If Not row.Cells(13).Value Is DBNull.Value Then
                        temp = row.Cells(13).Value
                        If row.Cells(13).Value = "Not In Compliance" Then
                            row.DefaultCellStyle.BackColor = Color.Tomato
                        End If
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Sub LoadSecondaryWork2()

        If SQL <> "" Then
            dsOpenWork = New DataSet
            daOpenWork = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daOpenWork.Fill(dsOpenWork, "OpenWork")
            dgvWorkViewer.DataSource = dsOpenWork
            dgvWorkViewer.DataMember = "OpenWork"

            dgvWorkViewer.RowHeadersVisible = False
            dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvWorkViewer.AllowUserToResizeColumns = True
            dgvWorkViewer.AllowUserToAddRows = False
            dgvWorkViewer.AllowUserToDeleteRows = False
            dgvWorkViewer.AllowUserToOrderColumns = True
            dgvWorkViewer.AllowUserToResizeRows = True
            dgvWorkViewer.ColumnHeadersHeight = "35"
            dgvWorkViewer.Columns("strApplicationNumber").HeaderText = "APL #"
            dgvWorkViewer.Columns("strApplicationNumber").DisplayIndex = 0
            dgvWorkViewer.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvWorkViewer.Columns("AIRSNumber").DisplayIndex = 1
            dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 2
            dgvWorkViewer.Columns("ReviewSubmitted").HeaderText = "Date Submitted"
            dgvWorkViewer.Columns("ReviewSubmitted").DisplayIndex = 3
            dgvWorkViewer.Columns("StaffResponsible").HeaderText = "Permitting Staff"
            dgvWorkViewer.Columns("StaffResponsible").DisplayIndex = 4
        End If


    End Sub
    Sub LoadSSCPOpenWork()
        If SQL <> "" Then

            dgvWorkViewer.DataSource = dsOpenWork
            dgvWorkViewer.DataMember = "OpenWork"

            dgvWorkViewer.RowHeadersVisible = False
            dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvWorkViewer.AllowUserToResizeColumns = True
            dgvWorkViewer.AllowUserToAddRows = False
            dgvWorkViewer.AllowUserToDeleteRows = False
            dgvWorkViewer.AllowUserToOrderColumns = True
            dgvWorkViewer.AllowUserToResizeRows = True
            dgvWorkViewer.ColumnHeadersHeight = "35"
            dgvWorkViewer.Columns("strEnforcementNumber").HeaderText = "Enforcement #"
            dgvWorkViewer.Columns("strEnforcementNumber").DisplayIndex = 0
            dgvWorkViewer.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvWorkViewer.Columns("AIRSNumber").DisplayIndex = 1
            dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 2
            dgvWorkViewer.Columns("EnforcementStatus").HeaderText = "Enforcement Status"
            dgvWorkViewer.Columns("EnforcementStatus").DisplayIndex = 3
            dgvWorkViewer.Columns("Violationdate").HeaderText = "Discovery Date"
            dgvWorkViewer.Columns("Violationdate").DisplayIndex = 4
            dgvWorkViewer.Columns("Violationdate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWorkViewer.Columns("HPVstatus").HeaderText = "Status"
            dgvWorkViewer.Columns("HPVstatus").DisplayIndex = 5
            dgvWorkViewer.Columns("Status").HeaderText = "Open/Closed"
            dgvWorkViewer.Columns("Status").DisplayIndex = 6
            dgvWorkViewer.Columns("Staff").HeaderText = "Staff Responsible"
            dgvWorkViewer.Columns("Staff").DisplayIndex = 7
        End If
    End Sub
    Sub LoadSSPPOpenWork()
        If SQL <> "" Then
            dgvWorkViewer.DataSource = dsOpenWork
            dgvWorkViewer.DataMember = "OpenWork"

            dgvWorkViewer.RowHeadersVisible = False
            dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvWorkViewer.AllowUserToResizeColumns = True
            dgvWorkViewer.AllowUserToAddRows = False
            dgvWorkViewer.AllowUserToDeleteRows = False
            dgvWorkViewer.AllowUserToOrderColumns = True
            dgvWorkViewer.AllowUserToResizeRows = True
            dgvWorkViewer.ColumnHeadersHeight = "35"
            dgvWorkViewer.Columns("strApplicationNumber").HeaderText = "APL #"
            dgvWorkViewer.Columns("strApplicationNumber").DisplayIndex = 0
            dgvWorkViewer.Columns("strAIRSNumber").HeaderText = "AIRS #"
            dgvWorkViewer.Columns("strAIRSNumber").DisplayIndex = 1
            dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 2
            dgvWorkViewer.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvWorkViewer.Columns("StaffResponsible").DisplayIndex = 3
            dgvWorkViewer.Columns("strApplicationType").HeaderText = "APL Type"
            dgvWorkViewer.Columns("strApplicationType").DisplayIndex = 4
            dgvWorkViewer.Columns("datReceivedDate").HeaderText = "APL Rcvd"
            dgvWorkViewer.Columns("datReceivedDate").DisplayIndex = 5
            dgvWorkViewer.Columns("datReceivedDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWorkViewer.Columns("strPermitNumber").HeaderText = "Permit Number"
            dgvWorkViewer.Columns("strPermitNumber").DisplayIndex = 6
            dgvWorkViewer.Columns("AppStatus").HeaderText = "App Status"
            dgvWorkViewer.Columns("AppStatus").DisplayIndex = 8
            dgvWorkViewer.Columns("StatusDate").HeaderText = "Status Date"
            dgvWorkViewer.Columns("StatusDate").DisplayIndex = 9
            dgvWorkViewer.Columns("strPermitType").HeaderText = "Action Type"
            dgvWorkViewer.Columns("strPermitType").DisplayIndex = 7

        End If
    End Sub
    Sub LoadSSCPTertiaryWork()

        If SQL <> "" Then
            dsOpenWork = New DataSet
            daOpenWork = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daOpenWork.Fill(dsOpenWork, "OpenWork")
            dgvWorkViewer.DataSource = dsOpenWork
            dgvWorkViewer.DataMember = "OpenWork"

            dgvWorkViewer.RowHeadersVisible = False
            dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvWorkViewer.AllowUserToResizeColumns = True
            dgvWorkViewer.AllowUserToAddRows = False
            dgvWorkViewer.AllowUserToDeleteRows = False
            dgvWorkViewer.AllowUserToOrderColumns = True
            dgvWorkViewer.AllowUserToResizeRows = True
            dgvWorkViewer.ColumnHeadersHeight = "35"
            dgvWorkViewer.Columns("strTrackingNumber").HeaderText = "Tracking #"
            dgvWorkViewer.Columns("strTrackingNumber").DisplayIndex = 0
            dgvWorkViewer.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvWorkViewer.Columns("AIRSNumber").DisplayIndex = 1
            dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 2
            dgvWorkViewer.Columns("DateReceived").HeaderText = "Date Received"
            dgvWorkViewer.Columns("DateReceived").DisplayIndex = 3
            dgvWorkViewer.Columns("DateReceived").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWorkViewer.Columns("Staff").HeaderText = "Staff Responsible"
            dgvWorkViewer.Columns("Staff").DisplayIndex = 4
            dgvWorkViewer.Columns("StrActivityName").HeaderText = "Activity Type"
            dgvWorkViewer.Columns("StrActivityName").DisplayIndex = 5
            dgvWorkViewer.Columns("strResponsibleStaff").HeaderText = "Responsible Staff"
            dgvWorkViewer.Columns("strResponsibleStaff").DisplayIndex = 6
            dgvWorkViewer.Columns("strResponsibleStaff").Visible = False

        End If

    End Sub
    Sub LoadISMPTertiaryWork()
        If SQL <> "" Then
            dgvWorkViewer.DataSource = dsOpenWork
            dgvWorkViewer.DataMember = "OpenWork"

            dgvWorkViewer.RowHeadersVisible = False
            dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvWorkViewer.AllowUserToResizeColumns = True
            dgvWorkViewer.AllowUserToAddRows = False
            dgvWorkViewer.AllowUserToDeleteRows = False
            dgvWorkViewer.AllowUserToOrderColumns = True
            dgvWorkViewer.AllowUserToResizeRows = True
            dgvWorkViewer.ColumnHeadersHeight = "35"
            dgvWorkViewer.Columns("TestNumber").HeaderText = "Test Log #"
            dgvWorkViewer.Columns("TestNumber").DisplayIndex = 0
            dgvWorkViewer.Columns("RefNum").HeaderText = "Reference #"
            dgvWorkViewer.Columns("RefNum").DisplayIndex = 1
            dgvWorkViewer.Columns("AIRSNumber").HeaderText = "AIRS #"
            dgvWorkViewer.Columns("AIRSNumber").DisplayIndex = 2
            dgvWorkViewer.Columns("FacilityName").HeaderText = "Facility Name"
            dgvWorkViewer.Columns("FacilityName").DisplayIndex = 3
            dgvWorkViewer.Columns("strEmissionUnit").HeaderText = "Emission Unit"
            dgvWorkViewer.Columns("strEmissionUnit").DisplayIndex = 4
            dgvWorkViewer.Columns("ProposedStartDate").HeaderText = "Start Date"
            dgvWorkViewer.Columns("ProposedStartDate").DisplayIndex = 5
            dgvWorkViewer.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvWorkViewer.Columns("StaffResponsible").DisplayIndex = 6

        End If
    End Sub
    Sub LoadSSCPQuaternaryWork()
        If SQL <> "" Then
            dgvWorkViewer.DataSource = dsOpenWork
            dgvWorkViewer.DataMember = "OpenWork"

            dgvWorkViewer.RowHeadersVisible = False
            dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvWorkViewer.AllowUserToResizeColumns = True
            dgvWorkViewer.AllowUserToAddRows = False
            dgvWorkViewer.AllowUserToDeleteRows = False
            dgvWorkViewer.AllowUserToOrderColumns = True
            dgvWorkViewer.AllowUserToResizeRows = True
            dgvWorkViewer.ColumnHeadersHeight = "35"
            dgvWorkViewer.Columns("AIRSnumber").HeaderText = "AIRS #"
            dgvWorkViewer.Columns("AIRSnumber").DisplayIndex = 0
            dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 1
        End If
    End Sub
    Sub LoadOpenWork(ByVal WorkBranch As String, ByVal WorkProgram As String, ByVal WorkUnit As String)
        Try
            SQL = ""
            dsOpenWork = New DataSet
            UserRequest = "Primary"
            Select Case WorkBranch
                Case "1" 'Air Protection Branch
                    Select Case WorkProgram
                        Case "1" 'Mobile & Area

                        Case "2" 'Planning & Support

                        Case "3" 'ISMP
                            If WorkUnit = "---" Then 'Program Manager
                                UserGridStyle = "LoadISMPTestReports"
                                SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " & _
                                "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " & _
                                "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " & _
                                  "AIRBranch.ISMPReportInformation.strReferenceNumber  " & _
                                "and  Status = 'Open' "
                            Else
                                If AccountArray(17, 2) = "1" Then  'Unit Manager
                                    UserGridStyle = "LoadISMPTestReports"
                                    SQL = "select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " & _
                                    "from AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " & _
                                    "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " & _
                                     "AIRBranch.ISMPReportInformation.strReferenceNumber  " & _
                                    "and status = 'Open' " & _
                                    "and strUserUnit = " & _
                                    "(select strUnitDesc from AIRBranch.LookUpEPDUnits where numUnitCode = '" & UserUnit & "') "
                                Else
                                    UserGridStyle = "LoadISMPTestReports"
                                    SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " & _
                                    "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " & _
                                    "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " & _
                                     "AIRBranch.ISMPReportInformation.strReferenceNumber  " & _
                                    "and Status = 'Open' " & _
                                    "and ReviewingEngineer = '" & pnl2.Text & "' "
                                End If
                            End If
                        Case "4" 'SSCP
                            If WorkUnit = "---" Then 'Program Manager
                                UserGridStyle = "LoadSSCPOpenWork"
                                SQL = "Select " & _
                                "distinct(to_number(AIRBranch.sscp_AuditedEnforcement.strEnforcementNumber)) as strEnforcementNumber,  " & _
                                "substr(AIRBranch.sscp_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,   " & _
                                "case   " & _
                                "when datEnforcementFinalized is Not Null then '4 - Closed Out'   " & _
                                "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " & _
                                "when strStatus = 'UC' then '2 - Submitted to UC'   " & _
                                "When strStatus Is Null then '1 - At Staff'   " & _
                                "else 'Unknown'   " & _
                                "end as EnforcementStatus,   " & _
                                "Case     " & _
                                "when datDiscoveryDate is Null then ''    " & _
                                "else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                                "END as Violationdate,     " & _
                                "strActionType as HPVStatus,    " & _
                                "Case    " & _
                                "when datEnforcementFinalized Is Not NULL then 'Closed'    " & _
                                "when datEnforcementFinalized is NUll then 'Open'    " & _
                                "Else 'Open'    " & _
                                "End as Status,    " & _
                                "strFacilityName,    " & _
                                "(strLastName||', '||strFirstName) as Staff     " & _
                                "from AIRBranch.sscp_AuditedEnforcement,     " & _
                                "AIRBranch.APBFacilityInformation, AIRBranch.EPDUserProfiles,    " & _
                                "(select numUserID  " & _
                                "from AIRBranch.EPDUserProfiles where numUnit is null) UnitStaff " & _
                                "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.sscp_AuditedEnforcement.strAIRSNumber    " & _
                                "and (strStatus IS Null or strStatus = 'UC')    " & _
                                "and datEnforcementFinalized is NULL   " & _
                                "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.sscp_AuditedEnforcement.numStaffResponsible    " & _
                                "order by strENforcementNumber DESC   "
                            Else
                                If AccountArray(22, 3) = "1" Then 'Unit Manager
                                    UserGridStyle = "LoadSSCPOpenWork"
                                    SQL = "Select to_number(AIRBranch.SSCP_aUDITEDEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                                     "substr(AIRBranch.SSCP_aUDITEDEnforcement.strAIRSNumber, 5) as AIRSNumber,   " & _
                                     "case   " & _
                                     "    when datEnforcementFinalized is Not Null then '4 - Closed Out'   " & _
                                     "    when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " & _
                                     "    when strStatus = 'UC' then '2 - Submitted to UC'   " & _
                                     "    When strStatus Is Null then '1 - At Staff'   " & _
                                     "   else 'Unknown'   " & _
                                     "end as EnforcementStatus, " & _
                                    " Case    " & _
                                    " 	when datDiscoveryDate is Null then ''   " & _
                                    " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                                     "END as Violationdate,    " & _
                                     "case    " & _
                                     " 	when strHPV IS NULL then strActionType   " & _
                                     "	When strHPV IS Not Null then 'HPV'    " & _
                                     "   Else 'HPV'   " & _
                                     "END as HPVStatus,   " & _
                                     "Case   " & _
                                     " 	when datEnforcementFinalized Is Not NULL then 'Closed'   " & _
                                     "	when datEnforcementFinalized is NUll then 'Open'   " & _
                                     "Else 'Open'   " & _
                                     "End as Status,   " & _
                                     "strFacilityName,   " & _
                                     "(strLastName||', '||strFirstName) as Staff   " & _
                                     "from AIRBranch.SSCP_aUDITEDEnforcement,    " & _
                                     "AIRBranch.APBFacilityInformation, AIRBranch.EPDUserProfiles,   " & _
                                     "( select numUserID from AIRBranch.EPDUserProfiles where numUnit = '" & UserUnit & "'  " & _
                                     "group by numUserID ) UnitStaff   " & _
                                     "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_aUDITEDEnforcement.strAIRSNumber   " & _
                                     "and (strStatus IS Null or strStatus = 'UC')   " & _
                                     "and numStaffResponsible = UnitStaff.numUserID   " & _
                                     "and datEnforcementFinalized is NULL   " & _
                                     "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCP_aUDITEDEnforcement.numStaffResponsible   " & _
                                     "order by strENforcementNumber DESC  "
                                Else
                                    If AccountArray(10, 3) = "1" Then 'District Liason
                                        UserGridStyle = "LoadSSCPOpenWork"
                                        SQL = "Select to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                                        "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,   " & _
                                        "case   " & _
                                        "when datEnforcementFinalized is Not Null then '4 - Closed Out'   " & _
                                        "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " & _
                                        "when strStatus = 'UC' then '2 - Submitted to UC'   " & _
                                        "When strStatus Is Null then '1 - At Staff'   " & _
                                        "   else 'Unknown'   " & _
                                        "end as EnforcementStatus, " & _
                                        "Case    " & _
                                        " 	when datDiscoveryDate is Null then ''   " & _
                                        " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                                        "END as Violationdate,    " & _
                                        "case    " & _
                                        " 	when strHPV IS NULL then strActionType   " & _
                                        " 	When strHPV IS Not Null then 'HPV'    " & _
                                        "   Else 'HPV'   " & _
                                        "END as HPVStatus,   " & _
                                        "Case   " & _
                                        " 	when datEnforcementFinalized Is Not NULL then 'Closed'   " & _
                                        "	when datEnforcementFinalized is NUll then 'Open'   " & _
                                        "Else 'Open'   " & _
                                        "End as Status,   " & _
                                        "strFacilityName,   " & _
                                        "(strLastName||', '||strFirstName) as Staff   " & _
                                        "from AIRBranch.SSCP_AuditedEnforcement,  " & _
                                        "AIRBranch.APBFacilityInformation, AIRBranch.EPDUSerProfiles,   " & _
                                        "(select numuserId  " & _
                                        "from AIRBranch.EPDUserProfiles  " & _
                                        "where strLastName = 'District' or (numBranch = '1' and numProgram = '4' and numUnit is null )  " & _
                                        "group by numUserID) UnitStaff   " & _
                                        "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber   " & _
                                        "and (strStatus IS Null or strStatus = 'UC')   " & _
                                        "and numStaffResponsible = UnitStaff.numUserID   " & _
                                        "and datEnforcementFinalized is NULL   " & _
                                        "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCP_AuditedEnforcement.numStaffResponsible   " & _
                                        "order by strENforcementNumber DESC   "
                                    Else
                                        UserGridStyle = "LoadSSCPOpenWork"
                                        SQL = "Select to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                                     "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " & _
                                     "case  " & _
                                     "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " & _
                                     "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " & _
                                     "when strStatus = 'UC' then '2 - Submitted to UC'  " & _
                                     "When strStatus Is Null then '1 - At Staff'  " & _
                                     "else 'Unknown'  " & _
                                     "end as EnforcementStatus,  " & _
                                     "Case   " & _
                                     " 	when datDiscoveryDate is Null then ''  " & _
                                     "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                                     "END as Violationdate,   " & _
                                     "case   " & _
                                     "	when strHPV IS NULL then strActionType  " & _
                                     "	When strHPV IS Not Null then 'HPV'   " & _
                                     "Else 'HPV'  " & _
                                     "END as HPVStatus,  " & _
                                     "Case  " & _
                                     " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " & _
                                     " 	when datEnforcementFinalized is NUll then 'Open'  " & _
                                     "Else 'Open'  " & _
                                     "End as Status,  " & _
                                     "AIRBranch.APBFacilityInformation.strFacilityName,  " & _
                                     "(strLastName||', '||strFirstName) as Staff  " & _
                                     "from AIRBranch.SSCP_AuditedEnforcement,   " & _
                                     "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " & _
                                     "AIRBranch.VW_SSCPINSPECTION_LIST " & _
                                     "Where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber  " & _
                                     "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = '0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " & _
                                     "and (numStaffResponsible = '" & UserGCode & "' or numSSCPEngineer = '" & UserGCode & "')  " & _
                                     "and (strStatus IS Null or strStatus = 'UC')  " & _
                                     "and datEnforcementFinalized is Null  " & _
                                     "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " & _
                                     "order by strENforcementNumber DESC  "

                                    End If
                                End If
                            End If
                        Case "5" 'SSPP
                            If AccountArray(3, 3) = "1" And WorkUnit = "---" Then  'Program Manager
                                ' If UserUnit = "---" Then 
                                UserGridStyle = "LoadSSPPOpenWork"
                                SQL = "Select " & _
                              "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber, " & _
                              "case " & _
                              " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' ' " & _
                              "	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' ' " & _
                              "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5) " & _
                              "end as strAIRSNumber, " & _
                              "case " & _
                              "	when strApplicationTypeDesc IS Null then ' ' " & _
                              "Else strApplicationTypeDesc " & _
                              "End as strApplicationType, " & _
                              "case " & _
                              " 	when datReceivedDate is Null then ' ' " & _
                              "Else to_char(datReceivedDate, 'RRRR-MM-dd') " & _
                              " End as datReceivedDate, " & _
                              "case  " & _
                              "when strPermitNumber is NULL then ' '  " & _
                              "else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'  " & _
                              " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-' " & _
                              " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1) " & _
                              "end As strPermitNumber, " & _
                              "case " & _
                              " 	when numUserID= '0' then ' ' " & _
                              "	when numUserID is Null then ' ' " & _
                              "else (strLastName||', '||strFirstName) " & _
                              "end as StaffResponsible, " & _
                              "case  " & _
                              "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd') " & _
                              "when datFinalizedDate is not Null then to_char(datFinalizedDate, 'RRRR-MM-dd') " & _
                              "when datToDirector is Not Null and datFinalizedDate is Null " & _
                              "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd') " & _
                              "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                              "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')  " & _
                              "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')   " & _
                              "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')   " & _
                              "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd') " & _
                              "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd') " & _
                              "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd') " & _
                              "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd') " & _
                              "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')   " & _
                              "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown' " & _
                              "else to_char(datAssignedToEngineer, 'RRRR-MM-dd') " & _
                              "end as StatusDate,  " & _
                              "case  " & _
                              " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '  " & _
                              "else AIRBranch.SSPPApplicationData.strFacilityName  " & _
                              "end as strFacilityName,  " & _
                              "case " & _
                              "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out' " & _
                              "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO' " & _
                              "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                              "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC' " & _
                              "when datEPAEnds is not Null then '08 - EPA 45-day Review' " & _
                              "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired' " & _
                              "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'  " & _
                              "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'  " & _
                              "when dattoPMII is Not Null then '04 - AT PM'  " & _
                              "when dattoPMI is Not Null then '03 - At UC'  " & _
                              "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')    " & _
                              "then '02 - Internal Review' " & _
                              "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'   " & _
                              "else '01 - At Engineer'  " & _
                              "end as AppStatus, " & _
                              "case " & _
                              " 	when strPermitTypeDescription is Null then '' " & _
                              "else strPermitTypeDescription " & _
                              "End as strPermitType " & _
                              "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking, " & _
                              "AIRBranch.SSPPApplicationData, " & _
                              "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes, " & _
                              "AIRBranch.EPDuserProfiles  " & _
                              "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)  " & _
                              "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+) " & _
                              "and strApplicationType = strApplicationTypeCode (+) " & _
                              "and strPermitType = strPermitTypeCode (+) " & _
                              "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " & _
                              "and datFinalizedDate is NULL " & _
                              "order by AIRBranch.SSPPApplicationMaster.strApplicationNumber DESC  "

                            Else
                                If AccountArray(24, 3) = "1" Then 'Unit Manager
                                    UserGridStyle = "LoadSSPPOpenWork"
                                    SQL = "Select " & _
                                     "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber,  " & _
                                     "case  " & _
                                    " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' '  " & _
                                    " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' '  " & _
                                     "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5)  " & _
                                     "end as strAIRSNumber,  " & _
                                     "case  " & _
                                     "	when strApplicationTypeDesc IS Null then ' '  " & _
                                     "Else strApplicationTypeDesc  " & _
                                     "End as strApplicationType,  " & _
                                     "case  " & _
                                     "	when datReceivedDate is Null then ' '  " & _
                                     "Else to_char(datReceivedDate, 'RRRR-MM-dd')  " & _
                                     "End as datReceivedDate,  " & _
                                     "case   " & _
                                     "  when strPermitNumber is NULL then ' '   " & _
                                     "   else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'   " & _
                                     " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-'  " & _
                                     " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1)  " & _
                                     "end As strPermitNumber,  " & _
                                     "case  " & _
                                     "	when numUserID = '0' then ' '  " & _
                                     "	when numUserID is Null then ' '  " & _
                                     "else (strLastName||', '||strFirstName)  " & _
                                     "end as StaffResponsible,  " & _
                                     "case   " & _
                                     "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd')     " & _
                                     "when datFinalizedDate is Not Null then to_char(datFinalizedDate, 'RRRR-MM-dd')  " & _
                                     "when datToDirector is Not Null and datFinalizedDate is Null  " & _
                                     "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd')  " & _
                                     "when datToBranchCheif is Not Null and datFinalizedDate is Null  " & _
                                     "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')   " & _
                                     "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')    " & _
                                     "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')    " & _
                                     "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd')     " & _
                                     "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd')     " & _
                                     "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd')     " & _
                                     "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd')     " & _
                                     "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')    " & _
                                     "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown'     " & _
                                     "else to_char(datAssignedToEngineer, 'RRRR-MM-dd')     " & _
                                     "end as StatusDate,   " & _
                                     "case   " & _
                                     "	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '   " & _
                                     "else AIRBranch.SSPPApplicationData.strFacilityName   " & _
                                     "end as strFacilityName,   " & _
                                     "case  " & _
                                     "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out'  " & _
                                     "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'  " & _
                                     "when datToBranchCheif is Not Null and datFinalizedDate is Null  " & _
                                     "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'  " & _
                                     "when datEPAEnds is not Null then '08 - EPA 45-day Review'  " & _
                                     "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired'  " & _
                                     "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'   " & _
                                     "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'   " & _
                                     "when dattoPMII is Not Null then '04 - AT PM'   " & _
                                     "when dattoPMI is Not Null then '03 - At UC'   " & _
                                     "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'  " & _
                                     "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'    " & _
                                     "else '01 - At Engineer'   " & _
                                     "end as AppStatus,  " & _
                                     "case  " & _
                                     "	when strPermitTypeDescription is Null then ''  " & _
                                     "else strPermitTypeDescription  " & _
                                     "End as strPermitType  " & _
                                     "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking,  " & _
                                     "AIRBranch.SSPPApplicationData,  " & _
                                     "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes,  " & _
                                     "AIRBranch.EPDUserProfiles " & _
                                     "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)   " & _
                                     "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+)  " & _
                                     "and strApplicationType = strApplicationTypeCode (+)  " & _
                                     "and strPermitType = strPermitTypeCode (+)  " & _
                                     "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible  " & _
                                     "and datFinalizedDate is NULL  " & _
                                     "and (AIRBranch.EPDUserProfiles.numUnit = '" & UserUnit & "'   " & _
                                     "or (APBUnit = '" & UserUnit & "'))  "

                                Else
                                    If AccountArray(9, 3) = "1" Then 'Administrative 2
                                        UserGridStyle = "LoadSSPPOpenWork"
                                        SQL = "Select " & _
                                        "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber,   " & _
                                        "case   " & _
                                        " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' '   " & _
                                        " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' '   " & _
                                        " else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5)   " & _
                                        " end as strAIRSNumber,   " & _
                                        " case   " & _
                                        " 	when strApplicationTypeDesc IS Null then ' '   " & _
                                        "Else strApplicationTypeDesc   " & _
                                        "End as strApplicationType,   " & _
                                        "case   " & _
                                        "	when datReceivedDate is Null then ' '   " & _
                                        "Else to_char(datReceivedDate, 'RRRR-MM-dd')   " & _
                                        "End as datReceivedDate,   " & _
                                        "case    " & _
                                        " when strPermitNumber is NULL then ' '    " & _
                                        " else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'    " & _
                                        "   ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-'   " & _
                                        "   ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1)   " & _
                                        "end As strPermitNumber,   " & _
                                        "case   " & _
                                        "	when numUserID = '0' then ' '   " & _
                                        "	when numUserID is Null then ' '   " & _
                                        "else (strLastName||', '||strFirstName)   " & _
                                        "end as StaffResponsible,   " & _
                                        "case    " & _
                                        "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd')      " & _
                                        "when datFinalizedDate is Not Null then to_char(datFinalizedDate, 'RRRR-MM-dd')   " & _
                                        "when datToDirector is Not Null and datFinalizedDate is Null   " & _
                                        "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd')   " & _
                                        "when datToBranchCheif is Not Null and datFinalizedDate is Null   " & _
                                     "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')    " & _
                                        "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')     " & _
                                         "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')     " & _
                                         "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd')      " & _
                                         "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd')      " & _
                                         "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd')      " & _
                                         "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd')      " & _
                                         "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')     " & _
                                         "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown'      " & _
                                         "else to_char(datAssignedToEngineer, 'RRRR-MM-dd')      " & _
                                         "end as StatusDate,    " & _
                                         "case    " & _
                                         " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '    " & _
                                         "else AIRBranch.SSPPApplicationData.strFacilityName    " & _
                                         "end as strFacilityName,    " & _
                                         "case   " & _
                                         "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out'   " & _
                                  "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'   " & _
                                         "when datToBranchCheif is Not Null and datFinalizedDate is Null   " & _
                                         "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'   " & _
                                         "when datEPAEnds is not Null then '08 - EPA 45-day Review'   " & _
                                         "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired'   " & _
                                         "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'    " & _
                                         "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'    " & _
                                         "when dattoPMII is Not Null then '04 - AT PM'    " & _
                                         "when dattoPMI is Not Null then '03 - At UC'    " & _
                                         "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'   " & _
                                         "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'     " & _
                                         "else '01 - At Engineer'    " & _
                                         "end as AppStatus,   " & _
                                         "case   " & _
                                          "when strPermitTypeDescription is Null then ''   " & _
                                         "else strPermitTypeDescription   " & _
                                         "End as strPermitType   " & _
                                         "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking,   " & _
                                         "AIRBranch.SSPPApplicationData,   " & _
                                         "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes,   " & _
                                         "AIRBranch.EPDUserProfiles    " & _
                                         "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)    " & _
                                         "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+)   " & _
                                         "and strApplicationType = strApplicationTypeCode (+)   " & _
                                         "and strPermitType = strPermitTypeCode (+)   " & _
                                         "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible   " & _
                                        "and datFinalizedDate is NULL   " & _
                                         "and numUserID = '" & UserGCode & "'  "
                                    Else
                                        UserGridStyle = "LoadSSPPOpenWork"
                                        SQL = "Select " & _
                                        "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber,   " & _
                                        "case   " & _
                                        " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' '   " & _
                                        " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' '   " & _
                                        " else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5)   " & _
                                        "end as strAIRSNumber,   " & _
                                        "   case   " & _
                                        " 	when strApplicationTypeDesc IS Null then ' '   " & _
                                        "Else strApplicationTypeDesc   " & _
                                        "End as strApplicationType,   " & _
                                        "case   " & _
                                        " 	when datReceivedDate is Null then ' '   " & _
                                        "Else to_char(datReceivedDate, 'RRRR-MM-dd')   " & _
                                        " End as datReceivedDate,   " & _
                                        " case    " & _
                                        " when strPermitNumber is NULL then ' '    " & _
                                        "  else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'    " & _
                                        " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-'   " & _
                                        " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1)   " & _
                                        "end As strPermitNumber,   " & _
                                        "case   " & _
                                        " 	when numUserID = '0' then ' '   " & _
                                        " 	when numUserID is Null then ' '   " & _
                                        "else (strLastName||', '||strFirstName)   " & _
                                        "end as StaffResponsible,   " & _
                                        "case    " & _
                                        "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd')      " & _
                                        "when datFinalizeddate is Not Null then to_char(datFinalizeddate, 'RRRR-MM-dd')   " & _
                                        "when datToDirector is Not Null and datFinalizedDate is Null and   " & _
                                        "(datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd')   " & _
                                        "when datToBranchCheif is Not Null and datFinalizedDate is Null and   " & _
                                        "datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')    " & _
                                        "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')     " & _
                                        "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')     " & _
                                        "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd')      " & _
                                        "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd')      " & _
                                        "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd')      " & _
                                        "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd')      " & _
                                        "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')     " & _
                                        "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown'      " & _
                                        "else to_char(datAssignedToEngineer, 'RRRR-MM-dd')      " & _
                                        "end as StatusDate,    " & _
                                        "case    " & _
                                        "	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '    " & _
                                        "else AIRBranch.SSPPApplicationData.strFacilityName    " & _
                                        "end as strFacilityName,    " & _
                                        "case   " & _
                                        "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out'   " & _
                                 "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'   " & _
                                        "when datToBranchCheif is Not Null and datFinalizedDate is Null   " & _
                                        "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'   " & _
                                        "when datEPAEnds is not Null then '08 - EPA 45-day Review'   " & _
                                        "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired'   " & _
                                        "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'    " & _
                                        "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'    " & _
                                        "when dattoPMII is Not Null then '04 - AT PM'    " & _
                                        "when dattoPMI is Not Null then '03 - At UC'    " & _
                                        "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'   " & _
                                        "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'     " & _
                                        "else '01 - At Engineer'    " & _
                                        "end as AppStatus,   " & _
                                        "case   " & _
                                        " 	when strPermitTypeDescription is Null then ''   " & _
                                        "else strPermitTypeDescription   " & _
                                        "End as strPermitType   " & _
                                        "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking,   " & _
                                        "AIRBranch.SSPPApplicationData,   " & _
                                        "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes,   " & _
                                        "AIRBranch.EPDUserProfiles  " & _
                                 "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)    " & _
                                 "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+)   " & _
                                        "and strApplicationType = strApplicationTypeCode (+)   " & _
                                        "and strPermitType = strPermitTypeCode (+)   " & _
                                        "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible   " & _
                                        "and datFinalizedDate is NULL   " & _
                                        "and numUserID = ('" & UserGCode & "') "
                                    End If
                                End If
                            End If
                        Case "6" 'Ambient

                    End Select
                Case "2" 'Watershed Protection

                Case "3" 'Hazard Waste

                Case "4" 'Land Protection

                Case "5" 'Program Coordination 

                    If WorkUnit = "---" Then 'Program Manager
                        UserGridStyle = "LoadSSCPOpenWork"
                        SQL = "Select " & _
                        "distinct(to_number(AIRBranch.sscp_AuditedEnforcement.strEnforcementNumber)) as strEnforcementNumber,  " & _
                        "substr(AIRBranch.sscp_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,   " & _
                        "case   " & _
                        "when datEnforcementFinalized is Not Null then '4 - Closed Out'   " & _
                        "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " & _
                        "when strStatus = 'UC' then '2 - Submitted to UC'   " & _
                        "When strStatus Is Null then '1 - At Staff'   " & _
                        "else 'Unknown'   " & _
                        "end as EnforcementStatus,   " & _
                        "Case     " & _
                        "when datDiscoveryDate is Null then ''    " & _
                        "else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                        "END as Violationdate,     " & _
                        "strActionType as HPVStatus,    " & _
                        "Case    " & _
                        "when datEnforcementFinalized Is Not NULL then 'Closed'    " & _
                        "when datEnforcementFinalized is NUll then 'Open'    " & _
                        "Else 'Open'    " & _
                        "End as Status,    " & _
                        "strFacilityName,    " & _
                        "(strLastName||', '||strFirstName) as Staff     " & _
                        "from AIRBranch.sscp_AuditedEnforcement,     " & _
                        "AIRBranch.APBFacilityInformation, AIRBranch.EPDUserProfiles,    " & _
                        "(select numUserID  " & _
                        "from AIRBranch.EPDUserProfiles where numUnit is null) UnitStaff " & _
                        "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.sscp_AuditedEnforcement.strAIRSNumber    " & _
                        "and (strStatus IS Null or strStatus = 'UC')    " & _
                        "and datEnforcementFinalized is NULL   " & _
                        "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.sscp_AuditedEnforcement.numStaffResponsible    " & _
                        "order by strENforcementNumber DESC   "
                    Else
                        If AccountArray(22, 3) = "1" Then 'Unit Manager
                            UserGridStyle = "LoadSSCPOpenWork"
                            SQL = "Select to_number(AIRBranch.SSCP_aUDITEDEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                             "substr(AIRBranch.SSCP_aUDITEDEnforcement.strAIRSNumber, 5) as AIRSNumber,   " & _
                             "case   " & _
                             "    when datEnforcementFinalized is Not Null then '4 - Closed Out'   " & _
                             "    when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " & _
                             "    when strStatus = 'UC' then '2 - Submitted to UC'   " & _
                             "    When strStatus Is Null then '1 - At Staff'   " & _
                             "   else 'Unknown'   " & _
                             "end as EnforcementStatus, " & _
                            " Case    " & _
                            " 	when datDiscoveryDate is Null then ''   " & _
                            " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                             "END as Violationdate,    " & _
                             "case    " & _
                             " 	when strHPV IS NULL then strActionType   " & _
                             "	When strHPV IS Not Null then 'HPV'    " & _
                             "   Else 'HPV'   " & _
                             "END as HPVStatus,   " & _
                             "Case   " & _
                             " 	when datEnforcementFinalized Is Not NULL then 'Closed'   " & _
                             "	when datEnforcementFinalized is NUll then 'Open'   " & _
                             "Else 'Open'   " & _
                             "End as Status,   " & _
                             "strFacilityName,   " & _
                             "(strLastName||', '||strFirstName) as Staff   " & _
                             "from AIRBranch.SSCP_aUDITEDEnforcement,    " & _
                             "AIRBranch.APBFacilityInformation, AIRBranch.EPDUserProfiles,   " & _
                             "( select numUserID from AIRBranch.EPDUserProfiles where numUnit = '" & UserUnit & "'  " & _
                             "group by numUserID ) UnitStaff   " & _
                             "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_aUDITEDEnforcement.strAIRSNumber   " & _
                             "and (strStatus IS Null or strStatus = 'UC')   " & _
                             "and numStaffResponsible = UnitStaff.numUserID   " & _
                             "and datEnforcementFinalized is NULL   " & _
                             "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCP_aUDITEDEnforcement.numStaffResponsible   " & _
                             "order by strENforcementNumber DESC  "
                        Else
                            If AccountArray(10, 3) = "1" Then 'District Liason
                                UserGridStyle = "LoadSSCPOpenWork"
                                SQL = "Select to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                                "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,   " & _
                                "case   " & _
                                "when datEnforcementFinalized is Not Null then '4 - Closed Out'   " & _
                                "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " & _
                                "when strStatus = 'UC' then '2 - Submitted to UC'   " & _
                                "When strStatus Is Null then '1 - At Staff'   " & _
                                "   else 'Unknown'   " & _
                                "end as EnforcementStatus, " & _
                                "Case    " & _
                                " 	when datDiscoveryDate is Null then ''   " & _
                                " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                                "END as Violationdate,    " & _
                                "case    " & _
                                " 	when strHPV IS NULL then strActionType   " & _
                                " 	When strHPV IS Not Null then 'HPV'    " & _
                                "   Else 'HPV'   " & _
                                "END as HPVStatus,   " & _
                                "Case   " & _
                                " 	when datEnforcementFinalized Is Not NULL then 'Closed'   " & _
                                "	when datEnforcementFinalized is NUll then 'Open'   " & _
                                "Else 'Open'   " & _
                                "End as Status,   " & _
                                "strFacilityName,   " & _
                                "(strLastName||', '||strFirstName) as Staff   " & _
                                "from AIRBranch.SSCP_AuditedEnforcement,  " & _
                                "AIRBranch.APBFacilityInformation, AIRBranch.EPDUSerProfiles,   " & _
                                "(select numuserId  " & _
                                "from AIRBranch.EPDUserProfiles  " & _
                                "where strLastName = 'District' or (numBranch = '1' and numProgram = '4' and numUnit is null )  " & _
                                "group by numUserID) UnitStaff   " & _
                                "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber   " & _
                                "and (strStatus IS Null or strStatus = 'UC')   " & _
                                "and numStaffResponsible = UnitStaff.numUserID   " & _
                                "and datEnforcementFinalized is NULL   " & _
                                "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCP_AuditedEnforcement.numStaffResponsible   " & _
                                "order by strENforcementNumber DESC   "
                            Else
                                UserGridStyle = "LoadSSCPOpenWork"
                                SQL = "Select to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                             "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " & _
                             "case  " & _
                             "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " & _
                             "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " & _
                             "when strStatus = 'UC' then '2 - Submitted to UC'  " & _
                             "When strStatus Is Null then '1 - At Staff'  " & _
                             "else 'Unknown'  " & _
                             "end as EnforcementStatus,  " & _
                             "Case   " & _
                             " 	when datDiscoveryDate is Null then ''  " & _
                             "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                             "END as Violationdate,   " & _
                             "case   " & _
                             "	when strHPV IS NULL then strActionType  " & _
                             "	When strHPV IS Not Null then 'HPV'   " & _
                             "Else 'HPV'  " & _
                             "END as HPVStatus,  " & _
                             "Case  " & _
                             " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " & _
                             " 	when datEnforcementFinalized is NUll then 'Open'  " & _
                             "Else 'Open'  " & _
                             "End as Status,  " & _
                             "AIRBranch.APBFacilityInformation.strFacilityName,  " & _
                             "(strLastName||', '||strFirstName) as Staff  " & _
                             "from AIRBranch.SSCP_AuditedEnforcement,   " & _
                             "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " & _
                             "AIRBranch.VW_SSCPINSPECTION_LIST " & _
                             "Where AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber  " & _
                             "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = '0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " & _
                             "and (numStaffResponsible = '" & UserGCode & "' or numSSCPEngineer = '" & UserGCode & "')  " & _
                             "and (strStatus IS Null or strStatus = 'UC')  " & _
                             "and datEnforcementFinalized is Null  " & _
                             "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " & _
                             "order by strENforcementNumber DESC  "

                            End If
                        End If
                    End If



                    'Select Case WorkProgram
                    '    Case "7", "8", "9", "10", "11", "12", "13", "14", "15" 'Distirct 
                    '        If WorkUnit = "---" Then 'Program Manager 
                    '            UserGridStyle = "LoadSSCPOpenWork"
                    '            SQL = "Select to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber, " & _
                    '              "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber, " & _
                    '              "case " & _
                    '              "    when datEnforcementFinalized is Not Null then '4 - Closed Out' " & _
                    '              "    when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA' " & _
                    '              "    when strStatus = 'UC' then '2 - Submitted to UC' " & _
                    '              "    When strStatus Is Null then '1 - At Staff' " & _
                    '              "   else 'Unknown' " & _
                    '              "end as EnforcementStatus, " & _
                    '              "Case  " & _
                    '              "	when datDiscoveryDate is Null then '' " & _
                    '              "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                    '              "END as Violationdate,  " & _
                    '              "case  " & _
                    '              " 	when strHPV IS NULL then strActionType " & _
                    '              " 	When strHPV IS Not Null then 'HPV'  " & _
                    '              "   Else 'HPV' " & _
                    '              "END as HPVStatus, " & _
                    '              "Case " & _
                    '             " 	when datEnforcementFinalized Is Not NULL then 'Closed' " & _
                    '              "	when datEnforcementFinalized is NUll then 'Open' " & _
                    '              "Else 'Open' " & _
                    '              "End as Status, " & _
                    '              "strFacilityName, " & _
                    '              "(strLastName||', '||strFirstName) as Staff " & _
                    '              "from AIRBranch.SSCP_AuditedEnforcement, " & _
                    '              "AIRBranch.APBFacilityInformation, AIRBranch.EPDUserProfiles, " & _
                    '              "(select numUserID from AIRBranch.EPDUserProfiles " & _
                    '              "where strLastName = 'District' or (numBranch = '1' and numProgram = '4' and numUnit is null) " & _
                    '              "group by numUserID) UnitStaff " & _
                    '              "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber " & _
                    '              "and (strStatus IS Null or strStatus = 'UC') " & _
                    '              "and numStaffResponsible = UnitStaff.numUserID " & _
                    '              "and datEnforcementFinalized is NULL " & _
                    '              "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCP_AuditedEnforcement.numStaffResponsible " & _
                    '              "order by strENforcementNumber DESC "
                    '        Else
                    '            UserGridStyle = "LoadSSCPOpenWork"
                    '            SQL = "Select to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                    '            "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,   " & _
                    '            "case   " & _
                    '            "    when datEnforcementFinalized is Not Null then '4 - Closed Out'   " & _
                    '            "    when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'   " & _
                    '            "    when strStatus = 'UC' then '2 - Submitted to UC'   " & _
                    '            "    When strStatus Is Null then '1 - At Staff'   " & _
                    '            "   else 'Unknown'   " & _
                    '            "end as EnforcementStatus, " & _
                    '            "Case    " & _
                    '            "	when datDiscoveryDate is Null then ''   " & _
                    '            "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                    '            "END as Violationdate,    " & _
                    '            "case    " & _
                    '            " 	when strHPV IS NULL then strActionType   " & _
                    '            " 	When strHPV IS Not Null then 'HPV'    " & _
                    '            "   Else 'HPV'   " & _
                    '            "END as HPVStatus,   " & _
                    '            "Case   " & _
                    '            "	when datEnforcementFinalized Is Not NULL then 'Closed'   " & _
                    '            "	when datEnforcementFinalized is NUll then 'Open'   " & _
                    '            "Else 'Open'   " & _
                    '            "End as Status,   " & _
                    '            "strFacilityName,   " & _
                    '            "(strLastName||', '||strFirstName) as Staff   " & _
                    '            "from AIRBranch.SSCP_AuditedEnforcement,  " & _
                    '            "AIRBranch.APBFacilityInformation, AIRBranch.EPDUserProfiles,   " & _
                    '            "(select numUserID from AIRBranch.EPDUserProfiles   " & _
                    '            "where strLastName = 'District' or (numBranch = '1' and numProgram = '4' and numUnit is null)  " & _
                    '            "group by numUserID) UnitStaff   " & _
                    '            "Where  AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber   " & _
                    '            "and (strStatus IS Null or strStatus = 'UC')   " & _
                    '            "and numStaffResponsible = UnitStaff.numUSerID  " & _
                    '            "and datEnforcementFinalized is NULL   " & _
                    '            "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCP_AuditedEnforcement.numStaffResponsible   " & _
                    '            "order by strEnforcementNumber DESC  "
                    '        End If

                    'End Select
                Case "6" 'Directors Office 

            End Select

            dsOpenWork = New DataSet
            daOpenWork = New OracleDataAdapter(SQL, Conn)
            If SQL <> "" Then
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                daOpenWork.Fill(dsOpenWork, "OpenWork")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)

        End Try

    End Sub
    Sub LoadSecondaryWork(ByVal WorkBranch As String, ByVal WorkProgram As String, ByVal WorkUnit As String)
        Try

            Select Case WorkBranch
                Case "1" 'Air Protection Branch
                    Select Case WorkProgram
                        Case "1" 'Mobile & Area

                        Case "2" 'Planning & Support

                        Case "3" 'ISMP
                            If WorkUnit = "---" Then 'Program Manager
                                UserGridStyle = "LoadSecondaryWork2"
                                SQL = "Select " & _
                                "to_number(AIRBranch.SSPPApplicationMaster.strApplicationNumber) as strApplicationNumber,    " & _
                                "substr(strAIRSNumber, 5) as AIRSNumber,    " & _
                                "to_char(datreviewsubmitted, 'dd-Mon-yyyy') as ReviewSubmitted,    " & _
                                "(strLastName||', ' ||strFirstName) as StaffResponsible,    " & _
                                "strFacilityName     " & _
                                "from AIRBranch.SSPPApplicationMaster,    " & _
                                "AIRBranch.SSPPApplicationData, AIRBranch.EPDUserProfiles,     " & _
                                "AIRBranch.SSPPApplicationTracking   " & _
                                "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber   " & _
                                "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber   " & _
                                "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible   " & _
                                "and strISMPReviewer is NULL   " & _
                                "and datFinalizedDate is Null  "
                            Else
                                If AccountArray(17, 3) = "1" Then  'Unit Manager
                                    UserGridStyle = "LoadSecondaryWork2"
                                    SQL = "Select " & _
                                    "to_number(AIRBranch.SSPPApplicationMaster.strApplicationNumber) as strApplicationNumber,    " & _
                                    "substr(strAIRSNumber, 5) as AIRSNumber,    " & _
                                    "to_char(datreviewsubmitted, 'dd-Mon-yyyy') as ReviewSubmitted,    " & _
                                    "(strLastName||', '||strFirstName) as StaffResponsible,    " & _
                                    "strFacilityName     " & _
                                    "from AIRBranch.SSPPApplicationMaster,    " & _
                                    "AIRBranch.SSPPApplicationData, AIRBranch.EPDUserProfiles,     " & _
                                    "AIRBranch.SSPPApplicationTracking   " & _
                                    "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber   " & _
                                    "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber   " & _
                                    "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible   " & _
                                    "and strISMPReviewer is NULL   " & _
                                    "and datFinalizedDate is Null  "
                                Else
                                    UserGridStyle = "LoadSecondaryWork2"
                                    SQL = "Select  " & _
                                   "to_number(AIRBranch.SSPPApplicationMaster.strApplicationNumber) as strApplicationNumber,    " & _
                                   "substr(strAIRSNumber, 5) as AIRSNumber,    " & _
                                   "to_char(datreviewsubmitted, 'dd-Mon-yyyy') as ReviewSubmitted,    " & _
                                   "(strLastName||', '||strFirstName) as StaffResponsible,    " & _
                                   "strFacilityName     " & _
                                   "from AIRBranch.SSPPApplicationMaster,    " & _
                                   "AIRBranch.SSPPApplicationData, AIRBranch.EPDUserProfiles,     " & _
                                   "AIRBranch.SSPPApplicationTracking   " & _
                                   "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber   " & _
                                   "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber   " & _
                                   "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible   " & _
                                   "and strISMPReviewer is NULL   " & _
                                   "and datFinalizedDate is Null  "
                                End If
                            End If
                        Case "4" 'SSCP
                            If WorkUnit = "---" Then 'Program Manager
                                UserGridStyle = "LoadSecondaryWork2"
                                SQL = "Select  " & _
                                 "to_number(AIRBranch.SSPPApplicationMaster.strApplicationNumber) as strApplicationNumber,    " & _
                                 "substr(strAIRSNumber, 5) as AIRSNumber,    " & _
                                 "to_char(datreviewsubmitted, 'dd-Mon-yyyy') as ReviewSubmitted,    " & _
                                 "(strLastName||', '||strFirstName) as StaffResponsible,    " & _
                                 "strFacilityName     " & _
                                 "from AIRBranch.SSPPApplicationMaster,    " & _
                                 "AIRBranch.SSPPApplicationData, AIRBranch.EPDUserProfiles,   " & _
                                 "AIRBranch.SSPPApplicationTracking   " & _
                                 "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber   " & _
                                 "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber   " & _
                                 "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible   " & _
                                 "and strSSCPReviewer is NULL   " & _
                                 "and datFinalizedDate is Null"
                            Else
                                If AccountArray(22, 3) = "1" Then 'Unit Manager
                                    UserGridStyle = "LoadSecondaryWork2"
                                    SQL = "Select " & _
                                    "to_number(AIRBranch.SSPPApplicationMaster.strApplicationNumber) as strApplicationNumber,  " & _
                                    "substr(strAIRSNumber, 5) as AIRSNumber,  " & _
                                    "to_char(datreviewsubmitted, 'dd-Mon-yyyy') as ReviewSubmitted,  " & _
                                    "(strLastName||', '||strFirstName) as StaffResponsible,  " & _
                                    "strFacilityName   " & _
                                    "from AIRBranch.SSPPApplicationMaster,  " & _
                                    "AIRBranch.SSPPApplicationData, AIRBranch.EPDUserProfiles,   " & _
                                    "AIRBranch.SSPPApplicationTracking " & _
                                    "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber " & _
                                    "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber " & _
                                    "and AIRBranch.EPDUserProfiles.numUserId = AIRBranch.SSPPApplicationMaster.strStaffResponsible " & _
                                    "and strSSCPReviewer is NULL " & _
                                    "and datFinalizedDate is Null "
                                Else
                                    If AccountArray(10, 3) = "1" Then 'District Liason
                                        UserGridStyle = "LoadSecondaryWork2"
                                        SQL = "Select " & _
                                        "to_number(AIRBranch.SSPPApplicationMaster.strApplicationNumber) as strApplicationNumber,  " & _
                                        "substr(strAIRSNumber, 5) as AIRSNumber,  " & _
                                        "to_char(datreviewsubmitted, 'dd-Mon-yyyy') as ReviewSubmitted,  " & _
                                        "(strLastName||', '||strFirstName) as StaffResponsible,  " & _
                                        "strFacilityName   " & _
                                        "from AIRBranch.SSPPApplicationMaster,  " & _
                                        "AIRBranch.SSPPApplicationData, AIRBranch.EPDUserProfiles,   " & _
                                        "AIRBranch.SSPPApplicationTracking " & _
                                        "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber " & _
                                        "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber " & _
                                        "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " & _
                                        "and strSSCPReviewer is NULL " & _
                                        "and datFinalizedDate is Null "
                                    Else
                                        UserGridStyle = "LoadSecondaryWork2"
                                        SQL = "Select " & _
                                        "to_number(AIRBranch.SSPPApplicationMaster.strApplicationNumber) as strApplicationNumber,  " & _
                                        "substr(strAIRSNumber, 5) as AIRSNumber,  " & _
                                        "to_char(datreviewsubmitted, 'dd-Mon-yyyy') as ReviewSubmitted,  " & _
                                        "(strLastName||', '||strFirstName) as StaffResponsible,  " & _
                                        "strFacilityName   " & _
                                        "from AIRBranch.SSPPApplicationMaster,  " & _
                                        "AIRBranch.SSPPApplicationData, AIRBranch.EPDUserProfiles, " & _
                                        "AIRBranch.SSPPApplicationTracking " & _
                                        "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber " & _
                                        "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber " & _
                                        "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " & _
                                        "and strSSCPReviewer is NULL " & _
                                        "and datFinalizedDate is Null "
                                    End If
                                End If
                            End If
                        Case "5" 'SSPP
                            If WorkUnit = "---" Then 'Program Manager

                            Else
                                If AccountArray(24, 3) = "1" Then 'Unit Manager

                                Else
                                    If AccountArray(9, 3) = "1" Then 'Administrative 2
                                        UserGridStyle = "LoadSecondaryWork2"
                                        SQL = "Select " & _
                                        "to_number(AIRBranch.SSPPApplicationMaster.strApplicationNumber) as strApplicationNumber,  " & _
                                        "substr(strAIRSNumber, 5) as AIRSNumber,  " & _
                                        "to_char(datreviewsubmitted, 'dd-Mon-yyyy') as ReviewSubmitted,  " & _
                                        "(strLastName||', '||strFirstName) as StaffResponsible,  " & _
                                        "strFacilityName   " & _
                                        "from AIRBranch.SSPPApplicationMaster, " & _
                                        "AIRBranch.SSPPApplicationData, AIRBranch.EPDUserProfiles, " & _
                                        "AIRBranch.SSPPApplicationTracking " & _
                                        "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber " & _
                                        "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber " & _
                                        "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " & _
                                        "and strSSCPReviewer is NULL " & _
                                        "and datFinalizedDate is Null "
                                    Else

                                    End If
                                End If
                            End If
                        Case "6" 'Ambient

                    End Select
                Case "2" 'Watershed Protection

                Case "3" 'Hazard Waste

                Case "4" 'Land Protection

                Case "5" 'Program Coordination 
                    Select Case WorkProgram
                        Case "7", "8", "9", "10", "11", "12", "13", "14", "15" 'Distirct 
                            If WorkUnit = "---" Then 'Program Manager 
                                UserGridStyle = "LoadSSCPTertiaryWork"
                                SQL = "select " & _
                                "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                                "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                                "(strLastName||', '||strFirstName) as Staff,  " & _
                                "strResponsibleStaff, " & _
                                "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived,  " & _
                                "strFacilityName, StrActivityName    " & _
                                "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                                "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,  " & _
                                "(select numUserID from AIRBranch.EPDUserProfiles " & _
                                "where strLastName = 'District' " & _
                                "group by numUserID) UnitStaff    " & _
                                "where AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCPItemMaster.strResponsibleStaff  " & _
                                "and AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCPItemMaster.strAIRSNumber  " & _
                                "and AIRBranch.LookUPComplianceActivities.strActivityType = AIRBranch.SSCPItemMaster.strEventType " & _
                                "and DatCompleteDate is Null   " & _
                                "and strResponsibleStaff = UnitStaff.numUserID " & _
                                "and strDelete is Null "
                            Else
                                UserGridStyle = "LoadSSCPTertiaryWork"
                                SQL = "Select " & _
                                  "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                                  "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                                  "(strLastName||', '||strFirstName) as Staff,  " & _
                                  "strResponsibleStaff, " & _
                                  "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived, " & _
                                  "strFacilityName, StrActivityName    " & _
                                  "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                                  "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities   " & _
                                  "where AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCPItemMaster.strResponsibleStaff  " & _
                                  "and AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCPItemMaster.strAIRSNumber  " & _
                                  "and AIRBranch.LookUPComplianceActivities.strActivityType = AIRBranch.SSCPItemMaster.strEventType  " & _
                                  "and strResponsibleStaff = '" & UserGCode & "'  " & _
                                  "and DatCompleteDate is Null  " & _
                                   "and strDelete is Null "
                            End If
                    End Select
                Case "6" 'Directors Office 

            End Select

            dsOpenWork = New DataSet
            daOpenWork = New OracleDataAdapter(SQL, Conn)
            If SQL <> "" Then
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                daOpenWork.Fill(dsOpenWork, "OpenWork")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)

        End Try

    End Sub
    Sub LoadTertiaryWork(ByVal WorkBranch As String, ByVal WorkProgram As String, ByVal WorkUnit As String)
        Try

            Select Case WorkBranch
                Case "1" 'Air Protection Branch
                    Select Case WorkProgram
                        Case "1" 'Mobile & Area

                        Case "2" 'Planning & Support

                        Case "3" 'ISMP
                            If WorkUnit = "---" Then 'Program Manager
                                UserGridStyle = "LoadISMPTertiaryWork"
                                SQL = "select  " & _
                                 "AIRBranch.ISMPTestNotification.strTestLogNumber as TestNumber,    " & _
                                 "case    " & _
                                 "when strReferenceNumber is null then ''    " & _
                                 "else strReferenceNumber    " & _
                                 "end RefNum,    " & _
                                 "case  " & _
                                 "when AIRBranch.ISMPTestNOtification.strAIRSNumber is Null then ''  " & _
                                 "else AIRBranch.APBFacilityInformation.strFacilityName    " & _
                                 "End FacilityName,  " & _
                                 "substr(AIRBranch.ISMPTestNOtification.strAIRSNumber, 5) as AIRSNumber,  " & _
                                 "strEmissionUnit,   " & _
                                 "to_char(datProposedStartDate, 'dd-Mon-yyyy') as ProposedStartDate,  " & _
                                 "case  " & _
                                 "when strFirstName is Null then ''  " & _
                                 "else(strLastName||', '||strFirstName)   " & _
                                 "END StaffResponsible  " & _
                                 "from AIRBranch.ismptestnotification, AIRBranch.APBFacilityinformation,  " & _
                                 "AIRBranch.EPDUserProfiles, AIRBranch.ISMPTestLogLink  " & _
                                 "where AIRBranch.ismptestnotification.strairsnumber = AIRBranch.apbfacilityinformation.strairsnumber (+)    " & _
                                 "and AIRBranch.ismptestnotification.strstaffresponsible = AIRBranch.EPDUserProfiles.numUserID (+)  " & _
                                 "and AIRBranch.ISMPTestnotification.strTestLogNumber = AIRBranch.ISMPTestLogLink.strTestLogNumber (+)   " & _
                                 "and datProposedStartDate > (sysdate - 180)    " & _
                                 "and strReferenceNumber is null    " & _
                                 "union    " & _
                                 "select    " & _
                                 "AIRBranch.ISMPTestNotification.strTestLogNumber as TestNumber,  " & _
                                 "AIRBranch.ISMpReportInformation.strReferenceNumber as RefNum,    " & _
                                 "case  " & _
                                 "when AIRBranch.ISMPTestNOtification.strAIRSNumber is Null then ''  " & _
                                 "else AIRBranch.APBFacilityInformation.strFacilityName    " & _
                                 "End FacilityName,  " & _
                                 "substr(AIRBranch.ISMPTestNOtification.strAIRSNumber, 5) as AIRSNumber,  " & _
                                 "strEmissionUnit,   " & _
                                 "to_char(datProposedStartDate, 'dd-Mon-yyyy') as ProposedStartDate,  " & _
                                 "case  " & _
                                 "when strFirstName is Null then ''  " & _
                                 "else(strLastName||', '||strFirstName)   " & _
                                 "END StaffResponsible  " & _
                                 "from AIRBranch.ismptestnotification, AIRBranch.APBFacilityinformation,  " & _
                                 "AIRBranch.EPDUserProfiles, AIRBranch.ISMPTestLogLink,    " & _
                                 "AIRBranch.ISMPReportInformation    " & _
                                 "where AIRBranch.ismptestnotification.strairsnumber = AIRBranch.apbfacilityinformation.strairsnumber (+)    " & _
                                 "and AIRBranch.ismptestnotification.strstaffresponsible = AIRBranch.EPDUserProfiles.numUserID (+)  " & _
                                 "and AIRBranch.ISMPTestNotification.strTestLogNumber = AIRBranch.ISMPTestLogLink.strTestLogNumber (+)    " & _
                                 "and AIRBranch.ISMPTestLogLink.strReferencenumber = AIRBranch.ISMPReportInformation.strReferenceNumber (+)    " & _
                                 "and datProposedStartDate > (sysdate - 180)    " & _
                                 "and AIRBranch.ISMPTestLogLink.strReferenceNumber is not null    " & _
                                 "and strClosed = 'False'  "
                            Else
                                If AccountArray(17, 3) = "1" Then  'Unit Manager
                                    UserGridStyle = "LoadISMPTertiaryWork"
                                    SQL = "select " & _
                                    "AIRBranch.ISMPTestNotification.strTestLogNumber as TestNumber,  " & _
                                    "case  " & _
                                    "when strReferenceNumber is null then ''  " & _
                                    "else strReferenceNumber  " & _
                                    "end RefNum,  " & _
                                    "case   " & _
                                    "when AIRBranch.ISMPTestNOtification.strAIRSNumber is Null then ''   " & _
                                    "else AIRBranch.APBFacilityInformation.strFacilityName  " & _
                                    "End FacilityName,   " & _
                                    "substr(AIRBranch.ISMPTestNOtification.strAIRSNumber, 5) as AIRSNumber,   " & _
                                    "strEmissionUnit,    " & _
                                    "to_char(datProposedStartDate, 'dd-Mon-yyyy') as ProposedStartDate,   " & _
                                    "case   " & _
                                    "when strFirstName is Null then ''   " & _
                                    "else(strLastName||', '||strFirstName) " & _
                                    "END StaffResponsible   " & _
                                    "from AIRBranch.ismptestnotification, AIRBranch.APBFacilityinformation,   " & _
                                    "AIRBranch.EPDUserProfiles, AIRBranch.ISMPTestLogLink   " & _
                                    "where AIRBranch.ismptestnotification.strairsnumber = AIRBranch.apbfacilityinformation.strairsnumber (+)  " & _
                                    "and AIRBranch.ismptestnotification.strstaffresponsible = AIRBranch.EPDUserProfiles.numUserID (+)   " & _
                                    "and AIRBranch.ISMPTestnotification.strTestLogNumber = AIRBranch.ISMPTestLogLink.strTestLogNumber (+) " & _
                                    "and datProposedStartDate > (sysdate - 180)  " & _
                                    "and strReferenceNumber is null  " & _
                                    "union  " & _
                                    "select  " & _
                                    "AIRBranch.ISMPTestNotification.strTestLogNumber as TestNumber,   " & _
                                    "AIRBranch.ISMpReportInformation.strReferenceNumber as RefNum,  " & _
                                    "case   " & _
                                    "when AIRBranch.ISMPTestNOtification.strAIRSNumber is Null then ''   " & _
                                    "else AIRBranch.APBFacilityInformation.strFacilityName  " & _
                                    "End FacilityName,   " & _
                                    "substr(AIRBranch.ISMPTestNOtification.strAIRSNumber, 5) as AIRSNumber,   " & _
                                    "strEmissionUnit,    " & _
                                    "to_char(datProposedStartDate, 'dd-Mon-yyyy') as ProposedStartDate,   " & _
                                    "case   " & _
                                    "when strFirstName is Null then ''   " & _
                                    "else(strLastName||', '||strFirstName) " & _
                                    "END StaffResponsible   " & _
                                    "from AIRBranch.ismptestnotification, AIRBranch.APBFacilityinformation,   " & _
                                    "AIRBranch.EPDUserProfiles, AIRBranch.ISMPTestLogLink,  " & _
                                    "AIRBranch.ISMPReportInformation  " & _
                                    "where AIRBranch.ismptestnotification.strairsnumber = AIRBranch.apbfacilityinformation.strairsnumber (+)  " & _
                                    "and AIRBranch.ismptestnotification.strstaffresponsible = AIRBranch.EPDUserProfiles.numUserID (+)   " & _
                                    "and AIRBranch.ISMPTestNotification.strTestLogNumber = AIRBranch.ISMPTestLogLink.strTestLogNumber (+)  " & _
                                    "and AIRBranch.ISMPTestLogLink.strReferencenumber = AIRBranch.ISMPReportInformation.strReferenceNumber (+)  " & _
                                    "and datProposedStartDate > (sysdate - 180)  " & _
                                    "and AIRBranch.ISMPTestLogLink.strReferenceNumber is not null  " & _
                                    "and strClosed = 'False' "
                                Else
                                    UserGridStyle = "LoadISMPTertiaryWork"
                                    SQL = "select " & _
                                    "AIRBranch.ISMPTestNotification.strTestLogNumber as TestNumber,  " & _
                                    "case  " & _
                                    "when strReferenceNumber is null then ''  " & _
                                    "else strReferenceNumber  " & _
                                    "end RefNum,  " & _
                                    "case   " & _
                                    "when AIRBranch.ISMPTestNOtification.strAIRSNumber is Null then ''   " & _
                                    "else AIRBranch.APBFacilityInformation.strFacilityName  " & _
                                    "End FacilityName,   " & _
                                    "substr(AIRBranch.ISMPTestNOtification.strAIRSNumber, 5) as AIRSNumber,   " & _
                                    "strEmissionUnit,    " & _
                                    "to_char(datProposedStartDate, 'dd-Mon-yyyy') as ProposedStartDate,   " & _
                                    "case   " & _
                                    "when strFirstName is Null then ''   " & _
                                    "else(strLastName||', '||strFirstName) " & _
                                    "END StaffResponsible   " & _
                                    "from AIRBranch.ismptestnotification, AIRBranch.APBFacilityinformation,   " & _
                                    "AIRBranch.EPDUserProfiles, AIRBranch.ISMPTestLogLink   " & _
                                    "where AIRBranch.ismptestnotification.strairsnumber = AIRBranch.apbfacilityinformation.strairsnumber (+)  " & _
                                    "and AIRBranch.ismptestnotification.strstaffresponsible = AIRBranch.EPDUserProfiles.numUserID (+)   " & _
                                    "and AIRBranch.ISMPTestnotification.strTestLogNumber = AIRBranch.ISMPTestLogLink.strTestLogNumber (+) " & _
                                    "and datProposedStartDate > (sysdate - 180)  " & _
                                    "and strStaffResponsible = '" & UserGCode & "' " & _
                                    "and strReferenceNumber is null  " & _
                                    "union  " & _
                                    "select  " & _
                                    "AIRBranch.ISMPTestNotification.strTestLogNumber as TestNumber,   " & _
                                    "AIRBranch.ISMpReportInformation.strReferenceNumber as RefNum,  " & _
                                    "case   " & _
                                    "when AIRBranch.ISMPTestNOtification.strAIRSNumber is Null then ''   " & _
                                    "else AIRBranch.APBFacilityInformation.strFacilityName  " & _
                                    "End FacilityName,   " & _
                                    "substr(AIRBranch.ISMPTestNOtification.strAIRSNumber, 5) as AIRSNumber,   " & _
                                    "strEmissionUnit,    " & _
                                    "to_char(datProposedStartDate, 'dd-Mon-yyyy') as ProposedStartDate,   " & _
                                    "case   " & _
                                    "when strFirstName is Null then ''   " & _
                                    "else(strLastName||', '||strFirstName) " & _
                                    "END StaffResponsible   " & _
                                    "from AIRBranch.ismptestnotification, AIRBranch.APBFacilityinformation,   " & _
                                    "AIRBranch.EPDUserProfiles, AIRBranch.ISMPTestLogLink,  " & _
                                    "AIRBranch.ISMPReportInformation  " & _
                                    "where AIRBranch.ismptestnotification.strairsnumber = AIRBranch.apbfacilityinformation.strairsnumber (+)  " & _
                                    "and AIRBranch.ismptestnotification.strstaffresponsible = AIRBranch.EPDUserProfiles.numUserID (+)   " & _
                                    "and AIRBranch.ISMPTestNotification.strTestLogNumber = AIRBranch.ISMPTestLogLink.strTestLogNumber (+)  " & _
                                    "and AIRBranch.ISMPTestLogLink.strReferencenumber = AIRBranch.ISMPReportInformation.strReferenceNumber (+)  " & _
                                    "and datProposedStartDate > (sysdate - 180)  " & _
                                    "and strStaffResponsible = '" & UserGCode & "' " & _
                                    "and AIRBranch.ISMPTestLogLink.strReferenceNumber is not null  " & _
                                    "and strClosed = 'False' "
                                End If
                            End If
                        Case "4" 'SSCP
                            If WorkUnit = "---" Then 'Program Manager
                                UserGridStyle = "LoadSSCPTertiaryWork"
                                SQL = "select " & _
                                "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                                "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                                "(strLastName||', '||strFirstName) as Staff,  " & _
                                "strResponsibleStaff, " & _
                                "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived,  " & _
                                "strFacilityName, StrActivityName    " & _
                                "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                                "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities " & _
                                "where AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCPItemMaster.strResponsibleStaff  " & _
                                "and AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCPItemMaster.strAIRSNumber  " & _
                                "and AIRBranch.LookUPComplianceActivities.strActivityType = AIRBranch.SSCPItemMaster.strEventType " & _
                                "and DatCompleteDate is Null   " & _
                                "and strDelete is Null "
                            Else
                                If AccountArray(22, 3) = "1" Then 'Unit Manager
                                    UserGridStyle = "LoadSSCPTertiaryWork"
                                    SQL = "select " & _
                                    "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                                    "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                                    "(strLastName||', '||strFirstName) as Staff,  " & _
                                    "strResponsibleStaff, " & _
                                    "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived,  " & _
                                    "strFacilityName, StrActivityName    " & _
                                    "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                                    "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,  " & _
                                    "(select numUserID from AIRBranch.EPDUserProfiles where numUnit = '" & UserUnit & "')  " & _
                                    "UnitStaff    " & _
                                    "where AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCPItemMaster.strResponsibleStaff  " & _
                                    "and AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCPItemMaster.strAIRSNumber  " & _
                                    "and AIRBranch.LookUPComplianceActivities.strActivityType = AIRBranch.SSCPItemMaster.strEventType " & _
                                    "and DatCompleteDate is Null   " & _
                                    "and strResponsibleStaff = UnitStaff.numUserID " & _
                                    "and strDelete is Null "
                                Else
                                    If AccountArray(10, 3) = "1" Then 'District Liason
                                        UserGridStyle = "LoadSSCPTertiaryWork"
                                        SQL = "select " & _
                                        "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                                        "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                                        "(strLastName||', '||strFirstName) as Staff,  " & _
                                        "strResponsibleStaff, " & _
                                        "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived,  " & _
                                        "strFacilityName, StrActivityName    " & _
                                        "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                                        "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,  " & _
                                        "(select numUserID from AIRBranch.EPDUSerProfiles " & _
                                        "where strLastName = 'District' or (numBranch = '1' and numProgram = '4' and numUnit is null) " & _
                                        "group by numUserID) UnitStaff    " & _
                                        "where AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCPItemMaster.strResponsibleStaff  " & _
                                        "and AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCPItemMaster.strAIRSNumber  " & _
                                        "and AIRBranch.LookUPComplianceActivities.strActivityType = AIRBranch.SSCPItemMaster.strEventType " & _
                                        "and DatCompleteDate is Null   " & _
                                        "and strResponsibleStaff = UnitStaff.numUserID " & _
                                        "and strDelete is Null "
                                    Else
                                        UserGridStyle = "LoadSSCPTertiaryWork"
                                        'SQL = "Select " & _
                                        '"to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                                        '"substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                                        '"(strLastName||', '||strFirstName) as Staff,  " & _
                                        '"strResponsibleStaff, " & _
                                        '"to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived, " & _
                                        '"strFacilityName, StrActivityName    " & _
                                        '"from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                                        '"AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,   " & _
                                        '"AIRBranch.SSCPFacilityAssignment " & _
                                        '"where AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCPItemMaster.strResponsibleStaff  " & _
                                        '"and AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCPItemMaster.strAIRSNumber  " & _
                                        '"and AIRBranch.LookUPComplianceActivities.strActivityType = AIRBranch.SSCPItemMaster.strEventType  " & _
                                        '" and AIRBranch.SSCPItemMaster.strAIRSnumber = AIRBranch.SSCPFacilityAssignment.strAIRSNumber  " & _
                                        '"and (strResponsibleStaff = '" & UserGCode & "' or strSSCPEngineer = '" & UserGCode & "') " & _
                                        '"and DatCompleteDate is Null  " & _
                                        ' "and strDelete is Null "

                                        SQL = "Select " & _
                                       "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                                       "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                                       "(strLastName||', '||strFirstName) as Staff,  " & _
                                       "strResponsibleStaff, " & _
                                       "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived, " & _
                                       "AIRBranch.APBFacilityInformation.strFacilityName, StrActivityName    " & _
                                       "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                                       "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,   " & _
                                       "AIRBranch.VW_SSCPInspection_List " & _
                                       "where AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSCPItemMaster.strResponsibleStaff  " & _
                                       "and AIRBranch.APBFacilityInformation.strAIRSNumber = AIRBranch.SSCPItemMaster.strAIRSNumber  " & _
                                       "and AIRBranch.LookUPComplianceActivities.strActivityType = AIRBranch.SSCPItemMaster.strEventType  " & _
                                       " and AIRBranch.SSCPItemMaster.strAIRSnumber = '0413'||AIRBranch.VW_SSCPInspection_List.AIRSNumber  " & _
                                       "and (strResponsibleStaff = '" & UserGCode & "' or numSSCPEngineer = '" & UserGCode & "') " & _
                                       "and DatCompleteDate is Null  " & _
                                        "and strDelete is Null "
                                    End If
                                End If
                            End If
                        Case "5" 'SSPP
                            If WorkUnit = "---" Then 'Program Manager

                            Else
                                If AccountArray(24, 3) = "1" Then 'Unit Manager

                                Else
                                    If AccountArray(9, 3) = "1" Then 'Administrative 2

                                    Else

                                    End If
                                End If
                            End If
                        Case "6" 'Ambient

                    End Select
                Case "2" 'Watershed Protection

                Case "3" 'Hazard Waste

                Case "4" 'Land Protection

                Case "5" 'Program Coordination 
                    Select Case WorkProgram
                        Case "7", "8", "9", "10", "11", "12", "13", "14", "15" 'Distirct 
                            If WorkUnit = "---" Then 'Program Manager 

                            Else

                            End If
                    End Select
                Case "6" 'Directors Office 

            End Select

            dsOpenWork = New DataSet
            daOpenWork = New OracleDataAdapter(SQL, Conn)
            If SQL <> "" Then
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                daOpenWork.Fill(dsOpenWork, "OpenWork")
            End If
        Catch ex As Exception
            ErrorReport(SQL & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadQuaternaryWork(ByVal WorkBranch As String, ByVal WorkProgram As String, ByVal WorkUnit As String)
        Try

            Select Case WorkBranch
                Case "1" 'Air Protection Branch
                    Select Case WorkProgram
                        Case "1" 'Mobile & Area

                        Case "2" 'Planning & Support

                        Case "3" 'ISMP
                            If WorkUnit = "---" Then 'Program Manager

                            Else
                                If AccountArray(17, 3) = "1" Then  'Unit Manager

                                Else

                                End If
                            End If
                        Case "4" 'SSCP
                            If WorkUnit = "---" Then 'Program Manager
                                UserGridStyle = "LoadSSCPQuaternaryWork"
                                SQL = "select distinct(substr(AIRBranch.APBHeaderData.strAIRSNumber, 5)) as AIRSnumber, " & _
                               "strFacilityName " & _
                               "from AIRBranch.APBHeaderData, AIRBranch.APBFacilityInformation  " & _
                               "where (Not exists (select * " & _
                               "from AIRBranch.APBSubpartData " & _
                               "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                               "and substr(strSubPartKey, 13, 1) = 'M') " & _
                               "and subStr(strAirProgramCodes, 12, 1) = '1' " & _
                               "or Not exists (select * " & _
                               "from AIRBranch.APBSubpartData " & _
                               "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                               "and substr(strSubPartKey, 13, 1) = '9') " & _
                               "and subStr(strAirProgramCodes, 8, 1) = '1' " & _
                               "or Not exists (select * " & _
                               "from AIRBranch.APBSubpartData " & _
                               "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                               "and substr(strSubPartKey, 13, 1) = '8') " & _
                               "and subStr(strAirProgramCodes, 7, 1) = '1' ) " & _
                               "and AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBFacilityInformation.strAIRsnumber " & _
                               "and AIRBranch.APBHeaderData.strOperationalStatus <> 'X' " & _
                               "order by AIRSNumber "
                            Else
                                If AccountArray(22, 3) = "1" Then 'Unit Manager
                                    UserGridStyle = "LoadSSCPQuaternaryWork"
                                    SQL = "select distinct(substr(AIRBranch.APBHeaderData.strAIRSNumber, 5)) as AIRSnumber, " & _
                                    "strFacilityName " & _
                                    "from AIRBranch.APBHeaderData, AIRBranch.APBFacilityInformation  " & _
                                    "where (Not exists (select * " & _
                                    "from AIRBranch.APBSubpartData " & _
                                    "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                                    "and substr(strSubPartKey, 13, 1) = 'M') " & _
                                    "and subStr(strAirProgramCodes, 12, 1) = '1' " & _
                                    "or Not exists (select * " & _
                                    "from AIRBranch.APBSubpartData " & _
                                    "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                                    "and substr(strSubPartKey, 13, 1) = '9') " & _
                                    "and subStr(strAirProgramCodes, 8, 1) = '1' " & _
                                    "or Not exists (select * " & _
                                    "from AIRBranch.APBSubpartData " & _
                                    "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                                    "and substr(strSubPartKey, 13, 1) = '8') " & _
                                    "and subStr(strAirProgramCodes, 7, 1) = '1' ) " & _
                                    "and AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBFacilityInformation.strAIRsnumber " & _
                                    "and AIRBranch.APBHeaderData.strOperationalStatus <> 'X' " & _
                                    "order by AIRSNumber "
                                Else
                                    If AccountArray(10, 3) = "1" Then 'District Liason
                                        UserGridStyle = "LoadSSCPQuaternaryWork"
                                        SQL = "select distinct(substr(AIRBranch.APBHeaderData.strAIRSNumber, 5)) as AIRSnumber, " & _
                                         "strFacilityName " & _
                                         "from AIRBranch.APBHeaderData, AIRBranch.APBFacilityInformation  " & _
                                         "where (Not exists (select * " & _
                                         "from AIRBranch.APBSubpartData " & _
                                         "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                                         "and substr(strSubPartKey, 13, 1) = 'M') " & _
                                         "and subStr(strAirProgramCodes, 12, 1) = '1' " & _
                                         "or Not exists (select * " & _
                                         "from AIRBranch.APBSubpartData " & _
                                         "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                                         "and substr(strSubPartKey, 13, 1) = '9') " & _
                                         "and subStr(strAirProgramCodes, 8, 1) = '1' " & _
                                         "or Not exists (select * " & _
                                         "from AIRBranch.APBSubpartData " & _
                                         "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                                         "and substr(strSubPartKey, 13, 1) = '8') " & _
                                         "and subStr(strAirProgramCodes, 7, 1) = '1' ) " & _
                                         "and AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBFacilityInformation.strAIRsnumber " & _
                                         "and AIRBranch.APBHeaderData.strOperationalStatus <> 'X' " & _
                                         "order by AIRSNumber "
                                    Else
                                        UserGridStyle = "LoadSSCPQuaternaryWork"
                                        SQL = "select distinct(substr(AIRBranch.APBHeaderData.strAIRSNumber, 5)) as AIRSnumber, " & _
                                        "strFacilityName " & _
                                        "from AIRBranch.APBHeaderData, AIRBranch.APBFacilityInformation  " & _
                                        "where (Not exists (select * " & _
                                        "from AIRBranch.APBSubpartData " & _
                                        "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                                        "and substr(strSubPartKey, 13, 1) = 'M') " & _
                                        "and subStr(strAirProgramCodes, 12, 1) = '1' " & _
                                        "or Not exists (select * " & _
                                        "from AIRBranch.APBSubpartData " & _
                                        "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                                        "and substr(strSubPartKey, 13, 1) = '9') " & _
                                        "and subStr(strAirProgramCodes, 8, 1) = '1' " & _
                                        "or Not exists (select * " & _
                                        "from AIRBranch.APBSubpartData " & _
                                        "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                                        "and substr(strSubPartKey, 13, 1) = '8') " & _
                                        "and subStr(strAirProgramCodes, 7, 1) = '1' ) " & _
                                        "and AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBFacilityInformation.strAIRsnumber " & _
                                        "and AIRBranch.APBHeaderData.strOperationalStatus <> 'X' " & _
                                        "order by AIRSNumber "
                                    End If
                                End If
                            End If
                        Case "5" 'SPP
                            If WorkUnit = "---" Then 'Program Manager

                            Else
                                If AccountArray(24, 3) = "1" Then 'Unit Manager

                                Else
                                    If AccountArray(9, 3) = "1" Then 'Administrative 2

                                    Else

                                    End If
                                End If
                            End If
                        Case "6" 'Ambient

                    End Select
                Case "2" 'Watershed Protection

                Case "3" 'Hazard Waste

                Case "4" 'Land Protection

                Case "5" 'Program Coordination 
                    Select Case WorkProgram
                        Case "7", "8", "9", "10", "11", "12", "13", "14", "15" 'Distirct 
                            If WorkUnit = "---" Then 'Program Manager 

                            Else

                            End If
                    End Select
                Case "6" 'Directors Office 

            End Select

            dsOpenWork = New DataSet
            daOpenWork = New OracleDataAdapter(SQL, Conn)
            If SQL <> "" Then
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                daOpenWork.Fill(dsOpenWork, "OpenWork")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)

        End Try
    End Sub
    Sub OpenApplication()
        Try

            If txtApplicationNumber.Text <> "" Then
                SQL = "select strApplicationNumber " & _
                "from AIRBranch.SSPPApplicationMaster " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    If PermitTrackingLog Is Nothing Then
                        PermitTrackingLog = Nothing
                        If PermitTrackingLog Is Nothing Then PermitTrackingLog = New SSPPApplicationTrackingLog
                        PermitTrackingLog.Show()
                        ' APB310 = Nothing
                    Else
                        PermitTrackingLog.Show()
                    End If
                    PermitTrackingLog.txtApplicationNumber.Clear()
                    PermitTrackingLog.txtApplicationNumber.Text = txtApplicationNumber.Text
                    PermitTrackingLog.LoadApplication()
                    PermitTrackingLog.BringToFront()
                    'PermitTrackingLog.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    PermitTrackingLog.TPTrackingLog.Focus()

                Else
                    MsgBox("Application Number is not in the system.", MsgBoxStyle.Information, "Navigation Screen")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub OpenTestReport()
        Try

            'If txtReferenceNumber.Text <> "" Then
            '    SQL = "select AIRBranch.ISMPDocumentType.strDocumentType " & _
            '     "from AIRBranch.ISMPDocumentType, AIRBranch.ISMPReportInformation " & _
            '     "where AIRBranch.ISMPReportInformation.strDocumentType = AIRBranch.ISMPDocumentType.strKey and " & _
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
            '    Else
            '        MsgBox("Reference Number is not in the system.", MsgBoxStyle.Information, "Navigation Screen")
            '    End If
            'End If
            If txtReferenceNumber.Text <> "" And txtReferenceNumber.Text.Length <= 9 Then
                If UserProgram = "3" Then
                    SQL = "select AIRBranch.ISMPDocumentType.strDocumentType " & _
                    "from AIRBranch.ISMPDocumentType, AIRBranch.ISMPReportInformation " & _
                    "where AIRBranch.ISMPReportInformation.strDocumentType = AIRBranch.ISMPDocumentType.strKey and " & _
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
                    "from AIRBranch.ISMPReportInformation " & _
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
        End Try

    End Sub
    Sub OpenEnforcement()
        Try
            Dim enfNum As String = txtEnforcementNumber.Text
            If enfNum = "" Then Exit Sub
            If DAL.SSCP.EnforcementExists(enfNum) Then
                OpenMultiForm(NewSscpEnforcementAudit, enfNum)
            Else
                MsgBox("Enforcement number is not in the system.", MsgBoxStyle.Information, "Navigation Screen")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub OpenSSCPWork()
        Try

            If txtTrackingNumber.Text <> "" And IsNumeric(txtTrackingNumber.Text) Then
                SQL = "Select " & _
                "strTrackingNumber " & _
                "from AIRBranch.SSCPItemMaster " & _
                "where strTrackingNumber = '" & txtTrackingNumber.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then

                    Dim RefNum As String = ""
                    Dim DocType As String = ""

                    SQL = "Select " & _
                    "AIRBranch.ISMPReportInformation.strReferenceNumber, AIRBranch.ISMPDocumentType.strDocumentType " & _
                    "from AIRBranch.SSCPTestReports, AIRBranch.ISMPDocumentType, " & _
                    "AIRBranch.ISMPReportInformation " & _
                    "where AIRBranch.SSCPTestReports.strReferenceNumber = AIRBranch.ISMPReportInformation.strReferenceNumber " & _
                    "and AIRBranch.ISMPReportInformation.strDocumentType = AIRBranch.ISMPDocumentType.strKey " & _
                    "and strTrackingNumber = '" & txtTrackingNumber.Text & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        RefNum = dr.Item("strReferenceNumber")
                        DocType = dr.Item("strDocumentType")
                    Else
                        RefNum = ""
                        DocType = ""
                    End If
                    dr.Close()
                    If RefNum <> "" Then
                        ISMPTestReportsEntry = Nothing
                        If ISMPTestReportsEntry Is Nothing Then ISMPTestReportsEntry = New ISMPTestReports
                        ISMPTestReportsEntry.txtReferenceNumber.Text = RefNum
                        ISMPTestReportsEntry.Show()
                    Else
                        If SSCPREports Is Nothing Then
                            SSCPREports = Nothing
                            If SSCPREports Is Nothing Then SSCPREports = New SSCPEvents
                            SSCPREports.txtTrackingNumber.Text = txtTrackingNumber.Text
                            SSCPREports.Show()
                        Else
                            SSCPREports.Close()
                            SSCPREports = Nothing
                            If SSCPREports Is Nothing Then SSCPREports = New SSCPEvents
                            SSCPREports.txtTrackingNumber.Text = txtTrackingNumber.Text
                            SSCPREports.Show()
                        End If
                        'SSCPREports.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    End If
                Else
                    MsgBox("Tracking Number is not in the system.", MsgBoxStyle.Information, "Navigation Screen")
                End If
            Else
                MsgBox("Tracking Number is not in the system.", MsgBoxStyle.Information, "Navigation Screen")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)

        End Try

    End Sub
    Sub OpenFacilitySummary()
        Try

            If txtAIRSNumber.Text <> "" And txtAIRSNumber.Text.Length = 8 Then
                SQL = "Select strAIRSNumber " & _
                "from AIRBranch.APBMasterAIRS " & _
                "where strAIRSnumber = '0413" & txtAIRSNumber.Text & "' "


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
                        FacilitySummary.mtbAIRSNumber.Text = txtAIRSNumber.Text
                        FacilitySummary.Show()
                    Else
                        FacilitySummary.mtbAIRSNumber.Text = txtAIRSNumber.Text
                        FacilitySummary.Show()
                    End If
                    'FacilitySummary.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    FacilitySummary.LoadInitialData()

                Else
                    MsgBox("AIRS Number is not in the system.", MsgBoxStyle.Information, "Navigation Screen")
                End If
            Else
                MsgBox("AIRS Number is not in the system.", MsgBoxStyle.Information, "Navigation Screen")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)

        End Try

    End Sub
    Sub OpenNewForm(ByVal Source As String, ByVal Options As String)
        Try
            Select Case Source
                Case "Facility Summary" '1
                    If FacilitySummary Is Nothing Then
                        If FacilitySummary Is Nothing Then FacilitySummary = New IAIPFacilitySummary
                    Else
                        FacilitySummary.Show()
                    End If
                    FacilitySummary.Show()
                    'FacilitySummary.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "DMU Tools" '2
                    'If ISMPDMU Is Nothing Then
                    '    If ISMPDMU Is Nothing Then ISMPDMU = New ISMPDataManagementTools
                    'Else
                    '    ISMPDMU.Dispose()
                    '    ISMPDMU = New ISMPDataManagementTools
                    '    If ISMPDMU Is Nothing Then ISMPDMU = New ISMPDataManagementTools
                    'End If
                    'ISMPDMU.Show()
                    'ISMPDMU.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Application Log" '3
                    If ApplicationLog Is Nothing Then
                        If ApplicationLog Is Nothing Then ApplicationLog = New SSPPApplicationLog
                    Else
                        ApplicationLog.Dispose()
                        ApplicationLog = New SSPPApplicationLog
                    End If
                    ApplicationLog.Show()
                    'ApplicationLog.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Compliance Log" '4
                    If SSCP_Work Is Nothing Then
                        If SSCP_Work Is Nothing Then SSCP_Work = New SSCPComplianceLog
                    Else
                        SSCP_Work.Dispose()
                        SSCP_Work = New SSCPComplianceLog
                    End If
                    SSCP_Work.Show()
                    'SSCP_Work.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Monitoring Log" '5
                    If ISMPReportViewer Is Nothing Then
                        If ISMPReportViewer Is Nothing Then ISMPReportViewer = New ISMPMonitoringLog
                    Else
                        ISMPReportViewer.Dispose()
                        ISMPReportViewer = New ISMPMonitoringLog
                    End If
                    ISMPReportViewer.Show()
                    'ISMPReportViewer.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Fees Log"
                    If FeesLog Is Nothing Then
                        If FeesLog Is Nothing Then FeesLog = New PASPFeesLog
                    Else
                        FeesLog.Dispose()
                        FeesLog = New PASPFeesLog
                    End If
                    FeesLog.Show()
                    'FeesLog.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Fee Management"
                    If FeeManagement Is Nothing Then
                        If FeeManagement Is Nothing Then FeeManagement = New PASPFeeManagement
                    Else
                        FeeManagement.Dispose()
                        FeeManagement = New PASPFeeManagement
                    End If
                    FeeManagement.Show()
                    'FeeManagement.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

                Case "Fee Statistics && Reports" ''"Fee Statistics && Mailout" '"Mailout && Statistics" '12
                    If MailoutAndStats Is Nothing Then
                        If MailoutAndStats Is Nothing Then MailoutAndStats = New PASPFeeStatistics
                    Else
                        MailoutAndStats.Dispose()
                        MailoutAndStats = New PASPFeeStatistics
                    End If
                    MailoutAndStats.Show()
                    'MailoutAndStats.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

                Case "IAIP Query Generator" '7
                    If DevSQLQuery Is Nothing Then
                        If DevSQLQuery Is Nothing Then DevSQLQuery = New IAIPQueryGenerator
                    Else
                        DevSQLQuery.Dispose()
                        DevSQLQuery = New IAIPQueryGenerator
                    End If
                    DevSQLQuery.Show()
                    'DevSQLQuery.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Profile Management"  ' 8
                    If UserAdminTool Is Nothing Then
                        If UserAdminTool Is Nothing Then UserAdminTool = New IAIPUserAdminTool
                    Else
                        UserAdminTool.Dispose()
                        UserAdminTool = New IAIPUserAdminTool
                    End If
                    UserAdminTool.Show()
                    'UserAdminTool.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Permit File Uploader" '9
                    If PermitUploader Is Nothing Then
                        If PermitUploader Is Nothing Then PermitUploader = New IAIPPermitUploader
                    Else
                        PermitUploader.Show()
                    End If
                    PermitUploader.Show()
                    'PermitUploader.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "District Tools" '10
                    If IAIPDistrictTool Is Nothing Then
                        If IAIPDistrictTool Is Nothing Then IAIPDistrictTool = New IAIPDistrictSourceTool
                    Else
                        IAIPDistrictTool.Dispose()
                        IAIPDistrictTool = New IAIPDistrictSourceTool
                    End If
                    IAIPDistrictTool.Show()
                    'IAIPDistrictTool.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "AFS Validator" '11
                    If Validator Is Nothing Then
                        If Validator Is Nothing Then Validator = New AFSValidator
                    Else
                        Validator.Dispose()
                        Validator = New AFSValidator
                    End If
                    Validator.Show()
                    'Validator.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Fees Reports" '6
                    If FeesReports Is Nothing Then
                        If FeesReports Is Nothing Then FeesReports = New PASPFeeReports
                    Else
                        FeesReports.Dispose()
                        FeesReports = New PASPFeeReports
                    End If
                    FeesReports.Show()
                    'FeesReports.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

                Case "APB Branch Tools" '13
                    If PrintOut Is Nothing Then
                        If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
                    Else
                        PrintOut.Dispose()
                        PrintOut = New IAIPPrintOut
                    End If
                    PrintOut.txtPrintType.Text = "OrgChart"

                    PrintOut.Show()
                    'PrintOut.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Test Report Information" '14
                    If ISMPFacility Is Nothing Then
                        If ISMPFacility Is Nothing Then ISMPFacility = New ISMPTestReportAdministrative
                    Else
                        ISMPFacility.Dispose()
                        ISMPFacility = New ISMPTestReportAdministrative
                    End If
                    ISMPFacility.Show()
                    'ISMPFacility.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Memo Viewer" '15
                    If ISMPMemoViewer Is Nothing Then
                        If ISMPMemoViewer Is Nothing Then ISMPMemoViewer = New ISMPTestMemoViewer
                    Else
                        ISMPMemoViewer.Dispose()
                        ISMPMemoViewer = New ISMPTestMemoViewer
                    End If
                    ISMPMemoViewer.Show()
                    'ISMPMemoViewer.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Ref. Number Management" '16
                    If ISMPRefNum Is Nothing Then
                        If ISMPRefNum Is Nothing Then ISMPRefNum = New ISMPReferenceNumber
                    Else
                        ISMPRefNum.Dispose()
                        ISMPRefNum = New ISMPReferenceNumber
                    End If
                    ISMPRefNum.Show()
                    'ISMPRefNum.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "ISMP Managers" '17
                    If ISMPManagers Is Nothing Then
                        If ISMPManagers Is Nothing Then ISMPManagers = New ISMPManagersTools
                    Else
                        ISMPManagers.Dispose()
                        ISMPManagers = New ISMPManagersTools
                    End If
                    ISMPManagers.txtProgram.Text = "Industrial Source Monitoring"
                    ISMPManagers.Show()
                    'ISMPManagers.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Deposits" '18
                    If DepositsAmendments Is Nothing Then
                        If DepositsAmendments Is Nothing Then DepositsAmendments = New PASPDepositsAmendments
                    Else
                        DepositsAmendments.Dispose()
                        DepositsAmendments = New PASPDepositsAmendments
                    End If
                    DepositsAmendments.Show()
                    'DepositsAmendments.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Attainment Status Tool" '19
                    If AttainmentStatus Is Nothing Then
                        If AttainmentStatus Is Nothing Then AttainmentStatus = New SSPPAttainmentStatus
                    Else
                        AttainmentStatus.Dispose()
                        AttainmentStatus = New SSPPAttainmentStatus
                    End If
                    AttainmentStatus.Show()
                    'AttainmentStatus.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Emissions Summary Tool" '20
                    If EmissionSummary Is Nothing Then
                        If EmissionSummary Is Nothing Then EmissionSummary = New SSCPEmissionSummaryTool
                    Else
                        EmissionSummary.Dispose()
                        EmissionSummary = New SSCPEmissionSummaryTool
                    End If
                    EmissionSummary.Show()
                    'EmissionSummary.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Inspection Tool" '21
                    MsgBox("This tool is temporary disabled", MsgBoxStyle.Information, Me.Text)
                    Exit Sub

                    If InspectionTool Is Nothing Then
                        If InspectionTool Is Nothing Then InspectionTool = New SSCPInspectionTool
                    Else
                        InspectionTool.Dispose()
                        InspectionTool = New SSCPInspectionTool
                    End If
                    InspectionTool.Show()
                    'InspectionTool.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    Exit Sub


                    If SSCPInspectionsTool Is Nothing Then
                        If SSCPInspectionsTool Is Nothing Then SSCPInspectionsTool = New SSCPEngineerInspectionTool
                    Else
                        SSCPInspectionsTool.Dispose()
                        SSCPInspectionsTool = New SSCPEngineerInspectionTool
                    End If
                    SSCPInspectionsTool.Show()
                    'SSCPInspectionsTool.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Compliance Managers" '22
                    If ManagersTools Is Nothing Then
                        If ManagersTools Is Nothing Then ManagersTools = New SSCPManagersTools
                    Else
                        ManagersTools.Dispose()
                        ManagersTools = New SSCPManagersTools
                    End If
                    ManagersTools.Show()
                    'ManagersTools.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "PA/PN Report" '23
                    If PublicLetter2 Is Nothing Then
                        If PublicLetter2 Is Nothing Then PublicLetter2 = New SSPPPublicNoticiesAndAdvisories
                    Else
                        PublicLetter2.Dispose()
                        PublicLetter2 = New SSPPPublicNoticiesAndAdvisories
                    End If
                    PublicLetter2.Show()
                    'PublicLetter2.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "SSPP Tools" '24
                    If StatisticalTools Is Nothing Then
                        If StatisticalTools Is Nothing Then StatisticalTools = New SSPPStatisticalTools
                    Else
                        StatisticalTools.Show()
                    End If
                    StatisticalTools.Show()
                    'StatisticalTools.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Fee Tools"
                    If FeeTools Is Nothing Then
                        If FeeTools Is Nothing Then FeeTools = New PASPFeeTools
                    Else
                        FeeTools.Dispose()
                        FeeTools = New PASPFeeTools
                    End If
                    FeeTools.Show()
                    'FeeTools.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    'Case "GAIT Inventory"
                    '    If ComputerInventory Is Nothing Then
                    '        If ComputerInventory Is Nothing Then ComputerInventory = New PASPInventory
                    '    Else
                    '        ComputerInventory.Dispose()
                    '        ComputerInventory = New PASPInventory
                    '    End If
                    '    ComputerInventory.Show()
                    '    ComputerInventory.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "DMU Only Tool" '25
                    If (UserGCode = "1" Or UserGCode = "345") Then
                        If DMUOnly Is Nothing Then
                            If DMUOnly Is Nothing Then DMUOnly = New DMUTool
                        Else
                            DMUOnly.Dispose()
                            DMUOnly = New DMUTool
                        End If
                        DMUOnly.Show()
                        'DMUOnly.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    Else
                        MsgBox("ACCESS DENIED")
                    End If
                Case "Smoke School" '26 
                    'If SmokeSchool IsNot Nothing Then
                    '    SmokeSchool.Dispose()
                    'End If
                    'SmokeSchool = New SmokeSchool
                    With SmokeSchool
                        .Show()
                        '.WindowState = FormWindowState.Normal
                        .Activate()
                        '.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    End With
                Case "AFS Tools"
                    If DevelopersTools Is Nothing Then
                        If DevelopersTools Is Nothing Then DevelopersTools = New DMUDeveloperTools
                    Else
                        DevelopersTools.Dispose()
                        DevelopersTools = New DMUDeveloperTools
                    End If
                    DevelopersTools.Show()
                    'DevelopersTools.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "DMU Staff Tools"
                    If StaffTools Is Nothing Then
                        If StaffTools Is Nothing Then StaffTools = New DMUStaffTools
                    Else
                        StaffTools.Dispose()
                        StaffTools = New DMUStaffTools
                    End If
                    StaffTools.Show()
                    'StaffTools.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Title V Tools"
                    If TitleVTools Is Nothing Then
                        If TitleVTools Is Nothing Then TitleVTools = New DMUTitleVTools
                    Else
                        TitleVTools.Dispose()
                        TitleVTools = New DMUTitleVTools
                    End If
                    TitleVTools.Show()
                    'TitleVTools.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "AFS Compare Tool"
                    If AFSCompare Is Nothing Then
                        If AFSCompare Is Nothing Then AFSCompare = New IAIPAFSCompare
                    Else
                        AFSCompare.Dispose()
                        AFSCompare = New IAIPAFSCompare
                    End If
                    AFSCompare.Show()
                    'AFSCompare.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Look Up Tables"
                    If LookUpTables Is Nothing Then
                        If LookUpTables Is Nothing Then LookUpTables = New IAIPLookUpTables
                    Else
                        LookUpTables.Dispose()
                        LookUpTables = New IAIPLookUpTables
                    End If
                    LookUpTables.Show()
                    'LookUpTables.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Fees Audit Tool"
                    If FeeAuditTool Is Nothing Then
                        If FeeAuditTool Is Nothing Then FeeAuditTool = New IAIPFeeAuditTool
                    Else
                        FeeAuditTool.Dispose()
                        FeeAuditTool = New IAIPFeeAuditTool
                    End If
                    FeeAuditTool.Show()
                    'FeeAuditTool.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Compliance Admin"
                    If SSCPAdmin Is Nothing Then
                        If SSCPAdmin Is Nothing Then SSCPAdmin = New SSCPAdministrator
                    Else
                        SSCPAdmin.Dispose()
                        SSCPAdmin = New SSCPAdministrator
                    End If
                    SSCPAdmin.Show()
                    'SSCPAdmin.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "Registration Tool"
                    With MASPRegistrationTool
                        .Show()
                        .Activate()
                    End With
                    'If RegistrationTool Is Nothing Then
                    '    If RegistrationTool Is Nothing Then RegistrationTool = New MASPRegistrationTool
                    'Else
                    '    RegistrationTool.Dispose()
                    '    RegistrationTool = New MASPRegistrationTool
                    'End If
                    'RegistrationTool.Show()
                    'RegistrationTool.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case "EIS Log"
                    If EISLog Is Nothing Then
                        If EISLog Is Nothing Then EISLog = New IAIP_EIS_Log
                    Else
                        EISLog.Dispose()
                        EISLog = New IAIP_EIS_Log
                    End If
                    EISLog.Show()
                    'EISLog.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

                Case "Enforcement Documents"
                    With SscpDocuments
                        .Show()
                        .Activate()
                    End With

                Case Else
                    MsgBox(Source.ToString, MsgBoxStyle.Information, "IAIP Navigation")
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Main Menu Items"

    Private Sub NavigationScreen_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Conn.Dispose()
        Application.Exit()
    End Sub

    Private Sub MmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiExit.Click
        Me.Close()
    End Sub

#End Region
    Private Sub LLSelectReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LLSelectReport.LinkClicked
        Try

            OpenTestReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbEnforcementRecord_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEnforcementRecord.LinkClicked
        Try
            OpenEnforcement()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)

        End Try

    End Sub
    Private Sub llbOpenApplication_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbOpenApplication.LinkClicked
        Try

            OpenApplication()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)

        End Try

    End Sub
    Private Sub llbPrimaryList_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbPrimaryList.LinkClicked
        Try

            Paneltemp1 = pnl1.Text
            pnl1.Text = ""
            ProgressBar.Value = 0
            lblMessageLabel.Text = "Loading...."
            dgvWorkViewer.Visible = False
            UserRequest = "Primary"

            If bgrLongProcess.IsBusy Then
                bgrLongProcess.CancelAsync()
            Else
                bgrLongProcess.WorkerReportsProgress = True
                bgrLongProcess.WorkerSupportsCancellation = True
                bgrLongProcess.RunWorkerAsync()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


        pnl1.Text = Paneltemp1
    End Sub
    Private Sub llbSecondaryList_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbSecondaryList.LinkClicked
        Try

            Paneltemp1 = pnl1.Text
            pnl1.Text = ""
            lblMessageLabel.Text = "Loading...."
            dgvWorkViewer.Visible = False
            UserRequest = "Secondary"

            If bgrLongProcess.IsBusy Then
                bgrLongProcess.CancelAsync()
            Else
                bgrLongProcess.WorkerReportsProgress = True
                bgrLongProcess.WorkerSupportsCancellation = True
                bgrLongProcess.RunWorkerAsync()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


        pnl1.Text = Paneltemp1
    End Sub
    Private Sub llbTerteraryList_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbTertiaryList.LinkClicked
        Try

            Paneltemp1 = pnl1.Text
            pnl1.Text = ""
            lblMessageLabel.Text = "Loading...."
            dgvWorkViewer.Visible = False
            UserRequest = "Tertiary"

            If bgrLongProcess.IsBusy Then
                bgrLongProcess.CancelAsync()
            Else
                bgrLongProcess.WorkerReportsProgress = True
                bgrLongProcess.WorkerSupportsCancellation = True
                bgrLongProcess.RunWorkerAsync()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

        pnl1.Text = Paneltemp1
    End Sub
    Private Sub llbQuaternaryList_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbQuaternaryList.LinkClicked
        Try

            Paneltemp1 = pnl1.Text
            pnl1.Text = ""
            lblMessageLabel.Text = "Loading...."
            dgvWorkViewer.Visible = False
            UserRequest = "Quaternary"

            If bgrLongProcess.IsBusy Then
                bgrLongProcess.CancelAsync()
            Else
                bgrLongProcess.WorkerReportsProgress = True
                bgrLongProcess.WorkerSupportsCancellation = True
                bgrLongProcess.RunWorkerAsync()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


        pnl1.Text = Paneltemp1
    End Sub
    Private Sub llbTrackingNumber_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbTrackingNumber.LinkClicked
        Try

            OpenSSCPWork()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbFacilitySummary_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFacilitySummary.LinkClicked
        Try

            OpenFacilitySummary()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)

        End Try

    End Sub
    Private Sub txtApplicationNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtApplicationNumber.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                OpenApplication()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)

        End Try

    End Sub
    Private Sub txtEnforcementNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEnforcementNumber.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                OpenEnforcement()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)

        End Try

    End Sub
    Private Sub txtReferenceNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReferenceNumber.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                OpenTestReport()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)

        End Try

    End Sub
    Private Sub txtTrackingNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTrackingNumber.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                OpenSSCPWork()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    'Private Sub mmiResetPointer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiResetPointer.Click


    'End Sub

    Private Sub mmiAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiAbout.Click
        OpenAboutUrl(Me)
        'Try
        '    DevelopmentTeam = Nothing
        '    If DevelopmentTeam Is Nothing Then DevelopmentTeam = New IAIPDevelopmentTeam
        '    DevelopmentTeam.Show()
        '    DevelopmentTeam.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        'Catch ex As Exception
        '    ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        'End Try
    End Sub
    Private Sub dgvWorkViewer_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvWorkViewer.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvWorkViewer.HitTest(e.X, e.Y)

        Try


            If dgvWorkViewer.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvWorkViewer.Columns(0).HeaderText = "Reference #" Then
                    txtReferenceNumber.Text = dgvWorkViewer(0, hti.RowIndex).Value
                    txtAIRSNumber.Text = dgvWorkViewer(1, hti.RowIndex).Value
                End If

                If dgvWorkViewer.Columns(0).HeaderText = "APL #" Then
                    txtApplicationNumber.Text = dgvWorkViewer(0, hti.RowIndex).Value
                    txtAIRSNumber.Text = dgvWorkViewer(1, hti.RowIndex).Value
                End If
                If dgvWorkViewer.Columns(0).HeaderText = "Enforcement #" Then
                    txtEnforcementNumber.Text = dgvWorkViewer(0, hti.RowIndex).Value
                    txtAIRSNumber.Text = dgvWorkViewer(1, hti.RowIndex).Value
                    txtAIRSNumber.Text = dgvWorkViewer(1, hti.RowIndex).Value
                End If
                If dgvWorkViewer.Columns(0).HeaderText = "Tracking #" Then
                    txtTrackingNumber.Text = dgvWorkViewer(0, hti.RowIndex).Value
                    txtAIRSNumber.Text = dgvWorkViewer(1, hti.RowIndex).Value
                End If
                If dgvWorkViewer.Columns(0).HeaderText = "AIRS #" Then
                    txtAIRSNumber.Text = dgvWorkViewer(0, hti.RowIndex).Value
                End If
                If dgvWorkViewer.Columns(0).HeaderText = "Test Log #" Then
                    txtTestLogNumber.Text = dgvWorkViewer(0, hti.RowIndex).Value
                    txtAIRSNumber.Text = dgvWorkViewer(3, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub bgrLongProcess_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgrLongProcess.DoWork
        Select Case UserRequest
            Case "Primary"
                LoadOpenWork(WorkBranch, WorkProgram, WorkUnit)
            Case "Secondary"
                LoadSecondaryWork(WorkBranch, WorkProgram, WorkUnit)
            Case "Tertiary"
                LoadTertiaryWork(WorkBranch, WorkProgram, WorkUnit)
            Case "Quaternary"
                LoadQuaternaryWork(WorkBranch, WorkProgram, WorkUnit)
        End Select
    End Sub
    Private Sub bgrLongProcess_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgrLongProcess.RunWorkerCompleted
        GPWorkTool.Text = pnl1.Text

        If dsOpenWork.Tables.Count > 0 Then
            dgvWorkViewer.DataSource = dsOpenWork
            dgvWorkViewer.DataMember = "OpenWork"
            dgvWorkViewer.Visible = True
        Else
            dgvWorkViewer.DataSource = dsOpenWork
            Me.lblMessageLabel.Text = "..."
        End If
        txtDataGridCount.Text = dgvWorkViewer.RowCount

        Select Case UserGridStyle
            Case "LoadISMPTestReports"
                LoadISMPTestReports()
            Case "LoadSSCPOpenWork"
                LoadSSCPOpenWork()
            Case "LoadSSPPOpenWork"
                LoadSSPPOpenWork()
            Case "LoadSSCPTertiaryWork"
                LoadSSCPTertiaryWork()
            Case "LoadSSCPQuaternaryWork"
                LoadSSCPQuaternaryWork()
            Case "LoadSecondaryWork2"
                LoadSecondaryWork2()
            Case "LoadISMPTertiaryWork"
                LoadISMPTertiaryWork()
            Case Else

                dgvWorkViewer.DataSource = dsOpenWork
                ' dgvWorkViewer.DataMember = "OpenWork"

                dgvWorkViewer.RowHeadersVisible = False
                dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvWorkViewer.AllowUserToResizeColumns = True
                dgvWorkViewer.AllowUserToAddRows = False
                dgvWorkViewer.AllowUserToDeleteRows = False
                dgvWorkViewer.AllowUserToOrderColumns = True
                dgvWorkViewer.AllowUserToResizeRows = True
                dgvWorkViewer.ColumnHeadersHeight = "35"
                'dgvWorkViewer.Columns("strReferenceNumber").HeaderText = "Reference #"
                'dgvWorkViewer.Columns("strReferenceNumber").DisplayIndex = 0
                'dgvWorkViewer.Columns("AIRSNumber").HeaderText = "AIRS #"
                'dgvWorkViewer.Columns("AIRSNumber").DisplayIndex = 1
                'dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
                'dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 2
                'dgvWorkViewer.Columns("strFacilityCity").HeaderText = "City"
                'dgvWorkViewer.Columns("strFacilityCity").DisplayIndex = 3
                'dgvWorkViewer.Columns("strCountyName").HeaderText = "County"
                'dgvWorkViewer.Columns("strCountyName").DisplayIndex = 4
                'dgvWorkViewer.Columns("strEmissionSource").HeaderText = "Emission Source"
                'dgvWorkViewer.Columns("strEmissionSource").DisplayIndex = 5
                'dgvWorkViewer.Columns("strPollutantDescription").HeaderText = "Pollutant"
                'dgvWorkViewer.Columns("strPollutantDescription").DisplayIndex = 6
                'dgvWorkViewer.Columns("strReportType").HeaderText = "Report Type"
                'dgvWorkViewer.Columns("strReportType").DisplayIndex = 7
                'dgvWorkViewer.Columns("strDocumentType").HeaderText = "Document Type"
                'dgvWorkViewer.Columns("strDocumentType").DisplayIndex = 8
                'dgvWorkViewer.Columns("ReviewingEngineer").HeaderText = "Reviewing Engineer"
                'dgvWorkViewer.Columns("ReviewingEngineer").DisplayIndex = 9
                'dgvWorkViewer.Columns("TestDateStart").HeaderText = "Test Date"
                'dgvWorkViewer.Columns("TestDateStart").DefaultCellStyle.Format = "dd-MMM-yyyy"
                'dgvWorkViewer.Columns("TestDateStart").DisplayIndex = 10
                'dgvWorkViewer.Columns("ReceivedDate").HeaderText = "Received Date"
                'dgvWorkViewer.Columns("ReceivedDate").DisplayIndex = 11
                'dgvWorkViewer.Columns("ReceivedDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                'dgvWorkViewer.Columns("CompleteDate").HeaderText = "Complete Date"
                'dgvWorkViewer.Columns("CompleteDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                'dgvWorkViewer.Columns("CompleteDate").DisplayIndex = 12
                'dgvWorkViewer.Columns("Status").HeaderText = "Report Open/Closed"
                'dgvWorkViewer.Columns("Status").DisplayIndex = 13
                'dgvWorkViewer.Columns("strComplianceStatus").HeaderText = "Compliance Status"
                'dgvWorkViewer.Columns("strComplianceStatus").DisplayIndex = 14
                'dgvWorkViewer.Columns("mmoCommentAREA").HeaderText = "Comment Field"
                'dgvWorkViewer.Columns("mmoCommentAREA").DisplayIndex = 15
                'dgvWorkViewer.Columns("strPreComplianceStatus").HeaderText = "Precompliance Status"
                'dgvWorkViewer.Columns("strPreComplianceStatus").DisplayIndex = 16
                'dgvWorkViewer.Columns("strWitnessingEngineer").Visible = False
                'dgvWorkViewer.Columns("strWitnessingEngineer2").Visible = False
                'dgvWorkViewer.Columns("strUserUnit").Visible = False

        End Select

        If dgvWorkViewer.Visible = True Then
            dgvWorkViewer.SanelyResizeColumns()
        End If

    End Sub

#Region "Buttons"
    Private Sub btnNav1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav1.Click
        Try
            OpenNewForm(btnNav1.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav2.Click
        Try
            OpenNewForm(btnNav2.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav3.Click
        Try
            OpenNewForm(btnNav3.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav4.Click
        Try
            OpenNewForm(btnNav4.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav5.Click
        Try
            OpenNewForm(btnNav5.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav6.Click
        Try
            OpenNewForm(btnNav6.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav7.Click
        Try
            OpenNewForm(btnNav7.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav8.Click
        Try
            OpenNewForm(btnNav8.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav9.Click
        Try
            OpenNewForm(btnNav9.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav10.Click
        Try
            OpenNewForm(btnNav10.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav11.Click
        Try
            OpenNewForm(btnNav11.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav12.Click
        Try
            OpenNewForm(btnNav12.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav13_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNav13.Click
        Try
            OpenNewForm(btnNav13.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav14.Click
        Try
            OpenNewForm(btnNav14.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav15.Click
        Try
            OpenNewForm(btnNav15.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav16.Click
        Try
            OpenNewForm(btnNav16.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav17.Click
        Try
            OpenNewForm(btnNav17.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav18.Click
        Try
            OpenNewForm(btnNav18.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav19.Click
        Try
            OpenNewForm(btnNav19.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav20.Click
        Try
            OpenNewForm(btnNav20.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav21.Click
        Try
            OpenNewForm(btnNav21.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav22.Click
        Try
            OpenNewForm(btnNav22.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav23.Click
        Try
            OpenNewForm(btnNav23.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav24.Click
        Try
            OpenNewForm(btnNav24.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav25.Click
        Try
            OpenNewForm(btnNav25.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav26.Click
        Try
            OpenNewForm(btnNav26.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav27.Click
        Try
            OpenNewForm(btnNav27.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav28.Click
        Try
            OpenNewForm(btnNav28.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav29.Click
        Try
            OpenNewForm(btnNav29.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav30.Click
        Try
            OpenNewForm(btnNav30.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav31.Click
        Try
            OpenNewForm(btnNav31.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav32.Click
        Try
            OpenNewForm(btnNav32.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav33.Click
        Try
            OpenNewForm(btnNav33.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav34.Click
        Try
            OpenNewForm(btnNav34.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav35.Click
        Try
            OpenNewForm(btnNav35.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav36.Click
        Try
            OpenNewForm(btnNav36.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNav37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav37.Click
        Try
            OpenNewForm(btnNav37.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav38.Click
        Try
            OpenNewForm(btnNav38.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav39.Click
        Try
            OpenNewForm(btnNav39.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNav40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNav40.Click
        Try
            OpenNewForm(btnNav40.Text, "")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
    Private Sub mmiOnlineHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOnlineHelp.Click
        OpenHelpUrl(Me)
    End Sub

    Private Sub llbOpenTestLog_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbOpenTestLog.LinkClicked
        Try
            If Not IsNothing(DevTestLog) Then
                DevTestLog.txtTestNotificationNumber.Text = txtTestLogNumber.Text
                DevTestLog.Show()
            Else
                DevTestLog = Nothing
                If DevTestLog Is Nothing Then DevTestLog = New ISMPNotificationLog
                DevTestLog.txtTestNotificationNumber.Text = txtTestLogNumber.Text
                DevTestLog.Show()
            End If
            'DevTestLog.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            DevTestLog.LoadTestNotification()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgrFormLoad_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgrFormLoad.DoWork
        Try
            LoadButtons()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadButtons()
        Try
            Dim navTemp As String
            Dim AccountTemp As String = ""
            Dim accessTemp As String

            If Permissions <> "" Then
                AccountTemp = Permissions

                Do While AccountTemp <> ""
                    SQL = "Select " & _
                    "strFormAccess " & _
                    "From AIRBranch.LookUpIAIPAccounts " & _
                    "where numAccountCode = '" & Mid(AccountTemp, 2, (AccountTemp.IndexOf(")") - 1)) & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strFormAccess")) Then
                            AccountAccess = ""
                        Else
                            AccountAccess = dr.Item("strFormAccess")
                        End If
                    End While
                    dr.Close()

                    If AccountAccess <> "" Then
                        Do While AccountAccess <> ""
                            navTemp = False
                            For j = 0 To 4
                                If AccountArray(j, 0) = Mid(AccountAccess, 2, (AccountAccess.IndexOf("-") - 1)) Then
                                    navTemp = True
                                End If
                            Next
                            If navTemp = False Then
                                accessTemp = Mid(AccountAccess, 2, (AccountAccess.IndexOf("-") - 1))
                                AccountArray(Mid(AccountAccess, 2, (AccountAccess.IndexOf("-") - 1)), 0) = Mid(AccountAccess, 2, (AccountAccess.IndexOf("-") - 1))

                                If AccountArray(accessTemp, 1) = "1" Then
                                    AccountArray(Mid(AccountAccess, 2, (AccountAccess.IndexOf("-") - 1)), 1) = "1"
                                Else
                                    AccountArray(Mid(AccountAccess, 2, (AccountAccess.IndexOf("-") - 1)), 1) = Mid(AccountAccess, (AccountAccess.IndexOf("-") + 2), 1)
                                End If
                                If AccountArray(accessTemp, 2) = "1" Then
                                    AccountArray(Mid(AccountAccess, 2, (AccountAccess.IndexOf("-") - 1)), 2) = "1"
                                Else
                                    AccountArray(Mid(AccountAccess, 2, (AccountAccess.IndexOf("-") - 1)), 2) = Mid(AccountAccess, (AccountAccess.IndexOf("-") + 4), 1)
                                End If
                                If AccountArray(accessTemp, 3) = "1" Then
                                    AccountArray(Mid(AccountAccess, 2, (AccountAccess.IndexOf("-") - 1)), 3) = "1"
                                Else
                                    AccountArray(Mid(AccountAccess, 2, (AccountAccess.IndexOf("-") - 1)), 3) = Mid(AccountAccess, (AccountAccess.IndexOf("-") + 6), 1)
                                End If
                                If AccountArray(accessTemp, 4) = "1" Then
                                    AccountArray(Mid(AccountAccess, 2, (AccountAccess.IndexOf("-") - 1)), 4) = "1"
                                Else
                                    AccountArray(Mid(AccountAccess, 2, (AccountAccess.IndexOf("-") - 1)), 4) = Mid(AccountAccess, (AccountAccess.IndexOf("-") + 8), 1)
                                End If
                            End If
                            AccountAccess = Replace(AccountAccess, (Mid(AccountAccess, AccountAccess.IndexOf("(") + 1, AccountAccess.IndexOf(")") + 1)), "")
                        Loop
                    End If
                    AccountTemp = Replace(AccountTemp, ("(" & Mid(AccountTemp, 2, (AccountTemp.IndexOf(")") - 1)) & ")"), "")
                Loop
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgrFormLoad_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgrFormLoad.RunWorkerCompleted
        Try
            i = 0

            rdbStaffView.Visible = False
            rdbUCView.Visible = False
            rdbPMView.Visible = False

            If AccountArray(129, 3) = "1" Or _
                (AccountArray(24, 3) = "1" And AccountArray(3, 4) = "1" And AccountArray(12, 1) = "1" And AccountArray(12, 2) = "0") Or _
                (AccountArray(17, 3) = "1" And AccountArray(17, 4) = "0") Or _
                (AccountArray(22, 4) = "1" And AccountArray(22, 3) = "0") Then
                rdbPMView.Visible = True
                rdbUCView.Visible = True
                rdbStaffView.Visible = True
            Else
                rdbPMView.Visible = False
            End If

            If (AccountArray(24, 3) = "1" And AccountArray(12, 1) = "1" And AccountArray(12, 2) = "0" And AccountArray(3, 4) = "0") Or _
                (AccountArray(17, 1) = "0" And AccountArray(17, 2) = "1" And AccountArray(17, 3) = "1") Or _
                (AccountArray(22, 4) = "0" And AccountArray(22, 3) = "1") Then
                rdbUCView.Visible = True
                rdbStaffView.Visible = True
            End If

            'If AccountArray(4, 4) = "1" Then
            '    rdbUCView.Visible = True
            '    rdbStaffView.Visible = True
            'End If
            'If AccountArray(3, 3) = "1" Then
            '    rdbUCView.Visible = True
            '    rdbStaffView.Visible = True
            'End If
            'If AccountArray(4, 3) = "1" Then
            '    rdbUCView.Visible = True
            '    rdbStaffView.Visible = True
            'End If
            'If AccountArray(5, 4) = "1" Then
            '    rdbUCView.Visible = True
            '    rdbStaffView.Visible = True
            'End If
            'If AccountArray(5, 3) = "1" Then
            '    rdbUCView.Visible = True
            '    rdbStaffView.Visible = True
            'End If



            If AccountArray(1, 0) Is Nothing Then
            Else
                If AccountArray(1, 0) = "1" Then
                    If AccountArray(1, 1) = "1" Or AccountArray(1, 2) = "1" Or AccountArray(1, 3) = "1" Or AccountArray(1, 4) = "1" Then
                        btnNav1.Text = "Facility Summary"
                        btnNav1.Visible = True
                    End If
                End If
            End If
            If AccountArray(3, 0) Is Nothing Then
            Else
                If AccountArray(3, 0) = "3" Then
                    If AccountArray(3, 1) = "1" Or AccountArray(3, 2) = "1" Or AccountArray(3, 3) = "1" Or AccountArray(3, 4) = "1" Then
                        btnNav3.Text = "Application Log"
                        btnNav3.Visible = True
                    End If
                End If
            End If
            If AccountArray(4, 0) Is Nothing Then
            Else
                If AccountArray(4, 0) = "4" Then
                    If AccountArray(4, 1) = "1" Or AccountArray(4, 2) = "1" Or AccountArray(4, 3) = "1" Or AccountArray(4, 4) = "1" Then
                        btnNav4.Text = "Compliance Log"
                        btnNav4.Visible = True
                    End If
                End If
            End If
            If AccountArray(5, 0) Is Nothing Then
            Else
                If AccountArray(5, 0) = "5" Then
                    If AccountArray(5, 1) = "1" Or AccountArray(5, 2) = "1" Or AccountArray(5, 3) = "1" Or AccountArray(5, 4) = "1" Then
                        btnNav5.Text = "Monitoring Log"
                        btnNav5.Visible = True
                    End If
                End If
            End If
            If AccountArray(6, 0) Is Nothing Then
            Else
                If AccountArray(6, 0) = "6" Then
                    If AccountArray(6, 1) = "1" Or AccountArray(6, 2) = "1" Or AccountArray(6, 3) = "1" Or AccountArray(6, 4) = "1" Then
                        btnNav12.Text = "Fees Reports"
                        btnNav12.Visible = True
                    End If
                End If
            End If
            If AccountArray(7, 0) Is Nothing Then
            Else
                If AccountArray(7, 0) = "7" Then
                    If AccountArray(7, 1) = "1" Or AccountArray(7, 2) = "1" Or AccountArray(7, 3) = "1" Or AccountArray(7, 4) = "1" Then
                        btnNav7.Text = "IAIP Query Generator"
                        btnNav7.Visible = True
                    End If
                End If
            End If
            If AccountArray(8, 0) Is Nothing Then
            Else
                If AccountArray(8, 0) = "8" Then
                    If AccountArray(8, 1) = "1" Or AccountArray(8, 2) = "1" Or AccountArray(8, 3) = "1" Or AccountArray(8, 4) = "1" Then
                        btnNav8.Text = "Profile Management"
                        btnNav8.Visible = True
                    End If
                End If
            End If
            If AccountArray(9, 0) Is Nothing Then
            Else
                If AccountArray(9, 0) = "9" Then
                    If AccountArray(9, 1) = "1" Or AccountArray(9, 2) = "1" Or AccountArray(9, 3) = "1" Or AccountArray(9, 4) = "1" Then
                        btnNav9.Text = "Permit File Uploader"
                        btnNav9.Visible = True
                    End If
                End If
            End If
            If AccountArray(10, 0) Is Nothing Then
            Else
                If AccountArray(10, 0) = "10" Then
                    If AccountArray(10, 1) = "1" Or AccountArray(10, 2) = "1" Or AccountArray(10, 3) = "1" Or AccountArray(10, 4) = "1" Then
                        btnNav10.Text = "District Tools"
                        btnNav10.Visible = True
                    End If
                End If
            End If
            If AccountArray(11, 0) Is Nothing Then
            Else
                If AccountArray(11, 0) = "11" Then
                    If AccountArray(11, 1) = "1" Or AccountArray(11, 2) = "1" Or AccountArray(11, 3) = "1" Or AccountArray(11, 4) = "1" Then
                        btnNav11.Text = "AFS Validator"
                        btnNav11.Visible = True
                    End If
                End If
            End If
            If AccountArray(12, 0) Is Nothing Then
            Else
                If AccountArray(12, 0) = "12" Then
                    If AccountArray(12, 1) = "1" Or AccountArray(12, 2) = "1" Or AccountArray(12, 3) = "1" Or AccountArray(12, 4) = "1" Then
                        'btnNav6.Text = "Mailout && Statistics"
                        'btnNav6.Text = "Fee Statistics && Mailout"
                        btnNav6.Text = "Fee Statistics && Reports"
                        btnNav6.Visible = True
                    End If
                End If
            End If
            If AccountArray(13, 0) Is Nothing Then
            Else
                If AccountArray(13, 0) = "13" Then
                    If AccountArray(13, 1) = "1" Or AccountArray(13, 2) = "1" Or AccountArray(13, 3) = "1" Or AccountArray(13, 4) = "1" Then
                        btnNav13.Text = "APB Branch Tools"
                        btnNav13.Visible = True
                    End If
                End If
            End If
            If AccountArray(14, 0) Is Nothing Then
            Else
                If AccountArray(14, 0) = "14" Then
                    If AccountArray(14, 1) = "1" Or AccountArray(14, 2) = "1" Or AccountArray(14, 3) = "1" Or AccountArray(14, 4) = "1" Then
                        btnNav14.Text = "Test Report Information"
                        btnNav14.Visible = True
                    End If
                End If
            End If
            If AccountArray(15, 0) Is Nothing Then
            Else
                If AccountArray(15, 0) = "15" Then
                    If AccountArray(15, 1) = "1" Or AccountArray(15, 2) = "1" Or AccountArray(15, 3) = "1" Or AccountArray(15, 4) = "1" Then
                        btnNav15.Text = "Memo Viewer"
                        btnNav15.Visible = True
                    End If
                End If
            End If
            If AccountArray(16, 0) Is Nothing Then
            Else
                If AccountArray(16, 0) = "16" Then
                    If AccountArray(16, 1) = "1" Or AccountArray(16, 2) = "1" Or AccountArray(16, 3) = "1" Or AccountArray(16, 4) = "1" Then
                        btnNav16.Text = "Ref. Number Management"
                        btnNav16.Visible = True
                    End If
                End If
            End If
            If AccountArray(17, 0) Is Nothing Then
            Else
                If AccountArray(17, 0) = "17" Then
                    If AccountArray(17, 1) = "1" Or AccountArray(17, 2) = "1" Or AccountArray(17, 3) = "1" Or AccountArray(17, 4) = "1" Then
                        btnNav17.Text = "ISMP Managers"
                        btnNav17.Visible = True
                    End If
                End If
            End If
            If AccountArray(18, 0) Is Nothing Then
            Else
                If AccountArray(18, 0) = "18" Then
                    If AccountArray(18, 1) = "1" Or AccountArray(18, 2) = "1" Or AccountArray(18, 3) = "1" Or AccountArray(18, 4) = "1" Then
                        btnNav18.Text = "Deposits"
                        btnNav18.Visible = True
                    End If
                End If
            End If
            If AccountArray(19, 0) Is Nothing Then
            Else
                If AccountArray(19, 0) = "19" Then
                    If AccountArray(19, 1) = "1" Or AccountArray(19, 2) = "1" Or AccountArray(19, 3) = "1" Or AccountArray(19, 4) = "1" Then
                        btnNav19.Text = "Attainment Status Tool"
                        btnNav19.Visible = True
                    End If
                End If
            End If
            If AccountArray(20, 0) Is Nothing Then
            Else
                If AccountArray(20, 0) = "20" Then
                    If AccountArray(20, 1) = "1" Or AccountArray(20, 2) = "1" Or AccountArray(20, 3) = "1" Or AccountArray(20, 4) = "1" Then
                        btnNav20.Text = "Emissions Summary Tool"
                        btnNav20.Visible = True
                    End If
                End If
            End If
            If AccountArray(21, 0) Is Nothing Then
            Else
                If AccountArray(21, 0) = "21" Then
                    If AccountArray(21, 1) = "1" Or AccountArray(21, 2) = "1" Or AccountArray(21, 3) = "1" Or AccountArray(21, 4) = "1" Then
                        btnNav21.Text = "Inspection Tool"
                        btnNav21.Visible = True
                    End If
                End If
            End If
            If AccountArray(22, 0) Is Nothing Then
            Else
                If AccountArray(22, 0) = "22" Then
                    If AccountArray(22, 1) = "1" Or AccountArray(22, 2) = "1" Or AccountArray(22, 3) = "1" Or AccountArray(22, 4) = "1" Then
                        btnNav22.Text = "Compliance Managers"
                        btnNav22.Visible = True
                    End If
                End If
            End If
            If AccountArray(23, 0) Is Nothing Then
            Else
                If AccountArray(23, 0) = "23" Then
                    If AccountArray(23, 1) = "1" Or AccountArray(23, 2) = "1" Or AccountArray(23, 3) = "1" Or AccountArray(24, 4) = "1" Then
                        btnNav23.Text = "PA/PN Report"
                        btnNav23.Visible = True
                    End If
                End If
            End If
            If AccountArray(24, 0) Is Nothing Then
            Else
                If AccountArray(24, 0) = "24" Then
                    If AccountArray(24, 1) = "1" Or AccountArray(24, 2) = "1" Or AccountArray(24, 3) = "1" Or AccountArray(24, 4) = "1" Then
                        btnNav24.Text = "SSPP Tools"
                        btnNav24.Visible = True
                    End If
                End If
            End If
            'If AccountArray(126, 0) Is Nothing Then
            'Else
            '    If AccountArray(126, 0) = "126" Then
            '        If AccountArray(126, 1) = "1" Or AccountArray(126, 2) = "1" Or AccountArray(126, 3) = "1" Or AccountArray(126, 4) = "1" Then
            '            btnNav26.Text = "Fee Tools"
            '            btnNav26.Visible = True
            '        End If
            '    End If
            'End If
            'If AccountArray(127, 0) Is Nothing Then
            'Else
            '    If AccountArray(127, 0) = "127" Then
            '        If AccountArray(127, 1) = "1" Or AccountArray(127, 2) = "1" Or AccountArray(127, 3) = "1" Or AccountArray(127, 4) = "1" Then
            '            btnNav27.Text = "GAIT Inventory"
            '            btnNav27.Visible = True
            '        End If
            '    End If
            'End If
            If AccountArray(128, 0) Is Nothing Then
            Else
                If AccountArray(128, 0) = "128" Then
                    If AccountArray(128, 1) = "1" Or AccountArray(128, 2) = "1" Or AccountArray(128, 3) = "1" Or AccountArray(128, 4) = "1" Then
                        btnNav28.Text = "Smoke School"
                        btnNav28.Visible = True
                    End If
                End If
            End If
            If AccountArray(129, 0) Is Nothing Then
            Else
                If AccountArray(129, 0) = "129" Then
                    If AccountArray(129, 1) = "1" Or AccountArray(129, 2) = "1" Or AccountArray(129, 3) = "1" Or AccountArray(129, 4) = "1" Then
                        btnNav29.Text = "AFS Tools"
                        btnNav29.Visible = True
                    End If
                End If
            End If
            If AccountArray(130, 0) Is Nothing Then
            Else
                If AccountArray(130, 0) = "130" Then
                    If AccountArray(130, 1) = "1" Or AccountArray(130, 2) = "1" Or AccountArray(130, 3) = "1" Or AccountArray(130, 4) = "1" Then
                        btnNav30.Text = "DMU Staff Tools"
                        btnNav30.Visible = True
                    End If
                End If
            End If
            If AccountArray(131, 0) Is Nothing Then
            Else
                If AccountArray(131, 0) = "131" Then
                    If AccountArray(131, 1) = "1" Or AccountArray(131, 2) = "1" Or AccountArray(131, 3) = "1" Or AccountArray(131, 4) = "1" Then
                        btnNav31.Text = "Title V Tools"
                        btnNav31.Visible = True
                    End If
                End If
            End If
            If AccountArray(132, 0) Is Nothing Then
            Else
                If AccountArray(132, 0) = "132" Then
                    If AccountArray(132, 1) = "1" Or AccountArray(132, 2) = "1" Or AccountArray(132, 3) = "1" Or AccountArray(132, 4) = "1" Then
                        btnNav32.Text = "AFS Compare Tool"
                        btnNav32.Visible = True
                    End If
                End If
            End If
            If AccountArray(63, 0) Is Nothing Then
            Else
                If AccountArray(63, 0) = "63" And (UserGCode = "1" Or UserGCode = "345") Then
                    If AccountArray(63, 1) = "1" Or AccountArray(63, 2) = "1" Or AccountArray(63, 3) = "1" Or AccountArray(63, 4) = "1" Then
                        btnNav25.Text = "DMU Only Tool"
                        btnNav25.Visible = True
                    End If
                End If
            End If

            If AccountArray(133, 0) Is Nothing Then
            Else
                If AccountArray(133, 0) = "133" Then
                    If AccountArray(133, 1) = "1" Or AccountArray(133, 2) = "1" Or AccountArray(133, 3) = "1" Or AccountArray(133, 4) = "1" Then
                        btnNav33.Text = "Look Up Tables"
                        btnNav33.Visible = True
                    End If
                End If
            End If
            If AccountArray(134, 0) Is Nothing Then
            Else
                If AccountArray(134, 0) = "134" Then
                    If AccountArray(134, 1) = "1" Or AccountArray(134, 2) = "1" Or AccountArray(134, 3) = "1" Or AccountArray(134, 4) = "1" Then
                        btnNav34.Text = "Fees Audit Tool"
                        btnNav34.Visible = True
                    End If
                End If
            End If
            If AccountArray(135, 0) Is Nothing Then
            Else
                If AccountArray(135, 0) = "135" Then
                    If AccountArray(135, 1) = "1" Or AccountArray(135, 2) = "1" Or AccountArray(135, 3) = "1" Or AccountArray(135, 4) = "1" Then
                        btnNav35.Text = "Fees Log"
                        btnNav35.Visible = True
                    End If
                End If
            End If

            If AccountArray(136, 0) Is Nothing Then
            Else
                If AccountArray(136, 0) = "136" Then
                    If AccountArray(136, 1) = "1" Or AccountArray(136, 2) = "1" Or AccountArray(136, 3) = "1" Or AccountArray(136, 4) = "1" Then
                        btnNav36.Text = "Compliance Admin"
                        btnNav36.Visible = True
                    End If
                End If
            End If

            If AccountArray(137, 0) Is Nothing Then
            Else
                If AccountArray(137, 0) = "137" Then
                    If AccountArray(137, 1) = "1" Or AccountArray(137, 2) = "1" Or AccountArray(137, 3) = "1" Or AccountArray(137, 4) = "1" Then
                        btnNav37.Text = "Registration Tool"
                        btnNav37.Visible = True
                    End If
                End If
            End If

            If AccountArray(139, 0) Is Nothing Then
            Else
                If AccountArray(139, 0) = "139" Then
                    If AccountArray(139, 1) = "1" Or AccountArray(139, 2) = "1" Or AccountArray(139, 3) = "1" Or AccountArray(139, 4) = "1" Then
                        btnNav38.Text = "Fee Management"
                        btnNav38.Visible = True
                    End If
                End If
            End If
            If AccountArray(140, 0) Is Nothing Then
            Else
                If AccountArray(140, 0) = "140" Then
                    If AccountArray(140, 1) = "1" Or AccountArray(140, 2) = "1" Or AccountArray(140, 3) = "1" Or AccountArray(140, 4) = "1" Then
                        btnNav39.Text = "EIS Log"
                        btnNav39.Visible = True
                    End If
                End If
            End If
            If TestingEnvironment AndAlso (Permissions.Contains("(19)") OrElse Permissions.Contains("(20)") _
            OrElse Permissions.Contains("(21)") OrElse Permissions.Contains("(23)") _
            OrElse Permissions.Contains("(25)") OrElse Permissions.Contains("(118)")) Then
                btnNav40.Text = "Enforcement Documents"
                btnNav40.Visible = True
            End If

            Select Case Permissions
                Case Is = "(1)"
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(2)" 'ISMP Program Manager   
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(3)", Is = "(4)"  'ISMP Unit Managers
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(5)", Is = "(6)"  'ISMP Engineer
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(7)"  'ISMP Administrative
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(8)", Is = "(9)"  'ISMP Specialist
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If

                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(10)"  'Web Publisher
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(11)"  'PASP Program Manager 
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(12)", Is = "(13)", Is = "(14)"  'Planning & regulator Manager and Planning & regulator Engineer/Admin
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If

                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(15)", Is = "(16)"  'Data & Modeling Manager and Engineers
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(17)"  'Financial Manager
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(18)", Is = "(44)", Is = "(70)", Is = "(71)"  'Planning and Support Admin or Finanacial User
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(19)"  'SSCP Program Manager 
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(20)"  'SSCP Admin
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If

                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(21)", Is = "(23)", Is = "(25)"  'SSCP Managers
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(22)", Is = "(24)", Is = "(26)"  'SSCP Engineers
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(27)"  'District Liason
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(28)"  'SSPP Program Manager 
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If

                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(29)"  'SSPP Admin 
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(30)"  'SSPP Admin 2 
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(31)", Is = "(33)", Is = "(35)", Is = "(37)", Is = "(39)"  'SSPP Manager 
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If

                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'If btnNav24.Visible = True Then
                    '    btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                    '    i += 1
                    'End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(32)", Is = "(34)", Is = "(36)", Is = "(38)", Is = "(40)"  'SSPP Engineer 
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If

                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(41)"  'Air Branch Director
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If

                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(42)"  'Air Branch Admin
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If

                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(43)"  'DMU User
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List

                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Is = "(44)"
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    'End of List
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If

                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                Case Else
                    If btnNav1.Visible = True Then
                        btnNav1.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav2.Visible = True Then
                        btnNav2.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav3.Visible = True Then
                        btnNav3.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav4.Visible = True Then
                        btnNav4.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav5.Visible = True Then
                        btnNav5.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav35.Visible = True Then
                        btnNav35.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav6.Visible = True Then
                        btnNav6.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav7.Visible = True Then
                        btnNav7.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav8.Visible = True Then
                        btnNav8.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav9.Visible = True Then
                        btnNav9.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav10.Visible = True Then
                        btnNav10.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav11.Visible = True Then
                        btnNav11.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav12.Visible = True Then
                        btnNav12.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav13.Visible = True Then
                        btnNav13.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav14.Visible = True Then
                        btnNav14.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav15.Visible = True Then
                        btnNav15.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav16.Visible = True Then
                        btnNav16.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav17.Visible = True Then
                        btnNav17.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav18.Visible = True Then
                        btnNav18.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav19.Visible = True Then
                        btnNav19.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav20.Visible = True Then
                        btnNav20.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav21.Visible = True Then
                        btnNav21.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav22.Visible = True Then
                        btnNav22.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav23.Visible = True Then
                        btnNav23.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav24.Visible = True Then
                        btnNav24.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav25.Visible = True Then
                        btnNav25.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav26.Visible = True Then
                        btnNav26.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav27.Visible = True Then
                        btnNav27.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav28.Visible = True Then
                        btnNav28.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav29.Visible = True Then
                        btnNav29.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav30.Visible = True Then
                        btnNav30.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav31.Visible = True Then
                        btnNav31.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav32.Visible = True Then
                        btnNav32.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav33.Visible = True Then
                        btnNav33.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav34.Visible = True Then
                        btnNav34.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
                    If btnNav36.Visible = True Then
                        btnNav36.Location = New System.Drawing.Point(12, (12 + 56 * i))
                        i += 1
                    End If
            End Select

            If btnNav37.Visible = True Then
                btnNav37.Location = New System.Drawing.Point(12, (12 + 56 * i))
                i += 1
            End If
            If btnNav38.Visible = True Then
                btnNav38.Location = New System.Drawing.Point(12, (12 + 56 * i))
                i += 1
            End If
            If btnNav39.Visible = True Then
                btnNav39.Location = New System.Drawing.Point(12, (12 + 56 * i))
                i += 1
            End If
            If btnNav40.Visible = True Then
                btnNav40.Location = New System.Drawing.Point(12, (12 + 56 * i))
                i += 1
            End If

            If rdbPMView.Visible = True Then
                rdbPMView.Checked = True
            Else
                If rdbUCView.Visible = True Then
                    rdbUCView.Checked = True
                Else
                    If rdbStaffView.Visible = True Then
                        rdbStaffView.Checked = True
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub mmiISMPLists_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiISMPLists.Click
        Try
            llbPrimaryList.Visible = False
            llbSecondaryList.Visible = False
            llbTertiaryList.Visible = False
            llbQuaternaryList.Visible = False

            llbPrimaryList.Visible = True
            llbSecondaryList.Visible = True
            llbTertiaryList.Visible = True
            llbPrimaryList.Text = "Test Reports"
            llbSecondaryList.Text = "Permit Applications"
            llbTertiaryList.Text = "Test Notifications"

            WorkBranch = "1"
            WorkProgram = "3"
            WorkUnit = "---"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub mmiSSCPLists_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSSCPLists.Click
        Try
            llbPrimaryList.Visible = False
            llbSecondaryList.Visible = False
            llbTertiaryList.Visible = False
            llbQuaternaryList.Visible = False

            llbPrimaryList.Visible = True
            llbSecondaryList.Visible = True
            llbTertiaryList.Visible = True
            llbQuaternaryList.Visible = True
            llbPrimaryList.Text = "Open Enforcement"
            llbSecondaryList.Text = "Permit Applications"
            llbTertiaryList.Text = "Open Compliance Work"
            llbQuaternaryList.Text = "MACT Sub Parts"

            WorkBranch = "1"
            WorkProgram = "4"
            WorkUnit = "---"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub mmiSSPPLists_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSSPPLists.Click
        Try
            llbPrimaryList.Visible = False
            llbSecondaryList.Visible = False
            llbTertiaryList.Visible = False
            llbQuaternaryList.Visible = False

            llbPrimaryList.Visible = True
            llbPrimaryList.Text = "Open Applications"

            WorkBranch = "1"
            WorkProgram = "5"
            WorkUnit = "---"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub mmiResetDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiResetDefault.Click
        Try

            WorkBranch = UserBranch
            WorkProgram = UserProgram
            WorkUnit = UserUnit

            LoadShortCutData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub dgvWorkViewer_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvWorkViewer.Sorted
        LoadCompliaceColor()
    End Sub

    Private Sub btnChangeListView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeListView.Click
        Try
            txtAIRSNumber.Clear()
            txtEnforcementNumber.Clear()
            txtApplicationNumber.Clear()
            txtReferenceNumber.Clear()
            txtTrackingNumber.Clear()
            txtTestLogNumber.Clear()

            Select Case cboIAIPList.Text
                Case "Compliance Facilities Assigned"
                    If rdbUCView.Checked = True Then
                        SQL = "Select distinct " & _
                       "substr(AIRBranch.SSCPInspectionsRequired.strAIRSnumber, 5) as AIRSNumber, " & _
                       "AIRBranch.APBFacilityInformation.strFacilityName, " & _
                       "(strLastName||', '||strFirstName) as Staff " & _
                       "from AIRBranch.SSCPInspectionsRequired, AIRBranch.APBFacilityInformation, " & _
                       "AIRBranch.EPDUserProfiles " & _
                       "where AIRBranch.SSCPInspectionsRequired.strAIRSNumber = AIRBranch.APBFacilityInformation.strAIRSNumber " & _
                       "and AIRBranch.SSCPInspectionsRequired.numSSCPEngineer = AIRBranch.EPDUserProfiles.numUserID " & _
                       "and numProgram = '" & UserProgram & "' " & _
                       "order by AIRSNumber  "
                    Else
                        SQL = "Select distinct " & _
                       "substr(AIRBranch.SSCPInspectionsRequired.strAIRSnumber, 5) as AIRSNumber, " & _
                       "AIRBranch.APBFacilityInformation.strFacilityName, " & _
                       "(strLastName||', '||strFirstName) as Staff " & _
                       "from AIRBranch.SSCPInspectionsRequired, AIRBranch.APBFacilityInformation, " & _
                       "AIRBranch.EPDUserProfiles " & _
                       "where AIRBranch.SSCPInspectionsRequired.strAIRSNumber = AIRBranch.APBFacilityInformation.strAIRSNumber " & _
                       "and AIRBranch.SSCPInspectionsRequired.numSSCPEngineer = AIRBranch.EPDUserProfiles.numUserID " & _
                       "and numSSCPEngineer = '" & UserGCode & "' " & _
                       "order by AIRSNumber  "

                    End If

                    dsOpenWork = New DataSet
                    daOpenWork = New OracleDataAdapter(SQL, Conn)
                    If SQL <> "" Then
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        daOpenWork.Fill(dsOpenWork, "OpenWork")
                    End If

                    dsOpenWork = New DataSet
                    daOpenWork = New OracleDataAdapter(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    daOpenWork.Fill(dsOpenWork, "OpenWork")
                    dgvWorkViewer.DataSource = dsOpenWork
                    dgvWorkViewer.DataMember = "OpenWork"

                    dgvWorkViewer.RowHeadersVisible = False
                    dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvWorkViewer.AllowUserToResizeColumns = True
                    dgvWorkViewer.AllowUserToAddRows = False
                    dgvWorkViewer.AllowUserToDeleteRows = False
                    dgvWorkViewer.AllowUserToOrderColumns = True
                    dgvWorkViewer.AllowUserToResizeRows = True
                    dgvWorkViewer.ColumnHeadersHeight = "35"
                    dgvWorkViewer.Columns("AIRSNumber").HeaderText = "AIRS #"
                    dgvWorkViewer.Columns("AIRSNumber").DisplayIndex = 0
                    dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
                    dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 1
                    dgvWorkViewer.Columns("strFacilityName").Width = dgvWorkViewer.Width * (0.5)
                    dgvWorkViewer.Columns("Staff").HeaderText = "Staff Responsible"
                    dgvWorkViewer.Columns("Staff").DisplayIndex = 2
                    dgvWorkViewer.Columns("Staff").Width = dgvWorkViewer.Width * (0.25)

                Case "Compliance Work"
                    SQL = "Select " & _
                          "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                          "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                          "(strLastName||', '||strFirstName) as Staff,  " & _
                          "strResponsibleStaff, " & _
                          "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived, " & _
                          "AIRBranch.APBFacilityInformation.strFacilityName, StrActivityName    " & _
                          "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                          "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,   " & _
                          "AIRBranch.VW_SSCPInspection_List " & _
                          "where AIRBranch.EPDUserProfiles.numUserID = " & _
                          DBNameSpace & ".SSCPItemMaster.strResponsibleStaff  " & _
                          "and AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                          DBNameSpace & ".SSCPItemMaster.strAIRSNumber  " & _
                          "and AIRBranch.LookUPComplianceActivities.strActivityType = " & _
                          DBNameSpace & ".SSCPItemMaster.strEventType  " & _
                          " and AIRBranch.SSCPItemMaster.strAIRSnumber = '0413'||" & _
                          DBNameSpace & ".VW_SSCPInspection_List.AIRSNumber  " & _
                          "and (strResponsibleStaff = '" & UserGCode & "' or numSSCPEngineer = '" & UserGCode & "') " & _
                          "and DatCompleteDate is Null  " & _
                          "and strDelete is Null "

                    If rdbStaffView.Checked = True Then
                        SQL = "Select " & _
                        "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                        "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                        "(strLastName||', '||strFirstName) as Staff,  " & _
                        "strResponsibleStaff, " & _
                        "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived, " & _
                        "AIRBranch.APBFacilityInformation.strFacilityName, StrActivityName    " & _
                        "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                        "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,   " & _
                        "AIRBranch.VW_SSCPInspection_List " & _
                        "where AIRBranch.EPDUserProfiles.numUserID = " & _
                        DBNameSpace & ".SSCPItemMaster.strResponsibleStaff  " & _
                        "and AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                        DBNameSpace & ".SSCPItemMaster.strAIRSNumber  " & _
                        "and AIRBranch.LookUPComplianceActivities.strActivityType = " & _
                        DBNameSpace & ".SSCPItemMaster.strEventType  " & _
                        " and AIRBranch.SSCPItemMaster.strAIRSnumber = '0413'||" & _
                        DBNameSpace & ".VW_SSCPInspection_List.AIRSNumber  " & _
                        "and (strResponsibleStaff = '" & UserGCode & "' or numSSCPEngineer = '" & UserGCode & "') " & _
                        "and DatCompleteDate is Null  " & _
                        "and strDelete is Null "
                    End If
                    If rdbUCView.Checked = True Then
                        If UserProgram <> "5" Then
                            SQL = "select " & _
                            "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                            "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                            "(strLastName||', '||strFirstName) as Staff,  " & _
                            "strResponsibleStaff, " & _
                            "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived,  " & _
                            "strFacilityName, StrActivityName    " & _
                            "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                            "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,  " & _
                            "(select numUserID from AIRBranch.EPDUserProfiles where numProgram = '" & UserProgram & "')  " & _
                            "UnitStaff    " & _
                            "where AIRBranch.EPDUserProfiles.numUserID = " & _
                            DBNameSpace & ".SSCPItemMaster.strResponsibleStaff  " & _
                            "and AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                            DBNameSpace & ".SSCPItemMaster.strAIRSNumber  " & _
                            "and AIRBranch.LookUPComplianceActivities.strActivityType = " & _
                            DBNameSpace & ".SSCPItemMaster.strEventType " & _
                            "and DatCompleteDate is Null   " & _
                            "and strResponsibleStaff = UnitStaff.numUserID " & _
                            "and strDelete is Null "
                        Else
                            SQL = "select " & _
                             "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                             "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                             "(strLastName||', '||strFirstName) as Staff,  " & _
                             "strResponsibleStaff, " & _
                             "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived,  " & _
                             "strFacilityName, StrActivityName    " & _
                             "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                             "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities,  " & _
                             "(select numUserID from AIRBranch.EPDUserProfiles where numUnit = '" & UserUnit & "')  " & _
                             "UnitStaff    " & _
                             "where AIRBranch.EPDUserProfiles.numUserID = " & _
                             DBNameSpace & ".SSCPItemMaster.strResponsibleStaff  " & _
                             "and AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                             DBNameSpace & ".SSCPItemMaster.strAIRSNumber  " & _
                             "and AIRBranch.LookUPComplianceActivities.strActivityType = " & _
                             DBNameSpace & ".SSCPItemMaster.strEventType " & _
                             "and DatCompleteDate is Null   " & _
                             "and strResponsibleStaff = UnitStaff.numUserID " & _
                             "and strDelete is Null "
                        End If

                    End If
                    If rdbPMView.Checked = True Then
                        SQL = "select " & _
                       "to_number(AIRBranch.SSCPItemMaster.strTrackingNumber) as strTrackingNumber,  " & _
                       "substr(AIRBranch.SSCPItemMaster.strAIRSNumber, 5) as AIRSNumber,  " & _
                        " case when AIRBranch.SSCPItemMaster.STRRESPONSIBLESTAFF = 0 then ': No one assigned' " & _
                        " when AIRBranch.SSCPItemMaster.STRRESPONSIBLESTAFF is null then ': Not assigned' " & _
                        "Else STRLASTNAME || ', ' || STRFIRSTNAME end AS Staff, " & _
                       "strResponsibleStaff, " & _
                       "to_char(datReceivedDate, 'dd-Mon-yyyy') as DateReceived,  " & _
                       "strFacilityName, StrActivityName    " & _
                       "from AIRBranch.SSCPItemMaster, AIRBranch.EPDUserProfiles,  " & _
                       "AIRBranch.APBFacilityInformation, AIRBranch.LookUPComplianceActivities " & _
                       "where AIRBranch.EPDUserProfiles.numUserID(+) = " & _
                                DBNameSpace & ".SSCPItemMaster.strResponsibleStaff  " & _
                       "and AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                       DBNameSpace & ".SSCPItemMaster.strAIRSNumber  " & _
                       "and AIRBranch.LookUPComplianceActivities.strActivityType = " & _
                       DBNameSpace & ".SSCPItemMaster.strEventType " & _
                       "and DatCompleteDate is Null   " & _
                       "and strDelete is Null "
                    End If

                    If rdbPMView.Checked = True Then
                        SQL = SQL
                    End If

                    dsOpenWork = New DataSet
                    daOpenWork = New OracleDataAdapter(SQL, Conn)
                    If SQL <> "" Then
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        daOpenWork.Fill(dsOpenWork, "OpenWork")
                    End If

                    dsOpenWork = New DataSet
                    daOpenWork = New OracleDataAdapter(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    daOpenWork.Fill(dsOpenWork, "OpenWork")
                    dgvWorkViewer.DataSource = dsOpenWork
                    dgvWorkViewer.DataMember = "OpenWork"

                    dgvWorkViewer.RowHeadersVisible = False
                    dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvWorkViewer.AllowUserToResizeColumns = True
                    dgvWorkViewer.AllowUserToAddRows = False
                    dgvWorkViewer.AllowUserToDeleteRows = False
                    dgvWorkViewer.AllowUserToOrderColumns = True
                    dgvWorkViewer.AllowUserToResizeRows = True
                    dgvWorkViewer.ColumnHeadersHeight = "35"
                    dgvWorkViewer.Columns("strTrackingNumber").HeaderText = "Tracking #"
                    dgvWorkViewer.Columns("strTrackingNumber").DisplayIndex = 0
                    dgvWorkViewer.Columns("AIRSNumber").HeaderText = "AIRS #"
                    dgvWorkViewer.Columns("AIRSNumber").DisplayIndex = 1
                    dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
                    dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 2
                    dgvWorkViewer.Columns("DateReceived").HeaderText = "Date Received"
                    dgvWorkViewer.Columns("DateReceived").DisplayIndex = 3
                    dgvWorkViewer.Columns("DateReceived").DefaultCellStyle.Format = "dd-MMM-yyyy"
                    dgvWorkViewer.Columns("Staff").HeaderText = "Staff Responsible"
                    dgvWorkViewer.Columns("Staff").DisplayIndex = 4
                    dgvWorkViewer.Columns("StrActivityName").HeaderText = "Activity Type"
                    dgvWorkViewer.Columns("StrActivityName").DisplayIndex = 5
                    dgvWorkViewer.Columns("strResponsibleStaff").HeaderText = "Responsible Staff"
                    dgvWorkViewer.Columns("strResponsibleStaff").DisplayIndex = 6
                    dgvWorkViewer.Columns("strResponsibleStaff").Visible = False
                Case "Full Compliance Evaluations - Delinquent"
                    Dim StartCMSA As String
                    Dim StartCMSS As String

                    StartCMSA = Format(CDate(OracleDate).AddDays(-730), "yyyy-MM-dd")
                    StartCMSS = Format(CDate(OracleDate).AddDays(-1825), "yyyy-MM-dd")

                    SQL = "Select * " & _
                    "from AIRBranch.VW_SSCP_CMSWarning " & _
                    "where AIRSNumber is not Null " & _
                    " and strCMSMember is not null " & _
                    " and ((strCMSMember = 'A' and lastFCE < '" & StartCMSA & "') " & _
                    "or (strCMSMember = 'S' and LastFCE < '" & StartCMSS & "')) "

                    If SQL <> "" Then
                        dsOpenWork = New DataSet
                        daOpenWork = New OracleDataAdapter(SQL, Conn)

                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If

                        daOpenWork.Fill(dsOpenWork, "OpenWork")
                        dgvWorkViewer.DataSource = dsOpenWork
                        dgvWorkViewer.DataMember = "OpenWork"

                        dgvWorkViewer.RowHeadersVisible = False
                        dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                        dgvWorkViewer.AllowUserToResizeColumns = True
                        dgvWorkViewer.AllowUserToAddRows = False
                        dgvWorkViewer.AllowUserToDeleteRows = False
                        dgvWorkViewer.AllowUserToOrderColumns = True
                        dgvWorkViewer.AllowUserToResizeRows = True


                        dgvWorkViewer.Columns("AIRSNumber").HeaderText = "AIRS #"
                        dgvWorkViewer.Columns("AIRSNumber").DisplayIndex = 0
                        dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
                        dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 1
                        dgvWorkViewer.Columns("strCMSMember").HeaderText = "CMS Class"
                        dgvWorkViewer.Columns("strCMSMember").DisplayIndex = 2
                        dgvWorkViewer.Columns("LastFCE").HeaderText = "Last FCE"
                        dgvWorkViewer.Columns("LastFCE").DisplayIndex = 3
                        dgvWorkViewer.Columns("LastFCE").DefaultCellStyle.Format = "dd-MMM-yyyy"
                        dgvWorkViewer.Columns("strFacilityCity").HeaderText = "City"
                        dgvWorkViewer.Columns("strFacilityCity").DisplayIndex = 4
                        dgvWorkViewer.Columns("strCountyName").HeaderText = "County"
                        dgvWorkViewer.Columns("strCountyName").DisplayIndex = 5
                        dgvWorkViewer.Columns("strDistrictName").HeaderText = "District"
                        dgvWorkViewer.Columns("strDistrictName").DisplayIndex = 6
                        dgvWorkViewer.Columns("strOperationalStatus").HeaderText = "Operational Status"
                        dgvWorkViewer.Columns("strOperationalStatus").DisplayIndex = 7
                        dgvWorkViewer.Columns("strClass").HeaderText = "Classification"
                        dgvWorkViewer.Columns("strClass").DisplayIndex = 8

                        ' txtDataGridCount.Text = dgvWorkViewer.RowCount
                    End If


                Case "Enforcement"
                    SQL = "Select " & _
                    "to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                    "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " & _
                    "case  " & _
                    "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " & _
                    "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " & _
                    "when strStatus = 'UC' then '2 - Submitted to UC'  " & _
                    "When strStatus Is Null then '1 - At Staff'  " & _
                    "else 'Unknown'  " & _
                    "end as EnforcementStatus,  " & _
                    "Case   " & _
                    " 	when datDiscoveryDate is Null then ''  " & _
                    "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                    "END as Violationdate,   " & _
                    "case   " & _
                    "	when strHPV IS NULL then strActionType  " & _
                    "	When strHPV IS Not Null then 'HPV'   " & _
                    "Else 'HPV'  " & _
                    "END as HPVStatus,  " & _
                    "Case  " & _
                    " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " & _
                    " 	when datEnforcementFinalized is NUll then 'Open'  " & _
                    "Else 'Open'  " & _
                    "End as Status,  " & _
                    "AIRBranch.APBFacilityInformation.strFacilityName,  " & _
                    "(strLastName||', '||strFirstName) as Staff  " & _
                    "from AIRBranch.SSCP_AuditedEnforcement,   " & _
                    "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " & _
                    "AIRBranch.VW_SSCPINSPECTION_LIST " & _
                    "Where AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                    DBNameSpace & ".SSCP_AuditedEnforcement.strAIRSNumber  " & _
                    "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = " & _
                    "'0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " & _
                    "and (strStatus IS Null or strStatus = 'UC')  " & _
                    "and datEnforcementFinalized is Null  " & _
                    "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " & _
                    "and (numStaffResponsible = '" & UserGCode & "' or numSSCPEngineer = '" & UserGCode & "') "

                    If rdbStaffView.Checked = True Then
                        SQL = "Select " & _
                      "to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                      "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " & _
                      "case  " & _
                      "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " & _
                      "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " & _
                      "when strStatus = 'UC' then '2 - Submitted to UC'  " & _
                      "When strStatus Is Null then '1 - At Staff'  " & _
                      "else 'Unknown'  " & _
                      "end as EnforcementStatus,  " & _
                      "Case   " & _
                      " 	when datDiscoveryDate is Null then ''  " & _
                      "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                      "END as Violationdate,   " & _
                      "case   " & _
                      "	when strHPV IS NULL then strActionType  " & _
                      "	When strHPV IS Not Null then 'HPV'   " & _
                      "Else 'HPV'  " & _
                      "END as HPVStatus,  " & _
                      "Case  " & _
                      " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " & _
                      " 	when datEnforcementFinalized is NUll then 'Open'  " & _
                      "Else 'Open'  " & _
                      "End as Status,  " & _
                      "AIRBranch.APBFacilityInformation.strFacilityName,  " & _
                      "(strLastName||', '||strFirstName) as Staff  " & _
                      "from AIRBranch.SSCP_AuditedEnforcement,   " & _
                      "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " & _
                      "AIRBranch.VW_SSCPINSPECTION_LIST " & _
                      "Where AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                      DBNameSpace & ".SSCP_AuditedEnforcement.strAIRSNumber  " & _
                      "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = " & _
                      "'0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " & _
                      "and (strStatus IS Null or strStatus = 'UC')  " & _
                      "and datEnforcementFinalized is Null  " & _
                      "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  " & _
                      "and (numStaffResponsible = '" & UserGCode & "' or numSSCPEngineer = '" & UserGCode & "') "
                    End If
                    If rdbUCView.Checked = True Then
                        SQL = "Select " & _
                    "to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                    "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " & _
                    "case  " & _
                    "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " & _
                    "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " & _
                    "when strStatus = 'UC' then '2 - Submitted to UC'  " & _
                    "When strStatus Is Null then '1 - At Staff'  " & _
                    "else 'Unknown'  " & _
                    "end as EnforcementStatus,  " & _
                    "Case   " & _
                    " 	when datDiscoveryDate is Null then ''  " & _
                    "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                    "END as Violationdate,   " & _
                    "case   " & _
                    "	when strHPV IS NULL then strActionType  " & _
                    "	When strHPV IS Not Null then 'HPV'   " & _
                    "Else 'HPV'  " & _
                    "END as HPVStatus,  " & _
                    "Case  " & _
                    " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " & _
                    " 	when datEnforcementFinalized is NUll then 'Open'  " & _
                    "Else 'Open'  " & _
                    "End as Status,  " & _
                    "AIRBranch.APBFacilityInformation.strFacilityName,  " & _
                    "(strLastName||', '||strFirstName) as Staff  " & _
                    "from AIRBranch.SSCP_AuditedEnforcement,   " & _
                    "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " & _
                    "AIRBranch.VW_SSCPINSPECTION_LIST " & _
                    "Where AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                    DBNameSpace & ".SSCP_AuditedEnforcement.strAIRSNumber  " & _
                    "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = " & _
                    "'0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " & _
                    "and (strStatus IS Null or strStatus = 'UC')  " & _
                    "and datEnforcementFinalized is Null  " & _
                    "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  "

                        If UserProgram = "4" Then
                            SQL = SQL & " and numUnit = '" & UserUnit & "' "
                        Else
                            SQL = SQL & " and numProgram = '" & UserProgram & "' "
                        End If

                    End If
                    If rdbPMView.Checked = True Then
                        SQL = "Select " & _
                   "to_number(AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber,  " & _
                   "substr(AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " & _
                   "case  " & _
                   "when datEnforcementFinalized is Not Null then '4 - Closed Out'  " & _
                   "when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " & _
                   "when strStatus = 'UC' then '2 - Submitted to UC'  " & _
                   "When strStatus Is Null then '1 - At Staff'  " & _
                   "else 'Unknown'  " & _
                   "end as EnforcementStatus,  " & _
                   "Case   " & _
                   " 	when datDiscoveryDate is Null then ''  " & _
                   "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy')  " & _
                   "END as Violationdate,   " & _
                   "case   " & _
                   "	when strHPV IS NULL then strActionType  " & _
                   "	When strHPV IS Not Null then 'HPV'   " & _
                   "Else 'HPV'  " & _
                   "END as HPVStatus,  " & _
                   "Case  " & _
                   " 	when datEnforcementFinalized Is Not NULL then 'Closed'  " & _
                   " 	when datEnforcementFinalized is NUll then 'Open'  " & _
                   "Else 'Open'  " & _
                   "End as Status,  " & _
                   "AIRBranch.APBFacilityInformation.strFacilityName,  " & _
                   "(strLastName||', '||strFirstName) as Staff  " & _
                   "from AIRBranch.SSCP_AuditedEnforcement,   " & _
                   "AIRBranch.APBFacilityInformation, AIRBranch.EPDuserProfiles,  " & _
                   "AIRBranch.VW_SSCPINSPECTION_LIST " & _
                   "Where AIRBranch.APBFacilityInformation.strAIRSNumber = " & _
                   DBNameSpace & ".SSCP_AuditedEnforcement.strAIRSNumber  " & _
                   "and AIRBranch.SSCP_AuditedEnforcement.strAIRSnumber = " & _
                   "'0413'||AIRBranch.VW_SSCPINSPECTION_LIST.AIRSNumber  " & _
                   "and (strStatus IS Null or strStatus = 'UC')  " & _
                   "and datEnforcementFinalized is Null  " & _
                   "and AIRBranch.EPDuserProfiles.numUserID = numStaffResponsible  "
                    End If
                    SQL = SQL & "order by strENforcementNumber DESC  "

                    dsOpenWork = New DataSet
                    daOpenWork = New OracleDataAdapter(SQL, Conn)
                    If SQL <> "" Then
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        daOpenWork.Fill(dsOpenWork, "OpenWork")
                    End If

                    dgvWorkViewer.DataSource = dsOpenWork
                    dgvWorkViewer.DataMember = "OpenWork"

                    dgvWorkViewer.RowHeadersVisible = False
                    dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvWorkViewer.AllowUserToResizeColumns = True
                    dgvWorkViewer.AllowUserToAddRows = False
                    dgvWorkViewer.AllowUserToDeleteRows = False
                    dgvWorkViewer.AllowUserToOrderColumns = True
                    dgvWorkViewer.AllowUserToResizeRows = True
                    dgvWorkViewer.ColumnHeadersHeight = "35"
                    dgvWorkViewer.Columns("strEnforcementNumber").HeaderText = "Enforcement #"
                    dgvWorkViewer.Columns("strEnforcementNumber").DisplayIndex = 0
                    dgvWorkViewer.Columns("AIRSNumber").HeaderText = "AIRS #"
                    dgvWorkViewer.Columns("AIRSNumber").DisplayIndex = 1
                    dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
                    dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 2
                    dgvWorkViewer.Columns("EnforcementStatus").HeaderText = "Enforcement Status"
                    dgvWorkViewer.Columns("EnforcementStatus").DisplayIndex = 3
                    dgvWorkViewer.Columns("Violationdate").HeaderText = "Discovery Date"
                    dgvWorkViewer.Columns("Violationdate").DisplayIndex = 4
                    dgvWorkViewer.Columns("Violationdate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                    dgvWorkViewer.Columns("HPVstatus").HeaderText = "Status"
                    dgvWorkViewer.Columns("HPVstatus").DisplayIndex = 5
                    dgvWorkViewer.Columns("Status").HeaderText = "Open/Closed"
                    dgvWorkViewer.Columns("Status").DisplayIndex = 6
                    dgvWorkViewer.Columns("Staff").HeaderText = "Staff Responsible"
                    dgvWorkViewer.Columns("Staff").DisplayIndex = 7


                Case "Facilities with Subparts"
                    SQL = "select distinct(substr(AIRBranch.APBHeaderData.strAIRSNumber, 5)) as AIRSnumber, " & _
                   "strFacilityName " & _
                   "from AIRBranch.APBHeaderData, AIRBranch.APBFacilityInformation  " & _
                   "where ( exists (select * " & _
                   "from AIRBranch.APBSubpartData " & _
                   "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                   "and substr(strSubPartKey, 13, 1) = 'M') " & _
                   "and subStr(strAirProgramCodes, 12, 1) = '1' " & _
                   "or  exists (select * " & _
                   "from AIRBranch.APBSubpartData " & _
                   "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                   "and substr(strSubPartKey, 13, 1) = '9') " & _
                   "and subStr(strAirProgramCodes, 8, 1) = '1' " & _
                   "or  exists (select * " & _
                   "from AIRBranch.APBSubpartData " & _
                   "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                   "and substr(strSubPartKey, 13, 1) = '8') " & _
                   "and subStr(strAirProgramCodes, 7, 1) = '1' ) " & _
                   "and AIRBranch.APBHeaderData.strAIRSnumber = " & _
                   DBNameSpace & ".APBFacilityInformation.strAIRsnumber " & _
                   "and AIRBranch.APBHeaderData.strOperationalStatus <> 'X' " & _
                   "order by AIRSNumber "

                    dsOpenWork = New DataSet
                    daOpenWork = New OracleDataAdapter(SQL, Conn)
                    If SQL <> "" Then
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        daOpenWork.Fill(dsOpenWork, "OpenWork")
                    End If

                    dgvWorkViewer.DataSource = dsOpenWork
                    dgvWorkViewer.DataMember = "OpenWork"

                    dgvWorkViewer.RowHeadersVisible = False
                    dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvWorkViewer.AllowUserToResizeColumns = True
                    dgvWorkViewer.AllowUserToAddRows = False
                    dgvWorkViewer.AllowUserToDeleteRows = False
                    dgvWorkViewer.AllowUserToOrderColumns = True
                    dgvWorkViewer.AllowUserToResizeRows = True
                    dgvWorkViewer.ColumnHeadersHeight = "35"
                    dgvWorkViewer.Columns("AIRSnumber").HeaderText = "AIRS #"
                    dgvWorkViewer.Columns("AIRSnumber").DisplayIndex = 0
                    dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
                    dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 1

                Case "Facilities missing Subparts"
                    SQL = "select distinct(substr(AIRBranch.APBHeaderData.strAIRSNumber, 5)) as AIRSnumber, " & _
                    "strFacilityName " & _
                    "from AIRBranch.APBHeaderData, AIRBranch.APBFacilityInformation  " & _
                    "where (Not exists (select * " & _
                    "from AIRBranch.APBSubpartData " & _
                    "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                    "and substr(strSubPartKey, 13, 1) = 'M') " & _
                    "and subStr(strAirProgramCodes, 12, 1) = '1' " & _
                    "or Not exists (select * " & _
                    "from AIRBranch.APBSubpartData " & _
                    "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                    "and substr(strSubPartKey, 13, 1) = '9') " & _
                    "and subStr(strAirProgramCodes, 8, 1) = '1' " & _
                    "or Not exists (select * " & _
                    "from AIRBranch.APBSubpartData " & _
                    "where AIRBranch.APBHeaderData.strAIRSnumber = AIRBranch.APBSubpartData.strAIRSnumber " & _
                    "and substr(strSubPartKey, 13, 1) = '8') " & _
                    "and subStr(strAirProgramCodes, 7, 1) = '1' ) " & _
                    "and AIRBranch.APBHeaderData.strAIRSnumber = " & _
                    DBNameSpace & ".APBFacilityInformation.strAIRsnumber " & _
                    "and AIRBranch.APBHeaderData.strOperationalStatus <> 'X' " & _
                    "order by AIRSNumber "

                    dsOpenWork = New DataSet
                    daOpenWork = New OracleDataAdapter(SQL, Conn)
                    If SQL <> "" Then
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        daOpenWork.Fill(dsOpenWork, "OpenWork")
                    End If

                    dgvWorkViewer.DataSource = dsOpenWork
                    dgvWorkViewer.DataMember = "OpenWork"

                    dgvWorkViewer.RowHeadersVisible = False
                    dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvWorkViewer.AllowUserToResizeColumns = True
                    dgvWorkViewer.AllowUserToAddRows = False
                    dgvWorkViewer.AllowUserToDeleteRows = False
                    dgvWorkViewer.AllowUserToOrderColumns = True
                    dgvWorkViewer.AllowUserToResizeRows = True
                    dgvWorkViewer.ColumnHeadersHeight = "35"
                    dgvWorkViewer.Columns("AIRSnumber").HeaderText = "AIRS #"
                    dgvWorkViewer.Columns("AIRSnumber").DisplayIndex = 0
                    dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
                    dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 1

                Case "Monitoring Test Reports"
                    SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " & _
                          "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " & _
                          "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " & _
                          "AIRBranch.ISMPReportInformation.strReferenceNumber  " & _
                          "and  Status = 'Open' " & _
                          " and ReviewingEngineer = '" & pnl2.Text & "' "


                    If rdbStaffView.Checked = True Then
                        SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " & _
                        "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " & _
                        "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " & _
                        "AIRBranch.ISMPReportInformation.strReferenceNumber  " & _
                        "and  Status = 'Open' " & _
                        " and ReviewingEngineer = '" & pnl2.Text & "' "
                    End If
                    If rdbUCView.Checked = True Then
                        SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " & _
                        "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " & _
                        "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " & _
                        "AIRBranch.ISMPReportInformation.strReferenceNumber  " & _
                        "and  Status = 'Open' " & _
                        "and strUserUnit = " & _
                          "(select strUnitDesc from AIRBranch.LookUpEPDUnits where numUnitCode = '" & UserUnit & "') "
                    End If
                    If rdbPMView.Checked = True Or UserUnit = "---" Then
                        SQL = "Select AIRBranch.VW_ISMPTestReportViewer.*, strPreComplianceStatus   " & _
                        "from  AIRBranch.VW_ISMPTestReportViewer, AIRBranch.ISMPReportInformation " & _
                        "where AIRBranch.VW_ISMPTestReportViewer.strReferenceNumber = " & _
                        "AIRBranch.ISMPReportInformation.strReferenceNumber  " & _
                        "and  Status = 'Open' "
                    End If

                    dsOpenWork = New DataSet
                    daOpenWork = New OracleDataAdapter(SQL, Conn)
                    If SQL <> "" Then
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        daOpenWork.Fill(dsOpenWork, "OpenWork")
                    End If

                    dgvWorkViewer.DataSource = dsOpenWork
                    dgvWorkViewer.DataMember = "OpenWork"

                    dgvWorkViewer.RowHeadersVisible = False
                    dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvWorkViewer.AllowUserToResizeColumns = True
                    dgvWorkViewer.AllowUserToAddRows = False
                    dgvWorkViewer.AllowUserToDeleteRows = False
                    dgvWorkViewer.AllowUserToOrderColumns = True
                    dgvWorkViewer.AllowUserToResizeRows = True
                    dgvWorkViewer.ColumnHeadersHeight = "35"
                    dgvWorkViewer.Columns("strReferenceNumber").HeaderText = "Reference #"
                    dgvWorkViewer.Columns("strReferenceNumber").DisplayIndex = 0
                    dgvWorkViewer.Columns("AIRSNumber").HeaderText = "AIRS #"
                    dgvWorkViewer.Columns("AIRSNumber").DisplayIndex = 1
                    dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
                    dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 2
                    dgvWorkViewer.Columns("strFacilityCity").HeaderText = "City"
                    dgvWorkViewer.Columns("strFacilityCity").DisplayIndex = 3
                    dgvWorkViewer.Columns("strCountyName").HeaderText = "County"
                    dgvWorkViewer.Columns("strCountyName").DisplayIndex = 4
                    dgvWorkViewer.Columns("strEmissionSource").HeaderText = "Emission Source"
                    dgvWorkViewer.Columns("strEmissionSource").DisplayIndex = 5
                    dgvWorkViewer.Columns("strPollutantDescription").HeaderText = "Pollutant"
                    dgvWorkViewer.Columns("strPollutantDescription").DisplayIndex = 6
                    dgvWorkViewer.Columns("strReportType").HeaderText = "Report Type"
                    dgvWorkViewer.Columns("strReportType").DisplayIndex = 7
                    dgvWorkViewer.Columns("strDocumentType").HeaderText = "Document Type"
                    dgvWorkViewer.Columns("strDocumentType").DisplayIndex = 8
                    dgvWorkViewer.Columns("ReviewingEngineer").HeaderText = "Reviewing Engineer"
                    dgvWorkViewer.Columns("ReviewingEngineer").DisplayIndex = 9
                    dgvWorkViewer.Columns("TestDateStart").HeaderText = "Test Date"
                    dgvWorkViewer.Columns("TestDateStart").DefaultCellStyle.Format = "dd-MMM-yyyy"
                    dgvWorkViewer.Columns("TestDateStart").DisplayIndex = 10
                    dgvWorkViewer.Columns("ReceivedDate").HeaderText = "Received Date"
                    dgvWorkViewer.Columns("ReceivedDate").DisplayIndex = 11
                    dgvWorkViewer.Columns("ReceivedDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                    dgvWorkViewer.Columns("CompleteDate").HeaderText = "Complete Date"
                    dgvWorkViewer.Columns("CompleteDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                    dgvWorkViewer.Columns("CompleteDate").DisplayIndex = 12
                    dgvWorkViewer.Columns("Status").HeaderText = "Report Open/Closed"
                    dgvWorkViewer.Columns("Status").DisplayIndex = 13
                    dgvWorkViewer.Columns("strComplianceStatus").HeaderText = "Compliance Status"
                    dgvWorkViewer.Columns("strComplianceStatus").DisplayIndex = 14
                    dgvWorkViewer.Columns("mmoCommentAREA").HeaderText = "Comment Field"
                    dgvWorkViewer.Columns("mmoCommentAREA").DisplayIndex = 15
                    dgvWorkViewer.Columns("strPreComplianceStatus").HeaderText = "Precompliance Status"
                    dgvWorkViewer.Columns("strPreComplianceStatus").DisplayIndex = 16
                    dgvWorkViewer.Columns("strWitnessingEngineer").Visible = False
                    dgvWorkViewer.Columns("strWitnessingEngineer2").Visible = False
                    dgvWorkViewer.Columns("strUserUnit").Visible = False

                    LoadCompliaceColor()

                Case "Monitoring Test Notifications"
                    SQL = "select  " & _
                    "AIRBranch.ISMPTestNotification.strTestLogNumber as TestNumber,    " & _
                    "case    " & _
                    "when strReferenceNumber is null then ''    " & _
                    "else strReferenceNumber    " & _
                    "end RefNum,    " & _
                    "case  " & _
                    "when AIRBranch.ISMPTestNOtification.strAIRSNumber is Null then ''  " & _
                    "else AIRBranch.APBFacilityInformation.strFacilityName    " & _
                    "End FacilityName,  " & _
                    "substr(AIRBranch.ISMPTestNOtification.strAIRSNumber, 5) as AIRSNumber,  " & _
                    "strEmissionUnit,   " & _
                    "to_char(datProposedStartDate, 'dd-Mon-yyyy') as ProposedStartDate,  " & _
                    "case  " & _
                    "when strFirstName is Null then ''  " & _
                    "else(strLastName||', '||strFirstName)   " & _
                    "END StaffResponsible  " & _
                    "from AIRBranch.ismptestnotification, AIRBranch.APBFacilityinformation,  " & _
                    "AIRBranch.EPDUserProfiles, AIRBranch.ISMPTestLogLink  " & _
                    "where AIRBranch.ismptestnotification.strairsnumber = " & _
                    DBNameSpace & ".apbfacilityinformation.strairsnumber (+)    " & _
                    "and AIRBranch.ismptestnotification.strstaffresponsible = " & _
                    DBNameSpace & ".EPDUserProfiles.numUserID (+)  " & _
                    "and AIRBranch.ISMPTestnotification.strTestLogNumber = " & _
                    DBNameSpace & ".ISMPTestLogLink.strTestLogNumber (+)   " & _
                    "and datProposedStartDate > (sysdate - 180)    " & _
                    "and strReferenceNumber is null    " & _
                    "union    " & _
                    "select    " & _
                    "AIRBranch.ISMPTestNotification.strTestLogNumber as TestNumber,  " & _
                    "AIRBranch.ISMpReportInformation.strReferenceNumber as RefNum,    " & _
                    "case  " & _
                    "when AIRBranch.ISMPTestNOtification.strAIRSNumber is Null then ''  " & _
                    "else AIRBranch.APBFacilityInformation.strFacilityName    " & _
                    "End FacilityName,  " & _
                    "substr(AIRBranch.ISMPTestNOtification.strAIRSNumber, 5) as AIRSNumber,  " & _
                    "strEmissionUnit,   " & _
                    "to_char(datProposedStartDate, 'dd-Mon-yyyy') as ProposedStartDate,  " & _
                    "case  " & _
                    "when strFirstName is Null then ''  " & _
                    "else(strLastName||', '||strFirstName)   " & _
                    "END StaffResponsible  " & _
                    "from AIRBranch.ismptestnotification, AIRBranch.APBFacilityinformation,  " & _
                    "AIRBranch.EPDUserProfiles, AIRBranch.ISMPTestLogLink,    " & _
                    "AIRBranch.ISMPReportInformation    " & _
                    "where AIRBranch.ismptestnotification.strairsnumber = " & _
                    DBNameSpace & ".apbfacilityinformation.strairsnumber (+)    " & _
                    "and AIRBranch.ismptestnotification.strstaffresponsible = " & _
                    DBNameSpace & ".EPDUserProfiles.numUserID (+)  " & _
                    "and AIRBranch.ISMPTestNotification.strTestLogNumber = " & _
                    DBNameSpace & ".ISMPTestLogLink.strTestLogNumber (+)    " & _
                    "and AIRBranch.ISMPTestLogLink.strReferencenumber = " & _
                    DBNameSpace & ".ISMPReportInformation.strReferenceNumber (+)    " & _
                    "and datProposedStartDate > (sysdate - 180)    " & _
                    "and AIRBranch.ISMPTestLogLink.strReferenceNumber is not null    " & _
                    "and strClosed = 'False'  "

                    If rdbStaffView.Checked = True Then

                    End If
                    If rdbUCView.Checked = True Then

                    End If
                    If rdbPMView.Checked = True Then

                    End If

                    dsOpenWork = New DataSet
                    daOpenWork = New OracleDataAdapter(SQL, Conn)
                    If SQL <> "" Then
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        daOpenWork.Fill(dsOpenWork, "OpenWork")
                    End If

                    dgvWorkViewer.DataSource = dsOpenWork
                    dgvWorkViewer.DataMember = "OpenWork"

                    dgvWorkViewer.RowHeadersVisible = False
                    dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvWorkViewer.AllowUserToResizeColumns = True
                    dgvWorkViewer.AllowUserToAddRows = False
                    dgvWorkViewer.AllowUserToDeleteRows = False
                    dgvWorkViewer.AllowUserToOrderColumns = True
                    dgvWorkViewer.AllowUserToResizeRows = True
                    dgvWorkViewer.ColumnHeadersHeight = "35"
                    dgvWorkViewer.Columns("TestNumber").HeaderText = "Test Log #"
                    dgvWorkViewer.Columns("TestNumber").DisplayIndex = 0
                    dgvWorkViewer.Columns("RefNum").HeaderText = "Reference #"
                    dgvWorkViewer.Columns("RefNum").DisplayIndex = 1
                    dgvWorkViewer.Columns("AIRSNumber").HeaderText = "AIRS #"
                    dgvWorkViewer.Columns("AIRSNumber").DisplayIndex = 2
                    dgvWorkViewer.Columns("FacilityName").HeaderText = "Facility Name"
                    dgvWorkViewer.Columns("FacilityName").DisplayIndex = 3
                    dgvWorkViewer.Columns("strEmissionUnit").HeaderText = "Emission Unit"
                    dgvWorkViewer.Columns("strEmissionUnit").DisplayIndex = 4
                    dgvWorkViewer.Columns("ProposedStartDate").HeaderText = "Start Date"
                    dgvWorkViewer.Columns("ProposedStartDate").DisplayIndex = 5
                    dgvWorkViewer.Columns("StaffResponsible").HeaderText = "Staff Responsible"
                    dgvWorkViewer.Columns("StaffResponsible").DisplayIndex = 6

                Case "Permit Applications"
                    SQL = "Select " & _
                   "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber, " & _
                   "case " & _
                   " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' ' " & _
                   "	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' ' " & _
                   "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5) " & _
                   "end as strAIRSNumber, " & _
                   "case " & _
                   "	when strApplicationTypeDesc IS Null then ' ' " & _
                   "Else strApplicationTypeDesc " & _
                   "End as strApplicationType, " & _
                   "case " & _
                   " 	when datReceivedDate is Null then ' ' " & _
                   "Else to_char(datReceivedDate, 'RRRR-MM-dd') " & _
                   " End as datReceivedDate, " & _
                   "case  " & _
                   "when strPermitNumber is NULL then ' '  " & _
                   "else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'  " & _
                   " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-' " & _
                   " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1) " & _
                   "end As strPermitNumber, " & _
                   "case " & _
                   " 	when numUserID= '0' then ' ' " & _
                   "	when numUserID is Null then ' ' " & _
                   "else (strLastName||', '||strFirstName) " & _
                   "end as StaffResponsible, " & _
                   "case  " & _
                   "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd') " & _
                   "when datFinalizedDate is not Null then to_char(datFinalizedDate, 'RRRR-MM-dd') " & _
                   "when datToDirector is Not Null and datFinalizedDate is Null " & _
                   "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd') " & _
                   "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                   "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')  " & _
                   "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')   " & _
                   "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')   " & _
                   "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd') " & _
                   "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd') " & _
                   "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd') " & _
                   "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd') " & _
                   "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')   " & _
                   "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown' " & _
                   "else to_char(datAssignedToEngineer, 'RRRR-MM-dd') " & _
                   "end as StatusDate,  " & _
                   "case  " & _
                   " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '  " & _
                   "else AIRBranch.SSPPApplicationData.strFacilityName  " & _
                   "end as strFacilityName,  " & _
                   "case " & _
                   "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out' " & _
                   "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO' " & _
                   "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                   "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC' " & _
                   "when datEPAEnds is not Null then '08 - EPA 45-day Review' " & _
                   "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired' " & _
                   "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'  " & _
                   "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'  " & _
                   "when dattoPMII is Not Null then '04 - AT PM'  " & _
                   "when dattoPMI is Not Null then '03 - At UC'  " & _
                   "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')    " & _
                   "then '02 - Internal Review' " & _
                   "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'   " & _
                   "else '01 - At Engineer'  " & _
                   "end as AppStatus, " & _
                   "case " & _
                   " 	when strPermitTypeDescription is Null then '' " & _
                   "else strPermitTypeDescription " & _
                   "End as strPermitType " & _
                   "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking, " & _
                   "AIRBranch.SSPPApplicationData, " & _
                   "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes, " & _
                   "AIRBranch.EPDuserProfiles  " & _
                   "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)  " & _
                   "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+) " & _
                   "and strApplicationType = strApplicationTypeCode (+) " & _
                   "and strPermitType = strPermitTypeCode (+) " & _
                   "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " & _
                   "and datFinalizedDate is NULL " & _
                  "and numUserID = ('" & UserGCode & "') "

                    If rdbStaffView.Checked = True Then
                        SQL = "Select " & _
                  "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber, " & _
                  "case " & _
                  " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' ' " & _
                  "	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' ' " & _
                  "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5) " & _
                  "end as strAIRSNumber, " & _
                  "case " & _
                  "	when strApplicationTypeDesc IS Null then ' ' " & _
                  "Else strApplicationTypeDesc " & _
                  "End as strApplicationType, " & _
                  "case " & _
                  " 	when datReceivedDate is Null then ' ' " & _
                  "Else to_char(datReceivedDate, 'RRRR-MM-dd') " & _
                  " End as datReceivedDate, " & _
                  "case  " & _
                  "when strPermitNumber is NULL then ' '  " & _
                  "else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'  " & _
                  " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-' " & _
                  " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1) " & _
                  "end As strPermitNumber, " & _
                  "case " & _
                  " 	when numUserID= '0' then ' ' " & _
                  "	when numUserID is Null then ' ' " & _
                  "else (strLastName||', '||strFirstName) " & _
                  "end as StaffResponsible, " & _
                  "case  " & _
                  "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd') " & _
                  "when datFinalizedDate is not Null then to_char(datFinalizedDate, 'RRRR-MM-dd') " & _
                  "when datToDirector is Not Null and datFinalizedDate is Null " & _
                  "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd') " & _
                  "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                  "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')  " & _
                  "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')   " & _
                  "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')   " & _
                  "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd') " & _
                  "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd') " & _
                  "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd') " & _
                  "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd') " & _
                  "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')   " & _
                  "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown' " & _
                  "else to_char(datAssignedToEngineer, 'RRRR-MM-dd') " & _
                  "end as StatusDate,  " & _
                  "case  " & _
                  " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '  " & _
                  "else AIRBranch.SSPPApplicationData.strFacilityName  " & _
                  "end as strFacilityName,  " & _
                  "case " & _
                  "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out' " & _
                  "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO' " & _
                  "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                  "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC' " & _
                  "when datEPAEnds is not Null then '08 - EPA 45-day Review' " & _
                  "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired' " & _
                  "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'  " & _
                  "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'  " & _
                  "when dattoPMII is Not Null then '04 - AT PM'  " & _
                  "when dattoPMI is Not Null then '03 - At UC'  " & _
                  "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')    " & _
                  "then '02 - Internal Review' " & _
                  "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'   " & _
                  "else '01 - At Engineer'  " & _
                  "end as AppStatus, " & _
                  "case " & _
                  " 	when strPermitTypeDescription is Null then '' " & _
                  "else strPermitTypeDescription " & _
                  "End as strPermitType " & _
                  "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking, " & _
                  "AIRBranch.SSPPApplicationData, " & _
                  "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes, " & _
                  "AIRBranch.EPDuserProfiles  " & _
                  "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)  " & _
                  "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+) " & _
                  "and strApplicationType = strApplicationTypeCode (+) " & _
                  "and strPermitType = strPermitTypeCode (+) " & _
                  "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " & _
                  "and datFinalizedDate is NULL " & _
                  "and numUserID = ('" & UserGCode & "') "
                    End If
                    If rdbUCView.Checked = True Then
                        SQL = "Select " & _
                    "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber, " & _
                    "case " & _
                    " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' ' " & _
                    "	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' ' " & _
                    "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5) " & _
                    "end as strAIRSNumber, " & _
                    "case " & _
                    "	when strApplicationTypeDesc IS Null then ' ' " & _
                    "Else strApplicationTypeDesc " & _
                    "End as strApplicationType, " & _
                    "case " & _
                    " 	when datReceivedDate is Null then ' ' " & _
                    "Else to_char(datReceivedDate, 'RRRR-MM-dd') " & _
                    " End as datReceivedDate, " & _
                    "case  " & _
                    "when strPermitNumber is NULL then ' '  " & _
                    "else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'  " & _
                    " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-' " & _
                    " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1) " & _
                    "end As strPermitNumber, " & _
                    "case " & _
                    " 	when numUserID= '0' then ' ' " & _
                    "	when numUserID is Null then ' ' " & _
                    "else (strLastName||', '||strFirstName) " & _
                    "end as StaffResponsible, " & _
                    "case  " & _
                    "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd') " & _
                    "when datFinalizedDate is not Null then to_char(datFinalizedDate, 'RRRR-MM-dd') " & _
                    "when datToDirector is Not Null and datFinalizedDate is Null " & _
                    "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd') " & _
                    "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                    "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')  " & _
                    "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')   " & _
                    "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')   " & _
                    "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd') " & _
                    "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd') " & _
                    "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd') " & _
                    "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd') " & _
                    "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')   " & _
                    "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown' " & _
                    "else to_char(datAssignedToEngineer, 'RRRR-MM-dd') " & _
                    "end as StatusDate,  " & _
                    "case  " & _
                    " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '  " & _
                    "else AIRBranch.SSPPApplicationData.strFacilityName  " & _
                    "end as strFacilityName,  " & _
                    "case " & _
                    "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out' " & _
                    "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO' " & _
                    "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                    "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC' " & _
                    "when datEPAEnds is not Null then '08 - EPA 45-day Review' " & _
                    "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired' " & _
                    "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'  " & _
                    "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'  " & _
                    "when dattoPMII is Not Null then '04 - AT PM'  " & _
                    "when dattoPMI is Not Null then '03 - At UC'  " & _
                    "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')    " & _
                    "then '02 - Internal Review' " & _
                    "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'   " & _
                    "else '01 - At Engineer'  " & _
                    "end as AppStatus, " & _
                    "case " & _
                    " 	when strPermitTypeDescription is Null then '' " & _
                    "else strPermitTypeDescription " & _
                    "End as strPermitType " & _
                    "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking, " & _
                    "AIRBranch.SSPPApplicationData, " & _
                    "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes, " & _
                    "AIRBranch.EPDuserProfiles  " & _
                    "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)  " & _
                    "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+) " & _
                    "and strApplicationType = strApplicationTypeCode (+) " & _
                    "and strPermitType = strPermitTypeCode (+) " & _
                    "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " & _
                    "and datFinalizedDate is NULL " & _
                    " and (AIRBranch.EPDUserProfiles.numUnit = '" & UserUnit & "'   or (APBUnit = '" & UserUnit & "'))  "
                    End If
                    If rdbPMView.Checked = True Or UserUnit = "---" Then
                        SQL = "Select " & _
                      "distinct(to_Number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber, " & _
                      "case " & _
                      " 	when AIRBranch.SSPPApplicationMaster.strAIRSNumber is Null then ' ' " & _
                      "	when AIRBranch.SSPPApplicationMaster.strAIRSNumber = '0413' then ' ' " & _
                      "else substr(AIRBranch.SSPPApplicationMaster.strAIRSNumber, 5) " & _
                      "end as strAIRSNumber, " & _
                      "case " & _
                      "	when strApplicationTypeDesc IS Null then ' ' " & _
                      "Else strApplicationTypeDesc " & _
                      "End as strApplicationType, " & _
                      "case " & _
                      " 	when datReceivedDate is Null then ' ' " & _
                      "Else to_char(datReceivedDate, 'RRRR-MM-dd') " & _
                      " End as datReceivedDate, " & _
                      "case  " & _
                      "when strPermitNumber is NULL then ' '  " & _
                      "else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'  " & _
                      " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-' " & _
                      " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1) " & _
                      "end As strPermitNumber, " & _
                      "case " & _
                      " 	when numUserID= '0' then ' ' " & _
                      "	when numUserID is Null then ' ' " & _
                      "else (strLastName||', '||strFirstName) " & _
                      "end as StaffResponsible, " & _
                      "case  " & _
                      "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd') " & _
                      "when datFinalizedDate is not Null then to_char(datFinalizedDate, 'RRRR-MM-dd') " & _
                      "when datToDirector is Not Null and datFinalizedDate is Null " & _
                      "and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd') " & _
                      "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                      "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')  " & _
                      "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')   " & _
                      "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')   " & _
                      "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd') " & _
                      "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd') " & _
                      "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd') " & _
                      "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd') " & _
                      "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')   " & _
                      "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown' " & _
                      "else to_char(datAssignedToEngineer, 'RRRR-MM-dd') " & _
                      "end as StatusDate,  " & _
                      "case  " & _
                      " 	when AIRBranch.SSPPApplicationData.strFacilityName is Null then ' '  " & _
                      "else AIRBranch.SSPPApplicationData.strFacilityName  " & _
                      "end as strFacilityName,  " & _
                      "case " & _
                      "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out' " & _
                      "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO' " & _
                      "when datToBranchCheif is Not Null and datFinalizedDate is Null " & _
                      "and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC' " & _
                      "when datEPAEnds is not Null then '08 - EPA 45-day Review' " & _
                      "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired' " & _
                      "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'  " & _
                      "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'  " & _
                      "when dattoPMII is Not Null then '04 - AT PM'  " & _
                      "when dattoPMI is Not Null then '03 - At UC'  " & _
                      "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0')    " & _
                      "then '02 - Internal Review' " & _
                      "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'   " & _
                      "else '01 - At Engineer'  " & _
                      "end as AppStatus, " & _
                      "case " & _
                      " 	when strPermitTypeDescription is Null then '' " & _
                      "else strPermitTypeDescription " & _
                      "End as strPermitType " & _
                      "from AIRBranch.SSPPApplicationMaster, AIRBranch.SSPPApplicationTracking, " & _
                      "AIRBranch.SSPPApplicationData, " & _
                      "AIRBranch.LookUpApplicationTypes, AIRBranch.LookUPPermitTypes, " & _
                      "AIRBranch.EPDuserProfiles  " & _
                      "where AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationData.strApplicationNumber (+)  " & _
                      "and AIRBranch.SSPPApplicationMaster.strApplicationNumber = AIRBranch.SSPPApplicationTracking.strApplicationNumber (+) " & _
                      "and strApplicationType = strApplicationTypeCode (+) " & _
                      "and strPermitType = strPermitTypeCode (+) " & _
                      "and AIRBranch.EPDUserProfiles.numUserID = AIRBranch.SSPPApplicationMaster.strStaffResponsible " & _
                      "and datFinalizedDate is NULL "
                    End If
                    SQL = SQL & "order by AIRBranch.SSPPApplicationMaster.strApplicationNumber DESC  "

                    dsOpenWork = New DataSet
                    daOpenWork = New OracleDataAdapter(SQL, Conn)
                    If SQL <> "" Then
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        daOpenWork.Fill(dsOpenWork, "OpenWork")
                    End If

                    dgvWorkViewer.DataSource = dsOpenWork
                    dgvWorkViewer.DataMember = "OpenWork"

                    dgvWorkViewer.RowHeadersVisible = False
                    dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvWorkViewer.AllowUserToResizeColumns = True
                    dgvWorkViewer.AllowUserToAddRows = False
                    dgvWorkViewer.AllowUserToDeleteRows = False
                    dgvWorkViewer.AllowUserToOrderColumns = True
                    dgvWorkViewer.AllowUserToResizeRows = True
                    dgvWorkViewer.ColumnHeadersHeight = "35"
                    dgvWorkViewer.Columns("strApplicationNumber").HeaderText = "APL #"
                    dgvWorkViewer.Columns("strApplicationNumber").DisplayIndex = 0
                    dgvWorkViewer.Columns("strAIRSNumber").HeaderText = "AIRS #"
                    dgvWorkViewer.Columns("strAIRSNumber").DisplayIndex = 1
                    dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
                    dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 2
                    dgvWorkViewer.Columns("StaffResponsible").HeaderText = "Staff Responsible"
                    dgvWorkViewer.Columns("StaffResponsible").DisplayIndex = 3
                    dgvWorkViewer.Columns("strApplicationType").HeaderText = "APL Type"
                    dgvWorkViewer.Columns("strApplicationType").DisplayIndex = 4
                    dgvWorkViewer.Columns("datReceivedDate").HeaderText = "APL Rcvd"
                    dgvWorkViewer.Columns("datReceivedDate").DisplayIndex = 5
                    dgvWorkViewer.Columns("datReceivedDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                    dgvWorkViewer.Columns("strPermitNumber").HeaderText = "Permit Number"
                    dgvWorkViewer.Columns("strPermitNumber").DisplayIndex = 6
                    dgvWorkViewer.Columns("AppStatus").HeaderText = "App Status"
                    dgvWorkViewer.Columns("AppStatus").DisplayIndex = 8
                    dgvWorkViewer.Columns("StatusDate").HeaderText = "Status Date"
                    dgvWorkViewer.Columns("StatusDate").DisplayIndex = 9
                    dgvWorkViewer.Columns("strPermitType").HeaderText = "Action Type"
                    dgvWorkViewer.Columns("strPermitType").DisplayIndex = 7

                Case Else

            End Select
            txtDataGridCount.Text = dgvWorkViewer.RowCount.ToString

            dgvWorkViewer.SanelyResizeColumns()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub txtAIRSNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAIRSNumber.KeyPress
        Try
            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtAIRSNumber.Text.Length = 8 Then
                OpenFacilitySummary()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiExport.Click
        If dgvWorkViewer.RowCount > 0 Then
            dgvWorkViewer.ExportToExcel(Me)
        End If
    End Sub

    Private Sub mmiPermitUploader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPermitUploader.Click
        SsppFileUploader.Show()
    End Sub

    Private Sub mmiResetForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiResetForm.Click
        ResetAllFormSettings()
        Me.Location = New Point(0, 0)
    End Sub

    Private Sub mmiEnforcementUploader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiEnforcementUploader.Click
        SscpDocuments.Show()
    End Sub
End Class