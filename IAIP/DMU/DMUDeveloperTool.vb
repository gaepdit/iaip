Imports Oracle.DataAccess.Client

Public Class DMUDeveloperTool

#Region " Page Load Functions "

    Private Sub DMUDeveloperTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        LoadPermissions()
    End Sub

    Private Sub LoadPermissions()
        rdbViewUnresolvedErrors.Checked = True
        rdbUnresolvedWebErrors.Checked = True
        FormatWebErrorListGrid()
    End Sub

    Private Sub FormatWebErrorListGrid()

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

    End Sub

#End Region

#Region " Error Log procedures "

    Private Sub LoadErrorLog()
        Dim query As String = "Select " & _
            " strErrorNumber, " & _
            " (strLastName||', '||strFirstName) as ErrorUser, " & _
            " strErrorLocation, strErrorMessage, " & _
            " to_char(datErrorDate, 'DD-Mon-YYYY') as ErrorDate, " & _
            " strSolution " & _
            " from AIRBranch.IAIPErrorLog, AIRBranch.EPDUserProfiles " & _
            " where AIRBranch.IAIPErrorLog.strUser = AIRBranch.EPDUserProfiles.numUserID "

        If rdbViewResolvedErrors.Checked = True Then
            query = query & " and strSolution IS NOT NUll "
        ElseIf rdbViewUnresolvedErrors.Checked = True Then
            query = query & " and strSolution IS NULL "
        End If

        If rdbLast30Days.Checked = True Then
            query = query & " and datErrorDate > add_months(sysdate, -1) "
        ElseIf rdbLast60days.Checked = True Then
            query = query & " and datErrorDate > add_months(sysdate, -2) "
        End If

        query = query & " Order by strErrornumber desc "

        Dim ErrorLog As DataTable = DB.GetDataTable(query)

        dgvErrorList.DataSource = ErrorLog

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

        txtErrorCount.Text = ErrorLog.Rows.Count.ToString

    End Sub

    Private Sub LoadWebErrorLog()

        Dim query As String = "select numError, " & _
            "strIPAddress, strUserEmail, " & _
            "strErrorPage, dateTimeStamp, " & _
            "strErrorMsg, strSolution " & _
            "From AIRBranch.OLAPERRORLog "

        If rdbResolvedWebErrors.Checked = True Then
            query = query & " where strSolution IS NOT NULL "
        ElseIf rdbUnresolvedWebErrors.Checked = True Then
            query = query & " where strSolution IS NULL "
        End If

        Dim ErrorLog As DataTable = DB.GetDataTable(query)
        dgrWebErrorList.DataSource = ErrorLog

        txtWebErrorCount.Text = ErrorLog.Rows.Count.ToString

    End Sub

    Private Sub btnFilterErrors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterErrors.Click
        LoadErrorLog()
    End Sub

    Private Sub btnSaveError_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveError.Click
        Dim ErrorSolution As String = ""

        If txtErrorSolution.Text <> "" Then
            ErrorSolution = Mid(txtErrorSolution.Text, 1, 4000)
        End If

        If txtErrorNumber.Text <> "" Then
            Dim query As String = "Update AIRBRANCH.IAIPErrorLog set " & _
            "strSolution = :errSol " & _
            "where strErrornumber = :errNum "

            Dim Parameters As OracleParameter() = { _
            New OracleParameter("errSol", ErrorSolution), _
            New OracleParameter("errNum", txtErrorNumber.Text) _
            }

            DB.RunCommand(query, Parameters)

            MessageBox.Show("Solution Saved", "Date Management Tools", MessageBoxButtons.OK)
        Else
            MessageBox.Show("Select an error", "Data Management Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnFilterWebErrors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterWebErrors.Click
        LoadWebErrorLog()
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

                    Dim query As String = "select numError, " & _
                    " strIPAddress, strUserEmail, " & _
                    " strErrorPage, dateTimeStamp, " & _
                    " strErrorMsg, strSolution " & _
                    " From AIRBRANCH.OLAPERRORLog " & _
                    " where numError = :errNum "

                    Dim parameter As OracleParameter = New OracleParameter("errNum", txtWebErrorNumber.Text)

                    Dim row As DataRow = DB.GetDataRow(query, parameter)

                    If row IsNot Nothing Then
                        txtIPAddress.Text = DB.GetNullable(Of String)(row("strIPAddress"))
                        txtWebErrorUser.Text = DB.GetNullable(Of String)(row("strUserEmail"))
                        txtWebErrorLocation.Text = DB.GetNullable(Of String)(row("strErrorPage"))
                        txtWebErrorDate.Text = DB.GetNullable(Of String)(row("dateTimeStamp"))
                        txtWebErrorMessage.Text = DB.GetNullable(Of String)(row("strErrorMsg"))
                        txtWebErrorSolution.Text = DB.GetNullable(Of String)(row("strSolution"))
                    End If

                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveWebErrorSolution_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveWebErrorSolution.Click
        Dim ErrorSolution As String = ""

        If txtWebErrorSolution.Text <> "" Then
            ErrorSolution = Mid(txtWebErrorSolution.Text, 1, 4000)
        End If

        If txtWebErrorNumber.Text <> "" Then
            Dim query As String = "Update AIRBRANCH.OLAPErrorLog set " & _
            "strSolution = :errSol " & _
            "where numError = :errNum "

            Dim Parameters As OracleParameter() = { _
            New OracleParameter("errSol", ErrorSolution), _
            New OracleParameter("errNum", txtWebErrorNumber.Text) _
            }

            DB.RunCommand(query, Parameters)

            MessageBox.Show("Solution Saved", "Date Management Tools", MessageBoxButtons.OK)
        Else
            MessageBox.Show("Select an error", "Data Management Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub dgvErrorList_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvErrorList.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvErrorList.HitTest(e.X, e.Y)
            If dgvErrorList.RowCount > 0 And hti.RowIndex <> -1 Then
                If IsDBNull(dgvErrorList(0, hti.RowIndex).Value) Then
                    txtErrorNumber.Text = ""
                Else
                    txtErrorNumber.Text = dgvErrorList(0, hti.RowIndex).Value
                End If

                If txtErrorNumber.Text <> "" Then

                    Dim query As String = "Select " & _
                    "strErrorNumber, " & _
                    "(strLastName||', '||strFirstName) as ErrorUser,  " & _
                    "strErrorLocation, strErrorMessage,  " & _
                    "to_char(datErrorDate, 'DD-Mon-YYYY') as ErrorDate,  " & _
                    "strSolution  " & _
                    "from AIRBRANCH.IAIPErrorLog, AIRBRANCH.EPDUserProfiles  " & _
                    "where AIRBRANCH.IAIPErrorLog.strUser = AIRBRANCH.EPDUserProfiles.numUserID " & _
                    "and strErrorNumber = '" & txtErrorNumber.Text & "' "

                    Dim parameter As OracleParameter = New OracleParameter("errNum", txtErrorNumber.Text)

                    Dim row As DataRow = DB.GetDataRow(query, parameter)

                    If row IsNot Nothing Then
                        txtErrorUser.Text = DB.GetNullable(Of String)(row("ErrorUser"))
                        txtErrorLocation.Text = DB.GetNullable(Of String)(row("strErrorLocation"))
                        txtErrorDate.Text = DB.GetNullable(Of String)(row("ErrorDate"))
                        txtErrorSolution.Text = DB.GetNullable(Of String)(row("strSolution"))
                        txtErrorMessage.Text = DB.GetNullable(Of String)(row("strErrorMessage"))
                    End If

                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

End Class
