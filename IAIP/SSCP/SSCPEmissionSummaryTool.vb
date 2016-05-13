Imports Oracle.ManagedDataAccess.Client


Public Class SSCPEmissionSummaryTool
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim daViewCount As OracleDataAdapter
    Dim dsViewCount As DataSet
    Dim ds As DataSet
    Dim da As OracleDataAdapter

    Private Sub SSCPEmissionSummaryTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            pnl1.Text = ""
            pnl2.Text = CurrentUser.AlphaName
            pnl3.Text = OracleDate

            loadYear()
            loadEIPollutant()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub loadYear()
        Dim year As String

        Try
            SQL = "Select " &
            "distinct intESYear " &
            "from AIRBRANCH.esschema " &
            "order by intESYear desc "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            ' cboYear.Items.Add("- Select a Year -")
            dr.Read()
            Do
                year = dr("intESYear")
                cboYear.Items.Add(year)

            Loop While dr.Read
            cboYear.SelectedIndex = 0

            cboEIYear.Items.Add("-Select a Year-")

            SQL = "Select " &
            "distinct(strInventoryYear) as EIYear " &
            "from AIRBRANCH.EISI " &
            "where strInventoryYear < 2010 " &
            "order by strInventoryYear desc "

            SQL = "select * from " &
            "(select " &
            "distinct(inventoryyear) as EIYear " &
            "from airbranch.eis_admin " &
            "union " &
            "Select  " &
            "distinct(to_number(strInventoryYear)) as EIYear " &
            "from AIRBranch.EISI " &
            "where strInventoryYear < 2010 ) " &
            "order by EIYear desc  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                cboEIYear.Items.Add(dr.Item("EIYear"))
            End While
            dr.Close()
            cboEIYear.SelectedIndex = 0


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub loadEIPollutant()
        Try
            Dim dtEIPollutant As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "select " &
            "distinct(AIRBranch.EIEM.strPollutantCode) as Pollutants,   " &
            "strPollutantDesc  " &
            "from AIRBranch.EILookUpPollutantCodes, AIRBranch.EIEM " &
            "where AIRBranch.EIEM.strPollutantCode = AIRBranch.EILookUpPollutantCodes.strPollutantCode " &
            "union " &
            "select distinct (AIRBranch.VW_EIS_RPEMISSIONS.PollutantCode), " &
            "AIRBranch.EISLK_PollutantCode .strDesc " &
            "from AIRBranch.VW_EIS_RPEMISSIONS, AIRBranch.EISLK_PollutantCode " &
            "where AIRBranch.VW_EIS_RPEMISSIONS.PollutantCode = AIRBranch.EISLK_PollutantCode.PollutantCode "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "EIPollutant")

            dtEIPollutant.Columns.Add("Pollutants", GetType(System.String))
            dtEIPollutant.Columns.Add("strPollutantDesc", GetType(System.String))

            drNewRow = dtEIPollutant.NewRow()
            drNewRow("Pollutants") = "%"
            drNewRow("strPollutantDesc") = "-Select a Pollutant-"
            dtEIPollutant.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("EIPollutant").Rows()
                drNewRow = dtEIPollutant.NewRow()
                drNewRow("Pollutants") = drDSRow("Pollutants")
                drNewRow("strPollutantDesc") = drDSRow("strPollutantDesc")
                dtEIPollutant.Rows.Add(drNewRow)
            Next

            With cboEIPollutants
                .DataSource = dtEIPollutant
                .DisplayMember = "strPollutantDesc"
                .ValueMember = "Pollutants"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "ES Tool"

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        Try
            runcount()
            lblYear.Text = cboYear.SelectedItem
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub runcount()
        Dim Nonresponsecount As Integer
        Dim MailoutCount As Integer
        Dim MailOutOptInCount As Integer
        Dim mailoutOptOutCount As Integer
        Dim ResponseCount As Integer
        Dim TotaloptinCount As Integer
        Dim TotaloptoutCount As Integer
        Dim TotalinincomplianceCount As Integer
        Dim TotaloutofcomplianceCount As Integer
        Dim extracount As Integer
        Dim extraOptincount As Integer
        Dim extraOptOutCount As Integer
        Dim TotalResponsecount As Integer
        txtESYear.Text = cboYear.SelectedItem
        Dim ESYear As String = txtESYear.Text
        Dim intESyear As Integer = CInt(ESYear)
        Dim DeadlineYear As String = txtESYear.Text

        Dim deadline As String = ""
        If DeadlineYear <> "" Then
            DeadlineYear = CInt(DeadlineYear) + 1
            deadline = "15-Jun-" & DeadlineYear
        Else
            deadline = "15-Jun-2007"
        End If


        Try
            Try
                SQL = "select count(*) as MailoutCount " &
                "from AIRBRANCH.esmailout, AIRBRANCH.ESSCHEMA " &
                "where AIRBRANCH.ESMAILOUT.STRAIRSYEAR = AIRBRANCH.ESSCHEMA.STRAIRSYEAR(+) " &
                "and AIRBRANCH.esmailout.STRESYEAR = '" & ESYear & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtMailOutCount.Text = dr.Item(MailoutCount)
                End While
                dr.Close()

                SQL = "select count(*) as ResponseCount " &
                "from AIRBRANCH.esmailout, AIRBRANCH.ESSCHEMA " &
                "where AIRBRANCH.ESMAILOUT.STRAIRSYEAR = AIRBRANCH.ESSCHEMA.STRAIRSYEAR " &
                "and AIRBRANCH.ESSCHEMA.STROPTOUT is not NULL " &
                "and AIRBRANCH.esmailout.STRESYEAR = '" & ESYear & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtResponseCount.Text = dr.Item(ResponseCount)
                End While
                dr.Close()

                SQL = "select count(*) as TotaloptinCount " &
                "from AIRBRANCH.ESSchema " &
                "where AIRBRANCH.ESSchema.intESYEAR = '" & intESyear & "'" &
                " and AIRBRANCH.ESSchema.strOptOut = 'NO'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                While dr.Read()
                    txtTotalOptInCount.Text = dr.Item(TotaloptinCount)
                End While
                dr.Close()

                SQL = "select count(*) as TotaloptOutCount " &
                "from AIRBRANCH.ESSchema " &
                "where AIRBRANCH.ESSchema.intESYEAR = '" & intESyear & "' " &
                "and AIRBRANCH.ESSchema.strOptOut = 'YES'"

                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                While dr.Read()
                    txtTotalOptOutCount.Text = dr.Item(TotaloptoutCount)
                End While
                dr.Close()

                SQL = "select count(*) as TotalinincomplianceCount " &
                "from AIRBRANCH.ESSchema " &
                "where AIRBRANCH.ESSchema.intESYEAR = '" & intESyear & "'" &
                " and to_date(AIRBRANCH.ESSchema.STRDATEFIRSTCONFIRM) < = '" & deadline & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read()
                    txtTotalincompliance.Text = dr.Item(TotalinincomplianceCount)
                End While
                dr.Close()

                SQL = "select count(*) as TotaloutofcomplianceCount " &
                "from AIRBRANCH.ESSchema " &
                "where AIRBRANCH.ESSchema.intESYEAR = '" & intESyear & "'" &
                " and to_date(AIRBRANCH.ESSchema.STRDATEFIRSTCONFIRM) > '" & deadline & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read()
                    txtTotaloutofcompliance.Text = dr.Item(TotaloutofcomplianceCount)
                End While
                dr.Close()

                SQL = "select count(*) as MailOutOptInCount " &
                "from AIRBRANCH.ESSchema, AIRBRANCH.ESMailout " &
                "where AIRBRANCH.ESMAILOUT.strESYEAR = '" & ESYear & "' " &
                " and AIRBRANCH.ESMAILOUT.STRAIRSYEAR = AIRBRANCH.ESSCHEMA.STRAIRSYEAR(+) " &
                " and AIRBRANCH.ESSchema.strOptOut = 'NO'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtMailoutOptin.Text = dr.Item(MailOutOptInCount)
                End While
                dr.Close()

                SQL = "select count(*) as MailOutOptOutCount " &
                "from AIRBRANCH.ESSchema, AIRBRANCH.ESMailout " &
                "where AIRBRANCH.ESMAILOUT.strESYEAR = '" & ESYear & "'" &
                " and AIRBRANCH.ESMAILOUT.STRAIRSYEAR = AIRBRANCH.ESSCHEMA.STRAIRSYEAR(+) " &
                " and AIRBRANCH.ESSchema.strOptOut = 'YES'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtMailOutOptOut.Text = dr.Item(mailoutOptOutCount)
                End While
                dr.Close()

            Catch ex As Exception
                MsgBox("That Prefix is not in the db" + vbCrLf + ex.ToString())
            End Try


            SQL = "select count(*) as Nonresponsecount " &
             "from AIRBRANCH.ESSCHEMA " &
             "where AIRBRANCH.ESSCHEMA.intESYEAR = '" & ESYear & "'" &
             " and AIRBRANCH.ESSchema.strOptOut is NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read()
                txtNonResponseCount.Text = dr.Item(Nonresponsecount)
            End While
            dr.Close()

            SQL = "select count(*) as ExtraCount " &
            "from (Select AIRBRANCH.ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, " &
            "AIRBRANCH.ESMAILOUT.STRAIRSYear AS MailoutAIRS " &
            "From AIRBRANCH.ESMailout, AIRBRANCH.ESSCHEMA" &
            " Where AIRBRANCH.ESMAILOUT.STRAIRSYEAR (+)= AIRBRANCH.ESSCHEMA.STRAIRSYEAR " &
            "AND AIRBRANCH.esschema.INTESYEAR= '" & intESyear & "' " &
            "AND AIRBRANCH.ESSCHEMA.STROPTOUT IS NOT NULL) " &
            "dt_NotInMailout, " &
            "AIRBRANCH.ESSCHEMA " &
            "Where AIRBRANCH.ESSCHEMA.STRAIRSYEAR = SchemaAIRS " &
            "AND MailoutAIRS is NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtextraResponse.Text = dr.Item(extracount)
            End While
            dr.Close()

            SQL = "select count(*) as ExtraOptinCount " &
            "from (Select AIRBRANCH.ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, " &
            "AIRBRANCH.ESMAILOUT.STRAIRSYear AS MailoutAIRS " &
            "From AIRBRANCH.ESMailout, AIRBRANCH.ESSCHEMA " &
            "Where AIRBRANCH.ESMAILOUT.STRAIRSYEAR (+)= AIRBRANCH.ESSCHEMA.STRAIRSYEAR " &
            "AND AIRBRANCH.esschema.INTESYEAR= '" & intESyear & "' " &
            "AND AIRBRANCH.ESSCHEMA.STROPTOUT IS NOT NULL) " &
            "dt_NotInMailout, " &
            "AIRBRANCH.ESSCHEMA " &
            "Where AIRBRANCH.ESSCHEMA.STRAIRSYEAR = SchemaAIRS " &
            "AND MailoutAIRS is NULL " &
            "and AIRBRANCH.ESSCHEMA.STROPTOUT='NO'"

            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtExtraOptin.Text = dr.Item(extraOptincount)
            End While
            dr.Close()

            SQL = "select count(*) as ExtraOptOUTCount " &
            "from (Select AIRBRANCH.ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, " &
            "AIRBRANCH.ESMAILOUT.STRAIRSYear AS MailoutAIRS " &
            "From AIRBRANCH.ESMailout, AIRBRANCH.ESSCHEMA " &
            "Where AIRBRANCH.ESMAILOUT.STRAIRSYEAR (+)= AIRBRANCH.ESSCHEMA.STRAIRSYEAR " &
            "AND AIRBRANCH.esschema.INTESYEAR= '" & intESyear & "' " &
            "AND AIRBRANCH.ESSCHEMA.STROPTOUT IS NOT NULL) " &
            "dt_NotInMailout, " &
            "AIRBRANCH.ESSCHEMA " &
            "Where AIRBRANCH.ESSCHEMA.STRAIRSYEAR = SchemaAIRS " &
            "AND MailoutAIRS is NULL " &
            "and AIRBRANCH.ESSCHEMA.STROPTOUT='YES'"

            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read()
                txtExtraOptout.Text = dr.Item(extraOptOutCount)
            End While
            dr.Close()

            SQL = "select count(*) as TotalResponsecount " &
            "from AIRBRANCH.ESSchema " &
            "where AIRBRANCH.ESSchema.intESYEAR = '" & intESyear & "'" &
            " and AIRBRANCH.ESSchema.strOptOut is not NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtTotalResponse.Text = dr.Item(TotalResponsecount)
            End While
            dr.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub findESData()
        Dim AirsNo As String = txtESAirsNo.Text
        Dim ESyear As String = cboYear.SelectedItem
        Dim intESyear As Integer = CInt(ESyear)
        Try

            SQL = "SELECT * " &
            "from AIRBRANCH.esschema " &
            "where STRAIRSNUMBER = '" & AirsNo & "' " &
            "and INTESYEAR = '" & intESyear & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr("STRAIRSNUMBER")) Then
                    txtESAirsNo.Text = ""
                Else
                    txtESAirsNo.Text = dr("STRAIRSNUMBER")
                End If
                If IsDBNull(dr("STRFACILITYNAME")) Then
                    txtFACILITYNAME.Text = ""
                Else
                    txtFACILITYNAME.Text = dr("STRFACILITYNAME")
                End If
                If IsDBNull(dr("STRFACILITYADDRESS")) Then
                    txtFACILITYADDRESS.Text = ""
                Else
                    txtFACILITYADDRESS.Text = dr("STRFACILITYADDRESS")
                End If
                If IsDBNull(dr("STRFACILITYCITY")) Then
                    txtFACILITYCITY.Text = ""
                Else
                    txtFACILITYCITY.Text = dr("STRFACILITYCITY")
                End If
                If IsDBNull(dr("STRFACILITYSTATE")) Then
                    txtFACILITYSTATE.Text = ""
                Else
                    txtFACILITYSTATE.Text = dr("STRFACILITYSTATE")
                End If
                If IsDBNull(dr("STRFACILITYZIP")) Then
                    txtFACILITYZIP.Text = ""
                Else
                    txtFACILITYZIP.Text = dr("STRFACILITYZIP")
                End If
                If IsDBNull(dr("STRCOUNTY")) Then
                    txtCOUNTY.Text = ""
                Else
                    txtCOUNTY.Text = dr("STRCOUNTY")
                End If
                If IsDBNull(dr("DBLXCOORDINATE")) Then
                    txtXCOORDINATE.Text = ""
                Else
                    txtXCOORDINATE.Text = dr("DBLXCOORDINATE")
                End If
                If IsDBNull(dr("DBLYCOORDINATE")) Then
                    txtYCOORDINATE.Text = ""
                Else
                    txtYCOORDINATE.Text = dr("DBLYCOORDINATE")
                End If
                If IsDBNull(dr("STRHORIZONTALCOLLECTIONCODE")) Then
                    txtHORIZONTALCOLLECTIONCODE.Text = ""
                Else
                    txtHORIZONTALCOLLECTIONCODE.Text = dr("STRHORIZONTALCOLLECTIONCODE")
                End If
                If IsDBNull(dr("STRHORIZONTALACCURACYMEASURE")) Then
                    txtHORIZONTALACCURACYMEASURE.Text = ""
                Else
                    txtHORIZONTALACCURACYMEASURE.Text = dr("STRHORIZONTALACCURACYMEASURE")
                End If
                If IsDBNull(dr("STRHORIZONTALREFERENCECODE")) Then
                    txtHORIZONTALREFERENCECODE.Text = ""
                Else
                    txtHORIZONTALREFERENCECODE.Text = dr("STRHORIZONTALREFERENCECODE")
                End If
                If IsDBNull(dr("STRCONTACTCOMPANY")) Then
                    txtCompany.Text = ""
                Else
                    txtCompany.Text = dr("STRCONTACTCOMPANY")
                End If
                If IsDBNull(dr("STRCONTACTTITLE")) Then
                    txtTitle.Text = ""
                Else
                    txtTitle.Text = dr("STRCONTACTTITLE")
                End If
                If IsDBNull(dr("STRCONTACTPHONENUMBER")) Then
                    txtPhone.Text = ""
                Else
                    txtPhone.Text = dr("STRCONTACTPHONENUMBER")
                End If
                If IsDBNull(dr("STRCONTACTFAXNUMBER")) Then
                    txtFax.Text = ""
                Else
                    txtFax.Text = dr("STRCONTACTFAXNUMBER")
                End If
                If IsDBNull(dr("STRCONTACTFIRSTNAME")) Then
                    txtContactFirstName.Text = ""
                Else
                    txtContactFirstName.Text = dr("STRCONTACTFIRSTNAME")
                End If
                If IsDBNull(dr("STRCONTACTLASTNAME")) Then
                    txtContactLastName.Text = ""
                Else
                    txtContactLastName.Text = dr("STRCONTACTLASTNAME")
                End If
                If IsDBNull(dr("STRCONTACTADDRESS1")) Then
                    txtAddress1.Text = ""
                Else
                    txtAddress1.Text = dr("STRCONTACTADDRESS1")
                End If
                If IsDBNull(dr("STRCONTACTADDRESS2")) Then
                    txtAddress2.Text = ""
                Else
                    txtAddress2.Text = dr("STRCONTACTADDRESS2")
                End If
                If IsDBNull(dr("STRCONTACTCITY")) Then
                    txtCity.Text = ""
                Else
                    txtCity.Text = dr("STRCONTACTCITY")
                End If
                If IsDBNull(dr("STRCONTACTSTATE")) Then
                    txtState.Text = ""
                Else
                    txtState.Text = dr("STRCONTACTSTATE")
                End If
                If IsDBNull(dr("STRCONTACTZIP")) Then
                    txtZip.Text = ""
                Else
                    txtZip.Text = dr("STRCONTACTZIP")
                End If
                If IsDBNull(dr("STRCONTACTEMAIL")) Then
                    txtEmail.Text = ""
                Else
                    txtEmail.Text = dr("STRCONTACTEMAIL")
                End If
                If IsDBNull(dr("DBLVOCEMISSION")) Then
                    txtVOCEmission.Text = ""
                Else
                    txtVOCEmission.Text = dr("DBLVOCEMISSION")
                End If
                If IsDBNull(dr("DBLNOXEMISSION")) Then
                    txtNOXEmission.Text = ""
                Else
                    txtNOXEmission.Text = dr("DBLNOXEMISSION")
                End If
                If IsDBNull(dr("STRCONFIRMATIONNBR")) Then
                    txtConfirmationNbr.Text = ""
                    txtConfirmationNumber.Text = ""
                Else
                    txtConfirmationNbr.Text = dr("STRCONFIRMATIONNBR")
                    txtConfirmationNumber.Text = dr("STRCONFIRMATIONNBR")
                End If
                If IsDBNull(dr("STRDATEFIRSTCONFIRM")) Then
                    txtFirstConfirmedDate.Text = ""
                Else
                    txtFirstConfirmedDate.Text = dr("STRDATEFIRSTCONFIRM")
                End If
            End While

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgvESDataCount_MouseUp1(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvESDataCount.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvESDataCount.HitTest(e.X, e.Y)

        Try


            If dgvESDataCount.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvESDataCount.Columns(0).HeaderText = "Airs No." Then
                    If IsDBNull(dgvESDataCount(0, hti.RowIndex).Value) Then

                    Else
                        txtESAirsNo.Text = dgvESDataCount(0, hti.RowIndex).Value
                    End If
                Else
                    If dgvESDataCount.Columns(0).HeaderText = "Airs No." Then
                        If IsDBNull(dgvESDataCount(0, hti.RowIndex).Value) Then

                        Else
                            txtESAirsNo.Text = dgvESDataCount(0, hti.RowIndex).Value
                        End If
                    End If
                End If
                If dgvESDataCount.Columns(3).HeaderText = "Confirmation Number" Then
                    If IsDBNull(dgvESDataCount(3, hti.RowIndex).Value) Then

                    Else

                        txtConfirmationNumber.Text = dgvESDataCount(3, hti.RowIndex).Value
                        findESData()
                    End If
                Else
                    If dgvESDataCount.Columns(3).HeaderText = "Confirmation Number" Then
                        If IsDBNull(dgvESDataCount(3, hti.RowIndex).Value) Then

                        Else
                            findESData()
                            txtConfirmationNumber.Text = dgvESDataCount(3, hti.RowIndex).Value
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub lblViewMailOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewMailOut.LinkClicked
        txtESYear.Text = cboYear.SelectedItem

        Try

            Dim year As String = txtESYear.Text

            SQL = "SELECT STRAIRSNUMBER, " &
            "STRFACILITYNAME, " &
            "STRCONTACTFIRSTNAME, " &
            "STRCONTACTLASTNAME, " &
            "STRCONTACTCOMPANYname, " &
            "STRCONTACTADDRESS1, " &
            "STRCONTACTCITY, " &
            "STRCONTACTSTATE, " &
            "STRCONTACTZIPCODE, " &
            "STRCONTACTEMAIL " &
            "from AIRBRANCH.esMailOut " &
            "where STRESYEAR = '" & year & "' " &
            "order by STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvESDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTCOMPANYname").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
            dgvESDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 5
            dgvESDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvESDataCount.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvESDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvESDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvESDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvESDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 9

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
            txtMailOutCount.Text = txtRecordNumber.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub lblViewOptin_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewTotalOptin.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT AIRBRANCH.esSchema.STRAIRSNUMBER, " &
            "AIRBRANCH.esSchema.STRFACILITYNAME, " &
            "AIRBRANCH.esSchema.STRDATEFIRSTCONFIRM, " &
            "AIRBRANCH.esSchema.STRCONFIRMATIONNBR " &
            "from AIRBRANCH.esSchema, AIRBRANCH.esmailout " &
            "where AIRBRANCH.esSchema.intESyear = '" & intYear & "' " &
            "and AIRBRANCH.esSchema.STROPTOUT = 'NO'" &
            "and AIRBRANCH.ESMAILOUT.STRAIRSYEAR (+)= AIRBRANCH.ESSCHEMA.STRAIRSYEAR " &
            "order by AIRBRANCH.esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

            txtTotalOptInCount.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtTotalOptInCount.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub lblViewOptOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewTotalOptOut.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT AIRBRANCH.esSchema.STRAIRSNUMBER, " &
            "AIRBRANCH.esSchema.STRFACILITYNAME, " &
            "AIRBRANCH.esSchema.STRDATEFIRSTCONFIRM, " &
            "AIRBRANCH.esSchema.STRCONFIRMATIONNBR " &
            "from AIRBRANCH.esSchema, AIRBRANCH.esmailout  " &
            "where AIRBRANCH.esSchema.intESyear = '" & intYear & "' " &
            "and AIRBRANCH.ESMAILOUT.STRAIRSYEAR (+)= AIRBRANCH.ESSCHEMA.STRAIRSYEAR " &
            "and AIRBRANCH.esSchema.STROPTOUT = 'YES'" &
            "order by AIRBRANCH.esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

            txtTotalOptOutCount.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtTotalOptOutCount.Text

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewOutofcompliance_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewOutofcompliance.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)
            Dim DeadlineYear As String = txtESYear.Text

            Dim deadline As String = ""
            If DeadlineYear <> "" Then
                DeadlineYear = CInt(DeadlineYear) + 1
                deadline = "15-Jun-" & DeadlineYear
            Else
                deadline = "15-Jun-2007"
            End If

            SQL = "SELECT airbranch.esSchema.STRAIRSNUMBER, " &
            "AIRBRANCH.esSchema.STRFACILITYNAME, " &
            "AIRBRANCH.esSchema.STROPTOUT, " &
            "AIRBRANCH.esSchema.STRCONFIRMATIONNBR, " &
            "AIRBRANCH.esSchema.STRCONTACTFIRSTNAME, " &
            "AIRBRANCH.esSchema.STRCONTACTLASTNAME, " &
            "AIRBRANCH.esSchema.STRCONTACTCOMPANY, " &
            "AIRBRANCH.esSchema.STRCONTACTADDRESS1, " &
            "AIRBRANCH.esSchema.STRCONTACTCITY, " &
            "AIRBRANCH.esSchema.STRCONTACTSTATE, " &
            "AIRBRANCH.esSchema.STRCONTACTZIP, " &
            "AIRBRANCH.esSchema.STRCONTACTEMAIL, " &
            "AIRBRANCH.esSchema.STRCONTACTPHONENUMBER " &
            "from AIRBRANCH.esSchema " &
            "where intESyear = '" & intYear & "' " &
            "and AIRBRANCH.esSchema.STRDATEFIRSTCONFIRM is not NULL " &
            "and to_date(AIRBRANCH.esSchema.STRDATEFIRSTCONFIRM) > '" & deadline & "' " &
            "order by AIRBRANCH.esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STROPTOUT").HeaderText = "OptOut Status"
            dgvESDataCount.Columns("STROPTOUT").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvESDataCount.Columns("STRCONTACTCOMPANY").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTCOMPANY").DisplayIndex = 6
            dgvESDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Street Address"
            dgvESDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 7
            dgvESDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvESDataCount.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvESDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvESDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvESDataCount.Columns("STRCONTACTZIP").HeaderText = "Zip"
            dgvESDataCount.Columns("STRCONTACTZIP").DisplayIndex = 10
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 11
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").HeaderText = "Phone No."
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").DisplayIndex = 12

            txtTotaloutofcompliance.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtTotaloutofcompliance.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewINCompliance_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewINCompliance.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)
            Dim DeadlineYear As String = txtESYear.Text

            Dim deadline As String = ""
            If DeadlineYear <> "" Then
                DeadlineYear = CInt(DeadlineYear) + 1
                deadline = "15-Jun-" & DeadlineYear
            Else
                deadline = "15-Jun-2007"
            End If

            SQL = "SELECT AIRBRANCH.esSchema.STRAIRSNUMBER, " &
            "AIRBRANCH.esSchema.STRFACILITYNAME, " &
            "AIRBRANCH.esSchema.STRDATEFIRSTCONFIRM, " &
            "AIRBRANCH.esSchema.STRCONTACTPHONENUMBER " &
            "from AIRBRANCH.esSchema " &
            "where AIRBRANCH.esSchema.intESyear = '" & intYear & "' " &
             "and AIRBRANCH.esSchema.STRDATEFIRSTCONFIRM is not NULL " &
            "and to_date(AIRBRANCH.esSchema.STRDATEFIRSTCONFIRM) <= '" & deadline & "' " &
            "order by AIRBRANCH.esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "Date First Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").DisplayIndex = 3

            txtTotalincompliance.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtTotalincompliance.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub

    Private Sub lblViewESData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewESData.LinkClicked
        Dim year As Integer = CInt(cboYear.SelectedItem)
        Try

            SQL = "SELECT STRAIRSNUMBER, " &
            "STRFACILITYNAME, " &
            "DBLVOCEMISSION, " &
            "STRCONFIRMATIONNBR, " &
            "DBLNOXEMISSION, " &
            "STRDATEFIRSTCONFIRM " &
            "from AIRBRANCH.esSchema " &
            "where intESyear = '" & year & "' " &
            "order by STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("DBLVOCEMISSION").HeaderText = "VOC Emissions"
            dgvESDataCount.Columns("DBLVOCEMISSION").DisplayIndex = 2
            dgvESDataCount.Columns("DBLNOXEMISSION").HeaderText = "NOX Emissions"
            dgvESDataCount.Columns("DBLNOXEMISSION").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 5

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub lblViewNonResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewNonResponse.LinkClicked
        Try

            Dim year As String = cboYear.SelectedItem

            SQL = "SELECT AIRBRANCH.esSchema.STRAIRSNUMBER, " &
          "AIRBRANCH.esSchema.STRFACILITYNAME " &
          "from AIRBRANCH.esMailOut, AIRBRANCH.ESSCHEMA " &
          "where AIRBRANCH.esSchema.INTESYEAR = '" & year & "'" &
          "and AIRBRANCH.esSchema.strOPTOUT is NULL " &
          "and AIRBRANCH.esmailout.STRAIRSYEAR = AIRBRANCH.ESSchema.STRAIRSYEAR(+) " &
          "order by AIRBRANCH.esMailOut.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1

            txtNonResponseCount.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtNonResponseCount.Text
            clearESData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblextraResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblextraResponse.LinkClicked
        Try

            Dim year As String = txtESYear.Text
            Dim intyear As Integer = Int(year)

            SQL = "SELECT dt_NotInMailout.SchemaAIRS, AIRBRANCH.esSchema.STRAIRSNUMBER, " &
            "AIRBRANCH.esSchema.STRFACILITYNAME, " &
            "AIRBRANCH.esSchema.STRCONTACTFIRSTNAME, " &
            "AIRBRANCH.esSchema.STRCONTACTLASTNAME, " &
            "AIRBRANCH.esSchema.STRCONTACTCOMPANY, " &
            "AIRBRANCH.esSchema.STRCONTACTEMAIL, " &
            "AIRBRANCH.esSchema.STRCONTACTPHONENUMBER " &
            "from (Select AIRBRANCH.ESSCHEMA.STRAIRSNUMBER AS SchemaAIRS, " &
            "AIRBRANCH.ESMAILOUT.STRAIRSNUMBER AS MailoutAIRS" &
            " From AIRBRANCH.ESMailout, AIRBRANCH.ESSCHEMA" &
            " Where AIRBRANCH.ESMAILOUT.STRAIRSYEAR (+)= AIRBRANCH.ESSCHEMA.STRAIRSYEAR " &
            "AND INTESYEAR=  '" & intyear & "' " &
            "AND AIRBRANCH.ESSCHEMA.STROPTOUT IS NOT NULL) " &
            "dt_NotInMailout, " &
            "AIRBRANCH.ESSCHEMA " &
            "Where AIRBRANCH.ESSCHEMA.STRAIRSNUMBER = SchemaAIRS " &
            "AND MailoutAIRS is NULL"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvESDataCount.Columns("STRCONTACTCOMPANY").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTCOMPANY").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 5
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").HeaderText = "Phone No."
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").DisplayIndex = 6

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
            txtextraResponse.Text = dgvESDataCount.RowCount.ToString

            clearESData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewOptIn_LinkClicked_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewOptIn.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT AIRBRANCH.esSchema.STRAIRSNUMBER, " &
            "AIRBRANCH.esSchema.STRFACILITYNAME, " &
            "AIRBRANCH.esSchema.STRDATEFIRSTCONFIRM, " &
            "AIRBRANCH.esSchema.STRCONFIRMATIONNBR " &
            "from AIRBRANCH.esSchema, AIRBRANCH.esmailout " &
            "where AIRBRANCH.esSchema.intESyear = '" & intYear & "' " &
            "and AIRBRANCH.esSchema.STROPTOUT = 'NO'" &
            "and AIRBRANCH.ESMAILOUT.STRAIRSYEAR = AIRBRANCH.ESSCHEMA.STRAIRSYEAR(+) " &
            "and AIRBRANCH.esSchema.STRDATEFIRSTCONFIRM is not NULL " &
            "order by AIRBRANCH.esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

            txtMailoutOptin.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtMailoutOptin.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewOptOut_LinkClicked_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewOptOut.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT AIRBRANCH.esSchema.STRAIRSNUMBER, " &
            "AIRBRANCH.esSchema.STRFACILITYNAME, " &
            "AIRBRANCH.esSchema.STRDATEFIRSTCONFIRM, " &
            "AIRBRANCH.esSchema.STRCONFIRMATIONNBR " &
            "from AIRBRANCH.esSchema, AIRBRANCH.esmailout " &
            "where AIRBRANCH.esSchema.intESyear = '" & intYear & "' " &
            "and AIRBRANCH.esSchema.STROPTOUT = 'YES'" &
            " and AIRBRANCH.ESMAILOUT.STRAIRSYEAR = AIRBRANCH.ESSCHEMA.STRAIRSYEAR(+) " &
            "and AIRBRANCH.esSchema.STRDATEFIRSTCONFIRM is not NULL " &
            "order by AIRBRANCH.esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

            txtMailOutOptOut.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtMailOutOptOut.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewExtraOptOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewExtraOptOut.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT AIRBRANCH.esSchema.STRAIRSNUMBER, " &
            "AIRBRANCH.esSchema.STRFACILITYNAME, " &
            "AIRBRANCH.esSchema.STRDATEFIRSTCONFIRM, " &
            "AIRBRANCH.esSchema.STRCONFIRMATIONNBR " &
            "from (select AIRBRANCH.esSchema.strairsyear as SchemaAIRS, " &
            "AIRBRANCH.esmailout.strairsyear as MailoutAIRS " &
            "From AIRBRANCH.ESMailout, AIRBRANCH.ESSCHEMA " &
            "where AIRBRANCH.ESMAILOUT.STRAIRSYEAR (+)= AIRBRANCH.ESSCHEMA.STRAIRSYEAR " &
            "and AIRBRANCH.esSchema.intESyear = '" & intYear & "' " &
            "and AIRBRANCH.ESSCHEMA.STROPTOUT IS NOT NULL) " &
            "dt_NotInMailout, AIRBRANCH.ESSCHEMA " &
            "Where AIRBRANCH.ESSCHEMA.STRAIRSYEAR = SchemaAIRS " &
            "and MailoutAIRS is NULL " &
            "and AIRBRANCH.ESSCHEMA.STROPTOUT='YES'"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 1
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 0
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

            txtExtraOptout.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtExtraOptout.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewExtraOptIn_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewExtraOptIn.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT AIRBRANCH.esSchema.STRAIRSNUMBER, " &
            "AIRBRANCH.esSchema.STRFACILITYNAME, " &
            "AIRBRANCH.esSchema.STRDATEFIRSTCONFIRM, " &
            "AIRBRANCH.esSchema.STRCONFIRMATIONNBR " &
            "from (select AIRBRANCH.esSchema.strairsyear as SchemaAIRS, " &
            "AIRBRANCH.esmailout.strairsyear as MailoutAIRS " &
            "From AIRBRANCH.ESMailout, AIRBRANCH.ESSCHEMA " &
            "where AIRBRANCH.ESMAILOUT.STRAIRSYEAR (+)= AIRBRANCH.ESSCHEMA.STRAIRSYEAR " &
            "and AIRBRANCH.esSchema.intESyear = '" & intYear & "' " &
            "and AIRBRANCH.ESSCHEMA.STROPTOUT IS NOT NULL) " &
            "dt_NotInMailout, AIRBRANCH.ESSCHEMA " &
            "Where AIRBRANCH.ESSCHEMA.STRAIRSYEAR = SchemaAIRS " &
            "and MailoutAIRS is NULL " &
            "and AIRBRANCH.ESSCHEMA.STROPTOUT='NO'"


            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

            txtExtraOptin.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtExtraOptin.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub lblViewTotalResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewTotalResponse.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT AIRBRANCH.esSchema.STRAIRSNUMBER, " &
            "AIRBRANCH.esSchema.STRFACILITYNAME, " &
            "AIRBRANCH.esSchema.STRCONTACTFIRSTNAME, " &
            "AIRBRANCH.esSchema.STRCONTACTLASTNAME, " &
            "AIRBRANCH.esSchema.STRCONTACTCOMPANY, " &
            "AIRBRANCH.esSchema.STRCONTACTEMAIL, " &
            "AIRBRANCH.esSchema.STRCONTACTPHONENUMBER " &
            "from AIRBRANCH.esSchema " &
            "where AIRBRANCH.esSchema.intESyear = '" & intYear & "' " &
            "and AIRBRANCH.esSchema.STROPTOUT is not NULL " &
            "order by AIRBRANCH.esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvESDataCount.Columns("STRCONTACTCOMPANY").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTCOMPANY").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 5
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").HeaderText = "Phone Number"
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").DisplayIndex = 6

            txtTotalResponse.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtTotalResponse.Text
            clearESData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub clearESData()
        Try
            txtESAirsNo.Text = ""
            txtFACILITYNAME.Text = ""
            txtFACILITYADDRESS.Text = ""
            txtFACILITYCITY.Text = ""
            txtFACILITYSTATE.Text = ""
            txtFACILITYZIP.Text = ""
            txtCOUNTY.Text = ""
            txtXCOORDINATE.Text = ""
            txtYCOORDINATE.Text = ""
            txtHORIZONTALCOLLECTIONCODE.Text = ""
            txtHORIZONTALACCURACYMEASURE.Text = ""
            txtHORIZONTALREFERENCECODE.Text = ""
            txtCompany.Text = ""
            txtTitle.Text = ""
            txtPhone.Text = ""
            txtFax.Text = ""
            txtContactFirstName.Text = ""
            txtContactLastName.Text = ""
            txtAddress1.Text = ""
            txtAddress2.Text = ""
            txtCity.Text = ""
            txtState.Text = ""
            txtZip.Text = ""
            txtEmail.Text = ""
            txtVOCEmission.Text = ""
            txtNOXEmission.Text = ""
            txtConfirmationNbr.Text = ""
            txtFirstConfirmedDate.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnoutofcomplianceExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnoutofcomplianceExport.Click
        dgvESDataCount.ExportToExcel(Me)
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If txtFACILITYNAME.Text = "" Then
                MsgBox("You must select a Facility from the data grid view")
            Else
                PrintOut = Nothing
                If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
                PrintOut.txtPrintType.Text = "ES Print Out"
                PrintOut.txtSQLLine.Text = Me.txtConfirmationNumber.Text
                PrintOut.Show()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#End Region
    Private Sub mmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiBack.Click
        Try
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub SSCPEmissionSummaryTool_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Try
            If Me.Size.Width > 560 Then
                SplitContainer1.SanelySetSplitterDistance(556)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem.Click
        OpenDocumentationUrl(Me)
    End Sub
    Private Sub btnEISummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISummary.Click
        Try
            'If cboEIYear.Text <> "" And cboEIYear.Items.Contains(cboEIYear.Text) And cboEIYear.SelectedIndex > 0 Then
            If cboEIYear.Text <> "" And cboEIYear.SelectedIndex > 0 Then
                If cboEIYear.Text < 2010 Then
                    SQL = "select " &
                    "rownum as EIRows, " &
                    "AIRSNumber, FacilityName, " &
                    "SO2, NOX, VOC, CO, NH3, Lead, " &
                    "PMFIL, '' as PM10FIL, PMPRI, PM10PRI, PM25PRI, " &
                    "'' as PMCON " &
                    "from " &
                    "(select " &
                    "rownum as EIRows, " &
                    "substr(strairsnumber,5,8) as AIRSNumber, strfacilityname as FacilityName, " &
                    "SO2, NOX, PMPRI, PMFIL, PM10PRI, PM25PRI, VOC, " &
                    "CO, NH3, Lead " &
                    "from " &
                    "(select dt.strairsnumber, dt.strfacilityname, " &
                    "sum(case when dt.strpollutantcode='SO2' then pollutanttotal else null end) SO2, " &
                    "sum(case when dt.strpollutantcode='NOX' then pollutanttotal else null end) NOx, " &
                    "sum(case when dt.strpollutantcode='PM-PRI' then pollutanttotal else null end) PMPRI, " &
                    "sum(case when dt.strpollutantcode='PM-FIL' then pollutanttotal else null end) PMFIL, " &
                    "sum(case when dt.strpollutantcode='PM10-PRI' then pollutanttotal else null end) PM10PRI, " &
                    "sum(case when dt.strpollutantcode='PM25-PRI' then pollutanttotal else null end) PM25PRI, " &
                    "sum(case when dt.strpollutantcode='VOC' then pollutanttotal else null end) VOC, " &
                    "sum(case when dt.strpollutantcode='CO' then pollutanttotal else null end) CO, " &
                    "sum(case when dt.strpollutantcode='NH3' then pollutanttotal else null end) NH3, " &
                    "sum(case when dt.strpollutantcode='7439921' then pollutanttotal else null end) Lead " &
                    "from " &
                    "(Select dtSumPollutant.strairsnumber, eisi.strfacilityname, dtSumPollutant.strpollutantcode, " &
                    "dtSumPollutant.PollutantTotal, dtSumPollutant.strinventoryyear " &
                    "from airbranch.eisi, " &
                    "(select eiem.strairsnumber, eiem.strpollutantcode, sum(eiem.dblemissionnumericvalue) as PollutantTotal, eiem.strinventoryyear " &
                    "from AIRBRANCH.eiem " &
                    "where eiem.strinventoryyear='" & cboEIYear.Text & "' " &
                    "group by eiem.strairsnumber, eiem.strpollutantcode, eiem.strinventoryyear) dtSumPollutant " &
                    "where eisi.strairsnumber = dtSumPollutant.strairsnumber and " &
                    "eisi.strinventoryyear = dtSumPollutant.strinventoryyear ) dt " &
                    "group by dt.strairsnumber, dt.strfacilityname) " &
                    "order by AIRSNumber) "

                    ds = New DataSet
                    da = New OracleDataAdapter(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    da.Fill(ds, "EISummary")
                    dgvEIResults.DataSource = ds
                    dgvEIResults.DataMember = "EISummary"

                    dgvEIResults.RowHeadersVisible = False
                    dgvEIResults.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvEIResults.AllowUserToResizeColumns = True
                    dgvEIResults.AllowUserToAddRows = False
                    dgvEIResults.AllowUserToDeleteRows = False
                    dgvEIResults.AllowUserToOrderColumns = True
                    dgvEIResults.AllowUserToResizeRows = True

                    dgvEIResults.Columns("EIRows").HeaderText = "#"
                    dgvEIResults.Columns("EIRows").DisplayIndex = 0
                    dgvEIResults.Columns("EIRows").Width = 25
                    dgvEIResults.Columns("EIRows").Visible = False
                    dgvEIResults.Columns("AIRSNumber").HeaderText = "Airs No."
                    dgvEIResults.Columns("AIRSNumber").DisplayIndex = 1
                    dgvEIResults.Columns("AIRSNumber").Width = 75
                    dgvEIResults.Columns("FacilityName").HeaderText = "Facility Name"
                    dgvEIResults.Columns("FacilityName").DisplayIndex = 2
                    dgvEIResults.Columns("FacilityName").Width = 225
                    dgvEIResults.Columns("SO2").HeaderText = "Sulfur Dioxide"
                    dgvEIResults.Columns("SO2").DisplayIndex = 3
                    dgvEIResults.Columns("NOX").HeaderText = "Nitrogen Oxides"
                    dgvEIResults.Columns("NOX").DisplayIndex = 4
                    dgvEIResults.Columns("VOC").HeaderText = "Volatile Organic Compounds"
                    dgvEIResults.Columns("VOC").DisplayIndex = 5
                    dgvEIResults.Columns("CO").HeaderText = "Carbon Monoxide"
                    dgvEIResults.Columns("CO").DisplayIndex = 6
                    dgvEIResults.Columns("NH3").HeaderText = "Ammonia "
                    dgvEIResults.Columns("NH3").DisplayIndex = 7
                    dgvEIResults.Columns("Lead").HeaderText = "Lead"
                    dgvEIResults.Columns("Lead").DisplayIndex = 8
                    dgvEIResults.Columns("PMPRI").HeaderText = "PM Primary - old EI"
                    dgvEIResults.Columns("PMPRI").DisplayIndex = 9
                    dgvEIResults.Columns("PM10PRI").HeaderText = "Primary PM10 (Includes Filterables + Condensibles)"
                    dgvEIResults.Columns("PM10PRI").DisplayIndex = 10
                    'dgvEIResults.Columns("PM10FIL").HeaderText = "Filterable PM10"
                    'dgvEIResults.Columns("PM10FIL").DisplayIndex = 10
                    dgvEIResults.Columns("PM25PRI").HeaderText = "Primary PM 2.5 (Includes Filterables + Condensibles)"
                    dgvEIResults.Columns("PM25PRI").DisplayIndex = 11
                    dgvEIResults.Columns("PMFIL").HeaderText = "Filterable PM 2.5"
                    dgvEIResults.Columns("PMFIL").DisplayIndex = 12
                    'dgvEIResults.Columns("PMCON").HeaderText = "Condensible PM (All less than 1 micron)"
                    'dgvEIResults.Columns("PMCON").DisplayIndex = 14

                Else
                    SQL = "select " &
                    " rownum as EIRows, " &
                    "ViewList.FacilitySiteID as AIRSNumber, " &
                    "strFacilityName as FacilityName, " &
                    "SO2, NOX, VOC,  CO, NH3, LEAD, " &
                    "PMFIL, PM10FIL, PM10PRI, PM25PRI, PMCON " &
                    "from " &
                    "(select distinct (FacilitySiteID ) as FacilitySiteID " &
                    "from airbranch.VW_EIS_RPEMISSIONS " &
                    "where intinventoryyear = '" & cboEIYear.Text & "')ViewList,   " &
                    "(select facilitysiteid, sum(fltTotalemissions) as VOC    " &
                    "from airbranch.VW_EIS_RPEMISSIONS " &
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'VOC'  " &
                    "group by facilitysiteid) VOCSum,  " &
                    "(select facilitysiteid, sum(fltTotalemissions) as PMFIL " &
                    "from airbranch.VW_EIS_RPEMISSIONS " &
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'PM25-FIL'  " &
                    "group by facilitysiteid) PM25FILSum,  " &
                    "(select facilitysiteid, sum(fltTotalemissions) as LEAD    " &
                    "from airbranch.VW_EIS_RPEMISSIONS " &
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = '7439921'  " &
                    "group by facilitysiteid) LEADSum,  " &
                    "(select facilitysiteid, sum(fltTotalemissions) as PM10PRI    " &
                    "from airbranch.VW_EIS_RPEMISSIONS " &
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'PM10-PRI'  " &
                    "group by facilitysiteid) PM10PRISum,  " &
                    "(select facilitysiteid, sum(fltTotalemissions) as PM25PRI    " &
                    "from airbranch.VW_EIS_RPEMISSIONS " &
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'PM25-PRI'  " &
                    "group by facilitysiteid) PM25PRISum,  " &
                    "(select facilitysiteid, sum(fltTotalemissions) as PMCON    " &
                    "from airbranch.VW_EIS_RPEMISSIONS " &
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'PM-CON'  " &
                    "group by facilitysiteid) PMCONSum,  " &
                    "(select facilitysiteid, sum(fltTotalemissions) as PM10FIL    " &
                    "from airbranch.VW_EIS_RPEMISSIONS " &
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'PM10-FIL'  " &
                    "group by facilitysiteid) PM10FILSum,  " &
                    "(select facilitysiteid, sum(fltTotalemissions) as NH3    " &
                    "from airbranch.VW_EIS_RPEMISSIONS " &
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'NH3'  " &
                    "group by facilitysiteid) NH3Sum,  " &
                    "(select facilitysiteid, sum(fltTotalemissions) as SO2    " &
                    "from airbranch.VW_EIS_RPEMISSIONS " &
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'SO2'  " &
                    "group by facilitysiteid) SO2Sum,  " &
                    "(select facilitysiteid, sum(fltTotalemissions) as CO    " &
                    "from airbranch.VW_EIS_RPEMISSIONS " &
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'CO'  " &
                    "group by facilitysiteid) COSum,  " &
                    "(select facilitysiteid, sum(fltTotalemissions) as NOX    " &
                    "from airbranch.VW_EIS_RPEMISSIONS " &
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'NOX'  " &
                    "group by facilitysiteid)NOXSum,  " &
                    "AIRBranch.APBFacilityInformation  " &
                    "where '0413'||ViewList.facilitysiteid = APBFacilityInformation.strAIRSNumber " &
                    "and ViewList.facilitysiteid = VOCSum.facilitysiteid (+)  " &
                    "and ViewList.facilitysiteid  = PM25FILSum.facilitysiteid (+)  " &
                    "and ViewList.facilitysiteid  = LEADSum.facilitysiteid (+)  " &
                    "and ViewList.facilitysiteid  = PM10PRISum.facilitysiteid (+)  " &
                    "and ViewList.facilitysiteid  = PM25PRISum.facilitysiteid (+)  " &
                    "and ViewList.facilitysiteid  = PMCONSum.facilitysiteid (+)  " &
                    "and ViewList.facilitysiteid  = PM10FILSum.facilitysiteid (+)  " &
                    "and ViewList.facilitysiteid  = NH3Sum.facilitysiteid (+)  " &
                    "and ViewList.facilitysiteid  = SO2Sum.facilitysiteid (+)  " &
                    "and ViewList.facilitysiteid  = COSum.facilitysiteid (+)  " &
                    "and ViewList.facilitysiteid  = NOXSum.facilitysiteid (+) "

                    ds = New DataSet
                    da = New OracleDataAdapter(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    da.Fill(ds, "EISummary")
                    dgvEIResults.DataSource = ds
                    dgvEIResults.DataMember = "EISummary"

                    dgvEIResults.RowHeadersVisible = False
                    dgvEIResults.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvEIResults.AllowUserToResizeColumns = True
                    dgvEIResults.AllowUserToAddRows = False
                    dgvEIResults.AllowUserToDeleteRows = False
                    dgvEIResults.AllowUserToOrderColumns = True
                    dgvEIResults.AllowUserToResizeRows = True

                    dgvEIResults.Columns("EIRows").HeaderText = "#"
                    dgvEIResults.Columns("EIRows").DisplayIndex = 0
                    dgvEIResults.Columns("EIRows").Width = 25
                    dgvEIResults.Columns("EIRows").Visible = False
                    dgvEIResults.Columns("AIRSNumber").HeaderText = "Airs No."
                    dgvEIResults.Columns("AIRSNumber").DisplayIndex = 1
                    dgvEIResults.Columns("AIRSNumber").Width = 75
                    dgvEIResults.Columns("FacilityName").HeaderText = "Facility Name"
                    dgvEIResults.Columns("FacilityName").DisplayIndex = 2
                    dgvEIResults.Columns("FacilityName").Width = 225
                    dgvEIResults.Columns("SO2").HeaderText = "Sulfur Dioxide"
                    dgvEIResults.Columns("SO2").DisplayIndex = 3
                    dgvEIResults.Columns("NOX").HeaderText = "Nitrogen Oxides"
                    dgvEIResults.Columns("NOX").DisplayIndex = 4
                    dgvEIResults.Columns("VOC").HeaderText = "Volatile Organic Compounds"
                    dgvEIResults.Columns("VOC").DisplayIndex = 5
                    dgvEIResults.Columns("CO").HeaderText = "Carbon Monoxide"
                    dgvEIResults.Columns("CO").DisplayIndex = 6
                    dgvEIResults.Columns("NH3").HeaderText = "Ammonia "
                    dgvEIResults.Columns("NH3").DisplayIndex = 7
                    dgvEIResults.Columns("Lead").HeaderText = "Lead"
                    dgvEIResults.Columns("Lead").DisplayIndex = 8
                    dgvEIResults.Columns("PMCON").HeaderText = "Condensible PM"
                    dgvEIResults.Columns("PMCON").DisplayIndex = 9
                    dgvEIResults.Columns("PM10PRI").HeaderText = "Primary PM10 (Includes Filterables + Condensibles)"
                    dgvEIResults.Columns("PM10PRI").DisplayIndex = 10
                    dgvEIResults.Columns("PM10FIL").HeaderText = "Filterable PM10"
                    dgvEIResults.Columns("PM10FIL").DisplayIndex = 11
                    dgvEIResults.Columns("PM25PRI").HeaderText = "Primary PM 2.5 (Includes Filterables + Condensibles)"
                    dgvEIResults.Columns("PM25PRI").DisplayIndex = 12
                    dgvEIResults.Columns("PMFIL").HeaderText = "Filterable PM 2.5"
                    dgvEIResults.Columns("PMFIL").DisplayIndex = 13

                End If


            End If
            Exit Sub


            'Original SQL statement from Brian Gregory 
            SQL = "select substr(strairsnumber,5,8) as AIRSNumber, strfacilityname as FacilityName, SO2, NOX, PMPRI, PMFIL, PM10PRI, PM25PRI, VOC, " &
            "CO, NH3, Lead " &
            "from " &
            "(select dt.strairsnumber, dt.strfacilityname, " &
            "sum(case when dt.strpollutantcode='SO2' then pollutanttotal else null end) SO2, " &
            "sum(case when dt.strpollutantcode='NOX' then pollutanttotal else null end) NOx, " &
            "sum(case when dt.strpollutantcode='PM-PRI' then pollutanttotal else null end) PMPRI, " &
            "sum(case when dt.strpollutantcode='PM-FIL' then pollutanttotal else null end) PMFIL, " &
            "sum(case when dt.strpollutantcode='PM10-PRI' then pollutanttotal else null end) PM10PRI, " &
            "sum(case when dt.strpollutantcode='PM25-PRI' then pollutanttotal else null end) PM25PRI, " &
            "sum(case when dt.strpollutantcode='VOC' then pollutanttotal else null end) VOC, " &
            "sum(case when dt.strpollutantcode='CO' then pollutanttotal else null end) CO, " &
            "sum(case when dt.strpollutantcode='NH3' then pollutanttotal else null end) NH3, " &
            "sum(case when dt.strpollutantcode='7439921' then pollutanttotal else null end) Lead " &
            "from " &
            "(Select dtSumPollutant.strairsnumber, eisi.strfacilityname, dtSumPollutant.strpollutantcode, " &
             "dtSumPollutant.PollutantTotal, dtSumPollutant.strinventoryyear " &
            "from airbranch.eisi, " &
            "(select eiem.strairsnumber, eiem.strpollutantcode, sum(eiem.dblemissionnumericvalue) as PollutantTotal, eiem.strinventoryyear " &
            "from airbranch.eiem " &
            "where eiem.strinventoryyear='2005' " &
            "group by eiem.strairsnumber, eiem.strpollutantcode, eiem.strinventoryyear) dtSumPollutant " &
            "where eisi.strairsnumber = dtSumPollutant.strairsnumber and " &
            "eisi.strinventoryyear = dtSumPollutant.strinventoryyear ) dt " &
            "group by dt.strairsnumber, dt.strfacilityname) "


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewEISummaryByPollutant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewEISummaryByPollutant.Click
        Try

            If cboEIYear.Text <> "" And cboEIYear.SelectedIndex > 0 Then
                If cboEIPollutants.Text <> "" Then
                    If cboEIYear.Text < 2010 Then
                        SQL = "select " &
                        "rownum as EIRow, " &
                        "AIRSNumber, FacilityName, " &
                        "Pollutant " &
                        "from " &
                        "(select " &
                        "substr(dt.strairsnumber, 5) as AIRSNumber, " &
                        "eisi.strfacilityname as FacilityName, " &
                        "dt.pollutanttotal as Pollutant " &
                        "from AIRBRANCH.eisi," &
                        "(select strairsnumber, strpollutantcode, " &
                        "sum(dblemissionnumericvalue) as PollutantTotal, strinventoryyear " &
                        "from AIRBRANCH.eiem " &
                        "where strinventoryyear = '" & cboEIYear.Text & "' and " &
                        "strpollutantcode = '" & cboEIPollutants.SelectedValue & "' " &
                        "group by eiem.strairsnumber, eiem.strinventoryyear, strpollutantcode) dt " &
                        "where eisi.strairsnumber = dt.strairsnumber and " &
                        "eisi.strinventoryyear = dt.strinventoryyear " &
                        "order by AIRSNumber) "
                    Else
                        SQL = "select  " &
                        "rownum as EIRow,  " &
                        "ViewList.FacilitySiteID as AIRSNumber,  " &
                        "strFacilityName as FacilityName,  " &
                        "Pollutant " &
                        "from  " &
                        "(select distinct (FacilitySiteID ) as FacilitySiteID  " &
                        "from airbranch.VW_EIS_RPEMISSIONS  " &
                        "where intinventoryyear = '" & cboEIYear.Text & "')ViewList,    " &
                        "(select facilitysiteid, sum(fltTotalemissions) as Pollutant     " &
                        "from airbranch.VW_EIS_RPEMISSIONS  " &
                        "where intinventoryyear = '" & cboEIYear.Text & "' " &
                        "and pollutantcode = '" & cboEIPollutants.SelectedValue & "'   " &
                        "group by facilitysiteid) PollutantSum, " &
                        "AIRBranch.APBFacilityInformation   " &
                        "where '0413'||ViewList.facilitysiteid = APBFacilityInformation.strAIRSNumber  " &
                        "and ViewList.facilitysiteid = PollutantSum.facilitysiteid    "

                    End If
                    ds = New DataSet
                    da = New OracleDataAdapter(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    da.Fill(ds, "EISummary")
                    dgvEIResults.DataSource = ds
                    dgvEIResults.DataMember = "EISummary"

                    dgvEIResults.RowHeadersVisible = False
                    dgvEIResults.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvEIResults.AllowUserToResizeColumns = True
                    dgvEIResults.AllowUserToAddRows = False
                    dgvEIResults.AllowUserToDeleteRows = False
                    dgvEIResults.AllowUserToOrderColumns = True
                    dgvEIResults.AllowUserToResizeRows = True

                    dgvEIResults.Columns("EIRow").HeaderText = "#"
                    dgvEIResults.Columns("EIRow").DisplayIndex = 0
                    dgvEIResults.Columns("EIRow").Width = 25
                    dgvEIResults.Columns("EIRow").Visible = False
                    dgvEIResults.Columns("AIRSNumber").HeaderText = "Airs No."
                    dgvEIResults.Columns("AIRSNumber").DisplayIndex = 1
                    dgvEIResults.Columns("AIRSNumber").Width = 75
                    dgvEIResults.Columns("FacilityName").HeaderText = "Facility Name"
                    dgvEIResults.Columns("FacilityName").DisplayIndex = 2
                    dgvEIResults.Columns("FacilityName").Width = 225
                    dgvEIResults.Columns("Pollutant").HeaderText = cboEIPollutants.Text
                    dgvEIResults.Columns("Pollutant").DisplayIndex = 3

                    txtEICount.Text = dgvEIResults.RowCount.ToString

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnExportEItoExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportEItoExcel.Click
        dgvEIResults.ExportToExcel(Me)
    End Sub
End Class