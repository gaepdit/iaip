Imports System.Data.OracleClient


Public Class SSCPEnforcement
    Dim SQL, SQL2, SQL3 As String
    Dim SQL4 As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean

    Dim ds As DataSet
    Dim da As OracleDataAdapter

    Dim dsStaff As DataSet
    Dim daStaff As OracleDataAdapter
    Dim dsComplianceStatus As DataSet
    Dim daComplianceStatus As OracleDataAdapter
    Dim dsHPV As DataSet
    Dim daHPV As OracleDataAdapter
    Dim dsStipulatedPenalty As DataSet
    Dim daStipulatedPenalty As OracleDataAdapter

    Private Sub SSCPEnforcement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try


            Panel3.Text = OracleDate
            Panel2.Text = UserName
            Panel1.Text = "Select Enforcement options... "

            LoadDefualts()
            Loadcombos()

            btnSubmitEnforcementToEPA.Visible = False
            btnManuallyEnterAFS.Visible = False
            If txtEnforcementNumber.Text = "" And txtAIRSNumber.Text <> "" Then
                LoadEnforcementPollutants2()
            End If

            If AccountArray(48, 3) = "1" Then
                DTPEnforcementResolved.Enabled = True
            Else
                DTPEnforcementResolved.Enabled = False
            End If

            SetUserPermissions()

            cboStaffResponsible.SelectedValue = UserGCode

            LoadEnforcement()

            If AccountArray(48, 2) = "1" Or AccountArray(48, 3) = "1" Or AccountArray(48, 4) = "1" Then
                CheckOpenStatus()
            End If

            If TCEnforcement.TabPages.Contains(Me.TPCO) Then
                If NavigationScreen.pnl4.Text = "TESTING ENVIRONMENT" Then
                    btnUploadCO.Visible = True
                    If Me.txtCONumber.Text <> "" Then
                        btnDownloadCO.Visible = True
                    Else
                        btnDownloadCO.Visible = False
                    End If
                Else
                    btnUploadCO.Visible = False
                    btnDownloadCO.Visible = False
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#Region "Page Load Functions"
    Sub LoadDefualts()
        Try

            '  TCEnforcement.TabPages.Remove(TPGeneralInfo)
            TCEnforcement.TabPages.Remove(TPLON)
            TCEnforcement.TabPages.Remove(TPNOV)
            TCEnforcement.TabPages.Remove(TPCO)
            TCEnforcement.TabPages.Remove(TPAO)
            'TCEnforcement.TabPages.Remove(TPPollutants)

            ' TCEnforcement.TabPages.Add(TPGeneralInfo)

            DTPViolationDate.Text = OracleDate
            DTPViolationDate.Checked = False
            DTPDayZero.Text = OracleDate
            DTPDayZero.Checked = False
            DTPEnforcementResolved.Text = OracleDate
            DTPEnforcementResolved.Checked = False
            DTPLONDateToUC.Text = OracleDate
            DTPLONDateToUC.Checked = False
            DTPLONSent.Text = OracleDate
            DTPLONSent.Checked = False
            DTPLonResolved.Text = OracleDate
            DTPLonResolved.Checked = False
            DTPNOVDateToUC.Text = OracleDate
            DTPNOVDateToUC.Checked = False
            DTPNOVDateToPM.Text = OracleDate
            DTPNOVDateToPM.Checked = False
            DTPNOVsent.Text = OracleDate
            DTPNOVsent.Checked = False
            DTPNOVReceived.Text = OracleDate
            DTPNOVReceived.Checked = False
            DTPNFADateToUC.Text = OracleDate
            DTPNFADateToUC.Checked = False
            DTPNFADateToPM.Text = OracleDate
            DTPNFADateToPM.Checked = False
            DTPNFALetterSent.Text = OracleDate
            DTPNFALetterSent.Checked = False
            DTPCODateToUC.Text = OracleDate
            DTPCODateToUC.Checked = False
            DTPCODateToPM.Text = OracleDate
            DTPCODateToPM.Checked = False
            DTPCOProposed.Text = OracleDate
            DTPCOProposed.Checked = False
            DTPCOReceivedfromCompany.Text = OracleDate
            DTPCOReceivedfromCompany.Checked = False
            DTPCOReceivedfromDirector.Text = OracleDate
            DTPCOReceivedfromDirector.Checked = False
            DTPConsentOrderExecuted.Text = OracleDate
            DTPConsentOrderExecuted.Checked = False
            DTPCOResolved.Text = OracleDate
            DTPCOResolved.Checked = False
            DTPAOExecuted.Text = OracleDate
            DTPAOExecuted.Checked = False
            DTPAOAppealed.Text = OracleDate
            DTPAOAppealed.Checked = False
            DTPAOResolved.Text = OracleDate
            DTPAOResolved.Checked = False

            DTPDayZero.Visible = False
            btn45DayZero.Visible = False
            lblDayZero.Visible = False
            lblPollutants.Visible = False
            lblPollutantStatus.Visible = False
            btnEditAirProgramPollutants.Visible = False
            cboPollutantStatus.Visible = False
            chbHPV.Visible = False
            cboHPVType.Visible = False
            btnSubmitToUC.Visible = False
            btnSubmitEnforcementToEPA.Visible = False
            btnManuallyEnterAFS.Visible = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub Loadcombos()
        Dim dtAirPrograms As New DataTable
        Dim dtComplianceStatus As New DataTable
        Dim dtHPV As New DataTable
        Dim dtStaff As New DataTable

        Dim drNewRow As DataRow
        Dim drDSRow As DataRow
        Dim drDSRow2 As DataRow
        Dim drDSRow3 As DataRow

        Try

            SQL = "Select " & _
            "strComplianceCode, " & _
            "(strComplianceCode || ' - ' || strComplianceDesc) as ComplianceDesc " & _
            "from " & connNameSpace & ".LookUpComplianceStatus "

            SQL2 = "select " & _
            "strHPVCode, " & _
            "(strHPVCode || ' - ' || strHPVViolationDesc) as HPVViolationDesc " & _
            "from " & connNameSpace & ".LookUPHPVViolations "

            SQL3 = "Select distinct(numUserID), " & _
            "(strLastName|| ', '||strFirstName) as StaffName, " & _
            "strLastName " & _
            "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".SSCPEnforcementItems " & _
            "where numProgram = '4' " & _
            "or numUserID = strStaffResponsible " & _
            "or (numBranch = '5' " & _
            "and strLastName = 'District') " & _
            "order by strLastName "

            dsComplianceStatus = New DataSet
            dsHPV = New DataSet
            dsStaff = New DataSet

            daComplianceStatus = New OracleDataAdapter(SQL, conn)
            daHPV = New OracleDataAdapter(SQL2, conn)
            daStaff = New OracleDataAdapter(SQL3, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            daComplianceStatus.Fill(dsComplianceStatus, "ComplianceStatus")
            daHPV.Fill(dsHPV, "HPV")
            daStaff.Fill(dsStaff, "Staff")

            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If

            dtComplianceStatus.Columns.Add("ComplianceDesc", GetType(System.String))
            dtComplianceStatus.Columns.Add("strComplianceCode", GetType(System.String))

            dtHPV.Columns.Add("HPVViolationDesc", GetType(System.String))
            dtHPV.Columns.Add("strHPVCode", GetType(System.String))

            dtStaff.Columns.Add("numUserID", GetType(System.String))
            dtStaff.Columns.Add("StaffName", GetType(System.String))

            drNewRow = dtComplianceStatus.NewRow()
            drNewRow("ComplianceDesc") = " "
            drNewRow("strComplianceCode") = ""
            dtComplianceStatus.Rows.Add(drNewRow)

            For Each drDSRow In dsComplianceStatus.Tables("ComplianceStatus").Rows()
                drNewRow = dtComplianceStatus.NewRow
                drNewRow("ComplianceDesc") = drDSRow("ComplianceDesc")
                drNewRow("strComplianceCode") = drDSRow("strComplianceCode")
                dtComplianceStatus.Rows.Add(drNewRow)
            Next

            With cboPollutantStatus
                .DataSource = dtComplianceStatus
                .DisplayMember = "ComplianceDesc"
                .ValueMember = "strComplianceCode"
                .SelectedIndex = 0
            End With

            drNewRow = dtHPV.NewRow()
            drNewRow("HPVViolationDesc") = " "
            drNewRow("strHPVCode") = ""
            dtHPV.Rows.Add(drNewRow)

            For Each drDSRow2 In dsHPV.Tables("HPV").Rows()
                drNewRow = dtHPV.NewRow
                drNewRow("HPVViolationDesc") = drDSRow2("HPVViolationDesc")
                drNewRow("strHPVCode") = drDSRow2("strHPVCode")
                dtHPV.Rows.Add(drNewRow)
            Next

            With cboHPVType
                .DataSource = dtHPV
                .DisplayMember = "HPVViolationDesc"
                .ValueMember = "strHPVCode"
                .SelectedIndex = 0
            End With

            drNewRow = dtStaff.NewRow()
            drNewRow("numUserID") = "0"
            drNewRow("StaffName") = "N/A"
            dtStaff.Rows.Add(drNewRow)

            For Each drDSRow3 In dsStaff.Tables("Staff").Rows()
                drNewRow = dtStaff.NewRow
                drNewRow("numUserID") = drDSRow3("numUserID")
                drNewRow("StaffName") = drDSRow3("StaffName")
                dtStaff.Rows.Add(drNewRow)
            Next

            With cboStaffResponsible
                .DataSource = dtStaff
                .DisplayMember = "StaffName"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub SetUserPermissions()
        Try

            If AccountArray(48, 3) = "1" Then
                btnLinkEnforcement.Enabled = True
                DTPViolationDate.Enabled = True
                chbLON.Enabled = True
                chbNOV.Enabled = True
                chbCO.Enabled = True
                chbAO.Enabled = True
                DTPDayZero.Enabled = True
                btnEditAirProgramPollutants.Enabled = True
                cboPollutantStatus.Enabled = True
                cboStaffResponsible.Enabled = True
                txtGeneralComments.ReadOnly = False
                chbHPV.Enabled = True
                cboHPVType.Enabled = True
                btnSubmitToUC.Enabled = True
                btnSubmitEnforcementToEPA.Enabled = True
                btnManuallyEnterAFS.Enabled = True
                DTPLONDateToUC.Enabled = True
                DTPLONSent.Enabled = True
                DTPLonResolved.Enabled = True
                txtLONComments.ReadOnly = False
                DTPNOVDateToUC.Enabled = True
                DTPNOVDateToPM.Enabled = True
                DTPNOVsent.Enabled = True
                DTPNOVReceived.Enabled = True
                DTPNFADateToUC.Enabled = True
                DTPNFADateToPM.Enabled = True
                DTPNFALetterSent.Enabled = True
                btnSaveNOVComments.Enabled = True
                txtNOVComments.ReadOnly = False
                cboNOVEntries.Enabled = True
                btnRemoveNOVComment.Enabled = True
                txtNOVPastComments.ReadOnly = False
                DTPCODateToUC.Enabled = True
                DTPCODateToPM.Enabled = True
                DTPCOProposed.Enabled = True
                DTPCOReceivedfromCompany.Enabled = True
                DTPCOReceivedfromDirector.Enabled = True
                DTPConsentOrderExecuted.Enabled = True
                txtCONumber.ReadOnly = False
                DTPCOResolved.Enabled = True
                txtCOPenaltyAmount.ReadOnly = False
                txtPenaltyComments.ReadOnly = False
                btnSaveCOComments.Enabled = True
                txtCOComments.ReadOnly = False
                cboCOEntries.Enabled = True
                btnDeleteCOComments.Enabled = True
                txtCoPastComments.ReadOnly = False
                txtStipulatedPenalty.ReadOnly = False
                btnSaveStipulatePenalty.Enabled = True
                btnClearStipulated.Enabled = True
                txtStipulatedComments.ReadOnly = False
                dgvStipulatedPenalties.Enabled = True
                DTPAOExecuted.Enabled = True
                DTPAOAppealed.Enabled = True
                DTPAOResolved.Enabled = True
                btnSaveAOComments.Enabled = True
                txtAOComments.ReadOnly = False
                cboAOEntries.Enabled = True
                btnRemoveAOEntries.Enabled = True
                txtAOPastComments.ReadOnly = False

                tsbSave.Enabled = True
                mmiSave.Enabled = True
            Else
                btnLinkEnforcement.Enabled = False
                DTPViolationDate.Enabled = False
                chbLON.Enabled = False
                chbNOV.Enabled = False
                chbCO.Enabled = False
                chbAO.Enabled = False
                DTPDayZero.Enabled = False
                btnEditAirProgramPollutants.Enabled = False
                cboPollutantStatus.Enabled = False
                cboStaffResponsible.Enabled = False
                txtGeneralComments.ReadOnly = True
                chbHPV.Enabled = False
                cboHPVType.Enabled = False
                DTPEnforcementResolved.Enabled = False
                btnSubmitToUC.Enabled = False
                btnSubmitEnforcementToEPA.Enabled = False
                btnManuallyEnterAFS.Enabled = False
                DTPLONDateToUC.Enabled = False
                DTPLONSent.Enabled = False
                DTPLonResolved.Enabled = False
                txtLONComments.ReadOnly = True
                DTPNOVDateToUC.Enabled = False
                DTPNOVDateToPM.Enabled = False
                DTPNOVsent.Enabled = False
                DTPNOVReceived.Enabled = False
                DTPNFADateToUC.Enabled = False
                DTPNFADateToPM.Enabled = False
                DTPNFALetterSent.Enabled = False
                btnSaveNOVComments.Enabled = False
                txtNOVComments.ReadOnly = True
                cboNOVEntries.Enabled = False
                btnRemoveNOVComment.Enabled = False
                txtNOVPastComments.ReadOnly = True
                DTPCODateToUC.Enabled = False
                DTPCODateToPM.Enabled = False
                DTPCOProposed.Enabled = False
                DTPCOReceivedfromCompany.Enabled = False
                DTPCOReceivedfromDirector.Enabled = False
                DTPConsentOrderExecuted.Enabled = False
                txtCONumber.ReadOnly = True
                DTPCOResolved.Enabled = False
                txtCOPenaltyAmount.ReadOnly = True
                txtPenaltyComments.ReadOnly = True
                btnSaveCOComments.Enabled = False
                txtCOComments.ReadOnly = True
                cboCOEntries.Enabled = False
                btnDeleteCOComments.Enabled = False
                txtCoPastComments.ReadOnly = True
                txtStipulatedPenalty.ReadOnly = True
                btnSaveStipulatePenalty.Enabled = False
                btnClearStipulated.Enabled = False
                txtStipulatedComments.ReadOnly = True
                dgvStipulatedPenalties.Enabled = False
                DTPAOExecuted.Enabled = False
                DTPAOAppealed.Enabled = False
                DTPAOResolved.Enabled = False
                btnSaveAOComments.Enabled = False
                txtAOComments.ReadOnly = True
                cboAOEntries.Enabled = False
                btnRemoveAOEntries.Enabled = False
                txtAOPastComments.ReadOnly = True

                tsbSave.Enabled = False
                mmiSave.Enabled = False
                mmiSave.Visible = False
                tsbSave.Visible = False
                tsbDelete.Visible = False
                mmiDelete.Visible = False

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub CheckOpenStatus()
        Try

            If DTPEnforcementResolved.Checked = True Then
                btnLinkEnforcement.Enabled = False
                DTPViolationDate.Enabled = False
                chbLON.Enabled = False
                chbNOV.Enabled = False
                chbCO.Enabled = False
                chbAO.Enabled = False
                DTPDayZero.Enabled = False
                btnEditAirProgramPollutants.Enabled = False
                cboPollutantStatus.Enabled = False
                cboStaffResponsible.Enabled = False
                txtGeneralComments.ReadOnly = True
                chbHPV.Enabled = False
                cboHPVType.Enabled = False
                DTPEnforcementResolved.Enabled = False
                btnSubmitToUC.Enabled = False
                btnSubmitEnforcementToEPA.Enabled = False
                btnManuallyEnterAFS.Enabled = False
                DTPLONDateToUC.Enabled = False
                DTPLONSent.Enabled = False
                DTPLonResolved.Enabled = False
                txtLONComments.ReadOnly = True
                DTPNOVDateToUC.Enabled = False
                DTPNOVDateToPM.Enabled = False
                DTPNOVsent.Enabled = False
                DTPNOVReceived.Enabled = False
                DTPNFADateToUC.Enabled = False
                DTPNFADateToPM.Enabled = False
                DTPNFALetterSent.Enabled = False
                btnSaveNOVComments.Enabled = False
                txtNOVComments.ReadOnly = True
                cboNOVEntries.Enabled = False
                btnRemoveNOVComment.Enabled = False
                txtNOVPastComments.ReadOnly = True
                DTPCODateToUC.Enabled = False
                DTPCODateToPM.Enabled = False
                DTPCOProposed.Enabled = False
                DTPCOReceivedfromCompany.Enabled = False
                DTPCOReceivedfromDirector.Enabled = False
                DTPConsentOrderExecuted.Enabled = False
                txtCONumber.ReadOnly = True
                DTPCOResolved.Enabled = False
                txtCOPenaltyAmount.ReadOnly = True
                txtPenaltyComments.ReadOnly = True
                btnSaveCOComments.Enabled = False
                txtCOComments.ReadOnly = True
                cboCOEntries.Enabled = False
                btnDeleteCOComments.Enabled = False
                txtCoPastComments.ReadOnly = True
                txtStipulatedPenalty.ReadOnly = True
                btnSaveStipulatePenalty.Enabled = False
                btnClearStipulated.Enabled = False
                txtStipulatedComments.ReadOnly = True
                dgvStipulatedPenalties.Enabled = False
                DTPAOExecuted.Enabled = False
                DTPAOAppealed.Enabled = False
                DTPAOResolved.Enabled = False
                btnSaveAOComments.Enabled = False
                txtAOComments.ReadOnly = True
                cboAOEntries.Enabled = False
                btnRemoveAOEntries.Enabled = False
                txtAOPastComments.ReadOnly = True

                tsbSave.Enabled = False
                mmiSave.Enabled = False
                mmiSave.Visible = False
                tsbSave.Visible = False
                tsbDelete.Visible = False
                mmiDelete.Visible = False

                If AccountArray(48, 3) = "1" Then
                    DTPEnforcementResolved.Enabled = True
                End If
            Else
                If AccountArray(48, 2) = "1" Or AccountArray(48, 3) = "1" Or AccountArray(48, 4) = "1" Then
                    btnLinkEnforcement.Enabled = True
                    DTPViolationDate.Enabled = True
                    chbLON.Enabled = True
                    chbNOV.Enabled = True
                    chbCO.Enabled = True
                    chbAO.Enabled = True
                    DTPDayZero.Enabled = True
                    btnEditAirProgramPollutants.Enabled = True
                    cboPollutantStatus.Enabled = True
                    cboStaffResponsible.Enabled = True
                    txtGeneralComments.ReadOnly = False
                    chbHPV.Enabled = True
                    cboHPVType.Enabled = True
                    btnSubmitToUC.Enabled = True
                    btnSubmitEnforcementToEPA.Enabled = True
                    btnManuallyEnterAFS.Enabled = True
                    DTPLONDateToUC.Enabled = True
                    DTPLONSent.Enabled = True
                    DTPLonResolved.Enabled = True
                    txtLONComments.ReadOnly = False
                    DTPNOVDateToUC.Enabled = True
                    DTPNOVDateToPM.Enabled = True
                    DTPNOVsent.Enabled = True
                    DTPNOVReceived.Enabled = True
                    DTPNFADateToUC.Enabled = True
                    DTPNFADateToPM.Enabled = True
                    DTPNFALetterSent.Enabled = True
                    btnSaveNOVComments.Enabled = True
                    txtNOVComments.ReadOnly = False
                    cboNOVEntries.Enabled = True
                    btnRemoveNOVComment.Enabled = True
                    txtNOVPastComments.ReadOnly = False
                    DTPCODateToUC.Enabled = True
                    DTPCODateToPM.Enabled = True
                    DTPCOProposed.Enabled = True
                    DTPCOReceivedfromCompany.Enabled = True
                    DTPCOReceivedfromDirector.Enabled = True
                    DTPConsentOrderExecuted.Enabled = True
                    txtCONumber.ReadOnly = False
                    DTPCOResolved.Enabled = True
                    txtCOPenaltyAmount.ReadOnly = False
                    txtPenaltyComments.ReadOnly = False
                    btnSaveCOComments.Enabled = True
                    txtCOComments.ReadOnly = False
                    cboCOEntries.Enabled = True
                    btnDeleteCOComments.Enabled = True
                    txtCoPastComments.ReadOnly = False
                    txtStipulatedPenalty.ReadOnly = False
                    btnSaveStipulatePenalty.Enabled = True
                    btnClearStipulated.Enabled = True
                    txtStipulatedComments.ReadOnly = False
                    dgvStipulatedPenalties.Enabled = True
                    DTPAOExecuted.Enabled = True
                    DTPAOAppealed.Enabled = True
                    DTPAOResolved.Enabled = True
                    btnSaveAOComments.Enabled = True
                    txtAOComments.ReadOnly = False
                    cboAOEntries.Enabled = True
                    btnRemoveAOEntries.Enabled = True
                    txtAOPastComments.ReadOnly = False

                    tsbSave.Visible = True
                    tsbSave.Enabled = True
                    mmiSave.Visible = True
                    mmiSave.Enabled = True
                    tsbDelete.Visible = True
                    tsbDelete.Enabled = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#End Region
#Region "Subs and Functions"
    Sub LoadEnforcement()
        Try


            If txtEnforcementNumber.Text <> "" And txtAIRSNumber.Text = "" Then
                SQL = "Select " & _
                "strAIRSNumber, strTrackingNumber, " & _
                "datModifingDate " & _
                "from " & connNameSpace & ".SSCPEnforcementItems " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strAIRSNumber")) Then
                        txtAIRSNumber.Text = ""
                    Else
                        txtAIRSNumber.Text = Mid(dr.Item("strAIRSnumber"), 5)
                    End If
                    If IsDBNull(dr.Item("strTrackingNumber")) Then
                        txtTrackingNumber.Text = ""
                    Else
                        txtTrackingNumber.Text = dr.Item("strTrackingNumber")
                    End If
                    If IsDBNull(dr.Item("datModifingDate")) Then
                        DTPLastSave.Text = OracleDate
                    Else
                        DTPLastSave.Text = dr.Item("datModifingDate")
                    End If
                End While
                dr.Close()
            End If

            If txtEnforcementNumber.Text <> "" And txtTrackingNumber.Text = "" Then
                SQL = "Select " & _
                "strTrackingNumber " & _
                "from " & connNameSpace & ".SSCPEnforcementItems " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strTrackingNumber")) Then
                        txtTrackingNumber.Text = ""
                    Else
                        txtTrackingNumber.Text = dr.Item("strTrackingNumber")
                    End If
                End While
                dr.Close()
            End If

            If txtAIRSNumber.Text <> "" And txtAIRSNumber.Text.Length = 8 Then
                LoadFacilityInfo()
                If txtEnforcementNumber.Text <> "" Then
                    'TCEnforcement.TabPages.Add(TPPollutants)
                    LoadEnforcementPollutants2()
                Else

                End If
            End If

            If txtEnforcementNumber.Text <> "" Then
                LoadEnforcementInformation()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadFacilityInfo()
        Try
            Dim AIRSNumber As String = ""
            Dim EnforcementNumber As String = ""
            Dim TrackingNumber As String = ""
            Dim EnforcementType As String = ""
            Dim FacName As String = ""
            Dim FacAddress As String = ""
            Dim FacCounty As String = ""
            Dim Classification As String = ""
            Dim AirProgramCode As String = ""
            Dim AirPrograms As String = ""

            If txtAIRSNumber.Text <> "" Then
                AIRSNumber = txtAIRSNumber.Text
            End If
            If txtEnforcementNumber.Text <> "" Then
                EnforcementNumber = txtEnforcementNumber.Text
            End If
            If txtTrackingNumber.Text <> "" Then
                TrackingNumber = txtTrackingNumber.Text
            End If

            SQL = "Select strFacilityName, strFacilityStreet1, " & _
            "strFacilityCity, strCountyName, strFacilityState, strFacilityZipCode, " & _
            "strClass, strAIRProgramCodes " & _
            "from " & connNameSpace & ".APBFacilityInformation, " & connNameSpace & ".LookUpCountyInformation, " & _
            "" & connNameSpace & ".APBHeaderData " & _
            "where " & connNameSpace & ".APBFacilityInformation.strAIRSNumber = '0413" & txtAIRSNumber.Text & "' " & _
            "and strCountyCode = '" & Mid(txtAIRSNumber.Text, 1, 3) & "' " & _
            "and " & connNameSpace & ".APBFacilityInformation.strairsnumber = " & connNameSpace & ".APBHeaderData.strairsnumber"

            cmd = New OracleCommand(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist Then

                If IsDBNull(dr.Item("strFacilityName")) Then
                    FacName = ""
                Else
                    FacName = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    FacAddress = ""
                Else
                    FacAddress = dr.Item("strFacilityStreet1")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    FacAddress = FacAddress
                Else
                    FacAddress = FacAddress & vbCrLf & dr.Item("strFacilityCity") & ", GA "
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    FacAddress = FacAddress
                Else
                    If dr.Item("strFacilityZipCode").ToString.Length > 5 Then
                        FacAddress = FacAddress & Mid(dr.Item("strFacilityZipCode"), 1, 5) & "-" & Mid(dr.Item("strFacilityZipCode"), 6)
                    Else
                        FacAddress = FacAddress & dr.Item("strFacilityZipCode")
                    End If
                End If
                If IsDBNull(dr.Item("strCountyName")) Then
                    FacCounty = ""
                Else
                    FacCounty = dr.Item("strCountyName")
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    Classification = ""
                Else
                    Classification = dr.Item("strClass")
                End If
                If IsDBNull(dr.Item("strAIRProgramCodes")) Then
                    AirProgramCode = ""
                Else
                    AirProgramCode = dr.Item("strAIRProgramCodes")
                End If
            End If
            txtFacilityName.Text = FacName
            txtFacilityAddress.Text = FacAddress
            txtCounty.Text = FacCounty
            txtClassification.Text = Classification

            If Mid(AirProgramCode, 1, 1) = 1 Then
                chbAPC0.Checked = True
            Else
                chbAPC0.Checked = False
            End If
            If Mid(AirProgramCode, 2, 1) = 1 Then
                chbAPC1.Checked = True
            Else
                chbAPC1.Checked = False
            End If
            If Mid(AirProgramCode, 3, 1) = 1 Then
                chbAPC3.Checked = True
            Else
                chbAPC3.Checked = False
            End If
            If Mid(AirProgramCode, 4, 1) = 1 Then
                chbAPC4.Checked = True
            Else
                chbAPC4.Checked = False
            End If
            If Mid(AirProgramCode, 5, 1) = 1 Then
                chbAPC6.Checked = True
            Else
                chbAPC6.Checked = False
            End If
            If Mid(AirProgramCode, 6, 1) = 1 Then
                chbAPC7.Checked = True
            Else
                chbAPC7.Checked = False
            End If
            If Mid(AirProgramCode, 7, 1) = 1 Then
                chbAPC8.Checked = True
            Else
                chbAPC8.Checked = False
            End If
            If Mid(AirProgramCode, 8, 1) = 1 Then
                chbAPC9.Checked = True
            Else
                chbAPC9.Checked = False
            End If
            If Mid(AirProgramCode, 9, 1) = 1 Then
                chbAPCA.Checked = True
            Else
                chbAPCA.Checked = False
            End If
            If Mid(AirProgramCode, 10, 1) = 1 Then
                chbAPCF.Checked = True
            Else
                chbAPCF.Checked = False
            End If
            If Mid(AirProgramCode, 11, 1) = 1 Then
                chbAPCI.Checked = True
            Else
                chbAPCI.Checked = False
            End If
            If Mid(AirProgramCode, 12, 1) = 1 Then
                chbAPCM.Checked = True
            Else
                chbAPCM.Checked = False
            End If
            If Mid(AirProgramCode, 13, 1) = 1 Then
                chbAPCV.Checked = True
            Else
                chbAPCV.Checked = False
            End If



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadEnforcementPollutants2()
        Try
            SQL = "Select " & _
            "strAIRPollutantKey, strPollutantKey, " & _
            "case " & _
            "when substr(strAirPollutantKey, 13,1) = '0' then '0 - SIP' " & _
            "when substr(strAirPollutantKey, 13,1) = '1' then '1 - Fed SIP' " & _
            "when substr(strAirPollutantKey, 13,1) = '3' then '3 - Non-Fed SIP' " & _
            "when substr(strAirPollutantKey, 13,1) = '4' then '4 - CFC' " & _
            "when substr(strAirPollutantKey, 13,1) = '6' then '6 - PSD' " & _
            "when substr(strAirPollutantKey, 13,1) = '7' then '7 - NSR' " & _
            "when substr(strAirPollutantKey, 13,1) = '8' then '8 - NESHAP' " & _
            "when substr(strAirPollutantKey, 13,1) = '9' then '9 - NSPS' " & _
            "when substr(strAirPollutantKey, 13,1) = 'A' then 'A - Acid Rain' " & _
            "when substr(strAirPollutantKey, 13,1) = 'F' then 'F - FESOP' " & _
            "when substr(strAirPollutantKey, 13,1) = 'I' then 'I - Native American' " & _
            "when substr(strAirPollutantKey, 13,1) = 'M' then 'M - MACT' " & _
            "when substr(strAirPollutantKey, 13,1) = 'V' then 'V - Title V' " & _
            "else '' " & _
            "end AirProgram, " & _
            "strPollutantDescription, " & _
            "(strCompliancestatus|| ' - '||strComplianceDesc) as ComplianceStatus " & _
            "from AIRBRANCH.APBAirProgramPollutants, AIRBranch.LookUpPollutants, " & _
            "AIRBranch.LookUpComplianceStatus  " & _
            "where AIRBranch.APBAirProgramPollutants.strPollutantKEy = AIRBranch.LookUpPollutants.strPollutantCode  " & _
            "and AIRBranch.APBAirProgramPollutants.strComplianceStatus = AIRBranch.LookUpComplianceStatus.strComplianceCode " & _
            "and strAIRSNumber = '0413" & txtAIRSNumber.Text & "' " & _
            "order by AirProgram, strPollutantDescription, ComplianceStatus "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da.Fill(ds, "AirPollutants")

            Dim dtAirProgramPollutants As New DataTable
            dtAirProgramPollutants = ds.Tables("AirPollutants")

            Dim drAirProgramPollutants As DataRow()
            Dim row As DataRow
            Dim temp As String = 6
            Dim ColumnArray(1, 13) As String
            Dim i As Integer
            Dim AirPollutantKey As String
            Dim PollutantKey As String
            Dim AirProgram As String
            Dim PollutantDesc As String
            Dim Compliance As String
            Dim Pollutants As String = ""
            Dim AirCode As String = ""
            Dim PollutantCode As String = ""
            Dim PollCheck As String = ""

            lvPollutants.Items.Clear()
            lvPollutants.Columns.Clear()
            lvPollutants.View = View.Details
            lvPollutants.AllowColumnReorder = True
            lvPollutants.CheckBoxes = True
            lvPollutants.GridLines = True
            lvPollutants.FullRowSelect = True

            lvPollutants.Columns.Add("", 25, HorizontalAlignment.Left)
            lvPollutants.Columns.Add("Air Program", 75, HorizontalAlignment.Left)
            lvPollutants.Columns.Add("Pollutant", 100, HorizontalAlignment.Left)
            lvPollutants.Columns.Add("Full Pollutant Desc.", 200, HorizontalAlignment.Left)
            lvPollutants.Columns.Add("Compliance Status", 200, HorizontalAlignment.Left)
            lvPollutants.Columns.Add("strAIRPollutantKey", 0, HorizontalAlignment.Left)

            drAirProgramPollutants = dtAirProgramPollutants.Select()

            For Each row In drAirProgramPollutants
                AirPollutantKey = row("strAIRPollutantKey").ToString()
                PollutantKey = row("strPollutantKey").ToString()
                AirProgram = row("AirProgram").ToString()
                PollutantDesc = row("strPollutantDescription").ToString()
                Compliance = row("ComplianceStatus").ToString()

                ColumnArray(1, 1) = ""
                ColumnArray(1, 2) = AirProgram
                ColumnArray(1, 3) = PollutantKey
                ColumnArray(1, 4) = PollutantDesc
                ColumnArray(1, 5) = Compliance
                ColumnArray(1, 6) = AirPollutantKey

                Dim item1 As New ListViewItem("")
                Dim tempshow As String

                item1.Checked = False

                If temp > 1 Then
                    For i = 2 To temp

                        tempshow = ColumnArray(1, i)

                        item1.SubItems.Add(ColumnArray(1, i))
                    Next
                End If
                lvPollutants.Items.AddRange(New ListViewItem() {item1})
            Next row

            If txtEnforcementNumber.Text <> "" Then
                SQL = "Select " & _
                "strPollutants " & _
                "from " & connNameSpace & ".SSCPEnforcement " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strPollutants")) Then
                        Pollutants = ""
                    Else
                        Pollutants = dr.Item("strPollutants")
                    End If
                End While
                dr.Close()

                Do While Pollutants <> ""
                    temp = Mid(Pollutants, 1, InStr(Pollutants, ",", CompareMethod.Text) - 1)
                    i = 0
                    For i = 0 To lvPollutants.Items.Count - 1
                        PollCheck = "0413" & txtAIRSNumber.Text & Mid(temp, 1, 1)
                        If lvPollutants.Items.Item(i).SubItems(5).Text = PollCheck And _
                                   lvPollutants.Items.Item(i).SubItems(2).Text = Mid(temp, 2) Then
                            lvPollutants.Items.Item(i).Checked = True
                        End If
                    Next
                    Pollutants = Replace(Pollutants, (temp & ","), "")
                Loop
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadEnforcementInformation()
        Try
            Dim ActionType As String = ""
            Dim GeneralComments As String = ""
            Dim DiscoveryDate As String = ""
            Dim LONSent As String = ""
            Dim LONToUC As String = ""
            Dim LONResolved As String = ""
            Dim LONComments As String = ""
            Dim DayZero As String = ""
            Dim HPV As String = ""
            Dim NOVSent As String = ""
            Dim NOVToUC As String = ""
            Dim NOVTOPM As String = ""
            Dim NOVComments As String = ""
            Dim NOVResponseReceived As String = ""
            Dim NOVResolved As String = ""
            Dim NFASent As String = ""
            Dim NFAToUC As String = ""
            Dim NFAToPM As String = ""
            Dim COProposed As String = ""
            Dim COToUC As String = ""
            Dim COToPM As String = ""
            Dim COComments As String = ""
            Dim COExecuted As String = ""
            Dim COReceivedFromCO As String = ""
            Dim COReceivedFromDO As String = ""
            Dim CONumber As String = ""
            Dim COResolved As String = ""
            Dim COPenaltyAmount As String = ""
            Dim COPenaltyComments As String = ""
            Dim Stipulated As String = ""
            Dim AOComments As String = ""
            Dim AOExecuted As String = ""
            Dim AOAppealed As String = ""
            Dim AOResolved As String = ""
            Dim count As Integer
            Dim Status As String = ""
            Dim AFSKeyActionNumber As String = ""
            Dim PollutantStatus As String = ""

            SQL = "select * " & _
            "from " & connNameSpace & ".SSCPEnforcement " & _
            "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("strActionType")) Then
                    ActionType = ""
                Else
                    ActionType = dr.Item("strActionType")
                End If
                If IsDBNull(dr.Item("strGeneralComments")) Then
                    GeneralComments = ""
                Else
                    GeneralComments = dr.Item("strGeneralComments")
                End If
                If IsDBNull(dr.Item("datDiscoveryDate")) Then
                    DiscoveryDate = ""
                Else
                    DiscoveryDate = dr.Item("datDiscoveryDate")
                End If
                If IsDBNull(dr.Item("datLONSent")) Then
                    LONSent = ""
                Else
                    LONSent = dr.Item("datLONSent")
                End If
                If IsDBNull(dr.Item("datLONToUC")) Then
                    LONToUC = ""
                Else
                    LONToUC = dr.Item("datLONToUC")
                End If
                If IsDBNull(dr.Item("strLONComments")) Then
                    LONComments = ""
                Else
                    LONComments = dr.Item("strLONComments")
                End If
                If IsDBNull(dr.Item("datLONResolved")) Then
                    LONResolved = ""
                Else
                    LONResolved = dr.Item("datLONResolved")
                End If

                If IsDBNull(dr.Item("datDayZero")) Then
                    DayZero = ""
                Else
                    DayZero = dr.Item("datDayZero")
                End If
                If IsDBNull(dr.Item("strHPV")) Then
                    HPV = ""
                Else
                    HPV = dr.Item("strHPV")
                End If
                If IsDBNull(dr.Item("datNOVSent")) Then
                    NOVSent = ""
                Else
                    NOVSent = dr.Item("datNOVSent")
                End If
                If IsDBNull(dr.Item("datNOVToUC")) Then
                    NOVToUC = ""
                Else
                    NOVToUC = dr.Item("datNOVToUC")
                End If
                If IsDBNull(dr.Item("datNOVToPM")) Then
                    NOVTOPM = ""
                Else
                    NOVTOPM = dr.Item("datNOVToPM")
                End If
                If IsDBNull(dr.Item("strNOVCommentsEntry")) Then
                    NOVComments = ""
                Else
                    NOVComments = dr.Item("strNOVCommentsEntry")
                End If
                If IsDBNull(dr.Item("datNOVREsponseReceived")) Then
                    NOVResponseReceived = ""
                Else
                    NOVResponseReceived = dr.Item("datNOVResponseReceived")
                End If
                If IsDBNull(dr.Item("strNOVResolvedEnforcement")) Then
                    NOVResolved = ""
                Else
                    NOVResolved = dr.Item("strNOVResolvedEnforcement")
                End If
                If IsDBNull(dr.Item("datNFALetterSent")) Then
                    NFASent = ""
                Else
                    NFASent = dr.Item("datNFALEtterSent")
                End If
                If IsDBNull(dr.Item("datNFAToUC")) Then
                    NFAToUC = ""
                Else
                    NFAToUC = dr.Item("datNFAToUC")
                End If
                If IsDBNull(dr.Item("datNFAToPM")) Then
                    NFAToPM = ""
                Else
                    NFAToPM = dr.Item("datNFAToPM")
                End If
                If IsDBNull(dr.Item("datCOProposed")) Then
                    COProposed = ""
                Else
                    COProposed = dr.Item("datCOProposed")
                End If
                If IsDBNull(dr.Item("datCOToUC")) Then
                    COToUC = ""
                Else
                    COToUC = dr.Item("datCOToUC")
                End If
                If IsDBNull(dr.Item("datCOToPM")) Then
                    COToPM = ""
                Else
                    COToPM = dr.Item("datCOToPM")
                End If
                If IsDBNull(dr.Item("strCOCommentsEntry")) Then
                    COComments = ""
                Else
                    COComments = dr.Item("strCOCommentsEntry")
                End If
                If IsDBNull(dr.Item("datCOExecuted")) Then
                    COExecuted = ""
                Else
                    COExecuted = dr.Item("datCOExecuted")
                End If
                If IsDBNull(dr.Item("datCOReceivedFromCompany")) Then
                    COReceivedFromCO = ""
                Else
                    COReceivedFromCO = dr.Item("datCOReceivedFromCompany")
                End If
                If IsDBNull(dr.Item("datCOReceivedFromDirector")) Then
                    COReceivedFromDO = ""
                Else
                    COReceivedFromDO = dr.Item("datCOReceivedFromDirector")
                End If
                If IsDBNull(dr.Item("datCOResolved")) Then
                    COResolved = ""
                Else
                    COResolved = dr.Item("datCOResolved")
                End If
                If IsDBNull(dr.Item("strCONumber")) Then
                    CONumber = ""
                Else
                    CONumber = dr.Item("strCONumber")
                End If
                If IsDBNull(dr.Item("strCOPenaltyAmount")) Then
                    COPenaltyAmount = ""
                Else
                    COPenaltyAmount = dr.Item("strCOPenaltyAmount")
                End If
                If IsDBNull(dr.Item("strCOPenaltyAmountComments")) Then
                    COPenaltyComments = ""
                Else
                    COPenaltyComments = dr.Item("strCOPenaltyAmountComments")
                End If

                If IsDBNull(dr.Item("strStipulatedPenalty")) Then
                    Stipulated = ""
                Else
                    Stipulated = dr.Item("strStipulatedPenalty")
                End If
                If IsDBNull(dr.Item("strAOCommentsEntry")) Then
                    AOComments = ""
                Else
                    AOComments = dr.Item("strAOCommentsEntry")
                End If
                If IsDBNull(dr.Item("datAOExecuted")) Then
                    AOExecuted = ""
                Else
                    AOExecuted = dr.Item("datAOExecuted")
                End If
                If IsDBNull(dr.Item("datAOAppealed")) Then
                    AOAppealed = ""
                Else
                    AOAppealed = dr.Item("datAOAppealed")
                End If
                If IsDBNull(dr.Item("datAOResolved")) Then
                    AOResolved = ""
                Else
                    AOResolved = dr.Item("datAOResolved")
                End If
                If IsDBNull(dr.Item("strAFSKeyActionNumber")) Then
                    AFSKeyActionNumber = ""
                Else
                    AFSKeyActionNumber = dr.Item("strAFSKeyActionNumber")
                End If
                If IsDBNull(dr.Item("strPollutantStatus")) Then
                    PollutantStatus = ""
                Else
                    PollutantStatus = dr.Item("strPollutantStatus")
                End If
            End If
            dr.Close()

            If LONSent <> "" Or LONToUC <> "" Then
                chbLON.Checked = True
            Else
                chbLON.Checked = False
            End If
            If NOVSent <> "" Or NOVToUC <> "" Or NOVTOPM <> "" _
                Or NOVResponseReceived <> "" Or NOVResolved <> "" Or NOVComments <> "" _
                Or NFASent <> "" Or NFAToUC <> "" Or NFAToPM <> "" Then
                chbNOV.Checked = True
            Else
                chbNOV.Checked = False
            End If
            If HPV <> "" Then
                chbHPV.Checked = True
            Else
                chbHPV.Checked = False
            End If
            If COExecuted <> "" Or COToUC <> "" Or COToPM <> "" _
                Or COProposed <> "" Or COReceivedFromCO <> "" Or COReceivedFromDO <> "" _
                    Or COResolved <> "" Then
                chbCO.Checked = True
            Else
                chbCO.Checked = False
            End If
            If AOExecuted <> "" Or AOAppealed <> "" Or AOResolved <> "" Then
                chbAO.Checked = True
            Else
                chbAO.Checked = False
            End If

            Select Case ActionType
                Case "LON"
                    chbLON.Checked = True
                    chbNOV.Checked = False
                    chbCO.Checked = False
                    chbAO.Checked = False
                    chbHPV.Checked = False
                Case "NOV"
                    chbNOV.Checked = True
                    chbCO.Checked = False
                    chbAO.Checked = False
                    chbHPV.Checked = False
                Case "NOVCOP"
                    chbNOV.Checked = True
                    chbCO.Checked = True
                    chbAO.Checked = False
                    chbHPV.Checked = False
                Case "NOVCO"
                    chbNOV.Checked = True
                    chbCO.Checked = True
                    chbAO.Checked = False
                    chbHPV.Checked = False
                Case "NOVAO"
                    chbNOV.Checked = True
                    chbAO.Checked = True
                    chbHPV.Checked = False
                Case "HPV"
                    chbHPV.Checked = True
                    chbCO.Checked = False
                    chbAO.Checked = False
                Case "HPVCOP"
                    chbHPV.Checked = True
                    chbCO.Checked = True
                    chbAO.Checked = False
                Case "HPVCO"
                    chbHPV.Checked = True
                    chbCO.Checked = True
                    chbAO.Checked = False
                Case "HPVAO"
                    chbHPV.Checked = True
                    chbAO.Checked = True
            End Select

            If PollutantStatus = "" Then
                cboPollutantStatus.SelectedValue = "0"
            Else
                cboPollutantStatus.SelectedValue = PollutantStatus
            End If
            txtGeneralComments.Text = GeneralComments
            If DiscoveryDate <> "" Then
                DTPViolationDate.Text = DiscoveryDate
                DTPViolationDate.Checked = True
            Else
                DTPViolationDate.Text = OracleDate
                DTPViolationDate.Checked = False
            End If
            If LONSent <> "" Then
                DTPLONSent.Text = LONSent
                DTPLONSent.Checked = True
            Else
                DTPLONSent.Text = OracleDate
                DTPLONSent.Checked = False
            End If
            If LONResolved <> "" Then
                DTPLonResolved.Text = LONResolved
                DTPLonResolved.Checked = True
            Else
                DTPLonResolved.Text = OracleDate
                DTPLonResolved.Checked = False
            End If
            If LONToUC <> "" Then
                DTPLONDateToUC.Text = LONToUC
                DTPLONDateToUC.Checked = True
            Else
                DTPLONDateToUC.Text = OracleDate
                DTPLONDateToUC.Checked = False
            End If
            If DayZero <> "" Then
                DTPDayZero.Text = DayZero
                DTPDayZero.Checked = True
            Else
                DTPDayZero.Text = OracleDate
                DTPDayZero.Checked = False
            End If
            If HPV <> "" Then
                chbHPV.Checked = True
                cboHPVType.SelectedValue = HPV
            Else
                chbHPV.Checked = False
                cboHPVType.Text = ""
            End If
            If NOVSent <> "" Then
                DTPNOVsent.Text = NOVSent
                DTPNOVsent.Checked = True
            Else
                DTPNOVsent.Text = OracleDate
                DTPNOVsent.Checked = False
            End If
            If NOVToUC <> "" Then
                DTPNOVDateToUC.Text = NOVToUC
                DTPNOVDateToUC.Checked = True
            Else
                DTPNOVDateToUC.Text = OracleDate
                DTPNOVDateToUC.Checked = False
            End If
            If NOVTOPM <> "" Then
                DTPNOVDateToPM.Text = NOVTOPM
                DTPNOVDateToPM.Checked = True
            Else
                DTPNOVDateToPM.Text = OracleDate
                DTPNOVDateToPM.Checked = False
            End If
            If NOVResponseReceived <> "" Then
                DTPNOVReceived.Text = NOVResponseReceived
                DTPNOVReceived.Checked = True
            Else
                DTPNOVReceived.Text = OracleDate
                DTPNOVReceived.Checked = False
            End If
            If NFASent <> "" Then
                DTPNFALetterSent.Text = NFASent
                DTPNFALetterSent.Checked = True
            Else
                DTPNFALetterSent.Text = OracleDate
                DTPNFALetterSent.Checked = False
            End If
            If NFAToUC <> "" Then
                DTPNFADateToUC.Text = NFAToUC
                DTPNFADateToUC.Checked = True
            Else
                DTPNFADateToUC.Text = OracleDate
                DTPNFADateToUC.Checked = False
            End If
            If NFAToPM <> "" Then
                DTPNFADateToPM.Text = NFAToPM
                DTPNFADateToPM.Checked = True
            Else
                DTPNFADateToPM.Text = OracleDate
                DTPNFADateToPM.Checked = False
            End If
            If COProposed <> "" Then
                DTPCOProposed.Text = COProposed
                DTPCOProposed.Checked = True
            Else
                DTPCOProposed.Text = OracleDate
                DTPCOProposed.Checked = False
            End If
            If COToUC <> "" Then
                DTPCODateToUC.Text = COToUC
                DTPCODateToUC.Checked = True
            Else
                DTPCODateToUC.Text = OracleDate
                DTPCODateToUC.Checked = False
            End If
            If COToPM <> "" Then
                DTPCODateToPM.Text = COToPM
                DTPCODateToPM.Checked = True
            Else
                DTPCODateToPM.Text = OracleDate
                DTPCODateToPM.Checked = False
            End If
            If COExecuted <> "" Then
                DTPConsentOrderExecuted.Text = COExecuted
                DTPConsentOrderExecuted.Checked = True
            Else
                DTPConsentOrderExecuted.Text = OracleDate
                DTPConsentOrderExecuted.Checked = False
            End If
            If COReceivedFromCO <> "" Then
                DTPCOReceivedfromCompany.Text = COReceivedFromCO
                DTPCOReceivedfromCompany.Checked = True
            Else
                DTPCOReceivedfromCompany.Text = OracleDate
                DTPCOReceivedfromCompany.Checked = False
            End If
            If COReceivedFromDO <> "" Then
                DTPCOReceivedfromDirector.Text = COReceivedFromDO
                DTPCOReceivedfromDirector.Checked = True
            Else
                DTPCOReceivedfromDirector.Text = OracleDate
                DTPCOReceivedfromDirector.Checked = False
            End If
            If CONumber <> "" Then
                txtCONumber.Text = CONumber
            Else
                txtCONumber.Clear()
            End If
            If COResolved <> "" Then
                DTPCOResolved.Text = COResolved
                DTPCOResolved.Checked = True
            Else
                DTPCOResolved.Text = OracleDate
                DTPCOResolved.Checked = False
            End If
            If COPenaltyAmount <> "" Then
                txtCOPenaltyAmount.Text = COPenaltyAmount
            Else
                txtCOPenaltyAmount.Clear()
            End If
            If COPenaltyComments <> "" Then
                txtPenaltyComments.Text = COPenaltyComments
            Else
                txtPenaltyComments.Clear()
            End If
            If AOExecuted <> "" Then
                DTPAOExecuted.Text = AOExecuted
                DTPAOExecuted.Checked = True
            Else
                DTPAOExecuted.Text = OracleDate
                DTPAOExecuted.Checked = False
            End If
            If AOAppealed <> "" Then
                DTPAOAppealed.Text = AOAppealed
                DTPAOAppealed.Checked = True
            Else
                DTPAOAppealed.Text = OracleDate
                DTPAOAppealed.Checked = False
            End If
            If AOResolved <> "" Then
                DTPAOResolved.Text = AOResolved
                DTPAOResolved.Checked = True
            Else
                DTPAOResolved.Text = OracleDate
                DTPAOResolved.Checked = False
            End If

            If LONComments <> "" Then
                txtLONComments.Text = LONComments
            Else
                txtLONComments.Clear()
            End If
            If NOVComments <> "" Then
                count = 0
                SQL = "Select * " & _
                "from " & connNameSpace & ".SSCPENforcementNOVComments " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                "order by strNOVEntryNumber "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    count += 1
                    If IsDBNull(dr.Item("strNOVComment")) Then
                        txtNOVPastComments.Clear()
                    Else
                        txtNOVPastComments.Text = dr.Item("strNOVComment")
                    End If
                    If IsDBNull(dr.Item("strNOVEntryNumber")) Then
                        cboNOVEntries.Items.Add("")
                    Else
                        cboNOVEntries.Items.Add(dr.Item("strNOVEntryNumber"))
                    End If
                End While
                dr.Close()
                txtMaxNOVComment.Text = count.ToString
                cboNOVEntries.Text = cboNOVEntries.Items.Item((count - 1))
            Else
                txtNOVComments.Clear()
                cboNOVEntries.Items.Clear()
                txtNOVPastComments.Clear()
                txtMaxNOVComment.Clear()
            End If
            If COComments <> "" Then
                count = 0
                SQL = "Select * " & _
                "from " & connNameSpace & ".SSCPEnforcementCOComments " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
               "order by strCOEntryNumber "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    count += 1
                    If IsDBNull(dr.Item("strCOEntryNumber")) Then
                        cboCOEntries.Items.Add("")
                    Else
                        cboCOEntries.Items.Add(dr.Item("strCOEntryNumber"))
                    End If
                    If IsDBNull(dr.Item("strCOComment")) Then
                        txtCoPastComments.Clear()
                    Else
                        txtCoPastComments.Text = dr.Item("strCOComment")
                    End If
                End While
                dr.Close()
                txtMaxCOComment.Text = count.ToString
                cboCOEntries.Text = cboCOEntries.Items.Item((count - 1))
            Else
                txtCOComments.Clear()
                cboCOEntries.Items.Clear()
                txtMaxCOComment.Clear()
                txtCoPastComments.Clear()
            End If

            If Stipulated <> "" And ActionType <> "LON" And ActionType <> "NOV" And chbCO.Checked = True Then
                SQL = "Select " & _
                "strStipulatedPenalty, " & _
                "Case " & _
                "    When strStipulatedPenaltyComments IS Null THen 'N/A' " & _
                "Else strStipulatedPenaltyComments  " & _
                "End StipulatedPenaltyComments, " & _
                "strAFSStipulatedPenaltyNumber, " & _
                "strEnforcementKey " & _
                "from " & connNameSpace & ".SSCPENforcementStipulated " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                "order by strEnforcementKey "

                dsStipulatedPenalty = New DataSet
                daStipulatedPenalty = New OracleDataAdapter(SQL, conn)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                daStipulatedPenalty.Fill(dsStipulatedPenalty, "StipulatedPenalty")
                dgvStipulatedPenalties.DataSource = dsStipulatedPenalty
                dgvStipulatedPenalties.DataMember = "StipulatedPenalty"

                If conn.State = ConnectionState.Open Then
                    'conn.close()
                End If

                dgvStipulatedPenalties.RowHeadersVisible = False
                dgvStipulatedPenalties.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvStipulatedPenalties.AllowUserToResizeColumns = True
                dgvStipulatedPenalties.AllowUserToAddRows = False
                dgvStipulatedPenalties.AllowUserToDeleteRows = False
                dgvStipulatedPenalties.AllowUserToOrderColumns = True
                dgvStipulatedPenalties.AllowUserToResizeRows = True
                dgvStipulatedPenalties.Columns("strStipulatedPenalty").HeaderText = "Penalty Amount"
                dgvStipulatedPenalties.Columns("strStipulatedPenalty").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dgvStipulatedPenalties.Columns("strStipulatedPenalty").DisplayIndex = 0
                dgvStipulatedPenalties.Columns("StipulatedPenaltyComments").HeaderText = "Comments"
                dgvStipulatedPenalties.Columns("StipulatedPenaltyComments").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dgvStipulatedPenalties.Columns("StipulatedPenaltyComments").DisplayIndex = 1
                dgvStipulatedPenalties.Columns("strAFSStipulatedPenaltyNumber").HeaderText = "AFS Action Number"
                dgvStipulatedPenalties.Columns("strAFSStipulatedPenaltyNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dgvStipulatedPenalties.Columns("strAFSStipulatedPenaltyNumber").DisplayIndex = 2
                dgvStipulatedPenalties.Columns("strEnforcementKey").HeaderText = "Penalty #"
                dgvStipulatedPenalties.Columns("strEnforcementKey").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dgvStipulatedPenalties.Columns("strEnforcementKey").DisplayIndex = 3
                dgvStipulatedPenalties.Columns("strEnforcementKey").Visible = False
            Else
                dsStipulatedPenalty = New DataSet
            End If
            If AOComments <> "" Then
                count = 0
                SQL = "Select * " & _
                "from " & connNameSpace & ".SSCPEnforcementAOComments " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                "order by strAOEntryNumber "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    count += 1
                    If IsDBNull(dr.Item("strAOEntryNumber")) Then
                        cboAOEntries.Items.Add("")
                    Else
                        cboAOEntries.Items.Add(dr.Item("strAOEntryNumber"))
                    End If
                    If IsDBNull(dr.Item("strAOComment")) Then
                        txtAOPastComments.Clear()
                    Else
                        txtAOPastComments.Text = dr.Item("strAOComment")
                    End If
                End While
                dr.Close()
                txtMaxAOComment.Text = count.ToString
                cboAOEntries.Text = cboAOEntries.Items.Item((count - 1))
            Else
                txtAOComments.Clear()
                cboAOEntries.Items.Clear()
                txtMaxAOComment.Clear()
                txtAOPastComments.Clear()
            End If

            If txtEnforcementNumber.Text = "N/A" Then
                SQL = "Select strStaffResponsible, " & _
                "datEnforcementFinalized, strStatus " & _
                "from " & connNameSpace & ".SSCPEnforcementItems " & _
                "where strEnforcementNumber = '' "
            Else
                SQL = "Select strStaffResponsible, " & _
                "datEnforcementFinalized, strStatus " & _
                "from " & connNameSpace & ".SSCPEnforcementItems " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
            End If

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strStaffResponsible")) Then
                    cboStaffResponsible.Text = ""
                Else
                    cboStaffResponsible.SelectedValue = dr.Item("strStaffResponsible")
                End If
                If IsDBNull(dr.Item("datEnforcementFinalized")) Then
                    DTPEnforcementResolved.Text = OracleDate
                    DTPEnforcementResolved.Checked = False
                Else
                    DTPEnforcementResolved.Text = dr.Item("datEnforcementFinalized")
                    DTPEnforcementResolved.Checked = True
                    'DTPEnforcementResolved.Text = OracleDate
                    'DTPEnforcementResolved.Checked = False
                End If
                If IsDBNull(dr.Item("strStatus")) Then
                    Status = ""
                Else
                    Status = dr.Item("strStatus")
                End If
            End While
            dr.Close()

            If AccountArray(48, 2) = "1" Then
                Select Case Status
                    Case ""
                        btnSubmitToUC.Visible = True
                    Case "UC"
                        btnSubmitToUC.Visible = False
                    Case Else
                        btnSubmitToUC.Visible = True
                End Select
            End If

            If AFSKeyActionNumber <> "" Then
                If btnSubmitEnforcementToEPA.Visible = False Then
                    btnSubmitEnforcementToEPA.Visible = True
                End If
                LoadAFSNumbers()
            End If

        Catch ex As Exception
            ErrorReport(txtEnforcementNumber.Text & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadAFSNumbers()
        Try
            Dim StipulatedCount As String = ""

            SQL = "Select strAFSKeyActionNumber, " & _
            "strAFSNOVSentNumber, strAFSNOVResolvedNumber, " & _
            "strAFSCOProposedNumber, strAFSCOExecutedNumber, " & _
            "strAFSCOResolvedNumber, strAFSAOToAGNumber, " & _
            "strAFSCivilCourtNumber, strAFSAOResolvedNumber, " & _
            "strStipulatedPenalty " & _
            "from " & connNameSpace & ".SSCPEnforcement " & _
            "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAFSKeyActionNumber")) Then
                    txtKeyActionNumber.Text = ""
                Else
                    txtKeyActionNumber.Text = dr.Item("strAFSKeyActionNumber")
                    Me.btnSubmitToUC.Visible = False
                End If
                If IsDBNull(dr.Item("strAFSNOVSentNumber")) Then
                    txtNOVActionNumber.Text = ""
                Else
                    txtNOVActionNumber.Text = dr.Item("strAFSNOVSentNumber")
                End If
                If IsDBNull(dr.Item("strAFSNOVResolvedNumber")) Then
                    txtNFAActionNumber.Text = ""
                Else
                    txtNFAActionNumber.Text = dr.Item("strAFSNOVResolvedNumber")
                End If
                If IsDBNull(dr.Item("strAFSCOProposedNumber")) Then
                    txtCOProposedActionNumber.Text = ""
                Else
                    txtCOProposedActionNumber.Text = dr.Item("strAFSCOProposedNumber")
                End If
                If IsDBNull(dr.Item("strAFSCOExecutedNumber")) Then
                    txtCOExecutedActionNumber.Text = ""
                Else
                    txtCOExecutedActionNumber.Text = dr.Item("strAFSCOExecutedNumber")
                End If
                If IsDBNull(dr.Item("strAFSCOResolvedNumber")) Then
                    txtCOResolvedActionNumber.Text = ""
                Else
                    txtCOResolvedActionNumber.Text = dr.Item("strAFSCOResolvedNumber")
                End If
                If IsDBNull(dr.Item("strAFSAOtoAGNumber")) Then
                    txtAOToAGActionNumber.Text = ""
                Else
                    txtAOToAGActionNumber.Text = dr.Item("strAFSAOtoAGNumber")
                End If
                If IsDBNull(dr.Item("strAFSCivilCourtNumber")) Then
                    txtCivilCourtActionNumber.Text = ""
                Else
                    txtCivilCourtActionNumber.Text = dr.Item("strAFSCivilCourtNumber")
                End If
                If IsDBNull(dr.Item("strAFSAOResolvedNumber")) Then
                    txtAOResolvedActionNumber.Text = ""
                Else
                    txtAOResolvedActionNumber.Text = dr.Item("strAFSAOResolvedNumber")
                End If
                If IsDBNull(dr.Item("strStipulatedPenalty")) Then
                    StipulatedCount = ""
                Else
                    StipulatedCount = dr.Item("strStipulatedPenalty")
                End If
            End While
            dr.Close()

            If StipulatedCount <> "" Then
                SQL = "Select strAFSStipulatedPenaltyNumber " & _
                "from " & connNameSpace & ".SSCPENforcementStipulated " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                "order by strEnforcementKey "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strAFSStipulatedPenaltyNumber")) Then
                        txtStipulatedPenalitiesActionNumber.Text = txtStipulatedPenalitiesActionNumber.Text
                    Else
                        If txtStipulatedPenalitiesActionNumber.Text.Contains(dr.Item("strAFSStipulatedPenaltyNumber")) Then
                        Else
                            txtStipulatedPenalitiesActionNumber.Text = txtStipulatedPenalitiesActionNumber.Text & dr.Item("strAFSStipulatedPenaltyNumber") & ", "
                        End If
                    End If
                End While
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub OpenChecklist()
        Try

            EnforcementChecklist = Nothing
            If EnforcementChecklist Is Nothing Then EnforcementChecklist = New SSCPEnforcementChecklist
            If txtAIRSNumber.Text <> "" Then
                EnforcementChecklist.txtAIRSNumber.Text = txtAIRSNumber.Text
            End If
            If txtEnforcementNumber.Text <> "" Then
                EnforcementChecklist.txtEnforcementNumber.Text = txtEnforcementNumber.Text
            End If
            If txtTrackingNumber.Text <> "" Then
                EnforcementChecklist.txtTrackingNumber.Text = txtTrackingNumber.Text
            End If

            EnforcementChecklist.Show()
            EnforcementChecklist.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub SaveEnforcement()
        Try
            Dim EnforcementDate As String = ""
            Dim StaffResponsible As String = ""
            Dim EnforcementStatus As String = ""
            Dim ActionType As String = ""
            Dim DiscoveryDate As String = ""
            Dim DayZero As String = ""
            Dim HPVType As String = ""
            Dim PollutantStatus As String = ""
            Dim Pollutants As String = ""
            Dim AirProgram As String = ""
            Dim Pollutant As String = ""
            Dim LONToUC As String = ""
            Dim LonSent As String = ""
            Dim LONResolved As String = ""
            Dim NOVToUC As String = ""
            Dim NOVToPM As String = ""
            Dim NOVSent As String = ""
            Dim NOVResponseRec As String = ""
            Dim NFAToUC As String = ""
            Dim NFAToPM As String = ""
            Dim NFASent As String = ""
            Dim COToUC As String = ""
            Dim COToPM As String = ""
            Dim COProposed As String = ""
            Dim COReceivedComp As String = ""
            Dim COReceivedDO As String = ""
            Dim COExecuted As String = ""
            Dim COResolved As String = ""
            Dim AOExecuted As String = ""
            Dim AOAppealed As String = ""
            Dim AOResolved As String = ""

            If AccountArray(48, 2) = "0" And AccountArray(48, 3) = "0" And AccountArray(48, 4) = "0" Then
                MsgBox("You do not have sufficent permission to save Compliance Events.", MsgBoxStyle.Information, "Compliance Events")
            Else
                If DTPEnforcementResolved.Checked = True Then
                    EnforcementDate = DTPEnforcementResolved.Text
                Else
                    EnforcementDate = ""
                End If
                StaffResponsible = cboStaffResponsible.SelectedValue
                If StaffResponsible = "" Then
                    StaffResponsible = UserGCode
                End If

                If txtEnforcementNumber.Text = "" Or txtEnforcementNumber.Text = "N/A" Then
                    SQL = "Insert into " & connNameSpace & ".SSCPEnforcementItems " & _
                   "(strEnforcementNumber, strTrackingNumber, " & _
                   "strAIRSNumber, datEnforcementFinalized, " & _
                   "strStaffResponsible, strModifingPerson, " & _
                   "datModifingDate, strStatus) " & _
                   "values " & _
                   "(" & connNameSpace & ".SSCPEnforcementNumber.nextval,  '" & txtTrackingNumber.Text & "', " & _
                   "'0413" & txtAIRSNumber.Text & "', '" & EnforcementDate & "', " & _
                   "'" & StaffResponsible & "', '" & UserGCode & "', " & _
                   "'" & OracleDate & "', '') "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    Try

                        dr = cmd.ExecuteReader
                        dr.Close()
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    End Try

                    SQL = "Select " & connNameSpace & ".SSCPEnforcementnumber.currval from dual "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        txtEnforcementNumber.Text = dr.Item(0)
                    End While

                    dr.Close()

                    SQL = "Insert into " & connNameSpace & ".SSCPEnforcement " & _
                    "(strEnforcementNumber, strModifingPerson, " & _
                    "datModifingDate) " & _
                    "values " & _
                    "('" & txtEnforcementNumber.Text & "', '" & UserGCode & "', " & _
                    "'" & OracleDate & "') "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    If conn.State = ConnectionState.Open Then
                        'conn.close()
                    End If

                    LoadFacilityInfo()
                Else
                    SQL = "Update " & connNameSpace & ".SSCPEnforcementItems set " & _
                    "strTrackingNumber = '" & txtTrackingNumber.Text & "', " & _
                    "datEnforcementFinalized = '" & EnforcementDate & "', " & _
                    "strStaffResponsible = '" & StaffResponsible & "', " & _
                    "strModifingPerson = '" & UserGCode & "', " & _
                    "datModifingDate = '" & OracleDate & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                ActionType = ""
                'If chbLON.Checked = True Then
                '    ActionType = "LON"
                'End If
                'If chbNOV.Checked = True Then
                '    ActionType = "NOV"
                'End If
                'If chbHPV.Checked = True Then
                '    ActionType = "HPV"
                'End If
                'If chbCO.Checked = True And chbNOV.Checked = False Then
                '    ActionType = "HPVCO"
                'End If
                'If chbAO.Checked = True And chbCO.Checked = False And chbHPV.Checked = False And chbLON.Checked = False Then
                '    ActionType = "NOV"
                'End If

                If chbLON.Checked = True And chbNOV.Checked = False And chbCO.Checked = False And _
                           chbAO.Checked = False And chbHPV.Checked = False Then
                    ActionType = "LON"
                End If
                If chbNOV.Checked = True And chbCO.Checked = False And chbAO.Checked = False And chbHPV.Checked = False Then
                    ActionType = "NOV"
                End If
                If chbCO.Checked = True And chbAO.Checked = False And chbHPV.Checked = False _
                       And DTPConsentOrderExecuted.Checked = False Then
                    ActionType = "NOVCOP"
                End If
                If chbCO.Checked = True And chbAO.Checked = False And chbHPV.Checked = False _
                       And DTPConsentOrderExecuted.Checked = True Then
                    ActionType = "NOVCO"
                End If
                If chbAO.Checked = True And chbHPV.Checked = False Then
                    ActionType = "NOVAO"
                End If

                If chbHPV.Checked = True And chbCO.Checked = True And chbAO.Checked = False _
                      And DTPConsentOrderExecuted.Checked = False Then
                    ActionType = "HPVCOP"
                End If
                If chbHPV.Checked = True And chbCO.Checked = True And chbAO.Checked = False _
                        And DTPConsentOrderExecuted.Checked = True Then
                    ActionType = "HPVCO"
                End If
                If chbHPV.Checked = True And chbAO.Checked = True Then
                    ActionType = "HPVAO"
                End If
                If chbHPV.Checked = True And chbCO.Checked = False And chbAO.Checked = False Then
                    ActionType = "HPV"
                End If

                If DTPViolationDate.Checked = True Then
                    DiscoveryDate = DTPViolationDate.Text
                Else
                    DiscoveryDate = ""
                End If
                If DTPDayZero.Checked = True Then
                    DayZero = DTPDayZero.Text
                Else
                    DayZero = ""
                End If
                If chbHPV.Checked = True Then
                    HPVType = cboHPVType.SelectedValue
                Else
                    HPVType = ""
                End If

                Dim i As Integer
                For i = 0 To lvPollutants.Items.Count - 1
                    If lvPollutants.Items.Item(i).Checked = True Then
                        Pollutants = Pollutants & Mid(lvPollutants.Items.Item(i).SubItems(1).Text, 1, 1) & lvPollutants.Items.Item(i).SubItems(2).Text & ","
                    End If
                Next

                PollutantStatus = cboPollutantStatus.SelectedValue

                SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                "strActionType = '" & ActionType & "', " & _
                "strGeneralComments = '" & Replace(txtGeneralComments.Text, "'", "''") & "', " & _
                "datDiscoverydate = '" & DiscoveryDate & "', " & _
                "datDayZero = '" & DayZero & "', " & _
                "strHPV = '" & HPVType & "', " & _
                "strPollutants = '" & Pollutants & "', " & _
                "strPollutantStatus = '" & PollutantStatus & "', " & _
                "strModifingPerson = '" & UserGCode & "', " & _
                "datModifingDate = '" & OracleDate & "' " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                If cboPollutantStatus.SelectedValue = "" Then
                    cboPollutantStatus.SelectedValue = "0"
                End If
                'Update Pollutant Status in Header Tables 
                i = 0
                For i = 0 To lvPollutants.Items.Count - 1
                    If lvPollutants.Items.Item(i).Checked = True Then

                        SQL = "Update " & connNameSpace & ".APBAirProgramPollutants set " & _
                        "strComplianceStatus = '" & cboPollutantStatus.SelectedValue & "', " & _
                        "strModifingPerson = '" & UserGCode & "', " & _
                        "datModifingDate = '" & OracleDate & "' " & _
                        "where strAirPollutantKey = '" & lvPollutants.Items.Item(i).SubItems(5).Text & "' " & _
                        "and strPollutantkey = '" & lvPollutants.Items.Item(i).SubItems(2).Text & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If
                Next

                If chbLON.Checked = True Then
                    If DTPLONDateToUC.Checked = True Then
                        LONToUC = DTPLONDateToUC.Text
                    Else
                        LONToUC = ""
                    End If
                    If DTPLONSent.Checked = True Then
                        LonSent = DTPLONSent.Text
                    Else
                        LonSent = ""
                    End If
                    If DTPLonResolved.Checked = True Then
                        LONResolved = DTPLonResolved.Text
                    Else
                        LONResolved = ""
                    End If

                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "datLONToUC = '" & LONToUC & "', " & _
                    "datLONSent = '" & LonSent & "', " & _
                    "datLONResolved = '" & LONResolved & "', " & _
                    "strLONComments = '" & Replace(txtLONComments.Text, "'", "''") & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                If chbNOV.Checked = True Then
                    If DTPNOVDateToUC.Checked = True Then
                        NOVToUC = DTPNOVDateToUC.Text
                    Else
                        NOVToUC = ""
                    End If
                    If DTPNOVDateToPM.Checked = True Then
                        NOVToPM = DTPNOVDateToPM.Text
                    Else
                        NOVToPM = ""
                    End If
                    If DTPNOVsent.Checked = True Then
                        NOVSent = DTPNOVsent.Text
                    Else
                        NOVSent = ""
                    End If
                    If DTPNOVReceived.Checked = True Then
                        NOVResponseRec = DTPNOVReceived.Text
                    Else
                        NOVResponseRec = ""
                    End If
                    If DTPNFADateToUC.Checked = True Then
                        NFAToUC = DTPNFADateToUC.Text
                    Else
                        NFAToUC = ""
                    End If
                    If DTPNFADateToPM.Checked = True Then
                        NFAToPM = DTPNFADateToPM.Text
                    Else
                        NFAToPM = ""
                    End If
                    If DTPNFALetterSent.Checked = True Then
                        NFASent = DTPNFALetterSent.Text
                    Else
                        NFASent = ""
                    End If
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "datNOVToUC = '" & NOVToUC & "', " & _
                    "datNOVToPM = '" & NOVToPM & "', " & _
                    "datNovSent = '" & NOVSent & "', " & _
                    "datNOVResponseReceived = '" & NOVResponseRec & "', " & _
                    "datNFAToUC = '" & NFAToUC & "', " & _
                    "datNFAToPM = '" & NFAToPM & "', " & _
                    "datNFALetterSent = '" & NFASent & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                If chbCO.Checked = True Then
                    If DTPCODateToUC.Checked = True Then
                        COToUC = DTPCODateToUC.Text
                    Else
                        COToUC = ""
                    End If
                    If DTPCODateToPM.Checked = True Then
                        COToPM = DTPCODateToPM.Text
                    Else
                        COToPM = ""
                    End If
                    If DTPCOProposed.Checked = True Then
                        COProposed = DTPCOProposed.Text
                    Else
                        COProposed = ""
                    End If
                    If DTPCOReceivedfromCompany.Checked = True Then
                        COReceivedComp = DTPCOReceivedfromCompany.Text
                    Else
                        COReceivedComp = ""
                    End If
                    If DTPCOReceivedfromDirector.Checked = True Then
                        COReceivedDO = DTPCOReceivedfromDirector.Text
                    Else
                        COReceivedDO = ""
                    End If
                    If Me.DTPConsentOrderExecuted.Checked = True Then
                        COExecuted = DTPConsentOrderExecuted.Text
                    Else
                        COExecuted = ""
                    End If
                    If DTPCOResolved.Checked = True Then
                        COResolved = DTPCOResolved.Text
                    Else
                        COResolved = ""
                    End If
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "DatCOToUC = '" & COToUC & "', " & _
                    "datCOToPM = '" & COToPM & "', " & _
                    "datCOProposed = '" & COProposed & "', " & _
                    "datCOReceivedFromCompany = '" & COReceivedComp & "', " & _
                    "datCOReceivedFromDirector = '" & COReceivedDO & "', " & _
                    "datCOExecuted = '" & COExecuted & "', " & _
                    "strCONumber = '" & Replace(txtCONumber.Text, "'", "''") & "', " & _
                    "datCOResolved = '" & COResolved & "', " & _
                    "strCOPenaltyAmount = '" & txtCOPenaltyAmount.Text & "', " & _
                    "strCOPenaltyAmountComments = '" & Replace(txtPenaltyComments.Text, "'", "''") & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                If chbAO.Checked = True Then
                    If DTPAOExecuted.Checked = True Then
                        AOExecuted = DTPAOExecuted.Text
                    Else
                        AOExecuted = ""
                    End If
                    If DTPAOAppealed.Checked = True Then
                        AOAppealed = DTPAOAppealed.Text
                    Else
                        AOAppealed = ""
                    End If
                    If DTPAOResolved.Checked = True Then
                        AOResolved = DTPAOResolved.Text
                    Else
                        AOResolved = ""
                    End If


                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "datAOExecuted = '" & AOExecuted & "', " & _
                    "datAOAppealed = '" & AOAppealed & "', " & _
                    "datAOResolved = '" & AOResolved & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If txtNOVComments.Text <> "" Then
                    If txtMaxNOVComment.Text = "" Then
                        txtMaxNOVComment.Text = "1"
                    Else
                        txtMaxNOVComment.Text = CStr(CInt(txtMaxNOVComment.Text) + 1)
                    End If
                    SQL = "Insert into " & connNameSpace & ".SSCPENforcementNOVComments " & _
                    "values " & _
                    "('" & txtEnforcementNumber.Text & "', " & _
                    "'" & txtMaxNOVComment.Text & "', " & _
                    "'" & Replace(txtNOVComments.Text, "'", "''") & "') "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "strNOVCommentsEntry = '" & txtMaxNOVComment.Text & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    txtNOVPastComments.Text = txtNOVComments.Text
                    txtNOVComments.Clear()
                    cboNOVEntries.Items.Add(txtMaxNOVComment.Text)
                    cboNOVEntries.Text = txtMaxNOVComment.Text
                End If

                If txtCOComments.Text <> "" Then
                    If txtMaxCOComment.Text = "" Then
                        txtMaxCOComment.Text = "1"
                    Else
                        txtMaxCOComment.Text = CStr(CInt(txtMaxCOComment.Text) + 1)
                    End If
                    SQL = "Insert into " & connNameSpace & ".SSCPEnforcementCOComments " & _
                    "values " & _
                    "('" & txtEnforcementNumber.Text & "', " & _
                    "'" & txtMaxCOComment.Text & "', " & _
                    "'" & Replace(txtCOComments.Text, "'", "''") & "') "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "strCOCommentsEntry = '" & txtMaxCOComment.Text & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    txtCoPastComments.Text = txtCOComments.Text
                    txtCOComments.Clear()
                    cboCOEntries.Items.Add(txtMaxCOComment.Text)
                    cboCOEntries.Text = txtMaxCOComment.Text
                End If
                If txtAOComments.Text <> "" Then
                    If txtMaxAOComment.Text = "" Then
                        txtMaxAOComment.Text = "1"
                    Else
                        txtMaxAOComment.Text = CStr(CInt(txtMaxAOComment.Text) + 1)
                    End If
                    SQL = "Insert into " & connNameSpace & ".SSCPEnforcementAOComments " & _
                    "values " & _
                    "('" & txtEnforcementNumber.Text & "', " & _
                    "'" & txtMaxAOComment.Text & "', " & _
                    "'" & Replace(txtAOComments.Text, "'", "''") & "') "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "strAOCommentsEntry = '" & txtMaxAOComment.Text & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    txtAOPastComments.Text = txtAOComments.Text
                    txtAOComments.Clear()
                    cboAOEntries.Items.Add(txtMaxAOComment.Text)
                    cboAOEntries.Text = txtMaxAOComment.Text
                End If

                If txtStipulatedPenalty.Text <> "" Then
                    SaveStipulatedPenalties()
                End If
                If EnforcementChecklist Is Nothing Then
                    MsgBox("Current Data SAVED.", MsgBoxStyle.Information, "SSCP Enforcement")
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub SaveStipulatedPenalties()
        Try
            Dim StipulatedCount As String = ""
            Dim AFSNumber As String = ""
            Dim temp As String = ""

            If txtStipulatedKey.Text <> "" And txtEnforcementNumber.Text <> "" Then
                SQL = "Select " & _
                "strEnforcementKey, strAFSStipulatedPenaltyNumber " & _
                "from " & connNameSpace & ".SSCPENforcementStipulated " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                "and strEnforcementKey = '" & txtStipulatedKey.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("strEnforcementKey")) Then
                        StipulatedCount = ""
                    Else
                        StipulatedCount = dr.Item("strEnforcementKey")
                    End If
                    If IsDBNull(dr.Item("strAFSStipulatedPenaltyNumber")) Then
                        AFSNumber = ""
                    Else
                        AFSNumber = dr.Item("strAFSStipulatedPenaltyNumber")
                    End If
                End If
                dr.Close()
                If AFSNumber = "" And txtKeyActionNumber.Text <> "" Then
                    SQL = "Select strAFSActionNumber " & _
                    "from " & connNameSpace & ".APBSupplamentalData " & _
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        AFSNumber = dr.Item("strAFSActionNumber")
                    End While
                    dr.Close()

                    'This was removed when AFS went to 5 digits. 

                    'Select Case AFSNumber.Length
                    '    Case 0
                    '        AFSNumber = "001"
                    '    Case 1
                    '        AFSNumber = "00" & AFSNumber
                    '    Case 2
                    '        AFSNumber = "0" & AFSNumber
                    '    Case 3
                    '        AFSNumber = AFSNumber
                    '    Case Else
                    '        AFSNumber = AFSNumber
                    'End Select

                    temp = CStr(CInt(AFSNumber) + 1)

                    'This was removed when AFS went to 5 digits. 

                    'Select Case temp.Length
                    '    Case 0
                    '        temp = "001"
                    '    Case 1
                    '        temp = "00" + temp
                    '    Case 2
                    '        temp = "0" + temp
                    '    Case 3
                    '        temp = temp
                    '    Case Else
                    '        temp = temp
                    'End Select
                    SQL = "Update " & connNameSpace & ".APBSupplamentalData set " & _
                    "strAFSActionNumber = '" & temp & "' " & _
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                If StipulatedCount <> "" Then
                    If AFSNumber <> "" Then
                        SQL = "Update " & connNameSpace & ".SSCPENforcementStipulated set " & _
                        "strStipulatedPenalty = '" & txtStipulatedPenalty.Text & "', " & _
                        "strStipulatedPenaltyComments = '" & Replace(txtStipulatedComments.Text, "'", "''") & "', " & _
                        "strAFSStipulatedPenaltyNumber = '" & AFSNumber & "', " & _
                        "strModifingPerson = '" & UserGCode & "', " & _
                        "datModifingDate = '" & OracleDate & "' " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                        "and strEnforcementKey = '" & StipulatedCount & "' "
                    Else
                        SQL = "Update " & connNameSpace & ".SSCPENforcementStipulated set " & _
                        "strStipulatedPenalty = '" & txtStipulatedPenalty.Text & "', " & _
                        "strStipulatedPenaltyComments = '" & Replace(txtStipulatedComments.Text, "'", "''") & "', " & _
                        "strModifingPerson = '" & UserGCode & "', " & _
                        "datModifingDate = '" & OracleDate & "' " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                        "and strEnforcementKey = '" & StipulatedCount & "' "
                    End If

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    MsgBox("Please Close Form and Try again.", MsgBoxStyle.Information, "Enforcement")
                End If
            Else
                If txtStipulatedPenalty.Text <> "" Then
                    SQL = "Select strStipulatedPenalty " & _
                    "from " & connNameSpace & ".SSCPEnforcement " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strStipulatedPenalty")) Then
                            StipulatedCount = ""
                        Else
                            StipulatedCount = dr.Item("strStipulatedPenalty")
                        End If
                    End While
                    dr.Close()
                    If StipulatedCount = "" Then
                        StipulatedCount = "1"
                    Else
                        StipulatedCount = CStr(CInt(StipulatedCount) + 1)
                    End If

                    If txtKeyActionNumber.Text <> "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & connNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            AFSNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        'This was removed when AFS went to 5 digits. 

                        'Select Case AFSNumber.Length
                        '    Case 0
                        '        AFSNumber = "001"
                        '    Case 1
                        '        AFSNumber = "00" & AFSNumber
                        '    Case 2
                        '        AFSNumber = "0" & AFSNumber
                        '    Case 3
                        '        AFSNumber = AFSNumber
                        '    Case Else
                        '        AFSNumber = AFSNumber
                        'End Select

                        temp = CStr(CInt(AFSNumber) + 1)

                        'This was removed when AFS went to 5 digits. 

                        'Select Case temp.Length
                        '    Case 0
                        '        temp = "001"
                        '    Case 1
                        '        temp = "00" + temp
                        '    Case 2
                        '        temp = "0" + temp
                        '    Case 3
                        '        temp = temp
                        '    Case Else
                        '        temp = temp
                        'End Select

                        SQL = "Update " & connNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & temp & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        AFSNumber = ""
                    End If
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "strStipulatedPenalty = '" & StipulatedCount & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Insert into " & connNameSpace & ".SSCPENforcementStipulated " & _
                    "values " & _
                    "('" & txtEnforcementNumber.Text & "', " & _
                    "'" & StipulatedCount & "', " & _
                    "'" & Replace(txtStipulatedPenalty.Text, "'", "''") & "', " & _
                    "'" & Replace(txtStipulatedComments.Text, "'", "''") & "', " & _
                    "'" & AFSNumber & "', " & _
                    "'" & UserGCode & "', " & _
                    "'" & OracleDate & "') "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
            End If

            If txtStipulatedPenalitiesActionNumber.Text.Contains(AFSNumber) Then
            Else
                txtStipulatedPenalitiesActionNumber.Text = txtStipulatedPenalitiesActionNumber.Text & AFSNumber & ", "
            End If

            txtStipulatedKey.Clear()
            txtStipulatedPenalty.Clear()
            txtStipulatedComments.Clear()

            SQL = "Select " & _
            "strStipulatedPenalty, " & _
            "Case " & _
            "    When strStipulatedPenaltyComments IS Null THen 'N/A' " & _
            "Else strStipulatedPenaltyComments  " & _
            "End StipulatedPenaltyComments, " & _
            "strAFSStipulatedPenaltyNumber, " & _
            "strEnforcementKey " & _
            "from " & connNameSpace & ".SSCPENforcementStipulated " & _
            "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
            "order by strEnforcementKey "

            dsStipulatedPenalty = New DataSet
            daStipulatedPenalty = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            daStipulatedPenalty.Fill(dsStipulatedPenalty, "StipulatedPenalty")
            dgvStipulatedPenalties.DataSource = dsStipulatedPenalty
            dgvStipulatedPenalties.DataMember = "StipulatedPenalty"

            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub SubmitToUC()
        Try

            If txtEnforcementNumber.Text <> "" Then
                If (chbNOV.Checked = True Or chbCO.Checked = True Or chbAO.Checked = True) And txtDiscoveryEventNumber.Text = "" Then
                    MsgBox("This Enforcement Action cannot be submitted to your UC until a discovery event is linked.", MsgBoxStyle.Exclamation, "Enforcement Tool")
                    btnSubmitToUC.Visible = True
                Else
                    SQL = "Select strStatus " & _
                    "from " & connNameSpace & ".SSCPEnforcementItems " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        SQL = "Update " & connNameSpace & ".SSCPEnforcementItems set " & _
                        "strStatus = 'UC' " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                        If conn.State = ConnectionState.Open Then
                            'conn.close()
                        End If
                        MsgBox("Enforcement submitted to UC", MsgBoxStyle.Information, "Enforcement Tool")
                        btnSubmitToUC.Visible = False
                    Else
                        MsgBox("This Enforcement has yet to be Saved. Please Save first.", MsgBoxStyle.Exclamation, "Enforcement Tool")
                        btnSubmitToUC.Visible = True
                    End If
                End If
            Else
                MsgBox("This Enforcement has yet to be Saved. Please Save first.", MsgBoxStyle.Exclamation, "Enforcement Tool")
            End If
            If conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub SaveAFSInformation()
        Dim KeyActionNumber As String = ""
        Dim NOVActionNumber As String = ""
        Dim NFAActionNumber As String = ""
        Dim COProposedActionNumber As String = ""
        Dim COExecutedActionNumber As String = ""
        Dim COResolvedActionNumber As String = ""
        Dim AOtoAGActionNumber As String = ""
        Dim AOtoCivilCourtActionNumber As String = ""
        Dim AOResolvedActionNumber As String = ""

        Try


            If chbHPV.Checked = True Or chbNOV.Checked = True Or chbCO.Checked = True Or chbAO.Checked = True Then
                If txtKeyActionNumber.Text = "" Then
                    SQL = "Select strAFSActionNumber " & _
                    "from " & connNameSpace & ".APBSupplamentalData " & _
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        KeyActionNumber = dr.Item("strAFSActionNumber")
                    End While
                    dr.Close()

                    'This was removed when AFS went to 5 digits. 

                    'Select Case KeyActionNumber.Length
                    '    Case 0
                    '        KeyActionNumber = "001"
                    '    Case 1
                    '        KeyActionNumber = "00" & KeyActionNumber
                    '    Case 2
                    '        KeyActionNumber = "0" & KeyActionNumber
                    '    Case 3
                    '        KeyActionNumber = KeyActionNumber
                    '    Case Else
                    '        KeyActionNumber = KeyActionNumber
                    'End Select

                    txtKeyActionNumber.Text = KeyActionNumber

                    KeyActionNumber = CStr(CInt(KeyActionNumber) + 1)

                    'This was removed when AFS went to 5 digits. 
                    'Select Case KeyActionNumber.Length
                    '    Case 0
                    '        KeyActionNumber = "001"
                    '    Case 1
                    '        KeyActionNumber = "00" & KeyActionNumber
                    '    Case 2
                    '        KeyActionNumber = "0" & KeyActionNumber
                    '    Case 3
                    '        KeyActionNumber = KeyActionNumber
                    '    Case Else
                    '        KeyActionNumber = KeyActionNumber
                    'End Select

                    SQL = "Update " & connNameSpace & ".APBSupplamentalData set " & _
                    "strAFSActionNUmber = '" & KeyActionNumber & "' " & _
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    KeyActionNumber = txtKeyActionNumber.Text
                End If

                If txtKeyActionNumber.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "strAFSKeyActionNumber = '" & txtKeyActionNumber.Text & "',  " & _
                    "strModifingPerson = '" & UserGCode & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    Try

                        dr = cmd.ExecuteReader
                        dr.Close()
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    End Try
                    ' 
                End If

                If DTPNOVsent.Checked = True Then 'HPV
                    If txtNOVActionNumber.Text = "" Then

                        SQL = "Select strAFSActionNumber " & _
                        "from " & connNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            NOVActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        'This was removed when AFS went to 5 digits. 
                        'Select Case NOVActionNumber.Length
                        '    Case 0
                        '        NOVActionNumber = "001"
                        '    Case 1
                        '        NOVActionNumber = "00" & NOVActionNumber
                        '    Case 2
                        '        NOVActionNumber = "0" & NOVActionNumber
                        '    Case 3
                        '        NOVActionNumber = NOVActionNumber
                        '    Case Else
                        '        NOVActionNumber = NOVActionNumber
                        'End Select

                        txtNOVActionNumber.Text = NOVActionNumber

                        NOVActionNumber = CStr(CInt(NOVActionNumber) + 1)

                        'This was removed when AFS went to 5 digits. 
                        'Select Case NOVActionNumber.Length
                        '    Case 0
                        '        NOVActionNumber = "001"
                        '    Case 1
                        '        NOVActionNumber = "00" & NOVActionNumber
                        '    Case 2
                        '        NOVActionNumber = "0" & NOVActionNumber
                        '    Case 3
                        '        NOVActionNumber = NOVActionNumber
                        '    Case Else
                        '        NOVActionNumber = NOVActionNumber
                        'End Select

                        SQL = "Update " & connNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNUmber = '" & NOVActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        NOVActionNumber = txtNOVActionNumber.Text
                    End If
                Else
                    NOVActionNumber = ""
                End If

                If txtNOVActionNumber.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "strAFSNOVSentNumber = '" & txtNOVActionNumber.Text & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    Try

                        dr = cmd.ExecuteReader
                        dr.Close()

                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    End Try
                    ' 
                End If

                If DTPNFALetterSent.Checked = True Then
                    If txtNFAActionNumber.Text = "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & connNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            NFAActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        'This was removed when AFS went to 5 digits. 
                        'Select Case NFAActionNumber.Length
                        '    Case 0
                        '        NFAActionNumber = "001"
                        '    Case 1
                        '        NFAActionNumber = "00" & NFAActionNumber
                        '    Case 2
                        '        NFAActionNumber = "0" & NFAActionNumber
                        '    Case 3
                        '        NFAActionNumber = NFAActionNumber
                        '    Case Else
                        '        NFAActionNumber = NFAActionNumber
                        'End Select

                        txtNFAActionNumber.Text = NFAActionNumber

                        NFAActionNumber = CStr(CInt(NFAActionNumber) + 1)

                        'This was removed when AFS went to 5 digits. 
                        'Select Case NFAActionNumber.Length
                        '    Case 0
                        '        NFAActionNumber = "001"
                        '    Case 1
                        '        NFAActionNumber = "00" & NFAActionNumber
                        '    Case 2
                        '        NFAActionNumber = "0" & NFAActionNumber
                        '    Case 3
                        '        NFAActionNumber = NFAActionNumber
                        '    Case Else
                        '        NFAActionNumber = NFAActionNumber
                        'End Select

                        SQL = "Update " & connNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & NFAActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        NFAActionNumber = txtNFAActionNumber.Text
                    End If
                Else
                    NFAActionNumber = ""
                End If

                If txtNFAActionNumber.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "strAFSNOVResolvedNumber = '" & txtNFAActionNumber.Text & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If DTPCOProposed.Checked = True Then
                    If txtCOProposedActionNumber.Text = "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & connNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            COProposedActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        'This was removed when AFS went to 5 digits. 
                        'Select Case COProposedActionNumber.Length
                        '    Case 0
                        '        COProposedActionNumber = "001"
                        '    Case 1
                        '        COProposedActionNumber = "00" & COProposedActionNumber
                        '    Case 2
                        '        COProposedActionNumber = "0" & COProposedActionNumber
                        '    Case 3
                        '        COProposedActionNumber = COProposedActionNumber
                        '    Case Else
                        '        COProposedActionNumber = COProposedActionNumber
                        'End Select

                        txtCOProposedActionNumber.Text = COProposedActionNumber

                        COProposedActionNumber = CStr(CInt(COProposedActionNumber) + 1)
                        'This was removed when AFS went to 5 digits. 
                        'Select Case COProposedActionNumber.Length
                        '    Case 0
                        '        COProposedActionNumber = "001"
                        '    Case 1
                        '        COProposedActionNumber = "00" & COProposedActionNumber
                        '    Case 2
                        '        COProposedActionNumber = "0" & COProposedActionNumber
                        '    Case 3
                        '        COProposedActionNumber = COProposedActionNumber
                        '    Case Else
                        '        COProposedActionNumber = COProposedActionNumber
                        'End Select

                        SQL = "Update " & connNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & COProposedActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        COProposedActionNumber = txtCOProposedActionNumber.Text
                    End If
                Else
                    COProposedActionNumber = ""
                End If

                If txtCOProposedActionNumber.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                         "strAFSCOProposedNumber = '" & txtCOProposedActionNumber.Text & "' " & _
                         "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If DTPConsentOrderExecuted.Checked = True Then
                    If txtCOExecutedActionNumber.Text = "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & connNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            COExecutedActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        'This was removed when AFS went to 5 digits. 
                        'Select Case COExecutedActionNumber.Length
                        '    Case 0
                        '        COExecutedActionNumber = "001"
                        '    Case 1
                        '        COExecutedActionNumber = "00" & COExecutedActionNumber
                        '    Case 2
                        '        COExecutedActionNumber = "0" & COExecutedActionNumber
                        '    Case 3
                        '        COExecutedActionNumber = COExecutedActionNumber
                        '    Case Else
                        '        COExecutedActionNumber = COExecutedActionNumber
                        'End Select

                        txtCOExecutedActionNumber.Text = COExecutedActionNumber

                        COExecutedActionNumber = CStr(CInt(COExecutedActionNumber) + 1)

                        'This was removed when AFS went to 5 digits. 
                        'Select Case COExecutedActionNumber.Length
                        '    Case 0
                        '        COExecutedActionNumber = "001"
                        '    Case 1
                        '        COExecutedActionNumber = "00" & COExecutedActionNumber
                        '    Case 2
                        '        COExecutedActionNumber = "0" & COExecutedActionNumber
                        '    Case 3
                        '        COExecutedActionNumber = COExecutedActionNumber
                        '    Case Else
                        '        COExecutedActionNumber = COExecutedActionNumber
                        'End Select

                        SQL = "Update " & connNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & COExecutedActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        COExecutedActionNumber = txtCOExecutedActionNumber.Text
                    End If
                Else
                    COExecutedActionNumber = ""
                End If

                If txtCOExecutedActionNumber.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "strAFSCOExecutedNumber = '" & txtCOExecutedActionNumber.Text & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If DTPCOResolved.Checked = True Then
                    If txtCOResolvedActionNumber.Text = "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & connNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            COResolvedActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        'This was removed when AFS went to 5 digits. 
                        'Select Case COResolvedActionNumber.Length
                        '    Case 0
                        '        COResolvedActionNumber = "001"
                        '    Case 1
                        '        COResolvedActionNumber = "00" & COResolvedActionNumber
                        '    Case 2
                        '        COResolvedActionNumber = "0" & COResolvedActionNumber
                        '    Case 3
                        '        COResolvedActionNumber = COResolvedActionNumber
                        '    Case Else
                        '        COResolvedActionNumber = COResolvedActionNumber
                        'End Select

                        txtCOResolvedActionNumber.Text = COResolvedActionNumber

                        COResolvedActionNumber = CStr(CInt(COResolvedActionNumber) + 1)

                        'This was removed when AFS went to 5 digits. 
                        'Select Case COResolvedActionNumber.Length
                        '    Case 0
                        '        COResolvedActionNumber = "001"
                        '    Case 1
                        '        COResolvedActionNumber = "00" & COResolvedActionNumber
                        '    Case 2
                        '        COResolvedActionNumber = "0" & COResolvedActionNumber
                        '    Case 3
                        '        COResolvedActionNumber = COResolvedActionNumber
                        '    Case Else
                        '        COResolvedActionNumber = COResolvedActionNumber
                        'End Select

                        SQL = "Update " & connNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & COResolvedActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        COResolvedActionNumber = txtCOResolvedActionNumber.Text
                    End If
                Else
                    COResolvedActionNumber = ""
                End If

                If txtCOResolvedActionNumber.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                           "strAFSCOResolvedNumber = '" & txtCOResolvedActionNumber.Text & "' " & _
                           "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                'Stipulated Penalties have to be handled by the stipulated penalties save
                If dgvStipulatedPenalties.RowCount <> 0 Then
                    Dim EnforcementKey As String = ""
                    Dim AFSStipNumber As String = ""
                    Dim temp As String = ""

                    SQL = "Select " & _
                    "strEnforcementKey, strAFSStipulatedPenaltyNumber " & _
                    "from " & connNameSpace & ".SSCPENforcementStipulated " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strAFSStipulatedPenaltyNumber")) Then
                            If IsDBNull(dr.Item("strEnforcementKey")) Then
                                EnforcementKey = EnforcementKey
                            Else
                                EnforcementKey = EnforcementKey & dr.Item("strEnforcementKey") & ","
                            End If
                        Else
                            AFSStipNumber = dr.Item("strAFSStipulatedPenaltyNumber")
                        End If
                    End While
                    dr.Close()

                    If EnforcementKey <> "" Then
                        Do While EnforcementKey <> ""
                            SQL = "Select strAFSActionNumber " & _
                            "from " & connNameSpace & ".APBSupplamentalData " & _
                            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                AFSStipNumber = dr.Item("strAFSActionNumber")
                            End While
                            dr.Close()

                            'This was removed when AFS went to 5 digits. 
                            'Select Case AFSStipNumber.Length
                            '    Case 0
                            '        AFSStipNumber = "001"
                            '    Case 1
                            '        AFSStipNumber = "00" & AFSStipNumber
                            '    Case 2
                            '        AFSStipNumber = "0" & AFSStipNumber
                            '    Case 3
                            '        AFSStipNumber = AFSStipNumber
                            '    Case Else
                            '        AFSStipNumber = AFSStipNumber
                            'End Select

                            If txtStipulatedPenalitiesActionNumber.Text.Contains(AFSStipNumber) Then
                            Else
                                txtStipulatedPenalitiesActionNumber.Text = txtStipulatedPenalitiesActionNumber.Text & AFSStipNumber & ", "
                            End If

                            temp = Mid(EnforcementKey, 1, (InStr(EnforcementKey, ",", CompareMethod.Text)) - 1)
                            EnforcementKey = Replace(EnforcementKey, (temp & ","), "")

                            SQL = "Update " & connNameSpace & ".SSCPENforcementStipulated set " & _
                            "strAFSStipulatedPenaltyNumber = '" & AFSStipNumber & "' " & _
                            "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                            "and strEnforcementKey = '" & temp & "' "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()

                            AFSStipNumber = CStr(CInt(AFSStipNumber) + 1)

                            'This was removed when AFS went to 5 digits. 
                            'Select Case AFSStipNumber.Length
                            '    Case 0
                            '        AFSStipNumber = "001"
                            '    Case 1
                            '        AFSStipNumber = "00" & AFSStipNumber
                            '    Case 2
                            '        AFSStipNumber = "0" & AFSStipNumber
                            '    Case 3
                            '        AFSStipNumber = AFSStipNumber
                            '    Case Else
                            '        AFSStipNumber = AFSStipNumber
                            'End Select

                            SQL = "Update " & connNameSpace & ".APBSupplamentalData set " & _
                            "strAFSActionNumber = '" & AFSStipNumber & "' " & _
                            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()

                        Loop

                        SQL = "Select " & _
                        "strStipulatedPenalty, " & _
                        "Case " & _
                        "    When strStipulatedPenaltyComments IS Null THen 'N/A' " & _
                        "Else strStipulatedPenaltyComments  " & _
                        "End StipulatedPenaltyComments, " & _
                        "strAFSStipulatedPenaltyNumber, " & _
                        "strEnforcementKey " & _
                        "from " & connNameSpace & ".SSCPENforcementStipulated " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                        "order by strEnforcementKey "

                        dsStipulatedPenalty = New DataSet
                        daStipulatedPenalty = New OracleDataAdapter(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If

                        daStipulatedPenalty.Fill(dsStipulatedPenalty, "StipulatedPenalty")
                        dgvStipulatedPenalties.DataSource = dsStipulatedPenalty
                        dgvStipulatedPenalties.DataMember = "StipulatedPenalty"
                    End If
                End If

                If DTPAOExecuted.Checked = True Then
                    If txtAOToAGActionNumber.Text = "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & connNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            AOtoAGActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        'This was removed when AFS went to 5 digits. 
                        'Select Case AOtoAGActionNumber.Length
                        '    Case 0
                        '        AOtoAGActionNumber = "001"
                        '    Case 1
                        '        AOtoAGActionNumber = "00" & AOtoAGActionNumber
                        '    Case 2
                        '        AOtoAGActionNumber = "0" & AOtoAGActionNumber
                        '    Case 3
                        '        AOtoAGActionNumber = AOtoAGActionNumber
                        '    Case Else
                        '        AOtoAGActionNumber = AOtoAGActionNumber
                        'End Select

                        txtAOToAGActionNumber.Text = AOtoAGActionNumber

                        AOtoAGActionNumber = CStr(CInt(AOtoAGActionNumber) + 1)

                        'This was removed when AFS went to 5 digits. 
                        'Select Case AOtoAGActionNumber.Length
                        '    Case 0
                        '        AOtoAGActionNumber = "001"
                        '    Case 1
                        '        AOtoAGActionNumber = "00" & AOtoAGActionNumber
                        '    Case 2
                        '        AOtoAGActionNumber = "0" & AOtoAGActionNumber
                        '    Case 3
                        '        AOtoAGActionNumber = AOtoAGActionNumber
                        '    Case Else
                        '        AOtoAGActionNumber = AOtoAGActionNumber
                        'End Select

                        SQL = "Update " & connNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & AOtoAGActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        AOtoAGActionNumber = txtAOToAGActionNumber.Text
                    End If
                Else
                    AOtoAGActionNumber = ""
                End If

                If txtAOToAGActionNumber.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                           "strAFSAOTOAGNumber = '" & txtAOToAGActionNumber.Text & "' " & _
                           "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If DTPAOAppealed.Checked = True Then
                    If txtCivilCourtActionNumber.Text = "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & connNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            AOtoCivilCourtActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        'This was removed when AFS went to 5 digits. 
                        'Select Case AOtoCivilCourtActionNumber.Length
                        '    Case 0
                        '        AOtoCivilCourtActionNumber = "001"
                        '    Case 1
                        '        AOtoCivilCourtActionNumber = "00" & AOtoCivilCourtActionNumber
                        '    Case 2
                        '        AOtoCivilCourtActionNumber = "0" & AOtoCivilCourtActionNumber
                        '    Case 3
                        '        AOtoCivilCourtActionNumber = AOtoCivilCourtActionNumber
                        '    Case Else
                        '        AOtoCivilCourtActionNumber = AOtoCivilCourtActionNumber
                        'End Select

                        txtCivilCourtActionNumber.Text = AOtoCivilCourtActionNumber

                        AOtoCivilCourtActionNumber = CStr(CInt(AOtoCivilCourtActionNumber) + 1)

                        'This was removed when AFS went to 5 digits. 
                        'Select Case AOtoCivilCourtActionNumber.Length
                        '    Case 0
                        '        AOtoCivilCourtActionNumber = "001"
                        '    Case 1
                        '        AOtoCivilCourtActionNumber = "00" & AOtoCivilCourtActionNumber
                        '    Case 2
                        '        AOtoCivilCourtActionNumber = "0" & AOtoCivilCourtActionNumber
                        '    Case 3
                        '        AOtoCivilCourtActionNumber = AOtoCivilCourtActionNumber
                        '    Case Else
                        '        AOtoCivilCourtActionNumber = AOtoCivilCourtActionNumber
                        'End Select

                        SQL = "Update " & connNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & AOtoCivilCourtActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        AOtoCivilCourtActionNumber = txtCivilCourtActionNumber.Text
                    End If
                Else
                    AOtoCivilCourtActionNumber = ""
                End If

                If txtCivilCourtActionNumber.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "strAFSCivilCourtNumber = '" & txtCivilCourtActionNumber.Text & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If DTPAOResolved.Checked = True Then
                    If txtAOResolvedActionNumber.Text = "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & connNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            AOResolvedActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        'This was removed when AFS went to 5 digits. 
                        'Select Case AOResolvedActionNumber.Length
                        '    Case 0
                        '        AOResolvedActionNumber = "001"
                        '    Case 1
                        '        AOResolvedActionNumber = "00" & AOResolvedActionNumber
                        '    Case 2
                        '        AOResolvedActionNumber = "0" & AOResolvedActionNumber
                        '    Case 3
                        '        AOResolvedActionNumber = AOResolvedActionNumber
                        '    Case Else
                        '        AOResolvedActionNumber = AOResolvedActionNumber
                        'End Select

                        txtAOResolvedActionNumber.Text = AOResolvedActionNumber

                        AOResolvedActionNumber = CStr(CInt(AOResolvedActionNumber) + 1)

                        'This was removed when AFS went to 5 digits. 
                        'Select Case AOResolvedActionNumber.Length
                        '    Case 0
                        '        AOResolvedActionNumber = "001"
                        '    Case 1
                        '        AOResolvedActionNumber = "00" & AOResolvedActionNumber
                        '    Case 2
                        '        AOResolvedActionNumber = "0" & AOResolvedActionNumber
                        '    Case 3
                        '        AOResolvedActionNumber = AOResolvedActionNumber
                        '    Case Else
                        '        AOResolvedActionNumber = AOResolvedActionNumber
                        'End Select

                        SQL = "Update " & connNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & AOResolvedActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        AOResolvedActionNumber = txtAOResolvedActionNumber.Text
                    End If
                Else
                    AOResolvedActionNumber = ""
                End If

                If txtAOResolvedActionNumber.Text <> "" Then
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "strAFSAOResolvedNumber = '" & txtAOResolvedActionNumber.Text & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

            Else
                KeyActionNumber = ""
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub ManualEntry()
        Try

            If txtKeyActionNumber.ReadOnly = True Then
                txtKeyActionNumber.ReadOnly = False
                txtCOExecutedActionNumber.ReadOnly = False
                txtCivilCourtActionNumber.ReadOnly = False
                txtNOVActionNumber.ReadOnly = False
                txtCOResolvedActionNumber.ReadOnly = False
                txtAOToAGActionNumber.ReadOnly = False
                txtNFAActionNumber.ReadOnly = False
                txtCOProposedActionNumber.ReadOnly = False
                txtAOResolvedActionNumber.ReadOnly = False
                txtStipulatedPenalitiesActionNumber.ReadOnly = False
            Else
                txtKeyActionNumber.ReadOnly = True
                txtCOExecutedActionNumber.ReadOnly = True
                txtCivilCourtActionNumber.ReadOnly = True
                txtNOVActionNumber.ReadOnly = True
                txtCOResolvedActionNumber.ReadOnly = True
                txtAOToAGActionNumber.ReadOnly = True
                txtNFAActionNumber.ReadOnly = True
                txtCOProposedActionNumber.ReadOnly = True
                txtAOResolvedActionNumber.ReadOnly = True
                txtStipulatedPenalitiesActionNumber.ReadOnly = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub ClearEnforcement()
        Try
            txtAOPastComments.Clear()
            cboAOEntries.Items.Clear()
            txtMaxAOComment.Clear()
            txtAOComments.Clear()
            DTPAOResolved.Text = OracleDate
            DTPAOResolved.Checked = False
            DTPAOAppealed.Text = OracleDate
            DTPAOAppealed.Checked = False
            DTPAOExecuted.Text = OracleDate
            DTPAOExecuted.Checked = False
            txtStipulatedComments.Clear()
            txtStipulatedKey.Clear()
            txtStipulatedPenalty.Clear()
            dsStipulatedPenalty = New DataSet
            txtCoPastComments.Clear()
            txtMaxCOComment.Clear()
            cboCOEntries.Items.Clear()
            txtCOComments.Clear()
            txtPenaltyComments.Clear()
            txtCOPenaltyAmount.Clear()
            DTPCOResolved.Text = OracleDate
            DTPCOResolved.Checked = False
            txtCONumber.Clear()
            DTPConsentOrderExecuted.Text = OracleDate
            DTPConsentOrderExecuted.Checked = False
            DTPCOReceivedfromDirector.Text = OracleDate
            DTPCOReceivedfromDirector.Checked = False
            DTPCOReceivedfromCompany.Text = OracleDate
            DTPCOReceivedfromCompany.Checked = False
            DTPCOProposed.Text = OracleDate
            DTPCOProposed.Checked = False
            DTPCODateToPM.Text = OracleDate
            DTPCODateToPM.Checked = False
            DTPCODateToUC.Text = OracleDate
            DTPCODateToUC.Checked = False
            txtNOVPastComments.Clear()
            txtMaxNOVComment.Clear()
            cboNOVEntries.Items.Clear()
            txtNOVComments.Clear()
            DTPNFALetterSent.Text = OracleDate
            DTPNFALetterSent.Checked = False
            DTPNFADateToPM.Text = OracleDate
            DTPNFADateToPM.Checked = False
            DTPNFADateToUC.Text = OracleDate
            DTPNFADateToUC.Checked = False
            DTPNOVReceived.Text = OracleDate
            DTPNOVReceived.Checked = False
            DTPNOVsent.Text = OracleDate
            DTPNOVsent.Checked = False
            DTPNOVDateToPM.Text = OracleDate
            DTPNOVDateToPM.Checked = False
            DTPNOVDateToUC.Text = OracleDate
            DTPNOVDateToUC.Checked = False
            txtLONComments.Clear()
            DTPLONSent.Text = OracleDate
            DTPLONSent.Checked = False
            DTPLonResolved.Text = OracleDate
            DTPLonResolved.Checked = False
            DTPLONDateToUC.Text = OracleDate
            DTPLONDateToUC.Checked = False
            txtStipulatedPenalitiesActionNumber.Clear()
            txtAOResolvedActionNumber.Clear()

            txtCOProposedActionNumber.Clear()
            txtNFAActionNumber.Clear()
            txtAOToAGActionNumber.Clear()
            txtCOResolvedActionNumber.Clear()
            txtNOVActionNumber.Clear()
            txtCivilCourtActionNumber.Clear()
            txtCOExecutedActionNumber.Clear()
            txtKeyActionNumber.Clear()
            DTPEnforcementResolved.Text = OracleDate
            DTPEnforcementResolved.Checked = False
            chbHPV.Checked = False
            txtGeneralComments.Clear()
            DTPDayZero.Text = OracleDate
            DTPDayZero.Checked = False
            DTPViolationDate.Text = OracleDate
            DTPViolationDate.Checked = False
            chbLON.Checked = False
            chbNOV.Checked = False
            chbCO.Checked = False
            chbAO.Checked = False
            cboStaffResponsible.Text = ""
            txtDiscoveryEventNumber.Clear()
            DTPLastSave.Text = OracleDate
            txtTrackingNumber.Clear()
            txtEnforcementNumber.Clear()


            LoadEnforcement()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub DeleteEnforcement()
        Try
            Dim AFSStatus As String = ""
            Dim tempAIRS As String = ""

            SQL = "Select strUpDateStatus " & _
            "from " & connNameSpace & ".AFSSSCPEnforcementRecords " & _
            "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                AFSStatus = dr.Item("strUpDateStatus")
            Else
                AFSStatus = "X"
            End If
            dr.Close()

            If AFSStatus = "C" Or AFSStatus = "N" Then
                MsgBox("This Enforcement has already been submitted to the EPA." & vbCrLf & _
                "Please contact your manager and Michael Floyd to have this enforcement Deleted.", _
                MsgBoxStyle.Exclamation, "Enforcement")
            Else
                Dim Result As DialogResult

                Result = MessageBox.Show("Are you certain that you want to delete this enforcement?", _
                  "Enforcement", MessageBoxButtons.YesNoCancel, _
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                Select Case Result
                    Case Windows.Forms.DialogResult.Yes
                        SQL = "Select strAIRSNumber " & _
                        "from " & connNameSpace & ".SSCPEnforcementItems " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("strAIRSNumber")) Then
                                tempAIRS = ""
                            Else
                                tempAIRS = dr.Item("strAIRSNumber")
                            End If
                        End While
                        dr.Close()

                        SQL = "Delete " & connNameSpace & ".AFSSSCPEnforcementRecords " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from " & connNameSpace & ".AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                        cmd = New OracleCommand(SQL2, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete " & connNameSpace & ".SSCPENforcementStipulated " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from " & connNameSpace & ".AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                        cmd = New OracleCommand(SQL2, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete " & connNameSpace & ".SSCPEnforcementAOComments " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from " & connNameSpace & ".AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                        cmd = New OracleCommand(SQL2, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete " & connNameSpace & ".SSCPEnforcementCOComments " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from " & connNameSpace & ".AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                        cmd = New OracleCommand(SQL2, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete " & connNameSpace & ".SSCPENforcementNOVComments " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from " & connNameSpace & ".AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                        cmd = New OracleCommand(SQL2, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete " & connNameSpace & ".SSCPEnforcement " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from " & connNameSpace & ".AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                        cmd = New OracleCommand(SQL2, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete " & connNameSpace & ".SSCPEnforcementItems " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from " & connNameSpace & ".AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                        cmd = New OracleCommand(SQL2, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        MsgBox("Enforcement Deleted.", MsgBoxStyle.Information, "Enforcement")
                        ClearEnforcement()

                    Case Windows.Forms.DialogResult.No
                        SQL = ""
                    Case Windows.Forms.DialogResult.Cancel
                        SQL = ""
                    Case Else
                        SQL = ""
                End Select
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#End Region
#Region "Declaration"
    Private Sub chbLON_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbLON.CheckedChanged
        Try
            If chbLON.Checked = True Then
                TCEnforcement.TabPages.Add(TPLON)
            Else
                TCEnforcement.TabPages.Remove(TPLON)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub chbNOV_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbNOV.CheckedChanged
        Try
            If chbNOV.Checked = True Then
                TCEnforcement.TabPages.Add(TPNOV)
                If chbCO.Checked <> True And chbAO.Checked <> True Then
                    DTPDayZero.Visible = True
                    btn45DayZero.Visible = True
                    lblDayZero.Visible = True
                    lblPollutants.Visible = True
                    lblPollutantStatus.Visible = True
                    btnEditAirProgramPollutants.Visible = True
                    cboPollutantStatus.Visible = True
                    chbHPV.Visible = True
                    If chbHPV.Checked = True Then
                        cboHPVType.Visible = True
                    Else
                        cboHPVType.Visible = False
                    End If
                    If AccountArray(48, 2) = "1" Then
                        btnSubmitToUC.Visible = True
                    End If

                    If AccountArray(48, 3) = "1" Then
                        btnSubmitEnforcementToEPA.Visible = True
                        btnManuallyEnterAFS.Visible = True
                        btnSubmitToUC.Visible = False
                    End If
                    If AccountArray(48, 4) = "1" Then
                        btnManuallyEnterAFS.Visible = True
                    End If
                    If txtKeyActionNumber.Text <> "" Then
                        btnSubmitEnforcementToEPA.Visible = True
                    End If
                End If
            Else
                TCEnforcement.TabPages.Remove(TPNOV)
                If chbCO.Checked <> True And chbAO.Checked <> True Then
                    DTPDayZero.Visible = False
                    btn45DayZero.Visible = False
                    lblDayZero.Visible = False
                    lblPollutants.Visible = False
                    lblPollutantStatus.Visible = False
                    btnEditAirProgramPollutants.Visible = False
                    cboPollutantStatus.Visible = False
                    chbHPV.Visible = False
                    cboHPVType.Visible = False
                    btnSubmitToUC.Visible = False
                    btnSubmitEnforcementToEPA.Visible = False
                    btnManuallyEnterAFS.Visible = False
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub chbCO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbCO.CheckedChanged
        Try
            If chbCO.Checked = True Then
                TCEnforcement.TabPages.Add(TPCO)
                If chbNOV.Checked <> True And chbAO.Checked <> True Then
                    DTPDayZero.Visible = True
                    btn45DayZero.Visible = True
                    lblDayZero.Visible = True
                    lblPollutants.Visible = True
                    lblPollutantStatus.Visible = True
                    btnEditAirProgramPollutants.Visible = True
                    cboPollutantStatus.Visible = True
                    chbHPV.Visible = True
                    'If chbNOV.Checked = False Then
                    '    chbHPV.Checked = True
                    'End If
                    If chbHPV.Checked = True Then
                        cboHPVType.Visible = True
                    Else
                        cboHPVType.Visible = False
                    End If
                    If AccountArray(48, 2) = "1" And txtKeyActionNumber.Text = "" Then
                        btnSubmitToUC.Visible = True
                    End If

                    If AccountArray(48, 3) = "1" Then
                        btnSubmitEnforcementToEPA.Visible = True
                        btnManuallyEnterAFS.Visible = True
                        btnSubmitToUC.Visible = False
                    End If
                    If AccountArray(48, 4) = "1" Then
                        btnManuallyEnterAFS.Visible = True
                    End If
                    If txtKeyActionNumber.Text <> "" Then
                        btnSubmitEnforcementToEPA.Visible = True
                    End If
                End If
            Else
                TCEnforcement.TabPages.Remove(TPCO)
                If chbNOV.Checked <> True And chbAO.Checked <> True Then
                    DTPDayZero.Visible = False
                    btn45DayZero.Visible = False
                    lblDayZero.Visible = False
                    lblPollutants.Visible = False
                    lblPollutantStatus.Visible = False
                    btnEditAirProgramPollutants.Visible = False
                    cboPollutantStatus.Visible = False
                    chbHPV.Visible = False
                    cboHPVType.Visible = False
                    btnSubmitToUC.Visible = False
                    btnSubmitEnforcementToEPA.Visible = False
                    btnManuallyEnterAFS.Visible = False
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub chbAO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAO.CheckedChanged
        Try
            If chbAO.Checked = True Then
                TCEnforcement.TabPages.Add(TPAO)
                If chbNOV.Checked <> True And chbCO.Checked <> True Then
                    DTPDayZero.Visible = True
                    btn45DayZero.Visible = True
                    lblDayZero.Visible = True
                    lblPollutants.Visible = True
                    lblPollutantStatus.Visible = True
                    btnEditAirProgramPollutants.Visible = True
                    cboPollutantStatus.Visible = True
                    chbHPV.Visible = True
                    If chbHPV.Checked = True Then
                        cboHPVType.Visible = True
                    Else
                        cboHPVType.Visible = False
                    End If

                    If AccountArray(48, 2) = "1" And txtKeyActionNumber.Text = "" Then
                        btnSubmitToUC.Visible = True
                    End If

                    If AccountArray(48, 3) = "1" Then
                        btnSubmitEnforcementToEPA.Visible = True
                        btnManuallyEnterAFS.Visible = True
                        btnSubmitToUC.Visible = False
                    End If
                    If AccountArray(48, 4) = "1" Then
                        btnManuallyEnterAFS.Visible = True
                    End If
                    If txtKeyActionNumber.Text <> "" Then
                        btnSubmitEnforcementToEPA.Visible = True
                    End If
                End If
            Else
                TCEnforcement.TabPages.Remove(TPAO)
                If chbNOV.Checked <> True And chbCO.Checked <> True Then
                    DTPDayZero.Visible = False
                    btn45DayZero.Visible = False
                    lblDayZero.Visible = False
                    lblPollutants.Visible = False
                    lblPollutantStatus.Visible = False
                    btnEditAirProgramPollutants.Visible = False
                    cboPollutantStatus.Visible = False
                    chbHPV.Visible = False
                    cboHPVType.Visible = False
                    btnSubmitToUC.Visible = False
                    btnSubmitEnforcementToEPA.Visible = False
                    btnManuallyEnterAFS.Visible = False
                End If
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub chbHPV_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbHPV.CheckedChanged
        Try
            If chbHPV.Checked = True Then
                cboHPVType.Visible = True
            Else
                cboHPVType.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSave.Click
        Try
            SaveEnforcement()

            LoadEnforcement()

            If AccountArray(48, 2) = "1" Or AccountArray(48, 3) = "1" Or AccountArray(48, 4) = "1" Then
                CheckOpenStatus()
            End If

            If TCEnforcement.TabPages.Contains(Me.TPCO) Then
                If NavigationScreen.pnl4.Text = "TESTING ENVIRONMENT" Then
                    btnUploadCO.Visible = True
                    If Me.txtCONumber.Text <> "" Then
                        btnDownloadCO.Visible = True
                    Else
                        btnDownloadCO.Visible = False
                    End If
                Else
                    btnUploadCO.Visible = False
                    btnDownloadCO.Visible = False
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub txtTrackingNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTrackingNumber.TextChanged
        Try
            txtDiscoveryEventNumber.Text = txtTrackingNumber.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnEditAirProgramPollutants_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditAirProgramPollutants.Click
        Try
            If txtEnforcementNumber.Text <> "" Then

                EditAirProgramPollutants = Nothing
                If EditAirProgramPollutants Is Nothing Then EditAirProgramPollutants = New IAIPEditAirProgramPollutants
                EditAirProgramPollutants.txtAirsNumber.Text = Me.txtAIRSNumber.Text
                EditAirProgramPollutants.txtEnforcementNumber.Text = txtEnforcementNumber.Text
                EditAirProgramPollutants.Show()
                EditAirProgramPollutants.TPEnforcementPollutants.Focus()
                EditAirProgramPollutants.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            Else
                MsgBox("Save this Enforcement Action atleast once before you try to add pollutants.", MsgBoxStyle.Information, "Enforcement")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub cboNOVEntries_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboNOVEntries.TextChanged
        Try
            If cboNOVEntries.Text <> "" Then
                SQL = "Select " & _
                "strNOVComment " & _
                "from " & connNameSpace & ".SSCPENforcementNOVComments " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                "and strNOVEntryNumber = '" & cboNOVEntries.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strNOVComment")) Then
                        txtNOVPastComments.Text = ""
                    Else
                        txtNOVPastComments.Text = dr.Item("strNOVComment")
                    End If
                End While
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub cboCOEntries_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCOEntries.TextChanged
        Try
            If cboCOEntries.Text <> "" Then
                SQL = "Select " & _
                "strCOComment " & _
                "from " & connNameSpace & ".SSCPEnforcementCOComments " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                "and strCOEntryNumber = '" & cboCOEntries.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strCOComment")) Then
                        txtCoPastComments.Text = ""
                    Else
                        txtCoPastComments.Text = dr.Item("strCOComment")
                    End If
                End While
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub cboAOEntries_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAOEntries.TextChanged
        Try
            If cboAOEntries.Text <> "" Then
                SQL = "Select " & _
                "strAOComment " & _
                "from " & connNameSpace & ".SSCPEnforcementAOComments " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                "and strAOEntryNumber = '" & cboAOEntries.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strAOComment")) Then
                        txtAOPastComments.Text = ""
                    Else
                        txtAOPastComments.Text = dr.Item("strAOComment")
                    End If
                End While
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnRemoveNOVComment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveNOVComment.Click
        Try
            If cboNOVEntries.Text <> "" Then
                SQL = "Select " & _
                "strNOVEntryNumber " & _
                "from " & connNameSpace & ".SSCPENforcementNOVComments " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                "and strNOVEntryNumber = '" & cboNOVEntries.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Delete " & connNameSpace & ".SSCPENforcementNOVComments " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                    "and strNOVEntryNumber = '" & cboNOVEntries.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    txtMaxNOVComment.Text = CStr(CInt(txtMaxNOVComment.Text) - 1)
                    If txtMaxNOVComment.Text = 0 Then
                        txtMaxNOVComment.Text = ""
                    Else
                        txtMaxNOVComment.Text = txtMaxNOVComment.Text
                    End If
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "strNOVCommentsEntry = '" & txtMaxNOVComment.Text & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    cboNOVEntries.Items.Remove(cboNOVEntries.Text)
                    txtNOVPastComments.Clear()
                    cboNOVEntries.Text = ""
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnDeleteCOComments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteCOComments.Click
        Try
            If cboCOEntries.Text <> "" Then
                SQL = "Select " & _
                "strCOEntryNumber " & _
                "from " & connNameSpace & ".SSCPEnforcementCOComments " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                "and strCOEntryNumber = '" & cboCOEntries.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Delete " & connNameSpace & ".SSCPEnforcementCOComments " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                    "and strCOEntryNumber = '" & cboCOEntries.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    txtMaxCOComment.Text = CStr(CInt(txtMaxCOComment.Text) - 1)
                    If txtMaxCOComment.Text = 0 Then
                        txtMaxCOComment.Text = ""
                    Else
                        txtMaxCOComment.Text = txtMaxCOComment.Text
                    End If
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "strCOCommentsEntry = '" & txtMaxCOComment.Text & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    cboCOEntries.Items.Remove(cboCOEntries.Text)
                    txtCoPastComments.Clear()
                    cboCOEntries.Text = ""
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnRemoveAOEntries_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAOEntries.Click
        Try
            If cboAOEntries.Text <> "" Then
                SQL = "Select " & _
                "strAOEntryNumber " & _
                "from " & connNameSpace & ".SSCPEnforcementAOComments " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                "and strAOEntryNumber = '" & cboAOEntries.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Delete " & connNameSpace & ".SSCPEnforcementAOComments " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                    "and strAOEntryNumber = '" & cboAOEntries.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    txtMaxAOComment.Text = CStr(CInt(txtMaxAOComment.Text) - 1)
                    If txtMaxAOComment.Text = 0 Then
                        txtMaxAOComment.Text = ""
                    Else
                        txtMaxAOComment.Text = txtMaxAOComment.Text
                    End If
                    SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                    "strAOCommentsEntry = '" & txtMaxAOComment.Text & "' " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    cboAOEntries.Items.Remove(cboAOEntries.Text)
                    txtAOPastComments.Clear()
                    cboAOEntries.Text = ""
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnSaveNOVComments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveNOVComments.Click
        Try

            If txtNOVComments.Text <> "" And txtEnforcementNumber.Text <> "" Then
                If txtMaxNOVComment.Text = "" Then
                    txtMaxNOVComment.Text = "1"
                Else
                    txtMaxNOVComment.Text = CStr(CInt(txtMaxNOVComment.Text) + 1)
                End If
                SQL = "Insert into " & connNameSpace & ".SSCPENforcementNOVComments " & _
                "values " & _
                "('" & txtEnforcementNumber.Text & "', " & _
                "'" & txtMaxNOVComment.Text & "', " & _
                "'" & Replace(txtNOVComments.Text, "'", "''") & "') "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                "strNOVCommentsEntry = '" & txtMaxNOVComment.Text & "' " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                txtNOVPastComments.Text = txtNOVComments.Text
                txtNOVComments.Clear()
                cboNOVEntries.Items.Add(txtMaxNOVComment.Text)
                cboNOVEntries.Text = txtMaxNOVComment.Text
            Else

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnSaveCOComments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCOComments.Click
        Try

            If txtCOComments.Text <> "" And txtEnforcementNumber.Text <> "" Then
                If txtMaxCOComment.Text = "" Then
                    txtMaxCOComment.Text = "1"
                Else
                    txtMaxCOComment.Text = CStr(CInt(txtMaxCOComment.Text) + 1)
                End If
                SQL = "Insert into " & connNameSpace & ".SSCPEnforcementCOComments " & _
                "values " & _
                "('" & txtEnforcementNumber.Text & "', " & _
                "'" & txtMaxCOComment.Text & "', " & _
                "'" & Replace(txtCOComments.Text, "'", "''") & "') "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                "strCOCommentsEntry = '" & txtMaxCOComment.Text & "' " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                txtCoPastComments.Text = txtCOComments.Text
                txtCOComments.Clear()
                cboCOEntries.Items.Add(txtMaxCOComment.Text)
                cboCOEntries.Text = txtMaxCOComment.Text
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnSaveAOComments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAOComments.Click
        Try

            If txtAOComments.Text <> "" Then
                If txtMaxAOComment.Text = "" Then
                    txtMaxAOComment.Text = "1"
                Else
                    txtMaxAOComment.Text = CStr(CInt(txtMaxAOComment.Text) + 1)
                End If
                SQL = "Insert into " & connNameSpace & ".SSCPEnforcementAOComments " & _
                "values " & _
                "('" & txtEnforcementNumber.Text & "', " & _
                "'" & txtMaxAOComment.Text & "', " & _
                "'" & Replace(txtAOComments.Text, "'", "''") & "') "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update " & connNameSpace & ".SSCPEnforcement set " & _
                "strAOCommentsEntry = '" & txtMaxAOComment.Text & "' " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                txtAOPastComments.Text = txtAOComments.Text
                txtAOComments.Clear()
                cboAOEntries.Items.Add(txtMaxAOComment.Text)
                cboAOEntries.Text = txtMaxAOComment.Text
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnLinkEnforcement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLinkEnforcement.Click
        Try

            OpenChecklist()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnOpenEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenEvent.Click
        Try

            If txtTrackingNumber.Text <> "" Then
                SSCPREports = Nothing
                If SSCPREports Is Nothing Then SSCPREports = New SSCPEvents
                SSCPREports.txtTrackingNumber.Text = txtTrackingNumber.Text
                SSCPREports.Show()
                SSCPREports.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnSaveStipulatePenalty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveStipulatePenalty.Click
        Try

            SaveStipulatedPenalties()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub dgvStipulatedPenalties_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvStipulatedPenalties.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvStipulatedPenalties.HitTest(e.X, e.Y)

        Try


            If dgvStipulatedPenalties.RowCount > 0 And hti.RowIndex <> -1 Then
                txtStipulatedKey.Text = dgvStipulatedPenalties(3, hti.RowIndex).Value
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub txtStipulatedKey_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStipulatedKey.TextChanged
        Try

            If txtStipulatedKey.Text <> "" Then
                SQL = "Select " & _
                "strStipulatedPenalty, strStipulatedPenaltyComments " & _
                "from " & connNameSpace & ".SSCPENforcementStipulated " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                "and strEnforcementKey = '" & txtStipulatedKey.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strStipulatedPenalty")) Then
                        txtStipulatedPenalty.Text = ""
                    Else
                        txtStipulatedPenalty.Text = dr.Item("strStipulatedPenalty")
                    End If
                    If IsDBNull(dr.Item("strStipulatedPenaltyComments")) Then
                        txtStipulatedComments.Text = ""
                    Else
                        txtStipulatedComments.Text = dr.Item("strStipulatedPenaltyComments")
                    End If
                End While
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnClearStipulated_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearStipulated.Click
        Try

            txtStipulatedPenalty.Clear()
            txtStipulatedComments.Clear()
            txtStipulatedKey.Clear()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnSubmitToUC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmitToUC.Click
        Try

            SaveEnforcement()
            SubmitToUC()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnSubmitEnforcementToEPA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmitEnforcementToEPA.Click
        Try
            SaveEnforcement()


            If txtDiscoveryEventNumber.Text <> "" Then
                SaveAFSInformation()
            Else
                Dim Result As DialogResult

                Result = MessageBox.Show("There is no linked event for this enforcement action." & vbCrLf & _
                "Do you want to submit this enforcement to EPA without an initating action?", "Enforcement", _
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                Select Case Result
                    Case Windows.Forms.DialogResult.Yes
                        SaveAFSInformation()
                    Case Windows.Forms.DialogResult.No
                        MsgBox("AFS Data not saved.", MsgBoxStyle.Information, "Enforcement")
                    Case Windows.Forms.DialogResult.Cancel
                        MsgBox("AFS Data not saved.", MsgBoxStyle.Information, "Enforcement")
                    Case Else
                        MsgBox("AFS Data not saved.", MsgBoxStyle.Information, "Enforcement")
                End Select
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnManuallyEnterAFS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManuallyEnterAFS.Click
        Try
            SaveEnforcement()
            ManualEntry()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub DTPEnforcementResolved_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTPEnforcementResolved.ValueChanged
        Try

            If DTPEnforcementResolved.Checked = False Then
                CheckOpenStatus()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btn45DayZero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn45DayZero.Click
        Try
            Dim ViolationDate As String = ""
            Dim DayZero As String = ""

            If DTPViolationDate.Checked = True Then
                ViolationDate = DTPViolationDate.Text
            Else
                ViolationDate = OracleDate
            End If

            DayZero = CStr(Format(CDate(ViolationDate).AddDays(45), "dd-MMM-yyyy"))

            DTPDayZero.Checked = True
            DTPDayZero.Text = DayZero

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub txtDiscoveryEventNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscoveryEventNumber.TextChanged
        Try
            If txtDiscoveryEventNumber.Text = "" Then
                txtDiscoveryEventNumber.Visible = False
                lblDiscoveryEvent.Visible = False
                btnOpenEvent.Visible = False
            Else
                txtDiscoveryEventNumber.Visible = True
                lblDiscoveryEvent.Visible = True
                btnOpenEvent.Visible = True
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub DevEnforcement_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            SSCP_Enforcement = Nothing
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Private Sub tsbClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbClear.Click
        Try
            ClearEnforcement()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub tsbDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbDelete.Click
        Try

            If txtEnforcementNumber.Text <> "" Then
                DeleteEnforcement()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Me.Dispose()
    End Sub
    Private Sub mmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSave.Click
        Try
            SaveEnforcement()

            LoadEnforcement()

            If AccountArray(48, 2) = "1" Or AccountArray(48, 3) = "1" Or AccountArray(48, 4) = "1" Then
                CheckOpenStatus()
            End If

            If TCEnforcement.TabPages.Contains(Me.TPCO) Then
                If NavigationScreen.pnl4.Text = "TESTING ENVIRONMENT" Then
                    btnUploadCO.Visible = True
                    If Me.txtCONumber.Text <> "" Then
                        btnDownloadCO.Visible = True
                    Else
                        btnDownloadCO.Visible = False
                    End If
                Else
                    btnUploadCO.Visible = False
                    btnDownloadCO.Visible = False
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub mmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiBack.Click
        Me.Dispose()
    End Sub
    Private Sub mmiDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiDelete.Click
        Try

            If txtEnforcementNumber.Text <> "" Then
                DeleteEnforcement()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub ClearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearToolStripMenuItem.Click
        Try
            ClearEnforcement()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        Try

            SendKeys.Send("(^V)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("(^X)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        Try

            SendKeys.Send("(^C)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#End Region

    Private Sub btnUploadCO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUploadCO.Click
        Try
            Dim myStream As IO.Stream
            Dim path As New OpenFileDialog
            Dim PathName As String = "N/A"
            Dim FileName As String = ""
            Dim IDnumber As String = ""

            If txtEnforcementNumber.Text <> "" Then
                path.InitialDirectory = "c:\"
                path.Filter = "Word files (*.doc)|*.doc|All files (*.*)|*.*"
                path.FilterIndex = 1
                path.RestoreDirectory = True

                If path.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    myStream = path.OpenFile()
                    If Not (myStream Is Nothing) Then
                        If path.ValidateNames() Then
                            PathName = path.FileName.ToString
                            FileName = txtEnforcementNumber.Text
                        Else
                            PathName = "N/A"
                            FileName = "N/A"
                        End If
                        myStream.Close()
                    End If
                End If

                If PathName <> "N/A" Then

                    SQL = "Delete " & connNameSpace & ".SSCPEnforcementLetter " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    Dim Fs As IO.FileStream = New IO.FileStream(PathName, IO.FileMode.Open, IO.FileAccess.Read)
                    Dim DocData As Byte()
                    ReDim DocData(Fs.Length)

                    Fs.Read(DocData, 0, System.Convert.ToInt32(Fs.Length))
                    Fs.Close()

                    Dim da As OracleDataAdapter
                    Dim cmdCB As OracleCommandBuilder
                    Dim ds As DataSet

                    SQL = "Select * " & _
                    "from " & connNameSpace & ".SSCPEnforcementLetter " & _
                    "where strEnforcementNumber = '" & FileName & "' "
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    da = New OracleDataAdapter(SQL, conn)
                    cmdCB = New OracleCommandBuilder(da)
                    ds = New DataSet("IAIPData")
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey

                    da.Fill(ds, "IAIPData")
                    Dim row As DataRow = ds.Tables("IAIPData").NewRow()
                    row("strEnforcementNumber") = FileName
                    row("EnforcementLetter") = DocData
                    row("strModifingPerson") = UserGCode
                    row("DatModifingDate") = OracleDate
                    ds.Tables("IAIPData").Rows.Add(row)
                    da.Update(ds, "IAIPData")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnDownloadCO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownloadCO.Click
        Try
            Dim FileName As String
            Dim path As New SaveFileDialog
            Dim DestFilePath As String = "N/A"
            Dim OutPutFile As String = ""



            If txtEnforcementNumber.Text <> "" Then
                FileName = txtCONumber.Text

                path.InitialDirectory = "C:\"
                path.FileName = FileName
                path.Filter = "Word files (*.doc)|*.doc|All files (*.*)|*.*"
                path.FilterIndex = 1
                path.RestoreDirectory = True
                path.DefaultExt = ".doc"

                If path.ShowDialog = Windows.Forms.DialogResult.OK Then
                    DestFilePath = path.FileName.ToString
                Else
                    DestFilePath = "N/A"
                End If

                If DestFilePath <> "N/A" Then
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    SQL = "Select " & _
                    "strEnforcementNumber, EnforcementLetter " & _
                    "from " & connNameSpace & ".SSCPEnforcementLetter " & _
                    "Where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    dr = cmd.ExecuteReader

                    dr.Read()
                    Dim b(dr.GetBytes(1, 0, Nothing, 0, Integer.MaxValue) - 1) As Byte
                    dr.GetBytes(1, 0, b, 0, b.Length)
                    dr.Close()

                    Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                    fs.Write(b, 0, b.Length)
                    fs.Close()

                    If conn.State = ConnectionState.Open Then
                        'conn.close()
                    End If

                End If
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem.Click
        Try
            Help.ShowHelp(Label1, "http://airpermit.dnr.state.ga.us/helpdocs/IAIP_help/")
        Catch ex As Exception
        End Try

    End Sub

    Private Sub lvPollutants_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvPollutants.ColumnClick
        Try

            ' Set the ListViewItemSorter property to a new ListViewItemComparer object.
            lvPollutants.ListViewItemSorter = New ListViewItemComparer(e.Column)
            ' Call the sort method to manually sort the column based on the ListViewItemComparer implementation.
            lvPollutants.Sort()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub


End Class