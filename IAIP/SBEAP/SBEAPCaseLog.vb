Imports Oracle.ManagedDataAccess.Client
Imports System.Math

Public Class SBEAPCaseLog
    Dim SQL As String
    Dim dsCaseLog As DataSet
    Dim daActions As OracleDataAdapter
    Dim daStaff As OracleDataAdapter
    Dim daCaseWork As OracleDataAdapter
    Dim dsActions As DataSet
    Dim dsCaseLogGrid As DataSet
    Dim daCaseLogGrid As OracleDataAdapter
    Dim SQLAction As String
    Dim SQLSearch1 As String
    Dim SQLSearch2 As String
    Dim SQLSearch3 As String
    Dim SQLOrder1 As String
    Dim SQLOrder2 As String


    Private Sub SBEAPCaseLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
        Try
            label1.Text = "Select Case Work"
            Label2.Text = CurrentUser.AlphaName
            Label3.Text = OracleDate

            LoadDataSets()
            FormLoad()
            rdbOpenCases.Checked = True

            cboSearchText2.SelectedValue = CurrentUser.UserID

            btnSearchCaseLog.Enabled = False
            btnResetSearch.Enabled = False

            If rdbOpenCases.Checked = True Then
                rdbOpenCases.Checked = True
            End If
            If rdbClosedCase.Checked = True Then
                rdbClosedCase.Checked = True
            End If
            If rdbAllCases.Checked = True Then
                rdbAllCases.Checked = True
            End If

            CreateCaseStatement()

            mmiOpenNewCase.Visible = True
            btnOpenCase.Enabled = True

            bgw1.WorkerReportsProgress = True
            bgw1.WorkerSupportsCancellation = True
            bgw1.RunWorkerAsync()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "Page Load"
    Sub LoadDataSets()
        Try
            dsCaseLog = New DataSet

            SQL = "select " & _
            "distinct(strLastName||', '||strFirstName) as Staff, " & _
            "numUserID " & _
            "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.SBEAPCaseLog " & _
            "where AIRBRANCH.epduserprofiles.numUserID = AIRBRANCH.SBEAPCaseLog.numStaffResponsible " & _
            "or (numBranch = '5' and numProgram = '35') "

            daStaff = New OracleDataAdapter(SQL, CurrentConnection)

            SQL = "Select " & _
            "strWorkDescription, numActionType " & _
            "from AIRBRANCH.LookUpSBEAPCaseWork " & _
            "order by strWorkDescription "

            daCaseWork = New OracleDataAdapter(SQL, CurrentConnection)

            dsActions = New DataSet

            SQL = "Select " & _
            "numActionType, strWorkDescription " & _
            "from AIRbranch.LookUpSBEAPcaseWork " & _
            "order by strWorkDescription  "

            daActions = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStaff.Fill(dsCaseLog, "StaffResponsible")
            daCaseWork.Fill(dsCaseLog, "CaseWork")
            daActions.Fill(dsActions, "Actions")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub FormLoad()
        Try
            cboFieldType1.Items.Clear()
            cboFieldType2.Items.Clear()
            txtSearchText1.Visible = False
            txtSearchText2.Visible = False
            cboSearchText1.Visible = False
            cboSearchText2.Visible = False
            DTPSearchDate1.Visible = False
            DTPSearchDate2.Visible = False
            DTPSearchDate3.Visible = False
            DTPSearchDate4.Visible = False

            cboFieldType1.Items.Add("Action Type")
            cboFieldType1.Items.Add("Case ID")
            cboFieldType1.Items.Add("Customer ID")
            cboFieldType1.Items.Add("Date Case Opened")
            cboFieldType1.Items.Add("Date Case Closed")
            cboFieldType1.Items.Add("Staff First Name")
            cboFieldType1.Items.Add("Staff Last Name")
            cboFieldType1.Items.Add("Staff Responsible")
            cboFieldType1.Items.Add("Case Description")

            cboFieldType2.Items.Add("Action Type")
            cboFieldType2.Items.Add("Case ID")
            cboFieldType2.Items.Add("Customer ID")
            cboFieldType2.Items.Add("Date Case Opened")
            cboFieldType2.Items.Add("Date Case Closed")
            cboFieldType2.Items.Add("Staff First Name")
            cboFieldType2.Items.Add("Staff Last Name")
            cboFieldType2.Items.Add("Staff Responsible")
            cboFieldType2.Items.Add("Case Description")

            cboSortType1.Items.Add("Case ID")
            cboSortType1.Items.Add("Customer ID")
            cboSortType1.Items.Add("Date Case Opened")
            cboSortType1.Items.Add("Date Case Closed")
            cboSortType1.Items.Add("Staff First Name")
            cboSortType1.Items.Add("Staff Last Name")
            cboSortType1.Items.Add("Staff Responsible")
            cboSortType1.Items.Add("Case Description")

            cboSortType2.Items.Add("")
            cboSortType2.Items.Add("Case ID")
            cboSortType2.Items.Add("Customer ID")
            cboSortType2.Items.Add("Date Case Opened")
            cboSortType2.Items.Add("Date Case Closed")
            cboSortType2.Items.Add("Staff First Name")
            cboSortType2.Items.Add("Staff Last Name")
            cboSortType2.Items.Add("Staff Responsible")
            cboSortType2.Items.Add("Case Description")

            cboSortOrder1.Items.Add("Ascending Order")
            cboSortOrder1.Items.Add("Descending Order")
            cboSortOrder2.Items.Add("Ascending Order")
            cboSortOrder2.Items.Add("Descending Order")

            cboFieldType1.Text = "Case ID"
            cboFieldType2.Text = "Staff Responsible"
            cboSortOrder1.Text = cboSortOrder1.Items.Item(0)
            cboSortOrder2.Text = cboSortOrder2.Items.Item(0)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region
#Region "Subs and Functions"
    Sub CreateCaseStatement()
        Try
            SQLAction = ""
            SQLSearch1 = ""
            SQLSearch2 = ""
            SQLSearch3 = ""
            SQLOrder1 = ""
            SQLOrder2 = ""

            'If NavScreen.label1.Text = "TESTING ENVIRONMENT" Then
            Select Case cboFieldType1.Text
                Case "Action Type"
                    SQLAction = " and (numActionType = '" & Replace(cboSearchText1.SelectedValue, "'", "''") & "' ) "
                    SQLSearch1 = " Actiontype = 'Action' "
                Case "Case ID"
                    SQLSearch1 = " numCaseID like '%" & Replace(txtSearchText1.Text, "'", "''") & "%' "
                Case "Customer ID"
                    SQLSearch1 = " clientID like '%" & Replace(txtSearchText1.Text, "'", "''") & "%' "
                Case "Date Case Opened"
                    SQLSearch1 = "  CaseOpened between '" & DTPSearchDate1.Text & "' and '" & DTPSearchDate2.Text & "' "
                Case "Date Case Closed"
                    SQLSearch1 = "  CaseClosed between '" & DTPSearchDate1.Text & "' and '" & DTPSearchDate2.Text & "' "
                Case "Staff First Name"
                    SQLSearch1 = "  upper(StaffResponsible) like '%, " & Replace(txtSearchText1.Text.ToUpper, "'", "''") & "%' "
                Case "Staff Last Name"
                    SQLSearch1 = "  upper(StaffResponsible) like '%" & Replace(txtSearchText1.Text.ToUpper, "'", "''") & ", %' "
                Case "Staff Responsible"
                    If cboSearchText1.SelectedIndex > 0 Then
                        SQLSearch1 = "  Upper(StaffResponsible) = '" & Replace(cboSearchText1.Text.ToUpper, "'", "''") & "' "
                    End If
                Case "Case Description"
                    SQLSearch1 = "  Upper(strCaseSummary) like '%" & Replace(txtSearchText1.Text.ToUpper, "'", "''") & "%' "
                Case Else

            End Select

            Select Case cboFieldType2.Text
                Case "Action Type"
                    If SQLAction <> "" Then
                        SQLAction = Mid(SQLAction, 1, SQLAction.Length - 2)
                        SQLAction = SQLAction & " or numActionType = '" & Replace(cboSearchText2.SelectedValue, "'", "''") & "' ) "
                    Else
                        SQLAction = "and numActionType = '" & Replace(cboSearchText2.SelectedValue, "'", "''") & "' "
                    End If
                    SQLSearch1 = " Actiontype = 'Action' "
                Case "Case ID"
                    SQLSearch2 = " numCaseID like '%" & Replace(txtSearchText2.Text, "'", "''") & "%' "
                Case "Customer ID"
                    SQLSearch2 = " ClientID like '%" & Replace(txtSearchText2.Text, "'", "''") & "%' "
                Case "Date Case Opened"
                    SQLSearch2 = " CaseOpened between '" & DTPSearchDate3.Text & "' and '" & DTPSearchDate4.Text & "' "
                Case "Date Case Closed"
                    SQLSearch2 = " CaseClosed between '" & DTPSearchDate3.Text & "' and '" & DTPSearchDate4.Text & "' "
                Case "Staff First Name"
                    SQLSearch2 = " upper(StaffResponsible) like '%, " & Replace(txtSearchText2.Text.ToUpper, "'", "''") & "%' "
                Case "Staff Last Name"
                    SQLSearch2 = " upper(StaffResponsible) like '%" & Replace(txtSearchText2.Text.ToUpper, "'", "''") & ", %' "
                Case "Staff Responsible"
                    If cboSearchText2.SelectedIndex > 0 Then
                        SQLSearch2 = " Upper(StaffResponsible) = '" & Replace(cboSearchText2.Text.ToUpper, "'", "''") & "' "
                    End If
                Case "Case Description"
                    SQLSearch2 = " Upper(strCaseSummary) like '%" & Replace(txtSearchText2.Text.ToUpper, "'", "''") & "%' "
                Case Else
                    SQLSearch2 = " datCaseClosed is null "
            End Select

            If SQLSearch1 <> "" And SQLSearch2 = "" Then
                SQLSearch1 = " where " & SQLSearch1
            End If
            If SQLSearch1 = "" And SQLSearch2 <> "" Then
                SQLSearch2 = " where " & SQLSearch2
            End If
            If SQLSearch1 <> "" And SQLSearch2 <> "" Then
                If cboFieldType1.Text = cboFieldType2.Text Then
                    SQLSearch1 = " where (" & SQLSearch1 & " or " & SQLSearch2 & ") "
                    SQLSearch2 = ""
                Else
                    SQLSearch1 = " where " & SQLSearch1 & " and " & SQLSearch2
                    SQLSearch2 = ""
                End If
            End If

            Select Case cboSortType1.Text
                Case "Case ID"
                    SQLOrder1 = " AIRBRANCH.VW_SBEAP_CaseLog.numCaseID "
                Case "Customer ID"
                    SQLOrder1 = " ClientID "
                Case "Date Case Opened"
                    SQLOrder1 = " datCaseOpened "
                Case "Date Case Closed"
                    SQLOrder1 = " datCaseClosed "
                Case "Staff First Name"
                    SQLOrder1 = " strFirstName "
                Case "Staff Last Name"
                    SQLOrder1 = " strLastName "
                Case "Staff Responsible"
                    SQLOrder1 = " numUserID "
                Case "Case Description"
                    SQLOrder1 = " strCaseSummary "
                Case Else
                    SQLOrder1 = " "
            End Select
            If SQLOrder1 <> " " Then
                If cboSortOrder1.Text = cboSortOrder1.Items.Item(0) Then
                    SQLOrder1 = SQLOrder1 & " ASC "
                Else
                    SQLOrder1 = SQLOrder1 & " DESC "
                End If
            End If

            Select Case cboSortType2.Text
                Case "Case ID"
                    SQLOrder2 = " AIRBRANCH.VW_SBEAP_CaseLog.numCaseID "
                Case "Customer ID"
                    SQLOrder2 = " ClientID "
                Case "Date Case Opened"
                    SQLOrder2 = " CaseOpened "
                Case "Date Case Closed"
                    SQLOrder2 = " CaseClosed "
                Case "Staff First Name"
                    SQLOrder2 = " strFirstName "
                Case "Staff Last Name"
                    SQLOrder2 = " strLastName "
                Case "Staff Responsible"
                    SQLOrder2 = " numUserID "
                Case "Case Description"
                    SQLOrder2 = " strCaseSummary "
                Case Else
                    SQLOrder2 = " "
            End Select
            If SQLOrder2 <> " " Then
                If cboSortOrder2.Text = cboSortOrder2.Items.Item(0) Then
                    SQLOrder2 = SQLOrder2 & " ASC "
                Else
                    SQLOrder2 = SQLOrder2 & " DESC "
                End If
            End If

            If SQLOrder1 <> " " Or SQLOrder2 <> " " Then
                If SQLOrder1 <> " " And SQLOrder2 <> " " Then
                    SQLOrder1 = " Order by " & SQLOrder1 & ", " & SQLOrder2
                Else
                    If SQLOrder1 <> " " And SQLOrder2 = " " Then
                        SQLOrder1 = " order by " & SQLOrder1
                    Else
                        If SQLOrder1 = " " And SQLOrder2 <> " " Then
                            SQLOrder1 = " Order by " & SQLOrder2
                        Else
                            SQLOrder1 = " "
                        End If
                    End If
                End If
            Else
                SQLOrder1 = " "
            End If

            If rdbOpenCases.Checked = True Then
                SQLSearch3 = " CaseClosed is null "
                rdbOpenCases.Checked = True
            End If
            If rdbClosedCase.Checked = True Then
                SQLSearch3 = " CaseClosed is not Null "
                rdbClosedCase.Checked = True
            End If
            If rdbAllCases.Checked = True Then
                SQLSearch3 = " (CaseClosed is null or CaseClosed is not Null) "
                rdbAllCases.Checked = True
            End If
            'Else
            '    Select Case cboFieldType1.Text
            '        Case "Action Type"

            '        Case "Case ID"
            '            SQLSearch1 = " and " & connNameSpace & ".SBEAPCaseLog.numCaseID like '%" & Replace(txtSearchText1.Text, "'", "''") & "%' "
            '        Case "Customer ID"
            '            SQLSearch1 = " and " & connNameSpace & ".SBEAPClients.ClientID like '%" & Replace(txtSearchText1.Text, "'", "''") & "%' "
            '        Case "Date Case Opened"
            '            SQLSearch1 = " and datCaseOpened between '" & DTPSearchDate1.Text & "' and '" & DTPSearchDate2.Text & "' "
            '        Case "Date Case Closed"
            '            SQLSearch1 = " and datCaseClosed between '" & DTPSearchDate1.Text & "' and '" & DTPSearchDate2.Text & "' "
            '        Case "Staff First Name"
            '            SQLSearch1 = " and upper(strFirstName) like '%" & Replace(txtSearchText1.Text.ToUpper, "'", "''") & "%' "
            '        Case "Staff Last Name"
            '            SQLSearch1 = " and upper(strLastName) like '%" & Replace(txtSearchText1.Text.ToUpper, "'", "''") & "%' "
            '        Case "Staff Responsible"
            '            If cboSearchText1.SelectedIndex > 0 Then
            '                SQLSearch1 = " and numUserID = '" & Replace(cboSearchText1.SelectedValue, "'", "''") & "' "
            '            End If
            '        Case "Case Description"
            '            SQLSearch1 = " and Upper(strCaseSummary) like '%" & Replace(txtSearchText1.Text.ToUpper, "'", "''") & "%' "
            '        Case Else

            '    End Select

            '    Select Case cboFieldType2.Text
            '        Case "Action Type"

            '        Case "Case ID"
            '            SQLSearch2 = " and " & connNameSpace & ".SBEAPCaseLog.numCaseID like '%" & Replace(txtSearchText2.Text, "'", "''") & "%' "
            '        Case "Customer ID"
            '            SQLSearch2 = " and " & connNameSpace & ".SBEAPClients.ClientID like '%" & Replace(txtSearchText2.Text, "'", "''") & "%' "
            '        Case "Date Case Opened"
            '            SQLSearch2 = " and datCaseOpened between '" & DTPSearchDate3.Text & "' and '" & DTPSearchDate4.Text & "' "
            '        Case "Date Case Closed"
            '            SQLSearch2 = " and datCaseClosed between '" & DTPSearchDate3.Text & "' and '" & DTPSearchDate4.Text & "' "
            '        Case "Staff First Name"
            '            SQLSearch2 = " and upper(strFirstName) like '%" & Replace(txtSearchText2.Text.ToUpper, "'", "''") & "%' "
            '        Case "Staff Last Name"
            '            SQLSearch2 = " and upper(strLastName) like '%" & Replace(txtSearchText2.Text.ToUpper, "'", "''") & "%' "
            '        Case "Staff Responsible"
            '            If cboSearchText2.SelectedIndex > 0 Then
            '                SQLSearch2 = " and numUserID = '" & Replace(cboSearchText2.SelectedValue, "'", "''") & "' "
            '            End If
            '        Case "Case Description"
            '            SQLSearch2 = " and Upper(strCaseSummary) like '%" & Replace(txtSearchText2.Text.ToUpper, "'", "''") & "%' "
            '        Case Else
            '            SQLSearch2 = " and datCaseClosed is null "
            '    End Select

            '    Select Case cboSortType1.Text
            '        Case "Case ID"
            '            SQLOrder1 = " " & connNameSpace & ".SBEAPCaseLog.numCaseID "
            '        Case "Customer ID"
            '            SQLOrder1 = " ClientID "
            '        Case "Date Case Opened"
            '            SQLOrder1 = " datCaseOpened "
            '        Case "Date Case Closed"
            '            SQLOrder1 = " datCaseClosed "
            '        Case "Staff First Name"
            '            SQLOrder1 = " strFirstName "
            '        Case "Staff Last Name"
            '            SQLOrder1 = " strLastName "
            '        Case "Staff Responsible"
            '            SQLOrder1 = " numUserID "
            '        Case "Case Description"
            '            SQLOrder1 = " strCaseSummary "
            '        Case Else
            '            SQLOrder1 = " "
            '    End Select
            '    If SQLOrder1 <> " " Then
            '        If cboSortOrder1.Text = cboSortOrder1.Items.Item(0) Then
            '            SQLOrder1 = SQLOrder1 & " ASC "
            '        Else
            '            SQLOrder1 = SQLOrder1 & " DESC "
            '        End If
            '    End If

            '    Select Case cboSortType2.Text
            '        Case "Case ID"
            '            SQLOrder2 = " " & connNameSpace & ".SBEAPCaseLog.numCaseID "
            '        Case "Customer ID"
            '            SQLOrder2 = " ClientID "
            '        Case "Date Case Opened"
            '            SQLOrder2 = " datCaseOpened "
            '        Case "Date Case Closed"
            '            SQLOrder2 = " datCaseClosed "
            '        Case "Staff First Name"
            '            SQLOrder2 = " strFirstName "
            '        Case "Staff Last Name"
            '            SQLOrder2 = " strLastName "
            '        Case "Staff Responsible"
            '            SQLOrder2 = " numUserID "
            '        Case "Case Description"
            '            SQLOrder2 = " strCaseSummary "
            '        Case Else
            '            SQLOrder2 = " "
            '    End Select
            '    If SQLOrder2 <> " " Then
            '        If cboSortOrder2.Text = cboSortOrder2.Items.Item(0) Then
            '            SQLOrder2 = SQLOrder2 & " ASC "
            '        Else
            '            SQLOrder2 = SQLOrder2 & " DESC "
            '        End If
            '    End If

            '    If SQLOrder1 <> " " Or SQLOrder2 <> " " Then
            '        If SQLOrder1 <> " " And SQLOrder2 <> " " Then
            '            SQLOrder1 = " Order by " & SQLOrder1 & ", " & SQLOrder2
            '        Else
            '            If SQLOrder1 <> " " And SQLOrder2 = " " Then
            '                SQLOrder1 = " order by " & SQLOrder1
            '            Else
            '                If SQLOrder1 = " " And SQLOrder2 <> " " Then
            '                    SQLOrder1 = " Order by " & SQLOrder2
            '                Else
            '                    SQLOrder1 = " "
            '                End If
            '            End If
            '        End If
            '    Else
            '        SQLOrder1 = " "
            '    End If

            '    If rdbOpenCases.Checked = True Then
            '        SQLSearch3 = " and datCaseClosed is null "
            '        rdbOpenCases.Checked = True
            '    End If
            '    If rdbClosedCase.Checked = True Then
            '        SQLSearch3 = " and datCaseClosed is not Null "
            '        rdbClosedCase.Checked = True
            '    End If
            '    If rdbAllCases.Checked = True Then
            '        SQLSearch3 = " "
            '        rdbAllCases.Checked = True
            '    End If
            'End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SearchCaseWork()
        Try
            'SQL = "Select " & _
            '"" & connNameSpace & ".SBEAPCaseLog.numCaseID, " & _
            '"numStaffResponsible, " & _
            '"case " & _
            '"when numStaffResponsible is Null then '' " & _
            '"Else (strLastName||', '||strFirstName) " & _
            '"END StaffResponsible, " & _
            '"to_date(datCaseOpened, 'dd-Mon-RRRR') as CaseOpened, " & _
            '"to_date(datCaseClosed, 'dd-Mon-RRRR') as CaseClosed, " & _
            '"strCompanyName, strCaseSummary, " & _
            '"" & connNameSpace & ".SBEAPCaseLogLink.ClientID " & _
            '"from " & connNameSpace & ".SBEAPCaseLog, " & connNameSpace & ".EPDUserProfiles, " & _
            '"" & connNameSpace & ".SBEAPClients, " & connNameSpace & ".SBEAPCaseLogLink " & _
            '"where " & connNameSpace & ".SBEAPCaseLog.numStaffResponsible = " & connNameSpace & ".EPDUserProfiles.numUserID (+) " & _
            '"and " & connNameSpace & ".SBEAPCaseLog.numCaseID = " & connNameSpace & ".SBEAPCaseLogLink.numCaseID (+) " & _
            '"and " & connNameSpace & ".SBEAPCaseLogLink.ClientID = " & connNameSpace & ".SBEAPClients.ClientID (+) " & _
            'SQLSearch1 & SQLSearch2 & SQLSearch3 & SQLOrder1

            'If NavScreen.label1.Text = "TESTING ENVIRONMENT" Then
            SQL = "select * from " & _
            "((select " & _
            "AIRBRANCH.VW_SBEAP_Caselog.*, 'Action' as ActionType " & _
            "from AIRBRANCH.VW_SBEAP_Caselog " & _
            "where " & SQLSearch3 & " " & _
            "and Exists " & _
            "(select * " & _
            "from AIRBRANCH.SBEAPActionLog " & _
            "where AIRBRANCH.VW_SBEAP_Caselog.numCaseID = AIRBRANCH.SBEAPActionLog.numCaseID " & _
            " " & SQLAction & ")) " & _
            "union " & _
            "select * from " & _
            "(select " & _
            "AIRBRANCH.VW_SBEAP_Caselog.*, 'No Action' as ActionType " & _
            "from AIRBRANCH.VW_SBEAP_Caselog " & _
            "where " & SQLSearch3 & " " & _
            "and Not Exists " & _
            "(select * " & _
            "from AIRBRANCH.SBEAPActionLog " & _
            "where AIRBRANCH.VW_SBEAP_Caselog.numCaseID = AIRBRANCH.SBEAPActionLog.numCaseID))) " & _
            SQLSearch1 & SQLSearch2
            'End If

            dsCaseLogGrid = New DataSet
            daCaseLogGrid = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daCaseLogGrid.Fill(dsCaseLogGrid, "NavScreen")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


#End Region
    Private Sub cboFieldType1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFieldType1.SelectedValueChanged
        Try
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow
            Dim dtCaseLog As New DataTable
            Dim dtActionType As New DataTable

            txtSearchText1.Clear()
            cboSearchText1.Text = ""
            DTPSearchDate1.Value = OracleDate
            DTPSearchDate2.Value = OracleDate

            Select Case cboFieldType1.Text
                Case "Action Type"
                    txtSearchText1.Visible = False
                    cboSearchText1.Visible = True
                    DTPSearchDate1.Visible = False
                    DTPSearchDate2.Visible = False

                    dtActionType.Columns.Add("strWorkDescription", GetType(System.String))
                    dtActionType.Columns.Add("numActionType", GetType(System.String))
                    drNewRow = dtActionType.NewRow()
                    drNewRow("strWorkDescription") = ""
                    drNewRow("numActionType") = " "
                    dtActionType.Rows.Add(drNewRow)

                    For Each drDSRow In dsActions.Tables("Actions").Rows()
                        drNewRow = dtActionType.NewRow()
                        drNewRow("strWorkDescription") = drDSRow("strWorkDescription")
                        drNewRow("numActionType") = drDSRow("numActionType")
                        dtActionType.Rows.Add(drNewRow)
                    Next

                    With cboSearchText1
                        .DataSource = dtActionType
                        .DisplayMember = "strWorkDescription"
                        .ValueMember = "numActionType"
                        .SelectedIndex = 0
                    End With
                Case "Case ID"
                    txtSearchText1.Visible = True
                    cboSearchText1.Visible = False
                    DTPSearchDate1.Visible = False
                    DTPSearchDate2.Visible = False
                Case "Customer ID"
                    txtSearchText1.Visible = True
                    cboSearchText1.Visible = False
                    DTPSearchDate1.Visible = False
                    DTPSearchDate2.Visible = False
                Case "Date Case Opened"
                    txtSearchText1.Visible = False
                    cboSearchText1.Visible = False
                    DTPSearchDate1.Visible = True
                    DTPSearchDate2.Visible = True
                Case "Date Case Closed"
                    txtSearchText1.Visible = False
                    cboSearchText1.Visible = False
                    DTPSearchDate1.Visible = True
                    DTPSearchDate2.Visible = True
                Case "Staff First Name"
                    txtSearchText1.Visible = True
                    cboSearchText1.Visible = False
                    DTPSearchDate1.Visible = False
                    DTPSearchDate2.Visible = False
                Case "Staff Last Name"
                    txtSearchText1.Visible = True
                    cboSearchText1.Visible = False
                    DTPSearchDate1.Visible = False
                    DTPSearchDate2.Visible = False
                Case "Staff Responsible"
                    txtSearchText1.Visible = False
                    cboSearchText1.Visible = True
                    DTPSearchDate1.Visible = False
                    DTPSearchDate2.Visible = False

                    dtCaseLog.Columns.Add("numUserID", GetType(System.String))
                    dtCaseLog.Columns.Add("Staff", GetType(System.String))
                    drNewRow = dtCaseLog.NewRow()
                    drNewRow("numUserID") = ""
                    drNewRow("Staff") = " "
                    dtCaseLog.Rows.Add(drNewRow)

                    For Each drDSRow In dsCaseLog.Tables("StaffResponsible").Rows()
                        drNewRow = dtCaseLog.NewRow()
                        drNewRow("numUserID") = drDSRow("numUserID")
                        drNewRow("Staff") = drDSRow("Staff")
                        dtCaseLog.Rows.Add(drNewRow)
                    Next

                    With cboSearchText1
                        .DataSource = dtCaseLog
                        .DisplayMember = "Staff"
                        .ValueMember = "numUserID"
                        .SelectedIndex = 0
                    End With
                Case "Case Description"
                    txtSearchText1.Visible = True
                    cboSearchText1.Visible = False
                    DTPSearchDate1.Visible = False
                    DTPSearchDate2.Visible = False
                Case Else
                    txtSearchText1.Visible = False
                    cboSearchText1.Visible = False
                    DTPSearchDate1.Visible = False
                    DTPSearchDate2.Visible = False
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboFieldType2_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFieldType2.SelectedValueChanged
        Try
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow
            Dim dtCaseLog As New DataTable
            Dim dtActionType As New DataTable

            txtSearchText2.Clear()
            cboSearchText2.Text = ""
            DTPSearchDate3.Value = OracleDate
            DTPSearchDate4.Value = OracleDate

            Select Case cboFieldType2.Text
                Case "Action Type"
                    txtSearchText2.Visible = False
                    cboSearchText2.Visible = True
                    DTPSearchDate3.Visible = False
                    DTPSearchDate4.Visible = False

                    dtActionType.Columns.Add("strWorkDescription", GetType(System.String))
                    dtActionType.Columns.Add("numActionType", GetType(System.String))
                    drNewRow = dtActionType.NewRow()
                    drNewRow("strWorkDescription") = ""
                    drNewRow("numActionType") = " "
                    dtActionType.Rows.Add(drNewRow)

                    For Each drDSRow In dsActions.Tables("Actions").Rows()
                        drNewRow = dtActionType.NewRow()
                        drNewRow("strWorkDescription") = drDSRow("strWorkDescription")
                        drNewRow("numActionType") = drDSRow("numActionType")
                        dtActionType.Rows.Add(drNewRow)
                    Next

                    With cboSearchText2
                        .DataSource = dtActionType
                        .DisplayMember = "strWorkDescription"
                        .ValueMember = "numActionType"
                        .SelectedIndex = 0
                    End With
                Case "Case ID"
                    txtSearchText2.Visible = True
                    cboSearchText2.Visible = False
                    DTPSearchDate3.Visible = False
                    DTPSearchDate4.Visible = False
                Case "Customer ID"
                    txtSearchText2.Visible = True
                    cboSearchText2.Visible = False
                    DTPSearchDate3.Visible = False
                    DTPSearchDate4.Visible = False
                Case "Date Case Opened"
                    txtSearchText2.Visible = False
                    cboSearchText2.Visible = False
                    DTPSearchDate3.Visible = True
                    DTPSearchDate4.Visible = True
                Case "Date Case Closed"
                    txtSearchText2.Visible = False
                    cboSearchText2.Visible = False
                    DTPSearchDate3.Visible = True
                    DTPSearchDate4.Visible = True
                Case "Staff First Name"
                    txtSearchText2.Visible = True
                    cboSearchText2.Visible = False
                    DTPSearchDate3.Visible = False
                    DTPSearchDate4.Visible = False
                Case "Staff Last Name"
                    txtSearchText2.Visible = True
                    cboSearchText2.Visible = False
                    DTPSearchDate3.Visible = False
                    DTPSearchDate4.Visible = False
                Case "Staff Responsible"
                    txtSearchText2.Visible = False
                    cboSearchText2.Visible = True
                    DTPSearchDate3.Visible = False
                    DTPSearchDate4.Visible = False

                    dtCaseLog.Columns.Add("numUserID", GetType(System.String))
                    dtCaseLog.Columns.Add("Staff", GetType(System.String))
                    drNewRow = dtCaseLog.NewRow()
                    drNewRow("numUserID") = ""
                    drNewRow("Staff") = " "
                    dtCaseLog.Rows.Add(drNewRow)

                    For Each drDSRow In dsCaseLog.Tables("StaffResponsible").Rows()
                        drNewRow = dtCaseLog.NewRow()
                        drNewRow("numUserID") = drDSRow("numUserID")
                        drNewRow("Staff") = drDSRow("Staff")
                        dtCaseLog.Rows.Add(drNewRow)
                    Next

                    With cboSearchText2
                        .DataSource = dtCaseLog
                        .DisplayMember = "Staff"
                        .ValueMember = "numUserID"
                        .SelectedIndex = 0
                    End With
                Case "Case Description"
                    txtSearchText2.Visible = True
                    cboSearchText2.Visible = False
                    DTPSearchDate3.Visible = False
                    DTPSearchDate4.Visible = False
                Case Else
                    txtSearchText2.Visible = False
                    cboSearchText2.Visible = False
                    DTPSearchDate3.Visible = False
                    DTPSearchDate4.Visible = False
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSearchCaseLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCaseLog.Click
        Try
            btnSearchCaseLog.Enabled = False
            btnResetSearch.Enabled = False

            CreateCaseStatement()

            bgw1.WorkerReportsProgress = True
            bgw1.WorkerSupportsCancellation = True
            bgw1.RunWorkerAsync()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnOpenCase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenCase.Click
        Try
            If txtCaseID.Text <> "" Then
                If CaseWork Is Nothing Then

                Else
                    CaseWork.Dispose()
                End If
                CaseWork = New SBEAPCaseWork
                CaseWork.txtCaseID.Text = txtCaseID.Text
                CaseWork.Show()
                CaseWork.LoadCaseLogData()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvCaseLog_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCaseLog.CellDoubleClick
        Try
            If e.RowIndex > -1 Then
                If dgvCaseLog.Columns(0).HeaderText = "Case ID" Then
                    If IsDBNull(dgvCaseLog(0, e.RowIndex).Value) Then
                        txtCaseID.Text = ""
                    Else
                        txtCaseID.Text = dgvCaseLog(0, e.RowIndex).Value
                        If CaseWork Is Nothing Then

                        Else
                            CaseWork.Dispose()
                        End If
                        CaseWork = New SBEAPCaseWork
                        CaseWork.txtCaseID.Text = txtCaseID.Text
                        CaseWork.Show()
                        CaseWork.LoadCaseLogData()
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvCaseLog_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvCaseLog.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvCaseLog.HitTest(e.X, e.Y)
            If dgvCaseLog.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvCaseLog.Columns(0).HeaderText = "Case ID" Then
                    If IsDBNull(dgvCaseLog(0, hti.RowIndex).Value) Then
                        txtCaseID.Text = ""
                    Else
                        txtCaseID.Text = dgvCaseLog(0, hti.RowIndex).Value
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub tsbExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExport.Click
        dgvCaseLog.ExportToExcel(Me)
    End Sub
    Private Sub btnResetSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetSearch.Click
        Try
            cboFieldType1.Text = "Case ID"
            cboFieldType2.Text = "Staff Responsible"
            cboSortType1.Text = ""
            cboSortType2.Text = ""
            cboSortOrder1.Text = cboSortOrder1.Items.Item(0)
            cboSortOrder2.Text = cboSortOrder2.Items.Item(0)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Try
            Me.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub mmiOpenNewCase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiOpenNewCase.Click
        Try
            If CaseWork Is Nothing Then

            Else
                CaseWork.Dispose()
            End If
            CaseWork = New SBEAPCaseWork
            CaseWork.Show()
            CaseWork.LoadCaseLogData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgw1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgw1.DoWork
        Try
            SearchCaseWork()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgw1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgw1.RunWorkerCompleted
        Try
            dgvCaseLog.DataSource = dsCaseLogGrid
            dgvCaseLog.DataMember = "NavScreen"

            dgvCaseLog.RowHeadersVisible = False
            dgvCaseLog.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvCaseLog.AllowUserToResizeColumns = True
            dgvCaseLog.AllowUserToAddRows = False
            dgvCaseLog.AllowUserToDeleteRows = False
            dgvCaseLog.AllowUserToOrderColumns = True
            dgvCaseLog.AllowUserToResizeRows = True
            dgvCaseLog.ColumnHeadersHeight = "35"

            dgvCaseLog.Columns("numCaseID").HeaderText = "Case ID"
            dgvCaseLog.Columns("numCaseID").DisplayIndex = 0
            dgvCaseLog.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvCaseLog.Columns("StaffResponsible").DisplayIndex = 4
            dgvCaseLog.Columns("CaseOpened").HeaderText = "Date Case Opened"
            dgvCaseLog.Columns("CaseOpened").DisplayIndex = 3
            dgvCaseLog.Columns("CaseOpened").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseLog.Columns("CaseClosed").HeaderText = "Date Case Closed"
            dgvCaseLog.Columns("CaseClosed").DisplayIndex = 6
            dgvCaseLog.Columns("CaseClosed").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseLog.Columns("strCompanyName").HeaderText = "Client Name"
            dgvCaseLog.Columns("strCompanyName").DisplayIndex = 1
            dgvCaseLog.Columns("strCompanyName").Width = "200"
            dgvCaseLog.Columns("ClientID").HeaderText = "Customer ID"
            dgvCaseLog.Columns("ClientID").DisplayIndex = 2
            'dgvCaseLog.Columns("ClientID").Visible = False
            dgvCaseLog.Columns("numStaffResponsible").HeaderText = "Staff Responsible"
            dgvCaseLog.Columns("numStaffResponsible").DisplayIndex = 7
            dgvCaseLog.Columns("numStaffResponsible").Visible = False
            dgvCaseLog.Columns("strCaseSummary").HeaderText = "Case Description"
            dgvCaseLog.Columns("strCaseSummary").DisplayIndex = 5

            LoadCaseColors()
            btnSearchCaseLog.Enabled = True
            btnResetSearch.Enabled = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub LoadCaseColors()
        Try
            Dim tempdate As Date = Date.Today
            Dim CurrDate As Date = Date.Today
            Dim temp As String = ""

            For Each row As DataGridViewRow In dgvCaseLog.Rows
                If Not row.IsNewRow Then
                    If Not row.Cells(8).Value Is DBNull.Value Then
                        If Not row.Cells(4).Value Is DBNull.Value Then
                            temp = row.Cells(4).Value
                            row.DefaultCellStyle.BackColor = Color.White
                        Else
                            tempdate = row.Cells(8).Value
                            temp = Abs(DateDiff(DateInterval.Day, CurrDate, tempdate))
                            If temp < 15 Then
                                row.DefaultCellStyle.BackColor = Color.White
                            End If
                            If temp > 15 And temp < 30 Then
                                row.DefaultCellStyle.BackColor = Color.Pink
                            End If
                            If temp > 30 Then
                                row.DefaultCellStyle.BackColor = Color.Tomato
                            End If
                        End If
                    Else
                        row.DefaultCellStyle.BackColor = Color.White
                    End If
                End If
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvCaseLog_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvCaseLog.Sorted
        Try
            LoadCaseColors()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class