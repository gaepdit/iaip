Imports Microsoft.Data.SqlClient

Public Class EmissionsStatement
    Private Property SelectedYear As Integer

    Private Sub EmissionsStatement_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadPermissions()
        LoadESYear()
        RunCounts()
    End Sub

    Private Sub LoadPermissions()
        If Not CurrentUser.HasPermission(UserCan.AccessEmissionsInventory) Then
            Close()
        End If
    End Sub

    Private Sub LoadESYear()
        Dim SQL As String = "Select " &
            "distinct intESYear " &
            "from esschema " &
            "order by intESYear desc"

        Dim dt As DataTable = DB.GetDataTable(SQL)

        For Each dr As DataRow In dt.Rows
            cboYear.Items.Add(dr("intESYear"))
        Next

        cboYear.SelectedIndex = 0

        SelectedYear = CInt(cboYear.SelectedItem)
    End Sub

    Private Sub cboYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboYear.SelectedIndexChanged
        SelectedYear = CInt(cboYear.SelectedItem)
        RunCounts()
        TabControl3.SelectTab(0)
    End Sub

    Private Sub RunCounts()
        Dim year As Integer = SelectedYear

        Dim deadline As New Date(year + 1, 6, 15)
        Dim SQL As String
        Dim param As New SqlParameter("@year", year.ToString)
        Dim params As SqlParameter() = {
            param,
            New SqlParameter("@deadline", deadline)
        }

        SQL = "SELECT COUNT (*) AS ESMailoutCount " &
                "FROM ESMAILOUT em " &
                "LEFT JOIN ESSCHEMA es " &
                "ON em.STRAIRSYEAR  = es.STRAIRSYEAR " &
                "WHERE em.STRESYEAR = @year"
        txtESMailOutCount.Text = DB.GetInteger(SQL, param).ToString

        SQL = "select count(*) as ResponseCount " &
            "from esmailout inner join ESSCHEMA " &
            "on ESMAILOUT.STRAIRSYEAR = ESSCHEMA.STRAIRSYEAR " &
            "where ESSCHEMA.STROPTOUT is not NULL " &
            "and esmailout.STRESYEAR = @year "
        txtResponseCount.Text = DB.GetInteger(SQL, param).ToString

        SQL = "select count(*) as TotaloptinCount " &
            "from ESSchema " &
            "where ESSchema.intESYEAR = @year " &
            " and ESSchema.strOptOut = 'NO'"
        txtTotalOptInCount.Text = DB.GetInteger(SQL, param).ToString

        SQL = "select count(*) as TotaloptOutCount " &
            "from ESSchema " &
            "where ESSchema.intESYEAR = @year " &
            "and ESSchema.strOptOut = 'YES'"
        txtTotalOptOutCount.Text = DB.GetInteger(SQL, param).ToString

        SQL = "select count(*) as TotalinincomplianceCount " &
            "from ESSchema " &
            "where ESSchema.intESYEAR = @year " &
            " and CAST(STRDATEFIRSTCONFIRM AS date) < = @deadline "
        txtTotalincompliance.Text = DB.GetInteger(SQL, params).ToString

        SQL = "select count(*) as TotaloutofcomplianceCount " &
            "from ESSchema " &
            "where ESSchema.intESYEAR = @year " &
            " and CAST(STRDATEFIRSTCONFIRM AS date) > @deadline "
        txtTotaloutofcompliance.Text = DB.GetInteger(SQL, params).ToString

        SQL = "SELECT COUNT ( *) AS MailOutOptInCount " &
                "FROM ESSchema es " &
                "RIGHT JOIN ESMailout em " &
                "ON em.STRAIRSYEAR  = es.STRAIRSYEAR " &
                "WHERE em.STRESYEAR = @year " &
                "AND es.STROPTOUT   = 'NO'"
        txtMailoutOptin.Text = DB.GetInteger(SQL, param).ToString

        SQL = "SELECT COUNT ( *) AS MailOutOptOutCount " &
                "FROM ESSchema es " &
                "RIGHT JOIN ESMailout em " &
                "ON em.STRAIRSYEAR  = es.STRAIRSYEAR " &
                "WHERE em.STRESYEAR = @year " &
                "AND es.STROPTOUT   = 'YES'"
        txtMailOutOptOut.Text = DB.GetInteger(SQL, param).ToString

        SQL = "select count(*) as Nonresponsecount " &
             "from ESSCHEMA " &
             "where ESSCHEMA.intESYEAR = @year " &
             " and ESSchema.strOptOut is NULL"
        txtNonResponseCount.Text = DB.GetInteger(SQL, param).ToString

        SQL = "SELECT COUNT (*) AS removedFacilitiescount " &
                "FROM ESSchema es " &
                "RIGHT JOIN esmailout em " &
                "ON em.STRAIRSYEAR   = es.STRAIRSYEAR " &
                "WHERE em.STRESYEAR  = @year " &
                "AND es.STRAIRSYEAR IS NULL"
        txtESremovedFacilities.Text = DB.GetInteger(SQL, param).ToString

        SQL = "SELECT COUNT ( *) AS extraNonresponderscount " &
                "FROM ESSchema es " &
                "WHERE NOT EXISTS " &
                "  (SELECT * " &
                "  FROM ESMAILOUT em " &
                "  WHERE es.STRAIRSNUMBER = em.STRAIRSNUMBER " &
                "  AND es.INTESYEAR       = em.STRESYEAR " &
                "  ) " &
                "AND es.INTESYEAR  = @year " &
                "AND es.STROPTOUT IS NULL"
        txtESextranonresponder.Text = DB.GetInteger(SQL, param).ToString

        SQL = "SELECT COUNT ( *) AS mailoutNonresponderscount " &
                "FROM esmailout em " &
                "LEFT JOIN ESSchema es " &
                "ON em.STRAIRSYEAR  = es.STRAIRSYEAR " &
                "WHERE em.STRESYEAR = @year " &
                "AND es.STROPTOUT  IS NULL"
        txtESmailoutNonResponder.Text = DB.GetInteger(SQL, param).ToString

        SQL = "SELECT COUNT ( *) AS ExtraCount " &
                "FROM " &
                "  (SELECT es.STRAIRSYEAR AS SchemaAIRS " &
                "  , em.STRAIRSYEAR       AS MailoutAIRS " &
                "  FROM ESMailout em " &
                "  RIGHT JOIN ESSCHEMA es " &
                "  ON es.STRAIRSYEAR  = em.STRAIRSYEAR " &
                "  WHERE es.INTESYEAR = @year " &
                "  AND es.STROPTOUT  IS NOT NULL " &
                "  ) dt_NotInMailout " &
                "INNER JOIN ESSCHEMA es " &
                "ON dt_NotInMailout.SchemaAIRS      = es.STRAIRSYEAR " &
                "WHERE dt_NotInMailout.MailoutAIRS IS NULL"
        Dim extracount As String = DB.GetInteger(SQL, param).ToString

        txtESextraResponders.Text = extracount
        txtextraResponse.Text = extracount

        SQL = "SELECT COUNT ( *) AS ExtraOptinCount " &
                "FROM " &
                "  (SELECT es.STRAIRSYEAR AS SchemaAIRS " &
                "  , em.STRAIRSYEAR       AS MailoutAIRS " &
                "  FROM ESMailout em " &
                "  RIGHT JOIN ESSCHEMA es " &
                "  ON es.STRAIRSYEAR  = em.STRAIRSYEAR " &
                "  WHERE es.INTESYEAR = @year " &
                "  AND es.STROPTOUT  IS NOT NULL " &
                "  ) dt_NotInMailout " &
                "INNER JOIN ESSCHEMA es " &
                "ON dt_NotInMailout.SchemaAIRS      = es.STRAIRSYEAR " &
                "WHERE dt_NotInMailout.MailoutAIRS IS NULL " &
                "AND es.STROPTOUT                   = 'NO'"
        txtExtraOptin.Text = DB.GetInteger(SQL, param).ToString

        SQL = "SELECT COUNT ( *) AS ExtraOptOUTCount " &
                "FROM " &
                "  (SELECT es.STRAIRSYEAR AS SchemaAIRS " &
                "  , em.STRAIRSYEAR       AS MailoutAIRS " &
                "  FROM ESMailout em " &
                "  RIGHT JOIN ESSCHEMA es " &
                "  ON es.STRAIRSYEAR  = em.STRAIRSYEAR " &
                "  WHERE es.INTESYEAR = @year " &
                "  AND es.STROPTOUT  IS NOT NULL " &
                "  ) dt_NotInMailout " &
                "INNER JOIN ESSCHEMA es " &
                "ON dt_NotInMailout.SchemaAIRS      = es.STRAIRSYEAR " &
                "WHERE dt_NotInMailout.MailoutAIRS IS NULL " &
                "AND es.STROPTOUT                   = 'YES'"
        txtExtraOptout.Text = DB.GetInteger(SQL, param).ToString

        SQL = "select count(*) as TotalResponsecount " &
            "from ESSchema " &
            "where ESSchema.intESYEAR = @year " &
            " and ESSchema.strOptOut is not NULL"
        txtTotalResponse.Text = DB.GetInteger(SQL, param).ToString
    End Sub

    Private Sub FindESMailOut()
        Dim AirsNo As String = txtESAIRSNo2.Text
        Dim ESyear As String = SelectedYear.ToString

        Try
            Dim SQL As String = "SELECT * " &
                  "from esMailOut " &
                  "where STRAIRSNUMBER = @STRAIRSNUMBER " &
                  "and STRESYEAR = @STRESYEAR "
            Dim params As SqlParameter() = {
                New SqlParameter("@STRAIRSNUMBER", AirsNo),
                New SqlParameter("@STRESYEAR", ESyear)
            }
            Dim dr As DataRow = DB.GetDataRow(SQL, params)

            If dr IsNot Nothing Then
                If IsDBNull(dr("STRFACILITYNAME")) Then
                    txtESFacilityName.Text = ""
                Else
                    txtESFacilityName.Text = dr("STRFACILITYNAME")
                End If
                If IsDBNull(dr("STRCONTACTPREFIX")) Then
                    txtESprefix.Text = ""
                Else
                    txtESprefix.Text = dr("STRCONTACTPREFIX")
                End If
                If IsDBNull(dr("STRCONTACTFIRSTNAME")) Then
                    txtESFirstName.Text = ""
                Else
                    txtESFirstName.Text = dr("STRCONTACTFIRSTNAME")
                End If
                If IsDBNull(dr("STRCONTACTLASTNAME")) Then
                    txtESLastName.Text = ""
                Else
                    txtESLastName.Text = dr("STRCONTACTLASTNAME")
                End If
                If IsDBNull(dr("STRCONTACTCOMPANYNAME")) Then
                    txtEScompanyName.Text = ""
                Else
                    txtEScompanyName.Text = dr("STRCONTACTCOMPANYNAME")
                End If
                If IsDBNull(dr("STRCONTACTADDRESS1")) Then
                    txtcontactAddress1.Text = ""
                Else
                    txtcontactAddress1.Text = dr("STRCONTACTADDRESS1")
                End If
                If IsDBNull(dr("STRCONTACTADDRESS2")) Then
                    txtcontactAddress2.Text = ""
                Else
                    txtcontactAddress2.Text = dr("STRCONTACTADDRESS2")
                End If
                If IsDBNull(dr("STRCONTACTCITY")) Then
                    txtcontactCity.Text = ""
                Else
                    txtcontactCity.Text = dr("STRCONTACTCITY")
                End If
                If IsDBNull(dr("STRCONTACTSTATE")) Then
                    txtcontactState.Text = ""
                Else
                    txtcontactState.Text = dr("STRCONTACTSTATE")
                End If
                If IsDBNull(dr("STRCONTACTZIPCODE")) Then
                    txtcontactZipCode.Text = ""
                Else
                    txtcontactZipCode.Text = dr("STRCONTACTZIPCODE")
                End If
                If IsDBNull(dr("STRCONTACTEMAIL")) Then
                    txtcontactEmail.Text = ""
                Else
                    txtcontactEmail.Text = dr("STRCONTACTEMAIL")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FindESData()
        Dim AirsNo As String = txtESAirsNo.Text
        Dim intESyear As Integer = SelectedYear

        Try
            Dim SQL As String = "SELECT * " &
            "from esschema " &
            "where STRAIRSNUMBER = @STRAIRSNUMBER " &
            "and INTESYEAR = @INTESYEAR "
            Dim params As SqlParameter() = {
                New SqlParameter("@STRAIRSNUMBER", AirsNo),
                New SqlParameter("@INTESYEAR", intESyear)
            }
            Dim dr As DataRow = DB.GetDataRow(SQL, params)

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
                    txtESContactFirstName.Text = ""
                Else
                    txtESContactFirstName.Text = dr("STRCONTACTFIRSTNAME")
                End If
                If IsDBNull(dr("STRCONTACTLASTNAME")) Then
                    txtESContactLastName.Text = ""
                Else
                    txtESContactLastName.Text = dr("STRCONTACTLASTNAME")
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
                    txtESEmail.Text = ""
                Else
                    txtESEmail.Text = dr("STRCONTACTEMAIL")
                End If
                If IsDBNull(dr("DBLVOCEMISSION")) Then
                    txtVOCEmission.Text = ""
                Else
                    txtVOCEmission.Text = dr("DBLVOCEMISSION")
                    If txtVOCEmission.Text = "-1" Then
                        txtVOCEmission.Text = "No Value"
                    End If
                End If
                If IsDBNull(dr("DBLNOXEMISSION")) Then
                    txtNOXEmission.Text = ""
                Else
                    txtNOXEmission.Text = dr("DBLNOXEMISSION")
                    If txtNOXEmission.Text = "-1" Then
                        txtNOXEmission.Text = "No Value"
                    End If
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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvESDataCount_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvESDataCount.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvESDataCount.HitTest(e.X, e.Y)

        Try
            If dgvESDataCount.RowCount = 0 Then
                Return
            End If

            If dgvESDataCount.ColumnCount <= 2 Then
                ClearMailOut()
                Return
            End If

            If dgvESDataCount.RowCount = 0 OrElse hti.RowIndex = -1 Then
                Return
            End If

            If dgvESDataCount.Columns(0).HeaderText = "Airs No." AndAlso
                Not IsDBNull(dgvESDataCount(0, hti.RowIndex).Value) Then

                ClearMailOut()
                txtESAIRSNo2.Text = dgvESDataCount(0, hti.RowIndex).Value.ToString
                txtESFacilityName.Text = dgvESDataCount(1, hti.RowIndex).Value.ToString
                FindESMailOut()
            End If

            If dgvESDataCount.ColumnCount > 3 AndAlso
                dgvESDataCount.Columns(3).HeaderText = "Confirmation Number" Then
                txtESAirsNo.Text = dgvESDataCount(0, hti.RowIndex).Value.ToString
                txtFACILITYNAME.Text = dgvESDataCount(1, hti.RowIndex).Value.ToString

                If Not IsDBNull(dgvESDataCount(3, hti.RowIndex).Value) Then
                    ClearMailOut()
                    txtConfirmationNumber.Text = dgvESDataCount(3, hti.RowIndex).Value.ToString
                    FindESData()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewMailOut_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewMailOut.LinkClicked
        Try
            Dim SQL As String = "SELECT STRAIRSNUMBER, " &
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

            Dim param As New SqlParameter("@year", SelectedYear.ToString)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewOptin_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewTotalOptin.LinkClicked
        Try
            Dim SQL As String = "SELECT es.STRAIRSNUMBER " &
                ", es.STRFACILITYNAME " &
                ", es.STRDATEFIRSTCONFIRM " &
                ", es.DBLVOCEMISSION " &
                ", es.DBLNOXEMISSION " &
                ", es.STRCONFIRMATIONNBR " &
                "FROM esSchema es " &
                "LEFT JOIN esmailout em " &
                "ON es.STRAIRSYEAR  = em.STRAIRSYEAR " &
                "WHERE es.INTESYEAR = @year " &
                "AND es.STROPTOUT   = 'NO' " &
                "ORDER BY es.STRFACILITYNAME"
            Dim param As New SqlParameter("@year", SelectedYear.ToString)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("DBLVOCEMISSION").HeaderText = "VOC Emissions"
            dgvESDataCount.Columns("DBLVOCEMISSION").DisplayIndex = 3
            dgvESDataCount.Columns("DBLNOXEMISSION").HeaderText = "NOX Emissions"
            dgvESDataCount.Columns("DBLNOXEMISSION").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 5

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewOptOut_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewTotalOptOut.LinkClicked
        Try
            Dim SQL As String = "SELECT es.STRAIRSNUMBER " &
                ", es.STRFACILITYNAME " &
                ", es.STRDATEFIRSTCONFIRM " &
                ", es.STRCONFIRMATIONNBR " &
                "FROM esSchema es " &
                "LEFT JOIN esmailout em " &
                "ON es.STRAIRSYEAR  = em.STRAIRSYEAR " &
                "WHERE es.INTESYEAR = @year " &
                "AND es.STROPTOUT   = 'YES' " &
                "ORDER BY es.STRFACILITYNAME"
            Dim param As New SqlParameter("@year", SelectedYear.ToString)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewOutofcompliance_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewOutofcompliance.LinkClicked
        Try
            Dim SQL As String = "SELECT esSchema.STRAIRSNUMBER, " &
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
            "where intESyear = @year " &
            "and esSchema.STRDATEFIRSTCONFIRM is not NULL " &
            " and CAST(esSchema.STRDATEFIRSTCONFIRM AS date) > @deadline " &
            "order by esSchema.STRFACILITYNAME"

            Dim params As SqlParameter() = {
                New SqlParameter("@year", SelectedYear.ToString),
                New SqlParameter("@deadline", New Date(SelectedYear + 1, 6, 15))
            }

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, params)

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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewINCompliance_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewINCompliance.LinkClicked
        Try
            Dim SQL As String = "SELECT esSchema.STRAIRSNUMBER, " &
            "esSchema.STRFACILITYNAME, " &
            "esSchema.STRDATEFIRSTCONFIRM, " &
            "esSchema.STRCONFIRMATIONNBR " &
            "from esSchema " &
            "where esSchema.intESyear = @year " &
            "and esSchema.STRDATEFIRSTCONFIRM is not NULL " &
            " and CAST(esSchema.STRDATEFIRSTCONFIRM AS date) < = @deadline " &
            "order by esSchema.STRFACILITYNAME"

            Dim params As SqlParameter() = {
                New SqlParameter("@year", SelectedYear.ToString),
                New SqlParameter("@deadline", New Date(SelectedYear + 1, 6, 15))
            }

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, params)

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "Date First Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewESMailOut_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewESMailOut.LinkClicked
        Try
            Dim SQL As String = "SELECT esMailOut.STRAIRSNUMBER, " &
            "esMailOut.STRFACILITYNAME, " &
            "esMailOut.STRCONTACTFIRSTNAME, " &
            "esMailOut.STRCONTACTLASTNAME, " &
            "esMailOut.STRCONTACTCOMPANYname, " &
            "esMailOut.STRCONTACTADDRESS1, " &
            "esMailOut.STRCONTACTCITY, " &
            "esMailOut.STRCONTACTSTATE, " &
            "esMailOut.STRCONTACTZIPCODE, " &
            "esMailOut.STRCONTACTEMAIL " &
            "from esMailOut " &
            "where STRESYEAR = @year " &
            "order by STRFACILITYNAME"

            Dim param As New SqlParameter("@year", SelectedYear.ToString)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewESData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewESData.LinkClicked
        Try
            Dim SQL As String = "SELECT esSchema.STRAIRSNUMBER, esSchema.STRFACILITYNAME,
                CASE WHEN DBLVOCEMISSION = '-1' THEN 'No Value' ELSE CAST(DBLVOCEMISSION AS VARCHAR(MAX)) END AS DBLVOCEMISSION, esSchema.STRCONFIRMATIONNBR,
                CASE WHEN DBLNOXEMISSION = '-1' THEN 'No Value' ELSE CAST(DBLNOXEMISSION AS VARCHAR(MAX)) END AS DBLNOXEMISSION, esSchema.STRDATEFIRSTCONFIRM
                FROM esSchema
                WHERE esSchema.intESyear = @year
                ORDER BY esSchema.STRFACILITYNAME"

            Dim param As New SqlParameter("@year", SelectedYear.ToString)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("DBLVOCEMISSION").HeaderText = "VOC Emissions"
            dgvESDataCount.Columns("DBLVOCEMISSION").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3
            dgvESDataCount.Columns("DBLNOXEMISSION").HeaderText = "NOX Emissions"
            dgvESDataCount.Columns("DBLNOXEMISSION").DisplayIndex = 4
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 5

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewNonResponse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewNonResponse.LinkClicked
        Try
            Dim SQL As String = "SELECT es.STRAIRSNUMBER " &
                ", es.STRFACILITYNAME " &
                "FROM ESSCHEMA es " &
                "LEFT JOIN esMailOut em " &
                "ON em.STRAIRSYEAR  = es.STRAIRSYEAR " &
                "WHERE es.INTESYEAR = @year " &
                "AND es.STROPTOUT  IS NULL " &
                "ORDER BY em.STRFACILITYNAME"
            Dim param As New SqlParameter("@year", SelectedYear.ToString)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewOptIn_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewOptIn.LinkClicked
        Try
            Dim SQL As String = "SELECT es.STRAIRSNUMBER " &
                ", es.STRFACILITYNAME " &
                ", es.STRDATEFIRSTCONFIRM " &
                ", es.STRCONFIRMATIONNBR " &
                "FROM esSchema es " &
                "RIGHT JOIN esmailout em " &
                "ON em.STRAIRSYEAR             = es.STRAIRSYEAR " &
                "WHERE es.STRDATEFIRSTCONFIRM IS NOT NULL " &
                "AND es.INTESYEAR              = @year " &
                "AND es.STROPTOUT              = 'NO' " &
                "ORDER BY es.STRFACILITYNAME"
            Dim param As New SqlParameter("@year", SelectedYear.ToString)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewOptOut_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewOptOut.LinkClicked
        Try
            Dim SQL As String = "SELECT es.STRAIRSNUMBER " &
                ", es.STRFACILITYNAME " &
                ", es.STRDATEFIRSTCONFIRM " &
                ", es.STRCONFIRMATIONNBR " &
                "FROM esSchema es " &
                "RIGHT JOIN esmailout em " &
                "ON em.STRAIRSYEAR             = es.STRAIRSYEAR " &
                "WHERE es.STRDATEFIRSTCONFIRM IS NOT NULL " &
                "AND es.INTESYEAR              = @year " &
                "AND es.STROPTOUT              = 'YES' " &
                "ORDER BY es.STRFACILITYNAME"
            Dim param As New SqlParameter("@year", SelectedYear.ToString)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewExtraOptOut_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewExtraOptOut.LinkClicked
        Try
            Dim SQL As String = "SELECT es.STRAIRSNUMBER " &
                ", es.STRFACILITYNAME " &
                ", es.STRDATEFIRSTCONFIRM " &
                ", es.STRCONFIRMATIONNBR " &
                "FROM " &
                "  (SELECT es.STRAIRSYEAR AS SchemaAIRS " &
                "  , em.STRAIRSYEAR       AS MailoutAIRS " &
                "  FROM ESMailout em " &
                "  RIGHT JOIN ESSCHEMA es " &
                "  ON es.STRAIRSYEAR  = em.STRAIRSYEAR " &
                "  WHERE es.INTESYEAR = @year " &
                "  AND es.STROPTOUT  IS NOT NULL " &
                "  ) dt_NotInMailout " &
                "INNER JOIN ESSCHEMA es " &
                "ON dt_NotInMailout.SchemaAIRS      = es.STRAIRSYEAR " &
                "WHERE dt_NotInMailout.MailoutAIRS IS NULL " &
                "AND es.STROPTOUT                   = 'YES'"
            Dim param As New SqlParameter("@year", SelectedYear.ToString)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 1
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 0
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewExtraOptIn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewExtraOptIn.LinkClicked
        Try
            Dim SQL As String = "SELECT es.STRAIRSNUMBER " &
                ", es.STRFACILITYNAME " &
                ", es.STRDATEFIRSTCONFIRM " &
                ", es.STRCONFIRMATIONNBR " &
                "FROM " &
                "  (SELECT es.STRAIRSYEAR AS SchemaAIRS " &
                "  , em.STRAIRSYEAR       AS MailoutAIRS " &
                "  FROM ESMailout em " &
                "  RIGHT JOIN ESSCHEMA es " &
                "  ON em.STRAIRSYEAR  = es.STRAIRSYEAR " &
                "  WHERE es.INTESYEAR = @year " &
                "  AND es.STROPTOUT  IS NOT NULL " &
                "  ) dt_NotInMailout " &
                "INNER JOIN ESSCHEMA es " &
                "ON es.STRAIRSYEAR                  = dt_NotInMailout.SchemaAIRS " &
                "WHERE dt_NotInMailout.MailoutAIRS IS NULL " &
                "AND es.STROPTOUT                   = 'NO'"
            Dim param As New SqlParameter("@year", SelectedYear.ToString)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewTotalResponse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewTotalResponse.LinkClicked
        Try
            Dim SQL As String = "SELECT esSchema.STRAIRSNUMBER, " &
            "esSchema.STRFACILITYNAME, " &
            "esSchema.STRCONTACTFIRSTNAME, " &
            "esSchema.STRCONTACTLASTNAME, " &
            "esSchema.STRCONTACTCOMPANY, " &
            "esSchema.STRCONTACTEMAIL, " &
            "esSchema.STRCONTACTPHONENUMBER " &
            "from esSchema " &
            "where esSchema.intESyear = @year " &
            "and esSchema.STROPTOUT is not NULL " &
            "order by esSchema.STRFACILITYNAME"
            Dim param As New SqlParameter("@year", SelectedYear.ToString)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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

            clearESData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ClearMailOut()
        txtESAIRSNo2.Clear()
        txtESFacilityName.Clear()
        txtESprefix.Clear()
        txtESFirstName.Clear()
        txtESLastName.Clear()
        txtEScompanyName.Clear()
        txtcontactAddress1.Clear()
        txtcontactAddress2.Clear()
        txtcontactCity.Clear()
        txtcontactState.Clear()
        txtcontactZipCode.Clear()
        txtcontactEmail.Clear()
    End Sub

    Private Sub clearESData()
        txtESAirsNo.Clear()
        txtFACILITYNAME.Clear()
        txtFACILITYADDRESS.Clear()
        txtFACILITYCITY.Clear()
        txtFACILITYSTATE.Clear()
        txtFACILITYZIP.Clear()
        txtCOUNTY.Clear()
        txtXCOORDINATE.Clear()
        txtYCOORDINATE.Clear()
        txtHORIZONTALCOLLECTIONCODE.Clear()
        txtHORIZONTALACCURACYMEASURE.Clear()
        txtHORIZONTALREFERENCECODE.Clear()
        txtCompany.Clear()
        txtTitle.Clear()
        txtPhone.Clear()
        txtFax.Clear()
        txtESContactFirstName.Clear()
        txtESContactLastName.Clear()
        txtAddress1.Clear()
        txtAddress2.Clear()
        txtCity.Clear()
        txtState.Clear()
        txtZip.Clear()
        txtVOCEmission.Clear()
        txtNOXEmission.Clear()
        txtConfirmationNbr.Clear()
        txtFirstConfirmedDate.Clear()
    End Sub

    Private Sub lblviewESextraresponder_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) _
        Handles lblviewESextraresponder.LinkClicked, lblextraResponse.LinkClicked

        Try
            Dim SQL As String = "SELECT es.STRAIRSNUMBER,
                             es.STRFACILITYNAME,
                             es.STRCONTACTFIRSTNAME,
                             es.STRCONTACTLASTNAME,
                             es.STRCONTACTCOMPANY,
                             es.STRCONTACTEMAIL,
                             es.STRCONTACTPHONENUMBER
                FROM (SELECT es.STRAIRSYEAR AS SchemaAIRS,
                             em.STRAIRSYEAR AS MailoutAIRS
                      FROM ESMailout em
                          Right Join ESSCHEMA es
                          ON es.STRAIRSYEAR = em.STRAIRSYEAR
                      WHERE es.INTESYEAR = @year
                        AND es.STROPTOUT IS NOT NULL) dt_NotInMailout
                    INNER JOIN ESSCHEMA es
                    On dt_NotInMailout.SchemaAIRS = es.STRAIRSYEAR
                WHERE dt_NotInMailout.MailoutAIRS IS NULL"
            Dim param As New SqlParameter("@year", SelectedYear.ToString)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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

            clearESData()
            ClearMailOut()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblviewESremovedfacility_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblviewESremovedfacility.LinkClicked
        Try
            Dim SQL As String = "SELECT em.STRAIRSNUMBER " &
                ", em.STRFACILITYNAME " &
                "FROM esSchema es " &
                "RIGHT JOIN esmailout em " &
                "ON em.STRAIRSYEAR   = es.STRAIRSYEAR " &
                "WHERE em.STRESYEAR  = @year " &
                "AND es.STRAIRSYEAR IS NULL " &
                "ORDER BY em.STRFACILITYNAME"
            Dim param As New SqlParameter("@year", SelectedYear.ToString)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblviewmailoutnonresponder_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblviewmailoutnonresponder.LinkClicked
        Try
            Dim SQL As String = "SELECT esmailout.STRAIRSNUMBER " &
                ", esmailout.STRFACILITYNAME " &
                ", esmailout.STRCONTACTFIRSTNAME " &
                ", esmailout.STRCONTACTLASTNAME " &
                ", esmailout.STRCONTACTCOMPANYNAME " &
                ", esmailout.STRCONTACTADDRESS1 " &
                ", esmailout.STRCONTACTCITY " &
                ", esmailout.STRCONTACTSTATE " &
                ", esmailout.STRCONTACTZIPCODE " &
                ", esmailout.STRCONTACTEMAIL " &
                "FROM esmailout " &
                "LEFT JOIN ESSchema " &
                "ON esmailout.STRAIRSYEAR  = ESSchema.STRAIRSYEAR " &
                "WHERE esmailout.STRESYEAR = @year " &
                "AND ESSchema.STROPTOUT   IS NULL " &
                "ORDER BY ESSchema.STRFACILITYNAME"
            Dim param As New SqlParameter("@year", SelectedYear.ToString)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvESDataCount.Columns("STRCONTACTCOMPANYNAME").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTCOMPANYNAME").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Contact Address"
            dgvESDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 5
            dgvESDataCount.Columns("STRCONTACTCITY").HeaderText = "Contact City"
            dgvESDataCount.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvESDataCount.Columns("STRCONTACTSTATE").HeaderText = "Contact State"
            dgvESDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvESDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Contact Zip"
            dgvESDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 9

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblviewextraNonresponse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblviewextraNonresponse.LinkClicked
        Try
            Dim intYear As Integer = SelectedYear

            Dim SQL As String = "SELECT esSchema.STRAIRSNUMBER, " &
                "esSchema.STRFACILITYNAME " &
                "from ESSchema " &
                " where  not exists (select * from ESMAILOUT " &
                " where ESSchema.STRAIRSNUMBER = ESMAILOUT.STRAIRSNUMBER" &
                " and ESSchema.INTESYEAR = ESMAILOUT.strESYEAR) " &
                " and ESSchema.INTESYEAR = @year " &
                " and ESSchema.STROPTOUT is null   " &
                "order by esSchema.STRFACILITYNAME"

            Dim p As New SqlParameter("@year", intYear)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, p)

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvESDataCount_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvESDataCount.CellFormatting
        If e IsNot Nothing AndAlso e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) AndAlso
            dgvESDataCount.Columns(e.ColumnIndex).HeaderText = "Airs No." Then
            e.Value = New Apb.ApbFacilityId(e.Value.ToString).FormattedString
        End If
    End Sub
End Class
