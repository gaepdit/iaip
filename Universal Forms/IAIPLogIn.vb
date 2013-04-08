Imports System.Data.OracleClient
Imports System.IO
Imports Microsoft.Win32
'Imports System.Security
'Imports System.Security.Cryptography
'Imports System.Drawing.Drawing2D

Public Class IAIPLogIn
    Dim Paneltemp1 As String
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim DefaultsText As String = ""
    Dim versionCheck As String = ""
    Dim APBFolder As String = "C:\APB"

    Private Sub Splash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim readValue As String
            '  conn = New OracleConnection(PRDconnLine)
            AddHandler t.Elapsed, AddressOf TimerFired
            t.Enabled = True
            Panel3.Text = OracleDate
            lblVersion.Text = String.Format("Version: {0}", My.Application.Info.Version.ToString)
            FindLogIn()
            Me.Width = 800
            Me.Height = 550
            Label3.Location = New System.Drawing.Point(9, 415)
            Me.lblVersion.Location = New System.Drawing.Point(658, 415)
            readValue = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Environment", "NLS_LANG", Nothing)

            If readValue Is Nothing Or readValue <> "AMERICAN" Then
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Environment", "NLS_LANG", "AMERICAN")
                MsgBox("Please Restart the application", MsgBoxStyle.Information, "IAIP")
                End
            End If

            If File.Exists("C:\APB2\johngaltproject.exe") Then
                APBFolder = "C:\APB2"
            End If
            If File.Exists(APBFolder & "\Oracle.DataAccess.dll") Then
                Dim version As FileVersionInfo = FileVersionInfo.GetVersionInfo(APBFolder & "\Oracle.DataAccess.dll")
                'If File.Exists("C:\APB\Oracle.DataAccess.dll") Then
                'Dim version As FileVersionInfo = FileVersionInfo.GetVersionInfo("C:\APB\Oracle.DataAccess.dll")
                Oracledll = version.FileVersion.ToString
                'If Oracledll = "2.111.6.20" Or Oracledll = "4.112.30" Then
                If Oracledll <> "9.2.0.401" Then
                    PrdConnString = "Data Source = luke.dnr.state.ga.us:1521/PRD; User ID = AIRBranch_App_User; " & _
                        "Password = " & SimpleCrypt("ÁÚ·Ú±Ï") & ";"
                    DevConnString = "Data Source = leia.dnr.state.ga.us:1521/DEV; User ID = AirBranch; " & _
                        "Password = " & SimpleCrypt("ÛÌÔÁ·ÏÂÚÙ") & ";"
                    Conn = New OracleConnection(PrdConnString)

                    PRDCRLogIn = "AIRBranch_App_User/" & SimpleCrypt("ÁÚ·Ú±Ï") & "@//luke.dnr.state.ga.us:1521/PRD"
                    PRDCRPassWord = ""
                    'TESTCRLogIn = "AIRBranch_App_User/" & SimpleCrypt("¡…“¡––’”≈“∞≥") & "@//leia.dnr.state.ga.us:1521/TEST"
                    'TESTCRPassWord = ""
                    DEVCRLogIn = "airbranch/" & SimpleCrypt("ÛÌÔÁ·ÏÂÚÙ") & "@//leia.dnr.state.ga.us:1521/DEV"
                    DEVCRPassWord = ""

                    CRLogIn = PRDCRLogIn
                    CRPassWord = PRDCRPassWord
                End If
            End If

            VerifyVersion()

            If Panel1.Text = "Enter your Password....." Then
                txtUserPassword.Focus()
            Else

            End If

            'Dim Profile_Code As String = ""
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

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#Region "Page Load Functions"

    Sub VerifyVersion()
        Dim version As FileVersionInfo = FileVersionInfo.GetVersionInfo(APBFolder & "\johngaltproject.exe")
        Dim currentVersionNumber As String = ""
        Dim publishedVersionNumber As String = ""
        Dim sqlStatement As String = "Select strVersionNumber from " & DBNameSpace & ".APBMasterApp where strApplicationName = 'IAIP'"

        Try
            currentVersionNumber = version.ProductVersion.ToString

            ' DWW TO-DO: Move the above code to a "GetCurrentVersion" function
            ' DWW TO-DO: Move the following db code to a "GetPublishedVersion" function

            Using dbConn = New OracleConnection(CurrentConnString)
                Using dbCommand = New OracleCommand(sqlStatement, dbConn)
                    dbConn.Open()
                    Dim reader As OracleDataReader = dbCommand.ExecuteReader
                    'dr = cmd.ExecuteReader

                    Try
                        While reader.Read
                            If Not IsDBNull(reader.Item("strVersionNumber")) Then
                                publishedVersionNumber = reader.Item("strVersionNumber")
                            End If
                        End While
                    Catch ee As OracleException
                        Select Case ee.Code
                            Case 12560
                                MessageBox.Show("The database is unavailable.")
                                ' DWW TO-DO: DisableIAIP
                        End Select
                    End Try
                End Using
            End Using

            If publishedVersionNumber = "0.0.0.0" Then
                MsgBox("The Integrated Air Information Platform (IAIP) is currently unavailable due to maintenance." & _
                       vbCrLf & "Please check back later.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
                'DWW TO-DO: Create "DisableIAIP" Sub that removes login form and replaces with this text.
            End If

            If currentVersionNumber <> publishedVersionNumber Then
                llbUpdateIAIP.Visible = True
            Else
                llbUpdateIAIP.Visible = False
            End If

            versionCheck = ""
            If (Replace(publishedVersionNumber, ".", "") - Replace(currentVersionNumber, ".", "")) > 1 Then
                versionCheck = "Update"
                ' DWW TO-DO: Replace this code with better version checking. If IAIP is too out of date, require update:
                ' DisableIAIP, but enable update link
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

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
        Try
            Dim EmployeeStatus As String = ""
            Dim PhoneNumber As String = ""
            Dim EmailAddress As String = ""
            Dim ValidateLogInInfo As String = ""
            'Dim FirstName As String = ""
            Dim LastName As String = ""

            If versionCheck = "Update" Then
                MessageBox.Show("This version of the platform is out of date and must be updated.")
                Exit Sub
            End If

            ProgressBar.PerformStep()
            Paneltemp1 = Panel1.Text
            Panel1.Text = "Logging In"
            If txtUserID.Text <> "" Then
                If txtUserPassword.Text <> "" Then
                    ProgressBar.PerformStep()
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
                    ProgressBar.PerformStep()
                    dr = cmd.ExecuteReader
                    ProgressBar.PerformStep()
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
                    ProgressBar.PerformStep()

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


                            ProgressBar.Value = 0

                            Exit Sub
                        End If

                        If ProfileUpdate Is Nothing Then
                        Else
                            ProfileUpdate.Close()
                            ProfileUpdate = Nothing
                        End If
                        NavigationScreen = Nothing
                        If NavigationScreen Is Nothing Then NavigationScreen = New IAIPNavigation

                        If File.Exists("C:\APB\Defaults.txt") Then
                        Else
                            DefaultsText = "LogInID-" & txtUserID.Text & "-DInIgoL"
                            ProgressBar.PerformStep()
                            Dim fs As New System.IO.FileStream("C:\APB\Defaults.txt", IO.FileMode.Create, IO.FileAccess.Write)

                            fs.Close()
                            ProgressBar.PerformStep()
                            Dim writer As StreamWriter = New StreamWriter("C:\APB\Defaults.txt")
                            writer.WriteLine(DefaultsText)
                            writer.Close()
                            ProgressBar.PerformStep()
                        End If
                        If Me.mmiTestingEnvior.Checked = True Or mmiTestingDatabase.Checked = True Or mmiLukeEnviornment.Checked = True Then
                            If Me.mmiTestingEnvior.Checked = True Then
                                NavigationScreen.pnl4.Text = "TESTING ENVIRONMENT"
                                NavigationScreen.pnl4.BackColor = Color.Tomato
                            End If
                            If mmiTestingDatabase.Checked = True Then
                                NavigationScreen.pnl4.Text = "TESTING ENVIRONMENT"
                                NavigationScreen.pnl4.BackColor = Color.Blue
                            End If
                            If mmiLukeEnviornment.Checked = True Then
                                NavigationScreen.pnl4.Text = "TESTING ENVIRONMENT"
                                NavigationScreen.pnl4.BackColor = Color.Black
                            End If
                        Else
                            NavigationScreen.pnl4.Text = ""
                        End If
                        NavigationScreen.mmiVersion.Text = lblVersion.Text
                        NavigationScreen.Show()

                        ProgressBar.Value = 0
                        Me.Hide()
                    Else
                        Panel1.Text = Paneltemp1

                        If EmployeeStatus = "0" Then
                            MsgBox("You status as been flagged as inactive." & vbCrLf & "If this is in error please contact your manager.", MsgBoxStyle.Exclamation, _
    "Log In Error")
                        Else
                            MsgBox("Log In information is incorrect." & vbCrLf & "Please try again.", MsgBoxStyle.Exclamation, _
    "Log In Error")
                        End If
                        txtUserPassword.Clear()
                        txtUserPassword.Focus()

                        ProgressBar.Value = 0
                    End If
                End If
            Else

                ProgressBar.Value = 0
                MsgBox("The User ID and Password provided is not a valid user combination.", MsgBoxStyle.Exclamation, _
                                 "Log In Error")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#End Region
    Private Sub btnEnter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnter.Click
        Try
            LogInCheck()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub Splash_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If APB110 Is Nothing Then
            Conn.Dispose()
            End
        Else
        End If
    End Sub
    Private Sub Splash_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If NavigationScreen Is Nothing Then
            Conn.Dispose()
            End
        Else

        End If
    End Sub
    Private Sub MmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiExit.Click
        End
    End Sub
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiLogIn.Click
        Try

            LogInCheck()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#Region ".EXE Update"
    Private Sub llbUpdateIAIP_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbUpdateIAIP.LinkClicked
        Try
            Dim URL As String = ""

            URL = "http://airpermit.dnr.state.ga.us/iaip/iaip.exe"
            System.Diagnostics.Process.Start(URL)
            Conn.Dispose()
            End
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "Mouse Actions"
    Private Sub txtUserID_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.MouseHover
        Try

            Paneltemp1 = Panel1.Text
            Panel1.Text = "Enter your Log In ID..."
            ToolTip1.SetToolTip(txtUserID, "Enter your Log In ID...")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub txtUserID_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.MouseLeave
        Try

            Panel1.Text = "Enter Your User ID..."
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub txtUserPassword_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserPassword.MouseHover
        Try

            Paneltemp1 = Panel1.Text
            Panel1.Text = "Enter your Password if your Log In ID is correct..."
            ToolTip1.SetToolTip(txtUserPassword, "Enter your Password if your Log In ID is correct...")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub txtUserPassword_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserPassword.MouseLeave
        Try

            Panel1.Text = "Enter Your User ID..."
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnEnter_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnter.MouseHover
        Try

            Paneltemp1 = Panel1.Text
            Panel1.Text = "If all information is correct enter main console..."
            ToolTip1.SetToolTip(btnEnter, "If all information is correct enter main console...")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnEnter_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnter.MouseLeave
        Try

            Panel1.Text = "Enter Your User ID..."
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#End Region
    Private Sub txtUserID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.LostFocus
        Try

            If txtUserID.Text <> "" Then
                Panel2.Text = txtUserID.Text
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub Splash_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try

            If File.Exists("C:\APB\Defaults.txt") Then
                Panel1.Text = "Enter your Password....."
                txtUserPassword.Focus()
            Else
                txtUserID.Focus()
            End If
            Panel2.Text = txtUserID.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtUserPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserPassword.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                LogInCheck()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtUserID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserID.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                LogInCheck()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
        Conn.Dispose()
        End
    End Sub
    Private Sub mmiTestingEnvior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiTestingEnvior.Click
        mmiTestingDatabase.Checked = False
        mmiLukeEnviornment.Checked = False
        If mmiTestingEnvior.Checked = False Then
            mmiTestingEnvior.Checked = True
            txtUserID.BackColor = Color.Tomato
            txtUserPassword.BackColor = Color.Tomato
            btnEnter.BackColor = Color.Tomato
            Conn = New OracleConnection(DevConnString)
            CRLogIn = DEVCRLogIn
            CRPassWord = DEVCRPassWord
            CurrentConnString = DevConnString
        Else
            mmiTestingEnvior.Checked = False
            txtUserID.BackColor = Color.White
            txtUserPassword.BackColor = Color.White
            btnEnter.BackColor = Color.White
            Conn = New OracleConnection(PrdConnString)
            CRLogIn = PRDCRLogIn
            CRPassWord = PRDCRPassWord
            CurrentConnString = PrdConnString
        End If
    End Sub
    Private Sub mmiTestingDatabase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mmiTestingDatabase.Click
        'mmiTestingEnvior.Checked = False
        'mmiLukeEnviornment.Checked = False
        'If mmiTestingDatabase.Checked = False Then
        '    mmiTestingDatabase.Checked = True
        '    txtUserID.BackColor = Color.Blue
        '    txtUserPassword.BackColor = Color.Blue
        '    btnEnter.BackColor = Color.Blue
        '    DBNameSpace = "AIRBRANCH"
        '    conn = New OracleConnection(TESTconnLine)
        '    CRLogIn = TESTCRLogIn
        '    CRPassWord = TESTCRPassWord
        'Else
        '    mmiTestingDatabase.Checked = False
        '    txtUserID.BackColor = Color.White
        '    txtUserPassword.BackColor = Color.White
        '    btnEnter.BackColor = Color.White
        '    DBNameSpace = "AIRBranch"
        '    conn = New OracleConnection(PRDconnLine)
        '    CRLogIn = PRDCRLogIn
        '    CRPassWord = PRDCRPassWord
        'End If
    End Sub
    Private Sub mmiLukeEnviornment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiLukeEnviornment.Click
        mmiTestingEnvior.Checked = False
        mmiTestingDatabase.Checked = False
        If mmiLukeEnviornment.Checked = False Then
            mmiLukeEnviornment.Checked = True
            txtUserID.BackColor = Color.Black
            txtUserPassword.BackColor = Color.Black
            btnEnter.BackColor = Color.Bisque
            Conn = New OracleConnection(PrdConnString)
            CRLogIn = PRDCRLogIn
            CRPassWord = PRDCRPassWord
            CurrentConnString = DevConnString
        Else
            mmiLukeEnviornment.Checked = False
            txtUserID.BackColor = Color.White
            txtUserPassword.BackColor = Color.White
            btnEnter.BackColor = Color.White
            Conn = New OracleConnection(PrdConnString)
            CRLogIn = PRDCRLogIn
            CRPassWord = PRDCRPassWord
            CurrentConnString = PrdConnString
        End If

    End Sub
    Private Sub llbIAIPPatch_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbIAIPPatch.LinkClicked
        Try
            Dim Result As DialogResult
            Dim URL As String = ""

            Result = MessageBox.Show("If you are not an Administrator User on this machine click 'Cancel'." & vbCrLf & _
                       "If you are not sure contact the Data Management Unit.", "IAIP Patch", _
                       MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            Select Case Result
                Case Windows.Forms.DialogResult.OK
                    URL = "http://airpermit.dnr.state.ga.us/iaip/iaipPatch.exe"
                    System.Diagnostics.Process.Start(URL)
                    Conn.Dispose()
                    End
                Case Windows.Forms.DialogResult.Cancel

                Case Else

            End Select

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
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
    Private Sub mmiOnlineHelp_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOnlineHelp.Click
        Try
            'Help.ShowHelp(Label1, HELP_URL)
            Help.ShowHelp(Label1, HELP_URL)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub mmiUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiUpdate.Click
        Try
            Dim URL As String = ""
            URL = "http://airpermit.dnr.state.ga.us/iaip/iaip.exe"
            System.Diagnostics.Process.Start(URL)
            Conn.Dispose()
            End
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
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



    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try

            Dim readValue As String
            '  conn = New OracleConnection(PRDconnLine)
            AddHandler t.Elapsed, AddressOf TimerFired
            t.Enabled = True

            readValue = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\dnr-tpfs4", "file", Nothing)

            '\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\DOMAINNAME
            If readValue Is Nothing Or readValue <> "1" Then
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\dnr-tpfs4", "file", "1", RegistryValueKind.DWord)
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\dnr-tpfs4", "*", "1", RegistryValueKind.DWord)
            End If

            readValue = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\dnr-tpfs5", "file", Nothing)

            '\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\DOMAINNAME
            If readValue Is Nothing Or readValue <> "1" Then
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\dnr-tpfs5", "file", "1", RegistryValueKind.DWord)
            End If



            Exit Sub









        Catch ex As Exception

        End Try
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            ' Exit Sub

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            cmd = New OracleCommand("AIRBranch.PD_EIS_Process", Conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("FACILITYID", OracleType.VarChar)).Value = "03900001"
            cmd.Parameters.Add(New OracleParameter("PROCID", OracleType.VarChar)).Value = "1"
            cmd.Parameters.Add(New OracleParameter("EMISSUNITID", OracleType.VarChar)).Value = "500A"
            cmd.Parameters.Add(New OracleParameter("INVENTORYYEAR", OracleType.Number)).Value = "2011"
            cmd.Parameters.Add(New OracleParameter("USERUPDATER", OracleType.VarChar)).Value = "217-John Doe"

            cmd.ExecuteNonQuery()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try

            SQL = "Delete airbranch.EIS_ProcessRPTPeriodSCP " & _
            "where FacilitySiteID = '03900001' " & _
            "and intInventoryYEar = '2011' " & _
            "and ProcessID = '1' " & _
            "and EmissionsUnitID = '500A' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader


            SQL = "Delete airbranch.EIS_ReportingPeriodEmissions " & _
            "where FacilitySiteID = '03900001' " & _
            "and intInventoryYEar = '2011' " & _
            "and ProcessID = '1' " & _
            "and EmissionsUnitID = '500A' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader

            SQL = "Delete airbranch.EIS_ProcessOperatingdetails " & _
           "where FacilitySiteID = '03900001' " & _
           " and intInventoryYEar = '2011' " & _
           "and ProcessID = '1' " & _
           "and EmissionsUnitID = '500A' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader

            SQL = "Delete airbranch.EIS_ProcessReportingPeriod " & _
            "where FacilitySiteID = '03900001' " & _
            "and intInventoryYEar = '2011' " & _
            "and ProcessID = '1' " & _
            "and EmissionsUnitID = '500A' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader

        Catch ex As Exception

        End Try
    End Sub


End Class