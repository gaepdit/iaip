Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Net.NetworkInformation
Imports Iaip.DAL.NavigationScreenData
Imports Iaip.UrlHelpers
Imports Iaip.ApiCalls.Notifications

Public Class IAIPNavigation

#Region " Local variables and properties "

    Private Property WorkViewerTable As DataTable
    Private Property CurrentNavWorkListContext As NavWorkListContext
    Private Property CurrentNavWorkListScope As NavWorkListScope
    Private Property CurrentNavWorkListParameter As Integer?
    Private Property NavWorkListContextDictionary As Dictionary(Of NavWorkListContext, String)

    Private Property CheckingNetwork As Boolean

#End Region

#Region " Form events "

    Protected Overrides Sub OnLoad(e As EventArgs)
        ' UI adjustments
        AssociateQuickNavButtons()
        SetUpNavWorkListContextChanger()
        SetUpNavWorkListScopeChanger()
        LoadStatusBar()
        EnableConnectionEnvironmentOptions()
        DisplayUsername()

        AddHandler NetworkChange.NetworkAddressChanged, AddressOf AddressChangedCallback

        MyBase.OnLoad(e)
    End Sub

    Private Sub AddressChangedCallback(sender As Object, e As EventArgs)
        If networkCheckTimer Is Nothing Then
            CheckNetworkConnection()
        Else
            networkCheckTimerCount = Math.Min(networkCheckTimerCount, 3)
        End If
    End Sub

    Private Sub btnRetryConnection_Click(sender As Object, e As EventArgs) Handles btnRetryConnection.Click
        lblNetworkCheckCountdown.Text = "Trying now..."
        CheckNetworkConnection()
    End Sub

    Public Sub CheckNetworkConnection()
        If CheckingNetwork OrElse bgrNetworkChecker.IsBusy Then Return

        StopNetworkCheckTimer()
        CheckingNetwork = True

        Try
            bgrNetworkChecker.RunWorkerAsync()
        Catch ex As InvalidOperationException
            If ex.Message.Contains("This BackgroundWorker is currently busy and cannot run multiple tasks concurrently.") Then
                Return
            End If
        End Try
    End Sub

    Private Sub bgrNetworkChecker_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgrNetworkChecker.DoWork
        e.Result = GetIaipNetworkStatusAsync().Result
    End Sub

    Private Sub bgrNetworkChecker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgrNetworkChecker.RunWorkerCompleted
        Dim status As IaipNetworkStatus = e.Result

        Select Case status
            Case IaipNetworkStatus.Enabled
                pnlConnectionStatus.Text = "🌐"
                pnlConnectionStatus.ToolTipText = "Connected"
                AutoValidate = AutoValidate.Inherit
                txtOpenFacilitySummary.AutoValidate = AutoValidate.EnableAllowFocusChange

            Case Else
                pnlConnectionStatus.Text = "⛔"
                pnlConnectionStatus.ToolTipText = "Connection Error"
                AutoValidate = AutoValidate.Disable
                txtOpenFacilitySummary.AutoValidate = AutoValidate.Disable
                StartNetworkCheckTimer()
                BringToFront()
        End Select

        Select Case status
            Case IaipNetworkStatus.Enabled
                pnlConnectionWarning.Visible = False

            Case IaipNetworkStatus.NoInternet
                pnlConnectionWarning.Visible = True
                lblConnectionWarning.Text = "It appears you have lost connection to the Internet. " &
                    vbNewLine & vbNewLine &
                    "Please check your connection and try again."

            Case IaipNetworkStatus.NoVpn
                pnlConnectionWarning.Visible = True
                lblConnectionWarning.Text = "It appears you have lost connection to the VPN. " &
                    vbNewLine & vbNewLine &
                    "Please check your connection and try again."

            Case IaipNetworkStatus.NoDb
                pnlConnectionWarning.Visible = True
                lblConnectionWarning.Text = "Unable to connect to the database. " &
                    vbNewLine & vbNewLine &
                    "Please wait a few minutes and try again. " &
                    "If still unable to connect, please contact EPD-IT for support."

            Case IaipNetworkStatus.AppDisabled
                pnlConnectionWarning.Visible = True
                lblConnectionWarning.Text = "The IAIP is temporarily down for maintenance. " &
                    vbNewLine & vbNewLine &
                    "Please wait a few minutes and try again or " &
                    "contact EPD-IT for more information."

        End Select

        CheckingNetwork = False
    End Sub

    Private networkCheckTimer As Timer
    Private networkCheckTimerCount As Integer
    Private ReadOnly networkCheckTimerCountMax As Integer = 45

    Private Sub StartNetworkCheckTimer()
        networkCheckTimerCount = networkCheckTimerCountMax
        networkCheckTimer = New Timer()
        AddHandler networkCheckTimer.Tick, AddressOf NetworkCheckTimerTickEventHandler
        networkCheckTimer.Interval = 1000
        networkCheckTimer.Start()
    End Sub

    Private Sub StopNetworkCheckTimer()
        If networkCheckTimer Is Nothing Then Return
        networkCheckTimerCount = networkCheckTimerCountMax
        networkCheckTimer.Stop()
        networkCheckTimer.Dispose()
    End Sub

    Private Sub NetworkCheckTimerTickEventHandler(sender As Object, e As EventArgs)
        networkCheckTimerCount -= 1
        lblNetworkCheckCountdown.Text = $"Trying again in {networkCheckTimerCount} second{If(networkCheckTimerCount = 1, "", "s")}..."

        If networkCheckTimerCount <= 0 Then
            lblNetworkCheckCountdown.Text = "Trying now..."
            CheckNetworkConnection()
        End If
    End Sub

    Private Sub DisplayUsername()
        mmiUsernameDisplay.Text = "Logged in as " & CurrentUser.Username
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        ' Start the bgrUserPermissions background worker
        BuildAccountPermissions()

        ' Start the org notifications check background worker
        LoadOrgNotifications()

        ' Start loading the Nav Work List background worker
        LoadWorkViewerData()

        ' Start pre-loading shared data in the background
        PreloadSharedData()

        MyBase.OnShown(e)
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If Not LoggingOff AndAlso Not IaipExiting Then
            Dim openForms As New List(Of Form)

            For Each frm As Form In Application.OpenForms
                If frm IsNot Me Then
                    openForms.Add(frm)
                End If
            Next

            For Each frm As Form In openForms
                frm.Close()
            Next

            If Application.OpenForms.Count <= 1 Then
                CloseIaip()
            Else
                ShowAllForms()
                e.Cancel = True
            End If
        End If

        LoggingOff = False

        MyBase.OnFormClosing(e)
    End Sub

#End Region

#Region " Page Load procedures "

    Private Sub LoadStatusBar()
        pnlName.Text = CurrentUser.FullName
        pnlDate.Text = Format(Today, DateFormat)
        pnlProgram.Text = CurrentUser.ProgramName
    End Sub

    Private Sub SetUpNavWorkListContextChanger()
        NavWorkListContextDictionary = New Dictionary(Of NavWorkListContext, String)
        For Each v As NavWorkListContext In [Enum].GetValues(GetType(NavWorkListContext))
            NavWorkListContextDictionary.Add(v, v.GetDescription)
        Next
        cboNavWorkListContext.BindToDictionary(NavWorkListContextDictionary)
        AddHandler cboNavWorkListContext.SelectedValueChanged, AddressOf cboNavWorkListContext_SelectedValueChanged
        cboNavWorkListContext.SelectedValue = [Enum].Parse(GetType(NavWorkListContext), GetUserSetting(UserSetting.SelectedNavWorkListContext))
    End Sub

    Private Sub EnableConnectionEnvironmentOptions()
        Select Case CurrentServerEnvironment
            Case ServerEnvironment.Development
                pnlDbEnv.Text = "DEV database"
                pnlDbEnv.BackColor = Color.YellowGreen
                pnlDbEnv.Visible = True
                lblTitle.Text = "IAIP Navigation Screen — DEV"
                TestingMenu.Visible = True
            Case ServerEnvironment.Production
                pnlDbEnv.Text = "PRD"
                pnlDbEnv.Visible = False
                TestingMenu.Visible = False
            Case ServerEnvironment.Staging
                pnlDbEnv.Text = "UAT database"
                pnlDbEnv.BackColor = Color.SpringGreen
                pnlDbEnv.Visible = True
                lblTitle.Text = "IAIP Navigation Screen — UAT"
                'TestingMenu.Visible = True
        End Select
    End Sub

#End Region

#Region " Quick Access Tool buttons and events "

    Private Sub AssociateQuickNavButtons()
        txtOpenApplication.Tag = btnOpenApplication
        txtOpenEnforcement.Tag = btnOpenEnforcement
        txtOpenFacilitySummary.Tag = btnOpenFacilitySummary
        txtOpenSscpItem.Tag = btnOpenSscpItem
        txtOpenTestLog.Tag = btnOpenTestLog
        txtOpenTestReport.Tag = btnOpenTestReport
    End Sub

    Private Sub QuickAccessButton_Click(sender As Object, e As EventArgs) _
    Handles btnOpenFacilitySummary.Click, btnOpenTestReport.Click, btnOpenTestLog.Click, btnOpenSscpItem.Click, btnOpenEnforcement.Click, btnOpenApplication.Click
        Cursor = Cursors.WaitCursor

        Dim thisButton As Button = CType(sender, Button)

        Try
            Select Case thisButton.Name
                Case btnOpenApplication.Name
                    OpenApplication()
                Case btnOpenEnforcement.Name
                    OpenEnforcement()
                Case btnOpenFacilitySummary.Name
                    OpenFacilitySummary()
                Case btnOpenSscpItem.Name
                    OpenSscpItem()
                Case btnOpenTestLog.Name
                    OpenTestLog()
                Case btnOpenTestReport.Name
                    OpenTestReport()
            End Select
        Catch ex As Exception
            ex.Data.AddAsUniqueIfExists("txtOpenApplication", txtOpenApplication.Text)
            ex.Data.AddAsUniqueIfExists("txtOpenEnforcement", txtOpenEnforcement.Text)
            ex.Data.AddAsUniqueIfExists("txtOpenFacilitySummary", txtOpenFacilitySummary.Text)
            ex.Data.AddAsUniqueIfExists("txtOpenSscpItem", txtOpenSscpItem.Text)
            ex.Data.AddAsUniqueIfExists("txtOpenTestLog", txtOpenTestLog.Text)
            ex.Data.AddAsUniqueIfExists("txtOpenTestReport", txtOpenTestReport.Text)
            Throw
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub QuickAccessTextbox_Enter(sender As Object, e As EventArgs) _
    Handles txtOpenApplication.Enter, txtOpenEnforcement.Enter, txtOpenFacilitySummary.AirsTextEnter, txtOpenSscpItem.Enter, txtOpenTestLog.Enter, txtOpenTestReport.Enter
        Dim thisButton As Button = CType(CType(sender, TextBox).Tag, Button)
        Me.AcceptButton = thisButton
        thisButton.FlatStyle = FlatStyle.Standard
        thisButton.ForeColor = SystemColors.ControlText
    End Sub

    Private Sub QuickAccessTextbox_Leave(sender As Object, e As EventArgs) _
    Handles txtOpenApplication.Leave, txtOpenEnforcement.Leave, txtOpenFacilitySummary.AirsTextLeave, txtOpenSscpItem.Leave, txtOpenTestLog.Leave, txtOpenTestReport.Leave
        Dim thisButton As Button = CType(CType(sender, TextBox).Tag, Button)
        Me.AcceptButton = Nothing
        If Not CBool(thisButton.Tag) Then
            thisButton.FlatStyle = FlatStyle.Flat
            thisButton.ForeColor = SystemColors.GrayText
        End If
    End Sub

    Private Sub QuickAccessTextbox_TextChanged(sender As Object, e As EventArgs) _
    Handles txtOpenApplication.TextChanged, txtOpenEnforcement.TextChanged, txtOpenFacilitySummary.AirsTextChanged, txtOpenSscpItem.TextChanged, txtOpenTestLog.TextChanged, txtOpenTestReport.TextChanged
        Dim thisTextbox As TextBox = CType(sender, TextBox)
        Dim thisButton As Button = CType(thisTextbox.Tag, Button)
        If thisTextbox.TextLength > 0 Then
            thisButton.FlatStyle = FlatStyle.Standard
            thisButton.ForeColor = SystemColors.ControlText
            thisButton.Tag = True
        Else
            thisButton.Tag = Nothing
            If Not thisTextbox.Focused AndAlso Not thisButton.Focused Then
                thisButton.FlatStyle = FlatStyle.Flat
                thisButton.ForeColor = SystemColors.GrayText
            End If
        End If
    End Sub

    Private Sub QuickAccessButton_Enter(sender As Object, e As EventArgs) _
    Handles btnOpenApplication.Enter, btnOpenEnforcement.Enter, btnOpenFacilitySummary.Enter, btnOpenSscpItem.Enter, btnOpenTestLog.Enter, btnOpenTestReport.Enter
        Dim thisButton As Button = CType(sender, Button)
        If CBool(thisButton.Tag) Then
            thisButton.FlatStyle = FlatStyle.Standard
            thisButton.ForeColor = SystemColors.ControlText
        End If
    End Sub

    Private Sub QuickAccessButton_Leave(sender As Object, e As EventArgs) _
    Handles btnOpenApplication.Leave, btnOpenEnforcement.Leave, btnOpenFacilitySummary.Leave, btnOpenSscpItem.Leave, btnOpenTestLog.Leave, btnOpenTestReport.Leave
        Dim thisButton As Button = CType(sender, Button)
        If AcceptButton IsNot thisButton AndAlso Not CBool(thisButton.Tag) Then
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
        OpenFormTestReport(txtOpenTestReport.Text, Me)
    End Sub

    Private Sub OpenEnforcement()
        OpenFormEnforcement(txtOpenEnforcement.Text)
    End Sub

    Private Sub OpenSscpItem()
        OpenFormSscpWorkItem(txtOpenSscpItem.Text)
    End Sub

    Private Sub OpenFacilitySummary()
        Select Case txtOpenFacilitySummary.ValidationStatus
            Case DAL.AirsNumberValidationResult.Empty
                OpenFormFacilitySummary()
            Case DAL.AirsNumberValidationResult.InvalidFormat
                MessageBox.Show("AIRS number is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Case DAL.AirsNumberValidationResult.NonExistent
                MessageBox.Show("Facility does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Case DAL.AirsNumberValidationResult.Valid
                OpenFormFacilitySummary(txtOpenFacilitySummary.AirsNumber)
        End Select
    End Sub

    Private Sub OpenTestLog()
        Try
            Dim id As String = txtOpenTestLog.Text
            If id = "" Then
                Return
            End If

            If DAL.Ismp.TestNotificationExists(id) Then
                OpenFormTestNotification(id)
            Else
                MsgBox("Notification number is not in the system.", MsgBoxStyle.Information, Me.Text)
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
    End Sub

#End Region

#Region " Nav Work List context "

    ''' <summary>
    ''' Enumeration of the various work list types (contexts) available on the main Navigation Screen
    ''' </summary>
    Public Enum NavWorkListContext
        <Description("Compliance Work")> ComplianceWork
        <Description("Late FCEs")> LateFce
        <Description("Enforcement")> Enforcement
        <Description("Facilities Missing Subparts")> FacilitiesMissingSubparts
        <Description("Monitoring Test Reports")> MonitoringTestReports
        <Description("Monitoring Test Notifications")> MonitoringTestNotifications
        <Description("Permit Applications")> PermitApplications
    End Enum

    Public Enum NavWorkListScope
        StaffView
        UnitView
        ProgramView
    End Enum

    Private Sub SetWorkViewerContext()
        ' Get current Nav Work List parameters
        CurrentNavWorkListContext = CType(cboNavWorkListContext.SelectedValue, NavWorkListContext)

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
        Dim c As NavWorkListContext = CType(cboNavWorkListContext.SelectedValue, NavWorkListContext)
        Select Case c
            Case NavWorkListContext.ComplianceWork, NavWorkListContext.Enforcement, NavWorkListContext.MonitoringTestReports, NavWorkListContext.PermitApplications
                NavWorkListScopePanel.Visible = True
            Case NavWorkListContext.LateFce, NavWorkListContext.FacilitiesMissingSubparts, NavWorkListContext.MonitoringTestNotifications
                NavWorkListScopePanel.Visible = False
        End Select
    End Sub

    Private Sub SetUpNavWorkListScopeChanger()
        Dim scope As NavWorkListScope = CType([Enum].Parse(GetType(NavWorkListScope), GetUserSetting(UserSetting.SelectedNavWorkListScope)), NavWorkListScope)

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
        If dgvWorkViewer.Visible Then
            dgvWorkViewer.SanelyResizeColumns()
            dgvWorkViewer.MakeColumnLookLikeLinks(0)
        End If

        If CurrentNavWorkListContext = NavWorkListContext.MonitoringTestReports Then
            LoadIsmpComplianceColor()
        End If
    End Sub

    Private Sub LoadIsmpComplianceColor()
        Try
            For Each row As DataGridViewRow In dgvWorkViewer.Rows
                If Not row.IsNewRow Then
                    If row.Cells("Precompliance Status").Value IsNot DBNull.Value AndAlso row.Cells("Precompliance Status").Value.ToString() = "True" Then
                        row.DefaultCellStyle.BackColor = Color.Pink
                    End If
                    If row.Cells("Compliance Status").Value IsNot DBNull.Value AndAlso row.Cells("Compliance Status").Value.ToString() = "Not In Compliance" Then
                        row.DefaultCellStyle.BackColor = Color.Tomato
                    End If
                End If
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvWorkViewer_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvWorkViewer.CellFormatting
        If e IsNot Nothing AndAlso e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
            If dgvWorkViewer.Columns(e.ColumnIndex).HeaderText.ToUpper = "AIRS #" AndAlso Apb.ApbFacilityId.IsValidAirsNumberFormat(e.Value.ToString()) Then
                e.Value = New Apb.ApbFacilityId(e.Value.ToString).FormattedString
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
            FormatWorkViewer()
        Else
            dgvWorkViewer.DataSource = Nothing
            dgvWorkViewer.Visible = False
            lblMessageLabel.Visible = True
            lblMessageLabel.Text = "No data to display"
        End If
    End Sub

#End Region

#Region " Nav Work List (dgvWorkViewer) events "

    Private Sub dgvWorkViewer_Sorted(sender As Object, e As EventArgs) Handles dgvWorkViewer.Sorted
        If CurrentNavWorkListContext = NavWorkListContext.MonitoringTestReports Then LoadIsmpComplianceColor()
    End Sub

    Private Sub dgvWorkViewer_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) _
    Handles dgvWorkViewer.CellMouseEnter
        ' Change cursor and text color when hovering over first column (treats text like a hyperlink)
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvWorkViewer.RowCount AndAlso e.ColumnIndex = 0 Then
            dgvWorkViewer.MakeCellLookLikeHoveredLink(e.RowIndex, e.ColumnIndex, True)
        End If
    End Sub

    Private Sub dgvWorkViewer_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) _
    Handles dgvWorkViewer.CellMouseLeave
        ' Reset cursor and text color when mouse leaves (un-hovers) a cell
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvWorkViewer.RowCount AndAlso e.ColumnIndex = 0 Then
            dgvWorkViewer.MakeCellLookLikeHoveredLink(e.RowIndex, e.ColumnIndex, False)
        End If
    End Sub

    Private Sub dgvWorkViewer_CellEnter(sender As Object, e As DataGridViewCellEventArgs) _
    Handles dgvWorkViewer.CellEnter
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvWorkViewer.RowCount Then
            SelectItemNumbers(e.RowIndex)
        End If
    End Sub

    Private Sub dgvWorkViewer_CellClick(sender As Object, e As DataGridViewCellEventArgs) _
    Handles dgvWorkViewer.CellClick
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvWorkViewer.RowCount AndAlso e.ColumnIndex = 0 Then
            OpenSelectedItem()
        End If
    End Sub

    Private Sub dgvWorkViewer_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) _
    Handles dgvWorkViewer.CellDoubleClick
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvWorkViewer.RowCount AndAlso e.ColumnIndex <> 0 Then
            OpenSelectedItem()
        End If
    End Sub

    Private Sub OpenSelectedItem()
        Cursor = Cursors.WaitCursor

        Select Case dgvWorkViewer.Columns(0).HeaderText
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

        Cursor = Cursors.Default
    End Sub

    Private Sub SelectItemNumbers(row As Integer)
        Select Case dgvWorkViewer.Columns(0).HeaderText
            Case "AIRS #" ' Compliance facilities assigned; delinquent FCEs; facility subparts
                txtOpenFacilitySummary.AirsNumber = If(Apb.ApbFacilityId.IsValidAirsNumberFormat(dgvWorkViewer(0, row).Value.ToString), New Apb.ApbFacilityId(dgvWorkViewer(0, row).Value.ToString), Nothing)
            Case "Tracking #" ' Compliance work
                txtOpenSscpItem.Text = dgvWorkViewer(0, row).FormattedValue.ToString
                txtOpenFacilitySummary.AirsNumber = If(Apb.ApbFacilityId.IsValidAirsNumberFormat(dgvWorkViewer(1, row).Value.ToString), New Apb.ApbFacilityId(dgvWorkViewer(1, row).Value.ToString), Nothing)
            Case "Enforcement #" ' Enforcement
                txtOpenEnforcement.Text = dgvWorkViewer(0, row).FormattedValue.ToString
                txtOpenFacilitySummary.AirsNumber = If(Apb.ApbFacilityId.IsValidAirsNumberFormat(dgvWorkViewer(1, row).Value.ToString), New Apb.ApbFacilityId(dgvWorkViewer(1, row).Value.ToString), Nothing)
            Case "Reference #" ' ISMP Test Reports
                txtOpenTestReport.Text = dgvWorkViewer(0, row).FormattedValue.ToString
                txtOpenFacilitySummary.AirsNumber = If(Apb.ApbFacilityId.IsValidAirsNumberFormat(dgvWorkViewer(1, row).Value.ToString), New Apb.ApbFacilityId(dgvWorkViewer(1, row).Value.ToString), Nothing)
            Case "Test Log #" ' ISMP Test Notifications
                txtOpenTestLog.Text = dgvWorkViewer(0, row).FormattedValue.ToString
                txtOpenTestReport.Text = dgvWorkViewer(1, row).FormattedValue.ToString
            Case "App #" ' Permit applications
                txtOpenApplication.Text = dgvWorkViewer(0, row).FormattedValue.ToString
                txtOpenFacilitySummary.AirsNumber = If(Apb.ApbFacilityId.IsValidAirsNumberFormat(dgvWorkViewer(1, row).Value.ToString), New Apb.ApbFacilityId(dgvWorkViewer(1, row).Value.ToString), Nothing)
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
            Dim accountInfo As DataRow = AccountFormAccessLookup.Rows.Find(account)
            If accountInfo IsNot Nothing Then
                accountFormAccessString = accountInfo("FormAccess").ToString

                If accountFormAccessString.Length > 0 Then
                    Dim formAccessArray As String() = accountFormAccessString.Split({"("c, ")"c}, StringSplitOptions.RemoveEmptyEntries)

                    For Each formAccessString As String In formAccessArray
                        Dim formAccessSplit As String() = formAccessString.Split("-"c, ","c)
                        Dim formNumber As Integer = CInt(formAccessSplit(0))
                        AccountFormAccess(formNumber, 0) = formNumber.ToString()
                        For i As Integer = 1 To 4
                            AccountFormAccess(formNumber, i) = (Convert.ToInt32(AccountFormAccess(formNumber, i)) Or Convert.ToInt32(formAccessSplit(i))).ToString
                        Next
                    Next
                End If
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

    Private ReadOnly AllTheNavButtons As New Dictionary(Of NavButtonCategories, List(Of NavButton))

    Private ReadOnly AllTheNavButtonCategories As New List(Of NavButtonCategory)

#End Region

#Region " Implementation "

    Private Shared Function AccountHasAccessToForm(index As Integer) As Boolean
        Return (AccountFormAccess(index, 0) IsNot Nothing _
                AndAlso AccountFormAccess(index, 0) = index.ToString _
                AndAlso (AccountFormAccess(index, 1) = "1" OrElse AccountFormAccess(index, 2) = "1" _
                         OrElse AccountFormAccess(index, 3) = "1" OrElse AccountFormAccess(index, 4) = "1")
                         )
    End Function

    Private Sub AddNavButton(buttonText As String, formName As String, category As NavButtonCategories)
        If Not AllTheNavButtonCategories.Exists(Function(x) x.Category = category) Then
            AllTheNavButtonCategories.Add(New NavButtonCategory(category, category.ToString))
        End If

        If AllTheNavButtons.ContainsKey(category) Then
            AllTheNavButtons(category).Add(New NavButton(buttonText, formName))
        Else
            Dim navButtonList As New List(Of NavButton) From {
                New NavButton(buttonText, formName)
            }
            AllTheNavButtons.Add(category, navButtonList)
        End If

    End Sub

    Private Sub AddNavButtonIfAccountHasFormAccess(index As Integer,
                                                    buttonText As String, formName As String,
                                                    category As NavButtonCategories)
        If AccountHasAccessToForm(index) OrElse CurrentUser.HasRole(118) Then
            AddNavButton(buttonText, formName, category)
        End If
    End Sub

    Private Sub AddNavButtonIfUserHasPermission(permissionsAllowed As Integer(),
                                                 buttonText As String, formName As String,
                                                 category As NavButtonCategories)
        If CurrentUser.HasRole(permissionsAllowed) OrElse CurrentUser.HasRole(118) Then
            AddNavButton(buttonText, formName, category)
        End If
    End Sub

    Private Sub AddNavButtonIfUserHasPermission(permissionAllowed As Integer,
                                                 buttonText As String, formName As String,
                                                 category As NavButtonCategories)
        AddNavButtonIfUserHasPermission({permissionAllowed},
                                        buttonText, formName, category)
    End Sub

    Private Sub AddNavButtonIfUserCan(userCan As UserCan,
                                      buttonText As String, formName As String,
                                      category As NavButtonCategories)
        If CurrentUser.HasPermission(userCan) Then
            AddNavButton(buttonText, formName, category)
        End If
    End Sub

    Private Sub AddNavButtonCategory(category As NavButtonCategories, name As String, Optional shortname As String = Nothing)
        If CurrentUser.ProgramName = name OrElse CurrentUser.UnitName = name Then
            AllTheNavButtonCategories.Insert(0, New NavButtonCategory(category, name, shortname))
        Else
            AllTheNavButtonCategories.Add(New NavButtonCategory(category, name, shortname))
        End If
    End Sub

    Private Sub NavButton_Click(sender As Object, e As EventArgs)
        Cursor = Cursors.WaitCursor

        OpenSingleForm(CType(CType(sender, Button).Tag, NavButton).FormName)

        Cursor = Cursors.Default
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
        AnnualFees
        ApplicationFees
        DX
        MASP
        EIS
    End Enum

    Private Sub CreateNavButtonCategoriesList()
        AddNavButtonCategory(NavButtonCategories.General, "General")
        AddNavButtonCategory(NavButtonCategories.ISMP, "Industrial Source Monitoring Program", "ISMU")
        AddNavButtonCategory(NavButtonCategories.SSPP, "Stationary Source Permitting Program")
        AddNavButtonCategory(NavButtonCategories.SSCP, "Stationary Source Compliance Program")
        AddNavButtonCategory(NavButtonCategories.AnnualFees, "Financial Management Unit", "Annual Fees")
        AddNavButtonCategory(NavButtonCategories.ApplicationFees, "Financial Management Unit", "Application Fees")
        AddNavButtonCategory(NavButtonCategories.DX, "Data Exchange", "Data Exchange")
        AddNavButtonCategory(NavButtonCategories.MASP, "Mobile & Area Sources Program", "Events")
        AddNavButtonCategory(NavButtonCategories.EIS, "Emissions and Control Strategies", "EI/ES")
    End Sub

    Private Sub CreateNavButtonsList()

        ' General
        AddNavButton("Facility Summary", NameOf(IAIPFacilitySummary), NavButtonCategories.General)
        AddNavButton("Query Generator", NameOf(IAIPQueryGenerator), NavButtonCategories.General)
        AddNavButton("User Management", NameOf(IaipUserManagement), NavButtonCategories.General)
        AddNavButtonIfUserCan(UserCan.AccessGecoUserManagement, "GECO User Management", NameOf(GecoTool), NavButtonCategories.General)

        ' SSPP
        AddNavButton("Application Log", NameOf(SSPPApplicationLog), NavButtonCategories.SSPP)
        AddNavButtonIfAccountHasFormAccess(9, "Permit File Uploader", NameOf(SSPPPermitUploader), NavButtonCategories.SSPP)
        AddNavButtonIfAccountHasFormAccess(19, "Attainment Status Tool", NameOf(SSPPAttainmentStatus), NavButtonCategories.SSPP)
        AddNavButtonIfAccountHasFormAccess(23, "PA/PN Report", NameOf(SSPPPublicNoticesAndAdvisories), NavButtonCategories.SSPP)
        AddNavButtonIfAccountHasFormAccess(24, "SSPP Statistical Tools", NameOf(SSPPStatisticalTools), NavButtonCategories.SSPP)
        AddNavButtonIfUserHasPermission(120, "Title V Tools", NameOf(SSPPTitleVTools), NavButtonCategories.SSPP)

        ' SSCP
        AddNavButton("Compliance Log", NameOf(SSCPComplianceLog), NavButtonCategories.SSCP)
        AddNavButtonIfAccountHasFormAccess(22, "Compliance Management", NameOf(SSCPManagersTools), NavButtonCategories.SSCP)

        ' ISMP
        AddNavButton("Monitoring Log", NameOf(ISMPMonitoringLog), NavButtonCategories.ISMP)
        AddNavButtonIfAccountHasFormAccess(14, "Test Report Information", NameOf(ISMPTestReportAdministrative), NavButtonCategories.ISMP)
        AddNavButtonIfAccountHasFormAccess(15, "Memo Viewer", NameOf(ISMPTestMemoViewer), NavButtonCategories.ISMP)
        AddNavButtonIfAccountHasFormAccess(17, "ISMU Management", NameOf(ISMPManagersTools), NavButtonCategories.ISMP)

        ' Emission Fees
        AddNavButtonIfUserCan(UserCan.EditAnnualFees, "Fees Log", NameOf(FeesLog), NavButtonCategories.AnnualFees)
        AddNavButtonIfUserCan(UserCan.ManageAnnualFees, "Fee Management", NameOf(FeesManagement), NavButtonCategories.AnnualFees)
        AddNavButtonIfAccountHasFormAccess(12, "Statistics && Reports", NameOf(FeesStatistics), NavButtonCategories.AnnualFees)
        AddNavButtonIfUserCan(UserCan.EditAnnualFeesDeposits, "Deposits", NameOf(FeesDeposits), NavButtonCategories.AnnualFees)

        ' Application Fees
        AddNavButtonIfUserHasPermission({123, 124, 125}, "New Deposit", NameOf(FinDepositView), NavButtonCategories.ApplicationFees)
        AddNavButtonIfUserHasPermission({123, 124, 125}, "Search Deposits", NameOf(FinSearchDeposits), NavButtonCategories.ApplicationFees)
        AddNavButton("Search Invoices", NameOf(FinSearchInvoices), NavButtonCategories.ApplicationFees)
        AddNavButton("Search Facilities", NameOf(FinSearchFacilities), NavButtonCategories.ApplicationFees)
        AddNavButtonIfUserHasPermission({124, 28}, "Manage Fee Rates", NameOf(FinFeeRateManagement), NavButtonCategories.ApplicationFees)
        AddNavButton("Statistics && Reports", NameOf(FinStatistics), NavButtonCategories.ApplicationFees)

        ' MASP
        AddNavButtonIfAccountHasFormAccess(137, "EPD Events", NameOf(EventsManagement), NavButtonCategories.MASP)

        ' DMU
        AddNavButtonIfUserCan(UserCan.AccessEdtTools, "EDT Errors", NameOf(DmuEdtErrorMessages), NavButtonCategories.DX)
        AddNavButtonIfUserHasPermission(118, "District Tools", NameOf(IAIPDistrictSourceTool), NavButtonCategories.DX)
        AddNavButtonIfUserHasPermission(118, "Lookup Tables", NameOf(IAIPLookUpTables), NavButtonCategories.DX)

        ' EIS
        AddNavButtonIfUserCan(UserCan.AccessEmissionsInventory, "Emissions Inventory", NameOf(EisTool), NavButtonCategories.EIS)
        AddNavButtonIfUserCan(UserCan.AccessEmissionsInventory, "Emissions Statement", NameOf(EmissionsStatement), NavButtonCategories.EIS)

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

    Private Sub mmiUpdateProfile_Click(sender As Object, e As EventArgs) Handles mmiUpdateProfile.Click
        Using pf As New IaipUserProfile
            If pf.ShowDialog() = DialogResult.OK Then
                MessageBox.Show("Profile successfully updated.", "Success", MessageBoxButtons.OK)
            End If
        End Using
    End Sub

    Private Sub mmiChangePassword_Click(sender As Object, e As EventArgs) Handles mmiChangePassword.Click
        Using cp As New IaipChangePassword
            If cp.ShowDialog() = DialogResult.OK Then
                MessageBox.Show("Password successfully updated.", "Success", MessageBoxButtons.OK)
            End If
        End Using
    End Sub

    Private Sub mmiSecurity_Click(sender As Object, e As EventArgs) Handles mmiSecurity.Click
        OpenSingleForm(IaipUserSecurity)
    End Sub

    Private Sub mmiLogOut_Click(sender As Object, e As EventArgs) Handles mmiLogOut.Click
        If Application.OpenForms Is Nothing Then
            CloseIaip()
        ElseIf Application.OpenForms.Count > 1 Then
            MessageBox.Show("Close all open IAIP windows before logging off.", "Save your work", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ShowAllForms()
        Else
            LoggingOff = True
            ClearTimersAndBackgroundWorkers()
            LogOutUser()
            OpenSingleForm(IAIPLogIn)
            Close()
        End If
    End Sub

    Private Sub ClearTimersAndBackgroundWorkers()
        dataPreloadTimer.Enabled = False
        networkCheckTimer.Enabled = False
        bgrDataPreloader.CancelAsync()
        bgrLoadWorkViewer.CancelAsync()
        bgrOrgNotifications.CancelAsync()
        bgrUserPermissions.CancelAsync()
    End Sub

    Private Sub mmiResetForm_Click(sender As Object, e As EventArgs) Handles mmiResetForm.Click
        ResetAllFormSettings()
        Me.Location = New Point(15, 15)
        Me.Size = New Size(808, 460)
    End Sub

    Private Sub mmiAbout_Click(sender As Object, e As EventArgs) Handles mmiAbout.Click
        IaipAbout.ShowDialog()
    End Sub

    Private Sub mmiOnlineHelp_Click(sender As Object, e As EventArgs) Handles mmiOnlineHelp.Click
        OpenDocumentationUrl(Me)
    End Sub

#End Region

#Region " Org Notifications "
    Private Property CheckingOrgNotifications As Boolean

    Public Sub LoadOrgNotifications()
        If CheckingOrgNotifications OrElse bgrOrgNotifications.IsBusy Then Return

        CheckingOrgNotifications = True

        Try
            bgrOrgNotifications.RunWorkerAsync()
        Catch ex As InvalidOperationException
            If ex.Message.Contains("This BackgroundWorker is currently busy and cannot run multiple tasks concurrently.") Then
                Return
            End If
        End Try
    End Sub

    Private Sub bgrOrgNotifications_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgrOrgNotifications.DoWork
        e.Result = CheckNotificationApiAsync().Result
    End Sub

    Private Sub bgrOrgNotifications_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgrOrgNotifications.RunWorkerCompleted
        CheckingOrgNotifications = False

        Dim notifications As List(Of OrgNotificationModel) = e.Result

        If notifications Is Nothing OrElse notifications.Count <= 0 Then Return

        pnlNotificationContainer.Visible = True

        Dim first As Boolean = True
        lblNotification.Text = ""

        For Each notification As OrgNotificationModel In notifications
            If notification.Message IsNot Nothing AndAlso notification.Message.Trim().Length > 0 Then
                If Not first Then lblNotification.Text &= Environment.NewLine & Environment.NewLine
                lblNotification.Text &= notification.Message
                first = False
            End If
        Next
    End Sub

    Private Sub DismissMessageButton_Click(sender As Object, e As EventArgs) Handles DismissMessageButton.Click
        pnlNotificationContainer.Visible = False
    End Sub

    Private Sub pnlNotifications_ClientSizeChanged(sender As Object, e As EventArgs) Handles pnlNotifications.ClientSizeChanged
        lblNotification.MaximumSize = New Size(CType(sender, Control).ClientSize.Width, 20000)
    End Sub

#End Region

#Region " Pre-load shared application data "

    ' Priority data can be gradually preloaded in the background.

    Private dataPreloadTimer As Timer
    Private Sub PreloadSharedData()
        LoadSomeData()

        dataPreloadTimer = New Timer() With {.Interval = 5_000, .Enabled = True}
        AddHandler dataPreloadTimer.Tick, AddressOf LoadSomeData
    End Sub

    Private Sub LoadSomeData()
        If bgrDataPreloader.IsBusy Then Return

        Try
            bgrDataPreloader.RunWorkerAsync()
        Catch ex As InvalidOperationException
            If ex.Message.Contains("This BackgroundWorker is currently busy and cannot run multiple tasks concurrently.") Then
                Return
            End If
        End Try
    End Sub

    Private sharedDataCounter As Integer = 1
    Private Sub bgrDataPreloader_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgrDataPreloader.DoWork

        Select Case sharedDataCounter
            Case 1
                GetSharedData(SharedTable.AllComplianceStaff)
            Case 2
                GetSharedData(SharedDataSet.RuleSubparts)
            Case 3
                GetSharedData(SharedTable.Counties)
            Case 4
                GetSharedData(SharedTable.SscpNotificationTypes)
            Case 5
                dataPreloadTimer.Enabled = False
                GetSharedData(SharedTable.FacilityOwnershipTypes)

            Case Else ' Should never be hit
                dataPreloadTimer.Enabled = False
        End Select

        sharedDataCounter += 1
    End Sub

#End Region

#Region " Testing Menu click events "

    Private Sub HandledExceptionTest(sender As Object, e As EventArgs) Handles RunTest.Click
        Try
            Throw New ArgumentException("NavScreen handled exception testing")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub UnhandledExceptionTest(sender As Object, e As EventArgs) Handles RunTest2.Click
        Throw New ArgumentException("NavScreen unhandled exception testing")
    End Sub

#End Region

End Class
