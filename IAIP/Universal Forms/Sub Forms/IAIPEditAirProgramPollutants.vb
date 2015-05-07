Imports Oracle.DataAccess.Client
Imports System.Collections.Generic

Public Class IAIPEditAirProgramPollutants
    Dim SQL, SQL2 As String
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
    End Sub

    Private Sub IAIPEditAirProgramPollutants_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            LoadTop()
            LoadDataSets()
            LoadPollutantCombos()
            LoadDataGrid()

            If AccountFormAccess(27, 3) = "1" Or AccountFormAccess(27, 2) = "1" Or (UserBranch = "1" And UserUnit = "---") Then
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
            "from AIRBRANCH.APBHeaderData, AIRBRANCH.APBFacilityInformation " & _
            "where AIRBRANCH.APBHeaderData.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSnumber " & _
            "and AIRBRANCH.APBHeaderData.strAIRSNumber = '0413" & AirsNumberDisplay.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            While dr.Read

                'AirPrograms = dr.Item("Programs")
                AirPrograms = dr.Item("strAirProgramCodes")
                FacilityNameDisplay.Text = dr.Item("strFacilityName")

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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub LoadDataSets()
        Try

            SQL = "Select strPollutantDescription, " & _
            "strPollutantCode " & _
            "from AIRBRANCH.LookUPPollutants " & _
            "where strAFSCode = 'True' " & _
            "order by strPollutantDescription "

            SQL2 = "Select " & _
            "strComplianceCode, " & _
            "(strComplianceCode || ' - ' || strComplianceDesc) as ComplianceDesc " & _
            "from AIRBRANCH.LookUpComplianceStatus " & _
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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "from AIRBRANCH.APBAirProgramPollutants, AIRBRANCH.LookUPPollutants,   " & _
            "AIRBRANCH.LookUpComplianceStatus, AIRBRANCH.EPDUserProfiles  " & _
            "where AIRBRANCH.APBAirProgramPollutants.strPollutantKey = AIRBRANCH.LookUPPollutants.strPOllutantCode   " & _
            "and AIRBRANCH.LookUpComplianceStatus.strComplianceCode = AIRBRANCH.APBAirProgramPollutants.strComplianceStatus " & _
            "and AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.APBAirProgramPollutants.strModifingPerson  " & _
            "and strAIRSNumber = '0413" & AirsNumberDisplay.Text & "' " & _
            "Order by AirProgram "

            dsDataGrid = New DataSet

            daDataGrid = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daDataGrid.Fill(dsDataGrid, "DataGrid")
            dgvAirProgramPollutants.DataSource = dsDataGrid
            dgvAirProgramPollutants.DataMember = "DataGrid"

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
        End Try
    End Sub
#End Region

    Sub Save()
        Try

            'If Mid(Permissions, 21, 5) = "00000" Then
            If UserProgram <> "4" And Mid(UserAccounts, 27, 2) = "0" And Mid(UserAccounts, 27, 3) = "0" _
               And Mid(UserAccounts, 27, 4) = "0" Then
                MsgBox("You do not have sufficent prilvages to Save Pollutant data.", MsgBoxStyle.Information, "Air Program Pollutants")
            Else
                Dim AIRSPollutantKey As String
                If cboAirProgramCodes.Text <> "" And _
                          cboPollutants.SelectedValue <> Nothing And cboPollutants.Text <> "" And _
                               cboComplianceStatus.SelectedValue <> Nothing And cboComplianceStatus.Text <> "" Then
                    Select Case cboAirProgramCodes.Text
                        Case "0 - SIP"
                            AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "0"
                        Case "1 - Fed. SIP"
                            AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "1"
                        Case "3 - Non Fed."
                            AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "3"
                        Case "4 - CFC Tracking"
                            AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "4"
                        Case "6 - PSD"
                            AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "6"
                        Case "7 - NSR"
                            AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "7"
                        Case "8 - NESHAP"
                            AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "8"
                        Case "9 - NSPS"
                            AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "9"
                        Case "F - FESOP"
                            AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "F"
                        Case "A - Acid Precip."
                            AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "A"
                        Case "I - Native American"
                            AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "I"
                        Case "M - MACT"
                            AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "M"
                        Case "V - Title V"
                            AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "V"
                        Case Else
                            AIRSPollutantKey = "0413" & AirsNumberDisplay.Text & "0"
                    End Select

                    SQL = "Select strairsnumber " & _
                    "from AIRBRANCH.APBAirProgramPollutants " & _
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
                        "from AIRBRANCH.APBHeaderData  " & _
                        "where strAIRSnumber = '0413" & AirsNumberDisplay.Text & "' "
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
                            SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                            "strComplianceStatus = '" & cboComplianceStatus.SelectedValue & "', " & _
                            "strModifingperson = '" & UserGCode & "', " & _
                            "datModifingdate = '" & OracleDate & "' " & _
                            "where strAIRPollutantKey = '" & AIRSPollutantKey & "' " & _
                            "and strAIRSnumber = '0413" & AirsNumberDisplay.Text & "' " & _
                            "and strPOllutantKey = '" & cboPollutants.SelectedValue & "' "
                        Else
                            SQL = "Update AIRBRANCH.APBAirProgramPollutants set " & _
                            "strComplianceStatus = '9', " & _
                            "strModifingperson = '" & UserGCode & "', " & _
                            "datModifingdate = '" & OracleDate & "', " & _
                            "strOperationalStatus = 'X' " & _
                            "where strAIRPollutantKey = '" & AIRSPollutantKey & "' " & _
                            "and strAIRSnumber = '0413" & AirsNumberDisplay.Text & "' " & _
                            "and strPOllutantKey = '" & cboPollutants.SelectedValue & "' "
                        End If
                    Else
                        SQL = "Insert into AIRBRANCH.APBAirProgramPollutants " & _
                        "(strAIRSnumber, strAIRPollutantKey, " & _
                        "strPOllutantKey, strComplianceStatus, " & _
                        "strModifingperson, datModifingdate, " & _
                        "strOperationalStatus) " & _
                        "values " & _
                        "('0413" & AirsNumberDisplay.Text & "', '" & AIRSPollutantKey & "', " & _
                        "'" & cboPollutants.SelectedValue & "', '" & cboComplianceStatus.SelectedValue & "', " & _
                        "'" & UserGCode & "', '" & OracleDate & "', 'O') "
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

                    MsgBox("Pollutant added to Air Program Code.", MsgBoxStyle.Information, "Edit Air Program Code Pollutants")

                    LoadDataGrid()
                    If MultiForm IsNot Nothing AndAlso MultiForm.ContainsKey(SscpEnforcement.Name) Then
                        For Each kvp As KeyValuePair(Of Integer, BaseForm) In MultiForm(SscpEnforcement.Name)
                            Dim enf As SscpEnforcement = kvp.Value
                            enf.LoadEnforcementPollutants2()
                        Next
                    End If

                Else
                    MsgBox("No Data Saved", MsgBoxStyle.Exclamation, "Edit Air Program Code Pollutants")
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveNewPollutant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveNewPollutant.Click
        Save()
    End Sub

    Private Sub dgvAirProgramPollutants_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvAirProgramPollutants.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvAirProgramPollutants.HitTest(e.X, e.Y)

            If dgvAirProgramPollutants.RowCount > 0 And hti.RowIndex <> -1 Then
                cboAirProgramCodes.Text = dgvAirProgramPollutants(0, hti.RowIndex).Value
                cboPollutants.Text = dgvAirProgramPollutants(1, hti.RowIndex).Value
                cboComplianceStatus.Text = dgvAirProgramPollutants(2, hti.RowIndex).Value
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class