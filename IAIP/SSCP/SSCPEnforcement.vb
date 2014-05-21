Imports System.Collections.Generic
Imports System.IO

Imports Iaip.Apb.SSCP
Imports Iaip.DAL.SSCP
Imports Iaip.DAL.Documents

Imports Oracle.DataAccess.Types
Imports Oracle.DataAccess.Client

Public Class SscpEnforcement

#Region "Properties"

    Public Property EnforcementInfo() As EnforcementInfo
        Get
            Return _enforcementInfo
        End Get
        Set(ByVal value As EnforcementInfo)
            _enforcementInfo = value
        End Set
    End Property
    Private _enforcementInfo As EnforcementInfo

    Public Property EnforcementNumber() As String
        Get
            If Me.ID = -1 Then
                Return ""
            Else
                Return Me.ID.ToString
            End If
        End Get
        Set(ByVal value As String)
            Dim i As Integer = -1
            If Integer.TryParse(value, i) Then
                Me.ID = i
            Else
                Me.ID = -1
            End If
        End Set
    End Property

#End Region

#Region "Local variables"
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

#End Region

#Region "Document uploads"

#Region "Local variables"

    Private ExistingFiles As List(Of EnforcementDocument)

#End Region

#Region "Display files"

    Private Sub LoadDocuments()
        DisableDocument()
        dgvDocumentList.DataSource = Nothing
        ExistingFiles = GetEnforcementDocumentsAsList(EnforcementInfo.EnforcementNumber)
        If ExistingFiles.Count > 0 Then
            With dgvDocumentList
                .DataSource = New BindingSource(ExistingFiles, Nothing)
                .Enabled = True
                .ClearSelection()
            End With
        End If
    End Sub

    Private Sub FormatDocumentList()
        With dgvDocumentList
            .Columns("EnforcementNumber").Visible = False
            .Columns("BinaryFileId").Visible = False
            With .Columns("Comment")
                .HeaderText = "Description"
                .DisplayIndex = 4
            End With
            .Columns("DocumentId").Visible = False
            With .Columns("DocumentType")
                .HeaderText = "Document Type"
                .DisplayIndex = 0
            End With
            .Columns("DocumentTypeId").Visible = False
            .Columns("FileExtension").Visible = False
            With .Columns("FileName")
                .HeaderText = "File Name"
                .DisplayIndex = 1
            End With
            With .Columns("FileSize")
                .HeaderText = "File Size"
                .DefaultCellStyle.Format = "fs:1"
                .DisplayIndex = 3
                .DefaultCellStyle.FormatProvider = New FileSizeFormatProvider
            End With
            With .Columns("UploadDate")
                .HeaderText = "Uploaded On"
                .DefaultCellStyle.Format = DateFormat
                .DisplayIndex = 2
            End With
        End With
    End Sub

    Private Sub dataGridView_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles dgvDocumentList.CellFormatting
        If TypeOf e.CellStyle.FormatProvider Is ICustomFormatter Then
            e.Value = TryCast(e.CellStyle.FormatProvider.GetFormat(GetType(ICustomFormatter)), ICustomFormatter).Format(e.CellStyle.Format, e.Value, e.CellStyle.FormatProvider)
            e.FormattingApplied = True
        End If
    End Sub

    Private Sub dgvDocumentList_DataBindingComplete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgvDocumentList.DataBindingComplete
        FormatDocumentList()
        CType(sender, DataGridView).SanelyResizeColumns()
        CType(sender, DataGridView).ClearSelection()
    End Sub

#End Region

#Region "Enable/Disable Form Regions"
    Private Sub EnableDocument()
        EnableOrDisableDocument(True)
    End Sub
    Private Sub DisableDocument()
        EnableOrDisableDocument(False)
    End Sub
    Private Sub EnableOrDisableDocument(ByVal enable As Boolean)
        With pnlDocument
            .Enabled = enable
            .Visible = enable
        End With
        If enable Then
            txtDocumentDescription.Text = dgvDocumentList.CurrentRow.Cells("Comment").Value
            lblDocumentName.Text = dgvDocumentList.CurrentRow.Cells("FileName").Value
        End If
    End Sub
#End Region

#Region "Clear form sections"

    Private Sub ClearEverything()
        ClearMessage(lblMessage, EP)
        ClearDocumentList()
    End Sub

    Private Sub ClearDocumentList()
        With dgvDocumentList
            .DataSource = Nothing
            .Enabled = False
        End With
        DisableDocument()
    End Sub

#End Region

#Region "Document update/download/delete"

    Private Sub dgvDocumentList_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvDocumentList.SelectionChanged
        If dgvDocumentList.SelectedRows.Count = 1 Then
            EnableDocument()
        Else
            DisableDocument()
        End If
    End Sub

    Private Sub btnDocumentDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDocumentDownload.Click
        ClearMessage(lblMessage, EP)

        Dim doc As EnforcementDocument = EnforcementDocumentFromFileListRow(dgvDocumentList.CurrentRow)
        DisplayMessage(lblMessage, String.Format(GetDocumentMessage(DocumentMessageType.DownloadingFile), doc.FileName))

        Dim canceled As Boolean = False
        Dim downloaded As Boolean = DownloadDocument(doc, canceled, Me)
        If downloaded Or canceled Then
            ClearMessage(lblMessage, EP)
        Else
            DisplayMessage(lblMessage, String.Format(GetDocumentMessage(DocumentMessageType.DownloadFailure), lblDocumentName), True, EP, lblMessage)
        End If
    End Sub

    Private Sub btnDocumentUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDocumentUpdate.Click
        Dim doc As EnforcementDocument = EnforcementDocumentFromFileListRow(dgvDocumentList.CurrentRow)
        doc.Comment = txtDocumentDescription.Text
        Dim updated As Boolean = UpdateEnforcementDocument(doc, Me)
        If updated Then
            DisplayMessage(lblMessage, String.Format(GetDocumentMessage(DocumentMessageType.UpdateSuccess), doc.FileName))
            LoadDocuments()
        Else
            DisplayMessage(lblMessage, String.Format(GetDocumentMessage(DocumentMessageType.UpdateFailure), lblDocumentName), True, EP)
        End If
    End Sub

    Private Function EnforcementDocumentFromFileListRow(ByVal row As DataGridViewRow) As EnforcementDocument
        Dim doc As New EnforcementDocument
        With doc
            .EnforcementNumber = row.Cells("EnforcementNumber").Value
            .BinaryFileId = row.Cells("BinaryFileId").Value
            .Comment = row.Cells("Comment").Value
            .DocumentId = row.Cells("DocumentId").Value
            .DocumentType = row.Cells("DocumentType").Value
            .DocumentTypeId = row.Cells("DocumentTypeId").Value
            .FileName = row.Cells("FileName").Value
            .FileSize = row.Cells("FileSize").Value
            .UploadDate = DateTime.Parse(row.Cells("UploadDate").Value)
        End With
        Return doc
    End Function

#End Region

#Region "Accept Button"

    Private Sub NoAcceptButton(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtDocumentDescription.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub FileProperties_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtDocumentDescription.Enter
        Me.AcceptButton = btnDocumentUpdate
    End Sub

#End Region

#End Region ' End Document uploads Region

    Private Sub SSCPEnforcementAudit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            txtEnforcementNumber.Text = Me.EnforcementNumber

            ParseParameters()

            LoadDefaults()
            LoadCombos()

            btnSubmitEnforcementToEPA.Visible = False
            btnManuallyEnterAFS.Visible = False
            cboStaffResponsible.SelectedValue = UserGCode

            LoadEnforcement()
            If txtStipulatedKey.Text <> "" Then
                LoadStipulatedPenalties()
            End If
            ClearStipulatedPenaltyForm()
            LoadEnforcementInfo()

            If AccountFormAccess(48, 3) = "1" Or AccountFormAccess(22, 3) = "1" Then
                DTPEnforcementResolved.Enabled = True
            Else
                DTPEnforcementResolved.Enabled = False
            End If

            SetUserPermissions()

            If AccountFormAccess(48, 2) = "1" Or AccountFormAccess(48, 3) = "1" Or AccountFormAccess(48, 4) = "1" Then
                CheckOpenStatus()
            End If

            If TCEnforcement.TabPages.Contains(TPAuditHistory) Then
                TCEnforcement.TabPages.Remove(TPAuditHistory)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#Region "Page Load Functions"

    Private Sub ParseParameters()
        If Parameters IsNot Nothing Then
            If Parameters.ContainsKey("airsnumber") Then txtAIRSNumber.Text = Parameters("airsnumber")
            If Parameters.ContainsKey("trackingnumber") Then txtTrackingNumber.Text = Parameters("trackingnumber")
        End If
    End Sub

    Private Sub LoadEnforcementInfo()
        EnforcementInfo = Nothing
        Dim enfNum As String = txtEnforcementNumber.Text
        If EnforcementExists(enfNum) Then
            EnforcementInfo = GetEnforcementInfo(enfNum)
            LoadDocuments()
        End If
    End Sub

    Sub LoadDefaults()
        Try
            '  TCEnforcement.TabPages.Remove(TPGeneralInfo)
            TCEnforcement.TabPages.Remove(TPLON)
            TCEnforcement.TabPages.Remove(TPNOV)
            TCEnforcement.TabPages.Remove(TPCO)
            TCEnforcement.TabPages.Remove(TPAO)
            'TCEnforcement.TabPages.Remove(TPPollutants)

            ' TCEnforcement.TabPages.Add(TPGeneralInfo)

            DTPDiscoveryDate.Text = OracleDate
            DTPDiscoveryDate.Checked = False
            DTPDayZero.Text = OracleDate
            DTPDayZero.Checked = False
            DTPEnforcementResolved.Text = OracleDate
            DTPEnforcementResolved.Checked = False
            DTPLONToUC.Text = OracleDate
            DTPLONToUC.Checked = False
            DTPLONSent.Text = OracleDate
            DTPLONSent.Checked = False
            DTPLONResolved.Text = OracleDate
            DTPLONResolved.Checked = False
            DTPNOVToUC.Text = OracleDate
            DTPNOVToUC.Checked = False
            DTPNOVToPM.Text = OracleDate
            DTPNOVToPM.Checked = False
            DTPNOVsent.Text = OracleDate
            DTPNOVsent.Checked = False
            DTPNOVResponseReceived.Text = OracleDate
            DTPNOVResponseReceived.Checked = False
            DTPNFAToUC.Text = OracleDate
            DTPNFAToUC.Checked = False
            DTPNFAToPM.Text = OracleDate
            DTPNFAToPM.Checked = False
            DTPNFALetterSent.Text = OracleDate
            DTPNFALetterSent.Checked = False
            DTPCOToUC.Text = OracleDate
            DTPCOToUC.Checked = False
            DTPCOToPM.Text = OracleDate
            DTPCOToPM.Checked = False
            DTPCOProposed.Text = OracleDate
            DTPCOProposed.Checked = False
            DTPCOReceivedfromCompany.Text = OracleDate
            DTPCOReceivedfromCompany.Checked = False
            DTPCOReceivedfromDirector.Text = OracleDate
            DTPCOReceivedfromDirector.Checked = False
            DTPCOExecuted.Text = OracleDate
            DTPCOExecuted.Checked = False
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
    Sub LoadCombos()
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
            "from " & DBNameSpace & ".LookUpComplianceStatus "

            SQL2 = "select " & _
            "strHPVCode, " & _
            "(strHPVCode || ' - ' || strHPVViolationDesc) as HPVViolationDesc " & _
            "from " & DBNameSpace & ".LookUPHPVViolations "

            'SQL3 = "Select distinct(numUserID), " & _
            '"(strLastName|| ', '||strFirstName) as StaffName, " & _
            '"strLastName " & _
            '"from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".SSCP_Enforcement " & _
            '"where numProgram = '4' " & _
            '"or numUserID = numStaffResponsible " & _
            '"or (numBranch = '5' " & _
            '"and strLastName = 'District') " & _
            '"order by strLastName "

            SQL3 = "select numuserID, Staff as StaffName " & _
            "from AIRBranch.VW_ComplianceStaff "

            dsComplianceStatus = New DataSet
            dsHPV = New DataSet
            dsStaff = New DataSet

            daComplianceStatus = New OracleDataAdapter(SQL, CurrentConnection)
            daHPV = New OracleDataAdapter(SQL2, CurrentConnection)
            daStaff = New OracleDataAdapter(SQL3, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daComplianceStatus.Fill(dsComplianceStatus, "ComplianceStatus")
            daHPV.Fill(dsHPV, "HPV")
            daStaff.Fill(dsStaff, "Staff")

            If CurrentConnection.State = ConnectionState.Open Then
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
            If AccountFormAccess(48, 3) = "1" Or AccountFormAccess(48, 2) = "1" Then
                btnLinkEnforcement.Enabled = True
                DTPDiscoveryDate.Enabled = True
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
                DTPLONToUC.Enabled = True
                DTPLONSent.Enabled = True
                DTPLONResolved.Enabled = True
                txtLONComments.ReadOnly = False
                DTPNOVToUC.Enabled = True
                DTPNOVToPM.Enabled = True
                DTPNOVsent.Enabled = True
                DTPNOVResponseReceived.Enabled = True
                DTPNFAToUC.Enabled = True
                DTPNFAToPM.Enabled = True
                DTPNFALetterSent.Enabled = True
                txtNOVComments.ReadOnly = False
                DTPCOToUC.Enabled = True
                DTPCOToPM.Enabled = True
                DTPCOProposed.Enabled = True
                DTPCOReceivedfromCompany.Enabled = True
                DTPCOReceivedfromDirector.Enabled = True
                DTPCOExecuted.Enabled = True
                txtCONumber.ReadOnly = False
                DTPCOResolved.Enabled = True
                txtCOPenaltyAmount.ReadOnly = False
                txtPenaltyComments.ReadOnly = False
                txtCOComments.ReadOnly = False
                txtStipulatedPenalty.ReadOnly = False
                SaveStipulatedPenaltyButton.Enabled = True
                ClearStipulatedPenaltyFormButton.Enabled = True
                txtStipulatedComments.ReadOnly = False
                dgvStipulatedPenalties.Enabled = True
                DTPAOExecuted.Enabled = True
                DTPAOAppealed.Enabled = True
                DTPAOResolved.Enabled = True
                txtAOComments.ReadOnly = False
                tsbSave.Enabled = True
                mmiSave.Enabled = True
            Else
                btnLinkEnforcement.Enabled = False
                DTPDiscoveryDate.Enabled = False
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
                DTPLONToUC.Enabled = False
                DTPLONSent.Enabled = False
                DTPLONResolved.Enabled = False
                txtLONComments.ReadOnly = True
                DTPNOVToUC.Enabled = False
                DTPNOVToPM.Enabled = False
                DTPNOVsent.Enabled = False
                DTPNOVResponseReceived.Enabled = False
                DTPNFAToUC.Enabled = False
                DTPNFAToPM.Enabled = False
                DTPNFALetterSent.Enabled = False
                txtNOVComments.ReadOnly = True
                DTPCOToUC.Enabled = False
                DTPCOToPM.Enabled = False
                DTPCOProposed.Enabled = False
                DTPCOReceivedfromCompany.Enabled = False
                DTPCOReceivedfromDirector.Enabled = False
                DTPCOExecuted.Enabled = False
                txtCONumber.ReadOnly = True
                DTPCOResolved.Enabled = False
                txtCOPenaltyAmount.ReadOnly = True
                txtPenaltyComments.ReadOnly = True
                txtCOComments.ReadOnly = True
                txtStipulatedPenalty.ReadOnly = True
                SaveStipulatedPenaltyButton.Enabled = False
                ClearStipulatedPenaltyFormButton.Enabled = False
                txtStipulatedComments.ReadOnly = True
                dgvStipulatedPenalties.Enabled = False
                DTPAOExecuted.Enabled = False
                DTPAOAppealed.Enabled = False
                DTPAOResolved.Enabled = False
                txtAOComments.ReadOnly = True
                tsbSave.Enabled = False
                mmiSave.Enabled = False
                mmiSave.Visible = False
                tsbSave.Visible = False
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
                DTPDiscoveryDate.Enabled = False
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
                DTPLONToUC.Enabled = False
                DTPLONSent.Enabled = False
                DTPLONResolved.Enabled = False
                txtLONComments.ReadOnly = True
                DTPNOVToUC.Enabled = False
                DTPNOVToPM.Enabled = False
                DTPNOVsent.Enabled = False
                DTPNOVResponseReceived.Enabled = False
                DTPNFAToUC.Enabled = False
                DTPNFAToPM.Enabled = False
                DTPNFALetterSent.Enabled = False
                txtNOVComments.ReadOnly = True
                DTPCOToUC.Enabled = False
                DTPCOToPM.Enabled = False
                DTPCOProposed.Enabled = False
                DTPCOReceivedfromCompany.Enabled = False
                DTPCOReceivedfromDirector.Enabled = False
                DTPCOExecuted.Enabled = False
                txtCONumber.ReadOnly = True
                DTPCOResolved.Enabled = False
                txtCOPenaltyAmount.ReadOnly = True
                txtPenaltyComments.ReadOnly = True
                txtCOComments.ReadOnly = True
                txtStipulatedPenalty.ReadOnly = True
                SaveStipulatedPenaltyButton.Enabled = False
                ClearStipulatedPenaltyFormButton.Enabled = False
                txtStipulatedComments.ReadOnly = True
                dgvStipulatedPenalties.Enabled = False
                DTPAOExecuted.Enabled = False
                DTPAOAppealed.Enabled = False
                DTPAOResolved.Enabled = False
                txtAOComments.ReadOnly = True
                tsbSave.Enabled = False
                mmiSave.Enabled = False
                mmiSave.Visible = False
                tsbSave.Visible = False
                mmiDelete.Visible = False

                If AccountFormAccess(48, 3) = "1" Then
                    DTPEnforcementResolved.Enabled = True
                End If
            Else
                If AccountFormAccess(48, 4) = "1" And AccountFormAccess(4, 4) = "0" And Not UserAccounts.Contains("(114)") Then 'District
                    btnEditAirProgramPollutants.Enabled = False
                    cboPollutantStatus.Enabled = False
                    btnSubmitEnforcementToEPA.Enabled = False
                    btnManuallyEnterAFS.Enabled = False

                    chbLON.Enabled = True
                    chbNOV.Enabled = False
                    chbCO.Enabled = False
                    chbAO.Enabled = False
                    chbHPV.Enabled = False
                    cboHPVType.Enabled = False
                    btnLinkEnforcement.Enabled = True
                    'btnSubmitToUC.Enabled = False
                    DTPDayZero.Enabled = False

                    DTPDiscoveryDate.Enabled = True
                    DTPDayZero.Enabled = True
                    cboStaffResponsible.Enabled = True
                    txtGeneralComments.ReadOnly = False
                    btn45DayZero.Visible = False
                    SaveStipulatedPenaltyButton.Visible = False
                    UpdateStipulatedPenaltyButton.Visible = False
                    ClearStipulatedPenaltyFormButton.Visible = False
                    'btnUploadCO.Visible = False
                    'btnDownloadCO.Visible = False
                    'btnUploadCO.Visible = False

                    DTPLONToUC.Enabled = True
                    DTPLONSent.Enabled = True
                    DTPLONResolved.Enabled = True
                    txtLONComments.ReadOnly = False
                    DTPNOVToUC.Enabled = False
                    DTPNOVToPM.Enabled = False
                    DTPNOVsent.Enabled = False
                    DTPNOVResponseReceived.Enabled = False
                    DTPNFAToUC.Enabled = False
                    DTPNFAToPM.Enabled = False
                    DTPNFALetterSent.Enabled = False
                    txtNOVComments.ReadOnly = True
                    DTPCOToUC.Enabled = False
                    DTPCOToPM.Enabled = False
                    DTPCOProposed.Enabled = False
                    DTPCOReceivedfromCompany.Enabled = False
                    DTPCOReceivedfromDirector.Enabled = False
                    DTPCOExecuted.Enabled = False
                    txtCONumber.ReadOnly = True
                    DTPCOResolved.Enabled = False
                    txtCOPenaltyAmount.ReadOnly = True
                    txtPenaltyComments.ReadOnly = True
                    txtCOComments.ReadOnly = True
                    txtStipulatedPenalty.ReadOnly = True
                    SaveStipulatedPenaltyButton.Enabled = True
                    ClearStipulatedPenaltyFormButton.Enabled = True
                    txtStipulatedComments.ReadOnly = True
                    dgvStipulatedPenalties.Enabled = False
                    DTPAOExecuted.Enabled = False
                    DTPAOAppealed.Enabled = False
                    DTPAOResolved.Enabled = False
                    txtAOComments.ReadOnly = True
                    tsbSave.Visible = True
                    tsbSave.Enabled = True
                    mmiSave.Visible = True
                    mmiSave.Enabled = True

                Else
                    If AccountFormAccess(48, 2) = "1" Or AccountFormAccess(48, 3) = "1" Or AccountFormAccess(48, 4) = "1" Then
                        btnLinkEnforcement.Enabled = True
                        DTPDiscoveryDate.Enabled = True
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
                        DTPLONToUC.Enabled = True
                        DTPLONSent.Enabled = True
                        DTPLONResolved.Enabled = True
                        txtLONComments.ReadOnly = False
                        DTPNOVToUC.Enabled = True
                        DTPNOVToPM.Enabled = True
                        DTPNOVsent.Enabled = True
                        DTPNOVResponseReceived.Enabled = True
                        DTPNFAToUC.Enabled = True
                        DTPNFAToPM.Enabled = True
                        DTPNFALetterSent.Enabled = True
                        txtNOVComments.ReadOnly = False
                        DTPCOToUC.Enabled = True
                        DTPCOToPM.Enabled = True
                        DTPCOProposed.Enabled = True
                        DTPCOReceivedfromCompany.Enabled = True
                        DTPCOReceivedfromDirector.Enabled = True
                        DTPCOExecuted.Enabled = True
                        txtCONumber.ReadOnly = False
                        DTPCOResolved.Enabled = True
                        txtCOPenaltyAmount.ReadOnly = False
                        txtPenaltyComments.ReadOnly = False
                        txtCOComments.ReadOnly = False
                        txtStipulatedPenalty.ReadOnly = False
                        SaveStipulatedPenaltyButton.Enabled = True
                        ClearStipulatedPenaltyFormButton.Enabled = True
                        txtStipulatedComments.ReadOnly = False
                        dgvStipulatedPenalties.Enabled = True
                        DTPAOExecuted.Enabled = True
                        DTPAOAppealed.Enabled = True
                        DTPAOResolved.Enabled = True
                        txtAOComments.ReadOnly = False
                        tsbSave.Visible = True
                        tsbSave.Enabled = True
                        mmiSave.Visible = True
                        mmiSave.Enabled = True
                    End If
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
            Dim TrackingNumber As String = ""
            Dim AIRSNumber As String = ""
            Dim EnforcementFinalized As String = ""
            Dim StaffResponsible As String = ""
            Dim Status As String = ""
            Dim ActionType As String = ""
            Dim GeneralComments As String = ""
            Dim DiscoveryDate As String = ""
            Dim DayZero As String = ""
            Dim HPV As String = ""
            Dim Pollutants As String = ""
            Dim PollutantStatus As String = ""
            Dim LONToUC As String = ""
            Dim LONSent As String = ""
            Dim LONResolved As String = ""
            Dim LONComments As String = ""
            Dim LONResolvedEnforcement As String = ""
            Dim NOVToUC As String = ""
            Dim NOVTOPM As String = ""
            Dim NOVSent As String = ""
            Dim NOVResponseReceived As String = ""
            Dim NFAToUC As String = ""
            Dim NFAToPM As String = ""
            Dim NFALetterSent As String = ""
            Dim NOVComments As String = ""
            Dim NOVResolvedEnforcement As String = ""
            Dim COToUC As String = ""
            Dim COToPM As String = ""
            Dim COProposed As String = ""
            Dim COReceivedFromCompany As String = ""
            Dim COReceivedFromDirector As String = ""
            Dim COExecuted As String = ""
            Dim CONumber As String = ""
            Dim COResolved As String = ""
            Dim COPenaltyAmount As String = ""
            Dim COPenaltyComments As String = ""
            Dim COComments As String = ""
            Dim Stipulated As String = ""
            Dim COResolvedEnforcement As String = ""
            Dim AOExecuted As String = ""
            Dim AOAppealed As String = ""
            Dim AOComments As String = ""
            Dim AOResolved As String = ""
            Dim AFSKeyActionNumber As String = ""
            Dim AFSNOVSentNumber As String = ""
            Dim AFSNOVResolvedNumber As String = ""
            Dim AFSCOProposedNumber As String = ""
            Dim AFSCOExecutedNumber As String = ""
            Dim AFSCOResolvedNumber As String = ""
            Dim AFSAOtoAGNumber As String = ""
            Dim AFSCivilCourtNumber As String = ""
            Dim AFSAOResolvedNumber As String = ""
            Dim ModifingPerson As String = ""
            Dim ModifingDate As String = ""

            If txtEnforcementNumber.Text <> "" And txtEnforcementNumber.Text <> "N/A" Then
                SQL = "Select * " & _
                "from " & DBNameSpace & ".SSCP_AuditedEnforcement " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strTrackingNumber")) Then
                        TrackingNumber = ""
                    Else
                        TrackingNumber = dr.Item("strTrackingNumber")
                    End If
                    If IsDBNull(dr.Item("strAIRSNumber")) Then
                        AIRSNumber = ""
                    Else
                        AIRSNumber = dr.Item("strAIRSNumber")
                    End If
                    If IsDBNull(dr.Item("strEnforcementFinalized")) Then
                        EnforcementFinalized = ""
                    Else
                        If dr.Item("strEnforcementFinalized") = "True" Then
                            If IsDBNull(dr.Item("datEnforcementFinalized")) Then
                                EnforcementFinalized = ""
                            Else
                                EnforcementFinalized = dr.Item("datEnforcementFinalized")
                            End If
                        Else
                            EnforcementFinalized = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("numStaffResponsible")) Then
                        StaffResponsible = ""
                    Else
                        StaffResponsible = dr.Item("numStaffresponsible")
                    End If
                    If IsDBNull(dr.Item("strStatus")) Then
                        Status = ""
                    Else
                        Status = dr.Item("strStatus")
                    End If
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
                    If IsDBNull(dr.Item("strDiscoveryDate")) Then
                        DiscoveryDate = ""
                    Else
                        If dr.Item("strDiscoveryDate") = "True" Then
                            If IsDBNull(dr.Item("datDiscoveryDate")) Then
                                DiscoveryDate = ""
                            Else
                                DiscoveryDate = dr.Item("datDiscoveryDate")
                            End If
                        Else
                            DiscoveryDate = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strDayZero")) Then
                        DayZero = ""
                    Else
                        If dr.Item("strDayZero") = "True" Then
                            If IsDBNull(dr.Item("datDayZero")) Then
                                DayZero = ""
                            Else
                                DayZero = dr.Item("datDayZero")
                            End If
                        Else
                            DayZero = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strHPV")) Then
                        HPV = ""
                    Else
                        HPV = dr.Item("strHPV")
                    End If
                    If IsDBNull(dr.Item("strPollutants")) Then
                        Pollutants = ""
                    Else
                        Pollutants = dr.Item("strPollutants")
                    End If
                    If IsDBNull(dr.Item("strPollutantStatus")) Then
                        PollutantStatus = ""
                    Else
                        PollutantStatus = dr.Item("strPollutantStatus")
                    End If
                    If IsDBNull(dr.Item("strLONtoUC")) Then
                        LONToUC = ""
                    Else
                        If dr.Item("strLONtoUC") = "True" Then
                            LONToUC = dr.Item("datLONtoUC")
                        Else
                            LONToUC = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strLONSent")) Then
                        LONSent = ""
                    Else
                        If dr.Item("strLONSent") = "True" Then
                            LONSent = dr.Item("datLONSent")
                        Else
                            LONSent = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strLONResolved")) Then
                        LONResolved = ""
                    Else
                        If dr.Item("strLONResolved") = "True" Then
                            LONResolved = dr.Item("datLONResolved")
                        Else
                            LONResolved = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strLONComments")) Then
                        LONComments = ""
                    Else
                        LONComments = dr.Item("strLONComments")
                    End If
                    If IsDBNull(dr.Item("strLONResolvedEnforcement")) Then
                        LONResolvedEnforcement = ""
                    Else
                        LONResolvedEnforcement = dr.Item("strLONResolvedEnforcement")
                    End If
                    If IsDBNull(dr.Item("strNOVtoUC")) Then
                        NOVToUC = ""
                    Else
                        If dr.Item("strNOVtoUC") = "True" Then
                            NOVToUC = dr.Item("datNOVtoUC")
                        Else
                            NOVToUC = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strNOVtoPM")) Then
                        NOVTOPM = ""
                    Else
                        If dr.Item("strNOVtoPM") = "True" Then
                            NOVTOPM = dr.Item("datNOVtoPM")
                        Else
                            NOVTOPM = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strNOVSent")) Then
                        NOVSent = ""
                    Else
                        If dr.Item("strNOVSent") = "True" Then
                            NOVSent = dr.Item("datNOVSent")
                        Else
                            NOVSent = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strNOVResponseReceived")) Then
                        NOVResponseReceived = ""
                    Else
                        If dr.Item("strNOVResponseReceived") = "True" Then
                            NOVResponseReceived = dr.Item("datNOVResponseReceived")
                        Else
                            NOVResponseReceived = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strNFAtoUC")) Then
                        NFAToUC = ""
                    Else
                        If dr.Item("strNFAtoUC") = "True" Then
                            NFAToUC = dr.Item("datNFAtoUC")
                        Else
                            NFAToUC = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strNFAtoPM")) Then
                        NFAToPM = ""
                    Else
                        If dr.Item("strNFAtoPM") = "True" Then
                            NFAToPM = dr.Item("datNFAtoPM")
                        Else
                            NFAToPM = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strNFALetterSent")) Then
                        NFALetterSent = ""
                    Else
                        If dr.Item("strNFALetterSent") = "True" Then
                            NFALetterSent = dr.Item("datNFALetterSent")
                        Else
                            NFALetterSent = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strNOVComment")) Then
                        NOVComments = ""
                    Else
                        NOVComments = dr.Item("strNOVComment")
                    End If
                    If IsDBNull(dr.Item("strNOVResolvedEnforcement")) Then
                        NOVResolvedEnforcement = ""
                    Else
                        NOVResolvedEnforcement = dr.Item("strNOVResolvedEnforcement")
                    End If
                    If IsDBNull(dr.Item("strCOtoUC")) Then
                        COToUC = ""
                    Else
                        If dr.Item("strCOtoUC") = "True" Then
                            COToUC = dr.Item("datCOtoUC")
                        Else
                            COToUC = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strCOtoPM")) Then
                        COToPM = ""
                    Else
                        If dr.Item("strCOtoPM") = "True" Then
                            COToPM = dr.Item("datCOtoPM")
                        Else
                            COToPM = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strCOProposed")) Then
                        COProposed = ""
                    Else
                        If dr.Item("strCOProposed") = "True" Then
                            COProposed = dr.Item("datCOProposed")
                        Else
                            COProposed = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strCOReceivedFromCompany")) Then
                        COReceivedFromCompany = ""
                    Else
                        If dr.Item("strCOReceivedFromCompany") = "True" Then
                            COReceivedFromCompany = dr.Item("datCOReceivedFromCompany")
                        Else
                            COReceivedFromCompany = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strCOReceivedFromDirector")) Then
                        COReceivedFromDirector = ""
                    Else
                        If dr.Item("strCOReceivedFromDirector") = "True" Then
                            COReceivedFromDirector = dr.Item("datCOReceivedFromDirector")
                        Else
                            COReceivedFromDirector = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strCOExecuted")) Then
                        COExecuted = ""
                    Else
                        If dr.Item("strCOExecuted") = "True" Then
                            COExecuted = dr.Item("datCOExecuted")
                        Else
                            COExecuted = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strCONumber")) Then
                        CONumber = ""
                    Else
                        CONumber = dr.Item("strCONumber")
                    End If
                    If IsDBNull(dr.Item("strCOResolved")) Then
                        COResolved = ""
                    Else
                        If dr.Item("strCOResolved") = "True" Then
                            COResolved = dr.Item("datCOResolved")
                        Else
                            COResolved = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("STRCOPENALTYAMOUNT")) Then
                        COPenaltyAmount = ""
                    Else
                        COPenaltyAmount = dr.Item("STRCOPENALTYAMOUNT")
                    End If
                    If IsDBNull(dr.Item("strCOPenaltyAmountComments")) Then
                        COPenaltyComments = ""
                    Else
                        COPenaltyComments = dr.Item("strCOPenaltyAmountComments")
                    End If
                    If IsDBNull(dr.Item("strCOComment")) Then
                        COComments = ""
                    Else
                        COComments = dr.Item("strCOComment")
                    End If
                    If IsDBNull(dr.Item("strStipulatedPenalty")) Then
                        Stipulated = ""
                    Else
                        Stipulated = dr.Item("strStipulatedPenalty")
                    End If
                    If IsDBNull(dr.Item("strCOResolvedEnforcement")) Then
                        COResolvedEnforcement = ""
                    Else
                        COResolvedEnforcement = dr.Item("strCOResolvedEnforcement")
                    End If
                    If IsDBNull(dr.Item("strAOExecuted")) Then
                        AOExecuted = ""
                    Else
                        If dr.Item("strAOExecuted") = "True" Then
                            AOExecuted = dr.Item("datAOExecuted")
                        Else
                            AOExecuted = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strAOAppealed")) Then
                        AOAppealed = ""
                    Else
                        If dr.Item("strAOAppealed") = "True" Then
                            AOAppealed = dr.Item("datAOAppealed")
                        Else
                            AOAppealed = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strAOResolved")) Then
                        AOResolved = ""
                    Else
                        If dr.Item("strAOResolved") = "True" Then
                            AOResolved = dr.Item("datAOResolved")
                        Else
                            AOResolved = ""
                        End If
                    End If
                    If IsDBNull(dr.Item("strAOComment")) Then
                        AOComments = ""
                    Else
                        AOComments = dr.Item("strAOComment")
                    End If
                    If IsDBNull(dr.Item("strAFSKeyActionNumber")) Then
                        AFSKeyActionNumber = ""
                    Else
                        AFSKeyActionNumber = dr.Item("strAFSKeyActionNumber")
                    End If
                    If IsDBNull(dr.Item("strAFSNOVSentNumber")) Then
                        AFSNOVSentNumber = ""
                    Else
                        AFSNOVSentNumber = dr.Item("strAFSNOVSentNumber")
                    End If
                    If IsDBNull(dr.Item("strAFSNOVResolvedNumber")) Then
                        AFSNOVResolvedNumber = ""
                    Else
                        AFSNOVResolvedNumber = dr.Item("strAFSNOVResolvedNumber")
                    End If
                    If IsDBNull(dr.Item("strAFSCOProposedNumber")) Then
                        AFSCOProposedNumber = ""
                    Else
                        AFSCOProposedNumber = dr.Item("strAFSCOProposedNumber")
                    End If
                    If IsDBNull(dr.Item("strAFSCOExecutedNumber")) Then
                        AFSCOExecutedNumber = ""
                    Else
                        AFSCOExecutedNumber = dr.Item("strAFSCOExecutedNumber")
                    End If
                    If IsDBNull(dr.Item("strAFSCOResolvedNumber")) Then
                        AFSCOResolvedNumber = ""
                    Else
                        AFSCOResolvedNumber = dr.Item("strAFSCOResolvedNumber")
                    End If
                    If IsDBNull(dr.Item("strAFSAOtoAGnumber")) Then
                        AFSAOtoAGNumber = ""
                    Else
                        AFSAOtoAGNumber = dr.Item("strAFSAOtoAGNumber")
                    End If
                    If IsDBNull(dr.Item("strAFSCivilCourtNumber")) Then
                        AFSCivilCourtNumber = ""
                    Else
                        AFSCivilCourtNumber = dr.Item("strAFSCivilCourtNumber")
                    End If
                    If IsDBNull(dr.Item("strAFSAOResolvedNumber")) Then
                        AFSAOResolvedNumber = ""
                    Else
                        AFSAOResolvedNumber = dr.Item("strAFSAOResolvedNumber")
                    End If
                    If IsDBNull(dr.Item("strModifingPerson")) Then
                        ModifingPerson = ""
                    Else
                        ModifingPerson = dr.Item("strModifingPerson")
                    End If
                    If IsDBNull(dr.Item("datModifingDate")) Then
                        ModifingDate = ""
                    Else
                        ModifingDate = dr.Item("datModifingDate")
                    End If
                End While
                dr.Close()
            Else
                If txtTrackingNumber.Text <> "" Then
                    TrackingNumber = txtTrackingNumber.Text
                End If
            End If

            If TrackingNumber <> "" Then
                txtTrackingNumber.Text = TrackingNumber
            Else
                txtTrackingNumber.Clear()
            End If
            If AIRSNumber <> "" Then
                txtAIRSNumber.Text = Mid(AIRSNumber, 5)
                LoadFacilityInfo()
            Else
                '  txtAIRSNumber.Clear()
            End If
            If EnforcementFinalized <> "" Then
                DTPEnforcementResolved.Text = EnforcementFinalized
                DTPEnforcementResolved.Checked = True
            Else
                DTPEnforcementResolved.Text = OracleDate
                DTPEnforcementResolved.Checked = False
            End If
            If StaffResponsible <> "" Then
                cboStaffResponsible.SelectedValue = StaffResponsible
            Else
                cboStaffResponsible.SelectedValue = UserGCode
            End If

            If NOVSent <> "" Or NOVToUC <> "" Or NOVTOPM <> "" _
                    Or NOVResponseReceived <> "" Or NOVResolvedEnforcement <> "" Or NOVComments <> "" _
                    Or NFALetterSent <> "" Or NFAToUC <> "" Or NFAToPM <> "" Then
                chbNOV.Checked = True
            Else
                chbNOV.Checked = False
            End If
            If COExecuted <> "" Or COToUC <> "" Or COToPM <> "" _
                         Or COProposed <> "" Or COReceivedFromCompany <> "" Or COReceivedFromDirector <> "" _
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
            If ActionType <> "" Then
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
            End If
            If GeneralComments <> "" Then
                txtGeneralComments.Text = GeneralComments
            Else
                txtGeneralComments.Clear()
            End If
            If DiscoveryDate <> "" Then
                DTPDiscoveryDate.Text = DiscoveryDate
                DTPDiscoveryDate.Checked = True
            Else
                DTPDiscoveryDate.Text = OracleDate
                DTPDiscoveryDate.Checked = False
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
            If LONToUC <> "" Then
                DTPLONToUC.Text = LONToUC
                DTPLONToUC.Checked = True
            Else
                DTPLONToUC.Text = OracleDate
                DTPLONToUC.Checked = False
            End If
            If LONSent <> "" Then
                DTPLONSent.Text = LONSent
                DTPLONSent.Checked = True
            Else
                DTPLONSent.Text = OracleDate
                DTPLONSent.Checked = False
            End If
            If LONResolved <> "" Then
                DTPLONResolved.Text = LONResolved
                DTPLONResolved.Checked = True
            Else
                DTPLONResolved.Text = OracleDate
                DTPLONResolved.Checked = False
            End If
            If LONComments <> "" Then
                txtLONComments.Text = LONComments
            Else
                txtLONComments.Clear()
            End If
            If LONResolvedEnforcement <> "" Then

            End If
            If NOVToUC <> "" Then
                DTPNOVToUC.Text = NOVToUC
                DTPNOVToUC.Checked = True
            Else
                DTPNOVToUC.Text = OracleDate
                DTPNOVToUC.Checked = False
            End If
            If NOVTOPM <> "" Then
                DTPNOVToPM.Text = NOVTOPM
                DTPNOVToPM.Checked = True
            Else
                DTPNOVToPM.Text = OracleDate
                DTPNOVToPM.Checked = False
            End If
            If NOVSent <> "" Then
                DTPNOVsent.Text = NOVSent
                DTPNOVsent.Checked = True
            Else
                DTPNOVsent.Text = OracleDate
                DTPNOVsent.Checked = False
            End If
            If NOVResponseReceived <> "" Then
                DTPNOVResponseReceived.Text = NOVResponseReceived
                DTPNOVResponseReceived.Checked = True
            Else
                DTPNOVResponseReceived.Text = OracleDate
                DTPNOVResponseReceived.Checked = False
            End If
            If NFAToUC <> "" Then
                DTPNFAToUC.Text = NFAToUC
                DTPNFAToUC.Checked = True
            Else
                DTPNFAToUC.Text = OracleDate
                DTPNFAToUC.Checked = False
            End If
            If NFAToPM <> "" Then
                DTPNFAToPM.Text = NFAToPM
                DTPNFAToPM.Checked = True
            Else
                DTPNFAToPM.Text = OracleDate
                DTPNFAToPM.Checked = False
            End If
            If NFALetterSent <> "" Then
                DTPNFALetterSent.Text = NFALetterSent
                DTPNFALetterSent.Checked = True
            Else
                DTPNFALetterSent.Text = OracleDate
                DTPNFALetterSent.Checked = False
            End If
            If NOVComments <> "" Then
                txtNOVComments.Text = NOVComments
            Else
                txtNOVComments.Clear()
            End If
            If COToUC <> "" Then
                DTPCOToUC.Text = COToUC
                DTPCOToUC.Checked = True
            Else
                DTPCOToUC.Text = OracleDate
                DTPCOToUC.Checked = False
            End If
            If COToPM <> "" Then
                DTPCOToPM.Text = COToPM
                DTPCOToPM.Checked = True
            Else
                DTPCOToPM.Text = OracleDate
                DTPCOToPM.Checked = False
            End If
            If COProposed <> "" Then
                DTPCOProposed.Text = COProposed
                DTPCOProposed.Checked = True
            Else
                DTPCOProposed.Text = OracleDate
                DTPCOProposed.Checked = False
            End If
            If COReceivedFromCompany <> "" Then
                DTPCOReceivedfromCompany.Text = COReceivedFromCompany
                DTPCOReceivedfromCompany.Checked = True
            Else
                DTPCOReceivedfromCompany.Text = OracleDate
                DTPCOReceivedfromCompany.Checked = False
            End If
            If COReceivedFromDirector <> "" Then
                DTPCOReceivedfromDirector.Text = COReceivedFromDirector
                DTPCOReceivedfromDirector.Checked = True
            Else
                DTPCOReceivedfromDirector.Text = OracleDate
                DTPCOReceivedfromDirector.Checked = False
            End If
            If COExecuted <> "" Then
                DTPCOExecuted.Text = COExecuted
                DTPCOExecuted.Checked = True
            Else
                DTPCOExecuted.Text = OracleDate
                DTPCOExecuted.Checked = False
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
            If COComments <> "" Then
                txtCOComments.Text = COComments
            Else
                txtCOComments.Clear()
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
            If AOComments <> "" Then
                txtAOComments.Text = AOComments
            Else
                txtAOComments.Clear()
            End If
            If AFSKeyActionNumber <> "" Then
                txtAFSKeyActionNumber.Text = AFSKeyActionNumber
                btnSubmitToUC.Visible = False
            Else
                txtAFSKeyActionNumber.Clear()
            End If
            If AFSNOVSentNumber <> "" Then
                txtAFSNOVActionNumber.Text = AFSNOVSentNumber
            Else
                txtAFSNOVActionNumber.Clear()
            End If
            If AFSNOVResolvedNumber <> "" Then
                txtAFSNOVResolvedNumber.Text = AFSNOVResolvedNumber
            Else
                txtAFSNOVResolvedNumber.Clear()
            End If
            If AFSCOProposedNumber <> "" Then
                txtAFSCOProposedActionNumber.Text = AFSCOProposedNumber
            Else
                txtAFSCOProposedActionNumber.Clear()
            End If
            If AFSCOExecutedNumber <> "" Then
                txtAFSCOExecutedActionNumber.Text = AFSCOExecutedNumber
            Else
                txtAFSCOExecutedActionNumber.Clear()
            End If
            If AFSCOResolvedNumber <> "" Then
                txtAFSCOResolvedActionNumber.Text = AFSCOResolvedNumber
            Else
                txtAFSCOResolvedActionNumber.Clear()
            End If
            If AFSAOtoAGNumber <> "" Then
                txtAFSAOToAGActionNumber.Text = AFSAOtoAGNumber
            Else
                txtAFSAOToAGActionNumber.Clear()
            End If
            If AFSAOResolvedNumber <> "" Then
                txtAFSAOResolvedActionNumber.Text = AFSAOResolvedNumber
            Else
                txtAFSAOResolvedActionNumber.Clear()
            End If
            If ModifingDate <> "" Then
                DTPLastSave.Text = ModifingDate
            Else
                DTPLastSave.Text = OracleDate
            End If
            If txtAIRSNumber.Text <> "" Then
                LoadEnforcementPollutants2()
            End If
            If PollutantStatus <> "" Then
                cboPollutantStatus.SelectedValue = PollutantStatus
            Else
                cboPollutantStatus.SelectedValue = "0"
            End If
            If Stipulated <> "" Then
                LoadStipulatedPenalties()
                ' txtStipulatedKey.Text = Stipulated
                txtStipulatedKey.Clear()
            Else
                txtStipulatedKey.Clear()
            End If
            txtSubmitToUC.Text = Status
            If Status <> "" Then
                If AccountFormAccess(48, 2) = "1" Then
                    Select Case Status
                        Case ""
                            btnSubmitToUC.Visible = True
                        Case "UC"
                            btnSubmitToUC.Visible = False
                        Case Else
                            btnSubmitToUC.Visible = True
                    End Select
                End If
            Else
                btnSubmitToUC.Visible = True
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
            If txtEnforcementNumber.Text <> "" And txtEnforcementNumber.Text <> "N/A" Then
                EnforcementNumber = txtEnforcementNumber.Text
            End If
            If txtTrackingNumber.Text <> "" Then
                TrackingNumber = txtTrackingNumber.Text
            End If

            SQL = "Select strFacilityName, strFacilityStreet1, " & _
            "strFacilityCity, strCountyName, strFacilityState, strFacilityZipCode, " & _
            "strClass, strAIRProgramCodes " & _
            "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".LookUpCountyInformation, " & _
            "" & DBNameSpace & ".APBHeaderData " & _
            "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = '0413" & txtAIRSNumber.Text & "' " & _
            "and strCountyCode = '" & Mid(txtAIRSNumber.Text, 1, 3) & "' " & _
            "and " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".APBHeaderData.strairsnumber"

            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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

            lvPollutants.GridLines = True
            lvPollutants.FullRowSelect = True

            If AccountFormAccess(48, 2) = "1" And AccountFormAccess(48, 4) = "1" And AccountFormAccess(48, 3) = "0" Then
                lvPollutants.Columns.Add("", 0, HorizontalAlignment.Left)
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

                If txtEnforcementNumber.Text <> "" And txtEnforcementNumber.Text <> "N/A" Then
                    SQL = "Select " & _
                    "strPollutants " & _
                    "from " & DBNameSpace & ".SSCP_AuditedEnforcement " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
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
            Else
                lvPollutants.CheckBoxes = True

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

                If txtEnforcementNumber.Text <> "" And txtEnforcementNumber.Text <> "N/A" Then
                    SQL = "Select " & _
                    "strPollutants " & _
                    "from " & DBNameSpace & ".SSCP_AuditedEnforcement " & _
                    "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
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
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadStipulatedPenalties()
        Try
            'If txtEnforcementNumber.Text <> "" And txtStipulatedKey.Text <> "" _
            '     And TCEnforcement.TabPages.Contains(TPCO) = True _
            '        And txtEnforcementNumber.Text <> "N/A" Then
            If txtEnforcementNumber.Text <> "" _
              And TCEnforcement.TabPages.Contains(TPCO) = True _
            And txtEnforcementNumber.Text <> "N/A" Then
                SQL = "Select " & _
                "strStipulatedPenalty, " & _
                "Case " & _
                "    When strStipulatedPenaltyComments IS Null THen 'N/A' " & _
                "Else strStipulatedPenaltyComments  " & _
                "End StipulatedPenaltyComments, " & _
                "strAFSStipulatedPenaltyNumber, " & _
                "strEnforcementKey " & _
                "from " & DBNameSpace & ".SSCPENforcementStipulated " & _
                "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' " & _
                "order by strEnforcementKey "

                dsStipulatedPenalty = New DataSet
                daStipulatedPenalty = New OracleDataAdapter(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                daStipulatedPenalty.Fill(dsStipulatedPenalty, "StipulatedPenalty")
                dgvStipulatedPenalties.DataSource = dsStipulatedPenalty
                dgvStipulatedPenalties.DataMember = "StipulatedPenalty"

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
                txtStipulatedPenalitiesActionNumber.Clear()
                For i As Integer = 0 To dgvStipulatedPenalties.RowCount - 1
                    txtStipulatedPenalitiesActionNumber.Text = txtStipulatedPenalitiesActionNumber.Text & _
                    dgvStipulatedPenalties.Item(2, i).Value & ", "
                Next
            Else
                dsStipulatedPenalty = New DataSet
                txtStipulatedPenalitiesActionNumber.Clear()
            End If
            txtStipulatedKey.Clear()

        Catch ex As Exception
            ErrorReport(txtEnforcementNumber.Text & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub OpenChecklist()
        Try
            If txtEnforcementNumber.Text = "" OrElse txtEnforcementNumber.Text = "N/A" Then
                MsgBox("Please save the current enforcement before linking a Discovery Event.", MsgBoxStyle.Exclamation, "SSCP Enforcement")
                Exit Sub
            End If

            Dim parameters As New Dictionary(Of String, String)

            If txtAIRSNumber.Text <> "" Then
                parameters("airsnumber") = txtAIRSNumber.Text
            End If
            If txtEnforcementNumber.Text <> "" And txtEnforcementNumber.Text <> "N/A" Then
                parameters("enforcementnumber") = txtEnforcementNumber.Text
            End If
            parameters("trackingnumber") = txtTrackingNumber.Text

            OpenSingleForm(SSCPEnforcementChecklist, Me.ID, parameters, True)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SaveEnforcement()
        Try
            If SingleFormIsOpen(SSCPEnforcementChecklist) Then
                MsgBox("Please close the linking tool before saving.", MsgBoxStyle.Exclamation, "SSCP Enforcement")
                Exit Sub
            End If

            Dim TrackingNumber As String = ""
            Dim AIRSNumber As String = ""
            Dim EnforcementFinalizedCheck As String = ""
            Dim EnforcementFinalized As String = ""
            Dim StaffResponsible As String = ""
            Dim EnforcementStatus As String = ""
            Dim ActionType As String = ""
            Dim GeneralComments As String = ""
            Dim DiscoveryDateCheck As String = ""
            Dim DiscoveryDate As String = ""
            Dim DayZeroCheck As String = ""
            Dim DayZero As String = ""
            Dim HPVType As String = ""
            Dim Pollutants As String = ""
            Dim PollutantStatus As String = ""
            Dim LONtoUCCheck As String = ""
            Dim LONToUC As String = ""
            Dim LONSentCheck As String = ""
            Dim LonSent As String = ""
            Dim LONResolvedCheck As String = ""
            Dim LONResolved As String = ""
            Dim LONComments As String = ""
            Dim LONResolvedEnforcement As String = ""
            Dim NOVtoUCCheck As String = ""
            Dim NOVToUC As String = ""
            Dim NOVtoPMCheck As String = ""
            Dim NOVToPM As String = ""
            Dim NOVSentCheck As String = ""
            Dim NOVSent As String = ""
            Dim NOVResponseRecieveCheck As String = ""
            Dim NOVResponseReceived As String = ""
            Dim NFAtoUCCheck As String = ""
            Dim NFAToUC As String = ""
            Dim NFAtoPMCheck As String = ""
            Dim NFAToPM As String = ""
            Dim NFALetterSentCheck As String = ""
            Dim NFALetterSent As String = ""
            Dim NOVCommetns As String = ""
            Dim NOVResovledEnforcement As String = ""
            Dim COtoUCCheck As String = ""
            Dim COToUC As String = ""
            Dim COtoPMCheck As String = ""
            Dim COToPM As String = ""
            Dim CoProposedCheck As String = ""
            Dim COProposed As String = ""
            Dim COReceivedCompanyCheck As String = ""
            Dim COReceivedCompany As String = ""
            Dim CORecievedDirectorCheck As String = ""
            Dim COReceivedDirector As String = ""
            Dim COExecutedCheck As String = ""
            Dim COExecuted As String = ""
            Dim CONumber As String = ""
            Dim COResolvedCheck As String = ""
            Dim COResolved As String = ""
            Dim COPenaltyAmount As String = ""
            Dim COPenaltyAmountComments As String = ""
            Dim COComment As String = ""
            Dim COResolvedEnforcement As String = ""
            Dim StipulatedPenalty As String = ""
            Dim AOExecutedCheck As String = ""
            Dim AOExecuted As String = ""
            Dim AOAppealedCheck As String = ""
            Dim AOAppealed As String = ""
            Dim AOResolvedCheck As String = ""
            Dim AOResolved As String = ""
            Dim AOComment As String = ""
            Dim AFSKeyActionNumber As String = ""
            Dim AFSNOVSentNumber As String = ""
            Dim AFSNOVResolvedNumber As String = ""
            Dim AFSCOProposedNumber As String = ""
            Dim AFSCOExecutedNumber As String = ""
            Dim AFSCOResolvedNumber As String = ""
            Dim AFSAOtoAGNumber As String = ""
            Dim AFSCivilCourtNumber As String = ""
            Dim AFSAOResolvedNumber As String = ""
            Dim AirProgram As String = ""
            Dim Pollutant As String = ""


            If AccountFormAccess(48, 2) = "0" And AccountFormAccess(48, 3) = "0" And AccountFormAccess(48, 4) = "0" Then
                MsgBox("You do not have sufficent permission to save Compliance Events.", MsgBoxStyle.Information, "Compliance Events")
                Exit Sub
            Else
                If txtTrackingNumber.Text <> "" Then
                    TrackingNumber = txtTrackingNumber.Text
                Else
                    TrackingNumber = ""
                End If
                If txtAIRSNumber.Text <> "" Then
                    AIRSNumber = txtAIRSNumber.Text
                Else
                    MsgBox("There is no AIRS #. An Enforcement Action cannot be saved without an AIRS #." & _
                           "No Data Saved", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
                If DTPEnforcementResolved.Checked = True Then
                    EnforcementFinalized = Format(DTPEnforcementResolved.Value, "dd-MMM-yyyy")
                    EnforcementFinalizedCheck = "True"
                Else
                    EnforcementFinalized = ""
                    EnforcementFinalizedCheck = "False"
                End If
                StaffResponsible = cboStaffResponsible.SelectedValue
                If StaffResponsible = "" Then
                    StaffResponsible = UserGCode
                End If
                If txtSubmitToUC.Text <> "" Then
                    EnforcementStatus = "UC"
                Else
                    EnforcementStatus = ""
                End If
                If chbLON.Checked = True And chbNOV.Checked = False And chbCO.Checked = False And _
                           chbAO.Checked = False And chbHPV.Checked = False Then
                    ActionType = "LON"
                End If
                If chbNOV.Checked = True And chbCO.Checked = False And chbAO.Checked = False And chbHPV.Checked = False Then
                    ActionType = "NOV"
                End If
                If chbCO.Checked = True And chbAO.Checked = False And chbHPV.Checked = False _
                       And DTPCOExecuted.Checked = False Then
                    ActionType = "NOVCOP"
                End If
                If chbCO.Checked = True And chbAO.Checked = False And chbHPV.Checked = False _
                       And DTPCOExecuted.Checked = True Then
                    ActionType = "NOVCO"
                End If
                If chbAO.Checked = True And chbHPV.Checked = False Then
                    ActionType = "NOVAO"
                End If

                If chbHPV.Checked = True And chbCO.Checked = True And chbAO.Checked = False _
                      And DTPCOExecuted.Checked = False Then
                    ActionType = "HPVCOP"
                End If
                If chbHPV.Checked = True And chbCO.Checked = True And chbAO.Checked = False _
                        And DTPCOExecuted.Checked = True Then
                    ActionType = "HPVCO"
                End If
                If chbHPV.Checked = True And chbAO.Checked = True Then
                    ActionType = "HPVAO"
                End If
                If chbHPV.Checked = True And chbCO.Checked = False And chbAO.Checked = False Then
                    ActionType = "HPV"
                End If
                If txtGeneralComments.Text <> "" Then
                    GeneralComments = txtGeneralComments.Text
                Else
                    GeneralComments = ""
                End If
                If DTPDiscoveryDate.Checked = True Then
                    DiscoveryDate = Format(DTPDiscoveryDate.Value, "dd-MMM-yyyy")
                    DiscoveryDateCheck = "True"
                Else
                    DiscoveryDate = ""
                    DiscoveryDateCheck = "False"
                End If
                If DTPDayZero.Checked = True Then
                    DayZero = Format(DTPDayZero.Value, "dd-MMM-yyyy")
                    DayZeroCheck = "True"
                Else
                    DayZero = ""
                    DayZeroCheck = "False"
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
                If DTPLONToUC.Checked = True Then
                    LONToUC = Format(DTPLONToUC.Value, "dd-MMM-yyyy")
                    LONtoUCCheck = "True"
                Else
                    LONToUC = ""
                    LONtoUCCheck = "False"
                End If
                If DTPLONSent.Checked = True Then
                    LonSent = Format(DTPLONSent.Value, "dd-MMM-yyyy")
                    LONSentCheck = "True"
                Else
                    LonSent = ""
                    LONSentCheck = "False"
                End If
                If DTPLONResolved.Checked = True Then
                    LONResolved = Format(DTPLONResolved.Value, "dd-MMM-yyyy")
                    LONResolvedCheck = "True"
                Else
                    LONResolved = ""
                    LONResolvedCheck = "False"
                End If
                If txtLONComments.Text <> "" Then
                    LONComments = txtLONComments.Text
                Else
                    LONComments = ""
                End If
                LONResolvedEnforcement = ""
                If DTPNOVToUC.Checked = True Then
                    NOVToUC = Format(DTPNOVToUC.Value, "dd-MMM-yyyy")
                    NOVtoUCCheck = "True"
                Else
                    NOVToUC = ""
                    NOVtoUCCheck = "False"
                End If
                If DTPNOVToPM.Checked = True Then
                    NOVToPM = Format(DTPNOVToPM.Value, "dd-MMM-yyyy")
                    NOVtoPMCheck = "True"
                Else
                    NOVToPM = ""
                    NOVtoPMCheck = "False"
                End If
                If DTPNOVsent.Checked = True Then
                    NOVSent = Format(DTPNOVsent.Value, "dd-MMM-yyyy")
                    NOVSentCheck = "True"
                Else
                    NOVSent = ""
                    NOVSentCheck = "False"
                End If
                If DTPNOVResponseReceived.Checked = True Then
                    NOVResponseReceived = Format(DTPNOVResponseReceived.Value, "dd-MMM-yyyy")
                    NOVResponseRecieveCheck = "True"
                Else
                    NOVResponseReceived = ""
                    NOVResponseRecieveCheck = "False"
                End If
                If DTPNFAToUC.Checked = True Then
                    NFAToUC = Format(DTPNFAToUC.Value, "dd-MMM-yyyy")
                    NFAtoUCCheck = "True"
                Else
                    NFAToUC = ""
                    NFAtoUCCheck = "False"
                End If
                If DTPNFAToPM.Checked = True Then
                    NFAToPM = Format(DTPNFAToPM.Value, "dd-MMM-yyyy")
                    NFAtoPMCheck = "True"
                Else
                    NFAToPM = ""
                    NFAtoPMCheck = "False"
                End If
                If DTPNFALetterSent.Checked = True Then
                    NFALetterSent = Format(DTPNFALetterSent.Value, "dd-MMM-yyyy")
                    NFALetterSentCheck = "True"
                Else
                    NFALetterSent = ""
                    NFALetterSentCheck = "False"
                End If
                If txtNOVComments.Text <> "" Then
                    NOVCommetns = txtNOVComments.Text
                Else
                    NOVCommetns = ""
                End If
                If DTPCOToUC.Checked = True Then
                    COToUC = Format(DTPCOToUC.Value, "dd-MMM-yyyy")
                    COtoUCCheck = "True"
                Else
                    COToUC = ""
                    COtoUCCheck = "False"
                End If
                If DTPCOToPM.Checked = True Then
                    COToPM = Format(DTPCOToPM.Value, "dd-MMM-yyyy")
                    COtoPMCheck = "True"
                Else
                    COToPM = ""
                    COtoPMCheck = "False"
                End If
                If DTPCOProposed.Checked = True Then
                    COProposed = Format(DTPCOProposed.Value, "dd-MMM-yyyy")
                    CoProposedCheck = "True"
                Else
                    COProposed = ""
                    CoProposedCheck = "False"
                End If
                If DTPCOReceivedfromCompany.Checked = True Then
                    COReceivedCompany = Format(DTPCOReceivedfromCompany.Value, "dd-MMM-yyyy")
                    COReceivedCompanyCheck = "True"
                Else
                    COReceivedCompany = ""
                    COReceivedCompanyCheck = "False"
                End If
                If DTPCOReceivedfromDirector.Checked = True Then
                    COReceivedDirector = Format(DTPCOReceivedfromDirector.Value, "dd-MMM-yyyy")
                    CORecievedDirectorCheck = "True"
                Else
                    COReceivedDirector = ""
                    CORecievedDirectorCheck = "False"
                End If
                If DTPCOExecuted.Checked = True Then
                    COExecuted = Format(DTPCOExecuted.Value, "dd-MMM-yyyy")
                    COExecutedCheck = "True"
                Else
                    COExecuted = ""
                    COExecutedCheck = "False"
                End If
                If txtCONumber.Text <> "" Then
                    CONumber = txtCONumber.Text
                Else
                    CONumber = ""
                End If
                If DTPCOResolved.Checked = True Then
                    COResolved = Format(DTPCOResolved.Value, "dd-MMM-yyyy")
                    COResolvedCheck = "True"
                Else
                    COResolved = ""
                    COResolvedCheck = "False"
                End If
                If txtCOPenaltyAmount.Text <> "" Then
                    COPenaltyAmount = txtCOPenaltyAmount.Text
                Else
                    COPenaltyAmount = ""
                End If
                If txtPenaltyComments.Text <> "" Then
                    COPenaltyAmountComments = txtPenaltyComments.Text
                Else
                    COPenaltyAmountComments = ""
                End If
                If txtCOComments.Text <> "" Then
                    COComment = txtCOComments.Text
                Else
                    COComment = ""
                End If
                If DTPAOExecuted.Checked = True Then
                    AOExecuted = Format(DTPAOExecuted.Value, "dd-MMM-yyyy")
                    AOExecutedCheck = "True"
                Else
                    AOExecuted = ""
                    AOExecutedCheck = "False"
                End If
                If DTPAOAppealed.Checked = True Then
                    AOAppealed = Format(DTPAOAppealed.Value, "dd-MMM-yyyy")
                    AOAppealedCheck = "True"
                Else
                    AOAppealed = ""
                    AOAppealedCheck = "False"
                End If
                If DTPAOResolved.Checked = True Then
                    AOResolved = Format(DTPAOResolved.Value, "dd-MMM-yyyy")
                    AOResolvedCheck = "True"
                Else
                    AOResolved = ""
                    AOResolvedCheck = "False"
                End If
                If txtAOComments.Text <> "" Then
                    AOComment = txtAOComments.Text
                Else
                    AOComment = ""
                End If

                'For Each row As DataGridViewRow In dgvStipulatedPenalties.Rows
                '    StipulatedPenalty = StipulatedPenalty + CDec(row.Cells(0).Value)
                'Next
                If IsDBNull(dgvStipulatedPenalties.RowCount.ToString) Then
                    StipulatedPenalty = ""
                Else
                    StipulatedPenalty = dgvStipulatedPenalties.RowCount.ToString
                End If
                'If txtStipulatedKey.Text <> "" Then
                '    StipulatedPenalty = txtStipulatedKey.Text
                'Else
                '    StipulatedPenalty = ""
                'End If
                If txtAFSKeyActionNumber.Text <> "" Then
                    AFSKeyActionNumber = txtAFSKeyActionNumber.Text
                Else
                    AFSKeyActionNumber = ""
                End If
                If txtAFSNOVActionNumber.Text <> "" Then
                    AFSNOVSentNumber = txtAFSNOVActionNumber.Text
                Else
                    AFSNOVSentNumber = ""
                End If
                If txtAFSNOVResolvedNumber.Text <> "" Then
                    AFSNOVResolvedNumber = txtAFSNOVResolvedNumber.Text
                Else
                    AFSNOVResolvedNumber = ""
                End If
                If txtAFSCOProposedActionNumber.Text <> "" Then
                    AFSCOProposedNumber = txtAFSCOProposedActionNumber.Text
                Else
                    AFSCOProposedNumber = ""
                End If
                If txtAFSCOExecutedActionNumber.Text <> "" Then
                    AFSCOExecutedNumber = txtAFSCOExecutedActionNumber.Text
                Else
                    AFSCOExecutedNumber = ""
                End If
                If txtAFSCOResolvedActionNumber.Text <> "" Then
                    AFSCOResolvedNumber = txtAFSCOResolvedActionNumber.Text
                Else
                    AFSCOResolvedNumber = ""
                End If
                If txtAFSAOToAGActionNumber.Text <> "" Then
                    AFSAOtoAGNumber = txtAFSAOToAGActionNumber.Text
                Else
                    AFSAOtoAGNumber = ""
                End If
                If txtAFSCivilCourtActionNumber.Text <> "" Then
                    AFSCivilCourtNumber = txtAFSCivilCourtActionNumber.Text
                Else
                    AFSCivilCourtNumber = ""
                End If
                If txtAFSAOResolvedActionNumber.Text <> "" Then
                    AFSAOResolvedNumber = txtAFSAOResolvedActionNumber.Text
                Else
                    AFSAOResolvedNumber = ""
                End If

                If txtEnforcementNumber.Text = "" Or txtEnforcementNumber.Text = "N/A" Then
                    SQL = "Insert into " & DBNameSpace & ".SSCP_Enforcement " & _
                    "values " & _
                    "((select max(ID) + 1 from " & DBNameSpace & ".SSCP_Enforcement), " & _
                    "" & DBNameSpace & ".SSCPEnforcementNumber.nextval, " & _
                    "'" & TrackingNumber & "', '0413" & AIRSNumber & "', " & _
                    "'" & EnforcementFinalizedCheck & "', '" & EnforcementFinalized & "', " & _
                    "'" & StaffResponsible & "', '" & EnforcementStatus & "', " & _
                    "'" & ActionType & "', '" & Replace(GeneralComments, "'", "''") & "', " & _
                    "'" & DiscoveryDateCheck & "', '" & DiscoveryDate & "', " & _
                    "'" & DayZeroCheck & "', '" & DayZero & "', " & _
                    "'" & HPVType & "', '" & Pollutants & "', " & _
                    "'" & PollutantStatus & "',  " & _
                    "'" & LONtoUCCheck & "', '" & LONToUC & "', " & _
                    "'" & LONSentCheck & "', '" & LonSent & "', " & _
                    "'" & LONResolvedCheck & "', '" & LONResolved & "', " & _
                    "'" & Replace(LONComments, "'", "''") & "', '" & LONResolvedEnforcement & "', " & _
                    "'" & NOVtoUCCheck & "', '" & NOVToUC & "', " & _
                    "'" & NOVtoPMCheck & "', '" & NOVToPM & "', " & _
                    "'" & NOVSentCheck & "', '" & NOVSent & "', " & _
                    "'" & NOVResponseRecieveCheck & "', '" & NOVResponseReceived & "', " & _
                    "'" & NFAtoUCCheck & "', '" & NFAToUC & "', " & _
                    "'" & NFAtoPMCheck & "', '" & NFAToPM & "', " & _
                    "'" & NFALetterSentCheck & "', '" & NFALetterSent & "', " & _
                    "'" & Replace(NOVCommetns, "'", "''") & "', '" & NOVResovledEnforcement & "', " & _
                    "'" & COtoUCCheck & "', '" & COToUC & "', " & _
                    "'" & COtoPMCheck & "', '" & COToPM & "', " & _
                    "'" & CoProposedCheck & "', '" & COProposed & "', " & _
                    "'" & COReceivedCompanyCheck & "', '" & COReceivedCompany & "', " & _
                    "'" & CORecievedDirectorCheck & "', '" & COReceivedDirector & "', " & _
                    "'" & COExecutedCheck & "', '" & COExecuted & "', " & _
                    "'" & CONumber & "', " & _
                    "'" & COResolvedCheck & "', '" & COResolved & "', " & _
                    "'" & COPenaltyAmount & "', '" & Replace(COPenaltyAmountComments, "'", "''") & "', " & _
                    "'" & Replace(COComment, "'", "''") & "', '" & StipulatedPenalty.ToString & "', " & _
                    "'" & COResolvedEnforcement & "', " & _
                    "'" & AOExecutedCheck & "', '" & AOExecuted & "', " & _
                    "'" & AOAppealedCheck & "', '" & AOAppealed & "', " & _
                    "'" & AOResolvedCheck & "', '" & AOResolved & "', " & _
                    "'" & Replace(AOComment, "'", "''") & "', " & _
                    "'" & AFSKeyActionNumber & "', '" & AFSNOVSentNumber & "', " & _
                    "'" & AFSNOVResolvedNumber & "', '" & AFSCOProposedNumber & "', " & _
                    "'" & AFSCOExecutedNumber & "', '" & AFSCOResolvedNumber & "', " & _
                    "'" & AFSAOtoAGNumber & "', '" & AFSCivilCourtNumber & "', " & _
                    "'" & AFSAOResolvedNumber & "', " & _
                    "'" & UserGCode & "', sysdate ) "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Select " & DBNameSpace & ".SSCPEnforcementnumber.currval from dual "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        txtEnforcementNumber.Text = dr.Item(0)
                        If Me.ID = -1 Then
                            Me.ID = CInt(txtEnforcementNumber.Text)
                            MultiForm(Me.Name).ChangeKey(-1, CInt(txtEnforcementNumber.Text))
                        End If
                    End While

                    dr.Close()
                Else
                    SQL = "Insert into " & DBNameSpace & ".SSCP_Enforcement " & _
                    "values " & _
                    "((select max(ID) + 1 from " & DBNameSpace & ".SSCP_Enforcement), " & _
                    "'" & txtEnforcementNumber.Text & "', " & _
                    "'" & TrackingNumber & "', '0413" & AIRSNumber & "', " & _
                    "'" & EnforcementFinalizedCheck & "', '" & EnforcementFinalized & "', " & _
                    "'" & StaffResponsible & "', '" & EnforcementStatus & "', " & _
                    "'" & ActionType & "', '" & Replace(GeneralComments, "'", "''") & "', " & _
                    "'" & DiscoveryDateCheck & "', '" & DiscoveryDate & "', " & _
                    "'" & DayZeroCheck & "', '" & DayZero & "', " & _
                    "'" & HPVType & "', '" & Pollutants & "', " & _
                    "'" & PollutantStatus & "',  " & _
                    "'" & LONtoUCCheck & "', '" & LONToUC & "', " & _
                    "'" & LONSentCheck & "', '" & LonSent & "', " & _
                    "'" & LONResolvedCheck & "', '" & LONResolved & "', " & _
                    "'" & Replace(LONComments, "'", "''") & "', '" & LONResolvedEnforcement & "', " & _
                    "'" & NOVtoUCCheck & "', '" & NOVToUC & "', " & _
                    "'" & NOVtoPMCheck & "', '" & NOVToPM & "', " & _
                    "'" & NOVSentCheck & "', '" & NOVSent & "', " & _
                    "'" & NOVResponseRecieveCheck & "', '" & NOVResponseReceived & "', " & _
                    "'" & NFAtoUCCheck & "', '" & NFAToUC & "', " & _
                    "'" & NFAtoPMCheck & "', '" & NFAToPM & "', " & _
                    "'" & NFALetterSentCheck & "', '" & NFALetterSent & "', " & _
                    "'" & Replace(NOVCommetns, "'", "''") & "', '" & NOVResovledEnforcement & "', " & _
                    "'" & COtoUCCheck & "', '" & COToUC & "', " & _
                    "'" & COtoPMCheck & "', '" & COToPM & "', " & _
                    "'" & CoProposedCheck & "', '" & COProposed & "', " & _
                    "'" & COReceivedCompanyCheck & "', '" & COReceivedCompany & "', " & _
                    "'" & CORecievedDirectorCheck & "', '" & COReceivedDirector & "', " & _
                    "'" & COExecutedCheck & "', '" & COExecuted & "', " & _
                    "'" & CONumber & "', " & _
                    "'" & COResolvedCheck & "', '" & COResolved & "', " & _
                    "'" & COPenaltyAmount & "', '" & Replace(COPenaltyAmountComments, "'", "''") & "', " & _
                    "'" & Replace(COComment, "'", "''") & "', '" & StipulatedPenalty & "', " & _
                    "'" & COResolvedEnforcement & "', " & _
                    "'" & AOExecutedCheck & "', '" & AOExecuted & "', " & _
                    "'" & AOAppealedCheck & "', '" & AOAppealed & "', " & _
                    "'" & AOResolvedCheck & "', '" & AOResolved & "', " & _
                    "'" & Replace(AOComment, "'", "''") & "', " & _
                    "'" & AFSKeyActionNumber & "', '" & AFSNOVSentNumber & "', " & _
                    "'" & AFSNOVResolvedNumber & "', '" & AFSCOProposedNumber & "', " & _
                    "'" & AFSCOExecutedNumber & "', '" & AFSCOResolvedNumber & "', " & _
                    "'" & AFSAOtoAGNumber & "', '" & AFSCivilCourtNumber & "', " & _
                    "'" & AFSAOResolvedNumber & "', " & _
                    "'" & UserGCode & "', sysdate ) "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd = New OracleCommand("AIRBranch.PD_SSCPEnforcement", CurrentConnection)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add(New OracleParameter("ENFORCEMENT", OracleDbType.Varchar2)).Value = txtEnforcementNumber.Text
                cmd.ExecuteNonQuery()

                If cboPollutantStatus.SelectedValue = "" Then
                    cboPollutantStatus.SelectedValue = "0"
                End If
                'Update Pollutant Status in Header Tables 
                i = 0
                For i = 0 To lvPollutants.Items.Count - 1
                    If lvPollutants.Items.Item(i).Checked = True Then

                        SQL = "Update " & DBNameSpace & ".APBAirProgramPollutants set " & _
                        "strComplianceStatus = '" & cboPollutantStatus.SelectedValue & "', " & _
                        "strModifingPerson = '" & UserGCode & "', " & _
                        "datModifingDate = '" & OracleDate & "' " & _
                        "where strAirPollutantKey = '" & lvPollutants.Items.Item(i).SubItems(5).Text & "' " & _
                        "and strPollutantkey = '" & lvPollutants.Items.Item(i).SubItems(2).Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If
                Next
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub SaveStipulatedPenalties()
        Try
            Dim stipulatedKey As String = ""
            Dim AFSNumber As String = ""
            Dim temp As String = ""
            Dim query As String

            If txtEnforcementNumber.Text <> "" And txtEnforcementNumber.Text <> "N/A" Then
                query = "SELECT MAX(SSCPENFORCEMENTSTIPULATED.STRENFORCEMENTKEY) AS MaxKey " & _
                "FROM " & DBNameSpace & ".SSCPENFORCEMENTSTIPULATED " & _
                "WHERE SSCPENFORCEMENTSTIPULATED.STRENFORCEMENTNUMBER = :enfNumber"
                Using connection As New OracleConnection(DB.CurrentConnectionString)
                    Using command As New OracleCommand(query, connection) With {.CommandType = CommandType.Text}
                        With command
                            .BindByName = True
                            .Parameters.Add(":enfNumber", txtEnforcementNumber.Text)
                        End With
                        Try
                            connection.Open()
                            Dim reader As OracleDataReader = command.ExecuteReader
                            While reader.Read
                                If Not IsDBNull(reader.Item("MaxKey")) Then
                                    stipulatedKey = reader.Item("MaxKey")
                                Else
                                    stipulatedKey = "0"
                                End If
                            End While
                        Catch ee As OracleException
                            MessageBox.Show("Could not connect to the database.")
                        End Try
                    End Using
                End Using
            Else
                stipulatedKey = "0"
            End If

            stipulatedKey = CStr(CInt(stipulatedKey) + 1)

            If txtAFSKeyActionNumber.Text <> "" Then
                SQL = "Select strAFSActionNumber " & _
                "from " & DBNameSpace & ".APBSupplamentalData " & _
                "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    AFSNumber = dr.Item("strAFSActionNumber")
                End While
                dr.Close()

                temp = CStr(CInt(AFSNumber) + 1)

                SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                "strAFSActionNumber = '" & temp & "' " & _
                "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Else
                SQL = "Select strAFSActionNumber " & _
                "from " & DBNameSpace & ".APBSupplamentalData " & _
                "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    AFSNumber = dr.Item("strAFSActionNumber")
                End While
                dr.Close()

                txtAFSKeyActionNumber.Text = AFSNumber

                temp = CStr(CInt(AFSNumber) + 1)

                SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                "strAFSActionNumber = '" & temp & "' " & _
                "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                AFSNumber = temp
                temp = CStr(CInt(AFSNumber) + 1)

                SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                "strAFSActionNumber = '" & temp & "' " & _
                "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

            If txtEnforcementNumber.Text = "" Or txtEnforcementNumber.Text = "N/A" Then SaveEnforcement()

            query = "Insert into " & DBNameSpace & ".SSCPEnforcementStipulated " & _
            "values (:enfNumber,:stipKey,:stipPenalty,:stipComments,:afsNumber,:userGCode,:oracleDate)"
            Using connection As New OracleConnection(DB.CurrentConnectionString)
                Using command As New OracleCommand(query, connection) With {.CommandType = CommandType.Text}
                    With command
                        .BindByName = True
                        .Parameters.Add(":enfNumber", txtEnforcementNumber.Text)
                        .Parameters.Add(":stipKey", stipulatedKey.ToString)
                        .Parameters.Add(":stipPenalty", txtStipulatedPenalty.Text)
                        .Parameters.Add(":stipComments", txtStipulatedComments.Text)
                        .Parameters.Add(":afsNumber", AFSNumber)
                        .Parameters.Add(":userGCode", UserGCode)
                        .Parameters.Add(":oracleDate", OracleDate)
                    End With
                    Try
                        connection.Open()
                        command.ExecuteNonQuery()
                    Catch ee As OracleException
                        MessageBox.Show("Could not connect to the database.")
                    End Try
                End Using
            End Using

            LoadStipulatedPenalties()
            SaveEnforcement()

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
                If txtAFSKeyActionNumber.Text = "" Then
                    SQL = "Select strAFSActionNumber " & _
                    "from " & DBNameSpace & ".APBSupplamentalData " & _
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        KeyActionNumber = dr.Item("strAFSActionNumber")
                    End While
                    dr.Close()

                    txtAFSKeyActionNumber.Text = KeyActionNumber

                    KeyActionNumber = CStr(CInt(KeyActionNumber) + 1)

                    SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                    "strAFSActionNUmber = '" & KeyActionNumber & "' " & _
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    KeyActionNumber = txtAFSKeyActionNumber.Text
                End If

                If DTPNOVsent.Checked = True Then 'HPV
                    If txtAFSNOVActionNumber.Text = "" Then

                        SQL = "Select strAFSActionNumber " & _
                        "from " & DBNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            NOVActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        txtAFSNOVActionNumber.Text = NOVActionNumber

                        NOVActionNumber = CStr(CInt(NOVActionNumber) + 1)

                        SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNUmber = '" & NOVActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        NOVActionNumber = txtAFSNOVActionNumber.Text
                    End If
                Else
                    NOVActionNumber = ""
                End If

                If DTPNFALetterSent.Checked = True Then
                    If txtAFSNOVResolvedNumber.Text = "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & DBNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            NFAActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        txtAFSNOVResolvedNumber.Text = NFAActionNumber

                        NFAActionNumber = CStr(CInt(NFAActionNumber) + 1)

                        SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & NFAActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        NFAActionNumber = txtAFSNOVResolvedNumber.Text
                    End If
                Else
                    NFAActionNumber = ""
                End If

                If DTPCOProposed.Checked = True Then
                    If txtAFSCOProposedActionNumber.Text = "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & DBNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            COProposedActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        txtAFSCOProposedActionNumber.Text = COProposedActionNumber

                        COProposedActionNumber = CStr(CInt(COProposedActionNumber) + 1)

                        SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & COProposedActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        COProposedActionNumber = txtAFSCOProposedActionNumber.Text
                    End If
                Else
                    COProposedActionNumber = ""
                End If

                If DTPCOExecuted.Checked = True Then
                    If txtAFSCOExecutedActionNumber.Text = "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & DBNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            COExecutedActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        txtAFSCOExecutedActionNumber.Text = COExecutedActionNumber

                        COExecutedActionNumber = CStr(CInt(COExecutedActionNumber) + 1)

                        SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & COExecutedActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        COExecutedActionNumber = txtAFSCOExecutedActionNumber.Text
                    End If
                Else
                    COExecutedActionNumber = ""
                End If

                If DTPCOResolved.Checked = True Then
                    If txtAFSCOResolvedActionNumber.Text = "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & DBNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            COResolvedActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        txtAFSCOResolvedActionNumber.Text = COResolvedActionNumber

                        COResolvedActionNumber = CStr(CInt(COResolvedActionNumber) + 1)

                        SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & COResolvedActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        COResolvedActionNumber = txtAFSCOResolvedActionNumber.Text
                    End If
                Else
                    COResolvedActionNumber = ""
                End If

                If DTPAOExecuted.Checked = True Then
                    If txtAFSAOToAGActionNumber.Text = "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & DBNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            AOtoAGActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        txtAFSAOToAGActionNumber.Text = AOtoAGActionNumber

                        AOtoAGActionNumber = CStr(CInt(AOtoAGActionNumber) + 1)

                        SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & AOtoAGActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        AOtoAGActionNumber = txtAFSAOToAGActionNumber.Text
                    End If
                Else
                    AOtoAGActionNumber = ""
                End If

                If DTPAOAppealed.Checked = True Then
                    If txtAFSCivilCourtActionNumber.Text = "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & DBNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            AOtoCivilCourtActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        txtAFSCivilCourtActionNumber.Text = AOtoCivilCourtActionNumber

                        AOtoCivilCourtActionNumber = CStr(CInt(AOtoCivilCourtActionNumber) + 1)

                        SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & AOtoCivilCourtActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        AOtoCivilCourtActionNumber = txtAFSCivilCourtActionNumber.Text
                    End If
                Else
                    AOtoCivilCourtActionNumber = ""
                End If

                If DTPAOResolved.Checked = True Then
                    If txtAFSAOResolvedActionNumber.Text = "" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from " & DBNameSpace & ".APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            AOResolvedActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        txtAFSAOResolvedActionNumber.Text = AOResolvedActionNumber

                        AOResolvedActionNumber = CStr(CInt(AOResolvedActionNumber) + 1)

                        SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                        "strAFSActionNumber = '" & AOResolvedActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Else
                        AOResolvedActionNumber = txtAFSAOResolvedActionNumber.Text
                    End If
                Else
                    AOResolvedActionNumber = ""
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
            If txtAFSKeyActionNumber.ReadOnly = True Then
                txtAFSKeyActionNumber.ReadOnly = False
                txtAFSCOExecutedActionNumber.ReadOnly = False
                txtAFSCivilCourtActionNumber.ReadOnly = False
                txtAFSNOVActionNumber.ReadOnly = False
                txtAFSCOResolvedActionNumber.ReadOnly = False
                txtAFSAOToAGActionNumber.ReadOnly = False
                txtAFSNOVResolvedNumber.ReadOnly = False
                txtAFSCOProposedActionNumber.ReadOnly = False
                txtAFSAOResolvedActionNumber.ReadOnly = False
                txtStipulatedPenalitiesActionNumber.ReadOnly = False
            Else
                txtAFSKeyActionNumber.ReadOnly = True
                txtAFSCOExecutedActionNumber.ReadOnly = True
                txtAFSCivilCourtActionNumber.ReadOnly = True
                txtAFSNOVActionNumber.ReadOnly = True
                txtAFSCOResolvedActionNumber.ReadOnly = True
                txtAFSAOToAGActionNumber.ReadOnly = True
                txtAFSNOVResolvedNumber.ReadOnly = True
                txtAFSCOProposedActionNumber.ReadOnly = True
                txtAFSAOResolvedActionNumber.ReadOnly = True
                txtStipulatedPenalitiesActionNumber.ReadOnly = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub ClearEnforcement()
        Try
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
            txtCOComments.Clear()
            txtPenaltyComments.Clear()
            txtCOPenaltyAmount.Clear()
            DTPCOResolved.Text = OracleDate
            DTPCOResolved.Checked = False
            txtCONumber.Clear()
            DTPCOExecuted.Text = OracleDate
            DTPCOExecuted.Checked = False
            DTPCOReceivedfromDirector.Text = OracleDate
            DTPCOReceivedfromDirector.Checked = False
            DTPCOReceivedfromCompany.Text = OracleDate
            DTPCOReceivedfromCompany.Checked = False
            DTPCOProposed.Text = OracleDate
            DTPCOProposed.Checked = False
            DTPCOToPM.Text = OracleDate
            DTPCOToPM.Checked = False
            DTPCOToUC.Text = OracleDate
            DTPCOToUC.Checked = False
            txtNOVComments.Clear()
            DTPNFALetterSent.Text = OracleDate
            DTPNFALetterSent.Checked = False
            DTPNFAToPM.Text = OracleDate
            DTPNFAToPM.Checked = False
            DTPNFAToUC.Text = OracleDate
            DTPNFAToUC.Checked = False
            DTPNOVResponseReceived.Text = OracleDate
            DTPNOVResponseReceived.Checked = False
            DTPNOVsent.Text = OracleDate
            DTPNOVsent.Checked = False
            DTPNOVToPM.Text = OracleDate
            DTPNOVToPM.Checked = False
            DTPNOVToUC.Text = OracleDate
            DTPNOVToUC.Checked = False
            txtLONComments.Clear()
            DTPLONSent.Text = OracleDate
            DTPLONSent.Checked = False
            DTPLONResolved.Text = OracleDate
            DTPLONResolved.Checked = False
            DTPLONToUC.Text = OracleDate
            DTPLONToUC.Checked = False
            txtStipulatedPenalitiesActionNumber.Clear()
            txtAFSAOResolvedActionNumber.Clear()

            txtAFSCOProposedActionNumber.Clear()
            txtAFSNOVResolvedNumber.Clear()
            txtAFSAOToAGActionNumber.Clear()
            txtAFSCOResolvedActionNumber.Clear()
            txtAFSNOVActionNumber.Clear()
            txtAFSCivilCourtActionNumber.Clear()
            txtAFSCOExecutedActionNumber.Clear()
            txtAFSKeyActionNumber.Clear()
            DTPEnforcementResolved.Text = OracleDate
            DTPEnforcementResolved.Checked = False
            chbHPV.Checked = False
            txtGeneralComments.Clear()
            DTPDayZero.Text = OracleDate
            DTPDayZero.Checked = False
            DTPDiscoveryDate.Text = OracleDate
            DTPDiscoveryDate.Checked = False
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
            "from " & DBNameSpace & ".AFSSSCPEnforcementRecords " & _
            "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                        "from " & DBNameSpace & ".SSCP_AuditedEnforcement " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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

                        SQL = "Delete " & DBNameSpace & ".AFSSSCPEnforcementRecords " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from " & DBNameSpace & ".AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                        cmd = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete " & DBNameSpace & ".SSCPENforcementStipulated " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from " & DBNameSpace & ".AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                        cmd = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete " & DBNameSpace & ".SSCP_AuditedEnforcement " & _
                        "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                               "values " & _
                               "(" & _
                               "(select " & _
                               "case when max(numCounter) is null then 1 " & _
                               "else max(numCounter) + 1 " & _
                               "end numCounter " & _
                               "from " & DBNameSpace & ".AFSDeletions), " & _
                               "'" & tempAIRS & "', " & _
                               "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                               "'" & OracleDate & "', '', " & _
                               "'') "

                        cmd = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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
                    If AccountFormAccess(48, 2) = "1" Then
                        btnSubmitToUC.Visible = True
                    End If

                    If AccountFormAccess(48, 3) = "1" Then
                        btnSubmitEnforcementToEPA.Visible = True
                        btnManuallyEnterAFS.Visible = True
                        btnSubmitToUC.Visible = False
                    End If
                    If AccountFormAccess(48, 4) = "1" Then
                        btnManuallyEnterAFS.Visible = True
                    End If
                    If txtAFSKeyActionNumber.Text <> "" Then
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
                    If AccountFormAccess(48, 2) = "1" And txtAFSKeyActionNumber.Text = "" Then
                        btnSubmitToUC.Visible = True
                    End If

                    If AccountFormAccess(48, 3) = "1" Then
                        btnSubmitEnforcementToEPA.Visible = True
                        btnManuallyEnterAFS.Visible = True
                        btnSubmitToUC.Visible = False
                    End If
                    If AccountFormAccess(48, 4) = "1" Then
                        btnManuallyEnterAFS.Visible = True
                    End If
                    If txtAFSKeyActionNumber.Text <> "" Then
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

                    If AccountFormAccess(48, 2) = "1" And txtAFSKeyActionNumber.Text = "" Then
                        btnSubmitToUC.Visible = True
                    End If

                    If AccountFormAccess(48, 3) = "1" Then
                        btnSubmitEnforcementToEPA.Visible = True
                        btnManuallyEnterAFS.Visible = True
                        btnSubmitToUC.Visible = False
                    End If
                    If AccountFormAccess(48, 4) = "1" Then
                        btnManuallyEnterAFS.Visible = True
                    End If
                    If txtAFSKeyActionNumber.Text <> "" Then
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
    Private Sub txtTrackingNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTrackingNumber.TextChanged
        Try
            txtDiscoveryEventNumber.Text = txtTrackingNumber.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnEditAirProgramPollutants_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditAirProgramPollutants.Click
        Try
            If txtEnforcementNumber.Text <> "" And txtEnforcementNumber.Text <> "N/A" Then

                If EditAirProgramPollutants IsNot Nothing Then EditAirProgramPollutants.Dispose()
                EditAirProgramPollutants = Nothing
                If EditAirProgramPollutants Is Nothing Then EditAirProgramPollutants = New IAIPEditAirProgramPollutants
                EditAirProgramPollutants.txtAirsNumber.Text = Me.txtAIRSNumber.Text
                EditAirProgramPollutants.txtEnforcementNumber.Text = txtEnforcementNumber.Text
                EditAirProgramPollutants.Show()
                EditAirProgramPollutants.TPEnforcementPollutants.Focus()
            Else
                MsgBox("Save this Enforcement Action atleast once before you try to add pollutants.", MsgBoxStyle.Information, "Enforcement")
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
                SSCPReports = Nothing
                If SSCPReports Is Nothing Then SSCPReports = New SSCPEvents
                SSCPReports.txtTrackingNumber.Text = txtTrackingNumber.Text
                SSCPReports.Show()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnSaveStipulatedPenalty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveStipulatedPenaltyButton.Click
        If String.IsNullOrEmpty(txtStipulatedPenalty.Text) Then
            MsgBox("Enter a stipulated penalty amount first.")
            Exit Sub
        End If

        SaveStipulatedPenalties()
        ClearStipulatedPenaltyForm()
    End Sub
    Private Sub DeletePenalty(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeletePenaltyButton.Click

        Dim query As String = "Delete from " & DBNameSpace & ".SSCPENforcementStipulated " & _
        "where strEnforcementNumber = :enfNumber and strEnforcementKey = :enfKey"

        Using connection As New OracleConnection(DB.CurrentConnectionString)
            Using command As New OracleCommand(query, connection) With {.CommandType = CommandType.Text}
                With command
                    .BindByName = True
                    .Parameters.Add(":enfNumber", txtEnforcementNumber.Text)
                    .Parameters.Add(":enfKey", txtStipulatedKey.Text)
                End With

                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                Catch ee As OracleException
                    MessageBox.Show("Could not connect to the database.")
                End Try
            End Using
        End Using

        LoadStipulatedPenalties()
        SaveEnforcement()
        ClearStipulatedPenaltyForm()

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
        If txtStipulatedKey.Text <> "" Then
            Dim success As Boolean = False

            Dim query As String = "Select strStipulatedPenalty, strStipulatedPenaltyComments " & _
            "from " & DBNameSpace & ".SSCPENforcementStipulated " & _
            "where strEnforcementNumber = :enfNumber and strEnforcementKey = :enfKey"

            Using connection As New OracleConnection(DB.CurrentConnectionString)
                Using command As New OracleCommand(query, connection) With {.CommandType = CommandType.Text}
                    With command
                        .BindByName = True
                        .Parameters.Add(":enfNumber", txtEnforcementNumber.Text)
                        .Parameters.Add(":enfKey", txtStipulatedKey.Text)
                    End With
                    Try
                        connection.Open()
                        Dim reader As OracleDataReader = command.ExecuteReader
                        While reader.Read
                            If IsDBNull(reader.Item("strStipulatedPenalty")) Then
                                txtStipulatedPenalty.Text = ""
                            Else
                                txtStipulatedPenalty.Text = reader.Item("strStipulatedPenalty")
                            End If
                            If IsDBNull(reader.Item("strStipulatedPenaltyComments")) Then
                                txtStipulatedComments.Text = ""
                            Else
                                txtStipulatedComments.Text = reader.Item("strStipulatedPenaltyComments")
                            End If
                        End While
                        success = True
                    Catch ee As OracleException
                        MessageBox.Show("Could not connect to the database.")
                    End Try
                End Using
            End Using

            If success Then
                Dim ctrl As Control
                Dim tag As String
                For Each ctrl In StipulatedPenalties.Controls
                    If ctrl.Tag IsNot Nothing Then
                        tag = ctrl.Tag.ToString
                        If tag.Contains("GroupExistingStipulatedPenalty") Then
                            If TypeOf (ctrl) Is Button Then
                                With ctrl
                                    .Visible = True
                                    .Enabled = True
                                End With
                            End If
                        ElseIf tag.Contains("GroupEmptyStipulatedPenalty") Then
                            If TypeOf (ctrl) Is Button Then
                                With ctrl
                                    .Visible = False
                                    .Enabled = False
                                End With
                            End If
                        End If
                    End If
                Next
            Else
                ClearStipulatedPenaltyForm()
            End If

        Else
            ClearStipulatedPenaltyForm()
        End If
    End Sub
    Private Sub ClearStipulatedPenaltyForm()
        Dim ctrl As Control
        Dim tag As String
        For Each ctrl In StipulatedPenalties.Controls
            If ctrl.Tag IsNot Nothing Then
                tag = ctrl.Tag.ToString
                If tag.Contains("GroupExistingStipulatedPenalty") Then
                    If TypeOf (ctrl) Is Button Then
                        With ctrl
                            .Visible = False
                            .Enabled = False
                        End With
                    End If
                ElseIf tag.Contains("GroupEmptyStipulatedPenalty") Then
                    If TypeOf (ctrl) Is Button Then
                        With ctrl
                            .Visible = True
                            .Enabled = True
                        End With
                    End If
                ElseIf tag.Contains("GroupStipulatedPenaltyInput") Then
                    If TypeOf (ctrl) Is TextBox Then
                        ctrl.Text = String.Empty
                    End If
                End If
            End If
        Next
    End Sub
    Private Sub btnSubmitToUC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmitToUC.Click
        Try
            If SingleFormIsOpen(SSCPEnforcementChecklist.Name) Then
                MsgBox("Please close the linking tool before saving.", MsgBoxStyle.Exclamation, "SSCP Enforcement")
                Exit Sub
            End If

            txtSubmitToUC.Text = "UC"
            btnSubmitToUC.Visible = False
            SaveEnforcement()
            MsgBox("Current data saved.", MsgBoxStyle.Information, "SSCP Enforcement")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSubmitEnforcementToEPA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmitEnforcementToEPA.Click
        Try
            If SingleFormIsOpen(SSCPEnforcementChecklist.Name) Then
                MsgBox("Please close the linking tool before saving.", MsgBoxStyle.Exclamation, "SSCP Enforcement")
                Exit Sub
            End If

            If txtDiscoveryEventNumber.Text = "" Then
                Dim result As DialogResult

                result = MessageBox.Show("There is no linked event for this enforcement action." & vbCrLf & _
                "Do you want to submit this enforcement to EPA without an initating action?", "Enforcement", _
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                Select Case result
                    Case Windows.Forms.DialogResult.No
                        Exit Sub
                End Select
            End If

            SaveAFSInformation()
            SaveEnforcement()
            MsgBox("Current data saved.", MsgBoxStyle.Information, "SSCP Enforcement")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnManuallyEnterAFS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManuallyEnterAFS.Click
        Try
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

            If DTPDiscoveryDate.Checked = True Then
                ViolationDate = Format(DTPDiscoveryDate.Value, "dd-MMM-yyyy")
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
    Private Sub tsbClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ClearEnforcement()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub tsbDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            If txtEnforcementNumber.Text <> "" And txtEnforcementNumber.Text <> "N/A" Then
                DeleteEnforcement()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Dispose()
    End Sub
    Private Sub mmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSave.Click
        SaveClick()
    End Sub
    Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSave.Click
        SaveClick()
    End Sub
    Private Sub SaveClick()
        Try
            If SingleFormIsOpen(SSCPEnforcementChecklist) Then
                MsgBox("Please close the linking tool before saving.", MsgBoxStyle.Exclamation, "SSCP Enforcement")
                Exit Sub
            End If

            SaveEnforcement()
            LoadEnforcement()

            If AccountFormAccess(48, 2) = "1" Or AccountFormAccess(48, 3) = "1" Or AccountFormAccess(48, 4) = "1" Then
                CheckOpenStatus()
            End If

            MsgBox("Current data saved.", MsgBoxStyle.Information, "SSCP Enforcement")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub mmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClose.Click
        Me.Dispose()
    End Sub
    Private Sub mmiDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiDelete.Click
        Try

            If txtEnforcementNumber.Text <> "" And txtEnforcementNumber.Text <> "N/A" Then
                DeleteEnforcement()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#End Region

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

    Private Sub mmiShowAuditHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiShowAuditHistory.Click
        Try
            If TCEnforcement.TabPages.Contains(TPAuditHistory) Then
            Else
                TCEnforcement.TabPages.Add(TPAuditHistory)
                LoadAuditData()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnHideAudit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHideAudit.Click
        Try
            If TCEnforcement.TabPages.Contains(TPAuditHistory) Then
                TCEnforcement.TabPages.Remove(TPAuditHistory)
            Else

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadAuditData()
        Try

            SQL = "select sb1.ID, sb1.strEnforcementNumber, " & _
       "strTrackingNumber, substr(strAIRSNumber, 5) as strAIRSnumber, " & _
       "strEnforcementFinalized, datEnforcementFinalized, " & _
       "(select (strLastName||', '||strFirstName) from " & DBNameSpace & ".EPDUserProfiles, " & _
       "" & DBNameSpace & ".SSCP_Enforcement " & _
       "where " & DBNameSpace & ".epduserprofiles.numUserID = " & DBNameSpace & ".SSCP_enforcement.numstaffresponsible " & _
       "and " & DBNameSpace & ".SSCP_Enforcement.id = sb1.id) as StaffResponsible, numStaffResponsible, " & _
       "strStatus, strActionType, " & _
       "strGeneralCOmments, strDiscoveryDate, " & _
       "strDayZero, datDayZero, " & _
       "strHPV, strPollutants, " & _
       "strPOllutantStatus, " & _
       "strLONToUC, datLONtoUC, " & _
       "strLONSent, datLONSEnt, " & _
       "strLOnresolved, datLONResolved, " & _
       "strLONComments, strLONResolvedEnforcement, " & _
       "strNOVToUC, datNOVtoUC, " & _
       "strNOVtoPM, datNOVtoPM, " & _
       "strNOVSent, datNOVsent, " & _
       "strNOVResponsereceived, datNOVResponseReceived, " & _
       "strNFAtoUC, datNFAtoUC, " & _
       "strNFAtoPM, datNFAtoPM, " & _
       "strNFALetterSent, datNFALetterSent, " & _
       "strNOVComment, strNOVResolvedEnforcement, " & _
       "strCOtoUC, datCOtoUC, " & _
       "strCOtoPM, datCOtoPM, " & _
       "strCOProposed, datCOProposed, " & _
       "strCOReceivedFromCompany, datCOReceivedFromCompany, " & _
       "strCOReceivedFromDirector, datCOReceivedFromDirector, " & _
       "strCOExecuted, datCOExecuted, " & _
       "strCONumber, " & _
       "strCOResolved, datCOResolved, " & _
       "strCOPenaltyAmount, strCOPenaltyAmountComments, " & _
       "strCOComment, strStipulatedPenalty, " & _
       "strCOResolvedEnforcement, " & _
       "strAOExecuted, datAOExecuted, " & _
       "strAOAppealed, datAOAppealed, " & _
       "strAOResolved, datAOResolved, " & _
       "strAOComment, " & _
       "(select " & _
       "(strLastName||', '||strFirstName) from " & DBNameSpace & ".EPDUserProfiles, " & _
       "" & DBNameSpace & ".SSCP_Enforcement " & _
       "where " & DBNameSpace & ".epduserprofiles.numUserID = " & DBNameSpace & ".SSCP_enforcement.strModifingPerson " & _
       "and " & DBNameSpace & ".SSCP_Enforcement.id = sb1.id) as ModifingPerson, strModifingPerson, " & _
       "DatModifingDate " & _
       "from " & _
       "(Select * " & _
       "From " & DBNameSpace & ".SSCP_Enforcement " & _
       "where strEnforcementNumber = '" & txtEnforcementNumber.Text & "' order by ID desc)  sb1 "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

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
            dgvAuditHistory.ReadOnly = True

            dgvAuditHistory.Columns("ID").HeaderText = "ID"
            dgvAuditHistory.Columns("ID").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("ID").DisplayIndex = 0

            dgvAuditHistory.Columns("strEnforcementNumber").HeaderText = "Enforcement #"
            dgvAuditHistory.Columns("strEnforcementNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strEnforcementNumber").DisplayIndex = 1
            dgvAuditHistory.Columns("strTrackingNumber").HeaderText = "Tracking #"
            dgvAuditHistory.Columns("strTrackingNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strTrackingNumber").DisplayIndex = 2
            dgvAuditHistory.Columns("strAIRSNumber").HeaderText = "AIRS #"
            dgvAuditHistory.Columns("strAIRSNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strAIRSNumber").DisplayIndex = 3
            dgvAuditHistory.Columns("strEnforcementFinalized").HeaderText = "Enforcement Finalized"
            dgvAuditHistory.Columns("strEnforcementFinalized").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strEnforcementFinalized").DisplayIndex = 4
            dgvAuditHistory.Columns("datEnforcementFinalized").HeaderText = "Enforcement Finalized Date"
            dgvAuditHistory.Columns("datEnforcementFinalized").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datEnforcementFinalized").DisplayIndex = 5
            dgvAuditHistory.Columns("datEnforcementFinalized").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvAuditHistory.Columns("StaffResponsible").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("StaffResponsible").DisplayIndex = 6
            dgvAuditHistory.Columns("numStaffResponsible").HeaderText = "Staff ID"
            dgvAuditHistory.Columns("numStaffResponsible").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("numStaffResponsible").DisplayIndex = 7
            dgvAuditHistory.Columns("strStatus").HeaderText = "Status"
            dgvAuditHistory.Columns("strStatus").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strStatus").DisplayIndex = 8
            dgvAuditHistory.Columns("strActionType").HeaderText = "Action Type"
            dgvAuditHistory.Columns("strActionType").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strActionType").DisplayIndex = 9
            dgvAuditHistory.Columns("strGeneralCOmments").HeaderText = "General Comments"
            dgvAuditHistory.Columns("strGeneralCOmments").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strGeneralCOmments").DisplayIndex = 10
            dgvAuditHistory.Columns("strDiscoveryDate").HeaderText = "Discovery Date"
            dgvAuditHistory.Columns("strDiscoveryDate").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strDiscoveryDate").DisplayIndex = 11
            dgvAuditHistory.Columns("strDayZero").HeaderText = "Day Zero"
            dgvAuditHistory.Columns("strDayZero").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strDayZero").DisplayIndex = 12
            dgvAuditHistory.Columns("datDayZero").HeaderText = "Day Zero Date"
            dgvAuditHistory.Columns("datDayZero").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datDayZero").DisplayIndex = 13
            dgvAuditHistory.Columns("datDayZero").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strHPV").HeaderText = "HPV Type"
            dgvAuditHistory.Columns("strHPV").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strHPV").DisplayIndex = 14
            dgvAuditHistory.Columns("strPollutants").HeaderText = "Pollutants"
            dgvAuditHistory.Columns("strPollutants").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strPollutants").DisplayIndex = 15
            dgvAuditHistory.Columns("strPOllutantStatus").HeaderText = "Pollutant Status"
            dgvAuditHistory.Columns("strPOllutantStatus").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strPOllutantStatus").DisplayIndex = 16
            dgvAuditHistory.Columns("strLONToUC").HeaderText = "LON to UC"
            dgvAuditHistory.Columns("strLONToUC").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strLONToUC").DisplayIndex = 17
            dgvAuditHistory.Columns("datLONtoUC").HeaderText = "Date LON to UC"
            dgvAuditHistory.Columns("datLONtoUC").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datLONtoUC").DisplayIndex = 18
            dgvAuditHistory.Columns("datLONtoUC").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strLONSent").HeaderText = "LON Sent"
            dgvAuditHistory.Columns("strLONSent").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strLONSent").DisplayIndex = 19
            dgvAuditHistory.Columns("datLONSEnt").HeaderText = "Date LON Sent"
            dgvAuditHistory.Columns("datLONSEnt").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datLONSEnt").DisplayIndex = 20
            dgvAuditHistory.Columns("datLONSEnt").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strLOnresolved").HeaderText = "LON Resolved"
            dgvAuditHistory.Columns("strLOnresolved").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strLOnresolved").DisplayIndex = 21
            dgvAuditHistory.Columns("datLONResolved").HeaderText = "Date LON Resolved"
            dgvAuditHistory.Columns("datLONResolved").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datLONResolved").DisplayIndex = 22
            dgvAuditHistory.Columns("datLONResolved").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strLONComments").HeaderText = "LON Comments"
            dgvAuditHistory.Columns("strLONComments").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strLONComments").DisplayIndex = 23
            dgvAuditHistory.Columns("strLONResolvedEnforcement").HeaderText = "LON Resoved Enforcement"
            dgvAuditHistory.Columns("strLONResolvedEnforcement").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strLONResolvedEnforcement").DisplayIndex = 24
            dgvAuditHistory.Columns("strNOVToUC").HeaderText = "NOV to UC"
            dgvAuditHistory.Columns("strNOVToUC").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strNOVToUC").DisplayIndex = 25
            dgvAuditHistory.Columns("datNOVtoUC").HeaderText = "Date NOV to UC"
            dgvAuditHistory.Columns("datNOVtoUC").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datNOVtoUC").DisplayIndex = 26
            dgvAuditHistory.Columns("datNOVtoUC").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strNOVtoPM").HeaderText = "NOV to PM"
            dgvAuditHistory.Columns("strNOVtoPM").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strNOVtoPM").DisplayIndex = 27
            dgvAuditHistory.Columns("datNOVtoPM").HeaderText = "Date NOV to PM"
            dgvAuditHistory.Columns("datNOVtoPM").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datNOVtoPM").DisplayIndex = 28
            dgvAuditHistory.Columns("datNOVtoPM").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strNOVSent").HeaderText = "NOV Sent"
            dgvAuditHistory.Columns("strNOVSent").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strNOVSent").DisplayIndex = 29
            dgvAuditHistory.Columns("datNOVsent").HeaderText = "Date NOV Sent"
            dgvAuditHistory.Columns("datNOVsent").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datNOVsent").DisplayIndex = 30
            dgvAuditHistory.Columns("datNOVsent").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strNOVResponsereceived").HeaderText = "NOV Response Recieved"
            dgvAuditHistory.Columns("strNOVResponsereceived").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strNOVResponsereceived").DisplayIndex = 31
            dgvAuditHistory.Columns("datNOVResponseReceived").HeaderText = "Date NOV Response Recieved"
            dgvAuditHistory.Columns("datNOVResponseReceived").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datNOVResponseReceived").DisplayIndex = 32
            dgvAuditHistory.Columns("datNOVResponseReceived").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strNFAtoUC").HeaderText = "NFA to UC"
            dgvAuditHistory.Columns("strNFAtoUC").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strNFAtoUC").DisplayIndex = 33
            dgvAuditHistory.Columns("datNFAtoUC").HeaderText = "Date NFA to UC"
            dgvAuditHistory.Columns("datNFAtoUC").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datNFAtoUC").DisplayIndex = 34
            dgvAuditHistory.Columns("datNFAtoUC").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strNFAtoPM").HeaderText = "NFA to PM"
            dgvAuditHistory.Columns("strNFAtoPM").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strNFAtoPM").DisplayIndex = 35
            dgvAuditHistory.Columns("datNFAtoPM").HeaderText = "Date NFA to PM"
            dgvAuditHistory.Columns("datNFAtoPM").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datNFAtoPM").DisplayIndex = 36
            dgvAuditHistory.Columns("datNFAtoPM").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strNFALetterSent").HeaderText = "NFA Letter Sent"
            dgvAuditHistory.Columns("strNFALetterSent").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strNFALetterSent").DisplayIndex = 37
            dgvAuditHistory.Columns("datNFALetterSent").HeaderText = "Date NFA Letter Sent"
            dgvAuditHistory.Columns("datNFALetterSent").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datNFALetterSent").DisplayIndex = 38
            dgvAuditHistory.Columns("datNFALetterSent").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strNOVComment").HeaderText = "NOV Comment"
            dgvAuditHistory.Columns("strNOVComment").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strNOVComment").DisplayIndex = 39
            dgvAuditHistory.Columns("strNOVResolvedEnforcement").HeaderText = "NOV Resolved"
            dgvAuditHistory.Columns("strNOVResolvedEnforcement").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strNOVResolvedEnforcement").DisplayIndex = 40
            dgvAuditHistory.Columns("strCOtoUC").HeaderText = "CO to UC"
            dgvAuditHistory.Columns("strCOtoUC").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strCOtoUC").DisplayIndex = 41
            dgvAuditHistory.Columns("datCOtoUC").HeaderText = "Date CO to UC"
            dgvAuditHistory.Columns("datCOtoUC").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datCOtoUC").DisplayIndex = 42
            dgvAuditHistory.Columns("datCOtoUC").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strCOtoPM").HeaderText = "CO to PM"
            dgvAuditHistory.Columns("strCOtoPM").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strCOtoPM").DisplayIndex = 43
            dgvAuditHistory.Columns("datCOtoPM").HeaderText = "Date CO to PM"
            dgvAuditHistory.Columns("datCOtoPM").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datCOtoPM").DisplayIndex = 44
            dgvAuditHistory.Columns("datCOtoPM").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strCOProposed").HeaderText = "CO Proposed"
            dgvAuditHistory.Columns("strCOProposed").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strCOProposed").DisplayIndex = 45
            dgvAuditHistory.Columns("datCOProposed").HeaderText = "Date CO Proposed"
            dgvAuditHistory.Columns("datCOProposed").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datCOProposed").DisplayIndex = 46
            dgvAuditHistory.Columns("datCOProposed").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strCOReceivedFromCompany").HeaderText = "CO Received From Company"
            dgvAuditHistory.Columns("strCOReceivedFromCompany").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strCOReceivedFromCompany").DisplayIndex = 47
            dgvAuditHistory.Columns("datCOReceivedFromCompany").HeaderText = "Date CO Recieved From Company"
            dgvAuditHistory.Columns("datCOReceivedFromCompany").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datCOReceivedFromCompany").DisplayIndex = 48
            dgvAuditHistory.Columns("datCOReceivedFromCompany").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strCOReceivedFromDirector").HeaderText = "CO Received From Director"
            dgvAuditHistory.Columns("strCOReceivedFromDirector").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strCOReceivedFromDirector").DisplayIndex = 49
            dgvAuditHistory.Columns("datCOReceivedFromDirector").HeaderText = "Date CO Received From Director"
            dgvAuditHistory.Columns("datCOReceivedFromDirector").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datCOReceivedFromDirector").DisplayIndex = 50
            dgvAuditHistory.Columns("datCOReceivedFromDirector").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strCOExecuted").HeaderText = "CO Executed"
            dgvAuditHistory.Columns("strCOExecuted").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strCOExecuted").DisplayIndex = 51
            dgvAuditHistory.Columns("datCOExecuted").HeaderText = "Date CO Executed"
            dgvAuditHistory.Columns("datCOExecuted").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datCOExecuted").DisplayIndex = 52
            dgvAuditHistory.Columns("datCOExecuted").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strCONumber").HeaderText = "CO Number"
            dgvAuditHistory.Columns("strCONumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strCONumber").DisplayIndex = 53
            dgvAuditHistory.Columns("strCOResolved").HeaderText = "CO Resolved"
            dgvAuditHistory.Columns("strCOResolved").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strCOResolved").DisplayIndex = 54
            dgvAuditHistory.Columns("datCOResolved").HeaderText = "Date CO Resolved"
            dgvAuditHistory.Columns("datCOResolved").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datCOResolved").DisplayIndex = 55
            dgvAuditHistory.Columns("datCOResolved").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strCOPenaltyAmount").HeaderText = "CO Penalty Amount"
            dgvAuditHistory.Columns("strCOPenaltyAmount").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strCOPenaltyAmount").DisplayIndex = 56
            dgvAuditHistory.Columns("strCOPenaltyAmountComments").HeaderText = "CO Penalty Amount Comments"
            dgvAuditHistory.Columns("strCOPenaltyAmountComments").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strCOPenaltyAmountComments").DisplayIndex = 57
            dgvAuditHistory.Columns("strCOComment").HeaderText = "CO Comment"
            dgvAuditHistory.Columns("strCOComment").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strCOComment").DisplayIndex = 58
            dgvAuditHistory.Columns("strStipulatedPenalty").HeaderText = "Stipulated Penalty"
            dgvAuditHistory.Columns("strStipulatedPenalty").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strStipulatedPenalty").DisplayIndex = 59
            dgvAuditHistory.Columns("strCOResolvedEnforcement").HeaderText = "CO Resolved"
            dgvAuditHistory.Columns("strCOResolvedEnforcement").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strCOResolvedEnforcement").DisplayIndex = 60
            dgvAuditHistory.Columns("strAOExecuted").HeaderText = "AO Executed"
            dgvAuditHistory.Columns("strAOExecuted").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strAOExecuted").DisplayIndex = 61
            dgvAuditHistory.Columns("datAOExecuted").HeaderText = "Date AO Executed"
            dgvAuditHistory.Columns("datAOExecuted").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datAOExecuted").DisplayIndex = 62
            dgvAuditHistory.Columns("datAOExecuted").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strAOAppealed").HeaderText = "AO Appealed"
            dgvAuditHistory.Columns("strAOAppealed").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strAOAppealed").DisplayIndex = 63
            dgvAuditHistory.Columns("datAOAppealed").HeaderText = "Date AO Appealed"
            dgvAuditHistory.Columns("datAOAppealed").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datAOAppealed").DisplayIndex = 64
            dgvAuditHistory.Columns("datAOAppealed").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strAOResolved").HeaderText = "AO Resolved"
            dgvAuditHistory.Columns("strAOResolved").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strAOResolved").DisplayIndex = 65
            dgvAuditHistory.Columns("datAOResolved").HeaderText = "Date AO Resolved"
            dgvAuditHistory.Columns("datAOResolved").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("datAOResolved").DisplayIndex = 66
            dgvAuditHistory.Columns("datAOResolved").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvAuditHistory.Columns("strAOComment").HeaderText = "AO Comments"
            dgvAuditHistory.Columns("strAOComment").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strAOComment").DisplayIndex = 67
            dgvAuditHistory.Columns("ModifingPerson").HeaderText = "Modifing Person"
            dgvAuditHistory.Columns("ModifingPerson").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("ModifingPerson").DisplayIndex = 68
            dgvAuditHistory.Columns("strModifingPerson").HeaderText = "Modifing ID"
            dgvAuditHistory.Columns("strModifingPerson").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("strModifingPerson").DisplayIndex = 69
            dgvAuditHistory.Columns("DatModifingDate").HeaderText = "Modifing Date"
            dgvAuditHistory.Columns("DatModifingDate").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvAuditHistory.Columns("DatModifingDate").DisplayIndex = 70
            dgvAuditHistory.Columns("DatModifingDate").DefaultCellStyle.Format = "dd-MMM-yyyy"

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnREfreshAudit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnREfreshAudit.Click
        Try
            If TCEnforcement.TabPages.Contains(TPAuditHistory) Then
                LoadAuditData()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateStipulatedPenalty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateStipulatedPenaltyButton.Click
        If String.IsNullOrEmpty(txtStipulatedKey.Text) Then
            MsgBox("Select an existing stipulated penalty first.", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        If String.IsNullOrEmpty(txtStipulatedPenalty.Text) Then
            MsgBox("Enter a stipulated penalty amount first.")
            Exit Sub
        End If

        Try
            SaveEnforcement()

            SQL = "Update " & DBNameSpace & ".SSCPEnforcementStipulated set " & _
            "strStipulatedPenalty = '" & Replace(txtStipulatedPenalty.Text, "'", "''") & "', " & _
            "strStipulatedPenaltyCOmments = '" & Replace(txtStipulatedComments.Text, "'", "''") & "' " & _
            "where strEnforcementNumber = '" & Replace(txtEnforcementNumber.Text, "'", "''") & "' " & _
            "and strEnforcementKey = '" & txtStipulatedKey.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            LoadStipulatedPenalties()
            ClearStipulatedPenaltyForm()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnExportAuditToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportAuditToExcel.Click
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        Dim i, j As Integer
        Try
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
        Finally
        End Try
    End Sub

    Private Sub btnClearStipulated_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearStipulatedPenaltyFormButton.Click
        ClearStipulatedPenaltyForm()
    End Sub

    Private Sub mmiOnlineHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOnlineHelp.Click
        OpenDocumentationUrl(Me)
    End Sub

    Private Sub dgvFileList_DataBindingComplete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgvDocumentList.DataBindingComplete
        dgvDocumentList.SanelyResizeColumns()
        dgvDocumentList.ClearSelection()
    End Sub
End Class