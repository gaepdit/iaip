Imports Oracle.DataAccess.Client


Public Class IAIPEditHeaderData
    Dim SQL As String
    Dim SQL2 As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim dsHeaderData As DataSet
    Dim daHeaderData As OracleDataAdapter
    Dim daHeaderData2 As OracleDataAdapter

    Private Sub IAIPEditHeaderData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            Panel1.Text = "Select a Function..."
            Panel2.Text = UserName
            Panel3.Text = OracleDate

            LoadComboBoxes()
            DTPStartUpDate.Text = OracleDate
            DTPShutdown.Text = OracleDate

            If txtAirsNumber.Text <> "" Then
                LoadFacilityHeaderData()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#Region "Page Load"
    Sub LoadComboBoxes()
        Try

            cboClassification.Items.Add(" ")
            cboClassification.Items.Add("A")
            cboClassification.Items.Add("B")
            cboClassification.Items.Add("SM")
            cboClassification.Items.Add("PR")
            cboClassification.Items.Add("C")

            cboOperationalStatus.Items.Add(" ")
            cboOperationalStatus.Items.Add("O - Operational")
            cboOperationalStatus.Items.Add("P - Planned")
            cboOperationalStatus.Items.Add("C - Under Construction")
            cboOperationalStatus.Items.Add("T - Temporarily Closed")
            cboOperationalStatus.Items.Add("X - Closed/Dismantled")
            cboOperationalStatus.Items.Add("I - Seasonal Operation")

            cboOneHour.Items.Add(" ")
            cboOneHour.Items.Add("Yes")
            cboOneHour.Items.Add("Contribute")
            cboOneHour.Items.Add("No")

            cboEightHour.Items.Add(" ")
            cboEightHour.Items.Add("Atlanta")
            cboEightHour.Items.Add("Macon")
            cboEightHour.Items.Add("No")

            cboPMFine.Items.Add(" ")
            cboPMFine.Items.Add("Atlanta")
            cboPMFine.Items.Add("Chattanooga")
            cboPMFine.Items.Add("Floyd")
            cboPMFine.Items.Add("Macon")
            cboPMFine.Items.Add("No")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadFacilityHeaderData()
        Dim temp As String = ""
        Dim ModifingPerson As String
        Dim ModifingDate As String
        Dim ModifingLocation As String

        Try
            SQL = "Select * " & _
            "From " & DBNameSpace & ".VW_APBFacilityHeader " & _
            "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "

            SQL2 = "Select * " & _
            "from " & DBNameSpace & ".VW_HB_APBHeaderData " & _
            "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' " & _
            "Order by strKey DESC "

            dsHeaderData = New DataSet
            daHeaderData = New OracleDataAdapter(SQL, CurrentConnection)
            daHeaderData2 = New OracleDataAdapter(SQL2, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daHeaderData.Fill(dsHeaderData, "Current")
            daHeaderData2.Fill(dsHeaderData, "Historical")

            temp = dsHeaderData.Tables("Current").Rows(0).Item(1).ToString
            Select Case temp
                Case "O"
                    cboOperationalStatus.Text = "O - Operational"
                Case "P"
                    cboOperationalStatus.Text = "P - Planned"
                Case "C"
                    cboOperationalStatus.Text = "C - Under Construction"
                Case "T"
                    cboOperationalStatus.Text = "T - Temporarily Closed"
                Case "X"
                    cboOperationalStatus.Text = "X - Closed/Dismantled"
                Case "I"
                    cboOperationalStatus.Text = "I - Seasonal Operation"
                Case Else
                    cboOperationalStatus.Text = "Unknown - Please Fix"
            End Select
            cboClassification.Text = dsHeaderData.Tables("Current").Rows(0).Item(2).ToString
            txtSICCode.Text = dsHeaderData.Tables("Current").Rows(0).Item(4).ToString
            txtComments.Text = dsHeaderData.Tables("Current").Rows(0).Item(13).ToString
            ModifingPerson = dsHeaderData.Tables("Current").Rows(0).Item(7).ToString
            ModifingDate = dsHeaderData.Tables("Current").Rows(0).Item(12).ToString
            ModifingLocation = dsHeaderData.Tables("Current").Rows(0).Item(16).ToString
            txtNAICSCode.Text = dsHeaderData.Tables("Current").Rows(0).Item(17).ToString

            txtModifingComments.Text = "Modified on " & ModifingDate & " by " & ModifingPerson & " from " & ModifingLocation

            temp = dsHeaderData.Tables("Current").Rows(0).Item(9).ToString
            If temp = "" Then
                DTPStartUpDate.Checked = False
                DTPStartUpDate.Text = OracleDate
            Else
                DTPStartUpDate.Checked = True
                DTPStartUpDate.Text = temp
            End If

            temp = dsHeaderData.Tables("Current").Rows(0).Item(10).ToString
            If temp = "" Then
                DTPShutdown.Checked = False
                DTPShutdown.Text = OracleDate
            Else
                DTPShutdown.Checked = True
                DTPShutdown.Text = temp
            End If

            txtPlantDescription.Text = dsHeaderData.Tables("Current").Rows(0).Item(14).ToString
            temp = dsHeaderData.Tables("Current").Rows(0).Item(15).ToString

            If temp = "" Then
                chbNSRMajor.Checked = False
                chbHAPsMajor.Checked = False
            Else
                If Mid(temp, 1, 1) = "1" Then
                    chbNSRMajor.Checked = True
                Else
                    chbNSRMajor.Checked = False
                End If
                If Mid(temp, 2, 1) = "1" Then
                    chbHAPsMajor.Checked = True
                Else
                    chbHAPsMajor.Checked = False
                End If
            End If

            temp = dsHeaderData.Tables("Current").Rows(0).Item(6).ToString
            If temp = "" Then
                cboOneHour.Text = "No"
                cboEightHour.Text = "No"
                cboPMFine.Text = "No"
            Else
                Select Case (Mid(temp, 2, 1))
                    Case 1
                        cboOneHour.Text = "Yes"
                    Case 2
                        cboOneHour.Text = "Contribute"
                    Case Else
                        cboOneHour.Text = "No"
                End Select
                Select Case (Mid(temp, 3, 1))
                    Case 1
                        cboEightHour.Text = "Atlanta"
                    Case 2
                        cboEightHour.Text = "Macon"
                    Case Else
                        cboEightHour.Text = "No"
                End Select
                Select Case (Mid(temp, 4, 1))
                    Case 1
                        cboPMFine.Text = "Atlanta"
                    Case 2
                        cboPMFine.Text = "Chattanooga"
                    Case 3
                        cboPMFine.Text = "Floyd"
                    Case 4
                        cboPMFine.Text = "Macon"
                    Case Else
                        cboPMFine.Text = "No"
                End Select
            End If
            temp = dsHeaderData.Tables("Current").Rows(0).Item(3).ToString
            AddAirProgramCodes(temp)

            dgvHeaderDataHistory.DataSource = dsHeaderData
            dgvHeaderDataHistory.DataMember = "Historical"
            dgvHeaderDataHistory.RowHeadersVisible = False
            dgvHeaderDataHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvHeaderDataHistory.AllowUserToResizeColumns = True
            dgvHeaderDataHistory.AllowUserToAddRows = False
            dgvHeaderDataHistory.AllowUserToDeleteRows = False
            dgvHeaderDataHistory.AllowUserToOrderColumns = True
            dgvHeaderDataHistory.AllowUserToResizeRows = True
            dgvHeaderDataHistory.Columns("strKey").HeaderText = "Key"
            dgvHeaderDataHistory.Columns("strKey").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgvHeaderDataHistory.Columns("strKey").DisplayIndex = 0
            dgvHeaderDataHistory.Columns("strKey").Visible = False
            dgvHeaderDataHistory.Columns("UserName").HeaderText = "Modifing Person"
            dgvHeaderDataHistory.Columns("UserName").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvHeaderDataHistory.Columns("UserName").DisplayIndex = 1
            dgvHeaderDataHistory.Columns("ModifingDate").HeaderText = "Date Modified"
            dgvHeaderDataHistory.Columns("ModifingDate").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvHeaderDataHistory.Columns("ModifingDate").DisplayIndex = 2
            dgvHeaderDataHistory.Columns("strModifingLocation").HeaderText = "Modifing Location"
            dgvHeaderDataHistory.Columns("strModifingLocation").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvHeaderDataHistory.Columns("strModifingLocation").DisplayIndex = 3
            dgvHeaderDataHistory.Columns("strOperationalStatus").HeaderText = "Operating Status"
            dgvHeaderDataHistory.Columns("strOperationalStatus").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvHeaderDataHistory.Columns("strOperationalStatus").DisplayIndex = 4
            dgvHeaderDataHistory.Columns("strClass").HeaderText = "Classification"
            dgvHeaderDataHistory.Columns("strClass").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvHeaderDataHistory.Columns("strClass").DisplayIndex = 5
            dgvHeaderDataHistory.Columns("strAIRProgramCodes").HeaderText = "Air Program Codes"
            dgvHeaderDataHistory.Columns("strAIRProgramCodes").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvHeaderDataHistory.Columns("strAIRProgramCodes").DisplayIndex = 6
            dgvHeaderDataHistory.Columns("strAIRProgramCodes").Visible = False
            dgvHeaderDataHistory.Columns("strSICCode").HeaderText = "SIC Code"
            dgvHeaderDataHistory.Columns("strSICCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvHeaderDataHistory.Columns("strSICCode").DisplayIndex = 7
            dgvHeaderDataHistory.Columns("strComments").HeaderText = "Comments"
            dgvHeaderDataHistory.Columns("strComments").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvHeaderDataHistory.Columns("strComments").DisplayIndex = 8
            dgvHeaderDataHistory.Columns("datStartUpDate").HeaderText = "Start Up Date"
            dgvHeaderDataHistory.Columns("datStartUpDate").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvHeaderDataHistory.Columns("datStartUpDate").DisplayIndex = 9
            dgvHeaderDataHistory.Columns("datShutDownDate").HeaderText = "Shut Down Date"
            dgvHeaderDataHistory.Columns("datShutDownDate").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvHeaderDataHistory.Columns("datShutDownDate").DisplayIndex = 10
            dgvHeaderDataHistory.Columns("strPlantDescription").HeaderText = "Plant Description"
            dgvHeaderDataHistory.Columns("strPlantDescription").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvHeaderDataHistory.Columns("strPlantDescription").DisplayIndex = 11
            dgvHeaderDataHistory.Columns("strAIRSNumber").HeaderText = "AIRS Number"
            dgvHeaderDataHistory.Columns("strAIRSNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvHeaderDataHistory.Columns("strAIRSNumber").DisplayIndex = 12
            dgvHeaderDataHistory.Columns("strAIRSNumber").Visible = False
            dgvHeaderDataHistory.Columns("strAttainmentStatus").Visible = False
            dgvHeaderDataHistory.Columns("strStateProgramCodes").Visible = False
            dgvHeaderDataHistory.Columns("strNAICSCode").HeaderText = "NAICS Code"
            dgvHeaderDataHistory.Columns("strNAICSCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvHeaderDataHistory.Columns("strNAICSCode").DisplayIndex = 13
            'dgvHeaderDataHistory.Columns("strRMPID").HeaderText = "RMP ID"
            'dgvHeaderDataHistory.Columns("strRMPID").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            'dgvHeaderDataHistory.Columns("strRMPID").DisplayIndex = 14



            'SQL = "Select * " & _
            '   "From " & DBNameSpace & ".VW_APBFacilityHeader " & _
            '   "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "

            'cmd = New OracleCommand(SQL, conn)

            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If

            'dr = cmd.ExecuteReader
            'recExist = dr.Read
            'If recExist = True Then
            'If IsDBNull(dr.Item("strOperationalstatus")) Then
            '    cboOperationalStatus.Text = "Unknown - Please Fix"
            'Else
            '    temp = dr.Item("strOperationalstatus")
            'End If
            'Select Case temp
            '    Case "O"
            '        cboOperationalStatus.Text = "O - Operational"
            '    Case "P"
            '        cboOperationalStatus.Text = "P - Planned"
            '    Case "C"
            '        cboOperationalStatus.Text = "C - Under Construction"
            '    Case "T"
            '        cboOperationalStatus.Text = "T - Temporarily Closed"
            '    Case "X"
            '        cboOperationalStatus.Text = "X - Closed/Dismantled"
            '    Case "I"
            '        cboOperationalStatus.Text = "I - Seasonal Operation"
            '    Case Else
            '        cboOperationalStatus.Text = "Unknown - Please Fix"
            'End Select
            'If IsDBNull(dr.Item("strClass")) Then
            '    cboClassification.Text = " "
            'Else
            '    cboClassification.Text = dr.Item("strClass")
            'End If
            'If IsDBNull(dr.Item("strSICCode")) Then
            '    txtSICCode.Text = ""
            'Else
            '    txtSICCode.Text = dr.Item("strSICCode")
            'End If
            'If IsDBNull(dr.Item("strComments")) Then
            '    txtComments.Text = ""
            'Else
            '    txtComments.Text = dr.Item("strComments")
            'End If
            'If IsDBNull(dr.Item("UserName")) Then
            '    ModifingPerson = "Unknown"
            'Else
            '    ModifingPerson = dr.Item("USERName")
            'End If
            'If IsDBNull(dr.Item("ModifingDate")) Then
            '    ModifingDate = "Unknown Date"
            'Else
            '    ModifingDate = dr.Item("ModifingDate")
            'End If
            'If IsDBNull(dr.Item("strModifingLocation")) Then
            '    ModifingLocation = "Unknown Location"
            'Else
            '    ModifingLocation = dr.Item("strModifingLocation")
            'End If
            'txtModifingComments.Text = "Modified on " & ModifingDate & " by " & ModifingPerson & " from " & ModifingLocation

            'If IsDBNull(dr.Item("datStartUpDate")) Then
            '    DTPStartUpDate.Checked = False
            '    DTPStartUpDate.Text = OracleDate
            'Else
            '    DTPStartUpDate.Checked = True
            '    DTPStartUpDate.Text = dr.Item("datStartUpDate")
            'End If
            'If IsDBNull(dr.Item("datShutDownDate")) Then
            '    DTPShutdown.Checked = False
            '    DTPShutdown.Text = OracleDate
            'Else
            '    DTPShutdown.Checked = True
            '    DTPShutdown.Text = dr.Item("datShutDownDate")
            'End If
            'If IsDBNull(dr.Item("strPlantDescription")) Then
            '    txtPlantDescription.Text = ""
            'Else
            '    txtPlantDescription.Text = dr.Item("strPlantDescription")
            'End If
            'If IsDBNull(dr.Item("strStateProgramCodes")) Then
            '    chbNSRMajor.Checked = False
            '    chbHAPsMajor.Checked = False
            'Else
            '    If Mid(dr.Item("strStateProgramCodes"), 1, 1) = "1" Then
            '        chbNSRMajor.Checked = True
            '    Else
            '        chbNSRMajor.Checked = False
            '    End If
            '    If Mid(dr.Item("strStateProgramCodes"), 2, 1) = "1" Then
            '        chbHAPsMajor.Checked = True
            '    Else
            '        chbHAPsMajor.Checked = False
            '    End If
            'End If
            'If IsDBNull(dr.Item("strAttainmentStatus")) Then
            '    cboOneHour.Text = "No"
            '    cboEightHour.Text = "No"
            '    cboPMFine.Text = "No"
            'Else
            '    Select Case (Mid(dr.Item("strAttainmentStatus"), 2, 1))
            '        Case 1
            '            cboOneHour.Text = "Yes"
            '        Case 2
            '            cboOneHour.Text = "Contribute"
            '        Case Else
            '            cboOneHour.Text = "No"
            '    End Select
            '    Select Case (Mid(dr.Item("strAttainmentStatus"), 3, 1))
            '        Case 1
            '            cboEightHour.Text = "Atlanta"
            '        Case 2
            '            cboEightHour.Text = "Macon"
            '        Case Else
            '            cboEightHour.Text = "No"
            '    End Select
            '    Select Case (Mid(dr.Item("strAttainmentStatus"), 4, 1))
            '        Case 1
            '            cboPMFine.Text = "Atlanta"
            '        Case 2
            '            cboPMFine.Text = "Chattanooga"
            '        Case 3
            '            cboPMFine.Text = "Floyd"
            '        Case 4
            '            cboPMFine.Text = "Macon"
            '        Case Else
            '            cboPMFine.Text = "No"
            '    End Select
            'End If
            'If IsDBNull(dr.Item("strAIRProgramcodes")) Then
            '    temp = "000000000000000"
            'Else
            '    temp = dr.Item("strAIRProgramcodes")
            'End If
            'End If

            'AddAirProgramCodes(temp)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub AddAirProgramCodes(ByRef AirProgramCode As String)
        Try

            chbAPC0.Checked = False
            chbAPC1.Checked = False
            chbAPC3.Checked = False
            chbAPC4.Checked = False
            chbAPC6.Checked = False
            chbAPC7.Checked = False
            chbAPC8.Checked = False
            chbAPC9.Checked = False
            chbAPCF.Checked = False
            chbAPCA.Checked = False
            chbAPCI.Checked = False
            chbAPCM.Checked = False
            chbAPCV.Checked = False
            chbAPCRMP.Checked = False

            If Mid(AirProgramCode, 1, 1) = 1 Then
                chbAPC0.Checked = True
            End If
            If Mid(AirProgramCode, 2, 1) = 1 Then
                chbAPC1.Checked = True
            End If
            If Mid(AirProgramCode, 3, 1) = 1 Then
                chbAPC3.Checked = True
            End If
            If Mid(AirProgramCode, 4, 1) = 1 Then
                chbAPC4.Checked = True
            End If
            If Mid(AirProgramCode, 5, 1) = 1 Then
                chbAPC6.Checked = True
            End If
            If Mid(AirProgramCode, 6, 1) = 1 Then
                chbAPC7.Checked = True
            End If
            If Mid(AirProgramCode, 7, 1) = 1 Then
                chbAPC8.Checked = True
            End If
            If Mid(AirProgramCode, 8, 1) = 1 Then
                chbAPC9.Checked = True
            End If
            If Mid(AirProgramCode, 9, 1) = 1 Then
                chbAPCF.Checked = True
            End If
            If Mid(AirProgramCode, 10, 1) = 1 Then
                chbAPCA.Checked = True
            End If
            If Mid(AirProgramCode, 11, 1) = 1 Then
                chbAPCI.Checked = True
            End If
            If Mid(AirProgramCode, 12, 1) = 1 Then
                chbAPCM.Checked = True
            End If
            If Mid(AirProgramCode, 13, 1) = 1 Then
                chbAPCV.Checked = True
            End If
            If Mid(AirProgramCode, 14, 1) = 1 Then
                chbAPCRMP.Checked = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
#End Region
#Region "Subs and Functions"
    Sub Save()
        Dim ErrorCheck As Boolean = False
        Dim Classification As String = ""
        Dim OperationalStatus As String = ""
        Dim SICCode As String = ""
        Dim NAICSCode As String = ""
        Dim AirProgramCode As String = ""
        Dim temp As String = ""
        Dim StartUpDate As String = ""
        Dim ShutDownDate As String = ""
        Dim Comments As String = ""
        Dim PlantDescription As String = ""
        Dim NSRMajor As String = ""
        Dim StateProgramCodes As String = "00000"
        Dim AttainmentStatus As String = "00000"
        Dim RMPNumber As String = ""

        Try
            temp = txtAirsNumber.Text

            cboOperationalStatus.BackColor = Color.White
            cboClassification.BackColor = Color.White
            txtSICCode.BackColor = Color.White
            txtNAICSCode.BackColor = Color.White

            If AccountArray(29, 2) = "1" Or AccountArray(29, 3) = "1" Or AccountArray(29, 4) = "1" Then
                'End If
                'If UserProgram = "5" Or (UserBranch = "1" And UserUnit = "---") _
                '    Or (UserProgram = "3" And AccountArray(68, 3) = "1") Then
                If cboClassification.Text <> "" And cboClassification.Text <> " " Then
                    If cboClassification.Text <> dsHeaderData.Tables("Current").Rows(0).Item(2).ToString Then
                        Classification = cboClassification.Text
                    Else
                        Classification = ""
                    End If
                Else
                    ErrorCheck = True
                    cboClassification.BackColor = Color.Yellow
                End If
                If cboOperationalStatus.Text <> "" And cboOperationalStatus.Text <> " " Then
                    If Mid(cboOperationalStatus.Text, 1, 1) <> dsHeaderData.Tables("Current").Rows(0).Item(1).ToString Then
                        OperationalStatus = Mid(cboOperationalStatus.Text, 1, 1)
                    Else
                        OperationalStatus = ""
                    End If
                Else
                    ErrorCheck = True
                    cboOperationalStatus.BackColor = Color.Yellow
                End If
                If txtSICCode.Text <> dsHeaderData.Tables("Current").Rows(0).Item(4).ToString Then
                    If IsNumeric(txtSICCode.Text) Then
                        If txtSICCode.Text <> "" Then
                            SICCode = txtSICCode.Text
                        Else
                            SICCode = ""
                        End If
                    Else
                        ErrorCheck = True
                        txtSICCode.BackColor = Color.Yellow
                    End If
                Else
                    SICCode = ""
                End If
                If txtNAICSCode.Text <> dsHeaderData.Tables("Current").Rows(0).Item(5).ToString Then
                    If IsNumeric(txtNAICSCode.Text) Then
                        If txtNAICSCode.Text <> "" Then
                            If ValidateNAICS(txtNAICSCode.Text) = False Then
                                ErrorCheck = True
                                txtNAICSCode.BackColor = Color.Yellow
                            End If
                            NAICSCode = txtNAICSCode.Text
                        Else
                            NAICSCode = ""
                        End If
                    Else
                        ErrorCheck = True
                        txtNAICSCode.BackColor = Color.Yellow
                    End If
                End If
                If chbNSRMajor.Checked = True Then
                    StateProgramCodes = "10000"
                Else
                    StateProgramCodes = "00000"
                End If
                If chbHAPsMajor.Checked = True Then
                    StateProgramCodes = Mid(StateProgramCodes, 1, 1) & "1" & Mid(StateProgramCodes, 3)
                Else
                    StateProgramCodes = Mid(StateProgramCodes, 1, 1) & "0" & Mid(StateProgramCodes, 3)
                End If
                If StateProgramCodes <> dsHeaderData.Tables("Current").Rows(0).Item(15).ToString Then
                    'StateProgramCodes = StateProgramCodes
                Else
                    StateProgramCodes = ""
                End If

                Select Case cboOneHour.Text
                    Case "Yes"
                        AttainmentStatus = "01000"
                    Case "Contribute"
                        AttainmentStatus = "02000"
                    Case Else
                        AttainmentStatus = "00000"
                End Select
                Select Case cboEightHour.Text
                    Case "Atlanta"
                        AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "1" & Mid(AttainmentStatus, 4)
                    Case "Macon"
                        AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "2" & Mid(AttainmentStatus, 4)
                    Case Else
                        AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "0" & Mid(AttainmentStatus, 4)
                End Select
                Select Case cboPMFine.Text
                    Case "Atlanta"
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "1" & Mid(AttainmentStatus, 5)
                    Case "Chattanooga"
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "2" & Mid(AttainmentStatus, 5)
                    Case "Floyd"
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "3" & Mid(AttainmentStatus, 5)
                    Case "Macon"
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "4" & Mid(AttainmentStatus, 5)
                    Case Else
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "0" & Mid(AttainmentStatus, 5)
                End Select
                If AttainmentStatus <> dsHeaderData.Tables("Current").Rows(0).Item(6).ToString Then
                    'AttainmentStatus = AttainmentStatus
                Else
                    AttainmentStatus = ""
                End If

                AirProgramCode = ""

                If chbAPC0.Checked = True Then
                    AirProgramCode = "1"
                    SQL = "Select strPollutantKey " & _
                    "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                    "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "0' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "0', " & _
                         "'OT', 'C', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        If OperationalStatus <> "" Then
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strOperationalStatus = '" & OperationalStatus & "' " & _
                            "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "0' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                        If Classification <> "" Then
                            SQL = "Select strUpdateStatus " & _
                            "from " & DBNameSpace & ".AFSAirPollutantData " & _
                            "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "0' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                temp = dr.Item("strUpdateStatus")
                            End While
                            dr.Close()
                            If temp = "N" Then
                                SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
                                "strUpdateStatus = 'C' " & _
                                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "0'"
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Read()
                                dr.Close()
                            End If
                        End If
                    End If
                Else
                    AirProgramCode = "0"
                    SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
                       "active = '0', " & _
                       "UpdateUser = '" & UserGCode & "', " & _
                       "updateDateTime = '" & OracleDate & "' " & _
                       "where strSubPartKey = '0413" & txtAirsNumber.Text & "0' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                If chbAPC1.Checked = True Then
                    AirProgramCode = AirProgramCode & "1"
                    SQL = "Select strPollutantKey " & _
                    "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                    "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "1' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "1', " & _
                         "'OT', 'C', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        If OperationalStatus <> "" Then
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strOperationalStatus = '" & OperationalStatus & "' " & _
                            "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "1' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                        If Classification <> "" Then
                            SQL = "Select strUpdateStatus " & _
                            "from " & DBNameSpace & ".AFSAirPollutantData " & _
                            "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "1' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                temp = dr.Item("strUpdateStatus")
                            End While
                            dr.Close()
                            If temp = "N" Then
                                SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
                                "strUpdateStatus = 'C' " & _
                                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "1'"
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Read()
                                dr.Close()
                            End If
                        End If
                    End If
                Else
                    AirProgramCode = AirProgramCode & "0"
                End If
                If chbAPC3.Checked = True Then
                    AirProgramCode = AirProgramCode & "1"
                    SQL = "Select strPollutantKey " & _
                   "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                   "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "3' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "3', " & _
                         "'OT', 'C', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        If OperationalStatus <> "" Then
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strOperationalStatus = '" & OperationalStatus & "' " & _
                            "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "3' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                        If Classification <> "" Then
                            SQL = "Select strUpdateStatus " & _
                            "from " & DBNameSpace & ".AFSAirPollutantData " & _
                            "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "3' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                temp = dr.Item("strUpdateStatus")
                            End While
                            dr.Close()
                            If temp = "N" Then
                                SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
                                "strUpdateStatus = 'C' " & _
                                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "3'"
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Read()
                                dr.Close()
                            End If
                        End If
                    End If
                Else
                    AirProgramCode = AirProgramCode & "0"
                End If
                If chbAPC4.Checked = True Then
                    AirProgramCode = AirProgramCode & "1"
                    SQL = "Select strPollutantKey " & _
                   "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                   "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "4' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "4', " & _
                         "'OT', 'C', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        If OperationalStatus <> "" Then
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strOperationalStatus = '" & OperationalStatus & "' " & _
                            "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "4' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                        If Classification <> "" Then
                            SQL = "Select strUpdateStatus " & _
                            "from " & DBNameSpace & ".AFSAirPollutantData " & _
                            "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "4' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                temp = dr.Item("strUpdateStatus")
                            End While
                            dr.Close()
                            If temp = "N" Then
                                SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
                                "strUpdateStatus = 'C' " & _
                                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "4'"
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Read()
                                dr.Close()
                            End If
                        End If
                    End If
                Else
                    AirProgramCode = AirProgramCode & "0"
                End If
                If chbAPC6.Checked = True Then
                    AirProgramCode = AirProgramCode & "1"
                    SQL = "Select strPollutantKey " & _
                   "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                   "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "6' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "6', " & _
                         "'OT', 'C', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        If OperationalStatus <> "" Then
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strOperationalStatus = '" & OperationalStatus & "' " & _
                            "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "6' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                        If Classification <> "" Then
                            SQL = "Select strUpdateStatus " & _
                            "from " & DBNameSpace & ".AFSAirPollutantData " & _
                            "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "6' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                temp = dr.Item("strUpdateStatus")
                            End While
                            dr.Close()
                            If temp = "N" Then
                                SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
                                "strUpdateStatus = 'C' " & _
                                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "6'"
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Read()
                                dr.Close()
                            End If
                        End If
                    End If
                Else
                    AirProgramCode = AirProgramCode & "0"
                End If
                If chbAPC7.Checked = True Then
                    AirProgramCode = AirProgramCode & "1"
                    SQL = "Select strPollutantKey " & _
                   "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                   "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "7' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "7', " & _
                         "'OT', 'C', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        If OperationalStatus <> "" Then
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strOperationalStatus = '" & OperationalStatus & "' " & _
                            "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "7' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                        If Classification <> "" Then
                            SQL = "Select strUpdateStatus " & _
                            "from " & DBNameSpace & ".AFSAirPollutantData " & _
                            "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "7' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                temp = dr.Item("strUpdateStatus")
                            End While
                            dr.Close()
                            If temp = "N" Then
                                SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
                                "strUpdateStatus = 'C' " & _
                                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "7'"
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Read()
                                dr.Close()
                            End If
                        End If
                    End If
                Else
                    AirProgramCode = AirProgramCode & "0"
                End If
                If chbAPC8.Checked = True Then
                    AirProgramCode = AirProgramCode & "1"
                    SQL = "Select strPollutantKey " & _
                   "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                   "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "8' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "8', " & _
                         "'OT', 'C', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        If OperationalStatus <> "" Then
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strOperationalStatus = '" & OperationalStatus & "' " & _
                            "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "8' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                        If Classification <> "" Then
                            SQL = "Select strUpdateStatus " & _
                            "from " & DBNameSpace & ".AFSAirPollutantData " & _
                            "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "8' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                temp = dr.Item("strUpdateStatus")
                            End While
                            dr.Close()
                            If temp = "N" Then
                                SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
                                "strUpdateStatus = 'C' " & _
                                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "8'"
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Read()
                                dr.Close()
                            End If
                        End If
                    End If
                Else
                    AirProgramCode = AirProgramCode & "0"
                    SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
                   "active = '0', " & _
                   "UpdateUser = '" & UserGCode & "', " & _
                   "updateDateTime = '" & OracleDate & "' " & _
                   "where strSubPartKey = '0413" & txtAirsNumber.Text & "8' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                If chbAPC9.Checked = True Then
                    AirProgramCode = AirProgramCode & "1"
                    SQL = "Select strPollutantKey " & _
                   "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                   "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "9' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "9', " & _
                         "'OT', 'C', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        If OperationalStatus <> "" Then
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strOperationalStatus = '" & OperationalStatus & "' " & _
                            "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "9' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                        If Classification <> "" Then
                            SQL = "Select strUpdateStatus " & _
                            "from " & DBNameSpace & ".AFSAirPollutantData " & _
                            "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "9' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                temp = dr.Item("strUpdateStatus")
                            End While
                            dr.Close()
                            If temp = "N" Then
                                SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
                                "strUpdateStatus = 'C' " & _
                                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "9'"
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Read()
                                dr.Close()
                            End If
                        End If
                    End If
                Else
                    AirProgramCode = AirProgramCode & "0"
                    SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
                   "active = '0', " & _
                   "UpdateUser = '" & UserGCode & "', " & _
                   "updateDateTime = '" & OracleDate & "' " & _
                   "where strSubPartKey = '0413" & txtAirsNumber.Text & "9' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                If chbAPCF.Checked = True Then
                    AirProgramCode = AirProgramCode & "1"
                    SQL = "Select strPollutantKey " & _
                   "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                   "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "F' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "F', " & _
                         "'OT', 'C', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        If OperationalStatus <> "" Then
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strOperationalStatus = '" & OperationalStatus & "' " & _
                            "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "F' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                        If Classification <> "" Then
                            SQL = "Select strUpdateStatus " & _
                            "from " & DBNameSpace & ".AFSAirPollutantData " & _
                            "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "F' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                temp = dr.Item("strUpdateStatus")
                            End While
                            dr.Close()
                            If temp = "N" Then
                                SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
                                "strUpdateStatus = 'C' " & _
                                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "F'"
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Read()
                                dr.Close()
                            End If
                        End If
                    End If
                Else
                    AirProgramCode = AirProgramCode & "0"
                End If
                If chbAPCA.Checked = True Then
                    AirProgramCode = AirProgramCode & "1"
                    SQL = "Select strPollutantKey " & _
                   "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                   "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "A' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "A', " & _
                         "'OT', 'C', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        If OperationalStatus <> "" Then
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strOperationalStatus = '" & OperationalStatus & "' " & _
                            "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "A' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                        If Classification <> "" Then
                            SQL = "Select strUpdateStatus " & _
                            "from " & DBNameSpace & ".AFSAirPollutantData " & _
                            "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "A' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                temp = dr.Item("strUpdateStatus")
                            End While
                            dr.Close()
                            If temp = "N" Then
                                SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
                                "strUpdateStatus = 'C' " & _
                                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "A'"
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Read()
                                dr.Close()
                            End If
                        End If
                    End If
                Else
                    AirProgramCode = AirProgramCode & "0"
                End If
                If chbAPCI.Checked = True Then
                    AirProgramCode = AirProgramCode & "1"
                    SQL = "Select strPollutantKey " & _
                   "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                   "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "I' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "I', " & _
                         "'OT', 'C', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        If OperationalStatus <> "" Then
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strOperationalStatus = '" & OperationalStatus & "' " & _
                            "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "I' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                        If Classification <> "" Then
                            SQL = "Select strUpdateStatus " & _
                            "from " & DBNameSpace & ".AFSAirPollutantData " & _
                            "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "I' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                temp = dr.Item("strUpdateStatus")
                            End While
                            dr.Close()
                            If temp = "N" Then
                                SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
                                "strUpdateStatus = 'C' " & _
                                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "I'"
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Read()
                                dr.Close()
                            End If
                        End If
                    End If
                Else
                    AirProgramCode = AirProgramCode & "0"
                End If
                If chbAPCM.Checked = True Then
                    AirProgramCode = AirProgramCode & "1"
                    SQL = "Select strPollutantKey " & _
                   "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                   "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "M' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "M', " & _
                         "'OT', 'C', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        If OperationalStatus <> "" Then
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strOperationalStatus = '" & OperationalStatus & "' " & _
                            "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "M' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                        If Classification <> "" Then
                            SQL = "Select strUpdateStatus " & _
                            "from " & DBNameSpace & ".AFSAirPollutantData " & _
                            "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "M' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                temp = dr.Item("strUpdateStatus")
                            End While
                            dr.Close()
                            If temp = "N" Then
                                SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
                                "strUpdateStatus = 'C' " & _
                                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "M'"
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Read()
                                dr.Close()
                            End If
                        End If
                    End If
                Else
                    AirProgramCode = AirProgramCode & "0"
                    SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
                    "active = '0', " & _
                    "UpdateUser = '" & UserGCode & "', " & _
                    "updateDateTime = '" & OracleDate & "' " & _
                    "where strSubPartKey = '0413" & txtAirsNumber.Text & "M' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                If chbAPCV.Checked = True Then
                    AirProgramCode = AirProgramCode & "1"
                    SQL = "Select strPollutantKey " & _
                   "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                   "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "V' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = False Then
                        SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
                         "(strAirsNumber, strAirPollutantKey, " & _
                         "strPollutantKey, strComplianceStatus, " & _
                         "strModifingPerson, datModifingDate, " & _
                         "strOperationalStatus) " & _
                         "values " & _
                         "('0413" & txtAirsNumber.Text & "', '0413" & txtAirsNumber.Text & "V', " & _
                         "'OT', 'C', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'O')"
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        dr.Close()
                    Else
                        If OperationalStatus <> "" Then
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strOperationalStatus = '" & OperationalStatus & "' " & _
                            "where strAirPOllutantKey = '0413" & txtAirsNumber.Text & "V' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                        If Classification <> "" Then
                            SQL = "Select strUpdateStatus " & _
                            "from " & DBNameSpace & ".AFSAirPollutantData " & _
                            "where strAirPollutantKey = '0413" & txtAirsNumber.Text & "V' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                temp = dr.Item("strUpdateStatus")
                            End While
                            dr.Close()
                            If temp = "N" Then
                                SQL = "update " & DBNameSpace & ".AFSAirPollutantData set " & _
                                "strUpdateStatus = 'C' " & _
                                "where strAIRPollutantKey = '0413" & txtAirsNumber.Text & "V'"
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Read()
                                dr.Close()
                            End If
                        End If
                    End If
                Else
                    AirProgramCode = AirProgramCode & "0"
                End If
                If chbAPCRMP.Checked = True Then
                    AirProgramCode = AirProgramCode & "1"
                Else
                    AirProgramCode = AirProgramCode & "0"
                End If

                AirProgramCode = AirProgramCode & "0"

                If AirProgramCode <> dsHeaderData.Tables("Current").Rows(0).Item(3).ToString Then
                    'AirProgramCode = AirProgramCode
                Else
                    AirProgramCode = ""
                End If

                If DTPStartUpDate.Text <> dsHeaderData.Tables("Current").Rows(0).Item(9).ToString Then
                    If DTPStartUpDate.Checked = True Then
                        StartUpDate = DTPStartUpDate.Text
                    Else
                        If dsHeaderData.Tables("Current").Rows(0).Item(9).ToString <> "" Then
                            StartUpDate = ""
                        Else
                            StartUpDate = "Ignore"
                        End If
                    End If
                Else
                    StartUpDate = "Ignore"
                End If

                If DTPShutdown.Text <> dsHeaderData.Tables("Current").Rows(0).Item(10).ToString Then
                    If DTPShutdown.Checked = True Then
                        ShutDownDate = DTPShutdown.Text
                    Else
                        If dsHeaderData.Tables("Current").Rows(0).Item(10).ToString <> "" Then
                            ShutDownDate = ""
                        Else
                            ShutDownDate = "Ignore"
                        End If
                    End If
                Else
                    ShutDownDate = "Ignore"
                End If

                If txtPlantDescription.Text <> dsHeaderData.Tables("Current").Rows(0).Item(14).ToString Then
                    If txtPlantDescription.Text <> "" Then
                        PlantDescription = Replace(txtPlantDescription.Text, "'", "''")
                    Else
                        PlantDescription = ""
                    End If
                Else
                    PlantDescription = "Ignore"
                End If

                If txtComments.Text <> "" Then
                    If txtComments.Text <> dsHeaderData.Tables("Current").Rows(0).Item(13).ToString Then
                        Comments = Replace(txtComments.Text, "'", "''")
                    Else
                        MsgBox("Since this is a direct change to the data, " & vbCrLf & _
                        "please make a unique comment. " & vbCrLf & _
                        "So future users know why the data was changed." & vbCrLf & _
                        "No data will be saved at this time.", _
                         MsgBoxStyle.Information, "Edit Header Data")
                        Comments = "Error"
                    End If
                Else
                    MsgBox("Since this is a direct change to the data, " & vbCrLf & _
                    "please make a unique comment. " & vbCrLf & _
                    "So future users know why the data was changed." & vbCrLf & _
                    "No data will be saved at this time.", _
                     MsgBoxStyle.Information, "Edit Header Data")
                    Comments = "Error"
                End If

                If ErrorCheck <> True Then
                    If Comments <> "Error" Then
                        If Classification <> "" Or AirProgramCode <> "" Or SICCode <> "" Or _
                          OperationalStatus <> "" Or StartUpDate <> "Ignore" Or _
                          ShutDownDate <> "Ignore" Or PlantDescription <> "Ignore" Or _
                          Comments <> "" Or AttainmentStatus <> "" Or _
                          StateProgramCodes <> "" Or NAICSCode <> "" Then

                            SQL = "Update " & DBNameSpace & ".APBHeaderData set "
                            If Classification <> "" Then
                                SQL = SQL & "strClass = '" & Classification & "', "
                            End If
                            If OperationalStatus <> "" Then
                                SQL = SQL & "strOperationalstatus = '" & OperationalStatus & "', "
                            End If
                            If SICCode <> "" Then
                                SQL = SQL & "strSICCode = '" & SICCode & "', "
                            End If
                            If NAICSCode <> "" Then
                                SQL = SQL & "strNAICSCode = '" & NAICSCode & "', "
                            End If
                            If StateProgramCodes <> "" Then
                                SQL = SQL & "strStateProgramCodes = '" & StateProgramCodes & "', "
                            End If
                            If AttainmentStatus <> "" Then
                                SQL = SQL & "strAttainmentStatus = '" & AttainmentStatus & "', "
                            End If
                            If AirProgramCode <> "" Then
                                SQL = SQL & "strAIRProgramCodes = '" & AirProgramCode & "', "
                            End If
                            If StartUpDate <> "Ignore" Then
                                SQL = SQL & "datStartUpDate = '" & StartUpDate & "', "
                            End If
                            If ShutDownDate <> "Ignore" Then
                                SQL = SQL & "datShutDownDate = '" & ShutDownDate & "', "
                            End If
                            If PlantDescription <> "Ignore" Then
                                SQL = SQL & "strPlantDescription = '" & Replace(PlantDescription, "'", "''") & "', "
                            End If
                            If Comments <> "Ignore" Then
                                SQL = SQL & "strComments = '" & Replace(Comments, "'", "''") & "', "
                            End If
                            SQL = SQL & "strModifingPerson = '" & UserGCode & "', " & _
                            "datModifingDate = '" & OracleDate & "', " & _
                            "strModifingLocation = '2' " & _
                            "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)

                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If


                            dr = cmd.ExecuteReader
                            dr.Close()

                            If FacilitySummary Is Nothing Then
                            Else

                            End If


                            If mtbRiskManagementNumber.Text <> "" Then
                                RMPNumber = mtbRiskManagementNumber.Text
                            Else
                                RMPNumber = ""
                            End If

                            SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                            "strRMPID = '" & Replace(RMPNumber, "'", "''") & "' " & _
                            "where strAIRSnumber = '0413" & txtAirsNumber.Text & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()

                            If OperationalStatus = "X" Then
                                SQL = "Update airbranch.EIS_FacilitySite set " & _
                                "strFacilitySiteStatusCode = 'PS', " & _
                                "strFacilitySiteComment = 'Facility Shutdown by permitting action.', " & _
                                "UpdateUSer = '" & UserName & "', " & _
                                "updateDateTime = sysdate " & _
                                "where facilitySiteID = '" & txtAirsNumber.Text & "' "
                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                cmd.ExecuteReader()
                            End If

                            SQL = "Update AIRBranch.EIS_FacilitySite set " & _
                            "strFacilitySiteDescription = '" & Replace(txtPlantDescription.Text, "'", "''") & "' " & _
                            "where facilitySiteID = '" & txtAirsNumber.Text & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            cmd.ExecuteReader()


                            LoadFacilityHeaderData()
                            MsgBox("Data Updated", MsgBoxStyle.Information, "Edit Header Data")
                        Else
                            MsgBox("No data was changed", MsgBoxStyle.Information, "Edit Header Data")
                        End If
                    End If
                Else
                    MsgBox("The data was not save due to bad data.", _
                                       MsgBoxStyle.Information, "Edit Header Data")
                End If
            End If

        Catch ex As Exception
            ErrorReport(temp & vbCrLf & ex.ToString(), "DevEditHeaderData.Save")
        Finally

        End Try

    End Sub
    Sub Back()
        Try

            EditHeaderData = Nothing
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "Declarations"
    Private Sub TBEditHeaderData_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBEditHeaderData.ButtonClick
        Try

            Select Case TBEditHeaderData.Buttons.IndexOf(e.Button)
                Case 0
                    Save()
                Case 1
                    Back()
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub IAIPEditHeaderData_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try


            EditHeaderData = Nothing

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "Main Menu"
    Private Sub mmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSave.Click
        Try

            Save()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiBack.Click
        Try

            Back()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("(^X)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCopy.Click
        Try

            SendKeys.Send("(^C)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPaste.Click
        Try

            SendKeys.Send("(^V)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try

            Help.ShowHelp(Label1, HelpUrl)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
    Private Sub llbCurrentData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbCurrentData.LinkClicked
        Try


            If txtAirsNumber.Text <> "" Then
                txtKey.Text = ""
                LoadFacilityHeaderData()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgvHeaderDataHistory_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvHeaderDataHistory.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvHeaderDataHistory.HitTest(e.X, e.Y)

        Try


            If dgvHeaderDataHistory.RowCount > 0 And hti.RowIndex <> -1 Then
                txtKey.Text = dgvHeaderDataHistory(0, hti.RowIndex).Value
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtKey_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKey.TextChanged
        Try
            Dim temp As String = ""
            Dim ModifingPerson As String
            Dim ModifingDate As String
            Dim ModifingLocation As String



            If txtKey.Text <> "" Then
                SQL = "select * " & _
                "from " & DBNameSpace & ".VW_HB_APBHeaderData " & _
                "where strKEy = '" & txtKey.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then

                    If IsDBNull(dr.Item("strOperationalStatus")) Then
                        cboOperationalStatus.Text = "Unknown - Please Fix"
                    Else
                        temp = dr.Item("strOperationalStatus")
                        Select Case temp
                            Case "O"
                                cboOperationalStatus.Text = "O - Operational"
                            Case "P"
                                cboOperationalStatus.Text = "P - Planned"
                            Case "C"
                                cboOperationalStatus.Text = "C - Under Construction"
                            Case "T"
                                cboOperationalStatus.Text = "T - Temporarily Closed"
                            Case "X"
                                cboOperationalStatus.Text = "X - Closed/Dismantled"
                            Case "I"
                                cboOperationalStatus.Text = "I - Seasonal Operation"
                            Case Else
                                cboOperationalStatus.Text = "Unknown - Please Fix"
                        End Select
                    End If
                    If IsDBNull(dr.Item("strClass")) Then
                        cboClassification.Text = " "
                    Else
                        temp = dr.Item("strClass")
                        cboClassification.Text = dr.Item("strClass")
                    End If

                    If IsDBNull(dr.Item("strSICCode")) Then
                        txtSICCode.Text = ""
                    Else
                        txtSICCode.Text = dr.Item("strSICCode")
                    End If
                    If IsDBNull(dr.Item("strNAICSCode")) Then
                        txtNAICSCode.Clear()
                    Else
                        txtNAICSCode.Text = dr.Item("strNAICSCode")
                    End If
                    If IsDBNull(dr.Item("UserName")) Then
                        ModifingPerson = "Unknown"
                    Else
                        ModifingPerson = dr.Item("USERName")
                    End If
                    If IsDBNull(dr.Item("ModifingDate")) Then
                        ModifingDate = "Unknown Date"
                    Else
                        ModifingDate = dr.Item("ModifingDate")
                    End If
                    If IsDBNull(dr.Item("strModifingLocation")) Then
                        ModifingLocation = "Unknown Location"
                    Else
                        ModifingLocation = dr.Item("strModifingLocation")
                    End If
                    txtModifingComments.Text = "Modified on " & ModifingDate & " by " & ModifingPerson & " from " & ModifingLocation

                    If IsDBNull(dr.Item("strComments")) Then
                        txtComments.Text = ""
                    Else
                        txtComments.Text = dr.Item("strComments")
                    End If
                    If IsDBNull(dr.Item("datStartUpDate")) Then
                        DTPStartUpDate.Text = OracleDate
                        DTPStartUpDate.Checked = False
                    Else
                        DTPStartUpDate.Checked = True
                        DTPStartUpDate.Text = dr.Item("datStartUpDate")
                    End If
                    If IsDBNull(dr.Item("datShutDownDate")) Then
                        DTPShutdown.Text = OracleDate
                        DTPShutdown.Checked = False
                    Else
                        DTPShutdown.Checked = True
                        DTPShutdown.Text = dr.Item("datShutDownDate")
                    End If
                    If IsDBNull(dr.Item("strPlantDescription")) Then
                        txtPlantDescription.Text = ""
                    Else
                        txtPlantDescription.Text = dr.Item("strPlantDescription")
                    End If
                    If IsDBNull(dr.Item("strStateProgramCodes")) Then
                        chbNSRMajor.Checked = False
                        chbHAPsMajor.Checked = False
                    Else
                        If Mid(dr.Item("strStateProgramCodes"), 1, 1) = "1" Then
                            chbNSRMajor.Checked = True
                        Else
                            chbNSRMajor.Checked = False
                        End If
                        If Mid(dr.Item("strStateProgramCodes"), 2, 1) = "1" Then
                            chbHAPsMajor.Checked = True
                        Else
                            chbHAPsMajor.Checked = False
                        End If
                    End If
                    If IsDBNull(dr.Item("strAttainmentStatus")) Then
                        cboOneHour.Text = "No"
                        cboEightHour.Text = "No"
                        cboPMFine.Text = "No"
                    Else
                        Select Case (Mid(dr.Item("strAttainmentStatus"), 2, 1))
                            Case 1
                                cboOneHour.Text = "Yes"
                            Case 2
                                cboOneHour.Text = "Contribute"
                            Case Else
                                cboOneHour.Text = "No"
                        End Select
                        Select Case (Mid(dr.Item("strAttainmentStatus"), 3, 1))
                            Case 1
                                cboEightHour.Text = "Atlanta"
                            Case 2
                                cboEightHour.Text = "Macon"
                            Case Else
                                cboEightHour.Text = "No"
                        End Select
                        Select Case (Mid(dr.Item("strAttainmentStatus"), 4, 1))
                            Case 1
                                cboPMFine.Text = "Atlanta"
                            Case 2
                                cboPMFine.Text = "Chattanooga"
                            Case 3
                                cboPMFine.Text = "Floyd"
                            Case 4
                                cboPMFine.Text = "Macon"
                            Case Else
                                cboPMFine.Text = "No"
                        End Select
                    End If
                    If IsDBNull(dr.Item("strAirProgramCodes")) Then
                        temp = "000000000000000"
                    Else
                        temp = dr.Item("strAirProgramCodes")
                    End If
                End If

                AddAirProgramCodes(temp)

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
#End Region

    Private Sub txtAirsNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAirsNumber.TextChanged
        Try
            If txtAirsNumber.Text <> "" Then
                LoadFacilityHeaderData()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

   
End Class