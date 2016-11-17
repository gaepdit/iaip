Imports System.Collections.Generic
Imports System.ComponentModel
Imports Iaip.DAL.NavigationScreenData
Imports Iaip.SharedData

Public Class IAIPNavigation

#Region " Local variables and properties "

    Private Property WorkViewerTable As DataTable
    Private Property CurrentNavWorkListContext As NavWorkListContext
    Private Property CurrentNavWorkListScope As NavWorkListScope
    Private Property CurrentNavWorkListParameter As String = ""
    Private Property ExitWhenClosed As Boolean = True
    Private Property NavWorkListContextDictionary As Dictionary(Of NavWorkListContext, String)

#End Region

#Region " Form events "

    Private Sub IAIPNavigation_Load(sender As Object, e As EventArgs) Handles Me.Load
        monitor.TrackFeature("Main." & Me.Name)
        monitor.TrackFeature("Forms." & Me.Name)

        ' UI adjustments
        AssociateQuickNavButtons()
        SetUpNavWorkListContextChanger()
        SetUpNavWorkListScopeChanger()
        CheckSbeapPermissions()
        LoadStatusBar()
        EnableConnectionEnvironmentOptions()
        DisplayUsername()

        ' Start various Timers
        AppTimers.StartAppTimers()

#If SqlServer Then
        Me.Text = "IAIP SQL Server Edition"
#ElseIf UAT Then
        Me.Text = "IAIP UAT"
#End If
    End Sub

    Private Sub DisplayUsername()
        UsernameDisplay.Text = "Logged in as " & CurrentUser.Username
    End Sub

    Private Sub IAIPNavigation_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        monitor.TrackFeatureStop("Startup.LoggingIn")

        ' Start the bgrUserPermissions background worker
        BuildAccountPermissions()

        ' Start loading the Nav Work List background worker
        LoadWorkViewerData()
    End Sub

    Private Sub IAIPNavigation_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If ExitWhenClosed Then StartupShutdown.CloseIaip()
    End Sub

#End Region

#Region " Page Load procedures "

    Private Sub CheckSbeapPermissions()
        If CurrentUser.HasRole({142, 143, 118}) Then
            ShowControls({SbeapQuickAccessPanel})
        End If
    End Sub

    Private Sub LoadStatusBar()
        pnlName.Text = CurrentUser.AlphaName
        pnlDate.Text = Format(Today, DateFormat)
        pnlProgram.Text = CurrentUser.ProgramName
    End Sub

    Private Sub SetUpNavWorkListContextChanger()
        NavWorkListContextDictionary = New Dictionary(Of NavWorkListContext, String)
        For Each v As NavWorkListContext In [Enum].GetValues(GetType(NavWorkListContext))
            NavWorkListContextDictionary.Add(v, v.GetDescription)
        Next
        If Not CurrentUser.HasRole({142, 143, 118}) Then
            NavWorkListContextDictionary.Remove(NavWorkListContext.SbeapCases)
        End If
        cboNavWorkListContext.BindToDictionary(NavWorkListContextDictionary)
        AddHandler cboNavWorkListContext.SelectedValueChanged, AddressOf cboNavWorkListContext_SelectedValueChanged
        cboNavWorkListContext.SelectedValue = [Enum].Parse(GetType(NavWorkListContext), GetUserSetting(UserSetting.SelectedNavWorkListContext))
    End Sub

    Private Sub EnableConnectionEnvironmentOptions()
        Select Case CurrentServerEnvironment
            Case ServerEnvironment.DEV
                pnlDbEnv.Text = "DEV database"
                pnlDbEnv.BackColor = Color.Tomato
                pnlDbEnv.Visible = True
                lblTitle.Text = "IAIP Navigation Screen — DEV"
                TestingMenu.Visible = True
            Case ServerEnvironment.PRD
                pnlDbEnv.Text = "PRD"
                pnlDbEnv.Visible = False
                TestingMenu.Visible = False
            Case ServerEnvironment.UAT
                pnlDbEnv.Text = "UAT database"
                pnlDbEnv.BackColor = Color.SpringGreen
                pnlDbEnv.Visible = True
                lblTitle.Text = "IAIP Navigation Screen — UAT"
                TestingMenu.Visible = True
        End Select

#If SqlServer Then
        lblTitle.Text = "IAIP for SQL Server"
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

    Private Sub QuickAccessButton_Click(sender As Object, e As EventArgs) _
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

    Private Sub QuickAccessTextbox_Enter(sender As Object, e As EventArgs) _
    Handles txtOpenApplication.Enter, txtOpenEnforcement.Enter, txtOpenFacilitySummary.Enter, txtOpenSbeapCaseLog.Enter,
    txtOpenSbeapClient.Enter, txtOpenSscpItem.Enter, txtOpenTestLog.Enter, txtOpenTestReport.Enter
        Dim thisButton As Button = CType(CType(sender, TextBox).Tag, Button)
        Me.AcceptButton = thisButton
        thisButton.FlatStyle = FlatStyle.Standard
        thisButton.ForeColor = SystemColors.ControlText
    End Sub

    Private Sub QuickAccessTextbox_Leave(sender As Object, e As EventArgs) _
    Handles txtOpenApplication.Leave, txtOpenEnforcement.Leave, txtOpenFacilitySummary.Leave, txtOpenSbeapCaseLog.Leave,
    txtOpenSbeapClient.Leave, txtOpenSscpItem.Leave, txtOpenTestLog.Leave, txtOpenTestReport.Leave
        Dim thisButton As Button = CType(CType(sender, TextBox).Tag, Button)
        Me.AcceptButton = Nothing
        If Not thisButton.Tag Then
            thisButton.FlatStyle = FlatStyle.Flat
            thisButton.ForeColor = SystemColors.GrayText
        End If
    End Sub

    Private Sub QuickAccessTextbox_TextChanged(sender As Object, e As EventArgs) _
    Handles txtOpenApplication.TextChanged, txtOpenEnforcement.TextChanged, txtOpenFacilitySummary.TextChanged, txtOpenSbeapCaseLog.TextChanged,
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

    Private Sub QuickAccessButton_Enter(sender As Object, e As EventArgs) _
    Handles btnOpenApplication.Enter, btnOpenEnforcement.Enter, btnOpenFacilitySummary.Enter, btnOpenSbeapCaseLog.Enter,
    btnOpenSbeapClient.Enter, btnOpenSscpItem.Enter, btnOpenTestLog.Enter, btnOpenTestReport.Enter
        Dim thisButton As Button = CType(sender, Button)
        If thisButton.Tag Then
            thisButton.FlatStyle = FlatStyle.Standard
            thisButton.ForeColor = SystemColors.ControlText
        End If
    End Sub

    Private Sub QuickAccessButton_Leave(sender As Object, e As EventArgs) _
    Handles btnOpenApplication.Leave, btnOpenEnforcement.Leave, btnOpenFacilitySummary.Leave, btnOpenSbeapCaseLog.Leave,
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
        OpenFormPermitApplication(txtOpenApplication.Text)
    End Sub

    Private Sub OpenTestReport()
        Try
            Dim id As String = txtOpenTestReport.Text
            If id = "" Then Exit Sub

            If DAL.Ismp.StackTestExists(id) Then
                If CurrentUser.ProgramID = 3 Then
                    OpenMultiForm(ISMPTestReports, id)
                Else
                    If DAL.Ismp.StackTestIsClosedOut(id) Then
                        Dim PrintOut As New IAIPPrintOut
                        PrintOut.ReferenceValue = txtOpenTestReport.Text
                        PrintOut.PrintoutType = IAIPPrintOut.PrintType.IsmpTestReport
                        PrintOut.Show()
                    Else
                        MsgBox("This test has not been completely reviewed by ISMP.", MsgBoxStyle.Information, "Facility Summary")
                    End If
                End If
            Else
                MsgBox("Reference number is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub OpenEnforcement()
        OpenFormEnforcement(txtOpenEnforcement.Text)
    End Sub

    Private Sub OpenSscpItem()
        OpenFormSscpWorkItem(txtOpenSscpItem.Text)
    End Sub

    Private Sub OpenFacilitySummary()
        OpenFormFacilitySummary(txtOpenFacilitySummary.Text)
    End Sub

    Private Sub OpenTestLog()
        Try
            Dim id As String = txtOpenTestLog.Text
            If id = "" Then Exit Sub

            If DAL.Ismp.TestNotificationExists(id) Then
                OpenFormTestNotification(id)
            Else
                MsgBox("Notification number is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub OpenSbeapClient()
        Try
            Dim id As String = txtOpenSbeapClient.Text
            If id = "" Then Exit Sub

            If DAL.Sbeap.ClientExists(id) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub OpenSbeapCaseLog()
        Try
            Dim id As String = txtOpenSbeapCaseLog.Text
            If id = "" Then Exit Sub

            If DAL.Sbeap.CaseExists(id) Then

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
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

#Region " Nav Work List context "

    ''' <summary>
    ''' Enumeration of the various work list types (contexts) available on the main Navigation Screen
    ''' </summary>
    ''' <remarks>"None" may not be useful but is included just in case</remarks>
    Public Enum NavWorkListContext
        <Description("Compliance Work")> ComplianceWork
        <Description("Late FCEs")> LateFce
        <Description("Enforcement")> Enforcement
        <Description("Facilities Missing Subparts")> FacilitiesMissingSubparts
        <Description("Monitoring Test Reports")> MonitoringTestReports
        <Description("Monitoring Test Notifications")> MonitoringTestNotifications
        <Description("Permit Applications")> PermitApplications
        <Description("SBEAP Cases")> SbeapCases
    End Enum

    Public Enum NavWorkListScope
        StaffView
        UnitView
        ProgramView
    End Enum

    Private Sub SetWorkViewerContext()
        ' Get current Nav Work List parameters
        CurrentNavWorkListContext = cboNavWorkListContext.SelectedValue

        If rdbStaffView.Checked Then
            CurrentNavWorkListScope = NavWorkListScope.StaffView
            CurrentNavWorkListParameter = CurrentUser.UserID
        ElseIf rdbUnitView.Checked Then
            CurrentNavWorkListScope = NavWorkListScope.UnitView
            CurrentNavWorkListParameter = CurrentUser.UnitId
        Else
            CurrentNavWorkListScope = NavWorkListScope.ProgramView
            CurrentNavWorkListParameter = CurrentUser.ProgramID
        End If
    End Sub

    Private Sub btnLoadNavWorkList_Click(sender As Object, e As EventArgs) Handles btnLoadNavWorkList.Click
        monitor.TrackFeature("NavScreen.ChangeWorkViewer")
        LoadWorkViewerData()
        SaveUserSetting(UserSetting.SelectedNavWorkListContext, CurrentNavWorkListContext.ToString)
        SaveUserSetting(UserSetting.SelectedNavWorkListScope, CurrentNavWorkListScope.ToString)
    End Sub

    Private Sub pnlCurrentList_Enter(sender As Object, e As EventArgs) Handles pnlCurrentList.Enter
        Me.AcceptButton = btnLoadNavWorkList
    End Sub

    Private Sub pnlCurrentList_Leave(sender As Object, e As EventArgs) Handles pnlCurrentList.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub cboNavWorkListContext_SelectedValueChanged(sender As Object, e As EventArgs)
        Dim c As NavWorkListContext = cboNavWorkListContext.SelectedValue
        Select Case c
            Case NavWorkListContext.ComplianceWork, NavWorkListContext.Enforcement, NavWorkListContext.MonitoringTestReports, NavWorkListContext.PermitApplications, NavWorkListContext.SbeapCases
                NavWorkListScopePanel.Visible = True
            Case NavWorkListContext.LateFce, NavWorkListContext.FacilitiesMissingSubparts, NavWorkListContext.MonitoringTestNotifications
                NavWorkListScopePanel.Visible = False
        End Select
    End Sub

    Private Sub SetUpNavWorkListScopeChanger()
        Dim scope As NavWorkListScope = [Enum].Parse(GetType(NavWorkListScope), GetUserSetting(UserSetting.SelectedNavWorkListScope))

        Select Case scope
            Case NavWorkListScope.ProgramView
                rdbAllView.Checked = True
            Case NavWorkListScope.UnitView
                rdbUnitView.Checked = True
            Case Else
                rdbStaffView.Checked = True
        End Select
    End Sub

#End Region

#Region " Nav Work List formatters "

    Private Sub FormatWorkViewer()
        If dgvWorkViewer.Visible = True Then
            dgvWorkViewer.SanelyResizeColumns()
            dgvWorkViewer.MakeColumnsLookLikeLinks(0)
        End If

        If CurrentNavWorkListContext = NavWorkListContext.MonitoringTestReports Then
            LoadIsmpComplianceColor()
        End If
    End Sub

    Private Sub LoadIsmpComplianceColor()
        Try
            For Each row As DataGridViewRow In dgvWorkViewer.Rows
                If Not row.IsNewRow Then
                    If row.Cells("Precompliance Status").Value IsNot DBNull.Value AndAlso row.Cells("Precompliance Status").Value = "True" Then
                        row.DefaultCellStyle.BackColor = Color.Pink
                    End If
                    If row.Cells("Compliance Status").Value IsNot DBNull.Value AndAlso row.Cells("Compliance Status").Value = "Not In Compliance" Then
                        row.DefaultCellStyle.BackColor = Color.Tomato
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvWorkViewer_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvWorkViewer.CellFormatting
        If e IsNot Nothing AndAlso e.Value IsNot Nothing Then
            If dgvWorkViewer.Columns(e.ColumnIndex).HeaderText.ToUpper = "AIRS #" AndAlso Apb.ApbFacilityId.IsValidAirsNumberFormat(e.Value) Then
                e.Value = New Apb.ApbFacilityId(e.Value).FormattedString
            ElseIf TypeOf e.Value Is Date Then
                e.CellStyle.Format = DateFormat
            End If
        End If
    End Sub

#End Region

#Region " Nav Work List background worker (bgrLoadWorkViewer) "

    Private Sub LoadWorkViewerData()
        dgvWorkViewer.DataSource = Nothing
        dgvWorkViewer.Visible = False
        lblMessageLabel.Visible = True
        lblMessageLabel.Text = "Loading data…"
        pnlCurrentList.Enabled = False
        btnLoadNavWorkList.Text = "Loading…"
        btnLoadNavWorkList.Enabled = False
        lblResultsCount.Visible = False
        lblResultsCount.Text = ""

        ClearQuickAccessTool()

        SetWorkViewerContext()
        If bgrLoadWorkViewer.IsBusy Then
            bgrLoadWorkViewer.CancelAsync()
        Else
            bgrLoadWorkViewer.RunWorkerAsync()
        End If
    End Sub

    Private Sub bgrLoadWorkViewer_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgrLoadWorkViewer.DoWork
        WorkViewerTable = GetNavWorkList(CurrentNavWorkListContext, CurrentNavWorkListScope, CurrentNavWorkListParameter)
    End Sub

    Private Sub bgrLoadWorkViewer_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgrLoadWorkViewer.RunWorkerCompleted
        pnlCurrentList.Enabled = True
        btnLoadNavWorkList.Text = "Load"
        btnLoadNavWorkList.Enabled = True

        If WorkViewerTable IsNot Nothing AndAlso WorkViewerTable.Rows.Count > 0 Then
            dgvWorkViewer.DataSource = WorkViewerTable
            dgvWorkViewer.Visible = True
            lblMessageLabel.Visible = False
            lblMessageLabel.Text = ""
            lblResultsCount.Visible = True
            lblResultsCount.Text = WorkViewerTable.Rows.Count & " results"
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

#Region " Nav Work List (dgvWorkViewer) events "

    Private Sub dgvWorkViewer_Sorted(sender As Object, e As EventArgs) Handles dgvWorkViewer.Sorted
        If CurrentNavWorkListContext = NavWorkListContext.MonitoringTestReports Then LoadIsmpComplianceColor()
    End Sub

    Private Sub dgvWorkViewer_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) _
    Handles dgvWorkViewer.CellMouseEnter
        'Console.WriteLine("CellMouseEnter: " & e.ColumnIndex.ToString & ", " & e.RowIndex.ToString)
        ' Change cursor and text color when hovering over first column (treats text like a hyperlink)
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvWorkViewer.RowCount AndAlso e.ColumnIndex = 0 Then
            dgvWorkViewer.MakeCellLookLikeHoveredLink(e.RowIndex, e.ColumnIndex, True)
        End If
    End Sub

    Private Sub dgvWorkViewer_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) _
    Handles dgvWorkViewer.CellMouseLeave
        'Console.WriteLine("CellMouseLeave: " & e.ColumnIndex.ToString & ", " & e.RowIndex.ToString)
        ' Reset cursor and text color when mouse leaves (un-hovers) a cell
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvWorkViewer.RowCount AndAlso e.ColumnIndex = 0 Then
            dgvWorkViewer.MakeCellLookLikeHoveredLink(e.RowIndex, e.ColumnIndex, False)
        End If
    End Sub

    Private Sub dgvWorkViewer_CellEnter(sender As Object, e As DataGridViewCellEventArgs) _
    Handles dgvWorkViewer.CellEnter
        'Console.WriteLine("CellEnter: " & e.ColumnIndex.ToString & ", " & e.RowIndex.ToString)
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvWorkViewer.RowCount Then
            SelectItemNumbers(e.RowIndex)
        End If
    End Sub

    Private Sub dgvWorkViewer_CellClick(sender As Object, e As DataGridViewCellEventArgs) _
    Handles dgvWorkViewer.CellClick
        'Console.WriteLine("CellClick: " & e.ColumnIndex.ToString & ", " & e.RowIndex.ToString)
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvWorkViewer.RowCount AndAlso e.ColumnIndex = 0 Then
            OpenSelectedItem()
        End If
    End Sub

    Private Sub dgvWorkViewer_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) _
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

    Private Sub SelectItemNumbers(row As Integer)
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


    Private Sub bgrUserPermissions_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgrUserPermissions.DoWork
        Dim AccountFormAccessLookup As DataTable = GetSharedData(SharedTable.IaipAccountRoles)
        Dim accountFormAccessString As String

        For Each account As Integer In CurrentUser.IaipRoles.RoleCodes
            accountFormAccessString = AccountFormAccessLookup.Rows.Find(account)("FormAccess").ToString

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
    End Sub

    Private Sub bgrUserPermissions_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgrUserPermissions.RunWorkerCompleted
        CreateNavButtonCategoriesList()
        CreateNavButtonsList()
        CreateNavButtons()
    End Sub

#End Region

#Region " Nav Button creation "

#Region " Containers "

    Private Structure NavButton
        Public Sub New(buttonText As String, formName As String)
            Me.ButtonText = buttonText
            Me.FormName = formName
        End Sub
        Public ButtonText As String
        Public FormName As String
    End Structure

    Private Structure NavButtonCategory
        Public Sub New(category As NavButtonCategories, name As String, Optional shortname As String = Nothing)
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

    Private Function AccountHasAccessToForm(index As Int32) As Boolean
        Return (AccountFormAccess(index, 0) IsNot Nothing _
                AndAlso AccountFormAccess(index, 0) = index.ToString _
                AndAlso (AccountFormAccess(index, 1) = "1" Or AccountFormAccess(index, 2) = "1" _
                         Or AccountFormAccess(index, 3) = "1" Or AccountFormAccess(index, 4) = "1")
                         )
    End Function

    Private Sub AddNavButton(buttonText As String, formName As String, category As NavButtonCategories)
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

    Private Sub AddNavButtonIfAccountHasFormAccess(index As Integer,
                                                    buttonText As String, formName As String,
                                                    category As NavButtonCategories)
        If AccountHasAccessToForm(index) Then
            AddNavButton(buttonText, formName, category)
        End If
    End Sub

    Private Sub AddNavButtonIfUserHasPermission(permissionsAllowed As Integer(),
                                                 buttonText As String, formName As String,
                                                 category As NavButtonCategories)
        If CurrentUser.HasRole(permissionsAllowed) Then
            AddNavButton(buttonText, formName, category)
        End If
    End Sub

    Private Sub AddNavButtonIfUserHasPermission(permissionAllowed As Integer,
                                                 buttonText As String, formName As String,
                                                 category As NavButtonCategories)
        AddNavButtonIfUserHasPermission({permissionAllowed},
                                        buttonText, formName, category)
    End Sub

    Private Sub AddNavButtonCategory(category As NavButtonCategories, name As String, Optional shortname As String = Nothing)
        If CurrentUser.ProgramName = name OrElse CurrentUser.UnitName = name Then
            AllTheNavButtonCategories.Insert(0, New NavButtonCategory(category, name, shortname))
        Else
            AllTheNavButtonCategories.Add(New NavButtonCategory(category, name, shortname))
        End If
    End Sub

    Private Sub NavButton_Click(sender As Object, e As EventArgs)
        Dim nb As NavButton = CType(sender, Button).Tag
        monitor.TrackFeature("NavScreen.NavButton")
        monitor.TrackFeature("NavButton." & nb.FormName)
        OpenSingleForm(nb.FormName)
    End Sub

    Private Sub CreateNavButtons()
        Dim buttonHeight As Integer = 38
        Dim buttonWidth As Integer = 90

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
        Fees
        DMU
        MASP
        EIS
        SBEAP
    End Enum

    Private Sub CreateNavButtonCategoriesList()
        AddNavButtonCategory(NavButtonCategories.General, "General")
        AddNavButtonCategory(NavButtonCategories.ISMP, "Industrial Source Monitoring Program", "ISMU")
        AddNavButtonCategory(NavButtonCategories.SSPP, "Stationary Source Permitting Program")
        AddNavButtonCategory(NavButtonCategories.SSCP, "Stationary Source Compliance Program")
        AddNavButtonCategory(NavButtonCategories.Fees, "Financial Management Unit")
        AddNavButtonCategory(NavButtonCategories.DMU, "Data Management Unit")
        AddNavButtonCategory(NavButtonCategories.MASP, "Mobile & Area Sources Program")
        AddNavButtonCategory(NavButtonCategories.EIS, "Emission Inventory System")
        AddNavButtonCategory(NavButtonCategories.SBEAP, "Small Business Environmental Assistance Program")
    End Sub

    Private Sub CreateNavButtonsList()

        ' General
        AddNavButtonIfAccountHasFormAccess(1, "Facility Summary", NameOf(IAIPFacilitySummary), NavButtonCategories.General)
        AddNavButtonIfAccountHasFormAccess(7, "Query Generator", NameOf(IAIPQueryGenerator), NavButtonCategories.General)
        AddNavButtonIfAccountHasFormAccess(8, "User Management", NameOf(IaipUserManagement), NavButtonCategories.General)

        ' SSPP
        AddNavButtonIfAccountHasFormAccess(3, "Application Log", NameOf(SSPPApplicationLog), NavButtonCategories.SSPP)
        AddNavButtonIfAccountHasFormAccess(9, "Permit File Uploader", NameOf(SSPPPermitUploader), NavButtonCategories.SSPP)
        AddNavButtonIfAccountHasFormAccess(19, "Attainment Status Tool", NameOf(SSPPAttainmentStatus), NavButtonCategories.SSPP)
        AddNavButtonIfAccountHasFormAccess(23, "PA/PN Report", NameOf(SSPPPublicNoticiesAndAdvisories), NavButtonCategories.SSPP)
        AddNavButtonIfAccountHasFormAccess(24, "SSPP Statistical Tools", NameOf(SSPPStatisticalTools), NavButtonCategories.SSPP)
        AddNavButtonIfAccountHasFormAccess(131, "Title V Tools", NameOf(SSPPTitleVTools), NavButtonCategories.SSPP)

        ' SSCP
        AddNavButtonIfAccountHasFormAccess(4, "Compliance Log", NameOf(SSCPComplianceLog), NavButtonCategories.SSCP)
        AddNavButtonIfAccountHasFormAccess(22, "Compliance Management", NameOf(SSCPManagersTools), NavButtonCategories.SSCP)
        AddNavButtonIfUserHasPermission({19, 20, 21, 23, 25, 118, 114}, "Enforcement Documents", NameOf(SscpDocuments), NavButtonCategories.SSCP)

        ' ISMP
        AddNavButtonIfAccountHasFormAccess(5, "Monitoring Log", NameOf(ISMPMonitoringLog), NavButtonCategories.ISMP)
        AddNavButtonIfAccountHasFormAccess(14, "Test Report Information", NameOf(ISMPTestReportAdministrative), NavButtonCategories.ISMP)
        AddNavButtonIfAccountHasFormAccess(15, "Memo Viewer", NameOf(ISMPTestMemoViewer), NavButtonCategories.ISMP)
        AddNavButtonIfAccountHasFormAccess(17, "ISMU Management", NameOf(ISMPManagersTools), NavButtonCategories.ISMP)

        ' Fees
        AddNavButtonIfAccountHasFormAccess(135, "Fees Log", NameOf(PASPFeesLog), NavButtonCategories.Fees)
        AddNavButtonIfAccountHasFormAccess(139, "Fee Management", NameOf(PASPFeeManagement), NavButtonCategories.Fees)
        AddNavButtonIfAccountHasFormAccess(12, "Fee Statistics && Reports", NameOf(PASPFeeStatistics), NavButtonCategories.Fees)
        AddNavButtonIfAccountHasFormAccess(18, "Deposits", NameOf(PASPDepositsAmendments), NavButtonCategories.Fees)

        ' MASP
        AddNavButtonIfAccountHasFormAccess(137, "Registration Tool", NameOf(MASPRegistrationTool), NavButtonCategories.MASP)

        ' DMU
        AddNavButtonIfAccountHasFormAccess(129, "Error Logs", NameOf(DMUIaipErrorLog), NavButtonCategories.DMU)
        AddNavButtonIfUserHasPermission({118, 19, 28}, "EDT Errors", NameOf(DmuEdtErrorMessages), NavButtonCategories.DMU)
        AddNavButtonIfAccountHasFormAccess(10, "District Tools", NameOf(IAIPDistrictSourceTool), NavButtonCategories.DMU)
        AddNavButtonIfAccountHasFormAccess(133, "Lookup Tables", NameOf(IAIPLookUpTables), NavButtonCategories.DMU)
        AddNavButtonIfUserHasPermission(118, "Organization Editor", NameOf(IAIPListTool), NavButtonCategories.DMU)

        ' EIS
        AddNavButtonIfAccountHasFormAccess(20, "Emissions Summary Tool", NameOf(SSCPEmissionSummaryTool), NavButtonCategories.EIS)
        AddNavButtonIfAccountHasFormAccess(130, "EIS && GECO Tools", NameOf(DMUEisGecoTool), NavButtonCategories.EIS)

        'SBEAP
        AddNavButtonIfUserHasPermission({142, 143, 118}, "Customer Summary", NameOf(SBEAPClientSummary), NavButtonCategories.SBEAP)
        AddNavButtonIfUserHasPermission({142, 143, 118}, "Case Log", NameOf(SBEAPCaseLog), NavButtonCategories.SBEAP)
        AddNavButtonIfUserHasPermission({142, 143, 118}, "Report Tool", NameOf(SBEAPReports), NavButtonCategories.SBEAP)
        AddNavButtonIfUserHasPermission({142, 143, 118}, "Phone Log", NameOf(SBEAPPhoneLog), NavButtonCategories.SBEAP)
        AddNavButtonIfUserHasPermission({142, 143, 118}, "Contact Data", NameOf(SBEAPMiscTools), NavButtonCategories.SBEAP)

    End Sub

#End Region

#End Region

#Region " Main Menu click events "

    Private Sub mmiExit_Click(sender As Object, e As EventArgs) Handles mmiExit.Click
        Me.Close()
    End Sub

    Private Sub mmiExport_Click(sender As Object, e As EventArgs) Handles mmiExport.Click
        dgvWorkViewer.ExportToExcel(Me)
    End Sub

    Private Sub UpdateProfile_Click(sender As Object, e As EventArgs) Handles UpdateProfile.Click
        Dim pf As New IaipUserProfile
        pf.ShowDialog()
        If pf.DialogResult = DialogResult.OK Then
            MessageBox.Show("Profile successfully updated.", "Success", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub ChangePassword_Click(sender As Object, e As EventArgs) Handles ChangePassword.Click
        Dim cp As New IaipChangePassword
        cp.ShowDialog()
        If cp.DialogResult = DialogResult.OK Then
            MessageBox.Show("Password successfully updated.", "Success", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub LogOut_Click(sender As Object, e As EventArgs) Handles LogOut.Click
        Dim currentlyOpenForms As FormCollection = Application.OpenForms

        If currentlyOpenForms Is Nothing Then
            CloseIaip()
        ElseIf currentlyOpenForms.Count > 1 Then
            MessageBox.Show("Close all open IAIP windows before logging off.", "Save your work", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            For Each f As Form In currentlyOpenForms
                If f.WindowState = FormWindowState.Minimized Then
                    f.WindowState = FormWindowState.Normal
                End If
                f.Show()
                f.Activate()
            Next
        Else
            ExitWhenClosed = False
            LogOutUser()
            OpenSingleForm(IAIPLogIn)
            Me.Close()
        End If
    End Sub

    Private Sub mmiResetForm_Click(sender As Object, e As EventArgs) Handles mmiResetForm.Click
        ResetAllFormSettings()
        Me.Location = New Point(0, 0)
        Me.Size = New Size(808, 460)
    End Sub

    Private Sub mmiAbout_Click(sender As Object, e As EventArgs) Handles mmiAbout.Click
        IaipAbout.ShowDialog()
    End Sub

    Private Sub mmiOnlineHelp_Click(sender As Object, e As EventArgs) Handles mmiOnlineHelp.Click
        OpenDocumentationUrl(Me)
    End Sub

#End Region

#Region " Testing Menu click events "

    Private Sub RunTest_Click(sender As Object, e As EventArgs) Handles RunTest.Click
        Dim StaffReports As New ISMPStaffReports
        StaffReports.Show()

        'Throw New Exception("Unhandled exception testing")
    End Sub

#End Region

End Class
