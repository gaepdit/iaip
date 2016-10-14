Imports System.Data.SqlClient
Imports Iaip.SharedData

Public Class IAIPLookUpTables
    Dim SQL As String
    Dim ds As DataSet
    Dim da As SqlDataAdapter
    Dim recExist As Boolean
    Dim dr As SqlDataReader
    Dim dr2 As SqlDataReader
    Dim cmd As SqlCommand
    Dim cmd2 As SqlCommand

#Region " Form load "

    Private Sub IAIPLookUpTables_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadAPBManagement()

    End Sub

#End Region

#Region "ApplicationTypes"

    Private Sub LoadApplicationTypes()
        Try
            SQL = "Select " &
            "CONVERT(int, strApplicationTypeCode) as strApplicationTypeCode, " &
            "strApplicationTypeDesc, " &
            "strApplicationTypeUsed " &
            "From LookUpApplicationTypes " &
            "order by strApplicationTypeDesc "

            ds = New DataSet
            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "ApplicationTypes")
            dgvApplicationType.DataSource = ds
            dgvApplicationType.DataMember = "ApplicationTypes"

            dgvApplicationType.RowHeadersVisible = False
            dgvApplicationType.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvApplicationType.AllowUserToResizeColumns = True
            dgvApplicationType.AllowUserToAddRows = False
            dgvApplicationType.AllowUserToDeleteRows = False
            dgvApplicationType.AllowUserToOrderColumns = True
            dgvApplicationType.AllowUserToResizeRows = True

            dgvApplicationType.Columns("strApplicationTypeCode").HeaderText = "ID"
            dgvApplicationType.Columns("strApplicationTypeCode").DisplayIndex = 0
            'dgvApplicationType.Columns("").DefaultCellStyle = number
            dgvApplicationType.Columns("strApplicationTypeCode").Width = dgvApplicationType.Width * 0.15
            dgvApplicationType.Columns("strApplicationTypeDesc").HeaderText = "App Type"
            dgvApplicationType.Columns("strApplicationTypeDesc").DisplayIndex = 1
            dgvApplicationType.Columns("strApplicationTypeDesc").Width = dgvApplicationType.Width * 0.35
            dgvApplicationType.Columns("strApplicationTypeUsed").HeaderText = "App Used"
            dgvApplicationType.Columns("strApplicationTypeUsed").DisplayIndex = 2
            dgvApplicationType.Columns("strApplicationTypeUsed").Width = dgvApplicationType.Width * 0.5

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnLoadApplicationTypes_Click(sender As Object, e As EventArgs) Handles btnLoadApplicationTypes.Click
        LoadApplicationTypes()
    End Sub

    Private Sub dgvApplicationType_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvApplicationType.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvApplicationType.HitTest(e.X, e.Y)
        Dim temp As String = ""
        Try

            chbActiveAppType.Checked = False

            If dgvApplicationType.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvApplicationType.Columns(0).HeaderText = "ID" Then
                    txtApplicationID.Text = dgvApplicationType(0, hti.RowIndex).Value
                    txtApplicationDesc.Text = dgvApplicationType(1, hti.RowIndex).Value
                    If IsDBNull(dgvApplicationType(2, hti.RowIndex).Value) Then
                        chbActiveAppType.Checked = True
                    Else
                        temp = dgvApplicationType(2, hti.RowIndex).Value
                        If temp = "True" Then
                            chbActiveAppType.Checked = True
                        Else
                            chbActiveAppType.Checked = False
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub btnClearAppTypes_Click(sender As Object, e As EventArgs) Handles btnClearAppTypes.Click
        txtApplicationID.Clear()
        txtApplicationDesc.Clear()
        chbActiveAppType.Checked = False
    End Sub

    Private Sub btnAddEditAppType_Click(sender As Object, e As EventArgs) Handles btnAddNewAppType.Click
        Try
            Dim AppStatus As String

            If txtApplicationID.Text <> "" Then
                'update
                MsgBox("The ID is not empty." & vbCrLf & "Either clear the form first or use the Edit button.", MsgBoxStyle.Exclamation, "Look Up Tables")
            Else
                'insert 
                If chbActiveAppType.Checked = True Then
                    AppStatus = True
                Else
                    AppStatus = False
                End If

                SQL = "Insert into LookUpApplicationTypes " &
                "values " &
                "((Select max(CONVERT(int, strApplicationTypeCode)) + 1 as MaxID " &
                "from LookUpApplicationTypes), " &
                "'" & txtApplicationDesc.Text & "', '" & AppStatus & "') "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select max(CONVERT(int, strApplicationTypeCode)) + 1 as MaxID " &
                "from LookUpApplicationTypes "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("MaxID")) Then
                        txtApplicationID.Clear()
                    Else
                        txtApplicationID.Text = dr.Item("MaxID")
                    End If
                End While
                dr.Close()

                LoadApplicationTypes()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEditAppType_Click(sender As Object, e As EventArgs) Handles btnEditAppType.Click
        Try
            Dim temp As String = ""
            If chbActiveAppType.Checked = True Then
                temp = "True"
            Else
                temp = "False"
            End If

            If txtApplicationID.Text <> "" Then
                SQL = "Update LookUpApplicationTypes set " &
                "strApplicationTypeDesc = '" & Replace(txtApplicationDesc.Text, "'", "''") & "', " &
                "strApplicationTypeUsed = '" & temp & "' " &
                "where strApplicationTypeCode = '" & txtApplicationID.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                LoadApplicationTypes()

                MsgBox("Updated", MsgBoxStyle.Information, "Look Up Tables")
            Else
                MsgBox("Select a valid Application Type first", MsgBoxStyle.Information, "Look Up Tables")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnDeleteAppType_Click(sender As Object, e As EventArgs) Handles btnDeleteAppType.Click
        Try
            Dim temp As String
            If txtApplicationID.Text <> "" Then
                SQL = "Select Count(*) as IDUsed " &
                "from SSPPApplicationMaster " &
                "where strApplicationType = '" & txtApplicationID.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("IDUsed")) Then
                        temp = "Delete"
                    Else
                        temp = dr.Item("IDUsed")
                        If temp > 0 Then
                            temp = "Keep"
                        End If
                    End If
                Else
                    temp = "Delete"
                End If
                dr.Close()

                If temp <> "Keep" Then
                    SQL = "delete LookUpApplicationTypes " &
                    "where strApplicationTypeCode = '" & txtApplicationID.Text & "' "
                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    LoadApplicationTypes()

                    MsgBox("Deleted", MsgBoxStyle.Information, "Look Up Tables")
                Else
                    MsgBox("Cannot DELETE entry because it is already being used.", MsgBoxStyle.Information, "Look Up Tables")
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " EPD Management "

    Private Sub LoadAPBManagement()
        dgvLookUpManagement.DataSource = GetSharedData(SharedTable.EpdManagers)

        With dgvLookUpManagement
            .RowHeadersVisible = False
            .AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            .AllowUserToResizeColumns = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = False
            .AllowUserToResizeRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .ReadOnly = True
            .SanelyResizeColumns
        End With
    End Sub

    Private Sub dgvLookUpManagement_SelectionChanged(sender As Object, e As EventArgs) Handles dgvLookUpManagement.SelectionChanged
        If dgvLookUpManagement.SelectedRows.Count = 1 Then
            txtApbManagementType.Text = dgvLookUpManagement.SelectedRows(0).Cells("Role").Value
            txtApbManagementName.Text = dgvLookUpManagement.SelectedRows(0).Cells("Name").Value
        End If
    End Sub

    Private Sub SaveAPBManagement()
        If Not String.IsNullOrEmpty(txtApbManagementType.Text) Then
            Dim role As DAL.EpdManagementTypes
            If [Enum].TryParse(txtApbManagementType.Text, role) Then
                If DAL.SaveEpdManagerName(role, txtApbManagementName.Text) Then
                    MessageBox.Show("Success!")
                Else
                    MessageBox.Show("Failure!")
                End If
            Else
                MessageBox.Show("Did not recognize management type.")
            End If
            LoadAPBManagement()
        End If
    End Sub

    Private Sub btnSaveAPBManagement_Click(sender As Object, e As EventArgs) Handles btnSaveAPBManagement.Click
        SaveAPBManagement()
    End Sub

    Private Sub txtApbManagementName_Enter(sender As Object, e As EventArgs) Handles txtApbManagementName.Enter
        Me.AcceptButton = btnSaveAPBManagement
    End Sub

    Private Sub txtApbManagementName_Leave(sender As Object, e As EventArgs) Handles txtApbManagementName.Leave
        Me.AcceptButton = Nothing
    End Sub

#End Region

End Class