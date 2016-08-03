Imports System.Data.SqlClient

Public Class SBEAPCaseWork
    Dim SQL, SQL2 As String
    Dim dsCaseWork As DataSet
    Dim daCaseWork As SqlDataAdapter
    Dim dsStaff As DataSet
    Dim daStaff As SqlDataAdapter
    Dim dsStaffList As DataSet
    Dim daStaffList As SqlDataAdapter
    Dim dsActionLog As DataSet
    Dim daActionLog As SqlDataAdapter
    Dim temp As String
    Dim i As Integer

    Public WriteOnly Property ValueFromClientLookUp() As String
        Set(Value As String)
            txtClientID.Text = Value
        End Set
    End Property

    Private Sub SBEAPCaseLog_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

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

            Label1.Text = "Enter Case Work data..."
            Label2.Text = CurrentUser.AlphaName
            Label3.Text = OracleDate

            FormStatus("Enable")

            TCCaseSpecificData.Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Page Load"
    Sub LoadComboBoxes()
        Try
            Dim dtCaseLog As New DataTable
            Dim dtStaff As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow
            Dim drNewRow2 As DataRow

            dsCaseWork = New DataSet

            SQL = "Select " &
            "numActionType, strWorkDescription " &
            "from LookUpSBEAPCaseWork " &
            "order by strWorkDescription "

            daCaseWork = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daCaseWork.Fill(dsCaseWork, "CaseWork")

            dtCaseLog.Columns.Add("numActionType", GetType(System.String))
            dtCaseLog.Columns.Add("strWorkDescription", GetType(System.String))

            drNewRow = dtCaseLog.NewRow
            drNewRow("numActionType") = ""
            drNewRow("strWorkDescription") = ""
            dtCaseLog.Rows.Add(drNewRow)

            For Each drDSRow In dsCaseWork.Tables("CaseWork").Rows()
                drNewRow = dtCaseLog.NewRow
                drNewRow("numActionType") = drDSRow("numActionType")
                drNewRow("strWorkDescription") = drDSRow("strWorkDescription")
                dtCaseLog.Rows.Add(drNewRow)
            Next

            With cboActionType
                .DataSource = dtCaseLog
                .DisplayMember = "strWorkDescription"
                .ValueMember = "numActionType"
            End With

            SQL = "Select strOfficeName " &
            "from LookUpDistrictOffice " &
            "Union " &
            "select 'APB' " &
            "from dual " &
            "Union " &
            "select 'LBP' " &
            "from dual " &
            "Union " &
            "select 'WPB' " &
            "from dual " &
            "Union " &
            "select 'P2AD' " &
            "from dual " &
            "Union " &
            "select 'GDED' " &
            "from dual " &
            "Union " &
            "select 'GEFA' " &
            "from dual " &
            "Union " &
            "select 'NSBEAP' " &
            "from dual "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.cboInteragency.Items.Add(dr.Item("strOfficeName"))
            End While
            dr.Close()

            dsStaff = New DataSet

            SQL = "select " &
            "NumUserID, " &
            "(strLastName||', '||strFirstName) as UserName " &
            "from EPDUserProfiles " &
            "where numBranch = '5' " &
            "and numProgram = '35' " &
            "union " &
            "select " &
            "distinct(NumUserID) as NumUserID, " &
            "(strLastName||', '||strFirstName) as UserName " &
            "from EPDUserProfiles, SBEAPCaseLog " &
            "where EPDUserProfiles.numUserID = SBEAPCaseLog.numStaffResponsible " &
            "Order by UserName "

            daStaff = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStaff.Fill(dsStaff, "Staff")

            dtStaff.Columns.Add("NumUserID", GetType(System.String))
            dtStaff.Columns.Add("UserName", GetType(System.String))

            drNewRow2 = dtStaff.NewRow
            drNewRow2("NumUserID") = ""
            drNewRow2("UserName") = ""
            dtStaff.Rows.Add(drNewRow2)

            For Each drDSRow In dsStaff.Tables("Staff").Rows()
                drNewRow2 = dtStaff.NewRow
                drNewRow2("NumUserID") = drDSRow("NumUserID")
                drNewRow2("UserName") = drDSRow("UserName")
                dtStaff.Rows.Add(drNewRow2)
            Next

            With cboStaffResponsible
                .DataSource = dtStaff
                .DisplayMember = "UserName"
                .ValueMember = "NumUserID"
            End With

            cboTechAssistType.Items.Add(" ")
            cboTechAssistType.Items.Add("1 - Easy")
            cboTechAssistType.Items.Add("2 - Medium")
            cboTechAssistType.Items.Add("3 - Hard")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub FormStatus(Status As String)
        Try
            If Status = "Disable" Then
                tsbSave.Enabled = False
                txtCaseID.Enabled = False
                txtClientID.Enabled = False
                btnRefreshClient.Enabled = False
                cboStaffResponsible.Enabled = False
                cboActionType.Enabled = False
                cboInteragency.Enabled = False
                DTPCaseOpened.Enabled = False
                DTPCaseClosed.Enabled = False
                TCCaseSpecificData.Enabled = False
                btnDeleteAction.Enabled = False
                btnAddNewAction.Enabled = False
                txtCaseDescription.Enabled = False
                txtReferralInformation.Enabled = False
                DTPReferralDate.Enabled = False
            Else
                tsbSave.Enabled = True
                txtCaseID.Enabled = True
                txtClientID.Enabled = True
                btnRefreshClient.Enabled = True
                cboStaffResponsible.Enabled = True
                cboActionType.Enabled = True
                cboInteragency.Enabled = True
                DTPCaseOpened.Enabled = True
                DTPCaseClosed.Enabled = True
                TCCaseSpecificData.Enabled = True
                btnDeleteAction.Enabled = True
                btnAddNewAction.Enabled = True
                txtCaseDescription.Enabled = True
                txtReferralInformation.Enabled = True
                DTPReferralDate.Enabled = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#Region "Subs and Functions"
    Sub LoadClientInfo()
        Try
            Dim ClientID As String = ""
            Dim CompanyName As String = ""
            Dim CompanyAddress As String = ""
            Dim County As String = ""

            SQL = "select " &
            "clientID, " &
            "strCompanyName, " &
            "strCompanyAddress, " &
            "strCompanyCity, " &
            "strCompanyState, " &
            "strCompanyZipCode, " &
            "strCountyName " &
            "from SBEAPClients, LookUpCountyInformation " &
            "where SBEAPClients.strCompanyCounty = LookUpCountyInformation.strCountyCode (+) " &
            "and ClientId = '" & txtClientID.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
            End While
            dr.Close()
            txtClientInformation.Text = ClientID & CompanyName & CompanyAddress & County

            SQL = "select " &
            "count(*) as Outstanding " &
            "from SBEAPCaseLog " &
            "where ClientID = '" & txtClientID.Text & "' " &
            "and datCaseClosed is null"
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("Outstanding")) Then
                    txtOutstandingCases.Text = "0"
                Else
                    txtOutstandingCases.Text = dr.Item("Outstanding")
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadCaseLogData()
        Try
            If txtCaseID.Text <> "" Then
                SQL = "Select " &
                "numStaffResponsible, datCaseOpened, " &
                "strCaseSummary, " &
                "datCaseClosed, " &
                "case " &
                "when numModifingStaff is Null then ' ' " &
                "else (strLastName|| ', '||strFirstName) " &
                "END ModifingStaff, " &
                "numModifingStaff, " &
                "datModifingDate, " &
                "strInterAgency, strReferralComments, " &
                "datReferralDate, strComplaintBased, " &
                "strCaseClosureLetterSent " &
                "from SBEAPCaseLog, EPDUserProfiles " &
                "where SBEAPCaseLog.numModifingStaff = EPDUserProfiles.numUserID (+) " &
                "and numCaseID = '" & txtCaseID.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("numStaffResponsible")) Then
                        cboStaffResponsible.SelectedValue = 0
                    Else
                        cboStaffResponsible.SelectedValue = dr.Item("numStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("datCaseOpened")) Then
                        DTPCaseOpened.Text = OracleDate
                    Else
                        DTPCaseOpened.Text = dr.Item("datCaseOpened")
                    End If
                    If IsDBNull(dr.Item("datCaseClosed")) Then
                        temp = "Data Load"
                        DTPCaseClosed.Text = OracleDate
                        DTPCaseClosed.Checked = False
                        temp = ""
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
                        DTPLastModified.Text = OracleDate
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
                        DTPReferralDate.Text = OracleDate
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
                End While
                dr.Close()

                LoadActionLog()
                If txtActionCount.Text = "1" Then
                    txtActionID.Text = dgvActionLog(0, 0).Value
                    txtActionType.Text = dgvActionLog(2, 0).Value
                    txtCreationDate.Text = dgvActionLog(3, 0).Value
                    LoadActionTab()
                End If

                SQL = "Select " &
                "count(*) as ClientCount " &
                "from SBEAPCaseLogLink " &
                "where numCaseID = '" & txtCaseID.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("ClientCount")) Then
                        temp = "0"
                    Else
                        temp = dr.Item("ClientCount")
                    End If
                End While
                dr.Close()

                Select Case temp
                    Case 0

                    Case 1
                        rdbSingleClient.Checked = True

                        SQL = "Select " &
                        "ClientID " &
                        "from SBEAPCaseLogLink " &
                        "where numCaseID = '" & txtCaseID.Text & "' "

                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("ClientID")) Then
                                txtClientID.Text = ""
                            Else
                                txtClientID.Text = dr.Item("ClientID")
                            End If
                        End While
                        dr.Close()
                        If txtClientID.Text <> "" Then
                            LoadClientInfo()
                        End If
                    Case Else
                        rdbMultiClient.Checked = True
                        txtMultiClientList.Clear()

                        SQL = "Select " &
                        "ClientID " &
                        "from SBEAPCaseLogLink " &
                        "where numCaseID = '" & txtCaseID.Text & "' "

                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("ClientID")) Then
                                txtMultiClientList.Text = txtMultiClientList.Text
                            Else
                                txtMultiClientList.Text = txtMultiClientList.Text & dr.Item("ClientID") & vbCrLf
                            End If
                        End While
                        dr.Close()

                        LoadMultiClientList()
                End Select
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadActionLog()
        Try
            SQL = "select " &
            "numActionID, " &
            "numCaseID, " &
            "case " &
            "when SBEAPActionLog.numActionType is null then ' ' " &
            "else strWorkDescription " &
            "end ActionDescription, " &
            "to_date(datCreationDate, 'dd-Mon-RRRR') as CreationDate,  " &
            "to_date(datActionOccured, 'dd-Mon-RRRR') as OccuredDate " &
            "from SBEAPActionLog, LookUpSBEAPCaseWork " &
            "where SBEAPActionLog.numActionType = LookUpSBEAPCaseWork.numActionType (+)  " &
            "and SBEAPActionLog.numCaseID = '" & txtCaseID.Text & "' " &
            "order by numActionID desc "

            dsActionLog = New DataSet
            daActionLog = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daActionLog.Fill(dsActionLog, "ActionLog")

            dgvActionLog.DataSource = dsActionLog
            dgvActionLog.DataMember = "ActionLog"

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

            dgvActionLog.Columns("OccuredDate").HeaderText = "Date Action Occured"
            dgvActionLog.Columns("OccuredDate").DisplayIndex = 0
            dgvActionLog.Columns("OccuredDate").Width = 100
            dgvActionLog.Columns("OccuredDate").DefaultCellStyle.Format = "dd-MMM-yyyy"

            txtActionCount.Text = dgvActionLog.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadActionTab()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadAttendingStaff()
        Try
            'clbStaffAttending
            Dim dtStaffList As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "select " &
            "NumUserID, " &
            "(strLastName||', '||strFirstName) as UserName " &
            "from EPDUserProfiles " &
            "where numBranch = '5' " &
            "and numProgram = '35' "

            dsStaffList = New DataSet

            daStaffList = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStaffList.Fill(dsStaffList, "StaffList")

            dtStaffList.Columns.Add("NumUserID", GetType(System.String))
            dtStaffList.Columns.Add("UserName", GetType(System.String))

            For Each drDSRow In dsStaff.Tables("Staff").Rows()
                drNewRow = dtStaffList.NewRow
                drNewRow("NumUserID") = drDSRow("NumUserID")
                drNewRow("UserName") = drDSRow("UserName")
                dtStaffList.Rows.Add(drNewRow)
            Next

            With clbStaffAttending
                .DataSource = dtStaffList
                .DisplayMember = "UserName"
                .ValueMember = "NumUserID"
            End With
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadComplianceAssist()
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

            SQL = "Select " &
            "strAIRAssist, strStormWaterAssist, " &
            "strHazWasteAssist, strSolidWasteAssist, " &
            "strUSTAssist, strScrapTireAssist, " &
            "strLeadAssist, strOtherAssist, " &
            "strComment, strModifingStaff, " &
            "datModifingDate " &
            "from SBEAPComplianceAssist " &
            "where numActionID = '" & txtActionID.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
                If IsDBNull(dr.Item("strComment")) Then
                    txtComplianceAssistanceComments.Clear()
                Else
                    txtComplianceAssistanceComments.Text = dr.Item("strComment")
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadTechnicalAssist()
        Try
            Dim AssistanceRequest As String = ""

            SQL = "Select " &
            "strTechnicalAssistType, datInitialContactDate, " &
            "datAssistStartDate, datAssistEndDate, " &
            "strAssistanceRequest, strAIRSnumber, " &
            "strTechnicalAssistNotes " &
            "from SBEAPTechnicalAssist " &
            "where numActionID = '" & txtActionID.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strTechnicalAssistType")) Then
                    cboTechAssistType.Text = " "
                Else
                    cboTechAssistType.Text = dr.Item("strTEchnicalAssistType")
                End If
                If IsDBNull(dr.Item("datInitialContactDate")) Then
                    DTPTechAssistInitialContact.Text = OracleDate
                    DTPTechAssistInitialContact.Checked = False
                Else
                    DTPTechAssistInitialContact.Text = dr.Item("datInitialContactDate")
                    DTPTechAssistInitialContact.Checked = True
                End If
                If IsDBNull(dr.Item("datAssistStartDate")) Then
                    DTPTechAssistStart.Text = OracleDate
                    DTPTechAssistStart.Checked = False
                Else
                    DTPTechAssistStart.Text = dr.Item("datAssistStartDate")
                    DTPTechAssistStart.Checked = True
                End If
                If IsDBNull(dr.Item("datAssistEndDate")) Then
                    DTPTechAssistEnd.Text = OracleDate
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
            End While
            dr.Close()

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadPhoneCall()
        Try
            SQL = "Select " &
            "strCallerInformation, " &
            "numCallerPhoneNumber, " &
            "strPhoneLogNotes, " &
            "strOneTimeAssist, " &
            "strFrontDeskCall " &
            "from SBEAPPhoneLog " &
            "where numActionID = '" & txtActionID.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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

            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadConference()
        Try
            Dim StaffAttending As String = ""

            SQL = "Select " &
            "strConferenceAttended, strConferenceLocation, " &
            "strConferenceTopic, strAttendees, " &
            "datConferenceStarted, datConferenceEnded, " &
            "strSBEAPPresentation, strListOfBusinessSectors, " &
            "strCOnferenceFollowUp, strStaffAttending " &
            "from SBEAPConferenceLog " &
            "where numActionID = '" & txtActionID.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
                    DTPConferenceStart.Text = OracleDate
                Else
                    DTPConferenceStart.Text = dr.Item("datConferenceStarted")
                End If
                If IsDBNull(dr.Item("datConferenceEnded")) Then
                    DTPConferenceEnd.Text = OracleDate
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
            End While
            dr.Close()

            If StaffAttending <> "" Then
                Do While StaffAttending <> ""
                    If StaffAttending.Contains(",") Then
                        temp = Mid(StaffAttending, 1, (InStr(StaffAttending, ",") - 1))
                    Else
                        temp = StaffAttending
                    End If
                    clbStaffAttending.SelectedValue = temp
                    i = clbStaffAttending.SelectedIndex
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadOther()
        Try
            SQL = "Select " &
            "strCaseNotes " &
            "from SBEAPOtherLog " &
            "where numActionID = '" & txtActionID.Text & "' "
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            txtCaseNotes.Clear()
            While dr.Read
                If IsDBNull(dr.Item("strCaseNotes")) Then
                    txtCaseNotes.Clear()
                Else
                    txtCaseNotes.Text = dr.Item("strCaseNotes")
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub UpdateCaseLog(Origin As String)
        Try
            Dim ClientList As String = ""
            Dim Staff As String = ""
            Dim ClientID As String = ""
            Dim CloseDate As String = ""
            Dim InterAgency As String = ""
            Dim ReferralComments As String = ""
            Dim ReferralDate As String = ""
            Dim ComplaintBased As String = ""
            Dim CaseClosedLetter As String = ""

            If cboStaffResponsible.Text <> "" Then
                Staff = cboStaffResponsible.SelectedValue
            Else
                Staff = ""
            End If
            If DTPCaseClosed.Checked = True Then
                CloseDate = DTPCaseClosed.Text
            Else
                CloseDate = ""
            End If
            If cboInteragency.Text <> "" Then
                InterAgency = cboInteragency.Text
            Else
                InterAgency = ""
            End If
            If txtReferralInformation.Text <> "" Then
                ReferralComments = txtReferralInformation.Text
            Else
                ReferralComments = ""
            End If
            If DTPReferralDate.Checked = True Then
                ReferralDate = DTPReferralDate.Text
            Else
                ReferralDate = ""
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
                "values " &
                "((Select " &
                "case " &
                "when (select max(numCaseID) from SBEAPCaseLog) is Null then 1 " &
                "else (select max(numCaseID) + 1 from SBEAPCaseLog) " &
                "End CaseID " &
                "from dual), " &
                "'" & Staff & "', '" & DTPCaseOpened.Text & "', " &
                "'" & Replace(txtCaseDescription.Text, "'", "''") & "', " &
                "'', '" & CloseDate & "', " &
                "'" & CurrentUser.UserID & "', '" & OracleDate & "', " &
                "'" & Replace(InterAgency, "'", "''") & "', '" & Replace(ReferralComments, "'", "''") & "', " &
                "'" & ReferralDate & "', '" & ComplaintBased & "', " &
                "'" & CaseClosedLetter & "') "

                SQL2 = "Select max(numCaseID) as CaseID from SBEAPCaseLog "
            Else
                SQL = "Update SBEAPCaseLog set " &
                "numStaffResponsible = '" & Staff & "', " &
                "datCaseOpened = '" & DTPCaseOpened.Text & "', " &
                "strCaseSummary = '" & Replace(txtCaseDescription.Text, "'", "''") & "', " &
                "datCaseClosed = '" & CloseDate & "', " &
                "numModifingStaff = '" & CurrentUser.UserID & "', " &
                "datModifingDate = '" & OracleDate & "', " &
                "strInterAgency = '" & Replace(InterAgency, "'", "''") & "', " &
                "strReferralComments = '" & Replace(ReferralComments, "'", "''") & "', " &
                "datReferralDate = '" & ReferralDate & "', " &
                "strComplaintBased = '" & ComplaintBased & "', " &
                "strCaseClosureLetterSent = '" & CaseClosedLetter & "' " &
                "where numCaseID = '" & txtCaseID.Text & "' "

                SQL2 = ""
            End If
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            If SQL2 <> "" Then
                cmd = New SqlCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("CaseID")) Then
                        txtCaseID.Text = ""
                    Else
                        txtCaseID.Text = dr.Item("CaseID")
                    End If
                End While
                dr.Close()
            End If

            If rdbSingleClient.Checked = True Then
                If txtClientID.Text <> "" Then
                    ClientID = txtClientID.Text
                Else
                    ClientID = ""
                End If

                SQL = "Delete SBEAPCaseLogLink " &
                "where numCaseID = '" & txtCaseID.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Insert into SBEAPCaseLogLink " &
                "values " &
                "('" & txtCaseID.Text & "', '" & ClientID & "') "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Else
                ClientList = txtMultiClientList.Text

                If ClientList <> "" Then
                    SQL = "Delete SBEAPCaseLogLink " &
                    "where numCaseID = '" & txtCaseID.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

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

                        If ClientID <> "" Then
                            SQL = "Insert into SBEAPCaseLogLink " &
                            "values " &
                            "('" & txtCaseID.Text & "', '" & ClientID & "') "
                            cmd = New SqlCommand(SQL, CurrentConnection)

                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()
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
            ErrorReport(ex, SQL, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub SaveActionData()
        Try
            Dim ActionOccured As String = DTPActionOccured.Text

            SQL = "Select " &
            "numActionId " &
            "from SBEAPActionLog " &
            "where numActionID = '" & txtActionID.Text & "' "
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Update SBEAPActionLog set " &
                "datActionOccured = '" & ActionOccured & "' " &
                "where numActionId = '" & txtActionID.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SaveComplianceAssist()
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

            SQL = "Select " &
            "numActionID " &
            "from SBEAPComplianceAssist " &
            "where numActionID = '" & txtActionID.Text & "' "
            cmd = New SqlCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = False Then
                SQL = "Insert into SBEAPComplianceAssist " &
                "values " &
                "('" & txtActionID.Text & "', " &
                "'" & Replace(AirAssist, "'", "''") & "', " &
                "'" & Replace(StormWaterAssist, "'", "''") & "', " &
                "'" & Replace(HazWasteAssist, "'", "''") & "', " &
                "'" & Replace(SolidWasteAssist, "'", "''") & "', " &
                "'" & Replace(USTAssist, "'", "''") & "', " &
                "'" & Replace(ScrapTireAssist, "'", "''") & "', " &
                "'" & Replace(LeadAssist, "'", "''") & "', " &
                "'" & Replace(OtherAssist, "'", "''") & "', " &
                "'" & Replace(Comments, "'", "''") & "', " &
                "'" & CurrentUser.UserID & "', '" & OracleDate & "') "
            Else
                SQL = "Update SBEAPComplianceAssist set " &
                "strAirAssist = '" & Replace(AirAssist, "'", "''") & "', " &
                "strStormWaterAssist = '" & StormWaterAssist & "', " &
                "strHazWasteAssist = '" & HazWasteAssist & "', " &
                "strSolidWasteAssist = '" & SolidWasteAssist & "', " &
                "strUSTAssist = '" & USTAssist & "', " &
                "strScrapTireAssist = '" & Replace(ScrapTireAssist, "'", "''") & "', " &
                "strLeadAssist = '" & Replace(LeadAssist, "'", "''") & "', " &
                "strOtherAssist = '" & Replace(OtherAssist, "'", "''") & "', " &
                "strComment =  '" & Replace(Comments, "'", "''") & "', " &
                "strModifingStaff = '" & CurrentUser.UserID & "', " &
                "datModifingDate = '" & OracleDate & "' " &
                "where numActionID = '" & txtActionID.Text & "' "
            End If
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()


        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SaveTechnicalAssist()
        Try
            Dim AssistType As String = ""
            Dim ContactDate As String = ""
            Dim AssistStart As String = ""
            Dim AssistEnd As String = ""
            Dim AssistRequest As String = ""
            Dim AIRSNumber As String = ""
            Dim TechnicalAssistComments As String = ""

            If cboTechAssistType.Text <> "" Then
                AssistType = cboTechAssistType.Text
            Else
                AssistType = ""
            End If
            If DTPTechAssistInitialContact.Checked = True Then
                ContactDate = DTPTechAssistInitialContact.Text
            Else
                ContactDate = ""
            End If
            If DTPTechAssistStart.Checked = True Then
                AssistStart = DTPTechAssistStart.Text
            Else
                AssistStart = ""
            End If
            If DTPTechAssistEnd.Checked = True Then
                AssistEnd = DTPTechAssistEnd.Text
            Else
                AssistEnd = ""
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
                AIRSNumber = ""
            End If
            If txtTechnicalAssistNotes.Text <> "" Then
                TechnicalAssistComments = txtTechnicalAssistNotes.Text
            Else
                TechnicalAssistComments = ""
            End If

            SQL = "Select " &
            "numActionID " &
            "from SBEAPTechnicalAssist " &
            "where numActionID = '" & txtActionID.Text & "' "
            cmd = New SqlCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = False Then
                SQL = "Insert into SBEAPTechnicalAssist " &
                "values " &
                "('" & txtActionID.Text & "', " &
                "'" & Replace(AssistType, "'", "''") & "', " &
                "'" & ContactDate & "', '" & AssistStart & "', " &
                "'" & AssistEnd & "', '" & AssistRequest & "', " &
                "'" & Replace(AIRSNumber, "'", "''") & "', " &
                "'" & Replace(TechnicalAssistComments, "'", "''") & "', " &
                "'" & CurrentUser.UserID & "', '" & OracleDate & "') "
            Else
                SQL = "Update SBEAPTechnicalAssist set " &
                "strTechnicalAssistType = '" & Replace(AssistType, "'", "''") & "', " &
                "datInitialContactDate = '" & ContactDate & "', " &
                "datAssistStartDate = '" & AssistStart & "', " &
                "datAssistEndDate = '" & AssistEnd & "', " &
                "strAssistanceRequest = '" & AssistRequest & "', " &
                "strAIRSNumber = '" & Replace(AIRSNumber, "'", "''") & "', " &
                "strTechnicalAssistNotes = '" & Replace(TechnicalAssistComments, "'", "''") & "', " &
                "strModifingStaff = '" & CurrentUser.UserID & "', " &
                "datModifingDate = '" & OracleDate & "' " &
                "where numActionID = '" & txtActionID.Text & "' "
            End If
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SavePhoneCall()
        Try
            Dim CallerInfo As String = ""
            Dim CallerPhone As String = ""
            Dim PhoneCallNotes As String = ""
            Dim OneTimeAssist As String = ""
            Dim FrontDeskCall As String = ""

            If txtCallName.Text <> "" Then
                CallerInfo = txtCallName.Text
            Else
                CallerInfo = ""
            End If
            If mtbPhoneNumber.Text <> "" Then
                CallerPhone = mtbPhoneNumber.Text
            Else
                CallerPhone = ""
            End If
            If txtPhoneCallNotes.Text <> "" Then
                PhoneCallNotes = txtPhoneCallNotes.Text
            Else
                PhoneCallNotes = ""
            End If
            If chbOnetimeAssist.Checked = True Then
                OneTimeAssist = "True"
            Else
                OneTimeAssist = "False"
            End If
            If chbFrontDeskCall.Checked = True Then
                FrontDeskCall = "True"
            Else
                FrontDeskCall = "False"
            End If


            SQL = "Select " &
            "numActionID " &
            "from SBEAPPhoneLog " &
            "where numActionID = '" & txtActionID.Text & "' "
            cmd = New SqlCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = False Then
                SQL = "Insert into SBEAPPhoneLog " &
                "values " &
                "('" & txtActionID.Text & "', '" & Replace(CallerInfo, "'", "''") & "', " &
                "'" & Replace(CallerPhone, "'", "''") & "', " &
                "'" & Replace(PhoneCallNotes, "'", "''") & "', " &
                "'" & OneTimeAssist & "', '" & FrontDeskCall & "', " &
                "'" & CurrentUser.UserID & "', '" & OracleDate & "') "
            Else
                SQL = "Update SBEAPPhoneLog set " &
                "strCallerInformation = '" & Replace(CallerInfo, "'", "''") & "', " &
                "numCallerPhoneNumber = '" & CallerPhone & "', " &
                "strPhoneLogNotes = '" & Replace(PhoneCallNotes, "'", "''") & "', " &
                "strOneTimeAssist = '" & OneTimeAssist & "', " &
                "strFrontDeskCall = '" & FrontDeskCall & "', " &
                "strModifingStaff = '" & CurrentUser.UserID & "', " &
                "datModifingDate = '" & OracleDate & "' " &
                "where numActionID = '" & txtActionID.Text & "' "
            End If
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SaveConference()
        Try
            Dim ConferenceAttended As String = ""
            Dim ConferenceLocation As String = ""
            Dim ConferenceTopic As String = ""
            Dim Attendees As String = ""
            Dim ConferenceStart As String = ""
            Dim ConferenceEnd As String = ""
            Dim SBEAPPresentation As String = ""
            Dim ListofBusinesses As String = ""
            Dim FollowUp As String = ""
            Dim StaffAttendies As String = ""
            Dim i As Integer

            If txtConferenceAttended.Text <> "" Then
                ConferenceAttended = txtConferenceAttended.Text
            Else
                ConferenceAttended = ""
            End If
            If txtConferenceLocation.Text <> "" Then
                ConferenceLocation = txtConferenceLocation.Text
            Else
                ConferenceLocation = ""
            End If
            If txtConferenceTopic.Text <> "" Then
                ConferenceTopic = txtConferenceTopic.Text
            Else
                ConferenceTopic = ""
            End If
            If txtConferenceAttendees.Text <> "" Then
                Attendees = txtConferenceAttendees.Text
            Else
                Attendees = ""
            End If
            ConferenceStart = DTPConferenceStart.Text
            If DTPConferenceEnd.Value < DTPConferenceStart.Value Then
                ConferenceEnd = ConferenceStart
            Else
                ConferenceEnd = DTPConferenceEnd.Text
            End If
            If rdbSBEAPPresentationYes.Checked = True Then
                SBEAPPresentation = "True"
            Else
                SBEAPPresentation = "False"
            End If
            If txtListOfBusinessSectors.Text <> "" Then
                ListofBusinesses = txtListOfBusinessSectors.Text
            Else
                ListofBusinesses = ""
            End If
            If txtConferenceFollowUp.Text <> "" Then
                FollowUp = txtConferenceFollowUp.Text
            Else
                FollowUp = ""
            End If

            For i = 0 To clbStaffAttending.Items.Count - 1
                temp = i
                If clbStaffAttending.GetItemChecked(i) = True Then
                    clbStaffAttending.SelectedIndex = i
                    StaffAttendies = StaffAttendies & clbStaffAttending.SelectedValue & ","
                End If
            Next
            If StaffAttendies <> "" Then
                StaffAttendies = Mid(StaffAttendies, 1, (StaffAttendies.Length - 1))
            End If

            SQL = "Select " &
            "numActionID " &
            "from SBEAPConferenceLog " &
            "where numActionID = '" & txtActionID.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = False Then
                SQL = "Insert into SBEAPConferenceLog " &
                "values " &
                "('" & txtActionID.Text & "', '" & Replace(ConferenceAttended, "'", "''") & "', " &
                "'" & Replace(ConferenceLocation, "'", "''") & "', '" & Replace(ConferenceTopic, "'", "''") & "', " &
                "'" & Replace(Attendees, "'", "''") & "', '" & Replace(ConferenceStart, "'", "''") & "', " &
                "'" & Replace(ConferenceEnd, "'", "'''") & "', '" & Replace(SBEAPPresentation, "'", "''") & "', " &
                "'" & Replace(ListofBusinesses, "'", "''") & "', '" & Replace(FollowUp, "'", "''") & "', " &
                "'" & StaffAttendies & "', '" & CurrentUser.UserID & "', '" & OracleDate & "') "
            Else
                SQL = "Update SBEAPConferenceLog set " &
                "strConferenceAttended = '" & Replace(ConferenceAttended, "'", "''") & "', " &
                "strConferenceLocation = '" & Replace(ConferenceLocation, "'", "''") & "', " &
                "strConferenceTopic = '" & Replace(ConferenceTopic, "'", "''") & "', " &
                "strAttendees = '" & Replace(Attendees, "'", "''") & "', " &
                "datConferenceStarted = '" & ConferenceStart & "', " &
                "datConferenceEnded = '" & ConferenceEnd & "', " &
                "strSBEAPPresentation = '" & Replace(SBEAPPresentation, "'", "''") & "', " &
                "strListOfBusinessSectors = '" & Replace(ListofBusinesses, "'", "''") & "', " &
                "strConferenceFollowUp = '" & Replace(FollowUp, "'", "''") & "', " &
                "strStaffAttending = '" & StaffAttendies & "', " &
                "strModifingStaff = '" & CurrentUser.UserID & "', " &
                "datModifingDate = '" & OracleDate & "' " &
                "where numActionID = '" & txtActionID.Text & "' "
            End If

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SaveOther()
        Try
            Dim CaseNotes As String = ""

            If txtCaseNotes.Text <> "" Then
                CaseNotes = txtCaseNotes.Text
            Else
                CaseNotes = ""
            End If

            SQL = "Select " &
            "numActionID " &
            "from SBEAPOtherLog " &
            "where numActionID = '" & txtActionID.Text & "' "
            cmd = New SqlCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = False Then
                SQL = "Insert into SBEAPOtherLog " &
                "values " &
                "('" & txtActionID.Text & "', '" & Replace(CaseNotes, "'", "''") & "', " &
                "'" & CurrentUser.UserID & "', '" & OracleDate & "') "
            Else
                SQL = "Update SBEAPOtherLog set " &
                "strCaseNotes = '" & Replace(CaseNotes, "'", "''") & "', " &
                "strModifingStaff = '" & CurrentUser.UserID & "', " &
                "datModifingDate = '" & OracleDate & "' " &
                "where numActionID = '" & txtActionID.Text & "' "
            End If
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearForm()
        Try

            ClearActions()
            txtActionID.Clear()
            txtCreationDate.Clear()
            txtActionType.Clear()
            TCCaseSpecificData.Visible = False

            dsActionLog = New DataSet
            dgvActionLog.DataSource = dsActionLog
            txtActionCount.Clear()
            DTPReferralDate.Text = OracleDate
            DTPReferralDate.Checked = False
            txtReferralInformation.Clear()
            cboInteragency.Text = ""
            cboStaffResponsible.SelectedValue = CurrentUser.UserID
            DTPCaseClosed.Text = OracleDate
            DTPCaseClosed.Checked = False
            DTPCaseOpened.Text = OracleDate
            txtCaseDescription.Clear()
            txtClientInformation.Clear()
            txtOutstandingCases.Clear()
            txtCaseID.Clear()
            txtClientID.Clear()
            chbComplaintBased.Checked = False
            chbCaseClosureLetter.Checked = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearActions()
        Try
            Dim i As Integer

            txtCaseNotes.Clear()
            txtCallName.Clear()
            mtbPhoneNumber.Clear()
            'txtReferralInformation.Clear()
            txtPhoneCallNotes.Clear()
            txtConferenceAttended.Clear()
            txtConferenceLocation.Clear()
            DTPConferenceStart.Text = OracleDate
            DTPConferenceEnd.Text = OracleDate
            txtListOfBusinessSectors.Clear()
            txtConferenceTopic.Clear()
            For i = 0 To clbStaffAttending.Items.Count - 1
                clbStaffAttending.SetItemCheckState(i, CheckState.Unchecked)
            Next
            '            clbStaffAttending
            rdbSBEAPPresentationYes.Checked = False
            rdbSBEAPPresentationNo.Checked = False
            txtConferenceAttendees.Clear()
            txtConferenceFollowUp.Clear()
            'DTPReferralDate.Text = OracleDate
            cboTechAssistType.Text = ""
            DTPTechAssistInitialContact.Text = OracleDate
            DTPTechAssistStart.Text = OracleDate
            DTPTechAssistEnd.Text = OracleDate
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
            'DTPReferralDate.Checked = False
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadMultiClientList()
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

                '                Client = Mid(txtMultiClientList.Text, 1, 8)
                txtMultiClientList.Text = Replace(txtMultiClientList.Text, Client, "")

                SQL = "Select " &
                      "strCompanyName, strCompanyAddress, " &
                      "strCompanyCity " &
                      "from SBEAPClients " &
                      "where ClientID = '" & Client & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
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
                End While
                dr.Close()
                If Client.Length > 7 Then
                    If txtMultiClients.Text.Contains(Client) Then
                    Else
                        txtMultiClients.Text = txtMultiClients.Text & Client & " - " & CompanyName & " - " & CompanyAddress & ", " & CompanyCity & vbCrLf
                    End If
                End If
            Loop

            txtMultiClientList.Text = ClientList


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        Try
            UpdateCaseLog("Non Action")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tsbClientSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbClientSearch.Click
        Try
            Dim clientSearchDialog As New SBEAPClientSearchTool
            clientSearchDialog.ShowDialog()
            If clientSearchDialog.DialogResult = DialogResult.OK Then
                Me.ValueFromClientLookUp = clientSearchDialog.SelectedClientID
                LoadClientInfo()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRefreshClient_Click(sender As System.Object, e As System.EventArgs) Handles btnRefreshClient.Click
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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub DTPCaseClosed_ValueChanged(sender As System.Object, e As System.EventArgs) Handles DTPCaseClosed.ValueChanged
        Try
            If temp <> "Data Load" Then
                If DTPCaseClosed.Checked = True Then
                    FormStatus("Disable")
                    DTPCaseClosed.Enabled = True
                    tsbSave.Enabled = True
                Else
                    FormStatus("Enable")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddNewAction_Click(sender As System.Object, e As System.EventArgs) Handles btnAddNewAction.Click
        Try
            If cboActionType.Text <> "" And cboActionType.SelectedIndex > 0 Then
                ClearActions()
                If txtCaseID.Text = "" Then
                    UpdateCaseLog("New Action")
                Else
                    SQL = "Update SBEAPCaseLog set " &
                    "datModifingDate = '" & OracleDate & "' " &
                    "where numCaseID = '" & txtCaseID.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                SQL = "select " &
                "case " &
                "when (select max(numActionID) from SBEAPActionLog) is Null then 1 " &
                "else (select max(numActionID) + 1 from SBEAPActionLog)   " &
                "end ActionNumber " &
                "from dual  "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("ActionNumber")) Then
                        txtActionID.Text = "1"
                    Else
                        txtActionID.Text = dr.Item("ActionNumber")
                    End If
                End While
                dr.Close()

                SQL = "Insert into SBEAPActionLog " &
                "values " &
                "('" & txtActionID.Text & "', '" & txtCaseID.Text & "', " &
                "'" & cboActionType.SelectedValue & "', '" & CurrentUser.UserID & "', " &
                "'" & OracleDate & "', '" & CurrentUser.UserID & "', " &
                "'" & OracleDate & "', '" & DTPActionOccured.Text & "') "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                LoadActionLog()
                txtActionType.Text = cboActionType.Text
                LoadActionTab()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtCaseID_TextChanged(sender As Object, e As System.EventArgs) Handles txtCaseID.TextChanged
        Try
            If IsNumeric(txtCaseID.Text) = True Then
                btnAddNewAction.Enabled = True
                btnViewActionType.Enabled = True
            Else
                btnAddNewAction.Enabled = False
                btnViewActionType.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvActionLog_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles dgvActionLog.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvActionLog.HitTest(e.X, e.Y)
            If dgvActionLog.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvActionLog.Columns(0).HeaderText = "Action ID" Then
                    txtActionID.Text = dgvActionLog(0, hti.RowIndex).Value
                    txtActionType.Text = dgvActionLog(2, hti.RowIndex).Value
                    txtCreationDate.Text = Format(dgvActionLog(3, hti.RowIndex).Value, "dd-MMM-yyyy")
                    If IsDBNull(dgvActionLog(4, hti.RowIndex).Value) Then
                        DTPActionOccured.Text = OracleDate
                    Else
                        DTPActionOccured.Text = Format(dgvActionLog(4, hti.RowIndex).Value, "dd-MMM-yyyy")
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnViewActionType_Click(sender As System.Object, e As System.EventArgs) Handles btnViewActionType.Click
        Try
            LoadActionTab()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearActions_Click(sender As System.Object, e As System.EventArgs) Handles btnClearActions.Click
        Try
            ClearActions()
            txtActionID.Clear()
            txtCreationDate.Clear()
            txtActionType.Clear()
            TCCaseSpecificData.Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tsbBack_Click(sender As System.Object, e As System.EventArgs) Handles tsbBack.Click
        Try
            CaseWork = Nothing
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteAction_Click(sender As System.Object, e As System.EventArgs) Handles btnDeleteAction.Click
        Try
            Dim Result As DialogResult
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
                                "where numActionID = '" & txtActionID.Text & "' "
                            Case "Phone Call Made/Received"
                                SQL = "delete SBEAPPhoneLog " &
                                "where numActionID = '" & txtActionID.Text & "' "
                            Case "Meeting/Conferences Attended"
                                SQL = "delete SBEAPConferenceLog " &
                                 "where numActionID = '" & txtActionID.Text & "' "
                            Case Else
                                SQL = "delete SBEAPOtherLog " &
                                "where numActionID = '" & txtActionID.Text & "' "
                        End Select

                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "delete SBEAPActionLog " &
                        "where numActionID = '" & txtActionID.Text & "' "

                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        ClearActions()
                        txtActionID.Clear()
                        txtActionType.Clear()
                        txtCreationDate.Clear()
                        LoadActionLog()
                        TCCaseSpecificData.Visible = False
                    Case Else

                End Select
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub mmiAddNewClient_Click(sender As System.Object, e As System.EventArgs) Handles mmiAddNewClient.Click
        Try
            If ClientSummary Is Nothing Then

            Else
                ClientSummary.Dispose()
            End If
            ClientSummary = New SBEAPClientSummary
            ClientSummary.Show()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tsbClearFrom_Click(sender As System.Object, e As System.EventArgs) Handles tsbClearFrom.Click
        Try

            ClearForm()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub rdbSingleClient_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdbSingleClient.CheckedChanged
        Try
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
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub rdbMultiClient_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdbMultiClient.CheckedChanged
        Try
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
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddClients_Click(sender As System.Object, e As System.EventArgs) Handles btnAddClients.Click
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRemoveClient_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveClient.Click
        Try
            If txtDeleteClient.Text.Length = 8 Then
                txtMultiClientList.Text = Replace(txtMultiClientList.Text, (txtDeleteClient.Text & vbCrLf), "")
            End If
            txtMultiClients.Clear()
            LoadMultiClientList()
            txtDeleteClient.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub DeleteCase()
        Try
            Dim Result As DialogResult
            Dim ActionID As String = ""
            Dim ActionType As String = ""

            If txtCaseID.Text <> "" Then
                Result = MessageBox.Show("Are you certain that you want to delete this Case?",
                  "Case Delete", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                Select Case Result
                    Case DialogResult.Yes
                        Do While ActionID <> "Done"
                            ActionID = "Done"

                            SQL = "Select " &
                            "numActionID, numActionType " &
                            "from SBEAPActionLog " &
                            "where numCaseID = '" & txtCaseID.Text & "' "
                            cmd = New SqlCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
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
                            End While
                            dr.Close()

                            If ActionID <> "" And ActionID <> "Done" Then
                                Select Case ActionType
                                    Case "4"
                                        SQL = "Delete SBEAPConferenceLog " &
                                        "where numActionID = '" & ActionID & "'  "
                                        cmd = New SqlCommand(SQL, CurrentConnection)
                                        If CurrentConnection.State = ConnectionState.Closed Then
                                            CurrentConnection.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        dr.Close()

                                        SQL = "Delete SBEAPActionLog " &
                                        "where numActionID = '" & ActionID & "'  "
                                        cmd = New SqlCommand(SQL, CurrentConnection)
                                        If CurrentConnection.State = ConnectionState.Closed Then
                                            CurrentConnection.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        dr.Close()
                                    Case "6"
                                        SQL = "Delete SBEAPPhoneLog " &
                                        "where numActionID = '" & ActionID & "'  "
                                        cmd = New SqlCommand(SQL, CurrentConnection)
                                        If CurrentConnection.State = ConnectionState.Closed Then
                                            CurrentConnection.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        dr.Close()

                                        SQL = "Delete SBEAPActionLog " &
                                        "where numActionID = '" & ActionID & "'  "
                                        cmd = New SqlCommand(SQL, CurrentConnection)
                                        If CurrentConnection.State = ConnectionState.Closed Then
                                            CurrentConnection.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        dr.Close()
                                    Case "10"
                                        SQL = "Delete SBEAPTechnicalAssist " &
                                        "where numActionID = '" & ActionID & "'  "
                                        cmd = New SqlCommand(SQL, CurrentConnection)
                                        If CurrentConnection.State = ConnectionState.Closed Then
                                            CurrentConnection.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        dr.Close()

                                        SQL = "Delete SBEAPActionLog " &
                                        "where numActionID = '" & ActionID & "'  "
                                        cmd = New SqlCommand(SQL, CurrentConnection)
                                        If CurrentConnection.State = ConnectionState.Closed Then
                                            CurrentConnection.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        dr.Close()
                                    Case "1" Or "2" Or "3" Or "5" Or "7" Or "8" Or "9" Or "11" Or "12"
                                        SQL = "Delete SBEAPOtherLog " &
                                        "where numActionID = '" & ActionID & "'  "
                                        cmd = New SqlCommand(SQL, CurrentConnection)
                                        If CurrentConnection.State = ConnectionState.Closed Then
                                            CurrentConnection.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        dr.Close()

                                        SQL = "Delete SBEAPActionLog " &
                                        "where numActionID = '" & ActionID & "'  "
                                        cmd = New SqlCommand(SQL, CurrentConnection)
                                        If CurrentConnection.State = ConnectionState.Closed Then
                                            CurrentConnection.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        dr.Close()
                                    Case Else
                                        SQL = "Delete SBEAPConferenceLog " &
                                        "where numActionID = '" & ActionID & "'  "
                                        cmd = New SqlCommand(SQL, CurrentConnection)
                                        If CurrentConnection.State = ConnectionState.Closed Then
                                            CurrentConnection.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        dr.Close()

                                        SQL = "Delete SBEAPPhoneLog " &
                                        "where numActionID = '" & ActionID & "'  "
                                        cmd = New SqlCommand(SQL, CurrentConnection)
                                        If CurrentConnection.State = ConnectionState.Closed Then
                                            CurrentConnection.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        dr.Close()

                                        SQL = "Delete SBEAPTechnicalAssist " &
                                        "where numActionID = '" & ActionID & "'  "
                                        cmd = New SqlCommand(SQL, CurrentConnection)
                                        If CurrentConnection.State = ConnectionState.Closed Then
                                            CurrentConnection.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        dr.Close()

                                        SQL = "Delete SBEAPOtherLog " &
                                        "where numActionID = '" & ActionID & "'  "
                                        cmd = New SqlCommand(SQL, CurrentConnection)
                                        If CurrentConnection.State = ConnectionState.Closed Then
                                            CurrentConnection.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        dr.Close()

                                        SQL = "Delete SBEAPActionLog " &
                                        "where numActionID = '" & ActionID & "'  "
                                        cmd = New SqlCommand(SQL, CurrentConnection)
                                        If CurrentConnection.State = ConnectionState.Closed Then
                                            CurrentConnection.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        dr.Close()
                                End Select
                            End If
                        Loop

                        SQL = "Delete SBEAPCaseLog " &
                        "where numCaseID = '" & txtCaseID.Text & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Delete SBEAPCaseLogLink " &
                        "where numCaseID = '" & txtCaseID.Text & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        ClearForm()
                    Case Else
                        Exit Sub
                End Select
                Exit Sub
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tsmDeleteCaseWork_Click(sender As System.Object, e As System.EventArgs) Handles tsmDeleteCaseWork.Click
        Try

            If txtCaseID.Text <> "" Then
                DeleteCase()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    'Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
    '    Try

    '        If PrintForm Is Nothing Then
    '        Else
    '            PrintForm.Dispose()
    '        End If
    '        PrintForm = New SBEAPPrintForms
    '        PrintForm.txtSource.Text = txtCaseID.Text
    '        PrintForm.txtOrigin.Text = "Case Work"
    '        PrintForm.Show()

    '    Catch ex As Exception
    '        ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    End Try
    'End Sub
End Class