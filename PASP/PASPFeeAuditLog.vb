Imports System.Data.OracleClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine


Public Class PASPFeeAuditLog
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim dtairs As New DataTable
    Dim FeeYear As String
    Dim AIRSNumber As String

    Private Sub PASPFeeStatisticsAndMailout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            LoadSelectedNSPSExemptions()
            LoadTransactionTypes()
            LoadPayTypes()
            LoadStaff()

            cboInitialOpStatus.Items.Add("")
            cboInitialOpStatus.Items.Add("O - Operational")
            cboInitialOpStatus.Items.Add("P - Planned")
            cboInitialOpStatus.Items.Add("C - Under Construction")
            cboInitialOpStatus.Items.Add("T - Temporarily Closed")
            cboInitialOpStatus.Items.Add("X - Closed/Dismantled")
            cboInitialOpStatus.Items.Add("I - Seasonal Operation")

            cboInitialClassification.Items.Add("")
            cboInitialClassification.Items.Add("A")
            cboInitialClassification.Items.Add("B")
            cboInitialClassification.Items.Add("SM")
            cboInitialClassification.Items.Add("PR")
            cboInitialClassification.Items.Add("C")

            cboEditClassification.Items.Add("")
            cboEditClassification.Items.Add("A")
            cboEditClassification.Items.Add("SM")
            cboEditClassification.Items.Add("B")
            cboEditClassification.Items.Add("PR")

            cboEditPaymentType.Items.Add("")
            cboEditPaymentType.Items.Add("Entire Annual Year")
            cboEditPaymentType.Items.Add("Four Quarterly Payments")

            DTPAuditStart.Text = OracleDate
            DTPAuditEnd.Text = OracleDate
            DTPDateCollectionsCeased.Text = OracleDate
            cboStaffResponsible.SelectedValue = UserGCode

            pnlInvoiceData.Enabled = False
            pnlFacilityData.Enabled = False
            pnlFacilityData2.Enabled = False

            cboAuditType.Items.Add("")
            cboAuditType.Items.Add("Facility Self Amendment")
            cboAuditType.Items.Add("Level 1 Audit")
            cboAuditType.Items.Add("Level 2 Audit")
            cboAuditType.Items.Add("Level 3 Audit")
            cboAuditType.Items.Add("Other")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Subs and Functions"
    Sub LoadPayTypes()
        Try
            Dim dtPayTypes As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "select " & _
            "numPayTypeID, strPayTypeDesc " & _
            "from " & DBNameSpace & ".FSLK_PayType " & _
            "order by numPayTypeID "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da.Fill(ds, "PayTypes")

            dtPayTypes.Columns.Add("numPayTypeID", GetType(System.String))
            dtPayTypes.Columns.Add("strPayTypeDesc", GetType(System.String))

            drNewRow = dtPayTypes.NewRow()
            drNewRow("numPayTypeID") = ""
            drNewRow("strPayTypeDesc") = ""
            dtPayTypes.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("PayTypes").Rows()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub



    Sub LoadTransactionTypes()
        Try
            Dim dtTransactions As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "Select " & _
            "transactionTypeCode, Description " & _
            "from " & DBNameSpace & ".FSLK_TransactionType " & _
            "order by description "
            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da.Fill(ds, "Transactions")

            dtTransactions.Columns.Add("transactionTypeCode", GetType(System.String))
            dtTransactions.Columns.Add("Description", GetType(System.String))

            drNewRow = dtTransactions.NewRow()
            drNewRow("transactionTypeCode") = ""
            drNewRow("Description") = ""
            dtTransactions.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("Transactions").Rows()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadSelectedNSPSExemptions()
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
            ' dgvEditExemptions.Columns(0).HeaderText = "NSPS ID"
            dgvEditExemptions.Columns(0).Width = (dgvEditExemptions.Width * 0.1)

            dgvEditExemptions.Columns.Add("NSPSReasonCode", "NSPS ID")
            dgvEditExemptions.Columns("NSPSReasonCode").DisplayIndex = 1
            dgvEditExemptions.Columns("NSPSReasonCode").Width = (dgvEditExemptions.Width * 0.15)
            dgvEditExemptions.Columns("NSPSReasonCode").ReadOnly = True
            dgvEditExemptions.Columns("NSPSReasonCode").Visible = False

            dgvEditExemptions.Columns.Add("Description", "NSPS Exemption Reason")
            dgvEditExemptions.Columns("Description").DisplayIndex = 2
            dgvEditExemptions.Columns("Description").Width = (dgvEditExemptions.Width * 0.9)
            dgvEditExemptions.Columns("Description").ReadOnly = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearAdminData()
        Try
            mtbFeeAdminExistingYear.Clear()
            mtbFeeAdminAIRSNumber.Clear()
            txtFeeAdminFacilityName.Clear()
            rdbEnrolledTrue.Checked = False
            rdbEnrolledFalse.Checked = False
            dtpEnrollmentDate.Text = OracleDate
            dtpEnrollmentInitial.Text = OracleDate
            rdbMailoutTrue.Checked = False
            rdbMailoutFalse.Checked = False
            rdbSubmittalTrue.Checked = False
            rdbSubmittalFalse.Checked = False
            dtpSubmittalDate.Text = OracleDate
            txtFSAdminComments.Clear()
            txtIAIPAdminStatus.Clear()
            txtGECOAdminStatus.Clear()
            dtpFeeAdminStatusDate.Text = OracleDate
            rdbActiveAdmin.Checked = False
            rdbInactiveStatus.Checked = False
            txtFSAdminUpdatingUser.Clear()
            dtpFSAdminUpdate.Text = OracleDate
            dtpFSAdminCreateDateTime.Text = OracleDate
            rdbLetterMailedTrue.Checked = False
            rdbLetterMailedFalse.Checked = False
            dtpLetterMailed.Text = OracleDate

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
            txtGECOUserEmail.Clear()
            txtInitialFacilityName.Clear()
            txtInitailFacilityAddress.Clear()
            txtInitialAddressLine2.Clear()
            txtInitialCity.Clear()
            mtbInitialZipCode.Clear()
            cboInitialOpStatus.Text = ""
            cboInitialClassification.Text = ""
            rdbInitialNSPSTrue.Checked = False
            rdbInitialNSPSFalse.Checked = False
            rdbInitialPart70True.Checked = False
            rdbInitialPart70False.Checked = False
            txtFSMailOutComments.Clear()
            txtFSMailOutUpdateUser.Clear()
            DTPFSMailOutUpdateDate.Text = OracleDate
            DTPFSMailOutDateCreated.Text = OracleDate

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadAdminData()
        Try
            'Dim FeeYear As String = ""
            'Dim AIRSNumber As String = ""
            'Dim FacilityName As String = ""
            Dim OpStatus As String = ""
            Dim Classification As String = ""

            If mtbFeeAdminExistingYear.Text <> "" And mtbFeeAdminAIRSNumber.Text <> "" Then
                If mtbFeeAdminExistingYear.Text <> "" Then
                    FeeYear = mtbFeeAdminExistingYear.Text
                End If
                If mtbFeeAdminExistingYear.Text <> "" Then
                    AIRSNumber = mtbFeeAdminAIRSNumber.Text
                End If
                ClearAdminData()

                mtbFeeAdminExistingYear.Text = FeeYear
                mtbFeeAdminAIRSNumber.Text = AIRSNumber
                txtAIRSNumber.Text = mtbFeeAdminAIRSNumber.Text
                txtYear.Text = mtbFeeAdminExistingYear.Text
                txtFeeAdminFacilityName.Clear()

                If txtFeeAdminFacilityName.Text = "" Then
                    SQL = "Select " & _
                    "strFacilityName " & _
                    "from " & DBNameSpace & ".APBFacilityInformation " & _
                    "where strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    txtFeeAdminFacilityName.Text = "ERROR"
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strFacilityname")) Then
                            txtFeeAdminFacilityName.Text = "ERROR"
                        Else
                            txtFeeAdminFacilityName.Text = dr.Item("strFacilityname")
                        End If
                    End While
                    dr.Close()
                End If

                If txtFeeAdminFacilityName.Text = "ERROR" Then
                    btnUpdateFSAdmin.Enabled = False
                    btnAddFSAdmin.Enabled = False
                    btnSaveNewFeeAudit.Enabled = False
                    btnEditFeeAudit.Enabled = False
                    btnAddNewInvoice.Enabled = False
                    btnVOIDInvoice.Enabled = False
                    btnVOIDAllUnpaid.Enabled = False
                    btnRemoveVOID.Enabled = False
                    btnTransactionNew.Enabled = False
                    btnTransactionUpdate.Enabled = False
                    btnTransactionDelete.Enabled = False
                    btnOpenFSMailout.Enabled = False
                    btnMailoutSaveUpdates.Enabled = False
                    btnGECOOpenForEditing.Enabled = False
                    btnGECOSaveUpdates.Enabled = False
                Else
                    btnUpdateFSAdmin.Enabled = True
                    btnAddFSAdmin.Enabled = True
                    btnSaveNewFeeAudit.Enabled = True
                    btnEditFeeAudit.Enabled = True
                    btnAddNewInvoice.Enabled = True
                    btnVOIDInvoice.Enabled = True
                    btnVOIDAllUnpaid.Enabled = True
                    btnRemoveVOID.Enabled = True
                    btnTransactionNew.Enabled = True
                    btnTransactionUpdate.Enabled = True
                    btnTransactionDelete.Enabled = True
                    btnOpenFSMailout.Enabled = True
                    btnMailoutSaveUpdates.Enabled = True
                    btnGECOOpenForEditing.Enabled = True
                    btnGECOSaveUpdates.Enabled = True
                End If


                SQL = "Select " & _
                "strEnrolled, datInitialEnrollment, " & _
                "datEnrollment, strInitialMailOut, " & _
                "strMailOutSent, datMailOutSent, " & _
                "intSubmittal, datSubmittal, " & _
                "numCurrentStatus, " & _
                "strIAIPDesc, strGECODesc, " & _
                "datStatusDate, " & _
                "strComment, " & _
                "" & DBNameSpace & ".FS_Admin.active, " & _
                "" & DBNameSpace & ".FS_Admin.updateUser, " & _
                "" & DBNameSpace & ".FS_Admin.updateDateTime, " & _
                "" & DBNameSpace & ".FS_Admin.CreateDateTime " & _
                "From " & DBNameSpace & ".FS_Admin, " & DBNameSpace & ".FSLK_ADMIN_Status  " & _
                "where " & DBNameSpace & ".FS_Admin.numCurrentStatus = " & DBNameSpace & ".FSLK_Admin_Status.ID (+) " & _
                "and numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' " & _
                "and strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
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
                End While
                dr.Close()

                SQL = "Select " & _
                "strFirstName, strLastName, " & _
                "strPrefix, strTitle, " & _
                "strContactCoName, strContactAddress1, " & _
                "strContactAddress2, strContactCity, " & _
                "strContactState, strContactZipCode, " & _
                "strGECOUserEmail, strOperationalStatus, " & _
                "strClass, strNSPS, " & _
                "strPart70, strFacilityName, " & _
                "strFacilityAddress1, strFacilityAddress2, " & _
                "strFacilityCity, strFacilityZipCode, " & _
                "strComment, " & _
                "Active, UpdateUser, " & _
                "UpdateDateTime, CreateDateTime " & _
                "from " & DBNameSpace & ".FS_MailOut " & _
                "where numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' " & _
                "and strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
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
                        txtGECOUserEmail.Clear()
                    Else
                        txtGECOUserEmail.Text = dr.Item("strGECOUserEmail")
                    End If
                    If IsDBNull(dr.Item("strOperationalStatus")) Then
                        cboInitialOpStatus.Text = ""
                    Else
                        OpStatus = dr.Item("strOperationalStatus")
                        Select Case OpStatus
                            Case "O"
                                cboInitialOpStatus.Text = "O - Operational"
                            Case "P"
                                cboInitialOpStatus.Text = "P - Planned"
                            Case "C"
                                cboInitialOpStatus.Text = "C - Under Construction"
                            Case "T"
                                cboInitialOpStatus.Text = "T - Temporarily Closed"
                            Case "X"
                                cboInitialOpStatus.Text = "X - Closed/Dismantled"
                            Case "I"
                                cboInitialOpStatus.Text = "I - Seasonal Operation"
                            Case Else
                                cboInitialOpStatus.Text = ""
                        End Select
                    End If
                    If IsDBNull(dr.Item("strClass")) Then
                        cboInitialClassification.Text = ""
                    Else
                        Classification = dr.Item("strClass")
                        Select Case Classification
                            Case "A"
                                cboInitialClassification.Text = "A"
                            Case "B"
                                cboInitialClassification.Text = "B"
                            Case "SM"
                                cboInitialClassification.Text = "SM"
                            Case "PR"
                                cboInitialClassification.Text = "PR"
                            Case "C"
                                cboInitialClassification.Text = "C"
                            Case Else
                                cboInitialClassification.Text = ""
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
                        txtFSMailOutComments.Clear()
                    Else
                        txtFSMailOutComments.Text = dr.Item("strComment")
                    End If
                    If IsDBNull(dr.Item("UpdateUser")) Then
                        txtFSMailOutUpdateUser.Clear()
                    Else
                        txtFSMailOutUpdateUser.Text = dr.Item("UpdateUser")
                    End If
                    If IsDBNull(dr.Item("UpdateDateTime")) Then
                        DTPFSMailOutUpdateDate.Text = OracleDate
                    Else
                        DTPFSMailOutUpdateDate.Text = dr.Item("UpdateDateTime")
                    End If
                    If IsDBNull(dr.Item("CreateDateTime")) Then
                        DTPFSMailOutDateCreated.Text = OracleDate
                    Else
                        DTPFSMailOutDateCreated.Text = dr.Item("CreateDateTime")
                    End If

                End While
                dr.Close()

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
                txtGECOUserEmail.Enabled = False
                txtInitialFacilityName.Enabled = False
                txtInitailFacilityAddress.Enabled = False
                txtInitialAddressLine2.Enabled = False
                txtInitialCity.Enabled = False
                mtbInitialZipCode.Enabled = False
                cboInitialOpStatus.Enabled = False
                cboInitialClassification.Enabled = False
                rdbInitialNSPSTrue.Enabled = False
                rdbInitialNSPSFalse.Enabled = False
                rdbInitialPart70True.Enabled = False
                rdbInitialPart70False.Enabled = False
                txtFSMailOutComments.Enabled = False
                txtFSMailOutUpdateUser.Enabled = False
                DTPFSMailOutUpdateDate.Enabled = False
                DTPFSMailOutDateCreated.Enabled = False

                SQL = "Select " & _
                "strContactFirstName, strContactlastName, " & _
                "strContactPrefix, strContactTitle, " & _
                "strContactCompanyName, strContactAddress, " & _
                "strContactCity, strContactState, " & _
                "strContactZipCode, strContactPhoneNumber, " & _
                "strContactFaxNumber, strContactEmail, " & _
                "strComment " & _
                "from " & DBNameSpace & ".FS_ContactInfo " & _
                "where numfeeyear = '" & mtbFeeAdminExistingYear.Text & "' " & _
                "and strAIRSnumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, Conn)

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dr = cmd.ExecuteReader
                While dr.Read
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
                End While
                dr.Close()

                LoadFeeInvoiceData()
                LoadTransactionData()

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadFeeInvoiceData()
        Try
            SQL = "select " & _
            "" & DBNameSpace & ".FS_FEEDATA.NUMFEEYEAR, " & _
            "" & DBNameSpace & ".FS_FEEDATA.STRAIRSNUMBER, " & _
            "STRSYNTHETICMINOR, NUMSMFEE, " & _
            "STRPART70, NUMPART70FEE, " & _
            "INTVOCTONS, INTPMTONS, " & _
            "INTSO2TONS, INTNOXTONS, " & _
            "NUMCALCULATEDFEE, NUMFEERATE, " & _
            "STRNSPS, NUMNSPSFEE, " & _
            "STRNSPSEXEMPT, STRNSPSEXEMPTREASON, " & _
            "NUMADMINFEE, NUMTOTALFEE, " & _
            "STRCLASS, STROPERATE, " & _
            "DATSHUTDOWN, STROFFICIALNAME, " & _
            "strOfficialTitle, " & _
            "strPaymentPlan, STRCONFIRMATIONNUMBER, " & _
            "" & DBNameSpace & ".FS_FEEDATA.strComment, " & _
            "updateUser, " & _
            "UpdateDateTime, CreateDateTime " & _
            "from " & DBNameSpace & ".FS_FEEDATA " & _
            "where STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
            "and NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "' "

            txtGECOExceptions.Clear()
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
            End While
            dr.Close()

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

                SQL = "Select Description " & _
                "from " & DBNameSpace & ".FSLK_NSPSReason " & _
                "where " & _
                SQLLine

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Description")) Then
                        txtGECOExceptions.Text = txtGECOExceptions.Text
                    Else
                        txtGECOExceptions.Text = txtGECOExceptions.Text & "- " & dr.Item("Description") & vbCrLf & vbCrLf
                    End If
                End While
                dr.Close()
            End If

            SQLLine = ""

            SQL = "Select " & _
            "strNonAttainment " & _
            "from " & DBNameSpace & ".LookUpCountyInformation " & _
            "where strCountyCode = '" & Mid(mtbFeeAdminAIRSNumber.Text, 1, 3) & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strNonAttainment")) Then
                    chbInvoicedataNonAttainment.Checked = False
                Else
                    temp = dr.Item("strNonAttainment")
                    If Mid(dr.Item("strNonAttainment").ToString, 2, 1) = "1" Then
                        chbInvoicedataNonAttainment.Checked = True
                    Else
                        chbInvoicedataNonAttainment.Checked = False
                    End If
                End If
            End While
            dr.Close()

            ' Dim SQLLine As String = ""
            Dim NSPStemp As String = ""

            ds = New DataSet
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

                SQL = "Select Description " & _
                "from " & DBNameSpace & ".FSLK_NSPSReason " & _
                "where " & _
                SQLLine

                da = New OracleDataAdapter(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                da.Fill(ds, "InvoiceData")

                dgvInvoiceDataNSPSExemptions.DataSource = ds
                dgvInvoiceDataNSPSExemptions.DataMember = "InvoiceData"

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
                dgvInvoiceDataNSPSExemptions.DataMember = ""
                dgvInvoiceDataNSPSExemptions.DataSource = ds
            End If

            SQL = "Select " & _
            "InvoiceID, numAmount, " & _
            "datInvoiceDate, strComment, " & _
            "strPayTypeDesc, " & _
            "case " & _
            "when strInvoiceStatus = '1' then 'Paid' " & _
            "else 'Unpaid' " & _
            "end InvoiceStatus " & _
            "from " & DBNameSpace & ".FS_FeeInvoice, " & DBNameSpace & ".FSLK_PayType " & _
            "where " & DBNameSpace & ".FS_FeeInvoice.strPaytype = " & DBNameSpace & ".FSLK_PayType.numpaytypeid " & _
            "and strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
            "and numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' " & _
            "and " & DBNameSpace & ".FS_FeeInvoice.Active = '1' "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            da.Fill(ds, "FeeInvoice")
            dgvInvoiceData.DataSource = ds
            dgvInvoiceData.DataMember = "FeeInvoice"

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
            For i = 0 To dgvInvoiceData.RowCount - 1
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadTransactionData()
        Try
            SQL = "select " & _
            "TRANSACTIONID,  INVOICES.INVOICEID, DATTRANSACTIONDATE, " & _
            "NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, STRBATCHNO, " & _
            "ENTRYPERSON, " & _
            "STRCOMMENT, STRCREDITCARDNO, TRANSACTIONTYPECODE, " & _
            "case " & _
            "when TRANSACTIONS.UPDATEUSER is not null then (STRLASTNAME||', '||STRFIRSTNAME) " & _
            "else '' " & _
            "end  UpdateUser, " & _
            "TRANSACTIONS.UPDATEDATETIME, " & _
            "TRANSACTIONS.CREATEDATETIME, " & _
            "strPayTypedesc " & _
            " from " & _
            "(select " & _
            "TRANSACTIONID,  INVOICEID, DATTRANSACTIONDATE, " & _
            "NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, STRBATCHNO, " & _
            "(STRLASTNAME||', '||STRFIRSTNAME) as ENTRYPERSON, " & _
            "STRCOMMENT, strCreditcardno, " & _
            "transactiontypecode, " & _
            "UPDATEUSER, UPDATEDATETIME, " & _
            "createDateTime, strairsnumber, numfeeyear, ''  " & _
            "from " & DBNameSpace & ".FS_TRANSACTIONS, " & DBNameSpace & ".EPDUSERPROFILES " & _
            "where " & DBNameSpace & ".FS_TRANSACTIONS.STRENTRYPERSON = " & DBNameSpace & ".EPDUSERPROFILES.NUMUSERID (+) " & _
            "and " & DBNameSpace & ".FS_TRANSACTIONS.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
            "and " & DBNameSpace & ".FS_TRANSACTIONS.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "' " & _
            "and active = 1) TRANSACTIONS,  " & _
            "(select " & _
            "0, INVOICEID, " & _
            "sysdate, 1, '', '', " & _
            "'', '', '', '', 2, " & _
            "" & DBNameSpace & ".FS_feeINVOICE.UPDATEUSER, " & DBNameSpace & ".FS_feeINVOICE.UPDATEDATETIME, " & _
            "" & DBNameSpace & ".FS_feeINVOICE.CREATEDATETIME, STRAIRSNUMBER, NUMFEEYEAR, strpaytypedesc " & _
            "from " & DBNameSpace & ".FS_feeINVOICE, " & DBNameSpace & ".FSLK_PayType " & _
            "where " & DBNameSpace & ".FS_FeeInvoice.strPayType = " & DBNameSpace & ".FSLK_PayType.numPayTypeID " & _
            "and STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
            "and NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "' " & _
            "and " & DBNameSpace & ".FS_feeINVOICE.Active = '1' ) INVOICES, " & _
            "" & DBNameSpace & ".EPDUSERPROFILES " & _
            "where TRANSACTIONS.STRAIRSNUMBER  =  INVOICES.STRAIRSNUMBER (+) " & _
            "and TRANSACTIONS.NUMFEEYEAR  =  INVOICES.NUMFEEYEAR  (+) " & _
            "and TRANSACTIONS.INVOICEID  =  INVOICES.INVOICEID (+) " & _
            "and TRANSACTIONS.UPDATEUSER  = " & DBNameSpace & ".epduserProfiles.numUserID   (+) " & _
            " union " & _
            "select " & _
            "TRANSACTIONID,  INVOICES.INVOICEID, DATTRANSACTIONDATE, " & _
            "NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, STRBATCHNO, " & _
            "ENTRYPERSON, " & _
            "STRCOMMENT, STRCREDITCARDNO, TRANSACTIONTYPECODE, " & _
            "case " & _
            "when TRANSACTIONS.UPDATEUSER is not null then (STRLASTNAME||', '||STRFIRSTNAME) " & _
            "else '' " & _
            "end  UpdateUser, " & _
            "TRANSACTIONS.UPDATEDATETIME, " & _
            "TRANSACTIONS.CREATEDATETIME, strPayTypeDesc " & _
            " from " & _
            "(select " & _
            "TRANSACTIONID,  INVOICEID, DATTRANSACTIONDATE, " & _
            "NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, STRBATCHNO, " & _
            "(STRLASTNAME||', '||STRFIRSTNAME) as ENTRYPERSON, " & _
            "STRCOMMENT, strCreditcardno, " & _
            "transactiontypecode, " & _
            "UPDATEUSER, UPDATEDATETIME, " & _
            "createDateTime, strairsnumber, numfeeyear, ''  " & _
            "from " & DBNameSpace & ".FS_TRANSACTIONS, " & DBNameSpace & ".EPDUSERPROFILES " & _
            "where " & DBNameSpace & ".FS_TRANSACTIONS.STRENTRYPERSON = " & DBNameSpace & ".EPDUSERPROFILES.NUMUSERID (+) " & _
            "and " & DBNameSpace & ".FS_TRANSACTIONS.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
            "and " & DBNameSpace & ".FS_TRANSACTIONS.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "' " & _
            "and active = 1) TRANSACTIONS,  " & _
            "(select " & _
            "0, INVOICEID, " & _
            "sysdate, 1, '', '', " & _
            "'', '', '', '', 2, " & _
            "" & DBNameSpace & ".FS_feeINVOICE.UPDATEUSER, " & DBNameSpace & ".FS_feeINVOICE.UPDATEDATETIME, " & _
            "" & DBNameSpace & ".FS_feeINVOICE.CREATEDATETIME, STRAIRSNUMBER, NUMFEEYEAR, strPayTypeDesc " & _
            "from " & DBNameSpace & ".FS_feeINVOICE, " & DBNameSpace & ".fsLK_Paytype " & _
            "where " & DBNameSpace & ".FS_feeINVOICE.strPayType = " & DBNameSpace & ".fsLK_Paytype.numPayTypeID " & _
            "and STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
            "and NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "' " & _
            "and " & DBNameSpace & ".FS_feeINVOICE.Active = '1') INVOICES, " & _
            "" & DBNameSpace & ".EPDUSERPROFILES " & _
            "where  INVOICES.STRAIRSNUMBER  = TRANSACTIONS.STRAIRSNUMBER (+) " & _
            "and INVOICES.NUMFEEYEAR  =  TRANSACTIONS.NUMFEEYEAR  (+) " & _
            "and INVOICES.INVOICEID  =  TRANSACTIONS.INVOICEID (+) " & _
            "and TRANSACTIONS.UPDATEUSER  = " & DBNameSpace & ".epduserProfiles.numUserID (+) "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da.Fill(ds, "Transactions")
            dgvTransactions.DataSource = ds
            dgvTransactions.DataMember = "Transactions"

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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadAuditedData()
        Try
            If mtbFeeAdminAIRSNumber.Text <> "" And mtbFeeAdminExistingYear.Text <> "" Then
                'txtAuditID.Clear()
                'cboStaffResponsible.Text = ""
                'rdbAudit0.Checked = True
                'txtAuditComment.Clear()
                'txtAuditEnforcementNumber.Clear()
                'DTPAuditStart.Text = OracleDate
                'DTPAuditEnd.Text = OracleDate
                'DTPAuditEnd.Checked = False
                'chbEndFeeCollectoins.Checked = False
                'DTPDateCollectionsCeased.Text = OracleDate

                SQL = "select * " & _
                "from ( " & _
                "SELECT " & _
                "  case  " & _
                "when (select  " & _
                "STRSYNTHETICMINOR  " & _
                "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                "where AuditID =  " & _
                "(select max(AuditID) MAXID  " & _
                "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                "and STRSYNTHETICMINOR is not null )) is not null then (select  " & _
                "STRSYNTHETICMINOR  " & _
                "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                "where AuditID =  " & _
                "(select max(AuditID) MAXID  " & _
                "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                "and STRSYNTHETICMINOR is not null ) ) " & _
                "else null  " & _
                "end STRSYNTHETICMINOR  " & _
                "from AIRBRANCH.FS_FEEDATA    " & _
                "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                "(SELECT " & _
                "  case  " & _
                "when (select  " & _
                "NUMsmfEE  " & _
                "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                "where AuditID =  " & _
                "(select max(AuditID) MAXID  " & _
                "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                "and NUMsmfEE is not null )) is not null then (select  " & _
                "NUMsmfEE  " & _
                "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                "where AuditID =  " & _
                "(select max(AuditID) MAXID  " & _
                "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                "and NUMSMFEE is not null ) ) " & _
                "else null  " & _
                "end  NUMsmfEE " & _
                "from AIRBRANCH.FS_FEEDATA    " & _
                "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),   " & _
                "(SELECT " & _
                "  case  " & _
                "when (select  " & _
                "STRPART70  " & _
                "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                "where AuditID =  " & _
                "(select max(AuditID) MAXID  " & _
                "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                "and STRPART70 is not null )) is not null then (select  " & _
                "STRPART70  " & _
                "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                "where AuditID =  " & _
                "(select max(AuditID) MAXID  " & _
                "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                "and STRPART70 is not null ) ) " & _
                    "else null  " & _
                    "end  STRPART70  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "numPart70Fee  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and numPart70Fee is not null )) is not null then (select  " & _
                    "numPart70Fee  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and numPart70Fee is not null ) ) " & _
                    "else null  " & _
                    "end  numPart70Fee " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "INtVOCTONS  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and INtVOCTONS is not null )) is not null then (select  " & _
                    "INtVOCTONS  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and INtVOCTONS is not null ) ) " & _
                    "else null " & _
                    "end  INtVOCTONS  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "intpmtons  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and intpmtons is not null )) is not null then (select  " & _
                    "intpmtons  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and INTPMTONS is not null ) ) " & _
                    "else null  " & _
                    "end intpmtons  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "intSO2Tons  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and intSO2Tons is not null )) is not null then (select  " & _
                    "intSO2Tons  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and INTSO2TONS is not null ) ) " & _
                    "else null " & _
                    "end  INTSO2TONS  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "INTNOXTONS  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and INTNOXTONS is not null )) is not null then (select  " & _
                    "INTNOXTONS  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and INTNOXTONS is not null ) ) " & _
                    "else null  " & _
                    "end INTNOXTONS  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "numcalculatedFee  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and numcalculatedFee is not null )) is not null then (select  " & _
                    "numcalculatedFee  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and numcalculatedFee is not null ) ) " & _
                    "else null " & _
                    "end  numcalculatedFee  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "numFeeRate  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and numFeeRate is not null )) is not null then (select  " & _
                    "numFeeRate  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and NUMFEERATE is not null ) ) " & _
                    "else null  " & _
                    "end  numFeeRate  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ), " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "strNSPS  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and strNSPS is not null )) is not null then (select  " & _
                    "strNSPS  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and strNSPS is not null ) ) " & _
                    "else AIRBRANCH.FS_FEEDATA.strNSPS " & _
                    "end  strNSPS  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "' ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "numNSPSFee  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and numNSPSFee is not null )) is not null then (select  " & _
                    "numNSPSFee  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and NUMNSPSFEE is not null ) ) " & _
                    "else null  " & _
                    "end  numNSPSFee  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "strNSPSExempt  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and strNSPSExempt is not null )) is not null then (select  " & _
                    "strNSPSExempt  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and STRNSPSEXEMPT is not null ) ) " & _
                    "else null " & _
                    "end  strNSPSExempt  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "strNSPSExemptReason  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and strNSPSExemptReason is not null )) is not null then (select  " & _
                    "strNSPSExemptReason  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and STRNSPSEXEMPTREASON is not null ) ) " & _
                    "else null  " & _
                    "end  strNSPSExemptReason  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "NUMADMINFEE  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and NUMADMINFEE is not null )) is not null then (select " & _
                    "NUMADMINFEE  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and NUMADMINFEE is not null ) ) " & _
                    "else null " & _
                    "end  NUMADMINFEE  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "numTotalFee  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and numTotalFee is not null )) is not null then (select  " & _
                    "numTotalFee  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and NUMTOTALFEE is not null ) ) " & _
                    "else null  " & _
                    "end  numTotalFee  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "strClass  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and strClass is not null )) is not null then (select  " & _
                    "strClass  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and strClass is not null ) ) " & _
                    "else null  " & _
                    "end  strClass  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "strOperate  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and strOperate is not null )) is not null then (select  " & _
                    "strOperate  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and STROPERATE is not null ) ) " & _
                    "else null  " & _
                    "end  strOperate  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    " (SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "datShutDown  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and datShutDown is not null )) is not null then (select  " & _
                    "datShutDown  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and datShutDown is not null ) ) " & _
                    "else null  " & _
                    "end  datShutDown  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "strOfficialName  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and strOfficialName is not null )) is not null then (select  " & _
                    "strOfficialName  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and strOfficialName is not null ) ) " & _
                    "else null  " & _
                    "end  strOfficialName  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "strOfficialTitle  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and strOfficialTitle is not null )) is not null then (select  " & _
                    "strOfficialTitle  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and strOfficialTitle is not null ) ) " & _
                    "else null  " & _
                    "end  strOfficialTitle  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "STRPAYMENTPLAN  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and STRPAYMENTPLAN is not null )) is not null then (select  " & _
                    "STRPAYMENTPLAN  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and STRPAYMENTPLAN is not null ) ) " & _
                    "else null  " & _
                    "end  STRPAYMENTPLAN  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "ACTIVE  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and ACTIVE is not null )) is not null then (select  " & _
                    "ACTIVE  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and ACTIVE is not null ) ) " & _
                    "else null  " & _
                    "end  ACTIVE  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ),  " & _
                    "(SELECT " & _
                    "  case  " & _
                    "when (select  " & _
                    "updateUser  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'   " & _
                    "and updateUser is not null )) is not null then (select  " & _
                    "updateUser  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AuditID =  " & _
                    "(select max(AuditID) MAXID  " & _
                    "from AIRBRANCH.FS_FEEAMENDMENT  " & _
                    "where AIRBRANCH.FS_FEEAMENDMENT.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEAMENDMENT.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  " & _
                    "and updateUser is not null ) ) " & _
                    "else null  " & _
                    "end  updateUser  " & _
                    "from AIRBRANCH.FS_FEEDATA    " & _
                    "where AIRBRANCH.FS_FEEDATA.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "'  " & _
                    "and AIRBRANCH.FS_FEEDATA.NUMFEEYEAR = '" & mtbFeeAdminExistingYear.Text & "'  ) "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
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
                End While
                dr.Close()

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

                    SQL = "Select Description " & _
                    "from " & DBNameSpace & ".FSLK_NSPSReason " & _
                    "where " & _
                    SQLLine

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("Description")) Then
                            txtAuditedExemptions.Text = txtAuditedExemptions.Text
                        Else
                            txtAuditedExemptions.Text = txtAuditedExemptions.Text & "- " & dr.Item("Description") & vbCrLf & vbCrLf
                        End If
                    End While
                    dr.Close()
                End If

                SQLLine = ""

                SQL = "Select " & _
                "" & DBNameSpace & ".FS_FeeAudit.AuditID, " & _
                "case when strSyntheticMinor = '1' then 'True' " & _
                "when strSYntheticMinor is null then '' " & _
                "else 'False' " & _
                "end SyntheticMinor, numSMFee, " & _
                "case when strPart70 = '1' then 'True' " & _
                   "when strPart70 is null then '' " & _
                "else 'False' " & _
                "end Part70, numPart70Fee, " & _
                "intVOCTons, intPMTons, " & _
                "intSO2Tons, intNOXTons, " & _
                "numCalculatedFee, numFeeRate, " & _
                "case when strNSPS = '1' then 'True' " & _
                "when strNSPS is null then '' " & _
                "else 'False' " & _
                "end NSPS, numNSPSFee, " & _
                "case when strNSPSExempt = '1' then 'True' " & _
                    "when strNSPSExempt is null then '' " & _
                "else 'False' " & _
                "end NSPSExempt, " & _
                "numAdminFee, numTotalFee, " & _
                "strClass, strOperate, " & _
                "datShutdown, strOfficialname, " & _
                "strOfficialTitle, strPaymentPlan, " & _
                "(strLastName||', '||strFirstName) as IAIPUpdate, " & _
                "" & DBNameSpace & ".FS_FeeAudit.UpdateDateTime, " & DBNameSpace & ".FS_FeeAudit.CreateDateTime " & _
                "from " & DBNameSpace & ".FS_FeeAmendment, " & DBNameSpace & ".EPDUserProfiles, " & _
                "" & DBNameSpace & ".FS_FeeAudit " & _
                "where " & DBNameSpace & ".FS_FeeAudit.UpdateUser = " & DBNameSpace & ".EPDUserProfiles.numUserID " & _
                "and " & DBNameSpace & ".FS_FeeAudit.AuditID = " & DBNameSpace & ".FS_FeeAmendment.AuditID (+) " & _
                "and " & DBNameSpace & ".FS_FeeAudit.strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
                "and " & DBNameSpace & ".FS_FeeAudit.numFeeyear  = '" & mtbFeeAdminExistingYear.Text & "' " & _
                "and " & DBNameSpace & ".FS_FeeAudit.Active = '1' "

                ds = New DataSet
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                da = New OracleDataAdapter(SQL, Conn)
                da.Fill(ds, "AuditHistory")
                dgvAuditHistory.DataSource = ds
                dgvAuditHistory.DataMember = "AuditHistory"

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

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub RefreshAdminStatus()
        Try
            If mtbFeeAdminAIRSNumber.Text <> "" And mtbFeeAdminExistingYear.Text <> "" Then
                SQL = "select " & _
                "strIAIPDesc " & _
                "From " & DBNameSpace & ".FS_Admin, " & DBNameSpace & ".FSLK_ADMIN_Status " & _
                "where " & DBNameSpace & ".FS_Admin.numCurrentStatus = " & DBNameSpace & ".FSLK_Admin_Status.ID (+) " & _
                "and numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' " & _
                "and strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "'  "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strIAIPDesc")) Then
                        txtIAIPAdminStatus.Clear()
                    Else
                        txtIAIPAdminStatus.Text = dr.Item("strIAIPDesc")
                    End If
                End While
                dr.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadStaff()
        Try
            Dim dtStaff As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "Select " & _
            "numUserID, " & _
            "(strLastName||', '||strFirstName) as Staff " & _
            "from " & DBNameSpace & ".EPDUserProfiles " & _
            "where numBranch = '1' " & _
            "and numProgram = '2' " & _
            "and numUnit = '9' " & _
            "and numEmployeeStatus = '1' "

            SQL = "select * " & _
            "from " & _
            "(select " & _
            "(strLastName||', '||strFirstName) as Staff, " & _
            "numUserID " & _
            "from AIrbranch.EPDUserProfiles " & _
            "where numbranch = '1' " & _
            "and numprogram = '2' " & _
            "and numUnit = '9' " & _
            "and numEmployeeStatus = '1' " & _
            "union " & _
            "select distinct " & _
            "(strLastName||', '||strFirstName) as Staff, " & _
            "numUserID " & _
            "from AIrbranch.EPDUserProfiles, AIRBranch.FS_FeeAmendment  " & _
            "where AIRBranch.EPDUserProfiles.nuMUserID = AIRbranch.FS_FeeAmendment.UpdateUser ) " & _
            "order by staff "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            da.Fill(ds, "StaffResponsible")

            dtStaff.Columns.Add("numUserID", GetType(System.String))
            dtStaff.Columns.Add("Staff", GetType(System.String))

            drNewRow = dtStaff.NewRow()
            drNewRow("numUserID") = ""
            drNewRow("Staff") = ""
            dtStaff.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("StaffResponsible").Rows()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
    Private Sub btnOpenFSMailout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFSMailout.Click
        Try

            EditingToggle(True)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub EditingToggle(ByVal setState As String)
        Try
            If setState = True Then
                txtContactFirstName.Enabled = True
                txtContactLastName.Enabled = True
                txtContactPrefix.Enabled = True
                txtContactTitle.Enabled = True
                txtContactCoName.Enabled = True
                txtContactAddress.Enabled = True
                txtContactCity.Enabled = True
                txtContactState.Enabled = True
                mtbContactZipCode.Enabled = True
                txtContactAddress2.Enabled = True
                txtGECOUserEmail.Enabled = True
                txtInitialFacilityName.Enabled = True
                txtInitailFacilityAddress.Enabled = True
                txtInitialAddressLine2.Enabled = True
                txtInitialCity.Enabled = True
                mtbInitialZipCode.Enabled = True
                cboInitialOpStatus.Enabled = True
                cboInitialClassification.Enabled = True
                rdbInitialNSPSTrue.Enabled = True
                rdbInitialNSPSFalse.Enabled = True
                rdbInitialPart70True.Enabled = True
                rdbInitialPart70False.Enabled = True
                txtFSMailOutComments.Enabled = True
            Else
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
                txtGECOUserEmail.Enabled = False
                txtInitialFacilityName.Enabled = False
                txtInitailFacilityAddress.Enabled = False
                txtInitialAddressLine2.Enabled = False
                txtInitialCity.Enabled = False
                mtbInitialZipCode.Enabled = False
                cboInitialOpStatus.Enabled = False
                cboInitialClassification.Enabled = False
                rdbInitialNSPSTrue.Enabled = False
                rdbInitialNSPSFalse.Enabled = False
                rdbInitialPart70True.Enabled = False
                rdbInitialPart70False.Enabled = False
                txtFSMailOutComments.Enabled = False
                txtFSMailOutUpdateUser.Enabled = False
                DTPFSMailOutUpdateDate.Enabled = False
                DTPFSMailOutDateCreated.Enabled = False

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnReloadFSData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReloadFSData.Click
        Try
            FeeYear = mtbFeeAdminExistingYear.Text
            AIRSNumber = mtbFeeAdminAIRSNumber.Text

            ClearForm()
            mtbFeeAdminAIRSNumber.Text = AIRSNumber
            txtAIRSNumber.Text = mtbFeeAdminAIRSNumber.Text
            txtYear.Text = mtbFeeAdminExistingYear.Text
            mtbFeeAdminExistingYear.Text = FeeYear
            LoadAdminData()
            LoadAuditedData()

            ds = New DataSet

            dgvInvoices.DataMember = ""
            dgvInvoices.DataSource = ds

            ClearInvoices()
            ClearAuditData()
            crFeeStatsAndInvoices.ReportSource = Nothing
            crFeeStatsAndInvoices.Refresh()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            EditingToggle(False)
            ClearEditData()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateFSAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateFSAdmin.Click
        Try
            Dim ResultDoc As DialogResult

            If rdbInactiveStatus.Checked = True Then
                ResultDoc = MessageBox.Show("If there are any transactions associated with this fee year they will ""effectively"" be deleted." & vbCrLf & _
                         "Do you want to continue with an inactive status for this fee year data?", Me.Text, _
                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                Select Case ResultDoc
                    Case Windows.Forms.DialogResult.Yes

                    Case Else
                        MsgBox("NO DATA SAVED.", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                End Select
            End If

            If (mtbFeeAdminAIRSNumber.Text <> txtAIRSNumber.Text) _
                Or (mtbFeeAdminExistingYear.Text <> txtYear.Text) _
                Or txtAIRSNumber.Text = "" Or mtbFeeAdminExistingYear.Text = "" _
                Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." & _
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Select strAIRSNumber from airbranch.FS_Admin " & _
            "where numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' " & _
            "and strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = False Then
                MsgBox("The faciltiy is not currently in the Fee universe for the selected year." & vbCrLf & _
                       "Use the Add New Facility to Year." & vbCrLf & vbCrLf & "NO DATA SAVED", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If


            If Update_FS_Admin(mtbFeeAdminExistingYear.Text, mtbFeeAdminAIRSNumber.Text, _
                             rdbEnrolledTrue.Checked, _
                             dtpEnrollmentDate.Text, rdbMailoutTrue.Checked, _
                             rdbLetterMailedTrue.Checked, dtpLetterMailed.Text, _
                             rdbSubmittalTrue.Checked, dtpSubmittalDate.Text, _
                             "", _
                             txtFSAdminComments.Text, rdbActiveAdmin.Checked) = True Then

                If rdbInactiveStatus.Checked = True Then
                    FeeYear = mtbFeeAdminExistingYear.Text
                    AIRSNumber = mtbFeeAdminAIRSNumber.Text

                    ClearForm()
                    mtbFeeAdminAIRSNumber.Text = AIRSNumber
                    txtAIRSNumber.Text = mtbFeeAdminAIRSNumber.Text
                    txtYear.Text = mtbFeeAdminExistingYear.Text
                    mtbFeeAdminExistingYear.Text = FeeYear
                    LoadAdminData()
                    LoadAuditedData()

                    ds = New DataSet

                    dgvInvoices.DataMember = ""
                    dgvInvoices.DataSource = ds

                    ClearInvoices()
                    ClearAuditData()

                End If

                MsgBox("Save completed", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Did not Save", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddFSAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFSAdmin.Click
        Try
            If (mtbFeeAdminAIRSNumber.Text <> txtAIRSNumber.Text) _
             Or (mtbFeeAdminExistingYear.Text <> txtYear.Text) _
             Or txtAIRSNumber.Text = "" Or mtbFeeAdminExistingYear.Text = "" _
             Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." & _
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If Insert_FS_Admin(mtbFeeAdminExistingYear.Text, mtbFeeAdminAIRSNumber.Text, _
                          rdbEnrolledTrue.Checked, _
                          dtpEnrollmentDate.Text, rdbMailoutTrue.Checked, _
                          rdbLetterMailedTrue.Checked, dtpLetterMailed.Text, _
                          rdbSubmittalTrue.Checked, dtpSubmittalDate.Text, _
                          "", _
                          txtFSAdminComments.Text, rdbActiveAdmin.Checked) = True Then

                MsgBox("Save completed", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Did not Save", MsgBoxStyle.Information, Me.Text)
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRefreshContactData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshContactData.Click
        Try
            If mtbFeeAdminAIRSNumber.Text = "" Or mtbFeeAdminAIRSNumber.Text.Length <> 8 Then
                Exit Sub
            End If

            SQL = "select " & _
     "case " & _
     "when dt_contact40.strKey is null then dt_contact30.strContactFirstName " & _
     "else dt_contact40.strContactFirstName " & _
     "end strContactFirstName, " & _
     "case " & _
     "when dt_contact40.strKey is null then dt_contact30.strContactLastName " & _
     "else dt_contact40.strContactLastName " & _
     "end strContactLastName, " & _
     "case " & _
     "when dt_contact40.strKey is null then dt_contact30.strContactPrefix " & _
     "else dt_contact40.strContactPrefix " & _
     "end strContactPrefix, " & _
     "case " & _
     "when dt_contact40.strKey is null then dt_contact30.strContactSuffix " & _
     "else dt_contact40.strContactSuffix " & _
     "end strContactSuffix,  " & _
     "case " & _
     "when dt_contact40.strKey is null then dt_contact30.strContactCompanyName " & _
     "else dt_contact40.strContactCompanyName " & _
     "end strContactCompanyName,  " & _
     "case " & _
     "when dt_contact40.strKey is null then dt_contact30.strContactAddress1 " & _
     "else dt_contact40.strContactAddress1 " & _
     "end strContactAddress1,  " & _
     "case " & _
     "when dt_contact40.strKey is null then dt_contact30.strContactAddress2 " & _
     "else dt_contact40.strContactAddress2 " & _
     "end strContactAddress2, " & _
     "case " & _
     "when dt_contact40.strKey is null then dt_contact30.strContactCity " & _
     "else dt_contact40.strContactCity " & _
     "end strContactCity,  " & _
     "case " & _
     "when dt_contact40.strKey is null then dt_contact30.strContactstate " & _
     "else dt_contact40.strContactstate " & _
     "end strContactstate,  " & _
     "case " & _
     "when dt_contact40.strKey is null then dt_contact30.strContactZipCode " & _
     "else dt_contact40.strContactZipCode " & _
     "end strContactZipCode,  " & _
     "case " & _
     "when dt_contact40.strKey is null then dt_contact30.strContactEmail " & _
     "else dt_contact40.strContactEmail " & _
     "end strContactEmail " & _
     "from " & _
     "(select " & _
     "strAIRSNumber, strKey,  " & _
     "strContactFirstName, strContactLastName, " & _
     "strContactPrefix, strContactSuffix, " & _
     "strContactCompanyName, " & _
     "strContactAddress1, strContactAddress2, " & _
     "strContactCity, strContactstate, " & _
     "strContactZipCode, strContactEmail " & _
     "from " & DBNameSpace & ".APBContactInformation " & _
     "where strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
     "and strKey = '40') dt_contact40, " & _
     "(select " & _
     "strAIRSNumber, strKey,  " & _
     "strContactFirstName, strContactLastName, " & _
     "strContactPrefix, strContactSuffix, " & _
     "strContactCompanyName, " & _
     "strContactAddress1, strContactAddress2, " & _
     "strContactCity, strContactstate, " & _
     "strContactZipCode, strContactEmail " & _
     "from " & DBNameSpace & ".APBContactInformation " & _
     "where strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
     "and strKey = '30') dt_contact30 "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    txtContactFirstName.Clear()
                Else
                    txtContactFirstName.Text = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtContactLastName.Clear()
                Else
                    txtContactLastName.Text = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtContactPrefix.Clear()
                Else
                    txtContactPrefix.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactSuffix")) Then
                    txtContactTitle.Clear()
                Else
                    txtContactTitle.Text = dr.Item("strContactSuffix")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtContactCoName.Clear()
                Else
                    txtContactCoName.Text = dr.Item("strContactCompanyName")
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
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtGECOUserEmail.Clear()
                Else
                    txtGECOUserEmail.Text = dr.Item("strContactEmail")
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRefreshCurrentFacilityInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshCurrentFacilityInfo.Click
        Try
            Dim OpStatus As String = ""
            Dim Classification As String = ""


            If mtbFeeAdminAIRSNumber.Text = "" Or mtbFeeAdminAIRSNumber.Text.Length <> 8 Then
                Exit Sub
            End If

            SQL = "select " & _
            "strOperationalStatus, strClass, " & _
            "case " & _
            "when substr(strAIRProgramCodes, 8,1)= '1' then '1' " & _
            "else '0' " & _
            "end strNSPS, " & _
            "case " & _
            "when substr(strAIRProgramCodes, 13, 1) = '1' then '1' " & _
            "else '0' " & _
            "end strPart70, " & _
            "strFacilityName, strFacilityStreet1, " & _
            "strFacilityStreet2, strFacilityCity, " & _
            "strFacilityZipCode " & _
            "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".APBHeaderData  " & _
            "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
            "and " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strOperationalStatus")) Then
                    cboInitialOpStatus.Text = ""
                Else
                    OpStatus = dr.Item("strOperationalStatus")
                    Select Case OpStatus
                        Case "O"
                            cboInitialOpStatus.Text = "O - Operational"
                        Case "P"
                            cboInitialOpStatus.Text = "P - Planned"
                        Case "C"
                            cboInitialOpStatus.Text = "C - Under Construction"
                        Case "T"
                            cboInitialOpStatus.Text = "T - Temporarily Closed"
                        Case "X"
                            cboInitialOpStatus.Text = "X - Closed/Dismantled"
                        Case "I"
                            cboInitialOpStatus.Text = "I - Seasonal Operation"
                        Case Else
                            cboInitialOpStatus.Text = ""
                    End Select
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    cboInitialClassification.Text = ""
                Else
                    Classification = dr.Item("strClass")
                    Select Case Classification
                        Case "A"
                            cboInitialClassification.Text = "A"
                        Case "B"
                            cboInitialClassification.Text = "B"
                        Case "SM"
                            cboInitialClassification.Text = "SM"
                        Case "PR"
                            cboInitialClassification.Text = "PR"
                        Case "C"
                            cboInitialClassification.Text = "C"
                        Case Else
                            cboInitialClassification.Text = ""
                    End Select
                End If
                If IsDBNull(dr.Item("strNSPS")) Then
                    rdbInitialNSPSFalse.Checked = True
                Else
                    If dr.Item("strNSPS") = "1" Then
                        rdbInitialNSPSTrue.Checked = True
                    Else
                        rdbInitialNSPSFalse.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strPart70")) Then
                    rdbInitialPart70False.Checked = True
                Else
                    If dr.Item("strPart70") = "1" Then
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
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    txtInitailFacilityAddress.Clear()
                Else
                    txtInitailFacilityAddress.Text = dr.Item("strFacilityStreet1")
                End If
                If IsDBNull(dr.Item("strFacilityStreet2")) Then
                    txtInitialAddressLine2.Clear()
                Else
                    txtInitialAddressLine2.Text = dr.Item("strFacilityStreet2")
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
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnMailoutSaveUpdates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMailoutSaveUpdates.Click
        Try
            Dim MailOutCheck As String = ""
            Dim ShutDownDate As String = ""

            If (mtbFeeAdminAIRSNumber.Text <> txtAIRSNumber.Text) _
                Or (mtbFeeAdminExistingYear.Text <> txtYear.Text) _
                Or txtAIRSNumber.Text = "" Or mtbFeeAdminExistingYear.Text = "" _
                Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." & _
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Select " & _
            "count(*) as MailoutCheck " & _
            "from " & DBNameSpace & ".FS_Mailout " & _
            "where numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' " & _
            "and strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("MailoutCheck")) Then
                    MailOutCheck = "0"
                Else
                    MailOutCheck = dr.Item("MailoutCheck")
                End If
            End While
            dr.Close()

            If dtpShutDownDate.Checked = True Then
                ShutDownDate = dtpShutDownDate.Text
            Else
                ShutDownDate = ""
            End If

            If MailOutCheck = "0" Then
                If Insert_FS_Mailout(mtbFeeAdminExistingYear.Text, mtbFeeAdminAIRSNumber.Text, _
                                  txtContactFirstName.Text, txtContactLastName.Text, _
                                  txtContactPrefix.Text, txtContactTitle.Text, _
                                  txtContactCoName.Text, txtContactAddress.Text, _
                                  txtContactAddress2.Text, txtContactCity.Text, _
                                  txtContactState.Text, mtbContactZipCode.Text, _
                                  txtGECOUserEmail.Text, cboInitialOpStatus.Text, _
                                  cboInitialClassification.Text, rdbInitialNSPSTrue.Checked, _
                                  rdbInitialPart70True.Checked, ShutDownDate, _
                                  txtInitialFacilityName.Text, _
                                  txtInitailFacilityAddress.Text, txtInitialAddressLine2.Text, _
                                  txtInitialCity.Text, mtbInitialZipCode.Text, _
                                  txtFSMailOutComments.Text, rdbActiveAdmin.Checked) = True Then
                    MsgBox("Save completed", MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox("Did not Save", MsgBoxStyle.Information, Me.Text)
                End If

            Else
                If Update_FS_Mailout(mtbFeeAdminExistingYear.Text, mtbFeeAdminAIRSNumber.Text, _
                               txtContactFirstName.Text, txtContactLastName.Text, _
                               txtContactPrefix.Text, txtContactTitle.Text, _
                               txtContactCoName.Text, txtContactAddress.Text, _
                               txtContactAddress2.Text, txtContactCity.Text, _
                               txtContactState.Text, mtbContactZipCode.Text, _
                               txtGECOUserEmail.Text, cboInitialOpStatus.Text, _
                               cboInitialClassification.Text, rdbInitialNSPSTrue.Checked, _
                               rdbInitialPart70True.Checked, ShutDownDate, _
                               txtInitialFacilityName.Text, _
                               txtInitailFacilityAddress.Text, txtInitialAddressLine2.Text, _
                               txtInitialCity.Text, mtbInitialZipCode.Text, _
                               txtFSMailOutComments.Text, rdbActiveAdmin.Checked) = True Then
                    MsgBox("Save completed", MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox("Did not Save", MsgBoxStyle.Information, Me.Text)
                End If
            End If

            EditingToggle(False)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnGECOViewCurrentContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGECOViewCurrentContacts.Click
        Try
            SQL = "Select " & _
             "strContactFirstName, strContactlastName, " & _
             "strContactPrefix, strContactTitle, " & _
             "strContactCompanyName, strContactAddress, " & _
             "strContactCity, strContactState, " & _
             "strContactZipCode, strContactPhoneNumber, " & _
             "strContactFaxNumber, strContactEmail, " & _
             "strComment " & _
             "from " & DBNameSpace & ".FS_ContactInfo " & _
             "where numfeeyear = '" & mtbFeeAdminExistingYear.Text & "' " & _
             "and strAIRSnumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
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
            End While
            dr.Close()

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

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnGECOViewPastContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGECOViewPastContacts.Click
        Try
            SQL = "Select * " & _
            "from " & DBNameSpace & ".FS_ContactInfo " & _
            "where strAIRSnumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
            "order by numFeeYear desc "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            da.Fill(ds, "GECOContacts")
            dgvGECOFeeContacts.DataSource = ds
            dgvGECOFeeContacts.DataMember = "GECOContacts"

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
            'dgvGECOFeeContacts.Columns("datFeePeriodStart").DefaultCellStyle.Format = "dd-MMM-yyyy"
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvGECOFeeContacts_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvGECOFeeContacts.MouseUp
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnGECOOpenForEditing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGECOOpenForEditing.Click
        Try

            txtGECOContactSalutation.ReadOnly = False
            txtGECOContactFirstName.ReadOnly = False
            txtGECOContactLastName.ReadOnly = False
            txtGECOContactTitle.ReadOnly = False
            txtGECOContactCompanyName.ReadOnly = False
            txtGECOContactStreetAddress.ReadOnly = False
            txtGECOContactCity.ReadOnly = False
            txtGECOContactState.ReadOnly = False
            mtbGECOContactZipCode.ReadOnly = False
            mtbGECOContactPhontNumber.ReadOnly = False
            mtbGECOContactFaxNumber.ReadOnly = False
            txtGECOContactEmail.ReadOnly = False
            txtGECOContactComments.ReadOnly = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnGECOSaveUpdates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGECOSaveUpdates.Click
        Try
            If (mtbFeeAdminAIRSNumber.Text <> txtAIRSNumber.Text) _
           Or (mtbFeeAdminExistingYear.Text <> txtYear.Text) _
           Or txtAIRSNumber.Text = "" Or mtbFeeAdminExistingYear.Text = "" _
           Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." & _
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If txtGECOContactFirstName.ReadOnly = True Then
                MsgBox("Contact Data is not open for editing" & vbCrLf & "No data saved.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            SQL = "Select " & _
            "numFeeYear " & _
            "from " & DBNameSpace & ".FS_ContactInfo " & _
            "where numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' " & _
            "and strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Update " & DBNameSpace & ".FS_ContactInfo set " & _
                "strContactFirstName = '" & txtGECOContactFirstName.Text & "', " & _
                "strContactLastName = '" & txtGECOContactLastName.Text & "', " & _
                "strContactPrefix = '" & txtGECOContactSalutation.Text & "', " & _
                "strContactTitle = '" & txtGECOContactTitle.Text & "', " & _
                "strContactCompanyName = '" & txtGECOContactCompanyName.Text & "', " & _
                "strContactAddress = '" & txtGECOContactStreetAddress.Text & "', " & _
                "strContactCity = '" & txtGECOContactCity.Text & "', " & _
                "strContactState = '" & txtGECOContactState.Text & "', " & _
                "strContactZipCode = '" & mtbGECOContactZipCode.Text & "', " & _
                "strContactPhoneNumber = '" & mtbGECOContactPhontNumber.Text & "', " & _
                "strContactFaxNumber = '" & mtbGECOContactFaxNumber.Text & "', " & _
                "strContactEmail = '" & txtGECOContactEmail.Text & "', " & _
                "strComment = '" & txtGECOContactComments.Text & "', " & _
                "updateuser = '" & UserGCode & "', " & _
                "updateDateTime = '" & OracleDate & "' " & _
                "where numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' " & _
                "and strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' "

            Else
                SQL = "Insert into " & DBNameSpace & ".FS_ContactInfo " & _
                "values " & _
                "('" & mtbFeeAdminExistingYear.Text & "', '0413" & mtbFeeAdminAIRSNumber.Text & "', " & _
                "'" & txtGECOContactFirstName.Text & "', '" & txtGECOContactLastName.Text & "', " & _
                "'" & txtGECOContactSalutation.Text & "', '" & txtGECOContactTitle.Text & "', " & _
                "'" & txtGECOContactCompanyName.Text & "', '" & txtGECOContactStreetAddress.Text & "', " & _
                "'" & txtGECOContactCity.Text & "', '" & txtGECOContactState.Text & "', " & _
                "'" & mtbGECOContactZipCode.Text & "', '" & mtbGECOContactPhontNumber.Text & "', " & _
                "'" & mtbGECOContactFaxNumber.Text & "', '" & txtGECOContactEmail.Text & "', " & _
                "'" & txtGECOContactComments.Text & "', '1', " & _
                "'" & UserGCode & "', '" & OracleDate & "', " & _
                "'" & OracleDate & "' ) "
            End If

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

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
            Update_FS_Admin_Status(mtbFeeAdminExistingYear.Text, mtbFeeAdminAIRSNumber.Text)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Function InvoiceCheck(ByVal ValidInvoice As String) As Boolean
        Try
            If IsNumeric(txtInvoiceID.Text) Then



                SQL = "Select " & _
                "InvoiceID " & _
                "from AIRBranch.FS_FeeInvoice " & _
                "where invoiceID = '" & txtInvoiceID.Text & "' " & _
                "and strAIRSNumber = '0413" & txtAIRSNumber.Text & "' " & _
                "and numFeeyear = '" & txtYear.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
 




    Private Sub btnTransactionNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransactionNew.Click
        Try
            Dim InvoiceStatus As String = ""

            If (mtbFeeAdminAIRSNumber.Text <> txtAIRSNumber.Text) _
           Or (mtbFeeAdminExistingYear.Text <> txtYear.Text) _
           Or txtAIRSNumber.Text = "" Or mtbFeeAdminExistingYear.Text = "" _
           Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." & _
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If txtTransactionID.Text <> "" Then
                Dim TransactionCheck As DialogResult
                TransactionCheck = MessageBox.Show("There is already an existing transaction associated with the " & _
                        "invoice #: " & txtInvoiceID.Text & "." & vbCrLf & "OK - Add additional Transaction" & vbCrLf & _
                        "Cancel - Do nothing.", Me.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                Select Case TransactionCheck
                    Case Windows.Forms.DialogResult.OK

                    Case Else
                        MsgBox("No data was saved", MsgBoxStyle.Information, Me.Text)
                        Exit Sub
                End Select
            End If
            If cboTransactionType.SelectedValue Is Nothing Or cboTransactionType.SelectedValue = "" Then
                MsgBox("A transaction type must be selected before continuing." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If InvoiceCheck(InvoiceStatus) = False Then
                MsgBox("The Invoice Number entered is not valid." & vbCrLf & "No Data saved", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If txtInvoiceID.Text <> "" Then
                SQL = "Insert into " & DBNameSpace & ".FS_Transactions " & _
                "values " & _
                "((" & DBNameSpace & ".seq_fs_transactions.nextVal), " & _
                "'" & Replace(txtInvoiceID.Text, "'", "''") & "', " & _
                "'" & Replace(cboTransactionType.SelectedValue, "'", "''") & "', '" & dtpTransactionDate.Text & "', " & _
                "'" & Replace(Replace(Replace(txtTransactionAmount.Text, "'", "''"), ",", ""), "$", "") & "', " & _
                "'" & Replace(txtTransactionCheckNo.Text, "'", "''") & "', " & _
                "'" & Replace(txtDepositNo.Text, "'", "''") & "', '" & Replace(txtBatchNo.Text, "'", "''") & "', " & _
                "'" & UserGCode & "', '" & Replace(txtAPBComments.Text, "'", "''") & "', " & _
                "'1', '" & UserGCode & "', " & _
                "'" & OracleDate & "', '" & OracleDate & "', " & _
                "'0413" & mtbFeeAdminAIRSNumber.Text & "', " & _
                "'" & mtbFeeAdminExistingYear.Text & "', '" & Replace(txtTransactionCreditCardNo.Text, "'", "''") & "') "
            Else
                SQL = "Insert into " & DBNameSpace & ".FS_Transactions " & _
               "values " & _
               "((" & DBNameSpace & ".seq_fs_transactions.nextVal), " & _
               "'', " & _
               "'" & Replace(cboTransactionType.SelectedValue, "'", "''") & "', '" & dtpTransactionDate.Text & "', " & _
               "'" & Replace(Replace(Replace(txtTransactionAmount.Text, "'", "''"), ",", ""), "$", "") & "', " & _
               "'" & Replace(txtTransactionCheckNo.Text, "'", "''") & "', " & _
               "'" & Replace(txtDepositNo.Text, "'", "''") & "', '" & Replace(txtBatchNo.Text, "'", "''") & "', " & _
               "'" & UserGCode & "', '" & Replace(txtAPBComments.Text, "'", "''") & "', " & _
               "'1', '" & UserGCode & "', " & _
               "'" & OracleDate & "', '" & OracleDate & "', " & _
               "'0413" & mtbFeeAdminAIRSNumber.Text & "', " & _
               "'" & mtbFeeAdminExistingYear.Text & "', '" & Replace(txtTransactionCreditCardNo.Text, "'", "''") & "') "
            End If

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Select max(TransactionID) " & _
            "from " & DBNameSpace & ".FS_TRANSACTIONS "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item(0)) Then
                    txtTransactionID.Text = "Error"
                Else
                    txtTransactionID.Text = dr.Item(0)
                End If
            End While
            dr.Close()

            InvoiceStatusCheck(txtInvoiceID.Text)
            Update_FS_Admin_Status(mtbFeeAdminExistingYear.Text, mtbFeeAdminAIRSNumber.Text)
            RefreshAdminStatus()

            LoadTransactionData()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvTransactions_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvTransactions.MouseUp
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearTransactions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTransactions.Click
        Try
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

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearEditableTransactionData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearEditableTransactionData.Click
        Try
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

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnTransactionUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransactionUpdate.Click
        Try
            Dim InvoiceStatus As String = ""

            If (mtbFeeAdminAIRSNumber.Text <> txtAIRSNumber.Text) _
             Or (mtbFeeAdminExistingYear.Text <> txtYear.Text) _
             Or txtAIRSNumber.Text = "" Or mtbFeeAdminExistingYear.Text = "" _
             Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." & _
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

            If InvoiceCheck(InvoiceStatus) = False Then
                MsgBox("The Invoice Number entered is not valid." & vbCrLf & "No Data saved", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Update " & DBNameSpace & ".FS_Transactions set " & _
            "invoiceid = '" & txtInvoiceID.Text & "', " & _
            "TransactionTypecode = '" & cboTransactionType.SelectedValue & "', " & _
            "datTransactionDate = '" & dtpTransactionDate.Text & "', " & _
            "numPayment = '" & Replace(Replace(Replace(txtTransactionAmount.Text, "'", "''"), ",", ""), "$", "") & "', " & _
            "strCheckNo = '" & txtTransactionCheckNo.Text & "', " & _
            "strDepositNo = '" & txtDepositNo.Text & "', " & _
            "strBatchNo = '" & txtBatchNo.Text & "', " & _
            "strComment = '" & txtAPBComments.Text & "', " & _
            "active = '1', " & _
            "updateUser = '" & UserGCode & "', " & _
            "updateDateTime = sysdate, " & _
            "strCreditCardNo = '" & txtTransactionCreditCardNo.Text & "' " & _
            "where TransactionID = '" & txtTransactionID.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            InvoiceStatusCheck(txtInvoiceID.Text)
            Update_FS_Admin_Status(mtbFeeAdminExistingYear.Text, mtbFeeAdminAIRSNumber.Text)
            RefreshAdminStatus()

            LoadTransactionData()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnTransactionDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransactionDelete.Click
        Try
            Dim InvoiceStatus As String = ""
            If (mtbFeeAdminAIRSNumber.Text <> txtAIRSNumber.Text) _
             Or (mtbFeeAdminExistingYear.Text <> txtYear.Text) _
             Or txtAIRSNumber.Text = "" Or mtbFeeAdminExistingYear.Text = "" _
             Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." & _
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If txtTransactionID.Text = "" Then
                MsgBox("Please select a valid transaction to update." & vbCrLf & "No data modified", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If InvoiceCheck(InvoiceStatus) = False Then
                MsgBox("The Invoice Number entered is not valid." & vbCrLf & "No Data saved", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            'SQL = "Update " & DBNameSpace & ".FS_Transactions set " & _
            '"invoiceid = '', " & _
            '"active = '0', " & _
            '"updateUser = '" & UserGCode & "', " & _
            '"updateDateTime = sysdate " & _
            '"where TransactionID = '" & txtTransactionID.Text & "' "

            SQL = "Update " & DBNameSpace & ".FS_Transactions set " & _
            "active = '0', " & _
            "updateUser = '" & UserGCode & "', " & _
            "updateDateTime = sysdate " & _
            "where TransactionID = '" & txtTransactionID.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            InvoiceStatusCheck(txtInvoiceID.Text)
            Update_FS_Admin_Status(mtbFeeAdminExistingYear.Text, mtbFeeAdminAIRSNumber.Text)
            RefreshAdminStatus()

            LoadTransactionData()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub InvoiceStatusCheck(ByVal invoiceID As String)
        Try
            SQL = "select " & _
            "(invoiceTotal - PaymentTotal) as Balance " & _
            "from (select " & _
            "sum(numAmount) as InvoiceTotal " & _
            "from airbranch.FS_Feeinvoice " & _
            "where invoiceid = '" & invoiceID & "' " & _
            "and Active = '1' ) INVOICED, " & _
            "(select " & _
            "case " & _
            "when sum(NumPayment) is null then 0 " & _
            "else sum(numPayment) " & _
            "End PaymentTotal " & _
            "from airbranch.FS_TRANSACTIONS " & _
            "where invoiceid = '" & invoiceID & "' " & _
            "and Active = '1' ) Payments "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("Balance")) Then
                    temp = "1"
                Else
                    temp = dr.Item("Balance")
                End If
            End While
            dr.Close()

            If temp <> "0" Then
                'Not Paid in full
                SQL = "Update " & DBNameSpace & ".FS_FeeInvoice set " & _
                "strInvoicestatus = '0' " & _
                "where invoiceId = '" & invoiceID & "' "
            Else
                'Paid in Full 
                SQL = "Update " & DBNameSpace & ".FS_FeeInvoice set " & _
                "strInvoicestatus = '1' " & _
                "where invoiceId = '" & invoiceID & "' "
            End If

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            LoadFeeInvoiceData()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveFeeAudit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveNewFeeAudit.Click
        Try
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
            Dim AuditEnforcement As String = ""
            Dim AuditComments As String = ""
            Dim AuditStart As String = ""
            Dim AuditEnd As String = ""
            Dim EndCollections As String = ""
            Dim CollectionsDate As String = ""
            Dim x As Integer = 0

            If (mtbFeeAdminAIRSNumber.Text <> txtAIRSNumber.Text) _
              Or (mtbFeeAdminExistingYear.Text <> txtYear.Text) _
              Or txtAIRSNumber.Text = "" Or mtbFeeAdminExistingYear.Text = "" _
              Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." & _
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
            'If chbEditOpStatus.Checked = True Then
            '    OpStatus = "1"
            'Else
            '    OpStatus = "0"
            'End If
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
                    For i = 0 To dgvEditExemptions.Rows.Count - 1
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

            SQL = "select count(*) as DataCheck " & _
            "From " & DBNameSpace & ".FS_FeeData " & _
            "where strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
            "and numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("DataCheck")) Then
                    temp = "0"
                Else
                    temp = dr.Item("DataCheck")
                End If
            End While
            dr.Close()

            If temp = "0" Then
                SQL = "insert into " & DBNameSpace & ".FS_FeeData " & _
                "(numfeeyear, strairsnumber, " & _
                "strComment, Active, " & _
                "UpdateUser, UpdateDateTime, " & _
                "CreateDateTime) " & _
                "values " & _
                "('" & mtbFeeAdminExistingYear.Text & "', '0413" & mtbFeeAdminAIRSNumber.Text & "', " & _
                "'Add Via IAIP Audit Process', '1', " & _
                "'IAIP||" & UserName & "', sysdate, " & _
                "sysdate) "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

            If cboStaffResponsible.SelectedValue <> "" Then
                StaffResponsible = cboStaffResponsible.SelectedValue
            Else
                StaffResponsible = UserGCode
            End If
            If StaffResponsible = "" Then
                StaffResponsible = UserGCode
            End If
            'If rdbAudit3.Checked = True Then
            '    AuditLevel = "3"
            'End If
            'If rdbAudit2.Checked = True Then
            '    AuditLevel = "2"
            'End If
            'If rdbAudit1.Checked = True Then
            '    AuditLevel = "1"
            'End If
            'If rdbAudit0.Checked = True Then
            '    AuditLevel = "0"
            'End If
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
                AuditEnforcement = txtAuditEnforcementNumber.Text
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

            SQL = "Insert into " & DBNameSpace & ".FS_FeeAudit " & _
            "values " & _
            "((select " & _
            "case " & _
            "when max(AuditID) is null then 1 " & _
            "when Max(AuditID) is not null then max(AuditID) + 1 " & _
            "else 1 " & _
            "end AuditID " & _
            "from " & DBNameSpace & ".FS_FeeAudit), " & _
            "'" & StaffResponsible & "', " & _
            "'" & AuditLevel & "', '" & AuditEnforcement & "', " & _
            "'" & Replace(AuditComments, "'", "''") & "', " & _
            "'" & AuditStart & "', '" & AuditEnd & "', " & _
            "'" & EndCollections & "', '" & CollectionsDate & "', " & _
            "'1', '" & UserGCode & "', " & _
            "'" & OracleDate & "', '" & OracleDate & "', " & _
            "'0413" & mtbFeeAdminAIRSNumber.Text & "', '" & mtbFeeAdminExistingYear.Text & "' )  "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "select max(AuditID) as AuditID " & _
            "from " & DBNameSpace & ". FS_FeeAudit " '& _
            ' "where strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
            ' "and numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("AuditID")) Then
                    txtAuditID.Clear()
                Else
                    txtAuditID.Text = dr.Item("AuditID")
                End If
            End While
            dr.Close()

            If chbMakeEdits.Checked = True Then
                SQL = "Insert into " & DBNameSpace & ".FS_FeeAmendment " & _
                "values " & _
                "('" & txtAuditID.Text & "',  " & _
                "'0413" & mtbFeeAdminAIRSNumber.Text & "', '" & mtbFeeAdminExistingYear.Text & "', " & _
                "'" & Replace(SM, "'", "''") & "', '" & Replace(SMFee, "'", "''") & "', " & _
                "'" & Replace(Part70, "'", "''") & "', '" & Replace(Part70Fee, "'", "''") & "', " & _
                "'" & Replace(VOCTons, "'", "''") & "', '" & Replace(PMTons, "'", "''") & "',  " & _
                "'" & Replace(SO2Tons, "'", "''") & "', '" & Replace(NOxTons, "'", "''") & "', " & _
                "'" & Replace(CalculatedFee, "'", "''") & "', '" & Replace(FeeRate, "'", "''") & "', " & _
                "'" & Replace(NSPS, "'", "''") & "', '" & Replace(NSPSFee, "'", "''") & "', " & _
                "'" & Replace(NSPSExempt, "'", "''") & "', '" & Replace(NSPSExemptions, "'", "''") & "', " & _
                "'" & Replace(AdminFee, "'", "''") & "', '" & Replace(TotalFee, "'", "''") & "', " & _
                "'" & Replace(Classification, "'", "''") & "', '" & Replace(OpStatus, "'", "''") & "', " & _
                "'" & Replace(ShutDown, "'", "''") & "', '" & Replace(OfficialName, "'", "''") & "', " & _
                "'" & Replace(OfficialTitle, "'", "''") & "', '" & Replace(PaymentType, "'", "''") & "', " & _
                "'1', '" & UserGCode & "', " & _
                "sysdate, sysdate) "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                cmd = New OracleCommand("AIRBranch.PD_FeeAmendment", Conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add(New OracleParameter("FeeYear", OracleType.Number)).Value = FeeYear
                cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleType.VarChar)).Value = "0413" & mtbFeeAdminAIRSNumber.Text

                cmd.ExecuteNonQuery()
            End If

            If EndCollections = "True" Then
                SQL = "update AIRBranch.FS_Admin set " & _
                "numCurrentStatus = '12' " & _
                "where numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' " & _
                "and strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                cmd.ExecuteReader()

            End If
            '  ClearEditData()
            LoadAuditedData()
            MsgBox("Audit Data Added", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex.ToString() & vbCrLf & SQL.ToString, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub rdbEditNSPSExemptTrue_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbEditNSPSExemptTrue.CheckedChanged
        Try
            Dim NSPStemp As String = ""
            Dim ReasonID As String = ""
            Dim DisplayOrder As String = ""
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 1

            If rdbEditNSPSExemptTrue.Checked = False Then
                Exit Sub
            End If

            SQL = "select " & _
            "" & DBNameSpace & ".FSLK_NspsReason.NSPSREasonCode , Description " & _
            "from " & DBNameSpace & ".FSLK_NSPSReason, " & DBNameSpace & ".fslk_NSPSReasonYear " & _
            "where " & DBNameSpace & ".FSLK_NspsReason.NSPSREasonCode = " & DBNameSpace & ".FSLK_NSPSREasonYear.nspsreasoncode  " & _
            "and numFeeYear = 2009 " & _
            "order by displayorder "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dgvEditExemptions.Rows.Clear()

            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEditExemptions)
                dgvRow.Cells(0).Value = 0
                dgvRow.Cells(1).Value = dr.Item("NSPSReasonCode")
                dgvRow.Cells(2).Value = dr.Item("description")
                dgvEditExemptions.Rows.Add(dgvRow)
            End While
            dr.Close()

            Exit Sub


            SQL = "Select " & _
            "NSPSReasonCode, DisplayOrder " & _
            "from " & DBNameSpace & ".FSLK_NSPSReasonYear " & _
            "where numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' " & _
            "order by NSPSReasonCode "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("NSPSReasonCode")) Then
                    NSPStemp = NSPStemp
                Else
                    NSPStemp = NSPStemp & dr.Item("NSPSReasonCode")
                    If IsDBNull(dr.Item("DisplayOrder")) Then
                        NSPStemp = NSPStemp & "-" & i & ","
                        i += 1
                    Else
                        NSPStemp = NSPStemp & "-" & dr.Item("DisplayOrder") & ","
                        If dr.Item("DisplayOrder") >= i Then
                            i = dr.Item("DisplayOrder") + 1
                        End If
                    End If
                End If
            End While
            dr.Close()

            dgvEditExemptions.Rows.Clear()
            Do While NSPStemp <> ""
                ReasonID = Mid(NSPStemp, 1, InStr(NSPStemp, "-", CompareMethod.Text) - 1)

                If ReasonID.Length = 1 Then
                    DisplayOrder = Mid(NSPStemp, InStr(NSPStemp, "-", CompareMethod.Text) + 1, InStr(NSPStemp, ",", CompareMethod.Text) - 3)
                Else
                    DisplayOrder = Mid(NSPStemp, InStr(NSPStemp, "-", CompareMethod.Text) + 1, InStr(NSPStemp, ",", CompareMethod.Text) - 4)
                End If


                temp = ReasonID & "-" & DisplayOrder & ","
                NSPStemp = Replace(NSPStemp, temp, "")

                'Dim x As Integer = 0
                'While x < dgvNSPSExemptions.Rows.Count
                '    Dim y As Integer = 0
                '    While y < dgvNSPSExemptions.Rows(x).Cells.Count
                '        Dim c As DataGridViewCell = dgvNSPSExemptions.Rows(x).Cells(y)
                '        If Not c.Value Is DBNull.Value Or Nothing Then
                '            If CType(c.Value, String) = ReasonID Then
                '                dgvRow = New DataGridViewRow
                '                dgvRow.CreateCells(dgvEditExemptions)
                '                dgvRow.Cells(0).Value = 0
                '                dgvRow.Cells(1).Value = dgvNSPSExemptions(0, x).Value
                '                dgvRow.Cells(2).Value = dgvNSPSExemptions(1, x).Value
                '                dgvEditExemptions.Rows.Add(dgvRow)
                '            End If
                '        End If
                '        System.Math.Min(System.Threading.Interlocked.Increment(y), y - 1)
                '    End While
                '    System.Math.Min(System.Threading.Interlocked.Increment(x), x - 1)
                'End While
            Loop

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvEditExemptions_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvEditExemptions.MouseUp
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnGenerateInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewPrintableInvoice.Click
        Try
            Dim rpt As New ReportClass
            Dim Director As String = ""
            Dim Commissioner As String = ""
            Dim TotalEmissionFees As String = ""
            Dim InvoiceDate As String = ""
            Dim PayType As String = ""
            Dim PayTypeID As String = ""
            Dim TotalInvoiceAmount As String = ""
            Dim TotalFee As String = ""
            Dim AdminFee As String = ""
            Dim DueDate As String = OracleDate
            Dim VOIDStatus As String = ""
            Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
            Dim ParameterField As CrystalDecisions.Shared.ParameterField
            Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

            If txtInvoice.Text = "" Then
                MsgBox("Please select an existing invoice to Print.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            SQL = "Select " & _
             "strManagementName from " & _
             "" & DBNameSpace & ".LookUpAPBManagementType " & _
             "where strCurrentContact = '1' " & _
             "and strKey = '1' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strManagementName")) Then
                    Director = ""
                Else
                    Director = dr.Item("strManagementName")
                End If
            End While
            dr.Close()

            SQL = "Select " & _
            "strManagementName from " & _
            "" & DBNameSpace & ".LookUpAPBManagementType " & _
            "where strCurrentContact = '1' " & _
            "and strKey = '2' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strManagementName")) Then
                    Commissioner = ""
                Else
                    Commissioner = dr.Item("strManagementName")
                End If
            End While
            dr.Close()

            SQL = "Select " & _
            "numAmount, datInvoiceDate, " & _
            "strPayType, strPayTypeDesc, " & _
            "" & DBNameSpace & ".FS_FeeInvoice.CreateDateTime, " & _
            "" & DBNameSpace & ".FS_FeeInvoice.Active " & _
            "from " & DBNameSpace & ".FS_FeeInvoice, " & _
            "" & DBNameSpace & ".FSLK_PayType " & _
            "where " & DBNameSpace & ".FS_FeeInvoice.strPayType = " & _
               "" & DBNameSpace & ".FSLK_PayType.numPayTypeID " & _
               "and InvoiceID = '" & txtInvoice.Text & "' " & _
               "and strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
               "and numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("datInvoiceDate")) Then
                    DueDate = OracleDate
                Else
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
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    InvoiceDate = OracleDate
                Else
                    InvoiceDate = dr.Item("CreateDateTime")
                End If
                If IsDBNull(dr.Item("Active")) Then
                    VOIDStatus = "1"
                Else
                    VOIDStatus = dr.Item("Active")
                End If
            End While
            dr.Close()

            SQL = "Select " & _
            "numTotalFee, numAdminFee, " & _
            "(numTotalFee - numAdminFee) as TotalEmissionFees " & _
            "from " & DBNameSpace & ".FS_FeeAuditedData " & _
            "where numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' " & _
            "and strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("TotalEmissionFees")) Then
                    TotalEmissionFees = "0"
                Else
                    TotalEmissionFees = Format(dr.Item("TotalEmissionFees"), "c")
                End If
                If IsDBNull(dr.Item("numTotalFee")) Then
                    TotalFee = "0"
                Else
                    If PayTypeID = "2" Or PayTypeID = "3" Or PayTypeID = "4" Or PayTypeID = "5" Then
                        TotalFee = Format(dr.Item("numTotalFee") / 4, "c")
                    Else
                        TotalFee = Format(dr.Item("numTotalFee"), "c")
                    End If
                End If
                If IsDBNull(dr.Item("numAdminFee")) Then
                    AdminFee = "$0.00"
                Else
                    AdminFee = Format(dr.Item("numAdminFee"), "c")
                End If
            End While
            dr.Close()

            SQL = "Select " & _
            "datTransactionDate, numPayment, " & _
            "strCheckNo, strCreditCardNo, " & _
            "strDepositNo " & _
            "From " & DBNameSpace & ".FS_Transactions " & _
            "where strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
            "and numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' " & _
            "and Active = '1' "

            ds = New DataSet

            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            ds.EnforceConstraints = False
            da.Fill(ds, "FS_Transactions")


            rpt = New crFS_Invoice
            monitor.TrackFeature("Report." & rpt.ResourceName)

            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "Director"
            spValue.Value = Director
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "Commissioner"
            spValue.Value = Commissioner
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "AIRSNumber"
            spValue.Value = Mid(mtbFeeAdminAIRSNumber.Text, 1, 3) & "-" & Mid(mtbFeeAdminAIRSNumber.Text, 4)
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "FeeYear"
            spValue.Value = mtbFeeAdminExistingYear.Text
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "TotalEmissionFees"
            spValue.Value = TotalEmissionFees
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "datInvoiceDate"
            spValue.Value = InvoiceDate
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "PaymentType"
            spValue.Value = PayType
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "TotalInvoiceAmount"
            spValue.Value = TotalInvoiceAmount
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "TotalFee"
            spValue.Value = TotalFee
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "AdminFee"
            spValue.Value = AdminFee
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "InvoiceNumber"
            spValue.Value = txtInvoice.Text
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "DueDate"
            spValue.Value = DueDate
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "VOIDStatus"
            spValue.Value = VOIDStatus
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)


            rpt.SetDataSource(ds)

            'Load Variables into the Fields
            crFeeStatsAndInvoices.ParameterFieldInfo = ParameterFields

            'Display the Report
            crFeeStatsAndInvoices.ReportSource = rpt
            crFeeStatsAndInvoices.Refresh()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub ViewAllInvoices()
        Try
            SQL = "select distinct " & _
            "" & DBNameSpace & ".FS_FeeInvoice.InvoiceID, " & _
            "" & DBNameSpace & ".FS_FeeInvoice.numAmount, " & _
            "datInvoiceDate, " & _
            "case " & _
            "when " & DBNameSpace & ".FS_FeeInvoice.active = '1' then 'Active' " & _
            "when " & DBNameSpace & ".FS_FeeInvoice.active = '0' then 'VOID' " & _
            "end InvoiceStatus, strPayTypeDesc, " & _
            "case " & _
            "when strInvoiceStatus = '1' then 'Paid in Full' " & _
            "when strInvoiceStatus = '0' and " & _
            "(numPayment <> '0' and numPayment is not null and " & DBNameSpace & ".FS_Transactions.active = '1') then 'Partial Payment' " & _
            "when strInvoicestatus = '0' then 'Unpaid' " & _
            "end PayStatus, " & _
            "" & DBNameSpace & ".FS_FeeInvoice.strComment " & _
            "from " & DBNameSpace & ".FS_FeeInvoice, " & DBNameSpace & ".FSLK_PayType, " & _
            "" & DBNameSpace & ".FS_Transactions " & _
            "where " & DBNameSpace & ".FS_FeeInvoice.strPayType = " & DBNameSpace & ".FSLK_PayType.nuMPayTypeID " & _
            "and " & DBNameSpace & ".FS_FeeInvoice.InvoiceID = " & DBNameSpace & ".FS_Transactions.InvoiceID (+) " & _
            "and " & DBNameSpace & ".FS_FeeInvoice.strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
            "and " & DBNameSpace & ".FS_FeeInvoice.numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' "

            ds = New DataSet

            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da.Fill(ds, "Invoices")
            dgvInvoices.DataSource = ds
            dgvInvoices.DataMember = "Invoices"

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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewInvoices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewInvoices.Click
        Try
            ViewAllInvoices()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvInvoices_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvInvoices.MouseUp
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddNewInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewInvoice.Click
        Try
            Dim InvoiceStatus As String = "0"

            If (mtbFeeAdminAIRSNumber.Text <> txtAIRSNumber.Text) _
             Or (mtbFeeAdminExistingYear.Text <> txtYear.Text) _
             Or txtAIRSNumber.Text = "" Or mtbFeeAdminExistingYear.Text = "" _
             Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." & _
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

            SQL = "Insert into " & DBNameSpace & ".FS_FeeINvoice " & _
            "values " & _
            "(" & DBNameSpace & ".FeeInvoice_ID.nextVal, " & _
            "'0413" & mtbFeeAdminAIRSNumber.Text & "', '" & mtbFeeAdminExistingYear.Text & "', " & _
            "'" & Replace(Replace(txtAmount.Text, "$", ""), ",", "") & "', '" & Format(DTPInvoiceDate.Value, "dd-MMM-yyyy") & "', " & _
            "'" & Replace(txtInvoiceComments.Text, "'", "''") & "', " & _
            "'1', 'IAIP||" & UserName & "', '" & OracleDate & "', " & _
            "'" & OracleDate & "', '" & cboInvoiceType.SelectedValue & "', " & _
            "'" & InvoiceStatus & "') "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            cmd = New OracleCommand("AIRBranch.PD_FEE_STATUS", Conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("FeeYear", OracleType.Number)).Value = mtbFeeAdminExistingYear.Text
            cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleType.VarChar)).Value = "0413" & mtbFeeAdminAIRSNumber.Text

            cmd.ExecuteNonQuery()

            ViewAllInvoices()

            RefreshAdminStatus()

            LoadTransactionData()

            MsgBox("New Invoice Added", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnVOIDInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVOIDInvoice.Click
        Try
            Dim TransactionID As String = ""
            Dim Payment As String = "0"

            If (mtbFeeAdminAIRSNumber.Text <> txtAIRSNumber.Text) _
            Or (mtbFeeAdminExistingYear.Text <> txtYear.Text) _
            Or txtAIRSNumber.Text = "" Or mtbFeeAdminExistingYear.Text = "" _
            Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." & _
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Select " & _
            "TransactionID, numPayment " & _
            "from " & DBNameSpace & ".FS_Transactions " & _
            "where invoiceID = '" & txtInvoice.Text & "' " & _
            "and Active <> '0' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("TransactionID")) Then
                    TransactionID = ""
                Else
                    TransactionID = dr.Item("TransactionID")
                End If
                If IsDBNull(dr.Item("numPayment")) Then
                    Payment = "0"
                Else
                    Payment = dr.Item("numPayment")
                End If
            End While
            dr.Close()

            If Payment <> "0" Then
                MsgBox("There already exists a transaction for this invoice." & vbCrLf & _
                       "Any Transaction needs to be deleted or zeroed out to VOID an Invoice.", _
                         MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            SQL = "Update " & DBNameSpace & ".FS_FeeInvoice set " & _
            "Active = '0' " & _
            "where InvoiceID = '" & txtInvoice.Text & "' "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            ViewAllInvoices()
            LoadTransactionData()

            MsgBox("Invoice VOIDED", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnVOIDAllUnpaid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVOIDAllUnpaid.Click
        Try
            Dim InvoiceID As String = ""

            If (mtbFeeAdminAIRSNumber.Text <> txtAIRSNumber.Text) _
               Or (mtbFeeAdminExistingYear.Text <> txtYear.Text) _
               Or txtAIRSNumber.Text = "" Or mtbFeeAdminExistingYear.Text = "" _
               Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." & _
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Select distinct " & _
            "" & DBNameSpace & ".FS_FeeInvoice.InvoiceID " & _
            "from " & DBNameSpace & ".FS_FeeInvoice, " & DBNameSpace & ".FS_Transactions " & _
            "where " & DBNameSpace & ".FS_FeeInvoice.invoiceid = " & DBNameSpace & ".FS_Transactions.InvoiceID (+) " & _
            "and " & DBNameSpace & ".FS_FeeInvoice.Active = '1' " & _
            "and " & DBNameSpace & ".FS_FeeInvoice.strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
            "and " & DBNameSpace & ".FS_FeeInvoice.numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' " & _
            "and (numPayment is null or numPayment = '0' ) "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("InvoiceID")) Then
                    InvoiceID = ""
                Else
                    InvoiceID = dr.Item("InvoiceID")
                End If

                If InvoiceID <> "" Then
                    SQL = "Update " & DBNameSpace & ".FS_FeeInvoice set " & _
                    "Active = '0' " & _
                    "where invoiceID = '" & InvoiceID & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

            ViewAllInvoices()
            LoadTransactionData()
            MsgBox("All unpaid Invoices have been VOIDED.", MsgBoxStyle.Exclamation, Me.Text)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRemoveVOID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveVOID.Click
        Try
            If (mtbFeeAdminAIRSNumber.Text <> txtAIRSNumber.Text) _
              Or (mtbFeeAdminExistingYear.Text <> txtYear.Text) _
              Or txtAIRSNumber.Text = "" Or mtbFeeAdminExistingYear.Text = "" _
              Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." & _
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If txtInvoice.Text <> "" Then
                SQL = "Update " & DBNameSpace & ".FS_FeeInvoice set " & _
                "Active = '1' " & _
                "where invoiceID = '" & txtInvoice.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                txtStatus.Text = "Active"

                ViewAllInvoices()
                LoadTransactionData()
                MsgBox("Invoice VOID Status Removed.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbEndFeeCollectoins_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbEndFeeCollectoins.CheckStateChanged
        Try
            'DTPDateCollectionsCeased
            If chbEndFeeCollectoins.Checked = True Then
                DTPDateCollectionsCeased.Enabled = True
            Else
                DTPDateCollectionsCeased.Enabled = False
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditFeeAudit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditFeeAudit.Click
        Try
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
            Dim x As Integer = 0

            If (mtbFeeAdminAIRSNumber.Text <> txtAIRSNumber.Text) _
            Or (mtbFeeAdminExistingYear.Text <> txtYear.Text) _
            Or txtAIRSNumber.Text = "" Or mtbFeeAdminExistingYear.Text = "" _
            Or txtYear.Text = "" Then
                MsgBox("The currently selected AIRS # does not match the selecting AIRS #." & _
                       vbCrLf & "NO DATA HAS BEEN SAVED", MsgBoxStyle.Exclamation, Me.Text)
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

            'If chbEditOpStatus.Checked = True Then
            '    OpStatus = "1"
            'Else
            '    OpStatus = "0"
            'End If
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
                    For i = 0 To dgvEditExemptions.Rows.Count - 1
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

            'SQL = "select count(*) as DataCheck " & _
            '"From " & DBNameSpace & ".FS_FeeData " & _
            '"where strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
            '"and numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' "

            'cmd = New OracleCommand(SQL, conn)
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If
            'dr = cmd.ExecuteReader
            'While dr.Read
            '    If IsDBNull(dr.Item("DataCheck")) Then
            '        temp = "0"
            '    Else
            '        temp = dr.Item("DataCheck")
            '    End If
            'End While
            'dr.Close()

            'If temp = "0" Then
            '    SQL = "insert into " & DBNameSpace & ".FS_FeeData " & _
            '    "(numfeeyear, strairsnumber, " & _
            '    "strComment, Active, " & _
            '    "UpdateUser, UpdateDateTime, " & _
            '    "CreateDateTime) " & _
            '    "values " & _
            '    "('" & mtbFeeAdminExistingYear.Text & "', '0413" & mtbFeeAdminAIRSNumber.Text & "', " & _
            '    "'Add Via IAIP Audit Process', '1', " & _
            '    "'IAIP||" & UserName & "', sysdate, " & _
            '    "sysdate) "
            '    cmd = New OracleCommand(SQL, conn)
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If
            '    dr = cmd.ExecuteReader
            '    dr.Close()
            'End If

            If chbMakeEdits.Checked = True Then
                SQL = "select updateuser " & _
                "from " & DBNameSpace & ".FS_FeeAmendment " & _
                "where numfeeyear = '" & FeeYear & "' " & _
                "and strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
                "and auditID = '" & txtAuditID.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".FS_FeeAmendment set " & _
                    "strSyntheticMinor = '" & SM & "', " & _
                    "numSMFee = '" & SMFee & "',  " & _
                    "strPart70 = '" & Part70 & "', " & _
                    "numPart70Fee = '" & Part70Fee & "', " & _
                    "intVOCTons = '" & VOCTons & "', " & _
                    "intPMTons = '" & PMTons & "', " & _
                    "intSO2Tons = '" & SO2Tons & "', " & _
                    "intNOxTons = '" & NOxTons & "', " & _
                    "numCalculatedFee = '" & CalculatedFee & "', " & _
                    "numFeeRate = '" & FeeRate & "', " & _
                    "strNSPS = '" & NSPS & "', " & _
                    "nuMNSPSFee = '" & NSPSFee & "', " & _
                    "strNSPSExempt = '" & NSPSExempt & "', " & _
                    "strNSPSExemptReason = '" & NSPSExemptions & "', " & _
                    "numAdminFee = '" & AdminFee & "', " & _
                    "numTotalFee = '" & TotalFee & "', " & _
                    "strClass = '" & Classification & "', " & _
                    "strOperate = '" & OpStatus & "', " & _
                    "datShutDown = '" & ShutDown & "', " & _
                    "strOfficialName = '" & OfficialName & "', " & _
                    "strOfficialTitle = '" & OfficialTitle & "', " & _
                    "strPaymentPlan = '" & PaymentType & "', " & _
                    "UpdateUser = '" & UserGCode & "', " & _
                    "updateDateTime = sysdate " & _
                    "where AuditID = '" & txtAuditID.Text & "' "
                Else
                    SQL = "Insert into " & DBNameSpace & ".FS_FeeAmendment " & _
                    "values " & _
                    "('" & txtAuditID.Text & "',  " & _
                    "'0413" & mtbFeeAdminAIRSNumber.Text & "', '" & mtbFeeAdminExistingYear.Text & "', " & _
                    "'" & Replace(SM, "'", "''") & "', '" & Replace(SMFee, "'", "''") & "', " & _
                    "'" & Replace(Part70, "'", "''") & "', '" & Replace(Part70Fee, "'", "''") & "', " & _
                    "'" & Replace(VOCTons, "'", "''") & "', '" & Replace(PMTons, "'", "''") & "',  " & _
                    "'" & Replace(SO2Tons, "'", "''") & "', '" & Replace(NOxTons, "'", "''") & "', " & _
                    "'" & Replace(CalculatedFee, "'", "''") & "', '" & Replace(FeeRate, "'", "''") & "', " & _
                    "'" & Replace(NSPS, "'", "''") & "', '" & Replace(NSPSFee, "'", "''") & "', " & _
                    "'" & Replace(NSPSExempt, "'", "''") & "', '" & Replace(NSPSExemptions, "'", "''") & "', " & _
                    "'" & Replace(AdminFee, "'", "''") & "', '" & Replace(TotalFee, "'", "''") & "', " & _
                    "'" & Replace(Classification, "'", "''") & "', '" & Replace(OpStatus, "'", "''") & "', " & _
                    "'" & Replace(ShutDown, "'", "''") & "', '" & Replace(OfficialName, "'", "''") & "', " & _
                    "'" & Replace(OfficialTitle, "'", "''") & "', '" & Replace(PaymentType, "'", "''") & "', " & _
                    "'1', '" & UserGCode & "', " & _
                    "sysdate, sysdate) "
                End If

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                cmd = New OracleCommand("AIRBranch.PD_FeeAmendment", Conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add(New OracleParameter("FeeYear", OracleType.Number)).Value = FeeYear
                cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleType.VarChar)).Value = "0413" & mtbFeeAdminAIRSNumber.Text

                cmd.ExecuteNonQuery()



            End If

            If cboStaffResponsible.SelectedValue <> "" Then
                StaffResponsible = cboStaffResponsible.SelectedValue
            Else
                StaffResponsible = UserGCode
            End If
            If StaffResponsible = "" Then
                StaffResponsible = UserGCode
            End If
            'If rdbAudit3.Checked = True Then
            '    AuditLevel = "3"
            'End If
            'If rdbAudit2.Checked = True Then
            '    AuditLevel = "2"
            'End If
            'If rdbAudit1.Checked = True Then
            '    AuditLevel = "1"
            'End If
            'If rdbAudit0.Checked = True Then
            '    AuditLevel = "0"
            'End If
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

            SQL = "Update " & DBNameSpace & ".FS_FeeAudit set " & _
            "numStaffResponsible = '" & StaffResponsible & "', " & _
            "strAuditLevel = '" & AuditLevel & "', " & _
            "numENFORCEMENT = '" & AuditENFORCEMENT & "', " & _
            "strComments = '" & Replace(AuditComments, "'", "''") & "', " & _
            "datAuditStart = '" & AuditStart & "', " & _
            "datAuditEnd = '" & AuditEnd & "', " & _
            "strEndCollections = '" & EndCollections & "', " & _
            "datCollectionsEnded = '" & CollectionsDate & "', " & _
            "updateuser = '" & UserGCode & "', " & _
            "updateDateTime = sysdate " & _
            "where AuditID = '" & txtAuditID.Text & "' "


            'SQL = "Insert into " & DBNameSpace & ".FS_FeeAudit " & _
            '"values " & _
            '"('" & txtAuditID.Text & "', '" & StaffResponsible & "', " & _
            '"'" & AuditLevel & "', '" & AuditNOV & "', " & _
            '"'" & AuditCO & "', '" & Replace(AuditComments, "'", "''") & "', " & _
            '"'" & AuditStart & "', '" & AuditEnd & "', " & _
            '"'" & EndCollections & "', '" & CollectionsDate & "', " & _
            '"'1', '" & UserGCode & "', " & _
            '"'" & OracleDate & "', '" & OracleDate & "') "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            If EndCollections = "True" Then
                SQL = "update AIRBranch.FS_Admin set " & _
                "numCurrentStatus = '12' " & _
                "where numFeeYear = '" & mtbFeeAdminExistingYear.Text & "' " & _
                "and strAIRSNumber = '0413" & mtbFeeAdminAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                cmd.ExecuteReader()

            End If

            '    ClearEditData()
            LoadAuditedData()

            MsgBox("Audit Data Saved", MsgBoxStyle.Information, Me.Text)
        Catch ex As Exception
            ErrorReport(ex.ToString() & vbCrLf & SQL.ToString, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSelectAuditToEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAuditToEdit.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            Dim temp2 As String = ""
            Dim AuditID As String = ""
            Dim AuditLevel As String = ""
            Dim NSPSExempt As String = ""
            Dim NSPSExemptions As String = ""

            If dgvAuditHistory.CurrentRow IsNot Nothing Then
            Else
                Exit Sub
            End If
            ClearAuditData()

            txtAuditID.Text = dgvAuditHistory(0, dgvAuditHistory.CurrentRow.Index).Value

            SQL = "Select * " & _
            "From " & DBNameSpace & ".FS_FeeAudit " & _
            "where AuditID = '" & txtAuditID.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
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
            End While
            dr.Close()
            Select Case AuditLevel
                Case "0"
                    'rdbAudit0.Checked = True
                    cboAuditType.Text = "Facility Self Amendment"
                Case "1"
                    'rdbAudit1.Checked = True
                    cboAuditType.Text = "Level 1 Audit"
                Case "2"
                    'rdbAudit2.Checked = True
                    cboAuditType.Text = "Level 2 Audit"
                Case "3"
                    'rdbAudit3.Checked = True
                    cboAuditType.Text = "Level 3 Audit"
                Case "4"
                    cboAuditType.Text = "Other"
                Case Else
                    cboAuditType.Text = "Other"
                    'rdbAudit0.Checked = False
                    'rdbAudit1.Checked = False
                    'rdbAudit2.Checked = False
                    'rdbAudit3.Checked = False
            End Select

            SQL = "select * " & _
            "from " & DBNameSpace & ".FS_FeeAmendment " & _
            "where auditID = '" & txtAuditID.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
                    'chbEditOpStatus.Checked = False
                Else
                    If dr.Item("strOperate") = "1" Then
                        rdbEditOpStatusTrue.Checked = True
                        'chbEditOpStatus.Checked = True
                    Else
                        rdbEditOpStatusFalse.Checked = True
                        'chbEditOpStatus.Checked = False
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

            End While
            dr.Close()

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
                            Dim y As Integer = 0
                            If dgvEditExemptions(1, x).Value = temp Then
                                dgvEditExemptions(0, x).Value = True
                            Else
                                '   temp2 = dgvEditExemptions(1, x).Value
                            End If
                            x += 1
                        End While
                    Loop
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnExportAuditToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportAuditToExcel.Click
        Try
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvAuditHistory.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvAuditHistory.ColumnCount - 1
                        .Cells(1, i + 1) = dgvAuditHistory.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvAuditHistory.ColumnCount - 1
                        For j = 0 To dgvAuditHistory.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvAuditHistory.Item(i, j).Value.ToString
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
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        End Try
    End Sub
    Private Sub btnClearAuditData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAuditData.Click
        Try
ClearAuditData
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub ClearAuditData()
        Try
            txtAuditID.Clear()
            cboStaffResponsible.Text = ""
            'rdbAudit0.Checked = True
            cboAuditType.Text = ""
            txtAuditComment.Clear()
            txtAuditEnforcementNumber.Clear()
            DTPAuditStart.Text = OracleDate
            DTPAuditEnd.Text = OracleDate
            DTPAuditEnd.Checked = False
            DTPDateCollectionsCeased.Text = OracleDate
            chbEndFeeCollectoins.Checked = False
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
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearInvoiceData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearInvoiceData.Click
        Try
         ClearInvoices
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearInvoices()
        Try
            txtInvoice.Clear()
            txtAmount.Clear()
            DTPInvoiceDate.Text = OracleDate
            txtStatus.Clear()
            cboInvoiceType.Text = ""
            txtPayStatus.Clear()
            txtInvoiceComments.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFindTransactions4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindTransactions.Click
        Try

            If rdbCurrentFeeyear.Checked = True Then
                LoadTransactionData()
            Else
                SQL = "select " & _
         "TRANSACTIONID,  INVOICES.INVOICEID, DATTRANSACTIONDATE, " & _
         "NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, STRBATCHNO, " & _
         "ENTRYPERSON, " & _
         "STRCOMMENT, STRCREDITCARDNO, TRANSACTIONTYPECODE, " & _
         "case " & _
         "when TRANSACTIONS.UPDATEUSER is not null then (STRLASTNAME||', '||STRFIRSTNAME) " & _
         "else '' " & _
         "end  UpdateUser, " & _
         "TRANSACTIONS.UPDATEDATETIME, " & _
         "TRANSACTIONS.CREATEDATETIME, TRANSACTIONS.numFeeYear " & _
         " from " & _
         "(select " & _
         "TRANSACTIONID,  INVOICEID, DATTRANSACTIONDATE, " & _
         "NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, STRBATCHNO, " & _
         "(STRLASTNAME||', '||STRFIRSTNAME) as ENTRYPERSON, " & _
         "STRCOMMENT, strCreditcardno, " & _
         "transactiontypecode, " & _
         "UPDATEUSER, UPDATEDATETIME, " & _
         "createDateTime, strairsnumber, numfeeyear  " & _
         "from " & DBNameSpace & ".FS_TRANSACTIONS, " & DBNameSpace & ".EPDUSERPROFILES " & _
         "where " & DBNameSpace & ".FS_TRANSACTIONS.STRENTRYPERSON = " & DBNameSpace & ".EPDUSERPROFILES.NUMUSERID " & _
         "and " & DBNameSpace & ".FS_TRANSACTIONS.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
         "and active = 1) TRANSACTIONS,  " & _
         "(select " & _
         "0, INVOICEID, " & _
         "sysdate, 1, '', '', " & _
         "'', '', '', '', 2, " & _
         "UPDATEUSER, UPDATEDATETIME, " & _
         "CREATEDATETIME, STRAIRSNUMBER, NUMFEEYEAR   " & _
         "from " & DBNameSpace & ".FS_feeINVOICE " & _
         "where STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
         "and " & DBNameSpace & ".FS_feeINVOICE.Active = '1' ) INVOICES, " & _
         "" & DBNameSpace & ".EPDUSERPROFILES " & _
         "where TRANSACTIONS.STRAIRSNUMBER  =  INVOICES.STRAIRSNUMBER (+) " & _
         "and TRANSACTIONS.NUMFEEYEAR  =  INVOICES.NUMFEEYEAR  (+) " & _
         "and TRANSACTIONS.INVOICEID  =  INVOICES.INVOICEID (+) " & _
         "and TRANSACTIONS.UPDATEUSER  = " & DBNameSpace & ".epduserProfiles.numUserID   (+) " & _
         " union " & _
         "select " & _
         "TRANSACTIONID,  INVOICES.INVOICEID, DATTRANSACTIONDATE, " & _
         "NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, STRBATCHNO, " & _
         "ENTRYPERSON, " & _
         "STRCOMMENT, STRCREDITCARDNO, TRANSACTIONTYPECODE, " & _
         "case " & _
         "when TRANSACTIONS.UPDATEUSER is not null then (STRLASTNAME||', '||STRFIRSTNAME) " & _
         "else '' " & _
         "end  UpdateUser, " & _
         "TRANSACTIONS.UPDATEDATETIME, " & _
         "TRANSACTIONS.CREATEDATETIME, TRANSACTIONS.numFeeYear  " & _
         " from " & _
         "(select " & _
         "TRANSACTIONID,  INVOICEID, DATTRANSACTIONDATE, " & _
         "NUMPAYMENT, STRCHECKNO, STRDEPOSITNO, STRBATCHNO, " & _
         "(STRLASTNAME||', '||STRFIRSTNAME) as ENTRYPERSON, " & _
         "STRCOMMENT, strCreditcardno, " & _
         "transactiontypecode, " & _
         "UPDATEUSER, UPDATEDATETIME, " & _
         "createDateTime, strairsnumber, numfeeyear  " & _
         "from " & DBNameSpace & ".FS_TRANSACTIONS, " & DBNameSpace & ".EPDUSERPROFILES " & _
         "where " & DBNameSpace & ".FS_TRANSACTIONS.STRENTRYPERSON = " & DBNameSpace & ".EPDUSERPROFILES.NUMUSERID " & _
         "and " & DBNameSpace & ".FS_TRANSACTIONS.STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
         "and active = 1) TRANSACTIONS,  " & _
         "(select " & _
         "0, INVOICEID, " & _
         "sysdate, 1, '', '', " & _
         "'', '', '', '', 2, " & _
         "UPDATEUSER, UPDATEDATETIME, " & _
         "CREATEDATETIME, STRAIRSNUMBER, NUMFEEYEAR   " & _
         "from " & DBNameSpace & ".FS_feeINVOICE " & _
         "where STRAIRSNUMBER = '0413" & mtbFeeAdminAIRSNumber.Text & "' " & _
         "and " & DBNameSpace & ".FS_feeINVOICE.Active = '1') INVOICES, " & _
         "" & DBNameSpace & ".EPDUSERPROFILES " & _
         "where  INVOICES.STRAIRSNUMBER  = TRANSACTIONS.STRAIRSNUMBER (+) " & _
         "and INVOICES.NUMFEEYEAR  =  TRANSACTIONS.NUMFEEYEAR  (+) " & _
         "and INVOICES.INVOICEID  =  TRANSACTIONS.INVOICEID (+) " & _
         "and TRANSACTIONS.UPDATEUSER  = " & DBNameSpace & ".epduserProfiles.numUserID (+) "

                ds = New DataSet
                da = New OracleDataAdapter(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                da.Fill(ds, "Transactions")
                dgvTransactions.DataSource = ds
                dgvTransactions.DataMember = "Transactions"

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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbMakeEdits_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbMakeEdits.CheckedChanged
        Try
            If chbMakeEdits.Checked = True Then
                pnlInvoiceData.Enabled = True
                pnlFacilityData.Enabled = True
                pnlFacilityData2.Enabled = True
            Else
                pnlInvoiceData.Enabled = False
                pnlFacilityData.Enabled = False
                pnlFacilityData2.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

     
    Private Sub btnCalculateDays_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculateDays.Click
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnCheckInvoices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckInvoices.Click
        Try

            Validate_FS_Invoices(mtbFeeAdminExistingYear.Text, mtbFeeAdminAIRSNumber.Text)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbChangeInvoiceNumber_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbChangeInvoiceNumber.CheckedChanged
        Try
            If chbChangeInvoiceNumber.Checked = True Then
                txtInvoiceID.ReadOnly = False
            Else
                txtInvoiceID.ReadOnly = True
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class

