Imports Oracle.DataAccess.Client
Imports System.IO
Imports Microsoft.Win32

Public Class IAIPLogIn
    'Dim Paneltemp1 As String
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim DefaultsText As String = ""
    Dim versionCheck As String = ""
    Dim IaipFolder As String = Application.StartupPath
    Dim IaipAvailable As Boolean = True

    Private Sub IAIPLogIn_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If txtUserID.Enabled Then txtUserID.Focus()
        If txtUserID.Text <> "" Then
            If txtUserPassword.Enabled Then txtUserPassword.Focus()
        End If

        monitor.TrackFeatureStop("Startup.Loading")
    End Sub
    Private Sub Splash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Main." & Me.Name)
        Try
            AddHandler t.Elapsed, AddressOf TimerFired
            t.Enabled = True
            'Panel3.Text = OracleDate

            CheckLanguageRegistrySetting()

            FindLogIn()

            If File.Exists(IaipFolder & "\Oracle.DataAccess.dll") Then
                Dim version As FileVersionInfo = FileVersionInfo.GetVersionInfo(IaipFolder & "\Oracle.DataAccess.dll")
                'If File.Exists("C:\APB\Oracle.DataAccess.dll") Then
                'Dim version As FileVersionInfo = FileVersionInfo.GetVersionInfo("C:\APB\Oracle.DataAccess.dll")
                Oracledll = version.FileVersion.ToString
                'If Oracledll = "2.111.6.20" Or Oracledll = "4.112.30" Then
                If Oracledll <> "9.2.0.401" Then
                    PrdConnString = "Data Source = luke.dnr.state.ga.us:1521/PRD; User ID = AIRBranch_App_User; " & _
                        "Password = " & SimpleCrypt("������") & ";"
                    DevConnString = "Data Source = leia.dnr.state.ga.us:1521/DEV; User ID = AirBranch; " & _
                        "Password = " & SimpleCrypt("���������") & ";"
                    Conn = New OracleConnection(PrdConnString)

                    PRDCRLogIn = "AIRBranch_App_User/" & SimpleCrypt("������") & "@//luke.dnr.state.ga.us:1521/PRD"
                    PRDCRPassWord = ""
                    'TESTCRLogIn = "AIRBranch_App_User/" & SimpleCrypt("���������Ұ�") & "@//leia.dnr.state.ga.us:1521/TEST"
                    'TESTCRPassWord = ""
                    DEVCRLogIn = "airbranch/" & SimpleCrypt("���������") & "@//leia.dnr.state.ga.us:1521/DEV"
                    DEVCRPassWord = ""

                    CRLogIn = PRDCRLogIn
                    CRPassWord = PRDCRPassWord
                End If
            End If

            Dim DefaultsText As String = ""
            temp = "SSCPProfile-313000000200000-eliforPPCSS"
            If File.Exists("C:\APB\Defaults.txt") Then
                Dim reader As StreamReader = New StreamReader("C:\APB\Defaults.txt")
                Do
                    DefaultsText = DefaultsText & reader.ReadLine
                Loop Until reader.Peek = -1
                reader.Close()

                'If DefaultsText.IndexOf("SSCPProfile-") <> -1 Then
                '    Profile_Code = Mid(DefaultsText, ((DefaultsText.IndexOf("SSCPProfile-")) + 13), ((DefaultsText.IndexOf("-eliforPPCSS")) - (DefaultsText.IndexOf("SSCPProfile-") + 12)))
                'Else
                '    Profile_Code = ""
                'End If
            Else
                'Profile_Code = ""
            End If

            VerifyVersion()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Page Load Functions"
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
            .Text = String.Format("Version: {0}", currentVersion.ToString)
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
    Sub FindLogIn()
        Try

            If File.Exists("C:\APB\Defaults.txt") Then
                Dim reader As StreamReader = New StreamReader("C:\APB\Defaults.txt")
                Do
                    DefaultsText = DefaultsText & reader.ReadLine
                Loop Until reader.Peek = -1
                reader.Close()

                If DefaultsText <> "" Then
                    If DefaultsText.IndexOf("LogInID-") <> -1 Then
                        txtUserID.Text = Mid(DefaultsText, ((DefaultsText.IndexOf("LogInID-")) + 9), ((DefaultsText.IndexOf("-DInIgoL")) - (DefaultsText.IndexOf("LogInID-") + 8)))
                    Else
                        DefaultsText = "LogInID-" & txtUserID.Text & "-DInIgoL"
                    End If
                    If DefaultsText.IndexOf("StartLocation-") <> -1 Then
                        temp = Mid(DefaultsText, ((DefaultsText.IndexOf("StartLocation-")) + 15), ((DefaultsText.IndexOf("-noitacoLtratS")) - (DefaultsText.IndexOf("StartLocation-") + 14)))
                    Else
                        DefaultsText = "StartLocation-" & temp & "-noitacoLtratS-"
                    End If
                End If
            Else

            End If

            If temp <> "" Then
                If IsNumeric(Mid(temp, 2, temp.IndexOf(",") - 1)) Then
                    DefaultX = Mid(temp, 2, temp.IndexOf(",") - 1)
                    If DefaultX < -1 Then
                        DefaultX = 0
                    End If
                Else
                    DefaultX = 0
                End If
                If IsNumeric(Mid(temp, temp.IndexOf(",") + 2, ((temp.IndexOf(")")) - (temp.IndexOf(","))) - 1)) Then
                    DefaultY = Mid(temp, temp.IndexOf(",") + 2, ((temp.IndexOf(")")) - (temp.IndexOf(","))) - 1)
                    If DefaultY < -1 Then
                        DefaultY = 0
                    End If
                Else
                    DefaultY = 0
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

#Region "Subs and Functions"
    Sub LogInCheck()
        monitor.TrackFeatureStart("Startup.LoggingIn")
        LoginProgressBar.Visible = True
        btnLoginButton.Visible = True
        Try
            Dim EmployeeStatus As String = ""
            Dim PhoneNumber As String = ""
            Dim EmailAddress As String = ""
            Dim ValidateLogInInfo As String = ""
            'Dim FirstName As String = ""
            Dim LastName As String = ""

            If versionCheck = "Update" Then
                LoginProgressBar.Value = 0
                LoginProgressBar.Visible = False
                btnLoginButton.Visible = True
                monitor.TrackFeatureCancel("Startup.LoggingIn")
                MessageBox.Show("This version of the platform is out of date and must be updated.")
                Exit Sub
            End If

            LoginProgressBar.PerformStep()
            'Paneltemp1 = Panel1.Text
            'Panel1.Text = "Logging In"
            If txtUserID.Text <> "" Then
                If txtUserPassword.Text <> "" Then
                    LoginProgressBar.PerformStep()
                    SQL = "Select " & DBNameSpace & ".EPDUsers.numUserID, " & _
                    "strIAIPPermissions, " & _
                    "(strLastName|| ', ' ||strFirstName) as UserName, " & _
                    "numBranch, numProgram, numUnit, " & _
                    "numEmployeeStatus, strPhone,  " & _
                    "strEmailAddress, strFirstName, " & _
                    "strLastName " & _
                    "from " & DBNameSpace & ".EPDUsers, " & DBNameSpace & ".IAIPPermissions, " & _
                    "" & DBNameSpace & ".EPDUserProfiles " & _
                    "where " & DBNameSpace & ".EPDUsers.numUserID = " & DBNameSpace & ".IAIPPermissions.numUserID " & _
                    "and " & DBNameSpace & ".EPDUsers.numUserID = " & DBNameSpace & ".EPDUserProfiles.numUserId " & _
                    "and upper(strUserName) = '" & Replace(txtUserID.Text.ToUpper, "'", "''") & "' " & _
                    "and strPassword = '" & Replace(EncryptDecrypt.EncryptText(txtUserPassword.Text), "'", "''") & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    LoginProgressBar.PerformStep()
                    dr = cmd.ExecuteReader
                    LoginProgressBar.PerformStep()
                    UserGCode = ""

                    While dr.Read
                        UserGCode = dr.Item("numUserId")
                        If IsDBNull(dr.Item("strIAIPPermissions")) Then
                            Permissions = "(0)"
                        Else
                            Permissions = dr.Item("strIAIPPermissions")
                        End If
                        If IsDBNull(dr.Item("UserName")) Then
                            UserName = " "
                        Else
                            UserName = dr.Item("UserName")
                        End If
                        If IsDBNull(dr.Item("numBranch")) Then
                            UserBranch = "---"
                        Else
                            UserBranch = dr.Item("numBranch")
                        End If
                        If IsDBNull(dr.Item("numProgram")) Then
                            UserProgram = "---"
                        Else
                            UserProgram = dr.Item("numProgram")
                        End If
                        If IsDBNull(dr.Item("numUnit")) Then
                            UserUnit = "---"
                        Else
                            UserUnit = dr.Item("numUnit")
                        End If
                        If IsDBNull(dr.Item("numEmployeeStatus")) Then
                            EmployeeStatus = "0"
                        Else
                            EmployeeStatus = dr.Item("numEmployeeStatus")
                        End If
                        If IsDBNull(dr.Item("strPhone")) Then
                            PhoneNumber = ""
                        Else
                            PhoneNumber = dr.Item("strPhone")
                        End If
                        If IsDBNull(dr.Item("strEmailAddress")) Then
                            EmailAddress = ""
                        Else
                            EmailAddress = dr.Item("strEmailAddress")
                        End If
                        'If IsDBNull(dr.Item("strFirstName")) Then
                        '    FirstName = ""
                        'Else
                        '    FirstName = dr.Item("strFirstName")
                        'End If
                        If IsDBNull(dr.Item("strLastName")) Then
                            LastName = ""
                        Else
                            LastName = dr.Item("strLastName")
                        End If
                    End While
                    dr.Close()
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
                            btnLoginButton.Visible = True

                            Exit Sub
                        End If

                        If ProfileUpdate Is Nothing Then
                        Else
                            ProfileUpdate.Close()
                            ProfileUpdate = Nothing
                        End If

                        ' Add additional installation meta data for analytics
                        Dim useridname As String = txtUserID.Text
                        ' TODO: Once a workable "User" object is set up, use userID from that instead
                        monitorInstallationInfo.Add("IaipUserName", useridname)
                        monitor.SetInstallationInfo(useridname, monitorInstallationInfo)

                        NavigationScreen = Nothing
                        If NavigationScreen Is Nothing Then NavigationScreen = New IAIPNavigation

                        If File.Exists("C:\APB\Defaults.txt") Then
                        Else
                            DefaultsText = "LogInID-" & txtUserID.Text & "-DInIgoL"
                            LoginProgressBar.PerformStep()
                            Dim fs As New System.IO.FileStream("C:\APB\Defaults.txt", IO.FileMode.Create, IO.FileAccess.Write)

                            fs.Close()
                            LoginProgressBar.PerformStep()
                            Dim writer As StreamWriter = New StreamWriter("C:\APB\Defaults.txt")
                            writer.WriteLine(DefaultsText)
                            writer.Close()
                            LoginProgressBar.PerformStep()
                        End If
                        If Me.mmiTestingEnvironment.Checked Or mmiTestingDatabase.Checked Or mmiLukeEnvironment.Checked Then
                            If Me.mmiTestingEnvironment.Checked Then
                                NavigationScreen.pnl4.Text = "TESTING ENVIRONMENT"
                                NavigationScreen.pnl4.BackColor = Color.Tomato
                            End If
                            If mmiTestingDatabase.Checked Then
                                NavigationScreen.pnl4.Text = "TESTING ENVIRONMENT"
                                NavigationScreen.pnl4.BackColor = Color.Blue
                            End If
                            If mmiLukeEnvironment.Checked Then
                                NavigationScreen.pnl4.Text = "TESTING ENVIRONMENT"
                                NavigationScreen.pnl4.BackColor = Color.Black
                            End If
                        Else
                            NavigationScreen.pnl4.Text = ""
                        End If
                        NavigationScreen.Show()

                        LoginProgressBar.Value = 0
                        LoginProgressBar.Visible = False
                        btnLoginButton.Visible = True
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
                        btnLoginButton.Visible = True
                        monitor.TrackFeatureCancel("Startup.LoggingIn")
                    End If
                Else
                    LoginProgressBar.Value = 0
                    LoginProgressBar.Visible = False
                    btnLoginButton.Visible = True
                    monitor.TrackFeatureCancel("Startup.LoggingIn")
                End If
            Else

                LoginProgressBar.Value = 0
                LoginProgressBar.Visible = False
                btnLoginButton.Visible = True
                monitor.TrackFeatureCancel("Startup.LoggingIn")
                MsgBox("The User ID and Password provided is not a valid user combination.", MsgBoxStyle.Exclamation, _
                                 "Log In Error")
            End If

        Catch ex As Exception
            LoginProgressBar.Value = 0
            LoginProgressBar.Visible = False
            btnLoginButton.Visible = True
            monitor.TrackFeatureCancel("Startup.LoggingIn")
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#End Region
    Private Sub btnLoginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoginButton.Click
        Try
            LogInCheck()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    'Private Sub Splash_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    '    If APB110 Is Nothing Then
    '        Conn.Dispose()
    '        End
    '    Else
    '    End If
    'End Sub
    Private Sub CloseIaip()
        Conn.Dispose()
        Application.Exit()
    End Sub
    Private Sub Form_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If NavigationScreen Is Nothing Then
            CloseIaip()
        End If
    End Sub
    Private Sub MmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiExit.Click
        CloseIaip()
    End Sub
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            LogInCheck()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#Region "Update IAIP"
    Private Sub StartIaipUpdate()
        OpenDownloadUrl()
        CloseIaip()
    End Sub
#End Region
#Region "Mouse Actions"
    'Private Sub txtUserID_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.MouseHover
    '    Try

    '        Paneltemp1 = Panel1.Text
    '        Panel1.Text = "Enter your Log In ID..."
    '        ToolTip1.SetToolTip(txtUserID, "Enter your Log In ID...")
    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    End Try

    'End Sub
    'Private Sub txtUserID_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.MouseLeave
    '    Try

    '        Panel1.Text = "Enter Your User ID..."
    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    End Try

    'End Sub
    'Private Sub txtUserPassword_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserPassword.MouseHover
    '    Try

    '        Paneltemp1 = Panel1.Text
    '        Panel1.Text = "Enter your Password if your Log In ID is correct..."
    '        ToolTip1.SetToolTip(txtUserPassword, "Enter your Password if your Log In ID is correct...")
    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    End Try

    'End Sub
    'Private Sub txtUserPassword_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserPassword.MouseLeave
    '    Try

    '        Panel1.Text = "Enter Your User ID..."
    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    End Try

    'End Sub
    'Private Sub btnEnter_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoginButton.MouseHover
    '    Try

    '        Paneltemp1 = Panel1.Text
    '        Panel1.Text = "If all information is correct enter main console..."
    '        ToolTip1.SetToolTip(btnLoginButton, "If all information is correct enter main console...")
    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    End Try

    'End Sub
    'Private Sub btnEnter_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoginButton.MouseLeave
    '    Try

    '        Panel1.Text = "Enter Your User ID..."
    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    End Try

    'End Sub
#End Region
    'Private Sub txtUserID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.LostFocus
    '    Try

    '        If txtUserID.Text <> "" Then
    '            Panel2.Text = txtUserID.Text
    '        End If
    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    End Try

    'End Sub
    'Private Sub Splash_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    '    Try

    '        If File.Exists("C:\APB\Defaults.txt") Then
    '            Panel1.Text = "Enter your Password....."
    '            txtUserPassword.Focus()
    '        Else
    '            txtUserID.Focus()
    '        End If
    '        'Panel2.Text = txtUserID.Text
    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally

    '    End Try

    'End Sub
    Private Sub LoginForm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
        Handles txtUserPassword.KeyPress, txtUserID.KeyPress

        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            LogInCheck()
        End If
    End Sub
    'Private Sub txtUserID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserID.KeyPress
    '    Try

    '        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
    '            LogInCheck()
    '        End If
    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally

    '    End Try

    'End Sub
    'Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    '    Conn.Dispose()
    '    End
    'End Sub
    Private Sub mmiTestingEnvior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiTestingEnvironment.Click
        mmiTestingDatabase.Checked = False
        mmiLukeEnvironment.Checked = False
        If mmiTestingEnvironment.Checked = False Then
            mmiTestingEnvironment.Checked = True
            TestingEnvironment = True

            'txtUserID.BackColor = Color.Tomato
            'txtUserPassword.BackColor = Color.Tomato
            'btnLoginButton.BackColor = Color.Tomato
            Me.BackColor = Color.PapayaWhip
            btnLoginButton.Text = "Testing Environment"

            Conn = New OracleConnection(DevConnString)
            CRLogIn = DEVCRLogIn
            CRPassWord = DEVCRPassWord
            CurrentConnString = DevConnString
        Else
            mmiTestingEnvironment.Checked = False
            TestingEnvironment = False

            'txtUserID.BackColor = Color.White
            'txtUserPassword.BackColor = Color.White
            'btnLoginButton.BackColor = Color.White
            Me.BackColor = SystemColors.Control
            btnLoginButton.Text = "Log In"

            Conn = New OracleConnection(PrdConnString)
            CRLogIn = PRDCRLogIn
            CRPassWord = PRDCRPassWord
            CurrentConnString = PrdConnString
        End If
    End Sub
    'Private Sub mmiTestingDatabase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mmiTestingDatabase.Click
    '    'mmiTestingEnvior.Checked = False
    '    'mmiLukeEnviornment.Checked = False
    '    'If mmiTestingDatabase.Checked = False Then
    '    '    mmiTestingDatabase.Checked = True
    '    '    txtUserID.BackColor = Color.Blue
    '    '    txtUserPassword.BackColor = Color.Blue
    '    '    btnEnter.BackColor = Color.Blue
    '    '    DBNameSpace = "AIRBRANCH"
    '    '    conn = New OracleConnection(TESTconnLine)
    '    '    CRLogIn = TESTCRLogIn
    '    '    CRPassWord = TESTCRPassWord
    '    'Else
    '    '    mmiTestingDatabase.Checked = False
    '    '    txtUserID.BackColor = Color.White
    '    '    txtUserPassword.BackColor = Color.White
    '    '    btnEnter.BackColor = Color.White
    '    '    DBNameSpace = "AIRBranch"
    '    '    conn = New OracleConnection(PRDconnLine)
    '    '    CRLogIn = PRDCRLogIn
    '    '    CRPassWord = PRDCRPassWord
    '    'End If
    'End Sub
    'Private Sub mmiLukeEnviornment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiLukeEnviornment.Click
    '    mmiTestingEnvior.Checked = False
    '    mmiTestingDatabase.Checked = False
    '    If mmiLukeEnviornment.Checked = False Then
    '        mmiLukeEnviornment.Checked = True
    '        txtUserID.BackColor = Color.Black
    '        txtUserPassword.BackColor = Color.Black
    '        btnLoginButton.BackColor = Color.Bisque
    '        Conn = New OracleConnection(PrdConnString)
    '        CRLogIn = PRDCRLogIn
    '        CRPassWord = PRDCRPassWord
    '        CurrentConnString = DevConnString
    '    Else
    '        mmiLukeEnviornment.Checked = False
    '        txtUserID.BackColor = Color.White
    '        txtUserPassword.BackColor = Color.White
    '        btnLoginButton.BackColor = Color.White
    '        Conn = New OracleConnection(PrdConnString)
    '        CRLogIn = PRDCRLogIn
    '        CRPassWord = PRDCRPassWord
    '        CurrentConnString = PrdConnString
    '    End If

    'End Sub
    'Private Sub IaipPatchLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles IaipPatchLink.LinkClicked
    '    Try
    '        Dim Result As DialogResult
    '        Dim URL As String = ""

    '        Result = MessageBox.Show("If you are not an Administrator User on this machine click 'Cancel'." & vbCrLf & _
    '                   "If you are not sure contact the Data Management Unit.", "IAIP Patch", _
    '                   MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    '        Select Case Result
    '            Case Windows.Forms.DialogResult.OK
    '                URL = "http://airpermit.dnr.state.ga.us/iaip/iaipPatch.exe"
    '                System.Diagnostics.Process.Start(URL)
    '                Conn.Dispose()
    '                End
    '            Case Windows.Forms.DialogResult.Cancel

    '            Case Else

    '        End Select

    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally

    '    End Try
    'End Sub
    Private Sub mmiRefreshUserID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiRefreshUserID.Click
        Try
            DefaultsText = ""
            temp = "LogInID-" & txtUserID.Text & "-DInIgoL"
            If File.Exists("C:\APB\Defaults.txt") Then
                Dim reader As StreamReader = New StreamReader("C:\APB\Defaults.txt")
                Do
                    DefaultsText = DefaultsText & reader.ReadLine
                Loop Until reader.Peek = -1
                reader.Close()

                If DefaultsText.IndexOf("LogInID-") <> -1 Then
                    DefaultsText = DefaultsText.Replace(Mid(DefaultsText, ((DefaultsText.IndexOf("LogInID-")) + 1), (DefaultsText.IndexOf("-DInIgoL")) + 8), temp)
                Else
                    DefaultsText = temp & vbCrLf & DefaultsText
                End If

                Dim fs As New System.IO.FileStream("C:\APB\Defaults.txt", IO.FileMode.OpenOrCreate, FileAccess.Write)
                fs.Close()
                Dim writer As StreamWriter = New StreamWriter("C:\APB\Defaults.txt")
                writer.WriteLine(DefaultsText)
                writer.Close()
            Else
                DefaultsText = temp
                Dim fs As New System.IO.FileStream("C:\APB\Defaults.txt", IO.FileMode.Create, IO.FileAccess.Write)
                fs.Close()
                Dim writer As StreamWriter = New StreamWriter("C:\APB\Defaults.txt")
                writer.WriteLine(DefaultsText)
                writer.Close()
            End If



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub mmiOnlineHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOnlineHelp.Click
        OpenHelpUrl(Me)
    End Sub
    Private Sub mmiUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiForceUpdate.Click
        StartIaipUpdate()
    End Sub
    Private Sub mmiRefreshDefaultLoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiRefreshDefaultLoc.Click
        Try
            DefaultsText = ""

            If File.Exists("C:\APB\Defaults.txt") Then
                Dim reader As StreamReader = New StreamReader("C:\APB\Defaults.txt")
                Do
                    DefaultsText = DefaultsText & reader.ReadLine
                Loop Until reader.Peek = -1
                reader.Close()
            End If

            temp = "StartLocation-(0,0)-noitacoLtratS"
            If DefaultsText.IndexOf("StartLocation-") <> -1 Then
                DefaultsText = DefaultsText.Replace((Mid(DefaultsText, ((DefaultsText.IndexOf("StartLocation-")) + 1), (DefaultsText.IndexOf("-noitacoLtratS")) - 8)), temp)
            Else
                DefaultsText = DefaultsText & vbCrLf & temp
            End If

            Dim fs As New System.IO.FileStream("C:\APB\Defaults.txt", IO.FileMode.Create, IO.FileAccess.Write)
            fs.Close()
            Dim writer As StreamWriter = New StreamWriter("C:\APB\Defaults.txt")
            writer.WriteLine(DefaultsText)
            writer.Close()
            DefaultX = 0
            DefaultY = 0
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub



    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdjustIntranet.Click
    '    Try

    '        Dim readValue As String
    '        '  conn = New OracleConnection(PRDconnLine)
    '        AddHandler t.Elapsed, AddressOf TimerFired
    '        t.Enabled = True

    '        readValue = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\dnr-tpfs4", "file", Nothing)

    '        '\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\DOMAINNAME
    '        If readValue Is Nothing Or readValue <> "1" Then
    '            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\dnr-tpfs4", "file", "1", RegistryValueKind.DWord)
    '            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\dnr-tpfs4", "*", "1", RegistryValueKind.DWord)
    '        End If

    '        readValue = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\dnr-tpfs5", "file", Nothing)

    '        '\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\DOMAINNAME
    '        If readValue Is Nothing Or readValue <> "1" Then
    '            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\dnr-tpfs5", "file", "1", RegistryValueKind.DWord)
    '        End If

    '        Exit Sub

    '    Catch ex As Exception

    '    End Try
    'End Sub



    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEIS.Click
    '    Try
    '        ' Exit Sub

    '        If Conn.State = ConnectionState.Closed Then
    '            Conn.Open()
    '        End If

    '        cmd = New OracleCommand("AIRBranch.PD_EIS_Process", Conn)
    '        cmd.CommandType = CommandType.StoredProcedure

    '        cmd.Parameters.Add(New OracleParameter("FACILITYID", OracleDbType.Varchar2)).Value = "03900001"
    '        cmd.Parameters.Add(New OracleParameter("PROCID", OracleDbType.Varchar2)).Value = "1"
    '        cmd.Parameters.Add(New OracleParameter("EMISSUNITID", OracleDbType.Varchar2)).Value = "500A"
    '        cmd.Parameters.Add(New OracleParameter("INVENTORYYEAR", OracleDbType.Varchar2)).Value = "2011"
    '        cmd.Parameters.Add(New OracleParameter("USERUPDATER", OracleDbType.Varchar2)).Value = "217-John Doe"

    '        cmd.ExecuteNonQuery()


    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteEIS.Click
    '    Try

    '        SQL = "Delete airbranch.EIS_ProcessRPTPeriodSCP " & _
    '        "where FacilitySiteID = '03900001' " & _
    '        "and intInventoryYEar = '2011' " & _
    '        "and ProcessID = '1' " & _
    '        "and EmissionsUnitID = '500A' "

    '        cmd = New OracleCommand(SQL, Conn)
    '        If Conn.State = ConnectionState.Closed Then
    '            Conn.Open()
    '        End If
    '        dr = cmd.ExecuteReader


    '        SQL = "Delete airbranch.EIS_ReportingPeriodEmissions " & _
    '        "where FacilitySiteID = '03900001' " & _
    '        "and intInventoryYEar = '2011' " & _
    '        "and ProcessID = '1' " & _
    '        "and EmissionsUnitID = '500A' "

    '        cmd = New OracleCommand(SQL, Conn)
    '        If Conn.State = ConnectionState.Closed Then
    '            Conn.Open()
    '        End If
    '        dr = cmd.ExecuteReader

    '        SQL = "Delete airbranch.EIS_ProcessOperatingdetails " & _
    '       "where FacilitySiteID = '03900001' " & _
    '       " and intInventoryYEar = '2011' " & _
    '       "and ProcessID = '1' " & _
    '       "and EmissionsUnitID = '500A' "

    '        cmd = New OracleCommand(SQL, Conn)
    '        If Conn.State = ConnectionState.Closed Then
    '            Conn.Open()
    '        End If
    '        dr = cmd.ExecuteReader

    '        SQL = "Delete airbranch.EIS_ProcessReportingPeriod " & _
    '        "where FacilitySiteID = '03900001' " & _
    '        "and intInventoryYEar = '2011' " & _
    '        "and ProcessID = '1' " & _
    '        "and EmissionsUnitID = '500A' "

    '        cmd = New OracleCommand(SQL, Conn)
    '        If Conn.State = ConnectionState.Closed Then
    '            Conn.Open()
    '        End If
    '        dr = cmd.ExecuteReader

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub UpdateLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkUpdateLink.LinkClicked
        StartIaipUpdate()
    End Sub

    Private Sub lblUserID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblUserID.Click
        txtUserID.Focus()
    End Sub

    Private Sub lblPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblPassword.Click
        txtUserPassword.Focus()
    End Sub

    Private Sub mmiAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiAbout.Click
        OpenAboutUrl(Me)
    End Sub

    Private Sub IAIPLogIn_HelpButtonClicked(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        OpenAboutUrl(Me)
    End Sub
End Class