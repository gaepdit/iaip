Imports System.Data.SqlClient
Imports System.Linq

Public Class EmissionsStatement
    Private Property SelectedYear As Integer

    Private Sub EmissionsStatement_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadPermissions()
        LoadESYear()
        LoadMailOutYear()
        LoadESEnrollmentYear()

        RunCounts()
    End Sub

    Private Sub LoadPermissions()
        If AccountFormAccess(130, 3) <> "1" AndAlso AccountFormAccess(130, 4) <> "1" Then
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

    Private Sub LoadMailOutYear()
        Dim SQL As String = "Select distinct STRESYEAR " &
            "from esmailout " &
            "order by STRESYEAR desc"
        Dim dt As DataTable = DB.GetDataTable(SQL)

        Dim maxYear As Integer = Convert.ToInt32(dt.AsEnumerable().Max(Function(row) Convert.ToInt32(row("STRESYEAR")))) + 1
        cboMailoutYear.Items.Add(maxYear.ToString)

        For Each dr As DataRow In dt.Rows
            cboMailoutYear.Items.Add(dr("STRESYEAR"))
        Next

        cboMailoutYear.SelectedIndex = 0
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
            "from esmailout, ESSCHEMA " &
            "where ESMAILOUT.STRAIRSYEAR = ESSCHEMA.STRAIRSYEAR " &
            "and ESSCHEMA.STROPTOUT is not NULL " &
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
                "FROM esMailOut em " &
                "LEFT JOIN ESSCHEMA es " &
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

    Private Sub lblextraResponse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblextraResponse.LinkClicked
        Try
            Dim SQL As String = "SELECT dt_NotInMailout.SchemaAIRS " &
                    ", es.STRAIRSNUMBER " &
                    ", es.STRFACILITYNAME " &
                    ", es.STRCONTACTFIRSTNAME " &
                    ", es.STRCONTACTLASTNAME " &
                    ", es.STRCONTACTCOMPANY " &
                    ", es.STRCONTACTEMAIL " &
                    ", es.STRCONTACTPHONENUMBER " &
                    "FROM " &
                    "  (SELECT es.STRAIRSNUMBER AS SchemaAIRS " &
                    "  , em.STRAIRSNUMBER       AS MailoutAIRS " &
                    "  FROM ESMailout em " &
                    "  RIGHT JOIN ESSCHEMA es " &
                    "  ON es.STRAIRSYEAR  = em.STRAIRSYEAR " &
                    "  WHERE es.INTESYEAR = @year " &
                    "  AND es.STROPTOUT  IS NOT NULL " &
                    "  ) dt_NotInMailout " &
                    "INNER JOIN ESSCHEMA es " &
                    "ON dt_NotInMailout.SchemaAIRS      = es.STRAIRSNUMBER " &
                    "WHERE dt_NotInMailout.MailoutAIRS IS NULL"
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

    Private Sub SaveESMailOut()
        Dim AirsNo As String = txtESAIRSNo2.Text
        Dim ESFacilityName As String = txtESFacilityName.Text
        Dim ESPrefix As String = txtESprefix.Text
        Dim ESFirstName As String = txtESFirstName.Text
        Dim ESLastName As String = txtESLastName.Text
        Dim ESCompanyName As String = txtEScompanyName.Text
        Dim ESContactAddress1 As String = txtcontactAddress1.Text
        Dim ESContactAddress2 As String = txtcontactAddress2.Text
        Dim ESYear As String = SelectedYear.ToString
        Dim ESContactCity As String = txtcontactCity.Text
        Dim EScontactState As String = txtcontactState.Text
        Dim ESContactZip As String = txtcontactZipCode.Text
        Dim ESContactEmail As String = txtcontactEmail.Text
        Dim airsYear As String = AirsNo & ESYear

        Try
            Dim SQL As String = "Select strAIRSYear " &
            "from EsMailOut " &
            "where STRAIRSYEAR = @STRAIRSYEAR "
            Dim param As New SqlParameter("@STRAIRSYEAR", airsYear)

            If DB.ValueExists(SQL, param) Then
                SQL = "update ESMailOut set " &
                    "ESMailOut.STRCONTACTPREFIX = @STRCONTACTPREFIX, " &
                    "ESMailOut.STRCONTACTFIRSTNAME = @STRCONTACTFIRSTNAME, " &
                    "ESMailOut.STRCONTACTLASTNAME = @STRCONTACTLASTNAME, " &
                    "ESMailOut.STRCONTACTCOMPANYNAME = @STRCONTACTCOMPANYNAME, " &
                    "ESMailOut.STRCONTACTADDRESS1 = @STRCONTACTADDRESS1, " &
                    "ESMailOut.STRCONTACTADDRESS2 = @STRCONTACTADDRESS2, " &
                    "ESMailOut.STRCONTACTCITY = @STRCONTACTCITY, " &
                    "ESMailOut.STRCONTACTSTATE = @STRCONTACTSTATE, " &
                    "ESMailOut.STRCONTACTZIPCODE = @STRCONTACTZIPCODE, " &
                    "ESMailOut.STRCONTACTEMAIL = @STRCONTACTEMAIL " &
                    "where ESMailOut.STRAIRSNUMBER = @STRAIRSNUMBER "

                Dim params As SqlParameter() = {
                    New SqlParameter("@STRCONTACTPREFIX", ESPrefix),
                    New SqlParameter("@STRCONTACTFIRSTNAME", ESFirstName),
                    New SqlParameter("@STRCONTACTLASTNAME", ESLastName),
                    New SqlParameter("@STRCONTACTCOMPANYNAME", ESCompanyName),
                    New SqlParameter("@STRCONTACTADDRESS1", ESContactAddress1),
                    New SqlParameter("@STRCONTACTADDRESS2", ESContactAddress2),
                    New SqlParameter("@STRCONTACTCITY", ESContactCity),
                    New SqlParameter("@STRCONTACTSTATE", EScontactState),
                    New SqlParameter("@STRCONTACTZIPCODE", ESContactZip),
                    New SqlParameter("@STRCONTACTEMAIL", ESContactEmail),
                    New SqlParameter("@STRAIRSNUMBER", AirsNo)
                }
                DB.RunCommand(SQL, params)

                MsgBox("Info updated")
            Else
                SQL = "Insert into ESMailOut " &
                    "(STRAIRSYEAR, " &
                    "STRAIRSNUMBER, " &
                    "STRFACILITYNAME, " &
                    "STRCONTACTPREFIX, " &
                    "STRCONTACTFIRSTNAME, " &
                    "STRCONTACTLASTNAME, " &
                    "STRCONTACTCOMPANYNAME, " &
                    "STRCONTACTADDRESS1, " &
                    "STRCONTACTADDRESS2, " &
                    "STRCONTACTCITY, " &
                    "STRCONTACTSTATE, " &
                    "STRCONTACTZIPCODE, " &
                    "STRESYEAR, " &
                    "STRCONTACTEMAIL) " &
                    "values (" &
                    "@STRAIRSYEAR, " &
                    "@STRAIRSNUMBER, " &
                    "@STRFACILITYNAME, " &
                    "@STRCONTACTPREFIX, " &
                    "@STRCONTACTFIRSTNAME, " &
                    "@STRCONTACTLASTNAME, " &
                    "@STRCONTACTCOMPANYNAME, " &
                    "@STRCONTACTADDRESS1, " &
                    "@STRCONTACTADDRESS2, " &
                    "@STRCONTACTCITY, " &
                    "@STRCONTACTSTATE, " &
                    "@STRCONTACTZIPCODE, " &
                    "@STRESYEAR, " &
                    "@STRCONTACTEMAIL) "
                Dim params As SqlParameter() = {
                    New SqlParameter("@STRAIRSYEAR", airsYear),
                    New SqlParameter("@STRAIRSNUMBER", AirsNo),
                    New SqlParameter("@STRFACILITYNAME", ESFacilityName),
                    New SqlParameter("@STRCONTACTPREFIX", ESPrefix),
                    New SqlParameter("@STRCONTACTFIRSTNAME", ESFirstName),
                    New SqlParameter("@STRCONTACTLASTNAME", ESLastName),
                    New SqlParameter("@STRCONTACTCOMPANYNAME", ESCompanyName),
                    New SqlParameter("@STRCONTACTADDRESS1", ESContactAddress1),
                    New SqlParameter("@STRCONTACTADDRESS2", ESContactAddress2),
                    New SqlParameter("@STRCONTACTCITY", ESContactCity),
                    New SqlParameter("@STRCONTACTSTATE", EScontactState),
                    New SqlParameter("@STRCONTACTZIPCODE", ESContactZip),
                    New SqlParameter("@STRESYEAR", ESYear),
                    New SqlParameter("@STRCONTACTEMAIL", ESContactEmail)
                }
                DB.RunCommand(SQL, params)

                MsgBox("Info added")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DeleteESMailOut()
        Dim AirsNo As String = txtESAIRSNo2.Text
        Dim ESyear As String = SelectedYear.ToString

        Try
            Dim SQL As String = "delete from ESMailOut " &
            "where ESMailOut.STRAIRSNUMBER = @STRAIRSNUMBER " &
            "and ESMailOut.STRESYEAR = @STRESYEAR "
            Dim params As SqlParameter() = {
                New SqlParameter("@STRAIRSNUMBER", AirsNo),
                New SqlParameter("@STRESYEAR", ESyear)
            }
            DB.RunCommand(SQL, params)
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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveESMailOut()
    End Sub

    Private Sub btnESDelete_Click(sender As Object, e As EventArgs) Handles btnESDelete.Click
        Try
            DeleteESMailOut()
            ClearMailOut()
            MsgBox("The info has been deleted")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub GenerateESMailOut()
        Dim airsNumber As String
        Dim airsYear As String
        Dim FACILITYNAME As String
        Dim CONTACTCOMPANYNAME As String
        Dim CONTACTADDRESS1 As String
        Dim CONTACTCITY As String
        Dim CONTACTSTATE As String
        Dim CONTACTZIPCODE As String
        Dim CONTACTFIRSTNAME As String
        Dim CONTACTLASTNAME As String
        Dim CONTACTEMAIL As String
        Dim ESYear As String = cboMailoutYear.SelectedItem
        Dim OperationalStatus As String
        Dim FacilityClass As String

        Try
            Dim SQL As String = "Select strAirsNumber " &
            "FROM ESmailOut " &
            "where strESyear = @strESyear "
            Dim param As New SqlParameter("strESyear", ESYear)

            If DB.ValueExists(SQL, param) Then
                MsgBox("That year is already being used." & vbCrLf & "If you want to use that year," & vbCrLf & "you must first delete it from the database.")
            Else
                If cboMailoutYear.Text <> "" Then
                    SQL = "SELECT dt_ESContact.STRAIRSNUMBER, fi.STRFACILITYNAME, hd.STROPERATIONALSTATUS, hd.STRCLASS,
                        CASE WHEN dt_ESContact.STRKEY = '42' THEN dt_ESContact.STRCONTACTLASTNAME WHEN dt_ESContact.STRKEY IS NULL THEN dt_PermitContact.STRCONTACTLASTNAME ELSE 'N/A' END AS STRContactLastName,
                        CASE WHEN dt_ESContact.STRKEY = '42' THEN dt_ESContact.STRCONTACTFIRSTNAME WHEN dt_ESContact.STRKEY IS NULL THEN dt_PermitContact.STRCONTACTFIRSTNAME ELSE 'N/A' END AS STRContactfirstName,
                        CASE WHEN dt_ESContact.STRKEY = '42' THEN dt_ESContact.STRCONTACTCOMPANYNAME WHEN dt_ESContact.STRKEY IS NULL THEN dt_PermitContact.STRCONTACTCOMPANYNAME END AS STRContactCompanyName,
                        CASE WHEN dt_ESContact.STRKEY = '42' THEN dt_ESContact.STRCONTACTEMAIL WHEN dt_ESContact.STRKEY IS NULL THEN dt_PermitContact.STRCONTACTEMAIL END AS STRContactEmail,
                        CASE WHEN dt_ESContact.STRKEY = '42' THEN dt_ESContact.STRCONTACTPREFIX WHEN dt_ESContact.STRKEY IS NULL THEN dt_PermitContact.STRCONTACTPREFIX END AS strCONTACTPREFIX,
                        CASE WHEN dt_ESContact.STRKEY = '42' THEN dt_ESContact.STRCONTACTADDRESS1 WHEN dt_ESContact.STRKEY IS NULL THEN dt_PermitContact.STRCONTACTADDRESS1 END AS STRCONTACTADDRESS1,
                        CASE WHEN dt_ESContact.STRKEY = '42' THEN dt_ESContact.STRCONTACTCITY WHEN dt_ESContact.STRKEY IS NULL THEN dt_PermitContact.STRCONTACTCITY END AS STRCONTACTCITY,
                        CASE WHEN dt_ESContact.STRKEY = '42' THEN dt_ESContact.STRCONTACTSTATE WHEN dt_ESContact.STRKEY IS NULL THEN dt_PermitContact.STRCONTACTSTATE END AS STRCONTACTSTATE,
                        CASE WHEN dt_ESContact.STRKEY = '42' THEN dt_ESContact.STRCONTACTZIPCODE WHEN dt_ESContact.STRKEY IS NULL THEN dt_PermitContact.STRCONTACTZIPCODE END AS STRCONTACTZIPCODE
                        FROM (SELECT DISTINCT
                        dt_ESList.STRAIRSNUMBER, dt_Contact.STRKEY, dt_Contact.STRCONTACTLASTNAME, dt_Contact.STRCONTACTFIRSTNAME, dt_Contact.STRCONTACTCOMPANYNAME, dt_Contact.STRCONTACTEMAIL, dt_Contact.STRCONTACTPREFIX, dt_Contact.STRCONTACTADDRESS1, dt_Contact.STRCONTACTCITY, dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE
                        FROM (SELECT *
                        FROM APBHEADERDATA AS hd
                        WHERE (hd.STROPERATIONALSTATUS = 'O' OR hd.STROPERATIONALSTATUS = 'P' OR hd.STROPERATIONALSTATUS = 'C') AND hd.STRCLASS = 'A' AND (hd.STRAIRSNUMBER LIKE '____121%' OR hd.STRAIRSNUMBER LIKE '____013%' OR hd.STRAIRSNUMBER LIKE '____015%' OR hd.STRAIRSNUMBER LIKE '____045%' OR hd.STRAIRSNUMBER LIKE '____057%' OR hd.STRAIRSNUMBER LIKE '____063%' OR hd.STRAIRSNUMBER LIKE '____067%' OR hd.STRAIRSNUMBER LIKE '____077%' OR hd.STRAIRSNUMBER LIKE '____089%' OR hd.STRAIRSNUMBER LIKE '____097%' OR hd.STRAIRSNUMBER LIKE '____113%' OR hd.STRAIRSNUMBER LIKE '____117%' OR hd.STRAIRSNUMBER LIKE '____135%' OR hd.STRAIRSNUMBER LIKE '____139%' OR hd.STRAIRSNUMBER LIKE '____151%' OR hd.STRAIRSNUMBER LIKE '____217%' OR hd.STRAIRSNUMBER LIKE '____223%' OR hd.STRAIRSNUMBER LIKE '____247%' OR hd.STRAIRSNUMBER LIKE '____255%' OR hd.STRAIRSNUMBER LIKE '____297%') ) AS dt_ESList
                        LEFT JOIN (SELECT *
                        FROM APBCONTACTINFORMATION AS ci
                        WHERE ci.STRKEY = 42) AS dt_Contact ON dt_ESList.STRAIRSNUMBER = dt_Contact.STRAIRSNUMBER) AS dt_ESContact
                        LEFT JOIN (SELECT DISTINCT
                        dt_ESList.STRAIRSNUMBER, dt_Contact.STRKEY, dt_Contact.STRCONTACTLASTNAME, dt_Contact.STRCONTACTFIRSTNAME, dt_Contact.STRCONTACTCOMPANYNAME, dt_Contact.STRCONTACTEMAIL, dt_Contact.STRCONTACTPREFIX, dt_Contact.STRCONTACTADDRESS1, dt_Contact.STRCONTACTCITY, dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE
                        FROM (SELECT *
                        FROM APBHEADERDATA AS hd
                        WHERE (hd.STROPERATIONALSTATUS = 'O' OR hd.STROPERATIONALSTATUS = 'P' OR hd.STROPERATIONALSTATUS = 'C') AND hd.STRCLASS = 'A' AND (hd.STRAIRSNUMBER LIKE '____121%' OR hd.STRAIRSNUMBER LIKE '____013%' OR hd.STRAIRSNUMBER LIKE '____015%' OR hd.STRAIRSNUMBER LIKE '____045%' OR hd.STRAIRSNUMBER LIKE '____057%' OR hd.STRAIRSNUMBER LIKE '____063%' OR hd.STRAIRSNUMBER LIKE '____067%' OR hd.STRAIRSNUMBER LIKE '____077%' OR hd.STRAIRSNUMBER LIKE '____089%' OR hd.STRAIRSNUMBER LIKE '____097%' OR hd.STRAIRSNUMBER LIKE '____113%' OR hd.STRAIRSNUMBER LIKE '____117%' OR hd.STRAIRSNUMBER LIKE '____135%' OR hd.STRAIRSNUMBER LIKE '____139%' OR hd.STRAIRSNUMBER LIKE '____151%' OR hd.STRAIRSNUMBER LIKE '____217%' OR hd.STRAIRSNUMBER LIKE '____223%' OR hd.STRAIRSNUMBER LIKE '____247%' OR hd.STRAIRSNUMBER LIKE '____255%' OR hd.STRAIRSNUMBER LIKE '____297%') ) AS dt_ESList
                        LEFT JOIN (SELECT *
                        FROM APBCONTACTINFORMATION AS ci
                        WHERE ci.STRKEY = 30) AS dt_Contact ON dt_ESList.STRAIRSNUMBER = dt_Contact.STRAIRSNUMBER) AS dt_PermitContact ON dt_ESContact.STRAIRSNUMBER = dt_PermitContact.STRAIRSNUMBER
                        INNER JOIN APBHEADERDATA AS hd ON dt_ESContact.STRAIRSNUMBER = hd.STRAIRSNUMBER
                        INNER JOIN APBFACILITYINFORMATION AS fi ON dt_ESContact.STRAIRSNUMBER = fi.STRAIRSNUMBER"
                    Dim dt As DataTable = DB.GetDataTable(SQL)

                    For Each dr As DataRow In dt.Rows
                        airsNumber = dr("strAirsNumber")
                        airsYear = airsNumber & ESYear
                        ESYear = cboMailoutYear.SelectedItem
                        If IsDBNull(dr("STRFACILITYNAME")) Then
                            FACILITYNAME = " "
                        Else
                            FACILITYNAME = dr("STRFACILITYNAME")
                        End If
                        If IsDBNull(dr("STROPERATIONALSTATUS")) Then
                            OperationalStatus = " "
                        Else
                            OperationalStatus = dr("STROPERATIONALSTATUS")
                        End If
                        If IsDBNull(dr("STRCLASS")) Then
                            FacilityClass = " "
                        Else
                            FacilityClass = dr("STRCLASS")
                        End If
                        If IsDBNull(dr("STRCONTACTCOMPANYNAME")) Then
                            CONTACTCOMPANYNAME = " "
                        Else
                            CONTACTCOMPANYNAME = dr("STRCONTACTCOMPANYNAME")
                        End If
                        If IsDBNull(dr("STRCONTACTADDRESS1")) Then
                            CONTACTADDRESS1 = " "
                        Else
                            CONTACTADDRESS1 = dr("STRCONTACTADDRESS1")
                        End If
                        If IsDBNull(dr("STRCONTACTCITY")) Then
                            CONTACTCITY = " "
                        Else
                            CONTACTCITY = dr("STRCONTACTCITY")
                        End If
                        If IsDBNull(dr("STRCONTACTSTATE")) Then
                            CONTACTSTATE = " "
                        Else
                            CONTACTSTATE = dr("STRCONTACTSTATE")
                        End If
                        If IsDBNull(dr("STRCONTACTZIPCODE")) Then
                            CONTACTZIPCODE = " "
                        Else
                            CONTACTZIPCODE = dr("STRCONTACTZIPCODE")
                        End If
                        If IsDBNull(dr("STRCONTACTFIRSTNAME")) Then
                            CONTACTFIRSTNAME = " "
                        Else
                            CONTACTFIRSTNAME = dr("STRCONTACTFIRSTNAME")
                        End If
                        If IsDBNull(dr("STRCONTACTLASTNAME")) Then
                            CONTACTLASTNAME = " "
                        Else
                            CONTACTLASTNAME = dr("STRCONTACTLASTNAME")
                        End If
                        If IsDBNull(dr("STRCONTACTEMAIL")) Then
                            CONTACTEMAIL = " "
                        Else
                            CONTACTEMAIL = dr("STRCONTACTEMAIL")
                        End If

                        Dim sql2 As String = "insert into ESmailOut " &
                            "(" &
                            "strAirsYear," &
                            "strAirsNumber," &
                            "STRFACILITYNAME," &
                            "STROPERATIONALSTATUS," &
                            "STRCLASS," &
                            "STRCONTACTCOMPANYNAME," &
                            "STRCONTACTADDRESS1," &
                            "STRCONTACTCITY," &
                            "STRCONTACTSTATE," &
                            "STRCONTACTZIPCODE," &
                            "STRCONTACTFIRSTNAME," &
                            "STRCONTACTLASTNAME," &
                            "STRCONTACTEMAIL," &
                            "strESYear" &
                            ") values (" &
                            "@strAirsYear," &
                            "@strAirsNumber," &
                            "@STRFACILITYNAME," &
                            "@STROPERATIONALSTATUS," &
                            "@STRCLASS," &
                            "@STRCONTACTCOMPANYNAME," &
                            "@STRCONTACTADDRESS1," &
                            "@STRCONTACTCITY," &
                            "@STRCONTACTSTATE," &
                            "@STRCONTACTZIPCODE," &
                            "@STRCONTACTFIRSTNAME," &
                            "@STRCONTACTLASTNAME," &
                            "@STRCONTACTEMAIL," &
                            "@strESYear" &
                            ")"
                        Dim params As SqlParameter() = {
                            New SqlParameter("@strAirsYear", airsYear),
                            New SqlParameter("@strAirsNumber", airsNumber),
                            New SqlParameter("@STRFACILITYNAME", FACILITYNAME),
                            New SqlParameter("@STROPERATIONALSTATUS", OperationalStatus),
                            New SqlParameter("@STRCLASS", FacilityClass),
                            New SqlParameter("@STRCONTACTCOMPANYNAME", Replace(CONTACTCOMPANYNAME, "N/A", " ")),
                            New SqlParameter("@STRCONTACTADDRESS1", Replace(CONTACTADDRESS1, "N/A", " ")),
                            New SqlParameter("@STRCONTACTCITY", Replace(CONTACTCITY, "N/A", " ")),
                            New SqlParameter("@STRCONTACTSTATE", CONTACTSTATE),
                            New SqlParameter("@STRCONTACTZIPCODE", Replace(CONTACTZIPCODE, "N/A", " ")),
                            New SqlParameter("@STRCONTACTFIRSTNAME", Replace(CONTACTFIRSTNAME, "N/A", " ")),
                            New SqlParameter("@STRCONTACTLASTNAME", Replace(CONTACTLASTNAME, "N/A", " ")),
                            New SqlParameter("@STRCONTACTEMAIL", Replace(CONTACTEMAIL, "N/A", " ")),
                            New SqlParameter("@strESYear", ESYear)
                        }
                        DB.RunCommand(sql2, params)
                    Next

                    SQL = "SELECT STRAIRSNUMBER, " &
                    "STRFACILITYNAME, " &
                    "STROPERATIONALSTATUS, " &
                    "STRCLASS, " &
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

                    Dim param2 As New SqlParameter("@year", cboMailoutYear.SelectedItem)

                    dgvESDataCount.DataSource = DB.GetDataTable(SQL, param2)

                    dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
                    dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
                    dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
                    dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
                    dgvESDataCount.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status"
                    dgvESDataCount.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
                    dgvESDataCount.Columns("STRCLASS").HeaderText = "Facility Class"
                    dgvESDataCount.Columns("STRCLASS").DisplayIndex = 3
                    dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
                    dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
                    dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
                    dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
                    dgvESDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
                    dgvESDataCount.Columns("STRCONTACTCOMPANYname").DisplayIndex = 6
                    dgvESDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
                    dgvESDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 7
                    dgvESDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
                    dgvESDataCount.Columns("STRCONTACTCITY").DisplayIndex = 8
                    dgvESDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
                    dgvESDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 9
                    dgvESDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
                    dgvESDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
                    dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
                    dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 11

                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub deleteESmailOutbyYear()
        Dim ESyear As String = cboMailoutYear.SelectedItem

        Try
            If ESyear = "Select a Mailout Year & Click Below" Then
                MsgBox("You must select a Mailout Year")
            Else
                Dim SQL As String = "delete from ESmailout " &
                "where strESyear = @strESyear "
                Dim param As New SqlParameter("@strESyear", ESyear)
                DB.RunCommand(SQL, param)
                MsgBox("ES mail out is deleted")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnGenMailOut_Click_1(sender As Object, e As EventArgs) Handles btnGenMailOut.Click
        Try
            GenerateESMailOut()
            cboMailoutYear.Items.Clear()
            LoadMailOutYear()
            cboMailoutYear.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnDelMailOut_Click_1(sender As Object, e As EventArgs) Handles btnDelMailOut.Click
        Try
            deleteESmailOutbyYear()
            cboMailoutYear.Items.Clear()
            LoadMailOutYear()
            cboMailoutYear.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblviewselectedyearMailoutList_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblviewselectedyearMailoutList.LinkClicked
        Try
            Dim year As String = cboMailoutYear.SelectedItem

            Dim SQL As String = "SELECT STRAIRSNUMBER, " &
            "STRFACILITYNAME, " &
            "STROPERATIONALSTATUS, " &
            "STRCLASS, " &
            "STRCONTACTFIRSTNAME, " &
            "STRCONTACTLASTNAME, " &
            "STRCONTACTCOMPANYNAME, " &
            "STRCONTACTADDRESS1, " &
            "STRCONTACTCITY, " &
            "STRCONTACTSTATE, " &
            "STRCONTACTZIPCODE, " &
            "STRCONTACTEMAIL " &
            "from esMailOut " &
            "where STRESYEAR = @STRESYEAR " &
            "order by STRFACILITYNAME"

            Dim param As New SqlParameter("@STRESYEAR", year)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status"
            dgvESDataCount.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
            dgvESDataCount.Columns("STRCLASS").HeaderText = "Facility Class"
            dgvESDataCount.Columns("STRCLASS").DisplayIndex = 3
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvESDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTCOMPANYname").DisplayIndex = 6
            dgvESDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
            dgvESDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 7
            dgvESDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvESDataCount.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvESDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvESDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvESDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvESDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 11

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadESEnrollmentYear()
        'Load MailOut Year dropdown boxes
        Dim SQL As String = "Select distinct STRESYEAR " &
                  "from ESMAILOUT  " &
                  "order by STRESYEAR desc"
        Dim dt As DataTable = DB.GetDataTable(SQL)

        For Each dr As DataRow In dt.Rows
            cboESYear.Items.Add(dr("STRESYEAR"))
        Next

        cboESYear.SelectedIndex = 0
    End Sub

    Private Sub btnESenrollment_Click(sender As Object, e As EventArgs) Handles btnESenrollment.Click
        Try
            If cboESYear.Text = "" Then
                MsgBox("Please choose a year", MsgBoxStyle.Information, "ES Enrollment")
            Else
                ESMailoutenrollment()
                cboESYear.Items.Clear()
                LoadESEnrollmentYear()
                cboESYear.Text = ""
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ESMailoutenrollment()
        Dim AirsNo As String
        Dim FacilityName As String
        Dim ESYear As Integer = CInt(cboESYear.SelectedItem)
        Dim airsYear As String

        Try
            Dim SQL As String = "Select * " &
            "FROM ESSCHEMA " &
            "where INTESYEAR = @ESYear "
            Dim param As New SqlParameter("@ESYear", ESYear)

            If DB.ValueExists(SQL, param) Then
                MsgBox("The year " & ESYear & " is already enrolled.", MsgBoxStyle.Information, "EI Enrollment")
            Else
                SQL = "Select ESMAILOUT.STRAIRSNUMBER, ESMAILOUT.STRFACILITYNAME " &
                "FROM ESMAILOUT " &
                "where STRESYEAR = @ESYear "
                Dim dt As DataTable = DB.GetDataTable(SQL, param)

                For Each dr As DataRow In dt.Rows
                    AirsNo = dr("strAirsNumber")
                    airsYear = AirsNo & ESYear
                    FacilityName = dr("STRFACILITYNAME")

                    Dim SQL2 As String = "Insert into ESSCHEMA " &
                    "(ESSCHEMA.STRAIRSNUMBER, " &
                    "ESSCHEMA.STRFACILITYNAME, " &
                    "ESSCHEMA.DATTRANSACTION, " &
                    "ESSCHEMA.INTESYEAR, " &
                    "ESSCHEMA.NUMUSERID, " &
                    "ESSCHEMA.STRAIRSYEAR) " &
                    "values (" &
                    "@STRAIRSNUMBER, " &
                    "@STRFACILITYNAME, " &
                    " GETDATE() , " &
                    "@INTESYEAR, " &
                    "'3', " &
                    "@STRAIRSYEAR) "

                    Dim params As SqlParameter() = {
                        New SqlParameter("@STRAIRSNUMBER", AirsNo),
                        New SqlParameter("@STRFACILITYNAME", FacilityName),
                        New SqlParameter("@INTESYEAR", ESYear),
                        New SqlParameter("@STRAIRSYEAR", airsYear)
                    }

                    DB.RunCommand(SQL2, params)
                Next

                MsgBox("The facilities for year " & ESYear & " have been enrolled", MsgBoxStyle.Information, "EI Enrollment")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnESdeenrollment_Click(sender As Object, e As EventArgs) Handles btnESdeenrollment.Click
        Dim ESYear As Integer = CInt(cboESYear.SelectedItem)
        Dim SQL As String
        Try
            If cboESYear.Text = "" Then
                MsgBox("Please choose a year", MsgBoxStyle.Information, "ES Enrollment")
            Else
                Dim intAnswer As DialogResult
                intAnswer = MessageBox.Show("Remove the enrollment?", "ES Enrollment", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)

                Select Case intAnswer
                    Case DialogResult.OK
                        SQL = "delete from ESSCHEMA " &
                            "where ESSCHEMA.INTESYEAR = @ESYear "
                        Dim param As New SqlParameter("@ESYear", ESYear)

                        DB.RunCommand(SQL, param)

                        MsgBox("Enrollment has been removed", MsgBoxStyle.Information, "ES Enrollment")
                    Case Else
                        MsgBox("Enrollment has not been removed", MsgBoxStyle.Information, "ES Enrollment")
                End Select

                cboESYear.Items.Clear()
                LoadESEnrollmentYear()
                cboESYear.Text = ""
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnaddfacilitytoES_Click(sender As Object, e As EventArgs) Handles btnaddfacilitytoES.Click
        If String.IsNullOrWhiteSpace(txtESairNumber.Text) OrElse Not Integer.TryParse(txtESYearforFacility.Text, Nothing) Then
            MsgBox("Enter both an AIRS number and a year.")
            Return
        End If

        Dim AirsNo As String = "0413" & txtESairNumber.Text
        Dim ESYear As Integer = CInt(txtESYearforFacility.Text)
        Dim airsYear As String = AirsNo & ESYear
        Dim facilityName As String

        Try
            Dim SQL As String = "Select INTESYEAR " &
                "FROM ESSCHEMA " &
                "where INTESYEAR = @ESYear "
            Dim param As New SqlParameter("@ESYear", ESYear)

            If DB.ValueExists(SQL, param) Then
                SQL = "Select STRFACILITYNAME " &
                    "FROM APBFACILITYINFORMATION " &
                    "where STRAIRSNUMBER = @AirsNo "
                Dim param2 As New SqlParameter("@AirsNo ", AirsNo)
                facilityName = DB.GetString(SQL, param2)

                If facilityName <> "" Then
                    SQL = "Select * " &
                        "FROM ESSCHEMA " &
                        "where INTESYEAR = @ESYear " &
                        " And STRAIRSNUMBER = @AirsNo "
                    Dim param3 As SqlParameter() = {
                        param,
                        New SqlParameter("@AirsNo", AirsNo)
                    }

                    If DB.ValueExists(SQL, param3) Then
                        MsgBox("This facility (" & AirsNo & ") is already enrolled for " & ESYear & ".", MsgBoxStyle.Information, "ES Enrollment")
                    Else
                        SQL = "Insert into ESSCHEMA " &
                        "(STRAIRSNUMBER, " &
                        "STRFACILITYNAME, " &
                        "DATTRANSACTION, " &
                        "INTESYEAR, " &
                        "NUMUSERID, " &
                        "STRAIRSYEAR) " &
                        "VALUES " &
                        "(@STRAIRSNUMBER, " &
                        "@STRFACILITYNAME, " &
                        "getdate(), " &
                        "@INTESYEAR, " &
                        "'3', " &
                        "@STRAIRSYEAR) "

                        Dim param4 As SqlParameter() = {
                            New SqlParameter("@STRAIRSNUMBER", AirsNo),
                            New SqlParameter("@STRFACILITYNAME", facilityName),
                            New SqlParameter("@INTESYEAR", ESYear),
                            New SqlParameter("@STRAIRSYEAR", airsYear)
                        }

                        DB.RunCommand(SQL, param4)

                        MsgBox("This facility (" & AirsNo & ") has been enrolled for " & ESYear & ".", MsgBoxStyle.Information, "ES Enrollment")
                    End If

                Else
                    MsgBox("This Airs Number (" & AirsNo & ") is not valid. Please enter valid Airs Number.", MsgBoxStyle.Information, "ES Enrollment")
                End If
            Else
                MsgBox("This year (" & ESYear & ") has not been enrolled.", MsgBoxStyle.Information, "ES Enrollment")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnremoveFacilityES_Click(sender As Object, e As EventArgs) Handles btnremoveFacilityES.Click
        If String.IsNullOrWhiteSpace(txtESairNumber.Text) OrElse Not Integer.TryParse(txtESYearforFacility.Text, Nothing) Then
            MsgBox("Enter both an AIRS number and a year.")
            Return
        End If

        Dim ESYear As Integer = CInt(txtESYearforFacility.Text)
        Dim AirsNo As String = "0413" & txtESairNumber.Text

        Try
            Dim intAnswer As DialogResult
            intAnswer = MessageBox.Show("Remove this facility (" & AirsNo & ") for " & ESYear & "?", "ES Enrollment", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

            Select Case intAnswer
                Case DialogResult.Yes
                    Dim SQL As String = "delete from ESSCHEMA " &
                        "where ESSCHEMA.INTESYEAR = @ESYear " &
                        " And ESSCHEMA.STRAIRSNUMBER = @AirsNo "
                    Dim param As SqlParameter() = {
                        New SqlParameter("@ESYear", ESYear),
                        New SqlParameter("@AirsNo", AirsNo)
                    }
                    DB.RunCommand(SQL, param)

                    MsgBox("This Facility (" & AirsNo & ") has been removed for " & ESYear & ".", MsgBoxStyle.Information, "ES Enrollment")
                Case Else
                    MsgBox("This Facility (" & AirsNo & ") has not been removed for " & ESYear & ".", MsgBoxStyle.Information, "ES Enrollment")
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCheckESstatus_Click(sender As Object, e As EventArgs) Handles btnCheckESstatus.Click
        If String.IsNullOrWhiteSpace(txtESairNumber.Text) OrElse Not Integer.TryParse(txtESYearforFacility.Text, Nothing) Then
            MsgBox("Enter both an AIRS number and a year.")
            Return
        End If

        Dim AirsNo As String = "0413" & txtESairNumber.Text
        Dim ESYear As Integer = CInt(txtESYearforFacility.Text)

        Try
            Dim SQL As String = "Select strAIRSYear " &
                "FROM ESSCHEMA " &
                "where ESSCHEMA.INTESYEAR = @ESYear " &
                " And ESSCHEMA.STRAIRSNUMBER = @AirsNo "

            Dim param As SqlParameter() = {
                New SqlParameter("@ESYear", ESYear),
                New SqlParameter("@AirsNo", AirsNo)
            }

            If DB.ValueExists(SQL, param) Then
                MsgBox("This facility (" & AirsNo & ") has been enrolled for " & ESYear & ".", MsgBoxStyle.Information, "ES Enrollment")
            Else
                MsgBox("This facility (" & AirsNo & ") is not enrolled for " & ESYear & ".", MsgBoxStyle.Information, "ES Enrollment")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblviewESenrollment_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblviewESenrollment.LinkClicked
        Try
            Dim year As String = cboESYear.Text

            If cboESYear.Text = "" Then
                MsgBox("Please choose a year to view", MsgBoxStyle.Information, "ES Enrollment")
            Else
                Dim SQL As String = "SELECT ESSCHEMA.STRAIRSNUMBER, " &
                    "ESSCHEMA.STRFACILITYNAME, " &
                    "ESSCHEMA.DATTRANSACTION " &
                    "from ESSCHEMA " &
                    "where ESSCHEMA.INTESYEAR = @year "
                Dim param As New SqlParameter("@year", year)

                dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

                dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
                dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
                dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
                dgvESDataCount.Columns("DATTRANSACTION").HeaderText = "Transaction Date"
                dgvESDataCount.Columns("DATTRANSACTION").DisplayIndex = 2

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblviewESextraresponder_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblviewESextraresponder.LinkClicked
        Try
            Dim SQL As String = "SELECT es.STRAIRSNUMBER " &
                ", es.STRFACILITYNAME " &
                ", es.STRCONTACTFIRSTNAME " &
                ", es.STRCONTACTLASTNAME " &
                ", es.STRCONTACTCOMPANY " &
                ", es.STRCONTACTEMAIL " &
                ", es.STRCONTACTPHONENUMBER " &
                "FROM " &
                "  (SELECT es.STRAIRSNUMBER AS SchemaAIRS " &
                "  , em.STRAIRSNUMBER       AS MailoutAIRS " &
                "  FROM ESMailout em " &
                "  RIGHT JOIN ESSCHEMA es " &
                "  ON es.STRAIRSYEAR  = em.STRAIRSYEAR " &
                "  WHERE es.INTESYEAR = @year " &
                "  AND es.STROPTOUT  IS NOT NULL " &
                "  ) dt_NotInMailout " &
                "INNER JOIN ESSCHEMA es " &
                "ON dt_NotInMailout.SchemaAIRS      = es.STRAIRSNUMBER " &
                "WHERE dt_NotInMailout.MailoutAIRS IS NULL"
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

End Class