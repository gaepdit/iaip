Imports Oracle.DataAccess.Client
Imports System.Collections
Imports System.Reflection
'Imports Microsoft.Reporting.WinForms
'Imports Microsoft.ReportingServices.ReportRendering

Public Class SBEAPReports
    Dim SQL, SQL2 As String
    Dim dsView As DataSet
    Dim daView As OracleDataAdapter
    Dim dsCombo As DataSet
    Dim daCombo As OracleDataAdapter
    Dim dsAction As DataSet
    Dim daAction As OracleDataAdapter

    Private Sub SBEAPReports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            DTPReportStartDate.Text = Format(Date.Today.AddMonths(-3), "dd-MMM-yyyy")
            DTPReportEndDate.Text = OracleDate

            LoadComboBoxes()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadComboBoxes()
        Try
            Dim dtActionTypes As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            dsCombo = New DataSet

            SQL = "select " & _
            "numActionType, strWorkDescription " & _
            "from AIRBranch.LookUpSBEAPCaseWOrk " & _
            "order by strWorkDescription "

            daCombo = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daCombo.Fill(dsCombo, "Combo")

            dtActionTypes.Columns.Add("strWorkDescription", GetType(System.String))
            dtActionTypes.Columns.Add("numActionType", GetType(System.String))

            drNewRow = dtActionTypes.NewRow()
            drNewRow("strWorkDescription") = ""
            drNewRow("numActionType") = " "
            dtActionTypes.Rows.Add(drNewRow)

            For Each drDSRow In dsCombo.Tables("Combo").Rows()
                drNewRow = dtActionTypes.NewRow()
                drNewRow("strWorkDescription") = drDSRow("strWorkDescription")
                drNewRow("numActionType") = drDSRow("numActionType")
                dtActionTypes.Rows.Add(drNewRow)
            Next
            With cboActionTypes
                .DataSource = dtActionTypes
                .DisplayMember = "strWorkDescription"
                .ValueMember = "numActionType"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "Subs and Functions"
    Sub ClearReport()
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
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub RunReport()
        Try
            ClearReport()

            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.SBEAPClients " & _
            "where datCompanyCreated between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtNewClientCount.Text = dr.Item("Count")
            End While
            dr.Close()

            SQL = "select count(*) as Count " & _
            "from (select " & _
            "distinct(AIRBRANCH.SBEAPActionLog.numCaseID) as CaseID " & _
            "from AIRBRANCH.SBEAPActionLog, AIRBRANCH.SBEAPCaseLog, " & _
            "AIRBRANCH.SBEAPCaseLogLink, AIRBRANCH.SBEAPClients  " & _
            "where AIRBRANCH.SBEAPActionLog.numCaseID = AIRBRANCH.SBEAPCaseLog.numCaseID " & _
            "and AIRBRANCH.SBEAPActionLog.numCaseID = AIRBRANCH.SBEAPCaseLogLink.numCaseID " & _
            "and AIRBRANCH.SBEAPCaseLogLink.ClientID = AIRBRANCH.SBEAPClients.ClientID " & _
            "and  (datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
            "or datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
            "or datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "') ) "


            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtClientAssistCount.Text = dr.Item("Count")
            End While
            dr.Close()

            SQL = "select count(*) as Count " & _
            "from AIRBranch.SBEAPCaseLog " & _
            "where datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtNewCaseCount.Text = dr.Item("Count")
            End While
            dr.Close()

            SQL = "select count(*) as Count " & _
            "from (select distinct(AIRBRANCH.SBEAPCaseLog.numCaseID) " & _
            "from AIRBRANCH.SBEAPCaseLog, AIRBRANCH.SBEAPActionLog  " & _
            "where AIRBRANCH.SBEAPCaseLog.numCaseID = AIRBRANCH.SBEAPActionLog.numCaseID " & _
            "and datCaseOpened < '" & DTPReportStartDate.Text & "' " & _
            "and (datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
            "or datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "'  " & _
            "or AIRBRANCH.sbeapActionLog.datCreationDate between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "')) "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtExistingCaseCount.Text = dr.Item("Count")
            End While
            dr.Close()

            If txtNewCaseCount.Text = "" Then
                txtNewCaseCount.Text = "0"
            End If
            If txtExistingCaseCount.Text = "" Then
                txtExistingCaseCount.Text = "0"
            End If

            txtTotalCaseCount.Text = CInt(txtNewCaseCount.Text) + CInt(txtExistingCaseCount.Text)

            SQL = "select Count(*) as Count " & _
            "from AIRBranch.SBEAPCaseLog " & _
            "where datCasecLosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtCaseClosedCount.Text = dr.Item("Count")
            End While
            dr.Close()

            SQL = "select count(*) as Count " & _
            "from AIRBRANCH.SBEAPActionLog " & _
            "where datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtTotalActionCount.Text = dr.Item("Count")
            End While
            dr.Close()

            SQL = "Select count(*) as Count " & _
            "from AIRBRANCH.SBEAPActionLog, " & _
            "AIRBRANCH.SBEAPPhoneLog " & _
            "where AIRBRANCH.SBEAPActionLog.numActionID = AIRBRANCH.SBEAPPhoneLog.numActionID " & _
            "and datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
            "and strFrontDeskCall is not Null " & _
            "and strFrontDeskCall = 'True' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtFrontDeskCallCount.Text = dr.Item("Count")
            End While
            dr.Close()

            dsAction = New DataSet

            SQL = "select Count(*) as Count, numActionType " & _
            "from AIRBranch.SBEAPActionLog " & _
            "where datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
            "group by numActionType "

            daAction = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daAction.Fill(dsAction, "ActionCount")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
    Private Sub btnViewCase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewCase.Click
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
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRunSBEAPReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunSBEAPReport.Click
        Try
            RunReport()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewNewClient_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewNewClient.LinkClicked
        Try
            SQL = "select " & _
            "AIRBRANCH.SBEAPClients.ClientID, strCompanyName, " & _
            "to_date(datCompanyCreated, 'dd-Mon-RRRR') as datCompanyCreated, STRCLIENTDESCRIPTION " & _
            "from AIRBRANCH.SBEAPClients, AIRBRANCH.SBEAPCLientData " & _
            "where AIRBRANCH.SBEAPClients.ClientID = AIRBRANCH.SBEAPClientData.ClientID " & _
            "and datCompanyCreated between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            dsView = New DataSet
            daView = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daView.Fill(dsView, "ViewCount")
            dgvCaseWork.DataSource = dsView
            dgvCaseWork.DataMember = "ViewCount"

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


            txtCount.Text = dgvCaseWork.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbClientsAssisted_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbClientsAssisted.LinkClicked
        Try
            SQL = "select " & _
            "distinct(AIRBRANCH.SBEAPActionLog.numCaseID) as CaseID, " & _
            "AIRBRANCH.SBEAPClients.ClientID, strCompanyName, " & _
            "case " & _
            "when datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then 'Case Opened' " & _
            "when datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "'  then 'Case Closed' " & _
            "when datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then 'Action Occured' " & _
            "end CountReason, " & _
            "case " & _
            "when datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then to_date(datCaseOpened, 'dd-Mon-RRRR') " & _
            "when datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then to_date(datCaseClosed, 'dd-Mon-RRRR') " & _
            "else null   " & _
            "end CountDate, strCaseSummary " & _
            "from AIRBRANCH.SBEAPActionLog, AIRBRANCH.SBEAPCaseLog, " & _
            "AIRBRANCH.SBEAPCaseLogLink, AIRBRANCH.SBEAPClients  " & _
            "where AIRBRANCH.SBEAPActionLog.numCaseID = AIRBRANCH.SBEAPCaseLog.numCaseID " & _
            "and AIRBRANCH.SBEAPActionLog.numCaseID = AIRBRANCH.SBEAPCaseLogLink.numCaseID " & _
            "and AIRBRANCH.SBEAPCaseLogLink.ClientID = AIRBRANCH.SBEAPClients.ClientID " & _
            "and  (datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
            "or datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
            "or datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "') "

            dsView = New DataSet
            daView = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daView.Fill(dsView, "ViewCount")
            dgvCaseWork.DataSource = dsView
            dgvCaseWork.DataMember = "ViewCount"

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

            txtCount.Text = dgvCaseWork.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewEmails_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try
            SQL = "select " & _
           "numCaseID, " & _
           "AIRBRANCH.SBEAPClients.ClientID, " & _
           "strCompanyName, " & _
           "to_date(datCaseOpened, 'dd-Mon-RRRR') as datCaseOpened, " & _
           "strWorkDescription " & _
           "from AIRBRANCH.SBEAPCaseLog, AIRBRANCH.LookUpSBEAPCaseWork, " & _
           "AIRBRANCH.SBEAPClients " & _
           "where AIRBRANCH.SBEAPCaseLog.numActionType = AIRBRANCH.LookUpSBEAPCaseWork.numActionType " & _
           "and AIRBRANCH.SBEAPCaseLog.ClientID = AIRBRANCH.SBEAPClients.ClientID (+) " & _
           "and datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
           "and numActionType = '3' "

            dsView = New DataSet
            daView = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daView.Fill(dsView, "ViewCount")
            dgvCaseWork.DataSource = dsView
            dgvCaseWork.DataMember = "ViewCount"

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
            dgvCaseWork.Columns("datCaseOpened").HeaderText = "Date Case Opened"
            dgvCaseWork.Columns("datCaseOpened").DisplayIndex = 3
            dgvCaseWork.Columns("datCaseOpened").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvCaseWork.Columns("strWorkDescription").HeaderText = "Assist Type"
            dgvCaseWork.Columns("strWorkDescription").DisplayIndex = 4

            txtCount.Text = dgvCaseWork.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbNewCases_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbNewCases.LinkClicked
        Try
            SQL = "select AIRBRANCH.SBEAPCaseLog.numCaseID, " & _
            "AIRBRANCH.SBEAPClients.ClientID, strCompanyName, " & _
            "strCaseSummary, " & _
            "to_date(datCaseOpened, 'dd-Mon-RRRR') as datCaseOpened, " & _
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible " & _
            "from AIRBRANCH.SBEAPCaseLog, AIRBRANCH.SBEAPClients, " & _
            "AIRBRANCH.SBEAPCaseLogLink, AIRBRANCH.EPDUserProfiles " & _
            "where AIRBRANCH.SBEAPCaseLog.numCaseID  = AIRBRANCH.SBEAPCaseLogLink.numCaseID (+) " & _
            "and AIRBRANCH.SBEAPCaseLogLink.ClientID = AIRBRANCH.SbeapClients.CLientID  (+) " & _
            "and AIRBRANCH.SBEAPCaseLog.numStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID (+) " & _
            "and datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            dsView = New DataSet
            daView = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daView.Fill(dsView, "ViewCount")
            dgvCaseWork.DataSource = dsView
            dgvCaseWork.DataMember = "ViewCount"

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

            txtCount.Text = dgvCaseWork.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbExistingCases_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbExistingCases.LinkClicked
        Try
            SQL = "select distinct(AIRBRANCH.SBEAPCaseLog.numCaseID), " & _
            "AIRBRANCH.SBEAPClients.ClientID, strCompanyName, " & _
            "strCaseSummary, " & _
            "Case " & _
            "when datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then 'Action Occured' " & _
            "when datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then 'Case Closed' " & _
            "end CountReason, " & _
            "to_date(datCaseOpened, 'dd-Mon-RRRR') as datCaseOpened, " & _
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible,  " & _
            "case " & _
            "when strComplaintBased = 'True' then 'True' " & _
            "else 'False' " & _
            "end ComplaintBased " & _
            "from AIRBRANCH.SBEAPCaseLog, AIRBRANCH.SBEAPActionLog, " & _
            "AIRBRANCH.SBEAPCaseLogLink, AIRBRANCH.SBEAPClients, " & _
            "AIRBRANCH.EPDUserProfiles " & _
            "where AIRBRANCH.SBEAPCaseLog.numCaseID = AIRBRANCH.SBEAPActionLog.numCaseID  " & _
            "and AIRBRANCH.SBEAPCaseLog.numCaseID = AIRBRANCH.SBEAPCaseLogLink.numCaseId (+) " & _
            "and AIRBRANCH.SBEAPCaseLogLink.ClientID = AIRBRANCH.SBEAPClients.ClientID  (+) " & _
            "and AIRBRANCH.SBEAPCaseLog.numStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID (+) " & _
            "and datCaseOpened < '" & DTPReportStartDate.Text & "'  " & _
            "and (datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "'  " & _
            "or datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
            "or AIRBRANCH.sbeapActionLog.datCreationDate between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "') "

            dsView = New DataSet
            daView = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daView.Fill(dsView, "ViewCount")
            dgvCaseWork.DataSource = dsView
            dgvCaseWork.DataMember = "ViewCount"

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

            txtCount.Text = dgvCaseWork.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbTotalCases_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbTotalCases.LinkClicked
        Try
            SQL = "select AIRBRANCH.SBEAPCaseLog.numCaseID, " & _
            "AIRBRANCH.SBEAPClients.ClientID, strCompanyName, " & _
            "strCaseSummary, " & _
            "to_date(datCaseOpened, 'dd-Mon-RRRR') as datCaseOpened, " & _
            "'Case Opened' as CountReason,  " & _
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible  " & _
            "from AIRBRANCH.SBEAPCaseLog, AIRBRANCH.SBEAPClients, " & _
            "AIRBRANCH.SBEAPCaseLogLink, " & _
            "AIRBRANCH.EPDUserProfiles " & _
            "where AIRBRANCH.SBEAPCaseLog.numCaseID = AIRBRANCH.SBEAPCaseLogLink.numCaseID (+) " & _
            "and AIRBRANCH.SBEAPCaseLogLink.ClientID = AIRBRANCH.SbeapClients.CLientID (+) " & _
              "and AIRBRANCH.SBEAPCaseLog.numStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID (+) " & _
            "and datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
            "union " & _
            "select distinct(AIRBRANCH.SBEAPCaseLog.numCaseID), " & _
            "AIRBRANCH.SBEAPClients.ClientID, strCompanyName, " & _
            "strCaseSummary, " & _
            "datCaseOpened , " & _
            "Case " & _
            "when datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then 'Action Occured' " & _
            "when datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then 'Case Closed' " & _
            "end CountReason, (strLastName|| ', ' ||strFirstName) as StaffResponsible   " & _
            "from AIRBRANCH.SBEAPCaseLog, AIRBRANCH.SBEAPActionLog, " & _
            "AIRBRANCH.SBEAPCaseLogLink, AIRBRANCH.SBEAPClients, " & _
            "AIRBRANCH.EPDUserProfiles " & _
            "where AIRBRANCH.SBEAPCaseLog.numCaseID = AIRBRANCH.SBEAPActionLog.numCaseID " & _
            "and AIRBRANCH.SBEAPCaseLog.numCaseID = AIRBRANCH.SBEAPCaseLogLink.numCaseId (+) " & _
            "and AIRBRANCH.SBEAPCaseLogLink.ClientID = AIRBRANCH.SBEAPClients.ClientID (+) " & _
            "and AIRBRANCH.SBEAPCaseLog.numStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID (+) " & _
            "and datCaseOpened < '" & DTPReportStartDate.Text & "' " & _
            "and (datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
            "or datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
            "or AIRBRANCH.sbeapActionLog.datCreationDate between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "') "

            dsView = New DataSet
            daView = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daView.Fill(dsView, "ViewCount")
            dgvCaseWork.DataSource = dsView
            dgvCaseWork.DataMember = "ViewCount"

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


            txtCount.Text = dgvCaseWork.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbCasesClosed_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbCasesClosed.LinkClicked
        Try
            SQL = "select " & _
            "distinct(AIRBRANCH.SBEAPCaseLog.numCaseID), " & _
            "AIRBRANCH.SBEAPClients.clientID, strCompanyName, " & _
            "to_date(datCaseOpened, 'dd-Mon-RRRR') as datCaseOpened, " & _
            "strCaseSummary, " & _
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible, " & _
            "datCaseClosed " & _
            "from AIRBRANCH.SBEAPCaseLog, AIRBRANCH.SBEAPCaseLogLink, " & _
            "AIRBRANCH.SBEAPClients, " & _
            "AIRBRANCH.EPDUserProfiles " & _
            "where AIRBRANCH.SBEAPCaseLog.numCaseID = AIRBRANCH.SBEAPCaseLogLink.numCaseID (+) " & _
            "and AIRBRANCH.SBEAPCaseLogLink.ClientID = AIRBRANCH.SBEAPClients.ClientID (+) " & _
            "and AIRBRANCH.SBEAPCaseLog.numStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID (+) " & _
            "and datCasecLosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            dsView = New DataSet
            daView = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daView.Fill(dsView, "ViewCount")
            dgvCaseWork.DataSource = dsView
            dgvCaseWork.DataMember = "ViewCount"

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

            txtCount.Text = dgvCaseWork.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbFrontDestCalls_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFrontDestCalls.LinkClicked
        Try
            SQL = "select " & _
           "distinct(AIRBRANCH.SBEAPActionLog.numActionID), " & _
           "AIRBRANCH.sbeapCaseLog.numCaseID, " & _
           "AIRBRANCH.SBEAPClients.ClientID, " & _
           "strCompanyName, strWorkDescription, " & _
           "to_date(datActionOccured, 'dd-Mon-RRRR') as datActionOccured, " & _
           "(strLastName|| ', ' ||strFirstName) as StaffResponsible     " & _
           "from AIRBRANCH.SBEAPActionLog, AIRBRANCH.LookUpSBEAPCaseWork, " & _
           "AIRBRANCH.SBEAPCaseLog, AIRBRANCH.SBEAPCaseLogLink, " & _
           "AIRBRANCH.SBEAPClients, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.SBEAPPhoneLog " & _
           "where AIRBRANCH.SBEAPActionLog.numActionType = AIRBRANCH.LookUpSBEAPCaseWork.numActionType  " & _
           "and AIRBRANCH.SBEAPActionlog.numCaseID = AIRBRANCH.SBEAPCaseLog.numCaseID   " & _
           "and AIRBRANCH.SBEAPCaseLog.numCaseID = AIRBRANCH.SBEAPCaseLogLink.numCaseID (+) " & _
           "and AIRBRANCH.SBEAPCaseLogLink.ClientID = AIRBRANCH.SBEAPClients.ClientID  (+) " & _
           "and AIRBRANCH.SBEAPCaseLog.numStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID (+) " & _
           "and AIRBRANCH.SBEAPActionLog.numActionID = AIRBRANCH.SBEAPPhoneLog.numActionID " & _
           "and datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
           "and AIRBRANCH.SBEAPActionLog.numActionType = '6' " & _
           "and strFrontDeskCall is not Null " & _
           "and strFrontDeskCall = 'True' "

            dsView = New DataSet
            daView = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daView.Fill(dsView, "ViewCount")
            dgvCaseWork.DataSource = dsView
            dgvCaseWork.DataMember = "ViewCount"

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
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbTotalActions_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbTotalActions.LinkClicked
        Try
            SQL = "select " & _
            "distinct(AIRBRANCH.SBEAPActionLog.numActionID), " & _
            "AIRBRANCH.sbeapCaseLog.numCaseID, " & _
            "AIRBRANCH.SBEAPClients.ClientID, " & _
            "strCompanyName, strWorkDescription, " & _
            "to_date(datActionOccured, 'dd-Mon-RRRR') as datActionOccured, " & _
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible     " & _
            "from AIRBRANCH.SBEAPActionLog, AIRBRANCH.LookUpSBEAPCaseWork, " & _
            "AIRBRANCH.SBEAPCaseLog, AIRBRANCH.SBEAPCaseLogLink, " & _
            "AIRBRANCH.SBEAPClients, AIRBRANCH.EPDUserProfiles  " & _
            "where AIRBRANCH.SBEAPActionLog.numActionType = AIRBRANCH.LookUpSBEAPCaseWork.numActionType  " & _
            "and AIRBRANCH.SBEAPActionlog.numCaseID = AIRBRANCH.SBEAPCaseLog.numCaseID   " & _
            "and AIRBRANCH.SBEAPCaseLog.numCaseID = AIRBRANCH.SBEAPCaseLogLink.numCaseID (+) " & _
            "and AIRBRANCH.SBEAPCaseLogLink.ClientID = AIRBRANCH.SBEAPClients.ClientID  (+) " & _
            "and AIRBRANCH.SBEAPCaseLog.numStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID (+) " & _
            "and datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            dsView = New DataSet
            daView = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daView.Fill(dsView, "ViewCount")
            dgvCaseWork.DataSource = dsView
            dgvCaseWork.DataMember = "ViewCount"

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
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewOther_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbActionTypes.LinkClicked
        Try
            If txtActionTypeCount.Text <> "" And txtActionTypeCount.Text <> "0" And txtActionTypeCount.Text <> " " Then
                SQL = "select " & _
                "distinct(AIRBRANCH.SBEAPActionLog.numActionID), " & _
                "AIRBRANCH.sbeapCaseLog.numCaseID, " & _
                "AIRBRANCH.SBEAPClients.ClientID, " & _
                "strCompanyName, strWorkDescription, " & _
                "to_date(datActionOccured, 'dd-Mon-RRRR') as datActionOccured, " & _
                "(strLastName|| ', ' ||strFirstName) as StaffResponsible, " & _
                "strCaseSummary " & _
                "from AIRBRANCH.SBEAPActionLog, AIRBRANCH.LookUpSBEAPCaseWork, " & _
                "AIRBRANCH.SBEAPCaseLog, AIRBRANCH.SBEAPCaseLogLink, " & _
                "AIRBRANCH.SBEAPClients, AIRBRANCH.EPDUserProfiles  " & _
                "where AIRBRANCH.SBEAPActionLog.numActionType = AIRBRANCH.LookUpSBEAPCaseWork.numActionType  " & _
                "and AIRBRANCH.SBEAPActionlog.numCaseID = AIRBRANCH.SBEAPCaseLog.numCaseID   " & _
                "and AIRBRANCH.SBEAPCaseLog.numCaseID = AIRBRANCH.SBEAPCaseLogLink.numCaseID (+) " & _
                "and AIRBRANCH.SBEAPCaseLogLink.ClientID = AIRBRANCH.SBEAPClients.ClientID  (+) " & _
                "and AIRBRANCH.SBEAPCaseLog.numStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID (+) " & _
                "and datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
                "and AIRBRANCH.SBEAPActionLog.numActionType = '" & cboActionTypes.SelectedValue & "' "
            Else
                SQL = "select " & _
              "distinct(AIRBRANCH.SBEAPActionLog.numActionID), " & _
              "AIRBRANCH.sbeapCaseLog.numCaseID, " & _
              "AIRBRANCH.SBEAPClients.ClientID, " & _
              "strCompanyName, strWorkDescription, " & _
              "to_date(datActionOccured, 'dd-Mon-RRRR') as datActionOccured, " & _
              "(strLastName|| ', ' ||strFirstName) as StaffResponsible, " & _
              "strCaseSummary " & _
              "from AIRBRANCH.SBEAPActionLog, AIRBRANCH.LookUpSBEAPCaseWork, " & _
              "AIRBRANCH.SBEAPCaseLog, AIRBRANCH.SBEAPCaseLogLink, " & _
              "AIRBRANCH.SBEAPClients, AIRBRANCH.EPDUserProfiles  " & _
              "where AIRBRANCH.SBEAPActionLog.numActionType = AIRBRANCH.LookUpSBEAPCaseWork.numActionType  " & _
              "and AIRBRANCH.SBEAPActionlog.numCaseID = AIRBRANCH.SBEAPCaseLog.numCaseID   " & _
              "and AIRBRANCH.SBEAPCaseLog.numCaseID = AIRBRANCH.SBEAPCaseLogLink.numCaseID (+) " & _
              "and AIRBRANCH.SBEAPCaseLogLink.ClientID = AIRBRANCH.SBEAPClients.ClientID  (+) " & _
              "and AIRBRANCH.SBEAPCaseLog.numStaffResponsible = AIRBRANCH.EPDUserProfiles.numUserID (+) " & _
              "and datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " & _
              "and AIRBRANCH.SBEAPActionLog.numActionType = '0' "
            End If

            dsView = New DataSet
            daView = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daView.Fill(dsView, "ViewCount")

            dgvCaseWork.DataSource = dsView
            dgvCaseWork.DataMember = "ViewCount"

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
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvCaseWork_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvCaseWork.MouseUp
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
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        Try
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If
            If dgvCaseWork.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvCaseWork.ColumnCount - 1
                        .Cells(1, i + 1) = dgvCaseWork.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvCaseWork.ColumnCount - 1
                        For j = 0 To dgvCaseWork.RowCount - 1
                            .Cells(j + 2, i + 1).value = dgvCaseWork.Item(i, j).Value.ToString
                        Next
                    Next

                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim cmd2 As OracleCommand
            Dim dr2 As OracleDataReader

            SQL = "Select " & _
            "numActionID, " & _
            "to_char(datCreationDate, 'dd-Mon-yyyy') as datCreationDate " & _
            "from AIRBranch.SBEAPActionLog " & _
            "where datActionoccured is null "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("datCreationDate")) Then
                Else
                    SQL2 = "Update AIRBranch.SBEAPActionLog set " & _
                    "datActionOccured = '" & dr.Item("datCreationDate") & "' " & _
                    "where numActionID = '" & dr.Item("numActionID") & "' "
                    cmd2 = New OracleCommand(SQL2, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub cboActionTypes_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboActionTypes.TextChanged
        Try
            If txtTotalActionCount.Text <> "" And txtTotalActionCount.Text <> "0" Then
                If cboActionTypes.SelectedValue <> " " Then
                    txtActionTypeCount.Text = "1"
                    Dim dtActionCount As New DataTable
                    Dim drDSRow As DataRow
                    Dim temp As String = 0
                    For Each drDSRow In dsAction.Tables("ActionCount").Select("numActionType = " & cboActionTypes.SelectedValue)
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
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class