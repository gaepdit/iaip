Imports Oracle.DataAccess.Client

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
            CheckDatabaseConnection()

#If NadcEnabled Then
            mmiNadcServer.Enabled = True
            mmiNadcServer.Visible = True
#End If

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

    Private Sub DisplayVersion()
        Dim currentVersion As Version = GetCurrentVersionAsMajorMinorBuild()

        With lblCurrentVersionMessage
            .Text = String.Format("Version: {0}", currentVersion.ToString)
            .Visible = True
        End With
    End Sub

    'Private Sub ShowUpdateLink(ByVal currentVersion As Version, ByVal publishedVersion As Version)
    '    lnkUpdateLink.Visible = True
    '    With lblCurrentVersionMessage
    '        .Text = String.Format("You are using version: {0}", currentVersion.ToString)
    '        .Visible = True
    '    End With
    '    With lblAvailableVersionMessage
    '        .Text = String.Format("Version {0} is available to install", publishedVersion.ToString)
    '        .Visible = True
    '    End With
    'End Sub

    Private Sub CheckLanguageRegistrySetting()
        Dim currentSetting As String
        currentSetting = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Environment", "NLS_LANG", Nothing)
        If currentSetting Is Nothing Or currentSetting <> "AMERICAN" Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Environment", "NLS_LANG", "AMERICAN")
            DisableLogin("Language settings have been updated. Please close and restart the Platform.")
        End If
    End Sub

    Private Sub CheckDatabaseConnection()
        If Not DB.PingDBConnection(CurrentConnection) Then
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
        LoginProgressBar.Visible = True
        LoginProgressBar.Refresh()

        Try

            Dim EmployeeStatus As String = ""
            Dim PhoneNumber As String = ""
            Dim EmailAddress As String = ""
            Dim InvalidUserData As Boolean = False
            Dim LastName As String = ""

            UserGCode = ""

            CurrentUser = DAL.GetIaipUser(txtUserID.Text.ToUpper, EncryptDecrypt.EncryptText(txtUserPassword.Text))

            If CurrentUser Is Nothing Then
                MsgBox("Login information is incorrect." & vbCrLf & "Please try again.", MsgBoxStyle.Exclamation, "Login Error")
                txtUserPassword.Clear()
                txtUserPassword.Focus()
                LoginProgressBar.Visible = False
                monitor.TrackFeatureCancel("Startup.LoggingIn")
                Exit Sub
            End If

            UserGCode = CurrentUser.Staff.StaffId
            Permissions = CurrentUser.PermissionsString
            If Permissions = "" Then Permissions = "(0)"
            UserName = CurrentUser.Staff.AlphaName
            If UserName = "" Then UserName = " "
            UserBranch = CurrentUser.Staff.BranchID.ToString
            If UserBranch = "0" OrElse UserBranch = "" Then UserBranch = "---"
            UserProgram = CurrentUser.Staff.ProgramID.ToString
            If UserProgram = "0" OrElse UserProgram = "" Then UserProgram = "---"
            UserUnit = CurrentUser.Staff.UnitId.ToString
            If UserUnit = "0" OrElse UserUnit = "" Then UserUnit = "---"
            EmployeeStatus = If(CurrentUser.Staff.ActiveStatus, "1", "0")
            PhoneNumber = CurrentUser.Staff.Phone
            EmailAddress = CurrentUser.Staff.Email
            LastName = CurrentUser.Staff.LastName

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
            monitorInstallationInfo.Add("IaipUserName", CurrentUser.UserName)
            monitor.SetInstallationInfo(CurrentUser.UserName, monitorInstallationInfo)

            If CurrentConnectionEnvironment = DB.ConnectionEnvironment.Development _
            OrElse CurrentConnectionEnvironment = DB.ConnectionEnvironment.NADC_Development _
            Then monitor.TrackFeature("Main.TestingEnvironment")
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

#If NadcEnabled Then

    Private Sub ToggleTestingEnvironment()
        mmiTestingEnvironment.Checked = Not mmiTestingEnvironment.Checked

        If mmiTestingEnvironment.Checked Then
            ' Switch to testing environment
            DevelopmentEnvironment = True
            Me.BackColor = Color.PapayaWhip
            btnLoginButton.Text = "Testing Environment"
            CurrentConnectionEnvironment = DB.ConnectionEnvironment.Development

            If NadcServer Then
                btnLoginButton.Text = "Testing (NADC)"
                CurrentConnectionEnvironment = DB.ConnectionEnvironment.NADC_Development
            End If

        Else
            ' Switch to production environment
            DevelopmentEnvironment = False
            Me.BackColor = SystemColors.Control
            btnLoginButton.Text = "Log In"
            CurrentConnectionEnvironment = DB.ConnectionEnvironment.Production

            If NadcServer Then
                btnLoginButton.Text = "Log In (NADC)"
                CurrentConnectionEnvironment = DB.ConnectionEnvironment.NADC_Production
            End If
        End If

        ' Reset current connection based on current connection environment
        CurrentConnectionString = DB.GetConnectionString(CurrentConnectionEnvironment)
        CurrentConnection = New OracleConnection(CurrentConnectionString)
    End Sub

    Private Sub ToggleDataCenter()
        mmiNadcServer.Checked = Not mmiNadcServer.Checked

        If mmiNadcServer.Checked Then
            'Switch to NADC servers
            NadcServer = True
            btnLoginButton.BackColor = Color.DarkOrange
            If DevelopmentEnvironment Then
                btnLoginButton.Text = "Testing (NADC)"
                CurrentConnectionEnvironment = DB.ConnectionEnvironment.NADC_Development
            Else
                btnLoginButton.Text = "Log In (NADC)"
                CurrentConnectionEnvironment = DB.ConnectionEnvironment.NADC_Production
            End If
        Else
            'Switch to Luke/Leia servers
            NadcServer = False
            btnLoginButton.BackColor = System.Drawing.SystemColors.Control
            If DevelopmentEnvironment Then
                btnLoginButton.Text = "Testing Environment"
                CurrentConnectionEnvironment = DB.ConnectionEnvironment.Development
            Else
                btnLoginButton.Text = "Log In"
                CurrentConnectionEnvironment = DB.ConnectionEnvironment.Production
            End If
        End If

        ' Reset current connection based on current connection environment
        CurrentConnectionString = DB.GetConnectionString(CurrentConnectionEnvironment)
        CurrentConnection = New OracleConnection(CurrentConnectionString)

    End Sub

#Else

    Private Sub ToggleTestingEnvironment()
        mmiTestingEnvironment.Checked = Not mmiTestingEnvironment.Checked

        If mmiTestingEnvironment.Checked Then
            ' Switch to testing environment
            DevelopmentEnvironment = True
            Me.BackColor = Color.PapayaWhip
            btnLoginButton.Text = "Testing Environment"
            CurrentConnectionEnvironment = DB.ConnectionEnvironment.Development
        Else
            ' Switch to production environment
            DevelopmentEnvironment = False
            Me.BackColor = SystemColors.Control
            btnLoginButton.Text = "Log In"
            CurrentConnectionEnvironment = DB.ConnectionEnvironment.Production
        End If

        ' Reset current connection based on current connection environment
        CurrentConnectionString = DB.GetConnectionString(CurrentConnectionEnvironment)
        CurrentConnection = New OracleConnection(CurrentConnectionString)
    End Sub

#End If

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
        'OpenAboutUrl(Me)
        IaipAbout.ShowDialog()
    End Sub

    Private Sub IAIPLogIn_HelpButtonClicked(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        OpenSupportUrl(Me)
    End Sub

    Private Sub mmiTestingEnvironment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiTestingEnvironment.Click
        ToggleTestingEnvironment()
    End Sub

#If NadcEnabled Then
    Private Sub mmiNadcServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiNadcServer.Click
        ToggleDataCenter()
    End Sub
#End If

#End Region

    '#Region "Update application"

    '    Private Sub StartIaipUpdate()
    '        OpenDownloadUrl()
    '        CloseIaip()
    '    End Sub

    '    Private Sub mmiUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiForceUpdate.Click
    '        StartIaipUpdate()
    '    End Sub

    '    Private Sub UpdateLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkUpdateLink.LinkClicked
    '        StartIaipUpdate()
    '    End Sub

    '#End Region

    Private Sub mmiCheckForUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCheckForUpdate.Click
        App.CheckForUpdate()
    End Sub
End Class