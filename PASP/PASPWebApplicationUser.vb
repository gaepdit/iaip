
Imports Oracle.DataAccess.Client

Public Class PASPWebApplicationUser
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim dsWorkEntry As DataSet
    Dim daWorkEntry As OracleDataAdapter
    Dim airsno As String



    Private Sub DevWebApplicationUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            LoadFacilities()
            LoadEmails()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

        End Try
    End Sub
#Region "Page Load Functions"
    Sub LoadFacilities()
        Try


            FormatDataGridForWorkEnTry()

            Dim dtAIRS As New DataTable

            dtAIRS = LoadComboBoxes()

            With cboAirsNo
                .DataSource = dtAIRS
                .DisplayMember = "strairsnumber"
                .ValueMember = "strairsnumber"
                .SelectedIndex = 0
            End With

            With cboFacilityName
                .DataSource = dtAIRS
                .DisplayMember = "strfacilityname"
                .ValueMember = "strairsnumber"
                .SelectedIndex = 0
            End With
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try
    End Sub
    Sub LoadEmails()
        Try

            FormatDataGridForWorkEnTryEmail()
            LoadComboBoxesEmail()

            Dim dtAIRS As New DataTable

            dtAIRS = LoadComboBoxes()

            With cboFacilityToAdd
                .DataSource = dtAIRS
                .DisplayMember = "strairsnumber"
                .ValueMember = "strFacilityName"
                .SelectedIndex = 0
            End With
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try
    End Sub
#End Region
#Region "Web Application Users"
    Private Sub btnActivateTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivateTool.Click
        Try



            Dim dtAIRS As New DataTable

            dtAIRS = LoadComboBoxes()

            With cboAirsNo
                .DataSource = dtAIRS
                .DisplayMember = "strairsnumber"
                .ValueMember = "strairsnumber"
                .SelectedIndex = 0
            End With

            With cboFacilityName
                .DataSource = dtAIRS
                .DisplayMember = "strfacilityname"
                .ValueMember = "strairsnumber"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnActivateEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivateEmail.Click
        Try

            LoadComboBoxesEmail()
            Dim dtAIRS As New DataTable

            dtAIRS = LoadComboBoxes()

            With cboFacilityToAdd
                .DataSource = dtAIRS
                .DisplayMember = "strairsnumber"
                .ValueMember = "strFacilityName"
                .SelectedIndex = 0
            End With
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

            dtairs.Columns.Add("strairsnumber", GetType(System.String))
            dtairs.Columns.Add("strfacilityname", GetType(System.String))

            drNewRow = dtairs.NewRow()
            drNewRow("strfacilityname") = " "
            drNewRow("strairsnumber") = " "
            dtairs.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("facilityInfo").Rows()
                drNewRow = dtairs.NewRow()
                drNewRow("strairsnumber") = drDSRow("strairsnumber")
                drNewRow("strfacilityname") = drDSRow("strfacilityname")
                dtairs.Rows.Add(drNewRow)
            Next

            Return dtairs

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            Return Nothing
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
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

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

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
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub FormatDataGridForWorkEnTry()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            Dim objbooleancol As New DataGridBoolColumn

            'objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "tblFacilityUser"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True

            'Formatting our DataGrid 0
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ID"
            objtextcol.HeaderText = "ID"
            objtextcol.Width = 1
            objtextcol.ReadOnly = True
            objGrid.GridColumnStyles.Add(objtextcol)

            'Formatting our DataGrid 1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "Email"
            objtextcol.HeaderText = "Email Address"
            objtextcol.ReadOnly = True
            objtextcol.Width = 300
            objGrid.GridColumnStyles.Add(objtextcol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intAdminAccess"
            objbooleancol.HeaderText = "Admin Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intFeeAccess"
            objbooleancol.HeaderText = "Fee Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intEIAccess"
            objbooleancol.HeaderText = "EI Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intESAccess"
            objbooleancol.HeaderText = "ES Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            'Applying the above formating 
            'dgrUsers.TableStyles.Clear()
            dgrUsers.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrUsers.CaptionText = "Current Users for " & cboAirsNo.SelectedText
            dgrUsers.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub FormatDataGridForWorkEnTryEmail()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            Dim objbooleancol As New DataGridBoolColumn

            'objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "tblUserFacility"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True

            'Formatting our DataGrid 0
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strairsnumber"
            objtextcol.HeaderText = "Airs Number"
            objtextcol.Width = 110
            objtextcol.ReadOnly = True
            objGrid.GridColumnStyles.Add(objtextcol)

            'Formatting our DataGrid 1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strfacilityname"
            objtextcol.HeaderText = "Facility Name"
            objtextcol.ReadOnly = True
            objtextcol.Width = 220
            objGrid.GridColumnStyles.Add(objtextcol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intAdminAccess"
            objbooleancol.HeaderText = "Admin Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intFeeAccess"
            objbooleancol.HeaderText = "Fee Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intEIAccess"
            objbooleancol.HeaderText = "EI Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intESAccess"
            objbooleancol.HeaderText = "ES Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            'Applying the above formating 
            'dgrUsers.TableStyles.Clear()
            dgrFacilities.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrFacilities.CaptionText = "Current Facilities for " & cboUserEmail.SelectedText
            dgrFacilities.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Private Sub llbViewAll_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAll.LinkClicked
        Try
            airsno = cboAirsNo.Text
            pnlUser.Visible = True
            'FormatDataGridForWorkEnTry()
            LoadDataGrid()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try
    End Sub
    Private Sub lblViewFacility_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewFacility.LinkClicked
        Try
            pnlUserFacility.Visible = True
            pnlUserInfo.Visible = True
            LoadDataGridFacility()
            LoadUserInfo()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try
    End Sub
    Sub LoadDataGrid()
        Dim SQL As String = ""
        Try

            dsWorkEntry = New DataSet
            If Not dsWorkEntry.Tables("tblFacilityUser") Is Nothing Then
                dsWorkEntry.Tables("tblFacilityUser").Clear()
                dsWorkEntry.Tables.Remove("tblFacilityUser")
                dsWorkEntry.AcceptChanges()
            End If

            SQL = "SELECT " & DBNameSpace & ".OlapUserAccess.NumUserID as ID, " & DBNameSpace & ".OlapUserLogin.numuserid, " & _
                    "" & DBNameSpace & ".OlapUserLogin.strUserEmail as Email, " & _
                    "Case When intAdminAccess = 0 Then 'False' When intAdminAccess = 1 Then 'True' End as intAdminAccess, " & _
                    "Case When intFeeAccess = 0 Then 'False' When intFeeAccess = 1 Then 'True' End as intFeeAccess, " & _
                    "Case When intEIAccess = 0 Then 'False' When intEIAccess = 1 Then 'True' End as intEIAccess, " & _
                    "Case When intESAccess = 0 Then 'False' When intESAccess = 1 Then 'True' End as intESAccess " & _
                    "FROM " & DBNameSpace & ".OlapUserAccess, " & DBNameSpace & ".OlapUserLogin " & _
                    "WHERE " & DBNameSpace & ".OLAPUserAccess.NumUserId = " & DBNameSpace & ".OlapUserLogin.NumUserID " & _
                    "AND " & DBNameSpace & ".OlapUserAccess.strAirsNumber = '0413" & airsno & "' order by email"

            daWorkEntry = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daWorkEntry.Fill(dsWorkEntry, "tblFacilityUser")

            dgrUsers.DataSource = dsWorkEntry
            dgrUsers.DataMember = "tblFacilityUser"

            cboUsers.DataSource = dsWorkEntry.Tables("tblFacilityUser")
            cboUsers.DisplayMember = "Email"
            cboUsers.ValueMember = "ID"

            'USER_ this little section removes the Append Row from
            ' the end of the datagrid
            Dim cm As CurrencyManager = Me.BindingContext(Me.dgrUsers.DataSource, Me.dgrUsers.DataMember)

            Dim dv As DataView = cm.List
            dv.AllowNew = False
            'End remove append Row

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub LoadDataGridFacility()
        Dim SQL As String = ""
        Try

            dsWorkEntry = New DataSet
            If Not dsWorkEntry.Tables("tblUserFacility") Is Nothing Then
                dsWorkEntry.Tables("tblUserFacility").Clear()
                dsWorkEntry.Tables.Remove("tblUserFacility")
                dsWorkEntry.AcceptChanges()
            End If

            SQL = "SELECT substr(strairsnumber, 5) as strAIRSNumber, strfacilityname, " & _
                    "Case When intAdminAccess = 0 Then 'False' When intAdminAccess = 1 Then 'True' End as intAdminAccess, " & _
                    "Case When intFeeAccess = 0 Then 'False' When intFeeAccess = 1 Then 'True' End as intFeeAccess, " & _
                    "Case When intEIAccess = 0 Then 'False' When intEIAccess = 1 Then 'True' End as intEIAccess, " & _
                    "Case When intESAccess = 0 Then 'False' When intESAccess = 1 Then 'True' End as intESAccess " & _
                    "FROM " & DBNameSpace & ".OlapUserAccess " & _
                    "WHERE numuserid = '" & cboUserEmail.SelectedValue & "' order by strfacilityname"

            daWorkEntry = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daWorkEntry.Fill(dsWorkEntry, "tblUserFacility")

            dgrFacilities.DataSource = dsWorkEntry
            dgrFacilities.DataMember = "tblUserFacility"

            cboFacilityToDelete.DataSource = dsWorkEntry.Tables("tblUserFacility")
            cboFacilityToDelete.DisplayMember = "strfacilityname"
            cboFacilityToDelete.ValueMember = "strairsnumber"

            'USER_ this little section removes the Append Row from
            ' the end of the datagrid
            Dim cm As CurrencyManager = Me.BindingContext(Me.dgrFacilities.DataSource, Me.dgrFacilities.DataMember)

            Dim dv As DataView = cm.List
            dv.AllowNew = False
            'End remove append Row

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub LoadUserInfo()
        Try


            SQL = "Select " & _
            "strfirstname, strlastname, " & _
            "strtitle, strcompanyname, " & _
            "straddress, strcity, " & _
            "strstate, strzip, " & _
            "strphonenumber, strfaxnumber " & _
            "from " & DBNameSpace & ".OlapUserProfile " & _
            "where numuserid = '" & cboUserEmail.SelectedValue & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("strfirstname")) Then
                Else
                    lblFName.Text = "First Name: " & dr.Item("strfirstname")
                End If
                If IsDBNull(dr.Item("strlastname")) Then
                Else
                    lblLName.Text = "Last Name: " & dr.Item("strlastname")
                End If
                If IsDBNull(dr.Item("strtitle")) Then
                Else

                    lblTitle.Text = "Title: " & dr.Item("strtitle")
                End If
                If IsDBNull(dr.Item("strcompanyname")) Then
                Else
                    lblCoName.Text = "Company Name: " & dr.Item("strcompanyname")
                End If
                If IsDBNull(dr.Item("straddress")) Then
                Else
                    lblAddress.Text = dr.Item("straddress")
                End If
                If IsDBNull(dr.Item("strcity")) Then
                Else
                    lblCityStateZip.Text = dr.Item("strcity") & ", " & dr.Item("strstate") & " " & dr.Item("strzip")
                End If
                If IsDBNull(dr.Item("strphonenumber")) Then
                Else
                    lblPhoneNo.Text = "Phone Number: " & dr.Item("strphonenumber")
                End If
                If IsDBNull(dr.Item("strfaxnumber")) Then
                Else
                    lblFaxNo.Text = "Fax Number: " & dr.Item("strfaxnumber")
                End If

            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub Back()
        Try

            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            Dim dt As DataTable
            Dim userId As Integer
            Dim adminaccess As Boolean
            Dim feeaccess As Boolean
            Dim eiaccess As Boolean
            Dim esaccess As Boolean

            dt = dsWorkEntry.Tables("tblFacilityUser").GetChanges
            If dt Is Nothing Then
            Else
                Dim Row As DataRow
                Dim intColumn As Integer
                For Each Row In dt.Rows
                    Select Case Row.RowState
                        'Case DataRowState.Added
                        '    blnDataChanged = True
                        'Case DataRowState.Deleted
                        '    blnDataChanged = True
                        Case DataRowState.Modified
                            For intColumn = 3 To 6
                                If Not IsDBNull(Row(intColumn, DataRowVersion.Original)) And Not IsDBNull(Row(intColumn, DataRowVersion.Current)) Then
                                    If Row(intColumn, DataRowVersion.Original) <> Row(intColumn, DataRowVersion.Current) Then
                                        userId = Row(0, DataRowVersion.Original)
                                        adminaccess = Row(3, DataRowVersion.Current)
                                        feeaccess = Row(4, DataRowVersion.Current)
                                        eiaccess = Row(5, DataRowVersion.Current)
                                        esaccess = Row(6, DataRowVersion.Current)
                                        UpdateRecords(userId, adminaccess, feeaccess, eiaccess, esaccess)
                                        'Exit For
                                    End If
                                End If
                            Next

                    End Select
                Next
            End If
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")
            LoadDataGrid()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub UpdateRecords(ByVal userid As Object, ByVal adminaccess As Object, ByVal feeaccess As Object, ByVal eiaccess As Object, ByVal esaccess As Object)

        Dim admin, fee, ei, es As Integer
        If adminaccess = True Then
            admin = 1
        Else
            admin = 0
        End If
        If feeaccess = True Then
            fee = 1
        Else
            fee = 0
        End If
        If eiaccess = True Then
            ei = 1
        Else
            ei = 0
        End If
        If esaccess = True Then
            es = 1
        Else
            es = 0
        End If

        Try
            Dim updateString As String = "UPDATE " & DBNameSpace & ".OlapUserAccess " & _
                      "SET intadminaccess = '" & admin & "', " & _
                      "intFeeAccess = '" & fee & "', " & _
                      "intEIAccess = '" & ei & "', " & _
                      "intESAccess = '" & es & "' " & _
                      "WHERE numUserID = '" & userid & "' " & _
                      "and strAirsNumber = '0413" & airsno & "' "

            Dim cmd As New OracleCommand(updateString, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()
            If CurrentConnection.State = ConnectionState.Closed Then
            Else
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub UpdateRecordsEmail(ByVal airsnumber As Object, ByVal adminaccess As Object, ByVal feeaccess As Object, ByVal eiaccess As Object, ByVal esaccess As Object)

        Dim admin, fee, ei, es As Integer
        If adminaccess = True Then
            admin = 1
        Else
            admin = 0
        End If
        If feeaccess = True Then
            fee = 1
        Else
            fee = 0
        End If
        If eiaccess = True Then
            ei = 1
        Else
            ei = 0
        End If
        If esaccess = True Then
            es = 1
        Else
            es = 0
        End If

        Try
            Dim updateString As String = "UPDATE " & DBNameSpace & ".OlapUserAccess " & _
                      "SET intadminaccess = '" & admin & "', " & _
                      "intFeeAccess = '" & fee & "', " & _
                      "intEIAccess = '" & ei & "', " & _
                      "intESAccess = '" & es & "' " & _
                      "WHERE numUserID = '" & cboUserEmail.SelectedValue & "' " & _
                      "and strAirsNumber = '0413" & airsnumber & "' "

            Dim cmd As New OracleCommand(updateString, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()
            If CurrentConnection.State = ConnectionState.Closed Then
            Else
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnAddFacilitytoUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFacilitytoUser.Click
        Try

            Dim InsertString As String = "Insert into " & DBNameSpace & ".OlapUserAccess " & _
            "(numUserId, strAirsNumber, strFacilityName) values( " & _
            "'" & cboUserEmail.SelectedValue & "', '0413" & cboFacilityToAdd.Text & "', " & _
            "'" & Replace(cboFacilityToAdd.SelectedValue, "'", "''") & "') "

            Dim cmd As New OracleCommand(InsertString, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            dgrFacilities.Refresh()
            MsgBox("The facility has beed added to this user", MsgBoxStyle.Information, "Insert Success!")
            LoadDataGridFacility()

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteFacilityUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFacilityUser.Click
        Try
            Dim deleteString As String = "DELETE " & DBNameSpace & ".OlapUserAccess " & _
                                "WHERE numUserID = '" & cboUserEmail.SelectedValue & "' " & _
                                "and strAirsNumber = '0413" & cboFacilityToDelete.SelectedValue & "' "

            Dim cmd As New OracleCommand(deleteString, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()
            If CurrentConnection.State = ConnectionState.Closed Then
            Else
                'conn.close()
            End If
            MsgBox("The facility has been removed for this user", MsgBoxStyle.Information, "Facility Removed!")
            LoadDataGridFacility()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        Try
            Dim userID As Integer
            Dim sql As String = "Select numUserId from " & DBNameSpace & ".olapuserlogin " & _
            "where struseremail = '" & Replace(UCase(txtEmail.Text), "'", "''") & "' "
            Dim cmd As New OracleCommand
            Dim dr As OracleDataReader
            Dim recexist As Boolean

            cmd = New OracleCommand(sql, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recexist = dr.Read

            If recexist = True Then 'Email address is registered
                userID = dr.Item("numUserId")
                Dim InsertString As String = "Insert into " & DBNameSpace & ".OlapUserAccess " & _
                "(numUserId, strAirsNumber, strFacilityName) values( " & _
                "'" & userID & "', '0413" & airsno & "', '" & Replace(cboFacilityName.Text, "'", "''") & "') "

                Dim cmd1 As New OracleCommand(InsertString, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd1.ExecuteNonQuery()

                dgrUsers.Refresh()
                MsgBox("The User has beed added to this facility", MsgBoxStyle.Information, "Insert Success!")
                LoadDataGrid()
            Else 'email address not registered
                MsgBox("This Email Address is not registered", MsgBoxStyle.OkOnly, "Insert Failed!")
            End If

            If dr.IsClosed = False Then dr.Close()
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim deleteString As String = "DELETE " & DBNameSpace & ".OlapUserAccess " & _
                                "WHERE numUserID = '" & cboUsers.SelectedValue & "' " & _
                                "and strAirsNumber = '0413" & airsno & "' "

            Dim cmd As New OracleCommand(deleteString, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()
            If CurrentConnection.State = ConnectionState.Closed Then
            Else
                'conn.close()
            End If
            MsgBox("The User has been removed for this facility", MsgBoxStyle.Information, "User Removed!")
            LoadDataGrid()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnActivateUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivateUser.Click
        Try
            SQL = "Select strUserEmail " & _
            "from " & DBNameSpace & ".OlapUserLogIn " & _
            "where strUserEmail = '" & Replace(UCase(txtEmailAddress.Text), "'", "''") & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                Dim updateString As String = "UPDATE " & DBNameSpace & ".OlapUserLogin " & _
                          "SET strconfirm = to_char(sysdate, 'YYYY/MM/DD HH:MI:SS') " & _
                          "WHERE struseremail = '" & Replace(UCase(txtEmailAddress.Text), "'", "''") & "' "

                cmd = New OracleCommand(updateString, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                'cmd.ExecuteNonQuery()
                dr = cmd.ExecuteReader
                dr.Close()
                If CurrentConnection.State = ConnectionState.Closed Then
                Else
                    'conn.close()
                End If
                MsgBox("The account has been activated", MsgBoxStyle.Information, "Activated!")
            Else
                MsgBox("This user does not exist.", MsgBoxStyle.Exclamation, "Activate failed!")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnAddFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFacility.Click
        Try
            Dim insertString As String = "Insert into " & DBNameSpace & ".FSPayandSubmit " & _
                      "(intyear, strairsnumber, strpaymenttype, strofficialname, strofficialtitle, datesubmit) " & _
                      "values ('" & CInt(txtYear.Text) & "', '" & txtAirsNo.Text & "', 'N/A', 'N/A', 'N/A', '" & OracleDate & "')"

            Dim cmd As New OracleCommand(insertString, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()
            If CurrentConnection.State = ConnectionState.Closed Then
            Else
                'conn.close()
            End If
            MsgBox("The facility has been added", MsgBoxStyle.Information, "Facility Added!")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRemoveFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFacility.Click
        Try
            txtYear.Text = "2006"
            Dim SQL As String = "Delete from " & DBNameSpace & ".FSPayandSubmit " & _
                      "where intyear = '" & CInt(txtYear.Text) & "' and strairsnumber = '" & txtAirsNo.Text & "'"

            Dim cmd As New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()
            If CurrentConnection.State = ConnectionState.Closed Then
            Else
                'conn.close()
            End If

            SQL = "Delete from " & DBNameSpace & ".FSCalculations " & _
                      "where intyear = '" & CInt(txtYear.Text) & "' and strairsnumber = '" & txtAirsNo.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()
            If CurrentConnection.State = ConnectionState.Closed Then
            Else
                'conn.close()
            End If

            SQL = "Delete from " & DBNameSpace & ".FSConfirmation " & _
                      "where intyear = '" & CInt(txtYear.Text) & "' and strairsnumber = '" & txtAirsNo.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()
            If CurrentConnection.State = ConnectionState.Closed Then
            Else
                'conn.close()
            End If

            MsgBox("The facility has been removed", MsgBoxStyle.Information, "Facility Removed!")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateUser.Click
        Try
            Dim dt As DataTable
            Dim airsnumber As String
            Dim adminaccess As Boolean
            Dim feeaccess As Boolean
            Dim eiaccess As Boolean
            Dim esaccess As Boolean

            dt = dsWorkEntry.Tables("tblUserFacility").GetChanges
            If dt Is Nothing Then
            Else
                Dim Row As DataRow
                Dim intColumn As Integer
                For Each Row In dt.Rows
                    Select Case Row.RowState
                        'Case DataRowState.Added
                        '    blnDataChanged = True
                        'Case DataRowState.Deleted
                        '    blnDataChanged = True
                        Case DataRowState.Modified
                            For intColumn = 2 To 5
                                If Not IsDBNull(Row(intColumn, DataRowVersion.Original)) And Not IsDBNull(Row(intColumn, DataRowVersion.Current)) Then
                                    If Row(intColumn, DataRowVersion.Original) <> Row(intColumn, DataRowVersion.Current) Then
                                        airsnumber = Row(0, DataRowVersion.Original)
                                        adminaccess = Row(2, DataRowVersion.Current)
                                        feeaccess = Row(3, DataRowVersion.Current)
                                        eiaccess = Row(4, DataRowVersion.Current)
                                        esaccess = Row(5, DataRowVersion.Current)
                                        UpdateRecordsEmail(airsnumber, adminaccess, feeaccess, eiaccess, esaccess)
                                        'Exit For
                                    End If
                                End If
                            Next
                    End Select
                Next
            End If
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")
            LoadDataGrid()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region

    
End Class