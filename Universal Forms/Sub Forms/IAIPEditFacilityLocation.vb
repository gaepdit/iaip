Imports System.Data.OracleClient


Public Class IAIPEditFacilityLocation
    Dim SQL, SQL2 As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim dsFacilityInformation As DataSet
    Dim daFacilityInformation As OracleDataAdapter
    Dim daFacilityInformation2 As OracleDataAdapter

    Private Sub IAIPEditFacilityLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            Panel1.Text = "Select a Function..."
            Panel2.Text = UserName
            Panel3.Text = OracleDate

            If txtAirsNumber.Text <> "" Then
                LoadFacilityInformation()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "Page Load"
    Sub LoadFacilityInformation()
        Dim ModifingPerson As String
        Dim ModifingDate As String
        Dim ModifingLocation As String

        Try

            SQL = "Select * " & _
            "from " & DBNameSpace & ".VW_APBFacilityLocation " & _
            "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "

            SQL2 = "Select * " & _
            "from " & DBNameSpace & ".VW_HB_APBFacilityLocation " & _
            "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' " & _
            "Order by strKey DESC "

            dsFacilityInformation = New DataSet
            daFacilityInformation = New OracleDataAdapter(SQL, Conn)
            daFacilityInformation2 = New OracleDataAdapter(SQL2, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            If AccountArray(28, 2) = "1" Or AccountArray(28, 3) = "1" Or AccountArray(28, 4) = "1" Then
                'End If
                'If UserProgram = "5" Or (UserBranch = "1" And UserUnit = "---") _
                '  Or (UserProgram = "3" And AccountArray(68, 3) = "1") Then
                If txtFacilityName.Text <> "" Then
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
                        MsgBox("Since this is a direct change to the data, " & vbCrLf & _
                        "please make a unique comment. " & vbCrLf & _
                        "So future users know why the data was changed." & vbCrLf & _
                        "No data will be saved at this time.", _
                         MsgBoxStyle.Information, "Edit Facility Location Data")
                        Comments = "Error"
                    End If
                Else
                    MsgBox("Since this is a direct change to the data, " & vbCrLf & _
                    "please make a unique comment. " & vbCrLf & _
                    "So future users know why the data was changed." & vbCrLf & _
                    "No data will be saved at this time.", _
                     MsgBoxStyle.Information, "Edit Facility Location Data")
                    Comments = "Error"
                End If

                If ErrorCheck <> True Then
                    If Comments <> "Error" Then
                        If FacilityName <> "" Or Street1 <> "" Or _
                          Street2 <> "" Or City <> "" Or _
                          State <> "" Or ZipCode <> "" Or _
                          Longitude <> "" Or Latitude <> "" Or _
                          Comments <> "" Then

                            SQL = "Update " & DBNameSpace & ".APBFacilityInformation set "
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
                            SQL = SQL & "strModifingPerson = '" & UserGCode & "', " & _
                            "datModifingDate = '" & OracleDate & "', " & _
                            "strModifingLocation = '2' " & _
                            "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            Try

                                dr = cmd.ExecuteReader
                            Catch ex As Exception
                                MsgBox(ex.ToString())
                            End Try


                            dr.Close()

                            If FacilityName <> "" Then
                                SQL = "Update " & DBNameSpace & ".OLAPUserAccess set " & _
                                "strFacilityName = '" & Replace(FacilityName, "'", "''") & "' " & _
                                "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()
                            End If

                            If FacilitySummary Is Nothing Then
                            Else

                            End If

                            SQL = "Update airbranch.EIS_FacilitySite set " & _
                            "strFacilitySiteName = '" & Replace(txtFacilityName.Text, "'", "''") & "' " & _
                            "strFacilitySiteComment = 'Facility Name updated.', " & _
                            "UpdateUSer = '" & UserName & "', " & _
                            "updateDateTime = sysdate " & _
                            "where facilitySiteID = '" & txtAirsNumber.Text & "' "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            cmd.ExecuteReader()

                            LoadFacilityInformation()
                            MsgBox("Data Updated", MsgBoxStyle.Information, "Edit Facility Location Data")
                        Else
                            MsgBox("No data was changed", MsgBoxStyle.Information, "Edit Facility Location Data")
                        End If
                    End If
                Else
                    MsgBox("The data was not save due to bad data.", _
                            MsgBoxStyle.Information, "Edit Facility Location Data")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub Back()
        Try

            EditFacilityLocation = Nothing
            Me.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

#End Region
#Region "Declaration"
    Private Sub TBEditFacilityLocation_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBEditFacilityLocation.ButtonClick
        Try

            Select Case TBEditFacilityLocation.Buttons.IndexOf(e.Button)
                Case 0
                    Save()
                Case 1
                    Back()
            End Select
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub APBEditFacilityLocation_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try


            EditFacilityLocation = Nothing

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgvFaciltiyInformaitonHistory_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvFaciltiyInformaitonHistory.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvFaciltiyInformaitonHistory.HitTest(e.X, e.Y)

        Try


            If dgvFaciltiyInformaitonHistory.RowCount > 0 And hti.RowIndex <> -1 Then
                txtKey.Text = dgvFaciltiyInformaitonHistory(1, hti.RowIndex).Value
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtKey_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKey.TextChanged
        Try
            Dim ModifingPerson As String
            Dim ModifingDate As String
            Dim ModifingLocation As String

            If txtKey.Text <> "" Then
                SQL = "Select * " & _
                "from " & DBNameSpace & ".VW_HB_APBFacilityLocation " & _
                "where strKey = '" & txtKey.Text & "' " & _
                "Order by strKey DESC "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub llbCurrentData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbCurrentData.LinkClicked
        Try
            txtKey.Text = ""
            LoadFacilityInformation()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "Main Menu"
    Private Sub mmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSave.Click
        Try

            Save()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiBack.Click
        Try

            Back()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("(^X)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCopy.Click
        Try

            SendKeys.Send("(^C)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPaste.Click
        Try

            SendKeys.Send("(^V)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try

            Help.ShowHelp(Label1, HELP_URL)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

#End Region

    Private Sub txtAirsNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAirsNumber.TextChanged
        Try
            If txtAirsNumber.Text <> "" Then
                LoadFacilityInformation()

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
End Class