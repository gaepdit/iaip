Imports Oracle.DataAccess.Client


Public Class SSCPEmissionSummaryTool
    Dim SQL, SQL2 As String
    Dim cmd, cmd2 As OracleCommand
    Dim dr, dr2 As OracleDataReader
    Dim recexist As Boolean
    Dim daViewCount As OracleDataAdapter
    Dim dsViewCount As DataSet
    Dim dsES As DataSet
    Dim daES As OracleDataAdapter
    Dim ds As DataSet
    Dim da As OracleDataAdapter

    Private Sub SSCPEmissionSummaryTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            pnl1.Text = ""
            pnl2.Text = UserName
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
            SQL = "Select " & _
            "distinct intESYear " & _
            "from " & DBNameSpace & ".esschema " & _
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

            SQL = "Select " & _
            "distinct(strInventoryYear) as EIYear " & _
            "from " & DBNameSpace & ".EISI " & _
            "where strInventoryYear < 2010 " & _
            "order by strInventoryYear desc "

            SQL = "select * from " & _
            "(select " & _
            "distinct(inventoryyear) as EIYear " & _
            "from airbranch.eis_admin " & _
            "union " & _
            "Select  " & _
            "distinct(to_number(strInventoryYear)) as EIYear " & _
            "from AIRBranch.EISI " & _
            "where strInventoryYear < 2010 ) " & _
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

            'SQL = "select  " & _
            '"distinct(AIRBranch.EIEM.strPollutantCode) as Pollutants,   " & _
            '"strPollutantDesc  " & _
            '"from AIRBranch.EILookUpPollutantCodes, AIRBranch.EIEM  " & _
            '"where AIRBranch.EIEM.strPollutantCode = AIRBranch.EILookUpPollutantCodes.strPollutantCode  "

            SQL = "select " & _
            "distinct(AIRBranch.EIEM.strPollutantCode) as Pollutants,   " & _
            "strPollutantDesc  " & _
            "from AIRBranch.EILookUpPollutantCodes, AIRBranch.EIEM " & _
            "where AIRBranch.EIEM.strPollutantCode = AIRBranch.EILookUpPollutantCodes.strPollutantCode " & _
            "union " & _
            "select distinct (AIRBranch.VW_EIS_RPEMISSIONS.PollutantCode), " & _
            "AIRBranch.EISLK_PollutantCode .strDesc " & _
            "from AIRBranch.VW_EIS_RPEMISSIONS, AIRBranch.EISLK_PollutantCode " & _
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
        Dim year As Integer = CInt(cboYear.SelectedItem)
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
                SQL = "select count(*) as MailoutCount " & _
                "from " & DBNameSpace & ".esmailout, " & DBNameSpace & ".ESSCHEMA " & _
                "where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR = " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR(+) " & _
                "and " & DBNameSpace & ".esmailout.STRESYEAR = '" & ESYear & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtMailOutCount.Text = dr.Item(MailoutCount)
                End While
                dr.Close()

                SQL = "select count(*) as ResponseCount " & _
                "from " & DBNameSpace & ".esmailout, " & DBNameSpace & ".ESSCHEMA " & _
                "where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR = " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
                "and " & DBNameSpace & ".ESSCHEMA.STROPTOUT is not NULL " & _
                "and " & DBNameSpace & ".esmailout.STRESYEAR = '" & ESYear & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtResponseCount.Text = dr.Item(ResponseCount)
                End While
                dr.Close()

                SQL = "select count(*) as TotaloptinCount " & _
                "from " & DBNameSpace & ".ESSchema " & _
                "where " & DBNameSpace & ".ESSchema.intESYEAR = '" & intESyear & "'" & _
                " and " & DBNameSpace & ".ESSchema.strOptOut = 'NO'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                While dr.Read()
                    txtTotalOptInCount.Text = dr.Item(TotaloptinCount)
                End While
                dr.Close()

                SQL = "select count(*) as TotaloptOutCount " & _
                "from " & DBNameSpace & ".ESSchema " & _
                "where " & DBNameSpace & ".ESSchema.intESYEAR = '" & intESyear & "' " & _
                "and " & DBNameSpace & ".ESSchema.strOptOut = 'YES'"

                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                While dr.Read()
                    txtTotalOptOutCount.Text = dr.Item(TotaloptoutCount)
                End While
                dr.Close()

                SQL = "select count(*) as TotalinincomplianceCount " & _
                "from " & DBNameSpace & ".ESSchema " & _
                "where " & DBNameSpace & ".ESSchema.intESYEAR = '" & intESyear & "'" & _
                " and to_date(" & DBNameSpace & ".ESSchema.STRDATEFIRSTCONFIRM) < = '" & deadline & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read()
                    txtTotalincompliance.Text = dr.Item(TotalinincomplianceCount)
                End While
                dr.Close()

                SQL = "select count(*) as TotaloutofcomplianceCount " & _
                "from " & DBNameSpace & ".ESSchema " & _
                "where " & DBNameSpace & ".ESSchema.intESYEAR = '" & intESyear & "'" & _
                " and to_date(" & DBNameSpace & ".ESSchema.STRDATEFIRSTCONFIRM) > '" & deadline & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read()
                    txtTotaloutofcompliance.Text = dr.Item(TotaloutofcomplianceCount)
                End While
                dr.Close()

                SQL = "select count(*) as MailOutOptInCount " & _
                "from " & DBNameSpace & ".ESSchema, " & DBNameSpace & ".ESMailout " & _
                "where " & DBNameSpace & ".ESMAILOUT.strESYEAR = '" & ESYear & "' " & _
                " and " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR = " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR(+) " & _
                " and " & DBNameSpace & ".ESSchema.strOptOut = 'NO'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtMailoutOptin.Text = dr.Item(MailOutOptInCount)
                End While
                dr.Close()

                SQL = "select count(*) as MailOutOptOutCount " & _
                "from " & DBNameSpace & ".ESSchema, " & DBNameSpace & ".ESMailout " & _
                "where " & DBNameSpace & ".ESMAILOUT.strESYEAR = '" & ESYear & "'" & _
                " and " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR = " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR(+) " & _
                " and " & DBNameSpace & ".ESSchema.strOptOut = 'YES'"

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


            SQL = "select count(*) as Nonresponsecount " & _
             "from " & DBNameSpace & ".ESSCHEMA " & _
             "where " & DBNameSpace & ".ESSCHEMA.intESYEAR = '" & ESYear & "'" & _
             " and " & DBNameSpace & ".ESSchema.strOptOut is NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read()
                txtNonResponseCount.Text = dr.Item(Nonresponsecount)
            End While
            dr.Close()

            SQL = "select count(*) as ExtraCount " & _
            "from (Select " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, " & _
            "" & DBNameSpace & ".ESMAILOUT.STRAIRSYear AS MailoutAIRS " & _
            "From " & DBNameSpace & ".ESMailout, " & DBNameSpace & ".ESSCHEMA" & _
            " Where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "AND " & DBNameSpace & ".esschema.INTESYEAR= '" & intESyear & "' " & _
            "AND " & DBNameSpace & ".ESSCHEMA.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & _
            "" & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR = SchemaAIRS " & _
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

            SQL = "select count(*) as ExtraOptinCount " & _
            "from (Select " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, " & _
            "" & DBNameSpace & ".ESMAILOUT.STRAIRSYear AS MailoutAIRS " & _
            "From " & DBNameSpace & ".ESMailout, " & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "AND " & DBNameSpace & ".esschema.INTESYEAR= '" & intESyear & "' " & _
            "AND " & DBNameSpace & ".ESSCHEMA.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & _
            "" & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR = SchemaAIRS " & _
            "AND MailoutAIRS is NULL " & _
            "and " & DBNameSpace & ".ESSCHEMA.STROPTOUT='NO'"

            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtExtraOptin.Text = dr.Item(extraOptincount)
            End While
            dr.Close()

            SQL = "select count(*) as ExtraOptOUTCount " & _
            "from (Select " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, " & _
            "" & DBNameSpace & ".ESMAILOUT.STRAIRSYear AS MailoutAIRS " & _
            "From " & DBNameSpace & ".ESMailout, " & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "AND " & DBNameSpace & ".esschema.INTESYEAR= '" & intESyear & "' " & _
            "AND " & DBNameSpace & ".ESSCHEMA.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & _
            "" & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR = SchemaAIRS " & _
            "AND MailoutAIRS is NULL " & _
            "and " & DBNameSpace & ".ESSCHEMA.STROPTOUT='YES'"

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

            SQL = "select count(*) as TotalResponsecount " & _
            "from " & DBNameSpace & ".ESSchema " & _
            "where " & DBNameSpace & ".ESSchema.intESYEAR = '" & intESyear & "'" & _
            " and " & DBNameSpace & ".ESSchema.strOptOut is not NULL"

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

            SQL = "SELECT * " & _
            "from " & DBNameSpace & ".esschema " & _
            "where STRAIRSNUMBER = '" & AirsNo & "' " & _
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
    Sub ExportEStoExcel()
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        'Dim ExcelApp As New Excel.Application
        Dim i As Integer
        Dim j As Integer

        Try
            If dgvESDataCount.RowCount <> 0 Then
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    For i = 0 To dgvESDataCount.ColumnCount - 1
                        .Cells(1, i + 1) = dgvESDataCount.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvESDataCount.ColumnCount - 1
                        'For j = 0 To dgvESDataCount.RowCount - 2
                        For j = 0 To dgvESDataCount.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvESDataCount.Item(i, j).Value.ToString
                        Next
                    Next

                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
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

            SQL = "SELECT STRAIRSNUMBER, " & _
            "STRFACILITYNAME, " & _
            "STRCONTACTFIRSTNAME, " & _
            "STRCONTACTLASTNAME, " & _
            "STRCONTACTCOMPANYname, " & _
            "STRCONTACTADDRESS1, " & _
            "STRCONTACTCITY, " & _
            "STRCONTACTSTATE, " & _
            "STRCONTACTZIPCODE, " & _
            "STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".esMailOut " & _
            "where STRESYEAR = '" & year & "' " & _
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

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR " & _
            "from " & DBNameSpace & ".esSchema, " & DBNameSpace & ".esmailout " & _
            "where " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".esSchema.STROPTOUT = 'NO'" & _
            "and " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

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

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR " & _
            "from " & DBNameSpace & ".esSchema, " & DBNameSpace & ".esmailout  " & _
            "where " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "and " & DBNameSpace & ".esSchema.STROPTOUT = 'YES'" & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

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

            SQL = "SELECT airbranch.esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STROPTOUT, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTCOMPANY, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTADDRESS1, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTCITY, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTSTATE, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTZIP, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTEMAIL, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTPHONENUMBER " & _
            "from " & DBNameSpace & ".esSchema " & _
            "where intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM is not NULL " & _
            "and to_date(" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM) > '" & deadline & "' " & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

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

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTPHONENUMBER " & _
            "from " & DBNameSpace & ".esSchema " & _
            "where " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
             "and " & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM is not NULL " & _
            "and to_date(" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM) <= '" & deadline & "' " & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

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
    Private Sub lblViewESMailOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try

            Dim year As String = txtESYear.Text

            SQL = "SELECT STRAIRSNUMBER, " & _
            "STRFACILITYNAME, " & _
            "STRCONTACTFIRSTNAME, " & _
            "STRCONTACTLASTNAME, " & _
            "STRCONTACTCOMPANYname, " & _
            "STRCONTACTADDRESS1, " & _
            "STRCONTACTCITY, " & _
            "STRCONTACTSTATE, " & _
            "STRCONTACTZIPCODE, " & _
            "STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".esMailOut " & _
            "where STRESYEAR = '" & year & "' " & _
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
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewESData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewESData.LinkClicked
        Dim year As Integer = CInt(cboYear.SelectedItem)
        Try

            dsES = New DataSet

            SQL = "SELECT STRAIRSNUMBER, " & _
            "STRFACILITYNAME, " & _
            "DBLVOCEMISSION, " & _
            "STRCONFIRMATIONNBR, " & _
            "DBLNOXEMISSION, " & _
            "STRDATEFIRSTCONFIRM " & _
            "from " & DBNameSpace & ".esSchema " & _
            "where intESyear = '" & year & "' " & _
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

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
          "" & DBNameSpace & ".esSchema.STRFACILITYNAME " & _
          "from " & DBNameSpace & ".esMailOut, " & DBNameSpace & ".ESSCHEMA " & _
          "where " & DBNameSpace & ".esSchema.INTESYEAR = '" & year & "'" & _
          "and " & DBNameSpace & ".esSchema.strOPTOUT is NULL " & _
          "and " & DBNameSpace & ".esmailout.STRAIRSYEAR = " & DBNameSpace & ".ESSchema.STRAIRSYEAR(+) " & _
          "order by " & DBNameSpace & ".esMailOut.STRFACILITYNAME"

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

            SQL = "SELECT dt_NotInMailout.SchemaAIRS, " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTCOMPANY, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTEMAIL, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTPHONENUMBER " & _
            "from (Select " & DBNameSpace & ".ESSCHEMA.STRAIRSNUMBER AS SchemaAIRS, " & _
            "" & DBNameSpace & ".ESMAILOUT.STRAIRSNUMBER AS MailoutAIRS" & _
            " From " & DBNameSpace & ".ESMailout, " & DBNameSpace & ".ESSCHEMA" & _
            " Where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "AND INTESYEAR=  '" & intyear & "' " & _
            "AND " & DBNameSpace & ".ESSCHEMA.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & _
            "" & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESSCHEMA.STRAIRSNUMBER = SchemaAIRS " & _
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

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR " & _
            "from " & DBNameSpace & ".esSchema, " & DBNameSpace & ".esmailout " & _
            "where " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".esSchema.STROPTOUT = 'NO'" & _
            "and " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR = " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR(+) " & _
            "and " & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM is not NULL " & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

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

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR " & _
            "from " & DBNameSpace & ".esSchema, " & DBNameSpace & ".esmailout " & _
            "where " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".esSchema.STROPTOUT = 'YES'" & _
            " and " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR = " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR(+) " & _
            "and " & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM is not NULL " & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

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

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR " & _
            "from (select " & DBNameSpace & ".esSchema.strairsyear as SchemaAIRS, " & _
            "" & DBNameSpace & ".esmailout.strairsyear as MailoutAIRS " & _
            "From " & DBNameSpace & ".ESMailout, " & DBNameSpace & ".ESSCHEMA " & _
            "where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "and " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".ESSCHEMA.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR = SchemaAIRS " & _
            "and MailoutAIRS is NULL " & _
            "and " & DBNameSpace & ".ESSCHEMA.STROPTOUT='YES'"

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

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR " & _
            "from (select " & DBNameSpace & ".esSchema.strairsyear as SchemaAIRS, " & _
            "" & DBNameSpace & ".esmailout.strairsyear as MailoutAIRS " & _
            "From " & DBNameSpace & ".ESMailout, " & DBNameSpace & ".ESSCHEMA " & _
            "where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "and " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".ESSCHEMA.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR = SchemaAIRS " & _
            "and MailoutAIRS is NULL " & _
            "and " & DBNameSpace & ".ESSCHEMA.STROPTOUT='NO'"


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

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTCOMPANY, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTEMAIL, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTPHONENUMBER " & _
            "from " & DBNameSpace & ".esSchema " & _
            "where " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".esSchema.STROPTOUT is not NULL " & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

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
    Private Sub DeleteESMailOut()
        Dim AirsNo As String = txtESAirsNo.Text
        Dim ESyear As String = txtESYear.Text

        Try
            SQL = "delete from " & DBNameSpace & ".ESMailOut " & _
            "where " & DBNameSpace & ".ESMailOut.STRAIRSNUMBER = '" & AirsNo & "' " & _
            "and " & DBNameSpace & ".ESMailOut.STRESYEAR = '" & ESyear & "'"

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
        Try
            ExportEStoExcel()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
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
                'PrintOut.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
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
        Try
            Help.ShowHelp(Label112, HelpUrl)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnEISummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISummary.Click
        Try
            'If cboEIYear.Text <> "" And cboEIYear.Items.Contains(cboEIYear.Text) And cboEIYear.SelectedIndex > 0 Then
            If cboEIYear.Text <> "" And cboEIYear.SelectedIndex > 0 Then
                If cboEIYear.Text < 2010 Then
                    SQL = "select " & _
                    "rownum as EIRows, " & _
                    "AIRSNumber, FacilityName, " & _
                    "SO2, NOX, VOC, CO, NH3, Lead, " & _
                    "PMFIL, '' as PM10FIL, PMPRI, PM10PRI, PM25PRI, " & _
                    "'' as PMCON " & _
                    "from " & _
                    "(select " & _
                    "rownum as EIRows, " & _
                    "substr(strairsnumber,5,8) as AIRSNumber, strfacilityname as FacilityName, " & _
                    "SO2, NOX, PMPRI, PMFIL, PM10PRI, PM25PRI, VOC, " & _
                    "CO, NH3, Lead " & _
                    "from " & _
                    "(select dt.strairsnumber, dt.strfacilityname, " & _
                    "sum(case when dt.strpollutantcode='SO2' then pollutanttotal else null end) SO2, " & _
                    "sum(case when dt.strpollutantcode='NOX' then pollutanttotal else null end) NOx, " & _
                    "sum(case when dt.strpollutantcode='PM-PRI' then pollutanttotal else null end) PMPRI, " & _
                    "sum(case when dt.strpollutantcode='PM-FIL' then pollutanttotal else null end) PMFIL, " & _
                    "sum(case when dt.strpollutantcode='PM10-PRI' then pollutanttotal else null end) PM10PRI, " & _
                    "sum(case when dt.strpollutantcode='PM25-PRI' then pollutanttotal else null end) PM25PRI, " & _
                    "sum(case when dt.strpollutantcode='VOC' then pollutanttotal else null end) VOC, " & _
                    "sum(case when dt.strpollutantcode='CO' then pollutanttotal else null end) CO, " & _
                    "sum(case when dt.strpollutantcode='NH3' then pollutanttotal else null end) NH3, " & _
                    "sum(case when dt.strpollutantcode='7439921' then pollutanttotal else null end) Lead " & _
                    "from " & _
                    "(Select dtSumPollutant.strairsnumber, eisi.strfacilityname, dtSumPollutant.strpollutantcode, " & _
                    "dtSumPollutant.PollutantTotal, dtSumPollutant.strinventoryyear " & _
                    "from airbranch.eisi, " & _
                    "(select eiem.strairsnumber, eiem.strpollutantcode, sum(eiem.dblemissionnumericvalue) as PollutantTotal, eiem.strinventoryyear " & _
                    "from " & DBNameSpace & ".eiem " & _
                    "where eiem.strinventoryyear='" & cboEIYear.Text & "' " & _
                    "group by eiem.strairsnumber, eiem.strpollutantcode, eiem.strinventoryyear) dtSumPollutant " & _
                    "where eisi.strairsnumber = dtSumPollutant.strairsnumber and " & _
                    "eisi.strinventoryyear = dtSumPollutant.strinventoryyear ) dt " & _
                    "group by dt.strairsnumber, dt.strfacilityname) " & _
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
                    SQL = "select " & _
                    " rownum as EIRows, " & _
                    "ViewList.FacilitySiteID as AIRSNumber, " & _
                    "strFacilityName as FacilityName, " & _
                    "SO2, NOX, VOC,  CO, NH3, LEAD, " & _
                    "PMFIL, PM10FIL, PM10PRI, PM25PRI, PMCON " & _
                    "from " & _
                    "(select distinct (FacilitySiteID ) as FacilitySiteID " & _
                    "from airbranch.VW_EIS_RPEMISSIONS " & _
                    "where intinventoryyear = '" & cboEIYear.Text & "')ViewList,   " & _
                    "(select facilitysiteid, sum(fltTotalemissions) as VOC    " & _
                    "from airbranch.VW_EIS_RPEMISSIONS " & _
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'VOC'  " & _
                    "group by facilitysiteid) VOCSum,  " & _
                    "(select facilitysiteid, sum(fltTotalemissions) as PMFIL " & _
                    "from airbranch.VW_EIS_RPEMISSIONS " & _
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'PM25-FIL'  " & _
                    "group by facilitysiteid) PM25FILSum,  " & _
                    "(select facilitysiteid, sum(fltTotalemissions) as LEAD    " & _
                    "from airbranch.VW_EIS_RPEMISSIONS " & _
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = '7439921'  " & _
                    "group by facilitysiteid) LEADSum,  " & _
                    "(select facilitysiteid, sum(fltTotalemissions) as PM10PRI    " & _
                    "from airbranch.VW_EIS_RPEMISSIONS " & _
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'PM10-PRI'  " & _
                    "group by facilitysiteid) PM10PRISum,  " & _
                    "(select facilitysiteid, sum(fltTotalemissions) as PM25PRI    " & _
                    "from airbranch.VW_EIS_RPEMISSIONS " & _
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'PM25-PRI'  " & _
                    "group by facilitysiteid) PM25PRISum,  " & _
                    "(select facilitysiteid, sum(fltTotalemissions) as PMCON    " & _
                    "from airbranch.VW_EIS_RPEMISSIONS " & _
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'PM-CON'  " & _
                    "group by facilitysiteid) PMCONSum,  " & _
                    "(select facilitysiteid, sum(fltTotalemissions) as PM10FIL    " & _
                    "from airbranch.VW_EIS_RPEMISSIONS " & _
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'PM10-FIL'  " & _
                    "group by facilitysiteid) PM10FILSum,  " & _
                    "(select facilitysiteid, sum(fltTotalemissions) as NH3    " & _
                    "from airbranch.VW_EIS_RPEMISSIONS " & _
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'NH3'  " & _
                    "group by facilitysiteid) NH3Sum,  " & _
                    "(select facilitysiteid, sum(fltTotalemissions) as SO2    " & _
                    "from airbranch.VW_EIS_RPEMISSIONS " & _
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'SO2'  " & _
                    "group by facilitysiteid) SO2Sum,  " & _
                    "(select facilitysiteid, sum(fltTotalemissions) as CO    " & _
                    "from airbranch.VW_EIS_RPEMISSIONS " & _
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'CO'  " & _
                    "group by facilitysiteid) COSum,  " & _
                    "(select facilitysiteid, sum(fltTotalemissions) as NOX    " & _
                    "from airbranch.VW_EIS_RPEMISSIONS " & _
                    "where intinventoryyear = '" & cboEIYear.Text & "' and pollutantcode = 'NOX'  " & _
                    "group by facilitysiteid)NOXSum,  " & _
                    "AIRBranch.APBFacilityInformation  " & _
                    "where '0413'||ViewList.facilitysiteid = APBFacilityInformation.strAIRSNumber " & _
                    "and ViewList.facilitysiteid = VOCSum.facilitysiteid (+)  " & _
                    "and ViewList.facilitysiteid  = PM25FILSum.facilitysiteid (+)  " & _
                    "and ViewList.facilitysiteid  = LEADSum.facilitysiteid (+)  " & _
                    "and ViewList.facilitysiteid  = PM10PRISum.facilitysiteid (+)  " & _
                    "and ViewList.facilitysiteid  = PM25PRISum.facilitysiteid (+)  " & _
                    "and ViewList.facilitysiteid  = PMCONSum.facilitysiteid (+)  " & _
                    "and ViewList.facilitysiteid  = PM10FILSum.facilitysiteid (+)  " & _
                    "and ViewList.facilitysiteid  = NH3Sum.facilitysiteid (+)  " & _
                    "and ViewList.facilitysiteid  = SO2Sum.facilitysiteid (+)  " & _
                    "and ViewList.facilitysiteid  = COSum.facilitysiteid (+)  " & _
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
            SQL = "select substr(strairsnumber,5,8) as AIRSNumber, strfacilityname as FacilityName, SO2, NOX, PMPRI, PMFIL, PM10PRI, PM25PRI, VOC, " & _
            "CO, NH3, Lead " & _
            "from " & _
            "(select dt.strairsnumber, dt.strfacilityname, " & _
            "sum(case when dt.strpollutantcode='SO2' then pollutanttotal else null end) SO2, " & _
            "sum(case when dt.strpollutantcode='NOX' then pollutanttotal else null end) NOx, " & _
            "sum(case when dt.strpollutantcode='PM-PRI' then pollutanttotal else null end) PMPRI, " & _
            "sum(case when dt.strpollutantcode='PM-FIL' then pollutanttotal else null end) PMFIL, " & _
            "sum(case when dt.strpollutantcode='PM10-PRI' then pollutanttotal else null end) PM10PRI, " & _
            "sum(case when dt.strpollutantcode='PM25-PRI' then pollutanttotal else null end) PM25PRI, " & _
            "sum(case when dt.strpollutantcode='VOC' then pollutanttotal else null end) VOC, " & _
            "sum(case when dt.strpollutantcode='CO' then pollutanttotal else null end) CO, " & _
            "sum(case when dt.strpollutantcode='NH3' then pollutanttotal else null end) NH3, " & _
            "sum(case when dt.strpollutantcode='7439921' then pollutanttotal else null end) Lead " & _
            "from " & _
            "(Select dtSumPollutant.strairsnumber, eisi.strfacilityname, dtSumPollutant.strpollutantcode, " & _
             "dtSumPollutant.PollutantTotal, dtSumPollutant.strinventoryyear " & _
            "from airbranch.eisi, " & _
            "(select eiem.strairsnumber, eiem.strpollutantcode, sum(eiem.dblemissionnumericvalue) as PollutantTotal, eiem.strinventoryyear " & _
            "from airbranch.eiem " & _
            "where eiem.strinventoryyear='2005' " & _
            "group by eiem.strairsnumber, eiem.strpollutantcode, eiem.strinventoryyear) dtSumPollutant " & _
            "where eisi.strairsnumber = dtSumPollutant.strairsnumber and " & _
            "eisi.strinventoryyear = dtSumPollutant.strinventoryyear ) dt " & _
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
                        SQL = "select " & _
                        "rownum as EIRow, " & _
                        "AIRSNumber, FacilityName, " & _
                        "Pollutant " & _
                        "from " & _
                        "(select " & _
                        "substr(dt.strairsnumber, 5) as AIRSNumber, " & _
                        "eisi.strfacilityname as FacilityName, " & _
                        "dt.pollutanttotal as Pollutant " & _
                        "from " & DBNameSpace & ".eisi," & _
                        "(select strairsnumber, strpollutantcode, " & _
                        "sum(dblemissionnumericvalue) as PollutantTotal, strinventoryyear " & _
                        "from " & DBNameSpace & ".eiem " & _
                        "where strinventoryyear = '" & cboEIYear.Text & "' and " & _
                        "strpollutantcode = '" & cboEIPollutants.SelectedValue & "' " & _
                        "group by eiem.strairsnumber, eiem.strinventoryyear, strpollutantcode) dt " & _
                        "where eisi.strairsnumber = dt.strairsnumber and " & _
                        "eisi.strinventoryyear = dt.strinventoryyear " & _
                        "order by AIRSNumber) "
                    Else
                        SQL = "select  " & _
                        "rownum as EIRow,  " & _
                        "ViewList.FacilitySiteID as AIRSNumber,  " & _
                        "strFacilityName as FacilityName,  " & _
                        "Pollutant " & _
                        "from  " & _
                        "(select distinct (FacilitySiteID ) as FacilitySiteID  " & _
                        "from airbranch.VW_EIS_RPEMISSIONS  " & _
                        "where intinventoryyear = '" & cboEIYear.Text & "')ViewList,    " & _
                        "(select facilitysiteid, sum(fltTotalemissions) as Pollutant     " & _
                        "from airbranch.VW_EIS_RPEMISSIONS  " & _
                        "where intinventoryyear = '" & cboEIYear.Text & "' " & _
                        "and pollutantcode = '" & cboEIPollutants.SelectedValue & "'   " & _
                        "group by facilitysiteid) PollutantSum, " & _
                        "AIRBranch.APBFacilityInformation   " & _
                        "where '0413'||ViewList.facilitysiteid = APBFacilityInformation.strAIRSNumber  " & _
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
        Try
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvEIResults.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvEIResults.ColumnCount - 1
                        .Cells(1, i + 1) = dgvEIResults.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvEIResults.ColumnCount - 1
                        For j = 0 To dgvEIResults.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvEIResults.Item(i, j).Value.ToString
                        Next
                    Next

                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class