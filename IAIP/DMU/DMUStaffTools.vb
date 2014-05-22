Imports System.DateTime
Imports System.IO
Imports Oracle.DataAccess.Client

Public Class DMUStaffTools
    Dim daStaff As OracleDataAdapter
    Dim dsStaff As DataSet
    Dim SQL, SQL2, SQL3 As String
    Dim cmd, cmd2, cmd3 As OracleCommand
    Dim dr, dr2, dr3 As OracleDataReader
    Dim recExist As Boolean
    Dim dsWebPublisher As DataSet
    Dim daWebPublisher As OracleDataAdapter
    Dim dsApplicationGrid As DataSet
    Dim daApplicationGrid As OracleDataAdapter
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim airsno As String
    Dim password, encryptpwd As String
    Dim Startdate As String
    Dim EndDate As String
    Dim dsErrorLog As DataSet
    Dim daErrorLog As OracleDataAdapter
    Dim dsWebErrorLog As DataSet
    Dim daWebErrorLog As OracleDataAdapter
    Dim Emssionyear As String = Now.Year
    Dim year As String
    Dim inventoryYear As Integer
    Dim recExist2 As Boolean
    Dim dsWorkEntry As DataSet
    Dim daWorkEnTry As OracleDataAdapter
    Public dsES As DataSet
    Public daES As OracleDataAdapter
    Dim dsViewCount As DataSet
    Dim daViewCount As OracleDataAdapter
    Dim TriggerStatus As String
    Dim dsAFSVerify As DataSet
    Dim daAFSVerify As OracleDataAdapter
    Dim cmdBuild As OracleCommandBuilder
    Dim dgvRow As New DataGridViewRow

    Private Sub DMUStaffTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            LoadPermissions()
            loadYear()
            loadMailOutYear()
            loadEIYear()
            loadEIMailOutYear()
            loadEIEnrollmentYear()
            loadESEnrollmentYear()
            loadEISEnrollmentYear()
            loadEIType()
            loadEISType()
            'loadEISYear()
            loadEISThresholdYear()
            'loadcboEISAccessCodes()
            loadcboEISstatusCodes()
            LoadcboEISYESNO()
            FormatWebUsers()
            LoadEISLog()
            LoadStats()
            LoadEISYear()

            lblEITypeYear.Text = cboEIMailoutYear.SelectedItem

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "Page Load"
    Sub LoadPermissions()
        Try
            TCDMUTools.TabPages.Remove(TPEmissionInventory)
            TCDMUTools.TabPages.Remove(TPFeeTools)
            TCDMUTools.TabPages.Remove(TPESTools)
            TCDMUTools.TabPages.Remove(TabEISTool)

            If AccountFormAccess(130, 3) = "1" Or AccountFormAccess(130, 4) = "1" Then
                TCDMUTools.TabPages.Add(TPESTools)
                TCDMUTools.TabPages.Add(TPFeeTools)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub LoadDataSetInformation()
        Try
            SQL = "select " & _
            "(strLastName||', '||strFirstName) as UserName,  " & _
            "numUserID  " & _
            "from AIRBranch.EPDUserProfiles  " & _
            "order by strLastName  "

            dsStaff = New DataSet

            daStaff = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daStaff.Fill(dsStaff, "Staff")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub loadYear()
        Dim year As String

        Try
            SQL = "Select " & _
            "distinct intESYear " & _
            "from " & DBNameSpace & ".esschema " & _
            "order by intESYear desc"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Read()
            Do
                year = dr("intESYear")
                cboYear.Items.Add(year)

            Loop While dr.Read
            cboYear.SelectedIndex = 0

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub loadMailOutYear()
        'Load MailOut Year dropdown boxes
        Dim year As Integer
        Dim SQL As String

        Try
            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            SQL = "Select distinct STRESYEAR " & _
                  "from " & DBNameSpace & ".esmailout " & _
                  "order by STRESYEAR desc"
            Dim cmd As New OracleCommand(SQL, CurrentConnection)

            Dim dr As OracleDataReader = cmd.ExecuteReader()
            dr.Read()
            year = dr("STRESYEAR") + 1
            cboMailoutYear.Items.Add(year)
            Do
                year = dr("STRESYEAR")
                cboMailoutYear.Items.Add(year)
            Loop While dr.Read
            cboMailoutYear.SelectedIndex = 0

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub FormatWebUsers()
        Try
            dgvUsers.RowHeadersVisible = False
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvUsers.AllowUserToResizeColumns = True
            dgvUsers.AllowUserToAddRows = False
            dgvUsers.AllowUserToDeleteRows = False
            dgvUsers.AllowUserToOrderColumns = True
            dgvUsers.AllowUserToResizeRows = True
            dgvUsers.ColumnHeadersHeight = "35"

            dgvUsers.Columns.Add("ID", "ID")
            dgvUsers.Columns("ID").DisplayIndex = 0
            dgvUsers.Columns("ID").Visible = False

            dgvUsers.Columns.Add("numuserid", "UserID")
            dgvUsers.Columns("numuserid").DisplayIndex = 1
            dgvUsers.Columns("numuserid").Visible = False

            dgvUsers.Columns.Add("Email", "Email Address")
            dgvUsers.Columns("Email").DisplayIndex = 2
            dgvUsers.Columns("Email").Width = 250
            dgvUsers.Columns("Email").ReadOnly = True

            Dim colReadOnly As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colReadOnly)
            dgvUsers.Columns(3).HeaderText = "Admin Access"

            Dim colWrite As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colWrite)
            dgvUsers.Columns(4).HeaderText = "Fee Access"

            Dim colFullAccess As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colFullAccess)
            dgvUsers.Columns(5).HeaderText = "EI Access"

            Dim colSpecialPermissions As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colSpecialPermissions)
            dgvUsers.Columns(6).HeaderText = "ES Access"


            dgvUserFacilities.RowHeadersVisible = False
            dgvUserFacilities.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvUserFacilities.AllowUserToResizeColumns = True
            dgvUserFacilities.AllowUserToAddRows = False
            dgvUserFacilities.AllowUserToDeleteRows = False
            dgvUserFacilities.AllowUserToOrderColumns = True
            dgvUserFacilities.AllowUserToResizeRows = True
            dgvUserFacilities.ColumnHeadersHeight = "35"

            dgvUserFacilities.Columns.Add("strAIRSNumber", "AIRS Number")
            dgvUserFacilities.Columns("strAIRSNumber").DisplayIndex = 0
            dgvUserFacilities.Columns("strAIRSNumber").Visible = True

            dgvUserFacilities.Columns.Add("strFacilityName", "Facility Name")
            dgvUserFacilities.Columns("strFacilityName").DisplayIndex = 1
            dgvUserFacilities.Columns("strFacilityName").Width = 250

            Dim colReadOnly2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colReadOnly2)
            dgvUserFacilities.Columns(2).HeaderText = "Admin Access"

            Dim colWrite2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colWrite2)
            dgvUserFacilities.Columns(3).HeaderText = "Fee Access"

            Dim colFullAccess2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colFullAccess2)
            dgvUserFacilities.Columns(4).HeaderText = "EI Access"

            Dim colSpecialPermissions2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colSpecialPermissions2)
            dgvUserFacilities.Columns(5).HeaderText = "ES Access"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadEISLog()
        Try
            Dim dtQAStatus As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "Select distinct(inventoryYear) as InvYear " & _
            "from " & DBNameSpace & ".EIS_Admin " & _
            "order by invYear desc "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("InvYear")) Then
                Else
                    cboEILogYear.Items.Add(dr.Item("InvYear"))
                    cboEISStatisticsYear.Items.Add(dr.Item("InvYear"))
                End If
            End While
            dr.Close()

            SQL = "select distinct strDMUResponsibleStaff as DMUStafff " & _
            "from AIRBranch.EIS_QAAdmin " & _
            "union " & _
            "select distinct (strLastName ||', '|| strFirstName) as DMUStafff " & _
            "from AIRBranch.EPDUserProfiles " & _
            "where numBranch = '1' " & _
            "and numProgram = '3' " & _
            "and numunit = '14' " & _
            "and numEmployeeStatus = '1'  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                cboEISQAStaff.Items.Add(dr.Item("DMUStafff"))
            End While
            dr.Close()

            SQL = "Select " & _
            "QAStatusCode, strDesc " & _
            "From AIRBranch.EISLK_QAStatus " & _
            "Where active = '1' " & _
            "order by UpdateDateTime  "

            ds = New DataSet

            da = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            da.Fill(ds, "QAStatus")

            dtQAStatus.Columns.Add("QAStatusCode", GetType(System.String))
            dtQAStatus.Columns.Add("strDesc", GetType(System.String))

            drNewRow = dtQAStatus.NewRow()
            drNewRow("QAStatusCode") = ""
            drNewRow("strDesc") = ""
            dtQAStatus.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("QAStatus").Rows()
                drNewRow = dtQAStatus.NewRow()
                drNewRow("QAStatusCode") = drDSRow("QAStatusCode")
                drNewRow("strDesc") = drDSRow("strDesc")
                dtQAStatus.Rows.Add(drNewRow)
            Next

            With cboEISQAStatus
                .DataSource = dtQAStatus
                .DisplayMember = "strDesc"
                .ValueMember = "QAStatusCode"
                .SelectedIndex = 0
            End With
           
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadStats()
        Try
            dgvEISStats.RowHeadersVisible = False
            dgvEISStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEISStats.AllowUserToResizeColumns = True
            dgvEISStats.AllowUserToAddRows = False
            dgvEISStats.AllowUserToDeleteRows = False
            dgvEISStats.AllowUserToOrderColumns = True
            dgvEISStats.AllowUserToResizeRows = True
            dgvEISStats.ColumnHeadersHeight = "35"

            'Dim colSelect As New DataGridViewCheckBoxColumn
            'dgvEISStats.Columns.Add(colSelect)
            'dgvEISStats.Columns(0).HeaderText = " "
            'dgvEISStats.Columns(0).Width = 50

            'dgvEISStats.Columns.Add("FacilitySiteID", "AIRS No.")
            'dgvEISStats.Columns("FacilitySiteID").DisplayIndex = 1
            'dgvEISStats.Columns("FacilitySiteID").Visible = True

            'dgvEISStats.Columns.Add("strFacilityName", "Facility Name")
            'dgvEISStats.Columns("strFacilityName").DisplayIndex = 2
            'dgvEISStats.Columns("strFacilityName").Width = 250
            'dgvEISStats.Columns("strFacilityName").ReadOnly = True

            'dgvEISStats.Columns.Add("InventoryYear", "EIS Year")
            'dgvEISStats.Columns("InventoryYear").DisplayIndex = 3
            'dgvEISStats.Columns("InventoryYear").Visible = True

            'dgvEISStats.Columns.Add("EISStatus", "EIS Status")
            'dgvEISStats.Columns("EISStatus").DisplayIndex = 4
            'dgvEISStats.Columns("EISStatus").Visible = True

            'dgvEISStats.Columns.Add("EISAccess", "EIS Access")
            'dgvEISStats.Columns("EISAccess").DisplayIndex = 5
            'dgvEISStats.Columns("EISAccess").Visible = True

            'dgvEISStats.Columns.Add("strOptOut", "Opt Out")
            'dgvEISStats.Columns("strOptOut").DisplayIndex = 6
            'dgvEISStats.Columns("strOptOut").Visible = True

            'dgvEISStats.Columns.Add("strMailOut", "Mailout")
            'dgvEISStats.Columns("strMailOut").DisplayIndex = 7
            'dgvEISStats.Columns("strMailOut").Visible = True

            'dgvEISStats.Columns.Add("ContactEmail", "Contact Email")
            'dgvEISStats.Columns("ContactEmail").DisplayIndex = 8
            'dgvEISStats.Columns("ContactEmail").Visible = True

            'dgvEISStats.Columns.Add("strContactPrefix", "Contact Prefix")
            'dgvEISStats.Columns("strContactPrefix").DisplayIndex = 9
            'dgvEISStats.Columns("strContactPrefix").Visible = True

            'dgvEISStats.Columns.Add("strContactFirstName", "Contact First Name")
            'dgvEISStats.Columns("strContactFirstName").DisplayIndex = 10
            'dgvEISStats.Columns("strContactFirstName").Visible = True

            'dgvEISStats.Columns.Add("strContactLastName", "Contact Last Name")
            'dgvEISStats.Columns("strContactLastName").DisplayIndex = 11
            'dgvEISStats.Columns("strContactLastName").Visible = True

            'dgvEISStats.Columns.Add("strDMUResponsibleStaff", "QA Reviewer")
            'dgvEISStats.Columns("strDMUResponsibleStaff").DisplayIndex = 12
            'dgvEISStats.Columns("strDMUResponsibleStaff").Visible = True

            'dgvEISStats.Columns.Add("strEnrollment", "Enrollment")
            'dgvEISStats.Columns("strEnrollment").DisplayIndex = 13
            'dgvEISStats.Columns("strEnrollment").Visible = True

            'dgvEISStats.Columns.Add("strDesc", "QA Status")
            'dgvEISStats.Columns("strDesc").DisplayIndex = 14
            'dgvEISStats.Columns("strDesc").Visible = True

            'dgvEISStats.Columns.Add("datQAStatus", "QA Status Data")
            'dgvEISStats.Columns("datQAStatus").DisplayIndex = 15
            'dgvEISStats.Columns("datQAStatus").Visible = True
            'dgvEISStats.Columns("datQAStatus").DefaultCellStyle.Format = "dd-MMM-yyyy"

            'dgvEISStats.Columns.Add("IAIPContactPrefix", "IAIP Contact Prefix")
            'dgvEISStats.Columns("IAIPContactPrefix").DisplayIndex = 16
            'dgvEISStats.Columns("IAIPContactPrefix").Visible = True

            'dgvEISStats.Columns.Add("IAIPContactFirstname", "IAIP Contact First Name")
            'dgvEISStats.Columns("IAIPContactFirstname").DisplayIndex = 17
            'dgvEISStats.Columns("IAIPContactFirstname").Visible = True

            'dgvEISStats.Columns.Add("IAIPContactLastName", "IAIP Contact Last Name")
            'dgvEISStats.Columns("IAIPContactLastName").DisplayIndex = 18
            'dgvEISStats.Columns("IAIPContactLastName").Visible = True

            'dgvEISStats.Columns.Add("IAIPContactEmail", "IAIP Contact Email")
            'dgvEISStats.Columns("IAIPContactEmail").DisplayIndex = 19
            'dgvEISStats.Columns("IAIPContactEmail").Visible = True

            'dgvEISStats.Columns.Add("strContactCompanyName", "Contact Co. Name")
            'dgvEISStats.Columns("strContactCompanyName").DisplayIndex = 20
            'dgvEISStats.Columns("strContactCompanyName").Visible = True

            'dgvEISStats.Columns.Add("strContactAddress1", "Contact Address")
            'dgvEISStats.Columns("strContactAddress1").DisplayIndex = 21
            'dgvEISStats.Columns("strContactAddress1").Visible = True

            'dgvEISStats.Columns.Add("strContactAddress2", "Contact Address 2")
            'dgvEISStats.Columns("strContactAddress2").DisplayIndex = 22
            'dgvEISStats.Columns("strContactAddress2").Visible = True

            'dgvEISStats.Columns.Add("strContactCity", "Contact City")
            'dgvEISStats.Columns("strContactCity").DisplayIndex = 23
            'dgvEISStats.Columns("strContactCity").Visible = True

            'dgvEISStats.Columns.Add("strContactState", "Contact State")
            'dgvEISStats.Columns("strContactState").DisplayIndex = 24
            'dgvEISStats.Columns("strContactState").Visible = True

            'dgvEISStats.Columns.Add("strContactZipCode", "Contact Zip Code")
            'dgvEISStats.Columns("strContactZipCode").DisplayIndex = 25
            'dgvEISStats.Columns("strContactZipCode").Visible = True

            'dgvEISStats.Columns.Add("strContactFirstname", "Contact First Name")
            'dgvEISStats.Columns("strContactFirstname").DisplayIndex = 26
            'dgvEISStats.Columns("strContactFirstname").Visible = True

            'dgvEISStats.Columns.Add("strContactLastName", "Contact Last Name")
            'dgvEISStats.Columns("strContactLastName").DisplayIndex = 27
            'dgvEISStats.Columns("strContactLastName").Visible = True

            'dgvEISStats.Columns.Add("strContactPrefix", "Contact Prefix")
            'dgvEISStats.Columns("strContactPrefix").DisplayIndex = 28
            'dgvEISStats.Columns("strContactPrefix").Visible = True

            'dgvEISStats.Columns.Add("strContactEmail", "Contact Email")
            'dgvEISStats.Columns("strContactEmail").DisplayIndex = 29
            'dgvEISStats.Columns("strContactEmail").Visible = True

            Dim colSelect As New DataGridViewCheckBoxColumn
            dgvEISStats.Columns.Add(colSelect)
            dgvEISStats.Columns(0).HeaderText = " "
            dgvEISStats.Columns(0).Width = 50

            dgvEISStats.Columns.Add("FacilitySiteID", "AIRS No.")
            dgvEISStats.Columns("FacilitySiteID").DisplayIndex = 1
            dgvEISStats.Columns("FacilitySiteID").Visible = True

            dgvEISStats.Columns.Add("strFacilityName", "Facility Name")
            dgvEISStats.Columns("strFacilityName").DisplayIndex = 2
            dgvEISStats.Columns("strFacilityName").Width = 250
            dgvEISStats.Columns("strFacilityName").ReadOnly = True

            dgvEISStats.Columns.Add("InventoryYear", "EIS Year")
            dgvEISStats.Columns("InventoryYear").DisplayIndex = 3
            dgvEISStats.Columns("InventoryYear").Visible = True

            dgvEISStats.Columns.Add("EISStatus", "EIS Status")
            dgvEISStats.Columns("EISStatus").DisplayIndex = 4
            dgvEISStats.Columns("EISStatus").Visible = True

            dgvEISStats.Columns.Add("EISAccess", "EIS Access")
            dgvEISStats.Columns("EISAccess").DisplayIndex = 5
            dgvEISStats.Columns("EISAccess").Visible = True

            dgvEISStats.Columns.Add("OptOut", "Opt Out")
            dgvEISStats.Columns("OptOut").DisplayIndex = 6
            dgvEISStats.Columns("OptOut").Visible = True

            dgvEISStats.Columns.Add("MailOut", "Mailout")
            dgvEISStats.Columns("MailOut").DisplayIndex = 7
            dgvEISStats.Columns("MailOut").Visible = True

            dgvEISStats.Columns.Add("MailoutEmail", "Mailout Email")
            dgvEISStats.Columns("MailoutEmail").DisplayIndex = 8
            dgvEISStats.Columns("MailoutEmail").Visible = True

            dgvEISStats.Columns.Add("strDMUResponsibleStaff", "QA Reviewer")
            dgvEISStats.Columns("strDMUResponsibleStaff").DisplayIndex = 9
            dgvEISStats.Columns("strDMUResponsibleStaff").Visible = True

            dgvEISStats.Columns.Add("Enrollment", "Enrollment")
            dgvEISStats.Columns("Enrollment").DisplayIndex = 10
            dgvEISStats.Columns("Enrollment").Visible = True

            dgvEISStats.Columns.Add("QASTATUS", "QA Status")
            dgvEISStats.Columns("QASTATUS").DisplayIndex = 11
            dgvEISStats.Columns("QASTATUS").Visible = True

            dgvEISStats.Columns.Add("datQAStatus", "QA Status Data")
            dgvEISStats.Columns("datQAStatus").DisplayIndex = 12
            dgvEISStats.Columns("datQAStatus").Visible = True
            dgvEISStats.Columns("datQAStatus").DefaultCellStyle.Format = "dd-MMM-yyyy"


            dgvEISStats.Columns.Add("IAIPPrefix", "IAIP Prefix")
            dgvEISStats.Columns("IAIPPrefix").DisplayIndex = 13
            dgvEISStats.Columns("IAIPPrefix").Visible = True

            dgvEISStats.Columns.Add("IAIPFIRSTNAME", "IAIP First Name")
            dgvEISStats.Columns("IAIPFIRSTNAME").DisplayIndex = 14
            dgvEISStats.Columns("IAIPFIRSTNAME").Visible = True


            dgvEISStats.Columns.Add("IAIPLASTNAME", "IAIP Last Name")
            dgvEISStats.Columns("IAIPLASTNAME").DisplayIndex = 15
            dgvEISStats.Columns("IAIPLASTNAME").Visible = True

            dgvEISStats.Columns.Add("IAIPEMAIL", "IAIP Email")
            dgvEISStats.Columns("IAIPEMAIL").DisplayIndex = 16
            dgvEISStats.Columns("IAIPEMAIL").Visible = True

            dgvEISStats.Columns.Add("EISCOMPANYNAME", "Contact Co. Name")
            dgvEISStats.Columns("EISCOMPANYNAME").DisplayIndex = 17
            dgvEISStats.Columns("EISCOMPANYNAME").Visible = True

            dgvEISStats.Columns.Add("EISADDRESS", "Contact Address")
            dgvEISStats.Columns("EISADDRESS").DisplayIndex = 18
            dgvEISStats.Columns("EISADDRESS").Visible = True

            dgvEISStats.Columns.Add("EISADDRESS2", "Contact Address 2")
            dgvEISStats.Columns("EISADDRESS2").DisplayIndex = 19
            dgvEISStats.Columns("EISADDRESS2").Visible = True

            dgvEISStats.Columns.Add("EISCITY", "Contact City")
            dgvEISStats.Columns("EISCITY").DisplayIndex = 20
            dgvEISStats.Columns("EISCITY").Visible = True

            dgvEISStats.Columns.Add("EISState", "Contact State")
            dgvEISStats.Columns("EISState").DisplayIndex = 21
            dgvEISStats.Columns("EISState").Visible = True

            dgvEISStats.Columns.Add("EISZipCode", "Contact Zip Code")
            dgvEISStats.Columns("EISZipCode").DisplayIndex = 22
            dgvEISStats.Columns("EISZipCode").Visible = True

            dgvEISStats.Columns.Add("EISPrefix", "Contact Prefix")
            dgvEISStats.Columns("EISPrefix").DisplayIndex = 23
            dgvEISStats.Columns("EISPrefix").Visible = True

            dgvEISStats.Columns.Add("EISFirstname", "Contact First Name")
            dgvEISStats.Columns("EISFirstname").DisplayIndex = 24
            dgvEISStats.Columns("EISFirstname").Visible = True

            dgvEISStats.Columns.Add("EISLastName", "Contact Last Name")
            dgvEISStats.Columns("EISLastName").DisplayIndex = 25
            dgvEISStats.Columns("EISLastName").Visible = True

        Catch ex As Exception

        End Try
    End Sub
#End Region
    Private Sub DEVDataManagementTools_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "Web Application Users"
    Private Sub btnActivateEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivateEmail.Click
        Try
            LoadComboBoxesEmail()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
#Region "Mahesh Code for Web App Users"
    Function LoadComboBoxes() As DataTable
        Dim dtairs As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow
        Dim SQL As String

        Try
            SQL = "Select DISTINCT substr(strairsnumber, 5) as strairsnumber, " _
            + "strfacilityname " _
            + "from " & DBNameSpace & ".APBFacilityInformation " _
            + "Order by strAIRSNumber "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            da.Fill(ds, "facilityInfo")

            dtairs.Columns.Add("strairsnumber", GetType(System.String))
            dtAIRS.Columns.Add("strfacilityname", GetType(System.String))

            drNewRow = dtAIRS.NewRow()
            drNewRow("strfacilityname") = " "
            drNewRow("strairsnumber") = " "
            dtAIRS.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("facilityInfo").Rows()
                drNewRow = dtAIRS.NewRow()
                drNewRow("strairsnumber") = drDSRow("strairsnumber")
                drNewRow("strfacilityname") = drDSRow("strfacilityname")
                dtAIRS.Rows.Add(drNewRow)
            Next

            Return dtairs

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            Return Nothing
        Finally
           
        End Try
         
    End Function
    Sub LoadComboBoxesEmail()
        Dim dtAIRS As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Dim SQL As String

        Try
            

            SQL = "Select numuserid, struseremail " _
            + "from " & DBNameSpace & ".OlapUserLogin " _
            + "Order by struseremail "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            da.Fill(ds, "UserEmail")

            dtAIRS.Columns.Add("numuserid", GetType(System.String))
            dtAIRS.Columns.Add("struseremail", GetType(System.String))

            drNewRow = dtAIRS.NewRow()
            drNewRow("numuserid") = " "
            drNewRow("struseremail") = " "
            dtAIRS.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("UserEmail").Rows()
                drNewRow = dtAIRS.NewRow()
                drNewRow("numuserid") = drDSRow("numuserid")
                drNewRow("struseremail") = drDSRow("struseremail")
                dtAIRS.Rows.Add(drNewRow)
            Next
            Dim temp As String

            temp = dtAIRS.Rows.Count

            With cboUserEmail
                .DataSource = dtAIRS
                .DisplayMember = "struseremail"
                .ValueMember = "numuserid"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
        End Try
         

    End Sub
    
    
    Private Sub lblViewFacility_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewFacility.LinkClicked
        Try



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Sub LoadUserInfo(ByVal UserData As String)
        Try
            SQL = "Select " & _
            "" & DBNameSpace & ".OLAPUserProfile.numUserID, " & _
            "strfirstname, strlastname, " & _
            "strtitle, strcompanyname, " & _
            "straddress, strcity, " & _
            "strstate, strzip, " & _
            "strphonenumber, strfaxnumber, " & _
            "datLastLogIn, strConfirm, " & _
            "strUserEmail " & _
            "from " & DBNameSpace & ".OlapUserProfile, " & DBNameSpace & ".OLAPUserLogIn " & _
            "where " & DBNameSpace & ".OLAPUserProfile.numUserID = " & DBNameSpace & ".OLAPUserLogIn.numuserid " & _
            "and strUserEmail = upper('" & UserData & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("numUserID")) Then
                    txtWebUserID.Clear()
                Else
                    txtWebUserID.Text = dr.Item("numUserID")
                End If
                If IsDBNull(dr.Item("strfirstname")) Then
                    txtEditFirstName.Clear()
                Else
                    lblFName.Text = "First Name: " & dr.Item("strfirstname")
                    txtEditFirstName.Text = dr.Item("strFirstName")
                End If
                If IsDBNull(dr.Item("strlastname")) Then
                    txtEditLastName.Clear()
                Else
                    lblLName.Text = "Last Name: " & dr.Item("strlastname")
                    txtEditLastName.Text = dr.Item("strLastName")
                End If
                If IsDBNull(dr.Item("strtitle")) Then
                    txtEditTitle.Clear()
                Else
                    lblTitle.Text = "Title: " & dr.Item("strtitle")
                    txtEditTitle.Text = dr.Item("strTitle")
                End If
                If IsDBNull(dr.Item("strcompanyname")) Then
                    txtEditCompany.Clear()
                Else
                    lblCoName.Text = "Company Name: " & dr.Item("strcompanyname")
                    txtEditCompany.Text = dr.Item("strCompanyName")
                End If
                If IsDBNull(dr.Item("straddress")) Then
                    txtEditAddress.Clear()
                Else
                    lblAddress.Text = dr.Item("straddress")
                    txtEditAddress.Text = dr.Item("strAddress")
                End If
                If IsDBNull(dr.Item("strcity")) Then
                    txtEditCity.Clear()
                    mtbEditState.Clear()
                    mtbEditZipCode.Clear()
                Else
                    lblCityStateZip.Text = dr.Item("strcity") & ", " & dr.Item("strstate") & " " & dr.Item("strzip")
                    txtEditCity.Text = dr.Item("strCity")
                    mtbEditState.Text = dr.Item("strState")
                    mtbEditZipCode.Text = dr.Item("strZip")
                End If
                If IsDBNull(dr.Item("strphonenumber")) Then
                    mtbEditPhoneNumber.Clear()
                Else
                    lblPhoneNo.Text = "Phone Number: " & dr.Item("strphonenumber")
                    mtbEditPhoneNumber.Text = dr.Item("strPhoneNumber")
                End If
                If IsDBNull(dr.Item("strfaxnumber")) Then
                    mtbEditFaxNumber.Clear()
                Else
                    lblFaxNo.Text = "Fax Number: " & dr.Item("strfaxnumber")
                    mtbEditFaxNumber.Text = dr.Item("strFaxNumber")
                End If
                If IsDBNull(dr.Item("strConfirm")) Then
                    lblConfirmDate.Text = ""
                Else
                    lblConfirmDate.Text = "Date User Email Confirmed: " & dr.Item("strConfirm")
                End If
                If IsDBNull(dr.Item("datLastLogIn")) Then
                    lblLastLogIn.Text = ""
                Else
                    lblLastLogIn.Text = "Date User Last Logged In: " & dr.Item("datLastLogIn")
                End If
                If IsDBNull(dr.Item("strUserEmail")) Then
                    txtEditEmail.Text = ""
                Else
                    txtEditEmail.Text = dr.Item("strUserEmail")
                End If
            Else
                txtWebUserID.Clear()
                txtEditFirstName.Clear()
                txtEditLastName.Clear()
                txtEditTitle.Clear()
                txtEditCompany.Clear()
                txtEditAddress.Clear()
                txtEditCity.Clear()
                mtbEditState.Clear()
                mtbEditZipCode.Clear()
                mtbEditPhoneNumber.Clear()
                mtbEditFaxNumber.Clear()
                lblLastLogIn.Text = ""
                lblConfirmDate.Text = ""
                txtEditEmail.Clear()
            End If

            txtEditUserPassword.Clear()
            txtEditFirstName.Visible = False
            txtEditLastName.Visible = False
            txtEditTitle.Visible = False
            txtEditCompany.Visible = False
            txtEditAddress.Visible = False
            txtEditCity.Visible = False
            mtbEditState.Visible = False
            mtbEditZipCode.Visible = False
            mtbEditPhoneNumber.Visible = False
            mtbEditFaxNumber.Visible = False
            btnSaveEditedData.Visible = False
            txtEditUserPassword.Visible = False
            btnChangeEmailAddress.Visible = False
            txtEditEmail.Visible = False
            btnUpdatePassword.Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadUserFacilityInfo(ByVal EmailLoc As String)
        Try
            Dim dgvRow As New DataGridViewRow

            SQL = "SELECT substr(strairsnumber, 5) as strAIRSNumber, strfacilityname, " & _
             "Case When intAdminAccess = 0 Then 'False' When intAdminAccess = 1 Then 'True' End as intAdminAccess, " & _
             "Case When intFeeAccess = 0 Then 'False' When intFeeAccess = 1 Then 'True' End as intFeeAccess, " & _
             "Case When intEIAccess = 0 Then 'False' When intEIAccess = 1 Then 'True' End as intEIAccess, " & _
             "Case When intESAccess = 0 Then 'False' When intESAccess = 1 Then 'True' End as intESAccess " & _
             "FROM " & DBNameSpace & ".OlapUserAccess, " & DBNameSpace & ".OLAPUserLogIn  " & _
             "WHERE " & DBNameSpace & ".OlapUserAccess.numUserId = " & DBNameSpace & ".OLAPUserLogIn.numUserId " & _
             "and  strUserEmail = upper('" & EmailLoc & "') " & _
             "order by strfacilityname"

            dgvUserFacilities.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvUserFacilities)
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("strFacilityName")
                End If

                If IsDBNull(dr.Item("intAdminAccess")) Then
                    dgvRow.Cells(2).Value = False
                Else
                    dgvRow.Cells(2).Value = dr.Item("intAdminAccess")
                End If
                If IsDBNull(dr.Item("intFeeAccess")) Then
                    dgvRow.Cells(3).Value = False
                Else
                    dgvRow.Cells(3).Value = dr.Item("intFeeAccess")
                End If

                If IsDBNull(dr.Item("intEIAccess")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("intEIAccess")
                End If
                If IsDBNull(dr.Item("intESAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("intESAccess")
                End If
                dgvUserFacilities.Rows.Add(dgvRow)
            End While
            dr.Close()

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "FacilityUsers")

            cboFacilityToDelete.DataSource = ds.Tables("FacilityUsers")
            cboFacilityToDelete.DisplayMember = "strfacilityname"
            cboFacilityToDelete.ValueMember = "strairsnumber"
            cboFacilityToDelete.Text = ""


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub Back()
        Try
            
            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
        End Try
         
    End Sub
 
    
#End Region
#Region "Fee Password Reset"
    Private Sub SetPassword_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
         
    End Sub

#End Region

    Private Sub llbViewEITools_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewEITools.LinkClicked
        Try
            If txtAirsNumber.Text <> "" Then
                SQL = "Select strFacilityName " & _
                "from " & DBNameSpace & ".APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        txtFacilityNameEITool.Text = ""
                    Else
                        txtFacilityNameEITool.Text = dr.Item("strFacilityName")
                    End If
                End While
                dr.Close()
                If txtFacilityNameEITool.Text <> "" Then
                    loadCboEIYear()
                    loadEIEnrollmentYear()
                Else
                    cboEIYear.Items.Clear()
                    cboEIYear.Text = ""
                    cboEIEnrollmentYear.Items.Clear()
                    cboEIEnrollmentYear.Text = ""
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnGetData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetData.Click
        Try
            If txtAirsNumber.Text <> "" Then
                SQL = "Select * from " & DBNameSpace & ".eiSI where strStateFacilityIdentifier = '" & txtAirsNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Select " & _
                    "distinct(EIEM.strInventoryYear),  " & _
                    "case  " & _
                    "when COTable.TotalEmissions is Null then  0 " & _
                    "else COTable.TotalEmissions " & _
                    "End CO,  " & _
                    "case  " & _
                    "when LeadTable.TotalEmissions is Null then 0 " & _
                    "else LeadTable. TotalEmissions " & _
                    "END Lead,   " & _
                    "case  " & _
                    "when NH3Table.TotalEmissions is Null then 0 " & _
                    "else NH3Table.TotalEmissions  " & _
                    "END NH3,   " & _
                    "case  " & _
                    "When NOXTable.TotalEmissions is Null then 0 " & _
                    "else NOXTable.TotalEmissions  " & _
                    "END NOX,  " & _
                    "case  " & _
                    "when PMTable.TotalEmissions is null then 0  " & _
                    "else PMTable.TotalEmissions  " & _
                    "end PM,   " & _
                    "case  " & _
                    "when PM10Table.TotalEmissions is NUll then 0  " & _
                    "else PM10Table.TotalEmissions  " & _
                    "end PM10,  " & _
                    "case  " & _
                    "when PM25Table.TotalEmissions is null then 0  " & _
                    "else PM25Table.TotalEmissions  " & _
                    "end PM25,  " & _
                    "case  " & _
                    "when SO2Table.TotalEmissions is NUll then 0  " & _
                    "else SO2Table.TotalEmissions  " & _
                    "End SO2,  " & _
                    "case  " & _
                    "when VOCTable.TotalEmissions is Null then 0  " & _
                    "else VOCTable.TotalEmissions  " & _
                    "end VOC,  " & _
                    "case  " & _
                    "when PMFILTable.TotalEmissions is Null then 0  " & _
                    "else PMFILTable.TotalEmissions  " & _
                    "end PMFIL " & _
                    "from " & DBNameSpace & ".EIEM,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & txtAirsNumber.Text & "'  " & _
                    "and strPollutantCode = 'CO'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) COTable,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & txtAirsNumber.Text & "'  " & _
                    "and strPollutantCode = '7439921'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) LeadTable,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & txtAirsNumber.Text & "'  " & _
                    "and strPollutantCode = 'NH3'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) NH3Table,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & txtAirsNumber.Text & "'  " & _
                    "and strPollutantCode = 'NOX'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) NOXTable,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & txtAirsNumber.Text & "'  " & _
                    "and strPollutantCode = 'PM-PRI'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) PMTable,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & txtAirsNumber.Text & "'  " & _
                    "and strPollutantCode = 'PM10-PMI'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) PM10Table,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & txtAirsNumber.Text & "'  " & _
                    "and strPollutantCode = 'PM25-PMI'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) PM25Table,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & txtAirsNumber.Text & "'  " & _
                    "and strPollutantCode = 'SO2'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) SO2Table,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & txtAirsNumber.Text & "'  " & _
                    "and strPollutantCode = 'VOC'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) VOCTable,  " & _
                    "(Select  " & _
                    "" & DBNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & DBNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & DBNameSpace & ".EIEM   " & _
                    "where " & DBNameSpace & ".EIEM.strStateFacilityIdentifier = '" & txtAirsNumber.Text & "'  " & _
                    "and strPollutantCode = 'PM-FIL'  " & _
                    "group by " & DBNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & DBNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) PMFILTable " & _
                    "where EIEM.strInventoryYear = COTable.strInventoryYear (+)   " & _
                    "and EIEM.strInventoryYear = LeadTable.strInventoryYear (+)   " & _
                    "and EIEM.strInventoryYear = NH3Table.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = NOXTable.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = PMTable.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = PM10Table.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = PM25Table.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = SO2Table.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = VOCTable.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = PMFILTable.strInventoryYear  (+) " & _
                    "and EIEM.strStateFacilityIdentifier = '" & txtAirsNumber.Text & "' " & _
                    "order by EIEM.strInventoryYear DESC "

                    ds = New DataSet
                    da = New OracleDataAdapter(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    da.Fill(ds, "EM")
                    dgvEIData.DataSource = ds
                    dgvEIData.DataMember = "EM"
                  

                    dgvEIData.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvEIData.ReadOnly = True
                    dgvEIData.AllowUserToOrderColumns = True
                    dgvEIData.AllowUserToOrderColumns = True
                    dgvEIData.RowHeadersVisible = False
                    dgvEIData.Columns("strInventoryYear").HeaderText = "Year"
                    dgvEIData.Columns("strInventoryYear").DisplayIndex = 0
                    dgvEIData.Columns("strInventoryYear").Width = 50
                    dgvEIData.Columns("CO").HeaderText = "CO"
                    dgvEIData.Columns("CO").DisplayIndex = 1
                    dgvEIData.Columns("CO").Width = 50
                    dgvEIData.Columns("LEAD").HeaderText = "LEAD"
                    dgvEIData.Columns("LEAD").DisplayIndex = 2
                    dgvEIData.Columns("LEAD").Width = 50
                    dgvEIData.Columns("NH3").HeaderText = "NH3"
                    dgvEIData.Columns("NH3").DisplayIndex = 3
                    dgvEIData.Columns("NH3").Width = 50
                    dgvEIData.Columns("NOX").HeaderText = "NOX"
                    dgvEIData.Columns("NOX").DisplayIndex = 4
                    dgvEIData.Columns("NOX").Width = 50
                    dgvEIData.Columns("PM").HeaderText = "PM"
                    dgvEIData.Columns("PM").DisplayIndex = 5
                    dgvEIData.Columns("PM").Width = 50
                    dgvEIData.Columns("PM10").HeaderText = "PM-10"
                    dgvEIData.Columns("PM10").DisplayIndex = 6
                    dgvEIData.Columns("PM10").Width = 50
                    dgvEIData.Columns("PM25").HeaderText = "PM-2.5"
                    dgvEIData.Columns("PM25").DisplayIndex = 7
                    dgvEIData.Columns("PM25").Width = 50
                    dgvEIData.Columns("SO2").HeaderText = "SO2"
                    dgvEIData.Columns("SO2").DisplayIndex = 8
                    dgvEIData.Columns("SO2").Width = 50
                    dgvEIData.Columns("VOC").HeaderText = "VOC"
                    dgvEIData.Columns("VOC").DisplayIndex = 9
                    dgvEIData.Columns("VOC").Width = 50
                    dgvEIData.Columns("PMFIL").HeaderText = "PM-FIL"
                    dgvEIData.Columns("PMFIL").DisplayIndex = 10
                    dgvEIData.Columns("PMFIL").Width = 50
                Else
                    MsgBox("There is no data available", MsgBoxStyle.OkOnly, "No EI data for this year, choose another year")
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
         
        End Try
         
    End Sub
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            ExporttoExcel()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
         
    End Sub
    Sub ExporttoExcel()
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        '   Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        'Dim ExcelApp As New Excel.Application
        Dim i As Integer
        Dim j As Integer

        Try

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If
            If dgvEIData.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()


                    For i = 0 To dgvEIData.ColumnCount - 1
                        .Cells(1, i + 1) = dgvEIData.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvEIData.ColumnCount - 1
                        For j = 0 To dgvEIData.RowCount - 2
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvEIData.Item(i, j).Value.ToString
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
    Private Sub btnExportEIExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportEIExport.Click
        Try
            If txtAirsNumber.Text <> "" And Me.cboEIYear.Text <> "" Then
                BindDataGridSI()
                BindDataGridEU()
                BindDataGridER()
                BindDataGridEP()
                BindDataGridEM()
                export2Excel()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#Region " Bind Data Grid View Routines "

    Private Sub BindDataGridSI()

        Try

            Dim SQL As String
            Dim AirsNumber As String = "0413" & txtAirsNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select strFacilityName, " & _
                            "strLocationAddress, " & _
                            "strCity, " & _
                            "strState, " & _
                            "strZipCode, " & _
                            "strCounty, " & _
                            "dblXCoordinate, " & _
                            "dblYCoordinate, " & _
                            "strHorizontalCollectionCode, " & _
                            "(Select STRHORIZCOLLECTIONMETHODDESC " & _
                               "from " & DBNameSpace & ".EILOOKUPHORIZCOLMETHOD " & _
                               "where " & DBNameSpace & ".EISI.STRHORIZONTALCOLLECTIONCODE = " & _
                               "" & DBNameSpace & ".EILOOKUPHORIZCOLMETHOD.STRHORIZCOLLECTIONMETHODCODE) as HMCdesc, " & _
                            "strHorizontalReferenceCode, " & _
                            "strHorizontalAccuracyMeasure, " & _
                            "(Select STRHORIZONTALREFERENCEDESC " & _
                               "from " & DBNameSpace & ".EILOOKUPHORIZREFDATUM " & _
                               "where " & DBNameSpace & ".EISI.STRHORIZONTALREFERENCECODE = " & _
                               "" & DBNameSpace & ".EILOOKUPHORIZREFDATUM.STRHORIZONTALREFERENCEDATUM) as HDRCdesc, " & _
                            "strContactPrefix, " & _
                            "strContactFirstName, " & _
                            "strContactLastName, " & _
                            "strContactTitle, " & _
                            "strContactEmail, " & _
                            "strContactPhoneNumber1, " & _
                            "strContactPhoneNumber2, " & _
                            "strContactFaxNumber, " & _
                            "strContactCompanyName, " & _
                            "strContactAddress1, " & _
                            "strContactCity, " & _
                            "strContactState, " & _
                            "strContactZipCode, " & _
                            "strSiteDescription, " & _
                            "strSICPrimary, " & _
                            "strNAICSPrimary " & _
                     "from " & DBNameSpace & ".eiSI where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            dgvSI.Rows.Clear()
            dgvSI.Columns.Clear()

            dgvSI.Columns.Add("strFacilityName", "Facility")
            dgvSI.Columns.Add("strLocationAddress", "Facility Location")
            dgvSI.Columns.Add("strCity", "City")
            dgvSI.Columns.Add("strState", "State")
            dgvSI.Columns.Add("strZipCode", "Zip Code")
            dgvSI.Columns.Add("strCounty", "County")
            dgvSI.Columns.Add("dblXCoordinate", "Longitude")
            dgvSI.Columns.Add("dblYCoordinate", "Latitude")
            dgvSI.Columns.Add("strHorizontalCollectionCode", "Horizontal Collection Code")
            dgvSI.Columns.Add("HMCdesc", "Horizontal Collection Desc")
            dgvSI.Columns.Add("strHorizontalAccuracyMeasure", "Horizontal Accuracy Measure")
            dgvSI.Columns.Add("strHorizontalReferenceCode", "Horizontal Datum Reference Code")
            dgvSI.Columns.Add("HDRCdesc", "Horizontal Datum Reference Desc")
            dgvSI.Columns.Add("strContactPrefix", "Contact Prefix")
            dgvSI.Columns.Add("strContactFirstName", "Contact First Name")
            dgvSI.Columns.Add("strContactLastName", "Contact Last Name")
            dgvSI.Columns.Add("strContactTitle", "Contact Title")
            dgvSI.Columns.Add("strContactEmail", "Contact Email")
            dgvSI.Columns.Add("strContactPhoneNumber1", "Contact Phone Number")
            dgvSI.Columns.Add("strContactPhoneNumber2", "Contact Phone Other")
            dgvSI.Columns.Add("strContactFaxNumber", "Contact Fax Number")
            dgvSI.Columns.Add("strContactCompanyName", "Contact Company Name")
            dgvSI.Columns.Add("strContactAddress1", "Contact Address")
            dgvSI.Columns.Add("strContactCity", "Contact City")
            dgvSI.Columns.Add("strContactState", "Contact State")
            dgvSI.Columns.Add("strContactZipCode", "Contact Zip Code")
            dgvSI.Columns.Add("strSiteDescription", "Site Description")
            dgvSI.Columns.Add("strSICPrimary", "SIC Primary")
            dgvSI.Columns.Add("strNAICSPrimary", "NAICS Primary")

            dgvSI.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvSI.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(29) As Object
                dr.GetValues(objCells)
                dgvSI.Rows.Add(objCells)
            End While

        Catch ex As Exception
            MsgBox(ex.ToString)
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub BindDataGridEU()

        Try

            Dim SQL As String
            Dim AirsNumber As String = "0413" & txtAirsNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select strEmissionUnitID, " & _
                         "sngDesignCapacity, " & _
                         "strDesignCapUnitNum, " & _
                         "(Select STRUNITDESCRIPTION " & _
                               "from " & DBNameSpace & ".EILOOKUPUNITCODES " & _
                               "where " & DBNameSpace & ".EIEU.strDesignCapUnitNum = " & _
                               "" & DBNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as numDesc, " & _
                         "strDesignCapUnitDenom, " & _
                         "(Select STRUNITDESCRIPTION " & _
                               "from " & DBNameSpace & ".EILOOKUPUNITCODES " & _
                               "where " & DBNameSpace & ".EIEU.strDesignCapUnitDenom = " & _
                               "" & DBNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as denomDesc, " & _
                         "sngMaxNameplateCapacity, " & _
                         "strEmissionUnitDesc " & _
                    "from " & DBNameSpace & ".eiEU where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            dgvEU.Rows.Clear()
            dgvEU.Columns.Clear()

            dgvEU.Columns.Add("strEmissionUnitID", "Emission Unit ID")
            dgvEU.Columns.Add("sngDesignCapacity", "Design Capacity")
            dgvEU.Columns.Add("strDesignCapUnitNum", "Design Capacity Unit Numerator")
            dgvEU.Columns.Add("numDesc", "Design Capacity Unit Numerator Desc")
            dgvEU.Columns.Add("strDesignCapUnitDenom", "Design Capacity Unit Denominator")
            dgvEU.Columns.Add("denomDesc", "Design Capacity Unit Denominator Desc")
            dgvEU.Columns.Add("sngMaxNameplateCapacity", "Max Name plate Capacity")
            dgvEU.Columns.Add("strEmissionUnitDesc", "Emission Unit Desc")

            dgvEU.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvEU.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(8) As Object
                dr.GetValues(objCells)
                dgvEU.Rows.Add(objCells)
            End While

        Catch ex As Exception
            MsgBox(ex.ToString)
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub BindDataGridER()

        Try

            Dim SQL As String
            Dim AirsNumber As String = "0413" & txtAirsNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select strEmissionReleasePointID, " & _
                            "strEmissionReleaseType, " & _
                            "(Select STREMISSIONTYPEDESC " & _
                               "from " & DBNameSpace & ".EILOOKUPEMISSIONTYPES " & _
                               "where " & DBNameSpace & ".EIER.STREMISSIONRELEASETYPE = " & _
                               "" & DBNameSpace & ".EILOOKUPEMISSIONTYPES.STREMISSIONTYPECODE) as stackType, " & _
                            "sngStackHeight, " & _
                            "sngStackDiameter, " & _
                            "sngExitGasTemperature, " & _
                            "sngExitGasVelocity, " & _
                            "sngExitGasFlowRate, " & _
                            "dblXCoordinate, " & _
                            "dblYCoordinate, " & _
                            "strEmissionReleasePtDesc, " & _
                            "strHorizontalCollectionCode, " & _
                            "(Select STRHORIZCOLLECTIONMETHODDESC " & _
                               "from " & DBNameSpace & ".EILOOKUPHORIZCOLMETHOD " & _
                               "where " & DBNameSpace & ".EIER.STRHORIZONTALCOLLECTIONCODE = " & _
                               "" & DBNameSpace & ".EILOOKUPHORIZCOLMETHOD.STRHORIZCOLLECTIONMETHODCODE) as HMCdesc, " & _
                            "strHorizontalAccuracyMeasure, " & _
                            "strHorizontalReferenceCode, " & _
                            "(Select STRHORIZONTALREFERENCEDESC " & _
                               "from " & DBNameSpace & ".EILOOKUPHORIZREFDATUM " & _
                               "where " & DBNameSpace & ".EIER.STRHORIZONTALREFERENCECODE = " & _
                               "" & DBNameSpace & ".EILOOKUPHORIZREFDATUM.STRHORIZONTALREFERENCEDATUM) as HDRCdesc " & _
                            "from " & DBNameSpace & ".eiER where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            dgvER.Rows.Clear()
            dgvER.Columns.Clear()

            dgvER.Columns.Add("strEmissionReleasePointID", "Emission Release Point ID")
            dgvER.Columns.Add("strEmissionReleaseType", "Emission Release Type")
            dgvER.Columns.Add("stackType", "Stack Type")
            dgvER.Columns.Add("sngStackHeight", "Stack Height")
            dgvER.Columns.Add("sngStackDiameter", "Stack Diameter")
            dgvER.Columns.Add("sngExitGasTemperature", "Exit Gas Temperature")
            dgvER.Columns.Add("sngExitGasVelocity", "Exit Gas Velocity")
            dgvER.Columns.Add("sngExitGasFlowRate", "Exit Gas Flow Rate")
            dgvER.Columns.Add("dblXCoordinate", "Longitude")
            dgvER.Columns.Add("dblYCoordinate", "Latitude")
            dgvER.Columns.Add("strEmissionReleasePtDesc", "Emission Release Point Desc")
            dgvER.Columns.Add("strHorizontalCollectionCode", "Horizontal Collection Code")
            dgvER.Columns.Add("HMCdesc", "Horizontal Collection Code Desc")
            dgvER.Columns.Add("strHorizontalAccuracyMeasure", "Horizontal Accuracy Measure")
            dgvER.Columns.Add("strHorizontalReferenceCode", "Horizontal Datum Reference Code")
            dgvER.Columns.Add("HDRCdesc", "Horizontal Datum Reference Code Desc")

            dgvER.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvER.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(5) As Object
                dr.GetValues(objCells)
                dgvER.Rows.Add(objCells)
            End While

        Catch ex As Exception
            MsgBox(ex.ToString)
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub BindDataGridEP()

        Try

            Dim SQL As String
            Dim AirsNumber As String = "0413" & txtAirsNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select strSCC, " & _
                            "strEmissionProcessDescription, " & _
                            "strEmissionUnitID, " & _
                            "strEmissionReleasePointID, " & _
                            "strProcessID, " & _
                            "intWinterThroughputPCT, " & _
                            "intSpringThroughputPCT, " & _
                            "intSummerThroughputPCT, " & _
                            "intFallThroughputPCT, " & _
                            "intAnnualAvgDaysPerWeek, " & _
                            "intAnnualAvgWeeksPerYear, " & _
                            "intAnnualAvgHoursPerDay, " & _
                            "intAnnualAvgHoursPerYear, " & _
                            "sngHeatContent, " & _
                            "sngSulfurContent, " & _
                            "sngAshContent, " & _
                            "sngDailySummerProcessTPut, " & _
                            "strDailySummerProcessTPutNum, " & _
                            "(Select STRUNITDESCRIPTION " & _
                               "from " & DBNameSpace & ".EILOOKUPUNITCODES " & _
                               "where " & DBNameSpace & ".EIEP.strDailySummerProcessTPutNum = " & _
                               "" & DBNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as DailySummerTputNumDesc, " & _
                            "sngActualThroughput, " & _
                            "strThroughputUnitNumerator, " & _
                            "(Select STRUNITDESCRIPTION " & _
                               "from " & DBNameSpace & ".EILOOKUPUNITCODES " & _
                               "where " & DBNameSpace & ".EIEP.strThroughputUnitNumerator = " & _
                               "" & DBNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as TputNumDesc, " & _
                            "strStartTime " & _
                       "from " & DBNameSpace & ".eiEP " & _
                      "where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            dgvEP.Rows.Clear()
            dgvEP.Columns.Clear()

            dgvEP.Columns.Add("strSCC", "SCC")
            dgvEP.Columns.Add("strEmissionProcessDescription", "Emission Process Description")
            dgvEP.Columns.Add("strEmissionUnitID", "Emission Unit ID")
            dgvEP.Columns.Add("strEmissionReleasePointID", "Emission Release Point ID")
            dgvEP.Columns.Add("strProcessID", "Process ID")
            dgvEP.Columns.Add("intWinterThroughputPCT", "Winter Throughput Percent")
            dgvEP.Columns.Add("intSpringThroughputPCT", "Spring Throughput Percent")
            dgvEP.Columns.Add("intSummerThroughputPCT", "Summer Throughput Percent")
            dgvEP.Columns.Add("intFallThroughputPCT", "Fall Throughput Percent")
            dgvEP.Columns.Add("intAnnualAvgDaysPerWeek", "Annual Average Days Per Week")
            dgvEP.Columns.Add("intAnnualAvgWeeksPerYear", "Annual Average Weeks Per Year")
            dgvEP.Columns.Add("intAnnualAvgHoursPerDay", "Annual Average Hours Per Day")
            dgvEP.Columns.Add("intAnnualAvgHoursPerYear", "Annual Average Hours Per Year")
            dgvEP.Columns.Add("sngHeatContent", "Heat Content")
            dgvEP.Columns.Add("sngSulfurContent", "Sulfur Content")
            dgvEP.Columns.Add("sngAshContent", "Ash Content")
            dgvEP.Columns.Add("sngDailySummerProcessTPut", "Daily Summer Process Throughput")
            dgvEP.Columns.Add("strDailySummerProcessTPutNum", "Daily Summer Process Throughput Numerator")
            dgvEP.Columns.Add("DailySummerTputNumDesc", "Daily Summer Process Throughput Numerator Desc")
            dgvEP.Columns.Add("sngActualThroughput", "Actual Throughput")
            dgvEP.Columns.Add("strThroughputUnitNumerator", "Actual Throughput Unit Numerator")
            dgvEP.Columns.Add("TputNumDesc", "Actual Throughput Unit Numerator Desc")
            dgvEP.Columns.Add("strStartTime", "Start Time")


            dgvEP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvEP.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(5) As Object
                dr.GetValues(objCells)
                dgvEP.Rows.Add(objCells)
            End While

        Catch ex As Exception
            MsgBox(ex.ToString)
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub BindDataGridEM()

        Try

            Dim SQL As String
            Dim AirsNumber As String = "0413" & txtAirsNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select STREMISSIONUNITID, "
            SQL += "STREMISSIONRELEASEPOINTID, "
            SQL += "STRPROCESSID, "
            SQL += "strPollutantCode, "
            SQL += "(Select STRPOLLUTANTDESC "
            SQL += "from " & DBNameSpace & ".EILOOKUPPOLLUTANTCODES "
            SQL += "where " & DBNameSpace & ".EIEM.STRPOLLUTANTCODE = "
            SQL += "" & DBNameSpace & ".EILOOKUPPOLLUTANTCODES.STRPOLLUTANTCODE) as pollutantDesc, "
            SQL += "DBLEMISSIONNUMERICVALUE, "
            SQL += "STREMISSIONUNITNUMERATOR, "
            SQL += "(Select STRUNITDESCRIPTION "
            SQL += "from " & DBNameSpace & ".EILOOKUPUNITCODES "
            SQL += "where " & DBNameSpace & ".EIEM.STREMISSIONUNITNUMERATOR = "
            SQL += "" & DBNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as EMISSIONUNITNUMERATORDesc, "
            SQL += "sngFactorNumericValue, "
            SQL += "strFactorUnitNumerator, "
            SQL += "(Select STRUNITDESCRIPTION "
            SQL += "from " & DBNameSpace & ".EILOOKUPUNITCODES "
            SQL += "where " & DBNameSpace & ".EIEM.strFactorUnitNumerator = "
            SQL += "" & DBNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as FactorUnitNumeratorDesc, "
            SQL += "strFactorUnitDenominator, "
            SQL += "(Select STRUNITDESCRIPTION "
            SQL += "from " & DBNameSpace & ".EILOOKUPUNITCODES "
            SQL += "where " & DBNameSpace & ".EIEM.strFactorUnitDenominator = "
            SQL += "" & DBNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as FactorUnitDenominatorDesc, "
            SQL += "strEmissionCalculationMetCode, "
            SQL += "(Select STREMISSIONCALCMETHODDESC "
            SQL += "from " & DBNameSpace & ".EILOOKUPEMISSIONCALCMETHOD "
            SQL += "where " & DBNameSpace & ".EIEM.strEmissionCalculationMetCode = "
            SQL += "" & DBNameSpace & ".EILOOKUPEMISSIONCALCMETHOD.STREMISSIONCALCMETHODCODE) as EMISSIONCALCMETHODDESC, "
            SQL += "strControlStatus, "
            SQL += "strControlSystemDescription, "
            SQL += "strPrimaryDeviceTypeCode, "
            SQL += "(Select STRCONTROLDEVICEDesc "
            SQL += "from " & DBNameSpace & ".EILOOKUPCONTROLDEVICE "
            SQL += "where " & DBNameSpace & ".EIEM.strPrimaryDeviceTypeCode = "
            SQL += "" & DBNameSpace & ".EILOOKUPCONTROLDEVICE.STRCONTROLDEVICECODE) as PrimaryDeviceTypeDesc, "
            SQL += "sngPrimaryPCTControlEffic, "
            SQL += "strSecondaryDeviceTypeCode, "
            SQL += "(Select STRCONTROLDEVICEDesc "
            SQL += "from " & DBNameSpace & ".EILOOKUPCONTROLDEVICE "
            SQL += "where " & DBNameSpace & ".EIEM.strSecondaryDeviceTypeCode = "
            SQL += "" & DBNameSpace & ".EILOOKUPCONTROLDEVICE.STRCONTROLDEVICECODE) as SecondaryDeviceTypeDesc, "
            SQL += "sngPCTCaptureEfficiency, "
            SQL += "sngTotalCaptureControlEffic "
            SQL += "from " & DBNameSpace & ".eiEM "
            SQL += "where strAirsYear = '" & airsYear & "'"

            'SQL = "Select * from " & DBNameSpace & ".eiEM where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            dgvEM.Rows.Clear()
            dgvEM.Columns.Clear()

            dgvEM.Columns.Add("STREMISSIONUNITID", "EMISSION UNIT ID")
            dgvEM.Columns.Add("STREMISSIONRELEASEPOINTID", "EMISSION RELEASE POINT ID")
            dgvEM.Columns.Add("STRPROCESSID", "PROCESS ID")
            dgvEM.Columns.Add("strPollutantCode", "Pollutant Code")
            dgvEM.Columns.Add("pollutantDesc", "pollutant Desc")
            dgvEM.Columns.Add("DBLEMISSIONNUMERICVALUE", "EMISSION NUMERIC VALUE")
            dgvEM.Columns.Add("STREMISSIONUNITNUMERATOR", "EMISSION UNIT NUMERATOR")
            dgvEM.Columns.Add("EMISSIONUNITNUMERATORDesc", "EMISSION UNIT NUMERATOR Desc")
            dgvEM.Columns.Add("sngFactorNumericValue", "Factor Numeric Value")
            dgvEM.Columns.Add("strFactorUnitNumerator", "Factor Unit Numerator")
            dgvEM.Columns.Add("FactorUnitNumeratorDesc", "Factor Unit Numerator Desc")
            dgvEM.Columns.Add("strFactorUnitDenominator", "Factor Unit Denominator")
            dgvEM.Columns.Add("FactorUnitDenominatorDesc", "Factor Unit Denominator Desc")
            dgvEM.Columns.Add("strEmissionCalculationMetCode", "Emission Calculation Method Code")
            dgvEM.Columns.Add("EMISSIONCALCMETHODDESC", "Emission Calculation Method Code Desc")
            dgvEM.Columns.Add("strControlStatus", "Control Status")
            dgvEM.Columns.Add("strControlSystemDescription", "Control System Description")
            dgvEM.Columns.Add("strPrimaryDeviceTypeCode", "Primary Device Type Code")
            dgvEM.Columns.Add("PrimaryDeviceTypeDesc", "Primary Device Type Code Desc")
            dgvEM.Columns.Add("sngPrimaryPCTControlEffic", "Primary Percentage Control Efficiency")
            dgvEM.Columns.Add("strSecondaryDeviceTypeCode", "Secondary Device Type Code")
            dgvEM.Columns.Add("SecondaryDeviceTypeDesc", "Secondary Device Type Code Desc")
            dgvEM.Columns.Add("sngPCTCaptureEfficiency", "Percent Capture Efficiency")
            dgvEM.Columns.Add("sngTotalCaptureControlEffic", "Total Capture Control Efficiency")



            dgvEM.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvEM.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(5) As Object
                dr.GetValues(objCells)
                dgvEM.Rows.Add(objCells)
            End While

        Catch ex As Exception
            MsgBox(ex.ToString)
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try

    End Sub

#End Region
    Private Sub export2Excel()
        Try
            exportSI()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub exportSI()
        Try
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            'Dim ExcelApp As Excel.Application = New Excel.ApplicationClass
            Dim col, row As Integer
            Dim x As String
            Dim y As String
            Dim a As Integer
            Dim b As Integer
            Dim c As Integer
            Dim d As Integer
            Dim startRow As Integer = 1

            

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            'load SI data into Excel
            If dgvSI.RowCount <> 0 Then

                ExcelApp.SheetsInNewWorkbook = 1
                ExcelApp.Workbooks.Add()


                ExcelApp.Cells(startRow, 1).value = "Facility Information"

                'For displaying the column name in the the excel file.
                '29 columns in SI 
                'cells(x,y) x=row  y=col
                For col = 0 To dgvSI.ColumnCount - 1
                    y = dgvSI.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow + 1, col + 1).value = y
                Next

                For row = 0 To 0
                    For col = 0 To dgvSI.ColumnCount - 1
                        If IsDBNull(dgvSI.Item(col, row).Value.ToString) Then
                            x = ""
                        Else
                            x = dgvSI.Item(col, row).Value.ToString
                        End If
                        'x = dgvSI.Item(col, row).Value.ToString
                        ExcelApp.Cells(startRow + 2, col + 1).value = x
                    Next
                Next
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

            txtRow.Text = startRow + 3

            'load EU data into Excel
            If dgvEU.RowCount <> 0 Then

                startRow = CInt(txtRow.Text) + 2

                ExcelApp.Cells(startRow, 1).value = "Emission Units"

                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For col = 0 To dgvEU.ColumnCount - 1
                    y = dgvEU.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow, col + 1).value = y
                Next

                a = dgvEU.ColumnCount - 1
                b = dgvEU.RowCount - 1

                'For col = 0 To dgvEU.RowCount - 1
                '    For row = 0 To dgvEU.ColumnCount - 1
                startRow = startRow + 1
                d = dgvEU.RowCount - 2
                For row = 0 To d

                    c = dgvEU.ColumnCount - 1
                    For col = 0 To c
                        If IsDBNull(dgvEU.Item(col, row).Value.ToString) Then
                            x = ""
                        Else
                            x = dgvEU.Item(col, row).Value.ToString
                        End If
                        'x = dgvEU.Item(col, row).Value.ToString
                        ExcelApp.Cells(startRow, col + 1).value = x

                    Next
                    startRow = startRow + 1
                Next
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

            txtRow.Text = startRow + 2

            'load ER data into Excel
            If dgvER.RowCount <> 0 Then

                startRow = CInt(txtRow.Text)

                ExcelApp.Cells(startRow, 1).value = "Stacks"

                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For col = 0 To dgvER.ColumnCount - 1
                    y = dgvER.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow, col + 1).value = y
                Next

                a = dgvER.ColumnCount - 1
                b = dgvER.RowCount - 1

                startRow = startRow + 1
                d = dgvER.RowCount - 2
                For row = 0 To d

                    c = dgvER.ColumnCount - 1
                    x = ""
                    For col = 0 To c
                        'If IsDBNull(dgvER.Item(col, row).Value.ToString) Then
                        If IsDBNull(dgvER.Item(col, row).Value) Then
                        Else
                            'x = dgvER.Item(col, row).Value.ToString
                            x = dgvER.Item(col, row).Value
                        End If

                        ExcelApp.Cells(startRow, col + 1).value = x

                    Next
                    startRow = startRow + 1
                Next

                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

            txtRow.Text = startRow + 2

            'load EP data into Excel
            If dgvEP.RowCount <> 0 Then

                startRow = CInt(txtRow.Text)

                ExcelApp.Cells(startRow, 1).value = "Processes"

                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For col = 0 To dgvEP.ColumnCount - 1
                    y = dgvEP.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow, col + 1).value = y
                Next

                a = dgvEP.ColumnCount - 1
                b = dgvEP.RowCount - 1

                startRow = startRow + 1
                d = dgvEP.RowCount - 2
                For row = 0 To d

                    c = dgvEP.ColumnCount - 1
                    x = ""
                    For col = 0 To c
                        'If IsDBNull(dgvEP.Item(col, row).Value.ToString) Then
                        If IsDBNull(dgvEP.Item(col, row).Value) Then
                        Else
                            'x = dgvEP.Item(col, row).Value.ToString
                            x = dgvEP.Item(col, row).Value
                        End If

                        ExcelApp.Cells(startRow, col + 1).value = x

                    Next
                    startRow = startRow + 1
                Next
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

            txtRow.Text = startRow + 2

            'load EM data into Excel
            If dgvEM.RowCount <> 0 Then

                startRow = CInt(txtRow.Text)

                ExcelApp.Cells(startRow, 1).value = "Pollutants and Control Equipment"

                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For col = 0 To dgvEM.ColumnCount - 1
                    y = dgvEM.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow, col + 1).value = y
                Next

                a = dgvEM.ColumnCount - 1
                b = dgvEM.RowCount - 1

                startRow = startRow + 1
                d = dgvEM.RowCount - 2
                For row = 0 To d

                    c = dgvEM.ColumnCount - 1
                    x = ""
                    For col = 0 To c
                        'If IsDBNull(dgvEM.Item(col, row).Value.ToString) Then
                        If IsDBNull(dgvEM.Item(col, row).Value) Then
                        Else
                            'x = dgvEM.Item(col, row).Value.ToString
                            x = dgvEM.Item(col, row).Value
                        End If

                        ExcelApp.Cells(startRow, col + 1).value = x

                    Next
                    startRow = startRow + 1
                Next

                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                MsgBox(ex.ToString)
                ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        Finally
             
        End Try

    End Sub
    Sub loadCboEIYear()
        Try
            cboEIYear.Items.Clear()

            SQL = "select distinct(strInventoryYear)  as EIYear " & _
            "from " & DBNameSpace & ".EISI " & _
            "where strStateFacilityIdentifier = '" & txtAirsNumber.Text & "' " & _
            "order by EIYear desc "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                cboEIYear.Items.Add(dr.Item("EIYear"))
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
        End Try
         

    End Sub
#Region "ES Tool"

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        Try
            runcount()
            lblYear.Text = cboYear.SelectedItem
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub runcount()
        Dim Nonresponsecount As Integer
        Dim ESMailoutCount As Integer
        Dim MailOutOptInCount As Integer
        Dim mailoutOptOutCount As Integer
        Dim ResponseCount As Integer
        Dim TotaloptinCount As Integer
        Dim TotaloptoutCount As Integer
        Dim TotalinincomplianceCount As Integer
        Dim TotaloutofcomplianceCount As Integer
        Dim extracount As Integer
        '  Dim extracount2 As Integer
        Dim extraOptincount As Integer
        Dim extraOptOutCount As Integer
        Dim TotalResponsecount As Integer
        Dim year As Integer = CInt(cboYear.SelectedItem)
        txtESYear.Text = cboYear.SelectedItem
        Dim ESYear As String = txtESYear.Text

        Dim removedFacilitiescount As Integer
        Dim mailoutNonresponderscount As Integer
        Dim extraNonresponderscount As Integer


        Dim intESyear As Integer = CInt(ESYear)
        Dim deadline As String = "15-Jun-" & year + 1

        Try
            Try
                SQL = "select count(*) as ESMailoutCount " & _
                "from " & DBNameSpace & ".esmailout, " & DBNameSpace & ".ESSCHEMA " & _
                "where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR = " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR(+) " & _
                "and " & DBNameSpace & ".esmailout.STRESYEAR = '" & ESYear & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtESMailOutCount.Text = dr.Item(ESMailoutCount)
                End While
                dr.Close()

                SQL = "select count(*) as ResponseCount " & _
                "from " & DBNameSpace & ".esmailout, " & DBNameSpace & ".ESSCHEMA " & _
                "where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR = " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
                "and " & DBNameSpace & ".ESSCHEMA.STROPTOUT is not NULL " & _
                "and " & DBNameSpace & ".esmailout.STRESYEAR = '" & ESYear & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtResponseCount.Text = dr.Item(ResponseCount)
                End While
                dr.Close()

                SQL = "select count(*) as TotaloptinCount " & _
                "from " & DBNameSpace & ".ESSchema " & _
                "where " & DBNameSpace & ".ESSchema.intESYEAR = '" & intESyear & "'" & _
                " and " & DBNameSpace & ".ESSchema.strOptOut = 'NO'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                While dr.Read()
                    txtTotalOptInCount.Text = dr.Item(TotaloptinCount)
                End While
                dr.Close()

                SQL = "select count(*) as TotaloptOutCount " & _
                "from " & DBNameSpace & ".ESSchema " & _
                "where " & DBNameSpace & ".ESSchema.intESYEAR = '" & intESyear & "' " & _
                "and " & DBNameSpace & ".ESSchema.strOptOut = 'YES'"

                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                While dr.Read()
                    txtTotalOptOutCount.Text = dr.Item(TotaloptoutCount)
                End While
                dr.Close()

                SQL = "select count(*) as TotalinincomplianceCount " & _
                "from " & DBNameSpace & ".ESSchema " & _
                "where " & DBNameSpace & ".ESSchema.intESYEAR = '" & intESyear & "'" & _
                " and to_date(" & DBNameSpace & ".ESSchema.STRDATEFIRSTCONFIRM) < = '" & deadline & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read()
                    txtTotalincompliance.Text = dr.Item(TotalinincomplianceCount)
                End While
                dr.Close()

                SQL = "select count(*) as TotaloutofcomplianceCount " & _
                "from " & DBNameSpace & ".ESSchema " & _
                "where " & DBNameSpace & ".ESSchema.intESYEAR = '" & intESyear & "'" & _
                " and to_date(" & DBNameSpace & ".ESSchema.STRDATEFIRSTCONFIRM) > '" & deadline & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read()
                    txtTotaloutofcompliance.Text = dr.Item(TotaloutofcomplianceCount)
                End While
                dr.Close()

                SQL = "select count(*) as MailOutOptInCount " & _
                "from " & DBNameSpace & ".ESSchema, " & DBNameSpace & ".ESMailout " & _
                "where " & DBNameSpace & ".ESMAILOUT.strESYEAR = '" & ESYear & "' " & _
                " and " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR = " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR(+) " & _
                " and " & DBNameSpace & ".ESSchema.strOptOut = 'NO'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtMailoutOptin.Text = dr.Item(MailOutOptInCount)
                End While
                dr.Close()

                SQL = "select count(*) as MailOutOptOutCount " & _
                "from " & DBNameSpace & ".ESSchema, " & DBNameSpace & ".ESMailout " & _
                "where " & DBNameSpace & ".ESMAILOUT.strESYEAR = '" & ESYear & "'" & _
                " and " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR = " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR(+) " & _
                " and " & DBNameSpace & ".ESSchema.strOptOut = 'YES'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtMailOutOptOut.Text = dr.Item(mailoutOptOutCount)
                End While
                dr.Close()

            Catch ex As Exception
                MsgBox("That Prefix is not in the db" + vbCrLf + ex.ToString())
            End Try


            SQL = "select count(*) as Nonresponsecount " & _
             "from " & DBNameSpace & ".ESSCHEMA " & _
             "where " & DBNameSpace & ".ESSCHEMA.intESYEAR = '" & ESYear & "'" & _
             " and " & DBNameSpace & ".ESSchema.strOptOut is NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read()
                txtNonResponseCount.Text = dr.Item(Nonresponsecount)
            End While
            dr.Close()

            SQL = "select count(*) as removedFacilitiescount " & _
          "from " & DBNameSpace & ".ESSchema , " & DBNameSpace & ".esmailout " & _
          "where " & DBNameSpace & ".esMailOut.STRESYEAR = '" & ESYear & "'" & _
            "and " & DBNameSpace & ".esmailout.STRAIRSYEAR = " & DBNameSpace & ".ESSchema.STRAIRSYEAR(+) " & _
          " and " & DBNameSpace & ".ESSchema.STRAIRSYEAR is NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read()
                txtESremovedFacilities.Text = dr.Item(removedFacilitiescount)
            End While
            dr.Close()

            SQL = "select count(*) as extraNonresponderscount " & _
           "from " & DBNameSpace & ".ESSchema " & _
           " where  not exists (select * from " & DBNameSpace & ".ESMAILOUT " & _
                " where " & DBNameSpace & ".ESSchema.STRAIRSNUMBER = " & DBNameSpace & ".ESMAILOUT.STRAIRSNUMBER" & _
                " and ESSchema.INTESYEAR = ESMAILOUT.strESYEAR) " & _
                " and " & DBNameSpace & ".ESSchema.INTESYEAR = '" & ESYear & "' " & _
                " and " & DBNameSpace & ".ESSchema.STROPTOUT is null"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read()
                txtESextranonresponder.Text = dr.Item(extraNonresponderscount)
            End While
            dr.Close()

            SQL = "select count(*) as mailoutNonresponderscount " & _
          "from  " & DBNameSpace & ".esmailout, " & DBNameSpace & ".ESSchema " & _
            "where " & DBNameSpace & ".esmailout.strESYEAR = '" & ESYear & "' " & _
            "and " & DBNameSpace & ".esmailout.STRAIRSYEAR = " & DBNameSpace & ".ESSchema.STRAIRSYEAR(+) " & _
            "and " & DBNameSpace & ".ESSchema.strOptOut is NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read()
                txtESmailoutNonResponder.Text = dr.Item(mailoutNonresponderscount)
            End While
            dr.Close()

            SQL = "select count(*) as ExtraCount " & _
            "from (Select " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, " & _
            "" & DBNameSpace & ".ESMAILOUT.STRAIRSYear AS MailoutAIRS " & _
            "From " & DBNameSpace & ".ESMailout, " & DBNameSpace & ".ESSCHEMA" & _
            " Where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "AND " & DBNameSpace & ".esschema.INTESYEAR= '" & intESyear & "' " & _
            "AND " & DBNameSpace & ".ESSCHEMA.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & _
            "" & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR = SchemaAIRS " & _
            "AND MailoutAIRS is NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtESextraResponders.Text = dr.Item(extracount)
                txtextraResponse.Text = dr.Item(extracount)
            End While
            dr.Close()

            '   SQL = "select count(*) as ExtraCount2 " & _
            '"from (Select " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, " & _
            '"" & DBNameSpace & ".ESMAILOUT.STRAIRSYear AS MailoutAIRS " & _
            '"From " & DBNameSpace & ".ESMailout, " & DBNameSpace & ".ESSCHEMA" & _
            '" Where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            '"AND " & DBNameSpace & ".esschema.INTESYEAR= '" & intESyear & "' " & _
            '"AND " & DBNameSpace & ".ESSCHEMA.STROPTOUT IS NOT NULL) " & _
            '"dt_NotInMailout, " & _
            '"" & DBNameSpace & ".ESSCHEMA " & _
            '"Where " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR = SchemaAIRS " & _
            '"AND MailoutAIRS is NULL"

            '   cmd = New OracleCommand(SQL, conn)
            '   If conn.State = ConnectionState.Closed Then
            '       conn.Open()
            '   End If
            '   dr = cmd.ExecuteReader

            '   While dr.Read()
            '       txtESextraResponders.Text = dr.Item(extracount2)
            '   End While
            '   dr.Close()

            SQL = "select count(*) as ExtraOptinCount " & _
            "from (Select " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, " & _
            "" & DBNameSpace & ".ESMAILOUT.STRAIRSYear AS MailoutAIRS " & _
            "From " & DBNameSpace & ".ESMailout, " & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "AND " & DBNameSpace & ".esschema.INTESYEAR= '" & intESyear & "' " & _
            "AND " & DBNameSpace & ".ESSCHEMA.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & _
            "" & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR = SchemaAIRS " & _
            "AND MailoutAIRS is NULL " & _
            "and " & DBNameSpace & ".ESSCHEMA.STROPTOUT='NO'"

            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtExtraOptin.Text = dr.Item(extraOptincount)
            End While
            dr.Close()

            SQL = "select count(*) as ExtraOptOUTCount " & _
            "from (Select " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR AS SchemaAIRS, " & _
            "" & DBNameSpace & ".ESMAILOUT.STRAIRSYear AS MailoutAIRS " & _
            "From " & DBNameSpace & ".ESMailout, " & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "AND " & DBNameSpace & ".esschema.INTESYEAR= '" & intESyear & "' " & _
            "AND " & DBNameSpace & ".ESSCHEMA.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & _
            "" & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR = SchemaAIRS " & _
            "AND MailoutAIRS is NULL " & _
            "and " & DBNameSpace & ".ESSCHEMA.STROPTOUT='YES'"

            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read()
                txtExtraOptout.Text = dr.Item(extraOptOutCount)
            End While
            dr.Close()

            SQL = "select count(*) as TotalResponsecount " & _
            "from " & DBNameSpace & ".ESSchema " & _
            "where " & DBNameSpace & ".ESSchema.intESYEAR = '" & intESyear & "'" & _
            " and " & DBNameSpace & ".ESSchema.strOptOut is not NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtTotalResponse.Text = dr.Item(TotalResponsecount)
            End While
            dr.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try

    End Sub
    Private Sub findESMailOut()
        Dim AirsNo As String = txtESAIRSNo2.Text
        Dim ESyear As String = txtESYear.Text

        Try
            SQL = "SELECT * " & _
                  "from " & DBNameSpace & ".esMailOut " & _
                  "where STRAIRSNUMBER = '" & AirsNo & "' " & _
                  "and STRESYEAR = '" & ESyear & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr("STRFACILITYNAME")) Then
                    txtESFacilityName.Text = ""
                Else
                    txtESFacilityName.Text = dr("STRFACILITYNAME")
                End If
                If IsDBNull(dr("STRCONTACTPREFIX")) Then
                    txtESprefix.Text = ""
                Else
                    txtESprefix.Text = dr("STRCONTACTPREFIX")
                End If
                If IsDBNull(dr("STRCONTACTFIRSTNAME")) Then
                    txtESFirstName.Text = ""
                Else
                    txtESFirstName.Text = dr("STRCONTACTFIRSTNAME")
                End If
                If IsDBNull(dr("STRCONTACTLASTNAME")) Then
                    txtESLastName.Text = ""
                Else
                    txtESLastName.Text = dr("STRCONTACTLASTNAME")
                End If
                If IsDBNull(dr("STRCONTACTCOMPANYNAME")) Then
                    txtEScompanyName.Text = ""
                Else
                    txtEScompanyName.Text = dr("STRCONTACTCOMPANYNAME")
                End If
                If IsDBNull(dr("STRCONTACTADDRESS1")) Then
                    txtcontactAddress1.Text = ""
                Else
                    txtcontactAddress1.Text = dr("STRCONTACTADDRESS1")
                End If
                If IsDBNull(dr("STRCONTACTADDRESS2")) Then
                    txtcontactAddress2.Text = ""
                Else
                    txtcontactAddress2.Text = dr("STRCONTACTADDRESS2")
                End If
                If IsDBNull(dr("STRCONTACTCITY")) Then
                    txtcontactCity.Text = ""
                Else
                    txtcontactCity.Text = dr("STRCONTACTCITY")
                End If
                If IsDBNull(dr("STRCONTACTSTATE")) Then
                    txtcontactState.Text = ""
                Else
                    txtcontactState.Text = dr("STRCONTACTSTATE")
                End If
                If IsDBNull(dr("STRCONTACTZIPCODE")) Then
                    txtcontactZipCode.Text = ""
                Else
                    txtcontactZipCode.Text = dr("STRCONTACTZIPCODE")
                End If
                If IsDBNull(dr("STRCONTACTEMAIL")) Then
                    txtcontactEmail.Text = ""
                Else
                    txtcontactEmail.Text = dr("STRCONTACTEMAIL")
                End If
            End While

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try

    End Sub
    Private Sub findESData()
        Dim AirsNo As String = txtESAirsNo.Text
        Dim ESyear As String = cboYear.SelectedItem
        Dim intESyear As Integer = CInt(ESyear)
        Try

            SQL = "SELECT * " & _
            "from " & DBNameSpace & ".esschema " & _
            "where STRAIRSNUMBER = '" & AirsNo & "' " & _
            "and INTESYEAR = '" & intESyear & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr("STRAIRSNUMBER")) Then
                    txtESAirsNo.Text = ""
                Else
                    txtESAirsNo.Text = dr("STRAIRSNUMBER")
                End If
                If IsDBNull(dr("STRFACILITYNAME")) Then
                    txtFACILITYNAME.Text = ""
                Else
                    txtFACILITYNAME.Text = dr("STRFACILITYNAME")
                End If
                If IsDBNull(dr("STRFACILITYADDRESS")) Then
                    txtFACILITYADDRESS.Text = ""
                Else
                    txtFACILITYADDRESS.Text = dr("STRFACILITYADDRESS")
                End If
                If IsDBNull(dr("STRFACILITYCITY")) Then
                    txtFACILITYCITY.Text = ""
                Else
                    txtFACILITYCITY.Text = dr("STRFACILITYCITY")
                End If
                If IsDBNull(dr("STRFACILITYSTATE")) Then
                    txtFACILITYSTATE.Text = ""
                Else
                    txtFACILITYSTATE.Text = dr("STRFACILITYSTATE")
                End If
                If IsDBNull(dr("STRFACILITYZIP")) Then
                    txtFACILITYZIP.Text = ""
                Else
                    txtFACILITYZIP.Text = dr("STRFACILITYZIP")
                End If
                If IsDBNull(dr("STRCOUNTY")) Then
                    txtCOUNTY.Text = ""
                Else
                    txtCOUNTY.Text = dr("STRCOUNTY")
                End If
                If IsDBNull(dr("DBLXCOORDINATE")) Then
                    txtXCOORDINATE.Text = ""
                Else
                    txtXCOORDINATE.Text = dr("DBLXCOORDINATE")
                End If
                If IsDBNull(dr("DBLYCOORDINATE")) Then
                    txtYCOORDINATE.Text = ""
                Else
                    txtYCOORDINATE.Text = dr("DBLYCOORDINATE")
                End If
                If IsDBNull(dr("STRHORIZONTALCOLLECTIONCODE")) Then
                    txtHORIZONTALCOLLECTIONCODE.Text = ""
                Else
                    txtHORIZONTALCOLLECTIONCODE.Text = dr("STRHORIZONTALCOLLECTIONCODE")
                End If
                If IsDBNull(dr("STRHORIZONTALACCURACYMEASURE")) Then
                    txtHORIZONTALACCURACYMEASURE.Text = ""
                Else
                    txtHORIZONTALACCURACYMEASURE.Text = dr("STRHORIZONTALACCURACYMEASURE")
                End If
                If IsDBNull(dr("STRHORIZONTALREFERENCECODE")) Then
                    txtHORIZONTALREFERENCECODE.Text = ""
                Else
                    txtHORIZONTALREFERENCECODE.Text = dr("STRHORIZONTALREFERENCECODE")
                End If
                If IsDBNull(dr("STRCONTACTCOMPANY")) Then
                    txtCompany.Text = ""
                Else
                    txtCompany.Text = dr("STRCONTACTCOMPANY")
                End If
                If IsDBNull(dr("STRCONTACTTITLE")) Then
                    txtTitle.Text = ""
                Else
                    txtTitle.Text = dr("STRCONTACTTITLE")
                End If
                If IsDBNull(dr("STRCONTACTPHONENUMBER")) Then
                    txtPhone.Text = ""
                Else
                    txtPhone.Text = dr("STRCONTACTPHONENUMBER")
                End If
                If IsDBNull(dr("STRCONTACTFAXNUMBER")) Then
                    txtFax.Text = ""
                Else
                    txtFax.Text = dr("STRCONTACTFAXNUMBER")
                End If
                If IsDBNull(dr("STRCONTACTFIRSTNAME")) Then
                    txtESContactFirstName.Text = ""
                Else
                    txtESContactFirstName.Text = dr("STRCONTACTFIRSTNAME")
                End If
                If IsDBNull(dr("STRCONTACTLASTNAME")) Then
                    txtESContactLastName.Text = ""
                Else
                    txtESContactLastName.Text = dr("STRCONTACTLASTNAME")
                End If
                If IsDBNull(dr("STRCONTACTADDRESS1")) Then
                    txtAddress1.Text = ""
                Else
                    txtAddress1.Text = dr("STRCONTACTADDRESS1")
                End If
                If IsDBNull(dr("STRCONTACTADDRESS2")) Then
                    txtAddress2.Text = ""
                Else
                    txtAddress2.Text = dr("STRCONTACTADDRESS2")
                End If
                If IsDBNull(dr("STRCONTACTCITY")) Then
                    txtCity.Text = ""
                Else
                    txtCity.Text = dr("STRCONTACTCITY")
                End If
                If IsDBNull(dr("STRCONTACTSTATE")) Then
                    txtState.Text = ""
                Else
                    txtState.Text = dr("STRCONTACTSTATE")
                End If
                If IsDBNull(dr("STRCONTACTZIP")) Then
                    txtZip.Text = ""
                Else
                    txtZip.Text = dr("STRCONTACTZIP")
                End If
                If IsDBNull(dr("STRCONTACTEMAIL")) Then
                    txtESEmail.Text = ""
                Else
                    txtESEmail.Text = dr("STRCONTACTEMAIL")
                End If
                If IsDBNull(dr("DBLVOCEMISSION")) Then
                    txtVOCEmission.Text = ""
                Else
                    txtVOCEmission.Text = dr("DBLVOCEMISSION")
                    If txtVOCEmission.Text = "-1" Then
                        txtVOCEmission.Text = "No Value"
                    End If
                End If
                If IsDBNull(dr("DBLNOXEMISSION")) Then
                    txtNOXEmission.Text = ""
                Else
                    txtNOXEmission.Text = dr("DBLNOXEMISSION")
                    If txtNOXEmission.Text = "-1" Then
                        txtNOXEmission.Text = "No Value"
                    End If
                End If
                If IsDBNull(dr("STRCONFIRMATIONNBR")) Then
                    txtConfirmationNbr.Text = ""
                    txtConfirmationNumber.Text = ""
                Else
                    txtConfirmationNbr.Text = dr("STRCONFIRMATIONNBR")
                    txtConfirmationNumber.Text = dr("STRCONFIRMATIONNBR")
                End If
                If IsDBNull(dr("STRDATEFIRSTCONFIRM")) Then
                    txtFirstConfirmedDate.Text = ""
                Else
                    txtFirstConfirmedDate.Text = dr("STRDATEFIRSTCONFIRM")
                End If
            End While

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try

    End Sub
    Sub ExportEStoExcel()
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        ' Dim ExcelApp As New Excel.Application
        Dim i As Integer
        Dim j As Integer

        Try
            If dgvESDataCount.RowCount <> 0 Then
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    For i = 0 To dgvESDataCount.ColumnCount - 1
                        .Cells(1, i + 1) = dgvESDataCount.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvESDataCount.ColumnCount - 1
                        'For j = 0 To dgvESDataCount.RowCount - 2
                        For j = 0 To dgvESDataCount.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvESDataCount.Item(i, j).Value.ToString
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
    Private Sub dgvESDataCount_MouseUp1(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvESDataCount.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvESDataCount.HitTest(e.X, e.Y)

        Try
            If dgvESDataCount.RowCount > 0 Then
                'If dgvESDataCount.Columns(1).HeaderText <> "Facility Name" Then
                If dgvESDataCount.ColumnCount > 2 Then
                    If dgvESDataCount.RowCount > 0 And hti.RowIndex <> -1 Then
                        If dgvESDataCount.Columns(0).HeaderText = "Airs No." Then
                            If IsDBNull(dgvESDataCount(0, hti.RowIndex).Value) Then

                            Else
                                ClearMailOut()
                                txtESAIRSNo2.Text = dgvESDataCount(0, hti.RowIndex).Value
                                txtESFacilityName.Text = dgvESDataCount(1, hti.RowIndex).Value
                                findESMailOut()
                            End If
                        Else
                            If dgvESDataCount.Columns(0).HeaderText = "Airs No." Then
                                If IsDBNull(dgvESDataCount(0, hti.RowIndex).Value) Then
                                    txtESAIRSNo2.Text = dgvESDataCount(0, hti.RowIndex).Value
                                    txtESFacilityName.Text = dgvESDataCount(1, hti.RowIndex).Value
                                Else
                                    ClearMailOut()
                                    findESMailOut()
                                    txtESAIRSNo2.Text = dgvESDataCount(0, hti.RowIndex).Value
                                End If
                            End If
                        End If
                        If dgvESDataCount.Columns(3).HeaderText = "Confirmation Number" Then
                            If IsDBNull(dgvESDataCount(3, hti.RowIndex).Value) Then
                                txtESAirsNo.Text = dgvESDataCount(0, hti.RowIndex).Value
                                txtFACILITYNAME.Text = dgvESDataCount(1, hti.RowIndex).Value
                            Else
                                ClearMailOut()
                                txtESAirsNo.Text = dgvESDataCount(0, hti.RowIndex).Value
                                txtFACILITYNAME.Text = dgvESDataCount(1, hti.RowIndex).Value
                                txtConfirmationNumber.Text = dgvESDataCount(3, hti.RowIndex).Value
                                findESData()
                            End If
                        Else
                            If dgvESDataCount.Columns(3).HeaderText = "Confirmation Number" Then
                                If IsDBNull(dgvESDataCount(3, hti.RowIndex).Value) Then
                                    txtESAirsNo.Text = dgvESDataCount(0, hti.RowIndex).Value
                                    txtFACILITYNAME.Text = dgvESDataCount(1, hti.RowIndex).Value
                                Else
                                    ClearMailOut()
                                    txtESAirsNo.Text = dgvESDataCount(0, hti.RowIndex).Value
                                    txtFACILITYNAME.Text = dgvESDataCount(1, hti.RowIndex).Value
                                    findESData()
                                    txtConfirmationNumber.Text = dgvESDataCount(3, hti.RowIndex).Value
                                End If
                            End If
                        End If
                    End If
                Else
                    ClearMailOut()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try

    End Sub
    Private Sub lblViewMailOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewMailOut.LinkClicked
        txtESYear.Text = cboYear.SelectedItem

        Try

            Dim year As String = txtESYear.Text

            SQL = "SELECT STRAIRSNUMBER, " & _
            "STRFACILITYNAME, " & _
            "STRCONTACTFIRSTNAME, " & _
            "STRCONTACTLASTNAME, " & _
            "STRCONTACTCOMPANYname, " & _
            "STRCONTACTADDRESS1, " & _
            "STRCONTACTCITY, " & _
            "STRCONTACTSTATE, " & _
            "STRCONTACTZIPCODE, " & _
            "STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".esMailOut " & _
            "where STRESYEAR = '" & year & "' " & _
            "order by STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvESDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTCOMPANYname").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
            dgvESDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 5
            dgvESDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvESDataCount.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvESDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvESDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvESDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvESDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 9

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally


        End Try

    End Sub
    Private Sub lblViewOptin_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewTotalOptin.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM, " & _
            "" & DBNameSpace & ".esSchema.DBLVOCEMISSION, " & _
           "" & DBNameSpace & ".esSchema.DBLNOXEMISSION, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR " & _
            "from " & DBNameSpace & ".esSchema, " & DBNameSpace & ".esmailout " & _
            "where " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".esSchema.STROPTOUT = 'NO'" & _
            "and " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("DBLVOCEMISSION").HeaderText = "VOC Emmissions"
            dgvESDataCount.Columns("DBLVOCEMISSION").DisplayIndex = 3
            dgvESDataCount.Columns("DBLNOXEMISSION").HeaderText = "NOX Emmissions"
            dgvESDataCount.Columns("DBLNOXEMISSION").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 5

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub lblViewOptOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewTotalOptOut.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR " & _
            "from " & DBNameSpace & ".esSchema, " & DBNameSpace & ".esmailout  " & _
            "where " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "and " & DBNameSpace & ".esSchema.STROPTOUT = 'YES'" & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewOutofcompliance_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewOutofcompliance.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)
            Dim deadline As String = "15-Jun-2007"
            deadline = "15-Jun-" & txtESYear.Text + 1

            SQL = "SELECT airbranch.esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STROPTOUT, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTCOMPANY, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTADDRESS1, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTCITY, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTSTATE, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTZIP, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTEMAIL, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTPHONENUMBER " & _
            "from " & DBNameSpace & ".esSchema " & _
            "where intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM is not NULL " & _
            "and to_date(" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM) > '" & deadline & "' " & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STROPTOUT").HeaderText = "OptOut Status"
            dgvESDataCount.Columns("STROPTOUT").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvESDataCount.Columns("STRCONTACTCOMPANY").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTCOMPANY").DisplayIndex = 6
            dgvESDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Street Address"
            dgvESDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 7
            dgvESDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvESDataCount.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvESDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvESDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvESDataCount.Columns("STRCONTACTZIP").HeaderText = "Zip"
            dgvESDataCount.Columns("STRCONTACTZIP").DisplayIndex = 10
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 11
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").HeaderText = "Phone No."
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").DisplayIndex = 12

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewINCompliance_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewINCompliance.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)
            Dim deadline As String = "15-Jun-" & intYear + 1

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR " & _
            "from " & DBNameSpace & ".esSchema " & _
            "where " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
             "and " & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM is not NULL " & _
            "and to_date(" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM) <= '" & deadline & "' " & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "Date First Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewESMailOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewESMailOut.LinkClicked
        Try

            Dim year As String = txtESYear.Text
            txtESYear.Text = cboYear.SelectedItem


            SQL = "SELECT esMailOut.STRAIRSNUMBER, " & _
            "esMailOut.STRFACILITYNAME, " & _
            "esMailOut.STRCONTACTFIRSTNAME, " & _
            "esMailOut.STRCONTACTLASTNAME, " & _
            "esMailOut.STRCONTACTCOMPANYname, " & _
            "esMailOut.STRCONTACTADDRESS1, " & _
            "esMailOut.STRCONTACTCITY, " & _
            "esMailOut.STRCONTACTSTATE, " & _
            "esMailOut.STRCONTACTZIPCODE, " & _
            "esMailOut.STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".esMailOut " & _
            "where STRESYEAR = '" & year & "' " & _
            "order by STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvESDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTCOMPANYname").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
            dgvESDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 5
            dgvESDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvESDataCount.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvESDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvESDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvESDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvESDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 9

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewESData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewESData.LinkClicked
        Dim year As Integer = CInt(cboYear.SelectedItem)
        Try

            dsES = New DataSet

            SQL = "SELECT esSchema.STRAIRSNUMBER, " & _
            "esSchema.STRFACILITYNAME, " & _
            "case " & _
            "when DBLVOCEMISSION= '-1' then 'No Value' " & _
            "else to_char(DBLVOCEMISSION) " & _
            "end DBLVOCEMISSION, " & _
            "esSchema.STRCONFIRMATIONNBR, " & _
            "case " & _
            "when DBLNOXEMISSION = '-1' then 'No Value' " & _
            "else to_char(DBLNOXEMISSION) " & _
            "end DBLNOXEMISSION, " & _
            "esSchema.STRDATEFIRSTCONFIRM " & _
            "from " & DBNameSpace & ".esSchema " & _
            "where esSchema.intESyear = '" & year & "' " & _
            "order by esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("DBLVOCEMISSION").HeaderText = "VOC Emissions"
            dgvESDataCount.Columns("DBLVOCEMISSION").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3
            dgvESDataCount.Columns("DBLNOXEMISSION").HeaderText = "NOX Emissions"
            dgvESDataCount.Columns("DBLNOXEMISSION").DisplayIndex = 4
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 5

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub lblViewNonResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewNonResponse.LinkClicked
        Try

            Dim year As String = txtESYear.Text

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME " & _
            "from " & DBNameSpace & ".esMailOut, " & DBNameSpace & ".ESSCHEMA " & _
            "where " & DBNameSpace & ".esSchema.INTESYEAR = '" & year & "'" & _
            "and " & DBNameSpace & ".esSchema.strOPTOUT is NULL " & _
            "and " & DBNameSpace & ".esmailout.STRAIRSYEAR = " & DBNameSpace & ".ESSchema.STRAIRSYEAR(+) " & _
            "order by " & DBNameSpace & ".esMailOut.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

            ' clearESData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblextraResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblextraResponse.LinkClicked
        Try

            Dim year As String = txtESYear.Text
            Dim intyear As Integer = Int(year)

            SQL = "SELECT dt_NotInMailout.SchemaAIRS, " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTCOMPANY, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTEMAIL, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTPHONENUMBER " & _
            "from (Select " & DBNameSpace & ".ESSCHEMA.STRAIRSNUMBER AS SchemaAIRS, " & _
            "" & DBNameSpace & ".ESMAILOUT.STRAIRSNUMBER AS MailoutAIRS" & _
            " From " & DBNameSpace & ".ESMailout, " & DBNameSpace & ".ESSCHEMA" & _
            " Where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "AND INTESYEAR=  '" & intyear & "' " & _
            "AND " & DBNameSpace & ".ESSCHEMA.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & _
            "" & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESSCHEMA.STRAIRSNUMBER = SchemaAIRS " & _
            "AND MailoutAIRS is NULL"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvESDataCount.Columns("STRCONTACTCOMPANY").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTCOMPANY").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 5
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").HeaderText = "Phone No."
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").DisplayIndex = 6

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

            'clearESData()
            'ClearMailOut()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewOptIn_LinkClicked_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewOptIn.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR " & _
            "from " & DBNameSpace & ".esSchema, " & DBNameSpace & ".esmailout " & _
            "where " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".esSchema.STROPTOUT = 'NO'" & _
            "and " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR = " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR(+) " & _
            "and " & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM is not NULL " & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewOptOut_LinkClicked_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewOptOut.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR " & _
            "from " & DBNameSpace & ".esSchema, " & DBNameSpace & ".esmailout " & _
            "where " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".esSchema.STROPTOUT = 'YES'" & _
            " and " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR = " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR(+) " & _
            "and " & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM is not NULL " & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewExtraOptOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewExtraOptOut.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR " & _
            "from (select " & DBNameSpace & ".esSchema.strairsyear as SchemaAIRS, " & _
            "" & DBNameSpace & ".esmailout.strairsyear as MailoutAIRS " & _
            "From " & DBNameSpace & ".ESMailout, " & DBNameSpace & ".ESSCHEMA " & _
            "where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "and " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".ESSCHEMA.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR = SchemaAIRS " & _
            "and MailoutAIRS is NULL " & _
            "and " & DBNameSpace & ".ESSCHEMA.STROPTOUT='YES'"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 1
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 0
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewExtraOptIn_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewExtraOptIn.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRDATEFIRSTCONFIRM, " & _
            "" & DBNameSpace & ".esSchema.STRCONFIRMATIONNBR " & _
            "from (select " & DBNameSpace & ".esSchema.strairsyear as SchemaAIRS, " & _
            "" & DBNameSpace & ".esmailout.strairsyear as MailoutAIRS " & _
            "From " & DBNameSpace & ".ESMailout, " & DBNameSpace & ".ESSCHEMA " & _
            "where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "and " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".ESSCHEMA.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR = SchemaAIRS " & _
            "and MailoutAIRS is NULL " & _
            "and " & DBNameSpace & ".ESSCHEMA.STROPTOUT='NO'"


            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").HeaderText = "First Date Confirmed"
            dgvESDataCount.Columns("STRDATEFIRSTCONFIRM").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").HeaderText = "Confirmation Number"
            dgvESDataCount.Columns("STRCONFIRMATIONNBR").DisplayIndex = 3

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub lblViewTotalResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewTotalResponse.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTCOMPANY, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTEMAIL, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTPHONENUMBER " & _
            "from " & DBNameSpace & ".esSchema " & _
            "where " & DBNameSpace & ".esSchema.intESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".esSchema.STROPTOUT is not NULL " & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvESDataCount.Columns("STRCONTACTCOMPANY").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTCOMPANY").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 5
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").HeaderText = "Phone Number"
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").DisplayIndex = 6

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

            clearESData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub AddESMailOut()
        Dim AirsNo As String = txtESAIRSNo2.Text
        Dim ESYear As String = txtESYear.Text
        Dim airsYear As String = AirsNo & ESYear
        Dim dr As OracleDataReader
        Dim ESFacilityName As String = txtESFacilityName.Text
        Dim ESPrefix As String = txtESprefix.Text
        Dim ESFirstName As String = txtESFirstName.Text
        Dim ESLastName As String = txtESLastName.Text
        Dim ESCompanyName As String = txtEScompanyName.Text
        Dim ESContactAddress1 As String = txtcontactAddress1.Text
        Dim ESContactAddress2 As String = txtcontactAddress2.Text
        Dim ESContactCity As String = txtcontactCity.Text
        Dim EScontactState As String = txtcontactState.Text
        Dim ESContactZip As String = txtcontactZipCode.Text
        Dim ESContactEmail As String = txtcontactEmail.Text

        Try
            SQL = "Insert into " & DBNameSpace & ".ESMailOut(STRAIRSYEAR, " & _
            "STRAIRSNUMBER, " & _
            "STRFACILITYNAME, " & _
            "STRCONTACTPREFIX, " & _
            "STRCONTACTFIRSTNAME, " & _
            "STRCONTACTLASTNAME, " & _
            "STRCONTACTCOMPANYNAME, " & _
            "STRCONTACTADDRESS1, " & _
            "STRCONTACTADDRESS2, " & _
            "STRCONTACTCITY, " & _
            "STRCONTACTSTATE, " & _
            "STRCONTACTZIPCODE, " & _
            "STRESYEAR, " & _
            "STRCONTACTEMAIL) " & _
            "values (" & _
            "'" & Replace(airsYear, "'", "''") & "', " & _
            "'" & Replace(AirsNo, "'", "''") & "', " & _
            "'" & Replace(ESFacilityName, "'", "''") & "', " & _
            "'" & Replace(ESPrefix, "'", "''") & "', " & _
            "'" & Replace(ESFirstName, "'", "''") & "', " & _
            "'" & Replace(ESLastName, "'", "''") & "', " & _
            "'" & Replace(ESCompanyName, "'", "''") & "', " & _
            "'" & Replace(ESContactAddress1, "'", "''") & "', " & _
            "'" & Replace(ESContactAddress2, "'", "''") & "', " & _
            "'" & Replace(ESContactCity, "'", "''") & "', " & _
            "'" & Replace(EScontactState, "'", "''") & "', " & _
            "'" & Replace(ESContactZip, "'", "''") & "', " & _
            "'" & Replace(ESYear, "'", "''") & "', " & _
            "'" & Replace(ESContactEmail, "'", "''") & "' " & _
            " )"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub SaveESMailOut()
        Dim AirsNo As String = txtESAIRSNo2.Text
        Dim ESFacilityName As String = txtESFacilityName.Text
        Dim ESPrefix As String = txtESprefix.Text
        Dim ESFirstName As String = txtESFirstName.Text
        Dim ESLastName As String = txtESLastName.Text
        Dim ESCompanyName As String = txtEScompanyName.Text
        Dim ESContactAddress1 As String = txtcontactAddress1.Text
        Dim ESContactAddress2 As String = txtcontactAddress2.Text
        Dim ESYear As String = txtESYear.Text
        Dim ESContactCity As String = txtcontactCity.Text
        Dim EScontactState As String = txtcontactState.Text
        Dim ESContactZip As String = txtcontactZipCode.Text
        Dim ESContactEmail As String = txtcontactEmail.Text
        Dim airsYear As String = AirsNo & ESYear

        Try
            SQL = "Select strAIRSYear " & _
            "from " & DBNameSpace & ".EsMailOut " & _
            "where STRAIRSYEAR = '" & airsYear & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                SQL = "update " & DBNameSpace & ".ESMailOut set " & _
                "" & DBNameSpace & ".ESMailOut.STRCONTACTPREFIX = '" & ESPrefix & "', " & _
                "" & DBNameSpace & ".ESMailOut.STRCONTACTFIRSTNAME = '" & ESFirstName & "', " & _
                "" & DBNameSpace & ".ESMailOut.STRCONTACTLASTNAME = '" & ESLastName & "', " & _
                "" & DBNameSpace & ".ESMailOut.STRCONTACTCOMPANYNAME = '" & ESCompanyName & "', " & _
                "" & DBNameSpace & ".ESMailOut.STRCONTACTADDRESS1 = '" & ESContactAddress1 & "', " & _
                "" & DBNameSpace & ".ESMailOut.STRCONTACTADDRESS2 = '" & ESContactAddress2 & "', " & _
                "" & DBNameSpace & ".ESMailOut.STRCONTACTCITY = '" & ESContactCity & "', " & _
                "" & DBNameSpace & ".ESMailOut.STRCONTACTSTATE = '" & EScontactState & "', " & _
                "" & DBNameSpace & ".ESMailOut.STRCONTACTZIPCODE = '" & ESContactZip & "', " & _
                "" & DBNameSpace & ".ESMailOut.STRCONTACTEMAIL = '" & ESContactEmail & "'" & _
                "where ESMailOut.STRAIRSNUMBER = '" & AirsNo & "' "

                MsgBox("your info is updated!")

            Else
                SQL = "Insert into " & DBNameSpace & ".ESMailOut " & _
                "(STRAIRSYEAR, " & _
                "STRAIRSNUMBER, " & _
                "STRFACILITYNAME, " & _
                "STRCONTACTPREFIX, " & _
                "STRCONTACTFIRSTNAME, " & _
                "STRCONTACTLASTNAME, " & _
                "STRCONTACTCOMPANYNAME, " & _
                "STRCONTACTADDRESS1, " & _
                "STRCONTACTADDRESS2, " & _
                "STRCONTACTCITY, " & _
                "STRCONTACTSTATE, " & _
                "STRCONTACTZIPCODE, " & _
                "STRESYEAR, " & _
                "STRCONTACTEMAIL) " & _
                "values (" & _
                "'" & Replace(airsYear, "'", "''") & "', " & _
                "'" & Replace(AirsNo, "'", "''") & "', " & _
                "'" & Replace(ESFacilityName, "'", "''") & "', " & _
                "'" & Replace(ESPrefix, "'", "''") & "', " & _
                "'" & Replace(ESFirstName, "'", "''") & "', " & _
                "'" & Replace(ESLastName, "'", "''") & "', " & _
                "'" & Replace(ESCompanyName, "'", "''") & "', " & _
                "'" & Replace(ESContactAddress1, "'", "''") & "', " & _
                "'" & Replace(ESContactAddress2, "'", "''") & "', " & _
                "'" & Replace(ESContactCity, "'", "''") & "', " & _
                "'" & Replace(EScontactState, "'", "''") & "', " & _
                "'" & Replace(ESContactZip, "'", "''") & "', " & _
                "'" & Replace(ESYear, "'", "''") & "', " & _
                "'" & Replace(ESContactEmail, "'", "''") & "' " & _
                " )"

                MsgBox("your info is added!")

            End If

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub DeleteESMailOut()
        Dim AirsNo As String = txtESAIRSNo2.Text
        Dim ESyear As String = txtESYear.Text

        Try
            SQL = "delete from " & DBNameSpace & ".ESMailOut " & _
            "where " & DBNameSpace & ".ESMailOut.STRAIRSNUMBER = '" & AirsNo & "' " & _
            "and " & DBNameSpace & ".ESMailOut.STRESYEAR = '" & ESyear & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub ClearMailOut()
        Try
            txtESAIRSNo2.Text = ""
            txtESFacilityName.Text = ""
            txtESprefix.Text = ""
            txtESFirstName.Text = ""
            txtESLastName.Text = ""
            txtEScompanyName.Text = ""
            txtcontactAddress1.Text = ""
            txtcontactAddress2.Text = ""
            txtcontactCity.Text = ""
            txtcontactState.Text = ""
            txtcontactZipCode.Text = ""
            txtcontactEmail.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub clearESData()
        Try
            txtESAirsNo.Text = ""
            txtFACILITYNAME.Text = ""
            txtFACILITYADDRESS.Text = ""
            txtFACILITYCITY.Text = ""
            txtFACILITYSTATE.Text = ""
            txtFACILITYZIP.Text = ""
            txtCOUNTY.Text = ""
            txtXCOORDINATE.Text = ""
            txtYCOORDINATE.Text = ""
            txtHORIZONTALCOLLECTIONCODE.Text = ""
            txtHORIZONTALACCURACYMEASURE.Text = ""
            txtHORIZONTALREFERENCECODE.Text = ""
            txtCompany.Text = ""
            txtTitle.Text = ""
            txtPhone.Text = ""
            txtFax.Text = ""
            txtESContactFirstName.Clear()
            txtESContactLastName.Clear()
            txtAddress1.Text = ""
            txtAddress2.Text = ""
            txtCity.Text = ""
            txtState.Text = ""
            txtZip.Text = ""
            'txtEmail.Text = ""
            txtVOCEmission.Text = ""
            txtNOXEmission.Text = ""
            txtConfirmationNbr.Text = ""
            txtFirstConfirmedDate.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    'Private Sub UpdateESData()
    '    Dim ConfirmationNumber As String = txtConfirmationNumber.Text
    '    Dim ConfirmationNbr As String = txtConfirmationNbr.Text
    '    Dim dr As OracleDataReader
    '    Dim ESFacilityName As String = txtESFacilityName.Text
    '    Dim ESFacilityAddress As String = txtFACILITYADDRESS.Text
    '    Dim ESFacilityCity As String = txtFACILITYCITY.Text
    '    Dim ESFacilityState As String = txtFACILITYSTATE.Text
    '    Dim ESFacilityZip As String = txtFACILITYZIP.Text
    '    Dim ESFacilityCounty As String = txtCOUNTY.Text
    '    Dim XCOORDINATE As String = txtXCOORDINATE.Text
    '    Dim YCOORDINATE As String = txtYCOORDINATE.Text
    '    Dim HORIZONTALCOLLECTIONCODE As String = txtHORIZONTALCOLLECTIONCODE.Text
    '    Dim HORIZONTALACCURACYMEASURE As String = txtHORIZONTALACCURACYMEASURE.Text
    '    Dim HORIZONTALREFERENCECODE As String = txtHORIZONTALREFERENCECODE.Text
    '    Dim ESPrefix As String = txtESprefix.Text
    '    Dim ESContactPhoneNo As String = txtPhone.Text
    '    Dim ESContactFaxNo As String = txtFax.Text
    '    Dim ESFirstName As String = txtESContactFirstName.Text
    '    Dim ESLastName As String = txtESContactLastName.Text
    '    Dim ESCompanyName As String = txtCompany.Text
    '    Dim ESContactAddress1 As String = txtAddress1.Text
    '    Dim ESContactAddress2 As String = txtAddress2.Text
    '    Dim ESContactCity As String = txtCity.Text
    '    Dim EScontactState As String = txtState.Text
    '    Dim ESContactZip As String = txtZip.Text
    '    '  Dim ESContactEmail As String = txtEmail.Text
    '    Dim VOCEmission As String = txtVOCEmission.Text
    '    Dim NOXEmission As String = txtNOXEmission.Text
    '    Dim FirstDateConfimed As String = txtFirstConfirmedDate.Text

    '    Try
    '        SQL = "update " & DBNameSpace & ".esSchema " & _
    '        "set STRFACILITYADDRESS = '" & ESFacilityAddress & "', " & _
    '        "STRFACILITYCITY = '" & ESFacilityCity & "', " & _
    '        "STRFACILITYSTATE = '" & ESFacilityState & "', " & _
    '        "STRFACILITYZIP = '" & ESFacilityZip & "', " & _
    '        "STRCOUNTY = '" & ESFacilityCounty & "', " & _
    '        "DBLXCOORDINATE = '" & XCOORDINATE & "', " & _
    '        "DBLYCOORDINATE = '" & YCOORDINATE & "', " & _
    '        "STRHORIZONTALCOLLECTIONCODE = '" & HORIZONTALCOLLECTIONCODE & "', " & _
    '        "STRHORIZONTALACCURACYMEASURE = '" & HORIZONTALACCURACYMEASURE & "', " & _
    '        "STRHORIZONTALREFERENCECODE = '" & HORIZONTALREFERENCECODE & "', " & _
    '        "STRCONTACTFIRSTNAME = '" & ESFirstName & "', " & _
    '        "STRCONTACTLASTNAME = '" & ESLastName & "', " & _
    '        "STRCONTACTPREFIX = '" & ESPrefix & "', " & _
    '        "STRCONTACTCOMPANY = '" & ESCompanyName & "', " & _
    '        "STRCONTACTADDRESS1 = '" & ESContactAddress1 & "', " & _
    '        "STRCONTACTADDRESS2 = '" & ESContactAddress2 & "', " & _
    '        "STRCONTACTCITY = '" & ESContactCity & "', " & _
    '        "STRCONTACTSTATE = '" & EScontactState & "', " & _
    '        "STRCONTACTZIP = '" & ESContactZip & "', " & _
    '        "STRCONTACTPHONENUMBER = '" & ESContactPhoneNo & "', " & _
    '        "STRCONTACTFAXNUMBER = '" & ESContactFaxNo & "', " & _
    '        "STRCONTACTEMAIL = '" & ESContactEmail & "', " & _
    '        "DBLVOCEMISSION = '" & VOCEmission & "', " & _
    '        "DBLNOXEMISSION = '" & NOXEmission & "', " & _
    '        "STRCONFIRMATIONNBR = '" & ESContactZip & "', " & _
    '        "sTRDATEFIRSTCONFIRM = '" & FirstDateConfimed & "'" & _
    '        "where strConfirmationNbr = '" & ConfirmationNbr & "' "

    '        cmd = New OracleCommand(SQL, conn)
    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If
    '        dr = cmd.ExecuteReader
    '        dr.Close()

    '    Catch ex As Exception
    '        ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally

    '    End Try
    'End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveESMailOut()
            MsgBox("The info has been saved!")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnoutofcomplianceExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnoutofcomplianceExport.Click
        Try
            ExportEStoExcel()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnExporttoExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExporttoExcel.Click
        Try
            ExportEStoExcel()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnESUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnESUpdate.Click
        Try
            'UpdateESData()
            MsgBox("The info has been updated!")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnESDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnESDelete.Click
        Try
            DeleteESMailOut()
            ClearMailOut()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        MsgBox("The info has been deleted!")
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If txtFACILITYNAME.Text = "" Then
                MsgBox("You must select a Facility from the data grid view")
            Else
                PrintOut = Nothing
                If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
                PrintOut.txtPrintType.Text = "ES Print Out"
                PrintOut.txtSQLLine.Text = Me.txtConfirmationNumber.Text
                PrintOut.Show()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub GenerateESMailOut()
        Dim airsNumber As String
        Dim airsYear As String
        Dim FACILITYNAME As String = " "
        Dim CONTACTCOMPANYNAME As String = " "
        Dim CONTACTADDRESS1 As String = " "
        Dim CONTACTADDRESS2 As String = " "
        Dim CONTACTCITY As String = " "
        Dim CONTACTSTATE As String = " "
        Dim CONTACTZIPCODE As String = " "
        Dim CONTACTFIRSTNAME As String = " "
        Dim CONTACTLASTNAME As String = " "
        Dim CONTACTEMAIL As String = " "
        Dim ESYear As String = cboMailoutYear.SelectedItem
        Dim OperationalStatus As String = " "
        Dim FacilityClass As String = " "

        Try



            SQL = "Select strAirsNumber " & _
            "FROM " & DBNameSpace & ".ESmailOut " & _
            "where strESyear = '" & ESYear & "'"


            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            recExist = dr.Read

            dr.Close()

            If recExist = True Then
                MsgBox("That year is already being used." & vbCrLf & "If you want to use that year," & vbCrLf & "you must first delete that year from the database.")
            Else
                If cboMailoutYear.Text <> "" Then
                    If cboMailoutYear.Text.Length = 4 Then
                        SQL = "Select dt_EScontact.STRairsnumber, " & DBNameSpace & ".APBFacilityinformation.STRFACILITYNAME, " & _
                        "" & DBNameSpace & ".APBHEADERDATA.stroperationalstatus, " & DBNameSpace & ".APBHEADERDATA.STRCLASS, " & _
                        "(Case " & _
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRContactLastName " & _
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRContactLastName " & _
                        "Else 'N/A' " & _
                        "END) STRContactLastName, " & _
                        "(Case " & _
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRContactfirstName " & _
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRContactfirstName " & _
                        "Else 'N/A' " & _
                        "END) STRContactfirstName, " & _
                        "(Case " & _
                        "When dt_esContact.STRKEY='42' THEN dt_esContact.STRContactCompanyName " & _
                        "When dt_esContact.STRKEY Is Null THEN dt_PermitContact.STRContactCompanyName " & _
                        "END) STRContactCompanyName, " & _
                        "(Case " & _
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRContactEmail " & _
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRContactEmail " & _
                        "END) STRContactEmail, " & _
                        "(Case " & _
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRCONTACTPREFIX " & _
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTPREFIX " & _
                        "END) strCONTACTPREFIX, " & _
                        "(Case " & _
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRCONTACTADDRESS1 " & _
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTADDRESS1 " & _
                        "END) STRCONTACTADDRESS1, " & _
                        "(Case " & _
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRCONTACTCITY " & _
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTCITY " & _
                        "END) STRCONTACTCITY, " & _
                        "(Case " & _
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRCONTACTSTATE " & _
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTSTATE " & _
                        "END) STRCONTACTSTATE,  " & _
                        "(Case " & _
                        "When dt_ESContact.STRKEY='42' THEN dt_ESContact.STRCONTACTZIPCODE " & _
                        "When dt_ESContact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTZIPCODE " & _
                        "END) STRCONTACTZIPCODE " & _
                        "From " & _
                        "(Select DISTINCT dt_eslist.STRAIRSNUMBER, dt_contact.STRKEY,  " & _
                        "dt_Contact.STRCONTACTLASTNAME, dt_Contact.STRCONTACTFIRSTNAME, " & _
                        "dt_Contact.STRContactCompanyName, dt_Contact.STRContactEmail,  " & _
                        "dt_Contact.STRCONTACTPREFIX, dt_Contact.STRCONTACTADDRESS1, dt_Contact.STRCONTACTCITY,  " & _
                        "dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE " & _
                        "FROM " & _
                        "(Select * FROM " & DBNameSpace & ".APBHEADERDATA " & _
                        "where (stroperationalstatus = 'O' OR stroperationalstatus = 'P' oR stroperationalstatus = 'C') AND  " & _
                        "(STRCLASS = 'A')   " & _
                        "AND (" & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER Like '____121%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____013%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____015%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____045%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____057%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____063%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____067%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____077%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____089%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____097%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____113%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____117%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____135%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____139%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____151%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____217%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____223%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____247%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____255%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____297%') " & _
                        " ) dt_ESList,      " & _
                        "(Select * From " & DBNameSpace & ".APBCONTACTINFORMATION where STRKEY=42) dt_Contact " & _
                        "Where dt_ESList.STRAIRSNUMBEr = dt_Contact.STRAIRSNUMBER (+)) dt_ESContact, " & _
                        "(Select DISTINCT dt_eslist.STRAIRSNUMBER, dt_contact.STRKEY,  " & _
                        "dt_Contact.STRCONTACTLASTNAME, dt_Contact.STRCONTACTFIRSTNAME, " & _
                        "dt_Contact.STRContactCompanyName, dt_Contact.STRContactEmail, dt_Contact.STRCONTACTPREFIX,  " & _
                        "dt_Contact.STRCONTACTADDRESS1, dt_Contact.strcontactcity, dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE " & _
                        "FROM " & _
                        "(Select * FROM " & DBNameSpace & ".APBHEADERDATA " & _
                        "where (stroperationalstatus = 'O' OR stroperationalstatus = 'P' oR stroperationalstatus = 'C') AND  " & _
                        "(STRCLASS = 'A')   " & _
                        "AND (" & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER Like '____121%'    " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____013%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____015%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____045%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____057%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____063%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____067%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____077%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____089%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____097%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____113%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____117%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____135%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____139%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____151%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____217%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____223%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____247%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____255%' " & _
                        "or " & DBNameSpace & ".apbheaderdata.STRAIRSNUMBER like '____297%') " & _
                        ")dt_ESList,  " & _
                        "(Select * From " & DBNameSpace & ".APBCONTACTINFORMATION where STRKEY=30) dt_Contact " & _
                        "Where dt_ESList.STRAIRSNUMBEr = dt_Contact.STRAIRSNUMBER (+)) dt_PermitContact, " & _
                        "" & DBNameSpace & ".APBFACILITYINFORMATION, " & _
                        "" & DBNameSpace & ".APBHEADERDATA " & _
                        "Where " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER= dt_ESContact.STRAIRSNumber and  " & _
                        "" & DBNameSpace & ".APBHEADERDATA.STRAIRSNUMBER= dt_ESContact.STRAIRSNumber and  " & _
                        "dt_ESContact.STRAIRSNumber  = dt_PermitContact.STRAIRSNUMBER (+) "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader

                        dr.Read()
                        Do
                            airsNumber = dr("strAirsNumber")
                            airsYear = airsNumber & ESYear
                            ESYear = cboMailoutYear.SelectedItem
                            If IsDBNull(dr("STRFACILITYNAME")) Then
                                FACILITYNAME = " "
                            Else
                                FACILITYNAME = dr("STRFACILITYNAME")
                            End If
                            If IsDBNull(dr("STROPERATIONALSTATUS")) Then
                                OperationalStatus = " "
                            Else
                                OperationalStatus = dr("STROPERATIONALSTATUS")
                            End If
                            If IsDBNull(dr("STRCLASS")) Then
                                FacilityClass = " "
                            Else
                                FacilityClass = dr("STRCLASS")
                            End If
                            If IsDBNull(dr("STRCONTACTCOMPANYNAME")) Then
                                CONTACTCOMPANYNAME = " "
                            Else
                                CONTACTCOMPANYNAME = dr("STRCONTACTCOMPANYNAME")
                            End If
                            If IsDBNull(dr("STRCONTACTADDRESS1")) Then
                                CONTACTADDRESS1 = " "
                            Else
                                CONTACTADDRESS1 = dr("STRCONTACTADDRESS1")
                            End If
                            If IsDBNull(dr("STRCONTACTCITY")) Then
                                CONTACTCITY = " "
                            Else
                                CONTACTCITY = dr("STRCONTACTCITY")
                            End If
                            If IsDBNull(dr("STRCONTACTSTATE")) Then
                                CONTACTSTATE = " "
                            Else
                                CONTACTSTATE = dr("STRCONTACTSTATE")
                            End If
                            If IsDBNull(dr("STRCONTACTZIPCODE")) Then
                                CONTACTZIPCODE = " "
                            Else
                                CONTACTZIPCODE = dr("STRCONTACTZIPCODE")
                            End If
                            If IsDBNull(dr("STRCONTACTFIRSTNAME")) Then
                                CONTACTFIRSTNAME = " "
                            Else
                                CONTACTFIRSTNAME = dr("STRCONTACTFIRSTNAME")
                            End If
                            If IsDBNull(dr("STRCONTACTLASTNAME")) Then
                                CONTACTLASTNAME = " "
                            Else
                                CONTACTLASTNAME = dr("STRCONTACTLASTNAME")
                            End If
                            If IsDBNull(dr("STRCONTACTEMAIL")) Then
                                CONTACTEMAIL = " "
                            Else
                                CONTACTEMAIL = dr("STRCONTACTEMAIL")
                            End If

                            SQL2 = "insert into " & DBNameSpace & ".ESmailOut " & _
                            "(strAirsYear, " & _
                            "strAirsNumber, " & _
                            "STRFACILITYNAME, " & _
                            "STROPERATIONALSTATUS, " & _
                            "STRCLASS, " & _
                            "STRCONTACTCOMPANYNAME, " & _
                            "STRCONTACTADDRESS1, " & _
                            "STRCONTACTCITY, " & _
                            "STRCONTACTSTATE, " & _
                            "STRCONTACTZIPCODE, " & _
                            "STRCONTACTFIRSTNAME, " & _
                            "STRCONTACTLASTNAME, " & _
                            "STRCONTACTEMAIL, " & _
                            "strESYear) " & _
                            "values " & _
                            "('" & airsYear & "', " & _
                            "'" & airsNumber & "', " & _
                            "'" & Replace(FACILITYNAME, "'", "''") & "', " & _
                            "'" & Replace(OperationalStatus, "'", "''") & "', " & _
                            "'" & Replace(FacilityClass, "'", "''") & "', " & _
                             "'" & Replace(Replace(CONTACTCOMPANYNAME, "'", "''"), "N/A", " ") & "', " & _
                            "'" & Replace(Replace(CONTACTADDRESS1, "'", "''"), "N/A", " ") & "', " & _
                            "'" & Replace(Replace(CONTACTCITY, "'", "''"), "N/A", " ") & "', " & _
                            "'" & CONTACTSTATE & "', " & _
                            "'" & Replace(CONTACTZIPCODE, "N/A", " ") & "', " & _
                            "'" & Replace(Replace(CONTACTFIRSTNAME, "'", "''"), "N/A", " ") & "', " & _
                            "'" & Replace(Replace(CONTACTLASTNAME, "'", "''"), "N/A", " ") & "', " & _
                            "'" & Replace(Replace(CONTACTEMAIL, "'", "''"), "N/A", " ") & "', " & _
                            "'" & ESYear & "')"

                            Dim cmd2 As New OracleCommand(SQL2, CurrentConnection)
                            'If conn.State = ConnectionState.Closed Then
                            '    conn.Open()
                            'End If
                            cmd2.CommandType = CommandType.Text

                            dr2 = cmd2.ExecuteReader
                            'dr2.Close()
                        Loop While dr.Read


                    End If


                    Dim year As String = cboMailoutYear.SelectedItem

                    SQL = "SELECT STRAIRSNUMBER, " & _
                    "STRFACILITYNAME, " & _
                    "STROPERATIONALSTATUS, " & _
                    "STRCLASS, " & _
                    "STRCONTACTFIRSTNAME, " & _
                    "STRCONTACTLASTNAME, " & _
                    "STRCONTACTCOMPANYname, " & _
                    "STRCONTACTADDRESS1, " & _
                    "STRCONTACTCITY, " & _
                    "STRCONTACTSTATE, " & _
                    "STRCONTACTZIPCODE, " & _
                    "STRCONTACTEMAIL " & _
                    "from " & DBNameSpace & ".esMailOut " & _
                    "where STRESYEAR = '" & year & "' " & _
                    "order by STRFACILITYNAME"

                    dsViewCount = New DataSet
                    daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    daViewCount.Fill(dsViewCount, "ViewCount")
                    dgvESDataCount.DataSource = dsViewCount
                    dgvESDataCount.DataMember = "ViewCount"

                    dgvESDataCount.RowHeadersVisible = False
                    dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvESDataCount.AllowUserToResizeColumns = True
                    dgvESDataCount.AllowUserToAddRows = False
                    dgvESDataCount.AllowUserToDeleteRows = False
                    dgvESDataCount.AllowUserToOrderColumns = True
                    dgvESDataCount.AllowUserToResizeRows = True

                    dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
                    dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
                    dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
                    dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
                    dgvESDataCount.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status"
                    dgvESDataCount.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
                    dgvESDataCount.Columns("STRCLASS").HeaderText = "Facility Class"
                    dgvESDataCount.Columns("STRCLASS").DisplayIndex = 3
                    dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
                    dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
                    dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
                    dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
                    dgvESDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
                    dgvESDataCount.Columns("STRCONTACTCOMPANYname").DisplayIndex = 6
                    dgvESDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
                    dgvESDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 7
                    dgvESDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
                    dgvESDataCount.Columns("STRCONTACTCITY").DisplayIndex = 8
                    dgvESDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
                    dgvESDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 9
                    dgvESDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
                    dgvESDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
                    dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
                    dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 11

                    txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub deleteESmailOutbyYear()
        Dim ESyear As String = cboMailoutYear.SelectedItem

        Try


            If ESyear = "Select a Mailout Year & Click Below" Then
                MsgBox("You must select a Mailout Year")
            Else
                SQL = "delete from " & DBNameSpace & ".ESmailout " & _
                "where strESyear = '" & ESyear & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                MsgBox("ES mail out is deleted!")

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnGenMailOut_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenMailOut.Click
        Try
            GenerateESMailOut()
            cboMailoutYear.Items.Clear()
            loadMailOutYear()
            cboMailoutYear.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDelMailOut_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelMailOut.Click
        Try
            deleteESmailOutbyYear()
            cboMailoutYear.Items.Clear()
            loadMailOutYear()
            cboMailoutYear.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewselectedyearMailoutList_LinkClicked_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewselectedyearMailoutList.LinkClicked

        Try
            Dim year As String = cboMailoutYear.SelectedItem

            SQL = "SELECT STRAIRSNUMBER, " & _
            "STRFACILITYNAME, " & _
            "STROPERATIONALSTATUS, " & _
            "STRCLASS, " & _
            "STRCONTACTFIRSTNAME, " & _
            "STRCONTACTLASTNAME, " & _
            "STRCONTACTCOMPANYNAME, " & _
            "STRCONTACTADDRESS1, " & _
            "STRCONTACTCITY, " & _
            "STRCONTACTSTATE, " & _
            "STRCONTACTZIPCODE, " & _
            "STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".esMailOut " & _
            "where STRESYEAR = '" & year & "' " & _
            "order by STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status"
            dgvESDataCount.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
            dgvESDataCount.Columns("STRCLASS").HeaderText = "Facility Class"
            dgvESDataCount.Columns("STRCLASS").DisplayIndex = 3
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvESDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTCOMPANYname").DisplayIndex = 6
            dgvESDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
            dgvESDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 7
            dgvESDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvESDataCount.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvESDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvESDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvESDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvESDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 11

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
#Region "EI Tool"
    Private Sub loadEIYear()

        Dim SQL As String
        Dim year As String
        Try
            SQL = "Select distinct STRINVENTORYYEAR " & _
            "from " & DBNameSpace & ".EISI " & _
            "order by STRINVENTORYYEAR desc "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                year = dr("STRINVENTORYYEAR")
                cboEIYear2.Items.Add(year)
            End While

            cboEIYear2.SelectedIndex = 0

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
         
        End Try
    End Sub
    Private Sub loadEIType()

        Dim SQL As String
        Dim EItype As String
        Try
            SQL = "Select distinct EITHRESHOLDS.STRTYPE " & _
            "from " & DBNameSpace & ".EITHRESHOLDS " & _
            "order by STRTYPE desc "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                EItype = dr("STRTYPE")
                cboEItype.Items.Add(EItype)
                cboEIType2.Items.Add(EItype)
            End While

            cboEItype.SelectedIndex = 0
            cboEIType2.SelectedIndex = 0

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnEIView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEIView.Click
        Try
            runEIcount()
            lblEIYear.Text = cboEIYear2.SelectedItem
            lblMailoutyear.Text = cboEIYear2.SelectedItem

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
    End Sub
    Private Sub runEIcount()
        Dim Nonresponsecount As Integer
        Dim MailoutCount As Integer
        Dim MailOutOptInCount As Integer
        Dim mailoutOptOutCount As Integer
        Dim ResponseCount As Integer
        Dim finalizedcount As Integer
        Dim inprocesscount As Integer
        Dim TotaloptinCount As Integer
        Dim TotaloptoutCount As Integer
        Dim extracount As Integer
        Dim extraOptincount As Integer
        Dim extraOptOutCount As Integer
        Dim TotalResponsecount As Integer
        Dim removedFacilitiescount As Integer
        Dim mailoutNonresponderscount As Integer
        Dim extraNonresponderscount As Integer

        Dim year As Integer = CInt(cboEIYear2.SelectedItem)
        txtEIYear.Text = cboEIYear2.SelectedItem
        Dim EIYear As String = txtEIYear.Text
        Dim deadline As String = "30-Sep-2007"
        Try
            Try

                SQL = "select count(*) as MailoutCount " & _
                "from " & DBNameSpace & ".eImailout, " & DBNameSpace & ".EISI " & _
                "where " & DBNameSpace & ".eImailout.STRAIRSYEAR = " & DBNameSpace & ".EISI.STRAIRSYEAR(+) " & _
                "and " & DBNameSpace & ".eImailout.STRINVENTORYYEAR = '" & EIYear & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read()
                    txtEIMailOutCount.Text = dr.Item(MailoutCount)
                End While
                dr.Close()

                SQL = "select count(*) as ResponseCount " & _
                "from " & DBNameSpace & ".eImailout, " & DBNameSpace & ".EISI " & _
                "where " & DBNameSpace & ".eImailout.STRAIRSYEAR = " & DBNameSpace & ".EISI.STRAIRSYEAR " & _
                "and " & DBNameSpace & ".EISI.STROPTOUT is not NULL " & _
                "and " & DBNameSpace & ".eImailout.STRINVENTORYYEAR = '" & EIYear & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read()
                    txtEIResponseCount.Text = dr.Item(ResponseCount)
                End While
                dr.Close()

                SQL = "select count(*) as removedFacilitiescount " & _
            "from  " & DBNameSpace & ".eImailout, " & DBNameSpace & ".EISI " & _
            "where " & DBNameSpace & ".eImailout.STRINVENTORYYEAR = '" & EIYear & "' " & _
            "and " & DBNameSpace & ".eImailout.STRAIRSYEAR = " & DBNameSpace & ".EISI.STRAIRSYEAR(+) " & _
            "and " & DBNameSpace & ".EISI.STRAIRSYEAR is NULL"

                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read()
                    txtRemovedFacilities.Text = dr.Item(removedFacilitiescount)
                End While
                dr.Close()

                SQL = "select count(*) as extraNonresponderscount  " & _
                "from " & DBNameSpace & ".EISI " & _
                " where  not exists (select * from " & DBNameSpace & ".EIMAILOUT " & _
                " where " & DBNameSpace & ".EISI.STRAIRSNUMBER = " & DBNameSpace & ".EIMAILOUT.STRAIRSNUMBER" & _
                " and EISI.STRINVENTORYYEAR = EIMAILOUT.STRINVENTORYYEAR) " & _
                " and " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & EIYear & "' " & _
                " and " & DBNameSpace & ".EISI.STROPTOUT is null"

                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read()
                    txtExtraNonResponses.Text = dr.Item(extraNonresponderscount)
                End While
                dr.Close()

                SQL = "select count(*) as TotaloptinCount " & _
                "from " & DBNameSpace & ".EISI " & _
                "where " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & EIYear & "'" & _
                " and " & DBNameSpace & ".EISI.strOptOut = 'NO'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read()
                    txtEITotalOptInCount.Text = dr.Item(TotaloptinCount)
                End While
                dr.Close()

                SQL = "select count(*) as TotaloptOutCount " & _
                "from " & DBNameSpace & ".EISI " & _
                "where " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & EIYear & "' " & _
                "and " & DBNameSpace & ".EISI.strOptOut = 'YES'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read()
                    txtEITotalOptOutCount.Text = dr.Item(TotaloptoutCount)
                End While
                dr.Close()

                SQL = "select count(*) as FinalizedCount " & _
                "from " & DBNameSpace & ".EISI, " & DBNameSpace & ".eImailout " & _
                "where " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & EIYear & "' " & _
                "and " & DBNameSpace & ".eImailout.STRAIRSYEAR = " & DBNameSpace & ".EISI.STRAIRSYEAR(+) " & _
                "and " & DBNameSpace & ".EISI.strOptOut = 'NO' " & _
                "and " & DBNameSpace & ".EISI.STRFINALIZE is not null"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtFinalized.Text = dr.Item(finalizedcount)
                End While
                dr.Close()

                SQL = "select count(*) as inprocesscount " & _
                "from " & DBNameSpace & ".EISI, " & DBNameSpace & ".eImailout  " & _
                "where " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & EIYear & "' " & _
                "and " & DBNameSpace & ".eImailout.STRAIRSYEAR = " & DBNameSpace & ".EISI.STRAIRSYEAR(+) " & _
                "and " & DBNameSpace & ".EISI.strOptOut = 'NO' " & _
                "and " & DBNameSpace & ".EISI.STRFINALIZE is null"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read()
                    txtinProgress.Text = dr.Item(inprocesscount)
                End While
                dr.Close()

                SQL = "select count(*) as MailOutOptInCount " & _
                "from " & DBNameSpace & ".EISI, " & DBNameSpace & ".eImailout " & _
                "where " & DBNameSpace & ".eImailout.STRINVENTORYYEAR = '" & EIYear & "' " & _
                "and " & DBNameSpace & ".eImailout.STRAIRSYEAR = " & DBNameSpace & ".EISI.STRAIRSYEAR(+) " & _
                "and " & DBNameSpace & ".EISI.strOptOut = 'NO'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtEIMailoutOptin.Text = dr.Item(MailOutOptInCount)
                End While
                dr.Close()

                SQL = "select count(*) as MailOutOptOutCount " & _
                "from " & DBNameSpace & ".EISI, " & DBNameSpace & ".eImailout " & _
                "where " & DBNameSpace & ".eImailout.STRINVENTORYYEAR = '" & EIYear & "' " & _
                "and " & DBNameSpace & ".eImailout.STRAIRSYEAR = " & DBNameSpace & ".EISI.STRAIRSYEAR " & _
                "and " & DBNameSpace & ".EISI.strOptOut = 'YES'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read()
                    txtEIMailoutOptOut.Text = dr.Item(mailoutOptOutCount)
                End While
                dr.Close()

            Catch ex As Exception
                MsgBox("That Prefix is not in the db" + vbCrLf + ex.ToString())
            End Try

            SQL = "select count(*) as mailoutNonresponderscount " & _
            "from  " & DBNameSpace & ".eImailout, " & DBNameSpace & ".EISI " & _
            "where " & DBNameSpace & ".eImailout.STRINVENTORYYEAR = '" & EIYear & "' " & _
            "and " & DBNameSpace & ".eImailout.STRAIRSYEAR = " & DBNameSpace & ".EISI.STRAIRSYEAR(+) " & _
            "and " & DBNameSpace & ".EISI.strOptOut is NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read()
                txtMailoutNonReaponses.Text = dr.Item(mailoutNonresponderscount)
            End While
            dr.Close()

            SQL = "select count(*) as Nonresponsecount " & _
         "from  " & DBNameSpace & ".EISI " & _
         "where " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & EIYear & "' " & _
         "and " & DBNameSpace & ".EISI.strOptOut is NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read()
                txtEINonResponseCount.Text = dr.Item(Nonresponsecount)
            End While
            dr.Close()

            SQL = "select count(*) as ExtraCount " & _
            "from (Select " & DBNameSpace & ".EISI.STRAIRSYEAR AS SchemaAIRS, " & _
            "" & DBNameSpace & ".eImailout.STRAIRSYear AS MailoutAIRS " & _
            "From " & DBNameSpace & ".eImailout, " & DBNameSpace & ".EISI  " & _
            "Where " & DBNameSpace & ".eImailout.STRAIRSYEAR (+) = " & DBNameSpace & ".EISI.STRAIRSYEAR " & _
            "AND " & DBNameSpace & ".EISI.STRINVENTORYYEAR= '" & EIYear & "' " & _
            "AND " & DBNameSpace & ".EISI.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & _
            "" & DBNameSpace & ".EISI " & _
            "Where " & DBNameSpace & ".EISI.STRAIRSYEAR = SchemaAIRS " & _
            "AND MailoutAIRS is NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read()
                txtEIextraResponse.Text = dr.Item(extracount)
                txtEIextraResponse2.Text = dr.Item(extracount)
            End While
            dr.Close()

            SQL = "select count(*) as ExtraOptinCount " & _
            "from (Select " & DBNameSpace & ".EISI.STRAIRSYEAR AS SchemaAIRS, " & _
            "" & DBNameSpace & ".EIMAILOUT.STRAIRSYear AS MailoutAIRS " & _
            "From " & DBNameSpace & ".EIMailout, " & DBNameSpace & ".EISI " & _
            "Where " & DBNameSpace & ".EIMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".EISI.STRAIRSYEAR " & _
            "AND " & DBNameSpace & ".EISI.STRINVENTORYYEAR= '" & EIYear & "' " & _
            "AND " & DBNameSpace & ".EISI.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & _
            "" & DBNameSpace & ".EISI " & _
            "Where " & DBNameSpace & ".EISI.STRAIRSYEAR = SchemaAIRS " & _
            "AND MailoutAIRS is NULL " & _
            "and " & DBNameSpace & ".EISI.STROPTOUT='NO'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read()
                txtEIExtraOptin.Text = dr.Item(extraOptincount)
            End While
            dr.Close()

            SQL = "select count(*) as ExtraOptOUTCount " & _
            "from (Select " & DBNameSpace & ".EISI.STRAIRSYEAR AS SchemaAIRS, " & _
            "" & DBNameSpace & ".EIMAILOUT.STRAIRSYear AS MailoutAIRS " & _
            "From " & DBNameSpace & ".EiMailout, " & DBNameSpace & ".EISI " & _
            " Where " & DBNameSpace & ".EIMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".EISI.STRAIRSYEAR " & _
            "AND " & DBNameSpace & ".EISI.STRINVENTORYYEAR= '" & EIYear & "' " & _
            "AND " & DBNameSpace & ".EISI.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & _
            "" & DBNameSpace & ".EISI " & _
            "Where " & DBNameSpace & ".EISI.STRAIRSYEAR = SchemaAIRS " & _
            "AND MailoutAIRS is NULL " & _
            "and " & DBNameSpace & ".EISI.STROPTOUT='YES'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtEIExtraOptOut.Text = dr.Item(extraOptOutCount)
            End While
            dr.Close()

            SQL = "select count(*) as TotalResponsecount " & _
            "from " & DBNameSpace & ".EISI " & _
            "where " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & EIYear & "' " & _
            "and EISI.strOptOut is not NULL"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtEITotalResponse.Text = dr.Item(TotalResponsecount)
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
        End Try
    End Sub
    Private Sub findEIMailOut()
        Dim SQL As String
        Dim AirsNo As String = txtEIAirsNo.Text
        Dim dr As OracleDataReader
        Dim EIyear As String = txtEIYear.Text

        Try
            SQL = "SELECT * " & _
            "from " & DBNameSpace & ".eIMailOut " & _
            "where STRAIRSNUMBER = '" & AirsNo & "' " & _
            "and STRINVENTORYYEAR = '" & EIyear & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read
                If IsDBNull(dr("STRFACILITYNAME")) Then
                    txtEIFacilityName.Text = ""
                Else
                    txtEIFacilityName.Text = dr("STRFACILITYNAME")
                End If

                If IsDBNull(dr("STRCONTACTPREFIX")) Then
                    txtEIprefix.Text = ""
                Else
                    txtEIprefix.Text = dr("STRCONTACTPREFIX")
                End If
                If IsDBNull(dr("STRCONTACTFIRSTNAME")) Then
                    txtEIFirstName.Text = ""
                Else
                    txtEIFirstName.Text = dr("STRCONTACTFIRSTNAME")
                End If
                If IsDBNull(dr("STRCONTACTLASTNAME")) Then
                    txtEILastName.Text = ""
                Else
                    txtEILastName.Text = dr("STRCONTACTLASTNAME")
                End If
                If IsDBNull(dr("STRCONTACTCOMPANYNAME")) Then
                    txtEIcompanyName.Text = ""
                Else
                    txtEIcompanyName.Text = dr("STRCONTACTCOMPANYNAME")
                End If
                If IsDBNull(dr("STRCONTACTADDRESS1")) Then
                    txtEIcontactAddress1.Text = ""
                Else
                    txtEIcontactAddress1.Text = dr("STRCONTACTADDRESS1")
                End If
                If IsDBNull(dr("STRCONTACTADDRESS2")) Then
                    txtEIcontactAddress2.Text = ""
                Else
                    txtEIcontactAddress2.Text = dr("STRCONTACTADDRESS2")
                End If
                If IsDBNull(dr("STRCONTACTCITY")) Then
                    txtEIcontactCity.Text = ""
                Else
                    txtEIcontactCity.Text = dr("STRCONTACTCITY")
                End If
                If IsDBNull(dr("STRCONTACTSTATE")) Then
                    txtEIcontactState.Text = ""
                Else
                    txtEIcontactState.Text = dr("STRCONTACTSTATE")
                End If
                If IsDBNull(dr("STRCONTACTZIPCODE")) Then
                    txtEIcontactZipCode.Text = ""
                Else
                    txtEIcontactZipCode.Text = dr("STRCONTACTZIPCODE")
                End If
                If IsDBNull(dr("STRCONTACTEMAIL")) Then
                    txtEIcontactEmail.Text = ""
                Else
                    txtEIcontactEmail.Text = dr("STRCONTACTEMAIL")
                End If
            End While

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
        End Try
    End Sub
    Private Sub clearEIMailout()
        Try
            txtEIAirsNo.Text = ""
            txtEIFacilityName.Text = ""
            txtEIprefix.Text = ""
            txtEIFirstName.Text = ""
            txtEILastName.Text = ""
            txtEIcompanyName.Text = ""
            txtEIcontactAddress1.Text = ""
            txtEIcontactAddress2.Text = ""
            txtEIcontactCity.Text = ""
            txtEIcontactState.Text = ""
            txtEIcontactZipCode.Text = ""
            txtEIcontactEmail.Text = ""

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewMailOutEI_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewMailOutEI.LinkClicked
        Try
            txtEIYear.Text = cboEIYear2.SelectedItem
            
            Dim year As String = txtEIYear.Text

            SQL = "SELECT STRAIRSNUMBER, " & _
            "STRFACILITYNAME, " & _
            "STRCONTACTFIRSTNAME, " & _
            "STRCONTACTLASTNAME, " & _
            "STRCONTACTCOMPANYname, " & _
            "STRCONTACTADDRESS1, " & _
            "STRCONTACTCITY, " & _
            "STRCONTACTSTATE, " & _
            "STRCONTACTZIPCODE, " & _
            "STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".eIMailOut " & _
            "where STRinventoryYEAR = '" & year & "' " & _
            "order by STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"
         
            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvEIDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvEIDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvEIDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
            dgvEIDataCount.Columns("STRCONTACTCOMPANYname").DisplayIndex = 4
            dgvEIDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
            dgvEIDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 5
            dgvEIDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvEIDataCount.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvEIDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvEIDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvEIDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvEIDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvEIDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvEIDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 9

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString
            ' txtEIMailOutCount.Text = txtEIRecordNumber.Text

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try

    End Sub
    Private Sub lblViewEINonResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEINonResponse.LinkClicked
        Try
            
            Dim year As String = txtEIYear.Text()

            SQL = "SELECT " & DBNameSpace & ".eiMailOut.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".eiMailOut.STRFACILITYNAME " & _
            "from  " & DBNameSpace & ".EISI,  " & DBNameSpace & ".eiMailOut " & _
            "where " & _
            "and " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & year & "' " & _
            "and " & DBNameSpace & ".EISI.strOptOut is NULL" & _
            " order by " & DBNameSpace & ".eiMailOut.STRFACILITYNAME"


            SQL = "select  " & _
            "" & DBNameSpace & ".EISI.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".EISI.STRFACILITYNAME " & _
            "from  " & DBNameSpace & ".EISI  " & _
            "where " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & year & "'  " & _
            "and " & DBNameSpace & ".EISI.strOptOut is NULL  "


            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"
          
            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString
            ' clearEIData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try
    End Sub
    Private Sub lblViewEIOptIn_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEIOptIn.LinkClicked
        txtEIYear.Text = cboEIYear2.SelectedItem
        Try
            
            Dim year As String = txtEIYear.Text

            SQL = "SELECT airbranch.EISI.STRAIRSNUMBER, " & _
            "airbranch.EISI.STRFACILITYNAME, " & _
            "airbranch.EISI.STRFINALIZE, " & _
            "airbranch.EISI.STRCONFIRMATIONNUMBER " & _
            "from " & DBNameSpace & ".EISI, " & DBNameSpace & ".eImailout " & _
            "where " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & year & "' " & _
            "and " & DBNameSpace & ".EISI.STROPTOUT = 'NO'" & _
            "and " & DBNameSpace & ".eImailout.STRAIRSYEAR = " & DBNameSpace & ".EISI.STRAIRSYEAR " & _
            "order by " & DBNameSpace & ".EISI.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"
          
            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("STRFINALIZE").HeaderText = "Finalized Date"
            dgvEIDataCount.Columns("STRFINALIZE").DisplayIndex = 2
            dgvEIDataCount.Columns("STRCONFIRMATIONNUMBER").HeaderText = "Confirmation Number"
            dgvEIDataCount.Columns("STRCONFIRMATIONNUMBER").DisplayIndex = 3

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try
    End Sub
    Private Sub lblViewEIOptOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEIOptOut.LinkClicked
        txtEIYear.Text = cboEIYear2.SelectedItem
        Try
            
            Dim year As String = txtEIYear.Text

            SQL = "SELECT " & DBNameSpace & ".EISI.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".EISI.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".EISI.STRFINALIZE, " & _
            "" & DBNameSpace & ".EISI.STRCONFIRMATIONNUMBER " & _
            "from " & DBNameSpace & ".EISI, " & DBNameSpace & ".eImailout " & _
            "where " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & year & "' " & _
            "and " & DBNameSpace & ".EISI.STROPTOUT = 'YES'" & _
            "and " & DBNameSpace & ".eImailout.STRAIRSYEAR = " & DBNameSpace & ".EISI.STRAIRSYEAR " & _
            "order by " & DBNameSpace & ".EISI.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"
           
            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("STRFINALIZE").HeaderText = "Finalized Date"
            dgvEIDataCount.Columns("STRFINALIZE").DisplayIndex = 2
            dgvEIDataCount.Columns("STRCONFIRMATIONNUMBER").HeaderText = "Confirmation Number"
            dgvEIDataCount.Columns("STRCONFIRMATIONNUMBER").DisplayIndex = 3

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try
    End Sub
    Private Sub lblextraEIResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblextraEIResponse.LinkClicked
        Try
            
            Dim year As String = txtEIYear.Text


            SQL = "SELECT dt_NotInMailout.SchemaAIRS, " & DBNameSpace & ".EISI.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".EISI.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTCOMPANYNAME, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTEMAIL, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTPHONENUMBER1 " & _
            "from (Select " & DBNameSpace & ".EISI.STRAIRSYEAR AS SchemaAIRS, " & _
            "" & DBNameSpace & ".EIMailout.STRAIRSYEAR AS MailoutAIRS" & _
            " From " & DBNameSpace & ".EIMailout, " & DBNameSpace & ".EISI" & _
            " Where " & DBNameSpace & ".EIMailout.STRAIRSYEAR (+)= " & DBNameSpace & ".EISI.STRAIRSYEAR " & _
            "AND " & DBNameSpace & ".EISI.STRINVENTORYYEAR=  '" & year & "' " & _
            "AND " & DBNameSpace & ".EISI.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & _
            "" & DBNameSpace & ".EISI " & _
            "Where " & DBNameSpace & ".EISI.STRAIRSYEAR = SchemaAIRS " & _
            "AND MailoutAIRS is NULL"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"
          
            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvEIDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvEIDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvEIDataCount.Columns("STRCONTACTCOMPANYNAME").HeaderText = "Contact Company"
            dgvEIDataCount.Columns("STRCONTACTCOMPANYNAME").DisplayIndex = 4
            dgvEIDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvEIDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 5
            dgvEIDataCount.Columns("STRCONTACTPHONENUMBER1").HeaderText = "Phone No."
            dgvEIDataCount.Columns("STRCONTACTPHONENUMBER1").DisplayIndex = 6

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString


            'clearEIData()
            'ClearMailOut()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try
    End Sub
    Private Sub lblViewEIExtraOptIn_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEIExtraOptIn.LinkClicked
        txtEIYear.Text = cboEIYear2.SelectedItem
        Try
            
            Dim year As String = txtEIYear.Text

            SQL = "SELECT " & DBNameSpace & ".EISI.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".EISI.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".EISI.strfinalize, " & _
            "" & DBNameSpace & ".EISI.STRCONFIRMATIONNumber " & _
            "from (select " & DBNameSpace & ".EISI.strairsyear as SchemaAIRS, " & _
            "" & DBNameSpace & ".eImailout.strairsyear as MailoutAIRS " & _
            "From " & DBNameSpace & ".EIMailout, " & DBNameSpace & ".EISI " & _
            "where " & DBNameSpace & ".eImailout.STRAIRSYEAR (+)= " & DBNameSpace & ".EISI.STRAIRSYEAR " & _
            "and " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & year & "' " & _
            "and " & DBNameSpace & ".EISI.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & DBNameSpace & ".EISI " & _
            "Where " & DBNameSpace & ".EISI.STRAIRSYEAR = SchemaAIRS " & _
            "and MailoutAIRS is NULL " & _
            "and " & DBNameSpace & ".EISI.STROPTOUT='NO'"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"
          
            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("strfinalize").HeaderText = "Finalize Date"
            dgvEIDataCount.Columns("strfinalize").DisplayIndex = 2
            dgvEIDataCount.Columns("STRCONFIRMATIONNumber").HeaderText = "Confirmation Number"
            dgvEIDataCount.Columns("STRCONFIRMATIONNumber").DisplayIndex = 3

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try
    End Sub
    Private Sub lblViewEIExtraOptOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEIExtraOptOut.LinkClicked
        txtEIYear.Text = cboEIYear2.SelectedItem
        Try
            
            Dim year As String = txtEIYear.Text

            SQL = "SELECT " & DBNameSpace & ".EISI.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".EISI.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".EISI.strfinalize, " & _
            "" & DBNameSpace & ".EISI.STRCONFIRMATIONNumber " & _
            "from (select " & DBNameSpace & ".EISI.strairsyear as SchemaAIRS, " & _
            "" & DBNameSpace & ".eImailout.strairsyear as MailoutAIRS " & _
            "From " & DBNameSpace & ".EIMailout, " & DBNameSpace & ".EISI " & _
            "where " & DBNameSpace & ".eImailout.STRAIRSYEAR (+) = " & DBNameSpace & ".EISI.STRAIRSYEAR " & _
            "and " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & year & "' " & _
            "and " & DBNameSpace & ".EISI.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & DBNameSpace & ".EISI " & _
            "Where " & DBNameSpace & ".EISI.STRAIRSYEAR = SchemaAIRS " & _
            "and MailoutAIRS is NULL " & _
            "and " & DBNameSpace & ".EISI.STROPTOUT='YES'"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"

            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("strfinalize").HeaderText = "Finalize Date"
            dgvEIDataCount.Columns("strfinalize").DisplayIndex = 2
            dgvEIDataCount.Columns("STRCONFIRMATIONNumber").HeaderText = "Confirmation Number"
            dgvEIDataCount.Columns("STRCONFIRMATIONNumber").DisplayIndex = 3

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try
    End Sub
    Private Sub lblViewEITotalResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEITotalResponse.LinkClicked
        Try
            txtEIYear.Text = cboEIYear2.SelectedItem

            
            Dim year As String = txtEIYear.Text

            SQL = "SELECT " & DBNameSpace & ".EISI.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".EISI.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTCOMPANYname, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTEMAIL, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTPHONENUMBER1 " & _
            "from " & DBNameSpace & ".EISI " & _
            "where " & DBNameSpace & ".EISI.strinventoryYear = '" & year & "' " & _
            "and " & DBNameSpace & ".EISI.STROPTOUT is not NULL " & _
            "order by " & DBNameSpace & ".EISI.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"
       
            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvEIDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvEIDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvEIDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
            dgvEIDataCount.Columns("STRCONTACTCOMPANYname").DisplayIndex = 4
            dgvEIDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvEIDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 5
            dgvEIDataCount.Columns("STRCONTACTPHONENUMBER1").HeaderText = "Phone Number"
            dgvEIDataCount.Columns("STRCONTACTPHONENUMBER1").DisplayIndex = 6

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString

            'clearEIData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try
    End Sub
    Private Sub lblViewEITotalOptin_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEITotalOptin.LinkClicked
        Try
            txtEIYear.Text = cboEIYear2.SelectedItem

            
            Dim year As String = txtEIYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT " & DBNameSpace & ".EISI.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".EISI.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".EISI.STRfinalize, " & _
            "" & DBNameSpace & ".EISI.STRCONFIRMATIONNumber " & _
            "from " & DBNameSpace & ".EISI, " & DBNameSpace & ".eimailout " & _
            "where " & DBNameSpace & ".EISI.strinventoryyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".EISI.STROPTOUT = 'NO'" & _
            "and " & DBNameSpace & ".EIMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".EISI.STRAIRSYEAR " & _
            "order by " & DBNameSpace & ".eisi.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"
           
            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("STRfinalize").HeaderText = "Finalize Date"
            dgvEIDataCount.Columns("STRfinalize").DisplayIndex = 2
            dgvEIDataCount.Columns("STRCONFIRMATIONNumber").HeaderText = "Confirmation Number"
            dgvEIDataCount.Columns("STRCONFIRMATIONNumber").DisplayIndex = 3

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try
    End Sub
    Private Sub lblViewEITotalOptOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEITotalOptOut.LinkClicked
        Try
            txtEIYear.Text = cboEIYear2.SelectedItem

            
            Dim year As String = txtEIYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT " & DBNameSpace & ".EISI.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".EISI.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".EISI.STRfinalize, " & _
            "" & DBNameSpace & ".EISI.STRCONFIRMATIONNumber " & _
            "from " & DBNameSpace & ".EISI, " & DBNameSpace & ".eimailout " & _
            "where " & DBNameSpace & ".EISI.strinventoryyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".EISI.STROPTOUT = 'YES'" & _
            "and " & DBNameSpace & ".EIMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".EISI.STRAIRSYEAR " & _
            "order by " & DBNameSpace & ".eisi.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"
          
            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("STRfinalize").HeaderText = "Finalize Date"
            dgvEIDataCount.Columns("STRfinalize").DisplayIndex = 2
            dgvEIDataCount.Columns("STRCONFIRMATIONNumber").HeaderText = "Confirmation Number"
            dgvEIDataCount.Columns("STRCONFIRMATIONNumber").DisplayIndex = 3

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
    End Sub
    Private Sub lblViewEIMailOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEIMailOut.LinkClicked
        Try
            
            Dim year As String = txtEIYear.Text

            SQL = "SELECT STRAIRSNUMBER, " & _
            "STRFACILITYNAME, " & _
            "STRCONTACTFIRSTNAME, " & _
            "STRCONTACTLASTNAME, " & _
            "STRCONTACTCOMPANYname, " & _
            "STRCONTACTADDRESS1, " & _
            "STRCONTACTCITY, " & _
            "STRCONTACTSTATE, " & _
            "STRCONTACTZIPCODE, " & _
            "STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".eIMailOut " & _
            "where strinventoryyear = '" & year & "' " & _
            "order by STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"
           
            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvEIDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvEIDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvEIDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
            dgvEIDataCount.Columns("STRCONTACTCOMPANYname").DisplayIndex = 4
            dgvEIDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
            dgvEIDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 5
            dgvEIDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvEIDataCount.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvEIDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvEIDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvEIDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvEIDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvEIDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvEIDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 9

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
    End Sub
    Private Sub dgvEIDataCount_MouseUp1(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvEIDataCount.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvEIDataCount.HitTest(e.X, e.Y)

        Try
            If dgvEIDataCount.RowCount > 0 Then
                ' If dgvEIDataCount.Columns(2).HeaderText <> "Transaction Date" Then
                If dgvEIDataCount.ColumnCount > 2 Then
                    If dgvEIDataCount.RowCount > 0 And hti.RowIndex <> -1 Then
                        If dgvEIDataCount.Columns(0).HeaderText = "Airs No." Then
                            If IsDBNull(dgvEIDataCount(0, hti.RowIndex).Value) Then

                            Else
                                clearEIMailout()
                                txtEIAirsNo.Text = dgvEIDataCount(0, hti.RowIndex).Value
                                txtEIFacilityName.Text = dgvEIDataCount(1, hti.RowIndex).Value
                                findEIMailOut()
                            End If
                        Else
                            If dgvEIDataCount.Columns(0).HeaderText = "Airs No." Then
                                If IsDBNull(dgvEIDataCount(0, hti.RowIndex).Value) Then
                                    txtEIAirsNo.Text = dgvEIDataCount(0, hti.RowIndex).Value
                                    txtEIFacilityName.Text = dgvEIDataCount(1, hti.RowIndex).Value

                                Else
                                    clearEIMailout()
                                    findEIMailOut()
                                    txtEIAirsNo.Text = dgvEIDataCount(0, hti.RowIndex).Value

                                End If
                            End If
                        End If
                        If dgvEIDataCount.Columns(2).HeaderText = "Transaction Date" Then
                            If IsDBNull(dgvEIDataCount(3, hti.RowIndex).Value) Then
                                txtEIAirsNo.Text = dgvEIDataCount(0, hti.RowIndex).Value
                                txtEIFacilityName.Text = dgvEIDataCount(1, hti.RowIndex).Value

                            Else
                                clearEIMailout()
                                findEIMailOut()
                                txtEIAirsNo.Text = dgvEIDataCount(0, hti.RowIndex).Value
                                txtEIFacilityName.Text = dgvEIDataCount(1, hti.RowIndex).Value

                            End If
                        Else
                            If dgvEIDataCount.Columns(2).HeaderText = "Transaction Date" Then
                                If IsDBNull(dgvEIDataCount(3, hti.RowIndex).Value) Then
                                    txtEIAirsNo.Text = dgvEIDataCount(0, hti.RowIndex).Value
                                    txtEIFacilityName.Text = dgvEIDataCount(1, hti.RowIndex).Value

                                Else
                                    clearEIMailout()
                                    findEIMailOut()
                                    txtEIAirsNo.Text = dgvEIDataCount(0, hti.RowIndex).Value
                                    txtEIFacilityName.Text = dgvEIDataCount(1, hti.RowIndex).Value

                                End If
                            End If
                        End If

                    End If
                Else
                    clearEIMailout()
                    findEIMailOut()
                    txtEIAirsNo.Text = dgvEIDataCount(0, hti.RowIndex).Value
                    txtEIFacilityName.Text = dgvEIDataCount(1, hti.RowIndex).Value
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub loadEIMailOutYear()
        'Load MailOut Year dropdown boxes
        Dim year As Integer
        Dim SQL As String
        Try
            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            SQL = "Select distinct STRINVENTORYYEAR " & _
                      "from " & DBNameSpace & ".EIMAILOUT  " & _
                      "order by STRINVENTORYYEAR desc"
            Dim cmd As New OracleCommand(SQL, CurrentConnection)

            Dim dr As OracleDataReader = cmd.ExecuteReader()
            dr.Read()
            year = dr("STRINVENTORYYEAR") + 1
            cboEIMailoutYear.Items.Add(year)
            Do
                year = dr("STRINVENTORYYEAR")
                cboEIMailoutYear.Items.Add(year)
            Loop While dr.Read
            cboEIMailoutYear.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub loadEIEnrollmentYear()
        'Load MailOut Year dropdown boxes
        Dim year As Integer
        Dim SQL As String
        Try
            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            SQL = "Select distinct STRINVENTORYYEAR " & _
                      "from " & DBNameSpace & ".EIMAILOUT  " & _
                      "order by STRINVENTORYYEAR desc"
            Dim cmd As New OracleCommand(SQL, CurrentConnection)

            Dim dr As OracleDataReader = cmd.ExecuteReader()
            dr.Read()
            Do
                year = dr("STRINVENTORYYEAR")
                cboEIEnrollmentYear.Items.Add(year)
            Loop While dr.Read
            cboEIEnrollmentYear.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub loadESEnrollmentYear()
        'Load MailOut Year dropdown boxes
        Dim year As Integer
        Dim SQL As String
        Try
            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            SQL = "Select distinct ESMAILOUT.STRESYEAR " & _
                      "from " & DBNameSpace & ".ESMAILOUT  " & _
                      "order by STRESYEAR desc"
            Dim cmd As New OracleCommand(SQL, CurrentConnection)

            Dim dr As OracleDataReader = cmd.ExecuteReader()
            dr.Read()
            Do
                year = dr("STRESYEAR")
                cboESYear.Items.Add(year)
            Loop While dr.Read
            ' cboESYear.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

   

    Private Sub btnGenerateEIMailOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateEIMailOut.Click
        Try
            GenerateEIMailOut()
            cboEIMailoutYear.Items.Clear()
            loadEIMailOutYear()
            cboEIMailoutYear.Text = ""



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try
    End Sub
    Sub GenerateEIMailOut()
        Dim airsNumber As String
        Dim airsYear As String
        Dim FACILITYNAME As String = " "
        Dim CONTACTCOMPANYNAME As String = " "
        Dim CONTACTADDRESS1 As String = " "
        Dim CONTACTADDRESS2 As String = " "
        Dim CONTACTCITY As String = " "
        Dim CONTACTSTATE As String = " "
        Dim CONTACTZIPCODE As String = " "
        Dim CONTACTFIRSTNAME As String = " "
        Dim CONTACTLASTNAME As String = " "
        Dim CONTACTEMAIL As String = " "
        Dim EIYear As String = cboEIMailoutYear.SelectedItem

        Dim OperationalStatus As String = " "
        Dim FacilityClass As String = " "

        Try
            

            If EIYear = "Select a Mailout Year & Click Below" Then
                MsgBox("You must select a Mailout Year")
            Else
                SQL = "Select strAirsNumber " & _
                "FROM " & DBNameSpace & ".EImailOut " & _
                "where STRINVENTORYYEAR = '" & EIYear & "'"
            End If
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                MsgBox("That year is already being used." & vbCrLf & "If you want to use that year," & vbCrLf & "you must first delete that year from the database.")
            Else
                If cboEIMailoutYear.Text <> "" Then
                    If cboEIMailoutYear.Text.Length = 4 Then
                        SQL = "Select dt_EIcontact.STRairsnumber, " & DBNameSpace & ".APBFacilityinformation.STRFACILITYNAME, " & _
                        "" & DBNameSpace & ".APBHEADERDATA.stroperationalstatus, " & DBNameSpace & ".APBHEADERDATA.STRCLASS, " & _
                        "(Case " & _
                        "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRContactLastName " & _
                        "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRContactLastName " & _
                        "Else '' " & _
                        "END) STRContactLastName, " & _
                        "(Case " & _
                        "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRContactfirstName " & _
                        "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRContactfirstName " & _
                        "Else '' " & _
                        "END) STRContactfirstName, " & _
                        "(Case " & _
                        "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRContactCompanyName " & _
                        "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRContactCompanyName " & _
                        "END) STRContactCompanyName, " & _
                        "(Case " & _
                        "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRContactEmail " & _
                        "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRContactEmail " & _
                        "END) STRContactEmail, " & _
                        "(Case " & _
                        "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRCONTACTPREFIX " & _
                        "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTPREFIX " & _
                        "END) strCONTACTPREFIX, " & _
                        "(Case " & _
                        "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRCONTACTADDRESS1 " & _
                        "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTADDRESS1 " & _
                        "END) STRCONTACTADDRESS1, " & _
                        "(Case " & _
                        "When dt_EIContact.STRKEY='41' THEN dt_EIContact.STRCONTACTCITY " & _
                        "When dt_EIContact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTCITY " & _
                        "END) STRCONTACTCITY, " & _
                        "(Case " & _
                        "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRCONTACTSTATE " & _
                        "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTSTATE " & _
                        "END) STRCONTACTSTATE,  " & _
                        "(Case " & _
                        "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRCONTACTZIPCODE " & _
                        "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTZIPCODE " & _
                        "END) STRCONTACTZIPCODE " & _
                        "From " & _
                        "(Select DISTINCT dt_eIlist.STRAIRSNUMBER, dt_contact.STRKEY,  " & _
                        "dt_Contact.STRCONTACTLASTNAME, dt_Contact.STRCONTACTFIRSTNAME, " & _
                        "dt_Contact.STRContactCompanyName, dt_Contact.STRContactEmail,  " & _
                        "dt_Contact.STRCONTACTPREFIX, dt_Contact.STRCONTACTADDRESS1, dt_Contact.STRCONTACTCITY,  " & _
                        "dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE " & _
                        "FROM " & _
                        "(Select * FROM " & DBNameSpace & ".APBHEADERDATA " & _
                        "where (stroperationalstatus = 'O' OR stroperationalstatus = 'P' oR stroperationalstatus = 'C') AND  " & _
                        "(STRCLASS = 'A')   " & _
                        ") dt_EIList,      " & _
                        "(Select * From " & DBNameSpace & ".APBCONTACTINFORMATION where STRKEY=41) dt_Contact " & _
                        "Where dt_EIList.STRAIRSNUMBEr = dt_Contact.STRAIRSNUMBER (+)) dt_EIContact, " & _
                        "(Select DISTINCT dt_eIlist.STRAIRSNUMBER, dt_contact.STRKEY,  " & _
                "dt_Contact.STRCONTACTLASTNAME, dt_Contact.STRCONTACTFIRSTNAME, " & _
                "dt_Contact.STRContactCompanyName, dt_Contact.STRContactEmail, dt_Contact.STRCONTACTPREFIX,  " & _
                "dt_Contact.STRCONTACTADDRESS1, dt_Contact.strcontactcity, dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE " & _
                "FROM " & _
                        "(Select * FROM " & DBNameSpace & ".APBHEADERDATA " & _
                        "where (stroperationalstatus = 'O' OR stroperationalstatus = 'P' oR stroperationalstatus = 'C') AND  " & _
                        "(STRCLASS = 'A')   " & _
                        ") dt_EIList,      " & _
                        "(Select * From " & DBNameSpace & ".APBCONTACTINFORMATION where STRKEY=30) dt_Contact " & _
                        "Where dt_EIList.STRAIRSNUMBEr = dt_Contact.STRAIRSNUMBER (+)) dt_PermitContact, " & _
                        "" & DBNameSpace & ".APBFACILITYINFORMATION, " & _
                        "" & DBNameSpace & ".APBHEADERDATA " & _
                        "Where " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER= dt_EIContact.STRAIRSNumber and  " & _
                        "" & DBNameSpace & ".APBHEADERDATA.STRAIRSNUMBER= dt_EIContact.STRAIRSNumber and  " & _
                        "dt_EIContact.STRAIRSNumber  = dt_PermitContact.STRAIRSNUMBER (+) "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            airsNumber = dr("strAirsNumber")
                            airsYear = airsNumber & EIYear
                            EIYear = cboEIMailoutYear.SelectedItem
                            If IsDBNull(dr("STRFACILITYNAME")) Then
                                FACILITYNAME = " "
                            Else
                                FACILITYNAME = dr("STRFACILITYNAME")
                            End If
                            If IsDBNull(dr("STROPERATIONALSTATUS")) Then
                                OperationalStatus = " "
                            Else
                                OperationalStatus = dr("STROPERATIONALSTATUS")
                            End If
                            If IsDBNull(dr("STRCLASS")) Then
                                FacilityClass = " "
                            Else
                                FacilityClass = dr("STRCLASS")
                            End If
                            If IsDBNull(dr("STRCONTACTCOMPANYNAME")) Then
                                CONTACTCOMPANYNAME = " "
                            Else
                                CONTACTCOMPANYNAME = dr("STRCONTACTCOMPANYNAME")
                            End If
                            If IsDBNull(dr("STRCONTACTADDRESS1")) Then
                                CONTACTADDRESS1 = " "
                            Else
                                CONTACTADDRESS1 = dr("STRCONTACTADDRESS1")
                            End If

                            If IsDBNull(dr("STRCONTACTCITY")) Then
                                CONTACTCITY = " "
                            Else
                                CONTACTCITY = dr("STRCONTACTCITY")
                            End If
                            If IsDBNull(dr("STRCONTACTSTATE")) Then
                                CONTACTSTATE = " "
                            Else
                                CONTACTSTATE = dr("STRCONTACTSTATE")
                            End If
                            If IsDBNull(dr("STRCONTACTZIPCODE")) Then
                                CONTACTZIPCODE = " "
                            Else
                                CONTACTZIPCODE = dr("STRCONTACTZIPCODE")
                            End If
                            If IsDBNull(dr("STRCONTACTFIRSTNAME")) Then
                                CONTACTFIRSTNAME = " "
                            Else
                                CONTACTFIRSTNAME = dr("STRCONTACTFIRSTNAME")
                            End If
                            If IsDBNull(dr("STRCONTACTLASTNAME")) Then
                                CONTACTLASTNAME = " "
                            Else
                                CONTACTLASTNAME = dr("STRCONTACTLASTNAME")
                            End If
                            If IsDBNull(dr("STRCONTACTEMAIL")) Then
                                CONTACTEMAIL = " "
                            Else
                                CONTACTEMAIL = dr("STRCONTACTEMAIL")
                            End If

                            SQL2 = "insert into " & DBNameSpace & ".EImailOut " & _
                           "(strAirsYear, " & _
                           "strAirsNumber, " & _
                           "STRFACILITYNAME, " & _
                           "STROPERATIONALSTATUS, " & _
                           "STRCLASS, " & _
                           "STRCONTACTCOMPANYNAME, " & _
                           "STRCONTACTADDRESS1, " & _
                           "STRCONTACTCITY, " & _
                           "STRCONTACTSTATE, " & _
                           "STRCONTACTZIPCODE, " & _
                           "STRCONTACTFIRSTNAME, " & _
                           "STRCONTACTLASTNAME, " & _
                           "STRCONTACTEMAIL, " & _
                           "STRINVENTORYYEAR) " & _
                           "values " & _
                           "('" & airsYear & "', " & _
                           "'" & airsNumber & "', " & _
                           "'" & Replace(FACILITYNAME, "'", "''") & "', " & _
                             "'" & Replace(OperationalStatus, "'", "''") & "', " & _
                           "'" & Replace(FacilityClass, "'", "''") & "', " & _
                           "'" & Replace(Replace(CONTACTCOMPANYNAME, "'", "''"), "N/A", " ") & "', " & _
                           "'" & Replace(Replace(CONTACTADDRESS1, "'", "''"), "N/A", " ") & "', " & _
                           "'" & Replace(Replace(CONTACTCITY, "'", "''"), "N/A", " ") & "', " & _
                           "'" & CONTACTSTATE & "', " & _
                           "'" & Replace(CONTACTZIPCODE, "N/A", " ") & "', " & _
                           "'" & Replace(Replace(CONTACTFIRSTNAME, "'", "''"), "N/A", " ") & "', " & _
                           "'" & Replace(Replace(CONTACTLASTNAME, "'", "''"), "N/A", " ") & "', " & _
                           "'" & Replace(Replace(CONTACTEMAIL, "'", "''"), "N/A", " ") & "', " & _
                           "'" & EIYear & "')"

                            cmd2 = New OracleCommand(SQL2, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr2 = cmd2.ExecuteReader
                            dr2.Close()
                        End While
                    End If

                    
                    Dim year As String = cboEIMailoutYear.SelectedItem

                    SQL = "SELECT STRAIRSNUMBER, " & _
                    "STRFACILITYNAME, " & _
                    "STROPERATIONALSTATUS, " & _
                    "STRCLASS, " & _
                    "STRCONTACTFIRSTNAME, " & _
                    "STRCONTACTLASTNAME, " & _
                    "STRCONTACTCOMPANYname, " & _
                    "STRCONTACTADDRESS1, " & _
                    "STRCONTACTCITY, " & _
                    "STRCONTACTSTATE, " & _
                    "STRCONTACTZIPCODE, " & _
                    "STRCONTACTEMAIL " & _
                    "from " & DBNameSpace & ".eIMailOut " & _
                    "where STRINVENTORYYEAR = '" & year & "' " & _
                    "order by STRFACILITYNAME"

                    dsViewCount = New DataSet
                    daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    daViewCount.Fill(dsViewCount, "ViewCount")
                    dgvEIDataCount.DataSource = dsViewCount
                    dgvEIDataCount.DataMember = "ViewCount"
                    dgvEIDataCount.RowHeadersVisible = False
                    dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvEIDataCount.AllowUserToResizeColumns = True
                    dgvEIDataCount.AllowUserToAddRows = False
                    dgvEIDataCount.AllowUserToDeleteRows = False
                    dgvEIDataCount.AllowUserToOrderColumns = True
                    dgvEIDataCount.AllowUserToResizeRows = True

                    dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
                    dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
                    dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
                    dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
                    dgvEIDataCount.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status"
                    dgvEIDataCount.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
                    dgvEIDataCount.Columns("STRCLASS").HeaderText = "Facility Class"
                    dgvEIDataCount.Columns("STRCLASS").DisplayIndex = 3
                    dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
                    dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
                    dgvEIDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
                    dgvEIDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
                    dgvEIDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
                    dgvEIDataCount.Columns("STRCONTACTCOMPANYname").DisplayIndex = 6
                    dgvEIDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
                    dgvEIDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 7
                    dgvEIDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
                    dgvEIDataCount.Columns("STRCONTACTCITY").DisplayIndex = 8
                    dgvEIDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
                    dgvEIDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 9
                    dgvEIDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
                    dgvEIDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
                    dgvEIDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
                    dgvEIDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 11

                    txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try

    End Sub
    Private Sub btnDeleteEIMailOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteEIMailOut.Click
        Try
            deleteEImailOutbyYear()
            cboEIMailoutYear.Items.Clear()
            loadEIMailOutYear()
            cboEIMailoutYear.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            
        End Try
    End Sub
    Private Sub deleteEImailOutbyYear()
        Dim EIyear As String = cboEIMailoutYear.SelectedItem

        Try

            

            If EIyear = "Select a Mailout Year & Click Below" Then
                MsgBox("You must select a Mailout Year", MsgBoxStyle.Information, "EIMailout")
            Else
                SQL = "delete from " & DBNameSpace & ".EImailout " & _
                "where " & DBNameSpace & ".EImailout.STRINVENTORYYEAR = '" & EIyear & "'"

                MsgBox("EI mail out is deleted!")

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try

    End Sub
    Private Sub BtnEIExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEIExportExcel.Click
        Try
            ExportEItoExcel()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try
    End Sub
    Sub ExportEItoExcel()
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        'Dim ExcelApp As New Excel.Application
        Dim i As Integer
        Dim j As Integer

        Try
            If dgvEIDataCount.RowCount <> 0 Then
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()


                    For i = 0 To dgvEIDataCount.ColumnCount - 1
                        .Cells(1, i + 1) = dgvEIDataCount.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvEIDataCount.ColumnCount - 1
                        For j = 0 To dgvEIDataCount.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvEIDataCount.Item(i, j).Value.ToString
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
    Private Sub btnEISave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISave.Click
        Try
            If txtEIFirstName.Text = "" Or txtEILastName.Text = "" Or txtEIcompanyName.Text = "" Or txtEIcontactAddress1.Text = "" Or txtEIcontactCity.Text = "" Or txtEIcontactState.Text = "" Or txtEIcontactZipCode.Text = "" Or txtEIcontactEmail.Text = "" Then
                MsgBox("Please enter required info.")

            Else
                SaveEIMailOut()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
              
        End Try
    End Sub
    Private Sub SaveEIMailOut()
        Dim SQL As String
        Dim AirsNo As String = txtEIAirsNo.Text
        Dim EIYear As String = txtEIYear.Text
        Dim airsYear As String = AirsNo & EIYear
        Dim EIFacilityName As String = txtEIFacilityName.Text
        Dim EIPrefix As String = txtEIprefix.Text
        Dim EIFirstName As String = txtEIFirstName.Text

        Dim EILastName As String = txtEILastName.Text
        Dim EICompanyName As String = txtEIcompanyName.Text
        Dim EIContactAddress1 As String = txtEIcontactAddress1.Text
        Dim EIContactAddress2 As String = txtEIcontactAddress2.Text

        Dim EIContactCity As String = txtEIcontactCity.Text
        Dim EIcontactState As String = txtEIcontactState.Text
        Dim EIContactZip As String = txtEIcontactZipCode.Text
        Dim EIContactEmail As String = txtEIcontactEmail.Text

        Try
            SQL = "Select strAIRSYear " & _
                  "from " & DBNameSpace & ".EIMailout " & _
                  "where STRAIRSYEAR = '" & airsYear & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read


            If recExist = True Then
                SQL = "update " & DBNameSpace & ".EIMailOut set " & _
                "" & DBNameSpace & ".EIMailOut.STRCONTACTPREFIX = '" & EIPrefix & "', " & _
                "" & DBNameSpace & ".EIMailOut.STRCONTACTFIRSTNAME = '" & EIFirstName & "', " & _
                "" & DBNameSpace & ".EIMailOut.STRCONTACTLASTNAME = '" & EILastName & "', " & _
                "" & DBNameSpace & ".EIMailOut.STRCONTACTCOMPANYNAME = '" & EICompanyName & "', " & _
                "" & DBNameSpace & ".EIMailOut.STRCONTACTADDRESS1 = '" & EIContactAddress1 & "', " & _
                "" & DBNameSpace & ".EIMailOut.STRCONTACTADDRESS2 = '" & EIContactAddress2 & "', " & _
                "" & DBNameSpace & ".EIMailOut.STRCONTACTCITY = '" & EIContactCity & "', " & _
                "" & DBNameSpace & ".EIMailOut.STRCONTACTSTATE = '" & EIcontactState & "', " & _
                "" & DBNameSpace & ".EIMailOut.STRCONTACTZIPCODE = '" & EIContactZip & "', " & _
                "" & DBNameSpace & ".EIMailOut.STRCONTACTEMAIL = '" & EIContactEmail & "'" & _
                "where " & DBNameSpace & ".EIMailOut.STRAIRSYEAR = '" & airsYear & "' "
                MsgBox("your info is updated")
            Else

                SQL = "Insert into " & DBNameSpace & ".EIMailOut" & _
                "(" & DBNameSpace & ".EIMailOut.STRAIRSYEAR, " & _
              "" & DBNameSpace & ".EIMailOut.STRAIRSNUMBER, " & _
              "" & DBNameSpace & ".EIMailOut.STRFACILITYNAME, " & _
              "" & DBNameSpace & ".EIMailOut.STRCONTACTPREFIX, " & _
              "" & DBNameSpace & ".EIMailOut.STRCONTACTFIRSTNAME, " & _
              "" & DBNameSpace & ".EIMailOut.STRCONTACTLASTNAME, " & _
              "" & DBNameSpace & ".EIMailOut.STRCONTACTCOMPANYNAME, " & _
              "" & DBNameSpace & ".EIMailOut.STRCONTACTADDRESS1, " & _
              "" & DBNameSpace & ".EIMailOut.STRCONTACTADDRESS2, " & _
              "" & DBNameSpace & ".EIMailOut.STRCONTACTCITY, " & _
              "" & DBNameSpace & ".EIMailOut.STRCONTACTSTATE, " & _
              "" & DBNameSpace & ".EIMailOut.STRCONTACTZIPCODE, " & _
              "" & DBNameSpace & ".EIMailOut.STRINVENTORYYEAR, " & _
              "" & DBNameSpace & ".EIMailOut.STRCONTACTEMAIL) " & _
                "values (" & _
                "'" & Replace(airsYear, "'", "''") & "', " & _
                "'" & Replace(AirsNo, "'", "''") & "', " & _
                "'" & Replace(EIFacilityName, "'", "''") & "', " & _
                "'" & Replace(EIPrefix, "'", "''") & "', " & _
                "'" & Replace(EIFirstName, "'", "''") & "', " & _
                "'" & Replace(EILastName, "'", "''") & "', " & _
                "'" & Replace(EICompanyName, "'", "''") & "', " & _
                "'" & Replace(EIContactAddress1, "'", "''") & "', " & _
                "'" & Replace(EIContactAddress2, "'", "''") & "', " & _
                "'" & Replace(EIContactCity, "'", "''") & "', " & _
                "'" & Replace(EIcontactState, "'", "''") & "', " & _
                "'" & Replace(EIContactZip, "'", "''") & "', " & _
                "'" & Replace(EIYear, "'", "''") & "', " & _
                "'" & Replace(EIContactEmail, "'", "''") & "' " & _
                " )"

                MsgBox("your info is added!")
            End If

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try
    End Sub
    Private Sub btnEIDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEIDelete.Click
        Try
            deleteEImailout()
            MsgBox("The info has been deleted!", MsgBoxStyle.Information, "EIMailout")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
              
        End Try
    End Sub
    Private Sub deleteEImailout()
        Dim SQL As String
        Dim AirsNo As String = txtEIAirsNo.Text
        Dim dr As OracleDataReader

        Dim EIyear As String = txtEIYear.Text

        Try
            SQL = "delete from " & DBNameSpace & ".EIMailOut " & _
          "where " & DBNameSpace & ".EIMailOut.STRAIRSNUMBER = '" & AirsNo & "' " & _
          " and " & DBNameSpace & ".EIMailOut.STRINVENTORYYEAR = '" & EIyear & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
             
        End Try
    End Sub
    Private Sub lblviewFinalized_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewFinalized.LinkClicked
        Try
            txtEIYear.Text = cboEIYear2.SelectedItem

            
            Dim year As String = txtEIYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT " & DBNameSpace & ".EISI.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".EISI.STRFACILITYNAME, " & _
            "case " & _
            "when substr(strFinalize, 2, 1) = '-' then '0'||substr(strFinalize, 1, 10) " & _
            "else substr(strFinalize, 1,11) " & _
            "end Finalize, " & _
            "" & DBNameSpace & ".EISI.STRfinalize, " & _
            "" & DBNameSpace & ".EISI.STRQADONE, " & _
            "" & DBNameSpace & ".EISI.STRCONFIRMATIONNumber " & _
            "from " & DBNameSpace & ".EISI, " & DBNameSpace & ".eimailout " & _
            "where " & DBNameSpace & ".EISI.strinventoryyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".EISI.STROPTOUT = 'NO' " & _
            "and " & DBNameSpace & ".EISI.strfinalize is not NULL " & _
            "and " & DBNameSpace & ".EIMAILOUT.STRAIRSYEAR = " & DBNameSpace & ".EISI.STRAIRSYEAR (+)" & _
            "order by " & DBNameSpace & ".eisi.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"
           
            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("Finalize").HeaderText = "Finalize Date"
            dgvEIDataCount.Columns("Finalize").DisplayIndex = 2
            dgvEIDataCount.Columns("Finalize").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvEIDataCount.Columns("STRQADONE").HeaderText = "QA Done Date"
            dgvEIDataCount.Columns("STRQADONE").DisplayIndex = 3
            dgvEIDataCount.Columns("STRCONFIRMATIONNumber").HeaderText = "Confirmation Number"
            dgvEIDataCount.Columns("STRCONFIRMATIONNumber").DisplayIndex = 4

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            
        End Try
    End Sub
    Private Sub lblviewInprocess_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewInprocess.LinkClicked
        Try
            txtEIYear.Text = cboEIYear2.SelectedItem

            Dim year As String = txtEIYear.Text

            SQL = "SELECT " & DBNameSpace & ".EISI.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".EISI.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".EISI.STRDATELASTLOGIN, " & _
            "" & DBNameSpace & ".EIMailOut.strContactEmail " & _
            "from " & DBNameSpace & ".EISI, " & DBNameSpace & ".eimailout " & _
            "where " & DBNameSpace & ".eImailout.STRINVENTORYYEAR = '" & year & "'" & _
            "and " & DBNameSpace & ".EISI.STROPTOUT = 'NO' " & _
            "and " & DBNameSpace & ".EISI.strfinalize is NULL " & _
            "and " & DBNameSpace & ".EIMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".EISI.STRAIRSYEAR " & _
            "order by " & DBNameSpace & ".eisi.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"
          
            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("STRDATELASTLOGIN").HeaderText = "Last Login Date"
            dgvEIDataCount.Columns("STRDATELASTLOGIN").DisplayIndex = 2
            dgvEIDataCount.Columns("strContactEmail").HeaderText = "Contact Email"
            dgvEIDataCount.Columns("strContactEmail").DisplayIndex = 3


            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
               
        End Try
    End Sub
    Private Sub lblEIviewselectedyearMailOutlist_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblEIviewselectedyearMailOutlist.LinkClicked
        
        Try
            Dim year As String = cboEIMailoutYear.SelectedItem

            SQL = "SELECT STRAIRSNUMBER, " & _
            "STRFACILITYNAME, " & _
            "STROPERATIONALSTATUS, " & _
            "STRCLASS, " & _
            "STRCONTACTFIRSTNAME, " & _
            "STRCONTACTLASTNAME, " & _
            "STRCONTACTCOMPANYname, " & _
            "STRCONTACTADDRESS1, " & _
            "STRCONTACTCITY, " & _
            "STRCONTACTSTATE, " & _
            "STRCONTACTZIPCODE, " & _
            "STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".eIMailOut " & _
            "where STRINVENTORYYEAR = '" & year & "' " & _
            "order by STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"
         
            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status"
            dgvEIDataCount.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
            dgvEIDataCount.Columns("STRCLASS").HeaderText = "Facility Class"
            dgvEIDataCount.Columns("STRCLASS").DisplayIndex = 3
            dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
            dgvEIDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvEIDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
            dgvEIDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
            dgvEIDataCount.Columns("STRCONTACTCOMPANYname").DisplayIndex = 6
            dgvEIDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
            dgvEIDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 7
            dgvEIDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvEIDataCount.Columns("STRCONTACTCITY").DisplayIndex = 8
            dgvEIDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvEIDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 9
            dgvEIDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvEIDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
            dgvEIDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvEIDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 11

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

             
        End Try

    End Sub
#End Region

    Private Sub MmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OpenDocumentationUrl(Me)
    End Sub
 
    Private Sub lblViewEmailData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEmailData.LinkClicked
        Try
            LoadUserInfo(txtWebUserEmail.Text)
            LoadUserFacilityInfo(txtWebUserEmail.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub cboUserEmail_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUserEmail.SelectedValueChanged
        Try
            txtWebUserEmail.Text = cboUserEmail.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    
    Private Sub btnEnroll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnroll.Click
        Try
            If cboEIEnrollmentYear.Text = "" Then
                MsgBox("Please choose a Year!", MsgBoxStyle.Information, "EI Enrollment")
            Else
                Mailoutenrollment()
                cboEIEnrollmentYear.Items.Clear()
                loadEIEnrollmentYear()
                cboEIEnrollmentYear.Text = ""
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub Mailoutenrollment()
        Dim AirsNo As String
        Dim FacilityName As String
        Dim EIYear As Integer = CInt(cboEIEnrollmentYear.SelectedItem)
        Dim airsYear As String

        Try
            SQL = "Select * " & _
            "FROM " & DBNameSpace & ".EISI " & _
            "where STRINVENTORYYEAR = '" & EIYear & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                MsgBox("That year " & EIYear & " is already enrolled.", MsgBoxStyle.Information, "EI Enrollment")
            Else
                SQL = "Select EImailout.STRAIRSNUMBER, eimailout.STRFACILITYNAME " & _
                "FROM " & DBNameSpace & ".EImailout " & _
                "where EImailout.STRINVENTORYYEAR = '" & EIYear & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Read()
                Do
                    AirsNo = dr("strAirsNumber")
                    airsYear = AirsNo & EIYear
                    FacilityName = dr("STRFACILITYNAME")
                    SQL2 = "Insert into " & DBNameSpace & ".EISI " & _
                    "(EISI.STRAIRSNUMBER, " & _
                    "EISI.STRFACILITYNAME, " & _
                    "EISI.STRTRANSACTIONDATE, " & _
                    "EISI.STRINVENTORYYEAR, " & _
                    "EISI.NUMUSERID, " & _
                    "EISI.STRAIRSYEAR) " & _
                    "values " & _
                    "('" & Replace(AirsNo, "'", "''") & "', " & _
                    "'" & Replace(FacilityName, "'", "''") & "', " & _
                    "'" & OracleDate & "', " & _
                    "'" & Replace(EIYear, "'", "''") & "', " & _
                    "'3', " & _
                    "'" & airsYear & "' )"

                    cmd2 = New OracleCommand(SQL2, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                Loop While dr.Read
                MsgBox("The facilities for year " & EIYear & " have been enrolled", MsgBoxStyle.Information, "EI Enrollment")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeEnroll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeEnroll.Click

        Dim EIYear As Integer = CInt(cboEIEnrollmentYear.SelectedItem)
        Dim sql As String
        Try
            If cboEIEnrollmentYear.Text = "" Then
                MsgBox("Please choose a year!", MsgBoxStyle.Information, "EI Enrollment")
            Else
                Dim intAnswer As DialogResult
                intAnswer = MessageBox.Show("Remove the enrollment?", "EI Enrollment", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                Select Case intAnswer
                    Case Windows.Forms.DialogResult.OK
                        sql = "delete from " & DBNameSpace & ".EISI " & _
                        "where EISI.STRINVENTORYYEAR = '" & EIYear & "'"
                        cmd = New OracleCommand(sql, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                        MsgBox("Enrollment has been removed!", MsgBoxStyle.Information, "EI Enrollment")
                    Case Else
                        MsgBox("Enrollment has not been removed!", MsgBoxStyle.Information, "EI Enrollment")
                End Select

                cboEIEnrollmentYear.Items.Clear()
                loadEIEnrollmentYear()
                cboEIEnrollmentYear.Text = ""
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnaddEIlist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddEIlist.Click
        Try
            addonefacilityEI()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub addonefacilityEI()
        Dim AirsNo As String = "0413" & txtEIairsNumber.Text
        Dim EIYear As Integer = txtaddEIYear.Text
        Dim airsYear As String = AirsNo & EIYear
        Dim facilityName As String


        Try

            SQL = "Select " & DBNameSpace & ".EISI.STRINVENTORYYEAR " & _
          "FROM " & DBNameSpace & ".EISI " & _
          "where  EISI.STRINVENTORYYEAR = '" & EIYear & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read


            If recExist = True Then


                SQL = "Select " & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME " & _
               "FROM " & DBNameSpace & ".APBFACILITYINFORMATION " & _
               "where  APBFACILITYINFORMATION.STRAIRSNUMBER = '" & AirsNo & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read


                If recExist = True Then
                    facilityName = dr("STRFACILITYNAME")

                    SQL = "Select * " & _
                             "FROM " & DBNameSpace & ".EISI " & _
                             "where STRINVENTORYYEAR = '" & EIYear & "' " & _
                             " And STRAIRSNUMBER = '" & AirsNo & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read

                    If recExist = True Then
                        MsgBox("This facility (" & AirsNo & ") is already enrolled for " & EIYear & ".", MsgBoxStyle.Information, "EI Enrollment")
                    Else
                        SQL2 = "Insert into " & DBNameSpace & ".EISI " & _
                        "(EISI.STRAIRSNUMBER, " & _
                        "EISI.STRFACILITYNAME, " & _
                        "EISI.STRTRANSACTIONDATE, " & _
                        "EISI.STRINVENTORYYEAR, " & _
                        "EISI.NUMUSERID, " & _
                        "EISI.STRAIRSYEAR) " & _
                        "values " & _
                        "('" & Replace(AirsNo, "'", "''") & "', " & _
                        "'" & Replace(facilityName, "'", "''") & "', " & _
                        "'" & OracleDate & "', " & _
                        "'" & Replace(EIYear, "'", "''") & "', " & _
                        "'3', " & _
                        "'" & airsYear & "' )"

                        cmd2 = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        MsgBox("This facility (" & AirsNo & ") have been enrolled for " & EIYear & ".", MsgBoxStyle.Information, "EI Enrollment")
                    End If

                Else

                    MsgBox("This Airs Number (" & AirsNo & ") is not valid. Please enter valid Airs Number.", MsgBoxStyle.Information, "EI Enrollment")
                End If
            Else
                MsgBox("This year (" & EIYear & ") have not been enrolled.", MsgBoxStyle.Information, "EI Enrollment")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnremoveEIlist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremoveEIlist.Click
        Dim EIYear As Integer = txtaddEIYear.Text
        Dim AirsNo As String = "0413" & txtEIairsNumber.Text

        Dim sql As String
        Try

            Dim intAnswer As DialogResult
            intAnswer = MessageBox.Show("Remove this facility (" & AirsNo & ") for " & EIYear & "?", "EI Enrollment", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
            Select Case intAnswer
                Case Windows.Forms.DialogResult.OK
                    sql = "Select " & _
                    "case " & _
                    "when strOptOut is null then 'Delete' " & _
                    "when strOptOut = 'YES' then 'OPTED OUT' " & _
                    "when strOptOut = 'NO' then 'OPTED IN' " & _
                    "End OptOut " & _
                    "from " & DBNameSpace & ".EISI " & _
                    "where strAIRSNumber = '" & AirsNo & "' " & _
                    "and strInventoryYear = '" & EIYear & "' "
                    cmd = New OracleCommand(sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("OptOut")) Then
                            temp = "Delete"
                        Else
                            temp = dr.Item("OptOut")
                        End If
                    End While
                    dr.Close()

                    Select Case temp
                        Case "OPTED IN"
                            MsgBox("This facility has already opted in and cannot be deleted unless this is changed.", MsgBoxStyle.Exclamation, "DMU Staff")
                            Exit Sub
                        Case "OPTED OUT"
                            MsgBox("This facility has already opted out and cannot be deleted unless this is changed.", MsgBoxStyle.Exclamation, "DMU Staff")
                            Exit Sub
                        Case Else

                    End Select

                    sql = "delete from " & DBNameSpace & ".EISI " & _
                    "where EISI.STRINVENTORYYEAR = '" & EIYear & "'" & _
                    " And STROPTOUT is null" & _
                    " And STRAIRSNUMBER = '" & AirsNo & "'"
                    cmd = New OracleCommand(sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    MsgBox("This Facility (" & AirsNo & ") has been removed for " & EIYear & "!", MsgBoxStyle.Information, "EI Enrollment")
                Case Else
                    MsgBox("This Facility (" & AirsNo & ") has not been removed for " & EIYear & "!", MsgBoxStyle.Information, "EI Enrollment")
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewEIExtraResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewEIExtraResponse.LinkClicked
        Try

            Dim year As String = txtEIYear.Text


            SQL = "SELECT dt_NotInMailout.SchemaAIRS, " & DBNameSpace & ".EISI.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".EISI.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTCOMPANYNAME, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTEMAIL, " & _
            "" & DBNameSpace & ".EISI.STRCONTACTPHONENUMBER1 " & _
            "from (Select " & DBNameSpace & ".EISI.STRAIRSYEAR AS SchemaAIRS, " & _
            "" & DBNameSpace & ".EIMailout.STRAIRSYEAR AS MailoutAIRS" & _
            " From " & DBNameSpace & ".EIMailout, " & DBNameSpace & ".EISI" & _
            " Where " & DBNameSpace & ".EIMailout.STRAIRSYEAR (+)= " & DBNameSpace & ".EISI.STRAIRSYEAR " & _
            "AND " & DBNameSpace & ".EISI.STRINVENTORYYEAR=  '" & year & "' " & _
            "AND " & DBNameSpace & ".EISI.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & _
            "" & DBNameSpace & ".EISI " & _
            "Where " & DBNameSpace & ".EISI.STRAIRSYEAR = SchemaAIRS " & _
            "AND MailoutAIRS is NULL"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"

            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvEIDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvEIDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvEIDataCount.Columns("STRCONTACTCOMPANYNAME").HeaderText = "Contact Company"
            dgvEIDataCount.Columns("STRCONTACTCOMPANYNAME").DisplayIndex = 4
            dgvEIDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvEIDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 5
            dgvEIDataCount.Columns("STRCONTACTPHONENUMBER1").HeaderText = "Phone No."
            dgvEIDataCount.Columns("STRCONTACTPHONENUMBER1").DisplayIndex = 6

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString

            'txtEIRecordNumber.Text = txtextraResponse.Text
            'clearEIData()
            'ClearMailOut()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewRemovedFacilities_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewRemovedFacilities.LinkClicked
        Try

            Dim year As String = txtEIYear.Text


            SQL = "SELECT " & DBNameSpace & ".eiMailOut.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".eiMailOut.STRFACILITYNAME " & _
            "from  " & DBNameSpace & ".eImailout, " & DBNameSpace & ".EISI " & _
            "where " & DBNameSpace & ".eImailout.STRINVENTORYYEAR = '" & year & "' " & _
            "and " & DBNameSpace & ".eImailout.STRAIRSYEAR = " & DBNameSpace & ".EISI.STRAIRSYEAR(+) " & _
            "and " & DBNameSpace & ".EISI.STRAIRSYEAR is NULL"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"

            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString
            'clearEIData()
            'ClearMailOut()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewMailoutNonResponses_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewMailoutNonResponses.LinkClicked
        Try

            Dim year As String = txtEIYear.Text


            SQL = "SELECT " & DBNameSpace & ".eiMailOut.STRAIRSNUMBER, " & _
             "" & DBNameSpace & ".eiMailOut.STRFACILITYNAME, " & _
             "" & DBNameSpace & ".eiMailOut.STRCONTACTFIRSTNAME, " & _
             "" & DBNameSpace & ".eiMailOut.STRCONTACTLASTNAME, " & _
             "" & DBNameSpace & ".eiMailOut.STRCONTACTCOMPANYname, " & _
             "" & DBNameSpace & ".eiMailOut.STRCONTACTADDRESS1, " & _
             "" & DBNameSpace & ".eiMailOut.STRCONTACTCITY, " & _
             "" & DBNameSpace & ".eiMailOut.STRCONTACTSTATE, " & _
             "" & DBNameSpace & ".eiMailOut.STRCONTACTZIPCODE, " & _
              "" & DBNameSpace & ".eiMailOut.STRCONTACTEMAIL " & _
            "from  " & DBNameSpace & ".eImailout, " & DBNameSpace & ".EISI " & _
             "where " & DBNameSpace & ".eImailout.STRINVENTORYYEAR = '" & year & "' " & _
             "and " & DBNameSpace & ".eImailout.STRAIRSYEAR = " & DBNameSpace & ".EISI.STRAIRSYEAR(+) " & _
             "and " & DBNameSpace & ".EISI.strOptOut is NULL"


            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"

            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvEIDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvEIDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvEIDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvEIDataCount.Columns("STRCONTACTCOMPANYNAME").HeaderText = "Contact Company"
            dgvEIDataCount.Columns("STRCONTACTCOMPANYNAME").DisplayIndex = 4
            dgvEIDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Contact Address"
            dgvEIDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 5
            dgvEIDataCount.Columns("STRCONTACTCITY").HeaderText = "Contact City"
            dgvEIDataCount.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvEIDataCount.Columns("STRCONTACTSTATE").HeaderText = "Contact State"
            dgvEIDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvEIDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Contact Zip"
            dgvEIDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvEIDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvEIDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 9

            txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString

            'clearEIData()
            'ClearMailOut()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewExtraNonResponses_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewExtraNonResponses.LinkClicked
        Try

            Dim year As String = txtEIYear.Text


            SQL = "SELECT " & DBNameSpace & ".EISI.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".EISI.STRFACILITYNAME " & _
           "from " & DBNameSpace & ".EISI " & _
                " where  not exists (select * from " & DBNameSpace & ".EIMAILOUT " & _
                " where " & DBNameSpace & ".EISI.STRAIRSNUMBER = " & DBNameSpace & ".EIMAILOUT.STRAIRSNUMBER" & _
                " and EISI.STRINVENTORYYEAR = EIMAILOUT.STRINVENTORYYEAR) " & _
                " and " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & year & "' " & _
                " and " & DBNameSpace & ".EISI.STROPTOUT is null"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIDataCount.DataSource = dsViewCount
            dgvEIDataCount.DataMember = "ViewCount"

            dgvEIDataCount.RowHeadersVisible = False
            dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIDataCount.AllowUserToResizeColumns = True
            dgvEIDataCount.AllowUserToAddRows = False
            dgvEIDataCount.AllowUserToDeleteRows = False
            dgvEIDataCount.AllowUserToOrderColumns = True
            dgvEIDataCount.AllowUserToResizeRows = True

            dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1

            txtextraResponse.Text = dgvEIDataCount.RowCount.ToString

            txtEIRecordNumber.Text = txtExtraNonResponses.Text
            'clearEIData()
            'ClearMailOut()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewEnrollmentlist_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEnrollmentlist.LinkClicked
        Try

            Dim year As String = cboEIEnrollmentYear.Text

            If cboEIEnrollmentYear.Text = "" Then

                MsgBox("Please choose a year to view!", MsgBoxStyle.Information, "EI Enrollment")

            Else
                SQL = "SELECT " & DBNameSpace & ".EISI.STRAIRSNUMBER, " & _
        "" & DBNameSpace & ".EISI.STRFACILITYNAME, " & _
        "" & DBNameSpace & ".EISI.STRTRANSACTIONDATE " & _
        "from " & DBNameSpace & ".EISI " & _
        "where " & DBNameSpace & ".EISI.STRINVENTORYYEAR = '" & year & "'"

                dsViewCount = New DataSet
                daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvEIDataCount.DataSource = dsViewCount
                dgvEIDataCount.DataMember = "ViewCount"

                dgvEIDataCount.RowHeadersVisible = False
                dgvEIDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvEIDataCount.AllowUserToResizeColumns = True
                dgvEIDataCount.AllowUserToAddRows = False
                dgvEIDataCount.AllowUserToDeleteRows = False
                dgvEIDataCount.AllowUserToOrderColumns = True
                dgvEIDataCount.AllowUserToResizeRows = True

                dgvEIDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
                dgvEIDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
                dgvEIDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvEIDataCount.Columns("strFacilityName").DisplayIndex = 1
                dgvEIDataCount.Columns("STRTRANSACTIONDATE").HeaderText = "Transaction Date"
                dgvEIDataCount.Columns("STRTRANSACTIONDATE").DisplayIndex = 2

                txtEIRecordNumber.Text = dgvEIDataCount.RowCount.ToString

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btncheckEnrollment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncheckEnrollment.Click
        Dim AirsNo As String = "0413" & txtEIairsNumber.Text
        Dim EIYear As Integer = txtaddEIYear.Text
        Dim airsYear As String = AirsNo & EIYear

        Try
            SQL = "Select * " & _
            "FROM " & DBNameSpace & ".EISI " & _
            "where STRINVENTORYYEAR = '" & EIYear & "' " & _
            " And STRAIRSNUMBER = '" & AirsNo & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                MsgBox("This facility (" & AirsNo & ") have been enrolled for " & EIYear & "!", MsgBoxStyle.Information, "EI Enrollment")
            Else
                MsgBox("This facility (" & AirsNo & ") is not enrolled for " & EIYear & "!", MsgBoxStyle.Information, "EI Enrollment")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnESenrollment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnESenrollment.Click
        Try
            If cboESYear.Text = "" Then
                MsgBox("Please choose a Year!", MsgBoxStyle.Information, "ES Enrollment")
            Else
                ESMailoutenrollment()
                cboESYear.Items.Clear()
                loadESEnrollmentYear()
                cboESYear.Text = ""
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub ESMailoutenrollment()
        Dim AirsNo As String
        Dim FacilityName As String
        Dim ESYear As Integer = CInt(cboESYear.SelectedItem)
        Dim airsYear As String

        Try
            SQL = "Select * " & _
            "FROM " & DBNameSpace & ".ESSCHEMA " & _
            "where ESSCHEMA.INTESYEAR = '" & ESYear & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                MsgBox("That year " & ESYear & " is already enrolled.", MsgBoxStyle.Information, "EI Enrollment")
            Else
                SQL = "Select ESMAILOUT.STRAIRSNUMBER, ESMAILOUT.STRFACILITYNAME " & _
                "FROM " & DBNameSpace & ".ESMAILOUT " & _
                "where ESMAILOUT.STRESYEAR = '" & ESYear & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Read()
                Do
                    AirsNo = dr("strAirsNumber")
                    airsYear = AirsNo & ESYear
                    FacilityName = dr("STRFACILITYNAME")
                    SQL2 = "Insert into " & DBNameSpace & ".ESSCHEMA " & _
                    "(ESSCHEMA.STRAIRSNUMBER, " & _
                    "ESSCHEMA.STRFACILITYNAME, " & _
                    "ESSCHEMA.DATTRANSACTION, " & _
                    "ESSCHEMA.INTESYEAR, " & _
                    "ESSCHEMA.NUMUSERID, " & _
                    "ESSCHEMA.STRAIRSYEAR) " & _
                    "values " & _
                    "('" & Replace(AirsNo, "'", "''") & "', " & _
                    "'" & Replace(FacilityName, "'", "''") & "', " & _
                    "'" & OracleDate & "', " & _
                    "'" & Replace(ESYear, "'", "''") & "', " & _
                    "'3', " & _
                    "'" & airsYear & "' )"

                    cmd2 = New OracleCommand(SQL2, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                Loop While dr.Read
                MsgBox("The facilities for year " & ESYear & " have been enrolled", MsgBoxStyle.Information, "EI Enrollment")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnESdeenrollment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnESdeenrollment.Click
        Dim ESYear As Integer = CInt(cboESYear.SelectedItem)
        Dim sql As String
        Try
            If cboESYear.Text = "" Then
                MsgBox("Please choose a year!", MsgBoxStyle.Information, "ES Enrollment")
            Else
                Dim intAnswer As DialogResult
                intAnswer = MessageBox.Show("Remove the enrollment?", "ES Enrollment", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                Select Case intAnswer
                    Case Windows.Forms.DialogResult.OK
                        sql = "delete from " & DBNameSpace & ".ESSCHEMA " & _
                        "where ESSCHEMA.INTESYEAR = '" & ESYear & "'"
                        cmd = New OracleCommand(sql, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                        MsgBox("Enrollment has been removed!", MsgBoxStyle.Information, "ES Enrollment")
                    Case Else
                        MsgBox("Enrollment has not been removed!", MsgBoxStyle.Information, "ES Enrollment")
                End Select

                cboESYear.Items.Clear()
                loadESEnrollmentYear()
                cboESYear.Text = ""
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnaddfacilitytoES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddfacilitytoES.Click
        Try
            addonefacilityES()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub addonefacilityES()
        Dim AirsNo As String = "0413" & txtESairNumber.Text
        Dim ESYear As Integer = txtESYearforFacility.Text
        Dim airsYear As String = AirsNo & ESYear
        Dim facilityName As String


        Try

            SQL = "Select " & DBNameSpace & ".ESSCHEMA.INTESYEAR " & _
          "FROM " & DBNameSpace & ".ESSCHEMA " & _
          "where  ESSCHEMA.INTESYEAR = '" & ESYear & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read


            If recExist = True Then


                SQL = "Select " & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME " & _
               "FROM " & DBNameSpace & ".APBFACILITYINFORMATION " & _
               "where  APBFACILITYINFORMATION.STRAIRSNUMBER = '" & AirsNo & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read


                If recExist = True Then
                    facilityName = dr("STRFACILITYNAME")

                    SQL = "Select * " & _
                             "FROM " & DBNameSpace & ".ESSCHEMA " & _
                             "where ESSCHEMA.INTESYEAR = '" & ESYear & "' " & _
                             " And ESSCHEMA.STRAIRSNUMBER = '" & AirsNo & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read

                    If recExist = True Then
                        MsgBox("This facility (" & AirsNo & ") is already enrolled for " & ESYear & ".", MsgBoxStyle.Information, "ES Enrollment")
                    Else
                        SQL2 = "Insert into " & DBNameSpace & ".ESSCHEMA " & _
                        "(ESSCHEMA.STRAIRSNUMBER, " & _
                        "ESSCHEMA.STRFACILITYNAME, " & _
                        "ESSCHEMA.DATTRANSACTION, " & _
                        "ESSCHEMA.INTESYEAR, " & _
                        "ESSCHEMA.NUMUSERID, " & _
                        "ESSCHEMA.STRAIRSYEAR) " & _
                        "values " & _
                        "('" & Replace(AirsNo, "'", "''") & "', " & _
                        "'" & Replace(facilityName, "'", "''") & "', " & _
                        "'" & OracleDate & "', " & _
                        "'" & Replace(ESYear, "'", "''") & "', " & _
                        "'3', " & _
                        "'" & airsYear & "' )"

                        cmd2 = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        MsgBox("This facility (" & AirsNo & ") has been enrolled for " & ESYear & ".", MsgBoxStyle.Information, "ES Enrollment")
                    End If

                Else

                    MsgBox("This Airs Number (" & AirsNo & ") is not valid. Please enter valid Airs Number.", MsgBoxStyle.Information, "ES Enrollment")
                End If
            Else
                MsgBox("This year (" & ESYear & ") has not been enrolled.", MsgBoxStyle.Information, "ES Enrollment")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnremoveFacilityES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremoveFacilityES.Click
        Dim ESYear As Integer = txtESYearforFacility.Text
        Dim AirsNo As String = "0413" & txtESairNumber.Text

        Dim sql As String
        Try

            Dim intAnswer As DialogResult
            intAnswer = MessageBox.Show("Remove this facility (" & AirsNo & ") for " & ESYear & "?", "ES Enrollment", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
            Select Case intAnswer
                Case Windows.Forms.DialogResult.OK
                    sql = "delete from " & DBNameSpace & ".ESSCHEMA " & _
                    "where ESSCHEMA.INTESYEAR = '" & ESYear & "'" & _
                    " And ESSCHEMA.STRAIRSNUMBER = '" & AirsNo & "'"
                    cmd = New OracleCommand(sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    MsgBox("This Facility (" & AirsNo & ") has been removed for " & ESYear & "!", MsgBoxStyle.Information, "ES Enrollment")
                Case Else
                    MsgBox("This Facility (" & AirsNo & ") has not been removed for " & ESYear & "!", MsgBoxStyle.Information, "ES Enrollment")
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnCheckESstatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckESstatus.Click
        Dim AirsNo As String = "0413" & txtESairNumber.Text
        Dim ESYear As Integer = txtESYearforFacility.Text
        Dim airsYear As String = AirsNo & ESYear

        Try
            SQL = "Select strAIRSYear as RowCount " & _
            "FROM " & DBNameSpace & ".ESSCHEMA " & _
            "where " & DBNameSpace & ".ESSCHEMA.INTESYEAR = '" & ESYear & "' " & _
            " And " & DBNameSpace & ".ESSCHEMA.STRAIRSNUMBER = '" & AirsNo & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read

            If recExist = True Then
                MsgBox("This facility (" & AirsNo & ") has been enrolled for " & ESYear & "!", MsgBoxStyle.Information, "ES Enrollment")
            Else
                MsgBox("This facility (" & AirsNo & ") is not enrolled for " & ESYear & "!", MsgBoxStyle.Information, "ES Enrollment")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewESenrollment_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewESenrollment.LinkClicked
        Try

            Dim year As String = cboESYear.Text

            If cboESYear.Text = "" Then

                MsgBox("Please choose a year to view!", MsgBoxStyle.Information, "ES Enrollment")

            Else
                SQL = "SELECT " & DBNameSpace & ".ESSCHEMA.STRAIRSNUMBER, " & _
        "" & DBNameSpace & ".ESSCHEMA.STRFACILITYNAME, " & _
        "" & DBNameSpace & ".ESSCHEMA.DATTRANSACTION " & _
        "from " & DBNameSpace & ".ESSCHEMA " & _
        "where " & DBNameSpace & ".ESSCHEMA.INTESYEAR = '" & year & "'"

                dsViewCount = New DataSet
                daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvESDataCount.DataSource = dsViewCount
                dgvESDataCount.DataMember = "ViewCount"

                dgvESDataCount.RowHeadersVisible = False
                dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvESDataCount.AllowUserToResizeColumns = True
                dgvESDataCount.AllowUserToAddRows = False
                dgvESDataCount.AllowUserToDeleteRows = False
                dgvESDataCount.AllowUserToOrderColumns = True
                dgvESDataCount.AllowUserToResizeRows = True

                dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
                dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
                dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
                dgvESDataCount.Columns("DATTRANSACTION").HeaderText = "Transaction Date"
                dgvESDataCount.Columns("DATTRANSACTION").DisplayIndex = 2

                txtRecordNumber.Text = dgvESDataCount.RowCount.ToString



            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewESextraresponder_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewESextraresponder.LinkClicked
        Try

            Dim year As String = txtESYear.Text
            Dim intyear As Integer = Int(year)

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".esSchema.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTCOMPANY, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTEMAIL, " & _
            "" & DBNameSpace & ".esSchema.STRCONTACTPHONENUMBER " & _
            "from (Select " & DBNameSpace & ".ESSCHEMA.STRAIRSNUMBER AS SchemaAIRS, " & _
            "" & DBNameSpace & ".ESMAILOUT.STRAIRSNUMBER AS MailoutAIRS" & _
            " From " & DBNameSpace & ".ESMailout, " & DBNameSpace & ".ESSCHEMA" & _
            " Where " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR (+)= " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR " & _
            "AND ESSCHEMA.INTESYEAR=  '" & intyear & "' " & _
            "AND " & DBNameSpace & ".ESSCHEMA.STROPTOUT IS NOT NULL) " & _
            "dt_NotInMailout, " & _
            "" & DBNameSpace & ".ESSCHEMA " & _
            "Where " & DBNameSpace & ".ESSCHEMA.STRAIRSNUMBER = SchemaAIRS " & _
            "AND MailoutAIRS is NULL"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvESDataCount.Columns("STRCONTACTCOMPANY").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTCOMPANY").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 5
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").HeaderText = "Phone No."
            dgvESDataCount.Columns("STRCONTACTPHONENUMBER").DisplayIndex = 6

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString
            '    txtextraResponse.Text = dgvESDataCount.RowCount.ToString

            clearESData()
            ClearMailOut()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewESremovedfacility_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewESremovedfacility.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT " & DBNameSpace & ".ESMAILOUT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".ESMAILOUT.STRFACILITYNAME " & _
            "from " & DBNameSpace & ".esSchema, " & DBNameSpace & ".esmailout " & _
            "where " & DBNameSpace & ".esMailOut.strESyear = '" & intYear & "' " & _
            "and " & DBNameSpace & ".esSchema.STRAIRSYEAR is null " & _
            "and " & DBNameSpace & ".ESMAILOUT.STRAIRSYEAR = " & DBNameSpace & ".ESSCHEMA.STRAIRSYEAR(+) " & _
            "order by " & DBNameSpace & ".ESMAILOUT.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewmailoutnonresponder_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewmailoutnonresponder.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT " & DBNameSpace & ".esMailOut.STRAIRSNUMBER, " & _
             "" & DBNameSpace & ".esMailOut.STRFACILITYNAME, " & _
             "" & DBNameSpace & ".esMailOut.STRCONTACTFIRSTNAME, " & _
             "" & DBNameSpace & ".esMailOut.STRCONTACTLASTNAME, " & _
             "" & DBNameSpace & ".esMailOut.STRCONTACTCOMPANYname, " & _
             "" & DBNameSpace & ".esMailOut.STRCONTACTADDRESS1, " & _
             "" & DBNameSpace & ".esMailOut.STRCONTACTCITY, " & _
             "" & DBNameSpace & ".esMailOut.STRCONTACTSTATE, " & _
             "" & DBNameSpace & ".esMailOut.STRCONTACTZIPCODE, " & _
             "" & DBNameSpace & ".esMailOut.STRCONTACTEMAIL " & _
            "from  " & DBNameSpace & ".esmailout, " & DBNameSpace & ".ESSchema " & _
            "where " & DBNameSpace & ".esmailout.strESYEAR = '" & intYear & "' " & _
            "and " & DBNameSpace & ".esmailout.STRAIRSYEAR = " & DBNameSpace & ".ESSchema.STRAIRSYEAR(+) " & _
            "and " & DBNameSpace & ".ESSchema.strOptOut is NULL " & _
            "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvESDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvESDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvESDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvESDataCount.Columns("STRCONTACTCOMPANYNAME").HeaderText = "Contact Company"
            dgvESDataCount.Columns("STRCONTACTCOMPANYNAME").DisplayIndex = 4
            dgvESDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Contact Address"
            dgvESDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 5
            dgvESDataCount.Columns("STRCONTACTCITY").HeaderText = "Contact City"
            dgvESDataCount.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvESDataCount.Columns("STRCONTACTSTATE").HeaderText = "Contact State"
            dgvESDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvESDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Contact Zip"
            dgvESDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvESDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvESDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 9

            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewextraNonresponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewextraNonresponse.LinkClicked
        txtESYear.Text = cboYear.SelectedItem
        Try

            Dim year As String = txtESYear.Text
            Dim intYear As Integer = Int(year)

            SQL = "SELECT " & DBNameSpace & ".esSchema.STRAIRSNUMBER, " & _
                "" & DBNameSpace & ".esSchema.STRFACILITYNAME " & _
                "from " & DBNameSpace & ".ESSchema " & _
                " where  not exists (select * from " & DBNameSpace & ".ESMAILOUT " & _
                " where " & DBNameSpace & ".ESSchema.STRAIRSNUMBER = " & DBNameSpace & ".ESMAILOUT.STRAIRSNUMBER" & _
                " and ESSchema.INTESYEAR = ESMAILOUT.strESYEAR) " & _
                " and " & DBNameSpace & ".ESSchema.INTESYEAR = '" & intYear & "' " & _
                " and " & DBNameSpace & ".ESSchema.STROPTOUT is null   " & _
                "order by " & DBNameSpace & ".esSchema.STRFACILITYNAME"


            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvESDataCount.DataSource = dsViewCount
            dgvESDataCount.DataMember = "ViewCount"

            dgvESDataCount.RowHeadersVisible = False
            dgvESDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvESDataCount.AllowUserToResizeColumns = True
            dgvESDataCount.AllowUserToAddRows = False
            dgvESDataCount.AllowUserToDeleteRows = False
            dgvESDataCount.AllowUserToOrderColumns = True
            dgvESDataCount.AllowUserToResizeRows = True

            dgvESDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvESDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvESDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvESDataCount.Columns("strFacilityName").DisplayIndex = 1
            txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewEIThreshold_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewEIThreshold.LinkClicked

        Try

            Dim EItype As String = cboEItype.SelectedItem
            Dim EItypeYear As String = lblEITypeYear.Text

            SQL = "SELECT " & DBNameSpace & ".EITHRESHOLDYEARS.STREITYPE " & _
                   "from  " & DBNameSpace & ".EITHRESHOLDYEARS  " & _
          "where " & DBNameSpace & ".EITHRESHOLDYEARS.STRYEAR = '" & EItypeYear & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr("STREITYPE")) Then
                    cboEItype.Text = ""
                Else
                    cboEItype.Text = dr("STREITYPE")
                End If
                EItype = cboEItype.SelectedItem
            End While
            SQL = "SELECT " & DBNameSpace & ".EITHRESHOLDS.STRPOLLUTANT, " & _
            "" & DBNameSpace & ".EITHRESHOLDS.NUMTHRESHOLD, " & _
                "" & DBNameSpace & ".EITHRESHOLDS.NUMTHRESHOLDNAA " & _
            "from  " & DBNameSpace & ".EITHRESHOLDS  " & _
            "where " & DBNameSpace & ".EITHRESHOLDS.STRTYPE = '" & EItype & "' " & _
             " order by " & DBNameSpace & ".EITHRESHOLDS.STRPOLLUTANT"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIThreshold.DataSource = dsViewCount
            dgvEIThreshold.DataMember = "ViewCount"

            dgvEIThreshold.RowHeadersVisible = False
            dgvEIThreshold.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIThreshold.AllowUserToResizeColumns = True
            dgvEIThreshold.AllowUserToAddRows = False
            dgvEIThreshold.AllowUserToDeleteRows = False
            dgvEIThreshold.AllowUserToOrderColumns = True
            dgvEIThreshold.AllowUserToResizeRows = True

            dgvEIThreshold.Columns("STRPOLLUTANT").HeaderText = "Pollutant Name"
            dgvEIThreshold.Columns("STRPOLLUTANT").DisplayIndex = 0
            dgvEIThreshold.Columns("NUMTHRESHOLD").HeaderText = "Threshold (TPY)"
            dgvEIThreshold.Columns("NUMTHRESHOLD").DisplayIndex = 1
            dgvEIThreshold.Columns("NUMTHRESHOLDNAA").HeaderText = "Threshold in NAA (TPY)"
            dgvEIThreshold.Columns("NUMTHRESHOLDNAA").DisplayIndex = 2


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub cboEIMailoutYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEIMailoutYear.TextChanged
        Try
            lblEITypeYear.Text = cboEIMailoutYear.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveEIType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveEIType.Click
        Dim EItype As String = cboEItype.Text
        Dim EItypeYear As String = lblEITypeYear.Text


        Try

            SQL = "Select " & DBNameSpace & ".EITHRESHOLDYEARS.STRYEAR " & _
          "FROM " & DBNameSpace & ".EITHRESHOLDYEARS " & _
          "where  EITHRESHOLDYEARS.STRYEAR = '" & EItypeYear & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                SQL = "Update " & DBNameSpace & ".EITHRESHOLDYEARS set " & _
                "STREITYPE = '" & EItype & "' " & _
                "where " & DBNameSpace & ".EITHRESHOLDYEARS.STRYEAR = '" & EItypeYear & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                MsgBox("The info has been updated", MsgBoxStyle.Information, "EI Tools")

            Else

                SQL = "Insert into " & DBNameSpace & ".EITHRESHOLDYEARS " & _
                 "(STRYEAR, STREITYPE ) " & _
                 "values " & _
                 "('" & EItypeYear & "', '" & EItype & "') "

                MsgBox("The info has been inserted", MsgBoxStyle.Information, "EI Tools")

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnAddNewEIType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewEIType.Click
        Dim EItype2 As String = txtNewEIType.Text
        Dim EItypeYear As String = lblEITypeYear.Text
        Dim temp As String

        Try
            If txtNewEIType.Text = "" Then
                MsgBox("Please enter new EI type!", MsgBoxStyle.Information, "EI Tools")
            Else
                SQL = "Select " & DBNameSpace & ".EITHRESHOLDS.STRTYPE " & _
                                " FROM " & DBNameSpace & ".EITHRESHOLDS " & _
                                "where   " & DBNameSpace & ".EITHRESHOLDS.STRTYPE = '" & Replace(EItype2, "'", "''") & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    MsgBox("EI type already exist. Please enter new type! ", MsgBoxStyle.Information, "EI Tools")
                Else
                    SQL = "Select distinct(" & DBNameSpace & ".EITHRESHOLDS.STRPOLLUTANT) as Pollutant " & _
                   " FROM " & DBNameSpace & ".EITHRESHOLDS "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("Pollutant")) Then
                            temp = ""
                        Else
                            temp = dr.Item("Pollutant")
                        End If

                        If temp <> "" Then
                            SQL = "Insert into AIRBranch.EIThresholds " & _
                                "Values " & _
                                "('" & Replace(temp, "'", "''") & "', " & _
                                "'', '', '" & Replace(EItype2, "'", "''") & "', " & _
                                "(Select max(PollutantID) + 1 from AIRBranch.EIThresholds) ) "
                            cmd2 = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr2 = cmd2.ExecuteReader
                            dr2.Close()
                        End If
                    End While
                    cboEIType2.Items.Clear()
                    cboEItype.Items.Clear()
                    loadEIType()
                    cboEIType2.Text = ""
                    cboEItype.Text = ""
                    dr.Close()

                    MsgBox("The info has been inserted", MsgBoxStyle.Information, "EI Tools")

                End If



                SQL = "Select * " & _
                    " FROM " & DBNameSpace & ".EITHRESHOLDS " & _
                    "where   " & DBNameSpace & ".EITHRESHOLDS.STRTYPE = '" & Replace(EItype2, "'", "''") & "' "

                dsViewCount = New DataSet
                daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmdBuild = New OracleCommandBuilder(daViewCount)

                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvEIThresholds.DataSource = dsViewCount
                dgvEIThresholds.DataMember = "ViewCount"

                dgvEIThresholds.RowHeadersVisible = True
                dgvEIThresholds.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvEIThresholds.AllowUserToResizeColumns = True
                dgvEIThresholds.AllowUserToAddRows = True
                dgvEIThresholds.AllowUserToDeleteRows = True
                dgvEIThresholds.AllowUserToOrderColumns = True
                dgvEIThresholds.AllowUserToResizeRows = True


                dgvEIThresholds.Columns("STRPOLLUTANT").HeaderText = "Pollutant Name"
                dgvEIThresholds.Columns("STRPOLLUTANT").DisplayIndex = 0
                dgvEIThresholds.Columns("NUMTHRESHOLD").HeaderText = "Thresholds (TPY)"
                dgvEIThresholds.Columns("NUMTHRESHOLD").DisplayIndex = 1
                dgvEIThresholds.Columns("NUMTHRESHOLDNAA").HeaderText = "Thresholds in NAA (TPY)"
                dgvEIThresholds.Columns("NUMTHRESHOLDNAA").DisplayIndex = 2
                dgvEIThresholds.Columns("STRTYPE").HeaderText = "EI Type"
                dgvEIThresholds.Columns("STRTYPE").DisplayIndex = 3


            End If



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSaveEIThresholds_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveEIThresholds.Click
        Try

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daViewCount.Update(dsViewCount, "ViewCount")
            CurrentConnection.Close()

            MessageBox.Show("The Update has Completed!", "EI Tool")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewEIthreshold2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewEIthreshold2.LinkClicked
        Try
            Dim EItype2 As String = cboEIType2.SelectedItem

            SQL = "Select * " & _
                " FROM " & DBNameSpace & ".EITHRESHOLDS " & _
                "where   " & DBNameSpace & ".EITHRESHOLDS.STRTYPE = '" & Replace(EItype2, "'", "''") & "' "


            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmdBuild = New OracleCommandBuilder(daViewCount)

            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEIThresholds.DataSource = dsViewCount
            dgvEIThresholds.DataMember = "ViewCount"

            dgvEIThresholds.RowHeadersVisible = False
            dgvEIThresholds.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEIThresholds.AllowUserToResizeColumns = True
            dgvEIThresholds.AllowUserToAddRows = False
            dgvEIThresholds.AllowUserToDeleteRows = False
            dgvEIThresholds.AllowUserToOrderColumns = True
            dgvEIThresholds.AllowUserToResizeRows = True

            dgvEIThresholds.Columns("STRPOLLUTANT").HeaderText = "Pollutant Name"
            dgvEIThresholds.Columns("STRPOLLUTANT").DisplayIndex = 0
            dgvEIThresholds.Columns("NUMTHRESHOLD").HeaderText = "Threshold (TPY)"
            dgvEIThresholds.Columns("NUMTHRESHOLD").DisplayIndex = 1
            dgvEIThresholds.Columns("NUMTHRESHOLDNAA").HeaderText = "Threshold in NAA (TPY)"
            dgvEIThresholds.Columns("NUMTHRESHOLDNAA").DisplayIndex = 2
            dgvEIThresholds.Columns("STRTYPE").HeaderText = "EI Type"
            dgvEIThresholds.Columns("STRTYPE").DisplayIndex = 3


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
  
    Private Sub btnaddEISfaciity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddEISfaciity.Click
        Try
            addonefacilityEIS()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub addonefacilityEIS()
        Dim EISYear As Integer = cboEISYear.SelectedItem
        Dim facilitySiteID As String = txtaddEISAirsno.Text
        Dim airsno As String = "0413" & facilitySiteID
        Dim facilityName As String
        Dim facilityLocation As String
        Dim facilityLocationCity As String
        Dim facilityLocastionZipcode As String
        Dim faciltyMailingAddress As String
        Dim facilityMailingCity As String
        Dim facilityMailingState As String
        Dim faciityMailingZipcode As String
        Dim facilityLongitude As String
        Dim facilityLatitude As String
        Dim HorizontalCollectionCode As String
        Dim HorizontalAccuracyMeasure As String
        Dim HorizontalReferenceCode As String
        Dim UpdateUser As String = UserGCode
        Dim FIPScode As String = Mid(airsno, 5, 3)
        Dim enrollment As String

        Try
            'Checks to see if AIRS No. is in IAIP
            SQL = "Select " & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME " & _
                           "FROM " & DBNameSpace & ".APBFACILITYINFORMATION " & _
                           "where  APBFACILITYINFORMATION.STRAIRSNUMBER = '" & airsno & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then

                SQL = "Select * " & _
                           "FROM " & DBNameSpace & ".APBFACILITYINFORMATION " & _
                           "where  APBFACILITYINFORMATION.STRAIRSNUMBER = '" & airsno & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Read()
                facilityName = dr("STRFACILITYNAME")

                facilityLocationCity = dr("STRFACILITYCITY")
                facilityLocastionZipcode = dr("STRFACILITYZIPCODE")
                faciltyMailingAddress = dr("STRFACILITYSTREET1")
                facilityMailingCity = dr("STRFACILITYCITY")
                facilityMailingState = dr("STRFACILITYSTATE")
                faciityMailingZipcode = dr("STRFACILITYZIPCODE")
                facilityLocation = dr("STRFACILITYSTREET1")
                facilityLongitude = dr("NUMFACILITYLONGITUDE")
                facilityLatitude = dr("NUMFACILITYLATITUDE")
                HorizontalCollectionCode = dr("STRHORIZONTALCOLLECTIONCODE")
                HorizontalAccuracyMeasure = dr("STRHORIZONTALACCURACYMEASURE")
                HorizontalReferenceCode = dr("STRHORIZONTALREFERENCECODE")
                dr.Close()


                'Check to see if AirsNo is in EIS Admin - previous FI submittal
                SQL = "Select " & DBNameSpace & ".EIS_Admin.FACILITYSITEID " & _
                      "FROM " & DBNameSpace & ".EIS_Admin " & _
                      "where  EIS_Admin.FACILITYSITEID = '" & facilitySiteID & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    'Check to see if AIRS No is enrolled for the year by checking strenrollment =1
                    SQL = "Select FACILITYSITEID, strenrollment " & _
                            "FROM " & DBNameSpace & ".EIS_Admin " & _
                            "where INVENTORYYEAR = '" & EISYear & "' " & _
                            " And FACILITYSITEID = '" & facilitySiteID & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read

                    If recExist = True Then
                        enrollment = dr("strenrollment")
                        Select Case enrollment
                            Case 1
                                MsgBox("This facility (" & facilitySiteID & ") is already enrolled for " & EISYear & ".", MsgBoxStyle.Information, "EIS Enrollment")
                            Case 0
                                'update enrollment =1 and then msgbox
                                SQL = "Update " & DBNameSpace & ".EIS_ADMIN set " & _
                                      "INVENTORYYEAR = '" & EISYear & "', " & _
                                      "FACILITYSITEID = '" & facilitySiteID & "', " & _
                                      "STRENROLLMENT = '1', " & _
                                      "EISSTATUSCODE = '1', " & _
                                      "STRMAILOUT = '1', " & _
                                      "UpdateUser = '" & UserGCode & "', " & _
                                      "DATEISSTATUS = '" & OracleDate & "', " & _
                                      "UpdateDateTime = '" & OracleDate & "' " & _
                                      "where FACILITYSITEID = '" & facilitySiteID & "' " & _
                                      " And INVENTORYYEAR = '" & EISYear & "' "

                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()
                                MsgBox("Facility (" & facilitySiteID & ") has been enrolled for " & EISYear & ".", MsgBoxStyle.Information, "EIS Enrollment")
                        End Select

                    Else
                        ' insert into EIS_admin table with EISaccesscode=1, EISStatus=1, Enrollment=1 and strmailout=1
                        SQL2 = "Insert into " & DBNameSpace & ".EIS_Admin " & _
                        "(EIS_Admin.INVENTORYYEAR, " & _
                        "EIS_Admin.FACILITYSITEID, " & _
                        "EIS_Admin.UPDATEDATETIME, " & _
                        "EIS_Admin.UPDATEUSER, " & _
                        "EIS_Admin.EISSTATUSCODE, " & _
                        "EIS_Admin.DATEISSTATUS, " & _
                        "EIS_Admin.STRMAILOUT, " & _
                        "EIS_Admin.strEnrollment, " & _
                        "EIS_Admin.ACTIVE, " & _
                        "EIS_Admin.CREATEDATETIME) " & _
                        "values " & _
                        "('" & Replace(EISYear, "'", "''") & "', " & _
                        "'" & Replace(facilitySiteID, "'", "''") & "', " & _
                        "'" & OracleDate & "', " & _
                        "'" & Replace(UpdateUser, "'", "''") & "', " & _
                        "'1', " & _
                        "'" & OracleDate & "', " & _
                        "'1', " & _
                        "'1', " & _
                        "'1', " & _
                        "'" & OracleDate & "' )"

                        cmd2 = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                        MsgBox("Facility (" & facilitySiteID & ") has been enrolled for " & EISYear & ".", MsgBoxStyle.Information, "EIS Enrollment")
                    End If

                Else
                    ' Inserts facility into the FI components - 5 tables
                    ' insert into EIS_FacilitySite table
                    SQL2 = "Insert into " & DBNameSpace & ".EIS_FACILITYSITE " & _
                   "(EIS_FACILITYSITE.FACILITYSITEID, " & _
                   "EIS_FACILITYSITE.STRFACILITYSITENAME, " & _
                   "EIS_FACILITYSITE.STRFACILITYSITESTATUSCODE, " & _
                   "EIS_FACILITYSITE.UPDATEDATETIME, " & _
                   "EIS_FACILITYSITE.UPDATEUSER, " & _
                   "EIS_FACILITYSITE.ACTIVE, " & _
                   "EIS_FACILITYSITE.CREATEDATETIME) " & _
                   "values " & _
                   "('" & Replace(facilitySiteID, "'", "''") & "', " & _
                   "'" & Replace(facilityName, "'", "''") & "', " & _
                   "'OP', " & _
                   "'" & OracleDate & "', " & _
                   "'" & Replace(UpdateUser, "'", "''") & "', " & _
                   "'1', " & _
                   "'" & OracleDate & "' )"
                    cmd2 = New OracleCommand(SQL2, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    ' insert into EIS_admin table
                    'EISACCESSCODE needs to be changed to 1 once the EI components are in place - BCG
                    SQL2 = "Insert into " & DBNameSpace & ".EIS_Admin " & _
                    "(EIS_Admin.INVENTORYYEAR, " & _
                    "EIS_Admin.FACILITYSITEID, " & _
                    "EIS_Admin.EISSTATUSCODE, " & _
                    "EIS_Admin.EISACCESSCODE, " & _
                    "EIS_Admin.STRENROLLMENT, " & _
                    "EIS_Admin.UPDATEDATETIME, " & _
                    "EIS_Admin.UPDATEUSER, " & _
                    "EIS_Admin.ACTIVE, " & _
                    "EIS_Admin.CREATEDATETIME) " & _
                    "values " & _
                    "('" & Replace(EISYear, "'", "''") & "', " & _
                    "'" & Replace(facilitySiteID, "'", "''") & "', " & _
                    "'1', " & _
                    "'1', " & _
                    "'1', " & _
                    "'" & OracleDate & "', " & _
                    "'" & Replace(UpdateUser, "'", "''") & "', " & _
                    "'1', " & _
                    "'" & OracleDate & "' )"

                    cmd2 = New OracleCommand(SQL2, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    'insert into EIS_FacilitySiteAddress table
                    SQL2 = "Insert into " & DBNameSpace & ".EIS_FACILITYSITEADDRESS " & _
                  "(EIS_FACILITYSITEADDRESS.FACILITYSITEID, " & _
                  "EIS_FACILITYSITEADDRESS.STRMAILINGADDRESSTEXT, " & _
                  "EIS_FACILITYSITEADDRESS.STRMAILINGADDRESSCITYNAME, " & _
                  "EIS_FACILITYSITEADDRESS.STRMAILINGADDRESSSTATECODE, " & _
                  "EIS_FACILITYSITEADDRESS.STRMAILINGADDRESSPOSTALCODE, " & _
                  "EIS_FACILITYSITEADDRESS.STRLOCATIONADDRESSTEXT, " & _
                  "EIS_FACILITYSITEADDRESS.STRLOCALITYNAME, " & _
                  "EIS_FACILITYSITEADDRESS.STRLOCATIONADDRESSPOSTALCODE, " & _
                  "EIS_FACILITYSITEADDRESS.UPDATEDATETIME, " & _
                  "EIS_FACILITYSITEADDRESS.UPDATEUSER, " & _
                  "EIS_FACILITYSITEADDRESS.ACTIVE, " & _
                  "EIS_FACILITYSITEADDRESS.CREATEDATETIME) " & _
                  "values " & _
                  "('" & Replace(facilitySiteID, "'", "''") & "', " & _
                  " '" & Replace(faciltyMailingAddress, " '", "''") & "', " & _
                  " '" & Replace(facilityMailingCity, "'", "''") & "', " & _
                  " '" & Replace(facilityMailingState, " '", "''") & "', " & _
                  " '" & Replace(faciityMailingZipcode, "'", "''") & "', " & _
                  " '" & Replace(facilityLocationCity, " '", "''") & "', " & _
                  "'GA', " & _
                  "'" & Replace(facilityLocastionZipcode, "'", "''") & "', " & _
                  "'" & OracleDate & "', " & _
                  "'" & Replace(UpdateUser, "'", "''") & "', " & _
                  "'1', " & _
                  "'" & OracleDate & "' )"

                 
                    cmd2 = New OracleCommand(SQL2, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    ' insert into EIS_FacilityIdentification table
                    SQL2 = "Insert into " & DBNameSpace & ".EIS_FACILITYIDENTIFICATION " & _
                  "(EIS_FACILITYIDENTIFICATION.FACILITYSITEID, " & _
                  "EIS_FACILITYIDENTIFICATION.STRSTATEANDCOUNTYFIPSCODE, " & _
                   "EIS_FACILITYIDENTIFICATION.UPDATEDATETIME, " & _
                  "EIS_FACILITYIDENTIFICATION.UPDATEUSER, " & _
                  "EIS_FACILITYIDENTIFICATION.ACTIVE, " & _
                  "EIS_FACILITYIDENTIFICATION.CREATEDATETIME) " & _
                  "values " & _
                  "('" & Replace(facilitySiteID, "'", "''") & "', " & _
                  "'" & Replace(FIPScode, "'", "''") & "', " & _
                  "'" & OracleDate & "', " & _
                  "'" & Replace(UpdateUser, "'", "''") & "', " & _
                  "'1', " & _
                  "'" & OracleDate & "' )"

                    cmd2 = New OracleCommand(SQL2, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    ' insert into EIS_FACILITYGEOCOORD table
                    SQL2 = "Insert into " & DBNameSpace & ".EIS_FACILITYGEOCOORD " & _
                  "(EIS_FACILITYGEOCOORD.FACILITYSITEID, " & _
                  "EIS_FACILITYGEOCOORD.NUMLATITUDEMEASURE, " & _
                  "EIS_FACILITYGEOCOORD.NUMLONGITUDEMEASURE, " & _
                  "EIS_FACILITYGEOCOORD.INTHORACCURACYMEASURE, " & _
                  "EIS_FACILITYGEOCOORD.STRHORCOLLMETCODE, " & _
                  "EIS_FACILITYGEOCOORD.STRHORREFDATUMCODE, " & _
                  "EIS_FACILITYGEOCOORD.UPDATEDATETIME, " & _
                  "EIS_FACILITYGEOCOORD.UPDATEUSER, " & _
                  "EIS_FACILITYGEOCOORD.ACTIVE, " & _
                  "EIS_FACILITYGEOCOORD.CREATEDATETIME) " & _
                  "values " & _
                  "('" & Replace(facilitySiteID, "'", "''") & "', " & _
                   "'" & facilityLatitude & "', " & _
                  "'" & facilityLongitude & "', " & _
                  "'" & HorizontalAccuracyMeasure & "', " & _
                  "'" & HorizontalCollectionCode & "', " & _
                  "'" & HorizontalReferenceCode & "', " & _
                  "'" & OracleDate & "', " & _
                  "'" & Replace(UpdateUser, "'", "''") & "', " & _
                  "'1', " & _
                  "'" & OracleDate & "' )"

                    cmd2 = New OracleCommand(SQL2, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    ' insert into EIS_FACILITYSITEAFFINDIV table
                    SQL2 = "Insert into " & DBNameSpace & ".EIS_FACILITYSITEAFFINDIV " & _
                  "(EIS_FACILITYSITEAFFINDIV.FACILITYSITEID, " & _
                  "EIS_FACILITYSITEAFFINDIV.UPDATEDATETIME, " & _
                  "EIS_FACILITYSITEAFFINDIV.UPDATEUSER, " & _
                  "EIS_FACILITYSITEAFFINDIV.ACTIVE, " & _
                  "EIS_FACILITYSITEAFFINDIV.CREATEDATETIME) " & _
                  "values " & _
                  "('" & Replace(facilitySiteID, "'", "''") & "', " & _
                  "'" & OracleDate & "', " & _
                  "'" & Replace(UpdateUser, "'", "''") & "', " & _
                  "'1', " & _
                  "'" & OracleDate & "' )"

                    cmd2 = New OracleCommand(SQL2, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()

                    MsgBox("This facility (" & facilitySiteID & ") has been added and enrolled for " & EISYear & ".", MsgBoxStyle.Information, "EI Enrollment")
                End If
            Else
                MsgBox("This Airs Number (" & facilitySiteID & ") is not valid. Please enter valid Airs Number.", MsgBoxStyle.Information, "EI Enrollment")
            End If

        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub btnRemoveEISfaciity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveEISfaciity.Click
        Dim EISYear As Integer = cboEISYear.SelectedItem
        Dim FacilitySiteID As String = txtaddEISAirsno.Text
        Dim active As String = "1"
        Dim enrollment As String = "0"
        Dim EISAccessCode As String = "0"
        Dim EISSTATUSCODE As String = "0"

        Dim sql As String
        Try

            sql = "Update " & DBNameSpace & ".EIS_ADMIN set " & _
             "INVENTORYYEAR = '" & EISYear & "', " & _
             "FACILITYSITEID = '" & FacilitySiteID & "', " & _
             "EISACCESSCODE = '" & EISAccessCode & "', " & _
             "EISSTATUSCODE = '" & EISSTATUSCODE & "', " & _
             "STRENROLLMENT = '" & enrollment & "', " & _
             "Active = '" & active & "', " & _
             "UpdateUser = '" & UserGCode & "', " & _
             "DATEISSTATUS = '" & OracleDate & "', " & _
             "UpdateDateTime = '" & OracleDate & "' " & _
             "where FACILITYSITEID = '" & FacilitySiteID & "' " & _
             " And INVENTORYYEAR = '" & EISYear & "' "

            cmd = New OracleCommand(sql, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            MsgBox("This facility (" & FacilitySiteID & ") have been removed for " & EISYear & ".", MsgBoxStyle.Information, "EI Enrollment")

        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub btnCheckEISfaciity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckEISfaciity.Click

        Dim EISYear As Integer = cboEISYear.SelectedItem
        Dim FacilitySiteID As String = txtaddEISAirsno.Text
        Dim statuscode As String
        Dim enrollment As String = "1"

        Dim SQL As String

        Try
            SQL = "Select FACILITYSITEID, EISACCESSCODE " & _
                  "FROM " & DBNameSpace & ".EIS_Admin " & _
                  "where INVENTORYYEAR = '" & EISYear & "' " & _
                  " And FACILITYSITEID = '" & FacilitySiteID & "' " & _
                  " And STRENROLLMENT = '" & enrollment & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                statuscode = dr("EISACCESSCODE")
                lblEISStatusCode.Text = statuscode
                MsgBox("This facility (" & FacilitySiteID & ") is already enrolled for " & EISYear & ".", MsgBoxStyle.Information, "EIS Enrollment")
            Else
                MsgBox("This facility (" & FacilitySiteID & ") is not enrolled for " & EISYear & ".", MsgBoxStyle.Information, "EIS Enrollment")

            End If

        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub btnCheckFI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckFI.Click
        Dim FacilitySiteID As String = txtaddEISAirsno.Text
        Dim SQL As String

        Try
            SQL = "Select FACILITYSITEID " & _
                                   "FROM " & DBNameSpace & ".EIS_Admin " & _
                                   "where FACILITYSITEID = '" & FacilitySiteID & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                MsgBox("This facility (" & FacilitySiteID & ") is already enrolled for FI")
            Else
                MsgBox("This facility (" & FacilitySiteID & ") is not enrolled for FI")

            End If

        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub loadEISType()

        Dim SQL As String
        Dim EIStype As String
        Try
            SQL = "Select distinct EITHRESHOLDS.STRTYPE " & _
            "from " & DBNameSpace & ".EITHRESHOLDS " & _
            "order by STRTYPE desc "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                EIStype = dr("STRTYPE")
                cboEIStype.Items.Add(EIStype)
                cboEISType2.Items.Add(EIStype)
            End While

            cboEIStype.SelectedIndex = 0
            cboEISType2.SelectedIndex = 0

        Catch ex As Exception
            '  ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub loadEISEnrollmentYear()
        'Load MailOut Year dropdown boxes
        Dim year As Integer
        Dim SQL As String
        Try
            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            SQL = "Select distinct INVENTORYYEAR " & _
                      "from " & DBNameSpace & ".EIS_Admin  " & _
                      "order by INVENTORYYEAR desc"
            Dim cmd As New OracleCommand(SQL, CurrentConnection)

            Dim dr As OracleDataReader = cmd.ExecuteReader()
            dr.Read()
            Do
                year = dr("INVENTORYYEAR")
                cboEISMailoutEnrollmentYear.Items.Add(year)
            Loop While dr.Read
            cboEISMailoutEnrollmentYear.SelectedIndex = 0
        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub loadEISThresholdYear()

        Dim SQL As String
        Dim year As String
        Try
            SQL = "Select distinct strYear " & _
            "from " & DBNameSpace & ".EITHRESHOLDYEARS " & _
            "order by strYear desc "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                year = dr("strYear")
                cboEISThreholdYear.Items.Add(year)
                cboEISYear.Items.Add(year)
            End While

            cboEISThreholdYear.SelectedIndex = 0
            cboEISYear.SelectedIndex = 0
        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LoadcboEISYESNO()
        cboEISEnrollmentStatus.Items.Add(1)
        cboEISEnrollmentStatus.Items.Add(0)
        cboEISOptoutStatus.Items.Add("Null")
        cboEISOptoutStatus.Items.Add(1)
        cboEISOptoutStatus.Items.Add(0)
        cboEISMailoutStatus.Items.Add(1)
        cboEISMailoutStatus.Items.Add(0)
    End Sub

    Private Sub loadcboEISstatusCodes()
        Dim dtCode As New DataTable
        Dim dscode As DataSet
        Dim dacode As OracleDataAdapter
        Dim daEIcode As OracleDataAdapter
        Dim dtEICode As New DataTable()

        Dim drDSRow As DataRow
        Dim DrNewRow As DataRow
        Dim Drnewrow2 As DataRow

        Dim SQL As String

        Try
            SQL = "Select distinct  EISSTATUSCODE, STRDESC " & _
            "from " & DBNameSpace & ".EISLK_EISSTATUSCODE "

            dscode = New DataSet
            dacode = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dacode.Fill(dscode, "EISLK_EISSTATUSCODE")

            dtCode.Columns.Add("EISSTATUSCODE", GetType(System.String))
            dtCode.Columns.Add("STRDESC", GetType(System.String))
            DrNewRow = dtCode.NewRow()
            DrNewRow("EISSTATUSCODE") = ""
            DrNewRow("STRDESC") = "- Select EIS Status Code -"
            dtCode.Rows.Add(DrNewRow)

            For Each drDSRow In dscode.Tables("EISLK_EISSTATUSCODE").Rows()
                DrNewRow = dtCode.NewRow()
                DrNewRow("EISSTATUSCODE") = drDSRow("EISSTATUSCODE")
                DrNewRow("STRDESC") = drDSRow("STRDESC")
                dtCode.Rows.Add(DrNewRow)
            Next
            Dim temp As String
            temp = dtCode.Rows.Count

            With cboEILogStatusCode
                .DataSource = dtCode
                .DisplayMember = "STRDESC"
                .ValueMember = "EISSTATUSCODE"
                .SelectedIndex = 0
            End With

            SQL = "select strDesc, EISAccessCode " & _
            " from AIRBranch.EISLK_EISAccesscode  " & _
            "order by strDesc"

            dscode = New DataSet
            daEIcode = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daEIcode.Fill(dscode, "EISAccessCode")

            dtEICode.Columns.Add("EISAccessCode", GetType(System.String))
            dtEICode.Columns.Add("STRDESC", GetType(System.String))
            Drnewrow2 = dtEICode.NewRow()
            Drnewrow2("EISAccessCode") = ""
            Drnewrow2("STRDESC") = "- Select EIS Access Code -"
            dtEICode.Rows.Add(Drnewrow2)

            For Each drDSRow In dscode.Tables("EISAccessCode").Rows()
                Drnewrow2 = dtEICode.NewRow()
                Drnewrow2("EISAccessCode") = drDSRow("EISAccessCode")
                Drnewrow2("STRDESC") = drDSRow("STRDESC")
                dtEICode.Rows.Add(Drnewrow2)
            Next

            With cboEILogAccessCode
                .DataSource = dtEICode
                .DisplayMember = "STRDESC"
                .ValueMember = "EISAccessCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            '  ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    'Private Sub loadcboEISAccessCodes()

    '    Dim SQL As String
    '    Dim EISAccessCode As String
    '    cboEISAccessCode.Items.Add("- Select EIS Access Code -")

    '    Try
    '        SQL = "Select distinct EISLK_EISACCESSCODE.EISACCESSCODE,EISLK_EISACCESSCODE.STRDESC " & _
    '        "from " & DBNameSpace & ".EISLK_EISACCESSCODE " & _
    '        "order by EISACCESSCODE desc "

    '        cmd = New OracleCommand(SQL, conn)
    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If

    '        dr = cmd.ExecuteReader
    '        While dr.Read
    '            EISAccessCode = dr("EISACCESSCODE")
    '            cboEISAccessCode.Items.Add(EISAccessCode)
    '        End While

    '        cboEISAccessCode.SelectedIndex = 0

    '    Catch ex As Exception
    '        '  ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally

    '    End Try
    'End Sub

    'Private Sub loadcboEISAccessCodes()
    '    Dim dtCode As New DataTable
    '    Dim dscode As DataSet
    '    Dim dacode As OracleDataAdapter


    '    Dim drDSRow As DataRow
    '    Dim DrNewRow As DataRow
    '    Dim SQL As String

    '    cboEISAccessCode.Items.Add("- Select EIS Access Code -")

    '    Try
    '        SQL = "Select distinct EISLK_EISACCESSCODE.EISACCESSCODE,EISLK_EISACCESSCODE.STRDESC " & _
    '        "from " & DBNameSpace & ".EISLK_EISACCESSCODE "
    '        dscode = New DataSet
    '        dacode = New OracleDataAdapter(SQL, conn)
    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If

    '        dacode.Fill(dscode, "EISAccessCodes")

    '        dtCode.Columns.Add("EISACCESSCODE", GetType(System.String))
    '        dtCode.Columns.Add("STRDESC", GetType(System.String))
    '        DrNewRow = dtCode.NewRow()
    '        DrNewRow("EISACCESSCODE") = ""
    '        DrNewRow("STRDESC") = ""
    '        dtCode.Rows.Add(DrNewRow)
    '        For Each drDSRow In dscode.Tables("EISAccessCodes").Rows()
    '            DrNewRow = dtCode.NewRow()
    '            DrNewROw("EISACCESSCODE") = drDSRow("EISACCESSCODE")
    '            DrNewROw("strdesc") = drDSRow("strdesc")

    '        Next
    '        Dim temp As String
    '        temp = dtCode.Rows.Count

    '        With cboEISAccessCode
    '            .DataSource = dtCode
    '            .DisplayMember = "STRDESC"
    '            .ValueMember = "EISACCESSCODE"
    '            .SelectedIndex = 0
    '        End With

    '    Catch ex As Exception
    '        '  ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally

    '    End Try
    'End Sub

    Private Sub loadcboEISAccessCodes()
        Dim dtCode As New DataTable
        Dim dscode As DataSet
        Dim dacode As OracleDataAdapter


        Dim drDSRow As DataRow
        Dim DrNewRow As DataRow
        Dim SQL As String

        Try
            SQL = "Select distinct EISLK_EISACCESSCODE.EISACCESSCODE,EISLK_EISACCESSCODE.STRDESC " & _
            "from " & DBNameSpace & ".EISLK_EISACCESSCODE "
            dscode = New DataSet
            dacode = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dacode.Fill(dscode, "EISAccessCodes")

            dtCode.Columns.Add("EISACCESSCODE", GetType(System.String))
            dtCode.Columns.Add("STRDESC", GetType(System.String))
            DrNewRow = dtCode.NewRow()
            DrNewRow("EISACCESSCODE") = ""
            DrNewRow("STRDESC") = "- Select EIS Access Code -"
            dtCode.Rows.Add(DrNewRow)

            For Each drDSRow In dscode.Tables("EISAccessCodes").Rows()
                DrNewRow = dtCode.NewRow()
                DrNewRow("EISACCESSCODE") = drDSRow("EISACCESSCODE")
                DrNewRow("strdesc") = drDSRow("strdesc")
                dtCode.Rows.Add(DrNewRow)
            Next
            Dim temp As String
            temp = dtCode.Rows.Count

            With cboEISAccessCode
                .DataSource = dtCode
                .DisplayMember = "STRDESC"
                .ValueMember = "EISACCESSCODE"
                .SelectedIndex = 0
            End With

            With cboEILogAccessCode
                .DataSource = dtCode
                .DisplayMember = "STRDESC"
                .ValueMember = "EISACCESSCODE"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            '  ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub


    Private Sub btnEISBulkEnrollment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISBulkEnrollment.Click
        Try
            If cboEISMailoutEnrollmentYear.Text = "" Then
                MsgBox("Please choose a Year!", MsgBoxStyle.Information, "EIS Enrollment")
            Else
                EISMailoutenrollment()
                cboEISMailoutEnrollmentYear.Items.Clear()
                loadEISEnrollmentYear()
                cboEISMailoutEnrollmentYear.Text = ""
            End If

        Catch ex As Exception
            ' ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Sub EISMailoutenrollment()
        Dim facilitySiteID As String
        Dim EISYear As Integer = CInt(cboEISMailoutEnrollmentYear.SelectedItem)
        Dim UpdateUser As String = UserGCode
        Dim active As String
        Dim enrollment As String
        Dim EISAccessCode As String
        Dim EISSTATUSCODE As String


        Try
            SQL = "Select * " & _
            "FROM " & DBNameSpace & ".EIS_ADMIN " & _
            "where INVENTORYYEAR = '" & EISYear & "'" & _
            " and active ='1' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                SQL = "Select * " & _
            "FROM " & DBNameSpace & ".EIS_ADMIN " & _
            "where INVENTORYYEAR = '" & EISYear & "'" & _
            " and strenrollment = '1'" & _
            " and active ='1'"


                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist2 = dr.Read

                If recExist2 = True Then
                    MsgBox("That year " & EISYear & " is already enrolled.", MsgBoxStyle.Information, "EIS Enrollment")
                Else
                    SQL = "Select EIS_MAILOUT.FACILITYSITEID, EIS_MAILOUT.STRFACILITYNAME " & _
               "FROM " & DBNameSpace & ".EIS_MAILOUT " & _
               "where EIS_MAILOUT.INTINVENTORYYEAR = '" & EISYear & "'" & _
               " and active '1'"

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Read()
                    'Update strenrollment =1, EISstatuscode=1, and active =1 to EIS_admin table
                    SQL = "SELECT FACILITYSITEID " & _
                                          "from " & DBNameSpace & ".EIS_ADMIN " & _
                                          "where EIS_ADMIN.INVENTORYYEAR = '" & EISYear & "'" & _
                                          " order by FACILITYSITEID " & _
                                          " and active = '1' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    enrollment = "1"
                    EISAccessCode = "1"
                    EISSTATUSCODE = "1"
                    active = "1"
                    While dr.Read
                        If IsDBNull(dr("FACILITYSITEID")) Then
                            facilitySiteID = " "
                        Else
                            facilitySiteID = dr("FACILITYSITEID")
                        End If

                        SQL2 = "Update " & DBNameSpace & ".EIS_ADMIN set " & _
            "EISACCESSCODE = '" & EISAccessCode & "', " & _
            "EISSTATUSCODE = '" & EISSTATUSCODE & "', " & _
            "STRENROLLMENT = '" & enrollment & "', " & _
            "Active = '" & active & "', " & _
            "UpdateUser = '" & UserGCode & "', " & _
            "DATEISSTATUS = '" & OracleDate & "', " & _
            "UpdateDateTime = '" & OracleDate & "' " & _
            "where FACILITYSITEID = '" & facilitySiteID & "' " & _
            " And INVENTORYYEAR = '" & EISYear & "' "
                        cmd2 = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                    End While

                    MsgBox("Enrollment has been removed!", MsgBoxStyle.Information, "EIS Enrollment")
                End If

                MsgBox("The facilities for year " & EISYear & " have been enrolled", MsgBoxStyle.Information, "EIS Enrollment")
            End If
        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub


    Private Sub btnUnenrollEISEntireYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnenrollEISEntireYear.Click
        Dim EISYear As Integer = CInt(cboEISMailoutEnrollmentYear.SelectedItem)
        Dim sql As String
        'Dim FacilitySiteID As String = txtaddEISAirsno.Text
        Dim active As String = "1"
        Dim enrollment As String = "0"
        Dim EISAccessCode As String = "0"
        Dim EISSTATUSCODE As String = "0"
        Dim FacilitySiteID As String
        Try
            If cboEISMailoutEnrollmentYear.Text = "" Then
                MsgBox("Please choose a year!", MsgBoxStyle.Information, "EIS Enrollment")
            Else
                Dim intAnswer As DialogResult
                intAnswer = MessageBox.Show("Remove the enrollment?", "EIS Enrollment", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                Select Case intAnswer
                    Case Windows.Forms.DialogResult.OK
                        sql = "SELECT FACILITYSITEID " & _
                       "from " & DBNameSpace & ".EIS_ADMIN " & _
                       "where EIS_ADMIN.INVENTORYYEAR = '" & EISYear & "'" & _
                       " order by FACILITYSITEID"
                        cmd = New OracleCommand(sql, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr("FACILITYSITEID")) Then
                                FacilitySiteID = " "
                            Else
                                FacilitySiteID = dr("FACILITYSITEID")
                            End If
                            ' add active 
                            SQL2 = "Update " & DBNameSpace & ".EIS_ADMIN set " & _
                "EISACCESSCODE = '" & EISAccessCode & "', " & _
                "EISSTATUSCODE = '" & EISSTATUSCODE & "', " & _
                "STRENROLLMENT = '" & enrollment & "', " & _
                "UpdateUser = '" & UserGCode & "', " & _
                "DATEISSTATUS = '" & OracleDate & "', " & _
                "UpdateDateTime = '" & OracleDate & "' " & _
                "where FACILITYSITEID = '" & FacilitySiteID & "' " & _
                " And INVENTORYYEAR = '" & EISYear & "' " & _
                " And Active = '" & active & "' "
                            cmd2 = New OracleCommand(SQL2, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr2 = cmd2.ExecuteReader
                            dr2.Close()
                        End While

                        MsgBox("Enrollment has been removed!", MsgBoxStyle.Information, "EIS Enrollment")
                    Case Else
                        MsgBox("Enrollment has not been removed!", MsgBoxStyle.Information, "EIS Enrollment")
                End Select

                cboEISMailoutEnrollmentYear.Items.Clear()
                loadEISEnrollmentYear()
                cboEISMailoutEnrollmentYear.Text = ""
                txtEISRecordNumber.Text = ""
            End If

        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub btnGenerateEISMailoutList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateEISMailoutList.Click
        Try
            GenerateEISMailOutList()
            cboEISMailoutYear.Items.Clear()
            loadEIMailOutYear()
            cboEISMailoutYear.Text = ""



        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub GenerateEISMailOutList()
        Dim FacilitySiteID As String
        Dim airsNumber As String '= "0413" & FacilitySiteID
        Dim airsYear As String
        Dim FACILITYNAME As String = " "
        Dim CONTACTCOMPANYNAME As String = " "
        Dim CONTACTADDRESS1 As String = " "
        Dim CONTACTADDRESS2 As String = " "
        Dim CONTACTCITY As String = " "
        Dim CONTACTSTATE As String = " "
        Dim CONTACTZIPCODE As String = " "
        Dim CONTACTFIRSTNAME As String = " "
        Dim CONTACTLASTNAME As String = " "
        Dim CONTACTEMAIL As String = " "
        Dim EISYear As String = cboEISMailoutEnrollmentYear.SelectedItem

        Dim OperationalStatus As String = " "
        Dim FacilityClass As String = " "
        Dim UpdateUser As String = UserGCode

        Try
            If EISYear = " " Then
                MsgBox("You must select a Mailout Year")
            Else
                SQL = "Select FacilitySiteID " & _
                "FROM " & DBNameSpace & ".EIS_MAILOUT " & _
                "where INTINVENTORYYEAR = '" & EISYear & "'"
            End If
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            'dr.Close()

            If recExist = True Then
                FacilitySiteID = dr("FacilitySiteID")
                MsgBox("That year is already being used." & vbCrLf & "If you want to use that year," & vbCrLf & "you must first delete that year from the database.")
            Else
                If cboEISMailoutEnrollmentYear.Text <> "" Then
                    If cboEISMailoutEnrollmentYear.Text.Length = 4 Then
                        SQL = "SELECT STRAIRSNUMBER, " & _
                   "STRFACILITYNAME, " & _
                   "STROPERATIONALSTATUS, " & _
                   "STRCLASS, " & _
                   "STRCONTACTFIRSTNAME, " & _
                   "STRCONTACTLASTNAME, " & _
                   "STRCONTACTCOMPANYNAME, " & _
                   "STRCONTACTADDRESS1, " & _
                   "STRCONTACTCITY, " & _
                   "STRCONTACTSTATE, " & _
                   "STRCONTACTZIPCODE, " & _
                   "STRCONTACTEMAIL " & _
                   "from " & DBNameSpace & ".VIEW_EIS_Currentmailout " & _
                   "order by STRFACILITYNAME"


                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            airsNumber = dr("strAirsNumber")
                            FacilitySiteID = Mid(airsNumber, 5, 8)
                            airsYear = airsNumber & EISYear
                            EISYear = cboEISMailoutEnrollmentYear.SelectedItem
                            If IsDBNull(dr("STRFACILITYNAME")) Then
                                FACILITYNAME = " "
                            Else
                                FACILITYNAME = dr("STRFACILITYNAME")
                            End If
                            If IsDBNull(dr("STROPERATIONALSTATUS")) Then
                                OperationalStatus = " "
                            Else
                                OperationalStatus = dr("STROPERATIONALSTATUS")
                            End If
                            If IsDBNull(dr("STRCLASS")) Then
                                FacilityClass = " "
                            Else
                                FacilityClass = dr("STRCLASS")
                            End If
                            If IsDBNull(dr("STRCONTACTCOMPANYNAME")) Then
                                CONTACTCOMPANYNAME = " "
                            Else
                                CONTACTCOMPANYNAME = dr("STRCONTACTCOMPANYNAME")
                            End If
                            If IsDBNull(dr("STRCONTACTADDRESS1")) Then
                                CONTACTADDRESS1 = " "
                            Else
                                CONTACTADDRESS1 = dr("STRCONTACTADDRESS1")
                            End If

                            If IsDBNull(dr("STRCONTACTCITY")) Then
                                CONTACTCITY = " "
                            Else
                                CONTACTCITY = dr("STRCONTACTCITY")
                            End If
                            If IsDBNull(dr("STRCONTACTSTATE")) Then
                                CONTACTSTATE = " "
                            Else
                                CONTACTSTATE = dr("STRCONTACTSTATE")
                            End If
                            If IsDBNull(dr("STRCONTACTZIPCODE")) Then
                                CONTACTZIPCODE = " "
                            Else
                                CONTACTZIPCODE = dr("STRCONTACTZIPCODE")
                            End If
                            If IsDBNull(dr("STRCONTACTFIRSTNAME")) Then
                                CONTACTFIRSTNAME = " "
                            Else
                                CONTACTFIRSTNAME = dr("STRCONTACTFIRSTNAME")
                            End If
                            If IsDBNull(dr("STRCONTACTLASTNAME")) Then
                                CONTACTLASTNAME = " "
                            Else
                                CONTACTLASTNAME = dr("STRCONTACTLASTNAME")
                            End If
                            If IsDBNull(dr("STRCONTACTEMAIL")) Then
                                CONTACTEMAIL = " "
                            Else
                                CONTACTEMAIL = dr("STRCONTACTEMAIL")
                            End If
                            'strmailout=1 in the EIS_admin
                            SQL2 = "insert into " & DBNameSpace & ".EIS_mailOut " & _
                           "(INTINVENTORYYEAR, " & _
                           "FACILITYSITEID, " & _
                           "STRFACILITYNAME, " & _
                           "STROPERATIONALSTATUS, " & _
                           "STRCLASS, " & _
                           "STRCONTACTCOMPANYNAME, " & _
                           "STRCONTACTADDRESS1, " & _
                           "STRCONTACTCITY, " & _
                           "STRCONTACTSTATE, " & _
                           "STRCONTACTZIPCODE, " & _
                           "STRCONTACTFIRSTNAME, " & _
                           "STRCONTACTLASTNAME, " & _
                           "STRCONTACTEMAIL, " & _
                           "EIS_mailOut.UPDATEDATETIME, " & _
                           "EIS_mailOut.UPDATEUSER, " & _
                           "EIS_mailOut.CREATEDATETIME ) " & _
                           "values " & _
                           "('" & Replace(EISYear, "'", "''") & "', " & _
                           "'" & Replace(FacilitySiteID, "'", "''") & "', " & _
                           "'" & Replace(FACILITYNAME, "'", "''") & "', " & _
                           "'" & Replace(OperationalStatus, "'", "''") & "', " & _
                           "'" & Replace(FacilityClass, "'", "''") & "', " & _
                           "'" & Replace(Replace(CONTACTCOMPANYNAME, "'", "''"), "N/A", " ") & "', " & _
                           "'" & Replace(Replace(CONTACTADDRESS1, "'", "''"), "N/A", " ") & "', " & _
                           "'" & Replace(Replace(CONTACTCITY, "'", "''"), "N/A", " ") & "', " & _
                           "'" & CONTACTSTATE & "', " & _
                           "'" & Replace(CONTACTZIPCODE, "N/A", " ") & "', " & _
                           "'" & Replace(Replace(CONTACTFIRSTNAME, "'", "''"), "N/A", " ") & "', " & _
                           "'" & Replace(Replace(CONTACTLASTNAME, "'", "''"), "N/A", " ") & "', " & _
                           "'" & Replace(Replace(CONTACTEMAIL, "'", "''"), "N/A", " ") & "', " & _
                           "'" & OracleDate & "', " & _
                           "'" & Replace(UpdateUser, "'", "''") & "', " & _
                           "'" & OracleDate & "' )"

                            cmd2 = New OracleCommand(SQL2, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr2 = cmd2.ExecuteReader
                            dr2.Close()
                        End While
                    End If


                    Dim year As String = cboEISMailoutEnrollmentYear.SelectedItem

                    SQL = "SELECT FACILITYSITEID, " & _
                    "STRFACILITYNAME, " & _
                    "STROPERATIONALSTATUS, " & _
                    "STRCLASS, " & _
                    "STRCONTACTFIRSTNAME, " & _
                    "STRCONTACTLASTNAME, " & _
                    "STRCONTACTCOMPANYNAME, " & _
                    "STRCONTACTADDRESS1, " & _
                    "STRCONTACTCITY, " & _
                    "STRCONTACTSTATE, " & _
                    "STRCONTACTZIPCODE, " & _
                    "STRCONTACTEMAIL " & _
                    "from " & DBNameSpace & ".EIS_MailOut " & _
                    "where INTINVENTORYYEAR = '" & year & "' " & _
                    "order by STRFACILITYNAME"

                    dsViewCount = New DataSet
                    daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    daViewCount.Fill(dsViewCount, "ViewCount")
                    dgvEISDataCount.DataSource = dsViewCount
                    dgvEISDataCount.DataMember = "ViewCount"
                    dgvEISDataCount.RowHeadersVisible = False
                    dgvEISDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvEISDataCount.AllowUserToResizeColumns = True
                    dgvEISDataCount.AllowUserToAddRows = False
                    dgvEISDataCount.AllowUserToDeleteRows = False
                    dgvEISDataCount.AllowUserToOrderColumns = True
                    dgvEISDataCount.AllowUserToResizeRows = True

                    dgvEISDataCount.Columns("FACILITYSITEID").HeaderText = "Facilitysite ID"
                    dgvEISDataCount.Columns("FACILITYSITEID").DisplayIndex = 0
                    dgvEISDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
                    dgvEISDataCount.Columns("strFacilityName").DisplayIndex = 1
                    dgvEISDataCount.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status"
                    dgvEISDataCount.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
                    dgvEISDataCount.Columns("STRCLASS").HeaderText = "Facility Class"
                    dgvEISDataCount.Columns("STRCLASS").DisplayIndex = 3
                    dgvEISDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
                    dgvEISDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
                    dgvEISDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
                    dgvEISDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
                    dgvEISDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
                    dgvEISDataCount.Columns("STRCONTACTCOMPANYname").DisplayIndex = 6
                    dgvEISDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
                    dgvEISDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 7
                    dgvEISDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
                    dgvEISDataCount.Columns("STRCONTACTCITY").DisplayIndex = 8
                    dgvEISDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
                    dgvEISDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 9
                    dgvEISDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
                    dgvEISDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
                    dgvEISDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
                    dgvEISDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 11

                    txtEISRecordNumber.Text = dgvEISDataCount.RowCount.ToString

                End If
            End If

        Catch ex As Exception
            ' ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub


    Private Sub lblViewEISEnrollment_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEISEnrollment.LinkClicked
        Try

            Dim year As String = cboEISMailoutEnrollmentYear.Text

            If cboEISMailoutEnrollmentYear.Text = "" Then

                MsgBox("Please choose a year to view!", MsgBoxStyle.Information, "EIS Enrollment")

            Else
                SQL = "SELECT " & DBNameSpace & ".EIS_admin.FACILITYSITEID, " & _
         "" & DBNameSpace & ".EIS_admin.DATENROLLMENT " & _
        "from " & DBNameSpace & ".EIS_admin " & _
        "where " & DBNameSpace & ".EIS_admin.INVENTORYYEAR = '" & year & "' " & _
        "and strenrollment = '1' "

                dsViewCount = New DataSet
                daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvEISDataCount.DataSource = dsViewCount
                dgvEISDataCount.DataMember = "ViewCount"

                dgvEISDataCount.RowHeadersVisible = False
                dgvEISDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvEISDataCount.AllowUserToResizeColumns = True
                dgvEISDataCount.AllowUserToAddRows = False
                dgvEISDataCount.AllowUserToDeleteRows = False
                dgvEISDataCount.AllowUserToOrderColumns = True
                dgvEISDataCount.AllowUserToResizeRows = True

                dgvEISDataCount.Columns("FACILITYSITEID").HeaderText = "Facility Site ID"
                dgvEISDataCount.Columns("FACILITYSITEID").DisplayIndex = 0
                'dgvEISDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
                'dgvEISDataCount.Columns("strFacilityName").DisplayIndex = 1
                dgvEISDataCount.Columns("DATENROLLMENT").HeaderText = "Transaction Date"
                dgvEISDataCount.Columns("DATENROLLMENT").DisplayIndex = 1

                txtEISRecordNumber.Text = dgvEISDataCount.RowCount.ToString

            End If

        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub

    Private Sub lblViewEISmailoutlist_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEISmailoutlist.LinkClicked
        Try

            Dim year As String = cboEISMailoutEnrollmentYear.Text

            If cboEISMailoutEnrollmentYear.Text = "" Then

                MsgBox("Please choose a year to view!", MsgBoxStyle.Information, "EIS Enrollment")

            Else
                SQL = "SELECT FACILITYSITEID, " & _
                "STRFACILITYNAME, " & _
                "STROPERATIONALSTATUS, " & _
                "STRCLASS, " & _
                "STRCONTACTFIRSTNAME, " & _
                "STRCONTACTLASTNAME, " & _
                "STRCONTACTCOMPANYNAME, " & _
                "STRCONTACTADDRESS1, " & _
                "STRCONTACTCITY, " & _
                "STRCONTACTSTATE, " & _
                "STRCONTACTZIPCODE, " & _
                "STRCONTACTEMAIL " & _
                "from " & DBNameSpace & ".EIS_MailOut " & _
                "where INTINVENTORYYEAR = '" & year & "' " & _
                "order by STRFACILITYNAME"

                dsViewCount = New DataSet
                daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvEISDataCount.DataSource = dsViewCount
                dgvEISDataCount.DataMember = "ViewCount"
                dgvEISDataCount.RowHeadersVisible = False
                dgvEISDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvEISDataCount.AllowUserToResizeColumns = True
                dgvEISDataCount.AllowUserToAddRows = False
                dgvEISDataCount.AllowUserToDeleteRows = False
                dgvEISDataCount.AllowUserToOrderColumns = True
                dgvEISDataCount.AllowUserToResizeRows = True

                dgvEISDataCount.Columns("FACILITYSITEID").HeaderText = "FACILITYSITE ID"
                dgvEISDataCount.Columns("FACILITYSITEID").DisplayIndex = 0
                dgvEISDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvEISDataCount.Columns("strFacilityName").DisplayIndex = 1
                dgvEISDataCount.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status"
                dgvEISDataCount.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
                dgvEISDataCount.Columns("STRCLASS").HeaderText = "Facility Class"
                dgvEISDataCount.Columns("STRCLASS").DisplayIndex = 3
                dgvEISDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
                dgvEISDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 4
                dgvEISDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
                dgvEISDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 5
                dgvEISDataCount.Columns("STRCONTACTCOMPANYname").HeaderText = "Contact Company"
                dgvEISDataCount.Columns("STRCONTACTCOMPANYname").DisplayIndex = 6
                dgvEISDataCount.Columns("STRCONTACTADDRESS1").HeaderText = "Address"
                dgvEISDataCount.Columns("STRCONTACTADDRESS1").DisplayIndex = 7
                dgvEISDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
                dgvEISDataCount.Columns("STRCONTACTCITY").DisplayIndex = 8
                dgvEISDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
                dgvEISDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 9
                dgvEISDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
                dgvEISDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 10
                dgvEISDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
                dgvEISDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 11

                txtEISRecordNumber.Text = dgvEISDataCount.RowCount.ToString


            End If

        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub BtnEISExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEISExportExcel.Click
        Try
            ExportEIStoExcel()
        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Sub ExportEIStoExcel()
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        'Dim ExcelApp As New Excel.Application
        Dim i As Integer
        Dim j As Integer

        Try
            If dgvEISDataCount.RowCount <> 0 Then
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()


                    For i = 0 To dgvEISDataCount.ColumnCount - 1
                        .Cells(1, i + 1) = dgvEISDataCount.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvEISDataCount.ColumnCount - 1
                        For j = 0 To dgvEISDataCount.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvEISDataCount.Item(i, j).Value.ToString
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
                'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        Finally

        End Try
    End Sub


    Private Sub btnSaveEISTypeYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveEISTypeYear.Click
        Dim EIStype As String = cboEISType.SelectedItem
        Dim EIStypeYear As String = cboEISThreholdYear.SelectedItem

        Try

            SQL = "Select " & DBNameSpace & ".EITHRESHOLDYEARS.STRYEAR " & _
          "FROM " & DBNameSpace & ".EITHRESHOLDYEARS " & _
          "where  EITHRESHOLDYEARS.STRYEAR = '" & EIStypeYear & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            SQL = "Update " & DBNameSpace & ".EITHRESHOLDYEARS set " & _
            "STREITYPE = '" & EIStype & "' " & _
            "where " & DBNameSpace & ".EITHRESHOLDYEARS.STRYEAR = '" & EIStypeYear & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            MsgBox("The info has been updated", MsgBoxStyle.Information, "EIS Tools")

        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblViewEISThrehold2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEISThrehold2.LinkClicked
        Try
            Dim EIStype2 As String = cboEISType2.SelectedItem

            SQL = "Select * " & _
                " FROM " & DBNameSpace & ".EITHRESHOLDS " & _
                "where   " & DBNameSpace & ".EITHRESHOLDS.STRTYPE = '" & Replace(EIStype2, "'", "''") & "' "


            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmdBuild = New OracleCommandBuilder(daViewCount)

            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEISDataCount.DataSource = dsViewCount
            dgvEISDataCount.DataMember = "ViewCount"

            dgvEISDataCount.RowHeadersVisible = False
            dgvEISDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEISDataCount.AllowUserToResizeColumns = True
            dgvEISDataCount.AllowUserToAddRows = False
            dgvEISDataCount.AllowUserToDeleteRows = False
            dgvEISDataCount.AllowUserToOrderColumns = True
            dgvEISDataCount.AllowUserToResizeRows = True

            dgvEISDataCount.Columns("STRPOLLUTANT").HeaderText = "Pollutant Name"
            dgvEISDataCount.Columns("STRPOLLUTANT").DisplayIndex = 0
            dgvEISDataCount.Columns("NUMTHRESHOLD").HeaderText = "Threshold (TPY)"
            dgvEISDataCount.Columns("NUMTHRESHOLD").DisplayIndex = 1
            dgvEISDataCount.Columns("NUMTHRESHOLDNAA").HeaderText = "Threshold in NAA (TPY)"
            dgvEISDataCount.Columns("NUMTHRESHOLDNAA").DisplayIndex = 2
            dgvEISDataCount.Columns("STRTYPE").HeaderText = "EIS Type"
            dgvEISDataCount.Columns("STRTYPE").DisplayIndex = 3
            loadEISThresholdDetails()


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub loadEISThresholdDetails()

        Dim SQL As String
        Dim EIStype As String = cboEISType2.SelectedItem
        Try
            SQL = "Select * " & _
            "from " & DBNameSpace & ".EITHRESHOLDS " & _
            "where EITHRESHOLDS.STRTYPE = '" & EIStype & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("strPollutant") = "SOx" Then
                    txtSOxforEISThreshold.Text = dr("numThreshold")
                End If
                If dr("strPollutant") = "VOC" Then
                    txtVOCforEISThreshold.Text = dr("numThreshold")
                End If
                If dr("strPollutant") = "NOx" Then
                    txtNOxForEISThreshold.Text = dr("numThreshold")
                End If
                If dr("strPollutant") = "CO" Then
                    txtCOForEISThreshold.Text = dr("numThreshold")
                End If
                If dr("strPollutant") = "PB" Then
                    If IsDBNull("numThreshold") Then
                        txtPbForEISThreshold.Text = "N/A"
                    Else
                        txtPbForEISThreshold.Text = dr("numThreshold")
                    End If

                End If
                If dr("strPollutant") = "PM10" Then
                    txtPM10forEISThreshold.Text = dr("numThreshold")
                End If
                If dr("strPollutant") = "PM25" Then
                    txtPM25ForEISThreshold.Text = dr("numThreshold")
                End If
                If dr("strPollutant") = "NH3" Then
                    txtNH3ForEISThreshold.Text = dr("numThreshold")
                End If

            End While



        Catch ex As Exception
            '  ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub lblviewEISThreshold_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewEISThreshold.LinkClicked
        Try
            Dim EIStype As String = cboEISType.SelectedItem

            SQL = "Select * " & _
                " FROM " & DBNameSpace & ".EITHRESHOLDS " & _
                "where   " & DBNameSpace & ".EITHRESHOLDS.STRTYPE = '" & Replace(EIStype, "'", "''") & "' "


            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmdBuild = New OracleCommandBuilder(daViewCount)

            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvEISDataCount.DataSource = dsViewCount
            dgvEISDataCount.DataMember = "ViewCount"

            dgvEISDataCount.RowHeadersVisible = False
            dgvEISDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEISDataCount.AllowUserToResizeColumns = True
            dgvEISDataCount.AllowUserToAddRows = False
            dgvEISDataCount.AllowUserToDeleteRows = False
            dgvEISDataCount.AllowUserToOrderColumns = True
            dgvEISDataCount.AllowUserToResizeRows = True

            dgvEISDataCount.Columns("STRPOLLUTANT").HeaderText = "Pollutant Name"
            dgvEISDataCount.Columns("STRPOLLUTANT").DisplayIndex = 0
            dgvEISDataCount.Columns("NUMTHRESHOLD").HeaderText = "Threshold (TPY)"
            dgvEISDataCount.Columns("NUMTHRESHOLD").DisplayIndex = 1
            dgvEISDataCount.Columns("NUMTHRESHOLDNAA").HeaderText = "Threshold in NAA (TPY)"
            dgvEISDataCount.Columns("NUMTHRESHOLDNAA").DisplayIndex = 2
            dgvEISDataCount.Columns("STRTYPE").HeaderText = "EIS Type"
            dgvEISDataCount.Columns("STRTYPE").DisplayIndex = 3


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub btnSaveEISThresholds_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveEISThresholds.Click

    End Sub
    Private Sub btnaddNewEISType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddNewEISType.Click
        Dim EIStype2 As String = txtNewEISType.Text
        Dim EIStypeYear As String = "2010"
        Dim temp As String

        Try
            If txtNewEISType.Text = "" Then
                MsgBox("Please enter new EIS type!", MsgBoxStyle.Information, "EIS Tools")
            Else
                SQL = "Select " & DBNameSpace & ".EITHRESHOLDS.STRTYPE " & _
                                " FROM " & DBNameSpace & ".EITHRESHOLDS " & _
                                "where   " & DBNameSpace & ".EITHRESHOLDS.STRTYPE = '" & Replace(EIStype2, "'", "''") & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    MsgBox("EIS type already exist. Please enter new type! ", MsgBoxStyle.Information, "EIS Tools")
                Else
                    SQL = "Select distinct(" & DBNameSpace & ".EITHRESHOLDS.STRPOLLUTANT) as Pollutant " & _
                   " FROM " & DBNameSpace & ".EITHRESHOLDS "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("Pollutant")) Then
                            temp = ""
                        Else
                            temp = dr.Item("Pollutant")
                        End If

                        If temp <> "" Then
                            SQL = "Insert into AIRBranch.EIThresholds " & _
                                "Values " & _
                                "('" & Replace(temp, "'", "''") & "', " & _
                                "'', '', '" & Replace(EIStype2, "'", "''") & "', " & _
                                "(Select max(PollutantID) + 1 from AIRBranch.EIThresholds) ) "
                            cmd2 = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr2 = cmd2.ExecuteReader
                            dr2.Close()
                        End If
                    End While
                    cboEISType2.Items.Clear()
                    cboEISType.Items.Clear()
                    loadEISType()
                    cboEISType2.Text = ""
                    cboEISType.Text = ""
                    dr.Close()

                    MsgBox("The info has been inserted", MsgBoxStyle.Information, "EIS Tools")

                End If

                SQL = "Select * " & _
                    " FROM " & DBNameSpace & ".EITHRESHOLDS " & _
                    "where   " & DBNameSpace & ".EITHRESHOLDS.STRTYPE = '" & Replace(EIStype2, "'", "''") & "' "

                dsViewCount = New DataSet
                daViewCount = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmdBuild = New OracleCommandBuilder(daViewCount)

                daViewCount.Fill(dsViewCount, "ViewCount")
                dgvEIThresholds.DataSource = dsViewCount
                dgvEIThresholds.DataMember = "ViewCount"

                dgvEISDataCount.RowHeadersVisible = True
                dgvEISDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvEISDataCount.AllowUserToResizeColumns = True
                dgvEISDataCount.AllowUserToAddRows = True
                dgvEISDataCount.AllowUserToDeleteRows = True
                dgvEISDataCount.AllowUserToOrderColumns = True
                dgvEISDataCount.AllowUserToResizeRows = True


                dgvEISDataCount.Columns("STRPOLLUTANT").HeaderText = "Pollutant Name"
                dgvEISDataCount.Columns("STRPOLLUTANT").DisplayIndex = 0
                dgvEISDataCount.Columns("NUMTHRESHOLD").HeaderText = "Thresholds (TPY)"
                dgvEISDataCount.Columns("NUMTHRESHOLD").DisplayIndex = 1
                dgvEISDataCount.Columns("NUMTHRESHOLDNAA").HeaderText = "Thresholds in NAA (TPY)"
                dgvEISDataCount.Columns("NUMTHRESHOLDNAA").DisplayIndex = 2
                dgvEISDataCount.Columns("STRTYPE").HeaderText = "EIS Type"
                dgvEISDataCount.Columns("STRTYPE").DisplayIndex = 3


            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub viewEISCodes()
        Dim EISYear As String = txtEISYear.Text
        Dim FacilitySiteID As String = txtEISAIRSNo.Text

        Try

            SQL = "Select * " & _
                  " FROM " & DBNameSpace & ".EIS_ADMIN " & _
                  "where   " & DBNameSpace & ".EIS_ADMIN.FACILITYSITEID = '" & Replace(FacilitySiteID, "'", "''") & "' " & _
                  " and EIS_ADMIN.INVENTORYYEAR = '" & Replace(EISYear, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Select * " & _
                 " FROM " & DBNameSpace & ".EIS_ADMIN " & _
                 "where   " & DBNameSpace & ".EIS_ADMIN.FACILITYSITEID = '" & Replace(FacilitySiteID, "'", "''") & "' " & _
                 " and EIS_ADMIN.INVENTORYYEAR = '" & Replace(EISYear, "'", "''") & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                dr.Read()

                If IsDBNull(dr("EISACCESSCODE")) Then
                    txtEISAccessCode.Text = " "
                    cboEISAccessCode.SelectedItem = "- Select EIS Access Code -"
                Else
                    txtEISAccessCode.Text = dr("EISACCESSCODE")
                    cboEISAccessCode.SelectedValue = dr("EISACCESSCODE")
                End If

                If IsDBNull(dr("EISSTATUSCODE")) Then
                    txtEISStatusCode.Text = " "
                    cboEISStatusCode.SelectedItem = "- Select EIS Status Code -"
                Else
                    txtEISStatusCode.Text = dr("EISSTATUSCODE")
                    cboEISStatusCode.SelectedValue = dr("EISSTATUSCODE")
                End If
                If IsDBNull(dr("STROPTOUT")) Then
                    txtEISOptoutStatus.Text = " "
                    cboEISOptoutStatus.SelectedItem = "Null"
                Else
                    txtEISOptoutStatus.Text = dr("STROPTOUT")
                    cboEISOptoutStatus.Text = dr("STROPTOUT")
                End If
                If IsDBNull(dr("STRMAILOUT")) Then
                    txtEISMailoutStatus.Text = " "
                    'cboEISMailoutStatus.SelectedItem = "- Select Mailout Status -"
                Else
                    txtEISMailoutStatus.Text = dr("STRMAILOUT")
                    cboEISMailoutStatus.Text = dr("STRMAILOUT")
                End If
                If IsDBNull(dr("STRENROLLMENT")) Then
                    txtEISEnrollmentStatus.Text = " "
                    'cboEISEnrollmentStatus.SelectedItem = "- Select Enrollment Status -"
                Else
                    txtEISEnrollmentStatus.Text = dr("STRENROLLMENT")
                    cboEISEnrollmentStatus.Text = dr("STRENROLLMENT")
                End If
                If IsDBNull(dr("STRCOMMENT")) Then
                    txtEISComments.Text = " "
                Else
                    txtEISComments.Text = dr("STRCOMMENT")
                End If
                If IsDBNull(dr("STRENROLLMENT")) Then
                    txtEISEnrollmentStatus.Text = " "
                    'cboEISEnrollmentStatus.SelectedItem = "- Select Enrollment Status -"
                Else
                    txtEISEnrollmentStatus.Text = dr("STRENROLLMENT")
                    cboEISEnrollmentStatus.Text = dr("STRENROLLMENT")
                End If
                If IsDBNull(dr("STRCOMMENT")) Then
                    txtEISComments.Text = " "
                Else
                    txtEISComments.Text = dr("STRCOMMENT")
                End If
                If IsDBNull(dr("UPDATEDATETIME")) Then
                    txtEISUpdatetime.Text = " "
                Else
                    txtEISUpdatetime.Text = dr("UPDATEDATETIME")
                End If
                If IsDBNull(dr("UPDATEUSER")) Then
                    txtEISUser.Text = " "
                Else
                    txtEISUser.Text = dr("UPDATEUSER")
                End If

            Else
                MsgBox("there is no info available", MsgBoxStyle.Information, "EIS Tools")
            End If
        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnViewEISCodes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewEISCodes.Click
        Dim EISYear As String = txtEISYear.Text
        Dim FacilitySiteID As String = txtEISAIRSNo.Text

        Try
            If EISYear = "" Then
                MsgBox("Please enter EIS Year!", MsgBoxStyle.Information, "Facility Details")
            Else

                If FacilitySiteID = "" Then
                    MsgBox("Please enter AIRS Number!", MsgBoxStyle.Information, "Facility Details")
                Else
                    viewEISCodes()

                End If
            End If


        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnUpdateCodes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateCodes.Click
        Dim AccessCode As String = cboEISAccessCode.SelectedValue
        Dim statuscode As String = cboEISStatusCode.SelectedValue
        Dim optout As String = cboEISOptoutStatus.SelectedItem
        Dim enrollment As String = cboEISEnrollmentStatus.SelectedItem
        Dim mailout As String = cboEISMailoutStatus.SelectedItem
        Dim comments As String = txtEISComments.Text
        Dim sql As String
        Dim fsid As String = txtEISAIRSNo.Text
        Dim inventoryyear As String = txtEISYear.Text
        Dim updateuser As String = UserGCode

        Try

            If optout = "Null" Then
                optout = ""
            End If
            sql = "UPDATE " & DBNameSpace & ".EIS_admin " & _
                 "SET EISSTATUSCODE = '" & statuscode & "', " & _
                 "EISACCESSCODE = '" & AccessCode & "', " & _
                 "STROPTOUT = '" & optout & "', " & _
                 "STRMAILOUT = '" & mailout & "', " & _
                 "STRENROLLMENT = '" & enrollment & "', " & _
                 "UPDATEUSER = '" & updateuser & "', " & _
                 "strcomment = '" & comments & "', " & _
                 "UPDATEDATETIME = '" & OracleDate & "' " & _
                 "WHERE FACILITYSITEID = '" & fsid & "' " & _
                 "and INVENTORYYEAR = '" & inventoryyear & "' "

            Dim cmd As New OracleCommand(sql, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            viewEISCodes()
            MsgBox("Your change is saved", MsgBoxStyle.Information, "EIS Tools")


        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    'Sub FormatWebUsers()
    '    Try
    '        dgvUsers.RowHeadersVisible = False
    '        dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
    '        dgvUsers.AllowUserToResizeColumns = True
    '        dgvUsers.AllowUserToAddRows = False
    '        dgvUsers.AllowUserToDeleteRows = False
    '        dgvUsers.AllowUserToOrderColumns = True
    '        dgvUsers.AllowUserToResizeRows = True
    '        dgvUsers.ColumnHeadersHeight = "35"


    Private Sub btnViewFacilitySiteInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewFacilitySiteInfo.Click
        Dim FacilitySiteID As String = txtFacilitySiteID.Text
        Try
            If FacilitySiteID = "" Then
                MsgBox("Please enter Airs Number!", MsgBoxStyle.Information, "Facility Site")
            Else
                viewEISFacilitySiteInfo()
            End If
        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

    Private Sub viewEISFacilitySiteInfo()
        Dim FacilitySiteID As String = txtFacilitySiteID.Text()
        Dim airsNo As String = "0413" & FacilitySiteID

        Try

            SQL = "Select * " & _
             " FROM " & DBNameSpace & ".APBFACILITYINFORMATION " & _
             "where   " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = '" & Replace(airsNo, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            dr.Read()

            If IsDBNull(dr("STRFACILITYNAME")) Then
                txtFacilityName_Apb.Text = " "
            Else
                txtFacilityName_Apb.Text = dr("STRFACILITYNAME")
            End If
            If IsDBNull(dr("STRFACILITYSTREET1")) Then
                txtLocation_APB.Text = " "
            Else
                txtLocation_APB.Text = dr("STRFACILITYSTREET1")
            End If
            If IsDBNull(dr("STRFACILITYCITY")) Then
                txtLocationCity_APB.Text = " "
            Else
                txtLocationCity_APB.Text = dr("STRFACILITYCITY")
            End If
            If IsDBNull(dr("STRFACILITYZIPCODE")) Then
                txtLocationZip_APB.Text = " "
            Else
                txtLocationZip_APB.Text = dr("STRFACILITYZIPCODE")
            End If
            dr.Close()

            SQL = "Select * " & _
                  " FROM " & DBNameSpace & ".EIS_FACILITYSITE " & _
                  "where   " & DBNameSpace & ".EIS_FACILITYSITE.FACILITYSITEID = '" & Replace(FacilitySiteID, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Select STRFACILITYSITENAME " & _
                 " FROM " & DBNameSpace & ".EIS_FACILITYSITE " & _
                 "where   " & DBNameSpace & ".EIS_FACILITYSITE.FACILITYSITEID = '" & Replace(FacilitySiteID, "'", "''") & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                dr.Read()

                If IsDBNull(dr("STRFACILITYSITENAME")) Then
                    txtEISFacilityName.Text = " "
                Else
                    txtEISFacilityName.Text = dr("STRFACILITYSITENAME")
                End If
                dr.Close()

            Else
                txtEISFacilityName.Text = ""
                txtLocalAddress.Text = ""
                txtLocalCity.Text = ""
                txtLocalZip.Text = ""
                MsgBox("there is no info available in Facility Site table.", MsgBoxStyle.Information, "Facility Sites")
            End If

            SQL = "Select * " & _
                " FROM " & DBNameSpace & ".EIS_FACILITYSITEADDRESS " & _
                "where   " & DBNameSpace & ".EIS_FACILITYSITEADDRESS.FACILITYSITEID = '" & Replace(FacilitySiteID, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then

                SQL = "Select * " & _
                 " FROM " & DBNameSpace & ".EIS_FACILITYSITEADDRESS " & _
                 "where   " & DBNameSpace & ".EIS_FACILITYSITEADDRESS.FACILITYSITEID = '" & Replace(FacilitySiteID, "'", "''") & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                dr.Read()

                If IsDBNull(dr("STRLOCATIONADDRESSTEXT")) Then
                    txtLocalAddress.Text = " "
                Else
                    txtLocalAddress.Text = dr("STRLOCATIONADDRESSTEXT")
                End If

                If IsDBNull(dr("STRLOCALITYNAME")) Then
                    txtLocalCity.Text = " "
                Else
                    txtLocalCity.Text = dr("STRLOCALITYNAME")
                End If

                If IsDBNull(dr("STRLOCATIONADDRESSPOSTALCODE")) Then
                    txtLocalZip.Text = " "
                Else
                    txtLocalZip.Text = dr("STRLOCATIONADDRESSPOSTALCODE")
                End If

                dr.Close()
            Else
                txtEISFacilityName.Text = ""
                txtLocalAddress.Text = ""
                txtLocalCity.Text = ""
                txtLocalZip.Text = ""
                MsgBox("there is no info available in Facility Site Address table", MsgBoxStyle.Information, "Facility Sites")
            End If
        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub btnSaveFacilitySiteInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFacilitySiteInfo.Click
        Dim facilityName As String = txtEISFacilityName.Text
        Dim FacilityAddress As String = txtLocalAddress.Text
        Dim facilityCity As String = txtLocalCity.Text
        Dim facilityZip As String = txtLocalZip.Text
        Dim sql, sql1 As String
        Dim fsid As String = txtFacilitySiteID.Text
        Dim updateuser As String = UserGCode

        Try
            sql = "UPDATE " & DBNameSpace & ".EIS_FACILITYSITE " & _
                 "SET STRFACILITYSITENAME = '" & facilityName & "', " & _
                 "UPDATEUSER = '" & updateuser & "', " & _
                 "UPDATEDATETIME = '" & OracleDate & "' " & _
                 "WHERE FACILITYSITEID = '" & fsid & "' "

            Dim cmd As New OracleCommand(sql, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            sql1 = "UPDATE " & DBNameSpace & ".EIS_FACILITYSITEADDRESS " & _
               "SET STRLOCATIONADDRESSTEXT = '" & FacilityAddress & "', " & _
               "STRLOCALITYNAME = '" & facilityCity & "', " & _
               "STRLOCATIONADDRESSPOSTALCODE = '" & facilityZip & "', " & _
               "UPDATEUSER = '" & updateuser & "', " & _
               "UPDATEDATETIME = '" & OracleDate & "' " & _
               "WHERE FACILITYSITEID = '" & fsid & "' "

            Dim cmd1 As New OracleCommand(sql1, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd1.ExecuteReader
            dr.Close()
            txtEISFacilityName.Text = ""
            txtFacilitySiteID.Text = ""
            txtLocalAddress.Text = ""
            txtLocalCity.Text = ""
            txtLocalZip.Text = ""
            txtFacilityName_Apb.Text = ""
            txtLocation_APB.Text = ""
            txtLocationCity_APB.Text = ""
            txtLocationZip_APB.Text = ""
           
            MsgBox("Your change is saved", MsgBoxStyle.Information, "Facility Site")

        Catch ex As Exception
            'ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbViewUserData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewUserData.LinkClicked
        Try
            ViewFacilitySpecificUsers()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ViewFacilitySpecificUsers()
        Try
            Dim dgvRow As New DataGridViewRow

            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please enter a complete 8 digit AIRS #.", MsgBoxStyle.Information, "DMU Tools")
            Else
                txtEmail.Clear()

                SQL = "Select strFacilityName " & _
                "from " & DBNameSpace & ".APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        lblFaciltyName.Text = " - "
                    Else
                        lblFaciltyName.Text = dr.Item("strFacilityName")
                    End If
                End While
                dr.Close()

                SQL = "SELECT " & _
                "" & DBNameSpace & ".OlapUserAccess.NumUserID as ID, " & DBNameSpace & ".OlapUserLogin.numuserid, " & _
                "" & DBNameSpace & ".OlapUserLogin.strUserEmail as Email, " & _
                "Case " & _
                "When intAdminAccess = 0 Then 'False' " & _
                "When intAdminAccess = 1 Then 'True' " & _
                "End as intAdminAccess, " & _
                "Case " & _
                "When intFeeAccess = 0 Then 'False' " & _
                "When intFeeAccess = 1 Then 'True' " & _
                "End as intFeeAccess, " & _
                "Case " & _
                "When intEIAccess = 0 Then 'False' " & _
                "When intEIAccess = 1 Then 'True' " & _
                "End as intEIAccess, " & _
                "Case " & _
                "When intESAccess = 0 Then 'False' " & _
                "When intESAccess = 1 Then 'True' " & _
                "End as intESAccess " & _
                "FROM " & DBNameSpace & ".OlapUserAccess, " & DBNameSpace & ".OlapUserLogin " & _
                "WHERE " & DBNameSpace & ".OLAPUserAccess.NumUserId = " & DBNameSpace & ".OlapUserLogin.NumUserID " & _
                "AND " & DBNameSpace & ".OlapUserAccess.strAirsNumber = '0413" & mtbAIRSNumber.Text & "' order by email"

                dgvUsers.Rows.Clear()
                ds = New DataSet

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    dgvRow = New DataGridViewRow
                    dgvRow.CreateCells(dgvUsers)
                    If IsDBNull(dr.Item("ID")) Then
                        dgvRow.Cells(0).Value = ""
                    Else
                        dgvRow.Cells(0).Value = dr.Item("ID")
                    End If
                    If IsDBNull(dr.Item("numuserid")) Then
                        dgvRow.Cells(1).Value = ""
                    Else
                        dgvRow.Cells(1).Value = dr.Item("numuserid")
                    End If
                    If IsDBNull(dr.Item("Email")) Then
                        dgvRow.Cells(2).Value = ""
                    Else
                        dgvRow.Cells(2).Value = dr.Item("Email")
                    End If
                    If IsDBNull(dr.Item("intAdminAccess")) Then
                        dgvRow.Cells(3).Value = False
                    Else
                        dgvRow.Cells(3).Value = dr.Item("intAdminAccess")
                    End If
                    If IsDBNull(dr.Item("intFeeAccess")) Then
                        dgvRow.Cells(4).Value = False
                    Else
                        dgvRow.Cells(4).Value = dr.Item("intFeeAccess")
                    End If

                    If IsDBNull(dr.Item("intEIAccess")) Then
                        dgvRow.Cells(5).Value = False
                    Else
                        dgvRow.Cells(5).Value = dr.Item("intEIAccess")
                    End If
                    If IsDBNull(dr.Item("intESAccess")) Then
                        dgvRow.Cells(6).Value = False
                    Else
                        dgvRow.Cells(6).Value = dr.Item("intESAccess")
                    End If
                    dgvUsers.Rows.Add(dgvRow)
                End While
                dr.Close()

                da = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                da.Fill(ds, "FacilityUsers")

                cboUsers.DataSource = ds.Tables("FacilityUsers")
                cboUsers.DisplayMember = "Email"
                cboUsers.ValueMember = "ID"
                cboUsers.Text = ""
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        Try
            Dim userID As Integer

            SQL = "Select numUserId " & _
            "from " & DBNameSpace & ".olapuserlogin " & _
            "where struseremail = '" & Replace(UCase(txtEmail.Text), "'", "''") & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then 'Email address is registered
                userID = dr.Item("numUserId")
                Dim InsertString As String = "Insert into " & DBNameSpace & ".OlapUserAccess " & _
                "(numUserId, strAirsNumber, strFacilityName) values( " & _
                "'" & userID & "', '0413" & mtbAIRSNumber.Text & "', '" & Replace(lblFaciltyName.Text, "'", "''") & "') "

                Dim cmd1 As New OracleCommand(InsertString, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd1.ExecuteNonQuery()

                ViewFacilitySpecificUsers()

                MsgBox("The User has beed added to this facility", MsgBoxStyle.Information, "Insert Success!")
            Else 'email address not registered
                MsgBox("This Email Address is not registered", MsgBoxStyle.OkOnly, "Insert Failed!")
            End If

            If dr.IsClosed = False Then dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            SQL = "DELETE " & DBNameSpace & ".OlapUserAccess " & _
            "WHERE numUserID = '" & cboUsers.SelectedValue & "' " & _
            "and strAirsNumber = '0413" & mtbAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            ViewFacilitySpecificUsers()
            MsgBox("The User has been removed for this facility", MsgBoxStyle.Information, "User Removed!")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            Dim adminaccess As String
            Dim feeaccess As String
            Dim eiaccess As String
            Dim esaccess As String

            For i As Integer = 0 To dgvUsers.Rows.Count - 1
                If dgvUsers(3, i).Value = True Then
                    adminaccess = "1"
                Else
                    adminaccess = "0"
                End If
                If dgvUsers(4, i).Value = True Then
                    feeaccess = "1"
                Else
                    feeaccess = "0"
                End If
                If dgvUsers(5, i).Value = True Then
                    eiaccess = "1"
                Else
                    eiaccess = "0"
                End If
                If dgvUsers(6, i).Value = True Then
                    esaccess = "1"
                Else
                    esaccess = "0"
                End If

                SQL = "UPDATE " & DBNameSpace & ".OlapUserAccess " & _
                "SET intadminaccess = '" & adminaccess & "', " & _
                "intFeeAccess = '" & feeaccess & "', " & _
                "intEIAccess = '" & eiaccess & "', " & _
                "intESAccess = '" & esaccess & "' " & _
                "WHERE numUserID = '" & dgvUsers(1, i).Value & "' " & _
                "and strAirsNumber = '0413" & mtbAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            Next

            ViewFacilitySpecificUsers()
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditUserData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditUserData.Click
        Try
            txtEditFirstName.Visible = True
            txtEditLastName.Visible = True
            txtEditTitle.Visible = True
            txtEditCompany.Visible = True
            txtEditAddress.Visible = True
            txtEditCity.Visible = True
            mtbEditState.Visible = True
            mtbEditZipCode.Visible = True
            mtbEditPhoneNumber.Visible = True
            mtbEditFaxNumber.Visible = True
            btnSaveEditedData.Visible = True
            txtEditUserPassword.Visible = True
            btnChangeEmailAddress.Visible = True
            txtEditEmail.Visible = True
            btnUpdatePassword.Visible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveEditedData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveEditedData.Click
        Try
            Dim FirstName As String = " "
            Dim LastName As String = " "
            Dim Title As String = " "
            Dim Company As String = " "
            Dim Address As String = " "
            Dim City As String = " "
            Dim State As String = " "
            Dim Zip As String = " "
            Dim PhoneNumber As String = " "
            Dim FaxNumber As String = " "

            If txtWebUserID.Text <> "" Then
                If txtEditFirstName.Text <> "" Then
                    FirstName = " strFirstName = '" & Replace(txtEditFirstName.Text, "'", "''") & "', "
                End If
                If txtEditLastName.Text <> "" Then
                    LastName = " strLastName = '" & Replace(txtEditLastName.Text, "'", "''") & "', "
                End If
                If txtEditTitle.Text <> "" Then
                    Title = " strTitle = '" & Replace(txtEditTitle.Text, "'", "''") & "', "
                End If
                If txtEditCompany.Text <> "" Then
                    Company = " strCompanyName = '" & Replace(txtEditCompany.Text, "'", "''") & "', "
                End If
                If txtEditAddress.Text <> "" Then
                    Address = " strAddress = '" & Replace(txtEditAddress.Text, "'", "''") & "', "
                End If
                If txtEditCity.Text <> "" Then
                    City = " strCity = '" & Replace(txtEditCity.Text, "'", "''") & "', "
                End If
                If mtbEditState.Text <> "" Then
                    State = " strState = '" & Replace(mtbEditState.Text, "'", "''") & "', "
                End If
                If mtbEditZipCode.Text <> "" Then
                    Zip = " strZip = '" & Replace(mtbEditZipCode.Text, "'", "''") & "', "
                End If
                If mtbEditPhoneNumber.Text <> "" Then
                    PhoneNumber = " strPhoneNumber = '" & Replace(mtbEditPhoneNumber.Text, "'", "''") & "', "
                End If
                If mtbEditFaxNumber.Text <> "" Then
                    FaxNumber = " strFaxNumber = '" & Replace(mtbEditFaxNumber.Text, "'", "''") & "', "
                End If

                SQL = "Update " & DBNameSpace & ".OLAPUserProfile set " & _
                FirstName & LastName & Title & Company & Address & _
                City & State & Zip & PhoneNumber & FaxNumber & _
                "numUserID = '" & txtWebUserID.Text & "' " & _
                "where numUserID = '" & txtWebUserID.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                lblFName.Text = "First Name: " & txtEditFirstName.Text
                lblLName.Text = "Last Name: " & txtEditLastName.Text
                lblTitle.Text = "Title: " & txtEditTitle.Text
                lblCoName.Text = "Company Name: " & txtEditCompany.Text
                lblAddress.Text = txtEditAddress.Text
                lblCityStateZip.Text = txtEditCity.Text & ", " & mtbEditState.Text & " " & mtbEditZipCode.Text
                lblPhoneNo.Text = "Phone Number: " & mtbEditPhoneNumber.Text
                lblFaxNo.Text = "Fax Number: " & mtbEditFaxNumber.Text

                txtEditFirstName.Visible = False
                txtEditLastName.Visible = False
                txtEditTitle.Visible = False
                txtEditCompany.Visible = False
                txtEditAddress.Visible = False
                txtEditCity.Visible = False
                mtbEditState.Visible = False
                mtbEditZipCode.Visible = False
                mtbEditPhoneNumber.Visible = False
                mtbEditFaxNumber.Visible = False
                btnSaveEditedData.Visible = False
                txtEditUserPassword.Visible = False
                btnChangeEmailAddress.Visible = False
                txtEditEmail.Visible = False
                btnUpdatePassword.Visible = False
            Else

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdatePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdatePassword.Click
        Try
            If txtWebUserID.Text <> "" And txtEditUserPassword.Text <> "" Then
                'New password change code 6/30/2010
                SQL = "Update " & DBNameSpace & ".OLAPUserLogIN set " & _
                "strUserPassword = '" & getMd5Hash(txtEditUserPassword.Text) & "' " & _
                "where numUserID = '" & txtWebUserID.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                txtEditUserPassword.Clear()
                txtEditFirstName.Visible = False
                txtEditLastName.Visible = False
                txtEditTitle.Visible = False
                txtEditCompany.Visible = False
                txtEditAddress.Visible = False
                txtEditCity.Visible = False
                mtbEditState.Visible = False
                mtbEditZipCode.Visible = False
                mtbEditPhoneNumber.Visible = False
                mtbEditFaxNumber.Visible = False
                btnSaveEditedData.Visible = False
                txtEditUserPassword.Visible = False
                btnChangeEmailAddress.Visible = False
                txtEditEmail.Visible = False
                btnUpdatePassword.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnChangeEmailAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeEmailAddress.Click
        Try
            If txtWebUserID.Text <> "" Then
                If EmailAddressIsValid(txtEditEmail.Text) Then
                    SQL = "Select " & _
                    "numUserID, strUserPassword " & _
                    "from " & DBNameSpace & ".OLAPUserLogIN " & _
                    "where upper(strUserEmail) = '" & Replace(txtEditEmail.Text.ToUpper, "'", "''") & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("numUserID")) Then
                            Else
                                If txtWebUserID.Text <> dr.Item("numUserID") Then
                                    MsgBox("Another user already has this email address and it would violate a unique constraint if you were " & _
                                           "to add this email to this user.", MsgBoxStyle.Exclamation, "Mailout and Stats")
                                    Exit Sub
                                End If
                            End If
                        End While
                        dr.Close()
                    End If

                    SQL = "Update " & DBNameSpace & ".OLAPUserLogIn set " & _
                    "strUserEmail = '" & Replace(txtEditEmail.Text.ToUpper, "'", "''") & "' " & _
                    "where numUserID = '" & txtWebUserID.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    cboUserEmail.Text = ""
                    txtWebUserEmail.Text = txtEditEmail.Text

                    '  LoadDataGridFacility(txtWebUserEmail.Text)
                    LoadUserInfo(txtWebUserEmail.Text)

                    If txtWebUserID.Text = "" Then
                        pnlUserInfo.Visible = False
                        pnlUserFacility.Visible = False
                    Else
                        pnlUserInfo.Visible = True
                        pnlUserFacility.Visible = True
                    End If

                Else
                    MsgBox("Invalid Email Address", MsgBoxStyle.Exclamation, "DMU Tools")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddFacilitytoUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFacilitytoUser.Click
        Try
            If txtWebUserID.Text <> "" And mtbFacilityToAdd.Text <> "" Then
                SQL = "Select " & _
                "numUserId " & _
                "from " & DBNameSpace & ".OlapUserAccess " & _
                "where numUserId = '" & txtWebUserID.Text & "' " & _
                "and strAirsNumber = '0413" & mtbFacilityToAdd.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = False Then
                    SQL = "Insert into " & DBNameSpace & ".OlapUserAccess " & _
                     "(numUserId, strAirsNumber, strFacilityName) " & _
                     "values " & _
                     "('" & txtWebUserID.Text & "', '0413" & mtbFacilityToAdd.Text & "', " & _
                     "(select strFacilityName " & _
                     "from " & DBNameSpace & ".APBFacilityInformation " & _
                     "where strAIRSnumber = '0413" & mtbFacilityToAdd.Text & "')) "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    LoadUserFacilityInfo(txtWebUserEmail.Text)
                    MsgBox("The facility has beed added to this user", MsgBoxStyle.Information, "Insert Success!")
                Else
                    MsgBox("The facility already exists for this user." & vbCrLf & "NO DATA SAVED", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteFacilityUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFacilityUser.Click
        Try
            If txtWebUserID.Text <> "" And cboFacilityToDelete.Text <> "" Then
                SQL = "DELETE " & DBNameSpace & ".OlapUserAccess " & _
                "WHERE numUserID = '" & txtWebUserID.Text & "' " & _
                "and strAirsNumber = '0413" & cboFacilityToDelete.SelectedValue & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

                LoadUserFacilityInfo(txtWebUserEmail.Text)
                MsgBox("The facility has been removed for this user", MsgBoxStyle.Information, "Facility Removed!")

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateUser.Click
        Try
            Dim adminaccess As String
            Dim feeaccess As String
            Dim eiaccess As String
            Dim esaccess As String

            For i As Integer = 0 To dgvUserFacilities.Rows.Count - 1
                If dgvUserFacilities(2, i).Value = True Then
                    adminaccess = "1"
                Else
                    adminaccess = "0"
                End If
                If dgvUserFacilities(3, i).Value = True Then
                    feeaccess = "1"
                Else
                    feeaccess = "0"
                End If
                If dgvUserFacilities(4, i).Value = True Then
                    eiaccess = "1"
                Else
                    eiaccess = "0"
                End If
                If dgvUserFacilities(5, i).Value = True Then
                    esaccess = "1"
                Else
                    esaccess = "0"
                End If

                SQL = "UPDATE " & DBNameSpace & ".OlapUserAccess " & _
                "SET intadminaccess = '" & adminaccess & "', " & _
                "intFeeAccess = '" & feeaccess & "', " & _
                "intEIAccess = '" & eiaccess & "', " & _
                "intESAccess = '" & esaccess & "' " & _
                "WHERE numUserID = '" & txtWebUserID.Text & "' " & _
                "and strAirsNumber = '0413" & dgvUserFacilities(0, i).Value & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            Next

            LoadUserFacilityInfo(txtWebUserEmail.Text)
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


#Region "Emission Inventory Log"
    Sub LoadEILog()
        Try


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub






#End Region

    Private Sub btnReloadFSData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReloadFSData.Click
        Try
            If cboEILogYear.Text = "" Or cboEILogYear.Text.Length <> 4 Then
                MsgBox("Please select a valid year from the EIS Year dropdown.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If mtbEILogAIRSNumber.Text = "" Or mtbEILogAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please enter a valid AIRS # into the EIS AIRS #", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            txtEILogSelectedYear.Text = cboEILogYear.Text
            txtEILogSelectedAIRSNumber.Text = mtbEILogAIRSNumber.Text

            LoadAdminData()

            SQL = "select  " & _
            "strFacilitySiteName " & _
            "from " & DBNameSpace & ".EIS_FacilitySite " & _
            "where FacilitySiteId = '" & txtEILogSelectedAIRSNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFacilitySiteName")) Then
                    txtEIModifyFacilityName.Clear()
                    txtEILogFacilityName.Clear()
                Else
                    txtEIModifyFacilityName.Text = dr.Item("strFacilitySiteName")
                    txtEILogFacilityName.Text = dr.Item("strFacilitySiteName")
                End If
            End While
            dr.Close()

            SQL = "select  " & _
            "* " & _
            "from " & DBNameSpace & ".EIS_FacilitySiteAddress " & _
            "where FacilitySiteId = '" & txtEILogSelectedAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strLocationAddressText")) Then
                    txtEIModifyLocation.Clear()
                Else
                    txtEIModifyLocation.Text = dr.Item("strLocationAddressText")
                End If
                If IsDBNull(dr.Item("strLocalityName")) Then
                    txtEIModifyCity.Clear()
                Else
                    txtEIModifyCity.Text = dr.Item("strLocalityName")
                End If
                If IsDBNull(dr.Item("strLocationAddressPostalCode")) Then
                    mtbEIModifyZipCode.Clear()
                Else
                    mtbEIModifyZipCode.Text = dr.Item("strLocationAddressPostalCode")
                End If
            End While
            dr.Close()

            SQL = "select  " & _
            "* " & _
            "from " & DBNameSpace & ".EIS_FacilityGeoCoord " & _
            "where FacilitySiteId = '" & txtEILogSelectedAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("numLatitudeMeasure")) Then
                    mtbEIModifyLatitude.Clear()
                Else
                    mtbEIModifyLatitude.Text = dr.Item("numLatitudeMeasure")
                End If
                If IsDBNull(dr.Item("numLongitudeMeasure")) Then
                    mtbEIModifyLongitude.Clear()
                Else
                    mtbEIModifyLongitude.Text = dr.Item("numLongitudeMeasure")
                End If
            End While
            dr.Close()

            SQL = "select * " & _
            "from " & DBNameSpace & ".APBFacilityInformation " & _
            "where strAIRSNumber = '0413" & txtEILogSelectedAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtEIModifyIAIPFacilityName.Clear()
                Else
                    txtEIModifyIAIPFacilityName.Text = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    txtEIModifyIAIPLocation.Clear()
                Else
                    txtEIModifyIAIPLocation.Text = dr.Item("strFacilityStreet1")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    txtEIModifyIAIPCity.Clear()
                Else
                    txtEIModifyIAIPCity.Text = dr.Item("strFacilityCity")
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    mtbEIModifyIAIPZipCode.Clear()
                Else
                    mtbEIModifyIAIPZipCode.Text = dr.Item("strFacilityZipCode")
                End If
                If IsDBNull(dr.Item("numFacilityLongitude")) Then
                    mtbEIModifyIAIPLongitude.Clear()
                Else
                    mtbEIModifyIAIPLongitude.Text = dr.Item("numFacilityLongitude")
                End If
                If IsDBNull(dr.Item("numFacilityLatitude")) Then
                    mtbEIModifyIAIPLatitude.Clear()
                Else
                    mtbEIModifyIAIPLatitude.Text = dr.Item("numFacilityLatitude")
                End If
            End While
            dr.Close()

            SQL = "Select * " & _
            "from " & DBNameSpace & ".EIS_Mailout " & _
            "where intInventoryYear = '" & txtEILogSelectedYear.Text & "' " & _
            "and FacilitySiteID = '" & txtEILogSelectedAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtEISMailoutFacilityName.Clear()
                Else
                    txtEISMailoutFacilityName.Text = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtEISMailoutCompanyName.Clear()
                Else
                    txtEISMailoutCompanyName.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtEISMailoutAddress.Clear()
                Else
                    txtEISMailoutAddress.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactAddress2")) Then
                    txtEISMailoutAddress2.Clear()
                Else
                    txtEISMailoutAddress2.Text = dr.Item("strContactAddress2")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtEISMailoutCity.Clear()
                Else
                    txtEISMailoutCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    txtEISMailoutState.Clear()
                Else
                    txtEISMailoutState.Text = dr.Item("strContactState")
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    txtEISMailoutZipCode.Clear()
                Else
                    txtEISMailoutZipCode.Text = dr.Item("strContactZipCode")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    txtEISMailoutFirstName.Clear()
                Else
                    txtEISMailoutFirstName.Text = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtEISMailoutLastName.Clear()
                Else
                    txtEISMailoutLastName.Text = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtEISMailoutPrefix.Clear()
                Else
                    txtEISMailoutPrefix.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtEISMailoutEmail.Clear()
                Else
                    txtEISMailoutEmail.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtEISMailoutComments.Clear()
                Else
                    txtEISMailoutComments.Text = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("UpdateUser")) Then
                    txtEISMailoutUpdateUser.Clear()
                Else
                    txtEISMailoutUpdateUser.Text = dr.Item("UpdateUser")
                End If
                If IsDBNull(dr.Item("UpdateDateTime")) Then
                    txtEISMailoutUpdateDateTime.Clear()
                Else
                    txtEISMailoutUpdateDateTime.Text = dr.Item("UpdateDateTime")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    txtEISMailoutCreateDateTime.Clear()
                Else
                    txtEISMailoutCreateDateTime.Text = dr.Item("CreateDateTime")
                End If
            End While
            dr.Close()

            SQL = "select " & _
            "strContactFirstName, strContactLastName, " & _
            "strContactPrefix, strContactSuffix, " & _
            "strContactTitle, strContactPhoneNumber1, " & _
            "strContactPhoneNumber2, strContactFaxNumber, " & _
            "strContactEmail, strContactCompanyName, " & _
            "strContactAddress1, strContactAddress2, " & _
            "strContactCity, strContactState, " & _
            "strContactZipCode, strContactDescription, " & _
            "datModifingDate, (strLastName||', '||strFirstName) as ModifingPerson " & _
            "from " & DBNameSpace & ".APBContactInformation, " & DBNameSpace & ".EPDUserProfiles " & _
            "where " & DBNameSpace & ".APBContactInformation.strModifingPerson = " & _
            "" & DBNameSpace & ".EPDUserProfiles.numUserID  " & _
            "and strContactKey = '0413" & txtEILogSelectedAIRSNumber.Text & "41' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    txtEISContactFirstName.Clear()
                Else
                    txtEISContactFirstName.Text = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtEISContactLastName.Clear()
                Else
                    txtEISContactLastName.Text = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtEISContactPrefix.Clear()
                Else
                    txtEISContactPrefix.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactSuffix")) Then
                    txtEISContactSuffix.Clear()
                Else
                    txtEISContactSuffix.Text = dr.Item("strContactSuffix")
                End If
                If IsDBNull(dr.Item("strContactTitle")) Then
                    txtEISContactTitle.Clear()
                Else
                    txtEISContactTitle.Text = dr.Item("strContactTitle")
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                    txtEISContactPhone.Clear()
                Else
                    txtEISContactPhone.Text = dr.Item("strContactPhoneNumber1")
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber2")) Then
                    txtEISContactPhone2.Clear()
                Else
                    txtEISContactPhone2.Text = dr.Item("strContactPhoneNumber2")
                End If
                If IsDBNull(dr.Item("strContactFaxNumber")) Then
                    txtEISContactFax.Clear()
                Else
                    txtEISContactFax.Text = dr.Item("strContactFaxNumber")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtEISContactEmail.Clear()
                Else
                    txtEISContactEmail.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtEISContactCompanyName.Clear()
                Else
                    txtEISContactCompanyName.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtEISContactAddress.Clear()
                Else
                    txtEISContactAddress.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactAddress2")) Then
                    txtEISContactAddress2.Clear()
                Else
                    txtEISContactAddress2.Text = dr.Item("strContactAddress2")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtEISContactCity.Clear()
                Else
                    txtEISContactCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    txtEISContactState.Clear()
                Else
                    txtEISContactState.Text = dr.Item("strContactState")
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    txtEISContactZipCode.Clear()
                Else
                    txtEISContactZipCode.Text = dr.Item("strContactZipCode")
                End If
                If IsDBNull(dr.Item("strContactDescription")) Then
                    txtEISContactDescription.Clear()
                Else
                    txtEISContactDescription.Text = dr.Item("strContactDescription")
                End If
                If IsDBNull(dr.Item("ModifingPerson")) Then
                    txtEISContactUpdateUser.Clear()
                Else
                    txtEISContactUpdateUser.Text = dr.Item("ModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    txtEISContactUpdateDateTime.Clear()
                Else
                    txtEISContactUpdateDateTime.Text = dr.Item("datModifingDate")
                End If

            End While
            dr.Close()

            LoadQASpecificData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub LoadAdminData()
        Try
            dtpDeadlineEIS.Checked = False
            txtEISDeadlineComment.Clear()
            txtAllEISDeadlineComment.Clear()

            SQL = "Select * " & _
           "From " & DBNameSpace & ".EIS_Admin " & _
           "where inventoryYear = '" & txtEILogSelectedYear.Text & "' " & _
           "and FacilitySiteID = '" & txtEILogSelectedAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("EISStatusCode")) Then
                    cboEILogStatusCode.Text = ""
                Else
                    cboEILogStatusCode.SelectedValue = dr.Item("EISStatusCode")
                    txtEILogStatusCode.Text = cboEILogStatusCode.Text
                End If
                If IsDBNull(dr.Item("datEISStatus")) Then
                    dtpEILogStatusDateSubmit.Text = OracleDate
                Else
                    dtpEILogStatusDateSubmit.Text = dr.Item("datEISStatus")
                End If
                If IsDBNull(dr.Item("EISAccessCode")) Then
                    cboEILogAccessCode.Text = ""
                Else
                    cboEILogAccessCode.SelectedValue = dr.Item("EISAccessCode")
                End If
                If IsDBNull(dr.Item("strOptOut")) Then
                    rdbEILogOpOutYes.Checked = False
                    rdbEILogOpOutNo.Checked = False
                Else
                    If dr.Item("strOptOut") = "1" Then
                        rdbEILogOpOutYes.Checked = True
                    Else
                        rdbEILogOpOutNo.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strIncorrectOptOut")) Then
                    chbOptedOutIncorrectly.Checked = False
                Else
                    If dr.Item("strIncorrectOptOut") = "1" Then
                        chbOptedOutIncorrectly.Checked = True
                    Else
                        chbOptedOutIncorrectly.Checked = False
                    End If
                End If

                'Not displayed
                'If IsDBNull(dr.Item("datInitialFinalize")) Then

                'Else

                'End If
                'If IsDBNull(dr.Item("datFinalize")) Then

                'Else

                'End If
                'If IsDBNull(dr.Item("strConfirmationNumber")) Then

                'Else

                'End If
                If IsDBNull(dr.Item("strMailout")) Then
                    rdbEILogMailoutYes.Checked = False
                    rdbEILogMailoutNo.Checked = False
                Else
                    If dr.Item("strMailout") = "1" Then
                        rdbEILogMailoutYes.Checked = True
                    Else
                        rdbEILogMailoutNo.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("strEnrollment")) Then
                    rdbEILogEnrolledYes.Checked = False
                    rdbEILogEnrolledNo.Checked = False
                Else
                    If dr.Item("strEnrollment") = "1" Then
                        rdbEILogEnrolledYes.Checked = True
                    Else
                        rdbEILogEnrolledNo.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("datEnrollment")) Then
                    dtpEILogDateEnrolled.Text = OracleDate
                Else
                    dtpEILogDateEnrolled.Text = dr.Item("datEnrollment")
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtEILogComments.Clear()
                Else
                    txtEILogComments.Text = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("Active")) Then
                    rdbEILogActiveYes.Checked = False
                    rdbEILogActiveNo.Checked = False
                Else
                    If dr.Item("Active") = "1" Then
                        rdbEILogActiveYes.Checked = True
                    Else
                        rdbEILogActiveNo.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("updateUser")) Then
                    txtEILogUpdatedBy.Clear()
                Else
                    txtEILogUpdatedBy.Text = dr.Item("UpdateUser")
                End If
                If IsDBNull(dr.Item("updateDatetime")) Then
                    txtEILogUpdatedTime.Clear()
                Else
                    txtEILogUpdatedTime.Text = dr.Item("updateDatetime")
                End If
                If IsDBNull(dr.Item("intPrepopYear")) Then
                    txtEILogPrePopYear.Clear()
                Else
                    txtEILogPrePopYear.Text = dr.Item("intPrepopYear")
                End If
                If IsDBNull(dr.Item("datEISDeadline")) Then

                Else
                    dtpDeadlineEIS.Text = dr.Item("datEISDeadline")
                    dtpDeadlineEIS.Checked = False
                End If
                If IsDBNull(dr.Item("strEISDeadlineComment")) Then

                Else
                    txtAllEISDeadlineComment.Text = dr.Item("strEISDeadlineComment")
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadQASpecificData()
        Try
            'dtpQAStarted.Text = OracleDate
            'dtpQAPassed.Text = OracleDate
            'dtpQAPassed.Checked = False
            'cboEISQAStatus.Text = ""
            'cboEISQAStaff.Text = ""
            'dtpQAStatus.Text = OracleDate
            'dtpQACompleted.Text = OracleDate
            'dtpQACompleted.Checked = False
            'txtQAComments.Clear()
            'txtFITrackingNumber.Text = ""
            'txtPointTrackingNumber.Text = ""
            'chbFIErrors.Checked = False
            'chbPointErrors.Checked = False
            'pnlQAProcess.Enabled = False

            'SQL = "Select * " & _
            '"from " & DBNameSpace & ".EIS_QAAdmin " & _
            '"where inventoryYear = '" & cboEILogYear.Text & "' " & _
            '"and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "
            'cmd = New OracleCommand(SQL, conn)
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If
            'dr = cmd.ExecuteReader
            'While dr.Read
            '    If IsDBNull(dr.Item("datDateQAStart")) Then
            '        dtpQAStarted.Text = OracleDate
            '    Else
            '        dtpQAStarted.Text = dr.Item("datDateQAStart")
            '    End If
            '    If IsDBNull(dr.Item("datDateQAPass")) Then
            '        dtpQAPassed.Text = OracleDate
            '        dtpQAPassed.Checked = False
            '    Else
            '        dtpQAPassed.Text = dr.Item("datDateQAPass")
            '        dtpQAPassed.Checked = True
            '    End If
            '    If IsDBNull(dr.Item("QAStatusCode")) Then
            '        cboEISQAStatus.Text = ""
            '    Else
            '        cboEISQAStatus.SelectedValue = dr.Item("QAStatusCode")
            '    End If
            '    If IsDBNull(dr.Item("datQAStatus")) Then
            '        dtpQAStatus.Text = OracleDate
            '    Else
            '        dtpQAStatus.Text = dr.Item("datQAStatus")
            '    End If
            '    If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
            '        cboEISQAStaff.Text = ""
            '    Else
            '        cboEISQAStaff.Text = dr.Item("strDMUResponsibleStaff")
            '    End If
            '    If IsDBNull(dr.Item("datQAComplete")) Then
            '        dtpQACompleted.Text = OracleDate
            '        dtpQACompleted.Checked = False
            '    Else
            '        dtpQACompleted.Text = dr.Item("datQAComplete")
            '        dtpQACompleted.Checked = True
            '    End If
            '    If IsDBNull(dr.Item("strComment")) Then
            '        txtQAComments.Clear()
            '    Else
            '        txtQAComments.Text = dr.Item("strComment")
            '    End If
            '    If IsDBNull(dr.Item("strFITrackingNumber")) Then
            '        txtFITrackingNumber.Text = ""
            '        txtAllFITrackingNumbers.Clear()
            '    Else
            '        txtFITrackingNumber.Text = ""
            '        txtAllFITrackingNumbers.Text = dr.Item("strFITrackingNumber")
            '    End If
            '    If IsDBNull(dr.Item("strPointTrackingNumber")) Then
            '        txtPointTrackingNumber.Text = ""
            '        txtAllPointTrackingNumbers.Clear()
            '    Else
            '        txtPointTrackingNumber.Text = ""
            '        txtAllPointTrackingNumbers.Text = dr.Item("strPointTrackingNumber")
            '    End If
            '    If IsDBNull(dr.Item("strFIError")) Then
            '        chbFIErrors.Checked = False
            '    Else
            '        If dr.Item("strFIError") = "True" Then
            '            chbFIErrors.Checked = True
            '        Else
            '            chbFIErrors.Checked = False
            '        End If
            '    End If
            '    If IsDBNull(dr.Item("strPointError")) Then
            '        chbPointErrors.Checked = False
            '    Else
            '        If dr.Item("strpointError") = "True" Then
            '            chbPointErrors.Checked = True
            '        Else
            '            chbPointErrors.Checked = False
            '        End If
            '    End If
            'End While
            'dr.Close()

            'If cboEILogStatusCode.SelectedValue >= 4 Then
            '    pnlQAProcess.Enabled = True
            'End If

            dtpQAStarted.Text = OracleDate
            dtpQAPassed.Text = OracleDate
            dtpQAPassed.Checked = False
            cboEISQAStatus.Text = ""
            cboEISQAStaff.Text = ""
            dtpQAStatus.Text = OracleDate
            dtpQACompleted.Text = OracleDate
            dtpQACompleted.Checked = False
            txtQAComments.Clear()
            txtFITrackingNumber.Text = ""
            txtAllFITrackingNumbers.Clear()
            txtPointTrackingNumber.Text = ""
            txtAllPointTrackingNumbers.Clear()
            chbFIErrors.Checked = False
            chbPointErrors.Checked = False
            dtpEISDeadline.Text = OracleDate
            dtpEISDeadline.Checked = False
            txtEISDeadlineComment.Clear()
            txtAllEISDeadlineComment.Clear()

            SQL = "Select * " & _
            "from " & DBNameSpace & ".EIS_QAAdmin " & _
            "where inventoryYear = '" & cboEILogYear.Text & "' " & _
            "and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("datDateQAStart")) Then
                    dtpQAStarted.Text = OracleDate
                Else
                    dtpQAStarted.Text = dr.Item("datDateQAStart")
                End If
                If IsDBNull(dr.Item("datDateQAPass")) Then
                    dtpQAPassed.Text = OracleDate
                    dtpQAPassed.Checked = False
                Else
                    dtpQAPassed.Text = dr.Item("datDateQAPass")
                    dtpQAPassed.Checked = True
                End If
                If IsDBNull(dr.Item("QAStatusCode")) Then
                    cboEISQAStatus.Text = ""
                Else
                    cboEISQAStatus.SelectedValue = dr.Item("QAStatusCode")
                End If
                If IsDBNull(dr.Item("datQAStatus")) Then
                    dtpQAStatus.Text = OracleDate
                Else
                    dtpQAStatus.Text = dr.Item("datQAStatus")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    cboEISQAStaff.Text = ""
                Else
                    cboEISQAStaff.Text = dr.Item("strDMUResponsibleStaff")
                End If
                If IsDBNull(dr.Item("datQAComplete")) Then
                    dtpQACompleted.Text = OracleDate
                    dtpQACompleted.Checked = False
                Else
                    dtpQACompleted.Text = dr.Item("datQAComplete")
                    dtpQACompleted.Checked = True
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtQAComments.Clear()
                    txtAllQAComments.Clear()
                Else
                    txtAllQAComments.Clear()
                    txtAllQAComments.Text = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("strFITrackingNumber")) Then
                    txtFITrackingNumber.Text = ""
                    txtAllFITrackingNumbers.Clear()
                Else
                    txtFITrackingNumber.Text = ""
                    txtAllFITrackingNumbers.Text = dr.Item("strFITrackingNumber")
                End If
                If IsDBNull(dr.Item("strPointTrackingNumber")) Then
                    txtPointTrackingNumber.Text = ""
                    txtAllPointTrackingNumbers.Clear()
                Else
                    txtPointTrackingNumber.Text = ""
                    txtAllPointTrackingNumbers.Text = dr.Item("strPointTrackingNumber")
                End If
                If IsDBNull(dr.Item("strFIError")) Then
                    chbFIErrors.Checked = False
                Else
                    If dr.Item("strFIError") = "True" Then
                        chbFIErrors.Checked = True
                    Else
                        chbFIErrors.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strPointError")) Then
                    chbPointErrors.Checked = False
                Else
                    If dr.Item("strpointError") = "True" Then
                        chbPointErrors.Checked = True
                    Else
                        chbPointErrors.Checked = False
                    End If
                End If
            End While
            dr.Close()

            If cboEILogStatusCode.SelectedValue >= 4 Then
                pnlQAProcess.Enabled = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnViewEISStats_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewEISStats.Click
        Try
            ViewEISStats()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub ViewEISStats()
        Try
            Dim CurrentTabPage As TabPage = TCEISStats.SelectedTab

            Select Case CurrentTabPage.Name.ToString
                Case "TPEISStatSummary"
                    txtSelectedEISStatYear.Text = cboEISStatisticsYear.Text

                    If txtSelectedEISStatYear.Text.Length <> 4 Then
                        MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

                    SQL = "select * from " & _
                     "(select count(*) as EISUniverse " & _
                     "from AIRbranch.EIS_Admin " & _
                     "where active = '1' " & _
                     "and inventoryyear = '" & cboEISStatisticsYear.Text & "'), " & _
                     "(select count(*) as EISMailout " & _
                     "from AIRbranch.EIS_Admin " & _
                     "where active = '1' " & _
                     "and inventoryyear = '" & cboEISStatisticsYear.Text & "'  " & _
                     "and strMailout = '1' ), " & _
                     "(select count(*) as EISEnrollment " & _
                     "from AIRbranch.EIS_Admin " & _
                     "where active = '1' " & _
                     "and inventoryyear = '" & cboEISStatisticsYear.Text & "'  " & _
                     "and strEnrollment = '1' ),  " & _
                     "(select count(*) as EISUNEnrollment " & _
                     "from AIRbranch.EIS_Admin " & _
                     "where active = '1' " & _
                     "and inventoryyear = '" & cboEISStatisticsYear.Text & "'  " & _
                     "and strMailout = '1' " & _
                     "and (strEnrollment = '0')),   " & _
                     "(select count(*) as EISNoActivity " & _
                     "from AIRbranch.EIS_Admin " & _
                     "where active = '1' " & _
                     "and inventoryyear = '" & cboEISStatisticsYear.Text & "' " & _
                     "and strOptOut is null and strEnrollment = '1'), " & _
                     "(select count(*) as EISOptsIn " & _
                     "from AIRbranch.EIS_Admin " & _
                     "where active = '1' " & _
                     "and inventoryyear = '" & cboEISStatisticsYear.Text & "' " & _
                     "and strOptOut = '0' and strEnrollment = '1'), " & _
                     "(select count(*) as EISOptsOut " & _
                     "from AIRbranch.EIS_Admin " & _
                     "where active = '1' " & _
                     "and inventoryyear = '" & cboEISStatisticsYear.Text & "'  " & _
                     "and strMailout = '1' " & _
                     "and strEnrollment = '1' " & _
                     "and (strOptOut = '1') and strEnrollment = '1' ), " & _
                     "(select count(*) as EISSubmittal  " & _
                     "from AIRbranch.EIS_Admin " & _
                     "where active = '1' " & _
                     "and inventoryyear = '" & cboEISStatisticsYear.Text & "'  " & _
                     "and strEnrollment = '1' " & _
                     "and eisstatuscode >= '3' " & _
                     "and (strOptOut = '0' )), " & _
                     "(select count(*) as EISInProgress " & _
                     "from AIRBranch.EIS_Admin " & _
                     "where active = '1' " & _
                     "and inventoryYear = '" & cboEISStatisticsYear.Text & "' " & _
                     "and strEnrollment = '1' " & _
                     "and eisStatuscode = '2' and strEnrollment = '1' " & _
                     "and (strOptOut = '0')), " & _
                     "(select count(*) as EISQABegan   " & _
                     "from AIRbranch.EIS_Admin " & _
                     "where active = '1' " & _
                     "and inventoryyear = '" & cboEISStatisticsYear.Text & "'  " & _
                     "and strMailout = '1' " & _
                     "and strEnrollment = '1' " & _
                     "and EISAccesscode = '2'  " & _
                     "and eisstatuscode = '4' " & _
                     "and (strOptOut = '0' )), " & _
                     "(select count(*) as EISEPASubmitted   " & _
                     "from AIRbranch.EIS_Admin " & _
                     "where active = '1' " & _
                     "and inventoryyear = '" & cboEISStatisticsYear.Text & "'  " & _
                     "and strMailout = '1' " & _
                     "and strEnrollment = '1' " & _
                     "and EISAccesscode = '0'  " & _
                     "and eisstatuscode = '5' " & _
                     "and (strOptOut = '0' )), " & _
                     "(select count(*) as EISFinalized " & _
                     "from AIRbranch.EIS_Admin " & _
                     "where active = '1' " & _
                     "and inventoryYear = '" & cboEISStatisticsYear.Text & "' " & _
                     "and strEnrollment = '1' " & _
                     "and (EISStatusCode = '3' OR EISStatusCode = '4' OR EISStatusCode = '5')), " & _
             "( select count(*) as QASubmittedToDo " & _
             "from AIRbranch.EIS_Admin  " & _
             "where active = '1'  " & _
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "'   " & _
             "and strEnrollment = '1'  " & _
             "and eisstatuscode >= 3 " & _
             "and (strOptOut = '0' ) " & _
             "and  NOT  exists (Select * from AIRBranch.EIS_QAAdmin " & _
             "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
             "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID) ), " & _
             "( select count(*) as QAOptOutToDo " & _
             "from AIRbranch.EIS_Admin  " & _
             "where active = '1'  " & _
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "'   " & _
             "and strEnrollment = '1'  " & _
             "and (eisstatuscode = 3 or eisstatuscode = 4) " & _
             "and (strOptOut = '1' or strOptout is null ) " & _
             "and  NOT  exists (Select * from AIRBranch.EIS_QAAdmin " & _
             "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
             "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID) ), " & _
             "( select count(*) as QASubmittedBegan   " & _
             "from AIRbranch.EIS_Admin  " & _
             "where active = '1'  " & _
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "'   " & _
             "and strEnrollment = '1'  " & _
             "and eisstatuscode >= 3   " & _
             "and (strOptOut = '0' ) " & _
             "and    exists (Select * from AIRBranch.EIS_QAAdmin " & _
             "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
             "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
             "and datQAComplete is null ) ), " & _
             "( select count(*) as QAOptOutBegan   " & _
             "from AIRbranch.EIS_Admin  " & _
             "where active = '1'  " & _
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "'   " & _
             "and strEnrollment = '1'  " & _
             "and (eisstatuscode = '3' or eisstatuscode = '4')   " & _
             "and (strOptOut = '1' or strOptout is null) " & _
             "and  (not  exists (Select * from AIRBranch.EIS_QAAdmin " & _
             "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
             "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
             "and datQAComplete is null )   " & _
             "or  exists (Select * from AIRBranch.EIS_QAAdmin " & _
             "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
             "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
             "and datQAComplete is null ))), " & _
             "( select count(*) as QASubmittedToEPA  " & _
             "from AIRbranch.EIS_Admin  " & _
             "where active = '1'  " & _
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "'   " & _
             "and strEnrollment = '1'  " & _
             "and eisstatuscode >= '3' " & _
             "and (strOptOut = '0' ) " & _
             "and    exists (Select * from AIRBranch.EIS_QAAdmin " & _
             "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
             "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
              "and datQAComplete is not null ) ),  " & _
             "( select count(*) as QAOptOutToEPA  " & _
             "from AIRbranch.EIS_Admin  " & _
             "where active = '1'  " & _
             "and inventoryyear = '" & cboEISStatisticsYear.Text & "'   " & _
             "and strEnrollment = '1'  " & _
             "and eisstatuscode = '5'  " & _
             "and (strOptOut = '1' or strOptout is null ) " & _
             "and  (not  exists (Select * from AIRBranch.EIS_QAAdmin " & _
             "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
             "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID) " & _
             "OR " & _
             "exists (Select * from AIRBranch.EIS_QAAdmin " & _
             "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
             "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
              "and datQAComplete is not null )" & _
              " ) ), " & _
              "(select count(*) as FIPassed " & _
              "from airbranch.EIS_Admin, AIRBranch.EIS_QAAdmin " & _
              "where EIS_Admin.InventoryYear = EIS_QAAdmin.inventoryYEar " & _
              "and EIS_Admin.facilitysiteID = EIS_QAAdmin.facilitysiteID " & _
              "and eis_qaAdmin.qaStatusCode = '2' " & _
              "and eis_admin.inventoryyear = '" & cboEISStatisticsYear.Text & "' ) "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("EISUniverse")) Then
                            txtEISActiveEIUniverse.Clear()
                        Else
                            txtEISActiveEIUniverse.Text = dr.Item("EISUniverse")
                        End If
                        If IsDBNull(dr.Item("EISMailout")) Then
                            txtEISMailout.Clear()
                        Else
                            txtEISMailout.Text = dr.Item("EISMailout")
                        End If
                        If IsDBNull(dr.Item("EISEnrollment")) Then
                            txtEISEnrolled.Clear()
                        Else
                            txtEISEnrolled.Text = dr.Item("EISEnrollment")
                        End If
                        If IsDBNull(dr.Item("EISUnenrollment")) Then
                            txtEISUnenrolled.Clear()
                        Else
                            txtEISUnenrolled.Text = dr.Item("EISUnenrollment")
                        End If
                        If IsDBNull(dr.Item("EISNoActivity")) Then
                            txtEISNoActivity.Clear()
                        Else
                            txtEISNoActivity.Text = dr.Item("EISNoActivity")
                        End If
                        If IsDBNull(dr.Item("EISOptsIn")) Then
                            txtEISOptedIn.Clear()
                        Else
                            txtEISOptedIn.Text = dr.Item("EISOptsIn")
                        End If
                        If IsDBNull(dr.Item("EISOptsOut")) Then
                            txtEISOptedOut.Clear()
                        Else
                            txtEISOptedOut.Text = dr.Item("EISOptsOut")
                        End If
                        If IsDBNull(dr.Item("EISInProgress")) Then
                            txtEISInProgress.Clear()
                        Else
                            txtEISInProgress.Text = dr.Item("EISInProgress")
                        End If
                        If IsDBNull(dr.Item("EISSubmittal")) Then
                            txtEISSubmitted.Clear()
                        Else
                            txtEISSubmitted.Text = dr.Item("EISSubmittal")
                        End If
                        If IsDBNull(dr.Item("EISQABegan")) Then
                            txtEISQABegan.Clear()
                        Else
                            txtEISQABegan.Text = dr.Item("EISQABegan")
                        End If

                        If IsDBNull(dr.Item("EISFinalized")) Then
                            txtEISFinalized.Clear()
                        Else
                            txtEISFinalized.Text = dr.Item("EISFinalized")
                        End If

                        If IsDBNull(dr.Item("QASubmittedToDo")) Then
                            txtEISSubmittedToDo.Clear()
                        Else
                            txtEISSubmittedToDo.Text = dr.Item("QASubmittedToDO")
                        End If
                        If IsDBNull(dr.Item("QASubmittedBegan")) Then
                            txtEISSubmittedBegan.Clear()
                        Else
                            txtEISSubmittedBegan.Text = dr.Item("QASubmittedBegan")
                        End If
                        If IsDBNull(dr.Item("QASubmittedToEPA")) Then
                            txtEISSubmittedToEPA.Clear()
                        Else
                            txtEISSubmittedToEPA.Text = dr.Item("QASubmittedToEPA")
                        End If

                        If IsDBNull(dr.Item("QAOptOutToDo")) Then
                            txtEISOpOutToDo.Clear()
                        Else
                            txtEISOpOutToDo.Text = dr.Item("QAOptOutToDo")
                        End If
                        If IsDBNull(dr.Item("QAOptOutBegan")) Then
                            txtEISOpOutBegan.Clear()
                        Else
                            txtEISOpOutBegan.Text = dr.Item("QAOptOutBegan")
                        End If
                        If IsDBNull(dr.Item("QAOptOutToEPA")) Then
                            txtEISOpOutToEPA.Clear()
                        Else
                            txtEISOpOutToEPA.Text = dr.Item("QAOptOutToEPA")
                        End If
                        If IsDBNull(dr.Item("FIPassed")) Then
                            txtEISFIPassed.Clear()
                        Else
                            txtEISFIPassed.Text = dr.Item("FIPassed")
                        End If
                    End While
                    dr.Close()
                Case "TPEISStatMailout"
                    txtSelectedEISMailout.Text = cboEISStatisticsYear.Text
                Case "TPEISEnrollment"
                    txtEISStatsEnrollmentYear.Text = cboEISStatisticsYear.Text

                    ' If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                    '     MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                    '     Exit Sub
                    ' End If

                    ' Dim dgvRow As New DataGridViewRow
                    ' dgvEISStats.Rows.Clear()
                    ' SQL = "select " & _
                    ' "'False' as ID, " & _
                    ' " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
                    '"" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
                    '"" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
                    '"AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
                    '"AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
                    '"case " & _
                    '"when strOptOut = '1' then 'Yes' " & _
                    '"when strOptOut = '0' then 'No' " & _
                    '"else '-' " & _
                    '"End strOptOut, " & _
                    '"case " & _
                    '"when strMailout = '1' then 'Yes' " & _
                    '"else 'No' " & _
                    '"end strMailout, " & _
                    '"case " & _
                    '"when strEnrollment = '1' then 'Yes' " & _
                    '"when strEnrollment = '0' then 'No' " & _
                    '"else '-' " & _
                    '"end strEnrollment, " & _
                    '"case " & _
                    '"when strContactEmail is null then '-' " & _
                    '"else strContactEmail " & _
                    '"end ContactEmail, " & _
                    '"case " & _
                    '"When strContactPrefix is null then '-' " & _
                    '"else strContactPrefix " & _
                    '"end strContactPrefix, " & _
                    '"case " & _
                    '"when strContactFirstName is null then '-' " & _
                    '"else strContactFirstName " & _
                    '"end strContactFirstName, " & _
                    '"case " & _
                    '"When strContactLastName is null then '-' " & _
                    '"else strContactLastName " & _
                    '"end strContactLastName, " & _
                    '"case " & _
                    '"when strDMUResponsibleStaff is null then '-' " & _
                    '"else strDMUResponsibleStaff " & _
                    '"end strDMUResponsibleStaff " & _
                    '"from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
                    '"airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
                    '"AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " & _
                    '"where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
                    '"and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
                    '"and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
                    '"and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
                    '"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
                    '"and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
                    '"and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
                    '"and AIRBranch.EIS_Admin.Active = '1' " & _
                    '"and AIRbranch.EIS_Admin.inventoryyear = '" & txtEISStatsEnrollmentYear.Text & "'"

                    ' dgvEISStats.Rows.Clear()
                    ' ds = New DataSet

                    ' cmd = New OracleCommand(SQL, conn)
                    ' If conn.State = ConnectionState.Closed Then
                    '     conn.Open()
                    ' End If
                    ' dr = cmd.ExecuteReader
                    ' While dr.Read
                    '     dgvRow = New DataGridViewRow
                    '     dgvRow.CreateCells(dgvEISStats)
                    '     If IsDBNull(dr.Item("ID")) Then
                    '         dgvRow.Cells(0).Value = ""
                    '     Else
                    '         dgvRow.Cells(0).Value = dr.Item("ID")
                    '     End If

                    '     If IsDBNull(dr.Item("FacilitySiteID")) Then
                    '         dgvRow.Cells(1).Value = ""
                    '     Else
                    '         dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                    '     End If
                    '     If IsDBNull(dr.Item("strFacilityName")) Then
                    '         dgvRow.Cells(2).Value = ""
                    '     Else
                    '         dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                    '     End If
                    '     If IsDBNull(dr.Item("InventoryYear")) Then
                    '         dgvRow.Cells(3).Value = ""
                    '     Else
                    '         dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                    '     End If
                    '     If IsDBNull(dr.Item("EISStatus")) Then
                    '         dgvRow.Cells(4).Value = ""
                    '     Else
                    '         dgvRow.Cells(4).Value = dr.Item("EISStatus")
                    '     End If
                    '     If IsDBNull(dr.Item("EISAccess")) Then
                    '         dgvRow.Cells(5).Value = ""
                    '     Else
                    '         dgvRow.Cells(5).Value = dr.Item("EISAccess")
                    '     End If
                    '     If IsDBNull(dr.Item("strOptOut")) Then
                    '         dgvRow.Cells(6).Value = ""
                    '     Else
                    '         dgvRow.Cells(6).Value = dr.Item("strOptOut")
                    '     End If

                    '     If IsDBNull(dr.Item("strMailout")) Then
                    '         dgvRow.Cells(7).Value = ""
                    '     Else
                    '         dgvRow.Cells(7).Value = dr.Item("strMailout")
                    '     End If
                    '     If IsDBNull(dr.Item("ContactEmail")) Then
                    '         dgvRow.Cells(8).Value = ""
                    '     Else
                    '         dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                    '     End If
                    '     If IsDBNull(dr.Item("strContactPrefix")) Then
                    '         dgvRow.Cells(9).Value = ""
                    '     Else
                    '         dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                    '     End If
                    '     If IsDBNull(dr.Item("strContactFirstName")) Then
                    '         dgvRow.Cells(10).Value = ""
                    '     Else
                    '         dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                    '     End If
                    '     If IsDBNull(dr.Item("strContactLastName")) Then
                    '         dgvRow.Cells(11).Value = ""
                    '     Else
                    '         dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                    '     End If
                    '     If IsDBNull(dr.Item("strEnrollment")) Then
                    '         dgvRow.Cells(13).Value = ""
                    '     Else
                    '         dgvRow.Cells(13).Value = dr.Item("strEnrollment")
                    '     End If


                    '     dgvEISStats.Rows.Add(dgvRow)
                    ' End While
                    ' dr.Close()

                    ' txtEISStatsCount.Text = dgvEISStats.RowCount.ToString

            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

  
    Private Sub llbEISEIUniverse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISEIUniverse.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If
            EIS_VIEW(txtSelectedEISStatYear.Text, "", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Active EIS Universe Count"

            Exit Sub

            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
            "'False' as ID, " & _
            " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
           "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
           "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
           "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
           "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
           "case " & _
           "when strOptOut = '1' then 'Yes' " & _
           "when strOptOut = '0' then 'No' " & _
           "else '' " & _
           "End strOptOut, " & _
           "case " & _
           "when strMailout = '1' then 'Yes' " & _
           "else 'No' " & _
           "end strMailout, " & _
           "case " & _
           "when EIS_Mailout.strContactEmail is null then '-' " & _
           "else EIS_Mailout.strContactEmail " & _
           "end ContactEmail, " & _
             "case " & _
                   "When EIS_Mailout.strContactPrefix is null then '-' " & _
                   "else EIS_Mailout.strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when EIS_Mailout.strContactFirstName is null then '-' " & _
                   "else EIS_Mailout.strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When EIS_Mailout.strContactLastName is null then '-' " & _
                   "else EIS_Mailout.strContactLastName " & _
                   "end strContactLastName, " & _
            "case " & _
            "when strDMUResponsibleStaff is null then '-' " & _
            "else strDMUResponsibleStaff " & _
            "end strDMUResponsibleStaff, " & _
            "case when APBContactInformation.strContactEmail is null and strKey = '41' then '-' " & _
                  "else APBContactInformation.strContactEmail  end IAIPContactEmail, " & _
            "case When APBContactInformation.strContactPrefix is null and strKey = '41' then '-' " & _
                  "else APBContactInformation.strContactPrefix   end IAIPContactPrefix, " & _
            "case when APBContactInformation.strContactFirstName is null and strKey = '41' " & _
                "then '-' else APBContactInformation.strContactFirstName  end IAIPContactFirstName, " & _
            "case When APBContactInformation.strContactLastName is null and strKey = '41' " & _
                "then '-' else APBContactInformation.strContactLastName  end IAIPContactLastName " & _
           "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
           "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
           "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin,  " & _
           "airbranch.APBContactInformation " & _
           "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode (+) " & _
           "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode (+) " & _
           "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
            "and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
           "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
           "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear  (+) " & _
           "and  '0413'||AIRBranch.EIS_Admin.FacilitySiteID||'41' =  airbranch.APBContactInformation.strContactkey (+) " & _
            "and AIRBranch.EIS_Admin.Active = '1' " & _
            "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If

                If IsDBNull(dr.Item("IAIPContactPrefix")) Then
                    dgvRow.Cells(16).Value = ""
                Else
                    dgvRow.Cells(16).Value = dr.Item("IAIPContactPrefix")
                End If
                If IsDBNull(dr.Item("IAIPContactFirstname")) Then
                    dgvRow.Cells(17).Value = ""
                Else
                    dgvRow.Cells(17).Value = dr.Item("IAIPContactFirstname")
                End If
                If IsDBNull(dr.Item("IAIPContactLastName")) Then
                    dgvRow.Cells(18).Value = ""
                Else
                    dgvRow.Cells(18).Value = dr.Item("IAIPContactLastName")
                End If
                If IsDBNull(dr.Item("IAIPContactEmail")) Then
                    dgvRow.Cells(19).Value = ""
                Else
                    dgvRow.Cells(19).Value = dr.Item("IAIPContactEmail")
                End If

                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()


            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Active EIS Universe Count"


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISMailOutTotal_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISMailOutTotal.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If
            EIS_VIEW(txtSelectedEISStatYear.Text, "1", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Mailout Total Count"



            Exit Sub


            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
            "'False' as ID, " & _
            " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
           "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
           "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
           "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
           "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
           "case " & _
           "when strOptOut = '1' then 'Yes' " & _
           "when strOptOut = '0' then 'No' " & _
           "else '' " & _
           "End strOptOut, " & _
           "case " & _
           "when strMailout = '1' then 'Yes' " & _
           "else 'No' " & _
           "end strMailout, " & _
           "case " & _
           "when EIS_Mailout.strContactEmail is null then '-' " & _
           "else EIS_Mailout.strContactEmail " & _
           "end ContactEmail, " & _
             "case " & _
                   "When EIS_Mailout.strContactPrefix is null then '-' " & _
                   "else EIS_Mailout.strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when EIS_Mailout.strContactFirstName is null then '-' " & _
                   "else EIS_Mailout.strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When EIS_Mailout.strContactLastName is null then '-' " & _
                   "else EIS_Mailout.strContactLastName " & _
                   "end strContactLastName, " & _
           "case " & _
          "when strDMUResponsibleStaff is null then '-' " & _
           "else strDMUResponsibleStaff " & _
            "end strDMUResponsibleStaff, " & _
            "case when AIRBranch.APBContactInformation.strContactEmail is null and strKey = '41' then '-' else AIRBranch.APBContactInformation.strContactEmail  end IAIPContactEmail, " & _
"case When AIRBranch.APBContactInformation.strContactPrefix is null and strKey = '41' then '-' else AIRBranch.APBContactInformation.strContactPrefix   end IAIPContactPrefix, " & _
"case when AIRBranch.APBContactInformation.strContactFirstName is null and strKey = '41' then '-' else AIRBranch.APBContactInformation.strContactFirstName  end IAIPContactFirstName, " & _
"case When AIRBranch.APBContactInformation.strContactLastName is null and strKey = '41' then '-' else AIRBranch.APBContactInformation.strContactLastName   end IAIPContactLastName " & _
           "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
           "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
           "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin,  " & _
           "AIRBranch.APBContactInformation " & _
           "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
           "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
           "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
           "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
               "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
               "and  '0413'||AIRBranch.EIS_Admin.FacilitySiteID||'41' =  airbranch.APBContactInformation.strContactkey (+) " & _
            "and AIRBranch.EIS_Admin.Active = '1' " & _
           "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
           "and strMailout = '1' " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If


                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                If IsDBNull(dr.Item("IAIPContactPrefix")) Then
                    dgvRow.Cells(16).Value = ""
                Else
                    dgvRow.Cells(16).Value = dr.Item("IAIPContactPrefix")
                End If
                If IsDBNull(dr.Item("IAIPContactFirstname")) Then
                    dgvRow.Cells(17).Value = ""
                Else
                    dgvRow.Cells(17).Value = dr.Item("IAIPContactFirstname")
                End If
                If IsDBNull(dr.Item("IAIPContactLastName")) Then
                    dgvRow.Cells(18).Value = ""
                Else
                    dgvRow.Cells(18).Value = dr.Item("IAIPContactLastName")
                End If
                If IsDBNull(dr.Item("IAIPContactEmail")) Then
                    dgvRow.Cells(19).Value = ""
                Else
                    dgvRow.Cells(19).Value = dr.Item("IAIPContactEmail")
                End If

                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Mailout Total Count"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISEnrolled_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISEnrolled.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Enrolled Count"

            Exit Sub

           
            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
       "'False' as ID, " & _
       " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
      "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
      "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
      "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
      "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
      "case " & _
      "when strOptOut = '1' then 'Yes' " & _
      "when strOptOut = '0' then 'No' " & _
      "else '' " & _
      "End strOptOut, " & _
      "case " & _
      "when strMailout = '1' then 'Yes' " & _
      "else 'No' " & _
      "end strMailout, " & _
      "case " & _
      "when strContactEmail is null then '-' " & _
      "else strContactEmail " & _
      "end ContactEmail, " & _
        "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
      "case " & _
        "when strDMUResponsibleStaff is null then '-' " & _
        "else strDMUResponsibleStaff " & _
        "end strDMUResponsibleStaff  " & _
      "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
      "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
      "AIRbranch.EIS_Mailout, AIRBranch.EIS_QAAdmin " & _
      "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
      "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
      "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
      "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
      "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
        "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
       "and AIRBranch.EIS_Admin.Active = '1' " & _
      "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
             "and strEnrollment = '1' " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If
                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Enrolled Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISNoActivity_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISNoActivity.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If
            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "Null", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "No Activity Count"

            Exit Sub

            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
        "'False' as ID, " & _
        " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
              "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
              "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
              "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
              "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
              "case " & _
              "when strOptOut = '1' then 'Yes' " & _
              "when strOptOut = '0' then 'No' " & _
              "else '' " & _
              "End strOptOut, " & _
              "case " & _
              "when strMailout = '1' then 'Yes' " & _
              "else 'No' " & _
              "end strMailout, " & _
              "case " & _
              "when strContactEmail is null then '-' " & _
              "else strContactEmail " & _
              "end ContactEmail, " & _
                "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
              "case " & _
"when strDMUResponsibleStaff is null then '-' " & _
"else strDMUResponsibleStaff " & _
"end strDMUResponsibleStaff  " & _
              "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
              "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
              "AIRbranch.EIS_Mailout, AIRBranch.EIS_QAAdmin " & _
              "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
              "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
              "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
              "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
                 "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
              "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
              "" & _
              "and AIRBranch.EIS_Admin.Active = '1' " & _
              "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
              "and strOptOut is null " & _
               "and strEnrollment = '1' " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "


            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If


                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "No Activity Count"

        Catch ex As Exception

        End Try
    End Sub
    Private Sub llbEISUnenrolled_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISUnenrolled.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If
            EIS_VIEW(txtSelectedEISStatYear.Text, "", "0", "1", "", "", "", "")
            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Unenrolled Count"


            Exit Sub

            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
             "'False' as ID, " & _
             " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
            "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
            "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
            "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
            "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
            "case " & _
            "when strOptOut = '1' then 'Yes' " & _
            "when strOptOut = '0' then 'No' " & _
            "else '' " & _
            "End strOptOut, " & _
            "case " & _
            "when strMailout = '1' then 'Yes' " & _
            "else 'No' " & _
            "end strMailout, " & _
            "case " & _
            "when strContactEmail is null then '-' " & _
            "else strContactEmail " & _
            "end ContactEmail, " & _
              "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
            "case " & _
            "when strDMUResponsibleStaff is null then '-' " & _
            "else strDMUResponsibleStaff " & _
            "end strDMUResponsibleStaff  " & _
            "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
            "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
            "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " & _
            "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
            "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
            "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
            "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
            "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
              "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
             "and AIRBranch.EIS_Admin.Active = '1' " & _
            "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
              "and strMailout = '1' " & _
              "and (strEnrollment = '0') " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If


                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()


            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Unenrolled Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISInProgress_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISInProgress.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", " and EISStatusCode = 2 ", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "In Progress Count"

            Exit Sub





            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
       "'False' as ID, " & _
       " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
       "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
        "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
        "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
        "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
        "case " & _
        "when strOptOut = '1' then 'Yes' " & _
        "when strOptOut = '0' then 'No' " & _
        "else '' " & _
        "End strOptOut, " & _
        "case " & _
        "when strMailout = '1' then 'Yes' " & _
        "else 'No' " & _
        "end strMailout, " & _
        "case " & _
        "when strContactEmail is null then '-' " & _
        "else strContactEmail " & _
        "end ContactEmail, " & _
          "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
        "case " & _
        "when strDMUResponsibleStaff is null then '-' " & _
        "else strDMUResponsibleStaff " & _
        "end strDMUResponsibleStaff  " & _
        "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
        "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
        "AIRbranch.EIS_Mailout, AIRBranch.EIS_QAAdmin " & _
        "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
             "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
             "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
             "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
                 "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
                    "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
             "and AIRBranch.EIS_Admin.Active = '1' " & _
             "and airbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
             "and airbranch.EIS_Admin.strEnrollment = '1' " & _
             "and airbranch.EIS_Admin.eisStatuscode = '2' " & _
             "and (strOptOut = '0' )" & _
              "and strEnrollment = '1' " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "



            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If
                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()


            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "In Progress Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISOptedIn_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISOptedIn.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Opted-In Count"

            Exit Sub

            'Dim dgvRow As New DataGridViewRow
 

            'added contact email and name
            SQL = "select distinct " & _
       "'False' as ID, " & _
       " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
       "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
            "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
            "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
            "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
            "case " & _
            "when strOptOut = '1' then 'Yes' " & _
            "when strOptOut = '0' then 'No' " & _
            "else '' " & _
            "End strOptOut, " & _
            "case " & _
            "when strMailout = '1' then 'Yes' " & _
            "else 'No' " & _
            "end strMailout, " & _
            "case " & _
            "when strContactEmail is null then '-' " & _
            "else strContactEmail " & _
            "end ContactEmail, " & _
              "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
            "case " & _
            "when strDMUResponsibleStaff is null then '-' " & _
            "else strDMUResponsibleStaff " & _
            "end strDMUResponsibleStaff  " & _
            "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
            "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
            "AIRbranch.EIS_Mailout, AIRBranch.EIS_QAAdmin " & _
            "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
            "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
            "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
            "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
            "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
            "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
              "and AIRBranch.EIS_Admin.Active = '1' " & _
             "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
             "and (strOptOut = '0')  " & _
              "and strEnrollment = '1' " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "


            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If


                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Opted-In Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISOptedOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISOptedOut.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "1", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Opted-Out Count"

            Exit Sub

            'Dim dgvRow As New DataGridViewRow

            'added contact email and name
            SQL = "select distinct " & _
          "'False' as ID, " & _
          " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
               "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
               "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
               "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
               "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
                          "case " & _
               "when strOptOutreason is null then 'No' " & _
               "when strOptoutReason = '1' then 'Did not Operate' " & _
               "when strOptOutReason = '2' then 'Pollutant below Threshold' " & _
               "end strOptOut, " & _
               "case " & _
               "when strMailout = '1' then 'Yes' " & _
               "else 'No' " & _
               "end strMailout, " & _
               "case " & _
               "when strContactEmail is null then '-' " & _
               "else strContactEmail " & _
               "end ContactEmail, " & _
                 "case " & _
                     "When strContactPrefix is null then '-' " & _
                     "else strContactPrefix " & _
                     "end strContactPrefix, " & _
                     "case " & _
                     "when strContactFirstName is null then '-' " & _
                     "else strContactFirstName " & _
                     "end strContactFirstName, " & _
                     "case " & _
                     "When strContactLastName is null then '-' " & _
                     "else strContactLastName " & _
                     "end strContactLastName, " & _
               "case " & _
  "when strDMUResponsibleStaff is null then '-' " & _
  "else strDMUResponsibleStaff " & _
  "end strDMUResponsibleStaff  " & _
               "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
               "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
               "AIRbranch.EIS_Mailout, AIRBranch.EIS_QAAdmin " & _
               "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
               "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
               "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
               "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
  "and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
               "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
                  "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
                "and AIRBranch.EIS_Admin.Active = '1' " & _
                "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
                "and strOptOut = '1'  " & _
               "and strEnrollment = '1' " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "


            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = False
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If


                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()


            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Opted-Out Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISSubmitted_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISSubmitted.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", " and EISStatusCode >= 3 ", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "In Progress Count"

            Exit Sub
 



            'Dim dgvRow As New DataGridViewRow

         
            SQL = "select distinct " & _
       "'False' as ID, " & _
       " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
         "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
    "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
    "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
    "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
    "case " & _
    "when strOptOut = '1' then 'Yes' " & _
    "when strOptOut = '0' then 'No' " & _
    "else '' " & _
    "End strOptOut, " & _
    "case " & _
    "when strMailout = '1' then 'Yes' " & _
    "else 'No' " & _
    "end strMailout, " & _
    "case " & _
    "when strContactEmail is null then '-' " & _
    "else strContactEmail " & _
    "end ContactEmail, " & _
      "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
    "case " & _
        "when strDMUResponsibleStaff is null then '-' " & _
        "else strDMUResponsibleStaff " & _
        "end strDMUResponsibleStaff  " & _
    "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
    "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
    "AIRbranch.EIS_Mailout, AIRBranch.EIS_QAAdmin " & _
    "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
    "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
    "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
    "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
    "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
       "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
     "and AIRBranch.EIS_Admin.Active = '1' " & _
    "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
             "and strEnrollment = '1' " & _
             "and AIRBranch.EIS_Admin.eisstatuscode >= 3 " & _
             "and (strOptOut = '0')  " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If
                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Submitted Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISFinalized_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISFinalized.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "", _
                     " and (EISStatusCode = '3' or EISStatusCode = '4' or EISStatusCode = '5') ", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Finalized Count"

            Exit Sub

            'Dim dgvRow As New DataGridViewRow
 
            SQL = "select distinct " & _
      "'False' as ID, " & _
      " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
     "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
"" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
"AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
"AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
      "case " & _
            "when strOptOutreason is null then 'No' " & _
            "when strOptoutReason = '1' then 'Did not Operate' " & _
            "when strOptOutReason = '2' then 'Pollutant below Threshold' " & _
            "end strOptOut, " & _
"case " & _
"when strMailout = '1' then 'Yes' " & _
"else 'No' " & _
"end strMailout, " & _
"case " & _
"when strContactEmail is null then '-' " & _
"else strContactEmail " & _
"end ContactEmail, " & _
 "case " & _
                  "When strContactPrefix is null then '-' " & _
                  "else strContactPrefix " & _
                  "end strContactPrefix, " & _
                  "case " & _
                  "when strContactFirstName is null then '-' " & _
                  "else strContactFirstName " & _
                  "end strContactFirstName, " & _
                  "case " & _
                  "When strContactLastName is null then '-' " & _
                  "else strContactLastName " & _
                  "end strContactLastName, " & _
"case " & _
"when strDMUResponsibleStaff is null then '-' " & _
"else strDMUResponsibleStaff " & _
"end strDMUResponsibleStaff  " & _
"from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
"airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
"AIRbranch.EIS_Mailout, AIRBranch.EIS_QAAdmin " & _
"where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
"and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
"and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
"and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
"and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
  "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
"and AIRBranch.EIS_Admin.Active = '1' " & _
"and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
             "and strEnrollment = '1' " & _
           "and (AIRBranch.EIS_Admin.eisstatuscode = '3' " & _
            "or AIRBranch.EIS_Admin.eisstatuscode = '4' or AIRBranch.EIS_Admin.eisstatuscode = '5') " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "Finalized Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISQABegan_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISQABegan.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "1", "1", "1", " and (strOptOut is null or strOptout = '0') ", "4", "2", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "In Progress Count"

            Exit Sub

            'Dim dgvRow As New DataGridViewRow

            SQL = "select " & _
      "'False' as ID, " & _
      " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
     "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
"" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
"AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
"AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
"case " & _
"when strOptOut = '1' then 'Yes' " & _
"when strOptOut = '0' then 'No' " & _
"else '' " & _
"End strOptOut, " & _
"case " & _
"when strMailout = '1' then 'Yes' " & _
"else 'No' " & _
"end strMailout, " & _
"case " & _
"when strContactEmail is null then '-' " & _
"else strContactEmail " & _
"end ContactEmail, " & _
  "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
"case " & _
"when strDMUResponsibleStaff is null then '-' " & _
"else strDMUResponsibleStaff " & _
"end strDMUResponsibleStaff  " & _
"from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
"airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
"AIRbranch.EIS_Mailout, AIRBranch.EIS_QAAdmin " & _
"where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
"and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
"and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
"and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
"and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.Active = '1' " & _
"and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
           "and strMailout = '1' " & _
            "and strEnrollment = '1' " & _
            "and  AIRBranch.EIS_Admin.EISAccesscode = '2'  " & _
            "and AIRBranch.EIS_Admin.eisstatuscode = '4'" & _
            "and (strOptOut = '0' and strOptout is null)  "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If
                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()


            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISSubmittedToEPA_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISSubmittedToEPA.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", _
                 " and EISStatusCode >= 3 ", "", " and datQAComplete is not null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, EPA Submitted Count"

            Exit Sub

            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
      "'False' as ID, " & _
      " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
     "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
"" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
"AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
"AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
"case " & _
"when strOptOut = '1' then 'Yes' " & _
"when strOptOut = '0' then 'No' " & _
"else '' " & _
"End strOptOut, " & _
"case " & _
"when strMailout = '1' then 'Yes' " & _
"else 'No' " & _
"end strMailout, " & _
"case " & _
"when strContactEmail is null then '-' " & _
"else strContactEmail " & _
"end ContactEmail, " & _
  "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
"case " & _
"when strDMUResponsibleStaff is null then '-' " & _
"else strDMUResponsibleStaff " & _
"end strDMUResponsibleStaff,  " & _
"AIRBranch.EISLK_QAStatus.strDesc,  " & _
            "datQAStatus " & _
"from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
"airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
"AIRbranch.EIS_Mailout, AIRBranch.EIS_QAAdmin, AIRbranch.EISLK_QAStatus  " & _
"where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
"and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
"and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
"and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
"and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
           "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
"and AIRBranch.EIS_QAAdmin.QAStatusCode = AIRBranch.EISLK_QAStatus.qastatuscode " & _
"and AIRBranch.EIS_Admin.Active = '1' " & _
"and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
            "and strEnrollment = '1' " & _
          "and AIRbranch.EIS_Admin.eisstatuscode >= 3  " & _
 "and (strOptOut = '0' ) " & _
 "and    exists (Select * from AIRBranch.EIS_QAAdmin " & _
 "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
 "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
 "and datQAComplete is not null )  " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If
                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If

                If IsDBNull(dr.Item("strDesc")) Then
                    dgvRow.Cells(14).Value = ""
                Else
                    dgvRow.Cells(14).Value = dr.Item("strDesc")
                End If
                If IsDBNull(dr.Item("datQAStatus")) Then
                    dgvRow.Cells(15).Value = ""
                Else
                    dgvRow.Cells(15).Value = dr.Item("datQAStatus")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, EPA Submitted Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnEISSummaryToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvEISStats.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvEISStats.ColumnCount - 1
                        If IsDBNull(dgvEISStats.Columns(i).HeaderText.ToString) Then
                            .Cells(1, i + 1) = "No Header"
                        Else
                            .Cells(1, i + 1) = dgvEISStats.Columns(i).HeaderText.ToString
                        End If
                    Next

                    For i = 0 To dgvEISStats.ColumnCount - 1
                        For j = 0 To dgvEISStats.RowCount - 1
                            If IsDBNull(dgvEISStats.Item(i, j).Value.ToString) Then
                                .Cells(j + 2, i + 1).numberformat = "@"
                                .Cells(j + 2, i + 1).value = "  "
                            Else
                                .Cells(j + 2, i + 1).numberformat = "@"
                                .Cells(j + 2, i + 1).value = dgvEISStats.Item(i, j).Value.ToString
                            End If

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
        End Try
    End Sub

    Private Sub dgvEISStats_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvEISStats.MouseUp
        Try
            Dim CurrentTabPage As TabPage = TCEISStats.SelectedTab
            Dim hti As DataGridView.HitTestInfo = dgvEISStats.HitTest(e.X, e.Y)
            Dim i As Integer = 0

            If hti.RowIndex = -1 And hti.ColumnIndex <> -1 Then
                If dgvEISStats.Columns(hti.ColumnIndex).HeaderText = " " Then
                    If dgvEISStats(0, 0).Value = True Then
                        For i = 0 To dgvEISStats.Rows.Count - 1
                            dgvEISStats(0, i).Value = False
                        Next
                    Else
                        For i = 0 To dgvEISStats.Rows.Count - 1
                            dgvEISStats(0, i).Value = True
                        Next
                    End If
                End If
            Else
                If hti.RowIndex <> -1 Then
                    mtbEISLogAIRSNumber.Text = dgvEISStats(1, hti.RowIndex).Value
                End If
            End If

            If CurrentTabPage.Name.ToString = "TPEISStatMailout" Then
                If dgvEISStats.RowCount > 0 And hti.RowIndex <> -1 Then
                    dgvEISStats.Enabled = False

                    txtEISStatsMailoutFacilityName.Clear()
                    txtEISStatsMailoutPrefix.Clear()
                    txtEISStatsMailoutFirstName.Clear()
                    txtEISStatsMailoutLastName.Clear()
                    txtEISStatsMailoutCompanyName.Clear()
                    txtEISStatsMailoutAddress1.Clear()
                    txtEISStatsMailoutAddress2.Clear()
                    txtEISStatsMailoutCity.Clear()
                    txtEISStatsMailoutState.Clear()
                    txtEISStatsMailoutZipCode.Clear()
                    txtEISStatsMailoutEmailAddress.Clear()
                    txtEISStatsMailoutComments.Clear()
                    txtEISStatsMailoutUpdateUser.Clear()
                    txtEISStatsMailoutUpdateDate.Clear()
                    txtEISStatsMailoutCreateDate.Clear()

                    If IsDBNull(dgvEISStats(1, hti.RowIndex).Value) Then
                        txtEISStatsMailoutAIRSNumber.Clear()
                    Else
                        txtEISStatsMailoutAIRSNumber.Text = dgvEISStats(1, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvEISStats(3, hti.RowIndex).Value) Then
                        txtSelectedEISMailout.Clear()
                    Else
                        txtSelectedEISMailout.Text = dgvEISStats(3, hti.RowIndex).Value
                    End If


                    SQL = "Select " & _
                    "strFacilityName, " & _
                    "strContactCompanyName, strContactAddress1, " & _
                    "strContactAddress2, strContactCity, " & _
                    "strcontactstate, strcontactzipCode, " & _
                    "strcontactFirstName, strcontactLastName, " & _
                    "strContactPrefix, strContactEmail, " & _
                    "stroperationalStatus, strClass, " & _
                    "strcomment, UpdateUser, " & _
                    "updateDateTime, CreateDateTime " & _
                     "from AIRBranch.EIS_Mailout " & _
                     "where intInventoryyear = '" & txtSelectedEISMailout.Text & "' " & _
                     "and FacilitySiteID = '" & txtEISStatsMailoutAIRSNumber.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            txtEISStatsMailoutFacilityName.Clear()
                        Else
                            txtEISStatsMailoutFacilityName.Text = dr.Item("strFacilityName")
                        End If
                        If IsDBNull(dr.Item("strContactCompanyName")) Then
                            txtEISStatsMailoutCompanyName.Clear()
                        Else
                            txtEISStatsMailoutCompanyName.Text = dr.Item("strContactCompanyName")
                        End If
                        If IsDBNull(dr.Item("strContactAddress1")) Then
                            txtEISStatsMailoutAddress1.Clear()
                        Else
                            txtEISStatsMailoutAddress1.Text = dr.Item("strContactAddress1")
                        End If
                        If IsDBNull(dr.Item("strContactAddress2")) Then
                            txtEISStatsMailoutAddress2.Clear()
                        Else
                            txtEISStatsMailoutAddress2.Text = dr.Item("strContactAddress2")
                        End If
                        If IsDBNull(dr.Item("strContactCity")) Then
                            txtEISStatsMailoutCity.Clear()
                        Else
                            txtEISStatsMailoutCity.Text = dr.Item("strContactCity")
                        End If
                        If IsDBNull(dr.Item("strcontactstate")) Then
                            txtEISStatsMailoutState.Clear()
                        Else
                            txtEISStatsMailoutState.Text = dr.Item("strcontactstate")
                        End If
                        If IsDBNull(dr.Item("strcontactzipCode")) Then
                            txtEISStatsMailoutZipCode.Clear()
                        Else
                            txtEISStatsMailoutZipCode.Text = dr.Item("strcontactzipCode")
                        End If
                        If IsDBNull(dr.Item("strcontactFirstName")) Then
                            txtEISStatsMailoutFirstName.Clear()
                        Else
                            txtEISStatsMailoutFirstName.Text = dr.Item("strcontactFirstName")
                        End If
                        If IsDBNull(dr.Item("strcontactLastName")) Then
                            txtEISStatsMailoutLastName.Clear()
                        Else
                            txtEISStatsMailoutLastName.Text = dr.Item("strcontactLastName")
                        End If
                        If IsDBNull(dr.Item("strContactPrefix")) Then
                            txtEISStatsMailoutPrefix.Clear()
                        Else
                            txtEISStatsMailoutPrefix.Text = dr.Item("strContactPrefix")
                        End If
                        If IsDBNull(dr.Item("strContactEmail")) Then
                            txtEISStatsMailoutEmailAddress.Clear()
                        Else
                            txtEISStatsMailoutEmailAddress.Text = dr.Item("strContactEmail")
                        End If
                        If IsDBNull(dr.Item("strcomment")) Then
                            txtEISStatsMailoutComments.Clear()
                        Else
                            txtEISStatsMailoutComments.Text = dr.Item("strcomment")
                        End If
                        If IsDBNull(dr.Item("UpdateUser")) Then
                            txtEISStatsMailoutUpdateUser.Clear()
                        Else
                            txtEISStatsMailoutUpdateUser.Text = dr.Item("UpdateUser")
                        End If
                        If IsDBNull(dr.Item("updateDateTime")) Then
                            txtEISStatsMailoutUpdateDate.Clear()
                        Else
                            txtEISStatsMailoutUpdateDate.Text = dr.Item("updateDateTime")
                        End If
                        If IsDBNull(dr.Item("CreateDateTime")) Then
                            txtEISStatsMailoutCreateDate.Clear()
                        Else
                            txtEISStatsMailoutCreateDate.Text = dr.Item("CreateDateTime")
                        End If

                    End While
                    dr.Close()

                End If
            End If
            dgvEISStats.Enabled = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveEISStatMailout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveEISStatMailout.Click
        Try
            If txtSelectedEISMailout.Text <> "" And txtEISStatsMailoutAIRSNumber.Text <> "" Then
                SQL = "UPdate " & DBNameSpace & ".EIS_Mailout set " & _
                "strFacilityName = '" & txtEISStatsMailoutFacilityName.Text & "', " & _
                "strContactCompanyName = '" & txtEISStatsMailoutCompanyName.Text & "', " & _
                "strContactAddress1 = '" & txtEISStatsMailoutAddress1.Text & "', " & _
                "strContactAddress2 = '" & txtEISStatsMailoutAddress2.Text & "', " & _
                "strContactCity = '" & txtEISStatsMailoutCity.Text & "', " & _
                "strContactState = '" & txtEISStatsMailoutState.Text & "', " & _
                "strContactZipCode = '" & txtEISStatsMailoutZipCode.Text & "', " & _
                "strContactFirstName = '" & txtEISStatsMailoutFirstName.Text & "', " & _
                "strContactLastName = '" & txtEISStatsMailoutLastName.Text & "', " & _
                "strContactPrefix = '" & txtEISStatsMailoutPrefix.Text & "', " & _
                "strContactEmail = '" & txtEISStatsMailoutEmailAddress.Text & "', " & _
                "strComment = '" & txtEISStatsMailoutComments.Text & "', " & _
                "updateDateTime = sysdate " & _
                "where intInventoryYear = '" & txtSelectedEISStatYear.Text & "' " & _
                "and FacilitySiteID = '" & txtEISStatsMailoutAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

                MsgBox("Data updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Please select a valid year from the dropdown and a valid contact from the resulting list." & vbCrLf & _
                       "NO DATA UPDATED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISStatsDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISStatsDelete.Click
        Try
            If txtSelectedEISMailout.Text <> "" And txtEISStatsMailoutAIRSNumber.Text <> "" Then
                'SQL = "UPdate " & DBNameSpace & ".EIS_Mailout set " & _
                '"strFacilityName = '" & txtEISStatsMailoutFacilityName.Text & "', " & _
                '"strContactCompanyName = '" & txtEISStatsMailoutCompanyName.Text & "', " & _
                '"strContactAddress1 = '" & txtEISStatsMailoutAddress1.Text & "', " & _
                '"strContactAddress2 = '" & txtEISStatsMailoutAddress2.Text & "', " & _
                '"strContactCity = '" & txtEISStatsMailoutCity.Text & "', " & _
                '"strContactState = '" & txtEISStatsMailoutState.Text & "', " & _
                '"strContactZipCode = '" & txtEISStatsMailoutZipCode.Text & "', " & _
                '"strContactFirstName = '" & txtEISStatsMailoutFirstName.Text & "', " & _
                '"strContactLastName = '" & txtEISStatsMailoutLastName.Text & "', " & _
                '"strContactPrefix = '" & txtEISStatsMailoutPrefix.Text & "', " & _
                '"strContactEmail = '" & txtEISStatsMailoutEmailAddress.Text & "', " & _
                '"strComment = '" & txtEISStatsMailoutComments.Text & "', " & _
                '"updateDateTime = sysdate " & _
                '"where intInventoryYear = '" & txtSelectedEISStatYear.Text & "' " & _
                '"and FacilitySiteID = '" & txtEISStatsMailoutAIRSNumber.Text & "' "

                'cmd = New OracleCommand(SQL, conn)
                'If conn.State = ConnectionState.Closed Then
                '    conn.Open()
                'End If
                'cmd.ExecuteNonQuery()

                MsgBox("DELETE CODE NOT HERE", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Please select a valid year from the dropdown and a valid contact from the resulting list." & vbCrLf & _
                       "NO DATA UPDATED", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

  
    Private Sub btnEISStatsEnrollment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISStatsEnrollment.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to enroll Facilities into the QA process.", Me.Text)

            If EISConfirm = txtEISStatsEnrollmentYear.Text Then
                temp = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next
                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "
                    SQL = "Update " & DBNameSpace & ".EIS_Admin set " & _
                    "strEnrollment = '1', " & _
                    "EISAccessCode = '1', " & _
                    "EISStatusCode = '1', " & _
                    "DatEISStatus = sysdate " & _
                    "where inventoryyear = '" & EISConfirm & "' " & _
                    "and strEnrollment = '0' " & _
                    "and strOptOut is null " & _
                    "and EISAccessCode = '0' " & _
                    "and EISStatusCode = '0' " & _
                    "and strMailout = '1' " & _
                    temp

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteReader()
                End If

                MsgBox("Facilities enrolled in " & EISConfirm & " EIS.", MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("Year does not match selected EIS year")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsViewEnrollment_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsViewEnrollment.LinkClicked
        Try
            txtEISStatsEnrollmentYear.Text = cboEISStatisticsYear.Text

            If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim dgvRow As New DataGridViewRow

            SQL = "select " & _
            "'False' as ID, " & _
            " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
           "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
           "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
           "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
           "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
           "case " & _
           "when strOptOut = '1' then 'Yes' " & _
           "when strOptOut = '0' then 'No' " & _
           "else '-' " & _
           "End strOptOut, " & _
           "case " & _
           "when strMailout = '1' then 'Yes' " & _
           "else 'No' " & _
           "end strMailout, " & _
           "case " & _
           "when strEnrollment = '1' then 'Yes' " & _
           "when strEnrollment = '0' then 'No' " & _
           "else '-' " & _
           "end strEnrollment, " & _
           "case " & _
           "when strContactEmail is null then '-' " & _
           "else strContactEmail " & _
           "end ContactEmail, " & _
           "case " & _
           "When strContactPrefix is null then '-' " & _
           "else strContactPrefix " & _
           "end strContactPrefix, " & _
           "case " & _
           "when strContactFirstName is null then '-' " & _
           "else strContactFirstName " & _
           "end strContactFirstName, " & _
           "case " & _
           "When strContactLastName is null then '-' " & _
           "else strContactLastName " & _
           "end strContactLastName, " & _
           "case " & _
           "when strDMUResponsibleStaff is null then '-' " & _
           "else strDMUResponsibleStaff " & _
           "end strDMUResponsibleStaff " & _
           "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
           "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
           "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " & _
           "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
           "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
           "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
           "and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
           "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
           "and AIRBranch.EIS_Admin.Active = '1' " & _
           "and AIRbranch.EIS_Admin.inventoryyear = '" & txtEISStatsEnrollmentYear.Text & "'" & _
           "and strEnrollment = '1' "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If
                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If

                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strEnrollment")) Then
                    dgvRow.Cells(13).Value = ""
                Else
                    dgvRow.Cells(13).Value = dr.Item("strEnrollment")
                End If


                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISStatsRemoveEnrollment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISStatsRemoveEnrollment.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to enroll Facilities into the QA process.", Me.Text)

            If EISConfirm = txtEISStatsEnrollmentYear.Text Then
                temp = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next
                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "
                    SQL = "Update " & DBNameSpace & ".EIS_Admin set " & _
                    "strEnrollment = '0', " & _
                    "EISAccessCode = '1', " & _
                    "EISStatusCode = '1', " & _
                    "DatEISStatus = sysdate " & _
                    "where inventoryyear = '" & EISConfirm & "' " & _
                    "and strEnrollment = '1' " & _
                    "and strOptOut is null " & _
                    "and EISAccessCode = '0' " & _
                    "and EISStatusCode = '0' " & _
                    "and strMailout = '1' " & _
                    temp

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteReader()
                End If

                MsgBox("Facilities enrolled in " & EISConfirm & " EIS.", MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("Year does not match selected EIS year")
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCloseOutEIS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseOutEIS.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to close out.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                temp = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                Next
                temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "

                SQL = "Update AIRBranch.EIS_Admin set " & _
                "EISAccessCode = '2' " & _
                "where inventoryYear = '" & EISConfirm & "' " & _
                temp

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()
                ViewEISStats()
                MsgBox(EISConfirm & " Emission Inventory Year Closed out.", MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("Year does not match selected EIS year")

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISBeginQA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISBeginQA.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to move Facilities into the QA process.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                temp = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True And dgvEISStats(6, i).Value = "No" Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next
                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "

                    SQL = "Update AIRBranch.EIS_Admin set " & _
                    "EISAccessCode = '2', " & _
                    "EISStatusCode = '4', " & _
                    "datEISstatus = sysdate, " & _
                    "UpdateUser = '" & Replace(UserName, "'", "''") & "', " & _
                    "updatedatetime = sysdate " & _
                    "where strOptOut = '0' " & _
                    "and inventoryYear = '" & EISConfirm & "' " & _
                    temp

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteReader(CommandBehavior.CloseConnection)
                End If

                temp = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    'temp = dgvEISStats(6, i).Value

                    If dgvEISStats(0, i).Value = True And dgvEISStats(6, i).Value = "Yes" Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next
                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "

                    SQL = "Update AIRBranch.EIS_Admin set " & _
                    "EISAccessCode = '2', " & _
                    "EISStatusCode = '5', " & _
                    "datEISstatus = sysdate, " & _
                    "UpdateUser = '" & Replace(UserName, "'", "''") & "', " & _
                    "updatedatetime = sysdate " & _
                    "where strOptOut = '1' " & _
                    "and inventoryYear = '" & EISConfirm & "' " & _
                    temp

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteReader(CommandBehavior.CloseConnection)
                End If

                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True Then
                        SQL = "insert into AIRBranch.EIS_QAAdmin " & _
                        "(select " & _
                        "'" & EISConfirm & "', '" & dgvEISStats(1, i).Value & "', " & _
                        "sysdate, '', " & _
                        "'1', sysdate, " & _
                        "'" & UserName & "', " & _
                        "'', '', " & _
                        "'1', '" & UserName & "', " & _
                        "sysdate, sysdate, " & _
                        "'', '', '', '' " & _
                        "from dual " & _
                        "where not exists (select * from AIRBranch.EIS_QAAdmin " & _
                        "where inventoryYear = '" & EISConfirm & "' " & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "') " & _
                        "and exists (select * from AIRBranch.EIS_Admin " & _
                        "where inventoryYear = '" & EISConfirm & "'  " & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' " & _
                        "and strOptOut = '0' )) "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd = New OracleCommand("AIRBranch.PD_EIS_QASTART", CurrentConnection)
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.Add(New OracleParameter("AIRSNUMBER_IN", OracleDbType.Varchar2)).Value = dgvEISStats(1, i).Value
                        cmd.Parameters.Add(New OracleParameter("INTYEAR_IN", OracleDbType.Decimal)).Value = EISConfirm

                        cmd.ExecuteNonQuery()




                        'SQL = "update AIRBranch.EIS_Process set " & _
                        '"intLastEmissionsYear = '" & EISConfirm & "' " & _
                        '"where exists " & _
                        '"(select * from airbranch.EIS_ProcessReportingPeriod " & _
                        '"where airbranch.EIS_ProcessReportingPeriod.facilitysiteID = AIRBranch.EIS_Process.FacilitySiteID " & _
                        '"and EIS_ProcessReportingPeriod.IntInventoryYear = '" & EISConfirm & "' " & _
                        '"and EIS_ProcessReportingPeriod.EmissionsUnitID = EIS_Process.emissionsunitid " & _
                        '"and EIS_ProcessReportingPeriod.ProcessID = eis_process.processid " & _
                        '"and EIS_ProcessReportingPeriod.FacilitySiteID = '" & dgvEISStats(1, i).Value & "'  ) "

                        'cmd = New OracleCommand(SQL, conn)
                        'If conn.State = ConnectionState.Closed Then
                        '    conn.Open()
                        'End If
                        'cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "update AIRBranch.EIS_ProcessControlApproach set " & _
                        '              "intLastInventoryYear = '" & EISConfirm & "' " & _
                        '              "where exists " & _
                        '              "(select * from airbranch.EIS_ProcessReportingPeriod " & _
                        '              "where airbranch.EIS_ProcessReportingPeriod.facilitysiteID = AIRBranch.EIS_ProcessControlApproach.FacilitySiteID " & _
                        '              "and EIS_ProcessReportingPeriod.IntInventoryYear = '" & EISConfirm & "' " & _
                        '              "and EIS_ProcessReportingPeriod.EmissionsUnitID = EIS_ProcessControlApproach.emissionsunitid " & _
                        '              "and EIS_ProcessReportingPeriod.ProcessID = EIS_ProcessControlApproach.processid " & _
                        '              "and EIS_ProcessReportingPeriod.FacilitySiteID = '" & dgvEISStats(1, i).Value & "'  ) "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "update AIRBranch.EIS_ProcessControlApproach set " & _
                        '               "intFirstInventoryYear = '" & EISConfirm & "' " & _
                        '               "where exists " & _
                        '               "(select * from airbranch.EIS_ProcessReportingPeriod " & _
                        '               "where airbranch.EIS_ProcessReportingPeriod.facilitysiteID = AIRBranch.EIS_ProcessControlApproach.FacilitySiteID " & _
                        '               "and EIS_ProcessReportingPeriod.IntInventoryYear = '" & EISConfirm & "' " & _
                        '               "and EIS_ProcessReportingPeriod.EmissionsUnitID = EIS_ProcessControlApproach.emissionsunitid " & _
                        '               "and EIS_ProcessReportingPeriod.ProcessID = EIS_ProcessControlApproach.processid " & _
                        '               "and intFirstInventoryYEar is null " & _
                        '               "and EIS_ProcessReportingPeriod.FacilitySiteID = '" & dgvEISStats(1, i).Value & "'  ) "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "update AIRBranch.EIS_UnitControlApproach set " & _
                        '                "intLastInventoryYear = '" & EISConfirm & "' " & _
                        '                "where exists " & _
                        '                "(select * from airbranch.EIS_ProcessReportingPeriod " & _
                        '                "where airbranch.EIS_ProcessReportingPeriod.facilitysiteID = AIRBranch.EIS_UnitControlApproach.FacilitySiteID " & _
                        '                "and EIS_ProcessReportingPeriod.IntInventoryYear = '" & EISConfirm & "' " & _
                        '                "and EIS_ProcessReportingPeriod.EmissionsUnitID = EIS_UnitControlApproach.emissionsunitid " & _
                        '                "and EIS_ProcessReportingPeriod.EmissionsUnitID = EIS_UnitControlApproach.EmissionsUnitID " & _
                        '                "and EIS_ProcessReportingPeriod.FacilitySiteID = '" & dgvEISStats(1, i).Value & "'  ) "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "update AIRBranch.EIS_UnitControlApproach set " & _
                        '                "intFirstInventoryYear = '" & EISConfirm & "' " & _
                        '                "where exists " & _
                        '                "(select * from airbranch.EIS_ProcessReportingPeriod " & _
                        '                "where airbranch.EIS_ProcessReportingPeriod.facilitysiteID = AIRBranch.EIS_UnitControlApproach.FacilitySiteID " & _
                        '                "and EIS_ProcessReportingPeriod.IntInventoryYear = '" & EISConfirm & "' " & _
                        '                "and EIS_ProcessReportingPeriod.EmissionsUnitID = EIS_UnitControlApproach.emissionsunitid " & _
                        '                "and EIS_ProcessReportingPeriod.EmissionsUnitID = EIS_UnitControlApproach.EmissionsUnitID " & _
                        '                "and intFirstInventoryYEar is null " & _
                        '                "and EIS_ProcessReportingPeriod.FacilitySiteID = '" & dgvEISStats(1, i).Value & "'  ) "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete airbranch.EIS_UnitControlPollutant " & _
                        '                "where active = '0' " & _
                        '                "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete airbranch.EIS_UnitControlMeasure  " & _
                        '                "where active = '0' " & _
                        '                "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete airbranch.EIS_UnitControlApproach  " & _
                        '                "where active = '0' " & _
                        '                "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete airbranch.EIS_RPGEOCoordinates  " & _
                        '                "where active = '0' " & _
                        '                "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete airbranch.EIS_RPApportionment  " & _
                        '                "where active = '0' " & _
                        '                "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete airbranch.EIS_ProcessControlPollutant " & _
                        '                "where active = '0' " & _
                        '                "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete airbranch.EIS_ProcessControlMeasure " & _
                        '                "where active = '0'" & _
                        '                "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete airbranch.EIS_ProcessControlApproach  " & _
                        '                "where active = '0' " & _
                        '                "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete airbranch.EIS_ReportingPeriodEmissions  " & _
                        '                "where active = '0'  " & _
                        '                "and intinventoryyear = '" & EISConfirm & "' " & _
                        '                "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete airbranch.EIS_ProcessOperatingDetails  " & _
                        '                "where active = '0'  " & _
                        '                "and intInventoryYear = '" & EISConfirm & "' " & _
                        '                "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete airbranch.EIS_ProcessRPTPeriodSCP  " & _
                        '                "where Active = '0'  " & _
                        '                "and intInventoryYear = '" & EISConfirm & "' " & _
                        '                "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete Airbranch.EIS_RPApportionment " & _
                        '                "where exists (select * " & _
                        '                "from Airbranch.eis_Process " & _
                        '                "where active = '0' " & _
                        '                "and Airbranch.EIS_RPApportionment.facilitysiteid = Airbranch.eis_Process.facilitysiteid " & _
                        '                "and Airbranch.EIS_RPApportionment.ProcessId = Airbranch.eis_Process.ProcessId " & _
                        '                "and Airbranch.EIS_RPApportionment.EmissionsUnitID  = Airbranch.eis_Process.EmissionsUnitID) " & _
                        '                "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = " delete Airbranch.EIS_ProcessControlPollutant " & _
                        '                " where exists (select *  " & _
                        '                " from Airbranch.EIS_ProcessControlApproach, airbranch.EIS_Process   " & _
                        '                " where Airbranch.EIS_ProcessControlPollutant.facilitysiteid = Airbranch.EIS_ProcessControlApproach.facilitysiteid " & _
                        '                " and Airbranch.EIS_ProcessControlPollutant.ProcessId = Airbranch.EIS_ProcessControlApproach.ProcessId   " & _
                        '                " and Airbranch.EIS_ProcessControlPollutant.EmissionsUnitID  = Airbranch.EIS_ProcessControlApproach.EmissionsUnitID " & _
                        '                "and  Airbranch.EIS_ProcessControlPollutant.facilitysiteid = Airbranch.EIS_Process.facilitysiteid " & _
                        '                " and Airbranch.EIS_ProcessControlPollutant.ProcessId = Airbranch.EIS_Process.ProcessId   " & _
                        '                " and Airbranch.EIS_ProcessControlPollutant.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID  " & _
                        '                " and EIS_Process.active = '0' ) " & _
                        '                "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete Airbranch.EIS_ProcessControlMeasure  " & _
                        ' "where exists (select * " & _
                        ' "from  airbranch.EIS_Process  " & _
                        ' "where   Airbranch.EIS_ProcessControlMeasure.facilitysiteid = Airbranch.EIS_Process.facilitysiteid " & _
                        ' "and Airbranch.EIS_ProcessControlMeasure.ProcessId = Airbranch.EIS_Process.ProcessId  " & _
                        ' "and Airbranch.EIS_ProcessControlMeasure.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID " & _
                        ' "and EIS_Process.active = '0' ) " & _
                        '                          "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete Airbranch.EIS_ProcessControlApproach " & _
                        '            "where exists (select * " & _
                        '            "from Airbranch.eis_Process " & _
                        '            "where active = '0' " & _
                        '            "and Airbranch.EIS_ProcessControlApproach.facilitysiteid = Airbranch.eis_Process.facilitysiteid " & _
                        '            "and Airbranch.EIS_ProcessControlApproach.ProcessId = Airbranch.eis_Process.ProcessId " & _
                        '            "and Airbranch.EIS_ProcessControlApproach.EmissionsUnitID  = Airbranch.eis_Process.EmissionsUnitID) " & _
                        '                          "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete Airbranch.EIS_ProcessControlPollutant " & _
                        '"where exists (select * " & _
                        '"from Airbranch.EIS_ProcessControlApproach " & _
                        '"where active = '0' " & _
                        '"and Airbranch.EIS_ProcessControlPollutant.facilitysiteid = Airbranch.EIS_ProcessControlApproach.facilitysiteid " & _
                        '"and Airbranch.EIS_ProcessControlPollutant.ProcessId = Airbranch.EIS_ProcessControlApproach.ProcessId " & _
                        '"and Airbranch.EIS_ProcessControlPollutant.EmissionsUnitID  = Airbranch.EIS_ProcessControlApproach.EmissionsUnitID) " & _
                        '                          "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete  Airbranch.EIS_ProcessOperatingDetails   " & _
                        '"where exists (select * " & _
                        '"from Airbranch.EIS_Process  " & _
                        '"where active = '0'  " & _
                        '"and Airbranch.EIS_ProcessOperatingDetails.facilitysiteid = Airbranch.EIS_Process.facilitysiteid  " & _
                        '"and Airbranch.EIS_ProcessOperatingDetails.ProcessId = Airbranch.EIS_Process.ProcessId  " & _
                        '"and Airbranch.EIS_ProcessOperatingDetails.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID)  " & _
                        '                          "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete  Airbranch.EIS_ReportingPeriodEmissions   " & _
                        '"where exists (select * " & _
                        '"from Airbranch.EIS_Process  " & _
                        '"where active = '0'  " & _
                        '"and Airbranch.EIS_ReportingPeriodEmissions.facilitysiteid = Airbranch.EIS_Process.facilitysiteid  " & _
                        '"and Airbranch.EIS_ReportingPeriodEmissions.ProcessId = Airbranch.EIS_Process.ProcessId  " & _
                        '"and Airbranch.EIS_ReportingPeriodEmissions.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID)  " & _
                        '                          "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete  Airbranch.EIS_ProcessRPTPeriodSCP   " & _
                        '             "where exists (select * " & _
                        '             "from Airbranch.EIS_Process  " & _
                        '             "where active = '0'  " & _
                        '             "and Airbranch.EIS_ProcessRPTPeriodSCP.facilitysiteid = Airbranch.EIS_Process.facilitysiteid  " & _
                        '             "and Airbranch.EIS_ProcessRPTPeriodSCP.ProcessId = Airbranch.EIS_Process.ProcessId  " & _
                        '             "and Airbranch.EIS_ProcessRPTPeriodSCP.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID)  " & _
                        '                          "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete Airbranch.eis_processReportingPeriod   " & _
                        ' "where exists (select * " & _
                        ' "from  airbranch.EIS_Process  " & _
                        ' "where   Airbranch.eis_processReportingPeriod.facilitysiteid = Airbranch.EIS_Process.facilitysiteid " & _
                        ' "and Airbranch.eis_processReportingPeriod.ProcessId = Airbranch.EIS_Process.ProcessId  " & _
                        ' "and Airbranch.eis_processReportingPeriod.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID " & _
                        ' "and EIS_Process.active = '0' ) " & _
                        '                          "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete airbranch.EIS_Process  " & _
                        '                              "where Active = '0' " & _
                        '                          "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "Delete airbranch.EIS_EmissionsUnit   " & _
                        '                "where active = '0' " & _
                        '                          "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        '                SQL = "delete airbranch.EIS_Releasepoint  " & _
                        '                                              "where active = '0'  " & _
                        '                                              "and numRPStatusCodeYear = '" & EISConfirm & "' " & _
                        '                          "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        '                cmd = New OracleCommand(SQL, conn)
                        '                If conn.State = ConnectionState.Closed Then
                        '                    conn.Open()
                        '                End If
                        '                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                        If CurrentConnection.State = ConnectionState.Open Then
                            CurrentConnection.Close()
                        End If
                    End If
                Next
                ViewEISStats()
                MsgBox(EISConfirm & " QA process began.", MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("Year does not match selected EIS year")

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnEILogUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEILogUpdate.Click
        Try
            Dim EISAccess As String = " "
            Dim OptOut As String = ""
            Dim EISStatus As String = ""
            Dim Enrollment As String = ""
            Dim Mailout As String = ""
            Dim ActiveStatus As String = ""
            Dim IncorrectlyOptedOut As String = ""

            If rdbEILogMailoutYes.Checked = True Then
                Mailout = "1"
            Else
                If rdbEILogMailoutNo.Checked = True Then
                    Mailout = "0"
                Else
                    Mailout = ""
                End If
            End If
            If rdbEILogEnrolledYes.Checked = True Then
                Enrollment = "1"
            Else
                If rdbEILogEnrolledNo.Checked = True Then
                    Enrollment = "0"
                Else
                    Enrollment = "0"
                End If
            End If
            If rdbEILogOpOutYes.Checked = True Then
                OptOut = "1"
            Else
                If rdbEILogOpOutNo.Checked = True Then
                    OptOut = "0"
                Else
                    OptOut = ""
                End If
            End If
            If chbOptedOutIncorrectly.Checked = True Then
                IncorrectlyOptedOut = "1"
            Else
                IncorrectlyOptedOut = "0"
            End If
            EISStatus = cboEILogStatusCode.SelectedValue
            EISAccess = cboEILogAccessCode.SelectedValue
            If rdbEILogActiveYes.Checked = True Then
                ActiveStatus = "1"
            Else
                ActiveStatus = "0"
            End If

            SQL = "Select FacilitySiteID from airbranch.EIS_Admin " & _
            "where inventoryyear = '" & cboEILogYear.Text & "' " & _
            "and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = False Then
                MsgBox("The faciltiy is not currently in the EIS universe for the selected year." & vbCrLf & _
                       "Use the Add New Facility to Year." & vbCrLf & vbCrLf & "NO DATA SAVED", MsgBoxStyle.Information, Me.Text)

                Exit Sub
            End If

            SQL = "Update AIRBranch.EIS_Admin set " & _
            "EISStatusCode = '" & EISStatus & "', " & _
            "DatEISStatus = '" & dtpEILogStatusDateSubmit.Text & "', " & _
            "EISAccessCode = '" & EISAccess & "', " & _
            "strOptOut = '" & OptOut & "', " & _
            "strIncorrectOptOut = '" & IncorrectlyOptedOut & "', " & _
            "strMailout = '" & Mailout & "', " & _
            "strEnrollment = '" & Enrollment & "', " & _
            "datEnrollment = '" & dtpEILogDateEnrolled.Text & "', " & _
            "strComment = '" & Replace(txtEILogComments.Text, "'", "''") & "', " & _
            "active = '" & ActiveStatus & "', " & _
            "updateUser = '" & Replace(UserName, "'", "''") & "', " & _
            "updateDateTime = sysdate " & _
            "where inventoryyear = '" & cboEILogYear.Text & "' " & _
            "and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteReader()

            If dtpDeadlineEIS.Checked = True Then
                Dim DeadLineComments As String = ""
                If txtAllEISDeadlineComment.Text.Contains(dtpDeadlineEIS.Text & "(deadline)- " & UserName & " - " & OracleDate & vbCrLf & _
                txtEISDeadlineComment.Text) Then
                Else
                    DeadLineComments = dtpDeadlineEIS.Text & "(deadline)- " & UserName & " - " & OracleDate & vbCrLf & _
                    txtEISDeadlineComment.Text & _
                    vbCrLf & vbCrLf & txtAllEISDeadlineComment.Text

                    SQL = "update Airbranch.EIS_Admin set " & _
                    "datEISDeadline = '" & dtpDeadlineEIS.Text & "',  " & _
                    "strEISDeadlineComment = '" & Replace(DeadLineComments, "'", "''") & "' " & _
                    "where INventoryyear = '" & cboEILogYear.Text & "' " & _
                    "and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteReader()
                End If
            End If

            'If cboEILogStatusCode.SelectedValue = "4" Or cboEILogStatusCode.SelectedValue = "5" And rdbEILogOpOutYes.Checked = False Then
            If rdbEILogOpOutYes.Checked = False Then
                Dim QAStart As String = ""
                Dim QAPass As String = ""
                Dim QAStatusCode As String = ""
                Dim QAStatusDate As String = ""
                Dim StaffResponsible As String = ""
                Dim QAComplete As String = ""
                Dim QAComments As String = ""
                Dim FITracking As String = ""
                Dim pointTracking As String = ""
                Dim FIError As String = ""
                Dim pointError As String = ""

                QAStart = dtpQAStarted.Text
                If dtpQAPassed.Checked = True Then
                    QAPass = dtpQAPassed.Text
                Else
                    QAPass = ""
                End If
                If dtpQACompleted.Checked = True Then
                    QAComplete = dtpQACompleted.Text
                Else
                    QAComplete = ""
                End If
                QAStatusCode = cboEISQAStatus.SelectedValue
                QAStatusDate = OracleDate
                StaffResponsible = cboEISQAStaff.Text
                If txtQAComments.Text = "" Then
                    If txtAllQAComments.Text = "" Then
                        QAComments = ""
                    Else
                        QAComments = txtAllQAComments.Text
                    End If
                Else
                    If txtAllQAComments.Text = "" Then
                        QAComments = UserName & " - " & OracleDate & vbCrLf & txtQAComments.Text
                    Else
                        QAComments = UserName & " - " & OracleDate & vbCrLf & txtQAComments.Text & vbCrLf & vbCrLf & _
                             txtAllQAComments.Text
                    End If
                End If
                If txtFITrackingNumber.Text = "" Then
                    If txtAllFITrackingNumbers.Text = "" Then
                        FITracking = ""
                    Else
                        FITracking = txtAllFITrackingNumbers.Text
                    End If
                Else
                    If txtAllFITrackingNumbers.Text = "" Then
                        FITracking = UserName & " - " & OracleDate & vbCrLf & txtFITrackingNumber.Text
                    Else
                        FITracking = UserName & " - " & OracleDate & vbCrLf & txtFITrackingNumber.Text & vbCrLf & vbCrLf & _
                                    txtAllFITrackingNumbers.Text
                    End If
                End If
                If chbFIErrors.Checked = True Then
                    FIError = "True"
                Else
                    FIError = "False"
                End If
                If txtPointTrackingNumber.Text = "" Then
                    If txtAllPointTrackingNumbers.Text = "" Then
                        pointTracking = ""
                    Else
                        pointTracking = txtAllPointTrackingNumbers.Text
                    End If
                Else
                    If txtAllPointTrackingNumbers.Text = "" Then
                        pointTracking = UserName & " - " & OracleDate & vbCrLf & txtPointTrackingNumber.Text
                    Else
                        pointTracking = UserName & " - " & OracleDate & vbCrLf & txtPointTrackingNumber.Text & vbCrLf & vbCrLf & _
                                txtAllPointTrackingNumbers.Text
                    End If
                End If
                If chbPointErrors.Checked = True Then
                    pointError = "True"
                Else
                    pointError = "False"
                End If

                ' SQL = "insert into AIRBranch.EIS_QAAdmin " & _
                '"(select " & _
                '"'" & txtEILogSelectedYear.Text & "', '" & txtEILogSelectedAIRSNumber.Text & "', " & _
                '"'" & QAStart & "', '" & QAPass & "', " & _
                '"'1', '" & QAStatusDate & "', " & _
                '"'" & Replace(StaffResponsible, "'", "''") & "', " & _
                '"'" & QAComplete & "', '" & Replace(QAComments, "'", "''") & "', " & _
                '"'1', '" & UserName & "', " & _
                '"sysdate, sysdate, " & _
                '"'" & Replace(FITracking, "'", "''") & "', " & _
                '"'" & Replace(FIError, "'", "''") & "', " & _
                '"'" & Replace(pointTracking, "'", "''") & "', " & _
                '"'" & Replace(pointError, "'", "''") & "', '', '' " & _
                '"from dual " & _
                '"where not exists (select * from AIRBranch.EIS_QAAdmin " & _
                '"where inventoryYear = '" & txtEILogSelectedYear.Text & "' " & _
                '"and FacilitySiteID = '" & txtEILogSelectedAIRSNumber.Text & "')) "

                ' cmd = New OracleCommand(SQL, conn)
                ' If conn.State = ConnectionState.Closed Then
                '     conn.Open()
                ' End If
                ' cmd.ExecuteReader()

                SQL = "Update " & DBNameSpace & ".eis_QAAdmin set " & _
               "datDateQAStart = '" & QAStart & "', " & _
               "datDateQAPass = '" & QAPass & "', " & _
               "QAStatusCode = '" & QAStatusCode & "', " & _
               "datQAStatus = '" & QAStatusDate & "', " & _
               "strDMUResponsibleStaff = '" & Replace(StaffResponsible, "'", "''") & "', " & _
               "datQAComplete = '" & QAComplete & "', " & _
               "strComment = '" & Replace(QAComments, "'", "''") & "', " & _
               "active = '1', " & _
               "updateuser = '" & Replace(UserName, "'", "''") & "', " & _
               "updateDateTime = sysdate, " & _
               "strFITrackingnumber = '" & Replace(FITracking, "'", "''") & "', " & _
               "strFIError = '" & Replace(FIError, "'", "''") & "', " & _
               "STRPOINTTRACKINGNUMBER = '" & Replace(pointTracking, "'", "''") & "', " & _
               "strpointerror = '" & Replace(pointError, "'", "''") & "' " & _
               "where INventoryyear = '" & cboEILogYear.Text & "' " & _
               "and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                LoadQASpecificData()
            End If

            LoadAdminData()
            MsgBox("Admin Data updated.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEILogAddNewFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEILogAddNewFacility.Click
        Try
            Dim EISAccess As String = " "
            Dim OptOut As String = ""
            Dim EISStatus As String = ""
            Dim Enrollment As String = ""
            Dim Mailout As String = ""
            Dim ActiveStatus As String = ""

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd = New OracleCommand("AIRBranch.PD_EIS_Data", CurrentConnection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("AIRSNUM", OracleDbType.Varchar2)).Value = txtEILogSelectedAIRSNumber.Text
            cmd.Parameters.Add(New OracleParameter("INTYEAR", OracleDbType.Decimal)).Value = txtEILogSelectedYear.Text

            cmd.ExecuteNonQuery()

            'If rdbEILogMailoutYes.Checked = True Then
            '    Mailout = "1"
            'Else
            '    If rdbEILogMailoutNo.Checked = True Then
            '        Mailout = "0"
            '    Else
            '        Mailout = ""
            '    End If
            'End If
            'If rdbEILogEnrolledYes.Checked = True Then
            '    Enrollment = "1"
            'Else
            '    If rdbEILogEnrolledNo.Checked = True Then
            '        Enrollment = "0"
            '    Else
            '        Enrollment = "0"
            '    End If
            'End If
            'If rdbEILogOpOutYes.Checked = True Then
            '    OptOut = "1"
            'Else
            '    If rdbEILogOpOutNo.Checked = True Then
            '        OptOut = "0"
            '    Else
            '        OptOut = ""
            '    End If
            'End If
            'EISStatus = cboEILogStatusCode.SelectedValue
            'EISAccess = cboEILogAccessCode.SelectedValue
            'If rdbEILogActiveYes.Checked = True Then
            '    ActiveStatus = "1"
            'Else
            '    ActiveStatus = "0"
            'End If

            'SQL = "Insert into AIRBranch.EIS_Admin " & _
            '"(select " & _
            '"'" & cboEILogYear.Text & "', '" & mtbEILogAIRSNumber.Text & "', " & _
            '"'" & EISStatus & "', sysdate, " & _
            '"'" & EISAccess & "', '" & OptOut & "', " & _
            '"'', '', '', " & _
            '"'" & Mailout & "', '" & Enrollment & "', " & _
            '"'" & dtpEILogDateEnrolled.Text & "', " & _
            '"'" & Replace(txtEILogComments.Text, "'", "''") & "', " & _
            '"'" & ActiveStatus & "', '" & Replace(UserName, "'", "''") & "', " & _
            '"sysdate, sysdate, '' from dual " & _
            '"where not exists (Select * from AIRBranch.EIS_Admin " & _
            '"where inventoryyear = '" & cboEILogYear.Text & "' " & _
            '"and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "')) "

            'cmd = New OracleCommand(SQL, conn)
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If
            'cmd.ExecuteReader()

            LoadAdminData()
            MsgBox("New Facility Added", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISEndQA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to move Facilities into the QA process.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                temp = ""

                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    SQL = "Update airbranch.EIS_QAAdmin set " & _
                    "datDateAQPass = sysdate " & _
                    "strDMUResponsibleStaff = '" & UserName & "', " & _
                    "updateUSer = '" & UserName & "', " & _
                    "updatedateTime = sysdate " & _
                    "where inventoryYear = '" & EISConfirm & "' " & _
                    "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' & " ' "


                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteReader()
                Next

                MsgBox(EISConfirm & " QA process began.", MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("Year does not match selected EIS year")

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnUpdateQAData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateQAData.Click
        Try
            Dim QAStart As String = ""
            Dim QAPass As String = ""
            Dim QAStatusCode As String = ""
            Dim QAStatusDate As String = ""
            Dim StaffResponsible As String = ""
            Dim QAComplete As String = ""
            Dim QAComments As String = ""
            Dim FITracking As String = ""
            Dim PointTracking As String = ""
            Dim FIError As String = ""
            Dim PointError As String = ""

            QAStart = dtpQAStarted.Text
            If dtpQAPassed.Checked = True Then
                QAPass = dtpQAPassed.Text
            Else
                QAPass = ""
            End If
            If dtpQACompleted.Checked = True Then
                QAComplete = dtpQACompleted.Text
            Else
                QAComplete = ""
            End If
            QAStatusCode = cboEISQAStatus.SelectedValue
            QAStatusDate = OracleDate
            StaffResponsible = cboEISQAStaff.Text
            If txtQAComments.Text = "" Then
                If txtAllQAComments.Text = "" Then
                    QAComments = ""
                Else
                    QAComments = txtAllQAComments.Text
                End If
            Else
                If txtAllQAComments.Text = "" Then
                    QAComments = UserName & " - " & OracleDate & vbCrLf & txtQAComments.Text
                Else
                    QAComments = UserName & " - " & OracleDate & vbCrLf & txtQAComments.Text & vbCrLf & vbCrLf & _
                         txtAllQAComments.Text
                End If
            End If
            If txtFITrackingNumber.Text = "" Then
                If txtAllFITrackingNumbers.Text = "" Then
                    FITracking = ""
                Else
                    FITracking = txtAllFITrackingNumbers.Text
                End If
            Else
                If txtAllFITrackingNumbers.Text = "" Then
                    FITracking = UserName & " - " & OracleDate & vbCrLf & txtFITrackingNumber.Text
                Else
                    FITracking = UserName & " - " & OracleDate & vbCrLf & txtFITrackingNumber.Text & vbCrLf & vbCrLf & _
                                txtAllFITrackingNumbers.Text
                End If
            End If
            If chbFIErrors.Checked = True Then
                FIError = "True"
            Else
                FIError = "False"
            End If

            If txtPointTrackingNumber.Text = "" Then
                If txtAllPointTrackingNumbers.Text = "" Then
                    PointTracking = ""
                Else
                    PointTracking = txtAllPointTrackingNumbers.Text
                End If
            Else
                If txtAllPointTrackingNumbers.Text = "" Then
                    PointTracking = UserName & " - " & OracleDate & vbCrLf & txtPointTrackingNumber.Text
                Else
                    PointTracking = UserName & " - " & OracleDate & vbCrLf & txtPointTrackingNumber.Text & vbCrLf & vbCrLf & _
                            txtAllPointTrackingNumbers.Text
                End If
            End If
            If chbPointErrors.Checked = True Then
                PointError = "True"
            Else
                PointError = "False"
            End If

            ' SQL = "insert into AIRBranch.EIS_QAAdmin " & _
            '"(select " & _
            '"'" & txtEILogSelectedYear.Text & "', '" & txtEILogSelectedAIRSNumber.Text & "', " & _
            '"'" & QAStart & "', '" & QAPass & "', " & _
            '"'1', '" & QAStatusDate & "', " & _
            '"'" & Replace(StaffResponsible, "'", "''") & "', " & _
            '"'" & QAComplete & "', '" & Replace(QAComments, "'", "''") & "', " & _
            '"'1', '" & UserName & "', " & _
            '"sysdate, sysdate, " & _
            '"'" & Replace(FITracking, "'", "''") & "', " & _
            '"'" & Replace(FIError, "'", "''") & "', " & _
            '"'" & Replace(PointTracking, "'", "''") & "', " & _
            '"'" & Replace(PointError, "'", "''") & "', '', '' " & _
            '"from dual " & _
            '"where not exists (select * from AIRBranch.EIS_QAAdmin " & _
            '"where inventoryYear = '" & txtEILogSelectedYear.Text & "' " & _
            '"and FacilitySiteID = '" & txtEILogSelectedAIRSNumber.Text & "')) "

            ' cmd = New OracleCommand(SQL, conn)
            ' If conn.State = ConnectionState.Closed Then
            '     conn.Open()
            ' End If
            ' cmd.ExecuteReader()

            SQL = "Update " & DBNameSpace & ".eis_QAAdmin set " & _
            "datDateQAStart = '" & QAStart & "', " & _
            "datDateQAPass = '" & QAPass & "', " & _
            "QAStatusCode = '" & QAStatusCode & "', " & _
            "datQAStatus = '" & QAStatusDate & "', " & _
            "strDMUResponsibleStaff = '" & Replace(StaffResponsible, "'", "''") & "', " & _
            "datQAComplete = '" & QAComplete & "', " & _
            "strComment = '" & Replace(QAComments, "'", "''") & "', " & _
            "active = '1', " & _
            "updateuser = '" & Replace(UserName, "'", "''") & "', " & _
            "updateDateTime = sysdate, " & _
            "strFITrackingnumber = '" & Replace(FITracking, "'", "''") & "', " & _
            "strFIError = '" & Replace(FIError, "'", "''") & "', " & _
            "STRPOINTTRACKINGNUMBER = '" & Replace(PointTracking, "'", "''") & "', " & _
            "strpointerror = '" & Replace(PointError, "'", "''") & "' " & _
            "where INventoryyear = '" & cboEILogYear.Text & "' " & _
            "and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteReader()

            LoadQASpecificData()

            If dtpQACompleted.Checked = True Then
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd = New OracleCommand("AIRBranch.PD_EIS_QA_Done", CurrentConnection)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add(New OracleParameter("AIRSNUM", OracleDbType.Varchar2)).Value = txtEILogSelectedAIRSNumber.Text
                cmd.Parameters.Add(New OracleParameter("INTYEAR", OracleDbType.Decimal)).Value = txtEILogSelectedYear.Text
                cmd.Parameters.Add(New OracleParameter("DATLASTSUBMIT", OracleDbType.Date)).Value = dtpQACompleted.Text

                cmd.ExecuteNonQuery()
            End If

            MsgBox("QA data saved.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEIModifyUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEIModifyUpdate.Click
        Try
            Dim FacilityName As String = ""
            Dim MailingAddress As String = ""
            Dim City As String = ""
            Dim State As String = ""
            Dim PostalCode As String = ""

            If txtEIModifyFacilityName.Text <> "" Then
                FacilityName = txtEIModifyFacilityName.Text
            Else
                FacilityName = ""
            End If
            If txtEIModifyLocation.Text <> "" Then
                MailingAddress = txtEIModifyLocation.Text
            Else
                MailingAddress = ""
            End If
            If txtEIModifyCity.Text <> "" Then
                City = txtEIModifyCity.Text
            Else
                City = ""
            End If
            If mtbEIModifyZipCode.Text <> "" Then
                PostalCode = mtbEIModifyZipCode.Text
            Else
                PostalCode = ""
            End If

            If txtEILogSelectedAIRSNumber.Text = "" Then
                MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If FacilityName <> "" Then
                SQL = "Update airbranch.EIS_FacilitySite set " & _
                "strFacilitySiteName = '" & Replace(FacilityName, "'", "''") & "' " & _
                "where facilitysiteid = '" & txtEILogSelectedAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

            End If
            If MailingAddress <> "" And City <> "" Then
                SQL = "Update airbranch.EIS_FacilitySiteAddress set " & _
                "strMailingAddressText = '" & Replace(MailingAddress, "'", "''") & "', " & _
                "strMailingAddresscityname = '" & Replace(City, "'", "''") & "', " & _
                "strMailingAddressPostalCode = '" & Replace(PostalCode, "'", "''") & "' " & _
                "where facilitysiteid = '" & txtEILogSelectedAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                UpdateFacilityGEOCoord()

                MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("No data saved." & vbCrLf & "BOTH MAILING ADDRESS AND CITY ARE REQUIRED", MsgBoxStyle.Exclamation, Me.Text)
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub UpdateFacilityGEOCoord()
        Try
            If txtEILogSelectedAIRSNumber.Text = "" Then
                MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If mtbEIModifyLatitude.Text <> "" And mtbEIModifyLongitude.Text <> "" Then
                SQL = "Update airbranch.EIS_FacilityGEOCoord set " & _
                "numLatitudeMeasure = '" & mtbEIModifyLatitude.Text & "', " & _
                "numLongitudeMeasure = '-" & mtbEIModifyLongitude.Text & "' " & _
                "where facilitySiteID = '" & txtEILogSelectedAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "Update AIRBranch.APBFacilityInformation set " & _
                "numFacilityLongitude = '-" & mtbEIModifyLongitude.Text & "', " & _
                "numFacilityLatitude = '" & mtbEIModifyLatitude.Text & "', " & _
                "strComments = 'Updated by " & UserName & " through DMU Staff Tools - Emissions Inventory Log. ', " & _
                "strModifingPerson = '" & UserGCode & "', " & _
                "datModifingDate = sysdate " & _
                "where strAIRSNumber = '0413" & txtEILogSelectedAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

            Else
                MsgBox("Latitude & Longitude data not saved." & vbCrLf & "Add both values to update.", _
                         MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateLatLong_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateLatLong.Click
        Try
            If txtEILogSelectedAIRSNumber.Text = "" Then
                MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            UpdateFacilityGEOCoord()
            MsgBox("Data updated.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEIModifyCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEIModifyCopy.Click
        Try
            If txtEILogSelectedAIRSNumber.Text = "" Then
                MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            txtEIModifyFacilityName.Text = txtEIModifyIAIPFacilityName.Text
            txtEIModifyLocation.Text = txtEIModifyIAIPLocation.Text
            txtEIModifyCity.Text = txtEIModifyIAIPCity.Text
            mtbEIModifyZipCode.Text = mtbEIModifyIAIPZipCode.Text
            mtbEIModifyLatitude.Text = mtbEIModifyIAIPLatitude.Text
            mtbEIModifyLongitude.Text = mtbEIModifyIAIPLongitude.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISMailoutUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISMailoutUpdate.Click
        Try

            If txtEILogSelectedAIRSNumber.Text = "" Then
                MsgBox("Select a valid AIRS Number.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Update airbranch.EIS_Mailout set " & _
            "strFacilityName= '" & Replace(txtEISMailoutEditFacilityName.Text, "'", "''") & "', " & _
            "strContactCompanyName = '" & Replace(txtEISMailoutEditCompanyName.Text, "'", "''") & "', " & _
            "strContactAddress1 = '" & Replace(txtEISMailoutEditAdress.Text, "'", "''") & "', " & _
            "strContactAddress2 = '" & Replace(txtEISMailoutEditAddress2.Text, "'", "''") & "', " & _
            "strContactCity = '" & Replace(txtEISMailoutEditCity.Text, "'", "''") & "', " & _
            "strContactState = '" & Replace(txtEISMailoutEditState.Text, "'", "''") & "', " & _
            "strContactZipCode = '" & Replace(txtEISMailoutEditZipCode.Text, "'", "''") & "', " & _
            "strContactFirstName = '" & Replace(txtEISMailoutEditFirstName.Text, "'", "''") & "', " & _
            "strContactLastName = '" & Replace(txtEISMailoutEditLastName.Text, "'", "''") & "', " & _
            "strContactPrefix = '" & Replace(txtEISMailoutEditPrefix.Text, "'", "''") & "', " & _
            "strContactEmail = '" & Replace(txtEISMailoutEditEmailAddress.Text, "'", "''") & "', " & _
            "strComment = '" & Replace(txtEISMailoutEditComments.Text, "'", "''") & "' " & _
            "where FacilitySiteid = '" & txtEILogSelectedAIRSNumber.Text & "' " & _
            "and intInventoryYear = '" & txtEILogSelectedYear.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteReader()

            MsgBox("Data updated", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedToDo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedToDo.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", " and EISStatusCode >= 3 ", "", " and QAStatusCode is null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, To-Do Count"

            Exit Sub

            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
            "'False' as ID, " & _
            " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
           "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
           "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
           "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
           "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
           "case " & _
           "when strOptOut = '1' then 'Yes' " & _
           "when strOptOut = '0' then 'No' " & _
           "else '' " & _
           "End strOptOut, " & _
           "case " & _
           "when strMailout = '1' then 'Yes' " & _
           "else 'No' " & _
           "end strMailout, " & _
           "case " & _
           "when strContactEmail is null then '-' " & _
           "else strContactEmail " & _
           "end ContactEmail, " & _
             "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
           "case " & _
          "when strDMUResponsibleStaff is null then '-' " & _
           "else strDMUResponsibleStaff " & _
            "end strDMUResponsibleStaff " & _
           "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
           "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
           "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " & _
           "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
           "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
           "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
           "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
           "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
            "and AIRBranch.EIS_Admin.Active = '1' " & _
           "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
          "and AIRbranch.EIS_Admin.strEnrollment = '1'  " & _
 "and AIRbranch.EIS_Admin.eisstatuscode >= 3 " & _
 "and (strOptOut = '0' ) " & _
 "and  NOT  exists (Select * from AIRBranch.EIS_QAAdmin " & _
 "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
 "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID)    " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "


            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = False
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = False
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If


                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, To-Do Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedBegan_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBegan.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", _
                      " and EISStatusCode >= 3 ", "", " and QAStatusCode is not null and datQAComplete is null ")


            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"

            Exit Sub

            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
            "'False' as ID, " & _
            " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
           "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
           "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
           "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
           "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
           "case " & _
           "when strOptOut = '1' then 'Yes' " & _
           "when strOptOut = '0' then 'No' " & _
           "else '' " & _
           "End strOptOut, " & _
           "case " & _
           "when strMailout = '1' then 'Yes' " & _
           "else 'No' " & _
           "end strMailout, " & _
           "case " & _
           "when strContactEmail is null then '-' " & _
           "else strContactEmail " & _
           "end ContactEmail, " & _
             "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
           "case " & _
          "when strDMUResponsibleStaff is null then '-' " & _
           "else strDMUResponsibleStaff " & _
            "end strDMUResponsibleStaff " & _
           "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
           "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
           "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " & _
           "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
           "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
           "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
           "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
                      "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
            "and AIRBranch.EIS_Admin.Active = '1' " & _
           "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
                     "and AIRbranch.EIS_Admin.strEnrollment = '1'  " & _
 "and AIRbranch.EIS_Admin.eisstatuscode >= 3  " & _
 "and (strOptOut = '0' ) " & _
 "and    exists (Select * from AIRBranch.EIS_QAAdmin " & _
 "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
 "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
 "and datQAComplete is null )  " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = False
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = False
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedBeganwFIErrors_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwFIErrors.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", _
             " and EISStatusCode >= 3 ", "", _
             " and datQAComplete is null and strFIError = 'True' and (strPointError = 'False' or strPointError is null) ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"

            Exit Sub


            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
            "'False' as ID, " & _
            " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
           "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
           "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
           "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
           "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
           "case " & _
           "when strOptOut = '1' then 'Yes' " & _
           "when strOptOut = '0' then 'No' " & _
           "else '' " & _
           "End strOptOut, " & _
           "case " & _
           "when strMailout = '1' then 'Yes' " & _
           "else 'No' " & _
           "end strMailout, " & _
           "case " & _
           "when strContactEmail is null then '-' " & _
           "else strContactEmail " & _
           "end ContactEmail, " & _
             "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
           "case " & _
          "when strDMUResponsibleStaff is null then '-' " & _
           "else strDMUResponsibleStaff " & _
            "end strDMUResponsibleStaff " & _
           "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
           "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
           "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " & _
           "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
           "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
           "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
           "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
                      "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
            "and AIRBranch.EIS_Admin.Active = '1' " & _
           "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
                     "and AIRbranch.EIS_Admin.strEnrollment = '1'  " & _
 "and AIRbranch.EIS_Admin.eisstatuscode >= 3  " & _
 "and (strOptOut = '0' ) " & _
 "and    exists (Select * from AIRBranch.EIS_QAAdmin " & _
 "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
 "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
 "and datQAComplete is null )  " & _
 "and strFIError = 'True' and (strPointError = 'False' or strPointError is null) " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = False
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = False
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsSubmittedBeganwithEIErrors_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwithEIErrors.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", _
             " and EISStatusCode >= 3 ", "", _
             " and datQAComplete is null and (strFIError = 'False' or strFIError is null) and strPointError = 'True'  ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"

            Exit Sub

            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
            "'False' as ID, " & _
            " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
           "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
           "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
           "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
           "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
           "case " & _
           "when strOptOut = '1' then 'Yes' " & _
           "when strOptOut = '0' then 'No' " & _
           "else '' " & _
           "End strOptOut, " & _
           "case " & _
           "when strMailout = '1' then 'Yes' " & _
           "else 'No' " & _
           "end strMailout, " & _
           "case " & _
           "when strContactEmail is null then '-' " & _
           "else strContactEmail " & _
           "end ContactEmail, " & _
             "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
           "case " & _
          "when strDMUResponsibleStaff is null then '-' " & _
           "else strDMUResponsibleStaff " & _
            "end strDMUResponsibleStaff " & _
           "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
           "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
           "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " & _
           "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
           "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
           "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
           "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
                      "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
            "and AIRBranch.EIS_Admin.Active = '1' " & _
           "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
                     "and AIRbranch.EIS_Admin.strEnrollment = '1'  " & _
 "and AIRbranch.EIS_Admin.eisstatuscode >= 3  " & _
 "and (strOptOut = '0' ) " & _
 "and    exists (Select * from AIRBranch.EIS_QAAdmin " & _
 "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
 "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
 "and datQAComplete is null )  " & _
 "and (strFIError = 'False' or strFIError is null) and strPointError = 'True' " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = False
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = False
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISStatsSubmittedBeganwithBothErrors_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwithBothErrors.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", _
          " and EISStatusCode >= 3 ", "", _
          " and datQAComplete is null and (strFIError = 'True' ) and (strPointError = 'True' ) ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"

            Exit Sub


            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
            "'False' as ID, " & _
            " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
           "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
           "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
           "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
           "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
           "case " & _
           "when strOptOut = '1' then 'Yes' " & _
           "when strOptOut = '0' then 'No' " & _
           "else '' " & _
           "End strOptOut, " & _
           "case " & _
           "when strMailout = '1' then 'Yes' " & _
           "else 'No' " & _
           "end strMailout, " & _
           "case " & _
           "when strContactEmail is null then '-' " & _
           "else strContactEmail " & _
           "end ContactEmail, " & _
             "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
           "case " & _
          "when strDMUResponsibleStaff is null then '-' " & _
           "else strDMUResponsibleStaff " & _
            "end strDMUResponsibleStaff " & _
           "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
           "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
           "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " & _
           "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
           "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
           "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
           "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
                      "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
            "and AIRBranch.EIS_Admin.Active = '1' " & _
           "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
                     "and AIRbranch.EIS_Admin.strEnrollment = '1'  " & _
 "and AIRbranch.EIS_Admin.eisstatuscode >= 3  " & _
 "and (strOptOut = '0' ) " & _
 "and    exists (Select * from AIRBranch.EIS_QAAdmin " & _
 "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
 "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
 "and datQAComplete is null )  " & _
 "and (strFIError = 'True' ) and (strPointError = 'True' ) " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = False
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = False
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISStatsSubmittedBeganwithoutErrors_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedBeganwithoutErrors.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", _
          " and EISStatusCode >= 3 ", "", _
          " and datQAComplete is null and (strFIError = 'False' or strFIError is null) and " & _
          "(strPointError = 'False' or strPointError is null) ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"

            Exit Sub

            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
            "'False' as ID, " & _
            " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
           "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
           "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
           "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
           "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
           "case " & _
           "when strOptOut = '1' then 'Yes' " & _
           "when strOptOut = '0' then 'No' " & _
           "else '' " & _
           "End strOptOut, " & _
           "case " & _
           "when strMailout = '1' then 'Yes' " & _
           "else 'No' " & _
           "end strMailout, " & _
           "case " & _
           "when strContactEmail is null then '-' " & _
           "else strContactEmail " & _
           "end ContactEmail, " & _
             "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
           "case " & _
          "when strDMUResponsibleStaff is null then '-' " & _
           "else strDMUResponsibleStaff " & _
            "end strDMUResponsibleStaff " & _
           "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
           "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
           "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " & _
           "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
           "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
           "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
           "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
                      "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
            "and AIRBranch.EIS_Admin.Active = '1' " & _
           "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
                     "and AIRbranch.EIS_Admin.strEnrollment = '1'  " & _
 "and AIRbranch.EIS_Admin.eisstatuscode >= 3  " & _
 "and (strOptOut = '0' ) " & _
 "and    exists (Select * from AIRBranch.EIS_QAAdmin " & _
 "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
 "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
 "and datQAComplete is null )  " & _
 "and (strFIError = 'False' or strFIError is null) and (strPointError = 'False' or strPointError is null) " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = False
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = False
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, Started Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub



    Private Sub llbEISStatsOptedOutToDo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsOptedOutToDo.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", " and (strOptOut = '1' or stroptout is null )", _
             " and EISStatusCode >= 3 ", "", " and QAStatusCode is null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Opted-Out, To-do Count"

            Exit Sub


            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
           "'False' as ID, " & _
           " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
          "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
          "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
          "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
          "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
                 "case " & _
            "when strOptOutreason is null then 'No' " & _
            "when strOptoutReason = '1' then 'Did not Operate' " & _
            "when strOptOutReason = '2' then 'Pollutant below Threshold' " & _
            "end strOptOut, " & _
          "case " & _
          "when strMailout = '1' then 'Yes' " & _
          "else 'No' " & _
          "end strMailout, " & _
          "case " & _
          "when strContactEmail is null then '-' " & _
          "else strContactEmail " & _
          "end ContactEmail, " & _
            "case " & _
                  "When strContactPrefix is null then '-' " & _
                  "else strContactPrefix " & _
                  "end strContactPrefix, " & _
                  "case " & _
                  "when strContactFirstName is null then '-' " & _
                  "else strContactFirstName " & _
                  "end strContactFirstName, " & _
                  "case " & _
                  "When strContactLastName is null then '-' " & _
                  "else strContactLastName " & _
                  "end strContactLastName, " & _
          "case " & _
         "when strDMUResponsibleStaff is null then '-' " & _
          "else strDMUResponsibleStaff " & _
           "end strDMUResponsibleStaff " & _
          "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
          "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
          "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " & _
          "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
          "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
          "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
          "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
          "and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
          "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
          "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
          "and AIRBranch.EIS_Admin.Active = '1' " & _
          "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
          "and AIRbranch.EIS_Admin.strEnrollment = '1'  " & _
          "and (AIRbranch.EIS_Admin.eisstatuscode = '3' or AIRbranch.EIS_Admin.eisstatuscode = 4) " & _
          "and (strOptOut = '1' or stroptout is null ) " & _
          "and  NOT  exists (Select * from AIRBranch.EIS_QAAdmin " & _
          "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
          "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID) "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = False
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = False
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If


                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Opted-Out, To-do Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsOptedOutBegan_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsOptedOutBegan.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", " and (strOptOut = '1' or strOptout is null) ", _
                     " and EISStatusCode >= 3 ", "", " and QAStatusCode is not null and datQAComplete is null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Opted-Out, Started Count"

            Exit Sub


            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
            "'False' as ID, " & _
            " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
           "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
           "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
           "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
           "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
           "case " & _
           "when strOptOut = '1' then 'Yes' " & _
           "when strOptOut = '0' then 'No' " & _
           "else '' " & _
           "End strOptOut, " & _
           "case " & _
           "when strMailout = '1' then 'Yes' " & _
           "else 'No' " & _
           "end strMailout, " & _
           "case " & _
           "when strContactEmail is null then '-' " & _
           "else strContactEmail " & _
           "end ContactEmail, " & _
             "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
           "case " & _
          "when strDMUResponsibleStaff is null then '-' " & _
           "else strDMUResponsibleStaff " & _
            "end strDMUResponsibleStaff " & _
           "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
           "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
           "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " & _
           "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
           "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
           "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
           "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
                      "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
            "and AIRBranch.EIS_Admin.Active = '1' " & _
           "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
          "and AIRbranch.EIS_Admin.strEnrollment = '1'  " & _
   "and (AIRbranch.EIS_Admin.eisstatuscode = '3' or AIRbranch.EIS_Admin.eisstatuscode = '4')   " & _
 "and (strOptOut = '1' or strOptout is null) " & _
 "and  (not  exists (Select * from AIRBranch.EIS_QAAdmin " & _
 "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
 "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
 "and datQAComplete is null )   " & _
 "or  exists (Select * from AIRBranch.EIS_QAAdmin " & _
 "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
 "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
 "and datQAComplete is null ))"



            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = False
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = False
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If


                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Opted-Out, Started Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsOptedOutSubmittedToEPA_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsOptedOutSubmittedToEPA.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", " and (strOptOut = '1' or strOptout is null )  ", _
               " and EISStatusCode >= 5 ", "", " and datQAComplete is not null ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, EPA Submitted Count"

            Exit Sub

            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
            "'False' as ID, " & _
            " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
           "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
           "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
           "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
           "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
           "case " & _
           "when strOptOut = '1' then 'Yes' " & _
           "when strOptOut = '0' then 'No' " & _
           "else '' " & _
           "End strOptOut, " & _
           "case " & _
           "when strMailout = '1' then 'Yes' " & _
           "else 'No' " & _
           "end strMailout, " & _
           "case " & _
           "when strContactEmail is null then '-' " & _
           "else strContactEmail " & _
           "end ContactEmail, " & _
             "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
           "case " & _
          "when strDMUResponsibleStaff is null then '-' " & _
           "else strDMUResponsibleStaff " & _
            "end strDMUResponsibleStaff " & _
           "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
           "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
           "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " & _
           "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
           "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
           "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
           "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
                      "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
            "and AIRBranch.EIS_Admin.Active = '1' " & _
           "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
          "and AIRbranch.EIS_Admin.strEnrollment = '1'  " & _
 "and AIRbranch.EIS_Admin.eisstatuscode = '5' " & _
 "and (strOptOut = '1' or strOptout is null ) " & _
 "and  (not  exists (Select * from AIRBranch.EIS_QAAdmin " & _
 "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
 "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
  "and datQAComplete is not null " & _
  "or " & _
 "exists (Select * from AIRBranch.EIS_QAAdmin " & _
 "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
 "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
  "and datQAComplete is not null )" & _
  " )) "


            '           "and eisstatuscode = '5'  " & _
            '"and (strOptOut = '1' or strOptout is null ) " & _
            '"and  (not  exists (Select * from AIRBranch.EIS_QAAdmin " & _
            '"where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
            '"and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID) " & _
            '"OR " & _
            '"exists (Select * from AIRBranch.EIS_QAAdmin " & _
            '"where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
            '"and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
            ' "and datQAComplete is not null )" & _
            ' " ) ) "


            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = False
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = False
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If


                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Opted-Out, EPA Submitted Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbSearchForFacility_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbSearchForFacility.LinkClicked
        Try
            If cboEISStatisticsYear.Text = "" Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Select " & _
                  "strFacilityName, " & _
                  "strContactCompanyName, strContactAddress1, " & _
                  "strContactAddress2, strContactCity, " & _
                  "strcontactstate, strcontactzipCode, " & _
                  "strcontactFirstName, strcontactLastName, " & _
                  "strContactPrefix, strContactEmail, " & _
                  "stroperationalStatus, strClass, " & _
                  "strcomment, UpdateUser, " & _
                  "updateDateTime, CreateDateTime " & _
                   "from AIRBranch.EIS_Mailout " & _
                   "where intInventoryyear = '" & cboEISStatisticsYear.Text & "' " & _
                   "and FacilitySiteID = '" & txtEISStatsMailoutAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtEISStatsMailoutFacilityName.Clear()
                Else
                    txtEISStatsMailoutFacilityName.Text = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtEISStatsMailoutCompanyName.Clear()
                Else
                    txtEISStatsMailoutCompanyName.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtEISStatsMailoutAddress1.Clear()
                Else
                    txtEISStatsMailoutAddress1.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactAddress2")) Then
                    txtEISStatsMailoutAddress2.Clear()
                Else
                    txtEISStatsMailoutAddress2.Text = dr.Item("strContactAddress2")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtEISStatsMailoutCity.Clear()
                Else
                    txtEISStatsMailoutCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strcontactstate")) Then
                    txtEISStatsMailoutState.Clear()
                Else
                    txtEISStatsMailoutState.Text = dr.Item("strcontactstate")
                End If
                If IsDBNull(dr.Item("strcontactzipCode")) Then
                    txtEISStatsMailoutZipCode.Clear()
                Else
                    txtEISStatsMailoutZipCode.Text = dr.Item("strcontactzipCode")
                End If
                If IsDBNull(dr.Item("strcontactFirstName")) Then
                    txtEISStatsMailoutFirstName.Clear()
                Else
                    txtEISStatsMailoutFirstName.Text = dr.Item("strcontactFirstName")
                End If
                If IsDBNull(dr.Item("strcontactLastName")) Then
                    txtEISStatsMailoutLastName.Clear()
                Else
                    txtEISStatsMailoutLastName.Text = dr.Item("strcontactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtEISStatsMailoutPrefix.Clear()
                Else
                    txtEISStatsMailoutPrefix.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtEISStatsMailoutEmailAddress.Clear()
                Else
                    txtEISStatsMailoutEmailAddress.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strcomment")) Then
                    txtEISStatsMailoutComments.Clear()
                Else
                    txtEISStatsMailoutComments.Text = dr.Item("strcomment")
                End If
                If IsDBNull(dr.Item("UpdateUser")) Then
                    txtEISStatsMailoutUpdateUser.Clear()
                Else
                    txtEISStatsMailoutUpdateUser.Text = dr.Item("UpdateUser")
                End If
                If IsDBNull(dr.Item("updateDateTime")) Then
                    txtEISStatsMailoutUpdateDate.Clear()
                Else
                    txtEISStatsMailoutUpdateDate.Text = dr.Item("updateDateTime")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    txtEISStatsMailoutCreateDate.Clear()
                Else
                    txtEISStatsMailoutCreateDate.Text = dr.Item("CreateDateTime")
                End If

            End While
            dr.Close()

            If txtEISStatsMailoutFacilityName.Text = "" Then
                SQL = "Select * from " & _
                "(Select dt_EIcontact.STRairsnumber, " & DBNameSpace & ".APBFacilityinformation.STRFACILITYNAME, " & _
                "" & DBNameSpace & ".APBHEADERDATA.stroperationalstatus, " & DBNameSpace & ".APBHEADERDATA.STRCLASS, " & _
                "(Case " & _
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRContactLastName " & _
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRContactLastName " & _
                "Else '' " & _
                "END) STRContactLastName, " & _
                "(Case " & _
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRContactfirstName " & _
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRContactfirstName " & _
                "Else '' " & _
                "END) STRContactfirstName, " & _
                "(Case " & _
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRContactCompanyName " & _
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRContactCompanyName " & _
                "END) STRContactCompanyName, " & _
                "(Case " & _
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRContactEmail " & _
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRContactEmail " & _
                "END) STRContactEmail, " & _
                "(Case " & _
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRCONTACTPREFIX " & _
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTPREFIX " & _
                "END) strCONTACTPREFIX, " & _
                "(Case " & _
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRCONTACTADDRESS1 " & _
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTADDRESS1 " & _
                "END) STRCONTACTADDRESS1, " & _
                "(Case " & _
                "When dt_EIContact.STRKEY='41' THEN dt_EIContact.STRCONTACTCITY " & _
                "When dt_EIContact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTCITY " & _
                "END) STRCONTACTCITY, " & _
                "(Case " & _
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRCONTACTSTATE " & _
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTSTATE " & _
                "END) STRCONTACTSTATE,  " & _
                "(Case " & _
                "When dt_EIcontact.STRKEY='41' THEN dt_EIcontact.STRCONTACTZIPCODE " & _
                "When dt_EIcontact.STRKEY Is Null THEN dt_PermitContact.STRCONTACTZIPCODE " & _
                "END) STRCONTACTZIPCODE " & _
                "From " & _
                "(Select DISTINCT dt_eIlist.STRAIRSNUMBER, dt_contact.STRKEY,  " & _
                "dt_Contact.STRCONTACTLASTNAME, dt_Contact.STRCONTACTFIRSTNAME, " & _
                "dt_Contact.STRContactCompanyName, dt_Contact.STRContactEmail,  " & _
                "dt_Contact.STRCONTACTPREFIX, dt_Contact.STRCONTACTADDRESS1, dt_Contact.STRCONTACTCITY,  " & _
                "dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE " & _
                "FROM " & _
                "(Select * FROM " & DBNameSpace & ".APBHEADERDATA " & _
                "where (stroperationalstatus = 'O' OR stroperationalstatus = 'P' oR stroperationalstatus = 'C') AND  " & _
                "(STRCLASS = 'A')   " & _
                ") dt_EIList,      " & _
                "(Select * From " & DBNameSpace & ".APBCONTACTINFORMATION where STRKEY=41) dt_Contact " & _
                "Where dt_EIList.STRAIRSNUMBEr = dt_Contact.STRAIRSNUMBER (+)) dt_EIContact, " & _
                "(Select DISTINCT dt_eIlist.STRAIRSNUMBER, dt_contact.STRKEY,  " & _
                "dt_Contact.STRCONTACTLASTNAME, dt_Contact.STRCONTACTFIRSTNAME, " & _
                "dt_Contact.STRContactCompanyName, dt_Contact.STRContactEmail, dt_Contact.STRCONTACTPREFIX,  " & _
                "dt_Contact.STRCONTACTADDRESS1, dt_Contact.strcontactcity, dt_Contact.STRCONTACTSTATE, dt_Contact.STRCONTACTZIPCODE " & _
                "FROM " & _
                "(Select * FROM " & DBNameSpace & ".APBHEADERDATA " & _
                "where (stroperationalstatus = 'O' OR stroperationalstatus = 'P' oR stroperationalstatus = 'C') AND  " & _
                "(STRCLASS = 'A')   " & _
                ") dt_EIList,      " & _
                "(Select * From " & DBNameSpace & ".APBCONTACTINFORMATION where STRKEY=30) dt_Contact " & _
                "Where dt_EIList.STRAIRSNUMBEr = dt_Contact.STRAIRSNUMBER (+)) dt_PermitContact, " & _
                "" & DBNameSpace & ".APBFACILITYINFORMATION, " & _
                "" & DBNameSpace & ".APBHEADERDATA " & _
                "Where " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER= dt_EIContact.STRAIRSNumber and  " & _
                "" & DBNameSpace & ".APBHEADERDATA.STRAIRSNUMBER= dt_EIContact.STRAIRSNumber and  " & _
                "dt_EIContact.STRAIRSNumber  = dt_PermitContact.STRAIRSNUMBER (+) ) " & _
                "where strAIRSnumber = '0413" & txtEISStatsMailoutAIRSNumber.Text & "' "


                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        txtEISStatsMailoutFacilityName.Clear()
                    Else
                        txtEISStatsMailoutFacilityName.Text = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strContactCompanyName")) Then
                        txtEISStatsMailoutCompanyName.Clear()
                    Else
                        txtEISStatsMailoutCompanyName.Text = dr.Item("strContactCompanyName")
                    End If
                    If IsDBNull(dr.Item("strContactAddress1")) Then
                        txtEISStatsMailoutAddress1.Clear()
                    Else
                        txtEISStatsMailoutAddress1.Text = dr.Item("strContactAddress1")
                    End If
                    'If IsDBNull(dr.Item("strContactAddress2")) Then
                    txtEISStatsMailoutAddress2.Clear()
                    'Else
                    '    txtEISStatsMailoutAddress2.Text = dr.Item("strContactAddress2")
                    'End If
                    If IsDBNull(dr.Item("strContactCity")) Then
                        txtEISStatsMailoutCity.Clear()
                    Else
                        txtEISStatsMailoutCity.Text = dr.Item("strContactCity")
                    End If
                    If IsDBNull(dr.Item("strcontactstate")) Then
                        txtEISStatsMailoutState.Clear()
                    Else
                        txtEISStatsMailoutState.Text = dr.Item("strcontactstate")
                    End If
                    If IsDBNull(dr.Item("strcontactzipCode")) Then
                        txtEISStatsMailoutZipCode.Clear()
                    Else
                        txtEISStatsMailoutZipCode.Text = dr.Item("strcontactzipCode")
                    End If
                    If IsDBNull(dr.Item("strcontactFirstName")) Then
                        txtEISStatsMailoutFirstName.Clear()
                    Else
                        txtEISStatsMailoutFirstName.Text = dr.Item("strcontactFirstName")
                    End If
                    If IsDBNull(dr.Item("strcontactLastName")) Then
                        txtEISStatsMailoutLastName.Clear()
                    Else
                        txtEISStatsMailoutLastName.Text = dr.Item("strcontactLastName")
                    End If
                    If IsDBNull(dr.Item("strContactPrefix")) Then
                        txtEISStatsMailoutPrefix.Clear()
                    Else
                        txtEISStatsMailoutPrefix.Text = dr.Item("strContactPrefix")
                    End If
                    If IsDBNull(dr.Item("strContactEmail")) Then
                        txtEISStatsMailoutEmailAddress.Clear()
                    Else
                        txtEISStatsMailoutEmailAddress.Text = dr.Item("strContactEmail")
                    End If
                    'If IsDBNull(dr.Item("strcomment")) Then
                    txtEISStatsMailoutComments.Clear()
                    'Else
                    '    txtEISStatsMailoutComments.Text = dr.Item("strcomment")
                    'End If
                    If IsDBNull(dr.Item("UpdateUser")) Then
                        txtEISStatsMailoutUpdateUser.Clear()
                    Else
                        txtEISStatsMailoutUpdateUser.Text = dr.Item("UpdateUser")
                    End If
                    If IsDBNull(dr.Item("updateDateTime")) Then
                        txtEISStatsMailoutUpdateDate.Clear()
                    Else
                        txtEISStatsMailoutUpdateDate.Text = dr.Item("updateDateTime")
                    End If
                    If IsDBNull(dr.Item("CreateDateTime")) Then
                        txtEISStatsMailoutCreateDate.Clear()
                    Else
                        txtEISStatsMailoutCreateDate.Text = dr.Item("CreateDateTime")
                    End If

                End While
                dr.Close()
                btnAddtoEISMailout.Visible = True

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnAddtoEISMailout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddtoEISMailout.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to add facilies into Mailout.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                temp = ""

                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True And dgvEISStats(7, i).Value = "No" Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next

                If temp <> "" Then
                    temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "



                    SQL = "Update AIRBranch.EIS_Admin set " & _
                    "strMailOut = '1' " & _
                    "where inventoryYear = '" & EISConfirm & "' " & _
                    temp

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteReader()
                    MsgBox(EISConfirm & " Emission Inventory Facilities in Mailout.", MsgBoxStyle.Information, Me.Text)
                End If

            Else
                MsgBox("Year does not match selected EIS year")

            End If



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEISComplete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISComplete.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to move Facilities into the QA process.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                temp = ""
                For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                    If dgvEISStats(0, i).Value = True Then
                        temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                    End If
                Next
                temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "

                SQL = "Update AIRBranch.EIS_Admin set " & _
                "EISAccessCode = '0', " & _
                "EISStatusCode = '5', " & _
                "datEISstatus = sysdate, " & _
                "UpdateUser = '" & Replace(UserName, "'", "''") & "', " & _
                "updatedatetime = sysdate " & _
                "where inventoryYear = '" & EISConfirm & "' " & _
                temp

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                MsgBox(EISConfirm & " EIS process completed.", MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("Year does not match selected EIS year")

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub txtQAComments_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If e.KeyChar = Microsoft.VisualBasic.ChrW(1) Then
                txtQAComments.SelectAll()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Sub ViewPollutantThresholds()
        Try
            Dim dsThreshold As DataSet
            Dim daThreshold As OracleDataAdapter

            If rdbThreeYearPollutants.Checked = True Then
                SQL = "Select " & _
                "strPollutant, numThreshold, " & _
                "numThresholdNAA " & _
                "from AIRbranch.EIThresholds " & _
                "where strType = '3YEAR' " & _
                "order by strPollutant "
            Else
                SQL = "Select " & _
                "strPollutant, numThreshold, " & _
                "numThresholdNAA " & _
                "from AIRbranch.EIThresholds " & _
                "where strType = 'ANNUAL' " & _
                "order by strPollutant "
            End If

            dsThreshold = New DataSet
            daThreshold = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daThreshold.Fill(dsThreshold, "ThresholdPollutants")
            dgvThresholdPollutants.DataSource = dsThreshold
            dgvThresholdPollutants.DataMember = "ThresholdPollutants"

            dgvThresholdPollutants.RowHeadersVisible = False
            dgvThresholdPollutants.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvThresholdPollutants.AllowUserToResizeColumns = True
            dgvThresholdPollutants.AllowUserToAddRows = False
            dgvThresholdPollutants.AllowUserToDeleteRows = False
            dgvThresholdPollutants.AllowUserToOrderColumns = True
            dgvThresholdPollutants.AllowUserToResizeRows = True

            dgvThresholdPollutants.Columns("strPollutant").HeaderText = "Pollutant"
            dgvThresholdPollutants.Columns("strPollutant").DisplayIndex = 0
            dgvThresholdPollutants.Columns("numThreshold").HeaderText = "Threshold"
            dgvThresholdPollutants.Columns("numThreshold").DisplayIndex = 1
            dgvThresholdPollutants.Columns("numThresholdNAA").HeaderText = "NonAttainment Area Threshold"
            dgvThresholdPollutants.Columns("numThresholdNAA").DisplayIndex = 2

            '   txtRecordNumber.Text = dgvESDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewThresholdPollutants_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewThresholdPollutants.LinkClicked
        Try
            ViewPollutantThresholds()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvThresholdPollutants_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvThresholdPollutants.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvThresholdPollutants.HitTest(e.X, e.Y)

        Try
            If dgvThresholdPollutants.RowCount > 0 And hti.RowIndex <> -1 Then
                If IsDBNull(dgvThresholdPollutants(0, hti.RowIndex).Value) Then
                    txtPollutant.Clear()
                Else
                    txtPollutant.Text = dgvThresholdPollutants(0, hti.RowIndex).Value
                End If
                If IsDBNull(dgvThresholdPollutants(1, hti.RowIndex).Value) Then
                    txtThreshold.Clear()
                Else
                    txtThreshold.Text = dgvThresholdPollutants(1, hti.RowIndex).Value
                End If
                If IsDBNull(dgvThresholdPollutants(2, hti.RowIndex).Value) Then
                    txtNonAttainmentThreshold.Clear()
                Else
                    txtNonAttainmentThreshold.Text = dgvThresholdPollutants(2, hti.RowIndex).Value
                End If
            Else
                txtPollutant.Clear()
                txtThreshold.Clear()
                txtNonAttainmentThreshold.Clear()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnAddNewPollutant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewPollutant.Click
        Try
            Dim ThresholdType As String = ""

            If rdbAnnualPollutants.Checked = True Then
                ThresholdType = "ANNUAL"
            End If
            If rdbThreeYearPollutants.Checked = True Then
                ThresholdType = "3YEAR"
            End If
            If ThresholdType = "" Then
                MsgBox("Select either an Annual or 3 Year threshold type." & vbCrLf & "No Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
            If txtPollutant.Text = "" Then
                Exit Sub
            End If

            SQL = "Select * from " & _
            "airbranch.EIThresholds " & _
            "where upper(strPollutant) = '" & Replace(txtPollutant.Text.ToUpper, "'", "''") & "' " & _
            "and strType = '" & ThresholdType & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read
            If recExist = True Then
                MsgBox("Pollutant currently exists for selected Type." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            Else
                SQL = "Insert into AIRBranch.EIThresholds " & _
                "values " & _
                "('" & Replace(txtPollutant.Text, "'", "''") & "', " & _
                "'" & txtThreshold.Text & "', " & _
                "'" & txtNonAttainmentThreshold.Text & "', " & _
                "'" & ThresholdType & "') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()
                ViewPollutantThresholds()
                MsgBox("Data Added", MsgBoxStyle.Information, Me.Text)

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdatePollutant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdatePollutant.Click
        Try

            Dim ThresholdType As String = ""

            If rdbAnnualPollutants.Checked = True Then
                ThresholdType = "ANNUAL"
            End If
            If rdbThreeYearPollutants.Checked = True Then
                ThresholdType = "3YEAR"
            End If
            If ThresholdType = "" Then
                MsgBox("Select either an Annual or 3 Year threshold type." & vbCrLf & "No Data Saved", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
            If txtPollutant.Text = "" Then
                Exit Sub
            End If

            SQL = "Select * from " & _
            "airbranch.EIThresholds " & _
            "where upper(strPollutant) = '" & Replace(txtPollutant.Text.ToUpper, "'", "''") & "' " & _
            "and strType = '" & ThresholdType & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read
            If recExist = True Then
                SQL = "Update AIRBranch.EIThresholds set " & _
                      "numThreshold = '" & txtThreshold.Text & "', " & _
                      "numThresholdNAA = '" & txtNonAttainmentThreshold.Text & "' " & _
                      "where strType = '" & ThresholdType & "'  " & _
                      "and strPollutant =  '" & Replace(txtPollutant.Text, "'", "''") & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()
                ViewPollutantThresholds()
                MsgBox("Data Updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Pollutant currently does not exists for selected Type." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeletePollutant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletePollutant.Click

    End Sub
    Sub LoadEISYear()
        Try
            SQL = "Select " & _
            "strYear, " & _
            "strEIType, datDeadLine " & _
            "from AIRBranch.EIThresholdYears " & _
            "order by strYear desc "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            da.Fill(ds, "EISYears")
            dgvEISYear.DataSource = ds
            dgvEISYear.DataMember = "EISYears"

            dgvEISYear.RowHeadersVisible = False
            dgvEISYear.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvEISYear.AllowUserToResizeColumns = True
            dgvEISYear.AllowUserToAddRows = False
            dgvEISYear.AllowUserToDeleteRows = False
            dgvEISYear.AllowUserToOrderColumns = True
            dgvEISYear.AllowUserToResizeRows = True

            dgvEISYear.Columns("strYear").HeaderText = "EIS Year"
            dgvEISYear.Columns("strYear").DisplayIndex = 0
            dgvEISYear.Columns("strEIType").HeaderText = "Type"
            dgvEISYear.Columns("strEIType").DisplayIndex = 1
            dgvEISYear.Columns("datDeadLine").HeaderText = "EIS Date Deadline"
            dgvEISYear.Columns("datDeadLine").DisplayIndex = 2
            dgvEISYear.Columns("datDeadLine").DefaultCellStyle.Format = "dd-MMM-yyyy"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvEISYear_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvEISYear.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvEISYear.HitTest(e.X, e.Y)

        Try
            If dgvEISYear.RowCount > 0 And hti.RowIndex <> -1 Then
                If IsDBNull(dgvEISYear(0, hti.RowIndex).Value) Then
                    mtbThresholdYear.Clear()
                Else
                    mtbThresholdYear.Text = dgvEISYear(0, hti.RowIndex).Value
                End If
                If IsDBNull(dgvEISYear(1, hti.RowIndex).Value) Then
                    rdbEISAnnual.Checked = False
                    rdbEISThreeYear.Checked = False
                Else
                    If dgvEISYear(1, hti.RowIndex).Value = "3YEAR" Then
                        rdbEISAnnual.Checked = False
                        rdbEISThreeYear.Checked = True
                    Else
                        rdbEISAnnual.Checked = True
                        rdbEISThreeYear.Checked = False
                    End If
                End If
                If IsDBNull(dgvEISYear(2, hti.RowIndex).Value) Then
                    dtpEISDeadline.Text = OracleDate
                Else
                    dtpEISDeadline.Text = dgvEISYear(2, hti.RowIndex).Value
                End If
            Else
                mtbThresholdYear.Clear()
                rdbEISAnnual.Checked = False
                rdbEISThreeYear.Checked = False
                dtpEISDeadline.Text = OracleDate
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbClearEISYear_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbClearEISYear.LinkClicked
        Try

            mtbThresholdYear.Clear()
            rdbEISAnnual.Checked = False
            rdbEISThreeYear.Checked = False
            dtpEISDeadline.Text = OracleDate

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddEISYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEISYear.Click
        Try
            Dim EISYearType As String = ""

            If mtbThresholdYear.Text.Length <> 4 Then
                MsgBox("Bad Year" & vbCrLf & "No Data Saved", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If rdbEISThreeYear.Checked = True Then
                EISYearType = "3YEAR"
            Else
                EISYearType = "ANNUAL"
            End If

            SQL = "Select " & _
            "strYear " & _
            "from AIRBranch.EIThresholdYears " & _
            "where strYEar = '" & Replace(mtbThresholdYear.Text, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read
            If recExist = True Then
                MsgBox("EIS Year currently exists." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            Else
                SQL = "Insert into AIRBranch.EIThresholdYears " & _
                "values " & _
                "('" & Replace(mtbThresholdYear.Text, "'", "''") & "', " & _
                "'" & EISYearType & "', " & _
                "'" & dtpEISDeadline.Text & "')  "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()
                LoadEISYear()
                MsgBox("Data Added", MsgBoxStyle.Information, Me.Text)

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateEISYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateEISYear.Click
        Try
            Dim EISYearType As String = ""

            If mtbThresholdYear.Text.Length <> 4 Then
                MsgBox("Bad Year" & vbCrLf & "No Data Saved", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If rdbEISThreeYear.Checked = True Then
                EISYearType = "3YEAR"
            Else
                EISYearType = "ANNUAL"
            End If

            SQL = "Select " & _
            "strYear " & _
            "from AIRBranch.EIThresholdYears " & _
            "where strYEar = '" & Replace(mtbThresholdYear.Text, "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read
            If recExist = True Then
                SQL = "Update AIRBranch.EIThresholdYears set " & _
                "strEIType = '" & EISYearType & "', " & _
                "DatDeadline = '" & dtpEISDeadline.Text & "'  " & _
                "where strYear = '" & mtbThresholdYear.Text & "'"

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()
                LoadEISYear()
                MsgBox("Data Updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("EIS Year does not currently exists." & vbCrLf & "No data Saved", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteEISYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteEISYear.Click
        Try


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearInactiveData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearInactiveData.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to delete inactive data.", Me.Text)

            If EISConfirm = txtSelectedEISStatYear.Text Then
                SQL = "delete airbranch.EIS_UnitControlPollutant " & _
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_UnitControlMeasure  " & _
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_UnitControlApproach  " & _
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_RPGEOCoordinates  " & _
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_RPApportionment  " & _
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_ProcessControlPollutant " & _
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_ProcessControlMeasure " & _
                "where active = '0'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_ProcessControlApproach  " & _
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_ReportingPeriodEmissions  " & _
              "where active = '0'  " & _
              "and intinventoryyear = '2010' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_ProcessOperatingDetails  " & _
                "where active = '0'  " & _
                "and intInventoryYear = '2010' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_ProcessRPTPeriodSCP  " & _
                "where Active = '0'  " & _
                "and intInventoryYear = '2010'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete Airbranch.EIS_RPApportionment " & _
             "where exists (select * " & _
             "from Airbranch.eis_Process " & _
             "where active = '0' " & _
             "and Airbranch.EIS_RPApportionment.facilitysiteid = Airbranch.eis_Process.facilitysiteid " & _
             "and Airbranch.EIS_RPApportionment.ProcessId = Airbranch.eis_Process.ProcessId " & _
             "and Airbranch.EIS_RPApportionment.EmissionsUnitID  = Airbranch.eis_Process.EmissionsUnitID) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = " delete Airbranch.EIS_ProcessControlPollutant " & _
                " where exists (select *  " & _
                " from Airbranch.EIS_ProcessControlApproach, airbranch.EIS_Process   " & _
                " where   Airbranch.EIS_ProcessControlPollutant.facilitysiteid = Airbranch.EIS_ProcessControlApproach.facilitysiteid " & _
                " and Airbranch.EIS_ProcessControlPollutant.ProcessId = Airbranch.EIS_ProcessControlApproach.ProcessId   " & _
                " and Airbranch.EIS_ProcessControlPollutant.EmissionsUnitID  = Airbranch.EIS_ProcessControlApproach.EmissionsUnitID " & _
                "and  Airbranch.EIS_ProcessControlPollutant.facilitysiteid = Airbranch.EIS_Process.facilitysiteid " & _
                " and Airbranch.EIS_ProcessControlPollutant.ProcessId = Airbranch.EIS_Process.ProcessId   " & _
                " and Airbranch.EIS_ProcessControlPollutant.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID  " & _
                " and EIS_Process.active = '0' ) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete Airbranch.EIS_ProcessControlMeasure  " & _
             "where exists (select * " & _
             "from  airbranch.EIS_Process  " & _
             "where   Airbranch.EIS_ProcessControlMeasure.facilitysiteid = Airbranch.EIS_Process.facilitysiteid " & _
             "and Airbranch.EIS_ProcessControlMeasure.ProcessId = Airbranch.EIS_Process.ProcessId  " & _
             "and Airbranch.EIS_ProcessControlMeasure.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID " & _
             "and EIS_Process.active = '0' ) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete Airbranch.EIS_ProcessControlApproach " & _
                "where exists (select * " & _
                "from Airbranch.eis_Process " & _
                "where active = '0' " & _
                "and Airbranch.EIS_ProcessControlApproach.facilitysiteid = Airbranch.eis_Process.facilitysiteid " & _
                "and Airbranch.EIS_ProcessControlApproach.ProcessId = Airbranch.eis_Process.ProcessId " & _
                "and Airbranch.EIS_ProcessControlApproach.EmissionsUnitID  = Airbranch.eis_Process.EmissionsUnitID) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete Airbranch.EIS_ProcessControlPollutant " & _
                "where exists (select * " & _
                "from Airbranch.EIS_ProcessControlApproach " & _
                "where active = '0' " & _
                "and Airbranch.EIS_ProcessControlPollutant.facilitysiteid = Airbranch.EIS_ProcessControlApproach.facilitysiteid " & _
                "and Airbranch.EIS_ProcessControlPollutant.ProcessId = Airbranch.EIS_ProcessControlApproach.ProcessId " & _
                "and Airbranch.EIS_ProcessControlPollutant.EmissionsUnitID  = Airbranch.EIS_ProcessControlApproach.EmissionsUnitID) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete  Airbranch.EIS_ProcessOperatingDetails   " & _
                "where exists (select * " & _
                "from Airbranch.EIS_Process  " & _
                "where active = '0'  " & _
                "and Airbranch.EIS_ProcessOperatingDetails.facilitysiteid = Airbranch.EIS_Process.facilitysiteid  " & _
                "and Airbranch.EIS_ProcessOperatingDetails.ProcessId = Airbranch.EIS_Process.ProcessId  " & _
                "and Airbranch.EIS_ProcessOperatingDetails.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID)  "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete  Airbranch.EIS_ReportingPeriodEmissions   " & _
                "where exists (select * " & _
                "from Airbranch.EIS_Process  " & _
                "where active = '0'  " & _
                "and Airbranch.EIS_ReportingPeriodEmissions.facilitysiteid = Airbranch.EIS_Process.facilitysiteid  " & _
                "and Airbranch.EIS_ReportingPeriodEmissions.ProcessId = Airbranch.EIS_Process.ProcessId  " & _
                "and Airbranch.EIS_ReportingPeriodEmissions.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID)  "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete  Airbranch.EIS_ProcessRPTPeriodSCP   " & _
                 "where exists (select * " & _
                 "from Airbranch.EIS_Process  " & _
                 "where active = '0'  " & _
                 "and Airbranch.EIS_ProcessRPTPeriodSCP.facilitysiteid = Airbranch.EIS_Process.facilitysiteid  " & _
                 "and Airbranch.EIS_ProcessRPTPeriodSCP.ProcessId = Airbranch.EIS_Process.ProcessId  " & _
                 "and Airbranch.EIS_ProcessRPTPeriodSCP.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID)  "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete Airbranch.eis_processReportingPeriod   " & _
                 "where exists (select * " & _
                 "from  airbranch.EIS_Process  " & _
                 "where   Airbranch.eis_processReportingPeriod.facilitysiteid = Airbranch.EIS_Process.facilitysiteid " & _
                 "and Airbranch.eis_processReportingPeriod.ProcessId = Airbranch.EIS_Process.ProcessId  " & _
                 "and Airbranch.eis_processReportingPeriod.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID " & _
                 "and EIS_Process.active = '0' ) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_Process  " & _
                              "where Active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "Delete airbranch.EIS_EmissionsUnit   " & _
                "where active = '0' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                SQL = "delete airbranch.EIS_Releasepoint  " & _
                "where active = '0'  " & _
                "and numRPStatusCodeYear = '2010' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

                MsgBox(EISConfirm & " Emission Inventory Year Inactive data deleted.", MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox("Year does not match selected EIS year")

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub mtbEILogAIRSNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mtbEILogAIRSNumber.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            Else
                Exit Sub
            End If

            If cboEILogYear.Text = "" Or cboEILogYear.Text.Length <> 4 Then
                MsgBox("Please select a valid year from the EIS Year dropdown.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If mtbEILogAIRSNumber.Text = "" Or mtbEILogAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please enter a valid AIRS # into the EIS AIRS #", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            txtEILogSelectedYear.Text = cboEILogYear.Text
            txtEILogSelectedAIRSNumber.Text = mtbEILogAIRSNumber.Text

            LoadAdminData()

            SQL = "select  " & _
            "strFacilitySiteName " & _
            "from " & DBNameSpace & ".EIS_FacilitySite " & _
            "where FacilitySiteId = '" & txtEILogSelectedAIRSNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFacilitySiteName")) Then
                    txtEIModifyFacilityName.Clear()
                    txtEILogFacilityName.Clear()
                Else
                    txtEIModifyFacilityName.Text = dr.Item("strFacilitySiteName")
                    txtEILogFacilityName.Text = dr.Item("strFacilitySiteName")
                End If
            End While
            dr.Close()

            SQL = "select  " & _
            "* " & _
            "from " & DBNameSpace & ".EIS_FacilitySiteAddress " & _
            "where FacilitySiteId = '" & txtEILogSelectedAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strLocationAddressText")) Then
                    txtEIModifyLocation.Clear()
                Else
                    txtEIModifyLocation.Text = dr.Item("strLocationAddressText")
                End If
                If IsDBNull(dr.Item("strLocalityName")) Then
                    txtEIModifyCity.Clear()
                Else
                    txtEIModifyCity.Text = dr.Item("strLocalityName")
                End If
                If IsDBNull(dr.Item("strLocationAddressPostalCode")) Then
                    mtbEIModifyZipCode.Clear()
                Else
                    mtbEIModifyZipCode.Text = dr.Item("strLocationAddressPostalCode")
                End If
            End While
            dr.Close()

            SQL = "select  " & _
            "* " & _
            "from " & DBNameSpace & ".EIS_FacilityGeoCoord " & _
            "where FacilitySiteId = '" & txtEILogSelectedAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("numLatitudeMeasure")) Then
                    mtbEIModifyLatitude.Clear()
                Else
                    mtbEIModifyLatitude.Text = dr.Item("numLatitudeMeasure")
                End If
                If IsDBNull(dr.Item("numLongitudeMeasure")) Then
                    mtbEIModifyLongitude.Clear()
                Else
                    mtbEIModifyLongitude.Text = dr.Item("numLongitudeMeasure")
                End If
            End While
            dr.Close()

            SQL = "select * " & _
            "from " & DBNameSpace & ".APBFacilityInformation " & _
            "where strAIRSNumber = '0413" & txtEILogSelectedAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtEIModifyIAIPFacilityName.Clear()
                Else
                    txtEIModifyIAIPFacilityName.Text = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    txtEIModifyIAIPLocation.Clear()
                Else
                    txtEIModifyIAIPLocation.Text = dr.Item("strFacilityStreet1")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    txtEIModifyIAIPCity.Clear()
                Else
                    txtEIModifyIAIPCity.Text = dr.Item("strFacilityCity")
                End If
                If IsDBNull(dr.Item("strFacilityZipCode")) Then
                    mtbEIModifyIAIPZipCode.Clear()
                Else
                    mtbEIModifyIAIPZipCode.Text = dr.Item("strFacilityZipCode")
                End If
                If IsDBNull(dr.Item("numFacilityLongitude")) Then
                    mtbEIModifyIAIPLongitude.Clear()
                Else
                    mtbEIModifyIAIPLongitude.Text = dr.Item("numFacilityLongitude")
                End If
                If IsDBNull(dr.Item("numFacilityLatitude")) Then
                    mtbEIModifyIAIPLatitude.Clear()
                Else
                    mtbEIModifyIAIPLatitude.Text = dr.Item("numFacilityLatitude")
                End If
            End While
            dr.Close()

            SQL = "Select * " & _
            "from " & DBNameSpace & ".EIS_Mailout " & _
            "where intInventoryYear = '" & txtEILogSelectedYear.Text & "' " & _
            "and FacilitySiteID = '" & txtEILogSelectedAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFacilityName")) Then
                    txtEISMailoutFacilityName.Clear()
                Else
                    txtEISMailoutFacilityName.Text = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtEISMailoutCompanyName.Clear()
                Else
                    txtEISMailoutCompanyName.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtEISMailoutAddress.Clear()
                Else
                    txtEISMailoutAddress.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactAddress2")) Then
                    txtEISMailoutAddress2.Clear()
                Else
                    txtEISMailoutAddress2.Text = dr.Item("strContactAddress2")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtEISMailoutCity.Clear()
                Else
                    txtEISMailoutCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    txtEISMailoutState.Clear()
                Else
                    txtEISMailoutState.Text = dr.Item("strContactState")
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    txtEISMailoutZipCode.Clear()
                Else
                    txtEISMailoutZipCode.Text = dr.Item("strContactZipCode")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    txtEISMailoutFirstName.Clear()
                Else
                    txtEISMailoutFirstName.Text = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtEISMailoutLastName.Clear()
                Else
                    txtEISMailoutLastName.Text = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtEISMailoutPrefix.Clear()
                Else
                    txtEISMailoutPrefix.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtEISMailoutEmail.Clear()
                Else
                    txtEISMailoutEmail.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strComment")) Then
                    txtEISMailoutComments.Clear()
                Else
                    txtEISMailoutComments.Text = dr.Item("strComment")
                End If
                If IsDBNull(dr.Item("UpdateUser")) Then
                    txtEISMailoutUpdateUser.Clear()
                Else
                    txtEISMailoutUpdateUser.Text = dr.Item("UpdateUser")
                End If
                If IsDBNull(dr.Item("UpdateDateTime")) Then
                    txtEISMailoutUpdateDateTime.Clear()
                Else
                    txtEISMailoutUpdateDateTime.Text = dr.Item("UpdateDateTime")
                End If
                If IsDBNull(dr.Item("CreateDateTime")) Then
                    txtEISMailoutCreateDateTime.Clear()
                Else
                    txtEISMailoutCreateDateTime.Text = dr.Item("CreateDateTime")
                End If
            End While
            dr.Close()

            SQL = "select " & _
            "strContactFirstName, strContactLastName, " & _
            "strContactPrefix, strContactSuffix, " & _
            "strContactTitle, strContactPhoneNumber1, " & _
            "strContactPhoneNumber2, strContactFaxNumber, " & _
            "strContactEmail, strContactCompanyName, " & _
            "strContactAddress1, strContactAddress2, " & _
            "strContactCity, strContactState, " & _
            "strContactZipCode, strContactDescription, " & _
            "datModifingDate, (strLastName||', '||strFirstName) as ModifingPerson " & _
            "from " & DBNameSpace & ".APBContactInformation, " & DBNameSpace & ".EPDUserProfiles " & _
            "where " & DBNameSpace & ".APBContactInformation.strModifingPerson = " & _
            "" & DBNameSpace & ".EPDUserProfiles.numUserID  " & _
            "and strContactKey = '0413" & txtEILogSelectedAIRSNumber.Text & "41' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    txtEISContactFirstName.Clear()
                Else
                    txtEISContactFirstName.Text = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtEISContactLastName.Clear()
                Else
                    txtEISContactLastName.Text = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtEISContactPrefix.Clear()
                Else
                    txtEISContactPrefix.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactSuffix")) Then
                    txtEISContactSuffix.Clear()
                Else
                    txtEISContactSuffix.Text = dr.Item("strContactSuffix")
                End If
                If IsDBNull(dr.Item("strContactTitle")) Then
                    txtEISContactTitle.Clear()
                Else
                    txtEISContactTitle.Text = dr.Item("strContactTitle")
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                    txtEISContactPhone.Clear()
                Else
                    txtEISContactPhone.Text = dr.Item("strContactPhoneNumber1")
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber2")) Then
                    txtEISContactPhone2.Clear()
                Else
                    txtEISContactPhone2.Text = dr.Item("strContactPhoneNumber2")
                End If
                If IsDBNull(dr.Item("strContactFaxNumber")) Then
                    txtEISContactFax.Clear()
                Else
                    txtEISContactFax.Text = dr.Item("strContactFaxNumber")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtEISContactEmail.Clear()
                Else
                    txtEISContactEmail.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtEISContactCompanyName.Clear()
                Else
                    txtEISContactCompanyName.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtEISContactAddress.Clear()
                Else
                    txtEISContactAddress.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactAddress2")) Then
                    txtEISContactAddress2.Clear()
                Else
                    txtEISContactAddress2.Text = dr.Item("strContactAddress2")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtEISContactCity.Clear()
                Else
                    txtEISContactCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    txtEISContactState.Clear()
                Else
                    txtEISContactState.Text = dr.Item("strContactState")
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    txtEISContactZipCode.Clear()
                Else
                    txtEISContactZipCode.Text = dr.Item("strContactZipCode")
                End If
                If IsDBNull(dr.Item("strContactDescription")) Then
                    txtEISContactDescription.Clear()
                Else
                    txtEISContactDescription.Text = dr.Item("strContactDescription")
                End If
                If IsDBNull(dr.Item("ModifingPerson")) Then
                    txtEISContactUpdateUser.Clear()
                Else
                    txtEISContactUpdateUser.Text = dr.Item("ModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    txtEISContactUpdateDateTime.Clear()
                Else
                    txtEISContactUpdateDateTime.Text = dr.Item("datModifingDate")
                End If

            End While
            dr.Close()

            LoadQASpecificData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnLoadEISLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadEISLog.Click
        Try
            If mtbEISLogAIRSNumber.Text <> "" And cboEISStatisticsYear.Text.Length = 4 Then
                mtbEILogAIRSNumber.Text = mtbEISLogAIRSNumber.Text
                cboEILogYear.Text = cboEISStatisticsYear.Text

                txtEILogSelectedYear.Text = cboEILogYear.Text
                txtEILogSelectedAIRSNumber.Text = mtbEILogAIRSNumber.Text

                LoadAdminData()

                SQL = "select  " & _
                "strFacilitySiteName " & _
                "from " & DBNameSpace & ".EIS_FacilitySite " & _
                "where FacilitySiteId = '" & txtEILogSelectedAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilitySiteName")) Then
                        txtEIModifyFacilityName.Clear()
                        txtEILogFacilityName.Clear()
                    Else
                        txtEIModifyFacilityName.Text = dr.Item("strFacilitySiteName")
                        txtEILogFacilityName.Text = dr.Item("strFacilitySiteName")
                    End If
                End While
                dr.Close()

                SQL = "select  " & _
                "* " & _
                "from " & DBNameSpace & ".EIS_FacilitySiteAddress " & _
                "where FacilitySiteId = '" & txtEILogSelectedAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strLocationAddressText")) Then
                        txtEIModifyLocation.Clear()
                    Else
                        txtEIModifyLocation.Text = dr.Item("strLocationAddressText")
                    End If
                    If IsDBNull(dr.Item("strLocalityName")) Then
                        txtEIModifyCity.Clear()
                    Else
                        txtEIModifyCity.Text = dr.Item("strLocalityName")
                    End If
                    If IsDBNull(dr.Item("strLocationAddressPostalCode")) Then
                        mtbEIModifyZipCode.Clear()
                    Else
                        mtbEIModifyZipCode.Text = dr.Item("strLocationAddressPostalCode")
                    End If
                End While
                dr.Close()

                SQL = "select  " & _
                "* " & _
                "from " & DBNameSpace & ".EIS_FacilityGeoCoord " & _
                "where FacilitySiteId = '" & txtEILogSelectedAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("numLatitudeMeasure")) Then
                        mtbEIModifyLatitude.Clear()
                    Else
                        mtbEIModifyLatitude.Text = dr.Item("numLatitudeMeasure")
                    End If
                    If IsDBNull(dr.Item("numLongitudeMeasure")) Then
                        mtbEIModifyLongitude.Clear()
                    Else
                        mtbEIModifyLongitude.Text = dr.Item("numLongitudeMeasure")
                    End If
                End While
                dr.Close()

                SQL = "select * " & _
                "from " & DBNameSpace & ".APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & txtEILogSelectedAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        txtEIModifyIAIPFacilityName.Clear()
                    Else
                        txtEIModifyIAIPFacilityName.Text = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        txtEIModifyIAIPLocation.Clear()
                    Else
                        txtEIModifyIAIPLocation.Text = dr.Item("strFacilityStreet1")
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        txtEIModifyIAIPCity.Clear()
                    Else
                        txtEIModifyIAIPCity.Text = dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        mtbEIModifyIAIPZipCode.Clear()
                    Else
                        mtbEIModifyIAIPZipCode.Text = dr.Item("strFacilityZipCode")
                    End If
                    If IsDBNull(dr.Item("numFacilityLongitude")) Then
                        mtbEIModifyIAIPLongitude.Clear()
                    Else
                        mtbEIModifyIAIPLongitude.Text = dr.Item("numFacilityLongitude")
                    End If
                    If IsDBNull(dr.Item("numFacilityLatitude")) Then
                        mtbEIModifyIAIPLatitude.Clear()
                    Else
                        mtbEIModifyIAIPLatitude.Text = dr.Item("numFacilityLatitude")
                    End If
                End While
                dr.Close()

                SQL = "Select * " & _
                "from " & DBNameSpace & ".EIS_Mailout " & _
                "where intInventoryYear = '" & txtEILogSelectedYear.Text & "' " & _
                "and FacilitySiteID = '" & txtEILogSelectedAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        txtEISMailoutFacilityName.Clear()
                    Else
                        txtEISMailoutFacilityName.Text = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strContactCompanyName")) Then
                        txtEISMailoutCompanyName.Clear()
                    Else
                        txtEISMailoutCompanyName.Text = dr.Item("strContactCompanyName")
                    End If
                    If IsDBNull(dr.Item("strContactAddress1")) Then
                        txtEISMailoutAddress.Clear()
                    Else
                        txtEISMailoutAddress.Text = dr.Item("strContactAddress1")
                    End If
                    If IsDBNull(dr.Item("strContactAddress2")) Then
                        txtEISMailoutAddress2.Clear()
                    Else
                        txtEISMailoutAddress2.Text = dr.Item("strContactAddress2")
                    End If
                    If IsDBNull(dr.Item("strContactCity")) Then
                        txtEISMailoutCity.Clear()
                    Else
                        txtEISMailoutCity.Text = dr.Item("strContactCity")
                    End If
                    If IsDBNull(dr.Item("strContactState")) Then
                        txtEISMailoutState.Clear()
                    Else
                        txtEISMailoutState.Text = dr.Item("strContactState")
                    End If
                    If IsDBNull(dr.Item("strContactZipCode")) Then
                        txtEISMailoutZipCode.Clear()
                    Else
                        txtEISMailoutZipCode.Text = dr.Item("strContactZipCode")
                    End If
                    If IsDBNull(dr.Item("strContactFirstName")) Then
                        txtEISMailoutFirstName.Clear()
                    Else
                        txtEISMailoutFirstName.Text = dr.Item("strContactFirstName")
                    End If
                    If IsDBNull(dr.Item("strContactLastName")) Then
                        txtEISMailoutLastName.Clear()
                    Else
                        txtEISMailoutLastName.Text = dr.Item("strContactLastName")
                    End If
                    If IsDBNull(dr.Item("strContactPrefix")) Then
                        txtEISMailoutPrefix.Clear()
                    Else
                        txtEISMailoutPrefix.Text = dr.Item("strContactPrefix")
                    End If
                    If IsDBNull(dr.Item("strContactEmail")) Then
                        txtEISMailoutEmail.Clear()
                    Else
                        txtEISMailoutEmail.Text = dr.Item("strContactEmail")
                    End If
                    If IsDBNull(dr.Item("strComment")) Then
                        txtEISMailoutComments.Clear()
                    Else
                        txtEISMailoutComments.Text = dr.Item("strComment")
                    End If
                    If IsDBNull(dr.Item("UpdateUser")) Then
                        txtEISMailoutUpdateUser.Clear()
                    Else
                        txtEISMailoutUpdateUser.Text = dr.Item("UpdateUser")
                    End If
                    If IsDBNull(dr.Item("UpdateDateTime")) Then
                        txtEISMailoutUpdateDateTime.Clear()
                    Else
                        txtEISMailoutUpdateDateTime.Text = dr.Item("UpdateDateTime")
                    End If
                    If IsDBNull(dr.Item("CreateDateTime")) Then
                        txtEISMailoutCreateDateTime.Clear()
                    Else
                        txtEISMailoutCreateDateTime.Text = dr.Item("CreateDateTime")
                    End If
                End While
                dr.Close()

                SQL = "select " & _
                "strContactFirstName, strContactLastName, " & _
                "strContactPrefix, strContactSuffix, " & _
                "strContactTitle, strContactPhoneNumber1, " & _
                "strContactPhoneNumber2, strContactFaxNumber, " & _
                "strContactEmail, strContactCompanyName, " & _
                "strContactAddress1, strContactAddress2, " & _
                "strContactCity, strContactState, " & _
                "strContactZipCode, strContactDescription, " & _
                "datModifingDate, (strLastName||', '||strFirstName) as ModifingPerson " & _
                "from " & DBNameSpace & ".APBContactInformation, " & DBNameSpace & ".EPDUserProfiles " & _
                "where " & DBNameSpace & ".APBContactInformation.strModifingPerson = " & _
                "" & DBNameSpace & ".EPDUserProfiles.numUserID  " & _
                "and strContactKey = '0413" & txtEILogSelectedAIRSNumber.Text & "41' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strContactFirstName")) Then
                        txtEISContactFirstName.Clear()
                    Else
                        txtEISContactFirstName.Text = dr.Item("strContactFirstName")
                    End If
                    If IsDBNull(dr.Item("strContactLastName")) Then
                        txtEISContactLastName.Clear()
                    Else
                        txtEISContactLastName.Text = dr.Item("strContactLastName")
                    End If
                    If IsDBNull(dr.Item("strContactPrefix")) Then
                        txtEISContactPrefix.Clear()
                    Else
                        txtEISContactPrefix.Text = dr.Item("strContactPrefix")
                    End If
                    If IsDBNull(dr.Item("strContactSuffix")) Then
                        txtEISContactSuffix.Clear()
                    Else
                        txtEISContactSuffix.Text = dr.Item("strContactSuffix")
                    End If
                    If IsDBNull(dr.Item("strContactTitle")) Then
                        txtEISContactTitle.Clear()
                    Else
                        txtEISContactTitle.Text = dr.Item("strContactTitle")
                    End If
                    If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                        txtEISContactPhone.Clear()
                    Else
                        txtEISContactPhone.Text = dr.Item("strContactPhoneNumber1")
                    End If
                    If IsDBNull(dr.Item("strContactPhoneNumber2")) Then
                        txtEISContactPhone2.Clear()
                    Else
                        txtEISContactPhone2.Text = dr.Item("strContactPhoneNumber2")
                    End If
                    If IsDBNull(dr.Item("strContactFaxNumber")) Then
                        txtEISContactFax.Clear()
                    Else
                        txtEISContactFax.Text = dr.Item("strContactFaxNumber")
                    End If
                    If IsDBNull(dr.Item("strContactEmail")) Then
                        txtEISContactEmail.Clear()
                    Else
                        txtEISContactEmail.Text = dr.Item("strContactEmail")
                    End If
                    If IsDBNull(dr.Item("strContactCompanyName")) Then
                        txtEISContactCompanyName.Clear()
                    Else
                        txtEISContactCompanyName.Text = dr.Item("strContactCompanyName")
                    End If
                    If IsDBNull(dr.Item("strContactAddress1")) Then
                        txtEISContactAddress.Clear()
                    Else
                        txtEISContactAddress.Text = dr.Item("strContactAddress1")
                    End If
                    If IsDBNull(dr.Item("strContactAddress2")) Then
                        txtEISContactAddress2.Clear()
                    Else
                        txtEISContactAddress2.Text = dr.Item("strContactAddress2")
                    End If
                    If IsDBNull(dr.Item("strContactCity")) Then
                        txtEISContactCity.Clear()
                    Else
                        txtEISContactCity.Text = dr.Item("strContactCity")
                    End If
                    If IsDBNull(dr.Item("strContactState")) Then
                        txtEISContactState.Clear()
                    Else
                        txtEISContactState.Text = dr.Item("strContactState")
                    End If
                    If IsDBNull(dr.Item("strContactZipCode")) Then
                        txtEISContactZipCode.Clear()
                    Else
                        txtEISContactZipCode.Text = dr.Item("strContactZipCode")
                    End If
                    If IsDBNull(dr.Item("strContactDescription")) Then
                        txtEISContactDescription.Clear()
                    Else
                        txtEISContactDescription.Text = dr.Item("strContactDescription")
                    End If
                    If IsDBNull(dr.Item("ModifingPerson")) Then
                        txtEISContactUpdateUser.Clear()
                    Else
                        txtEISContactUpdateUser.Text = dr.Item("ModifingPerson")
                    End If
                    If IsDBNull(dr.Item("datModifingDate")) Then
                        txtEISContactUpdateDateTime.Clear()
                    Else
                        txtEISContactUpdateDateTime.Text = dr.Item("datModifingDate")
                    End If

                End While
                dr.Close()

                LoadQASpecificData()
                TCDMUTools.SelectedIndex = 0

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub



    Private Sub btnCopyAIRSNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyAIRSNumber.Click
        Try
            Clipboard.SetDataObject(Replace(mtbEILogAIRSNumber.Text, "-", ""), True)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbEISStatsFipassed_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsFipassed.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISStatYear.Text, "", "1", "1", "0", _
                 " and EISStatusCode >= 3 ", "", " and QAStatusCode = '2' ")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, EPA Submitted Count"

            Exit Sub

            'Dim dgvRow As New DataGridViewRow

            SQL = "select distinct " & _
      "'False' as ID, " & _
      " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
     "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
"" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
"AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
"AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
"case " & _
"when strOptOut = '1' then 'Yes' " & _
"when strOptOut = '0' then 'No' " & _
"else '' " & _
"End strOptOut, " & _
"case " & _
"when strMailout = '1' then 'Yes' " & _
"else 'No' " & _
"end strMailout, " & _
"case " & _
"when strContactEmail is null then '-' " & _
"else strContactEmail " & _
"end ContactEmail, " & _
  "case " & _
                   "When strContactPrefix is null then '-' " & _
                   "else strContactPrefix " & _
                   "end strContactPrefix, " & _
                   "case " & _
                   "when strContactFirstName is null then '-' " & _
                   "else strContactFirstName " & _
                   "end strContactFirstName, " & _
                   "case " & _
                   "When strContactLastName is null then '-' " & _
                   "else strContactLastName " & _
                   "end strContactLastName, " & _
"case " & _
"when strDMUResponsibleStaff is null then '-' " & _
"else strDMUResponsibleStaff " & _
"end strDMUResponsibleStaff,  " & _
"AIRBranch.EISLK_QAStatus.strDesc,  " & _
            "datQAStatus " & _
"from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
"airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
"AIRbranch.EIS_Mailout, AIRBranch.EIS_QAAdmin, AIRbranch.EISLK_QAStatus  " & _
"where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
"and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
"and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
"and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
"and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
"and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
           "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
"and AIRBranch.EIS_QAAdmin.QAStatusCode = AIRBranch.EISLK_QAStatus.qastatuscode " & _
"and AIRBranch.EIS_Admin.Active = '1' " & _
"and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'" & _
            "and strEnrollment = '1' " & _
          "and AIRbranch.EIS_Admin.eisstatuscode >= 3  " & _
 "and (strOptOut = '0' ) " & _
 "and eis_qaadmin.qastatuscode  = '2'" & _
 "and    exists (Select * from AIRBranch.EIS_QAAdmin " & _
 "where AIRBranch.EIS_QAAdmin.inventoryYear = AIRBranch.EIS_Admin.inventoryYEar " & _
 "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID " & _
 "and eis_qaAdmin.qaStatusCode = '2') " & _
 "order by " & DBNameSpace & ".EIS_Admin.facilitysiteid "
            ' "and datQAComplete is not null )  "

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If
                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("strDMUResponsibleStaff")
                End If

                If IsDBNull(dr.Item("strDesc")) Then
                    dgvRow.Cells(14).Value = ""
                Else
                    dgvRow.Cells(14).Value = dr.Item("strDesc")
                End If
                If IsDBNull(dr.Item("datQAStatus")) Then
                    dgvRow.Cells(15).Value = ""
                Else
                    dgvRow.Cells(15).Value = dr.Item("datQAStatus")
                End If
                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "QA Submitted, EPA Submitted Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRemoveFromQA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFromQA.Click
        Try
            Dim EISConfirm As String = ""

            EISConfirm = InputBox("Type in the EIS Year that you have selected to delete all current QA data.", Me.Text)

            If EISConfirm = txtEILogSelectedYear.Text Then
                SQL = ""
                SQL = "delete AIRBranch.EIS_QAAdmin " & _
                "where inventoryyear = '" & EISConfirm & "', " & _
                "and facilitysiteid = '" & txtEILogSelectedAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update AIRBranch.EIS_Admin set " & _
                  "EISAccessCode = '2', " & _
                  "EISStatusCode = '3', " & _
                  "datEISstatus = sysdate, " & _
                  "UpdateUser = '" & Replace(UserName, "'", "''") & "', " & _
                  "updatedatetime = sysdate " & _
                  "where inventoryYear = '" & EISConfirm & "' " & _
                  "and facilitysiteid = '" & txtEILogSelectedAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader(CommandBehavior.CloseConnection)

                MsgBox("Done", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCleanUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCleanUp.Click
        Try
            For i As Integer = 0 To dgvEISStats.Rows.Count - 1
                If dgvEISStats(0, i).Value = True Then

                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd = New OracleCommand("airbranch.PD_EIS_QASTART", CurrentConnection)
                    cmd.CommandType = CommandType.StoredProcedure
                    temp = dgvEISStats(1, i).Value

                    cmd.Parameters.Add(New OracleParameter("AIRSNUMBER_IN", OracleDbType.Varchar2)).Value = dgvEISStats(1, i).Value
                    cmd.Parameters.Add(New OracleParameter("INTYEAR_IN", OracleDbType.Decimal)).Value = cboEISStatisticsYear.Text

                    cmd.ExecuteNonQuery()

                    If CurrentConnection.State = ConnectionState.Open Then
                        CurrentConnection.Close()
                    End If
                End If
            Next

            MsgBox("Complete", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub ViewMailoutData(ByVal MailoutStatus As String)
        Try
            Dim dgvRow As New DataGridViewRow
            dgvEISStats.Rows.Clear()
            SQL = "select " & _
            "'False' as ID, " & _
            " " & DBNameSpace & ".EIS_Admin.facilitysiteid, " & _
           "" & DBNameSpace & ".APBFacilityInformation.strFacilityname, " & _
           "" & DBNameSpace & ".EIS_Admin.inventoryyear, " & _
           "AIRbranch.EISLK_EISStatusCode.strDesc as EISStatus, " & _
           "AIRbranch.EISLK_EISAccessCode.strDesc as EISAccess, " & _
           "case " & _
           "when strOptOut = '1' then 'Yes' " & _
           "when strOptOut = '0' then 'No' " & _
           "else '-' " & _
           "End strOptOut, " & _
             "case " & _
           "when strEnrollment = '1' then 'Yes' " & _
           "when strEnrollment = '0' then 'No' " & _
           "else '-' " & _
           "end strEnrollment, " & _
           "case " & _
           "when strMailout = '1' then 'Yes' " & _
           "else 'No' " & _
           "end strMailout, " & _
           "case " & _
           "when strContactEmail is null then '-' " & _
           "else strContactEmail " & _
           "end ContactEmail, " & _
             "case " & _
           "When strContactPrefix is null then '-' " & _
           "else strContactPrefix " & _
           "end strContactPrefix, " & _
           "case " & _
           "when strContactFirstName is null then '-' " & _
           "else strContactFirstName " & _
           "end strContactFirstName, " & _
           "case " & _
           "When strContactLastName is null then '-' " & _
           "else strContactLastName " & _
           "end strContactLastName, " & _
           "case " & _
          "when strDMUResponsibleStaff is null then '-' " & _
           "else strDMUResponsibleStaff " & _
            "end strDMUResponsibleStaff, " & _
            "AIRBranch.EIS_Mailout.strContactCompanyName as CoName, " & _
            "AIRBranch.EIS_Mailout.strContactAddress1 as ContactAddress1, " & _
            "AIRBranch.EIS_Mailout.strContactAddress2 as ContactAddress2, " & _
            "AIRBranch.EIS_Mailout.strContactCity as ContactCity, " & _
            "AIRBranch.EIS_Mailout.strContactState as  ContactState, " & _
            "AIRBranch.EIS_Mailout.strContactZipCode as ContactZip, " & _
            "AIRBranch.EIS_Mailout.strContactFirstname as ContactFirstName, " & _
            "AIRBranch.EIS_Mailout.strContactLastName as ContactLastName, " & _
            "AIRBranch.EIS_Mailout.strContactPrefix as ContactPrefix, " & _
            "AIRBranch.EIS_Mailout.strContactEmail  as ContactEmail " & _
           "from AIRbranch.EIS_Admin, airbranch.APBFacilityInformation, " & _
           "airbranch.EISLK_EISAccessCode, AIRBranch.EISLK_EISStatusCode,  " & _
           "AIRbranch.EIS_Mailout, AIRbranch.EIS_QAAdmin " & _
           "where '0413'||airbranch.EIS_Admin.FacilitySiteId = airbranch.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBranch.EIS_Admin.EISAccessCode = AIRBranch.EISLK_EISAccessCode.EISAccessCode " & _
           "and AIRBranch.EIS_Admin.EISStatusCode = AIRBranch.EISLK_EISStatusCode.EISStatusCode " & _
           "and AIRBranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_QAAdmin.FacilitySiteID (+) " & _
           "and AIRBranch.EIS_Admin.inventoryyear = AIRBranch.EIS_QAAdmin.inventoryyear (+) " & _
           "and AIRbranch.EIS_Admin.FacilitySiteID = AIRBranch.EIS_Mailout.FacilitySiteID (+) " & _
           "and AIRbranch.EIS_Admin.inventoryyear = AIRBranch.EIS_Mailout.intinventoryyear (+) " & _
            "and AIRBranch.EIS_Admin.Active = '1' " & _
           "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISMailout.Text & "'"

            If MailoutStatus = "1" Then
                SQL = SQL & " and strMailout = '1' "
            End If

            dgvEISStats.Rows.Clear()
            ds = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("InventoryYear")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("InventoryYear")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If

                If IsDBNull(dr.Item("strOptOut")) Then
                    dgvRow.Cells(6).Value = False
                Else
                    dgvRow.Cells(6).Value = dr.Item("strOptOut")
                End If
                If IsDBNull(dr.Item("strMailout")) Then
                    dgvRow.Cells(7).Value = False
                Else
                    dgvRow.Cells(7).Value = dr.Item("strMailout")
                End If

                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(8).Value = False
                Else
                    dgvRow.Cells(8).Value = dr.Item("ContactEmail")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    dgvRow.Cells(9).Value = False
                Else
                    dgvRow.Cells(9).Value = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    dgvRow.Cells(10).Value = False
                Else
                    dgvRow.Cells(10).Value = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    dgvRow.Cells(11).Value = False
                Else
                    dgvRow.Cells(11).Value = dr.Item("strContactLastName")
                End If

                If IsDBNull(dr.Item("CoName")) Then
                    dgvRow.Cells(20).Value = False
                Else
                    dgvRow.Cells(20).Value = dr.Item("CoName")
                End If
                If IsDBNull(dr.Item("ContactAddress1")) Then
                    dgvRow.Cells(21).Value = False
                Else
                    dgvRow.Cells(21).Value = dr.Item("ContactAddress1")
                End If
                If IsDBNull(dr.Item("ContactAddress2")) Then
                    dgvRow.Cells(22).Value = False
                Else
                    dgvRow.Cells(22).Value = dr.Item("ContactAddress2")
                End If
                If IsDBNull(dr.Item("ContactCity")) Then
                    dgvRow.Cells(23).Value = False
                Else
                    dgvRow.Cells(23).Value = dr.Item("ContactCity")
                End If
                If IsDBNull(dr.Item("ContactState")) Then
                    dgvRow.Cells(24).Value = False
                Else
                    dgvRow.Cells(24).Value = dr.Item("ContactState")
                End If
                If IsDBNull(dr.Item("ContactZip")) Then
                    dgvRow.Cells(25).Value = False
                Else
                    dgvRow.Cells(25).Value = dr.Item("ContactZip")
                End If
                If IsDBNull(dr.Item("ContactFirstName")) Then
                    dgvRow.Cells(26).Value = False
                Else
                    dgvRow.Cells(26).Value = dr.Item("ContactFirstName")
                End If
                If IsDBNull(dr.Item("ContactLastName")) Then
                    dgvRow.Cells(27).Value = False
                Else
                    dgvRow.Cells(27).Value = dr.Item("ContactLastName")
                End If
                If IsDBNull(dr.Item("ContactPrefix")) Then
                    dgvRow.Cells(28).Value = False
                Else
                    dgvRow.Cells(28).Value = dr.Item("ContactPrefix")
                End If
                If IsDBNull(dr.Item("ContactEmail")) Then
                    dgvRow.Cells(29).Value = False
                Else
                    dgvRow.Cells(29).Value = dr.Item("ContactEmail")
                End If

                dgvEISStats.Rows.Add(dgvRow)
            End While
            dr.Close()

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewMailoutData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewMailoutData.Click
        Try
            If txtSelectedEISMailout.Text = "" Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            EIS_VIEW(txtSelectedEISMailout.Text, "1", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Mailout Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnGenerateMailout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateMailout.Click
        Try
            If txtSelectedEISMailout.Text = "" Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Update airbranch.EIS_Admin set " & _
            "strMailout = '1' " & _
            "where inventoryYear = '" & txtSelectedEISMailout.Text & "' " & _
            "and Active = '1' "
            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            EIS_VIEW(txtSelectedEISMailout.Text, "1", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Mailout Count (Generated)"


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRemoveAllMailout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAllMailout.Click
        Try

            SQL = "Update airbranch.EIS_Admin set " & _
          "strMailout = '' " & _
          "where inventoryYear = '" & txtSelectedEISMailout.Text & "' " & _
          "and strMailout = '1' " & _
          "and Active = '1' "

            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()
            EIS_VIEW(txtSelectedEISMailout.Text, "1", "", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Mailout Count (Removed)"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
 

    Private Sub btnViewEISEnrolled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewEISEnrolled.Click
        Try
            If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            EIS_VIEW(txtEISStatsEnrollmentYear.Text, "", "1", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Enrolled"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEISEnrollMailoutList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISEnrollMailoutList.Click
        Try
            If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Update AIRBranch.EIS_Admin set " & _
            "strEnrollment = '1' , " & _
            "EISSTATUSCODE= '1' " & _
            "where active = '1' " & _
            "and InventoryYear = '" & txtEISStatsEnrollmentYear.Text & "' " & _
            "and strMailout = '1' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            EIS_VIEW(txtEISStatsEnrollmentYear.Text, "", "1", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Enrolled (Generated)"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRemoveEISEnrolled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveEISEnrolled.Click
        Try
            If txtEISStatsEnrollmentYear.Text.Length <> 4 Then
                MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Update AIRBranch.EIS_Admin set " & _
            "strEnrollment = '0' " & _
            "where active = '1' & " & _
            "and InventoryYear = '" & txtEISStatsEnrollmentYear.Text & "' " & _
            "and strEnrollment = '1' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            EIS_VIEW(txtEISStatsEnrollmentYear.Text, "", "1", "1", "", "", "", "")

            txtEISStatsCount.Text = dgvEISStats.RowCount.ToString
            lblEISCount.Text = "EIS Enrolled (Removed)"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub EIS_VIEW(ByVal EISYear As String, ByVal EISMailout As String, ByVal EISEnrollment As String, _
                   ByVal EISActive As String, ByVal Optout As String, ByVal EISStatus As String, _
                   ByVal EISAccess As String, ByVal QAStatus As String)

        'EISYear = value
        'EISMailout = value: 0,1, or null
        'EISEnrollment = value: 0, 1, or null 
        'EISActive = value: 0, 1 or null 
        'Optout = value: 0, 1, Null, or text
        'EISStatus = text
        'EISAccess = text
        'QAStatus = text 

        Try
            If EISYear = "" Then
                Exit Sub
            End If
            If EISActive = "" Then
                EISActive = "1"
            End If

            Dim dgvRow As New DataGridViewRow

            SQL = "Select " & _
            "'False' as ID, " & _
            "FACILITYSITEID, " & _
            "STRFACILITYNAME, INVENTORYYEAR," & _
            "EISSTAtuS, EISACCESS, OPTOUT, " & _
            "MAILOUT, MAILOUTEMAIL, " & _
            "STRDMURESPONSIBLESTAFF, ENROLLMENT, " & _
            "QASTATUS, DATQASTATUS, " & _
            "IAIPPREFIX, IAIPFIRSTNAME, " & _
            "IAIPLASTNAME, IAIPEMAIL, " & _
            "EISCOMPANYNAME, EISADDRESS, " & _
            "EISADDRESS2, EISCITY, " & _
            "EISSTATE, EISZIPCODE, " & _
            "EISPREFIX, EISFIRSTNAME, " & _
            "EISLASTNAME " & _
            "from AIRBranch.VW_EIS_Stats " & _
            "where inventoryyear = '" & EISYear & "' " & _
            "and Active = '" & EISActive & "' "

            If EISMailout <> "" Then
                SQL = SQL & " and strMailout = '" & EISMailout & "' "
            End If
            If EISEnrollment <> "" Then
                SQL = SQL & " and strEnrollment = '" & EISEnrollment & "' "
            End If
            If Optout <> "" Then
                Select Case Optout
                    Case "Null"
                        SQL = SQL & " and strOptOut is null "
                    Case "0"
                        SQL = SQL & " and strOptOut = '0' "
                    Case "1"
                        SQL = SQL & " and strOptOut = '1'  "
                    Case Else
                        SQL = SQL & Optout
                End Select
            End If
            If EISStatus <> "" Then
                SQL = SQL & EISStatus
            End If
            If EISAccess <> "" Then
                SQL = SQL & EISAccess
            End If
            If QAStatus <> "" Then
                SQL = SQL & QAStatus
            End If

            dgvEISStats.Rows.Clear()

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvEISStats)
                If IsDBNull(dr.Item("ID")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("ID")
                End If

                If IsDBNull(dr.Item("FacilitySiteID")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("FacilitySiteID")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(2).Value = ""
                Else
                    dgvRow.Cells(2).Value = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("INVENTORYYEAR")) Then
                    dgvRow.Cells(3).Value = ""
                Else
                    dgvRow.Cells(3).Value = dr.Item("INVENTORYYEAR")
                End If
                If IsDBNull(dr.Item("EISStatus")) Then
                    dgvRow.Cells(4).Value = ""
                Else
                    dgvRow.Cells(4).Value = dr.Item("EISStatus")
                End If
                If IsDBNull(dr.Item("EISAccess")) Then
                    dgvRow.Cells(5).Value = ""
                Else
                    dgvRow.Cells(5).Value = dr.Item("EISAccess")
                End If
                If IsDBNull(dr.Item("OptOut")) Then
                    dgvRow.Cells(6).Value = ""
                Else
                    dgvRow.Cells(6).Value = dr.Item("OptOut")
                End If

                If IsDBNull(dr.Item("MailOut")) Then
                    dgvRow.Cells(7).Value = ""
                Else
                    dgvRow.Cells(7).Value = dr.Item("Mailout")
                End If
                If IsDBNull(dr.Item("MailoutEmail")) Then
                    dgvRow.Cells(8).Value = ""
                Else
                    dgvRow.Cells(8).Value = dr.Item("MailoutEmail")
                End If
                If IsDBNull(dr.Item("strDMUResponsibleStaff")) Then
                    dgvRow.Cells(9).Value = ""
                Else
                    dgvRow.Cells(9).Value = dr.Item("strDMUResponsibleStaff")
                End If
                If IsDBNull(dr.Item("Enrollment")) Then
                    dgvRow.Cells(10).Value = ""
                Else
                    dgvRow.Cells(10).Value = dr.Item("Enrollment")
                End If
                If IsDBNull(dr.Item("QASTATUS")) Then
                    dgvRow.Cells(11).Value = ""
                Else
                    dgvRow.Cells(11).Value = dr.Item("QASTATUS")
                End If
                If IsDBNull(dr.Item("DATQASTATUS")) Then
                    dgvRow.Cells(12).Value = ""
                Else
                    dgvRow.Cells(12).Value = dr.Item("DATQASTATUS")
                End If
                If IsDBNull(dr.Item("IAIPPrefix")) Then
                    dgvRow.Cells(13).Value = ""
                Else
                    dgvRow.Cells(13).Value = dr.Item("IAIPPrefix")
                End If
                If IsDBNull(dr.Item("IAIPFIRSTNAME")) Then
                    dgvRow.Cells(14).Value = ""
                Else
                    dgvRow.Cells(14).Value = dr.Item("IAIPFIRSTNAME")
                End If
                If IsDBNull(dr.Item("IAIPLASTNAME")) Then
                    dgvRow.Cells(15).Value = ""
                Else
                    dgvRow.Cells(15).Value = dr.Item("IAIPLASTNAME")
                End If

                If IsDBNull(dr.Item("IAIPEMAIL")) Then
                    dgvRow.Cells(16).Value = ""
                Else
                    dgvRow.Cells(16).Value = dr.Item("IAIPEMAIL")
                End If
                If IsDBNull(dr.Item("EISCOMPANYNAME")) Then
                    dgvRow.Cells(17).Value = ""
                Else
                    dgvRow.Cells(17).Value = dr.Item("EISCOMPANYNAME")
                End If
                If IsDBNull(dr.Item("EISADDRESS")) Then
                    dgvRow.Cells(18).Value = ""
                Else
                    dgvRow.Cells(18).Value = dr.Item("EISADDRESS")
                End If
                If IsDBNull(dr.Item("EISADDRESS2")) Then
                    dgvRow.Cells(19).Value = ""
                Else
                    dgvRow.Cells(19).Value = dr.Item("EISADDRESS2")
                End If
                If IsDBNull(dr.Item("EISCITY")) Then
                    dgvRow.Cells(20).Value = ""
                Else
                    dgvRow.Cells(20).Value = dr.Item("EISCITY")
                End If
                If IsDBNull(dr.Item("EISState")) Then
                    dgvRow.Cells(21).Value = ""
                Else
                    dgvRow.Cells(21).Value = dr.Item("EISState")
                End If
                If IsDBNull(dr.Item("EISZipCode")) Then
                    dgvRow.Cells(22).Value = ""
                Else
                    dgvRow.Cells(22).Value = dr.Item("EISZipCode")
                End If
                If IsDBNull(dr.Item("EISPrefix")) Then
                    dgvRow.Cells(23).Value = ""
                Else
                    dgvRow.Cells(23).Value = dr.Item("EISPrefix")
                End If
                If IsDBNull(dr.Item("EISFirstname")) Then
                    dgvRow.Cells(24).Value = ""
                Else
                    dgvRow.Cells(24).Value = dr.Item("EISFirstname")
                End If

                If IsDBNull(dr.Item("EISLASTNAME")) Then
                    dgvRow.Cells(25).Value = ""
                Else
                    dgvRow.Cells(25).Value = dr.Item("EISLASTNAME")
                End If

                dgvEISStats.Rows.Add(dgvRow)

            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
 

    Private Sub btnEISSummaryToExcel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISSummaryToExcel.Click
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        '   Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        'Dim ExcelApp As New Excel.Application
        Dim i As Integer
        Dim j As Integer

        Try

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If
            If dgvEISStats.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    For i = 0 To dgvEISStats.ColumnCount - 1
                        .Cells(1, i + 1) = dgvEISStats.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvEISStats.ColumnCount - 1
                        For j = 0 To dgvEISStats.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvEISStats.Item(i, j).Value.ToString
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