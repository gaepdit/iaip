Imports System.Data.OracleClient
'Imports System.Data.OleDb
'Imports Excel

Public Class ISMPSmokeSchool
    Public dsSchedule As DataSet
    Public daSchedule As OracleDataAdapter
    Public dsYear As DataSet
    Public daYear As OracleDataAdapter
    Public dsRes As DataSet
    Public daRes As OracleDataAdapter
    Public dsRes2 As DataSet
    Public daRes2 As OracleDataAdapter
    Public dsStudent As DataSet
    Public daStudent As OracleDataAdapter
    Public dsClasses As DataSet
    Public daClasses As OracleDataAdapter
    Public dsScores As DataSet
    Public daScores As OracleDataAdapter
    Public dsScores3 As DataSet
    Public daScores3 As OracleDataAdapter
    Dim my1 As Roster
    Dim my2 As PassFailNoShow
    Public getIDoverRideFlag As String = "off"
    Public ErrorFlag As String = "no"

    Private Sub ISMPSmokeSchool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            pnl1.Text = " "
            pnl2.Text = UserName
            pnl3.Text = OracleDate

            setPassedDate()
            txtsortnbr.Text = "1"
            LoadCboSalutation()
            LoadCboYear()
            LoadCboCity()
            LoadCboSeason()
            LoadCboDisplay()
            LoadCboLecture()
            loadCboVision()
            LoadLocTerm1()
            LoadLocTerm3()
            loadCboPassFail()
            BindDataGridSchedule()
            BindDataGridSchedule2()
            PanelStats.Visible = False
            LoadDiplomaYear()
            setPermissions()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub

#Region " Load Routines "
    Sub setPermissions()
        Try

            If AccountArray(128, 3) <> "1" Then
                tcSmokeSchool.TabPages.Remove(TabMoveRes2Scores)
                tcSmokeSchool.TabPages.Remove(TPDiplomas)
                tcSmokeSchool.TabPages.Remove(TabSetup)
                tcSmokeSchool.TabPages.Remove(TabCache)

                btnSaveRes.Visible = False
                btnDeleteRes.Visible = False
                btnMoveResStudent2Scores.Visible = False
                btnMove2Cache.Visible = False
                btnUpdateIDs.Visible = False
                PanelStats.Visible = False
                btnDelete.Visible = False
                btnSave3.Visible = False
                btnRefreshClasses.Visible = False
            End If



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadDiplomaYear()
        Try
            SQL = "Select " & _
            "distinct(strYear) as TermYear " & _
            "from " & DBNameSpace & ".SmokeSchoolSchedule " & _
            "order by TermYear desc "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            cboTermYear.Items.Clear()
            cboTermYear.Items.Add("-Select a Year-")
            While dr.Read
                cboTermYear.Items.Add(dr.Item("TermYear"))
            End While
            dr.Close()

            cboTermYear.Text = cboTermYear.Items.Item(0)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub LoadCboYear()
        Try
            'Load Year dropdown boxes
            Dim year As String = CInt(Now.Year)
            Dim x As String = 0

            cboYear.Items.Add("- Select a Year -")

            x = CStr(year - 5)
            cboYear.Items.Add(x)
            x = CStr(year - 4)
            cboYear.Items.Add(x)
            x = CStr(year - 4)
            cboYear.Items.Add(x)
            x = CStr(year - 2)
            cboYear.Items.Add(x)
            x = CStr(year - 1)
            cboYear.Items.Add(x)
            x = CStr(year - 0)
            cboYear.Items.Add(x)
            x = CStr(year + 1)
            cboYear.Items.Add(x)
            x = CStr(year + 2)
            cboYear.Items.Add(x)
            x = CStr(year + 3)
            cboYear.Items.Add(x)
            x = CStr(year + 4)
            cboYear.Items.Add(x)
            x = CStr(year + 5)
            cboYear.Items.Add(x)

            cboYear.SelectedIndex = 0

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub LoadCboSalutation()
        Try
            'Load Saluation dropdownboxes
            cboSalutation.Items.Add("- Select one -")
            cboSalutation.Items.Add("Mr")
            cboSalutation.Items.Add("Mrs")
            cboSalutation.Items.Add("Miss")
            cboSalutation.Items.Add("Ms")
            cboSalutation.Items.Add("Dr")
            cboSalutation.Items.Add("Prof")
            cboSalutation.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub LoadCboCity()
        Try
            'Load City dropdown boxes
            cboCity.Items.Add("- Select a City -")
            cboCity.Items.Add("Adel")
            cboCity.Items.Add("Atlanta")
            cboCity.Items.Add("Cartersville")
            cboCity.Items.Add("Dalton")
            cboCity.Items.Add("Perry")
            cboCity.Items.Add("Savannah")
            cboCity.Items.Add("Tifton")
            cboCity.Items.Add("Special Venue")
            cboCity.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub LoadCboSeason()
        Try
            'Load Season dropdown boxes
            cboSeason.Items.Add("- Select a Season -")
            cboSeason.Items.Add("Fall")
            cboSeason.Items.Add("Spring")
            cboSeason.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub LoadCboDisplay()
        Try
            'Load Display dropdown boxes with YES/NO
            cboDisplay.Items.Add("- Display This Session -")
            cboDisplay.Items.Add("YES")
            cboDisplay.Items.Add("NO")
            cboDisplay.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub LoadCboLecture()
        Try
            'Load Attend Lecture Yes/No box
            cboLecture.Items.Add("---")
            cboLecture.Items.Add("YES")
            cboLecture.Items.Add("NO")
            cboLecture.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub LoadLocTerm1()
        Try
            Dim locTerm As String

            Dim oracleSQL As String


            If Conn.State = ConnectionState.Open Then
            Else
                Conn.Open()
            End If

            cboSchedule1.Items.Clear()

            oracleSQL = "Select strSchedule from airbranch.smokeSchoolSchedule " & _
                        "where strDisplay = 'YES' " & _
                        "order by strSchedule"
            Dim cmd As New OracleCommand(oracleSQL, Conn)

            Dim dr As OracleDataReader = cmd.ExecuteReader()

            cboSchedule1.Items.Add("- Select a Location/Term -")
            cboSchedule1.Items.Add("- All Terms -")

            dr.Read()
            Do
                locTerm = dr("strSchedule")
                cboSchedule1.Items.Add(locTerm)

            Loop While dr.Read

            cboSchedule1.SelectedIndex = 0

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub LoadLocTerm3()
        Try
            Dim locTerm As String
            Dim oracleSQL As String

            cboSchedule3.Items.Clear()

            If Conn.State = ConnectionState.Open Then
            Else
                Conn.Open()
            End If

            oracleSQL = "Select strSchedule from airbranch.smokeSchoolSchedule order by strSchedule desc"
            Dim cmd As New OracleCommand(oracleSQL, Conn)

            Dim dr As OracleDataReader = cmd.ExecuteReader()

            cboSchedule3.Items.Add("- Select a Location/Term -")
            cboSchedule3.Items.Add("- All Terms -")

            dr.Read()
            Do
                locTerm = dr("strSchedule")
                cboSchedule3.Items.Add(locTerm)
            Loop While dr.Read

            cboSchedule3.SelectedIndex = 0

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub loadCboPassFail()
        Try
            cboPassFail.Items.Add("--")
            cboPassFail.Items.Add("Pass")
            cboPassFail.Items.Add("Fail")
            cboPassFail.Items.Add("No Show")
            cboPassFail.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub loadCboVision()
        Try
            cboVisionCorrection.Items.Add("--")
            cboVisionCorrection.Items.Add("Yes")
            cboVisionCorrection.Items.Add("No")
            cboVisionCorrection.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub setPassedDate()
        Try
            Dim date1 As Date
            Dim date2 As Date

            date1 = dtpDatePassed.Value
            date2 = DateAdd(DateInterval.Day, -1, date1)
            dtpDatePassed.Value = date2
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
#End Region
#Region " Bind Routines "
    Private Sub BindDataGridSchedule()
        Try
            'populate a read-only dataGridView control with oracle
            Dim SQL As String
            Dim dr As OracleDataReader

            SQL = "select strSchedule, strDisplay from airbranch.smokeSchoolSchedule " & _
            "order by strSchedule desc "

            Dim cmd As New OracleCommand(SQL, Conn)
            cmd.CommandType = CommandType.Text

            If Conn.State = ConnectionState.Open Then
            Else
                Conn.Open()
            End If

            dr = cmd.ExecuteReader

            dgvSchedule.Rows.Clear()
            dgvSchedule.Columns.Clear()

            dgvSchedule.Columns.Add("strSchedule", "Schedule")
            dgvSchedule.Columns.Add("strDisplay", "Display")

            dgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvSchedule.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(1) As Object
                dr.GetValues(objCells)
                dgvSchedule.Rows.Add(objCells)
            End While

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub BindDataGridSchedule2()
        Try
            'populate a read-only dataGridView control with oracle
            Dim SQL As String
            Dim dr As OracleDataReader

            SQL = "select strSchedule from airbranch.smokeSchoolSchedule " & _
            "where strDisplay = 'YES' " & _
            "order by strSchedule desc "

            Dim cmd As New OracleCommand(SQL, Conn)
            cmd.CommandType = CommandType.Text

            If Conn.State = ConnectionState.Open Then
            Else
                Conn.Open()
            End If

            dr = cmd.ExecuteReader

            dgvSchedule2.Rows.Clear()
            dgvSchedule2.Columns.Clear()

            dgvSchedule2.Columns.Add("strSchedule", "Schedule")

            dgvSchedule2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvSchedule2.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(0) As Object
                dr.GetValues(objCells)
                dgvSchedule2.Rows.Add(objCells)
            End While

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub BindDataGridReservation()
        Try
            Dim locationTerm As String = cboSchedule1.SelectedItem
            Dim SQL As String = ""
            Dim dr As OracleDataReader

            If cboSchedule1.SelectedIndex <> 0 Or cboSchedule1.SelectedIndex <> 1 Then

                dsRes = New DataSet

                If txtsortnbr.Text = "1" Then
                    SQL = "SELECT numUserID, " & _
                                 "strLastName, " & _
                                 "strFirstName, " & _
                                 "strSalutation, " & _
                                 "strTitle, " & _
                                 "strCompanyName, " & _
                                 "strAddress1, " & _
                                 "strAddress2, " & _
                                 "strCity, " & _
                                 "strState, " & _
                                 "strZip, " & _
                                 "strPhoneNumber, " & _
                                 "strFax, " & _
                                 "strEmail, " & _
                                 "strConfirmationNbr, " & _
                                 "strLocationDate, " & _
                                 "strLectureYesNo " & _
                          "from airbranch.smokeSchoolReservation " & _
                          "order by strLocationDate, strLastName, strFirstName"
                End If
                If txtsortnbr.Text = "2" Then
                    SQL = "SELECT numUserID, " & _
                                 "strLastName, " & _
                                 "strFirstName, " & _
                                 "strSalutation, " & _
                                 "strTitle, " & _
                                 "strCompanyName, " & _
                                 "strAddress1, " & _
                                 "strAddress2, " & _
                                 "strCity, " & _
                                 "strState, " & _
                                 "strZip, " & _
                                 "strPhoneNumber, " & _
                                 "strFax, " & _
                                 "strEmail, " & _
                                 "strConfirmationNbr, " & _
                                 "strLocationDate, " & _
                                 "strLectureYesNo " & _
                             "from airbranch.smokeSchoolReservation " & _
                             "where strLocationDate = '" & locationTerm & "' " & _
                             "order by strLastName, strFirstName"
                End If

                Dim cmd As New OracleCommand(SQL, Conn)
                cmd.CommandType = CommandType.Text

                If Conn.State = ConnectionState.Open Then
                Else
                    Conn.Open()
                End If

                dr = cmd.ExecuteReader

                dgvRes.Rows.Clear()
                dgvRes.Columns.Clear()

                dgvRes.Columns.Add("numUserID", "User ID")
                dgvRes.Columns.Add("strLastName", "Last Name")
                dgvRes.Columns.Add("strFirstName", "First Name")
                dgvRes.Columns.Add("strSalutation", "Salutation")
                dgvRes.Columns.Add("strTitle", "Title")
                dgvRes.Columns.Add("strCompanyName", "Company")
                dgvRes.Columns.Add("strAddress1", "Address")
                dgvRes.Columns.Add("strAddress2", "Address")
                dgvRes.Columns.Add("strCity", "City")
                dgvRes.Columns.Add("strState", "State")
                dgvRes.Columns.Add("strZip", "Zip")
                dgvRes.Columns.Add("strPhoneNumber", "Phone")
                dgvRes.Columns.Add("strFax", "Fax")
                dgvRes.Columns.Add("strEmail", "Email")
                dgvRes.Columns.Add("strConfirmationNbr", "Confirmation Number")
                dgvRes.Columns.Add("strLocationDate", "Session")
                dgvRes.Columns.Add("strLectureYesNo", "Lecture")

                dgvRes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
                dgvRes.RowHeadersVisible = False

                While dr.Read
                    'Get row data as an object array
                    'objCells(2) means col[0], col[1], col[2]
                    'which returns the first colums in the table
                    Dim objCells(16) As Object
                    dr.GetValues(objCells)
                    If IsDBNull(objCells(0)) Then
                        objCells(0) = 0
                    End If

                    dgvRes.Rows.Add(objCells)
                End While

            Else
                MsgBox("You must select a Term and Location")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    'just a shell
    Private Sub BindDataGridReservation1()
        Try
            dsRes = New DataSet

            If txtsortnbr.Text = "1" Then
                SQL = "SELECT numUserID, " & _
                             "strLastName, " & _
                             "strFirstName, " & _
                             "strTitle, " & _
                             "strCompanyName, " & _
                             "strAddress1, " & _
                             "strCity, " & _
                             "strState, " & _
                             "strZip, " & _
                             "strPhoneNumber, " & _
                             "strFax, " & _
                             "strConfirmationNbr, " & _
                             "strLocationDate, " & _
                             "strLectureYesNo " & _
                      "from airbranch.smokeSchoolReservation " & _
                      "order by strLocationDate, strLastName"
            End If
            If txtsortnbr.Text = "2" Then
                SQL = "SELECT numUserID, " & _
                             "strLastName, " & _
                             "strFirstName, " & _
                             "strTitle, " & _
                             "strCompanyName, " & _
                             "strAddress1, " & _
                             "strCity, " & _
                             "strState, " & _
                             "strZip, " & _
                             "strPhoneNumber, " & _
                             "strFax, " & _
                             "strConfirmationNbr, " & _
                             "strLocationDate, " & _
                             "strLectureYesNo " & _
                      "from airbranch.smokeSchoolReservation " & _
                         "where strLocationDate = '" & txtSortItem.Text & "' " & _
                         "order by strLastName, strFirstName"
            End If

            Dim cmd As New OracleCommand(SQL, Conn)
            cmd.CommandType = CommandType.Text

            If Conn.State = ConnectionState.Open Then
            Else
                Conn.Open()
            End If

            dr = cmd.ExecuteReader

            dgvRes.Rows.Clear()

            dgvRes.Columns.Add("numUserID", "ID")
            dgvRes.Columns.Add("strLastName", "Last Name")
            dgvRes.Columns.Add("strFirstName", "First Name")
            dgvRes.Columns.Add("strTitle", "Title")
            dgvRes.Columns.Add("strCompanyName", "Company")
            dgvRes.Columns.Add("strAddress1", "Address")
            dgvRes.Columns.Add("strCity", "City")
            dgvRes.Columns.Add("strState", "State")
            dgvRes.Columns.Add("strZip", "Zip")
            dgvRes.Columns.Add("strPhoneNumber", "Phone")
            dgvRes.Columns.Add("strFax", "Fax")
            dgvRes.Columns.Add("strEmail", "Email")
            dgvRes.Columns.Add("strConfirmationNbr", "Confirmation Number")
            dgvRes.Columns.Add("strLocationDate", "Session")
            dgvRes.Columns.Add("strLectureYesNo", "Lecture")

            dgvRes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvRes.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(14) As Object
                dr.GetValues(objCells)
                dgvRes.Rows.Add(objCells)
            End While

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub BindDataGridReservation2()
        Try
            Dim SQL As String = ""
            Dim dr As OracleDataReader

            dsRes = New DataSet

            If txtsortnbr.Text = "1" Then
                SQL = "SELECT numUserID, " & _
                             "strLastName, " & _
                             "strFirstName, " & _
                             "strTitle, " & _
                             "strCompanyName, " & _
                             "strAddress1, " & _
                             "strCity, " & _
                             "strState, " & _
                             "strZip, " & _
                             "strPhoneNumber, " & _
                             "strFax, " & _
                             "strEmail, " & _
                             "strConfirmationNbr, " & _
                             "strLocationDate, " & _
                             "strLectureYesNo " & _
                      "from airbranch.smokeSchoolReservation " & _
                      "order by strLocationDate, strLastName, strLastName"
            End If
            If txtsortnbr.Text = "2" Then
                SQL = "SELECT numUserID, " & _
                             "strLastName, " & _
                             "strFirstName, " & _
                             "strTitle, " & _
                             "strCompanyName, " & _
                             "strAddress1, " & _
                             "strCity, " & _
                             "strState, " & _
                             "strZip, " & _
                             "strPhoneNumber, " & _
                             "strFax, " & _
                             "strEmail, " & _
                             "strConfirmationNbr, " & _
                             "strLocationDate, " & _
                             "strLectureYesNo " & _
                      "from airbranch.smokeSchoolReservation " & _
                         "where strLocationDate = '" & txtSchedule2.Text & "' " & _
                         "order by strLastName, strFirstName"
            End If

            Dim cmd As New OracleCommand(SQL, Conn)
            cmd.CommandType = CommandType.Text

            If Conn.State = ConnectionState.Open Then
            Else
                Conn.Open()
            End If

            dr = cmd.ExecuteReader

            dgvRes2.Rows.Clear()
            dgvRes2.Columns.Clear()

            dgvRes2.Columns.Add("numUserID", "ID")
            dgvRes2.Columns.Add("strLastName", "Last Name")
            dgvRes2.Columns.Add("strFirstName", "First Name")
            dgvRes2.Columns.Add("strTitle", "Title")
            dgvRes2.Columns.Add("strCompanyName", "Company")
            dgvRes2.Columns.Add("strAddress1", "Address")
            dgvRes2.Columns.Add("strCity", "City")
            dgvRes2.Columns.Add("strState", "State")
            dgvRes2.Columns.Add("strZip", "Zip")
            dgvRes2.Columns.Add("strPhoneNumber", "Phone")
            dgvRes2.Columns.Add("strFax", "Fax")
            dgvRes2.Columns.Add("strEmail", "Email")
            dgvRes2.Columns.Add("strConfirmationNbr", "Confirmation Number")
            dgvRes2.Columns.Add("strLocationDate", "Session")
            dgvRes2.Columns.Add("strLectureYesNo", "Lecture")

            dgvRes2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvRes2.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(15) As Object
                dr.GetValues(objCells)
                If IsDBNull(objCells(0)) Then
                    objCells(0) = 0
                End If
                dgvRes2.Rows.Add(objCells)
            End While


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub BindDataGridScores3()
        Try
            Dim SQL As String
            Dim dr As OracleDataReader
            Dim locationTerm As String
            Dim colon As Integer



            locationTerm = cboSchedule3.SelectedItem
            colon = InStr(locationTerm, ":")
            locationTerm = Mid(locationTerm, 1, colon - 1)

            dsScores3 = New DataSet

            SQL = "SELECT distinct intStudentId, " & _
                  "strName, " & _
                  "strCompanyName, " & _
                  "strPassFailNoShow, " & _
                  "strQuizScore, " & _
                  "strDatePassed, " & _
                  "strVisualRestrictions, " & _
                  "strComment, " & _
                  "strRUN1, " & _
                  "strRUN2, " & _
                  "strRUN3, " & _
                  "strRUN4, " & _
                  "strRUN5, " & _
                  "strRUN6, " & _
                  "strRUN7, " & _
                  "strRUN8, " & _
                  "strRUN9, " & _
                  "strRUN10, " & _
                  "strFirstName, " & _
                  "strLastName, strScoreKey " & _
                  "from airbranch.smokeSchoolScores " & _
                  "where strLocationTerm = '" & locationTerm & "' " & _
                  "order by strName"

            Dim cmd As New OracleCommand(SQL, Conn)
            cmd.CommandType = CommandType.Text

            If Conn.State = ConnectionState.Open Then
            Else
                Conn.Open()
            End If

            dr = cmd.ExecuteReader

            dgvScore3.Rows.Clear()
            dgvScore3.Columns.Clear()

            dgvScore3.Columns.Add("intStudentId", "ID")
            dgvScore3.Columns.Add("strName", "Name")
            dgvScore3.Columns.Add("strCompanyName", "Company")
            dgvScore3.Columns.Add("strPassFailNoShow", "Pass Fail NoShow")
            dgvScore3.Columns.Add("strQuizScore", "Quiz Score")
            dgvScore3.Columns.Add("strDatePassed", "Date Passed")
            dgvScore3.Columns.Add("strVisualRestrictions", "Visual Restrictions")
            dgvScore3.Columns.Add("strComment", "Comments")
            dgvScore3.Columns.Add("strRUN1", "RUN 1")
            dgvScore3.Columns.Add("strRUN1", "RUN 2")
            dgvScore3.Columns.Add("strRUN1", "RUN 3")
            dgvScore3.Columns.Add("strRUN1", "RUN 4")
            dgvScore3.Columns.Add("strRUN1", "RUN 5")
            dgvScore3.Columns.Add("strRUN1", "RUN 6")
            dgvScore3.Columns.Add("strRUN1", "RUN 7")
            dgvScore3.Columns.Add("strRUN1", "RUN 8")
            dgvScore3.Columns.Add("strRUN1", "RUN 9")
            dgvScore3.Columns.Add("strRUN1", "RUN 10")
            dgvScore3.Columns.Add("strFirstName", "First Name")
            dgvScore3.Columns.Add("strLastName", "Last Name")
            dgvScore3.Columns.Add("strScoreKey", "Score Key")

            dgvScore3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvScore3.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(20) As Object
                dr.GetValues(objCells)
                dgvScore3.Rows.Add(objCells)
            End While

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub BindDataGridScores1()
        Try
            Dim SQL As String
            Dim dr As OracleDataReader
            Dim locationTerm As String = txtSchedule2.Text
            Dim colon As Integer

            colon = InStr(locationTerm, ":")
            locationTerm = Mid(locationTerm, 1, colon - 1)

            dsScores = New DataSet

            SQL = "SELECT distinct intStudentId, " & _
                  "strName, " & _
                  "strCompanyName " & _
                  "from airbranch.smokeSchoolScores " & _
                  "where strLocationTerm = '" & locationTerm & "' " & _
                  "order by strName"

            Dim cmd As New OracleCommand(SQL, Conn)
            cmd.CommandType = CommandType.Text

            If Conn.State = ConnectionState.Open Then
            Else
                Conn.Open()
            End If

            dr = cmd.ExecuteReader

            dgvScores.Rows.Clear()
            dgvScores.Columns.Clear()

            dgvScores.Columns.Add("intStudentId", "ID")
            dgvScores.Columns.Add("strName", "Name")
            dgvScores.Columns.Add("strCompanyName", "Company")

            dgvScores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvScores.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(2) As Object
                dr.GetValues(objCells)
                dgvScores.Rows.Add(objCells)
            End While

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    'just a shell
    Private Sub BindDataGridScores()

        '    Dim strSQL As String

        '    Try

        '        dsScores = New DataSet

        '        If txtsortnbr.Text = "1" Then
        '            strSQL = "SELECT * from smokeSchoolScores order by strLocationTerm, strName"
        '        End If
        '        If txtsortnbr.Text = "2" Then
        '            strSQL = "SELECT * from smokeSchoolReservation " & _
        '                     "where strLocationTerm = '" & txtSortItem.Text & "' " & _
        '                     "order by strLocationTerm, strName"
        '        End If
        '        daScores = New OracleDataAdapter
        '        daScores.SelectCommand = New OracleCommand
        '        daScores.SelectCommand.Connection = conn
        '        daScores.SelectCommand.CommandText = strSQL

        '        If conn.State = ConnectionState.Open Then
        '        Else
        '            conn.Open()
        '        End If

        '        daScores.Fill(dsScores, "score")
        '        If conn.State = ConnectionState.Open Then
        '            conn.Close()
        '        End If

        '        dgrScores.DataSource = dsScores
        '        dgrScores.DataMember = "score"

        '        'Formatting our DataGrid
        '        Dim objGrid As New DataGridTableStyle
        '        Dim objTextCol As New DataGridTextBoxColumn

        '        objGrid.AlternatingBackColor = Color.WhiteSmoke
        '        objGrid.MappingName = "res"
        '        objGrid.RowHeadersVisible = False
        '        objGrid.AllowSorting = True
        '        objGrid.ReadOnly = True

        '        objTextCol = New DataGridTextBoxColumn
        '        objTextCol.MappingName = "intStudentID"
        '        objTextCol.HeaderText = "ID"
        '        objTextCol.Width = 60
        '        objGrid.GridColumnStyles.Add(objTextCol)

        '        objTextCol = New DataGridTextBoxColumn
        '        objTextCol.MappingName = "strName"
        '        objTextCol.HeaderText = "Name"
        '        objTextCol.Width = 100
        '        objGrid.GridColumnStyles.Add(objTextCol)

        '        objTextCol = New DataGridTextBoxColumn
        '        objTextCol.MappingName = "strFacilityName"
        '        objTextCol.HeaderText = "Facility"
        '        objTextCol.Width = 150
        '        objGrid.GridColumnStyles.Add(objTextCol)

        '        objTextCol = New DataGridTextBoxColumn
        '        objTextCol.MappingName = "strPhoneNumber"
        '        objTextCol.HeaderText = "Phone"
        '        objTextCol.Width = 80
        '        objGrid.GridColumnStyles.Add(objTextCol)

        '        objTextCol = New DataGridTextBoxColumn
        '        objTextCol.MappingName = "strLocationTerm"
        '        objTextCol.HeaderText = "Term/Location"
        '        objTextCol.Width = 120
        '        objGrid.GridColumnStyles.Add(objTextCol)

        '        'Applying the above formating 
        '        dgrScores.TableStyles.Clear()
        '        dgrScores.TableStyles.Add(objGrid)

        '        'Setting the DataGrid Caption, which defines the table title
        '        dgrScores.CaptionText = "Reservation Schedule"
        '        dgrScores.ColumnHeadersVisible = True

        '        dgrScores.DataSource = dsScores
        '        dgrScores.DataMember = "score"

        '    Catch ex As Exception
        '        MsgBox(ex.ToString)
        '    Finally
        '        txtsortnbr.Text = "1"
        '        If conn.State = ConnectionState.Open Then
        '            conn.Close()
        '        End If
        '    End Try



    End Sub
#End Region
#Region " SETUP Routines "
    Private Sub btnSaveSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSchedule.Click
        Try
            saveSetupSchedule()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub saveSetupSchedule()
        Try
            Dim year As String = cboYear.SelectedItem
            Dim season As String = cboSeason.SelectedItem
            Dim city As String = cboCity.SelectedItem
            Dim startDate As String = dtpStartDate.Value.Month.ToString & "/" & dtpStartDate.Value.Day.ToString & "/" & dtpStartDate.Value.Year.ToString
            Dim endDate As String = dtpEndDate.Value.Month.ToString & "/" & dtpEndDate.Value.Day.ToString & "/" & dtpEndDate.Value.Year.ToString
            Dim display As String = cboDisplay.SelectedItem
            Dim schedule As String
            Dim scheduleShort As String
            Dim checkDate As String = Now.Month.ToString & "/" & Now.Day.ToString & "/" & Now.Year.ToString

            If cboYear.SelectedIndex = 0 Then
                MsgBox("You must select a Year")
                Exit Sub
            End If
            If cboSeason.SelectedIndex = 0 Then
                MsgBox("You must select a Season")
                Exit Sub
            End If
            If cboCity.SelectedIndex = 0 Then
                MsgBox("You must select a City")
                Exit Sub
            End If
            If startDate = checkDate Then
                MsgBox("You must select a Start Date")
                Exit Sub
            End If
            If endDate = checkDate Then
                MsgBox("You must select an End Date")
                Exit Sub
            End If
            If cboDisplay.SelectedIndex = 0 Then
                MsgBox("You must select 'YES/NO'")
                Exit Sub
            End If
            startDate = Format(CDate(startDate), "MMM-dd-yyyy")
            endDate = Format(CDate(endDate), "MMM-dd-yyyy")
            'startDate = convertDate(CDate(startDate))
            'endDate = convertDate(CDate(endDate))

            scheduleShort = year & " " & season & " - " & city
            schedule = scheduleShort & ": " & startDate & " thru " & endDate

            SQL = "Select strSchedule " & _
            "from " & DBNameSpace & ".SmokeSchoolSchedule " & _
            "where strSchedule = '" & schedule & "' "

            cmd = New OracleCommand(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                SQL = "update airbranch.SmokeSchoolSchedule " & _
                "set strYear = '" & Replace(year, "'", "''") & "', " & _
                "strTerm = '" & Replace(season, "'", "''") & "', " & _
                "strLocation = '" & Replace(city, "'", "''") & "', " & _
                "strStartDate = '" & Replace(startDate, "'", "''") & "', " & _
                "strEndDate = '" & Replace(endDate, "'", "''") & "', " & _
                "strSchedule = '" & Replace(schedule, "'", "''") & "', " & _
                "strScheduleShort = '" & Replace(scheduleShort, "'", "''") & "', " & _
                "strDisplay = '" & Replace(display, "'", "''") & "'" & _
                " where  strSchedule = '" & Replace(schedule, "'", "''") & "'"
            Else
                SQL = "Insert Into airbranch.SmokeSchoolSchedule (strYear, " & _
                "strTerm, " & _
                "strLocation, " & _
                "strStartDate, " & _
                "strEndDate, " & _
                "strSchedule, " & _
                "strScheduleShort, " & _
                "strDisplay) " & _
                " Values (" & _
                "'" & Replace(year, "'", "''") & "', " & _
                "'" & Replace(season, "'", "''") & "', " & _
                "'" & Replace(city, "'", "''") & "', " & _
                "'" & Replace(startDate, "'", "''") & "', " & _
                "'" & Replace(endDate, "'", "''") & "', " & _
                "'" & Replace(schedule, "'", "''") & "', " & _
                "'" & Replace(scheduleShort, "'", "''") & "', " & _
                "'" & Replace(display, "'", "''") & "') "
            End If

            'Open the connection to the database and write the record
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            MsgBox("Schedule " & schedule & " has been saved.", MsgBoxStyle.Information, "Setup Save Routine")

            BindDataGridSchedule()
            LoadLocTerm1()
            LoadLocTerm3()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnDeleteSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSchedule.Click
        Try
            deleteSetupSchedule()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub deleteSetupSchedule()
        Try
            Dim year As String = cboYear.SelectedItem
            Dim season As String = cboSeason.SelectedItem
            Dim city As String = cboCity.SelectedItem
            Dim startDate As String = dtpStartDate.Value.Month.ToString & "/" & dtpStartDate.Value.Day.ToString & "/" & dtpStartDate.Value.Year.ToString
            Dim endDate As String = dtpEndDate.Value.Month.ToString & "/" & dtpEndDate.Value.Day.ToString & "/" & dtpEndDate.Value.Year.ToString
            Dim display As String = cboDisplay.SelectedItem
            Dim schedule As String
            Dim scheduleShort As String
            Dim checkDate As String = Now.Month.ToString & "/" & Now.Day.ToString & "/" & Now.Year.ToString

            Dim SQL As String

            If cboYear.SelectedIndex = 0 Then
                MsgBox("You must select a Year")
                Exit Sub
            End If
            If cboSeason.SelectedIndex = 0 Then
                MsgBox("You must select a Season")
                Exit Sub
            End If
            If cboCity.SelectedIndex = 0 Then
                MsgBox("You must select a City")
                Exit Sub
            End If
            If startDate = checkDate Then
                MsgBox("You must select a Start Date")
                Exit Sub
            End If
            If endDate = checkDate Then
                MsgBox("You must select an End Date")
                Exit Sub
            End If
            If cboDisplay.SelectedIndex = 0 Then
                MsgBox("You must select 'YES/NO'")
                Exit Sub
            End If
            startDate = Format(CDate(startDate), "MMM-dd-yyyy")
            endDate = Format(CDate(endDate), "MMM-dd-yyyy")
            'startDate = convertDate(CDate(startDate))
            'endDate = convertDate(CDate(endDate))

            scheduleShort = year & " " & season & " - " & city
            schedule = scheduleShort & ": " & startDate & " thru " & endDate

            SQL = "delete from airbranch.SmokeSchoolSchedule where strSchedule = '" & schedule & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            MsgBox("Schedule " & schedule & " has been Deleted.", MsgBoxStyle.Information, "Setup Delete Routine")

            BindDataGridSchedule()
            LoadLocTerm1()
            LoadLocTerm3()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub dtpStartDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpStartDate.TextChanged
        Dim date1 As Date
        Dim date2 As Date
        Try
            date1 = dtpStartDate.Value
            date2 = DateAdd(DateInterval.Day, 2, date1)
            dtpEndDate.Value = date2
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
#End Region
#Region " RESERVATION Routines "
    Private Sub btnSaveRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveRes.Click
        Try
            ErrorFlag = "no"
            saveReservation()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub saveReservation()
        Try
            Dim studentID As Decimal = 0
            Dim fname As String = txtFirstName1.Text
            Dim lname As String = txtLastName1.Text
            Dim title As String = txtTitle1.Text
            Dim salutation As String = cboSalutation.SelectedItem
            Dim facility As String = txtCompanyName1.Text
            Dim address1 As String = txtAddress11.Text
            Dim address2 As String = txtAddress12.Text
            Dim city As String = txtCity1.Text
            Dim state As String = txtState1.Text.ToUpper
            Dim zip As String = txtZip1.Text
            Dim phone As String = txtPhoneAC1.Text & txtPhoneEXC1.Text & txtPhoneNBR1.Text & txtPhoneExt1.Text
            Dim phoneac As String = txtPhoneAC1.Text
            Dim phoneexc As String = txtPhoneEXC1.Text
            Dim phonenbr As String = txtPhoneNBR1.Text
            Dim extention As String = txtPhoneExt1.Text
            Dim fax As String = txtFaxAC1.Text & txtFaxEXC1.Text & txtFaxNBR1.Text
            Dim faxac As String = txtFaxAC1.Text
            Dim faxexc As String = txtFaxEXC1.Text
            Dim faxnbr As String = txtFaxNBR1.Text
            Dim email As String = txtEmail1.Text
            'Dim day1 As String= getToday() 
            Dim day1 As String = Format(Now.Date, "dd-MMM-yyyy")
            Dim hr As String = Now.Hour
            Dim min As String = Now.Minute
            Dim sec As String = Now.Second
            Dim time24 As String = hr & ":" & min & ":" & sec
            Dim TransactionDate As String = day1
            Dim RequestDate As String
            Dim LocationTerm As String = cboSchedule1.SelectedItem
            Dim ConfirmNbr As String = txtConfirmation.Text
            Dim lecture As String = cboLecture.SelectedItem
            Dim schedule As String = txtSchedule1.Text
            Dim msgResult As Integer = 0
            Dim messageOut As String = ""

            checkForEmptyTextBoxes()

            If ErrorFlag = "yes" Then
                Exit Sub
            Else
                txtSchedule.Text = cboSchedule1.SelectedItem

                If txtID1.Text = "" Then
                    SQL = "select max(numUserID) as strUserNumber " & _
                    "from airbranch.SmokeSchoolReservation " & _
                    "where strlocationDate = '" & LocationTerm & "'"

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If

                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strUserNumber")) Then
                            studentID = 0
                        Else
                            studentID = dr.Item("strUserNumber")
                        End If
                    End While
                    dr.Close()
                    studentID = studentID + 1
                Else
                    studentID = txtID1.Text
                End If

                phone = txtPhoneAC1.Text & txtPhoneEXC1.Text & txtPhoneNBR1.Text & txtPhoneExt1.Text
                fax = txtFaxAC1.Text & txtFaxEXC1.Text & txtFaxNBR1.Text

                fname = txtFirstName1.Text
                lname = txtLastName1.Text
                'Name = lname & ", " & fname

                If txtConfirmation.Text = "" Then
                    'Create confirmation number
                    'RequestDate = toOraDate(Now.Date)
                    RequestDate = Format(Now.Date, "dd-MMM-yyyy")
                    ConfirmNbr = "1111" & Now.Date.ToString("yyyy MM dd").Replace(" ", "")
                    ConfirmNbr = ConfirmNbr & time24.Replace(":", "")
                End If

                If txtID1.Text = "" And txtConfirmation.Text = "" Then
                    txtFirstName1X.Text = txtFirstName1.Text
                    txtLastName1X.Text = txtLastName1.Text
                End If

                SQL = "select * " & _
                "from " & DBNameSpace & ".SmokeSchoolReservation " & _
                "where upper(strLastName) = upper('" & Replace(txtLastName1X.Text, "'", "''") & "') " & _
                "and upper(strFirstName) = upper('" & Replace(txtFirstName1X.Text, "'", "''") & "') " & _
                "and upper(strLocationDate) = upper('" & Replace(txtSchedule.Text, "'", "''") & "') "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    messageOut = "Student: " & fname & " " & lname & " is already registered in the database" & vbCrLf & " Do "
                    messageOut += "you want to Update this Student?"
                    msgResult = MsgBox(messageOut, MsgBoxStyle.YesNo)

                    If msgResult = 6 Then
                        SQL = "update airbranch.SmokeSchoolReservation " & _
                              "set numUserID = " & studentID & ", " & _
                              "strFirstName = '" & Replace(fname, "'", "''") & "', " & _
                              "strLastName = '" & Replace(lname, "'", "''") & "', " & _
                              "strTitle = '" & Replace(title, "'", "''") & "', " & _
                              "strSalutation = '" & Replace(salutation, "'", "''") & "', " & _
                              "strCompanyName = '" & Replace(facility, "'", "''") & "', " & _
                              "strAddress1 = '" & Replace(address1, "'", "''") & "', " & _
                              "strAddress2 = '" & Replace(address2, "'", "''") & "', " & _
                              "strCity = '" & Replace(city, "'", "''") & "', " & _
                              "strState = '" & Replace(state, "'", "''") & "', " & _
                              "strZip = '" & Replace(zip, "'", "''") & "', " & _
                              "strPhoneNumber = '" & Replace(phone, "'", "''") & "', " & _
                              "strFax = '" & Replace(fax, "'", "''") & "', " & _
                              "strLectureYesNo = '" & lecture & "', " & _
                              "strConfirmationNbr = '" & ConfirmNbr & "', " & _
                              "strLocationDate = '" & txtSchedule1.Text & "', " & _
                              "datTransactionDate = '" & TransactionDate & "' " & _
                              " where upper(strLastName) = upper('" & Replace(txtLastName1X.Text, "'", "''") & "')" & _
                              " and upper(strLocationDate) = upper('" & Replace(txtSchedule1.Text, "'", "''") & "')" & _
                              " and upper(strFirstName) = upper('" & Replace(txtFirstName1X.Text, "'", "''") & "')"

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        txtSortItem.Text = cboSchedule1.SelectedItem
                        txtsortnbr.Text = "2"
                        BindDataGridReservation()
                        'txtSortItem.Text = cboSchedule1.SelectedItem
                        'txtsortnbr.Text = "2"
                        'BindDataGridReservation1()

                        numberOfStudents1()
                        numberOfAttendingLecture()

                        ErrorFlag = "no"

                        MsgBox("Student: " & fname & " " & lname & " was updated in the Reservation Table")
                        clearRes()

                    End If
                Else
                    SQL = "insert into airbranch.SmokeSchoolReservation " & _
                          "(numUserID, " & _
                          "strFirstName, " & _
                          "strLastName, " & _
                          "strTitle, " & _
                          "strSalutation, " & _
                          "strCompanyName, " & _
                          "strAddress1, " & _
                          "strAddress2, " & _
                          "strCity, " & _
                          "strState, " & _
                          "strZip, " & _
                          "strPhoneNumber, " & _
                          "strFax, " & _
                          "strEmail, " & _
                          "strLectureYesNO, " & _
                          "strConfirmationNbr, " & _
                          "strLocationDate, " & _
                          "datTransactionDate) " & _
                          "values (" & _
                          "'" & Replace(studentID, "'", "''") & "', " & _
                          "'" & Replace(fname, "'", "''") & "', " & _
                          "'" & Replace(lname, "'", "''") & "', " & _
                          "'" & Replace(title, "'", "''") & "', " & _
                          "'" & Replace(salutation, "'", "''") & "', " & _
                          "'" & Replace(facility, "'", "''") & "', " & _
                          "'" & Replace(address1, "'", "''") & "', " & _
                          "'" & Replace(address2, "'", "''") & "', " & _
                          "'" & Replace(city, "'", "''") & "', " & _
                          "'" & Replace(state, "'", "''") & "', " & _
                          "'" & Replace(zip, "'", "''") & "', " & _
                          "'" & Replace(phone, "'", "''") & "', " & _
                          "'" & Replace(fax, "'", "''") & "', " & _
                          "'" & Replace(email, "'", "''") & "', " & _
                          "'" & Replace(lecture, "'", "''") & "', " & _
                          "'" & Replace(ConfirmNbr, "'", "''") & "', " & _
                          "'" & Replace(LocationTerm, "'", "''") & "', " & _
                          "to_date('" & TransactionDate & "', 'dd-mon-yyyy hh24:mi:ss')) "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    txtSortItem.Text = cboSchedule1.SelectedItem
                    txtsortnbr.Text = "2"
                    BindDataGridReservation()
                    'txtSortItem.Text = cboSchedule1.SelectedItem
                    'txtsortnbr.Text = "2"
                    'BindDataGridReservation1()

                    numberOfStudents1()
                    numberOfAttendingLecture()

                    ErrorFlag = "no"

                    MsgBox("Student: " & fname & " " & lname & " was added to the Reservation Table")
                    clearRes()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            'clearRes()
        End Try
    End Sub
    Private Sub checkForEmptyTextBoxes()
        Try
            If txtSchedule1.Text = "" Then
                If cboSchedule1.SelectedIndex = 1 Or cboSchedule1.SelectedIndex = 0 Then
                    MsgBox("You must select a Term and Location")
                    ErrorFlag = "yes"
                    Exit Sub
                End If
            End If
            If txtLastName1.Text = "" Then
                MsgBox("You must select a student or enter student information manually")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If txtFirstName1.Text = "" Then
                MsgBox("You must select a student or enter student information manually")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If txtTitle1.Text = "" Then
                MsgBox("You must enter a title")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If cboSalutation.SelectedIndex = 0 Then
                MsgBox("You must select a Salutation")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If txtCompanyName1.Text = "" Then
                MsgBox("You must enter a Company")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If txtAddress11.Text = "" Then
                MsgBox("You must enter a address for 'Address 1'")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If txtCity1.Text = "" Then
                MsgBox("You must enter a city")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If txtState1.Text = "" Then
                MsgBox("You must enter a state")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If txtZip1.Text = "" Then
                MsgBox("You must enter a zip")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If txtPhoneAC1.Text = "" Then
                MsgBox("You must enter a phone area code")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If txtPhoneEXC1.Text = "" Then
                MsgBox("You must enter a phone exchange")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If txtPhoneNBR1.Text = "" Then
                MsgBox("You must enter a phone number")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If txtFaxAC1.Text = "" Then
                MsgBox("YOu must enter a fax area code")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If txtFaxEXC1.Text = "" Then
                MsgBox("You must enter a fax exchange")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If txtFaxNBR1.Text = "" Then
                MsgBox("You must enter a fax number")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If txtEmail1.Text = "" Then
                MsgBox("You must enter an email address")
                ErrorFlag = "yes"
                Exit Sub
            End If
            If cboSchedule1.SelectedIndex = 0 Or cboSchedule1.SelectedIndex = 1 Then
                MsgBox("You must select a location and term")
                ErrorFlag = "yes"
                Exit Sub
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub btnDeleteRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteRes.Click
        Try
            deleteReservation()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub deleteReservation()
        Try
            Dim studentID As String = txtID1.Text
            Dim lName As String = txtLastName1.Text
            Dim fName As String = txtFirstName1.Text

            Dim x As String
            x = cboSchedule1.SelectedItem
            If cboSchedule1.SelectedIndex = 0 Or cboSchedule1.SelectedIndex = 1 Then
                MsgBox("You must select a Location/Term")
            Else

                If lName <> "" And fName <> "" And txtID1.Text <> "" Then

                    lName = Replace(lName, "'", "''")
                    fName = Replace(fName, "'", "''")

                    SQL = "Delete from airbranch.SmokeSchoolReservation " & _
                                    " where upper(strLastName) = upper('" & lName & "') " & _
                                    " and upper(strFirstName) = upper('" & fName & "') " & _
                                    " and upper(numUserID) = upper('" & studentID & "') " & _
                                    "and strLocationDate = '" & cboSchedule1.SelectedItem & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    BindDataGridReservation()

                    txtSortItem.Text = cboSchedule1.SelectedItem
                    txtsortnbr.Text = "2"
                    BindDataGridReservation()
                    'txtSortItem.Text = cboSchedule1.SelectedItem
                    'txtsortnbr.Text = "2"
                    'BindDataGridReservation1()
                    clearRes()

                    numberOfStudents1()
                    numberOfAttendingLecture()

                    MsgBox("Student: " & lName & ", " & fName & " has been deleted.")
                Else
                    MsgBox("You must select a student")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub getUserNumber()
        Try
            Dim userNumber As String = "0"
            Dim locationDate As String = cboSchedule1.SelectedItem

            updateUserNumber()

            If Conn.State = ConnectionState.Open Then
            Else
                Conn.Open()
            End If

            SQL = "select max(numUserID) as strUserNumber " & _
            "from airbranch.SmokeSchoolReservation " & _
            "where strlocationDate = '" & locationDate & "'"

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read
                userNumber = dr.Item("strUserNumber")
            End While
            dr.Close()

            userNumber = userNumber + 1
            txtID1.Text = userNumber
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub updateUserNumber()
        Try
            Dim studentID As Decimal = 0
            Dim FirstName As String = ""
            Dim LastName As String = ""
            Dim locationDate As String = ""

            If cboSchedule1.SelectedIndex = 0 Then
                MsgBox("You must click on a Location/Term.")
            Else
                locationDate = cboSchedule1.SelectedItem
                SQL = "select * from airbranch.SmokeSchoolReservation " & _
                "where upper(strLocationDate) = upper('" & Replace(locationDate, "'", "''") & "') "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    studentID = studentID + 1
                    FirstName = dr.Item("strFirstName")
                    LastName = dr.Item("strLastName")

                    SQL2 = "update airbranch.SmokeSchoolReservation " & _
                          "set numUserID = " & studentID & " " & _
                          " where upper(strLocationDate) = upper('" & Replace(locationDate, "'", "''") & "')" & _
                          " and upper(strFirstName) = upper('" & Replace(FirstName, "'", "''") & "')" & _
                          " and upper(strLastName) = upper('" & Replace(LastName, "'", "''") & "')"

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End While
                dr.Close()

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region
#Region " Move Reservation Routines "
    Private Sub btnMoveRes2Scores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveRes2Scores.Click
        Try
            moveRes2Scores()
            BindDataGridScores1()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub moveRes2Scores()
        Try
            Dim studentID As Integer = 0
            Dim name As String = ""
            Dim lastName As String = ""
            Dim firstName As String = ""
            Dim CompanyName As String = ""
            Dim scoreKey As String = ""
            Dim locationDate As String = ""
            Dim email As String = ""
            Dim QuizScore As String = ""
            Dim Comment As String = "NA"
            Dim Run1 As String = "No Test"
            Dim Run2 As String = "No Test"
            Dim Run3 As String = "No Test"
            Dim Run4 As String = "No Test"
            Dim Run5 As String = "No Test"
            Dim Run6 As String = "No Test"
            Dim Run7 As String = "No Test"
            Dim Run8 As String = "No Test"
            Dim Run9 As String = "No Test"
            Dim Run10 As String = "No Test"
            'Dim day1 As String = getToday()
            Dim day1 As String = Format(CDate(Now.Date), "dd-MMM-yyyy")
            Dim hr As String = Now.Hour
            Dim min As String = Now.Minute
            Dim sec As String = Now.Second
            Dim time24 As String = hr & ":" & min & ":" & sec
            Dim TransactionDate As String = day1
            Dim colon As Integer = 0

            If txtSchedule2.Text = "" Then
                MsgBox("You must select a Location/Term")
                Exit Sub
            Else
                SQL = "select * from airbranch.SmokeSchoolReservation " & _
                "where strLocationDate = '" & txtSchedule2.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("numUserID")) Then
                        studentID = 0
                    Else
                        studentID = dr.Item("numUserID")
                    End If
                    If IsDBNull(dr("strLocationDate")) Then
                        locationDate = ""
                    Else
                        locationDate = dr.Item("strLocationDate")
                        colon = InStr(locationDate, ":")
                        locationDate = Mid(locationDate, 1, colon - 1)
                    End If
                    If IsDBNull(dr.Item("strEmail")) Then
                        email = ""
                    Else
                        email = dr.Item("strEmail")
                    End If
                    If IsDBNull(dr.Item("strCompanyName")) Then
                        CompanyName = ""
                    Else
                        CompanyName = dr.Item("strCompanyName")
                    End If
                    If IsDBNull(dr.Item("strLastName")) Then
                        lastName = ""
                    Else
                        lastName = dr.Item("strLastName")
                    End If
                    If IsDBNull(dr.Item("strFirstName")) Then
                        firstName = ""
                    Else
                        firstName = dr.Item("strFirstName")
                    End If

                    name = lastName & ", " & firstName

                    If studentID = 0 Then
                        MsgBox("Student:  " & name & "has an ID of zero")
                    Else
                        scoreKey = locationDate & " - " & email

                        SQL = "Select * from airbranch.SmokeSchoolScores " & _
                               "where strscoreKey = '" & scoreKey & "' " & _
                               "and intStudentID = " & studentID

                        cmd2 = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        recExist = dr2.Read
                        dr2.Close()

                        If recExist = True Then

                        Else
                            SQL = "insert into airbranch.SmokeSchoolScores ( " & _
                                   "intStudentID, " & _
                                   "strName, " & _
                                   "strCompanyName, " & _
                                   "strScoreKey, " & _
                                   "strLocationTerm, " & _
                                   "strComment, " & _
                                   "strRun1, " & _
                                   "strRun2, " & _
                                   "strRun3, " & _
                                   "strRun4, " & _
                                   "strRun5, " & _
                                   "strRun6, " & _
                                   "strRun7, " & _
                                   "strRun8, " & _
                                   "strRun9, " & _
                                   "strRun10, " & _
                                   "strLastName, " & _
                                   "strFirstName, " & _
                                   "datTransactionDate) " & _
                                   "values (" & studentID & ", " & _
                                   "'" & Replace(name, "'", "''") & "', " & _
                                   "'" & Replace(CompanyName, "'", "''") & "', " & _
                                   "'" & Replace(scoreKey, "'", "''") & "', " & _
                                   "'" & Replace(locationDate, "'", "''") & "', " & _
                                   "'" & Replace(Comment, "'", "''") & "', " & _
                                   "'" & Replace(Run1, "'", "''") & "', " & _
                                   "'" & Replace(Run2, "'", "''") & "', " & _
                                   "'" & Replace(Run3, "'", "''") & "', " & _
                                   "'" & Replace(Run4, "'", "''") & "', " & _
                                   "'" & Replace(Run5, "'", "''") & "', " & _
                                   "'" & Replace(Run6, "'", "''") & "', " & _
                                   "'" & Replace(Run7, "'", "''") & "', " & _
                                   "'" & Replace(Run8, "'", "''") & "', " & _
                                   "'" & Replace(Run9, "'", "''") & "', " & _
                                   "'" & Replace(Run10, "'", "''") & "', " & _
                                   "'" & Replace(lastName, "'", "''") & "', " & _
                                   "'" & Replace(firstName, "'", "''") & "', " & _
                                   "'" & TransactionDate & "')"

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd.ExecuteReader
                            dr2.Close()
                        End If
                    End If
                End While
                dr.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnMoveResStudent2Scores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveResStudent2Scores.Click
        Try
            moveResStudent2Scores()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub moveResStudent2Scores()
        Try
            Dim studentID As Integer = txtID1.Text
            Dim name As String = txtLastName1.Text & ", " & txtFirstName1.Text
            Dim lastName As String = txtLastName1.Text
            Dim firstName As String = txtFirstName1.Text
            Dim CompanyName As String = txtCompanyName1.Text
            Dim locationDate As String = cboSchedule1.SelectedItem
            Dim scoreKey As String = locationDate & " - " & name
            Dim email As String = txtEmail1.Text
            Dim Comment As String = "NA"
            Dim Run1 As String = "No Test"
            Dim Run2 As String = "No Test"
            Dim Run3 As String = "No Test"
            Dim Run4 As String = "No Test"
            Dim Run5 As String = "No Test"
            Dim Run6 As String = "No Test"
            Dim Run7 As String = "No Test"
            Dim Run8 As String = "No Test"
            Dim Run9 As String = "No Test"
            Dim Run10 As String = "No Test"
            'Dim day1 As String = getToday()
            Dim day1 As String = Format(CDate(Now.Date), "dd-MMM-yyyy")
            Dim hr As String = Now.Hour
            Dim min As String = Now.Minute
            Dim sec As String = Now.Second
            Dim time24 As String = hr & ":" & min & ":" & sec
            Dim TransactionDate As String = day1
            Dim colon As Integer = 0

            If studentID = 0 Then
                MsgBox("Student:  " & name & "has an ID of zero")
                Exit Sub
            Else
                name = lastName & ", " & firstName
                colon = InStr(locationDate, ":")
                locationDate = Mid(locationDate, 1, colon - 1)
                name = Replace(name, "'", "''")
                scoreKey = locationDate & " - " & name
                SQL = "Select * from airbranch.SmokeSchoolScores " & _
                       "where strLocationTerm = '" & locationDate & "' " & _
                       "and strName = '" & name & "'"

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    MsgBox("Scores for " & name & " already exist for " & locationDate)
                    Exit Sub
                Else
                    SQL = "insert into airbranch.SmokeSchoolScores ( " & _
                           "intStudentID, " & _
                           "strName, " & _
                           "strCompanyName, " & _
                           "strScoreKey, " & _
                           "strLocationTerm, " & _
                           "strComment, " & _
                           "strRun1, " & _
                           "strRun2, " & _
                           "strRun3, " & _
                           "strRun4, " & _
                           "strRun5, " & _
                           "strRun6, " & _
                           "strRun7, " & _
                           "strRun8, " & _
                           "strRun9, " & _
                           "strRun10, " & _
                           "strLastName, " & _
                           "strFirstName, " & _
                           "datTransactionDate) " & _
                           "values (" & studentID & ", " & _
                           "'" & name & "', " & _
                           "'" & Replace(CompanyName, "'", "''") & "', " & _
                           "'" & Replace(scoreKey, "'", "''") & "', " & _
                           "'" & Replace(locationDate, "'", "''") & "', " & _
                           "'" & Replace(Comment, "'", "''") & "', " & _
                           "'" & Replace(Run1, "'", "''") & "', " & _
                           "'" & Replace(Run2, "'", "''") & "', " & _
                           "'" & Replace(Run3, "'", "''") & "', " & _
                           "'" & Replace(Run4, "'", "''") & "', " & _
                           "'" & Replace(Run5, "'", "''") & "', " & _
                           "'" & Replace(Run6, "'", "''") & "', " & _
                           "'" & Replace(Run7, "'", "''") & "', " & _
                           "'" & Replace(Run8, "'", "''") & "', " & _
                           "'" & Replace(Run9, "'", "''") & "', " & _
                           "'" & Replace(Run10, "'", "''") & "', " & _
                           "'" & Replace(lastName, "'", "''") & "', " & _
                           "'" & Replace(firstName, "'", "''") & "', " & _
                           "'" & TransactionDate & "')"

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    MsgBox("Student " & name & " was moved to the Scores table")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnRemoveRes2Scores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRes2Scores.Click
        Try
            removeResFromScores()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub removeResFromScores()
        Try
            Dim colon As Integer = InStr(txtSchedule2.Text, ":")
            Dim locationTerm As String = Mid(txtSchedule2.Text, 1, colon - 1)

            If txtSchedule2.Text = "" Then
                MsgBox("You must select a Location/Term")
                Exit Sub
            Else
                SQL = "Delete from airbranch.SmokeSchoolScores " & _
                " where strlocationTerm = '" & locationTerm & "'"

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                BindDataGridScores1()
                MsgBox("Class: " & locationTerm & " has been deleted.")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnCache2Res_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCache2Res.Click
        Try
            moveCacheToReservation()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub moveCacheToReservation()
        Try
            txtID1.Text = txtCacheID.Text
            txtLastName1.Text = txtCacheLastName.Text
            txtFirstName1.Text = txtCacheFirstName.Text
            txtTitle1.Text = txtCacheTitle.Text
            cboSalutation.SelectedItem = txtCacheSalutation.Text
            txtCompanyName1.Text = txtCacheCompanyName.Text
            txtAddress11.Text = txtCacheAddress1.Text
            txtAddress12.Text = txtCacheAddress2.Text
            txtCity1.Text = txtCacheCity.Text
            txtState1.Text = txtCacheState.Text
            txtZip1.Text = txtCacheZip.Text
            txtPhoneAC1.Text = txtCachePhoneAC.Text
            txtPhoneEXC1.Text = txtCachePhoneEXC.Text
            txtPhoneNBR1.Text = txtCachePhoneNBR.Text
            txtPhoneExt1.Text = txtCachePhoneExt.Text
            txtFaxAC1.Text = txtCacheFaxAC.Text
            txtFaxEXC1.Text = txtCacheFaxEXC.Text
            txtFaxNBR1.Text = txtCacheFaxNBR.Text
            txtEmail1.Text = txtCacheEmail.Text
            txtConfirmation.Text = txtCacheConfirmation.Text
            cboLecture.SelectedItem = txtCacheLecture.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnMove2Cache_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMove2Cache.Click
        Try
            moveReservationToCache()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub moveReservationToCache()
        Try
            txtCacheID.Text = txtID1.Text
            txtCacheLastName.Text = txtLastName1.Text
            txtCacheFirstName.Text = txtFirstName1.Text
            txtCacheTitle.Text = txtTitle1.Text
            txtCacheSalutation.Text = cboSalutation.SelectedItem
            txtCacheCompanyName.Text = txtCompanyName1.Text
            txtCacheAddress1.Text = txtAddress11.Text
            txtCacheAddress2.Text = txtAddress12.Text
            txtCacheCity.Text = txtCity1.Text
            txtCacheState.Text = txtState1.Text
            txtCacheZip.Text = txtZip1.Text
            txtCachePhoneAC.Text = txtPhoneAC1.Text
            txtCachePhoneEXC.Text = txtPhoneEXC1.Text
            txtCachePhoneNBR.Text = txtPhoneNBR1.Text
            txtCachePhoneExt.Text = txtPhoneExt1.Text
            txtCacheFaxAC.Text = txtFaxAC1.Text
            txtCacheFaxEXC.Text = txtFaxEXC1.Text
            txtCacheFaxNBR.Text = txtFaxNBR1.Text
            txtCacheEmail.Text = txtEmail1.Text
            txtCacheConfirmation.Text = txtConfirmation.Text
            txtCacheLecture.Text = cboLecture.SelectedItem
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region
#Region " Scores Routines "
    Private Sub btnSave3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave3.Click
        Try
            saveScores()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub saveScores()
        Try
            Dim StudentID As String = ""
            Dim name As String = txtStudentName3.Text
            Dim fname As String = txtFirstName3.Text
            Dim lname As String = txtLastName3.Text
            Dim email As String = ""
            Dim CompanyName As String = ""
            Dim ScoreKey As String = ""
            Dim LocationTerm As String = ""
            Dim PassFailNoShow As String = ""
            Dim QuizScore As String = ""
            Dim Comment As String = ""
            Dim Run1 As String = ""
            Dim Run2 As String = ""
            Dim Run3 As String = ""
            Dim Run4 As String = ""
            Dim Run5 As String = ""
            Dim Run6 As String = ""
            Dim Run7 As String = ""
            Dim Run8 As String = ""
            Dim Run9 As String = ""
            Dim Run10 As String = ""
            Dim VisualRestrictions As String = ""
            Dim datePassed As String = ""
            Dim space1 As Integer = 0
            'Dim day1 As String = getToday()
            Dim day1 As String = Format(CDate(Now.Date), "dd-MMM-yyyy")
            Dim hr As String = Now.Hour
            Dim min As String = Now.Minute
            Dim sec As String = Now.Second
            Dim time24 As String = hr & ":" & min & ":" & sec
            Dim TransactionDate As String = day1
            Dim colon As Integer = 0

            If cboPassFail.SelectedItem = "--" Then
                MsgBox("You must select [Pass], [Fail], or [No Show]")
                Exit Sub
            Else
                If cboVisionCorrection.SelectedItem = "--" Then
                    MsgBox("You must select [Yes] or [No]")
                    Exit Sub
                Else
                    If chbRun1A.Checked = True Then
                        Run1 = "Pass"
                    Else
                        If chbRun1B.Checked = True Then
                            Run1 = "Fail"
                        Else
                            If chbRun1C.Checked = True Then
                                Run1 = "No Test"
                            End If
                        End If
                    End If
                    If chbRun2A.Checked = True Then
                        Run2 = "Pass"
                    Else
                        If chbRun2B.Checked = True Then
                            Run2 = "Fail"
                        Else
                            If chbRun2C.Checked = True Then
                                Run2 = "No Test"
                            End If
                        End If
                    End If
                    If chbRun3A.Checked = True Then
                        Run3 = "Pass"
                    Else
                        If chbRun3B.Checked = True Then
                            Run3 = "Fail"
                        Else
                            If chbRun3C.Checked = True Then
                                Run3 = "No Test"
                            End If
                        End If
                    End If
                    If chbRun4A.Checked = True Then
                        Run4 = "Pass"
                    Else
                        If chbRun4B.Checked = True Then
                            Run4 = "Fail"
                        Else
                            If chbRun4C.Checked = True Then
                                Run4 = "No Test"
                            End If
                        End If
                    End If
                    If chbRun5A.Checked = True Then
                        Run5 = "Pass"
                    Else
                        If chbRun5B.Checked = True Then
                            Run5 = "Fail"
                        Else
                            If chbRun5C.Checked = True Then
                                Run5 = "No Test"
                            End If
                        End If
                    End If
                    If chbRun6A.Checked = True Then
                        Run6 = "Pass"
                    Else
                        If chbRun6B.Checked = True Then
                            Run6 = "Fail"
                        Else
                            If chbRun6C.Checked = True Then
                                Run6 = "No Test"
                            End If
                        End If
                    End If
                    If chbRun7A.Checked = True Then
                        Run7 = "Pass"
                    Else
                        If chbRun7B.Checked = True Then
                            Run7 = "Fail"
                        Else
                            If chbRun7C.Checked = True Then
                                Run7 = "No Test"
                            End If
                        End If
                    End If
                    If chbRun8A.Checked = True Then
                        Run8 = "Pass"
                    Else
                        If chbRun8B.Checked = True Then
                            Run8 = "Fail"
                        Else
                            If chbRun8C.Checked = True Then
                                Run8 = "No Test"
                            End If
                        End If
                    End If
                    If chbRun9A.Checked = True Then
                        Run9 = "Pass"
                    Else
                        If chbRun9B.Checked = True Then
                            Run9 = "Fail"
                        Else
                            If chbRun9C.Checked = True Then
                                Run9 = "No Test"
                            End If
                        End If
                    End If
                    If chbRun10A.Checked = True Then
                        Run10 = "Pass"
                    Else
                        If chbRun10B.Checked = True Then
                            Run10 = "Fail"
                        Else
                            If chbRun10C.Checked = True Then
                                Run10 = "No Test"
                            End If
                        End If
                    End If

                    LocationTerm = cboSchedule3.SelectedItem
                    colon = InStr(LocationTerm, ":")
                    LocationTerm = Mid(LocationTerm, 1, colon - 1)
                    StudentID = txtStudentID3.Text
                    ScoreKey = LocationTerm & " - " & name

                    PassFailNoShow = cboPassFail.SelectedItem
                    QuizScore = txtQuizGrade.Text
                    Comment = txtComments.Text
                    CompanyName = txtCompanyName3.Text
                    CompanyName = Replace(CompanyName, "'", "''")
                    VisualRestrictions = cboVisionCorrection.SelectedItem
                    datePassed = dtpDatePassed.Value
                    space1 = InStr(datePassed, " ")
                    If space1 > 0 Then
                        datePassed = Mid(datePassed, 1, space1 - 1)
                    End If

                    'datePassed = toOraDate(datePassed)
                    datePassed = Format(CDate(datePassed), "dd-MMM-yyyy")
                    name = Replace(name, "'", "''")

                    If txtScoreKey.Text <> "" Then
                        SQL = "Select strScoreKey " & _
                        "From " & DBNameSpace & ".SmokeSchoolScores " & _
                        "where strScoreKey =  '" & Replace(txtScoreKey.Text, "'", "''") & "' "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        i = 0
                        While dr.Read
                            If IsDBNull(dr.Item("strScoreKey")) Then
                            Else
                                i += 1
                            End If
                        End While
                        dr.Close()

                        Select Case i
                            Case 0
                                Exit Sub
                            Case 1
                                SQL = "update airbranch.SmokeSchoolScores " & _
                                "set strPassFailNoShow = '" & PassFailNoShow & "', " & _
                                "strQuizScore = '" & QuizScore & "', " & _
                                "strComment = '" & Replace(Comment, "'", "''") & "', " & _
                                "strRun1 = '" & Run1 & "', " & _
                                "strRun2 = '" & Run2 & "', " & _
                                "strRun3 = '" & Run3 & "', " & _
                                "strRun4 = '" & Run4 & "', " & _
                                "strRun5 = '" & Run5 & "', " & _
                                "strRun6 = '" & Run6 & "', " & _
                                "strRun7 = '" & Run7 & "', " & _
                                "strRun8 = '" & Run8 & "', " & _
                                "strRun9 = '" & Run9 & "', " & _
                                "strRun10 = '" & Run10 & "', " & _
                                "strFirstName = '" & Replace(fname, "'", "''") & "', " & _
                                "strLastName = '" & Replace(lname, "'", "''") & "', " & _
                                "strName = '" & Replace(name, "'", "''") & "', " & _
                                "strCompanyName = '" & Replace(CompanyName, "'", "''") & "', " & _
                                "strVisualRestrictions = '" & Replace(VisualRestrictions, "'", "''") & "', " & _
                                "strDatePassed = '" & datePassed & "', " & _
                                "datTransactionDate = '" & TransactionDate & "' " & _
                                "where strScoreKey = '" & txtScoreKey.Text & "' "
                            Case Else
                                Dim Result As DialogResult
                                Result = MessageBox.Show("There are " & i.ToString & " entries for this individual. " & vbCrLf & _
                                         "Do you want to update all the entries? " & vbCrLf & _
                                         "If you need assistance contact the Data Management Unit.", _
                                  "Smoke School", MessageBoxButtons.YesNoCancel, _
                                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                                Select Case Result
                                    Case Windows.Forms.DialogResult.Yes
                                        SQL = "update airbranch.SmokeSchoolScores " & _
                                        "set strPassFailNoShow = '" & PassFailNoShow & "', " & _
                                        "strQuizScore = '" & QuizScore & "', " & _
                                        "strComment = '" & Replace(Comment, "'", "''") & "', " & _
                                        "strRun1 = '" & Run1 & "', " & _
                                        "strRun2 = '" & Run2 & "', " & _
                                        "strRun3 = '" & Run3 & "', " & _
                                        "strRun4 = '" & Run4 & "', " & _
                                        "strRun5 = '" & Run5 & "', " & _
                                        "strRun6 = '" & Run6 & "', " & _
                                        "strRun7 = '" & Run7 & "', " & _
                                        "strRun8 = '" & Run8 & "', " & _
                                        "strRun9 = '" & Run9 & "', " & _
                                        "strRun10 = '" & Run10 & "', " & _
                                        "strFirstName = '" & Replace(fname, "'", "''") & "', " & _
                                        "strLastName = '" & Replace(lname, "'", "''") & "', " & _
                                        "strName = '" & Replace(name, "'", "''") & "', " & _
                                        "strCompanyName = '" & Replace(CompanyName, "'", "''") & "', " & _
                                        "strVisualRestrictions = '" & Replace(VisualRestrictions, "'", "''") & "', " & _
                                        "strDatePassed = '" & datePassed & "', " & _
                                        "datTransactionDate = '" & TransactionDate & "' " & _
                                        "where strScoreKey = '" & txtScoreKey.Text & "' "
                                    Case Else
                                        Exit Sub
                                End Select
                        End Select


                        'SQL = "update airbranch.SmokeSchoolScores " & _
                        '      "set strPassFailNoShow = '" & PassFailNoShow & "', " & _
                        '      "strQuizScore = '" & QuizScore & "', " & _
                        '      "strComment = '" & Replace(Comment, "'", "''") & "', " & _
                        '      "strRun1 = '" & Run1 & "', " & _
                        '      "strRun2 = '" & Run2 & "', " & _
                        '      "strRun3 = '" & Run3 & "', " & _
                        '      "strRun4 = '" & Run4 & "', " & _
                        '      "strRun5 = '" & Run5 & "', " & _
                        '      "strRun6 = '" & Run6 & "', " & _
                        '      "strRun7 = '" & Run7 & "', " & _
                        '      "strRun8 = '" & Run8 & "', " & _
                        '      "strRun9 = '" & Run9 & "', " & _
                        '      "strRun10 = '" & Run10 & "', " & _
                        '      "strFirstName = '" & fname & "', " & _
                        '      "strLastName = '" & lname & "', " & _
                        '      "strName = '" & name & "', " & _
                        '      "strCompanyName = '" & CompanyName & "', " & _
                        '      "strVisualRestrictions = '" & VisualRestrictions & "', " & _
                        '      "strDatePassed = '" & datePassed & "', " & _
                        '      "datTransactionDate = '" & TransactionDate & "' " & _
                        '      "where strLocationTerm = '" & LocationTerm & "' " & _
                        '      "and intStudentID = " & StudentID

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        BindDataGridScores3()

                        MsgBox("Student:  " & name & " data was saved", MsgBoxStyle.Information, "Save Score Info")

                        getStats()
                    Else
                        MessageBox.Show("Select an individual from the grid to the left before saving.")
                        Exit Sub
                    End If
                End If
            End If

        Catch ex As Exception
            MsgBox("Student:  " & Name & " data was saved" & ex.ToString, MsgBoxStyle.Information, "Save Score Info")
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            deleteScores()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub deleteScores()
        Try
            Dim name As String = txtStudentName3.Text
            Dim LocationTerm As String = cboSchedule3.SelectedItem
            Dim colon As Integer = 0
            Dim i As Integer = 0

            If txtScoreKey.Text <> "" Then
                SQL = "Select strScoreKey " & _
                "From " & DBNameSpace & ".SmokeSchoolScores " & _
                "where strScoreKey =  '" & Replace(txtScoreKey.Text, "'", "''") & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                i = 0
                While dr.Read
                    If IsDBNull(dr.Item("strScoreKey")) Then
                    Else
                        i += 1
                    End If
                End While
                dr.Close()

                Select Case i
                    Case 0

                        Exit Sub
                    Case 1
                        SQL = "Delete " & DBNameSpace & ".SmokeSchoolScores " & _
                        "where strScoreKey = '" & Replace(txtScoreKey.Text, "'", "''") & "' "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Case Else
                        Dim Result As DialogResult
                        Result = MessageBox.Show("There are " & i.ToString & " entries for this individual. " & vbCrLf & _
                                                 "Do you want to remove all the entries? " & vbCrLf & _
                                                 "If you need assistance contact the Data Management Unit.", _
                          "Smoke School", MessageBoxButtons.YesNoCancel, _
                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Select Case Result
                            Case Windows.Forms.DialogResult.Yes
                                SQL = "Delete " & DBNameSpace & ".SmokeSchoolScores " & _
                                "where strScoreKey = '" & Replace(txtScoreKey.Text, "'", "''") & "' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()
                            Case Else
                                Exit Sub
                        End Select
                End Select
                'colon = InStr(LocationTerm, ":")
                'LocationTerm = Mid(LocationTerm, 1, colon - 1)

                'name = Replace(name, "'", "''")

                'SQL = "delete from airbranch.SmokeSchoolScores " & _
                '"where strLocationTerm = '" & LocationTerm & "' " & _
                '"and strName = '" & name & "'"

                'cmd = New OracleCommand(SQL, conn)
                'If conn.State = ConnectionState.Closed Then
                '    conn.Open()
                'End If
                'dr = cmd.ExecuteReader
                'dr.Close()

                BindDataGridScores3()

                MsgBox("Student:  " & name & " data was deleted", MsgBoxStyle.Information, "Delete Score Info")

                getStats()
            Else
                MessageBox.Show("Select an individual from the grid to the left before deleting.")
                Exit Sub
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnSelectClass3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectClass3.Click
        Try
            If cboSchedule3.SelectedIndex = 0 Or cboSchedule3.SelectedIndex = 1 Then
                MsgBox("You must select a Location/Term")
                PanelStats.Visible = False
            Else
                If cboSchedule3.SelectedIndex = 1 Then
                    txtsortnbr.Text = "1"
                    PanelStats.Visible = False
                Else
                    txtSortItem.Text = cboSchedule3.SelectedItem
                    txtsortnbr.Text = "2"
                    dsScores3 = New DataSet
                    dsScores3.Clear()
                    BindDataGridScores3()
                    PanelStats.Visible = True
                    getStats()
                End If

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub getStats()
        Try
            Dim shortLocation As String = cboSchedule3.SelectedItem
            Dim count0 As Integer = 0
            Dim count1 As Integer = 0
            Dim count2 As Integer = 0
            Dim count3 As Integer = 0
            Dim colon As Integer = 0
            Dim PassFailNoShow As String

            colon = InStr(shortLocation, ":")
            shortLocation = Mid(shortLocation, 1, colon - 1)

            SQL = "select * from airbranch.SmokeSchoolScores " & _
            "where strLocationTerm = '" & shortLocation & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                SQL = "select * from airbranch.SmokeSchoolScores " & _
                "where strLocationTerm = '" & shortLocation & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    count0 = count0 + 1
                    If IsDBNull(dr.Item("strPassFailNoShow")) Then
                        PassFailNoShow = "NA"
                    Else
                        PassFailNoShow = dr.Item("strPassFailNoShow")
                    End If
                    If PassFailNoShow = "Pass" Then
                        count1 = count1 + 1
                    End If
                    If PassFailNoShow = "Fail" Then
                        count2 = count2 + 1
                    End If
                    If PassFailNoShow = "No Show" Then
                        count3 = count3 + 1
                    End If
                End While
                dr.Close()

                txtTotal.Text = count1 + count2 + count3
                txtPass.Text = count1
                txtFail.Text = count2
                txtNoShow.Text = count3
            Else
                MsgBox("No Scores for that Location/Term")
                txtPass.Text = ""
                txtFail.Text = ""
                txtNoShow.Text = ""
                PanelStats.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#Region "Checkbox Changes"
    Private Sub chbRun1A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun1A.Click
        Try
            chbRun1B.Checked = False
            chbRun1C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun1B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun1B.Click
        Try
            chbRun1A.Checked = False
            chbRun1C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun1C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun1C.Click
        Try
            chbRun1A.Checked = False
            chbRun1B.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun2A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun2A.Click
        Try
            chbRun2B.Checked = False
            chbRun2C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun2B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun2B.Click
        Try
            chbRun2A.Checked = False
            chbRun2C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun2C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun2C.Click
        Try
            chbRun2A.Checked = False
            chbRun2B.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun3A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun3A.Click
        Try
            chbRun3B.Checked = False
            chbRun3C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun3B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun3B.Click
        Try
            chbRun3A.Checked = False
            chbRun3C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun3C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun3C.Click
        Try
            chbRun3A.Checked = False
            chbRun3B.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun4A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun4A.Click
        Try
            chbRun4B.Checked = False
            chbRun4C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun4B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun4B.Click
        Try
            chbRun4A.Checked = False
            chbRun4C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun4C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun4C.Click
        Try
            chbRun4A.Checked = False
            chbRun4B.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun5A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun5A.Click
        Try
            chbRun5B.Checked = False
            chbRun5C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun5B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun5B.Click
        Try
            chbRun5A.Checked = False
            chbRun5C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun5C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun5C.Click
        Try
            chbRun5A.Checked = False
            chbRun5B.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun6A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun6A.Click
        Try
            chbRun6B.Checked = False
            chbRun6C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun6B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun6B.Click
        Try
            chbRun6A.Checked = False
            chbRun6C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun6C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun6C.Click
        Try
            chbRun6A.Checked = False
            chbRun6B.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun7A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun7A.Click
        Try
            chbRun7B.Checked = False
            chbRun7C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun7B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun7B.Click
        Try
            chbRun7A.Checked = False
            chbRun7C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun7C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun7C.Click
        Try
            chbRun7A.Checked = False
            chbRun7B.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun8A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun8A.Click
        Try
            chbRun8B.Checked = False
            chbRun8C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun8B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun8B.Click
        Try
            chbRun8A.Checked = False
            chbRun8C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun8C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun8C.Click
        Try
            chbRun8A.Checked = False
            chbRun8B.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun9A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun9A.Click
        Try
            chbRun9B.Checked = False
            chbRun9C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun9B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun9B.Click
        Try
            chbRun9A.Checked = False
            chbRun9C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun9C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun9C.Click
        Try
            chbRun9A.Checked = False
            chbRun9B.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun10A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun10A.Click
        Try
            chbRun10B.Checked = False
            chbRun10C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun10B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun10B.Click
        Try
            chbRun10A.Checked = False
            chbRun10C.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun10C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun10C.Click
        Try
            chbRun10A.Checked = False
            chbRun10B.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region
#End Region
#Region " Print Routines"
    Private Sub btnPrintRoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintRoster.Click
        Try
            If cboSchedule1.SelectedItem = "- Select a Location/Term -" Or cboSchedule1.SelectedItem = "- All Terms -" Then
                MsgBox("You must select a Location/Term")
            Else
                my1 = Nothing
                If my1 Is Nothing Then my1 = New Roster
                my1.txtLocationTerm.Text = Me.cboSchedule1.SelectedItem
                loadPrintInfo4Res()
                my1.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnPrintPass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintPass.Click
        Try
            txtPassFailNoShow.Text = "Pass"

            If cboSchedule3.SelectedItem = "- Select a Location/Term -" Or cboSchedule1.SelectedItem = "- All Terms -" Then
                MsgBox("You must select a Location/Term")
            Else
                my2 = Nothing
                If my2 Is Nothing Then my2 = New PassFailNoShow
                my2.txtPassFailNoShow.Text = "Pass"
                loadPrintInfo()
                my2.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub loadPrintInfo()
        Dim cnt As Integer = 0
        Try
            Dim StudentID As String = ""
            Dim name As String = txtStudentName3.Text
            Dim FacilityName As String = ""
            Dim LocationTerm As String = ""
            Dim LocationTermOriginal As String = ""
            Dim PassFailNoShow As String = ""
            Dim VisualRestrictions As String = ""
            Dim datePassed As String = ""
            Dim Address1 As String = ""
            Dim Address2 As String = ""
            Dim city As String = ""
            Dim state As String = ""
            Dim zip As String = ""
            Dim firstName As String = ""
            Dim lastName As String = ""
            Dim site As String = ""
            Dim startDate As String = ""
            Dim endDate As String = ""
            Dim schedule As String = ""
            Dim dash As Integer = 0
            Dim colon As Integer = 0


            LocationTerm = cboSchedule3.SelectedItem
            LocationTermOriginal = cboSchedule3.SelectedItem
            colon = InStr(LocationTerm, ":")
            LocationTerm = Mid(LocationTerm, 1, colon - 1)

            SQL = "delete from airbranch.SmokeSchoolPrintInfo"

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "select * from airbranch.SmokeSchoolScores " & _
            "where strLocationTerm = '" & LocationTerm & "'" & _
            "  and strPassFailNoShow = '" & txtPassFailNoShow.Text & "'" & _
            " order by strName"

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                schedule = LocationTerm
                dash = InStr(schedule, "-")
                site = schedule.Remove(0, dash + 1)

                PassFailNoShow = dr.Item("strPassFailNoShow")
                VisualRestrictions = dr.Item("strVisualRestrictions")
                datePassed = dr.Item("strdatePassed")
                FacilityName = dr.Item("strCompanyName")
                name = dr.Item("strName")
                StudentID = dr.Item("intStudentID")
                firstName = dr.Item("strFirstName")
                lastName = dr.Item("strlastName")

                SQL = "select * from airbranch.SmokeSchoolReservation " & _
                "where strLastName = '" & Replace(lastName, "'", "''") & "' " & _
                "and strFirstName = '" & Replace(firstName, "'", "''") & "' " & _
                "and strLocationDate = '" & cboSchedule3.SelectedItem & "'"

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("strAddress1")) Then
                        Address1 = "N/A"
                    Else
                        Address1 = dr2.Item("strAddress1")
                    End If
                    If IsDBNull(dr2.Item("strAddress2")) Then
                        Address2 = "N/A"
                    Else
                        Address2 = dr2.Item("strAddress2")
                    End If
                    If IsDBNull(dr2.Item("strCity")) Then
                        city = "N/A"
                    Else
                        city = dr2.Item("strCity")
                    End If
                    If IsDBNull(dr2.Item("strState")) Then
                        state = "N/A"
                    Else
                        state = dr2.Item("strState")
                    End If
                    If IsDBNull(dr2.Item("strZip")) Then
                        zip = "N/A"
                    Else
                        zip = dr2.Item("strZip")
                    End If
                End While
                dr2.Close()

                SQL = "select * from airbranch.SmokeSchoolSchedule " & _
                "where strScheduleShort = '" & LocationTerm & "'"

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    startDate = dr2.Item("strStartDate")
                    endDate = dr2.Item("strEndDate")
                End While
                dr2.Close()

                SQL = "insert into airbranch.SmokeSchoolPrintInfo (" & _
                       "intStudentID, " & _
                       "strName, " & _
                       "strLocationTerm, " & _
                       "strDatePassed, " & _
                       "strCompanyName, " & _
                       "strAddress1, " & _
                       "strAddress2, " & _
                       "strCity, " & _
                       "strState, " & _
                       "strZip, " & _
                       "strFirstName, " & _
                       "strLastName, " & _
                       "strSite, " & _
                       "strStartDate, " & _
                       "strEndDate, " & _
                       "strSchedule, " & _
                       "strVisualRestrictions, " & _
                       "strPassFailNoShow) " & _
                       "values (" & _
                       "'" & StudentID & "', " & _
                       "'" & Replace(name, "'", "''") & "', " & _
                       "'" & Replace(LocationTerm, "'", "''") & "', " & _
                       "'" & Replace(datePassed, "'", "''") & "', " & _
                       "'" & Replace(FacilityName, "'", "''") & "', " & _
                       "'" & Replace(Address1, "'", "''") & "', " & _
                       "'" & Replace(Address2, "'", "''") & "', " & _
                       "'" & Replace(city, "'", "''") & "', " & _
                       "'" & Replace(state, "'", "''") & "', " & _
                       "'" & Replace(zip, "'", "''") & "', " & _
                       "'" & Replace(firstName, "'", "''") & "', " & _
                       "'" & Replace(lastName, "'", "''") & "', " & _
                       "'" & Replace(site, "'", "''") & "', " & _
                       "'" & Replace(startDate, "'", "''") & "', " & _
                       "'" & Replace(endDate, "'", "''") & "', " & _
                       "'" & Replace(schedule, "'", "''") & "', " & _
                       "'" & Replace(VisualRestrictions, "'", "''") & "', " & _
                       "'" & Replace(PassFailNoShow, "'", "''") & "') "


                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd.ExecuteReader
                dr2.Close()

                cnt = cnt + 1
            End While
            dr.Close()

            MsgBox("The print information is ready with " & cnt & " records")

        Catch ex As Exception
            MsgBox(ex.ToString & "   =>> count = " & cnt)
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub loadPrintInfo4Res()
        Try
            Dim StudentID As String = ""
            Dim name As String = ""
            Dim title As String = ""
            Dim CompanyName As String = ""
            Dim Address1 As String = ""
            Dim Address2 As String = ""
            Dim city As String = ""
            Dim state As String = ""
            Dim zip As String = ""
            Dim phone As String = ""
            Dim phoneOut As String = ""
            Dim phone1 As String = ""
            Dim phone2 As String = ""
            Dim phone3 As String = ""
            Dim phone4 As String = ""
            Dim fax As String = ""
            Dim faxOut As String = ""
            Dim fax1 As String = ""
            Dim fax2 As String = ""
            Dim fax3 As String = ""
            Dim email As String = ""
            Dim firstName As String = ""
            Dim lastName As String = ""
            Dim confirmationNbr As String = ""
            Dim lecture As String = ""
            Dim LocationDate As String = ""

            LocationDate = cboSchedule1.SelectedItem

            SQL = "delete from airbranch.SmokeSchoolPrintInfo"

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "select * from airbranch.SmokeSchoolReservation " & _
                   "where strLocationDate = '" & LocationDate & "'" & _
                   "order by strLastName, strFirstName"

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                LocationDate = dr.Item("strLocationDate")

                If IsDBNull(dr.Item("numUserID")) Then
                    StudentID = 0
                Else
                    StudentID = dr.Item("numUserID")
                End If
                If IsDBNull(dr.Item("strTitle")) Then
                    title = ""
                Else
                    title = dr.Item("strTitle")
                End If
                firstName = dr.Item("strFirstName")
                lastName = dr.Item("strLastName")
                CompanyName = dr.Item("strCompanyName")
                Address1 = dr.Item("strAddress1")
                If IsDBNull(dr.Item("strAddress2")) Then
                    Address2 = "NA"
                Else
                    Address2 = dr.Item("strAddress2")
                End If
                city = dr.Item("strCity")
                state = dr.Item("strState")
                zip = dr.Item("strZip")
                phone = dr.Item("strPhoneNumber")
                phone1 = Mid(phone, 1, 3)
                phone2 = Mid(phone, 4, 3)
                phone3 = Mid(phone, 7, 4)
                phone4 = Mid(phone, 11)
                phoneOut = "(" & phone1 & ")" & " " & phone2 & "-" & phone3 & " Ext: " & phone4
                If IsDBNull(dr.Item("strFax")) Then
                    fax = "NA"
                Else
                    fax = dr.Item("strFax")
                    fax1 = Mid(fax, 1, 3)
                    fax2 = Mid(fax, 4, 3)
                    fax3 = Mid(fax, 7, 4)
                    faxOut = "(" & fax1 & ")" & " " & fax2 & "-" & fax3
                End If
                email = dr.Item("strEmail")
                confirmationNbr = dr.Item("strConfirmationNbr")
                lecture = dr.Item("strLectureYesNo")
                name = lastName & ", " & firstName

                SQL = "insert into airbranch.SmokeSchoolPrintInfo (" & _
                       "intStudentID, " & _
                       "strName, " & _
                       "strLocationTerm, " & _
                       "strCompanyName, " & _
                       "strAddress1, " & _
                       "strAddress2, " & _
                       "strCity, " & _
                       "strState, " & _
                       "strZip, " & _
                       "strFirstName, " & _
                       "strLastName, " & _
                       "strPhoneNumber, " & _
                       "strFax, " & _
                       "strEmail, " & _
                       "strConfirmationNbr, " & _
                       "strLectureYesNo, " & _
                       "strTitle) " & _
                       "values (" & _
                       "'" & StudentID & "', " & _
                       "'" & Replace(name, "'", "''") & "', " & _
                       "'" & Replace(LocationDate, "'", "''") & "', " & _
                       "'" & Replace(CompanyName, "'", "''") & "', " & _
                       "'" & Replace(Address1, "'", "''") & "', " & _
                       "'" & Replace(Address2, "'", "''") & "', " & _
                       "'" & Replace(city, "'", "''") & "', " & _
                       "'" & Replace(state, "'", "''") & "', " & _
                       "'" & Replace(zip, "'", "''") & "', " & _
                       "'" & Replace(firstName, "'", "''") & "', " & _
                       "'" & Replace(lastName, "'", "''") & "', " & _
                       "'" & Replace(phoneOut, "'", "''") & "', " & _
                       "'" & Replace(faxOut, "'", "''") & "', " & _
                       "'" & Replace(email, "'", "''") & "', " & _
                       "'" & Replace(confirmationNbr, "'", "''") & "', " & _
                       "'" & Replace(lecture, "'", "''") & "', " & _
                       "'" & Replace(title, "'", "''") & "') "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd.ExecuteReader
                dr2.Close()
            End While
            dr.Close()

            MsgBox("The print information is ready")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnPrintThisOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintThisOne.Click
        Try
            txtPassFailNoShow.Text = "Pass"

            If txtStudentID3.Text <> "" And (cboSchedule3.SelectedItem = "- Select a Location/Term -" Or cboSchedule1.SelectedItem = "- All Terms -") Then
                MsgBox("You must select a Location/Term and a Student")
            Else
                my2 = Nothing
                If my2 Is Nothing Then my2 = New PassFailNoShow
                my2.txtPassFailNoShow.Text = "Pass"
                printThisOne()
                my2.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub printThisOne()
        Try
            Dim StudentID As Integer = CInt(txtStudentID3.Text)
            Dim name As String = txtStudentName3.Text
            Dim FacilityName As String = ""
            Dim LocationTerm As String = ""
            Dim LocationTermOriginal As String = ""
            Dim PassFailNoShow As String = ""
            Dim VisualRestrictions As String = ""
            Dim datePassed As String = ""
            Dim Address1 As String = ""
            Dim Address2 As String = ""
            Dim city As String = ""
            Dim state As String = ""
            Dim zip As String = ""
            Dim firstName As String = txtFirstName3.Text
            Dim lastName As String = txtLastName3.Text
            Dim site As String = ""
            Dim startDate As String = ""
            Dim endDate As String = ""
            Dim schedule As String = ""
            Dim dash As Integer = 0
            Dim colon As Integer = 0
            Dim cnt As Integer = 0

            LocationTerm = cboSchedule3.SelectedItem
            LocationTermOriginal = cboSchedule3.SelectedItem
            colon = InStr(LocationTerm, ":")
            LocationTerm = Mid(LocationTerm, 1, colon - 1)

            SQL = "delete from airbranch.SmokeSchoolPrintInfo"

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "select * from airbranch.SmokeSchoolScores " & _
            "where strLocationTerm = '" & LocationTerm & "'" & _
            "  and strPassFailNoShow = '" & txtPassFailNoShow.Text & "'" & _
            "  and strName = '" & txtStudentName3.Text & "'" & _
            " order by strName"

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                schedule = LocationTerm
                dash = InStr(schedule, "-")
                site = schedule.Remove(0, dash + 1)

                PassFailNoShow = dr.Item("strPassFailNoShow")
                VisualRestrictions = dr.Item("strVisualRestrictions")
                datePassed = dr.Item("strdatePassed")
                FacilityName = dr.Item("strCompanyName")
                StudentID = dr.Item("intStudentID")

                SQL = "select * from airbranch.SmokeSchoolReservation " & _
                       "where numUserID = '" & Replace(StudentID, "'", "''") & "' " & _
                       "and strLocationDate = '" & cboSchedule3.SelectedItem & "'"

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("strAddress1")) Then
                        Address1 = "N/A"
                    Else
                        Address1 = dr2.Item("strAddress1")
                    End If
                    If IsDBNull(dr2.Item("strAddress2")) Then
                        Address2 = "N/A"
                    Else
                        Address2 = dr2.Item("strAddress2")
                    End If
                    If IsDBNull(dr2.Item("strCity")) Then
                        city = "N/A"
                    Else
                        city = dr2.Item("strCity")
                    End If
                    If IsDBNull(dr2.Item("strState")) Then
                        state = "N/A"
                    Else
                        state = dr2.Item("strState")
                    End If
                    If IsDBNull(dr2.Item("strZip")) Then
                        zip = "N/A"
                    Else
                        zip = dr2.Item("strZip")
                    End If
                End While
                dr2.Close()

                SQL = "select * from airbranch.SmokeSchoolSchedule " & _
                "where strScheduleShort = '" & LocationTerm & "'"

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    startDate = dr2.Item("strStartDate")
                    endDate = dr2.Item("strEndDate")
                End While
                dr2.Close()

                SQL = "insert into airbranch.SmokeSchoolPrintInfo (" & _
                       "intStudentID, " & _
                       "strName, " & _
                       "strLocationTerm, " & _
                       "strDatePassed, " & _
                       "strCompanyName, " & _
                       "strAddress1, " & _
                       "strAddress2, " & _
                       "strCity, " & _
                       "strState, " & _
                       "strZip, " & _
                       "strFirstName, " & _
                       "strLastName, " & _
                       "strSite, " & _
                       "strStartDate, " & _
                       "strEndDate, " & _
                       "strSchedule, " & _
                       "strVisualRestrictions, " & _
                       "strPassFailNoShow) " & _
                       "values (" & _
                       "'" & StudentID & "', " & _
                       "'" & Replace(name, "'", "''") & "', " & _
                       "'" & Replace(LocationTerm, "'", "''") & "', " & _
                       "'" & Replace(datePassed, "'", "''") & "', " & _
                       "'" & Replace(FacilityName, "'", "''") & "', " & _
                       "'" & Replace(Address1, "'", "''") & "', " & _
                       "'" & Replace(Address2, "'", "''") & "', " & _
                       "'" & Replace(city, "'", "''") & "', " & _
                       "'" & Replace(state, "'", "''") & "', " & _
                       "'" & Replace(zip, "'", "''") & "', " & _
                       "'" & Replace(firstName, "'", "''") & "', " & _
                       "'" & Replace(lastName, "'", "''") & "', " & _
                       "'" & Replace(site, "'", "''") & "', " & _
                       "'" & Replace(startDate, "'", "''") & "', " & _
                       "'" & Replace(endDate, "'", "''") & "', " & _
                       "'" & Replace(schedule, "'", "''") & "', " & _
                       "'" & Replace(VisualRestrictions, "'", "''") & "', " & _
                       "'" & Replace(PassFailNoShow, "'", "''") & "') "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd.ExecuteReader
                dr2.Close()
            End While
            dr.Close()

            MsgBox("The print information is ready for " & name)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region
#Region " Clear Routine "
    Private Sub btnClearRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearRes.Click
        Try
            clearRes()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub clearRes()
        Try
            txtID1.Text = ""
            txtLastName1.Text = ""
            txtFirstName1.Text = ""
            txtTitle1.Text = ""
            cboSalutation.SelectedIndex = 0
            txtCompanyName1.Text = ""
            txtAddress11.Text = ""
            txtAddress12.Text = ""
            txtCity1.Text = ""
            txtState1.Text = "GA"
            txtZip1.Text = ""
            txtPhoneAC1.Text = ""
            txtPhoneEXC1.Text = ""
            txtPhoneNBR1.Text = ""
            txtPhoneExt1.Text = ""
            txtFaxAC1.Text = ""
            txtFaxEXC1.Text = ""
            txtFaxNBR1.Text = ""
            txtEmail1.Text = ""
            txtConfirmation.Text = ""
            cboLecture.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnScheduleClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScheduleClear.Click
        Try
            clearSchedule()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub clearSchedule()
        Try
            Dim today As Date = Now.Month.ToString & "/" & Now.Day.ToString & "/" & Now.Year.ToString

            cboYear.SelectedIndex = 0
            cboSeason.SelectedIndex = 0
            cboCity.SelectedIndex = 0
            dtpStartDate.Value = today
            dtpEndDate.Value = today
            cboDisplay.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnCacheClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCacheClear.Click
        Try
            clearCache()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub clearCache()
        Try
            txtCacheID.Text = ""
            txtCacheLastName.Text = ""
            txtCacheFirstName.Text = ""
            txtCacheTitle.Text = ""
            txtCacheSalutation.Text = ""
            txtCacheCompanyName.Text = ""
            txtCacheAddress1.Text = ""
            txtCacheAddress2.Text = ""
            txtCacheCity.Text = ""
            txtCacheState.Text = ""
            txtCacheZip.Text = ""
            txtCachePhoneAC.Text = ""
            txtCachePhoneEXC.Text = ""
            txtCachePhoneNBR.Text = ""
            txtCachePhoneExt.Text = ""
            txtCacheFaxAC.Text = ""
            txtCacheFaxEXC.Text = ""
            txtCacheFaxNBR.Text = ""
            txtCacheEmail.Text = ""
            txtCacheConfirmation.Text = ""
            txtCacheLecture.Text = ""
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region
#Region " Mouse UP routines "
    Private Sub dgvSchedule_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvSchedule.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvSchedule.HitTest(e.X, e.Y)
            Dim year As String
            Dim Season As String
            Dim city As String
            Dim startDate As String
            Dim endDate As String
            Dim schedule As String
            Dim dash As Integer
            Dim colon As Integer
            Dim space As Integer
            Dim thru As Integer

            If dgvSchedule.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvSchedule.Columns(0).HeaderText = "Schedule" Then
                    txtSchedule.Text = dgvSchedule(0, hti.RowIndex).Value
                    cboDisplay.SelectedItem = dgvSchedule(1, hti.RowIndex).Value
                    schedule = dgvSchedule(0, hti.RowIndex).Value
                    year = Mid(schedule, 1, 4)
                    schedule = Mid(schedule, 6)
                    dash = InStr(schedule, "-")
                    Season = Mid(schedule, 1, dash - 2)
                    schedule = Mid(schedule, dash + 2)
                    colon = InStr(schedule, ":")
                    city = Mid(schedule, 1, colon - 1)
                    schedule = Mid(schedule, colon + 2)
                    space = InStr(schedule, " ")
                    startDate = Mid(schedule, 1, space - 1)
                    thru = InStr(schedule, "thru")
                    endDate = Mid(schedule, thru + 5)
                    cboYear.SelectedItem = year
                    cboSeason.SelectedItem = Season
                    cboCity.SelectedItem = city
                    dtpStartDate.Value = startDate
                    dtpEndDate.Value = endDate
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub dgvRes_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvRes.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvRes.HitTest(e.X, e.Y)
            Dim phoneNumber1 As String
            Dim x, y As String
            Dim fax1 As String

            If dgvRes.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvRes.Columns(0).HeaderText = "User ID" Then

                    txtID1.Text = dgvRes(0, hti.RowIndex).Value
                    If IsDBNull(dgvRes(1, hti.RowIndex).Value) Then
                        txtLastName1.Clear()
                    Else
                        txtLastName1.Text = dgvRes(1, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(2, hti.RowIndex).Value) Then
                        txtFirstName1.Clear()
                    Else
                        txtFirstName1.Text = dgvRes(2, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(0, hti.RowIndex).Value) Then
                        txtID1X.Clear()
                    Else
                        txtID1X.Text = dgvRes(0, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(1, hti.RowIndex).Value) Then
                        txtLastName1X.Clear()
                    Else
                        txtLastName1X.Text = dgvRes(1, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(2, hti.RowIndex).Value) Then
                        txtFirstName1X.Clear()
                    Else
                        txtFirstName1X.Text = dgvRes(2, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(3, hti.RowIndex).Value) Then
                        cboSalutation.SelectedItem = ""
                    Else
                        cboSalutation.SelectedItem = dgvRes(3, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(3, hti.RowIndex).Value) Then
                        cboSalutation.SelectedItem = ""
                    Else
                        cboSalutation.SelectedItem = dgvRes(3, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(4, hti.RowIndex).Value) Then
                        txtTitle1.Clear()
                    Else
                        txtTitle1.Text = dgvRes(4, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(5, hti.RowIndex).Value) Then
                        txtCompanyName1.Clear()
                    Else
                        txtCompanyName1.Text = dgvRes(5, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(6, hti.RowIndex).Value) Then
                        txtAddress11.Clear()
                    Else
                        txtAddress11.Text = dgvRes(6, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(7, hti.RowIndex).Value) Then
                        txtAddress12.Text = ""
                    Else
                        txtAddress12.Text = dgvRes(7, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(8, hti.RowIndex).Value) Then
                        txtCity1.Text = ""
                    Else
                        txtCity1.Text = dgvRes(8, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(9, hti.RowIndex).Value) Then
                        txtState1.Text = "GA"
                    Else
                        txtState1.Text = dgvRes(9, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(10, hti.RowIndex).Value) Then
                        txtZip1.Clear()
                    Else
                        txtZip1.Text = dgvRes(10, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(11, hti.RowIndex).Value) Then
                        phoneNumber1 = "0000000000"
                    Else
                        phoneNumber1 = dgvRes(11, hti.RowIndex).Value
                    End If
                    txtPhoneAC1.Text = Mid(phoneNumber1, 1, 3)
                    txtPhoneEXC1.Text = Mid(phoneNumber1, 4, 3)
                    txtPhoneNBR1.Text = Mid(phoneNumber1, 7, 4)
                    If phoneNumber1.Length > 10 Then
                        txtPhoneExt1.Text = Mid(phoneNumber1, 11)
                    Else
                        txtPhoneExt1.Text = ""
                    End If

                    If IsDBNull(dgvRes(12, hti.RowIndex).Value) Then
                        fax1 = "0000000000"
                    Else
                        fax1 = dgvRes(12, hti.RowIndex).Value
                    End If
                    txtFaxAC1.Text = Mid(fax1, 1, 3)
                    txtFaxEXC1.Text = Mid(fax1, 4, 3)
                    txtFaxNBR1.Text = Mid(fax1, 7, 4)
                    If IsDBNull(dgvRes(13, hti.RowIndex).Value) Then
                        txtEmail1.Clear()
                    Else
                        txtEmail1.Text = dgvRes(13, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(14, hti.RowIndex).Value) Then
                        txtConfirmation.Clear()
                    Else
                        txtConfirmation.Text = dgvRes(14, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvRes(15, hti.RowIndex).Value) Then
                        txtSchedule1.Clear()
                    Else
                        x = dgvRes(15, hti.RowIndex).Value
                        txtSchedule1.Text = x
                    End If
                    If IsDBNull(dgvRes(16, hti.RowIndex).Value) Then
                        cboLecture.SelectedItem = ""
                    Else
                        y = dgvRes(16, hti.RowIndex).Value
                        cboLecture.SelectedItem = y
                    End If

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub dgvSchedule2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvSchedule2.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvSchedule2.HitTest(e.X, e.Y)
            If dgvSchedule2.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvSchedule2.Columns(0).HeaderText = "Schedule" Then
                    txtSchedule2.Text = dgvSchedule2(0, hti.RowIndex).Value
                    txtsortnbr.Text = "2"
                    numberOfStudents3()
                    BindDataGridReservation2()
                    BindDataGridScores1()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub dgvScore3_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvScore3.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvScore3.HitTest(e.X, e.Y)
        Dim run1 As String
        Dim run2 As String
        Dim run3 As String
        Dim run4 As String
        Dim run5 As String
        Dim run6 As String
        Dim run7 As String
        Dim run8 As String
        Dim run9 As String
        Dim run10 As String

        Try
            chbRun1A.Checked = False
            chbRun1B.Checked = False
            chbRun1C.Checked = False
            chbRun2A.Checked = False
            chbRun2B.Checked = False
            chbRun2C.Checked = False
            chbRun3A.Checked = False
            chbRun3B.Checked = False
            chbRun3C.Checked = False
            chbRun4A.Checked = False
            chbRun4B.Checked = False
            chbRun4C.Checked = False
            chbRun5A.Checked = False
            chbRun5B.Checked = False
            chbRun5C.Checked = False
            chbRun6A.Checked = False
            chbRun6B.Checked = False
            chbRun6C.Checked = False
            chbRun7A.Checked = False
            chbRun7B.Checked = False
            chbRun7C.Checked = False
            chbRun8A.Checked = False
            chbRun8B.Checked = False
            chbRun8C.Checked = False
            chbRun9A.Checked = False
            chbRun9B.Checked = False
            chbRun9C.Checked = False
            chbRun10A.Checked = False
            chbRun10B.Checked = False
            chbRun10C.Checked = False

            If dgvScore3.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvScore3.Columns(0).HeaderText = "ID" Then

                    txtStudentID3.Text = dgvScore3(0, hti.RowIndex).Value

                    If IsDBNull(dgvScore3(1, hti.RowIndex).Value) Then
                        txtStudentName3.Text = "NA"
                    Else
                        txtStudentName3.Text = dgvScore3(1, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(2, hti.RowIndex).Value) Then
                        txtCompanyName3.Text = "NA"
                    Else
                        txtCompanyName3.Text = dgvScore3(2, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(3, hti.RowIndex).Value) Then
                        cboPassFail.SelectedItem = "--"
                    Else
                        cboPassFail.SelectedItem = dgvScore3(3, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(4, hti.RowIndex).Value) Then
                        txtQuizGrade.Text = "NA"
                    Else
                        txtQuizGrade.Text = dgvScore3(4, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(5, hti.RowIndex).Value) Then
                        dtpDatePassed.Value = "9-sep-9998"
                    Else
                        dtpDatePassed.Value = dgvScore3(5, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(6, hti.RowIndex).Value) Then
                        cboVisionCorrection.SelectedItem = "--"
                    Else
                        cboVisionCorrection.SelectedItem = dgvScore3(6, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(7, hti.RowIndex).Value) Then
                        txtComments.Text = ""
                    Else
                        txtComments.Text = dgvScore3(7, hti.RowIndex).Value
                    End If

                    If IsDBNull(dgvScore3(8, hti.RowIndex).Value) Then
                        run1 = "No Test"
                    Else
                        run1 = dgvScore3(8, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(9, hti.RowIndex).Value) Then
                        run2 = "No Test"
                    Else
                        run2 = dgvScore3(9, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(10, hti.RowIndex).Value) Then
                        run3 = "No Test"
                    Else
                        run3 = dgvScore3(10, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(11, hti.RowIndex).Value) Then
                        run4 = "No Test"
                    Else
                        run4 = dgvScore3(11, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(12, hti.RowIndex).Value) Then
                        run5 = "No Test"
                    Else
                        run5 = dgvScore3(12, hti.RowIndex).Value
                    End If

                    If IsDBNull(dgvScore3(13, hti.RowIndex).Value) Then
                        run6 = "No Test"
                    Else
                        run6 = dgvScore3(13, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(14, hti.RowIndex).Value) Then
                        run7 = "No Test"
                    Else
                        run7 = dgvScore3(14, hti.RowIndex).Value
                    End If
                  
                    If IsDBNull(dgvScore3(15, hti.RowIndex).Value) Then
                        run8 = "No Test"
                    Else
                        run8 = dgvScore3(15, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(16, hti.RowIndex).Value) Then
                        run9 = "No Test"
                    Else
                        run9 = dgvScore3(16, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(17, hti.RowIndex).Value) Then
                        run10 = "No Test"
                    Else
                        run10 = dgvScore3(17, hti.RowIndex).Value
                    End If

                    'run2 = dgvScore3(9, hti.RowIndex).Value
                    'run3 = dgvScore3(10, hti.RowIndex).Value
                    'run4 = dgvScore3(11, hti.RowIndex).Value
                    'run5 = dgvScore3(12, hti.RowIndex).Value
                    'run6 = dgvScore3(13, hti.RowIndex).Value
                    'run7 = dgvScore3(14, hti.RowIndex).Value
                    'run8 = dgvScore3(15, hti.RowIndex).Value
                    'run9 = dgvScore3(16, hti.RowIndex).Value
                    'run10 = dgvScore3(17, hti.RowIndex).Value

                    If IsDBNull(dgvScore3(18, hti.RowIndex).Value) Then
                        txtFirstName3.Text = ""
                    Else
                        txtFirstName3.Text = dgvScore3(18, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(19, hti.RowIndex).Value) Then
                        txtLastName3.Text = ""
                    Else
                        txtLastName3.Text = dgvScore3(19, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvScore3(20, hti.RowIndex).Value) Then
                        txtScoreKey.Clear()
                    Else
                        txtScoreKey.Text = dgvScore3(20, hti.RowIndex).Value
                    End If
                    If run1 = "Pass" Then
                        chbRun1A.Checked = True
                    End If
                    If run1 = "Fail" Then
                        chbRun1B.Checked = True
                    End If
                    If run1 = "No Test" Then
                        chbRun1C.Checked = True
                    End If
                    If run2 = "Pass" Then
                        chbRun2A.Checked = True
                    End If
                    If run2 = "Fail" Then
                        chbRun2B.Checked = True
                    End If
                    If run2 = "No Test" Then
                        chbRun2C.Checked = True
                    End If
                    If run3 = "Pass" Then
                        chbRun3A.Checked = True
                    End If
                    If run3 = "Fail" Then
                        chbRun3B.Checked = True
                    End If
                    If run3 = "No Test" Then
                        chbRun3C.Checked = True
                    End If
                    If run4 = "Pass" Then
                        chbRun4A.Checked = True
                    End If
                    If run4 = "Fail" Then
                        chbRun4B.Checked = True
                    End If
                    If run4 = "No Test" Then
                        chbRun4C.Checked = True
                    End If
                    If run5 = "Pass" Then
                        chbRun5A.Checked = True
                    End If
                    If run5 = "Fail" Then
                        chbRun5B.Checked = True
                    End If
                    If run5 = "No Test" Then
                        chbRun5C.Checked = True
                    End If
                    If run6 = "Pass" Then
                        chbRun6A.Checked = True
                    End If
                    If run6 = "Fail" Then
                        chbRun6B.Checked = True
                    End If
                    If run6 = "No Test" Then
                        chbRun6C.Checked = True
                    End If
                    If run7 = "Pass" Then
                        chbRun7A.Checked = True
                    End If
                    If run7 = "Fail" Then
                        chbRun7B.Checked = True
                    End If
                    If run7 = "No Test" Then
                        chbRun7C.Checked = True
                    End If
                    If run8 = "Pass" Then
                        chbRun8A.Checked = True
                    End If
                    If run8 = "Fail" Then
                        chbRun8B.Checked = True
                    End If
                    If run8 = "No Test" Then
                        chbRun8C.Checked = True
                    End If
                    If run9 = "Pass" Then
                        chbRun9A.Checked = True
                    End If
                    If run9 = "Fail" Then
                        chbRun9B.Checked = True
                    End If
                    If run9 = "No Test" Then
                        chbRun9C.Checked = True
                    End If
                    If run10 = "Pass" Then
                        chbRun10A.Checked = True
                    End If
                    If run10 = "Fail" Then
                        chbRun10B.Checked = True
                    End If
                    If run10 = "No Test" Then
                        chbRun10C.Checked = True
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region
#Region " Text Box Change Routines "
    Private Sub txtPhoneAC1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPhoneAC1.TextChanged
        Try
            Dim phoneac As String = txtPhoneAC1.Text
            If phoneac.Length = 3 Then
                txtPhoneEXC1.Focus()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub txtPhoneEXC1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPhoneEXC1.TextChanged
        Try
            Dim phoneexc As String = txtPhoneEXC1.Text
            If phoneexc.Length = 3 Then
                txtPhoneNBR1.Focus()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub txtPhoneNBR1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPhoneNBR1.TextChanged
        Try
            Dim phonenbr As String = txtPhoneNBR1.Text
            If phonenbr.Length = 4 Then
                txtPhoneExt1.Focus()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub txtFaxAC1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFaxAC1.TextChanged
        Try
            Dim faxac As String = txtFaxAC1.Text
            If faxac.Length = 3 Then
                txtFaxEXC1.Focus()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub txtFaxEXC1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFaxEXC1.TextChanged
        Try
            Dim faxexc As String = txtFaxEXC1.Text
            If faxexc.Length = 3 Then
                txtFaxNBR1.Focus()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region
#Region " Misc Routines "
    Private Sub numberOfStudents1()
        Try
            Dim cnt As Decimal

            If cboSchedule1.SelectedIndex = 0 Or cboSchedule1.SelectedIndex = 1 Then
                MsgBox("You must select a Location/Term")
            Else
                SQL = "select count(*) as numberOfStudents from airbranch.SmokeSchoolReservation " & _
                "where strLocationDate = '" & cboSchedule1.SelectedItem & "'"
            End If

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Read()

            cnt = dr.Item("numberOfStudents")
            txtNumberOfStudents1.Text = cnt
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub numberOfAttendingLecture()
        Try
            Dim SQL As String
            Dim dr As OracleDataReader
            Dim cnt As Decimal
            Dim locationDate As String = cboSchedule1.SelectedItem

            SQL = "select count(*) as numberOfStudents " & _
                  "from airbranch.SmokeSchoolReservation " & _
                  "where strLocationDate = '" & locationDate & "'" & _
                  "and strLectureYesNo = 'YES'"

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Read()

            cnt = dr.Item("numberOfStudents")
            txtLectureCnt.Text = cnt
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub numberOfStudents3()
        Try
            Dim SQL As String
            Dim dr As OracleDataReader
            Dim cnt As Decimal

            SQL = "select count(*) as numberOfStudents3 from airbranch.SmokeSchoolReservation " & _
                  "where strLocationDate = '" & txtSchedule2.Text & "'"

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Read()

            cnt = dr.Item("numberOfStudents3")
            txtNumberOfStudents3.Text = cnt
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub numberOfStudents4()
        Try
           Dim cnt As Decimal
            SQL = "select count(*) as numberOfStudents4 from airbranch.SmokeSchoolStudent "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Read()

            cnt = dr.Item("numberOfStudents4")
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub GetNext()
        Try
            Dim IDnumber As Decimal

            SQL = "select * from airbranch.SmokeSchoolIDNumber"
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader()
            dr.Read()

            IDnumber = dr("intIDnumber")
            IDnumber = IDnumber + 1
            dr.Close()

            SQL = "update airbranch.SmokeSchoolIDNumber " & _
            "set intIDnumber = '" & IDnumber & "'"
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub GetNext1()
        Try
            Dim IDnumber As Decimal

            SQL = "select * from airbranch.SmokeSchoolIDNumber"

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader()
            dr.Read()

            IDnumber = dr("intIDnumber")
            txtID1.Text = IDnumber
            dr.Close()

            IDnumber = IDnumber + 1

            SQL = "update airbranch.SmokeSchoolIDNumber " & _
            "set intIDnumber = '" & IDnumber & "'"

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#Region "Functions"
    'Public Function getToday()
    '    Try
    '        Dim mon As String
    '        Dim Day As String
    '        Dim Month As String
    '        Dim Year As String
    '        Dim dateOut As String
    '        Dim dateIN As Date = Now.Date

    '        Day = dateIN.Day.ToString
    '        Month = dateIN.Month.ToString
    '        Select Case Month
    '            Case "1"
    '                mon = "Jan"
    '            Case "2"
    '                mon = "Feb"
    '            Case "3"
    '                mon = "Mar"
    '            Case "4"
    '                mon = "Apr"
    '            Case "5"
    '                mon = "May"
    '            Case "6"
    '                mon = "Jun"
    '            Case "7"
    '                mon = "Jul"
    '            Case "8"
    '                mon = "Aug"
    '            Case "9"
    '                mon = "Sep"
    '            Case "10"
    '                mon = "Oct"
    '            Case "11"
    '                mon = "Nov"
    '            Case Else
    '                mon = "Dec"
    '        End Select

    '        Year = dateIN.Year.ToString
    '        dateOut = Day & "-" & mon & "-" & Year

    '        Return dateOut
    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally
    '    End Try
    'End Function
    'Private Function mondayyear(ByVal dateIN)
    '    Try
    '        Dim mon As String
    '        Dim Day As String
    '        Dim Month As String
    '        Dim Year As String
    '        Dim dateOut As String
    '        'Dim dateIN As Date = dtPStartDate.Value

    '        Day = dateIN.Day.ToString
    '        Month = dateIN.Month.ToString
    '        Select Case Month
    '            Case "1"
    '                mon = "Jan"
    '            Case "2"
    '                mon = "Feb"
    '            Case "3"
    '                mon = "Mar"
    '            Case "4"
    '                mon = "Apr"
    '            Case "5"
    '                mon = "May"
    '            Case "6"
    '                mon = "Jun"
    '            Case "7"
    '                mon = "Jul"
    '            Case "8"
    '                mon = "Aug"
    '            Case "9"
    '                mon = "Sep"
    '            Case "10"
    '                mon = "Oct"
    '            Case "11"
    '                mon = "Nov"
    '            Case Else
    '                mon = "Dec"
    '        End Select

    '        Year = dateIN.Year.ToString
    '        dateOut = mon & "-" & Day & "-" & Year

    '        Return dateOut
    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally
    '    End Try
    'End Function
    'Private Function toOraDate(ByVal dateIn As Date)
    '    Try
    '        'Function to convert date (mm/dd/yyyy) to Oracle format (dd-mmm-yyyy)
    '        Dim dateOut As String
    '        Dim day As String
    '        Dim month As String
    '        Dim mon As String
    '        Dim year As String

    '        dateIn = CDate(dateIn)

    '        day = dateIn.Day.ToString
    '        month = dateIn.Month.ToString
    '        year = dateIn.Year.ToString

    '        Select Case month
    '            Case "1"
    '                mon = "Jan"
    '            Case "2"
    '                mon = "Feb"
    '            Case "3"
    '                mon = "Mar"
    '            Case "4"
    '                mon = "Apr"
    '            Case "5"
    '                mon = "May"
    '            Case "6"
    '                mon = "Jun"
    '            Case "7"
    '                mon = "Jul"
    '            Case "8"
    '                mon = "Aug"
    '            Case "9"
    '                mon = "Sep"
    '            Case "10"
    '                mon = "Oct"
    '            Case "11"
    '                mon = "Nov"
    '            Case Else
    '                mon = "Dec"
    '        End Select

    '        dateOut = day & "-" & mon & "-" & year

    '        Return dateOut
    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally
    '    End Try
    'End Function
    'Private Function convertDate(ByVal dateIn As Object)
    '    Try
    '        Dim dateout As String
    '        Dim month As String = dateIn.Month
    '        Dim day As String = dateIn.Day
    '        Dim year As String = dateIn.Year

    '        Select Case month
    '            Case 1
    '                month = "Jan"
    '            Case 2
    '                month = "Feb"
    '            Case 3
    '                month = "Mar"
    '            Case 4
    '                month = "Apr"
    '            Case 5
    '                month = "May"
    '            Case 6
    '                month = "Jun"
    '            Case 7
    '                month = "Jul"
    '            Case 8
    '                month = "Aug"
    '            Case 9
    '                month = "Sep"
    '            Case 10
    '                month = "Oct"
    '            Case 11
    '                month = "Nov"
    '            Case 12
    '                month = "Dec"
    '        End Select

    '        dateout = month & "-" & day & "-" & year
    '        Return dateout
    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally
    '    End Try
    'End Function
#End Region
    Private Sub btnSelectClass1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectClass1.Click
        Try
            If cboSchedule1.SelectedIndex <> 0 Or cboSchedule1.SelectedIndex <> 1 Then
                txtsortnbr.Text = "2"
                txtSortItem.Text = cboSchedule1.SelectedItem
            End If

            If cboSchedule1.SelectedIndex = 1 Then
                txtsortnbr.Text = "1"
            End If

            BindDataGridReservation()
            numberOfStudents1()
            numberOfAttendingLecture()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            export()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub export()
        Try
            'Response.AddHeader("content-disposition", "attachment;filename=EIData.xls")
            '' Set MIME type to Excel.
            'Response.ContentType = "application/vnd.ms-excel"
            '' Remove the charset from the Content-Type header.
            'Response.Charset = ""
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            'Dim ExcelApp As Excel.Application = New Excel.ApplicationClass
            Dim col, row As Integer
            Dim x As String
            Dim y As String
            'Dim a As Integer
            'Dim b As Integer
            Dim c As Integer
            Dim d As Integer
            Dim startRow As Integer = 1
            Dim location As String = cboSchedule1.SelectedItem

            'load Reservation data into Excel
            If dgvRes.RowCount <> 0 Then

                ExcelApp.SheetsInNewWorkbook = 1
                ExcelApp.Workbooks.Add()

                ExcelApp.Cells(startRow, 1).value = location

                ExcelApp.Visible = True

                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For col = 0 To dgvRes.ColumnCount - 1
                    y = dgvRes.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow, col + 1).value = y
                Next

                'a = dgvRes.ColumnCount - 1
                'b = dgvRes.RowCount - 1

                'For col = 0 To dgvEU.RowCount - 1
                '    For row = 0 To dgvEU.ColumnCount - 1
                startRow = startRow + 1
                d = dgvRes.RowCount - 2
                For row = 0 To d

                    c = dgvRes.ColumnCount - 1
                    For col = 0 To c
                        If IsDBNull(dgvRes.Item(col, row).Value.ToString) Then
                            x = ""
                        Else
                            x = dgvRes.Item(col, row).Value.ToString
                        End If
                        'x = dgvEU.Item(col, row).Value.ToString
                        ExcelApp.Cells(startRow, col + 1).value = x

                    Next
                    startRow = startRow + 1
                Next
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
#Region "Obsolete Code "
    'Private Sub btnMoveWalkupToRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        moveWalkupToRes()
    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally
    '    End Try
    'End Sub
    'Private Sub moveWalkupToRes()
    '    Try
    '        Dim fname As String = ""
    '        Dim lname As String = ""
    '        Dim title As String = ""
    '        Dim salutation As String = ""
    '        Dim facility As String = ""
    '        Dim address1 As String = ""
    '        Dim address2 As String = ""
    '        Dim city As String = ""
    '        Dim state As String = ""
    '        Dim zip As String = ""
    '        Dim phone As String = ""
    '        Dim fax As String = ""
    '        Dim email As String = ""
    '        Dim TransactionDate As String = ""
    '        Dim LocationTerm As String = cboSchedule1.SelectedItem
    '        Dim ConfirmNbr As String = ""
    '        Dim lecture As String = ""
    '        Dim locationDate As String = ""
    '        Dim ipAddress As String = "NA"
    '        Dim userNumber As Integer = 0
    '        Dim SQL1 As String = ""
    '        Dim SQL2 As String = ""
    '        Dim SQL3 As String = ""
    '        Dim SQL4 As String = ""
    '        Dim dr1 As OleDbDataReader
    '        Dim dr2 As OracleDataReader
    '        Dim dr3 As OracleDataReader
    '        Dim dr4 As OracleDataReader



    '        If cboSchedule1.SelectedIndex = 0 Or cboSchedule1.SelectedIndex = 1 Then
    '            MsgBox("You must click on a Location/Term.")
    '        Else
    '            If OleDBconn.State = ConnectionState.Open Then
    '            Else
    '                OleDBconn.Open()
    '            End If

    '            If conn.State = ConnectionState.Open Then
    '            Else
    '                conn.Open()
    '            End If

    '            SQL1 = "select * from SmokeSchoolReservation "


    '            Dim cmd1 As New OleDbCommand(SQL1, OleDBconn)
    '            cmd1.CommandType = CommandType.Text

    '            dr1 = cmd1.ExecuteReader

    '            dr1.Read()
    '            Do

    '                If IsDBNull(dr1("numUserID")) Then
    '                    userNumber = 0
    '                Else
    '                    userNumber = dr1("numUserID")
    '                End If
    '                If IsDBNull(dr1("strFirstName")) Then
    '                    fname = ""
    '                Else
    '                    fname = dr1("strFirstName")
    '                End If
    '                If IsDBNull(dr1("strLastName")) Then
    '                    lname = ""
    '                Else
    '                    lname = dr1("strLastName")
    '                End If
    '                If IsDBNull(dr1("strTitle")) Then
    '                    title = ""
    '                Else
    '                    title = dr1("strTitle")
    '                End If
    '                If IsDBNull(dr1("strSalutation")) Then
    '                    salutation = ""
    '                Else
    '                    salutation = dr1("strSalutation")
    '                End If
    '                If IsDBNull(dr1("strCompanyName")) Then
    '                    facility = ""
    '                Else
    '                    facility = dr1("strCompanyName")
    '                End If
    '                If IsDBNull(dr1("strAddress1")) Then
    '                    address1 = ""
    '                Else
    '                    address1 = dr1("strAddress1")
    '                End If
    '                If IsDBNull(dr1("strAddress2")) Then
    '                    address2 = ""
    '                Else
    '                    address2 = dr1("strAddress2")
    '                End If
    '                If IsDBNull(dr1("strCity")) Then
    '                    city = ""
    '                Else
    '                    city = dr1("strCity")
    '                End If
    '                If IsDBNull(dr1("strState")) Then
    '                    state = ""
    '                Else
    '                    state = dr1("strState")
    '                End If
    '                If IsDBNull(dr1("strZip")) Then
    '                    zip = ""
    '                Else
    '                    zip = dr1("strZip")
    '                End If
    '                If IsDBNull(dr1("strPhoneNumber")) Then
    '                    phone = ""
    '                Else
    '                    phone = dr1("strPhoneNumber")
    '                End If
    '                If IsDBNull(dr1("strFax")) Then
    '                    fax = ""
    '                Else
    '                    fax = dr1("strFax")
    '                End If
    '                If IsDBNull(dr1("strEmail")) Then
    '                    email = ""
    '                Else
    '                    email = dr1("strEmail")
    '                End If
    '                If IsDBNull(dr1("strLectureYesNO")) Then
    '                    lecture = ""
    '                Else
    '                    lecture = dr1("strLectureYesNO")
    '                End If
    '                If IsDBNull(dr1("strConfirmationNbr")) Then
    '                    ConfirmNbr = ""
    '                Else
    '                    ConfirmNbr = dr1("strConfirmationNbr")
    '                End If
    '                If IsDBNull(dr1("strLocationDate")) Then
    '                    locationDate = ""
    '                Else
    '                    locationDate = dr1("strLocationDate")
    '                End If
    '                If IsDBNull(dr1("datTransactionDate")) Then
    '                    TransactionDate = ""
    '                Else
    '                    TransactionDate = dr1("datTransactionDate")
    '                End If

    '                'TransactionDate = toOraDate(TransactionDate)
    '                TransactionDate = Format(CDate(TransactionDate), "dd-MMM-yyyy")
    '                SQL2 = "Select * from airbranch.SmokeSchoolReservation " & _
    '                    "where strLocationDate = '" & locationDate & "' " & _
    '                    "and strLastName = '" & lname & "' " & _
    '                    "and strFirstName = '" & fname & "' "

    '                Dim cmd2 As New OracleCommand(SQL2, conn)
    '                cmd2.CommandType = CommandType.Text

    '                dr2 = cmd2.ExecuteReader

    '                Dim recExist As Boolean = dr2.Read

    '                If recExist = True Then
    '                    MsgBox("Student " & fname & " " & lname & " already exist")
    '                Else
    '                    SQL3 = "select max(numUserID) as strUserNumber from airbranch.SmokeSchoolReservation " & _
    '                           "where strlocationDate = '" & locationDate & "'"

    '                    Dim cmd3 As New OracleCommand(SQL3, conn)
    '                    cmd3.CommandType = CommandType.Text

    '                    dr3 = cmd3.ExecuteReader

    '                    While dr3.Read
    '                        If IsDBNull(dr3("strUserNumber")) Then
    '                            userNumber = 0
    '                        Else
    '                            userNumber = dr3("strUserNumber")
    '                        End If
    '                    End While

    '                    userNumber = userNumber + 1

    '                    SQL4 = "insert into airbranch.SmokeSchoolReservation " & _
    '                           "(numUserID, " & _
    '                           "strFirstName, " & _
    '                           "strLastName, " & _
    '                           "strTitle, " & _
    '                           "strSalutation, " & _
    '                           "strCompanyName, " & _
    '                           "strAddress1, " & _
    '                           "strAddress2, " & _
    '                           "strCity, " & _
    '                           "strState, " & _
    '                           "strZip, " & _
    '                           "strPhoneNumber, " & _
    '                           "strFax, " & _
    '                           "strEmail, " & _
    '                           "strLectureYesNO, " & _
    '                           "strConfirmationNbr, " & _
    '                           "strLocationDate, " & _
    '                           "datTransactionDate) " & _
    '                           "values (" & _
    '                           "'" & Replace(userNumber, "'", "''") & "', " & _
    '                           "'" & Replace(fname, "'", "''") & "', " & _
    '                           "'" & Replace(lname, "'", "''") & "', " & _
    '                           "'" & Replace(title, "'", "''") & "', " & _
    '                           "'" & Replace(salutation, "'", "''") & "', " & _
    '                           "'" & Replace(facility, "'", "''") & "', " & _
    '                           "'" & Replace(address1, "'", "''") & "', " & _
    '                           "'" & Replace(address2, "'", "''") & "', " & _
    '                           "'" & Replace(city, "'", "''") & "', " & _
    '                           "'" & Replace(state, "'", "''") & "', " & _
    '                           "'" & Replace(zip, "'", "''") & "', " & _
    '                           "'" & Replace(phone, "'", "''") & "', " & _
    '                           "'" & Replace(fax, "'", "''") & "', " & _
    '                           "'" & Replace(email, "'", "''") & "', " & _
    '                           "'" & Replace(lecture, "'", "''") & "', " & _
    '                           "'" & Replace(ConfirmNbr, "'", "''") & "', " & _
    '                           "'" & Replace(LocationTerm, "'", "''") & "', " & _
    '                           "to_date('" & TransactionDate & "', 'dd-mon-yyyy hh24:mi:ss')) "

    '                    Dim cmd4 As New OracleCommand(SQL4, conn)
    '                    cmd4.CommandType = CommandType.Text

    '                    dr4 = cmd4.ExecuteReader
    '                    dr4.Close()

    '                End If

    '            Loop While dr1.Read()

    '        End If

    '    Catch ex As Exception
    '        ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    Finally
    '    End Try

    'End Sub


#End Region
    Private Sub btnUpdateIDs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateIDs.Click
        Try
            updateUserIDs()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub updateUserIDs()
        Try
            Dim studentID As Decimal = 0
            Dim FirstName As String = ""
            Dim LastName As String = ""
            Dim locationDate As String = ""

            If cboSchedule1.SelectedIndex = 0 Then
                MsgBox("You must click on a Location/Term.")
            Else
                locationDate = cboSchedule1.SelectedItem

                SQL = "select * from airbranch.SmokeSchoolReservation " & _
                "where upper(strLocationDate) = upper('" & Replace(locationDate, "'", "''") & "')"

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    studentID = studentID + 1
                    FirstName = dr.Item("strFirstName")
                    LastName = dr.Item("strLastName")

                    SQL = "update airbranch.SmokeSchoolReservation " & _
                          "set numUserID = " & studentID & " " & _
                          " where upper(strLocationDate) = upper('" & Replace(locationDate, "'", "''") & "')" & _
                          " and upper(strFirstName) = upper('" & Replace(FirstName, "'", "''") & "')" & _
                          " and upper(strLastName) = upper('" & Replace(LastName, "'", "''") & "')"

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()
                End While

                BindDataGridReservation()

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnExportPassToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportPassToExcel.Click
        Try
            'Dim ExcelApp As Excel.Application = New Excel.ApplicationClass
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim col, row As Integer
            Dim x As String
            Dim y As String
            'Dim a As Integer
            'Dim b As Integer
            Dim c As Integer
            Dim d As Integer
            Dim startRow As Integer = 1
            Dim location As String = cboSchedule3.SelectedItem

            'load Reservation data into Excel
            If dgvScore3.RowCount <> 0 Then

                ExcelApp.SheetsInNewWorkbook = 1
                ExcelApp.Workbooks.Add()

                ExcelApp.Cells(startRow, 1).value = location

                ExcelApp.Visible = True

                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For col = 0 To dgvScore3.ColumnCount - 1
                    y = dgvScore3.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow, col + 1).value = y
                Next

                'a = dgvScore3.ColumnCount - 1
                'b = dgvScore3.RowCount - 1

                startRow = startRow + 1
                d = dgvScore3.RowCount - 2
                For row = 0 To d

                    c = dgvScore3.ColumnCount - 1
                    For col = 0 To c
                        If IsDBNull(dgvScore3.Item(col, row).Value.ToString) Then
                            x = ""
                        Else
                            x = dgvScore3.Item(col, row).Value.ToString
                        End If
                        'x = dgvEU.Item(col, row).Value.ToString
                        ExcelApp.Cells(startRow, col + 1).value = x

                    Next
                    startRow = startRow + 1
                Next

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnActivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivate.Click
        Try
            SQL = "Select strUserEmail " & _
            "from " & DBNameSpace & ".OlapUserLogIn " & _
            "where strUserEmail = '" & Replace(UCase(txtEmailAddress.Text), "'", "''") & "' "

            cmd = New OracleCommand(sql, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                Dim updateString As String = "UPDATE " & DBNameSpace & ".OlapUserLogin " & _
                          "SET strconfirm = to_char(sysdate, 'yyyy/mm/dd hh:mi:ss') " & _
                          "WHERE struseremail = '" & Replace(UCase(txtEmailAddress.Text), "'", "''") & "' "
                cmd = New OracleCommand(updateString, Conn)

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                cmd.ExecuteNonQuery()

                MsgBox("The account has been activated", MsgBoxStyle.Information, "Activated!")
            Else
                MsgBox("This user does not exist.", MsgBoxStyle.Exclamation, "Activate failed!")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

#End Region

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            BindDataGridSchedule2()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRefreshClasses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshClasses.Click
        Try
            LoadLocTerm3()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRunDiplomaReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunDiplomaReport.Click
        Try
            Dim Term As String = ""
            Dim Location As String = ""
            Dim SavStart As String = ""
            Dim SavEnd As String = ""
            Dim SavExp As String = ""
            Dim TifStart As String = ""
            Dim TifEnd As String = ""
            Dim TifExp As String = ""
            Dim AtlStart As String = ""
            Dim AtlEnd As String = ""
            Dim AtlExp As String = ""
            Dim SVStart As String = ""
            Dim SVEnd As String = ""
            Dim SVExp As String = ""
            Dim i As Integer = 0
            Dim LastName As String = ""
            Dim FirstName As String = ""
            Dim Cert As Integer = 0
            Dim CertTemp As String = ""
            Dim CertNo As String = ""

            If cboTermYear.Text <> "-Select a Year-" And IsNumeric(cboTermYear.Text) And cboTermYear.Items.Contains(cboTermYear.Text) Then
            Else
                MessageBox.Show("Please select a valid year from the drop down.")
                Exit Sub
            End If
            If rdbSpringTerm.Checked = True Or rdbFallTerm.Checked = True Then
                If rdbSpringTerm.Checked = True Then
                    Term = "Spring"
                Else
                    Term = "Fall"
                End If
            Else
                MessageBox.Show("Please select a term")
                Exit Sub
            End If
            If txtCertNumber.Text <> "" And IsNumeric(txtCertNumber.Text) Then
                Cert = CInt(txtCertNumber.Text) + 1
            Else
                MessageBox.Show("Please enter in a valid Certification Number.")
                Exit Sub
            End If

            dgvDiplomas.Rows.Clear()
            dgvDiplomas.Columns.Clear()

            dgvDiplomas.Columns.Add("Count", "User ID")
            dgvDiplomas.Columns.Add("Last_Name", "Last Name")
            dgvDiplomas.Columns("Last_Name").Width = 200
            dgvDiplomas.Columns.Add("First_Name", "First Name")
            dgvDiplomas.Columns("First_Name").Width = 200
            dgvDiplomas.Columns.Add("CertNo", "Certification #")
            dgvDiplomas.Columns.Add("Expiration", "Expiration Date")
            dgvDiplomas.Columns.Add("Schedule", "Schedule Date")

            SQL = "Select " & _
            "strLocation, " & _
            "strStartDate, strEndDate  " & _
            "from " & DBNameSpace & ".SmokeSchoolSchedule " & _
            "where stryear = '" & cboTermYear.Text & "' " & _
            "and strTerm = '" & Replace(Term, "'", "''") & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strLocation")) Then
                Else
                    Location = dr.Item("strLocation")
                    Select Case Location
                        Case "Savannah"
                            If IsDBNull(dr.Item("strStartDate")) Then
                            Else
                                SavStart = dr.Item("strStartDate")
                            End If
                            If IsDBNull(dr.Item("strEndDate")) Then
                            Else
                                SavEnd = dr.Item("strEndDate")
                            End If
                        Case "Tifton"
                            If IsDBNull(dr.Item("strStartDate")) Then
                            Else
                                TifStart = dr.Item("strStartDate")
                            End If
                            If IsDBNull(dr.Item("strEndDate")) Then
                            Else
                                TifEnd = dr.Item("strEndDate")
                            End If
                        Case "Atlanta"
                            If IsDBNull(dr.Item("strStartDate")) Then
                            Else
                                AtlStart = dr.Item("strStartDate")
                            End If
                            If IsDBNull(dr.Item("strEndDate")) Then
                            Else
                                AtlEnd = dr.Item("strEndDate")
                            End If
                        Case "Special Venue"
                            If IsDBNull(dr.Item("strStartDate")) Then
                            Else
                                SVStart = dr.Item("strStartDate")
                            End If
                            If IsDBNull(dr.Item("strEndDate")) Then
                            Else
                                SVEnd = dr.Item("strEndDate")
                            End If
                    End Select
                End If
            End While
            dr.Close()

            If SavEnd <> "" Then
                SavExp = CStr(Format(CDate(SavEnd).AddMonths(6), "dd-MMM-yyyy"))
            End If
            If TifEnd <> "" Then
                TifExp = CStr(Format(CDate(TifEnd).AddMonths(6), "dd-MMM-yyyy"))
            End If
            If AtlEnd <> "" Then
                AtlExp = CStr(Format(CDate(AtlEnd).AddMonths(6), "dd-MMM-yyyy"))
            End If
            If SVEnd <> "" Then
                SVExp = CStr(Format(CDate(SVEnd).AddMonths(6), "dd-MMM-yyyy"))
            End If

            'Savannah Class
            SQL = "select " & _
            "strLastName, strFirstName  " & _
            "from " & DBNameSpace & ".SmokeSchoolScores  " & _
            "where strLocationTerm = '" & cboTermYear.Text & " " & Term & " - Savannah' " & _
            "and strPassFailNoShow = 'Pass' " & _
            "order by strLastName "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                i += 1
                If IsDBNull(dr.Item("strLastName")) Then
                    LastName = ""
                Else
                    LastName = dr.Item("strLastName")
                End If
                If IsDBNull(dr.Item("strFirstname")) Then
                    FirstName = ""
                Else
                    FirstName = dr.Item("strFirstName")
                End If

                CertTemp = Cert
                Cert += 1
                Select Case CertTemp.Length
                    Case 1
                        CertTemp = "000" & CertTemp
                    Case 2
                        CertTemp = "00" & CertTemp
                    Case 3
                        CertTemp = "0" & CertTemp
                    Case Else
                        'CertTemp = CertTemp
                End Select

                CertNo = cboTermYear.Text & CertTemp & "SAV"

                dgvDiplomas.Rows.Add(i, LastName, FirstName, CertNo, SavExp, SavStart & " - " & SavEnd)
            End While

            'Tifton
            SQL = "select " & _
            "strLastName, strFirstName  " & _
            "from " & DBNameSpace & ".SmokeSchoolScores  " & _
            "where strLocationTerm = '" & cboTermYear.Text & " " & Term & " - Tifton' " & _
            "and strPassFailNoShow = 'Pass' " & _
            "order by strLastName "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                i += 1
                If IsDBNull(dr.Item("strLastName")) Then
                    LastName = ""
                Else
                    LastName = dr.Item("strLastName")
                End If
                If IsDBNull(dr.Item("strFirstname")) Then
                    FirstName = ""
                Else
                    FirstName = dr.Item("strFirstName")
                End If

                CertTemp = Cert
                Cert += 1
                Select Case CertTemp.Length
                    Case 1
                        CertTemp = "000" & CertTemp
                    Case 2
                        CertTemp = "00" & CertTemp
                    Case 3
                        CertTemp = "0" & CertTemp
                    Case Else
                        'CertTemp = CertTemp
                End Select

                CertNo = cboTermYear.Text & CertTemp & "TIF"

                dgvDiplomas.Rows.Add(i, LastName, FirstName, CertNo, TifExp, TifStart & " - " & TifEnd)
            End While


            SQL = "select " & _
            "strLastName, strFirstName  " & _
            "from " & DBNameSpace & ".SmokeSchoolScores  " & _
            "where strLocationTerm = '" & cboTermYear.Text & " " & Term & " - Atlanta' " & _
            "and strPassFailNoShow = 'Pass' " & _
            "order by strLastName "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                i += 1
                If IsDBNull(dr.Item("strLastName")) Then
                    LastName = ""
                Else
                    LastName = dr.Item("strLastName")
                End If
                If IsDBNull(dr.Item("strFirstname")) Then
                    FirstName = ""
                Else
                    FirstName = dr.Item("strFirstName")
                End If

                CertTemp = Cert
                Cert += 1
                Select Case CertTemp.Length
                    Case 1
                        CertTemp = "000" & CertTemp
                    Case 2
                        CertTemp = "00" & CertTemp
                    Case 3
                        CertTemp = "0" & CertTemp
                    Case Else
                        'CertTemp = CertTemp
                End Select

                CertNo = cboTermYear.Text & CertTemp & "ATL"

                dgvDiplomas.Rows.Add(i, LastName, FirstName, CertNo, AtlExp, AtlStart & " - " & AtlEnd)
            End While

            'Special Venue
            SQL = "select " & _
            "strLastName, strFirstName  " & _
            "from " & DBNameSpace & ".SmokeSchoolScores  " & _
            "where strLocationTerm = '" & cboTermYear.Text & " " & Term & " - Special Venue' " & _
            "and strPassFailNoShow = 'Pass' " & _
            "order by strLastName "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                i += 1
                If IsDBNull(dr.Item("strLastName")) Then
                    LastName = ""
                Else
                    LastName = dr.Item("strLastName")
                End If
                If IsDBNull(dr.Item("strFirstname")) Then
                    FirstName = ""
                Else
                    FirstName = dr.Item("strFirstName")
                End If

                CertTemp = Cert
                Cert += 1
                Select Case CertTemp.Length
                    Case 1
                        CertTemp = "000" & CertTemp
                    Case 2
                        CertTemp = "00" & CertTemp
                    Case 3
                        CertTemp = "0" & CertTemp
                    Case Else
                        'CertTemp = CertTemp
                End Select

                CertNo = cboTermYear.Text & CertTemp & "SV"

                dgvDiplomas.Rows.Add(i, LastName, FirstName, CertNo, SVExp, SVStart & " - " & SVEnd)
            End While

            txtDiplomaCount.Text = dgvDiplomas.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnExportDiploma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportDiploma.Click
        Try
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            'Dim ExcelApp As New Excel.Application
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvDiplomas.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvDiplomas.ColumnCount - 1
                        .Cells(1, i + 1) = dgvDiplomas.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvDiplomas.ColumnCount - 1
                        For j = 0 To dgvDiplomas.RowCount - 2
                            temp = dgvDiplomas.Item(i, j).Value

                            If temp Is Nothing Then
                                .Cells(j + 2, i + 1).numberformat = "@"
                                .Cells(j + 2, i + 1).value = " "
                            Else
                                If dgvDiplomas.Item(i, j).Value.ToString = Nothing Then
                                    .Cells(j + 2, i + 1).numberformat = "@"
                                    .Cells(j + 2, i + 1).value = " "
                                Else
                                    .Cells(j + 2, i + 1).numberformat = "@"
                                    .Cells(j + 2, i + 1).value = dgvDiplomas.Item(i, j).Value.ToString
                                End If
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
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        End Try
    End Sub
End Class