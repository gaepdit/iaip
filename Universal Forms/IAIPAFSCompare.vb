Imports System.Data.OracleClient
Imports System.IO

Public Class IAIPAFSCompare
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim temp2 As String
    Dim drDSRow As DataRow

    Private Sub IAIPAFSCompare_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            LoadCounty()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "Page Load"
    Sub LoadCounty()
        Try
            Dim dtCounty As New DataTable
            Dim drNewRow As DataRow
            ds = New DataSet

            SQL = "Select " & _
            "strCountyCode, strCountyName " & _
            "from " & DBNameSpace & ".LookUpCountyInformation " & _
            "order by strCountyName "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            da.Fill(ds, "County")

            dtCounty.Columns.Add("strCountyCode", GetType(System.String))
            dtCounty.Columns.Add("strCountyName", GetType(System.String))

            drNewRow = dtCounty.NewRow()
            drNewRow("strCountyName") = " "
            drNewRow("strCountyCode") = ""
            dtCounty.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("County").Rows()
                drNewRow = dtCounty.NewRow()
                drNewRow("strCountyName") = drDSRow("strCountyName")
                drNewRow("strCountyCode") = drDSRow("strCountyCode")
                dtCounty.Rows.Add(drNewRow)
            Next

            With cboCounty
                .DataSource = dtCounty
                .DisplayMember = "strCountyName"
                .ValueMember = "strCountyCode"
                .SelectedIndex = 0
            End With


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Try
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Try
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSearchForAFSFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchForAFSFile.Click
        Try
            Dim FileName As String = ""
            Dim path As New OpenFileDialog
            Dim temp As String = ""

            path.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            path.FileName = FileName
            path.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            path.FilterIndex = 1

            If path.ShowDialog = Windows.Forms.DialogResult.OK Then
                temp = path.FileName.ToString
            Else
                temp = "N/A"
            End If

            If temp.Length > 3 Then
                If Mid(temp, (temp.Length - 3)).ToUpper = ".TXT" Then
                    txtAFSFile.Text = temp
                Else
                End If
            Else
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnExportAFSData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportAFSData.Click
        Try
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            'Dim ExcelApp As New Excel.Application
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvAFSData.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvAFSData.ColumnCount - 1
                        .Cells(1, i + 1) = dgvAFSData.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvAFSData.ColumnCount - 1
                        For j = 0 To dgvAFSData.RowCount - 2
                            temp = dgvAFSData.Item(i, j).Value

                            If temp Is Nothing Then
                                .Cells(j + 2, i + 1).numberformat = "@"
                                .Cells(j + 2, i + 1).value = " "
                            Else
                                If dgvAFSData.Item(i, j).Value.ToString = Nothing Then
                                    .Cells(j + 2, i + 1).numberformat = "@"
                                    .Cells(j + 2, i + 1).value = " "
                                Else
                                    .Cells(j + 2, i + 1).numberformat = "@"
                                    .Cells(j + 2, i + 1).value = dgvAFSData.Item(i, j).Value.ToString
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
    Private Sub btnLoadAFSData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadAFSData.Click
        Try

            If rdbFacilityName.Checked = True Then
                LoadFacilityName()
                DetectDifferences()
            End If
            If rdbAddress.Checked = True Then
                LoadAddress()
                DetectDifferences()
            End If
            If rdbCityZipCode.Checked = True Then
                LoadCityZip()
                DetectDifferences()
            End If
            If rdbSIC.Checked = True Then
                LoadSIC()
                DetectDifferences()
            End If
            If rdbContact.Checked = True Then
                LoadContact()
                DetectDifferences()
            End If
            If rdbAirProgramCode.Checked = True Then
                LoadAirProgramCode()
                DetectDifferences()
            End If
            If rdbSubPart.Checked = True Then
                LoadSubParts()
                DetectDifferences()
            End If
            If rdbPollutants.Checked = True Then

            End If

            'LoadAFSData2()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadFacilityName()
        Try
            Dim AIRSNumber As String = ""
            Dim IAIPAIRSNumber As String = ""
            Dim TransactionType As String = ""
            Dim IAIPFacilityName As String = ""
            Dim AFSFacilityName As String = ""
            Dim DefaultText As String = ""
            Dim ErrorCompare As String = ""
            Dim intCounter As Integer = 0

            dgvAFSData.Columns.Clear()
            If File.Exists(txtAFSFile.Text) Then
                dgvAFSData.Rows.Clear()

                'Show Data from AFS
                If rdbViewAFSDataOnly.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                    dgvAFSData.Columns("AIRS_Number").Width = 100
                    dgvAFSData.Columns.Add("AFS_Facility_Name", "AFS Facility Name")
                    dgvAFSData.Columns("AFS_Facility_Name").Width = 200

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)
                        Select Case TransactionType
                            Case "101"
                                AFSFacilityName = Mid(DefaultText, 16, 40)
                                dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSFacilityName)
                            Case Else

                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()
                End If

                'Compare AFS against IAIP 
                If rdbCompareAFSDataOnly.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                    dgvAFSData.Columns("AIRS_Number").Width = 100
                    dgvAFSData.Columns.Add("AFS_Facility_Name", "AFS Facility Name")
                    dgvAFSData.Columns("AFS_Facility_Name").Width = 200
                    dgvAFSData.Columns.Add("IAIP_Facility_Name", "IAIP Facility Name")
                    dgvAFSData.Columns("IAIP_Facility_Name").Width = 200

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)
                        Select Case TransactionType
                            Case "101"
                                AFSFacilityName = Mid(DefaultText, 16, 40)
                                IAIPFacilityName = ""

                                SQL = "Select strFacilityName  " & _
                                "from " & DBNameSpace & ".APBFacilityInformation " & _
                                "where strAIRSNumber = '0413" & AIRSNumber & "' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("strFacilityName")) Then
                                        IAIPFacilityName = ""
                                    Else
                                        IAIPFacilityName = dr.Item("strFacilityName")
                                    End If
                                End While
                                dr.Close()

                                dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSFacilityName, IAIPFacilityName)
                            Case Else
                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()
                End If

                'Load IAIP Data and compare AFS
                If rdbCompareFullIAIPdata.Checked = True Then
                    If rdbAIRSNumber.Checked = True Or rdbCounty.Checked = True Then
                        If rdbAIRSNumber.Checked = True Then
                            dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                            dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                            dgvAFSData.Columns("AIRS_Number").Width = 100
                            dgvAFSData.Columns.Add("AFS_Facility_Name", "AFS Facility Name")
                            dgvAFSData.Columns("AFS_Facility_Name").Width = 200
                            dgvAFSData.Columns.Add("IAIP_Facility_Name", "IAIP Facility Name")
                            dgvAFSData.Columns("IAIP_Facility_Name").Width = 200

                            SQL = "Select " & _
                            "strFacilityName " & _
                            "from " & DBNameSpace & ".APBFacilityInformation " & _
                            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                If IsDBNull(dr.Item("strFacilityName")) Then
                                    IAIPFacilityName = ""
                                Else
                                    IAIPFacilityName = dr.Item("strFacilityName")
                                End If
                            End While
                            dr.Close()

                            Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                            Do
                                DefaultText = reader.ReadLine
                                AIRSNumber = Mid(DefaultText, 3, 8)
                                TransactionType = Mid(DefaultText, 11, 3)
                                Select Case TransactionType
                                    Case "101"
                                        AFSFacilityName = Mid(DefaultText, 16, 40)
                                        If AIRSNumber = mtbAIRSNumber.Text Then
                                            dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSFacilityName, IAIPFacilityName)
                                        End If
                                    Case Else
                                End Select

                            Loop Until reader.Peek = -1
                            reader.Close()
                        Else
                            Dim county As String
                            If cboCounty.Text <> " " Or cboCounty.Text <> "" Then
                                county = cboCounty.SelectedValue

                                dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                                dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                                dgvAFSData.Columns("AIRS_Number").Width = 100
                                dgvAFSData.Columns.Add("AFS_Facility_Name", "AFS Facility Name")
                                dgvAFSData.Columns("AFS_Facility_Name").Width = 200
                                dgvAFSData.Columns.Add("IAIP_Facility_Name", "IAIP Facility Name")
                                dgvAFSData.Columns("IAIP_Facility_Name").Width = 200

                                SQL = "Select " & _
                                "substr(strAIRSNumber, 5) as AIRSNumber, " & _
                                "strFacilityName " & _
                                "from " & DBNameSpace & ".APBFacilityInformation " & _
                                "where strAIRSNumber like '0413" & county & "%' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("AIRSNumber")) Then
                                        IAIPAIRSNumber = ""
                                    Else
                                        IAIPAIRSNumber = dr.Item("AIRSNumber")
                                    End If
                                    If IsDBNull(dr.Item("strFacilityName")) Then
                                        IAIPFacilityName = ""
                                    Else
                                        IAIPFacilityName = dr.Item("strFacilityName")
                                    End If
                                    AFSFacilityName = ""
                                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                                    Do
                                        DefaultText = reader.ReadLine
                                        AIRSNumber = Mid(DefaultText, 3, 8)
                                        TransactionType = Mid(DefaultText, 11, 3)
                                        Select Case TransactionType
                                            Case "101"
                                                If AIRSNumber = IAIPAIRSNumber Then
                                                    AFSFacilityName = Mid(DefaultText, 16, 40)
                                                End If
                                            Case Else
                                        End Select

                                    Loop Until reader.Peek = -1
                                    reader.Close()

                                    dgvAFSData.Rows.Add(TransactionType, IAIPAIRSNumber, AFSFacilityName, IAIPFacilityName)

                                End While
                                dr.Close()
                            Else
                                MessageBox.Show("Select a county from the dropdown.")
                                Exit Sub
                            End If

                        End If

                    Else
                        MessageBox.Show("Select either County or AIRS #.")
                        Exit Sub
                    End If
                End If
                txtAFSDataCount.Text = dgvAFSData.RowCount.ToString
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub DetectDifferences()
        Try
            If dgvAFSData.Columns.Count > 3 Then
                For Each row As DataGridViewRow In dgvAFSData.Rows
                    'temp = row.Cells(0).Value
                    'temp = row.Cells(1).Value
                    'temp = row.Cells(2).Value
                    'temp = row.Cells(3).Value
                    If Not row.IsNewRow Then
                        If row.Cells(2).Value.ToString.ToUpper <> row.Cells(3).Value.ToString.ToUpper Then
                            row.DefaultCellStyle.BackColor = Color.Tomato
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadAddress()
        Try
            Dim AIRSNumber As String = ""
            Dim IAIPAIRSNumber As String = ""
            Dim TransactionType As String = ""
            Dim IAIPAddress As String = ""
            Dim AFSAddress As String = ""
            Dim DefaultText As String = ""
            Dim ErrorCompare As String = ""
            Dim intCounter As Integer = 0

            dgvAFSData.Columns.Clear()
            If File.Exists(txtAFSFile.Text) Then
                dgvAFSData.Rows.Clear()

                'Show Data from AFS
                If rdbViewAFSDataOnly.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                    dgvAFSData.Columns("AIRS_Number").Width = 100
                    dgvAFSData.Columns.Add("AFS_FAddress", "AFS Address")
                    dgvAFSData.Columns("AFS_FAddress").Width = 200

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)
                        Select Case TransactionType
                            Case "102"
                                AFSAddress = Mid(DefaultText, 16, 30)
                                dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSAddress)
                            Case Else

                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()
                End If

                'Compare AFS against IAIP 
                If rdbCompareAFSDataOnly.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                    dgvAFSData.Columns("AIRS_Number").Width = 100
                    dgvAFSData.Columns.Add("AFS_Address", "AFS Address")
                    dgvAFSData.Columns("AFS_Address").Width = 200
                    dgvAFSData.Columns.Add("IAIP_Address", "IAIP Address")
                    dgvAFSData.Columns("IAIP_Address").Width = 200

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)
                        Select Case TransactionType
                            Case "102"
                                AFSAddress = Mid(DefaultText, 16, 30)
                                IAIPAddress = ""

                                SQL = "Select strFacilityStreet1 " & _
                                "from " & DBNameSpace & ".APBFacilityInformation " & _
                                "where strAIRSNumber = '0413" & AIRSNumber & "' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                                        IAIPAddress = ""
                                    Else
                                        IAIPAddress = dr.Item("strFacilityStreet1")
                                    End If
                                End While
                                dr.Close()

                                dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSAddress, IAIPAddress)
                            Case Else
                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()
                End If

                'Load IAIP Data and compare AFS
                If rdbCompareFullIAIPdata.Checked = True Then
                    If rdbAIRSNumber.Checked = True Or rdbCounty.Checked = True Then
                        If rdbAIRSNumber.Checked = True Then
                            dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                            dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                            dgvAFSData.Columns("AIRS_Number").Width = 100
                            dgvAFSData.Columns.Add("AFS_Address", "AFS Address")
                            dgvAFSData.Columns("AFS_Address").Width = 200
                            dgvAFSData.Columns.Add("IAIP_Address", "IAIP Address")
                            dgvAFSData.Columns("IAIP_Address").Width = 200

                            SQL = "Select " & _
                            "strFacilityStreet1 " & _
                            "from " & DBNameSpace & ".APBFacilityInformation " & _
                            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                                    IAIPAddress = ""
                                Else
                                    IAIPAddress = dr.Item("strFacilityStreet1")
                                End If
                            End While
                            dr.Close()

                            Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                            Do
                                DefaultText = reader.ReadLine
                                AIRSNumber = Mid(DefaultText, 3, 8)
                                TransactionType = Mid(DefaultText, 11, 3)
                                Select Case TransactionType
                                    Case "102"
                                        AFSAddress = Mid(DefaultText, 16, 30)
                                        If AIRSNumber = mtbAIRSNumber.Text Then
                                            dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSAddress, IAIPAddress)
                                        End If
                                    Case Else
                                End Select

                            Loop Until reader.Peek = -1
                            reader.Close()
                        Else
                            Dim county As String
                            If cboCounty.Text <> " " Or cboCounty.Text <> "" Then
                                county = cboCounty.SelectedValue

                                dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                                dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                                dgvAFSData.Columns("AIRS_Number").Width = 100
                                dgvAFSData.Columns.Add("AFS_Address", "AFS Address")
                                dgvAFSData.Columns("AFS_Address").Width = 200
                                dgvAFSData.Columns.Add("IAIP_Address", "IAIP Address")
                                dgvAFSData.Columns("IAIP_Address").Width = 200

                                SQL = "Select " & _
                                "substr(strAIRSNumber, 5) as AIRSNumber, " & _
                                "strFacilityStreet1 " & _
                                "from " & DBNameSpace & ".APBFacilityInformation " & _
                                "where strAIRSNumber like '0413" & county & "%' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("AIRSNumber")) Then
                                        IAIPAIRSNumber = ""
                                    Else
                                        IAIPAIRSNumber = dr.Item("AIRSNumber")
                                    End If
                                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                                        IAIPAddress = ""
                                    Else
                                        IAIPAddress = dr.Item("strFacilityStreet1")
                                    End If
                                    AFSAddress = ""
                                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                                    Do
                                        DefaultText = reader.ReadLine
                                        AIRSNumber = Mid(DefaultText, 3, 8)
                                        TransactionType = Mid(DefaultText, 11, 3)
                                        Select Case TransactionType
                                            Case "102"
                                                If AIRSNumber = IAIPAIRSNumber Then
                                                    AFSAddress = Mid(DefaultText, 16, 30)
                                                End If
                                            Case Else
                                        End Select

                                    Loop Until reader.Peek = -1
                                    reader.Close()

                                    dgvAFSData.Rows.Add(TransactionType, IAIPAIRSNumber, AFSAddress, IAIPAddress)

                                End While
                                dr.Close()
                            Else
                                MessageBox.Show("Select a county from the dropdown.")
                                Exit Sub
                            End If

                        End If

                    Else
                        MessageBox.Show("Select either County or AIRS #.")
                        Exit Sub
                    End If
                End If
                txtAFSDataCount.Text = dgvAFSData.RowCount.ToString
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadCityZip()
        Try
            Dim AIRSNumber As String = ""
            Dim IAIPAIRSNumber As String = ""
            Dim TransactionType As String = ""
            Dim IAIPCityZip As String = ""
            Dim AFSCityZip As String = ""
            Dim DefaultText As String = ""
            Dim ErrorCompare As String = ""
            Dim intCounter As Integer = 0

            dgvAFSData.Columns.Clear()
            If File.Exists(txtAFSFile.Text) Then
                dgvAFSData.Rows.Clear()

                'Show Data from AFS
                If rdbViewAFSDataOnly.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                    dgvAFSData.Columns("AIRS_Number").Width = 100
                    dgvAFSData.Columns.Add("AFS_CityZip", "AFS City/Zip Code")
                    dgvAFSData.Columns("AFS_CityZip").Width = 200

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)
                        Select Case TransactionType
                            Case "103"
                                AFSCityZip = RTrim(Mid(DefaultText, 16, 30)) & " - " & RTrim(Mid(DefaultText, 46, 9))
                                dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSCityZip)
                            Case Else

                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()
                End If

                'Compare AFS against IAIP 
                If rdbCompareAFSDataOnly.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                    dgvAFSData.Columns("AIRS_Number").Width = 100
                    dgvAFSData.Columns.Add("AFS_CityZip", "AFS City/Zip Code")
                    dgvAFSData.Columns("AFS_CityZip").Width = 200
                    dgvAFSData.Columns.Add("IAIP_CityZip", "IAIP City/Zip Code")
                    dgvAFSData.Columns("IAIP_CityZip").Width = 200

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)
                        Select Case TransactionType
                            Case "103"
                                AFSCityZip = RTrim(Mid(DefaultText, 16, 30)) & " - " & RTrim(Mid(DefaultText, 46, 9))
                                IAIPCityZip = ""

                                SQL = "Select " & _
                                "(strFacilityCity|| ' - '||strFacilityZipCode) as CityZip " & _
                                "from " & DBNameSpace & ".APBFacilityInformation  " & _
                                "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = '0413" & AIRSNumber & "' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("CityZip")) Then
                                        IAIPCityZip = ""
                                    Else
                                        IAIPCityZip = dr.Item("CityZip")
                                    End If
                                End While
                                dr.Close()

                                dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSCityZip, IAIPCityZip)
                            Case Else
                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()
                End If

                'Load IAIP Data and compare AFS
                If rdbCompareFullIAIPdata.Checked = True Then
                    If rdbAIRSNumber.Checked = True Or rdbCounty.Checked = True Then
                        If rdbAIRSNumber.Checked = True Then
                            dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                            dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                            dgvAFSData.Columns("AIRS_Number").Width = 100
                            dgvAFSData.Columns.Add("AFS_CityZip", "AFS City/Zip Code")
                            dgvAFSData.Columns("AFS_CityZip").Width = 200
                            dgvAFSData.Columns.Add("IAIP_CityZip", "IAIP City/Zip Code")
                            dgvAFSData.Columns("IAIP_CityZip").Width = 200

                            SQL = "Select " & _
                            "(strFacilityCity|| ' - '||strFacilityZipCode) as CityZip " & _
                            "from " & DBNameSpace & ".APBFacilityInformation  " & _
                            "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                If IsDBNull(dr.Item("CityZip")) Then
                                    IAIPCityZip = ""
                                Else
                                    IAIPCityZip = dr.Item("CityZip")
                                End If
                            End While
                            dr.Close()

                            Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                            Do
                                DefaultText = reader.ReadLine
                                AIRSNumber = Mid(DefaultText, 3, 8)
                                TransactionType = Mid(DefaultText, 11, 3)
                                Select Case TransactionType
                                    Case "103"
                                        AFSCityZip = RTrim(Mid(DefaultText, 16, 30)) & " - " & RTrim(Mid(DefaultText, 46, 9))
                                        If AIRSNumber = mtbAIRSNumber.Text Then
                                            dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSCityZip, IAIPCityZip)
                                        End If
                                    Case Else
                                End Select

                            Loop Until reader.Peek = -1
                            reader.Close()
                        Else
                            Dim county As String
                            If cboCounty.Text <> " " Or cboCounty.Text <> "" Then
                                county = cboCounty.SelectedValue

                                dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                                dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                                dgvAFSData.Columns("AIRS_Number").Width = 100
                                dgvAFSData.Columns.Add("AFS_CityZip", "AFS City/Zip Code")
                                dgvAFSData.Columns("AFS_CityZip").Width = 200
                                dgvAFSData.Columns.Add("IAIP_CityZip", "IAIP City/Zip Code")
                                dgvAFSData.Columns("IAIP_CityZip").Width = 200

                                SQL = "Select " & _
                                "substr(strAIRSNumber, 5) as AIRSNumber, " & _
                                "(strFacilityCity|| ' - '||strFacilityZipCode) as CityZip " & _
                                "from " & DBNameSpace & ".APBFacilityInformation " & _
                                "where strAIRSNumber like '0413" & county & "%' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("AIRSNumber")) Then
                                        IAIPAIRSNumber = ""
                                    Else
                                        IAIPAIRSNumber = dr.Item("AIRSNumber")
                                    End If
                                    If IsDBNull(dr.Item("CityZip")) Then
                                        IAIPCityZip = ""
                                    Else
                                        IAIPCityZip = dr.Item("CityZip")
                                    End If
                                    AFSCityZip = ""
                                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                                    Do
                                        DefaultText = reader.ReadLine
                                        AIRSNumber = Mid(DefaultText, 3, 8)
                                        TransactionType = Mid(DefaultText, 11, 3)
                                        Select Case TransactionType
                                            Case "103"
                                                If AIRSNumber = IAIPAIRSNumber Then
                                                    AFSCityZip = RTrim(Mid(DefaultText, 16, 30)) & " - " & RTrim(Mid(DefaultText, 46, 9))
                                                End If
                                            Case Else
                                        End Select

                                    Loop Until reader.Peek = -1
                                    reader.Close()

                                    dgvAFSData.Rows.Add(TransactionType, IAIPAIRSNumber, AFSCityZip, IAIPCityZip)

                                End While
                                dr.Close()
                            Else
                                MessageBox.Show("Select a county from the dropdown.")
                                Exit Sub
                            End If

                        End If

                    Else
                        MessageBox.Show("Select either County or AIRS #.")
                        Exit Sub
                    End If
                End If
                txtAFSDataCount.Text = dgvAFSData.RowCount.ToString
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadSIC()
        Try
            Dim AIRSNumber As String = ""
            Dim IAIPAIRSNumber As String = ""
            Dim TransactionType As String = ""
            Dim IAIPSIC As String = ""
            Dim AFSSIC As String = ""
            Dim DefaultText As String = ""
            Dim ErrorCompare As String = ""
            Dim intCounter As Integer = 0

            dgvAFSData.Columns.Clear()
            If File.Exists(txtAFSFile.Text) Then
                dgvAFSData.Rows.Clear()

                'Show Data from AFS
                If rdbViewAFSDataOnly.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                    dgvAFSData.Columns("AIRS_Number").Width = 100
                    dgvAFSData.Columns.Add("AFS_SIC", "AFS City/Zip Code")
                    dgvAFSData.Columns("AFS_SIC").Width = 200

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)
                        Select Case TransactionType
                            Case "103"
                                AFSSIC = Mid(DefaultText, 55, 4)
                                dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSIC)
                            Case Else

                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()
                End If

                'Compare AFS against IAIP 
                If rdbCompareAFSDataOnly.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                    dgvAFSData.Columns("AIRS_Number").Width = 100
                    dgvAFSData.Columns.Add("AFS_SIC", "AFS SIC")
                    dgvAFSData.Columns("AFS_SIC").Width = 200
                    dgvAFSData.Columns.Add("IAIP_SIC", "IAIP SIC")
                    dgvAFSData.Columns("IAIP_SIC").Width = 200

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)
                        Select Case TransactionType
                            Case "103"
                                AFSSIC = Mid(DefaultText, 55, 4)
                                IAIPSIC = ""

                                SQL = "Select " & _
                                "strSICCode " & _
                                "from " & DBNameSpace & ".APBHeaderData  " & _
                                "where " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & AIRSNumber & "' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("strSICCode")) Then
                                        IAIPSIC = ""
                                    Else
                                        IAIPSIC = dr.Item("strSICCode")
                                    End If
                                End While
                                dr.Close()

                                dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSIC, IAIPSIC)
                            Case Else
                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()
                End If

                'Load IAIP Data and compare AFS
                If rdbCompareFullIAIPdata.Checked = True Then
                    If rdbAIRSNumber.Checked = True Or rdbCounty.Checked = True Then
                        If rdbAIRSNumber.Checked = True Then
                            dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                            dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                            dgvAFSData.Columns("AIRS_Number").Width = 100
                            dgvAFSData.Columns.Add("AFS_SIC", "AFS SIC")
                            dgvAFSData.Columns("AFS_SIC").Width = 200
                            dgvAFSData.Columns.Add("IAIP_SIC", "IAIP SIC")
                            dgvAFSData.Columns("IAIP_SIC").Width = 200

                            SQL = "Select " & _
                            "strSICCode " & _
                            "from " & DBNameSpace & ".APBHeaderData  " & _
                            "where " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                If IsDBNull(dr.Item("strSICCode")) Then
                                    IAIPSIC = ""
                                Else
                                    IAIPSIC = dr.Item("strSICCode")
                                End If
                            End While
                            dr.Close()

                            Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                            Do
                                DefaultText = reader.ReadLine
                                AIRSNumber = Mid(DefaultText, 3, 8)
                                TransactionType = Mid(DefaultText, 11, 3)
                                Select Case TransactionType
                                    Case "103"
                                        AFSSIC = Mid(DefaultText, 55, 4)
                                        If AIRSNumber = mtbAIRSNumber.Text Then
                                            dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSIC, IAIPSIC)
                                        End If
                                    Case Else
                                End Select

                            Loop Until reader.Peek = -1
                            reader.Close()
                        Else
                            Dim county As String
                            If cboCounty.Text <> " " Or cboCounty.Text <> "" Then
                                county = cboCounty.SelectedValue

                                dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                                dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                                dgvAFSData.Columns("AIRS_Number").Width = 100
                                dgvAFSData.Columns.Add("AFS_SIC", "AFS SIC")
                                dgvAFSData.Columns("AFS_SIC").Width = 200
                                dgvAFSData.Columns.Add("IAIP_SIC", "IAIP SIC")
                                dgvAFSData.Columns("IAIP_SIC").Width = 200

                                SQL = "Select " & _
                                "substr(strAIRSNumber, 5) as AIRSNumber, " & _
                                "strSICCode " & _
                                "from " & DBNameSpace & ".APBHeaderData " & _
                                "where strAIRSNumber like '0413" & county & "%' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("AIRSNumber")) Then
                                        IAIPAIRSNumber = ""
                                    Else
                                        IAIPAIRSNumber = dr.Item("AIRSNumber")
                                    End If
                                    If IsDBNull(dr.Item("strSICCode")) Then
                                        IAIPSIC = ""
                                    Else
                                        IAIPSIC = dr.Item("strSICCode")
                                    End If
                                    AFSSIC = ""
                                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                                    Do
                                        DefaultText = reader.ReadLine
                                        AIRSNumber = Mid(DefaultText, 3, 8)
                                        TransactionType = Mid(DefaultText, 11, 3)
                                        Select Case TransactionType
                                            Case "103"
                                                If AIRSNumber = IAIPAIRSNumber Then
                                                    AFSSIC = Mid(DefaultText, 55, 4)
                                                End If
                                            Case Else
                                        End Select

                                    Loop Until reader.Peek = -1
                                    reader.Close()

                                    dgvAFSData.Rows.Add(TransactionType, IAIPAIRSNumber, AFSSIC, IAIPSIC)

                                End While
                                dr.Close()
                            Else
                                MessageBox.Show("Select a county from the dropdown.")
                                Exit Sub
                            End If

                        End If

                    Else
                        MessageBox.Show("Select either County or AIRS #.")
                        Exit Sub
                    End If
                End If
                txtAFSDataCount.Text = dgvAFSData.RowCount.ToString
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadContact()
        Try
            Dim AIRSNumber As String = ""
            Dim IAIPAIRSNumber As String = ""
            Dim TransactionType As String = ""
            Dim IAIPContact As String = ""
            Dim AFSContact As String = ""
            Dim DefaultText As String = ""
            Dim ErrorCompare As String = ""
            Dim intCounter As Integer = 0

            dgvAFSData.Columns.Clear()
            If File.Exists(txtAFSFile.Text) Then
                dgvAFSData.Rows.Clear()

                'Show Data from AFS
                If rdbViewAFSDataOnly.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                    dgvAFSData.Columns("AIRS_Number").Width = 100
                    dgvAFSData.Columns.Add("AFS_Contact", "AFS Contact")
                    dgvAFSData.Columns("AFS_Contact").Width = 200

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)
                        Select Case TransactionType
                            Case "105"
                                AFSContact = RTrim(Mid(DefaultText, 14, 20)) & " - " & RTrim(Mid(DefaultText, 34, 10))
                                dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSContact)
                            Case Else

                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()
                End If

                'Compare AFS against IAIP 
                If rdbCompareAFSDataOnly.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                    dgvAFSData.Columns("AIRS_Number").Width = 100
                    dgvAFSData.Columns.Add("AFS_Contact", "AFS Contact")
                    dgvAFSData.Columns("AFS_Contact").Width = 200
                    dgvAFSData.Columns.Add("IAIP_Contact", "IAIP Contact")
                    dgvAFSData.Columns("IAIP_Contact").Width = 200

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)
                        Select Case TransactionType
                            Case "105"
                                AFSContact = RTrim(Mid(DefaultText, 14, 20)) & " - " & RTrim(Mid(DefaultText, 34, 10))
                                IAIPContact = ""

                                SQL = "select " & _
                                "(strContactFirstName||' '||strContactLastName|| ' - ' ||strContactPhoneNumber1) as Contact " & _
                                "from " & DBNameSpace & ".APBContactInformation " & _
                                "where " & DBNameSpace & ".APBContactInformation.strairsnumber = '0413" & AIRSNumber & "'  " & _
                                "and strkey = '30'"

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("Contact")) Then
                                        IAIPContact = ""
                                    Else
                                        IAIPContact = dr.Item("Contact")
                                    End If
                                End While
                                dr.Close()

                                dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSContact, IAIPContact)
                            Case Else
                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()
                End If

                'Load IAIP Data and compare AFS
                If rdbCompareFullIAIPdata.Checked = True Then
                    If rdbAIRSNumber.Checked = True Or rdbCounty.Checked = True Then
                        If rdbAIRSNumber.Checked = True Then
                            dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                            dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                            dgvAFSData.Columns("AIRS_Number").Width = 100
                            dgvAFSData.Columns.Add("AFS_Contact", "AFS Contact")
                            dgvAFSData.Columns("AFS_Contact").Width = 200
                            dgvAFSData.Columns.Add("IAIP_Contact", "IAIP Contact")
                            dgvAFSData.Columns("IAIP_Contact").Width = 200

                            SQL = "select " & _
                            "(strContactFirstName||' '||strContactLastName|| ' - ' ||strContactPhoneNumber1) as Contact " & _
                            "from " & DBNameSpace & ".APBContactInformation " & _
                            "where " & DBNameSpace & ".APBContactInformation.strairsnumber = '0413" & mtbAIRSNumber.Text & "'  " & _
                            "and strkey = '30'"

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            While dr.Read
                                If IsDBNull(dr.Item("Contact")) Then
                                    IAIPContact = ""
                                Else
                                    IAIPContact = dr.Item("Contact")
                                End If
                            End While
                            dr.Close()

                            Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                            Do
                                DefaultText = reader.ReadLine
                                AIRSNumber = Mid(DefaultText, 3, 8)
                                TransactionType = Mid(DefaultText, 11, 3)
                                Select Case TransactionType
                                    Case "105"
                                        AFSContact = RTrim(Mid(DefaultText, 14, 20)) & " - " & RTrim(Mid(DefaultText, 34, 10))
                                        If AIRSNumber = mtbAIRSNumber.Text Then
                                            dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSContact, IAIPContact)
                                        End If
                                    Case Else
                                End Select

                            Loop Until reader.Peek = -1
                            reader.Close()
                        Else
                            Dim county As String
                            If cboCounty.Text <> " " Or cboCounty.Text <> "" Then
                                county = cboCounty.SelectedValue

                                dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                                dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                                dgvAFSData.Columns("AIRS_Number").Width = 100
                                dgvAFSData.Columns.Add("AFS_Contact", "AFS Contact")
                                dgvAFSData.Columns("AFS_Contact").Width = 200
                                dgvAFSData.Columns.Add("IAIP_Contact", "IAIP Contact")
                                dgvAFSData.Columns("IAIP_Contact").Width = 200

                                SQL = "select " & _
                                "substr(strAIRSNumber, 5) as AIRSNumber, " & _
                                "(strContactFirstName||' '||strContactLastName|| ' - ' ||strContactPhoneNumber1) as Contact " & _
                                "from " & DBNameSpace & ".APBContactInformation " & _
                                "where " & DBNameSpace & ".APBContactInformation.strairsnumber like '0413" & county & "%' " & _
                                "and strkey = '30'"

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("AIRSNumber")) Then
                                        IAIPAIRSNumber = ""
                                    Else
                                        IAIPAIRSNumber = dr.Item("AIRSNumber")
                                    End If
                                    If IsDBNull(dr.Item("Contact")) Then
                                        IAIPContact = ""
                                    Else
                                        IAIPContact = dr.Item("Contact")
                                    End If
                                    AFSContact = ""
                                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                                    Do
                                        DefaultText = reader.ReadLine
                                        AIRSNumber = Mid(DefaultText, 3, 8)
                                        TransactionType = Mid(DefaultText, 11, 3)
                                        Select Case TransactionType
                                            Case "105"
                                                If AIRSNumber = IAIPAIRSNumber Then
                                                    AFSContact = RTrim(Mid(DefaultText, 14, 20)) & " - " & RTrim(Mid(DefaultText, 34, 10))
                                                End If
                                            Case Else
                                        End Select

                                    Loop Until reader.Peek = -1
                                    reader.Close()
                                    dgvAFSData.Rows.Add(TransactionType, IAIPAIRSNumber, AFSContact, IAIPContact)
                                End While
                                dr.Close()
                            Else
                                MessageBox.Show("Select a county from the dropdown.")
                                Exit Sub
                            End If
                        End If
                    Else
                        MessageBox.Show("Select either County or AIRS #.")
                        Exit Sub
                    End If
                End If
                txtAFSDataCount.Text = dgvAFSData.RowCount.ToString
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadAirProgramCode()
        Try
            Dim AIRSNumber As String = ""
            Dim AFSAIRSNumber As String = ""
            Dim IAIPAIRSNumber As String = ""
            Dim TransactionType As String = ""
            Dim IAIPAirCode As String = ""
            Dim AFSAirCode As String = ""
            Dim DefaultText As String = ""
            Dim ErrorCompare As String = ""
            Dim intCounter As Integer = 0

            dgvAFSData.Columns.Clear()
            If File.Exists(txtAFSFile.Text) Then
                dgvAFSData.Rows.Clear()

                'Show Data from AFS
                If rdbViewAFSDataOnly.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                    dgvAFSData.Columns("AIRS_Number").Width = 100
                    dgvAFSData.Columns.Add("AFS_AIR_CODE", "AFS AIR Code/Status")
                    dgvAFSData.Columns("AFS_AIR_CODE").Width = 200

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)
                        Select Case TransactionType
                            Case "121"
                                AFSAirCode = Mid(DefaultText, 14, 1) & " - " & Mid(DefaultText, 15, 1)
                                dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSAirCode)
                            Case Else

                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()
                End If

                'Compare AFS against IAIP 
                If rdbCompareAFSDataOnly.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                    dgvAFSData.Columns("AIRS_Number").Width = 100
                    dgvAFSData.Columns.Add("AFS_AIR_CODE", "AFS AIR Code/Status")
                    dgvAFSData.Columns("AFS_AIR_CODE").Width = 200
                    dgvAFSData.Columns.Add("IAIP_AIR_Code", "IAIP AIR Code/Status")
                    dgvAFSData.Columns("IAIP_AIR_Code").Width = 200

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)
                        Select Case TransactionType
                            Case "121"
                                AFSAirCode = Mid(DefaultText, 14, 1) & " - " & Mid(DefaultText, 15, 1)
                                IAIPAirCode = ""

                                SQL = "select " & _
                                "distinct(substr(strAirPollutantKey, 13, 1)) as AirCode, " & _
                                "strOperationalStatus " & _
                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                "and substr(strAirPollutantKey, 13, 1) = '" & Mid(DefaultText, 14, 1) & "'  "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("AirCode")) Then
                                        IAIPAirCode = ""
                                    Else
                                        IAIPAirCode = dr.Item("AirCode")
                                    End If
                                    If IsDBNull(dr.Item("strOperationalStatus")) Then
                                        'IAIPAirCode = IAIPAirCode
                                    Else
                                        IAIPAirCode = IAIPAirCode & " - " & dr.Item("strOperationalStatus")
                                    End If
                                End While
                                dr.Close()

                                dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSAirCode, IAIPAirCode)
                            Case Else
                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()
                End If

                'Load IAIP Data and compare AFS
                If rdbCompareFullIAIPdata.Checked = True Then
                    If rdbAIRSNumber.Checked = True Or rdbCounty.Checked = True Then
                        If rdbAIRSNumber.Checked = True Then
                            dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                            dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                            dgvAFSData.Columns("AIRS_Number").Width = 100
                            dgvAFSData.Columns.Add("AFS_AIR_CODE", "AFS AIR Code/Status")
                            dgvAFSData.Columns("AFS_AIR_CODE").Width = 200
                            dgvAFSData.Columns.Add("IAIP_AIR_CODE", "IAIP AIR Code/Status")
                            dgvAFSData.Columns("IAIP_AIR_CODE").Width = 200

                            SQL = "select " & _
                            "distinct(substr(strAirPollutantKey, 13, 1)) as AirCode, " & _
                            "strOperationalStatus " & _
                            "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            AIRSNumber = mtbAIRSNumber.Text
                            AFSAirCode = ""

                            While dr.Read
                                If IsDBNull(dr.Item("AirCode")) Then
                                    IAIPAirCode = ""
                                Else
                                    IAIPAirCode = dr.Item("AirCode")
                                End If
                                If IsDBNull(dr.Item("strOperationalStatus")) Then
                                    'IAIPAirCode = IAIPAirCode
                                Else
                                    IAIPAirCode = IAIPAirCode & " - " & dr.Item("strOperationalStatus")
                                End If
                                AFSAirCode = ""

                                Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                                Do
                                    DefaultText = reader.ReadLine
                                    TransactionType = Mid(DefaultText, 11, 3)
                                    Select Case TransactionType
                                        Case "121"
                                            AFSAIRSNumber = Mid(DefaultText, 3, 8)

                                            If AIRSNumber = AFSAIRSNumber Then
                                                If IAIPAirCode = Mid(DefaultText, 14, 1) & " - " & Mid(DefaultText, 15, 1) Then
                                                    AFSAirCode = Mid(DefaultText, 14, 1) & " - " & Mid(DefaultText, 15, 1)
                                                End If
                                            End If
                                        Case Else
                                    End Select
                                Loop Until reader.Peek = -1
                                reader.Close()

                                dgvAFSData.Rows.Add("121", AIRSNumber, AFSAirCode, IAIPAirCode)

                            End While
                            dr.Close()

                            Dim reader2 As StreamReader = New StreamReader(txtAFSFile.Text)
                            Do
                                DefaultText = reader2.ReadLine
                                AIRSNumber = Mid(DefaultText, 3, 8)
                                TransactionType = Mid(DefaultText, 11, 3)
                                Select Case TransactionType
                                    Case "121"
                                        If AIRSNumber = mtbAIRSNumber.Text Then
                                            AFSAirCode = Mid(DefaultText, 14, 1) & " - " & Mid(DefaultText, 15, 1)
                                        End If
                                End Select
                                temp = "0"
                                If AIRSNumber = mtbAIRSNumber.Text Then
                                    For Each row As DataGridViewRow In dgvAFSData.Rows
                                        If Not row.IsNewRow Then
                                            If row.Cells(2).Value.ToString = AFSAirCode Then
                                                temp = "1"
                                            End If
                                        End If
                                    Next
                                    If temp = "0" Then
                                        dgvAFSData.Rows.Add("121", AIRSNumber, AFSAirCode, "")
                                    End If
                                End If
                            Loop Until reader2.Peek = -1
                        Else
                            Dim county As String
                            If cboCounty.Text <> " " Or cboCounty.Text <> "" Then
                                county = cboCounty.SelectedValue

                                dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                                dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                                dgvAFSData.Columns("AIRS_Number").Width = 100
                                dgvAFSData.Columns.Add("AFS_AIR_CODE", "AFS AIR Code/Status")
                                dgvAFSData.Columns("AFS_AIR_CODE").Width = 200
                                dgvAFSData.Columns.Add("IAIP_AIR_CODE", "IAIP AIR Code/Status")
                                dgvAFSData.Columns("IAIP_AIR_CODE").Width = 200

                                SQL = "select " & _
                                "distinct(substr(strAirPollutantKey, 13, 1)) as AirCode, " & _
                                "strOperationalStatus, " & _
                                "substr(strAIRSNumber, 5) as AIRSNumber " & _
                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                "where strAIRSNumber like '0413" & county & "%' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("AIRSNumber")) Then
                                        IAIPAIRSNumber = ""
                                    Else
                                        IAIPAIRSNumber = dr.Item("AIRSNumber")
                                    End If
                                    If IsDBNull(dr.Item("AirCode")) Then
                                        IAIPAirCode = ""
                                    Else
                                        IAIPAirCode = dr.Item("AirCode")
                                    End If
                                    If IsDBNull(dr.Item("strOperationalStatus")) Then
                                        'IAIPAirCode = IAIPAirCode
                                    Else
                                        IAIPAirCode = IAIPAirCode & " - " & dr.Item("strOperationalStatus")
                                    End If
                                    AFSAirCode = ""

                                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                                    Do
                                        DefaultText = reader.ReadLine
                                        AIRSNumber = Mid(DefaultText, 3, 8)
                                        TransactionType = Mid(DefaultText, 11, 3)
                                        Select Case TransactionType
                                            Case "121"
                                                If AIRSNumber = IAIPAIRSNumber Then
                                                    If IAIPAirCode = Mid(DefaultText, 14, 1) & " - " & Mid(DefaultText, 15, 1) Then
                                                        AFSAirCode = Mid(DefaultText, 14, 1) & " - " & Mid(DefaultText, 15, 1)
                                                    End If
                                                End If
                                            Case Else
                                        End Select

                                    Loop Until reader.Peek = -1
                                    reader.Close()

                                    dgvAFSData.Rows.Add(TransactionType, IAIPAIRSNumber, AFSAirCode, IAIPAirCode)

                                End While
                                dr.Close()

                                Dim reader2 As StreamReader = New StreamReader(txtAFSFile.Text)
                                Do
                                    DefaultText = reader2.ReadLine
                                    AIRSNumber = Mid(DefaultText, 3, 8)
                                    TransactionType = Mid(DefaultText, 11, 3)
                                    AFSAirCode = ""
                                    Select Case TransactionType
                                        Case "121"
                                            AFSAirCode = Mid(DefaultText, 14, 1) & " - " & Mid(DefaultText, 15, 1)
                                    End Select
                                    temp = "0"
                                    If AFSAirCode <> "" And AIRSNumber <> "" Then
                                        For Each row As DataGridViewRow In dgvAFSData.Rows
                                            If Not row.IsNewRow Then
                                                If row.Cells(1).Value.ToString = AIRSNumber And row.Cells(2).Value.ToString = AFSAirCode Then
                                                    temp = "1"
                                                End If
                                            End If
                                        Next
                                        If temp = "0" Then
                                            dgvAFSData.Rows.Add("121", AIRSNumber, AFSAirCode, "")
                                        End If
                                    End If
                                Loop Until reader2.Peek = -1
                            Else
                                MessageBox.Show("Select a county from the dropdown.")
                                Exit Sub
                            End If

                        End If

                    Else
                        MessageBox.Show("Select either County or AIRS #.")
                        Exit Sub
                    End If
                End If
                txtAFSDataCount.Text = dgvAFSData.RowCount.ToString
                Exit Sub
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadSubParts()
        Try
            Dim AIRSNumber As String = ""
            Dim AFSAIRSNumber As String = ""
            Dim IAIPAIRSNumber As String = ""
            Dim TransactionType As String = ""
            Dim AFSAirCode As String = ""
            Dim AFSSubPart As String = ""
            Dim IAIPAirCode As String = ""
            Dim IAIPSubPart As String = ""
            Dim DefaultText As String = ""
            Dim ErrorCompare As String = ""
            Dim intCounter As Integer = 0

            dgvAFSData.Columns.Clear()
            If File.Exists(txtAFSFile.Text) Then
                dgvAFSData.Rows.Clear()

                'Show Data from AFS
                If rdbViewAFSDataOnly.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                    dgvAFSData.Columns("AIRS_Number").Width = 100
                    dgvAFSData.Columns.Add("AFS_SubPart", "AFS SubPart")
                    dgvAFSData.Columns("AFS_SubPart").Width = 200

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)

                        Select Case TransactionType
                            Case "122"
                                AFSAirCode = Mid(DefaultText, 14, 1)
                                If Mid(DefaultText, 16, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & Mid(DefaultText, 16, 5)
                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart)
                                End If
                                If Mid(DefaultText, 21, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & Mid(DefaultText, 21, 5)
                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart)
                                End If

                                If Mid(DefaultText, 26, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & Mid(DefaultText, 26, 5)
                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart)
                                End If
                                If Mid(DefaultText, 31, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & Mid(DefaultText, 31, 5)
                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart)
                                End If
                                If Mid(DefaultText, 36, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & Mid(DefaultText, 36, 5)
                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart)
                                End If
                                If Mid(DefaultText, 41, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & Mid(DefaultText, 41, 5)
                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart)
                                End If
                                If Mid(DefaultText, 46, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & Mid(DefaultText, 46, 5)
                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart)
                                End If
                                If Mid(DefaultText, 51, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & Mid(DefaultText, 51, 5)
                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart)
                                End If
                                If Mid(DefaultText, 56, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & Mid(DefaultText, 56, 5)
                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart)
                                End If
                            Case Else

                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()
                End If

                'Compare AFS against IAIP 
                If rdbCompareAFSDataOnly.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                    dgvAFSData.Columns("AIRS_Number").Width = 100
                    dgvAFSData.Columns.Add("AFS_SubPart", "AFS SubPart")
                    dgvAFSData.Columns("AFS_SubPart").Width = 200
                    dgvAFSData.Columns.Add("IAIP_SubPart", "IAIP SubPart")
                    dgvAFSData.Columns("IAIP_SubPart").Width = 200

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)
                        Select Case TransactionType
                            Case "122"
                                AFSAirCode = Mid(DefaultText, 14, 1)
                                IAIPSubPart = ""
                                If Mid(DefaultText, 16, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & RTrim(Mid(DefaultText, 16, 5))

                                    SQL = "select " & _
                                    "strSubPart " & _
                                    "from " & DBNameSpace & ".APBSubPartData  " & _
                                    "where strsubpartkey = '0413" & AIRSNumber & "" & AFSAirCode & "' " & _
                                    "and (upper(strSubPart) = '" & RTrim(Mid(DefaultText, 16, 5).ToUpper) & "') "
                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    While dr.Read
                                        If IsDBNull(dr.Item("strSubPart")) Then
                                            IAIPSubPart = ""
                                        Else
                                            IAIPSubPart = AFSAirCode & " - " & dr.Item("strSubPart")
                                        End If
                                    End While
                                    dr.Close()

                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart, IAIPSubPart)
                                End If
                                IAIPSubPart = ""
                                If Mid(DefaultText, 21, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & RTrim(Mid(DefaultText, 21, 5))

                                    SQL = "select " & _
                                    "strSubPart " & _
                                    "from " & DBNameSpace & ".APBSubPartData  " & _
                                    "where strsubpartkey = '0413" & AIRSNumber & "" & AFSAirCode & "' " & _
                                    "and (upper(strSubPart) = '" & RTrim(Mid(DefaultText, 21, 5).ToUpper) & "') "
                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    While dr.Read
                                        If IsDBNull(dr.Item("strSubPart")) Then
                                            IAIPSubPart = ""
                                        Else
                                            IAIPSubPart = AFSAirCode & " - " & dr.Item("strSubPart")
                                        End If
                                    End While
                                    dr.Close()

                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart, IAIPSubPart)
                                End If
                                IAIPSubPart = ""
                                If Mid(DefaultText, 26, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & RTrim(Mid(DefaultText, 26, 5))

                                    SQL = "select " & _
                                    "strSubPart " & _
                                    "from " & DBNameSpace & ".APBSubPartData  " & _
                                    "where strsubpartkey = '0413" & AIRSNumber & "" & AFSAirCode & "' " & _
                                    "and (upper(strSubPart) = '" & RTrim(Mid(DefaultText, 26, 5).ToUpper) & "') "
                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    While dr.Read
                                        If IsDBNull(dr.Item("strSubPart")) Then
                                            IAIPSubPart = ""
                                        Else
                                            IAIPSubPart = AFSAirCode & " - " & dr.Item("strSubPart")
                                        End If
                                    End While
                                    dr.Close()

                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart, IAIPSubPart)
                                End If
                                IAIPSubPart = ""
                                If Mid(DefaultText, 31, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & RTrim(Mid(DefaultText, 31, 5))

                                    SQL = "select " & _
                                    "strSubPart " & _
                                    "from " & DBNameSpace & ".APBSubPartData  " & _
                                    "where strsubpartkey = '0413" & AIRSNumber & "" & AFSAirCode & "' " & _
                                    "and (upper(strSubPart) = '" & RTrim(Mid(DefaultText, 31, 5).ToUpper) & "') "
                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    While dr.Read
                                        If IsDBNull(dr.Item("strSubPart")) Then
                                            IAIPSubPart = ""
                                        Else
                                            IAIPSubPart = AFSAirCode & " - " & dr.Item("strSubPart")
                                        End If
                                    End While
                                    dr.Close()

                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart, IAIPSubPart)
                                End If
                                IAIPSubPart = ""
                                If Mid(DefaultText, 36, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & RTrim(Mid(DefaultText, 36, 5))

                                    SQL = "select " & _
                                    "strSubPart " & _
                                    "from " & DBNameSpace & ".APBSubPartData  " & _
                                    "where strsubpartkey = '0413" & AIRSNumber & "" & AFSAirCode & "' " & _
                                    "and (upper(strSubPart) = '" & RTrim(Mid(DefaultText, 36, 5).ToUpper) & "') "
                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    While dr.Read
                                        If IsDBNull(dr.Item("strSubPart")) Then
                                            IAIPSubPart = ""
                                        Else
                                            IAIPSubPart = AFSAirCode & " - " & dr.Item("strSubPart")
                                        End If
                                    End While
                                    dr.Close()

                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart, IAIPSubPart)
                                End If
                                IAIPSubPart = ""
                                If Mid(DefaultText, 41, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & RTrim(Mid(DefaultText, 41, 5))

                                    SQL = "select " & _
                                    "strSubPart " & _
                                    "from " & DBNameSpace & ".APBSubPartData  " & _
                                    "where strsubpartkey = '0413" & AIRSNumber & "" & AFSAirCode & "' " & _
                                    "and (upper(strSubPart) = '" & RTrim(Mid(DefaultText, 41, 5).ToUpper) & "') "
                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    While dr.Read
                                        If IsDBNull(dr.Item("strSubPart")) Then
                                            IAIPSubPart = ""
                                        Else
                                            IAIPSubPart = AFSAirCode & " - " & dr.Item("strSubPart")
                                        End If
                                    End While
                                    dr.Close()

                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart, IAIPSubPart)
                                End If
                                IAIPSubPart = ""
                                If Mid(DefaultText, 46, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & RTrim(Mid(DefaultText, 46, 5))

                                    SQL = "select " & _
                                    "strSubPart " & _
                                    "from " & DBNameSpace & ".APBSubPartData  " & _
                                    "where strsubpartkey = '0413" & AIRSNumber & "" & AFSAirCode & "' " & _
                                    "and (upper(strSubPart) = '" & RTrim(Mid(DefaultText, 46, 5).ToUpper) & "') "
                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    While dr.Read
                                        If IsDBNull(dr.Item("strSubPart")) Then
                                            IAIPSubPart = ""
                                        Else
                                            IAIPSubPart = AFSAirCode & " - " & dr.Item("strSubPart")
                                        End If
                                    End While
                                    dr.Close()

                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart, IAIPSubPart)
                                End If
                                IAIPSubPart = ""
                                If Mid(DefaultText, 51, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & RTrim(Mid(DefaultText, 51, 5))

                                    SQL = "select " & _
                                    "strSubPart " & _
                                    "from " & DBNameSpace & ".APBSubPartData  " & _
                                    "where strsubpartkey = '0413" & AIRSNumber & "" & AFSAirCode & "' " & _
                                    "and (upper(strSubPart) = '" & RTrim(Mid(DefaultText, 51, 5).ToUpper) & "') "
                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    While dr.Read
                                        If IsDBNull(dr.Item("strSubPart")) Then
                                            IAIPSubPart = ""
                                        Else
                                            IAIPSubPart = AFSAirCode & " - " & dr.Item("strSubPart")
                                        End If
                                    End While
                                    dr.Close()

                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart, IAIPSubPart)
                                End If
                                IAIPSubPart = ""
                                If Mid(DefaultText, 56, 5) <> "" Then
                                    AFSSubPart = AFSAirCode & " - " & RTrim(Mid(DefaultText, 56, 5))

                                    SQL = "select " & _
                                    "strSubPart " & _
                                    "from " & DBNameSpace & ".APBSubPartData  " & _
                                    "where strsubpartkey = '0413" & AIRSNumber & "" & AFSAirCode & "' " & _
                                    "and (upper(strSubPart) = '" & RTrim(Mid(DefaultText, 56, 5).ToUpper) & "') "
                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    While dr.Read
                                        If IsDBNull(dr.Item("strSubPart")) Then
                                            IAIPSubPart = ""
                                        Else
                                            IAIPSubPart = AFSAirCode & " - " & dr.Item("strSubPart")
                                        End If
                                    End While
                                    dr.Close()

                                    dgvAFSData.Rows.Add(TransactionType, AIRSNumber, AFSSubPart, IAIPSubPart)
                                End If
                            Case Else
                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()
                End If

                'Load IAIP Data and compare AFS
                If rdbCompareFullIAIPdata.Checked = True Then
                    If rdbAIRSNumber.Checked = True Or rdbCounty.Checked = True Then
                        If rdbAIRSNumber.Checked = True Then
                            dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                            dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                            dgvAFSData.Columns("AIRS_Number").Width = 100
                            dgvAFSData.Columns.Add("AFS_SubPart", "AFS SubPart")
                            dgvAFSData.Columns("AFS_SubPart").Width = 200
                            dgvAFSData.Columns.Add("IAIP_SubPart", "IAIP SubPart")
                            dgvAFSData.Columns("IAIP_SubPart").Width = 200

                            SQL = "select " & _
                            "substr(strSubPartKey, 13,1) as AirCode, " & _
                            "strSubPart " & _
                            "from " & DBNameSpace & ".APBSubPartData " & _
                            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                            "and substr(strSubPartKey, 13,1) <> '0' "

                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            AIRSNumber = mtbAIRSNumber.Text
                            AFSAirCode = ""

                            While dr.Read
                                If IsDBNull(dr.Item("AirCode")) Then
                                    IAIPAirCode = ""
                                Else
                                    IAIPAirCode = dr.Item("AirCode")
                                End If
                                If IsDBNull(dr.Item("strSubPart")) Then
                                    IAIPSubPart = ""
                                Else
                                    IAIPSubPart = dr.Item("strSubPart")
                                End If
                                AFSAirCode = ""

                                Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                                Do
                                    DefaultText = reader.ReadLine
                                    TransactionType = Mid(DefaultText, 11, 3)
                                    Select Case TransactionType
                                        Case "122"
                                            AFSAIRSNumber = Mid(DefaultText, 3, 8)
                                            If AIRSNumber = AFSAIRSNumber Then
                                                If IAIPAirCode = Mid(DefaultText, 14, 1) Then
                                                    If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 16, 5)).ToUpper Then
                                                        AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 16, 5))
                                                    End If
                                                    If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 21, 5)).ToUpper Then
                                                        AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 21, 5))
                                                    End If
                                                    If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 26, 5)).ToUpper Then
                                                        AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 26, 5))
                                                    End If
                                                    If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 31, 5)).ToUpper Then
                                                        AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 31, 5))
                                                    End If
                                                    If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 36, 5)).ToUpper Then
                                                        AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 36, 5))
                                                    End If
                                                    If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 41, 5)).ToUpper Then
                                                        AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 41, 5))
                                                    End If
                                                    If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 46, 5)).ToUpper Then
                                                        AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 46, 5))
                                                    End If
                                                    If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 51, 5)).ToUpper Then
                                                        AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 51, 5))
                                                    End If
                                                    If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 56, 5)).ToUpper Then
                                                        AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 56, 5))
                                                    End If
                                                End If
                                            End If
                                        Case Else
                                    End Select
                                Loop Until reader.Peek = -1
                                reader.Close()

                                IAIPSubPart = IAIPAirCode & " - " & IAIPSubPart
                                dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, IAIPSubPart)

                            End While
                            dr.Close()

                            Dim reader2 As StreamReader = New StreamReader(txtAFSFile.Text)
                            Do
                                DefaultText = reader2.ReadLine
                                AIRSNumber = Mid(DefaultText, 3, 8)
                                TransactionType = Mid(DefaultText, 11, 3)
                                Select Case TransactionType
                                    Case "122"
                                        If AIRSNumber = mtbAIRSNumber.Text Then
                                            AFSAirCode = Mid(DefaultText, 14, 1)
                                            AFSSubPart = Mid(DefaultText, 16, 5)
                                            If RTrim(AFSSubPart) <> "" Then
                                                AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                temp = "0"
                                                For Each row As DataGridViewRow In dgvAFSData.Rows
                                                    If Not row.IsNewRow Then
                                                        If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = mtbAIRSNumber.Text Then
                                                            temp = "1"
                                                        End If
                                                    End If
                                                Next
                                                If temp = "0" Then
                                                    dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                End If
                                            End If

                                            AFSSubPart = Mid(DefaultText, 21, 5)
                                            If RTrim(AFSSubPart) <> "" Then
                                                AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                temp = "0"
                                                For Each row As DataGridViewRow In dgvAFSData.Rows
                                                    If Not row.IsNewRow Then
                                                        If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = mtbAIRSNumber.Text Then
                                                            temp = "1"
                                                        End If
                                                    End If
                                                Next
                                                If temp = "0" Then
                                                    dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                End If
                                            End If

                                            AFSSubPart = Mid(DefaultText, 26, 5)
                                            If RTrim(AFSSubPart) <> "" Then
                                                AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                temp = "0"
                                                For Each row As DataGridViewRow In dgvAFSData.Rows
                                                    If Not row.IsNewRow Then
                                                        If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = mtbAIRSNumber.Text Then
                                                            temp = "1"
                                                        End If
                                                    End If
                                                Next
                                                If temp = "0" Then
                                                    dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                End If
                                            End If

                                            AFSSubPart = Mid(DefaultText, 31, 5)
                                            If RTrim(AFSSubPart) <> "" Then
                                                AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                temp = "0"
                                                For Each row As DataGridViewRow In dgvAFSData.Rows
                                                    If Not row.IsNewRow Then
                                                        If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = mtbAIRSNumber.Text Then
                                                            temp = "1"
                                                        End If
                                                    End If
                                                Next
                                                If temp = "0" Then
                                                    dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                End If
                                            End If

                                            AFSSubPart = Mid(DefaultText, 36, 5)
                                            If RTrim(AFSSubPart) <> "" Then
                                                AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                temp = "0"
                                                For Each row As DataGridViewRow In dgvAFSData.Rows
                                                    If Not row.IsNewRow Then
                                                        If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = mtbAIRSNumber.Text Then
                                                            temp = "1"
                                                        End If
                                                    End If
                                                Next
                                                If temp = "0" Then
                                                    dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                End If
                                            End If

                                            AFSSubPart = Mid(DefaultText, 41, 5)
                                            If RTrim(AFSSubPart) <> "" Then
                                                AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                temp = "0"
                                                For Each row As DataGridViewRow In dgvAFSData.Rows
                                                    If Not row.IsNewRow Then
                                                        If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = mtbAIRSNumber.Text Then
                                                            temp = "1"
                                                        End If
                                                    End If
                                                Next
                                                If temp = "0" Then
                                                    dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                End If
                                            End If

                                            AFSSubPart = Mid(DefaultText, 46, 5)
                                            If RTrim(AFSSubPart) <> "" Then
                                                AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                temp = "0"
                                                For Each row As DataGridViewRow In dgvAFSData.Rows
                                                    If Not row.IsNewRow Then
                                                        If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = mtbAIRSNumber.Text Then
                                                            temp = "1"
                                                        End If
                                                    End If
                                                Next
                                                If temp = "0" Then
                                                    dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                End If
                                            End If

                                            AFSSubPart = Mid(DefaultText, 51, 5)
                                            If RTrim(AFSSubPart) <> "" Then
                                                AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                temp = "0"
                                                For Each row As DataGridViewRow In dgvAFSData.Rows
                                                    If Not row.IsNewRow Then
                                                        If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = mtbAIRSNumber.Text Then
                                                            temp = "1"
                                                        End If
                                                    End If
                                                Next
                                                If temp = "0" Then
                                                    dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                End If
                                            End If

                                            AFSSubPart = Mid(DefaultText, 56, 5)
                                            If RTrim(AFSSubPart) <> "" Then
                                                AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                temp = "0"
                                                For Each row As DataGridViewRow In dgvAFSData.Rows
                                                    If Not row.IsNewRow Then
                                                        If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = mtbAIRSNumber.Text Then
                                                            temp = "1"
                                                        End If
                                                    End If
                                                Next
                                                If temp = "0" Then
                                                    dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                End If
                                            End If
                                        End If
                                End Select
                            Loop Until reader2.Peek = -1
                        Else
                            Dim county As String
                            If cboCounty.Text <> " " Or cboCounty.Text <> "" Then
                                county = cboCounty.SelectedValue

                                dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                                dgvAFSData.Columns.Add("AIRS_Number", "AIRS Number")
                                dgvAFSData.Columns("AIRS_Number").Width = 100
                                dgvAFSData.Columns.Add("AFS_SubPart", "AFS SubPart")
                                dgvAFSData.Columns("AFS_SubPart").Width = 200
                                dgvAFSData.Columns.Add("IAIP_SubPart", "IAIP SubPart")
                                dgvAFSData.Columns("IAIP_SubPart").Width = 200

                                SQL = "select " & _
                                "substr(strAIRSNumber, 5) as AIRSNumber, " & _
                                "substr(strSubPartKey, 13,1) as AirCode, " & _
                                "strSubPart " & _
                                "from " & DBNameSpace & ".APBSubPartData " & _
                                "where strAIRSNumber like '041312100129' " & _
                                "and substr(strSubPartKey, 13,1) <> '0' "

                                SQL = "select " & _
                                "substr(strAIRSNumber, 5) as AIRSNumber, " & _
                                "substr(strSubPartKey, 13,1) as AirCode, " & _
                                "strSubPart " & _
                                "from " & DBNameSpace & ".APBSubPartData " & _
                                "where strAIRSNumber like '0413" & county & "%' " & _
                                "and substr(strSubPartKey, 13,1) <> '0' "

                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                While dr.Read
                                    If IsDBNull(dr.Item("AIRSNumber")) Then
                                        IAIPAIRSNumber = ""
                                    Else
                                        IAIPAIRSNumber = dr.Item("AIRSNumber")
                                    End If
                                    If IsDBNull(dr.Item("AirCode")) Then
                                        IAIPAirCode = ""
                                    Else
                                        IAIPAirCode = dr.Item("AirCode")
                                    End If
                                    If IsDBNull(dr.Item("strSubPart")) Then
                                        IAIPSubPart = ""
                                    Else
                                        IAIPSubPart = dr.Item("strSubPart")
                                    End If
                                    AFSAirCode = ""

                                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                                    Do
                                        DefaultText = reader.ReadLine
                                        TransactionType = Mid(DefaultText, 11, 3)
                                        Select Case TransactionType
                                            Case "122"
                                                AFSAIRSNumber = Mid(DefaultText, 3, 8)
                                                If IAIPAIRSNumber = AFSAIRSNumber Then
                                                    If IAIPAirCode = Mid(DefaultText, 14, 1) Then
                                                        If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 16, 5)).ToUpper Then
                                                            AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 16, 5))
                                                        End If
                                                        If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 21, 5)).ToUpper Then
                                                            AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 21, 5))
                                                        End If
                                                        If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 26, 5)).ToUpper Then
                                                            AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 26, 5))
                                                        End If
                                                        If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 31, 5)).ToUpper Then
                                                            AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 31, 5))
                                                        End If
                                                        If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 36, 5)).ToUpper Then
                                                            AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 36, 5))
                                                        End If
                                                        If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 41, 5)).ToUpper Then
                                                            AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 41, 5))
                                                        End If
                                                        If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 46, 5)).ToUpper Then
                                                            AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 46, 5))
                                                        End If
                                                        If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 51, 5)).ToUpper Then
                                                            AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 51, 5))
                                                        End If
                                                        If IAIPSubPart.ToUpper = RTrim(Mid(DefaultText, 56, 5)).ToUpper Then
                                                            AFSSubPart = Mid(DefaultText, 14, 1) & " - " & RTrim(Mid(DefaultText, 56, 5))
                                                        End If
                                                    End If
                                                End If
                                            Case Else
                                        End Select
                                    Loop Until reader.Peek = -1
                                    reader.Close()

                                    IAIPSubPart = IAIPAirCode & " - " & IAIPSubPart
                                    dgvAFSData.Rows.Add("122", IAIPAIRSNumber, AFSSubPart, IAIPSubPart)
                                    AFSSubPart = ""

                                End While
                                dr.Close()


                                Dim reader2 As StreamReader = New StreamReader(txtAFSFile.Text)
                                Do
                                    DefaultText = reader2.ReadLine
                                    AIRSNumber = Mid(DefaultText, 3, 8)
                                    TransactionType = Mid(DefaultText, 11, 3)
                                    Select Case TransactionType
                                        Case "122"
                                            If AIRSNumber <> "" Then
                                                AFSAirCode = Mid(DefaultText, 14, 1)
                                                AFSSubPart = Mid(DefaultText, 16, 5)
                                                If RTrim(AFSSubPart) <> "" Then
                                                    AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                    temp = "0"
                                                    For Each row As DataGridViewRow In dgvAFSData.Rows
                                                        If Not row.IsNewRow Then
                                                            If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = AIRSNumber Then
                                                                temp = "1"
                                                            End If
                                                        End If
                                                    Next
                                                    If temp = "0" Then
                                                        dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                    End If
                                                End If

                                                AFSSubPart = Mid(DefaultText, 21, 5)
                                                If RTrim(AFSSubPart) <> "" Then
                                                    AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                    temp = "0"
                                                    For Each row As DataGridViewRow In dgvAFSData.Rows
                                                        If Not row.IsNewRow Then
                                                            If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = AIRSNumber Then
                                                                temp = "1"
                                                            End If
                                                        End If
                                                    Next
                                                    If temp = "0" Then
                                                        dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                    End If
                                                End If

                                                AFSSubPart = Mid(DefaultText, 26, 5)
                                                If RTrim(AFSSubPart) <> "" Then
                                                    AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                    temp = "0"
                                                    For Each row As DataGridViewRow In dgvAFSData.Rows
                                                        If Not row.IsNewRow Then
                                                            If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = AIRSNumber Then
                                                                temp = "1"
                                                            End If
                                                        End If
                                                    Next
                                                    If temp = "0" Then
                                                        dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                    End If
                                                End If

                                                AFSSubPart = Mid(DefaultText, 31, 5)
                                                If RTrim(AFSSubPart) <> "" Then
                                                    AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                    temp = "0"
                                                    For Each row As DataGridViewRow In dgvAFSData.Rows
                                                        If Not row.IsNewRow Then
                                                            If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = AIRSNumber Then
                                                                temp = "1"
                                                            End If
                                                        End If
                                                    Next
                                                    If temp = "0" Then
                                                        dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                    End If
                                                End If

                                                AFSSubPart = Mid(DefaultText, 36, 5)
                                                If RTrim(AFSSubPart) <> "" Then
                                                    AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                    temp = "0"
                                                    For Each row As DataGridViewRow In dgvAFSData.Rows
                                                        If Not row.IsNewRow Then
                                                            If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = AIRSNumber Then
                                                                temp = "1"
                                                            End If
                                                        End If
                                                    Next
                                                    If temp = "0" Then
                                                        dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                    End If
                                                End If

                                                AFSSubPart = Mid(DefaultText, 41, 5)
                                                If RTrim(AFSSubPart) <> "" Then
                                                    AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                    temp = "0"
                                                    For Each row As DataGridViewRow In dgvAFSData.Rows
                                                        If Not row.IsNewRow Then
                                                            If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = AIRSNumber Then
                                                                temp = "1"
                                                            End If
                                                        End If
                                                    Next
                                                    If temp = "0" Then
                                                        dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                    End If
                                                End If

                                                AFSSubPart = Mid(DefaultText, 46, 5)
                                                If RTrim(AFSSubPart) <> "" Then
                                                    AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                    temp = "0"
                                                    For Each row As DataGridViewRow In dgvAFSData.Rows
                                                        If Not row.IsNewRow Then
                                                            If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = AIRSNumber Then
                                                                temp = "1"
                                                            End If
                                                        End If
                                                    Next
                                                    If temp = "0" Then
                                                        dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                    End If
                                                End If

                                                AFSSubPart = Mid(DefaultText, 51, 5)
                                                If RTrim(AFSSubPart) <> "" Then
                                                    AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                    temp = "0"
                                                    For Each row As DataGridViewRow In dgvAFSData.Rows
                                                        If Not row.IsNewRow Then
                                                            If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = AIRSNumber Then
                                                                temp = "1"
                                                            End If
                                                        End If
                                                    Next
                                                    If temp = "0" Then
                                                        dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                    End If
                                                End If

                                                AFSSubPart = Mid(DefaultText, 56, 5)
                                                If RTrim(AFSSubPart) <> "" Then
                                                    AFSSubPart = AFSAirCode & " - " & AFSSubPart
                                                    temp = "0"
                                                    For Each row As DataGridViewRow In dgvAFSData.Rows
                                                        If Not row.IsNewRow Then
                                                            If row.Cells(2).Value.ToString.ToUpper = RTrim(AFSSubPart).ToUpper And row.Cells(1).Value.ToString = AIRSNumber Then
                                                                temp = "1"
                                                            End If
                                                        End If
                                                    Next
                                                    If temp = "0" Then
                                                        dgvAFSData.Rows.Add("122", AIRSNumber, AFSSubPart, "")
                                                    End If
                                                End If
                                            End If
                                    End Select
                                Loop Until reader2.Peek = -1







                                'Dim reader2 As StreamReader = New StreamReader(txtAFSFile.Text)
                                'Do
                                '    DefaultText = reader2.ReadLine
                                '    AIRSNumber = Mid(DefaultText, 3, 8)
                                '    TransactionType = Mid(DefaultText, 11, 3)
                                '    AFSAirCode = ""
                                '    Select Case TransactionType
                                '        Case "122"
                                '            AFSAirCode = Mid(DefaultText, 14, 1) & " - " & Mid(DefaultText, 15, 1)
                                '    End Select
                                '    temp = "0"
                                '    If AFSAirCode <> "" And AIRSNumber <> "" Then
                                '        For Each row As DataGridViewRow In dgvAFSData.Rows
                                '            If Not row.IsNewRow Then
                                '                If row.Cells(1).Value.ToString = AIRSNumber And row.Cells(2).Value.ToString = AFSAirCode Then
                                '                    temp = "1"
                                '                End If
                                '            End If
                                '        Next
                                '        If temp = "0" Then
                                '            dgvAFSData.Rows.Add("121", AIRSNumber, AFSAirCode, "")
                                '        End If
                                '    End If
                                'Loop Until reader2.Peek = -1
                            Else
                                MessageBox.Show("Select a county from the dropdown.")
                                Exit Sub
                            End If

                        End If

                    Else
                        MessageBox.Show("Select either County or AIRS #.")
                        Exit Sub
                    End If
                End If
                txtAFSDataCount.Text = dgvAFSData.RowCount.ToString
                Exit Sub
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadAFSData2()
        Try
            Dim DefaultText As String = ""
            Dim AIRSNumber As String = ""
            Dim IAIPAIRSNumber As String = ""
            Dim TransactionType As String = ""
            Dim FacilityName As String = ""
            Dim IAIPFacilityName As String = ""
            Dim FacilityAddress As String = ""
            Dim IAIPFacilityAddress As String = ""
            Dim FacilityCity As String = ""
            Dim IAIPFacilityCity As String = ""
            Dim FacilityZipCode As String = ""
            Dim IAIPFacilityZipCode As String = ""
            Dim FacilitySIC As String = ""
            Dim IAIPFacilitySIC As String = ""
            Dim FacilityNAICS As String = ""
            Dim IAIPFacilityNAICS As String = ""
            Dim FacilityContactPerson As String = ""
            Dim IAIPFacilityContactPerson As String = ""
            Dim FacilityContactNumber As String = ""
            Dim IAIPFacilityContactNumber As String = ""
            Dim FacilityDescription As String = ""
            Dim IAIPFacilityDescription As String = ""
            Dim AirProgramCode As String = ""
            Dim IAIPAirProgramCode As String = ""
            Dim ProgramStatus As String = ""
            Dim IAIPProgramStatus As String = ""
            Dim Pollutant As String = ""
            Dim IAIPPollutant As String = ""
            Dim SubPart1 As String = ""
            Dim IAIPSubPart As String = ""
            Dim SubPart2 As String = ""
            Dim SubPart3 As String = ""
            Dim SubPart4 As String = ""
            Dim SubPart5 As String = ""
            Dim SubPart6 As String = ""
            Dim SubPart7 As String = ""
            Dim SubPart8 As String = ""
            Dim SubPart9 As String = ""
            Dim FacilityClassification As String = ""
            Dim IAIPFacilityClassification As String = ""
            Dim ComplianceStatus As String = ""
            Dim IAIPComplianceStatus As String = ""
            Dim AttainmentStatus As String = ""
            Dim IAIPAttainmentStatus As String = ""
            Dim SICCode As String = ""
            Dim IAIPSICCode As String = ""
            Dim ActionNumber As String = ""
            Dim IAIPActionNumber As String = ""
            Dim ActionType As String = ""
            Dim IAIPActionType As String = ""
            Dim DateScheduled As String = ""
            Dim IAIPDateScheduled As String = ""
            Dim DateAcheived As String = ""
            Dim IAIPDateAcheived As String = ""
            Dim ResultCode As String = ""
            Dim IAIPResultCode As String = ""
            Dim PenaltyAmount As String = ""
            Dim IAIPPenaltyAmount As String = ""
            Dim Pollutant161 As String = ""
            Dim IAIPPollutant161 As String = ""
            Dim KeyActionNumber As String = ""
            Dim IAIPKeyActionNumber As String = ""
            Dim ActionNumber163 As String = ""
            Dim IAIPActionNumber163 As String = ""
            Dim ActionNumber164 As String = ""
            Dim IAIPActionNumber164 As String = ""
            Dim KeyActionNumber164 As String = ""
            Dim IAIPKeyActionNumber164 As String = ""
            Dim ViolationType As String = ""
            Dim IAIPViolationType As String = ""
            Dim Pollutant163 As String = ""
            Dim IAIPPollutant163 As String = ""
            Dim CMSStatus As String = ""
            Dim IAIPCMSStatus As String = ""
            Dim ErrorCompare As String = ""
            Dim intCounter As Integer = 0

            dgvAFSData.Columns.Clear()
            If File.Exists(txtAFSFile.Text) Then
                dgvAFSData.Rows.Clear()

                If rdbCompareFullIAIPdata.Checked = True Then
                    dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                    dgvAFSData.Columns.Add("Errors", "Error Status")
                    dgvAFSData.Columns.Add("AFSAIRSNumber", "AFS AIRS Number")
                    dgvAFSData.Columns.Add("AFSFacilityName", "AFS Facility Name")
                    dgvAFSData.Columns("AFSFacilityName").Width = 200
                    dgvAFSData.Columns.Add("IAIPAIRSNumber", "IAIP AIRS Number")
                    dgvAFSData.Columns.Add("IAIPFacilityName", "IAIP Facility Name")
                    dgvAFSData.Columns("IAIPFacilityName").Width = 200

                    If chb101Card.Checked = True Then
                    Else
                        dgvAFSData.Columns("AFSFacilityName").Visible = False
                        dgvAFSData.Columns("IAIPFacilityName").Visible = False
                    End If

                    Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                    Do
                        DefaultText = reader.ReadLine
                        AIRSNumber = Mid(DefaultText, 3, 8)
                        TransactionType = Mid(DefaultText, 11, 3)
                        ErrorCompare = ""

                        Select Case TransactionType
                            Case "101"
                                If chb101Card.Checked = True Then
                                    FacilityName = Mid(DefaultText, 16, 40)
                                    IAIPAIRSNumber = AIRSNumber
                                    IAIPFacilityName = "NULL"

                                    SQL = "Select upper(strFacilityName) as strFacilityName " & _
                                    "from " & DBNameSpace & ".APBFacilityInformation " & _
                                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    While dr.Read
                                        If IsDBNull(dr.Item("strFacilityName")) Then
                                            IAIPFacilityName = "NULL"
                                        Else
                                            IAIPFacilityName = dr.Item("strFacilityName")
                                        End If
                                    End While
                                    dr.Close()


                                    If Replace(Mid(IAIPFacilityName, 1, 40), " ", "") = Replace(FacilityName, " ", "") Then
                                        IAIPFacilityName = ""
                                    Else
                                        If IAIPFacilityName = "NULL" Then
                                            IAIPAIRSNumber = "---"
                                            IAIPFacilityName = "---"
                                            ErrorCompare = "No IAIP Value"
                                        Else
                                            ErrorCompare = "Mismatch Name"
                                        End If
                                    End If

                                    dgvAFSData.Rows.Add(TransactionType, ErrorCompare, AIRSNumber, FacilityName, IAIPAIRSNumber, IAIPFacilityName)
                                End If

                        End Select

                    Loop Until reader.Peek = -1
                    reader.Close()

                    If chb101Card.Checked = True Then
                        SQL = "Select " & _
                        "substr(strAIRSNumber, 5) as strAIRSNumber, " & _
                        "upper(strFacilityName) as strFacilityName " & _
                        "from " & DBNameSpace & ".APBFacilityInformation " & _
                        "order by strAIRSNumber "

                        ds = New DataSet
                        da = New OracleDataAdapter(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        da.Fill(ds, "FacilityCheck")

                        For Each drDSRow In ds.Tables("FacilityCheck").Select()
                            If IsDBNull(drDSRow("strAIRSNumber")) Then
                                IAIPAIRSNumber = ""
                            Else
                                IAIPAIRSNumber = drDSRow("strAIRSNumber")
                            End If
                            If IsDBNull(drDSRow("strFacilityName")) Then
                                IAIPFacilityName = ""
                            Else
                                IAIPFacilityName = drDSRow("strFacilityName")
                            End If
                            intCounter = 0
                            temp = "False"
                            For Each row As DataGridViewRow In dgvAFSData.Rows
                                If dgvAFSData.Rows(intCounter).Cells(2).Value = IAIPAIRSNumber Then
                                    temp = "True"
                                Else

                                End If
                                intCounter += 1
                            Next row
                            If temp <> "True" Then
                                TransactionType = "101"
                                ErrorCompare = "No AFS value"
                                AIRSNumber = "---"
                                FacilityName = "---"
                                dgvAFSData.Rows.Add(TransactionType, ErrorCompare, AIRSNumber, FacilityName, IAIPAIRSNumber, IAIPFacilityName)
                            End If
                        Next
                    End If


                Else
                    If rdbCompareAFSDataOnly.Checked = True Then
                        dgvAFSData.Columns.Add("AIRSNumber", "AIRS Number")
                        dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                        dgvAFSData.Columns.Add("FacilityName", "AFS Facility Name")
                        dgvAFSData.Columns("FacilityName").Width = 200
                        dgvAFSData.Columns.Add("IAIPFacilityName", "IAIP Facility Name")
                        dgvAFSData.Columns("IAIPFacilityName").Width = 200
                        dgvAFSData.Columns.Add("FacilityAddress", "AFS Facility Address")
                        dgvAFSData.Columns("FacilityAddress").Width = 200
                        dgvAFSData.Columns.Add("IAIPFacilityAddress", "IAIP Facility Address")
                        dgvAFSData.Columns("IAIPFacilityAddress").Width = 200
                        dgvAFSData.Columns.Add("City", "City")
                        dgvAFSData.Columns.Add("IAIPCity", "IAIP City")
                        dgvAFSData.Columns.Add("ZipCode", "Zip Code")
                        dgvAFSData.Columns.Add("IAIPZipCode", "IAIP Zip Code")
                        dgvAFSData.Columns.Add("SICCode", "SIC Code")
                        dgvAFSData.Columns.Add("IAIPSICCode", "IAIP SIC Code")
                        dgvAFSData.Columns.Add("ContactPerson", "Contact Person")
                        dgvAFSData.Columns.Add("IAIPContactPerson", "IAIP Contact Person")
                        dgvAFSData.Columns.Add("ContactNumber", "Contact Number")
                        dgvAFSData.Columns.Add("IAIPContactNumber", "IAIP Contact Number")
                        dgvAFSData.Columns.Add("FacilityDescription", "Facility Description")
                        dgvAFSData.Columns("FacilityDescription").Width = 200
                        dgvAFSData.Columns.Add("IAIPFacilityDescription", "IAIP Facility Description")
                        dgvAFSData.Columns("IAIPFacilityDescription").Width = 200
                        dgvAFSData.Columns.Add("AirProgramCode", "Air Program Code")
                        dgvAFSData.Columns.Add("IAIPAirProgramCode", "IAIP Air Program Code")
                        dgvAFSData.Columns.Add("ProgramCodeStatus", "Program Code Status")
                        dgvAFSData.Columns.Add("IAIPProgramCodeStatus", "IAIP Program Code Status")
                        dgvAFSData.Columns.Add("IAIPSubPart", "IAIP Sub Part 1")
                        dgvAFSData.Columns.Add("SubPart1", "Sub Part 1")
                        dgvAFSData.Columns.Add("SubPart2", "Sub Part 2")
                        dgvAFSData.Columns.Add("SubPart3", "Sub Part 3")
                        dgvAFSData.Columns.Add("SubPart4", "Sub Part 4")
                        dgvAFSData.Columns.Add("SubPart5", "Sub Part 5")
                        dgvAFSData.Columns.Add("SubPart6", "Sub Part 6")
                        dgvAFSData.Columns.Add("SubPart7", "Sub Part 7")
                        dgvAFSData.Columns.Add("SubPart8", "Sub Part 8")
                        dgvAFSData.Columns.Add("SubPart9", "Sub Part 9")
                        dgvAFSData.Columns.Add("Pollutant", "Pollutant")
                        dgvAFSData.Columns.Add("IAIPPollutant", "IAIP Pollutant")
                        dgvAFSData.Columns.Add("Class", "Classification")
                        dgvAFSData.Columns.Add("IAIPClass", "IAIP Classification")
                        dgvAFSData.Columns.Add("ComplianceStauts", "Compliance Status")
                        dgvAFSData.Columns.Add("IAIPComplianceStauts", "IAIP Compliance Status")
                        dgvAFSData.Columns.Add("AttainmentStatus", "Attainment Status")
                        dgvAFSData.Columns.Add("IAIPAttainmentStatus", "IAIP Attainment Status")
                        dgvAFSData.Columns.Add("SICCode2", "SIC Code")
                        dgvAFSData.Columns.Add("IAIPSICCode2", "IAIP SIC Code")
                        dgvAFSData.Columns.Add("ActionNumber", "Action Number")
                        dgvAFSData.Columns.Add("IAIPActionNumber", "IAIP Action Number")
                        dgvAFSData.Columns.Add("ActionType", "Action Type")
                        dgvAFSData.Columns.Add("IAIPActionType", "IAIP Action Type")
                        dgvAFSData.Columns.Add("DateScheduled", "Date Scheduled")
                        dgvAFSData.Columns.Add("IAIPDateScheduled", "IAIP Date Scheduled")
                        dgvAFSData.Columns.Add("DateAcheived", "Date Acheived")
                        dgvAFSData.Columns.Add("IAIPDateAcheived", "IAIP Date Acheived")
                        dgvAFSData.Columns.Add("ResultCode", "Result Code")
                        dgvAFSData.Columns.Add("IAIPResultCode", "IAIP Result Code")
                        dgvAFSData.Columns.Add("PenaltyAmount", "Penalty Amount")
                        dgvAFSData.Columns.Add("IAIPPenaltyAmount", "IAIP Penalty Amount")
                        dgvAFSData.Columns.Add("Pollutant161", "Pollutant")
                        dgvAFSData.Columns.Add("IAIPPollutant161", "IAIP Pollutant")
                        dgvAFSData.Columns.Add("KeyActionNumber", "Key Action Number")
                        dgvAFSData.Columns.Add("IAIPKeyActionNumber", "IAIP Key Action Number")
                        dgvAFSData.Columns.Add("ActionNumber163", "Action Number")
                        dgvAFSData.Columns.Add("IAIPActionNumber163", "IAIP Action Number")
                        dgvAFSData.Columns.Add("Pollutant163", "Pollutant")
                        dgvAFSData.Columns.Add("IAIPPollutant163", "IAIP Pollutant")

                        dgvAFSData.Columns.Add("ActionNumber164", "Action Number")
                        dgvAFSData.Columns.Add("IAIPActionNumber164", "IAIP Action Number")
                        dgvAFSData.Columns.Add("KeyActionNumber164", "Key Action Number")
                        dgvAFSData.Columns.Add("IAIPKeyActionNumber164", "IAIP Key Action Number")
                        dgvAFSData.Columns.Add("ViolationType", "Violation Type")
                        dgvAFSData.Columns.Add("IAIPViolationType", "IAIP Violation Type")
                        dgvAFSData.Columns.Add("CMSStatus", "CMS Status")
                        dgvAFSData.Columns.Add("IAIPCMSStatus", "IAIP CMS Status")

                        If chb101Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("FacilityName").Visible = False
                            dgvAFSData.Columns("IAIPFacilityName").Visible = False
                        End If
                        If chb102Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("FacilityAddress").Visible = False
                            dgvAFSData.Columns("IAIPFacilityAddress").Visible = False
                        End If
                        If chb103Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("City").Visible = False
                            dgvAFSData.Columns("ZipCode").Visible = False
                            dgvAFSData.Columns("SICCode").Visible = False
                            dgvAFSData.Columns("IAIPCity").Visible = False
                            dgvAFSData.Columns("IAIPZipCode").Visible = False
                            dgvAFSData.Columns("IAIPSICCode").Visible = False
                        End If
                        If chb105Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("ContactPerson").Visible = False
                            dgvAFSData.Columns("ContactNumber").Visible = False
                            dgvAFSData.Columns("FacilityDescription").Visible = False
                            dgvAFSData.Columns("IAIPContactPerson").Visible = False
                            dgvAFSData.Columns("IAIPContactNumber").Visible = False
                            dgvAFSData.Columns("IAIPFacilityDescription").Visible = False
                        End If
                        If chb121Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("AirProgramCode").Visible = False
                            dgvAFSData.Columns("ProgramCodeStatus").Visible = False
                            dgvAFSData.Columns("IAIPAirProgramCode").Visible = False
                            dgvAFSData.Columns("IAIPProgramCodeStatus").Visible = False
                        End If
                        If chb122Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("SubPart1").Visible = False
                            dgvAFSData.Columns("SubPart2").Visible = False
                            dgvAFSData.Columns("SubPart3").Visible = False
                            dgvAFSData.Columns("SubPart4").Visible = False
                            dgvAFSData.Columns("SubPart5").Visible = False
                            dgvAFSData.Columns("SubPart6").Visible = False
                            dgvAFSData.Columns("SubPart7").Visible = False
                            dgvAFSData.Columns("SubPart8").Visible = False
                            dgvAFSData.Columns("SubPart9").Visible = False
                            dgvAFSData.Columns("IAIPSubPart").Visible = False
                        End If
                        If chb131Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("Pollutant").Visible = False
                            dgvAFSData.Columns("Class").Visible = False
                            dgvAFSData.Columns("ComplianceStauts").Visible = False
                            dgvAFSData.Columns("AttainmentStatus").Visible = False
                            dgvAFSData.Columns("SICCode2").Visible = False
                            dgvAFSData.Columns("IAIPPollutant").Visible = False
                            dgvAFSData.Columns("IAIPClass").Visible = False
                            dgvAFSData.Columns("IAIPComplianceStauts").Visible = False
                            dgvAFSData.Columns("IAIPAttainmentStatus").Visible = False
                            dgvAFSData.Columns("IAIPSICCode2").Visible = False
                        End If
                        If chb161Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("ActionNumber").Visible = False
                            dgvAFSData.Columns("ActionType").Visible = False
                            dgvAFSData.Columns("DateScheduled").Visible = False
                            dgvAFSData.Columns("DateAcheived").Visible = False
                            dgvAFSData.Columns("ResultCode").Visible = False
                            dgvAFSData.Columns("PenaltyAmount").Visible = False
                            dgvAFSData.Columns("Pollutant161").Visible = False
                            dgvAFSData.Columns("IAIPActionNumber").Visible = False
                            dgvAFSData.Columns("IAIPActionType").Visible = False
                            dgvAFSData.Columns("IAIPDateScheduled").Visible = False
                            dgvAFSData.Columns("IAIPDateAcheived").Visible = False
                            dgvAFSData.Columns("IAIPResultCode").Visible = False
                            dgvAFSData.Columns("IAIPPenaltyAmount").Visible = False
                            dgvAFSData.Columns("IAIPPollutant161").Visible = False
                        End If
                        If chb163Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("KeyActionNumber").Visible = False
                            dgvAFSData.Columns("IAIPKeyActionNumber").Visible = False
                            dgvAFSData.Columns("ActionNumber163").Visible = False
                            dgvAFSData.Columns("IAIPActionNumber163").Visible = False
                            dgvAFSData.Columns("Pollutant163").Visible = False
                            dgvAFSData.Columns("IAIPPollutant163").Visible = False
                        End If
                        If chb164Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("ActionNumber164").Visible = False
                            dgvAFSData.Columns("IAIPActionNumber164").Visible = False
                            dgvAFSData.Columns("KeyActionNumber164").Visible = False
                            dgvAFSData.Columns("IAIPKeyActionNumber164").Visible = False
                            dgvAFSData.Columns("ViolationType").Visible = False
                            dgvAFSData.Columns("IAIPViolationType").Visible = False
                        End If
                        If chb181Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("CMSStatus").Visible = False
                            dgvAFSData.Columns("IAIPCMSStatus").Visible = False
                        End If

                        Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                        Do
                            DefaultText = reader.ReadLine
                            AIRSNumber = Mid(DefaultText, 3, 8)
                            TransactionType = Mid(DefaultText, 11, 3)
                            Select Case TransactionType
                                Case "101"
                                    If chb101Card.Checked = True Then
                                        FacilityName = Mid(DefaultText, 16, 40)
                                        IAIPFacilityName = "NULL"

                                        SQL = "Select upper(strFacilityName) as strFacilityName " & _
                                        "from " & DBNameSpace & ".APBFacilityInformation " & _
                                        "where strAIRSNumber = '0413" & AIRSNumber & "' "
                                        cmd = New OracleCommand(SQL, Conn)
                                        If Conn.State = ConnectionState.Closed Then
                                            Conn.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        While dr.Read
                                            If IsDBNull(dr.Item("strFacilityName")) Then
                                                IAIPFacilityName = "NULL"
                                            Else
                                                IAIPFacilityName = dr.Item("strFacilityName")
                                            End If
                                        End While
                                        dr.Close()
                                        If Replace(Mid(IAIPFacilityName, 1, 40), " ", "") = Replace(FacilityName, " ", "") Then
                                            IAIPFacilityName = ""
                                        End If

                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, FacilityName, IAIPFacilityName)
                                    End If
                                Case "102"
                                    If chb102Card.Checked = True Then
                                        FacilityAddress = Mid(DefaultText, 16, 30)
                                        IAIPFacilityAddress = "NULL"

                                        SQL = "Select upper(strFacilityStreet1) as strFacilityStreet1 " & _
                                        "from " & DBNameSpace & ".APBFacilityInformation " & _
                                        "where strAIRSNumber = '0413" & AIRSNumber & "' "
                                        cmd = New OracleCommand(SQL, Conn)
                                        If Conn.State = ConnectionState.Closed Then
                                            Conn.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        While dr.Read
                                            If IsDBNull(dr.Item("strFacilityStreet1")) Then
                                                IAIPFacilityAddress = "NULL"
                                            Else
                                                IAIPFacilityAddress = dr.Item("strFacilityStreet1")
                                            End If
                                        End While
                                        dr.Close()
                                        If Mid(IAIPFacilityAddress, 1, 30) = FacilityAddress Then
                                            IAIPFacilityAddress = ""
                                        End If

                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", FacilityAddress, IAIPFacilityAddress)
                                    End If
                                Case "103"
                                    If chb103Card.Checked = True Then
                                        FacilityCity = Mid(DefaultText, 16, 30)
                                        FacilityZipCode = Mid(DefaultText, 46, 9)
                                        FacilitySIC = Mid(DefaultText, 55, 4)
                                        IAIPFacilityCity = "NULL"
                                        IAIPFacilityZipCode = "NULL"
                                        IAIPFacilitySIC = "NULL"

                                        SQL = "Select " & _
                                        "Upper(strFacilityCity) as strFacilityCity, " & _
                                        "Upper(strFacilityZipCode) as strFacilityZipCode, " & _
                                        "upper(strSICCode) as strSICCode " & _
                                        "from " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".APBHeaderData " & _
                                        "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                        "and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = '0413" & AIRSNumber & "' "
                                        cmd = New OracleCommand(SQL, Conn)
                                        If Conn.State = ConnectionState.Closed Then
                                            Conn.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        While dr.Read
                                            If IsDBNull(dr.Item("strFacilityCity")) Then
                                                IAIPFacilityCity = "NULL"
                                            Else
                                                IAIPFacilityCity = dr.Item("strFacilityCity")
                                            End If
                                            If IsDBNull(dr.Item("strFacilityZipCode")) Then
                                                IAIPFacilityZipCode = "NULL"
                                            Else
                                                IAIPFacilityZipCode = dr.Item("strFacilityZipCode")
                                            End If
                                            If IsDBNull(dr.Item("strSICCode")) Then
                                                IAIPFacilitySIC = "NULL"
                                            Else
                                                IAIPFacilitySIC = dr.Item("strSICCode")
                                            End If
                                        End While
                                        dr.Close()
                                        If Mid(IAIPFacilityCity, 1, 30) = FacilityCity Then
                                            IAIPFacilityCity = ""
                                        End If
                                        If Replace(IAIPFacilityCity, " ", "") = Replace(FacilityCity, " ", "") Then
                                            IAIPFacilityCity = ""
                                        End If
                                        If Mid(IAIPFacilityZipCode, 1, 5) = FacilityZipCode Then
                                            IAIPFacilityZipCode = ""
                                        End If
                                        If Replace(IAIPFacilityZipCode, " ", "") = Replace(FacilityZipCode, " ", "") Then
                                            IAIPFacilityZipCode = ""
                                        End If
                                        If Mid(IAIPFacilitySIC, 1, 4) = FacilitySIC Then
                                            IAIPFacilitySIC = ""
                                        End If

                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", FacilityCity, IAIPFacilityCity, _
                                                            FacilityZipCode, IAIPFacilityZipCode, FacilitySIC, IAIPFacilitySIC)
                                    End If
                                Case "104"
                                    'Ignore
                                    'FacilityNAICS = Mid(DefaultText, 57, 6)
                                Case "105"
                                    If chb105Card.Checked = True Then
                                        FacilityContactPerson = Mid(DefaultText, 14, 20)
                                        FacilityContactNumber = Mid(DefaultText, 34, 10)
                                        FacilityDescription = Mid(DefaultText, 44, 25)
                                        IAIPFacilityContactPerson = "NULL"
                                        IAIPFacilityContactNumber = "NULL"
                                        IAIPFacilityDescription = "NULL"

                                        SQL = "select " & _
                                        "upper(strContactFirstName||' '||strContactLastName) as strContactName,  " & _
                                        "strContactPhoneNumber1,  " & _
                                        "upper(strPlantDescription) as strPlantDescription  " & _
                                        "from " & DBNameSpace & ".APBContactInformation, " & DBNameSpace & ".APBHeaderData  " & _
                                        "where " & DBNameSpace & ".apbcontactinformation.strAIRSNumber = " & DBNameSpace & ".apbheaderdata.strairsnumber  " & _
                                        "and " & DBNameSpace & ".APBHeaderData.strairsnumber = '0413" & AIRSNumber & "'  " & _
                                        "and strkey = '30'"

                                        cmd = New OracleCommand(SQL, Conn)
                                        If Conn.State = ConnectionState.Closed Then
                                            Conn.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        While dr.Read
                                            If IsDBNull(dr.Item("strContactName")) Then
                                                IAIPFacilityContactPerson = "NULL"
                                            Else
                                                IAIPFacilityContactPerson = Mid(dr.Item("strContactName"), 1, 20)
                                            End If
                                            If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                                                IAIPFacilityContactNumber = "NULL"
                                            Else
                                                IAIPFacilityContactNumber = Mid(dr.Item("strContactPhoneNumber1"), 1, 10)
                                            End If
                                            If IsDBNull(dr.Item("strPlantDescription")) Then
                                                IAIPFacilityDescription = "NULL"
                                            Else
                                                IAIPFacilityDescription = Mid(dr.Item("strPlantDescription"), 1, 25)
                                            End If
                                        End While
                                        dr.Close()
                                        If Mid(IAIPFacilityContactPerson, 1, 30) = FacilityContactPerson Then
                                            IAIPFacilityContactPerson = ""
                                        End If
                                        If Replace(IAIPFacilityContactPerson, " ", "") = Replace(FacilityContactPerson, " ", "") Then
                                            IAIPFacilityContactPerson = ""
                                        End If
                                        If Mid(IAIPFacilityContactNumber, 1, 30) = FacilityContactNumber Then
                                            IAIPFacilityContactNumber = ""
                                        End If
                                        If Replace(IAIPFacilityContactNumber, " ", "") = Replace(FacilityContactNumber, " ", "") Then
                                            IAIPFacilityContactNumber = ""
                                        End If
                                        If Mid(IAIPFacilityDescription, 1, 30) = FacilityDescription Then
                                            IAIPFacilityDescription = ""
                                        End If
                                        If Replace(IAIPFacilityDescription, " ", "") = Replace(FacilityDescription, " ", "") Then
                                            IAIPFacilityDescription = ""
                                        End If

                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", "", "", "", "", "", _
                                                            FacilityContactPerson, IAIPFacilityContactPerson, FacilityContactNumber, _
                                                            IAIPFacilityContactNumber, FacilityDescription, IAIPFacilityDescription)
                                    End If
                                Case "106"
                                    'Ignore
                                Case "107"
                                    'Ignore
                                Case "121"
                                    If chb121Card.Checked = True Then
                                        AirProgramCode = Mid(DefaultText, 14, 1)
                                        ProgramStatus = Mid(DefaultText, 15, 1)
                                        IAIPAirProgramCode = "NULL"
                                        IAIPProgramStatus = "NULL"

                                        Select Case AirProgramCode
                                            Case "0"
                                                IAIPAirProgramCode = "0"
                                                SQL = "select " & _
                                                "strOperationalStatus  " & _
                                                "from " & DBNameSpace & ".APBHeaderData  " & _
                                                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and substr(strAirProgramCodes, 1, 1) = '1' "
                                            Case "1"
                                                IAIPAirProgramCode = "1"
                                                SQL = "select " & _
                                                "strOperationalStatus  " & _
                                                "from " & DBNameSpace & ".APBHeaderData  " & _
                                                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and substr(strAirProgramCodes, 2, 1) = '1' "
                                            Case "3"
                                                IAIPAirProgramCode = "3"
                                                SQL = "select " & _
                                                "strOperationalStatus  " & _
                                                "from " & DBNameSpace & ".APBHeaderData  " & _
                                                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and substr(strAirProgramCodes, 3, 1) = '1' "
                                            Case "4"
                                                IAIPAirProgramCode = "4"
                                                SQL = "select " & _
                                                "strOperationalStatus  " & _
                                                "from " & DBNameSpace & ".APBHeaderData  " & _
                                                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and substr(strAirProgramCodes, 4, 1) = '1' "
                                            Case "6"
                                                IAIPAirProgramCode = "6"
                                                SQL = "select " & _
                                                "strOperationalStatus  " & _
                                                "from " & DBNameSpace & ".APBHeaderData  " & _
                                                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and substr(strAirProgramCodes, 5, 1) = '1' "
                                            Case "7"
                                                IAIPAirProgramCode = "7"
                                                SQL = "select " & _
                                                "strOperationalStatus  " & _
                                                "from " & DBNameSpace & ".APBHeaderData  " & _
                                                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and substr(strAirProgramCodes, 6, 1) = '1' "
                                            Case "8"
                                                IAIPAirProgramCode = "8"
                                                SQL = "select " & _
                                                "strOperationalStatus  " & _
                                                "from " & DBNameSpace & ".APBHeaderData  " & _
                                                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and substr(strAirProgramCodes, 7, 1) = '1' "
                                            Case "9"
                                                IAIPAirProgramCode = "9"
                                                SQL = "select " & _
                                                "strOperationalStatus  " & _
                                                "from " & DBNameSpace & ".APBHeaderData  " & _
                                                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and substr(strAirProgramCodes, 8, 1) = '1' "
                                            Case "A"
                                                IAIPAirProgramCode = "A"
                                                SQL = "select " & _
                                                "strOperationalStatus  " & _
                                                "from " & DBNameSpace & ".APBHeaderData  " & _
                                                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and substr(strAirProgramCodes, 9, 1) = '1' "
                                            Case "F"
                                                IAIPAirProgramCode = "F"
                                                SQL = "select " & _
                                                "strOperationalStatus  " & _
                                                "from " & DBNameSpace & ".APBHeaderData  " & _
                                                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and substr(strAirProgramCodes, 10, 1) = '1' "
                                            Case "I"
                                                IAIPAirProgramCode = "I"
                                                SQL = "select " & _
                                                "strOperationalStatus  " & _
                                                "from " & DBNameSpace & ".APBHeaderData  " & _
                                                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and substr(strAirProgramCodes, 11, 1) = '1' "
                                            Case "M"
                                                IAIPAirProgramCode = "M"
                                                SQL = "select " & _
                                                "strOperationalStatus  " & _
                                                "from " & DBNameSpace & ".APBHeaderData  " & _
                                                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and substr(strAirProgramCodes, 12, 1) = '1' "
                                            Case "V"
                                                IAIPAirProgramCode = "V"
                                                SQL = "select " & _
                                                "strOperationalStatus  " & _
                                                "from " & DBNameSpace & ".APBHeaderData  " & _
                                                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and substr(strAirProgramCodes, 13, 1) = '1' "
                                            Case Else
                                                SQL = ""
                                                IAIPAirProgramCode = "NULL"
                                                IAIPProgramStatus = "NULL"
                                        End Select
                                        If SQL <> "" Then
                                            cmd = New OracleCommand(SQL, Conn)
                                            dr = cmd.ExecuteReader
                                            While dr.Read
                                                If IsDBNull(dr.Item("strOperationalStatus")) Then
                                                    IAIPProgramStatus = "NULL"
                                                Else
                                                    IAIPProgramStatus = dr.Item("strOperationalStatus")
                                                End If
                                            End While
                                            dr.Close()
                                        End If

                                        If IAIPAirProgramCode = AirProgramCode Then
                                            IAIPAirProgramCode = ""
                                        End If
                                        If IAIPProgramStatus = ProgramStatus Then
                                            IAIPProgramStatus = ""
                                        End If

                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                            AirProgramCode, IAIPAirProgramCode, ProgramStatus, IAIPProgramStatus)
                                    End If
                                Case "122"
                                    If chb122Card.Checked = True Then
                                        AirProgramCode = Mid(DefaultText, 14, 1)
                                        SubPart1 = Replace(Mid(DefaultText, 16, 5), " ", "")
                                        SubPart2 = Replace(Mid(DefaultText, 21, 5), " ", "")
                                        SubPart3 = Replace(Mid(DefaultText, 26, 5), " ", "")
                                        SubPart4 = Replace(Mid(DefaultText, 31, 5), " ", "")
                                        SubPart5 = Replace(Mid(DefaultText, 36, 5), " ", "")
                                        SubPart6 = Replace(Mid(DefaultText, 41, 5), " ", "")
                                        SubPart7 = Replace(Mid(DefaultText, 46, 5), " ", "")
                                        SubPart8 = Replace(Mid(DefaultText, 51, 5), " ", "")
                                        SubPart9 = Replace(Mid(DefaultText, 56, 5), " ", "")
                                        IAIPAirProgramCode = "NULL"
                                        IAIPSubPart = "NULL"

                                        Select Case AirProgramCode
                                            Case "0"
                                                IAIPAirProgramCode = "0"
                                                SQL = "select " & _
                                                "distinct(strAIRSNumber) as subPartTest " & _
                                                "from " & DBNameSpace & ".APBSubPartData  " & _
                                                "where strsubpartkey = '0413" & AIRSNumber & "0' " & _
                                                "and (strSubPart = '" & SubPart1 & "' or strsubpart = '" & SubPart2 & "' " & _
                                                "or strsubpart = '" & SubPart3 & "' or strsubpart = '" & SubPart4 & "' " & _
                                                "or strsubpart = '" & SubPart5 & "' or strsubpart = '" & SubPart6 & "' " & _
                                                "or strsubpart = '" & SubPart7 & "' or strsubpart = '" & SubPart8 & "' " & _
                                                "or strsubpart = '" & SubPart9 & "') "
                                            Case "1"
                                                IAIPAirProgramCode = "1"
                                                SQL = "select " & _
                                                "distinct(strAIRSNumber) as subPartTest " & _
                                                "from " & DBNameSpace & ".APBSubPartData  " & _
                                                "where strsubpartkey = '0413" & AIRSNumber & "1' " & _
                                                "and (strSubPart = '" & SubPart1 & "' or strsubpart = '" & SubPart2 & "' " & _
                                                "or strsubpart = '" & SubPart3 & "' or strsubpart = '" & SubPart4 & "' " & _
                                                "or strsubpart = '" & SubPart5 & "' or strsubpart = '" & SubPart6 & "' " & _
                                                "or strsubpart = '" & SubPart7 & "' or strsubpart = '" & SubPart8 & "' " & _
                                                "or strsubpart = '" & SubPart9 & "') "
                                            Case "3"
                                                IAIPAirProgramCode = "3"
                                                SQL = "select " & _
                                                "distinct(strAIRSNumber) as subPartTest " & _
                                                "from " & DBNameSpace & ".APBSubPartData  " & _
                                                "where strsubpartkey = '0413" & AIRSNumber & "3' " & _
                                                "and (strSubPart = '" & SubPart1 & "' or strsubpart = '" & SubPart2 & "' " & _
                                                "or strsubpart = '" & SubPart3 & "' or strsubpart = '" & SubPart4 & "' " & _
                                                "or strsubpart = '" & SubPart5 & "' or strsubpart = '" & SubPart6 & "' " & _
                                                "or strsubpart = '" & SubPart7 & "' or strsubpart = '" & SubPart8 & "' " & _
                                                "or strsubpart = '" & SubPart9 & "') "
                                            Case "4"
                                                IAIPAirProgramCode = "4"
                                                SQL = "select " & _
                                                "distinct(strAIRSNumber) as subPartTest " & _
                                                "from " & DBNameSpace & ".APBSubPartData  " & _
                                                "where strsubpartkey = '0413" & AIRSNumber & "4' " & _
                                                "and (strSubPart = '" & SubPart1 & "' or strsubpart = '" & SubPart2 & "' " & _
                                                "or strsubpart = '" & SubPart3 & "' or strsubpart = '" & SubPart4 & "' " & _
                                                "or strsubpart = '" & SubPart5 & "' or strsubpart = '" & SubPart6 & "' " & _
                                                "or strsubpart = '" & SubPart7 & "' or strsubpart = '" & SubPart8 & "' " & _
                                                "or strsubpart = '" & SubPart9 & "') "
                                            Case "6"
                                                IAIPAirProgramCode = "6"
                                                SQL = "select " & _
                                                "distinct(strAIRSNumber) as subPartTest " & _
                                                "from " & DBNameSpace & ".APBSubPartData  " & _
                                                "where strsubpartkey = '0413" & AIRSNumber & "6' " & _
                                                "and (strSubPart = '" & SubPart1 & "' or strsubpart = '" & SubPart2 & "' " & _
                                                "or strsubpart = '" & SubPart3 & "' or strsubpart = '" & SubPart4 & "' " & _
                                                "or strsubpart = '" & SubPart5 & "' or strsubpart = '" & SubPart6 & "' " & _
                                                "or strsubpart = '" & SubPart7 & "' or strsubpart = '" & SubPart8 & "' " & _
                                                "or strsubpart = '" & SubPart9 & "') "
                                            Case "7"
                                                IAIPAirProgramCode = "7"
                                                SQL = "select " & _
                                                "distinct(strAIRSNumber) as subPartTest " & _
                                                "from " & DBNameSpace & ".APBSubPartData  " & _
                                                "where strsubpartkey = '0413" & AIRSNumber & "7' " & _
                                                "and (strSubPart = '" & SubPart1 & "' or strsubpart = '" & SubPart2 & "' " & _
                                                "or strsubpart = '" & SubPart3 & "' or strsubpart = '" & SubPart4 & "' " & _
                                                "or strsubpart = '" & SubPart5 & "' or strsubpart = '" & SubPart6 & "' " & _
                                                "or strsubpart = '" & SubPart7 & "' or strsubpart = '" & SubPart8 & "' " & _
                                                "or strsubpart = '" & SubPart9 & "') "
                                            Case "8"
                                                IAIPAirProgramCode = "8"
                                                SQL = "select " & _
                                                "distinct(strAIRSNumber) as subPartTest " & _
                                                "from " & DBNameSpace & ".APBSubPartData  " & _
                                                "where strsubpartkey = '0413" & AIRSNumber & "8' " & _
                                                "and (strSubPart = '" & SubPart1 & "' or strsubpart = '" & SubPart2 & "' " & _
                                                "or strsubpart = '" & SubPart3 & "' or strsubpart = '" & SubPart4 & "' " & _
                                                "or strsubpart = '" & SubPart5 & "' or strsubpart = '" & SubPart6 & "' " & _
                                                "or strsubpart = '" & SubPart7 & "' or strsubpart = '" & SubPart8 & "' " & _
                                                "or strsubpart = '" & SubPart9 & "') "
                                            Case "9"
                                                IAIPAirProgramCode = "9"
                                                SQL = "select " & _
                                                "distinct(strAIRSNumber) as subPartTest " & _
                                                "from " & DBNameSpace & ".APBSubPartData  " & _
                                                "where strsubpartkey = '0413" & AIRSNumber & "9' " & _
                                                "and (strSubPart = '" & SubPart1 & "' or strsubpart = '" & SubPart2 & "' " & _
                                                "or strsubpart = '" & SubPart3 & "' or strsubpart = '" & SubPart4 & "' " & _
                                                "or strsubpart = '" & SubPart5 & "' or strsubpart = '" & SubPart6 & "' " & _
                                                "or strsubpart = '" & SubPart7 & "' or strsubpart = '" & SubPart8 & "' " & _
                                                "or strsubpart = '" & SubPart9 & "') "
                                            Case "A"
                                                IAIPAirProgramCode = "A"
                                                SQL = "select " & _
                                                "distinct(strAIRSNumber) as subPartTest " & _
                                                "from " & DBNameSpace & ".APBSubPartData  " & _
                                                "where strsubpartkey = '0413" & AIRSNumber & "A' " & _
                                                "and (strSubPart = '" & SubPart1 & "' or strsubpart = '" & SubPart2 & "' " & _
                                                "or strsubpart = '" & SubPart3 & "' or strsubpart = '" & SubPart4 & "' " & _
                                                "or strsubpart = '" & SubPart5 & "' or strsubpart = '" & SubPart6 & "' " & _
                                                "or strsubpart = '" & SubPart7 & "' or strsubpart = '" & SubPart8 & "' " & _
                                                "or strsubpart = '" & SubPart9 & "') "
                                            Case "F"
                                                IAIPAirProgramCode = "F"
                                                SQL = "select " & _
                                                "distinct(strAIRSNumber) as subPartTest " & _
                                                "from " & DBNameSpace & ".APBSubPartData  " & _
                                                "where strsubpartkey = '0413" & AIRSNumber & "F' " & _
                                                "and (strSubPart = '" & SubPart1 & "' or strsubpart = '" & SubPart2 & "' " & _
                                                "or strsubpart = '" & SubPart3 & "' or strsubpart = '" & SubPart4 & "' " & _
                                                "or strsubpart = '" & SubPart5 & "' or strsubpart = '" & SubPart6 & "' " & _
                                                "or strsubpart = '" & SubPart7 & "' or strsubpart = '" & SubPart8 & "' " & _
                                                "or strsubpart = '" & SubPart9 & "') "
                                            Case "I"
                                                IAIPAirProgramCode = "I"
                                                SQL = "select " & _
                                                "distinct(strAIRSNumber) as subPartTest " & _
                                                "from " & DBNameSpace & ".APBSubPartData  " & _
                                                "where strsubpartkey = '0413" & AIRSNumber & "I' " & _
                                                "and (strSubPart = '" & SubPart1 & "' or strsubpart = '" & SubPart2 & "' " & _
                                                "or strsubpart = '" & SubPart3 & "' or strsubpart = '" & SubPart4 & "' " & _
                                                "or strsubpart = '" & SubPart5 & "' or strsubpart = '" & SubPart6 & "' " & _
                                                "or strsubpart = '" & SubPart7 & "' or strsubpart = '" & SubPart8 & "' " & _
                                                "or strsubpart = '" & SubPart9 & "') "
                                            Case "M"
                                                IAIPAirProgramCode = "M"
                                                SQL = "select " & _
                                                "distinct(strAIRSNumber) as subPartTest " & _
                                                "from " & DBNameSpace & ".APBSubPartData  " & _
                                                "where strsubpartkey = '0413" & AIRSNumber & "M' " & _
                                                "and (strSubPart = '" & SubPart1 & "' or strsubpart = '" & SubPart2 & "' " & _
                                                "or strsubpart = '" & SubPart3 & "' or strsubpart = '" & SubPart4 & "' " & _
                                                "or strsubpart = '" & SubPart5 & "' or strsubpart = '" & SubPart6 & "' " & _
                                                "or strsubpart = '" & SubPart7 & "' or strsubpart = '" & SubPart8 & "' " & _
                                                "or strsubpart = '" & SubPart9 & "') "
                                            Case "V"
                                                IAIPAirProgramCode = "V"
                                                SQL = "select " & _
                                                "distinct(strAIRSNumber) as subPartTest " & _
                                                "from " & DBNameSpace & ".APBSubPartData  " & _
                                                "where strsubpartkey = '0413" & AIRSNumber & "V' " & _
                                                "and (strSubPart = '" & SubPart1 & "' or strsubpart = '" & SubPart2 & "' " & _
                                                "or strsubpart = '" & SubPart3 & "' or strsubpart = '" & SubPart4 & "' " & _
                                                "or strsubpart = '" & SubPart5 & "' or strsubpart = '" & SubPart6 & "' " & _
                                                "or strsubpart = '" & SubPart7 & "' or strsubpart = '" & SubPart8 & "' " & _
                                                "or strsubpart = '" & SubPart9 & "') "
                                            Case Else
                                                SQL = ""
                                                IAIPAirProgramCode = "NULL"
                                        End Select
                                        If SQL <> "" Then
                                            cmd = New OracleCommand(SQL, Conn)
                                            If Conn.State = ConnectionState.Closed Then
                                                Conn.Open()
                                            End If
                                            dr = cmd.ExecuteReader
                                            While dr.Read
                                                If IsDBNull(dr.Item("subPartTest")) Then
                                                    IAIPSubPart = "NULL"
                                                Else
                                                    IAIPSubPart = dr.Item("subPartTest")
                                                End If
                                            End While
                                            dr.Close()
                                        End If

                                        If IAIPAirProgramCode = AirProgramCode Then
                                            IAIPAirProgramCode = ""
                                        End If
                                        If IAIPSubPart <> "NULL" Then
                                            IAIPSubPart = ""
                                        End If

                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, _
                                                            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                            AirProgramCode, IAIPAirProgramCode, "", "", IAIPSubPart, SubPart1, SubPart2, SubPart3, _
                                                            SubPart4, SubPart5, SubPart6, SubPart7, SubPart8, SubPart9)
                                    End If
                                Case "131"
                                    If chb131Card.Checked = True Then
                                        AirProgramCode = Mid(DefaultText, 14, 1)
                                        Pollutant = Mid(DefaultText, 15, 9)
                                        FacilityClassification = Mid(DefaultText, 25, 2)
                                        ComplianceStatus = Mid(DefaultText, 24, 1)
                                        AttainmentStatus = Mid(DefaultText, 27, 1)
                                        SICCode = Mid(DefaultText, 35, 4)
                                        IAIPAirProgramCode = "NULL"
                                        IAIPPollutant = "NULL"
                                        IAIPComplianceStatus = "NULL"
                                        IAIPFacilityClassification = "NULL"
                                        IAIPAttainmentStatus = "NULL"
                                        IAIPSICCode = "NULL"

                                        Select Case AirProgramCode
                                            Case "0"
                                                IAIPAirProgramCode = "0"
                                                SQL = "Select " & _
                                                "strPollutantKey, strComplianceStatus " & _
                                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                                "where strAirPollutantKey = '0413" & AIRSNumber & "0' " & _
                                                "and strPollutantKey = '" & Pollutant & "' "
                                            Case "1"
                                                IAIPAirProgramCode = "1"
                                                SQL = "Select " & _
                                                "strPollutantKey, strComplianceStatus " & _
                                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                                "where strAirPollutantKey = '0413" & AIRSNumber & "1' " & _
                                                "and strPollutantKey = '" & Pollutant & "' "
                                            Case "3"
                                                IAIPAirProgramCode = "3"
                                                SQL = "Select " & _
                                                "strPollutantKey, strComplianceStatus " & _
                                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                                "where strAirPollutantKey = '0413" & AIRSNumber & "3' " & _
                                                "and strPollutantKey = '" & Pollutant & "' "
                                            Case "4"
                                                IAIPAirProgramCode = "4"
                                                SQL = "Select " & _
                                                "strPollutantKey, strComplianceStatus " & _
                                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                                "where strAirPollutantKey = '0413" & AIRSNumber & "4' " & _
                                                "and strPollutantKey = '" & Pollutant & "' "
                                            Case "6"
                                                IAIPAirProgramCode = "6"
                                                SQL = "Select " & _
                                                "strPollutantKey, strComplianceStatus " & _
                                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                                "where strAirPollutantKey = '0413" & AIRSNumber & "6' " & _
                                                "and strPollutantKey = '" & Pollutant & "' "
                                            Case "7"
                                                IAIPAirProgramCode = "7"
                                                SQL = "Select " & _
                                                "strPollutantKey, strComplianceStatus " & _
                                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                                "where strAirPollutantKey = '0413" & AIRSNumber & "7' " & _
                                                "and strPollutantKey = '" & Pollutant & "' "
                                            Case "8"
                                                IAIPAirProgramCode = "8"
                                                SQL = "Select " & _
                                                "strPollutantKey, strComplianceStatus " & _
                                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                                "where strAirPollutantKey = '0413" & AIRSNumber & "8' " & _
                                                "and strPollutantKey = '" & Pollutant & "' "
                                            Case "9"
                                                IAIPAirProgramCode = "9"
                                                SQL = "Select " & _
                                                "strPollutantKey, strComplianceStatus " & _
                                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                                "where strAirPollutantKey = '0413" & AIRSNumber & "9' " & _
                                                "and strPollutantKey = '" & Pollutant & "' "
                                            Case "A"
                                                IAIPAirProgramCode = "A"
                                                SQL = "Select " & _
                                                "strPollutantKey, strComplianceStatus " & _
                                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                                "where strAirPollutantKey = '0413" & AIRSNumber & "A' " & _
                                                "and strPollutantKey = '" & Pollutant & "' "
                                            Case "F"
                                                IAIPAirProgramCode = "F"
                                                SQL = "Select " & _
                                                "strPollutantKey, strComplianceStatus " & _
                                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                                "where strAirPollutantKey = '0413" & AIRSNumber & "F' " & _
                                                "and strPollutantKey = '" & Pollutant & "' "
                                            Case "I"
                                                IAIPAirProgramCode = "I"
                                                SQL = "Select " & _
                                                "strPollutantKey, strComplianceStatus " & _
                                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                                "where strAirPollutantKey = '0413" & AIRSNumber & "I' " & _
                                                "and strPollutantKey = '" & Pollutant & "' "
                                            Case "M"
                                                IAIPAirProgramCode = "M"
                                                SQL = "Select " & _
                                                "strPollutantKey, strComplianceStatus " & _
                                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                                "where strAirPollutantKey = '0413" & AIRSNumber & "M' " & _
                                                "and strPollutantKey = '" & Pollutant & "' "
                                            Case "V"
                                                IAIPAirProgramCode = "V"
                                                SQL = "Select " & _
                                                "strPollutantKey, strComplianceStatus " & _
                                                "from " & DBNameSpace & ".APBAirProgramPollutants " & _
                                                "where strAirPollutantKey = '0413" & AIRSNumber & "V' " & _
                                                "and strPollutantKey = '" & Pollutant & "' "
                                            Case Else
                                                SQL = ""
                                                IAIPAirProgramCode = "NULL"
                                        End Select

                                        If SQL <> "" Then
                                            cmd = New OracleCommand(SQL, Conn)
                                            If Conn.State = ConnectionState.Closed Then
                                                Conn.Open()
                                            End If
                                            dr = cmd.ExecuteReader
                                            While dr.Read
                                                If IsDBNull(dr.Item("strPollutantKey")) Then
                                                    IAIPPollutant = "NULL"
                                                Else
                                                    IAIPPollutant = dr.Item("strPollutantKey")
                                                End If
                                                If IsDBNull(dr.Item("strComplianceStatus")) Then
                                                    IAIPComplianceStatus = "NULL"
                                                Else
                                                    IAIPComplianceStatus = dr.Item("strComplianceStatus")
                                                End If
                                            End While
                                            dr.Close()

                                            SQL = "Select " & _
                                            "strClass, strAttainmentStatus, " & _
                                            "strSICCode " & _
                                            "from " & DBNameSpace & ".APBHeaderData " & _
                                            "where strAIRSNumber = '0413" & AIRSNumber & "' "

                                            cmd = New OracleCommand(SQL, Conn)
                                            If Conn.State = ConnectionState.Closed Then
                                                Conn.Open()
                                            End If
                                            dr = cmd.ExecuteReader
                                            While dr.Read
                                                If IsDBNull(dr.Item("strClass")) Then
                                                    IAIPFacilityClassification = "NULL"
                                                Else
                                                    IAIPFacilityClassification = dr.Item("strClass")
                                                End If
                                                If IsDBNull(dr.Item("strAttainmentStatus")) Then
                                                    IAIPAttainmentStatus = "NULL"
                                                Else
                                                    IAIPAttainmentStatus = dr.Item("strAttainmentStatus")
                                                End If
                                                If IsDBNull(dr.Item("strSICCode")) Then
                                                    IAIPSICCode = "NULL"
                                                Else
                                                    IAIPSICCode = dr.Item("strSICCode")
                                                End If
                                            End While
                                            dr.Close()
                                        End If

                                        If IAIPAirProgramCode = AirProgramCode Then
                                            IAIPAirProgramCode = ""
                                        End If
                                        If IAIPPollutant = Pollutant Then
                                            IAIPPollutant = ""
                                        End If
                                        If IAIPFacilityClassification = FacilityClassification Then
                                            IAIPFacilityClassification = ""
                                        End If
                                        If Replace(IAIPFacilityClassification, " ", "") = Replace(FacilityClassification, " ", "") Then
                                            IAIPFacilityClassification = ""
                                        End If

                                        If IAIPComplianceStatus = ComplianceStatus Then
                                            IAIPComplianceStatus = ""
                                        End If
                                        If IAIPAttainmentStatus <> "0000" Then
                                            IAIPAttainmentStatus = "A"
                                        Else
                                            IAIPAttainmentStatus = "N"
                                        End If
                                        If IAIPAttainmentStatus = AttainmentStatus Then
                                            IAIPAttainmentStatus = ""
                                        End If
                                        If IAIPSICCode = SICCode Then
                                            IAIPSICCode = ""
                                        End If

                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                            AirProgramCode, IAIPAirProgramCode, "", "", "", "", "", "", "", "", "", "", "", "", _
                                                                  Pollutant, IAIPPollutant, FacilityClassification, IAIPFacilityClassification, _
                                                                  ComplianceStatus, IAIPComplianceStatus, AttainmentStatus, IAIPAttainmentStatus, _
                                                                  SICCode, IAIPSICCode)
                                    End If
                                Case "141"
                                    'Ignore
                                Case "151"
                                    'Ignore
                                Case "161" 'Various Action Types
                                    If chb161Card.Checked = True Then
                                        SQL = ""
                                        ActionNumber = Mid(DefaultText, 14, 3)
                                        AirProgramCode = Mid(DefaultText, 17, 6)
                                        ActionType = Mid(DefaultText, 23, 2)
                                        DateScheduled = Mid(DefaultText, 41, 6)
                                        DateAcheived = Mid(DefaultText, 47, 6)
                                        ResultCode = Mid(DefaultText, 53, 2)
                                        PenaltyAmount = Mid(DefaultText, 55, 7)
                                        Pollutant161 = Mid(DefaultText, 67, 5)
                                        IAIPActionNumber = "NULL"
                                        IAIPAirProgramCode = "NULL"
                                        IAIPDateScheduled = "NULL"
                                        IAIPDateAcheived = "NULL"
                                        IAIPResultCode = "NULL"
                                        IAIPPenaltyAmount = "NULL"
                                        IAIPPollutant161 = "NULL"

                                        Select Case ActionType
                                            Case "00"
                                                SQL = ""
                                            Case "33"
                                                SQL = ""
                                            Case "34"
                                                SQL = ""
                                            Case "35"
                                                SQL = ""
                                            Case "36"
                                                SQL = ""
                                            Case "FS"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datFCECompleted as DatCompleteDate, " & _
                                                "case " & _
                                                "when strFCEStatus = 'True' then '21' " & _
                                                "else '' " & _
                                                "end ResultCode, " & _
                                                "'0000000' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPFCERecords, " & DBNameSpace & ".SSCPFCEMaster, " & _
                                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPFCE  " & _
                                                "where " & DBNameSpace & ".AFSSSCPFCERecords.strFCENumber = " & DBNameSpace & ".SSCPFCEMaster.StrFCENumber " & _
                                                "and " & DBNameSpace & ".SSCPFCEMaster.strFCENumber = " & DBNameSpace & ".SSCPFCE.strFCENumber " & _
                                                "and " & DBNameSpace & ".SSCPFCEMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and strSiteInspection = 'True' " & _
                                                "and " & DBNameSpace & ".SSCPFCEMaster.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber  = '" & ActionNumber & "' "
                                            Case "FF"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datFCECompleted as DatCompleteDate" & _
                                                "case " & _
                                                "when strFCEStatus = 'True' then '21' " & _
                                                "else '' " & _
                                                "end ResultCode, " & _
                                                "'0000000' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPFCERecords, " & DBNameSpace & ".SSCPFCEMaster, " & _
                                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPFCE  " & _
                                                "where " & DBNameSpace & ".AFSSSCPFCERecords.strFCENumber = " & DBNameSpace & ".SSCPFCEMaster.StrFCENumber " & _
                                                "and " & DBNameSpace & ".SSCPFCEMaster.strFCENumber = " & DBNameSpace & ".SSCPFCE.strFCENumber " & _
                                                "and " & DBNameSpace & ".SSCPFCEMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and strSiteInspection <> 'True' " & _
                                                "and " & DBNameSpace & ".SSCPFCEMaster.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber  = '" & ActionNumber & "' "
                                            Case "37"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datCompleteDate, " & _
                                                "'01' as ResultCode, " & _
                                                "'0000000' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPRecords, " & DBNameSpace & ".SSCPItemMaster, " & _
                                                "" & DBNameSpace & ".APBHeaderData " & _
                                                "where " & DBNameSpace & ".AFSSSCPRecords.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber " & _
                                                "and " & DBNameSpace & ".SSCPItemMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".SSCPItemMaster.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber  = '" & ActionNumber & "' "
                                            Case "27"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datCompleteDate, " & _
                                                "'01' as ResultCode, " & _
                                                "'0000000' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPRecords, " & DBNameSpace & ".SSCPItemMaster, " & _
                                                "" & DBNameSpace & ".APBHeaderData " & _
                                                "where " & DBNameSpace & ".AFSSSCPRecords.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber " & _
                                                "and " & DBNameSpace & ".SSCPItemMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".SSCPItemMaster.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber  = '" & ActionNumber & "' "
                                            Case "SR"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datCompleteDate, " & _
                                                "case " & _
                                                "when strEnforcementNumber is Null then 'MC' " & _
                                                "when strEnforcementNumber is Not Null then 'MV' " & _
                                                "else 'MC' " & _
                                                "end ResultCode, " & _
                                                "'0000000' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPRecords, " & DBNameSpace & ".SSCPItemMaster, " & _
                                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPEnforcementItems " & _
                                                "where " & DBNameSpace & ".AFSSSCPRecords.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber " & _
                                                "and " & DBNameSpace & ".SSCPItemMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber = " & DBNameSpace & ".SSCPEnforcementItems.strTrackingNumber (+) " & _
                                                "and " & DBNameSpace & ".SSCPItemMaster.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber  = '" & ActionNumber & "' "
                                            Case "CS"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datCompleteDate, " & _
                                                "'  ' as ResultCode, " & _
                                                "'       ' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPRecords, " & DBNameSpace & ".SSCPItemMaster, " & _
                                                "" & DBNameSpace & ".APBHeaderData " & _
                                                "where " & DBNameSpace & ".AFSSSCPRecords.strTrackingNumber = " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber " & _
                                                "and " & DBNameSpace & ".SSCPItemMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".SSCPItemMaster.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber  = '" & ActionNumber & "' "
                                            Case "04"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datDayZero as datCompleteDate, " & _
                                                "'01' as ResultCode, " & _
                                                "'0000000' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPEnforcementRecords, " & DBNameSpace & ".SSCPEnforcementItems, " & _
                                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPEnforcement " & _
                                                "where " & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber  " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber = '" & ActionNumber & "' "
                                            Case "VZ"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datDayZero as datCompleteDate, " & _
                                                "'01' as ResultCode, " & _
                                                "'0000000' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPEnforcementRecords, " & DBNameSpace & ".SSCPEnforcementItems, " & _
                                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPEnforcement " & _
                                                "where " & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber  " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber = '" & ActionNumber & "' "
                                            Case "56"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datNOVSent as datCompleteDate, " & _
                                                "'  ' as ResultCode, " & _
                                                "'0000000' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPEnforcementRecords, " & DBNameSpace & ".SSCPEnforcementItems, " & _
                                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPEnforcement " & _
                                                "where " & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber  " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber = '" & ActionNumber & "' "
                                            Case "AW"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datNFALetterSent as datCompleteDate, " & _
                                                "'  ' as ResultCode, " & _
                                                "'0000000' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPEnforcementRecords, " & DBNameSpace & ".SSCPEnforcementItems, " & _
                                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPEnforcement " & _
                                                "where " & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber  " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber = '" & ActionNumber & "' "
                                            Case "57"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datCOProposed as datCompleteDate, " & _
                                                "'  ' as ResultCode, " & _
                                                "'0000000' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPEnforcementRecords, " & DBNameSpace & ".SSCPEnforcementItems, " & _
                                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPEnforcement " & _
                                                "where " & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber  " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber = '" & ActionNumber & "' "
                                            Case "X1"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datCOExecuted as datCompleteDate, " & _
                                                "'01' as ResultCode, " & _
                                                "strCOPenaltyAmount as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPEnforcementRecords, " & DBNameSpace & ".SSCPEnforcementItems, " & _
                                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPEnforcement " & _
                                                "where " & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber  " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber = '" & ActionNumber & "' "
                                            Case "AS"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datCOResolved as datCompleteDate, " & _
                                                "'  ' as ResultCode, " & _
                                                "'0000000' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPEnforcementRecords, " & DBNameSpace & ".SSCPEnforcementItems, " & _
                                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPEnforcement " & _
                                                "where " & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber  " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber = '" & ActionNumber & "' "
                                            Case "Z4"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "" & DBNameSpace & ".SSCPEnforcementStipulated.datModifingDate as datCompleteDate, " & _
                                                "'01' as ResultCode, " & _
                                                "" & DBNameSpace & ".SSCPEnforcementStipulated.strStipulatedPenalty as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPEnforcementRecords, " & DBNameSpace & ".SSCPEnforcementItems, " & _
                                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPEnforcement, " & _
                                                "" & DBNameSpace & ".SSCPEnforcementStipulated " & _
                                                "where " & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber  " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementITems.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcementStipulated.strEnforcementNumber " & _
                                                "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber = '" & ActionNumber & "' "
                                            Case "60"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datAOExecuted as datCompleteDate, " & _
                                                "'  ' as ResultCode, " & _
                                                "'0000000' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPEnforcementRecords, " & DBNameSpace & ".SSCPEnforcementItems, " & _
                                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPEnforcement " & _
                                                "where " & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber  " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber = '" & ActionNumber & "' "
                                            Case "64"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datAOAppealed as datCompleteDate, " & _
                                                "'  ' as ResultCode, " & _
                                                "'0000000' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPEnforcementRecords, " & DBNameSpace & ".SSCPEnforcementItems, " & _
                                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPEnforcement " & _
                                                "where " & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber  " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber = '" & ActionNumber & "' "
                                            Case "XX"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datAOResolved as datCompleteDate, " & _
                                                "'  ' as ResultCode, " & _
                                                "'0000000' as PenaltyAmount, " & _
                                                "'  ' as Pollutant " & _
                                                "from " & DBNameSpace & ".AFSSSCPEnforcementRecords, " & DBNameSpace & ".SSCPEnforcementItems, " & _
                                                "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".SSCPEnforcement " & _
                                                "where " & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber = " _
                                                                 & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber  " & _
                                                "and " & DBNameSpace & ".SSCPEnforcementItems.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber = '" & ActionNumber & "' "
                                            Case "TR"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "case " & _
                                                "when strWitnessingEngineer = '0' then 'TR' " & _
                                                "else '23' " & _
                                                "end ActionType, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datTestDateEnd as datCompleteDate, " & _
                                                "case " & _
                                                "when strComplianceStatus = '01' then 'N/A' " & _
                                                "when strCompliancestatus = '02' then 'PP'  " & _
                                                "when strComplianceStatus = '03' then 'PP'  " & _
                                                "when strComplianceStatus = '04' then 'N/A' " & _
                                                "when strComplianceStatus = '05' then 'FF' " & _
                                                "else '01' " & _
                                                "end ResultCode, " & _
                                                "'       ' as PenaltyAmount, " & _
                                                "case  " & _
                                                "when strAFSCode = 'True' then strPollutantCode " & _
                                                "else '     ' " & _
                                                "end Pollutant  " & _
                                                "from " & DBNameSpace & ".AFSISMPRecords, " & DBNameSpace & ".ISMPMaster, " & _
                                                "" & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".APBHeaderData, " & _
                                                "" & DBNameSpace & ".LookUpPollutants " & _
                                                "where " & DBNameSpace & ".AFSISMPRecords.strReferenceNumber = " & DBNameSpace & ".ISMPMaster.strReferenceNumber " & _
                                                "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                                                "and " & DBNameSpace & ".ISMPMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".ISMPReportInformation.strPollutant = " & DBNameSpace & ".LookUpPollutants.strPollutantCode " & _
                                                "and " & DBNameSpace & ".ISMPMaster.strAIRSnumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber = '" & ActionNumber & "' "
                                            Case "23"
                                                SQL = "select " & _
                                                "strAIRProgramCodes, " & _
                                                "strAFSActionNumber, " & _
                                                "case " & _
                                                "when strWitnessingEngineer = '0' then 'TR' " & _
                                                "else '23' " & _
                                                "end ActionType, " & _
                                                "'000000' as DateScheduled, " & _
                                                "datTestDateEnd as datCompleteDate, " & _
                                                "case " & _
                                                "when strComplianceStatus = '01' then 'N/A' " & _
                                                "when strCompliancestatus = '02' then 'PP'  " & _
                                                "when strComplianceStatus = '03' then 'PP'  " & _
                                                "when strComplianceStatus = '04' then 'N/A' " & _
                                                "when strComplianceStatus = '05' then 'FF' " & _
                                                "else '01' " & _
                                                "end ResultCode, " & _
                                                "'       ' as PenaltyAmount, " & _
                                                "case  " & _
                                                "when strAFSCode = 'True' then strPollutantCode " & _
                                                "else '     ' " & _
                                                "end Pollutant  " & _
                                                "from " & DBNameSpace & ".AFSISMPRecords, " & DBNameSpace & ".ISMPMaster, " & _
                                                "" & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".APBHeaderData, " & _
                                                "" & DBNameSpace & ".LookUpPollutants " & _
                                                "where " & DBNameSpace & ".AFSISMPRecords.strReferenceNumber = " & DBNameSpace & ".ISMPMaster.strReferenceNumber " & _
                                                "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                                                "and " & DBNameSpace & ".ISMPMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                                                "and " & DBNameSpace & ".ISMPReportInformation.strPollutant = " & DBNameSpace & ".LookUpPollutants.strPollutantCode " & _
                                                "and " & DBNameSpace & ".ISMPMaster.strAIRSnumber = '0413" & AIRSNumber & "' " & _
                                                "and strAFSActionNumber = '" & ActionNumber & "' "
                                            Case Else
                                                SQL = ""
                                        End Select

                                        If SQL <> "" Then
                                            cmd = New OracleCommand(SQL, Conn)
                                            If Conn.State = ConnectionState.Closed Then
                                                Conn.Open()
                                            End If
                                            dr = cmd.ExecuteReader
                                            While dr.Read
                                                If IsDBNull(dr.Item("strAFSActionNumber")) Then
                                                    IAIPActionNumber = "NULL"
                                                Else
                                                    IAIPActionNumber = dr.Item("strAFSActionNumber")
                                                End If
                                                If IsDBNull(dr.Item("strAIRProgramCodes")) Then
                                                    IAIPAirProgramCode = "NULL"
                                                Else
                                                    IAIPAirProgramCode = dr.Item("strAIRProgramCodes")
                                                End If
                                                If IsDBNull(dr.Item("DateScheduled")) Then
                                                    IAIPDateScheduled = "NULL"
                                                Else
                                                    IAIPDateScheduled = dr.Item("DateScheduled")
                                                End If
                                                If IsDBNull(dr.Item("datCompleteDate")) Then
                                                    IAIPDateAcheived = "NULL"
                                                Else
                                                    IAIPDateAcheived = dr.Item("datCompleteDate")
                                                End If
                                                If IsDBNull(dr.Item("ResultCode")) Then
                                                    IAIPResultCode = "NULL"
                                                Else
                                                    IAIPResultCode = dr.Item("ResultCode")
                                                End If
                                                If IsDBNull(dr.Item("PenaltyAmount")) Then
                                                    IAIPPenaltyAmount = "NULL"
                                                Else
                                                    IAIPPenaltyAmount = dr.Item("PenaltyAmount")
                                                End If
                                                If IsDBNull(dr.Item("Pollutant")) Then
                                                    IAIPPollutant161 = "NULL"
                                                Else
                                                    IAIPPollutant161 = dr.Item("Pollutant")
                                                End If
                                            End While
                                            dr.Close()
                                        End If

                                        If IAIPActionNumber = ActionNumber Then
                                            IAIPActionNumber = ""
                                        End If
                                        If IAIPActionType = ActionType Then
                                            IAIPActionType = ""
                                        End If
                                        If IAIPDateScheduled = DateScheduled Then
                                            IAIPDateScheduled = ""
                                        End If
                                        If IAIPDateScheduled = "000000" Then
                                            IAIPDateScheduled = ""
                                        End If

                                        If IAIPDateAcheived.Length > 4 And IAIPDateAcheived.Contains("/") Then
                                            temp = Mid(IAIPDateAcheived, IAIPDateAcheived.IndexOf("/") + 2)
                                            temp = Mid(temp, 1, temp.ToString.IndexOf("/"))
                                            If temp.ToString.Length = 1 Then
                                                temp = "0" & temp
                                            End If
                                            temp2 = Mid(IAIPDateAcheived, 1, (IAIPDateAcheived.IndexOf("/")))
                                            If temp2.ToString.Length = 1 Then
                                                temp2 = "0" & temp2
                                            End If
                                            IAIPDateAcheived = Mid(IAIPDateAcheived, (IAIPDateAcheived.Length - 1), 2) & temp2 & temp
                                        End If
                                        If IAIPDateAcheived = DateAcheived Then
                                            IAIPDateAcheived = ""
                                        End If
                                        If IAIPDateAcheived = "000000" Then
                                            IAIPDateAcheived = ""
                                        End If
                                        If IAIPResultCode = ResultCode Then
                                            IAIPResultCode = ""
                                        End If
                                        If IAIPPenaltyAmount.Length < 7 And IAIPPenaltyAmount <> "NULL" Then
                                            Do Until IAIPPenaltyAmount.Length = 7
                                                IAIPPenaltyAmount = "0" & IAIPPenaltyAmount
                                            Loop
                                        End If
                                        If IAIPPenaltyAmount = PenaltyAmount Then
                                            IAIPPenaltyAmount = ""
                                        End If
                                        If IAIPPenaltyAmount = "0000000" Then
                                            IAIPPenaltyAmount = ""
                                        End If
                                        If Replace(IAIPPollutant161, " ", "") = Replace(Pollutant161, " ", "") Then
                                            IAIPPollutant161 = ""
                                        End If

                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                            AirProgramCode, IAIPAirProgramCode, "", "", "", "", "", "", "", "", "", "", "", "", _
                                                            "", "", "", "", "", "", "", "", "", "", _
                                                            ActionNumber, IAIPActionNumber, ActionType, IAIPActionType, DateScheduled, IAIPDateScheduled, _
                                                            DateAcheived, IAIPDateAcheived, ResultCode, IAIPResultCode, PenaltyAmount, IAIPPenaltyAmount, _
                                                            Pollutant161, IAIPPollutant161)
                                    End If
                                Case "163"
                                    If chb163Card.Checked = True Then
                                        ActionNumber163 = Mid(DefaultText, 14, 3)
                                        KeyActionNumber = Mid(DefaultText, 17, 3)
                                        Pollutant163 = Mid(DefaultText, 43, 5)

                                        IAIPActionNumber163 = "NULL"
                                        IAIPKeyActionNumber = "NULL"
                                        IAIPPollutant163 = "NULL"

                                        SQL = "Select  " & _
                                        "" & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber,    " & _
                                        "" & DBNameSpace & ".APBHeaderData.strAIRSNumber,    " & _
                                        "" & DBNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber,    " & _
                                        "strAFSKeyActionNumber,  " & _
                                        "strPollutants,   " & _
                                        "" & DBNameSpace & ".AFSSSCPRecords.strAFSActionNumber as LinkingEvent  " & _
                                        "from  " & DBNameSpace & ".AFSSSCPEnforcementRecords,    " & _
                                        "" & DBNameSpace & ".SSCPEnforcementItems,  " & DBNameSpace & ".SSCPEnforcement,    " & _
                                        "" & DBNameSpace & ".APBHeaderData,  " & DBNameSpace & ".SSCPENforcementStipulated, " & _
                                        "" & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".AFSSSCPRecords    " & _
                                        "where " & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber =  " _
                                                 & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber    " & _
                                        "and  " & DBNameSpace & ".APBHeaderData.strAIRSNumber =  " & DBNameSpace & ".SSCPEnforcementItems.strAIRSNumber    " & _
                                        "and  " & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber =  " & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber    " & _
                                        "and  " & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber =  " _
                                                & DBNameSpace & ".SSCPENforcementStipulated.strEnforcementNumber (+)    " & _
                                        "and  " & DBNameSpace & ".sscpEnforcementItems.strTrackingNumber =  " & DBNameSpace & ".SSCPItemMaster.strTrackingNumber (+)   " & _
                                        "and " & DBNameSpace & ".sscpEnforcementItems.strTrackingNumber = " & DBNameSpace & ".AFSSSCPRecords.strTrackingNumber (+) " & _
                                        "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '0413" & AIRSNumber & "' " & _
                                        "and " & DBNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber = '" & ActionNumber163 & "' " & _
                                        "and strAFSKeyActionNumber = '" & KeyActionNumber & "' "

                                        cmd = New OracleCommand(SQL, Conn)
                                        If Conn.State = ConnectionState.Closed Then
                                            Conn.Open()
                                        End If

                                        dr = cmd.ExecuteReader
                                        While dr.Read
                                            If IsDBNull(dr.Item("strAFSActionNumber")) Then
                                                IAIPActionNumber163 = "NULL"
                                            Else
                                                IAIPActionNumber163 = dr.Item("strAFSActionNumber")
                                            End If
                                            If IsDBNull(dr.Item("strAFSKeyActionNumber")) Then
                                                IAIPKeyActionNumber = "NULL"
                                            Else
                                                IAIPKeyActionNumber = dr.Item("strAFSKeyActionNumber")
                                            End If
                                            If IsDBNull(dr.Item("LinkingEvent")) Then
                                                'IAIPActionNumber163 = IAIPActionNumber163
                                            Else
                                                '  IAIPActionNumber163 = dr.Item("LinkingEvent")
                                            End If
                                            If IsDBNull(dr.Item("strPollutants")) Then
                                                IAIPPollutant163 = "NULL"
                                            Else
                                                IAIPPollutant163 = dr.Item("strPollutants")
                                            End If
                                        End While
                                        dr.Close()


                                        If IAIPActionNumber163 = ActionNumber163 Then
                                            IAIPActionNumber163 = ""
                                        End If
                                        If IAIPKeyActionNumber = KeyActionNumber Then
                                            IAIPKeyActionNumber = ""
                                        End If
                                        If IAIPPollutant163 <> "NULL" Then
                                            IAIPPollutant163 = Mid(IAIPPollutant163, 2, IAIPPollutant163.IndexOf(",") - 1)
                                        End If
                                        If Replace(IAIPPollutant163, " ", "") = Replace(Pollutant163, " ", "") Then
                                            IAIPPollutant163 = ""
                                        End If

                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                            AirProgramCode, "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                            "", "", "", "", "", "", _
                                                            "", "", ActionNumber163, IAIPActionNumber163, KeyActionNumber, IAIPKeyActionNumber, Pollutant163, IAIPPollutant163)
                                    End If

                                Case "164"
                                    If chb164Card.Checked = True Then
                                        ActionNumber164 = Mid(DefaultText, 14, 3)
                                        KeyActionNumber164 = Mid(DefaultText, 17, 3)
                                        ViolationType = Mid(DefaultText, 42, 3)
                                        IAIPActionNumber164 = "NULL"
                                        IAIPKeyActionNumber164 = "NULL"
                                        ViolationType = "NULL"

                                        SQL = "Select " & _
                                        "" & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber,    " & _
                                        "" & DBNameSpace & ".APBHeaderData.strAIRSNumber,    " & _
                                        "" & DBNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber,    " & _
                                        "strAFSKeyActionNumber, " & _
                                        "strHPV " & _
                                        "from  " & DBNameSpace & ".AFSSSCPEnforcementRecords,    " & _
                                        "" & DBNameSpace & ".SSCPEnforcementItems,  " & DBNameSpace & ".SSCPEnforcement,    " & _
                                        "" & DBNameSpace & ".APBHeaderData " & _
                                        "where " & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber =  " & DBNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber    " & _
                                        "and  " & DBNameSpace & ".APBHeaderData.strAIRSNumber =  " & DBNameSpace & ".SSCPEnforcementItems.strAIRSNumber    " & _
                                        "and  " & DBNameSpace & ".SSCPEnforcementItems.strEnforcementNumber =  " & DBNameSpace & ".SSCPEnforcement.strEnforcementNumber    " & _
                                        "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = '041312100816' " & _
                                        "and " & DBNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber = '007' " & _
                                        "and strAFSKeyActionNumber = '007' "

                                        cmd = New OracleCommand(SQL, Conn)
                                        If Conn.State = ConnectionState.Closed Then
                                            Conn.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        While dr.Read
                                            If IsDBNull(dr.Item("strAFSActionNumber")) Then
                                                IAIPActionNumber164 = "NULL"
                                            Else
                                                IAIPActionNumber164 = dr.Item("strAFSActionNumber")
                                            End If
                                            If IsDBNull(dr.Item("strAFSKeyActionNumber")) Then
                                                IAIPKeyActionNumber164 = "NULL"
                                            Else
                                                IAIPKeyActionNumber164 = dr.Item("strAFSKeyActionNumber")
                                            End If
                                            If IsDBNull(dr.Item("strHPV")) Then
                                                IAIPViolationType = "NULL"
                                            Else
                                                IAIPViolationType = dr.Item("strHPV")
                                            End If
                                        End While
                                        dr.Close()

                                        If IAIPActionNumber164 = ActionNumber164 Then
                                            IAIPActionNumber164 = ""
                                        End If
                                        If IAIPKeyActionNumber164 = KeyActionNumber164 Then
                                            IAIPKeyActionNumber164 = ""
                                        End If
                                        If IAIPViolationType = ViolationType Then
                                            IAIPViolationType = ""
                                        End If

                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                            AirProgramCode, "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                            "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                            ActionNumber164, IAIPActionNumber164, KeyActionNumber164, IAIPKeyActionNumber164, _
                                                            ViolationType, IAIPViolationType)
                                    End If
                                Case "171" 'Comments
                                    'Ignore
                                Case "181"
                                    If chb181Card.Checked = True Then
                                        CMSStatus = Mid(DefaultText, 14, 2)
                                        IAIPCMSStatus = "NULL"

                                        SQL = "Select " & _
                                        "strCMSMember " & _
                                        "from " & DBNameSpace & ".APBSupplamentalData " & _
                                        "where strAIRSNumber = '0413" & AIRSNumber & "' "
                                        cmd = New OracleCommand(SQL, Conn)
                                        If Conn.State = ConnectionState.Closed Then
                                            Conn.Open()
                                        End If
                                        dr = cmd.ExecuteReader
                                        While dr.Read
                                            If IsDBNull(dr.Item("strCMSMember")) Then
                                                IAIPCMSStatus = "NULL"
                                            Else
                                                IAIPCMSStatus = dr.Item("strCMSMember")
                                            End If
                                        End While
                                        dr.Close()

                                        If IAIPCMSStatus = "A" Then
                                            IAIPCMSStatus = "A2"
                                        End If
                                        If IAIPCMSStatus = "S" Then
                                            IAIPCMSStatus = "S5"
                                        End If

                                        If IAIPCMSStatus = CMSStatus Then
                                            IAIPCMSStatus = ""
                                        End If

                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                             "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                             "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                             "", "", "", "", "", CMSStatus, IAIPCMSStatus)
                                    End If
                                Case Else

                            End Select

                        Loop Until reader.Peek = -1
                        reader.Close()

                    Else
                        dgvAFSData.Columns.Add("AIRSNumber", "AIRS Number")
                        dgvAFSData.Columns.Add("TransactionType", "Transaction Type")
                        dgvAFSData.Columns.Add("FacilityName", "Facility Name")
                        dgvAFSData.Columns("FacilityName").Width = 200
                        dgvAFSData.Columns.Add("FacilityAddress", "Facility Address")
                        dgvAFSData.Columns("FacilityAddress").Width = 200
                        dgvAFSData.Columns.Add("City", "City")
                        dgvAFSData.Columns.Add("ZipCode", "Zip Code")
                        dgvAFSData.Columns.Add("SICCode", "SIC Code")
                        dgvAFSData.Columns.Add("ContactPerson", "Contact Person")
                        dgvAFSData.Columns.Add("ContactNumber", "Contact Number")
                        dgvAFSData.Columns.Add("FacilityDescription", "Facility Description")
                        dgvAFSData.Columns("FacilityDescription").Width = 200
                        dgvAFSData.Columns.Add("AirProgramCode", "Air Program Code")
                        dgvAFSData.Columns.Add("ProgramCodeStatus", "Program Code Status")
                        dgvAFSData.Columns.Add("SubPart1", "Sub Part 1")
                        dgvAFSData.Columns.Add("SubPart2", "Sub Part 2")
                        dgvAFSData.Columns.Add("SubPart3", "Sub Part 3")
                        dgvAFSData.Columns.Add("SubPart4", "Sub Part 4")
                        dgvAFSData.Columns.Add("SubPart5", "Sub Part 5")
                        dgvAFSData.Columns.Add("SubPart6", "Sub Part 6")
                        dgvAFSData.Columns.Add("SubPart7", "Sub Part 7")
                        dgvAFSData.Columns.Add("SubPart8", "Sub Part 8")
                        dgvAFSData.Columns.Add("SubPart9", "Sub Part 9")
                        dgvAFSData.Columns.Add("Pollutant", "Pollutant")
                        dgvAFSData.Columns.Add("Class", "Classification")
                        dgvAFSData.Columns.Add("ComplianceStauts", "Compliance Status")
                        dgvAFSData.Columns.Add("AttainmentStatus", "Attainment Status")
                        dgvAFSData.Columns.Add("SICCode2", "SIC Code")
                        dgvAFSData.Columns.Add("ActionNumber", "Action Number")
                        dgvAFSData.Columns.Add("ActionType", "Action Type")
                        dgvAFSData.Columns.Add("DateScheduled", "Date Scheduled")
                        dgvAFSData.Columns.Add("DateAcheived", "Date Acheived")
                        dgvAFSData.Columns.Add("ResultCode", "Result Code")
                        dgvAFSData.Columns.Add("PenaltyAmount", "Penalty Amount")
                        dgvAFSData.Columns.Add("KeyActionNumber", "Key Action Number")
                        dgvAFSData.Columns.Add("ActionNumber163", "Action Number")
                        dgvAFSData.Columns.Add("ViolationType", "Violation Type")
                        dgvAFSData.Columns.Add("CMSStatus", "CMS Status")

                        If chb101Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("FacilityName").Visible = False
                        End If
                        If chb102Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("FacilityAddress").Visible = False
                        End If
                        If chb103Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("City").Visible = False
                            dgvAFSData.Columns("ZipCode").Visible = False
                            dgvAFSData.Columns("SICCode").Visible = False
                        End If
                        If chb105Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("ContactPerson").Visible = False
                            dgvAFSData.Columns("ContactNumber").Visible = False
                            dgvAFSData.Columns("FacilityDescription").Visible = False
                        End If
                        If chb121Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("AirProgramCode").Visible = False
                            dgvAFSData.Columns("ProgramCodeStatus").Visible = False
                        End If
                        If chb122Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("SubPart1").Visible = False
                            dgvAFSData.Columns("SubPart2").Visible = False
                            dgvAFSData.Columns("SubPart3").Visible = False
                            dgvAFSData.Columns("SubPart4").Visible = False
                            dgvAFSData.Columns("SubPart5").Visible = False
                            dgvAFSData.Columns("SubPart6").Visible = False
                            dgvAFSData.Columns("SubPart7").Visible = False
                            dgvAFSData.Columns("SubPart8").Visible = False
                            dgvAFSData.Columns("SubPart9").Visible = False
                        End If
                        If chb131Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("Pollutant").Visible = False
                            dgvAFSData.Columns("Class").Visible = False
                            dgvAFSData.Columns("ComplianceStauts").Visible = False
                            dgvAFSData.Columns("AttainmentStatus").Visible = False
                            dgvAFSData.Columns("SICCode2").Visible = False
                        End If
                        If chb161Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("ActionNumber").Visible = False
                            dgvAFSData.Columns("ActionType").Visible = False
                            dgvAFSData.Columns("DateScheduled").Visible = False
                            dgvAFSData.Columns("DateAcheived").Visible = False
                            dgvAFSData.Columns("ResultCode").Visible = False
                            dgvAFSData.Columns("PenaltyAmount").Visible = False
                        End If
                        If chb163Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("KeyActionNumber").Visible = False
                            dgvAFSData.Columns("ActionNumber163").Visible = False
                        End If
                        If chb164Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("ViolationType").Visible = False
                        End If
                        If chb181Card.Checked = True Then
                        Else
                            dgvAFSData.Columns("CMSStatus").Visible = False
                        End If

                        Dim reader As StreamReader = New StreamReader(txtAFSFile.Text)
                        Do
                            DefaultText = reader.ReadLine
                            AIRSNumber = Mid(DefaultText, 3, 8)
                            TransactionType = Mid(DefaultText, 11, 3)
                            Select Case TransactionType
                                Case "101"
                                    If chb101Card.Checked = True Then
                                        FacilityName = Mid(DefaultText, 16, 40)
                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, FacilityName)
                                    End If
                                Case "102"
                                    If chb102Card.Checked = True Then
                                        FacilityAddress = Mid(DefaultText, 16, 30)
                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", FacilityAddress)
                                    End If
                                Case "103"
                                    If chb103Card.Checked = True Then
                                        FacilityCity = Mid(DefaultText, 16, 30)
                                        FacilityZipCode = Mid(DefaultText, 46, 9)
                                        FacilitySIC = Mid(DefaultText, 55, 4)
                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", FacilityCity, FacilityZipCode, FacilitySIC)
                                    End If
                                Case "104"
                                    'Ignore
                                    'FacilityNAICS = Mid(DefaultText, 57, 6)
                                Case "105"
                                    If chb105Card.Checked = True Then
                                        FacilityContactPerson = Mid(DefaultText, 14, 20)
                                        FacilityContactNumber = Mid(DefaultText, 34, 10)
                                        FacilityDescription = Mid(DefaultText, 44, 25)
                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", FacilityContactPerson, FacilityContactNumber, FacilityDescription)
                                    End If
                                Case "106"
                                    'Ignore
                                Case "107"
                                    'Ignore
                                Case "121"
                                    If chb121Card.Checked = True Then
                                        AirProgramCode = Mid(DefaultText, 14, 1)
                                        ProgramStatus = Mid(DefaultText, 15, 1)
                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", "", "", "", AirProgramCode, ProgramStatus)
                                    End If


                                Case "122"
                                    If chb122Card.Checked = True Then
                                        AirProgramCode = Mid(DefaultText, 14, 1)
                                        SubPart1 = Mid(DefaultText, 16, 5)
                                        SubPart2 = Mid(DefaultText, 21, 5)
                                        SubPart3 = Mid(DefaultText, 26, 5)
                                        SubPart4 = Mid(DefaultText, 31, 5)
                                        SubPart5 = Mid(DefaultText, 36, 5)
                                        SubPart6 = Mid(DefaultText, 41, 5)
                                        SubPart7 = Mid(DefaultText, 46, 5)
                                        SubPart8 = Mid(DefaultText, 51, 5)
                                        SubPart9 = Mid(DefaultText, 56, 5)
                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", "", "", "", AirProgramCode, "", SubPart1, SubPart2, SubPart3, _
                                                            SubPart4, SubPart5, SubPart6, SubPart7, SubPart8, SubPart9)
                                    End If
                                Case "131"
                                    If chb131Card.Checked = True Then
                                        AirProgramCode = Mid(DefaultText, 14, 1)
                                        Pollutant = Mid(DefaultText, 15, 9)
                                        FacilityClassification = Mid(DefaultText, 25, 2)
                                        ComplianceStatus = Mid(DefaultText, 24, 1)
                                        AttainmentStatus = Mid(DefaultText, 27, 1)
                                        SICCode = Mid(DefaultText, 35, 4)


                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", "", "", "", AirProgramCode, "", "", "", "", _
                                                             "", "", "", "", "", "", Pollutant, FacilityClassification, ComplianceStatus, AttainmentStatus, _
                                                             SICCode)
                                    End If
                                Case "141"
                                    'Ignore
                                Case "151"
                                    'Ignore
                                Case "161" 'Various Action Types
                                    If chb161Card.Checked = True Then
                                        ActionNumber = Mid(DefaultText, 14, 3)
                                        AirProgramCode = Mid(DefaultText, 17, 6)
                                        ActionType = Mid(DefaultText, 23, 2)
                                        DateScheduled = Mid(DefaultText, 41, 6)
                                        DateAcheived = Mid(DefaultText, 47, 6)
                                        ResultCode = Mid(DefaultText, 53, 2)
                                        PenaltyAmount = Mid(DefaultText, 55, 7)
                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", "", "", "", AirProgramCode, "", "", "", "", _
                                                             "", "", "", "", "", "", "", "", "", "", "", ActionNumber, ActionType, DateScheduled, DateAcheived, _
                                                             ResultCode, PenaltyAmount)
                                    End If
                                Case "163"
                                    If chb163Card.Checked = True Then
                                        ActionNumber = Mid(DefaultText, 14, 3)
                                        KeyActionNumber = Mid(DefaultText, 17, 3)
                                        Pollutant163 = Mid(DefaultText, 43, 5)

                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                             "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                             "", "", "", KeyActionNumber, Pollutant163)
                                    End If



                                Case "164"
                                    If chb164Card.Checked = True Then
                                        ActionNumber = Mid(DefaultText, 14, 3)
                                        ViolationType = Mid(DefaultText, 42, 3)
                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                             "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                             "", "", "", "", "", ViolationType)
                                    End If
                                Case "171" 'Comments
                                    'Ignore
                                Case "181"
                                    If chb181Card.Checked = True Then
                                        CMSStatus = Mid(DefaultText, 14, 2)
                                        dgvAFSData.Rows.Add(AIRSNumber, TransactionType, "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                             "", "", "", "", "", "", "", "", "", "", "", "", "", "", _
                                                             "", "", "", "", "", "", CMSStatus)
                                    End If
                                Case Else

                            End Select

                        Loop Until reader.Peek = -1
                        reader.Close()
                    End If
                End If
            Else

            End If
            txtAFSDataCount.Text = dgvAFSData.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub rdbCompareFullIAIPdata_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCompareFullIAIPdata.CheckedChanged
        Try
            If rdbCompareFullIAIPdata.Checked = True Then
                pnlCompareFull.Visible = True
                rdbCounty.Visible = True
                rdbAIRSNumber.Visible = True
            Else
                pnlCompareFull.Visible = False
                rdbCounty.Visible = False
                rdbAIRSNumber.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub rdbCounty_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCounty.CheckedChanged
        Try
            If rdbCounty.Checked = True Then
                cboCounty.Visible = True
            Else
                cboCounty.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub rdbAIRSNumber_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAIRSNumber.CheckedChanged
        Try
            If rdbAIRSNumber.Checked = True Then
                mtbAIRSNumber.Visible = True
            Else
                mtbAIRSNumber.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class