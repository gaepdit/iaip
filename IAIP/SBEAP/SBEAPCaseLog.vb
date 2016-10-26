Public Class SBEAPCaseLog

#Region " Properties "

    Private dtCaseLogGrid As DataTable
    Private dtStaff As DataTable
    Private dtCaseWork As DataTable
    Private dtActions As DataTable
    Private SQLAction As String
    Private SQLSearch1 As String
    Private SQLSearch2 As String
    Private SQLSearch3 As String
    Private SQLOrder1 As String
    Private SQLOrder2 As String

#End Region

#Region " Page Load "

    Private Sub SBEAPCaseLog_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            LoadDataSets()
            FormLoad()
            rdbOpenCases.Checked = True

            cboSearchText2.SelectedValue = CurrentUser.UserID

            btnSearchCaseLog.Enabled = False
            btnResetSearch.Enabled = False

            CreateCaseStatement()

            mmiOpenNewCase.Visible = True
            btnOpenCase.Enabled = True

            bgw1.WorkerReportsProgress = True
            bgw1.WorkerSupportsCancellation = True
            bgw1.RunWorkerAsync()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadDataSets()
        Try
            Dim SQL As String = "select " &
            "distinct concat(strLastName,', ',strFirstName) as Staff, " &
            "numUserID " &
            "from EPDUserProfiles inner join SBEAPCaseLog " &
            "on epduserprofiles.numUserID = SBEAPCaseLog.numStaffResponsible " &
            "where (numBranch = '5' and numProgram = '35') "

            dtStaff = DB.GetDataTable(SQL)

            SQL = "Select " &
            "strWorkDescription, numActionType " &
            "from LookUpSBEAPCaseWork " &
            "order by strWorkDescription "

            dtCaseWork = DB.GetDataTable(SQL)

            SQL = "Select " &
            "numActionType, strWorkDescription " &
            "from LookUpSBEAPcaseWork " &
            "order by strWorkDescription  "

            dtActions = DB.GetDataTable(SQL)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FormLoad()
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
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

    Private Sub CreateCaseStatement()
        Try
            SQLAction = ""
            SQLSearch1 = ""
            SQLSearch2 = ""
            SQLSearch3 = ""
            SQLOrder1 = ""
            SQLOrder2 = ""

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
                    SQLSearch1 = "  StaffResponsible like '%, " & Replace(txtSearchText1.Text, "'", "''") & "%' "
                Case "Staff Last Name"
                    SQLSearch1 = "  StaffResponsible like '%" & Replace(txtSearchText1.Text, "'", "''") & ", %' "
                Case "Staff Responsible"
                    If cboSearchText1.SelectedIndex > 0 Then
                        SQLSearch1 = "  StaffResponsible = '" & Replace(cboSearchText1.Text, "'", "''") & "' "
                    End If
                Case "Case Description"
                    SQLSearch1 = "  strCaseSummary like '%" & Replace(txtSearchText1.Text, "'", "''") & "%' "
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
                    SQLSearch2 = " StaffResponsible like '%, " & Replace(txtSearchText2.Text, "'", "''") & "%' "
                Case "Staff Last Name"
                    SQLSearch2 = " StaffResponsible like '%" & Replace(txtSearchText2.Text, "'", "''") & ", %' "
                Case "Staff Responsible"
                    If cboSearchText2.SelectedIndex > 0 Then
                        SQLSearch2 = " StaffResponsible = '" & Replace(cboSearchText2.Text, "'", "''") & "' "
                    End If
                Case "Case Description"
                    SQLSearch2 = " strCaseSummary like '%" & Replace(txtSearchText2.Text, "'", "''") & "%' "
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
                    SQLOrder1 = " VW_SBEAP_CaseLog.numCaseID "
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
                    SQLOrder2 = " VW_SBEAP_CaseLog.numCaseID "
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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SearchCaseWork()
        Try
            Dim SQL As String = "select * from " &
            "((select " &
            "VW_SBEAP_Caselog.*, 'Action' as ActionType " &
            "from VW_SBEAP_Caselog " &
            "where " & SQLSearch3 & " " &
            "and Exists " &
            "(select * " &
            "from SBEAPActionLog " &
            "where VW_SBEAP_Caselog.numCaseID = SBEAPActionLog.numCaseID " &
            " " & SQLAction & ")) " &
            "union " &
            "select * from " &
            "(select " &
            "VW_SBEAP_Caselog.*, 'No Action' as ActionType " &
            "from VW_SBEAP_Caselog " &
            "where " & SQLSearch3 & " " &
            "and Not Exists " &
            "(select * " &
            "from SBEAPActionLog " &
            "where VW_SBEAP_Caselog.numCaseID = SBEAPActionLog.numCaseID)) as t1 ) as t2 " &
            SQLSearch1 & SQLSearch2

            dtCaseLogGrid = DB.GetDataTable(SQL)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub cboFieldType1_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboFieldType1.SelectedValueChanged
        Try
            txtSearchText1.Clear()
            cboSearchText1.Text = ""
            DTPSearchDate1.Value = Today
            DTPSearchDate2.Value = Today

            Select Case cboFieldType1.Text
                Case "Action Type"
                    txtSearchText1.Visible = False
                    cboSearchText1.Visible = True
                    DTPSearchDate1.Visible = False
                    DTPSearchDate2.Visible = False

                    With cboSearchText1
                        .DataSource = dtActions
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

                    With cboSearchText1
                        .DataSource = dtStaff
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
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub cboFieldType2_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboFieldType2.SelectedValueChanged
        Try
            txtSearchText2.Clear()
            cboSearchText2.Text = ""
            DTPSearchDate3.Value = Today
            DTPSearchDate4.Value = Today

            Select Case cboFieldType2.Text
                Case "Action Type"
                    txtSearchText2.Visible = False
                    cboSearchText2.Visible = True
                    DTPSearchDate3.Visible = False
                    DTPSearchDate4.Visible = False

                    With cboSearchText2
                        .DataSource = dtActions
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

                    With cboSearchText2
                        .DataSource = dtStaff
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
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSearchCaseLog_Click(sender As Object, e As EventArgs) Handles btnSearchCaseLog.Click
        Try
            btnSearchCaseLog.Enabled = False
            btnResetSearch.Enabled = False

            CreateCaseStatement()

            bgw1.WorkerReportsProgress = True
            bgw1.WorkerSupportsCancellation = True
            bgw1.RunWorkerAsync()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnOpenCase_Click(sender As Object, e As EventArgs) Handles btnOpenCase.Click
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
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvCaseLog_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCaseLog.CellDoubleClick
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
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvCaseLog_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvCaseLog.MouseUp
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
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

    Private Sub btnResetSearch_Click(sender As Object, e As EventArgs) Handles btnResetSearch.Click
        cboFieldType1.Text = "Case ID"
        cboFieldType2.Text = "Staff Responsible"
        cboSortType1.Text = ""
        cboSortType2.Text = ""
        cboSortOrder1.SelectedIndex = 0
        cboSortOrder2.SelectedIndex = 0
    End Sub

    Private Sub mmiOpenNewCase_Click(sender As Object, e As EventArgs) Handles mmiOpenNewCase.Click
        Try
            If CaseWork Is Nothing Then

            Else
                CaseWork.Dispose()
            End If
            CaseWork = New SBEAPCaseWork
            CaseWork.Show()
            CaseWork.LoadCaseLogData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub bgw1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgw1.DoWork
        SearchCaseWork()
    End Sub

    Private Sub bgw1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgw1.RunWorkerCompleted
        Try
            If dtCaseLogGrid IsNot Nothing Then
                dgvCaseLog.DataSource = dtCaseLogGrid

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
                dgvCaseLog.Columns("numStaffResponsible").HeaderText = "Staff Responsible"
                dgvCaseLog.Columns("numStaffResponsible").DisplayIndex = 7
                dgvCaseLog.Columns("numStaffResponsible").Visible = False
                dgvCaseLog.Columns("strCaseSummary").HeaderText = "Case Description"
                dgvCaseLog.Columns("strCaseSummary").DisplayIndex = 5

                LoadCaseColors()
                btnSearchCaseLog.Enabled = True
                btnResetSearch.Enabled = True

                dgvCaseLog.SanelyResizeColumns()
            Else
                dgvCaseLog.DataSource = Nothing
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadCaseColors()
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
                            temp = Math.Abs(DateDiff(DateInterval.Day, CurrDate, tempdate))
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
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvCaseLog_Sorted(sender As Object, e As EventArgs) Handles dgvCaseLog.Sorted
        LoadCaseColors()
    End Sub

    Private Sub mmiExportToExcel_Click(sender As Object, e As EventArgs) Handles mmiExportToExcel.Click
        dgvCaseLog.ExportToExcel(Me)
    End Sub

End Class