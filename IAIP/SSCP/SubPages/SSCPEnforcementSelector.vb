Imports Oracle.ManagedDataAccess.Client
Imports System.Collections.Generic


Public Class SSCPEnforcementSelector
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim dsSSCPEnforcement As DataSet
    Dim daSSCPEnforcement As OracleDataAdapter
    Dim dsComplianceUnits As DataSet
    Dim daComplianceUnits As OracleDataAdapter

    Private Sub SSCPEnforcementSelector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            ParseParameters()
            LoadComplianceUnits()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub SSCPEnforcementSelector_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtAIRSNumber.Focus()
    End Sub
    Private Sub ParseParameters()
        If Parameters IsNot Nothing Then
            If Parameters.ContainsKey("airsnumber") Then txtAIRSNumber.Text = Parameters("airsnumber")
            If Parameters.ContainsKey("trackingnumber") Then txtTrackingNumber.Text = Parameters("trackingnumber")
        End If
    End Sub
    Private Sub LoadComplianceUnits()
        Dim dtSSCPUnits As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try


            SQL = "Select numUnitCode, strUnitDesc " & _
            "from AIRBranch.LookUpEPDUnits  " & _
            "where numProgramCode = '4'  " & _
            "order by strUnitDesc  "

            dsComplianceUnits = New DataSet
            daComplianceUnits = New OracleDataAdapter(SQL, CurrentConnection)

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
#Region "Main Menu"
    Private Sub mmiSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSearch.Click
        Try

            OpenFacilityLookupTool()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiOpenEnforcement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOpenEnforcement.Click
        Try

            OpenEnforcement()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub
#End Region
    Public WriteOnly Property ValueFromFacilityLookUp() As String
        Set(ByVal Value As String)
            txtAIRSNumber.Text = Value
        End Set
    End Property
    Private Sub TBSSCPFCESelector_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBSSCPFCESelector.ButtonClick
        Try

            Select Case TBSSCPFCESelector.Buttons.IndexOf(e.Button)
                Case 0
                    OpenFacilityLookupTool()
                Case 1
                    OpenEnforcement()
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtAIRSNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAIRSNumber.TextChanged
        Try
            If txtAIRSNumber.Text.Length = 8 Then
                Dim airsNum As String = txtAIRSNumber.Text
                Dim facName As String = DAL.FacilityModule.GetFacilityName(airsNum)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnOpenEnforcement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenEnforcement.Click
        Try
            If txtFacilityName.Text <> "Invalid AIRS Number" And txtFacilityName.Text <> "" Then
                OpenEnforcement()
            Else
                MsgBox("Please Enter a valid AIRS Number First.", MsgBoxStyle.Information, "SSCP Enforcement Tool")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnOpenEnforcements_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenEnforcements.Click
        Try

            LoadSSCPEnforcementDataGrid("AllOpen")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub OpenFacilityLookupTool()
        Try
            Dim facilityLookupDialog As New IAIPFacilityLookUpTool
            facilityLookupDialog.ShowDialog()
            If facilityLookupDialog.DialogResult = Windows.Forms.DialogResult.OK _
            AndAlso facilityLookupDialog.SelectedAirsNumber <> "" Then
                Me.ValueFromFacilityLookUp = facilityLookupDialog.SelectedAirsNumber
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub OpenEnforcement()
        Try

            If txtFacilityName.Text <> "" And txtFacilityName.Text <> "Invalid AIRS Number" Then

                If txtEnforcementNumber.Text <> "" Then
                    OpenFormEnforcement(txtEnforcementNumber.Text)
                Else

                    Dim parameters As New Dictionary(Of String, String)
                    parameters("airsnumber") = txtAIRSNumber.Text
                    If txtTrackingNumber.Text <> "" Then parameters("trackingnumber") = txtTrackingNumber.Text
                    OpenMultiForm("SscpEnforcement", -1, parameters)

                End If

                Me.Dispose()

            Else
                MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Enforcement Selection Tool Warning")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadSSCPEnforcementDataGrid(ByVal Source As String)
        Try

            Select Case Source
                Case "All"
                    SQL = "Select distinct(to_number(SSCP_AuditedEnforcement.strEnforcementNumber)) as strEnforcementNumber, " & _
                       "Case  " & _
                       "	when datDiscoveryDate is Null then '' " & _
                       "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                       "END as Violationdate,  " & _
                        "case " & _
                       "    when datEnforcementFinalized is Not Null then '4 - Closed Out' " & _
                       "    when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA' " & _
                       "    when strStatus = 'UC' then '2 - Submitted to UC' " & _
                       "    When strStatus Is Null then '1 - At Staff' " & _
                       "   else 'Unknown' " & _
                       "end as EnforcementStatus, " & _
                       "case  " & _
                       " 	when strHPV IS NULL then strActionType " & _
                       " 	When strHPV IS Not Null then 'HPV'  " & _
                       "   Else 'HPV' " & _
                       "END as HPVStatus, " & _
                       "Case " & _
                       "	when datEnforcementFinalized Is Not NULL then 'Closed' " & _
                       "	when datEnforcementFinalized is NUll then 'Open' " & _
                       "Else 'Open' " & _
                       "End as Status, " & _
                       "substr(AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber, " & _
                        "(strLastName|| ', ' ||strFirstName) as Staff,  " & _
                       "strFacilityName  " & _
                       "from AIRBRANCH.SSCP_AuditedEnforcement,  " & _
                       "AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles " & _
                       "Where AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber " & _
                       "and AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.SSCP_AuditedEnforcement.numStaffResponsible  " & _
                       "order by strENforcementNumber DESC "
                Case "AllOpen"
                    'txtAIRSNumber.Clear()
                    'txtEnforcementNumber.Clear()
                    SQL = "Select distinct(to_number(SSCP_AuditedEnforcement.strEnforcementNumber)) as strEnforcementNumber, " & _
                    "Case  " & _
                    "	when datDiscoveryDate is Null then '' " & _
                    "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                    "END as Violationdate,  " & _
                     "case " & _
                    "    when datEnforcementFinalized is Not Null then '4 - Closed Out' " & _
                    "    when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA' " & _
                    "    when strStatus = 'UC' then '2 - Submitted to UC' " & _
                    "    When strStatus Is Null then '1 - At Staff' " & _
                    "   else 'Unknown' " & _
                    "end as EnforcementStatus, " & _
                    "case  " & _
                    " 	when strHPV IS NULL then strActionType " & _
                    " 	When strHPV IS Not Null then 'HPV'  " & _
                    "   Else 'HPV' " & _
                    "END as HPVStatus, " & _
                    "Case " & _
                    "	when datEnforcementFinalized Is Not NULL then 'Closed' " & _
                    "	when datEnforcementFinalized is NUll then 'Open' " & _
                    "Else 'Open' " & _
                    "End as Status, " & _
                    "substr(AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber, " & _
                     "(strLastName|| ', ' ||strFirstName) as Staff,  " & _
                    "strFacilityName  " & _
                    "from AIRBRANCH.SSCP_AuditedEnforcement,   " & _
                    "AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles " & _
                    "Where  AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber " & _
                    "and AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.SSCP_AuditedEnforcement.numStaffResponsible  " & _
                    "and datEnforcementFinalized IS Null " & _
                    "order by strENforcementNumber DESC "
                Case "Single"
                    SQL = "Select distinct(to_number(SSCP_AuditedEnforcement.strEnforcementNumber)) as strEnforcementNumber, " & _
                    "Case  " & _
                    "	when datDiscoveryDate is Null then '' " & _
                    "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                    "END as Violationdate,  " & _
                     "case " & _
                    "    when datEnforcementFinalized is Not Null then '4 - Closed Out' " & _
                    "    when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA' " & _
                    "    when strStatus = 'UC' then '2 - Submitted to UC' " & _
                    "    When strStatus Is Null then '1 - At Staff' " & _
                    "   else 'Unknown' " & _
                    "end as EnforcementStatus, " & _
                    "case  " & _
                    " 	when strHPV IS NULL then strActionType " & _
                    " 	When strHPV IS Not Null then 'HPV'  " & _
                    "   Else 'HPV' " & _
                    "END as HPVStatus, " & _
                    "Case " & _
                    "	when datEnforcementFinalized Is Not NULL then 'Closed' " & _
                    "	when datEnforcementFinalized is NUll then 'Open' " & _
                    "Else 'Open' " & _
                    "End as Status, " & _
                    "substr(AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber, " & _
                     "(strLastName|| ', ' ||strFirstName) as Staff,  " & _
                    "strFacilityName " & _
                    "from AIRBRANCH.SSCP_AuditedEnforcement, " & _
                    "AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles " & _
                    "Where  AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber " & _
                    "and AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.SSCP_AuditedEnforcement.numStaffResponsible  " & _
                    "and AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber = '0413" & txtAIRSNumber.Text & "' " & _
                    "order by strENforcementNumber DESC "
                Case "ByUnit"
                    If cboComplianceUnits.Text = "Unassigned" Then
                        SQL = "Select " & _
                        "to_number(AIRBRANCH.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber, " & _
                        "case  " & _
                        "   when datEnforcementFinalized is Not Null then '4 - Closed Out' " & _
                        "   when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " & _
                        "   when strStatus = 'UC' then '2 - Submitted to UC'  " & _
                        "   When strStatus Is Null then '1 - At Staff'  " & _
                        "   else 'Unknown'  " & _
                        "end as EnforcementStatus, " & _
                        "Case   " & _
                        " 	when datDiscoveryDate is Null then ''  " & _
                        " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                        "END as Violationdate,   " & _
                        "case   " & _
                        "  	when strHPV IS NULL then strActionType  " & _
                        "  	When strHPV IS Not Null then 'HPV'   " & _
                        "    Else 'HPV'  " & _
                        " END as HPVStatus,  " & _
                        " Case  " & _
                        "  	when datEnforcementFinalized Is Not NULL then 'Closed'  " & _
                        " 	when datEnforcementFinalized is NUll then 'Open'  " & _
                        "Else 'Open'  " & _
                        "End as Status,  " & _
                         "substr(AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " & _
                        "strFacilityName,  " & _
                        "(strLastName|| ', ' ||strFirstName) as Staff  " & _
                        "from AIRBRANCH.SSCP_AuditedEnforcement,   " & _
                        "AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles  " & _
                        "Where  AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber  " & _
                        "and (strStatus IS Null or strStatus = 'UC')  " & _
                        "and datEnforcementFinalized is NULL  " & _
                        "and AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.SSCP_AuditedEnforcement.numStaffResponsible  " & _
                        "and AIRBRANCH.EPDUserProfiles.numUserID = '0'  " & _
                        "order by strENforcementNumber DESC "
                    Else
                        SQL = "Select to_number(AIRBRANCH.SSCP_AuditedEnforcement.strEnforcementNumber) as strEnforcementNumber, " & _
                        "case  " & _
                        "   when datEnforcementFinalized is Not Null then '4 - Closed Out' " & _
                        "   when strAFSKeyActionNumber is Not Null then '3 - Submitted to EPA'  " & _
                        "   when strStatus = 'UC' then '2 - Submitted to UC'  " & _
                        "   When strStatus Is Null then '1 - At Staff'  " & _
                        "   else 'Unknown'  " & _
                        "end as EnforcementStatus, " & _
                        "Case   " & _
                        " 	when datDiscoveryDate is Null then ''  " & _
                        " 	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
                        "END as Violationdate,   " & _
                        "case   " & _
                        "  	when strHPV IS NULL then strActionType  " & _
                        "  	When strHPV IS Not Null then 'HPV'   " & _
                        "    Else 'HPV'  " & _
                        " END as HPVStatus,  " & _
                        " Case  " & _
                        "  	when datEnforcementFinalized Is Not NULL then 'Closed'  " & _
                        " 	when datEnforcementFinalized is NUll then 'Open'  " & _
                        "Else 'Open'  " & _
                        "End as Status,  " & _
                         "substr(AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber, 5) as AIRSNumber,  " & _
                        "strFacilityName,  " & _
                        "(strLastName|| ', ' ||strFirstName) as Staff  " & _
                        "from AIRBRANCH.SSCP_AuditedEnforcement, " & _
                        "AIRBRANCH.APBFacilityInformation, AIRBRANCH.EPDUserProfiles  " & _
                        "Where  AIRBRANCH.APBFacilityInformation.strAIRSNumber = AIRBRANCH.SSCP_AuditedEnforcement.strAIRSNumber  " & _
                        "and (strStatus IS Null or strStatus = 'UC')  " & _
                        "and datEnforcementFinalized is NULL  " & _
                        "and AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.SSCP_AuditedEnforcement.numStaffResponsible  " & _
                        "and (AIRBRANCH.EPDUserProfiles.numUnit = '" & cboComplianceUnits.SelectedValue & "'  " & _
                        "or AIRBRANCH.EPDUserProfiles.numUserID = '0')  " & _
                        "order by strENforcementNumber DESC "
                    End If
            End Select

            dsSSCPEnforcement = New DataSet

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

            daSSCPEnforcement = New OracleDataAdapter(cmd)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daSSCPEnforcement.Fill(dsSSCPEnforcement, "SSCPEnforcement")
            dgvSSCPEnforcement.DataSource = dsSSCPEnforcement
            dgvSSCPEnforcement.DataMember = "SSCPEnforcement"



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
            ErrorReport(ex, SQL, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        Try

            LoadSSCPEnforcementDataGrid("ByUnit")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgvSSCPEnforcement_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvSSCPEnforcement.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvSSCPEnforcement.HitTest(e.X, e.Y)

        Try


            If dgvSSCPEnforcement.RowCount > 0 And hti.RowIndex <> -1 Then
                txtEnforcementNumber.Text = dgvSSCPEnforcement(0, hti.RowIndex).Value
                txtAIRSNumber.Text = dgvSSCPEnforcement(5, hti.RowIndex).Value
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnViewAllEnforcements_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewAllEnforcements.Click
        Try

            LoadSSCPEnforcementDataGrid("All")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'Dim ExcelApp As New Excel.Application
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        Dim i As Integer
        Dim j As Integer

        Try

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If
            If dgvSSCPEnforcement.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()
                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvSSCPEnforcement.ColumnCount - 1
                        .Cells(1, i + 1) = dgvSSCPEnforcement.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvSSCPEnforcement.ColumnCount - 1
                        For j = 0 To dgvSSCPEnforcement.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvSSCPEnforcement.Item(i, j).Value.ToString
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
                ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        Finally

        End Try

    End Sub


End Class