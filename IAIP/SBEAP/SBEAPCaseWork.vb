Imports System.Data.SqlClient

Public Class SBEAPCaseWork
    Dim FixDtpCheckBox As Boolean = False

    Private Sub SBEAPCaseLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If IsNumeric(txtCaseID.Text) = True Then
                btnAddNewAction.Enabled = True
                btnViewActionType.Enabled = True
            Else
                btnAddNewAction.Enabled = False
                btnViewActionType.Enabled = False
            End If

            rdbSingleClient.Checked = True

            LoadComboBoxes()
            LoadAttendingStaff()

            FormStatus(EnableOrDisable.Enable)

            TCCaseSpecificData.Visible = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadComboBoxes()
        Try
            Dim SQL As String = "Select " &
            "'' as numActionType, '' as strWorkDescription " &
            "union select " &
            "convert(varchar(max),numActionType), strWorkDescription " &
            "from LookUpSBEAPCaseWork " &
            "order by strWorkDescription "

            With cboActionType
                .DataSource = DB.GetDataTable(SQL)
                .DisplayMember = "strWorkDescription"
                .ValueMember = "numActionType"
            End With

            SQL = "Select STRDISTRICTNAME as dn " &
            "from LOOKUPDISTRICTS " &
            "Union " &
            "select 'APB' " &
            "Union " &
            "select 'LBP' " &
            "Union " &
            "select 'WPB' " &
            "Union " &
            "select 'P2AD' " &
            "Union " &
            "select 'GDED' " &
            "Union " &
            "select 'GEFA' " &
            "Union " &
            "select 'NSBEAP' "

            With cboInteragency
                .DataSource = DB.GetDataTable(SQL)
                .DisplayMember = "dn"
                .ValueMember = "dn"
            End With

            SQL = "select
                0  as NUMUSERID,
                '' as UserName
            union
            select
                convert(varchar(max), p.NUMUSERID),
                concat(STRLASTNAME, ', ', STRFIRSTNAME)
            from EPDUSERPROFILES p
                inner join
                IAIPPERMISSIONS i
                    on p.NUMUSERID = i.NUMUSERID
            where (p.NUMEMPLOYEESTATUS = 1
                   AND (i.STRIAIPPERMISSIONS LIKE '%(142)%'
                        OR i.STRIAIPPERMISSIONS LIKE '%(143)%'))
            union
            select
                distinct
                convert(varchar(max), NUMUSERID),
                concat(STRLASTNAME, ', ', STRFIRSTNAME)
            from EPDUSERPROFILES p
                inner join SBEAPCASELOG l
                    on p.NUMUSERID = l.NUMSTAFFRESPONSIBLE
            order by UserName "

            With cboStaffResponsible
                .DataSource = DB.GetDataTable(SQL)
                .DisplayMember = "UserName"
                .ValueMember = "NumUserID"
            End With

            cboTechAssistType.Items.Add(" ")
            cboTechAssistType.Items.Add("1 - Easy")
            cboTechAssistType.Items.Add("2 - Medium")
            cboTechAssistType.Items.Add("3 - Hard")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FormStatus(enable As EnableOrDisable)
        Dim status As Boolean = CBool(enable)

        tsbSave.Enabled = status
        txtCaseID.Enabled = status
        txtClientID.Enabled = status
        btnRefreshClient.Enabled = status
        cboStaffResponsible.Enabled = status
        cboActionType.Enabled = status
        cboInteragency.Enabled = status
        DTPCaseOpened.Enabled = status
        DTPCaseClosed.Enabled = status
        TCCaseSpecificData.Enabled = status
        btnDeleteAction.Enabled = status
        btnAddNewAction.Enabled = status
        txtCaseDescription.Enabled = status
        txtReferralInformation.Enabled = status
        DTPReferralDate.Enabled = status
    End Sub

    Public Sub LoadClientInfo()
        Try
            Dim ClientID As String = ""
            Dim CompanyName As String = ""
            Dim CompanyAddress As String = ""
            Dim County As String = ""

            Dim SQL As String = "select " &
            "clientID, " &
            "strCompanyName, " &
            "strCompanyAddress, " &
            "strCompanyCity, " &
            "strCompanyState, " &
            "strCompanyZipCode, " &
            "strCountyName " &
            "from SBEAPClients left join LookUpCountyInformation " &
            "on SBEAPClients.strCompanyCounty = LookUpCountyInformation.strCountyCode " &
            "where ClientId = @clientid "

            Dim p As New SqlParameter("@clientid", txtClientID.Text)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("ClientID")) Then
                    ClientID = "Client ID: " & vbCrLf & vbCrLf
                Else
                    ClientID = "Client ID - " & dr.Item("ClientID") & vbCrLf & vbCrLf
                End If
                If IsDBNull(dr.Item("strCompanyName")) Then
                    CompanyName = ""
                Else
                    CompanyName = dr.Item("strCompanyName") & vbCrLf
                End If
                If IsDBNull(dr.Item("strCompanyAddress")) Then
                    CompanyAddress = CompanyAddress
                Else
                    CompanyAddress = CompanyAddress & dr.Item("strCompanyAddress") & vbCrLf
                End If
                If IsDBNull(dr.Item("strCompanyCity")) Then
                    CompanyAddress = CompanyAddress
                Else
                    CompanyAddress = CompanyAddress & dr.Item("strCompanyCity")
                End If
                If IsDBNull(dr.Item("strCompanyState")) Then
                    CompanyAddress = CompanyAddress
                Else
                    CompanyAddress = CompanyAddress & ", " & dr.Item("strCompanyState")
                End If
                If IsDBNull(dr.Item("strCompanyZipCode")) Then
                    CompanyAddress = CompanyAddress & vbCrLf & vbCrLf
                Else
                    CompanyAddress = CompanyAddress & " " & dr.Item("strCompanyZipCode") & vbCrLf & vbCrLf
                End If
                If IsDBNull(dr.Item("strCountyName")) Then
                    County = "County- " & CompanyAddress
                Else
                    County = "County- " & dr.Item("strCountyName")
                End If
            End If

            txtClientInformation.Text = ClientID & CompanyName & CompanyAddress & County

            SQL = "select " &
            "count(*) as Outstanding " &
            "from SBEAPCaseLog " &
            "where ClientID = @clientid " &
            "and datCaseClosed is null"

            Dim dr2 As DataRow = DB.GetDataRow(SQL, p)

            If dr2 IsNot Nothing Then
                If IsDBNull(dr2.Item("Outstanding")) Then
                    txtOutstandingCases.Text = "0"
                Else
                    txtOutstandingCases.Text = dr2.Item("Outstanding")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Public Sub LoadCaseLogData()
        Try
            If txtCaseID.Text <> "" Then
                Dim SQL As String = "Select " &
                "numStaffResponsible, datCaseOpened, " &
                "strCaseSummary, " &
                "datCaseClosed, " &
                "case " &
                "when numModifingStaff is Null then ' ' " &
                "else concat(strLastName, ', ',strFirstName) " &
                "END ModifingStaff, " &
                "numModifingStaff, " &
                "datModifingDate, " &
                "strInterAgency, strReferralComments, " &
                "datReferralDate, strComplaintBased, " &
                "strCaseClosureLetterSent " &
                "from SBEAPCaseLog left join EPDUserProfiles " &
                "on SBEAPCaseLog.numModifingStaff = EPDUserProfiles.numUserID " &
                "where numCaseID = @caseid "

                Dim p As New SqlParameter("@caseid", txtCaseID.Text)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("numStaffResponsible")) Then
                        cboStaffResponsible.SelectedValue = 0
                    Else
                        cboStaffResponsible.SelectedValue = dr.Item("numStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datCaseOpened")) Then
                        DTPCaseOpened.Value = Today
                    Else
                        DTPCaseOpened.Text = dr.Item("datCaseOpened")
                    End If
                    If IsDBNull(dr.Item("datCaseClosed")) Then
                        FixDtpCheckBox = True
                        DTPCaseClosed.Value = Today
                        DTPCaseClosed.Checked = False
                        FixDtpCheckBox = False
                    Else
                        DTPCaseClosed.Text = dr.Item("datCaseClosed")
                        DTPCaseClosed.Checked = True
                    End If
                    If IsDBNull(dr.Item("ModifingStaff")) Then
                        txtLastModifingStaff.Text = ""
                    Else
                        txtLastModifingStaff.Text = dr.Item("ModifingStaff")
                    End If
                    If IsDBNull(dr.Item("datModifingDate")) Then
                        DTPLastModified.Value = Today
                    Else
                        DTPLastModified.Text = dr.Item("datModifingDate")
                    End If
                    If IsDBNull(dr.Item("strCaseSummary")) Then
                        txtCaseDescription.Clear()
                    Else
                        txtCaseDescription.Text = dr.Item("strCaseSummary")
                    End If
                    If IsDBNull(dr.Item("strInterAgency")) Then
                        cboInteragency.Text = ""
                    Else
                        cboInteragency.Text = dr.Item("strInterAgency")
                    End If
                    If IsDBNull(dr.Item("strReferralComments")) Then
                        txtReferralInformation.Clear()
                    Else
                        txtReferralInformation.Text = dr.Item("strReferralComments")
                    End If
                    If IsDBNull(dr.Item("datReferralDate")) Then
                        DTPReferralDate.Value = Today
                        DTPReferralDate.Checked = False
                    Else
                        DTPReferralDate.Text = dr.Item("datReferralDate")
                        DTPReferralDate.Checked = True
                    End If
                    If IsDBNull(dr.Item("strComplaintBased")) Then
                        chbComplaintBased.Checked = False
                    Else
                        If dr.Item("strComplaintBased") = "True" Then
                            chbComplaintBased.Checked = True
                        Else
                            chbComplaintBased.Checked = False
                        End If
                    End If
                    If IsDBNull(dr.Item("strCaseClosureLetterSent")) Then
                        chbCaseClosureLetter.Checked = False
                    Else
                        If dr.Item("strCaseClosureLetterSent") = "False" Then
                            chbCaseClosureLetter.Checked = False
                        Else
                            chbCaseClosureLetter.Checked = True
                        End If
                    End If
                End If

                LoadActionLog()

                If txtActionCount.Text = "1" Then
                    txtActionID.Text = dgvActionLog(0, 0).Value
                    txtActionType.Text = dgvActionLog(2, 0).Value
                    txtCreationDate.Text = dgvActionLog(3, 0).Value
                    LoadActionTab()
                End If

                SQL = "Select " &
                "count(*) " &
                "from SBEAPCaseLogLink " &
                "where numCaseID = @caseid "

                Dim ClientCount As Integer = DB.GetInteger(SQL, p)

                If ClientCount = 1 Then
                    rdbSingleClient.Checked = True

                    SQL = "Select " &
                        "ClientID " &
                        "from SBEAPCaseLogLink " &
                        "where numCaseID = @caseid "

                    txtClientID.Text = DB.GetString(SQL, p)

                    If txtClientID.Text <> "" Then
                        LoadClientInfo()
                    End If
                ElseIf ClientCount > 1 Then
                    rdbMultiClient.Checked = True
                    txtMultiClientList.Clear()

                    SQL = "Select " &
                        "ClientID " &
                        "from SBEAPCaseLogLink " &
                        "where numCaseID = @caseid "

                    Dim dt As DataTable = DB.GetDataTable(SQL, p)

                    For Each dr2 As DataRow In dt.Rows
                        txtMultiClientList.Text = txtMultiClientList.Text & dr2.Item("ClientID") & vbCrLf
                    Next

                    LoadMultiClientList()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadActionLog()
        Try
            Dim SQL As String = "select " &
            "numActionID, " &
            "numCaseID, " &
            "case " &
            "when SBEAPActionLog.numActionType is null then ' ' " &
            "else strWorkDescription " &
            "end ActionDescription, " &
            "datCreationDate as CreationDate,  " &
            "datActionOccured as OccuredDate " &
            "from SBEAPActionLog left join LookUpSBEAPCaseWork " &
            "on SBEAPActionLog.numActionType = LookUpSBEAPCaseWork.numActionType " &
            "where SBEAPActionLog.numCaseID = @caseid " &
            "order by numActionID desc "

            Dim p As New SqlParameter("@caseid", txtCaseID.Text)

            Dim dt As DataTable = DB.GetDataTable(SQL, p)
            dgvActionLog.DataSource = dt

            If dt IsNot Nothing Then
                dgvActionLog.RowHeadersVisible = False
                dgvActionLog.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvActionLog.AllowUserToResizeColumns = True
                dgvActionLog.AllowUserToAddRows = False
                dgvActionLog.AllowUserToDeleteRows = False
                dgvActionLog.AllowUserToOrderColumns = True
                dgvActionLog.AllowUserToResizeRows = True
                dgvActionLog.ColumnHeadersHeight = "35"
                dgvActionLog.Columns("numActionID").HeaderText = "Action ID"
                dgvActionLog.Columns("numActionID").DisplayIndex = 1
                dgvActionLog.Columns("numActionID").Width = 100
                dgvActionLog.Columns("numActionID").Visible = False
                dgvActionLog.Columns("numCaseID").HeaderText = "Client ID"
                dgvActionLog.Columns("numCaseID").DisplayIndex = 2
                dgvActionLog.Columns("numCaseID").Visible = False
                dgvActionLog.Columns("ActionDescription").HeaderText = "Action Description"
                dgvActionLog.Columns("ActionDescription").DisplayIndex = 3
                dgvActionLog.Columns("ActionDescription").Width = (dgvActionLog.Width - dgvActionLog.Columns("numActionID").Width)
                dgvActionLog.Columns("CreationDate").HeaderText = "Date Action Created"
                dgvActionLog.Columns("CreationDate").DisplayIndex = 4
                dgvActionLog.Columns("CreationDate").Width = 100
                dgvActionLog.Columns("CreationDate").DefaultCellStyle.Format = "dd-MMM-yyyy"

                dgvActionLog.Columns("OccuredDate").HeaderText = "Date Action Occurred"
                dgvActionLog.Columns("OccuredDate").DisplayIndex = 0
                dgvActionLog.Columns("OccuredDate").Width = 100
                dgvActionLog.Columns("OccuredDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            End If

            txtActionCount.Text = dgvActionLog.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadActionTab()
        Try
            If txtActionID.Text <> "" Then
                TCCaseSpecificData.TabPages.Remove(TPOtherCases)
                TCCaseSpecificData.TabPages.Remove(TPPhoneCalls)
                TCCaseSpecificData.TabPages.Remove(TPConferences)
                TCCaseSpecificData.TabPages.Remove(TPTechnicalAssist)
                TCCaseSpecificData.TabPages.Remove(TPComplianceAssistance)
                Select Case txtActionType.Text
                    Case "Compliance Assistance"
                        TCCaseSpecificData.TabPages.Remove(TPOtherCases)
                        TCCaseSpecificData.TabPages.Remove(TPPhoneCalls)
                        TCCaseSpecificData.TabPages.Remove(TPConferences)
                        TCCaseSpecificData.TabPages.Remove(TPTechnicalAssist)
                        If TCCaseSpecificData.TabPages.Contains(TPComplianceAssistance) Then
                        Else
                            TCCaseSpecificData.TabPages.Add(TPComplianceAssistance)
                            LoadComplianceAssist()
                        End If
                    Case "Permit Assistance"
                        TCCaseSpecificData.TabPages.Remove(TPComplianceAssistance)
                        TCCaseSpecificData.TabPages.Remove(TPOtherCases)
                        TCCaseSpecificData.TabPages.Remove(TPPhoneCalls)
                        TCCaseSpecificData.TabPages.Remove(TPConferences)
                        If TCCaseSpecificData.TabPages.Contains(TPTechnicalAssist) Then
                        Else
                            TCCaseSpecificData.TabPages.Add(TPTechnicalAssist)
                            LoadTechnicalAssist()
                        End If
                    Case "Phone Call Made/Received"
                        TCCaseSpecificData.TabPages.Remove(TPComplianceAssistance)
                        TCCaseSpecificData.TabPages.Remove(TPTechnicalAssist)
                        TCCaseSpecificData.TabPages.Remove(TPOtherCases)
                        TCCaseSpecificData.TabPages.Remove(TPConferences)
                        If TCCaseSpecificData.TabPages.Contains(TPPhoneCalls) Then
                        Else
                            TCCaseSpecificData.TabPages.Add(TPPhoneCalls)
                            LoadPhoneCall()
                        End If
                    Case "Meeting/Conferences Attended"
                        TCCaseSpecificData.TabPages.Remove(TPComplianceAssistance)
                        TCCaseSpecificData.TabPages.Remove(TPOtherCases)
                        TCCaseSpecificData.TabPages.Remove(TPPhoneCalls)
                        TCCaseSpecificData.TabPages.Remove(TPTechnicalAssist)
                        If TCCaseSpecificData.TabPages.Contains(TPConferences) Then
                        Else
                            TCCaseSpecificData.TabPages.Add(TPConferences)
                            LoadAttendingStaff()
                            LoadConference()
                        End If
                    Case Else
                        If txtActionType.Text <> "" Then
                            TCCaseSpecificData.TabPages.Remove(TPComplianceAssistance)
                            TCCaseSpecificData.TabPages.Remove(TPTechnicalAssist)
                            TCCaseSpecificData.TabPages.Remove(TPPhoneCalls)
                            TCCaseSpecificData.TabPages.Remove(TPConferences)
                            If TCCaseSpecificData.TabPages.Contains(TPOtherCases) Then
                            Else
                                TCCaseSpecificData.TabPages.Add(TPOtherCases)
                                LoadOther()
                            End If
                            TPOtherCases.Text = txtActionType.Text
                        End If
                End Select

                If IsNumeric(txtActionID.Text) = True Then
                    TCCaseSpecificData.Visible = True
                Else
                    TCCaseSpecificData.Visible = False
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadAttendingStaff()
        Try
            ' Hoo boy would you look at this mess
            Dim SQL As String = "SELECT NUMUSERID, concat(STRLASTNAME, ', ', STRFIRSTNAME) AS UserName
                FROM EPDUSERPROFILES
                WHERE NUMUNIT IN (47, 48)
                UNION
                SELECT NUMUSERID, concat(STRLASTNAME, ', ', STRFIRSTNAME) AS UserName
                FROM (SELECT *
                FROM (SELECT DISTINCT
                LTRIM(RTRIM(m.n.value ('.[1]', 'varchar(8000)') )) AS StaffId
                FROM (SELECT NUMACTIONID, CAST('<XMLRoot><RowData>'+REPLACE(STRSTAFFATTENDING, ',', '</RowData><RowData>')+'</RowData></XMLRoot>' AS xml) AS x
                FROM SBEAPCONFERENCELOG) AS t
                CROSS APPLY x.nodes ('/XMLRoot/RowData') AS m(n)) AS t
                WHERE t.StaffId <> '') AS t
                INNER JOIN EPDUSERPROFILES AS p ON p.NUMUSERID = t.StaffId
                ORDER BY UserName"

            With clbStaffAttending
                .DataSource = DB.GetDataTable(SQL)
                .DisplayMember = "UserName"
                .ValueMember = "NumUserID"
            End With
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadComplianceAssist()
        Try
            chbAirAssist.Checked = False
            chbStormWaterAssist.Checked = False
            chbHazardousWasteAssist.Checked = False
            chbSolidWasteAssist.Checked = False
            chbUSTAssist.Checked = False
            chbScrapTireAssist.Checked = False
            chbLeadAndAsbestosAssist.Checked = False
            chbOtherAssist.Checked = False
            txtComplianceAssistanceComments.Clear()

            Dim SQL As String = "Select " &
            "strAIRAssist, strStormWaterAssist, " &
            "strHazWasteAssist, strSolidWasteAssist, " &
            "strUSTAssist, strScrapTireAssist, " &
            "strLeadAssist, strOtherAssist, " &
            "strComments, strModifingStaff, " &
            "datModifingDate " &
            "from SBEAPComplianceAssist " &
            "where numActionID = @actionid "

            Dim p As New SqlParameter("@actionid", txtActionID.Text)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)
            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strAirAssist")) Then
                    chbAirAssist.Checked = False
                Else
                    If dr.Item("strAirAssist") = "True" Then
                        chbAirAssist.Checked = True
                    Else
                        chbAirAssist.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strStormWaterAssist")) Then
                    chbStormWaterAssist.Checked = False
                Else
                    If dr.Item("strStormWaterAssist") = "True" Then
                        chbStormWaterAssist.Checked = True
                    Else
                        chbStormWaterAssist.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strHazWasteAssist")) Then
                    chbHazardousWasteAssist.Checked = False
                Else
                    If dr.Item("strHazWasteAssist") = "True" Then
                        chbHazardousWasteAssist.Checked = True
                    Else
                        chbHazardousWasteAssist.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strSolidWasteAssist")) Then
                    chbSolidWasteAssist.Checked = False
                Else
                    If dr.Item("strSolidWasteAssist") = "True" Then
                        chbSolidWasteAssist.Checked = True
                    Else
                        chbSolidWasteAssist.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strUSTAssist")) Then
                    chbUSTAssist.Checked = False
                Else
                    If dr.Item("strUSTAssist") = "True" Then
                        chbUSTAssist.Checked = True
                    Else
                        chbUSTAssist.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strScrapTireAssist")) Then
                    chbScrapTireAssist.Checked = False
                Else
                    If dr.Item("strScrapTireAssist") = "True" Then
                        chbScrapTireAssist.Checked = True
                    Else
                        chbScrapTireAssist.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strLeadAssist")) Then
                    chbLeadAndAsbestosAssist.Checked = False
                Else
                    If dr.Item("strLeadAssist") = "True" Then
                        chbLeadAndAsbestosAssist.Checked = True
                    Else
                        chbLeadAndAsbestosAssist.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strOtherAssist")) Then
                    chbOtherAssist.Checked = False
                Else
                    If dr.Item("strOtherAssist") = "True" Then
                        chbOtherAssist.Checked = True
                    Else
                        chbOtherAssist.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strComments")) Then
                    txtComplianceAssistanceComments.Clear()
                Else
                    txtComplianceAssistanceComments.Text = dr.Item("strComments")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadTechnicalAssist()
        Try
            Dim AssistanceRequest As String = ""

            Dim SQL As String = "Select " &
            "strTechnicalAssistType, datInitialContactDate, " &
            "datAssistStartDate, datAssistEndDate, " &
            "strAssistanceRequest, strAIRSnumber, " &
            "strTechnicalAssistNotes " &
            "from SBEAPTechnicalAssist " &
            "where numActionID = @actionid "

            Dim p As New SqlParameter("@actionid", txtActionID.Text)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)
            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strTechnicalAssistType")) Then
                    cboTechAssistType.Text = " "
                Else
                    cboTechAssistType.Text = dr.Item("strTEchnicalAssistType")
                End If
                If IsDBNull(dr.Item("datInitialContactDate")) Then
                    DTPTechAssistInitialContact.Value = Today
                    DTPTechAssistInitialContact.Checked = False
                Else
                    DTPTechAssistInitialContact.Text = dr.Item("datInitialContactDate")
                    DTPTechAssistInitialContact.Checked = True
                End If
                If IsDBNull(dr.Item("datAssistStartDate")) Then
                    DTPTechAssistStart.Value = Today
                    DTPTechAssistStart.Checked = False
                Else
                    DTPTechAssistStart.Text = dr.Item("datAssistStartDate")
                    DTPTechAssistStart.Checked = True
                End If
                If IsDBNull(dr.Item("datAssistEndDate")) Then
                    DTPTechAssistEnd.Value = Today
                    DTPTechAssistEnd.Checked = False
                Else
                    DTPTechAssistEnd.Text = dr.Item("datAssistEndDate")
                    DTPTechAssistEnd.Checked = True
                End If
                If IsDBNull(dr.Item("strAssistanceRequest")) Then
                    AssistanceRequest = ""
                Else
                    AssistanceRequest = dr.Item("strAssistanceRequest")
                End If
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    txtAIRSNumber.Clear()
                Else
                    txtAIRSNumber.Text = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("strTechnicalAssistNotes")) Then
                    txtTechnicalAssistNotes.Clear()
                Else
                    txtTechnicalAssistNotes.Text = dr.Item("strTechnicalAssistNotes")
                End If
            End If

            If AssistanceRequest <> "" Then
                If Mid(AssistanceRequest, 1, 1) = "1" Then
                    chbAirAppPrep.Checked = True
                Else
                    chbAirAppPrep.Checked = False
                End If
                If Mid(AssistanceRequest, 2, 1) = "1" Then
                    chbAirEmissInv.Checked = True
                Else
                    chbAirEmissInv.Checked = False
                End If
                If Mid(AssistanceRequest, 3, 1) = "1" Then
                    chbAirCompCert.Checked = True
                Else
                    chbAirCompCert.Checked = False
                End If
                If Mid(AssistanceRequest, 4, 1) = "1" Then
                    chbAirPermitAssit.Checked = True
                Else
                    chbAirPermitAssit.Checked = False
                End If
                If Mid(AssistanceRequest, 5, 1) = "1" Then
                    chbAirRecordAssist.Checked = True
                Else
                    chbAirRecordAssist.Checked = False
                End If
                If Mid(AssistanceRequest, 6, 1) = "1" Then
                    chbAirEnforceAssist.Checked = True
                Else
                    chbAirEnforceAssist.Checked = False
                End If
                If Mid(AssistanceRequest, 7, 1) = "1" Then
                    chbAirOther.Checked = True
                Else
                    chbAirOther.Checked = False
                End If
                If Mid(AssistanceRequest, 8, 1) = "1" Then
                    chbWaterConstruction.Checked = True
                Else
                    chbWaterConstruction.Checked = False
                End If
                If Mid(AssistanceRequest, 9, 1) = "1" Then
                    chbWaterIndustrial.Checked = True
                Else
                    chbWaterIndustrial.Checked = False
                End If
                If Mid(AssistanceRequest, 10, 1) = "1" Then
                    chbWaterSPCCC.Checked = True
                Else
                    chbWaterSPCCC.Checked = False
                End If
                If Mid(AssistanceRequest, 11, 1) = "1" Then
                    chbWaterEandS.Checked = True
                Else
                    chbWaterEandS.Checked = False
                End If
                If Mid(AssistanceRequest, 12, 1) = "1" Then
                    chbWaterNPDES.Checked = True
                Else
                    chbWaterNPDES.Checked = False
                End If
                If Mid(AssistanceRequest, 13, 1) = "1" Then
                    chbWaterPOTW.Checked = True
                Else
                    chbWaterPOTW.Checked = False
                End If
                If Mid(AssistanceRequest, 14, 1) = "1" Then
                    chbWaterWetlands.Checked = True
                Else
                    chbWaterWetlands.Checked = False
                End If
                If Mid(AssistanceRequest, 15, 1) = "1" Then
                    chbWaterOther.Checked = True
                Else
                    chbWaterOther.Checked = False
                End If
                If Mid(AssistanceRequest, 16, 1) = "1" Then
                    chbWasteFormR.Checked = True
                Else
                    chbWasteFormR.Checked = False
                End If
                If Mid(AssistanceRequest, 17, 1) = "1" Then
                    chbWasteTier2.Checked = True
                Else
                    chbWasteTier2.Checked = False
                End If
                If Mid(AssistanceRequest, 18, 1) = "1" Then
                    chbWasteHazWaste.Checked = True
                Else
                    chbWasteHazWaste.Checked = False
                End If
                If Mid(AssistanceRequest, 19, 1) = "1" Then
                    chbWasteSolidWaste.Checked = True
                Else
                    chbWasteSolidWaste.Checked = False
                End If
                If Mid(AssistanceRequest, 20, 1) = "1" Then
                    chbWasteUST.Checked = True
                Else
                    chbWasteUST.Checked = False
                End If
                If Mid(AssistanceRequest, 21, 1) = "1" Then
                    chbWasteAST.Checked = True
                Else
                    chbWasteAST.Checked = False
                End If
                If Mid(AssistanceRequest, 22, 1) = "1" Then
                    chbWasteOther.Checked = True
                Else
                    chbWasteOther.Checked = False
                End If
                If Mid(AssistanceRequest, 23, 1) = "1" Then
                    chbGeneralMultiMedia.Checked = True
                Else
                    chbGeneralMultiMedia.Checked = False
                End If
                If Mid(AssistanceRequest, 24, 1) = "1" Then
                    chbGeneralEMS.Checked = True
                Else
                    chbGeneralEMS.Checked = False
                End If
                If Mid(AssistanceRequest, 25, 1) = "1" Then
                    chbGeneralOther.Checked = True
                Else
                    chbGeneralOther.Checked = False
                End If
                If Mid(AssistanceRequest, 26, 1) = "1" Then
                    chbPollEnergy.Checked = True
                Else
                    chbPollEnergy.Checked = False
                End If
                If Mid(AssistanceRequest, 27, 1) = "1" Then
                    chbPollWaste.Checked = True
                Else
                    chbPollWaste.Checked = False
                End If
                If Mid(AssistanceRequest, 28, 1) = "1" Then
                    chbPollSovent.Checked = True
                Else
                    chbPollSovent.Checked = False
                End If
                If Mid(AssistanceRequest, 29, 1) = "1" Then
                    chbPollWater.Checked = True
                Else
                    chbPollWater.Checked = False
                End If
                If Mid(AssistanceRequest, 30, 1) = "1" Then
                    chbPollOther.Checked = True
                Else
                    chbPollOther.Checked = False
                End If
            Else
                chbAirAppPrep.Checked = False
                chbAirEmissInv.Checked = False
                chbAirCompCert.Checked = False
                chbAirPermitAssit.Checked = False
                chbAirRecordAssist.Checked = False
                chbAirEnforceAssist.Checked = False
                chbAirOther.Checked = False
                chbWaterConstruction.Checked = False
                chbWaterIndustrial.Checked = False
                chbWaterSPCCC.Checked = False
                chbWaterEandS.Checked = False
                chbWaterNPDES.Checked = False
                chbWaterPOTW.Checked = False
                chbWaterWetlands.Checked = False
                chbWaterOther.Checked = False
                chbWasteFormR.Checked = False
                chbWasteTier2.Checked = False
                chbWasteHazWaste.Checked = False
                chbWasteSolidWaste.Checked = False
                chbWasteUST.Checked = False
                chbWasteAST.Checked = False
                chbWasteOther.Checked = False
                chbGeneralMultiMedia.Checked = False
                chbGeneralEMS.Checked = False
                chbGeneralOther.Checked = False
                chbPollEnergy.Checked = False
                chbPollWaste.Checked = False
                chbPollSovent.Checked = False
                chbPollWater.Checked = False
                chbPollOther.Checked = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadPhoneCall()
        Try
            Dim SQL As String = "Select " &
            "strCallerInformation, " &
            "numCallerPhoneNumber, " &
            "strPhoneLogNotes, " &
            "strOneTimeAssist, " &
            "strFrontDeskCall " &
            "from SBEAPPhoneLog " &
            "where numActionID = @actionid "

            Dim p As New SqlParameter("@actionid", txtActionID.Text)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strCallerInformation")) Then
                    txtCallName.Clear()
                Else
                    txtCallName.Text = dr.Item("strcallerInformation")
                End If
                If IsDBNull(dr.Item("numCallerPhoneNumber")) Then
                    mtbPhoneNumber.Clear()
                Else
                    mtbPhoneNumber.Text = dr.Item("numCallerPhoneNumber")
                End If
                If IsDBNull(dr.Item("strPhoneLogNotes")) Then
                    txtPhoneCallNotes.Clear()
                Else
                    txtPhoneCallNotes.Text = dr.Item("strPhoneLogNotes")
                End If
                If IsDBNull(dr.Item("strOneTimeAssist")) Then
                    chbOnetimeAssist.Checked = False
                Else
                    If dr.Item("strOneTimeAssist") = "True" Then
                        chbOnetimeAssist.Checked = True
                    Else
                        chbOnetimeAssist.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strFrontDeskCall")) Then
                    chbFrontDeskCall.Checked = False
                Else
                    If dr.Item("strFrontDeskCall") = "True" Then
                        chbFrontDeskCall.Checked = True
                    Else
                        chbFrontDeskCall.Checked = False
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadConference()
        Try
            Dim StaffAttending As String = ""

            Dim SQL As String = "Select " &
            "strConferenceAttended, strConferenceLocation, " &
            "strConferenceTopic, strAttendees, " &
            "datConferenceStarted, datConferenceEnded, " &
            "strSBEAPPresentation, strListOfBusinessSectors, " &
            "strCOnferenceFollowUp, strStaffAttending " &
            "from SBEAPConferenceLog " &
            "where numActionID = @actionid "

            Dim p As New SqlParameter("@actionid", txtActionID.Text)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strConferenceAttended")) Then
                    txtConferenceAttended.Clear()
                Else
                    txtConferenceAttended.Text = dr.Item("strConferenceAttended")
                End If
                If IsDBNull(dr.Item("strConferenceLocation")) Then
                    txtConferenceLocation.Clear()
                Else
                    txtConferenceLocation.Text = dr.Item("strConferenceLocation")
                End If
                If IsDBNull(dr.Item("strConferenceTopic")) Then
                    txtConferenceTopic.Clear()
                Else
                    txtConferenceTopic.Text = dr.Item("strConferenceTopic")
                End If
                If IsDBNull(dr.Item("strAttendees")) Then
                    txtConferenceAttendees.Clear()
                Else
                    txtConferenceAttendees.Text = dr.Item("strAttendees")
                End If
                If IsDBNull(dr.Item("datConferenceStarted")) Then
                    DTPConferenceStart.Value = Today
                Else
                    DTPConferenceStart.Text = dr.Item("datConferenceStarted")
                End If
                If IsDBNull(dr.Item("datConferenceEnded")) Then
                    DTPConferenceEnd.Value = Today
                Else
                    DTPConferenceEnd.Text = dr.Item("datConferenceEnded")
                End If
                If IsDBNull(dr.Item("strSBEAPPresentation")) Then
                    rdbSBEAPPresentationYes.Checked = False
                    rdbSBEAPPresentationNo.Checked = False
                Else
                    If dr.Item("strSBEAPPresentation") = "True" Then
                        rdbSBEAPPresentationYes.Checked = True
                    Else
                        rdbSBEAPPresentationNo.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strListOfBusinessSectors")) Then
                    txtListOfBusinessSectors.Clear()
                Else
                    txtListOfBusinessSectors.Text = dr.Item("strListOfBusinessSectors")
                End If
                If IsDBNull(dr.Item("strConferenceFollowUp")) Then
                    txtConferenceFollowUp.Clear()
                Else
                    txtConferenceFollowUp.Text = dr.Item("strConferenceFollowUp")
                End If
                If IsDBNull(dr.Item("strStaffAttending")) Then
                    StaffAttending = ""
                Else
                    StaffAttending = dr.Item("strStaffAttending")
                End If
            End If

            If StaffAttending <> "" Then
                Dim temp As String = ""
                Do While StaffAttending <> ""
                    If StaffAttending.Contains(",") Then
                        temp = Mid(StaffAttending, 1, (InStr(StaffAttending, ",") - 1))
                    Else
                        temp = StaffAttending
                    End If
                    clbStaffAttending.SelectedValue = temp
                    Dim i As Integer = clbStaffAttending.SelectedIndex
                    If i <> -1 Then
                        clbStaffAttending.SetItemCheckState(i, CheckState.Checked)
                    End If
                    clbStaffAttending.SelectedValue = 0
                    If StaffAttending.Contains(",") Then
                        StaffAttending = Replace(StaffAttending, (temp & ","), "")
                    Else
                        StaffAttending = Replace(StaffAttending, temp, "")
                    End If
                Loop
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadOther()
        Try
            Dim SQL As String = "Select " &
            "strCaseNotes " &
            "from SBEAPOtherLog " &
            "where numActionID = @actionid "

            txtCaseNotes.Clear()

            Dim p As New SqlParameter("@actionid", txtActionID.Text)

            txtCaseNotes.Text = DB.GetString(SQL, p)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub UpdateCaseLog(Origin As String)
        Dim SQL As String = ""
        Dim SQL2 As String

        Try
            Dim ClientList As String = ""
            Dim Staff As String = ""
            Dim ClientID As String = ""
            Dim CloseDate As Date? = Nothing
            Dim InterAgency As String = ""
            Dim ReferralComments As String = ""
            Dim ReferralDate As Date? = Nothing
            Dim ComplaintBased As String = ""
            Dim CaseClosedLetter As String = ""

            If cboStaffResponsible.Text <> "" Then
                Staff = cboStaffResponsible.SelectedValue
            Else
                Staff = Nothing
            End If
            If DTPCaseClosed.Checked = True Then
                CloseDate = DTPCaseClosed.Value
            End If
            If cboInteragency.Text <> "" Then
                InterAgency = cboInteragency.Text
            Else
                InterAgency = Nothing
            End If
            If txtReferralInformation.Text <> "" Then
                ReferralComments = txtReferralInformation.Text
            Else
                ReferralComments = Nothing
            End If
            If DTPReferralDate.Checked = True Then
                ReferralDate = DTPReferralDate.Value
            End If
            If chbComplaintBased.Checked = True Then
                ComplaintBased = "True"
            Else
                ComplaintBased = "False"
            End If
            If chbCaseClosureLetter.Checked = True Then
                CaseClosedLetter = "True"
            Else
                CaseClosedLetter = "False"
            End If

            If txtCaseID.Text = "" Then
                SQL = "Insert into SBEAPCaseLog " &
                "(NUMCASEID, " &
                " NUMSTAFFRESPONSIBLE, DATCASEOPENED, STRCASESUMMARY, CLIENTID, DATCASECLOSED, " &
                " NUMMODIFINGSTAFF, DATMODIFINGDATE, STRINTERAGENCY, STRREFERRALCOMMENTS, DATREFERRALDATE, " &
                " STRCOMPLAINTBASED, STRCASECLOSURELETTERSENT) " &
                "values " &
                "((Select " &
                "case " &
                "when (select max(numCaseID) from SBEAPCaseLog) is Null then 1 " &
                "else (select max(convert(int, NUMCASEID)) + 1 from SBEAPCaseLog) " &
                "End CaseID), " &
                " @NUMSTAFFRESPONSIBLE, @DATCASEOPENED, @STRCASESUMMARY, null, @DATCASECLOSED, " &
                " @NUMMODIFINGSTAFF, GETDATE(), @STRINTERAGENCY, @STRREFERRALCOMMENTS, @DATREFERRALDATE, " &
                " @STRCOMPLAINTBASED, @STRCASECLOSURELETTERSENT) "

                SQL2 = "select max(convert(int, NUMCASEID)) as CaseID from SBEAPCASELOG"
            Else
                SQL = "Update SBEAPCaseLog set " &
                "numStaffResponsible = @numStaffResponsible, " &
                "datCaseOpened = @datCaseOpened, " &
                "strCaseSummary = @strCaseSummary, " &
                "datCaseClosed = @datCaseClosed, " &
                "numModifingStaff = @numModifingStaff, " &
                "datModifingDate = getdate(), " &
                "strInterAgency = @strInterAgency, " &
                "strReferralComments = @strReferralComments, " &
                "datReferralDate = @datReferralDate, " &
                "strComplaintBased = @strComplaintBased, " &
                "strCaseClosureLetterSent = @strCaseClosureLetterSent " &
                "where numCaseID = @numCaseID "

                SQL2 = ""
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@NUMSTAFFRESPONSIBLE", Staff),
                New SqlParameter("@DATCASEOPENED", DTPCaseOpened.Value),
                New SqlParameter("@STRCASESUMMARY", txtCaseDescription.Text),
                New SqlParameter("@DATCASECLOSED", CloseDate),
                New SqlParameter("@NUMMODIFINGSTAFF", CurrentUser.UserID),
                New SqlParameter("@STRINTERAGENCY", InterAgency),
                New SqlParameter("@STRREFERRALCOMMENTS", ReferralComments),
                New SqlParameter("@DATREFERRALDATE", ReferralDate),
                New SqlParameter("@STRCOMPLAINTBASED", ComplaintBased),
                New SqlParameter("@STRCASECLOSURELETTERSENT", CaseClosedLetter),
                New SqlParameter("@NUMCASEID", txtCaseID.Text)
            }

            DB.RunCommand(SQL, p)

            If SQL2 <> "" Then
                txtCaseID.Text = DB.GetInteger(SQL2).ToString
            End If

            If rdbSingleClient.Checked = True Then
                ClientID = txtClientID.Text

                SQL = "Delete SBEAPCaseLogLink " &
                "where numCaseID = @caseid "

                Dim p2 As New SqlParameter("@caseid", txtCaseID.Text)

                DB.RunCommand(SQL, p2)

                SQL = "Insert into SBEAPCaseLogLink " &
                    "(NUMCASEID, CLIENTID ) " &
                    "values " &
                    "(@NUMCASEID, @CLIENTID ) "

                Dim p3 As SqlParameter() = {
                    New SqlParameter("@NUMCASEID", txtCaseID.Text),
                    New SqlParameter("@CLIENTID", ClientID)
                }

                DB.RunCommand(SQL, p3)
            Else
                ClientList = txtMultiClientList.Text

                If ClientList <> "" Then
                    SQL = "Delete SBEAPCaseLogLink " &
                        "where numCaseID = @caseid "

                    Dim p4 As New SqlParameter("@caseid", txtCaseID.Text)

                    DB.RunCommand(SQL, p4)

                    Do While txtMultiClientList.Text <> ""
                        If txtMultiClientList.Text.StartsWith(vbCrLf) Then
                            txtMultiClientList.Text = Mid(txtMultiClientList.Text, 3)
                        End If
                        If txtMultiClientList.Text.Contains(vbCrLf) Then
                            ClientID = Mid(txtMultiClientList.Text, 1, txtMultiClientList.Text.IndexOf(vbCrLf))
                        Else
                            ClientID = txtMultiClientList.Text
                        End If

                        txtMultiClientList.Text = Replace(txtMultiClientList.Text, ClientID, "")

                        SQL = "Insert into SBEAPCaseLogLink " &
                            "(NUMCASEID, CLIENTID ) " &
                            "values " &
                            "(@NUMCASEID, @CLIENTID ) "

                        If ClientID <> "" Then
                            Dim p5 As SqlParameter() = {
                                New SqlParameter("@NUMCASEID", txtCaseID.Text),
                                New SqlParameter("@CLIENTID", ClientID)
                            }
                            DB.RunCommand(SQL, p5)
                        End If
                    Loop
                End If
            End If

            If txtActionID.Text <> "" Then
                SaveActionData()
            End If

            If Origin <> "New Action" Then
                MsgBox("Data Saved", MsgBoxStyle.Information, "SBEAP Case Work")
            End If

        Catch ex As Exception
            ErrorReport(ex, SQL, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveActionData()
        Try
            If DAL.Sbeap.ActionExists(txtActionID.Text) Then
                Dim SQL As String = "Update SBEAPActionLog set " &
                "datActionOccured = @ActionOccured " &
                "where numActionId = @actionid "

                Dim p As SqlParameter() = {
                    New SqlParameter("@ActionOccured", DTPActionOccured.Text),
                    New SqlParameter("@actionid", txtActionID.Text)
                }

                DB.RunCommand(SQL, p)
            End If

            Select Case txtActionType.Text
                Case "Compliance Assistance"
                    SaveComplianceAssist()
                Case "Permit Assistance"
                    SaveTechnicalAssist()
                Case "Phone Call Made/Received"
                    SavePhoneCall()
                Case "Meeting/Conferences Attended"
                    SaveConference()
                Case Else
                    SaveOther()
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveComplianceAssist()
        Try
            Dim AirAssist As String = ""
            Dim StormWaterAssist As String = ""
            Dim HazWasteAssist As String = ""
            Dim SolidWasteAssist As String = ""
            Dim USTAssist As String = ""
            Dim ScrapTireAssist As String = ""
            Dim LeadAssist As String = ""
            Dim OtherAssist As String = ""
            Dim Comments As String = ""

            If chbAirAssist.Checked = True Then
                AirAssist = "True"
            Else
                AirAssist = "False"
            End If
            If chbStormWaterAssist.Checked = True Then
                StormWaterAssist = "True"
            Else
                StormWaterAssist = "False"
            End If
            If chbHazardousWasteAssist.Checked = True Then
                HazWasteAssist = "True"
            Else
                HazWasteAssist = "False"
            End If
            If chbSolidWasteAssist.Checked = True Then
                SolidWasteAssist = "True"
            Else
                SolidWasteAssist = "False"
            End If
            If chbUSTAssist.Checked = True Then
                USTAssist = "True"
            Else
                USTAssist = "False"
            End If
            If chbScrapTireAssist.Checked = True Then
                ScrapTireAssist = "True"
            Else
                ScrapTireAssist = "False"
            End If
            If chbLeadAndAsbestosAssist.Checked = True Then
                LeadAssist = "True"
            Else
                LeadAssist = "False"
            End If
            If chbOtherAssist.Checked = True Then
                OtherAssist = "True"
            Else
                OtherAssist = "False"
            End If
            Comments = txtComplianceAssistanceComments.Text

            Dim SQL As String

            If Not DAL.Sbeap.ComplianceAssistExists(txtActionID.Text) Then
                SQL = "Insert into SBEAPComplianceAssist " &
                    "(NUMACTIONID, STRAIRASSIST, STRSTORMWATERASSIST, STRHAZWASTEASSIST, 
                    STRSOLIDWASTEASSIST, STRUSTASSIST, STRSCRAPTIREASSIST, STRLEADASSIST, 
                    STROTHERASSIST, STRCOMMENTS, STRMODIFINGSTAFF, DATMODIFINGDATE) " &
                    "values " &
                    "(@NUMACTIONID, @STRAIRASSIST, @STRSTORMWATERASSIST, @STRHAZWASTEASSIST, 
                    @STRSOLIDWASTEASSIST, @STRUSTASSIST, @STRSCRAPTIREASSIST, @STRLEADASSIST, 
                    @STROTHERASSIST, @STRCOMMENTS, @STRMODIFINGSTAFF, GETDATE()) "
            Else
                SQL = "Update SBEAPComplianceAssist set " &
                    "strAirAssist = @STRAIRASSIST, " &
                    "strStormWaterAssist = @STRSTORMWATERASSIST, " &
                    "strHazWasteAssist = @STRHAZWASTEASSIST, " &
                    "strSolidWasteAssist = @STRSOLIDWASTEASSIST, " &
                    "strUSTAssist = @STRUSTASSIST, " &
                    "strScrapTireAssist = @STRSCRAPTIREASSIST, " &
                    "strLeadAssist = @STRLEADASSIST, " &
                    "strOtherAssist = @STROTHERASSIST, " &
                    "strComments = @STRCOMMENTS, " &
                    "strModifingStaff = @STRMODIFINGSTAFF " &
                    "datModifingDate =  GETDATE()  " &
                    "where numActionID = @NUMACTIONID "
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@NUMACTIONID", txtActionID.Text),
                New SqlParameter("@STRAIRASSIST", AirAssist),
                New SqlParameter("@STRSTORMWATERASSIST", StormWaterAssist),
                New SqlParameter("@STRHAZWASTEASSIST", HazWasteAssist),
                New SqlParameter("@STRSOLIDWASTEASSIST", SolidWasteAssist),
                New SqlParameter("@STRUSTASSIST", USTAssist),
                New SqlParameter("@STRSCRAPTIREASSIST", ScrapTireAssist),
                New SqlParameter("@STRLEADASSIST", LeadAssist),
                New SqlParameter("@STROTHERASSIST", OtherAssist),
                New SqlParameter("@STRCOMMENTS", Comments),
                New SqlParameter("@STRMODIFINGSTAFF", CurrentUser.UserID)
            }

            DB.RunCommand(SQL, p)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveTechnicalAssist()
        Try
            Dim AssistType As String = ""
            Dim ContactDate As Date? = Nothing
            Dim AssistStart As Date? = Nothing
            Dim AssistEnd As Date? = Nothing
            Dim AssistRequest As String = ""
            Dim AIRSNumber As String = ""
            Dim TechnicalAssistComments As String = ""

            If cboTechAssistType.Text <> "" Then
                AssistType = cboTechAssistType.Text
            Else
                AssistType = ""
            End If
            If DTPTechAssistInitialContact.Checked = True Then
                ContactDate = DTPTechAssistInitialContact.Value
            End If
            If DTPTechAssistStart.Checked = True Then
                AssistStart = DTPTechAssistStart.Value
            End If
            If DTPTechAssistEnd.Checked = True Then
                AssistEnd = DTPTechAssistEnd.Value
            End If
            If chbAirAppPrep.Checked = True Then
                AssistRequest = "1"
            Else
                AssistRequest = "0"
            End If
            If chbAirEmissInv.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbAirCompCert.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbAirPermitAssit.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbAirRecordAssist.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbAirEnforceAssist.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbAirOther.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWaterConstruction.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWaterIndustrial.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWaterSPCCC.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWaterEandS.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWaterNPDES.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWaterPOTW.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWaterWetlands.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWaterOther.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWasteFormR.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWasteTier2.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWasteHazWaste.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWasteSolidWaste.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWasteUST.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWasteAST.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbWasteOther.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbGeneralMultiMedia.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbGeneralEMS.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbGeneralOther.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbPollEnergy.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbPollWaste.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbPollSovent.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbPollWater.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If chbPollOther.Checked = True Then
                AssistRequest = AssistRequest & "1"
            Else
                AssistRequest = AssistRequest & "0"
            End If
            If txtAIRSNumber.Text <> "" Then
                AIRSNumber = txtAIRSNumber.Text
            Else
                AIRSNumber = Nothing
            End If
            If txtTechnicalAssistNotes.Text <> "" Then
                TechnicalAssistComments = txtTechnicalAssistNotes.Text
            Else
                TechnicalAssistComments = Nothing
            End If

            Dim SQL As String

            If Not DAL.Sbeap.TechnicalAssistExists(txtActionID.Text) Then
                SQL = "Insert into SBEAPTechnicalAssist " &
                    "(NUMACTIONID, STRTECHNICALASSISTTYPE, DATINITIALCONTACTDATE, DATASSISTSTARTDATE, 
                    DATASSISTENDDATE, STRASSISTANCEREQUEST, STRAIRSNUMBER, STRTECHNICALASSISTNOTES, 
                    STRMODIFINGSTAFF, DATMODIFINGDATE) " &
                    "values " &
                    "(@NUMACTIONID, @STRTECHNICALASSISTTYPE, @DATINITIALCONTACTDATE, @DATASSISTSTARTDATE, 
                    @DATASSISTENDDATE, @STRASSISTANCEREQUEST, @STRAIRSNUMBER, @STRTECHNICALASSISTNOTES, 
                    @STRMODIFINGSTAFF, @DATMODIFINGDATE) "
            Else
                SQL = "Update SBEAPTechnicalAssist set " &
                    "strTechnicalAssistType = @STRTECHNICALASSISTTYPE, " &
                    "datInitialContactDate = @DATINITIALCONTACTDATE, " &
                    "datAssistStartDate = @DATASSISTSTARTDATE, " &
                    "datAssistEndDate = @DATASSISTENDDATE, " &
                    "strAssistanceRequest = @STRASSISTANCEREQUEST, " &
                    "strAIRSNumber = @STRAIRSNUMBER, " &
                    "strTechnicalAssistNotes = @STRTECHNICALASSISTNOTES, " &
                    "strModifingStaff = @STRMODIFINGSTAFF, " &
                    "datModifingDate =  GETDATE() " &
                    "where numActionID = @NUMACTIONID "
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@NUMACTIONID", txtActionID.Text),
                New SqlParameter("@STRTECHNICALASSISTTYPE", AssistType),
                New SqlParameter("@DATINITIALCONTACTDATE", ContactDate),
                New SqlParameter("@DATASSISTSTARTDATE", AssistStart),
                New SqlParameter("@DATASSISTENDDATE", AssistEnd),
                New SqlParameter("@STRASSISTANCEREQUEST", AssistRequest),
                New SqlParameter("@STRAIRSNUMBER", AIRSNumber),
                New SqlParameter("@STRTECHNICALASSISTNOTES", TechnicalAssistComments),
                New SqlParameter("@STRMODIFINGSTAFF", CurrentUser.UserID)
            }

            DB.RunCommand(SQL, p)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SavePhoneCall()
        Try

            Dim CallerInfo As String = txtCallName.Text
            Dim CallerPhone As String = mtbPhoneNumber.Text
            Dim PhoneCallNotes As String = txtPhoneCallNotes.Text
            Dim OneTimeAssist As String = chbOnetimeAssist.Checked.ToString
            Dim FrontDeskCall As String = chbFrontDeskCall.Checked.ToString

            If CallerPhone <> "" AndAlso Not IsNumeric(CallerPhone) Then
                MessageBox.Show("Phone call could not be saved because there is a problem with the phone number. " &
                                "Please fix and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim SQL As String

            If Not DAL.Sbeap.PhoneLogExists(txtActionID.Text) Then
                SQL = "INSERT INTO SBEAPPHONELOG 
                    (NUMACTIONID, STRCALLERINFORMATION, NUMCALLERPHONENUMBER, STRPHONELOGNOTES, 
                    STRONETIMEASSIST, STRFRONTDESKCALL, STRMODIFINGSTAFF, DATMODIFINGDATE)
                    VALUES 
                    (@NUMACTIONID, @STRCALLERINFORMATION, @NUMCALLERPHONENUMBER, @STRPHONELOGNOTES, 
                    @STRONETIMEASSIST, @STRFRONTDESKCALL, @STRMODIFINGSTAFF, GETDATE())"
            Else
                SQL = "Update SBEAPPhoneLog set " &
                    "strCallerInformation = @STRCALLERINFORMATION, " &
                    "numCallerPhoneNumber = @NUMCALLERPHONENUMBER, " &
                    "strPhoneLogNotes = @STRPHONELOGNOTES, " &
                    "strOneTimeAssist = @STRONETIMEASSIST, " &
                    "strFrontDeskCall = @STRFRONTDESKCALL, " &
                    "strModifingStaff = @STRMODIFINGSTAFF, " &
                    "datModifingDate =  GETDATE()  " &
                    "where numActionID = @NUMACTIONID "
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@NUMACTIONID", txtActionID.Text),
                New SqlParameter("@STRCALLERINFORMATION", CallerInfo),
                New SqlParameter("@NUMCALLERPHONENUMBER", CallerPhone),
                New SqlParameter("@STRPHONELOGNOTES", PhoneCallNotes),
                New SqlParameter("@STRONETIMEASSIST", OneTimeAssist),
                New SqlParameter("@STRFRONTDESKCALL", FrontDeskCall),
                New SqlParameter("@STRMODIFINGSTAFF", CurrentUser.UserID)
            }

            DB.RunCommand(SQL, p)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveConference()
        Try
            Dim ConferenceAttended As String = ""
            Dim ConferenceLocation As String = ""
            Dim ConferenceTopic As String = ""
            Dim Attendees As String = ""
            Dim ConferenceStart As Date? = Nothing
            Dim ConferenceEnd As Date? = Nothing
            Dim SBEAPPresentation As String = ""
            Dim ListofBusinesses As String = ""
            Dim FollowUp As String = ""
            Dim StaffAttendies As String = ""
            Dim i As Integer

            If txtConferenceAttended.Text <> "" Then
                ConferenceAttended = txtConferenceAttended.Text
            Else
                ConferenceAttended = Nothing
            End If
            If txtConferenceLocation.Text <> "" Then
                ConferenceLocation = txtConferenceLocation.Text
            Else
                ConferenceLocation = Nothing
            End If
            If txtConferenceTopic.Text <> "" Then
                ConferenceTopic = txtConferenceTopic.Text
            Else
                ConferenceTopic = Nothing
            End If
            If txtConferenceAttendees.Text <> "" Then
                Attendees = txtConferenceAttendees.Text
            Else
                Attendees = Nothing
            End If
            ConferenceStart = DTPConferenceStart.Value
            If DTPConferenceEnd.Value < DTPConferenceStart.Value Then
                ConferenceEnd = ConferenceStart
            Else
                ConferenceEnd = DTPConferenceEnd.Value
            End If
            If rdbSBEAPPresentationYes.Checked = True Then
                SBEAPPresentation = "True"
            Else
                SBEAPPresentation = "False"
            End If
            If txtListOfBusinessSectors.Text <> "" Then
                ListofBusinesses = txtListOfBusinessSectors.Text
            Else
                ListofBusinesses = Nothing
            End If
            If txtConferenceFollowUp.Text <> "" Then
                FollowUp = txtConferenceFollowUp.Text
            Else
                FollowUp = Nothing
            End If

            For i = 0 To clbStaffAttending.Items.Count - 1
                If clbStaffAttending.GetItemChecked(i) = True Then
                    clbStaffAttending.SelectedIndex = i
                    StaffAttendies = StaffAttendies & clbStaffAttending.SelectedValue & ","
                End If
            Next
            If StaffAttendies <> "" Then
                StaffAttendies = Mid(StaffAttendies, 1, (StaffAttendies.Length - 1))
            End If

            Dim SQL As String

            If Not DAL.Sbeap.ConferenceLogExists(txtActionID.Text) Then
                SQL = "INSERT INTO SBEAPConferenceLog 
                    (NUMACTIONID, STRCONFERENCEATTENDED, STRCONFERENCELOCATION, 
                    STRCONFERENCETOPIC, STRATTENDEES, DATCONFERENCESTARTED, DATCONFERENCEENDED, 
                    STRSBEAPPRESENTATION, STRLISTOFBUSINESSSECTORS, STRCONFERENCEFOLLOWUP, 
                    STRSTAFFATTENDING, STRMODIFINGSTAFF, DATMODIFINGDATE)
                    VALUES 
                    (@NUMACTIONID, @STRCONFERENCEATTENDED, @STRCONFERENCELOCATION, 
                    @STRCONFERENCETOPIC, @STRATTENDEES, @DATCONFERENCESTARTED, @DATCONFERENCEENDED, 
                    @STRSBEAPPRESENTATION, @STRLISTOFBUSINESSSECTORS, @STRCONFERENCEFOLLOWUP, 
                    @STRSTAFFATTENDING, @STRMODIFINGSTAFF, GETDATE())"
            Else
                SQL = "Update SBEAPConferenceLog set " &
                    "strConferenceAttended = @STRCONFERENCEATTENDED, " &
                    "strConferenceLocation = @STRCONFERENCELOCATION, " &
                    "strConferenceTopic = @STRCONFERENCETOPIC, " &
                    "strAttendees = @STRATTENDEES, " &
                    "datConferenceStarted = @DATCONFERENCESTARTED, " &
                    "datConferenceEnded = @DATCONFERENCEENDED, " &
                    "strSBEAPPresentation = @STRSBEAPPRESENTATION, " &
                    "strListOfBusinessSectors = @STRLISTOFBUSINESSSECTORS, " &
                    "strConferenceFollowUp = @STRCONFERENCEFOLLOWUP, " &
                    "strStaffAttending = @STRSTAFFATTENDING, " &
                    "strModifingStaff = @STRMODIFINGSTAFF, " &
                    "datModifingDate =  GETDATE()  " &
                    "where numActionID = @NUMACTIONID "
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@NUMACTIONID", txtActionID.Text),
                New SqlParameter("@STRCONFERENCEATTENDED", ConferenceAttended),
                New SqlParameter("@STRCONFERENCELOCATION", ConferenceLocation),
                New SqlParameter("@STRCONFERENCETOPIC", ConferenceTopic),
                New SqlParameter("@STRATTENDEES", Attendees),
                New SqlParameter("@DATCONFERENCESTARTED", ConferenceStart),
                New SqlParameter("@DATCONFERENCEENDED", ConferenceEnd),
                New SqlParameter("@STRSBEAPPRESENTATION", SBEAPPresentation),
                New SqlParameter("@STRLISTOFBUSINESSSECTORS", ListofBusinesses),
                New SqlParameter("@STRCONFERENCEFOLLOWUP", FollowUp),
                New SqlParameter("@STRSTAFFATTENDING", StaffAttendies),
                New SqlParameter("@STRMODIFINGSTAFF", CurrentUser.UserID)
            }

            DB.RunCommand(SQL, p)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveOther()
        Try
            Dim CaseNotes As String = ""

            If txtCaseNotes.Text <> "" Then
                CaseNotes = txtCaseNotes.Text
            Else
                CaseNotes = Nothing
            End If

            Dim SQL As String

            If Not DAL.Sbeap.OtherLogExists(txtActionID.Text) Then
                SQL = "INSERT INTO SBEAPOtherLog (NUMACTIONID, STRCASENOTES, STRMODIFINGSTAFF, DATMODIFINGDATE)
                    VALUES (@NUMACTIONID, @STRCASENOTES, @STRMODIFINGSTAFF, GETDATE())"
            Else
                SQL = "Update SBEAPOtherLog set " &
                    "strCaseNotes = @STRCASENOTES, " &
                    "strModifingStaff = @STRMODIFINGSTAFF , " &
                    "datModifingDate = GETDATE() " &
                    "where numActionID = @NUMACTIONID "
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@NUMACTIONID", txtActionID.Text),
                New SqlParameter("@STRCASENOTES", CaseNotes),
                New SqlParameter("@STRMODIFINGSTAFF", CurrentUser.UserID)
            }

            DB.RunCommand(SQL, p)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ClearForm()
        Try

            ClearActions()
            txtActionID.Clear()
            txtCreationDate.Clear()
            txtActionType.Clear()
            TCCaseSpecificData.Visible = False

            dgvActionLog.DataSource = Nothing
            txtActionCount.Clear()
            DTPReferralDate.Value = Today
            DTPReferralDate.Checked = False
            txtReferralInformation.Clear()
            cboInteragency.Text = ""
            cboStaffResponsible.SelectedValue = CurrentUser.UserID
            DTPCaseClosed.Value = Today
            DTPCaseClosed.Checked = False
            DTPCaseOpened.Value = Today
            txtCaseDescription.Clear()
            txtClientInformation.Clear()
            txtOutstandingCases.Clear()
            txtCaseID.Clear()
            txtClientID.Clear()
            chbComplaintBased.Checked = False
            chbCaseClosureLetter.Checked = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ClearActions()
        Try
            Dim i As Integer

            txtCaseNotes.Clear()
            txtCallName.Clear()
            mtbPhoneNumber.Clear()
            txtPhoneCallNotes.Clear()
            txtConferenceAttended.Clear()
            txtConferenceLocation.Clear()
            DTPConferenceStart.Value = Today
            DTPConferenceEnd.Value = Today
            txtListOfBusinessSectors.Clear()
            txtConferenceTopic.Clear()
            For i = 0 To clbStaffAttending.Items.Count - 1
                clbStaffAttending.SetItemCheckState(i, CheckState.Unchecked)
            Next
            rdbSBEAPPresentationYes.Checked = False
            rdbSBEAPPresentationNo.Checked = False
            txtConferenceAttendees.Clear()
            txtConferenceFollowUp.Clear()
            cboTechAssistType.Text = ""
            DTPTechAssistInitialContact.Value = Today
            DTPTechAssistStart.Value = Today
            DTPTechAssistEnd.Value = Today
            txtTechnicalAssistNotes.Clear()
            chbAirAppPrep.Checked = False
            chbAirEmissInv.Checked = False
            chbAirCompCert.Checked = False
            chbAirPermitAssit.Checked = False
            chbAirRecordAssist.Checked = False
            chbAirEnforceAssist.Checked = False
            chbAirOther.Checked = False
            txtAIRSNumber.Clear()
            chbWaterConstruction.Checked = False
            chbWaterIndustrial.Checked = False
            chbWaterSPCCC.Checked = False
            chbWaterEandS.Checked = False
            chbWaterNPDES.Checked = False
            chbWaterPOTW.Checked = False
            chbWaterWetlands.Checked = False
            chbWaterOther.Checked = False
            chbWasteFormR.Checked = False
            chbWasteTier2.Checked = False
            chbWasteHazWaste.Checked = False
            chbWasteSolidWaste.Checked = False
            chbWasteUST.Checked = False
            chbWasteAST.Checked = False
            chbWasteOther.Checked = False
            chbGeneralMultiMedia.Checked = False
            chbGeneralEMS.Checked = False
            chbGeneralOther.Checked = False
            chbPollEnergy.Checked = False
            chbPollWaste.Checked = False
            chbPollSovent.Checked = False
            chbPollWater.Checked = False
            chbPollOther.Checked = False
            DTPTechAssistInitialContact.Checked = False
            DTPTechAssistStart.Checked = False
            DTPTechAssistEnd.Checked = False
            chbFrontDeskCall.Checked = False
            chbOnetimeAssist.Checked = True
            chbAirAssist.Checked = False
            chbStormWaterAssist.Checked = False
            chbHazardousWasteAssist.Checked = False
            chbSolidWasteAssist.Checked = False
            chbUSTAssist.Checked = False
            chbScrapTireAssist.Checked = False
            chbLeadAndAsbestosAssist.Checked = False
            chbOtherAssist.Checked = False
            txtComplianceAssistanceComments.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadMultiClientList()
        Try
            Dim ClientList As String = ""
            Dim Client As String = ""
            Dim CompanyName As String = ""
            Dim CompanyAddress As String = ""
            Dim CompanyCity As String = ""

            ClientList = txtMultiClientList.Text

            Do While txtMultiClientList.Text <> ""
                If txtMultiClientList.Text.StartsWith(vbCrLf) Then
                    txtMultiClientList.Text = Mid(txtMultiClientList.Text, 3)
                End If
                If txtMultiClientList.Text.Contains(vbCrLf) Then
                    Client = Mid(txtMultiClientList.Text, 1, txtMultiClientList.Text.IndexOf(vbCrLf))
                Else
                    Client = txtMultiClientList.Text
                End If

                txtMultiClientList.Text = Replace(txtMultiClientList.Text, Client, "")

                Dim SQL As String = "Select " &
                      "strCompanyName, strCompanyAddress, " &
                      "strCompanyCity " &
                      "from SBEAPClients " &
                      "where ClientID = @clientid "

                Dim p As New SqlParameter("@clientid", Client)

                Dim dr As DataRow = DB.GetDataRow(SQL, p)

                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("strCompanyName")) Then
                        CompanyName = ""
                    Else
                        CompanyName = dr.Item("strCompanyName")
                    End If
                    If IsDBNull(dr.Item("strCompanyAddress")) Then
                        CompanyAddress = ""
                    Else
                        CompanyAddress = dr.Item("strCompanyAddress")
                    End If
                    If IsDBNull(dr.Item("strCompanyCity")) Then
                        CompanyCity = ""
                    Else
                        CompanyCity = dr.Item("strCompanyCity")
                    End If
                End If

                If Client.Length > 7 Then
                    If txtMultiClients.Text.Contains(Client) Then
                    Else
                        txtMultiClients.Text = txtMultiClients.Text & Client & " - " & CompanyName & " - " & CompanyAddress & ", " & CompanyCity & vbCrLf
                    End If
                End If
            Loop

            txtMultiClientList.Text = ClientList
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        UpdateCaseLog("Non Action")
    End Sub

    Private Sub tsbClientSearch_Click(sender As Object, e As EventArgs) Handles tsbClientSearch.Click
        Try
            Dim clientSearchDialog As New SBEAPClientSearchTool
            clientSearchDialog.ShowDialog()
            If clientSearchDialog.DialogResult = DialogResult.OK Then
                txtClientID.Text = clientSearchDialog.SelectedClientID
                LoadClientInfo()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRefreshClient_Click(sender As Object, e As EventArgs) Handles btnRefreshClient.Click
        Try
            If txtClientID.Text <> "" Then
                LoadClientInfo()
                txtClientInformation.BackColor = Color.White
            Else
                txtClientID.BackColor = Color.Tomato
                txtClientInformation.Clear()
                txtOutstandingCases.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DTPCaseClosed_ValueChanged(sender As Object, e As EventArgs) Handles DTPCaseClosed.ValueChanged
        Try
            If Not FixDtpCheckBox Then
                If DTPCaseClosed.Checked = True Then
                    FormStatus(EnableOrDisable.Disable)
                    DTPCaseClosed.Enabled = True
                    tsbSave.Enabled = True
                Else
                    FormStatus(EnableOrDisable.Enable)
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddNewAction_Click(sender As Object, e As EventArgs) Handles btnAddNewAction.Click
        Try
            Dim SQL As String
            If cboActionType.Text <> "" And cboActionType.SelectedIndex > 0 Then
                ClearActions()
                If txtCaseID.Text = "" Then
                    UpdateCaseLog("New Action")
                Else
                    SQL = "Update SBEAPCaseLog set " &
                        "datModifingDate =  GETDATE()  " &
                        "where numCaseID = @caseid "

                    Dim p As New SqlParameter("@caseid", txtCaseID.Text)

                    DB.RunCommand(SQL, p)
                End If

                SQL = "select " &
                "case " &
                "when (select max(numActionID) from SBEAPActionLog) is Null then 1 " &
                "else (select max(numActionID) + 1 from SBEAPActionLog)   " &
                "end ActionNumber "

                txtActionID.Text = DB.GetString(SQL)

                SQL = "INSERT INTO SBEAPACTIONLOG 
                    (NUMACTIONID, NUMCASEID, NUMACTIONTYPE, NUMMODIFINGSTAFF, 
                    DATMODIFINGDATE, STRCREATINGSTAFF, DATCREATIONDATE, DATACTIONOCCURED)
                    VALUES 
                    (@NUMACTIONID, @NUMCASEID, @NUMACTIONTYPE, @NUMMODIFINGSTAFF, 
                    GETDATE(), @STRCREATINGSTAFF, GETDATE(), @DATACTIONOCCURED)"

                Dim p2 As SqlParameter() = {
                    New SqlParameter("@NUMACTIONID", txtActionID.Text),
                    New SqlParameter("@NUMCASEID", txtCaseID.Text),
                    New SqlParameter("@NUMACTIONTYPE", cboActionType.SelectedValue),
                    New SqlParameter("@NUMMODIFINGSTAFF", CurrentUser.UserID),
                    New SqlParameter("@STRCREATINGSTAFF", CurrentUser.UserID),
                    New SqlParameter("@DATACTIONOCCURED", DTPActionOccured.Text)
                }

                DB.RunCommand(SQL, p2)

                LoadActionLog()
                txtActionType.Text = cboActionType.Text
                LoadActionTab()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub txtCaseID_TextChanged(sender As Object, e As EventArgs) Handles txtCaseID.TextChanged
        If IsNumeric(txtCaseID.Text) = True Then
            btnAddNewAction.Enabled = True
            btnViewActionType.Enabled = True
        Else
            btnAddNewAction.Enabled = False
            btnViewActionType.Enabled = False
        End If
    End Sub

    Private Sub dgvActionLog_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvActionLog.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvActionLog.HitTest(e.X, e.Y)
            If dgvActionLog.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvActionLog.Columns(0).HeaderText = "Action ID" Then
                    txtActionID.Text = dgvActionLog(0, hti.RowIndex).Value
                    txtActionType.Text = dgvActionLog(2, hti.RowIndex).Value
                    txtCreationDate.Text = Format(dgvActionLog(3, hti.RowIndex).Value, "dd-MMM-yyyy")
                    If IsDBNull(dgvActionLog(4, hti.RowIndex).Value) Then
                        DTPActionOccured.Value = Today
                    Else
                        DTPActionOccured.Text = Format(dgvActionLog(4, hti.RowIndex).Value, "dd-MMM-yyyy")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewActionType_Click(sender As Object, e As EventArgs) Handles btnViewActionType.Click
        LoadActionTab()
    End Sub

    Private Sub btnClearActions_Click(sender As Object, e As EventArgs) Handles btnClearActions.Click
        ClearActions()
        txtActionID.Clear()
        txtCreationDate.Clear()
        txtActionType.Clear()
        TCCaseSpecificData.Visible = False
    End Sub

    Private Sub btnDeleteAction_Click(sender As Object, e As EventArgs) Handles btnDeleteAction.Click
        Try
            Dim Result As DialogResult
            Dim SQL As String = ""
            If txtActionID.Text <> "" Then
                Result = MessageBox.Show("Are you certain that you want to delete this Action?",
                  "Action Delete", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                Select Case Result
                    Case DialogResult.Yes
                        Select Case txtActionType.Text
                            Case "Compliance Assistance"

                            Case "Permit Assistance"
                                SQL = "delete SBEAPTechnicalAssist " &
                                "where numActionID = @actionid "
                            Case "Phone Call Made/Received"
                                SQL = "delete SBEAPPhoneLog " &
                                "where numActionID = @actionid "
                            Case "Meeting/Conferences Attended"
                                SQL = "delete SBEAPConferenceLog " &
                                "where numActionID = @actionid "
                            Case Else
                                SQL = "delete SBEAPOtherLog " &
                                "where numActionID = @actionid "
                        End Select

                        Dim p As New SqlParameter("@actionid", txtActionID.Text)

                        If SQL <> "" Then
                            DB.RunCommand(SQL, p)
                        End If

                        SQL = "delete SBEAPActionLog " &
                            "where numActionID = @actionid "

                        DB.RunCommand(SQL, p)

                        ClearActions()
                        txtActionID.Clear()
                        txtActionType.Clear()
                        txtCreationDate.Clear()
                        LoadActionLog()
                        TCCaseSpecificData.Visible = False
                End Select
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mmiAddNewClient_Click(sender As Object, e As EventArgs) Handles mmiAddNewClient.Click
        Try
            If ClientSummary IsNot Nothing Then
                ClientSummary.Dispose()
            End If
            ClientSummary = New SBEAPClientSummary

            If ClientSummary IsNot Nothing AndAlso Not ClientSummary.IsDisposed Then
                ClientSummary.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub tsbClearFrom_Click(sender As Object, e As EventArgs) Handles tsbClearFrom.Click
        ClearForm()
    End Sub

    Private Sub rdbSingleClient_CheckedChanged(sender As Object, e As EventArgs) Handles rdbSingleClient.CheckedChanged
        If rdbSingleClient.Checked = True Then
            pnlSingleClient.Visible = True
            pnlMultiClient.Visible = False
        Else
            If rdbMultiClient.Checked = True Then
                pnlMultiClient.Visible = True
                pnlSingleClient.Visible = False
            Else
                pnlSingleClient.Visible = False
                pnlMultiClient.Visible = False
            End If
        End If
    End Sub

    Private Sub rdbMultiClient_CheckedChanged(sender As Object, e As EventArgs) Handles rdbMultiClient.CheckedChanged
        If rdbMultiClient.Checked = True Then
            pnlMultiClient.Visible = True
            pnlSingleClient.Visible = False
        Else
            If rdbSingleClient.Checked = True Then
                pnlSingleClient.Visible = True
                pnlMultiClient.Visible = False
            Else
                pnlSingleClient.Visible = False
                pnlMultiClient.Visible = False
            End If
        End If
    End Sub

    Private Sub btnAddClients_Click(sender As Object, e As EventArgs) Handles btnAddClients.Click
        Try
            Dim Client As String = ""

            If txtAddMultiClient.Text <> "" Then
                Do While txtAddMultiClient.Text <> ""
                    If txtAddMultiClient.Text.StartsWith(vbCrLf) Then
                        txtAddMultiClient.Text = Mid(txtAddMultiClient.Text, 3)
                    End If
                    If txtAddMultiClient.Text.Length < 8 Then
                        txtAddMultiClient.Text = ""
                    Else
                        Client = Mid(txtAddMultiClient.Text, 1, 9)
                        txtMultiClientList.Text = txtMultiClientList.Text & Client & vbCrLf
                        txtAddMultiClient.Text = Replace(Replace(Replace(txtAddMultiClient.Text, Client, ""), ",", ""), ".", "")
                    End If
                Loop
            End If
            txtAddMultiClient.Clear()
            LoadMultiClientList()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRemoveClient_Click(sender As Object, e As EventArgs) Handles btnRemoveClient.Click
        Try
            If txtDeleteClient.Text.Length = 8 Then
                txtMultiClientList.Text = Replace(txtMultiClientList.Text, (txtDeleteClient.Text & vbCrLf), "")
            End If
            txtMultiClients.Clear()
            LoadMultiClientList()
            txtDeleteClient.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DeleteCase()
        Try
            Dim Result As DialogResult
            Dim ActionID As String = ""
            Dim ActionType As String = ""
            Dim SQL As String = ""
            If txtCaseID.Text <> "" Then
                Result = MessageBox.Show("Are you certain that you want to delete this Case?",
                  "Case Delete", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                Select Case Result
                    Case DialogResult.Yes
                        Dim p As New SqlParameter("@caseid", txtCaseID.Text)

                        Do While ActionID <> "Done"
                            ActionID = "Done"

                            SQL = "Select " &
                                "numActionID, numActionType " &
                                "from SBEAPActionLog " &
                                "where numCaseID = @caseid "

                            Dim dr As DataRow = DB.GetDataRow(SQL, p)

                            If dr IsNot Nothing Then
                                If IsDBNull(dr.Item("numActionID")) Then
                                    ActionID = "Done"
                                Else
                                    ActionID = dr.Item("numActionID")
                                End If
                                If IsDBNull(dr.Item("numActionType")) Then
                                    ActionType = "None"
                                Else
                                    ActionType = dr.Item("numActionType")
                                End If
                            End If

                            If ActionID <> "" And ActionID <> "Done" Then
                                Dim p2 As New SqlParameter("@actionid", ActionID)

                                Select Case ActionType
                                    Case "4"
                                        SQL = "Delete SBEAPConferenceLog " &
                                            "where numActionID = @actionid "
                                        DB.RunCommand(SQL, p2)

                                        SQL = "Delete SBEAPActionLog " &
                                            "where numActionID = @actionid "
                                        DB.RunCommand(SQL, p2)

                                    Case "6"
                                        SQL = "Delete SBEAPPhoneLog " &
                                            "where numActionID = @actionid "
                                        DB.RunCommand(SQL, p2)

                                        SQL = "Delete SBEAPActionLog " &
                                            "where numActionID = @actionid "
                                        DB.RunCommand(SQL, p2)

                                    Case "10"
                                        SQL = "Delete SBEAPTechnicalAssist " &
                                            "where numActionID = @actionid "
                                        DB.RunCommand(SQL, p2)

                                        SQL = "Delete SBEAPActionLog " &
                                            "where numActionID = @actionid "
                                        DB.RunCommand(SQL, p2)

                                    Case "1" Or "2" Or "3" Or "5" Or "7" Or "8" Or "9" Or "11" Or "12"
                                        SQL = "Delete SBEAPOtherLog " &
                                            "where numActionID = @actionid "
                                        DB.RunCommand(SQL, p2)

                                        SQL = "Delete SBEAPActionLog " &
                                            "where numActionID = @actionid "
                                        DB.RunCommand(SQL, p2)

                                    Case Else
                                        SQL = "Delete SBEAPConferenceLog " &
                                            "where numActionID = @actionid "
                                        DB.RunCommand(SQL, p2)

                                        SQL = "Delete SBEAPPhoneLog " &
                                            "where numActionID = @actionid "
                                        DB.RunCommand(SQL, p2)

                                        SQL = "Delete SBEAPTechnicalAssist " &
                                            "where numActionID = @actionid "
                                        DB.RunCommand(SQL, p2)

                                        SQL = "Delete SBEAPOtherLog " &
                                            "where numActionID = @actionid "
                                        DB.RunCommand(SQL, p2)

                                        SQL = "Delete SBEAPActionLog " &
                                            "where numActionID = @actionid "
                                        DB.RunCommand(SQL, p2)

                                End Select
                            End If
                        Loop

                        SQL = "Delete SBEAPCaseLog " &
                            "where numCaseID = @caseid "
                        DB.RunCommand(SQL, p)

                        SQL = "Delete SBEAPCaseLogLink " &
                            "where numCaseID = @caseid "
                        DB.RunCommand(SQL, p)

                        ClearForm()
                End Select
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub tsmDeleteCaseWork_Click(sender As Object, e As EventArgs) Handles tsmDeleteCaseWork.Click
        If txtCaseID.Text <> "" Then
            DeleteCase()
        End If
    End Sub

End Class