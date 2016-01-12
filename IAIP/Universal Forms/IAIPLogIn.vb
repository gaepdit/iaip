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

    Private Sub IAIPLogIn_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If txtUserID.Enabled Then txtUserID.Text = GetUserSetting(UserSetting.PrefillLoginId)
        FocusLogin()
        DisplayVersion()
        monitor.TrackFeatureStop("Startup.Loading")
        If AppFirstRun Or AppUpdated Then
            App.TestCrystalReportsInstallation()
        End If
    End Sub

    Private Sub IAIPLogIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Main." & Me.Name)
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            CheckLanguageRegistrySetting()

#If DEBUG Then
            ToggleServerEnvironment()
            TestingMenuItem.Visible = True
#Else
            CheckDBAvailability()
#End If

#If BETA Then
            Me.LogoBox.Image = My.Resources.Resources.BetaLogo
            lblIAIP.Text = "IAIP Beta Test"
            ToggleServerEnvironment()
#End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DisableLogin(Optional messageText As String = Nothing)
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
        
#If BETA Then
        currentVersion = GetCurrentVersion()
#End If

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
            CancelLogin(True)
            Exit Sub
        End If

        CurrentUser = DAL.LoginIaipUser(txtUserID.Text, EncryptDecrypt.EncryptText(txtUserPassword.Text))

        If CurrentUser Is Nothing OrElse CurrentUser.StaffId = 0 Then
            Me.Message = New IaipMessage("Login information is incorrect. Please try again.", IaipMessage.WarningLevels.ErrorReport)
            CancelLogin(False)
            Exit Sub
        End If

        If Not CurrentUser.ActiveEmployee Then
            Me.Message = New IaipMessage("Your user status has been flagged as inactive. Please contact your manager for more information.", IaipMessage.WarningLevels.ErrorReport)
            CancelLogin(True)
            Exit Sub
        End If

        If CheckForRequiredPasswordUpdate(txtUserPassword.Text) Then
            CancelLogin(True)
            Exit Sub
        End If

        If Not ValidateUserData() Then
            Me.Message = New IaipMessage("Your profile must be updated before you can log in.", IaipMessage.WarningLevels.Warning)
            CancelLogin(False)
            Exit Sub
        End If

        AddMonitorLoginData()
        SaveUserSetting(UserSetting.PrefillLoginId, txtUserID.Text)
        OpenSingleForm(IAIPNavigation)

        Me.Close()
    End Sub

    Private Sub CancelLogin(Optional clearPasswordField As Boolean = False)
        monitor.TrackFeatureCancel("Startup.LoggingIn")
        CurrentUser = Nothing
        If clearPasswordField Then txtUserPassword.Clear()
        FocusLogin()
        Me.Cursor = Cursors.Default
    End Sub

    Private Function CheckForRequiredPasswordUpdate(currentPassword As String) As Boolean
        If CurrentUser.RequirePasswordChange _
            OrElse (CurrentUser.LastName.ToUpper = currentPassword.ToUpper) Then

            Using changePassword As New IaipChangePassword
                changePassword.Message = New IaipMessage("You must change your password before you can log in.", IaipMessage.WarningLevels.Info)
                changePassword.Message.Display(changePassword.MessageDisplay)
                Dim dr As DialogResult = changePassword.ShowDialog()
                If dr = DialogResult.OK Then
                    Me.Message = New IaipMessage("Password successfully changed. Please log in with new password.", IaipMessage.WarningLevels.Success)
                Else
                    Me.Message = New IaipMessage("You must change your password before you can log in.", IaipMessage.WarningLevels.ErrorReport)
                End If
            End Using

            Return True
        End If

        Return False
    End Function

    Private Function ValidateUserData() As Boolean
        ' Returns false only if profile info is incomplete and user doesn't update it
        If CurrentUser.PhoneNumber = "" OrElse CurrentUser.FirstName = "" OrElse CurrentUser.LastName = "" _
            OrElse CurrentUser.EmailAddress = "" Then

            Using editProfile As New IaipUserProfile
                editProfile.Message = New IaipMessage("Your profile must be completed before you can log in.", IaipMessage.WarningLevels.Info)
                editProfile.Message.Display(editProfile.MessageDisplay)
                Dim dr As DialogResult = editProfile.ShowDialog()
                If dr = DialogResult.Cancel Then
                    Return False
                End If
            End Using

        ElseIf Not IsValidEmailAddress(CurrentUser.EmailAddress, True) Then

            Using editProfile As New IaipUserProfile
                editProfile.Message = New IaipMessage("Your profile must be updated with a valid DNR email address.", IaipMessage.WarningLevels.Info)
                editProfile.Message.Display(editProfile.MessageDisplay)
                Dim dr As DialogResult = editProfile.ShowDialog()
                If dr = DialogResult.Cancel Then
                    Return False
                End If
            End Using

        ElseIf CurrentUser.RequestProfileUpdate Then

            Using editProfile As New IaipUserProfile
                editProfile.Message = New IaipMessage("Please verify and confirm that your profile information is correct.", IaipMessage.WarningLevels.Info)
                editProfile.Message.Display(editProfile.MessageDisplay)
                editProfile.ShowDialog()
            End Using

        End If

        Return True
    End Function

    Private Sub btnLoginButton_Click(sender As Object, e As EventArgs) Handles btnLoginButton.Click
        LogInCheck()
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

    Private Sub Form_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        If Not SingleFormIsOpen(IAIPNavigation) Then
            StartupShutdown.CloseIaip()
        End If
    End Sub
    Private Sub mmiExit_Click(sender As Object, e As EventArgs) Handles mmiExit.Click
        StartupShutdown.CloseIaip()
    End Sub

#End Region

#Region " Form usability "

    Private Sub lblUserID_Click(sender As Object, e As EventArgs) Handles lblUserID.Click
        txtUserID.Focus()
    End Sub

    Private Sub lblPassword_Click(sender As Object, e As EventArgs) Handles lblPassword.Click
        txtUserPassword.Focus()
    End Sub

#End Region

#Region " Menu items "

    Private Sub mmiRefreshUserID_Click(sender As Object, e As EventArgs) Handles mmiRefreshUserID.Click
        ResetUserSetting(UserSetting.PrefillLoginId)
        txtUserID.Text = ""
    End Sub

    Private Sub mmiResetAllForms_Click(sender As Object, e As EventArgs) Handles mmiResetAllForms.Click
        ResetAllFormSettings()
    End Sub

    Private Sub mmiOnlineHelp_Click(sender As Object, e As EventArgs) Handles mmiOnlineHelp.Click
        OpenDocumentationUrl(Me)
    End Sub

    Private Sub mmiAbout_Click(sender As Object, e As EventArgs) Handles mmiAbout.Click
        IaipAbout.ShowDialog()
    End Sub

    Private Sub IAIPLogIn_HelpButtonClicked(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        OpenSupportUrl(Me)
    End Sub

    Private Sub mmiTestingEnvironment_Click(sender As Object, e As EventArgs) Handles mmiTestingEnvironment.Click
        ToggleServerEnvironment()
    End Sub

    Private Sub mmiCheckForUpdate_Click(sender As Object, e As EventArgs) Handles mmiCheckForUpdate.Click
        App.CheckForUpdate()
    End Sub

#End Region

    Private Sub MenuItem6_Click(sender As Object, e As EventArgs) Handles MenuItem6.Click
        Dim pf As New IaipUserProfile
        pf.ShowDialog()
    End Sub
End Class