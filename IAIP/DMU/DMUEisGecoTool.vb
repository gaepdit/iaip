Imports System.Data.SqlClient
Imports Iaip.Apb.Facilities
Imports EpdIt
Imports System.Linq
Imports System.Collections.Generic

Public Class DMUEisGecoTool

    Private Sub DMUEisGecoTool_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadPermissions()
        loadYear()
        loadMailOutYear()
        loadESEnrollmentYear()
        loadcboEISstatusCodes()
        FormatWebUsers()
        LoadEISLog()
        LoadStats()
        LoadEISYear()
        LoadOperStatus()
    End Sub

#Region "Page Load"

    Private Sub LoadOperStatus()
        cbIaipOperStatus.BindToDictionary(FacilityOperationalStatusDescriptions)
        cbEisModifyOperStatus.BindToDictionary(EisSiteStatusCodeDescriptions)
    End Sub

    Private Sub LoadPermissions()
        If AccountFormAccess(130, 3) <> "1" And AccountFormAccess(130, 4) <> "1" Then
            TCDMUTools.TabPages.Remove(TPESTools)
            TCDMUTools.TabPages.Remove(TPFeeTools)
        End If
    End Sub

    Private Sub loadYear()
        Dim SQL As String = "Select " &
        "distinct intESYear " &
        "from esschema " &
        "order by intESYear desc"
        Dim dt As DataTable = DB.GetDataTable(SQL)

        For Each dr As DataRow In dt.Rows
            cboYear.Items.Add(dr("intESYear"))
        Next

        cboYear.SelectedIndex = 0
    End Sub

    Private Sub loadMailOutYear()
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

    Private Sub FormatWebUsers()
        Try
            dgvUsers.RowHeadersVisible = False
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvUsers.AllowUserToResizeColumns = True
            dgvUsers.AllowUserToAddRows = False
            dgvUsers.AllowUserToDeleteRows = False
            dgvUsers.AllowUserToOrderColumns = True
            dgvUsers.AllowUserToResizeRows = True
            dgvUsers.ColumnHeadersHeight = "35"

            dgvUsers.Columns.Add("ID", "ID")
            dgvUsers.Columns("ID").DisplayIndex = 0
            dgvUsers.Columns("ID").Visible = False

            dgvUsers.Columns.Add("numuserid", "UserID")
            dgvUsers.Columns("numuserid").DisplayIndex = 1
            dgvUsers.Columns("numuserid").Visible = False

            dgvUsers.Columns.Add("Email", "Email Address")
            dgvUsers.Columns("Email").DisplayIndex = 2
            dgvUsers.Columns("Email").Width = 250
            dgvUsers.Columns("Email").ReadOnly = True

            Dim colReadOnly As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colReadOnly)
            dgvUsers.Columns(3).HeaderText = "Admin Access"

            Dim colWrite As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colWrite)
            dgvUsers.Columns(4).HeaderText = "Fee Access"

            Dim colFullAccess As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colFullAccess)
            dgvUsers.Columns(5).HeaderText = "EI Access"

            Dim colSpecialPermissions As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colSpecialPermissions)
            dgvUsers.Columns(6).HeaderText = "ES Access"


            dgvUserFacilities.RowHeadersVisible = False
            dgvUserFacilities.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvUserFacilities.AllowUserToResizeColumns = True
            dgvUserFacilities.AllowUserToAddRows = False
            dgvUserFacilities.AllowUserToDeleteRows = False
            dgvUserFacilities.AllowUserToOrderColumns = True
            dgvUserFacilities.AllowUserToResizeRows = True
            dgvUserFacilities.ColumnHeadersHeight = "35"

            dgvUserFacilities.Columns.Add("strAIRSNumber", "AIRS Number")
            dgvUserFacilities.Columns("strAIRSNumber").DisplayIndex = 0
            dgvUserFacilities.Columns("strAIRSNumber").Visible = True

            dgvUserFacilities.Columns.Add("strFacilityName", "Facility Name")
            dgvUserFacilities.Columns("strFacilityName").DisplayIndex = 1
            dgvUserFacilities.Columns("strFacilityName").Width = 250

            Dim colReadOnly2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colReadOnly2)
            dgvUserFacilities.Columns(2).HeaderText = "Admin Access"

            Dim colWrite2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colWrite2)
            dgvUserFacilities.Columns(3).HeaderText = "Fee Access"

            Dim colFullAccess2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colFullAccess2)
            dgvUserFacilities.Columns(4).HeaderText = "EI Access"

            Dim colSpecialPermissions2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colSpecialPermissions2)
            dgvUserFacilities.Columns(5).HeaderText = "ES Access"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadEISLog()
        Dim SQL As String = "Select distinct(inventoryYear) as InvYear " &
            "from EIS_Admin " &
            "order by invYear desc "
        Dim dt As DataTable = DB.GetDataTable(SQL)

        For Each dr As DataRow In dt.Rows
            cboEILogYear.Items.Add(dr.Item("InvYear"))
            cboEISStatisticsYear.Items.Add(dr.Item("InvYear"))
        Next

        SQL = "select distinct strDMUResponsibleStaff as DMUStafff " &
            "from EIS_QAAdmin " &
            "union " &
            "select distinct (strLastName +', '+ strFirstName) as DMUStafff " &
            "from EPDUserProfiles " &
            "where numBranch = '1' " &
            "and numProgram = '3' " &
            "and numunit = '14' " &
            "and numEmployeeStatus = '1' "
        dt = DB.GetDataTable(SQL)

        For Each dr As DataRow In dt.Rows
            cboEISQAStaff.Items.Add(dr.Item("DMUStafff"))
        Next

        SQL = "Select " &
            " '' as QAStatusCode, '' as strDesc " &
            " union Select " &
            "QAStatusCode, strDesc " &
            "From EISLK_QAStatus " &
            "Where active = '1' "
        Dim dtQAStatus As DataTable = DB.GetDataTable(SQL)

        With cboEISQAStatus
            .DataSource = dtQAStatus
            .DisplayMember = "strDesc"
            .ValueMember = "QAStatusCode"
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub LoadStats()
        dgvEISStats.RowHeadersVisible = False
        dgvEISStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvEISStats.AllowUserToResizeColumns = True
        dgvEISStats.AllowUserToAddRows = False
        dgvEISStats.AllowUserToDeleteRows = False
        dgvEISStats.AllowUserToOrderColumns = True
        dgvEISStats.AllowUserToResizeRows = True
        dgvEISStats.ColumnHeadersHeight = "35"

        Dim colSelect As New DataGridViewCheckBoxColumn
        dgvEISStats.Columns.Add(colSelect)
        dgvEISStats.Columns(0).HeaderText = " "
        dgvEISStats.Columns(0).Width = 50

        dgvEISStats.Columns.Add("FacilitySiteID", "AIRS No.")
        dgvEISStats.Columns("FacilitySiteID").DisplayIndex = 1
        dgvEISStats.Columns("FacilitySiteID").Visible = True

        dgvEISStats.Columns.Add("strFacilityName", "Facility Name")
        dgvEISStats.Columns("strFacilityName").DisplayIndex = 2
        dgvEISStats.Columns("strFacilityName").Width = 250
        dgvEISStats.Columns("strFacilityName").ReadOnly = True

        dgvEISStats.Columns.Add("InventoryYear", "EIS Year")
        dgvEISStats.Columns("InventoryYear").DisplayIndex = 3
        dgvEISStats.Columns("InventoryYear").Visible = True

        dgvEISStats.Columns.Add("EISStatus", "EIS Status")
        dgvEISStats.Columns("EISStatus").DisplayIndex = 4
        dgvEISStats.Columns("EISStatus").Visible = True

        dgvEISStats.Columns.Add("EISAccess", "EIS Access")
        dgvEISStats.Columns("EISAccess").DisplayIndex = 5
        dgvEISStats.Columns("EISAccess").Visible = True

        dgvEISStats.Columns.Add("OptOut", "Opt Out")
        dgvEISStats.Columns("OptOut").DisplayIndex = 6
        dgvEISStats.Columns("OptOut").Visible = True

        dgvEISStats.Columns.Add("MailOut", "Mailout")
        dgvEISStats.Columns("MailOut").DisplayIndex = 7
        dgvEISStats.Columns("MailOut").Visible = True

        dgvEISStats.Columns.Add("MailoutEmail", "Mailout Email")
        dgvEISStats.Columns("MailoutEmail").DisplayIndex = 8
        dgvEISStats.Columns("MailoutEmail").Visible = True

        dgvEISStats.Columns.Add("strDMUResponsibleStaff", "QA Reviewer")
        dgvEISStats.Columns("strDMUResponsibleStaff").DisplayIndex = 9
        dgvEISStats.Columns("strDMUResponsibleStaff").Visible = True

        dgvEISStats.Columns.Add("Enrollment", "Enrollment")
        dgvEISStats.Columns("Enrollment").DisplayIndex = 10
        dgvEISStats.Columns("Enrollment").Visible = True

        dgvEISStats.Columns.Add("QASTATUS", "QA Status")
        dgvEISStats.Columns("QASTATUS").DisplayIndex = 11
        dgvEISStats.Columns("QASTATUS").Visible = True

        dgvEISStats.Columns.Add("datQAStatus", "QA Status Data")
        dgvEISStats.Columns("datQAStatus").DisplayIndex = 12
        dgvEISStats.Columns("datQAStatus").Visible = True
        dgvEISStats.Columns("datQAStatus").DefaultCellStyle.Format = "dd-MMM-yyyy"


        dgvEISStats.Columns.Add("IAIPPrefix", "IAIP Prefix")
        dgvEISStats.Columns("IAIPPrefix").DisplayIndex = 13
        dgvEISStats.Columns("IAIPPrefix").Visible = True

        dgvEISStats.Columns.Add("IAIPFIRSTNAME", "IAIP First Name")
        dgvEISStats.Columns("IAIPFIRSTNAME").DisplayIndex = 14
        dgvEISStats.Columns("IAIPFIRSTNAME").Visible = True


        dgvEISStats.Columns.Add("IAIPLASTNAME", "IAIP Last Name")
        dgvEISStats.Columns("IAIPLASTNAME").DisplayIndex = 15
        dgvEISStats.Columns("IAIPLASTNAME").Visible = True

        dgvEISStats.Columns.Add("IAIPEMAIL", "IAIP Email")
        dgvEISStats.Columns("IAIPEMAIL").DisplayIndex = 16
        dgvEISStats.Columns("IAIPEMAIL").Visible = True

        dgvEISStats.Columns.Add("EISCOMPANYNAME", "Contact Co. Name")
        dgvEISStats.Columns("EISCOMPANYNAME").DisplayIndex = 17
        dgvEISStats.Columns("EISCOMPANYNAME").Visible = True

        dgvEISStats.Columns.Add("EISADDRESS", "Contact Address")
        dgvEISStats.Columns("EISADDRESS").DisplayIndex = 18
        dgvEISStats.Columns("EISADDRESS").Visible = True

        dgvEISStats.Columns.Add("EISADDRESS2", "Contact Address 2")
        dgvEISStats.Columns("EISADDRESS2").DisplayIndex = 19
        dgvEISStats.Columns("EISADDRESS2").Visible = True

        dgvEISStats.Columns.Add("EISCITY", "Contact City")
        dgvEISStats.Columns("EISCITY").DisplayIndex = 20
        dgvEISStats.Columns("EISCITY").Visible = True

        dgvEISStats.Columns.Add("EISState", "Contact State")
        dgvEISStats.Columns("EISState").DisplayIndex = 21
        dgvEISStats.Columns("EISState").Visible = True

        dgvEISStats.Columns.Add("EISZipCode", "Contact Zip Code")
        dgvEISStats.Columns("EISZipCode").DisplayIndex = 22
        dgvEISStats.Columns("EISZipCode").Visible = True

        dgvEISStats.Columns.Add("EISPrefix", "Contact Prefix")
        dgvEISStats.Columns("EISPrefix").DisplayIndex = 23
        dgvEISStats.Columns("EISPrefix").Visible = True

        dgvEISStats.Columns.Add("EISFirstname", "Contact First Name")
        dgvEISStats.Columns("EISFirstname").DisplayIndex = 24
        dgvEISStats.Columns("EISFirstname").Visible = True

        dgvEISStats.Columns.Add("EISLastName", "Contact Last Name")
        dgvEISStats.Columns("EISLastName").DisplayIndex = 25
        dgvEISStats.Columns("EISLastName").Visible = True

        dgvEISStats.Columns.Add("DATFINALIZE", "Date Submitted")
        dgvEISStats.Columns("DATFINALIZE").DisplayIndex = 26
        dgvEISStats.Columns("DATFINALIZE").Visible = True

        dgvEISStats.Columns.Add("FITrackingNumber", "FI Tracking Number")
        dgvEISStats.Columns("FITrackingNumber").DisplayIndex = 27
        dgvEISStats.Columns("FITrackingNumber").Visible = True

        dgvEISStats.Columns.Add("PointTrackingNumber", "Point Tracking Number")
        dgvEISStats.Columns("PointTrackingNumber").DisplayIndex = 28
        dgvEISStats.Columns("PointTrackingNumber").Visible = True

        dgvEISStats.Columns.Add("Comments", "Comments")
        dgvEISStats.Columns("Comments").DisplayIndex = 29
        dgvEISStats.Columns("Comments").Visible = True
    End Sub

#End Region

#Region "Mahesh Code for Web App Users"

    Private Sub LoadUserInfo(UserData As String)
        Try
            Dim SQL As String = "Select " &
            "OLAPUserProfile.numUserID, " &
            "strfirstname, strlastname, " &
            "strtitle, strcompanyname, " &
            "straddress, strcity, " &
            "strstate, strzip, " &
            "strphonenumber, strfaxnumber, " &
            "datLastLogIn, strConfirm, " &
            "strUserEmail " &
            "from OlapUserProfile, OLAPUserLogIn " &
            "where OLAPUserProfile.numUserID = OLAPUserLogIn.numuserid " &
            "and strUserEmail = @strUserEmail "
            Dim param As New SqlParameter("@strUserEmail", UserData)
            Dim dr As DataRow = DB.GetDataRow(SQL, param)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("numUserID")) Then
                    txtWebUserID.Clear()
                Else
                    txtWebUserID.Text = dr.Item("numUserID")
                End If
                If IsDBNull(dr.Item("strfirstname")) Then
                    txtEditFirstName.Clear()
                Else
                    lblFName.Text = "First Name: " & dr.Item("strfirstname")
                    txtEditFirstName.Text = dr.Item("strFirstName")
                End If
                If IsDBNull(dr.Item("strlastname")) Then
                    txtEditLastName.Clear()
                Else
                    lblLName.Text = "Last Name: " & dr.Item("strlastname")
                    txtEditLastName.Text = dr.Item("strLastName")
                End If
                If IsDBNull(dr.Item("strtitle")) Then
                    txtEditTitle.Clear()
                Else
                    lblTitle.Text = "Title: " & dr.Item("strtitle")
                    txtEditTitle.Text = dr.Item("strTitle")
                End If
                If IsDBNull(dr.Item("strcompanyname")) Then
                    txtEditCompany.Clear()
                Else
                    lblCoName.Text = "Company Name: " & dr.Item("strcompanyname")
                    txtEditCompany.Text = dr.Item("strCompanyName")
                End If
                If IsDBNull(dr.Item("straddress")) Then
                    txtEditAddress.Clear()
                Else
                    lblAddress.Text = dr.Item("straddress")
                    txtEditAddress.Text = dr.Item("strAddress")
                End If
                If IsDBNull(dr.Item("strcity")) Then
                    txtEditCity.Clear()
                    mtbEditState.Clear()
                    mtbEditZipCode.Clear()
                Else
                    lblCityStateZip.Text = dr.Item("strcity") & ", " & dr.Item("strstate") & " " & dr.Item("strzip")
                    txtEditCity.Text = dr.Item("strCity")
                    mtbEditState.Text = dr.Item("strState")
                    mtbEditZipCode.Text = dr.Item("strZip")
                End If
                If IsDBNull(dr.Item("strphonenumber")) Then
                    mtbEditPhoneNumber.Clear()
                Else
                    lblPhoneNo.Text = "Phone Number: " & dr.Item("strphonenumber")
                    mtbEditPhoneNumber.Text = dr.Item("strPhoneNumber")
                End If
                If IsDBNull(dr.Item("strfaxnumber")) Then
                    mtbEditFaxNumber.Clear()
                Else
                    lblFaxNo.Text = "Fax Number: " & dr.Item("strfaxnumber")
                    mtbEditFaxNumber.Text = dr.Item("strFaxNumber")
                End If
                If IsDBNull(dr.Item("strConfirm")) Then
                    lblConfirmDate.Text = ""
                Else
                    lblConfirmDate.Text = "Date User Email Confirmed: " & dr.Item("strConfirm")
                End If
                If IsDBNull(dr.Item("datLastLogIn")) Then
                    lblLastLogIn.Text = ""
                Else
                    lblLastLogIn.Text = "Date User Last Logged In: " & dr.Item("datLastLogIn")
                End If
                If IsDBNull(dr.Item("strUserEmail")) Then
                    txtEditEmail.Text = ""
                Else
                    txtEditEmail.Text = dr.Item("strUserEmail")
                End If
            Else
                txtWebUserID.Clear()
                txtEditFirstName.Clear()
                txtEditLastName.Clear()
                txtEditTitle.Clear()
                txtEditCompany.Clear()
                txtEditAddress.Clear()
                txtEditCity.Clear()
                mtbEditState.Clear()
                mtbEditZipCode.Clear()
                mtbEditPhoneNumber.Clear()
                mtbEditFaxNumber.Clear()
                lblLastLogIn.Text = ""
                lblConfirmDate.Text = ""
                txtEditEmail.Clear()
            End If

            txtEditUserPassword.Clear()
            txtEditFirstName.Visible = False
            txtEditLastName.Visible = False
            txtEditTitle.Visible = False
            txtEditCompany.Visible = False
            txtEditAddress.Visible = False
            txtEditCity.Visible = False
            mtbEditState.Visible = False
            mtbEditZipCode.Visible = False
            mtbEditPhoneNumber.Visible = False
            mtbEditFaxNumber.Visible = False
            btnSaveEditedData.Visible = False
            txtEditUserPassword.Visible = False
            btnChangeEmailAddress.Visible = False
            txtEditEmail.Visible = False
            btnUpdatePassword.Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadUserFacilityInfo(EmailLoc As String)
        Try
            Dim dgvRow As New DataGridViewRow

            Dim SQL As String = "SELECT right(strairsnumber, 8) as strAIRSNumber, strfacilityname, " &
             "Case When intAdminAccess = 0 Then 'False' When intAdminAccess = 1 Then 'True' End as intAdminAccess, " &
             "Case When intFeeAccess = 0 Then 'False' When intFeeAccess = 1 Then 'True' End as intFeeAccess, " &
             "Case When intEIAccess = 0 Then 'False' When intEIAccess = 1 Then 'True' End as intEIAccess, " &
             "Case When intESAccess = 0 Then 'False' When intESAccess = 1 Then 'True' End as intESAccess " &
             "FROM OlapUserAccess, OLAPUserLogIn  " &
             "WHERE OlapUserAccess.numUserId = OLAPUserLogIn.numUserId " &
             "and  strUserEmail = @strUserEmail " &
             "order by strfacilityname"
            Dim param As New SqlParameter("@strUserEmail", EmailLoc)

            dgvUserFacilities.Rows.Clear()

            Dim dt As DataTable = DB.GetDataTable(SQL, param)

            For Each dr As DataRow In dt.Rows
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvUserFacilities)
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("strFacilityName")
                End If

                If IsDBNull(dr.Item("intAdminAccess")) Then
                    dgvRow.Cells(2).Value = False
                Else
                    dgvRow.Cells(2).Value = dr.Item("intAdminAccess")
                End If
                If IsDBNull(dr.Item("intFeeAccess")) Then
                    dgvRow.Cells(3).Value = False
                Else
                    dgvRow.Cells(3).Value = dr.Item("intFeeAccess")
                End If

                If IsDBNull(dr.Item("intEIAccess")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("intEIAccess")
                End If
                If IsDBNull(dr.Item("intESAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("intESAccess")
                End If
                dgvUserFacilities.Rows.Add(dgvRow)
            Next

            cboFacilityToDelete.DataSource = dt
            cboFacilityToDelete.DisplayMember = "strfacilityname"
            cboFacilityToDelete.ValueMember = "strairsnumber"
            cboFacilityToDelete.Text = ""

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "ES Tool"

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        runcount()
        lblYear.Text = cboYear.SelectedItem
    End Sub

    Private Sub runcount()
        Dim year As Integer = CInt(cboYear.SelectedItem)
        txtESYear.Text = year.ToString

        Dim deadline As New Date(year + 1, 6, 15)
        Dim SQL As String
        Dim param As New SqlParameter("@year", year.ToString)
        Dim params As SqlParameter() = {
            param,
            New SqlParameter("@deadline", deadline)
        }

        Try
            Try
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

            Catch ex As Exception
                MsgBox("That Prefix is not in the db" + vbCrLf + ex.ToString())
            End Try


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
            Dim extracount As Integer = DB.GetInteger(SQL, param).ToString
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
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub findESMailOut()
        Dim AirsNo As String = txtESAIRSNo2.Text
        Dim ESyear As String = cboYear.SelectedItem

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

    Private Sub findESData()
        Dim AirsNo As String = txtESAirsNo.Text
        Dim ESyear As String = cboYear.SelectedItem
        Dim intESyear As Integer = CInt(ESyear)

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

    Private Sub ExportEStoExcel()
        dgvESDataCount.ExportToExcel(Me)
    End Sub

    Private Sub dgvESDataCount_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvESDataCount.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvESDataCount.HitTest(e.X, e.Y)

        Try
            If dgvESDataCount.RowCount > 0 Then
                If dgvESDataCount.ColumnCount > 2 Then
                    If dgvESDataCount.RowCount > 0 And hti.RowIndex <> -1 Then
                        If dgvESDataCount.Columns(0).HeaderText = "Airs No." Then
                            If IsDBNull(dgvESDataCount(0, hti.RowIndex).Value) Then

                            Else
                                ClearMailOut()
                                txtESAIRSNo2.Text = dgvESDataCount(0, hti.RowIndex).Value
                                txtESFacilityName.Text = dgvESDataCount(1, hti.RowIndex).Value
                                findESMailOut()
                            End If
                        Else
                            If dgvESDataCount.Columns(0).HeaderText = "Airs No." Then
                                If IsDBNull(dgvESDataCount(0, hti.RowIndex).Value) Then
                                    txtESAIRSNo2.Text = dgvESDataCount(0, hti.RowIndex).Value
                                    txtESFacilityName.Text = dgvESDataCount(1, hti.RowIndex).Value
                                Else
                                    ClearMailOut()
                                    findESMailOut()
                                    txtESAIRSNo2.Text = dgvESDataCount(0, hti.RowIndex).Value
                                End If
                            End If
                        End If
                        If dgvESDataCount.Columns(3).HeaderText = "Confirmation Number" Then
                            If IsDBNull(dgvESDataCount(3, hti.RowIndex).Value) Then
                                txtESAirsNo.Text = dgvESDataCount(0, hti.RowIndex).Value
                                txtFACILITYNAME.Text = dgvESDataCount(1, hti.RowIndex).Value
                            Else
                                ClearMailOut()
                                txtESAirsNo.Text = dgvESDataCount(0, hti.RowIndex).Value
                                txtFACILITYNAME.Text = dgvESDataCount(1, hti.RowIndex).Value
                                txtConfirmationNumber.Text = dgvESDataCount(3, hti.RowIndex).Value
                                findESData()
                            End If
                        Else
                            If dgvESDataCount.Columns(3).HeaderText = "Confirmation Number" Then
                                If IsDBNull(dgvESDataCount(3, hti.RowIndex).Value) Then
                                    txtESAirsNo.Text = dgvESDataCount(0, hti.RowIndex).Value
                                    txtFACILITYNAME.Text = dgvESDataCount(1, hti.RowIndex).Value
                                Else
                                    ClearMailOut()
                                    txtESAirsNo.Text = dgvESDataCount(0, hti.RowIndex).Value
                                    txtFACILITYNAME.Text = dgvESDataCount(1, hti.RowIndex).Value
                                    findESData()
                                    txtConfirmationNumber.Text = dgvESDataCount(3, hti.RowIndex).Value
                                End If
                            End If
                        End If
                    End If
                Else
                    ClearMailOut()
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
            Dim param As New SqlParameter("@year", lblYear.Text)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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
            Dim param As New SqlParameter("@year", lblYear.Text)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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
            dgvESDataCount.Columns("DBLVOCEMISSION").HeaderText = "VOC Emmissions"
            dgvESDataCount.Columns("DBLVOCEMISSION").DisplayIndex = 3
            dgvESDataCount.Columns("DBLNOXEMISSION").HeaderText = "NOX Emmissions"
            dgvESDataCount.Columns("DBLNOXEMISSION").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 5

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

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
            Dim param As New SqlParameter("@year", lblYear.Text)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
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
                New SqlParameter("@year", lblYear.Text),
                New SqlParameter("@deadline", New Date(CInt(lblYear.Text) + 1, 6, 15))
            }

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, params)

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
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
                New SqlParameter("@year", lblYear.Text),
                New SqlParameter("@deadline", New Date(CInt(lblYear.Text) + 1, 6, 15))
            }

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, params)

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
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
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
            Dim param As New SqlParameter("@year", cboYear.SelectedItem)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewESData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewESData.LinkClicked
        Try
            Dim SQL As String = "SELECT esSchema.STRAIRSNUMBER, esSchema.STRFACILITYNAME,
                CASE WHEN DBLVOCEMISSION = '-1' THEN 'No Value' ELSE CAST(DBLVOCEMISSION AS VARCHAR(MAX)) END AS DBLVOCEMISSION, esSchema.STRCONFIRMATIONNBR,
                CASE WHEN DBLNOXEMISSION = '-1' THEN 'No Value' ELSE CAST(DBLNOXEMISSION AS VARCHAR(MAX)) END AS DBLNOXEMISSION, esSchema.STRDATEFIRSTCONFIRM
                FROM esSchema
                WHERE esSchema.intESyear = '2014'
                ORDER BY esSchema.STRFACILITYNAME"

            Dim param As New SqlParameter("@year", cboYear.SelectedItem)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3
            dgvESDataCount.Columns("DBLNOXEMISSION").HeaderText = "NOX Emissions"
            dgvESDataCount.Columns("DBLNOXEMISSION").DisplayIndex = 4
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 5

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
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
            Dim param As New SqlParameter("@year", lblYear.Text)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
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
            Dim param As New SqlParameter("@year", lblYear.Text)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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
            Dim param As New SqlParameter("@year", lblYear.Text)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
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
            Dim param As New SqlParameter("@year", lblYear.Text)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
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
            Dim param As New SqlParameter("@year", lblYear.Text)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
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
            Dim param As New SqlParameter("@year", lblYear.Text)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

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
            Dim param As New SqlParameter("@year", lblYear.Text)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

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
        Dim ESYear As String = cboYear.SelectedItem
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

                MsgBox("your info is updated!")
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

                MsgBox("your info is added!")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DeleteESMailOut()
        Dim AirsNo As String = txtESAIRSNo2.Text
        Dim ESyear As String = cboYear.SelectedItem

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
        Try
            txtESAIRSNo2.Text = ""
            txtESFacilityName.Text = ""
            txtESprefix.Text = ""
            txtESFirstName.Text = ""
            txtESLastName.Text = ""
            txtEScompanyName.Text = ""
            txtcontactAddress1.Text = ""
            txtcontactAddress2.Text = ""
            txtcontactCity.Text = ""
            txtcontactState.Text = ""
            txtcontactZipCode.Text = ""
            txtcontactEmail.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
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
            txtESContactFirstName.Clear()
            txtESContactLastName.Clear()
            txtAddress1.Text = ""
            txtAddress2.Text = ""
            txtCity.Text = ""
            txtState.Text = ""
            txtZip.Text = ""
            'txtEmail.Text = ""
            txtVOCEmission.Text = ""
            txtNOXEmission.Text = ""
            txtConfirmationNbr.Text = ""
            txtFirstConfirmedDate.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveESMailOut()
    End Sub

    Private Sub btnExporttoExcel_Click(sender As Object, e As EventArgs) Handles btnExporttoExcel.Click
        ExportEStoExcel()
    End Sub

    Private Sub btnESDelete_Click(sender As Object, e As EventArgs) Handles btnESDelete.Click
        Try
            DeleteESMailOut()
            ClearMailOut()
            MsgBox("The info has been deleted!")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub GenerateESMailOut()
        Dim airsNumber As String
        Dim airsYear As String
        Dim FACILITYNAME As String = " "
        Dim CONTACTCOMPANYNAME As String = " "
        Dim CONTACTADDRESS1 As String = " "
        Dim CONTACTCITY As String = " "
        Dim CONTACTSTATE As String = " "
        Dim CONTACTZIPCODE As String = " "
        Dim CONTACTFIRSTNAME As String = " "
        Dim CONTACTLASTNAME As String = " "
        Dim CONTACTEMAIL As String = " "
        Dim ESYear As String = cboMailoutYear.SelectedItem
        Dim OperationalStatus As String = " "
        Dim FacilityClass As String = " "

        Try
            Dim SQL As String = "Select strAirsNumber " &
            "FROM ESmailOut " &
            "where strESyear = @strESyear "
            Dim param As New SqlParameter("strESyear", ESYear)

            If DB.ValueExists(SQL, param) Then
                MsgBox("That year is already being used." & vbCrLf & "If you want to use that year," & vbCrLf & "you must first delete that year from the database.")
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

                    txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
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
                MsgBox("ES mail out is deleted!")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnGenMailOut_Click_1(sender As Object, e As EventArgs) Handles btnGenMailOut.Click
        Try
            GenerateESMailOut()
            cboMailoutYear.Items.Clear()
            loadMailOutYear()
            cboMailoutYear.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnDelMailOut_Click_1(sender As Object, e As EventArgs) Handles btnDelMailOut.Click
        Try
            deleteESmailOutbyYear()
            cboMailoutYear.Items.Clear()
            loadMailOutYear()
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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "EI Tool"

    Private Sub loadESEnrollmentYear()
        'Load MailOut Year dropdown boxes
        Dim SQL As String = "Select distinct STRESYEAR " &
                  "from ESMAILOUT  " &
                  "order by STRESYEAR desc"
        Dim dt As DataTable = DB.GetDataTable(SQL)

        For Each dr As DataRow In dt.Rows
            cboESYear.Items.Add(dr("STRESYEAR"))
        Next
    End Sub

#End Region

    Private Sub lblViewEmailData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewEmailData.LinkClicked
        LoadUserInfo(txtWebUserEmail.Text)
        LoadUserFacilityInfo(txtWebUserEmail.Text)
    End Sub

    Private Sub btnESenrollment_Click(sender As Object, e As EventArgs) Handles btnESenrollment.Click
        Try
            If cboESYear.Text = "" Then
                MsgBox("Please choose a Year!", MsgBoxStyle.Information, "ES Enrollment")
            Else
                ESMailoutenrollment()
                cboESYear.Items.Clear()
                loadESEnrollmentYear()
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
                MsgBox("That year " & ESYear & " is already enrolled.", MsgBoxStyle.Information, "EI Enrollment")
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
                MsgBox("Please choose a year!", MsgBoxStyle.Information, "ES Enrollment")
            Else
                Dim intAnswer As DialogResult
                intAnswer = MessageBox.Show("Remove the enrollment?", "ES Enrollment", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)

                Select Case intAnswer
                    Case DialogResult.OK
                        SQL = "delete from ESSCHEMA " &
                            "where ESSCHEMA.INTESYEAR = @ESYear "
                        Dim param As New SqlParameter("@ESYear", ESYear)

                        DB.RunCommand(SQL, param)

                        MsgBox("Enrollment has been removed!", MsgBoxStyle.Information, "ES Enrollment")
                    Case Else
                        MsgBox("Enrollment has not been removed!", MsgBoxStyle.Information, "ES Enrollment")
                End Select

                cboESYear.Items.Clear()
                loadESEnrollmentYear()
                cboESYear.Text = ""
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnaddfacilitytoES_Click(sender As Object, e As EventArgs) Handles btnaddfacilitytoES.Click
        addonefacilityES()
    End Sub

    Private Sub addonefacilityES()
        Dim AirsNo As String = "0413" & txtESairNumber.Text
        Dim ESYear As Integer = txtESYearforFacility.Text
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
        Dim ESYear As Integer = txtESYearforFacility.Text
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

                    MsgBox("This Facility (" & AirsNo & ") has been removed for " & ESYear & "!", MsgBoxStyle.Information, "ES Enrollment")
                Case Else
                    MsgBox("This Facility (" & AirsNo & ") has not been removed for " & ESYear & "!", MsgBoxStyle.Information, "ES Enrollment")
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCheckESstatus_Click(sender As Object, e As EventArgs) Handles btnCheckESstatus.Click
        Dim AirsNo As String = "0413" & txtESairNumber.Text
        Dim ESYear As Integer = txtESYearforFacility.Text

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
                MsgBox("This facility (" & AirsNo & ") has been enrolled for " & ESYear & "!", MsgBoxStyle.Information, "ES Enrollment")
            Else
                MsgBox("This facility (" & AirsNo & ") is not enrolled for " & ESYear & "!", MsgBoxStyle.Information, "ES Enrollment")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblviewESenrollment_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblviewESenrollment.LinkClicked
        Try
            Dim year As String = cboESYear.Text

            If cboESYear.Text = "" Then
                MsgBox("Please choose a year to view!", MsgBoxStyle.Information, "ES Enrollment")
            Else
                Dim SQL As String = "SELECT ESSCHEMA.STRAIRSNUMBER, " &
                    "ESSCHEMA.STRFACILITYNAME, " &
                    "ESSCHEMA.DATTRANSACTION " &
                    "from ESSCHEMA " &
                    "where ESSCHEMA.INTESYEAR = @year "
                Dim param As New SqlParameter("@year", year)

                dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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
                dgvESDataCount.Columns("DATTRANSACTION").HeaderText = "Transaction Date"
                dgvESDataCount.Columns("DATTRANSACTION").DisplayIndex = 2

                txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
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
            Dim param As New SqlParameter("@year", lblYear.Text)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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
            Dim param As New SqlParameter("@year", lblYear.Text)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
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
            Dim param As New SqlParameter("@year", lblYear.Text)

            dgvESDataCount.DataSource = DB.GetDataTable(SQL, param)

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblviewextraNonresponse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblviewextraNonresponse.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

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
            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub loadcboEISstatusCodes()
        Dim SQL As String = "Select '' as EISSTATUSCODE, '- Select EIS Status Code -' as STRDESC " &
            " union select distinct  EISSTATUSCODE, STRDESC " &
            " from EISLK_EISSTATUSCODE "

        With cboEILogStatusCode
            .DataSource = DB.GetDataTable(SQL)
            .DisplayMember = "STRDESC"
            .ValueMember = "EISSTATUSCODE"
            .SelectedIndex = 0
        End With

        SQL = "select '' as EISAccessCode, '- Select EIS Access Code -' as STRDESC " &
            " union select EISAccessCode, strDesc " &
            " from EISLK_EISAccesscode " &
            " order by strDesc"

        With cboEILogAccessCode
            .DataSource = DB.GetDataTable(SQL)
            .DisplayMember = "STRDESC"
            .ValueMember = "EISAccessCode"
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub llbViewUserData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewUserData.LinkClicked
        ViewFacilitySpecificUsers()
    End Sub

    Private Sub ViewFacilitySpecificUsers()
        Try

            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please enter a complete 8 digit AIRS #.", MsgBoxStyle.Information, "DMU Tools")
            Else
                Dim dgvRow As DataGridViewRow
                txtEmail.Clear()

                Dim SQL As String = "Select strFacilityName " &
                "from APBFacilityInformation " &
                "where strAIRSNumber = @strAIRSNumber "
                Dim param As New SqlParameter("@strAIRSNumber", "0413" & mtbAIRSNumber.Text)
                Dim fn As String = DB.GetString(SQL, param)

                If fn = "" Then
                    lblFaciltyName.Text = " - "
                Else
                    lblFaciltyName.Text = Facility.SanitizeFacilityNameForDb(fn)
                End If

                SQL = "SELECT " &
                "OlapUserAccess.NumUserID as ID, OlapUserLogin.numuserid, " &
                "OlapUserLogin.strUserEmail as Email, " &
                "Case " &
                "When intAdminAccess = 0 Then 'False' " &
                "When intAdminAccess = 1 Then 'True' " &
                "End as intAdminAccess, " &
                "Case " &
                "When intFeeAccess = 0 Then 'False' " &
                "When intFeeAccess = 1 Then 'True' " &
                "End as intFeeAccess, " &
                "Case " &
                "When intEIAccess = 0 Then 'False' " &
                "When intEIAccess = 1 Then 'True' " &
                "End as intEIAccess, " &
                "Case " &
                "When intESAccess = 0 Then 'False' " &
                "When intESAccess = 1 Then 'True' " &
                "End as intESAccess " &
                "FROM OlapUserAccess, OlapUserLogin " &
                "WHERE OLAPUserAccess.NumUserId = OlapUserLogin.NumUserID " &
                "AND OlapUserAccess.strAirsNumber = @strAirsNumber order by email"

                Dim dt As DataTable = DB.GetDataTable(SQL, param)

                dgvUsers.Rows.Clear()

                For Each dr As DataRow In dt.Rows
                    dgvRow = New DataGridViewRow
                    dgvRow.CreateCells(dgvUsers)
                    If IsDBNull(dr.Item("ID")) Then
                        dgvRow.Cells(0).Value = ""
                    Else
                        dgvRow.Cells(0).Value = dr.Item("ID")
                    End If
                    If IsDBNull(dr.Item("numuserid")) Then
                        dgvRow.Cells(1).Value = ""
                    Else
                        dgvRow.Cells(1).Value = dr.Item("numuserid")
                    End If
                    If IsDBNull(dr.Item("Email")) Then
                        dgvRow.Cells(2).Value = ""
                    Else
                        dgvRow.Cells(2).Value = dr.Item("Email")
                    End If
                    If IsDBNull(dr.Item("intAdminAccess")) Then
                        dgvRow.Cells(3).Value = False
                    Else
                        dgvRow.Cells(3).Value = dr.Item("intAdminAccess")
                    End If
                    If IsDBNull(dr.Item("intFeeAccess")) Then
                        dgvRow.Cells(4).Value = False
                    Else
                        dgvRow.Cells(4).Value = dr.Item("intFeeAccess")
                    End If

                    If IsDBNull(dr.Item("intEIAccess")) Then
                        dgvRow.Cells(5).Value = False
                    Else
                        dgvRow.Cells(5).Value = dr.Item("intEIAccess")
                    End If
                    If IsDBNull(dr.Item("intESAccess")) Then
                        dgvRow.Cells(6).Value = False
                    Else
                        dgvRow.Cells(6).Value = dr.Item("intESAccess")
                    End If
                    dgvUsers.Rows.Add(dgvRow)
                Next

                cboUsers.DataSource = dt
                cboUsers.DisplayMember = "Email"
                cboUsers.ValueMember = "ID"
                cboUsers.Text = ""
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        Try
            Dim userID As Integer?

            Dim SQL As String = "Select numUserId " &
            "from olapuserlogin " &
            "where struseremail = @struseremail "
            Dim param As New SqlParameter("@struseremail", UCase(txtEmail.Text))
            userID = DB.GetSingleValue(Of Decimal?)(SQL, param)

            If userID IsNot Nothing Then 'Email address is registered
                SQL = "Insert into OlapUserAccess " &
                    "(numUserId, strAirsNumber, strFacilityName) values " &
                    "(@numUserId, @strAirsNumber, @strFacilityName) "
                Dim params As SqlParameter() = {
                    New SqlParameter("@numUserId", userID),
                    New SqlParameter("@strAirsNumber", "0413" & mtbAIRSNumber.Text),
                    New SqlParameter("@strFacilityName", lblFaciltyName.Text)
                }
                DB.RunCommand(SQL, params)

                ViewFacilitySpecificUsers()

                MsgBox("The User has beed added to this facility", MsgBoxStyle.Information, "Insert Success!")
            Else 'email address not registered
                MsgBox("This Email Address is not registered", MsgBoxStyle.OkOnly, "Insert Failed!")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim SQL As String = "DELETE OlapUserAccess " &
                "WHERE numUserID = @numUserID " &
                "and strAirsNumber = @strAirsNumber "
            Dim params As SqlParameter() = {
                New SqlParameter("@numUserID", cboUsers.SelectedValue),
                New SqlParameter("@strAirsNumber", "0413" & mtbAIRSNumber.Text)
            }
            DB.RunCommand(SQL, params)

            ViewFacilitySpecificUsers()
            MsgBox("The User has been removed for this facility", MsgBoxStyle.Information, "User Removed!")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Dim adminaccess As String
            Dim feeaccess As String
            Dim eiaccess As String
            Dim esaccess As String

            For i As Integer = 0 To dgvUsers.Rows.Count - 1
                If dgvUsers(3, i).Value = True Then
                    adminaccess = "1"
                Else
                    adminaccess = "0"
                End If
                If dgvUsers(4, i).Value = True Then
                    feeaccess = "1"
                Else
                    feeaccess = "0"
                End If
                If dgvUsers(5, i).Value = True Then
                    eiaccess = "1"
                Else
                    eiaccess = "0"
                End If
                If dgvUsers(6, i).Value = True Then
                    esaccess = "1"
                Else
                    esaccess = "0"
                End If

                Dim SQL As String = "UPDATE OlapUserAccess " &
                    "SET " &
                    "intadminaccess = @intadminaccess, " &
                    "intFeeAccess = @intFeeAccess, " &
                    "intEIAccess = @intEIAccess, " &
                    "intESAccess = @intESAccess " &
                    "WHERE numUserID = @numUserID " &
                    "and strAirsNumber = @strAirsNumber "
                Dim params As SqlParameter() = {
                    New SqlParameter("@intadminaccess", adminaccess),
                    New SqlParameter("@intFeeAccess", feeaccess),
                    New SqlParameter("@intEIAccess", eiaccess),
                    New SqlParameter("@intESAccess", esaccess),
                    New SqlParameter("@numUserID", dgvUsers(1, i).Value),
                    New SqlParameter("@strAirsNumber", "0413" & mtbAIRSNumber.Text)
                }
                DB.RunCommand(SQL, params)
            Next

            ViewFacilitySpecificUsers()
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEditUserData_Click(sender As Object, e As EventArgs) Handles btnEditUserData.Click
        Try
            txtEditFirstName.Visible = True
            txtEditLastName.Visible = True
            txtEditTitle.Visible = True
            txtEditCompany.Visible = True
            txtEditAddress.Visible = True
            txtEditCity.Visible = True
            mtbEditState.Visible = True
            mtbEditZipCode.Visible = True
            mtbEditPhoneNumber.Visible = True
            mtbEditFaxNumber.Visible = True
            btnSaveEditedData.Visible = True
            txtEditUserPassword.Visible = True
            btnChangeEmailAddress.Visible = True
            txtEditEmail.Visible = True
            btnUpdatePassword.Visible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveEditedData_Click(sender As Object, e As EventArgs) Handles btnSaveEditedData.Click
        Try
            Dim FirstName As String = ""
            Dim LastName As String = ""
            Dim Title As String = ""
            Dim Company As String = ""
            Dim Address As String = ""
            Dim City As String = ""
            Dim State As String = ""
            Dim Zip As String = ""
            Dim PhoneNumber As String = ""
            Dim FaxNumber As String = ""

            If txtWebUserID.Text <> "" Then
                If txtEditFirstName.Text <> "" Then
                    FirstName = " strFirstName = @strFirstName "
                End If
                If txtEditLastName.Text <> "" Then
                    LastName = " strLastName = @strLastName "
                End If
                If txtEditTitle.Text <> "" Then
                    Title = " strTitle = @strTitle "
                End If
                If txtEditCompany.Text <> "" Then
                    Company = " strCompanyName = @strCompanyName "
                End If
                If txtEditAddress.Text <> "" Then
                    Address = " strAddress = @strAddress "
                End If
                If txtEditCity.Text <> "" Then
                    City = " strCity = @strCity "
                End If
                If mtbEditState.Text <> "" Then
                    State = " strState = @strState "
                End If
                If mtbEditZipCode.Text <> "" Then
                    Zip = " strZip = @strZip "
                End If
                If mtbEditPhoneNumber.Text <> "" Then
                    PhoneNumber = " strPhoneNumber = @strPhoneNumber "
                End If
                If mtbEditFaxNumber.Text <> "" Then
                    FaxNumber = " strFaxNumber = @strFaxNumber "
                End If

                Dim SQL As String = "Update OLAPUserProfile set " &
                    ConcatNonEmptyStrings(",", {FirstName, LastName, Title, Company, Address, City, State, Zip, PhoneNumber, FaxNumber}) &
                    "where numUserID = @numUserID "

                Dim params As SqlParameter() = {
                    New SqlParameter("@strFirstName", txtEditFirstName.Text),
                    New SqlParameter("@strLastName", txtEditLastName.Text),
                    New SqlParameter("@strTitle", txtEditTitle.Text),
                    New SqlParameter("@strCompanyName", txtEditCompany.Text),
                    New SqlParameter("@strAddress", txtEditAddress.Text),
                    New SqlParameter("@strCity", txtEditCity.Text),
                    New SqlParameter("@strState", mtbEditState.Text),
                    New SqlParameter("@strZip", mtbEditZipCode.Text),
                    New SqlParameter("@strPhoneNumber", mtbEditPhoneNumber.Text),
                    New SqlParameter("@strFaxNumber", mtbEditFaxNumber.Text),
                    New SqlParameter("@numUserID", txtWebUserID.Text)
                }

                DB.RunCommand(SQL, params)

                lblFName.Text = "First Name: " & txtEditFirstName.Text
                lblLName.Text = "Last Name: " & txtEditLastName.Text
                lblTitle.Text = "Title: " & txtEditTitle.Text
                lblCoName.Text = "Company Name: " & txtEditCompany.Text
                lblAddress.Text = txtEditAddress.Text
                lblCityStateZip.Text = txtEditCity.Text & ", " & mtbEditState.Text & " " & mtbEditZipCode.Text
                lblPhoneNo.Text = "Phone Number: " & mtbEditPhoneNumber.Text
                lblFaxNo.Text = "Fax Number: " & mtbEditFaxNumber.Text

                txtEditFirstName.Visible = False
                txtEditLastName.Visible = False
                txtEditTitle.Visible = False
                txtEditCompany.Visible = False
                txtEditAddress.Visible = False
                txtEditCity.Visible = False
                mtbEditState.Visible = False
                mtbEditZipCode.Visible = False
                mtbEditPhoneNumber.Visible = False
                mtbEditFaxNumber.Visible = False
                btnSaveEditedData.Visible = False
                txtEditUserPassword.Visible = False
                btnChangeEmailAddress.Visible = False
                txtEditEmail.Visible = False
                btnUpdatePassword.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdatePassword_Click(sender As Object, e As EventArgs) Handles btnUpdatePassword.Click
        Try
            If txtWebUserID.Text <> "" And txtEditUserPassword.Text <> "" Then
                'New password change code 6/30/2010
                Dim SQL As String = "Update OLAPUserLogIN set " &
                "strUserPassword = @strUserPassword " &
                "where numUserID = @numUserID "
                Dim params As SqlParameter() = {
                    New SqlParameter("@strUserPassword", getMd5Hash(txtEditUserPassword.Text)),
                    New SqlParameter("@numUserID", txtWebUserID.Text)
                }
                DB.RunCommand(SQL, params)

                txtEditUserPassword.Clear()
                txtEditFirstName.Visible = False
                txtEditLastName.Visible = False
                txtEditTitle.Visible = False
                txtEditCompany.Visible = False
                txtEditAddress.Visible = False
                txtEditCity.Visible = False
                mtbEditState.Visible = False
                mtbEditZipCode.Visible = False
                mtbEditPhoneNumber.Visible = False
                mtbEditFaxNumber.Visible = False
                btnSaveEditedData.Visible = False
                txtEditUserPassword.Visible = False
                btnChangeEmailAddress.Visible = False
                txtEditEmail.Visible = False
                btnUpdatePassword.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnChangeEmailAddress_Click(sender As Object, e As EventArgs) Handles btnChangeEmailAddress.Click
        Try
            If txtWebUserID.Text <> "" Then
                If IsValidEmailAddress(txtEditEmail.Text) Then
                    Dim SQL As String = "Select " &
                    "1 " &
                    "from OLAPUserLogIN " &
                    "where strUserEmail = @strUserEmail " &
                    " and numUserID <> @numUserID "

                    Dim params As SqlParameter() = {
                        New SqlParameter("@strUserEmail", txtEditEmail.Text.ToUpper),
                        New SqlParameter("@numUserID", txtWebUserID.Text)
                    }

                    If DB.GetBoolean(SQL, params) Then
                        MsgBox("Another user already has this email address and it would violate a unique constraint if you were " &
                               "to add this email to this user.", MsgBoxStyle.Exclamation, "Mailout and Stats")
                        Exit Sub
                    End If

                    SQL = "Update OLAPUserLogIn set " &
                    " strUserEmail = @strUserEmail " &
                    " where numUserID = @numUserID "

                    DB.RunCommand(SQL, params)

                    txtWebUserEmail.Text = txtEditEmail.Text

                    LoadUserInfo(txtWebUserEmail.Text)

                    If txtWebUserID.Text = "" Then
                        pnlUserInfo.Visible = False
                        pnlUserFacility.Visible = False
                    Else
                        pnlUserInfo.Visible = True
                        pnlUserFacility.Visible = True
                    End If

                Else
                    MsgBox("Invalid Email Address", MsgBoxStyle.Exclamation, "DMU Tools")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddFacilitytoUser_Click(sender As Object, e As EventArgs) Handles btnAddFacilitytoUser.Click
        Try
            If txtWebUserID.Text <> "" And mtbFacilityToAdd.Text <> "" Then
                Dim SQL As String = "Select " &
                "1 " &
                "from OlapUserAccess " &
                "where numUserId = @numUserId " &
                " And strAirsNumber = @strAirsNumber "
                Dim params As SqlParameter() = {
                    New SqlParameter("@numUserId", txtWebUserID.Text),
                    New SqlParameter("@strAirsNumber", "0413" & mtbFacilityToAdd.Text)
                }

                If Not DB.GetBoolean(SQL, params) Then
                    SQL = "Insert into OlapUserAccess " &
                        "(numUserId, strAirsNumber, strFacilityName) " &
                        "values " &
                        "(@numUserId, @strAirsNumber, " &
                        "(select strFacilityName " &
                        "from APBFacilityInformation " &
                        "where strAIRSnumber = @strAirsNumber)) "

                    Dim params2 As SqlParameter() = {
                        New SqlParameter("@numUserId", txtWebUserID.Text),
                        New SqlParameter("@strAirsNumber", "0413" & mtbFacilityToAdd.Text)
                    }

                    DB.RunCommand(SQL, params2)

                    LoadUserFacilityInfo(txtWebUserEmail.Text)
                    MsgBox("The facility has been added to this user", MsgBoxStyle.Information, "Insert Success!")
                Else
                    MsgBox("The facility already exists for this user." & vbCrLf & "NO DATA SAVED", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnDeleteFacilityUser_Click(sender As Object, e As EventArgs) Handles btnDeleteFacilityUser.Click
        Try
            If txtWebUserID.Text <> "" And cboFacilityToDelete.Text <> "" Then
                Dim SQL As String = "DELETE OlapUserAccess " &
                "WHERE numUserID = @numUserID " &
                "and strAirsNumber = @strAirsNumber "
                Dim params As SqlParameter() = {
                    New SqlParameter("@numUserID", txtWebUserID.Text),
                    New SqlParameter("@strAirsNumber", "0413" & cboFacilityToDelete.SelectedValue)
                }
                DB.RunCommand(SQL, params)

                LoadUserFacilityInfo(txtWebUserEmail.Text)
                MsgBox("The facility has been removed for this user", MsgBoxStyle.Information, "Facility Removed!")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateUser_Click(sender As Object, e As EventArgs) Handles btnUpdateUser.Click
        Try
            Dim adminaccess As String
            Dim feeaccess As String
            Dim eiaccess As String
            Dim esaccess As String

            For i As Integer = 0 To dgvUserFacilities.Rows.Count - 1
                If dgvUserFacilities(2, i).Value = True Then
                    adminaccess = "1"
                Else
                    adminaccess = "0"
                End If
                If dgvUserFacilities(3, i).Value = True Then
                    feeaccess = "1"
                Else
                    feeaccess = "0"
                End If
                If dgvUserFacilities(4, i).Value = True Then
                    eiaccess = "1"
                Else
                    eiaccess = "0"
                End If
                If dgvUserFacilities(5, i).Value = True Then
                    esaccess = "1"
                Else
                    esaccess = "0"
                End If

                Dim SQL As String = "UPDATE OlapUserAccess " &
                    "SET intadminaccess = @intadminaccess, " &
                    "intFeeAccess = @intFeeAccess, " &
                    "intEIAccess = @intEIAccess, " &
                    "intESAccess = @intESAccess " &
                    "WHERE numUserID = @numUserID " &
                    "and strAirsNumber = @strAirsNumber "

                Dim params As SqlParameter() = {
                    New SqlParameter("@intadminaccess", adminaccess),
                    New SqlParameter("@intFeeAccess", feeaccess),
                    New SqlParameter("@intEIAccess", eiaccess),
                    New SqlParameter("@intESAccess", esaccess),
                    New SqlParameter("@numUserID", txtWebUserID.Text),
                    New SqlParameter("@strAirsNumber", "0413" & dgvUserFacilities(0, i).Value)
                }
                DB.RunCommand(SQL, params)
            Next

            LoadUserFacilityInfo(txtWebUserEmail.Text)
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnReloadFSData_Click(sender As Object, e As EventArgs) Handles btnReloadFSData.Click
        LoadFSData()
    End Sub

    Private Sub LoadFSData()
        Try
            If cboEILogYear.Text = "" Or cboEILogYear.Text.Length <> 4 Then
                MsgBox("Please select a valid year from the EIS Year dropdown.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If mtbEILogAIRSNumber.Text = "" Or mtbEILogAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please enter a valid AIRS # into the EIS AIRS #", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            txtEILogSelectedYear.Text = cboEILogYear.Text
            txtEILogSelectedAIRSNumber.Text = mtbEILogAIRSNumber.Text

            LoadAdminData()

            Dim SQL As String = "select  " &
            "strFacilitySiteName, STRFACILITYSITESTATUSCODE " &
            "from EIS_FacilitySite " &
            "where FacilitySiteId = @FacilitySiteId "

            Dim param As New SqlParameter("@FacilitySiteId", txtEILogSelectedAIRSNumber.Text)

            Dim dr As DataRow = DB.GetDataRow(SQL, param)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strFacilitySiteName")) Then
                    txtEIModifyFacilityName.Clear()
                    txtEILogFacilityName.Clear()
                Else
                    txtEIModifyFacilityName.Text = dr.Item("strFacilitySiteName")
                    txtEILogFacilityName.Text = dr.Item("strFacilitySiteName")
                End If

                If IsDBNull(dr.Item("STRFACILITYSITESTATUSCODE")) Then
                    cbEisModifyOperStatus.SelectedValue = EisSiteStatus.UNK
                Else
                    cbEisModifyOperStatus.SelectedValue = [Enum].Parse(GetType(EisSiteStatus), dr.Item("STRFACILITYSITESTATUSCODE"))
                End If
            End If

            SQL = "select * " &
            "from EIS_FacilitySiteAddress " &
            "where FacilitySiteId = @FacilitySiteId "

            dr = DB.GetDataRow(SQL, param)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strLocationAddressText")) Then
                    txtEIModifyLocation.Clear()
                Else
                    txtEIModifyLocation.Text = dr.Item("strLocationAddressText")
                End If
                If IsDBNull(dr.Item("strLocalityName")) Then
                    txtEIModifyCity.Clear()
                Else
                    txtEIModifyCity.Text = dr.Item("strLocalityName")
                End If
                If IsDBNull(dr.Item("strLocationAddressPostalCode")) Then
                    mtbEIModifyZipCode.Clear()
                Else
                    mtbEIModifyZipCode.Text = dr.Item("strLocationAddressPostalCode")
                End If
                If IsDBNull(dr.Item("STRMAILINGADDRESSTEXT")) Then
                    txtEIModifyMLocation.Clear()
                Else
                    txtEIModifyMLocation.Text = dr.Item("STRMAILINGADDRESSTEXT")
                End If
                If IsDBNull(dr.Item("STRMAILINGADDRESSCITYNAME")) Then
                    txtEIModifyMCity.Clear()
                Else
                    txtEIModifyMCity.Text = dr.Item("STRMAILINGADDRESSCITYNAME")
                End If
                If IsDBNull(dr.Item("STRMAILINGADDRESSPOSTALCODE")) Then
                    mtbEIModifyMZipCode.Clear()
                Else
                    mtbEIModifyMZipCode.Text = dr.Item("STRMAILINGADDRESSPOSTALCODE")
                End If
            End If

            SQL = "select numLatitudeMeasure, numLongitudeMeasure " &
            "from EIS_FacilityGeoCoord " &
            "where FacilitySiteId = @FacilitySiteId "

            dr = DB.GetDataRow(SQL, param)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("numLatitudeMeasure")) Then
                    mtbEIModifyLatitude.Clear()
                Else
                    mtbEIModifyLatitude.Text = dr.Item("numLatitudeMeasure")
                End If
                If IsDBNull(dr.Item("numLongitudeMeasure")) Then
                    mtbEIModifyLongitude.Clear()
                Else
                    mtbEIModifyLongitude.Text = dr.Item("numLongitudeMeasure")
                End If
            End If

            SQL = "SELECT fi.STRFACILITYNAME, fi.STRFACILITYSTREET1, " &
                "  fi.STRFACILITYCITY, fi.STRFACILITYSTATE, " &
                "  fi.STRFACILITYZIPCODE, fi.NUMFACILITYLONGITUDE, " &
                "  fi.NUMFACILITYLATITUDE, hd.STROPERATIONALSTATUS " &
                "FROM APBFACILITYINFORMATION fi " &
                "INNER JOIN APBHEADERDATA hd ON fi.STRAIRSNUMBER = " &
                "  hd.STRAIRSNUMBER " &
                "WHERE fi.STRAIRSNUMBER = @airs "

            Dim param2 As New SqlParameter("@airs", "0413" & txtEILogSelectedAIRSNumber.Text)

            dr = DB.GetDataRow(SQL, param2)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtEIModifyIAIPFacilityName.Clear()
                Else
                    txtEIModifyIAIPFacilityName.Text = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    txtEIModifyIAIPLocation.Clear()
                Else
                    txtEIModifyIAIPLocation.Text = dr.Item("strFacilityStreet1")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    txtEIModifyIAIPCity.Clear()
                Else
                    txtEIModifyIAIPCity.Text = dr.Item("strFacilityCity")
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    mtbEIModifyIAIPZipCode.Clear()
                Else
                    mtbEIModifyIAIPZipCode.Text = dr.Item("strFacilityZipCode")
                End If
                If IsDBNull(dr.Item("numFacilityLongitude")) Then
                    mtbEIModifyIAIPLongitude.Clear()
                Else
                    mtbEIModifyIAIPLongitude.Text = dr.Item("numFacilityLongitude")
                End If
                If IsDBNull(dr.Item("numFacilityLatitude")) Then
                    mtbEIModifyIAIPLatitude.Clear()
                Else
                    mtbEIModifyIAIPLatitude.Text = dr.Item("numFacilityLatitude")
                End If
                If IsDBNull(dr.Item("STROPERATIONALSTATUS")) Then
                    cbIaipOperStatus.SelectedValue = FacilityOperationalStatus.U
                Else
                    cbIaipOperStatus.SelectedValue = [Enum].Parse(GetType(FacilityOperationalStatus), dr.Item("STROPERATIONALSTATUS"))
                End If
            End If

            SQL = "Select * " &
            "from EIS_Mailout " &
            "where intInventoryYear = @intInventoryYear " &
            "and FacilitySiteID = @FacilitySiteID "

            Dim params As SqlParameter() = {
                New SqlParameter("@intInventoryYear", txtEILogSelectedYear.Text),
                New SqlParameter("@FacilitySiteID", txtEILogSelectedAIRSNumber.Text)
            }

            dr = DB.GetDataRow(SQL, params)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtEISMailoutFacilityName.Clear()
                Else
                    txtEISMailoutFacilityName.Text = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtEISMailoutCompanyName.Clear()
                Else
                    txtEISMailoutCompanyName.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtEISMailoutAddress.Clear()
                Else
                    txtEISMailoutAddress.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactAddress2")) Then
                    txtEISMailoutAddress2.Clear()
                Else
                    txtEISMailoutAddress2.Text = dr.Item("strContactAddress2")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtEISMailoutCity.Clear()
                Else
                    txtEISMailoutCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    txtEISMailoutState.Clear()
                Else
                    txtEISMailoutState.Text = dr.Item("strContactState")
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    txtEISMailoutZipCode.Clear()
                Else
                    txtEISMailoutZipCode.Text = dr.Item("strContactZipCode")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    txtEISMailoutFirstName.Clear()
                Else
                    txtEISMailoutFirstName.Text = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtEISMailoutLastName.Clear()
                Else
                    txtEISMailoutLastName.Text = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtEISMailoutPrefix.Clear()
                Else
                    txtEISMailoutPrefix.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtEISMailoutEmail.Clear()
                Else
                    txtEISMailoutEmail.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtEISMailoutComments.Clear()
                Else
                    txtEISMailoutComments.Text = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("UpdateUser")) Then
                    txtEISMailoutUpdateUser.Clear()
                Else
                    txtEISMailoutUpdateUser.Text = dr.Item("UpdateUser")
                End If
                If IsDBNull(dr.Item("UpdateDateTime")) Then
                    txtEISMailoutUpdateDateTime.Clear()
                Else
                    txtEISMailoutUpdateDateTime.Text = dr.Item("UpdateDateTime")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    txtEISMailoutCreateDateTime.Clear()
                Else
                    txtEISMailoutCreateDateTime.Text = dr.Item("CreateDateTime")
                End If
            End If

            SQL = "select " &
            "strContactFirstName, strContactLastName, " &
            "strContactPrefix, strContactSuffix, " &
            "strContactTitle, strContactPhoneNumber1, " &
            "strContactPhoneNumber2, strContactFaxNumber, " &
            "strContactEmail, strContactCompanyName, " &
            "strContactAddress1, strContactAddress2, " &
            "strContactCity, strContactState, " &
            "strContactZipCode, strContactDescription, " &
            "datModifingDate, (strLastName+', '+strFirstName) as ModifingPerson " &
            "from APBContactInformation, EPDUserProfiles " &
            "where APBContactInformation.strModifingPerson = " &
            "EPDUserProfiles.numUserID  " &
            "and strContactKey = @key "

            Dim param3 As New SqlParameter("@key", "0413" & txtEILogSelectedAIRSNumber.Text & "41")

            dr = DB.GetDataRow(SQL, param3)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    txtEISContactFirstName.Clear()
                Else
                    txtEISContactFirstName.Text = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtEISContactLastName.Clear()
                Else
                    txtEISContactLastName.Text = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtEISContactPrefix.Clear()
                Else
                    txtEISContactPrefix.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactSuffix")) Then
                    txtEISContactSuffix.Clear()
                Else
                    txtEISContactSuffix.Text = dr.Item("strContactSuffix")
                End If
                If IsDBNull(dr.Item("strContactTitle")) Then
                    txtEISContactTitle.Clear()
                Else
                    txtEISContactTitle.Text = dr.Item("strContactTitle")
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                    txtEISContactPhone.Clear()
                Else
                    txtEISContactPhone.Text = dr.Item("strContactPhoneNumber1")
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber2")) Then
                    txtEISContactPhone2.Clear()
                Else
                    txtEISContactPhone2.Text = dr.Item("strContactPhoneNumber2")
                End If
                If IsDBNull(dr.Item("strContactFaxNumber")) Then
                    txtEISContactFax.Clear()
                Else
                    txtEISContactFax.Text = dr.Item("strContactFaxNumber")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtEISContactEmail.Clear()
                Else
                    txtEISContactEmail.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtEISContactCompanyName.Clear()
                Else
                    txtEISContactCompanyName.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtEISContactAddress.Clear()
                Else
                    txtEISContactAddress.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactAddress2")) Then
                    txtEISContactAddress2.Clear()
                Else
                    txtEISContactAddress2.Text = dr.Item("strContactAddress2")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtEISContactCity.Clear()
                Else
                    txtEISContactCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    txtEISContactState.Clear()
                Else
                    txtEISContactState.Text = dr.Item("strContactState")
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    txtEISContactZipCode.Clear()
                Else
                    txtEISContactZipCode.Text = dr.Item("strContactZipCode")
                End If
                If IsDBNull(dr.Item("strContactDescription")) Then
                    txtEISContactDescription.Clear()
                Else
                    txtEISContactDescription.Text = dr.Item("strContactDescription")
                End If
                If IsDBNull(dr.Item("ModifingPerson")) Then
                    txtEISContactUpdateUser.Clear()
                Else
                    txtEISContactUpdateUser.Text = dr.Item("ModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    txtEISContactUpdateDateTime.Clear()
                Else
                    txtEISContactUpdateDateTime.Text = dr.Item("datModifingDate")
                End If
            End If

            LoadQASpecificData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadAdminData()
        Try
            dtpDeadlineEIS.Checked = False
            txtEISDeadlineComment.Clear()
            txtAllEISDeadlineComment.Clear()

            Dim SQL As String = "Select * " &
           "From EIS_Admin " &
           "where inventoryYear = @inventoryYear " &
           "and FacilitySiteID = @FacilitySiteID "

            Dim params As SqlParameter() = {
                New SqlParameter("@inventoryYear", txtEILogSelectedYear.Text),
                New SqlParameter("@FacilitySiteID", txtEILogSelectedAIRSNumber.Text)
            }

            Dim dr As DataRow = DB.GetDataRow(SQL, params)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("EISStatusCode")) Then
                    cboEILogStatusCode.SelectedText = ""
                Else
                    cboEILogStatusCode.SelectedValue = dr.Item("EISStatusCode")
                    txtEILogStatusCode.Text = cboEILogStatusCode.Text
                End If
                If IsDBNull(dr.Item("datEISStatus")) Then
                    dtpEILogStatusDateSubmit.Value = Today
                Else
                    dtpEILogStatusDateSubmit.Text = dr.Item("datEISStatus")
                End If
                If IsDBNull(dr.Item("EISAccessCode")) Then
                    cboEILogAccessCode.Text = ""
                Else
                    cboEILogAccessCode.SelectedValue = dr.Item("EISAccessCode")
                End If
                If IsDBNull(dr.Item("strOptOut")) Then
                    rdbEILogOpOutYes.Checked = False
                    rdbEILogOpOutNo.Checked = False
                Else
                    If dr.Item("strOptOut") = "1" Then
                        rdbEILogOpOutYes.Checked = True
                    Else
                        rdbEILogOpOutNo.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strIncorrectOptOut")) Then
                    chbOptedOutIncorrectly.Checked = False
                Else
                    If dr.Item("strIncorrectOptOut") = "1" Then
                        chbOptedOutIncorrectly.Checked = True
                    Else
                        chbOptedOutIncorrectly.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    rdbEILogMailoutYes.Checked = False
                    rdbEILogMailoutNo.Checked = False
                Else
                    If dr.Item("strMailout") = "1" Then
                        rdbEILogMailoutYes.Checked = True
                    Else
                        rdbEILogMailoutNo.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strEnrollment")) Then
                    rdbEILogEnrolledYes.Checked = False
                    rdbEILogEnrolledNo.Checked = False
                Else
                    If dr.Item("strEnrollment") = "1" Then
                        rdbEILogEnrolledYes.Checked = True
                    Else
                        rdbEILogEnrolledNo.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datEnrollment")) Then
                    dtpEILogDateEnrolled.Value = Today
                Else
                    dtpEILogDateEnrolled.Text = dr.Item("datEnrollment")
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtEILogComments.Clear()
                Else
                    txtEILogComments.Text = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("Active")) Then
                    rdbEILogActiveYes.Checked = False
                    rdbEILogActiveNo.Checked = False
                Else
                    If dr.Item("Active") = "1" Then
                        rdbEILogActiveYes.Checked = True
                    Else
                        rdbEILogActiveNo.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("updateUser")) Then
                    txtEILogUpdatedBy.Clear()
                Else
                    txtEILogUpdatedBy.Text = dr.Item("UpdateUser")
                End If
                If IsDBNull(dr.Item("updateDatetime")) Then
                    txtEILogUpdatedTime.Clear()
                Else
                    txtEILogUpdatedTime.Text = dr.Item("updateDatetime")
                End If
                If IsDBNull(dr.Item("intPrepopYear")) Then
                    txtEILogPrePopYear.Clear()
                Else
                    txtEILogPrePopYear.Text = dr.Item("intPrepopYear")
                End If
                If IsDBNull(dr.Item("datEISDeadline")) Then

                Else
                    dtpDeadlineEIS.Text = dr.Item("datEISDeadline")
                    dtpDeadlineEIS.Checked = False
                End If
                If IsDBNull(dr.Item("strEISDeadlineComment")) Then

                Else
                    txtAllEISDeadlineComment.Text = dr.Item("strEISDeadlineComment")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadQASpecificData()
        Try
            dtpQAStarted.Value = Today
            dtpQAPassed.Value = Today
            dtpQAPassed.Checked = False
            cboEISQAStatus.Text = ""
            cboEISQAStaff.Text = ""
            dtpQAStatus.Value = Today
            dtpQACompleted.Value = Today
            dtpQACompleted.Checked = False
            txtQAComments.Clear()
            txtFITrackingNumber.Text = ""
            txtAllFITrackingNumbers.Clear()
            txtPointTrackingNumber.Text = ""
            txtAllPointTrackingNumbers.Clear()
            chbFIErrors.Checked = False
            chbPointErrors.Checked = False
            dtpEISDeadline.Value = Today
            dtpEISDeadline.Checked = False
            txtEISDeadlineComment.Clear()
            txtAllEISDeadlineComment.Clear()

            Dim SQL As String = "Select * " &
            "from EIS_QAAdmin " &
            "where inventoryYear = @inventoryYear " &
            "and FacilitySiteID = @FacilitySiteID "

            Dim params As SqlParameter() = {
                New SqlParameter("@inventoryYear", cboEILogYear.Text),
                New SqlParameter("@FacilitySiteID", mtbEILogAIRSNumber.Text)
            }

            Dim dr As DataRow = DB.GetDataRow(SQL, params)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("datDateQAStart")) Then
                    dtpQAStarted.Value = Today
                Else
                    dtpQAStarted.Text = dr.Item("datDateQAStart")
                End If
                If IsDBNull(dr.Item("datDateQAPass")) Then
                    dtpQAPassed.Value = Today
                    dtpQAPassed.Checked = False
                Else
                    dtpQAPassed.Text = dr.Item("datDateQAPass")
                    dtpQAPassed.Checked = True
                End If
                If IsDBNull(dr.Item("QAStatusCode")) Then
                    cboEISQAStatus.Text = ""
                Else
                    cboEISQAStatus.SelectedValue = dr.Item("QAStatusCode")
                End If
                If IsDBNull(dr.Item("datQAStatus")) Then
                    dtpQAStatus.Value = Today
                Else
                    dtpQAStatus.Text = dr.Item("datQAStatus")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    cboEISQAStaff.Text = ""
                Else
                    cboEISQAStaff.Text = dr.Item("strDMUResponsibleStaff")
                End If
                If IsDBNull(dr.Item("datQAComplete")) Then
                    dtpQACompleted.Value = Today
                    dtpQACompleted.Checked = False
                Else
                    dtpQACompleted.Text = dr.Item("datQAComplete")
                    dtpQACompleted.Checked = True
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtQAComments.Clear()
                    txtAllQAComments.Clear()
                Else
                    txtAllQAComments.Clear()
                    txtAllQAComments.Text = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("strFITrackingNumber")) Then
                    txtFITrackingNumber.Text = ""
                    txtAllFITrackingNumbers.Clear()
                Else
                    txtFITrackingNumber.Text = ""
                    txtAllFITrackingNumbers.Text = dr.Item("strFITrackingNumber")
                End If
                If IsDBNull(dr.Item("strPointTrackingNumber")) Then
                    txtPointTrackingNumber.Text = ""
                    txtAllPointTrackingNumbers.Clear()
                Else
                    txtPointTrackingNumber.Text = ""
                    txtAllPointTrackingNumbers.Text = dr.Item("strPointTrackingNumber")
                End If
                If IsDBNull(dr.Item("strFIError")) Then
                    chbFIErrors.Checked = False
                Else
                    If dr.Item("strFIError") = "True" Then
                        chbFIErrors.Checked = True
                    Else
                        chbFIErrors.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strPointError")) Then
                    chbPointErrors.Checked = False
                Else
                    If dr.Item("strpointError") = "True" Then
                        chbPointErrors.Checked = True
                    Else
                        chbPointErrors.Checked = False
                    End If
                End If
            End If

            If cboEILogStatusCode.SelectedText <> "" AndAlso cboEILogStatusCode.SelectedValue >= 4 Then
                pnlQAProcess.Enabled = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnViewEISStats_Click(sender As Object, e As EventArgs) Handles btnViewEISStats.Click
        ViewEISStats()
    End Sub

    Private Sub ViewEISStats()
        Try

            If cboEISStatisticsYear.Text = "" Then
                MessageBox.Show("Please select a valid year first.")
                Exit Sub
            End If

            txtSelectedEISStatYear.Text = cboEISStatisticsYear.Text
            txtSelectedEISMailout.Text = cboEISStatisticsYear.Text
            txtEISStatsEnrollmentYear.Text = cboEISStatisticsYear.Text

            Dim query As String = "SELECT * FROM (SELECT COUNT(*) AS EISUniverse FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear) AS t1, 
                (SELECT COUNT(*) AS EISMailout FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strMailout = '1') AS t2, 
                (SELECT COUNT(*) AS EISEnrollment FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1') AS t3, 
                (SELECT COUNT(*) AS EISUNEnrollment FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strMailout = '1' AND strEnrollment = '0') AS t4, 
                (SELECT COUNT(*) AS EISNoActivity FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strOptOut IS NULL AND strEnrollment = '1') AS t5, 
                (SELECT COUNT(*) AS EISOptsIn FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strOptOut = '0' AND strEnrollment = '1') AS t6, 
                (SELECT COUNT(*) AS EISOptsOut FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strMailout = '1' AND strEnrollment = '1' AND strOptOut = '1' AND strEnrollment = '1') AS t7, 
                (SELECT COUNT(*) AS EISSubmittal FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1' AND eisstatuscode >= '3' AND strOptOut = '0') AS t8, 
                (SELECT COUNT(*) AS EISInProgress FROM EIS_Admin WHERE active = '1' AND inventoryYear = @inventoryyear AND strEnrollment = '1' AND eisStatuscode = '2' AND strEnrollment = '1' AND strOptOut = '0') AS t9, 
                (SELECT COUNT(*) AS EISQABegan FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strMailout = '1' AND strEnrollment = '1' AND EISAccesscode = '2' AND eisstatuscode = '4' AND strOptOut = '0') AS t10, 
                (SELECT COUNT(*) AS EISEPASubmitted FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strMailout = '1' AND strEnrollment = '1' AND EISAccesscode = '0' AND eisstatuscode = '5' AND strOptOut = '0') AS t11, 
                (SELECT COUNT(*) AS EISFinalized FROM EIS_Admin WHERE active = '1' AND inventoryYear = @inventoryyear AND strEnrollment = '1' AND (EISStatusCode = '3' OR EISStatusCode = '4' OR EISStatusCode = '5') ) AS t12, 
                (SELECT COUNT(*) AS QASubmittedToDo FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1' AND eisstatuscode >= 3 AND strOptOut = '0' AND NOT EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID) ) AS t13, 
                (SELECT COUNT(*) AS QAOptOutToDo FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1' AND (eisstatuscode = 3 OR eisstatuscode = 4) AND (strOptOut = '1' OR strOptout IS NULL) AND NOT EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID) ) AS t14, 
                (SELECT COUNT(*) AS QASubmittedBegan FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1' AND eisstatuscode >= 3 AND strOptOut = '0' AND EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID AND datQAComplete IS NULL) ) AS t15, 
                (SELECT COUNT(*) AS QAOptOutBegan FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1' AND (eisstatuscode = '3' OR eisstatuscode = '4') AND (strOptOut = '1' OR strOptout IS NULL) AND (NOT EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID AND datQAComplete IS NULL) OR EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID AND datQAComplete IS NULL) ) ) AS t16, 
                (SELECT COUNT(*) AS QASubmittedToEPA FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1' AND eisstatuscode >= '3' AND strOptOut = '0' AND EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID AND datQAComplete IS NOT NULL) ) AS t17, 
                (SELECT COUNT(*) AS QAOptOutToEPA FROM EIS_Admin WHERE active = '1' AND inventoryyear = @inventoryyear AND strEnrollment = '1' AND eisstatuscode = '5' AND (strOptOut = '1' OR strOptout IS NULL) AND (NOT EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID) OR EXISTS (SELECT * FROM EIS_QAAdmin WHERE EIS_QAAdmin.inventoryYear = EIS_Admin.inventoryYEar AND EIS_QAAdmin.facilitysiteID = EIS_Admin.facilitysiteID AND datQAComplete IS NOT NULL) ) ) AS t18, 
                (SELECT COUNT(*) AS FIPassed FROM EIS_Admin, EIS_QAAdmin WHERE EIS_Admin.InventoryYear = EIS_QAAdmin.inventoryYEar AND EIS_Admin.facilitysiteID = EIS_QAAdmin.facilitysiteID AND eis_qaAdmin.qaStatusCode = '2' AND eis_admin.inventoryyear = @inventoryyear) AS t19"

            Dim param As New SqlParameter("@inventoryyear", cboEISStatisticsYear.Text)

            Dim dr As DataRow = DB.GetDataRow(query, param)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("EISUniverse")) Then
                    txtEISActiveEIUniverse.Clear()
                Else
                    txtEISActiveEIUniverse.Text = dr.Item("EISUniverse")
                End If
                If IsDBNull(dr.Item("EISMailout")) Then
                    txtEISMailout.Clear()
                Else
                    txtEISMailout.Text = dr.Item("EISMailout")
                End If
                If IsDBNull(dr.Item("EISEnrollment")) Then
                    txtEISEnrolled.Clear()
                Else
                    txtEISEnrolled.Text = dr.Item("EISEnrollment")
                End If
                If IsDBNull(dr.Item("EISUnenrollment")) Then
                    txtEISUnenrolled.Clear()
                Else
                    txtEISUnenrolled.Text = dr.Item("EISUnenrollment")
                End If
                If IsDBNull(dr.Item("EISNoActivity")) Then
                    txtEISNoActivity.Clear()
                Else
                    txtEISNoActivity.Text = dr.Item("EISNoActivity")
                End If
                If IsDBNull(dr.Item("EISOptsIn")) Then
                    txtEISOptedIn.Clear()
                Else
                    txtEISOptedIn.Text = dr.Item("EISOptsIn")
                End If
                If IsDBNull(dr.Item("EISOptsOut")) Then
                    txtEISOptedOut.Clear()
                Else
                    txtEISOptedOut.Text = dr.Item("EISOptsOut")
                End If
                If IsDBNull(dr.Item("EISInProgress")) Then
                    txtEISInProgress.Clear()
                Else
                    txtEISInProgress.Text = dr.Item("EISInProgress")
                End If
                If IsDBNull(dr.Item("EISSubmittal")) Then
                    txtEISSubmitted.Clear()
                Else
                    txtEISSubmitted.Text = dr.Item("EISSubmittal")
                End If
                If IsDBNull(dr.Item("EISQABegan")) Then
                    txtEISQABegan.Clear()
                Else
                    txtEISQABegan.Text = dr.Item("EISQABegan")
                End If

                If IsDBNull(dr.Item("EISFinalized")) Then
                    txtEISFinalized.Clear()
                Else
                    txtEISFinalized.Text = dr.Item("EISFinalized")
                End If

                If IsDBNull(dr.Item("QASubmittedToDo")) Then
                    txtEISSubmittedToDo.Clear()
                Else
                    txtEISSubmittedToDo.Text = dr.Item("QASubmittedToDO")
                End If
                If IsDBNull(dr.Item("QASubmittedBegan")) Then
                    txtEISSubmittedBegan.Clear()
                Else
                    txtEISSubmittedBegan.Text = dr.Item("QASubmittedBegan")
                End If
                If IsDBNull(dr.Item("QASubmittedToEPA")) Then
                    txtEISSubmittedToEPA.Clear()
                Else
                    txtEISSubmittedToEPA.Text = dr.Item("QASubmittedToEPA")
                End If

                If IsDBNull(dr.Item("QAOptOutToDo")) Then
                    txtEISOpOutToDo.Clear()
                Else
                    txtEISOpOutToDo.Text = dr.Item("QAOptOutToDo")
                End If
                If IsDBNull(dr.Item("QAOptOutBegan")) Then
                    txtEISOpOutBegan.Clear()
                Else
                    txtEISOpOutBegan.Text = dr.Item("QAOptOutBegan")
                End If
                If IsDBNull(dr.Item("QAOptOutToEPA")) Then
                    txtEISOpOutToEPA.Clear()
                Else
                    txtEISOpOutToEPA.Text = dr.Item("QAOptOutToEPA")
                End If
                If IsDBNull(dr.Item("FIPassed")) Then
                    txtEISFIPassed.Clear()
                Else
                    txtEISFIPassed.Text = dr.Item("FIPassed")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISEIUniverse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISEIUniverse.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If
            EIS_VIEW(txtSelectedEISStatYear.Text, "", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Active EIS Universe Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISMailOutTotal_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISMailOutTotal.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If
            EIS_VIEW(txtSelectedEISStatYear.Text, "1", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Mailout Total Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISEnrolled_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISEnrolled.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Enrolled Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISNoActivity_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISNoActivity.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If
            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "Null", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "No Activity Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISUnenrolled_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISUnenrolled.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If
            EIS_VIEW(txtSelectedEISStatYear.Text, "", "0", "1", "", "", "", "")
            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Unenrolled Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISInProgress_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISInProgress.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", " and EISStatusCode = 2 ", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "In Progress Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISOptedIn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISOptedIn.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Opted-In Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISOptedOut_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISOptedOut.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "1", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Opted-Out Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISSubmitted_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISSubmitted.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", " and EISStatusCode >= 3 ", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "In Progress Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISFinalized_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISFinalized.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "",
                     " and (EISStatusCode = '3' or EISStatusCode = '4' or EISStatusCode = '5') ", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Finalized Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISQABegan_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISQABegan.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "1", "1", "1", " and (strOptOut is null or strOptout = '0') ", "4", "2", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "In Progress Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISSubmittedToEPA_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISSubmittedToEPA.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
                 " and EISStatusCode >= 3 ", "", " and datQAComplete is not null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, EPA Submitted Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvEISStats_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvEISStats.MouseUp
        Try
            Dim CurrentTabPage As TabPage = TCEISStats.SelectedTab
            Dim hti As DataGridView.HitTestInfo = dgvEISStats.HitTest(e.X, e.Y)
            Dim i As Integer = 0

            If hti.RowIndex = -1 And hti.ColumnIndex <> -1 Then
                If dgvEISStats.Columns(hti.ColumnIndex).HeaderText = " " Then
                    If dgvEISStats(0, 0).Value = True Then
                        For i = 0 To dgvEISStats.Rows.Count - 1
                            dgvEISStats(0, i).Value = False
                        Next
                    Else
                        For i = 0 To dgvEISStats.Rows.Count - 1
                            dgvEISStats(0, i).Value = True
                        Next
                    End If
                End If
            Else
                If hti.RowIndex <> -1 Then
                    mtbEISLogAIRSNumber.Text = dgvEISStats(1, hti.RowIndex).Value
                End If
            End If

            If CurrentTabPage.Name.ToString = "TPEISStatMailout" Then
                If dgvEISStats.RowCount > 0 And hti.RowIndex <> -1 Then
                    dgvEISStats.Enabled = False

                    txtEISStatsMailoutFacilityName.Clear()
                    txtEISStatsMailoutPrefix.Clear()
                    txtEISStatsMailoutFirstName.Clear()
                    txtEISStatsMailoutLastName.Clear()
                    txtEISStatsMailoutCompanyName.Clear()
                    txtEISStatsMailoutAddress1.Clear()
                    txtEISStatsMailoutAddress2.Clear()
                    txtEISStatsMailoutCity.Clear()
                    txtEISStatsMailoutState.Clear()
                    txtEISStatsMailoutZipCode.Clear()
                    txtEISStatsMailoutEmailAddress.Clear()
                    txtEISStatsMailoutComments.Clear()
                    txtEISStatsMailoutUpdateUser.Clear()
                    txtEISStatsMailoutUpdateDate.Clear()
                    txtEISStatsMailoutCreateDate.Clear()

                    If IsDBNull(dgvEISStats(1, hti.RowIndex).Value) Then
                        txtEISStatsMailoutAIRSNumber.Clear()
                    Else
                        txtEISStatsMailoutAIRSNumber.Text = dgvEISStats(1, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvEISStats(3, hti.RowIndex).Value) Then
                        txtSelectedEISMailout.Clear()
                    Else
                        txtSelectedEISMailout.Text = dgvEISStats(3, hti.RowIndex).Value
                    End If

                    Dim SQL As String = "Select " &
                    "strFacilityName, " &
                    "strContactCompanyName, strContactAddress1, " &
                    "strContactAddress2, strContactCity, " &
                    "strcontactstate, strcontactzipCode, " &
                    "strcontactFirstName, strcontactLastName, " &
                    "strContactPrefix, strContactEmail, " &
                    "stroperationalStatus, strClass, " &
                    "strcomment, UpdateUser, " &
                    "updateDateTime, CreateDateTime " &
                     "from EIS_Mailout " &
                     "where intInventoryyear = @year " &
                     "and FacilitySiteID = @airs "

                    Dim params As SqlParameter() = {
                        New SqlParameter("@year", txtSelectedEISMailout.Text),
                        New SqlParameter("@airs", txtEISStatsMailoutAIRSNumber.Text)
                    }

                    Dim dr As DataRow = DB.GetDataRow(SQL, params)
                    If dr IsNot Nothing Then
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            txtEISStatsMailoutFacilityName.Clear()
                        Else
                            txtEISStatsMailoutFacilityName.Text = dr.Item("strFacilityName")
                        End If
                        If IsDBNull(dr.Item("strContactCompanyName")) Then
                            txtEISStatsMailoutCompanyName.Clear()
                        Else
                            txtEISStatsMailoutCompanyName.Text = dr.Item("strContactCompanyName")
                        End If
                        If IsDBNull(dr.Item("strContactAddress1")) Then
                            txtEISStatsMailoutAddress1.Clear()
                        Else
                            txtEISStatsMailoutAddress1.Text = dr.Item("strContactAddress1")
                        End If
                        If IsDBNull(dr.Item("strContactAddress2")) Then
                            txtEISStatsMailoutAddress2.Clear()
                        Else
                            txtEISStatsMailoutAddress2.Text = dr.Item("strContactAddress2")
                        End If
                        If IsDBNull(dr.Item("strContactCity")) Then
                            txtEISStatsMailoutCity.Clear()
                        Else
                            txtEISStatsMailoutCity.Text = dr.Item("strContactCity")
                        End If
                        If IsDBNull(dr.Item("strcontactstate")) Then
                            txtEISStatsMailoutState.Clear()
                        Else
                            txtEISStatsMailoutState.Text = dr.Item("strcontactstate")
                        End If
                        If IsDBNull(dr.Item("strcontactzipCode")) Then
                            txtEISStatsMailoutZipCode.Clear()
                        Else
                            txtEISStatsMailoutZipCode.Text = dr.Item("strcontactzipCode")
                        End If
                        If IsDBNull(dr.Item("strcontactFirstName")) Then
                            txtEISStatsMailoutFirstName.Clear()
                        Else
                            txtEISStatsMailoutFirstName.Text = dr.Item("strcontactFirstName")
                        End If
                        If IsDBNull(dr.Item("strcontactLastName")) Then
                            txtEISStatsMailoutLastName.Clear()
                        Else
                            txtEISStatsMailoutLastName.Text = dr.Item("strcontactLastName")
                        End If
                        If IsDBNull(dr.Item("strContactPrefix")) Then
                            txtEISStatsMailoutPrefix.Clear()
                        Else
                            txtEISStatsMailoutPrefix.Text = dr.Item("strContactPrefix")
                        End If
                        If IsDBNull(dr.Item("strContactEmail")) Then
                            txtEISStatsMailoutEmailAddress.Clear()
                        Else
                            txtEISStatsMailoutEmailAddress.Text = dr.Item("strContactEmail")
                        End If
                        If IsDBNull(dr.Item("strcomment")) Then
                            txtEISStatsMailoutComments.Clear()
                        Else
                            txtEISStatsMailoutComments.Text = dr.Item("strcomment")
                        End If
                        If IsDBNull(dr.Item("UpdateUser")) Then
                            txtEISStatsMailoutUpdateUser.Clear()
                        Else
                            txtEISStatsMailoutUpdateUser.Text = dr.Item("UpdateUser")
                        End If
                        If IsDBNull(dr.Item("updateDateTime")) Then
                            txtEISStatsMailoutUpdateDate.Clear()
                        Else
                            txtEISStatsMailoutUpdateDate.Text = dr.Item("updateDateTime")
                        End If
                        If IsDBNull(dr.Item("CreateDateTime")) Then
                            txtEISStatsMailoutCreateDate.Clear()
                        Else
                            txtEISStatsMailoutCreateDate.Text = dr.Item("CreateDateTime")
                        End If
                    End If
                End If
            End If
            dgvEISStats.Enabled = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveEISStatMailout_Click(sender As Object, e As EventArgs) Handles btnSaveEISStatMailout.Click
        Try
            If txtSelectedEISMailout.Text <> "" And txtEISStatsMailoutAIRSNumber.Text <> "" Then
                Dim SQL As String = "UPdate EIS_Mailout set " &
                    "strFacilityName = @strFacilityName, " &
                    "strContactCompanyName = @strContactCompanyName, " &
                    "strContactAddress1 = @strContactAddress1, " &
                    "strContactAddress2 = @strContactAddress2, " &
                    "strContactCity = @strContactCity, " &
                    "strContactState = @strContactState, " &
                    "strContactZipCode = @strContactZipCode, " &
                    "strContactFirstName = @strContactFirstName, " &
                    "strContactLastName = @strContactLastName, " &
                    "strContactPrefix = @strContactPrefix, " &
                    "strContactEmail = @strContactEmail, " &
                    "strComment = @strComment " &
                    "where intInventoryYear = @intInventoryYear " &
                    "and FacilitySiteID = @FacilitySiteID "

                Dim params As SqlParameter() = {
                    New SqlParameter("@strFacilityName", txtEISStatsMailoutFacilityName.Text),
                    New SqlParameter("@strContactCompanyName", txtEISStatsMailoutCompanyName.Text),
                    New SqlParameter("@strContactAddress1", txtEISStatsMailoutAddress1.Text),
                    New SqlParameter("@strContactAddress2", txtEISStatsMailoutAddress2.Text),
                    New SqlParameter("@strContactCity", txtEISStatsMailoutCity.Text),
                    New SqlParameter("@strContactState", txtEISStatsMailoutState.Text),
                    New SqlParameter("@strContactZipCode", txtEISStatsMailoutZipCode.Text),
                    New SqlParameter("@strContactFirstName", txtEISStatsMailoutFirstName.Text),
                    New SqlParameter("@strContactLastName", txtEISStatsMailoutLastName.Text),
                    New SqlParameter("@strContactPrefix", txtEISStatsMailoutPrefix.Text),
                    New SqlParameter("@strContactEmail", txtEISStatsMailoutEmailAddress.Text),
                    New SqlParameter("@strComment", txtEISStatsMailoutComments.Text),
                    New SqlParameter("@intInventoryYear", txtSelectedEISStatYear.Text),
                    New SqlParameter("@FacilitySiteID", txtEISStatsMailoutAIRSNumber.Text)
                }

                DB.RunCommand(SQL, params)

                MsgBox("Data updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Please select a valid year from the dropdown and a valid contact from the resulting list." & vbCrLf &
                       "NO DATA UPDATED", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISStatsEnrollment_Click(sender As Object, e As EventArgs) Handles btnEISStatsEnrollment.Click
        Try
            Dim EISConfirm As String = InputBox("Type in the EIS Year that you have selected to enroll Facilities into the QA process.", Me.Text)

            If EISConfirm = txtEISStatsEnrollmentYear.Text Then
                Dim temp As String = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next
                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "

                    Dim SQL As String = "Update EIS_Admin set " &
                    "strEnrollment = '1', " &
                    "EISAccessCode = '1', " &
                    "EISStatusCode = '1', " &
                    "DatEISStatus = getdate() " &
                    "where inventoryyear = @inventoryyear " &
                    "and strEnrollment = '0' " &
                    "and strOptOut is null " &
                    "and EISAccessCode = '0' " &
                    "and EISStatusCode = '0' " &
                    "and strMailout = '1' " &
                    temp

                    Dim param As New SqlParameter("@inventoryyear", EISConfirm)
                    DB.RunCommand(SQL, param)
                End If

                MsgBox("Facilities enrolled in " & EISConfirm & " EIS.", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Year does not match selected EIS year")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISStatsRemoveEnrollment_Click(sender As Object, e As EventArgs) Handles btnEISStatsRemoveEnrollment.Click
        Try
            Dim EISConfirm As String = InputBox("Type in the EIS Year that you have selected to enroll Facilities into the QA process.", Me.Text)

            If EISConfirm = txtEISStatsEnrollmentYear.Text Then
                Dim temp As String = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next
                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "
                    Dim SQL As String = "Update EIS_Admin set " &
                    "strEnrollment = '0', " &
                    "EISAccessCode = '1', " &
                    "EISStatusCode = '1', " &
                    "DatEISStatus = getdate() " &
                    "where inventoryyear = @inventoryyear " &
                    "and strEnrollment = '1' " &
                    "and strOptOut is null " &
                    "and EISAccessCode = '0' " &
                    "and EISStatusCode = '0' " &
                    "and strMailout = '1' " &
                    temp

                    Dim param As New SqlParameter("@inventoryyear", EISConfirm)
                    DB.RunCommand(SQL, param)
                End If

                MsgBox("Facilities enrolled in " & EISConfirm & " EIS.", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Year does not match selected EIS year")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCloseOutEIS_Click(sender As Object, e As EventArgs) Handles btnCloseOutEIS.Click
        Try
            Dim EISConfirm As String = InputBox("Type in the EIS Year that you have selected to close out.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                Dim query As String = "Update EIS_Admin set " &
                " EISAccessCode = '2' " &
                " where inventoryYear = @inventoryYear " &
                " and FacilitySiteID in ({0}) "

                Dim paramNameList As New List(Of String)
                Dim paramList As New List(Of SqlParameter)

                paramList.Add(New SqlParameter("@inventoryYear", EISConfirm))

                ' TODO DWW: Change to table-valued parameter instead of dynamically built "IN" list
                Dim paramName As String
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    paramName = "@site" & Replace(dgvEISStats(1, i).Value, "-", "")
                    paramNameList.Add(paramName)
                    paramList.Add(New SqlParameter(paramName, dgvEISStats(1, i).Value))
                Next
                Dim inClause As String = String.Join(",", paramNameList)

                If paramNameList.Count > 0 Then
                    DB.RunCommand(String.Format(query, inClause), paramList.ToArray)
                    ViewEISStats()
                    MsgBox(EISConfirm & " Emission Inventory Year closed out.", MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox("No facilities displayed.")
                End If
            Else
                MsgBox("Year does not match selected EIS year.")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISBeginQA_Click(sender As Object, e As EventArgs) Handles btnEISBeginQA.Click
        Try
            Dim EISConfirm As String = InputBox("Type in the EIS Year that you have selected to move Facilities into the QA process.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then

                Dim selection As Boolean = False
                For Each row As DataGridViewRow In dgvEISStats.Rows
                    If row.Cells(0).Value Then selection = True
                Next
                If Not selection Then
                    MsgBox("No facilities selected.")
                    Exit Sub
                End If

                Dim queryList As New List(Of String)
                Dim paramsList As New List(Of SqlParameter())

                ' Update EIS_Admin for non-opted-out facilities
                Dim query1 As String = "Update EIS_Admin set " &
                    "EISAccessCode = '2', " &
                    "EISStatusCode = '4', " &
                    "datEISstatus = GETDATE(), " &
                    "UpdateUser = @updateuser, " &
                    "updatedatetime = getdate() " &
                    "where strOptOut = '0' " &
                    "and inventoryYear = @inventoryYear " &
                    "and FacilitySiteID in ({0}) "

                Dim paramNameList1 As New List(Of String)
                Dim paramList1 As New List(Of SqlParameter)

                paramList1.Add(New SqlParameter("@updateuser", CurrentUser.AlphaName))
                paramList1.Add(New SqlParameter("@inventoryYear", EISConfirm))

                ' TODO DWW: Change to table-valued parameter instead of dynamically built "IN" list
                Dim paramName As String
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True And dgvEISStats(6, i).Value = "No" Then
                        paramName = "@site" & Replace(dgvEISStats(1, i).Value, "-", "")
                        paramNameList1.Add(paramName)
                        paramList1.Add(New SqlParameter(paramName, dgvEISStats(1, i).Value))
                    End If
                Next

                If paramNameList1.Count > 0 Then
                    queryList.Add(String.Format(query1, String.Join(",", paramNameList1)))
                    paramsList.Add(paramList1.ToArray)
                End If

                ' Update EIS_Admin for opted-out facilities
                Dim query2 As String = "Update EIS_Admin set " &
                    "EISAccessCode = '2', " &
                    "EISStatusCode = '5', " &
                    "datEISstatus = getdate(), " &
                    "UpdateUser = @UpdateUser, " &
                    "updatedatetime = getdate() " &
                    "where strOptOut = '1' " &
                    "and inventoryYear = @inventoryYear " &
                    "and FacilitySiteID in ({0}) "

                Dim paramNameList2 As New List(Of String)
                Dim paramList2 As New List(Of SqlParameter)

                paramList2.Add(New SqlParameter("@updateuser", CurrentUser.AlphaName))
                paramList2.Add(New SqlParameter("@inventoryYear", EISConfirm))

                ' TODO DWW: Change to table-valued parameter instead of dynamically built "IN" list
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True And dgvEISStats(6, i).Value = "Yes" Then
                        paramName = "@site" & Replace(dgvEISStats(1, i).Value, "-", "")
                        paramNameList2.Add(paramName)
                        paramList2.Add(New SqlParameter(paramName, dgvEISStats(1, i).Value))
                    End If
                Next

                If paramNameList2.Count > 0 Then
                    queryList.Add(String.Format(query2, String.Join(",", paramNameList2)))
                    paramsList.Add(paramList2.ToArray)
                End If

                ' Update EIS_QAAdmin with new facilities
                Dim query3 As String = "INSERT INTO EIS_QAAdmin (INVENTORYYEAR, FACILITYSITEID, DATDATEQASTART, DATDATEQAPASS, QASTATUSCODE, DATQASTATUS, STRDMURESPONSIBLESTAFF, DATQACOMPLETE, STRCOMMENT, ACTIVE, UPDATEUSER, UPDATEDATETIME, CREATEDATETIME, STRFITRACKINGNUMBER, STRPOINTTRACKINGNUMBER, STRFIERROR, STRPOINTERROR)
                    SELECT @INVENTORYYEAR, @FACILITYSITEID, GETDATE(), '', '1', GETDATE(), @UPDATEUSER, '', '', '1', @UPDATEUSER, GETDATE(), GETDATE(), '', '', '', ''
                    WHERE NOT EXISTS (SELECT * FROM EIS_QAAdmin
                    WHERE inventoryYear = @INVENTORYYEAR AND FacilitySiteID = @FACILITYSITEID) 
                    AND EXISTS (SELECT * FROM EIS_Admin
                    WHERE inventoryYear = @INVENTORYYEAR AND FacilitySiteID = @FACILITYSITEID AND strOptOut = '0')"

                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True Then
                        queryList.Add(query3)
                        paramsList.Add({
                            New SqlParameter("@INVENTORYYEAR", EISConfirm),
                            New SqlParameter("@FACILITYSITEID", dgvEISStats(1, i).Value),
                            New SqlParameter("@UPDATEUSER", CurrentUser.AlphaName)
                        })
                    End If
                Next

                DB.RunCommand(queryList, paramsList)

                Dim spName As String = "PD_EIS_QASTART"
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True Then
                        Dim param As SqlParameter() = {
                            New SqlParameter("@AIRSNUMBER_IN", dgvEISStats(1, i).Value),
                            New SqlParameter("@INTYEAR_IN", EISConfirm)
                        }
                        DB.SPRunCommand(spName, param)
                    End If
                Next

                ViewEISStats()
                MsgBox(EISConfirm & " QA process begun.", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Year does not match selected EIS year.")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnEILogUpdate_Click(sender As Object, e As EventArgs) Handles btnEILogUpdate.Click
        Try
            Dim EISAccess As String = " "
            Dim OptOut As String = ""
            Dim EISStatus As String = ""
            Dim Enrollment As String = ""
            Dim Mailout As String = ""
            Dim ActiveStatus As String = ""
            Dim IncorrectlyOptedOut As String = ""

            If rdbEILogMailoutYes.Checked = True Then
                Mailout = "1"
            Else
                If rdbEILogMailoutNo.Checked = True Then
                    Mailout = "0"
                Else
                    Mailout = ""
                End If
            End If
            If rdbEILogEnrolledYes.Checked = True Then
                Enrollment = "1"
            Else
                If rdbEILogEnrolledNo.Checked = True Then
                    Enrollment = "0"
                Else
                    Enrollment = "0"
                End If
            End If
            If rdbEILogOpOutYes.Checked = True Then
                OptOut = "1"
            Else
                If rdbEILogOpOutNo.Checked = True Then
                    OptOut = "0"
                Else
                    OptOut = ""
                End If
            End If
            If chbOptedOutIncorrectly.Checked = True Then
                IncorrectlyOptedOut = "1"
            Else
                IncorrectlyOptedOut = "0"
            End If
            If cboEILogStatusCode.SelectedValue <> "" Then
                EISStatus = cboEILogStatusCode.SelectedValue
            End If
            If cboEILogAccessCode.SelectedValue <> "" Then
                EISAccess = cboEILogAccessCode.SelectedValue
            End If
            If rdbEILogActiveYes.Checked = True Then
                ActiveStatus = "1"
            Else
                ActiveStatus = "0"
            End If

            Dim SQL As String = "Select FacilitySiteID from EIS_Admin " &
            "where inventoryyear = @inventoryyear " &
            "and FacilitySiteID = @FacilitySiteID "

            Dim params As SqlParameter() = {
                New SqlParameter("@inventoryyear", cboEILogYear.Text),
                New SqlParameter("@FacilitySiteID", mtbEILogAIRSNumber.Text)
            }

            If Not DB.ValueExists(SQL, params) Then
                MsgBox("The facility is not currently in the EIS universe for the selected year." & vbCrLf &
                       "Use the Add New Facility to Year." & vbCrLf & vbCrLf & "NO DATA SAVED", MsgBoxStyle.Information, Me.Text)

                Exit Sub
            End If

            SQL = "Update EIS_Admin set " &
            "EISStatusCode = @EISStatusCode, " &
            "DatEISStatus = @DatEISStatus, " &
            "EISAccessCode = @EISAccessCode, " &
            "strOptOut = @strOptOut, " &
            "strIncorrectOptOut = @strIncorrectOptOut, " &
            "strMailout = @strMailout, " &
            "strEnrollment = @strEnrollment, " &
            "datEnrollment = @datEnrollment, " &
            "strComment = @strComment, " &
            "active = @active, " &
            "updateUser = @updateUser, " &
            "updateDateTime = getdate() " &
            "where inventoryyear = @inventoryyear " &
            "and FacilitySiteID = @FacilitySiteID "

            Dim params2 As SqlParameter() = {
                New SqlParameter("@EISStatusCode", EISStatus),
                New SqlParameter("@DatEISStatus", dtpEILogStatusDateSubmit.Value),
                New SqlParameter("@EISAccessCode", EISAccess),
                New SqlParameter("@strOptOut", OptOut),
                New SqlParameter("@strIncorrectOptOut", IncorrectlyOptedOut),
                New SqlParameter("@strMailout", Mailout),
                New SqlParameter("@strEnrollment", Enrollment),
                New SqlParameter("@datEnrollment", dtpEILogDateEnrolled.Value),
                New SqlParameter("@strComment", txtEILogComments.Text),
                New SqlParameter("@active", ActiveStatus),
                New SqlParameter("@updateUser", CurrentUser.AlphaName),
                New SqlParameter("@inventoryyear", cboEILogYear.Text),
                New SqlParameter("@FacilitySiteID", mtbEILogAIRSNumber.Text)
            }

            DB.RunCommand(SQL, params2)

            If dtpDeadlineEIS.Checked = True Then
                Dim DeadLineComments As String = ""
                If txtAllEISDeadlineComment.Text.Contains(dtpDeadlineEIS.Text & "(deadline)- " & CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf &
                txtEISDeadlineComment.Text) Then
                Else
                    DeadLineComments = dtpDeadlineEIS.Text & "(deadline)- " & CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf &
                    txtEISDeadlineComment.Text &
                    vbCrLf & vbCrLf & txtAllEISDeadlineComment.Text

                    SQL = "update EIS_Admin set " &
                    "datEISDeadline = @datEISDeadline,  " &
                    "strEISDeadlineComment = @strEISDeadlineComment  " &
                    "where INventoryyear = @INventoryyear " &
                    "and FacilitySiteID = @FacilitySiteID  "

                    Dim params3 As SqlParameter() = {
                        New SqlParameter("@datEISDeadline", dtpDeadlineEIS.Text),
                        New SqlParameter("@strEISDeadlineComment", DeadLineComments),
                        New SqlParameter("@INventoryyear", cboEILogYear.Text),
                        New SqlParameter("@FacilitySiteID", mtbEILogAIRSNumber.Text)
                    }

                    DB.RunCommand(SQL, params3)
                End If
            End If

            If rdbEILogOpOutYes.Checked = False Then
                Dim QAStart As String = ""
                Dim QAPass As String = ""
                Dim QAStatusCode As String = ""
                Dim QAStatusDate As String = ""
                Dim StaffResponsible As String = ""
                Dim QAComplete As String = ""
                Dim QAComments As String = ""
                Dim FITracking As String = ""
                Dim pointTracking As String = ""
                Dim FIError As String = ""
                Dim pointError As String = ""

                QAStart = dtpQAStarted.Text
                If dtpQAPassed.Checked = True Then
                    QAPass = dtpQAPassed.Text
                Else
                    QAPass = ""
                End If
                If dtpQACompleted.Checked = True Then
                    QAComplete = dtpQACompleted.Text
                Else
                    QAComplete = ""
                End If
                QAStatusCode = cboEISQAStatus.SelectedValue
                QAStatusDate = TodayFormatted
                StaffResponsible = cboEISQAStaff.Text
                If txtQAComments.Text = "" Then
                    If txtAllQAComments.Text = "" Then
                        QAComments = ""
                    Else
                        QAComments = txtAllQAComments.Text
                    End If
                Else
                    If txtAllQAComments.Text = "" Then
                        QAComments = CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf & txtQAComments.Text
                    Else
                        QAComments = CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf & txtQAComments.Text & vbCrLf & vbCrLf &
                             txtAllQAComments.Text
                    End If
                End If
                If txtFITrackingNumber.Text = "" Then
                    If txtAllFITrackingNumbers.Text = "" Then
                        FITracking = ""
                    Else
                        FITracking = txtAllFITrackingNumbers.Text
                    End If
                Else
                    If txtAllFITrackingNumbers.Text = "" Then
                        FITracking = CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf & txtFITrackingNumber.Text
                    Else
                        FITracking = CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf & txtFITrackingNumber.Text & vbCrLf & vbCrLf &
                                    txtAllFITrackingNumbers.Text
                    End If
                End If
                If chbFIErrors.Checked = True Then
                    FIError = "True"
                Else
                    FIError = "False"
                End If
                If txtPointTrackingNumber.Text = "" Then
                    If txtAllPointTrackingNumbers.Text = "" Then
                        pointTracking = ""
                    Else
                        pointTracking = txtAllPointTrackingNumbers.Text
                    End If
                Else
                    If txtAllPointTrackingNumbers.Text = "" Then
                        pointTracking = CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf & txtPointTrackingNumber.Text
                    Else
                        pointTracking = CurrentUser.AlphaName & " - " & TodayFormatted & vbCrLf & txtPointTrackingNumber.Text & vbCrLf & vbCrLf &
                                txtAllPointTrackingNumbers.Text
                    End If
                End If
                If chbPointErrors.Checked = True Then
                    pointError = "True"
                Else
                    pointError = "False"
                End If

                SQL = "Update eis_QAAdmin set " &
               "datDateQAStart = @datDateQAStart, " &
               "datDateQAPass = @datDateQAPass, " &
               "QAStatusCode = @QAStatusCode, " &
               "datQAStatus = @datQAStatus, " &
               "strDMUResponsibleStaff = @strDMUResponsibleStaff, " &
               "datQAComplete = @datQAComplete, " &
               "strComment = @strComment, " &
               "active = '1', " &
               "updateuser = @updateuser, " &
               "updateDateTime = getdate(), " &
               "strFITrackingnumber = @strFITrackingnumber, " &
               "strFIError = @strFIError, " &
               "STRPOINTTRACKINGNUMBER = @STRPOINTTRACKINGNUMBER, " &
               "strpointerror = @strpointerror " &
               "where INventoryyear = @INventoryyear " &
               "and FacilitySiteID = @FacilitySiteID "

                Dim params4 As SqlParameter() = {
                    New SqlParameter("@datDateQAStart", QAStart),
                    New SqlParameter("@datDateQAPass", QAPass),
                    New SqlParameter("@QAStatusCode", QAStatusCode),
                    New SqlParameter("@datQAStatus", QAStatusDate),
                    New SqlParameter("@strDMUResponsibleStaff", StaffResponsible),
                    New SqlParameter("@datQAComplete", QAComplete),
                    New SqlParameter("@strComment", QAComments),
                    New SqlParameter("@updateuser", CurrentUser.AlphaName),
                    New SqlParameter("@strFITrackingnumber", FITracking),
                    New SqlParameter("@strFIError", FIError),
                    New SqlParameter("@STRPOINTTRACKINGNUMBER", pointTracking),
                    New SqlParameter("@strpointerror", pointError),
                    New SqlParameter("@INventoryyear", cboEILogYear.Text),
                    New SqlParameter("@FacilitySiteID", mtbEILogAIRSNumber.Text)
                }

                DB.RunCommand(SQL, params4)

                LoadQASpecificData()
            End If

            LoadAdminData()
            MsgBox("Admin Data updated.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEILogAddNewFacility_Click(sender As Object, e As EventArgs) Handles btnEILogAddNewFacility.Click
        Try
            Dim spname As String = "PD_EIS_Data"
            Dim params As SqlParameter() = {
                New SqlParameter("@AIRSNUM", txtEILogSelectedAIRSNumber.Text),
                New SqlParameter("@INTYEAR", txtEILogSelectedYear.Text)
            }
            DB.SPRunCommand(spname, params)

            LoadAdminData()
            MsgBox("New Facility Added", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateQAData_Click(sender As Object, e As EventArgs) Handles btnUpdateQAData.Click
        Try
            Dim QAStart As String = ""
            Dim QAPass As String = ""
            Dim QAStatusCode As String = ""
            Dim QAStatusDate As String = ""
            Dim StaffResponsible As String = ""
            Dim QAComplete As String = ""
            Dim QAComments As String = ""
            Dim FITracking As String = ""
            Dim PointTracking As String = ""
            Dim FIError As String = ""
            Dim PointError As String = ""

            QAStart = dtpQAStarted.Text
            If dtpQAPassed.Checked = True Then
                QAPass = dtpQAPassed.Text
            Else
                QAPass = ""
            End If
            If dtpQACompleted.Checked = True Then
                QAComplete = dtpQACompleted.Text
            Else
                QAComplete = ""
            End If
            QAStatusCode = cboEISQAStatus.SelectedValue
            QAStatusDate = Format(Today, DateFormat)
            StaffResponsible = cboEISQAStaff.Text
            If txtQAComments.Text = "" Then
                If txtAllQAComments.Text = "" Then
                    QAComments = ""
                Else
                    QAComments = txtAllQAComments.Text
                End If
            Else
                If txtAllQAComments.Text = "" Then
                    QAComments = CurrentUser.AlphaName & " - " & Format(Today, DateFormat) & vbCrLf & txtQAComments.Text
                Else
                    QAComments = CurrentUser.AlphaName & " - " & Format(Today, DateFormat) & vbCrLf & txtQAComments.Text & vbCrLf & vbCrLf &
                         txtAllQAComments.Text
                End If
            End If
            If txtFITrackingNumber.Text = "" Then
                If txtAllFITrackingNumbers.Text = "" Then
                    FITracking = ""
                Else
                    FITracking = txtAllFITrackingNumbers.Text
                End If
            Else
                If txtAllFITrackingNumbers.Text = "" Then
                    FITracking = CurrentUser.AlphaName & " - " & Format(Today, DateFormat) & vbCrLf & txtFITrackingNumber.Text
                Else
                    FITracking = CurrentUser.AlphaName & " - " & Format(Today, DateFormat) & vbCrLf & txtFITrackingNumber.Text & vbCrLf & vbCrLf &
                                txtAllFITrackingNumbers.Text
                End If
            End If
            If chbFIErrors.Checked = True Then
                FIError = "True"
            Else
                FIError = "False"
            End If

            If txtPointTrackingNumber.Text = "" Then
                If txtAllPointTrackingNumbers.Text = "" Then
                    PointTracking = ""
                Else
                    PointTracking = txtAllPointTrackingNumbers.Text
                End If
            Else
                If txtAllPointTrackingNumbers.Text = "" Then
                    PointTracking = CurrentUser.AlphaName & " - " & Format(Today, DateFormat) & vbCrLf & txtPointTrackingNumber.Text
                Else
                    PointTracking = CurrentUser.AlphaName & " - " & Format(Today, DateFormat) & vbCrLf & txtPointTrackingNumber.Text & vbCrLf & vbCrLf &
                            txtAllPointTrackingNumbers.Text
                End If
            End If
            If chbPointErrors.Checked = True Then
                PointError = "True"
            Else
                PointError = "False"
            End If

            Dim SQL As String = "Update eis_QAAdmin set " &
            "datDateQAStart = @QAStart & " &
            "datDateQAPass = @QAPass & " &
            "QAStatusCode = @QAStatusCode & " &
            "datQAStatus = @QAStatusDate & " &
            "strDMUResponsibleStaff = @StaffResponsible, " &
            "datQAComplete = @QAComplete & " &
            "strComment = @QAComments, " &
            "active = '1', " &
            "updateuser = @CurrentUser, " &
            "updateDateTime = getdate(), " &
            "strFITrackingnumber = @FITracking, " &
            "strFIError = @FIError, " &
            "STRPOINTTRACKINGNUMBER = @PointTracking, " &
            "strpointerror = @PointError," &
            "where INventoryyear = @cboEILogYear " &
            "and FacilitySiteID = @mtbEILogAIRSNumber "

            Dim params As SqlParameter() = {
                New SqlParameter("@datDateQAStart", QAStart),
                New SqlParameter("@datDateQAPass", QAPass),
                New SqlParameter("@QAStatusCode", QAStatusCode),
                New SqlParameter("@datQAStatus", QAStatusDate),
                New SqlParameter("@strDMUResponsibleStaff", StaffResponsible),
                New SqlParameter("@datQAComplete", QAComplete),
                New SqlParameter("@strComment", QAComments),
                New SqlParameter("@updateuser", CurrentUser),
                New SqlParameter("@strFITrackingnumber", FITracking),
                New SqlParameter("@strFIError", FIError),
                New SqlParameter("@STRPOINTTRACKINGNUMBER", PointTracking),
                New SqlParameter("@strpointerror", PointError),
                New SqlParameter("@INventoryyear", cboEILogYear.Text),
                New SqlParameter("@FacilitySiteID", mtbEILogAIRSNumber.Text)
            }

            DB.RunCommand(SQL, params)

            LoadQASpecificData()

            If dtpQACompleted.Checked = True Then
                Dim spname As String = "PD_EIS_QA_Done"
                Dim params2 As SqlParameter() = {
                    New SqlParameter("@AIRSNUM", txtEILogSelectedAIRSNumber.Text),
                    New SqlParameter("@INTYEAR", txtEILogSelectedYear.Text),
                    New SqlParameter("@DATLASTSUBMIT", dtpQACompleted.Value)
                }
                DB.SPRunCommand(spname, params2)
            End If

            MsgBox("QA data saved.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEIModifyUpdateLocation_Click(sender As Object, e As EventArgs) Handles btnEIModifyUpdateLocation.Click

        If txtEILogSelectedAIRSNumber.Text = "" Then
            MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim Address As String = txtEIModifyLocation.Text
        Dim City As String = txtEIModifyCity.Text
        Dim PostalCode As String = mtbEIModifyZipCode.Text

        If Address <> "" And City <> "" Then
            Dim query As String = "Update EIS_FacilitySiteAddress set " &
            " STRLOCATIONADDRESSTEXT = @Address, " &
            " STRLOCALITYNAME = @City, " &
            " STRLOCATIONADDRESSPOSTALCODE = @PostalCode " &
            " where facilitysiteid = @AirsNumber"

            Dim parameters As SqlParameter()

            parameters = New SqlParameter() {
                New SqlParameter("@Address", Address),
                New SqlParameter("@City", City),
                New SqlParameter("@PostalCode", PostalCode),
                New SqlParameter("@AirsNumber", txtEILogSelectedAIRSNumber.Text)
            }

            DB.RunCommand(query, parameters)

            MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)
        Else
            MsgBox("No data saved." & vbCrLf & "BOTH LOCATION ADDRESS AND CITY ARE REQUIRED" & vbCrLf & vbCrLf & "Sorry for yelling.", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub

    Private Sub btnEIModifyUpdateMailing_Click(sender As Object, e As EventArgs) Handles btnEIModifyUpdateMailing.Click

        If txtEILogSelectedAIRSNumber.Text = "" Then
            MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim Address As String = txtEIModifyMLocation.Text
        Dim City As String = txtEIModifyMCity.Text
        Dim PostalCode As String = mtbEIModifyMZipCode.Text

        If Address <> "" And City <> "" Then
            Dim query As String = "Update EIS_FacilitySiteAddress set " &
            " strMailingAddressText = @Address, " &
            " strMailingAddresscityname = @City, " &
            " strMailingAddressPostalCode = @PostalCode " &
            " where facilitysiteid = @AirsNumber"

            Dim parameters As SqlParameter()

            parameters = New SqlParameter() {
                New SqlParameter("@Address", Address),
                New SqlParameter("@City", City),
                New SqlParameter("@PostalCode", PostalCode),
                New SqlParameter("@AirsNumber", txtEILogSelectedAIRSNumber.Text)
            }

            DB.RunCommand(query, parameters)

            MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)
        Else
            MsgBox("No data saved." & vbCrLf & "BOTH MAILING ADDRESS AND CITY ARE REQUIRED" & vbCrLf & vbCrLf & "Sorry for yelling.", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub

    Private Sub btnEIModifyUpdateName_Click(sender As Object, e As EventArgs) Handles btnEIModifyUpdateName.Click
        If txtEILogSelectedAIRSNumber.Text = "" Then
            MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If txtEIModifyFacilityName.Text <> "" Then
            txtEIModifyFacilityName.Text = Facility.SanitizeFacilityNameForDb(txtEIModifyFacilityName.Text)
        End If

        Dim FacilityName As String = txtEIModifyFacilityName.Text

        If FacilityName <> "" Then
            Dim query As String = "Update EIS_FacilitySite set " &
            " strFacilitySiteName = @FacilityName " &
            " where facilitysiteid = @AirsNumber"

            Dim parameters As SqlParameter()

            parameters = New SqlParameter() {
                New SqlParameter("@FacilityName", FacilityName),
                New SqlParameter("@AirsNumber", txtEILogSelectedAIRSNumber.Text)
            }

            DB.RunCommand(query, parameters)

            MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)
        Else
            MsgBox("No data saved.", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub

    Sub UpdateFacilityGEOCoord()
        Try
            If txtEILogSelectedAIRSNumber.Text = "" Then
                MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If mtbEIModifyLatitude.Text <> "" And mtbEIModifyLongitude.Text <> "" Then
                Dim SQL As String = "Update EIS_FacilityGEOCoord set " &
                "numLatitudeMeasure = @numLatitudeMeasure, " &
                "numLongitudeMeasure = @numLongitudeMeasure " &
                "where facilitySiteID = @facilitySiteID "

                Dim params As SqlParameter() = {
                    New SqlParameter("@numLatitudeMeasure", mtbEIModifyLatitude.Text),
                    New SqlParameter("@numLongitudeMeasure", -mtbEIModifyLongitude.Text),
                    New SqlParameter("@facilitySiteID", txtEILogSelectedAIRSNumber.Text)
                }

                DB.RunCommand(SQL, params)

                SQL = "Update APBFacilityInformation set " &
                    "numFacilityLongitude = @numFacilityLongitude, " &
                    "numFacilityLatitude = @numFacilityLatitude, " &
                    "strComments = @strComments, " &
                    "strModifingPerson = @strModifingPerson, " &
                    "datModifingDate = getdate() " &
                    "where strAIRSNumber = @strAIRSNumber "

                Dim params2 As SqlParameter() = {
                    New SqlParameter("@numFacilityLongitude", -mtbEIModifyLongitude.Text),
                    New SqlParameter("@numFacilityLatitude", mtbEIModifyLatitude.Text),
                    New SqlParameter("@strComments", "Updated by " & CurrentUser.AlphaName & " through DMU Staff Tools - Emissions Inventory Log. "),
                    New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                    New SqlParameter("@strAIRSNumber", "0413" & txtEILogSelectedAIRSNumber.Text)
                }

                DB.RunCommand(SQL, params2)

                MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Latitude & Longitude data not saved." & vbCrLf & "Add both values to update.",
                         MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateLatLong_Click(sender As Object, e As EventArgs) Handles btnUpdateLatLong.Click
        UpdateFacilityGEOCoord()
    End Sub

    Private Sub btnUpdateEisOperStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateEisOperStatus.Click
        If txtEILogSelectedAIRSNumber.Text = "" Then
            MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
        Else
            Dim query As String = "UPDATE EIS_FACILITYSITE " &
                " SET STRFACILITYSITESTATUSCODE = @statuscode " &
                " , STRFACILITYSITECOMMENT = @sitecomment " &
                " , UPDATEUSER = @updateuser " &
                " , UPDATEDATETIME = GETDATE() " &
                " WHERE FACILITYSITEID = @siteid "

            Dim parameters As SqlParameter() = New SqlParameter() {
                New SqlParameter("@statuscode", cbEisModifyOperStatus.SelectedValue.ToString),
                New SqlParameter("@sitecomment", "Site status updated from IAIP"),
                New SqlParameter("@updateuser", CurrentUser.UserID & "-" & CurrentUser.AlphaName),
                New SqlParameter("@siteid", txtEILogSelectedAIRSNumber.Text)
            }

            If DB.RunCommand(query, parameters) Then
                MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("There was an error updating the data.", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub

    Private Sub btnEIModifyCopy_Click(sender As Object, e As EventArgs) Handles btnEIModifyCopy.Click
        txtEIModifyFacilityName.Text = txtEIModifyIAIPFacilityName.Text
        txtEIModifyLocation.Text = txtEIModifyIAIPLocation.Text
        txtEIModifyCity.Text = txtEIModifyIAIPCity.Text
        mtbEIModifyMZipCode.Text = mtbEIModifyIAIPZipCode.Text
        txtEIModifyMLocation.Text = txtEIModifyIAIPLocation.Text
        txtEIModifyMCity.Text = txtEIModifyIAIPCity.Text
        mtbEIModifyZipCode.Text = mtbEIModifyIAIPZipCode.Text
        mtbEIModifyLatitude.Text = mtbEIModifyIAIPLatitude.Text
        mtbEIModifyLongitude.Text = mtbEIModifyIAIPLongitude.Text
        Select Case CType(cbIaipOperStatus.SelectedValue, FacilityOperationalStatus)
            Case FacilityOperationalStatus.C, FacilityOperationalStatus.I, FacilityOperationalStatus.P, FacilityOperationalStatus.U
                cbEisModifyOperStatus.SelectedValue = EisSiteStatus.UNK
            Case FacilityOperationalStatus.O
                cbEisModifyOperStatus.SelectedValue = EisSiteStatus.OP
            Case FacilityOperationalStatus.T
                cbEisModifyOperStatus.SelectedValue = EisSiteStatus.TS
            Case FacilityOperationalStatus.X
                cbEisModifyOperStatus.SelectedValue = EisSiteStatus.PS
        End Select
    End Sub

    Private Sub btnEISMailoutUpdate_Click(sender As Object, e As EventArgs) Handles btnEISMailoutUpdate.Click
        Try

            If txtEILogSelectedAIRSNumber.Text = "" Then
                MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim SQL As String = "Update EIS_Mailout Set " &
            "strFacilityName= @strFacilityName, " &
            "strContactCompanyName = @strContactCompanyName, " &
            "strContactAddress1 = @strContactAddress1, " &
            "strContactAddress2 = @strContactAddress2, " &
            "strContactCity = @strContactCity, " &
            "strContactState = @strContactState, " &
            "strContactZipCode = @strContactZipCode, " &
            "strContactFirstName = @strContactFirstName, " &
            "strContactLastName = @strContactLastName, " &
            "strContactPrefix = @strContactPrefix, " &
            "strContactEmail = @strContactEmail, " &
            "strComment = @strComment " &
            "where FacilitySiteid = @FacilitySiteid " &
            "and intInventoryYear = @intInventoryYear "

            Dim params As SqlParameter() = {
                New SqlParameter("@strFacilityName", txtEISMailoutEditFacilityName.Text),
                New SqlParameter("@strContactCompanyName", txtEISMailoutEditCompanyName.Text),
                New SqlParameter("@strContactAddress1", txtEISMailoutEditAdress.Text),
                New SqlParameter("@strContactAddress2", txtEISMailoutEditAddress2.Text),
                New SqlParameter("@strContactCity", txtEISMailoutEditCity.Text),
                New SqlParameter("@strContactState", txtEISMailoutEditState.Text),
                New SqlParameter("@strContactZipCode", txtEISMailoutEditZipCode.Text),
                New SqlParameter("@strContactFirstName", txtEISMailoutEditFirstName.Text),
                New SqlParameter("@strContactLastName", txtEISMailoutEditLastName.Text),
                New SqlParameter("@strContactPrefix", txtEISMailoutEditPrefix.Text),
                New SqlParameter("@strContactEmail", txtEISMailoutEditEmailAddress.Text),
                New SqlParameter("@strComment", txtEISMailoutEditComments.Text),
                New SqlParameter("@FacilitySiteid", txtEILogSelectedAIRSNumber.Text),
                New SqlParameter("@intInventoryYear", txtEILogSelectedYear.Text)
            }

            DB.RunCommand(SQL, params)

            MsgBox("Data updated", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedToDo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedToDo.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", " and EISStatusCode >= 3 ", "", " and QAStatusCode is null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, To-Do Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedBegan_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBegan.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
                      " and EISStatusCode >= 3 ", "", " and QAStatusCode is not null and datQAComplete is null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedBeganwFIErrors_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwFIErrors.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
             " and EISStatusCode >= 3 ", "",
             " and datQAComplete is null and strFIError = 'True' and (strPointError = 'False' or strPointError is null) ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedBeganwithEIErrors_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwithEIErrors.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
             " and EISStatusCode >= 3 ", "",
             " and datQAComplete is null and (strFIError = 'False' or strFIError is null) and strPointError = 'True'  ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISStatsSubmittedBeganwithBothErrors_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwithBothErrors.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
          " and EISStatusCode >= 3 ", "",
          " and datQAComplete is null and (strFIError = 'True' ) and (strPointError = 'True' ) ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISStatsSubmittedBeganwithoutErrors_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwithoutErrors.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
          " and EISStatusCode >= 3 ", "",
          " and datQAComplete is null and (strFIError = 'False' or strFIError is null) and " &
          "(strPointError = 'False' or strPointError is null) " &
          "and QAStatusCode is not null")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsOptedOutToDo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsOptedOutToDo.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", " and (strOptOut = '1' or stroptout is null )",
             " and EISStatusCode >= 3 ", "", " and QAStatusCode is null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Opted-Out, To-do Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsOptedOutBegan_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsOptedOutBegan.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", " and (strOptOut = '1' or strOptout is null) ",
                     " and EISStatusCode >= 3 ", "", " and QAStatusCode is not null and datQAComplete is null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Opted-Out, Started Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsOptedOutSubmittedToEPA_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsOptedOutSubmittedToEPA.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", " and (strOptOut = '1' or strOptout is null )  ",
               " and EISStatusCode >= 5 ", "", " and datQAComplete is not null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, EPA Submitted Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbSearchForFacility_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSearchForFacility.LinkClicked
        Try
            If cboEISStatisticsYear.Text = "" Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim SQL As String = "Select " &
                  "strFacilityName, " &
                  "strContactCompanyName, strContactAddress1, " &
                  "strContactAddress2, strContactCity, " &
                  "strcontactstate, strcontactzipCode, " &
                  "strcontactFirstName, strcontactLastName, " &
                  "strContactPrefix, strContactEmail, " &
                  "stroperationalStatus, strClass, " &
                  "strcomment, UpdateUser, " &
                  "updateDateTime, CreateDateTime " &
                   "from EIS_Mailout " &
                   "where intInventoryyear = @intInventoryyear " &
                   "and FacilitySiteID = @FacilitySiteID "
            Dim params As SqlParameter() = {
                New SqlParameter("@intInventoryyear", cboEISStatisticsYear.Text),
                New SqlParameter("@FacilitySiteID", txtEISStatsMailoutAIRSNumber.Text)
            }
            Dim dr As DataRow = DB.GetDataRow(SQL, params)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtEISStatsMailoutFacilityName.Clear()
                Else
                    txtEISStatsMailoutFacilityName.Text = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtEISStatsMailoutCompanyName.Clear()
                Else
                    txtEISStatsMailoutCompanyName.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtEISStatsMailoutAddress1.Clear()
                Else
                    txtEISStatsMailoutAddress1.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactAddress2")) Then
                    txtEISStatsMailoutAddress2.Clear()
                Else
                    txtEISStatsMailoutAddress2.Text = dr.Item("strContactAddress2")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtEISStatsMailoutCity.Clear()
                Else
                    txtEISStatsMailoutCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strcontactstate")) Then
                    txtEISStatsMailoutState.Clear()
                Else
                    txtEISStatsMailoutState.Text = dr.Item("strcontactstate")
                End If
                If IsDBNull(dr.Item("strcontactzipCode")) Then
                    txtEISStatsMailoutZipCode.Clear()
                Else
                    txtEISStatsMailoutZipCode.Text = dr.Item("strcontactzipCode")
                End If
                If IsDBNull(dr.Item("strcontactFirstName")) Then
                    txtEISStatsMailoutFirstName.Clear()
                Else
                    txtEISStatsMailoutFirstName.Text = dr.Item("strcontactFirstName")
                End If
                If IsDBNull(dr.Item("strcontactLastName")) Then
                    txtEISStatsMailoutLastName.Clear()
                Else
                    txtEISStatsMailoutLastName.Text = dr.Item("strcontactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtEISStatsMailoutPrefix.Clear()
                Else
                    txtEISStatsMailoutPrefix.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtEISStatsMailoutEmailAddress.Clear()
                Else
                    txtEISStatsMailoutEmailAddress.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strcomment")) Then
                    txtEISStatsMailoutComments.Clear()
                Else
                    txtEISStatsMailoutComments.Text = dr.Item("strcomment")
                End If
                If IsDBNull(dr.Item("UpdateUser")) Then
                    txtEISStatsMailoutUpdateUser.Clear()
                Else
                    txtEISStatsMailoutUpdateUser.Text = dr.Item("UpdateUser")
                End If
                If IsDBNull(dr.Item("updateDateTime")) Then
                    txtEISStatsMailoutUpdateDate.Clear()
                Else
                    txtEISStatsMailoutUpdateDate.Text = dr.Item("updateDateTime")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    txtEISStatsMailoutCreateDate.Clear()
                Else
                    txtEISStatsMailoutCreateDate.Text = dr.Item("CreateDateTime")
                End If
            End If

            If txtEISStatsMailoutFacilityName.Text = "" Then
                SQL = "SELECT * " &
                    "FROM " &
                    "  (SELECT dt_EIContact.STRAIRSNUMBER, fi.STRFACILITYNAME, " &
                    "    hd.STROPERATIONALSTATUS, hd.STRCLASS,( " &
                    "    CASE                                WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTLASTNAME WHEN " &
                    "        dt_EIContact.STRKEY IS NULL THEN " &
                    "        dt_PermitContact.STRCONTACTLASTNAME ELSE '' " &
                    "    END) STRContactLastName,( " &
                    "    CASE                                 WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTFIRSTNAME WHEN " &
                    "        dt_EIContact.STRKEY IS NULL THEN " &
                    "        dt_PermitContact.STRCONTACTFIRSTNAME ELSE '' " &
                    "    END) STRContactfirstName,( " &
                    "    CASE                                   WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTCOMPANYNAME WHEN " &
                    "        dt_EIContact.STRKEY IS NULL THEN " &
                    "        dt_PermitContact.STRCONTACTCOMPANYNAME " &
                    "    END) STRContactCompanyName,( " &
                    "    CASE                             WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTEMAIL WHEN dt_EIContact.STRKEY " &
                    "        IS NULL THEN dt_PermitContact.STRCONTACTEMAIL " &
                    "    END) STRContactEmail,( " &
                    "    CASE                              WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTPREFIX WHEN dt_EIContact.STRKEY " &
                    "        IS NULL THEN dt_PermitContact.STRCONTACTPREFIX " &
                    "    END) strCONTACTPREFIX,( " &
                    "    CASE                                WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTADDRESS1 WHEN " &
                    "        dt_EIContact.STRKEY IS NULL THEN " &
                    "        dt_PermitContact.STRCONTACTADDRESS1 " &
                    "    END) STRCONTACTADDRESS1,( " &
                    "    CASE                            WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTCITY WHEN dt_EIContact.STRKEY IS " &
                    "        NULL THEN dt_PermitContact.STRCONTACTCITY " &
                    "    END) STRCONTACTCITY,( " &
                    "    CASE                             WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTSTATE WHEN dt_EIContact.STRKEY " &
                    "        IS NULL THEN dt_PermitContact.STRCONTACTSTATE " &
                    "    END) STRCONTACTSTATE,( " &
                    "    CASE                               WHEN dt_EIContact.STRKEY = '41' THEN " &
                    "        dt_EIContact.STRCONTACTZIPCODE WHEN dt_EIContact.STRKEY " &
                    "        IS NULL THEN dt_PermitContact.STRCONTACTZIPCODE " &
                    "    END) STRCONTACTZIPCODE " &
                    "  FROM " &
                    "    (SELECT DISTINCT dt_EIList.STRAIRSNUMBER, dt_Contact.STRKEY " &
                    "      , dt_Contact.STRCONTACTLASTNAME, " &
                    "      dt_Contact.STRCONTACTFIRSTNAME, " &
                    "      dt_Contact.STRCONTACTCOMPANYNAME, " &
                    "      dt_Contact.STRCONTACTEMAIL, dt_Contact.STRCONTACTPREFIX, " &
                    "      dt_Contact.STRCONTACTADDRESS1, dt_Contact.STRCONTACTCITY, " &
                    "      dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE " &
                    "    FROM " &
                    "      (SELECT * " &
                    "      FROM APBHEADERDATA hd " &
                    "      WHERE(hd.STROPERATIONALSTATUS = 'O' OR " &
                    "        hd.STROPERATIONALSTATUS = 'P' OR " &
                    "        hd.STROPERATIONALSTATUS = 'C') AND hd.STRCLASS = 'A' " &
                    "      ) dt_EIList " &
                    "    LEFT JOIN " &
                    "      (SELECT * FROM APBCONTACTINFORMATION ci WHERE ci.STRKEY = " &
                    "        41 " &
                    "      ) dt_Contact ON dt_EIList.STRAIRSNUMBER = " &
                    "      dt_Contact.STRAIRSNUMBER " &
                    "    ) dt_EIContact " &
                    "  LEFT JOIN " &
                    "    (SELECT DISTINCT dt_EIList.STRAIRSNUMBER, dt_Contact.STRKEY " &
                    "      , dt_Contact.STRCONTACTLASTNAME, " &
                    "      dt_Contact.STRCONTACTFIRSTNAME, " &
                    "      dt_Contact.STRCONTACTCOMPANYNAME, " &
                    "      dt_Contact.STRCONTACTEMAIL, dt_Contact.STRCONTACTPREFIX, " &
                    "      dt_Contact.STRCONTACTADDRESS1, dt_Contact.STRCONTACTCITY, " &
                    "      dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE " &
                    "    FROM " &
                    "      (SELECT * FROM APBHEADERDATA hd WHERE( " &
                    "        hd.STROPERATIONALSTATUS = 'O' OR " &
                    "        hd.STROPERATIONALSTATUS = 'P' OR " &
                    "        hd.STROPERATIONALSTATUS = 'C') AND hd.STRCLASS = 'A' " &
                    "      ) dt_EIList " &
                    "    LEFT JOIN " &
                    "      (SELECT * FROM APBCONTACTINFORMATION ci WHERE ci.STRKEY = " &
                    "        30 " &
                    "      ) dt_Contact ON dt_EIList.STRAIRSNUMBER = " &
                    "      dt_Contact.STRAIRSNUMBER " &
                    "    ) dt_PermitContact ON dt_EIContact.STRAIRSNUMBER = " &
                    "    dt_PermitContact.STRAIRSNUMBER " &
                    "  INNER JOIN APBHEADERDATA hd ON dt_EIContact.STRAIRSNUMBER = " &
                    "    hd.STRAIRSNUMBER " &
                    "  INNER JOIN APBFACILITYINFORMATION fi ON " &
                    "    dt_EIContact.STRAIRSNUMBER = fi.STRAIRSNUMBER " &
                    "  ) t1 " &
                    " WHERE STRAIRSNUMBER = @STRAIRSNUMBER "
                Dim param As New SqlParameter("@strAIRSnumber", "0413" & txtEISStatsMailoutAIRSNumber.Text)

                Dim dr2 As DataRow = DB.GetDataRow(SQL, param)

                If dr2 IsNot Nothing Then
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        txtEISStatsMailoutFacilityName.Clear()
                    Else
                        txtEISStatsMailoutFacilityName.Text = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strContactCompanyName")) Then
                        txtEISStatsMailoutCompanyName.Clear()
                    Else
                        txtEISStatsMailoutCompanyName.Text = dr.Item("strContactCompanyName")
                    End If
                    If IsDBNull(dr.Item("strContactAddress1")) Then
                        txtEISStatsMailoutAddress1.Clear()
                    Else
                        txtEISStatsMailoutAddress1.Text = dr.Item("strContactAddress1")
                    End If
                    txtEISStatsMailoutAddress2.Clear()
                    If IsDBNull(dr.Item("strContactCity")) Then
                        txtEISStatsMailoutCity.Clear()
                    Else
                        txtEISStatsMailoutCity.Text = dr.Item("strContactCity")
                    End If
                    If IsDBNull(dr.Item("strcontactstate")) Then
                        txtEISStatsMailoutState.Clear()
                    Else
                        txtEISStatsMailoutState.Text = dr.Item("strcontactstate")
                    End If
                    If IsDBNull(dr.Item("strcontactzipCode")) Then
                        txtEISStatsMailoutZipCode.Clear()
                    Else
                        txtEISStatsMailoutZipCode.Text = dr.Item("strcontactzipCode")
                    End If
                    If IsDBNull(dr.Item("strcontactFirstName")) Then
                        txtEISStatsMailoutFirstName.Clear()
                    Else
                        txtEISStatsMailoutFirstName.Text = dr.Item("strcontactFirstName")
                    End If
                    If IsDBNull(dr.Item("strcontactLastName")) Then
                        txtEISStatsMailoutLastName.Clear()
                    Else
                        txtEISStatsMailoutLastName.Text = dr.Item("strcontactLastName")
                    End If
                    If IsDBNull(dr.Item("strContactPrefix")) Then
                        txtEISStatsMailoutPrefix.Clear()
                    Else
                        txtEISStatsMailoutPrefix.Text = dr.Item("strContactPrefix")
                    End If
                    If IsDBNull(dr.Item("strContactEmail")) Then
                        txtEISStatsMailoutEmailAddress.Clear()
                    Else
                        txtEISStatsMailoutEmailAddress.Text = dr.Item("strContactEmail")
                    End If
                    txtEISStatsMailoutComments.Clear()
                    If IsDBNull(dr.Item("UpdateUser")) Then
                        txtEISStatsMailoutUpdateUser.Clear()
                    Else
                        txtEISStatsMailoutUpdateUser.Text = dr.Item("UpdateUser")
                    End If
                    If IsDBNull(dr.Item("updateDateTime")) Then
                        txtEISStatsMailoutUpdateDate.Clear()
                    Else
                        txtEISStatsMailoutUpdateDate.Text = dr.Item("updateDateTime")
                    End If
                    If IsDBNull(dr.Item("CreateDateTime")) Then
                        txtEISStatsMailoutCreateDate.Clear()
                    Else
                        txtEISStatsMailoutCreateDate.Text = dr.Item("CreateDateTime")
                    End If
                End If

                btnAddtoEISMailout.Visible = True

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddtoEISMailout_Click(sender As Object, e As EventArgs) Handles btnAddtoEISMailout.Click
        Try
            Dim EISConfirm As String = InputBox("Type in the EIS Year that you have selected to add facilies into Mailout.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                Dim temp As String = ""

                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True And dgvEISStats(7, i).Value = "No" Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next

                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "

                    Dim SQL As String = "Update EIS_Admin set " &
                    "strMailOut = '1' " &
                    "where inventoryYear = @inventoryyear " &
                    temp

                    Dim param As New SqlParameter("@inventoryyear", EISConfirm)
                    DB.RunCommand(SQL, param)

                    MsgBox(EISConfirm & " Emission Inventory Facilities in Mailout.", MsgBoxStyle.Information, Me.Text)
                End If

            Else
                MsgBox("Year does not match selected EIS year")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISComplete_Click(sender As Object, e As EventArgs) Handles btnEISComplete.Click
        Try
            Dim EISConfirm As String = InputBox("Type in the EIS Year that you have selected to mark Facilities as complete.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                Dim query As String = "Update EIS_Admin set " &
                "EISAccessCode = '0', " &
                "EISStatusCode = '5', " &
                "datEISstatus = getdate(), " &
                "UpdateUser = @UpdateUser, " &
                "updatedatetime = getdate() " &
                "where inventoryYear = @inventoryYear " &
                " and FacilitySiteID in ({0}) "

                Dim paramNameList As New List(Of String)
                Dim paramList As New List(Of SqlParameter)

                paramList.Add(New SqlParameter("@inventoryYear", EISConfirm))
                paramList.Add(New SqlParameter("@UpdateUser", CurrentUser.AlphaName))

                ' TODO DWW: Change to table-valued parameter instead of dynamically built "IN" list
                Dim paramName As String
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True Then
                        paramName = "@site" & Replace(dgvEISStats(1, i).Value, "-", "")
                        paramNameList.Add(paramName)
                        paramList.Add(New SqlParameter(paramName, dgvEISStats(1, i).Value))
                    End If
                Next
                Dim inClause As String = String.Join(",", paramNameList)

                If paramNameList.Count > 0 Then
                    DB.RunCommand(String.Format(query, inClause), paramList.ToArray)
                    MsgBox(EISConfirm & " EIS process completed.", MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox("No facilities selected.")
                End If
            Else
                MsgBox("Year does not match selected EIS year")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub ViewPollutantThresholds()
        Try
            Dim SQL As String
            If rdbThreeYearPollutants.Checked = True Then
                SQL = "Select " &
                "strPollutant, numThreshold, " &
                "numThresholdNAA " &
                "from EIThresholds " &
                "where strType = '3YEAR' " &
                "order by strPollutant "
            Else
                SQL = "Select " &
                "strPollutant, numThreshold, " &
                "numThresholdNAA " &
                "from EIThresholds " &
                "where strType = 'ANNUAL' " &
                "order by strPollutant "
            End If

            Dim dt As DataTable = DB.GetDataTable(SQL)

            dgvThresholdPollutants.DataSource = dt

            dgvThresholdPollutants.RowHeadersVisible = False
            dgvThresholdPollutants.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvThresholdPollutants.AllowUserToResizeColumns = True
            dgvThresholdPollutants.AllowUserToAddRows = False
            dgvThresholdPollutants.AllowUserToDeleteRows = False
            dgvThresholdPollutants.AllowUserToOrderColumns = True
            dgvThresholdPollutants.AllowUserToResizeRows = True

            dgvThresholdPollutants.Columns("strPollutant").HeaderText = "Pollutant"
            dgvThresholdPollutants.Columns("strPollutant").DisplayIndex = 0
            dgvThresholdPollutants.Columns("numThreshold").HeaderText = "Threshold"
            dgvThresholdPollutants.Columns("numThreshold").DisplayIndex = 1
            dgvThresholdPollutants.Columns("numThresholdNAA").HeaderText = "NonAttainment Area Threshold"
            dgvThresholdPollutants.Columns("numThresholdNAA").DisplayIndex = 2

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewThresholdPollutants_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewThresholdPollutants.LinkClicked
        ViewPollutantThresholds()
    End Sub

    Private Sub dgvThresholdPollutants_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvThresholdPollutants.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvThresholdPollutants.HitTest(e.X, e.Y)

        Try
            If dgvThresholdPollutants.RowCount > 0 And hti.RowIndex <> -1 Then
                If IsDBNull(dgvThresholdPollutants(0, hti.RowIndex).Value) Then
                    txtPollutant.Clear()
                Else
                    txtPollutant.Text = dgvThresholdPollutants(0, hti.RowIndex).Value
                End If
                If IsDBNull(dgvThresholdPollutants(1, hti.RowIndex).Value) Then
                    txtThreshold.Clear()
                Else
                    txtThreshold.Text = dgvThresholdPollutants(1, hti.RowIndex).Value
                End If
                If IsDBNull(dgvThresholdPollutants(2, hti.RowIndex).Value) Then
                    txtNonAttainmentThreshold.Clear()
                Else
                    txtNonAttainmentThreshold.Text = dgvThresholdPollutants(2, hti.RowIndex).Value
                End If
            Else
                txtPollutant.Clear()
                txtThreshold.Clear()
                txtNonAttainmentThreshold.Clear()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnAddNewPollutant_Click(sender As Object, e As EventArgs) Handles btnAddNewPollutant.Click
        Try
            Dim ThresholdType As String = ""

            If rdbAnnualPollutants.Checked = True Then
                ThresholdType = "ANNUAL"
            End If
            If rdbThreeYearPollutants.Checked = True Then
                ThresholdType = "3YEAR"
            End If
            If ThresholdType = "" Then
                MsgBox("Select either an Annual or 3 Year threshold type." & vbCrLf & "No Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
            If txtPollutant.Text = "" Then
                Exit Sub
            End If

            Dim SQL As String = "Select * from " &
            "EIThresholds " &
            "where strPollutant = @strPollutant " &
            "and strType = @strType "

            Dim params As SqlParameter() = {
                New SqlParameter("@strPollutant", txtPollutant.Text),
                New SqlParameter("@strType", ThresholdType)
            }

            If DB.ValueExists(SQL, params) Then
                MsgBox("Pollutant currently exists for selected Type." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            Else
                Dim SQL2 As String = "INSERT INTO EITHRESHOLDS " &
                    " (STRPOLLUTANT, NUMTHRESHOLD, NUMTHRESHOLDNAA, STRTYPE) " &
                    "VALUES " &
                    " (@STRPOLLUTANT, @NUMTHRESHOLD, @NUMTHRESHOLDNAA, @STRTYPE) "
                Dim params2 As SqlParameter() = {
                    New SqlParameter("@STRPOLLUTANT", txtPollutant.Text),
                    New SqlParameter("@NUMTHRESHOLD", txtThreshold.Text),
                    New SqlParameter("@NUMTHRESHOLDNAA", txtNonAttainmentThreshold.Text),
                    New SqlParameter("@STRTYPE", ThresholdType)
                }
                DB.RunCommand(SQL2, params2)

                ViewPollutantThresholds()
                MsgBox("Data Added", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdatePollutant_Click(sender As Object, e As EventArgs) Handles btnUpdatePollutant.Click
        Try
            Dim ThresholdType As String = ""

            If rdbAnnualPollutants.Checked = True Then
                ThresholdType = "ANNUAL"
            End If
            If rdbThreeYearPollutants.Checked = True Then
                ThresholdType = "3YEAR"
            End If
            If ThresholdType = "" Then
                MsgBox("Select either an Annual or 3 Year threshold type." & vbCrLf & "No Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
            If txtPollutant.Text = "" Then
                Exit Sub
            End If

            Dim SQL As String = "Select * from " &
            "EIThresholds " &
            "where strPollutant = @strPollutant " &
            "and strType = @strType "

            Dim params As SqlParameter() = {
                New SqlParameter("@strPollutant", txtPollutant.Text),
                New SqlParameter("@strType", ThresholdType)
            }

            If DB.ValueExists(SQL, params) Then
                Dim SQL2 As String = "UPDATE EITHRESHOLDS " &
                    "SET NUMTHRESHOLD   = @NUMTHRESHOLD " &
                    ", NUMTHRESHOLDNAA  = @NUMTHRESHOLDNAA " &
                    "WHERE STRPOLLUTANT = @STRPOLLUTANT " &
                    "AND STRTYPE        = @STRTYPE"
                Dim params2 As SqlParameter() = {
                    New SqlParameter("@NUMTHRESHOLD", txtThreshold.Text),
                    New SqlParameter("@NUMTHRESHOLDNAA", txtNonAttainmentThreshold.Text),
                    New SqlParameter("@STRPOLLUTANT", txtPollutant.Text),
                    New SqlParameter("@STRTYPE", ThresholdType)
                }
                DB.RunCommand(SQL2, params2)

                ViewPollutantThresholds()
                MsgBox("Data Updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Pollutant currently does not exists for selected Type." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadEISYear()
        Dim SQL As String = "Select " &
        "strYear, " &
        "strEIType, datDeadLine " &
        "from EIThresholdYears " &
        "order by strYear desc "
        Dim dt As DataTable = DB.GetDataTable(SQL)

        dgvEISYear.DataSource = dt

        dgvEISYear.RowHeadersVisible = False
        dgvEISYear.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvEISYear.AllowUserToResizeColumns = True
        dgvEISYear.AllowUserToAddRows = False
        dgvEISYear.AllowUserToDeleteRows = False
        dgvEISYear.AllowUserToOrderColumns = True
        dgvEISYear.AllowUserToResizeRows = True

        dgvEISYear.Columns("strYear").HeaderText = "EIS Year"
        dgvEISYear.Columns("strYear").DisplayIndex = 0
        dgvEISYear.Columns("strEIType").HeaderText = "Type"
        dgvEISYear.Columns("strEIType").DisplayIndex = 1
        dgvEISYear.Columns("datDeadLine").HeaderText = "EIS Date Deadline"
        dgvEISYear.Columns("datDeadLine").DisplayIndex = 2
        dgvEISYear.Columns("datDeadLine").DefaultCellStyle.Format = "dd-MMM-yyyy"
    End Sub

    Private Sub dgvEISYear_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvEISYear.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvEISYear.HitTest(e.X, e.Y)

        Try
            If dgvEISYear.RowCount > 0 And hti.RowIndex <> -1 Then
                If IsDBNull(dgvEISYear(0, hti.RowIndex).Value) Then
                    mtbThresholdYear.Clear()
                Else
                    mtbThresholdYear.Text = dgvEISYear(0, hti.RowIndex).Value
                End If
                If IsDBNull(dgvEISYear(1, hti.RowIndex).Value) Then
                    rdbEISAnnual.Checked = False
                    rdbEISThreeYear.Checked = False
                Else
                    If dgvEISYear(1, hti.RowIndex).Value = "3YEAR" Then
                        rdbEISAnnual.Checked = False
                        rdbEISThreeYear.Checked = True
                    Else
                        rdbEISAnnual.Checked = True
                        rdbEISThreeYear.Checked = False
                    End If
                End If
                If IsDBNull(dgvEISYear(2, hti.RowIndex).Value) Then
                    dtpEISDeadline.Value = Today
                Else
                    dtpEISDeadline.Text = dgvEISYear(2, hti.RowIndex).Value
                End If
            Else
                mtbThresholdYear.Clear()
                rdbEISAnnual.Checked = False
                rdbEISThreeYear.Checked = False
                dtpEISDeadline.Value = Today
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbClearEISYear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbClearEISYear.LinkClicked
        Try

            mtbThresholdYear.Clear()
            rdbEISAnnual.Checked = False
            rdbEISThreeYear.Checked = False
            dtpEISDeadline.Value = Today

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddEISYear_Click(sender As Object, e As EventArgs) Handles btnAddEISYear.Click
        Try
            Dim EISYearType As String = ""

            If mtbThresholdYear.Text.Length <> 4 Then
                MsgBox("Bad Year" & vbCrLf & "No Data Saved", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If rdbEISThreeYear.Checked = True Then
                EISYearType = "3YEAR"
            Else
                EISYearType = "ANNUAL"
            End If

            Dim SQL As String = "Select " &
            "strYear " &
            "from EIThresholdYears " &
            "where strYEar = @strYEar "

            Dim param As New SqlParameter("@strYEar", mtbThresholdYear.Text)

            If DB.ValueExists(SQL, param) Then
                MsgBox("EIS Year currently exists." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            Else
                SQL = "INSERT INTO EITHRESHOLDYEARS " &
                    " (STRYEAR, STREITYPE, DATDEADLINE) " &
                    " VALUES " &
                    " (@STRYEAR, @STREITYPE, @DATDEADLINE) "
                Dim params As SqlParameter() = {
                    New SqlParameter("@STRYEAR", mtbThresholdYear.Text),
                    New SqlParameter("@STREITYPE", EISYearType),
                    New SqlParameter("@DATDEADLINE", dtpEISDeadline.Text)
                }
                DB.RunCommand(SQL, params)

                LoadEISYear()
                MsgBox("Data Added", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateEISYear_Click(sender As Object, e As EventArgs) Handles btnUpdateEISYear.Click
        Try
            Dim EISYearType As String = ""

            If mtbThresholdYear.Text.Length <> 4 Then
                MsgBox("Bad Year" & vbCrLf & "No Data Saved", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If rdbEISThreeYear.Checked = True Then
                EISYearType = "3YEAR"
            Else
                EISYearType = "ANNUAL"
            End If

            Dim SQL As String = "Select " &
                "strYear " &
                "from EIThresholdYears " &
                "where strYEar = @strYEar "
            Dim param As New SqlParameter("@strYEar", mtbThresholdYear.Text)

            If DB.ValueExists(SQL, param) Then
                SQL = "UPDATE EITHRESHOLDYEARS " &
                    " SET STREITYPE = @STREITYPE, " &
                    " DATDEADLINE = @DATDEADLINE " &
                    " WHERE STRYEAR = @STRYEAR "
                Dim params As SqlParameter() = {
                    New SqlParameter("@STREITYPE", EISYearType),
                    New SqlParameter("@DATDEADLINE", dtpEISDeadline.Text),
                    New SqlParameter("@STRYEAR", mtbThresholdYear.Text)
                }
                DB.RunCommand(SQL, params)

                LoadEISYear()
                MsgBox("Data Updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("EIS Year does not currently exists." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnLoadEISLog_Click(sender As Object, e As EventArgs) Handles btnLoadEISLog.Click
        Try
            If mtbEISLogAIRSNumber.Text <> "" And cboEISStatisticsYear.Text.Length = 4 Then
                mtbEILogAIRSNumber.Text = mtbEISLogAIRSNumber.Text
                cboEILogYear.Text = cboEISStatisticsYear.Text

                LoadFSData()

                TCDMUTools.SelectedIndex = 0
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsFipassed_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEISStatsFipassed.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
                 " and EISStatusCode >= 3 ", "", " and QAStatusCode = '2' ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, EPA Submitted Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRemoveFromQA_Click(sender As Object, e As EventArgs) Handles btnRemoveFromQA.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to delete all current QA data.", Me.Text)

            If EISConfirm = txtEILogSelectedYear.Text Then
                Dim SQL1 As String = "delete EIS_QAAdmin " &
                "where inventoryyear = @inventoryyear " &
                "and facilitysiteid = @facilitysiteid "
                Dim params1 As SqlParameter() = {
                    New SqlParameter("@inventoryyear", EISConfirm),
                    New SqlParameter("@facilitysiteid", txtEILogSelectedAIRSNumber.Text)
                }

                Dim SQL2 As String = "Update EIS_Admin set " &
                  "EISAccessCode = '2', " &
                  "EISStatusCode = '3', " &
                  "datEISstatus = GETDATE(), " &
                  "UpdateUser = '" & Replace(CurrentUser.AlphaName, "'", "''") & "', " &
                  "updatedatetime = getdate() " &
                  "where inventoryYear = '" & EISConfirm & "' " &
                  "and facilitysiteid = '" & txtEILogSelectedAIRSNumber.Text & "' "
                Dim params2 As SqlParameter() = {
                    New SqlParameter("@UpdateUser", CurrentUser.AlphaName),
                    New SqlParameter("@inventoryYear", EISConfirm),
                    New SqlParameter("@facilitysiteid", txtEILogSelectedAIRSNumber.Text)
                }

                Dim querylist As New List(Of String) From {SQL1, SQL2}
                Dim paramlist As New List(Of SqlParameter()) From {params1, params2}

                DB.RunCommand(querylist, paramlist)

                MsgBox("Done", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCleanUp_Click(sender As Object, e As EventArgs) Handles btnCleanUp.Click
        Try
            Dim spName As String = "PD_EIS_QASTART"

            Dim selection As Boolean = False

            For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                If dgvEISStats(0, i).Value = True Then
                    Dim params As SqlParameter() = {
                        New SqlParameter("@AIRSNUMBER_IN", dgvEISStats(1, i).Value),
                        New SqlParameter("@INTYEAR_IN", cboEISStatisticsYear.Text)
                    }
                    DB.SPRunCommand(spName, params)
                    selection = True
                End If
            Next

            If selection Then
                MsgBox("Complete", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("No facilities selected.")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewMailoutData_Click(sender As Object, e As EventArgs) Handles btnViewMailoutData.Click
        Try
            If txtSelectedEISMailout.Text = "" Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISMailout.Text, "1", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Mailout Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnGenerateMailout_Click(sender As Object, e As EventArgs) Handles btnGenerateMailout.Click
        Try
            If txtSelectedEISMailout.Text = "" Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim SQL As String = "Update EIS_Admin set " &
            "strMailout = '1' " &
            "where inventoryYear = @inventoryYear " &
            "and Active = '1' "
            Dim param As New SqlParameter("@inventoryYear", txtSelectedEISMailout.Text)

            DB.RunCommand(SQL, param)

            EIS_VIEW(txtSelectedEISMailout.Text, "1", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Mailout Count (Generated)"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRemoveAllMailout_Click(sender As Object, e As EventArgs) Handles btnRemoveAllMailout.Click
        Try

            Dim SQL As String = "Update EIS_Admin set " &
          "strMailout = '' " &
          "where inventoryYear = @inventoryYear " &
          "and strMailout = '1' " &
          "and Active = '1' "
            Dim param As New SqlParameter("@inventoryYear", txtSelectedEISMailout.Text)
            DB.RunCommand(SQL, param)

            EIS_VIEW(txtSelectedEISMailout.Text, "1", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Mailout Count (Removed)"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnViewEISEnrolled_Click(sender As Object, e As EventArgs) Handles btnViewEISEnrolled.Click
        Try
            If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            EIS_VIEW(txtEISStatsEnrollmentYear.Text, "", "1", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Enrolled"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEISEnrollMailoutList_Click(sender As Object, e As EventArgs) Handles btnEISEnrollMailoutList.Click
        Try
            If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim SQL As String = "Update EIS_Admin set " &
            "strEnrollment = '1' , " &
            "EISSTATUSCODE= '1' " &
            "where active = '1' " &
            "and InventoryYear = @InventoryYear " &
            "and strMailout = '1' "
            Dim param As New SqlParameter("@InventoryYear", txtEISStatsEnrollmentYear.Text)
            DB.RunCommand(SQL, param)

            EIS_VIEW(txtEISStatsEnrollmentYear.Text, "", "1", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Enrolled (Generated)"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRemoveEISEnrolled_Click(sender As Object, e As EventArgs) Handles btnRemoveEISEnrolled.Click
        Try
            If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim SQL As String = "Update EIS_Admin set " &
            "strEnrollment = '0' " &
            "where active = '1' " &
            "and InventoryYear = @InventoryYear " &
            "and strEnrollment = '1' "

            Dim param As New SqlParameter("@InventoryYear", txtEISStatsEnrollmentYear.Text)
            DB.RunCommand(SQL, param)

            EIS_VIEW(txtEISStatsEnrollmentYear.Text, "", "1", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Enrolled (Removed)"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub EIS_VIEW(EISYear As String, EISMailout As String, EISEnrollment As String,
                    EISActive As String, Optout As String, EISStatus As String,
                    EISAccess As String, QAStatus As String)

        'EISYear = value
        'EISMailout = value: 0,1, or null
        'EISEnrollment = value: 0, 1, or null 
        'EISActive = value: 0, 1 or null 
        'Optout = value: 0, 1, Null, or text
        'EISStatus = text
        'EISAccess = text
        'QAStatus = text 

        Try
            If EISYear = "" Then
                Exit Sub
            End If
            If EISActive = "" Then
                EISActive = "1"
            End If

            Dim dgvRow As New DataGridViewRow

            Dim SQL As String = "Select " &
            "'False' as ID, " &
            "FACILITYSITEID, " &
            "STRFACILITYNAME, INVENTORYYEAR, " &
            "EISSTAtuS, EISACCESS, OPTOUT, " &
            "MAILOUT, MAILOUTEMAIL, " &
            "STRDMURESPONSIBLESTAFF, ENROLLMENT, " &
            "QASTATUS, DATQASTATUS, " &
            "IAIPPREFIX, IAIPFIRSTNAME, " &
            "IAIPLASTNAME, IAIPEMAIL, " &
            "EISCOMPANYNAME, EISADDRESS, " &
            "EISADDRESS2, EISCITY, " &
            "EISSTATE, EISZIPCODE, " &
            "EISPREFIX, EISFIRSTNAME, " &
            "EISLASTNAME, DATFINALIZE, " &
            "strComment as Comments, " &
            "STRFITRACKINGNUMBER as FITrackingNumber, " &
            "STRPOINTTRACKINGNUMBER as PointTrackingNumber " &
            " from VW_EIS_Stats " &
            "where inventoryyear = @inventoryyear " &
            "and Active = @Active "

            If EISMailout <> "" Then
                SQL = SQL & " and strMailout = @strMailout "
            End If
            If EISEnrollment <> "" Then
                SQL = SQL & " and strEnrollment = @strEnrollment "
            End If
            If Optout <> "" Then
                Select Case Optout
                    Case "Null"
                        SQL = SQL & " and strOptOut is null "
                    Case "0"
                        SQL = SQL & " and strOptOut = '0' "
                    Case "1"
                        SQL = SQL & " and strOptOut = '1'  "
                    Case Else
                        SQL = SQL & Optout
                End Select
            End If
            If EISStatus <> "" Then
                SQL = SQL & EISStatus
            End If
            If EISAccess <> "" Then
                SQL = SQL & EISAccess
            End If
            If QAStatus <> "" Then
                SQL = SQL & QAStatus
            End If

            Dim params As SqlParameter() = {
                New SqlParameter("@inventoryyear", EISYear),
                New SqlParameter("@Active", EISActive),
                New SqlParameter("@strMailout", EISMailout),
                New SqlParameter("@strEnrollment", EISEnrollment)
            }

            dgvEISStats.Rows.Clear()

            Dim dt As DataTable = DB.GetDataTable(SQL, params)

            For Each dr As DataRow In dt.Rows
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)

                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If
                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("INVENTORYYEAR")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("INVENTORYYEAR")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If
                If IsDBNull(dr.Item("OptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("OptOut")
                End If

                If IsDBNull(dr.Item("MailOut")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("Mailout")
                End If
                If IsDBNull(dr.Item("MailoutEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("MailoutEmail")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strDMUResponsibleStaff")
                End If
                If IsDBNull(dr.Item("Enrollment")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("Enrollment")
                End If
                If IsDBNull(dr.Item("QASTATUS")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("QASTATUS")
                End If
                If IsDBNull(dr.Item("DATQASTATUS")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("DATQASTATUS")
                End If
                If IsDBNull(dr.Item("IAIPPrefix")) Then
                    dgvRow.Cells(13).Value = ""
                Else
                    dgvRow.Cells(13).Value = dr.Item("IAIPPrefix")
                End If
                If IsDBNull(dr.Item("IAIPFIRSTNAME")) Then
                    dgvRow.Cells(14).Value = ""
                Else
                    dgvRow.Cells(14).Value = dr.Item("IAIPFIRSTNAME")
                End If
                If IsDBNull(dr.Item("IAIPLASTNAME")) Then
                    dgvRow.Cells(15).Value = ""
                Else
                    dgvRow.Cells(15).Value = dr.Item("IAIPLASTNAME")
                End If
                If IsDBNull(dr.Item("IAIPEMAIL")) Then
                    dgvRow.Cells(16).Value = ""
                Else
                    dgvRow.Cells(16).Value = dr.Item("IAIPEMAIL")
                End If
                If IsDBNull(dr.Item("EISCOMPANYNAME")) Then
                    dgvRow.Cells(17).Value = ""
                Else
                    dgvRow.Cells(17).Value = dr.Item("EISCOMPANYNAME")
                End If
                If IsDBNull(dr.Item("EISADDRESS")) Then
                    dgvRow.Cells(18).Value = ""
                Else
                    dgvRow.Cells(18).Value = dr.Item("EISADDRESS")
                End If
                If IsDBNull(dr.Item("EISADDRESS2")) Then
                    dgvRow.Cells(19).Value = ""
                Else
                    dgvRow.Cells(19).Value = dr.Item("EISADDRESS2")
                End If
                If IsDBNull(dr.Item("EISCITY")) Then
                    dgvRow.Cells(20).Value = ""
                Else
                    dgvRow.Cells(20).Value = dr.Item("EISCITY")
                End If
                If IsDBNull(dr.Item("EISState")) Then
                    dgvRow.Cells(21).Value = ""
                Else
                    dgvRow.Cells(21).Value = dr.Item("EISState")
                End If
                If IsDBNull(dr.Item("EISZipCode")) Then
                    dgvRow.Cells(22).Value = ""
                Else
                    dgvRow.Cells(22).Value = dr.Item("EISZipCode")
                End If
                If IsDBNull(dr.Item("EISPrefix")) Then
                    dgvRow.Cells(23).Value = ""
                Else
                    dgvRow.Cells(23).Value = dr.Item("EISPrefix")
                End If
                If IsDBNull(dr.Item("EISFirstname")) Then
                    dgvRow.Cells(24).Value = ""
                Else
                    dgvRow.Cells(24).Value = dr.Item("EISFirstname")
                End If

                If IsDBNull(dr.Item("EISLASTNAME")) Then
                    dgvRow.Cells(25).Value = ""
                Else
                    dgvRow.Cells(25).Value = dr.Item("EISLASTNAME")
                End If

                If IsDBNull(dr.Item("DATFINALIZE")) Then
                    dgvRow.Cells(26).Value = ""
                Else
                    dgvRow.Cells(26).Value = dr.Item("DATFINALIZE")
                End If

                dgvRow.Cells(27).Value = DBUtilities.GetNullable(Of String)(dr.Item("FITrackingNumber"))
                dgvRow.Cells(28).Value = DBUtilities.GetNullable(Of String)(dr.Item("PointTrackingNumber"))
                dgvRow.Cells(29).Value = DBUtilities.GetNullable(Of String)(dr.Item("Comments"))

                dgvEISStats.Rows.Add(dgvRow)
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISSummaryToExcel_Click(sender As Object, e As EventArgs) Handles btnEISSummaryToExcel.Click
        dgvEISStats.ExportToExcel(Me)
    End Sub

#Region " Accept Button "

    Private Sub AcceptButton_Leave(sender As Object, e As EventArgs) _
    Handles mtbEILogAIRSNumber.Leave,
    txtEIModifyFacilityName.Leave,
    txtEIModifyLocation.Leave, txtEIModifyCity.Leave, mtbEIModifyZipCode.Leave,
    txtEIModifyMLocation.Leave, txtEIModifyMCity.Leave, mtbEIModifyMZipCode.Leave,
    mtbEIModifyLatitude.Leave, mtbEIModifyLongitude.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub mtbEILogAIRSNumber_Enter(sender As Object, e As EventArgs) _
    Handles mtbEILogAIRSNumber.Enter
        Me.AcceptButton = btnReloadFSData
    End Sub

    Private Sub txtEIModifyFacilityName_Enter(sender As Object, e As EventArgs) _
    Handles txtEIModifyFacilityName.Enter
        Me.AcceptButton = btnEIModifyUpdateName
    End Sub

    Private Sub EIModifyLocation_Enter(sender As Object, e As EventArgs) _
    Handles txtEIModifyLocation.Enter, txtEIModifyCity.Enter, mtbEIModifyZipCode.Enter
        Me.AcceptButton = btnEIModifyUpdateLocation
    End Sub

    Private Sub EIModifyMailing_Enter(sender As Object, e As EventArgs) _
    Handles txtEIModifyMLocation.Enter, txtEIModifyMCity.Enter, mtbEIModifyMZipCode.Enter
        Me.AcceptButton = btnEIModifyUpdateMailing
    End Sub

    Private Sub EIModifyLatitudeLongitude_Enter(sender As Object, e As EventArgs) _
    Handles mtbEIModifyLatitude.Enter, mtbEIModifyLongitude.Enter
        Me.AcceptButton = btnUpdateLatLong
    End Sub

#End Region

#Region " Operating status mismatch "

    Private Sub llbOperatingStatusMismatch_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbOperatingStatusMismatch.LinkClicked
        ShowMismatchedOperatingStatus()
    End Sub

    Private Sub ShowMismatchedOperatingStatus()
        Dim query As String = "SELECT ef.FACILITYSITEID AS ""AIRS Number"", " &
            "  ef.STRFACILITYSITENAME AS ""Facility Name"", " &
            "  ef.STRFACILITYSITESTATUSCODE AS ""EIS Site Status"", " &
            "  hd.STROPERATIONALSTATUS AS ""IAIP Site Status"" " &
            "FROM EIS_FACILITYSITE ef " &
            "INNER JOIN APBHEADERDATA hd ON ef.FACILITYSITEID = RIGHT( " &
            "  hd.STRAIRSNUMBER, 8) " &
            "WHERE(ef.STRFACILITYSITESTATUSCODE = 'OP' AND " &
            "  hd.STROPERATIONALSTATUS <> 'O') OR( " &
            "  ef.STRFACILITYSITESTATUSCODE = 'PS' AND " &
            "  hd.STROPERATIONALSTATUS <> 'X') OR( " &
            "  ef.STRFACILITYSITESTATUSCODE = 'TS' AND " &
            "  hd.STROPERATIONALSTATUS <> 'T') OR( " &
            "  ef.STRFACILITYSITESTATUSCODE <> 'OP' AND " &
            "  hd.STROPERATIONALSTATUS = 'O') OR( " &
            "  ef.STRFACILITYSITESTATUSCODE <> 'PS' AND " &
            "  hd.STROPERATIONALSTATUS = 'X') OR( " &
            "  ef.STRFACILITYSITESTATUSCODE <> 'TS' AND " &
            "  hd.STROPERATIONALSTATUS = 'T') OR " &
            "  ef.STRFACILITYSITESTATUSCODE IS NULL OR " &
            "  hd.STROPERATIONALSTATUS IS NULL " &
            "ORDER BY ef.FACILITYSITEID"
        dgvOperStatusMismatch.DataSource = DB.GetDataTable(query)
        dgvOperStatusMismatch.SanelyResizeColumns
        lblOperStatusCount.Text = dgvOperStatusMismatch.RowCount.ToString
    End Sub

#End Region

End Class