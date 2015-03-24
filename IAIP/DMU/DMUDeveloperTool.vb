Imports System.DateTime
Imports Oracle.DataAccess.Client
Imports System.IO
Imports System.Data.OleDb
Imports System.Text.RegularExpressions

Public Class DMUDeveloperTool
    Dim SQL, SQL2 As String
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim airsno As String
    Dim dsErrorLog As DataSet
    Dim daErrorLog As OracleDataAdapter
    Dim dsWebErrorLog As DataSet
    Dim daWebErrorLog As OracleDataAdapter
    Dim TriggerStatus As String

    Private Sub DMUDeveloperTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            LoadPermissions()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Page Load Functions"
    Sub LoadPermissions()

        TCDMUTools.TabPages.Remove(TPErrorLog)
        TCDMUTools.TabPages.Remove(TPWebErrorLog)

        If AccountFormAccess(129, 3) = "1" Or AccountFormAccess(129, 4) = "1" Then
            TCDMUTools.TabPages.Add(TPErrorLog)
            rdbViewUnresolvedErrors.Checked = True

            TCDMUTools.TabPages.Add(TPWebErrorLog)
            Me.rdbUnresolvedWebErrors.Checked = True
            FormatWebErrorListGrid()
        End If

    End Sub
    Sub FormatWebErrorListGrid()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "WebErrorLog"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            '0
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "NumError"
            objtextcol.HeaderText = "Error #"
            objtextcol.Width = 50
            objGrid.GridColumnStyles.Add(objtextcol)

            '1
            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strUserEmail"
            objtextcol.HeaderText = "User Email"
            objtextcol.Width = 200
            objGrid.GridColumnStyles.Add(objtextcol)

            '2
            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strErrorPage"
            objtextcol.HeaderText = "Web Page"
            objtextcol.Width = 180
            objGrid.GridColumnStyles.Add(objtextcol)

            '3
            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "dateTimeStamp"
            objtextcol.HeaderText = "Error Time Stamp"
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            '4
            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strErrorMsg"
            objtextcol.HeaderText = "Details"
            objtextcol.Width = 400
            objGrid.GridColumnStyles.Add(objtextcol)

            '5
            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strSolution"
            objtextcol.HeaderText = "Error Solution"
            objtextcol.Width = 200
            objGrid.GridColumnStyles.Add(objtextcol)

            '6
            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strIPAddress"
            objtextcol.HeaderText = "IP Address"
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrWebErrorList.TableStyles.Clear()
            dgrWebErrorList.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrWebErrorList.CaptionText = "Web Error Log"
            dgrWebErrorList.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

#Region "Subs and Functions"

    Sub LoadErrorLog()
        Try
            SQL = "Select " & _
            "strErrorNumber, " & _
            "(strLastName||', '||strFirstName) as ErrorUser,  " & _
            "strErrorLocation, strErrorMessage,  " & _
            "to_char(datErrorDate, 'DD-Mon-YYYY') as ErrorDate,  " & _
            "strSolution  " & _
            "from AIRBranch.IAIPErrorLog, AIRBranch.EPDUserProfiles  " & _
            "where AIRBranch.IAIPErrorLog.strUser = AIRBranch.EPDUserProfiles.numUserID "


            If rdbViewAllErrors.Checked = True Then
                SQL = SQL
            End If
            If rdbViewResolvedErrors.Checked = True Then
                SQL = SQL & " and strSolution IS NOT NUll "
            End If
            If rdbViewUnresolvedErrors.Checked = True Then
                SQL = SQL & " and strSolution IS NULL "
            End If

            'add_month(sysdate, 1
            If rdbLast30Days.Checked = True Then
                SQL = SQL & " and datErrorDate > add_months(sysdate, -1)  "
            End If
            If rdbLast60days.Checked = True Then
                SQL = SQL & " and datErrorDate > add_months(sysdate, -2)  "
            End If
            If rdbNoLimit.Checked = True Then

            End If
            SQL = SQL & "Order by strErrornumber desc "

            If SQL <> "" Then
                dsErrorLog = New DataSet
                daErrorLog = New OracleDataAdapter(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                daErrorLog.Fill(dsErrorLog, "ErrorLog")

                dgvErrorList.DataSource = dsErrorLog
                dgvErrorList.DataMember = "ErrorLog"

                dgvErrorList.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvErrorList.AllowUserToResizeColumns = True
                dgvErrorList.AllowUserToResizeRows = True
                dgvErrorList.AllowUserToAddRows = False
                dgvErrorList.AllowUserToDeleteRows = False
                dgvErrorList.AllowUserToOrderColumns = True
                dgvErrorList.Columns("strErrorNumber").HeaderText = "Error #"
                dgvErrorList.Columns("strErrorNumber").DisplayIndex = 0
                dgvErrorList.Columns("ErrorUser").HeaderText = "User"
                dgvErrorList.Columns("ErrorUser").DisplayIndex = 1
                dgvErrorList.Columns("ErrorUser").Width = 300

                dgvErrorList.Columns("strErrorLocation").HeaderText = "Error Location"
                dgvErrorList.Columns("strErrorLocation").DisplayIndex = 2
                dgvErrorList.Columns("strErrorMessage").HeaderText = "Error"
                dgvErrorList.Columns("strErrorMessage").DisplayIndex = 3

                dgvErrorList.Columns("ErrorDate").HeaderText = "Error Date"
                dgvErrorList.Columns("ErrorDate").DisplayIndex = 4
                dgvErrorList.Columns("strSolution").HeaderText = "Solution"
                dgvErrorList.Columns("strSolution").DisplayIndex = 5


                txtErrorCount.Text = dsErrorLog.Tables(0).Rows.Count.ToString

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadWegErrorLog()
        Try


            SQL = "Select " & _
            "strIPAddress, strAgent, strPage, " & _
            "strTime, " & _
            "strDetails, numError, " & _
            "strSolution " & _
            "from AIRBranch.LogErrors "


            SQL = "select numError, " & _
            "strIPAddress, strUserEmail, " & _
            "strErrorPage, dateTimeStamp, " & _
            "strErrorMsg, strSolution " & _
            "From AIRBranch.OLAPERRORLog "

            If rdbAllWebErrors.Checked = True Then
                SQL = SQL
            End If
            If rdbResolvedWebErrors.Checked = True Then
                SQL = SQL & " where strSolution IS NOT NULL "
            End If
            If rdbUnresolvedWebErrors.Checked = True Then
                SQL = SQL & " where strSolution IS NULL "
            End If

            If SQL <> "" Then
                dsWebErrorLog = New DataSet
                daWebErrorLog = New OracleDataAdapter(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                daWebErrorLog.Fill(dsWebErrorLog, "WebErrorLog")
                dgrWebErrorList.DataSource = dsWebErrorLog
                dgrWebErrorList.DataMember = "WebErrorLog"

                txtWebErrorCount.Text = dsWebErrorLog.Tables(0).Rows.Count.ToString

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

    Private Sub btnFilterErrors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterErrors.Click
        Try
            LoadErrorLog()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnSaveError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveError.Click
        Try


            Dim ErrorSolution As String = ""
            If txtErrorSolution.Text <> "" Then
                ErrorSolution = Mid(txtErrorSolution.Text, 1, 4000)
            End If
            If txtErrorNumber.Text <> "" Then
                SQL = "Update AIRBRANCH.IAIPErrorLog set " & _
                "strSolution = '" & Replace(ErrorSolution, "'", "''") & "' " & _
                "where strErrornumber = '" & txtErrorNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                dr.Read()
                dr.Close()

                MsgBox("Solution Saved", MsgBoxStyle.Information, "Date Management Tools")
            Else
                MsgBox("Select an error", MsgBoxStyle.Exclamation, "Data Management Tools")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnFilterWebErrors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterWebErrors.Click
        Try

            LoadWegErrorLog()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgrWebErrorList_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrWebErrorList.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrWebErrorList.HitTest(e.X, e.Y)
        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrWebErrorList(hti.Row, 0)) Then
                    txtWebErrorNumber.Text = ""
                Else
                    txtWebErrorNumber.Text = dgrWebErrorList(hti.Row, 0)
                End If

                If txtWebErrorNumber.Text <> "" Then
                    SQL = "Select " & _
                    "strIPAddress, strAgent, strPage, " & _
                    "strTime, strDetails, numError, " & _
                    "strSolution " & _
                    "from AIRBRANCH.LogErrors " & _
                    "where NumError = " & txtWebErrorNumber.Text & " "

                    SQL = "select numError, " & _
                    "strIPAddress, strUserEmail, " & _
                    "strErrorPage, dateTimeStamp, " & _
                    "strErrorMsg, strSolution " & _
                    "From AIRBRANCH.OLAPERRORLog " & _
                    "where numError = " & txtWebErrorNumber.Text & " "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        If IsDBNull(dr.Item("strIPAddress")) Then
                            txtIPAddress.Text = ""
                        Else
                            txtIPAddress.Text = dr.Item("strIPAddress")
                        End If
                        If IsDBNull(dr.Item("strUserEmail")) Then
                            txtWebErrorUser.Text = ""
                        Else
                            txtWebErrorUser.Text = dr.Item("strUserEmail")
                        End If
                        If IsDBNull(dr.Item("strErrorPage")) Then
                            txtWebErrorLocation.Text = ""
                        Else
                            txtWebErrorLocation.Text = dr.Item("strErrorPage")
                        End If
                        If IsDBNull(dr.Item("dateTimeStamp")) Then
                            txtWebErrorDate.Text = ""
                        Else
                            txtWebErrorDate.Text = dr.Item("dateTimeStamp")
                        End If
                        If IsDBNull(dr.Item("strErrorMsg")) Then
                            txtWebErrorMessage.Text = ""
                        Else
                            txtWebErrorMessage.Text = dr.Item("strErrorMsg")
                        End If
                        If IsDBNull(dr.Item("strSolution")) Then
                            txtWebErrorSolution.Text = ""
                        Else
                            txtWebErrorSolution.Text = dr.Item("strSolution")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnSaveWebErrorSolution_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveWebErrorSolution.Click
        Try
            Dim ErrorSolution As String = ""
            If txtWebErrorSolution.Text <> "" Then
                ErrorSolution = Mid(txtWebErrorSolution.Text, 1, 4000)
            End If
            If txtWebErrorNumber.Text <> "" Then
                SQL = "Update AIRBRANCH.OLAPErrorLog set " & _
                "strSolution = '" & Replace(ErrorSolution, "'", "''") & "' " & _
                "where numError = '" & txtWebErrorNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                dr.Read()
                dr.Close()

                MsgBox("Solution Saved", MsgBoxStyle.Information, "Date Management Tools")
            Else
                MsgBox("Select an error", MsgBoxStyle.Exclamation, "Data Management Tools")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub btnExporttoExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExporttoExcel.Click
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        Dim i, j As Integer

        Try
            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvErrorList.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvErrorList.ColumnCount - 1
                        .Cells(1, i + 1) = dgvErrorList.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvErrorList.ColumnCount - 1
                        For j = 0 To dgvErrorList.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvErrorList.Item(i, j).Value.ToString
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

    Private Sub dgvErrorList_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvErrorList.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvErrorList.HitTest(e.X, e.Y)
            If dgvErrorList.RowCount > 0 And hti.RowIndex <> -1 Then
                '    If dgvFeeStats.Columns(0).HeaderText = "Airs No." Then
                '        If IsDBNull(dgvFeeStats(0, hti.RowIndex).Value) Then
                '            txtFeeStatAirsNumber.Clear()
                '        Else
                '            txtFeeStatAirsNumber.Text = dgvFeeStats(0, hti.RowIndex).Value
                '        End If
                '    End If
                If IsDBNull(dgvErrorList(0, hti.RowIndex).Value) Then
                    txtErrorNumber.Text = ""
                Else
                    txtErrorNumber.Text = dgvErrorList(0, hti.RowIndex).Value
                End If

                If txtErrorNumber.Text <> "" Then
                    SQL = "Select " & _
                    "strErrorNumber, " & _
                    "(strLastName||', '||strFirstName) as ErrorUser,  " & _
                    "strErrorLocation, strErrorMessage,  " & _
                    "to_char(datErrorDate, 'DD-Mon-YYYY') as ErrorDate,  " & _
                    "strSolution  " & _
                    "from AIRBRANCH.IAIPErrorLog, AIRBRANCH.EPDUserProfiles  " & _
                    "where AIRBRANCH.IAIPErrorLog.strUser = AIRBRANCH.EPDUserProfiles.numUserID " & _
                    "and strErrorNumber = '" & txtErrorNumber.Text & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        If IsDBNull(dr.Item("ErrorUser")) Then
                            txtErrorUser.Text = ""
                        Else
                            txtErrorUser.Text = dr.Item("ErrorUser")
                        End If
                        If IsDBNull(dr.Item("strErrorLocation")) Then
                            txtErrorLocation.Text = ""
                        Else
                            txtErrorLocation.Text = dr.Item("strErrorLocation")
                        End If
                        If IsDBNull(dr.Item("ErrorDate")) Then
                            txtErrorDate.Text = ""
                        Else
                            txtErrorDate.Text = dr.Item("ErrorDate")
                        End If
                        If IsDBNull(dr.Item("strSolution")) Then
                            txtErrorSolution.Text = ""
                        Else
                            txtErrorSolution.Text = dr.Item("strSolution")
                        End If
                        If IsDBNull(dr.Item("strErrorMessage")) Then
                            txtErrorMessage.Text = ""
                        Else
                            txtErrorMessage.Text = dr.Item("strErrorMessage")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

End Class
