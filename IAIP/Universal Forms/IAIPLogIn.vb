Imports Oracle.DataAccess.Client
'Imports System.IO
'Imports Microsoft.Win32

Public Class IAIPLogIn

#Region " Page Load "

    Private Sub IAIPLogIn_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If txtUserID.Enabled Then
            txtUserID.Text = GetUserSetting(UserSetting.PrefillLoginId)
            If txtUserID.Text = "" Then
                txtUserID.Focus()
            Else
                If txtUserPassword.Enabled Then txtUserPassword.Focus()
            End If
        End If
        monitor.TrackFeatureStop("Startup.Loading")
    End Sub

    Private Sub IAIPLogIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Main." & Me.Name)
        Try
            DisplayVersion()
            CheckLanguageRegistrySetting()
            CheckDBAvailability()

#If NadcTesting Then
            EnableAndShow(mmiNadcServer)
            If CurrentServerLocation = DB.ServerLocation.NADC Then
                mmiNadcServer.Checked = True
            End If
            Me.Text = APP_FRIENDLY_NAME & " — " & CurrentServerLocation.ToString & ", " & CurrentServerEnvironment.ToString
#End If

#If DEBUG Then
            ToggleServerEnvironment()
#End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DisableLogin(Optional ByVal message As String = "")
        Dim loginControls As Control() = {txtUserID, lblUserID, txtUserPassword, lblPassword, btnLoginButton}
        DisableAndHide(loginControls)

        Me.AcceptButton = Nothing
        With lblGeneralMessage
            .Text = message
            .Visible = True
        End With
    End Sub

    Private Sub EnableLogin()
        Dim loginControls As Control() = {txtUserID, lblUserID, txtUserPassword, lblPassword, btnLoginButton}
        EnableAndShow(loginControls)

        Me.AcceptButton = btnLoginButton
        With lblGeneralMessage
            .Text = ""
            .Visible = False
        End With
    End Sub

    Private Sub DisplayVersion()
        Dim currentVersion As Version = GetCurrentVersionAsMajorMinorBuild()

        With lblCurrentVersionMessage
            .Text = String.Format("Version: {0}", currentVersion.ToString)
            .Visible = True
        End With
    End Sub

    Private Sub CheckLanguageRegistrySetting()
        Dim currentSetting As String
        currentSetting = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Environment", "NLS_LANG", Nothing)
        If currentSetting Is Nothing Or currentSetting <> "AMERICAN" Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Environment", "NLS_LANG", "AMERICAN")
            DisableLogin("Language settings have been updated. Please close and restart the Platform.")
            DisableAndHide(mmiNadcServer)
            DisableAndHide(mmiTestingEnvironment)
        End If
    End Sub

    Private Sub CheckDBAvailability()
        Console.WriteLine("CurrentServerEnvironment: " & CurrentServerEnvironment.ToString)
        Console.WriteLine("CurrentServerLocation: " & CurrentServerLocation.ToString)

        If DAL.AppIsEnabled Then
            EnableLogin()
        Else
            DisableLogin("The IAIP is currently unavailable. Please check " & vbNewLine & _
                             "back later. If you continue to see this message after " & vbNewLine & _
                             "two hours, please inform the Data Management Unit. " & vbNewLine & _
                             "Thank you.")
        End If
    End Sub

#End Region

#Region " Login "

    Private Sub LogInCheck()
        If txtUserID.Text = "" OrElse txtUserPassword.Text = "" Then Exit Sub

        monitor.TrackFeatureStart("Startup.LoggingIn")

        Console.WriteLine("CurrentServerEnvironment: " & CurrentServerEnvironment.ToString)
        Console.WriteLine("CurrentServerLocation: " & CurrentServerLocation.ToString)
        If Not DAL.AppIsEnabled Then
            DisableLogin("The IAIP is currently unavailable. Please check " & vbNewLine & _
                             "back later. If you continue to see this message after " & vbNewLine & _
                             "two hours, please inform the Data Management Unit. " & vbNewLine & _
                             "Thank you.")
            txtUserPassword.Clear()
            monitor.TrackFeatureCancel("Startup.LoggingIn")
            Exit Sub
        End If

        LoginProgressBar.Visible = True
        LoginProgressBar.Refresh()

        Try

            Dim EmployeeStatus As String = ""
            Dim PhoneNumber As String = ""
            Dim EmailAddress As String = ""
            Dim InvalidUserData As Boolean = False
            Dim LastName As String = ""

            UserGCode = ""

            Dim loginCred As LoginCred = DAL.GetLoginCred(txtUserID.Text.ToUpper, EncryptDecrypt.EncryptText(txtUserPassword.Text))

            If loginCred Is Nothing Then
                MsgBox("Login information is incorrect." & vbCrLf & "Please try again.", MsgBoxStyle.Exclamation, "Login Error")
                txtUserPassword.Clear()
                txtUserPassword.Focus()
                LoginProgressBar.Visible = False
                monitor.TrackFeatureCancel("Startup.LoggingIn")
                Exit Sub
            End If

            UserGCode = loginCred.Staff.StaffId
            Permissions = loginCred.PermissionsString
            If Permissions = "" Then Permissions = "(0)"
            UserName = loginCred.Staff.AlphaName
            If UserName = "" Then UserName = " "
            UserBranch = loginCred.Staff.BranchID.ToString
            If UserBranch = "0" OrElse UserBranch = "" Then UserBranch = "---"
            UserProgram = loginCred.Staff.ProgramID.ToString
            If UserProgram = "0" OrElse UserProgram = "" Then UserProgram = "---"
            UserUnit = loginCred.Staff.UnitId.ToString
            If UserUnit = "0" OrElse UserUnit = "" Then UserUnit = "---"
            EmployeeStatus = If(loginCred.Staff.ActiveStatus, "1", "0")
            PhoneNumber = loginCred.Staff.Phone
            EmailAddress = loginCred.Staff.Email
            LastName = loginCred.Staff.LastName

            If EmployeeStatus = "0" Then
                MsgBox("You status has been flagged as inactive." & vbCrLf & "Please contact your manager for more information.", MsgBoxStyle.Exclamation, "Login Error")
                txtUserPassword.Clear()
                LoginProgressBar.Visible = False
                monitor.TrackFeatureCancel("Startup.LoggingIn")
                Exit Sub
            End If

            If UserGCode = "" Then
                MsgBox("Login information is incorrect." & vbCrLf & "Please try again.", MsgBoxStyle.Exclamation, "Login Error")
                txtUserPassword.Clear()
                txtUserPassword.Focus()
                LoginProgressBar.Visible = False
                monitor.TrackFeatureCancel("Startup.LoggingIn")
                Exit Sub
            End If

            'Check for valid user data
            If EmailAddress = "" Then
                InvalidUserData = True
                EmailAddress = "Require"
            End If
            If PhoneNumber = "" Or PhoneNumber = "4043637000" Then
                InvalidUserData = True
                PhoneNumber = "Require"
            End If
            If LastName.ToUpper = txtUserPassword.Text.ToUpper Then
                InvalidUserData = True
                LastName = "Require"
            End If
            If InvalidUserData Then
                ProfileUpdate = Nothing
                If ProfileUpdate Is Nothing Then ProfileUpdate = New IAIPProfileUpdate
                ProfileUpdate.Show()
                txtUserPassword.Clear()
                If EmailAddress = "Require" Then
                    ProfileUpdate.pnlEmailAddress.Visible = True
                    ProfileUpdate.txtEmailAddress.BackColor = Color.Tomato
                Else
                    ProfileUpdate.txtEmailAddress.Text = EmailAddress
                End If
                If PhoneNumber = "Require" Then
                    ProfileUpdate.pnlPhoneNumber.Visible = True
                    ProfileUpdate.mtbPhoneNumber.BackColor = Color.Tomato
                Else
                    ProfileUpdate.mtbPhoneNumber.Text = PhoneNumber
                End If
                If LastName = "Require" Then
                    ProfileUpdate.pnlUserIDPassword.Visible = True
                    ProfileUpdate.txtUserPassword.BackColor = Color.Tomato
                    ProfileUpdate.txtConfirmPassword.BackColor = Color.Tomato
                End If

                LoginProgressBar.Visible = False
                monitor.TrackFeatureCancel("Startup.LoggingIn")
                Exit Sub
            End If

            If ProfileUpdate IsNot Nothing Then
                ProfileUpdate.Close()
                ProfileUpdate = Nothing
            End If

            ' Add additional installation meta data for analytics
            monitorInstallationInfo.Add("IaipUserName", loginCred.UserName)
            monitor.SetInstallationInfo(loginCred.UserName, monitorInstallationInfo)
            If (CurrentServerEnvironment <> DB.DefaultServerEnvironment OrElse _
                 CurrentServerLocation <> DB.DefaultServerLocation) Then
                monitor.TrackFeature("Main.TestingEnvironment")
            End If
            monitor.ForceSync()

            SaveUserSetting(UserSetting.PrefillLoginId, txtUserID.Text)
            OpenSingleForm(IAIPNavigation)

            Me.Close()

        Catch ex As Exception
            LoginProgressBar.Visible = False
            monitor.TrackFeatureCancel("Startup.LoggingIn")
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub btnLoginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoginButton.Click
        LogInCheck()
    End Sub

#End Region

#Region " Database Environment "

    Private Sub ToggleServerEnvironment()
        ' Toggle mmiTestingEnvironment menu item
        mmiTestingEnvironment.Checked = Not mmiTestingEnvironment.Checked

        If mmiTestingEnvironment.Checked Then
            ' Switch to DEV environment
            CurrentServerEnvironment = DB.ServerEnvironment.DEV
            Me.BackColor = Color.PapayaWhip
            btnLoginButton.Text = "Testing Environment"
        Else
            ' Switch to PRD environment
            CurrentServerEnvironment = DB.ServerEnvironment.PRD
            Me.BackColor = SystemColors.Control
            btnLoginButton.Text = "Log In"
        End If

        Me.Text = APP_FRIENDLY_NAME & " — " & CurrentServerLocation.ToString & " " & CurrentServerEnvironment.ToString

        ' Reset current connection based on current connection environment
        ' and check connection/app availability
        CurrentConnection = New OracleConnection(DB.CurrentConnectionString)
        CheckDBAvailability()
    End Sub

    Private Sub ToggleServerLocation()
        ' Toggle mmiNadcServer menu item
        mmiNadcServer.Checked = Not mmiNadcServer.Checked

        If mmiNadcServer.Checked Then
            'Switch to NADC servers
            CurrentServerLocation = DB.ServerLocation.NADC
        Else
            'Switch to Legacy servers
            CurrentServerLocation = DB.ServerLocation.Legacy
        End If

        Me.Text = APP_FRIENDLY_NAME & " — " & CurrentServerLocation.ToString & " " & CurrentServerEnvironment.ToString

        ' Reset current connection based on current connection environment
        ' and check connection/app availability
        CurrentConnection = New OracleConnection(DB.CurrentConnectionString)
        CheckDBAvailability()
    End Sub

#End Region

#Region " Close application "

    Private Sub Form_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Not SingleFormIsOpen(IAIPNavigation) Then
            StartupShutdown.CloseIaip()
        End If
    End Sub
    Private Sub mmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiExit.Click
        StartupShutdown.CloseIaip()
    End Sub

#End Region

#Region " Form usability "

    Private Sub lblUserID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblUserID.Click
        txtUserID.Focus()
    End Sub

    Private Sub lblPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblPassword.Click
        txtUserPassword.Focus()
    End Sub

#End Region

#Region " Menu items "

    Private Sub mmiRefreshUserID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiRefreshUserID.Click
        ResetUserSetting(UserSetting.PrefillLoginId)
        txtUserID.Text = ""
    End Sub

    Private Sub mmiResetAllForms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiResetAllForms.Click
        ResetAllFormSettings()
    End Sub

    Private Sub mmiOnlineHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOnlineHelp.Click
        OpenDocumentationUrl(Me)
    End Sub

    Private Sub mmiAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiAbout.Click
        IaipAbout.ShowDialog()
    End Sub

    Private Sub IAIPLogIn_HelpButtonClicked(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        OpenSupportUrl(Me)
    End Sub

    Private Sub mmiTestingEnvironment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiTestingEnvironment.Click
        ToggleServerEnvironment()
    End Sub

    Private Sub mmiNadcServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiNadcServer.Click
        ToggleServerLocation()
    End Sub

    Private Sub mmiCheckForUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCheckForUpdate.Click
        App.CheckForUpdate()
    End Sub

#End Region

End Class