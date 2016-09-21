Imports System.Data.SqlClient
Imports System.Collections.Generic


Public Class SSCPEnforcementSelector
    Dim SQL As String
    Dim dsSSCPEnforcement As DataSet
    Dim daSSCPEnforcement As SqlDataAdapter
    Dim dsComplianceUnits As DataSet
    Dim daComplianceUnits As SqlDataAdapter

    Private Sub SSCPEnforcementSelector_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            ParseParameters()
            LoadComplianceUnits()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub SSCPEnforcementSelector_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        txtAIRSNumber.Focus()
    End Sub
    Private Sub ParseParameters()
        If Parameters IsNot Nothing Then
            If Parameters.ContainsKey(FormParameter.AirsNumber) Then txtAIRSNumber.Text = Parameters(FormParameter.AirsNumber)
            If Parameters.ContainsKey(FormParameter.TrackingNumber) Then txtTrackingNumber.Text = Parameters(FormParameter.TrackingNumber)
        End If
    End Sub
    Private Sub LoadComplianceUnits()
        Dim dtSSCPUnits As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try


            SQL = "Select numUnitCode, strUnitDesc " &
            "from LookUpEPDUnits  " &
            "where numProgramCode = '4'  " &
            "order by strUnitDesc  "

            dsComplianceUnits = New DataSet
            daComplianceUnits = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daComplianceUnits.Fill(dsComplianceUnits, "ComplianceUnits")

            dtSSCPUnits.Columns.Add("strUnitDesc", GetType(System.String))
            dtSSCPUnits.Columns.Add("numUnitCode", GetType(System.String))

            drNewRow = dtSSCPUnits.NewRow()
            drNewRow("strUnitDesc") = " "
            drNewRow("numUnitCode") = " "
            dtSSCPUnits.Rows.Add(drNewRow)

            For Each drDSRow In dsComplianceUnits.Tables("ComplianceUnits").Rows()
                drNewRow = dtSSCPUnits.NewRow()
                drNewRow("strUnitDesc") = drDSRow("strUnitDesc")
                drNewRow("numUnitCode") = drDSRow("numUnitCode")
                dtSSCPUnits.Rows.Add(drNewRow)
            Next

            drNewRow = dtSSCPUnits.NewRow()
            drNewRow("strUnitDesc") = "Unassigned"
            drNewRow("numUnitCode") = ""
            dtSSCPUnits.Rows.Add(drNewRow)

            With cboComplianceUnits
                .DataSource = dtSSCPUnits
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
#Region "Main Menu"
    Private Sub mmiSearch_Click(sender As Object, e As EventArgs) Handles mmiSearch.Click
        Try

            OpenFacilityLookupTool()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiOpenEnforcement_Click(sender As Object, e As EventArgs) Handles mmiOpenEnforcement.Click
        Try

            OpenEnforcement()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiHelp_Click(sender As Object, e As EventArgs) Handles mmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub
#End Region
    Public WriteOnly Property ValueFromFacilityLookUp() As String
        Set(Value As String)
            txtAIRSNumber.Text = Value
        End Set
    End Property
    Private Sub TBSSCPFCESelector_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles TBSSCPFCESelector.ButtonClick
        Try

            Select Case TBSSCPFCESelector.Buttons.IndexOf(e.Button)
                Case 0
                    OpenFacilityLookupTool()
                Case 1
                    OpenEnforcement()
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtAIRSNumber_TextChanged(sender As Object, e As EventArgs) Handles txtAIRSNumber.TextChanged
        Try
            If txtAIRSNumber.Text.Length = 8 Then
                Dim airsNum As String = txtAIRSNumber.Text
                Dim facName As String = DAL.FacilityData.GetFacilityName(airsNum)
                If facName IsNot Nothing Then
                    txtFacilityName.Text = facName
                    LoadSSCPEnforcementDataGrid("Single")
                Else
                    txtFacilityName.Text = "Invalid AIRS Number"
                End If
            Else
                txtFacilityName.Text = ""
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnOpenEnforcement_Click(sender As Object, e As EventArgs) Handles btnOpenEnforcement.Click
        Try
            If txtFacilityName.Text <> "Invalid AIRS Number" And txtFacilityName.Text <> "" Then
                OpenEnforcement()
            Else
                MsgBox("Please Enter a valid AIRS Number First.", MsgBoxStyle.Information, "SSCP Enforcement Tool")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnOpenEnforcements_Click(sender As Object, e As EventArgs) Handles btnOpenEnforcements.Click
        Try

            LoadSSCPEnforcementDataGrid("AllOpen")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub OpenFacilityLookupTool()
        Try
            Dim facilityLookupDialog As New IAIPFacilityLookUpTool
            facilityLookupDialog.ShowDialog()
            If facilityLookupDialog.DialogResult = DialogResult.OK _
            AndAlso facilityLookupDialog.SelectedAirsNumber <> "" Then
                Me.ValueFromFacilityLookUp = facilityLookupDialog.SelectedAirsNumber
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub OpenEnforcement()
        Try

            If DAL.AirsNumberExists(txtAIRSNumber.Text) Then

                If txtEnforcementNumber.Text <> "" Then
                    OpenFormEnforcement(txtEnforcementNumber.Text)
                ElseIf txtTrackingNumber.Text = "" Then
                    OpenFormEnforcement(New Apb.ApbFacilityId(txtAIRSNumber.Text))
                Else
                    OpenFormEnforcement(New Apb.ApbFacilityId(txtAIRSNumber.Text), txtTrackingNumber.Text)
                End If

                Me.Close()

            Else
                MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Enforcement Selection Tool Warning")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadSSCPEnforcementDataGrid(Source As String)
        Try

            Select Case Source
                Case "All"
                    SQL = "Select distinct(to_number(SSCP_AuditedEnforcement.strEnforcementNumber)) as strEnforcementNumber, " &
                       "Case  " &
                       "	when datDiscoveryDate is Null then '' " &
                       "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " &
                       "END as Violationdate,  " &
                        "case " &
                       "    when datEnforcementFinalized is Not Null then '4 - Closed Out' " &
                       "    when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA' " &
                       "    when strStatus = 'UC' then '2 - Submitted to UC' " &
                       "    When strStatus Is Null then '1 - At Staff' " &
                       "   else 'Unknown' " &
                       "end as EnforcementStatus, " &
                       "case  " &
                       " 	when strHPV IS NULL then strActionType " &
                       " 	When strHPV IS Not Null then 'HPV'  " &
                       "   Else 'HPV' " &
                       "END as HPVStatus, " &
                       "Case " &
                       "	when datEnforcementFinalized Is Not NULL then 'Closed' " &
                       "	when datEnforcementFinalized is NUll then 'Open' " &
                       "Else 'Open' " &
                       "End as Status, " &
                       "SUBSTRING(SSCP_AuditedEnforcement.strAIRSNumber, 5,8) as AIRSNumber, " &
                        "(strLastName|| ', ' ||strFirstName) as Staff,  " &
                       "strFacilityName  " &
                       "from SSCP_AuditedEnforcement,  " &
                       "APBFacilityInformation, EPDUserProfiles " &
                       "Where APBFacilityInformation.strAIRSNumber = SSCP_AuditedEnforcement.strAIRSNumber " &
                       "and EPDUserProfiles.numUserID = SSCP_AuditedEnforcement.numStaffResponsible  " &
                       "order by strENforcementNumber DESC "
                Case "AllOpen"
                    'txtAIRSNumber.Clear()
                    'txtEnforcementNumber.Clear()
                    SQL = "Select distinct(to_number(SSCP_AuditedEnforcement.strEnforcementNumber)) as strEnforcementNumber, " &
                    "Case  " &
                    "	when datDiscoveryDate is Null then '' " &
                    "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " &
                    "END as Violationdate,  " &
                     "case " &
                    "    when datEnforcementFinalized is Not Null then '4 - Closed Out' " &
                    "    when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA' " &
                    "    when strStatus = 'UC' then '2 - Submitted to UC' " &
                    "    When strStatus Is Null then '1 - At Staff' " &
                    "   else 'Unknown' " &
                    "end as EnforcementStatus, " &
                    "case  " &
                    " 	when strHPV IS NULL then strActionType " &
                    " 	When strHPV IS Not Null then 'HPV'  " &
                    "   Else 'HPV' " &
                    "END as HPVStatus, " &
                    "Case " &
                    "	when datEnforcementFinalized Is Not NULL then 'Closed' " &
                    "	when datEnforcementFinalized is NUll then 'Open' " &
                    "Else 'Open' " &
                    "End as Status, " &
                    "SUBSTRING(SSCP_AuditedEnforcement.strAIRSNumber, 5,8) as AIRSNumber, " &
                     "(strLastName|| ', ' ||strFirstName) as Staff,  " &
                    "strFacilityName  " &
                    "from SSCP_AuditedEnforcement,   " &
                    "APBFacilityInformation, EPDUserProfiles " &
                    "Where  APBFacilityInformation.strAIRSNumber = SSCP_AuditedEnforcement.strAIRSNumber " &
                    "and EPDUserProfiles.numUserID = SSCP_AuditedEnforcement.numStaffResponsible  " &
                    "and datEnforcementFinalized IS Null " &
                    "order by strENforcementNumber DESC "
                Case "Single"
                    SQL = "Select distinct(to_number(SSCP_AuditedEnforcement.strEnforcementNumber)) as strEnforcementNumber, " &
                    "Case  " &
                    "	when datDiscoveryDate is Null then '' " &
                    "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " &
                    "END as Violationdate,  " &
                     "case " &
                    "    when datEnforcementFinalized is Not Null then '4 - Closed Out' " &
                    "    when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA' " &
                    "    when strStatus = 'UC' then '2 - Submitted to UC' " &
                    "    When strStatus Is Null then '1 - At Staff' " &
                    "   else 'Unknown' " &
                    "end as EnforcementStatus, " &
                    "case  " &
                    " 	when strHPV IS NULL then strActionType " &
                    " 	When strHPV IS Not Null then 'HPV'  " &
                    "   Else 'HPV' " &
                    "END as HPVStatus, " &
                    "Case " &
                    "	when datEnforcementFinalized Is Not NULL then 'Closed' " &
                    "	when datEnforcementFinalized is NUll then 'Open' " &
                    "Else 'Open' " &
                    "End as Status, " &
                    "SUBSTRING(SSCP_AuditedEnforcement.strAIRSNumber, 5,8) as AIRSNumber, " &
                     "(strLastName|| ', ' ||strFirstName) as Staff,  " &
                    "strFacilityName " &
                    "from SSCP_AuditedEnforcement, " &
                    "APBFacilityInformation, EPDUserProfiles " &
                    "Where  APBFacilityInformation.strAIRSNumber = SSCP_AuditedEnforcement.strAIRSNumber " &
                    "and EPDUserProfiles.numUserID = SSCP_AuditedEnforcement.numStaffResponsible  " &
                    "and SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtAIRSNumber.Text & "' " &
                    "order by strENforcementNumber DESC "
                Case "ByUnit"
                    If cboComplianceUnits.Text = "Unassigned" Then
                        SQL = "Select " &
                        "to_number(SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber, " &
                        "case  " &
                        "   when datEnforcementFinalized is Not Null then '4 - Closed Out' " &
                        "   when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " &
                        "   when strStatus = 'UC' then '2 - Submitted to UC'  " &
                        "   When strStatus Is Null then '1 - At Staff'  " &
                        "   else 'Unknown'  " &
                        "end as EnforcementStatus, " &
                        "Case   " &
                        " 	when datDiscoveryDate is Null then ''  " &
                        " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " &
                        "END as Violationdate,   " &
                        "case   " &
                        "  	when strHPV IS NULL then strActionType  " &
                        "  	When strHPV IS Not Null then 'HPV'   " &
                        "    Else 'HPV'  " &
                        " END as HPVStatus,  " &
                        " Case  " &
                        "  	when datEnforcementFinalized Is Not NULL then 'Closed'  " &
                        " 	when datEnforcementFinalized is NUll then 'Open'  " &
                        "Else 'Open'  " &
                        "End as Status,  " &
                         "SUBSTRING(SSCP_AuditedEnforcement.strAIRSNumber, 5,8) as AIRSNumber,  " &
                        "strFacilityName,  " &
                        "(strLastName|| ', ' ||strFirstName) as Staff  " &
                        "from SSCP_AuditedEnforcement,   " &
                        "APBFacilityInformation, EPDUserProfiles  " &
                        "Where  APBFacilityInformation.strAIRSNumber = SSCP_AuditedEnforcement.strAIRSNumber  " &
                        "and (strStatus IS Null or strStatus = 'UC')  " &
                        "and datEnforcementFinalized is NULL  " &
                        "and EPDUserProfiles.numUserID = SSCP_AuditedEnforcement.numStaffResponsible  " &
                        "and EPDUserProfiles.numUserID = '0'  " &
                        "order by strENforcementNumber DESC "
                    Else
                        SQL = "Select to_number(SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber, " &
                        "case  " &
                        "   when datEnforcementFinalized is Not Null then '4 - Closed Out' " &
                        "   when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " &
                        "   when strStatus = 'UC' then '2 - Submitted to UC'  " &
                        "   When strStatus Is Null then '1 - At Staff'  " &
                        "   else 'Unknown'  " &
                        "end as EnforcementStatus, " &
                        "Case   " &
                        " 	when datDiscoveryDate is Null then ''  " &
                        " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " &
                        "END as Violationdate,   " &
                        "case   " &
                        "  	when strHPV IS NULL then strActionType  " &
                        "  	When strHPV IS Not Null then 'HPV'   " &
                        "    Else 'HPV'  " &
                        " END as HPVStatus,  " &
                        " Case  " &
                        "  	when datEnforcementFinalized Is Not NULL then 'Closed'  " &
                        " 	when datEnforcementFinalized is NUll then 'Open'  " &
                        "Else 'Open'  " &
                        "End as Status,  " &
                         "SUBSTRING(SSCP_AuditedEnforcement.strAIRSNumber, 5,8) as AIRSNumber,  " &
                        "strFacilityName,  " &
                        "(strLastName|| ', ' ||strFirstName) as Staff  " &
                        "from SSCP_AuditedEnforcement, " &
                        "APBFacilityInformation, EPDUserProfiles  " &
                        "Where  APBFacilityInformation.strAIRSNumber = SSCP_AuditedEnforcement.strAIRSNumber  " &
                        "and (strStatus IS Null or strStatus = 'UC')  " &
                        "and datEnforcementFinalized is NULL  " &
                        "and EPDUserProfiles.numUserID = SSCP_AuditedEnforcement.numStaffResponsible  " &
                        "and (EPDUserProfiles.numUnit = '" & cboComplianceUnits.SelectedValue & "'  " &
                        "or EPDUserProfiles.numUserID = '0')  " &
                        "order by strENforcementNumber DESC "
                    End If
            End Select

            dsSSCPEnforcement = New DataSet

            Dim cmd As New SqlCommand(SQL, CurrentConnection)

            daSSCPEnforcement = New SqlDataAdapter(cmd)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daSSCPEnforcement.Fill(dsSSCPEnforcement, "SSCPEnforcementTable")
            dgvSSCPEnforcement.DataSource = dsSSCPEnforcement
            dgvSSCPEnforcement.DataMember = "SSCPEnforcementTable"



            dgvSSCPEnforcement.RowHeadersVisible = False
            dgvSSCPEnforcement.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvSSCPEnforcement.AllowUserToResizeColumns = True
            dgvSSCPEnforcement.AllowUserToAddRows = False
            dgvSSCPEnforcement.AllowUserToDeleteRows = False
            dgvSSCPEnforcement.AllowUserToOrderColumns = True
            dgvSSCPEnforcement.AllowUserToResizeRows = True
            dgvSSCPEnforcement.Columns("AIRSNumber").HeaderText = "AIRS Number"
            dgvSSCPEnforcement.Columns("AIRSNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgvSSCPEnforcement.Columns("AIRSNumber").DisplayIndex = 0
            dgvSSCPEnforcement.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvSSCPEnforcement.Columns("strFacilityName").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            dgvSSCPEnforcement.Columns("strFacilityName").DisplayIndex = 1
            dgvSSCPEnforcement.Columns("strEnforcementNumber").HeaderText = "Enforcement Number"
            dgvSSCPEnforcement.Columns("strEnforcementNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgvSSCPEnforcement.Columns("strEnforcementNumber").DisplayIndex = 2
            dgvSSCPEnforcement.Columns("Violationdate").HeaderText = "Violation Discovery Date"
            dgvSSCPEnforcement.Columns("Violationdate").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgvSSCPEnforcement.Columns("Violationdate").DisplayIndex = 3
            dgvSSCPEnforcement.Columns("Violationdate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvSSCPEnforcement.Columns("EnforcementStatus").HeaderText = "Enforcement Status"
            dgvSSCPEnforcement.Columns("EnforcementStatus").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgvSSCPEnforcement.Columns("EnforcementStatus").DisplayIndex = 4
            dgvSSCPEnforcement.Columns("HPVstatus").HeaderText = "HPV Status"
            dgvSSCPEnforcement.Columns("HPVstatus").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgvSSCPEnforcement.Columns("HPVstatus").DisplayIndex = 5
            dgvSSCPEnforcement.Columns("Status").HeaderText = "Open/Closed"
            dgvSSCPEnforcement.Columns("Status").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgvSSCPEnforcement.Columns("Status").DisplayIndex = 6

            txtEnforcementCount.Text = dgvSSCPEnforcement.RowCount

        Catch ex As Exception
            ErrorReport(ex, SQL, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Try

            LoadSSCPEnforcementDataGrid("ByUnit")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgvSSCPEnforcement_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvSSCPEnforcement.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvSSCPEnforcement.HitTest(e.X, e.Y)

        Try


            If dgvSSCPEnforcement.RowCount > 0 And hti.RowIndex <> -1 Then
                txtEnforcementNumber.Text = dgvSSCPEnforcement(0, hti.RowIndex).Value
                txtAIRSNumber.Text = dgvSSCPEnforcement(5, hti.RowIndex).Value
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnViewAllEnforcements_Click(sender As Object, e As EventArgs) Handles btnViewAllEnforcements.Click
        Try

            LoadSSCPEnforcementDataGrid("All")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        dgvSSCPEnforcement.ExportToExcel(Me)
    End Sub


End Class