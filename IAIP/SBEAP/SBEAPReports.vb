Imports System.Data.SqlClient
Imports System.Collections
Imports System.Reflection
'Imports Microsoft.Reporting.WinForms
'Imports Microsoft.ReportingServices.ReportRendering

Public Class SBEAPReports
    Dim SQL, SQL2 As String
    Dim dsView As DataSet
    Dim daView As SqlDataAdapter
    Dim dsCombo As DataSet
    Dim daCombo As SqlDataAdapter
    Dim dsAction As DataSet
    Dim daAction As SqlDataAdapter

    Private Sub SBEAPReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            DTPReportStartDate.Text = Format(Date.Today.AddMonths(-3), "dd-MMM-yyyy")
            DTPReportEndDate.Text = OracleDate

            LoadComboBoxes()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadComboBoxes()
        Try
            Dim dtActionTypes As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            dsCombo = New DataSet

            SQL = "select " &
            "numActionType, strWorkDescription " &
            "from LookUpSBEAPCaseWOrk " &
            "order by strWorkDescription "

            daCombo = New SqlDataAdapter(SQL, CurrentConnection)

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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub RunReport()
        Try
            ClearReport()

            SQL = "Select count(*) as Count " &
            "from SBEAPClients " &
            "where datCompanyCreated between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtNewClientCount.Text = dr.Item("Count")
            End While
            dr.Close()

            SQL = "select count(*) as Count " &
            "from (select " &
            "distinct(SBEAPActionLog.numCaseID) as CaseID " &
            "from SBEAPActionLog, SBEAPCaseLog, " &
            "SBEAPCaseLogLink, SBEAPClients  " &
            "where SBEAPActionLog.numCaseID = SBEAPCaseLog.numCaseID " &
            "and SBEAPActionLog.numCaseID = SBEAPCaseLogLink.numCaseID " &
            "and SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID " &
            "and  (datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " &
            "or datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " &
            "or datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "') ) "


            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtClientAssistCount.Text = dr.Item("Count")
            End While
            dr.Close()

            SQL = "select count(*) as Count " &
            "from SBEAPCaseLog " &
            "where datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtNewCaseCount.Text = dr.Item("Count")
            End While
            dr.Close()

            SQL = "select count(*) as Count " &
            "from (select distinct(SBEAPCaseLog.numCaseID) " &
            "from SBEAPCaseLog, SBEAPActionLog  " &
            "where SBEAPCaseLog.numCaseID = SBEAPActionLog.numCaseID " &
            "and datCaseOpened < '" & DTPReportStartDate.Text & "' " &
            "and (datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " &
            "or datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "'  " &
            "or sbeapActionLog.datCreationDate between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "')) "

            cmd = New SqlCommand(SQL, CurrentConnection)
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

            SQL = "select Count(*) as Count " &
            "from SBEAPCaseLog " &
            "where datCasecLosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtCaseClosedCount.Text = dr.Item("Count")
            End While
            dr.Close()

            SQL = "select count(*) as Count " &
            "from SBEAPActionLog " &
            "where datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtTotalActionCount.Text = dr.Item("Count")
            End While
            dr.Close()

            SQL = "Select count(*) as Count " &
            "from SBEAPActionLog, " &
            "SBEAPPhoneLog " &
            "where SBEAPActionLog.numActionID = SBEAPPhoneLog.numActionID " &
            "and datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " &
            "and strFrontDeskCall is not Null " &
            "and strFrontDeskCall = 'True' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtFrontDeskCallCount.Text = dr.Item("Count")
            End While
            dr.Close()

            dsAction = New DataSet

            SQL = "select Count(*) as Count, numActionType " &
            "from SBEAPActionLog " &
            "where datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " &
            "group by numActionType "

            daAction = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daAction.Fill(dsAction, "ActionCount")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
    Private Sub btnViewCase_Click(sender As Object, e As EventArgs) Handles btnViewCase.Click
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
    Private Sub btnRunSBEAPReport_Click(sender As Object, e As EventArgs) Handles btnRunSBEAPReport.Click
        Try
            RunReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewNewClient_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewNewClient.LinkClicked
        Try
            SQL = "select " &
            "SBEAPClients.ClientID, strCompanyName, " &
            "datCompanyCreated as datCompanyCreated, STRCLIENTDESCRIPTION " &
            "from SBEAPClients, SBEAPCLientData " &
            "where SBEAPClients.ClientID = SBEAPClientData.ClientID " &
            "and datCompanyCreated between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            dsView = New DataSet
            daView = New SqlDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbClientsAssisted_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbClientsAssisted.LinkClicked
        Try
            SQL = "select " &
            "distinct(SBEAPActionLog.numCaseID) as CaseID, " &
            "SBEAPClients.ClientID, strCompanyName, " &
            "case " &
            "when datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then 'Case Opened' " &
            "when datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "'  then 'Case Closed' " &
            "when datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then 'Action Occured' " &
            "end CountReason, " &
            "case " &
            "when datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then datCaseOpened " &
            "when datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then datCaseClosed " &
            "else null   " &
            "end CountDate, strCaseSummary " &
            "from SBEAPActionLog, SBEAPCaseLog, " &
            "SBEAPCaseLogLink, SBEAPClients  " &
            "where SBEAPActionLog.numCaseID = SBEAPCaseLog.numCaseID " &
            "and SBEAPActionLog.numCaseID = SBEAPCaseLogLink.numCaseID " &
            "and SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID " &
            "and  (datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " &
            "or datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " &
            "or datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "') "

            dsView = New DataSet
            daView = New SqlDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbNewCases_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbNewCases.LinkClicked
        Try
            SQL = "select SBEAPCaseLog.numCaseID, " &
            "SBEAPClients.ClientID, strCompanyName, " &
            "strCaseSummary, " &
            "datCaseOpened as datCaseOpened, " &
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible " &
            "from SBEAPCaseLog, SBEAPClients, " &
            "SBEAPCaseLogLink, EPDUserProfiles " &
            "where SBEAPCaseLog.numCaseID  = SBEAPCaseLogLink.numCaseID (+) " &
            "and SBEAPCaseLogLink.ClientID = SbeapClients.CLientID  (+) " &
            "and SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID (+) " &
            "and datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            dsView = New DataSet
            daView = New SqlDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbExistingCases_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbExistingCases.LinkClicked
        Try
            SQL = "select distinct(SBEAPCaseLog.numCaseID), " &
            "SBEAPClients.ClientID, strCompanyName, " &
            "strCaseSummary, " &
            "Case " &
            "when datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then 'Action Occured' " &
            "when datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then 'Case Closed' " &
            "end CountReason, " &
            "datCaseOpened as datCaseOpened, " &
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible,  " &
            "case " &
            "when strComplaintBased = 'True' then 'True' " &
            "else 'False' " &
            "end ComplaintBased " &
            "from SBEAPCaseLog, SBEAPActionLog, " &
            "SBEAPCaseLogLink, SBEAPClients, " &
            "EPDUserProfiles " &
            "where SBEAPCaseLog.numCaseID = SBEAPActionLog.numCaseID  " &
            "and SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseId (+) " &
            "and SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID  (+) " &
            "and SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID (+) " &
            "and datCaseOpened < '" & DTPReportStartDate.Text & "'  " &
            "and (datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "'  " &
            "or datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " &
            "or sbeapActionLog.datCreationDate between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "') "

            dsView = New DataSet
            daView = New SqlDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbTotalCases_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbTotalCases.LinkClicked
        Try
            SQL = "select SBEAPCaseLog.numCaseID, " &
            "SBEAPClients.ClientID, strCompanyName, " &
            "strCaseSummary, " &
            "datCaseOpened as datCaseOpened, " &
            "'Case Opened' as CountReason,  " &
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible  " &
            "from SBEAPCaseLog, SBEAPClients, " &
            "SBEAPCaseLogLink, " &
            "EPDUserProfiles " &
            "where SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseID (+) " &
            "and SBEAPCaseLogLink.ClientID = SbeapClients.CLientID (+) " &
              "and SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID (+) " &
            "and datCaseOpened between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " &
            "union " &
            "select distinct(SBEAPCaseLog.numCaseID), " &
            "SBEAPClients.ClientID, strCompanyName, " &
            "strCaseSummary, " &
            "datCaseOpened , " &
            "Case " &
            "when datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then 'Action Occured' " &
            "when datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' then 'Case Closed' " &
            "end CountReason, (strLastName|| ', ' ||strFirstName) as StaffResponsible   " &
            "from SBEAPCaseLog, SBEAPActionLog, " &
            "SBEAPCaseLogLink, SBEAPClients, " &
            "EPDUserProfiles " &
            "where SBEAPCaseLog.numCaseID = SBEAPActionLog.numCaseID " &
            "and SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseId (+) " &
            "and SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID (+) " &
            "and SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID (+) " &
            "and datCaseOpened < '" & DTPReportStartDate.Text & "' " &
            "and (datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " &
            "or datCaseClosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " &
            "or sbeapActionLog.datCreationDate between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "') "

            dsView = New DataSet
            daView = New SqlDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbCasesClosed_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbCasesClosed.LinkClicked
        Try
            SQL = "select " &
            "distinct(SBEAPCaseLog.numCaseID), " &
            "SBEAPClients.clientID, strCompanyName, " &
            "datCaseOpened as datCaseOpened, " &
            "strCaseSummary, " &
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible, " &
            "datCaseClosed " &
            "from SBEAPCaseLog, SBEAPCaseLogLink, " &
            "SBEAPClients, " &
            "EPDUserProfiles " &
            "where SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseID (+) " &
            "and SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID (+) " &
            "and SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID (+) " &
            "and datCasecLosed between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            dsView = New DataSet
            daView = New SqlDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbFrontDestCalls_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbFrontDestCalls.LinkClicked
        Try
            SQL = "select " &
           "distinct(SBEAPActionLog.numActionID), " &
           "sbeapCaseLog.numCaseID, " &
           "SBEAPClients.ClientID, " &
           "strCompanyName, strWorkDescription, " &
           "datActionOccured as datActionOccured, " &
           "(strLastName|| ', ' ||strFirstName) as StaffResponsible     " &
           "from SBEAPActionLog, LookUpSBEAPCaseWork, " &
           "SBEAPCaseLog, SBEAPCaseLogLink, " &
           "SBEAPClients, EPDUserProfiles,  " &
           "SBEAPPhoneLog " &
           "where SBEAPActionLog.numActionType = LookUpSBEAPCaseWork.numActionType  " &
           "and SBEAPActionlog.numCaseID = SBEAPCaseLog.numCaseID   " &
           "and SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseID (+) " &
           "and SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID  (+) " &
           "and SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID (+) " &
           "and SBEAPActionLog.numActionID = SBEAPPhoneLog.numActionID " &
           "and datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " &
           "and SBEAPActionLog.numActionType = '6' " &
           "and strFrontDeskCall is not Null " &
           "and strFrontDeskCall = 'True' "

            dsView = New DataSet
            daView = New SqlDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbTotalActions_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbTotalActions.LinkClicked
        Try
            SQL = "select " &
            "distinct(SBEAPActionLog.numActionID), " &
            "sbeapCaseLog.numCaseID, " &
            "SBEAPClients.ClientID, " &
            "strCompanyName, strWorkDescription, " &
            "datActionOccured as datActionOccured, " &
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible     " &
            "from SBEAPActionLog, LookUpSBEAPCaseWork, " &
            "SBEAPCaseLog, SBEAPCaseLogLink, " &
            "SBEAPClients, EPDUserProfiles  " &
            "where SBEAPActionLog.numActionType = LookUpSBEAPCaseWork.numActionType  " &
            "and SBEAPActionlog.numCaseID = SBEAPCaseLog.numCaseID   " &
            "and SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseID (+) " &
            "and SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID  (+) " &
            "and SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID (+) " &
            "and datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' "

            dsView = New DataSet
            daView = New SqlDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewOther_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbActionTypes.LinkClicked
        Try
            If txtActionTypeCount.Text <> "" And txtActionTypeCount.Text <> "0" And txtActionTypeCount.Text <> " " Then
                SQL = "select " &
                "distinct(SBEAPActionLog.numActionID), " &
                "sbeapCaseLog.numCaseID, " &
                "SBEAPClients.ClientID, " &
                "strCompanyName, strWorkDescription, " &
                "datActionOccured as datActionOccured, " &
                "(strLastName|| ', ' ||strFirstName) as StaffResponsible, " &
                "strCaseSummary " &
                "from SBEAPActionLog, LookUpSBEAPCaseWork, " &
                "SBEAPCaseLog, SBEAPCaseLogLink, " &
                "SBEAPClients, EPDUserProfiles  " &
                "where SBEAPActionLog.numActionType = LookUpSBEAPCaseWork.numActionType  " &
                "and SBEAPActionlog.numCaseID = SBEAPCaseLog.numCaseID   " &
                "and SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseID (+) " &
                "and SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID  (+) " &
                "and SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID (+) " &
                "and datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " &
                "and SBEAPActionLog.numActionType = '" & cboActionTypes.SelectedValue & "' "
            Else
                SQL = "select " &
              "distinct(SBEAPActionLog.numActionID), " &
              "sbeapCaseLog.numCaseID, " &
              "SBEAPClients.ClientID, " &
              "strCompanyName, strWorkDescription, " &
              "datActionOccured, " &
              "(strLastName|| ', ' ||strFirstName) as StaffResponsible, " &
              "strCaseSummary " &
              "from SBEAPActionLog, LookUpSBEAPCaseWork, " &
              "SBEAPCaseLog, SBEAPCaseLogLink, " &
              "SBEAPClients, EPDUserProfiles  " &
              "where SBEAPActionLog.numActionType = LookUpSBEAPCaseWork.numActionType  " &
              "and SBEAPActionlog.numCaseID = SBEAPCaseLog.numCaseID   " &
              "and SBEAPCaseLog.numCaseID = SBEAPCaseLogLink.numCaseID (+) " &
              "and SBEAPCaseLogLink.ClientID = SBEAPClients.ClientID  (+) " &
              "and SBEAPCaseLog.numStaffResponsible = EPDUserProfiles.numUserID (+) " &
              "and datActionOccured between '" & DTPReportStartDate.Text & "' and '" & DTPReportEndDate.Text & "' " &
              "and SBEAPActionLog.numActionType = '0' "
            End If

            dsView = New DataSet
            daView = New SqlDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        dgvCaseWork.ExportToExcel(Me)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim cmd2 As SqlCommand
            Dim dr2 As SqlDataReader

            SQL = "Select " &
            "numActionID, " &
            "to_char(datCreationDate, 'dd-Mon-yyyy') as datCreationDate " &
            "from SBEAPActionLog " &
            "where datActionoccured is null "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("datCreationDate")) Then
                Else
                    SQL2 = "Update SBEAPActionLog set " &
                    "datActionOccured = '" & dr.Item("datCreationDate") & "' " &
                    "where numActionID = '" & dr.Item("numActionID") & "' "
                    cmd2 = New SqlCommand(SQL2, CurrentConnection)
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
    Private Sub cboActionTypes_TextChanged(sender As Object, e As EventArgs) Handles cboActionTypes.TextChanged
        Try
            If txtTotalActionCount.Text <> "" And txtTotalActionCount.Text <> "0" Then
                If cboActionTypes.SelectedValue <> " " Then
                    txtActionTypeCount.Text = "1"
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
            ErrorReport(ex, Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class