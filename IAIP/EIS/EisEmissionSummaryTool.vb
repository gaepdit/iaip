Imports System.Data.SqlClient
Imports EpdIt.DBUtilities

Public Class EisEmissionSummaryTool
    Dim SQL As String

#Region " Form load "

    Private Sub SSCPEmissionSummaryTool_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadComboBoxes()
    End Sub

    Private Sub loadComboBoxes()
        SQL = "Select " &
            "distinct intESYear " &
            "from esschema " &
            "order by intESYear desc "

        With cboYear
            .DataSource = DB.GetDataTable(SQL)
            .DisplayMember = "intESYear"
            .ValueMember = "intESYear"
            .SelectedIndex = 0
        End With

        SQL = "SELECT inventoryyear AS EIYear
                FROM eis_admin
                UNION
                SELECT strInventoryYear
                FROM EISI
                ORDER BY EIYear DESC"

        With cboEIYear
            .DataSource = DB.GetDataTable(SQL)
            .DisplayMember = "EIYear"
            .ValueMember = "EIYear"
            .SelectedIndex = 0
        End With

        SQL = "Select " &
            "distinct(EIEM.strPollutantCode) As Pollutants,   " &
            "strPollutantDesc  " &
            "from EILookUpPollutantCodes, EIEM " &
            "where EIEM.strPollutantCode = EILookUpPollutantCodes.strPollutantCode " &
            "union " &
            "Select distinct (VW_EIS_RPEMISSIONS.PollutantCode), " &
            "EISLK_PollutantCode .strDesc " &
            "from VW_EIS_RPEMISSIONS, EISLK_PollutantCode " &
            "where VW_EIS_RPEMISSIONS.PollutantCode = EISLK_PollutantCode.PollutantCode "

        With cboEIPollutants
            .DataSource = DB.GetDataTable(SQL)
            .DisplayMember = "strPollutantDesc"
            .ValueMember = "Pollutants"
            .SelectedIndex = 0
        End With
    End Sub

#End Region

#Region " ES Tool "

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        runcount()
        lblYear.Text = cboYear.SelectedValue.ToString
    End Sub

    Private Sub runcount()
        txtESYear.Text = cboYear.SelectedValue.ToString
        Dim DeadlineYear As String = txtESYear.Text
        If DeadlineYear = "" Then DeadlineYear = "2007"
        Dim deadlineDate As New Date(CInt(DeadlineYear), 6, 15)

        Dim ESYearParam As New SqlParameter("@ESYear", DeadlineYear)
        Dim intEsYearParam As New SqlParameter("@intESyear", CInt(DeadlineYear))
        Dim params As SqlParameter() = {
            New SqlParameter("@intESyear", CInt(txtESYear.Text)),
            New SqlParameter("@deadline", deadlineDate)
        }

        Try
            SQL = "SELECT COUNT(*) FROM esmailout WHERE STRESYEAR = @ESYear "
            txtMailOutCount.Text = DB.GetString(SQL, ESYearParam)

            SQL = "SELECT COUNT(*)
                FROM   esmailout
                INNER JOIN ESSCHEMA
                  ON esmailout.STRAIRSYEAR = ESSCHEMA.STRAIRSYEAR
                WHERE  ESSCHEMA.STROPTOUT IS NOT NULL
                       AND esmailout.STRESYEAR = @ESYear"
            txtResponseCount.Text = DB.GetString(SQL, ESYearParam)

            SQL = "SELECT COUNT(*) FROM ESSchema
                    WHERE intESYEAR = @intESyear AND strOptOut = 'NO'"
            txtTotalOptInCount.Text = DB.GetString(SQL, intEsYearParam)

            SQL = "SELECT COUNT(*) FROM ESSchema
                    WHERE intESYEAR = @intESyear AND strOptOut = 'YES'"
            txtTotalOptOutCount.Text = DB.GetString(SQL, intEsYearParam)

            SQL = "SELECT COUNT(*)
                FROM   ESSchema
                WHERE  intESYEAR = @intESyear
                AND CAST(STRDATEFIRSTCONFIRM AS date) <= @deadline"
            txtTotalincompliance.Text = DB.GetString(SQL, params)

            SQL = "select count(*) as TotaloutofcomplianceCount 
            from ESSchema 
                WHERE  intESYEAR = @intESyear
                AND CAST(STRDATEFIRSTCONFIRM AS date) > @deadline"
            txtTotaloutofcompliance.Text = DB.GetString(SQL, params)

            SQL = "SELECT COUNT(*)
                FROM ESSchema
                RIGHT JOIN ESMailout ON ESMailout.STRAIRSYEAR = ESSchema.STRAIRSYEAR
                WHERE ESMailout.STRESYEAR = @ESYear AND ESSchema.STROPTOUT = 'NO'"
            txtMailoutOptin.Text = DB.GetString(SQL, ESYearParam)

            SQL = "SELECT COUNT(*)
                FROM ESSchema
                RIGHT JOIN ESMailout ON ESMailout.STRAIRSYEAR = ESSchema.STRAIRSYEAR
                WHERE ESMailout.STRESYEAR = @ESYear AND ESSchema.STROPTOUT = 'YES'"
            txtMailOutOptOut.Text = DB.GetString(SQL, ESYearParam)

            SQL = "select count(*) " &
                "from ESSCHEMA " &
                "where intESYEAR = @intESyear " &
                " and strOptOut is NULL"
            txtNonResponseCount.Text = DB.GetString(SQL, intEsYearParam)

            SQL = "SELECT COUNT(*) AS ExtraCount
                FROM (SELECT ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, ESMailout.STRAIRSYEAR AS MailoutAIRS
                FROM ESMailout
                RIGHT JOIN ESSCHEMA ON ESSCHEMA.STRAIRSYEAR = ESMailout.STRAIRSYEAR
                WHERE ESSCHEMA.INTESYEAR = @intESyear AND ESSCHEMA.STROPTOUT IS NOT NULL) AS dt_NotInMailout
                INNER JOIN ESSCHEMA ON dt_NotInMailout.SchemaAIRS = ESSCHEMA.STRAIRSYEAR
                WHERE dt_NotInMailout.MailoutAIRS IS NULL"
            txtextraResponse.Text = DB.GetString(SQL, intEsYearParam)

            SQL = "SELECT COUNT(*) AS ExtraOptinCount
                FROM (SELECT ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, ESMailout.STRAIRSYEAR AS MailoutAIRS
                FROM ESMailout
                RIGHT JOIN ESSCHEMA ON ESSCHEMA.STRAIRSYEAR = ESMailout.STRAIRSYEAR
                WHERE ESSCHEMA.INTESYEAR = @intESyear AND ESSCHEMA.STROPTOUT IS NOT NULL) AS dt_NotInMailout
                INNER JOIN ESSCHEMA ON dt_NotInMailout.SchemaAIRS = ESSCHEMA.STRAIRSYEAR
                WHERE dt_NotInMailout.MailoutAIRS IS NULL AND ESSCHEMA.STROPTOUT = 'NO'"
            txtExtraOptin.Text = DB.GetString(SQL, intEsYearParam)

            SQL = "SELECT COUNT(*) AS ExtraOptinCount
                FROM (SELECT ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, ESMailout.STRAIRSYEAR AS MailoutAIRS
                FROM ESMailout
                RIGHT JOIN ESSCHEMA ON ESSCHEMA.STRAIRSYEAR = ESMailout.STRAIRSYEAR
                WHERE ESSCHEMA.INTESYEAR = @intESyear AND ESSCHEMA.STROPTOUT IS NOT NULL) AS dt_NotInMailout
                INNER JOIN ESSCHEMA ON dt_NotInMailout.SchemaAIRS = ESSCHEMA.STRAIRSYEAR
                WHERE dt_NotInMailout.MailoutAIRS IS NULL AND ESSCHEMA.STROPTOUT = 'YES'"
            txtExtraOptout.Text = DB.GetString(SQL, intEsYearParam)

            SQL = "select count(*) as TotalResponsecount " &
            "from ESSchema " &
            "where ESSchema.intESYEAR = @intESyear " &
            " and ESSchema.strOptOut is not NULL"
            txtTotalResponse.Text = DB.GetString(SQL, intEsYearParam)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub findESData()
        Dim AirsNo As String = txtESAirsNo.Text
        Dim ESyear As String = cboYear.SelectedValue.ToString
        Dim intESyear As Integer = CInt(ESyear)

        SQL = "SELECT * " &
        "from esschema " &
        "where STRAIRSNUMBER = @AirsNo " &
        "and INTESYEAR = @intESyear "

        Dim param As SqlParameter() = {New SqlParameter("@AirsNo", AirsNo), New SqlParameter("@intESyear", intESyear)}

        Dim dr As DataRow = DB.GetDataRow(SQL, param)

        If dr IsNot Nothing Then
            txtESAirsNo.Text = GetNullableString(dr("STRAIRSNUMBER"))
            txtFACILITYNAME.Text = GetNullableString(dr("STRFACILITYNAME"))
            txtFACILITYADDRESS.Text = GetNullableString(dr("STRFACILITYADDRESS"))
            txtFACILITYCITY.Text = GetNullableString(dr("STRFACILITYCITY"))
            txtFACILITYSTATE.Text = GetNullableString(dr("STRFACILITYSTATE"))
            txtFACILITYZIP.Text = GetNullableString(dr("STRFACILITYZIP"))
            txtCOUNTY.Text = GetNullableString(dr("STRCOUNTY"))
            txtXCOORDINATE.Text = GetNullableString(dr("DBLXCOORDINATE"))
            txtYCOORDINATE.Text = GetNullableString(dr("DBLYCOORDINATE"))
            txtHORIZONTALCOLLECTIONCODE.Text = GetNullableString(dr("STRHORIZONTALCOLLECTIONCODE"))
            txtHORIZONTALACCURACYMEASURE.Text = GetNullableString(dr("STRHORIZONTALACCURACYMEASURE"))
            txtHORIZONTALREFERENCECODE.Text = GetNullableString(dr("STRHORIZONTALREFERENCECODE"))
            txtCompany.Text = GetNullableString(dr("STRCONTACTCOMPANY"))
            txtTitle.Text = GetNullableString(dr("STRCONTACTTITLE"))
            txtPhone.Text = GetNullableString(dr("STRCONTACTPHONENUMBER"))
            txtFax.Text = GetNullableString(dr("STRCONTACTFAXNUMBER"))
            txtContactFirstName.Text = GetNullableString(dr("STRCONTACTFIRSTNAME"))
            txtContactLastName.Text = GetNullableString(dr("STRCONTACTLASTNAME"))
            txtAddress1.Text = GetNullableString(dr("STRCONTACTADDRESS1"))
            txtAddress2.Text = GetNullableString(dr("STRCONTACTADDRESS2"))
            txtCity.Text = GetNullableString(dr("STRCONTACTCITY"))
            txtState.Text = GetNullableString(dr("STRCONTACTSTATE"))
            txtZip.Text = GetNullableString(dr("STRCONTACTZIP"))
            txtEmail.Text = GetNullableString(dr("STRCONTACTEMAIL"))
            txtVOCEmission.Text = GetNullableString(dr("DBLVOCEMISSION"))
            txtNOXEmission.Text = GetNullableString(dr("DBLNOXEMISSION"))
            txtConfirmationNbr.Text = GetNullableString(dr("STRCONFIRMATIONNBR"))
            txtConfirmationNumber.Text = GetNullableString(dr("STRCONFIRMATIONNBR"))
            txtFirstConfirmedDate.Text = GetNullableString(dr("STRDATEFIRSTCONFIRM"))
        End If
    End Sub

    Private Sub dgvESDataCount_MouseUp1(sender As Object, e As MouseEventArgs) Handles dgvESDataCount.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvESDataCount.HitTest(e.X, e.Y)

        Try
            If dgvESDataCount.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvESDataCount.Columns(0).HeaderText = "Airs No." Then
                    If Not IsDBNull(dgvESDataCount(0, hti.RowIndex).Value) Then
                        txtESAirsNo.Text = dgvESDataCount(0, hti.RowIndex).Value.ToString
                        If dgvESDataCount.Columns(3).HeaderText = "Confirmation Number" Then
                            If Not IsDBNull(dgvESDataCount(3, hti.RowIndex).Value) Then
                                txtConfirmationNumber.Text = dgvESDataCount(3, hti.RowIndex).Value.ToString
                                findESData()
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewMailOut_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewMailOut.LinkClicked
        txtESYear.Text = cboYear.SelectedValue.ToString

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
            "from esMailOut " &
            "where STRESYEAR = @year " &
            "order by STRFACILITYNAME"

            Dim dtViewCount As DataTable = DB.GetDataTable(SQL, New SqlParameter("@year", year))

            dgvESDataCount.DataSource = dtViewCount

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
            dgvESDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvESDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvESDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
            txtMailOutCount.Text = txtRecordNumber.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub lblViewOptin_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewTotalOptin.LinkClicked
        txtESYear.Text = cboYear.SelectedValue.ToString
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = CInt(year)

            SQL = "SELECT esSchema.STRAIRSNUMBER, esSchema.STRFACILITYNAME, esSchema.STRDATEFIRSTCONFIRM, esSchema.STRCONFIRMATIONNBR
                FROM esSchema
                LEFT JOIN esmailout ON esSchema.STRAIRSYEAR = esmailout.STRAIRSYEAR
                WHERE esSchema.INTESYEAR = @intYear AND esSchema.STROPTOUT = 'NO'
                ORDER BY esSchema.STRFACILITYNAME"

            Dim dtView As DataTable = DB.GetDataTable(SQL, New SqlParameter("@intYear", intYear))

            dgvESDataCount.DataSource = dtView

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"

            txtTotalOptInCount.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtTotalOptInCount.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewOptOut_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewTotalOptOut.LinkClicked
        txtESYear.Text = cboYear.SelectedValue.ToString
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = CInt(year)

            SQL = "SELECT esSchema.STRAIRSNUMBER, esSchema.STRFACILITYNAME, esSchema.STRDATEFIRSTCONFIRM, esSchema.STRCONFIRMATIONNBR
                FROM esSchema
                LEFT JOIN esmailout ON esSchema.STRAIRSYEAR = esmailout.STRAIRSYEAR
                WHERE esSchema.INTESYEAR = @intYear AND esSchema.STROPTOUT = 'YES'
                ORDER BY esSchema.STRFACILITYNAME"

            Dim dtView As DataTable = DB.GetDataTable(SQL, New SqlParameter("@intYear", intYear))

            dgvESDataCount.DataSource = dtView

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"

            txtTotalOptOutCount.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtTotalOptOutCount.Text

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewOutofcompliance_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewOutofcompliance.LinkClicked
        txtESYear.Text = cboYear.SelectedValue.ToString
        Try

            Dim DeadlineYear As String = txtESYear.Text
            If DeadlineYear = "" Then DeadlineYear = "2007"
            Dim deadlineDate As New Date(CInt(DeadlineYear), 6, 15)

            Dim params As SqlParameter() = {
                New SqlParameter("@intYear", CInt(DeadlineYear)),
                New SqlParameter("@deadline", deadlineDate)
            }

            SQL = "SELECT esSchema.STRAIRSNUMBER, " &
            "esSchema.STRFACILITYNAME, " &
            "esSchema.STROPTOUT, " &
            "esSchema.STRCONFIRMATIONNBR, " &
            "esSchema.STRCONTACTFIRSTNAME, " &
            "esSchema.STRCONTACTLASTNAME, " &
            "esSchema.STRCONTACTCOMPANY, " &
            "esSchema.STRCONTACTADDRESS1, " &
            "esSchema.STRCONTACTCITY, " &
            "esSchema.STRCONTACTSTATE, " &
            "esSchema.STRCONTACTZIP, " &
            "esSchema.STRCONTACTEMAIL, " &
            "esSchema.STRCONTACTPHONENUMBER " &
            "from esSchema " &
            "where intESyear = @intYear " &
            "and esSchema.STRDATEFIRSTCONFIRM is not NULL " &
            "and cast(esSchema.STRDATEFIRSTCONFIRM as date) > @deadline " &
            "order by esSchema.STRFACILITYNAME"

            Dim dtView As DataTable = DB.GetDataTable(SQL, params)

            dgvESDataCount.DataSource = dtView

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("STROPTOUT").HeaderText = "OptOut Status"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTCOMPANY").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Street Address"
            dgvESDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvESDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvESDataCount.Columns("STRCONTACTZIP").HeaderText = "Zip"
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").HeaderText = "Phone No."

            txtTotaloutofcompliance.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtTotaloutofcompliance.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewINCompliance_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewINCompliance.LinkClicked
        txtESYear.Text = cboYear.SelectedValue.ToString
        Try

            Dim DeadlineYear As String = txtESYear.Text
            If DeadlineYear = "" Then DeadlineYear = "2007"
            Dim deadlineDate As New Date(CInt(DeadlineYear), 6, 15)

            Dim params As SqlParameter() = {
                New SqlParameter("@intYear", CInt(DeadlineYear)),
                New SqlParameter("@deadline", deadlineDate)
            }

            SQL = "SELECT esSchema.STRAIRSNUMBER, " &
            "esSchema.STRFACILITYNAME, " &
            "esSchema.STRDATEFIRSTCONFIRM, " &
            "esSchema.STRCONTACTPHONENUMBER " &
            "from esSchema " &
            "where esSchema.intESyear = @intYear " &
             "and esSchema.STRDATEFIRSTCONFIRM is not NULL " &
            "and cast(esSchema.STRDATEFIRSTCONFIRM as date) <= @deadline " &
            "order by esSchema.STRFACILITYNAME"

            Dim dtView As DataTable = DB.GetDataTable(SQL, params)

            dgvESDataCount.DataSource = dtView

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "Date First Confirmed"
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").HeaderText = "Confirmation Number"

            txtTotalincompliance.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtTotalincompliance.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub

    Private Sub lblViewESData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewESData.LinkClicked
        Dim year As Integer = CInt(cboYear.SelectedValue)
        Try

            SQL = "SELECT STRAIRSNUMBER, " &
            "STRFACILITYNAME, " &
            "DBLVOCEMISSION, " &
            "STRCONFIRMATIONNBR, " &
            "DBLNOXEMISSION, " &
            "STRDATEFIRSTCONFIRM " &
            "from esSchema " &
            "where intESyear = @year " &
            "order by STRFACILITYNAME"

            Dim dtViewCount As DataTable = DB.GetDataTable(SQL, New SqlParameter("@year", year))

            dgvESDataCount.DataSource = dtViewCount

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("DBLVOCEMISSION").HeaderText = "VOC Emissions"
            dgvESDataCount.Columns("DBLNOXEMISSION").HeaderText = "NOX Emissions"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub lblViewNonResponse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewNonResponse.LinkClicked
        Try

            Dim year As String = cboYear.SelectedValue.ToString

            SQL = "SELECT ESSCHEMA.STRAIRSNUMBER, ESSCHEMA.STRFACILITYNAME
                FROM esMailOut
                LEFT JOIN ESSCHEMA ON esMailOut.STRAIRSYEAR = ESSCHEMA.STRAIRSYEAR
                WHERE ESSCHEMA.INTESYEAR = @year AND ESSCHEMA.STROPTOUT IS NULL
                ORDER BY esMailOut.STRFACILITYNAME"

            Dim dtViewCount As DataTable = DB.GetDataTable(SQL, New SqlParameter("@year", year))

            dgvESDataCount.DataSource = dtViewCount

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"

            txtNonResponseCount.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtNonResponseCount.Text
            clearESData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblextraResponse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblextraResponse.LinkClicked
        Try

            Dim year As String = txtESYear.Text
            Dim intyear As Integer = Int(year)

            SQL = "SELECT dt_NotInMailout.SchemaAIRS, ESSCHEMA.STRAIRSNUMBER, ESSCHEMA.STRFACILITYNAME, ESSCHEMA.STRCONTACTFIRSTNAME, ESSCHEMA.STRCONTACTLASTNAME, ESSCHEMA.STRCONTACTCOMPANY, ESSCHEMA.STRCONTACTEMAIL, ESSCHEMA.STRCONTACTPHONENUMBER
                    FROM (SELECT ESSCHEMA.STRAIRSNUMBER AS SchemaAIRS, ESMailout.STRAIRSNUMBER AS MailoutAIRS
                          FROM ESMailout
                          RIGHT JOIN ESSCHEMA ON ESSCHEMA.STRAIRSYEAR = ESMailout.STRAIRSYEAR
                          WHERE ESSCHEMA.INTESYEAR = @intyear AND ESSCHEMA.STROPTOUT IS NOT NULL) AS dt_NotInMailout
                    INNER JOIN ESSCHEMA ON dt_NotInMailout.SchemaAIRS = ESSCHEMA.STRAIRSNUMBER
                    WHERE dt_NotInMailout.MailoutAIRS IS NULL"

            Dim dtView As DataTable = DB.GetDataTable(SQL, New SqlParameter("@intYear", intyear))

            dgvESDataCount.DataSource = dtView

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTCOMPANY").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").HeaderText = "Phone No."

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
            txtextraResponse.Text = dgvESDataCount.RowCount.ToString

            clearESData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewOptIn_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewOptIn.LinkClicked
        txtESYear.Text = cboYear.SelectedValue.ToString
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = CInt(year)

            SQL = "SELECT esSchema.STRAIRSNUMBER, esSchema.STRFACILITYNAME, esSchema.STRDATEFIRSTCONFIRM, esSchema.STRCONFIRMATIONNBR
                FROM esSchema
                RIGHT JOIN esmailout ON esmailout.STRAIRSYEAR = esSchema.STRAIRSYEAR
                WHERE esSchema.STRDATEFIRSTCONFIRM IS NOT NULL AND esSchema.INTESYEAR = @intYear AND esSchema.STROPTOUT = 'NO'
                ORDER BY esSchema.STRFACILITYNAME"

            Dim dtView As DataTable = DB.GetDataTable(SQL, New SqlParameter("@intYear", intYear))

            dgvESDataCount.DataSource = dtView

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"

            txtMailoutOptin.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtMailoutOptin.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewOptOut_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewOptOut.LinkClicked
        txtESYear.Text = cboYear.SelectedValue.ToString
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = CInt(year)

            SQL = "SELECT esSchema.STRAIRSNUMBER, esSchema.STRFACILITYNAME, esSchema.STRDATEFIRSTCONFIRM, esSchema.STRCONFIRMATIONNBR
                FROM esSchema
                RIGHT JOIN esmailout ON esSchema.STRAIRSYEAR = esmailout.STRAIRSYEAR
                WHERE esSchema.STRDATEFIRSTCONFIRM IS NOT NULL AND esSchema.INTESYEAR = @intYear AND esSchema.STROPTOUT = 'YES'
                ORDER BY esSchema.STRFACILITYNAME"

            Dim dtView As DataTable = DB.GetDataTable(SQL, New SqlParameter("@intYear", intYear))

            dgvESDataCount.DataSource = dtView

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"

            txtMailOutOptOut.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtMailOutOptOut.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewExtraOptOut_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewExtraOptOut.LinkClicked
        txtESYear.Text = cboYear.SelectedValue.ToString
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = CInt(year)

            SQL = "SELECT ESSCHEMA.STRAIRSNUMBER, ESSCHEMA.STRFACILITYNAME, ESSCHEMA.STRDATEFIRSTCONFIRM, ESSCHEMA.STRCONFIRMATIONNBR
                FROM (SELECT ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, ESMailout.STRAIRSYEAR AS MailoutAIRS
                      FROM ESMailout
                      RIGHT JOIN ESSCHEMA ON ESMailout.STRAIRSYEAR = ESSCHEMA.STRAIRSYEAR
                      WHERE ESSCHEMA.INTESYEAR = @intYear AND ESSCHEMA.STROPTOUT IS NOT NULL) AS dt_NotInMailout
                INNER JOIN ESSCHEMA ON dt_NotInMailout.SchemaAIRS = ESSCHEMA.STRAIRSYEAR
                WHERE dt_NotInMailout.MailoutAIRS IS NULL AND ESSCHEMA.STROPTOUT = 'YES'"

            Dim dtView As DataTable = DB.GetDataTable(SQL, New SqlParameter("@intYear", intYear))

            dgvESDataCount.DataSource = dtView

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"

            txtExtraOptout.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtExtraOptout.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewExtraOptIn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewExtraOptIn.LinkClicked
        txtESYear.Text = cboYear.SelectedValue.ToString
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = CInt(year)

            SQL = "SELECT ESSCHEMA.STRAIRSNUMBER, ESSCHEMA.STRFACILITYNAME, ESSCHEMA.STRDATEFIRSTCONFIRM, ESSCHEMA.STRCONFIRMATIONNBR
                FROM (SELECT ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, ESMailout.STRAIRSYEAR AS MailoutAIRS
                      FROM ESMailout
                      RIGHT JOIN ESSCHEMA ON ESMailout.STRAIRSYEAR = ESSCHEMA.STRAIRSYEAR
                      WHERE ESSCHEMA.INTESYEAR = @intYear AND ESSCHEMA.STROPTOUT IS NOT NULL) AS dt_NotInMailout
                INNER JOIN ESSCHEMA ON dt_NotInMailout.SchemaAIRS = ESSCHEMA.STRAIRSYEAR
                WHERE dt_NotInMailout.MailoutAIRS IS NULL AND ESSCHEMA.STROPTOUT = 'NO'"

            Dim dtView As DataTable = DB.GetDataTable(SQL, New SqlParameter("@intYear", intYear))

            dgvESDataCount.DataSource = dtView

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"

            txtExtraOptin.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtExtraOptin.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub lblViewTotalResponse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewTotalResponse.LinkClicked
        txtESYear.Text = cboYear.SelectedValue.ToString
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = CInt(year)

            SQL = "SELECT esSchema.STRAIRSNUMBER, " &
            "esSchema.STRFACILITYNAME, " &
            "esSchema.STRCONTACTFIRSTNAME, " &
            "esSchema.STRCONTACTLASTNAME, " &
            "esSchema.STRCONTACTCOMPANY, " &
            "esSchema.STRCONTACTEMAIL, " &
            "esSchema.STRCONTACTPHONENUMBER " &
            "from esSchema " &
            "where esSchema.intESyear = @intYear " &
            "and esSchema.STROPTOUT is not NULL " &
            "order by esSchema.STRFACILITYNAME"

            Dim dtView As DataTable = DB.GetDataTable(SQL, New SqlParameter("@intYear", intYear))

            dgvESDataCount.DataSource = dtView

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTCOMPANY").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").HeaderText = "Phone Number"

            txtTotalResponse.Text = dgvESDataCount.RowCount.ToString
            txtRecordNumber.Text = txtTotalResponse.Text
            clearESData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub btnoutofcomplianceExport_Click(sender As Object, e As EventArgs) Handles btnoutofcomplianceExport.Click
        dgvESDataCount.ExportToExcel(Me)
    End Sub

#End Region

#Region " Form events "

    Private Sub SSCPEmissionSummaryTool_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Try
            If Me.Size.Width > 560 Then
                SplitContainer1.SanelySetSplitterDistance(556)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#End Region

#Region " EI Tool "

    Private Sub btnEISummary_Click(sender As Object, e As EventArgs) Handles btnEISummary.Click
        Try
            If cboEIYear.Text <> "" And cboEIYear.Text <> "-Select a Year-" Then
                If CInt(cboEIYear.Text) < 2010 Then
                    SQL = "SELECT AIRSNumber, FacilityName, SO2, NOX, VOC, CO, NH3, Lead, PMFIL, '' AS PM10FIL, PMPRI, PM10PRI, PM25PRI, '' AS PMCON
                        FROM (SELECT SUBSTRING(strairsnumber, 5, 8) AS AIRSNumber, strfacilityname AS FacilityName, SO2, NOX, PMPRI, PMFIL, PM10PRI, PM25PRI, VOC, CO, NH3, Lead
                        FROM (SELECT dt.strairsnumber, dt.strfacilityname, SUM(CASE WHEN dt.strpollutantcode = 'SO2' THEN pollutanttotal ELSE NULL END) AS SO2, SUM(CASE WHEN dt.strpollutantcode = 'NOX' THEN pollutanttotal ELSE NULL END) AS NOx, SUM(CASE WHEN dt.strpollutantcode = 'PM-PRI' THEN pollutanttotal ELSE NULL END) AS PMPRI, SUM(CASE WHEN dt.strpollutantcode = 'PM-FIL' THEN pollutanttotal ELSE NULL END) AS PMFIL, SUM(CASE WHEN dt.strpollutantcode = 'PM10-PRI' THEN pollutanttotal ELSE NULL END) AS PM10PRI, SUM(CASE WHEN dt.strpollutantcode = 'PM25-PRI' THEN pollutanttotal ELSE NULL END) AS PM25PRI, SUM(CASE WHEN dt.strpollutantcode = 'VOC' THEN pollutanttotal ELSE NULL END) AS VOC, SUM(CASE WHEN dt.strpollutantcode = 'CO' THEN pollutanttotal ELSE NULL END) AS CO, SUM(CASE WHEN dt.strpollutantcode = 'NH3' THEN pollutanttotal ELSE NULL END) AS NH3, SUM(CASE WHEN dt.strpollutantcode = '7439921' THEN pollutanttotal ELSE NULL END) AS Lead
                        FROM (SELECT dtSumPollutant.strairsnumber, eisi.strfacilityname, dtSumPollutant.strpollutantcode, dtSumPollutant.PollutantTotal, dtSumPollutant.strinventoryyear
                        FROM eisi, (SELECT eiem.strairsnumber, eiem.strpollutantcode, SUM(eiem.dblemissionnumericvalue) AS PollutantTotal, eiem.strinventoryyear
                        FROM eiem
                        WHERE eiem.strinventoryyear = @year
                        GROUP BY eiem.strairsnumber, eiem.strpollutantcode, eiem.strinventoryyear) AS dtSumPollutant
                        WHERE eisi.strairsnumber = dtSumPollutant.strairsnumber AND eisi.strinventoryyear = dtSumPollutant.strinventoryyear) AS dt
                        GROUP BY dt.strairsnumber, dt.strfacilityname) AS t1) AS t2 order by AIRSNumber"
                    Dim param As New SqlParameter("@year", cboEIYear.Text)

                    dgvEIResults.DataSource = DB.GetDataTable(SQL, param)

                    dgvEIResults.RowHeadersVisible = False
                    dgvEIResults.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvEIResults.AllowUserToResizeColumns = True
                    dgvEIResults.AllowUserToAddRows = False
                    dgvEIResults.AllowUserToDeleteRows = False
                    dgvEIResults.AllowUserToOrderColumns = True
                    dgvEIResults.AllowUserToResizeRows = True

                    dgvEIResults.Columns("AIRSNumber").HeaderText = "Airs No."
                    dgvEIResults.Columns("AIRSNumber").Width = 75
                    dgvEIResults.Columns("FacilityName").HeaderText = "Facility Name"
                    dgvEIResults.Columns("FacilityName").Width = 225
                    dgvEIResults.Columns("SO2").HeaderText = "Sulfur Dioxide"
                    dgvEIResults.Columns("NOX").HeaderText = "Nitrogen Oxides"
                    dgvEIResults.Columns("VOC").HeaderText = "Volatile Organic Compounds"
                    dgvEIResults.Columns("CO").HeaderText = "Carbon Monoxide"
                    dgvEIResults.Columns("NH3").HeaderText = "Ammonia "
                    dgvEIResults.Columns("Lead").HeaderText = "Lead"
                    dgvEIResults.Columns("PMPRI").HeaderText = "PM Primary - old EI"
                    dgvEIResults.Columns("PM10PRI").HeaderText = "Primary PM10 (Includes Filterables + Condensibles)"
                    dgvEIResults.Columns("PM25PRI").HeaderText = "Primary PM 2.5 (Includes Filterables + Condensibles)"
                    dgvEIResults.Columns("PMFIL").HeaderText = "Filterable PM 2.5"

                Else
                    SQL = "select FACILITYSITEID as AIRSNumber,
                           STRFACILITYNAME as FacilityName,
                           SO2,
                           NOX,
                           VOC,
                           CO,
                           NH3,
                           Lead,
                           [PM-CON] as PMCON,
                           [PM10-PRI] as PM10PRI,
                           [PM10-FIL] as PM10FIL,
                           [PM25-PRI] as PM25PRI,
                           [PM25-FIL] as PMFIL
                    from (
                             SELECT FACILITYSITEID, f.STRFACILITYNAME, POLLUTANTCODE, SUM(FLTTOTALEMISSIONS) AS Pollutant
                             FROM VW_EIS_RPEMISSIONS e
                                  inner join APBFACILITYINFORMATION f
                                          on right(f.STRAIRSNUMBER, 8) = e.FACILITYSITEID
                             WHERE INTINVENTORYYEAR = @year
                               and RPTPERIODTYPECODE = 'A'
                             GROUP BY FACILITYSITEID, f.STRFACILITYNAME, POLLUTANTCODE
                         ) t pivot (sum(Pollutant) for POLLUTANTCODE
                                 in (SO2, NOX, VOC, CO, NH3, Lead, [PM-CON], [PM10-PRI], [PM10-FIL], [PM25-PRI], [PM25-FIL])
                                 ) p
                    order by FACILITYSITEID"

                    Dim param As New SqlParameter("@year", cboEIYear.Text)

                    dgvEIResults.DataSource = DB.GetDataTable(SQL, param)

                    dgvEIResults.RowHeadersVisible = False
                    dgvEIResults.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvEIResults.AllowUserToResizeColumns = True
                    dgvEIResults.AllowUserToAddRows = False
                    dgvEIResults.AllowUserToDeleteRows = False
                    dgvEIResults.AllowUserToOrderColumns = True
                    dgvEIResults.AllowUserToResizeRows = True

                    dgvEIResults.Columns("AIRSNumber").HeaderText = "Airs No."
                    dgvEIResults.Columns("AIRSNumber").Width = 75
                    dgvEIResults.Columns("FacilityName").HeaderText = "Facility Name"
                    dgvEIResults.Columns("FacilityName").Width = 225
                    dgvEIResults.Columns("SO2").HeaderText = "Sulfur Dioxide"
                    dgvEIResults.Columns("NOX").HeaderText = "Nitrogen Oxides"
                    dgvEIResults.Columns("VOC").HeaderText = "Volatile Organic Compounds"
                    dgvEIResults.Columns("CO").HeaderText = "Carbon Monoxide"
                    dgvEIResults.Columns("NH3").HeaderText = "Ammonia "
                    dgvEIResults.Columns("Lead").HeaderText = "Lead"
                    dgvEIResults.Columns("PMCON").HeaderText = "Condensible PM"
                    dgvEIResults.Columns("PM10PRI").HeaderText = "Primary PM10 (Includes Filterables + Condensibles)"
                    dgvEIResults.Columns("PM10FIL").HeaderText = "Filterable PM10"
                    dgvEIResults.Columns("PM25PRI").HeaderText = "Primary PM 2.5 (Includes Filterables + Condensibles)"
                    dgvEIResults.Columns("PMFIL").HeaderText = "Filterable PM 2.5"
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewEISummaryByPollutant_Click(sender As Object, e As EventArgs) Handles btnViewEISummaryByPollutant.Click
        Try

            If cboEIYear.Text <> "" And cboEIYear.Text <> "-Select a Year-" AndAlso cboEIPollutants.Text <> "" Then
                If CInt(cboEIYear.Text) < 2010 Then
                    SQL = "SELECT right(eiem.STRAIRSNUMBER, 8) as AIRSNumber,
                               eisi.STRFACILITYNAME AS FacilityName,
                               SUM(eiem.DBLEMISSIONNUMERICVALUE) AS Pollutant
                        FROM eiem
                             inner join eisi
                                     on eiem.STRAIRSNUMBER = eisi.STRAIRSNUMBER
                                            AND eiem.STRINVENTORYYEAR = eisi.STRINVENTORYYEAR
                        WHERE eiem.STRINVENTORYYEAR = @year
                          AND eiem.STRPOLLUTANTCODE = @poll
                        GROUP BY eiem.STRAIRSNUMBER, eisi.STRFACILITYNAME
                        order by eiem.STRAIRSNUMBER"
                Else
                    SQL = "SELECT FACILITYSITEID as AIRSNumber, f.STRFACILITYNAME AS FacilityName, SUM(FLTTOTALEMISSIONS) AS Pollutant
                        FROM VW_EIS_RPEMISSIONS e
                             inner join APBFACILITYINFORMATION f
                                     on right(f.STRAIRSNUMBER, 8) = e.FACILITYSITEID
                        WHERE INTINVENTORYYEAR = @year
                          AND POLLUTANTCODE = @poll
                          and RPTPERIODTYPECODE = 'A'
                        GROUP BY FACILITYSITEID, f.STRFACILITYNAME, POLLUTANTCODE
                        order by FACILITYSITEID "
                End If

                Dim params As SqlParameter() = {
                    New SqlParameter("@year", cboEIYear.Text),
                    New SqlParameter("@poll", cboEIPollutants.SelectedValue)
                }

                dgvEIResults.DataSource = DB.GetDataTable(SQL, params)

                dgvEIResults.RowHeadersVisible = False
                dgvEIResults.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvEIResults.AllowUserToResizeColumns = True
                dgvEIResults.AllowUserToAddRows = False
                dgvEIResults.AllowUserToDeleteRows = False
                dgvEIResults.AllowUserToOrderColumns = True
                dgvEIResults.AllowUserToResizeRows = True

                dgvEIResults.Columns("AIRSNumber").HeaderText = "Airs No."
                dgvEIResults.Columns("AIRSNumber").Width = 75
                dgvEIResults.Columns("FacilityName").HeaderText = "Facility Name"
                dgvEIResults.Columns("FacilityName").Width = 225
                dgvEIResults.Columns("Pollutant").HeaderText = cboEIPollutants.Text

                txtEICount.Text = dgvEIResults.RowCount.ToString
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnExportEItoExcel_Click(sender As Object, e As EventArgs) Handles btnExportEItoExcel.Click
        dgvEIResults.ExportToExcel(Me)
    End Sub

#End Region

End Class