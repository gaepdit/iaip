Imports Oracle.ManagedDataAccess.Client

Public Class IAIPLogIn

    Private _message As IaipMessage
    Private Property Message As IaipMessage
        Get
            Return _message
        End Get
        Set(value As IaipMessage)
            If value Is Nothing And Message IsNot Nothing Then Message.Clear()
            _message = value
            If value IsNot Nothing Then Message.Display(lblGeneralMessage)
        End Set
    End Property


#Region " Page Load "

    Private Sub IAIPLogIn_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If txtUserID.Enabled Then txtUserID.Text = GetUserSetting(UserSetting.PrefillLoginId)
        FocusLogin()
        DisplayVersion()
        monitor.TrackFeatureStop("Startup.Loading")
        If AppFirstRun Or AppUpdated Then
            App.TestCrystalReportsInstallation()
        End If
    End Sub

    Private Sub IAIPLogIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Main." & Me.Name)
        Try
            CheckLanguageRegistrySetting()

#If DEBUG Then
            ToggleServerEnvironment()
#Else
            CheckDBAvailability()
#End If

#If BETA Then
            Me.LogoBox.Image = My.Resources.Resources.BetaLogo
            lblIAIP.Text = "IAIP Beta Test"
            ToggleServerEnvironment()
#End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DisableLogin(Optional ByVal messageText As String = Nothing)
        Dim loginControls As Control() = {txtUserID, lblUserID, txtUserPassword, lblPassword, btnLoginButton}
        DisableControls(loginControls)
        Me.AcceptButton = Nothing
        Me.Message = New IaipMessage(messageText, IaipMessage.WarningLevels.Warning)
    End Sub

    Private Sub EnableLogin()
        Dim loginControls As Control() = {txtUserID, lblUserID, txtUserPassword, lblPassword, btnLoginButton}
        EnableControls(loginControls)

        Me.AcceptButton = btnLoginButton
        If Message IsNot Nothing Then Message.Clear()

        FocusLogin()
    End Sub

    Private Sub FocusLogin()
        If txtUserID.Text = "" Then
            If txtUserID.Enabled Then txtUserID.Focus()
        Else
            If txtUserPassword.Enabled Then txtUserPassword.Focus()
        End If
    End Sub

    Private Sub DisplayVersion()
        Dim currentVersion As Version = GetCurrentVersionAsMajorMinorBuild()
        Dim msgText As String
        Dim msg As IaipMessage

        If AppUpdated Then
            msgText = String.Format("The IAIP has been updated. Current version: {0}", currentVersion.ToString)
            msg = New IaipMessage(msgText, IaipMessage.WarningLevels.Info)
        Else
            msgText = String.Format("Version: {0}", currentVersion.ToString)
            msg = New IaipMessage(msgText, IaipMessage.WarningLevels.None)
        End If

#If BETA Then
        lblCurrentVersionMessage.Text = lblCurrentVersionMessage.Text & " β"
#End If

        msg.Display(lblCurrentVersionMessage)
    End Sub

    Private Sub CheckLanguageRegistrySetting()
        Dim currentSetting As String
        currentSetting = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Environment", "NLS_LANG", Nothing)
        If currentSetting Is Nothing Or currentSetting <> "AMERICAN" Then
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Environment", "NLS_LANG", "AMERICAN")
            DisableLogin("Language settings have been updated. Please close and restart the Platform.")
            DisableAndHide(mmiTestingEnvironment)
        End If
    End Sub

    Private Function CheckDBAvailability() As Boolean
        If DAL.AppIsEnabled Then
            EnableLogin()
            Return True
        Else
            DisableLogin("The IAIP is currently unavailable. Please check " &
                         "back later. If you are working remotely, you must " &
                         "connect to the VPN before using the IAIP. " &
                         "If you continue to see this message after " &
                         "two hours, please inform the Data Management Unit. " &
                         "Thank you.")
            Return False
        End If


    End Function

#End Region

#Region " Login "

    Private Sub LogInCheck()
        If Message IsNot Nothing Then Message.Clear()

        If txtUserID.Text = "" OrElse txtUserPassword.Text = "" Then
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        monitor.TrackFeatureStart("Startup.LoggingIn")

        If Not CheckDBAvailability() Then
            CancelLogin()
            Exit Sub
        End If

        CurrentUser = DAL.LoginIaipUser(txtUserID.Text.ToUpper, EncryptDecrypt.EncryptText(txtUserPassword.Text))

        If CurrentUser Is Nothing OrElse CurrentUser.StaffId = 0 Then
            Me.Message = New IaipMessage("Login information is incorrect. Please try again.", IaipMessage.WarningLevels.ErrorReport)
            CancelLogin()
            Exit Sub
        End If

        If Not CurrentUser.ActiveEmployee Then
            Me.Message = New IaipMessage("Your user status has been flagged as inactive. Please contact your manager for more information.", IaipMessage.WarningLevels.ErrorReport)
            CancelLogin()
            Exit Sub
        End If

        If CheckForRequiredPasswordUpdate(txtUserPassword.Text) Then
            CancelLogin()
            Exit Sub
        End If

        If Not ValidateUserData() Then
            CancelLogin()
            Exit Sub
        End If

        ' Add additional installation meta data for analytics
        monitorInstallationInfo.Add("IaipUserName", CurrentUser.UserName)
        monitor.SetInstallationInfo(CurrentUser.UserName, monitorInstallationInfo)
        If (CurrentServerEnvironment <> DB.DefaultServerEnvironment) Then
            monitor.TrackFeature("Main.TestingEnvironment")
        End If
        monitor.ForceSync()

        SaveUserSetting(UserSetting.PrefillLoginId, txtUserID.Text)
        OpenSingleForm(IAIPNavigation)

        Me.Close()
    End Sub

    Private Sub CancelLogin()
        monitor.TrackFeatureCancel("Startup.LoggingIn")
        CurrentUser = Nothing
        txtUserPassword.Clear()
        FocusLogin()
        Me.Cursor = Cursors.Default
    End Sub

    Private Function CheckForRequiredPasswordUpdate(currentPassword As String) As Boolean
        If Not (CurrentUser.LastName.ToUpper = currentPassword.ToUpper) Then Return False

        Dim dialog As New IaipChangePassword
        dialog.ShowDialog()
        If dialog.PasswordSuccessfullyChanged Then
            Me.Message = New IaipMessage("Password successfully changed", IaipMessage.WarningLevels.Success)
        End If
        dialog.Dispose()
        Return True
    End Function

    Private Function ValidateUserData() As Boolean

        Dim ValidUserData As Boolean = True
        Dim PhoneNumber As String = CurrentUser.PhoneNumber
        Dim EmailAddress As String = CurrentUser.EmailAddress


        'Check for valid user data
        If EmailAddress = "" Then
            ValidUserData = False
            EmailAddress = "Require"
        End If
        If PhoneNumber = "" Or PhoneNumber = "4043637000" Then
            ValidUserData = False
            PhoneNumber = "Require"
        End If
        If Not ValidUserData Then
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
        End If

        If ProfileUpdate IsNot Nothing Then
            ProfileUpdate.Close()
            ProfileUpdate = Nothing
        End If

        Return ValidUserData
    End Function

    Private Sub btnLoginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoginButton.Click
        LogInCheck()
        If ProfileUpdate IsNot Nothing Then
            ProfileUpdate.Close()
            ProfileUpdate = Nothing
        End If
    End Sub

#End Region

#Region " Database Environment "

    Private Sub ToggleServerEnvironment()
        ' Toggle mmiTestingEnvironment menu item
#If BETA Then
        mmiTestingEnvironment.Checked = True
#Else
        mmiTestingEnvironment.Checked = Not mmiTestingEnvironment.Checked
#End If

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

#If DEBUG Then
        Me.Text = APP_FRIENDLY_NAME & " — " & CurrentServerEnvironment.ToString
#End If

        ' Reset current connection based on current connection environment
        ' and check connection/app availability
        CurrentConnection = New OracleConnection(DB.CurrentConnectionString)
        CheckDBAvailability()

#If BETA Then
        Me.BackColor = Color.Snow
#End If

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

    Private Sub mmiCheckForUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCheckForUpdate.Click
        App.CheckForUpdate()
    End Sub

#End Region

End Class