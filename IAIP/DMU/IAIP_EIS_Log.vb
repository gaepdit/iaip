Imports System.DateTime
Imports Oracle.DataAccess.Client
Imports System.IO

Public Class IAIP_EIS_Log
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

    Private Sub IAIP_EIS_Log_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)

        Try
            Panel1.Text = "Select a Function..."
            Panel2.Text = UserName
            Panel3.Text = OracleDate

            loadYear()
            loadMailOutYear()
            loadcboEISstatusCodes()
            LoadEISLog()
            LoadStats()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Page Load"
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

            Loop While dr.Read

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

            Do
                year = dr("STRESYEAR")
            Loop While dr.Read

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

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
            "order by qastatuscode  "

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

            dgvEISStats.Columns.Add("strOptOut", "Opt Out")
            dgvEISStats.Columns("strOptOut").DisplayIndex = 6
            dgvEISStats.Columns("strOptOut").Visible = True

            dgvEISStats.Columns.Add("strMailOut", "Mailout")
            dgvEISStats.Columns("strMailOut").DisplayIndex = 7
            dgvEISStats.Columns("strMailOut").Visible = True

            dgvEISStats.Columns.Add("ContactEmail", "Contact Email")
            dgvEISStats.Columns("ContactEmail").DisplayIndex = 8
            dgvEISStats.Columns("ContactEmail").Visible = True

            dgvEISStats.Columns.Add("strContactPrefix", "Contact Prefix")
            dgvEISStats.Columns("strContactPrefix").DisplayIndex = 9
            dgvEISStats.Columns("strContactPrefix").Visible = True

            dgvEISStats.Columns.Add("strContactFirstName", "Contact First Name")
            dgvEISStats.Columns("strContactFirstName").DisplayIndex = 10
            dgvEISStats.Columns("strContactFirstName").Visible = True

            dgvEISStats.Columns.Add("strContactLastName", "Contact Last Name")
            dgvEISStats.Columns("strContactLastName").DisplayIndex = 11
            dgvEISStats.Columns("strContactLastName").Visible = True

            dgvEISStats.Columns.Add("strDMUResponsibleStaff", "QA Reviewer")
            dgvEISStats.Columns("strDMUResponsibleStaff").DisplayIndex = 12
            dgvEISStats.Columns("strDMUResponsibleStaff").Visible = True

            dgvEISStats.Columns.Add("strEnrollment", "Enrollment")
            dgvEISStats.Columns("strEnrollment").DisplayIndex = 13
            dgvEISStats.Columns("strEnrollment").Visible = True

            dgvEISStats.Columns.Add("strDesc", "QA Status")
            dgvEISStats.Columns("strDesc").DisplayIndex = 14
            dgvEISStats.Columns("strDesc").Visible = True

            dgvEISStats.Columns.Add("datQAStatus", "QA Status Data")
            dgvEISStats.Columns("datQAStatus").DisplayIndex = 15
            dgvEISStats.Columns("datQAStatus").Visible = True
            dgvEISStats.Columns("datQAStatus").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvEISStats.Columns.Add("IAIPContactPrefix", "IAIP Contact Prefix")
            dgvEISStats.Columns("IAIPContactPrefix").DisplayIndex = 16
            dgvEISStats.Columns("IAIPContactPrefix").Visible = True

            dgvEISStats.Columns.Add("IAIPContactFirstname", "IAIP Contact First Name")
            dgvEISStats.Columns("IAIPContactFirstname").DisplayIndex = 17
            dgvEISStats.Columns("IAIPContactFirstname").Visible = True

            dgvEISStats.Columns.Add("IAIPContactLastName", "IAIP Contact Last Name")
            dgvEISStats.Columns("IAIPContactLastName").DisplayIndex = 18
            dgvEISStats.Columns("IAIPContactLastName").Visible = True

            dgvEISStats.Columns.Add("IAIPContactEmail", "IAIP Contact Email")
            dgvEISStats.Columns("IAIPContactEmail").DisplayIndex = 18
            dgvEISStats.Columns("IAIPContactEmail").Visible = True

        Catch ex As Exception

        End Try
    End Sub
#End Region
    Private Sub DEVDataManagementTools_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "Web Application Users"
    Private Sub btnActivateEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub


    Private Sub lblViewFacility_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

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
    Private Sub MmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Try
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
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
                    txtEILogFacilityName.Clear()
                Else
                    txtEILogFacilityName.Text = dr.Item("strFacilitySiteName")
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
                If IsDBNull(dr.Item("datInitialFinalize")) Then

                Else

                End If
                If IsDBNull(dr.Item("datFinalize")) Then

                Else

                End If
                If IsDBNull(dr.Item("strConfirmationNumber")) Then

                Else

                End If
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

            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadQASpecificData()
        Try
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
                If IsDBNull(dr.Item("datEISDeadline")) Then

                Else
                    dtpEISDeadline.Text = dr.Item("datEISDeadline")
                    dtpEISDeadline.Checked = False
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
              " ) ) "

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

                    End While
                    dr.Close()
                Case "TPEISStatMailout"
                    txtSelectedEISMailout.Text = cboEISStatisticsYear.Text

                    If txtSelectedEISMailout.Text = "" Then
                        MsgBox("Please select a valid Year from the dropdown first.", MsgBoxStyle.Exclamation, Me.Text)
                        Exit Sub
                    End If

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
                   "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISMailout.Text & "'" & _
                   "and strMailout = '1' "

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
                        dgvEISStats.Rows.Add(dgvRow)
                    End While
                    dr.Close()

                    txtEISStatsCount.Text = dgvEISStats.RowCount.ToString

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

            Dim dgvRow As New DataGridViewRow

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
            "and AIRbranch.EIS_Admin.inventoryyear = '" & txtSelectedEISStatYear.Text & "'"

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

            Dim dgvRow As New DataGridViewRow

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
           "and strMailout = '1' "

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

            Dim dgvRow As New DataGridViewRow

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

            Dim dgvRow As New DataGridViewRow

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

            Dim dgvRow As New DataGridViewRow

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
              "and (strEnrollment = '0') "

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

            Dim dgvRow As New DataGridViewRow

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

            Dim dgvRow As New DataGridViewRow


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

            Dim dgvRow As New DataGridViewRow

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

            Dim dgvRow As New DataGridViewRow


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
        "and strMailout = '1' " & _
             "and strEnrollment = '1' " & _
             "and AIRBranch.EIS_Admin.eisstatuscode >= 3 " & _
             "and (strOptOut = '0')  "

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

            Dim dgvRow As New DataGridViewRow

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
             "or AIRBranch.EIS_Admin.eisstatuscode = '4' or AIRBranch.EIS_Admin.eisstatuscode = '5') "
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
            lblEISCount.Text = "QA Began Count"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbEISSubmittedToEPA_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISSubmittedToEPA.LinkClicked
        Try
            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            Dim dgvRow As New DataGridViewRow

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
               "and datQAComplete is not null )  "

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
    Private Sub btnEISSummaryToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEISSummaryToExcel.Click
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
                    cmd.ExecuteReader()
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
                    cmd.ExecuteReader()
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
                        "'', '', '', '', '', '' " & _
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
                        cmd.ExecuteReader()

                        SQL = "update AIRBranch.EIS_Process set " & _
                        "intLastEmissionsYear = '" & EISConfirm & "' " & _
                        "where exists " & _
                        "(select * from airbranch.EIS_ProcessReportingPeriod " & _
                        "where airbranch.EIS_ProcessReportingPeriod.facilitysiteID = AIRBranch.EIS_Process.FacilitySiteID " & _
                        "and EIS_ProcessReportingPeriod.IntInventoryYear = '" & EISConfirm & "' " & _
                        "and EIS_ProcessReportingPeriod.EmissionsUnitID = EIS_Process.emissionsunitid " & _
                        "and EIS_ProcessReportingPeriod.ProcessID = eis_process.processid " & _
                        "and EIS_ProcessReportingPeriod.FacilitySiteID = '" & dgvEISStats(1, i).Value & "'  ) "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "update AIRBranch.EIS_ProcessControlApproach set " & _
                      "intLastInventoryYear = '" & EISConfirm & "' " & _
                      "where exists " & _
                      "(select * from airbranch.EIS_ProcessReportingPeriod " & _
                      "where airbranch.EIS_ProcessReportingPeriod.facilitysiteID = AIRBranch.EIS_ProcessControlApproach.FacilitySiteID " & _
                      "and EIS_ProcessReportingPeriod.IntInventoryYear = '" & EISConfirm & "' " & _
                      "and EIS_ProcessReportingPeriod.EmissionsUnitID = EIS_ProcessControlApproach.emissionsunitid " & _
                      "and EIS_ProcessReportingPeriod.ProcessID = EIS_ProcessControlApproach.processid " & _
                      "and EIS_ProcessReportingPeriod.FacilitySiteID = '" & dgvEISStats(1, i).Value & "'  ) "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "update AIRBranch.EIS_ProcessControlApproach set " & _
                       "intFirstInventoryYear = '" & EISConfirm & "' " & _
                       "where exists " & _
                       "(select * from airbranch.EIS_ProcessReportingPeriod " & _
                       "where airbranch.EIS_ProcessReportingPeriod.facilitysiteID = AIRBranch.EIS_ProcessControlApproach.FacilitySiteID " & _
                       "and EIS_ProcessReportingPeriod.IntInventoryYear = '" & EISConfirm & "' " & _
                       "and EIS_ProcessReportingPeriod.EmissionsUnitID = EIS_ProcessControlApproach.emissionsunitid " & _
                       "and EIS_ProcessReportingPeriod.ProcessID = EIS_ProcessControlApproach.processid " & _
                       "and intFirstInventoryYEar is null " & _
                       "and EIS_ProcessReportingPeriod.FacilitySiteID = '" & dgvEISStats(1, i).Value & "'  ) "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "update AIRBranch.EIS_UnitControlApproach set " & _
                        "intLastInventoryYear = '" & EISConfirm & "' " & _
                        "where exists " & _
                        "(select * from airbranch.EIS_ProcessReportingPeriod " & _
                        "where airbranch.EIS_ProcessReportingPeriod.facilitysiteID = AIRBranch.EIS_UnitControlApproach.FacilitySiteID " & _
                        "and EIS_ProcessReportingPeriod.IntInventoryYear = '" & EISConfirm & "' " & _
                        "and EIS_ProcessReportingPeriod.EmissionsUnitID = EIS_UnitControlApproach.emissionsunitid " & _
                        "and EIS_ProcessReportingPeriod.EmissionsUnitID = EIS_UnitControlApproach.EmissionsUnitID " & _
                        "and EIS_ProcessReportingPeriod.FacilitySiteID = '" & dgvEISStats(1, i).Value & "'  ) "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "update AIRBranch.EIS_UnitControlApproach set " & _
                        "intFirstInventoryYear = '" & EISConfirm & "' " & _
                        "where exists " & _
                        "(select * from airbranch.EIS_ProcessReportingPeriod " & _
                        "where airbranch.EIS_ProcessReportingPeriod.facilitysiteID = AIRBranch.EIS_UnitControlApproach.FacilitySiteID " & _
                        "and EIS_ProcessReportingPeriod.IntInventoryYear = '" & EISConfirm & "' " & _
                        "and EIS_ProcessReportingPeriod.EmissionsUnitID = EIS_UnitControlApproach.emissionsunitid " & _
                        "and EIS_ProcessReportingPeriod.EmissionsUnitID = EIS_UnitControlApproach.EmissionsUnitID " & _
                        "and intFirstInventoryYEar is null " & _
                        "and EIS_ProcessReportingPeriod.FacilitySiteID = '" & dgvEISStats(1, i).Value & "'  ) "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "delete airbranch.EIS_UnitControlPollutant " & _
                        "where active = '0' " & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "delete airbranch.EIS_UnitControlMeasure  " & _
                        "where active = '0' " & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "delete airbranch.EIS_UnitControlApproach  " & _
                        "where active = '0' " & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "delete airbranch.EIS_RPGEOCoordinates  " & _
                        "where active = '0' " & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "delete airbranch.EIS_RPApportionment  " & _
                        "where active = '0' " & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "delete airbranch.EIS_ProcessControlPollutant " & _
                        "where active = '0' " & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "delete airbranch.EIS_ProcessControlMeasure " & _
                        "where active = '0'" & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "delete airbranch.EIS_ProcessControlApproach  " & _
                        "where active = '0' " & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "delete airbranch.EIS_ReportingPeriodEmissions  " & _
                        "where active = '0'  " & _
                        "and intinventoryyear = '" & EISConfirm & "' " & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "delete airbranch.EIS_ProcessOperatingDetails  " & _
                        "where active = '0'  " & _
                        "and intInventoryYear = '" & EISConfirm & "' " & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "delete airbranch.EIS_ProcessRPTPeriodSCP  " & _
                        "where Active = '0'  " & _
                        "and intInventoryYear = '" & EISConfirm & "' " & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

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
                        "and Airbranch.EIS_RPApportionment.EmissionsUnitID  = Airbranch.eis_Process.EmissionsUnitID) " & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = " delete Airbranch.EIS_ProcessControlPollutant " & _
                        " where exists (select *  " & _
                        " from Airbranch.EIS_ProcessControlApproach, airbranch.EIS_Process   " & _
                        " where Airbranch.EIS_ProcessControlPollutant.facilitysiteid = Airbranch.EIS_ProcessControlApproach.facilitysiteid " & _
                        " and Airbranch.EIS_ProcessControlPollutant.ProcessId = Airbranch.EIS_ProcessControlApproach.ProcessId   " & _
                        " and Airbranch.EIS_ProcessControlPollutant.EmissionsUnitID  = Airbranch.EIS_ProcessControlApproach.EmissionsUnitID " & _
                        "and  Airbranch.EIS_ProcessControlPollutant.facilitysiteid = Airbranch.EIS_Process.facilitysiteid " & _
                        " and Airbranch.EIS_ProcessControlPollutant.ProcessId = Airbranch.EIS_Process.ProcessId   " & _
                        " and Airbranch.EIS_ProcessControlPollutant.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID  " & _
                        " and EIS_Process.active = '0' ) " & _
                        "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

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
                     "and EIS_Process.active = '0' ) " & _
                                  "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

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
                    "and Airbranch.EIS_ProcessControlApproach.EmissionsUnitID  = Airbranch.eis_Process.EmissionsUnitID) " & _
                                  "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

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
        "and Airbranch.EIS_ProcessControlPollutant.EmissionsUnitID  = Airbranch.EIS_ProcessControlApproach.EmissionsUnitID) " & _
                                  "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

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
        "and Airbranch.EIS_ProcessOperatingDetails.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID)  " & _
                                  "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

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
        "and Airbranch.EIS_ReportingPeriodEmissions.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID)  " & _
                                  "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

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
                     "and Airbranch.EIS_ProcessRPTPeriodSCP.EmissionsUnitID  = Airbranch.EIS_Process.EmissionsUnitID)  " & _
                                  "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

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
         "and EIS_Process.active = '0' ) " & _
                                  "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "delete airbranch.EIS_Process  " & _
                                      "where Active = '0' " & _
                                  "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "Delete airbranch.EIS_EmissionsUnit   " & _
                        "where active = '0' " & _
                                  "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                        SQL = "delete airbranch.EIS_Releasepoint  " & _
                                                      "where active = '0'  " & _
                                                      "and numRPStatusCodeYear = '" & EISConfirm & "' " & _
                                  "and FacilitySiteID = '" & dgvEISStats(1, i).Value & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()

                    End If
                Next
                ViewEISStats()
                ToDoQAList()

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
            '  Dim OptOut As String = ""
            Dim EISStatus As String = ""
            '  Dim Enrollment As String = ""
            '   Dim Mailout As String = ""
            '  Dim ActiveStatus As String = ""

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
            EISStatus = cboEILogStatusCode.SelectedValue
            EISAccess = cboEILogAccessCode.SelectedValue
            'If rdbEILogActiveYes.Checked = True Then
            '    ActiveStatus = "1"
            'Else
            '    ActiveStatus = "0"
            'End If

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
            "updateUser = '" & Replace(UserName, "'", "''") & "', " & _
            "updateDateTime = sysdate " & _
            "where inventoryyear = '" & cboEILogYear.Text & "' " & _
            "and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteReader()

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

                SQL = "insert into AIRBranch.EIS_QAAdmin " & _
               "(select " & _
               "'" & txtEILogSelectedYear.Text & "', '" & txtEILogSelectedAIRSNumber.Text & "', " & _
               "'" & QAStart & "', '" & QAPass & "', " & _
               "'1', '" & QAStatusDate & "', " & _
               "'" & Replace(StaffResponsible, "'", "''") & "', " & _
               "'" & QAComplete & "', '" & Replace(QAComments, "'", "''") & "', " & _
               "'1', '" & UserName & "', " & _
               "sysdate, sysdate, " & _
               "'" & Replace(FITracking, "'", "''") & "', " & _
               "'" & Replace(FIError, "'", "''") & "', " & _
               "'" & Replace(pointTracking, "'", "''") & "', " & _
               "'" & Replace(pointError, "'", "''") & "', '', '' " & _
               "from dual " & _
               "where not exists (select * from AIRBranch.EIS_QAAdmin " & _
               "where inventoryYear = '" & txtEILogSelectedYear.Text & "' " & _
               "and FacilitySiteID = '" & txtEILogSelectedAIRSNumber.Text & "')) "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteReader()

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

            SQL = "insert into AIRBranch.EIS_QAAdmin " & _
           "(select " & _
           "'" & txtEILogSelectedYear.Text & "', '" & txtEILogSelectedAIRSNumber.Text & "', " & _
           "'" & QAStart & "', '" & QAPass & "', " & _
           "'1', '" & QAStatusDate & "', " & _
           "'" & Replace(StaffResponsible, "'", "''") & "', " & _
           "'" & QAComplete & "', '" & Replace(QAComments, "'", "''") & "', " & _
           "'1', '" & UserName & "', " & _
           "sysdate, sysdate, " & _
           "'" & Replace(FITracking, "'", "''") & "', " & _
           "'" & Replace(FIError, "'", "''") & "', " & _
           "'" & Replace(PointTracking, "'", "''") & "', " & _
           "'" & Replace(PointError, "'", "''") & "', '', '' " & _
           "from dual " & _
           "where not exists (select * from AIRBranch.EIS_QAAdmin " & _
           "where inventoryYear = '" & txtEILogSelectedYear.Text & "' " & _
           "and FacilitySiteID = '" & txtEILogSelectedAIRSNumber.Text & "')) "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteReader()

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

            'If dtpEISDeadline.Checked = True Then
            '    Dim DeadLineComments As String = ""

            '    DeadLineComments = dtpEISDeadline.Text & "(deadline)- " & UserName & " - " & OracleDate & vbCrLf & _
            '    txtEISDeadlineComment.Text & _
            '    vbCrLf & vbCrLf & txtAllEISDeadlineComment.Text

            '    SQL = "update Airbranch.EIS_QAAdmin set " & _
            '    "datEISDeadline = '" & dtpEISDeadline.Text & "',  " & _
            '    "strEISDeadlineComment = '" & Replace(DeadLineComments, "'", "''") & "' " & _
            '    "where INventoryyear = '" & cboEILogYear.Text & "' " & _
            '    "and FacilitySiteID = '" & mtbEILogAIRSNumber.Text & "' "
            '    cmd = New OracleCommand(SQL, conn)
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If
            '    cmd.ExecuteReader()

            'End If

            If dtpEISDeadline.Checked = True Then
                Dim DeadLineComments As String = ""
                If txtAllEISDeadlineComment.Text.Contains(dtpEISDeadline.Text & "(deadline)- " & UserName & " - " & OracleDate & vbCrLf & _
                txtEISDeadlineComment.Text) Then
                Else
                    DeadLineComments = dtpEISDeadline.Text & "(deadline)- " & UserName & " - " & OracleDate & vbCrLf & _
                    txtEISDeadlineComment.Text & _
                    vbCrLf & vbCrLf & txtAllEISDeadlineComment.Text

                    SQL = "update Airbranch.EIS_QAAdmin set " & _
                    "datEISDeadline = '" & dtpEISDeadline.Text & "',  " & _
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
    Private Sub llbEISStatsSubmittedToDo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbEISStatsSubmittedToDo.LinkClicked
        Try
            ToDoQAList()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ToDoQAList()
        Try

            If txtSelectedEISStatYear.Text = "" Then
                Exit Sub
            End If

            Dim dgvRow As New DataGridViewRow

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
 "and AIRBranch.EIS_QAAdmin.facilitysiteID = AIRBranch.EIS_Admin.facilitysiteID)    "


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

            Dim dgvRow As New DataGridViewRow

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
 "and datQAComplete is null )  "

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

            Dim dgvRow As New DataGridViewRow

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

            Dim dgvRow As New DataGridViewRow

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

            Dim dgvRow As New DataGridViewRow

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
                'For i = 0 To dgvEISStats.Rows.Count - 1
                '    temp = temp & " FacilitySiteID = '" & dgvEISStats(1, i).Value & "' or "
                'Next
                'temp = " and ( " & Mid(temp, 1, (temp.Length - 3)) & " ) "



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
    Private Sub txtQAComments_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQAComments.KeyPress
        Try
            If e.KeyChar = Microsoft.VisualBasic.ChrW(1) Then
                txtQAComments.SelectAll()
            End If

        Catch ex As Exception

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
                        txtEILogFacilityName.Clear()
                    Else
                        txtEILogFacilityName.Text = dr.Item("strFacilitySiteName")
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
    Private Sub mtbEILogAIRSNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mtbEILogAIRSNumber.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
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
                        txtEILogFacilityName.Clear()
                    Else
                        txtEILogFacilityName.Text = dr.Item("strFacilitySiteName")
                    End If
                End While
                dr.Close()

                LoadQASpecificData()
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

End Class