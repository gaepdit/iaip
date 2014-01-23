Imports Oracle.DataAccess.Client
Imports System.Collections.Generic


Public Class IAIPEditAirProgramPollutants
    Dim SQL, SQL2, SQL3 As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean

    Dim dsPollutant As DataSet
    Dim daPollutant As OracleDataAdapter
    Dim dsComplianceStatus As DataSet
    Dim daComplianceStatus As OracleDataAdapter
    Dim dsDataGrid As DataSet
    Dim daDataGrid As OracleDataAdapter

    Private Sub DevEditAirProgramPollutants_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try


            Panel1.Text = "Select a Function..."
            Panel2.Text = UserName
            Panel3.Text = OracleDate

            LoadTop()
            LoadDataSets()
            LoadPollutantCombos()
            LoadDataGrid()
            TCEditPollutants.TabPages.Remove(TPEnforcementPollutants)

            'If txtEnforcementNumber.Text <> "" Then
            '    LoadEnforcementPollutants()
            '    lblEnforcement.Text = "Enforcement Action #" & txtEnforcementNumber.Text & " - Pollutants in Violation"
            'Else
            '    TCEditPollutants.TabPages.Remove(TPEnforcementPollutants)
            'End If

            If AccountArray(27, 3) = "1" Or AccountArray(27, 2) = "1" Or (UserBranch = "1" And UserUnit = "---") Then
                cboComplianceStatus.Enabled = True
                cboComplianceStatus.SelectedValue = 4
            Else
                cboComplianceStatus.Enabled = False
                cboComplianceStatus.SelectedValue = 4
            End If

            If UserProgram = "7" Or UserProgram = "9" Or UserProgram = "10" _
                Or UserProgram = "11" Or UserProgram = "12" Or UserProgram = "13" _
                    Or UserProgram = "14" Or UserProgram = "15" Then
                btnSaveNewPollutant.Enabled = False
                tsbSave.Visible = False
                mmiSave.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#Region "Page Load Functions"
    Sub LoadTop()
        Dim AirPrograms As String

        Try

            cboAirProgramCodes.Items.Clear()
            cboAirProgramCodes.Text = ""

            cboAirProgramCodes.Items.Add("")

            SQL = "Select " & _
            "strAirProgramCodes, strFacilityName " & _
            "from " & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation " & _
            "where " & DBNameSpace & ".APBHeaderData.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber " & _
            "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & txtAirsNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            While dr.Read

                'AirPrograms = dr.Item("Programs")
                AirPrograms = dr.Item("strAirProgramCodes")
                txtFacilityName.Text = dr.Item("strFacilityName")

                If AirPrograms <> "" Then
                    If Mid(AirPrograms, 1, 1) = "1" Then
                        cboAirProgramCodes.Items.Add("0 - SIP")
                    End If
                    If Mid(AirPrograms, 2, 1) = "1" Then
                        cboAirProgramCodes.Items.Add("1 - Fed. SIP")
                    End If
                    If Mid(AirPrograms, 3, 1) = "1" Then
                        cboAirProgramCodes.Items.Add("3 - Non Fed.")
                    End If
                    If Mid(AirPrograms, 4, 1) = "1" Then
                        cboAirProgramCodes.Items.Add("4 - CFC Tracking")
                    End If
                    If Mid(AirPrograms, 5, 1) = "1" Then
                        cboAirProgramCodes.Items.Add("6 - PSD")
                    End If
                    If Mid(AirPrograms, 6, 1) = "1" Then
                        cboAirProgramCodes.Items.Add("7 - NSR")
                    End If
                    If Mid(AirPrograms, 7, 1) = "1" Then
                        cboAirProgramCodes.Items.Add("8 - NESHAP")
                    End If
                    If Mid(AirPrograms, 8, 1) = "1" Then
                        cboAirProgramCodes.Items.Add("9 - NSPS")
                    End If
                    If Mid(AirPrograms, 9, 1) = "1" Then
                        cboAirProgramCodes.Items.Add("F - FESOP")
                    End If
                    If Mid(AirPrograms, 10, 1) = "1" Then
                        cboAirProgramCodes.Items.Add("A - Acid Precip.")
                    End If
                    If Mid(AirPrograms, 11, 1) = "1" Then
                        cboAirProgramCodes.Items.Add("I - Native American")
                    End If
                    If Mid(AirPrograms, 12, 1) = "1" Then
                        cboAirProgramCodes.Items.Add("M - MACT")
                    End If
                    If Mid(AirPrograms, 13, 1) = "1" Then
                        cboAirProgramCodes.Items.Add("V - Title V")
                    End If
                End If
            End While

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub LoadDataSets()
        Try

            SQL = "Select strPollutantDescription, " & _
            "strPollutantCode " & _
            "from " & DBNameSpace & ".LookUPPollutants " & _
            "where strAFSCode = 'True' " & _
            "order by strPollutantDescription "

            SQL2 = "Select " & _
            "strComplianceCode, " & _
            "(strComplianceCode || ' - ' || strComplianceDesc) as ComplianceDesc " & _
            "from " & DBNameSpace & ".LookUpComplianceStatus " & _
            "order by strComplianceCode "

            dsPollutant = New DataSet
            dsComplianceStatus = New DataSet

            daPollutant = New OracleDataAdapter(SQL, CurrentConnection)
            daComplianceStatus = New OracleDataAdapter(SQL2, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daPollutant.Fill(dsPollutant, "Pollutant")
            daComplianceStatus.Fill(dsComplianceStatus, "ComplianceStatus")

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub LoadPollutantCombos()
        Dim dtPollutants As New DataTable
        Dim dtComplianceStatus As New DataTable

        Dim drDSRow As DataRow
        Dim drDSRow2 As DataRow
        Dim drNewRow As DataRow

        Try

            dtPollutants.Columns.Add("strPollutantDescription", GetType(System.String))
            dtPollutants.Columns.Add("strPollutantCode", GetType(System.String))

            dtComplianceStatus.Columns.Add("ComplianceDesc", GetType(System.String))
            dtComplianceStatus.Columns.Add("strComplianceCode", GetType(System.String))

            drNewRow = dtPollutants.NewRow
            drNewRow("strPollutantDescription") = " "
            drNewRow("StrPollutantCode") = ""
            dtPollutants.Rows.Add(drNewRow)

            For Each drDSRow In dsPollutant.Tables("Pollutant").Rows()
                drNewRow = dtPollutants.NewRow
                drNewRow("strPollutantDescription") = drDSRow("strPollutantDescription")
                drNewRow("strPollutantCode") = drDSRow("strPOllutantCode")
                dtPollutants.Rows.Add(drNewRow)
            Next

            With cboPollutants
                .DataSource = dtPollutants
                .DisplayMember = "strPollutantDescription"
                .ValueMember = "strPollutantCode"
                .SelectedIndex = 0
            End With

            drNewRow = dtComplianceStatus.NewRow
            drNewRow("ComplianceDesc") = " "
            drNewRow("strComplianceCode") = ""
            dtComplianceStatus.Rows.Add(drNewRow)

            For Each drDSRow2 In dsComplianceStatus.Tables("ComplianceStatus").Rows()
                drNewRow = dtComplianceStatus.NewRow
                drNewRow("ComplianceDesc") = drDSRow2("ComplianceDesc")
                drNewRow("strComplianceCode") = drDSRow2("strComplianceCode")
                dtComplianceStatus.Rows.Add(drNewRow)
            Next

            With cboComplianceStatus
                .DataSource = dtComplianceStatus
                .DisplayMember = "ComplianceDesc"
                .ValueMember = "strComplianceCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub LoadDataGrid()
        Try

            SQL = "Select " & _
            "case " & _
            "	when substr(strAIRPollutantKey, 13, 1) = '0' then '0 - SIP' " & _
            "	when substr(strAIRPollutantKey, 13, 1) = '1' then '1 - Fed. SIP'  " & _
            "	when substr(strAIRPollutantKey, 13, 1) = '3' then '3 - Non Fed.'  " & _
            "	when substr(strAIRPollutantKey, 13, 1) = '4' then '4 - CFC Tracking'  " & _
            "	when substr(strAIRPollutantKey, 13, 1) = '6' then '6 - PSD'  " & _
            "	when substr(strAIRPollutantKey, 13, 1) = '7' then '7 - NSR'  " & _
            "	when substr(strAIRPollutantKey, 13, 1) = '8' then '8 - NESHAP'  " & _
            "	when substr(strAIRPollutantKey, 13, 1) = '9' then '9 - NSPS'  " & _
            "	when substr(strAIRPollutantKey, 13, 1) = 'F' then 'F - FESOP'  " & _
            "	when substr(strAIRPollutantKey, 13, 1) = 'A' then 'A - Acid Precip.'  " & _
            "	when substr(strAIRPollutantKey, 13, 1) = 'I' then 'I - Native American'  " & _
            "	when substr(strAIRPollutantKey, 13, 1) = 'M' then 'M - MACT'  " & _
            "	when substr(strAIRPollutantKey, 13, 1) = 'V' then 'V - Title V'  " & _
            "ELSE 'ERROR'  " & _
            "End as AirProgram,   " & _
            "strPollutantDescription,   " & _
            "(strComplianceCode || ' - ' || strComplianceDesc) as ComplianceDesc,  " & _
            "datModifingDate,  " & _
            "(strLastName||', '||strFirstName) as ModifingPerson       " & _
            "from " & DBNameSpace & ".APBAirProgramPollutants, " & DBNameSpace & ".LookUPPollutants,   " & _
            "" & DBNameSpace & ".LookUpComplianceStatus, " & DBNameSpace & ".EPDUserProfiles  " & _
            "where " & DBNameSpace & ".APBAirProgramPollutants.strPollutantKey = " & DBNameSpace & ".LookUPPollutants.strPOllutantCode   " & _
            "and " & DBNameSpace & ".LookUpComplianceStatus.strComplianceCode = " & DBNameSpace & ".APBAirProgramPollutants.strComplianceStatus " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".APBAirProgramPollutants.strModifingPerson  " & _
            "and strAIRSNumber = '0413" & txtAirsNumber.Text & "' " & _
            "Order by AirProgram "

            dsDataGrid = New DataSet

            daDataGrid = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daDataGrid.Fill(dsDataGrid, "DataGrid")
            dgvAirProgramPollutants.DataSource = dsDataGrid
            dgvAirProgramPollutants.DataMember = "DataGrid"

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

            dgvAirProgramPollutants.RowHeadersVisible = False
            dgvAirProgramPollutants.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvAirProgramPollutants.AllowUserToResizeColumns = True
            dgvAirProgramPollutants.AllowUserToAddRows = False
            dgvAirProgramPollutants.AllowUserToDeleteRows = False
            dgvAirProgramPollutants.AllowUserToOrderColumns = True
            dgvAirProgramPollutants.AllowUserToResizeRows = True
            dgvAirProgramPollutants.Columns("AirProgram").HeaderText = "Air Program"
            dgvAirProgramPollutants.Columns("AirProgram").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgvAirProgramPollutants.Columns("AirProgram").DisplayIndex = 0
            dgvAirProgramPollutants.Columns("strPollutantDescription").HeaderText = "Pollutant"
            dgvAirProgramPollutants.Columns("strPollutantDescription").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvAirProgramPollutants.Columns("strPollutantDescription").DisplayIndex = 1
            dgvAirProgramPollutants.Columns("ComplianceDesc").HeaderText = "Compliance Status"
            dgvAirProgramPollutants.Columns("ComplianceDesc").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvAirProgramPollutants.Columns("ComplianceDesc").DisplayIndex = 2
            dgvAirProgramPollutants.Columns("datModifingDate").HeaderText = "Last Modified"
            dgvAirProgramPollutants.Columns("datModifingDate").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvAirProgramPollutants.Columns("datModifingDate").DisplayIndex = 3
            dgvAirProgramPollutants.Columns("ModifingPerson").HeaderText = "Modifying Person"
            dgvAirProgramPollutants.Columns("ModifingPerson").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvAirProgramPollutants.Columns("ModifingPerson").DisplayIndex = 4

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub LoadEnforcementPollutants()
        Try
            Dim Pollutants As String = ""
            Dim AirProgram As String = ""
            Dim Pollutant As String = ""
            Dim temp As String = ""

            SQL = "Select strPollutants " & _
            "from " & DBNameSpace & ".SSCPEnforcement " & _
            "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("strPollutants")) Then
                    Pollutants = ""
                Else
                    Pollutants = dr.Item("strPollutants")
                End If
            End If
            dr.Close()

            If Pollutants <> "" Then
                Do While Pollutants <> ""
                    temp = Mid(Pollutants, 1, (InStr(Pollutants, ",", CompareMethod.Text) - 1))
                    Pollutants = Mid(Pollutants, (InStr(Pollutants, ",", CompareMethod.Text) + 1))
                    AirProgram = Mid(temp, 1, 1)
                    Pollutant = Mid(temp, 2)

                    SQL = "Select " & _
                    "strPollutantDescription, strComplianceStatus " & _
                    "from " & DBNameSpace & ".LookUPPollutants, " & DBNameSpace & ".APBAirProgramPollutants " & _
                    "where strAirPollutantKey = '0413" & txtAirsNumber.Text & AirProgram & "' " & _
                    "and strPollutantKey = strPollutantCode  " & _
                    "and strPollutantKey = '" & Pollutant & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        Select Case AirProgram
                            Case "0"
                                temp = "(0 - SIP) - " & dr.Item("strPollutantDescription")
                            Case "1"
                                temp = "(1 - Fed. SIP) - " & dr.Item("strPollutantDescription")
                            Case "3"
                                temp = "(3 - Non Fed.) - " & dr.Item("strPollutantDescription")
                            Case "4"
                                temp = "(4 - CFC Tracking) - " & dr.Item("strPollutantDescription")
                            Case "6"
                                temp = "(6 - PSD) - " & dr.Item("strPollutantDescription")
                            Case "7"
                                temp = "(7 - NSR) - " & dr.Item("strPollutantDescription")
                            Case "8"
                                temp = "(8 - NESHAP) - " & dr.Item("strPollutantDescription")
                            Case "9"
                                temp = "(9 - NSPS) - " & dr.Item("strPollutantDescription")
                            Case "F"
                                temp = "(F - FESOP) - " & dr.Item("strPollutantDescription")
                            Case "A"
                                temp = "(A - Acid Precip.) - " & dr.Item("strPollutantDescription")
                            Case "I"
                                temp = "(I - Native American) - " & dr.Item("strPollutantDescription")
                            Case "M"
                                temp = "(M - MACT) - " & dr.Item("strPollutantDescription")
                            Case "V"
                                temp = "(V - Title V) - " & dr.Item("strPollutantDescription")
                            Case Else
                                temp = ""
                        End Select
                        If clbEnforcementPollutants.Items.Contains(temp) = False Then
                            clbEnforcementPollutants.Items.Add(temp)
                            clbEnforcementPollutants.SetItemChecked(clbEnforcementPollutants.Items.IndexOf(temp), True)
                        End If
                    End If
                    dr.Close()
                Loop
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region
#Region "Subs and Functions"
    Sub Save()
        Try

            'If Mid(Permissions, 21, 5) = "00000" Then
            If UserProgram <> "4" And Mid(Permissions, 27, 2) = "0" And Mid(Permissions, 27, 3) = "0" _
               And Mid(Permissions, 27, 4) = "0" Then
                MsgBox("You do not have sufficent prilvages to Save Pollutant data.", MsgBoxStyle.Information, "Air Program Pollutants")
            Else
                Dim AIRSPollutantKey As String
                If cboAirProgramCodes.Text <> "" And _
                          cboPollutants.SelectedValue <> Nothing And cboPollutants.Text <> "" And _
                               cboComplianceStatus.SelectedValue <> Nothing And cboComplianceStatus.Text <> "" Then
                    Select Case cboAirProgramCodes.Text
                        Case "0 - SIP"
                            AIRSPollutantKey = "0413" & txtAirsNumber.Text & "0"
                        Case "1 - Fed. SIP"
                            AIRSPollutantKey = "0413" & txtAirsNumber.Text & "1"
                        Case "3 - Non Fed."
                            AIRSPollutantKey = "0413" & txtAirsNumber.Text & "3"
                        Case "4 - CFC Tracking"
                            AIRSPollutantKey = "0413" & txtAirsNumber.Text & "4"
                        Case "6 - PSD"
                            AIRSPollutantKey = "0413" & txtAirsNumber.Text & "6"
                        Case "7 - NSR"
                            AIRSPollutantKey = "0413" & txtAirsNumber.Text & "7"
                        Case "8 - NESHAP"
                            AIRSPollutantKey = "0413" & txtAirsNumber.Text & "8"
                        Case "9 - NSPS"
                            AIRSPollutantKey = "0413" & txtAirsNumber.Text & "9"
                        Case "F - FESOP"
                            AIRSPollutantKey = "0413" & txtAirsNumber.Text & "F"
                        Case "A - Acid Precip."
                            AIRSPollutantKey = "0413" & txtAirsNumber.Text & "A"
                        Case "I - Native American"
                            AIRSPollutantKey = "0413" & txtAirsNumber.Text & "I"
                        Case "M - MACT"
                            AIRSPollutantKey = "0413" & txtAirsNumber.Text & "M"
                        Case "V - Title V"
                            AIRSPollutantKey = "0413" & txtAirsNumber.Text & "V"
                        Case Else
                            AIRSPollutantKey = "0413" & txtAirsNumber.Text & "0"
                    End Select

                    SQL = "Select strairsnumber " & _
                    "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                    "where strAIRPollutantKey = '" & AIRSPollutantKey & "' " & _
                    "and strPollutantKey = '" & cboPollutants.SelectedValue & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read

                    dr.Close()

                    If recExist = True Then
                        Dim AirProgramCodes As String
                        Dim ProgramStatus As String

                        SQL = "select strAirProgramCodes " & _
                        "from " & DBNameSpace & ".APBHeaderData  " & _
                        "where strAIRSnumber = '0413" & txtAirsNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        If recExist = True Then
                            AirProgramCodes = dr.Item("strAirProgramCodes")
                        Else
                            AirProgramCodes = "000000000000000 "
                        End If
                        dr.Close()

                        Select Case cboAirProgramCodes.Text
                            Case "0 - SIP"
                                If Mid(AirProgramCodes, 1, 1) = "1" Then
                                    ProgramStatus = "True"
                                Else
                                    ProgramStatus = "False"
                                End If
                            Case "1 - Fed. SIP"
                                If Mid(AirProgramCodes, 2, 1) = "1" Then
                                    ProgramStatus = "True"
                                Else
                                    ProgramStatus = "False"
                                End If
                            Case "3 - Non Fed."
                                If Mid(AirProgramCodes, 3, 1) = "1" Then
                                    ProgramStatus = "True"
                                Else
                                    ProgramStatus = "False"
                                End If
                            Case "4 - CFC Tracking"
                                If Mid(AirProgramCodes, 4, 1) = "1" Then
                                    ProgramStatus = "True"
                                Else
                                    ProgramStatus = "False"
                                End If
                            Case "6 - PSD"
                                If Mid(AirProgramCodes, 5, 1) = "1" Then
                                    ProgramStatus = "True"
                                Else
                                    ProgramStatus = "False"
                                End If
                            Case "7 - NSR"
                                If Mid(AirProgramCodes, 6, 1) = "1" Then
                                    ProgramStatus = "True"
                                Else
                                    ProgramStatus = "False"
                                End If
                            Case "8 - NESHAP"
                                If Mid(AirProgramCodes, 7, 1) = "1" Then
                                    ProgramStatus = "True"
                                Else
                                    ProgramStatus = "False"
                                End If
                            Case "9 - NSPS"
                                If Mid(AirProgramCodes, 8, 1) = "1" Then
                                    ProgramStatus = "True"
                                Else
                                    ProgramStatus = "False"
                                End If
                            Case "F - FESOP"
                                If Mid(AirProgramCodes, 9, 1) = "1" Then
                                    ProgramStatus = "True"
                                Else
                                    ProgramStatus = "False"
                                End If
                            Case "A - Acid Precip."
                                If Mid(AirProgramCodes, 10, 1) = "1" Then
                                    ProgramStatus = "True"
                                Else
                                    ProgramStatus = "False"
                                End If
                            Case "I - Native American"
                                If Mid(AirProgramCodes, 11, 1) = "1" Then
                                    ProgramStatus = "True"
                                Else
                                    ProgramStatus = "False"
                                End If
                            Case "M - MACT"
                                If Mid(AirProgramCodes, 12, 1) = "1" Then
                                    ProgramStatus = "True"
                                Else
                                    ProgramStatus = "False"
                                End If
                            Case "V - Title V"
                                If Mid(AirProgramCodes, 13, 1) = "1" Then
                                    ProgramStatus = "True"
                                Else
                                    ProgramStatus = "False"
                                End If
                            Case Else
                                ProgramStatus = "False"
                        End Select
                        If ProgramStatus = "True" Then
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strComplianceStatus = '" & cboComplianceStatus.SelectedValue & "', " & _
                            "strModifingperson = '" & UserGCode & "', " & _
                            "datModifingdate = '" & OracleDate & "' " & _
                            "where strAIRPollutantKey = '" & AIRSPollutantKey & "' " & _
                            "and strAIRSnumber = '0413" & txtAirsNumber.Text & "' " & _
                            "and strPOllutantKey = '" & cboPollutants.SelectedValue & "' "
                        Else
                            SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                            "strComplianceStatus = '9', " & _
                            "strModifingperson = '" & UserGCode & "', " & _
                            "datModifingdate = '" & OracleDate & "', " & _
                            "strOperationalStatus = 'X' " & _
                            "where strAIRPollutantKey = '" & AIRSPollutantKey & "' " & _
                            "and strAIRSnumber = '0413" & txtAirsNumber.Text & "' " & _
                            "and strPOllutantKey = '" & cboPollutants.SelectedValue & "' "
                        End If
                    Else
                        SQL = "Insert into " & DBNameSpace & ".APBAirProgramPollutants " & _
                        "(strAIRSnumber, strAIRPollutantKey, " & _
                        "strPOllutantKey, strComplianceStatus, " & _
                        "strModifingperson, datModifingdate, " & _
                        "strOperationalStatus) " & _
                        "values " & _
                        "('0413" & txtAirsNumber.Text & "', '" & AIRSPollutantKey & "', " & _
                        "'" & cboPollutants.SelectedValue & "', '" & cboComplianceStatus.SelectedValue & "', " & _
                        "'" & UserGCode & "', '" & OracleDate & "', 'O') "
                        txtModifingPerson.Text = UserName
                        txtModifingDate.Text = OracleDate
                    End If

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    Try

                        dr = cmd.ExecuteReader
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    End Try

                    If CurrentConnection.State = ConnectionState.Open Then
                        'conn.close()
                    End If
                    MsgBox("Pollutant added to Air Program Code.", MsgBoxStyle.Information, "Edit Air Program Code Pollutants")

                    LoadDataGrid()
                    If MultiForm IsNot Nothing AndAlso MultiForm.ContainsKey(SscpEnforcement.Name) Then
                        For Each kvp As KeyValuePair(Of Integer, BaseForm) In MultiForm(SscpEnforcement.Name)
                            Dim enf As SscpEnforcement = kvp.Value
                            enf.LoadEnforcementPollutants2()
                        Next
                    End If

                    'If SSCP_Enforcement IsNot Nothing Then
                    '    SSCP_Enforcement.LoadEnforcementPollutants2()
                    'End If
                Else
                    MsgBox("No Data Saved", MsgBoxStyle.Exclamation, "Edit Air Program Code Pollutants")
                End If

                If CurrentConnection.State = ConnectionState.Open Then
                    'conn.close()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub EditCompliancePollutants()
        Try

            Dim strObject As Object
            Dim temp As String = ""
            Dim AirProgram As String = ""
            Dim Pollutant As String = ""
            Dim PollutantList As String = ""

            For Each strObject In clbEnforcementPollutants.CheckedItems
                temp = strObject
                AirProgram = Mid(strObject, 2, 1)
                Pollutant = Mid(strObject, InStr(strObject, ") - ", CompareMethod.Text) + 4)

                SQL = "Select strPollutantKey " & _
                "from " & DBNameSpace & ".APBAirProgramPollutants, " & DBNameSpace & ".LookUPPollutants " & _
                "where " & DBNameSpace & ".APBAirProgramPollutants.strPollutantKey = " & DBNameSpace & ".LookUPPollutants.strPollutantCode " & _
                "and strAirPollutantKey = '0413" & txtAirsNumber.Text & AirProgram & "' " & _
                "and strPollutantDescription = '" & Pollutant & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("strPollutantKey")) Then
                        Pollutant = ""
                    Else
                        Pollutant = dr.Item("strPollutantKey")
                    End If
                End If
                dr.Close()

                If Pollutant <> "" Then
                    PollutantList = PollutantList & AirProgram & Pollutant & ","
                Else
                    'PollutantList = PollutantList
                End If
            Next

            SQL = "Update " & DBNameSpace & ".SSCPEnforcement set " & _
            "strPollutants = '" & PollutantList & "' " & _
            "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub Back()
        Try


            EditAirProgramPollutants = Nothing

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region
#Region "Declarations"
    Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSave.Click
        Try

            Save()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Try

            Back()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#Region "Main Menu"
    Private Sub mmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSave.Click
        Try

            Save()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiBack.Click
        Try

            Back()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("(^X)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCopy.Click
        Try

            SendKeys.Send("(^C)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPaste.Click
        Try

            SendKeys.Send("(^V)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try

            Help.ShowHelp(Label1, HelpUrl)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region
    Private Sub APBEditAIRProgramPollutants_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            EditAirProgramPollutants = Nothing
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub dgvAirProgramPollutants_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvAirProgramPollutants.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvAirProgramPollutants.HitTest(e.X, e.Y)

        Try

            Dim temp As String = ""
            Dim temp2 As String = ""

            If dgvAirProgramPollutants.RowCount > 0 And hti.RowIndex <> -1 Then
                cboAirProgramCodes.Text = dgvAirProgramPollutants(0, hti.RowIndex).Value
                cboPollutants.Text = dgvAirProgramPollutants(1, hti.RowIndex).Value
                cboComplianceStatus.Text = dgvAirProgramPollutants(2, hti.RowIndex).Value
                txtModifingDate.Text = dgvAirProgramPollutants(3, hti.RowIndex).Value
                txtModifingPerson.Text = dgvAirProgramPollutants(4, hti.RowIndex).Value
                temp = "(" & dgvAirProgramPollutants(0, hti.RowIndex).Value & ") - " & dgvAirProgramPollutants(1, hti.RowIndex).Value
            End If

            If clbEnforcementPollutants.Items.Contains(temp) = False Then
                clbEnforcementPollutants.Items.Add(temp)
                temp2 = clbEnforcementPollutants.Items.IndexOf(temp)
                clbEnforcementPollutants.SetItemChecked(temp2, True)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnAddPollutants_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPollutants.Click
        Try

            If txtEnforcementNumber.Text <> "" Then
                EditCompliancePollutants()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnSaveNewPollutant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveNewPollutant.Click
        Try

            Save()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnRemovePollutantsfromList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePollutantsfromList.Click
        Try
            Dim strObject As Object
            Dim temp As String = ""
            Dim count As Integer = 0
            Dim i As Integer = 0
            Dim uncheckeditems As CheckedListBox.CheckedItemCollection = clbEnforcementPollutants.CheckedItems
            Dim CheckedItems(count) As String

            For Each strObject In uncheckeditems
                ReDim Preserve CheckedItems(count)
                CheckedItems(count) = strObject.ToString()
                count += 1
            Next

            clbEnforcementPollutants.Items.Clear()

            For i = 0 To count - 1
                clbEnforcementPollutants.Items.Add(CheckedItems(i))
                clbEnforcementPollutants.SetItemChecked(i, True)
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region


End Class