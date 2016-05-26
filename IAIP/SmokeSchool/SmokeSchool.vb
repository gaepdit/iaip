Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared

Public Class SmokeSchool
    Dim SQL As String
    Public dsSchedule As DataSet
    Public daSchedule As SqlDataAdapter
    Public dsYear As DataSet
    Public daYear As SqlDataAdapter
    Public dsRes As DataSet
    Public daRes As SqlDataAdapter
    Public dsRes2 As DataSet
    Public daRes2 As SqlDataAdapter
    Public dsStudent As DataSet
    Public daStudent As SqlDataAdapter
    Public dsClasses As DataSet
    Public daClasses As SqlDataAdapter
    Public dsScores As DataSet
    Public daScores As SqlDataAdapter
    Public dsScores3 As DataSet
    Public daScores3 As SqlDataAdapter
    Dim my2 As PassFailNoShow
    Public getIDoverRideFlag As String = "off"
    Public ErrorFlag As String = "no"

    Private Sub ISMPSmokeSchool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub

#Region " Load Routines "
    Sub setPermissions()
        Try

            If AccountFormAccess(128, 3) <> "1" Then
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadDiplomaYear()
        Try
            SQL = "Select " &
            "distinct(strYear) as TermYear " &
            "from SmokeSchoolSchedule " &
            "order by TermYear desc "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            cboCity.Items.Add("Crawfordville")
            cboCity.Items.Add("Dalton")
            cboCity.Items.Add("Perry")
            cboCity.Items.Add("Savannah")
            cboCity.Items.Add("Tifton")
            cboCity.Items.Add("Special Venue")
            cboCity.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub LoadLocTerm1()
        Try
            Dim locTerm As String

            Dim oracleSQL As String


            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            cboSchedule1.Items.Clear()

            oracleSQL = "Select strSchedule from smokeSchoolSchedule " &
                        "where strDisplay = 'YES' " &
                        "order by strSchedule"
            Dim cmd As New SqlCommand(oracleSQL, CurrentConnection)

            Dim dr As SqlDataReader = cmd.ExecuteReader()

            cboSchedule1.Items.Add("- Select a Location/Term -")
            cboSchedule1.Items.Add("- All Terms -")

            While (dr.Read)
                locTerm = dr("strSchedule")
                cboSchedule1.Items.Add(locTerm)
            End While

            cboSchedule1.SelectedIndex = 0

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub LoadLocTerm3()
        Try
            Dim locTerm As String
            Dim oracleSQL As String

            cboSchedule3.Items.Clear()

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            oracleSQL = "Select strSchedule from smokeSchoolSchedule order by strSchedule desc"
            Dim cmd As New SqlCommand(oracleSQL, CurrentConnection)

            Dim dr As SqlDataReader = cmd.ExecuteReader()

            cboSchedule3.Items.Add("- Select a Location/Term -")
            cboSchedule3.Items.Add("- All Terms -")

            While (dr.Read)
                locTerm = dr("strSchedule")
                cboSchedule3.Items.Add(locTerm)
            End While

            cboSchedule3.SelectedIndex = 0

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
#End Region
#Region " Bind Routines "
    Private Sub BindDataGridSchedule()
        Try
            'populate a read-only dataGridView control with oracle
            Dim SQL As String
            Dim dr As SqlDataReader

            SQL = "select strSchedule, strDisplay from smokeSchoolSchedule " &
            "order by strSchedule desc "

            Dim cmd As New SqlCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub BindDataGridSchedule2()
        Try
            'populate a read-only dataGridView control with oracle
            Dim SQL As String
            Dim dr As SqlDataReader

            SQL = "select strSchedule from smokeSchoolSchedule " &
            "where strDisplay = 'YES' " &
            "order by strSchedule desc "

            Dim cmd As New SqlCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub BindDataGridReservation()
        Try
            Dim locationTerm As String = cboSchedule1.SelectedItem
            Dim SQL As String = ""
            Dim dr As SqlDataReader

            If cboSchedule1.SelectedIndex <> 0 Then

                dsRes = New DataSet

                If txtsortnbr.Text = "1" Then
                    SQL = "SELECT numUserID, " &
                                 "strLastName, " &
                                 "strFirstName, " &
                                 "strSalutation, " &
                                 "strTitle, " &
                                 "strCompanyName, " &
                                 "strAddress1, " &
                                 "strAddress2, " &
                                 "strCity, " &
                                 "strState, " &
                                 "strZip, " &
                                 "strPhoneNumber, " &
                                 "strFax, " &
                                 "strEmail, " &
                                 "strConfirmationNbr, " &
                                 "strLocationDate, " &
                                 "strLectureYesNo " &
                          "from smokeSchoolReservation " &
                          "order by strLocationDate, strLastName, strFirstName"
                Else
                    SQL = "SELECT numUserID, " &
                                                     "strLastName, " &
                                                     "strFirstName, " &
                                                     "strSalutation, " &
                                                     "strTitle, " &
                                                     "strCompanyName, " &
                                                     "strAddress1, " &
                                                     "strAddress2, " &
                                                     "strCity, " &
                                                     "strState, " &
                                                     "strZip, " &
                                                     "strPhoneNumber, " &
                                                     "strFax, " &
                                                     "strEmail, " &
                                                     "strConfirmationNbr, " &
                                                     "strLocationDate, " &
                                                     "strLectureYesNo " &
                                                 "from smokeSchoolReservation " &
                                                 "where strLocationDate = '" & locationTerm & "' " &
                                                 "order by strLastName, strFirstName"
                End If

                Dim cmd As New SqlCommand(SQL, CurrentConnection)
                cmd.CommandType = CommandType.Text

                If CurrentConnection.State = ConnectionState.Open Then
                Else
                    CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub BindDataGridReservation2()
        Try
            Dim SQL As String = ""
            Dim dr As SqlDataReader

            dsRes = New DataSet

            If txtsortnbr.Text = "1" Then
                SQL = "SELECT numUserID, " &
                             "strLastName, " &
                             "strFirstName, " &
                             "strTitle, " &
                             "strCompanyName, " &
                             "strAddress1, " &
                             "strCity, " &
                             "strState, " &
                             "strZip, " &
                             "strPhoneNumber, " &
                             "strFax, " &
                             "strEmail, " &
                             "strConfirmationNbr, " &
                             "strLocationDate, " &
                             "strLectureYesNo " &
                      "from smokeSchoolReservation " &
                      "order by strLocationDate, strLastName, strFirstName"
            End If
            If txtsortnbr.Text = "2" Then
                SQL = "SELECT numUserID, " &
                             "strLastName, " &
                             "strFirstName, " &
                             "strTitle, " &
                             "strCompanyName, " &
                             "strAddress1, " &
                             "strCity, " &
                             "strState, " &
                             "strZip, " &
                             "strPhoneNumber, " &
                             "strFax, " &
                             "strEmail, " &
                             "strConfirmationNbr, " &
                             "strLocationDate, " &
                             "strLectureYesNo " &
                      "from smokeSchoolReservation " &
                         "where strLocationDate = '" & txtSchedule2.Text & "' " &
                         "order by strLastName, strFirstName"
            End If

            Dim cmd As New SqlCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub BindDataGridScores3()
        Try
            Dim SQL As String
            Dim dr As SqlDataReader
            Dim locationTerm As String
            Dim colon As Integer



            locationTerm = cboSchedule3.SelectedItem
            colon = InStr(locationTerm, ":")
            locationTerm = Mid(locationTerm, 1, colon - 1)

            dsScores3 = New DataSet

            SQL = "SELECT distinct intStudentId, " &
                  "strName, " &
                  "strCompanyName, " &
                  "strPassFailNoShow, " &
                  "strQuizScore, " &
                  "strDatePassed, " &
                  "strVisualRestrictions, " &
                  "strComment, " &
                  "strRUN1, " &
                  "strRUN2, " &
                  "strRUN3, " &
                  "strRUN4, " &
                  "strRUN5, " &
                  "strRUN6, " &
                  "strRUN7, " &
                  "strRUN8, " &
                  "strRUN9, " &
                  "strRUN10, " &
                  "strFirstName, " &
                  "strLastName, strScoreKey " &
                  "from smokeSchoolScores " &
                  "where strLocationTerm = '" & locationTerm & "' " &
                  "order by strName"

            Dim cmd As New SqlCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub BindDataGridScores1()
        Try
            Dim SQL As String
            Dim dr As SqlDataReader
            Dim locationTerm As String = txtSchedule2.Text
            Dim colon As Integer

            colon = InStr(locationTerm, ":")
            locationTerm = Mid(locationTerm, 1, colon - 1)

            dsScores = New DataSet

            SQL = "SELECT distinct intStudentId, " &
                  "strName, " &
                  "strCompanyName " &
                  "from smokeSchoolScores " &
                  "where strLocationTerm = '" & locationTerm & "' " &
                  "order by strName"

            Dim cmd As New SqlCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region
#Region " SETUP Routines "
    Private Sub btnSaveSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSchedule.Click
        Try
            saveSetupSchedule()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            scheduleShort = year & " " & season & " - " & city
            schedule = scheduleShort & ": " & startDate & " thru " & endDate

            SQL = "Select strSchedule " &
            "from SmokeSchoolSchedule " &
            "where strSchedule = '" & schedule & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                SQL = "update SmokeSchoolSchedule " &
                "set strYear = '" & Replace(year, "'", "''") & "', " &
                "strTerm = '" & Replace(season, "'", "''") & "', " &
                "strLocation = '" & Replace(city, "'", "''") & "', " &
                "strStartDate = '" & Replace(startDate, "'", "''") & "', " &
                "strEndDate = '" & Replace(endDate, "'", "''") & "', " &
                "strSchedule = '" & Replace(schedule, "'", "''") & "', " &
                "strScheduleShort = '" & Replace(scheduleShort, "'", "''") & "', " &
                "strDisplay = '" & Replace(display, "'", "''") & "'" &
                " where  strSchedule = '" & Replace(schedule, "'", "''") & "'"
            Else
                SQL = "Insert Into SmokeSchoolSchedule (strYear, " &
                "strTerm, " &
                "strLocation, " &
                "strStartDate, " &
                "strEndDate, " &
                "strSchedule, " &
                "strScheduleShort, " &
                "strDisplay) " &
                " Values (" &
                "'" & Replace(year, "'", "''") & "', " &
                "'" & Replace(season, "'", "''") & "', " &
                "'" & Replace(city, "'", "''") & "', " &
                "'" & Replace(startDate, "'", "''") & "', " &
                "'" & Replace(endDate, "'", "''") & "', " &
                "'" & Replace(schedule, "'", "''") & "', " &
                "'" & Replace(scheduleShort, "'", "''") & "', " &
                "'" & Replace(display, "'", "''") & "') "
            End If

            'Open the connection to the database and write the record
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            MsgBox("Schedule " & schedule & " has been saved.", MsgBoxStyle.Information, "Setup Save Routine")

            BindDataGridSchedule()
            LoadLocTerm1()
            LoadLocTerm3()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnDeleteSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSchedule.Click
        Try
            deleteSetupSchedule()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            scheduleShort = year & " " & season & " - " & city
            schedule = scheduleShort & ": " & startDate & " thru " & endDate

            SQL = "delete from SmokeSchoolSchedule where strSchedule = '" & schedule & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            MsgBox("Schedule " & schedule & " has been Deleted.", MsgBoxStyle.Information, "Setup Delete Routine")

            BindDataGridSchedule()
            LoadLocTerm1()
            LoadLocTerm3()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            Dim fax As String = txtFaxAC1.Text & txtFaxEXC1.Text & txtFaxNBR1.Text
            Dim email As String = txtEmail1.Text
            Dim day1 As String = Format(Now.Date, "dd-MMM-yyyy")
            Dim hr As String = Now.Hour
            Dim min As String = Now.Minute
            Dim sec As String = Now.Second
            Dim time24 As String = hr & ":" & min & ":" & sec
            Dim TransactionDate As String = day1
            Dim LocationTerm As String = cboSchedule1.SelectedItem
            Dim ConfirmNbr As String = txtConfirmation.Text
            Dim lecture As String = cboLecture.SelectedItem
            Dim msgResult As Integer = 0
            Dim messageOut As String = ""

            checkForEmptyTextBoxes()

            If ErrorFlag = "yes" Then
                Exit Sub
            Else
                txtSchedule.Text = cboSchedule1.SelectedItem

                If txtID1.Text = "" Then
                    SQL = "select max(numUserID) as strUserNumber " &
                    "from SmokeSchoolReservation " &
                    "where strlocationDate = '" & LocationTerm & "'"

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
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

                If txtConfirmation.Text = "" Then
                    'Create confirmation number
                    ConfirmNbr = "1111" & Now.Date.ToString("yyyy MM dd").Replace(" ", "")
                    ConfirmNbr = ConfirmNbr & time24.Replace(":", "")
                End If

                If txtID1.Text = "" And txtConfirmation.Text = "" Then
                    txtFirstName1X.Text = txtFirstName1.Text
                    txtLastName1X.Text = txtLastName1.Text
                End If

                SQL = "select * " &
                "from SmokeSchoolReservation " &
                "where upper(strLastName) = upper('" & Replace(txtLastName1X.Text, "'", "''") & "') " &
                "and upper(strFirstName) = upper('" & Replace(txtFirstName1X.Text, "'", "''") & "') " &
                "and upper(strLocationDate) = upper('" & Replace(txtSchedule.Text, "'", "''") & "') "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    messageOut = "Student: " & fname & " " & lname & " is already registered in the database" & vbCrLf & " Do "
                    messageOut += "you want to Update this Student?"
                    msgResult = MsgBox(messageOut, MsgBoxStyle.YesNo)

                    If msgResult = 6 Then
                        SQL = "update SmokeSchoolReservation " &
                              "set numUserID = " & studentID & ", " &
                              "strFirstName = '" & Replace(fname, "'", "''") & "', " &
                              "strLastName = '" & Replace(lname, "'", "''") & "', " &
                              "strTitle = '" & Replace(title, "'", "''") & "', " &
                              "strSalutation = '" & Replace(salutation, "'", "''") & "', " &
                              "strCompanyName = '" & Replace(facility, "'", "''") & "', " &
                              "strAddress1 = '" & Replace(address1, "'", "''") & "', " &
                              "strAddress2 = '" & Replace(address2, "'", "''") & "', " &
                              "strCity = '" & Replace(city, "'", "''") & "', " &
                              "strState = '" & Replace(state, "'", "''") & "', " &
                              "strZip = '" & Replace(zip, "'", "''") & "', " &
                              "strPhoneNumber = '" & Replace(phone, "'", "''") & "', " &
                              "strFax = '" & Replace(fax, "'", "''") & "', " &
                              "strLectureYesNo = '" & lecture & "', " &
                              "strConfirmationNbr = '" & ConfirmNbr & "', " &
                              "strLocationDate = '" & txtSchedule1.Text & "', " &
                              "datTransactionDate = '" & TransactionDate & "' " &
                              " where upper(strLastName) = upper('" & Replace(txtLastName1X.Text, "'", "''") & "')" &
                              " and upper(strLocationDate) = upper('" & Replace(txtSchedule1.Text, "'", "''") & "')" &
                              " and upper(strFirstName) = upper('" & Replace(txtFirstName1X.Text, "'", "''") & "')"

                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        txtSortItem.Text = cboSchedule1.SelectedItem
                        txtsortnbr.Text = "2"
                        BindDataGridReservation()

                        numberOfStudents1()
                        numberOfAttendingLecture()

                        ErrorFlag = "no"

                        MsgBox("Student: " & fname & " " & lname & " was updated in the Reservation Table")
                        clearRes()

                    End If
                Else
                    SQL = "insert into SmokeSchoolReservation " &
                          "(numUserID, " &
                          "strFirstName, " &
                          "strLastName, " &
                          "strTitle, " &
                          "strSalutation, " &
                          "strCompanyName, " &
                          "strAddress1, " &
                          "strAddress2, " &
                          "strCity, " &
                          "strState, " &
                          "strZip, " &
                          "strPhoneNumber, " &
                          "strFax, " &
                          "strEmail, " &
                          "strLectureYesNO, " &
                          "strConfirmationNbr, " &
                          "strLocationDate, " &
                          "datTransactionDate) " &
                          "values (" &
                          "'" & Replace(studentID, "'", "''") & "', " &
                          "'" & Replace(fname, "'", "''") & "', " &
                          "'" & Replace(lname, "'", "''") & "', " &
                          "'" & Replace(title, "'", "''") & "', " &
                          "'" & Replace(salutation, "'", "''") & "', " &
                          "'" & Replace(facility, "'", "''") & "', " &
                          "'" & Replace(address1, "'", "''") & "', " &
                          "'" & Replace(address2, "'", "''") & "', " &
                          "'" & Replace(city, "'", "''") & "', " &
                          "'" & Replace(state, "'", "''") & "', " &
                          "'" & Replace(zip, "'", "''") & "', " &
                          "'" & Replace(phone, "'", "''") & "', " &
                          "'" & Replace(fax, "'", "''") & "', " &
                          "'" & Replace(email, "'", "''") & "', " &
                          "'" & Replace(lecture, "'", "''") & "', " &
                          "'" & Replace(ConfirmNbr, "'", "''") & "', " &
                          "'" & Replace(LocationTerm, "'", "''") & "', " &
                          "to_date('" & TransactionDate & "', 'dd-mon-yyyy hh24:mi:ss')) "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    txtSortItem.Text = cboSchedule1.SelectedItem
                    txtsortnbr.Text = "2"
                    BindDataGridReservation()

                    numberOfStudents1()
                    numberOfAttendingLecture()

                    ErrorFlag = "no"

                    MsgBox("Student: " & fname & " " & lname & " was added to the Reservation Table")
                    clearRes()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub btnDeleteRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteRes.Click
        Try
            deleteReservation()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub deleteReservation()
        Try
            Dim studentID As String = txtID1.Text
            Dim lName As String = txtLastName1.Text
            Dim fName As String = txtFirstName1.Text

            If cboSchedule1.SelectedIndex = 0 Or cboSchedule1.SelectedIndex = 1 Then
                MsgBox("You must select a Location/Term")
            Else

                If lName <> "" And fName <> "" And txtID1.Text <> "" Then
                    SQL = "Delete from SmokeSchoolReservation " &
                                    " where upper(strLastName) = upper('" & Replace(lName, "'", "''") & "') " &
                                    " and upper(strFirstName) = upper('" & Replace(fName, "'", "''") & "') " &
                                    " and upper(numUserID) = upper('" & studentID & "') " &
                                    "and strLocationDate = '" & cboSchedule1.SelectedItem & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    BindDataGridReservation()

                    txtSortItem.Text = cboSchedule1.SelectedItem
                    txtsortnbr.Text = "2"
                    BindDataGridReservation()
                    clearRes()

                    numberOfStudents1()
                    numberOfAttendingLecture()

                    MsgBox("Student: " & lName & ", " & fName & " has been deleted.")
                Else
                    MsgBox("You must select a student")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            Dim day1 As String = Format(CDate(Now.Date), "dd-MMM-yyyy")
            Dim TransactionDate As String = day1
            Dim colon As Integer = 0

            If txtSchedule2.Text = "" Then
                MsgBox("You must select a Location/Term")
                Exit Sub
            Else
                SQL = "select * from SmokeSchoolReservation " &
                "where strLocationDate = '" & txtSchedule2.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
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

                        SQL = "Select * from SmokeSchoolScores " &
                               "where strscoreKey = '" & Replace(scoreKey, "'", "''") & "' " &
                               "and intStudentID = " & studentID

                        cmd2 = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        recExist = dr2.Read
                        dr2.Close()

                        If recExist = True Then

                        Else
                            SQL = "insert into SmokeSchoolScores ( " &
                                   "intStudentID, " &
                                   "strName, " &
                                   "strCompanyName, " &
                                   "strScoreKey, " &
                                   "strLocationTerm, " &
                                   "strComment, " &
                                   "strRun1, " &
                                   "strRun2, " &
                                   "strRun3, " &
                                   "strRun4, " &
                                   "strRun5, " &
                                   "strRun6, " &
                                   "strRun7, " &
                                   "strRun8, " &
                                   "strRun9, " &
                                   "strRun10, " &
                                   "strLastName, " &
                                   "strFirstName, " &
                                   "datTransactionDate) " &
                                   "values (" & studentID & ", " &
                                   "'" & Replace(name, "'", "''") & "', " &
                                   "'" & Replace(CompanyName, "'", "''") & "', " &
                                   "'" & Replace(scoreKey, "'", "''") & "', " &
                                   "'" & Replace(locationDate, "'", "''") & "', " &
                                   "'" & Replace(Comment, "'", "''") & "', " &
                                   "'" & Replace(Run1, "'", "''") & "', " &
                                   "'" & Replace(Run2, "'", "''") & "', " &
                                   "'" & Replace(Run3, "'", "''") & "', " &
                                   "'" & Replace(Run4, "'", "''") & "', " &
                                   "'" & Replace(Run5, "'", "''") & "', " &
                                   "'" & Replace(Run6, "'", "''") & "', " &
                                   "'" & Replace(Run7, "'", "''") & "', " &
                                   "'" & Replace(Run8, "'", "''") & "', " &
                                   "'" & Replace(Run9, "'", "''") & "', " &
                                   "'" & Replace(Run10, "'", "''") & "', " &
                                   "'" & Replace(lastName, "'", "''") & "', " &
                                   "'" & Replace(firstName, "'", "''") & "', " &
                                   "'" & TransactionDate & "')"

                            cmd = New SqlCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr2 = cmd.ExecuteReader
                            dr2.Close()
                        End If
                    End If
                End While
                dr.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnMoveResStudent2Scores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveResStudent2Scores.Click
        Try
            moveResStudent2Scores()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            Dim day1 As String = Format(CDate(Now.Date), "dd-MMM-yyyy")
            Dim TransactionDate As String = day1
            Dim colon As Integer = 0

            If studentID = 0 Then
                MsgBox("Student:  " & name & "has an ID of zero")
                Exit Sub
            Else
                name = lastName & ", " & firstName
                colon = InStr(locationDate, ":")
                locationDate = Mid(locationDate, 1, colon - 1)
                scoreKey = locationDate & " - " & name
                SQL = "Select * from SmokeSchoolScores " &
                       "where strLocationTerm = '" & locationDate & "' " &
                       "and strName = '" & Replace(name, "'", "''") & "'"

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    MsgBox("Scores for " & name & " already exist for " & locationDate)
                    Exit Sub
                Else
                    SQL = "insert into SmokeSchoolScores ( " &
                           "intStudentID, " &
                           "strName, " &
                           "strCompanyName, " &
                           "strScoreKey, " &
                           "strLocationTerm, " &
                           "strComment, " &
                           "strRun1, " &
                           "strRun2, " &
                           "strRun3, " &
                           "strRun4, " &
                           "strRun5, " &
                           "strRun6, " &
                           "strRun7, " &
                           "strRun8, " &
                           "strRun9, " &
                           "strRun10, " &
                           "strLastName, " &
                           "strFirstName, " &
                           "datTransactionDate) " &
                           "values (" & studentID & ", " &
                           "'" & Replace(name, "'", "''") & "', " &
                           "'" & Replace(CompanyName, "'", "''") & "', " &
                           "'" & Replace(scoreKey, "'", "''") & "', " &
                           "'" & Replace(locationDate, "'", "''") & "', " &
                           "'" & Replace(Comment, "'", "''") & "', " &
                           "'" & Replace(Run1, "'", "''") & "', " &
                           "'" & Replace(Run2, "'", "''") & "', " &
                           "'" & Replace(Run3, "'", "''") & "', " &
                           "'" & Replace(Run4, "'", "''") & "', " &
                           "'" & Replace(Run5, "'", "''") & "', " &
                           "'" & Replace(Run6, "'", "''") & "', " &
                           "'" & Replace(Run7, "'", "''") & "', " &
                           "'" & Replace(Run8, "'", "''") & "', " &
                           "'" & Replace(Run9, "'", "''") & "', " &
                           "'" & Replace(Run10, "'", "''") & "', " &
                           "'" & Replace(lastName, "'", "''") & "', " &
                           "'" & Replace(firstName, "'", "''") & "', " &
                           "'" & TransactionDate & "')"

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    MsgBox("Student " & name & " was moved to the Scores table")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnRemoveRes2Scores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRes2Scores.Click
        Try
            removeResFromScores()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                SQL = "Delete from SmokeSchoolScores " &
                " where strlocationTerm = '" & locationTerm & "'"

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                BindDataGridScores1()
                MsgBox("Class: " & locationTerm & " has been deleted.")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnCache2Res_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCache2Res.Click
        Try
            moveCacheToReservation()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnMove2Cache_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMove2Cache.Click
        Try
            moveReservationToCache()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region
#Region " Scores Routines "
    Private Sub btnSave3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave3.Click
        Try
            saveScores()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub saveScores()
        Try
            Dim name As String = txtStudentName3.Text
            Dim fname As String = txtFirstName3.Text
            Dim lname As String = txtLastName3.Text
            Dim CompanyName As String = ""
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
            Dim day1 As String = Format(CDate(Now.Date), "dd-MMM-yyyy")
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

                    PassFailNoShow = cboPassFail.SelectedItem
                    QuizScore = txtQuizGrade.Text
                    Comment = txtComments.Text
                    CompanyName = txtCompanyName3.Text
                    VisualRestrictions = cboVisionCorrection.SelectedItem
                    datePassed = dtpDatePassed.Value
                    space1 = InStr(datePassed, " ")
                    If space1 > 0 Then
                        datePassed = Mid(datePassed, 1, space1 - 1)
                    End If

                    datePassed = Format(CDate(datePassed), "dd-MMM-yyyy")

                    If txtScoreKey.Text <> "" Then
                        SQL = "Select strScoreKey " &
                        "From SmokeSchoolScores " &
                        "where strScoreKey =  '" & Replace(txtScoreKey.Text, "'", "''") & "' "

                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        Dim i As Integer = 0
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
                                SQL = "update SmokeSchoolScores " &
                                "set strPassFailNoShow = '" & PassFailNoShow & "', " &
                                "strQuizScore = '" & QuizScore & "', " &
                                "strComment = '" & Replace(Comment, "'", "''") & "', " &
                                "strRun1 = '" & Run1 & "', " &
                                "strRun2 = '" & Run2 & "', " &
                                "strRun3 = '" & Run3 & "', " &
                                "strRun4 = '" & Run4 & "', " &
                                "strRun5 = '" & Run5 & "', " &
                                "strRun6 = '" & Run6 & "', " &
                                "strRun7 = '" & Run7 & "', " &
                                "strRun8 = '" & Run8 & "', " &
                                "strRun9 = '" & Run9 & "', " &
                                "strRun10 = '" & Run10 & "', " &
                                "strFirstName = '" & Replace(fname, "'", "''") & "', " &
                                "strLastName = '" & Replace(lname, "'", "''") & "', " &
                                "strName = '" & Replace(name, "'", "''") & "', " &
                                "strCompanyName = '" & Replace(CompanyName, "'", "''") & "', " &
                                "strVisualRestrictions = '" & Replace(VisualRestrictions, "'", "''") & "', " &
                                "strDatePassed = '" & datePassed & "', " &
                                "datTransactionDate = '" & TransactionDate & "' " &
                                "where strScoreKey = '" & Replace(txtScoreKey.Text, "'", "''") & "' "
                            Case Else
                                Dim Result As DialogResult
                                Result = MessageBox.Show("There are " & i.ToString & " entries for this individual. " & vbCrLf &
                                         "Do you want to update all the entries? " & vbCrLf &
                                         "If you need assistance contact the Data Management Unit.",
                                  "Smoke School", MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                                Select Case Result
                                    Case DialogResult.Yes
                                        SQL = "update SmokeSchoolScores " &
                                        "set strPassFailNoShow = '" & PassFailNoShow & "', " &
                                        "strQuizScore = '" & QuizScore & "', " &
                                        "strComment = '" & Replace(Comment, "'", "''") & "', " &
                                        "strRun1 = '" & Run1 & "', " &
                                        "strRun2 = '" & Run2 & "', " &
                                        "strRun3 = '" & Run3 & "', " &
                                        "strRun4 = '" & Run4 & "', " &
                                        "strRun5 = '" & Run5 & "', " &
                                        "strRun6 = '" & Run6 & "', " &
                                        "strRun7 = '" & Run7 & "', " &
                                        "strRun8 = '" & Run8 & "', " &
                                        "strRun9 = '" & Run9 & "', " &
                                        "strRun10 = '" & Run10 & "', " &
                                        "strFirstName = '" & Replace(fname, "'", "''") & "', " &
                                        "strLastName = '" & Replace(lname, "'", "''") & "', " &
                                        "strName = '" & Replace(name, "'", "''") & "', " &
                                        "strCompanyName = '" & Replace(CompanyName, "'", "''") & "', " &
                                        "strVisualRestrictions = '" & Replace(VisualRestrictions, "'", "''") & "', " &
                                        "strDatePassed = '" & datePassed & "', " &
                                        "datTransactionDate = '" & TransactionDate & "' " &
                                        "where strScoreKey = '" & Replace(txtScoreKey.Text, "'", "''") & "' "
                                    Case Else
                                        Exit Sub
                                End Select
                        End Select

                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            deleteScores()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub deleteScores()
        Try
            Dim name As String = txtStudentName3.Text
            Dim i As Integer = 0

            If txtScoreKey.Text <> "" Then
                SQL = "Select strScoreKey " &
                "From SmokeSchoolScores " &
                "where strScoreKey =  '" & Replace(txtScoreKey.Text, "'", "''") & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
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
                        SQL = "Delete SmokeSchoolScores " &
                        "where strScoreKey = '" & Replace(txtScoreKey.Text, "'", "''") & "' "

                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    Case Else
                        Dim Result As DialogResult
                        Result = MessageBox.Show("There are " & i.ToString & " entries for this individual. " & vbCrLf &
                                                 "Do you want to remove all the entries? " & vbCrLf &
                                                 "If you need assistance contact the Data Management Unit.",
                          "Smoke School", MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Select Case Result
                            Case DialogResult.Yes
                                SQL = "Delete SmokeSchoolScores " &
                                "where strScoreKey = '" & Replace(txtScoreKey.Text, "'", "''") & "' "

                                cmd = New SqlCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()
                            Case Else
                                Exit Sub
                        End Select
                End Select

                BindDataGridScores3()

                MsgBox("Student:  " & name & " data was deleted", MsgBoxStyle.Information, "Delete Score Info")

                getStats()
            Else
                MessageBox.Show("Select an individual from the grid to the left before deleting.")
                Exit Sub
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            SQL = "select * from SmokeSchoolScores " &
            "where strLocationTerm = '" & shortLocation & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                SQL = "select * from SmokeSchoolScores " &
                "where strLocationTerm = '" & shortLocation & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#Region "Checkbox Changes"
    Private Sub chbRun1A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun1A.Click
        Try
            chbRun1B.Checked = False
            chbRun1C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun1B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun1B.Click
        Try
            chbRun1A.Checked = False
            chbRun1C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun1C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun1C.Click
        Try
            chbRun1A.Checked = False
            chbRun1B.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun2A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun2A.Click
        Try
            chbRun2B.Checked = False
            chbRun2C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun2B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun2B.Click
        Try
            chbRun2A.Checked = False
            chbRun2C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun2C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun2C.Click
        Try
            chbRun2A.Checked = False
            chbRun2B.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun3A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun3A.Click
        Try
            chbRun3B.Checked = False
            chbRun3C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun3B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun3B.Click
        Try
            chbRun3A.Checked = False
            chbRun3C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun3C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun3C.Click
        Try
            chbRun3A.Checked = False
            chbRun3B.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun4A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun4A.Click
        Try
            chbRun4B.Checked = False
            chbRun4C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun4B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun4B.Click
        Try
            chbRun4A.Checked = False
            chbRun4C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun4C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun4C.Click
        Try
            chbRun4A.Checked = False
            chbRun4B.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun5A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun5A.Click
        Try
            chbRun5B.Checked = False
            chbRun5C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun5B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun5B.Click
        Try
            chbRun5A.Checked = False
            chbRun5C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun5C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun5C.Click
        Try
            chbRun5A.Checked = False
            chbRun5B.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun6A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun6A.Click
        Try
            chbRun6B.Checked = False
            chbRun6C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun6B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun6B.Click
        Try
            chbRun6A.Checked = False
            chbRun6C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun6C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun6C.Click
        Try
            chbRun6A.Checked = False
            chbRun6B.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun7A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun7A.Click
        Try
            chbRun7B.Checked = False
            chbRun7C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun7B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun7B.Click
        Try
            chbRun7A.Checked = False
            chbRun7C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun7C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun7C.Click
        Try
            chbRun7A.Checked = False
            chbRun7B.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun8A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun8A.Click
        Try
            chbRun8B.Checked = False
            chbRun8C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun8B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun8B.Click
        Try
            chbRun8A.Checked = False
            chbRun8C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun8C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun8C.Click
        Try
            chbRun8A.Checked = False
            chbRun8B.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun9A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun9A.Click
        Try
            chbRun9B.Checked = False
            chbRun9C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun9B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun9B.Click
        Try
            chbRun9A.Checked = False
            chbRun9C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun9C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun9C.Click
        Try
            chbRun9A.Checked = False
            chbRun9B.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun10A_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun10A.Click
        Try
            chbRun10B.Checked = False
            chbRun10C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun10B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun10B.Click
        Try
            chbRun10A.Checked = False
            chbRun10C.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbRun10C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRun10C.Click
        Try
            chbRun10A.Checked = False
            chbRun10B.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region
#End Region
#Region " Print Routines"
    Private Sub PrintRoster()
        If cboSchedule1.SelectedItem = "- Select a Location/Term -" Or cboSchedule1.SelectedItem = "- All Terms -" Then
            MsgBox("You must select a Location/Term.", MsgBoxStyle.Exclamation, "Print Error")
            Exit Sub
        End If

        Try
            Dim title As String = cboSchedule1.SelectedItem.ToString
            Dim roster As DataTable = DAL.GetSmokeSchoolRosterAsDataTable(title)
            Dim parameters As New Dictionary(Of String, String) From {{"LocationDateTitle", title}}
            Dim rpt As New CR.Reports.SmokeSchoolRoster
            Dim crv As New CRViewerForm(rpt, roster, parameters, title)
            crv.Show()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnPrintRoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintRoster.Click
        PrintRoster()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            colon = InStr(LocationTerm, ":")
            LocationTerm = Mid(LocationTerm, 1, colon - 1)

            SQL = "delete from SmokeSchoolPrintInfo"

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "select * from SmokeSchoolScores " &
            "where strLocationTerm = '" & LocationTerm & "'" &
            "  and strPassFailNoShow = '" & txtPassFailNoShow.Text & "'" &
            " order by strName"

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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

                SQL = "select * from SmokeSchoolReservation " &
                "where strLastName = '" & Replace(lastName, "'", "''") & "' " &
                "and strFirstName = '" & Replace(firstName, "'", "''") & "' " &
                "and strLocationDate = '" & cboSchedule3.SelectedItem & "'"

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
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

                SQL = "select * from SmokeSchoolSchedule " &
                "where strScheduleShort = '" & LocationTerm & "'"

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    startDate = dr2.Item("strStartDate")
                    endDate = dr2.Item("strEndDate")
                End While
                dr2.Close()

                SQL = "insert into SmokeSchoolPrintInfo (" &
                       "intStudentID, " &
                       "strName, " &
                       "strLocationTerm, " &
                       "strDatePassed, " &
                       "strCompanyName, " &
                       "strAddress1, " &
                       "strAddress2, " &
                       "strCity, " &
                       "strState, " &
                       "strZip, " &
                       "strFirstName, " &
                       "strLastName, " &
                       "strSite, " &
                       "strStartDate, " &
                       "strEndDate, " &
                       "strSchedule, " &
                       "strVisualRestrictions, " &
                       "strPassFailNoShow) " &
                       "values (" &
                       "'" & StudentID & "', " &
                       "'" & Replace(name, "'", "''") & "', " &
                       "'" & Replace(LocationTerm, "'", "''") & "', " &
                       "'" & Replace(datePassed, "'", "''") & "', " &
                       "'" & Replace(FacilityName, "'", "''") & "', " &
                       "'" & Replace(Address1, "'", "''") & "', " &
                       "'" & Replace(Address2, "'", "''") & "', " &
                       "'" & Replace(city, "'", "''") & "', " &
                       "'" & Replace(state, "'", "''") & "', " &
                       "'" & Replace(zip, "'", "''") & "', " &
                       "'" & Replace(firstName, "'", "''") & "', " &
                       "'" & Replace(lastName, "'", "''") & "', " &
                       "'" & Replace(site, "'", "''") & "', " &
                       "'" & Replace(startDate, "'", "''") & "', " &
                       "'" & Replace(endDate, "'", "''") & "', " &
                       "'" & Replace(schedule, "'", "''") & "', " &
                       "'" & Replace(VisualRestrictions, "'", "''") & "', " &
                       "'" & Replace(PassFailNoShow, "'", "''") & "') "


                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr2 = cmd.ExecuteReader
                dr2.Close()

                cnt = cnt + 1
            End While
            dr.Close()

            MsgBox("The print information is ready with " & cnt & " records")

        Catch ex As Exception
            MsgBox(ex.ToString & "   =>> count = " & cnt)
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub printThisOne()
        Try
            Dim StudentID As Integer = CInt(txtStudentID3.Text)
            Dim name As String = txtStudentName3.Text
            Dim FacilityName As String = ""
            Dim LocationTerm As String = ""
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

            LocationTerm = cboSchedule3.SelectedItem
            colon = InStr(LocationTerm, ":")
            LocationTerm = Mid(LocationTerm, 1, colon - 1)

            SQL = "delete from SmokeSchoolPrintInfo"

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "select * from SmokeSchoolScores " &
            "where strLocationTerm = '" & LocationTerm & "'" &
            "  and strPassFailNoShow = '" & txtPassFailNoShow.Text & "'" &
            "  and strName = '" & Replace(txtStudentName3.Text, "'", "''") & "'" &
            " order by strName"

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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

                SQL = "select * from SmokeSchoolReservation " &
                       "where numUserID = '" & Replace(StudentID, "'", "''") & "' " &
                       "and strLocationDate = '" & cboSchedule3.SelectedItem & "'"

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
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

                SQL = "select * from SmokeSchoolSchedule " &
                "where strScheduleShort = '" & LocationTerm & "'"

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    startDate = dr2.Item("strStartDate")
                    endDate = dr2.Item("strEndDate")
                End While
                dr2.Close()

                SQL = "insert into SmokeSchoolPrintInfo (" &
                       "intStudentID, " &
                       "strName, " &
                       "strLocationTerm, " &
                       "strDatePassed, " &
                       "strCompanyName, " &
                       "strAddress1, " &
                       "strAddress2, " &
                       "strCity, " &
                       "strState, " &
                       "strZip, " &
                       "strFirstName, " &
                       "strLastName, " &
                       "strSite, " &
                       "strStartDate, " &
                       "strEndDate, " &
                       "strSchedule, " &
                       "strVisualRestrictions, " &
                       "strPassFailNoShow) " &
                       "values (" &
                       "'" & StudentID & "', " &
                       "'" & Replace(name, "'", "''") & "', " &
                       "'" & Replace(LocationTerm, "'", "''") & "', " &
                       "'" & Replace(datePassed, "'", "''") & "', " &
                       "'" & Replace(FacilityName, "'", "''") & "', " &
                       "'" & Replace(Address1, "'", "''") & "', " &
                       "'" & Replace(Address2, "'", "''") & "', " &
                       "'" & Replace(city, "'", "''") & "', " &
                       "'" & Replace(state, "'", "''") & "', " &
                       "'" & Replace(zip, "'", "''") & "', " &
                       "'" & Replace(firstName, "'", "''") & "', " &
                       "'" & Replace(lastName, "'", "''") & "', " &
                       "'" & Replace(site, "'", "''") & "', " &
                       "'" & Replace(startDate, "'", "''") & "', " &
                       "'" & Replace(endDate, "'", "''") & "', " &
                       "'" & Replace(schedule, "'", "''") & "', " &
                       "'" & Replace(VisualRestrictions, "'", "''") & "', " &
                       "'" & Replace(PassFailNoShow, "'", "''") & "') "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr2 = cmd.ExecuteReader
                dr2.Close()
            End While
            dr.Close()

            MsgBox("The print information is ready for " & name)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region
#Region " Clear Routine "
    Private Sub btnClearRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearRes.Click
        Try
            clearRes()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnScheduleClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScheduleClear.Click
        Try
            clearSchedule()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnCacheClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCacheClear.Click
        Try
            clearCache()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region
#Region " Misc Routines "
    Private Sub numberOfStudents1()
        Try
            Dim cnt As Decimal

            If cboSchedule1.SelectedIndex = 0 Then
                MsgBox("You must select a Location/Term")
                Exit Sub
            End If

            If txtsortnbr.Text = "1" Then
                SQL = "select count(*) as numberOfStudents from SmokeSchoolReservation "
            Else
                SQL = "select count(*) as numberOfStudents from SmokeSchoolReservation " &
                "where strLocationDate = '" & cboSchedule1.SelectedItem & "'"
            End If

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Read()

            cnt = dr.Item("numberOfStudents")
            txtNumberOfStudents1.Text = cnt
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub numberOfAttendingLecture()
        Try
            Dim SQL As String
            Dim dr As SqlDataReader
            Dim cnt As Decimal
            Dim locationDate As String = cboSchedule1.SelectedItem

            SQL = "select count(*) as numberOfStudents " &
                  "from SmokeSchoolReservation " &
                  "where strLocationDate = '" & locationDate & "'" &
                  "and strLectureYesNo = 'YES'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Read()

            cnt = dr.Item("numberOfStudents")
            txtLectureCnt.Text = cnt
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub numberOfStudents3()
        Try
            Dim SQL As String
            Dim dr As SqlDataReader
            Dim cnt As Decimal

            SQL = "select count(*) as numberOfStudents3 from SmokeSchoolReservation " &
                  "where strLocationDate = '" & txtSchedule2.Text & "'"

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Read()

            cnt = dr.Item("numberOfStudents3")
            txtNumberOfStudents3.Text = cnt
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnSelectClass1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectClass1.Click
        Try
            If cboSchedule1.SelectedIndex = 0 Then
                MsgBox("You must select a Term and Location")
                Exit Sub
            End If

            If cboSchedule1.SelectedIndex <> 0 AndAlso cboSchedule1.SelectedIndex <> 1 Then
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        dgvRes.ExportToExcel(Me)
    End Sub

    Private Sub btnUpdateIDs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateIDs.Click
        Try
            updateUserIDs()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

                SQL = "select * from SmokeSchoolReservation " &
                "where upper(strLocationDate) = upper('" & Replace(locationDate, "'", "''") & "')"

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    studentID = studentID + 1
                    FirstName = dr.Item("strFirstName")
                    LastName = dr.Item("strLastName")

                    SQL = "update SmokeSchoolReservation " &
                          "set numUserID = " & studentID & " " &
                          " where upper(strLocationDate) = upper('" & Replace(locationDate, "'", "''") & "')" &
                          " and upper(strFirstName) = upper('" & Replace(FirstName, "'", "''") & "')" &
                          " and upper(strLastName) = upper('" & Replace(LastName, "'", "''") & "')"

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    dr2.Close()
                End While

                BindDataGridReservation()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnExportPassToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportPassToExcel.Click
        dgvScore3.ExportToExcel(Me)
    End Sub
    Private Sub btnActivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivate.Click
        Try
            SQL = "Select strUserEmail " &
            "from OlapUserLogIn " &
            "where strUserEmail = '" & Replace(UCase(txtEmailAddress.Text), "'", "''") & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                Dim updateString As String = "UPDATE OlapUserLogin " &
                          "SET strconfirm = to_char(sysdate, 'yyyy/mm/dd hh:mi:ss') " &
                          "WHERE struseremail = '" & Replace(UCase(txtEmailAddress.Text), "'", "''") & "' "
                cmd = New SqlCommand(updateString, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

                MsgBox("The account has been activated", MsgBoxStyle.Information, "Activated!")
            Else
                MsgBox("This user does not exist.", MsgBoxStyle.Exclamation, "Activate failed!")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

#End Region

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            BindDataGridSchedule2()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRefreshClasses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshClasses.Click
        Try
            LoadLocTerm3()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Public Function GetSmokeSchoolClassesByTerm(ByVal year As String, ByVal season As String) As DataTable
        Dim query As String = "SELECT strLocation, strStartDate, strEndDate " &
            " FROM SmokeSchoolSchedule " &
            " WHERE stryear = @pYear " &
            " AND strTerm   = @pTerm "

        Dim parameters As SqlParameter() = New SqlParameter() {
            New SqlParameter("@pYear", year),
            New SqlParameter("@pTerm", season)
        }

        Dim dataTable As DataTable = DB.GetDataTable(query, parameters)
        Return dataTable
    End Function

    Public Function GetSmokeSchoolPassingGradesByTerm(ByVal year As String, ByVal season As String) As DataTable
        Dim query As String = "SELECT strLastName, strFirstName, strLocationTerm, " &
            " SUBSTR(strlocationterm, instr(strlocationterm, '-', 1, 1)+2) strLocation, " &
            " row_number () over (partition BY strlocationterm order by strlastname, strfirstname DESC) CertId " &
            " FROM SmokeSchoolScores " &
            " WHERE strLocationTerm LIKE @pTerm " &
            " AND strPassFailNoShow = 'Pass' " &
            " ORDER BY strLocation, strLastName, strfirstname "

        '2013 Fall - %'
        Dim term As String = year.ToString & " " & season.ToString & " - %"
        Dim parameter As SqlParameter = New SqlParameter("@pTerm", term)

        Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
        Return dataTable
    End Function

    Private Sub newbtnRunDiplomaReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunDiplomaReport.Click
        Try

            Dim termYear As String
            If cboTermYear.Text <> "-Select a Year-" And IsNumeric(cboTermYear.Text) And cboTermYear.Items.Contains(cboTermYear.Text) Then
                termYear = cboTermYear.Text
            Else
                MessageBox.Show("Please select a valid year from the drop down.")
                Exit Sub
            End If

            Dim termSeason As String
            If rdbSpringTerm.Checked = True Or rdbFallTerm.Checked = True Then
                If rdbSpringTerm.Checked = True Then
                    termSeason = "Spring"
                Else
                    termSeason = "Fall"
                End If
            Else
                MessageBox.Show("Please select a term")
                Exit Sub
            End If

            Dim dtSmokeSchoolClasses As DataTable = GetSmokeSchoolClassesByTerm(termYear, termSeason)
            Dim dtSmokeSchoolPassingGrades As DataTable = GetSmokeSchoolPassingGradesByTerm(termYear, termSeason)

            dtSmokeSchoolPassingGrades.Columns.Add("CertNumber")
            dtSmokeSchoolPassingGrades.Columns.Add("ExpDate")
            dtSmokeSchoolPassingGrades.Columns.Add("ClassDates")

            Dim loc As String
            Dim endDate As Date
            Dim startDate As Date
            For Each row As DataRow In dtSmokeSchoolPassingGrades.Rows
                loc = row("strLocation")
                endDate = dtSmokeSchoolClasses.Select("STRLOCATION = '" & loc & "'")(0).Item("STRENDDATE")
                startDate = dtSmokeSchoolClasses.Select("STRLOCATION = '" & loc & "'")(0).Item("STRSTARTDATE")
                row.Item("CertNumber") = termYear.ToString & termSeason.Substring(0, 1) & CInt(row.Item("CertId")).ToString("D4") & loc.Substring(0, 3).ToUpper
                row.Item("ExpDate") = CStr(Format(CDate(endDate).AddMonths(6), "dd-MMM-yyyy"))
                If endDate = startDate Then
                    row.Item("ClassDates") = CStr(Format(CDate(startDate), "dd-MMM-yyyy"))
                Else
                    row.Item("ClassDates") = CStr(Format(CDate(startDate), "dd-MMM-yyyy")) & " to " & CStr(Format(CDate(endDate), "dd-MMM-yyyy"))
                End If
            Next

            dtSmokeSchoolPassingGrades.Columns("CertId").SetOrdinal(0)
            dtSmokeSchoolPassingGrades.Columns("strLastName").SetOrdinal(1)
            dtSmokeSchoolPassingGrades.Columns("strFirstName").SetOrdinal(2)
            dtSmokeSchoolPassingGrades.Columns("CertNumber").SetOrdinal(3)
            dtSmokeSchoolPassingGrades.Columns("ExpDate").SetOrdinal(4)
            dtSmokeSchoolPassingGrades.Columns("ClassDates").SetOrdinal(5)
            dtSmokeSchoolPassingGrades.Columns.Remove("strLocationTerm")
            dtSmokeSchoolPassingGrades.Columns.Remove("strLocation")

            dgvDiplomas.DataSource = dtSmokeSchoolPassingGrades
            dgvDiplomas.Columns("CertId").HeaderText = "User ID"
            dgvDiplomas.Columns("strLastName").HeaderText = "Last Name"
            dgvDiplomas.Columns("strFirstName").HeaderText = "First Name"
            dgvDiplomas.Columns("CertNumber").HeaderText = "Certification #"
            dgvDiplomas.Columns("ExpDate").HeaderText = "Expiration Date"
            dgvDiplomas.Columns("ClassDates").HeaderText = "Schedule Date"

            dgvDiplomas.SanelyResizeColumns()

            txtDiplomaCount.Text = dgvDiplomas.RowCount.ToString
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnExportDiplomas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportDiplomas.Click
        dgvDiplomas.ExportToExcel(Me)
    End Sub

End Class