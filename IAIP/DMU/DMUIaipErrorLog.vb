﻿Imports System.Data.SqlClient
Imports EpdIt

Public Class DMUIaipErrorLog

#Region " Page Load Functions "

    Private Sub DMUDeveloperTools_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        Dim query As String = "Select " &
            " strErrorNumber, " &
            " (strLastName + ', ' + strFirstName) as ErrorUser, " &
            " strErrorLocation, strErrorMessage, " &
            " datErrorDate as ErrorDate, " &
            " strSolution " &
            " from IAIPErrorLog, EPDUserProfiles " &
            " where IAIPErrorLog.strUser = EPDUserProfiles.numUserID "

        If rdbViewResolvedErrors.Checked = True Then
            query = query & " and strSolution IS NOT NUll "
        ElseIf rdbViewUnresolvedErrors.Checked = True Then
            query = query & " and strSolution IS NULL "
        End If

        If rdbLast30Days.Checked = True Then
            query = query & " and datErrorDate > dateadd(mm, -1, getdate()) "
        ElseIf rdbLast60days.Checked = True Then
            query = query & " and datErrorDate > dateadd(mm, -2, getdate()) "
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
        dgvErrorList.SanelyResizeColumns

        txtErrorCount.Text = ErrorLog.Rows.Count.ToString

    End Sub

    Private Sub LoadWebErrorLog()

        Dim query As String = "select numError, " &
            "strIPAddress, strUserEmail, " &
            "strErrorPage, dateTimeStamp, " &
            "strErrorMsg, strSolution " &
            "From OLAPERRORLog "

        If rdbResolvedWebErrors.Checked = True Then
            query = query & " where strSolution IS NOT NULL "
        ElseIf rdbUnresolvedWebErrors.Checked = True Then
            query = query & " where strSolution IS NULL "
        End If

        Dim ErrorLog As DataTable = DB.GetDataTable(query)
        dgrWebErrorList.DataSource = ErrorLog

        txtWebErrorCount.Text = ErrorLog.Rows.Count.ToString

    End Sub

    Private Sub btnFilterErrors_Click(sender As Object, e As EventArgs) Handles btnFilterErrors.Click
        LoadErrorLog()
    End Sub

    Private Sub btnSaveError_Click(sender As Object, e As EventArgs) Handles btnSaveError.Click
        Dim ErrorSolution As String = ""

        If txtErrorSolution.Text <> "" Then
            ErrorSolution = Mid(txtErrorSolution.Text, 1, 4000)
        End If

        If txtErrorNumber.Text <> "" Then
            Dim query As String = "Update IAIPErrorLog set " &
            "strSolution = @errSol " &
            "where strErrornumber = @errNum "

            Dim Parameters As SqlParameter() = {
                New SqlParameter("@errSol", ErrorSolution),
                New SqlParameter("@errNum", txtErrorNumber.Text)
            }

            DB.RunCommand(query, Parameters)

            MessageBox.Show("Solution Saved", "Date Management Tools", MessageBoxButtons.OK)
        Else
            MessageBox.Show("Select an error", "Data Management Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnFilterWebErrors_Click(sender As Object, e As EventArgs) Handles btnFilterWebErrors.Click
        LoadWebErrorLog()
    End Sub

    Private Sub dgrWebErrorList_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrWebErrorList.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrWebErrorList.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrWebErrorList(hti.Row, 0)) Then
                    txtWebErrorNumber.Text = ""
                Else
                    txtWebErrorNumber.Text = dgrWebErrorList(hti.Row, 0)
                End If

                If txtWebErrorNumber.Text <> "" Then

                    Dim query As String = "select numError, " &
                    " strIPAddress, strUserEmail, " &
                    " strErrorPage, dateTimeStamp, " &
                    " strErrorMsg, strSolution " &
                    " From OLAPERRORLog " &
                    " where numError = @errNum "

                    Dim parameter As SqlParameter = New SqlParameter("@errNum", txtWebErrorNumber.Text)

                    Dim row As DataRow = DB.GetDataRow(query, parameter)

                    If row IsNot Nothing Then
                        txtIPAddress.Text = DBUtilities.GetNullable(Of String)(row("strIPAddress"))
                        txtWebErrorUser.Text = DBUtilities.GetNullable(Of String)(row("strUserEmail"))
                        txtWebErrorLocation.Text = DBUtilities.GetNullable(Of String)(row("strErrorPage"))
                        txtWebErrorDate.Text = DBUtilities.GetNullable(Of String)(row("dateTimeStamp"))
                        txtWebErrorMessage.Text = DBUtilities.GetNullable(Of String)(row("strErrorMsg"))
                        txtWebErrorSolution.Text = DBUtilities.GetNullable(Of String)(row("strSolution"))
                    End If

                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveWebErrorSolution_Click(sender As Object, e As EventArgs) Handles btnSaveWebErrorSolution.Click
        Dim ErrorSolution As String = ""

        If txtWebErrorSolution.Text <> "" Then
            ErrorSolution = Mid(txtWebErrorSolution.Text, 1, 4000)
        End If

        If txtWebErrorNumber.Text <> "" Then
            Dim query As String = "Update OLAPErrorLog set " &
            "strSolution = @errSol " &
            "where numError = @errNum "

            Dim Parameters As SqlParameter() = {
                New SqlParameter("@errSol", ErrorSolution),
                New SqlParameter("@errNum", txtWebErrorNumber.Text)
            }

            DB.RunCommand(query, Parameters)

            MessageBox.Show("Solution Saved", "Date Management Tools", MessageBoxButtons.OK)
        Else
            MessageBox.Show("Select an error", "Data Management Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub dgvErrorList_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvErrorList.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvErrorList.HitTest(e.X, e.Y)
            If dgvErrorList.RowCount > 0 And hti.RowIndex <> -1 Then
                If IsDBNull(dgvErrorList(0, hti.RowIndex).Value) Then
                    txtErrorNumber.Text = ""
                Else
                    txtErrorNumber.Text = dgvErrorList(0, hti.RowIndex).Value
                End If

                If txtErrorNumber.Text <> "" Then

                    Dim query As String = "Select " &
                    "strErrorNumber, " &
                    "(strLastName + ', ' + strFirstName) as ErrorUser,  " &
                    "strErrorLocation, strErrorMessage,  " &
                    "datErrorDate as ErrorDate,  " &
                    "strSolution  " &
                    "from IAIPErrorLog, EPDUserProfiles  " &
                    "where IAIPErrorLog.strUser = EPDUserProfiles.numUserID " &
                    "and strErrorNumber = '" & txtErrorNumber.Text & "' "

                    Dim parameter As SqlParameter = New SqlParameter("@errNum", txtErrorNumber.Text)

                    Dim row As DataRow = DB.GetDataRow(query, parameter)

                    If row IsNot Nothing Then
                        txtErrorUser.Text = DBUtilities.GetNullable(Of String)(row("ErrorUser"))
                        txtErrorLocation.Text = DBUtilities.GetNullable(Of String)(row("strErrorLocation"))
                        txtErrorDate.Text = DBUtilities.GetNullable(Of String)(row("ErrorDate"))
                        txtErrorSolution.Text = DBUtilities.GetNullable(Of String)(row("strSolution"))
                        txtErrorMessage.Text = DBUtilities.GetNullable(Of String)(row("strErrorMessage"))
                    End If

                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

End Class
