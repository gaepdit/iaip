Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports Iaip.Apb.Facilities

Public Class PASPFeeAuditLog
    Dim SQL As String
    Dim ds As DataSet
    Dim da As SqlDataAdapter
    Dim tempContact As Contact
    Dim tempFacility As Facility

#Region " Properties "

    Public Property FeeYear() As String
    Public Property AirsNumber() As Apb.ApbFacilityId

#End Region

    Private Sub PASPFeeAuditLog_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadSelectedNSPSExemptions()
        LoadTransactionTypes()
        LoadPayTypes()
        LoadStaff()
        LoadFeeYears()

        PopulateComboBoxes()

        DTPAuditStart.Value = Today
        DTPAuditEnd.Value = Today
        DTPDateCollectionsCeased.Value = Today
        cboStaffResponsible.SelectedValue = CurrentUser.UserID

        pnlInvoiceData.Enabled = False
        pnlFacilityData.Enabled = False
        pnlFacilityData2.Enabled = False

        ParseParameters()
    End Sub

#Region "Subs and Functions"

    Private Sub PopulateComboBoxes()

        ' Operational Status
        cboInitialOpStatus.BindToDictionary(FacilityOperationalStatusDescriptions)

        ' Classification
        cboInitialClassification.BindToDictionary(FacilityClassificationDescriptions)

        cboEditClassification.Items.Add("")
        cboEditClassification.Items.Add("A")
        cboEditClassification.Items.Add("SM")
        cboEditClassification.Items.Add("B")
        cboEditClassification.Items.Add("PR")

        ' Payment Type
        cboEditPaymentType.Items.Add("")
        cboEditPaymentType.Items.Add("Entire Annual Year")
        cboEditPaymentType.Items.Add("Four Quarterly Payments")

        ' Audit Type
        cboAuditType.Items.Add("")
        cboAuditType.Items.Add("Facility Self Amendment")
        cboAuditType.Items.Add("Level 1 Audit")
        cboAuditType.Items.Add("Level 2 Audit")
        cboAuditType.Items.Add("Level 3 Audit")
        cboAuditType.Items.Add("Other")
    End Sub

    Private Sub ParseParameters()
        If Parameters IsNot Nothing Then
            If Parameters.ContainsKey(FormParameter.AirsNumber) Then
                Try
                    Me.AirsNumber = Parameters(FormParameter.AirsNumber)
                    mtbAirsNumber.Text = Me.AirsNumber.FormattedString
                Catch ex As Apb.InvalidAirsNumberException
                    Me.AirsNumber = Nothing
                    mtbAirsNumber.Clear()
                End Try
            End If

            If Parameters.ContainsKey(FormParameter.FeeYear) Then
                Me.FeeYear = Parameters(FormParameter.FeeYear)
            End If
        End If

        If FeeYearsComboBox.Items.Contains(Me.FeeYear) Then
            FeeYearsComboBox.SelectedItem = Me.FeeYear
        Else
            FeeYearsComboBox.SelectedIndex = 0
            Me.FeeYear = Nothing
        End If

        MailoutEditingToggle(False)
        MailoutEditingToggle(False, False)

        If Me.AirsNumber.ToString IsNot Nothing AndAlso Me.FeeYear IsNot Nothing Then
            LoadAdminData()
            LoadAuditedData()
        End If
    End Sub

    Private Sub LoadFeeYears()
        FeeYearsComboBox.DataSource = DAL.GetAllFeeYears().AddBlankRowToList("Select…")
    End Sub

    Private Sub LoadPayTypes()
        Try
            Dim dtPayTypes As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            Dim SQL As String = "select " &
            "numPayTypeID, strPayTypeDesc " &
            "from FSLK_PayType " &
            "order by numPayTypeID "

            Dim dt As DataTable = DB.GetDataTable(SQL)

            dtPayTypes.Columns.Add("numPayTypeID", GetType(String))
            dtPayTypes.Columns.Add("strPayTypeDesc", GetType(String))

            drNewRow = dtPayTypes.NewRow()
            drNewRow("numPayTypeID") = ""
            drNewRow("strPayTypeDesc") = ""
            dtPayTypes.Rows.Add(drNewRow)

            For Each drDSRow In dt.Rows()
                drNewRow = dtPayTypes.NewRow()
                drNewRow("numPayTypeID") = drDSRow("numPayTypeID")
                drNewRow("strPayTypeDesc") = drDSRow("strPayTypeDesc")
                dtPayTypes.Rows.Add(drNewRow)
            Next

            With cboInvoiceType
                .DataSource = dtPayTypes
                .DisplayMember = "strPayTypeDesc"
                .ValueMember = "numPayTypeID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadTransactionTypes()
        Try
            Dim dtTransactions As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            Dim SQL As String = "Select " &
            "transactionTypeCode, Description " &
            "from FSLK_TransactionType " &
            "order by description "
            Dim dt As DataTable = DB.GetDataTable(SQL)

            dtTransactions.Columns.Add("transactionTypeCode", GetType(System.String))
            dtTransactions.Columns.Add("Description", GetType(System.String))

            drNewRow = dtTransactions.NewRow()
            drNewRow("transactionTypeCode") = ""
            drNewRow("Description") = ""
            dtTransactions.Rows.Add(drNewRow)

            For Each drDSRow In dt.Rows()
                drNewRow = dtTransactions.NewRow()
                drNewRow("transactionTypeCode") = drDSRow("transactionTypeCode")
                drNewRow("Description") = drDSRow("Description")
                dtTransactions.Rows.Add(drNewRow)
            Next

            With cboTransactionType
                .DataSource = dtTransactions
                .DisplayMember = "Description"
                .ValueMember = "transactionTypeCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadSelectedNSPSExemptions()
        Try
            dgvEditExemptions.RowHeadersVisible = False
            dgvEditExemptions.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEditExemptions.AllowUserToResizeColumns = True
            dgvEditExemptions.AllowUserToAddRows = False
            dgvEditExemptions.AllowUserToDeleteRows = False
            dgvEditExemptions.AllowUserToOrderColumns = True
            dgvEditExemptions.AllowUserToResizeRows = True
            dgvEditExemptions.ColumnHeadersHeight = "35"

            Dim colWrite As New DataGridViewCheckBoxColumn
            dgvEditExemptions.Columns.Add(colWrite)

            dgvEditExemptions.Columns.Add("NSPSReasonCode", "NSPS ID")
            dgvEditExemptions.Columns("NSPSReasonCode").DisplayIndex = 1
            dgvEditExemptions.Columns("NSPSReasonCode").ReadOnly = True
            dgvEditExemptions.Columns("NSPSReasonCode").Visible = False

            dgvEditExemptions.Columns.Add("Description", "NSPS Exemption Reason")
            dgvEditExemptions.Columns("Description").DisplayIndex = 2
            dgvEditExemptions.Columns("Description").ReadOnly = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ClearAdminData()
        Try
            txtFeeAdminFacilityName.Clear()
            rdbEnrolledTrue.Checked = False
            rdbEnrolledFalse.Checked = False
            dtpEnrollmentDate.Value = Today
            dtpEnrollmentInitial.Value = Today
            rdbMailoutTrue.Checked = False
            rdbMailoutFalse.Checked = False
            rdbSubmittalTrue.Checked = False
            rdbSubmittalFalse.Checked = False
            dtpSubmittalDate.Value = Today
            txtFSAdminComments.Clear()
            txtIAIPAdminStatus.Clear()
            txtGECOAdminStatus.Clear()
            dtpFeeAdminStatusDate.Value = Today
            rdbActiveAdmin.Checked = False
            rdbInactiveStatus.Checked = False
            txtFSAdminUpdatingUser.Clear()
            dtpFSAdminUpdate.Value = Today
            dtpFSAdminCreateDateTime.Value = Today
            rdbLetterMailedTrue.Checked = False
            rdbLetterMailedFalse.Checked = False
            dtpLetterMailed.Value = Today

            txtContactFirstName.Clear()
            txtContactLastName.Clear()
            txtContactPrefix.Clear()
            txtContactTitle.Clear()
            txtContactCoName.Clear()
            txtContactAddress.Clear()
            txtContactCity.Clear()
            txtContactState.Clear()
            mtbContactZipCode.Clear()
            txtContactAddress2.Clear()
            txtContactEmail.Clear()
            txtInitialFacilityName.Clear()
            txtInitailFacilityAddress.Clear()
            txtInitialAddressLine2.Clear()
            txtInitialCity.Clear()
            mtbInitialZipCode.Clear()
            cboInitialOpStatus.SelectedValue = FacilityOperationalStatus.U
            cboInitialClassification.SelectedValue = FacilityClassification.Unspecified
            rdbInitialNSPSTrue.Checked = False
            rdbInitialNSPSFalse.Checked = False
            rdbInitialPart70True.Checked = False
            rdbInitialPart70False.Checked = False
            txtInitialFacilityComment.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadAdminData()
        If Me.AirsNumber.ToString Is Nothing OrElse Me.FeeYear Is Nothing Then
            Exit Sub
        End If

        Try
            Dim OpStatus As String = ""
            Dim itemClassification As String = ""

            ClearAdminData()

            txtAIRSNumber.Text = Me.AirsNumber.FormattedString
            txtYear.Text = Me.FeeYear

            txtFeeAdminFacilityName.Text = DAL.GetFacilityName(Me.AirsNumber)

            Dim enable As Boolean = True
            If txtFeeAdminFacilityName.Text Is Nothing OrElse txtFeeAdminFacilityName.Text = "" Then
                enable = False
            End If

            btnUpdateFSAdmin.Enabled = enable
            btnAddFSAdmin.Enabled = enable
            btnSaveNewFeeAudit.Enabled = enable
            btnEditFeeAudit.Enabled = enable
            btnAddNewInvoice.Enabled = enable
            btnVOIDInvoice.Enabled = enable
            btnVOIDAllUnpaid.Enabled = enable
            btnRemoveVOID.Enabled = enable
            btnTransactionNew.Enabled = enable
            btnTransactionUpdate.Enabled = enable
            btnTransactionDelete.Enabled = enable

            MailoutEditContactButton.Enabled = False
            MailoutEditFacilityButton.Enabled = False
            MailoutReplaceContactWithFeeContactButton.Enabled = False
            MailoutReplaceFacilityInfoButton.Enabled = False

            Dim SQL As String = "Select " &
            "strEnrolled, datInitialEnrollment, " &
            "datEnrollment, strInitialMailOut, " &
            "strMailOutSent, datMailOutSent, " &
            "intSubmittal, datSubmittal, " &
            "numCurrentStatus, " &
            "strIAIPDesc, strGECODesc, " &
            "datStatusDate, " &
            "strComment, " &
            "FS_Admin.active, " &
            "FS_Admin.updateUser, " &
            "FS_Admin.updateDateTime, " &
            "FS_Admin.CreateDateTime " &
            "From FS_Admin left join FSLK_ADMIN_Status  " &
            "on FS_Admin.numCurrentStatus = FSLK_Admin_Status.ID " &
            "and numFeeYear = @numFeeYear " &
            "and strAIRSNumber = @strAIRSNumber "

            Dim params As SqlParameter() = {
                New SqlParameter("@numFeeYear", Me.FeeYear),
                New SqlParameter("@strAIRSNumber", AirsNumber.DbFormattedString)
            }

            Dim dr As DataRow = DB.GetDataRow(SQL, params)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strEnrolled")) Then
                    rdbEnrolledTrue.Checked = False
                    rdbEnrolledFalse.Checked = False
                Else
                    If dr.Item("strEnrolled") = "1" Then
                        rdbEnrolledTrue.Checked = True
                    Else
                        rdbEnrolledFalse.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datInitialEnrollment")) Then
                    dtpEnrollmentInitial.Text = OracleDate
                Else
                    dtpEnrollmentInitial.Text = dr.Item("datInitialEnrollment")
                End If
                If IsDBNull(dr.Item("datEnrollment")) Then
                    dtpEnrollmentDate.Text = OracleDate
                Else
                    dtpEnrollmentDate.Text = dr.Item("datEnrollment")
                End If
                If IsDBNull(dr.Item("strInitialMailOut")) Then
                    rdbMailoutTrue.Checked = False
                    rdbMailoutFalse.Checked = False
                Else
                    If dr.Item("strInitialMailOut") = "1" Then
                        rdbMailoutTrue.Checked = True
                    Else
                        rdbMailoutFalse.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strMailOutSent")) Then
                    rdbLetterMailedTrue.Checked = False
                    rdbLetterMailedFalse.Checked = False
                Else
                    If dr.Item("strMailOutSent") = "1" Then
                        rdbLetterMailedTrue.Checked = True
                    Else
                        rdbLetterMailedFalse.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datMailOutSent")) Then
                    dtpLetterMailed.Text = OracleDate
                Else
                    dtpLetterMailed.Text = dr.Item("datMailOutSent")
                End If
                If IsDBNull(dr.Item("intSubmittal")) Then
                    rdbSubmittalTrue.Checked = False
                    rdbSubmittalFalse.Checked = False
                Else
                    If dr.Item("intSubmittal") = "1" Then
                        rdbSubmittalTrue.Checked = True
                    Else
                        rdbSubmittalFalse.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datSubmittal")) Then
                    dtpSubmittalDate.Text = OracleDate
                Else
                    dtpSubmittalDate.Text = dr.Item("datSubmittal")
                End If
                If IsDBNull(dr.Item("strIAIPDesc")) Then
                    txtIAIPAdminStatus.Clear()
                Else
                    txtIAIPAdminStatus.Text = dr.Item("strIAIPDesc")
                End If
                If IsDBNull(dr.Item("strGECODesc")) Then
                    txtGECOAdminStatus.Clear()
                Else
                    txtGECOAdminStatus.Text = dr.Item("strGECODesc")
                End If
                If IsDBNull(dr.Item("datStatusDate")) Then
                    dtpFeeAdminStatusDate.Text = OracleDate
                Else
                    dtpFeeAdminStatusDate.Text = dr.Item("datStatusDate")
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtFSAdminComments.Clear()
                Else
                    txtFSAdminComments.Text = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("Active")) Then
                    rdbActiveAdmin.Checked = False
                    rdbInactiveStatus.Checked = False
                Else
                    If dr.Item("Active") = "0" Then
                        rdbInactiveStatus.Checked = True
                    Else
                        rdbActiveAdmin.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("UpdateUser")) Then
                    txtFSAdminUpdatingUser.Clear()
                Else
                    txtFSAdminUpdatingUser.Text = Replace(Replace(dr.Item("UpDateUser"), "IAIP||", "IAIP - "), "GECO||", "GECO - ")
                End If
                If IsDBNull(dr.Item("updateDateTime")) Then
                    dtpFSAdminUpdate.Text = OracleDate
                Else
                    dtpFSAdminUpdate.Text = dr.Item("UpDateDateTime")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    dtpFSAdminCreateDateTime.Text = OracleDate
                Else
                    dtpFSAdminCreateDateTime.Text = dr.Item("CreateDateTime")
                End If
            End If

            SQL = "Select " &
            "strFirstName, strLastName, " &
            "strPrefix, strTitle, " &
            "strContactCoName, strContactAddress1, " &
            "strContactAddress2, strContactCity, " &
            "strContactState, strContactZipCode, " &
            "strGECOUserEmail, strOperationalStatus, " &
            "strClass, strNSPS, " &
            "strPart70, strFacilityName, " &
            "strFacilityAddress1, strFacilityAddress2, " &
            "strFacilityCity, strFacilityZipCode, " &
            "strComment, " &
            "datShutDownDate, " &
            "Active " &
            "from FS_MailOut " &
            "where numFeeYear = @numFeeYear " &
            "and strAIRSNumber = @strAIRSNumber "

            dr = DB.GetDataRow(SQL, params)

            If dr IsNot Nothing Then
                MailoutEditContactButton.Enabled = True
                MailoutEditFacilityButton.Enabled = True
                MailoutReplaceContactWithFeeContactButton.Enabled = True
                MailoutReplaceFacilityInfoButton.Enabled = True

                If IsDBNull(dr.Item("strFirstName")) Then
                    txtContactFirstName.Clear()
                Else
                    txtContactFirstName.Text = dr.Item("strFirstName")
                End If
                If IsDBNull(dr.Item("strLastname")) Then
                    txtContactLastName.Clear()
                Else
                    txtContactLastName.Text = dr.Item("strlastName")
                End If
                If IsDBNull(dr.Item("strPrefix")) Then
                    txtContactPrefix.Clear()
                Else
                    txtContactPrefix.Text = dr.Item("strPrefix")
                End If
                If IsDBNull(dr.Item("strTitle")) Then
                    txtContactTitle.Clear()
                Else
                    txtContactTitle.Text = dr.Item("strTitle")
                End If
                If IsDBNull(dr.Item("strContactCoName")) Then
                    txtContactCoName.Clear()
                Else
                    txtContactCoName.Text = dr.Item("strContactCoName")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtContactAddress.Clear()
                Else
                    txtContactAddress.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactAddress2")) Then
                    txtContactAddress2.Clear()
                Else
                    txtContactAddress2.Text = dr.Item("strContactAddress2")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtContactCity.Clear()
                Else
                    txtContactCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    txtContactState.Clear()
                Else
                    txtContactState.Text = dr.Item("strContactState")
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    mtbContactZipCode.Clear()
                Else
                    mtbContactZipCode.Text = dr.Item("strContactZipCode")
                End If
                If IsDBNull(dr.Item("strGECOUserEmail")) Then
                    txtContactEmail.Clear()
                Else
                    txtContactEmail.Text = dr.Item("strGECOUserEmail")
                End If
                If IsDBNull(dr.Item("strOperationalStatus")) Then
                    cboInitialOpStatus.SelectedValue = FacilityOperationalStatus.U
                Else
                    OpStatus = dr.Item("strOperationalStatus")
                    Select Case OpStatus
                        Case "O"
                            cboInitialOpStatus.SelectedValue = FacilityOperationalStatus.O
                        Case "P"
                            cboInitialOpStatus.SelectedValue = FacilityOperationalStatus.P
                        Case "C"
                            cboInitialOpStatus.SelectedValue = FacilityOperationalStatus.C
                        Case "T"
                            cboInitialOpStatus.SelectedValue = FacilityOperationalStatus.T
                        Case "X"
                            cboInitialOpStatus.SelectedValue = FacilityOperationalStatus.X
                        Case "I"
                            cboInitialOpStatus.SelectedValue = FacilityOperationalStatus.I
                        Case Else
                            cboInitialOpStatus.SelectedValue = FacilityOperationalStatus.U
                    End Select
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    cboInitialClassification.SelectedValue = FacilityClassification.Unspecified
                Else
                    itemClassification = dr.Item("strClass")
                    Select Case itemClassification
                        Case "A"
                            cboInitialClassification.SelectedValue = FacilityClassification.A
                        Case "B"
                            cboInitialClassification.SelectedValue = FacilityClassification.B
                        Case "SM"
                            cboInitialClassification.SelectedValue = FacilityClassification.SM
                        Case "PR"
                            cboInitialClassification.SelectedValue = FacilityClassification.PR
                        Case "C"
                            cboInitialClassification.SelectedValue = FacilityClassification.C
                        Case Else
                            cboInitialClassification.SelectedValue = FacilityClassification.Unspecified
                    End Select
                End If
                If IsDBNull(dr.Item("strNSPS")) Then
                    rdbInitialNSPSTrue.Checked = False
                    rdbInitialNSPSFalse.Checked = False
                Else
                    If dr.Item("strNSPS") = True Then
                        rdbInitialNSPSTrue.Checked = True
                    Else
                        rdbInitialNSPSFalse.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strPart70")) Then
                    rdbInitialPart70True.Checked = False
                    rdbInitialPart70False.Checked = False
                Else
                    If dr.Item("strPart70") = True Then
                        rdbInitialPart70True.Checked = True
                    Else
                        rdbInitialPart70False.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtInitialFacilityName.Clear()
                Else
                    txtInitialFacilityName.Text = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strFacilityAddress1")) Then
                    txtInitailFacilityAddress.Clear()
                Else
                    txtInitailFacilityAddress.Text = dr.Item("strFacilityAddress1")
                End If
                If IsDBNull(dr.Item("strFacilityAddress2")) Then
                    txtInitialAddressLine2.Clear()
                Else
                    txtInitialAddressLine2.Text = dr.Item("strFacilityAddress2")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    txtInitialCity.Clear()
                Else
                    txtInitialCity.Text = dr.Item("strFacilityCity")
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    mtbInitialZipCode.Clear()
                Else
                    mtbInitialZipCode.Text = dr.Item("strFacilityZipCode")
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtInitialFacilityComment.Clear()
                Else
                    txtInitialFacilityComment.Text = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("datShutDownDate")) Then
                    dtpInitialShutDownDate.Checked = False
                Else
                    dtpInitialShutDownDate.Value = dr.Item("datShutDownDate")
                End If

            End If

            txtContactFirstName.Enabled = False
            txtContactLastName.Enabled = False
            txtContactPrefix.Enabled = False
            txtContactTitle.Enabled = False
            txtContactCoName.Enabled = False
            txtContactAddress.Enabled = False
            txtContactCity.Enabled = False
            txtContactState.Enabled = False
            mtbContactZipCode.Enabled = False
            txtContactAddress2.Enabled = False
            txtContactEmail.Enabled = False
            txtInitialFacilityName.Enabled = False
            txtInitailFacilityAddress.Enabled = False
            txtInitialAddressLine2.Enabled = False
            txtInitialCity.Enabled = False
            mtbInitialZipCode.Enabled = False
            cboInitialOpStatus.Enabled = False
            cboInitialClassification.Enabled = False
            MailoutInitialNspsPanel.Enabled = False
            MailoutInitialPart70Panel.Enabled = False
            txtInitialFacilityComment.Enabled = False

            SQL = "Select " &
            "strContactFirstName, strContactlastName, " &
            "strContactPrefix, strContactTitle, " &
            "strContactCompanyName, strContactAddress, " &
            "strContactCity, strContactState, " &
            "strContactZipCode, strContactPhoneNumber, " &
            "strContactFaxNumber, strContactEmail, " &
            "strComment " &
            "from FS_ContactInfo " &
            "where numFeeYear = @numFeeYear " &
            "and strAIRSNumber = @strAIRSNumber "

            dr = DB.GetDataRow(SQL, params)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    txtGECOContactFirstName.Clear()
                Else
                    txtGECOContactFirstName.Text = dr.Item("strContactFirstName")
                End If

                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtGECOContactLastName.Clear()
                Else
                    txtGECOContactLastName.Text = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtGECOContactSalutation.Clear()
                Else
                    txtGECOContactSalutation.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactTitle")) Then
                    txtGECOContactTitle.Clear()
                Else
                    txtGECOContactTitle.Text = dr.Item("strContactTitle")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtGECOContactCompanyName.Clear()
                Else
                    txtGECOContactCompanyName.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactAddress")) Then
                    txtGECOContactStreetAddress.Clear()
                Else
                    txtGECOContactStreetAddress.Text = dr.Item("strContactAddress")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtGECOContactCity.Clear()
                Else
                    txtGECOContactCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    txtGECOContactState.Clear()
                Else
                    txtGECOContactState.Text = dr.Item("strContactState")
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    mtbGECOContactZipCode.Clear()
                Else
                    mtbGECOContactZipCode.Text = dr.Item("strContactZipCode")
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber")) Then
                    mtbGECOContactPhontNumber.Clear()
                Else
                    mtbGECOContactPhontNumber.Text = dr.Item("strContactPhoneNumber")
                End If
                If IsDBNull(dr.Item("strContactFaxNumber")) Then
                    mtbGECOContactFaxNumber.Clear()
                Else
                    mtbGECOContactFaxNumber.Text = dr.Item("strContactFaxNumber")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtGECOContactEmail.Clear()
                Else
                    txtGECOContactEmail.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtGECOContactComments.Clear()
                Else
                    txtGECOContactComments.Text = dr.Item("strComment")
                End If
            End If

            LoadFeeInvoiceData()
            LoadTransactionData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadFeeInvoiceData()
        Dim temp As String
        Try
            Dim SQL As String = "select " &
            "FS_FEEDATA.NUMFEEYEAR, " &
            "FS_FEEDATA.STRAIRSNUMBER, " &
            "STRSYNTHETICMINOR, NUMSMFEE, " &
            "STRPART70, NUMPART70FEE, " &
            "INTVOCTONS, INTPMTONS, " &
            "INTSO2TONS, INTNOXTONS, " &
            "NUMCALCULATEDFEE, NUMFEERATE, " &
            "STRNSPS, NUMNSPSFEE, " &
            "STRNSPSEXEMPT, STRNSPSEXEMPTREASON, " &
            "NUMADMINFEE, NUMTOTALFEE, " &
            "STRCLASS, STROPERATE, " &
            "DATSHUTDOWN, STROFFICIALNAME, " &
            "strOfficialTitle, " &
            "strPaymentPlan, STRCONFIRMATIONNUMBER, " &
            "FS_FEEDATA.strComment, " &
            "updateUser, " &
            "UpdateDateTime, CreateDateTime " &
            "from FS_FEEDATA " &
            "where numFeeYear = @numFeeYear " &
            "and strAIRSNumber = @strAIRSNumber "

            Dim params As SqlParameter() = {
                New SqlParameter("@numFeeYear", Me.FeeYear),
                New SqlParameter("@strAIRSNumber", AirsNumber.DbFormattedString)
            }

            Dim dr As DataRow = DB.GetDataRow(SQL, params)

            txtGECOExceptions.Clear()
            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strClass")) Then
                    txtInvoiceClassification.Clear()
                    txtGECOClass.Clear()
                Else
                    txtInvoiceClassification.Text = dr.Item("strClass")
                    txtGECOClass.Text = txtInvoiceClassification.Text
                End If
                If IsDBNull(dr.Item("strOperate")) Then
                    chbInvoiceDataOperating.Checked = False
                    txtGECOOpStatus.Clear()
                Else
                    If dr.Item("strOperate") = "1" Then
                        chbInvoiceDataOperating.Checked = True
                        txtGECOOpStatus.Text = "True"
                    Else
                        chbInvoiceDataOperating.Checked = False
                        txtGECOOpStatus.Text = "False"
                    End If
                End If
                If IsDBNull(dr.Item("strNSPS")) Then
                    chbInvoiceDataNSPS.Checked = False
                    txtGECONSPS.Clear()
                Else
                    If dr.Item("strNSPS") = "1" Then
                        chbInvoiceDataNSPS.Checked = True
                        txtGECONSPS.Text = "True"
                    Else
                        chbInvoiceDataNSPS.Checked = False
                        txtGECONSPS.Text = "False"
                    End If
                End If
                If IsDBNull(dr.Item("STRNSPSEXEMPTREASON")) Then
                    txtInvoiceDataNSPSExempts.Clear()
                    txtGECOExceptions.Clear()
                Else
                    txtInvoiceDataNSPSExempts.Text = dr.Item("STRNSPSEXEMPTREASON")
                    txtGECOExceptions.Text = dr.Item("STRNSPSEXEMPTREASON")
                End If

                If IsDBNull(dr.Item("strPart70")) Then
                    chbInvoiceDataPart70.Checked = False
                    txtGECOPart70.Clear()
                Else
                    If dr.Item("strPart70") = "1" Then
                        chbInvoiceDataPart70.Checked = True
                        txtGECOPart70.Text = "True"
                    Else
                        chbInvoiceDataPart70.Checked = False
                        txtGECOPart70.Text = "False"
                    End If
                End If
                If IsDBNull(dr.Item("strSyntheticMinor")) Then
                    chbInvoiceDataSyntheticMinor.Checked = False
                    txtGECOSM.Clear()
                Else
                    If dr.Item("strSyntheticMinor") = "1" Then
                        chbInvoiceDataSyntheticMinor.Checked = True
                        txtGECOSM.Text = "True"
                    Else
                        chbInvoiceDataSyntheticMinor.Checked = False
                        txtGECOSM.Text = "False"
                    End If
                End If
                If IsDBNull(dr.Item("intVOCTons")) Then
                    txtInvoiceDataVOCTons.Clear()
                    txtGECOVOCTons.Clear()
                Else
                    txtInvoiceDataVOCTons.Text = dr.Item("intVOCTons")
                    txtGECOVOCTons.Text = txtInvoiceDataVOCTons.Text
                End If
                If IsDBNull(dr.Item("intPMTons")) Then
                    txtInvoiceDataPMTons.Clear()
                    txtGECOPMTons.Clear()
                Else
                    txtInvoiceDataPMTons.Text = dr.Item("intPMTons")
                    txtGECOPMTons.Text = txtInvoiceDataPMTons.Text
                End If
                If IsDBNull(dr.Item("intSO2Tons")) Then
                    txtInvoiceDataSO2Tons.Clear()
                    txtGECOSO2Tons.Clear()
                Else
                    txtInvoiceDataSO2Tons.Text = dr.Item("intSO2Tons")
                    txtGECOSO2Tons.Text = txtInvoiceDataSO2Tons.Text
                End If
                If IsDBNull(dr.Item("intNOxTons")) Then
                    txtInvoiceDataNOxTons.Clear()
                    txtGECONOxTons.Clear()
                Else
                    txtInvoiceDataNOxTons.Text = dr.Item("intNOxTons")
                    txtGECONOxTons.Text = txtInvoiceDataNOxTons.Text
                End If
                If IsDBNull(dr.Item("numFeeRate")) Then
                    txtInvoiceDataFeeRate.Clear()
                    txtGECOFeeRate.Clear()
                Else
                    txtInvoiceDataFeeRate.Text = Format(dr.Item("numFeeRate"), "c")
                    txtGECOFeeRate.Text = txtInvoiceDataFeeRate.Text
                End If

                'NUMCALCULATEDFEE
                If IsDBNull(dr.Item("NUMCALCULATEDFEE")) Then
                    txtGECOCalculatedFee.Clear()
                Else
                    txtGECOCalculatedFee.Text = Format(dr.Item("NUMCALCULATEDFEE"), "c")
                End If

                If IsDBNull(dr.Item("numPart70Fee")) Then
                    txtInvoiceDataPart70Fee.Clear()
                    txtGECOPart70Fee.Clear()
                Else
                    txtInvoiceDataPart70Fee.Text = Format(dr.Item("numPart70Fee"), "c")
                    txtGECOPart70Fee.Text = txtInvoiceDataPart70Fee.Text
                End If
                If IsDBNull(dr.Item("numSMFee")) Then
                    txtInvoiceDataSMFee.Clear()
                    txtGECOSMFee.Clear()
                Else
                    txtInvoiceDataSMFee.Text = Format(dr.Item("numSMFee"), "c")
                    txtGECOSMFee.Text = txtInvoiceDataSMFee.Text
                End If
                If IsDBNull(dr.Item("numNSPSFee")) Then
                    txtInvoiceDataNSPSFee.Clear()
                    txtGECONSPSFee.Clear()
                Else
                    txtInvoiceDataNSPSFee.Text = Format(dr.Item("numNSPSFee"), "c")
                    txtGECONSPSFee.Text = txtInvoiceDataNSPSFee.Text
                End If
                If IsDBNull(dr.Item("numAdminFee")) Then
                    txtInvoiceDataAdminFee.Clear()
                    txtGECOAdminFee.Clear()
                Else
                    txtInvoiceDataAdminFee.Text = Format(dr.Item("numAdminFee"), "c")
                    txtGECOAdminFee.Text = txtInvoiceDataAdminFee.Text
                End If
                If IsDBNull(dr.Item("numTotalFee")) Then
                    txtInvoiceDataTotalFees.Clear()
                    txtGECOTotalFees.Clear()
                Else
                    txtInvoiceDataTotalFees.Text = Format(dr.Item("numTotalFee"), "c")
                    txtGECOTotalFees.Text = txtInvoiceDataTotalFees.Text
                End If
                If IsDBNull(dr.Item("strNSPSExempt")) Then
                    chbInvoiceDataNSPSExempt.Checked = False
                    txtGECONSPSExempt.Clear()
                Else
                    If dr.Item("strNSPSExempt") = "1" Then
                        chbInvoiceDataNSPSExempt.Checked = True
                        txtGECONSPSExempt.Text = "True"
                    Else
                        chbInvoiceDataNSPSExempt.Checked = False
                        txtGECONSPSExempt.Text = "False"
                    End If
                End If
                If IsDBNull(dr.Item("strOfficialName")) Then
                    txtInvoiceDataOfficialName.Clear()
                    txtGECOOfficialName.Clear()
                Else
                    txtInvoiceDataOfficialName.Text = dr.Item("strOfficialName")
                    txtGECOOfficialName.Text = txtInvoiceDataOfficialName.Text
                End If
                If IsDBNull(dr.Item("strOfficialTitle")) Then
                    txtInvoiceDataOfficialTitle.Clear()
                    txtGECOOfficialTitle.Clear()
                Else
                    txtInvoiceDataOfficialTitle.Text = dr.Item("strOfficialTitle")
                    txtGECOOfficialTitle.Text = txtInvoiceDataOfficialTitle.Text
                End If
                If IsDBNull(dr.Item("strconfirmationNumber")) Then
                    txtInvoiceDataConfirmationNumber.Clear()
                Else
                    txtInvoiceDataConfirmationNumber.Text = dr.Item("strConfirmationNumber")
                End If
                If IsDBNull(dr.Item("strPaymentPlan")) Then
                    txtInvoiceDataPaymentType.Clear()
                    txtGECOPaymentType.Clear()
                Else
                    txtInvoiceDataPaymentType.Text = dr.Item("strPaymentPlan")
                    txtGECOPaymentType.Text = txtInvoiceDataPaymentType.Text
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtInvoiceDataGECOComments.Clear()
                Else
                    txtInvoiceDataGECOComments.Text = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("updateUser")) Then
                    txtInvoiceDataUpdate.Clear()
                Else
                    txtInvoiceDataUpdate.Text = dr.Item("UpdateUser")
                End If
                If IsDBNull(dr.Item("updateDateTime")) Then
                    dtpInvoiceDataDateUpdated.Text = OracleDate
                Else
                    dtpInvoiceDataDateUpdated.Text = dr.Item("UpdateDateTime")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    dtpInvoiceDataCreatedDate.Text = OracleDate
                Else
                    dtpInvoiceDataCreatedDate.Text = dr.Item("CreateDateTime")
                End If
                If IsDBNull(dr.Item("DATSHUTDOWN")) Then
                    txtGECOShutDown.Clear()
                Else
                    txtGECOShutDown.Text = dr.Item("DATSHUTDOWN")
                End If
            End If

            Dim SQLLine As String = ""
            If txtGECOExceptions.Text <> "" Then
                Do While txtGECOExceptions.Text <> ""
                    If txtGECOExceptions.Text.Contains(",") Then
                        temp = Mid(txtGECOExceptions.Text, 1, InStr(txtGECOExceptions.Text, ",", CompareMethod.Text) - 1)
                        txtGECOExceptions.Text = Replace(txtGECOExceptions.Text, temp & ",", "")
                    Else
                        temp = txtGECOExceptions.Text
                        txtGECOExceptions.Text = Replace(txtGECOExceptions.Text, temp, "")
                    End If
                    SQLLine = SQLLine & " NSPSReasonCode = '" & temp & "' or "
                Loop
                If SQLLine <> "" Then
                    SQLLine = Mid(SQLLine, 1, SQLLine.Length - 3)
                End If

                SQL = "Select Description " &
                "from FSLK_NSPSReason " &
                "where " & SQLLine
                Dim desc As String = DB.GetSingleValue(Of String)(SQL)
                If Not String.IsNullOrEmpty(desc) Then
                    txtGECOExceptions.Text = txtGECOExceptions.Text & "- " & desc & vbCrLf & vbCrLf
                End If
            End If

            SQLLine = ""

            SQL = "Select " &
            "strNonAttainment " &
            "from LookUpCountyInformation " &
            "where strCountyCode = @strCountyCode "

            Dim param As New SqlParameter("@strCountyCode", AirsNumber.CountySubstring)

            temp = DB.GetSingleValue(Of String)(SQL, param)
            If String.IsNullOrEmpty(temp) Then
                chbInvoicedataNonAttainment.Checked = False
            Else
                If Mid(temp, 2, 1) = "1" Then
                    chbInvoicedataNonAttainment.Checked = True
                Else
                    chbInvoicedataNonAttainment.Checked = False
                End If
            End If

            Dim NSPStemp As String = ""

            If chbInvoiceDataNSPSExempt.Checked = True And txtInvoiceDataNSPSExempts.Text <> "" Then
                NSPStemp = txtInvoiceDataNSPSExempts.Text
                Do While NSPStemp <> ""
                    If NSPStemp.Contains(",") Then
                        temp = Mid(NSPStemp, 1, InStr(NSPStemp, ",", CompareMethod.Text) - 1)
                        NSPStemp = Replace(NSPStemp, temp & ",", "")
                    Else
                        temp = NSPStemp
                        NSPStemp = Replace(NSPStemp, temp, "")
                    End If
                    SQLLine = SQLLine & "  NSPSReasonCode = '" & temp & "' or "
                Loop

                If SQLLine <> "" Then
                    SQLLine = Mid(SQLLine, 1, SQLLine.Length - 3)
                End If

                SQL = "Select Description " &
                "from FSLK_NSPSReason " &
                "where " & SQLLine

                dgvInvoiceDataNSPSExemptions.DataSource = DB.GetDataTable(SQL)

                dgvInvoiceDataNSPSExemptions.RowHeadersVisible = False
                dgvInvoiceDataNSPSExemptions.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvInvoiceDataNSPSExemptions.AllowUserToResizeColumns = True
                dgvInvoiceDataNSPSExemptions.AllowUserToResizeRows = True
                dgvInvoiceDataNSPSExemptions.AllowUserToAddRows = False
                dgvInvoiceDataNSPSExemptions.AllowUserToDeleteRows = False
                dgvInvoiceDataNSPSExemptions.AllowUserToOrderColumns = True
                dgvInvoiceDataNSPSExemptions.Columns("Description").HeaderText = "NSPS Exemption Reason"
                dgvInvoiceDataNSPSExemptions.Columns("Description").DisplayIndex = 0
                dgvInvoiceDataNSPSExemptions.Columns("Description").Width = dgvInvoiceDataNSPSExemptions.Width
            Else
                dgvInvoiceDataNSPSExemptions.DataSource = Nothing
            End If

            SQL = "Select " &
            "InvoiceID, numAmount, " &
            "datInvoiceDate, strComment, " &
            "strPayTypeDesc, " &
            "case " &
            "when strInvoiceStatus = '1' then 'Paid' " &
            "else 'Unpaid' " &
            "end as InvoiceStatus " &
            "from FS_FeeInvoice inner join FSLK_PayType " &
            "on FS_FeeInvoice.strPaytype = FSLK_PayType.numpaytypeid " &
            "where numFeeYear = @numFeeYear " &
            "and strAIRSNumber = @strAIRSNumber " &
            "and FS_FeeInvoice.Active = '1' "

            dgvInvoiceData.DataSource = DB.GetDataTable(SQL, params)

            dgvInvoiceData.RowHeadersVisible = False
            dgvInvoiceData.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvInvoiceData.AllowUserToResizeColumns = True
            dgvInvoiceData.AllowUserToResizeRows = True
            dgvInvoiceData.AllowUserToAddRows = False
            dgvInvoiceData.AllowUserToDeleteRows = False
            dgvInvoiceData.AllowUserToOrderColumns = True
            dgvInvoiceData.Columns("InvoiceID").HeaderText = "Invoice ID"
            dgvInvoiceData.Columns("InvoiceID").DisplayIndex = 0
            dgvInvoiceData.Columns("InvoiceID").Width = dgvInvoiceData.Width * 0.075

            dgvInvoiceData.Columns("numAmount").HeaderText = "Invoice Amount"
            dgvInvoiceData.Columns("numAmount").DisplayIndex = 1
            dgvInvoiceData.Columns("numAmount").Width = dgvInvoiceData.Width * 0.15
            dgvInvoiceData.Columns("numAmount").DefaultCellStyle.Format = "c"
            dgvInvoiceData.Columns("strPayTypeDesc").HeaderText = "Invoice Type"
            dgvInvoiceData.Columns("strPayTypeDesc").DisplayIndex = 2
            dgvInvoiceData.Columns("strPayTypeDesc").Width = dgvInvoiceData.Width * 0.15
            dgvInvoiceData.Columns("datInvoiceDate").HeaderText = "Invoiced Date"
            dgvInvoiceData.Columns("datInvoiceDate").DisplayIndex = 3
            dgvInvoiceData.Columns("datInvoiceDate").Width = dgvInvoiceData.Width * 0.15
            dgvInvoiceData.Columns("datInvoiceDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvInvoiceData.Columns("InvoiceStatus").HeaderText = "Invoice Status"
            dgvInvoiceData.Columns("InvoiceStatus").DisplayIndex = 4
            dgvInvoiceData.Columns("InvoiceStatus").Width = dgvInvoiceData.Width * 0.15
            dgvInvoiceData.Columns("strComment").HeaderText = "GECO Comments"
            dgvInvoiceData.Columns("strComment").DisplayIndex = 5
            dgvInvoiceData.Columns("strComment").Width = dgvInvoiceData.Width * 0.45

            rdbInvoiceDataPaidStatus.Checked = True
            For i As Integer = 0 To dgvInvoiceData.RowCount - 1
                temp = dgvInvoiceData(5, i).Value
                If dgvInvoiceData(5, i).Value = "Unpaid" Then
                    rdbInvoiceDataUnpaidStatus.Checked = True
                End If
            Next

            If dgvInvoiceData.RowCount = 0 Then
                rdbInvoiceDataPaidStatus.Checked = False
                rdbInvoiceDataUnpaidStatus.Checked = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub LoadTransactionData()
        Try
            Dim SQL As String = "select " &
            "TRANSACTIONID,  INVOICES.INVOICEID, DATTRANSACTIONDATE, " &
            "NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, STRBATCHNO, " &
            "ENTRYPERSON, " &
            "STRCOMMENT, STRCREDITCARDNO, TRANSACTIONTYPECODE, " &
            "case " &
            "when TRANSACTIONS.UPDATEUSER is not null then (STRLASTNAME+', '+STRFIRSTNAME) " &
            "else '' " &
            "end  UpdateUser, " &
            "TRANSACTIONS.UPDATEDATETIME, " &
            "TRANSACTIONS.CREATEDATETIME, " &
            "strPayTypedesc " &
            " from " &
            "(select " &
            "TRANSACTIONID,  INVOICEID, DATTRANSACTIONDATE, " &
            "NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, STRBATCHNO, " &
            "(STRLASTNAME+', '+STRFIRSTNAME) as ENTRYPERSON, " &
            "STRCOMMENT, strCreditcardno, " &
            "transactiontypecode, " &
            "UPDATEUSER, UPDATEDATETIME, " &
            "createDateTime, strairsnumber, numfeeyear " &
            "from FS_TRANSACTIONS left join EPDUSERPROFILES " &
            "on FS_TRANSACTIONS.STRENTRYPERSON = EPDUSERPROFILES.NUMUSERID " &
            "where FS_TRANSACTIONS.STRAIRSNUMBER = @airs " &
            "and FS_TRANSACTIONS.NUMFEEYEAR = @year " &
            "and active = 1) as TRANSACTIONS left join  " &
            "(select " &
            "INVOICEID, " &
            "FS_feeINVOICE.UPDATEUSER, FS_feeINVOICE.UPDATEDATETIME, " &
            "FS_feeINVOICE.CREATEDATETIME, STRAIRSNUMBER, NUMFEEYEAR, strpaytypedesc " &
            "from FS_feeINVOICE inner join FSLK_PayType " &
            "on FS_FeeInvoice.strPayType = FSLK_PayType.numPayTypeID " &
            "where STRAIRSNUMBER = @airs " &
            "and NUMFEEYEAR = @year " &
            "and FS_feeINVOICE.Active = '1' ) as INVOICES " &
            "on TRANSACTIONS.STRAIRSNUMBER  =  INVOICES.STRAIRSNUMBER " &
            "and TRANSACTIONS.NUMFEEYEAR  =  INVOICES.NUMFEEYEAR  " &
            "and TRANSACTIONS.INVOICEID  =  INVOICES.INVOICEID  " &
            "left join EPDUSERPROFILES " &
            "on TRANSACTIONS.UPDATEUSER  = epduserProfiles.numUserID   " &
            " union " &
            "select " &
            "TRANSACTIONID,  INVOICES.INVOICEID, DATTRANSACTIONDATE, " &
            "NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, STRBATCHNO, " &
            "ENTRYPERSON, " &
            "STRCOMMENT, STRCREDITCARDNO, TRANSACTIONTYPECODE, " &
            "case " &
            "when TRANSACTIONS.UPDATEUSER is not null then (STRLASTNAME+', '+STRFIRSTNAME) " &
            "else '' " &
            "end  UpdateUser, " &
            "TRANSACTIONS.UPDATEDATETIME, " &
            "TRANSACTIONS.CREATEDATETIME, strPayTypeDesc " &
            " from " &
            "(select " &
            "TRANSACTIONID,  INVOICEID, DATTRANSACTIONDATE, " &
            "NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, STRBATCHNO, " &
            "(STRLASTNAME+', '+STRFIRSTNAME) as ENTRYPERSON, " &
            "STRCOMMENT, strCreditcardno, " &
            "transactiontypecode, " &
            "UPDATEUSER, UPDATEDATETIME, " &
            "createDateTime, strairsnumber, numfeeyear " &
            "from FS_TRANSACTIONS left join EPDUSERPROFILES " &
            "on FS_TRANSACTIONS.STRENTRYPERSON = EPDUSERPROFILES.NUMUSERID " &
            "and FS_TRANSACTIONS.STRAIRSNUMBER = @airs " &
            "and FS_TRANSACTIONS.NUMFEEYEAR = @year " &
            "and active = 1) as TRANSACTIONS right join  " &
            "(select " &
            "INVOICEID, " &
            "FS_feeINVOICE.UPDATEUSER, FS_feeINVOICE.UPDATEDATETIME, " &
            "FS_feeINVOICE.CREATEDATETIME, STRAIRSNUMBER, NUMFEEYEAR, strPayTypeDesc " &
            "from FS_feeINVOICE, fsLK_Paytype " &
            "where FS_feeINVOICE.strPayType = fsLK_Paytype.numPayTypeID " &
            "and STRAIRSNUMBER = @airs " &
            "and NUMFEEYEAR = @year " &
            "and FS_feeINVOICE.Active = '1') as INVOICES " &
            "on INVOICES.STRAIRSNUMBER  = TRANSACTIONS.STRAIRSNUMBER " &
            "and INVOICES.NUMFEEYEAR  =  TRANSACTIONS.NUMFEEYEAR  " &
            "and INVOICES.INVOICEID  =  TRANSACTIONS.INVOICEID " &
            "left join EPDUSERPROFILES " &
            "on TRANSACTIONS.UPDATEUSER  = epduserProfiles.numUserID "

            Dim params As SqlParameter() = {
                New SqlParameter("@airs", AirsNumber.DbFormattedString),
                New SqlParameter("@year", FeeYear)
            }

            dgvTransactions.DataSource = DB.GetDataTable(SQL, params)

            dgvTransactions.RowHeadersVisible = False
            dgvTransactions.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvTransactions.AllowUserToResizeColumns = True
            dgvTransactions.AllowUserToResizeRows = True
            dgvTransactions.AllowUserToAddRows = False
            dgvTransactions.AllowUserToDeleteRows = False
            dgvTransactions.AllowUserToOrderColumns = True
            dgvTransactions.Columns("transactionID").HeaderText = "Transaction ID"
            dgvTransactions.Columns("transactionID").DisplayIndex = 0
            dgvTransactions.Columns("INVOICEID").HeaderText = "Invoice ID"
            dgvTransactions.Columns("INVOICEID").DisplayIndex = 1
            dgvTransactions.Columns("DATTRANSACTIONDATE").HeaderText = "Transaction Date"
            dgvTransactions.Columns("DATTRANSACTIONDATE").DisplayIndex = 2
            dgvTransactions.Columns("DATTRANSACTIONDATE").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvTransactions.Columns("NUMPAYMENT").HeaderText = "Amount"
            dgvTransactions.Columns("NUMPAYMENT").DisplayIndex = 3
            dgvTransactions.Columns("NUMPAYMENT").DefaultCellStyle.Format = "c"
            dgvTransactions.Columns("strPayTypeDesc").HeaderText = "Invoice Type"
            dgvTransactions.Columns("strPayTypeDesc").DisplayIndex = 4

            dgvTransactions.Columns("STRDEPOSITNO").HeaderText = "Deposit No"
            dgvTransactions.Columns("STRDEPOSITNO").DisplayIndex = 5
            dgvTransactions.Columns("STRBATCHNO").HeaderText = "Batch No."
            dgvTransactions.Columns("STRBATCHNO").DisplayIndex = 6
            dgvTransactions.Columns("ENTRYPERSON").HeaderText = "Entry Person"
            dgvTransactions.Columns("ENTRYPERSON").DisplayIndex = 7
            dgvTransactions.Columns("STRCHECKNO").HeaderText = "Check No"
            dgvTransactions.Columns("STRCHECKNO").DisplayIndex = 8
            dgvTransactions.Columns("strCreditcardno").HeaderText = "Credit No."
            dgvTransactions.Columns("strCreditcardno").DisplayIndex = 9
            dgvTransactions.Columns("strComment").HeaderText = "Comments"
            dgvTransactions.Columns("strComment").DisplayIndex = 10

            dgvTransactions.Columns("TRANSACTIONTYPECODE").HeaderText = "Transaction Type"
            dgvTransactions.Columns("TRANSACTIONTYPECODE").DisplayIndex = 11
            dgvTransactions.Columns("TRANSACTIONTYPECODE").Width = 0
            dgvTransactions.Columns("UPDATEUSER").HeaderText = "Update User"
            dgvTransactions.Columns("UPDATEUSER").DisplayIndex = 12
            dgvTransactions.Columns("UPDATEDATETIME").HeaderText = "Update Time"
            dgvTransactions.Columns("UPDATEDATETIME").DisplayIndex = 13
            dgvTransactions.Columns("UPDATEDATETIME").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvTransactions.Columns("CREATEDATETIME").HeaderText = "Create Time"
            dgvTransactions.Columns("CREATEDATETIME").DisplayIndex = 14
            dgvTransactions.Columns("CREATEDATETIME").DefaultCellStyle.Format = "dd-MMM-yyyy"


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadAuditedData()
        If Me.AirsNumber.ToString Is Nothing OrElse Me.FeeYear Is Nothing Then
            Exit Sub
        End If

        Try
            Dim SQL As String = "WITH cte AS " &
            "(SELECT * FROM FS_FEEAMENDMENT WHERE STRAIRSNUMBER = @airs AND NUMFEEYEAR = @year) " &
            "SELECT " &
            "(SELECT TOP 1 STRSYNTHETICMINOR " &
            "FROM cte " &
            "WHERE STRSYNTHETICMINOR IS NOT NULL " &
            "ORDER BY AuditID DESC " &
            ") AS STRSYNTHETICMINOR, " &
            "(SELECT TOP 1 NUMSMFEE " &
            "FROM cte " &
            "WHERE NUMSMFEE IS NOT NULL " &
            "ORDER BY AuditID DESC " &
            ") AS NUMSMFEE, " &
            "(SELECT TOP 1 STRPART70 " &
            "FROM cte " &
            "WHERE STRPART70 IS NOT NULL " &
            ") AS STRPART70, " &
            "(SELECT TOP 1 NUMPART70FEE " &
            "FROM cte " &
            "WHERE numPart70Fee IS NOT NULL " &
            ") AS NUMPART70FEE, " &
            "(SELECT TOP 1 INTVOCTONS " &
            "FROM cte " &
            "WHERE INtVOCTONS IS NOT NULL " &
            ") AS INTVOCTONS, " &
            "(SELECT TOP 1 INTPMTONS " &
            "FROM cte " &
            "WHERE INTPMTONS IS NOT NULL " &
            ") AS INTPMTONS, " &
            "(SELECT TOP 1 INTSO2TONS " &
            "FROM cte " &
            "WHERE intSO2Tons IS NOT NULL " &
            ") AS INTSO2TONS, " &
            "(SELECT TOP 1 INTNOXTONS " &
            "FROM cte " &
            "WHERE INTNOXTONS IS NOT NULL " &
            ") AS INTNOXTONS, " &
            "(SELECT TOP 1 NUMCALCULATEDFEE " &
            "FROM cte " &
            "WHERE numcalculatedFee IS NOT NULL " &
            ") AS NUMCALCULATEDFEE, " &
            "(SELECT TOP 1 NUMFEERATE " &
            "FROM cte " &
            "WHERE numFeeRate IS NOT NULL " &
            ") AS NUMFEERATE, " &
            "(SELECT TOP 1 STRNSPS " &
            "FROM cte " &
            "WHERE strNSPS IS NOT NULL " &
            ") AS STRNSPS, " &
            "(SELECT TOP 1 NUMNSPSFEE " &
            "FROM cte " &
            "WHERE numNSPSFee IS NOT NULL " &
            ") AS NUMNSPSFEE, " &
            "(SELECT TOP 1 STRNSPSEXEMPT " &
            "FROM cte " &
            "WHERE strNSPSExempt IS NOT NULL " &
            ") AS STRNSPSEXEMPT, " &
            "(SELECT TOP 1 STRNSPSEXEMPTREASON " &
            "FROM cte " &
            "WHERE strNSPSExemptReason IS NOT NULL " &
            ") AS STRNSPSEXEMPTREASON, " &
            "(SELECT TOP 1 NUMADMINFEE " &
            "FROM cte " &
            "WHERE NUMADMINFEE IS NOT NULL " &
            ") AS NUMADMINFEE, " &
            "(SELECT TOP 1 NUMTOTALFEE " &
            "FROM cte " &
            "WHERE numTotalFee IS NOT NULL " &
            ") AS NUMTOTALFEE, " &
            "(SELECT TOP 1 STRCLASS " &
            "FROM cte " &
            "WHERE strClass IS NOT NULL " &
            ") AS STRCLASS, " &
            "(SELECT TOP 1 STROPERATE " &
            "FROM cte " &
            "WHERE strOperate IS NOT NULL " &
            ") AS STROPERATE, " &
            "(SELECT TOP 1 DATSHUTDOWN " &
            "FROM cte " &
            "WHERE datShutDown IS NOT NULL " &
            ") AS DATSHUTDOWN, " &
            "(SELECT TOP 1 STROFFICIALNAME " &
            "FROM cte " &
            "WHERE strOfficialName IS NOT NULL " &
            ") AS STROFFICIALNAME, " &
            "(SELECT TOP 1 STROFFICIALTITLE " &
            "FROM cte " &
            "WHERE strOfficialTitle IS NOT NULL " &
            ") AS STROFFICIALTITLE, " &
            "(SELECT TOP 1 STRPAYMENTPLAN " &
            "FROM cte " &
            "WHERE STRPAYMENTPLAN IS NOT NULL " &
            ") AS STRPAYMENTPLAN, " &
            "(SELECT TOP 1 ACTIVE " &
            "FROM cte " &
            "WHERE ACTIVE IS NOT NULL " &
            ") AS ACTIVE, " &
            "(SELECT TOP 1 UPDATEUSER " &
            "FROM cte " &
            "WHERE UPDATEUSER IS NOT NULL " &
            ") AS UPDATEUSER "

            Dim params As SqlParameter() = {
                New SqlParameter("@airs", AirsNumber.DbFormattedString),
                New SqlParameter("@year", FeeYear)
            }

            Dim dr As DataRow = DB.GetDataRow(SQL, params)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strSyntheticMinor")) Then
                    txtAuditedSM.Clear()
                Else
                    If dr.Item("strSyntheticMinor") = "1" Then
                        txtAuditedSM.Text = "True"
                    Else
                        txtAuditedSM.Text = "False"
                    End If
                End If
                If IsDBNull(dr.Item("numSMFee")) Then
                    txtAuditedSMFee.Clear()
                Else
                    txtAuditedSMFee.Text = Format(dr.Item("numSMFee"), "c")
                End If
                If IsDBNull(dr.Item("strPart70")) Then
                    txtAuditedPart70.Clear()
                Else
                    If dr.Item("strPart70") = "1" Then
                        txtAuditedPart70.Text = "True"
                    Else
                        txtAuditedPart70.Text = "False"
                    End If
                End If
                If IsDBNull(dr.Item("numpart70Fee")) Then
                    txtAuditedPart70Fee.Clear()
                Else
                    txtAuditedPart70Fee.Text = Format(dr.Item("numPart70Fee"), "c")
                End If
                If IsDBNull(dr.Item("intVOCTons")) Then
                    txtAuditedVOCTons.Clear()
                Else
                    txtAuditedVOCTons.Text = dr.Item("intVOCTons")
                End If
                If IsDBNull(dr.Item("intPMTons")) Then
                    txtAuditedPMTons.Clear()
                Else
                    txtAuditedPMTons.Text = dr.Item("intPMTons")
                End If
                If IsDBNull(dr.Item("intSO2Tons")) Then
                    txtAuditedSO2Tons.Clear()
                Else
                    txtAuditedSO2Tons.Text = dr.Item("intSO2Tons")
                End If
                If IsDBNull(dr.Item("intNOxTons")) Then
                    txtAuditedNOxTons.Clear()
                Else
                    txtAuditedNOxTons.Text = dr.Item("intNOxTons")
                End If
                If IsDBNull(dr.Item("numCalculatedFee")) Then
                    txtAuditedCalculatedFee.Clear()
                Else
                    txtAuditedCalculatedFee.Text = Format(dr.Item("numCalculatedFee"), "c")
                End If
                If IsDBNull(dr.Item("numTotalFee")) Then
                    txtAuditedTotalFees.Clear()
                Else
                    txtAuditedTotalFees.Text = Format(dr.Item("numTotalFee"), "c")
                End If
                If IsDBNull(dr.Item("numFeeRate")) Then
                    txtAuditedFeeRate.Clear()
                Else
                    txtAuditedFeeRate.Text = Format(dr.Item("numFeeRate"), "c")
                End If
                txtAuditedNSPS.Clear()
                If IsDBNull(dr.Item("strNSPS")) Then
                    txtAuditedNSPS.Clear()
                Else
                    If dr.Item("strNSPS") = "1" Then
                        txtAuditedNSPS.Text = "True"
                    Else
                        txtAuditedNSPS.Text = "False"
                    End If
                End If
                If IsDBNull(dr.Item("numNSPSFee")) Then
                    txtAuditedNSPSFee.Clear()
                Else
                    txtAuditedNSPSFee.Text = Format(dr.Item("numNSPSFee"), "c")
                End If
                If IsDBNull(dr.Item("strNSPSExempt")) Then
                    txtAuditedNSPSExempt.Clear()
                Else
                    If dr.Item("strNSPSExempt") = "1" Then
                        txtAuditedNSPSExempt.Text = "True"
                    Else
                        txtAuditedNSPSExempt.Text = "False"
                    End If
                End If
                If IsDBNull(dr.Item("strNSPSExemptReason")) Then
                    txtAuditedExemptions.Clear()
                Else
                    txtAuditedExemptions.Text = dr.Item("strNSPSExemptReason")
                End If
                If IsDBNull(dr.Item("numAdminFee")) Then
                    txtAuditedAdminFee.Clear()
                Else
                    txtAuditedAdminFee.Text = Format(dr.Item("numAdminFee"), "c")
                End If
                If IsDBNull(dr.Item("numTotalFee")) Then
                    txtAuditedTotalFees.Clear()
                Else
                    txtAuditedTotalFees.Text = Format(dr.Item("numTotalFee"), "c")
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    txtAuditedClass.Clear()
                Else
                    txtAuditedClass.Text = dr.Item("strClass")
                End If
                If IsDBNull(dr.Item("stroperate")) Then
                    txtAuditedOpStatus.Clear()
                Else
                    If dr.Item("strOperate") = "1" Then
                        txtAuditedOpStatus.Text = "True"
                    Else
                        txtAuditedOpStatus.Text = "False"
                    End If
                End If
                If IsDBNull(dr.Item("datShutDown")) Then
                    txtAuditedShutdown.Clear()
                Else
                    txtAuditedShutdown.Text = dr.Item("datShutDown")
                End If

                If IsDBNull(dr.Item("strOfficialName")) Then
                    txtAuditedOfficialName.Clear()
                Else
                    txtAuditedOfficialName.Text = dr.Item("strOfficialName")
                End If
                If IsDBNull(dr.Item("strOfficialTitle")) Then
                    txtAuditedOfficialTitle.Clear()
                Else
                    txtAuditedOfficialTitle.Text = dr.Item("strOfficialTitle")
                End If
                If IsDBNull(dr.Item("strPaymentPlan")) Then
                    txtAuditedPaymentType.Clear()
                Else
                    txtAuditedPaymentType.Text = dr.Item("strPaymentPlan")
                End If
            End If

            Dim SQLLine As String = ""
            If txtAuditedExemptions.Text <> "" Then
                Do While txtAuditedExemptions.Text <> ""
                    If txtAuditedExemptions.Text.Contains(",") Then
                        temp = Mid(txtAuditedExemptions.Text, 1, InStr(txtAuditedExemptions.Text, ",", CompareMethod.Text) - 1)
                        txtAuditedExemptions.Text = Replace(txtAuditedExemptions.Text, temp & ",", "")
                    Else
                        temp = txtAuditedExemptions.Text
                        txtAuditedExemptions.Text = Replace(txtAuditedExemptions.Text, temp, "")
                    End If
                    SQLLine = SQLLine & " NSPSReasonCode = '" & temp & "' or "
                Loop
                If SQLLine <> "" Then
                    SQLLine = Mid(SQLLine, 1, SQLLine.Length - 3)
                End If

                SQL = "Select Description " &
                "from FSLK_NSPSReason " &
                "where " & SQLLine

                Dim desc As String = DB.GetSingleValue(Of String)(SQL)
                If Not String.IsNullOrEmpty(desc) Then
                    txtAuditedExemptions.Text = txtAuditedExemptions.Text & "- " & desc & vbCrLf & vbCrLf
                End If
            End If

            SQL = "Select " &
            "FS_FeeAudit.AuditID, " &
            "case when strSyntheticMinor = '1' then 'True' " &
            "when strSYntheticMinor is null then '' " &
            "else 'False' " &
            "end SyntheticMinor, numSMFee, " &
            "case when strPart70 = '1' then 'True' " &
               "when strPart70 is null then '' " &
            "else 'False' " &
            "end Part70, numPart70Fee, " &
            "intVOCTons, intPMTons, " &
            "intSO2Tons, intNOXTons, " &
            "numCalculatedFee, numFeeRate, " &
            "case when strNSPS = '1' then 'True' " &
            "when strNSPS is null then '' " &
            "else 'False' " &
            "end NSPS, numNSPSFee, " &
            "case when strNSPSExempt = '1' then 'True' " &
                "when strNSPSExempt is null then '' " &
            "else 'False' " &
            "end NSPSExempt, " &
            "numAdminFee, numTotalFee, " &
            "strClass, strOperate, " &
            "datShutdown, strOfficialname, " &
            "strOfficialTitle, strPaymentPlan, " &
            "(strLastName+', '+strFirstName) as IAIPUpdate, " &
            "FS_FeeAudit.UpdateDateTime, FS_FeeAudit.CreateDateTime " &
            "from FS_FeeAmendment " &
            "left join FS_FeeAudit " &
            "on FS_FeeAudit.AuditID = FS_FeeAmendment.AuditID " &
            "inner join EPDUserProfiles " &
            "on FS_FeeAudit.UpdateUser = EPDUserProfiles.numUserID " &
            "where FS_FeeAudit.strAIRSNumber = @airs " &
            "and FS_FeeAudit.numFeeyear  = @year "

            dgvAuditHistory.DataSource = DB.GetDataTable(SQL, params)

            dgvAuditHistory.RowHeadersVisible = False
            dgvAuditHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvAuditHistory.AllowUserToResizeColumns = True
            dgvAuditHistory.AllowUserToAddRows = False
            dgvAuditHistory.AllowUserToDeleteRows = False
            dgvAuditHistory.AllowUserToOrderColumns = True
            dgvAuditHistory.AllowUserToResizeRows = True

            dgvAuditHistory.Columns("AuditID").HeaderText = "Audit ID"
            dgvAuditHistory.Columns("AuditID").DisplayIndex = 0
            dgvAuditHistory.Columns("AuditID").Width = 40
            dgvAuditHistory.Columns("SyntheticMinor").HeaderText = "SM Status"
            dgvAuditHistory.Columns("SyntheticMinor").DisplayIndex = 1
            dgvAuditHistory.Columns("SyntheticMinor").Width = 50
            dgvAuditHistory.Columns("numSMFee").HeaderText = "SM Fee"
            dgvAuditHistory.Columns("numSMFee").DisplayIndex = 2
            dgvAuditHistory.Columns("numSMFee").Width = 100
            dgvAuditHistory.Columns("numSMFee").DefaultCellStyle.Format = "c"
            dgvAuditHistory.Columns("Part70").HeaderText = "Part 70 Stauts"
            dgvAuditHistory.Columns("Part70").DisplayIndex = 3
            dgvAuditHistory.Columns("Part70").Width = 50
            dgvAuditHistory.Columns("numPart70Fee").HeaderText = "Part 70 Fee"
            dgvAuditHistory.Columns("numPart70Fee").DisplayIndex = 4
            dgvAuditHistory.Columns("numPart70Fee").Width = 100
            dgvAuditHistory.Columns("numPart70Fee").DefaultCellStyle.Format = "c"
            dgvAuditHistory.Columns("intVOCTons").HeaderText = "VOC Tons"
            dgvAuditHistory.Columns("intVOCTons").DisplayIndex = 5
            dgvAuditHistory.Columns("intVOCTons").Width = 50
            dgvAuditHistory.Columns("intPMTons").HeaderText = "PM Tons"
            dgvAuditHistory.Columns("intPMTons").DisplayIndex = 6
            dgvAuditHistory.Columns("intPMTons").Width = 50
            dgvAuditHistory.Columns("intSO2Tons").HeaderText = "SO2 Tons"
            dgvAuditHistory.Columns("intSO2Tons").DisplayIndex = 7
            dgvAuditHistory.Columns("intSO2Tons").Width = 50
            dgvAuditHistory.Columns("intNOXTons").HeaderText = "NOx Tons"
            dgvAuditHistory.Columns("intNOXTons").DisplayIndex = 8
            dgvAuditHistory.Columns("intNOXTons").Width = 50
            dgvAuditHistory.Columns("numCalculatedFee").HeaderText = "Calculated Fee"
            dgvAuditHistory.Columns("numCalculatedFee").DisplayIndex = 9
            dgvAuditHistory.Columns("numCalculatedFee").Width = 100
            dgvAuditHistory.Columns("numFeeRate").HeaderText = "Fee Rate"
            dgvAuditHistory.Columns("numFeeRate").DisplayIndex = 10
            dgvAuditHistory.Columns("numFeeRate").Width = 100
            dgvAuditHistory.Columns("numFeeRate").DefaultCellStyle.Format = "c"
            dgvAuditHistory.Columns("NSPS").HeaderText = "NSPS Status"
            dgvAuditHistory.Columns("NSPS").DisplayIndex = 11
            dgvAuditHistory.Columns("NSPS").Width = 50
            dgvAuditHistory.Columns("numNSPSFee").HeaderText = "NSPS Fee"
            dgvAuditHistory.Columns("numNSPSFee").DisplayIndex = 12
            dgvAuditHistory.Columns("numNSPSFee").Width = 100
            dgvAuditHistory.Columns("numNSPSFee").DefaultCellStyle.Format = "c"
            dgvAuditHistory.Columns("NSPSExempt").HeaderText = "NSPS Exempt Stauts"
            dgvAuditHistory.Columns("NSPSExempt").DisplayIndex = 13
            dgvAuditHistory.Columns("NSPSExempt").Width = 50
            dgvAuditHistory.Columns("numAdminFee").HeaderText = "Admin Fee"
            dgvAuditHistory.Columns("numAdminFee").DisplayIndex = 14
            dgvAuditHistory.Columns("numAdminFee").Width = 100
            dgvAuditHistory.Columns("numAdminFee").DefaultCellStyle.Format = "c"
            dgvAuditHistory.Columns("numTotalFee").HeaderText = "Total Fee"
            dgvAuditHistory.Columns("numTotalFee").DisplayIndex = 15
            dgvAuditHistory.Columns("numTotalFee").Width = 100
            dgvAuditHistory.Columns("numTotalFee").DefaultCellStyle.Format = "c"
            dgvAuditHistory.Columns("strClass").HeaderText = "Class"
            dgvAuditHistory.Columns("strClass").DisplayIndex = 16
            dgvAuditHistory.Columns("strClass").Width = 50
            dgvAuditHistory.Columns("strOperate").HeaderText = "Op. Status"
            dgvAuditHistory.Columns("strOperate").DisplayIndex = 17
            dgvAuditHistory.Columns("strOperate").Width = 50
            dgvAuditHistory.Columns("datShutdown").HeaderText = "Shutdown"
            dgvAuditHistory.Columns("datShutdown").DisplayIndex = 18
            dgvAuditHistory.Columns("datShutdown").Width = 100
            dgvAuditHistory.Columns("datShutdown").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strOfficialname").HeaderText = "Official Name"
            dgvAuditHistory.Columns("strOfficialname").DisplayIndex = 19
            dgvAuditHistory.Columns("strOfficialname").Width = 100
            dgvAuditHistory.Columns("strOfficialTitle").HeaderText = "Official Title"
            dgvAuditHistory.Columns("strOfficialTitle").DisplayIndex = 20
            dgvAuditHistory.Columns("strOfficialTitle").Width = 100
            dgvAuditHistory.Columns("strPaymentPlan").HeaderText = "Payment Plan"
            dgvAuditHistory.Columns("strPaymentPlan").DisplayIndex = 21
            dgvAuditHistory.Columns("strPaymentPlan").Width = 100
            dgvAuditHistory.Columns("IAIPUpdate").HeaderText = "IAIP Update"
            dgvAuditHistory.Columns("IAIPUpdate").DisplayIndex = 22
            dgvAuditHistory.Columns("IAIPUpdate").Width = 75
            dgvAuditHistory.Columns("UpdateDateTime").HeaderText = "Date Updated"
            dgvAuditHistory.Columns("UpdateDateTime").DisplayIndex = 23
            dgvAuditHistory.Columns("UpdateDateTime").Width = 100
            dgvAuditHistory.Columns("UpdateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("CreateDateTime").HeaderText = "Date Created"
            dgvAuditHistory.Columns("CreateDateTime").DisplayIndex = 24
            dgvAuditHistory.Columns("CreateDateTime").Width = 100
            dgvAuditHistory.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"

            chbMakeEdits.Checked = False
            llbAuditPerformed.Visible = False
            If dgvAuditHistory.RowCount > 1 Then
                llbAuditPerformed.Visible = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub RefreshAdminStatus()
        Try
            If Me.AirsNumber.ToString IsNot Nothing AndAlso Me.FeeYear IsNot Nothing Then
                Dim SQL As String = "select " &
                "strIAIPDesc " &
                "From FS_Admin " &
                "left join FSLK_ADMIN_Status " &
                "on FS_Admin.numCurrentStatus = FSLK_Admin_Status.ID " &
                "and numFeeYear = @year " &
                "and strAIRSNumber = @airs "

                Dim p As SqlParameter() = {
                    New SqlParameter("@year", FeeYear),
                    New SqlParameter("@airs", AirsNumber.DbFormattedString)
                }

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("strIAIPDesc")) Then
                        txtIAIPAdminStatus.Clear()
                    Else
                        txtIAIPAdminStatus.Text = dr.Item("strIAIPDesc")
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadStaff()
        Try
            Dim dtStaff As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            Dim SQL As String = "select * " &
            "from " &
            "(select " &
            "(strLastName+', '+strFirstName) as Staff, " &
            "numUserID " &
            "from EPDUserProfiles " &
            "where numbranch = '1' " &
            "and numprogram = '2' " &
            "and numUnit = '9' " &
            "and numEmployeeStatus = '1' " &
            "union " &
            "select distinct " &
            "(strLastName+', '+strFirstName) as Staff, " &
            "numUserID " &
            "from EPDUserProfiles inner join FS_FeeAmendment  " &
            "on EPDUserProfiles.nuMUserID = FS_FeeAmendment.UpdateUser ) as t1 " &
            "order by staff "

            Dim dt As DataTable = DB.GetDataTable(SQL)

            dtStaff.Columns.Add("numUserID", GetType(String))
            dtStaff.Columns.Add("Staff", GetType(String))

            drNewRow = dtStaff.NewRow()
            drNewRow("numUserID") = ""
            drNewRow("Staff") = ""
            dtStaff.Rows.Add(drNewRow)

            For Each drDSRow In dt.Rows()
                drNewRow = dtStaff.NewRow()
                drNewRow("numUserID") = drDSRow("numUserID")
                drNewRow("Staff") = drDSRow("Staff")
                dtStaff.Rows.Add(drNewRow)
            Next

            With cboStaffResponsible
                .DataSource = dtStaff
                .DisplayMember = "Staff"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " Mailout Information tab "

    Private Sub MailoutEnableEditingButton_Click(sender As Object, e As EventArgs) Handles MailoutEditContactButton.Click
        MailoutEditingToggle(True)
    End Sub

    Private Sub MailoutEditFacilityButton_Click(sender As Object, e As EventArgs) Handles MailoutEditFacilityButton.Click
        MailoutEditingToggle(True, False)
    End Sub

    Private Sub MailoutStoreTempContact()
        tempContact = MailoutGetContactFromForm()
    End Sub

    Private Function MailoutGetContactFromForm() As Contact
        Dim contact As New Contact
        Dim tempAddress As New Address
        With tempAddress
            .Street = txtContactAddress.Text
            .Street2 = txtContactAddress2.Text
            .City = txtContactCity.Text
            .State = txtContactState.Text
            .PostalCode = mtbContactZipCode.Text
        End With
        With contact
            .FirstName = txtContactFirstName.Text
            .LastName = txtContactLastName.Text
            .EmailAddress = txtContactEmail.Text
            .Prefix = txtContactPrefix.Text
            .Suffix = txtContactSuffix.Text
            .Title = txtContactTitle.Text
            .CompanyName = txtContactCoName.Text
            .MailingAddress = tempAddress
        End With
        Return contact
    End Function

    Private Sub MailoutStoreTempFacility()
        tempFacility = MailoutGetFacilityFromForm()
    End Sub

    Private Function MailoutGetFacilityFromForm() As Facility
        Dim facility As New Facility

        With facility
            .FacilityName = txtInitialFacilityName.Text
            .FacilityLocation = New Location
            With .FacilityLocation
                .Address = New Address
                With .Address
                    .Street = txtInitailFacilityAddress.Text
                    .Street2 = txtInitialAddressLine2.Text
                    .City = txtInitialCity.Text
                    .PostalCode = mtbInitialZipCode.Text
                End With
            End With
            .Comment = txtInitialFacilityComment.Text
            .HeaderData = New FacilityHeaderData
            With .HeaderData
                .OperationalStatus = cboInitialOpStatus.SelectedValue
                .Classification = cboInitialClassification.SelectedValue
                .ShutdownDate = If(dtpInitialShutDownDate.Checked, dtpInitialShutDownDate.Value, CType(Nothing, DateTime?))
            End With
            If rdbInitialNSPSTrue.Checked Then .HeaderData.AirPrograms = .HeaderData.AirPrograms Or AirProgram.NSPS
            If rdbInitialPart70True.Checked Then .HeaderData.AirPrograms = .HeaderData.AirPrograms Or AirProgram.TitleV
        End With

        Return facility
    End Function

    Private Sub MailoutEditingToggle(enable As Boolean, Optional facilitySection As Boolean = True)
        If facilitySection Then

            If enable Then MailoutStoreTempContact()

            txtContactPrefix.Enabled = enable
            txtContactFirstName.Enabled = enable
            txtContactLastName.Enabled = enable
            txtContactSuffix.Enabled = enable
            txtContactTitle.Enabled = enable
            txtContactCoName.Enabled = enable
            txtContactAddress.Enabled = enable
            txtContactAddress2.Enabled = enable
            txtContactCity.Enabled = enable
            txtContactState.Enabled = enable
            mtbContactZipCode.Enabled = enable
            txtContactEmail.Enabled = enable

            MailoutEditContactButton.Enabled = Not (enable)
            MailoutEditContactButton.Visible = Not (enable)
            MailoutCancelEditingContactButton.Enabled = enable
            MailoutCancelEditingContactButton.Visible = enable
            MailoutSaveContactButton.Enabled = enable
            MailoutSaveContactButton.Visible = enable

        Else

            If enable Then MailoutStoreTempFacility()

            txtInitialFacilityName.Enabled = enable
            txtInitailFacilityAddress.Enabled = enable
            txtInitialAddressLine2.Enabled = enable
            txtInitialCity.Enabled = enable
            mtbInitialZipCode.Enabled = enable
            txtInitialFacilityComment.Enabled = enable
            cboInitialOpStatus.Enabled = enable
            cboInitialClassification.Enabled = enable
            MailoutInitialNspsPanel.Enabled = enable
            MailoutInitialPart70Panel.Enabled = enable
            dtpInitialShutDownDate.Enabled = enable

            MailoutEditFacilityButton.Enabled = Not (enable)
            MailoutEditFacilityButton.Visible = Not (enable)
            MailoutCancelEditFacilityButton.Enabled = enable
            MailoutCancelEditFacilityButton.Visible = enable
            MailoutSaveFacilityButton.Enabled = enable
            MailoutSaveFacilityButton.Visible = enable

        End If
    End Sub

    Private Sub MailoutCancelEditingContactButton_Click(sender As Object, e As EventArgs) Handles MailoutCancelEditingContactButton.Click
        MailoutFillContactFrom(tempContact)
        tempContact = Nothing
        MailoutEditingToggle(False)
    End Sub

    Private Sub MailoutCancelEditFacilityButton_Click(sender As Object, e As EventArgs) Handles MailoutCancelEditFacilityButton.Click
        MailoutFillFacilityFrom(tempFacility)
        tempFacility = Nothing
        MailoutEditingToggle(False, False)
    End Sub

    Private Sub MailoutFillContactFrom(contact As Contact)
        With contact
            txtContactFirstName.Text = .FirstName
            txtContactLastName.Text = .LastName
            txtContactEmail.Text = .EmailAddress
            txtContactPrefix.Text = .Prefix
            txtContactSuffix.Text = .Suffix
            txtContactTitle.Text = .Title
            txtContactCoName.Text = .CompanyName
            txtContactAddress.Text = .MailingAddress.Street
            txtContactAddress2.Text = .MailingAddress.Street2
            txtContactCity.Text = .MailingAddress.City
            txtContactState.Text = .MailingAddress.State
            mtbContactZipCode.Text = .MailingAddress.PostalCode
        End With
    End Sub

    Private Sub MailoutFillFacilityFrom(facility As Facility)
        With facility
            txtInitialFacilityName.Text = .FacilityName
            txtInitailFacilityAddress.Text = .FacilityLocation.Address.Street
            txtInitialAddressLine2.Text = .FacilityLocation.Address.Street2
            txtInitialCity.Text = .FacilityLocation.Address.City
            mtbInitialZipCode.Text = .FacilityLocation.Address.PostalCode
            txtInitialFacilityComment.Text = .Comment
            cboInitialOpStatus.SelectedValue = .HeaderData.OperationalStatus
            cboInitialClassification.SelectedValue = .HeaderData.Classification
            rdbInitialNSPSTrue.Checked = .SubjectToNsps
            rdbInitialNSPSFalse.Checked = Not (.SubjectToNsps)
            rdbInitialPart70True.Checked = .SubjectToPart70
            rdbInitialPart70False.Checked = Not (.SubjectToPart70)
            If .HeaderData.ShutdownDate Is Nothing Then
                dtpInitialShutDownDate.Checked = False
            Else
                dtpInitialShutDownDate.Checked = True
                dtpInitialShutDownDate.Value = .HeaderData.ShutdownDate
            End If
        End With
    End Sub

    Private Sub MailoutReplaceContactWithFeeContactButton_Click(sender As Object, e As EventArgs) Handles MailoutReplaceContactWithFeeContactButton.Click
        MailoutEditingToggle(True)
        Dim contact As Contact = DAL.GetCurrentContact(AirsNumber, DAL.ContactKey.Fees)
        MailoutFillContactFrom(contact)
    End Sub

    Private Sub MailoutReplaceFacilityInfoButton_Click(sender As Object, e As EventArgs) Handles MailoutReplaceFacilityInfoButton.Click
        Try

            MailoutEditingToggle(True, False)
            Dim facility As Facility = DAL.GetFacility(Me.AirsNumber)
            facility.RetrieveHeaderData()

            MailoutFillFacilityFrom(facility)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MailoutSaveContactButton_Click(sender As Object, e As EventArgs) Handles MailoutSaveContactButton.Click
        If (mtbAirsNumber.Text <> AirsNumber.FormattedString) OrElse (FeeYearsComboBox.SelectedItem.ToString <> FeeYear) Then
            MessageBox.Show("The selected AIRS number or fee year don't match the displayed information. " &
                            "Please double-check and try again." &
                            vbNewLine & vbNewLine & "NO DATA SAVED.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Not DAL.FeeMailoutEntryExists(AirsNumber, FeeYear) Then
            MessageBox.Show("Can't save contact: No mailout exists for that AIRS number and year.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim contact As Contact = MailoutGetContactFromForm()
        Dim result As Boolean = DAL.UpdateFeeMailoutContact(contact, AirsNumber.DbFormattedString, FeeYear)

        If result Then
            tempContact = Nothing
            MailoutEditingToggle(False)
        Else
            MessageBox.Show("There was an error saving contact data. Please try again.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub MailoutSaveFacilityButton_Click(sender As Object, e As EventArgs) Handles MailoutSaveFacilityButton.Click
        If (mtbAirsNumber.Text <> AirsNumber.FormattedString) OrElse (FeeYearsComboBox.SelectedItem.ToString <> FeeYear) Then
            MessageBox.Show("The selected AIRS number or fee year don't match the displayed information. " &
                            "Please double-check and try again." &
                            vbNewLine & vbNewLine & "NO DATA SAVED.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Not DAL.FeeMailoutEntryExists(AirsNumber, FeeYear) Then
            MessageBox.Show("Can't save facility: No mailout exists for that AIRS number and year.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim facility As Facility = MailoutGetFacilityFromForm()
        Dim result As Boolean = DAL.UpdateFeeMailoutFacility(facility, AirsNumber.DbFormattedString, FeeYear)

        If result Then
            tempFacility = Nothing
            MailoutEditingToggle(False, False)
        Else
            MessageBox.Show("There was an error saving facility data. Please try again.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

#End Region

    Private Sub EditContactsButton_Click(sender As Object, e As EventArgs) Handles EditContactsButton.Click
        If AirsNumber.ToString Is Nothing OrElse (mtbAirsNumber.Text <> AirsNumber.FormattedString) Then
            MessageBox.Show("Please select a valid AIRS number first.",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim parameters As New Generic.Dictionary(Of FormParameter, String)
        parameters(FormParameter.AirsNumber) = AirsNumber.ToString
        parameters(FormParameter.FacilityName) = txtFeeAdminFacilityName.Text
        parameters(FormParameter.Key) = DAL.ContactKey.Fees.ToString
        OpenMultiForm(IAIPEditContacts, AirsNumber.ToString, parameters)
    End Sub

    Private Sub ReloadButton_Click(sender As Object, e As EventArgs) Handles ReloadButton.Click
        Try
            If FeeYearsComboBox.SelectedIndex = 0 Then
                MessageBox.Show("Please select a Fee Year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If Not Apb.ApbFacilityId.IsValidAirsNumberFormat(mtbAirsNumber.Text) Then
                MessageBox.Show("AIRS number is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Me.FeeYear = FeeYearsComboBox.Text
            Me.AirsNumber = mtbAirsNumber.Text

            ClearForm()

            ds = New DataSet
            dgvInvoices.DataMember = ""
            dgvInvoices.DataSource = ds

            ClearInvoices()
            ClearAuditData()
            crFeeStatsAndInvoices.ReportSource = Nothing
            crFeeStatsAndInvoices.Refresh()

            MailoutEditingToggle(False)
            MailoutEditingToggle(False, False)

            LoadAdminData()
            LoadAuditedData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearForm()
        Try
            ClearAdminData()

            rdbInvoiceDataPaidStatus.Checked = False
            rdbInvoiceDataUnpaidStatus.Checked = False
            txtInvoiceClassification.Clear()
            chbInvoiceDataOperating.Checked = False
            chbInvoicedataNonAttainment.Checked = False
            chbInvoiceDataNSPS.Checked = False
            chbInvoiceDataPart70.Checked = False
            chbInvoiceDataSyntheticMinor.Checked = False
            txtInvoiceDataPaymentType.Clear()
            txtInvoiceDataGECOComments.Clear()
            txtInvoiceDataVOCTons.Clear()
            txtInvoiceDataPMTons.Clear()
            txtInvoiceDataSO2Tons.Clear()
            txtInvoiceDataNOxTons.Clear()
            txtInvoiceDataFeeRate.Clear()
            txtInvoiceDataPart70Fee.Clear()
            txtInvoiceDataSMFee.Clear()
            txtInvoiceDataNSPSFee.Clear()
            txtInvoiceDataAdminFee.Clear()
            txtInvoiceDataTotalFees.Clear()
            dtpInvoiceDataDateInvoiced.Text = OracleDate
            chbInvoiceDataNSPSExempt.Checked = False
            txtInvoiceDataNSPSExempts.Clear()
            ds = New DataSet
            dgvInvoiceDataNSPSExemptions.DataMember = ""
            dgvInvoiceDataNSPSExemptions.DataSource = ds

            txtInvoiceDataOfficialName.Clear()
            txtInvoiceDataOfficialTitle.Clear()
            txtInvoiceDataConfirmationNumber.Clear()
            txtInvoiceDataUpdate.Clear()
            dtpInvoiceDataDateUpdated.Text = OracleDate
            dtpInvoiceDataCreatedDate.Text = OracleDate
            dgvInvoiceData.DataMember = ""
            dgvInvoiceData.DataSource = ds

            txtGECOContactSalutation.Clear()
            txtGECOContactFirstName.Clear()
            txtGECOContactLastName.Clear()
            txtGECOContactTitle.Clear()
            txtGECOContactCompanyName.Clear()
            txtGECOContactStreetAddress.Clear()
            txtGECOContactCity.Clear()
            txtGECOContactState.Clear()
            mtbGECOContactZipCode.Clear()
            mtbGECOContactPhontNumber.Clear()
            mtbGECOContactFaxNumber.Clear()
            txtGECOContactEmail.Clear()
            txtGECOContactComments.Clear()

            dgvGECOFeeContacts.DataMember = ""
            dgvGECOFeeContacts.DataSource = ds

            txtTransactionID.Clear()
            txtInvoiceID.Clear()
            txtDepositNo.Clear()
            txtBatchNo.Clear()
            txtTransactionCreatedBy.Clear()
            cboTransactionType.SelectedValue = 0
            dtpTransactionDate.Text = OracleDate
            txtTransactionAmount.Clear()
            txtTransactionCheckNo.Clear()
            txtTransactionCreditCardNo.Clear()
            txtAPBComments.Clear()
            txtTransactionUpdated.Clear()
            dtpTransactionUpdated.Text = OracleDate
            dtpTransactionCreated.Text = OracleDate

            dgvTransactions.DataMember = ""
            dgvTransactions.DataSource = ds

            txtGECOContactSalutation.ReadOnly = True
            txtGECOContactFirstName.ReadOnly = True
            txtGECOContactLastName.ReadOnly = True
            txtGECOContactTitle.ReadOnly = True
            txtGECOContactCompanyName.ReadOnly = True
            txtGECOContactStreetAddress.ReadOnly = True
            txtGECOContactCity.ReadOnly = True
            txtGECOContactState.ReadOnly = True
            mtbGECOContactZipCode.ReadOnly = True
            mtbGECOContactPhontNumber.ReadOnly = True
            mtbGECOContactFaxNumber.ReadOnly = True
            txtGECOContactEmail.ReadOnly = True
            txtGECOContactComments.ReadOnly = True

            MailoutEditingToggle(False)
            ClearEditData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateFSAdmin_Click(sender As Object, e As EventArgs) Handles btnUpdateFSAdmin.Click
        Try
            Dim ResultDoc As DialogResult

            If rdbInactiveStatus.Checked = True Then
                ResultDoc = MessageBox.Show("If there are any transactions associated with this fee year they will ""effectively"" be deleted." &
                                            "Do you want to continue with an inactive status for this fee year data?", Me.Text,
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                Select Case ResultDoc
                    Case DialogResult.No
                        MsgBox("NO DATA SAVED.", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                End Select
            End If

            If (mtbAirsNumber.Text <> AirsNumber.FormattedString) OrElse (FeeYearsComboBox.SelectedItem.ToString <> FeeYear) Then
                MessageBox.Show("The selected AIRS number or fee year don't match the displayed information. " &
                                "Please double-check and try again." &
                                vbNewLine & vbNewLine & "NO DATA SAVED.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " &
                " FROM FS_ADMIN " &
                " WHERE strAIRSnumber = @pAirsNumber " &
                " AND numFeeYear = @pFeeYear "
            Dim parameters As SqlParameter() = {
                New SqlParameter("@pAirsNumber", Me.AirsNumber.DbFormattedString),
                New SqlParameter("@pFeeYear", Me.FeeYear)
            }
            Dim result As Boolean = DB.GetBoolean(query, parameters)

            If Not result Then
                MsgBox("The facility is not currently in the Fee universe for the selected year." & vbCrLf &
                       "Use the Add New Facility to Year." & vbCrLf & vbCrLf & "NO DATA SAVED", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If Update_FS_Admin(Me.FeeYear, Me.AirsNumber,
                             rdbEnrolledTrue.Checked,
                             dtpEnrollmentDate.Value, rdbMailoutTrue.Checked,
                             rdbLetterMailedTrue.Checked, dtpLetterMailed.Value,
                             rdbSubmittalTrue.Checked, dtpSubmittalDate.Value,
                             txtFSAdminComments.Text, rdbActiveAdmin.Checked) = True Then

                If rdbInactiveStatus.Checked = True Then
                    ClearForm()

                    ds = New DataSet
                    dgvInvoices.DataMember = ""
                    dgvInvoices.DataSource = ds

                    ClearInvoices()
                    ClearAuditData()

                    LoadAdminData()
                    LoadAuditedData()
                End If

                MsgBox("Save completed", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Did not Save", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddFSAdmin_Click(sender As Object, e As EventArgs) Handles btnAddFSAdmin.Click
        Try
            If (mtbAirsNumber.Text <> AirsNumber.FormattedString) OrElse (FeeYearsComboBox.SelectedItem.ToString <> FeeYear) Then
                MessageBox.Show("The selected AIRS number or fee year don't match the displayed information. " &
                                "Please double-check and try again." &
                                vbNewLine & vbNewLine & "NO DATA SAVED.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If Insert_FS_Admin(Me.FeeYear, Me.AirsNumber,
                          rdbEnrolledTrue.Checked,
                          dtpEnrollmentDate.Value, rdbMailoutTrue.Checked,
                          rdbLetterMailedTrue.Checked, dtpLetterMailed.Value,
                          rdbSubmittalTrue.Checked, dtpSubmittalDate.Value,
                          txtFSAdminComments.Text) = True Then

                MsgBox("Save completed", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Did not Save", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnGECOViewPastContacts_Click(sender As Object, e As EventArgs) Handles btnGECOViewPastContacts.Click
        Try
            Dim query As String = "Select * " &
            "from FS_ContactInfo " &
            "where strAIRSnumber = @strAIRSnumber " &
            "order by numFeeYear desc "

            Dim parameter As New SqlParameter("@strAIRSnumber", AirsNumber.DbFormattedString)

            dgvGECOFeeContacts.DataSource = DB.GetDataTable(query, parameter)

            dgvGECOFeeContacts.RowHeadersVisible = False
            dgvGECOFeeContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvGECOFeeContacts.AllowUserToResizeColumns = True
            dgvGECOFeeContacts.AllowUserToAddRows = False
            dgvGECOFeeContacts.AllowUserToDeleteRows = False
            dgvGECOFeeContacts.AllowUserToOrderColumns = True
            dgvGECOFeeContacts.AllowUserToResizeRows = True

            dgvGECOFeeContacts.Columns("numFeeYear").HeaderText = "Year"
            dgvGECOFeeContacts.Columns("numFeeYear").DisplayIndex = 0
            dgvGECOFeeContacts.Columns("numFeeYear").Width = 40
            dgvGECOFeeContacts.Columns("strContactFirstName").HeaderText = "First Name"
            dgvGECOFeeContacts.Columns("strContactFirstName").DisplayIndex = 1
            dgvGECOFeeContacts.Columns("strContactLastName").HeaderText = "Last Name"
            dgvGECOFeeContacts.Columns("strContactLastName").DisplayIndex = 2
            dgvGECOFeeContacts.Columns("strContactPrefix").HeaderText = "Prefix"
            dgvGECOFeeContacts.Columns("strContactPrefix").DisplayIndex = 3
            dgvGECOFeeContacts.Columns("strContactTitle").HeaderText = "Title"
            dgvGECOFeeContacts.Columns("strContactTitle").DisplayIndex = 4
            dgvGECOFeeContacts.Columns("strContactCompanyName").HeaderText = "Company Name"
            dgvGECOFeeContacts.Columns("strContactCompanyName").DisplayIndex = 5
            dgvGECOFeeContacts.Columns("strContactAddress").HeaderText = "Contact Address"
            dgvGECOFeeContacts.Columns("strContactAddress").DisplayIndex = 6
            dgvGECOFeeContacts.Columns("strContactCity").HeaderText = "City"
            dgvGECOFeeContacts.Columns("strContactCity").DisplayIndex = 7
            dgvGECOFeeContacts.Columns("strContactState").HeaderText = "State"
            dgvGECOFeeContacts.Columns("strContactState").DisplayIndex = 8
            dgvGECOFeeContacts.Columns("strContactZipCode").HeaderText = "Zip Code"
            dgvGECOFeeContacts.Columns("strContactZipCode").DisplayIndex = 9
            dgvGECOFeeContacts.Columns("strContactPhoneNumber").HeaderText = "Phone Number"
            dgvGECOFeeContacts.Columns("strContactPhoneNumber").DisplayIndex = 10
            dgvGECOFeeContacts.Columns("strContactFaxNumber").HeaderText = "Fax Number"
            dgvGECOFeeContacts.Columns("strContactFaxNumber").DisplayIndex = 11
            dgvGECOFeeContacts.Columns("strContactEmail").HeaderText = "Email Address"
            dgvGECOFeeContacts.Columns("strContactEmail").DisplayIndex = 12
            dgvGECOFeeContacts.Columns("strComment").HeaderText = "Comments"
            dgvGECOFeeContacts.Columns("strComment").DisplayIndex = 13
            dgvGECOFeeContacts.Columns("strAIRSnumber").HeaderText = "AIRS #"
            dgvGECOFeeContacts.Columns("strAIRSNumber").DisplayIndex = 14
            dgvGECOFeeContacts.Columns("strAIRSnumber").Visible = False

            dgvGECOFeeContacts.Columns("Active").HeaderText = "Active"
            dgvGECOFeeContacts.Columns("Active").DisplayIndex = 15
            dgvGECOFeeContacts.Columns("UpdateUser").HeaderText = "UpdateUser"
            dgvGECOFeeContacts.Columns("UpdateUser").DisplayIndex = 16

            dgvGECOFeeContacts.Columns("UpdateDateTime").HeaderText = "Update Date Time"
            dgvGECOFeeContacts.Columns("UpdateDateTime").DisplayIndex = 17
            dgvGECOFeeContacts.Columns("UpdateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvGECOFeeContacts.Columns("CreateDateTime").HeaderText = "Create Date Time"
            dgvGECOFeeContacts.Columns("CreateDateTime").DisplayIndex = 18
            dgvGECOFeeContacts.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvGECOFeeContacts_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvGECOFeeContacts.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvGECOFeeContacts.HitTest(e.X, e.Y)
            If dgvGECOFeeContacts.RowCount > 0 And hti.RowIndex <> -1 Then
                If IsDBNull(dgvGECOFeeContacts(2, hti.RowIndex).Value) Then
                    txtGECOContactFirstName.Clear()
                Else
                    txtGECOContactFirstName.Text = dgvGECOFeeContacts(2, hti.RowIndex).Value
                End If

                If IsDBNull(dgvGECOFeeContacts(3, hti.RowIndex).Value) Then
                    txtGECOContactLastName.Clear()
                Else
                    txtGECOContactLastName.Text = dgvGECOFeeContacts(3, hti.RowIndex).Value
                End If

                If IsDBNull(dgvGECOFeeContacts(4, hti.RowIndex).Value) Then
                    txtGECOContactSalutation.Clear()
                Else
                    txtGECOContactSalutation.Text = dgvGECOFeeContacts(4, hti.RowIndex).Value
                End If
                If IsDBNull(dgvGECOFeeContacts(5, hti.RowIndex).Value) Then
                    txtGECOContactTitle.Clear()
                Else
                    txtGECOContactTitle.Text = dgvGECOFeeContacts(5, hti.RowIndex).Value
                End If
                If IsDBNull(dgvGECOFeeContacts(6, hti.RowIndex).Value) Then
                    txtGECOContactCompanyName.Clear()
                Else
                    txtGECOContactCompanyName.Text = dgvGECOFeeContacts(6, hti.RowIndex).Value
                End If
                If IsDBNull(dgvGECOFeeContacts(7, hti.RowIndex).Value) Then
                    txtGECOContactStreetAddress.Clear()
                Else
                    txtGECOContactStreetAddress.Text = dgvGECOFeeContacts(7, hti.RowIndex).Value
                End If
                If IsDBNull(dgvGECOFeeContacts(8, hti.RowIndex).Value) Then
                    txtGECOContactCity.Clear()
                Else
                    txtGECOContactCity.Text = dgvGECOFeeContacts(8, hti.RowIndex).Value
                End If
                If IsDBNull(dgvGECOFeeContacts(9, hti.RowIndex).Value) Then
                    txtGECOContactState.Clear()
                Else
                    txtGECOContactState.Text = dgvGECOFeeContacts(9, hti.RowIndex).Value
                End If
                If IsDBNull(dgvGECOFeeContacts(10, hti.RowIndex).Value) Then
                    mtbGECOContactZipCode.Clear()
                Else
                    mtbGECOContactZipCode.Text = dgvGECOFeeContacts(10, hti.RowIndex).Value
                End If
                If IsDBNull(dgvGECOFeeContacts(11, hti.RowIndex).Value) Then
                    mtbGECOContactPhontNumber.Clear()
                Else
                    mtbGECOContactPhontNumber.Text = dgvGECOFeeContacts(11, hti.RowIndex).Value
                End If
                If IsDBNull(dgvGECOFeeContacts(12, hti.RowIndex).Value) Then
                    mtbGECOContactFaxNumber.Clear()
                Else
                    mtbGECOContactFaxNumber.Text = dgvGECOFeeContacts(12, hti.RowIndex).Value
                End If
                If IsDBNull(dgvGECOFeeContacts(13, hti.RowIndex).Value) Then
                    txtGECOContactEmail.Clear()
                Else
                    txtGECOContactEmail.Text = dgvGECOFeeContacts(13, hti.RowIndex).Value
                End If
                If IsDBNull(dgvGECOFeeContacts(14, hti.RowIndex).Value) Then
                    txtGECOContactComments.Clear()
                Else
                    txtGECOContactComments.Text = dgvGECOFeeContacts(14, hti.RowIndex).Value
                End If


            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Function InvoiceCheck() As Boolean
        If Not IsNumeric(txtInvoiceID.Text) Then
            Return False
        End If

        Dim SQL As String = "Select " &
           "InvoiceID " &
           "from FS_FeeInvoice " &
           "where invoiceID = @invoiceID " &
           "and strAIRSNumber = @airs " &
           "and numFeeyear = @year "

        Dim p2 As SqlParameter() = {
            New SqlParameter("@year", FeeYear),
            New SqlParameter("@airs", AirsNumber.DbFormattedString),
            New SqlParameter("@invoiceID", txtInvoiceID.Text)
        }

        Return DB.ValueExists(SQL, p2)
    End Function

    Private Sub btnTransactionNew_Click(sender As Object, e As EventArgs) Handles btnTransactionNew.Click
        Try

            If (mtbAirsNumber.Text <> Me.AirsNumber.FormattedString) _
                Or (FeeYearsComboBox.SelectedItem.ToString <> txtYear.Text) _
                Or txtAIRSNumber.Text = "" Or FeeYearsComboBox.SelectedIndex = 0 _
                Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." &
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If txtTransactionID.Text <> "" Then
                Dim TransactionCheck As DialogResult
                TransactionCheck = MessageBox.Show("There is already an existing transaction associated with the " &
                        "invoice #: " & txtInvoiceID.Text & "." & vbCrLf & "OK - Add additional Transaction" & vbCrLf &
                        "Cancel - Do nothing.", Me.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                Select Case TransactionCheck
                    Case DialogResult.OK

                    Case Else
                        MsgBox("No data was saved", MsgBoxStyle.Information, Me.Text)
                        Exit Sub
                End Select
            End If
            If cboTransactionType.SelectedValue Is Nothing Or cboTransactionType.SelectedValue = "" Then
                MsgBox("A transaction type must be selected before continuing." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If InvoiceCheck() = False Then
                MsgBox("The Invoice Number entered is not valid." & vbCrLf & "No Data saved", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim SQL As String = "Insert into FS_Transactions " &
                    "(TRANSACTIONID, INVOICEID, TRANSACTIONTYPECODE, DATTRANSACTIONDATE, " &
                    "NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, STRBATCHNO, " &
                    "STRENTRYPERSON, STRCOMMENT, ACTIVE, UPDATEUSER, " &
                    "UPDATEDATETIME, CREATEDATETIME, STRAIRSNUMBER, NUMFEEYEAR, " &
                    "STRCREDITCARDNO) " &
                    " values " &
                    "(NEXT VALUE FOR SEQ_FS_TRANSACTIONS, @INVOICEID, @TRANSACTIONTYPECODE, @DATTRANSACTIONDATE, " &
                    "@NUMPAYMENT, @STRCHECKNO, @STRDEPOSITNO, @STRBATCHNO, " &
                    "@STRENTRYPERSON, @STRCOMMENT, '1', @UPDATEUSER, " &
                    "getdate(), getdate(), @STRAIRSNUMBER, @NUMFEEYEAR, " &
                    "@STRCREDITCARDNO) "


            If txtInvoiceID.Text <> "" Then
                Dim params As SqlParameter() = {
                    New SqlParameter("@INVOICEID", txtInvoiceID.Text),
                    New SqlParameter("@TRANSACTIONTYPECODE", cboTransactionType.SelectedValue),
                    New SqlParameter("@DATTRANSACTIONDATE", dtpTransactionDate.Value),
                    New SqlParameter("@NUMPAYMENT", Replace(Replace(txtTransactionAmount.Text, ",", ""), "$", "")),
                    New SqlParameter("@STRCHECKNO", txtTransactionCheckNo.Text),
                    New SqlParameter("@STRDEPOSITNO", txtDepositNo.Text),
                    New SqlParameter("@STRBATCHNO", txtBatchNo.Text),
                    New SqlParameter("@STRENTRYPERSON", CurrentUser.UserID),
                    New SqlParameter("@STRCOMMENT", txtAPBComments.Text),
                    New SqlParameter("@UPDATEUSER", CurrentUser.UserID),
                    New SqlParameter("@STRAIRSNUMBER", AirsNumber.DbFormattedString),
                    New SqlParameter("@NUMFEEYEAR", FeeYear),
                    New SqlParameter("@STRCREDITCARDNO", txtTransactionCreditCardNo.Text)
                }

                DB.RunCommand(SQL, params)
            Else
                Dim params As SqlParameter() = {
                    New SqlParameter("@INVOICEID", ""),
                    New SqlParameter("@TRANSACTIONTYPECODE", cboTransactionType.SelectedValue),
                    New SqlParameter("@DATTRANSACTIONDATE", dtpTransactionDate.Value),
                    New SqlParameter("@NUMPAYMENT", Replace(Replace(txtTransactionAmount.Text, ",", ""), "$", "")),
                    New SqlParameter("@STRCHECKNO", txtTransactionCheckNo.Text),
                    New SqlParameter("@STRDEPOSITNO", txtDepositNo.Text),
                    New SqlParameter("@STRBATCHNO", txtBatchNo.Text),
                    New SqlParameter("@STRENTRYPERSON", CurrentUser.UserID),
                    New SqlParameter("@STRCOMMENT", txtAPBComments.Text),
                    New SqlParameter("@UPDATEUSER", CurrentUser.UserID),
                    New SqlParameter("@STRAIRSNUMBER", AirsNumber.DbFormattedString),
                    New SqlParameter("@NUMFEEYEAR", FeeYear),
                    New SqlParameter("@STRCREDITCARDNO", txtTransactionCreditCardNo.Text)
                }

                DB.RunCommand(SQL, params)
            End If

            SQL = "Select max(TransactionID) " &
            "from FS_TRANSACTIONS "

            Dim dr As DataRow = DB.GetDataRow(SQL)

            If dr Is Nothing OrElse IsDBNull(dr.Item(0)) Then
                txtTransactionID.Text = "Error"
            Else
                txtTransactionID.Text = dr.Item(0)
            End If

            InvoiceStatusCheck(txtInvoiceID.Text)
            If Not DAL.Update_FS_Admin_Status(FeeYear, AirsNumber.ShortString) Then
                MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            RefreshAdminStatus()

            LoadTransactionData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvTransactions_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvTransactions.MouseUp
        Try

            Dim hti As DataGridView.HitTestInfo = dgvTransactions.HitTest(e.X, e.Y)

            If dgvTransactions.RowCount > 0 And hti.RowIndex <> -1 Then
                If IsDBNull(dgvTransactions(0, hti.RowIndex).Value) Then
                    txtTransactionID.Clear()
                Else
                    txtTransactionID.Text = dgvTransactions(0, hti.RowIndex).Value
                End If


                If IsDBNull(dgvTransactions(1, hti.RowIndex).Value) Then
                    txtInvoiceID.Clear()
                Else
                    txtInvoiceID.Text = dgvTransactions(1, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTransactions(2, hti.RowIndex).Value) Then
                    dtpTransactionDate.Text = OracleDate
                Else
                    dtpTransactionDate.Text = dgvTransactions(2, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTransactions(3, hti.RowIndex).Value) Then
                    txtTransactionAmount.Clear()
                Else
                    txtTransactionAmount.Text = Format(dgvTransactions(3, hti.RowIndex).Value, "c")
                End If
                If IsDBNull(dgvTransactions(4, hti.RowIndex).Value) Then
                    txtTransactionCheckNo.Clear()
                Else
                    txtTransactionCheckNo.Text = dgvTransactions(4, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTransactions(5, hti.RowIndex).Value) Then
                    txtDepositNo.Clear()
                Else
                    txtDepositNo.Text = dgvTransactions(5, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTransactions(6, hti.RowIndex).Value) Then
                    txtBatchNo.Clear()
                Else
                    txtBatchNo.Text = dgvTransactions(6, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTransactions(7, hti.RowIndex).Value) Then
                    txtTransactionCreatedBy.Clear()
                Else
                    txtTransactionCreatedBy.Text = dgvTransactions(7, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTransactions(8, hti.RowIndex).Value) Then
                    txtAPBComments.Clear()
                Else
                    txtAPBComments.Text = dgvTransactions(8, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTransactions(9, hti.RowIndex).Value) Then
                    txtTransactionCreditCardNo.Clear()
                Else
                    txtTransactionCreditCardNo.Text = dgvTransactions(9, hti.RowIndex).Value
                End If


                If IsDBNull(dgvTransactions(10, hti.RowIndex).Value) Then
                    cboTransactionType.SelectedValue = 0
                Else
                    cboTransactionType.SelectedValue = dgvTransactions(10, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTransactions(11, hti.RowIndex).Value) Then
                    txtTransactionUpdated.Clear()
                Else
                    txtTransactionUpdated.Text = dgvTransactions(11, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTransactions(12, hti.RowIndex).Value) Then
                    dtpTransactionUpdated.Text = OracleDate
                Else
                    dtpTransactionUpdated.Text = dgvTransactions(12, hti.RowIndex).Value
                End If
                If IsDBNull(dgvTransactions(13, hti.RowIndex).Value) Then
                    dtpTransactionCreated.Text = OracleDate
                Else
                    dtpTransactionCreated.Text = dgvTransactions(13, hti.RowIndex).Value
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearTransactions_Click(sender As Object, e As EventArgs) Handles btnClearTransactions.Click
        txtTransactionID.Clear()
        txtInvoiceID.Clear()
        txtDepositNo.Clear()
        txtBatchNo.Clear()
        txtTransactionCreatedBy.Clear()
        cboTransactionType.SelectedValue = 0
        dtpTransactionDate.Value = Today
        txtTransactionAmount.Clear()
        txtTransactionCheckNo.Clear()
        txtTransactionCreditCardNo.Clear()
        txtAPBComments.Clear()
        txtTransactionUpdated.Clear()
        dtpTransactionUpdated.Value = Today
        dtpTransactionCreated.Value = Today
    End Sub

    Private Sub btnClearEditableTransactionData_Click(sender As Object, e As EventArgs) Handles btnClearEditableTransactionData.Click
        txtDepositNo.Clear()
        txtBatchNo.Clear()
        txtTransactionCreatedBy.Clear()
        cboTransactionType.SelectedValue = 0
        dtpTransactionDate.Value = Today
        txtTransactionAmount.Clear()
        txtTransactionCheckNo.Clear()
        txtTransactionCreditCardNo.Clear()
        txtAPBComments.Clear()
        txtTransactionUpdated.Clear()
        dtpTransactionUpdated.Value = Today
        dtpTransactionCreated.Value = Today
    End Sub

    Private Sub btnTransactionUpdate_Click(sender As Object, e As EventArgs) Handles btnTransactionUpdate.Click
        Try

            If (mtbAirsNumber.Text <> Me.AirsNumber.FormattedString) _
                Or (FeeYearsComboBox.SelectedItem.ToString <> txtYear.Text) _
                Or txtAIRSNumber.Text = "" Or FeeYearsComboBox.SelectedIndex = 0 _
                Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." &
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If txtTransactionID.Text = "" Then
                MsgBox("Please select a valid transaction to update." & vbCrLf & "No data modified", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If cboTransactionType.SelectedValue Is Nothing Or cboTransactionType.SelectedValue = "" Then
                MsgBox("A transaction type must be selected before continuing." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If InvoiceCheck() = False Then
                MsgBox("The Invoice Number entered is not valid." & vbCrLf & "No Data saved", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim SQL As String = "Update FS_Transactions set " &
            "invoiceid = @invoiceid, " &
            "TransactionTypecode = @TransactionTypecode, " &
            "datTransactionDate = @datTransactionDate, " &
            "numPayment = @numPayment, " &
            "strCheckNo = @strCheckNo, " &
            "strDepositNo = @strDepositNo, " &
            "strBatchNo = @strBatchNo, " &
            "strComment = @strComment, " &
            "active = '1', " &
            "updateUser = @updateUser, " &
            "updateDateTime = getdate(), " &
            "strCreditCardNo = @strCreditCardNo " &
            "where TransactionID = @TransactionID "

            Dim p As SqlParameter() = {
                New SqlParameter("@invoiceid", txtInvoiceID.Text),
                New SqlParameter("@TransactionTypecode", cboTransactionType.SelectedValue),
                New SqlParameter("@datTransactionDate", dtpTransactionDate.Text),
                New SqlParameter("@numPayment", Replace(Replace(txtTransactionAmount.Text, ",", ""), "$", "")),
                New SqlParameter("@strCheckNo", txtTransactionCheckNo.Text),
                New SqlParameter("@strDepositNo", txtDepositNo.Text),
                New SqlParameter("@strBatchNo", txtBatchNo.Text),
                New SqlParameter("@strComment", txtAPBComments.Text),
                New SqlParameter("@updateUser", CurrentUser.UserID),
                New SqlParameter("@strCreditCardNo", txtTransactionCreditCardNo.Text),
                New SqlParameter("@TransactionID", txtTransactionID.Text)
            }

            DB.RunCommand(SQL, p)

            InvoiceStatusCheck(txtInvoiceID.Text)

            If Not DAL.Update_FS_Admin_Status(Me.FeeYear, Me.AirsNumber.ShortString) Then
                MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            RefreshAdminStatus()

            LoadTransactionData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnTransactionDelete_Click(sender As Object, e As EventArgs) Handles btnTransactionDelete.Click
        Try

            If (mtbAirsNumber.Text <> Me.AirsNumber.FormattedString) _
                Or (FeeYearsComboBox.SelectedItem.ToString <> txtYear.Text) _
                Or txtAIRSNumber.Text = "" Or FeeYearsComboBox.SelectedIndex = 0 _
                Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." &
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If txtTransactionID.Text = "" Then
                MsgBox("Please select a valid transaction to update." & vbCrLf & "No data modified", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If InvoiceCheck() = False Then
                MsgBox("The Invoice Number entered is not valid." & vbCrLf & "No Data saved", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim SQL As String = "Update FS_Transactions set " &
            "active = '0', " &
            "updateUser = @updateUser, " &
            "updateDateTime = getdate() " &
            "where TransactionID = @TransactionID "

            Dim p As SqlParameter() = {
                New SqlParameter("@updateUser", CurrentUser.UserID),
                New SqlParameter("@TransactionID", txtTransactionID.Text)
            }

            DB.RunCommand(SQL, p)

            InvoiceStatusCheck(txtInvoiceID.Text)
            If Not DAL.Update_FS_Admin_Status(Me.FeeYear, Me.AirsNumber.ShortString) Then
                MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            RefreshAdminStatus()

            LoadTransactionData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub InvoiceStatusCheck(invoiceID As String)
        Try
            Dim temp As String

            Dim SQL As String = "select " &
            "(invoiceTotal - PaymentTotal) as Balance " &
            "from (select " &
            "sum(numAmount) as InvoiceTotal " &
            "from FS_Feeinvoice " &
            "where invoiceId = @invoiceId " &
            "and Active = '1' ) INVOICED, " &
            "(select " &
            "case " &
            "when sum(NumPayment) is null then 0 " &
            "else sum(numPayment) " &
            "End PaymentTotal " &
            "from FS_TRANSACTIONS " &
            "where invoiceId = @invoiceId " &
            "and Active = '1' ) Payments "

            Dim p As New SqlParameter("@invoiceId", invoiceID)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr Is Nothing OrElse IsDBNull(dr.Item("Balance")) Then
                temp = "1"
            Else
                temp = dr.Item("Balance")
            End If

            If temp <> "0" Then
                'Not Paid in full
                SQL = "Update FS_FeeInvoice set " &
                "strInvoicestatus = '0' " &
                "where invoiceId = @invoiceId "
            Else
                'Paid in Full 
                SQL = "Update FS_FeeInvoice set " &
                "strInvoicestatus = '1' " &
                "where invoiceId = @invoiceId "
            End If

            DB.RunCommand(SQL, p)

            LoadFeeInvoiceData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveFeeAudit_Click(sender As Object, e As EventArgs) Handles btnSaveNewFeeAudit.Click
        Try
            Dim SQL As String
            Dim OpStatus As String = ""
            Dim ShutDown As String = ""
            Dim Classification As String = ""
            Dim VOCTons As String = ""
            Dim PMTons As String = ""
            Dim SO2Tons As String = ""
            Dim NOxTons As String = ""
            Dim FeeRate As String = ""
            Dim Part70Fee As String = ""
            Dim CalculatedFee As String = ""
            Dim SMFee As String = ""
            Dim NSPSFee As String = ""
            Dim AdminFee As String = ""
            Dim TotalFee As String = ""
            Dim SM As String = ""
            Dim Part70 As String = ""
            Dim NSPS As String = ""
            Dim PaymentType As String = ""
            Dim OfficialName As String = ""
            Dim OfficialTitle As String = ""
            Dim NSPSExempt As String = ""
            Dim NSPSExemptions As String = ""
            Dim StaffResponsible As String = ""
            Dim AuditLevel As String = ""
            Dim AuditEnforcement As Integer = 0
            Dim AuditComments As String = ""
            Dim AuditStart As String = ""
            Dim AuditEnd As String = ""
            Dim EndCollections As String = ""
            Dim CollectionsDate As String = ""

            If (mtbAirsNumber.Text <> Me.AirsNumber.FormattedString) _
                Or (FeeYearsComboBox.SelectedItem.ToString <> txtYear.Text) _
                Or txtAIRSNumber.Text = "" Or FeeYearsComboBox.SelectedIndex = 0 _
                Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." &
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If rdbEditOpStatusTrue.Checked = True Or rdbEditOpStatusFalse.Checked = True Then
                If rdbEditOpStatusTrue.Checked = True Then
                    OpStatus = "1"
                Else
                    OpStatus = "0"
                End If
            End If
            If dtpEditShutDownDate.Checked = True Then
                ShutDown = dtpEditShutDownDate.Text
            Else
                ShutDown = ""
            End If
            If cboEditClassification.Text <> "" Then
                If cboEditClassification.Items.Contains(cboEditClassification.Text) Then
                    Classification = cboEditClassification.Text
                Else
                    Classification = ""
                End If
            Else
                Classification = ""
            End If
            If txtEditVOCTons.Text <> "" Then
                VOCTons = txtEditVOCTons.Text
            Else
                VOCTons = ""
            End If
            If txtEditPMTons.Text <> "" Then
                PMTons = txtEditPMTons.Text
            Else
                PMTons = ""
            End If
            If txtEditSO2Tons.Text <> "" Then
                SO2Tons = txtEditSO2Tons.Text
            Else
                SO2Tons = ""
            End If
            If txtEditNOxTons.Text <> "" Then
                NOxTons = txtEditNOxTons.Text
            Else
                NOxTons = ""
            End If
            If txtEditFeeRate.Text <> "" Then
                FeeRate = txtEditFeeRate.Text
            Else
                FeeRate = ""
            End If
            If txtEditCalculatedFee.Text <> "" Then
                CalculatedFee = txtEditCalculatedFee.Text
            Else
                CalculatedFee = ""
            End If
            If txtEditPart70Fee.Text <> "" Then
                Part70Fee = txtEditPart70Fee.Text
            Else
                Part70Fee = ""
            End If
            If txtEditSMFee.Text <> "" Then
                SMFee = txtEditSMFee.Text
            Else
                SMFee = ""
            End If
            If txtEditNSPSFee.Text <> "" Then
                NSPSFee = txtEditNSPSFee.Text
            Else
                NSPSFee = ""
            End If
            If txtEditAdminFee.Text <> "" Then
                AdminFee = txtEditAdminFee.Text
            Else
                AdminFee = ""
            End If
            If txtEditTotalFees.Text <> "" Then
                TotalFee = txtEditTotalFees.Text
            Else
                TotalFee = ""
            End If
            If rdbEditSMTrue.Checked = True Or rdbEditSMFalse.Checked = True Then
                If rdbEditSMTrue.Checked = True Then
                    SM = "1"
                Else
                    SM = "0"
                End If
            End If
            If rdbEditPart70True.Checked = True Or rdbEditPart70False.Checked = True Then
                If rdbEditPart70True.Checked = True Then
                    Part70 = "1"
                Else
                    Part70 = "0"
                End If
            End If
            If rdbEditNSPSTrue.Checked = True Or rdbEditNSPSFalse.Checked = True Then
                If rdbEditNSPSTrue.Checked = True Then
                    NSPS = "1"
                Else
                    NSPS = "0"
                End If
            End If
            If cboEditPaymentType.Text <> "" Then
                If cboEditPaymentType.Items.Contains(cboEditPaymentType.Text) Then
                    PaymentType = cboEditPaymentType.Text
                Else
                    PaymentType = ""
                End If
            Else
                PaymentType = ""
            End If
            If txtEditOfficialName.Text <> "" Then
                OfficialName = txtEditOfficialName.Text
            Else
                OfficialName = ""
            End If
            If txtEditOfficialTitle.Text <> "" Then
                OfficialTitle = txtEditOfficialTitle.Text
            Else
                OfficialTitle = ""
            End If
            If rdbEditNSPSExemptTrue.Checked = True Or rdbEditNSPSExemptFalse.Checked = True Then
                If rdbEditNSPSExemptTrue.Checked = True Then
                    NSPSExempt = "1"
                Else
                    NSPSExempt = "0"
                End If
                If NSPSExempt = "1" Then
                    For i As Integer = 0 To dgvEditExemptions.Rows.Count - 1
                        If dgvEditExemptions(0, i).Value = True Then
                            NSPSExemptions = NSPSExemptions & dgvEditExemptions(1, i).Value & ","
                        End If
                    Next
                    If NSPSExemptions.Length > 1 Then
                        NSPSExemptions = Mid(NSPSExemptions, 1, NSPSExemptions.Length - 1)
                    End If
                Else
                    NSPSExemptions = ""
                End If
            End If

            SQL = "select count(*) as DataCheck " &
            "From FS_FeeData " &
            "where strAIRSNumber = @airs " &
            "and numFeeYear = @year "

            Dim p2 As SqlParameter() = {
                New SqlParameter("@year", FeeYear),
                New SqlParameter("@airs", AirsNumber.DbFormattedString)
            }

            If DB.GetSingleValue(Of Integer)(SQL, p2) = 0 Then
                SQL = "insert into FS_FeeData " &
                "(numfeeyear, strairsnumber, " &
                "strComment, Active, " &
                "UpdateUser, UpdateDateTime, " &
                "CreateDateTime) " &
                "values " &
                "(@numfeeyear, @strairsnumber, " &
                "@strComment, '1', " &
                "@UpdateUser, getdate(), " &
                "getdate() ) "

                Dim p As SqlParameter() = {
                    New SqlParameter("@numfeeyear", FeeYear),
                    New SqlParameter("@strairsnumber", AirsNumber.DbFormattedString),
                    New SqlParameter("@strComment", "Add Via IAIP Audit Process"),
                    New SqlParameter("@UpdateUser", "IAIP||" & CurrentUser.AlphaName)
                }

                DB.RunCommand(SQL, p)
            End If

            If cboStaffResponsible.SelectedValue <> "" Then
                StaffResponsible = cboStaffResponsible.SelectedValue
            Else
                StaffResponsible = CurrentUser.UserID
            End If
            If StaffResponsible = "" Then
                StaffResponsible = CurrentUser.UserID
            End If
            Select Case cboAuditType.Text
                Case "Facility Self Amendment"
                    AuditLevel = "0"
                Case "Level 1 Audit"
                    AuditLevel = "1"
                Case "Level 2 Audit"
                    AuditLevel = "2"
                Case "Level 3 Audit"
                    AuditLevel = "3"
                Case "Other"
                    AuditLevel = "4"
                Case Else
                    AuditLevel = "4"
            End Select

            Integer.TryParse(txtAuditEnforcementNumber.Text, AuditEnforcement)
            If txtAuditComment.Text <> "" Then
                AuditComments = txtAuditComment.Text
            End If
            AuditStart = Format(DTPAuditStart.Value, "dd-MMM-yyyy")
            If DTPAuditEnd.Checked = True Then
                AuditEnd = Format(DTPAuditEnd.Value, "dd-MMM-yyyy")
            Else
                AuditEnd = ""
            End If
            If chbEndFeeCollectoins.Checked = True Then
                EndCollections = "True"
                CollectionsDate = Format(DTPDateCollectionsCeased.Value, "dd-MMM-yyyy")
            Else
                EndCollections = "False"
            End If

            SQL = "Insert into FS_FeeAudit " &
                "( AUDITID, NUMSTAFFRESPONSIBLE, STRAUDITLEVEL, NUMENFORCEMENT, " &
                "STRCOMMENTS, DATAUDITSTART, DATAUDITEND, STRENDCOLLECTIONS, " &
                "DATCOLLECTIONSENDED, ACTIVE, UPDATEUSER, UPDATEDATETIME, " &
                "CREATEDATETIME, STRAIRSNUMBER, NUMFEEYEAR) " &
                "values " &
                "( " &
                "(SELECT CASE " &
                "WHEN MAX(AuditID) IS NULL " &
                "THEN 1 " &
                "ELSE MAX(AuditID) + 1 " &
                "END AS AuditID " &
                "FROM FS_FeeAudit), " &
                "@NUMSTAFFRESPONSIBLE, @STRAUDITLEVEL, @NUMENFORCEMENT, " &
                "@STRCOMMENTS, @DATAUDITSTART, @DATAUDITEND, @STRENDCOLLECTIONS, " &
                "@DATCOLLECTIONSENDED, '1', @UPDATEUSER, getdate(), " &
                "getdate(), @STRAIRSNUMBER, @NUMFEEYEAR) "
            Dim p3 As SqlParameter() = {
                New SqlParameter("@NUMSTAFFRESPONSIBLE", StaffResponsible),
                New SqlParameter("@STRAUDITLEVEL", AuditLevel),
                New SqlParameter("@NUMENFORCEMENT", If(AuditEnforcement = 0, DBNull.Value, AuditEnforcement)),
                New SqlParameter("@STRCOMMENTS", AuditComments),
                New SqlParameter("@DATAUDITSTART", AuditStart),
                New SqlParameter("@DATAUDITEND", AuditEnd),
                New SqlParameter("@STRENDCOLLECTIONS", EndCollections),
                New SqlParameter("@DATCOLLECTIONSENDED", CollectionsDate),
                New SqlParameter("@UPDATEUSER", CurrentUser.UserID),
                New SqlParameter("@STRAIRSNUMBER", AirsNumber.DbFormattedString),
                New SqlParameter("@NUMFEEYEAR", FeeYear)
            }
            DB.RunCommand(SQL, p3)

            SQL = "select max(AuditID) as AuditID " &
            "from  FS_FeeAudit "

            txtAuditID.Text = DB.GetSingleValue(Of Integer)(SQL)

            If chbMakeEdits.Checked = True Then
                SQL = "INSERT INTO FS_FEEAMENDMENT " &
                    "( AUDITID, STRAIRSNUMBER, NUMFEEYEAR, STRSYNTHETICMINOR, " &
                    "NUMSMFEE, STRPART70, NUMPART70FEE, INTVOCTONS, " &
                    "INTPMTONS, INTSO2TONS, INTNOXTONS, NUMCALCULATEDFEE, " &
                    "NUMFEERATE, STRNSPS, NUMNSPSFEE, STRNSPSEXEMPT, " &
                    "STRNSPSEXEMPTREASON, NUMADMINFEE, NUMTOTALFEE, STRCLASS, " &
                    "STROPERATE, DATSHUTDOWN, STROFFICIALNAME, STROFFICIALTITLE, " &
                    "STRPAYMENTPLAN, ACTIVE, UPDATEUSER, UPDATEDATETIME, CREATEDATETIME ) " &
                    "VALUES " &
                    "( @AUDITID, @STRAIRSNUMBER, @NUMFEEYEAR, @STRSYNTHETICMINOR, " &
                    "@NUMSMFEE, @STRPART70, @NUMPART70FEE, @INTVOCTONS, " &
                    "@INTPMTONS, @INTSO2TONS, @INTNOXTONS, @NUMCALCULATEDFEE, " &
                    "@NUMFEERATE, @STRNSPS, @NUMNSPSFEE, @STRNSPSEXEMPT, " &
                    "@STRNSPSEXEMPTREASON, @NUMADMINFEE, @NUMTOTALFEE, @STRCLASS, " &
                    "@STROPERATE, @DATSHUTDOWN, @STROFFICIALNAME, @STROFFICIALTITLE, " &
                    "@STRPAYMENTPLAN, @ACTIVE, @UPDATEUSER, getdate(), getdate() "
                Dim p4 As SqlParameter() = {
                    New SqlParameter("@AUDITID", txtAuditID.Text),
                    New SqlParameter("@STRAIRSNUMBER", AirsNumber.DbFormattedString),
                    New SqlParameter("@NUMFEEYEAR", FeeYear),
                    New SqlParameter("@STRSYNTHETICMINOR", SM),
                    New SqlParameter("@NUMSMFEE", If(SMFee = "", DBNull.Value, SMFee)),
                    New SqlParameter("@STRPART70", Part70),
                    New SqlParameter("@NUMPART70FEE", If(Part70Fee = "", DBNull.Value, Part70Fee)),
                    New SqlParameter("@INTVOCTONS", If(VOCTons = "", DBNull.Value, VOCTons)),
                    New SqlParameter("@INTPMTONS", If(PMTons = "", DBNull.Value, PMTons)),
                    New SqlParameter("@INTSO2TONS", If(SO2Tons = "", DBNull.Value, SO2Tons)),
                    New SqlParameter("@INTNOXTONS", If(NOxTons = "", DBNull.Value, NOxTons)),
                    New SqlParameter("@NUMCALCULATEDFEE", If(CalculatedFee = "", DBNull.Value, CalculatedFee)),
                    New SqlParameter("@NUMFEERATE", If(FeeRate = "", DBNull.Value, FeeRate)),
                    New SqlParameter("@STRNSPS", NSPS),
                    New SqlParameter("@NUMNSPSFEE", If(NSPSFee = "", DBNull.Value, NSPSFee)),
                    New SqlParameter("@STRNSPSEXEMPT", NSPSExempt),
                    New SqlParameter("@STRNSPSEXEMPTREASON", NSPSExemptions),
                    New SqlParameter("@NUMADMINFEE", If(AdminFee = "", DBNull.Value, AdminFee)),
                    New SqlParameter("@NUMTOTALFEE", If(TotalFee = "", DBNull.Value, TotalFee)),
                    New SqlParameter("@STRCLASS", Classification),
                    New SqlParameter("@STROPERATE", OpStatus),
                    New SqlParameter("@DATSHUTDOWN", ShutDown),
                    New SqlParameter("@STROFFICIALNAME", OfficialName),
                    New SqlParameter("@STROFFICIALTITLE", OfficialTitle),
                    New SqlParameter("@STRPAYMENTPLAN", PaymentType),
                    New SqlParameter("@ACTIVE", "1"),
                    New SqlParameter("@UPDATEUSER", CurrentUser.UserID)
                }
                DB.RunCommand(SQL, p4)

                Dim p5 As SqlParameter() = {
                    New SqlParameter("@AIRSNumber", AirsNumber.DbFormattedString),
                    New SqlParameter("@FeeYear", FeeYear)
                }
                DB.SPRunCommand("PD_FeeAmendment", p5)
            End If

            If EndCollections = "True" Then
                SQL = "update FS_Admin set " &
                "numCurrentStatus = '12' " &
                "where numFeeYear = @AIRSNumber " &
                "and strAIRSNumber = @FeeYear "
                Dim p6 As SqlParameter() = {
                    New SqlParameter("@AIRSNumber", AirsNumber.DbFormattedString),
                    New SqlParameter("@FeeYear", FeeYear)
                }
                DB.RunCommand(SQL, p6)
            End If

            LoadAuditedData()
            MsgBox("Audit Data Added", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, SQL, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub ClearEditData()
        Try
            rdbEditOpStatusTrue.Checked = False
            rdbEditOpStatusFalse.Checked = False
            'chbEditOpStatus.Checked = False
            dtpEditShutDownDate.Text = OracleDate
            dtpEditShutDownDate.Checked = False
            cboEditClassification.Text = ""
            txtEditVOCTons.Clear()
            txtEditPMTons.Clear()
            txtEditSO2Tons.Clear()
            txtEditNOxTons.Clear()
            txtEditFeeRate.Clear()
            txtEditCalculatedFee.Clear()
            txtEditPart70Fee.Clear()
            txtEditSMFee.Clear()
            txtEditNSPSFee.Clear()
            txtEditAdminFee.Clear()
            txtEditTotalFees.Clear()
            rdbEditSMTrue.Checked = False
            rdbEditSMFalse.Checked = False
            rdbEditPart70True.Checked = False
            rdbEditPart70False.Checked = False
            rdbEditNSPSTrue.Checked = False
            rdbEditNSPSFalse.Checked = False
            cboEditPaymentType.Text = ""
            txtEditOfficialName.Clear()
            txtEditOfficialTitle.Clear()
            rdbEditNSPSExemptTrue.Checked = False
            rdbEditNSPSExemptFalse.Checked = False

            dgvEditExemptions.Rows.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbEditNSPSExemptTrue_CheckedChanged(sender As Object, e As EventArgs) Handles rdbEditNSPSExemptTrue.CheckedChanged
        Try
            Dim dgvRow As New DataGridViewRow

            If rdbEditNSPSExemptTrue.Checked = False Then
                Exit Sub
            End If

            Dim SQL As String = "select " &
            "FSLK_NspsReason.NSPSREasonCode , Description " &
            "from FSLK_NSPSReason inner join fslk_NSPSReasonYear " &
            "on FSLK_NspsReason.NSPSREasonCode = FSLK_NSPSREasonYear.nspsreasoncode  " &
            "where numFeeYear = 2009 " &
            "order by displayorder "

            Dim dt As DataTable = DB.GetDataTable(SQL)

            dgvEditExemptions.Rows.Clear()

            For Each dr As DataRow In dt.Rows
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEditExemptions)
                dgvRow.Cells(0).Value = 0
                dgvRow.Cells(1).Value = dr.Item("NSPSReasonCode")
                dgvRow.Cells(2).Value = dr.Item("description")
                dgvEditExemptions.Rows.Add(dgvRow)
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvEditExemptions_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvEditExemptions.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvEditExemptions.HitTest(e.X, e.Y)
            Dim i As Integer = 0

            If hti.RowIndex = -1 And hti.ColumnIndex <> -1 Then
                If dgvEditExemptions.Columns(hti.ColumnIndex).HeaderText = "" Then
                    If dgvEditExemptions(0, 0).Value = True Then
                        For i = 0 To dgvEditExemptions.Rows.Count - 1
                            dgvEditExemptions(0, i).Value = False
                        Next
                    Else
                        For i = 0 To dgvEditExemptions.Rows.Count - 1
                            dgvEditExemptions(0, i).Value = True
                        Next
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

    Private Sub btnGenerateInvoice_Click(sender As Object, e As EventArgs) Handles btnViewPrintableInvoice.Click
        Try
            Dim rpt As New ReportClass
            Dim Director As String = DAL.GetEpdManagerName(DAL.EpdManagementTypes.EpdDirector)
            Dim Commissioner As String = DAL.GetEpdManagerName(DAL.EpdManagementTypes.DnrCommissioner)
            Dim TotalEmissionFees As String = ""
            Dim InvoiceDate As Date = Today
            Dim PayType As String = ""
            Dim PayTypeID As String = ""
            Dim TotalInvoiceAmount As String = ""
            Dim TotalFee As String = ""
            Dim AdminFee As String = ""
            Dim DueDate As Date = Today
            Dim VOIDStatus As String = ""
            Dim ParameterFields As ParameterFields
            Dim ParameterField As ParameterField
            Dim spValue As ParameterDiscreteValue

            If txtInvoice.Text = "" Then
                MsgBox("Please select an existing invoice to Print.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim SQL As String = "Select " &
            "numAmount, datInvoiceDate, " &
            "strPayType, strPayTypeDesc, " &
            "FS_FeeInvoice.CreateDateTime, " &
            "FS_FeeInvoice.Active " &
            "from FS_FeeInvoice " &
            "inner join FSLK_PayType " &
            "on FS_FeeInvoice.strPayType = " &
               "FSLK_PayType.numPayTypeID " &
               "where InvoiceID = @InvoiceID " &
               "and strAIRSNumber = @strAIRSNumber " &
               "and numFeeYear = @numFeeYear "

            Dim p As SqlParameter() = {
                New SqlParameter("@InvoiceID", txtInvoice.Text),
                New SqlParameter("@strAIRSNumber", AirsNumber.DbFormattedString),
                New SqlParameter("@numFeeYear", FeeYear)
            }

            Dim dr As DataRow = DB.GetDataRow(SQL, p)
            If dr IsNot Nothing Then
                If Not IsDBNull(dr.Item("datInvoiceDate")) Then
                    DueDate = dr.Item("datInvoiceDate")
                End If
                If IsDBNull(dr.Item("strPayTypeDesc")) Then
                    PayType = ""
                Else
                    PayType = dr.Item("strPayTypeDesc")
                End If
                If IsDBNull(dr.Item("numAmount")) Then
                    TotalInvoiceAmount = ""
                Else
                    TotalInvoiceAmount = Format(dr.Item("numAmount"), "c")
                End If
                If IsDBNull(dr.Item("strPayType")) Then
                    PayTypeID = "1"
                Else
                    PayTypeID = dr.Item("strPayType")
                End If
                If Not IsDBNull(dr.Item("CreateDateTime")) Then
                    InvoiceDate = dr.Item("CreateDateTime")
                End If
                If IsDBNull(dr.Item("Active")) Then
                    VOIDStatus = "1"
                Else
                    VOIDStatus = dr.Item("Active")
                End If
            End If

            SQL = "Select " &
            "numTotalFee, numAdminFee, " &
            "(numTotalFee - numAdminFee) as TotalEmissionFees " &
            "from FS_FeeAuditedData " &
            "where numFeeYear = @year " &
            "and strAIRSNumber = @airs "
            Dim p2 As SqlParameter() = {
                New SqlParameter("@year", FeeYear),
                New SqlParameter("@airs", AirsNumber.DbFormattedString)
            }

            Dim dr2 As DataRow = DB.GetDataRow(SQL, p2)
            If dr2 IsNot Nothing Then
                If IsDBNull(dr2.Item("TotalEmissionFees")) Then
                    TotalEmissionFees = "0"
                Else
                    TotalEmissionFees = Format(dr2.Item("TotalEmissionFees"), "c")
                End If
                If IsDBNull(dr2.Item("numTotalFee")) Then
                    TotalFee = "0"
                Else
                    If PayTypeID = "2" Or PayTypeID = "3" Or PayTypeID = "4" Or PayTypeID = "5" Then
                        TotalFee = Format(dr2.Item("numTotalFee") / 4, "c")
                    Else
                        TotalFee = Format(dr2.Item("numTotalFee"), "c")
                    End If
                End If
                If IsDBNull(dr2.Item("numAdminFee")) Then
                    AdminFee = "$0.00"
                Else
                    AdminFee = Format(dr2.Item("numAdminFee"), "c")
                End If
            End If

            SQL = "Select " &
            "datTransactionDate, numPayment, " &
            "strCheckNo, strCreditCardNo, " &
            "strDepositNo " &
            "From FS_Transactions " &
            "where strAIRSNumber = @airs  " &
            "and numFeeYear = @year " &
            "and Active = '1' "

            Dim dt As DataTable = DB.GetDataTable(SQL, p2)

            rpt = New crFS_Invoice
            monitor.TrackFeature("Report." & rpt.ResourceName)

            'Do this just once at the start
            ParameterFields = New ParameterFields

            'Do this at the beginning of every new entry 
            ParameterField = New ParameterField
            spValue = New ParameterDiscreteValue

            ParameterField.ParameterFieldName = "Director"
            spValue.Value = Director
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New ParameterField
            spValue = New ParameterDiscreteValue

            ParameterField.ParameterFieldName = "Commissioner"
            spValue.Value = Commissioner
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New ParameterField
            spValue = New ParameterDiscreteValue

            ParameterField.ParameterFieldName = "AIRSNumber"
            spValue.Value = mtbAirsNumber.Text
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New ParameterField
            spValue = New ParameterDiscreteValue

            ParameterField.ParameterFieldName = "FeeYear"
            spValue.Value = Me.FeeYear
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New ParameterField
            spValue = New ParameterDiscreteValue

            ParameterField.ParameterFieldName = "TotalEmissionFees"
            spValue.Value = TotalEmissionFees
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New ParameterField
            spValue = New ParameterDiscreteValue

            ParameterField.ParameterFieldName = "datInvoiceDate"
            spValue.Value = Format(InvoiceDate, DateFormat)
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New ParameterField
            spValue = New ParameterDiscreteValue

            ParameterField.ParameterFieldName = "PaymentType"
            spValue.Value = PayType
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New ParameterField
            spValue = New ParameterDiscreteValue

            ParameterField.ParameterFieldName = "TotalInvoiceAmount"
            spValue.Value = TotalInvoiceAmount
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New ParameterField
            spValue = New ParameterDiscreteValue

            ParameterField.ParameterFieldName = "TotalFee"
            spValue.Value = TotalFee
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New ParameterField
            spValue = New ParameterDiscreteValue

            ParameterField.ParameterFieldName = "AdminFee"
            spValue.Value = AdminFee
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New ParameterField
            spValue = New ParameterDiscreteValue

            ParameterField.ParameterFieldName = "InvoiceNumber"
            spValue.Value = txtInvoice.Text
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New ParameterField
            spValue = New ParameterDiscreteValue

            ParameterField.ParameterFieldName = "DueDate"
            spValue.Value = Format(DueDate, DateFormat)
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New ParameterField
            spValue = New ParameterDiscreteValue

            ParameterField.ParameterFieldName = "VOIDStatus"
            spValue.Value = VOIDStatus
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            rpt.SetDataSource(dt)

            'Load Variables into the Fields
            crFeeStatsAndInvoices.ParameterFieldInfo = ParameterFields

            'Display the Report
            crFeeStatsAndInvoices.ReportSource = rpt
            crFeeStatsAndInvoices.Refresh()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub ViewAllInvoices()
        Try
            Dim SQL As String = "select distinct " &
            "FS_FeeInvoice.InvoiceID, " &
            "FS_FeeInvoice.numAmount, " &
            "datInvoiceDate, " &
            "case " &
            "when FS_FeeInvoice.active = '1' then 'Active' " &
            "when FS_FeeInvoice.active = '0' then 'VOID' " &
            "end InvoiceStatus, strPayTypeDesc, " &
            "case " &
            "when strInvoiceStatus = '1' then 'Paid in Full' " &
            "when strInvoiceStatus = '0' and " &
            "(numPayment <> '0' and numPayment is not null and FS_Transactions.active = '1') then 'Partial Payment' " &
            "when strInvoicestatus = '0' then 'Unpaid' " &
            "end PayStatus, " &
            "FS_FeeInvoice.strComment " &
            "from FS_FeeInvoice " &
            "inner join FSLK_PayType " &
            "on FS_FeeInvoice.strPayType = FSLK_PayType.nuMPayTypeID " &
            "left join FS_Transactions " &
            "on FS_FeeInvoice.InvoiceID = FS_Transactions.InvoiceID " &
            "where FS_FeeInvoice.strAIRSNumber = @airs " &
            "and FS_FeeInvoice.numFeeYear = @year "
            Dim params As SqlParameter() = {
                New SqlParameter("@airs", AirsNumber.DbFormattedString),
                New SqlParameter("@year", FeeYear)
            }

            dgvInvoices.DataSource = DB.GetDataTable(SQL, params)

            dgvInvoices.RowHeadersVisible = False
            dgvInvoices.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvInvoices.AllowUserToResizeColumns = True
            dgvInvoices.AllowUserToAddRows = False
            dgvInvoices.AllowUserToDeleteRows = False
            dgvInvoices.AllowUserToOrderColumns = True
            dgvInvoices.AllowUserToResizeRows = True

            dgvInvoices.Columns("InvoiceID").HeaderText = "ID"
            dgvInvoices.Columns("InvoiceID").DisplayIndex = 0
            dgvInvoices.Columns("InvoiceID").Width = 40
            dgvInvoices.Columns("numAmount").HeaderText = "Invoice Amount"
            dgvInvoices.Columns("numAmount").DisplayIndex = 1
            dgvInvoices.Columns("numAmount").Width = 100
            dgvInvoices.Columns("numAmount").DefaultCellStyle.Format = "c"
            dgvInvoices.Columns("datInvoiceDate").HeaderText = "Invoice Date"
            dgvInvoices.Columns("datInvoiceDate").DisplayIndex = 2
            dgvInvoices.Columns("datInvoiceDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvInvoices.Columns("datInvoiceDate").Width = 85
            dgvInvoices.Columns("InvoiceStatus").HeaderText = "Invoice Status"
            dgvInvoices.Columns("InvoiceStatus").DisplayIndex = 3
            dgvInvoices.Columns("strPayTypeDesc").HeaderText = "Invoice Type"
            dgvInvoices.Columns("strPayTypeDesc").DisplayIndex = 4
            dgvInvoices.Columns("PayStatus").HeaderText = "Pay Status"
            dgvInvoices.Columns("PayStatus").DisplayIndex = 5
            dgvInvoices.Columns("strComment").HeaderText = "Comment"
            dgvInvoices.Columns("strComment").DisplayIndex = 6

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewInvoices_Click(sender As Object, e As EventArgs) Handles btnViewInvoices.Click
        ViewAllInvoices()
    End Sub

    Private Sub dgvInvoices_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvInvoices.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvInvoices.HitTest(e.X, e.Y)
            If dgvInvoices.RowCount > 0 And hti.RowIndex <> -1 Then
                If IsDBNull(dgvInvoices(0, hti.RowIndex).Value) Then
                    txtInvoice.Clear()
                Else
                    txtInvoice.Text = dgvInvoices(0, hti.RowIndex).Value
                End If
                If IsDBNull(dgvInvoices(1, hti.RowIndex).Value) Then
                    txtAmount.Clear()
                Else
                    txtAmount.Text = Format(dgvInvoices(1, hti.RowIndex).Value, "c")
                End If
                If IsDBNull(dgvInvoices(2, hti.RowIndex).Value) Then
                    DTPInvoiceDate.Text = OracleDate
                Else
                    DTPInvoiceDate.Text = dgvInvoices(2, hti.RowIndex).Value
                End If
                If IsDBNull(dgvInvoices(3, hti.RowIndex).Value) Then
                    txtStatus.Clear()
                Else
                    txtStatus.Text = dgvInvoices(3, hti.RowIndex).Value
                End If

                If IsDBNull(dgvInvoices(4, hti.RowIndex).Value) Then
                    cboInvoiceType.Text = ""
                Else
                    cboInvoiceType.Text = dgvInvoices(4, hti.RowIndex).Value
                End If
                If IsDBNull(dgvInvoices(5, hti.RowIndex).Value) Then
                    txtPayStatus.Clear()
                Else
                    txtPayStatus.Text = dgvInvoices(5, hti.RowIndex).Value
                End If
                If IsDBNull(dgvInvoices(6, hti.RowIndex).Value) Then
                    txtInvoiceComments.Clear()
                Else
                    txtInvoiceComments.Text = dgvInvoices(6, hti.RowIndex).Value
                End If

            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddNewInvoice_Click(sender As Object, e As EventArgs) Handles btnAddNewInvoice.Click
        Try
            Dim InvoiceStatus As String = "0"

            If (mtbAirsNumber.Text <> Me.AirsNumber.FormattedString) _
                Or (FeeYearsComboBox.SelectedItem.ToString <> txtYear.Text) _
                Or txtAIRSNumber.Text = "" Or FeeYearsComboBox.SelectedIndex = 0 _
                Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." &
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If txtAmount.Text <> "" And cboInvoiceType.Text <> "" Then
                If CInt(txtAmount.Text) = 0 Then
                    InvoiceStatus = "1"
                Else
                    InvoiceStatus = "0"
                End If
            Else
                MsgBox("A valid Invoice Amount, Pay Type and Invoice Date must be selected.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            Dim SQL As String = "Insert into FS_FeeINvoice " &
                "(INVOICEID, STRAIRSNUMBER, NUMFEEYEAR, NUMAMOUNT, " &
                " DATINVOICEDATE, STRCOMMENT, ACTIVE, UPDATEUSER, " &
                " UPDATEDATETIME, CREATEDATETIME, STRPAYTYPE, STRINVOICESTATUS) " &
                " values " &
                "( NEXT VALUE FOR FEEINVOICE_ID, @STRAIRSNUMBER, @NUMFEEYEAR, @NUMAMOUNT, " &
                " @DATINVOICEDATE, @STRCOMMENT, '1', @UPDATEUSER, " &
                " @UPDATEDATETIME, @CREATEDATETIME, @STRPAYTYPE, @STRINVOICESTATUS) "

            Dim p As SqlParameter() = {
                New SqlParameter("@STRAIRSNUMBER", AirsNumber.DbFormattedString),
                New SqlParameter("@NUMFEEYEAR", FeeYear),
                New SqlParameter("@NUMAMOUNT", Replace(Replace(txtAmount.Text, "$", ""), ",", "")),
                New SqlParameter("@DATINVOICEDATE", DTPInvoiceDate.Value),
                New SqlParameter("@STRCOMMENT", txtInvoiceComments.Text),
                New SqlParameter("@UPDATEUSER", "IAIP||" & CurrentUser.AlphaName),
                New SqlParameter("@UPDATEDATETIME", Today),
                New SqlParameter("@CREATEDATETIME", Today),
                New SqlParameter("@STRPAYTYPE", cboInvoiceType.SelectedValue),
                New SqlParameter("@STRINVOICESTATUS", InvoiceStatus)
            }

            DB.RunCommand(SQL, p)

            If Not DAL.Update_FS_Admin_Status(FeeYear, AirsNumber.ToString) Then
                MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            ViewAllInvoices()

            RefreshAdminStatus()

            LoadTransactionData()

            MsgBox("New Invoice Added", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnVOIDInvoice_Click(sender As Object, e As EventArgs) Handles btnVOIDInvoice.Click
        Try
            Dim Payment As String = "0"

            If (mtbAirsNumber.Text <> AirsNumber.FormattedString) OrElse (FeeYearsComboBox.SelectedItem.ToString <> FeeYear) Then
                MessageBox.Show("The selected AIRS number or fee year don't match the displayed information. " &
                                "Please double-check and try again." &
                                vbNewLine & vbNewLine & "NO DATA SAVED.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim SQL As String = "Select " &
            "numPayment " &
            "from FS_Transactions " &
            "where invoiceID = @invoiceID " &
            "and Active <> '0' "

            Dim p As New SqlParameter("@invoiceID", txtInvoice.Text)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)
            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("numPayment")) Then
                    Payment = "0"
                Else
                    Payment = dr.Item("numPayment")
                End If
            End If

            If Payment <> "0" Then
                MsgBox("There already exists a transaction for this invoice." & vbCrLf &
                       "Any Transaction needs to be deleted or zeroed out to VOID an Invoice.",
                         MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Update FS_FeeInvoice set " &
            "Active = '0' " &
            "where invoiceID = @invoiceID "

            DB.RunCommand(SQL, p)

            ViewAllInvoices()
            LoadTransactionData()

            MsgBox("Invoice VOIDED", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnVOIDAllUnpaid_Click(sender As Object, e As EventArgs) Handles btnVOIDAllUnpaid.Click
        Try
            Dim InvoiceID As String = ""

            If (mtbAirsNumber.Text <> AirsNumber.FormattedString) OrElse (FeeYearsComboBox.SelectedItem.ToString <> FeeYear) Then
                MessageBox.Show("The selected AIRS number or fee year don't match the displayed information. " &
                                "Please double-check and try again." &
                                vbNewLine & vbNewLine & "NO DATA SAVED.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim SQL As String = "Select distinct " &
            "FS_FeeInvoice.InvoiceID " &
            "from FS_FeeInvoice " &
            "left join FS_Transactions " &
            "on FS_FeeInvoice.invoiceid = FS_Transactions.InvoiceID " &
            "where FS_FeeInvoice.Active = '1' " &
            "and FS_FeeInvoice.strAIRSNumber = @airs " &
            "and FS_FeeInvoice.numFeeYear = @year " &
            "and (numPayment is null or numPayment = '0' ) "

            Dim params As SqlParameter() = {
                New SqlParameter("@airs", AirsNumber.DbFormattedString),
                New SqlParameter("@year", FeeYear)
            }

            Dim dr As DataRow = DB.GetDataRow(SQL, params)
            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("InvoiceID")) Then
                    InvoiceID = ""
                Else
                    InvoiceID = dr.Item("InvoiceID")
                End If

                If InvoiceID <> "" Then
                    SQL = "Update FS_FeeInvoice set " &
                    "Active = '0' " &
                    "where invoiceID = @invoiceID "

                    Dim p As New SqlParameter("@invoiceID", InvoiceID)

                    DB.RunCommand(SQL, p)
                End If
            End If

            ViewAllInvoices()
            LoadTransactionData()
            MsgBox("All unpaid Invoices have been VOIDED.", MsgBoxStyle.Exclamation, Me.Text)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRemoveVOID_Click(sender As Object, e As EventArgs) Handles btnRemoveVOID.Click
        Try
            If (mtbAirsNumber.Text <> AirsNumber.FormattedString) OrElse (FeeYearsComboBox.SelectedItem.ToString <> FeeYear) Then
                MessageBox.Show("The selected AIRS number or fee year don't match the displayed information. " &
                                "Please double-check and try again." &
                                vbNewLine & vbNewLine & "NO DATA SAVED.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If txtInvoice.Text <> "" Then
                Dim SQL As String = "Update FS_FeeInvoice set " &
                "Active = '1' " &
                "where invoiceID = @invoiceID "
                Dim p As New SqlParameter("@invoiceID", txtInvoice.Text)
                DB.RunCommand(SQL, p)

                txtStatus.Text = "Active"

                ViewAllInvoices()
                LoadTransactionData()
                MsgBox("Invoice VOID Status Removed.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbEndFeeCollectoins_CheckStateChanged(sender As Object, e As EventArgs) Handles chbEndFeeCollectoins.CheckStateChanged
        Try
            'DTPDateCollectionsCeased
            If chbEndFeeCollectoins.Checked = True Then
                DTPDateCollectionsCeased.Enabled = True
            Else
                DTPDateCollectionsCeased.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEditFeeAudit_Click(sender As Object, e As EventArgs) Handles btnEditFeeAudit.Click
        Try
            Dim SQL As String
            Dim OpStatus As String = ""
            Dim ShutDown As String = ""
            Dim Classification As String = ""
            Dim VOCTons As String = ""
            Dim PMTons As String = ""
            Dim SO2Tons As String = ""
            Dim NOxTons As String = ""
            Dim FeeRate As String = ""
            Dim Part70Fee As String = ""
            Dim CalculatedFee As String = ""
            Dim SMFee As String = ""
            Dim NSPSFee As String = ""
            Dim AdminFee As String = ""
            Dim TotalFee As String = ""
            Dim SM As String = ""
            Dim Part70 As String = ""
            Dim NSPS As String = ""
            Dim PaymentType As String = ""
            Dim OfficialName As String = ""
            Dim OfficialTitle As String = ""
            Dim NSPSExempt As String = ""
            Dim NSPSExemptions As String = ""
            Dim StaffResponsible As String = ""
            Dim AuditLevel As String = ""
            Dim AuditENFORCEMENT As String = ""
            Dim AuditComments As String = ""
            Dim AuditStart As String = ""
            Dim AuditEnd As String = ""
            Dim EndCollections As String = ""
            Dim CollectionsDate As String = ""

            If (mtbAirsNumber.Text <> AirsNumber.FormattedString) OrElse (FeeYearsComboBox.SelectedItem.ToString <> FeeYear) Then
                MessageBox.Show("The selected AIRS number or fee year don't match the displayed information. " &
                                "Please double-check and try again." &
                                vbNewLine & vbNewLine & "NO DATA SAVED.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If txtAuditID.Text = "" Then
                MsgBox("Please select an existing Audit before attempting to edit.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If rdbEditOpStatusTrue.Checked = True Or rdbEditOpStatusFalse.Checked = True Then
                If rdbEditOpStatusTrue.Checked = True Then
                    OpStatus = "1"
                Else
                    OpStatus = "0"
                End If
            End If

            If dtpEditShutDownDate.Checked = True Then
                ShutDown = dtpEditShutDownDate.Text
            Else
                ShutDown = ""
            End If
            If cboEditClassification.Text <> "" Then
                If cboEditClassification.Items.Contains(cboEditClassification.Text) Then
                    Classification = cboEditClassification.Text
                Else
                    Classification = ""
                End If
            Else
                Classification = ""
            End If
            If txtEditVOCTons.Text <> "" Then
                VOCTons = txtEditVOCTons.Text
            Else
                VOCTons = ""
            End If
            If txtEditPMTons.Text <> "" Then
                PMTons = txtEditPMTons.Text
            Else
                PMTons = ""
            End If
            If txtEditSO2Tons.Text <> "" Then
                SO2Tons = txtEditSO2Tons.Text
            Else
                SO2Tons = ""
            End If
            If txtEditNOxTons.Text <> "" Then
                NOxTons = txtEditNOxTons.Text
            Else
                NOxTons = ""
            End If
            If txtEditFeeRate.Text <> "" Then
                FeeRate = txtEditFeeRate.Text
            Else
                FeeRate = ""
            End If
            If txtEditCalculatedFee.Text <> "" Then
                CalculatedFee = txtEditCalculatedFee.Text
            Else
                CalculatedFee = ""
            End If
            If txtEditPart70Fee.Text <> "" Then
                Part70Fee = txtEditPart70Fee.Text
            Else
                Part70Fee = ""
            End If
            If txtEditSMFee.Text <> "" Then
                SMFee = txtEditSMFee.Text
            Else
                SMFee = ""
            End If
            If txtEditNSPSFee.Text <> "" Then
                NSPSFee = txtEditNSPSFee.Text
            Else
                NSPSFee = ""
            End If
            If txtEditAdminFee.Text <> "" Then
                AdminFee = txtEditAdminFee.Text
            Else
                AdminFee = ""
            End If
            If txtEditTotalFees.Text <> "" Then
                TotalFee = txtEditTotalFees.Text
            Else
                TotalFee = ""
            End If
            If rdbEditSMTrue.Checked = True Or rdbEditSMFalse.Checked = True Then
                If rdbEditSMTrue.Checked = True Then
                    SM = "1"
                Else
                    SM = "0"
                End If
            End If
            If rdbEditPart70True.Checked = True Or rdbEditPart70False.Checked = True Then
                If rdbEditPart70True.Checked = True Then
                    Part70 = "1"
                Else
                    Part70 = "0"
                End If
            End If
            If rdbEditNSPSTrue.Checked = True Or rdbEditNSPSFalse.Checked = True Then
                If rdbEditNSPSTrue.Checked = True Then
                    NSPS = "1"
                Else
                    NSPS = "0"
                End If
            End If
            If cboEditPaymentType.Text <> "" Then
                If cboEditPaymentType.Items.Contains(cboEditPaymentType.Text) Then
                    PaymentType = cboEditPaymentType.Text
                Else
                    PaymentType = ""
                End If
            Else
                PaymentType = ""
            End If
            If txtEditOfficialName.Text <> "" Then
                OfficialName = txtEditOfficialName.Text
            Else
                OfficialName = ""
            End If
            If txtEditOfficialTitle.Text <> "" Then
                OfficialTitle = txtEditOfficialTitle.Text
            Else
                OfficialTitle = ""
            End If
            If rdbEditNSPSExemptTrue.Checked = True Or rdbEditNSPSExemptFalse.Checked = True Then
                If rdbEditNSPSExemptTrue.Checked = True Then
                    NSPSExempt = "1"
                Else
                    NSPSExempt = "0"
                End If
                If NSPSExempt = "1" Then
                    For i As Integer = 0 To dgvEditExemptions.Rows.Count - 1
                        If dgvEditExemptions(0, i).Value = True Then
                            NSPSExemptions = NSPSExemptions & dgvEditExemptions(1, i).Value & ","
                        End If
                    Next
                    If NSPSExemptions.Length > 1 Then
                        NSPSExemptions = Mid(NSPSExemptions, 1, NSPSExemptions.Length - 1)
                    End If
                Else
                    NSPSExemptions = ""
                End If
            End If

            If chbMakeEdits.Checked = True Then
                SQL = "select updateuser " &
                    "from FS_FeeAmendment " &
                    "where numfeeyear = @year " &
                    "and strAIRSNumber = @airs " &
                    "and auditID = @auditID "
                Dim p2 As SqlParameter() = {
                    New SqlParameter("@year", FeeYear),
                    New SqlParameter("@airs", AirsNumber.DbFormattedString),
                    New SqlParameter("@auditID", txtAuditID.Text)
                }

                If DB.ValueExists(SQL, p2) Then
                    SQL = "Update FS_FeeAmendment set " &
                        "strSyntheticMinor = @strSyntheticMinor, " &
                        "numSMFee = @numSMFee, " &
                        "strPart70 = @strPart70, " &
                        "numPart70Fee = @numPart70Fee, " &
                        "intVOCTons = @intVOCTons, " &
                        "intPMTons = @intPMTons, " &
                        "intSO2Tons = @intSO2Tons, " &
                        "intNOxTons = @intNOxTons, " &
                        "numCalculatedFee = @numCalculatedFee, " &
                        "numFeeRate = @numFeeRate, " &
                        "strNSPS = @strNSPS, " &
                        "nuMNSPSFee = @nuMNSPSFee, " &
                        "strNSPSExempt = @strNSPSExempt, " &
                        "strNSPSExemptReason = @strNSPSExemptReason, " &
                        "numAdminFee = @numAdminFee, " &
                        "numTotalFee = @numTotalFee, " &
                        "strClass = @strClass, " &
                        "strOperate = @strOperate, " &
                        "datShutDown = @datShutDown, " &
                        "strOfficialName = @strOfficialName, " &
                        "strOfficialTitle = @strOfficialTitle, " &
                        "strPaymentPlan = @strPaymentPlan, " &
                        "UpdateUser = @UpdateUser, " &
                        "updateDateTime = getdate() " &
                        "where AuditID = @AuditID "
                    Dim p1 As SqlParameter() = {
                        New SqlParameter("@strSyntheticMinor", SM),
                        New SqlParameter("@numSMFee", If(SMFee = "", DBNull.Value, SMFee)),
                        New SqlParameter("@strPart70", Part70),
                        New SqlParameter("@numPart70Fee", If(Part70Fee = "", DBNull.Value, Part70Fee)),
                        New SqlParameter("@intVOCTons", If(VOCTons = "", DBNull.Value, VOCTons)),
                        New SqlParameter("@intPMTons", If(PMTons = "", DBNull.Value, PMTons)),
                        New SqlParameter("@intSO2Tons", If(SO2Tons = "", DBNull.Value, SO2Tons)),
                        New SqlParameter("@intNOxTons", If(NOxTons = "", DBNull.Value, NOxTons)),
                        New SqlParameter("@numCalculatedFee", If(CalculatedFee = "", DBNull.Value, CalculatedFee)),
                        New SqlParameter("@numFeeRate", If(FeeRate = "", DBNull.Value, FeeRate)),
                        New SqlParameter("@strNSPS", NSPS),
                        New SqlParameter("@nuMNSPSFee", If(NSPSFee = "", DBNull.Value, NSPSFee)),
                        New SqlParameter("@strNSPSExempt", NSPSExempt),
                        New SqlParameter("@strNSPSExemptReason", NSPSExemptions),
                        New SqlParameter("@numAdminFee", If(AdminFee = "", DBNull.Value, AdminFee)),
                        New SqlParameter("@numTotalFee", If(TotalFee = "", DBNull.Value, TotalFee)),
                        New SqlParameter("@strClass", Classification),
                        New SqlParameter("@strOperate", OpStatus),
                        New SqlParameter("@datShutDown", ShutDown),
                        New SqlParameter("@strOfficialName", OfficialName),
                        New SqlParameter("@strOfficialTitle", OfficialTitle),
                        New SqlParameter("@strPaymentPlan", PaymentType),
                        New SqlParameter("@UpdateUser", CurrentUser.UserID),
                        New SqlParameter("@AuditID", txtAuditID.Text)
                    }
                    DB.RunCommand(SQL, p1)
                Else
                    SQL = "INSERT INTO FS_FEEAMENDMENT " &
                        "( AUDITID, STRAIRSNUMBER, NUMFEEYEAR, STRSYNTHETICMINOR, " &
                        "NUMSMFEE, STRPART70, NUMPART70FEE, INTVOCTONS, " &
                        "INTPMTONS, INTSO2TONS, INTNOXTONS, NUMCALCULATEDFEE, " &
                        "NUMFEERATE, STRNSPS, NUMNSPSFEE, STRNSPSEXEMPT, " &
                        "STRNSPSEXEMPTREASON, NUMADMINFEE, NUMTOTALFEE, STRCLASS, " &
                        "STROPERATE, DATSHUTDOWN, STROFFICIALNAME, STROFFICIALTITLE, " &
                        "STRPAYMENTPLAN, ACTIVE, UPDATEUSER, UPDATEDATETIME, CREATEDATETIME ) " &
                        "VALUES " &
                        "( @AUDITID, @STRAIRSNUMBER, @NUMFEEYEAR, @STRSYNTHETICMINOR, " &
                        "@NUMSMFEE, @STRPART70, @NUMPART70FEE, @INTVOCTONS, " &
                        "@INTPMTONS, @INTSO2TONS, @INTNOXTONS, @NUMCALCULATEDFEE, " &
                        "@NUMFEERATE, @STRNSPS, @NUMNSPSFEE, @STRNSPSEXEMPT, " &
                        "@STRNSPSEXEMPTREASON, @NUMADMINFEE, @NUMTOTALFEE, @STRCLASS, " &
                        "@STROPERATE, @DATSHUTDOWN, @STROFFICIALNAME, @STROFFICIALTITLE, " &
                        "@STRPAYMENTPLAN, @ACTIVE, @UPDATEUSER, getdate(), getdate() )"
                    Dim p4 As SqlParameter() = {
                        New SqlParameter("@AUDITID", txtAuditID.Text),
                        New SqlParameter("@STRAIRSNUMBER", AirsNumber.DbFormattedString),
                        New SqlParameter("@NUMFEEYEAR", If(FeeYear = "", DBNull.Value, FeeYear)),
                        New SqlParameter("@STRSYNTHETICMINOR", SM),
                        New SqlParameter("@NUMSMFEE", If(SMFee = "", DBNull.Value, SMFee)),
                        New SqlParameter("@STRPART70", Part70),
                        New SqlParameter("@NUMPART70FEE", If(Part70Fee = "", DBNull.Value, Part70Fee)),
                        New SqlParameter("@INTVOCTONS", If(VOCTons = "", DBNull.Value, VOCTons)),
                        New SqlParameter("@INTPMTONS", If(PMTons = "", DBNull.Value, PMTons)),
                        New SqlParameter("@INTSO2TONS", If(SO2Tons = "", DBNull.Value, SO2Tons)),
                        New SqlParameter("@INTNOXTONS", If(NOxTons = "", DBNull.Value, NOxTons)),
                        New SqlParameter("@NUMCALCULATEDFEE", If(CalculatedFee = "", DBNull.Value, CalculatedFee)),
                        New SqlParameter("@NUMFEERATE", If(FeeRate = "", DBNull.Value, FeeRate)),
                        New SqlParameter("@STRNSPS", NSPS),
                        New SqlParameter("@NUMNSPSFEE", If(NSPSFee = "", DBNull.Value, NSPSFee)),
                        New SqlParameter("@STRNSPSEXEMPT", NSPSExempt),
                        New SqlParameter("@STRNSPSEXEMPTREASON", NSPSExemptions),
                        New SqlParameter("@NUMADMINFEE", If(AdminFee = "", DBNull.Value, AdminFee)),
                        New SqlParameter("@NUMTOTALFEE", If(TotalFee = "", DBNull.Value, TotalFee)),
                        New SqlParameter("@STRCLASS", Classification),
                        New SqlParameter("@STROPERATE", OpStatus),
                        New SqlParameter("@DATSHUTDOWN", ShutDown),
                        New SqlParameter("@STROFFICIALNAME", OfficialName),
                        New SqlParameter("@STROFFICIALTITLE", OfficialTitle),
                        New SqlParameter("@STRPAYMENTPLAN", PaymentType),
                        New SqlParameter("@ACTIVE", "1"),
                        New SqlParameter("@UPDATEUSER", CurrentUser.UserID)
                    }
                    DB.RunCommand(SQL, p4)
                End If

                Dim p5 As SqlParameter() = {
                    New SqlParameter("@AIRSNumber", AirsNumber.DbFormattedString),
                    New SqlParameter("@FeeYear", FeeYear)
                }
                DB.SPRunCommand("PD_FeeAmendment", p5)
            End If

            If cboStaffResponsible.SelectedValue <> "" Then
                StaffResponsible = cboStaffResponsible.SelectedValue
            Else
                StaffResponsible = CurrentUser.UserID
            End If
            If StaffResponsible = "" Then
                StaffResponsible = CurrentUser.UserID
            End If
            Select Case cboAuditType.Text
                Case "Facility Self Amendment"
                    AuditLevel = "0"
                Case "Level 1 Audit"
                    AuditLevel = "1"
                Case "Level 2 Audit"
                    AuditLevel = "2"
                Case "Level 3 Audit"
                    AuditLevel = "3"
                Case "Other"
                    AuditLevel = "4"
                Case Else
                    AuditLevel = "4"
            End Select
            If txtAuditEnforcementNumber.Text <> "" Then
                AuditENFORCEMENT = txtAuditEnforcementNumber.Text
            End If
            If txtAuditComment.Text <> "" Then
                AuditComments = txtAuditComment.Text
            End If
            AuditStart = Format(DTPAuditStart.Value, "dd-MMM-yyyy")
            If DTPAuditEnd.Checked = True Then
                AuditEnd = Format(DTPAuditEnd.Value, "dd-MMM-yyyy")
            Else
                AuditEnd = ""
            End If
            If chbEndFeeCollectoins.Checked = True Then
                EndCollections = "True"
                CollectionsDate = Format(DTPDateCollectionsCeased.Value, "dd-MMM-yyyy")
            Else
                EndCollections = "False"
            End If

            SQL = "Update FS_FeeAudit set " &
                "numStaffResponsible = @numStaffResponsible, " &
                "strAuditLevel = @strAuditLevel, " &
                "numENFORCEMENT = @numENFORCEMENT, " &
                "strComments = @strComments, " &
                "datAuditStart = @datAuditStart, " &
                "datAuditEnd = @datAuditEnd, " &
                "strEndCollections = @strEndCollections, " &
                "datCollectionsEnded = @datCollectionsEnded, " &
                "updateuser = @updateuser, " &
                "updateDateTime = getdate() " &
                "where AuditID = @AuditID "
            Dim p3 As SqlParameter() = {
                New SqlParameter("@numStaffResponsible", StaffResponsible),
                New SqlParameter("@strAuditLevel", AuditLevel),
                New SqlParameter("@numENFORCEMENT", If(AuditENFORCEMENT = "", DBNull.Value, AuditENFORCEMENT)),
                New SqlParameter("@strComments", AuditComments),
                New SqlParameter("@datAuditStart", AuditStart),
                New SqlParameter("@datAuditEnd", AuditEnd),
                New SqlParameter("@strEndCollections", EndCollections),
                New SqlParameter("@datCollectionsEnded", CollectionsDate),
                New SqlParameter("@updateuser", CurrentUser.UserID),
                New SqlParameter("@AuditID", txtAuditID.Text)
            }
            DB.RunCommand(SQL, p3)

            If EndCollections = "True" Then
                SQL = "update FS_Admin set " &
                    "numCurrentStatus = '12' " &
                    "where numFeeYear = @year " &
                    "and strAIRSNumber = @airs "
                Dim p2 As SqlParameter() = {
                    New SqlParameter("@year", FeeYear),
                    New SqlParameter("@airs", AirsNumber.DbFormattedString)
                }
                DB.RunCommand(SQL, p2)
            End If

            LoadAuditedData()

            MsgBox("Audit Data Saved", MsgBoxStyle.Information, Me.Text)
        Catch ex As Exception
            ErrorReport(ex, SQL, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSelectAuditToEdit_Click(sender As Object, e As EventArgs) Handles btnSelectAuditToEdit.Click
        Try
            Dim AuditLevel As String = ""
            Dim NSPSExempt As String = ""
            Dim NSPSExemptions As String = ""

            If dgvAuditHistory.CurrentRow Is Nothing Then
                Exit Sub
            End If
            ClearAuditData()

            txtAuditID.Text = dgvAuditHistory(0, dgvAuditHistory.CurrentRow.Index).Value

            Dim SQL As String = "Select * " &
            "From FS_FeeAudit " &
            "where AuditID = @AuditID "
            Dim p As New SqlParameter("@AuditID", txtAuditID.Text)
            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("numStaffResponsible")) Then
                    cboStaffResponsible.Text = ""
                Else
                    cboStaffResponsible.SelectedValue = dr.Item("numStaffResponsible")
                End If
                If IsDBNull(dr.Item("strAuditLevel")) Then
                    AuditLevel = ""
                Else
                    AuditLevel = dr.Item("strAuditLevel")
                End If
                If IsDBNull(dr.Item("numENFORCEMENT")) Then
                    txtAuditEnforcementNumber.Clear()
                Else
                    txtAuditEnforcementNumber.Text = dr.Item("numeNFORCEMENT")
                End If
                If IsDBNull(dr.Item("strComments")) Then
                    txtAuditComment.Clear()
                Else
                    txtAuditComment.Text = dr.Item("strComments")
                End If
                If IsDBNull(dr.Item("datAuditStart")) Then
                    DTPAuditStart.Text = OracleDate
                Else
                    DTPAuditStart.Text = dr.Item("datAuditStart")
                End If
                If IsDBNull(dr.Item("datAuditEnd")) Then
                    DTPAuditEnd.Text = DTPAuditStart.Text
                    DTPAuditEnd.Checked = False
                Else
                    DTPAuditEnd.Text = dr.Item("datAuditEnd")
                    DTPAuditEnd.Checked = True
                End If
                If IsDBNull(dr.Item("strEndCollections")) Then
                    chbEndFeeCollectoins.Checked = False
                Else
                    chbEndFeeCollectoins.Checked = dr.Item("strEndCollections")
                End If
                If IsDBNull(dr.Item("datCollectionsEnded")) Then
                    DTPDateCollectionsCeased.Text = OracleDate
                    DTPDateCollectionsCeased.Checked = False
                Else
                    DTPDateCollectionsCeased.Text = dr.Item("datCollectionsEnded")
                    DTPDateCollectionsCeased.Checked = True
                End If
            End If

            Select Case AuditLevel
                Case "0"
                    cboAuditType.Text = "Facility Self Amendment"
                Case "1"
                    cboAuditType.Text = "Level 1 Audit"
                Case "2"
                    cboAuditType.Text = "Level 2 Audit"
                Case "3"
                    cboAuditType.Text = "Level 3 Audit"
                Case "4"
                    cboAuditType.Text = "Other"
                Case Else
                    cboAuditType.Text = "Other"
            End Select

            SQL = "select * " &
            "from FS_FeeAmendment " &
            "where auditID = @AuditID "

            dr = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strSyntheticMinor")) Then
                    rdbEditSMTrue.Checked = False
                    rdbEditSMFalse.Checked = False
                Else
                    If dr.Item("strSyntheticMinor") = "0" Then
                        rdbEditSMFalse.Checked = True
                    Else
                        rdbEditSMTrue.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("numSMFee")) Then
                    txtEditSMFee.Clear()
                Else
                    txtEditSMFee.Text = dr.Item("numSMFee")
                End If
                If IsDBNull(dr.Item("strPart70")) Then
                    rdbEditPart70False.Checked = False
                    rdbEditPart70True.Checked = False
                Else
                    If dr.Item("strPart70") = "0" Then
                        rdbEditPart70False.Checked = True
                    Else
                        rdbEditPart70True.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("numPart70Fee")) Then
                    txtEditPart70Fee.Clear()
                Else
                    txtEditPart70Fee.Text = dr.Item("numPart70Fee")
                End If
                If IsDBNull(dr.Item("intVOCTons")) Then
                    txtEditVOCTons.Clear()
                Else
                    txtEditVOCTons.Text = dr.Item("intVOCTons")
                End If
                If IsDBNull(dr.Item("intPMTons")) Then
                    txtEditPMTons.Clear()
                Else
                    txtEditPMTons.Text = dr.Item("intPMTons")
                End If
                If IsDBNull(dr.Item("intSO2Tons")) Then
                    txtEditSO2Tons.Clear()
                Else
                    txtEditSO2Tons.Text = dr.Item("intSO2Tons")
                End If
                If IsDBNull(dr.Item("intNOxTons")) Then
                    txtEditNOxTons.Clear()
                Else
                    txtEditNOxTons.Text = dr.Item("intNOxTons")
                End If
                If IsDBNull(dr.Item("numCalculatedFee")) Then
                    txtEditCalculatedFee.Clear()
                Else
                    txtEditCalculatedFee.Text = dr.Item("numCalculatedFee")
                End If
                If IsDBNull(dr.Item("nuMFeeRate")) Then
                    txtEditFeeRate.Clear()
                Else
                    txtEditFeeRate.Text = dr.Item("numFeeRate")
                End If
                If IsDBNull(dr.Item("strNSPS")) Then
                    rdbEditNSPSFalse.Checked = False
                    rdbEditNSPSTrue.Checked = False
                Else
                    If dr.Item("strNSPS") = "0" Then
                        rdbEditNSPSFalse.Checked = True
                    Else
                        rdbEditNSPSTrue.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("numNSPSFee")) Then
                    txtEditNSPSFee.Clear()
                Else
                    txtEditNSPSFee.Text = dr.Item("numNSPSFee")
                End If
                If IsDBNull(dr.Item("strNSPSExempt")) Then
                    rdbEditNSPSExemptFalse.Checked = False
                    rdbEditNSPSExemptTrue.Checked = False
                Else
                    NSPSExempt = dr.Item("strNSPSExempt")
                End If
                If IsDBNull(dr.Item("strNSPSexemptReason")) Then
                    NSPSExemptions = ""
                Else
                    NSPSExemptions = dr.Item("strNSPSExemptReason")
                End If
                If IsDBNull(dr.Item("numAdminFee")) Then
                    txtEditAdminFee.Clear()
                Else
                    txtEditAdminFee.Text = dr.Item("numAdminFee")
                End If
                If IsDBNull(dr.Item("numTotalFee")) Then
                    txtEditTotalFees.Clear()
                Else
                    txtEditTotalFees.Text = dr.Item("numTotalFee")
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    cboEditClassification.Text = ""
                Else
                    cboEditClassification.Text = dr.Item("strClass")
                End If
                If IsDBNull(dr.Item("strOperate")) Then
                    rdbEditOpStatusTrue.Checked = False
                    rdbEditOpStatusFalse.Checked = False
                Else
                    If dr.Item("strOperate") = "1" Then
                        rdbEditOpStatusTrue.Checked = True
                    Else
                        rdbEditOpStatusFalse.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datShutDown")) Then
                    dtpEditShutDownDate.Text = OracleDate
                    dtpEditShutDownDate.Checked = False
                Else
                    dtpEditShutDownDate.Text = dr.Item("datShutDown")
                    dtpEditShutDownDate.Checked = True
                End If
                If IsDBNull(dr.Item("strOfficialName")) Then
                    txtEditOfficialName.Clear()
                Else
                    txtEditOfficialName.Text = dr.Item("strOfficialName")
                End If
                If IsDBNull(dr.Item("strOfficialTitle")) Then
                    txtEditOfficialTitle.Clear()
                Else
                    txtEditOfficialTitle.Text = dr.Item("strOfficialTitle")
                End If
                If IsDBNull(dr.Item("strPaymentPlan")) Then
                    cboEditPaymentType.Text = ""
                Else
                    cboEditPaymentType.Text = dr.Item("strPaymentPlan")
                End If

            End If

            If NSPSExempt = "0" Then
                rdbEditNSPSExemptFalse.Checked = True
            Else
                rdbEditNSPSExemptTrue.Checked = True
                If NSPSExemptions <> "" Then
                    Do While NSPSExemptions <> ""
                        If NSPSExemptions.Contains(",") Then
                            temp = Mid(NSPSExemptions, 1, InStr(NSPSExemptions, ",", CompareMethod.Text) - 1)
                            NSPSExemptions = Replace(NSPSExemptions, (temp & ","), "")
                        Else
                            temp = NSPSExemptions
                            NSPSExemptions = Replace(NSPSExemptions, temp, "")
                        End If
                        Dim x As Integer = 0
                        While x < dgvEditExemptions.Rows.Count
                            If dgvEditExemptions(1, x).Value = temp Then
                                dgvEditExemptions(0, x).Value = True
                            End If
                            x += 1
                        End While
                    Loop
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnExportAuditToExcel_Click(sender As Object, e As EventArgs) Handles btnExportAuditToExcel.Click
        dgvAuditHistory.ExportToExcel(Me)
    End Sub

    Private Sub btnClearAuditData_Click(sender As Object, e As EventArgs) Handles btnClearAuditData.Click
        ClearAuditData()
    End Sub

    Private Sub ClearAuditData()
        txtAuditID.Clear()
        cboStaffResponsible.Text = ""
        cboAuditType.Text = ""
        txtAuditComment.Clear()
        txtAuditEnforcementNumber.Clear()
        DTPAuditStart.Value = Today
        DTPAuditEnd.Value = Today
        DTPAuditEnd.Checked = False
        DTPDateCollectionsCeased.Value = Today
        chbEndFeeCollectoins.Checked = False
        rdbEditOpStatusTrue.Checked = False
        rdbEditOpStatusFalse.Checked = False
        dtpEditShutDownDate.Value = Today
        dtpEditShutDownDate.Checked = False
        cboEditClassification.Text = ""
        txtEditVOCTons.Clear()
        txtEditPMTons.Clear()
        txtEditSO2Tons.Clear()
        txtEditNOxTons.Clear()
        txtEditFeeRate.Clear()
        txtEditCalculatedFee.Clear()
        txtEditPart70Fee.Clear()
        txtEditSMFee.Clear()
        txtEditNSPSFee.Clear()
        txtEditAdminFee.Clear()
        txtEditTotalFees.Clear()
        rdbEditSMTrue.Checked = False
        rdbEditSMFalse.Checked = False
        rdbEditPart70True.Checked = False
        rdbEditPart70False.Checked = False
        rdbEditNSPSTrue.Checked = False
        rdbEditNSPSFalse.Checked = False
        cboEditPaymentType.Text = ""
        txtEditOfficialName.Clear()
        txtEditOfficialTitle.Clear()
        rdbEditNSPSExemptTrue.Checked = False
        rdbEditNSPSExemptFalse.Checked = False
    End Sub

    Private Sub btnClearInvoiceData_Click(sender As Object, e As EventArgs) Handles btnClearInvoiceData.Click
        ClearInvoices()
    End Sub

    Private Sub ClearInvoices()
        txtInvoice.Clear()
        txtAmount.Clear()
        DTPInvoiceDate.Value = Today
        txtStatus.Clear()
        cboInvoiceType.Text = ""
        txtPayStatus.Clear()
        txtInvoiceComments.Clear()
    End Sub

    Private Sub btnFindTransactions4_Click(sender As Object, e As EventArgs) Handles btnFindTransactions.Click
        Try

            If rdbCurrentFeeyear.Checked = True Then
                LoadTransactionData()
            Else
                Dim SQL As String = "SELECT tr.TRANSACTIONID, inv.INVOICEID, tr.DATTRANSACTIONDATE, tr.NUMPAYMENT, tr.STRCHECKNO, tr.STRDEPOSITNO, tr.STRBATCHNO, tr.ENTRYPERSON, tr.STRCOMMENT, tr.STRCREDITCARDNO, tr.TRANSACTIONTYPECODE, " &
                    "CASE WHEN tr.UPDATEUSER IS NOT NULL THEN u.STRLASTNAME+', '+u.STRFIRSTNAME ELSE '' END AS UpdateUser, tr.UPDATEDATETIME, tr.CREATEDATETIME, tr.NUMFEEYEAR " &
                    "FROM (SELECT t.TRANSACTIONID, t.INVOICEID, t.DATTRANSACTIONDATE, t.NUMPAYMENT, t.STRCHECKNO, t.STRDEPOSITNO, t.STRBATCHNO, p.STRLASTNAME+', '+p.STRFIRSTNAME AS ENTRYPERSON, t.STRCOMMENT, t.STRCREDITCARDNO, t.TRANSACTIONTYPECODE, t.UPDATEUSER, t.UPDATEDATETIME, t.CREATEDATETIME, t.STRAIRSNUMBER, t.NUMFEEYEAR " &
                    "FROM FS_TRANSACTIONS AS t " &
                    "INNER JOIN EPDUSERPROFILES AS p ON t.STRENTRYPERSON = p.NUMUSERID " &
                    "WHERE t.STRAIRSNUMBER = @airs AND t.ACTIVE = 1) AS tr " &
                    "LEFT JOIN (SELECT i.INVOICEID, i.UPDATEUSER, i.UPDATEDATETIME, i.CREATEDATETIME, i.STRAIRSNUMBER, i.NUMFEEYEAR " &
                    "FROM FS_feeINVOICE AS i " &
                    "WHERE i.STRAIRSNUMBER = @airs AND i.ACTIVE = '1') AS inv ON tr.INVOICEID = inv.INVOICEID AND tr.STRAIRSNUMBER = inv.STRAIRSNUMBER AND tr.NUMFEEYEAR = inv.NUMFEEYEAR " &
                    "LEFT JOIN EPDUSERPROFILES AS u ON tr.UPDATEUSER = u.NUMUSERID " &
                    "UNION " &
                    "SELECT tr.TRANSACTIONID, inv.INVOICEID, tr.DATTRANSACTIONDATE, tr.NUMPAYMENT, tr.STRCHECKNO, tr.STRDEPOSITNO, tr.STRBATCHNO, tr.ENTRYPERSON, tr.STRCOMMENT, tr.STRCREDITCARDNO, tr.TRANSACTIONTYPECODE, " &
                    "CASE WHEN tr.UPDATEUSER IS NOT NULL THEN p.STRLASTNAME+', '+p.STRFIRSTNAME ELSE '' END AS UpdateUser, tr.UPDATEDATETIME, tr.CREATEDATETIME, tr.NUMFEEYEAR " &
                    "FROM (SELECT t.TRANSACTIONID, t.INVOICEID, t.DATTRANSACTIONDATE, t.NUMPAYMENT, t.STRCHECKNO, t.STRDEPOSITNO, t.STRBATCHNO, p.STRLASTNAME+', '+p.STRFIRSTNAME AS ENTRYPERSON, t.STRCOMMENT, t.STRCREDITCARDNO, t.TRANSACTIONTYPECODE, t.UPDATEUSER, t.UPDATEDATETIME, t.CREATEDATETIME, t.STRAIRSNUMBER, t.NUMFEEYEAR " &
                    "FROM FS_TRANSACTIONS AS t " &
                    "INNER JOIN EPDUSERPROFILES AS p ON t.STRENTRYPERSON = p.NUMUSERID " &
                    "WHERE t.STRAIRSNUMBER = @airs AND t.ACTIVE = 1) AS tr " &
                    "LEFT JOIN EPDUSERPROFILES AS p ON tr.UPDATEUSER = p.NUMUSERID " &
                    "RIGHT JOIN (SELECT i.INVOICEID, i.UPDATEUSER, i.UPDATEDATETIME, i.CREATEDATETIME, i.STRAIRSNUMBER, i.NUMFEEYEAR " &
                    "FROM FS_feeINVOICE AS i " &
                    "WHERE i.STRAIRSNUMBER = @airs AND i.ACTIVE = '1') AS inv ON inv.INVOICEID = tr.INVOICEID AND inv.STRAIRSNUMBER = tr.STRAIRSNUMBER AND inv.NUMFEEYEAR = tr.NUMFEEYEAR "

                Dim p As New SqlParameter("@airs", AirsNumber.DbFormattedString)

                dgvTransactions.DataSource = DB.GetDataTable(SQL, p)

                dgvTransactions.RowHeadersVisible = False
                dgvTransactions.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvTransactions.AllowUserToResizeColumns = True
                dgvTransactions.AllowUserToResizeRows = True
                dgvTransactions.AllowUserToAddRows = False
                dgvTransactions.AllowUserToDeleteRows = False
                dgvTransactions.AllowUserToOrderColumns = True
                dgvTransactions.Columns("transactionID").HeaderText = "Transaction ID"
                dgvTransactions.Columns("transactionID").DisplayIndex = 0
                dgvTransactions.Columns("INVOICEID").HeaderText = "Invoice ID"
                dgvTransactions.Columns("INVOICEID").DisplayIndex = 1
                dgvTransactions.Columns("DATTRANSACTIONDATE").HeaderText = "Transaction Date"
                dgvTransactions.Columns("DATTRANSACTIONDATE").DisplayIndex = 2
                dgvTransactions.Columns("DATTRANSACTIONDATE").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvTransactions.Columns("NUMPAYMENT").HeaderText = "Amount"
                dgvTransactions.Columns("NUMPAYMENT").DisplayIndex = 3
                dgvTransactions.Columns("NUMPAYMENT").DefaultCellStyle.Format = "c"
                dgvTransactions.Columns("STRDEPOSITNO").HeaderText = "Deposit No"
                dgvTransactions.Columns("STRDEPOSITNO").DisplayIndex = 4
                dgvTransactions.Columns("STRBATCHNO").HeaderText = "Batch No."
                dgvTransactions.Columns("STRBATCHNO").DisplayIndex = 5
                dgvTransactions.Columns("ENTRYPERSON").HeaderText = "Entry Person"
                dgvTransactions.Columns("ENTRYPERSON").DisplayIndex = 6
                dgvTransactions.Columns("STRCHECKNO").HeaderText = "Check No"
                dgvTransactions.Columns("STRCHECKNO").DisplayIndex = 7
                dgvTransactions.Columns("strCreditcardno").HeaderText = "Credit No."
                dgvTransactions.Columns("strCreditcardno").DisplayIndex = 8
                dgvTransactions.Columns("strComment").HeaderText = "Comments"
                dgvTransactions.Columns("strComment").DisplayIndex = 9

                dgvTransactions.Columns("TRANSACTIONTYPECODE").HeaderText = "Transaction Type"
                dgvTransactions.Columns("TRANSACTIONTYPECODE").DisplayIndex = 10
                dgvTransactions.Columns("TRANSACTIONTYPECODE").Width = 0
                dgvTransactions.Columns("UPDATEUSER").HeaderText = "Update User"
                dgvTransactions.Columns("UPDATEUSER").DisplayIndex = 11
                dgvTransactions.Columns("UPDATEDATETIME").HeaderText = "Update Time"
                dgvTransactions.Columns("UPDATEDATETIME").DisplayIndex = 12
                dgvTransactions.Columns("UPDATEDATETIME").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvTransactions.Columns("CREATEDATETIME").HeaderText = "Create Time"
                dgvTransactions.Columns("CREATEDATETIME").DisplayIndex = 13
                dgvTransactions.Columns("CREATEDATETIME").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvTransactions.Columns("numFeeYear").HeaderText = "Fee Year"
                dgvTransactions.Columns("numFeeYear").DisplayIndex = 14
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbMakeEdits_CheckedChanged(sender As Object, e As EventArgs) Handles chbMakeEdits.CheckedChanged
        If chbMakeEdits.Checked = True Then
            pnlInvoiceData.Enabled = True
            pnlFacilityData.Enabled = True
            pnlFacilityData2.Enabled = True
        Else
            pnlInvoiceData.Enabled = False
            pnlFacilityData.Enabled = False
            pnlFacilityData2.Enabled = False
        End If
    End Sub

    Private Sub btnCalculateDays_Click(sender As Object, e As EventArgs) Handles btnCalculateDays.Click
        Try
            Dim TotalFee As String = "0"
            Dim Days As String = "0"
            Dim AdminFee As String = "0"
            Dim AdminAmount As String = ""

            If txtTotalFee.Text <> "" Then
                TotalFee = txtTotalFee.Text
            Else
                TotalFee = "0"
            End If
            Days = DateDiff(DateInterval.Day, CDate(dtpStartDate.Text), CDate(dtpEndDate.Text))
            If txtAdminFee.Text <> "" Then
                AdminFee = txtAdminFee.Text
            Else
                AdminFee = "0"
            End If

            AdminAmount = CDbl(TotalFee) * CDbl(Days) * CDbl(AdminFee) / 100

            lblAdminFee.Text = "$" & AdminAmount & " - Admin Fee, Total Due - $ " & CDbl(TotalFee) + CDbl(AdminAmount)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCheckInvoices_Click(sender As Object, e As EventArgs) Handles btnCheckInvoices.Click
        Validate_FS_Invoices(Me.FeeYear, Me.AirsNumber)
    End Sub

    Private Sub chbChangeInvoiceNumber_CheckedChanged(sender As Object, e As EventArgs) Handles chbChangeInvoiceNumber.CheckedChanged
        Try
            If chbChangeInvoiceNumber.Checked = True Then
                txtInvoiceID.ReadOnly = False
            Else
                txtInvoiceID.ReadOnly = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadPanel_Enter(sender As Object, e As EventArgs) Handles LoadPanel.Enter
        Me.AcceptButton = ReloadButton
    End Sub

    Private Sub LoadPanel_Leave(sender As Object, e As EventArgs) Handles LoadPanel.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub ClearFormButton_Click(sender As Object, e As EventArgs) Handles ClearFormButton.Click
        ClearForm()
        ClearAdminData()
        ClearAuditData()
        mtbAirsNumber.Clear()
        FeeYearsComboBox.SelectedIndex = 0
    End Sub

#Region " CodeFile "
    ' Code that was formerly in CodeFile.vb but is only used in this form anyway

    Function Insert_FS_Admin(FeeYear As String, AIRSNumber As Apb.ApbFacilityId,
                         Enrolled As Boolean,
                         DateEnrolled As Date, InitialMailOut As Boolean,
                         MailoutSent As Boolean, DateMailOutSent As Date,
                         Submittal As Boolean, DateSubmittal As Date,
                         Comment As String) As Boolean
        Try
            If String.IsNullOrEmpty(FeeYear) Then
                Return False
            End If
            If AIRSNumber Is Nothing Then
                Return False
            End If

            Dim SQL As String = "Select " &
                "count(*)  " &
                "from FS_Admin " &
                "where numFeeYear = @FeeYear " &
                "and strAIRSNumber = @AIRSNumber "
            Dim params As SqlParameter() = {
                New SqlParameter("@FeeYear", FeeYear),
                New SqlParameter("@AIRSNumber", AIRSNumber.DbFormattedString)
            }

            If DB.GetSingleValue(Of Integer)(SQL, params) > 0 Then
                Return False
            End If

            SQL = "Insert into FS_Admin " &
                "(NUMFEEYEAR, STRAIRSNUMBER, STRENROLLED, DATINITIALENROLLMENT, DATENROLLMENT, STRINITIALMAILOUT, " &
                "STRMAILOUTSENT, DATMAILOUTSENT, INTSUBMITTAL, DATSUBMITTAL, NUMCURRENTSTATUS, DATSTATUSDATE, STRCOMMENT, " &
                "ACTIVE, UPDATEUSER, UPDATEDATETIME, CREATEDATETIME) " &
                "values " &
                "(@NUMFEEYEAR, @STRAIRSNUMBER, @STRENROLLED, '', @DATENROLLMENT, @STRINITIALMAILOUT, " &
                "@STRMAILOUTSENT, @DATMAILOUTSENT, @INTSUBMITTAL, @DATSUBMITTAL, 1, getdate(), @STRCOMMENT, " &
                "1, @UPDATEUSER, getdate(), getdate() ) "

            Dim params2 As SqlParameter() = {
                New SqlParameter("@NUMFEEYEAR", FeeYear),
                New SqlParameter("@STRAIRSNUMBER", AIRSNumber.DbFormattedString),
                New SqlParameter("@STRENROLLED", Convert.ToInt32(Enrolled)),
                New SqlParameter("@DATENROLLMENT", DateEnrolled),
                New SqlParameter("@STRINITIALMAILOUT", Convert.ToInt32(InitialMailOut)),
                New SqlParameter("@STRMAILOUTSENT", Convert.ToInt32(MailoutSent)),
                New SqlParameter("@DATMAILOUTSENT", DateMailOutSent),
                New SqlParameter("@INTSUBMITTAL", Convert.ToInt32(Submittal)),
                New SqlParameter("@DATSUBMITTAL", DateSubmittal),
                New SqlParameter("@STRCOMMENT", Comment),
                New SqlParameter("@UPDATEUSER", "IAIP||" & CurrentUser.AlphaName)
            }
            DB.RunCommand(SQL, params2)

            SQL = "Update FS_Admin set " &
               "datInitialEnrollment = datEnrollment " &
               "where numFeeYear = @FeeYear " &
               "and strAIRSnumber = @AIRSNumber " &
               "and datInitialEnrollment is null "
            DB.RunCommand(SQL, params)

            DB.SPRunCommand("PD_FEE_MAILOUT", params)
            DB.SPRunCommand("PD_FEE_DATA", params)

            If Not DAL.Update_FS_Admin_Status(FeeYear, AIRSNumber.ToString) Then
                MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            Return True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

    Private Function Update_FS_Admin(FeeYear As String, AIRSNumber As Apb.ApbFacilityId,
                             Enrolled As Boolean,
                             DateEnrolled As Date, InitialMailOut As Boolean,
                             MailoutSent As Boolean, DateMailOutSent As Date,
                             Submittal As Boolean, DateSubmittal As Date,
                             Comment As String, Active As Boolean) As Boolean
        Try
            If AIRSNumber Is Nothing OrElse Not IsNumeric(FeeYear) Then
                Return False
            End If

            Dim SQL As String = ""

            If Enrolled = False Then
                SQL = SQL & "strEnrolled = '0', " &
                "datEnrollment = '', "
            Else
                SQL = SQL & "strEnrolled = '1', "
                SQL = SQL & "datEnrollment = @datEnrollment, "
            End If

            If Active = False Then
                SQL = SQL & "Active = '0', "
            Else
                SQL = SQL & "Active = '1', "
            End If

            If InitialMailOut = False Then
                SQL = SQL & "strInitialMailOut = '0', "
            Else
                SQL = SQL & "strInitialMailOut = '1', "
            End If

            If MailoutSent = False Then
                SQL = SQL & "strMailOutsent = '0', " &
                "datMailOutSent = '', "
            Else
                SQL = SQL & "strMailOutSent = '1', "
                SQL = SQL & "datMailOutSent = @datMailOutSent, "
            End If

            If Submittal = False Then
                SQL = SQL & "intSubmittal = '0', " &
                "datSubmittal = '', "
            Else
                SQL = SQL & "intsubmittal = '1', "
                SQL = SQL & "datSubmittal = @datSubmittal, "
            End If

            SQL = SQL & "strComment = @strComment, "

            If SQL = "" Then
                Return False
            Else
                SQL = SQL &
                "updateUser = @updateUser, " &
                "updateDateTime = getdate() "
            End If

            SQL = "Update FS_Admin set " & SQL &
            "where numFeeYear = @year " &
            "and strAIRSNumber = @airs "

            Dim params As SqlParameter() = {
                New SqlParameter("@airs", AIRSNumber.DbFormattedString),
                New SqlParameter("@year", FeeYear),
                New SqlParameter("@datEnrollment", DateEnrolled),
                New SqlParameter("@datMailOutSent", DateMailOutSent),
                New SqlParameter("@datSubmittal", DateSubmittal),
                New SqlParameter("@strComment", Comment),
                New SqlParameter("@updateUser", "IAIP||" & CurrentUser.AlphaName)
            }

            DB.RunCommand(SQL, params)

            SQL = "Update FS_Admin set " &
            "datInitialEnrollment = datEnrollment " &
            "where numFeeYear = @FeeYear " &
            "and strAIRSnumber = @AIRSNumber " &
            "and datInitialEnrollment is null "

            Dim params2 As SqlParameter() = {
                New SqlParameter("@FeeYear", SqlDbType.Decimal) With {.Value = FeeYear},
                New SqlParameter("@AIRSNumber", SqlDbType.VarChar) With {.Value = AIRSNumber.DbFormattedString}
            }

            DB.RunCommand(SQL, params2)
            Dim spName As String = "PD_FEE_MAILOUT"
            DB.SPRunCommand(spName, params2)

            spName = "PD_FEE_DATA"
            DB.SPRunCommand(spName, params2)

            If Not DAL.Update_FS_Admin_Status(FeeYear, AIRSNumber.ShortString) Then
                MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            Return True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

    Private Sub Validate_FS_Invoices(FeeYear As String, AirsNumber As Apb.ApbFacilityId)
        Try
            Dim SQL As String = "Update FS_FeeInvoice set " &
            "strInvoiceStatus = '1', " &
            "UpdateUser = @Username,  " &
            "updateDateTime = sysdate " &
            "where numFeeYear = @FeeYear " &
            "and strAIRSNumber = @AirsNumber " &
            "and numAmount = '0' " &
            "and strInvoiceStatus = '0' " &
            "and active = '1' "

            Dim parameters As SqlParameter() = New SqlParameter() {
                New SqlParameter("@Username", CurrentUser.AlphaName),
                New SqlParameter("@FeeYear", FeeYear),
                New SqlParameter("@AirsNumber", AirsNumber.DbFormattedString)
            }

            If Not DB.RunCommand(SQL, parameters) Then
                MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                If Not DAL.Update_FS_Admin_Status(FeeYear, AirsNumber.ToString) Then
                    MessageBox.Show("There was an error updating the database", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

End Class
