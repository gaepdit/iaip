﻿Imports Oracle.ManagedDataAccess.Client
Imports Iaip.Apb.Facilities

Public Class DMUEisGecoTool
    Dim SQL, SQL2 As String
    Dim cmd, cmd2 As OracleCommand
    Dim dr, dr2 As OracleDataReader
    Dim recExist As Boolean
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Public dsES As DataSet
    Public daES As OracleDataAdapter
    Dim dsViewCount As DataSet
    Dim daViewCount As OracleDataAdapter

    Private Sub DMUEisGecoTool_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Page Load"

    Private Sub LoadOperStatus()
        cbIaipOperStatus.BindToDictionary(FacilityOperationalStatusDescriptions)
        cbEisModifyOperStatus.BindToDictionary(EisSiteStatusCodeDescriptions)
    End Sub

    Sub LoadPermissions()
        Try
            TCDMUTools.TabPages.Remove(TPFeeTools)
            TCDMUTools.TabPages.Remove(TPESTools)

            If AccountFormAccess(130, 3) = "1" Or AccountFormAccess(130, 4) = "1" Then
                TCDMUTools.TabPages.Add(TPESTools)
                TCDMUTools.TabPages.Add(TPFeeTools)
            End If
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
            "order by intESYear desc"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Read()
            Do
                year = dr("intESYear")
                cboYear.Items.Add(year)

            Loop While dr.Read
            cboYear.SelectedIndex = 0

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub loadMailOutYear()
        'Load MailOut Year dropdown boxes
        Dim year As Integer
        Dim SQL As String

        Try
            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            SQL = "Select distinct STRESYEAR " &
                  "from AIRBRANCH.esmailout " &
                  "order by STRESYEAR desc"
            Dim cmd As New OracleCommand(SQL, CurrentConnection)

            Dim dr As OracleDataReader = cmd.ExecuteReader()
            dr.Read()
            year = dr("STRESYEAR") + 1
            cboMailoutYear.Items.Add(year)
            Do
                year = dr("STRESYEAR")
                cboMailoutYear.Items.Add(year)
            Loop While dr.Read
            cboMailoutYear.SelectedIndex = 0

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub FormatWebUsers()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadEISLog()
        Try
            Dim dtQAStatus As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "Select distinct(inventoryYear) as InvYear " &
            "from AIRBRANCH.EIS_Admin " &
            "order by invYear desc "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("InvYear")) Then
                Else
                    cboEILogYear.Items.Add(dr.Item("InvYear"))
                    cboEISStatisticsYear.Items.Add(dr.Item("InvYear"))
                End If
            End While
            dr.Close()

            SQL = "select distinct strDMUResponsibleStaff as DMUStafff " &
            "from AIRBranch.EIS_QAAdmin " &
            "union " &
            "select distinct (strLastName ||', '|| strFirstName) as DMUStafff " &
            "from AIRBranch.EPDUserProfiles " &
            "where numBranch = '1' " &
            "and numProgram = '3' " &
            "and numunit = '14' " &
            "and numEmployeeStatus = '1'  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                cboEISQAStaff.Items.Add(dr.Item("DMUStafff"))
            End While
            dr.Close()

            SQL = "Select " &
            "QAStatusCode, strDesc " &
            "From AIRBranch.EISLK_QAStatus " &
            "Where active = '1' " &
            "order by UpdateDateTime  "

            ds = New DataSet

            da = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            da.Fill(ds, "QAStatus")

            dtQAStatus.Columns.Add("QAStatusCode", GetType(System.String))
            dtQAStatus.Columns.Add("strDesc", GetType(System.String))

            drNewRow = dtQAStatus.NewRow()
            drNewRow("QAStatusCode") = ""
            drNewRow("strDesc") = ""
            dtQAStatus.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("QAStatus").Rows()
                drNewRow = dtQAStatus.NewRow()
                drNewRow("QAStatusCode") = drDSRow("QAStatusCode")
                drNewRow("strDesc") = drDSRow("strDesc")
                dtQAStatus.Rows.Add(drNewRow)
            Next

            With cboEISQAStatus
                .DataSource = dtQAStatus
                .DisplayMember = "strDesc"
                .ValueMember = "QAStatusCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadStats()
        Try
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

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Web Application Users"
    Private Sub btnActivateEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivateEmail.Click
        Try
            LoadComboBoxesEmail()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

#Region "Mahesh Code for Web App Users"

    Sub LoadComboBoxesEmail()
        Dim dtAIRS As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Dim SQL As String

        Try


            SQL = "Select numuserid, struseremail " _
            + "from AIRBRANCH.OlapUserLogin " _
            + "Order by struseremail "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            da.Fill(ds, "UserEmail")

            dtAIRS.Columns.Add("numuserid", GetType(System.String))
            dtAIRS.Columns.Add("struseremail", GetType(System.String))

            drNewRow = dtAIRS.NewRow()
            drNewRow("numuserid") = " "
            drNewRow("struseremail") = " "
            dtAIRS.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("UserEmail").Rows()
                drNewRow = dtAIRS.NewRow()
                drNewRow("numuserid") = drDSRow("numuserid")
                drNewRow("struseremail") = drDSRow("struseremail")
                dtAIRS.Rows.Add(drNewRow)
            Next

            With cboUserEmail
                .DataSource = dtAIRS
                .DisplayMember = "struseremail"
                .ValueMember = "numuserid"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

    Private Sub lblViewFacility_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewFacility.LinkClicked
        Try



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Sub LoadUserInfo(ByVal UserData As String)
        Try
            SQL = "Select " &
            "AIRBRANCH.OLAPUserProfile.numUserID, " &
            "strfirstname, strlastname, " &
            "strtitle, strcompanyname, " &
            "straddress, strcity, " &
            "strstate, strzip, " &
            "strphonenumber, strfaxnumber, " &
            "datLastLogIn, strConfirm, " &
            "strUserEmail " &
            "from AIRBRANCH.OlapUserProfile, AIRBRANCH.OLAPUserLogIn " &
            "where AIRBRANCH.OLAPUserProfile.numUserID = AIRBRANCH.OLAPUserLogIn.numuserid " &
            "and strUserEmail = upper('" & UserData & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadUserFacilityInfo(ByVal EmailLoc As String)
        Try
            Dim dgvRow As New DataGridViewRow

            SQL = "SELECT substr(strairsnumber, 5) as strAIRSNumber, strfacilityname, " &
             "Case When intAdminAccess = 0 Then 'False' When intAdminAccess = 1 Then 'True' End as intAdminAccess, " &
             "Case When intFeeAccess = 0 Then 'False' When intFeeAccess = 1 Then 'True' End as intFeeAccess, " &
             "Case When intEIAccess = 0 Then 'False' When intEIAccess = 1 Then 'True' End as intEIAccess, " &
             "Case When intESAccess = 0 Then 'False' When intESAccess = 1 Then 'True' End as intESAccess " &
             "FROM AIRBRANCH.OlapUserAccess, AIRBRANCH.OLAPUserLogIn  " &
             "WHERE AIRBRANCH.OlapUserAccess.numUserId = AIRBRANCH.OLAPUserLogIn.numUserId " &
             "and  strUserEmail = upper('" & EmailLoc & "') " &
             "order by strfacilityname"

            dgvUserFacilities.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
            End While
            dr.Close()

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "FacilityUsers")

            cboFacilityToDelete.DataSource = ds.Tables("FacilityUsers")
            cboFacilityToDelete.DisplayMember = "strfacilityname"
            cboFacilityToDelete.ValueMember = "strairsnumber"
            cboFacilityToDelete.Text = ""


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

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
        Dim ESMailoutCount As Integer
        Dim MailOutOptInCount As Integer
        Dim mailoutOptOutCount As Integer
        Dim ResponseCount As Integer
        Dim TotaloptinCount As Integer
        Dim TotaloptoutCount As Integer
        Dim TotalinincomplianceCount As Integer
        Dim TotaloutofcomplianceCount As Integer
        Dim extracount As Integer
        '  Dim extracount2 As Integer
        Dim extraOptincount As Integer
        Dim extraOptOutCount As Integer
        Dim TotalResponsecount As Integer
        Dim year As Integer = CInt(cboYear.SelectedItem)
        txtESYear.Text = cboYear.SelectedItem
        Dim ESYear As String = txtESYear.Text

        Dim removedFacilitiescount As Integer
        Dim mailoutNonresponderscount As Integer
        Dim extraNonresponderscount As Integer


        Dim intESyear As Integer = CInt(ESYear)
        Dim deadline As String = "15-Jun-" & year + 1

        Try
            Try
                SQL = "select count(*) as ESMailoutCount " &
                "from AIRBRANCH.esmailout, AIRBRANCH.ESSCHEMA " &
                "where AIRBRANCH.ESMAILOUT.STRAIRSYEAR = AIRBRANCH.ESSCHEMA.STRAIRSYEAR(+) " &
                "and AIRBRANCH.esmailout.STRESYEAR = '" & ESYear & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtESMailOutCount.Text = dr.Item(ESMailoutCount)
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

            SQL = "select count(*) as removedFacilitiescount " &
          "from AIRBRANCH.ESSchema , AIRBRANCH.esmailout " &
          "where AIRBRANCH.esMailOut.STRESYEAR = '" & ESYear & "'" &
            "and AIRBRANCH.esmailout.STRAIRSYEAR = AIRBRANCH.ESSchema.STRAIRSYEAR(+) " &
          " and AIRBRANCH.ESSchema.STRAIRSYEAR is NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read()
                txtESremovedFacilities.Text = dr.Item(removedFacilitiescount)
            End While
            dr.Close()

            SQL = "select count(*) as extraNonresponderscount " &
           "from AIRBRANCH.ESSchema " &
           " where  not exists (select * from AIRBRANCH.ESMAILOUT " &
                " where AIRBRANCH.ESSchema.STRAIRSNUMBER = AIRBRANCH.ESMAILOUT.STRAIRSNUMBER" &
                " and ESSchema.INTESYEAR = ESMAILOUT.strESYEAR) " &
                " and AIRBRANCH.ESSchema.INTESYEAR = '" & ESYear & "' " &
                " and AIRBRANCH.ESSchema.STROPTOUT is null"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read()
                txtESextranonresponder.Text = dr.Item(extraNonresponderscount)
            End While
            dr.Close()

            SQL = "select count(*) as mailoutNonresponderscount " &
          "from  AIRBRANCH.esmailout, AIRBRANCH.ESSchema " &
            "where AIRBRANCH.esmailout.strESYEAR = '" & ESYear & "' " &
            "and AIRBRANCH.esmailout.STRAIRSYEAR = AIRBRANCH.ESSchema.STRAIRSYEAR(+) " &
            "and AIRBRANCH.ESSchema.strOptOut is NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read()
                txtESmailoutNonResponder.Text = dr.Item(mailoutNonresponderscount)
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
                txtESextraResponders.Text = dr.Item(extracount)
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
    Private Sub findESMailOut()
        Dim AirsNo As String = txtESAIRSNo2.Text
        Dim ESyear As String = txtESYear.Text

        Try
            SQL = "SELECT * " &
                  "from AIRBRANCH.esMailOut " &
                  "where STRAIRSNUMBER = '" & AirsNo & "' " &
                  "and STRESYEAR = '" & ESyear & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
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
            End While

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
            End While

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try

    End Sub
    Sub ExportEStoExcel()
        dgvESDataCount.ExportToExcel(Me)
    End Sub
    Private Sub dgvESDataCount_MouseUp1(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvESDataCount.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvESDataCount.HitTest(e.X, e.Y)

        Try
            If dgvESDataCount.RowCount > 0 Then
                'If dgvESDataCount.Columns(1).HeaderText <> "Facility Name" Then
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
            "AIRBRANCH.esSchema.DBLVOCEMISSION, " &
           "AIRBRANCH.esSchema.DBLNOXEMISSION, " &
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
            dgvESDataCount.Columns("DBLVOCEMISSION").HeaderText = "VOC Emmissions"
            dgvESDataCount.Columns("DBLVOCEMISSION").DisplayIndex = 3
            dgvESDataCount.Columns("DBLNOXEMISSION").HeaderText = "NOX Emmissions"
            dgvESDataCount.Columns("DBLNOXEMISSION").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 5

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString


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
            Dim deadline As String = "15-Jun-2007"
            deadline = "15-Jun-" & txtESYear.Text + 1

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

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
            Dim deadline As String = "15-Jun-" & intYear + 1

            SQL = "SELECT AIRBRANCH.esSchema.STRAIRSNUMBER, " &
            "AIRBRANCH.esSchema.STRFACILITYNAME, " &
            "AIRBRANCH.esSchema.STRDATEFIRSTCONFIRM, " &
            "AIRBRANCH.esSchema.STRCONFIRMATIONNBR " &
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
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewESMailOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewESMailOut.LinkClicked
        Try

            Dim year As String = txtESYear.Text
            txtESYear.Text = cboYear.SelectedItem


            SQL = "SELECT esMailOut.STRAIRSNUMBER, " &
            "esMailOut.STRFACILITYNAME, " &
            "esMailOut.STRCONTACTFIRSTNAME, " &
            "esMailOut.STRCONTACTLASTNAME, " &
            "esMailOut.STRCONTACTCOMPANYname, " &
            "esMailOut.STRCONTACTADDRESS1, " &
            "esMailOut.STRCONTACTCITY, " &
            "esMailOut.STRCONTACTSTATE, " &
            "esMailOut.STRCONTACTZIPCODE, " &
            "esMailOut.STRCONTACTEMAIL " &
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
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewESData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewESData.LinkClicked
        Dim year As Integer = CInt(cboYear.SelectedItem)
        Try

            dsES = New DataSet

            SQL = "SELECT esSchema.STRAIRSNUMBER, " &
            "esSchema.STRFACILITYNAME, " &
            "case " &
            "when DBLVOCEMISSION= '-1' then 'No Value' " &
            "else to_char(DBLVOCEMISSION) " &
            "end DBLVOCEMISSION, " &
            "esSchema.STRCONFIRMATIONNBR, " &
            "case " &
            "when DBLNOXEMISSION = '-1' then 'No Value' " &
            "else to_char(DBLNOXEMISSION) " &
            "end DBLNOXEMISSION, " &
            "esSchema.STRDATEFIRSTCONFIRM " &
            "from AIRBRANCH.esSchema " &
            "where esSchema.intESyear = '" & year & "' " &
            "order by esSchema.STRFACILITYNAME"

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
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3
            dgvESDataCount.Columns("DBLNOXEMISSION").HeaderText = "NOX Emissions"
            dgvESDataCount.Columns("DBLNOXEMISSION").DisplayIndex = 4
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

            Dim year As String = txtESYear.Text

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

            ' clearESData()

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

            'clearESData()
            'ClearMailOut()

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

            clearESData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

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
        Dim ESYear As String = txtESYear.Text
        Dim ESContactCity As String = txtcontactCity.Text
        Dim EScontactState As String = txtcontactState.Text
        Dim ESContactZip As String = txtcontactZipCode.Text
        Dim ESContactEmail As String = txtcontactEmail.Text
        Dim airsYear As String = AirsNo & ESYear

        Try
            SQL = "Select strAIRSYear " &
            "from AIRBRANCH.EsMailOut " &
            "where STRAIRSYEAR = '" & airsYear & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                SQL = "update AIRBRANCH.ESMailOut set " &
                "AIRBRANCH.ESMailOut.STRCONTACTPREFIX = '" & ESPrefix & "', " &
                "AIRBRANCH.ESMailOut.STRCONTACTFIRSTNAME = '" & ESFirstName & "', " &
                "AIRBRANCH.ESMailOut.STRCONTACTLASTNAME = '" & ESLastName & "', " &
                "AIRBRANCH.ESMailOut.STRCONTACTCOMPANYNAME = '" & ESCompanyName & "', " &
                "AIRBRANCH.ESMailOut.STRCONTACTADDRESS1 = '" & ESContactAddress1 & "', " &
                "AIRBRANCH.ESMailOut.STRCONTACTADDRESS2 = '" & ESContactAddress2 & "', " &
                "AIRBRANCH.ESMailOut.STRCONTACTCITY = '" & ESContactCity & "', " &
                "AIRBRANCH.ESMailOut.STRCONTACTSTATE = '" & EScontactState & "', " &
                "AIRBRANCH.ESMailOut.STRCONTACTZIPCODE = '" & ESContactZip & "', " &
                "AIRBRANCH.ESMailOut.STRCONTACTEMAIL = '" & ESContactEmail & "'" &
                "where ESMailOut.STRAIRSNUMBER = '" & AirsNo & "' "

                MsgBox("your info is updated!")

            Else
                SQL = "Insert into AIRBRANCH.ESMailOut " &
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
                "'" & Replace(airsYear, "'", "''") & "', " &
                "'" & Replace(AirsNo, "'", "''") & "', " &
                "'" & Replace(ESFacilityName, "'", "''") & "', " &
                "'" & Replace(ESPrefix, "'", "''") & "', " &
                "'" & Replace(ESFirstName, "'", "''") & "', " &
                "'" & Replace(ESLastName, "'", "''") & "', " &
                "'" & Replace(ESCompanyName, "'", "''") & "', " &
                "'" & Replace(ESContactAddress1, "'", "''") & "', " &
                "'" & Replace(ESContactAddress2, "'", "''") & "', " &
                "'" & Replace(ESContactCity, "'", "''") & "', " &
                "'" & Replace(EScontactState, "'", "''") & "', " &
                "'" & Replace(ESContactZip, "'", "''") & "', " &
                "'" & Replace(ESYear, "'", "''") & "', " &
                "'" & Replace(ESContactEmail, "'", "''") & "' " &
                " )"

                MsgBox("your info is added!")

            End If

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
    Private Sub DeleteESMailOut()
        Dim AirsNo As String = txtESAIRSNo2.Text
        Dim ESyear As String = txtESYear.Text

        Try
            SQL = "delete from AIRBRANCH.ESMailOut " &
            "where AIRBRANCH.ESMailOut.STRAIRSNUMBER = '" & AirsNo & "' " &
            "and AIRBRANCH.ESMailOut.STRESYEAR = '" & ESyear & "'"

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveESMailOut()
            MsgBox("The info has been saved!")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnExporttoExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExporttoExcel.Click
        Try
            ExportEStoExcel()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnESDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnESDelete.Click
        Try
            DeleteESMailOut()
            ClearMailOut()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        MsgBox("The info has been deleted!")
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
        End Try
    End Sub
    Sub GenerateESMailOut()
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



            SQL = "Select strAirsNumber " &
            "FROM AIRBRANCH.ESmailOut " &
            "where strESyear = '" & ESYear & "'"


            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            recExist = dr.Read

            dr.Close()

            If recExist = True Then
                MsgBox("That year is already being used." & vbCrLf & "If you want to use that year," & vbCrLf & "you must first delete that year from the database.")
            Else
                If cboMailoutYear.Text <> "" Then
                    If cboMailoutYear.Text.Length = 4 Then
                        SQL = "Select dt_EScontact.STRairsnumber, AIRBRANCH.APBFacilityinformation.STRFACILITYNAME, " &
                        "AIRBRANCH.APBHEADERDATA.stroperationalstatus, AIRBRANCH.APBHEADERDATA.STRCLASS, " &
                        "(Case " &
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRContactLastName " &
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRContactLastName " &
                        "Else 'N/A' " &
                        "END) STRContactLastName, " &
                        "(Case " &
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRContactfirstName " &
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRContactfirstName " &
                        "Else 'N/A' " &
                        "END) STRContactfirstName, " &
                        "(Case " &
                        "When dt_esContact.STRKEY='42' THEN dt_esContact.STRContactCompanyName " &
                        "When dt_esContact.STRKEY Is Null THEN dt_PermitContact.STRContactCompanyName " &
                        "END) STRContactCompanyName, " &
                        "(Case " &
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRContactEmail " &
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRContactEmail " &
                        "END) STRContactEmail, " &
                        "(Case " &
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRCONTACTPREFIX " &
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTPREFIX " &
                        "END) strCONTACTPREFIX, " &
                        "(Case " &
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRCONTACTADDRESS1 " &
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTADDRESS1 " &
                        "END) STRCONTACTADDRESS1, " &
                        "(Case " &
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRCONTACTCITY " &
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTCITY " &
                        "END) STRCONTACTCITY, " &
                        "(Case " &
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRCONTACTSTATE " &
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTSTATE " &
                        "END) STRCONTACTSTATE,  " &
                        "(Case " &
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRCONTACTZIPCODE " &
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTZIPCODE " &
                        "END) STRCONTACTZIPCODE " &
                        "From " &
                        "(Select DISTINCT dt_eslist.STRAIRSNUMBER, dt_contact.STRKEY,  " &
                        "dt_Contact.STRCONTACTLASTNAME, dt_Contact.STRCONTACTFIRSTNAME, " &
                        "dt_Contact.STRContactCompanyName, dt_Contact.STRContactEmail,  " &
                        "dt_Contact.STRCONTACTPREFIX, dt_Contact.STRCONTACTADDRESS1, dt_Contact.STRCONTACTCITY,  " &
                        "dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE " &
                        "FROM " &
                        "(Select * FROM AIRBRANCH.APBHEADERDATA " &
                        "where (stroperationalstatus = 'O' OR stroperationalstatus = 'P' oR stroperationalstatus = 'C') AND  " &
                        "(STRCLASS = 'A')   " &
                        "AND (AIRBRANCH.apbheaderdata.STRAIRSNUMBER Like '____121%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____013%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____015%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____045%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____057%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____063%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____067%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____077%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____089%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____097%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____113%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____117%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____135%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____139%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____151%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____217%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____223%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____247%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____255%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____297%') " &
                        " ) dt_ESList,      " &
                        "(Select * From AIRBRANCH.APBCONTACTINFORMATION where STRKEY=42) dt_Contact " &
                        "Where dt_ESList.STRAIRSNUMBEr = dt_Contact.STRAIRSNUMBER (+)) dt_ESContact, " &
                        "(Select DISTINCT dt_eslist.STRAIRSNUMBER, dt_contact.STRKEY,  " &
                        "dt_Contact.STRCONTACTLASTNAME, dt_Contact.STRCONTACTFIRSTNAME, " &
                        "dt_Contact.STRContactCompanyName, dt_Contact.STRContactEmail, dt_Contact.STRCONTACTPREFIX,  " &
                        "dt_Contact.STRCONTACTADDRESS1, dt_Contact.strcontactcity, dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE " &
                        "FROM " &
                        "(Select * FROM AIRBRANCH.APBHEADERDATA " &
                        "where (stroperationalstatus = 'O' OR stroperationalstatus = 'P' oR stroperationalstatus = 'C') AND  " &
                        "(STRCLASS = 'A')   " &
                        "AND (AIRBRANCH.apbheaderdata.STRAIRSNUMBER Like '____121%'    " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____013%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____015%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____045%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____057%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____063%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____067%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____077%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____089%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____097%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____113%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____117%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____135%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____139%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____151%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____217%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____223%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____247%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____255%' " &
                        "or AIRBRANCH.apbheaderdata.STRAIRSNUMBER like '____297%') " &
                        ")dt_ESList,  " &
                        "(Select * From AIRBRANCH.APBCONTACTINFORMATION where STRKEY=30) dt_Contact " &
                        "Where dt_ESList.STRAIRSNUMBEr = dt_Contact.STRAIRSNUMBER (+)) dt_PermitContact, " &
                        "AIRBRANCH.APBFACILITYINFORMATION, " &
                        "AIRBRANCH.APBHEADERDATA " &
                        "Where AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER= dt_ESContact.STRAIRSNumber and  " &
                        "AIRBRANCH.APBHEADERDATA.STRAIRSNUMBER= dt_ESContact.STRAIRSNumber and  " &
                        "dt_ESContact.STRAIRSNumber  = dt_PermitContact.STRAIRSNUMBER (+) "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader

                        dr.Read()
                        Do
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

                            SQL2 = "insert into AIRBRANCH.ESmailOut " &
                            "(strAirsYear, " &
                            "strAirsNumber, " &
                            "STRFACILITYNAME, " &
                            "STROPERATIONALSTATUS, " &
                            "STRCLASS, " &
                            "STRCONTACTCOMPANYNAME, " &
                            "STRCONTACTADDRESS1, " &
                            "STRCONTACTCITY, " &
                            "STRCONTACTSTATE, " &
                            "STRCONTACTZIPCODE, " &
                            "STRCONTACTFIRSTNAME, " &
                            "STRCONTACTLASTNAME, " &
                            "STRCONTACTEMAIL, " &
                            "strESYear) " &
                            "values " &
                            "('" & airsYear & "', " &
                            "'" & airsNumber & "', " &
                            "'" & Replace(FACILITYNAME, "'", "''") & "', " &
                            "'" & Replace(OperationalStatus, "'", "''") & "', " &
                            "'" & Replace(FacilityClass, "'", "''") & "', " &
                             "'" & Replace(Replace(CONTACTCOMPANYNAME, "'", "''"), "N/A", " ") & "', " &
                            "'" & Replace(Replace(CONTACTADDRESS1, "'", "''"), "N/A", " ") & "', " &
                            "'" & Replace(Replace(CONTACTCITY, "'", "''"), "N/A", " ") & "', " &
                            "'" & CONTACTSTATE & "', " &
                            "'" & Replace(CONTACTZIPCODE, "N/A", " ") & "', " &
                            "'" & Replace(Replace(CONTACTFIRSTNAME, "'", "''"), "N/A", " ") & "', " &
                            "'" & Replace(Replace(CONTACTLASTNAME, "'", "''"), "N/A", " ") & "', " &
                            "'" & Replace(Replace(CONTACTEMAIL, "'", "''"), "N/A", " ") & "', " &
                            "'" & ESYear & "')"

                            Dim cmd2 As New OracleCommand(SQL2, CurrentConnection)
                            'If conn.State = ConnectionState.Closed Then
                            '    conn.Open()
                            'End If
                            cmd2.CommandType = CommandType.Text

                            dr2 = cmd2.ExecuteReader
                            'dr2.Close()
                        Loop While dr.Read


                    End If


                    Dim year As String = cboMailoutYear.SelectedItem

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub deleteESmailOutbyYear()
        Dim ESyear As String = cboMailoutYear.SelectedItem

        Try


            If ESyear = "Select a Mailout Year & Click Below" Then
                MsgBox("You must select a Mailout Year")
            Else
                SQL = "delete from AIRBRANCH.ESmailout " &
                "where strESyear = '" & ESyear & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                MsgBox("ES mail out is deleted!")

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnGenMailOut_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenMailOut.Click
        Try
            GenerateESMailOut()
            cboMailoutYear.Items.Clear()
            loadMailOutYear()
            cboMailoutYear.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDelMailOut_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelMailOut.Click
        Try
            deleteESmailOutbyYear()
            cboMailoutYear.Items.Clear()
            loadMailOutYear()
            cboMailoutYear.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewselectedyearMailoutList_LinkClicked_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewselectedyearMailoutList.LinkClicked

        Try
            Dim year As String = cboMailoutYear.SelectedItem

            SQL = "SELECT STRAIRSNUMBER, " &
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "EI Tool"

    Private Sub loadESEnrollmentYear()
        'Load MailOut Year dropdown boxes
        Dim year As Integer
        Dim SQL As String
        Try
            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            SQL = "Select distinct ESMAILOUT.STRESYEAR " &
                      "from AIRBRANCH.ESMAILOUT  " &
                      "order by STRESYEAR desc"
            Dim cmd As New OracleCommand(SQL, CurrentConnection)

            Dim dr As OracleDataReader = cmd.ExecuteReader()
            dr.Read()
            Do
                year = dr("STRESYEAR")
                cboESYear.Items.Add(year)
            Loop While dr.Read
            ' cboESYear.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

    Private Sub lblViewEmailData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEmailData.LinkClicked
        Try
            LoadUserInfo(txtWebUserEmail.Text)
            LoadUserFacilityInfo(txtWebUserEmail.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub cboUserEmail_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUserEmail.SelectedValueChanged
        Try
            txtWebUserEmail.Text = cboUserEmail.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

    Private Sub btnESenrollment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnESenrollment.Click
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub ESMailoutenrollment()
        Dim AirsNo As String
        Dim FacilityName As String
        Dim ESYear As Integer = CInt(cboESYear.SelectedItem)
        Dim airsYear As String

        Try
            SQL = "Select * " &
            "FROM AIRBRANCH.ESSCHEMA " &
            "where ESSCHEMA.INTESYEAR = '" & ESYear & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                MsgBox("That year " & ESYear & " is already enrolled.", MsgBoxStyle.Information, "EI Enrollment")
            Else
                SQL = "Select ESMAILOUT.STRAIRSNUMBER, ESMAILOUT.STRFACILITYNAME " &
                "FROM AIRBRANCH.ESMAILOUT " &
                "where ESMAILOUT.STRESYEAR = '" & ESYear & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Read()
                Do
                    AirsNo = dr("strAirsNumber")
                    airsYear = AirsNo & ESYear
                    FacilityName = dr("STRFACILITYNAME")
                    SQL2 = "Insert into AIRBRANCH.ESSCHEMA " &
                    "(ESSCHEMA.STRAIRSNUMBER, " &
                    "ESSCHEMA.STRFACILITYNAME, " &
                    "ESSCHEMA.DATTRANSACTION, " &
                    "ESSCHEMA.INTESYEAR, " &
                    "ESSCHEMA.NUMUSERID, " &
                    "ESSCHEMA.STRAIRSYEAR) " &
                    "values " &
                    "('" & Replace(AirsNo, "'", "''") & "', " &
                    "'" & Replace(FacilityName, "'", "''") & "', " &
                    "'" & OracleDate & "', " &
                    "'" & Replace(ESYear, "'", "''") & "', " &
                    "'3', " &
                    "'" & airsYear & "' )"

                    cmd2 = New OracleCommand(SQL2, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                Loop While dr.Read
                MsgBox("The facilities for year " & ESYear & " have been enrolled", MsgBoxStyle.Information, "EI Enrollment")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnESdeenrollment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnESdeenrollment.Click
        Dim ESYear As Integer = CInt(cboESYear.SelectedItem)
        Dim sql As String
        Try
            If cboESYear.Text = "" Then
                MsgBox("Please choose a year!", MsgBoxStyle.Information, "ES Enrollment")
            Else
                Dim intAnswer As DialogResult
                intAnswer = MessageBox.Show("Remove the enrollment?", "ES Enrollment", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                Select Case intAnswer
                    Case DialogResult.OK
                        sql = "delete from AIRBRANCH.ESSCHEMA " &
                        "where ESSCHEMA.INTESYEAR = '" & ESYear & "'"
                        cmd = New OracleCommand(sql, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                        MsgBox("Enrollment has been removed!", MsgBoxStyle.Information, "ES Enrollment")
                    Case Else
                        MsgBox("Enrollment has not been removed!", MsgBoxStyle.Information, "ES Enrollment")
                End Select

                cboESYear.Items.Clear()
                loadESEnrollmentYear()
                cboESYear.Text = ""
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnaddfacilitytoES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddfacilitytoES.Click
        Try
            addonefacilityES()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub addonefacilityES()
        Dim AirsNo As String = "0413" & txtESairNumber.Text
        Dim ESYear As Integer = txtESYearforFacility.Text
        Dim airsYear As String = AirsNo & ESYear
        Dim facilityName As String


        Try

            SQL = "Select AIRBRANCH.ESSCHEMA.INTESYEAR " &
          "FROM AIRBRANCH.ESSCHEMA " &
          "where  ESSCHEMA.INTESYEAR = '" & ESYear & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read


            If recExist = True Then


                SQL = "Select AIRBRANCH.APBFACILITYINFORMATION.STRFACILITYNAME " &
               "FROM AIRBRANCH.APBFACILITYINFORMATION " &
               "where  APBFACILITYINFORMATION.STRAIRSNUMBER = '" & AirsNo & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read


                If recExist = True Then
                    facilityName = dr("STRFACILITYNAME")

                    SQL = "Select * " &
                             "FROM AIRBRANCH.ESSCHEMA " &
                             "where ESSCHEMA.INTESYEAR = '" & ESYear & "' " &
                             " And ESSCHEMA.STRAIRSNUMBER = '" & AirsNo & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read

                    If recExist = True Then
                        MsgBox("This facility (" & AirsNo & ") is already enrolled for " & ESYear & ".", MsgBoxStyle.Information, "ES Enrollment")
                    Else
                        SQL2 = "Insert into AIRBRANCH.ESSCHEMA " &
                        "(ESSCHEMA.STRAIRSNUMBER, " &
                        "ESSCHEMA.STRFACILITYNAME, " &
                        "ESSCHEMA.DATTRANSACTION, " &
                        "ESSCHEMA.INTESYEAR, " &
                        "ESSCHEMA.NUMUSERID, " &
                        "ESSCHEMA.STRAIRSYEAR) " &
                        "values " &
                        "('" & Replace(AirsNo, "'", "''") & "', " &
                        "'" & Replace(facilityName, "'", "''") & "', " &
                        "'" & OracleDate & "', " &
                        "'" & Replace(ESYear, "'", "''") & "', " &
                        "'3', " &
                        "'" & airsYear & "' )"

                        cmd2 = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        MsgBox("This facility (" & AirsNo & ") has been enrolled for " & ESYear & ".", MsgBoxStyle.Information, "ES Enrollment")
                    End If

                Else

                    MsgBox("This Airs Number (" & AirsNo & ") is not valid. Please enter valid Airs Number.", MsgBoxStyle.Information, "ES Enrollment")
                End If
            Else
                MsgBox("This year (" & ESYear & ") has not been enrolled.", MsgBoxStyle.Information, "ES Enrollment")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnremoveFacilityES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremoveFacilityES.Click
        Dim ESYear As Integer = txtESYearforFacility.Text
        Dim AirsNo As String = "0413" & txtESairNumber.Text

        Dim sql As String
        Try

            Dim intAnswer As DialogResult
            intAnswer = MessageBox.Show("Remove this facility (" & AirsNo & ") for " & ESYear & "?", "ES Enrollment", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
            Select Case intAnswer
                Case DialogResult.OK
                    sql = "delete from AIRBRANCH.ESSCHEMA " &
                    "where ESSCHEMA.INTESYEAR = '" & ESYear & "'" &
                    " And ESSCHEMA.STRAIRSNUMBER = '" & AirsNo & "'"
                    cmd = New OracleCommand(sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    MsgBox("This Facility (" & AirsNo & ") has been removed for " & ESYear & "!", MsgBoxStyle.Information, "ES Enrollment")
                Case Else
                    MsgBox("This Facility (" & AirsNo & ") has not been removed for " & ESYear & "!", MsgBoxStyle.Information, "ES Enrollment")
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnCheckESstatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckESstatus.Click
        Dim AirsNo As String = "0413" & txtESairNumber.Text
        Dim ESYear As Integer = txtESYearforFacility.Text

        Try
            SQL = "Select strAIRSYear as RowCount " &
            "FROM AIRBRANCH.ESSCHEMA " &
            "where AIRBRANCH.ESSCHEMA.INTESYEAR = '" & ESYear & "' " &
            " And AIRBRANCH.ESSCHEMA.STRAIRSNUMBER = '" & AirsNo & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read

            If recExist = True Then
                MsgBox("This facility (" & AirsNo & ") has been enrolled for " & ESYear & "!", MsgBoxStyle.Information, "ES Enrollment")
            Else
                MsgBox("This facility (" & AirsNo & ") is not enrolled for " & ESYear & "!", MsgBoxStyle.Information, "ES Enrollment")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewESenrollment_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewESenrollment.LinkClicked
        Try

            Dim year As String = cboESYear.Text

            If cboESYear.Text = "" Then

                MsgBox("Please choose a year to view!", MsgBoxStyle.Information, "ES Enrollment")

            Else
                SQL = "SELECT AIRBRANCH.ESSCHEMA.STRAIRSNUMBER, " &
        "AIRBRANCH.ESSCHEMA.STRFACILITYNAME, " &
        "AIRBRANCH.ESSCHEMA.DATTRANSACTION " &
        "from AIRBRANCH.ESSCHEMA " &
        "where AIRBRANCH.ESSCHEMA.INTESYEAR = '" & year & "'"

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
                dgvESDataCount.Columns("DATTRANSACTION").HeaderText = "Transaction Date"
                dgvESDataCount.Columns("DATTRANSACTION").DisplayIndex = 2

                txtRecordNumber.Text = dgvESDataCount.RowCount.ToString



            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewESextraresponder_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewESextraresponder.LinkClicked
        Try

            Dim year As String = txtESYear.Text
            Dim intyear As Integer = Int(year)

            SQL = "SELECT AIRBRANCH.esSchema.STRAIRSNUMBER, " &
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
            "AND ESSCHEMA.INTESYEAR=  '" & intyear & "' " &
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
            '    txtextraResponse.Text = dgvESDataCount.RowCount.ToString

            clearESData()
            ClearMailOut()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewESremovedfacility_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewESremovedfacility.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT AIRBRANCH.ESMAILOUT.STRAIRSNUMBER, " &
            "AIRBRANCH.ESMAILOUT.STRFACILITYNAME " &
            "from AIRBRANCH.esSchema, AIRBRANCH.esmailout " &
            "where AIRBRANCH.esMailOut.strESyear = '" & intYear & "' " &
            "and AIRBRANCH.esSchema.STRAIRSYEAR is null " &
            "and AIRBRANCH.ESMAILOUT.STRAIRSYEAR = AIRBRANCH.ESSCHEMA.STRAIRSYEAR(+) " &
            "order by AIRBRANCH.ESMAILOUT.STRFACILITYNAME"

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

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewmailoutnonresponder_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewmailoutnonresponder.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT AIRBRANCH.esMailOut.STRAIRSNUMBER, " &
             "AIRBRANCH.esMailOut.STRFACILITYNAME, " &
             "AIRBRANCH.esMailOut.STRCONTACTFIRSTNAME, " &
             "AIRBRANCH.esMailOut.STRCONTACTLASTNAME, " &
             "AIRBRANCH.esMailOut.STRCONTACTCOMPANYname, " &
             "AIRBRANCH.esMailOut.STRCONTACTADDRESS1, " &
             "AIRBRANCH.esMailOut.STRCONTACTCITY, " &
             "AIRBRANCH.esMailOut.STRCONTACTSTATE, " &
             "AIRBRANCH.esMailOut.STRCONTACTZIPCODE, " &
             "AIRBRANCH.esMailOut.STRCONTACTEMAIL " &
            "from  AIRBRANCH.esmailout, AIRBRANCH.ESSchema " &
            "where AIRBRANCH.esmailout.strESYEAR = '" & intYear & "' " &
            "and AIRBRANCH.esmailout.STRAIRSYEAR = AIRBRANCH.ESSchema.STRAIRSYEAR(+) " &
            "and AIRBRANCH.ESSchema.strOptOut is NULL " &
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewextraNonresponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewextraNonresponse.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT AIRBRANCH.esSchema.STRAIRSNUMBER, " &
                "AIRBRANCH.esSchema.STRFACILITYNAME " &
                "from AIRBRANCH.ESSchema " &
                " where  not exists (select * from AIRBRANCH.ESMAILOUT " &
                " where AIRBRANCH.ESSchema.STRAIRSNUMBER = AIRBRANCH.ESMAILOUT.STRAIRSNUMBER" &
                " and ESSchema.INTESYEAR = ESMAILOUT.strESYEAR) " &
                " and AIRBRANCH.ESSchema.INTESYEAR = '" & intYear & "' " &
                " and AIRBRANCH.ESSchema.STROPTOUT is null   " &
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
            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub loadcboEISstatusCodes()
        Dim dtCode As New DataTable
        Dim dscode As DataSet
        Dim dacode As OracleDataAdapter
        Dim daEIcode As OracleDataAdapter
        Dim dtEICode As New DataTable()

        Dim drDSRow As DataRow
        Dim DrNewRow As DataRow
        Dim Drnewrow2 As DataRow

        Dim SQL As String

        Try
            SQL = "Select distinct  EISSTATUSCODE, STRDESC " &
            "from AIRBRANCH.EISLK_EISSTATUSCODE "

            dscode = New DataSet
            dacode = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dacode.Fill(dscode, "EISLK_EISSTATUSCODE")

            dtCode.Columns.Add("EISSTATUSCODE", GetType(System.String))
            dtCode.Columns.Add("STRDESC", GetType(System.String))
            DrNewRow = dtCode.NewRow()
            DrNewRow("EISSTATUSCODE") = ""
            DrNewRow("STRDESC") = "- Select EIS Status Code -"
            dtCode.Rows.Add(DrNewRow)

            For Each drDSRow In dscode.Tables("EISLK_EISSTATUSCODE").Rows()
                DrNewRow = dtCode.NewRow()
                DrNewRow("EISSTATUSCODE") = drDSRow("EISSTATUSCODE")
                DrNewRow("STRDESC") = drDSRow("STRDESC")
                dtCode.Rows.Add(DrNewRow)
            Next

            With cboEILogStatusCode
                .DataSource = dtCode
                .DisplayMember = "STRDESC"
                .ValueMember = "EISSTATUSCODE"
                .SelectedIndex = 0
            End With

            SQL = "select strDesc, EISAccessCode " &
            " from AIRBranch.EISLK_EISAccesscode  " &
            "order by strDesc"

            dscode = New DataSet
            daEIcode = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daEIcode.Fill(dscode, "EISAccessCode")

            dtEICode.Columns.Add("EISAccessCode", GetType(System.String))
            dtEICode.Columns.Add("STRDESC", GetType(System.String))
            Drnewrow2 = dtEICode.NewRow()
            Drnewrow2("EISAccessCode") = ""
            Drnewrow2("STRDESC") = "- Select EIS Access Code -"
            dtEICode.Rows.Add(Drnewrow2)

            For Each drDSRow In dscode.Tables("EISAccessCode").Rows()
                Drnewrow2 = dtEICode.NewRow()
                Drnewrow2("EISAccessCode") = drDSRow("EISAccessCode")
                Drnewrow2("STRDESC") = drDSRow("STRDESC")
                dtEICode.Rows.Add(Drnewrow2)
            Next

            With cboEILogAccessCode
                .DataSource = dtEICode
                .DisplayMember = "STRDESC"
                .ValueMember = "EISAccessCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            '  ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub llbViewUserData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewUserData.LinkClicked
        Try
            ViewFacilitySpecificUsers()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewFacilitySpecificUsers()
        Try
            Dim dgvRow As New DataGridViewRow

            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please enter a complete 8 digit AIRS #.", MsgBoxStyle.Information, "DMU Tools")
            Else
                txtEmail.Clear()

                SQL = "Select strFacilityName " &
                "from AIRBRANCH.APBFacilityInformation " &
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        lblFaciltyName.Text = " - "
                    Else
                        lblFaciltyName.Text = Facility.SanitizeFacilityNameForDb(dr.Item("strFacilityName"))
                    End If
                End While
                dr.Close()

                SQL = "SELECT " &
                "AIRBRANCH.OlapUserAccess.NumUserID as ID, AIRBRANCH.OlapUserLogin.numuserid, " &
                "AIRBRANCH.OlapUserLogin.strUserEmail as Email, " &
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
                "FROM AIRBRANCH.OlapUserAccess, AIRBRANCH.OlapUserLogin " &
                "WHERE AIRBRANCH.OLAPUserAccess.NumUserId = AIRBRANCH.OlapUserLogin.NumUserID " &
                "AND AIRBRANCH.OlapUserAccess.strAirsNumber = '0413" & mtbAIRSNumber.Text & "' order by email"

                dgvUsers.Rows.Clear()
                ds = New DataSet

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
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
                End While
                dr.Close()

                da = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                da.Fill(ds, "FacilityUsers")

                cboUsers.DataSource = ds.Tables("FacilityUsers")
                cboUsers.DisplayMember = "Email"
                cboUsers.ValueMember = "ID"
                cboUsers.Text = ""
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        Try
            Dim userID As Integer

            SQL = "Select numUserId " &
            "from AIRBRANCH.olapuserlogin " &
            "where struseremail = '" & Replace(UCase(txtEmail.Text), "'", "''") & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then 'Email address is registered
                userID = dr.Item("numUserId")
                Dim InsertString As String = "Insert into AIRBRANCH.OlapUserAccess " &
                "(numUserId, strAirsNumber, strFacilityName) values( " &
                "'" & userID & "', '0413" & mtbAIRSNumber.Text & "', '" & Replace(lblFaciltyName.Text, "'", "''") & "') "

                Dim cmd1 As New OracleCommand(InsertString, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd1.ExecuteNonQuery()

                ViewFacilitySpecificUsers()

                MsgBox("The User has beed added to this facility", MsgBoxStyle.Information, "Insert Success!")
            Else 'email address not registered
                MsgBox("This Email Address is not registered", MsgBoxStyle.OkOnly, "Insert Failed!")
            End If

            If dr.IsClosed = False Then dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            SQL = "DELETE AIRBRANCH.OlapUserAccess " &
            "WHERE numUserID = '" & cboUsers.SelectedValue & "' " &
            "and strAirsNumber = '0413" & mtbAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            ViewFacilitySpecificUsers()
            MsgBox("The User has been removed for this facility", MsgBoxStyle.Information, "User Removed!")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
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

                SQL = "UPDATE AIRBRANCH.OlapUserAccess " &
                "SET intadminaccess = '" & adminaccess & "', " &
                "intFeeAccess = '" & feeaccess & "', " &
                "intEIAccess = '" & eiaccess & "', " &
                "intESAccess = '" & esaccess & "' " &
                "WHERE numUserID = '" & dgvUsers(1, i).Value & "' " &
                "and strAirsNumber = '0413" & mtbAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            Next

            ViewFacilitySpecificUsers()
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditUserData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditUserData.Click
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveEditedData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveEditedData.Click
        Try
            Dim FirstName As String = " "
            Dim LastName As String = " "
            Dim Title As String = " "
            Dim Company As String = " "
            Dim Address As String = " "
            Dim City As String = " "
            Dim State As String = " "
            Dim Zip As String = " "
            Dim PhoneNumber As String = " "
            Dim FaxNumber As String = " "

            If txtWebUserID.Text <> "" Then
                If txtEditFirstName.Text <> "" Then
                    FirstName = " strFirstName = '" & Replace(txtEditFirstName.Text, "'", "''") & "', "
                End If
                If txtEditLastName.Text <> "" Then
                    LastName = " strLastName = '" & Replace(txtEditLastName.Text, "'", "''") & "', "
                End If
                If txtEditTitle.Text <> "" Then
                    Title = " strTitle = '" & Replace(txtEditTitle.Text, "'", "''") & "', "
                End If
                If txtEditCompany.Text <> "" Then
                    Company = " strCompanyName = '" & Replace(txtEditCompany.Text, "'", "''") & "', "
                End If
                If txtEditAddress.Text <> "" Then
                    Address = " strAddress = '" & Replace(txtEditAddress.Text, "'", "''") & "', "
                End If
                If txtEditCity.Text <> "" Then
                    City = " strCity = '" & Replace(txtEditCity.Text, "'", "''") & "', "
                End If
                If mtbEditState.Text <> "" Then
                    State = " strState = '" & Replace(mtbEditState.Text, "'", "''") & "', "
                End If
                If mtbEditZipCode.Text <> "" Then
                    Zip = " strZip = '" & Replace(mtbEditZipCode.Text, "'", "''") & "', "
                End If
                If mtbEditPhoneNumber.Text <> "" Then
                    PhoneNumber = " strPhoneNumber = '" & Replace(mtbEditPhoneNumber.Text, "'", "''") & "', "
                End If
                If mtbEditFaxNumber.Text <> "" Then
                    FaxNumber = " strFaxNumber = '" & Replace(mtbEditFaxNumber.Text, "'", "''") & "', "
                End If

                SQL = "Update AIRBRANCH.OLAPUserProfile set " &
                FirstName & LastName & Title & Company & Address &
                City & State & Zip & PhoneNumber & FaxNumber &
                "numUserID = '" & txtWebUserID.Text & "' " &
                "where numUserID = '" & txtWebUserID.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

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
            Else

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdatePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdatePassword.Click
        Try
            If txtWebUserID.Text <> "" And txtEditUserPassword.Text <> "" Then
                'New password change code 6/30/2010
                SQL = "Update AIRBRANCH.OLAPUserLogIN set " &
                "strUserPassword = '" & getMd5Hash(txtEditUserPassword.Text) & "' " &
                "where numUserID = '" & txtWebUserID.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnChangeEmailAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeEmailAddress.Click
        Try
            If txtWebUserID.Text <> "" Then
                If IsValidEmailAddress(txtEditEmail.Text) Then
                    SQL = "Select " &
                    "numUserID, strUserPassword " &
                    "from AIRBRANCH.OLAPUserLogIN " &
                    "where upper(strUserEmail) = '" & Replace(txtEditEmail.Text.ToUpper, "'", "''") & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("numUserID")) Then
                            Else
                                If txtWebUserID.Text <> dr.Item("numUserID") Then
                                    MsgBox("Another user already has this email address and it would violate a unique constraint if you were " &
                                           "to add this email to this user.", MsgBoxStyle.Exclamation, "Mailout and Stats")
                                    Exit Sub
                                End If
                            End If
                        End While
                        dr.Close()
                    End If

                    SQL = "Update AIRBRANCH.OLAPUserLogIn set " &
                    "strUserEmail = '" & Replace(txtEditEmail.Text.ToUpper, "'", "''") & "' " &
                    "where numUserID = '" & txtWebUserID.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    cboUserEmail.Text = ""
                    txtWebUserEmail.Text = txtEditEmail.Text

                    '  LoadDataGridFacility(txtWebUserEmail.Text)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddFacilitytoUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFacilitytoUser.Click
        Try
            If txtWebUserID.Text <> "" And mtbFacilityToAdd.Text <> "" Then
                SQL = "Select " &
                "numUserId " &
                "from AIRBRANCH.OlapUserAccess " &
                "where numUserId = '" & txtWebUserID.Text & "' " &
                "and strAirsNumber = '0413" & mtbFacilityToAdd.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = False Then
                    SQL = "Insert into AIRBRANCH.OlapUserAccess " &
                     "(numUserId, strAirsNumber, strFacilityName) " &
                     "values " &
                     "('" & txtWebUserID.Text & "', '0413" & mtbFacilityToAdd.Text & "', " &
                     "(select strFacilityName " &
                     "from AIRBRANCH.APBFacilityInformation " &
                     "where strAIRSnumber = '0413" & mtbFacilityToAdd.Text & "')) "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    LoadUserFacilityInfo(txtWebUserEmail.Text)
                    MsgBox("The facility has been added to this user", MsgBoxStyle.Information, "Insert Success!")
                Else
                    MsgBox("The facility already exists for this user." & vbCrLf & "NO DATA SAVED", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteFacilityUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFacilityUser.Click
        Try
            If txtWebUserID.Text <> "" And cboFacilityToDelete.Text <> "" Then
                SQL = "DELETE AIRBRANCH.OlapUserAccess " &
                "WHERE numUserID = '" & txtWebUserID.Text & "' " &
                "and strAirsNumber = '0413" & cboFacilityToDelete.SelectedValue & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

                LoadUserFacilityInfo(txtWebUserEmail.Text)
                MsgBox("The facility has been removed for this user", MsgBoxStyle.Information, "Facility Removed!")

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateUser.Click
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

                SQL = "UPDATE AIRBRANCH.OlapUserAccess " &
                "SET intadminaccess = '" & adminaccess & "', " &
                "intFeeAccess = '" & feeaccess & "', " &
                "intEIAccess = '" & eiaccess & "', " &
                "intESAccess = '" & esaccess & "' " &
                "WHERE numUserID = '" & txtWebUserID.Text & "' " &
                "and strAirsNumber = '0413" & dgvUserFacilities(0, i).Value & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            Next

            LoadUserFacilityInfo(txtWebUserEmail.Text)
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


#Region "Emission Inventory Log"
    Sub LoadEILog()
        Try


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub






#End Region

    Private Sub btnReloadFSData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReloadFSData.Click
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

            SQL = "select  " &
            "strFacilitySiteName, STRFACILITYSITESTATUSCODE " &
            "from AIRBRANCH.EIS_FacilitySite " &
            "where FacilitySiteId = '" & txtEILogSelectedAIRSNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
            End While
            dr.Close()

            SQL = "select  " &
            "* " &
            "from AIRBRANCH.EIS_FacilitySiteAddress " &
            "where FacilitySiteId = '" & txtEILogSelectedAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
            End While
            dr.Close()

            SQL = "select  " &
            "* " &
            "from AIRBRANCH.EIS_FacilityGeoCoord " &
            "where FacilitySiteId = '" & txtEILogSelectedAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
            End While
            dr.Close()


            SQL = "SELECT fi.STRFACILITYNAME, fi.STRFACILITYSTREET1, " &
                "  fi.STRFACILITYCITY, fi.STRFACILITYSTATE, " &
                "  fi.STRFACILITYZIPCODE, fi.NUMFACILITYLONGITUDE, " &
                "  fi.NUMFACILITYLATITUDE, hd.STROPERATIONALSTATUS " &
                "FROM AIRBRANCH.APBFACILITYINFORMATION fi " &
                "INNER JOIN AIRBRANCH.APBHEADERDATA hd ON fi.STRAIRSNUMBER = " &
                "  hd.STRAIRSNUMBER " &
                "WHERE fi.STRAIRSNUMBER = '0413" & txtEILogSelectedAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
            End While
            dr.Close()

            SQL = "Select * " &
            "from AIRBRANCH.EIS_Mailout " &
            "where intInventoryYear = '" & txtEILogSelectedYear.Text & "' " &
            "and FacilitySiteID = '" & txtEILogSelectedAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
            End While
            dr.Close()

            SQL = "select " &
            "strContactFirstName, strContactLastName, " &
            "strContactPrefix, strContactSuffix, " &
            "strContactTitle, strContactPhoneNumber1, " &
            "strContactPhoneNumber2, strContactFaxNumber, " &
            "strContactEmail, strContactCompanyName, " &
            "strContactAddress1, strContactAddress2, " &
            "strContactCity, strContactState, " &
            "strContactZipCode, strContactDescription, " &
            "datModifingDate, (strLastName||', '||strFirstName) as ModifingPerson " &
            "from AIRBRANCH.APBContactInformation, AIRBRANCH.EPDUserProfiles " &
            "where AIRBRANCH.APBContactInformation.strModifingPerson = " &
            "AIRBRANCH.EPDUserProfiles.numUserID  " &
            "and strContactKey = '0413" & txtEILogSelectedAIRSNumber.Text & "41' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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

            End While
            dr.Close()

            LoadQASpecificData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub LoadAdminData()
        Try
            dtpDeadlineEIS.Checked = False
            txtEISDeadlineComment.Clear()
            txtAllEISDeadlineComment.Clear()

            SQL = "Select * " &
           "From AIRBRANCH.EIS_Admin " &
           "where inventoryYear = '" & txtEILogSelectedYear.Text & "' " &
           "and FacilitySiteID = '" & txtEILogSelectedAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("EISStatusCode")) Then
                    cboEILogStatusCode.Text = ""
                Else
                    cboEILogStatusCode.SelectedValue = dr.Item("EISStatusCode")
                    txtEILogStatusCode.Text = cboEILogStatusCode.Text
                End If
                If IsDBNull(dr.Item("datEISStatus")) Then
                    dtpEILogStatusDateSubmit.Text = OracleDate
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

                'Not displayed
                'If IsDBNull(dr.Item("datInitialFinalize")) Then

                'Else

                'End If
                'If IsDBNull(dr.Item("datFinalize")) Then

                'Else

                'End If
                'If IsDBNull(dr.Item("strConfirmationNumber")) Then

                'Else

                'End If
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
                    dtpEILogDateEnrolled.Text = OracleDate
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
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadQASpecificData()
        Try
            dtpQAStarted.Text = OracleDate
            dtpQAPassed.Text = OracleDate
            dtpQAPassed.Checked = False
            cboEISQAStatus.Text = ""
            cboEISQAStaff.Text = ""
            dtpQAStatus.Text = OracleDate
            dtpQACompleted.Text = OracleDate
            dtpQACompleted.Checked = False
            txtQAComments.Clear()
            txtFITrackingNumber.Text = ""
            txtAllFITrackingNumbers.Clear()
            txtPointTrackingNumber.Text = ""
            txtAllPointTrackingNumbers.Clear()
            chbFIErrors.Checked = False
            chbPointErrors.Checked = False
            dtpEISDeadline.Text = OracleDate
            dtpEISDeadline.Checked = False
            txtEISDeadlineComment.Clear()
            txtAllEISDeadlineComment.Clear()

            SQL = "Select * " &
            "from AIRBRANCH.EIS_QAAdmin " &
            "where inventoryYear = '" & cboEILogYear.Text & "' " &
            "and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("datDateQAStart")) Then
                    dtpQAStarted.Text = OracleDate
                Else
                    dtpQAStarted.Text = dr.Item("datDateQAStart")
                End If
                If IsDBNull(dr.Item("datDateQAPass")) Then
                    dtpQAPassed.Text = OracleDate
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
                    dtpQAStatus.Text = OracleDate
                Else
                    dtpQAStatus.Text = dr.Item("datQAStatus")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    cboEISQAStaff.Text = ""
                Else
                    cboEISQAStaff.Text = dr.Item("strDMUResponsibleStaff")
                End If
                If IsDBNull(dr.Item("datQAComplete")) Then
                    dtpQACompleted.Text = OracleDate
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
            End While
            dr.Close()

            If cboEILogStatusCode.SelectedValue >= 4 Then
                pnlQAProcess.Enabled = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnViewEISStats_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewEISStats.Click
        Try
            ViewEISStats()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub ViewEISStats()
        Try

            txtSelectedEISStatYear.Text = cboEISStatisticsYear.Text

            If txtSelectedEISStatYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "select * from " &
             "(select count(*) as EISUniverse " &
             "from AIRbranch.EIS_Admin " &
             "where active = '1' " &
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "'), " &
             "(select count(*) as EISMailout " &
             "from AIRbranch.EIS_Admin " &
             "where active = '1' " &
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "'  " &
             "and strMailout = '1' ), " &
             "(select count(*) as EISEnrollment " &
             "from AIRbranch.EIS_Admin " &
             "where active = '1' " &
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "'  " &
             "and strEnrollment = '1' ),  " &
             "(select count(*) as EISUNEnrollment " &
             "from AIRbranch.EIS_Admin " &
             "where active = '1' " &
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "'  " &
             "and strMailout = '1' " &
             "and (strEnrollment = '0')),   " &
             "(select count(*) as EISNoActivity " &
             "from AIRbranch.EIS_Admin " &
             "where active = '1' " &
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "' " &
             "and strOptOut is null and strEnrollment = '1'), " &
             "(select count(*) as EISOptsIn " &
             "from AIRbranch.EIS_Admin " &
             "where active = '1' " &
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "' " &
             "and strOptOut = '0' and strEnrollment = '1'), " &
             "(select count(*) as EISOptsOut " &
             "from AIRbranch.EIS_Admin " &
             "where active = '1' " &
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "'  " &
             "and strMailout = '1' " &
             "and strEnrollment = '1' " &
             "and (strOptOut = '1') and strEnrollment = '1' ), " &
             "(select count(*) as EISSubmittal  " &
             "from AIRbranch.EIS_Admin " &
             "where active = '1' " &
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "'  " &
             "and strEnrollment = '1' " &
             "and eisstatuscode >= '3' " &
             "and (strOptOut = '0' )), " &
             "(select count(*) as EISInProgress " &
             "from AIRBranch.EIS_Admin " &
             "where active = '1' " &
             "and inventoryYear = '" & cboEISStatisticsYear.Text & "' " &
             "and strEnrollment = '1' " &
             "and eisStatuscode = '2' and strEnrollment = '1' " &
             "and (strOptOut = '0')), " &
             "(select count(*) as EISQABegan   " &
             "from AIRbranch.EIS_Admin " &
             "where active = '1' " &
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "'  " &
             "and strMailout = '1' " &
             "and strEnrollment = '1' " &
             "and EISAccesscode = '2'  " &
             "and eisstatuscode = '4' " &
             "and (strOptOut = '0' )), " &
             "(select count(*) as EISEPASubmitted   " &
             "from AIRbranch.EIS_Admin " &
             "where active = '1' " &
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "'  " &
             "and strMailout = '1' " &
             "and strEnrollment = '1' " &
             "and EISAccesscode = '0'  " &
             "and eisstatuscode = '5' " &
             "and (strOptOut = '0' )), " &
             "(select count(*) as EISFinalized " &
             "from AIRbranch.EIS_Admin " &
             "where active = '1' " &
             "and inventoryYear = '" & cboEISStatisticsYear.Text & "' " &
             "and strEnrollment = '1' " &
             "and (EISStatusCode = '3' OR EISStatusCode = '4' OR EISStatusCode = '5')), " &
     "( select count(*) as QASubmittedToDo " &
     "from AIRbranch.EIS_Admin  " &
     "where active = '1'  " &
     "and inventoryyear = '" & cboEISStatisticsYear.Text & "'   " &
     "and strEnrollment = '1'  " &
     "and eisstatuscode >= 3 " &
     "and (strOptOut = '0' ) " &
     "and  NOT  exists (Select * from AIRBranch.EIS_QAAdmin " &
     "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " &
     "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID) ), " &
     "( select count(*) as QAOptOutToDo " &
     "from AIRbranch.EIS_Admin  " &
     "where active = '1'  " &
     "and inventoryyear = '" & cboEISStatisticsYear.Text & "'   " &
     "and strEnrollment = '1'  " &
     "and (eisstatuscode = 3 or eisstatuscode = 4) " &
     "and (strOptOut = '1' or strOptout is null ) " &
     "and  NOT  exists (Select * from AIRBranch.EIS_QAAdmin " &
     "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " &
     "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID) ), " &
     "( select count(*) as QASubmittedBegan   " &
     "from AIRbranch.EIS_Admin  " &
     "where active = '1'  " &
     "and inventoryyear = '" & cboEISStatisticsYear.Text & "'   " &
     "and strEnrollment = '1'  " &
     "and eisstatuscode >= 3   " &
     "and (strOptOut = '0' ) " &
     "and    exists (Select * from AIRBranch.EIS_QAAdmin " &
     "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " &
     "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " &
     "and datQAComplete is null ) ), " &
     "( select count(*) as QAOptOutBegan   " &
     "from AIRbranch.EIS_Admin  " &
     "where active = '1'  " &
     "and inventoryyear = '" & cboEISStatisticsYear.Text & "'   " &
     "and strEnrollment = '1'  " &
     "and (eisstatuscode = '3' or eisstatuscode = '4')   " &
     "and (strOptOut = '1' or strOptout is null) " &
     "and  (not  exists (Select * from AIRBranch.EIS_QAAdmin " &
     "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " &
     "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " &
     "and datQAComplete is null )   " &
     "or  exists (Select * from AIRBranch.EIS_QAAdmin " &
     "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " &
     "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " &
     "and datQAComplete is null ))), " &
     "( select count(*) as QASubmittedToEPA  " &
     "from AIRbranch.EIS_Admin  " &
     "where active = '1'  " &
     "and inventoryyear = '" & cboEISStatisticsYear.Text & "'   " &
     "and strEnrollment = '1'  " &
     "and eisstatuscode >= '3' " &
     "and (strOptOut = '0' ) " &
     "and    exists (Select * from AIRBranch.EIS_QAAdmin " &
     "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " &
     "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " &
      "and datQAComplete is not null ) ),  " &
     "( select count(*) as QAOptOutToEPA  " &
     "from AIRbranch.EIS_Admin  " &
     "where active = '1'  " &
     "and inventoryyear = '" & cboEISStatisticsYear.Text & "'   " &
     "and strEnrollment = '1'  " &
     "and eisstatuscode = '5'  " &
     "and (strOptOut = '1' or strOptout is null ) " &
     "and  (not  exists (Select * from AIRBranch.EIS_QAAdmin " &
     "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " &
     "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID) " &
     "OR " &
     "exists (Select * from AIRBranch.EIS_QAAdmin " &
     "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " &
     "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " &
      "and datQAComplete is not null )" &
      " ) ), " &
      "(select count(*) as FIPassed " &
      "from airbranch.EIS_Admin, AIRBranch.EIS_QAAdmin " &
      "where EIS_Admin.InventoryYear = EIS_QAAdmin.inventoryYEar " &
      "and EIS_Admin.facilitysiteID = EIS_QAAdmin.facilitysiteID " &
      "and eis_qaAdmin.qaStatusCode = '2' " &
      "and eis_admin.inventoryyear = '" & cboEISStatisticsYear.Text & "' ) "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
            End While
            dr.Close()
            txtSelectedEISMailout.Text = cboEISStatisticsYear.Text
            txtEISStatsEnrollmentYear.Text = cboEISStatisticsYear.Text

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISEIUniverse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISEIUniverse.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If
            EIS_VIEW(txtSelectedEISStatYear.Text, "", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Active EIS Universe Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISMailOutTotal_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISMailOutTotal.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If
            EIS_VIEW(txtSelectedEISStatYear.Text, "1", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Mailout Total Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISEnrolled_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISEnrolled.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Enrolled Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISNoActivity_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISNoActivity.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If
            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "Null", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "No Activity Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISUnenrolled_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISUnenrolled.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If
            EIS_VIEW(txtSelectedEISStatYear.Text, "", "0", "1", "", "", "", "")
            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Unenrolled Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISInProgress_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISInProgress.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", " and EISStatusCode = 2 ", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "In Progress Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISOptedIn_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISOptedIn.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Opted-In Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISOptedOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISOptedOut.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "1", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Opted-Out Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISSubmitted_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISSubmitted.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", " and EISStatusCode >= 3 ", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "In Progress Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISFinalized_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISFinalized.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "",
                     " and (EISStatusCode = '3' or EISStatusCode = '4' or EISStatusCode = '5') ", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Finalized Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISQABegan_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISQABegan.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "1", "1", "1", " and (strOptOut is null or strOptout = '0') ", "4", "2", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "In Progress Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISSubmittedToEPA_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISSubmittedToEPA.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
                 " and EISStatusCode >= 3 ", "", " and datQAComplete is not null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, EPA Submitted Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvEISStats_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvEISStats.MouseUp
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


                    SQL = "Select " &
                    "strFacilityName, " &
                    "strContactCompanyName, strContactAddress1, " &
                    "strContactAddress2, strContactCity, " &
                    "strcontactstate, strcontactzipCode, " &
                    "strcontactFirstName, strcontactLastName, " &
                    "strContactPrefix, strContactEmail, " &
                    "stroperationalStatus, strClass, " &
                    "strcomment, UpdateUser, " &
                    "updateDateTime, CreateDateTime " &
                     "from AIRBranch.EIS_Mailout " &
                     "where intInventoryyear = '" & txtSelectedEISMailout.Text & "' " &
                     "and FacilitySiteID = '" & txtEISStatsMailoutAIRSNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    While dr.Read
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

                    End While
                    dr.Close()

                End If
            End If
            dgvEISStats.Enabled = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveEISStatMailout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveEISStatMailout.Click
        Try
            If txtSelectedEISMailout.Text <> "" And txtEISStatsMailoutAIRSNumber.Text <> "" Then
                SQL = "UPdate AIRBRANCH.EIS_Mailout set " &
                "strFacilityName = '" & txtEISStatsMailoutFacilityName.Text & "', " &
                "strContactCompanyName = '" & txtEISStatsMailoutCompanyName.Text & "', " &
                "strContactAddress1 = '" & txtEISStatsMailoutAddress1.Text & "', " &
                "strContactAddress2 = '" & txtEISStatsMailoutAddress2.Text & "', " &
                "strContactCity = '" & txtEISStatsMailoutCity.Text & "', " &
                "strContactState = '" & txtEISStatsMailoutState.Text & "', " &
                "strContactZipCode = '" & txtEISStatsMailoutZipCode.Text & "', " &
                "strContactFirstName = '" & txtEISStatsMailoutFirstName.Text & "', " &
                "strContactLastName = '" & txtEISStatsMailoutLastName.Text & "', " &
                "strContactPrefix = '" & txtEISStatsMailoutPrefix.Text & "', " &
                "strContactEmail = '" & txtEISStatsMailoutEmailAddress.Text & "', " &
                "strComment = '" & txtEISStatsMailoutComments.Text & "', " &
                "updateDateTime = sysdate " &
                "where intInventoryYear = '" & txtSelectedEISStatYear.Text & "' " &
                "and FacilitySiteID = '" & txtEISStatsMailoutAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

                MsgBox("Data updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Please select a valid year from the dropdown and a valid contact from the resulting list." & vbCrLf &
                       "NO DATA UPDATED", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISStatsEnrollment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISStatsEnrollment.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to enroll Facilities into the QA process.", Me.Text)

            If EISConfirm = txtEISStatsEnrollmentYear.Text Then
                temp = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next
                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "
                    SQL = "Update AIRBRANCH.EIS_Admin set " &
                    "strEnrollment = '1', " &
                    "EISAccessCode = '1', " &
                    "EISStatusCode = '1', " &
                    "DatEISStatus = sysdate " &
                    "where inventoryyear = '" & EISConfirm & "' " &
                    "and strEnrollment = '0' " &
                    "and strOptOut is null " &
                    "and EISAccessCode = '0' " &
                    "and EISStatusCode = '0' " &
                    "and strMailout = '1' " &
                    temp

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteReader()
                End If

                MsgBox("Facilities enrolled in " & EISConfirm & " EIS.", MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("Year does not match selected EIS year")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsViewEnrollment_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsViewEnrollment.LinkClicked
        Try
            txtEISStatsEnrollmentYear.Text = cboEISStatisticsYear.Text

            If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim dgvRow As New DataGridViewRow

            SQL = "select " &
            "'False' as ID, " &
            " AIRBRANCH.EIS_Admin.facilitysiteid, " &
           "AIRBRANCH.APBFacilityInformation.strFacilityname, " &
           "AIRBRANCH.EIS_Admin.inventoryyear, " &
           "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " &
           "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " &
           "case " &
           "when strOptOut = '1' then 'Yes' " &
           "when strOptOut = '0' then 'No' " &
           "else '-' " &
           "End strOptOut, " &
           "case " &
           "when strMailout = '1' then 'Yes' " &
           "else 'No' " &
           "end strMailout, " &
           "case " &
           "when strEnrollment = '1' then 'Yes' " &
           "when strEnrollment = '0' then 'No' " &
           "else '-' " &
           "end strEnrollment, " &
           "case " &
           "when strContactEmail is null then '-' " &
           "else strContactEmail " &
           "end ContactEmail, " &
           "case " &
           "When strContactPrefix is null then '-' " &
           "else strContactPrefix " &
           "end strContactPrefix, " &
           "case " &
           "when strContactFirstName is null then '-' " &
           "else strContactFirstName " &
           "end strContactFirstName, " &
           "case " &
           "When strContactLastName is null then '-' " &
           "else strContactLastName " &
           "end strContactLastName, " &
           "case " &
           "when strDMUResponsibleStaff is null then '-' " &
           "else strDMUResponsibleStaff " &
           "end strDMUResponsibleStaff " &
           "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " &
           "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " &
           "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " &
           "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " &
           "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " &
           "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " &
           "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " &
           "and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " &
           "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " &
           "and AIRBranch.EIS_Admin.Active = '1' " &
           "and AIRbranch.EIS_Admin.inventoryyear = '" & txtEISStatsEnrollmentYear.Text & "'" &
           "and strEnrollment = '1' "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
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
                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If

                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strEnrollment")) Then
                    dgvRow.Cells(13).Value = ""
                Else
                    dgvRow.Cells(13).Value = dr.Item("strEnrollment")
                End If


                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISStatsRemoveEnrollment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISStatsRemoveEnrollment.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to enroll Facilities into the QA process.", Me.Text)

            If EISConfirm = txtEISStatsEnrollmentYear.Text Then
                temp = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next
                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "
                    SQL = "Update AIRBRANCH.EIS_Admin set " &
                    "strEnrollment = '0', " &
                    "EISAccessCode = '1', " &
                    "EISStatusCode = '1', " &
                    "DatEISStatus = sysdate " &
                    "where inventoryyear = '" & EISConfirm & "' " &
                    "and strEnrollment = '1' " &
                    "and strOptOut is null " &
                    "and EISAccessCode = '0' " &
                    "and EISStatusCode = '0' " &
                    "and strMailout = '1' " &
                    temp

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteReader()
                End If

                MsgBox("Facilities enrolled in " & EISConfirm & " EIS.", MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("Year does not match selected EIS year")
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCloseOutEIS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseOutEIS.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to close out.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                temp = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                Next

                If temp = "" Then
                    MsgBox("No facilities are selected. No data changed.")
                    Exit Sub
                End If

                temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "

                SQL = "Update AIRBranch.EIS_Admin set " &
                "EISAccessCode = '2' " &
                "where inventoryYear = '" & EISConfirm & "' " &
                temp

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()
                ViewEISStats()
                MsgBox(EISConfirm & " Emission Inventory Year Closed out.", MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("Year does not match selected EIS year")

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISBeginQA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISBeginQA.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to move Facilities into the QA process.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                temp = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True And dgvEISStats(6, i).Value = "No" Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next
                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "

                    SQL = "Update AIRBranch.EIS_Admin set " &
                    "EISAccessCode = '2', " &
                    "EISStatusCode = '4', " &
                    "datEISstatus = sysdate, " &
                    "UpdateUser = '" & Replace(CurrentUser.AlphaName, "'", "''") & "', " &
                    "updatedatetime = sysdate " &
                    "where strOptOut = '0' " &
                    "and inventoryYear = '" & EISConfirm & "' " &
                    temp

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteReader(CommandBehavior.CloseConnection)
                End If

                temp = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    'temp = dgvEISStats(6, i).Value

                    If dgvEISStats(0, i).Value = True And dgvEISStats(6, i).Value = "Yes" Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next
                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "

                    SQL = "Update AIRBranch.EIS_Admin set " &
                    "EISAccessCode = '2', " &
                    "EISStatusCode = '5', " &
                    "datEISstatus = sysdate, " &
                    "UpdateUser = '" & Replace(CurrentUser.AlphaName, "'", "''") & "', " &
                    "updatedatetime = sysdate " &
                    "where strOptOut = '1' " &
                    "and inventoryYear = '" & EISConfirm & "' " &
                    temp

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteReader(CommandBehavior.CloseConnection)
                End If

                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True Then
                        SQL = "insert into AIRBranch.EIS_QAAdmin " &
                        "(select " &
                        "'" & EISConfirm & "', '" & dgvEISStats(1, i).Value & "', " &
                        "sysdate, '', " &
                        "'1', sysdate, " &
                        "'" & CurrentUser.AlphaName & "', " &
                        "'', '', " &
                        "'1', '" & CurrentUser.AlphaName & "', " &
                        "sysdate, sysdate, " &
                        "'', '', '', '' " &
                        "from dual " &
                        "where not exists (select * from AIRBranch.EIS_QAAdmin " &
                        "where inventoryYear = '" & EISConfirm & "' " &
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "') " &
                        "and exists (select * from AIRBranch.EIS_Admin " &
                        "where inventoryYear = '" & EISConfirm & "'  " &
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' " &
                        "and strOptOut = '0' )) "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd = New OracleCommand("AIRBranch.PD_EIS_QASTART", CurrentConnection)
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.Add(New OracleParameter("AIRSNUMBER_IN", OracleDbType.Varchar2)).Value = dgvEISStats(1, i).Value
                        cmd.Parameters.Add(New OracleParameter("INTYEAR_IN", OracleDbType.Decimal)).Value = EISConfirm

                        cmd.ExecuteNonQuery()

                    End If
                Next
                ViewEISStats()
                MsgBox(EISConfirm & " QA process began.", MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("Year does not match selected EIS year")

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnEILogUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEILogUpdate.Click
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
            EISStatus = cboEILogStatusCode.SelectedValue
            EISAccess = cboEILogAccessCode.SelectedValue
            If rdbEILogActiveYes.Checked = True Then
                ActiveStatus = "1"
            Else
                ActiveStatus = "0"
            End If

            SQL = "Select FacilitySiteID from airbranch.EIS_Admin " &
            "where inventoryyear = '" & cboEILogYear.Text & "' " &
            "and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = False Then
                MsgBox("The facility is not currently in the EIS universe for the selected year." & vbCrLf &
                       "Use the Add New Facility to Year." & vbCrLf & vbCrLf & "NO DATA SAVED", MsgBoxStyle.Information, Me.Text)

                Exit Sub
            End If

            SQL = "Update AIRBranch.EIS_Admin set " &
            "EISStatusCode = '" & EISStatus & "', " &
            "DatEISStatus = '" & dtpEILogStatusDateSubmit.Text & "', " &
            "EISAccessCode = '" & EISAccess & "', " &
            "strOptOut = '" & OptOut & "', " &
            "strIncorrectOptOut = '" & IncorrectlyOptedOut & "', " &
            "strMailout = '" & Mailout & "', " &
            "strEnrollment = '" & Enrollment & "', " &
            "datEnrollment = '" & dtpEILogDateEnrolled.Text & "', " &
            "strComment = '" & Replace(txtEILogComments.Text, "'", "''") & "', " &
            "active = '" & ActiveStatus & "', " &
            "updateUser = '" & Replace(CurrentUser.AlphaName, "'", "''") & "', " &
            "updateDateTime = sysdate " &
            "where inventoryyear = '" & cboEILogYear.Text & "' " &
            "and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteReader()

            If dtpDeadlineEIS.Checked = True Then
                Dim DeadLineComments As String = ""
                If txtAllEISDeadlineComment.Text.Contains(dtpDeadlineEIS.Text & "(deadline)- " & CurrentUser.AlphaName & " - " & OracleDate & vbCrLf &
                txtEISDeadlineComment.Text) Then
                Else
                    DeadLineComments = dtpDeadlineEIS.Text & "(deadline)- " & CurrentUser.AlphaName & " - " & OracleDate & vbCrLf &
                    txtEISDeadlineComment.Text &
                    vbCrLf & vbCrLf & txtAllEISDeadlineComment.Text

                    SQL = "update Airbranch.EIS_Admin set " &
                    "datEISDeadline = '" & dtpDeadlineEIS.Text & "',  " &
                    "strEISDeadlineComment = '" & Replace(DeadLineComments, "'", "''") & "' " &
                    "where INventoryyear = '" & cboEILogYear.Text & "' " &
                    "and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteReader()
                End If
            End If

            'If cboEILogStatusCode.SelectedValue = "4" Or cboEILogStatusCode.SelectedValue = "5" And rdbEILogOpOutYes.Checked = False Then
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
                QAStatusDate = OracleDate
                StaffResponsible = cboEISQAStaff.Text
                If txtQAComments.Text = "" Then
                    If txtAllQAComments.Text = "" Then
                        QAComments = ""
                    Else
                        QAComments = txtAllQAComments.Text
                    End If
                Else
                    If txtAllQAComments.Text = "" Then
                        QAComments = CurrentUser.AlphaName & " - " & OracleDate & vbCrLf & txtQAComments.Text
                    Else
                        QAComments = CurrentUser.AlphaName & " - " & OracleDate & vbCrLf & txtQAComments.Text & vbCrLf & vbCrLf &
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
                        FITracking = CurrentUser.AlphaName & " - " & OracleDate & vbCrLf & txtFITrackingNumber.Text
                    Else
                        FITracking = CurrentUser.AlphaName & " - " & OracleDate & vbCrLf & txtFITrackingNumber.Text & vbCrLf & vbCrLf &
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
                        pointTracking = CurrentUser.AlphaName & " - " & OracleDate & vbCrLf & txtPointTrackingNumber.Text
                    Else
                        pointTracking = CurrentUser.AlphaName & " - " & OracleDate & vbCrLf & txtPointTrackingNumber.Text & vbCrLf & vbCrLf &
                                txtAllPointTrackingNumbers.Text
                    End If
                End If
                If chbPointErrors.Checked = True Then
                    pointError = "True"
                Else
                    pointError = "False"
                End If

                SQL = "Update AIRBRANCH.eis_QAAdmin set " &
               "datDateQAStart = '" & QAStart & "', " &
               "datDateQAPass = '" & QAPass & "', " &
               "QAStatusCode = '" & QAStatusCode & "', " &
               "datQAStatus = '" & QAStatusDate & "', " &
               "strDMUResponsibleStaff = '" & Replace(StaffResponsible, "'", "''") & "', " &
               "datQAComplete = '" & QAComplete & "', " &
               "strComment = '" & Replace(QAComments, "'", "''") & "', " &
               "active = '1', " &
               "updateuser = '" & Replace(CurrentUser.AlphaName, "'", "''") & "', " &
               "updateDateTime = sysdate, " &
               "strFITrackingnumber = '" & Replace(FITracking, "'", "''") & "', " &
               "strFIError = '" & Replace(FIError, "'", "''") & "', " &
               "STRPOINTTRACKINGNUMBER = '" & Replace(pointTracking, "'", "''") & "', " &
               "strpointerror = '" & Replace(pointError, "'", "''") & "' " &
               "where INventoryyear = '" & cboEILogYear.Text & "' " &
               "and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                LoadQASpecificData()
            End If

            LoadAdminData()
            MsgBox("Admin Data updated.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEILogAddNewFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEILogAddNewFacility.Click
        Try

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd = New OracleCommand("AIRBranch.PD_EIS_Data", CurrentConnection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("AIRSNUM", OracleDbType.Varchar2)).Value = txtEILogSelectedAIRSNumber.Text
            cmd.Parameters.Add(New OracleParameter("INTYEAR", OracleDbType.Decimal)).Value = txtEILogSelectedYear.Text

            cmd.ExecuteNonQuery()

            LoadAdminData()
            MsgBox("New Facility Added", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateQAData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateQAData.Click
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
            QAStatusDate = OracleDate
            StaffResponsible = cboEISQAStaff.Text
            If txtQAComments.Text = "" Then
                If txtAllQAComments.Text = "" Then
                    QAComments = ""
                Else
                    QAComments = txtAllQAComments.Text
                End If
            Else
                If txtAllQAComments.Text = "" Then
                    QAComments = CurrentUser.AlphaName & " - " & OracleDate & vbCrLf & txtQAComments.Text
                Else
                    QAComments = CurrentUser.AlphaName & " - " & OracleDate & vbCrLf & txtQAComments.Text & vbCrLf & vbCrLf &
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
                    FITracking = CurrentUser.AlphaName & " - " & OracleDate & vbCrLf & txtFITrackingNumber.Text
                Else
                    FITracking = CurrentUser.AlphaName & " - " & OracleDate & vbCrLf & txtFITrackingNumber.Text & vbCrLf & vbCrLf &
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
                    PointTracking = CurrentUser.AlphaName & " - " & OracleDate & vbCrLf & txtPointTrackingNumber.Text
                Else
                    PointTracking = CurrentUser.AlphaName & " - " & OracleDate & vbCrLf & txtPointTrackingNumber.Text & vbCrLf & vbCrLf &
                            txtAllPointTrackingNumbers.Text
                End If
            End If
            If chbPointErrors.Checked = True Then
                PointError = "True"
            Else
                PointError = "False"
            End If

            SQL = "Update AIRBRANCH.eis_QAAdmin set " &
            "datDateQAStart = '" & QAStart & "', " &
            "datDateQAPass = '" & QAPass & "', " &
            "QAStatusCode = '" & QAStatusCode & "', " &
            "datQAStatus = '" & QAStatusDate & "', " &
            "strDMUResponsibleStaff = '" & Replace(StaffResponsible, "'", "''") & "', " &
            "datQAComplete = '" & QAComplete & "', " &
            "strComment = '" & Replace(QAComments, "'", "''") & "', " &
            "active = '1', " &
            "updateuser = '" & Replace(CurrentUser.AlphaName, "'", "''") & "', " &
            "updateDateTime = sysdate, " &
            "strFITrackingnumber = '" & Replace(FITracking, "'", "''") & "', " &
            "strFIError = '" & Replace(FIError, "'", "''") & "', " &
            "STRPOINTTRACKINGNUMBER = '" & Replace(PointTracking, "'", "''") & "', " &
            "strpointerror = '" & Replace(PointError, "'", "''") & "' " &
            "where INventoryyear = '" & cboEILogYear.Text & "' " &
            "and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteReader()

            LoadQASpecificData()

            If dtpQACompleted.Checked = True Then
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd = New OracleCommand("AIRBranch.PD_EIS_QA_Done", CurrentConnection)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add(New OracleParameter("AIRSNUM", OracleDbType.Varchar2)).Value = txtEILogSelectedAIRSNumber.Text
                cmd.Parameters.Add(New OracleParameter("INTYEAR", OracleDbType.Decimal)).Value = txtEILogSelectedYear.Text
                cmd.Parameters.Add(New OracleParameter("DATLASTSUBMIT", OracleDbType.Date)).Value = dtpQACompleted.Text

                cmd.ExecuteNonQuery()
            End If

            MsgBox("QA data saved.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEIModifyUpdateLocation_Click(sender As Object, e As EventArgs) _
    Handles btnEIModifyUpdateLocation.Click

        If txtEILogSelectedAIRSNumber.Text = "" Then
            MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim Address As String = txtEIModifyLocation.Text
        Dim City As String = txtEIModifyCity.Text
        Dim PostalCode As String = mtbEIModifyZipCode.Text

        If Address <> "" And City <> "" Then
            Dim query As String = "Update airbranch.EIS_FacilitySiteAddress set " &
            " STRLOCATIONADDRESSTEXT = :Address, " &
            " STRLOCALITYNAME = :City, " &
            " STRLOCATIONADDRESSPOSTALCODE = :PostalCode " &
            " where facilitysiteid = :AirsNumber"

            Dim parameters As OracleParameter()

            parameters = New OracleParameter() {
                New OracleParameter("Address", Address),
                New OracleParameter("City", City),
                New OracleParameter("PostalCode", PostalCode),
                New OracleParameter("AirsNumber", txtEILogSelectedAIRSNumber.Text)
            }

            DB.RunCommand(query, parameters)

            MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)
        Else
            MsgBox("No data saved." & vbCrLf & "BOTH LOCATION ADDRESS AND CITY ARE REQUIRED" & vbCrLf & vbCrLf & "Sorry for yelling.", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub

    Private Sub btnEIModifyUpdateMailing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles btnEIModifyUpdateMailing.Click

        If txtEILogSelectedAIRSNumber.Text = "" Then
            MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim Address As String = txtEIModifyMLocation.Text
        Dim City As String = txtEIModifyMCity.Text
        Dim PostalCode As String = mtbEIModifyMZipCode.Text

        If Address <> "" And City <> "" Then
            Dim query As String = "Update airbranch.EIS_FacilitySiteAddress set " &
            " strMailingAddressText = :Address, " &
            " strMailingAddresscityname = :City, " &
            " strMailingAddressPostalCode = :PostalCode " &
            " where facilitysiteid = :AirsNumber"

            Dim parameters As OracleParameter()

            parameters = New OracleParameter() {
                New OracleParameter("Address", Address),
                New OracleParameter("City", City),
                New OracleParameter("PostalCode", PostalCode),
                New OracleParameter("AirsNumber", txtEILogSelectedAIRSNumber.Text)
            }

            DB.RunCommand(query, parameters)

            MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)
        Else
            MsgBox("No data saved." & vbCrLf & "BOTH MAILING ADDRESS AND CITY ARE REQUIRED" & vbCrLf & vbCrLf & "Sorry for yelling.", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub

    Private Sub btnEIModifyUpdateName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles btnEIModifyUpdateName.Click
        If txtEILogSelectedAIRSNumber.Text = "" Then
            MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If txtEIModifyFacilityName.Text <> "" Then
            txtEIModifyFacilityName.Text = Facility.SanitizeFacilityNameForDb(txtEIModifyFacilityName.Text)
        End If

        Dim FacilityName As String = txtEIModifyFacilityName.Text

        If FacilityName <> "" Then
            Dim query As String = "Update airbranch.EIS_FacilitySite set " &
            " strFacilitySiteName = :FacilityName " &
            " where facilitysiteid = :AirsNumber"

            Dim parameters As OracleParameter()

            parameters = New OracleParameter() {
                New OracleParameter("FacilityName", FacilityName),
                New OracleParameter("AirsNumber", txtEILogSelectedAIRSNumber.Text)
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
                SQL = "Update airbranch.EIS_FacilityGEOCoord set " &
                "numLatitudeMeasure = '" & mtbEIModifyLatitude.Text & "', " &
                "numLongitudeMeasure = '-" & mtbEIModifyLongitude.Text & "' " &
                "where facilitySiteID = '" & txtEILogSelectedAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "Update AIRBranch.APBFacilityInformation set " &
                "numFacilityLongitude = '-" & mtbEIModifyLongitude.Text & "', " &
                "numFacilityLatitude = '" & mtbEIModifyLatitude.Text & "', " &
                "strComments = 'Updated by " & CurrentUser.AlphaName & " through DMU Staff Tools - Emissions Inventory Log. ', " &
                "strModifingPerson = '" & CurrentUser.UserID & "', " &
                "datModifingDate = sysdate " &
                "where strAIRSNumber = '0413" & txtEILogSelectedAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Latitude & Longitude data not saved." & vbCrLf & "Add both values to update.",
                         MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateLatLong_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateLatLong.Click
        UpdateFacilityGEOCoord()
    End Sub

    Private Sub btnUpdateEisOperStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateEisOperStatus.Click
        If txtEILogSelectedAIRSNumber.Text = "" Then
            MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
        Else
            Dim query As String = "UPDATE AIRBRANCH.EIS_FACILITYSITE " &
                " SET STRFACILITYSITESTATUSCODE = :statuscode " &
                " , STRFACILITYSITECOMMENT = :sitecomment " &
                " , UPDATEUSER = :updateuser " &
                " , UPDATEDATETIME = sysdate " &
                " WHERE FACILITYSITEID = :siteid "

            Dim parameters As OracleParameter() = New OracleParameter() {
                New OracleParameter("statuscode", cbEisModifyOperStatus.SelectedValue.ToString),
                New OracleParameter("sitecomment", "Site status updated from IAIP"),
                New OracleParameter("updateuser", CurrentUser.UserID & "-" & CurrentUser.AlphaName),
                New OracleParameter("siteid", txtEILogSelectedAIRSNumber.Text)
            }

            If DB.RunCommand(query, parameters) Then
                MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("There was an error updating the data.", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub

    Private Sub btnEIModifyCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEIModifyCopy.Click
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

    Private Sub btnEISMailoutUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISMailoutUpdate.Click
        Try

            If txtEILogSelectedAIRSNumber.Text = "" Then
                MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Update airbranch.EIS_Mailout Set " &
            "strFacilityName= '" & Replace(txtEISMailoutEditFacilityName.Text, "'", "''") & "', " &
            "strContactCompanyName = '" & Replace(txtEISMailoutEditCompanyName.Text, "'", "''") & "', " &
            "strContactAddress1 = '" & Replace(txtEISMailoutEditAdress.Text, "'", "''") & "', " &
            "strContactAddress2 = '" & Replace(txtEISMailoutEditAddress2.Text, "'", "''") & "', " &
            "strContactCity = '" & Replace(txtEISMailoutEditCity.Text, "'", "''") & "', " &
            "strContactState = '" & Replace(txtEISMailoutEditState.Text, "'", "''") & "', " &
            "strContactZipCode = '" & Replace(txtEISMailoutEditZipCode.Text, "'", "''") & "', " &
            "strContactFirstName = '" & Replace(txtEISMailoutEditFirstName.Text, "'", "''") & "', " &
            "strContactLastName = '" & Replace(txtEISMailoutEditLastName.Text, "'", "''") & "', " &
            "strContactPrefix = '" & Replace(txtEISMailoutEditPrefix.Text, "'", "''") & "', " &
            "strContactEmail = '" & Replace(txtEISMailoutEditEmailAddress.Text, "'", "''") & "', " &
            "strComment = '" & Replace(txtEISMailoutEditComments.Text, "'", "''") & "' " &
            "where FacilitySiteid = '" & txtEILogSelectedAIRSNumber.Text & "' " &
            "and intInventoryYear = '" & txtEILogSelectedYear.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteReader()

            MsgBox("Data updated", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedToDo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedToDo.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", " and EISStatusCode >= 3 ", "", " and QAStatusCode is null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, To-Do Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedBegan_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBegan.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
                      " and EISStatusCode >= 3 ", "", " and QAStatusCode is not null and datQAComplete is null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedBeganwFIErrors_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwFIErrors.LinkClicked
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedBeganwithEIErrors_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwithEIErrors.LinkClicked
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISStatsSubmittedBeganwithBothErrors_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwithBothErrors.LinkClicked
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISStatsSubmittedBeganwithoutErrors_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwithoutErrors.LinkClicked
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsOptedOutToDo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsOptedOutToDo.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", " and (strOptOut = '1' or stroptout is null )",
             " and EISStatusCode >= 3 ", "", " and QAStatusCode is null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Opted-Out, To-do Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsOptedOutBegan_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsOptedOutBegan.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", " and (strOptOut = '1' or strOptout is null) ",
                     " and EISStatusCode >= 3 ", "", " and QAStatusCode is not null and datQAComplete is null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Opted-Out, Started Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsOptedOutSubmittedToEPA_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsOptedOutSubmittedToEPA.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", " and (strOptOut = '1' or strOptout is null )  ",
               " and EISStatusCode >= 5 ", "", " and datQAComplete is not null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, EPA Submitted Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbSearchForFacility_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbSearchForFacility.LinkClicked
        Try
            If cboEISStatisticsYear.Text = "" Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Select " &
                  "strFacilityName, " &
                  "strContactCompanyName, strContactAddress1, " &
                  "strContactAddress2, strContactCity, " &
                  "strcontactstate, strcontactzipCode, " &
                  "strcontactFirstName, strcontactLastName, " &
                  "strContactPrefix, strContactEmail, " &
                  "stroperationalStatus, strClass, " &
                  "strcomment, UpdateUser, " &
                  "updateDateTime, CreateDateTime " &
                   "from AIRBranch.EIS_Mailout " &
                   "where intInventoryyear = '" & cboEISStatisticsYear.Text & "' " &
                   "and FacilitySiteID = '" & txtEISStatsMailoutAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
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

            End While
            dr.Close()

            If txtEISStatsMailoutFacilityName.Text = "" Then
                SQL = "Select * from " &
                "(Select dt_EIcontact.STRairsnumber, AIRBRANCH.APBFacilityinformation.STRFACILITYNAME, " &
                "AIRBRANCH.APBHEADERDATA.stroperationalstatus, AIRBRANCH.APBHEADERDATA.STRCLASS, " &
                "(Case " &
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRContactLastName " &
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRContactLastName " &
                "Else '' " &
                "END) STRContactLastName, " &
                "(Case " &
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRContactfirstName " &
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRContactfirstName " &
                "Else '' " &
                "END) STRContactfirstName, " &
                "(Case " &
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRContactCompanyName " &
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRContactCompanyName " &
                "END) STRContactCompanyName, " &
                "(Case " &
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRContactEmail " &
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRContactEmail " &
                "END) STRContactEmail, " &
                "(Case " &
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRCONTACTPREFIX " &
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTPREFIX " &
                "END) strCONTACTPREFIX, " &
                "(Case " &
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRCONTACTADDRESS1 " &
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTADDRESS1 " &
                "END) STRCONTACTADDRESS1, " &
                "(Case " &
                "When dt_EIContact.STRKEY='41' THEN dt_EIContact.STRCONTACTCITY " &
                "When dt_EIContact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTCITY " &
                "END) STRCONTACTCITY, " &
                "(Case " &
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRCONTACTSTATE " &
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTSTATE " &
                "END) STRCONTACTSTATE,  " &
                "(Case " &
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRCONTACTZIPCODE " &
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTZIPCODE " &
                "END) STRCONTACTZIPCODE " &
                "From " &
                "(Select DISTINCT dt_eIlist.STRAIRSNUMBER, dt_contact.STRKEY,  " &
                "dt_Contact.STRCONTACTLASTNAME, dt_Contact.STRCONTACTFIRSTNAME, " &
                "dt_Contact.STRContactCompanyName, dt_Contact.STRContactEmail,  " &
                "dt_Contact.STRCONTACTPREFIX, dt_Contact.STRCONTACTADDRESS1, dt_Contact.STRCONTACTCITY,  " &
                "dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE " &
                "FROM " &
                "(Select * FROM AIRBRANCH.APBHEADERDATA " &
                "where (stroperationalstatus = 'O' OR stroperationalstatus = 'P' oR stroperationalstatus = 'C') AND  " &
                "(STRCLASS = 'A')   " &
                ") dt_EIList,      " &
                "(Select * From AIRBRANCH.APBCONTACTINFORMATION where STRKEY=41) dt_Contact " &
                "Where dt_EIList.STRAIRSNUMBEr = dt_Contact.STRAIRSNUMBER (+)) dt_EIContact, " &
                "(Select DISTINCT dt_eIlist.STRAIRSNUMBER, dt_contact.STRKEY,  " &
                "dt_Contact.STRCONTACTLASTNAME, dt_Contact.STRCONTACTFIRSTNAME, " &
                "dt_Contact.STRContactCompanyName, dt_Contact.STRContactEmail, dt_Contact.STRCONTACTPREFIX,  " &
                "dt_Contact.STRCONTACTADDRESS1, dt_Contact.strcontactcity, dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE " &
                "FROM " &
                "(Select * FROM AIRBRANCH.APBHEADERDATA " &
                "where (stroperationalstatus = 'O' OR stroperationalstatus = 'P' oR stroperationalstatus = 'C') AND  " &
                "(STRCLASS = 'A')   " &
                ") dt_EIList,      " &
                "(Select * From AIRBRANCH.APBCONTACTINFORMATION where STRKEY=30) dt_Contact " &
                "Where dt_EIList.STRAIRSNUMBEr = dt_Contact.STRAIRSNUMBER (+)) dt_PermitContact, " &
                "AIRBRANCH.APBFACILITYINFORMATION, " &
                "AIRBRANCH.APBHEADERDATA " &
                "Where AIRBRANCH.APBFACILITYINFORMATION.STRAIRSNUMBER= dt_EIContact.STRAIRSNumber and  " &
                "AIRBRANCH.APBHEADERDATA.STRAIRSNUMBER= dt_EIContact.STRAIRSNumber and  " &
                "dt_EIContact.STRAIRSNumber  = dt_PermitContact.STRAIRSNUMBER (+) ) " &
                "where strAIRSnumber = '0413" & txtEISStatsMailoutAIRSNumber.Text & "' "


                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                While dr.Read
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
                    'If IsDBNull(dr.Item("strContactAddress2")) Then
                    txtEISStatsMailoutAddress2.Clear()
                    'Else
                    '    txtEISStatsMailoutAddress2.Text = dr.Item("strContactAddress2")
                    'End If
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
                    'If IsDBNull(dr.Item("strcomment")) Then
                    txtEISStatsMailoutComments.Clear()
                    'Else
                    '    txtEISStatsMailoutComments.Text = dr.Item("strcomment")
                    'End If
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

                End While
                dr.Close()
                btnAddtoEISMailout.Visible = True

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnAddtoEISMailout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddtoEISMailout.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to add facilies into Mailout.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                temp = ""

                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True And dgvEISStats(7, i).Value = "No" Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next

                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "



                    SQL = "Update AIRBranch.EIS_Admin set " &
                    "strMailOut = '1' " &
                    "where inventoryYear = '" & EISConfirm & "' " &
                    temp

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteReader()
                    MsgBox(EISConfirm & " Emission Inventory Facilities in Mailout.", MsgBoxStyle.Information, Me.Text)
                End If

            Else
                MsgBox("Year does not match selected EIS year")

            End If



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISComplete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISComplete.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to move Facilities into the QA process.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                temp = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next

                If temp = "" Then
                    MsgBox("No facilities are selected. No data changed.")
                    Exit Sub
                End If

                temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "

                SQL = "Update AIRBranch.EIS_Admin set " &
                "EISAccessCode = '0', " &
                "EISStatusCode = '5', " &
                "datEISstatus = sysdate, " &
                "UpdateUser = '" & Replace(CurrentUser.AlphaName, "'", "''") & "', " &
                "updatedatetime = sysdate " &
                "where inventoryYear = '" & EISConfirm & "' " &
                temp

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                MsgBox(EISConfirm & " EIS process completed.", MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("Year does not match selected EIS year")

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub ViewPollutantThresholds()
        Try
            Dim dsThreshold As DataSet
            Dim daThreshold As OracleDataAdapter

            If rdbThreeYearPollutants.Checked = True Then
                SQL = "Select " &
                "strPollutant, numThreshold, " &
                "numThresholdNAA " &
                "from AIRbranch.EIThresholds " &
                "where strType = '3YEAR' " &
                "order by strPollutant "
            Else
                SQL = "Select " &
                "strPollutant, numThreshold, " &
                "numThresholdNAA " &
                "from AIRbranch.EIThresholds " &
                "where strType = 'ANNUAL' " &
                "order by strPollutant "
            End If

            dsThreshold = New DataSet
            daThreshold = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daThreshold.Fill(dsThreshold, "ThresholdPollutants")
            dgvThresholdPollutants.DataSource = dsThreshold
            dgvThresholdPollutants.DataMember = "ThresholdPollutants"

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

            '   txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewThresholdPollutants_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewThresholdPollutants.LinkClicked
        Try
            ViewPollutantThresholds()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvThresholdPollutants_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvThresholdPollutants.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnAddNewPollutant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewPollutant.Click
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

            SQL = "Select * from " &
            "airbranch.EIThresholds " &
            "where upper(strPollutant) = '" & Replace(txtPollutant.Text.ToUpper, "'", "''") & "' " &
            "and strType = '" & ThresholdType & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read
            If recExist = True Then
                MsgBox("Pollutant currently exists for selected Type." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            Else
                SQL = "Insert into AIRBranch.EIThresholds " &
                "values " &
                "('" & Replace(txtPollutant.Text, "'", "''") & "', " &
                "'" & txtThreshold.Text & "', " &
                "'" & txtNonAttainmentThreshold.Text & "', " &
                "'" & ThresholdType & "') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()
                ViewPollutantThresholds()
                MsgBox("Data Added", MsgBoxStyle.Information, Me.Text)

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdatePollutant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdatePollutant.Click
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

            SQL = "Select * from " &
            "airbranch.EIThresholds " &
            "where upper(strPollutant) = '" & Replace(txtPollutant.Text.ToUpper, "'", "''") & "' " &
            "and strType = '" & ThresholdType & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read
            If recExist = True Then
                SQL = "Update AIRBranch.EIThresholds set " &
                      "numThreshold = '" & txtThreshold.Text & "', " &
                      "numThresholdNAA = '" & txtNonAttainmentThreshold.Text & "' " &
                      "where strType = '" & ThresholdType & "'  " &
                      "and strPollutant =  '" & Replace(txtPollutant.Text, "'", "''") & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()
                ViewPollutantThresholds()
                MsgBox("Data Updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Pollutant currently does not exists for selected Type." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub LoadEISYear()
        Try
            SQL = "Select " &
            "strYear, " &
            "strEIType, datDeadLine " &
            "from AIRBranch.EIThresholdYears " &
            "order by strYear desc "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            da.Fill(ds, "EISYears")
            dgvEISYear.DataSource = ds
            dgvEISYear.DataMember = "EISYears"

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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvEISYear_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvEISYear.MouseUp
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
                    dtpEISDeadline.Text = OracleDate
                Else
                    dtpEISDeadline.Text = dgvEISYear(2, hti.RowIndex).Value
                End If
            Else
                mtbThresholdYear.Clear()
                rdbEISAnnual.Checked = False
                rdbEISThreeYear.Checked = False
                dtpEISDeadline.Text = OracleDate
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbClearEISYear_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbClearEISYear.LinkClicked
        Try

            mtbThresholdYear.Clear()
            rdbEISAnnual.Checked = False
            rdbEISThreeYear.Checked = False
            dtpEISDeadline.Text = OracleDate

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddEISYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEISYear.Click
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

            SQL = "Select " &
            "strYear " &
            "from AIRBranch.EIThresholdYears " &
            "where strYEar = '" & Replace(mtbThresholdYear.Text, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read
            If recExist = True Then
                MsgBox("EIS Year currently exists." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            Else
                SQL = "Insert into AIRBranch.EIThresholdYears " &
                "values " &
                "('" & Replace(mtbThresholdYear.Text, "'", "''") & "', " &
                "'" & EISYearType & "', " &
                "'" & dtpEISDeadline.Text & "')  "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()
                LoadEISYear()
                MsgBox("Data Added", MsgBoxStyle.Information, Me.Text)

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateEISYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateEISYear.Click
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

            SQL = "Select " &
            "strYear " &
            "from AIRBranch.EIThresholdYears " &
            "where strYEar = '" & Replace(mtbThresholdYear.Text, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read
            If recExist = True Then
                SQL = "Update AIRBranch.EIThresholdYears set " &
                "strEIType = '" & EISYearType & "', " &
                "DatDeadline = '" & dtpEISDeadline.Text & "'  " &
                "where strYear = '" & mtbThresholdYear.Text & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()
                LoadEISYear()
                MsgBox("Data Updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("EIS Year does not currently exists." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearInactiveData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearInactiveData.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to delete inactive data.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                SQL = "delete airbranch.EIS_UnitControlPollutant " &
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_UnitControlMeasure  " &
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_UnitControlApproach  " &
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_RPGEOCoordinates  " &
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_RPApportionment  " &
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_ProcessControlPollutant " &
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_ProcessControlMeasure " &
                "where active = '0'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_ProcessControlApproach  " &
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_ReportingPeriodEmissions  " &
              "where active = '0'  " &
              "and intinventoryyear = '2010' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_ProcessOperatingDetails  " &
                "where active = '0'  " &
                "and intInventoryYear = '2010' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_ProcessRPTPeriodSCP  " &
                "where Active = '0'  " &
                "and intInventoryYear = '2010'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete Airbranch.EIS_RPApportionment " &
             "where exists (select * " &
             "from Airbranch.eis_Process " &
             "where active = '0' " &
             "and Airbranch.EIS_RPApportionment.facilitysiteid = Airbranch.eis_Process.facilitysiteid " &
             "and Airbranch.EIS_RPApportionment.ProcessId = Airbranch.eis_Process.ProcessId " &
             "and Airbranch.EIS_RPApportionment.EmissionsUnitID  = Airbranch.eis_Process.EmissionsUnitID) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = " delete Airbranch.EIS_ProcessControlPollutant " &
                " where exists (select *  " &
                " from Airbranch.EIS_ProcessControlApproach, airbranch.EIS_Process   " &
                " where   Airbranch.EIS_ProcessControlPollutant.facilitysiteid = Airbranch.EIS_ProcessControlApproach.facilitysiteid " &
                " and Airbranch.EIS_ProcessControlPollutant.ProcessId = Airbranch.EIS_ProcessControlApproach.ProcessId   " &
                " and Airbranch.EIS_ProcessControlPollutant.EmissionsUnitID  = Airbranch.EIS_ProcessControlApproach.EmissionsUnitID " &
                "and  Airbranch.EIS_ProcessControlPollutant.facilitysiteid = Airbranch.EIS_Process.facilitysiteid " &
                " and Airbranch.EIS_ProcessControlPollutant.ProcessId = Airbranch.EIS_Process.ProcessId   " &
                " and Airbranch.EIS_ProcessControlPollutant.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID  " &
                " and EIS_Process.active = '0' ) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete Airbranch.EIS_ProcessControlMeasure  " &
             "where exists (select * " &
             "from  airbranch.EIS_Process  " &
             "where   Airbranch.EIS_ProcessControlMeasure.facilitysiteid = Airbranch.EIS_Process.facilitysiteid " &
             "and Airbranch.EIS_ProcessControlMeasure.ProcessId = Airbranch.EIS_Process.ProcessId  " &
             "and Airbranch.EIS_ProcessControlMeasure.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID " &
             "and EIS_Process.active = '0' ) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete Airbranch.EIS_ProcessControlApproach " &
                "where exists (select * " &
                "from Airbranch.eis_Process " &
                "where active = '0' " &
                "and Airbranch.EIS_ProcessControlApproach.facilitysiteid = Airbranch.eis_Process.facilitysiteid " &
                "and Airbranch.EIS_ProcessControlApproach.ProcessId = Airbranch.eis_Process.ProcessId " &
                "and Airbranch.EIS_ProcessControlApproach.EmissionsUnitID  = Airbranch.eis_Process.EmissionsUnitID) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete Airbranch.EIS_ProcessControlPollutant " &
                "where exists (select * " &
                "from Airbranch.EIS_ProcessControlApproach " &
                "where active = '0' " &
                "and Airbranch.EIS_ProcessControlPollutant.facilitysiteid = Airbranch.EIS_ProcessControlApproach.facilitysiteid " &
                "and Airbranch.EIS_ProcessControlPollutant.ProcessId = Airbranch.EIS_ProcessControlApproach.ProcessId " &
                "and Airbranch.EIS_ProcessControlPollutant.EmissionsUnitID  = Airbranch.EIS_ProcessControlApproach.EmissionsUnitID) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete  Airbranch.EIS_ProcessOperatingDetails   " &
                "where exists (select * " &
                "from Airbranch.EIS_Process  " &
                "where active = '0'  " &
                "and Airbranch.EIS_ProcessOperatingDetails.facilitysiteid = Airbranch.EIS_Process.facilitysiteid  " &
                "and Airbranch.EIS_ProcessOperatingDetails.ProcessId = Airbranch.EIS_Process.ProcessId  " &
                "and Airbranch.EIS_ProcessOperatingDetails.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID)  "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete  Airbranch.EIS_ReportingPeriodEmissions   " &
                "where exists (select * " &
                "from Airbranch.EIS_Process  " &
                "where active = '0'  " &
                "and Airbranch.EIS_ReportingPeriodEmissions.facilitysiteid = Airbranch.EIS_Process.facilitysiteid  " &
                "and Airbranch.EIS_ReportingPeriodEmissions.ProcessId = Airbranch.EIS_Process.ProcessId  " &
                "and Airbranch.EIS_ReportingPeriodEmissions.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID)  "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete  Airbranch.EIS_ProcessRPTPeriodSCP   " &
                 "where exists (select * " &
                 "from Airbranch.EIS_Process  " &
                 "where active = '0'  " &
                 "and Airbranch.EIS_ProcessRPTPeriodSCP.facilitysiteid = Airbranch.EIS_Process.facilitysiteid  " &
                 "and Airbranch.EIS_ProcessRPTPeriodSCP.ProcessId = Airbranch.EIS_Process.ProcessId  " &
                 "and Airbranch.EIS_ProcessRPTPeriodSCP.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID)  "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete Airbranch.eis_processReportingPeriod   " &
                 "where exists (select * " &
                 "from  airbranch.EIS_Process  " &
                 "where   Airbranch.eis_processReportingPeriod.facilitysiteid = Airbranch.EIS_Process.facilitysiteid " &
                 "and Airbranch.eis_processReportingPeriod.ProcessId = Airbranch.EIS_Process.ProcessId  " &
                 "and Airbranch.eis_processReportingPeriod.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID " &
                 "and EIS_Process.active = '0' ) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_Process  " &
                              "where Active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "Delete airbranch.EIS_EmissionsUnit   " &
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_Releasepoint  " &
                "where active = '0'  " &
                "and numRPStatusCodeYear = '2010' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                MsgBox(EISConfirm & " Emission Inventory Year Inactive data deleted.", MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("Year does not match selected EIS year")

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnLoadEISLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadEISLog.Click
        Try
            If mtbEISLogAIRSNumber.Text <> "" And cboEISStatisticsYear.Text.Length = 4 Then
                mtbEILogAIRSNumber.Text = mtbEISLogAIRSNumber.Text
                cboEILogYear.Text = cboEISStatisticsYear.Text

                LoadFSData()

                TCDMUTools.SelectedIndex = 0
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsFipassed_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsFipassed.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0",
                 " and EISStatusCode >= 3 ", "", " and QAStatusCode = '2' ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, EPA Submitted Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRemoveFromQA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFromQA.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to delete all current QA data.", Me.Text)

            If EISConfirm = txtEILogSelectedYear.Text Then
                SQL = "delete AIRBranch.EIS_QAAdmin " &
                "where inventoryyear = '" & EISConfirm & "', " &
                "and facilitysiteid = '" & txtEILogSelectedAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update AIRBranch.EIS_Admin set " &
                  "EISAccessCode = '2', " &
                  "EISStatusCode = '3', " &
                  "datEISstatus = sysdate, " &
                  "UpdateUser = '" & Replace(CurrentUser.AlphaName, "'", "''") & "', " &
                  "updatedatetime = sysdate " &
                  "where inventoryYear = '" & EISConfirm & "' " &
                  "and facilitysiteid = '" & txtEILogSelectedAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                MsgBox("Done", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCleanUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCleanUp.Click
        Try
            For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                If dgvEISStats(0, i).Value = True Then

                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd = New OracleCommand("airbranch.PD_EIS_QASTART", CurrentConnection)
                    cmd.CommandType = CommandType.StoredProcedure
                    temp = dgvEISStats(1, i).Value

                    cmd.Parameters.Add(New OracleParameter("AIRSNUMBER_IN", OracleDbType.Varchar2)).Value = dgvEISStats(1, i).Value
                    cmd.Parameters.Add(New OracleParameter("INTYEAR_IN", OracleDbType.Decimal)).Value = cboEISStatisticsYear.Text

                    cmd.ExecuteNonQuery()

                    If CurrentConnection.State = ConnectionState.Open Then
                        CurrentConnection.Close()
                    End If
                End If
            Next

            MsgBox("Complete", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub ViewMailoutData(ByVal MailoutStatus As String)
        Try
            Dim dgvRow As New DataGridViewRow
            dgvEISStats.Rows.Clear()
            SQL = "select " &
            "'False' as ID, " &
            " AIRBRANCH.EIS_Admin.facilitysiteid, " &
           "AIRBRANCH.APBFacilityInformation.strFacilityname, " &
           "AIRBRANCH.EIS_Admin.inventoryyear, " &
           "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " &
           "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " &
           "case " &
           "when strOptOut = '1' then 'Yes' " &
           "when strOptOut = '0' then 'No' " &
           "else '-' " &
           "End strOptOut, " &
             "case " &
           "when strEnrollment = '1' then 'Yes' " &
           "when strEnrollment = '0' then 'No' " &
           "else '-' " &
           "end strEnrollment, " &
           "case " &
           "when strMailout = '1' then 'Yes' " &
           "else 'No' " &
           "end strMailout, " &
           "case " &
           "when strContactEmail is null then '-' " &
           "else strContactEmail " &
           "end ContactEmail, " &
             "case " &
           "When strContactPrefix is null then '-' " &
           "else strContactPrefix " &
           "end strContactPrefix, " &
           "case " &
           "when strContactFirstName is null then '-' " &
           "else strContactFirstName " &
           "end strContactFirstName, " &
           "case " &
           "When strContactLastName is null then '-' " &
           "else strContactLastName " &
           "end strContactLastName, " &
           "case " &
          "when strDMUResponsibleStaff is null then '-' " &
           "else strDMUResponsibleStaff " &
            "end strDMUResponsibleStaff, " &
            "AIRBranch.EIS_Mailout.strContactCompanyName as CoName, " &
            "AIRBranch.EIS_Mailout.strContactAddress1 as ContactAddress1, " &
            "AIRBranch.EIS_Mailout.strContactAddress2 as ContactAddress2, " &
            "AIRBranch.EIS_Mailout.strContactCity as ContactCity, " &
            "AIRBranch.EIS_Mailout.strContactState as  ContactState, " &
            "AIRBranch.EIS_Mailout.strContactZipCode as ContactZip, " &
            "AIRBranch.EIS_Mailout.strContactFirstname as ContactFirstName, " &
            "AIRBranch.EIS_Mailout.strContactLastName as ContactLastName, " &
            "AIRBranch.EIS_Mailout.strContactPrefix as ContactPrefix, " &
            "AIRBranch.EIS_Mailout.strContactEmail  as ContactEmail " &
           "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " &
           "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " &
           "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " &
           "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " &
           "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " &
           "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " &
           "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " &
           "and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " &
           "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " &
           "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " &
            "and AIRBranch.EIS_Admin.Active = '1' " &
           "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISMailout.Text & "'"

            If MailoutStatus = "1" Then
                SQL = SQL & " and strMailout = '1' "
            End If

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = False
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = False
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If

                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = False
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = False
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = False
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = False
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If

                If IsDBNull(dr.Item("CoName")) Then
                    dgvRow.Cells(20).Value = False
                Else
                    dgvRow.Cells(20).Value = dr.Item("CoName")
                End If
                If IsDBNull(dr.Item("ContactAddress1")) Then
                    dgvRow.Cells(21).Value = False
                Else
                    dgvRow.Cells(21).Value = dr.Item("ContactAddress1")
                End If
                If IsDBNull(dr.Item("ContactAddress2")) Then
                    dgvRow.Cells(22).Value = False
                Else
                    dgvRow.Cells(22).Value = dr.Item("ContactAddress2")
                End If
                If IsDBNull(dr.Item("ContactCity")) Then
                    dgvRow.Cells(23).Value = False
                Else
                    dgvRow.Cells(23).Value = dr.Item("ContactCity")
                End If
                If IsDBNull(dr.Item("ContactState")) Then
                    dgvRow.Cells(24).Value = False
                Else
                    dgvRow.Cells(24).Value = dr.Item("ContactState")
                End If
                If IsDBNull(dr.Item("ContactZip")) Then
                    dgvRow.Cells(25).Value = False
                Else
                    dgvRow.Cells(25).Value = dr.Item("ContactZip")
                End If
                If IsDBNull(dr.Item("ContactFirstName")) Then
                    dgvRow.Cells(26).Value = False
                Else
                    dgvRow.Cells(26).Value = dr.Item("ContactFirstName")
                End If
                If IsDBNull(dr.Item("ContactLastName")) Then
                    dgvRow.Cells(27).Value = False
                Else
                    dgvRow.Cells(27).Value = dr.Item("ContactLastName")
                End If
                If IsDBNull(dr.Item("ContactPrefix")) Then
                    dgvRow.Cells(28).Value = False
                Else
                    dgvRow.Cells(28).Value = dr.Item("ContactPrefix")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(29).Value = False
                Else
                    dgvRow.Cells(29).Value = dr.Item("ContactEmail")
                End If

                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewMailoutData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewMailoutData.Click
        Try
            If txtSelectedEISMailout.Text = "" Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISMailout.Text, "1", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Mailout Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnGenerateMailout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateMailout.Click
        Try
            If txtSelectedEISMailout.Text = "" Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Update airbranch.EIS_Admin set " &
            "strMailout = '1' " &
            "where inventoryYear = '" & txtSelectedEISMailout.Text & "' " &
            "and Active = '1' "
            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            EIS_VIEW(txtSelectedEISMailout.Text, "1", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Mailout Count (Generated)"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRemoveAllMailout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAllMailout.Click
        Try

            SQL = "Update airbranch.EIS_Admin set " &
          "strMailout = '' " &
          "where inventoryYear = '" & txtSelectedEISMailout.Text & "' " &
          "and strMailout = '1' " &
          "and Active = '1' "

            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()
            EIS_VIEW(txtSelectedEISMailout.Text, "1", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Mailout Count (Removed)"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnViewEISEnrolled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewEISEnrolled.Click
        Try
            If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            EIS_VIEW(txtEISStatsEnrollmentYear.Text, "", "1", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Enrolled"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEISEnrollMailoutList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISEnrollMailoutList.Click
        Try
            If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Update AIRBranch.EIS_Admin set " &
            "strEnrollment = '1' , " &
            "EISSTATUSCODE= '1' " &
            "where active = '1' " &
            "and InventoryYear = '" & txtEISStatsEnrollmentYear.Text & "' " &
            "and strMailout = '1' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            EIS_VIEW(txtEISStatsEnrollmentYear.Text, "", "1", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Enrolled (Generated)"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRemoveEISEnrolled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveEISEnrolled.Click
        Try
            If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Update AIRBranch.EIS_Admin set " &
            "strEnrollment = '0' " &
            "where active = '1' " &
            "and InventoryYear = '" & txtEISStatsEnrollmentYear.Text & "' " &
            "and strEnrollment = '1' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            EIS_VIEW(txtEISStatsEnrollmentYear.Text, "", "1", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Enrolled (Removed)"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub EIS_VIEW(ByVal EISYear As String, ByVal EISMailout As String, ByVal EISEnrollment As String,
                   ByVal EISActive As String, ByVal Optout As String, ByVal EISStatus As String,
                   ByVal EISAccess As String, ByVal QAStatus As String)

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

            SQL = "Select " &
            "'False' as ID, " &
            "FACILITYSITEID, " &
            "STRFACILITYNAME, INVENTORYYEAR," &
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
            " from AIRBranch.VW_EIS_Stats " &
            "where inventoryyear = '" & EISYear & "' " &
            "and Active = '" & EISActive & "' "

            If EISMailout <> "" Then
                SQL = SQL & " and strMailout = '" & EISMailout & "' "
            End If
            If EISEnrollment <> "" Then
                SQL = SQL & " and strEnrollment = '" & EISEnrollment & "' "
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

            dgvEISStats.Rows.Clear()

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read
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

                dgvRow.Cells(27).Value = DB.GetNullable(Of String)(dr.Item("FITrackingNumber"))
                dgvRow.Cells(28).Value = DB.GetNullable(Of String)(dr.Item("PointTrackingNumber"))
                dgvRow.Cells(29).Value = DB.GetNullable(Of String)(dr.Item("Comments"))

                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISSummaryToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISSummaryToExcel.Click
        dgvEISStats.ExportToExcel(Me)
    End Sub

#Region " Accept Button "

    Private Sub AcceptButton_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles mtbEILogAIRSNumber.Leave,
    txtEIModifyFacilityName.Leave,
    txtEIModifyLocation.Leave, txtEIModifyCity.Leave, mtbEIModifyZipCode.Leave,
    txtEIModifyMLocation.Leave, txtEIModifyMCity.Leave, mtbEIModifyMZipCode.Leave,
    mtbEIModifyLatitude.Leave, mtbEIModifyLongitude.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub mtbEILogAIRSNumber_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles mtbEILogAIRSNumber.Enter
        Me.AcceptButton = btnReloadFSData
    End Sub

    Private Sub txtEIModifyFacilityName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtEIModifyFacilityName.Enter
        Me.AcceptButton = btnEIModifyUpdateName
    End Sub

    Private Sub EIModifyLocation_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtEIModifyLocation.Enter, txtEIModifyCity.Enter, mtbEIModifyZipCode.Enter
        Me.AcceptButton = btnEIModifyUpdateLocation
    End Sub

    Private Sub EIModifyMailing_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtEIModifyMLocation.Enter, txtEIModifyMCity.Enter, mtbEIModifyMZipCode.Enter
        Me.AcceptButton = btnEIModifyUpdateMailing
    End Sub


    Private Sub EIModifyLatitudeLongitude_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
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
            "FROM airbranch.EIS_FACILITYSITE ef " &
            "INNER JOIN airbranch.APBHEADERDATA hd ON ef.FACILITYSITEID = SUBSTR( " &
            "  hd.STRAIRSNUMBER, 5) " &
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