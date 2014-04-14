Imports System.IO
Imports System.Collections.Generic
Imports Iaip.DAL.NavigationScreenData

Public Class IAIPNavigation

#Region " Local variables and properties "

    Private dtWorkViewerTable As DataTable

    Private _currentWorkViewerContext As WorkViewerType
    Private Property CurrentWorkViewerContext() As WorkViewerType
        Get
            Return _currentWorkViewerContext
        End Get
        Set(ByVal value As WorkViewerType)
            _currentWorkViewerContext = value
        End Set
    End Property

    Private _currentWorkViewerContextParameter As String
    Private Property CurrentWorkViewerContextParameter() As String
        Get
            Return _currentWorkViewerContextParameter
        End Get
        Set(ByVal value As String)
            _currentWorkViewerContextParameter = value
        End Set
    End Property

#End Region

#Region " Form events "

    Private Sub APBNavigation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Main." & Me.Name)
        Try
            IAIPLogIn.Hide()

            LoadNavButtons()

            BuildListChangerCombo()

            pnl2.Text = UserName
            pnl3.Text = OracleDate

            LoadProgramDescription()

            EnableConnectionEnvironmentOptions()

            ' Timers
            App.StartAppTimers()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub IAIPNavigation_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        monitor.TrackFeatureStop("Startup.LoggingIn")
        LoadWorkViewerData()
    End Sub

    Private Sub NavigationScreen_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        CurrentConnection.Dispose()
        Application.Exit()
    End Sub

#End Region

#Region " Page Load procedures "

    Private Sub LoadNavButtons()
        If bgrLoadButtons.IsBusy Then
            bgrLoadButtons.CancelAsync()
        Else
            bgrLoadButtons.WorkerReportsProgress = True
            bgrLoadButtons.WorkerSupportsCancellation = True
            bgrLoadButtons.RunWorkerAsync()
        End If
    End Sub

    Private Sub LoadProgramDescription()
        Dim id As Integer

        If Integer.TryParse(UserProgram, id) Then
            pnl1.Text = DAL.GetProgramDescription(id)
        End If
    End Sub

    Private Sub BuildListChangerCombo()
        cboWorkViewerContext.Items.Clear()

        cboWorkViewerContext.Items.Add("Default List")

        cboWorkViewerContext.Items.Add("Compliance Facilities Assigned")
        cboWorkViewerContext.Items.Add("Compliance Work")
        cboWorkViewerContext.Items.Add("Delinquent Full Compliance Evaluations")
        cboWorkViewerContext.Items.Add("Enforcement")
        cboWorkViewerContext.Items.Add("Facilities with Subparts")
        cboWorkViewerContext.Items.Add("Facilities missing Subparts")
        cboWorkViewerContext.Items.Add("Monitoring Test Reports")
        cboWorkViewerContext.Items.Add("Monitoring Test Notifications")
        cboWorkViewerContext.Items.Add("Permit Applications")

        cboWorkViewerContext.SelectedIndex = 0
    End Sub

    Private Sub EnableConnectionEnvironmentOptions()

        If CurrentConnectionEnvironment <> DB.ConnectionEnvironment.Production Then
            lblTitle.Text = lblTitle.Text & " — " & CurrentConnectionEnvironment.ToString
        End If

        If DevelopmentEnvironment Then
            pnl4.Text = "TESTING ENVIRONMENT"
            pnl4.BackColor = Color.Tomato
            pnl4.Visible = True

            'mmiTesting.Visible = True
            'mmiTesting.Enabled = True
        Else
            pnl4.Text = ""
            pnl4.Visible = False

            mmiTesting.Visible = False
            mmiTesting.Enabled = False
        End If

#If NadcEnabled Then

        If NadcServer Then
            pnl5.Text = "NADC Server"
            pnl5.BackColor = Color.DarkOrange
            pnl5.Visible = True
        Else
            pnl5.Text = ""
            pnl5.Visible = False
        End If

#End If

    End Sub

#End Region

#Region "Quick Access Tool link clicked and keypress events"

    Private Sub LLSelectReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LLSelectReport.LinkClicked
        OpenTestReport()
    End Sub
    Private Sub llbEnforcementRecord_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEnforcementRecord.LinkClicked
        OpenEnforcement()
    End Sub
    Private Sub llbOpenApplication_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbOpenApplication.LinkClicked
        OpenApplication()
    End Sub
    Private Sub llbTrackingNumber_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbTrackingNumber.LinkClicked
        OpenSSCPWork()
    End Sub
    Private Sub llbFacilitySummary_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFacilitySummary.LinkClicked
        OpenFacilitySummary()
    End Sub
    Private Sub llbOpenTestLog_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbOpenTestLog.LinkClicked
        OpenTestNotification()
    End Sub

    Private Sub txtApplicationNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtApplicationNumber.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then OpenApplication()
    End Sub
    Private Sub txtEnforcementNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEnforcementNumber.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then OpenEnforcement()
    End Sub
    Private Sub txtReferenceNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReferenceNumber.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then OpenTestReport()
    End Sub
    Private Sub txtTrackingNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTrackingNumber.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then OpenSSCPWork()
    End Sub
    Private Sub txtAIRSNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAIRSNumber.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then OpenFacilitySummary()
    End Sub
    Private Sub txtTestLogNumber_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTestLogNumber.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then OpenTestNotification()
    End Sub

#End Region

#Region "Quick Access Tool procedures"

    Private Sub OpenApplication()
        Try
            Dim id As String = txtApplicationNumber.Text
            If id = "" Then Exit Sub

            If DAL.SSPP.ApplicationExists(id) Then
                If PermitTrackingLog IsNot Nothing AndAlso Not PermitTrackingLog.IsDisposed Then
                    PermitTrackingLog.Dispose()
                End If

                PermitTrackingLog = New SSPPApplicationTrackingLog
                PermitTrackingLog.Show()
                PermitTrackingLog.txtApplicationNumber.Text = txtApplicationNumber.Text
                PermitTrackingLog.LoadApplication()
                PermitTrackingLog.TPTrackingLog.Focus()
            Else
                MsgBox("Application number is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub OpenTestReport()
        Try
            Dim id As String = txtReferenceNumber.Text
            If id = "" Then Exit Sub

            If DAL.ISMP.StackTestExists(id) Then
                If UserProgram = "3" Then
                    OpenMultiForm(ISMPTestReports, id)
                Else
                    If DAL.ISMP.StackTestIsClosedOut(id) Then
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

    Private Sub OpenEnforcement()
        Try
            Dim id As String = txtEnforcementNumber.Text
            If id = "" Then Exit Sub
            If DAL.SSCP.EnforcementExists(id) Then
                OpenMultiForm(SscpEnforcement, id)
            Else
                MsgBox("Enforcement number is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub OpenSSCPWork()
        Try
            Dim id As String = txtTrackingNumber.Text
            If id = "" Then Exit Sub

            If DAL.SSCP.WorkItemExists(id) Then
                Dim refNum As String = ""
                If DAL.SSCP.WorkItemIsAStackTest(id, refNum) Then
                    OpenMultiForm(ISMPTestReports, refNum)
                Else
                    If SSCPReports IsNot Nothing AndAlso Not SSCPReports.IsDisposed Then
                        SSCPReports.Dispose()
                    End If
                    SSCPReports = New SSCPEvents
                    SSCPReports.txtTrackingNumber.Text = id
                    SSCPReports.Show()
                End If
            Else
                MsgBox("Tracking number is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub OpenFacilitySummary()
        If txtAIRSNumber.TextLength = 0 Then
            OpenSingleForm(IAIPFacilitySummary)
            Exit Sub
        End If

        If Not DAL.FacilityInfo.AirsNumberExists(txtAIRSNumber.Text) Then
            MsgBox("AIRS Number is not in the system.", MsgBoxStyle.Information, "Navigation Screen")
            Exit Sub
        End If

        Dim parameters As New Generic.Dictionary(Of String, String)
        parameters("airsnumber") = txtAIRSNumber.Text
        OpenSingleForm(IAIPFacilitySummary, parameters:=parameters, closeFirst:=True)
    End Sub

    Private Sub OpenTestNotification()
        Try
            Dim id As String = txtTestLogNumber.Text
            If id = "" Then Exit Sub

            If DAL.ISMP.TestNotificationExists(id) Then
                If ISMPNotificationLogForm IsNot Nothing AndAlso Not ISMPNotificationLogForm.IsDisposed Then
                    ISMPNotificationLogForm.Dispose()
                End If

                ISMPNotificationLogForm = New ISMPNotificationLog
                ISMPNotificationLogForm.txtTestNotificationNumber.Text = id
                ISMPNotificationLogForm.Show()
            Else
                MsgBox("Notification number is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ClearQuickAccessTool()
        txtAIRSNumber.Clear()
        txtEnforcementNumber.Clear()
        txtApplicationNumber.Clear()
        txtReferenceNumber.Clear()
        txtTrackingNumber.Clear()
        txtTestLogNumber.Clear()
    End Sub

#End Region

#Region "WorkViewer context selector"

    Private Sub SetWorkViewerContext()
        Try

            CurrentWorkViewerContext = WorkViewerType.None
            CurrentWorkViewerContextParameter = Nothing

            Select Case cboWorkViewerContext.Text
                Case "Default List"
                    Select Case UserBranch
                        Case "1" 'Air Protection Branch
                            Select Case UserProgram

                                Case "3" 'ISMP
                                    If UserUnit = "---" Then 'Program Manager
                                        CurrentWorkViewerContext = WorkViewerType.ISMP_PM
                                    ElseIf AccountArray(17, 2) = "1" Then  'Unit Manager
                                        CurrentWorkViewerContext = WorkViewerType.ISMP_UC
                                        CurrentWorkViewerContextParameter = UserUnit
                                    Else
                                        CurrentWorkViewerContext = WorkViewerType.ISMP_Staff
                                        ' TODO (Doug): When a better user object is set up, change this (pnl2.Text)
                                        ' to something more appropriate
                                        CurrentWorkViewerContextParameter = pnl2.Text
                                    End If

                                Case "4" 'SSCP
                                    If UserUnit = "---" Then 'Program Manager
                                        CurrentWorkViewerContext = WorkViewerType.SSCP_PM
                                    ElseIf AccountArray(22, 3) = "1" Then 'Unit Manager
                                        CurrentWorkViewerContext = WorkViewerType.SSCP_UC
                                        CurrentWorkViewerContextParameter = UserUnit
                                    ElseIf AccountArray(10, 3) = "1" Then 'District Liaison
                                        CurrentWorkViewerContext = WorkViewerType.SSCP_DistrictLiaison
                                    Else
                                        CurrentWorkViewerContext = WorkViewerType.SSCP_Staff
                                        CurrentWorkViewerContextParameter = UserGCode
                                    End If

                                Case "5" 'SSPP
                                    If AccountArray(3, 3) = "1" And UserUnit = "---" Then  'Program Manager
                                        CurrentWorkViewerContext = WorkViewerType.SSPP_PM
                                    ElseIf AccountArray(24, 3) = "1" Then 'Unit Manager
                                        CurrentWorkViewerContext = WorkViewerType.SSPP_UC
                                        CurrentWorkViewerContextParameter = UserUnit
                                    ElseIf AccountArray(9, 3) = "1" Then 'Administrative 2
                                        CurrentWorkViewerContext = WorkViewerType.SSPP_Administrative
                                        CurrentWorkViewerContextParameter = UserGCode
                                    Else
                                        CurrentWorkViewerContext = WorkViewerType.SSPP_Staff
                                        CurrentWorkViewerContextParameter = UserGCode
                                    End If

                                Case Else
                                    CurrentWorkViewerContext = WorkViewerType.PermitApplications_PM

                            End Select

                        Case "5" 'Program Coordination 
                            If UserUnit = "---" Then 'Program Manager
                                CurrentWorkViewerContext = WorkViewerType.ProgCoord_PM
                            ElseIf AccountArray(22, 3) = "1" Then 'Unit Manager
                                CurrentWorkViewerContext = WorkViewerType.ProgCoord_UC
                                CurrentWorkViewerContextParameter = UserUnit
                            ElseIf AccountArray(10, 3) = "1" Then 'District Liaison
                                CurrentWorkViewerContext = WorkViewerType.ProgCoord_DistrictLiaison
                            Else
                                CurrentWorkViewerContext = WorkViewerType.ProgCoord_Staff
                                CurrentWorkViewerContextParameter = UserGCode
                            End If

                        Case Else
                            CurrentWorkViewerContext = WorkViewerType.None

                    End Select

                Case "Compliance Facilities Assigned"
                    If rdbUCView.Checked Or rdbPMView.Checked Then
                        CurrentWorkViewerContext = WorkViewerType.ComplianceFacilitiesAssigned_Program
                        CurrentWorkViewerContextParameter = UserProgram
                    Else
                        CurrentWorkViewerContext = WorkViewerType.ComplianceFacilitiesAssigned_Staff
                        CurrentWorkViewerContextParameter = UserGCode
                    End If

                Case "Compliance Work"
                    If rdbUCView.Checked Then
                        CurrentWorkViewerContext = WorkViewerType.ComplianceWork_UC
                        CurrentWorkViewerContextParameter = UserUnit
                        If UserProgram = "5" Then
                            CurrentWorkViewerContext = WorkViewerType.ComplianceWork_UC_ProgCoord
                            CurrentWorkViewerContextParameter = UserProgram
                        End If
                    ElseIf rdbPMView.Checked = True Then
                        CurrentWorkViewerContext = WorkViewerType.ComplianceWork_PM
                    Else
                        CurrentWorkViewerContext = WorkViewerType.ComplianceWork_Staff
                        CurrentWorkViewerContextParameter = UserGCode
                    End If

                Case "Delinquent Full Compliance Evaluations"
                    CurrentWorkViewerContext = WorkViewerType.DelinquentFCEs

                Case "Enforcement"
                    If rdbUCView.Checked Then
                        CurrentWorkViewerContext = WorkViewerType.Enforcement_UC
                        CurrentWorkViewerContextParameter = UserUnit
                        If UserProgram = "5" Then
                            CurrentWorkViewerContext = WorkViewerType.Enforcement_UC_ProgCoord
                            CurrentWorkViewerContextParameter = UserProgram
                        End If
                    ElseIf rdbPMView.Checked Then
                        CurrentWorkViewerContext = WorkViewerType.Enforcement_PM
                    Else
                        CurrentWorkViewerContext = WorkViewerType.Enforcement_Staff
                        CurrentWorkViewerContextParameter = UserGCode
                    End If

                Case "Facilities with Subparts"
                    CurrentWorkViewerContext = WorkViewerType.FacilitiesWithSubparts

                Case "Facilities missing Subparts"
                    CurrentWorkViewerContext = WorkViewerType.FacilitiesMissingSubparts

                Case "Monitoring Test Reports"
                    If rdbStaffView.Checked Then
                        CurrentWorkViewerContext = WorkViewerType.MonitoringTestReports_Staff
                        ' TODO (Doug): When a better user object is set up, change this (pnl2.Text)
                        ' to something more appropriate
                        CurrentWorkViewerContextParameter = pnl2.Text
                    ElseIf rdbUCView.Checked Then
                        CurrentWorkViewerContext = WorkViewerType.MonitoringTestReports_UC
                        CurrentWorkViewerContextParameter = UserUnit
                    ElseIf rdbPMView.Checked Or UserUnit = "---" Then
                        CurrentWorkViewerContext = WorkViewerType.MonitoringTestReports_PM
                    End If

                Case "Monitoring Test Notifications"
                    CurrentWorkViewerContext = WorkViewerType.MonitoringTestNotifications

                Case "Permit Applications"
                    If rdbUCView.Checked Then
                        CurrentWorkViewerContext = WorkViewerType.PermitApplications_UC
                        CurrentWorkViewerContextParameter = UserUnit
                    ElseIf rdbPMView.Checked Or UserUnit = "---" Then
                        CurrentWorkViewerContext = WorkViewerType.PermitApplications_PM
                    Else
                        CurrentWorkViewerContext = WorkViewerType.PermitApplications_Staff
                        CurrentWorkViewerContextParameter = UserGCode
                    End If

                Case Else
                    CurrentWorkViewerContext = WorkViewerType.None

            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "WorkViewer context selector events"

    Private Sub btnChangeWorkViewerContext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeWorkViewerContext.Click
        LoadWorkViewerData()
    End Sub

    Private Sub pnlCurrentList_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlCurrentList.Enter
        Me.AcceptButton = btnChangeWorkViewerContext
    End Sub

    Private Sub pnlCurrentList_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlCurrentList.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub cboWorkViewerContext_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWorkViewerContext.SelectedIndexChanged
        Select Case cboWorkViewerContext.Text
            Case "Default List"
                pnlContextSubView.Visible = False
            Case "Compliance Facilities Assigned"
                pnlContextSubView.Visible = True
            Case "Compliance Work"
                pnlContextSubView.Visible = True
            Case "Delinquent Full Compliance Evaluations"
                pnlContextSubView.Visible = False
            Case "Enforcement"
                pnlContextSubView.Visible = True
            Case "Facilities with Subparts"
                pnlContextSubView.Visible = False
            Case "Facilities missing Subparts"
                pnlContextSubView.Visible = False
            Case "Monitoring Test Reports"
                pnlContextSubView.Visible = True
            Case "Monitoring Test Notifications"
                pnlContextSubView.Visible = False
            Case "Permit Applications"
                pnlContextSubView.Visible = True
        End Select
    End Sub

#End Region

#Region "WorkViewer formatters"

    Private Sub FormatWorkViewer()
        If dgvWorkViewer.Visible = True Then

            dgvWorkViewer.RowHeadersVisible = False
            dgvWorkViewer.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvWorkViewer.AllowUserToResizeColumns = True
            dgvWorkViewer.AllowUserToAddRows = False
            dgvWorkViewer.AllowUserToDeleteRows = False
            dgvWorkViewer.AllowUserToOrderColumns = True
            dgvWorkViewer.AllowUserToResizeRows = True
            dgvWorkViewer.ColumnHeadersHeight = "35"

            Select Case CurrentWorkViewerContext

                Case WorkViewerType.ISMP_PM, WorkViewerType.ISMP_Staff, WorkViewerType.ISMP_UC, _
                WorkViewerType.MonitoringTestReports_PM, WorkViewerType.MonitoringTestReports_Staff, WorkViewerType.MonitoringTestReports_UC
                    FormatWorkViewerForTestReports()

                Case WorkViewerType.SSCP_DistrictLiaison, WorkViewerType.SSCP_PM, WorkViewerType.SSCP_Staff, WorkViewerType.SSCP_UC, _
                WorkViewerType.ProgCoord_DistrictLiaison, WorkViewerType.ProgCoord_PM, WorkViewerType.ProgCoord_Staff, WorkViewerType.ProgCoord_UC, _
                WorkViewerType.Enforcement_PM, WorkViewerType.Enforcement_Staff, WorkViewerType.Enforcement_UC, WorkViewerType.Enforcement_UC_ProgCoord
                    FormatWorkViewerForEnforcement()

                Case WorkViewerType.SSPP_Administrative, WorkViewerType.SSPP_PM, WorkViewerType.SSPP_Staff, WorkViewerType.SSPP_UC, _
                WorkViewerType.PermitApplications_PM, WorkViewerType.PermitApplications_Staff, WorkViewerType.PermitApplications_UC
                    FormatWorkViewerForPermitApplications()

                Case WorkViewerType.ComplianceFacilitiesAssigned_Program, WorkViewerType.ComplianceFacilitiesAssigned_Staff
                    FormatWorkViewerForComplianceFacilitiesAssigned()

                Case WorkViewerType.ComplianceWork_PM, WorkViewerType.ComplianceWork_Staff, _
                WorkViewerType.ComplianceWork_UC, WorkViewerType.ComplianceWork_UC_ProgCoord
                    FormatWorkViewerForComplianceWork()

                Case WorkViewerType.DelinquentFCEs
                    FormatWorkViewerForFCEs()

                Case WorkViewerType.FacilitiesMissingSubparts, WorkViewerType.FacilitiesWithSubparts
                    FormatWorkViewerForFacilitySubparts()

                Case WorkViewerType.MonitoringTestNotifications
                    FormatWorkViewerForTestNotifications()

            End Select

            dgvWorkViewer.SanelyResizeColumns()
        End If
    End Sub

    Private Sub FormatWorkViewerForComplianceFacilitiesAssigned()
        dgvWorkViewer.Columns("AIRSNumber").HeaderText = "AIRS #"
        dgvWorkViewer.Columns("AIRSNumber").DisplayIndex = 0
        dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
        dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 1
        dgvWorkViewer.Columns("strFacilityName").Width = dgvWorkViewer.Width * (0.5)
        dgvWorkViewer.Columns("Staff").HeaderText = "Staff Responsible"
        dgvWorkViewer.Columns("Staff").DisplayIndex = 2
        dgvWorkViewer.Columns("Staff").Width = dgvWorkViewer.Width * (0.25)
    End Sub

    Private Sub FormatWorkViewerForComplianceWork()
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
    End Sub

    Private Sub FormatWorkViewerForFCEs()
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
    End Sub

    Private Sub FormatWorkViewerForEnforcement()
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
    End Sub

    Private Sub FormatWorkViewerForFacilitySubparts()
        dgvWorkViewer.Columns("AIRSnumber").HeaderText = "AIRS #"
        dgvWorkViewer.Columns("AIRSnumber").DisplayIndex = 0
        dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
        dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 1
    End Sub

    Private Sub FormatWorkViewerForTestReports()
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

        LoadISMPComplianceColor()
    End Sub

    Private Sub FormatWorkViewerForTestNotifications()
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
    End Sub

    Private Sub FormatWorkViewerForPermitApplications()
        dgvWorkViewer.Columns("strApplicationNumber").HeaderText = "App #"
        dgvWorkViewer.Columns("strApplicationNumber").DisplayIndex = 0
        dgvWorkViewer.Columns("strAIRSNumber").HeaderText = "AIRS #"
        dgvWorkViewer.Columns("strAIRSNumber").DisplayIndex = 1
        dgvWorkViewer.Columns("strFacilityName").HeaderText = "Facility Name"
        dgvWorkViewer.Columns("strFacilityName").DisplayIndex = 2
        dgvWorkViewer.Columns("StaffResponsible").HeaderText = "Staff Responsible"
        dgvWorkViewer.Columns("StaffResponsible").DisplayIndex = 3
        dgvWorkViewer.Columns("strApplicationType").HeaderText = "App Type"
        dgvWorkViewer.Columns("strApplicationType").DisplayIndex = 4
        dgvWorkViewer.Columns("datReceivedDate").HeaderText = "App Rcvd"
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
    End Sub

    Private Sub LoadISMPComplianceColor()
        Try
            For Each row As DataGridViewRow In dgvWorkViewer.Rows
                If Not row.IsNewRow Then
                    If row.Cells(19).Value IsNot DBNull.Value AndAlso row.Cells(19).Value = "True" Then
                        row.DefaultCellStyle.BackColor = Color.Pink
                    End If
                    If row.Cells(13).Value IsNot DBNull.Value AndAlso row.Cells(13).Value = "Not In Compliance" Then
                        row.DefaultCellStyle.BackColor = Color.Tomato
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "WorkViewer background worker (bgrLoadWorkViewer)"

    Private Sub LoadWorkViewerData()
        dgvWorkViewer.Visible = False
        lblMessageLabel.Visible = True
        lblMessageLabel.Text = "Loading data…"
        'cboWorkViewerContext.Enabled = False
        'btnChangeWorkViewerContext.Enabled = False
        pnlCurrentList.Enabled = False
        btnChangeWorkViewerContext.Text = "Loading…"
        lblResultsCount.Visible = False
        lblResultsCount.Text = ""

        ClearQuickAccessTool()

        SetWorkViewerContext()
        Try
            If bgrLoadWorkViewer.IsBusy Then
                bgrLoadWorkViewer.CancelAsync()
            Else
                bgrLoadWorkViewer.RunWorkerAsync()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub bgrLoadWorkViewer_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgrLoadWorkViewer.DoWork
        Try
            dtWorkViewerTable = New DataTable
            dtWorkViewerTable = DAL.GetWorkViewerListAsDataTable(CurrentWorkViewerContext, CurrentWorkViewerContextParameter)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub bgrLoadWorkViewer_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgrLoadWorkViewer.RunWorkerCompleted
        'cboWorkViewerContext.Enabled = True
        'btnChangeWorkViewerContext.Enabled = True
        pnlCurrentList.Enabled = True
        btnChangeWorkViewerContext.Text = "Load"

        If dtWorkViewerTable.Rows.Count > 0 Then
            dgvWorkViewer.DataSource = dtWorkViewerTable

            dgvWorkViewer.Visible = True
            lblMessageLabel.Visible = False
            lblMessageLabel.Text = ""
            lblResultsCount.Visible = True
            lblResultsCount.Text = dtWorkViewerTable.Rows.Count & " results"

            FormatWorkViewer()
        Else
            dgvWorkViewer.DataSource = Nothing

            dgvWorkViewer.Visible = False
            lblMessageLabel.Visible = True
            lblMessageLabel.Text = "No data to display"
            lblResultsCount.Visible = False
            lblResultsCount.Text = ""
        End If
    End Sub

#End Region

#Region "WorkViewer (dgvWorkViewer) events"

    Private Sub dgvWorkViewer_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvWorkViewer.Sorted
        If CurrentWorkViewerContext = WorkViewerType.ISMP_PM _
        Or CurrentWorkViewerContext = WorkViewerType.ISMP_Staff _
        Or CurrentWorkViewerContext = WorkViewerType.ISMP_UC _
        Or CurrentWorkViewerContext = WorkViewerType.MonitoringTestReports_PM _
        Or CurrentWorkViewerContext = WorkViewerType.MonitoringTestReports_Staff _
        Or CurrentWorkViewerContext = WorkViewerType.MonitoringTestReports_UC Then
            LoadISMPComplianceColor()
        End If
    End Sub

    Private Sub dgvWorkViewer_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvWorkViewer.MouseUp
        ' TODO (Doug): Is this the best way to handle this?
        Dim hti As DataGridView.HitTestInfo = dgvWorkViewer.HitTest(e.X, e.Y)

        Try
            If dgvWorkViewer.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvWorkViewer.Columns(0).HeaderText = "Reference #" Then
                    txtReferenceNumber.Text = dgvWorkViewer(0, hti.RowIndex).Value
                    txtAIRSNumber.Text = dgvWorkViewer(1, hti.RowIndex).Value
                End If
                If dgvWorkViewer.Columns(0).HeaderText = "App #" Then
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

#End Region

#Region "Nav button click events"

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

#Region "Nav button procedure"

    Private Sub OpenNewForm(ByVal Source As String, ByVal Options As String)
        Try
            Select Case Source
                Case "Facility Summary" '1
                    OpenSingleForm(IAIPFacilitySummary)
                Case "DMU Tools" '2
                Case "Application Log" '3
                    OpenSingleForm(SSPPApplicationLog)
                Case "Compliance Log" '4
                    If SSCP_Work Is Nothing Then
                        If SSCP_Work Is Nothing Then SSCP_Work = New SSCPComplianceLog
                    Else
                        SSCP_Work.Dispose()
                        SSCP_Work = New SSCPComplianceLog
                    End If
                    SSCP_Work.Show()
                Case "Monitoring Log" '5
                    If ISMPReportViewer Is Nothing Then
                        If ISMPReportViewer Is Nothing Then ISMPReportViewer = New ISMPMonitoringLog
                    Else
                        ISMPReportViewer.Dispose()
                        ISMPReportViewer = New ISMPMonitoringLog
                    End If
                    ISMPReportViewer.Show()
                Case "Fees Log"
                    If FeesLog Is Nothing Then
                        If FeesLog Is Nothing Then FeesLog = New PASPFeesLog
                    Else
                        FeesLog.Dispose()
                        FeesLog = New PASPFeesLog
                    End If
                    FeesLog.Show()
                Case "Fee Management"
                    If FeeManagement Is Nothing Then
                        If FeeManagement Is Nothing Then FeeManagement = New PASPFeeManagement
                    Else
                        FeeManagement.Dispose()
                        FeeManagement = New PASPFeeManagement
                    End If
                    FeeManagement.Show()

                Case "Fee Statistics && Reports" ''"Fee Statistics && Mailout" '"Mailout && Statistics" '12
                    If MailoutAndStats Is Nothing Then
                        If MailoutAndStats Is Nothing Then MailoutAndStats = New PASPFeeStatistics
                    Else
                        MailoutAndStats.Dispose()
                        MailoutAndStats = New PASPFeeStatistics
                    End If
                    MailoutAndStats.Show()

                Case "IAIP Query Generator" '7
                    If QueryGenerator Is Nothing Then
                        If QueryGenerator Is Nothing Then QueryGenerator = New IAIPQueryGenerator
                    Else
                        QueryGenerator.Dispose()
                        QueryGenerator = New IAIPQueryGenerator
                    End If
                    QueryGenerator.Show()
                Case "Profile Management"  ' 8
                    If UserAdminTool Is Nothing Then
                        If UserAdminTool Is Nothing Then UserAdminTool = New IAIPUserAdminTool
                    Else
                        UserAdminTool.Dispose()
                        UserAdminTool = New IAIPUserAdminTool
                    End If
                    UserAdminTool.Show()
                Case "Permit File Uploader" '9
                    If PermitUploader Is Nothing Then
                        If PermitUploader Is Nothing Then PermitUploader = New IAIPPermitUploader
                    Else
                        PermitUploader.Show()
                    End If
                    PermitUploader.Show()
                Case "District Tools" '10
                    If IAIPDistrictTool Is Nothing Then
                        If IAIPDistrictTool Is Nothing Then IAIPDistrictTool = New IAIPDistrictSourceTool
                    Else
                        IAIPDistrictTool.Dispose()
                        IAIPDistrictTool = New IAIPDistrictSourceTool
                    End If
                    IAIPDistrictTool.Show()
                Case "AFS Validator" '11
                    If Validator Is Nothing Then
                        If Validator Is Nothing Then Validator = New AFSValidator
                    Else
                        Validator.Dispose()
                        Validator = New AFSValidator
                    End If
                    Validator.Show()
                Case "Fees Reports" '6
                    If FeesReports Is Nothing Then
                        If FeesReports Is Nothing Then FeesReports = New PASPFeeReports
                    Else
                        FeesReports.Dispose()
                        FeesReports = New PASPFeeReports
                    End If
                    FeesReports.Show()

                Case "APB Branch Tools" '13
                    If PrintOut Is Nothing Then
                        If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
                    Else
                        PrintOut.Dispose()
                        PrintOut = New IAIPPrintOut
                    End If
                    PrintOut.txtPrintType.Text = "OrgChart"

                    PrintOut.Show()
                Case "Test Report Information" '14
                    If ISMPFacility Is Nothing Then
                        If ISMPFacility Is Nothing Then ISMPFacility = New ISMPTestReportAdministrative
                    Else
                        ISMPFacility.Dispose()
                        ISMPFacility = New ISMPTestReportAdministrative
                    End If
                    ISMPFacility.Show()
                Case "Memo Viewer" '15
                    If ISMPMemoViewer Is Nothing Then
                        If ISMPMemoViewer Is Nothing Then ISMPMemoViewer = New ISMPTestMemoViewer
                    Else
                        ISMPMemoViewer.Dispose()
                        ISMPMemoViewer = New ISMPTestMemoViewer
                    End If
                    ISMPMemoViewer.Show()
                Case "Ref. Number Management" '16
                    If ISMPRefNum Is Nothing Then
                        If ISMPRefNum Is Nothing Then ISMPRefNum = New ISMPReferenceNumber
                    Else
                        ISMPRefNum.Dispose()
                        ISMPRefNum = New ISMPReferenceNumber
                    End If
                    ISMPRefNum.Show()
                Case "ISMP Managers" '17
                    If ISMPManagers Is Nothing Then
                        If ISMPManagers Is Nothing Then ISMPManagers = New ISMPManagersTools
                    Else
                        ISMPManagers.Dispose()
                        ISMPManagers = New ISMPManagersTools
                    End If
                    ISMPManagers.txtProgram.Text = "Industrial Source Monitoring"
                    ISMPManagers.Show()
                Case "Deposits" '18
                    If DepositsAmendments Is Nothing Then
                        If DepositsAmendments Is Nothing Then DepositsAmendments = New PASPDepositsAmendments
                    Else
                        DepositsAmendments.Dispose()
                        DepositsAmendments = New PASPDepositsAmendments
                    End If
                    DepositsAmendments.Show()
                Case "Attainment Status Tool" '19
                    If AttainmentStatus Is Nothing Then
                        If AttainmentStatus Is Nothing Then AttainmentStatus = New SSPPAttainmentStatus
                    Else
                        AttainmentStatus.Dispose()
                        AttainmentStatus = New SSPPAttainmentStatus
                    End If
                    AttainmentStatus.Show()
                Case "Emissions Summary Tool" '20
                    If EmissionSummary Is Nothing Then
                        If EmissionSummary Is Nothing Then EmissionSummary = New SSCPEmissionSummaryTool
                    Else
                        EmissionSummary.Dispose()
                        EmissionSummary = New SSCPEmissionSummaryTool
                    End If
                    EmissionSummary.Show()
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
                    Exit Sub


                    If SSCPInspectionsTool Is Nothing Then
                        If SSCPInspectionsTool Is Nothing Then SSCPInspectionsTool = New SSCPEngineerInspectionTool
                    Else
                        SSCPInspectionsTool.Dispose()
                        SSCPInspectionsTool = New SSCPEngineerInspectionTool
                    End If
                    SSCPInspectionsTool.Show()
                Case "Compliance Managers" '22
                    If ManagersTools Is Nothing Then
                        If ManagersTools Is Nothing Then ManagersTools = New SSCPManagersTools
                    Else
                        ManagersTools.Dispose()
                        ManagersTools = New SSCPManagersTools
                    End If
                    ManagersTools.Show()
                Case "PA/PN Report" '23
                    If PublicLetter2 Is Nothing Then
                        If PublicLetter2 Is Nothing Then PublicLetter2 = New SSPPPublicNoticiesAndAdvisories
                    Else
                        PublicLetter2.Dispose()
                        PublicLetter2 = New SSPPPublicNoticiesAndAdvisories
                    End If
                    PublicLetter2.Show()
                Case "SSPP Tools" '24
                    If StatisticalTools Is Nothing Then
                        If StatisticalTools Is Nothing Then StatisticalTools = New SSPPStatisticalTools
                    Else
                        StatisticalTools.Show()
                    End If
                    StatisticalTools.Show()
                Case "Fee Tools"
                    If FeeTools Is Nothing Then
                        If FeeTools Is Nothing Then FeeTools = New PASPFeeTools
                    Else
                        FeeTools.Dispose()
                        FeeTools = New PASPFeeTools
                    End If
                Case "DMU Only Tool" '25
                    If (UserGCode = "1" Or UserGCode = "345") Then
                        If DMUOnly Is Nothing Then
                            If DMUOnly Is Nothing Then DMUOnly = New DMUTool
                        Else
                            DMUOnly.Dispose()
                            DMUOnly = New DMUTool
                        End If
                        DMUOnly.Show()
                    Else
                        MsgBox("ACCESS DENIED")
                    End If
                Case "Smoke School" '26 
                    With SmokeSchool
                        .Show()
                        .Activate()
                    End With
                Case "AFS Tools"
                    OpenSingleForm(DMUDeveloperTools, closeFirst:=True)
                Case "DMU Staff Tools"
                    If StaffTools Is Nothing Then
                        If StaffTools Is Nothing Then StaffTools = New DMUStaffTools
                    Else
                        StaffTools.Dispose()
                        StaffTools = New DMUStaffTools
                    End If
                    StaffTools.Show()
                Case "Title V Tools"
                    If TitleVTools Is Nothing Then
                        If TitleVTools Is Nothing Then TitleVTools = New DMUTitleVTools
                    Else
                        TitleVTools.Dispose()
                        TitleVTools = New DMUTitleVTools
                    End If
                    TitleVTools.Show()
                Case "AFS Compare Tool"
                    If AFSCompare Is Nothing Then
                        If AFSCompare Is Nothing Then AFSCompare = New IAIPAFSCompare
                    Else
                        AFSCompare.Dispose()
                        AFSCompare = New IAIPAFSCompare
                    End If
                    AFSCompare.Show()
                Case "Look Up Tables"
                    If LookUpTables Is Nothing Then
                        If LookUpTables Is Nothing Then LookUpTables = New IAIPLookUpTables
                    Else
                        LookUpTables.Dispose()
                        LookUpTables = New IAIPLookUpTables
                    End If
                    LookUpTables.Show()
                Case "Compliance Admin"
                    If SSCPAdmin Is Nothing Then
                        If SSCPAdmin Is Nothing Then SSCPAdmin = New SSCPAdministrator
                    Else
                        SSCPAdmin.Dispose()
                        SSCPAdmin = New SSCPAdministrator
                    End If
                    SSCPAdmin.Show()
                Case "Registration Tool"
                    With MASPRegistrationTool
                        .Show()
                        .Activate()
                    End With
                Case "EIS Log"
                    If EISLog Is Nothing Then
                        If EISLog Is Nothing Then EISLog = New IAIP_EIS_Log
                    Else
                        EISLog.Dispose()
                        EISLog = New IAIP_EIS_Log
                    End If
                    EISLog.Show()

                Case "Enforcement Documents"
                    OpenSingleForm(SscpDocuments)

                Case Else
                    MsgBox(Source.ToString, MsgBoxStyle.Information, "IAIP Navigation")
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Nav buttons background worker (bgrLoadButtons)"

    Private Sub bgrLoadButtons_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgrLoadButtons.DoWork
        Try
            Dim navTemp As Boolean
            Dim accountTemp As String = Permissions
            Dim accessTemp As String = ""
            Dim accountAccess As String = ""

            If accountTemp <> "" Then

                Do While accountTemp <> ""

                    accountAccess = GetFormAccessForAccountCode(Mid(accountTemp, 2, (accountTemp.IndexOf(")") - 1)))

                    If accountAccess <> "" Then
                        Do While accountAccess <> ""
                            navTemp = False
                            For j = 0 To 4
                                If AccountArray(j, 0) = Mid(accountAccess, 2, (accountAccess.IndexOf("-") - 1)) Then
                                    navTemp = True
                                End If
                            Next
                            If navTemp = False Then
                                accessTemp = Mid(accountAccess, 2, (accountAccess.IndexOf("-") - 1))
                                AccountArray(Mid(accountAccess, 2, (accountAccess.IndexOf("-") - 1)), 0) = Mid(accountAccess, 2, (accountAccess.IndexOf("-") - 1))

                                If AccountArray(accessTemp, 1) = "1" Then
                                    AccountArray(Mid(accountAccess, 2, (accountAccess.IndexOf("-") - 1)), 1) = "1"
                                Else
                                    AccountArray(Mid(accountAccess, 2, (accountAccess.IndexOf("-") - 1)), 1) = Mid(accountAccess, (accountAccess.IndexOf("-") + 2), 1)
                                End If
                                If AccountArray(accessTemp, 2) = "1" Then
                                    AccountArray(Mid(accountAccess, 2, (accountAccess.IndexOf("-") - 1)), 2) = "1"
                                Else
                                    AccountArray(Mid(accountAccess, 2, (accountAccess.IndexOf("-") - 1)), 2) = Mid(accountAccess, (accountAccess.IndexOf("-") + 4), 1)
                                End If
                                If AccountArray(accessTemp, 3) = "1" Then
                                    AccountArray(Mid(accountAccess, 2, (accountAccess.IndexOf("-") - 1)), 3) = "1"
                                Else
                                    AccountArray(Mid(accountAccess, 2, (accountAccess.IndexOf("-") - 1)), 3) = Mid(accountAccess, (accountAccess.IndexOf("-") + 6), 1)
                                End If
                                If AccountArray(accessTemp, 4) = "1" Then
                                    AccountArray(Mid(accountAccess, 2, (accountAccess.IndexOf("-") - 1)), 4) = "1"
                                Else
                                    AccountArray(Mid(accountAccess, 2, (accountAccess.IndexOf("-") - 1)), 4) = Mid(accountAccess, (accountAccess.IndexOf("-") + 8), 1)
                                End If
                            End If
                            accountAccess = Replace(accountAccess, (Mid(accountAccess, accountAccess.IndexOf("(") + 1, accountAccess.IndexOf(")") + 1)), "")
                        Loop
                    End If
                    accountTemp = Replace(accountTemp, ("(" & Mid(accountTemp, 2, (accountTemp.IndexOf(")") - 1)) & ")"), "")
                Loop
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub bgrLoadButtons_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgrLoadButtons.RunWorkerCompleted
        Try
            SetContextSelectorSubView()
            CreateButtons()
            ArrangeButtons()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SetContextSelectorSubView()
        rdbStaffView.Checked = True

        If (AccountArray(24, 3) = "1" And AccountArray(12, 1) = "1" And AccountArray(12, 2) = "0" And AccountArray(3, 4) = "0") Or _
        (AccountArray(17, 1) = "0" And AccountArray(17, 2) = "1" And AccountArray(17, 3) = "1") Or _
        (AccountArray(22, 4) = "0" And AccountArray(22, 3) = "1") Then
            rdbUCView.Checked = True
        End If

        If AccountArray(129, 3) = "1" Or _
        (AccountArray(24, 3) = "1" And AccountArray(3, 4) = "1" And AccountArray(12, 1) = "1" And AccountArray(12, 2) = "0") Or _
        (AccountArray(17, 3) = "1" And AccountArray(17, 4) = "0") Or _
        (AccountArray(22, 4) = "1" And AccountArray(22, 3) = "0") Then
            rdbPMView.Checked = True
        End If
    End Sub

    Private Sub CreateButtons()
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
        'If AccountArray(134, 0) Is Nothing Then
        'Else
        '    If AccountArray(134, 0) = "134" Then
        '        If AccountArray(134, 1) = "1" Or AccountArray(134, 2) = "1" Or AccountArray(134, 3) = "1" Or AccountArray(134, 4) = "1" Then
        '            btnNav34.Text = "Fees Audit Tool"
        '            btnNav34.Visible = True
        '        End If
        '    End If
        'End If
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
        If (Permissions.Contains("(19)") OrElse Permissions.Contains("(20)") _
        OrElse Permissions.Contains("(21)") OrElse Permissions.Contains("(23)") _
        OrElse Permissions.Contains("(25)") OrElse Permissions.Contains("(118)") _
        OrElse Permissions.Contains("(114)")) Then
            btnNav40.Text = "Enforcement Documents"
            btnNav40.Visible = True
        End If
    End Sub

    Private Sub ArrangeButtons()

        Dim i As Integer = 0

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

    End Sub

#End Region

#Region "Main Menu click events"

    Private Sub mmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiExit.Click
        Me.Close()
    End Sub

    Private Sub mmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiExport.Click
        If dgvWorkViewer.RowCount > 0 Then dgvWorkViewer.ExportToExcel(Me)
    End Sub

    Private Sub mmiResetForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiResetForm.Click
        ResetAllFormSettings()
        Me.Location = New Point(0, 0)
        Me.Size = New Size(808, 460)
    End Sub

    Private Sub mmiAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiAbout.Click
        IaipAbout.ShowDialog()
    End Sub

    Private Sub mmiOnlineHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOnlineHelp.Click
        OpenDocumentationUrl(Me)
    End Sub

#End Region

    Private Sub mmiPing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPing.Click
        DB.PingDBConnection(CurrentConnection)
    End Sub

End Class
