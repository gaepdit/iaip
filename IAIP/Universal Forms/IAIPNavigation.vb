Imports System.IO
Imports System.Collections.Generic
Imports Iaip.DAL.NavigationScreenData

Public Class IAIPNavigation

#Region " Local variables and properties "

#Region " WorkViewer properties "

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

#End Region

#Region " Form events "

    Private Sub IAIPNavigation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        monitor.TrackFeature("Main." & Me.Name)

        ' UI adjustments
        AssociateQuickNavButtons()
        SetContextSelectorSubView()
        BuildListChangerCombo()
        EnableSbeapTools()
        LoadStatusBar()
        EnableConnectionEnvironmentOptions()

        ' Start various Timers
        AppTimers.StartAppTimers()

    End Sub

    Private Sub IAIPNavigation_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        monitor.TrackFeatureStop("Startup.LoggingIn")

        ' Start the bgrUserPermissions background worker
        BuildAccountPermissions()
    End Sub

    Private Sub IAIPNavigation_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        StartupShutdown.CloseIaip()
    End Sub

#End Region

#Region " Page Load procedures "

    Private Sub EnableSbeapTools()
        If UserHasPermission(New String() {"(142)", "(143)", "(118)"}) Then
            cboWorkViewerContext.Items.Add("SBEAP Cases")
            EnableAndShow(SbeapQuickAccessPanel)
        End If
    End Sub

    Private Sub LoadStatusBar()
        pnlName.Text = UserName
        pnlDate.Text = OracleDate

        Dim id As Integer
        If Integer.TryParse(UserProgram, id) Then
            pnlProgram.Text = DAL.GetProgramDescription(id)
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

        If CurrentServerEnvironment = DB.ServerEnvironment.DEV Then
            pnlDbEnv.Text = "TESTING ENVIRONMENT"
            pnlDbEnv.BackColor = Color.Tomato
            pnlDbEnv.Visible = True
            EnableAndShow(mmiTesting)
        Else
            pnlDbEnv.Text = "PRD"
            pnlDbEnv.Visible = False
            DisableAndHide(mmiTesting)
        End If

#If DEBUG Then
        lblTitle.Text = "IAIP Navigation Screen � " & CurrentServerEnvironment.ToString
#End If

    End Sub

#End Region

#Region " Quick Access Tool buttons and events "

    Private Sub AssociateQuickNavButtons()
        txtOpenApplication.Tag = btnOpenApplication
        txtOpenEnforcement.Tag = btnOpenEnforcement
        txtOpenFacilitySummary.Tag = btnOpenFacilitySummary
        txtOpenSbeapCaseLog.Tag = btnOpenSbeapCaseLog
        txtOpenSbeapClient.Tag = btnOpenSbeapClient
        txtOpenSscpItem.Tag = btnOpenSscpItem
        txtOpenTestLog.Tag = btnOpenTestLog
        txtOpenTestReport.Tag = btnOpenTestReport
    End Sub

    Private Sub QuickAccessButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles btnOpenFacilitySummary.Click, btnOpenTestReport.Click, btnOpenTestLog.Click, btnOpenSscpItem.Click, btnOpenSbeapClient.Click, btnOpenSbeapCaseLog.Click, btnOpenEnforcement.Click, btnOpenApplication.Click
        Dim thisButton As Button = CType(sender, Button)
        monitor.TrackFeature("QuickAccess." & thisButton.Name)
        monitor.TrackFeature("NavScreen.QuickAccess")
        Select Case thisButton.Name
            Case btnOpenApplication.Name
                OpenApplication()
            Case btnOpenEnforcement.Name
                OpenEnforcement()
            Case btnOpenFacilitySummary.Name
                OpenFacilitySummary()
            Case btnOpenSbeapCaseLog.Name
                OpenSbeapCaseLog()
            Case btnOpenSbeapClient.Name
                OpenSbeapClient()
            Case btnOpenSscpItem.Name
                OpenSscpItem()
            Case btnOpenTestLog.Name
                OpenTestLog()
            Case btnOpenTestReport.Name
                OpenTestReport()
        End Select
    End Sub

    Private Sub QuickAccessTextbox_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtOpenApplication.Enter, txtOpenEnforcement.Enter, txtOpenFacilitySummary.Enter, txtOpenSbeapCaseLog.Enter, _
    txtOpenSbeapClient.Enter, txtOpenSscpItem.Enter, txtOpenTestLog.Enter, txtOpenTestReport.Enter
        Dim thisButton As Button = CType(CType(sender, TextBox).Tag, Button)
        Me.AcceptButton = thisButton
        thisButton.FlatStyle = FlatStyle.Standard
        thisButton.ForeColor = SystemColors.ControlText
    End Sub

    Private Sub QuickAccessTextbox_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtOpenApplication.Leave, txtOpenEnforcement.Leave, txtOpenFacilitySummary.Leave, txtOpenSbeapCaseLog.Leave, _
    txtOpenSbeapClient.Leave, txtOpenSscpItem.Leave, txtOpenTestLog.Leave, txtOpenTestReport.Leave
        Dim thisButton As Button = CType(CType(sender, TextBox).Tag, Button)
        Me.AcceptButton = Nothing
        If Not thisButton.Tag Then
            thisButton.FlatStyle = FlatStyle.Flat
            thisButton.ForeColor = SystemColors.GrayText
        End If
    End Sub

    Private Sub QuickAccessTextbox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtOpenApplication.TextChanged, txtOpenEnforcement.TextChanged, txtOpenFacilitySummary.TextChanged, txtOpenSbeapCaseLog.TextChanged, _
    txtOpenSbeapClient.TextChanged, txtOpenSscpItem.TextChanged, txtOpenTestLog.TextChanged, txtOpenTestReport.TextChanged
        Dim thisTextbox As TextBox = CType(sender, TextBox)
        Dim thisButton As Button = CType(thisTextbox.Tag, Button)
        If thisTextbox.TextLength > 0 Then
            thisButton.FlatStyle = FlatStyle.Standard
            thisButton.ForeColor = SystemColors.ControlText
            thisButton.Tag = True
        Else
            thisButton.Tag = Nothing
            If Not thisTextbox.Focused And Not thisButton.Focused Then
                thisButton.FlatStyle = FlatStyle.Flat
                thisButton.ForeColor = SystemColors.GrayText
            End If
        End If
    End Sub

    Private Sub QuickAccessButton_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles btnOpenApplication.Enter, btnOpenEnforcement.Enter, btnOpenFacilitySummary.Enter, btnOpenSbeapCaseLog.Enter, _
    btnOpenSbeapClient.Enter, btnOpenSscpItem.Enter, btnOpenTestLog.Enter, btnOpenTestReport.Enter
        Dim thisButton As Button = CType(sender, Button)
        If thisButton.Tag Then
            thisButton.FlatStyle = FlatStyle.Standard
            thisButton.ForeColor = SystemColors.ControlText
        End If
    End Sub

    Private Sub QuickAccessButton_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles btnOpenApplication.Leave, btnOpenEnforcement.Leave, btnOpenFacilitySummary.Leave, btnOpenSbeapCaseLog.Leave, _
    btnOpenSbeapClient.Leave, btnOpenSscpItem.Leave, btnOpenTestLog.Leave, btnOpenTestReport.Leave
        Dim thisButton As Button = CType(sender, Button)
        If Not Me.AcceptButton Is thisButton And Not thisButton.Tag Then
            thisButton.FlatStyle = FlatStyle.Flat
            thisButton.ForeColor = SystemColors.GrayText
        End If
    End Sub

#End Region

#Region " Quick Access Tool procedures "

    Private Sub OpenApplication()
        Try
            Dim id As String = txtOpenApplication.Text
            If id = "" Then Exit Sub

            If DAL.SSPP.ApplicationExists(id) Then
                If PermitTrackingLog IsNot Nothing AndAlso Not PermitTrackingLog.IsDisposed Then
                    PermitTrackingLog.Dispose()
                End If

                PermitTrackingLog = New SSPPApplicationTrackingLog
                PermitTrackingLog.Show()
                PermitTrackingLog.txtApplicationNumber.Text = txtOpenApplication.Text
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
            Dim id As String = txtOpenTestReport.Text
            If id = "" Then Exit Sub

            If DAL.ISMP.StackTestExists(id) Then
                If UserProgram = "3" Then
                    OpenMultiForm("ISMPTestReports", id)
                Else
                    If DAL.ISMP.StackTestIsClosedOut(id) Then
                        If PrintOut IsNot Nothing AndAlso Not PrintOut.IsDisposed Then
                            PrintOut.Dispose()
                        End If
                        PrintOut = New IAIPPrintOut
                        PrintOut.txtReferenceNumber.Text = txtOpenTestReport.Text
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
            Dim id As String = txtOpenEnforcement.Text
            If id = "" Then Exit Sub
            If DAL.SSCP.EnforcementExists(id) Then
                OpenMultiForm("SscpEnforcement", id)
            Else
                MsgBox("Enforcement number is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub OpenSscpItem()
        Try
            Dim id As String = txtOpenSscpItem.Text
            If id = "" Then Exit Sub

            If DAL.SSCP.WorkItemExists(id) Then
                Dim refNum As String = ""
                If DAL.SSCP.WorkItemIsAStackTest(id, refNum) Then
                    OpenMultiForm("ISMPTestReports", refNum)
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
        If txtOpenFacilitySummary.TextLength = 0 Then
            OpenSingleForm(IAIPFacilitySummary)
            Exit Sub
        End If

        If Not DAL.FacilityInfo.AirsNumberExists(txtOpenFacilitySummary.Text) Then
            MsgBox("AIRS Number is not in the system.", MsgBoxStyle.Information, "Navigation Screen")
            Exit Sub
        End If

        Dim parameters As New Generic.Dictionary(Of String, String)
        parameters("airsnumber") = txtOpenFacilitySummary.Text
        OpenSingleForm(IAIPFacilitySummary, parameters:=parameters, closeFirst:=True)
    End Sub

    Private Sub OpenTestLog()
        Try
            Dim id As String = txtOpenTestLog.Text
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

    Private Sub OpenSbeapClient()
        Try
            Dim id As String = txtOpenSbeapClient.Text
            If id = "" Then Exit Sub

            If DAL.SBEAP.ClientExists(id) Then
                If ClientSummary IsNot Nothing AndAlso Not ClientSummary.IsDisposed Then
                    ClientSummary.Dispose()
                End If

                ClientSummary = New SBEAPClientSummary
                ClientSummary.Show()
                ClientSummary.txtClientID.Text = id
                ClientSummary.LoadClientData()
            Else
                MsgBox("Customer ID is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub OpenSbeapCaseLog()
        Try
            Dim id As String = txtOpenSbeapCaseLog.Text
            If id = "" Then Exit Sub

            If DAL.SBEAP.CaseExists(id) Then

                If CaseWork IsNot Nothing AndAlso Not CaseWork.IsDisposed Then
                    CaseWork.Dispose()
                End If

                CaseWork = New SBEAPCaseWork
                CaseWork.Show()
                CaseWork.txtCaseID.Text = id
                CaseWork.LoadCaseLogData()
            Else
                MsgBox("Case number is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ClearQuickAccessTool()
        txtOpenFacilitySummary.Clear()
        txtOpenEnforcement.Clear()
        txtOpenApplication.Clear()
        txtOpenTestReport.Clear()
        txtOpenSscpItem.Clear()
        txtOpenTestLog.Clear()
        txtOpenSbeapClient.Clear()
        txtOpenSbeapCaseLog.Clear()
    End Sub

#End Region

#Region " WorkViewer context selector "

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
                                    ElseIf AccountFormAccess(17, 2) = "1" Then  'Unit Manager
                                        CurrentWorkViewerContext = WorkViewerType.ISMP_UC
                                        CurrentWorkViewerContextParameter = UserUnit
                                    Else
                                        CurrentWorkViewerContext = WorkViewerType.ISMP_Staff
                                        ' TODO (Doug): When a better user object is set up, change this (pnl2.Text)
                                        ' to something more appropriate
                                        CurrentWorkViewerContextParameter = pnlName.Text
                                    End If

                                Case "4" 'SSCP
                                    If UserUnit = "---" Then 'Program Manager
                                        CurrentWorkViewerContext = WorkViewerType.SSCP_PM
                                    ElseIf UserHasPermission("(143)") Then ' SBEAP Manager
                                        CurrentWorkViewerContext = WorkViewerType.SBEAP_Program
                                    ElseIf UserHasPermission("(142)") Then ' SBEAP staff
                                        CurrentWorkViewerContext = WorkViewerType.SBEAP_Staff
                                        CurrentWorkViewerContextParameter = UserGCode
                                    ElseIf AccountFormAccess(22, 3) = "1" Then 'Unit Manager
                                        CurrentWorkViewerContext = WorkViewerType.SSCP_UC
                                        CurrentWorkViewerContextParameter = UserUnit
                                    ElseIf AccountFormAccess(10, 3) = "1" Then 'District Liaison
                                        CurrentWorkViewerContext = WorkViewerType.SSCP_DistrictLiaison
                                    Else
                                        CurrentWorkViewerContext = WorkViewerType.SSCP_Staff
                                        CurrentWorkViewerContextParameter = UserGCode
                                    End If

                                Case "5" 'SSPP
                                    If AccountFormAccess(3, 3) = "1" And UserUnit = "---" Then  'Program Manager
                                        CurrentWorkViewerContext = WorkViewerType.SSPP_PM
                                    ElseIf AccountFormAccess(24, 3) = "1" Then 'Unit Manager
                                        CurrentWorkViewerContext = WorkViewerType.SSPP_UC
                                        CurrentWorkViewerContextParameter = UserUnit
                                    ElseIf AccountFormAccess(9, 3) = "1" Then 'Administrative 2
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
                            ElseIf AccountFormAccess(22, 3) = "1" Then 'Unit Manager
                                CurrentWorkViewerContext = WorkViewerType.ProgCoord_UC
                                CurrentWorkViewerContextParameter = UserUnit
                            ElseIf AccountFormAccess(10, 3) = "1" Then 'District Liaison
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
                        CurrentWorkViewerContextParameter = pnlName.Text
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

                Case "SBEAP Cases"
                    If rdbUCView.Checked Or rdbPMView.Checked Then
                        CurrentWorkViewerContext = WorkViewerType.SBEAP_Program
                    Else
                        CurrentWorkViewerContext = WorkViewerType.SBEAP_Staff
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

#Region " WorkViewer context selector events "

    Private Sub btnChangeWorkViewerContext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeWorkViewerContext.Click
        monitor.TrackFeature("NavScreen.ChangeWorkViewer")
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
            Case "SBEAP Cases"
                pnlContextSubView.Visible = True
        End Select
    End Sub

    Private Sub SetContextSelectorSubView()
        rdbStaffView.Checked = True

        If UserHasPermission(New String() {"(114)", "(115)", "(121)", "(128)", "(141)", "(63)"}) Then
            rdbUCView.Checked = True
        End If

        If UserHasPermission(New String() {"(2)", "(19)", "(28)", "(45)", "(57)", "(143)"}) Then
            rdbPMView.Checked = True
        End If
    End Sub

#End Region

#Region " WorkViewer formatters "

    Private Sub FormatWorkViewer()
        If dgvWorkViewer.Visible = True Then

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

                Case WorkViewerType.SBEAP_Program, WorkViewerType.SBEAP_Staff
                    FormatWorkViewerForSbeapCases()

            End Select

            dgvWorkViewer.SanelyResizeColumns()
            dgvWorkViewer.MakeColumnsLookLikeLinks(0)
            'Try
            '    dgvWorkViewer.Columns("AIRSNumber").DefaultCellStyle.Format = "000-00000"
            'Catch e As Exception
            'End Try

        End If
    End Sub

    Private Sub FormatWorkViewerForSbeapCases()
        dgvWorkViewer.Columns("numCaseID").HeaderText = "Case ID"
        dgvWorkViewer.Columns("numCaseID").DisplayIndex = 0
        dgvWorkViewer.Columns("ClientID").HeaderText = "Customer ID"
        dgvWorkViewer.Columns("ClientID").DisplayIndex = 1
        dgvWorkViewer.Columns("strCompanyName").HeaderText = "Customer Name"
        dgvWorkViewer.Columns("strCompanyName").DisplayIndex = 2
        dgvWorkViewer.Columns("CaseOpened").HeaderText = "Date Case Opened"
        dgvWorkViewer.Columns("CaseOpened").DisplayIndex = 3
        dgvWorkViewer.Columns("CaseOpened").DefaultCellStyle.Format = "dd-MMM-yyyy"
        dgvWorkViewer.Columns("StaffResponsible").HeaderText = "Staff Responsible"
        dgvWorkViewer.Columns("StaffResponsible").DisplayIndex = 4
        dgvWorkViewer.Columns("strCaseSummary").HeaderText = "Case Description"
        dgvWorkViewer.Columns("strCaseSummary").DisplayIndex = 5
        dgvWorkViewer.Columns("strCaseSummary").Width = 200
        dgvWorkViewer.Columns("numStaffResponsible").HeaderText = "Staff Responsible"
        dgvWorkViewer.Columns("numStaffResponsible").DisplayIndex = 6
        dgvWorkViewer.Columns("numStaffResponsible").Visible = False
        dgvWorkViewer.Columns("caseClosed").HeaderText = "Date Closed"
        dgvWorkViewer.Columns("caseClosed").DisplayIndex = 7
        dgvWorkViewer.Columns("caseClosed").DefaultCellStyle.Format = "dd-MMM-yyyy"
        dgvWorkViewer.Columns("caseClosed").Visible = False
        dgvWorkViewer.Columns("LastUpDated").HeaderText = "Last Updated"
        dgvWorkViewer.Columns("LastUpDated").DisplayIndex = 8
        dgvWorkViewer.Columns("LastUpDated").DefaultCellStyle.Format = "dd-MMM-yyyy"
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

    Private Sub dgvWorkViewer_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvWorkViewer.CellFormatting
        If e IsNot Nothing Then
            Try
                If dgvWorkViewer.Columns(e.ColumnIndex).HeaderText = "AIRS #" Then
                    Dim text As String = e.Value.ToString
                    e.Value = String.Format("{0}-{1}", text.Substring(0, 3), text.Substring(3))
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

#End Region

#Region " WorkViewer background worker (bgrLoadWorkViewer) "

    Private Sub LoadWorkViewerData()
        dgvWorkViewer.Visible = False
        lblMessageLabel.Visible = True
        lblMessageLabel.Text = "Loading data�"
        pnlCurrentList.Enabled = False
        btnChangeWorkViewerContext.Text = "Loading�"
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

#Region " WorkViewer (dgvWorkViewer) events "

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

    Private Sub dgvWorkViewer_CellMouseEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
    Handles dgvWorkViewer.CellMouseEnter
        'Console.WriteLine("CellMouseEnter: " & e.ColumnIndex.ToString & ", " & e.RowIndex.ToString)
        ' Change cursor and text color when hovering over first column (treats text like a hyperlink)
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvWorkViewer.RowCount AndAlso e.ColumnIndex = 0 Then
            dgvWorkViewer.MakeCellLookLikeHoveredLink(e.RowIndex, e.ColumnIndex)
        End If
    End Sub

    Private Sub dgvWorkViewer_CellMouseLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
    Handles dgvWorkViewer.CellMouseLeave
        'Console.WriteLine("CellMouseLeave: " & e.ColumnIndex.ToString & ", " & e.RowIndex.ToString)
        ' Reset cursor and text color when mouse leaves (un-hovers) a cell
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvWorkViewer.RowCount AndAlso e.ColumnIndex = 0 Then
            dgvWorkViewer.MakeCellNotLookLikeHoveredLink(e.RowIndex, e.ColumnIndex)
        End If
    End Sub

    Private Sub dgvWorkViewer_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
    Handles dgvWorkViewer.CellEnter
        'Console.WriteLine("CellEnter: " & e.ColumnIndex.ToString & ", " & e.RowIndex.ToString)
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvWorkViewer.RowCount Then
            SelectItemNumbers(e.RowIndex)
        End If
    End Sub

    Private Sub dgvWorkViewer_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
    Handles dgvWorkViewer.CellClick
        'Console.WriteLine("CellClick: " & e.ColumnIndex.ToString & ", " & e.RowIndex.ToString)
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvWorkViewer.RowCount AndAlso e.ColumnIndex = 0 Then
            OpenSelectedItem()
        End If
    End Sub

    Private Sub dgvWorkViewer_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
    Handles dgvWorkViewer.CellDoubleClick
        'Console.WriteLine("CellDoubleClick: " & e.ColumnIndex.ToString & ", " & e.RowIndex.ToString)
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvWorkViewer.RowCount AndAlso e.ColumnIndex <> 0 Then
            OpenSelectedItem()
        End If
    End Sub

    Private Sub OpenSelectedItem()
        monitor.TrackFeature("NavScreen.WorkViewerLink")
        monitor.TrackFeature("WorkViewerLink." & dgvWorkViewer.Columns(0).HeaderText.Replace(" ", "_"))
        Select Case dgvWorkViewer.Columns(0).HeaderText
            Case "Case ID" ' SBEAP cases
                OpenSbeapCaseLog()
            Case "AIRS #" ' Compliance facilities assigned; delinquent FCEs; facility subparts
                OpenFacilitySummary()
            Case "Tracking #" ' Compliance work
                OpenSscpItem()
            Case "Enforcement #" 'Enforcement
                OpenEnforcement()
            Case "Reference #" ' ISMP Test Reports
                OpenTestReport()
            Case "Test Log #" ' ISMP Test Notifications
                OpenTestLog()
            Case "App #" ' Permit applications
                OpenApplication()
        End Select
    End Sub

    Private Sub SelectItemNumbers(ByVal row As Integer)
        Select Case dgvWorkViewer.Columns(0).HeaderText
            Case "Case ID" ' SBEAP cases
                txtOpenSbeapCaseLog.Text = dgvWorkViewer(0, row).FormattedValue
                txtOpenSbeapClient.Text = dgvWorkViewer(1, row).FormattedValue
            Case "AIRS #" ' Compliance facilities assigned; delinquent FCEs; facility subparts
                txtOpenFacilitySummary.Text = dgvWorkViewer(0, row).FormattedValue
            Case "Tracking #" ' Compliance work
                txtOpenSscpItem.Text = dgvWorkViewer(0, row).FormattedValue
                txtOpenFacilitySummary.Text = dgvWorkViewer(1, row).FormattedValue
            Case "Enforcement #" ' Enforcement
                txtOpenEnforcement.Text = dgvWorkViewer(0, row).FormattedValue
                txtOpenFacilitySummary.Text = dgvWorkViewer(1, row).FormattedValue
            Case "Reference #" ' ISMP Test Reports
                txtOpenTestReport.Text = dgvWorkViewer(0, row).FormattedValue
                txtOpenFacilitySummary.Text = dgvWorkViewer(1, row).FormattedValue
            Case "Test Log #" ' ISMP Test Notifications
                txtOpenTestLog.Text = dgvWorkViewer(0, row).FormattedValue
                txtOpenTestReport.Text = dgvWorkViewer(1, row).FormattedValue
            Case "App #" ' Permit applications
                txtOpenApplication.Text = dgvWorkViewer(0, row).FormattedValue
                txtOpenFacilitySummary.Text = dgvWorkViewer(1, row).FormattedValue
        End Select
    End Sub
#End Region

#Region " User account permissions background worker (bgrUserPermissions) "

    Private Sub BuildAccountPermissions()
        If bgrUserPermissions.IsBusy Then
            bgrUserPermissions.CancelAsync()
        Else
            bgrUserPermissions.RunWorkerAsync()
        End If
    End Sub


    Private Sub bgrUserPermissions_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgrUserPermissions.DoWork
        Try
            Dim AccountFormAccessLookup As DataTable = GetAccountFormAccessLookup()
            AccountFormAccessLookup.PrimaryKey = New DataColumn() {AccountFormAccessLookup.Columns(0)}

            If UserAccounts.Length > 0 Then
                Dim userAccountArray() As String = UserAccounts.Split(New Char() {"(", ")"}, StringSplitOptions.RemoveEmptyEntries)

                For Each account As String In userAccountArray
                    Dim accountFormAccessString As String = AccountFormAccessLookup.Rows.Find(account)(1).ToString

                    If accountFormAccessString.Length > 0 Then
                        Dim formAccessArray() As String = accountFormAccessString.Split(New Char() {"(", ")"}, StringSplitOptions.RemoveEmptyEntries)

                        For Each formAccessString As String In formAccessArray
                            Dim formAccessSplit() As String = formAccessString.Split(New Char() {"-", ","})
                            Dim formNumber As String = formAccessSplit(0)
                            AccountFormAccess(formNumber, 0) = formNumber
                            For i As Integer = 1 To 4
                                AccountFormAccess(formNumber, i) = Convert.ToInt32(AccountFormAccess(formNumber, i)) Or Convert.ToInt32(formAccessSplit(i))
                            Next
                        Next
                    End If
                Next
            End If

            'Dim sb As New System.Text.StringBuilder
            'For Each a As String In AccountFormAccess
            '    sb.Append(a)
            'Next
            'Console.WriteLine(sb.ToString)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub bgrUserPermissions_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgrUserPermissions.RunWorkerCompleted
        Try
            btnChangeWorkViewerContext.Enabled = True
            LoadWorkViewerData()

            CreateNavButtonCategoriesList()
            CreateNavButtonsList()
            CreateNavButtons()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " Nav Button creation "

#Region " Containers "

    Private Structure NavButton
        Public Sub New(ByVal buttonText As String, ByVal formName As String)
            Me.ButtonText = buttonText
            Me.FormName = formName
        End Sub
        Public ButtonText As String
        Public FormName As String
    End Structure

    Private Structure NavButtonCategory
        Public Sub New(ByVal category As NavButtonCategories, ByVal name As String, Optional ByVal shortname As String = Nothing)
            Me.Category = category
            Me.Name = name
            Me.ShortName = If(shortname, category.ToString)
        End Sub
        Public Category As NavButtonCategories
        Public Name As String
        Public ShortName As String
    End Structure

    Private AllTheNavButtons As New Dictionary(Of NavButtonCategories, List(Of NavButton))

    Private AllTheNavButtonCategories As New List(Of NavButtonCategory)

#End Region

#Region " Implementation "

    Private Function UserHasPermission(ByVal permissionCode As String) As Boolean
        If UserAccounts.Contains(permissionCode) Then Return True
        Return False
    End Function

    Private Function UserHasPermission(ByVal permissionsAllowed As String()) As Boolean
        For Each permissionCode As String In permissionsAllowed
            If UserHasPermission(permissionCode) Then Return True
        Next
        Return False
    End Function

    Private Function AccountHasAccessToForm(ByVal index As Int32) As Boolean
        Return (AccountFormAccess(index, 0) IsNot Nothing _
                AndAlso AccountFormAccess(index, 0) = index.ToString _
                AndAlso (AccountFormAccess(index, 1) = "1" Or AccountFormAccess(index, 2) = "1" _
                         Or AccountFormAccess(index, 3) = "1" Or AccountFormAccess(index, 4) = "1") _
                         )
    End Function

    Private Sub AddNavButton(ByVal buttonText As String, ByVal formName As String, ByVal category As NavButtonCategories)
        If Not AllTheNavButtonCategories.Exists(Function(x) x.Category = category) Then
            AllTheNavButtonCategories.Add(New NavButtonCategory(category, category.ToString))
        End If

        If AllTheNavButtons.ContainsKey(category) Then
            AllTheNavButtons(category).Add(New NavButton(buttonText, formName))
        Else
            Dim navButtonList As New List(Of NavButton)
            navButtonList.Add(New NavButton(buttonText, formName))
            AllTheNavButtons.Add(category, navButtonList)
        End If

    End Sub

    Private Sub AddNavButtonIfAccountHasFormAccess(ByVal index As Int32, _
                                                   ByVal buttonText As String, ByVal formName As String, _
                                                   ByVal category As NavButtonCategories)
        If AccountHasAccessToForm(index) Then
            AddNavButton(buttonText, formName, category)
        End If
    End Sub

    Private Sub AddNavButtonIfUserHasPermission(ByVal permissionsAllowed As String(), _
                                                ByVal buttonText As String, ByVal formName As String, _
                                                ByVal category As NavButtonCategories)
        If UserHasPermission(permissionsAllowed) Then
            AddNavButton(buttonText, formName, category)
        End If
    End Sub

    Private Sub AddNavButtonCategory(ByVal category As NavButtonCategories, ByVal name As String, Optional ByVal shortname As String = Nothing)
        If CurrentUser.Staff.ProgramName = name OrElse CurrentUser.Staff.UnitName = name Then
            AllTheNavButtonCategories.Insert(0, New NavButtonCategory(category, name, shortname))
        Else
            AllTheNavButtonCategories.Add(New NavButtonCategory(category, name, shortname))
        End If
    End Sub

    Private Sub NavButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nb As NavButton = CType(CType(sender, Button).Tag, NavButton)
        monitor.TrackFeature("NavScreen.NavButton")
        monitor.TrackFeature("NavButton." & nb.FormName)
        OpenSingleForm(nb.FormName)
    End Sub

    Private Sub CreateNavButtons()
        Dim margin As Integer = 7
        Dim buttonHeight As Integer = 38
        Dim buttonWidth As Integer = 90
        Dim currentYPosition As Integer = margin

        For Each newCategory As NavButtonCategory In AllTheNavButtonCategories
            If AllTheNavButtons.ContainsKey(newCategory.Category) Then

                Dim categoryHeader As New Label
                With categoryHeader
                    .Text = newCategory.ShortName
                    .TextAlign = ContentAlignment.BottomCenter
                    .Width = buttonWidth
                    .UseMnemonic = False
                    .ForeColor = SystemColors.InactiveCaptionText
                End With
                flpNavButtons.Controls.Add(categoryHeader)

                For Each newNavButton As NavButton In AllTheNavButtons(newCategory.Category)
                    Dim newButton As New Button
                    With newButton
                        .Text = newNavButton.ButtonText
                        .Size = New Size(buttonWidth, buttonHeight)
                        .Tag = newNavButton
                    End With
                    flpNavButtons.Controls.Add(newButton)
                    AddHandler newButton.Click, AddressOf NavButton_Click
                Next

            End If
        Next
    End Sub

#End Region

#Region " Specifics "

    Private Enum NavButtonCategories
        General
        ISMP
        SSPP
        SSCP
        PASP
        DMU
        MASP
        EIS
        SBEAP
    End Enum

    Private Sub CreateNavButtonCategoriesList()
        AddNavButtonCategory(NavButtonCategories.General, "General")
        AddNavButtonCategory(NavButtonCategories.ISMP, "Industrial Source Monitoring Program")
        AddNavButtonCategory(NavButtonCategories.SSPP, "Stationary Source Permitting Program")
        AddNavButtonCategory(NavButtonCategories.SSCP, "Stationary Source Compliance Program")
        AddNavButtonCategory(NavButtonCategories.PASP, "Planning & Support Program", "P&SP")
        AddNavButtonCategory(NavButtonCategories.DMU, "Data Management Unit")
        AddNavButtonCategory(NavButtonCategories.MASP, "Mobile & Area Sources Program")
        AddNavButtonCategory(NavButtonCategories.EIS, "Emission Inventory System")
        AddNavButtonCategory(NavButtonCategories.SBEAP, "Small Business Environmental Assistance Program")
    End Sub

    Private Sub CreateNavButtonsList()

        ' General
        AddNavButtonIfAccountHasFormAccess(1, "Facility Summary", "IAIPFacilitySummary", NavButtonCategories.General)
        AddNavButtonIfAccountHasFormAccess(7, "IAIP Query Generator", "IAIPQueryGenerator", NavButtonCategories.General)
        AddNavButtonIfAccountHasFormAccess(8, "Profile Management", "IAIPUserAdminTool", NavButtonCategories.General)

        ' SSPP
        AddNavButtonIfAccountHasFormAccess(3, "Application Log", "SSPPApplicationLog", NavButtonCategories.SSPP)
        AddNavButtonIfAccountHasFormAccess(9, "Permit File Uploader", "IAIPPermitUploader", NavButtonCategories.SSPP)
        AddNavButtonIfAccountHasFormAccess(19, "Attainment Status Tool", "SSPPAttainmentStatus", NavButtonCategories.SSPP)
        AddNavButtonIfAccountHasFormAccess(23, "PA/PN Report", "SSPPPublicNoticiesAndAdvisories", NavButtonCategories.SSPP)
        AddNavButtonIfAccountHasFormAccess(24, "SSPP Statistical Tools", "SSPPStatisticalTools", NavButtonCategories.SSPP)
        AddNavButtonIfAccountHasFormAccess(131, "Title V Tools", "DMUTitleVTools", NavButtonCategories.SSPP)

        ' SSCP
        AddNavButtonIfAccountHasFormAccess(4, "Compliance Log", "SSCPComplianceLog", NavButtonCategories.SSCP)
        AddNavButtonIfAccountHasFormAccess(22, "Compliance Managers", "SSCPManagersTools", NavButtonCategories.SSCP)
        AddNavButtonIfAccountHasFormAccess(136, "Compliance Admin", "SSCPAdministrator", NavButtonCategories.SSCP)
        AddNavButtonIfUserHasPermission(New String() {"(19)", "(20)", "(21)", "(23)", "(25)", "(118)", "(114)"}, _
                                        "Enforcement Documents", "SscpDocuments", NavButtonCategories.SSCP)

        ' ISMP
        AddNavButtonIfAccountHasFormAccess(5, "Monitoring Log", "ISMPMonitoringLog", NavButtonCategories.ISMP)
        AddNavButtonIfAccountHasFormAccess(14, "Test Report Information", "ISMPTestReportAdministrative", NavButtonCategories.ISMP)
        AddNavButtonIfAccountHasFormAccess(15, "Memo Viewer", "ISMPTestMemoViewer", NavButtonCategories.ISMP)
        AddNavButtonIfAccountHasFormAccess(16, "Ref. Number Management", "ISMPReferenceNumber", NavButtonCategories.ISMP)
        AddNavButtonIfAccountHasFormAccess(17, "ISMP Managers", "ISMPManagersTools", NavButtonCategories.ISMP)
        AddNavButtonIfAccountHasFormAccess(128, "Smoke School", "SmokeSchool", NavButtonCategories.ISMP)

        ' P&SP
        AddNavButtonIfAccountHasFormAccess(135, "Fees Log", "PASPFeesLog", NavButtonCategories.PASP)
        AddNavButtonIfAccountHasFormAccess(139, "Fee Management", "PASPFeeManagement", NavButtonCategories.PASP)
        AddNavButtonIfAccountHasFormAccess(12, "Fee Statistics && Reports", "PASPFeeStatistics", NavButtonCategories.PASP)
        AddNavButtonIfAccountHasFormAccess(6, "Fees Reports", "PASPFeeReports", NavButtonCategories.PASP)
        AddNavButtonIfAccountHasFormAccess(18, "Deposits", "PASPDepositsAmendments", NavButtonCategories.PASP)

        ' MASP
        AddNavButtonIfAccountHasFormAccess(137, "Registration Tool", "MASPRegistrationTool", NavButtonCategories.MASP)

        ' DMU
        AddNavButtonIfAccountHasFormAccess(129, "AFS Tools", "DMUDeveloperTools", NavButtonCategories.DMU)
        AddNavButtonIfAccountHasFormAccess(10, "District Tools", "IAIPDistrictSourceTool", NavButtonCategories.DMU)
        AddNavButtonIfAccountHasFormAccess(133, "Look Up Tables", "IAIPLookUpTables", NavButtonCategories.DMU)
        AddNavButtonIfAccountHasFormAccess(11, "AFS Validator", "AFSValidator", NavButtonCategories.DMU)
        AddNavButtonIfAccountHasFormAccess(132, "AFS Compare Tool", "IAIPAFSCompare", NavButtonCategories.DMU)
        If (UserGCode = "345") Then
            AddNavButtonIfAccountHasFormAccess(63, "Scary DMU-Only Tool", "DMUTool", NavButtonCategories.DMU)
        End If

        ' EIS
        AddNavButtonIfAccountHasFormAccess(20, "Emissions Summary Tool", "SSCPEmissionSummaryTool", NavButtonCategories.EIS)
        AddNavButtonIfAccountHasFormAccess(140, "Emission Inventory Log", "IAIP_EIS_Log", NavButtonCategories.EIS)
        AddNavButtonIfAccountHasFormAccess(130, "EIS && GECO Tools", "DMUStaffTools", NavButtonCategories.EIS)

        'SBEAP
        AddNavButtonIfUserHasPermission(New String() {"(142)", "(143)", "(118)"}, _
                                "Customer Summary", "SBEAPClientSummary", NavButtonCategories.SBEAP)
        AddNavButtonIfUserHasPermission(New String() {"(142)", "(143)", "(118)"}, _
                                "Case Log", "SBEAPCaseLog", NavButtonCategories.SBEAP)
        AddNavButtonIfUserHasPermission(New String() {"(142)", "(143)", "(118)"}, _
                                "Report Tool", "SBEAPReports", NavButtonCategories.SBEAP)
        AddNavButtonIfUserHasPermission(New String() {"(142)", "(143)", "(118)"}, _
                                "Phone Log", "SBEAPPhoneLog", NavButtonCategories.SBEAP)
        AddNavButtonIfUserHasPermission(New String() {"(142)", "(143)", "(118)"}, _
                                "Misc. Tools", "SBEAPMiscTools", NavButtonCategories.SBEAP)

    End Sub

#End Region

#End Region

#Region " Main Menu click events "

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

#Region " Testing Menu click events "

    Private Sub mmiPing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPing.Click
        DB.PingDBConnection(CurrentConnection)
    End Sub

#End Region

End Class
