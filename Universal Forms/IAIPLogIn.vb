Imports Oracle.DataAccess.Client
'Imports System.IO
'Imports Microsoft.Win32

Public Class IAIPLogIn
    Dim recExist As Boolean
    Dim IaipAvailable As Boolean = True

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
            CheckLanguageRegistrySetting()

            'AddHandler t.Elapsed, AddressOf TimerFired
            't.Enabled = True

            VerifyVersion()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DisableLogin(Optional ByVal message As String = "")
        With txtUserID
            .Enabled = False
            .Visible = False
        End With

        With lblUserID
            .Enabled = False
            .Visible = False
        End With

        With txtUserPassword
            .Enabled = False
            .Visible = False
        End With

        With lblPassword
            .Enabled = False
            .Visible = False
        End With

        With btnLoginButton
            .Enabled = False
            .Visible = False
        End With

        Me.AcceptButton = Nothing

        mmiTestingEnvironment.Enabled = False

        With lblGeneralMessage
            .Text = message
            .Visible = True
        End With
    End Sub

    Private Sub VerifyVersion()
        ' Do version checking
        Dim currentVersion As Version = GetCurrentVersion()
        Dim publishedVersion As Version = GetPublishedVersion()

        lnkUpdateLink.Visible = False

        With lblCurrentVersionMessage
            .Text = String.Format("Version: {0}", GetCurrentVersionAsBuild.ToString)
            .Visible = True
        End With

        If publishedVersion.Equals(New Version("0.0.0.0")) Then
            DisableLogin("The Platform is currently unavailable. Please check " & vbNewLine & _
                         "back later. If you continue to see this message after " & vbNewLine & _
                         "two hours, please inform the Data Management Unit. " & vbNewLine & _
                         "Thank you.")
            lblCurrentVersionMessage.Location = New Point(337, lblCurrentVersionMessage.Location.Y)
            Exit Sub
        End If

        If IsUpdateMandatory() Then
            DisableLogin("Your installation of the Platform is out of date " & vbNewLine & _
                         "and must be updated before you can proceed.")
            ShowUpdateLink(currentVersion, publishedVersion)
            With lblCurrentVersionMessage
                .Location = New Point(337, 278)
                .ForeColor = SystemColors.ControlText
            End With
            With lblAvailableVersionMessage
                .Location = New Point(337, 296)
                .ForeColor = Color.Maroon
            End With
            With lnkUpdateLink
                .Location = New Point(337, 330)
                .LinkColor = Color.Red
            End With

            Exit Sub
        End If

        If IsUpdateAvailable() Then
            ShowUpdateLink(currentVersion, publishedVersion)
            lblCurrentVersionMessage.ForeColor = SystemColors.ControlText
        End If

    End Sub

    Private Sub ShowUpdateLink(ByVal currentVersion As Version, ByVal publishedVersion As Version)
        lnkUpdateLink.Visible = True
        With lblCurrentVersionMessage
            .Text = String.Format("You are using version: {0}", currentVersion.ToString)
            .Visible = True
        End With
        With lblAvailableVersionMessage
            .Text = String.Format("Version {0} is available to install", publishedVersion.ToString)
            .Visible = True
        End With
    End Sub

    Private Sub CheckLanguageRegistrySetting()
        Dim currentSetting As String
        currentSetting = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Environment", "NLS_LANG", Nothing)
        If currentSetting Is Nothing Or currentSetting <> "AMERICAN" Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Environment", "NLS_LANG", "AMERICAN")
            DisableLogin("Language settings have been updated. Please close and restart the Platform.")
        End If
    End Sub

#End Region

#Region " Login "

    Private Sub LogInCheck()
        monitor.TrackFeatureStart("Startup.LoggingIn")
        LoginProgressBar.Visible = True

        'btnLoginButton.Visible = True
        Try
            Dim EmployeeStatus As String = ""
            Dim PhoneNumber As String = ""
            Dim EmailAddress As String = ""
            Dim ValidateLogInInfo As String = ""
            Dim LastName As String = ""

            UserGCode = ""

            LoginProgressBar.PerformStep()

            If txtUserID.Text <> "" Then
                If txtUserPassword.Text <> "" Then
                    LoginProgressBar.PerformStep()

                    Dim loginCred As LoginCred = DAL.GetLoginCred(txtUserID.Text.ToUpper, EncryptDecrypt.EncryptText(txtUserPassword.Text))

                    LoginProgressBar.PerformStep()

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

                    LoginProgressBar.PerformStep()

                    If UserGCode <> "" And EmployeeStatus = "1" Then
                        If EmailAddress = "" Then
                            ValidateLogInInfo = "Check"
                            EmailAddress = "Require"
                        End If
                        If PhoneNumber = "" Or PhoneNumber = "4043637000" Then
                            ValidateLogInInfo = "Check"
                            PhoneNumber = "Require"
                        End If
                        If LastName.ToUpper = txtUserPassword.Text.ToUpper Then
                            ValidateLogInInfo = "Check"
                            LastName = "Require"
                        End If
                        If ValidateLogInInfo = "Check" Then
                            ProfileUpdate = Nothing
                            If ProfileUpdate Is Nothing Then ProfileUpdate = New IAIPProfileUpdate
                            ProfileUpdate.Show()
                            txtUserPassword.Clear()
                            If EmailAddress = "Require" Then
                                ProfileUpdate.pnlEmailAddress.Visible = True
                                ProfileUpdate.txtEmailAddress.BackColor = Color.Tomato
                            Else
                                'ProfileUpdate.pnlEmailAddress.Visible = False
                                ProfileUpdate.txtEmailAddress.Text = EmailAddress
                            End If
                            If PhoneNumber = "Require" Then
                                ProfileUpdate.pnlPhoneNumber.Visible = True
                                ProfileUpdate.mtbPhoneNumber.BackColor = Color.Tomato
                            Else
                                'ProfileUpdate.pnlPhoneNumber.Visible = False
                                ProfileUpdate.mtbPhoneNumber.Text = PhoneNumber
                            End If
                            If LastName = "Require" Then
                                ProfileUpdate.pnlUserIDPassword.Visible = True
                                ProfileUpdate.txtUserPassword.BackColor = Color.Tomato
                                ProfileUpdate.txtConfirmPassword.BackColor = Color.Tomato

                            Else
                                ' ProfileUpdate.pnlUserIDPassword.Visible = False
                            End If


                            LoginProgressBar.Value = 0
                            LoginProgressBar.Visible = False
                            'btnLoginButton.Visible = True

                            Exit Sub
                        End If

                        If ProfileUpdate IsNot Nothing Then
                            ProfileUpdate.Close()
                            ProfileUpdate = Nothing
                        End If

                        ' Add additional installation meta data for analytics
                        monitorInstallationInfo.Add("IaipUserName", loginCred.UserName)
                        monitor.SetInstallationInfo(loginCred.UserName, monitorInstallationInfo)
                        If TestingEnvironment Then monitor.TrackFeature("Main.TestingEnvironment")

                        NavigationScreen = Nothing
                        If NavigationScreen Is Nothing Then NavigationScreen = New IAIPNavigation

                        SaveUserSetting(UserSetting.PrefillLoginId, txtUserID.Text)

                        NavigationScreen.Show()

                        LoginProgressBar.Value = 0
                        LoginProgressBar.Visible = False
                        'btnLoginButton.Visible = True
                        Me.Close()
                    Else
                        'Panel1.Text = Paneltemp1

                        If EmployeeStatus = "0" Then
                            MsgBox("You status as been flagged as inactive." & vbCrLf & "If this is in error please contact your manager.", MsgBoxStyle.Exclamation, _
    "Log In Error")
                        Else
                            MsgBox("Log In information is incorrect." & vbCrLf & "Please try again.", MsgBoxStyle.Exclamation, _
    "Log In Error")
                        End If
                        txtUserPassword.Clear()
                        txtUserPassword.Focus()

                        LoginProgressBar.Value = 0
                        LoginProgressBar.Visible = False
                        'btnLoginButton.Visible = True
                        monitor.TrackFeatureCancel("Startup.LoggingIn")
                    End If
                Else
                    LoginProgressBar.Value = 0
                    LoginProgressBar.Visible = False
                    'btnLoginButton.Visible = True
                    monitor.TrackFeatureCancel("Startup.LoggingIn")
                End If
            Else

                LoginProgressBar.Value = 0
                LoginProgressBar.Visible = False
                'btnLoginButton.Visible = True
                monitor.TrackFeatureCancel("Startup.LoggingIn")
                MsgBox("The User ID and Password provided is not a valid user combination.", MsgBoxStyle.Exclamation, _
                                 "Log In Error")
            End If

        Catch ex As Exception
            LoginProgressBar.Value = 0
            LoginProgressBar.Visible = False
            'btnLoginButton.Visible = True
            monitor.TrackFeatureCancel("Startup.LoggingIn")
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub btnLoginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoginButton.Click
        LogInCheck()
    End Sub

    Private Sub ToggleTestingEnvironment(ByVal currentlyTestingEnvironment As Boolean)
        If currentlyTestingEnvironment Then
            ' Switch to production environment
            TestingEnvironment = False

            mmiTestingEnvironment.Checked = False
            Me.BackColor = SystemColors.Control
            btnLoginButton.Text = "Log In"

            CurrentConnectionString = DB.GetConnectionString(False)
        Else
            ' Switch to testing environment
            TestingEnvironment = True

            mmiTestingEnvironment.Checked = True
            Me.BackColor = Color.PapayaWhip
            btnLoginButton.Text = "Testing Environment"

            CurrentConnectionString = DB.GetConnectionString(True)
        End If

        ' Reset current connection based on current connection string
        CurrentConnection = New OracleConnection(CurrentConnectionString)
    End Sub

#End Region

#Region " Close application "

    Private Sub CloseIaip()
        CurrentConnection.Dispose()
        Application.Exit()
    End Sub
    Private Sub Form_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If NavigationScreen Is Nothing Then
            CloseIaip()
        End If
    End Sub
    Private Sub mmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiExit.Click
        CloseIaip()
    End Sub

#End Region

#Region " Update application "

    Private Sub StartIaipUpdate()
        OpenDownloadUrl()
        CloseIaip()
    End Sub

    Private Sub mmiUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiForceUpdate.Click
        StartIaipUpdate()
    End Sub

    Private Sub UpdateLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkUpdateLink.LinkClicked
        StartIaipUpdate()
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

    Private Sub mmiTestingEnvironment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiTestingEnvironment.Click
        ToggleTestingEnvironment(mmiTestingEnvironment.Checked)
    End Sub

    Private Sub mmiRefreshUserID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiRefreshUserID.Click
        ResetUserSetting(UserSetting.PrefillLoginId)
        txtUserID.Text = ""
    End Sub

    Private Sub mmiResetAllForms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiResetAllForms.Click
        ResetAllFormSettings()
    End Sub

    Private Sub mmiOnlineHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOnlineHelp.Click
        OpenHelpUrl(Me)
    End Sub

    Private Sub mmiAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiAbout.Click
        OpenAboutUrl(Me)
    End Sub

    Private Sub IAIPLogIn_HelpButtonClicked(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        OpenAboutUrl(Me)
    End Sub

#End Region

End Class