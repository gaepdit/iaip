Imports System.Deployment.Application
Imports System.Threading
Imports System.Threading.Tasks
Imports Iaip.UrlHelpers

Public Class IAIPLogIn

#Region " Properties "

    Private ReadOnly synchronizationContext As WindowsFormsSynchronizationContext
    Private _message As IaipMessage

    Private Property Message As IaipMessage
        Get
            Return _message
        End Get
        Set(value As IaipMessage)
            If value Is Nothing AndAlso Message IsNot Nothing Then Message.Clear()
            _message = value
            If value IsNot Nothing Then Message.Display(lblGeneralMessage)
        End Set
    End Property

#End Region

#Region " Page Load "

    Public Sub New()
        InitializeComponent()
        synchronizationContext = CType(Threading.SynchronizationContext.Current, WindowsFormsSynchronizationContext)
    End Sub


    Private Sub IAIPLogIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillLoginForm()
        SetUpUi()
    End Sub

    Private Sub IAIPLogIn_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        CheckConnectionStatusAsync()
    End Sub

    Private Sub SetUpUi()
        UseDbServerEnvironmentBranding()
        DisplayVersion()
        CheckForPasswordResetRequest()
        DisableLogin("Connecting...", True)
    End Sub

    Private Sub FillLoginForm()
        If txtUserID.Enabled Then
            txtUserID.Text = GetUserSetting(UserSetting.PrefillLoginId)
        End If
    End Sub

    Private Async Sub CheckConnectionStatusAsync()
        Dim sessionLogin As Boolean = False
        NetworkStatus = Await GetIaipNetworkStatusAsync().ConfigureAwait(False)

        If NetworkStatus = IaipNetworkStatus.Enabled Then
            sessionLogin = Await CheckUserSavedSessionAsync().ConfigureAwait(False)
        End If

        synchronizationContext.Post(
            New SendOrPostCallback(
            Sub()
                SetUpLoginUi(sessionLogin)
            End Sub
            ), sessionLogin)
    End Sub

    Private Sub SetUpLoginUi(sessionLogin As Boolean)
        If sessionLogin Then
            LogInAlready()
        End If

        Select Case NetworkStatus
            Case IaipNetworkStatus.NoInternet
                DisableLogin("It appears you are not connected to the Internet. " &
                             "Please check your connection and try again.")
            Case IaipNetworkStatus.NoVpn
                DisableLogin("It appears you are not connected to the VPN. " &
                             "If you are working remotely, you must " &
                             "connect to the VPN before using the IAIP.")
            Case IaipNetworkStatus.NoDb
                DisableLogin("Unable to connect to the database. " &
                             "Please wait a few minutes and try again. " &
                             "If still unable to connect, please contact EPD-IT for support.")
            Case IaipNetworkStatus.AppDisabled
                DisableLogin("The IAIP is temporarily down for maintenance. " &
                             "Please wait a few minutes and try again or " &
                             "contact EPD-IT for more information.")
            Case Else
                EnableLogin()
        End Select
    End Sub

    Private Shared Async Function CheckUserSavedSessionAsync() As Task(Of Boolean)
        Dim userId As Integer = Await ValidateSessionAsync().ConfigureAwait(False)

        If userId <= 0 Then
            Return False
        End If

        CurrentUser = Await Task.Run(
            Function()
                Return DAL.GetIaipUserByUserId(userId.ToString)
            End Function
            ).ConfigureAwait(False)

        If CurrentUser Is Nothing OrElse CurrentUser.RequirePasswordChange OrElse Not ValidateUserData() Then
            CurrentUser = Nothing
            Return False
        End If

        Return True
    End Function

    Private Sub DisableLogin(Optional messageText As String = Nothing, Optional waiting As Boolean = False)
        DisableControls({txtUserID, lblUserID, txtUserPassword, lblPassword, btnLoginButton, chkRemember})

        If waiting Then
            Message = New IaipMessage(messageText, IaipMessage.WarningLevels.Info)
            RetryButton.Visible = False
            AcceptButton = Nothing
        Else
            Message = New IaipMessage(messageText, IaipMessage.WarningLevels.Warning)
            RetryButton.Visible = True
            AcceptButton = RetryButton
            RetryButton.Select()
        End If
    End Sub

    Private Sub EnableLogin()
        EnableControls({txtUserID, lblUserID, txtUserPassword, lblPassword, btnLoginButton, chkRemember})
        AcceptButton = btnLoginButton
        If Message IsNot Nothing Then Message.Clear()
        RetryButton.Visible = False
        FocusLogin()
    End Sub

    Private Sub FocusLogin()
        If String.IsNullOrEmpty(txtUserID.Text) Then
            If txtUserID.Enabled Then txtUserID.Select()
        Else
            If txtUserPassword.Enabled Then txtUserPassword.Select()
        End If
    End Sub

    Private Sub DisplayVersion()
        Dim currentVersion As Version
        Dim msgText As String
        Dim msg As IaipMessage

        currentVersion = GetCurrentVersionAsMajorMinorBuild()

        If ApplicationDeployment.IsNetworkDeployed AndAlso ApplicationDeployment.CurrentDeployment.IsFirstRun Then
            msgText = $"The IAIP has been updated.{vbNewLine}Current version:  {currentVersion}"
            msg = New IaipMessage(msgText, IaipMessage.WarningLevels.Info)
            lnkChangelog.Visible = True
            lnkChangelog.BackColor = IaipColors.InfoBackColor
        Else
            msgText = $"Version: {currentVersion}"
            msg = New IaipMessage(msgText, IaipMessage.WarningLevels.None)
        End If

#If UAT Then
        msg.MessageText &= " UAT"
#End If

        msg.Display(lblCurrentVersionMessage)
    End Sub

    Private Sub CheckForPasswordResetRequest()
        Dim prr As String = GetUserSetting(UserSetting.PasswordResetRequestedDate)

        If Not String.IsNullOrEmpty(prr) Then
            Dim prrd As Date = Date.ParseExact(prr, DateParseExactFormat, Nothing)
            If Date.Compare(prrd, Date.Now.AddHours(-24)) > 0 Then
                mmiPasswordReset.Visible = True
            Else
                ResetUserSetting(UserSetting.PasswordResetRequestedDate)
            End If
        End If
    End Sub

    Private Sub RetryButton_Click(sender As Object, e As EventArgs) Handles RetryButton.Click
        DisableLogin("Connecting...", True)
        CheckConnectionStatusAsync()
    End Sub

#End Region

#Region " Login "

    Private Sub LogInCheck()
        Cursor = Cursors.WaitCursor
        If Message IsNot Nothing Then Message.Clear()
        ForgotPasswordLink.Visible = False
        ForgotUsernameLink.Visible = False

        If String.IsNullOrEmpty(txtUserID.Text) OrElse String.IsNullOrEmpty(txtUserPassword.Text) Then
            CancelLogin(True)
        Else
            Dim authenticationResult As DAL.IaipAuthenticationResult = DAL.AuthenticateIaipUser(txtUserID.Text, txtUserPassword.Text)

            Select Case authenticationResult
                Case DAL.IaipAuthenticationResult.InvalidUsername
                    Message = New IaipMessage("That Username does not exist.", IaipMessage.WarningLevels.ErrorReport)
                    ForgotUsernameLink.Visible = True
                    CancelLogin(True)

                Case DAL.IaipAuthenticationResult.InactiveUser
                    Message = New IaipMessage("Your user status has been flagged as inactive.", IaipMessage.WarningLevels.ErrorReport)
                    CancelLogin(True)

                Case DAL.IaipAuthenticationResult.InvalidLogin
                    Message = New IaipMessage("Login information is incorrect.", IaipMessage.WarningLevels.ErrorReport)
                    ForgotPasswordLink.Visible = True
                    CancelLogin(False)

                Case DAL.IaipAuthenticationResult.Success
                    CurrentUser = DAL.GetIaipUserByUsername(txtUserID.Text)
                    If CurrentUser Is Nothing Then
                        Message = New IaipMessage("There was a system error. Please contact support.", IaipMessage.WarningLevels.ErrorReport)
                        CancelLogin(False)
                    ElseIf CurrentUser.RequirePasswordChange Then
                        RequirePasswordUpdate()
                        CancelLogin(True)
                    ElseIf Not ValidateUserData() Then
                        Message = New IaipMessage("Your profile must be completed before you can use the IAIP.", IaipMessage.WarningLevels.Warning)
                        CancelLogin(False)
                    Else
                        UpdateSession(chkRemember.Checked)
                        LogInAlready()
                    End If

            End Select
        End If
    End Sub

    Private Sub LogInAlready()
        SaveUserSetting(UserSetting.PrefillLoginId, txtUserID.Text)
        ResetUserSetting(UserSetting.PasswordResetRequestedDate)
        OpenSingleForm(IAIPNavigation)
        DAL.LogSystemProperties(IsVpnConnected())

        Close()
    End Sub

    Private Sub CancelLogin(Optional clearPasswordField As Boolean = False)
        CurrentUser = Nothing
        If clearPasswordField Then txtUserPassword.Clear()
        FocusLogin()
        Cursor = Cursors.Default
    End Sub

    Private Sub RequirePasswordUpdate()
        Using changePassword As New IaipChangePassword
            changePassword.Message = New IaipMessage("You must change your password before you can log in.", IaipMessage.WarningLevels.Info)
            changePassword.Message.Display(changePassword.MessageDisplay)
            Dim dr As DialogResult = changePassword.ShowDialog()
            If dr = DialogResult.OK Then
                Message = New IaipMessage("Password successfully changed. Please log in with new password.", IaipMessage.WarningLevels.Success)
            Else
                Message = New IaipMessage("You must change your password before you can log in.", IaipMessage.WarningLevels.Warning)
            End If
        End Using
    End Sub

    Private Shared Function ValidateUserData() As Boolean
        ' Returns false only if profile info is incomplete and user doesn't update it
        If String.IsNullOrEmpty(CurrentUser.PhoneNumber) OrElse
            String.IsNullOrEmpty(CurrentUser.FirstName) OrElse
            String.IsNullOrEmpty(CurrentUser.LastName) OrElse
            String.IsNullOrEmpty(CurrentUser.EmailAddress) Then

            Using editProfile As New IaipUserProfile
                editProfile.Message = New IaipMessage("Your profile must be completed before you can use the IAIP.", IaipMessage.WarningLevels.Info)
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

#Region " User account tools "

    Private Sub RequestUsernameReminder()
        Cursor = Cursors.WaitCursor

        Using requestUsername As New IaipRequestUsername
            If requestUsername.ShowDialog() = DialogResult.OK Then
                Message = New IaipMessage("Check your email for username information. Please allow up to 15 minutes for delivery.", IaipMessage.WarningLevels.Info)
            End If
        End Using

        ForgotUsernameLink.Visible = False
        Cursor = Cursors.Default
    End Sub

    Private Sub RequestPasswordReset()
        Cursor = Cursors.WaitCursor

        Dim passwordResetRequested As Boolean = False
        Dim usernameSubmitted As String = ""

        Using requestPasswordReset As New IaipRequestPasswordReset
            If requestPasswordReset.ShowDialog() = DialogResult.OK Then
                ' Set things up for resetting the password with a valid token
                passwordResetRequested = True
                usernameSubmitted = requestPasswordReset.Username.Text
            End If
        End Using

        If passwordResetRequested Then
            SaveUserSetting(UserSetting.PasswordResetRequestedDate, Date.Now.ToString(DateParseExactFormat))
            mmiPasswordReset.Visible = True
            ShowPasswordResetForm(usernameSubmitted)
        End If

        ForgotPasswordLink.Visible = False
        Cursor = Cursors.Default
    End Sub

    Private Sub ShowPasswordResetForm(Optional username As String = "")
        If String.IsNullOrEmpty(username) Then
            username = GetUserSetting(UserSetting.PrefillLoginId)
        End If

        Using resetPassword As New IaipResetPassword
            resetPassword.Username.Text = username

            If resetPassword.ShowDialog() = DialogResult.OK Then
                ResetUserSetting(UserSetting.PasswordResetRequestedDate)
                mmiPasswordReset.Visible = False
                Message = New IaipMessage("Password successfully changed. Please log in with your new password.", IaipMessage.WarningLevels.Info)
            Else
                Message = New IaipMessage("Check your email for password reset information. Please allow up to 15 minutes for delivery. " &
                                                 "Or if you remember your old password, you can log in using that.", IaipMessage.WarningLevels.Info)
            End If
        End Using
    End Sub

#End Region

#Region " Database Environment "

    Private Sub UseDbServerEnvironmentBranding()
        btnLoginButton.Text = "Log In"

#If DEBUG Then
        ' Switch to DEV environment
        BackColor = Color.PapayaWhip
        Text = APP_FRIENDLY_NAME & " — DEV"
        btnLoginButton.Text = "Log in to DEV"
        LogoBox.Image = My.Resources.DevLogo
        mmiTestingMenu.Visible = True
        lblIAIP.Text = "IAIP DEV"
#ElseIf UAT Then
        ' Switch to DEV environment
        BackColor = Color.Snow
        Text = APP_FRIENDLY_NAME & " — UAT"
        btnLoginButton.Text = "Log in to UAT"
        LogoBox.Image = My.Resources.UatLogo
        lblIAIP.Text = "IAIP User Acceptance Testing (UAT)"
#End If

    End Sub

#End Region

#Region " Close application "

    Private Sub Form_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        If Not SingleFormIsOpen(IAIPNavigation) Then
            StartupShutdown.CloseIaip()
        End If
    End Sub

#End Region

#Region " Form usability "

    Private Sub lblUserID_Click(sender As Object, e As EventArgs) Handles lblUserID.Click
        txtUserID.Select()
    End Sub

    Private Sub lblPassword_Click(sender As Object, e As EventArgs) Handles lblPassword.Click
        txtUserPassword.Select()
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

    Private Sub mmiForgotUsername_Click(sender As Object, e As EventArgs) Handles mmiForgotUsername.Click
        RequestUsernameReminder()
    End Sub

    Private Sub mmiForgotPassword_Click(sender As Object, e As EventArgs) Handles mmiForgotPassword.Click
        RequestPasswordReset()
    End Sub

    Private Sub ForgotUsernameLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ForgotUsernameLink.LinkClicked
        RequestUsernameReminder()
    End Sub

    Private Sub ForgotPasswordLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ForgotPasswordLink.LinkClicked
        RequestPasswordReset()
    End Sub

    Private Sub PasswordResetMenuItem_Click(sender As Object, e As EventArgs) Handles mmiPasswordReset.Click
        ShowPasswordResetForm()
    End Sub

    Private Sub lnkChangelog_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkChangelog.LinkClicked
        OpenChangelogUrl()
    End Sub

#End Region

#Region " Exception testing "

    Private Sub mmiThrowUnhandledError_Click(sender As Object, e As EventArgs) Handles mmiThrowUnhandledError.Click
        Throw New ArgumentException("Unhandled exception testing")
    End Sub

    Private Sub mmiThrowHandledError_Click(sender As Object, e As EventArgs) Handles mmiThrowHandledError.Click
        Try
            Throw New ArgumentException("Handled exception testing")
        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

End Class