Imports Oracle.DataAccess.Client


Public Class SSPPAttainmentStatus
    Dim SQL, SQL2 As String
    Dim cmd, cmd2 As OracleCommand
    Dim dr, dr2 As OracleDataReader
    Dim recExist As Boolean
    Dim dsAttainment As DataSet
    Dim daAttainment As OracleDataAdapter
    Dim dsCounty As DataSet
    Dim daCounty As OracleDataAdapter


    Private Sub SSPPAttainmentStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try


            LoadAttainmentStatusComboBoxes()
            LoadCountyComboBox()

            Panel1.Text = "Select a county...."
            Panel2.Text = UserName
            Panel3.Text = OracleDate

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#Region "Page Load"
    Sub LoadAttainmentStatusComboBoxes()
        Try


            cboNonAttainmentStatus.Items.Clear()
            cboNonAttainmentStatus.Items.Add("-Select an Attainment Status-")
            cboNonAttainmentStatus.Items.Add("-View All-")
            cboNonAttainmentStatus.Items.Add("1 hr Maintenance (Yes)")
            cboNonAttainmentStatus.Items.Add("1 hr Maintenance (No)")
            cboNonAttainmentStatus.Items.Add("1 hr Maintenance (Contribute)")
            cboNonAttainmentStatus.Items.Add("8 hr Atlanta")
            cboNonAttainmentStatus.Items.Add("8 hr Macon")
            cboNonAttainmentStatus.Items.Add("8 hr (No)")
            cboNonAttainmentStatus.Items.Add("PM 2.5 Atlanta")
            cboNonAttainmentStatus.Items.Add("PM 2.5 Chattanooga")
            cboNonAttainmentStatus.Items.Add("PM 2.5 Floyd")
            cboNonAttainmentStatus.Items.Add("PM 2.5 Macon")
            cboNonAttainmentStatus.Items.Add("PM 2.5 (No)")

            cboNonAttainmentStatus.Text = cboNonAttainmentStatus.Items.Item(0)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub LoadCountyComboBox()
        Try


            Dim dtCounty As New DataTable

            Dim drNewRow As DataRow
            Dim drDSRow As DataRow

            SQL = "select strCountyCode, strCountyname " & _
            "from " & DBNameSpace & ".LookUpCountyInformation " & _
            "order by strcountyName"
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dsCounty = New DataSet
            daCounty = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daCounty.Fill(dsCounty, "County")

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If

            dtCounty.Columns.Add("strCountyCode", GetType(System.String))
            dtCounty.Columns.Add("strCountyName", GetType(System.String))

            drNewRow = dtCounty.NewRow()
            drNewRow("strCountyCode") = "000"
            drNewRow("strCountyName") = "-County-"
            dtCounty.Rows.Add(drNewRow)

            For Each drDSRow In dsCounty.Tables("County").Rows()
                drNewRow = dtCounty.NewRow
                drNewRow("strCountyCode") = drDSRow("strCountyCode")
                drNewRow("strCountyName") = drDSRow("strCountyName")
                dtCounty.Rows.Add(drNewRow)
            Next

            With cboCounty
                .DataSource = dtCounty
                .DisplayMember = "strCountyName"
                .ValueMember = "strCountyCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region
#Region "Subs and Functions"
    Sub ViewSelectedData()
        Dim SQLClause As String = ""

        Select Case cboNonAttainmentStatus.Text
            Case ("-Select an Attainment Status-")
                SQLClause = "Where strNonAttainment like '____1'"
            Case ("-View All-")
                SQLClause = "Where strNonAttainment like '_____'"
            Case ("1 hr Maintenance (Yes)")
                SQLClause = "Where strNonAttainment like '_1___'"
            Case ("1 hr Maintenance (No)")
                SQLClause = "Where strNonAttainment like '_0___'"
            Case ("1 hr Maintenance (Contribute)")
                SQLClause = "Where strNonAttainment like '_2___'"
            Case ("8 hr Atlanta")
                SQLClause = "Where strNonAttainment like '__1__'"
            Case ("8 hr Macon")
                SQLClause = "Where strNonAttainment like '__2__'"
            Case ("8 hr (No)")
                SQLClause = "Where strNonAttainment like '__0__'"
            Case ("PM 2.5 Atlanta")
                SQLClause = "Where strNonAttainment like '___1_'"
            Case ("PM 2.5 Chattanooga")
                SQLClause = "Where strNonAttainment like '___2_'"
            Case ("PM 2.5 Floyd")
                SQLClause = "Where strNonAttainment like '___3_'"
            Case ("PM 2.5 Macon")
                SQLClause = "Where strNonAttainment like '___4_'"
            Case ("PM 2.5 (No)")
                SQLClause = "Where strNonAttainment like '___0_'"
            Case Else
                SQLClause = "Where strNonAttainment like '____1'"
        End Select

        SQL = "Select " & _
        "strCountyName, strCountyCode, strNonAttainment  " & _
        "from " & DBNameSpace & ".LookUpCountyInformation  " & _
        SQLClause

        dsAttainment = New DataSet
        daAttainment = New OracleDataAdapter(SQL, Conn)

        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If

        daAttainment.Fill(dsAttainment, "Attainment")

        If Conn.State = ConnectionState.Open Then
            'conn.close()
        End If

        dgvAttainmentStatus.DataSource = dsAttainment
        dgvAttainmentStatus.DataMember = "Attainment"

        dgvAttainmentStatus.RowHeadersVisible = False
        dgvAttainmentStatus.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvAttainmentStatus.AllowUserToResizeColumns = True
        dgvAttainmentStatus.AllowUserToAddRows = False
        dgvAttainmentStatus.AllowUserToDeleteRows = False
        dgvAttainmentStatus.AllowUserToOrderColumns = True
        dgvAttainmentStatus.AllowUserToResizeRows = True

        dgvAttainmentStatus.Columns("strCountyCode").HeaderText = "County Code"
        dgvAttainmentStatus.Columns("strCountyCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        dgvAttainmentStatus.Columns("strCountyCode").DisplayIndex = 0
        dgvAttainmentStatus.Columns("strCountyName").HeaderText = "County Name"
        dgvAttainmentStatus.Columns("strCountyName").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        dgvAttainmentStatus.Columns("strCountyName").DisplayIndex = 1
        dgvAttainmentStatus.Columns("strNonAttainment").HeaderText = "Attainment status"
        dgvAttainmentStatus.Columns("strNonAttainment").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgvAttainmentStatus.Columns("strNonAttainment").DisplayIndex = 2
        dgvAttainmentStatus.Columns("strNonAttainment").Visible = False

    End Sub
    Sub ViewCountyInformation()
        Try
            Dim Attainment As String = ""



            SQL = "select " & _
            "strNonAttainment " & _
            "from " & DBNameSpace & ".LookUpCountyInformation " & _
            "where strCountyCode = '" & cboCounty.SelectedValue & "'"

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                Attainment = dr.Item("strNonAttainment")
            End If

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If

            If Attainment <> "" Then
                Select Case Mid(Attainment, 2, 1)
                    Case 0
                        rdbOneHourNo.Checked = True
                    Case 1
                        rdbOneHourYes.Checked = True
                    Case 2
                        rdbOneHourCont.Checked = True
                    Case Else
                        rdbOneHourNo.Checked = True
                End Select
                Select Case Mid(Attainment, 3, 1)
                    Case 0
                        rdbEightHourNo.Checked = True
                    Case 1
                        rdbEightHourAtlanta.Checked = True
                    Case 2
                        rdbEightHourMacon.Checked = True
                    Case Else
                        rdbEightHourNo.Checked = True
                End Select
                Select Case Mid(Attainment, 4, 1)
                    Case 0
                        rdbPMFineNo.Checked = True
                    Case 1
                        rdbPMFineAtlanta.Checked = True
                    Case 2
                        rdbPMFineChattanooga.Checked = True
                    Case 3
                        rdbPMFineFloyd.Checked = True
                    Case 4
                        rdbPMFineMacon.Checked = True
                    Case Else
                        rdbPMFineNo.Checked = True
                End Select

            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try



    End Sub
    Sub ClearForm()
        Try


            cboNonAttainmentStatus.Text = cboNonAttainmentStatus.Items.Item(0)
            cboCounty.SelectedIndex = 0
            rdbOneHourNo.Checked = True
            rdbEightHourNo.Checked = True
            rdbPMFineNo.Checked = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub SaveOneHourChanges()
        Try

            Dim AttainmentStatus As String = "00000"

            SQL = "select strNonAttainment " & _
            "from " & DBNameSpace & ".LookUpCountyInformation " & _
            "where strCountyCode = '" & cboCounty.SelectedValue & "' "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strNonAttainment")) Then
                    AttainmentStatus = "00000"
                Else
                    AttainmentStatus = dr.Item("strNonAttainment")
                End If
            End While
            dr.Close()

            If rdbOneHourNo.Checked = True Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 1) & "0" & Mid(AttainmentStatus, 3)
            End If
            If rdbOneHourYes.Checked = True Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 1) & "1" & Mid(AttainmentStatus, 3)
            End If
            If rdbOneHourCont.Checked = True Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 1) & "2" & Mid(AttainmentStatus, 3)
            End If

            SQL = "Update " & DBNameSpace & ".LookUpCountyInformation set " & _
            "strNonAttainment = '" & AttainmentStatus & "' " & _
            "where strCountyCode = '" & cboCounty.SelectedValue & "' "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Select strAttainmentStatus, strAIRSNumber " & _
            "from " & DBNameSpace & ".APBHeaderData " & _
            "where substr(strAIRSNumber, 5, 3) = '" & cboCounty.SelectedValue & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If rdbOneHourNo.Checked = True Then
                    If IsDBNull(dr.Item("strAttainmentStatus")) Then
                        AttainmentStatus = "00000"
                    Else
                        AttainmentStatus = dr.Item("strAttainmentStatus")
                        AttainmentStatus = Mid(AttainmentStatus, 1, 1) & "0" & Mid(AttainmentStatus, 3)
                    End If
                End If
                If rdbOneHourYes.Checked = True Then
                    If IsDBNull(dr.Item("strAttainmentStatus")) Then
                        AttainmentStatus = "01000"
                    Else
                        AttainmentStatus = dr.Item("strAttainmentStatus")
                        AttainmentStatus = Mid(AttainmentStatus, 1, 1) & "1" & Mid(AttainmentStatus, 3)
                    End If
                End If
                If rdbOneHourCont.Checked = True Then
                    If IsDBNull(dr.Item("strAttainmentStatus")) Then
                        AttainmentStatus = "02000"
                    Else
                        AttainmentStatus = dr.Item("strAttainmentStatus")
                        AttainmentStatus = Mid(AttainmentStatus, 1, 1) & "2" & Mid(AttainmentStatus, 3)
                    End If
                End If

                SQL2 = "Update " & DBNameSpace & ".APBHeaderData set " & _
                "strAttainmentStatus = '" & AttainmentStatus & "' " & _
                "where strAIRSNumber = '" & dr.Item("strAirsNumber") & "' "

                cmd2 = New OracleCommand(SQL2, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd2.ExecuteReader
                dr2.Close()
            End While

            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub SaveEightHourChanges()
        Try

            Dim AttainmentStatus As String = "00000"

            SQL = "select strNonAttainment " & _
            "from " & DBNameSpace & ".LookUpCountyInformation " & _
            "where strCountyCode = '" & cboCounty.SelectedValue & "' "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strNonAttainment")) Then
                    AttainmentStatus = "00000"
                Else
                    AttainmentStatus = dr.Item("strNonAttainment")
                End If
            End While
            dr.Close()

            If rdbEightHourNo.Checked = True Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "0" & Mid(AttainmentStatus, 4)
            End If
            If rdbEightHourAtlanta.Checked = True Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "1" & Mid(AttainmentStatus, 4)
            End If
            If rdbEightHourMacon.Checked = True Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "2" & Mid(AttainmentStatus, 4)
            End If

            SQL = "Update " & DBNameSpace & ".LookUpCountyInformation set " & _
            "strNonAttainment = '" & AttainmentStatus & "' " & _
            "where strCountyCode = '" & cboCounty.SelectedValue & "' "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Select strAttainmentStatus, strAIRSNumber " & _
            "from " & DBNameSpace & ".APBHeaderData " & _
            "where substr(strAIRSNumber, 5, 3) = '" & cboCounty.SelectedValue & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If rdbEightHourNo.Checked = True Then
                    If IsDBNull(dr.Item("strAttainmentStatus")) Then
                        AttainmentStatus = "00000"
                    Else
                        AttainmentStatus = dr.Item("strAttainmentStatus")
                        AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "0" & Mid(AttainmentStatus, 4)
                    End If
                End If
                If rdbEightHourAtlanta.Checked = True Then
                    If IsDBNull(dr.Item("strAttainmentStatus")) Then
                        AttainmentStatus = "00100"
                    Else
                        AttainmentStatus = dr.Item("strAttainmentStatus")
                        AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "1" & Mid(AttainmentStatus, 4)
                    End If
                End If
                If rdbEightHourMacon.Checked = True Then
                    If IsDBNull(dr.Item("strAttainmentStatus")) Then
                        AttainmentStatus = "00200"
                    Else
                        AttainmentStatus = dr.Item("strAttainmentStatus")
                        AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "2" & Mid(AttainmentStatus, 4)
                    End If
                End If

                SQL2 = "Update " & DBNameSpace & ".APBHeaderData set " & _
                "strAttainmentStatus = '" & AttainmentStatus & "' " & _
                "where strAIRSNumber = '" & dr.Item("strAirsNumber") & "' "

                cmd2 = New OracleCommand(SQL2, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd2.ExecuteReader
                dr2.Close()
            End While

            dr.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub SavePMFineChanges()
        Try

            Dim AttainmentStatus As String = "00000"

            SQL = "select strNonAttainment " & _
            "from " & DBNameSpace & ".LookUpCountyInformation " & _
            "where strCountyCode = '" & cboCounty.SelectedValue & "' "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strNonAttainment")) Then
                    AttainmentStatus = "00000"
                Else
                    AttainmentStatus = dr.Item("strNonAttainment")
                End If
            End While
            dr.Close()

            If rdbPMFineNo.Checked = True Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "0" & Mid(AttainmentStatus, 5)
            End If
            If rdbPMFineAtlanta.Checked = True Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "1" & Mid(AttainmentStatus, 5)
            End If
            If rdbPMFineChattanooga.Checked = True Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "2" & Mid(AttainmentStatus, 5)
            End If
            If rdbPMFineFloyd.Checked = True Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "3" & Mid(AttainmentStatus, 5)
            End If
            If rdbPMFineMacon.Checked = True Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "4" & Mid(AttainmentStatus, 5)
            End If

            SQL = "Update " & DBNameSpace & ".LookUpCountyInformation set " & _
            "strNonAttainment = '" & AttainmentStatus & "' " & _
            "where strCountyCode = '" & cboCounty.SelectedValue & "' "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Select strAttainmentStatus, strAIRSNumber " & _
            "from " & DBNameSpace & ".APBHeaderData " & _
            "where substr(strAIRSNumber, 5, 3) = '" & cboCounty.SelectedValue & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If rdbPMFineNo.Checked = True Then
                    If IsDBNull(dr.Item("strAttainmentStatus")) Then
                        AttainmentStatus = "00000"
                    Else
                        AttainmentStatus = dr.Item("strAttainmentStatus")
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "0" & Mid(AttainmentStatus, 5)
                    End If
                End If
                If rdbPMFineAtlanta.Checked = True Then
                    If IsDBNull(dr.Item("strAttainmentStatus")) Then
                        AttainmentStatus = "00010"
                    Else
                        AttainmentStatus = dr.Item("strAttainmentStatus")
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "1" & Mid(AttainmentStatus, 5)
                    End If
                End If
                If rdbPMFineChattanooga.Checked = True Then
                    If IsDBNull(dr.Item("strAttainmentStatus")) Then
                        AttainmentStatus = "00020"
                    Else
                        AttainmentStatus = dr.Item("strAttainmentStatus")
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "2" & Mid(AttainmentStatus, 5)
                    End If
                End If
                If rdbPMFineFloyd.Checked = True Then
                    If IsDBNull(dr.Item("strAttainmentStatus")) Then
                        AttainmentStatus = "00030"
                    Else
                        AttainmentStatus = dr.Item("strAttainmentStatus")
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "3" & Mid(AttainmentStatus, 5)
                    End If
                End If
                If rdbPMFineMacon.Checked = True Then
                    If IsDBNull(dr.Item("strAttainmentStatus")) Then
                        AttainmentStatus = "00040"
                    Else
                        AttainmentStatus = dr.Item("strAttainmentStatus")
                        AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "4" & Mid(AttainmentStatus, 5)
                    End If
                End If

                SQL2 = "Update " & DBNameSpace & ".APBHeaderData set " & _
                "strAttainmentStatus = '" & AttainmentStatus & "' " & _
                "where strAIRSNumber = '" & dr.Item("strAirsNumber") & "' "

                cmd2 = New OracleCommand(SQL2, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd2.ExecuteReader
                dr2.Close()
            End While

            dr.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region
#Region "Declarations"
    Private Sub btnViewSelectedData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewSelectedData.Click
        Try


            ViewSelectedData()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnViewCountyInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewCountyInfo.Click
        Try


            ViewCountyInformation()


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub dgvAttainmentStatus_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvAttainmentStatus.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvAttainmentStatus.HitTest(e.X, e.Y)

        Try


            If dgvAttainmentStatus.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvAttainmentStatus.Columns(0).HeaderText = "County Name" Then
                    cboCounty.Text = dgvAttainmentStatus(0, hti.RowIndex).Value
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try



    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try

            ClearForm()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Private Sub btnSaveChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveChanges.Click
        Try


            SaveOneHourChanges()
            SaveEightHourChanges()
            SavePMFineChanges()

            MsgBox("Done", MsgBoxStyle.Information, "Attainment Status Update")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnOneHourChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOneHourChanges.Click
        Try

            SaveOneHourChanges()
            MsgBox("Done", MsgBoxStyle.Information, "Attainment Status Update")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnEightHourChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEightHourChanges.Click
        Try

            SaveEightHourChanges()
            MsgBox("Done", MsgBoxStyle.Information, "Attainment Status Update")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnPMFineChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPMFineChanges.Click
        Try

            SavePMFineChanges()
            MsgBox("Done", MsgBoxStyle.Information, "Attainment Status Update")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub TBAttainmentStatus_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBAttainmentStatus.ButtonClick
        Try

            Select Case TBAttainmentStatus.Buttons.IndexOf(e.Button)
                Case 0
                    SaveOneHourChanges()
                    SaveEightHourChanges()
                    SavePMFineChanges()
                    MsgBox("Done", MsgBoxStyle.Information, "Attainment Status Update")
                Case 1
                    Me.Close()
                Case Else

            End Select

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region


    Private Sub MmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiHelp.Click
        Try
            Help.ShowHelp(Label1, HELP_URL)
        Catch ex As Exception
        End Try
    End Sub
End Class