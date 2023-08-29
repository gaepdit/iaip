Imports System.Data.SqlClient
Imports EpdIt
Imports Iaip.Apb.Facilities
Imports Iaip.DAL
Imports Iaip.DAL.FacilityData
Imports Iaip.DAL.Geco
Imports Iaip.UrlHelpers

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

        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
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
            "DateEmailConfirmed, datLastLogIn, " &
            "NewEmail " &
            "from OlapUserProfile inner join OLAPUserLogIn " &
            "on OLAPUserProfile.numUserID = OLAPUserLogIn.numuserid " &
            "where strUserEmail = @strUserEmail "
            Dim param As New SqlParameter("@strUserEmail", UserData)
            Dim dr As DataRow = DB.GetDataRow(SQL, param)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("numUserID")) Then
                    txtWebUserID.Clear()
                Else
                    txtWebUserID.Text = dr.Item("numUserID")
                End If
                If IsDBNull(dr.Item("strfirstname")) Then
                    lblFName.Text = ""
                Else
                    lblFName.Text = "First Name: " & dr.Item("strfirstname")
                End If
                If IsDBNull(dr.Item("strlastname")) Then
                    lblLName.Text = ""
                Else
                    lblLName.Text = "Last Name: " & dr.Item("strlastname")
                End If
                If IsDBNull(dr.Item("strtitle")) Then
                    lblTitle.Text = ""
                Else
                    lblTitle.Text = "Title: " & dr.Item("strtitle")
                End If
                If IsDBNull(dr.Item("strcompanyname")) Then
                    lblCoName.Text = ""
                Else
                    lblCoName.Text = "Company Name: " & dr.Item("strcompanyname")
                End If
                If IsDBNull(dr.Item("straddress")) Then
                    lblAddress.Text = ""
                Else
                    lblAddress.Text = dr.Item("straddress")
                End If
                If IsDBNull(dr.Item("strcity")) Then
                    lblCityStateZip.Text = ""
                Else
                    lblCityStateZip.Text = dr.Item("strcity") & ", " & dr.Item("strstate") & " " & dr.Item("strzip")
                End If
                If IsDBNull(dr.Item("strphonenumber")) Then
                    lblPhoneNo.Text = ""
                Else
                    lblPhoneNo.Text = "Phone Number: " & dr.Item("strphonenumber")
                End If
                If IsDBNull(dr.Item("strfaxnumber")) Then
                    lblFaxNo.Text = ""
                Else
                    lblFaxNo.Text = "Fax Number: " & dr.Item("strfaxnumber")
                End If
                If IsDBNull(dr.Item("DateEmailConfirmed")) Then
                    lblConfirmDate.Text = $"{UserData} is registered but email not yet confirmed by user."
                    lblConfirmDate.BackColor = IaipColors.WarningBackColor
                    lblConfirmDate.ForeColor = IaipColors.WarningForeColor
                Else
                    lblConfirmDate.Text = "Date account confirmed: " & CDate(dr.Item("DateEmailConfirmed")).ToShortDateString
                    lblConfirmDate.BackColor = Nothing
                    lblConfirmDate.ForeColor = Nothing
                End If
                If IsDBNull(dr.Item("datLastLogIn")) Then
                    lblLastLogIn.Text = ""
                Else
                    lblLastLogIn.Text = "Date user last logged in: " & dr.Item("datLastLogIn")
                End If
                If IsDBNull(dr.Item("NewEmail")) Then
                    txtEditEmail.Text = ""
                Else
                    txtEditEmail.Text = dr.Item("NewEmail")
                End If
                btnEditUserData.Visible = True
            Else
                ClearUserInfo()
            End If

            HideEmailEditForm()
        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ClearUserInfo()
        txtWebUserID.Clear()
        lblLastLogIn.Text = ""
        lblConfirmDate.Text = ""
        txtEditEmail.Clear()
        lblFName.Text = ""
        lblLName.Text = ""
        lblTitle.Text = ""
        lblCoName.Text = ""
        lblAddress.Text = ""
        lblCityStateZip.Text = ""
        lblPhoneNo.Text = ""
        lblFaxNo.Text = ""
        lblConfirmDate.Text = "Entered email not found in GECO."
        lblConfirmDate.BackColor = IaipColors.WarningBackColor
        lblConfirmDate.ForeColor = IaipColors.WarningForeColor
        btnEditUserData.Visible = False
    End Sub

    Private Sub HideEmailEditForm()
        btnChangeEmailAddress.Visible = False
        lblChangeEmailAddress.Visible = False
        txtEditEmail.Visible = False
    End Sub

    Private Sub LoadUserFacilityInfo(EmailLoc As String)
        Try
            Dim dt As DataTable = GetUserGecoAccessTable(EmailLoc)

            dgvUserFacilities.Rows.Clear()

            For Each dr As DataRow In dt.Rows
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    Continue For
                End If

                Using dgvRow As New DataGridViewRow
                    dgvRow.CreateCells(dgvUserFacilities)

                    dgvRow.Cells(0).Value = dr.Item("strAIRSNumber").ToString
                    dgvRow.Cells(0).ReadOnly = True
                    dgvRow.Cells(1).Value = DBUtilities.GetNullableString(dr.Item("strFacilityName"))
                    dgvRow.Cells(1).ReadOnly = True
                    dgvRow.Cells(2).Value = CBool(dr.Item("intAdminAccess"))
                    dgvRow.Cells(3).Value = CBool(dr.Item("intFeeAccess"))
                    dgvRow.Cells(4).Value = CBool(dr.Item("intEIAccess"))

                    dgvUserFacilities.Rows.Add(dgvRow)
                End Using
            Next

            cboFacilityToDelete.DataSource = dt
            cboFacilityToDelete.DisplayMember = "strfacilityname"
            cboFacilityToDelete.ValueMember = "strairsnumber"
            cboFacilityToDelete.Text = ""

        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewEmailData_Click(sender As Object, e As EventArgs) Handles btnViewEmailData.Click
        If Not String.IsNullOrEmpty(txtWebUserEmail.Text) Then
            LoadUserInfo(txtWebUserEmail.Text)
            LoadUserFacilityInfo(txtWebUserEmail.Text)
        Else
            ClearUserInfo()
            HideEmailEditForm()
        End If
    End Sub


    Private Sub btnViewUserData_Click(sender As Object, e As EventArgs) Handles btnViewUserData.Click
        ViewFacilitySpecificUsers()
    End Sub

    Private Sub ViewFacilitySpecificUsers()
        Try

            If Not Apb.ApbFacilityId.IsValidAirsNumberFormat(mtbAIRSNumber.Text) Then
                MsgBox("Please enter a valid AIRS #.")
            Else
                Dim dgvRow As DataGridViewRow
                txtEmail.Clear()

                Dim airs As New Apb.ApbFacilityId(mtbAIRSNumber.Text)

                Dim fn As String = GetFacilityName(airs)

                If fn = "" Then
                    lblFaciltyName.Text = " - "
                Else
                    lblFaciltyName.Text = Facility.SanitizeFacilityNameForDb(fn)
                End If

                Dim dt As DataTable = GetGecoAccessForFacility(airs)

                dgvUsers.Rows.Clear()

                For Each dr As DataRow In dt.Rows
                    dgvRow = New DataGridViewRow
                    dgvRow.CreateCells(dgvUsers)

                    dgvRow.Cells(0).Value = CInt(dr.Item("numuserid"))
                    dgvRow.Cells(1).Value = CInt(dr.Item("numuserid"))
                    dgvRow.Cells(2).Value = DBUtilities.GetNullableString(dr.Item("Email"))
                    dgvRow.Cells(3).Value = CBool(dr.Item("intAdminAccess"))
                    dgvRow.Cells(4).Value = CBool(dr.Item("intFeeAccess"))
                    dgvRow.Cells(5).Value = CBool(dr.Item("intEIAccess"))

                    dgvUsers.Rows.Add(dgvRow)
                Next

                cboUsers.DataSource = dt
                cboUsers.DisplayMember = "Email"
                cboUsers.ValueMember = "numuserid"
                cboUsers.Text = ""
            End If

        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        Try
            Dim result As AirsNumberValidationResult = ValidateAirsFacility(mtbAIRSNumber.Text)

            If result <> AirsNumberValidationResult.Valid Then
                MessageBox.Show("The AIRS number is not valid.", "Error")
                Return
            End If

            Dim query As String = "Select numUserId from olapuserlogin " &
                " where struseremail = @struseremail "

            Dim param As New SqlParameter("@struseremail", UCase(txtEmail.Text))

            Dim userID As Integer = DB.GetInteger(query, param)

            If userID = 0 Then ' Email address is not registered
                MessageBox.Show("This Email Address is not registered", "Error")
                Return
            End If

            Dim airs As New Apb.ApbFacilityId(mtbAIRSNumber.Text)

            If UserGecoAccessExists(userID, airs) Then ' already assigned
                MessageBox.Show("This user already has access to this facility.")
                Return
            End If

            AddUserGecoAccess(userID, airs)

            ViewFacilitySpecificUsers()

            MessageBox.Show("The user has been added to this facility", "Success")

        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            DeleteUserGecoAccess(CInt(cboUsers.SelectedValue), New Apb.ApbFacilityId(mtbAIRSNumber.Text))
            ViewFacilitySpecificUsers()
            MsgBox("The user has been removed from this facility", MsgBoxStyle.Information, "User removed")
        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Dim adminaccess As Boolean
            Dim feeaccess As Boolean
            Dim eiaccess As Boolean

            For i As Integer = 0 To dgvUsers.Rows.Count - 1
                adminaccess = CBool(dgvUsers(3, i).Value)
                feeaccess = CBool(dgvUsers(4, i).Value)
                eiaccess = CBool(dgvUsers(5, i).Value)

                UpdateUserGecoAccess(adminaccess, feeaccess, eiaccess, CInt(dgvUsers(1, i).Value), New Apb.ApbFacilityId(mtbAIRSNumber.Text))
            Next

            ViewFacilitySpecificUsers()

            MsgBox("The records have been updated", MsgBoxStyle.Information, "Success")
        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEditUserData_Click(sender As Object, e As EventArgs) Handles btnEditUserData.Click
        Try
            btnChangeEmailAddress.Visible = True
            lblChangeEmailAddress.Visible = True
            txtEditEmail.Visible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnChangeEmailAddress_Click(sender As Object, e As EventArgs) Handles btnChangeEmailAddress.Click
        If txtWebUserID.Text <> "" OrElse Not IsValidEmailAddress(txtEditEmail.Text) Then

            Dim token As String = Nothing
            Dim result As UpdateGecoUserEmailResult = UpdateGecoUserEmail(txtWebUserEmail.Text, txtEditEmail.Text, token)

            Select Case result
                Case UpdateGecoUserEmailResult.Success
                    MessageBox.Show("The new email has been saved. A web page will now open to confirm a link has been sent to the user.",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    OpenEmailChangeSuccessPage(txtEditEmail.Text, token)


                Case UpdateGecoUserEmailResult.NewEmailExists
                    MessageBox.Show("An account already exists for that email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Case Else
                    MessageBox.Show("An error occurred. The email address has not been changed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Select

        Else
            MessageBox.Show("The email address entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnAddFacilitytoUser_Click(sender As Object, e As EventArgs) Handles btnAddFacilitytoUser.Click
        Try
            If txtWebUserID.Text <> "" Then
                Dim result As AirsNumberValidationResult = ValidateAirsFacility(mtbFacilityToAdd.Text)

                If result = AirsNumberValidationResult.Valid Then
                    If UserGecoAccessExists(CInt(txtWebUserID.Text), New Apb.ApbFacilityId(mtbFacilityToAdd.Text)) Then
                        MessageBox.Show(Me, "The facility already exists for this user." & vbCrLf & "NO DATA SAVED", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        AddUserGecoAccess(CInt(txtWebUserID.Text), New Apb.ApbFacilityId(mtbFacilityToAdd.Text))
                        LoadUserFacilityInfo(txtWebUserEmail.Text)
                        MessageBox.Show(Me, "The facility has been added to this user", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    MessageBox.Show(Me, GetAirsValidationMsg(result), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                MessageBox.Show(Me, "You must enter a user's e-mail address.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnDeleteFacilityUser_Click(sender As Object, e As EventArgs) Handles btnDeleteFacilityUser.Click
        Try
            If txtWebUserID.Text <> "" AndAlso cboFacilityToDelete.Text <> "" Then
                DeleteUserGecoAccess(CInt(txtWebUserID.Text), New Apb.ApbFacilityId(cboFacilityToDelete.SelectedValue.ToString))
                LoadUserFacilityInfo(txtWebUserEmail.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateUser_Click(sender As Object, e As EventArgs) Handles btnUpdateUser.Click
        Try
            Dim adminaccess As Boolean
            Dim feeaccess As Boolean
            Dim eiaccess As Boolean

            For i As Integer = 0 To dgvUserFacilities.Rows.Count - 1
                adminaccess = CBool(dgvUserFacilities(2, i).Value)
                feeaccess = CBool(dgvUserFacilities(3, i).Value)
                eiaccess = CBool(dgvUserFacilities(4, i).Value)

                UpdateUserGecoAccess(adminaccess, feeaccess, eiaccess, CInt(txtWebUserID.Text), New Apb.ApbFacilityId(dgvUserFacilities(0, i).Value.ToString))
            Next

            LoadUserFacilityInfo(txtWebUserEmail.Text)
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Success")
        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region " Accept button "

    Private Sub NoAcceptButton(sender As Object, e As EventArgs) _
        Handles txtWebUserEmail.Leave, mtbAIRSNumber.Leave,
        txtEditEmail.Leave, mtbFacilityToAdd.Leave, txtEmail.Leave

        AcceptButton = Nothing

    End Sub

    Private Sub mtbAIRSNumber_Enter(sender As Object, e As EventArgs) Handles mtbAIRSNumber.Enter
        AcceptButton = btnViewUserData
    End Sub

    Private Sub txtWebUserEmail_Enter(sender As Object, e As EventArgs) Handles txtWebUserEmail.Enter
        AcceptButton = btnViewEmailData
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