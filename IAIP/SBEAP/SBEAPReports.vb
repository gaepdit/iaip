Imports System.Data.SqlClient

Public Class SBEAPReports
    Private dtAction As DataTable

    Private Sub SBEAPReports_Load(sender As Object, e As EventArgs) Handles Me.Load
        DTPReportStartDate.Value = Today.AddMonths(-3)
        DTPReportEndDate.Value = Today

        LoadComboBoxes()
    End Sub

    Private Sub LoadComboBoxes()
        Try
            Dim query As String = "select " &
            "'' as numActionType, ' ' as strWorkDescription " &
            " union select " &
            "convert(varchar(max),numActionType), strWorkDescription " &
            "from LookUpSBEAPCaseWOrk " &
            "order by strWorkDescription "

            With cboActionTypes
                .DataSource = DB.GetDataTable(query)
                .DisplayMember = "strWorkDescription"
                .ValueMember = "numActionType"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ClearReport()
        Try
            txtNewClientCount.Clear()
            txtClientAssistCount.Clear()
            txtNewCaseCount.Clear()
            txtExistingCaseCount.Clear()
            txtTotalCaseCount.Clear()
            txtCaseClosedCount.Clear()
            txtTotalActionCount.Clear()
            txtActionTypeCount.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub RunReport()
        Try
            ClearReport()

            Dim p As SqlParameter() = {
                New SqlParameter("@sdate", DTPReportStartDate.Value),
                New SqlParameter("@edate", DTPReportEndDate.Value)
            }

            Dim query As String = "Select count(*) " &
            "from SBEAPClients " &
            "where datCompanyCreated between @sdate and @edate "

            txtNewClientCount.Text = DB.GetInteger(query, p)

            query = "select count(*) " &
            "from (select " &
            "distinct(SBEAPActionLog.numCaseID) as CaseID " &
            "from SBEAPActionLog, SBEAPCaseLog, " &
            "SBEAPCaseLogLink, SBEAPClients  " &
            "where SBEAPActionLog.numCaseID = SBEAPCaseLog.numCaseID " &
            "and SBEAPActionLog.numCaseID = SBEAPCaseLogLink.numCaseID " &
            "and SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID " &
            "and  (datActionOccured between @sdate and @edate " &
            "or datCaseOpened between @sdate and @edate " &
            "or datCaseClosed between @sdate and @edate ) ) t "

            txtClientAssistCount.Text = DB.GetInteger(query, p)

            query = "select count(*) " &
            "from SBEAPCaseLog " &
            "where datCaseOpened between @sdate and @edate "

            txtNewCaseCount.Text = DB.GetInteger(query, p)

            query = "select count(*) as Count " &
            "from (select distinct(SBEAPCaseLog.numCaseID) " &
            "from SBEAPCaseLog, SBEAPActionLog  " &
            "where SBEAPCaseLog.numCaseID = SBEAPActionLog.numCaseID " &
            "and datCaseOpened < @sdate " &
            "and (datActionOccured between @sdate and @edate " &
            "or datCaseClosed between @sdate and @edate " &
            "or sbeapActionLog.datCreationDate between @sdate and @edate )) t "

            txtExistingCaseCount.Text = DB.GetInteger(query, p)

            If txtNewCaseCount.Text = "" Then
                txtNewCaseCount.Text = "0"
            End If
            If txtExistingCaseCount.Text = "" Then
                txtExistingCaseCount.Text = "0"
            End If

            txtTotalCaseCount.Text = CInt(txtNewCaseCount.Text) + CInt(txtExistingCaseCount.Text)

            query = "select Count(*)  " &
            "from SBEAPCaseLog " &
            "where datCasecLosed between @sdate and @edate "

            txtCaseClosedCount.Text = DB.GetInteger(query, p)

            query = "select count(*)  " &
            "from SBEAPActionLog " &
            "where datActionOccured between @sdate and @edate "

            txtTotalActionCount.Text = DB.GetInteger(query, p)

            query = "Select count(*)  " &
            "from SBEAPActionLog, " &
            "SBEAPPhoneLog " &
            "where SBEAPActionLog.numActionID = SBEAPPhoneLog.numActionID " &
            "and datActionOccured between @sdate and @edate " &
            "and strFrontDeskCall is not Null " &
            "and strFrontDeskCall = 'True' "

            txtFrontDeskCallCount.Text = DB.GetInteger(query, p)

            query = "select Count(*) as Count, numActionType " &
            "from SBEAPActionLog " &
            "where datActionOccured between @sdate and @edate " &
            "group by numActionType "

            dtAction = DB.GetDataTable(query, p)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewCase_Click(sender As Object, e As EventArgs) Handles btnViewCase.Click
        Try
            If txtCaseID.Text <> "" Then
                If CaseWork IsNot Nothing Then
                    CaseWork.Dispose()
                End If

                CaseWork = New SBEAPCaseWork

                If CaseWork IsNot Nothing AndAlso Not CaseWork.IsDisposed Then
                    CaseWork.txtCaseID.Text = txtCaseID.Text
                    CaseWork.Show()
                    CaseWork.LoadCaseLogData()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRunSBEAPReport_Click(sender As Object, e As EventArgs) Handles btnRunSBEAPReport.Click
        RunReport()
    End Sub

    Private Sub llbViewNewClient_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewNewClient.LinkClicked
        Try
            Dim p As SqlParameter() = {
                New SqlParameter("@sdate", DTPReportStartDate.Value),
                New SqlParameter("@edate", DTPReportEndDate.Value)
            }

            Dim query As String = "select " &
            "SBEAPClients.ClientID, strCompanyName, " &
            "datCompanyCreated as datCompanyCreated, STRCLIENTDESCRIPTION " &
            "from SBEAPClients, SBEAPCLientData " &
            "where SBEAPClients.ClientID = SBEAPClientData.ClientID " &
            "and datCompanyCreated between @sdate and @edate "

            dgvCaseWork.DataSource = DB.GetDataTable(query, p)

            dgvCaseWork.RowHeadersVisible = False
            dgvCaseWork.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvCaseWork.AllowUserToResizeColumns = True
            dgvCaseWork.AllowUserToAddRows = False
            dgvCaseWork.AllowUserToDeleteRows = False
            dgvCaseWork.AllowUserToOrderColumns = True
            dgvCaseWork.AllowUserToResizeRows = True

            dgvCaseWork.Columns("ClientID").HeaderText = "Customer ID"
            dgvCaseWork.Columns("ClientID").DisplayIndex = 0
            dgvCaseWork.Columns("strCompanyName").HeaderText = "Customer Company"
            dgvCaseWork.Columns("strCompanyName").DisplayIndex = 1
            dgvCaseWork.Columns("STRCLIENTDESCRIPTION").HeaderText = "Customer Description"
            dgvCaseWork.Columns("STRCLIENTDESCRIPTION").DisplayIndex = 3
            dgvCaseWork.Columns("datCompanyCreated").HeaderText = "Date Client Created"
            dgvCaseWork.Columns("datCompanyCreated").DisplayIndex = 2
            dgvCaseWork.Columns("datCompanyCreated").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseWork.SanelyResizeColumns

            txtCount.Text = dgvCaseWork.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbClientsAssisted_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbClientsAssisted.LinkClicked
        Try
            Dim p As SqlParameter() = {
                New SqlParameter("@sdate", DTPReportStartDate.Value),
                New SqlParameter("@edate", DTPReportEndDate.Value)
            }

            Dim query As String = "select " &
            "distinct(SBEAPActionLog.numCaseID) as CaseID, " &
            "SBEAPClients.ClientID, strCompanyName, " &
            "case " &
            "when datCaseOpened between @sdate and @edate then 'Case Opened' " &
            "when datCaseClosed between @sdate and @edate  then 'Case Closed' " &
            "when datActionOccured between @sdate and @edate then 'Action Occured' " &
            "end CountReason, " &
            "case " &
            "when datCaseOpened between @sdate and @edate then datCaseOpened " &
            "when datCaseClosed between @sdate and @edate then datCaseClosed " &
            "else null   " &
            "end CountDate, strCaseSummary " &
            "from SBEAPActionLog, SBEAPCaseLog, " &
            "SBEAPCaseLogLink, SBEAPClients  " &
            "where SBEAPActionLog.numCaseID = SBEAPCaseLog.numCaseID " &
            "and SBEAPActionLog.numCaseID = SBEAPCaseLogLink.numCaseID " &
            "and SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID " &
            "and  (datActionOccured between @sdate and @edate " &
            "or datCaseOpened between @sdate and @edate " &
            "or datCaseClosed between @sdate and @edate) "

            dgvCaseWork.DataSource = DB.GetDataTable(query, p)

            dgvCaseWork.RowHeadersVisible = False
            dgvCaseWork.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvCaseWork.AllowUserToResizeColumns = True
            dgvCaseWork.AllowUserToAddRows = False
            dgvCaseWork.AllowUserToDeleteRows = False
            dgvCaseWork.AllowUserToOrderColumns = True
            dgvCaseWork.AllowUserToResizeRows = True

            dgvCaseWork.Columns("ClientID").HeaderText = "Customer ID"
            dgvCaseWork.Columns("ClientID").DisplayIndex = 0
            dgvCaseWork.Columns("strCompanyName").HeaderText = "Customer Company"
            dgvCaseWork.Columns("strCompanyName").DisplayIndex = 1
            dgvCaseWork.Columns("CountReason").HeaderText = "Count Reason"
            dgvCaseWork.Columns("CountReason").DisplayIndex = 2
            dgvCaseWork.Columns("CountDate").HeaderText = "Date of Activity"
            dgvCaseWork.Columns("CountDate").DisplayIndex = 3
            dgvCaseWork.Columns("CountDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseWork.Columns("CaseID").HeaderText = "Case ID"
            dgvCaseWork.Columns("CaseID").DisplayIndex = 4
            dgvCaseWork.Columns("strCaseSummary").HeaderText = "Case Summary"
            dgvCaseWork.Columns("strCaseSummary").DisplayIndex = 4
            dgvCaseWork.SanelyResizeColumns

            txtCount.Text = dgvCaseWork.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbNewCases_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbNewCases.LinkClicked
        Try
            Dim p As SqlParameter() = {
                New SqlParameter("@sdate", DTPReportStartDate.Value),
                New SqlParameter("@edate", DTPReportEndDate.Value)
            }

            Dim query As String = "select SBEAPCaseLog.numCaseID, " &
            "SBEAPClients.ClientID, strCompanyName, " &
            "strCaseSummary, " &
            "datCaseOpened as datCaseOpened, " &
            "concat(strLastName, ', ' ,strFirstName) as StaffResponsible " &
            "from SBEAPCaseLog " &
            "left join SBEAPCaseLogLink " &
            "on SBEAPCaseLog.numCaseID  = SBEAPCaseLogLink.numCaseID " &
            "left join SBEAPClients " &
            "on SBEAPCaseLogLink.ClientID = SbeapClients.CLientID " &
            "left join EPDUserProfiles " &
            "on SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID " &
            "where datCaseOpened between @sdate and @edate "

            dgvCaseWork.DataSource = DB.GetDataTable(query, p)

            dgvCaseWork.RowHeadersVisible = False
            dgvCaseWork.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvCaseWork.AllowUserToResizeColumns = True
            dgvCaseWork.AllowUserToAddRows = False
            dgvCaseWork.AllowUserToDeleteRows = False
            dgvCaseWork.AllowUserToOrderColumns = True
            dgvCaseWork.AllowUserToResizeRows = True

            dgvCaseWork.Columns("ClientID").HeaderText = "Customer ID"
            dgvCaseWork.Columns("ClientID").DisplayIndex = 0
            dgvCaseWork.Columns("strCompanyName").HeaderText = "Customer Company"
            dgvCaseWork.Columns("strCompanyName").DisplayIndex = 1
            dgvCaseWork.Columns("datCaseOpened").HeaderText = "Date Case Opened"
            dgvCaseWork.Columns("datCaseOpened").DisplayIndex = 2
            dgvCaseWork.Columns("datCaseOpened").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseWork.Columns("strCaseSummary").HeaderText = "Case Summary"
            dgvCaseWork.Columns("strCaseSummary").DisplayIndex = 3
            dgvCaseWork.Columns("numCaseID").HeaderText = "Case #"
            dgvCaseWork.Columns("numCaseID").DisplayIndex = 4
            dgvCaseWork.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvCaseWork.Columns("StaffResponsible").DisplayIndex = 5
            dgvCaseWork.SanelyResizeColumns

            txtCount.Text = dgvCaseWork.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbExistingCases_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbExistingCases.LinkClicked
        Try
            Dim p As SqlParameter() = {
                New SqlParameter("@sdate", DTPReportStartDate.Value),
                New SqlParameter("@edate", DTPReportEndDate.Value)
            }

            Dim query As String = "select distinct(SBEAPCaseLog.numCaseID), " &
            "SBEAPClients.ClientID, strCompanyName, " &
            "strCaseSummary, " &
            "Case " &
            "when datActionOccured between @sdate and @edate then 'Action Occured' " &
            "when datCaseClosed between @sdate and @edate then 'Case Closed' " &
            "end CountReason, " &
            "datCaseOpened as datCaseOpened, " &
            "concat(strLastName, ', ' ,strFirstName) as StaffResponsible,  " &
            "case " &
            "when strComplaintBased = 'True' then 'True' " &
            "else 'False' " &
            "end ComplaintBased " &
            "from SBEAPCaseLog inner join SBEAPActionLog " &
            "on SBEAPCaseLog.numCaseID = SBEAPActionLog.numCaseID  " &
            "left join SBEAPCaseLogLink " &
            "on SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseId " &
            "left join SBEAPClients " &
            "on SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID " &
            "left join EPDUserProfiles " &
            "on SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID " &
            "where datCaseOpened < @sdate  " &
            "and (datActionOccured between @sdate and @edate  " &
            "or datCaseClosed between @sdate and @edate " &
            "or sbeapActionLog.datCreationDate between @sdate and @edate) "

            dgvCaseWork.DataSource = DB.GetDataTable(query, p)

            dgvCaseWork.RowHeadersVisible = False
            dgvCaseWork.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvCaseWork.AllowUserToResizeColumns = True
            dgvCaseWork.AllowUserToAddRows = False
            dgvCaseWork.AllowUserToDeleteRows = False
            dgvCaseWork.AllowUserToOrderColumns = True
            dgvCaseWork.AllowUserToResizeRows = True

            dgvCaseWork.Columns("ClientID").HeaderText = "Customer ID"
            dgvCaseWork.Columns("ClientID").DisplayIndex = 0
            dgvCaseWork.Columns("strCompanyName").HeaderText = "Customer Company"
            dgvCaseWork.Columns("strCompanyName").DisplayIndex = 1
            dgvCaseWork.Columns("CountReason").HeaderText = "Count Reason"
            dgvCaseWork.Columns("CountReason").DisplayIndex = 2
            dgvCaseWork.Columns("datCaseOpened").HeaderText = "Date Case Opened"
            dgvCaseWork.Columns("datCaseOpened").DisplayIndex = 3
            dgvCaseWork.Columns("datCaseOpened").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseWork.Columns("strCaseSummary").HeaderText = "Case Summary"
            dgvCaseWork.Columns("strCaseSummary").DisplayIndex = 4
            dgvCaseWork.Columns("numCaseID").HeaderText = "Case #"
            dgvCaseWork.Columns("numCaseID").DisplayIndex = 5
            dgvCaseWork.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvCaseWork.Columns("StaffResponsible").DisplayIndex = 6
            dgvCaseWork.Columns("ComplaintBased").HeaderText = "Enforcement Based"
            dgvCaseWork.Columns("ComplaintBased").DisplayIndex = 7
            dgvCaseWork.SanelyResizeColumns

            txtCount.Text = dgvCaseWork.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbTotalCases_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbTotalCases.LinkClicked
        Try
            Dim p As SqlParameter() = {
                New SqlParameter("@sdate", DTPReportStartDate.Value),
                New SqlParameter("@edate", DTPReportEndDate.Value)
            }

            Dim query As String = "select SBEAPCaseLog.numCaseID, " &
            "SBEAPClients.ClientID, strCompanyName, " &
            "strCaseSummary, " &
            "datCaseOpened as datCaseOpened, " &
            "'Case Opened' as CountReason,  " &
            "concat(strLastName, ', ' ,strFirstName) as StaffResponsible  " &
            "from SBEAPCaseLog " &
            "left join SBEAPCaseLogLink " &
            "on SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseID " &
            "left join SBEAPClients " &
            "on  SBEAPCaseLogLink.ClientID = SbeapClients.CLientID " &
            "left join EPDUserProfiles " &
            "on SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID " &
            "where datCaseOpened between @sdate and @edate " &
            "union " &
            "select distinct(SBEAPCaseLog.numCaseID), " &
            "SBEAPClients.ClientID, strCompanyName, " &
            "strCaseSummary, " &
            "datCaseOpened , " &
            "Case " &
            "when datActionOccured between @sdate and @edate then 'Action Occured' " &
            "when datCaseClosed between @sdate and @edate then 'Case Closed' " &
            "end CountReason, concat(strLastName, ', ' ,strFirstName) as StaffResponsible   " &
            "from SBEAPCaseLog " &
            "inner join SBEAPActionLog " &
            "on SBEAPCaseLog.numCaseID = SBEAPActionLog.numCaseID " &
            "left join SBEAPCaseLogLink " &
            "on SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseId " &
            "left join SBEAPClients " &
            "on SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID " &
            "left join EPDUserProfiles " &
            "on SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID " &
            "where datCaseOpened < @sdate " &
            "and (datActionOccured between @sdate and @edate " &
            "or datCaseClosed between @sdate and @edate " &
            "or sbeapActionLog.datCreationDate between @sdate and @edate) "

            dgvCaseWork.DataSource = DB.GetDataTable(query, p)

            dgvCaseWork.RowHeadersVisible = False
            dgvCaseWork.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvCaseWork.AllowUserToResizeColumns = True
            dgvCaseWork.AllowUserToAddRows = False
            dgvCaseWork.AllowUserToDeleteRows = False
            dgvCaseWork.AllowUserToOrderColumns = True
            dgvCaseWork.AllowUserToResizeRows = True

            dgvCaseWork.Columns("ClientID").HeaderText = "Customer ID"
            dgvCaseWork.Columns("ClientID").DisplayIndex = 0
            dgvCaseWork.Columns("strCompanyName").HeaderText = "Customer Company"
            dgvCaseWork.Columns("strCompanyName").DisplayIndex = 1
            dgvCaseWork.Columns("CountReason").HeaderText = "Count Reason"
            dgvCaseWork.Columns("CountReason").DisplayIndex = 2
            dgvCaseWork.Columns("datCaseOpened").HeaderText = "Date Case Opened"
            dgvCaseWork.Columns("datCaseOpened").DisplayIndex = 3
            dgvCaseWork.Columns("datCaseOpened").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseWork.Columns("strCaseSummary").HeaderText = "Case Summary"
            dgvCaseWork.Columns("strCaseSummary").DisplayIndex = 4
            dgvCaseWork.Columns("numCaseID").HeaderText = "Case #"
            dgvCaseWork.Columns("numCaseID").DisplayIndex = 5
            dgvCaseWork.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvCaseWork.Columns("StaffResponsible").DisplayIndex = 6
            dgvCaseWork.SanelyResizeColumns

            txtCount.Text = dgvCaseWork.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbCasesClosed_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbCasesClosed.LinkClicked
        Try
            Dim p As SqlParameter() = {
                New SqlParameter("@sdate", DTPReportStartDate.Value),
                New SqlParameter("@edate", DTPReportEndDate.Value)
            }

            Dim query As String = "select " &
            "distinct(SBEAPCaseLog.numCaseID), " &
            "SBEAPClients.clientID, strCompanyName, " &
            "datCaseOpened as datCaseOpened, " &
            "strCaseSummary, " &
            "concat(strLastName, ', ' ,strFirstName) as StaffResponsible, " &
            "datCaseClosed " &
            "from SBEAPCaseLog " &
            "left join SBEAPCaseLogLink " &
            "on SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseID " &
            "left join SBEAPClients " &
            "on SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID " &
            "left join EPDUserProfiles " &
            "on SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID " &
            "where datCasecLosed between @sdate and @edate "

            dgvCaseWork.DataSource = DB.GetDataTable(query, p)

            dgvCaseWork.RowHeadersVisible = False
            dgvCaseWork.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvCaseWork.AllowUserToResizeColumns = True
            dgvCaseWork.AllowUserToAddRows = False
            dgvCaseWork.AllowUserToDeleteRows = False
            dgvCaseWork.AllowUserToOrderColumns = True
            dgvCaseWork.AllowUserToResizeRows = True

            dgvCaseWork.Columns("ClientID").HeaderText = "Customer ID"
            dgvCaseWork.Columns("ClientID").DisplayIndex = 0
            dgvCaseWork.Columns("strCompanyName").HeaderText = "Customer Company"
            dgvCaseWork.Columns("strCompanyName").DisplayIndex = 1
            dgvCaseWork.Columns("datCaseOpened").HeaderText = "Date Case Opened"
            dgvCaseWork.Columns("datCaseOpened").DisplayIndex = 2
            dgvCaseWork.Columns("datCaseOpened").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseWork.Columns("datCaseClosed").HeaderText = "Date Case Closed"
            dgvCaseWork.Columns("datCaseClosed").DisplayIndex = 3
            dgvCaseWork.Columns("datCaseClosed").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseWork.Columns("strCaseSummary").HeaderText = "Case Summary"
            dgvCaseWork.Columns("strCaseSummary").DisplayIndex = 4
            dgvCaseWork.Columns("numCaseID").HeaderText = "Case #"
            dgvCaseWork.Columns("numCaseID").DisplayIndex = 5
            dgvCaseWork.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvCaseWork.Columns("StaffResponsible").DisplayIndex = 6
            dgvCaseWork.SanelyResizeColumns

            txtCount.Text = dgvCaseWork.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbFrontDestCalls_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFrontDestCalls.LinkClicked
        Try
            Dim p As SqlParameter() = {
                New SqlParameter("@sdate", DTPReportStartDate.Value),
                New SqlParameter("@edate", DTPReportEndDate.Value)
            }

            Dim query As String = "select " &
           "distinct(SBEAPActionLog.numActionID), " &
           "sbeapCaseLog.numCaseID, " &
           "SBEAPClients.ClientID, " &
           "strCompanyName, strWorkDescription, " &
           "datActionOccured as datActionOccured, " &
           "concat(strLastName, ', ' ,strFirstName) as StaffResponsible     " &
           "from SBEAPActionLog " &
           "inner join LookUpSBEAPCaseWork " &
           "on SBEAPActionLog.numActionType = LookUpSBEAPCaseWork.numActionType  " &
           "inner join SBEAPCaseLog " &
           "on  SBEAPActionlog.numCaseID = SBEAPCaseLog.numCaseID   " &
           "left join SBEAPCaseLogLink " &
           "on SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseID  " &
           "left join SBEAPClients " &
           "on SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID  " &
           "left join EPDUserProfiles  " &
           "on SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID  " &
           "inner join SBEAPPhoneLog " &
           "on SBEAPActionLog.numActionID = SBEAPPhoneLog.numActionID " &
           "where datActionOccured between @sdate and @edate " &
           "and SBEAPActionLog.numActionType = '6' " &
           "and strFrontDeskCall is not Null " &
           "and strFrontDeskCall = 'True' "

            dgvCaseWork.DataSource = DB.GetDataTable(query, p)

            dgvCaseWork.RowHeadersVisible = False
            dgvCaseWork.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvCaseWork.AllowUserToResizeColumns = True
            dgvCaseWork.AllowUserToAddRows = False
            dgvCaseWork.AllowUserToDeleteRows = False
            dgvCaseWork.AllowUserToOrderColumns = True
            dgvCaseWork.AllowUserToResizeRows = True

            dgvCaseWork.Columns("numCaseID").HeaderText = "Case #"
            dgvCaseWork.Columns("numCaseID").DisplayIndex = 0
            dgvCaseWork.Columns("ClientID").HeaderText = "Customer ID"
            dgvCaseWork.Columns("ClientID").DisplayIndex = 1
            dgvCaseWork.Columns("strCompanyName").HeaderText = "Customer Company"
            dgvCaseWork.Columns("strCompanyName").DisplayIndex = 2
            dgvCaseWork.Columns("strWorkDescription").HeaderText = "Action Type"
            dgvCaseWork.Columns("strWorkDescription").DisplayIndex = 3
            dgvCaseWork.Columns("datActionOccured").HeaderText = "Date Action Occured"
            dgvCaseWork.Columns("datActionOccured").DisplayIndex = 4
            dgvCaseWork.Columns("datActionOccured").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseWork.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvCaseWork.Columns("StaffResponsible").DisplayIndex = 5
            dgvCaseWork.Columns("numActionID").HeaderText = "Action ID"
            dgvCaseWork.Columns("numActionID").DisplayIndex = 6
            dgvCaseWork.Columns("numActionID").Visible = False
            dgvCaseWork.SanelyResizeColumns

            txtCount.Text = dgvCaseWork.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbTotalActions_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbTotalActions.LinkClicked
        Try
            Dim p As SqlParameter() = {
                New SqlParameter("@sdate", DTPReportStartDate.Value),
                New SqlParameter("@edate", DTPReportEndDate.Value)
            }

            Dim query As String = "select " &
            "distinct(SBEAPActionLog.numActionID), " &
            "sbeapCaseLog.numCaseID, " &
            "SBEAPClients.ClientID, " &
            "strCompanyName, strWorkDescription, " &
            "datActionOccured as datActionOccured, " &
            "concat(strLastName, ', ' ,strFirstName) as StaffResponsible     " &
            "from SBEAPActionLog " &
            "inner join LookUpSBEAPCaseWork " &
            "on SBEAPActionLog.numActionType = LookUpSBEAPCaseWork.numActionType  " &
            "inner join SBEAPCaseLog " &
            "on SBEAPActionlog.numCaseID = SBEAPCaseLog.numCaseID   " &
            "left join SBEAPCaseLogLink " &
            "on SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseID " &
            "left join SBEAPClients " &
            "on SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID  " &
            "left join EPDUserProfiles  " &
            "on SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID  " &
            "where datActionOccured between @sdate and @edate "

            dgvCaseWork.DataSource = DB.GetDataTable(query, p)

            dgvCaseWork.RowHeadersVisible = False
            dgvCaseWork.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvCaseWork.AllowUserToResizeColumns = True
            dgvCaseWork.AllowUserToAddRows = False
            dgvCaseWork.AllowUserToDeleteRows = False
            dgvCaseWork.AllowUserToOrderColumns = True
            dgvCaseWork.AllowUserToResizeRows = True

            dgvCaseWork.Columns("numCaseID").HeaderText = "Case #"
            dgvCaseWork.Columns("numCaseID").DisplayIndex = 0
            dgvCaseWork.Columns("ClientID").HeaderText = "Customer ID"
            dgvCaseWork.Columns("ClientID").DisplayIndex = 1
            dgvCaseWork.Columns("strCompanyName").HeaderText = "Customer Company"
            dgvCaseWork.Columns("strCompanyName").DisplayIndex = 2
            dgvCaseWork.Columns("strWorkDescription").HeaderText = "Action Type"
            dgvCaseWork.Columns("strWorkDescription").DisplayIndex = 3
            dgvCaseWork.Columns("datActionOccured").HeaderText = "Date Action Occured"
            dgvCaseWork.Columns("datActionOccured").DisplayIndex = 4
            dgvCaseWork.Columns("datActionOccured").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseWork.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvCaseWork.Columns("StaffResponsible").DisplayIndex = 5
            dgvCaseWork.Columns("numActionID").HeaderText = "Action ID"
            dgvCaseWork.Columns("numActionID").DisplayIndex = 6
            dgvCaseWork.Columns("numActionID").Visible = False

            txtCount.Text = dgvCaseWork.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbViewOther_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbActionTypes.LinkClicked
        Try
            Dim p As SqlParameter() = {
                New SqlParameter("@sdate", DTPReportStartDate.Value),
                New SqlParameter("@edate", DTPReportEndDate.Value),
                New SqlParameter("@actiontype", cboActionTypes.SelectedValue)
            }

            Dim query As String

            If txtActionTypeCount.Text <> "" And txtActionTypeCount.Text <> "0" And txtActionTypeCount.Text <> " " Then
                query = "select " &
                "distinct(SBEAPActionLog.numActionID), " &
                "sbeapCaseLog.numCaseID, " &
                "SBEAPClients.ClientID, " &
                "strCompanyName, strWorkDescription, " &
                "datActionOccured as datActionOccured, " &
                "concat(strLastName, ', ' ,strFirstName) as StaffResponsible, " &
                "strCaseSummary " &
                "from SBEAPActionLog " &
                "inner join LookUpSBEAPCaseWork " &
                "on SBEAPActionLog.numActionType = LookUpSBEAPCaseWork.numActionType  " &
                "inner join SBEAPCaseLog " &
                "on SBEAPActionlog.numCaseID = SBEAPCaseLog.numCaseID   " &
                "left join SBEAPCaseLogLink " &
                "on SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseID " &
                "left join SBEAPClients " &
                "on SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID   " &
                "left join EPDUserProfiles  " &
                "on SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID  " &
                "where datActionOccured between @sdate and @edate " &
                "and SBEAPActionLog.numActionType = @actiontype "
            Else
                query = "select " &
                "distinct(SBEAPActionLog.numActionID), " &
                "sbeapCaseLog.numCaseID, " &
                "SBEAPClients.ClientID, " &
                "strCompanyName, strWorkDescription, " &
                "datActionOccured, " &
                "concat(strLastName, ', ' ,strFirstName) as StaffResponsible, " &
                "strCaseSummary " &
                "from SBEAPActionLog " &
                "inner join LookUpSBEAPCaseWork " &
                "on SBEAPActionLog.numActionType = LookUpSBEAPCaseWork.numActionType  " &
                "inner join SBEAPCaseLog " &
                "on SBEAPActionlog.numCaseID = SBEAPCaseLog.numCaseID   " &
                "left join SBEAPCaseLogLink " &
                "on SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseID " &
                "left join SBEAPClients " &
                "on SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID  " &
                "left join EPDUserProfiles  " &
                "on SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID " &
                "where datActionOccured between @sdate and @edate " &
                "and SBEAPActionLog.numActionType = '0' "
            End If

            dgvCaseWork.DataSource = DB.GetDataTable(query, p)

            dgvCaseWork.RowHeadersVisible = False
            dgvCaseWork.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvCaseWork.AllowUserToResizeColumns = True
            dgvCaseWork.AllowUserToAddRows = False
            dgvCaseWork.AllowUserToDeleteRows = False
            dgvCaseWork.AllowUserToOrderColumns = True
            dgvCaseWork.AllowUserToResizeRows = True

            dgvCaseWork.Columns("numCaseID").HeaderText = "Case #"
            dgvCaseWork.Columns("numCaseID").DisplayIndex = 0
            dgvCaseWork.Columns("ClientID").HeaderText = "Customer ID"
            dgvCaseWork.Columns("ClientID").DisplayIndex = 1
            dgvCaseWork.Columns("strCompanyName").HeaderText = "Customer Company"
            dgvCaseWork.Columns("strCompanyName").DisplayIndex = 2
            dgvCaseWork.Columns("strWorkDescription").HeaderText = "Action Type"
            dgvCaseWork.Columns("strWorkDescription").DisplayIndex = 3
            dgvCaseWork.Columns("datActionOccured").HeaderText = "Date Action Occured"
            dgvCaseWork.Columns("datActionOccured").DisplayIndex = 4
            dgvCaseWork.Columns("datActionOccured").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseWork.Columns("StaffResponsible").HeaderText = "Staff Responsible"
            dgvCaseWork.Columns("StaffResponsible").DisplayIndex = 5
            dgvCaseWork.Columns("numActionID").HeaderText = "Action ID"
            dgvCaseWork.Columns("numActionID").DisplayIndex = 6
            dgvCaseWork.Columns("numActionID").Visible = False
            dgvCaseWork.Columns("strCaseSummary").HeaderText = "Case Summary"
            dgvCaseWork.Columns("strCaseSummary").DisplayIndex = 7
            dgvCaseWork.Columns("strCaseSummary").Width = 250

            txtCount.Text = dgvCaseWork.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvCaseWork_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvCaseWork.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvCaseWork.HitTest(e.X, e.Y)
            If dgvCaseWork.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvCaseWork.Columns(0).HeaderText = "Case #" Then
                    If IsDBNull(dgvCaseWork(0, hti.RowIndex).Value) Then
                        txtCaseID.Text = ""
                    Else
                        txtCaseID.Text = dgvCaseWork(0, hti.RowIndex).Value
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        dgvCaseWork.ExportToExcel(Me)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim query As String = "Select " &
        "numActionID, " &
        "datCreationDate " &
        "from SBEAPActionLog " &
        "where datActionoccured is null " &
        " and datCreationDate is not null "

        Dim dr As DataRow = DB.GetDataRow(query)

        If dr IsNot Nothing Then
            query = "Update SBEAPActionLog set " &
            "datActionOccured = @actiondate " &
            "where numActionID = @actionid "

            Dim p As SqlParameter() = {
                New SqlParameter("@actiondate", dr.Item("datCreationDate")),
                New SqlParameter("@actionid", dr.Item("numActionID"))
            }

            DB.RunCommand(query, p)
        End If
    End Sub

    Private Sub cboActionTypes_TextChanged(sender As Object, e As EventArgs) Handles cboActionTypes.TextChanged
        Try
            If txtTotalActionCount.Text <> "" And txtTotalActionCount.Text <> "0" Then
                If cboActionTypes.SelectedValue <> " " Then
                    txtActionTypeCount.Text = "1"
                    Dim drDSRow As DataRow
                    Dim temp As String = 0
                    For Each drDSRow In dtAction.Select("numActionType = " & cboActionTypes.SelectedValue)
                        If Not IsDBNull(drDSRow("Count")) Then
                            temp = drDSRow("Count")
                        Else
                            temp = "0"
                        End If
                    Next
                    txtActionTypeCount.Text = temp
                Else
                    txtActionTypeCount.Text = " "
                End If
            Else
                txtActionTypeCount.Text = " "
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class