Imports System.Data.SqlClient

Public Class IAIPLookUpTables
    Dim SQL As String
    Dim ds As DataSet
    Dim da As SqlDataAdapter

#Region "ApplicationTypes"

    Sub LoadApplicationTypes()
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

#Region "APBManagement"

    Private Sub btnLoadAPBManagement_Click(sender As Object, e As EventArgs) Handles btnLoadAPBManagement.Click
        Try
            '1 - EPD Director
            '2 - EPD Commissioner
            '3 - APB Branch Chief
            '4 - PMII AMP
            '5 - PMII ISMP
            '6 - PMII MASP
            '7 - PMII PASP
            '8 - PMII SSCP
            '9 - PMII SSPP
            '10 - PMI AMP Data Analysis
            '11 - PMI AMP Operations
            '12 - PMI AMP Meterorology
            '13 - PMI AMP QA
            '14 - PMI AMP Operations 2
            '15 - PMI ISMP Chemical & VOC
            '16 - PMI ISMP Combustion & Mineral
            '17 - PMI ISMP Data Management
            '18 - PMI MASP Engines & Fuels
            '19 - PMI MASP I/M
            '20 - PMI MASP Enforucement 
            '21 - PMI MASP Public Affairs
            '22 - PMI MASP PIRT
            '23 - PMI PASP Admin & Financial 
            '24 - PMI PASP Data & Modeling
            '25 - PMI PASP Planning & Reg.
            '26 - PMI SSCP Air Toxics 
            '27 - PMI SSCP Chemcials/Minerals
            '28 - PMI SSCP VOC/Combustion
            '29 - PMI SSPP Chemcial 
            '30 - PMI SSPP Combustion
            '31 - PMI SSPP Minerals
            '32 - PMI SSPP NOx
            '33 - PMI SSPP VOC
            pnlAPBManagement.Visible = True
            dgvLookUpManagement.Visible = True
            LoadMangementCombo()
            LoadAPBManagement()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadMangementCombo()
        Try
            'cboManagementType
            cboManagementType.Items.Add("EPD Director")
            cboManagementType.Items.Add("EPD Commissioner")
            cboManagementType.Items.Add("APB Branch Chief")
            cboManagementType.Items.Add("PMII AMP")
            cboManagementType.Items.Add("PMII ISMP")
            cboManagementType.Items.Add("PMII MASP")
            cboManagementType.Items.Add("PMII PASP")
            cboManagementType.Items.Add("PMII SSCP")
            cboManagementType.Items.Add("PMII SSPP")
            cboManagementType.Items.Add("PMI AMP Data Analysis")
            cboManagementType.Items.Add("PMI AMP Operations")
            cboManagementType.Items.Add("PMI AMP Meterorology")
            cboManagementType.Items.Add("PMI AMP QA")
            cboManagementType.Items.Add("PMI AMP Operations 2")
            cboManagementType.Items.Add("PMI ISMP Chemical & VOC")
            cboManagementType.Items.Add("PMI ISMP Combustion & Mineral")
            cboManagementType.Items.Add("PMI ISMP Data Management")
            cboManagementType.Items.Add("PMI MASP Engines & Fuels")
            cboManagementType.Items.Add("PMI MASP I/M")
            cboManagementType.Items.Add("PMI MASP Enforucement")
            cboManagementType.Items.Add("PMI MASP Public Affairs")
            cboManagementType.Items.Add("PMI MASP PIRT")
            cboManagementType.Items.Add("PMI PASP Admin & Financial")
            cboManagementType.Items.Add("PMI PASP Data & Modeling")
            cboManagementType.Items.Add("PMI PASP Planning & Reg.")
            cboManagementType.Items.Add("PMI SSCP Air Toxics")
            cboManagementType.Items.Add("PMI SSCP Chemcials/Minerals")
            cboManagementType.Items.Add("PMI SSCP VOC/Combustion")
            cboManagementType.Items.Add("PMI SSPP Chemcial")
            cboManagementType.Items.Add("PMI SSPP Combustion")
            cboManagementType.Items.Add("PMI SSPP Minerals")
            cboManagementType.Items.Add("PMI SSPP NOx")
            cboManagementType.Items.Add("PMI SSPP VOC")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadAPBManagement()
        Try
            '1 - EPD Director
            '2 - EPD Commissioner
            '3 - APB Branch Chief
            '4 - PMII AMP
            '5 - PMII ISMP
            '6 - PMII MASP
            '7 - PMII PASP
            '8 - PMII SSCP
            '9 - PMII SSPP
            '10 - PMI AMP Data Analysis
            '11 - PMI AMP Operations
            '12 - PMI AMP Meterorology
            '13 - PMI AMP QA
            '14 - PMI AMP Operations 2
            '15 - PMI ISMP Chemical & VOC
            '16 - PMI ISMP Combustion & Mineral
            '17 - PMI ISMP Data Management
            '18 - PMI MASP Engines & Fuels
            '19 - PMI MASP I/M
            '20 - PMI MASP Enforucement 
            '21 - PMI MASP Public Affairs
            '22 - PMI MASP PIRT
            '23 - PMI PASP Admin & Financial 
            '24 - PMI PASP Data & Modeling
            '25 - PMI PASP Planning & Reg.
            '26 - PMI SSCP Air Toxics 
            '27 - PMI SSCP Chemcials/Minerals
            '28 - PMI SSCP VOC/Combustion
            '29 - PMI SSPP Chemcial 
            '30 - PMI SSPP Combustion
            '31 - PMI SSPP Minerals
            '32 - PMI SSPP NOx
            '33 - PMI SSPP VOC

            SQL = "Select " &
            "numId, strKey, " &
            "case " &
            "when strKey = '1' then 'EPD Director' " &
            "when strKey = '2' then 'EPD Commissioner' " &
            "when strKey = '3' then 'APB Branch Chief' " &
            "when strKey = '4' then 'PMII AMP' " &
            "when strKey = '5' then 'PMII ISMP' " &
            "when strKey = '6' then 'PMII MASP' " &
            "when strKey = '7' then 'PMII PASP' " &
            "when strKey = '8' then 'PMII SSCP' " &
            "when strKey = '9' then 'PMII SSPP' " &
            "when strKey = '10' then 'PMI AMP Data Analysis' " &
            "when strKey = '11' then 'PMI AMP Operations' " &
            "when strKey = '12' then 'PMI AMP Meterorology' " &
            "when strKey = '13' then 'PMI AMP QA' " &
            "when strKey = '14' then 'PMI AMP Operations 2' " &
            "when strKey = '15' then 'PMI ISMP Chemical & VOC' " &
            "when strKey = '16' then 'PMI ISMP Combustion & Mineral' " &
            "when strKey = '17' then 'PMI ISMP Data Management' " &
            "when strKey = '18' then 'PMI MASP Engines & Fuels' " &
            "when strKey = '19' then 'PMI MASP I/M' " &
            "when strKey = '20' then 'PMI MASP Enforucement' " &
            "when strKey = '21' then 'PMI MASP Public Affairs' " &
            "when strKey = '22' then 'PMI MASP PIRT' " &
            "when strKey = '23' then 'PMI PASP Admin & Financial' " &
            "when strKey = '24' then 'PMI PASP Data & Modeling' " &
            "when strKey = '25' then 'PMI PASP Planning & Reg.' " &
            "when strKey = '26' then 'PMI SSCP Air Toxics' " &
            "when strKey = '27' then 'PMI SSCP Chemcials/Minerals' " &
            "when strKey = '28' then 'PMI SSCP VOC/Combustion' " &
            "when strKey = '29' then 'PMI SSPP Chemcial' " &
            "when strKey = '30' then 'PMI SSPP Combustion' " &
            "when strKey = '31' then 'PMI SSPP Minerals' " &
            "when strKey = '32' then 'PMI SSPP NOx' " &
            "when strKey = '33' then 'PMI SSPP VOC' " &
            "else strKey " &
            "end MangerType, " &
            "strManagementName, datStartDate, " &
            "datEndDate, strCurrentContact " &
            "from LookUpAPBManagementType " &
            "where strCurrentContact = '1' " &
            "order by CONVERT(int, strKey) "

            ds = New DataSet
            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "APBManagement")
            dgvLookUpManagement.DataSource = ds
            dgvLookUpManagement.DataMember = "APBManagement"

            dgvLookUpManagement.RowHeadersVisible = False
            dgvLookUpManagement.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvLookUpManagement.AllowUserToResizeColumns = True
            dgvLookUpManagement.AllowUserToAddRows = False
            dgvLookUpManagement.AllowUserToDeleteRows = False
            dgvLookUpManagement.AllowUserToOrderColumns = True
            dgvLookUpManagement.AllowUserToResizeRows = True

            dgvLookUpManagement.Columns("numId").HeaderText = "ID"
            dgvLookUpManagement.Columns("numId").DisplayIndex = 0
            dgvLookUpManagement.Columns("numId").Width = dgvApplicationType.Width * 0.05
            dgvLookUpManagement.Columns("strKey").HeaderText = "Key"
            dgvLookUpManagement.Columns("strKey").DisplayIndex = 1
            dgvLookUpManagement.Columns("strKey").Visible = False
            dgvLookUpManagement.Columns("MangerType").HeaderText = "Manager Type"
            dgvLookUpManagement.Columns("MangerType").DisplayIndex = 2
            dgvLookUpManagement.Columns("MangerType").Width = dgvApplicationType.Width * 0.25
            dgvLookUpManagement.Columns("strManagementName").HeaderText = "Management Name"
            dgvLookUpManagement.Columns("strManagementName").DisplayIndex = 3
            dgvLookUpManagement.Columns("strManagementName").Width = dgvApplicationType.Width * 0.4
            dgvLookUpManagement.Columns("datStartDate").HeaderText = "Start Date"
            dgvLookUpManagement.Columns("datStartDate").DisplayIndex = 4
            dgvLookUpManagement.Columns("datStartDate").Width = dgvApplicationType.Width * 0.15
            dgvLookUpManagement.Columns("datStartDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvLookUpManagement.Columns("datEndDate").HeaderText = "End Date"
            dgvLookUpManagement.Columns("datEndDate").DisplayIndex = 5
            dgvLookUpManagement.Columns("datEndDate").Width = dgvApplicationType.Width * 0.15
            dgvLookUpManagement.Columns("datEndDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvLookUpManagement.Columns("strCurrentContact").HeaderText = "Current Contact"
            dgvLookUpManagement.Columns("strCurrentContact").DisplayIndex = 5
            dgvLookUpManagement.Columns("strCurrentContact").Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvLookUpManagement_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvLookUpManagement.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvLookUpManagement.HitTest(e.X, e.Y)

            If dgvLookUpManagement.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvLookUpManagement.Columns(0).HeaderText = "ID" Then
                    txtAPBManagemetnID.Text = dgvLookUpManagement(0, hti.RowIndex).Value
                    LoadManagementID()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadManagementID()
        Try
            Dim Key As String = ""

            If txtAPBManagemetnID.Text <> "" Then
                SQL = "Select " &
                "strKey, " &
                "strManagementName, datStartDate, " &
                "datEndDate, strCurrentContact " &
                "from LookUpAPBManagementType " &
                "where numId = '" & txtAPBManagemetnID.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strManagementName")) Then
                        txtAPBManagementName.Clear()
                    Else
                        txtAPBManagementName.Text = dr.Item("strManagementName")
                    End If
                    If IsDBNull(dr.Item("strKey")) Then
                        cboManagementType.Text = ""
                    Else
                        cboManagementType.SelectedIndex = dr.Item("strKey") - 1
                    End If
                    If IsDBNull(dr.Item("strKey")) Then
                        Key = ""
                    Else
                        Key = dr.Item("strKey")
                    End If
                End While
                dr.Close()
            End If
            If Key <> "" Then
                Select Case Key
                    Case "1"
                        cboManagementType.Text = "EPD Director"
                    Case "2"
                        cboManagementType.Text = "EPD Commissioner"
                    Case "3"
                        cboManagementType.Text = "APB Branch Chief"
                    Case "4"
                        cboManagementType.Text = "PMII AMP"
                    Case "5"
                        cboManagementType.Text = "PMII ISMP"
                    Case "6"
                        cboManagementType.Text = "PMII MASP"
                    Case "7"
                        cboManagementType.Text = "PMII PASP"
                    Case "8"
                        cboManagementType.Text = "PMII SSCP"
                    Case "9"
                        cboManagementType.Text = "PMII SSPP"
                    Case "10"
                        cboManagementType.Text = "PMI AMP Data Analysis"
                    Case "11"
                        cboManagementType.Text = "PMI AMP Operations"
                    Case "12"
                        cboManagementType.Text = "PMI AMP Meterorology"
                    Case "13"
                        cboManagementType.Text = "PMI AMP QA"
                    Case "14"
                        cboManagementType.Text = "PMI AMP Operations 2"
                    Case "15"
                        cboManagementType.Text = "PMI ISMP Chemical & VOC"
                    Case "16"
                        cboManagementType.Text = "PMI ISMP Combustion & Mineral"
                    Case "17"
                        cboManagementType.Text = "PMI ISMP Data Management"
                    Case "18"
                        cboManagementType.Text = "PMI MASP Engines & Fuels"
                    Case "19"
                        cboManagementType.Text = "PMI MASP I/M"
                    Case "20"
                        cboManagementType.Text = "PMI MASP Enforucement"
                    Case "21"
                        cboManagementType.Text = "PMI MASP Public Affairs"
                    Case "22"
                        cboManagementType.Text = "PMI MASP PIRT"
                    Case "23"
                        cboManagementType.Text = "PMI PASP Admin & Financial"
                    Case "24"
                        cboManagementType.Text = "PMI PASP Data & Modeling"
                    Case "25"
                        cboManagementType.Text = "PMI PASP Planning & Reg."
                    Case "26"
                        cboManagementType.Text = "PMI SSCP Air Toxics"
                    Case "27"
                        cboManagementType.Text = "PMI SSCP Chemcials/Minerals"
                    Case "28"
                        cboManagementType.Text = "PMI SSCP VOC/Combustion"
                    Case "29"
                        cboManagementType.Text = "PMI SSPP Chemcial"
                    Case "30"
                        cboManagementType.Text = "PMI SSPP Combustion"
                    Case "31"
                        cboManagementType.Text = "PMI SSPP Minerals"
                    Case "32"
                        cboManagementType.Text = "PMI SSPP NOx"
                    Case "33"
                        cboManagementType.Text = "PMI SSPP VOC"
                    Case Else
                        cboManagementType.Text = ""
                End Select
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbAPBMangementVacant_CheckedChanged(sender As Object, e As EventArgs) Handles chbAPBMangementVacant.CheckedChanged
        If chbAPBMangementVacant.Checked = True Then
            txtAPBManagementName.Clear()
        End If
    End Sub
    Sub SaveAPBManagement()
        Try
            Dim ManagementType As String = ""
            Dim ManagementName As String = ""

            ManagementType = cboManagementType.Text
            If chbAPBMangementVacant.Checked = True Then
                ManagementName = "Vacant"
            Else
                ManagementName = txtAPBManagementName.Text
            End If

            Select Case ManagementType
                Case "EPD Director"
                    ManagementType = "1"
                Case "EPD Commissioner"
                    ManagementType = "2"
                Case "APB Branch Chief"
                    ManagementType = "3"
                Case "PMII AMP"
                    ManagementType = "4"
                Case "PMII ISMP"
                    ManagementType = "5"
                Case "PMII MASP"
                    ManagementType = "6"
                Case "PMII PASP"
                    ManagementType = "7"
                Case "PMII SSCP"
                    ManagementType = "8"
                Case "PMII SSPP"
                    ManagementType = "9"
                Case "PMI AMP Data Analysis"
                    ManagementType = "10"
                Case "PMI AMP Operations"
                    ManagementType = "11"
                Case "PMI AMP Meterorology"
                    ManagementType = "12"
                Case "PMI AMP QA"
                    ManagementType = "13"
                Case "PMI AMP Operations 2"
                    ManagementType = "14"
                Case "PMI ISMP Chemical & VOC"
                    ManagementType = "15"
                Case "PMI ISMP Combustion & Mineral"
                    ManagementType = "16"
                Case "PMI ISMP Data Management"
                    ManagementType = "17"
                Case "PMI MASP Engines & Fuels"
                    ManagementType = "18"
                Case "PMI MASP I/M"
                    ManagementType = "19"
                Case "PMI MASP Enforucement"
                    ManagementType = "20"
                Case "PMI MASP Public Affairs"
                    ManagementType = "21"
                Case "PMI MASP PIRT"
                    ManagementType = "22"
                Case "PMI PASP Admin & Financial"
                    ManagementType = "23"
                Case "PMI PASP Data & Modeling"
                    ManagementType = "24"
                Case "PMI PASP Planning & Reg."
                    ManagementType = "25"
                Case "PMI SSCP Air Toxics"
                    ManagementType = "26"
                Case "PMI SSCP Chemcials/Minerals"
                    ManagementType = "27"
                Case "PMI SSCP VOC/Combustion"
                    ManagementType = "28"
                Case "PMI SSPP Chemcial"
                    ManagementType = "29"
                Case "PMI SSPP Combustion"
                    ManagementType = "30"
                Case "PMI SSPP Minerals"
                    ManagementType = "31"
                Case "PMI SSPP NOx"
                    ManagementType = "32"
                Case "PMI SSPP VOC"
                    ManagementType = "33"
                Case Else
                    MessageBox.Show("Invalid Management Type", "APB Management Type", MessageBoxButtons.OK)
                    Exit Sub
            End Select

            SQL = "Select numId " &
            "from LookUpAPBManagementType " &
            "where strCurrentContact = '1' " &
            "and strKey = '" & ManagementType & "' "
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("numID")) Then
                Else
                    SQL = "Update LookUpAPBManagementType set " &
                    "strCurrentContact = '0', " &
                    "datEndDate =  GETDATE()  " &
                    "where numId = '" & dr.Item("numID") & "' "
                    cmd2 = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

            SQL = "Insert into LookUpAPBManagementType " &
            "values " &
            "((select max(numId) + 1 from LookUpAPBManagementType), '" & ManagementType & "', " &
            "'" & Replace(ManagementName, "'", "''") & "',  GETDATE() , " &
            "'', '1') "
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            txtAPBManagemetnID.Clear()
            txtAPBManagementName.Clear()
            chbAPBMangementVacant.Checked = False
            cboManagementType.Text = ""

            LoadAPBManagement()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveAPBManagement_Click(sender As Object, e As EventArgs) Handles btnSaveAPBManagement.Click
        SaveAPBManagement()
    End Sub
    Private Sub btnClearManagement_Click(sender As Object, e As EventArgs) Handles btnClearManagement.Click
        txtAPBManagemetnID.Clear()
        txtAPBManagementName.Clear()
        chbAPBMangementVacant.Checked = False
        cboManagementType.Text = ""
    End Sub
    Private Sub btnViewAllPastTypes_Click(sender As Object, e As EventArgs) Handles btnViewAllPastTypes.Click
        Try
            Dim ManagementType As String = ""

            '1 - EPD Director
            '2 - EPD Commissioner
            '3 - APB Branch Chief
            '4 - PMII AMP
            '5 - PMII ISMP
            '6 - PMII MASP
            '7 - PMII PASP
            '8 - PMII SSCP
            '9 - PMII SSPP
            '10 - PMI AMP Data Analysis
            '11 - PMI AMP Operations
            '12 - PMI AMP Meterorology
            '13 - PMI AMP QA
            '14 - PMI AMP Operations 2
            '15 - PMI ISMP Chemical & VOC
            '16 - PMI ISMP Combustion & Mineral
            '17 - PMI ISMP Data Management
            '18 - PMI MASP Engines & Fuels
            '19 - PMI MASP I/M
            '20 - PMI MASP Enforucement 
            '21 - PMI MASP Public Affairs
            '22 - PMI MASP PIRT
            '23 - PMI PASP Admin & Financial 
            '24 - PMI PASP Data & Modeling
            '25 - PMI PASP Planning & Reg.
            '26 - PMI SSCP Air Toxics 
            '27 - PMI SSCP Chemcials/Minerals
            '28 - PMI SSCP VOC/Combustion
            '29 - PMI SSPP Chemcial 
            '30 - PMI SSPP Combustion
            '31 - PMI SSPP Minerals
            '32 - PMI SSPP NOx
            '33 - PMI SSPP VOC

            ManagementType = cboManagementType.Text

            Select Case ManagementType
                Case "EPD Director"
                    ManagementType = "1"
                Case "EPD Commissioner"
                    ManagementType = "2"
                Case "APB Branch Chief"
                    ManagementType = "3"
                Case "PMII AMP"
                    ManagementType = "4"
                Case "PMII ISMP"
                    ManagementType = "5"
                Case "PMII MASP"
                    ManagementType = "6"
                Case "PMII PASP"
                    ManagementType = "7"
                Case "PMII SSCP"
                    ManagementType = "8"
                Case "PMII SSPP"
                    ManagementType = "9"
                Case "PMI AMP Data Analysis"
                    ManagementType = "10"
                Case "PMI AMP Operations"
                    ManagementType = "11"
                Case "PMI AMP Meterorology"
                    ManagementType = "12"
                Case "PMI AMP QA"
                    ManagementType = "13"
                Case "PMI AMP Operations 2"
                    ManagementType = "14"
                Case "PMI ISMP Chemical & VOC"
                    ManagementType = "15"
                Case "PMI ISMP Combustion & Mineral"
                    ManagementType = "16"
                Case "PMI ISMP Data Management"
                    ManagementType = "17"
                Case "PMI MASP Engines & Fuels"
                    ManagementType = "18"
                Case "PMI MASP I/M"
                    ManagementType = "19"
                Case "PMI MASP Enforucement"
                    ManagementType = "20"
                Case "PMI MASP Public Affairs"
                    ManagementType = "21"
                Case "PMI MASP PIRT"
                    ManagementType = "22"
                Case "PMI PASP Admin & Financial"
                    ManagementType = "23"
                Case "PMI PASP Data & Modeling"
                    ManagementType = "24"
                Case "PMI PASP Planning & Reg."
                    ManagementType = "25"
                Case "PMI SSCP Air Toxics"
                    ManagementType = "26"
                Case "PMI SSCP Chemcials/Minerals"
                    ManagementType = "27"
                Case "PMI SSCP VOC/Combustion"
                    ManagementType = "28"
                Case "PMI SSPP Chemcial"
                    ManagementType = "29"
                Case "PMI SSPP Combustion"
                    ManagementType = "30"
                Case "PMI SSPP Minerals"
                    ManagementType = "31"
                Case "PMI SSPP NOx"
                    ManagementType = "32"
                Case "PMI SSPP VOC"
                    ManagementType = "33"
                Case Else
                    MessageBox.Show("Invalid Management Type", "APB Management Type", MessageBoxButtons.OK)
                    Exit Sub
            End Select

            SQL = "Select " &
            "numId, strKey, " &
            "case " &
            "when strKey = '1' then 'EPD Director' " &
            "when strKey = '2' then 'EPD Commissioner' " &
            "when strKey = '3' then 'APB Branch Chief' " &
            "when strKey = '4' then 'PMII AMP' " &
            "when strKey = '5' then 'PMII ISMP' " &
            "when strKey = '6' then 'PMII MASP' " &
            "when strKey = '7' then 'PMII PASP' " &
            "when strKey = '8' then 'PMII SSCP' " &
            "when strKey = '9' then 'PMII SSPP' " &
            "when strKey = '10' then 'PMI AMP Data Analysis' " &
            "when strKey = '11' then 'PMI AMP Operations' " &
            "when strKey = '12' then 'PMI AMP Meterorology' " &
            "when strKey = '13' then 'PMI AMP QA' " &
            "when strKey = '14' then 'PMI AMP Operations 2' " &
            "when strKey = '15' then 'PMI ISMP Chemical & VOC' " &
            "when strKey = '16' then 'PMI ISMP Combustion & Mineral' " &
            "when strKey = '17' then 'PMI ISMP Data Management' " &
            "when strKey = '18' then 'PMI MASP Engines & Fuels' " &
            "when strKey = '19' then 'PMI MASP I/M' " &
            "when strKey = '20' then 'PMI MASP Enforucement' " &
            "when strKey = '21' then 'PMI MASP Public Affairs' " &
            "when strKey = '22' then 'PMI MASP PIRT' " &
            "when strKey = '23' then 'PMI PASP Admin & Financial' " &
            "when strKey = '24' then 'PMI PASP Data & Modeling' " &
            "when strKey = '25' then 'PMI PASP Planning & Reg.' " &
            "when strKey = '26' then 'PMI SSCP Air Toxics' " &
            "when strKey = '27' then 'PMI SSCP Chemcials/Minerals' " &
            "when strKey = '28' then 'PMI SSCP VOC/Combustion' " &
            "when strKey = '29' then 'PMI SSPP Chemcial' " &
            "when strKey = '30' then 'PMI SSPP Combustion' " &
            "when strKey = '31' then 'PMI SSPP Minerals' " &
            "when strKey = '32' then 'PMI SSPP NOx' " &
            "when strKey = '33' then 'PMI SSPP VOC' " &
            "else strKey " &
            "end MangerType, " &
            "strManagementName, datStartDate, " &
            "datEndDate, strCurrentContact " &
            "from LookUpAPBManagementType " &
            "where strKey = '" & ManagementType & "' " &
            "order by numID "

            ds = New DataSet
            da = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            da.Fill(ds, "APBManagement")
            dgvLookUpManagement.DataSource = ds
            dgvLookUpManagement.DataMember = "APBManagement"

            dgvLookUpManagement.RowHeadersVisible = False
            dgvLookUpManagement.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvLookUpManagement.AllowUserToResizeColumns = True
            dgvLookUpManagement.AllowUserToAddRows = False
            dgvLookUpManagement.AllowUserToDeleteRows = False
            dgvLookUpManagement.AllowUserToOrderColumns = True
            dgvLookUpManagement.AllowUserToResizeRows = True

            dgvLookUpManagement.Columns("numId").HeaderText = "ID"
            dgvLookUpManagement.Columns("numId").DisplayIndex = 0
            dgvLookUpManagement.Columns("numId").Width = dgvApplicationType.Width * 0.05
            dgvLookUpManagement.Columns("strKey").HeaderText = "Key"
            dgvLookUpManagement.Columns("strKey").DisplayIndex = 1
            dgvLookUpManagement.Columns("strKey").Visible = False
            dgvLookUpManagement.Columns("MangerType").HeaderText = "Manager Type"
            dgvLookUpManagement.Columns("MangerType").DisplayIndex = 2
            dgvLookUpManagement.Columns("MangerType").Width = dgvApplicationType.Width * 0.25
            dgvLookUpManagement.Columns("strManagementName").HeaderText = "Management Name"
            dgvLookUpManagement.Columns("strManagementName").DisplayIndex = 3
            dgvLookUpManagement.Columns("strManagementName").Width = dgvApplicationType.Width * 0.4
            dgvLookUpManagement.Columns("datStartDate").HeaderText = "Start Date"
            dgvLookUpManagement.Columns("datStartDate").DisplayIndex = 4
            dgvLookUpManagement.Columns("datStartDate").Width = dgvApplicationType.Width * 0.15
            dgvLookUpManagement.Columns("datStartDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvLookUpManagement.Columns("datEndDate").HeaderText = "End Date"
            dgvLookUpManagement.Columns("datEndDate").DisplayIndex = 5
            dgvLookUpManagement.Columns("datEndDate").Width = dgvApplicationType.Width * 0.15
            dgvLookUpManagement.Columns("datEndDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvLookUpManagement.Columns("strCurrentContact").HeaderText = "Current Contact"
            dgvLookUpManagement.Columns("strCurrentContact").DisplayIndex = 5
            dgvLookUpManagement.Columns("strCurrentContact").Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

End Class