Imports Oracle.DataAccess.Client


Public Class IAIPEditSubParts
    Dim SQL, SQL2, SQL3, SQL4 As String
    Dim cmd, cmd2 As OracleCommand
    Dim dr, dr2 As OracleDataReader
    Dim recExist As Boolean
    Dim dsPart60 As DataSet
    Dim daPart60 As OracleDataAdapter
    Dim dsPart61 As DataSet
    Dim daPart61 As OracleDataAdapter
    Dim dsPart63 As DataSet
    Dim daPart63 As OracleDataAdapter
    Dim dsSIP As DataSet
    Dim daSIP As OracleDataAdapter

    Private Sub DevEditSubParts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            LoadSubPartData()
            SetPermissions()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#Region "Page Load"
    Sub LoadSubPartData()
        Try
            Dim dtPart60 As New DataTable
            Dim dtPart61 As New DataTable
            Dim dtPart63 As New DataTable
            Dim dtSIP As New DataTable
            Dim drDSRow As DataRow
            Dim drDSRow2 As DataRow
            Dim drDSRow3 As DataRow
            Dim drDSRow4 As DataRow
            Dim drNewRow As DataRow

            SQL = "Select * from " & DBNameSpace & ".LookupSubPart60 order by strSubpart "
            SQL2 = "Select * from " & DBNameSpace & ".LookupSubPart61 order by strSubpart "
            SQL3 = "Select * from " & DBNameSpace & ".LookupSubPart63 order by strSubpart "
            SQL4 = "Select * from " & DBNameSpace & ".LookUpSubPartSIP order by strSubPart "

            dsPart60 = New DataSet
            dsPart61 = New DataSet
            dsPart63 = New DataSet
            dsSIP = New DataSet

            daPart60 = New OracleDataAdapter(SQL, CurrentConnection)
            daPart61 = New OracleDataAdapter(SQL2, CurrentConnection)
            daPart63 = New OracleDataAdapter(SQL3, CurrentConnection)
            daSIP = New OracleDataAdapter(SQL4, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daPart60.Fill(dsPart60, "Part60")
            daPart61.Fill(dsPart61, "Part61")
            daPart63.Fill(dsPart63, "Part63")
            daSIP.Fill(dsSIP, "SIP")

            dgvNSPS.DataSource = dsPart60
            dgvNSPS.DataMember = "Part60"
            dgvNESHAP.DataSource = dsPart61
            dgvNESHAP.DataMember = "Part61"
            dgvMACT.DataSource = dsPart63
            dgvMACT.DataMember = "Part63"
            dgvSIP.DataSource = dsSIP
            dgvSIP.DataMember = "SIP"

            dgvNSPS.RowHeadersVisible = False
            dgvNSPS.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNSPS.AllowUserToResizeColumns = True
            dgvNSPS.AllowUserToAddRows = False
            dgvNSPS.AllowUserToDeleteRows = False
            dgvNSPS.AllowUserToOrderColumns = True
            dgvNSPS.AllowUserToResizeRows = True
            dgvNSPS.Columns("strSubPart").HeaderText = "Subpart Code"
            dgvNSPS.Columns("strSubPart").DisplayIndex = 0
            dgvNSPS.Columns("strDescription").HeaderText = "Description"
            dgvNSPS.Columns("strdescription").DisplayIndex = 1
            dgvNSPS.Columns("strdescription").Width = 500

            dgvNESHAP.RowHeadersVisible = False
            dgvNESHAP.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNESHAP.AllowUserToResizeColumns = True
            dgvNESHAP.AllowUserToAddRows = False
            dgvNESHAP.AllowUserToDeleteRows = False
            dgvNESHAP.AllowUserToOrderColumns = True
            dgvNESHAP.AllowUserToResizeRows = True
            dgvNESHAP.Columns("strSubPart").HeaderText = "Subpart Code"
            dgvNESHAP.Columns("strSubPart").DisplayIndex = 0
            dgvNESHAP.Columns("strDescription").HeaderText = "Description"
            dgvNESHAP.Columns("strdescription").DisplayIndex = 1
            dgvNESHAP.Columns("strdescription").Width = 500

            dgvMACT.RowHeadersVisible = False
            dgvMACT.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvMACT.AllowUserToResizeColumns = True
            dgvMACT.AllowUserToAddRows = False
            dgvMACT.AllowUserToDeleteRows = False
            dgvMACT.AllowUserToOrderColumns = True
            dgvMACT.AllowUserToResizeRows = True
            dgvMACT.Columns("strSubPart").HeaderText = "Subpart Code"
            dgvMACT.Columns("strSubPart").DisplayIndex = 0
            dgvMACT.Columns("strDescription").HeaderText = "Description"
            dgvMACT.Columns("strdescription").DisplayIndex = 1
            dgvMACT.Columns("strdescription").Width = 500

            dgvSIP.RowHeadersVisible = False
            dgvSIP.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvSIP.AllowUserToResizeColumns = True
            dgvSIP.AllowUserToAddRows = False
            dgvSIP.AllowUserToDeleteRows = False
            dgvSIP.AllowUserToOrderColumns = True
            dgvSIP.AllowUserToResizeRows = True
            dgvSIP.Columns("strSubPart").HeaderText = "Subpart Code"
            dgvSIP.Columns("strSubPart").DisplayIndex = 0
            dgvSIP.Columns("strDescription").HeaderText = "Description"
            dgvSIP.Columns("strdescription").DisplayIndex = 1
            dgvSIP.Columns("strdescription").Width = 500

            dtSIP.Columns.Add("strSubPart", GetType(System.String))
            dtSIP.Columns.Add("strDescription", GetType(System.String))

            drNewRow = dtSIP.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("strDescription") = " "
            dtSIP.Rows.Add(drNewRow)

            For Each drDSRow4 In dsSIP.Tables("SIP").Rows()
                drNewRow = dtSIP.NewRow()
                drNewRow("strSubPart") = drDSRow4("strSubPart")
                drNewRow("strDescription") = drDSRow4("strSubPart") & " - " & drDSRow4("strDescription")
                dtSIP.Rows.Add(drNewRow)
            Next

            With cboSIPSubpart
                .DataSource = dtSIP
                .DisplayMember = "strDescription"
                .ValueMember = "strSubPart"
                .SelectedIndex = 0
            End With

            dtPart60.Columns.Add("strSubPart", GetType(System.String))
            dtPart60.Columns.Add("strDescription", GetType(System.String))

            drNewRow = dtPart60.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("strDescription") = " "
            dtPart60.Rows.Add(drNewRow)

            For Each drDSRow In dsPart60.Tables("Part60").Rows()
                drNewRow = dtPart60.NewRow()
                drNewRow("strSubPart") = drDSRow("strSubPart")
                drNewRow("strDescription") = drDSRow("strSubPart") & " - " & drDSRow("strDescription")
                'drNewRow("strDescription") = drDSRow("strDescription")
                dtPart60.Rows.Add(drNewRow)
            Next

            With cboNSPSSubpart
                .DataSource = dtPart60
                .DisplayMember = "strDescription"
                .ValueMember = "strSubPart"
                .SelectedIndex = 0
            End With

            dtPart61.Columns.Add("strSubPart", GetType(System.String))
            dtPart61.Columns.Add("strDescription", GetType(System.String))

            drNewRow = dtPart61.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("strDescription") = " "
            dtPart61.Rows.Add(drNewRow)

            For Each drDSRow2 In dsPart61.Tables("Part61").Rows()
                drNewRow = dtPart61.NewRow()
                drNewRow("strSubPart") = drDSRow2("strSubPart")
                drNewRow("strDescription") = drDSRow2("strSubPart") & " - " & drDSRow2("strDescription")
                dtPart61.Rows.Add(drNewRow)
            Next

            With cboNESHAPSubpart
                .DataSource = dtPart61
                .DisplayMember = "strDescription"
                .ValueMember = "strSubPart"
                .SelectedIndex = 0
            End With

            dtPart63.Columns.Add("strSubPart", GetType(System.String))
            dtPart63.Columns.Add("strDescription", GetType(System.String))

            drNewRow = dtPart63.NewRow()
            drNewRow("strSubPart") = " "
            drNewRow("strDescription") = " "
            dtPart63.Rows.Add(drNewRow)

            For Each drDSRow3 In dsPart63.Tables("Part63").Rows()
                drNewRow = dtPart63.NewRow()
                drNewRow("strSubPart") = drDSRow3("strSubPart")
                drNewRow("strDescription") = drDSRow3("strSubPart") & " - " & drDSRow3("strDescription")
                dtPart63.Rows.Add(drNewRow)
            Next

            With cboMACTSubPart
                .DataSource = dtPart63
                .DisplayMember = "strDescription"
                .ValueMember = "strSubPart"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub SetPermissions()
        Try
            If _
                AccountFormAccess(26, 1) = "1" Or AccountFormAccess(26, 2) = "1" Or _
                AccountFormAccess(26, 3) = "1" Or AccountFormAccess(26, 4) = "1" Or _
                UserAccounts.Contains("(19)") Or UserAccounts.Contains("(113)") Or _
                UserAccounts.Contains("(114)") Or UserAccounts.Contains("(141)") _
            Then
            Else
                btnSaveSIPSubpart.Enabled = False
                btnRemoveSIPSubpart.Enabled = False
                btnSaveNSPSSubpart.Enabled = False
                btnRemoveNSPSSubpart.Enabled = False
                btnSaveNESHAPSubpart.Enabled = False
                btnRemoveNESHAPSubpart.Enabled = False
                btnAddMACTSubpart.Enabled = False
                btnremoveMACTSubPart.Enabled = False
                btnEditSIP.Enabled = False
                btnDeleteSIPSubpart.Enabled = False
                btnEditNSPS.Enabled = False
                btnDeleteNSPSSubpart.Enabled = False
                btnEditNESHAP.Enabled = False
                btnDeleteNESHAPSubpart.Enabled = False
                btnEditMACT.Enabled = False
                btnDeleteMACTSubpart.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
#End Region
#Region "Subs and Functions"
    Sub LoadInformation()
        Try

            Dim AirProgramCodes As String = ""
            Dim Program As String = ""
            Dim SubPart As String = ""

            If txtAIRSNumber.Text <> "" Then
                SQL = "Select " & _
                "strFacilityName, strAirProgramCodes  " & _
                "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".APBHeaderData " & _
                "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSnumber " & _
                "and " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber = '0413" & txtAIRSNumber.Text & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    dr.Close()
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            txtFacilityName.Text = "ERROR"
                        Else
                            txtFacilityName.Text = dr.Item("strFacilityName")
                        End If
                        If IsDBNull(dr.Item("strAirProgramCodes")) Then
                            AirProgramCodes = "000000000000000"
                        Else
                            AirProgramCodes = dr.Item("strAirProgramCodes")
                        End If
                    End While
                Else
                    txtFacilityName.Text = "ERROR"
                    AirProgramCodes = "000000000000000"
                    clbSIP.Items.Clear()
                    clbNSPS.Items.Clear()
                    clbNESHAP.Items.Clear()
                    clbMACT.Items.Clear()
                End If

                SQL = "Select " & _
                "strSubPartKey, strSubPart " & _
                "from " & DBNameSpace & ".APBSubpartData " & _
                "where " & DBNameSpace & ".APBSubpartData.strAIRSnumber = '0413" & txtAIRSNumber.Text & "' " & _
                "and Active = '1' " & _
                "order by strSubPart "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    dr.Close()
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strSubPartKey")) Then
                            Program = ""
                        Else
                            Program = Mid(dr.Item("strSubPartKey"), 13)
                            If IsDBNull(dr.Item("strSubPart")) Then
                                SubPart = ""
                            Else
                                SubPart = dr.Item("strSubPart")
                            End If
                            Select Case Program
                                Case "0"
                                    LoadSIPDescription(SubPart)
                                Case "9"
                                    LoadNSPSDescription(SubPart)
                                Case "8"
                                    LoadNESHAPDescription(SubPart)
                                Case "M"
                                    LoadMACTDescription(SubPart)
                                Case Else
                            End Select
                        End If
                    End While
                Else
                    clbSIP.Items.Clear()
                    clbNSPS.Items.Clear()
                    clbNESHAP.Items.Clear()
                    clbMACT.Items.Clear()
                End If

            Else
                txtFacilityName.Text = "ERROR"
                AirProgramCodes = "000000000000000"
                clbSIP.Items.Clear()
                clbNSPS.Items.Clear()
                clbNESHAP.Items.Clear()
                clbMACT.Items.Clear()
            End If

            If Mid(AirProgramCodes, 1, 1) = "0" Then
                TCSupParts.TabPages.Remove(TPSIP)
                TCMiscTools.TabPages.Remove(Me.TPEditSIP)
            End If
            If Mid(AirProgramCodes, 8, 1) = "0" Then
                TCSupParts.TabPages.Remove(TPPart60)
                TCMiscTools.TabPages.Remove(Me.TPEditNSPS)
            End If
            If Mid(AirProgramCodes, 7, 1) = "0" Then
                TCSupParts.TabPages.Remove(TPPart61)
                TCMiscTools.TabPages.Remove(TPEditNESHAP)
            End If
            If Mid(AirProgramCodes, 12, 1) = "0" Then
                TCSupParts.TabPages.Remove(TPPart63)
                TCMiscTools.TabPages.Remove(TPEditMACT)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadSIPDescription(ByVal SubPart As String)
        Try


            SQL = "Select strDescription " & _
            "from " & DBNameSpace & ".LookupSubPartSIP " & _
            "where strSubPart = '" & SubPart & "' "
            cmd2 = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr2 = cmd2.ExecuteReader
            While dr2.Read
                If IsDBNull(dr2.Item("strDescription")) Then
                Else
                    If clbSIP.Items.Contains(SubPart & " - " & dr2.Item("strDescription")) Then
                    Else
                        clbSIP.Items.Add(SubPart & " - " & dr2.Item("strDescription"))
                    End If
                End If
            End While
            dr2.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub LoadNSPSDescription(ByVal SubPart As String)
        Try


            SQL = "Select strDescription " & _
            "from " & DBNameSpace & ".LookupSubPart60 " & _
            "where strSubPart = '" & SubPart & "' "
            cmd2 = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr2 = cmd2.ExecuteReader
            While dr2.Read
                If IsDBNull(dr2.Item("strDescription")) Then
                Else
                    clbNSPS.Items.Add(SubPart & " - " & dr2.Item("strDescription"))
                End If
            End While
            dr2.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub LoadNESHAPDescription(ByVal SubPart As String)
        Try

            SQL = "Select strDescription " & _
                       "from " & DBNameSpace & ".LookupSubPart61 " & _
                       "where strSubPart = '" & SubPart & "' "
            cmd2 = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr2 = cmd2.ExecuteReader
            While dr2.Read
                If IsDBNull(dr2.Item("strDescription")) Then
                Else
                    clbNESHAP.Items.Add(SubPart & " - " & dr2.Item("strDescription"))
                End If
            End While
            dr2.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub LoadMACTDescription(ByVal SubPart As String)
        Try

            SQL = "Select strDescription " & _
                       "from " & DBNameSpace & ".LookupSubPart63 " & _
                       "where strSubPart = '" & SubPart & "' "
            cmd2 = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr2 = cmd2.ExecuteReader
            While dr2.Read
                If IsDBNull(dr2.Item("strDescription")) Then
                Else
                    clbMACT.Items.Add(SubPart & " - " & dr2.Item("strDescription"))
                End If
            End While
            dr2.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub SaveSIPSubpart()
        Try
            SQL = "Select " & _
            "strSubPart " & _
            "from " & DBNameSpace & ".APBSubpartData " & _
            "where strSubpartKey = '0413" & txtAIRSNumber.Text & "0' " & _
            "and strSubpart = '" & cboSIPSubpart.SelectedValue & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
                "Active = '1', " & _
                "UpdateUser = '" & UserGCode & "', " & _
                "updateDateTime = '" & OracleDate & "' " & _
                "where strSubpartKey = '0413" & txtAIRSNumber.Text & "0' " & _
                "and strSubpart = '" & cboSIPSubpart.SelectedValue & "' "
            Else
                SQL = "Insert into " & DBNameSpace & ".APBSubpartData " & _
                "values " & _
                "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "0', " & _
                "'" & cboSIPSubpart.SelectedValue & "', '" & UserGCode & "', " & _
                "'" & OracleDate & "', '1', " & _
                "'" & OracleDate & "', NULL) "
            End If
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            LoadSIPDescription(cboSIPSubpart.SelectedValue)

            SQL = "Update " & DBNameSpace & ".AFSAirPollutantData set " & _
            "strUpdateStatus = 'C' " & _
            "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "0' " & _
            "and strUpdateStatus = 'N' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub SaveNSPSSubpart()
        Try
            SQL = "Select " & _
            "strSubPart " & _
            "from " & DBNameSpace & ".APBSubpartData " & _
            "where strSubpartKey = '0413" & txtAIRSNumber.Text & "9' " & _
            "and strSubpart = '" & cboNSPSSubpart.SelectedValue & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
                "Active = '1', " & _
                "UpdateUser = '" & UserGCode & "', " & _
                "updateDateTime = '" & OracleDate & "' " & _
                "where strSubpartKey = '0413" & txtAIRSNumber.Text & "9' " & _
                "and strSubpart = '" & cboNSPSSubpart.SelectedValue & "' "
            Else
                SQL = "Insert into " & DBNameSpace & ".APBSubpartData " & _
                "values " & _
                "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "9', " & _
                "'" & cboNSPSSubpart.SelectedValue & "', '" & UserGCode & "', " & _
                "'" & OracleDate & "', '1', " & _
                "'" & OracleDate & "', NULL) "
            End If
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            LoadNSPSDescription(cboNSPSSubpart.SelectedValue)

            SQL = "Update " & DBNameSpace & ".AFSAirPollutantData set " & _
            "strUpdateStatus = 'C' " & _
            "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "9' " & _
            "and strUpdateStatus = 'N' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub SaveNESHAPSubpart()
        Try
            SQL = "Select " & _
            "strSubPart " & _
            "from " & DBNameSpace & ".APBSubpartData " & _
            "where strSubpartKey = '0413" & txtAIRSNumber.Text & "8' " & _
            "and strSubpart = '" & cboNESHAPSubpart.SelectedValue & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
                "Active = '1', " & _
                "UpdateUser = '" & UserGCode & "', " & _
                "updateDateTime = '" & OracleDate & "' " & _
                "where strSubpartKey = '0413" & txtAIRSNumber.Text & "8' " & _
                "and strSubpart = '" & cboNESHAPSubpart.SelectedValue & "' "
            Else
                SQL = "Insert into " & DBNameSpace & ".APBSubpartData " & _
                "values " & _
                "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "8', " & _
                "'" & cboNESHAPSubpart.SelectedValue & "', '" & UserGCode & "', " & _
                "'" & OracleDate & "', '1', " & _
                "'" & OracleDate & "', NULL) "
            End If
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            LoadNESHAPDescription(cboNESHAPSubpart.SelectedValue)

            SQL = "Update " & DBNameSpace & ".AFSAirPollutantData set " & _
            "strUpdateStatus = 'C' " & _
            "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "8' " & _
            "and strUpdateStatus = 'N' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub SaveMACTSubpart()
        Try
            SQL = "Select " & _
            "strSubPart " & _
            "from " & DBNameSpace & ".APBSubpartData " & _
            "where strSubpartKey = '0413" & txtAIRSNumber.Text & "M' " & _
            "and strSubpart = '" & cboMACTSubPart.SelectedValue & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
                "Active = '1', " & _
                "UpdateUser = '" & UserGCode & "', " & _
                "updateDateTime = '" & OracleDate & "' " & _
                "where strSubpartKey = '0413" & txtAIRSNumber.Text & "M' " & _
                "and strSubpart = '" & cboMACTSubPart.SelectedValue & "' "
            Else
                SQL = "Insert into " & DBNameSpace & ".APBSubpartData " & _
                "values " & _
                "('0413" & txtAIRSNumber.Text & "', '0413" & txtAIRSNumber.Text & "M', " & _
                "'" & cboMACTSubPart.SelectedValue & "', '" & UserGCode & "', " & _
                "'" & OracleDate & "', '1', " & _
                "'" & OracleDate & "', NULL) "
            End If
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            LoadMACTDescription(cboMACTSubPart.SelectedValue)

            SQL = "Update " & DBNameSpace & ".AFSAirPollutantData set " & _
            "strUpdateStatus = 'C' " & _
            "where strAirPollutantKey = '0413" & txtAIRSNumber.Text & "M' " & _
            "and strUpdateStatus = 'N' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub DeleteSIPSubpart()
        Try

            Dim count As Integer = 0
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim temp(i) As String
            Dim Subpart As String = ""

            For count = 0 To (clbSIP.Items.Count - 1)
                If clbSIP.CheckedIndices.Contains(count) = True Then
                    Subpart = ""
                    Subpart = Mid(clbSIP.Items.Item(count), 1, ((clbSIP.Items.Item(count).ToString.IndexOf("-")) - 1))
                    SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
                    "active = '0', " & _
                    "UpdateUser = '" & UserGCode & "', " & _
                    "updateDateTime = '" & OracleDate & "' " & _
                    "where strSubPartKey = '0413" & txtAIRSNumber.Text & "0' " & _
                    "and strSubpart = '" & Subpart & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                     "values " & _
                     "(" & _
                     "(select " & _
                     "case when max(numCounter) is null then 1 " & _
                     "else max(numCounter) + 1 " & _
                     "end numCounter " & _
                     "from " & DBNameSpace & ".AFSDeletions), " & _
                     "'0413" & txtAIRSNumber.Text & "', " & _
                     "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                     "'" & OracleDate & "', '', " & _
                     "'') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    ReDim Preserve temp(i)
                    temp(i) = clbSIP.Items.Item(count)
                    i += 1

                End If
            Next
            For j = 0 To i - 1
                clbSIP.Items.Remove(temp(j))
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub DeleteNSPSSubpart()
        Try

            Dim count As Integer = 0
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim temp(i) As String
            Dim Subpart As String

            For count = 0 To (clbNSPS.Items.Count - 1)
                If clbNSPS.CheckedIndices.Contains(count) = True Then
                    Subpart = ""
                    Subpart = Mid(clbNSPS.Items.Item(count), 1, ((clbNSPS.Items.Item(count).ToString.IndexOf("-")) - 1))
                    SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
                    "active = '0', " & _
                    "UpdateUser = '" & UserGCode & "', " & _
                    "updateDateTime = '" & OracleDate & "' " & _
                    "where strSubPartKey = '0413" & txtAIRSNumber.Text & "9' " & _
                    "and strSubpart = '" & Subpart & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                     "values " & _
                     "(" & _
                     "(select " & _
                     "case when max(numCounter) is null then 1 " & _
                     "else max(numCounter) + 1 " & _
                     "end numCounter " & _
                     "from " & DBNameSpace & ".AFSDeletions), " & _
                     "'0413" & txtAIRSNumber.Text & "', " & _
                     "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                     "'" & OracleDate & "', '', " & _
                     "'') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    ReDim Preserve temp(i)
                    temp(i) = clbNSPS.Items.Item(count)
                    i += 1

                End If
            Next
            For j = 0 To i - 1
                clbNSPS.Items.Remove(temp(j))
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub DeleteNESHAPSubpart()
        Try


            Dim count As Integer = 0
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim temp(i) As String
            Dim Subpart As String

            For count = 0 To (clbNESHAP.Items.Count - 1)
                If clbNESHAP.CheckedIndices.Contains(count) = True Then
                    Subpart = ""
                    Subpart = Mid(clbNESHAP.Items.Item(count), 1, ((clbNESHAP.Items.Item(count).ToString.IndexOf("-")) - 1))
                    SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
                    "active = '0', " & _
                    "UpdateUser = '" & UserGCode & "', " & _
                    "updateDateTime = '" & OracleDate & "' " & _
                    "where strSubPartKey = '0413" & txtAIRSNumber.Text & "8' " & _
                    "and strSubpart = '" & Subpart & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                     "values " & _
                     "(" & _
                     "(select " & _
                     "case when max(numCounter) is null then 1 " & _
                     "else max(numCounter) + 1 " & _
                     "end numCounter " & _
                     "from " & DBNameSpace & ".AFSDeletions), " & _
                     "'0413" & txtAIRSNumber.Text & "', " & _
                     "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                     "'" & OracleDate & "', '', " & _
                     "'') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    ReDim Preserve temp(i)
                    temp(i) = clbNESHAP.Items.Item(count)
                    i += 1

                End If
            Next
            For j = 0 To i - 1
                clbNESHAP.Items.Remove(temp(j))
            Next


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub DeleteMACTSubpart()
        Try

            Dim count As Integer = 0
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim temp(i) As String
            Dim Subpart As String

            For count = 0 To (clbMACT.Items.Count - 1)
                If clbMACT.CheckedIndices.Contains(count) = True Then
                    Subpart = ""
                    Subpart = Mid(clbMACT.Items.Item(count), 1, ((clbMACT.Items.Item(count).ToString.IndexOf("-")) - 1))
                    SQL = "Update " & DBNameSpace & ".APBSubpartData set " & _
                    "active = '0', " & _
                    "UpdateUser = '" & UserGCode & "', " & _
                    "updateDateTime = '" & OracleDate & "' " & _
                    "where strSubPartKey = '0413" & txtAIRSNumber.Text & "M' " & _
                    "and strSubpart = '" & Subpart & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                     "values " & _
                     "(" & _
                     "(select " & _
                     "case when max(numCounter) is null then 1 " & _
                     "else max(numCounter) + 1 " & _
                     "end numCounter " & _
                     "from " & DBNameSpace & ".AFSDeletions), " & _
                     "'0413" & txtAIRSNumber.Text & "', " & _
                     "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                     "'" & OracleDate & "', '', " & _
                     "'') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    ReDim Preserve temp(i)
                    temp(i) = clbMACT.Items.Item(count)
                    i += 1
                End If
            Next
            For j = 0 To i - 1
                clbMACT.Items.Remove(temp(j))
            Next


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "Declarations"
    Private Sub txtAIRSNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAIRSNumber.TextChanged
        Try


            LoadInformation()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnSaveSIPSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSIPSubpart.Click
        Try


            If cboSIPSubpart.SelectedIndex <> 0 Then
                SaveSIPSubpart()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnSaveNSPSSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveNSPSSubpart.Click
        Try


            If cboNSPSSubpart.SelectedIndex <> 0 Then
                SaveNSPSSubpart()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnSaveNESHAPSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveNESHAPSubpart.Click
        Try


            If cboNESHAPSubpart.SelectedIndex <> 0 Then
                SaveNESHAPSubpart()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnAddMACTSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddMACTSubpart.Click
        Try


            If cboMACTSubPart.SelectedIndex <> 0 Then
                SaveMACTSubpart()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnDeleteSIPSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSIPSubpart.Click
        Try


            If clbSIP.CheckedItems.Count > 0 Then
                DeleteSIPSubpart()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnDeleteNSPSSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveNSPSSubpart.Click
        Try


            If clbNSPS.CheckedItems.Count > 0 Then
                DeleteNSPSSubpart()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnDeleteNESHAPSubpart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveNESHAPSubpart.Click
        Try


            If clbNESHAP.CheckedItems.Count > 0 Then
                DeleteNESHAPSubpart()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnDeleteMACTSubPart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremoveMACTSubPart.Click
        Try


            If clbMACT.CheckedItems.Count > 0 Then
                DeleteMACTSubpart()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEditSIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditSIP.Click
        Try
            If txtSIPCode.Text <> "" And txtSIPDescription.Text <> "" Then
                txtSIPCode.BackColor = Color.White
                txtSIPDescription.BackColor = Color.White

                SQL = "Select strSubPart " & _
                "From " & DBNameSpace & ".LookUpSubpartSIP " & _
                "where strSubPart = '" & txtSIPCode.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".LookUpSubpartSIP set " & _
                    "strDescription = '" & Replace(txtSIPDescription.Text, "'", "''") & "' " & _
                    "where strSubpart = '" & txtSIPCode.Text & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".LookUpSubpartSIP " & _
                    "values " & _
                    "('" & Replace(txtSIPCode.Text, "'", "''") & "', " & _
                    "'" & Replace(txtSIPDescription.Text, "'", "''") & "') "
                End If
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadSubPartData()

            Else
                If txtSIPCode.Text = "" Then
                    txtSIPCode.BackColor = Color.Tomato
                Else
                    txtSIPCode.BackColor = Color.White
                End If
                If txtSIPDescription.Text = "" Then
                    txtSIPDescription.BackColor = Color.Tomato
                Else
                    txtSIPDescription.BackColor = Color.White
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnEditNSPS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditNSPS.Click
        Try
            If txtNSPSCode.Text <> "" And txtNSPSDescription.Text <> "" Then
                txtNSPSCode.BackColor = Color.White
                txtNSPSDescription.BackColor = Color.White

                SQL = "Select strSubPart " & _
                "From " & DBNameSpace & ".LookUpSubpart60 " & _
                "where strSubPart = '" & txtNSPSCode.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".LookUpSubpart60 set " & _
                    "strDescription = '" & Replace(txtNSPSDescription.Text, "'", "''") & "' " & _
                    "where strSubpart = '" & txtNSPSCode.Text & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".LookUpSubpart60 " & _
                    "values " & _
                    "('" & Replace(txtNSPSCode.Text, "'", "''") & "', " & _
                    "'" & Replace(txtNSPSDescription.Text, "'", "''") & "') "
                End If
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadSubPartData()

            Else
                If txtNSPSCode.Text = "" Then
                    txtNSPSCode.BackColor = Color.Tomato
                Else
                    txtNSPSCode.BackColor = Color.White
                End If
                If txtNSPSDescription.Text = "" Then
                    txtNSPSDescription.BackColor = Color.Tomato
                Else
                    txtNSPSDescription.BackColor = Color.White
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnEditNESHAP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditNESHAP.Click
        Try
            If txtNESHAPCode.Text <> "" And txtNESHAPDescription.Text <> "" Then
                txtNESHAPCode.BackColor = Color.White
                txtNESHAPDescription.BackColor = Color.White

                SQL = "Select strSubPart " & _
                "From " & DBNameSpace & ".LookUpSubpart61 " & _
                "where strSubPart = '" & txtNESHAPCode.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".LookUpSubpart61 set " & _
                    "strDescription = '" & Replace(txtNESHAPDescription.Text, "'", "''") & "' " & _
                    "where strSubpart = '" & txtNESHAPCode.Text & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".LookUpSubpart61 " & _
                    "values " & _
                    "('" & Replace(txtNESHAPCode.Text, "'", "''") & "', " & _
                    "'" & Replace(txtNESHAPDescription.Text, "'", "''") & "') "
                End If
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadSubPartData()

            Else
                If txtNESHAPCode.Text = "" Then
                    txtNESHAPCode.BackColor = Color.Tomato
                Else
                    txtNESHAPCode.BackColor = Color.White
                End If
                If txtNESHAPDescription.Text = "" Then
                    txtNESHAPDescription.BackColor = Color.Tomato
                Else
                    txtNESHAPDescription.BackColor = Color.White
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnEditMACT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditMACT.Click
        Try
            If txtMACTCode.Text <> "" And txtMACTDescription.Text <> "" Then
                txtMACTCode.BackColor = Color.White
                txtMACTDescription.BackColor = Color.White

                SQL = "Select strSubPart " & _
                "From " & DBNameSpace & ".LookUpSubpart63 " & _
                "where strSubPart = '" & txtMACTCode.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".LookUpSubpart63 set " & _
                    "strDescription = '" & Replace(txtMACTDescription.Text, "'", "''") & "' " & _
                    "where strSubpart = '" & txtMACTCode.Text & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".LookUpSubpart63 " & _
                    "values " & _
                    "('" & Replace(txtMACTCode.Text, "'", "''") & "', " & _
                    "'" & Replace(txtMACTDescription.Text, "'", "''") & "') "
                End If
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadSubPartData()

            Else
                If txtMACTCode.Text = "" Then
                    txtMACTCode.BackColor = Color.Tomato
                Else
                    txtMACTCode.BackColor = Color.White
                End If
                If txtMACTDescription.Text = "" Then
                    txtMACTDescription.BackColor = Color.Tomato
                Else
                    txtMACTDescription.BackColor = Color.White
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteSIPSubpart_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSIPSubpart.Click
        Try
            SQL = "Delete " & DBNameSpace & ".LookUpSubpartSIP " & _
            "where strSubpart = '" & Replace(txtSIPCode.Text, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            LoadSubPartData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteNSPSSubpart_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteNSPSSubpart.Click
        Try
            SQL = "Delete " & DBNameSpace & ".LookUpSubpart60 " & _
            "where strSubpart = '" & Replace(txtSIPCode.Text, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            LoadSubPartData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteNESHAPSubpart_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteNESHAPSubpart.Click
        Try
            SQL = "Delete " & DBNameSpace & ".LookUpSubpart61 " & _
            "where strSubpart = '" & Replace(txtSIPCode.Text, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            LoadSubPartData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteMACTSubpart_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteMACTSubpart.Click
        Try
            SQL = "Delete " & DBNameSpace & ".LookUpSubpart63 " & _
            "where strSubpart = '" & Replace(txtSIPCode.Text, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            LoadSubPartData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnClearSIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSIP.Click
        Try
            txtSIPCode.Clear()
            txtSIPCode.BackColor = Color.White
            txtSIPDescription.Clear()
            txtSIPDescription.BackColor = Color.White
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnClearNSPS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearNSPS.Click
        Try
            txtNSPSCode.Clear()
            txtNSPSCode.BackColor = Color.White
            txtNSPSDescription.Clear()
            txtNSPSDescription.BackColor = Color.White
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnClearNESHAP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearNESHAP.Click
        Try
            txtNESHAPCode.Clear()
            txtNESHAPCode.BackColor = Color.White
            txtNESHAPDescription.Clear()
            txtNESHAPDescription.BackColor = Color.White
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnClearMACT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearMACT.Click
        Try
            txtMACTCode.Clear()
            txtMACTCode.BackColor = Color.White
            txtMACTDescription.Clear()
            txtMACTDescription.BackColor = Color.White
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#End Region
    Private Sub dgvSIP_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvSIP.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvSIP.HitTest(e.X, e.Y)
            If dgvSIP.Columns(0).HeaderText = "Subpart Code" Then
                If dgvSIP.RowCount > 0 And hti.RowIndex <> -1 Then
                    txtSIPCode.BackColor = Color.White
                    txtSIPDescription.BackColor = Color.White

                    txtSIPCode.Text = dgvSIP(0, hti.RowIndex).Value
                    txtSIPDescription.Text = dgvSIP(1, hti.RowIndex).Value
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub dgvNSPS_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvNSPS.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvNSPS.HitTest(e.X, e.Y)
            If dgvNSPS.Columns(0).HeaderText = "Subpart Code" Then
                If dgvNSPS.RowCount > 0 And hti.RowIndex <> -1 Then
                    txtNSPSCode.BackColor = Color.White
                    txtNSPSDescription.BackColor = Color.White

                    txtNSPSCode.Text = dgvNSPS(0, hti.RowIndex).Value
                    txtNSPSDescription.Text = dgvNSPS(1, hti.RowIndex).Value
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub dgvNESHAP_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvNESHAP.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvNESHAP.HitTest(e.X, e.Y)
            If dgvNESHAP.Columns(0).HeaderText = "Subpart Code" Then
                If dgvNESHAP.RowCount > 0 And hti.RowIndex <> -1 Then
                    txtNESHAPCode.BackColor = Color.White
                    txtNESHAPDescription.BackColor = Color.White

                    txtNESHAPCode.Text = dgvNESHAP(0, hti.RowIndex).Value
                    txtNESHAPDescription.Text = dgvNESHAP(1, hti.RowIndex).Value
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub dgvMACT_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvMACT.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvMACT.HitTest(e.X, e.Y)
            If dgvMACT.Columns(0).HeaderText = "Subpart Code" Then
                If dgvMACT.RowCount > 0 And hti.RowIndex <> -1 Then
                    txtMACTCode.BackColor = Color.White
                    txtMACTDescription.BackColor = Color.White

                    txtMACTCode.Text = dgvMACT(0, hti.RowIndex).Value
                    txtMACTDescription.Text = dgvMACT(1, hti.RowIndex).Value
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OpenDocumentationUrl(Me)
    End Sub
End Class