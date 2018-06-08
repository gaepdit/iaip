Imports System.Data.SqlClient
Imports Iaip.Apb.Facilities
Imports Iaip.DAL.FacilityData

Public Class GecoTool

#Region "Page Load"

    Private Sub GecoTool_Load(sender As Object, e As EventArgs) Handles Me.Load
        FormatWebUsers()
    End Sub

    Private Sub FormatWebUsers()
        Try
            dgvUsers.RowHeadersVisible = False
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvUsers.AllowUserToResizeColumns = True
            dgvUsers.AllowUserToAddRows = False
            dgvUsers.AllowUserToDeleteRows = False
            dgvUsers.AllowUserToOrderColumns = False
            dgvUsers.AllowUserToResizeRows = False
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

#End Region

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

    Private Sub btnViewEmailData_Click(sender As Object, e As EventArgs) Handles btnViewEmailData.Click
        LoadUserInfo(txtWebUserEmail.Text)
        LoadUserFacilityInfo(txtWebUserEmail.Text)
    End Sub


    Private Sub btnViewUserData_Click(sender As Object, e As EventArgs) Handles btnViewUserData.Click
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
            Dim result As AirsNumberValidationResult = ValidateAirsFacility(mtbAIRSNumber.Text)

            If result <> AirsNumberValidationResult.Valid Then
                MessageBox.Show("The AIRS number is not valid.", "Error")
                Exit Sub
            End If


            Dim query As String = "Select numUserId from olapuserlogin " &
                " where struseremail = @struseremail "

            Dim param As New SqlParameter("@struseremail", UCase(txtEmail.Text))

            Dim userID As Integer = DB.GetInteger(query, param)

            If userID = 0 Then ' Email address is not registered
                MessageBox.Show("This Email Address is not registered", "Error")
                Exit Sub
            End If

            query = "select convert(bit, count(*)) from OLAPUSERACCESS " &
                " where NUMUSERID = @NUMUSERID and STRAIRSNUMBER = @STRAIRSNUMBER "

            Dim params As SqlParameter() = {
                    New SqlParameter("@NUMUSERID", userID),
                    New SqlParameter("@STRAIRSNUMBER", "0413" & mtbAIRSNumber.Text),
                    New SqlParameter("@STRFACILITYNAME", lblFaciltyName.Text)
                }

            If DB.GetBoolean(query, params) Then ' already assigned
                MessageBox.Show("This user already has access to this facility.")
                Exit Sub
            End If

            query = "Insert into OlapUserAccess " &
                    " (numUserId, strAirsNumber, strFacilityName) values " &
                    " (@NUMUSERID, @STRAIRSNUMBER, @STRFACILITYNAME) "

            DB.RunCommand(query, params)

            ViewFacilitySpecificUsers()

            MessageBox.Show("The User has been added to this facility", "Success")

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
            If txtWebUserID.Text <> "" Then
                Dim Result As AirsNumberValidationResult = ValidateAirsFacility(mtbFacilityToAdd.Text)
                If Result = AirsNumberValidationResult.Valid Then
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
                        MessageBox.Show(Me, "The facility has been added to this user", "Insert Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show(Me, "The facility already exists for this user." & vbCrLf & "NO DATA SAVED", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                Else
                    MessageBox.Show(Me, GetAirsValidationMsg(Result), Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                MessageBox.Show(Me, "You must enter a User's e-mail address.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

#Region " Accept button "

    Private Sub NoAcceptButton(sender As Object, e As EventArgs) _
        Handles txtWebUserEmail.Leave, mtbAIRSNumber.Leave, txtEditUserPassword.Leave,
        txtEditEmail.Leave, mtbFacilityToAdd.Leave, txtEmail.Leave

        AcceptButton = Nothing

    End Sub

    Private Sub mtbAIRSNumber_Enter(sender As Object, e As EventArgs) Handles mtbAIRSNumber.Enter
        AcceptButton = btnViewUserData
    End Sub

    Private Sub txtWebUserEmail_Enter(sender As Object, e As EventArgs) Handles txtWebUserEmail.Enter
        AcceptButton = btnViewEmailData
    End Sub

    Private Sub txtEditUserPassword_Enter(sender As Object, e As EventArgs) Handles txtEditUserPassword.Enter
        AcceptButton = btnUpdatePassword
    End Sub

    Private Sub txtEditEmail_Enter(sender As Object, e As EventArgs) Handles txtEditEmail.Enter
        AcceptButton = btnChangeEmailAddress
    End Sub

    Private Sub mtbFacilityToAdd_Enter(sender As Object, e As EventArgs) Handles mtbFacilityToAdd.Enter
        AcceptButton = btnAddFacilitytoUser
    End Sub

    Private Sub txtEmail_Enter(sender As Object, e As EventArgs) Handles txtEmail.Enter
        AcceptButton = btnAddUser
    End Sub

#End Region

End Class