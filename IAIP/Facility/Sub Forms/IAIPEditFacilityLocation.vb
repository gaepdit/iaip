Imports Oracle.ManagedDataAccess.Client


Public Class IAIPEditFacilityLocation
    Dim SQL, SQL2 As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim dsFacilityInformation As DataSet
    Dim daFacilityInformation As OracleDataAdapter
    Dim daFacilityInformation2 As OracleDataAdapter

    Private Sub IAIPEditFacilityLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            If Apb.ApbFacilityId.IsValidAirsNumberFormat(txtAirsNumber.Text) Then
                LoadFacilityInformation()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Page Load"
    Sub LoadFacilityInformation()
        Dim ModifingPerson As String
        Dim ModifingDate As String
        Dim ModifingLocation As String

        Try

            SQL = "Select * " &
            "from AIRBRANCH.VW_APBFacilityLocation " &
            "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "

            SQL2 = "Select * " &
            "from AIRBRANCH.VW_HB_APBFacilityLocation " &
            "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' " &
            "Order by strKey DESC "

            dsFacilityInformation = New DataSet
            daFacilityInformation = New OracleDataAdapter(SQL, CurrentConnection)
            daFacilityInformation2 = New OracleDataAdapter(SQL2, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daFacilityInformation.Fill(dsFacilityInformation, "Current")
            daFacilityInformation2.Fill(dsFacilityInformation, "Historical")

            txtFacilityName.Text = dsFacilityInformation.Tables("Current").Rows(0).Item(1).ToString()
            txtStreetAddress.Text = dsFacilityInformation.Tables("Current").Rows(0).Item(2).ToString()
            txtStreetAddress2.Text = dsFacilityInformation.Tables("Current").Rows(0).Item(3).ToString()
            txtFacilityCity.Text = dsFacilityInformation.Tables("Current").Rows(0).Item(4).ToString()
            txtFacilityState.Text = dsFacilityInformation.Tables("Current").Rows(0).Item(5).ToString()
            mtbFacilityZipCode.Text = dsFacilityInformation.Tables("Current").Rows(0).Item(6).ToString

            If IsDBNull(dsFacilityInformation.Tables("Current").Rows(0).Item(8).ToString()) Then
                txtFacilityLatitude.Text = "00.0000"
            Else
                txtFacilityLatitude.Text = dsFacilityInformation.Tables("Current").Rows(0).Item(8).ToString()
            End If
            If IsDBNull(dsFacilityInformation.Tables("Current").Rows(0).Item(7).ToString()) Then
                txtFacilityLongitude.Text = "00.0000"
            Else
                txtFacilityLongitude.Text = dsFacilityInformation.Tables("Current").Rows(0).Item(7).ToString()
            End If

            If IsDBNull(dsFacilityInformation.Tables("Current").Rows(0).Item(12).ToString()) Then
                ModifingPerson = "Unknown"
            Else
                ModifingPerson = dsFacilityInformation.Tables("Current").Rows(0).Item(12).ToString()
            End If
            If IsDBNull(dsFacilityInformation.Tables("Current").Rows(0).Item(13).ToString()) Then
                ModifingDate = "Unknown Date"
            Else
                ModifingDate = dsFacilityInformation.Tables("Current").Rows(0).Item(13).ToString()
            End If
            If IsDBNull(dsFacilityInformation.Tables("Current").Rows(0).Item(15).ToString()) Then
                ModifingLocation = "Unknown Location"
            Else
                ModifingLocation = dsFacilityInformation.Tables("Current").Rows(0).Item(15).ToString()
            End If
            txtModifingComments.Text = "Modified on " & ModifingDate & " by " & ModifingPerson & " from " & ModifingLocation
            txtComments.Text = dsFacilityInformation.Tables("Current").Rows(0).Item(14).ToString()

            dgvFaciltiyInformaitonHistory.DataSource = dsFacilityInformation
            dgvFaciltiyInformaitonHistory.DataMember = "Historical"
            dgvFaciltiyInformaitonHistory.RowHeadersVisible = False
            dgvFaciltiyInformaitonHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFaciltiyInformaitonHistory.AllowUserToResizeColumns = True
            dgvFaciltiyInformaitonHistory.AllowUserToAddRows = False
            dgvFaciltiyInformaitonHistory.AllowUserToDeleteRows = False
            dgvFaciltiyInformaitonHistory.AllowUserToOrderColumns = True
            dgvFaciltiyInformaitonHistory.AllowUserToResizeRows = True
            dgvFaciltiyInformaitonHistory.Columns("strKey").HeaderText = "Key"
            dgvFaciltiyInformaitonHistory.Columns("strKey").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            dgvFaciltiyInformaitonHistory.Columns("strKey").DisplayIndex = 0
            dgvFaciltiyInformaitonHistory.Columns("strKey").Visible = False
            dgvFaciltiyInformaitonHistory.Columns("ModifingDate").HeaderText = "Date Modified"
            dgvFaciltiyInformaitonHistory.Columns("ModifingDate").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvFaciltiyInformaitonHistory.Columns("ModifingDate").DisplayIndex = 1
            dgvFaciltiyInformaitonHistory.Columns("UserName").HeaderText = "Modifing Person"
            dgvFaciltiyInformaitonHistory.Columns("UserName").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvFaciltiyInformaitonHistory.Columns("UserName").DisplayIndex = 2
            dgvFaciltiyInformaitonHistory.Columns("strModifingLocation").HeaderText = "Modifing Location"
            dgvFaciltiyInformaitonHistory.Columns("strModifingLocation").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvFaciltiyInformaitonHistory.Columns("strModifingLocation").DisplayIndex = 3
            dgvFaciltiyInformaitonHistory.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFaciltiyInformaitonHistory.Columns("strFacilityName").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvFaciltiyInformaitonHistory.Columns("strFacilityName").DisplayIndex = 4
            dgvFaciltiyInformaitonHistory.Columns("strFacilityStreet1").HeaderText = "Facility Address"
            dgvFaciltiyInformaitonHistory.Columns("strFacilityStreet1").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvFaciltiyInformaitonHistory.Columns("strFacilityStreet1").DisplayIndex = 5
            dgvFaciltiyInformaitonHistory.Columns("strFacilityStreet2").HeaderText = "Facility Address 2"
            dgvFaciltiyInformaitonHistory.Columns("strFacilityStreet2").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvFaciltiyInformaitonHistory.Columns("strFacilityStreet2").DisplayIndex = 6
            dgvFaciltiyInformaitonHistory.Columns("strFacilityCity").HeaderText = "City"
            dgvFaciltiyInformaitonHistory.Columns("strFacilityCity").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvFaciltiyInformaitonHistory.Columns("strFacilityCity").DisplayIndex = 7
            dgvFaciltiyInformaitonHistory.Columns("strFacilityState").HeaderText = "State"
            dgvFaciltiyInformaitonHistory.Columns("strFacilityState").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvFaciltiyInformaitonHistory.Columns("strFacilityState").DisplayIndex = 8
            dgvFaciltiyInformaitonHistory.Columns("strFacilityZipCode").HeaderText = "Zip Code"
            dgvFaciltiyInformaitonHistory.Columns("strFacilityZipCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvFaciltiyInformaitonHistory.Columns("strFacilityZipCode").DisplayIndex = 9
            dgvFaciltiyInformaitonHistory.Columns("numFacilityLongitude").HeaderText = "Longitude"
            dgvFaciltiyInformaitonHistory.Columns("numFacilityLongitude").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvFaciltiyInformaitonHistory.Columns("numFacilityLongitude").DisplayIndex = 10
            dgvFaciltiyInformaitonHistory.Columns("numFacilityLatitude").HeaderText = "Latitude"
            dgvFaciltiyInformaitonHistory.Columns("numFacilityLatitude").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvFaciltiyInformaitonHistory.Columns("numFacilityLatitude").DisplayIndex = 11
            dgvFaciltiyInformaitonHistory.Columns("strComments").HeaderText = "Comment(s)"
            dgvFaciltiyInformaitonHistory.Columns("strComments").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvFaciltiyInformaitonHistory.Columns("strComments").DisplayIndex = 12
            dgvFaciltiyInformaitonHistory.Columns("strAIRSNumber").HeaderText = "AIRS Number"
            dgvFaciltiyInformaitonHistory.Columns("strAIRSNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvFaciltiyInformaitonHistory.Columns("strAIRSNumber").DisplayIndex = 13
            dgvFaciltiyInformaitonHistory.Columns("strAIRSNumber").Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

#Region "Subs and Functions"
    Sub Save()
        Dim ErrorCheck As Boolean = False
        Dim FacilityName As String = ""
        Dim Street1 As String = ""
        Dim Street2 As String = ""
        Dim City As String = ""
        Dim State As String = ""
        Dim ZipCode As String = ""
        Dim Longitude As String = ""
        Dim Latitude As String = ""
        Dim Comments As String = ""

        Try

            txtFacilityName.BackColor = Color.White
            txtStreetAddress.BackColor = Color.White
            txtStreetAddress2.BackColor = Color.White
            txtFacilityCity.BackColor = Color.White
            txtFacilityState.BackColor = Color.White
            mtbFacilityZipCode.BackColor = Color.White
            txtFacilityLongitude.BackColor = Color.White
            txtFacilityLatitude.BackColor = Color.White
            txtComments.BackColor = Color.White

            If AccountFormAccess(28, 2) = "1" Or AccountFormAccess(28, 3) = "1" Or AccountFormAccess(28, 4) = "1" Then
                'End If
                'If UserProgram = "5" Or (UserBranch = "1" And UserUnit = "---") _
                '  Or (UserProgram = "3" And AccountArray(68, 3) = "1") Then
                If txtFacilityName.Text <> "" Then
                    txtFacilityName.Text = Apb.Facilities.Facility.SanitizeFacilityNameForDb(txtFacilityName.Text)
                    If txtFacilityName.Text <> dsFacilityInformation.Tables("Current").Rows(0).Item(1).ToString() Then
                        FacilityName = Replace(txtFacilityName.Text, "'", "''")
                    Else
                        FacilityName = ""
                    End If
                Else
                    ErrorCheck = True
                    txtFacilityName.BackColor = Color.Yellow
                End If
                If txtStreetAddress.Text <> "" Then
                    If txtStreetAddress.Text <> dsFacilityInformation.Tables("Current").Rows(0).Item(2).ToString() Then
                        Street1 = Replace(txtStreetAddress.Text, "'", "''")
                    Else
                        Street1 = ""
                    End If
                Else
                    ErrorCheck = True
                    txtStreetAddress.BackColor = Color.Yellow
                End If
                If txtStreetAddress2.Text <> "" Then
                    If txtStreetAddress2.Text <> dsFacilityInformation.Tables("Current").Rows(0).Item(3).ToString() Then
                        Street2 = Replace(txtStreetAddress2.Text, "'", "''")
                    Else
                        Street2 = ""
                    End If
                Else
                    ErrorCheck = True
                    txtStreetAddress2.BackColor = Color.Yellow
                End If
                If txtFacilityCity.Text <> "" Then
                    If txtFacilityCity.Text <> dsFacilityInformation.Tables("Current").Rows(0).Item(4).ToString() Then
                        City = Replace(txtFacilityCity.Text, "'", "''")
                    Else
                        City = ""
                    End If
                Else
                    ErrorCheck = True
                    txtFacilityCity.BackColor = Color.Yellow
                End If
                If txtFacilityState.Text <> "" Then
                    If txtFacilityState.Text <> dsFacilityInformation.Tables("Current").Rows(0).Item(5).ToString() Then
                        State = Replace(txtFacilityState.Text, "'", "''")
                    Else
                        State = ""
                    End If
                Else
                    ErrorCheck = True
                    txtFacilityState.BackColor = Color.Yellow
                End If
                If mtbFacilityZipCode.Text <> "" Then
                    If Replace(mtbFacilityZipCode.Text, "-", "") <> Replace(dsFacilityInformation.Tables("Current").Rows(0).Item(6).ToString(), "-", "") Then
                        ZipCode = Replace(mtbFacilityZipCode.Text, "-", "")
                    Else
                        ZipCode = ""
                    End If
                Else
                    ErrorCheck = True
                    mtbFacilityZipCode.BackColor = Color.Yellow
                End If

                If txtFacilityLongitude.Text <> "" Then
                    If txtFacilityLongitude.Text <> dsFacilityInformation.Tables("Current").Rows(0).Item(7).ToString() Then
                        If IsNumeric(txtFacilityLongitude.Text) Then
                            Longitude = txtFacilityLongitude.Text
                        Else
                            Longitude = "00.0000"
                        End If
                    Else
                        Longitude = ""
                    End If
                Else
                    Longitude = ""
                End If
                If txtFacilityLatitude.Text <> "" Then
                    If txtFacilityLatitude.Text <> dsFacilityInformation.Tables("Current").Rows(0).Item(8).ToString() Then
                        If IsNumeric(txtFacilityLatitude.Text) Then
                            Latitude = txtFacilityLatitude.Text
                        Else
                            Latitude = "00.0000"
                        End If
                    Else
                        Latitude = ""
                    End If
                Else
                    Latitude = ""
                End If
                If txtComments.Text <> "" Then
                    If txtComments.Text <> dsFacilityInformation.Tables("Current").Rows(0).Item(14).ToString() Then
                        Comments = Replace(txtComments.Text, "'", "''")
                    Else
                        MsgBox("Since this is a direct change to the data, " & vbCrLf &
                        "please make a unique comment. " & vbCrLf &
                        "So future users know why the data was changed." & vbCrLf &
                        "No data will be saved at this time.",
                         MsgBoxStyle.Information, "Edit Facility Location Data")
                        Comments = "Error"
                    End If
                Else
                    MsgBox("Since this is a direct change to the data, " & vbCrLf &
                    "please make a unique comment. " & vbCrLf &
                    "So future users know why the data was changed." & vbCrLf &
                    "No data will be saved at this time.",
                     MsgBoxStyle.Information, "Edit Facility Location Data")
                    Comments = "Error"
                End If

                If ErrorCheck <> True Then
                    If Comments <> "Error" Then
                        If FacilityName <> "" Or Street1 <> "" Or
                          Street2 <> "" Or City <> "" Or
                          State <> "" Or ZipCode <> "" Or
                          Longitude <> "" Or Latitude <> "" Or
                          Comments <> "" Then

                            SQL = "Update AIRBRANCH.APBFacilityInformation set "
                            If FacilityName <> "" Then
                                SQL = SQL & "strFacilityName = '" & FacilityName & "', "
                            End If
                            If Street1 <> "" Then
                                SQL = SQL & "strFacilityStreet1 = '" & Street1 & "', "
                            End If
                            If Street2 <> "" Then
                                SQL = SQL & "strFacilityStreet2 = '" & Street2 & "', "
                            End If
                            If City <> "" Then
                                SQL = SQL & "strFacilityCity = '" & City & "', "
                            End If
                            If State <> "" Then
                                SQL = SQL & "strFacilityState = '" & State & "', "
                            End If
                            If ZipCode <> "" Then
                                SQL = SQL & "strFacilityZipCode = '" & ZipCode & "', "
                            End If
                            If Longitude <> "" Then
                                SQL = SQL & "numFacilityLongitude = '" & Longitude & "', "
                            End If
                            If Latitude <> "" Then
                                SQL = SQL & "numFacilityLatitude = '" & Latitude & "', "
                            End If
                            If Comments <> "" Then
                                SQL = SQL & "strComments = '" & Comments & "', "
                            End If
                            SQL = SQL & "strModifingPerson = '" & CurrentUser.UserID & "', " &
                            "datModifingDate = '" & OracleDate & "', " &
                            "strModifingLocation = '2' " &
                            "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            Try

                                dr = cmd.ExecuteReader
                            Catch ex As Exception
                                MsgBox(ex.ToString())
                            End Try


                            dr.Close()

                            If FacilityName <> "" Then
                                SQL = "Update AIRBRANCH.OLAPUserAccess set " &
                                "strFacilityName = '" & FacilityName & "' " &
                                "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "

                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()

                                SQL = "Update airbranch.EIS_FacilitySite set " &
                                "strFacilitySiteName = '" & FacilityName & "', " &
                                "strFacilitySiteComment = 'Facility Name updated.', " &
                                "UpdateUSer = '" & CurrentUser.AlphaName & "', " &
                                "updateDateTime = sysdate " &
                                "where facilitySiteID = '" & txtAirsNumber.Text & "' "

                                cmd = New OracleCommand(SQL, CurrentConnection)
                                If CurrentConnection.State = ConnectionState.Closed Then
                                    CurrentConnection.Open()
                                End If
                                cmd.ExecuteReader()
                            End If

                            LoadFacilityInformation()
                            MsgBox("Data Updated", MsgBoxStyle.Information, "Edit Facility Location Data")
                        Else
                            MsgBox("No data was changed", MsgBoxStyle.Information, "Edit Facility Location Data")
                        End If
                    End If
                Else
                    MsgBox("The data was not save due to bad data.",
                            MsgBoxStyle.Information, "Edit Facility Location Data")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

#End Region
#Region "Declaration"
    Private Sub dgvFaciltiyInformaitonHistory_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvFaciltiyInformaitonHistory.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvFaciltiyInformaitonHistory.HitTest(e.X, e.Y)

        Try


            If dgvFaciltiyInformaitonHistory.RowCount > 0 And hti.RowIndex <> -1 Then
                txtKey.Text = dgvFaciltiyInformaitonHistory(1, hti.RowIndex).Value
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtKey_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKey.TextChanged
        Try
            Dim ModifingPerson As String
            Dim ModifingDate As String
            Dim ModifingLocation As String

            If txtKey.Text <> "" Then
                SQL = "Select * " &
                "from AIRBRANCH.VW_HB_APBFacilityLocation " &
                "where strKey = '" & txtKey.Text & "' " &
                "Order by strKey DESC "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        txtFacilityName.Text = ""
                    Else
                        txtFacilityName.Text = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        txtStreetAddress.Text = ""
                    Else
                        txtStreetAddress.Text = dr.Item("strFacilityStreet1")
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet2")) Then
                        txtStreetAddress2.Text = ""
                    Else
                        txtStreetAddress2.Text = dr.Item("strFacilityStreet2")
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        txtFacilityCity.Text = ""
                    Else
                        txtFacilityCity.Text = dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strFacilityState")) Then
                        txtFacilityState.Text = "GA"
                    Else
                        txtFacilityState.Text = dr.Item("strFacilityState")
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        mtbFacilityZipCode.Text = ""
                    Else
                        mtbFacilityZipCode.Text = dr.Item("strFacilityZipCode")
                    End If
                    If IsDBNull(dr.Item("numFacilityLongitude")) Then
                        txtFacilityLongitude.Text = ""
                    Else
                        txtFacilityLongitude.Text = dr.Item("numFacilityLongitude")
                    End If
                    If IsDBNull(dr.Item("numFacilityLatitude")) Then
                        txtFacilityLatitude.Text = ""
                    Else
                        txtFacilityLatitude.Text = dr.Item("numFacilityLatitude")
                    End If

                    If IsDBNull(dr.Item("UserName")) Then
                        ModifingPerson = "Unknown"
                    Else
                        ModifingPerson = dr.Item("USERName")
                    End If
                    If IsDBNull(dr.Item("ModifingDate")) Then
                        ModifingDate = "Unknown Date"
                    Else
                        ModifingDate = dr.Item("ModifingDate")
                    End If
                    If IsDBNull(dr.Item("strModifingLocation")) Then
                        ModifingLocation = "Unknown Location"
                    Else
                        ModifingLocation = dr.Item("strModifingLocation")
                    End If
                    txtModifingComments.Text = "Modified on " & ModifingDate & " by " & ModifingPerson & " from " & ModifingLocation

                    If IsDBNull(dr.Item("strComments")) Then
                        txtComments.Text = ""
                    Else
                        txtComments.Text = dr.Item("strComments")
                    End If
                End While

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub llbCurrentData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbCurrentData.LinkClicked
        Try
            txtKey.Text = ""
            LoadFacilityInformation()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "Main Menu and Toolbar"

    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        Save()
    End Sub

#End Region

#End Region

    Private Sub txtAirsNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAirsNumber.TextChanged
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(txtAirsNumber.Text) Then
            LoadFacilityInformation()
        End If
    End Sub

End Class