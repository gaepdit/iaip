Imports System.Data.SqlClient
Imports EpdIt

Public Class SSCPEmissionSummaryTool
    Dim SQL As String

#Region " Form load "

    Private Sub SSCPEmissionSummaryTool_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadYear()
        loadEIPollutant()
    End Sub

    Private Sub loadYear()
        SQL = "Select " &
        "distinct intESYear " &
        "from esschema " &
        "order by intESYear desc "

        Dim dt As DataTable = DB.GetDataTable(SQL)
        If dt IsNot Nothing Then
            For Each dr As DataRow In dt.Rows
                cboYear.Items.Add(DBUtilities.GetNullable(Of Integer)(dr("intESYear")))
            Next
        End If

        cboYear.SelectedIndex = 0

        cboEIYear.Items.Add("-Select a Year-")

        SQL = "SELECT inventoryyear AS EIYear
                FROM eis_admin
                UNION
                SELECT strInventoryYear
                FROM EISI
                ORDER BY EIYear DESC"

        dt = DB.GetDataTable(SQL)
        If dt IsNot Nothing Then
            For Each dr As DataRow In dt.Rows
                cboEIYear.Items.Add(dr.Item("EIYear"))
            Next
        End If

        cboEIYear.SelectedIndex = 0
    End Sub

    Private Sub loadEIPollutant()
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
        Dim dtEIPollutant As DataTable = DB.GetDataTable(SQL)

        With cboEIPollutants
            .DataSource = dtEIPollutant
            .DisplayMember = "strPollutantDesc"
            .ValueMember = "Pollutants"
            .SelectedIndex = 0
        End With
    End Sub

#End Region

#Region " ES Tool "

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        runcount()
        lblYear.Text = cboYear.SelectedItem
    End Sub

    Private Sub runcount()
        txtESYear.Text = cboYear.SelectedItem
        Dim DeadlineYear As String = txtESYear.Text
        If DeadlineYear = "" Then DeadlineYear = "2007"
        Dim deadlineDate As New Date(DeadlineYear, 6, 15)

        Dim ESYearParam As New SqlParameter("@ESYear", DeadlineYear)
        Dim intEsYearParam As New SqlParameter("@intESyear", CInt(DeadlineYear))
        Dim params As SqlParameter() = {
            New SqlParameter("@intESyear", CInt(txtESYear.Text)),
            New SqlParameter("@deadline", deadlineDate)
        }

        Try
            SQL = "SELECT COUNT(*) FROM esmailout WHERE STRESYEAR = @ESYear "
            txtMailOutCount.Text = DB.GetSingleValue(Of String)(SQL, ESYearParam)

            SQL = "SELECT COUNT(*)
                FROM   esmailout
                INNER JOIN ESSCHEMA
                  ON esmailout.STRAIRSYEAR = ESSCHEMA.STRAIRSYEAR
                WHERE  ESSCHEMA.STROPTOUT IS NOT NULL
                       AND esmailout.STRESYEAR = @ESYear"
            txtResponseCount.Text = DB.GetSingleValue(Of String)(SQL, ESYearParam)

            SQL = "SELECT COUNT(*) FROM ESSchema
                    WHERE intESYEAR = @intESyear AND strOptOut = 'NO'"
            txtTotalOptInCount.Text = DB.GetSingleValue(Of String)(SQL, intEsYearParam)

            SQL = "SELECT COUNT(*) FROM ESSchema
                    WHERE intESYEAR = @intESyear AND strOptOut = 'YES'"
            txtTotalOptOutCount.Text = DB.GetSingleValue(Of String)(SQL, intEsYearParam)

            SQL = "SELECT COUNT(*)
                FROM   ESSchema
                WHERE  intESYEAR = @intESyear
                AND CAST(STRDATEFIRSTCONFIRM AS date) <= @deadline"
            txtTotalincompliance.Text = DB.GetSingleValue(Of String)(SQL, params)

            SQL = "select count(*) as TotaloutofcomplianceCount 
            from ESSchema 
                WHERE  intESYEAR = @intESyear
                AND CAST(STRDATEFIRSTCONFIRM AS date) > @deadline"
            txtTotaloutofcompliance.Text = DB.GetSingleValue(Of String)(SQL, params)

            SQL = "SELECT COUNT(*)
                FROM ESSchema
                RIGHT JOIN ESMailout ON ESMailout.STRAIRSYEAR = ESSchema.STRAIRSYEAR
                WHERE ESMailout.STRESYEAR = @ESYear AND ESSchema.STROPTOUT = 'NO'"
            txtMailoutOptin.Text = DB.GetSingleValue(Of String)(SQL, ESYearParam)

            SQL = "SELECT COUNT(*)
                FROM ESSchema
                RIGHT JOIN ESMailout ON ESMailout.STRAIRSYEAR = ESSchema.STRAIRSYEAR
                WHERE ESMailout.STRESYEAR = @ESYear AND ESSchema.STROPTOUT = 'YES'"
            txtMailOutOptOut.Text = DB.GetSingleValue(Of String)(SQL, ESYearParam)

            SQL = "select count(*) " &
                "from ESSCHEMA " &
                "where intESYEAR = @intESyear " &
                " and strOptOut is NULL"
            txtNonResponseCount.Text = DB.GetSingleValue(Of String)(SQL, intEsYearParam)

            SQL = "SELECT COUNT(*) AS ExtraCount
                FROM (SELECT ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, ESMailout.STRAIRSYEAR AS MailoutAIRS
                FROM ESMailout
                RIGHT JOIN ESSCHEMA ON ESSCHEMA.STRAIRSYEAR = ESMailout.STRAIRSYEAR
                WHERE ESSCHEMA.INTESYEAR = @intESyear AND ESSCHEMA.STROPTOUT IS NOT NULL) AS dt_NotInMailout
                INNER JOIN ESSCHEMA ON dt_NotInMailout.SchemaAIRS = ESSCHEMA.STRAIRSYEAR
                WHERE dt_NotInMailout.MailoutAIRS IS NULL"
            txtextraResponse.Text = DB.GetSingleValue(Of String)(SQL, intEsYearParam)

            SQL = "SELECT COUNT(*) AS ExtraOptinCount
                FROM (SELECT ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, ESMailout.STRAIRSYEAR AS MailoutAIRS
                FROM ESMailout
                RIGHT JOIN ESSCHEMA ON ESSCHEMA.STRAIRSYEAR = ESMailout.STRAIRSYEAR
                WHERE ESSCHEMA.INTESYEAR = @intESyear AND ESSCHEMA.STROPTOUT IS NOT NULL) AS dt_NotInMailout
                INNER JOIN ESSCHEMA ON dt_NotInMailout.SchemaAIRS = ESSCHEMA.STRAIRSYEAR
                WHERE dt_NotInMailout.MailoutAIRS IS NULL AND ESSCHEMA.STROPTOUT = 'NO'"
            txtExtraOptin.Text = DB.GetSingleValue(Of String)(SQL, intEsYearParam)

            SQL = "SELECT COUNT(*) AS ExtraOptinCount
                FROM (SELECT ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, ESMailout.STRAIRSYEAR AS MailoutAIRS
                FROM ESMailout
                RIGHT JOIN ESSCHEMA ON ESSCHEMA.STRAIRSYEAR = ESMailout.STRAIRSYEAR
                WHERE ESSCHEMA.INTESYEAR = @intESyear AND ESSCHEMA.STROPTOUT IS NOT NULL) AS dt_NotInMailout
                INNER JOIN ESSCHEMA ON dt_NotInMailout.SchemaAIRS = ESSCHEMA.STRAIRSYEAR
                WHERE dt_NotInMailout.MailoutAIRS IS NULL AND ESSCHEMA.STROPTOUT = 'YES'"
            txtExtraOptout.Text = DB.GetSingleValue(Of String)(SQL, intEsYearParam)

            SQL = "select count(*) as TotalResponsecount " &
            "from ESSchema " &
            "where ESSchema.intESYEAR = @intESyear " &
            " and ESSchema.strOptOut is not NULL"
            txtTotalResponse.Text = DB.GetSingleValue(Of String)(SQL, intEsYearParam)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub findESData()
        Dim AirsNo As String = txtESAirsNo.Text
        Dim ESyear As String = cboYear.SelectedItem
        Dim intESyear As Integer = CInt(ESyear)

        SQL = "SELECT * " &
        "from esschema " &
        "where STRAIRSNUMBER = @AirsNo " &
        "and INTESYEAR = @intESyear "

        Dim param As SqlParameter() = {New SqlParameter("@AirsNo", AirsNo), New SqlParameter("@intESyear", intESyear)}

        Dim dr As DataRow = DB.GetDataRow(SQL, param)
        If dr IsNot Nothing Then

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
        End If
    End Sub

    Private Sub dgvESDataCount_MouseUp1(sender As Object, e As MouseEventArgs) Handles dgvESDataCount.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvESDataCount.HitTest(e.X, e.Y)

        Try
            If dgvESDataCount.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvESDataCount.Columns(0).HeaderText = "Airs No." Then
                    If Not IsDBNull(dgvESDataCount(0, hti.RowIndex).Value) Then
                        txtESAirsNo.Text = dgvESDataCount(0, hti.RowIndex).Value
                        If dgvESDataCount.Columns(3).HeaderText = "Confirmation Number" Then
                            If Not IsDBNull(dgvESDataCount(3, hti.RowIndex).Value) Then
                                txtConfirmationNumber.Text = dgvESDataCount(3, hti.RowIndex).Value
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub lblViewOptin_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewTotalOptin.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewOptOut_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewTotalOptOut.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewOutofcompliance_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewOutofcompliance.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim DeadlineYear As String = txtESYear.Text
            If DeadlineYear = "" Then DeadlineYear = "2007"
            Dim deadlineDate As New Date(DeadlineYear, 6, 15)

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewINCompliance_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewINCompliance.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim DeadlineYear As String = txtESYear.Text
            If DeadlineYear = "" Then DeadlineYear = "2007"
            Dim deadlineDate As New Date(DeadlineYear, 6, 15)

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try
    End Sub

    Private Sub lblViewESData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewESData.LinkClicked
        Dim year As Integer = CInt(cboYear.SelectedItem)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub lblViewNonResponse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewNonResponse.LinkClicked
        Try

            Dim year As String = cboYear.SelectedItem

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
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewOptIn_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewOptIn.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewOptOut_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewOptOut.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewExtraOptOut_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewExtraOptOut.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewExtraOptIn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewExtraOptIn.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub lblViewTotalResponse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewTotalResponse.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

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
            If cboEIYear.Text <> "" Then
                If cboEIYear.Text < 2010 Then
                    SQL = "SELECT ROW_NUMBER() OVER(ORDER BY AIRSNumber) AS EIRows, AIRSNumber, FacilityName, SO2, NOX, VOC, CO, NH3, Lead, PMFIL, '' AS PM10FIL, PMPRI, PM10PRI, PM25PRI, '' AS PMCON
                        FROM (SELECT SUBSTRING(strairsnumber, 5, 8) AS AIRSNumber, strfacilityname AS FacilityName, SO2, NOX, PMPRI, PMFIL, PM10PRI, PM25PRI, VOC, CO, NH3, Lead
                        FROM (SELECT dt.strairsnumber, dt.strfacilityname, SUM(CASE WHEN dt.strpollutantcode = 'SO2' THEN pollutanttotal ELSE NULL END) AS SO2, SUM(CASE WHEN dt.strpollutantcode = 'NOX' THEN pollutanttotal ELSE NULL END) AS NOx, SUM(CASE WHEN dt.strpollutantcode = 'PM-PRI' THEN pollutanttotal ELSE NULL END) AS PMPRI, SUM(CASE WHEN dt.strpollutantcode = 'PM-FIL' THEN pollutanttotal ELSE NULL END) AS PMFIL, SUM(CASE WHEN dt.strpollutantcode = 'PM10-PRI' THEN pollutanttotal ELSE NULL END) AS PM10PRI, SUM(CASE WHEN dt.strpollutantcode = 'PM25-PRI' THEN pollutanttotal ELSE NULL END) AS PM25PRI, SUM(CASE WHEN dt.strpollutantcode = 'VOC' THEN pollutanttotal ELSE NULL END) AS VOC, SUM(CASE WHEN dt.strpollutantcode = 'CO' THEN pollutanttotal ELSE NULL END) AS CO, SUM(CASE WHEN dt.strpollutantcode = 'NH3' THEN pollutanttotal ELSE NULL END) AS NH3, SUM(CASE WHEN dt.strpollutantcode = '7439921' THEN pollutanttotal ELSE NULL END) AS Lead
                        FROM (SELECT dtSumPollutant.strairsnumber, eisi.strfacilityname, dtSumPollutant.strpollutantcode, dtSumPollutant.PollutantTotal, dtSumPollutant.strinventoryyear
                        FROM eisi, (SELECT eiem.strairsnumber, eiem.strpollutantcode, SUM(eiem.dblemissionnumericvalue) AS PollutantTotal, eiem.strinventoryyear
                        FROM eiem
                        WHERE eiem.strinventoryyear = @year
                        GROUP BY eiem.strairsnumber, eiem.strpollutantcode, eiem.strinventoryyear) AS dtSumPollutant
                        WHERE eisi.strairsnumber = dtSumPollutant.strairsnumber AND eisi.strinventoryyear = dtSumPollutant.strinventoryyear) AS dt
                        GROUP BY dt.strairsnumber, dt.strfacilityname) AS t1) AS t2"
                    Dim param As New SqlParameter("@year", cboEIYear.Text)

                    dgvEIResults.DataSource = DB.GetDataTable(SQL, param)

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
                    dgvEIResults.Columns("PM25PRI").HeaderText = "Primary PM 2.5 (Includes Filterables + Condensibles)"
                    dgvEIResults.Columns("PM25PRI").DisplayIndex = 11
                    dgvEIResults.Columns("PMFIL").HeaderText = "Filterable PM 2.5"
                    dgvEIResults.Columns("PMFIL").DisplayIndex = 12

                Else
                    SQL = "SELECT ROW_NUMBER() OVER(ORDER BY ViewList.FacilitySiteID) AS EIRows, ViewList.FacilitySiteID AS AIRSNumber, APBFacilityInformation.STRFACILITYNAME AS FacilityName, SO2Sum.SO2, NOXSum.NOX, VOCSum.VOC, COSum.CO, NH3Sum.NH3, LEADSum.LEAD, PM25FILSum.PMFIL, PM10FILSum.PM10FIL, PM10PRISum.PM10PRI, PM25PRISum.PM25PRI, PMCONSum.PMCON
                        FROM (SELECT DISTINCT
                        VW_EIS_RPEMISSIONS.FACILITYSITEID AS FacilitySiteID
                        FROM VW_EIS_RPEMISSIONS
                        WHERE VW_EIS_RPEMISSIONS.INTINVENTORYYEAR = @year) AS ViewList
                        LEFT JOIN (SELECT VW_EIS_RPEMISSIONS.FACILITYSITEID, SUM(VW_EIS_RPEMISSIONS.FLTTOTALEMISSIONS) AS NOX
                        FROM VW_EIS_RPEMISSIONS
                        WHERE VW_EIS_RPEMISSIONS.INTINVENTORYYEAR = @year AND VW_EIS_RPEMISSIONS.POLLUTANTCODE = 'NOX'
                        GROUP BY VW_EIS_RPEMISSIONS.FACILITYSITEID) AS NOXSum ON ViewList.FacilitySiteID = NOXSum.FACILITYSITEID AND ViewList.FacilitySiteID = NOXSum.NOX
                        LEFT JOIN (SELECT VW_EIS_RPEMISSIONS.FACILITYSITEID, SUM(VW_EIS_RPEMISSIONS.FLTTOTALEMISSIONS) AS SO2
                        FROM VW_EIS_RPEMISSIONS
                        WHERE VW_EIS_RPEMISSIONS.INTINVENTORYYEAR = @year AND VW_EIS_RPEMISSIONS.POLLUTANTCODE = 'SO2'
                        GROUP BY VW_EIS_RPEMISSIONS.FACILITYSITEID) AS SO2Sum ON ViewList.FacilitySiteID = SO2Sum.FACILITYSITEID
                        LEFT JOIN (SELECT VW_EIS_RPEMISSIONS.FACILITYSITEID, SUM(VW_EIS_RPEMISSIONS.FLTTOTALEMISSIONS) AS PM10FIL
                        FROM VW_EIS_RPEMISSIONS
                        WHERE VW_EIS_RPEMISSIONS.INTINVENTORYYEAR = @year AND VW_EIS_RPEMISSIONS.POLLUTANTCODE = 'PM10-FIL'
                        GROUP BY VW_EIS_RPEMISSIONS.FACILITYSITEID) AS PM10FILSum ON ViewList.FacilitySiteID = PM10FILSum.FACILITYSITEID
                        LEFT JOIN (SELECT VW_EIS_RPEMISSIONS.FACILITYSITEID, SUM(VW_EIS_RPEMISSIONS.FLTTOTALEMISSIONS) AS PM25PRI
                        FROM VW_EIS_RPEMISSIONS
                        WHERE VW_EIS_RPEMISSIONS.INTINVENTORYYEAR = @year AND VW_EIS_RPEMISSIONS.POLLUTANTCODE = 'PM25-PRI'
                        GROUP BY VW_EIS_RPEMISSIONS.FACILITYSITEID) AS PM25PRISum ON ViewList.FacilitySiteID = PM25PRISum.FACILITYSITEID
                        LEFT JOIN (SELECT VW_EIS_RPEMISSIONS.FACILITYSITEID, SUM(VW_EIS_RPEMISSIONS.FLTTOTALEMISSIONS) AS LEAD
                        FROM VW_EIS_RPEMISSIONS
                        WHERE VW_EIS_RPEMISSIONS.INTINVENTORYYEAR = @year AND VW_EIS_RPEMISSIONS.POLLUTANTCODE = '7439921'
                        GROUP BY VW_EIS_RPEMISSIONS.FACILITYSITEID) AS LEADSum ON ViewList.FacilitySiteID = LEADSum.FACILITYSITEID
                        LEFT JOIN (SELECT VW_EIS_RPEMISSIONS.FACILITYSITEID, SUM(VW_EIS_RPEMISSIONS.FLTTOTALEMISSIONS) AS VOC
                        FROM VW_EIS_RPEMISSIONS
                        WHERE VW_EIS_RPEMISSIONS.INTINVENTORYYEAR = @year AND VW_EIS_RPEMISSIONS.POLLUTANTCODE = 'VOC'
                        GROUP BY VW_EIS_RPEMISSIONS.FACILITYSITEID) AS VOCSum ON ViewList.FacilitySiteID = VOCSum.FACILITYSITEID
                        LEFT JOIN (SELECT VW_EIS_RPEMISSIONS.FACILITYSITEID, SUM(VW_EIS_RPEMISSIONS.FLTTOTALEMISSIONS) AS PMFIL
                        FROM VW_EIS_RPEMISSIONS
                        WHERE VW_EIS_RPEMISSIONS.INTINVENTORYYEAR = @year AND VW_EIS_RPEMISSIONS.POLLUTANTCODE = 'PM25-FIL'
                        GROUP BY VW_EIS_RPEMISSIONS.FACILITYSITEID) AS PM25FILSum ON ViewList.FacilitySiteID = PM25FILSum.FACILITYSITEID
                        LEFT JOIN (SELECT VW_EIS_RPEMISSIONS.FACILITYSITEID, SUM(VW_EIS_RPEMISSIONS.FLTTOTALEMISSIONS) AS PM10PRI
                        FROM VW_EIS_RPEMISSIONS
                        WHERE VW_EIS_RPEMISSIONS.INTINVENTORYYEAR = @year AND VW_EIS_RPEMISSIONS.POLLUTANTCODE = 'PM10-PRI'
                        GROUP BY VW_EIS_RPEMISSIONS.FACILITYSITEID) AS PM10PRISum ON ViewList.FacilitySiteID = PM10PRISum.FACILITYSITEID
                        LEFT JOIN (SELECT VW_EIS_RPEMISSIONS.FACILITYSITEID, SUM(VW_EIS_RPEMISSIONS.FLTTOTALEMISSIONS) AS PMCON
                        FROM VW_EIS_RPEMISSIONS
                        WHERE VW_EIS_RPEMISSIONS.INTINVENTORYYEAR = @year AND VW_EIS_RPEMISSIONS.POLLUTANTCODE = 'PM-CON'
                        GROUP BY VW_EIS_RPEMISSIONS.FACILITYSITEID) AS PMCONSum ON ViewList.FacilitySiteID = PMCONSum.FACILITYSITEID
                        LEFT JOIN (SELECT VW_EIS_RPEMISSIONS.FACILITYSITEID, SUM(VW_EIS_RPEMISSIONS.FLTTOTALEMISSIONS) AS NH3
                        FROM VW_EIS_RPEMISSIONS
                        WHERE VW_EIS_RPEMISSIONS.INTINVENTORYYEAR = @year AND VW_EIS_RPEMISSIONS.POLLUTANTCODE = 'NH3'
                        GROUP BY VW_EIS_RPEMISSIONS.FACILITYSITEID) AS NH3Sum ON ViewList.FacilitySiteID = NH3Sum.FACILITYSITEID
                        LEFT JOIN (SELECT VW_EIS_RPEMISSIONS.FACILITYSITEID, SUM(VW_EIS_RPEMISSIONS.FLTTOTALEMISSIONS) AS CO
                        FROM VW_EIS_RPEMISSIONS
                        WHERE VW_EIS_RPEMISSIONS.INTINVENTORYYEAR = @year AND VW_EIS_RPEMISSIONS.POLLUTANTCODE = 'CO'
                        GROUP BY VW_EIS_RPEMISSIONS.FACILITYSITEID) AS COSum ON ViewList.FacilitySiteID = COSum.FACILITYSITEID
                        LEFT JOIN APBFacilityInformation ON '0413'+ViewList.FacilitySiteID = APBFacilityInformation.STRAIRSNUMBER"

                    Dim param As New SqlParameter("@year", cboEIYear.Text)

                    dgvEIResults.DataSource = DB.GetDataTable(SQL, param)

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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewEISummaryByPollutant_Click(sender As Object, e As EventArgs) Handles btnViewEISummaryByPollutant.Click
        Try

            If cboEIYear.Text <> "" AndAlso cboEIPollutants.Text <> "" Then
                If cboEIYear.Text < 2010 Then
                    SQL = "SELECT ROW_NUMBER() OVER(ORDER BY t1.AIRSNumber) AS EIRow, t1.AIRSNumber, t1.FacilityName, t1.Pollutant
                        FROM (SELECT RIGHT(dt.STRAIRSNUMBER, 8) AS AIRSNumber, eisi.STRFACILITYNAME AS FacilityName, dt.PollutantTotal AS Pollutant
                        FROM eisi, (SELECT eiem.STRAIRSNUMBER, eiem.STRPOLLUTANTCODE, SUM(eiem.DBLEMISSIONNUMERICVALUE) AS PollutantTotal, eiem.STRINVENTORYYEAR
                        FROM eiem
                        WHERE eiem.STRINVENTORYYEAR = @year AND eiem.STRPOLLUTANTCODE = @poll
                        GROUP BY eiem.STRAIRSNUMBER, eiem.STRPOLLUTANTCODE, eiem.STRINVENTORYYEAR) AS dt
                        WHERE dt.STRAIRSNUMBER = eisi.STRAIRSNUMBER AND dt.STRINVENTORYYEAR = eisi.STRINVENTORYYEAR) AS t1"
                Else
                    SQL = "SELECT ROW_NUMBER() OVER(ORDER BY ViewList.FacilitySiteID) AS EIRow, ViewList.FacilitySiteID AS AIRSNumber, strFacilityName AS FacilityName, Pollutant
                        FROM (SELECT DISTINCT
                        FacilitySiteID AS FacilitySiteID
                        FROM VW_EIS_RPEMISSIONS
                        WHERE intinventoryyear = @year) AS ViewList, (SELECT facilitysiteid, SUM(fltTotalemissions) AS Pollutant
                        FROM VW_EIS_RPEMISSIONS
                        WHERE intinventoryyear = @year AND pollutantcode = @poll
                        GROUP BY facilitysiteid) AS PollutantSum, APBFacilityInformation
                        WHERE '0413'+ViewList.facilitysiteid = APBFacilityInformation.strAIRSNumber AND ViewList.facilitysiteid = PollutantSum.facilitysiteid "

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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnExportEItoExcel_Click(sender As Object, e As EventArgs) Handles btnExportEItoExcel.Click
        dgvEIResults.ExportToExcel(Me)
    End Sub

#End Region

End Class