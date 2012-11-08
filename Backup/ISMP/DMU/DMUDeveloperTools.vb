﻿Imports System.DateTime
Imports System.Data.OracleClient
Imports System.IO

Public Class DMUDeveloperTools
    Dim dsStaff As DataSet
    Dim daStaff As New OracleDataAdapter
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim airsno As String
    Dim dsErrorLog As DataSet
    Dim daErrorLog As OracleDataAdapter
    Dim dsWebErrorLog As DataSet
    Dim daWebErrorLog As OracleDataAdapter
    Dim TriggerStatus As String

    Private Sub DMUDeveloperTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Panel1.Text = "Select a Function..."
            Panel2.Text = UserName
            Panel3.Text = OracleDate

            LoadPermissions()
            rdbDEVTransfer.Text = conn.DataSource.ToString & " --> DEV "
            rdbTESTTransfer.Text = conn.DataSource.ToString & " --> TEST "

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    
#Region "Page Load Functions"
    Sub LoadPermissions()
        Try

            TCDMUTools.TabPages.Remove(TPAFSFileGenerator)
            TCDMUTools.TabPages.Remove(TPAddNewFacility)
            TCDMUTools.TabPages.Remove(TPErrorLog)
            TCDMUTools.TabPages.Remove(TPWebErrorLog)
            TCDMUTools.TabPages.Remove(TPUpdateDEVTest)

            'AFS Users
            If AccountArray(129, 1) = "1" Then
                TCDMUTools.TabPages.Add(TPAFSFileGenerator)
                TCDMUTools.TabPages.Add(TPAddNewFacility)

                LoadOtherComboBoxes()

                DevelopersTools.Width = 800
                DevelopersTools.Height = 730
            End If
            'Web Publishers
            If AccountArray(129, 2) = "1" Then
                DevelopersTools.Width = 800
                DevelopersTools.Height = 600

            End If
            If AccountArray(129, 3) = "1" Or AccountArray(129, 4) = "1" Then
                TCDMUTools.TabPages.Add(TPErrorLog)
                rdbViewUnresolvedErrors.Checked = True
                FormatErrorListGrid()

                TCDMUTools.TabPages.Add(TPWebErrorLog)
                Me.rdbUnresolvedWebErrors.Checked = True
                FormatWebErrorListGrid()

                If SystemInformation.PrimaryMonitorSize.Width > 1200 Then
                    DevelopersTools.Width = (SystemInformation.PrimaryMonitorSize.Width - 400)
                Else
                    DevelopersTools.Width = 800
                End If
                DevelopersTools.Height = 1000
                TCDMUTools.TabPages.Add(TPUpdateDEVTest)
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LoadDataSetInformation()
        Try
            SQL = "select " & _
            "(strLastName||', '||strFirstName) as UserName,  " & _
            "numUserID  " & _
            "from AIRBranch.EPDUserProfiles  " & _
            "order by strLastName  "

            dsStaff = New DataSet

            daStaff = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            daStaff.Fill(dsStaff, "Staff")

           
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
        End Try
         
    End Sub
    Sub FormatErrorListGrid()
        Try
            
            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "ErrorLog"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            '0
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strErrorNumber"
            objtextcol.HeaderText = "Error #"
            objtextcol.Width = 50
            objGrid.GridColumnStyles.Add(objtextcol)

            '1
            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ErrorUser"
            objtextcol.HeaderText = "User"
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            '2
            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strErrorLocation"
            objtextcol.HeaderText = "Error Location"
            objtextcol.Width = 180
            objGrid.GridColumnStyles.Add(objtextcol)

            '3
            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ErrorDate"
            objtextcol.HeaderText = "Error Date"
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            '4
            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strSolution"
            objtextcol.HeaderText = "Error Solution"
            objtextcol.Width = 200
            objGrid.GridColumnStyles.Add(objtextcol)

            '5
            'Setting the Column Headings
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strErrorMessage"
            objtextcol.HeaderText = "Error Message"
            objtextcol.Width = 400
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrErrorList.TableStyles.Clear()
            dgrErrorList.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrErrorList.CaptionText = "Error Log"
            dgrErrorList.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            
        End Try
         

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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            
        End Try
         
    End Sub
    Private Sub LoadOtherComboBoxes()
        Try
            
            cboCDSOperationalStatus.Items.Add("O - Operating")
            cboCDSOperationalStatus.Items.Add("P - Planned")
            cboCDSOperationalStatus.Items.Add("C - Under Construction")
            cboCDSOperationalStatus.Items.Add("T - Temporarily Closed")
            cboCDSOperationalStatus.Items.Add("X - Permanently Closed")
            cboCDSOperationalStatus.Items.Add("I - Seasonal Operation")

            cboCDSClassCode.Items.Add("A - MAJOR")
            cboCDSClassCode.Items.Add("B - MINOR")
            cboCDSClassCode.Items.Add("C - UNKNOWN")
            cboCDSClassCode.Items.Add("SM - SYNTHETIC MINOR")
            cboCDSClassCode.Items.Add("PR - PERMIT BY RULE")
            cboCDSClassCode.Items.Add("U - UNDEFINED")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
         
    End Sub
#End Region

#Region "Subs and Functions"
    Sub GenerateBatchFile()
        Try
            Try
                GenerateNewFacilities()
                AddNewAirPollutants()
                GenerateFacilityChanges()
                PermittingActions()
                ISMPActions()
                ComplianceActions()
                FCEActions()
                EnforcementActions()
            Catch ex As Exception
                MsgBox("There was an error that occured while generating the batch." & vbCrLf & _
                     "What is currently run is saved and can be uploaded to AFS.", MsgBoxStyle.Exclamation, "DMU AFS Batch")
            End Try

            Dim FileName As String = ""
            Dim path As New SaveFileDialog
            Dim DestFilePath As String = "N/A"
            Dim OutPutFile As String = ""
            Dim da As OracleDataAdapter
            Dim cmdCB As OracleCommandBuilder
            Dim ds As DataSet

            If txtAFSBatchFile.Text <> "" Then

                SQL = "select " & connNameSpace & ".afsFileNumber.nextval from Dual"
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    FileName = "GA" & dr.Item(0)
                End While
                dr.Close()

                If FileName <> "" Then
                    path.InitialDirectory = "S:\ISMP\DMU\Production\APB IAIP\Batches"
                    path.FileName = FileName
                    path.Filter = "AFS Data (.txt)|.txt"
                    path.FilterIndex = 1
                    path.DefaultExt = ".txt"

                    If path.ShowDialog = Windows.Forms.DialogResult.OK Then
                        DestFilePath = path.FileName.ToString
                    Else
                        DestFilePath = "N/A"
                    End If

                    If DestFilePath <> "N/A" Then
                        Dim Encoder As New System.Text.ASCIIEncoding
                        Dim bytedata As Byte() = Encoder.GetBytes(txtAFSBatchFile.Text)

                        Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                        fs.Write(bytedata, 0, bytedata.Length)
                        fs.Close()

                        SQL = "Select * " & _
                        "from " & connNameSpace & ".AFSBatchFiles " & _
                        "where AFSFileName = '" & FileName & "' "

                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        da = New OracleDataAdapter(SQL, conn)
                        cmdCB = New OracleCommandBuilder(da)
                        ds = New DataSet("AFSData")
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey

                        da.Fill(ds, "AFSData")
                        Dim row As DataRow = ds.Tables("AFSData").NewRow()
                        row("AFSFileName") = FileName
                        row("AFSBatchFile") = bytedata
                        row("strStaffResponsible") = UserGCode
                        row("DatModifingDate") = OracleDate
                        ds.Tables("AFSData").Rows.Add(row)
                        da.Update(ds, "AFSData")
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub GenerateNewFacilities()
        Dim BatchText As String = ""
        Dim len As Integer
        Dim i As Integer

        Dim UpdateCode As String
        Dim AIRSNumber As String
        Dim UserAFSCode As String
        Dim FacilityName As String
        Dim FacilityStreet As String
        Dim FacilityCity As String
        Dim FacilityZipCode As String
        Dim SICCode As String
        Dim FacilityContactPerson As String
        Dim ContactPhoneNumber As String
        Dim PlantDesc As String
        Dim APCCheck As String = ""
        Dim AirProgramCode As String
        Dim Pollutant As String
        Dim Classification As String
        Dim ComplianceStatus As String
        Dim AttainmentStatus As String
        Dim AirProgramPollutantLines As String
        Dim CMSMember As String
        Dim Inspector As String
        Dim OperationalStatus As String
        Dim temp As String

        Try
            'G81 was Ender 
            'G62 is for Permitting Manager
            'G36 is for Compliance Manager 
            'GM8 is for Monitoring Manager 

            SQL = "Select " & connNameSpace & ".APBMasterAIRS.strAIRSNumber, " & _
            "strFacilityName, strFacilityStreet1,   " & _
            "strFacilityCity, strFacilityzipCode,   " & _
            "strSICCode, strContactFirstName,   " & _
            "strContactLastName, strContactTitle,   " & _
            "strContactPhoneNumber1, strPlantDescription,   " & _
            "" & connNameSpace & ".AFSFacilityData.strModifingPerson, strUpdateStatus,  " & _
            "strCMSMember, strAIrProgramcodes " & _
            "from " & connNameSpace & ".APBMasterAIRS, " & connNameSpace & ".APBFacilityInformation,  " & _
            "" & connNameSpace & ".APBHeaderData, " & connNameSpace & ".APBContactInformation,  " & _
            "" & connNameSpace & ".APBSupplamentalData, " & connNameSpace & ".AFSFacilityData  " & _
            "where " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".AFSFacilityData.strAIRSNumber    " & _
            "and " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".APBFacilityInformation.strAIRSnumber  " & _
            "and " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".APBHeaderData.strAIRSNumber   " & _
            "and " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".APBContactInformation.strAIRSNumber " & _
            "and " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".APBSupplamentalData.strAIRSNumber   " & _
            "and " & connNameSpace & ".APBContactInformation.strKEy = '30'  " & _
            "and strUpDateStatus = 'A'  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                temp = dr.Item("strUpdateStatus")
                Select Case temp
                    Case "A"
                        UpdateCode = "A"
                    Case Else
                        UpdateCode = "N"
                End Select
                If UpdateCode = "A" Then
                    If IsDBNull(dr.Item("strAIRSNumber")) Then
                        AIRSNumber = "N/A"
                    Else
                        AIRSNumber = Mid(dr.Item("strAIRSNumber"), 3)
                    End If
                    If AIRSNumber <> "N/A" Then
                        UserAFSCode = "G62"
                        If IsDBNull(dr.Item("StrFacilityname")) Then
                            FacilityName = "N/A"
                        Else
                            FacilityName = dr.Item("strFacilityName")
                        End If
                        If FacilityName.Length > 40 Then
                            FacilityName = Mid(FacilityName, 1, 40)
                        End If
                        len = FacilityName.Length
                        For i = len To 63
                            FacilityName = FacilityName & " "
                        Next
                        If IsDBNull(dr.Item("strFacilityStreet1")) Then
                            FacilityStreet = "N/A"
                        Else
                            FacilityStreet = dr.Item("strFacilityStreet1")
                        End If
                        If FacilityStreet.Length > 30 Then
                            FacilityStreet = Mid(FacilityStreet, 1, 30)
                        End If
                        len = FacilityStreet.Length
                        For i = len To 63
                            FacilityStreet = FacilityStreet & " "
                        Next
                        If IsDBNull(dr.Item("strFacilityCity")) Then
                            FacilityCity = "N/A"
                        Else
                            FacilityCity = dr.Item("strFacilityCity")
                        End If
                        If FacilityCity.Length > 30 Then
                            FacilityCity = Mid(FacilityCity, 1, 30)
                        End If
                        len = FacilityCity.Length
                        For i = len To 29
                            FacilityCity = FacilityCity & " "
                        Next
                        If IsDBNull(dr.Item("strFacilityZipCode")) Then
                            FacilityZipCode = "000000000"
                        Else
                            FacilityZipCode = Replace(Replace(dr.Item("strFacilityZipCode"), "-", ""), "N/A", "")
                        End If
                        If FacilityZipCode.Length > 5 Then
                            FacilityZipCode = Mid(FacilityZipCode, 1, 5)
                        End If
                        len = FacilityZipCode.Length
                        If FacilityZipCode.Length <> 9 Then
                            For i = len To 8
                                FacilityZipCode = FacilityZipCode & " "
                            Next
                        End If
                        If IsDBNull(dr.Item("strContactFirstName")) Then
                            FacilityContactPerson = ""
                        Else
                            FacilityContactPerson = dr.Item("strContactFirstName")
                        End If
                        If IsDBNull(dr.Item("strContactLastName")) Then
                            FacilityContactPerson = FacilityContactPerson
                        Else
                            If FacilityContactPerson <> "" Then
                                FacilityContactPerson = FacilityContactPerson & " " & dr.Item("strContactLastName")
                            Else
                                FacilityContactPerson = dr.Item("strContactLastName")
                            End If
                        End If
                        If FacilityContactPerson = "" Then
                            FacilityContactPerson = "N/A"
                        End If
                        If FacilityContactPerson.Length > 20 Then
                            FacilityContactPerson = Mid(FacilityContactPerson, 1, 20)
                        End If
                        len = FacilityContactPerson.Length
                        If FacilityContactPerson.Length <> 20 Then
                            For i = len To 19
                                FacilityContactPerson = FacilityContactPerson & " "
                            Next
                        End If
                        If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                            ContactPhoneNumber = "0000000000"
                        Else
                            ContactPhoneNumber = Replace(Replace(Replace(dr.Item("strContactPhoneNumber1"), ")", ""), "(", ""), "N/A", "")
                        End If
                        len = ContactPhoneNumber.Length
                        If len > 10 Then
                            ContactPhoneNumber = Mid(ContactPhoneNumber, 1, 10)
                        End If
                        If ContactPhoneNumber.Length <> 10 Then
                            For i = len To 9
                                ContactPhoneNumber = ContactPhoneNumber & "0"
                            Next
                        End If
                        If IsDBNull(dr.Item("strPlantDescription")) Then
                            PlantDesc = "N/A"
                        Else
                            PlantDesc = dr.Item("strPlantDescription")
                        End If
                        len = PlantDesc.Length
                        If PlantDesc.Length > 25 Then
                            PlantDesc = Mid(PlantDesc, 1, 25)
                        End If
                        len = PlantDesc.Length
                        If PlantDesc.Length <> 36 Then
                            For i = len To 35
                                PlantDesc = PlantDesc & " "
                            Next
                        End If
                        If IsDBNull(dr.Item("strCMSMember")) Then
                            CMSMember = "**"
                        Else
                            CMSMember = dr.Item("strCMSMember")
                            Select Case CMSMember
                                Case "A"
                                    CMSMember = CMSMember & "2"
                                Case "S"
                                    CMSMember = CMSMember & "5"
                                Case Else
                                    CMSMember = "**"
                            End Select
                        End If
                        len = CMSMember.Length
                        If len <> 66 Then
                            For i = len To 65
                                CMSMember = CMSMember & " "
                            Next
                        End If

                        Inspector = "G36"

                        If IsDBNull(dr.Item("strSICCode")) Then
                            SICCode = "0000"
                        Else
                            SICCode = dr.Item("strSICCode")
                        End If
                        If FacilityZipCode.Length > 4 Then
                            SICCode = Mid(dr.Item("strSICCode"), 1, 4)
                        End If
                        len = FacilityZipCode.Length
                        If SICCode.Length <> 4 Then
                            For i = len To 3
                                SICCode = SICCode & " "
                            Next
                        End If
                        SICCode = SICCode & "            " & Inspector & "      "

                        SQL2 = "Select " & _
                        "" & connNameSpace & ".AFSAirPollutantData.strAIRPollutantKey, " & _
                        "" & connNameSpace & ".AFSAirPollutantData.strPollutantKey, " & _
                        "strComplianceStatus, strClass, " & _
                        "" & connNameSpace & ".APBHeaderData.strAttainmentStatus, " & _
                        "" & connNameSpace & ".APBAirProgramPollutants.strOperationalStatus " & _
                        "from " & connNameSpace & ".APBAirProgramPollutants, " & connNameSpace & ".AFSAirPollutantData, " & _
                        "" & connNameSpace & ".APBHeaderData, " & connNameSpace & ".LookUpCountyInformation " & _
                        "where " & connNameSpace & ".APBAirProgramPollutants.strAIRSNumber = '" & dr.Item("strAIRSNumber") & "' " & _
                        "and " & connNameSpace & ".APBHeaderData.strAIRSNumber = " & connNameSpace & ".APBAirProgramPollutants.strAIRSNumber " & _
                        "and substr(" & connNameSpace & ".APBHeaderData.strAIRSNumber, 5, 3) = " & connNameSpace & ".LookUpCountyInformation.strCountyCode " & _
                        "and " & connNameSpace & ".AFSAirPollutantData.strAirPollutantKey = " & connNameSpace & ".APBAirProgramPollutants.strAirPollutantKey " & _
                        "and " & connNameSpace & ".AFSAirPollutantData.strPollutantKey = " & connNameSpace & ".APBAirProgramPollutants.strPollutantKey " & _
                        "and " & connNameSpace & ".AFSAirPollutantData.strAIRSNumber  = '04" & AIRSNumber & "' "
                        cmd2 = New OracleCommand(SQL2, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr2 = cmd2.ExecuteReader

                        AirProgramPollutantLines = ""

                        While dr2.Read
                            If IsDBNull(dr2.Item("strAIRPollutantKey")) Then
                                AirProgramCode = "0"
                            Else
                                AirProgramCode = Mid(dr2.Item("strAIRPollutantKey"), 13)
                            End If
                            If IsDBNull(dr2.Item("strOperationalStatus")) Then
                                OperationalStatus = "O"
                            Else
                                OperationalStatus = dr2.Item("strOperationalStatus")
                            End If
                            If IsDBNull(dr2.Item("strPollutantKey")) Then
                                Pollutant = "OT"
                            Else
                                Pollutant = dr2.Item("strPollutantKey")
                            End If

                            If Pollutant.Length > 9 Then
                                Pollutant = Mid(Pollutant, 1, 9)
                            End If
                            len = Pollutant.Length
                            If Pollutant.Length <> 9 Then
                                For i = len To 8
                                    Pollutant = Pollutant & " "
                                Next
                            End If
                            If IsDBNull(dr2.Item("strClass")) Then
                                Classification = "B "
                            Else
                                Classification = dr2.Item("strClass")
                            End If
                            Select Case Classification
                                Case "A"
                                    Classification = "A "
                                Case "SM"
                                    Classification = "SM"
                                Case "B"
                                    Classification = "B "
                                Case "C"
                                    Classification = "B "
                                Case "PR"
                                    Classification = "SM"
                                Case "U"
                                    Classification = "B "
                                Case Else
                                    Classification = "B "
                            End Select
                            If IsDBNull(dr2.Item("strComplianceStatus")) Then
                                ComplianceStatus = "C"
                            Else
                                ComplianceStatus = dr2.Item("strComplianceStatus")
                            End If
                            If IsDBNull(dr2.Item("strAttainmentStatus")) Then
                                AttainmentStatus = "A"
                            Else
                                AttainmentStatus = dr2.Item("strAttainmentStatus")
                                If AttainmentStatus = "True" Then
                                    AttainmentStatus = "A"
                                Else
                                    AttainmentStatus = "N"
                                End If
                            End If

                            'Pollutant = Pollutant & ComplianceStatus & Classification & AttainmentStatus & "                                                    "
                            'AirProgramPollutantLines = AirProgramPollutantLines & AIRSNumber & "121" & AirProgramCode & OperationalStatus & "                                                     " & UserAFSCode & "                    " & UpdateCode & vbCrLf & _
                            'AIRSNumber & "131" & AirProgramCode & Pollutant & UpdateCode & vbCrLf

                            Pollutant = Pollutant & ComplianceStatus & Classification & AttainmentStatus & "                                                    "
                            AirProgramPollutantLines = AirProgramPollutantLines & AIRSNumber & "121" & AirProgramCode & OperationalStatus & "                                                     " & "           " & UpdateCode & vbCrLf & _
                            AIRSNumber & "131" & AirProgramCode & Pollutant & UpdateCode & vbCrLf
                        End While
                        dr2.Close()

                        SQL2 = "Update " & connNameSpace & ".AFSAirPollutantData set " & _
                        "strUpDateStatus = 'N' " & _
                        "where strAIRSNumber = '" & dr.Item("strAIRSNumber") & "' "

                        cmd2 = New OracleCommand(SQL2, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If

                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        If IsDBNull(dr.Item("strAIrProgramcodes")) Then
                            APCCheck = "0"
                        Else
                            APCCheck = dr.Item("strAIrProgramcodes")
                        End If
                        If APCCheck <> "0" And APCCheck.Length > 12 Then
                            If Mid(APCCheck, 1, 1) = "0" And Mid(APCCheck, 12, 1) = "1" Then
                                BatchText = BatchText & _
            AIRSNumber & "101  " & FacilityName & UpdateCode & vbCrLf & _
           AIRSNumber & "102  " & FacilityStreet & UpdateCode & vbCrLf & _
            AIRSNumber & "103  " & FacilityCity & FacilityZipCode & SICCode & UpdateCode & vbCrLf & _
            AIRSNumber & "1040                                                                 " & UpdateCode & vbCrLf & _
            AIRSNumber & "105" & FacilityContactPerson & ContactPhoneNumber & PlantDesc & UpdateCode & vbCrLf & _
            AirProgramPollutantLines & _
           AIRSNumber & "181" & CMSMember & UpdateCode & vbCrLf & _
         AIRSNumber & "16190000M     00 EPA ACT.>90000       " & Format(Date.Today, "yyMMdd") & "                        NA" & vbCrLf
                            Else
                                BatchText = BatchText & _
          AIRSNumber & "101  " & FacilityName & UpdateCode & vbCrLf & _
          AIRSNumber & "102  " & FacilityStreet & UpdateCode & vbCrLf & _
          AIRSNumber & "103  " & FacilityCity & FacilityZipCode & SICCode & UpdateCode & vbCrLf & _
          AIRSNumber & "1040                                                                 " & UpdateCode & vbCrLf & _
          AIRSNumber & "105" & FacilityContactPerson & ContactPhoneNumber & PlantDesc & UpdateCode & vbCrLf & _
          AirProgramPollutantLines & _
          AIRSNumber & "181" & CMSMember & UpdateCode & vbCrLf & _
          AIRSNumber & "161900000     00 EPA ACT.>90000       " & Format(Date.Today, "yyMMdd") & "                        NA" & vbCrLf
                            End If
                        Else
                            BatchText = BatchText & _
                            AIRSNumber & "101  " & FacilityName & UpdateCode & vbCrLf & _
                            AIRSNumber & "102  " & FacilityStreet & UpdateCode & vbCrLf & _
                            AIRSNumber & "103  " & FacilityCity & FacilityZipCode & SICCode & UpdateCode & vbCrLf & _
                            AIRSNumber & "1040                                                                 " & UpdateCode & vbCrLf & _
                            AIRSNumber & "105" & FacilityContactPerson & ContactPhoneNumber & PlantDesc & UpdateCode & vbCrLf & _
                            AirProgramPollutantLines & _
                            AIRSNumber & "181" & CMSMember & UpdateCode & vbCrLf & _
                            AIRSNumber & "161900000     00 EPA ACT.>90000       " & Format(Date.Today, "yyMMdd") & "                        NA" & vbCrLf
                        End If
                        'This was the original separator for entries at 700 after Dec 2009 the separator moved to 90,000 - MFloyd 
                        ' AIRSNumber & "1617000     00 EPA ACTION >700      " & Format(Date.Today, "yyMMdd") & "                          NA" & vbCrLf
                    End If
                End If
            End While
            dr.Close()

            SQL = "Update " & connNameSpace & ".AFSFacilityData set " & _
            "strUpDateStatus = 'N' " & _
            "where strUpDateStatus = 'A' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            txtAFSBatchFile.Text = BatchText
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
    End Sub
    Sub AddNewAirPollutants()
        Dim AirProgramPollutantLines As String
        Dim len As Integer
        Dim i As Integer

        Dim UpdateCode As String
        Dim AIRSNumber As String
        Dim UserAFSCode As String
        Dim Pollutant As String
        Dim Classification As String
        Dim ComplianceStatus As String
        Dim AttainmentStatus As String
        Dim AirProgramCode As String
        Dim OperationalStatus As String
        Dim Subpart As String
        Dim SubpartData As String

        SQL = "Select " & _
        "" & connNameSpace & ".AFSAirPollutantData.strAIRSNUmber, " & _
        "" & connNameSpace & ".AFSAirPollutantData.strAIRPollutantKey, " & _
        "" & connNameSpace & ".AFSAirPollutantData.strPollutantKey, " & _
        "" & connNameSpace & ".APBAirProgramPollutants.strComplianceStatus, " & _
        "" & connNameSpace & ".APBHeaderData.strClass, " & _
        "" & connNameSpace & ".APBHeaderData.strAttainmentStatus, " & _
        "" & connNameSpace & ".AFSAirPollutantData.strUpdatestatus, " & _
        "" & connNameSpace & ".APBAirProgramPollutants.strOperationalStatus " & _
        "from " & connNameSpace & ".APBAirProgramPollutants, " & connNameSpace & ".AFSAirPollutantData, " & _
        "" & connNameSpace & ".APBHeaderData, " & connNameSpace & ".LookUpCountyInformation,  " & _
        "" & connNameSpace & ".AFSFacilityData " & _
        "where " & _
        "" & connNameSpace & ".APBHeaderData.strAIRSNumber = " & connNameSpace & ".APBAirProgramPollutants.strAIRSNumber " & _
        "and substr(" & connNameSpace & ".APBHeaderData.strAIRSNumber, 5, 3) = " & connNameSpace & ".LookUpCountyInformation.strCountyCode " & _
        "and " & connNameSpace & ".AFSAirPollutantData.strAirPollutantKey = " & connNameSpace & ".APBAirProgramPollutants.strAirPollutantKey " & _
        "and " & connNameSpace & ".AFSAirPollutantData.strPollutantKey = " & connNameSpace & ".APBAirProgramPollutants.strPollutantKey " & _
        " and " & connNameSpace & ".AFSAirPollutantData.strAIRSNUmber = " & connNameSpace & ".AFSFacilityData.strAIRSNUmber " & _
        "and " & connNameSpace & ".AFSAirPollutantData.strUpdateStatus <> 'N' " & _
        " and " & connNameSpace & ".AFSFacilityData.strUpdateStatus <>  'H' "

        Try
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader

            AirProgramPollutantLines = ""

            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = Mid(dr.Item("strAIRSNumber"), 3)
                End If

                If AIRSNumber.Length = 10 Then
                    If IsDBNull(dr.Item("strUpdateStatus")) Then
                        UpdateCode = "A"
                    Else
                        UpdateCode = dr.Item("strUpdateStatus")
                    End If
                    If IsDBNull(dr.Item("strAIRPollutantKey")) Then
                        AirProgramCode = "0"
                    Else
                        AirProgramCode = Mid(dr.Item("strAIRPollutantKey"), 13)
                    End If
                    If IsDBNull(dr.Item("strOperationalStatus")) Then
                        OperationalStatus = "O"
                    Else
                        OperationalStatus = dr.Item("strOperationalStatus")
                    End If
                    UserAFSCode = "G62"
                    If IsDBNull(dr.Item("strPollutantKey")) Then
                        Pollutant = "OT"
                    Else
                        Pollutant = dr.Item("strPollutantKey")
                    End If

                    If Pollutant.Length > 9 Then
                        Pollutant = Mid(Pollutant, 1, 9)
                    End If
                    len = Pollutant.Length
                    If Pollutant.Length <> 9 Then
                        For i = len To 8
                            Pollutant = Pollutant & " "
                        Next
                    End If
                    If IsDBNull(dr.Item("strClass")) Then
                        Classification = "B "
                    Else
                        Classification = dr.Item("strClass")
                    End If
                    Select Case Classification
                        Case "A"
                            Classification = "A "
                        Case "SM"
                            Classification = "SM"
                        Case "B"
                            Classification = "B "
                        Case "C"
                            Classification = "B "
                        Case "PR"
                            Classification = "SM"
                        Case "U"
                            Classification = "B "
                        Case Else
                            Classification = "B "
                    End Select
                    If IsDBNull(dr.Item("strComplianceStatus")) Then
                        ComplianceStatus = "C"
                    Else
                        ComplianceStatus = dr.Item("strComplianceStatus")
                    End If
                    If IsDBNull(dr.Item("strAttainmentStatus")) Then
                        AttainmentStatus = "A"
                    Else
                        AttainmentStatus = dr.Item("strAttainmentStatus")
                        If AttainmentStatus <> "00000" Then
                            AttainmentStatus = "A"
                        Else
                            AttainmentStatus = "N"
                        End If
                    End If

                    'Pollutant = Pollutant & ComplianceStatus & Classification & AttainmentStatus & "      "
                    'AirProgramPollutantLines = AirProgramPollutantLines & AIRSNumber & "121" & _
                    'AirProgramCode & OperationalStatus & "                                         " & UserAFSCode & "                    " & UpdateCode & vbCrLf & _
                    'AIRSNumber & "131" & AirProgramCode & Pollutant & "                                              " & UpdateCode & vbCrLf

                    Pollutant = Pollutant & ComplianceStatus & Classification & AttainmentStatus & "                                                    "
                    AirProgramPollutantLines = AirProgramPollutantLines & AIRSNumber & "121" & AirProgramCode & OperationalStatus & "                                                     " & "           " & UpdateCode & vbCrLf & _
                    AIRSNumber & "131" & AirProgramCode & Pollutant & UpdateCode & vbCrLf

                End If
            End While
            dr.Close()

            Subpart = ""
            SubpartData = ""

            SQL = "Select " & _
            "distinct(substr(strSubPartkey, 13, 1)) as subpart,  " & _
            "" & connNameSpace & ".APBSubpartData.strAIRSnumber, " & _
            "" & connNameSpace & ".AFSAirPollutantData.strUpdateStatus    " & _
            "from " & connNameSpace & ".APBSubpartData, " & connNameSpace & ".AFSAirPollutantData,   " & _
            "" & connNameSpace & ".AFSFacilityData " & _
            "where " & connNameSpace & ".APBSubpartData.strSubpartKey = " & _
                   "" & connNameSpace & ".AFSAirPollutantData.strAIRPollutantKey  " & _
            " and " & connNameSpace & ".AFSAirPollutantData.strAIRSNUmber = " & _
                   "" & connNameSpace & ".AFSFacilityData.strAIRSNUmber  " & _
            "and " & connNameSpace & ".AFSAirPollutantData.strUpdateStatus <> 'N' " & _
            " and " & connNameSpace & ".AFSFacilityData.strUpdateStatus <>  'H' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                AirProgramCode = ""
                AIRSNumber = ""
                UpdateCode = "N"

                If IsDBNull(dr.Item("SubPart")) Then
                    AirProgramCode = ""
                Else
                    AirProgramCode = dr.Item("SubPart")
                    If IsDBNull(dr.Item("strAIRSnumber")) Then
                    Else
                        AIRSNumber = Mid(dr.Item("strAIRSnumber"), 3)
                    End If
                    If IsDBNull(dr.Item("strUpdateStatus")) Then
                    Else
                        UpdateCode = dr.Item("strUpdateStatus")
                    End If
                End If

                Select Case AirProgramCode
                    Case "8"
                        SQL = "Select " & _
                        "strSubPart " & _
                        "from " & connNameSpace & ".APBSubpartData " & _
                        "where strSubpartKey = '04" & AIRSNumber & "8' "

                        cmd2 = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("strSubPart")) Then
                                Subpart = ""
                            Else
                                Subpart = dr2.Item("strSubpart")
                                If Subpart <> "" Then
                                    If Subpart.Length > 5 Then
                                        Subpart = Mid(Subpart, 1, 5)
                                    End If
                                    len = Subpart.Length
                                    If Subpart.Length <> 5 Then
                                        For i = len To 4
                                            Subpart = Subpart & " "
                                        Next
                                    End If
                                    SubpartData = SubpartData & Subpart
                                    Subpart = ""
                                End If
                            End If
                        End While
                        dr2.Close()
                        If SubpartData <> "" Then
                            If SubpartData.Length > 64 Then
                                SubpartData = Mid(SubpartData, 1, 64)
                            End If
                            len = SubpartData.Length
                            If SubpartData.Length <> 64 Then
                                For i = len To 63
                                    SubpartData = SubpartData & " "
                                Next
                            End If
                            SubpartData = AIRSNumber & "1228A" & SubpartData & UpdateCode
                        End If
                    Case "9"
                        SQL = "Select " & _
                        "strSubPart " & _
                        "from " & connNameSpace & ".APBSubpartData " & _
                        "where strSubpartKey = '04" & AIRSNumber & "9' "

                        cmd2 = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("strSubPart")) Then
                                Subpart = ""
                            Else
                                Subpart = dr2.Item("strSubpart")
                                If Subpart <> "" Then
                                    If Subpart.Length > 5 Then
                                        Subpart = Mid(Subpart, 1, 5)
                                    End If
                                    len = Subpart.Length
                                    If Subpart.Length <> 5 Then
                                        For i = len To 4
                                            Subpart = Subpart & " "
                                        Next
                                    End If
                                    SubpartData = SubpartData & Subpart
                                    Subpart = ""
                                End If
                            End If
                        End While
                        dr2.Close()
                        If SubpartData <> "" Then
                            If SubpartData.Length > 64 Then
                                SubpartData = Mid(SubpartData, 1, 64)
                            End If
                            len = SubpartData.Length
                            If SubpartData.Length <> 64 Then
                                For i = len To 63
                                    SubpartData = SubpartData & " "
                                Next
                            End If
                            SubpartData = AIRSNumber & "1229A" & SubpartData & UpdateCode
                        End If
                    Case "M"
                        SQL = "Select " & _
                         "strSubPart " & _
                         "from " & connNameSpace & ".APBSubpartData " & _
                         "where strSubpartKey = '04" & AIRSNumber & "M' "

                        cmd2 = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("strSubPart")) Then
                                Subpart = ""
                            Else
                                Subpart = dr2.Item("strSubpart")
                                If Subpart <> "" Then
                                    If Subpart.Length > 5 Then
                                        Subpart = Mid(Subpart, 1, 5)
                                    End If
                                    len = Subpart.Length
                                    If Subpart.Length <> 5 Then
                                        For i = len To 4
                                            Subpart = Subpart & " "
                                        Next
                                    End If
                                    SubpartData = SubpartData & Subpart
                                    Subpart = ""
                                End If
                            End If
                        End While
                        dr2.Close()
                        If SubpartData <> "" Then
                            If SubpartData.Length > 64 Then
                                SubpartData = Mid(SubpartData, 1, 64)
                            End If
                            len = SubpartData.Length
                            If SubpartData.Length <> 64 Then
                                For i = len To 63
                                    SubpartData = SubpartData & " "
                                Next
                            End If
                            SubpartData = AIRSNumber & "122MA" & SubpartData & UpdateCode
                        End If
                    Case Else
                        SubpartData = ""
                End Select
                If SubpartData <> "" Then
                    AirProgramPollutantLines = AirProgramPollutantLines & SubpartData & vbCrLf
                End If
                SubpartData = ""
                Subpart = ""

            End While

            'SQL = "Update " & connNameSpace & ".AFSAirPollutantData set " & _
            '"strUpDateStatus = 'N' " & _
            '"where strUpDateStatus <> 'N' "

            SQL = "Update " & connNameSpace & ".AFSAirPollutantData set " & _
            "" & connNameSpace & ".AFSAirPollutantData.strUpDateStatus = 'N' " & _
            "where " & connNameSpace & ".AFSAirPollutantData.strUpdateStatus <> 'N' " & _
            "and exists (select * from " & connNameSpace & ".AFSFacilityData " & _
            "where " & connNameSpace & ".AFSAirPollutantData.STRAIRSNUMBER = " & connNameSpace & ".AFSFacilityData.STRAIRSNUMBER " & _
            "and " & connNameSpace & ".afsFacilityData.strUpdateStatus <> 'H' ) "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            dr = cmd.ExecuteReader
            dr.Close()

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & AirProgramPollutantLines

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub GenerateFacilityChanges()
        Dim BatchText As String = ""
        Dim len As Integer
        Dim i As Integer

        Dim UpdateCode As String
        Dim UpdateCode2 As String
        Dim AIRSNumber As String
        Dim UserAFSCode As String
        Dim FacilityName As String
        Dim FacilityStreet As String
        Dim FacilityCity As String
        Dim FacilityZipCode As String
        Dim SICCode As String
        Dim FacilityContactPerson As String
        Dim ContactPhoneNumber As String
        Dim PlantDesc As String
        Dim AirProgramCode As String
        Dim Pollutant As String
        Dim AirProgramPollutantLines As String
        Dim Inspector As String
        Dim OperationalStatus As String
        Dim CMSMember As String
        Dim temp As String = ""

        SQL = "Select " & connNameSpace & ".APBMasterAIRS.strAIRSNumber, " & _
        "strFacilityName, strFacilityStreet1,  " & _
        "strFacilityCity, strFacilityzipCode,  " & _
        "strSICCode, strContactFirstName,  " & _
        "strContactLastName, strContactTitle,  " & _
        "strContactPhoneNumber1, strPlantDescription,  " & _
        "" & connNameSpace & ".AFSFacilityData.strModifingPerson, strUpdateStatus, " & _
        "strCMSMember " & _
        "from " & connNameSpace & ".APBMasterAIRS, " & connNameSpace & ".APBFacilityInformation, " & _
        "" & connNameSpace & ".APBHeaderData, " & connNameSpace & ".APBContactInformation, " & _
        "" & connNameSpace & ".APBSupplamentalData, " & connNameSpace & ".AFSFacilityData " & _
        "where " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".AFSFacilityData.strAIRSNumber   " & _
        "and " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".APBFacilityInformation.strAIRSnumber " & _
        "and " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".APBHeaderData.strAIRSNumber  " & _
        "and " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".APBContactInformation.strAIRSNumber  " & _
        "and " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".APBSupplamentalData.strAIRSNumber  " & _
        "and " & connNameSpace & ".APBContactInformation.strKEy = '30' " & _
        "and strUpDateStatus = 'C' "

        Try
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                temp = dr.Item("strUpdateStatus")
                Select Case temp
                    Case "C"
                        UpdateCode = "C"
                    Case Else
                        UpdateCode = "N"
                End Select
                If UpdateCode = "C" Then
                    If IsDBNull(dr.Item("strAIRSNumber")) Then
                        AIRSNumber = "N/A"
                    Else
                        AIRSNumber = Mid(dr.Item("strAIRSNumber"), 3)
                    End If
                    If AIRSNumber <> "N/A" Then
                        UserAFSCode = "G62"
                        If IsDBNull(dr.Item("StrFacilityname")) Then
                            FacilityName = "N/A"
                        Else
                            FacilityName = dr.Item("strFacilityName")
                        End If
                        len = FacilityName.Length
                        If len > 40 Then
                            FacilityName = Mid(FacilityName, 1, 40)
                        End If
                        len = FacilityName.Length
                        For i = len To 63
                            FacilityName = FacilityName & " "
                        Next
                        If IsDBNull(dr.Item("strFacilityStreet1")) Then
                            FacilityStreet = "N/A"
                        Else
                            FacilityStreet = dr.Item("strFacilityStreet1")
                        End If
                        len = FacilityStreet.Length
                        If len > 30 Then
                            FacilityStreet = Mid(FacilityStreet, 1, 30)
                        End If
                        len = FacilityStreet.Length
                        For i = len To 63
                            FacilityStreet = FacilityStreet & " "
                        Next
                        If IsDBNull(dr.Item("strFacilityCity")) Then
                            FacilityCity = "N/A"
                        Else
                            FacilityCity = dr.Item("strFacilityCity")
                        End If
                        len = FacilityCity.Length
                        If len > 30 Then
                            FacilityCity = Mid(FacilityCity, 1, 30)
                        End If
                        For i = len To 29
                            FacilityCity = FacilityCity & " "
                        Next
                        If IsDBNull(dr.Item("strFacilityZipCode")) Then
                            FacilityZipCode = "000000000"
                        Else
                            FacilityZipCode = Replace(dr.Item("strFacilityZipCode"), "-", "")
                        End If
                        len = FacilityZipCode.Length
                        If len > 9 Then
                            FacilityZipCode = Mid(FacilityZipCode, 1, 9)
                        End If
                        If FacilityZipCode.Length <> 9 Then
                            For i = len To 8
                                FacilityZipCode = FacilityZipCode & " "
                            Next
                        End If
                        If IsDBNull(dr.Item("strContactFirstName")) Then
                            FacilityContactPerson = ""
                        Else
                            FacilityContactPerson = dr.Item("strContactFirstName")
                        End If
                        If IsDBNull(dr.Item("strContactLastName")) Then
                            FacilityContactPerson = FacilityContactPerson
                        Else
                            If FacilityContactPerson <> "" Then
                                FacilityContactPerson = FacilityContactPerson & " " & dr.Item("strContactLastName")
                            Else
                                FacilityContactPerson = dr.Item("strContactLastName")
                            End If
                        End If
                        If FacilityContactPerson = "" Then
                            FacilityContactPerson = "N/A"
                        End If
                        len = FacilityContactPerson.Length
                        If len > 20 Then
                            FacilityContactPerson = Mid(FacilityContactPerson, 1, 20)
                        End If
                        len = FacilityContactPerson.Length
                        If FacilityContactPerson.Length <> 20 Then
                            For i = len To 19
                                FacilityContactPerson = FacilityContactPerson & " "
                            Next
                        End If
                        If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                            ContactPhoneNumber = "0000000000"
                        Else
                            ContactPhoneNumber = Replace(Replace(dr.Item("strContactPhoneNumber1"), "(", ""), ")", "")
                        End If
                        len = ContactPhoneNumber.Length
                        If len > 10 Then
                            ContactPhoneNumber = Mid(ContactPhoneNumber, 1, 10)
                        End If
                        len = ContactPhoneNumber.Length
                        If ContactPhoneNumber.Length <> 10 Then
                            For i = len To 9
                                ContactPhoneNumber = ContactPhoneNumber & "0"
                            Next
                        End If
                        If IsDBNull(dr.Item("strPlantDescription")) Then
                            PlantDesc = "N/A"
                        Else
                            PlantDesc = Replace(dr.Item("strPlantDescription"), "'", "")
                        End If
                        len = PlantDesc.Length
                        If len > 25 Then
                            PlantDesc = Mid(PlantDesc, 1, 25)
                        End If
                        len = PlantDesc.Length
                        If PlantDesc.Length <> 36 Then
                            For i = len To 35
                                PlantDesc = PlantDesc & " "
                            Next
                        End If

                        Inspector = "G36"

                        If IsDBNull(dr.Item("strSICCode")) Then
                            SICCode = "0000"
                        Else
                            SICCode = dr.Item("strSICCode")
                        End If

                        If FacilityZipCode.Length > 4 Then
                            SICCode = Mid(dr.Item("strSICCode"), 1, 4)
                        End If
                        len = FacilityZipCode.Length
                        If SICCode.Length <> 4 Then
                            For i = len To 3
                                SICCode = SICCode & " "
                            Next
                        End If
                        SICCode = SICCode & SICCode & SICCode & "    " & Inspector & "      "

                        If IsDBNull(dr.Item("strCMSMember")) Then
                            CMSMember = "**"
                        Else
                            CMSMember = dr.Item("strCMSMember")
                            Select Case CMSMember
                                Case "A"
                                    CMSMember = CMSMember & "2"
                                Case "S"
                                    CMSMember = CMSMember & "5"
                                Case Else
                                    CMSMember = "**"
                            End Select
                        End If
                        len = CMSMember.Length
                        If len <> 66 Then
                            For i = len To 65
                                CMSMember = CMSMember & " "
                            Next
                        End If

                        SQL2 = "Select " & _
                        "" & connNameSpace & ".AFSAirPollutantData.strAIRPollutantKey, " & _
                        "" & connNameSpace & ".AFSAirPollutantData.strPollutantKey, " & _
                        "strComplianceStatus, strUpdateStatus, " & _
                        "strOperationalStatus " & _
                        "from " & connNameSpace & ".APBAirProgramPollutants, " & connNameSpace & ".AFSAirPollutantData " & _
                        "where " & connNameSpace & ".APBAirProgramPollutants.strAIRSNumber = '" & dr.Item("strAIRSNumber") & "' " & _
                        "and " & connNameSpace & ".AFSAirPollutantData.strAirPollutantKey = " & connNameSpace & ".APBAirProgramPollutants.strAirPollutantKey " & _
                        "and " & connNameSpace & ".AFSAirPollutantData.strPollutantKey = " & connNameSpace & ".APBAirProgramPollutants.strPollutantKey " & _
                        "and strUpdateStatus <> 'N' "

                        cmd2 = New OracleCommand(SQL2, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr2 = cmd2.ExecuteReader

                        AirProgramPollutantLines = ""

                        While dr2.Read
                            If IsDBNull(dr2.Item("strAIRPollutantKey")) Then
                                AirProgramCode = "0"
                            Else
                                AirProgramCode = Mid(dr2.Item("strAIRPollutantKey"), 13)
                            End If
                            If IsDBNull(dr2.Item("strOperationalStatus")) Then
                                OperationalStatus = "O"
                            Else
                                OperationalStatus = dr2.Item("strOperationalStatus")
                            End If
                            If IsDBNull(dr2.Item("strPollutantKey")) Then
                                Pollutant = "OT"
                            Else
                                Pollutant = dr2.Item("strPollutantKey")
                            End If
                            len = Pollutant.Length
                            If len > 9 Then
                                Pollutant = Mid(Pollutant, 1, 9)
                            End If
                            If Pollutant.Length <> 9 Then
                                For i = len To 8
                                    Pollutant = Pollutant & " "
                                Next
                            End If
                            If IsDBNull(dr2.Item("strUpdateStatus")) Then
                                UpdateCode2 = "N"
                            Else
                                UpdateCode2 = dr2.Item("strUpdateStatus")
                            End If
                            Pollutant = Pollutant & "4A A                                                    "
                            AirProgramPollutantLines = AirProgramPollutantLines & AIRSNumber & "121" & AirProgramCode & OperationalStatus & "                                         " & "                       " & UpdateCode2 & vbCrLf & _
                            AIRSNumber & "131" & AirProgramCode & Pollutant & UpdateCode2 & vbCrLf
                        End While
                        dr2.Close()

                        SQL2 = "Update " & connNameSpace & ".AFSAirPollutantData set " & _
                        "strUpDateStatus = 'N' " & _
                        "where strAIRSNumber = '" & dr.Item("strAIRSNumber") & "' "

                        cmd2 = New OracleCommand(SQL2, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If

                        dr2 = cmd2.ExecuteReader
                        dr2.Close()

                        BatchText = BatchText & _
                        AIRSNumber & "101  " & FacilityName & UpdateCode & vbCrLf & _
                        AIRSNumber & "102  " & FacilityStreet & UpdateCode & vbCrLf & _
                        AIRSNumber & "103  " & FacilityCity & FacilityZipCode & SICCode & UpdateCode & vbCrLf & _
                        AIRSNumber & "1040                                                                 " & UpdateCode & vbCrLf & _
                        AIRSNumber & "105" & FacilityContactPerson & ContactPhoneNumber & PlantDesc & UpdateCode & vbCrLf & _
                        AirProgramPollutantLines & _
                        AIRSNumber & "181" & CMSMember & UpdateCode & vbCrLf
                    End If
                End If
            End While
            dr.Close()

            SQL = "Update " & connNameSpace & ".AFSFacilityData set " & _
            "strUpDateStatus = 'N' " & _
            "where strUpDateStatus = 'C' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & BatchText

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        
        End Try
         

    End Sub
    Sub PermittingActions()
        Dim BatchText As String = ""
        Dim Comments As String = ""
        Dim PermitFile As String = ""
        Dim len As Integer
        Dim i As Integer

        Dim UpdateCode As String = ""
        Dim AIRSNumber As String = ""
        Dim AIRProgramCodes As String = ""
        Dim UserAFSCode As String = ""
        Dim ActionNumber As String = ""
        Dim ActionType As String = ""
        Dim ResultsCode As String = ""
        Dim ActionComments As String = ""
        Dim ActionComments2 As String = ""
        Dim ActionComments3 As String = ""
        Dim ActionComments4 As String = ""
        Dim DateAcheived As String = ""
        Dim DateExpire As String = ""
        Dim SubmitPermit As String = ""
        Dim PermitState As String = ""
        Dim PermitNumber As String = ""

        SQL = "Select " & _
        "" & connNameSpace & ".ssppapplicationmaster.strApplicationnumber, " & _
        "strAFSActionNumber, strUpDateStatus,  " & _
        "" & connNameSpace & ".SSPPApplicationMaster.strAIRSNumber, " & _
        "" & connNameSpace & ".SSPPApplicationMaster.strStaffResponsible,  " & _
        "Case " & _
        "    when strPermitType = '1' then '35' " & _
        "    when strPermitType = '4' then '33' " & _
        "    when strPermitType = '5' then '36' " & _
        "    when strPermitType = '7' then '33' " & _
        "    when strPermitType = '10' then '34' " & _
        "    when strPermitType = '12' then '33' " & _
        "    when strPermitType = '13' then '33' " & _
        "    else '00' " & _
        "end as strActionType, " & _
        "" & connNameSpace & ".SSPPApplicationData.strComments, " & _
        "to_Char(" & connNameSpace & ".SSPPApplicationMaster.DatFinalizedDate, 'YYMMDD') as AchievedDate, " & _
        "" & connNameSpace & ".SSPPApplicationData.strAirProgramCodes, " & _
        "" & connNameSpace & ".SSPPApplicationData.strPermitNumber " & _
        "from " & connNameSpace & ".AFSSSPPRecords, " & connNameSpace & ".SSPPApplicationMaster,  " & _
        "" & connNameSpace & ".SSPPApplicationData " & _
        "where strUpDateStatus <> 'N'  " & _
        "and " & connNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & connNameSpace & ".SSPPApplicationData.strApplicationNumber  " & _
        "and " & connNameSpace & ".AFSSSPPRecords.strApplicationNumber = " & connNameSpace & ".SSPPApplicationMaster.strApplicationNumber " & _
        "and (strPermitType = '1' or strPermitType = '4' or strPermitType = '5' " & _
        "or strPermitType = '7' or strPermitType = '10' or strPermitType = '12' " & _
        "or strPermitType = '13') " & _
        "and " & connNameSpace & ".SSPPApplicationMaster.strAIRSNumber not like '%APL%' "

        Try
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                AIRSNumber = Mid(dr.Item("strAIRSNumber"), 3, 10)
                If AIRSNumber.Length = 10 Then
                    If IsDBNull(dr.Item("strUpDatestatus")) Then
                        UpdateCode = "A"
                    Else
                        UpdateCode = dr.Item("strUpDateStatus")
                    End If
                    AIRProgramCodes = ""
                    If IsDBNull(dr.Item("strAIRProgramCodes")) Then
                        AIRProgramCodes = "0"
                    Else
                        If Mid(dr.Item("strAIRProgramCodes"), 1, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "0"
                        End If
                        'If Mid(dr.Item("strAIRProgramCodes"), 2, 1) = 1 Then
                        '    AIRProgramCodes = AIRProgramCodes & "1"
                        'End If
                        If Mid(dr.Item("strAIRProgramCodes"), 3, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "3"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 4, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "4"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 5, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "6"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 6, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "7"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 7, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "8"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 8, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "9"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 9, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "F"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 10, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "A"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 11, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "I"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 12, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "M"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 13, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "V"
                            SubmitPermit = "V"
                        Else
                            SubmitPermit = ""
                        End If
                        If AIRProgramCodes = "" Then
                            AIRProgramCodes = "0"
                        End If
                    End If
                    If AIRProgramCodes.Length > 6 Then
                        AIRProgramCodes = Mid(AIRProgramCodes, 1, 6)
                    End If
                    len = AIRProgramCodes.Length
                    If len <> 6 Then
                        For i = len To 5
                            AIRProgramCodes = AIRProgramCodes & " "
                        Next
                    End If
                    If IsDBNull(dr.Item("strPermitNumber")) Then
                        PermitNumber = ""
                    Else
                        PermitNumber = dr.Item("strPermitNumber")
                    End If
                    len = PermitNumber.Length
                    If PermitNumber.Length > 15 Then
                        PermitNumber = Mid(PermitNumber, 1, 15)
                    End If
                    If len <> 15 Then
                        For i = len To 14
                            PermitNumber = PermitNumber & " "
                        Next
                    End If

                    If IsDBNull(dr.Item("strAFSActionNumber")) Then
                        ActionNumber = "00001"

                        'After Dec 2009 Action numbers were updated from 3-digits to 5-digits
                        ' ActionNumber = "001"
                    Else
                        ActionNumber = dr.Item("strAFSActionNumber")
                        Select Case ActionNumber.Length
                            Case "0"
                                ActionNumber = "00001"
                            Case "1"
                                ActionNumber = "0000" & ActionNumber
                            Case "2"
                                ActionNumber = "000" & ActionNumber
                            Case "3"
                                ActionNumber = "00" & ActionNumber
                            Case "4"
                                ActionNumber = "0" & ActionNumber
                            Case "5"
                                ActionNumber = ActionNumber
                            Case Else
                                ActionNumber = "00001"
                        End Select
                    End If
                    'strStaffResponsible
                    UserAFSCode = "G62"
                   
                    len = UserAFSCode.Length

                    'This was changed when AFS went to 5-digit Action #
                    'If len <> 18 Then
                    '    For i = len To 17
                    '        UserAFSCode = UserAFSCode & " "
                    '    Next
                    'End If
                    If len <> 16 Then
                        For i = len To 15
                            UserAFSCode = UserAFSCode & " "
                        Next
                    End If

                    If IsDBNull(dr.Item("strActionType")) Then
                        ActionType = "00"
                    Else
                        ActionType = dr.Item("strActionType")
                    End If
                    If ActionType = "N " Then
                        ActionType = "00"
                    End If
                    Select Case ActionType
                        Case "33"
                            ResultsCode = "01"
                            PermitState = "Issued"
                        Case "34"
                            ResultsCode = "01"
                        Case "35"
                            ResultsCode = "01"
                            PermitState = "Admend"
                        Case "36"
                            ResultsCode = "01"
                            PermitState = "No Permit Req"
                        Case "38"
                            ResultsCode = "01"
                        Case "00"
                            ResultsCode = "01"
                            PermitState = "Draft"
                        Case Else
                            ResultsCode = "01"
                    End Select
                    len = ActionType.Length
                    If len <> 18 Then
                        For i = len To 17
                            ActionType = ActionType & " "
                        Next
                    End If
                    'strComments
                    If IsDBNull(dr.Item("strApplicationnumber")) Then
                        ActionComments = "N/A"
                    Else
                        ActionComments = Replace(dr.Item("strApplicationnumber"), "'", "")
                        ActionComments = "GA Application Number: " & ActionComments
                    End If

                    len = ActionComments.Length
                    'This was changed when AFS went to 5-digit AFS Number
                    'If len < 55 Then
                    '    For i = len To 54
                    '        ActionComments = ActionComments & " "
                    '    Next
                    'Else
                    '    ActionComments = Mid(ActionComments, 1, 54)
                    'End If

                    If len < 53 Then
                        For i = len To 52
                            ActionComments = ActionComments & " "
                        Next
                    Else
                        ActionComments = Mid(ActionComments, 1, 52)
                    End If

                    If IsDBNull(dr.Item("AchievedDate")) Then
                        DateAcheived = Format(Date.Today, "yyMMdd")
                    Else
                        DateAcheived = dr.Item("AchievedDate")
                    End If

                    DateExpire = CStr(CInt(Mid(DateAcheived, 1, 2)) + 5) & Mid(DateAcheived, 3)

                    If SubmitPermit = "V" Then
                        Select Case PermitState
                            Case "Issued"
                                PermitFile = "13                  A21" & PermitNumber & "V                                       N" & UpdateCode & vbCrLf & _
                                "13                  A22" & PermitNumber & DateAcheived & "        " & DateExpire & "                 " & UpdateCode & vbCrLf & _
                                AIRSNumber & "          A71" & PermitNumber & "                                         " & UpdateCode & vbCrLf & _
                                AIRSNumber & "          A81               " & PermitNumber & "IF001  13                 " & UpdateCode & vbCrLf & _
                                AIRSNumber & "          A82               " & PermitNumber & "IF0010120" & DateAcheived & "           " & UpdateCode & vbCrLf
                            Case "Admend"
                                PermitFile = "13                  A21" & PermitNumber & "V                                       N" & UpdateCode & vbCrLf & _
                                "13                  A22" & PermitNumber & DateAcheived & "        " & DateExpire & "                 " & UpdateCode & vbCrLf & _
                                "13                  A23" & PermitNumber & "AMENDMENT                                " & UpdateCode & vbCrLf & _
                                AIRSNumber & "          A71" & PermitNumber & "                                         " & UpdateCode & vbCrLf
                            Case "No Permit Req"
                                PermitFile = "13                  A21" & PermitNumber & "V                                       N" & UpdateCode & vbCrLf & _
                                "13                  A22" & PermitNumber & DateAcheived & "        " & DateExpire & "                 " & UpdateCode & vbCrLf & _
                                "13                  A23" & PermitNumber & "AMENDMENT                                " & UpdateCode & vbCrLf & _
                                AIRSNumber & "          A71" & PermitNumber & "                                         " & UpdateCode & vbCrLf
                            Case "Draft"
                                PermitFile = "13                  A21" & PermitNumber & "V                                       N" & UpdateCode & vbCrLf & _
                                "13                  A22" & PermitNumber & DateAcheived & "        " & DateExpire & "                 " & UpdateCode & vbCrLf & _
                                AIRSNumber & "          A71" & PermitNumber & "                                         " & UpdateCode & vbCrLf & _
                                AIRSNumber & "          A81               " & PermitNumber & "DP001  13                 " & UpdateCode & vbCrLf & _
                                AIRSNumber & "          A82               " & PermitNumber & "DP0010120" & DateAcheived & "           " & UpdateCode & vbCrLf
                        End Select
                    End If
                    BatchText = BatchText & _
                    AIRSNumber & "161" & ActionNumber & AIRProgramCodes & ActionType & DateAcheived & _
                    DateAcheived & ResultsCode & "0000000" & UserAFSCode & UpdateCode & vbCrLf
                    Comments = AIRSNumber & "171  " & ActionNumber & "001C" & ActionComments & "NN" & UpdateCode & vbCrLf
                    If Comments <> "" Then
                        BatchText = BatchText & Comments
                    End If
                End If
            End While
            dr.Close()

            SQL = "Update " & connNameSpace & ".AFSSSPPRecords set " & _
            "strUpDateStatus = 'N' " & _
            "where strUPDateStatus <> 'N' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & BatchText

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
         

    End Sub
    Sub FCEActions()
        Dim BatchText As String = ""
        Dim Comments As String
        Dim len As Integer
        Dim i As Integer

        Dim UpdateCode As String
        Dim AIRSNumber As String
        Dim AIRProgramCodes As String
        Dim UserAFSCode As String
        Dim ActionNumber As String
        Dim ResultsCode As String
        Dim ActionComments As String
        Dim DateAcheived As String
        Dim SiteStatus As String

        SQL = "Select strUpDateStatus, " & _
        "" & connNameSpace & ".AFSSSCPFCERecords.strFCENumber, strAFSActionNumber, " & _
        "" & connNameSpace & ".SSCPFCEMaster.strAIRSNumber, strAIRProgramCodes, " & _
        "to_char(datFCECompleted, 'YYMMDD') as AchievedDate, strFCEStatus, " & _
        "strFCEComments, strSiteInspection " & _
        "from " & connNameSpace & ".AFSSSCPFCERecords, " & connNameSpace & ".SSCPFCEMaster, " & _
        "" & connNameSpace & ".SSCPFCE, " & connNameSpace & ".APBHeaderData " & _
        "where strUpDateStatus <> 'N' " & _
        "and " & connNameSpace & ".AFSSSCPFCERecords.strFCENumber = " & connNameSpace & ".SSCPFCEMaster.strFCENumber " & _
        "and " & connNameSpace & ".SSCPFCE.strFCENumber = " & connNameSpace & ".AFSSSCPFCERecords.strFCENumber " & _
        "and " & connNameSpace & ".APBHeaderData.strAIRSNumber = " & connNameSpace & ".SSCPFCEMaster.strAIRSNumber " 

        Try
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                AIRSNumber = Mid(dr.Item("strAIRSNumber"), 3)
                If AIRSNumber.Length = 10 Then
                    If IsDBNull(dr.Item("strUpDatestatus")) Then
                        UpdateCode = "A"
                    Else
                        UpdateCode = dr.Item("strUpDateStatus")
                    End If
                    AIRProgramCodes = ""
                    If IsDBNull(dr.Item("strAIRProgramCodes")) Then
                        AIRProgramCodes = "0"
                    Else
                        If Mid(dr.Item("strAIRProgramCodes"), 1, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "0"
                        End If
                        'If Mid(dr.Item("strAIRProgramCodes"), 2, 1) = 1 Then
                        '    AIRProgramCodes = AIRProgramCodes & "1"
                        'End If
                        If Mid(dr.Item("strAIRProgramCodes"), 3, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "3"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 4, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "4"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 5, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "6"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 6, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "7"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 7, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "8"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 8, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "9"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 9, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "F"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 10, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "A"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 11, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "I"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 12, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "M"
                        End If
                        If Mid(dr.Item("strAIRProgramCodes"), 13, 1) = 1 Then
                            AIRProgramCodes = AIRProgramCodes & "V"
                        End If
                        If AIRProgramCodes = "" Then
                            AIRProgramCodes = "0"
                        End If
                    End If
                    If AIRProgramCodes.Length > 6 Then
                        AIRProgramCodes = Mid(AIRProgramCodes, 1, 6)
                    End If
                    len = AIRProgramCodes.Length
                    If len <> 6 Then
                        For i = len To 5
                            AIRProgramCodes = AIRProgramCodes & " "
                        Next
                    End If
                    If IsDBNull(dr.Item("strAFSActionNumber")) Then
                        ActionNumber = "00001"
                    Else
                        ActionNumber = dr.Item("strAFSActionNumber")
                        Select Case ActionNumber.Length
                            Case "0"
                                ActionNumber = "00001"
                            Case "1"
                                ActionNumber = "0000" & ActionNumber
                            Case "2"
                                ActionNumber = "000" & ActionNumber
                            Case "3"
                                ActionNumber = "00" & ActionNumber
                            Case "4"
                                ActionNumber = "0" & ActionNumber
                            Case "5"
                                ActionNumber = ActionNumber
                            Case Else
                                ActionNumber = "00001"
                        End Select
                    End If
                    UserAFSCode = "G62"
                    If IsDBNull(dr.Item("AchievedDate")) Then
                        DateAcheived = Format(Date.Today, "yyMMdd")
                    Else
                        DateAcheived = dr.Item("AchievedDate")
                    End If
                    If IsDBNull(dr.Item("strFCEStatus")) Then
                        ResultsCode = ""
                    Else
                        ResultsCode = "21"
                    End If
                    If IsDBNull(dr.Item("strSiteInspection")) Then
                        SiteStatus = "FF"
                    Else
                        If dr.Item("strSiteInspection") = "True" Then
                            SiteStatus = "FS"
                        Else
                            SiteStatus = "FF"
                        End If
                    End If
                    If ResultsCode.Length > 9 Then
                        ResultsCode = Mid(ResultsCode, 1, 9)
                    End If

                    len = ResultsCode.Length
                    If len <> 9 Then
                        For i = len To 8
                            ResultsCode = ResultsCode & " "
                        Next
                    End If
                    If IsDBNull(dr.Item("strFCENumber")) Then
                        ActionComments = ""
                    Else
                        ActionComments = dr.Item("strFCENumber")
                        ActionComments = "GA FCE Number: " & ActionComments
                    End If

                    len = ActionComments.Length
                    'This was changed when AFS went to 5-digit AFS Number
                    'If len < 55 Then
                    '    For i = len To 54
                    '        ActionComments = ActionComments & " "
                    '    Next
                    'Else
                    '    ActionComments = Mid(ActionComments, 1, 54)
                    'End If

                    If len < 53 Then
                        For i = len To 52
                            ActionComments = ActionComments & " "
                        Next
                    Else
                        ActionComments = Mid(ActionComments, 1, 52)
                    End If

                    Comments = AIRSNumber & "171  " & ActionNumber & "001C" & ActionComments & "NN" & UpdateCode & vbCrLf

                    BatchText = BatchText & _
                    AIRSNumber & "161" & ActionNumber & AIRProgramCodes & SiteStatus & "                      " & DateAcheived & ResultsCode & UserAFSCode & "             " & UpdateCode & vbCrLf

                    'This change was made when AFS went to 5-digit action numbers 
                    ' AIRSNumber & "161" & ActionNumber & AIRProgramCodes & SiteStatus & "                      " & DateAcheived & ResultsCode & UserAFSCode & "               " & UpdateCode & vbCrLf

                    If Comments <> "" Then
                        BatchText = BatchText & Comments
                    End If
                End If
            End While

            SQL = "Update " & connNameSpace & ".AFSSSCPFCERecords set " & _
            "strUpDateStatus = 'N' " & _
            "where strUPDateStatus <> 'N' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & BatchText

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
         

    End Sub
    Sub ComplianceActions()
        Dim BatchText As String = ""
        Dim len As Integer
        Dim i As Integer

        Dim UpdateCode As String
        Dim AIRSNumber As String
        Dim AIRProgramCodes As String
        Dim UserAFSCode As String
        Dim ActionNumber As String
        Dim ActionComments As String
        Dim DateAcheived As String
        Dim EventType As String
        Dim EnforcementNumber As String

        'changed acheived dates from completed to received or inspection dates based on conversation with LMusgrove, MFloyd 8/10/2010
        SQL = "Select " & _
        "strUpDateStatus, " & connNameSpace & ".AFSSSCPRecords.strTrackingNumber,  " & _
        "strEventType,  " & _
        "strResponsibleStaff,  " & _
        "case " & _
        "when strEventType = '01' then to_char(datReceivedDate, 'YYMMDD') " & _
        "when strEventType = '02' then to_char(datInspectionDateEnd, 'YYMMDD')   " & _
        "when strEventType = '03' then to_char(datReceivedDate, 'YYMMDD') " & _
        "when strEventType = '05' then to_char(datReceivedDate, 'YYMMDD') " & _
        "else to_char(datCompleteDate, 'YYMMDD') " & _
        "end AchievedDate,   " & _
        "" & connNameSpace & ".APBHeaderData.strAIRSNumber,  " & _
        "strAIRProgramCodes, strAFSActionNumber,  " & _
        "strEnforcementNumber  " & _
        "from " & connNameSpace & ".AFSSSCPRecords, " & connNameSpace & ".SSCPItemMaster,  " & _
        "" & connNameSpace & ".APBHeaderData,  " & _
        "" & connNameSpace & ".SSCP_AuditedEnforcement,  " & _
        "" & connNameSpace & ".SSCPInspections " & _
        "where strUpdateStatus <> 'N'  " & _
        "and " & connNameSpace & ".SSCPItemMaster.strTrackingNumber = " & connNameSpace & ".AFSSSCPRecords.strTrackingNumber  " & _
        "and " & connNameSpace & ".APBHeaderData.strAIRSNumber = " & connNameSpace & ".SSCPItemMaster.strAIRSNumber   " & _
        "and " & connNameSpace & ".SSCPItemMaster.strTrackingNumber = " & connNameSpace & ".SSCP_AuditedEnforcement.strTrackingNumber (+)  " & _
        " and " & connNameSpace & ".SSCPItemMaster.strTrackingNumber = " & connNameSpace & ".SSCPInspections.strTrackingNumber  (+) "

        Try
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                AIRSNumber = Mid(dr.Item("strAIRSNumber"), 3)
                If IsDBNull(dr.Item("strUpDatestatus")) Then
                    UpdateCode = "A"
                Else
                    UpdateCode = dr.Item("strUpDateStatus")
                End If
                AIRProgramCodes = ""
                If IsDBNull(dr.Item("strAIRProgramCodes")) Then
                    AIRProgramCodes = "0"
                Else
                    If Mid(dr.Item("strAIRProgramCodes"), 1, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "0"
                    End If
                    'If Mid(dr.Item("strAIRProgramCodes"), 2, 1) = 1 Then
                    '    AIRProgramCodes = AIRProgramCodes & "1"
                    'End If
                    If Mid(dr.Item("strAIRProgramCodes"), 3, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "3"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 4, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "4"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 5, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "6"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 6, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "7"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 7, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "8"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 8, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "9"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 9, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "F"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 10, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "A"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 11, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "I"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 12, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "M"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 13, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "V"
                    End If
                    If AIRProgramCodes = "" Then
                        AIRProgramCodes = "0"
                    End If
                End If
                If AIRProgramCodes.Length > 6 Then
                    AIRProgramCodes = Mid(AIRProgramCodes, 1, 6)
                End If
                len = AIRProgramCodes.Length
                If len <> 6 Then
                    For i = len To 5
                        AIRProgramCodes = AIRProgramCodes & " "
                    Next
                End If
                UserAFSCode = "G36"

                len = UserAFSCode.Length
                'This was changed when AFS went to 5-digit action Numbers
                'If len <> 18 Then
                '    For i = len To 17
                '        UserAFSCode = UserAFSCode & " "
                '    Next
                'End If
                If len <> 16 Then
                    For i = len To 15
                        UserAFSCode = UserAFSCode & " "
                    Next
                End If

                If IsDBNull(dr.Item("strAFSActionNumber")) Then
                    ActionNumber = "00001"
                Else
                    ActionNumber = dr.Item("strAFSActionNumber")
                End If
                Select Case ActionNumber.Length
                    Case "0"
                        ActionNumber = "00001"
                    Case "1"
                        ActionNumber = "0000" & ActionNumber
                    Case "2"
                        ActionNumber = "000" & ActionNumber
                    Case "3"
                        ActionNumber = "00" & ActionNumber
                    Case "4"
                        ActionNumber = "0" & ActionNumber
                    Case "5"
                        ActionNumber = ActionNumber
                    Case Else
                        ActionNumber = "00001"
                End Select
                If IsDBNull(dr.Item("AchievedDate")) Then
                    DateAcheived = Format(Date.Today, "yyMMdd")
                Else
                    DateAcheived = dr.Item("AchievedDate")
                End If
                If IsDBNull(dr.Item("strEventType")) Then
                    EventType = ""
                Else
                    EventType = dr.Item("strEventType")
                End If
                If IsDBNull(dr.Item("strTrackingNumber")) Then
                    ActionComments = "GA EPD# "
                Else
                    ActionComments = "GA EPD Compliance# " & dr.Item("strTrackingNumber")
                End If
                len = ActionComments.Length
                'This was changed when AFS went to 5-digit Action Numbers
                'If len <> 55 Then
                '    For i = len To 54
                '        ActionComments = ActionComments & " "
                '    Next
                'End If

                If len <> 53 Then
                    For i = len To 52
                        ActionComments = ActionComments & " "
                    Next
                End If

                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If

                Select Case EventType
                    Case "01" 'Reports 
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "37                      " & DateAcheived & "010000000" & UserAFSCode & UpdateCode & vbCrLf & _
                        AIRSNumber & "171  " & ActionNumber & "001C" & ActionComments & "NN" & UpdateCode & vbCrLf
                    Case "02" 'Inspections
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "27                      " & DateAcheived & "010000000" & UserAFSCode & UpdateCode & vbCrLf & _
                        AIRSNumber & "171  " & ActionNumber & "001C" & ActionComments & "NN" & UpdateCode & vbCrLf
                    Case "03" 'Stack Test
                        'BatchText = BatchText & _
                        'AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "SR                      " & DateAcheived & "010000000" & UserAFSCode & UpdateCode & vbCrLf & _
                        'AIRSNumber & "171  " & ActionNumber & "001C" & ActionComments & "NN" & UpdateCode & vbCrLf
                    Case "04" 'Title V ACC Reviews
                        If EnforcementNumber <> "" Then
                            BatchText = BatchText & _
                            AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "SR                      " & DateAcheived & "MV0000000" & UserAFSCode & UpdateCode & vbCrLf & _
                            AIRSNumber & "171  " & ActionNumber & "001C" & ActionComments & "NN" & UpdateCode & vbCrLf
                        Else
                            BatchText = BatchText & _
                            AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "SR                      " & DateAcheived & "MC0000000" & UserAFSCode & UpdateCode & vbCrLf & _
                            AIRSNumber & "171  " & ActionNumber & "001C" & ActionComments & "NN" & UpdateCode & vbCrLf
                        End If
                    Case "05" 'Notifications
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "PX                      " & DateAcheived & "010000000" & UserAFSCode & UpdateCode & vbCrLf & _
                        AIRSNumber & "171  " & ActionNumber & "001C" & ActionComments & "NN" & UpdateCode & vbCrLf
                    Case "06" 'Title V Compliance Cert Due
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "CS                      " & DateAcheived & "         " & UserAFSCode & UpdateCode & vbCrLf & _
                        AIRSNumber & "171  " & ActionNumber & "001C" & ActionComments & "NN" & UpdateCode & vbCrLf
                End Select
            End While
            dr.Close()

            SQL = "Update " & connNameSpace & ".AFSSSCPRecords set " & _
            "strUpDateStatus = 'N' " & _
            "where strUPDateStatus <> 'N' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & BatchText

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub
    Sub EnforcementActions()
        Dim BatchText As String = ""
        Dim len As Integer
        Dim i As Integer
        Dim UpdateCode As String
        Dim AIRSNumber As String
        Dim AIRProgramCodes As String
        Dim UserAFSCode As String
        Dim ActionType As String
        Dim KeyActionNumber As String
        Dim ActionNumber As String
        Dim DiscoveryDate As String
        Dim DayZero As String
        Dim NOVSent As String
        Dim NFALetter As String
        Dim COProposed As String
        Dim COExecuted As String
        Dim COResolved As String
        Dim AOExecuted As String
        Dim CivilCourt As String
        Dim AOResolved As String
        Dim StipulatedDate As String
        Dim PenaltyAmount As String
        Dim StipulatedPenalty As String
        Dim HPV As String
        Dim TrackingNumber As String
        Dim Pollutants As String
        Dim Pollutant1 As String
        Dim Pollutant2 As String
        Dim Pollutant3 As String
        Dim LinkedEvent As String

        Dim EnforcementNumber As String = ""
        'Dim AIRSNumber As String = ""
        Dim AFSNumber As String = ""
        Dim ModifingPerson As String = ""
        Dim ModifingDate As String = ""

        Try
            SQL = "select " & _
            "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
            "strafskeyactionnumber, strModifingPerson, datModifingDate " & _
            "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
            "where strafskeyactionnumber is not null  " & _
            "and not exists (select * " & _
            "from " & connNameSpace & ".afssscpenforcementrecords " & _
            "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber )  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strafskeyactionnumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strafskeyactionnumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If

                SQL = "Insert into " & connNameSpace & ".AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            SQL = "select " & _
            "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strafskeyactionnumber, strModifingPerson, datModifingDate " & _
            "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
            "where strafskeyactionnumber is not null " & _
            "and exists (select * from " & connNameSpace & ".afssscpenforcementrecords " & _
            "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.datModifingDate <> " & _
            "" & connNameSpace & ".afssscpenforcementrecords.datModifingDate " & _
            "and strUpdateStatus = 'N' ) "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strafskeyactionnumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strafskeyactionnumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If
                SQL = "Update " & connNameSpace & ".AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            SQL = "select " & _
            "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
            "strAFSNOVSentNumber, strModifingPerson, datModifingDate " & _
            "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
            "where strAFSNOVSentNumber is not null  " & _
            "and not exists (select * " & _
            "from " & connNameSpace & ".afssscpenforcementrecords " & _
            "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSNOVSentNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSNOVSentNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSNOVSentNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If

                SQL = "Insert into " & connNameSpace & ".AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            SQL = "select " & _
            "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSNOVSentNumber, strModifingPerson, datModifingDate " & _
            "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
            "where strAFSNOVSentNumber is not null " & _
            "and exists (select * from " & connNameSpace & ".afssscpenforcementrecords " & _
            "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.datModifingDate <> " & _
            "" & connNameSpace & ".afssscpenforcementrecords.datModifingDate " & _
            "and strUpdateStatus = 'N' " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSNOVSentNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSNOVSentNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSNOVSentNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If
                SQL = "Update " & connNameSpace & ".AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            SQL = "select " & _
          "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
          "strAFSNOVResolvedNumber, strModifingPerson, datModifingDate " & _
          "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
          "where strAFSNOVResolvedNumber is not null  " & _
          "and not exists (select * " & _
          "from " & connNameSpace & ".afssscpenforcementrecords " & _
          "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
          "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSNOVResolvedNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSNOVResolvedNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSNOVResolvedNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If

                SQL = "Insert into " & connNameSpace & ".AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            SQL = "select " & _
            "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSNOVResolvedNumber, strModifingPerson, datModifingDate " & _
            "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
            "where strAFSNOVResolvedNumber is not null " & _
            "and exists (select * from " & connNameSpace & ".afssscpenforcementrecords " & _
            "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.datModifingDate <> " & _
            "" & connNameSpace & ".afssscpenforcementrecords.datModifingDate " & _
            "and strUpdateStatus = 'N' " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSNOVResolvedNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSNOVResolvedNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSNOVResolvedNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If
                SQL = "Update " & connNameSpace & ".AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            SQL = "select " & _
          "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
          "strAFSCOProposedNumber, strModifingPerson, datModifingDate " & _
          "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
          "where strAFSCOProposedNumber is not null  " & _
          "and not exists (select * " & _
          "from " & connNameSpace & ".afssscpenforcementrecords " & _
          "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
          "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSCOProposedNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSCOProposedNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSCOProposedNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If

                SQL = "Insert into " & connNameSpace & ".AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            SQL = "select " & _
            "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSCOProposedNumber, strModifingPerson, datModifingDate " & _
            "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
            "where strAFSCOProposedNumber is not null " & _
            "and exists (select * from " & connNameSpace & ".afssscpenforcementrecords " & _
            "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.datModifingDate <> " & _
            "" & connNameSpace & ".afssscpenforcementrecords.datModifingDate " & _
            "and strUpdateStatus = 'N' " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSCOProposedNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSCOProposedNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSCOProposedNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If
                SQL = "Update " & connNameSpace & ".AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            SQL = "select " & _
          "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
          "strAFSCOExecutedNumber, strModifingPerson, datModifingDate " & _
          "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
          "where strAFSCOExecutedNumber is not null  " & _
          "and not exists (select * " & _
          "from " & connNameSpace & ".afssscpenforcementrecords " & _
          "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
          "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSCOExecutedNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSCOExecutedNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSCOExecutedNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If

                SQL = "Insert into " & connNameSpace & ".AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            SQL = "select " & _
            "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSCOExecutedNumber, strModifingPerson, datModifingDate " & _
            "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
            "where strAFSCOExecutedNumber is not null " & _
            "and exists (select * from " & connNameSpace & ".afssscpenforcementrecords " & _
            "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.datModifingDate <> " & _
            "" & connNameSpace & ".afssscpenforcementrecords.datModifingDate " & _
            "and strUpdateStatus = 'N' " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSCOExecutedNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSCOExecutedNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSCOExecutedNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If
                SQL = "Update " & connNameSpace & ".AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            SQL = "select " & _
          "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
          "strAFSCOResolvedNumber, strModifingPerson, datModifingDate " & _
          "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
          "where strAFSCOResolvedNumber is not null  " & _
          "and not exists (select * " & _
          "from " & connNameSpace & ".afssscpenforcementrecords " & _
          "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
          "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSCOResolvedNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSCOResolvedNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSCOResolvedNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If

                SQL = "Insert into " & connNameSpace & ".AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            SQL = "select " & _
            "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSCOResolvedNumber, strModifingPerson, datModifingDate " & _
            "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
            "where strAFSCOResolvedNumber is not null " & _
            "and exists (select * from " & connNameSpace & ".afssscpenforcementrecords " & _
            "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.datModifingDate <> " & _
            "" & connNameSpace & ".afssscpenforcementrecords.datModifingDate " & _
            "and strUpdateStatus = 'N' " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSCOResolvedNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSCOResolvedNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSCOResolvedNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If
                SQL = "Update " & connNameSpace & ".AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            SQL = "select " & _
          "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
          "strAFSAOtoAGNumber, strModifingPerson, datModifingDate " & _
          "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
          "where strAFSAOtoAGNumber is not null  " & _
          "and not exists (select * " & _
          "from " & connNameSpace & ".afssscpenforcementrecords " & _
          "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
          "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSAOtoAGNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSAOtoAGNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSAOtoAGNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If

                SQL = "Insert into " & connNameSpace & ".AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            SQL = "select " & _
            "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSAOtoAGNumber, strModifingPerson, datModifingDate " & _
            "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
            "where strAFSAOtoAGNumber is not null " & _
            "and exists (select * from " & connNameSpace & ".afssscpenforcementrecords " & _
            "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.datModifingDate <> " & _
            "" & connNameSpace & ".afssscpenforcementrecords.datModifingDate " & _
            "and strUpdateStatus = 'N' " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSAOtoAGNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSAOtoAGNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSAOtoAGNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If
                SQL = "Update " & connNameSpace & ".AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            SQL = "select " & _
          "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
          "strAFSCivilCOurtNumber, strModifingPerson, datModifingDate " & _
          "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
          "where strAFSCivilCOurtNumber is not null  " & _
          "and not exists (select * " & _
          "from " & connNameSpace & ".afssscpenforcementrecords " & _
          "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
          "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSCivilCOurtNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSCivilCOurtNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSCivilCOurtNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If

                SQL = "Insert into " & connNameSpace & ".AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            SQL = "select " & _
            "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSCivilCOurtNumber, strModifingPerson, datModifingDate " & _
            "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
            "where strAFSCivilCOurtNumber is not null " & _
            "and exists (select * from " & connNameSpace & ".afssscpenforcementrecords " & _
            "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.datModifingDate <> " & _
            "" & connNameSpace & ".afssscpenforcementrecords.datModifingDate " & _
            "and strUpdateStatus = 'N' " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSCivilCOurtNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSCivilCOurtNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSCivilCOurtNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If
                SQL = "Update " & connNameSpace & ".AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            SQL = "select " & _
            "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
            "strAFSAOResolvedNumber, strModifingPerson, datModifingDate " & _
            "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
            "where strAFSAOResolvedNumber is not null  " & _
            "and not exists (select * " & _
            "from " & connNameSpace & ".afssscpenforcementrecords " & _
            "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSAOResolvedNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSAOResolvedNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSAOResolvedNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If

                SQL = "Insert into " & connNameSpace & ".AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            SQL = "select " & _
            "" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSAOResolvedNumber, strModifingPerson, datModifingDate " & _
            "from " & connNameSpace & ".SSCP_AuditedEnforcement " & _
            "where strAFSAOResolvedNumber is not null " & _
            "and exists (select * from " & connNameSpace & ".afssscpenforcementrecords " & _
            "where " & connNameSpace & ".sscp_auditedEnforcement.strenforcementnumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strenforcementnumber " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.datModifingDate <> " & _
            "" & connNameSpace & ".afssscpenforcementrecords.datModifingDate " & _
            "and strUpdateStatus = 'N' " & _
            "and " & connNameSpace & ".sscp_auditedEnforcement.strAFSAOResolvedNumber = " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If
                If IsDBNull(dr.Item("strAFSAOResolvedNumber")) Then
                    AFSNumber = ""
                Else
                    AFSNumber = dr.Item("strAFSAOResolvedNumber")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModifingPerson = UserGCode
                Else
                    ModifingPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    ModifingDate = OracleDate
                Else
                    ModifingDate = Format(dr.Item("datModifingDate"), "dd-MMM-yyyy")
                End If
                SQL = "Update " & connNameSpace & ".AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            'SQL = "Select   " & _
            '"" & connNameSpace & ".AFSSSCPEnforcementRecords.strUpDateStatus,   " & _
            '"" & connNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber,    " & _
            '"strStaffResponsible,    " & _
            '"to_Char(datDiscoveryDate, 'YYMMDD') as DiscoveryDate,    " & _
            '"to_char(datDayZero, 'YYMMDD') as DayZero,    " & _
            '"to_char(datEnforcementFinalized, 'YYMMDD') as AchievedDate,    " & _
            '"to_char(datNOVSent, 'YYMMDD') as NOVSent,    " & _
            '"to_char(datNFALetterSent, 'YYMMDD') as NFALetterSent,    " & _
            '"to_char(datCOProposed, 'YYMMDD') as COProposed,    " & _
            '"to_char(datCOExecuted, 'YYMMDD') as COExecuted,    " & _
            '"to_Char(datCOResolved, 'YYMMDD') as COResolved,    " & _
            '"to_char(datAOExecuted, 'YYMMDD') as AOExecuted,    " & _
            '"to_char(datAOAppealed, 'YYMMDD') as AOAppealed,    " & _
            '"to_char(datAOResolved, 'YYMMDD') as AOResolved,    " & _
            '"to_char(SSCPEnforcement.datModifingDate, 'YYMMDD') as StipulatedDate,    " & _
            '"strCOPenaltyAmount,    " & _
            '"" & connNameSpace & ".SSCPENforcementStipulated.strStipulatedPenalty,    " & _
            '"" & connNameSpace & ".APBHeaderData.strAIRSNumber, strAIRProgramCodes,    " & _
            '"" & connNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber,    " & _
            '"strAFSKeyActionNumber,  " & connNameSpace & ".sscpEnforcementItems.strTrackingNumber,    " & _
            '"case    " & _
            '"when " & connNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSKeyActionNumber then '04'    " & _
            ' "when " & connNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSNOVSentNumber then '56'    " & _
            '"when " & connNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSNOVResolvedNumber then 'AW'    " & _
            '"when " & connNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSCOProposedNumber then '57'    " & _
            '"when " & connNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSCOExecutedNumber then 'X1'    " & _
            '"when " & connNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSCOResolvedNumber then 'AS'    " & _
            '"when " & connNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSStipulatedPenaltyNumber then 'Z4'   " & _
            '"when " & connNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSAOtoAGNumber then '60'    " & _
            '"when " & connNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSCivilCourtNumber then '64'    " & _
            '"when " & connNameSpace & ".AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSAOResolvedNumber then 'AS'    " & _
            '"Else 'ERROR'     " & _
            '"End as ActionType,    " & _
            '"strPollutants, strHPV,  " & _
            '"" & connNameSpace & ".AFSSSCPRecords.strAFSActionNumber as LinkingEvent  " & _
            '"from  " & connNameSpace & ".AFSSSCPEnforcementRecords,    " & _
            '"" & connNameSpace & ".SSCPEnforcementItems,  " & connNameSpace & ".SSCPEnforcement,    " & _
            '"" & connNameSpace & ".APBHeaderData,  " & connNameSpace & ".SSCPENforcementStipulated,    " & _
            '"" & connNameSpace & ".SSCPItemMaster,  " & _
            '"" & connNameSpace & ".AFSSSCPRecords    " & _
            '"where " & connNameSpace & ".AFSSSCPEnforcementRecords.strUpdateStatus <> 'N'    " & _
            '"and  " & connNameSpace & ".SSCPEnforcement.strEnforcementNumber =  " & connNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber    " & _
            '"and  " & connNameSpace & ".APBHeaderData.strAIRSNumber =  " & connNameSpace & ".SSCPEnforcementItems.strAIRSNumber    " & _
            '"and  " & connNameSpace & ".SSCPEnforcementItems.strEnforcementNumber =  " & connNameSpace & ".SSCPEnforcement.strEnforcementNumber    " & _
            '"and  " & connNameSpace & ".SSCPEnforcementItems.strEnforcementNumber =  " & connNameSpace & ".SSCPENforcementStipulated.strEnforcementNumber (+)    " & _
            '"and  " & connNameSpace & ".sscpEnforcementItems.strTrackingNumber =  " & connNameSpace & ".SSCPItemMaster.strTrackingNumber (+)   " & _
            '"and strEventType <> '03' " & _
            '"and " & connNameSpace & ".sscpEnforcementItems.strTrackingNumber = " & connNameSpace & ".AFSSSCPRecords.strTrackingNumber (+) " & _
            '"Order by strAFSActionNumber ASC "

            SQL = "Select distinct " & _
            "AIRBranch.AFSSSCPEnforcementRecords.strUpDateStatus,    " & _
            "AIRBranch.AFSSSCPEnforcementRecords.strEnforcementNumber,    " & _
            "numStaffResponsible,     " & _
            "to_Char(datDiscoveryDate, 'YYMMDD') as DiscoveryDate,     " & _
            "to_char(datDayZero, 'YYMMDD') as DayZero,     " & _
            "to_char(datEnforcementFinalized, 'YYMMDD') as AchievedDate,     " & _
            "to_char(datNOVSent, 'YYMMDD') as NOVSent,     " & _
            "to_char(datNFALetterSent, 'YYMMDD') as NFALetterSent,     " & _
            "to_char(datCOProposed, 'YYMMDD') as COProposed,     " & _
            "to_char(datCOExecuted, 'YYMMDD') as COExecuted,     " & _
            "to_Char(datCOResolved, 'YYMMDD') as COResolved,     " & _
            "to_char(datAOExecuted, 'YYMMDD') as AOExecuted,     " & _
            "to_char(datAOAppealed, 'YYMMDD') as AOAppealed,     " & _
            "to_char(datAOResolved, 'YYMMDD') as AOResolved,     " & _
            "to_char(SSCP_AuditedEnforcement.datModifingDate, 'YYMMDD') as StipulatedDate,     " & _
            "strCOPenaltyAmount,     " & _
            "AIRBranch.SSCPENforcementStipulated.strStipulatedPenalty,     " & _
            "AIRBranch.APBHeaderData.strAIRSNumber, strAIRProgramCodes,     " & _
            "AIRBranch.AFSSSCPEnforcementRecords.strAFSActionNumber,     " & _
            "strAFSKeyActionNumber,  AIRBranch.SSCP_AuditedEnforcement.strTrackingNumber,     " & _
            "case     " & _
            "when AIRBranch.AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSKeyActionNumber then '04'     " & _
            "when AIRBranch.AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSNOVSentNumber then '56'     " & _
            "when AIRBranch.AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSNOVResolvedNumber then 'AW'     " & _
            "when AIRBranch.AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSCOProposedNumber then '57'     " & _
            "when AIRBranch.AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSCOExecutedNumber then 'X1'     " & _
            "when AIRBranch.AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSCOResolvedNumber then 'AS'     " & _
            "when AIRBranch.AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSStipulatedPenaltyNumber then 'Z4'   " & _
            "when AIRBranch.AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSAOtoAGNumber then '60'     " & _
            "when AIRBranch.AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSCivilCourtNumber then '64'     " & _
            "when AIRBranch.AFSSSCPEnforcementRecords.strAFSActionNumber = strAFSAOResolvedNumber then 'AS'     " & _
            "Else 'ERROR'      " & _
            "End as ActionType,     " & _
            "strPollutants, strHPV,   " & _
            "AIRBranch.AFSSSCPRecords.strAFSActionNumber as LinkingEvent   " & _
            "from    AIRBranch.AFSSSCPEnforcementRecords,     " & _
            "AIRBranch.SSCP_AuditedEnforcement,   " & _
            "AIRBranch.APBHeaderData,  AIRBranch.SSCPENforcementStipulated,     " & _
            "AIRBranch.SSCPItemMaster,   " & _
            "AIRBranch.AFSSSCPRecords     " & _
            "where AIRBranch.AFSSSCPEnforcementRecords.strUpdateStatus <> 'N'     " & _
            "and  AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber =  " & _
            "AIRBranch.AFSSSCPEnforcementRecords.strEnforcementNumber     " & _
            "and  AIRBranch.APBHeaderData.strAIRSNumber =  AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber         " & _
            "and  AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber =  " & _
            "AIRBranch.SSCPENforcementStipulated.strEnforcementNumber (+)     " & _
            "and  AIRBranch.SSCP_AuditedEnforcement.strTrackingNumber =  AIRBranch.SSCPItemMaster.strTrackingNumber (+)    " & _
            "and strEventType <> '03'  " & _
            "and AIRBranch.SSCP_AuditedEnforcement.strTrackingNumber = AIRBranch.AFSSSCPRecords.strTrackingNumber (+)  " & _
            "Order by strAIRSNumber, strAFSActionNumber ASC  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                AIRSNumber = Mid(dr.Item("strAIRSNumber"), 3)
                If IsDBNull(dr.Item("strUpDatestatus")) Then
                    UpdateCode = "A"
                Else
                    UpdateCode = dr.Item("strUpDateStatus")
                End If
                AIRProgramCodes = ""
                If IsDBNull(dr.Item("strAIRProgramCodes")) Then
                    AIRProgramCodes = "0"
                Else
                    If Mid(dr.Item("strAIRProgramCodes"), 1, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "0"
                    End If
                    'If Mid(dr.Item("strAIRProgramCodes"), 2, 1) = 1 Then
                    '    AIRProgramCodes = AIRProgramCodes & "1"
                    'End If
                    If Mid(dr.Item("strAIRProgramCodes"), 3, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "3"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 4, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "4"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 5, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "6"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 6, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "7"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 7, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "8"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 8, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "9"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 9, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "F"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 10, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "A"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 11, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "I"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 12, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "M"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 13, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "V"
                    End If
                    If AIRProgramCodes = "" Then
                        AIRProgramCodes = "0"
                    End If
                End If
                If AIRProgramCodes.Length > 6 Then
                    AIRProgramCodes = Mid(AIRProgramCodes, 1, 6)
                End If
                len = AIRProgramCodes.Length
                If len <> 6 Then
                    For i = len To 5
                        AIRProgramCodes = AIRProgramCodes & " "
                    Next
                End If
                UserAFSCode = "G36"
                If IsDBNull(dr.Item("ActionType")) Then
                    ActionType = "XX"
                Else
                    ActionType = dr.Item("ActionType")
                End If
                If IsDBNull(dr.Item("strHPV")) Then
                    HPV = "NOV"
                Else
                    HPV = dr.Item("strHPV")
                End If
                If ActionType = "04" Then
                    If HPV = "NOV" Then
                        ActionType = "VZ"
                    Else
                        ActionType = ActionType
                    End If
                End If
                If IsDBNull(dr.Item("DiscoveryDate")) Then
                    DiscoveryDate = Format(Date.Today, "yyMMdd")
                Else
                    DiscoveryDate = dr.Item("DiscoveryDate")
                End If
                If IsDBNull(dr.Item("DayZero")) Then
                    DayZero = Format(Date.Today, "yyMMdd")
                Else
                    DayZero = dr.Item("DayZero")
                End If
                If IsDBNull(dr.Item("NOVSent")) Then
                    NOVSent = Format(Date.Today, "yyMMdd")
                Else
                    NOVSent = dr.Item("NOVSent")
                End If
                If IsDBNull(dr.Item("NFALetterSent")) Then
                    NFALetter = Format(Date.Today, "yyMMdd")
                Else
                    NFALetter = dr.Item("NFALetterSent")
                End If
                If IsDBNull(dr.Item("COProposed")) Then
                    COProposed = Format(Date.Today, "yyMMdd")
                Else
                    COProposed = dr.Item("COProposed")
                End If
                If IsDBNull(dr.Item("COExecuted")) Then
                    COExecuted = Format(Date.Today, "yyMMdd")
                Else
                    COExecuted = dr.Item("COExecuted")
                End If
                If IsDBNull(dr.Item("COResolved")) Then
                    COResolved = Format(Date.Today, "yyMMdd")
                Else
                    COResolved = dr.Item("COResolved")
                End If
                If IsDBNull(dr.Item("AOExecuted")) Then
                    AOExecuted = Format(Date.Today, "yyMMdd")
                Else
                    AOExecuted = dr.Item("AOExecuted")
                End If
                If IsDBNull(dr.Item("AOAppealed")) Then
                    CivilCourt = Format(Date.Today, "yyMMdd")
                Else
                    CivilCourt = dr.Item("AOAppealed")
                End If
                If IsDBNull(dr.Item("AOResolved")) Then
                    AOResolved = Format(Date.Today, "yyMMdd")
                Else
                    AOResolved = dr.Item("AOResolved")
                End If
                If IsDBNull(dr.Item("StipulatedDate")) Then
                    StipulatedDate = Format(Date.Today, "yyMMdd")
                Else
                    StipulatedDate = dr.Item("StipulatedDate")
                End If
                If IsDBNull(dr.Item("strCOPenaltyAmount")) Then
                    PenaltyAmount = "0"
                Else
                    PenaltyAmount = Replace(dr.Item("strCOPenaltyAmount"), "$", "")
                    PenaltyAmount = Replace(PenaltyAmount, ",", "")
                     If PenaltyAmount.Contains(".") Then
                        PenaltyAmount = Mid(PenaltyAmount, 1, (PenaltyAmount.IndexOf(".")))
                    End If
                End If

                If PenaltyAmount.Length > 7 Then
                    PenaltyAmount = CSng(PenaltyAmount)
                End If
                len = PenaltyAmount.Length
                If PenaltyAmount.Length <> 7 Then
                    For i = len To 6
                        PenaltyAmount = "0" & PenaltyAmount
                    Next
                End If
                If IsDBNull(dr.Item("strStipulatedPenalty")) Then
                    StipulatedPenalty = "0"
                Else
                    StipulatedPenalty = Replace(dr.Item("strStipulatedPenalty"), "$", "")
                End If
                If StipulatedPenalty.Contains(".") Then
                    StipulatedPenalty = Mid(StipulatedPenalty, 1, (StipulatedPenalty.IndexOf(".")))
                End If

                If StipulatedPenalty.Length > 7 Then
                    StipulatedPenalty = CSng(StipulatedPenalty)
                End If
                len = StipulatedPenalty.Length
                If StipulatedPenalty.Length <> 7 Then
                    For i = len To 6
                        StipulatedPenalty = "0" & StipulatedPenalty
                    Next
                End If
                If IsDBNull(dr.Item("strTrackingNumber")) Then
                    TrackingNumber = ""
                Else
                    TrackingNumber = dr.Item("strTrackingNumber")
                End If
                If IsDBNull(dr.Item("strAFSActionNumber")) Then
                    ActionNumber = ""
                Else
                    ActionNumber = dr.Item("strAFSActionNumber")
                    Select Case ActionNumber.Length
                        Case "0"
                            ActionNumber = "00001"
                        Case "1"
                            ActionNumber = "0000" & ActionNumber
                        Case "2"
                            ActionNumber = "000" & ActionNumber
                        Case "3"
                            ActionNumber = "00" & ActionNumber
                        Case "4"
                            ActionNumber = "0" & ActionNumber
                        Case "5"
                            ActionNumber = ActionNumber
                        Case Else
                            ActionNumber = "00001"
                    End Select
                End If
                If IsDBNull(dr.Item("strAFSKeyActionNumber")) Then
                    KeyActionNumber = ""
                Else
                    KeyActionNumber = dr.Item("strAFSKeyActionNumber")
                    Select Case KeyActionNumber.Length
                        Case "0"
                            KeyActionNumber = "00001"
                        Case "1"
                            KeyActionNumber = "0000" & KeyActionNumber
                        Case "2"
                            KeyActionNumber = "000" & KeyActionNumber
                        Case "3"
                            KeyActionNumber = "00" & KeyActionNumber
                        Case "4"
                            KeyActionNumber = "0" & KeyActionNumber
                        Case "5"
                            KeyActionNumber = KeyActionNumber
                        Case Else
                            KeyActionNumber = "00001"
                    End Select
                End If
                If IsDBNull(dr.Item("strPollutants")) Then
                    Pollutants = "0OT,"
                Else
                    Pollutants = dr.Item("strPollutants")
                End If

                Pollutant1 = Mid(Pollutants, 2, (InStr(Pollutants, ",", CompareMethod.Text) - 2))
                Pollutants = Mid(Pollutants, (InStr(Pollutants, ",", CompareMethod.Text) + 1))
                If Pollutants <> "" Then
                    Pollutant2 = Mid(Pollutants, 2, (InStr(Pollutants, ",", CompareMethod.Text) - 2))
                    Pollutants = Mid(Pollutants, (InStr(Pollutants, ",", CompareMethod.Text) + 1))
                    If Pollutants <> "" Then
                        Pollutant3 = Mid(Pollutants, 2, (InStr(Pollutants, ",", CompareMethod.Text) - 2))
                        Pollutants = Mid(Pollutants, (InStr(Pollutants, ",", CompareMethod.Text) + 1))
                    Else
                        Pollutant3 = ""
                    End If
                Else
                    Pollutant2 = ""
                    Pollutant3 = ""
                End If
                If Pollutant2 = Pollutant1 Then
                    Pollutant2 = ""
                End If
                If Pollutant3 = Pollutant1 Then
                    Pollutant3 = ""
                End If

                If Pollutant1.Length > 5 Then
                    Pollutant1 = Mid(Pollutant1, 1, 5)
                End If
                len = Pollutant1.Length
                If len <> 5 Then
                    For i = len To 4
                        Pollutant1 = Pollutant1 & " "
                    Next
                End If
                If Pollutant2.Length > 5 Then
                    Pollutant2 = Mid(Pollutant2, 1, 5)
                End If
                len = Pollutant2.Length
                If len <> 5 Then
                    For i = len To 4
                        Pollutant2 = Pollutant2 & " "
                    Next
                End If
                If Pollutant3.Length > 5 Then
                    Pollutant1 = Mid(Pollutant3, 1, 5)
                End If
                len = Pollutant3.Length
                If len <> 5 Then
                    For i = len To 4
                        Pollutant3 = Pollutant3 & " "
                    Next
                End If
                If IsDBNull(dr.Item("LinkingEvent")) Then
                    LinkedEvent = ""
                Else
                    LinkedEvent = dr.Item("LinkingEvent")
                End If
                Select Case LinkedEvent.Length
                    Case 0
                        LinkedEvent = "     "
                    Case 1
                        LinkedEvent = "0000" & LinkedEvent
                    Case 2
                        LinkedEvent = "000" & LinkedEvent
                    Case 3
                        LinkedEvent = "00" & LinkedEvent
                    Case 4
                        LinkedEvent = "0" & LinkedEvent
                    Case 5
                        LinkedEvent = LinkedEvent
                    Case Else
                        LinkedEvent = "     "
                End Select
                If ActionNumber <> "" Then
                    Select Case ActionType
                        Case "04"   'Day Zero 
                            BatchText = BatchText & _
                            AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "04                      " & DayZero & "010000000" & UserAFSCode & "  " & Pollutant1 & "     N" & UpdateCode & _
                               vbCrLf & _
                            AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                   " & Pollutant1 & Pollutant2 & Pollutant3 & "                      " & UpdateCode & _
                               vbCrLf & _
                            AIRSNumber & "164" & ActionNumber & KeyActionNumber & "                    " & HPV & "                                 " & UpdateCode & vbCrLf
                            If LinkedEvent <> "" And LinkedEvent <> "     " Then
                                BatchText = BatchText & _
                                AIRSNumber & "163" & LinkedEvent & KeyActionNumber & "                                                        " & UpdateCode & vbCrLf
                            End If
                        Case "VZ" 'Non HPV
                            BatchText = BatchText & _
                         AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "VZ                      " & DayZero & "010000000" & UserAFSCode & "            N" & UpdateCode & _
                             vbCrLf & _
                         AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                   " & Pollutant1 & Pollutant2 & Pollutant3 & "                      " & UpdateCode & _
                             vbCrLf
                            If LinkedEvent <> "" And LinkedEvent <> "     " Then
                                BatchText = BatchText & _
                                AIRSNumber & "163" & LinkedEvent & KeyActionNumber & "                                                        " & UpdateCode & vbCrLf
                            End If
                        Case "56"   'NOV Sent 
                            BatchText = BatchText & _
                            AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "56                      " & NOVSent & "  0000000" & UserAFSCode & "            N" & UpdateCode & vbCrLf & _
                            AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & vbCrLf
                        Case "AW"   'NFA Letter Sent
                            BatchText = BatchText & _
                            AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "AW                      " & NFALetter & "  0000000" & UserAFSCode & "            N" & UpdateCode & vbCrLf & _
                            AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & vbCrLf
                        Case "57"   'CO Proposed
                            BatchText = BatchText & _
                            AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "57                      " & COProposed & "  0000000" & UserAFSCode & "            N" & UpdateCode & vbCrLf & _
                            AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & vbCrLf
                        Case "X1"   'CO Executed
                            BatchText = BatchText & _
                            AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "X1                      " & COExecuted & "01" & PenaltyAmount & UserAFSCode & "            N" & UpdateCode & _
                                vbCrLf & _
                            AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & vbCrLf
                        Case "AS"   'CO Resolved
                            BatchText = BatchText & _
                            AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "AS                      " & COResolved & "  0000000" & UserAFSCode & "            N" & UpdateCode & vbCrLf & _
                            AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & vbCrLf
                        Case "Z4"   'Stipulated Penalties
                            BatchText = BatchText & _
                            AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "Z4                      " & StipulatedDate & "01" & StipulatedPenalty & UserAFSCode & "            N" & UpdateCode & vbCrLf & _
                            AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & vbCrLf
                        Case "60"   'AO to AG
                            BatchText = BatchText & _
                            AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "60                      " & AOExecuted & "  0000000" & UserAFSCode & "            N" & UpdateCode & vbCrLf & _
                            AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & vbCrLf
                        Case "64"   'Civil Court
                            BatchText = BatchText & _
                            AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "64                      " & CivilCourt & "  0000000" & UserAFSCode & "            N" & UpdateCode & vbCrLf & _
                            AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & vbCrLf
                        Case "XX"   'AO Resolved
                            BatchText = BatchText & _
                            AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "AS                      " & AOResolved & "010000000" & UserAFSCode & "            N" & UpdateCode & vbCrLf & _
                            AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & vbCrLf
                    End Select
                End If
            End While
            dr.Close()

            SQL = "Select   distinct " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strUpDateStatus,   " & _
            "" & connNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber,   " & _
            "numStaffResponsible,    " & _
            "to_Char(datDiscoveryDate, 'YYMMDD') as DiscoveryDate,    " & _
            "to_char(datDayZero, 'YYMMDD') as DayZero,    " & _
            "to_char(datEnforcementFinalized, 'YYMMDD') as AchievedDate,    " & _
            "to_char(datNOVSent, 'YYMMDD') as NOVSent,    " & _
            "to_char(datNFALetterSent, 'YYMMDD') as NFALetterSent,    " & _
            "to_char(datCOProposed, 'YYMMDD') as COProposed,    " & _
            "to_char(datCOExecuted, 'YYMMDD') as COExecuted,    " & _
            "to_Char(datCOResolved, 'YYMMDD') as COResolved,    " & _
            "to_char(datAOExecuted, 'YYMMDD') as AOExecuted,    " & _
            "to_char(datAOAppealed, 'YYMMDD') as AOAppealed,    " & _
            "to_char(datAOResolved, 'YYMMDD') as AOResolved,    " & _
            "to_char(SSCP_AuditedEnforcement.datModifingDate, 'YYMMDD') as StipulatedDate,    " & _
            "strCOPenaltyAmount,    " & _
            "" & connNameSpace & ".SSCPENforcementStipulated.strStipulatedPenalty,    " & _
            "" & connNameSpace & ".APBHeaderData.strAIRSNumber, strAIRProgramCodes,    " & _
            "" & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber,    " & _
            "strAFSKeyActionNumber, " & connNameSpace & ".SSCP_AuditedEnforcement.strTrackingNumber,    " & _
            "case    " & _
            "when " & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber = strAFSKeyActionNumber then '04'    " & _
            "when " & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber = strAFSNOVSentNumber then '56'    " & _
            "when " & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber = strAFSNOVResolvedNumber then 'AW'    " & _
            "when " & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber = strAFSCOProposedNumber then '57'    " & _
            "when " & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber = strAFSCOExecutedNumber then 'X1'    " & _
            "when " & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber = strAFSCOResolvedNumber then 'AS'    " & _
            "when " & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber = strAFSStipulatedPenaltyNumber then 'Z4'  " & _
            "when " & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber = strAFSAOtoAGNumber then '60'    " & _
            "when " & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber = strAFSCivilCourtNumber then '64'    " & _
            "when " & connNameSpace & ".afssscpenforcementrecords.strAFSActionNumber = strAFSAOResolvedNumber then 'AS'    " & _
            "Else 'ERROR'     " & _
            "End as ActionType,    " & _
            "strPollutants, strHPV,   " & _
            "" & connNameSpace & ".afsismprecords.strafsactionnumber as LinkingEvent   " & _
            "from " & connNameSpace & ".AFSSSCPEnforcementRecords,    " & _
            "" & connNameSpace & ".SSCP_AuditedEnforcement,     " & _
            "" & connNameSpace & ".APBHeaderData, " & connNameSpace & ".SSCPENforcementStipulated,    " & _
            "" & connNameSpace & ".SSCPItemMaster,   " & _
            "" & connNameSpace & ".sscptestreports,   " & _
            "" & connNameSpace & ".AFSISMPRecords   " & _
            "where " & connNameSpace & ".afssscpenforcementrecords.strUpdateStatus <> 'N'    " & _
            "and " & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber = " & _
            "" & connNameSpace & ".AFSSSCPEnforcementRecords.strEnforcementNumber    " & _
            "and " & connNameSpace & ".APBHeaderData.strAIRSNumber = " & connNameSpace & ".SSCP_AuditedEnforcement.strAIRSNumber    " & _
            "and " & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber = " & _
            "" & connNameSpace & ".SSCPENforcementStipulated.strEnforcementNumber (+)    " & _
            "and " & connNameSpace & ".SSCP_AuditedEnforcement.strTrackingNumber = " & _
            "" & connNameSpace & ".SSCPItemMaster.strTrackingNumber (+)   " & _
            "and " & connNameSpace & ".SSCP_AuditedEnforcement.strTrackingNumber = " & _
            "" & connNameSpace & ".SSCPTestReports.strTrackingNumber (+)  " & _
            "and " & connNameSpace & ".sscptestReports.strReferenceNumber  = " & _
            "" & connNameSpace & ".AFSISMPRecords.strReferenceNumber  (+)  " & _
            "and (strEventType = '03' or strEventType is null) " & _
            "order by strairsnumber, discoverydate "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                AIRSNumber = Mid(dr.Item("strAIRSNumber"), 3)
                If IsDBNull(dr.Item("strUpDatestatus")) Then
                    UpdateCode = "A"
                Else
                    UpdateCode = dr.Item("strUpDateStatus")
                End If
                AIRProgramCodes = ""
                If IsDBNull(dr.Item("strAIRProgramCodes")) Then
                    AIRProgramCodes = "0"
                Else
                    If Mid(dr.Item("strAIRProgramCodes"), 1, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "0"
                    End If
                    'If Mid(dr.Item("strAIRProgramCodes"), 2, 1) = 1 Then
                    '    AIRProgramCodes = AIRProgramCodes & "1"
                    'End If
                    If Mid(dr.Item("strAIRProgramCodes"), 3, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "3"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 4, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "4"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 5, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "6"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 6, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "7"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 7, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "8"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 8, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "9"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 9, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "F"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 10, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "A"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 11, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "I"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 12, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "M"
                    End If
                    If Mid(dr.Item("strAIRProgramCodes"), 13, 1) = 1 Then
                        AIRProgramCodes = AIRProgramCodes & "V"
                    End If
                    If AIRProgramCodes = "" Then
                        AIRProgramCodes = "0"
                    End If
                End If
                If AIRProgramCodes.Length > 6 Then
                    AIRProgramCodes = Mid(AIRProgramCodes, 1, 6)
                End If
                len = AIRProgramCodes.Length
                If len <> 6 Then
                    For i = len To 5
                        AIRProgramCodes = AIRProgramCodes & " "
                    Next
                End If
                UserAFSCode = "G36"
                If IsDBNull(dr.Item("ActionType")) Then
                    ActionType = "XX"
                Else
                    ActionType = dr.Item("ActionType")
                End If
                If IsDBNull(dr.Item("strHPV")) Then
                    HPV = "NOV"
                Else
                    HPV = dr.Item("strHPV")
                End If
                If ActionType = "04" Then
                    If HPV = "NOV" Then
                        ActionType = "VZ"
                    Else
                        ActionType = ActionType
                    End If
                End If
                If IsDBNull(dr.Item("DiscoveryDate")) Then
                    DiscoveryDate = Format(Date.Today, "yyMMdd")
                Else
                    DiscoveryDate = dr.Item("DiscoveryDate")
                End If
                If IsDBNull(dr.Item("DayZero")) Then
                    DayZero = Format(Date.Today, "yyMMdd")
                Else
                    DayZero = dr.Item("DayZero")
                End If
                If IsDBNull(dr.Item("NOVSent")) Then
                    NOVSent = Format(Date.Today, "yyMMdd")
                Else
                    NOVSent = dr.Item("NOVSent")
                End If
                If IsDBNull(dr.Item("NFALetterSent")) Then
                    NFALetter = Format(Date.Today, "yyMMdd")
                Else
                    NFALetter = dr.Item("NFALetterSent")
                End If
                If IsDBNull(dr.Item("COProposed")) Then
                    COProposed = Format(Date.Today, "yyMMdd")
                Else
                    COProposed = dr.Item("COProposed")
                End If
                If IsDBNull(dr.Item("COExecuted")) Then
                    COExecuted = Format(Date.Today, "yyMMdd")
                Else
                    COExecuted = dr.Item("COExecuted")
                End If
                If IsDBNull(dr.Item("COResolved")) Then
                    COResolved = Format(Date.Today, "yyMMdd")
                Else
                    COResolved = dr.Item("COResolved")
                End If
                If IsDBNull(dr.Item("AOExecuted")) Then
                    AOExecuted = Format(Date.Today, "yyMMdd")
                Else
                    AOExecuted = dr.Item("AOExecuted")
                End If
                If IsDBNull(dr.Item("AOAppealed")) Then
                    CivilCourt = Format(Date.Today, "yyMMdd")
                Else
                    CivilCourt = dr.Item("AOAppealed")
                End If
                If IsDBNull(dr.Item("AOResolved")) Then
                    AOResolved = Format(Date.Today, "yyMMdd")
                Else
                    AOResolved = dr.Item("AOResolved")
                End If
                If IsDBNull(dr.Item("StipulatedDate")) Then
                    StipulatedDate = Format(Date.Today, "yyMMdd")
                Else
                    StipulatedDate = dr.Item("StipulatedDate")
                End If
                If IsDBNull(dr.Item("strCOPenaltyAmount")) Then
                    PenaltyAmount = "0"
                Else
                    PenaltyAmount = Replace(dr.Item("strCOPenaltyAmount"), "$", "")
                    PenaltyAmount = Replace(PenaltyAmount, ",", "")
                    If PenaltyAmount.Contains(".") Then
                        PenaltyAmount = Mid(PenaltyAmount, 1, (PenaltyAmount.IndexOf(".")))
                    End If
                End If
                If PenaltyAmount.Length > 7 Then
                    PenaltyAmount = CSng(PenaltyAmount)
                End If
                len = PenaltyAmount.Length
                If PenaltyAmount.Length <> 7 Then
                    For i = len To 6
                        PenaltyAmount = "0" & PenaltyAmount
                    Next
                End If
                If IsDBNull(dr.Item("strStipulatedPenalty")) Then
                    StipulatedPenalty = "0"
                Else
                    StipulatedPenalty = Replace(dr.Item("strStipulatedPenalty"), "$", "")
                End If
                If StipulatedPenalty.Contains(".") Then
                    StipulatedPenalty = Mid(StipulatedPenalty, 1, (StipulatedPenalty.IndexOf(".")))
                End If
                If StipulatedPenalty.Length > 7 Then
                    StipulatedPenalty = CSng(StipulatedPenalty)
                End If
                len = StipulatedPenalty.Length
                If StipulatedPenalty.Length <> 7 Then
                    For i = len To 6
                        StipulatedPenalty = "0" & StipulatedPenalty
                    Next
                End If
                If IsDBNull(dr.Item("strTrackingNumber")) Then
                    TrackingNumber = ""
                Else
                    TrackingNumber = dr.Item("strTrackingNumber")
                End If
                If IsDBNull(dr.Item("strAFSActionNumber")) Then
                    ActionNumber = ""
                Else
                    ActionNumber = dr.Item("strAFSActionNumber")
                End If
                Select Case ActionNumber.Length
                    Case "0"
                        ActionNumber = "00001"
                    Case "1"
                        ActionNumber = "0000" & ActionNumber
                    Case "2"
                        ActionNumber = "000" & ActionNumber
                    Case "3"
                        ActionNumber = "00" & ActionNumber
                    Case "4"
                        ActionNumber = "0" & ActionNumber
                    Case "5"
                        ActionNumber = ActionNumber
                    Case Else
                        ActionNumber = "00001"
                End Select
                If IsDBNull(dr.Item("strAFSKeyActionNumber")) Then
                    KeyActionNumber = ""
                Else
                    KeyActionNumber = dr.Item("strAFSKeyActionNumber")
                End If
                Select Case KeyActionNumber.Length
                    Case "0"
                        KeyActionNumber = "00001"
                    Case "1"
                        KeyActionNumber = "0000" & KeyActionNumber
                    Case "2"
                        KeyActionNumber = "000" & KeyActionNumber
                    Case "3"
                        KeyActionNumber = "00" & KeyActionNumber
                    Case "4"
                        KeyActionNumber = "0" & KeyActionNumber
                    Case "5"
                        KeyActionNumber = KeyActionNumber
                    Case Else
                        KeyActionNumber = "00001"
                End Select
                If IsDBNull(dr.Item("strPollutants")) Then
                    Pollutants = "0OT,"
                Else
                    Pollutants = dr.Item("strPollutants")
                End If

                Pollutant1 = Mid(Pollutants, 2, (InStr(Pollutants, ",", CompareMethod.Text) - 2))
                Pollutants = Mid(Pollutants, (InStr(Pollutants, ",", CompareMethod.Text) + 1))
                If Pollutants <> "" Then
                    Pollutant2 = Mid(Pollutants, 2, (InStr(Pollutants, ",", CompareMethod.Text) - 2))
                    Pollutants = Mid(Pollutants, (InStr(Pollutants, ",", CompareMethod.Text) + 1))
                    If Pollutants <> "" Then
                        Pollutant3 = Mid(Pollutants, 2, (InStr(Pollutants, ",", CompareMethod.Text) - 2))
                        Pollutants = Mid(Pollutants, (InStr(Pollutants, ",", CompareMethod.Text) + 1))
                    Else
                        Pollutant3 = ""
                    End If
                Else
                    Pollutant2 = ""
                    Pollutant3 = ""
                End If
                If Pollutant2 = Pollutant1 Then
                    Pollutant2 = ""
                End If
                If Pollutant3 = Pollutant1 Then
                    Pollutant3 = ""
                End If

                If Pollutant1.Length > 5 Then
                    Pollutant1 = Mid(Pollutant1, 1, 5)
                End If
                len = Pollutant1.Length
                If len <> 5 Then
                    For i = len To 4
                        Pollutant1 = Pollutant1 & " "
                    Next
                End If
                If Pollutant2.Length > 5 Then
                    Pollutant2 = Mid(Pollutant2, 1, 5)
                End If
                len = Pollutant2.Length
                If len <> 5 Then
                    For i = len To 4
                        Pollutant2 = Pollutant2 & " "
                    Next
                End If
                If Pollutant3.Length > 5 Then
                    Pollutant1 = Mid(Pollutant3, 1, 5)
                End If
                len = Pollutant3.Length
                If len <> 5 Then
                    For i = len To 4
                        Pollutant3 = Pollutant3 & " "
                    Next
                End If
                If IsDBNull(dr.Item("LinkingEvent")) Then
                    LinkedEvent = ""
                Else
                    LinkedEvent = dr.Item("LinkingEvent")
                End If
                Select Case LinkedEvent.Length
                    Case 0
                        LinkedEvent = "     "
                    Case 1
                        LinkedEvent = "0000" & LinkedEvent
                    Case 2
                        LinkedEvent = "000" & LinkedEvent
                    Case 3
                        LinkedEvent = "00" & LinkedEvent
                    Case 4
                        LinkedEvent = "0" & LinkedEvent
                    Case 5
                        LinkedEvent = LinkedEvent
                    Case Else
                        LinkedEvent = "     "
                End Select

                Select Case ActionType
                    Case "04"   'Day Zero 
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "04                      " & DayZero & "010000000" & UserAFSCode & "            N" & UpdateCode & _
                           vbCrLf & _
                        AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                   " & Pollutant1 & Pollutant2 & Pollutant3 & "                      " & UpdateCode & _
                           vbCrLf & _
                        AIRSNumber & "164" & ActionNumber & KeyActionNumber & "                    " & HPV & "                                 " & UpdateCode & _
                           vbCrLf
                        If LinkedEvent <> "" And LinkedEvent <> "     " Then
                            BatchText = BatchText & _
                            AIRSNumber & "163" & LinkedEvent & KeyActionNumber & "                                                        " & UpdateCode & vbCrLf
                        End If
                    Case "VZ" 'Non HPV
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "VZ                      " & DayZero & "010000000" & UserAFSCode & "            N" & UpdateCode & _
                           vbCrLf & _
                        AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                   " & Pollutant1 & Pollutant2 & Pollutant3 & "                      " & UpdateCode & _
                           vbCrLf
                        If LinkedEvent <> "" And LinkedEvent <> "     " Then
                            BatchText = BatchText & _
                            AIRSNumber & "163" & LinkedEvent & KeyActionNumber & "                                                        " & UpdateCode & vbCrLf
                        End If
                    Case "56"   'NOV Sent 
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "56                      " & NOVSent & "  0000000" & UserAFSCode & "            N" & UpdateCode & _
                           vbCrLf & _
                        AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & _
                           vbCrLf
                    Case "AW"   'NFA Letter Sent
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "AW                      " & NFALetter & "  0000000" & UserAFSCode & "            N" & UpdateCode & _
                           vbCrLf & _
                        AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & _
                           vbCrLf
                    Case "57"   'CO Proposed
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "57                      " & COProposed & "  0000000" & UserAFSCode & "            N" & UpdateCode & _
                           vbCrLf & _
                        AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & _
                           vbCrLf
                    Case "X1"   'CO Executed
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "X1                      " & COExecuted & "01" & PenaltyAmount & UserAFSCode & "            N" & UpdateCode & _
                           vbCrLf & _
                        AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & _
                           vbCrLf
                    Case "AS"   'CO Resolved
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "AS                      " & COResolved & "  0000000" & UserAFSCode & "            N" & UpdateCode & vbCrLf & _
                        AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & vbCrLf
                    Case "Z4"   'Stipulated Penalties
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "Z4                      " & StipulatedDate & "01" & StipulatedPenalty & UserAFSCode & "            N" & UpdateCode & _
                           vbCrLf & _
                        AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & _
                           vbCrLf
                    Case "60"   'AO to AG
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "60                      " & AOExecuted & "  0000000" & UserAFSCode & "            N" & UpdateCode & _
                           vbCrLf & _
                        AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & _
                           vbCrLf
                    Case "64"   'Civil Court
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "64                      " & CivilCourt & "  0000000" & UserAFSCode & "            N" & UpdateCode & _
                           vbCrLf & _
                        AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & _
                           vbCrLf
                    Case "XX"   'AO Resolved
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "AS                      " & AOResolved & "010000000" & UserAFSCode & "            N" & UpdateCode & _
                           vbCrLf & _
                        AIRSNumber & "163" & ActionNumber & KeyActionNumber & "                                                        " & UpdateCode & _
                           vbCrLf
                End Select
            End While
            dr.Close()

            SQL = "Update " & connNameSpace & ".AFSSSCPEnforcementRecords set " & _
            "strUpDateStatus = 'N' " & _
            "where strUPDateStatus <> 'N' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & BatchText

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub
    Sub ISMPActions()
        Dim BatchText As String = ""
        Dim len As Integer
        Dim i As Integer
        Dim AIRSNumber As String = ""
        Dim ActionNumber As String = ""
        Dim AirProgramCodes As String = ""
        Dim ActionType As String = ""
        Dim ResultsCode As String = ""
        Dim DateAchieved As String = ""
        Dim Pollutant As String = ""
        Dim Staff As String = ""
        Dim UpdateStatus As String = ""
        Dim ReferenceNumber As String = ""
        Dim CommentLine As String = ""

        SQL = "Select " & connNameSpace & ".AFSISMPRecords.strReferenceNumber, " & _
        "strAfsActionNumber, substr(" & connNameSpace & ".ISMPMaster.strAIRSNumber, 3) as AIRSNumber,  " & _
        "strAIRProgramCodes,  " & _
        "case  " & _
        "	when strWitnessingEngineer = '0' then 'TR'  " & _
        "	Else '23' " & _
        "END as ActionType,  " & _
        "case  " & _
        "	WHEN strComplianceStatus = '01' then 'N/A' " & _
        "	when strComplianceStatus = '02' then 'PP' " & _
        "	when strComplianceStatus = '03' then 'PP'  " & _
        "	when strComplianceStatus = '04' then 'N/A'  " & _
        "	when strComplianceStatus = '05' then 'FF'  " & _
        "End as ResultsCode,  " & _
        "to_char(datTestDateEnd, 'YYMMDD') as DateAchieved, " & _
        "strReviewingEngineer, strUpdateStatus, strPollutant, " & _
        "mmoCommentArea, strafscode  " & _
        "from " & connNameSpace & ".AFSISMPRecords, " & connNameSpace & ".ISMPMaster, " & _
        "" & connNameSpace & ".ISMPReportInformation, " & connNameSpace & ".APBHeaderData, " & _
        "" & connNameSpace & ".LookUPPollutants  " & _
        "where " & connNameSpace & ".ISMPMaster.strReferenceNumber = " & connNameSpace & ".AFSISMPRecords.strReferenceNumber  " & _
        "and " & connNameSpace & ".ISMPMaster.strReferenceNumber = " & connNameSpace & ".ISMPReportInformation.strReferenceNumber  " & _
        "and " & connNameSpace & ".ISMPMaster.strAIRSNumber = " & connNameSpace & ".APBHeaderData.strAIRSnumber " & _
        "and " & connNameSpace & ".LookUPPollutants.strPollutantcode = " & connNameSpace & ".ISMPReportInformation.strPollutant " & _
        "and strUpdateStatus <> 'N'"

        Try
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                AIRSNumber = dr.Item("AIRSNumber")
                ActionNumber = dr.Item("strAFSActionNumber")
                Select Case ActionNumber.Length
                    Case "0"
                        ActionNumber = "00001"
                    Case "1"
                        ActionNumber = "0000" & ActionNumber
                    Case "2"
                        ActionNumber = "000" & ActionNumber
                    Case "3"
                        ActionNumber = "00" & ActionNumber
                    Case "4"
                        ActionNumber = "0" & ActionNumber
                    Case "5"
                        ActionNumber = ActionNumber
                    Case Else
                        ActionNumber = "00001"
                End Select

                ActionType = dr.Item("ActionType")
                ResultsCode = dr.Item("resultsCode")
               
                DateAchieved = dr.Item("DateAchieved")
                AirProgramCodes = ""

                If Mid(dr.Item("strAIRProgramCodes"), 1, 1) = 1 Then
                    AirProgramCodes = AirProgramCodes & "0"
                End If
                'If Mid(dr.Item("strAIRProgramCodes"), 2, 1) = 1 Then
                '    AirProgramCodes = AirProgramCodes & "1"
                'End If
                If Mid(dr.Item("strAIRProgramCodes"), 3, 1) = 1 Then
                    AirProgramCodes = AirProgramCodes & "3"
                End If
                If Mid(dr.Item("strAIRProgramCodes"), 4, 1) = 1 Then
                    AirProgramCodes = AirProgramCodes & "4"
                End If
                If Mid(dr.Item("strAIRProgramCodes"), 5, 1) = 1 Then
                    AirProgramCodes = AirProgramCodes & "6"
                End If
                If Mid(dr.Item("strAIRProgramCodes"), 6, 1) = 1 Then
                    AirProgramCodes = AirProgramCodes & "7"
                End If
                If Mid(dr.Item("strAIRProgramCodes"), 7, 1) = 1 Then
                    AirProgramCodes = AirProgramCodes & "8"
                End If
                If Mid(dr.Item("strAIRProgramCodes"), 8, 1) = 1 Then
                    AirProgramCodes = AirProgramCodes & "9"
                End If
                If Mid(dr.Item("strAIRProgramCodes"), 9, 1) = 1 Then
                    AirProgramCodes = AirProgramCodes & "F"
                End If
                If Mid(dr.Item("strAIRProgramCodes"), 10, 1) = 1 Then
                    AirProgramCodes = AirProgramCodes & "A"
                End If
                If Mid(dr.Item("strAIRProgramCodes"), 11, 1) = 1 Then
                    AirProgramCodes = AirProgramCodes & "I"
                End If
                If Mid(dr.Item("strAIRProgramCodes"), 12, 1) = 1 Then
                    AirProgramCodes = AirProgramCodes & "M"
                End If
                If Mid(dr.Item("strAIRProgramCodes"), 13, 1) = 1 Then
                    AirProgramCodes = AirProgramCodes & "V"
                End If
                Staff = "GM8"
                UpdateStatus = dr.Item("strUpdateStatus")

                If AirProgramCodes.Length > 6 Then
                    AirProgramCodes = Mid(AirProgramCodes, 1, 6)
                End If
                If AirProgramCodes = "" Then
                    AirProgramCodes = "0"
                End If
                If AirProgramCodes.Length <> 6 Then
                    len = AirProgramCodes.Length
                    For i = len To 5
                        AirProgramCodes = AirProgramCodes & " "
                    Next
                End If

                len = ActionType.Length
                For i = len To 23
                    ActionType = ActionType & " "
                Next
              
                len = Staff.Length
                For i = len To 4
                    Staff = Staff & " "
                Next
                If IsDBNull(dr.Item("strafscode")) Then
                    Pollutant = "     "
                Else
                    If dr.Item("strafscode") = True Then
                        If IsDBNull(dr.Item("strPollutant")) Then
                            Pollutant = "     "
                        Else
                            Pollutant = dr.Item("strPOllutant")
                        End If
                    End If
                End If

                len = Pollutant.Length
                'This line was changed when AFS went to 5-digit Action Numbers
                'For i = len To 12
                '    Pollutant = Pollutant & " "
                'Next
                For i = len To 10
                    Pollutant = Pollutant & " "
                Next

                If IsDBNull(dr.Item("strReferenceNumber")) Then
                    ReferenceNumber = ""
                Else
                    ReferenceNumber = dr.Item("strReferenceNumber")
                End If

                CommentLine = "GA EPD #:" & ReferenceNumber
                'This was changed when AFS went to 5-digit AFS Number
                'CommentLine = Mid(CommentLine, 1, 55)

                CommentLine = Mid(CommentLine, 1, 53)

                len = CommentLine.Length
                'For i = len To 54
                '    CommentLine = CommentLine & " "
                'Next

                For i = len To 52
                    CommentLine = CommentLine & " "
                Next

                Select Case ResultsCode
                    Case "PP"
                        BatchText = BatchText & AIRSNumber & "161" & ActionNumber & AirProgramCodes & _
                        ActionType & DateAchieved & "PP       " & Staff & Pollutant & UpdateStatus & vbCrLf & _
                        AIRSNumber & "171  " & ActionNumber & "001C" & CommentLine & "  " & UpdateStatus & vbCrLf
                    Case "FF"
                        BatchText = BatchText & AIRSNumber & "161" & ActionNumber & AirProgramCodes & _
                        ActionType & DateAchieved & "FF       " & Staff & Pollutant & UpdateStatus & vbCrLf & _
                        AIRSNumber & "171  " & ActionNumber & "001C" & CommentLine & "  " & UpdateStatus & vbCrLf
                    Case Else
                End Select
            End While

            SQL = "Update " & connNameSpace & ".AFSISMPRecords set " & _
            "strUpDateStatus = 'N' " & _
            "where strUPDateStatus <> 'N' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & BatchText

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
         
        End Try
         

    End Sub
    Sub FindRegion(ByVal Region As String, ByVal AIRSNumber As String)
        Try
            
            If Len(AIRSNumber) = 12 And IsNumeric(AIRSNumber) Then
                SQL = "Select (" & connNameSpace & ".LookUPDistricts.strDistrictcode|| '-'||strDistrictName) as District " & _
                "from " & connNameSpace & ".LookUPDistricts, " & connNameSpace & ".LookUPDistrictInformation " & _
                "where " & connNameSpace & ".LookUPDistricts.strDistrictCode = " & connNameSpace & ".LookUPDistrictInformation.strDistrictCode " & _
                "and strDistrictCounty = '" & Mid(AIRSNumber, 5, 3) & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read

                If recExist Then
                    Region = dr.Item("District")
                Else
                    Region = "WARNING"
                End If
            Else
                Region = "WARNING"
            End If

            txtCDSRegionCode.Text = Region

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
       
        End Try
         

    End Sub
    Sub CreateNewFacility()
        Dim AIRSNumber As String = ""
        Dim FacilityName As String = ""
        Dim FacilityStreet As String = ""
        Dim FacilityCity As String = ""
        Dim FacilityZipCode As String = ""
        Dim FacilityLongitude As String = ""
        Dim FacilityLatitude As String = ""
        Dim MailingStreet As String = ""
        Dim MailingCity As String = ""
        Dim MailingState As String = ""
        Dim MailingZipCode As String = ""
        Dim OperatingStatus As String = ""
        Dim Classification As String = ""
        Dim AirProgramCode As String = ""
        Dim SICCode As String = ""
        Dim DistrictOffice As String = ""
        Dim PlantDesc As String = ""
        Dim ContactFirstName As String = ""
        Dim ContactLastName As String = ""
        Dim ContactPrefix As String = ""
        Dim ContactSuffix As String = ""
        Dim ContactTitle As String = ""
        Dim ContactPhoneNumber As String = ""

        Try
            txtCDSAIRSNumber.BackColor = Color.White
            txtCDSFacilityName.BackColor = Color.White

            If txtCDSAIRSNumber.Text.Length = 12 Then
                AIRSNumber = txtCDSAIRSNumber.Text

                SQL = "Select strAIRSNumber " & _
                "from " & connNameSpace & ".APBMasterAIRS " & _
                "where strAIRSNumber = '" & AIRSNumber & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = False Then
                    If txtCDSFacilityName.Text <> "" Then
                        FacilityName = txtCDSFacilityName.Text
                        If txtCDSStreetAddress.Text <> "" Then
                            FacilityStreet = txtCDSStreetAddress.Text
                        Else
                            FacilityStreet = "N/A"
                        End If
                        If txtCDSCity.Text <> "" Then
                            FacilityCity = txtCDSCity.Text
                        Else
                            FacilityCity = "N/A"
                        End If
                        If mtbCDSZipCode.Text <> "" Then
                            FacilityZipCode = mtbCDSZipCode.Text
                        Else
                            FacilityZipCode = "00000"
                        End If
                        If mtbFacilityLongitude.Text <> "" Then
                            FacilityLongitude = mtbFacilityLongitude.Text
                        Else
                            FacilityLongitude = "00.000000"
                        End If
                        If mtbFacilityLatitude.Text <> "" Then
                            FacilityLatitude = mtbFacilityLatitude.Text
                        Else
                            FacilityLatitude = "-00.000000"
                        End If
                        If cboCDSOperationalStatus.Items.Contains(cboCDSOperationalStatus.Text) Then
                            OperatingStatus = Mid(cboCDSOperationalStatus.Text, 1, 1)
                        Else
                            OperatingStatus = "O"
                        End If
                        If cboCDSClassCode.Items.Contains(cboCDSClassCode.Text) Then
                            Classification = Mid(cboCDSClassCode.Text, 1, (InStr(1, cboCDSClassCode.Text, "-", CompareMethod.Text)) - 2)
                        Else
                            Classification = "C"
                        End If

                        AirProgramCode = "000000000000000"

                        If chbCDS_1.Checked = True Then
                            AirProgramCode = "1" & Mid(AirProgramCode, 2)
                        End If
                        If chbCDS_2.Checked = True Then
                            AirProgramCode = Mid(AirProgramCode, 1, 1) & "1" & Mid(AirProgramCode, 3)
                        End If
                        If chbCDS_3.Checked = True Then
                            AirProgramCode = Mid(AirProgramCode, 1, 2) & "1" & Mid(AirProgramCode, 4)
                        End If
                        If chbCDS_4.Checked = True Then
                            AirProgramCode = Mid(AirProgramCode, 1, 3) & "1" & Mid(AirProgramCode, 5)
                        End If
                        If chbCDS_5.Checked = True Then
                            AirProgramCode = Mid(AirProgramCode, 1, 4) & "1" & Mid(AirProgramCode, 6)
                        End If
                        If chbCDS_6.Checked = True Then
                            AirProgramCode = Mid(AirProgramCode, 1, 5) & "1" & Mid(AirProgramCode, 7)
                        End If
                        If chbCDS_7.Checked = True Then
                            AirProgramCode = Mid(AirProgramCode, 1, 6) & "1" & Mid(AirProgramCode, 8)
                        End If
                        If chbCDS_8.Checked = True Then
                            AirProgramCode = Mid(AirProgramCode, 1, 7) & "1" & Mid(AirProgramCode, 9)
                        End If
                        If chbCDS_9.Checked = True Then
                            AirProgramCode = Mid(AirProgramCode, 1, 8) & "1" & Mid(AirProgramCode, 10)
                        End If
                        If chbCDS_10.Checked = True Then
                            AirProgramCode = Mid(AirProgramCode, 1, 9) & "1" & Mid(AirProgramCode, 11)
                        End If
                        If chbCDS_11.Checked = True Then
                            AirProgramCode = Mid(AirProgramCode, 1, 10) & "1" & Mid(AirProgramCode, 12)
                        End If
                        If chbCDS_12.Checked = True Then
                            AirProgramCode = Mid(AirProgramCode, 1, 11) & "1" & Mid(AirProgramCode, 13)
                        End If
                        If chbCDS_13.Checked = True Then
                            AirProgramCode = Mid(AirProgramCode, 1, 12) & "1" & Mid(AirProgramCode, 14)
                        End If

                        If AirProgramCode.Length <> 15 Then
                            AirProgramCode = "100000000000000"
                        Else
                            AirProgramCode = AirProgramCode
                        End If
                        If mtbCDSSICCode.Text <> "" Then
                            SICCode = mtbCDSSICCode.Text
                        Else
                            SICCode = "0000"
                        End If
                        If txtCDSRegionCode.Text <> "" Then
                            DistrictOffice = Mid(txtCDSRegionCode.Text, 1, 1)
                        Else
                            DistrictOffice = "A"
                        End If
                        If txtFacilityDescription.Text <> "" Then
                            PlantDesc = txtFacilityDescription.Text
                        Else
                            PlantDesc = "N/A"
                        End If
                        If txtContactSocialTitle.Text <> "" Then
                            ContactPrefix = Replace(txtContactSocialTitle.Text, "'", "''")
                        Else
                            ContactPrefix = ""
                        End If
                        If txtContactPedigree.Text <> "" Then
                            ContactSuffix = Replace(txtContactPedigree.Text, "'", "''")
                        Else
                            ContactSuffix = ""
                        End If
                        If txtContactFirstName.Text <> "" Then
                            ContactFirstName = Replace(txtContactFirstName.Text, "'", "''")
                        Else
                            ContactFirstName = "N/A"
                        End If
                        If txtContactLastName.Text <> "" Then
                            ContactLastName = Replace(txtContactLastName.Text, "'", "''")
                        Else
                            ContactLastName = "N/A"
                        End If
                        If txtContactTitle.Text <> "" Then
                            ContactTitle = txtContactTitle.Text
                        Else
                            ContactTitle = "N/A"
                        End If
                        If mtbContactPhoneNumber.Text <> "" Then
                            ContactPhoneNumber = Replace(Replace(Replace(Replace(mtbContactPhoneNumber.Text, "(", ""), ")", ""), " ", ""), "-", "")
                        Else
                            ContactPhoneNumber = "0000000000"
                        End If
                        If mtbContactNumberExtension.Text <> "" Then
                            ContactPhoneNumber = ContactPhoneNumber & mtbContactNumberExtension.Text
                        End If
                        If txtMailingAddress.Text <> "" Then
                            MailingStreet = txtMailingAddress.Text
                        Else
                            MailingStreet = "N/A"
                        End If
                        If txtMailingCity.Text <> "" Then
                            MailingCity = txtMailingCity.Text
                        Else
                            MailingCity = "N/A"
                        End If
                        If txtMailingState.Text <> "" Then
                            MailingState = txtMailingState.Text
                        Else
                            MailingState = "GA"
                        End If
                        If mtbMailingZipCode.Text <> "" Then
                            MailingZipCode = mtbMailingZipCode.Text
                        Else
                            MailingZipCode = "00000"
                        End If

                        SQL = "Insert into " & connNameSpace & ".APBMasterAIRS " & _
                        "(strAIRSNumber, strModifingPerson, " & _
                        "datModifingDate) " & _
                        "values " & _
                        "('" & AIRSNumber & "', '" & UserGCode & "', " & _
                        "'" & OracleDate & "') "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Insert into " & connNameSpace & ".APBFacilityInformation " & _
                        "(strAIRSNumber, strFacilityName, " & _
                        "strFacilityStreet1, strFacilityStreet2, " & _
                        "strFacilityCity, strFacilityState, " & _
                        "strFacilityZipCode, strModifingPerson, " & _
                        "datModifingDate, numFacilityLongitude, " & _
                        "numFacilityLatitude, strHorizontalCollectionCode, " & _
                        "strHorizontalAccuracyMeasure, strHorizontalReferenceCode) " & _
                        "values " & _
                        "('" & AIRSNumber & "', '" & Replace(FacilityName, "'", "''") & "', " & _
                        "'" & Replace(FacilityStreet, "'", "''") & "', 'N/A', " & _
                        "'" & Replace(FacilityCity, "'", "''") & "', 'GA', " & _
                        "'" & FacilityZipCode & "', '" & UserGCode & "', " & _
                        "'" & OracleDate & "', " & FacilityLongitude & ", " & _
                        "" & FacilityLatitude & ", '007', " & _
                        "'25', '002') "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        Dim AttainmentStatus As String = ""

                        SQL = "select " & _
                        "strNonAttainment " & _
                        "from " & connNameSpace & ".LookUpCountyInformation " & _
                        "where strCountyCode = '" & Mid(AIRSNumber, 5, 3) & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("strNonAttainment")) Then
                                AttainmentStatus = "00000"
                            Else
                                AttainmentStatus = dr.Item("strNonAttainment")
                            End If
                        End While
                        dr.Close()

                        SQL = "Insert into " & connNameSpace & ".APBHeaderData " & _
                        "(strAIRSNumber, strOperationalStatus, " & _
                        "strClass, " & _
                        "strAIRProgramCodes, strSICCode, " & _
                        "strFEINumber, strModifingPerson, " & _
                        "datModifingDate, datStartUpDate, " & _
                        "datShutDownDate, strComments, " & _
                        "strPlantDescription, strAttainmentStatus) " & _
                        "values " & _
                        "('" & AIRSNumber & "', '" & OperatingStatus & "', " & _
                        "'" & Classification & "', " & _
                        "'" & AirProgramCode & "', '" & SICCode & "', " & _
                        "'N/A', '" & UserGCode & "', " & _
                        "'" & OracleDate & "', '', '', " & _
                        "'From ISMP Data Management Tool, by " & UserName & "', " & _
                        "'" & Replace(PlantDesc, "'", "''") & "', '" & AttainmentStatus & "') "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If

                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Insert into " & connNameSpace & ".APBSupplamentalData " & _
                        "(strAIRSNumber, datSSCPTestReportDue, " & _
                        "strModifingPerson, DatModifingDate, " & _
                        "strDistrictOffice, strCMSMember, " & _
                        "strAFSActionNumber) " & _
                        "values " & _
                         "('" & AIRSNumber & "', '', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'" & DistrictOffice & "', '', " & _
                         "'00001' ) "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If

                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "insert into " & connNameSpace & ".APBContactInformation " & _
                        "(strContactKey, strAIRSNumber, strKey, " & _
                        "strContactFirstName, strContactLastName, " & _
                        "strContactPrefix, strContactSuffix, " & _
                        "strContactTitle, strContactCompanyName, " & _
                        "strContactPhoneNumber1, strContactPhoneNumber2, " & _
                        "strContactFaxNumber, strContactEmail, " & _
                        "strContactAddress1, strContactAddress2, " & _
                        "strContactCity, strContactState, " & _
                        "strContactZipCode, strModifingPerson, " & _
                        "datModifingDate) " & _
                        "values " & _
                        "('" & AIRSNumber & "30', '" & AIRSNumber & "', '30', " & _
                        "'" & Replace(ContactFirstName, "'", "''") & "', '" & Replace(ContactLastName, "'", "''") & "', " & _
                        "'" & ContactPrefix & "', '" & ContactSuffix & "', " & _
                        "'" & Replace(ContactTitle, "'", "''") & "', 'N/A', " & _
                        "'" & ContactPhoneNumber & "', 'N/A', " & _
                        "'N/A', 'N/A', " & _
                        "'" & Replace(MailingStreet, "'", "''") & "', 'N/A', " & _
                        "'" & Replace(MailingCity, "'", "''") & "', '" & MailingState & "', " & _
                        "'" & MailingZipCode & "', '" & UserGCode & "', " & _
                        "'" & OracleDate & "') "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If

                        dr = cmd.ExecuteReader
                        dr.Close()

                        If chbCDS_1.Checked = True Then
                            SQL = "Insert into " & connNameSpace & ".APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "0', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_2.Checked = True Then
                            SQL = "Insert into " & connNameSpace & ".APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "1', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_3.Checked = True Then
                            SQL = "Insert into " & connNameSpace & ".APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "3', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_4.Checked = True Then
                            SQL = "Insert into " & connNameSpace & ".APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "4', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_5.Checked = True Then
                            SQL = "Insert into " & connNameSpace & ".APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "6', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_6.Checked = True Then
                            SQL = "Insert into " & connNameSpace & ".APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "7', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_7.Checked = True Then
                            SQL = "Insert into " & connNameSpace & ".APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "8', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_8.Checked = True Then
                            SQL = "Insert into " & connNameSpace & ".APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "9', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_9.Checked = True Then
                            SQL = "Insert into " & connNameSpace & ".APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "F', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_10.Checked = True Then
                            SQL = "Insert into " & connNameSpace & ".APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "A', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_11.Checked = True Then
                            SQL = "Insert into " & connNameSpace & ".APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "I', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_12.Checked = True Then
                            SQL = "Insert into " & connNameSpace & ".APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "M', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_13.Checked = True Then
                            SQL = "Insert into " & connNameSpace & ".APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "V', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If

                        SQL = "Insert into " & connNameSpace & ".SSCPDistrictResponsible " & _
                        "values " & _
                        "('" & AIRSNumber & "', 'False', " & _
                        "'1', sysdate) "
                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        ' SQL = "insert into " & connNameSpace & ".SSCPFacilityAssignment " & _
                        '"values " & _
                        '"('" & AIRSNumber & "', '', '', '', '') "

                        SQL = "Insert into " & connNameSpace & ".SSCPInspectionsRequired " & _
                        "(numKey, strAIRSnumber, intyear) " & _
                        "values " & _
                        "((Select max(numkey) + 1 from " & connNameSpace & ".SSCPInspectionsRequired), " & _
                        "'" & AIRSNumber & "', '" & Now.Year.ToString & "') "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        MsgBox("Facility Added to Integrated Air Information Platform", MsgBoxStyle.Information, "AFS Tools")

                    Else
                        MsgBox("A Facility Name must be provided at this time.", MsgBoxStyle.Information, "AFS Tools")
                        txtCDSFacilityName.BackColor = Color.PeachPuff
                    End If
                Else
                    MsgBox("This AIRS Number already exists in the platform.", MsgBoxStyle.Information, "AFS Tools")
                End If
            Else
                MsgBox("The AIRS Number must be 12 characters long." & vbCrLf & "Beginning with '0413'", MsgBoxStyle.Information, "AFS Tools")
                txtCDSAIRSNumber.BackColor = Color.PeachPuff
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
         

    End Sub
#Region "Clears"
    Sub ClearCreateFacility()
        Try
            
            txtCDSAIRSNumber.Text = "0413"
            txtCDSFacilityName.Text = ""
            txtCDSStreetAddress.Clear()
            txtCDSCity.Clear()
            mtbCDSZipCode.Clear()
            mtbFacilityLongitude.Clear()
            mtbFacilityLatitude.Clear()
            txtMailingAddress.Clear()
            txtMailingCity.Clear()
            txtMailingState.Text = "GA"
            mtbMailingZipCode.Clear()
            cboCDSOperationalStatus.Text = ""
            mtbCDSSICCode.Clear()
            cboCDSClassCode.Text = ""
            chbCDS_1.Checked = False
            chbCDS_2.Checked = False
            chbCDS_3.Checked = False
            chbCDS_4.Checked = False
            chbCDS_5.Checked = False
            chbCDS_6.Checked = False
            chbCDS_7.Checked = False
            chbCDS_8.Checked = False
            chbCDS_9.Checked = False
            chbCDS_10.Checked = False
            chbCDS_11.Checked = False
            chbCDS_12.Checked = False
            chbCDS_13.Checked = False
            txtFacilityDescription.Clear()
            txtContactFirstName.Clear()
            txtContactLastName.Clear()
            txtContactSocialTitle.Clear()
            txtContactPedigree.Clear()
            txtContactTitle.Clear()
            mtbContactPhoneNumber.Clear()
            mtbContactNumberExtension.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
        End Try
         
    End Sub

#End Region
    Sub LoadErrorLog()
        Try
            SQL = "Select " & _
            "strErrorNumber, " & _
            "(strLastName||', '||strFirstName) as ErrorUser,  " & _
            "strErrorLocation, strErrorMessage,  " & _
            "to_char(datErrorDate, 'DD-Mon-YYYY') as ErrorDate,  " & _
            "strSolution  " & _
            "from " & connNameSpace & ".IAIPErrorLog, " & connNameSpace & ".EPDUserProfiles  " & _
            "where " & connNameSpace & ".IAIPErrorLog.strUser = " & connNameSpace & ".EPDUserProfiles.numUserID "

            If rdbViewAllErrors.Checked = True Then
                SQL = SQL
            End If
            If rdbViewResolvedErrors.Checked = True Then
                SQL = SQL & " and strSolution IS NOT NUll "
            End If
            If rdbViewUnresolvedErrors.Checked = True Then
                SQL = SQL & " and strSolution IS NULL "
            End If

            If SQL <> "" Then
                dsErrorLog = New DataSet
                daErrorLog = New OracleDataAdapter(SQL, conn)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                daErrorLog.Fill(dsErrorLog, "ErrorLog")
                dgrErrorList.DataSource = dsErrorLog
                dgrErrorList.DataMember = "ErrorLog"

                txtErrorCount.Text = dsErrorLog.Tables(0).Rows.Count.ToString

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            "from " & connNameSpace & ".LogErrors "


            SQL = "select numError, " & _
            "strIPAddress, strUserEmail, " & _
            "strErrorPage, dateTimeStamp, " & _
            "strErrorMsg, strSolution " & _
            "From " & connNameSpace & ".OLAPERRORLog "

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
                daWebErrorLog = New OracleDataAdapter(SQL, conn)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                daWebErrorLog.Fill(dsWebErrorLog, "WebErrorLog")
                dgrWebErrorList.DataSource = dsWebErrorLog
                dgrWebErrorList.DataMember = "WebErrorLog"

                txtWebErrorCount.Text = dsWebErrorLog.Tables(0).Rows.Count.ToString

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
         
        End Try
         
    End Sub
    
 

#End Region
    Private Sub DEVDataManagementTools_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            
            If NavigationScreen Is Nothing Then
                NavigationScreen = New IAIPNavigation
            End If
            NavigationScreen.Show()

            DevelopersTools = Nothing

            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
        End Try
         
    End Sub
    Private Sub txtCDSAIRSNumber_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCDSAIRSNumber.Leave
        Try

            FindRegion(txtCDSRegionCode.Text, txtCDSAIRSNumber.Text)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnNewFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewFacility.Click
        Try
            CreateNewFacility()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub
    Private Sub llbFacilityInformation_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFacilityInformation.LinkClicked
        Try
            
            GBFacilityInformation.Visible = True
            GBFacilityInformation.Location = New Point(224, 0)
            GBFacilityInformation.Size = New System.Drawing.Size(560, 280)
            GBMailingLocation.Visible = False
            GBHeaderData.Visible = False
            GBAirProgramCodes.Visible = False
            GBContactInformation.Visible = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
         
        End Try
         
    End Sub
    Private Sub llbMailingLocation_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbMailingLocation.LinkClicked
        Try
            
            GBMailingLocation.Visible = True
            GBMailingLocation.Location = New Point(224, 0)
            GBMailingLocation.Size = New System.Drawing.Size(560, 120)
            GBFacilityInformation.Visible = False
            GBHeaderData.Visible = False
            GBAirProgramCodes.Visible = False
            GBContactInformation.Visible = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
         
    End Sub
    Private Sub llbHeaderData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbHeaderData.LinkClicked
        Try
            
            GBHeaderData.Visible = True
            GBHeaderData.Location = New Point(224, 0)
            GBHeaderData.Size = New System.Drawing.Size(560, 152)
            GBMailingLocation.Visible = False
            GBFacilityInformation.Visible = False
            GBAirProgramCodes.Visible = False
            GBContactInformation.Visible = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            
        End Try
         
    End Sub
    Private Sub llbAirProgramCodes_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbAirProgramCodes.LinkClicked
        Try
            
            GBAirProgramCodes.Visible = True
            GBAirProgramCodes.Location = New Point(224, 0)
            GBAirProgramCodes.Size = New System.Drawing.Size(560, 120)
            GBMailingLocation.Visible = False
            GBHeaderData.Visible = False
            GBFacilityInformation.Visible = False
            GBContactInformation.Visible = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
        End Try
         
    End Sub
    Private Sub llbContactInformation_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbContactInformation.LinkClicked
        Try
            
            GBContactInformation.Visible = True
            GBContactInformation.Location = New Point(224, 0)
            GBContactInformation.Size = New System.Drawing.Size(560, 157)
            GBMailingLocation.Visible = False
            GBHeaderData.Visible = False
            GBAirProgramCodes.Visible = False
            GBFacilityInformation.Visible = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
         
    End Sub
    Private Sub txtCDSAIRSNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCDSAIRSNumber.TextChanged
        Try
            
            If txtCDSAIRSNumber.Text.Length = 12 Then
                btnNewFacility.Enabled = True
                txtCDSAIRSNumber.BackColor = Color.White

                GBFacilityInformation.Visible = True
                GBFacilityInformation.Location = New Point(224, 0)
                GBFacilityInformation.Size = New System.Drawing.Size(560, 280)
                GBMailingLocation.Visible = False
                GBHeaderData.Visible = False
                GBAirProgramCodes.Visible = False
                GBContactInformation.Visible = False

                llbFacilityInformation.Enabled = True
                llbMailingLocation.Enabled = True
                llbHeaderData.Enabled = True
                llbAirProgramCodes.Enabled = True
                llbContactInformation.Enabled = True

            Else
                btnNewFacility.Enabled = False
                txtCDSAIRSNumber.BackColor = Color.PeachPuff

                llbFacilityInformation.Enabled = False
                llbMailingLocation.Enabled = False
                llbHeaderData.Enabled = False
                llbAirProgramCodes.Enabled = False
                llbContactInformation.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
         
        End Try
         
    End Sub
    Private Sub txtCDSStreetAddress_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCDSStreetAddress.TextChanged
        Try
            
            txtMailingAddress.Text = txtCDSStreetAddress.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
         
        End Try
         
    End Sub
    Private Sub txtCDSCity_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCDSCity.TextChanged
        Try
            
            txtMailingCity.Text = txtCDSCity.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
        End Try
         
    End Sub
    Private Sub btnGenerateBatchFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateBatchFile.Click
        Try
            GenerateBatchFile()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
         
    End Sub
    Private Sub btnClearAFSFileGenerator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAFSFileGenerator.Click
        Try
            
            txtAFSBatchFile.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
         
        End Try
         
    End Sub
    Private Sub btnClearAddNewFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAddNewFacility.Click
        Try
            
            ClearCreateFacility()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
        End Try
         
    End Sub

#Region "Mahesh Code for Web App Users"
    Function LoadComboBoxes() As DataTable
        Dim dtairs As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow
        Dim SQL As String

        Try
            

            SQL = "Select DISTINCT substr(strairsnumber, 5) as strairsnumber, " _
            + "strfacilityname " _
            + "from " & connNameSpace & ".APBFacilityInformation " _
            + "Order by strAIRSNumber "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            da.Fill(ds, "facilityInfo")

            dtairs.Columns.Add("strairsnumber", GetType(System.String))
            dtAIRS.Columns.Add("strfacilityname", GetType(System.String))

            drNewRow = dtAIRS.NewRow()
            drNewRow("strfacilityname") = " "
            drNewRow("strairsnumber") = " "
            dtAIRS.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("facilityInfo").Rows()
                drNewRow = dtAIRS.NewRow()
                drNewRow("strairsnumber") = drDSRow("strairsnumber")
                drNewRow("strfacilityname") = drDSRow("strfacilityname")
                dtAIRS.Rows.Add(drNewRow)
            Next

            Return dtairs

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            Return Nothing
        Finally
           
        End Try
         
    End Function

    Private Sub Back()
        Try
            
            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
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
            Dim updateString As String = "UPDATE " & connNameSpace & ".OlapUserAccess " & _
                      "SET intadminaccess = '" & admin & "', " & _
                      "intFeeAccess = '" & fee & "', " & _
                      "intEIAccess = '" & ei & "', " & _
                      "intESAccess = '" & es & "' " & _
                      "WHERE numUserID = '" & userid & "' " & _
                      "and strAirsNumber = '0413" & airsno & "' "

            Dim cmd As New OracleCommand(updateString, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()
          
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
 
 
#End Region
#Region "Fee Password Reset"
    Private Sub SetPassword_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            
            If NavigationScreen Is Nothing Then
                NavigationScreen = New IAIPNavigation
            End If
            NavigationScreen.Show()
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
         
    End Sub

#End Region

    Private Sub btnFilterErrors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterErrors.Click
        Try
            
            LoadErrorLog()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
        End Try
         
    End Sub
    Private Sub dgrErrorList_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrErrorList.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrErrorList.HitTest(e.X, e.Y)
        Try
            
            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrErrorList(hti.Row, 0)) Then
                    txtErrorNumber.Text = ""
                Else
                    txtErrorNumber.Text = dgrErrorList(hti.Row, 0)
                End If

                If txtErrorNumber.Text <> "" Then
                    SQL = "Select " & _
                    "strErrorNumber, " & _
                    "(strLastName||', '||strFirstName) as ErrorUser,  " & _
                    "strErrorLocation, strErrorMessage,  " & _
                    "to_char(datErrorDate, 'DD-Mon-YYYY') as ErrorDate,  " & _
                    "strSolution  " & _
                    "from " & connNameSpace & ".IAIPErrorLog, " & connNameSpace & ".EPDUserProfiles  " & _
                    "where " & connNameSpace & ".IAIPErrorLog.strUser = " & connNameSpace & ".EPDUserProfiles.numUserID " & _
                    "and strErrorNumber = '" & txtErrorNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                SQL = "Update " & connNameSpace & ".IAIPErrorLog set " & _
                "strSolution = '" & Replace(ErrorSolution, "'", "''") & "' " & _
                "where strErrornumber = '" & txtErrorNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                dr = cmd.ExecuteReader
                dr.Read()
                dr.Close()

                MsgBox("Solution Saved", MsgBoxStyle.Information, "Date Management Tools")
            Else
                MsgBox("Select an error", MsgBoxStyle.Exclamation, "Data Management Tools")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
         
        End Try
         
    End Sub
    Private Sub btnFilterWebErrors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterWebErrors.Click
        Try
            
            LoadWegErrorLog()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                    "from " & connNameSpace & ".LogErrors " & _
                    "where NumError = " & txtWebErrorNumber.Text & " "

                    SQL = "select numError, " & _
                    "strIPAddress, strUserEmail, " & _
                    "strErrorPage, dateTimeStamp, " & _
                    "strErrorMsg, strSolution " & _
                    "From " & connNameSpace & ".OLAPERRORLog " & _
                    "where numError = " & txtWebErrorNumber.Text & " "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                SQL = "Update " & connNameSpace & ".OLAPErrorLog set " & _
                "strSolution = '" & Replace(ErrorSolution, "'", "''") & "' " & _
                "where numError = '" & txtWebErrorNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                dr = cmd.ExecuteReader
                dr.Read()
                dr.Close()

                MsgBox("Solution Saved", MsgBoxStyle.Information, "Date Management Tools")
            Else
                MsgBox("Select an error", MsgBoxStyle.Exclamation, "Data Management Tools")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
         
    End Sub
    Private Sub mtbCDSZipCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mtbCDSZipCode.TextChanged
        Try

            mtbMailingZipCode.Text = mtbCDSZipCode.Text

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteAIRSNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAIRSNumber.Click
        Try

            If Me.txtDeleteAIRSNumber.Text <> "" And txtDeleteAIRSNumber.Text.Length = 8 Then
                SQL = "delete " & connNameSpace & ".afsairpollutantdata where strAIRSNumber = '0413" & txtDeleteAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                "values " & _
                "(" & _
                "(select " & _
                "case when max(numCounter) is null then 1 " & _
                "else max(numCounter) + 1 " & _
                "end numCounter " & _
                "from " & connNameSpace & ".AFSDeletions), " & _
                "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                "'" & OracleDate & "', '', " & _
                "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & connNameSpace & ".afsfacilitydata where strAirsnumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & connNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & connNameSpace & ".apbairprogrampollutants where strAIRSNumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & connNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & connNameSpace & ".hb_apbairprogrampollutants where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & connNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & connNameSpace & ".apbcontactinformation where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & connNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & connNameSpace & ".apbheaderdata where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & connNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & connNameSpace & ".HB_APBHeaderData where strAIRSNumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & connNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & connNameSpace & ".HB_APBFacilityInformation where strAIRSNumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & connNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & connNameSpace & ".APBFacilityInformation where strAIRSNumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & connNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & connNameSpace & ".apbsupplamentaldata where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & connNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                'SQL = "Delete " & connNameSpace & ".SSCPFacilityAssignment where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "' "

                SQL = "Delete " & connNameSpace & ".SSCPInspectionsRequired where strAIRSnumber = '0413" & txtDeleteAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & connNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & connNameSpace & ".SSCPDistrictResponsible where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & connNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & connNameSpace & ".sscpfacilityassignment where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & connNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()


                SQL = "delete " & connNameSpace & ".sscpInspectionsRequired where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & connNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()


                SQL = "delete " & connNameSpace & ".apbmasterairs where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & connNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & connNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
          
        End Try
    End Sub
#Region "Data Transfer Page"
#Region "Transfer Variables"
    Dim transferConn As OracleConnection
    Dim dsTemp As DataSet
    Dim daTemp As OracleDataAdapter
    Dim dtTemp As New DataTable
    Dim drDSRow As DataRow
    Dim temp As Object = ""
    Dim temp2 As Object = ""
    Dim temp3 As Object = ""
    Dim temp4 As Object = ""
    Dim temp5 As Object = ""
    Dim temp6 As Object = ""
    Dim temp7 As Object = ""
    Dim temp8 As Object = ""
    Dim temp9 As Object = ""
    Dim temp10 As Object = ""
    Dim temp11 As Object = ""
    Dim temp12 As Object = ""
    Dim temp13 As Object = ""
    Dim temp14 As Object = ""
    Dim temp15 As Object = ""
    Dim temp16 As Object = ""
    Dim temp17 As Object = ""
    Dim temp18 As Object = ""
    Dim temp19 As Object = ""
    Dim temp20 As Object = ""
    Dim temp21 As Object = ""
    Dim temp22 As Object = ""
    Dim temp23 As Object = ""
    Dim temp24 As Object = ""
    Dim temp25 As Object = ""
    Dim temp26 As Object = ""
    Dim temp27 As Object = ""
    Dim temp28 As Object = ""
    Dim temp29 As Object = ""
    Dim temp30 As Object = ""
    Dim temp31 As Object = ""
    Dim temp32 As Object = ""
    Dim temp33 As Object = ""
    Dim temp34 As Object = ""
    Dim temp35 As Object = ""
    Dim temp36 As Object = ""
    Dim temp37 As Object = ""
    Dim temp38 As Object = ""
    Dim temp39 As Object = ""
    Dim temp40 As Object = ""
    Dim temp41 As Object = ""
    Dim temp42 As Object = ""
    Dim temp43 As Object = ""
    Dim temp44 As Object = ""
    Dim temp45 As Object = ""
    Dim temp46 As Object = ""
    Dim temp47 As Object = ""
    Dim temp48 As Object = ""
    Dim temp49 As Object = ""
    Dim temp50 As Object = ""
    Dim temp51 As Object = ""
    Dim temp52 As Object = ""
    Dim temp53 As Object = ""
    Dim temp54 As Object = ""
    Dim temp55 As Object = ""
    Dim temp56 As Object = ""
    Dim temp57 As Object = ""
    Dim temp58 As Object = ""
    Dim temp59 As Object = ""
    Dim temp60 As Object = ""
    Dim temp61 As Object = ""
    Dim temp62 As Object = ""
    Dim temp63 As Object = ""
    Dim temp64 As Object = ""
    Dim temp65 As Object = ""
    Dim temp66 As Object = ""
    Dim temp67 As Object = ""

#End Region
    Private Sub chbAllTables_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbAllTables.CheckedChanged
        Try
            If chbAllTables.Checked = True Then
                pnlLookUpTables.Enabled = False
                pnlHeaderTables.Enabled = False
                pnlISMPTables.Enabled = False
                pnlSBEAPTables.Enabled = False
                pnlSSCPTables.Enabled = False
                pnlSSPPTables.Enabled = False
                pnlAFSTables.Enabled = False
                pnlMiscTables.Enabled = False

                chbAllLookUpTables.Checked = False
                chbAllHeaderTables.Checked = False
                chbAllISMPTables.Checked = False
                chbAllSBEAPTables.Checked = False
                chbAllSSCPTables.Checked = False
                chbAllSSPPTables.Checked = False
                chbAllAFSTables.Checked = False

                ClearLookUpTables()
                ClearAPBTables()
                ClearISMPTables()
                ClearSBEAPTables()
                ClearSSCPTables()
                ClearSSPPTables()
                ClearAFSTables()
            Else
                pnlLookUpTables.Enabled = True
                pnlHeaderTables.Enabled = True
                pnlISMPTables.Enabled = True
                pnlSBEAPTables.Enabled = True
                pnlSSCPTables.Enabled = True
                pnlSSPPTables.Enabled = True
                pnlAFSTables.Enabled = True
                pnlMiscTables.Enabled = True
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbAllLookUpTables_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbAllLookUpTables.CheckedChanged
        Try
            If chbAllLookUpTables.Checked = True Then
                chbLookUpAPBManagement.Enabled = False
                chbLookUpApplicationType.Enabled = False
                chbLookUpComplianceActivities.Enabled = False
                chbLookUpComplianceStatus.Enabled = False
                chbLookUpComplianceUnits.Enabled = False
                chbLookUpCountyInformation.Enabled = False
                chbLookUpDistrictInformation.Enabled = False
                chbLookUpDistrictOffice.Enabled = False
                chbLookUpDistricts.Enabled = False
                chbLookUpEPDBranches.Enabled = False
                chbLookUpEPDPrograms.Enabled = False
                chbLookUpEPDUnits.Enabled = False
                chbLookUpHPVViolations.Enabled = False
                chbLookUpIAIPAccounts.Enabled = False
                chbLookUpIAIPForms.Enabled = False
                chbLookUpISMPComplianceStatus.Enabled = False
                chbLookUpISMPMethods.Enabled = False
                chbLookUpMonitoringUnits.Enabled = False
                chbLookUpNonAttainment.Enabled = False
                chbLookUpPermittingUnits.Enabled = False
                chbLookUpPermitTypes.Enabled = False
                chbLookUpPollutants.Enabled = False
                chbLookUpSBEAPCaseWork.Enabled = False
                chbLookUpSICCodes.Enabled = False
                chbLookUpSSCPNotifications.Enabled = False
                chbLookUpStates.Enabled = False
                chbLookUpSubPart60.Enabled = False
                chbLookUpSubPart61.Enabled = False
                chbLookUpSubPart63.Enabled = False
                chbLookUpSubPartSIP.Enabled = False
                chbLookUpTestingFirms.Enabled = False
                chbLookUpUnits.Enabled = False
                ClearLookUpTables()
            Else
                chbLookUpAPBManagement.Enabled = True
                chbLookUpApplicationType.Enabled = True
                chbLookUpComplianceActivities.Enabled = True
                chbLookUpComplianceStatus.Enabled = True
                chbLookUpComplianceUnits.Enabled = True
                chbLookUpCountyInformation.Enabled = True
                chbLookUpDistrictInformation.Enabled = True
                chbLookUpDistrictOffice.Enabled = True
                chbLookUpDistricts.Enabled = True
                chbLookUpEPDBranches.Enabled = True
                chbLookUpEPDPrograms.Enabled = True
                chbLookUpEPDUnits.Enabled = True
                chbLookUpHPVViolations.Enabled = True
                chbLookUpIAIPAccounts.Enabled = True
                chbLookUpIAIPForms.Enabled = True
                chbLookUpISMPComplianceStatus.Enabled = True
                chbLookUpISMPMethods.Enabled = True
                chbLookUpMonitoringUnits.Enabled = True
                chbLookUpNonAttainment.Enabled = True
                chbLookUpPermittingUnits.Enabled = True
                chbLookUpPermitTypes.Enabled = True
                chbLookUpPollutants.Enabled = True
                chbLookUpSBEAPCaseWork.Enabled = True
                chbLookUpSICCodes.Enabled = True
                chbLookUpSSCPNotifications.Enabled = True
                chbLookUpStates.Enabled = True
                chbLookUpSubPart60.Enabled = True
                chbLookUpSubPart61.Enabled = True
                chbLookUpSubPart63.Enabled = True
                chbLookUpSubPartSIP.Enabled = True
                chbLookUpTestingFirms.Enabled = True
                chbLookUpUnits.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub ClearLookUpTables()
        Try
            chbLookUpAPBManagement.Checked = False
            chbLookUpApplicationType.Checked = False
            chbLookUpComplianceActivities.Checked = False
            chbLookUpComplianceStatus.Checked = False
            chbLookUpComplianceUnits.Checked = False
            chbLookUpCountyInformation.Checked = False
            chbLookUpDistrictInformation.Checked = False
            chbLookUpDistrictOffice.Checked = False
            chbLookUpDistricts.Checked = False
            chbLookUpEPDBranches.Checked = False
            chbLookUpEPDPrograms.Checked = False
            chbLookUpEPDUnits.Checked = False
            chbLookUpHPVViolations.Checked = False
            chbLookUpIAIPAccounts.Checked = False
            chbLookUpIAIPForms.Checked = False
            chbLookUpISMPComplianceStatus.Checked = False
            chbLookUpISMPMethods.Checked = False
            chbLookUpMonitoringUnits.Checked = False
            chbLookUpNonAttainment.Checked = False
            chbLookUpPermittingUnits.Checked = False
            chbLookUpPermitTypes.Checked = False
            chbLookUpPollutants.Checked = False
            chbLookUpSBEAPCaseWork.Checked = False
            chbLookUpSICCodes.Checked = False
            chbLookUpSSCPNotifications.Checked = False
            chbLookUpStates.Checked = False
            chbLookUpSubPart60.Checked = False
            chbLookUpSubPart61.Checked = False
            chbLookUpSubPart63.Checked = False
            chbLookUpSubPartSIP.Checked = False
            chbLookUpTestingFirms.Checked = False
            chbLookUpUnits.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbAllHeaderTables_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAllHeaderTables.CheckedChanged
        Try
            If chbAllHeaderTables.Checked = True Then
                chbAPBAirProgramPollutants.Enabled = False
                chbAPBContactInformation.Enabled = False
                chbAPBFacilityInformation.Enabled = False
                chbAPBHeaderData.Enabled = False
                chbAPBMasterAIRS.Enabled = False
                chbAPBMasterAPP.Enabled = False
                chbAPBPermits.Enabled = False
                chbAPBSubPartData.Enabled = False
                chbAPBSupplamentalData.Enabled = False
                chbAPPMaster.Enabled = False
                chbHBAPBAirProgramPollutants.Enabled = False
                chbHBAPBFacilityInformation.Enabled = False
                chbHBAPBHeaderData.Enabled = False
                ClearAPBTables()
            Else
                chbAPBAirProgramPollutants.Enabled = True
                chbAPBContactInformation.Enabled = True
                chbAPBFacilityInformation.Enabled = True
                chbAPBHeaderData.Enabled = True
                chbAPBMasterAIRS.Enabled = True
                chbAPBMasterAPP.Enabled = True
                chbAPBPermits.Enabled = True
                chbAPBSubPartData.Enabled = True
                chbAPBSupplamentalData.Enabled = True
                chbAPPMaster.Enabled = True
                chbHBAPBAirProgramPollutants.Enabled = True
                chbHBAPBFacilityInformation.Enabled = True
                chbHBAPBHeaderData.Enabled = True
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearAPBTables()
        Try
            chbAPBAirProgramPollutants.Checked = False
            chbAPBContactInformation.Checked = False
            chbAPBFacilityInformation.Checked = False
            chbAPBHeaderData.Checked = False
            chbAPBMasterAIRS.Checked = False
            chbAPBMasterAPP.Checked = False
            chbAPBPermits.Checked = False
            chbAPBSubPartData.Checked = False
            chbAPBSupplamentalData.Checked = False
            chbAPPMaster.Checked = False
            chbHBAPBAirProgramPollutants.Checked = False
            chbHBAPBFacilityInformation.Checked = False
            chbHBAPBHeaderData.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbAllISMPTables_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAllISMPTables.CheckedChanged
        Try
            If chbAllISMPTables.Checked = True Then
                chbISMPDocumentTypes.Enabled = False
                chbISMPFacilityAssignment.Enabled = False
                chbISMPMaster.Enabled = False
                chbISMPReferenceNumber.Enabled = False
                chbISMPReportFlare.Enabled = False
                chbISMPReportInformation.Enabled = False
                chbISMPReportMemo.Enabled = False
                chbISMPReportOneStack.Enabled = False
                chbISMPReportOpacity.Enabled = False
                chbISMPReportPondAndGas.Enabled = False
                chbISMPReportRATA.Enabled = False
                chbISMPReportTwoStack.Enabled = False
                chbISMPReportType.Enabled = False
                chbISMPTestFirmComments.Enabled = False
                chbISMPTestLogLink.Enabled = False
                chbISMPTestLogNumber.Enabled = False
                chbISMPTestNotification.Enabled = False
                chbISMPTestNotificationLog.Enabled = False
                chbISMPTestReportAids.Enabled = False
                chbISMPReportMemo.Enabled = False
                chbISMPTestReportMemo.Enabled = False
                chbISMPWitnessingEng.Enabled = False
                ClearISMPTables()
            Else
                chbISMPDocumentTypes.Enabled = True
                chbISMPFacilityAssignment.Enabled = True
                chbISMPMaster.Enabled = True
                chbISMPReferenceNumber.Enabled = True
                chbISMPReportFlare.Enabled = True
                chbISMPReportInformation.Enabled = True
                chbISMPReportMemo.Enabled = True
                chbISMPReportOneStack.Enabled = True
                chbISMPReportOpacity.Enabled = True
                chbISMPReportPondAndGas.Enabled = True
                chbISMPReportRATA.Enabled = True
                chbISMPReportTwoStack.Enabled = True
                chbISMPReportType.Enabled = True
                chbISMPTestFirmComments.Enabled = True
                chbISMPTestLogLink.Enabled = True
                chbISMPTestLogNumber.Enabled = True
                chbISMPTestNotification.Enabled = True
                chbISMPTestNotificationLog.Enabled = True
                chbISMPTestReportAids.Enabled = True
                chbISMPReportMemo.Enabled = True
                chbISMPTestReportMemo.Enabled = True
                chbISMPWitnessingEng.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearISMPTables()
        Try
            chbISMPDocumentTypes.Checked = False
            chbISMPFacilityAssignment.Checked = False
            chbISMPMaster.Checked = False
            chbISMPReferenceNumber.Checked = False
            chbISMPReportFlare.Checked = False
            chbISMPReportInformation.Checked = False
            chbISMPReportMemo.Checked = False
            chbISMPReportOneStack.Checked = False
            chbISMPReportOpacity.Checked = False
            chbISMPReportPondAndGas.Checked = False
            chbISMPReportRATA.Checked = False
            chbISMPReportTwoStack.Checked = False
            chbISMPReportType.Checked = False
            chbISMPTestFirmComments.Checked = False
            chbISMPTestLogLink.Checked = False
            chbISMPTestLogNumber.Checked = False
            chbISMPTestNotification.Checked = False
            chbISMPTestNotificationLog.Checked = False
            chbISMPTestReportAids.Checked = False
            chbISMPReportMemo.Checked = False
            chbISMPTestReportMemo.Checked = False
            chbISMPWitnessingEng.Checked = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbAllSBEAPTables_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAllSBEAPTables.CheckedChanged
        Try
            If chbAllSBEAPTables.Checked = True Then
                chbSBEAPCaseLog.Enabled = False
                chbSBEAPClientContacts.Enabled = False
                chbSBEAPClientData.Enabled = False
                chbSBEAPClientLink.Enabled = False
                chbSBEAPClients.Enabled = False
                chbSBEAPErrorLog.Enabled = False
                chbHBSBEAPClientData.Enabled = False
                chbHBSBEAPClients.Enabled = False
                ClearSBEAPTables()
            Else
                chbSBEAPCaseLog.Enabled = True
                chbSBEAPClientContacts.Enabled = True
                chbSBEAPClientData.Enabled = True
                chbSBEAPClientLink.Enabled = True
                chbSBEAPClients.Enabled = True
                chbSBEAPErrorLog.Enabled = True
                chbHBSBEAPClientData.Enabled = True
                chbHBSBEAPClients.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearSBEAPTables()
        Try
            chbSBEAPCaseLog.Checked = False
            chbSBEAPClientContacts.Checked = False
            chbSBEAPClientData.Checked = False
            chbSBEAPClientLink.Checked = False
            chbSBEAPClients.Checked = False
            chbSBEAPErrorLog.Checked = False
            chbHBSBEAPClientData.Checked = False
            chbHBSBEAPClients.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbAllSSCPTables_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAllSSCPTables.CheckedChanged
        Try
            If chbAllSSCPTables.Checked = True Then
                chbSSCPACCS.Enabled = False
                chbSSCPACCSHistory.Enabled = False
                chbSSCPDistrictAssignment.Enabled = False
                chbSSCPDistrictResponsible.Enabled = False
                chbSSCPEnforcement.Enabled = False
                chbSSCPEnforcementAOComments.Enabled = False
                chbSSCPEnforcementCOComments.Enabled = False
                chbSSCPEnforcementItems.Enabled = False
                chbSSCPEnforcementLetter.Enabled = False
                chbSSCPEnforcementNOVComments.Enabled = False
                chbSSCPEnforcementStipulated.Enabled = False
                chbSSCPFacilityAssignment.Enabled = False
                chbSSCPFCE.Enabled = False
                chbSSCPFCEMaster.Enabled = False
                chbSSCPInspectionActivity.Enabled = False
                chbSSCPInspections.Enabled = False
                chbSSCPInspectionsRequired.Enabled = False
                chbSSCPInspectionTracking.Enabled = False
                chbSSCPItemMaster.Enabled = False
                chbSSCPNotifications.Enabled = False
                chbSSCPReports.Enabled = False
                chbSSCPReportsHistory.Enabled = False
                chbSSCPTestReports.Enabled = False
                ClearSSCPTables()
            Else
                chbSSCPACCS.Enabled = True
                chbSSCPACCSHistory.Enabled = True
                chbSSCPDistrictAssignment.Enabled = True
                chbSSCPDistrictResponsible.Enabled = True
                chbSSCPEnforcement.Enabled = True
                chbSSCPEnforcementAOComments.Enabled = True
                chbSSCPEnforcementCOComments.Enabled = True
                chbSSCPEnforcementItems.Enabled = True
                chbSSCPEnforcementLetter.Enabled = True
                chbSSCPEnforcementNOVComments.Enabled = True
                chbSSCPEnforcementStipulated.Enabled = True
                chbSSCPFacilityAssignment.Enabled = True
                chbSSCPFCE.Enabled = True
                chbSSCPFCEMaster.Enabled = True
                chbSSCPInspectionActivity.Enabled = True
                chbSSCPInspections.Enabled = True
                chbSSCPInspectionsRequired.Enabled = True
                chbSSCPInspectionTracking.Enabled = True
                chbSSCPItemMaster.Enabled = True
                chbSSCPNotifications.Enabled = True
                chbSSCPReports.Enabled = True
                chbSSCPReportsHistory.Enabled = True
                chbSSCPTestReports.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearSSCPTables()
        Try
            chbSSCPACCS.Checked = False
            chbSSCPACCSHistory.Checked = False
            chbSSCPDistrictAssignment.Checked = False
            chbSSCPDistrictResponsible.Checked = False
            chbSSCPEnforcement.Checked = False
            chbSSCPEnforcementAOComments.Checked = False
            chbSSCPEnforcementCOComments.Checked = False
            chbSSCPEnforcementItems.Checked = False
            chbSSCPEnforcementLetter.Checked = False
            chbSSCPEnforcementNOVComments.Checked = False
            chbSSCPEnforcementStipulated.Checked = False
            chbSSCPFacilityAssignment.Checked = False
            chbSSCPFCE.Checked = False
            chbSSCPFCEMaster.Checked = False
            chbSSCPInspectionActivity.Checked = False
            chbSSCPInspections.Checked = False
            chbSSCPInspectionsRequired.Checked = False
            chbSSCPInspectionTracking.Checked = False
            chbSSCPItemMaster.Checked = False
            chbSSCPNotifications.Checked = False
            chbSSCPReports.Checked = False
            chbSSCPReportsHistory.Checked = False
            chbSSCPTestReports.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbAllSSPPTables_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAllSSPPTables.CheckedChanged
        Try
            If chbAllSSPPTables.Checked = True Then
                chbSSPPApplicationContact.Enabled = False
                chbSSPPApplicationData.Enabled = False
                chbSSPPApplicationInformation.Enabled = False
                chbSSPPApplicationLinking.Enabled = False
                chbSSPPApplicationMaster.Enabled = False
                chbSSPPApplicationQuality.Enabled = False
                chbSSPPApplicationTracking.Enabled = False
                chbSSPPCDS.Enabled = False
                chbSSPPPublicLetters.Enabled = False
                ClearSSPPTables()
            Else
                chbSSPPApplicationContact.Enabled = True
                chbSSPPApplicationData.Enabled = True
                chbSSPPApplicationInformation.Enabled = True
                chbSSPPApplicationLinking.Enabled = True
                chbSSPPApplicationMaster.Enabled = True
                chbSSPPApplicationQuality.Enabled = True
                chbSSPPApplicationTracking.Enabled = True
                chbSSPPCDS.Enabled = True
                chbSSPPPublicLetters.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearSSPPTables()
        Try
            chbSSPPApplicationContact.Checked = False
            chbSSPPApplicationData.Checked = False
            chbSSPPApplicationInformation.Checked = False
            chbSSPPApplicationLinking.Checked = False
            chbSSPPApplicationMaster.Checked = False
            chbSSPPApplicationQuality.Checked = False
            chbSSPPApplicationTracking.Checked = False
            chbSSPPCDS.Checked = False
            chbSSPPPublicLetters.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbAllAFSTables_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAllAFSTables.CheckedChanged
        Try
            If chbAllAFSTables.Checked = True Then
                chbAFSAirPollutantData.Enabled = False
                chbAFSBatchFiles.Enabled = False
                chbAFSFacilityData.Enabled = False
                chbAFSISMPRecords.Enabled = False
                chbAFSSSCPEnforcementRecords.Enabled = False
                chbAFSSSCPFCERecords.Enabled = False
                chbAFSSSCPRecords.Enabled = False
                chbAFSSSPPRecords.Enabled = False
                ClearAFSTables()
            Else
                chbAFSAirPollutantData.Enabled = True
                chbAFSBatchFiles.Enabled = True
                chbAFSFacilityData.Enabled = True
                chbAFSISMPRecords.Enabled = True
                chbAFSSSCPEnforcementRecords.Enabled = True
                chbAFSSSCPFCERecords.Enabled = True
                chbAFSSSCPRecords.Enabled = True
                chbAFSSSPPRecords.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearAFSTables()
        Try
            chbAFSAirPollutantData.Checked = False
            chbAFSBatchFiles.Checked = False
            chbAFSFacilityData.Checked = False
            chbAFSISMPRecords.Checked = False
            chbAFSSSCPEnforcementRecords.Checked = False
            chbAFSSSCPFCERecords.Checked = False
            chbAFSSSCPRecords.Checked = False
            chbAFSSSPPRecords.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearSelection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSelection.Click
        Try
            chbAllTables.Checked = False
            chbAllLookUpTables.Checked = False
            chbAllHeaderTables.Checked = False
            chbAllISMPTables.Checked = False
            chbAllSBEAPTables.Checked = False
            chbAllSSCPTables.Checked = False
            chbAllSSPPTables.Checked = False
            chbAllAFSTables.Checked = False

            ClearLookUpTables()
            ClearAPBTables()
            ClearISMPTables()
            ClearSBEAPTables()
            ClearSSCPTables()
            ClearSSPPTables()
            ClearAFSTables()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnTransferData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransferData.Click
        Try
            btnTransferData.Enabled = False
            btnClearSelection.Enabled = False
            lblTransfer.Text = "Data Transfer in Progress."

            If rdbDEVTransfer.Checked = True Then
                If Oracledll = "2.111.6.20" Then
                    transferConn = New OracleConnection("Data Source = leia.dnr.state.ga.us:1521/DEV; User ID = AirBranch; " & _
                             "Password = " & SimpleCrypt("óíïçáìåòô") & ";")
                Else
                    transferConn = New OracleConnection("data Source = DEV; " & _
                                       "User ID = AIRBranch; Password = smogalert;")
                End If
            Else
                If Oracledll = "2.111.6.20" Then
                    transferConn = New OracleConnection("Data Source = leia.dnr.state.ga.us:1521/TEST; User ID = AIRBRANCH_APP_USER; " & _
                            "Password = " & SimpleCrypt("ÁÉÒÁÐÐÕÓÅÒ°³") & ";")
                Else
                    transferConn = New OracleConnection("Data Source = TEST; " & _
                                       "User ID = AIRBRANCH_APP_USER; Password = AIRAPPUSER03;")
                End If
            End If

            bgwTransfer.WorkerReportsProgress = True
            bgwTransfer.WorkerSupportsCancellation = True
            bgwTransfer.RunWorkerAsync()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwTransfer_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwTransfer.DoWork
        If chbAllLookUpTables.Checked = True Then
            TransferLookUpAPBManagement()
            TransferLookUpApplicationType()
            TransferLookUpComplianceActivities()
            TransferLookUpComplianceStatus()
            TransferLookUpComplinaceUnits()
            TransferLookUpCountyInformation()
            TransferLookUpDistrictInformation()
            TransferLookUpDistrictOffice()
            TransferLookUpDistricts()
            TransferLookUpEPDBranches()
            TransferLookUpEPDPrograms()
            TransferLookUpEPDUnits()
            TransferLookUpHPVViolations()
            TransferLookUpIAIPAccounts()
            TransferLookUpIAIPForms()
            TransferLookUPISMPComplianceStatus()
            TransferLookUpISMPMethods()
            TransferLookUpMonitoringUnits()
            TransferLookUpNonAttainment()
            TransferLookUpPermittingUnits()
            TransferLookUpPermitType()
            TransferLookUpPollutants()
            TransferLookUpSBEAPCaseWork()
            TransferLookUpSICCodes()
            TransferLookUpSSCPNotificaitons()
            TransferLookUpStates()
            TransferLookUpSubPart60()
            TransferLookUpSubPart61()
            TransferLookUpSubPart63()
            TransferLookUpSubPartSIP()
            TransferLookUpTestingFirms()
            TransferLookUpUnits()
            TransferEDPUsers()
            TransferNSPSReason()
            TransferEISI()
            TransferEIEU()
            TransferEIER()
            TransferEIEP()
            TransferEIEM()
        Else
            If chbLookUpAPBManagement.Checked = True Then
                TransferLookUpAPBManagement()
            End If
            If chbLookUpApplicationType.Checked = True Then
                TransferLookUpApplicationType()
            End If
            If chbLookUpComplianceActivities.Checked = True Then
                TransferLookUpComplianceActivities()
            End If
            If chbLookUpComplianceStatus.Checked = True Then
                TransferLookUpComplianceStatus()
            End If
            If chbLookUpComplianceUnits.Checked = True Then
                TransferLookUpComplinaceUnits()
            End If
            If chbLookUpCountyInformation.Checked = True Then
                TransferLookUpCountyInformation()
            End If
            If chbLookUpDistrictInformation.Checked = True Then
                TransferLookUpDistrictInformation()
            End If
            If chbLookUpDistrictOffice.Checked = True Then
                TransferLookUpDistrictOffice()
            End If
            If chbLookUpDistricts.Checked = True Then
                TransferLookUpDistricts()
            End If
            If chbLookUpEPDBranches.Checked = True Then
                TransferLookUpEPDBranches()
            End If
            If chbLookUpEPDPrograms.Checked = True Then
                TransferLookUpEPDPrograms()
            End If
            If chbLookUpEPDUnits.Checked = True Then
                TransferLookUpEPDUnits()
            End If
            If chbLookUpHPVViolations.Checked = True Then
                TransferLookUpHPVViolations()
            End If
            If chbLookUpIAIPAccounts.Checked = True Then
                TransferLookUpIAIPAccounts()
            End If
            If chbLookUpIAIPForms.Checked = True Then
                TransferLookUpIAIPForms()
            End If
            If chbLookUpISMPComplianceStatus.Checked = True Then
                TransferLookUPISMPComplianceStatus()
            End If
            If chbLookUpISMPMethods.Checked = True Then
                TransferLookUpISMPMethods()
            End If
            If chbLookUpMonitoringUnits.Checked = True Then
                TransferLookUpMonitoringUnits()
            End If
            If chbLookUpNonAttainment.Checked = True Then
                TransferLookUpNonAttainment()
            End If
            If chbLookUpPermittingUnits.Checked = True Then
                TransferLookUpPermittingUnits()
            End If
            If chbLookUpPermitTypes.Checked = True Then
                TransferLookUpPermitType()
            End If
            If chbLookUpPollutants.Checked = True Then
                TransferLookUpPollutants()
            End If
            If chbLookUpSBEAPCaseWork.Checked = True Then
                TransferLookUpSBEAPCaseWork()
            End If
            If chbLookUpSICCodes.Checked = True Then
                TransferLookUpSICCodes()
            End If
            If chbLookUpSSCPNotifications.Checked = True Then
                TransferLookUpSSCPNotificaitons()
            End If
            If chbLookUpStates.Checked = True Then
                TransferLookUpStates()
            End If
            If chbLookUpSubPart60.Checked = True Then
                TransferLookUpSubPart60()
            End If
            If chbLookUpSubPart61.Checked = True Then
                TransferLookUpSubPart61()
            End If
            If chbLookUpSubPart63.Checked = True Then
                TransferLookUpSubPart63()
            End If
            If chbLookUpSubPartSIP.Checked = True Then
                TransferLookUpSubPartSIP()
            End If
            If chbLookUpTestingFirms.Checked = True Then
                TransferLookUpTestingFirms()
            End If
            If chbLookUpUnits.Checked = True Then
                TransferLookUpUnits()
            End If
            If chbEPDUsers.Checked = True Then
                TransferEDPUsers()
            End If
            If chbUpDateEISI.Checked = True Then
                TransferEISI()
            End If
            If chbUpdateEIEU.Checked = True Then
                TransferEIEU()
            End If
            If chbUpdateEIER.Checked = True Then
                TransferEIER()
            End If
            If chbUpdateEIEP.Checked = True Then
                TransferEIEP()
            End If
            If chbUpdateEIEM.Checked = True Then
                TransferEIEM()
            End If
            If chbFSNSPSReason.Checked = True Then
                TransferNSPSReason()
            End If
        End If
        If chbAllHeaderTables.Checked = True Then
            TransferAPBAirProgramPollutants()
            TransferAPBContactInformation()
            TransferAPBFacilityInformation()
            TransferAPBHeaderData()
            TransferAPBMasterAIRS()
            TransferAPBMasterAPP()
            TransferAPBPermits()
            TransferAPBSubPartData()
            TransferAPBSupplamentalData()

            TransferAPPMaster()
            TransferHBAPBAirProgramPollutants()
            TransferHBAPBFacilityINformation()
            TransferHBAPBHeaderData()
        Else
            If chbAPBAirProgramPollutants.Checked = True Then
                TransferAPBAirProgramPollutants()
            End If
            If chbAPBContactInformation.Checked = True Then
                TransferAPBContactInformation()
            End If
            If chbAPBFacilityInformation.Checked = True Then
                TransferAPBFacilityInformation()
            End If
            If chbAPBHeaderData.Checked = True Then
                TransferAPBHeaderData()
            End If
            If chbAPBMasterAIRS.Checked = True Then
                TransferAPBMasterAIRS()
            End If
            If chbAPBMasterAPP.Checked = True Then
                TransferAPBMasterAPP()
            End If
            If chbAPBPermits.Checked = True Then
                TransferAPBPermits()
            End If
            If chbAPBSubPartData.Checked = True Then
                TransferAPBSubPartData()
            End If
            If chbAPBSupplamentalData.Checked = True Then
                TransferAPBSupplamentalData()
            End If
            If chbAPPMaster.Checked = True Then
                TransferAPPMaster()
            End If
            If chbHBAPBAirProgramPollutants.Checked = True Then
                TransferHBAPBAirProgramPollutants()
            End If
            If chbHBAPBFacilityInformation.Checked = True Then
                TransferHBAPBFacilityINformation()
            End If
            If chbHBAPBHeaderData.Checked = True Then
                TransferHBAPBHeaderData()
            End If
        End If
        If chbAllISMPTables.Checked = True Then
            TransferISMPDocumentTypes()
            TransferISMPFacilityAssignment()
            TransferISMPMaster()
            TransferISMPReferenceNumber()
            TransferISMPReportFlare()
            TransferISMPReportInformation()
            TransferISMPREportMemo()
            TransferISMPReportOneStack()
            TransferISMPREportOpacity()
            TransferISMPREportPondAndGas()
            TransferISMPReportRATA()
            TransferISMPREportTwoStack()
            TransferISMPReportType()
            TransferISMPTestFirmComments()
            TransferISMPTestLogLink()
            TransferISMPTestLogNumber()
            TransferISMPTestNotification()
            TransferISMPTestNotificationLog()
            TransferISMPTestREportAids()
            TransferISMPTestREportMemo()
            TransferISMPWitnessingEng()
        Else
            If chbISMPDocumentTypes.Checked = True Then
                TransferISMPDocumentTypes()
            End If
            If chbISMPFacilityAssignment.Checked = True Then
                TransferISMPFacilityAssignment()
            End If
            If chbISMPMaster.Checked = True Then
                TransferISMPMaster()
            End If
            If chbISMPReferenceNumber.Checked = True Then
                TransferISMPReferenceNumber()
            End If
            If chbISMPReportFlare.Checked = True Then
                TransferISMPReportFlare()
            End If
            If chbISMPReportInformation.Checked = True Then
                TransferISMPReportInformation()
            End If
            If chbISMPReportMemo.Checked = True Then
                TransferISMPREportMemo()
            End If
            If chbISMPReportOneStack.Checked = True Then
                TransferISMPReportOneStack()
            End If
            If chbISMPReportOpacity.Checked = True Then
                TransferISMPREportOpacity()
            End If
            If chbISMPReportPondAndGas.Checked = True Then
                TransferISMPREportPondAndGas()
            End If
            If chbISMPReportRATA.Checked = True Then
                TransferISMPReportRATA()
            End If
            If chbISMPReportTwoStack.Checked = True Then
                TransferISMPREportTwoStack()
            End If
            If chbISMPReportType.Checked = True Then
                TransferISMPReportType()
            End If
            If chbISMPTestFirmComments.Checked = True Then
                TransferISMPTestFirmComments()
            End If
            If chbISMPTestLogLink.Checked = True Then
                TransferISMPTestLogLink()
            End If
            If chbISMPTestLogNumber.Checked = True Then
                TransferISMPTestLogNumber()
            End If
            If chbISMPTestNotification.Checked = True Then
                TransferISMPTestNotification()
            End If
            If chbISMPTestNotificationLog.Checked = True Then
                TransferISMPTestNotificationLog()
            End If
            If chbISMPTestReportAids.Checked = True Then
                TransferISMPTestREportAids()
            End If
            If chbISMPTestReportMemo.Checked = True Then
                TransferISMPTestREportMemo()
            End If
            If chbISMPWitnessingEng.Checked = True Then
                TransferISMPWitnessingEng()
            End If
        End If
        If chbAllSSCPTables.Checked = True Then
            TransferSSCPACCS()
            TransferSSCPACCSHistory()
            TransferSSCPDistrictAssignment()
            TransferSSCPDistrictResponsible()
            TransferSSCPEnforcement()
            TransferSSCPEnforcementAOComments()
            TransferSSCPEnforcementCOComments()
            TransferSSCPEnforcementItems()
            TransferSSCPEnforcementLetter()
            TransferSSCPEnforceNOVComments()
            TransferSSCPEnforcementStipulated()
            '  TransferSSCPFacilityAssignment()
            TransferSSCPFCE()
            TransferSSCPFCEMaster()
            TransferSSCPInspectionActivity()
            TransferSSCPInspections()
            TransferSSCPInspectionsRequired()
            TransferSSCPInspectionTracking()
            TransferSSCPItemMaster()
            TransferSSCPNotifications()
            TransferSSCPReports()
            TransferSSCPReportsHistory()
            TransferSSCPTestReports()
        Else
            If chbSSCPACCS.Checked = True Then
                TransferSSCPACCS()
            End If
            If chbSSCPACCSHistory.Checked = True Then
                TransferSSCPACCSHistory()
            End If
            If chbSSCPDistrictAssignment.Checked = True Then
                TransferSSCPDistrictAssignment()
            End If
            If chbSSCPDistrictResponsible.Checked = True Then
                TransferSSCPDistrictResponsible()
            End If
            If chbSSCPEnforcement.Checked = True Then
                TransferSSCPEnforcement()
            End If
            If chbSSCPEnforcementAOComments.Checked = True Then
                TransferSSCPEnforcementAOComments()
            End If
            If chbSSCPEnforcementCOComments.Checked = True Then
                TransferSSCPEnforcementCOComments()
            End If
            If chbSSCPEnforcementItems.Checked = True Then
                TransferSSCPEnforcementItems()
            End If
            If chbSSCPEnforcementLetter.Checked = True Then
                TransferSSCPEnforcementLetter()
            End If
            If chbSSCPEnforcementNOVComments.Checked = True Then
                TransferSSCPEnforceNOVComments()
            End If
            If chbSSCPEnforcementStipulated.Checked = True Then
                TransferSSCPEnforcementStipulated()
            End If
            If chbSSCPFacilityAssignment.Checked = True Then
                'TransferSSCPFacilityAssignment()
            End If
            If chbSSCPFCE.Checked = True Then
                TransferSSCPFCE()
            End If
            If chbSSCPFCEMaster.Checked = True Then
                TransferSSCPFCEMaster()
            End If
            If chbSSCPInspectionActivity.Checked = True Then
                TransferSSCPInspectionActivity()
            End If
            If chbSSCPInspections.Checked = True Then
                TransferSSCPInspections()
            End If
            If chbSSCPInspectionsRequired.Checked = True Then
                TransferSSCPInspectionsRequired()
            End If
            If chbSSCPInspectionTracking.Checked = True Then
                TransferSSCPInspectionTracking()
            End If
            If chbSSCPNotifications.Checked = True Then
                TransferSSCPNotifications()
            End If
            If chbSSCPReports.Checked = True Then
                TransferSSCPReports()
            End If
            If chbSSCPReportsHistory.Checked = True Then
                TransferSSCPReportsHistory()
            End If
            If chbSSCPTestReports.Checked = True Then
                TransferSSCPTestReports()
            End If
        End If
        If chbAllSSPPTables.Checked = True Then
            TransferSSPPApplicationContact()
            TransferSSPPApplicationData()
            TransferSSPPApplicationInformation()
            TransferSSPPApplicationLinking()
            TransferSSPPApplicationMaster()
            TransferSSPPApplicationQuality()
            TransferSSPPApplicationTracking()
            TransferSSPPCDS()
            TransferSSPPPublicLetters()
        Else
            If chbSSPPApplicationContact.Checked = True Then
                TransferSSPPApplicationContact()
            End If
            If chbSSPPApplicationData.Checked = True Then
                TransferSSPPApplicationData()
            End If
            If chbSSPPApplicationInformation.Checked = True Then
                TransferSSPPApplicationInformation()
            End If
            If chbSSPPApplicationLinking.Checked = True Then
                TransferSSPPApplicationLinking()
            End If
            If chbSSPPApplicationMaster.Checked = True Then
                TransferSSPPApplicationMaster()
            End If
            If chbSSPPApplicationQuality.Checked = True Then
                TransferSSPPApplicationQuality()
            End If
            If chbSSPPApplicationTracking.Checked = True Then
                TransferSSPPApplicationTracking()
            End If
            If chbSSPPCDS.Checked = True Then
                TransferSSPPCDS()
            End If
            If chbSSPPPublicLetters.Checked = True Then
                TransferSSPPPublicLetters()
            End If
        End If

        If chbAllAFSTables.Checked = True Then
            TransferAFSAirPollutantDate()
            TransferAFSBatchFile()
            TransferAFSFacilityData()
            TransferAFSISMPRecords()
            TransferAFSSSCPEnforcementRecords()
            TransferAFSSSCPFCERecords()
            TransferAFSSSCPRecords()
            TransferAFSSSPPRecords()
        Else
            If chbAFSAirPollutantData.Checked = True Then
                TransferAFSAirPollutantDate()
            End If
            If chbAFSBatchFiles.Checked = True Then
                TransferAFSBatchFile()
            End If
            If chbAFSFacilityData.Checked = True Then
                TransferAFSFacilityData()
            End If
            If chbAFSISMPRecords.Checked = True Then
                TransferAFSISMPRecords()
            End If
            If chbAFSSSCPEnforcementRecords.Checked = True Then
                TransferAFSSSCPEnforcementRecords()
            End If
            If chbAFSSSCPFCERecords.Checked = True Then
                TransferAFSSSCPFCERecords()
            End If
            If chbAFSSSCPRecords.Checked = True Then
                TransferAFSSSCPRecords()
            End If
            If chbAFSSSPPRecords.Checked = True Then
                TransferAFSSSPPRecords()
            End If
        End If



    End Sub
    Sub TransferLookUpAPBManagement()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPAPBMANAGEMENT "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        SQL = "DELETE " & connNameSpace & ".LOOKUPAPBMANAGEMENT "
        cmd = New OracleCommand(SQL, transferConn)
        If transferConn.State = ConnectionState.Closed Then
            transferConn.Open()
        End If
        dr = cmd.ExecuteReader
        dr.Close()

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRDIRECTOR")) Then
                temp = ""
            Else
                temp = drDSRow("STRDIRECTOR")
            End If
            If IsDBNull(drDSRow("STRCOMMISSIONER")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRCOMMISSIONER")
            End If
            If IsDBNull(drDSRow("STRISMPPROGRAMMANG")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRISMPPROGRAMMANG")
            End If
            If IsDBNull(drDSRow("STRSSCPPROGRAMMANG")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("STRSSCPPROGRAMMANG")
            End If
            If IsDBNull(drDSRow("STRSSPPPROGRAMMANG")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("STRSSPPPROGRAMMANG")
            End If
            If IsDBNull(drDSRow("strBranchChief")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("STRBRANCHCHIEF")
            End If

            SQL = "Insert into " & connNameSpace & ".LOOKUPAPBMANAGEMENT " & _
            "Values " & _
            "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
            "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
            "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp5, "'", "''") & "') "

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpApplicationType()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPAPPLICATIONTYPES "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRAPPLICATIONTYPECODE")) Then
                temp = ""
            Else
                temp = drDSRow("STRAPPLICATIONTYPECODE")
            End If
            If IsDBNull(drDSRow("STRAPPLICATIONTYPEDESC")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRAPPLICATIONTYPEDESC")
            End If
            If IsDBNull(drDSRow("STRAPPLICATIONTYPEUSED")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRAPPLICATIONTYPEUSED")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPAPPLICATIONTYPES " & _
            "WHERE STRAPPLICATIONTYPECODE = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPAPPLICATIONTYPES " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPAPPLICATIONTYPES SET " & _
                "STRAPPLICATIONTYPECODE = '" & Replace(temp, "'", "''") & "', " & _
                "STRAPPLICATIONTYPEDESC = '" & Replace(temp2, "'", "''") & "', " & _
                "STRAPPLICATIONTYPEUSED = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE STRAPPLICATIONTYPECODE = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpComplianceActivities()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPCOMPLIANCEACTIVITIES "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRACTIVITYTYPE")) Then
                temp = ""
            Else
                temp = drDSRow("STRACTIVITYTYPE")
            End If
            If IsDBNull(drDSRow("STRACTIVITYNAME")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRACTIVITYNAME")
            End If
            If IsDBNull(drDSRow("STRACTIVITYDESCRIPTION")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRACTIVITYDESCRIPTION")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPCOMPLIANCEACTIVITIES " & _
            "WHERE STRACTIVITYTYPE = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPCOMPLIANCEACTIVITIES " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPCOMPLIANCEACTIVITIES SET " & _
                "STRACTIVITYTYPE = '" & Replace(temp, "'", "''") & "', " & _
                "STRACTIVITYNAME = '" & Replace(temp2, "'", "''") & "', " & _
                "STRACTIVITYDESCRIPTION = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE STRACTIVITYTYPE = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpComplianceStatus()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPCOMPLIANCESTATUS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRCOMPLIANCECODE")) Then
                temp = ""
            Else
                temp = drDSRow("STRCOMPLIANCECODE")
            End If
            If IsDBNull(drDSRow("STRCOMPLIANCEDESC")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRCOMPLIANCEDESC")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPCOMPLIANCESTATUS " & _
            "WHERE STRCOMPLIANCECODE = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPCOMPLIANCESTATUS " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPCOMPLIANCESTATUS SET " & _
                "STRCOMPLIANCECODE = '" & Replace(temp, "'", "''") & "', " & _
                "STRCOMPLIANCEDESC = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE STRCOMPLIANCECODE = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next

    End Sub
    Sub TransferLookUpComplinaceUnits()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPCOMPLIANCEUNITS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRCOMPLIANCEUNIT")) Then
                temp = ""
            Else
                temp = drDSRow("STRCOMPLIANCEUNIT")
            End If
            If IsDBNull(drDSRow("STRUNITTITLE")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRUNITTITLE")
            End If
            If IsDBNull(drDSRow("STRUNITMANAGER")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRUNITMANAGER")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPCOMPLIANCEUNITS " & _
            "WHERE STRCOMPLIANCEUNIT = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPCOMPLIANCEUNITS " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPCOMPLIANCEUNITS SET " & _
                "STRCOMPLIANCEUNIT = '" & Replace(temp, "'", "''") & "', " & _
                "STRUNITTITLE = '" & Replace(temp2, "'", "''") & "', " & _
                "STRUNITMANAGER = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE STRCOMPLIANCEUNIT = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpCountyInformation()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPCOUNTYINFORMATION "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRCOUNTYCODE")) Then
                temp = ""
            Else
                temp = drDSRow("STRCOUNTYCODE")
            End If
            If IsDBNull(drDSRow("STRCOUNTYNAME")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRCOUNTYNAME")
            End If
            If IsDBNull(drDSRow("STRATTAINMENTSTATUS")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRATTAINMENTSTATUS")
            End If
            If IsDBNull(drDSRow("STRNONATTAINMENT")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("STRNONATTAINMENT")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPCOUNTYINFORMATION " & _
            "WHERE STRCOUNTYCODE = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPCOUNTYINFORMATION " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPCOUNTYINFORMATION SET " & _
                "STRCOUNTYCODE = '" & Replace(temp, "'", "''") & "', " & _
                "STRCOUNTYNAME = '" & Replace(temp2, "'", "''") & "', " & _
                "STRATTAINMENTSTATUS = '" & Replace(temp3, "'", "''") & "', " & _
                "STRNONATTAINMENT = '" & Replace(temp4, "'", "''") & "' " & _
                "WHERE STRCOUNTYCODE = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next


    End Sub
    Sub TransferLookUpDistrictInformation()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPDISTRICTINFORMATION "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRDISTRICTCOUNTY")) Then
                temp = ""
            Else
                temp = drDSRow("STRDISTRICTCOUNTY")
            End If
            If IsDBNull(drDSRow("STRDISTRICTCODE")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRDISTRICTCODE")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPDISTRICTINFORMATION " & _
            "WHERE STRDISTRICTCOUNTY = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPDISTRICTINFORMATION " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPDISTRICTINFORMATION SET " & _
                "STRDISTRICTCOUNTY = '" & Replace(temp, "'", "''") & "', " & _
                "STRDISTRICTCODE = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE STRDISTRICTCOUNTY = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpDistrictOffice()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPDISTRICTOFFICE "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRDISTRICTCODE")) Then
                temp = ""
            Else
                temp = drDSRow("STRDISTRICTCODE")
            End If
            If IsDBNull(drDSRow("STROFFICENAME")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STROFFICENAME")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPDISTRICTOFFICE " & _
            "WHERE STRDISTRICTCODE = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPDISTRICTOFFICE " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPDISTRICTOFFICE SET " & _
                "STRDISTRICTCODE = '" & Replace(temp, "'", "''") & "', " & _
                "STROFFICENAME = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE STRDISTRICTCODE = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next

    End Sub
    Sub TransferLookUpDistricts()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPDISTRICTS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRDISTRICTCODE")) Then
                temp = ""
            Else
                temp = drDSRow("STRDISTRICTCODE")
            End If
            If IsDBNull(drDSRow("STRDISTRICTNAME")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRDISTRICTNAME")
            End If
            If IsDBNull(drDSRow("STRDISTRICTMANAGER")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRDISTRICTMANAGER")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPDISTRICTS " & _
            "WHERE STRDISTRICTCODE = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPDISTRICTS " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPDISTRICTS SET " & _
                "STRDISTRICTCODE = '" & Replace(temp, "'", "''") & "', " & _
                "STRDISTRICTNAME = '" & Replace(temp2, "'", "''") & "', " & _
                "STRDISTRICTMANAGER = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE STRDISTRICTCODE = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpEPDBranches()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPEPDBranches "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("numbranchCode")) Then
                temp = ""
            Else
                temp = drDSRow("numbranchCode")
            End If
            If IsDBNull(drDSRow("strBranchDESC")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strBranchDESC")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPEPDBranches " & _
            "WHERE numbranchCode = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPEPDBranches " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPEPDBranches SET " & _
                "numbranchCode = '" & Replace(temp, "'", "''") & "', " & _
                "strBranchDESC = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE numbranchCode = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpEPDPrograms()
        SQL = "Select * " & _
              "from " & connNameSpace & ".LOOKUPEPDPrograms "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("numProgramCode")) Then
                temp = ""
            Else
                temp = drDSRow("numProgramCode")
            End If
            If IsDBNull(drDSRow("strProgramDesc")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strProgramDesc")
            End If
            If IsDBNull(drDSRow("numBranchCode")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("numBranchCode")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPEPDPrograms " & _
            "WHERE numProgramCode = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPEPDPrograms " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPEPDPrograms SET " & _
                "numProgramCode = '" & Replace(temp, "'", "''") & "', " & _
                "strProgramDesc = '" & Replace(temp2, "'", "''") & "', " & _
                "numBranchCode = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE numProgramCode = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpEPDUnits()
        SQL = "Select * " & _
                    "from " & connNameSpace & ".LookUpEPDUnits "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("numUnitCode")) Then
                temp = ""
            Else
                temp = drDSRow("numUnitCode")
            End If
            If IsDBNull(drDSRow("strUnitDesc")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strUnitDesc")
            End If
            If IsDBNull(drDSRow("numProgramCode")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("numProgramCode")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LookUpEPDUnits " & _
            "WHERE numUnitCode = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LookUpEPDUnits " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LookUpEPDUnits SET " & _
                "numUnitCode = '" & Replace(temp, "'", "''") & "', " & _
                "strUnitDesc = '" & Replace(temp2, "'", "''") & "', " & _
                "numProgramCode = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE numUnitCode = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpHPVViolations()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPHPVVIOLATIONS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRHPVVIOLATIONDESC")) Then
                temp = ""
            Else
                temp = drDSRow("STRHPVVIOLATIONDESC")
            End If
            If IsDBNull(drDSRow("STRHPVCODE")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRHPVCODE")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPHPVVIOLATIONS " & _
            "WHERE STRHPVCODE = '" & Replace(temp, "'", "''") & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPHPVVIOLATIONS " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPHPVVIOLATIONS SET " & _
                "STRHPVVIOLATIONDESC = '" & Replace(temp, "'", "''") & "', " & _
                "STRHPVCODE = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE STRHPVCODE = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next

    End Sub
    Sub TransferLookUpIAIPAccounts()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LookupIAIPAccounts "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("numAccountCode")) Then
                temp = ""
            Else
                temp = drDSRow("numAccountCode")
            End If
            If IsDBNull(drDSRow("strAccountDesc")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strAccountDesc")
            End If
            If IsDBNull(drDSRow("numBranchCode")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("numBranchCode")
            End If
            If IsDBNull(drDSRow("numProgramCode")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("numProgramCode")
            End If
            If IsDBNull(drDSRow("numUnitCode")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("numUnitCode")
            End If
            If IsDBNull(drDSRow("strFormAccess")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strFormAccess")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LookupIAIPAccounts " & _
            "WHERE numAccountCode = '" & Replace(temp, "'", "''") & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LookupIAIPAccounts " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LookupIAIPAccounts SET " & _
                "numAccountCode = '" & Replace(temp, "'", "''") & "', " & _
                "strAccountDesc = '" & Replace(temp2, "'", "''") & "', " & _
                "numBranchCode = '" & Replace(temp3, "'", "''") & "', " & _
                "numProgramCode = '" & Replace(temp4, "'", "''") & "', " & _
                "numUnitCode = '" & Replace(temp5, "'", "''") & "', " & _
                "strFormAccess = '" & Replace(temp6, "'", "''") & "'  " & _
                "WHERE numAccountCode = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpIAIPForms()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LookUpIAIPForms "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("numFormCode")) Then
                temp = ""
            Else
                temp = drDSRow("numFormCode")
            End If
            If IsDBNull(drDSRow("strForm")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strForm")
            End If
            If IsDBNull(drDSRow("strFormDesc")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strFormDesc")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LookUpIAIPForms " & _
            "WHERE numFormCode = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LookUpIAIPForms " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LookUpIAIPForms SET " & _
                "numFormCode = '" & Replace(temp, "'", "''") & "', " & _
                "strForm = '" & Replace(temp2, "'", "''") & "', " & _
                "strFormDesc = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE numFormCode = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUPISMPComplianceStatus()
        SQL = "Select * " & _
              "from " & connNameSpace & ".LookUpISMPComplianceStatus "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strComplianceKey")) Then
                temp = ""
            Else
                temp = drDSRow("strComplianceKey")
            End If
            If IsDBNull(drDSRow("strComplianceStatus")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strComplianceStatus")
            End If
            If IsDBNull(drDSRow("strComplianceStatement")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strComplianceStatement")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LookUpISMPComplianceStatus " & _
            "WHERE strComplianceKey = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LookUpISMPComplianceStatus " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LookUpISMPComplianceStatus SET " & _
                "strComplianceKey = '" & Replace(temp, "'", "''") & "', " & _
                "strComplianceStatus = '" & Replace(temp2, "'", "''") & "', " & _
                "strComplianceStatement = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE strComplianceKey = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpISMPMethods()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPISMPMETHODS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRMETHODCODE")) Then
                temp = ""
            Else
                temp = drDSRow("STRMETHODCODE")
            End If
            If IsDBNull(drDSRow("STRMETHODDESC")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRMETHODDESC")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPISMPMETHODS " & _
            "WHERE STRMETHODCODE = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPISMPMETHODS " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPISMPMETHODS SET " & _
                "STRMETHODCODE = '" & Replace(temp, "'", "''") & "', " & _
                "STRMETHODDESC = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE STRMETHODCODE = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpMonitoringUnits()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPMONITORINGUNITS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRMONITORINGUNIT")) Then
                temp = ""
            Else
                temp = drDSRow("STRMONITORINGUNIT")
            End If
            If IsDBNull(drDSRow("STRUNITTITLE")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRUNITTITLE")
            End If
            If IsDBNull(drDSRow("STRUNITMANAGER")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRUNITMANAGER")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPMONITORINGUNITS " & _
            "WHERE STRMONITORINGUNIT = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPMONITORINGUNITS " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPMONITORINGUNITS SET " & _
                "STRMONITORINGUNIT = '" & Replace(temp, "'", "''") & "', " & _
                "STRUNITTITLE = '" & Replace(temp2, "'", "''") & "', " & _
                "STRUNITMANAGER = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE STRMONITORINGUNIT = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next

    End Sub
    Sub TransferLookUpNonAttainment()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPNONATTAINMENT "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRCOUNTYCODE")) Then
                temp = ""
            Else
                temp = drDSRow("STRCOUNTYCODE")
            End If
            If IsDBNull(drDSRow("STRATTAINMENTSTATUS")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRATTAINMENTSTATUS")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPNONATTAINMENT " & _
            "WHERE STRCOUNTYCODE = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPNONATTAINMENT " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPNONATTAINMENT SET " & _
                "STRCOUNTYCODE = '" & Replace(temp, "'", "''") & "', " & _
                "STRATTAINMENTSTATUS = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE STRCOUNTYCODE = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next

    End Sub
    Sub TransferLookUpPermittingUnits()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPPERMITTINGUNITS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRMONITORINGUNIT")) Then
                temp = ""
            Else
                temp = drDSRow("STRMONITORINGUNIT")
            End If
            If IsDBNull(drDSRow("STRUNITTITLE")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRUNITTITLE")
            End If
            If IsDBNull(drDSRow("STRUNITMANAGER")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRUNITMANAGER")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPPERMITTINGUNITS " & _
            "WHERE STRMONITORINGUNIT = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPPERMITTINGUNITS " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPPERMITTINGUNITS SET " & _
                "STRMONITORINGUNIT = '" & Replace(temp, "'", "''") & "', " & _
                "STRUNITTITLE = '" & Replace(temp2, "'", "''") & "', " & _
                "STRUNITMANAGER = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE STRMONITORINGUNIT = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpPermitType()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPPERMITTYPES "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRPERMITTYPECODE")) Then
                temp = ""
            Else
                temp = drDSRow("STRPERMITTYPECODE")
            End If
            If IsDBNull(drDSRow("STRPERMITTYPEDESCRIPTION")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRPERMITTYPEDESCRIPTION")
            End If
            If IsDBNull(drDSRow("STRTYPEUSED")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRTYPEUSED")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPPERMITTYPES " & _
            "WHERE STRPERMITTYPECODE = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPPERMITTYPES " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPPERMITTYPES SET " & _
                "STRPERMITTYPECODE = '" & Replace(temp, "'", "''") & "', " & _
                "STRPERMITTYPEDESCRIPTION = '" & Replace(temp2, "'", "''") & "', " & _
                "STRTYPEUSED = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE STRPERMITTYPECODE = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpPollutants()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPPOLLUTANTS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRPOLLUTANTCODE")) Then
                temp = ""
            Else
                temp = drDSRow("STRPOLLUTANTCODE")
            End If
            If IsDBNull(drDSRow("STRPOLLUTANTDESCRIPTION")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRPOLLUTANTDESCRIPTION")
            End If
            If IsDBNull(drDSRow("STRAFSCODE")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRAFSCODE")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPPOLLUTANTS " & _
            "WHERE STRPOLLUTANTCODE = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPPOLLUTANTS " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPPOLLUTANTS SET " & _
                "STRPOLLUTANTCODE = '" & Replace(temp, "'", "''") & "', " & _
                "STRPOLLUTANTDESCRIPTION = '" & Replace(temp2, "'", "''") & "', " & _
                "STRAFSCODE = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE STRPOLLUTANTCODE = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpSBEAPCaseWork()

    End Sub
    Sub TransferLookUpSICCodes()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPSICCODES "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRSICCODE")) Then
                temp = ""
            Else
                temp = drDSRow("STRSICCODE")
            End If
            If IsDBNull(drDSRow("STRSICDESC")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRSICDESC")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPSICCODES " & _
            "WHERE STRSICCODE = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPSICCODES " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPSICCODES SET " & _
                "STRSICCODE = '" & Replace(temp, "'", "''") & "', " & _
                "STRSICDESC = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE STRSICCODE = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next

    End Sub
    Sub TransferLookUpSSCPNotificaitons()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPSSCPNOTIFICATIONS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRNOTIFICATIONKEY")) Then
                temp = ""
            Else
                temp = drDSRow("STRNOTIFICATIONKEY")
            End If
            If IsDBNull(drDSRow("STRNOTIFICATIONDESC")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRNOTIFICATIONDESC")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPSSCPNOTIFICATIONS " & _
            "WHERE STRNOTIFICATIONKEY = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPSSCPNOTIFICATIONS " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPSSCPNOTIFICATIONS SET " & _
                "STRNOTIFICATIONKEY = '" & Replace(temp, "'", "''") & "', " & _
                "STRNOTIFICATIONDESC = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE STRNOTIFICATIONKEY = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpStates()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPSTATES "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRSTATE")) Then
                temp = ""
            Else
                temp = drDSRow("STRSTATE")
            End If
            If IsDBNull(drDSRow("STRABBREV")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRABBREV")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPSTATES " & _
            "WHERE STRABBREV = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPSTATES " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPSTATES SET " & _
                "STRSTATE = '" & Replace(temp, "'", "''") & "', " & _
                "STRABBREV = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE STRABBREV = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpSubPart60()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPSUBPART60 "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRSUBPART")) Then
                temp = ""
            Else
                temp = drDSRow("STRSUBPART")
            End If
            If IsDBNull(drDSRow("STRDESCRIPTION")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRDESCRIPTION")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPSUBPART60 " & _
            "WHERE STRSUBPART = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPSUBPART60 " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPSUBPART60 SET " & _
                "STRSUBPART = '" & Replace(temp, "'", "''") & "', " & _
                "STRDESCRIPTION = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE STRSUBPART = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpSubPart61()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPSUBPART61 "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRSUBPART")) Then
                temp = ""
            Else
                temp = drDSRow("STRSUBPART")
            End If
            If IsDBNull(drDSRow("STRDESCRIPTION")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRDESCRIPTION")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPSUBPART61 " & _
            "WHERE STRSUBPART = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPSUBPART61 " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPSUBPART61 SET " & _
                "STRSUBPART = '" & Replace(temp, "'", "''") & "', " & _
                "STRDESCRIPTION = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE STRSUBPART = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpSubPart63()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPSUBPART63 "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRSUBPART")) Then
                temp = ""
            Else
                temp = drDSRow("STRSUBPART")
            End If
            If IsDBNull(drDSRow("STRDESCRIPTION")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRDESCRIPTION")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPSUBPART63 " & _
            "WHERE STRSUBPART = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPSUBPART63 " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPSUBPART63 SET " & _
                "STRSUBPART = '" & Replace(temp, "'", "''") & "', " & _
                "STRDESCRIPTION = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE STRSUBPART = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpSubPartSIP()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPSUBPARTSIP "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRSUBPART")) Then
                temp = ""
            Else
                temp = drDSRow("STRSUBPART")
            End If
            If IsDBNull(drDSRow("STRDESCRIPTION")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRDESCRIPTION")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPSUBPARTSIP " & _
            "WHERE STRSUBPART = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPSUBPARTSIP " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPSUBPARTSIP SET " & _
                "STRSUBPART = '" & Replace(temp, "'", "''") & "', " & _
                "STRDESCRIPTION = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE STRSUBPART = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpTestingFirms()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPTESTINGFIRMS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRTESTINGFIRMKEY")) Then
                temp = ""
            Else
                temp = drDSRow("STRTESTINGFIRMKEY")
            End If
            If IsDBNull(drDSRow("STRTESTINGFIRM")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRTESTINGFIRM")
            End If
            If IsDBNull(drDSRow("STRFIRMADDRESS1")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRFIRMADDRESS1")
            End If
            If IsDBNull(drDSRow("STRFIRMADDRESS2")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("STRFIRMADDRESS2")
            End If
            If IsDBNull(drDSRow("STRFIRMCITY")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("STRFIRMCITY")
            End If
            If IsDBNull(drDSRow("STRFIRMSTATE")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("STRFIRMSTATE")
            End If
            If IsDBNull(drDSRow("STRFIRMZIPCODE")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("STRFIRMZIPCODE")
            End If
            If IsDBNull(drDSRow("STRFIRMPHONENUMBER1")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("STRFIRMPHONENUMBER1")
            End If
            If IsDBNull(drDSRow("STRFIRMPHONENUMBER2")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("STRFIRMPHONENUMBER2")
            End If
            If IsDBNull(drDSRow("STRFIRMFAX")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("STRFIRMFAX")
            End If
            If IsDBNull(drDSRow("STRFIRMEMAIL")) Then
                temp11 = "N/A"
            Else
                temp11 = drDSRow("STRFIRMEMAIL")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPTESTINGFIRMS " & _
            "WHERE STRTESTINGFIRMKEY = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPTESTINGFIRMS " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                "'" & Replace(temp11, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPTESTINGFIRMS SET " & _
                "STRTESTINGFIRMKEY = '" & Replace(temp, "'", "''") & "', " & _
                "STRTESTINGFIRM = '" & Replace(temp2, "'", "''") & "', " & _
                "STRFIRMADDRESS1 = '" & Replace(temp3, "'", "''") & "', " & _
                "STRFIRMADDRESS2 = '" & Replace(temp4, "'", "''") & "', " & _
                "STRFIRMCITY = '" & Replace(temp5, "'", "''") & "', " & _
                "STRFIRMSTATE = '" & Replace(temp6, "'", "''") & "', " & _
                "STRFIRMZIPCODE = '" & Replace(temp7, "'", "''") & "', " & _
                "STRFIRMPHONENUMBER1 = '" & Replace(temp8, "'", "''") & "', " & _
                "STRFIRMPHONENUMBER2 = '" & Replace(temp9, "'", "''") & "', " & _
                "STRFIRMFAX = '" & Replace(temp10, "'", "''") & "', " & _
                "STRFIRMEMAIL = '" & Replace(temp11, "'", "''") & "' " & _
                "WHERE STRTESTINGFIRMKEY = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferLookUpUnits()
        SQL = "Select * " & _
        "from " & connNameSpace & ".LOOKUPUNITS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRUNITKEY")) Then
                temp = ""
            Else
                temp = drDSRow("STRUNITKEY")
            End If
            If IsDBNull(drDSRow("STRUNITDESCRIPTION")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRUNITDESCRIPTION")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".LOOKUPUNITS " & _
            "WHERE STRUNITKEY = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".LOOKUPUNITS " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".LOOKUPUNITS SET " & _
                "STRUNITKEY = '" & Replace(temp, "'", "''") & "', " & _
                "STRUNITDESCRIPTION = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE STRUNITKEY = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferNSPSReason()
        SQL = "Select * " & _
        "From " & connNameSpace & ".FSNSPSReason " & _
        "order by ReasonID "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("ReasonID")) Then
                temp = ""
            Else
                temp = drDSRow("ReasonID")
            End If
            If IsDBNull(drDSRow("Reason")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("Reason")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".FSNSPSReason " & _
            "WHERE ReasonID = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".FSNSPSReason " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".FSNSPSReason SET " & _
                "Reason = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE ReasonID = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next

    End Sub
    Sub TransferEDPUsers()
        SQL = "Select * " & _
        "From " & connNameSpace & ".EPDUsers " & _
        "order by numUserID "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("numUserID")) Then
                temp = ""
            Else
                temp = drDSRow("numUserID")
            End If
            If IsDBNull(drDSRow("strUserName")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strUserName")
            End If
            If IsDBNull(drDSRow("strPassword")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strPassword")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".EPDUsers " & _
            "WHERE numUserID = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".EPDUsers " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".EPDUsers SET " & _
                "strUserName = '" & Replace(temp2, "'", "''") & "', " & _
                "strPassword = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE numUserID = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Next

        SQL = "Select * " & _
        "From " & connNameSpace & ".EPDUserProfiles " & _
        "order by numUserID "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("numUserID")) Then
                temp = ""
            Else
                temp = drDSRow("numUserID")
            End If
            If IsDBNull(drDSRow("strEmployeeID")) Then
                temp2 = "000"
            Else
                temp2 = drDSRow("strEmployeeID")
            End If
            If IsDBNull(drDSRow("strLastname")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strLastname")
            End If

            If IsDBNull(drDSRow("strFirstName")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strFirstName")
            End If
            If IsDBNull(drDSRow("strEmailAddress")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strEmailAddress")
            End If
            If IsDBNull(drDSRow("strPhone")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strPhone")
            End If
            If IsDBNull(drDSRow("strFax")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strFax")
            End If
            If IsDBNull(drDSRow("numBranch")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("numBranch")
            End If
            If IsDBNull(drDSRow("numProgram")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("numProgram")
            End If
            If IsDBNull(drDSRow("numUnit")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("numUnit")
            End If
            If IsDBNull(drDSRow("strOffice")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strOffice")
            End If
            If IsDBNull(drDSRow("numEmployeeStatus")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("numEmployeeStatus")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".EPDUserProfiles " & _
            "WHERE numUserID = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".EPDUserProfiles " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".EPDUserProfiles SET " & _
                "strEmployeeID = '" & Replace(temp2, "'", "''") & "', " & _
                "strLastName = '" & Replace(temp3, "'", "''") & "', " & _
                "strFirstName = '" & Replace(temp4, "'", "''") & "', " & _
                "strEmailAddress = '" & Replace(temp5, "'", "''") & "', " & _
                "strPhone = '" & Replace(temp6, "'", "''") & "', " & _
                "strFax = '" & Replace(temp7, "'", "''") & "', " & _
                "numBranch = '" & Replace(temp8, "'", "''") & "', " & _
                "numProgram = '" & Replace(temp9, "'", "''") & "', " & _
                "numUnit = '" & Replace(temp10, "'", "''") & "', " & _
                "strOffice = '" & Replace(temp11, "'", "''") & "', " & _
                "numEmployeeStatus = '" & Replace(temp12, "'", "''") & "' " & _
                "WHERE numUserID = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Next


    End Sub
    Sub TransferEISI()
        SQL = "Select * " & _
        "From " & connNameSpace & ".EISI " & _
        "order by strAIRSYear "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strConfirmationNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strConfirmationNumber")
            End If
            If IsDBNull(drDSRow("strInventoryYear")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strInventoryYear")
            End If
            If IsDBNull(drDSRow("strStateCountyFIPS")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strStateCountyFIPS")
            End If
            If IsDBNull(drDSRow("strStateFacilityIdentifier")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strStateFacilityIdentifier")
            End If
            If IsDBNull(drDSRow("strStateFacilityCategory")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strStateFacilityCategory")
            End If
            If IsDBNull(drDSRow("strSICPrimary")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strSICPrimary")
            End If
            If IsDBNull(drDSRow("strNAICSPrimary")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strNAICSPrimary")
            End If
            If IsDBNull(drDSRow("strFacilityName")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strFacilityName")
            End If
            If IsDBNull(drDSRow("strSiteDescription")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strSiteDescription")
            End If
            If IsDBNull(drDSRow("strLocationAddress")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strLocationAddress")
            End If
            If IsDBNull(drDSRow("strCity")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strCity")
            End If
            If IsDBNull(drDSRow("strState")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strState")
            End If
            If IsDBNull(drDSRow("strZipCode")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strZipCode")
            End If
            If IsDBNull(drDSRow("strCounty")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strCounty")
            End If
            If IsDBNull(drDSRow("strHorizontalCollectionCode")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("strHorizontalCollectionCode")
            End If
            If IsDBNull(drDSRow("strHorizontalAccuracyMeasure")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("strHorizontalAccuracyMeasure")
            End If
            If IsDBNull(drDSRow("strHorizontalReferenceCode")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("strHorizontalReferenceCode")
            End If
            If IsDBNull(drDSRow("dblXcoordinate")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("dblXcoordinate")
            End If
            If IsDBNull(drDSRow("dblYcoordinate")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("dblYcoordinate")
            End If
            If IsDBNull(drDSRow("strContactFirstName")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("strContactFirstName")
            End If
            If IsDBNull(drDSRow("strContactLastName")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("strContactLastName")
            End If
            If IsDBNull(drDSRow("strContactPrefix")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("strContactPrefix")
            End If
            If IsDBNull(drDSRow("strContactSuffix")) Then
                temp23 = ""
            Else
                temp23 = drDSRow("strContactSuffix")
            End If
            If IsDBNull(drDSRow("strContactTitle")) Then
                temp24 = ""
            Else
                temp24 = drDSRow("strContactTitle")
            End If
            If IsDBNull(drDSRow("strContactCompanyName")) Then
                temp25 = ""
            Else
                temp25 = drDSRow("strContactCompanyName")
            End If
            If IsDBNull(drDSRow("strContactPhoneNumber1")) Then
                temp26 = ""
            Else
                temp26 = drDSRow("strContactPhoneNumber1")
            End If
            If IsDBNull(drDSRow("strContactPhoneNumber2")) Then
                temp27 = ""
            Else
                temp27 = drDSRow("strContactPhoneNumber2")
            End If
            If IsDBNull(drDSRow("strContactFaxNumber")) Then
                temp28 = ""
            Else
                temp28 = drDSRow("strContactFaxNumber")
            End If
            If IsDBNull(drDSRow("strContactEmail")) Then
                temp29 = ""
            Else
                temp29 = drDSRow("strContactEmail")
            End If
            If IsDBNull(drDSRow("strContactAddress1")) Then
                temp30 = ""
            Else
                temp30 = drDSRow("strContactAddress1")
            End If
            If IsDBNull(drDSRow("strContactAddress2")) Then
                temp31 = ""
            Else
                temp31 = drDSRow("strContactAddress2")
            End If
            If IsDBNull(drDSRow("strContactCity")) Then
                temp32 = ""
            Else
                temp32 = drDSRow("strContactCity")
            End If
            If IsDBNull(drDSRow("strContactState")) Then
                temp33 = ""
            Else
                temp33 = drDSRow("strContactState")
            End If
            If IsDBNull(drDSRow("strContactZipCode")) Then
                temp34 = ""
            Else
                temp34 = drDSRow("strContactZipCode")
            End If
            If IsDBNull(drDSRow("strOptOut")) Then
                temp35 = ""
            Else
                temp35 = drDSRow("strOptOut")
            End If
            If IsDBNull(drDSRow("strUserName")) Then
                temp36 = ""
            Else
                temp36 = drDSRow("strUserName")
            End If
            If IsDBNull(drDSRow("strFinalize")) Then
                temp37 = ""
            Else
                temp37 = drDSRow("strFinalize")
            End If
            If IsDBNull(drDSRow("strValidate0")) Then
                temp38 = ""
            Else
                temp38 = drDSRow("strValidate0")
            End If
            If IsDBNull(drDSRow("strValidate1")) Then
                temp39 = ""
            Else
                temp39 = drDSRow("strValidate1")
            End If
            If IsDBNull(drDSRow("strValidate2")) Then
                temp40 = ""
            Else
                temp40 = drDSRow("strValidate2")
            End If
            If IsDBNull(drDSRow("strValidate3")) Then
                temp41 = ""
            Else
                temp41 = drDSRow("strValidate3")
            End If
            If IsDBNull(drDSRow("strDateLastLogIn")) Then
                temp42 = ""
            Else
                temp42 = drDSRow("strDateLastLogIn")
            End If
            If IsDBNull(drDSRow("strTimeLastLogIn")) Then
                temp43 = ""
            Else
                temp43 = drDSRow("strTimeLastLogIn")
            End If
            If IsDBNull(drDSRow("strAIRSYear")) Then
                temp44 = ""
            Else
                temp44 = drDSRow("strAIRSYear")
            End If
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp45 = ""
            Else
                temp45 = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("strQAStarted")) Then
                temp46 = ""
            Else
                temp46 = drDSRow("strQAStarted")
            End If
            If IsDBNull(drDSRow("strQADone")) Then
                temp47 = ""
            Else
                temp47 = drDSRow("strQADone")
            End If
            If IsDBNull(drDSRow("strQAComments")) Then
                temp48 = ""
            Else
                temp48 = drDSRow("strQAComments")
            End If
            If IsDBNull(drDSRow("numUserID")) Then
                temp49 = ""
            Else
                temp49 = drDSRow("numUserID")
            End If
            If IsDBNull(drDSRow("strTransactionTime")) Then
                temp50 = ""
            Else
                temp50 = drDSRow("strTransactionTime")
            End If
            If IsDBNull(drDSRow("strTransactionDate")) Then
                temp51 = ""
            Else
                temp51 = drDSRow("strTransactionDate")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".EISI " & _
            "WHERE strAIRSYear = '" & temp44 & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".EISI " & _
                "Values " & _
                "('" & Replace(temp2, "'", "''") & "', '" & Replace(temp3, "'", "''") & "', " & _
                "'" & Replace(temp4, "'", "''") & "', '" & Replace(temp5, "'", "''") & "', " & _
                "'" & Replace(temp6, "'", "''") & "', '" & Replace(temp7, "'", "''") & "', " & _
                "'" & Replace(temp8, "'", "''") & "', '" & Replace(temp9, "'", "''") & "', " & _
                "'" & Replace(temp10, "'", "''") & "', '" & Replace(temp11, "'", "''") & "', " & _
                "'" & Replace(temp12, "'", "''") & "', '" & Replace(temp13, "'", "''") & "', " & _
                "'" & Replace(temp14, "'", "''") & "', '" & Replace(temp15, "'", "''") & "', " & _
                "'" & Replace(temp16, "'", "''") & "', '" & Replace(temp17, "'", "''") & "', " & _
                "'" & Replace(temp18, "'", "''") & "', '" & Replace(temp19, "'", "''") & "', " & _
                "'" & Replace(temp20, "'", "''") & "', '" & Replace(temp21, "'", "''") & "', " & _
                "'" & Replace(temp22, "'", "''") & "', '" & Replace(temp23, "'", "''") & "', " & _
                "'" & Replace(temp24, "'", "''") & "', '" & Replace(temp25, "'", "''") & "', " & _
                "'" & Replace(temp26, "'", "''") & "', '" & Replace(temp27, "'", "''") & "', " & _
                "'" & Replace(temp28, "'", "''") & "', '" & Replace(temp29, "'", "''") & "', " & _
                "'" & Replace(temp30, "'", "''") & "', '" & Replace(temp31, "'", "''") & "', " & _
                "'" & Replace(temp32, "'", "''") & "', '" & Replace(temp33, "'", "''") & "', " & _
                "'" & Replace(temp34, "'", "''") & "', '" & Replace(temp35, "'", "''") & "', " & _
                "'" & Replace(temp36, "'", "''") & "', '" & Replace(temp37, "'", "''") & "', " & _
                "'" & Replace(temp38, "'", "''") & "', '" & Replace(temp39, "'", "''") & "', " & _
                "'" & Replace(temp40, "'", "''") & "', '" & Replace(temp41, "'", "''") & "', " & _
                "'" & Replace(temp42, "'", "''") & "', '" & Replace(temp43, "'", "''") & "', " & _
                "'" & Replace(temp44, "'", "''") & "', '" & Replace(temp45, "'", "''") & "', " & _
                "'" & Replace(temp, "'", "''") & "', '" & Replace(temp46, "'", "''") & "', " & _
                "'" & Replace(temp47, "'", "''") & "', '" & Replace(temp48, "'", "''") & "', " & _
                "'" & Replace(temp49, "'", "''") & "', '" & Replace(temp50, "'", "''") & "', " & _
                "'" & Replace(temp51, "'", "''") & "', '' ) "
            Else
                SQL = "UPDATE " & connNameSpace & ".EISI SET " & _
                "strInventoryYear = '" & Replace(temp2, "'", "''") & "', " & _
                "strStateCountyFIPS = '" & Replace(temp3, "'", "''") & "', " & _
                "strStateFacilityIdentifier = '" & Replace(temp4, "'", "''") & "', " & _
                "strStateFacilityCategory = '" & Replace(temp5, "'", "''") & "', " & _
                "strSICPrimary = '" & Replace(temp6, "'", "''") & "', " & _
                "strNAICSPrimary = '" & Replace(temp7, "'", "''") & "', " & _
                "strFacilityName = '" & Replace(temp8, "'", "''") & "', " & _
                "strSiteDescription = '" & Replace(temp9, "'", "''") & "', " & _
                "strLocationAddress = '" & Replace(temp10, "'", "''") & "', " & _
                "strCity = '" & Replace(temp11, "'", "''") & "', " & _
                "strState = '" & Replace(temp12, "'", "''") & "', " & _
                "strZipCode = '" & Replace(temp13, "'", "''") & "', " & _
                "strCounty = '" & Replace(temp14, "'", "''") & "', " & _
                "strHorizontalCollectionCode = '" & Replace(temp15, "'", "''") & "', " & _
                "strHorizontalAccuracyMeasure = '" & Replace(temp16, "'", "''") & "', " & _
                "strHorizontalReferenceCode = '" & Replace(temp17, "'", "''") & "', " & _
                "dblXCoordinate = '" & Replace(temp18, "'", "''") & "', " & _
                "dblYCoordinate = '" & Replace(temp19, "'", "''") & "', " & _
                "strContactFirstName = '" & Replace(temp20, "'", "''") & "', " & _
                "strContactLastName = '" & Replace(temp21, "'", "''") & "', " & _
                "strContactPrefix = '" & Replace(temp22, "'", "''") & "', " & _
                "strContactSuffix = '" & Replace(temp23, "'", "''") & "', " & _
                "strContactTitle = '" & Replace(temp24, "'", "''") & "', " & _
                "strContactCompanyName = '" & Replace(temp25, "'", "''") & "', " & _
                "strContactPhoneNumber1 = '" & Replace(temp26, "'", "''") & "', " & _
                "strContactPhoneNumber2 = '" & Replace(temp27, "'", "''") & "', " & _
                "strContactFaxNumber = '" & Replace(temp28, "'", "''") & "', " & _
                "strContactEmail = '" & Replace(temp29, "'", "''") & "', " & _
                "strContactAddress1 = '" & Replace(temp30, "'", "''") & "', " & _
                "strContactAddress2 = '" & Replace(temp31, "'", "''") & "', " & _
                "strContactCity = '" & Replace(temp32, "'", "''") & "', " & _
                "strContactState = '" & Replace(temp33, "'", "''") & "', " & _
                "strContactZipCode = '" & Replace(temp34, "'", "''") & "', " & _
                "strOptOut = '" & Replace(temp35, "'", "''") & "', " & _
                "strUserName = '" & Replace(temp36, "'", "''") & "', " & _
                "strFinalize = '" & Replace(temp37, "'", "''") & "', " & _
                "strValidate0 = '" & Replace(temp38, "'", "''") & "', " & _
                "strValidate1 = '" & Replace(temp39, "'", "''") & "', " & _
                "strValidate2 = '" & Replace(temp40, "'", "''") & "', " & _
                "strValidate3 = '" & Replace(temp41, "'", "''") & "', " & _
                "strDateLastLogIn = '" & Replace(temp42, "'", "''") & "', " & _
                "strTimeLastLogIn = '" & Replace(temp43, "'", "''") & "', " & _
                "strAIRSYear = '" & Replace(temp44, "'", "''") & "', " & _
                "strAIRSNumber = '" & Replace(temp45, "'", "''") & "', " & _
                "strConfirmationNumber = '" & Replace(temp, "'", "''") & "', " & _
                "strQAStarted = '" & Replace(temp46, "'", "''") & "', " & _
                "strQADone = '" & Replace(temp47, "'", "''") & "', " & _
                "strQAComments = '" & Replace(temp48, "'", "''") & "', " & _
                "numUserID = '" & Replace(temp49, "'", "''") & "', " & _
                "strTransactionTime = '" & Replace(temp50, "'", "''") & "', " & _
                "strTransactionDate = '" & Replace(temp51, "'", "''") & "' " & _
                "WHERE strAIRSYear = '" & Replace(temp44, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Next

    End Sub
    Sub TransferEIEU()
        SQL = "Select * " & _
       "From " & connNameSpace & ".EIEU " & _
       "order by strAIRSYear "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strInventoryYear")) Then
                temp = ""
            Else
                temp = drDSRow("strInventoryYear")
            End If
            If IsDBNull(drDSRow("strStateCountyFIPS")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strStateCountyFIPS")
            End If
            If IsDBNull(drDSRow("strStateFacilityIdentifier")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strStateFacilityIdentifier")
            End If
            If IsDBNull(drDSRow("strEmissionUnitID")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strEmissionUnitID")
            End If
            If IsDBNull(drDSRow("sngDesignCapacity")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("sngDesignCapacity")
            End If
            If IsDBNull(drDSRow("strDesignCapUnitNum")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strDesignCapUnitNum")
            End If
            If IsDBNull(drDSRow("strDesignCapUnitDenom")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strDesignCapUnitDenom")
            End If
            If IsDBNull(drDSRow("sngMaxNamePlateCapacity")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("sngMaxNamePlateCapacity")
            End If
            If IsDBNull(drDSRow("strEmissionUnitDesc")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strEmissionUnitDesc")
            End If
            If IsDBNull(drDSRow("strAIRSYearEU")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strAIRSYearEU")
            End If
            If IsDBNull(drDSRow("strAIRSYear")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strAIRSYear")
            End If
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("strTransactionDate")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strTransactionDate")
            End If
            If IsDBNull(drDSRow("strDesignCapUnitNumDesc")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strDesignCapUnitNumDesc")
            End If
            If IsDBNull(drDSRow("strDesignCapUnitDenomDesc")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("strDesignCapUnitDenomDesc")
            End If
            If IsDBNull(drDSRow("strYesNo")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("strYesNo")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".EIEU " & _
            "WHERE strAIRSYearEU = '" & temp10 & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".EIEU " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".EIEU SET " & _
                "strInventoryYear = '" & Replace(temp, "'", "''") & "', " & _
                "strStateCountyFIPS = '" & Replace(temp2, "'", "''") & "', " & _
                "strStateFacilityIdentifier = '" & Replace(temp3, "'", "''") & "', " & _
                "strEmissionunitID = '" & Replace(temp4, "'", "''") & "', " & _
                "sngDesignCapacity = '" & Replace(temp5, "'", "''") & "', " & _
                "strDesignCapUnitNum = '" & Replace(temp6, "'", "''") & "', " & _
                "strDesignCapUnitDenom = '" & Replace(temp7, "'", "''") & "', " & _
                "sngMaxNamePlateCapacity = '" & Replace(temp8, "'", "''") & "', " & _
                "strEmissionUnitDesc = '" & Replace(temp9, "'", "''") & "', " & _
                "strAIRSYearEU = '" & Replace(temp10, "'", "''") & "', " & _
                "strAIRSYear = '" & Replace(temp11, "'", "''") & "', " & _
                "strAIRSNumber = '" & Replace(temp12, "'", "''") & "', " & _
                "strTransactionDate = '" & Replace(temp13, "'", "''") & "', " & _
                "strDesignCapUnitNumDesc = '" & Replace(temp14, "'", "''") & "', " & _
                "strDesignCapUnitDenomDesc = '" & Replace(temp15, "'", "''") & "', " & _
                "strYesNo = '" & Replace(temp16, "'", "''") & "' " & _
                "WHERE strAIRSYearEU = '" & Replace(temp10, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Next

    End Sub
    Sub TransferEIER()
        SQL = "Select * " & _
  "From " & connNameSpace & ".EIER " & _
  "order by strAIRSYearERPointID "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strInventoryYear")) Then
                temp = ""
            Else
                temp = drDSRow("strInventoryYear")
            End If
            If IsDBNull(drDSRow("strStateCountyFIPS")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strStateCountyFIPS")
            End If
            If IsDBNull(drDSRow("strStateFacilityIdentifier")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strStateFacilityIdentifier")
            End If
            If IsDBNull(drDSRow("strEmissionReleasePointID")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strEmissionReleasePointID")
            End If
            If IsDBNull(drDSRow("strEmissionReleaseType")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strEmissionReleaseType")
            End If
            If IsDBNull(drDSRow("sngStackHeight")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("sngStackHeight")
            End If
            If IsDBNull(drDSRow("sngStackDiameter")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("sngStackDiameter")
            End If
            If IsDBNull(drDSRow("sngExitGasTemperature")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("sngExitGasTemperature")
            End If
            If IsDBNull(drDSRow("sngExitGasVelocity")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("sngExitGasVelocity")
            End If
            If IsDBNull(drDSRow("sngExitGasFlowRate")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("sngExitGasFlowRate")
            End If
            If IsDBNull(drDSRow("dblXcoordinate")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("dblXcoordinate")
            End If
            If IsDBNull(drDSRow("dblYcoordinate")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("dblYcoordinate")
            End If
            If IsDBNull(drDSRow("strEmissionReleasePtDesc")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strEmissionReleasePtDesc")
            End If
            If IsDBNull(drDSRow("strHorizontalCollectionCode")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strHorizontalCollectionCode")
            End If
            If IsDBNull(drDSRow("strHorizontalAccuracyMeasure")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("strHorizontalAccuracyMeasure")
            End If
            If IsDBNull(drDSRow("strHorizontalReferenceCode")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("strHorizontalReferenceCode")
            End If
            If IsDBNull(drDSRow("strAIRSYearERPointID")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("strAIRSYearERPointID")
            End If
            If IsDBNull(drDSRow("strAIRSYear")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("strAIRSYear")
            End If
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("strTransActionDate")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("strTransActionDate")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".EIER " & _
            "WHERE strAIRSYearERPointID = '" & temp17 & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".EIER " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".EIER SET " & _
                "strInventoryYear = '" & Replace(temp, "'", "''") & "', " & _
                "strStateCountyFIPS = '" & Replace(temp2, "'", "''") & "', " & _
                "strStateFacilityIdentifier = '" & Replace(temp3, "'", "''") & "', " & _
                "strEmissionReleasePointID = '" & Replace(temp4, "'", "''") & "', " & _
                "strEmissionReleaseType = '" & Replace(temp5, "'", "''") & "', " & _
                "sngStackHeight = '" & Replace(temp6, "'", "''") & "', " & _
                "sngStackDiameter = '" & Replace(temp7, "'", "''") & "', " & _
                "sngExitGasTemperature = '" & Replace(temp8, "'", "''") & "', " & _
                "sngExitGasVelocity = '" & Replace(temp9, "'", "''") & "', " & _
                "sngExitGasFlowRate = '" & Replace(temp10, "'", "''") & "', " & _
                "dblXCoordinate = '" & Replace(temp11, "'", "''") & "', " & _
                "dblYCoordinate = '" & Replace(temp12, "'", "''") & "', " & _
                "strEmissionReleasePTDesc = '" & Replace(temp13, "'", "''") & "', " & _
                "strHorizontalCollectionCode = '" & Replace(temp14, "'", "''") & "', " & _
                "strHorizontalAccuracyMeasure = '" & Replace(temp15, "'", "''") & "', " & _
                "strHorizontalReferenceCode = '" & Replace(temp16, "'", "''") & "', " & _
                "strAIRSYearERPointID = '" & Replace(temp17, "'", "''") & "', " & _
                "strAIRSYear = '" & Replace(temp18, "'", "''") & "', " & _
                "strAIRSNumber = '" & Replace(temp19, "'", "''") & "', " & _
                "strTransActionDate = '" & Replace(temp20, "'", "''") & "' " & _
                "WHERE strAIRSYearERPointID = '" & Replace(temp17, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Next
    End Sub
    Sub TransferEIEP()
        SQL = "Select * " & _
"From " & connNameSpace & ".EIEP " & _
"order by strAIRSYearEUProcID "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strInventoryYear")) Then
                temp = ""
            Else
                temp = drDSRow("strInventoryYear")
            End If
            If IsDBNull(drDSRow("strStateCountyFIPS")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strStateCountyFIPS")
            End If
            If IsDBNull(drDSRow("strStateFacilityIdentifier")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strStateFacilityIdentifier")
            End If
            If IsDBNull(drDSRow("strEmissionUnitID")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strEmissionUnitID")
            End If
            If IsDBNull(drDSRow("strEmissionReleasePointID")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strEmissionReleasePointID")
            End If
            If IsDBNull(drDSRow("strProcessID")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strProcessID")
            End If
            If IsDBNull(drDSRow("strSCC")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strSCC")
            End If
            If IsDBNull(drDSRow("strProcessMactCode")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strProcessMactCode")
            End If
            If IsDBNull(drDSRow("strEmissionProcessDescription")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strEmissionProcessDescription")
            End If
            If IsDBNull(drDSRow("intWinterThroughPutPCT")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("intWinterThroughPutPCT")
            End If
            If IsDBNull(drDSRow("intSpringThroughPutPCT")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("intSpringThroughPutPCT")
            End If
            If IsDBNull(drDSRow("intSummerThroughPutPCT")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("intSummerThroughputPCT")
            End If
            If IsDBNull(drDSRow("intFallThroughPutPCT")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("intFallThroughPutPCT")
            End If
            If IsDBNull(drDSRow("intAnnualAvgDaysPerWeek")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("intAnnualAvgDaysPerWeek")
            End If
            If IsDBNull(drDSRow("intAnnualAvgWeeksPerYear")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("intAnnualAvgWeeksPerYear")
            End If
            If IsDBNull(drDSRow("intAnnualAvgHoursPerDay")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("intAnnualAvgHoursPerDay")
            End If
            If IsDBNull(drDSRow("intAnnualAvgHoursPerYear")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("intAnnualAvgHoursPerYear")
            End If
            If IsDBNull(drDSRow("sngHeatContent")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("sngHeatContent")
            End If
            If IsDBNull(drDSRow("sngSulfurContent")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("sngSulfurContent")
            End If
            If IsDBNull(drDSRow("sngAshContent")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("sngAshContent")
            End If
            If IsDBNull(drDSRow("strProcessMACTComplianceStat")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("strProcessMACTComplianceStat")
            End If
            If IsDBNull(drDSRow("sngDailySummerProcesstPut")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("sngDailySummerProcesstPut")
            End If
            If IsDBNull(drDSRow("strDailySummerProcesstPutNum")) Then
                temp23 = ""
            Else
                temp23 = drDSRow("strDailySummerProcesstPutNum")
            End If
            If IsDBNull(drDSRow("sngActualThroughPut")) Then
                temp24 = ""
            Else
                temp24 = drDSRow("sngActualThroughPut")
            End If
            If IsDBNull(drDSRow("strThroughPutUnitNumerator")) Then
                temp25 = ""
            Else
                temp25 = drDSRow("strThroughPutUnitNumerator")
            End If
            If IsDBNull(drDSRow("strAIRSYearEU")) Then
                temp26 = ""
            Else
                temp26 = drDSRow("strAIRSYearEU")
            End If
            If IsDBNull(drDSRow("strAIRSYearEUProcID")) Then
                temp27 = ""
            Else
                temp27 = drDSRow("strAIRSYearEUProcID")
            End If
            If IsDBNull(drDSRow("strAIRSYear")) Then
                temp28 = ""
            Else
                temp28 = drDSRow("strAIRSYear")
            End If
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp29 = ""
            Else
                temp29 = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("strTransactionDate")) Then
                temp30 = ""
            Else
                temp30 = drDSRow("strTransactionDate")
            End If
            If IsDBNull(drDSRow("strStartTime")) Then
                temp31 = ""
            Else
                temp31 = drDSRow("strStartTime")
            End If
            If IsDBNull(drDSRow("strYesNo")) Then
                temp32 = ""
            Else
                temp32 = drDSRow("strYesNo")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".EIEP " & _
            "WHERE strAIRSYearEUProcID = '" & temp27 & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".EIEP " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                "'" & Replace(temp21, "'", "''") & "', '" & Replace(temp22, "'", "''") & "', " & _
                "'" & Replace(temp23, "'", "''") & "', '" & Replace(temp24, "'", "''") & "', " & _
                "'" & Replace(temp25, "'", "''") & "', '" & Replace(temp26, "'", "''") & "', " & _
                "'" & Replace(temp27, "'", "''") & "', '" & Replace(temp28, "'", "''") & "', " & _
                "'" & Replace(temp29, "'", "''") & "', '" & Replace(temp30, "'", "''") & "', " & _
                "'" & Replace(temp31, "'", "''") & "', '" & Replace(temp32, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".EIEP SET " & _
                "strInventoryYear = '" & Replace(temp, "'", "''") & "', " & _
                "strStateCountyFIPS = '" & Replace(temp2, "'", "''") & "', " & _
                "strStateFacilityIdentifier = '" & Replace(temp3, "'", "''") & "', " & _
                "strEmissionUnitID = '" & Replace(temp4, "'", "''") & "', " & _
                "strEmissionReleasePointID = '" & Replace(temp5, "'", "''") & "', " & _
                "strProcessID = '" & Replace(temp6, "'", "''") & "', " & _
                "strSCC = '" & Replace(temp7, "'", "''") & "', " & _
                "strProcessMACTCode = '" & Replace(temp8, "'", "''") & "', " & _
                "strEmissionProcessDescription = '" & Replace(temp9, "'", "''") & "', " & _
                "intWinterThroughPutPCT = '" & Replace(temp10, "'", "''") & "', " & _
                "intSpringThroughPutPCT = '" & Replace(temp11, "'", "''") & "', " & _
                "intSummerThroughPutPCT = '" & Replace(temp12, "'", "''") & "', " & _
                "intFallThroughPutPCT = '" & Replace(temp13, "'", "''") & "', " & _
                "intAnnualAvgDaysPerWeek = '" & Replace(temp14, "'", "''") & "', " & _
                "intAnnualAvgWeeksPerYear = '" & Replace(temp15, "'", "''") & "', " & _
                "intAnnualAvgHoursPerDay = '" & Replace(temp16, "'", "''") & "', " & _
                "intAnnualAvgHoursPerYear = '" & Replace(temp17, "'", "''") & "', " & _
                "sngHeatContent = '" & Replace(temp18, "'", "''") & "', " & _
                "sngSulfurContent = '" & Replace(temp19, "'", "''") & "', " & _
                "sngASHContent = '" & Replace(temp20, "'", "''") & "', " & _
                "strProcessMACTComplianceStat = '" & Replace(temp21, "'", "''") & "', " & _
                "sngDailySummerProcesstPut = '" & Replace(temp22, "'", "''") & "', " & _
                "strDailySummerProcesstPutNum = '" & Replace(temp23, "'", "''") & "', " & _
                "sngActualThroughPut = '" & Replace(temp24, "'", "''") & "', " & _
                "strThroughPutUnitNumerator = '" & Replace(temp25, "'", "''") & "', " & _
                "strAIRSYearEU = '" & Replace(temp26, "'", "''") & "', " & _
                "strAIRSYearEUProcID = '" & Replace(temp27, "'", "''") & "', " & _
                "strAIRSYear = '" & Replace(temp28, "'", "''") & "', " & _
                "strAIRSNumber = '" & Replace(temp29, "'", "''") & "', " & _
                "strTransActionDate = '" & Replace(temp30, "'", "''") & "', " & _
                "strStartTime = '" & Replace(temp31, "'", "''") & "', " & _
                "strYesNo = '" & Replace(temp32, "'", "''") & "' " & _
                "WHERE strAIRSYearEUProcID = '" & Replace(temp27, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Next


    End Sub
    Sub TransferEIEM()
        SQL = "Select * " & _
"From " & connNameSpace & ".EIEM " & _
"order by strAIRSYearEUEPEM "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strInventoryYear")) Then
                temp = ""
            Else
                temp = drDSRow("strInventoryYear")
            End If
            If IsDBNull(drDSRow("strStateCountyFIPS")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strStateCountyFIPS")
            End If
            If IsDBNull(drDSRow("strStateFacilityIdentifier")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strStateFacilityIdentifier")
            End If
            If IsDBNull(drDSRow("strEmissionUnitID")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strEmissionUnitID")
            End If
            If IsDBNull(drDSRow("strProcessID")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strProcessID")
            End If
            If IsDBNull(drDSRow("strPollutantCode")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strPollutantCode")
            End If
            If IsDBNull(drDSRow("strEmissionReleasePointID")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strEmissionReleasePointID")
            End If
            If IsDBNull(drDSRow("dblEmissionNumericValue")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("dblEmissionNumericValue")
            End If
            If IsDBNull(drDSRow("strEmissionUnitNumerator")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strEmissionUnitNumerator")
            End If
            If IsDBNull(drDSRow("strEmissionType")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strEmissionType")
            End If
            If IsDBNull(drDSRow("sngFactorNumericValue")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("sngFactorNumericValue")
            End If
            If IsDBNull(drDSRow("strFactorUnitNumerator")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strFactorUnitNumerator")
            End If
            If IsDBNull(drDSRow("strFactorUnitDenominator")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strFactorUnitDenominator")
            End If
            If IsDBNull(drDSRow("strEmissionCalculationMetCode")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strEmissionCalculationMetCode")
            End If
            If IsDBNull(drDSRow("sngRuleEffectiveNess")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("sngRuleEffectiveNess")
            End If
            If IsDBNull(drDSRow("strControlStatus")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("strControlStatus")
            End If
            If IsDBNull(drDSRow("sngPrimaryPCTControlEffic")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("sngPrimaryPCTControlEffic")
            End If
            If IsDBNull(drDSRow("sngPctCaptureEfficiency")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("sngPctCaptureEfficiency")
            End If
            If IsDBNull(drDSRow("sngTotalCaptureControlEffic")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("sngTotalCaptureControlEffic")
            End If
            If IsDBNull(drDSRow("strPrimaryDeviceTypeCode")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("strPrimaryDeviceTypeCode")
            End If
            If IsDBNull(drDSRow("strSecondaryDeviceTypeCode")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("strSecondaryDeviceTypeCode")
            End If
            If IsDBNull(drDSRow("strControlSystemDescription")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("strControlSystemDescription")
            End If
            If IsDBNull(drDSRow("strAIRSYearEUProcid")) Then
                temp23 = ""
            Else
                temp23 = drDSRow("strAIRSYearEUProcid")
            End If
            If IsDBNull(drDSRow("strAIRSYear")) Then
                temp24 = ""
            Else
                temp24 = drDSRow("strAIRSYear")
            End If
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp25 = ""
            Else
                temp25 = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("strTransactionDate")) Then
                temp26 = ""
            Else
                temp26 = drDSRow("strTransactionDate")
            End If
            If IsDBNull(drDSRow("strAIRSYearEUEPEM")) Then
                temp27 = ""
            Else
                temp27 = drDSRow("strAIRSYearEUEPEM")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".EIEM " & _
            "WHERE strAIRSYearEUEPEM = '" & temp27 & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".EIEM " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                "'" & Replace(temp21, "'", "''") & "', '" & Replace(temp22, "'", "''") & "', " & _
                "'" & Replace(temp23, "'", "''") & "', '" & Replace(temp24, "'", "''") & "', " & _
                "'" & Replace(temp25, "'", "''") & "', '" & Replace(temp26, "'", "''") & "', " & _
                "'" & Replace(temp27, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".EIEM SET " & _
                "strInventoryYear = '" & Replace(temp, "'", "''") & "', " & _
                "strStateCountyFIPS = '" & Replace(temp2, "'", "''") & "', " & _
                "strStateFacilityIdentifier = '" & Replace(temp3, "'", "''") & "', " & _
                "strEmissionUnitID = '" & Replace(temp4, "'", "''") & "', " & _
                "strProcessID = '" & Replace(temp5, "'", "''") & "', " & _
                "strPollutantCode = '" & Replace(temp6, "'", "''") & "', " & _
                "strEmissionReleasePointID = '" & Replace(temp7, "'", "''") & "', " & _
                "dblEmissionNumericValue = '" & Replace(temp8, "'", "''") & "', " & _
                "strEmissionUnitNumerator = '" & Replace(temp9, "'", "''") & "', " & _
                "strEmissionType = '" & Replace(temp10, "'", "''") & "', " & _
                "sngFactorNumericValue = '" & Replace(temp11, "'", "''") & "', " & _
                "strFactorUnitNumerator = '" & Replace(temp12, "'", "''") & "', " & _
                "strFactorUnitDenominator = '" & Replace(temp13, "'", "''") & "', " & _
                "strEmissionCalculationMetCode = '" & Replace(temp14, "'", "''") & "', " & _
                "sngRuleEffectiveness = '" & Replace(temp15, "'", "''") & "', " & _
                "strControlStatus = '" & Replace(temp16, "'", "''") & "', " & _
                "sngPrimaryPctControlEffic = '" & Replace(temp17, "'", "''") & "', " & _
                "sngPctCaptureEfficiency = '" & Replace(temp18, "'", "''") & "', " & _
                "sngTotalCaptureControlEffic = '" & Replace(temp19, "'", "''") & "', " & _
                "strPrimaryDeviceTypeCode = '" & Replace(temp20, "'", "''") & "', " & _
                "strSecondaryDeviceTypeCode = '" & Replace(temp21, "'", "''") & "', " & _
                "strControlSystemDescription = '" & Replace(temp22, "'", "''") & "', " & _
                "strAIRSYearEUProcid = '" & Replace(temp23, "'", "''") & "', " & _
                "strAIRSYear = '" & Replace(temp24, "'", "''") & "', " & _
                "strAIRSNumber = '" & Replace(temp25, "'", "''") & "', " & _
                "strTransactionDate = '" & Replace(temp26, "'", "''") & "', " & _
                "strAirsYearEUEPEM = '" & Replace(temp27, "'", "''") & "' " & _
                "WHERE strAIRSYearEUEPEM = '" & Replace(temp27, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Next
    End Sub
    Sub TransferAPBAirProgramPollutants()
        SQL = "Select Status " & _
          "from user_triggers " & _
          "where trigger_name = 'tg_afs_airpollutants' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "ENABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.tg_afs_airpollutants DISABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

        SQL = "Select Status " & _
          "from user_triggers " & _
          "where trigger_name = 'tg_afs_airprogrampollutants' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "ENABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.tg_afs_airprogrampollutants DISABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

        SQL = "Select Status " & _
          "from user_triggers " & _
          "where trigger_name = 'tg_HB_APBAirProgramPollutants' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "ENABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.tg_HB_APBAirProgramPollutants DISABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If



        SQL = "Select * " & _
        "from " & connNameSpace & ".APBAIRPROGRAMPOLLUTANTS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRAIRSNUMBER")) Then
                temp = ""
            Else
                temp = drDSRow("STRAIRSNUMBER")
            End If
            If IsDBNull(drDSRow("STRAIRPOLLUTANTKEY")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRAIRPOLLUTANTKEY")
            End If
            If IsDBNull(drDSRow("STRPOLLUTANTKEY")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRPOLLUTANTKEY")
            End If
            If IsDBNull(drDSRow("STRCOMPLIANCESTATUS")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("STRCOMPLIANCESTATUS")
            End If
            If IsDBNull(drDSRow("STRMODIFINGPERSON")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("STRMODIFINGPERSON")
            End If
            If IsDBNull(drDSRow("DATMODIFINGDATE")) Then
                temp6 = ""
            Else
                temp6 = Format(drDSRow("DATMODIFINGDATE"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("STROPERATIONALSTATUS")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("STROPERATIONALSTATUS")
            End If

            SQL = "Select " & _
            "strAIRSNumber " & _
            "from " & connNameSpace & ".APBMasterAIRS " & _
            "where strAIRSNumber = '" & temp & "'"

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".APBAIRPROGRAMPOLLUTANTS " & _
                "WHERE STRAIRPOLLUTANTKEY = '" & temp2 & "' " & _
                "AND STRPOLLUTANTKEY = '" & temp3 & "' "

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".APBAIRPROGRAMPOLLUTANTS " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".APBAIRPROGRAMPOLLUTANTS SET " & _
                    "STRAIRSNUMBER = '" & Replace(temp, "'", "''") & "', " & _
                    "STRAIRPOLLUTANTKEY = '" & Replace(temp2, "'", "''") & "', " & _
                    "STRPOLLUTANTKEY = '" & Replace(temp3, "'", "''") & "', " & _
                    "STRCOMPLIANCESTATUS = '" & Replace(temp4, "'", "''") & "', " & _
                    "STRMODIFINGPERSON = '" & Replace(temp5, "'", "''") & "', " & _
                    "DATMODIFINGDATE = '" & Replace(temp6, "'", "''") & "', " & _
                    "STROPERATIONALSTATUS = '" & Replace(temp7, "'", "''") & "' " & _
                    "WHERE STRAIRPOLLUTANTKEY = '" & temp2 & "' " & _
                    "AND STRPOLLUTANTKEY = '" & temp3 & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next

        SQL = "Select Status " & _
          "from user_triggers " & _
          "where trigger_name = 'tg_afs_airpollutants' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "DISABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.tg_afs_airpollutants ENABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

        SQL = "Select Status " & _
          "from user_triggers " & _
          "where trigger_name = 'tg_afs_airprogrampollutants' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "DISABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.tg_afs_airprogrampollutants ENABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

        SQL = "Select Status " & _
         "from user_triggers " & _
         "where trigger_name = 'tg_HB_APBAirProgramPollutants' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "DISABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.tg_HB_APBAirProgramPollutants ENABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

    End Sub
    Sub TransferAPBContactInformation()
        SQL = "Select * " & _
        "from " & connNameSpace & ".APBCONTACTINFORMATION "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRCONTACTKEY")) Then
                temp = ""
            Else
                temp = drDSRow("STRCONTACTKEY")
            End If
            If IsDBNull(drDSRow("STRAIRSNUMBER")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRAIRSNUMBER")
            End If
            If IsDBNull(drDSRow("STRKEY")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRKEY")
            End If
            If IsDBNull(drDSRow("STRCONTACTFIRSTNAME")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("STRCONTACTFIRSTNAME")
            End If
            If IsDBNull(drDSRow("STRCONTACTLASTNAME")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("STRCONTACTLASTNAME")
            End If
            If IsDBNull(drDSRow("STRCONTACTPREFIX")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("STRCONTACTPREFIX")
            End If
            If IsDBNull(drDSRow("STRCONTACTSUFFIX")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("STRCONTACTSUFFIX")
            End If

            If IsDBNull(drDSRow("STRCONTACTTITLE")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("STRCONTACTTITLE")
            End If
            If IsDBNull(drDSRow("STRCONTACTCOMPANYNAME")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("STRCONTACTCOMPANYNAME")
            End If
            If IsDBNull(drDSRow("STRCONTACTPHONENUMBER1")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("STRCONTACTPHONENUMBER1")
            End If
            If IsDBNull(drDSRow("STRCONTACTPHONENUMBER2")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("STRCONTACTPHONENUMBER2")
            End If
            If IsDBNull(drDSRow("STRCONTACTFAXNUMBER")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("STRCONTACTFAXNUMBER")
            End If
            If IsDBNull(drDSRow("STRCONTACTEMAIL")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("STRCONTACTEMAIL")
            End If
            If IsDBNull(drDSRow("STRCONTACTADDRESS1")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("STRCONTACTADDRESS1")
            End If
            If IsDBNull(drDSRow("STRCONTACTADDRESS2")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("STRCONTACTADDRESS2")
            End If
            If IsDBNull(drDSRow("STRCONTACTCITY")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("STRCONTACTCITY")
            End If
            If IsDBNull(drDSRow("STRCONTACTSTATE")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("STRCONTACTSTATE")
            End If
            If IsDBNull(drDSRow("STRCONTACTZIPCODE")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("STRCONTACTZIPCODE")
            End If
            If IsDBNull(drDSRow("STRMODIFINGPERSON")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("STRMODIFINGPERSON")
            End If
            If IsDBNull(drDSRow("DATMODIFINGDATE")) Then
                temp20 = ""
            Else
                temp20 = Format(drDSRow("DATMODIFINGDATE"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("STRCONTACTDESCRIPTION")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("STRCONTACTDESCRIPTION")
            End If

            SQL = "Select " & _
            "strAIRSNumber " & _
            "from " & connNameSpace & ".APBMasterAIRS " & _
            "where strAIRSNumber = '" & temp2 & "'"

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".APBCONTACTINFORMATION " & _
                "WHERE STRCONTACTKEY = '" & temp & "' "

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".APBCONTACTINFORMATION " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                    "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                    "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                    "'" & Replace(temp21, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".APBCONTACTINFORMATION SET " & _
                    "STRCONTACTKEY = '" & Replace(temp, "'", "''") & "', " & _
                    "STRAIRSNUMBER = '" & Replace(temp2, "'", "''") & "', " & _
                    "STRKEY = '" & Replace(temp3, "'", "''") & "', " & _
                    "STRCONTACTFIRSTNAME = '" & Replace(temp4, "'", "''") & "', " & _
                    "STRCONTACTLASTNAME = '" & Replace(temp5, "'", "''") & "', " & _
                    "STRCONTACTPREFIX = '" & Replace(temp6, "'", "''") & "', " & _
                    "STRCONTACTSUFFIX = '" & Replace(temp7, "'", "''") & "', " & _
                    "STRCONTACTTITLE = '" & Replace(temp8, "'", "''") & "', " & _
                    "STRCONTACTCOMPANYNAME = '" & Replace(temp9, "'", "''") & "', " & _
                    "STRCONTACTPHONENUMBER1 = '" & Replace(temp10, "'", "''") & "', " & _
                    "STRCONTACTPHONENUMBER2 = '" & Replace(temp11, "'", "''") & "', " & _
                    "STRCONTACTFAXNUMBER = '" & Replace(temp12, "'", "''") & "', " & _
                    "STRCONTACTEMAIL = '" & Replace(temp13, "'", "''") & "', " & _
                    "STRCONTACTADDRESS1 = '" & Replace(temp14, "'", "''") & "', " & _
                    "STRCONTACTADDRESS2 = '" & Replace(temp15, "'", "''") & "', " & _
                    "STRCONTACTCITY = '" & Replace(temp16, "'", "''") & "', " & _
                    "STRCONTACTSTATE = '" & Replace(temp17, "'", "''") & "', " & _
                    "STRCONTACTZIPCODE = '" & Replace(temp18, "'", "''") & "', " & _
                    "STRMODIFINGPERSON = '" & Replace(temp19, "'", "''") & "', " & _
                    "DATMODIFINGDATE = '" & Replace(temp20, "'", "''") & "', " & _
                    "STRCONTACTDESCRIPTION = '" & Replace(temp21, "'", "''") & "' " & _
                    "WHERE STRCONTACTKEY = '" & temp & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

        Next

    End Sub
    Sub TransferAPBFacilityInformation()

        SQL = "Select Status " & _
          "from user_triggers " & _
          "where trigger_name = 'TG_HB_APBFACILITYINFORMATION' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "ENABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.TG_HB_APBFACILITYINFORMATION DISABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

        SQL = "Select Status " & _
          "from user_triggers " & _
          "where trigger_name = 'TG_AFS_FACILITYINFORMATION' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "ENABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.TG_AFS_FACILITYINFORMATION DISABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

        SQL = "Select * " & _
        "from " & connNameSpace & ".APBFACILITYINFORMATION "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRAIRSNUMBER")) Then
                temp = ""
            Else
                temp = drDSRow("STRAIRSNUMBER")
            End If
            If IsDBNull(drDSRow("STRFACILITYNAME")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRFACILITYNAME")
            End If
            If IsDBNull(drDSRow("STRFACILITYSTREET1")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRFACILITYSTREET1")
            End If
            If IsDBNull(drDSRow("STRFACILITYSTREET2")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("STRFACILITYSTREET2")
            End If
            If IsDBNull(drDSRow("STRFACILITYCITY")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("STRFACILITYCITY")
            End If
            If IsDBNull(drDSRow("STRFACILITYSTATE")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("STRFACILITYSTATE")
            End If
            If IsDBNull(drDSRow("STRFACILITYZIPCODE")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("STRFACILITYZIPCODE")
            End If
            If IsDBNull(drDSRow("STRMODIFINGPERSON")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("STRMODIFINGPERSON")
            End If
            If IsDBNull(drDSRow("DATMODIFINGDATE")) Then
                temp9 = ""
            Else
                temp9 = Format(drDSRow("DATMODIFINGDATE"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("STRCOMMENTS")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("STRCOMMENTS")
            End If
            If IsDBNull(drDSRow("STRMODIFINGLOCATION")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("STRMODIFINGLOCATION")
            End If
            If IsDBNull(drDSRow("NUMFACILITYLONGITUDE")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("NUMFACILITYLONGITUDE")
            End If
            If IsDBNull(drDSRow("NUMFACILITYLATITUDE")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("NUMFACILITYLATITUDE")
            End If
            If IsDBNull(drDSRow("STRHORIZONTALCOLLECTIONCODE")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("STRHORIZONTALCOLLECTIONCODE")
            End If
            If IsDBNull(drDSRow("STRHORIZONTALACCURACYMEASURE")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("STRHORIZONTALACCURACYMEASURE")
            End If
            If IsDBNull(drDSRow("STRHORIZONTALREFERENCECODE")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("STRHORIZONTALREFERENCECODE")
            End If

            SQL = "Select " & _
            "strAIRSNumber " & _
            "from " & connNameSpace & ".APBMasterAIRS " & _
            "where strAIRSNumber = '" & temp & "'"

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".APBFACILITYINFORMATION " & _
                "WHERE STRAIRSNUMBER = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".APBFACILITYINFORMATION " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".APBFACILITYINFORMATION SET " & _
                    "STRAIRSNUMBER = '" & Replace(temp, "'", "''") & "', " & _
                    "STRFACILITYNAME = '" & Replace(temp2, "'", "''") & "', " & _
                    "STRFACILITYSTREET1 = '" & Replace(temp3, "'", "''") & "', " & _
                    "STRFACILITYSTREET2 = '" & Replace(temp4, "'", "''") & "', " & _
                    "STRFACILITYCITY = '" & Replace(temp5, "'", "''") & "', " & _
                    "STRFACILITYSTATE = '" & Replace(temp6, "'", "''") & "', " & _
                    "STRFACILITYZIPCODE = '" & Replace(temp7, "'", "''") & "', " & _
                    "STRMODIFINGPERSON = '" & Replace(temp8, "'", "''") & "', " & _
                    "DATMODIFINGDATE = '" & Replace(temp9, "'", "''") & "', " & _
                    "STRCOMMENTS = '" & Replace(temp10, "'", "''") & "', " & _
                    "STRMODIFINGLOCATION = '" & Replace(temp11, "'", "''") & "', " & _
                    "NUMFACILITYLONGITUDE = '" & Replace(temp12, "'", "''") & "', " & _
                    "NUMFACILITYLATITUDE = '" & Replace(temp13, "'", "''") & "', " & _
                    "STRHORIZONTALCOLLECTIONCODE = '" & Replace(temp14, "'", "''") & "', " & _
                    "STRHORIZONTALACCURACYMEASURE = '" & Replace(temp15, "'", "''") & "', " & _
                    "STRHORIZONTALREFERENCECODE = '" & Replace(temp16, "'", "''") & "' " & _
                    "WHERE STRAIRSNUMBER = '" & temp & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next

        SQL = "Select Status " & _
        "from user_triggers " & _
        "where trigger_name = 'TG_HB_APBFACILITYINFORMATION' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "DISABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.TG_HB_APBFACILITYINFORMATION Enable"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

        SQL = "Select Status " & _
       "from user_triggers " & _
       "where trigger_name = 'TG_AFS_FACILITYINFORMATION' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "DISABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.TG_AFS_FACILITYINFORMATION DISABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

    End Sub
    Sub TransferAPBHeaderData()
        SQL = "Select Status " & _
          "from user_triggers " & _
          "where trigger_name = 'tg_afs_headerdata' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "ENABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.tg_afs_headerdata DISABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

        SQL = "Select Status " & _
          "from user_triggers " & _
          "where trigger_name = 'tg_hb_apbheaderData' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "ENABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.tg_hb_apbheaderData DISABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

        SQL = "Select Status " & _
          "from user_triggers " & _
          "where trigger_name = 'tg_air_Programcodes' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "ENABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.tg_air_Programcodes DISABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

        SQL = "Select * " & _
        "from " & connNameSpace & ".APBHEADERDATA "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRAIRSNUMBER")) Then
                temp = ""
            Else
                temp = drDSRow("STRAIRSNUMBER")
            End If
            If IsDBNull(drDSRow("STROPERATIONALSTATUS")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STROPERATIONALSTATUS")
            End If
            If IsDBNull(drDSRow("STRCLASS")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRCLASS")
            End If
            If IsDBNull(drDSRow("STRAIRPROGRAMCODES")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("STRAIRPROGRAMCODES")
            End If
            If IsDBNull(drDSRow("STRSICCODE")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("STRSICCODE")
            End If
            If IsDBNull(drDSRow("STRFEINUMBER")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("STRFEINUMBER")
            End If
            If IsDBNull(drDSRow("STRMODIFINGPERSON")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("STRMODIFINGPERSON")
            End If
            If IsDBNull(drDSRow("DATMODIFINGDATE")) Then
                temp8 = ""
            Else
                temp8 = Format(drDSRow("DATMODIFINGDATE"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("DATSTARTUPDATE")) Then
                temp9 = ""
            Else
                temp9 = Format(drDSRow("DATSTARTUPDATE"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("DATSHUTDOWNDATE")) Then
                temp10 = ""
            Else
                temp10 = Format(drDSRow("DATSHUTDOWNDATE"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("STRCOMMENTS")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("STRCOMMENTS")
            End If
            If IsDBNull(drDSRow("STRPLANTDESCRIPTION")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("STRPLANTDESCRIPTION")
            End If
            If IsDBNull(drDSRow("STRATTAINMENTSTATUS")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("STRATTAINMENTSTATUS")
            End If
            If IsDBNull(drDSRow("STRSTATEPROGRAMCODES")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("STRSTATEPROGRAMCODES")
            End If
            If IsDBNull(drDSRow("STRMODIFINGLOCATION")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("STRMODIFINGLOCATION")
            End If

            SQL = "Select " & _
            "strAIRSNumber " & _
            "from " & connNameSpace & ".APBMasterAIRS " & _
            "where strAIRSNumber = '" & temp & "'"

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".APBHEADERDATA " & _
                "WHERE STRAIRSNUMBER = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".APBHEADERDATA " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".APBHEADERDATA SET " & _
                    "STRAIRSNUMBER = '" & Replace(temp, "'", "''") & "', " & _
                    "STROPERATIONALSTATUS = '" & Replace(temp2, "'", "''") & "', " & _
                    "STRCLASS = '" & Replace(temp3, "'", "''") & "', " & _
                    "STRAIRPROGRAMCODES = '" & Replace(temp4, "'", "''") & "', " & _
                    "STRSICCODE = '" & Replace(temp5, "'", "''") & "', " & _
                    "STRFEINUMBER = '" & Replace(temp6, "'", "''") & "', " & _
                    "STRMODIFINGPERSON = '" & Replace(temp7, "'", "''") & "', " & _
                    "DATMODIFINGDATE = '" & Replace(temp8, "'", "''") & "', " & _
                    "DATSTARTUPDATE = '" & Replace(temp9, "'", "''") & "', " & _
                    "DATSHUTDOWNDATE = '" & Replace(temp10, "'", "''") & "', " & _
                    "STRCOMMENTS = '" & Replace(temp11, "'", "''") & "', " & _
                    "STRPLANTDESCRIPTION = '" & Replace(temp12, "'", "''") & "', " & _
                    "STRATTAINMENTSTATUS = '" & Replace(temp13, "'", "''") & "', " & _
                    "STRSTATEPROGRAMCODES = '" & Replace(temp14, "'", "''") & "', " & _
                    "STRMODIFINGLOCATION = '" & Replace(temp15, "'", "''") & "' " & _
                    "WHERE STRAIRSNUMBER = '" & temp & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next

        SQL = "Select Status " & _
          "from user_triggers " & _
          "where trigger_name = 'tg_air_Programcodes' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "DISABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.tg_air_Programcodes ENABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

        SQL = "Select Status " & _
         "from user_triggers " & _
         "where trigger_name = 'tg_afs_headerdata' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "DISABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.tg_afs_headerdata ENABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

        SQL = "Select Status " & _
         "from user_triggers " & _
         "where trigger_name = 'tg_hb_apbheaderData' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "DISABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.tg_hb_apbheaderData ENABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If
    End Sub
    Sub TransferAPBMasterAIRS()
        SQL = "Select * " & _
        "from " & connNameSpace & ".APBMASTERAIRS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRAIRSNUMBER")) Then
                temp = ""
            Else
                temp = drDSRow("STRAIRSNUMBER")
            End If
            If IsDBNull(drDSRow("STRMODIFINGPERSON")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRMODIFINGPERSON")
            End If
            If IsDBNull(drDSRow("DATMODIFINGDATE")) Then
                temp3 = ""
            Else
                temp3 = Format(drDSRow("DATMODIFINGDATE"), "dd-MMM-yyyy")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".APBMASTERAIRS " & _
            "WHERE STRAIRSNUMBER = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".APBMASTERAIRS " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".APBMASTERAIRS SET " & _
                "STRAIRSNUMBER = '" & Replace(temp, "'", "''") & "', " & _
                "STRMODIFINGPERSON = '" & Replace(temp2, "'", "''") & "', " & _
                "DATMODIFINGDATE = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE STRAIRSNUMBER = '" & temp & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferAPBMasterAPP()
        SQL = "Select * " & _
              "from " & connNameSpace & ".APBMasterAPP "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strVersionNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strVersionNumber")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".APBMasterAPP " & _
            "WHERE strVersionNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".APBMasterAPP " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".APBMasterAPP SET " & _
                "strVersionNumber = '" & Replace(temp, "'", "''") & "' " & _
                "WHERE strVersionNumber = '" & temp & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next

    End Sub
    Sub TransferAPBPermits()



    End Sub
    Sub TransferAPBSubPartData()

        SQL = "Select * " & _
        "from " & connNameSpace & ".APBSUBPARTDATA "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRAIRSNUMBER")) Then
                temp = ""
            Else
                temp = drDSRow("STRAIRSNUMBER")
            End If
            If IsDBNull(drDSRow("STRSUBPARTKEY")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRSUBPARTKEY")
            End If
            If IsDBNull(drDSRow("STRSUBPART")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRSUBPART")
            End If
            If IsDBNull(drDSRow("STRMODIFINGPERSON")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("STRMODIFINGPERSON")
            End If
            If IsDBNull(drDSRow("DATMODIFINGDATE")) Then
                temp5 = ""
            Else
                temp5 = Format(drDSRow("DATMODIFINGDATE"), "dd-MMM-yyyy")
            End If
            SQL = "Select " & _
            "strAIRSNumber " & _
            "from " & connNameSpace & ".APBMasterAIRS " & _
            "where strAIRSNumber = '" & temp & "'"

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".APBSUBPARTDATA " & _
                "WHERE STRSUBPARTKEY = '" & temp2 & "' " & _
                "AND STRSUBPART = '" & temp3 & "' "

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".APBSUBPARTDATA " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".APBSUBPARTDATA SET " & _
                    "STRAIRSNUMBER = '" & Replace(temp, "'", "''") & "', " & _
                    "STRSUBPARTKEY = '" & Replace(temp2, "'", "''") & "', " & _
                    "STRSUBPART = '" & Replace(temp3, "'", "''") & "', " & _
                    "STRMODIFINGPERSON = '" & Replace(temp4, "'", "''") & "', " & _
                    "DATMODIFINGDATE = '" & Replace(temp5, "'", "''") & "' " & _
                    "WHERE STRSUBPARTKEY = '" & temp2 & "' " & _
                    "AND STRSUBPART = '" & temp3 & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next


    End Sub
    Sub TransferAPBSupplamentalData()
        SQL = "Select Status " & _
          "from user_triggers " & _
          "where trigger_name = 'tg_afs_SupplamentalData' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "ENABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.tg_afs_SupplamentalData DISABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

        SQL = "Select * " & _
        "from " & connNameSpace & ".APBSUPPLAMENTALDATA "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRAIRSNUMBER")) Then
                temp = ""
            Else
                temp = drDSRow("STRAIRSNUMBER")
            End If
            If IsDBNull(drDSRow("DATSSCPTESTREPORTDUE")) Then
                temp2 = ""
            Else
                temp2 = Format(drDSRow("DATSSCPTESTREPORTDUE"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("STRMODIFINGPERSON")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRMODIFINGPERSON")
            End If
            If IsDBNull(drDSRow("DATMODIFINGDATE")) Then
                temp4 = ""
            Else
                temp4 = Format(drDSRow("DATMODIFINGDATE"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("STRDISTRICTOFFICE")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("STRDISTRICTOFFICE")
            End If
            If IsDBNull(drDSRow("STRCMSMEMBER")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("STRCMSMEMBER")
            End If
            If IsDBNull(drDSRow("STRAFSACTIONNUMBER")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("STRAFSACTIONNUMBER")
            End If
            If IsDBNull(drDSRow("STRFEEMAILINGSTATUS")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("STRFEEMAILINGSTATUS")
            End If
            If IsDBNull(drDSRow("ISBANKRUPT")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("ISBANKRUPT")
            End If
            If IsDBNull(drDSRow("STRCURRENTPERMIT")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("STRCURRENTPERMIT")
            End If
            If IsDBNull(drDSRow("DATPERMITEFFECTIVE")) Then
                temp11 = ""
            Else
                temp11 = Format(drDSRow("DATPERMITEFFECTIVE"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("STREPATOPSEXCLUDED")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("STREPATOPSEXCLUDED")
            End If

            SQL = "Select " & _
           "strAIRSNumber " & _
           "from " & connNameSpace & ".APBMasterAIRS " & _
           "where strAIRSNumber = '" & temp & "'"

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then

                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".APBSUPPLAMENTALDATA " & _
                "WHERE STRAIRSNUMBER = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".APBSUPPLAMENTALDATA " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".APBSUPPLAMENTALDATA SET " & _
                    "STRAIRSNUMBER = '" & Replace(temp, "'", "''") & "', " & _
                    "DATSSCPTESTREPORTDUE = '" & Replace(temp2, "'", "''") & "', " & _
                    "STRMODIFINGPERSON = '" & Replace(temp3, "'", "''") & "', " & _
                    "DATMODIFINGDATE = '" & Replace(temp4, "'", "''") & "', " & _
                    "STRDISTRICTOFFICE = '" & Replace(temp5, "'", "''") & "', " & _
                    "STRCMSMEMBER = '" & Replace(temp6, "'", "''") & "', " & _
                    "STRAFSACTIONNUMBER = '" & Replace(temp7, "'", "''") & "', " & _
                    "STRFEEMAILINGSTATUS = '" & Replace(temp8, "'", "''") & "', " & _
                    "ISBANKRUPT = '" & Replace(temp9, "'", "''") & "', " & _
                    "STRCURRENTPERMIT = '" & Replace(temp10, "'", "''") & "', " & _
                    "DATPERMITEFFECTIVE = '" & Replace(temp11, "'", "''") & "', " & _
                    "STREPATOPSEXCLUDED = '" & Replace(temp12, "'", "''") & "' " & _
                    "WHERE STRAIRSNUMBER = '" & temp & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next

        SQL = "Select Status " & _
                  "from user_triggers " & _
                  "where trigger_name = 'tg_afs_SupplamentalData' "
        cmd = New OracleCommand(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr.Item("Status")) Then
                TriggerStatus = ""
            Else
                If dr.Item("Status") = "ENABLED" Then
                    TriggerStatus = "ENABLED"
                Else
                    TriggerStatus = "DISABLED"
                End If
            End If
        End While
        dr.Close()

        If TriggerStatus = "DISABLED" Then
            SQL = "ALTER TRIGGER AIRBRANCH.tg_afs_SupplamentalData ENABLE"
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        End If

    End Sub
    Sub TransferAPPMaster()






    End Sub
    Sub TransferHBAPBAirProgramPollutants()
        SQL = "Select * " & _
        "from " & connNameSpace & ".HB_APBAIRPROGRAMPOLLUTANTS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        SQL = "DELETE " & connNameSpace & ".HB_APBAIRPROGRAMPOLLUTANTS "
        cmd = New OracleCommand(SQL, transferConn)
        If transferConn.State = ConnectionState.Closed Then
            transferConn.Open()
        End If
        dr = cmd.ExecuteReader
        dr.Close()

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRKEY")) Then
                temp = ""
            Else
                temp = drDSRow("STRKEY")
            End If
            If IsDBNull(drDSRow("STRAIRSNUMBER")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRAIRSNUMBER")
            End If
            If IsDBNull(drDSRow("STRAIRPOLLUTANTKEY")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRAIRPOLLUTANTKEY")
            End If
            If IsDBNull(drDSRow("STRPOLLUTANTKEY")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("STRPOLLUTANTKEY")
            End If
            If IsDBNull(drDSRow("STRCOMPLIANCESTATUS")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("STRCOMPLIANCESTATUS")
            End If
            If IsDBNull(drDSRow("STRMODIFINGPERSON")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("STRMODIFINGPERSON")
            End If
            If IsDBNull(drDSRow("DATMODIFINGDATE")) Then
                temp7 = ""
            Else
                temp7 = Format(drDSRow("DATMODIFINGDATE"), "dd-MMM-yyyy")
            End If

            SQL = "Select " & _
            "strAIRSNumber " & _
            "from " & connNameSpace & ".APBMasterAIRS " & _
            "where strAIRSNumber = '" & temp2 & "'"

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".HB_APBAIRPROGRAMPOLLUTANTS " & _
                "WHERE STRKEY = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".HB_APBAIRPROGRAMPOLLUTANTS " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".HB_APBAIRPROGRAMPOLLUTANTS SET " & _
                    "STRKEY = '" & Replace(temp, "'", "''") & "', " & _
                    "STRAIRSNUMBER = '" & Replace(temp2, "'", "''") & "', " & _
                    "STRAIRPOLLUTANTKEY = '" & Replace(temp3, "'", "''") & "', " & _
                    "STRPOLLUTANTKEY = '" & Replace(temp4, "'", "''") & "', " & _
                    "STRCOMPLIANCESTATUS = '" & Replace(temp5, "'", "''") & "', " & _
                    "STRMODIFINGPERSON = '" & Replace(temp6, "'", "''") & "', " & _
                    "DATMODIFINGDATE = '" & Replace(temp7, "'", "''") & "' " & _
                    "WHERE STRKEY = '" & temp & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next

    End Sub
    Sub TransferHBAPBFacilityINformation()

        SQL = "Select * " & _
        "from " & connNameSpace & ".HB_APBFACILITYINFORMATION "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        SQL = "DELETE " & connNameSpace & ".HB_APBFACILITYINFORMATION "
        cmd = New OracleCommand(SQL, transferConn)
        If transferConn.State = ConnectionState.Closed Then
            transferConn.Open()
        End If
        dr = cmd.ExecuteReader
        dr.Close()

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRKEY")) Then
                temp = ""
            Else
                temp = drDSRow("STRKEY")
            End If
            If IsDBNull(drDSRow("STRAIRSNUMBER")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRAIRSNUMBER")
            End If
            If IsDBNull(drDSRow("STRFACILITYNAME")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STRFACILITYNAME")
            End If
            If IsDBNull(drDSRow("STRFACILITYSTREET1")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("STRFACILITYSTREET1")
            End If
            If IsDBNull(drDSRow("STRFACILITYSTREET2")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("STRFACILITYSTREET2")
            End If
            If IsDBNull(drDSRow("STRFACILITYCITY")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("STRFACILITYCITY")
            End If
            If IsDBNull(drDSRow("STRFACILITYSTATE")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("STRFACILITYSTATE")
            End If
            If IsDBNull(drDSRow("STRFACILITYZIPCODE")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("STRFACILITYZIPCODE")
            End If
            If IsDBNull(drDSRow("STRMODIFINGPERSON")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("STRMODIFINGPERSON")
            End If
            If IsDBNull(drDSRow("DATMODIFINGDATE")) Then
                temp10 = ""
            Else
                temp10 = Format(drDSRow("DATMODIFINGDATE"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("STRCOMMENTS")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("STRCOMMENTS")
            End If
            If IsDBNull(drDSRow("STRMODIFINGLOCATION")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("STRMODIFINGLOCATION")
            End If
            If IsDBNull(drDSRow("NUMFACILITYLONGITUDE")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("NUMFACILITYLONGITUDE")
            End If
            If IsDBNull(drDSRow("NUMFACILITYLATITUDE")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("NUMFACILITYLATITUDE")
            End If

            SQL = "Select " & _
           "strAIRSNumber " & _
           "from " & connNameSpace & ".APBMasterAIRS " & _
           "where strAIRSNumber = '" & temp2 & "'"

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".HB_APBFACILITYINFORMATION " & _
                "WHERE STRKEY = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".HB_APBFACILITYINFORMATION " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".HB_APBFACILITYINFORMATION SET " & _
                    "STRKEY = '" & Replace(temp, "'", "''") & "', " & _
                    "STRAIRSNUMBER = '" & Replace(temp2, "'", "''") & "', " & _
                    "STRFACILITYNAME = '" & Replace(temp3, "'", "''") & "', " & _
                    "STRFACILITYSTREET1 = '" & Replace(temp4, "'", "''") & "', " & _
                    "STRFACILITYSTREET2 = '" & Replace(temp5, "'", "''") & "', " & _
                    "STRFACILITYCITY = '" & Replace(temp6, "'", "''") & "', " & _
                    "STRFACILITYSTATE = '" & Replace(temp7, "'", "''") & "', " & _
                    "STRFACILITYZIPCODE = '" & Replace(temp8, "'", "''") & "', " & _
                    "STRMODIFINGPERSON = '" & Replace(temp9, "'", "''") & "', " & _
                    "DATMODIFINGDATE = '" & Replace(temp10, "'", "''") & "', " & _
                    "STRCOMMENTS = '" & Replace(temp11, "'", "''") & "', " & _
                    "STRMODIFINGLOCATION = '" & Replace(temp12, "'", "''") & "', " & _
                    "NUMFACILITYLONGITUDE = '" & Replace(temp13, "'", "''") & "', " & _
                    "NUMFACILITYLATITUDE = '" & Replace(temp14, "'", "''") & "' " & _
                    "WHERE STRKEY = '" & temp & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next


    End Sub
    Sub TransferHBAPBHeaderData()

        SQL = "Select * " & _
        "from " & connNameSpace & ".HB_APBHEADERDATA "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        SQL = "DELETE " & connNameSpace & ".HB_APBHEADERDATA "
        cmd = New OracleCommand(SQL, transferConn)
        If transferConn.State = ConnectionState.Closed Then
            transferConn.Open()
        End If
        dr = cmd.ExecuteReader
        dr.Close()

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("STRKEY")) Then
                temp = ""
            Else
                temp = drDSRow("STRKEY")
            End If
            If IsDBNull(drDSRow("STRAIRSNUMBER")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("STRAIRSNUMBER")
            End If
            If IsDBNull(drDSRow("STROPERATIONALSTATUS")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("STROPERATIONALSTATUS")
            End If
            If IsDBNull(drDSRow("STRCLASS")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("STRCLASS")
            End If
            If IsDBNull(drDSRow("STRAIRPROGRAMCODES")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("STRAIRPROGRAMCODES")
            End If
            If IsDBNull(drDSRow("STRSICCODE")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("STRSICCODE")
            End If
            If IsDBNull(drDSRow("STRFEINUMBER")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("STRFEINUMBER")
            End If
            If IsDBNull(drDSRow("STRMODIFINGPERSON")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("STRMODIFINGPERSON")
            End If
            If IsDBNull(drDSRow("DATMODIFINGDATE")) Then
                temp9 = ""
            Else
                temp9 = Format(drDSRow("DATMODIFINGDATE"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("DATSTARTUPDATE")) Then
                temp10 = ""
            Else
                temp10 = Format(drDSRow("DATSTARTUPDATE"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("DATSHUTDOWNDATE")) Then
                temp11 = ""
            Else
                temp11 = Format(drDSRow("DATSHUTDOWNDATE"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("STRCOMMENTS")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("STRCOMMENTS")
            End If
            If IsDBNull(drDSRow("STRPLANTDESCRIPTION")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("STRPLANTDESCRIPTION")
            End If
            If IsDBNull(drDSRow("STRATTAINMENTSTATUS")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("STRATTAINMENTSTATUS")
            End If
            If IsDBNull(drDSRow("STRSTATEPROGRAMCODES")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("STRSTATEPROGRAMCODES")
            End If
            If IsDBNull(drDSRow("STRMODIFINGLOCATION")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("STRMODIFINGLOCATION")
            End If

            SQL = "Select " & _
           "strAIRSNumber " & _
           "from " & connNameSpace & ".APBMasterAIRS " & _
           "where strAIRSNumber = '" & temp2 & "'"

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".HB_APBHEADERDATA " & _
                "WHERE STRKEY = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".HB_APBHEADERDATA " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".HB_APBHEADERDATA SET " & _
                    "STRKEY = '" & Replace(temp, "'", "''") & "', " & _
                    "STRAIRSNUMBER = '" & Replace(temp2, "'", "''") & "', " & _
                    "STROPERATIONALSTATUS = '" & Replace(temp3, "'", "''") & "', " & _
                    "STRCLASS = '" & Replace(temp4, "'", "''") & "', " & _
                    "STRAIRPROGRAMCODES = '" & Replace(temp5, "'", "''") & "', " & _
                    "STRSICCODE = '" & Replace(temp6, "'", "''") & "', " & _
                    "STRFEINUMBER = '" & Replace(temp7, "'", "''") & "', " & _
                    "STRMODIFINGPERSON = '" & Replace(temp8, "'", "''") & "', " & _
                    "DATMODIFINGDATE = '" & Replace(temp9, "'", "''") & "', " & _
                    "DATSTARTUPDATE = '" & Replace(temp10, "'", "''") & "', " & _
                    "DATSHUTDOWNDATE = '" & Replace(temp11, "'", "''") & "', " & _
                    "STRCOMMENTS = '" & Replace(temp12, "'", "''") & "', " & _
                    "STRPLANTDESCRIPTION = '" & Replace(temp13, "'", "''") & "', " & _
                    "STRATTAINMENTSTATUS = '" & Replace(temp14, "'", "''") & "', " & _
                    "STRSTATEPROGRAMCODES = '" & Replace(temp15, "'", "''") & "', " & _
                    "STRMODIFINGLOCATION = '" & Replace(temp16, "'", "''") & "' " & _
                    "WHERE STRKEY = '" & temp & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next

    End Sub
    Sub TransferISMPDocumentTypes()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPDocumentType "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strKey")) Then
                temp = ""
            Else
                temp = drDSRow("strKey")
            End If
            If IsDBNull(drDSRow("strDocumentType")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strDocumentType")
            End If
            If IsDBNull(drDSRow("strTableName")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strTableName")
            End If
            If IsDBNull(drDSRow("strAFSPrint")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strAFSPrint")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".ISMPDocumentType " & _
            "WHERE strKey = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".ISMPDocumentType " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".ISMPDocumentType SET " & _
                "strKey = '" & Replace(temp, "'", "''") & "', " & _
                "strDocumentType = '" & Replace(temp2, "'", "''") & "', " & _
                "strTableName = '" & Replace(temp2, "'", "''") & "', " & _
                "strAFSPrint = '" & Replace(temp3, "'", "''") & "' " & _
                "WHERE strKey = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferISMPFacilityAssignment()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPFacilityAssignment "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("strISMPUnit")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strISMPUnit")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".ISMPFacilityAssignment " & _
            "WHERE strKey = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".ISMPFacilityAssignment " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".ISMPFacilityAssignment SET " & _
                "strAIRSNumber = '" & Replace(temp, "'", "''") & "', " & _
                "strISMPUnit = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE strAIRSNumber = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferISMPMaster()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPMaster "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strReferenceNumber")
            End If
            If IsDBNull(drDSRow("strAIRSnumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strAIRSnumber")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp4 = ""
            Else
                temp4 = Format(drDSRow("datModifingDate"), "dd-MMM-yyyy")
            End If

            SQL = "Select " & _
            "strAIRSNumber " & _
            "from " & connNameSpace & ".APBMasterAIRS " & _
            "where strAIRSNumber = '" & temp2 & "'"

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".ISMPMaster " & _
                "WHERE STRUNIT = '" & temp & "' "

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".ISMPMaster " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".ISMPMaster SET " & _
                    "strReferenceNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strAIRSnumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp3, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp4, "'", "''") & "' " & _
                    "WHERE strReferenceNumber = '" & temp & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next

    End Sub
    Sub TransferISMPReferenceNumber()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPReferenceNumber "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strReferenceNumber")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".ISMPReferenceNumber " & _
            "WHERE strKey = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".ISMPReferenceNumber " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".ISMPReferenceNumber SET " & _
                "strReferenceNumber = '" & Replace(temp, "'", "''") & "' " & _
                "WHERE strReferenceNumber = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next




    End Sub
    Sub TransferISMPReportFlare()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPReportFlare "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strReferenceNumber")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacity")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strMaxOperatingCapacity")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacityUnit")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strMaxOperatingCapacityUnit")
            End If
            If IsDBNull(drDSRow("strOperatingCapacity")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strOperatingCapacity")
            End If
            If IsDBNull(drDSRow("strOperatingCapacityUnit")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strOperatingCapacityUnit")
            End If
            If IsDBNull(drDSRow("strLimitationVelocity")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strLimitationVelocity")
            End If
            If IsDBNull(drDSRow("strLimitationHeatCapacity")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strLimitationHeatCapacity")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate1A")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strAllowableEmissionRate1A")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate2A")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strAllowableEmissionRate2A")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate3A")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strAllowableEmissionRate3A")
            End If
            If IsDBNull(drDSRow("strAllowEmissionRateUnit1A")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strAllowEmissionRateUnit1A")
            End If
            If IsDBNull(drDSRow("strAllowEmissionRateUnit2A")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strAllowEmissionRateUnit2A")
            End If
            If IsDBNull(drDSRow("strAllowEmissionRateUnit3A")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strAllowEmissionRateUnit3A")
            End If
            If IsDBNull(drDSRow("strHeatingValue1A")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strHeatingValue1A")
            End If
            If IsDBNull(drDSRow("strHeatingValue2A")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("strHeatingValue2A")
            End If
            If IsDBNull(drDSRow("strHeatingValue3A")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("strHeatingValue3A")
            End If
            If IsDBNull(drDSRow("strHeatingValueUnits")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("strHeatingValueUnits")
            End If
            If IsDBNull(drDSRow("strHeatingValueAvg")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("strHeatingValueAvg")
            End If
            If IsDBNull(drDSRow("strVelocity1A")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("strVelocity1A")
            End If
            If IsDBNull(drDSRow("strVelocity2A")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("strVelocity2A")
            End If
            If IsDBNull(drDSRow("strVelocity3A")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("strVelocity3A")
            End If
            If IsDBNull(drDSRow("strVelocityUnits")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("strVelocityUnits")
            End If
            If IsDBNull(drDSRow("strVelocityAvg")) Then
                temp23 = ""
            Else
                temp23 = drDSRow("strVelocityAvg")
            End If
            If IsDBNull(drDSRow("strTestDuration")) Then
                temp24 = ""
            Else
                temp24 = drDSRow("strTestDuration")
            End If
            If IsDBNull(drDSRow("strTestDurationUnit")) Then
                temp25 = ""
            Else
                temp25 = drDSRow("strTestDurationUnit")
            End If
            If IsDBNull(drDSRow("strPollutantConcenIn")) Then
                temp26 = ""
            Else
                temp26 = drDSRow("strPollutantConcenIn")
            End If
            If IsDBNull(drDSRow("strPollutantConcenUnitIn")) Then
                temp27 = ""
            Else
                temp27 = drDSRow("strPollutantConcenUnitIn")
            End If
            If IsDBNull(drDSRow("strPollutantConcenOut")) Then
                temp28 = ""
            Else
                temp28 = drDSRow("strPollutantConcenOut")
            End If
            If IsDBNull(drDSRow("strPollutantConcenUnitOut")) Then
                temp29 = ""
            Else
                temp29 = drDSRow("strPollutantConcenUnitOut")
            End If
            If IsDBNull(drDSRow("strEmissionRate")) Then
                temp30 = ""
            Else
                temp30 = drDSRow("strEmissionRate")
            End If
            If IsDBNull(drDSRow("strEmissionRateUnit")) Then
                temp31 = ""
            Else
                temp31 = drDSRow("strEmissionRateUnit")
            End If
            If IsDBNull(drDSRow("strDestructionEfficiency")) Then
                temp32 = ""
            Else
                temp32 = drDSRow("strDestructionEfficiency")
            End If
            If IsDBNull(drDSRow("strPercentAllowable")) Then
                temp33 = ""
            Else
                temp33 = drDSRow("strPercentAllowable")
            End If

            SQL = "SELECT strReferenceNumber " & _
            "FROM " & connNameSpace & ".ISMPReportInformation " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".ISMPReportFlare " & _
                "WHERE strReferenceNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".ISMPReportFlare " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                    "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                    "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                    "'" & Replace(temp21, "'", "''") & "', '" & Replace(temp22, "'", "''") & "', " & _
                    "'" & Replace(temp23, "'", "''") & "', '" & Replace(temp24, "'", "''") & "', " & _
                    "'" & Replace(temp25, "'", "''") & "', '" & Replace(temp26, "'", "''") & "', " & _
                    "'" & Replace(temp27, "'", "''") & "', '" & Replace(temp28, "'", "''") & "', " & _
                    "'" & Replace(temp29, "'", "''") & "', '" & Replace(temp30, "'", "''") & "', " & _
                    "'" & Replace(temp31, "'", "''") & "', '" & Replace(temp32, "'", "''") & "', " & _
                    "'" & Replace(temp33, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".ISMPReportFlare SET " & _
                    "strReferenceNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strMaxOperatingCapacity = '" & Replace(temp2, "'", "''") & "', " & _
                    "strMaxOperatingCapacityUnit = '" & Replace(temp3, "'", "''") & "', " & _
                    "strOperatingCapacity = '" & Replace(temp4, "'", "''") & "', " & _
                    "strOperatingCapacityUnit = '" & Replace(temp5, "'", "''") & "', " & _
                    "strLimitationVelocity = '" & Replace(temp6, "'", "''") & "', " & _
                    "strLimitationHeatCapacity = '" & Replace(temp7, "'", "''") & "', " & _
                    "strAllowableEmissionRate1A = '" & Replace(temp8, "'", "''") & "', " & _
                    "strAllowableEmissionRate2A = '" & Replace(temp9, "'", "''") & "', " & _
                    "strAllowableEmissionRate3A = '" & Replace(temp10, "'", "''") & "', " & _
                    "strAllowEmissionRateUnit1A = '" & Replace(temp11, "'", "''") & "', " & _
                    "strAllowEmissionRateUnit2A = '" & Replace(temp12, "'", "''") & "', " & _
                    "strAllowEmissionRateUnit3A = '" & Replace(temp13, "'", "''") & "', " & _
                    "strHeatingValue1A = '" & Replace(temp14, "'", "''") & "', " & _
                    "strHeatingValue2A = '" & Replace(temp15, "'", "''") & "', " & _
                    "strHeatingValue3A = '" & Replace(temp16, "'", "''") & "', " & _
                    "strHeatingValueUnits = '" & Replace(temp17, "'", "''") & "', " & _
                    "strHeatingValueAvg = '" & Replace(temp18, "'", "''") & "', " & _
                    "strVelocity1A = '" & Replace(temp19, "'", "''") & "', " & _
                    "strVelocity2A = '" & Replace(temp20, "'", "''") & "', " & _
                    "strVelocity3A = '" & Replace(temp21, "'", "''") & "', " & _
                    "strVelocityUnits = '" & Replace(temp22, "'", "''") & "', " & _
                    "strVelocityAvg = '" & Replace(temp23, "'", "''") & "', " & _
                    "strTestDuration = '" & Replace(temp24, "'", "''") & "', " & _
                    "strTestDurationUnit = '" & Replace(temp25, "'", "''") & "', " & _
                    "strPollutantConcenIn = '" & Replace(temp26, "'", "''") & "', " & _
                    "strPollutantConcenUnitIn = '" & Replace(temp27, "'", "''") & "', " & _
                    "strPollutantConcenOut = '" & Replace(temp28, "'", "''") & "', " & _
                    "strPollutantConcenUnitOut = '" & Replace(temp29, "'", "''") & "', " & _
                    "strEmissionRate = '" & Replace(temp30, "'", "''") & "', " & _
                    "strEmissionRateUnit = '" & Replace(temp31, "'", "''") & "', " & _
                    "strDestructionEfficiency = '" & Replace(temp32, "'", "''") & "', " & _
                    "strPercentAllowable = '" & Replace(temp33, "'", "''") & "' " & _
                    "WHERE strReferenceNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferISMPReportInformation()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPReportInformation "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strReferenceNumber")
            End If
            If IsDBNull(drDSRow("strPollutant")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strPollutant")
            End If
            If IsDBNull(drDSRow("strEmissionSource")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strEmissionSource")
            End If
            If IsDBNull(drDSRow("strReportType")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strReportType")
            End If
            If IsDBNull(drDSRow("strDocumentType")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strDocumentType")
            End If
            If IsDBNull(drDSRow("strApplicableRequirement")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strApplicableRequirement")
            End If
            If IsDBNull(drDSRow("strTestingFirm")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strTestingFirm")
            End If
            If IsDBNull(drDSRow("strReviewingEngineer")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strReviewingEngineer")
            End If
            If IsDBNull(drDSRow("strWitnessingEngineer")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strWitnessingEngineer")
            End If
            If IsDBNull(drDSRow("strWitnessingEngineer2")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strWitnessingEngineer2")
            End If
            If IsDBNull(drDSRow("strReviewingUnit")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strReviewingUnit")
            End If
            If IsDBNull(drDSRow("datReviewedByUnitManager")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("datReviewedByUnitManager")
            End If
            If IsDBNull(drDSRow("strComplianceManager")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strComplianceManager")
            End If
            If IsDBNull(drDSRow("datTestDateStart")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("datTestDateStart")
            End If
            If IsDBNull(drDSRow("datTestDateEnd")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("datTestDateEnd")
            End If
            If IsDBNull(drDSRow("datReceivedDate")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("datReceivedDate")
            End If
            If IsDBNull(drDSRow("datCompleteDate")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("datCompleteDate")
            End If
            If IsDBNull(drDSRow("mmoCommentArea")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("mmoCommentArea")
            End If
            If IsDBNull(drDSRow("strClosed")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("strClosed")
            End If
            If IsDBNull(drDSRow("strCommissioner")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("strCommissioner")
            End If
            If IsDBNull(drDSRow("strDirector")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("strDirector")
            End If
            If IsDBNull(drDSRow("strProgramManager")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("strProgramManager")
            End If
            If IsDBNull(drDSRow("strComplianceStatus")) Then
                temp23 = ""
            Else
                temp23 = drDSRow("strComplianceStatus")
            End If
            If IsDBNull(drDSRow("strCC")) Then
                temp24 = ""
            Else
                temp24 = drDSRow("strCC")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp25 = ""
            Else
                temp25 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp26 = ""
            Else
                temp26 = drDSRow("datModifingDate")
            End If
            If IsDBNull(drDSRow("strControlEquipmentData")) Then
                temp27 = ""
            Else
                temp27 = drDSRow("strControlEquipmentData")
            End If
            If IsDBNull(drDSRow("strDelete")) Then
                temp28 = ""
            Else
                temp28 = drDSRow("strDelete")
            End If
            If IsDBNull(drDSRow("strDeterminationMethod")) Then
                temp29 = ""
            Else
                temp29 = drDSRow("strDeterminationMethod")
            End If
            If IsDBNull(drDSRow("strOtherWitnessingEng")) Then
                temp30 = ""
            Else
                temp30 = drDSRow("strOtherWitnessingEng")
            End If
            If IsDBNull(drDSRow("strConfidentialData")) Then
                temp31 = ""
            Else
                temp31 = drDSRow("strConfidentialData")
            End If

            SQL = "SELECT strReferenceNumber " & _
            "FROM " & connNameSpace & ".ISMPMaster " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".ISMPReportInformation " & _
                "WHERE strReferenceNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".ISMPReportInformation " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                    "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                    "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                    "'" & Replace(temp21, "'", "''") & "', '" & Replace(temp22, "'", "''") & "', " & _
                    "'" & Replace(temp23, "'", "''") & "', '" & Replace(temp24, "'", "''") & "', " & _
                    "'" & Replace(temp25, "'", "''") & "', '" & Replace(temp26, "'", "''") & "', " & _
                    "'" & Replace(temp27, "'", "''") & "', '" & Replace(temp28, "'", "''") & "', " & _
                    "'" & Replace(temp29, "'", "''") & "', '" & Replace(temp30, "'", "''") & "', " & _
                    "'" & Replace(temp31, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".ISMPReportInformation SET " & _
                    "strReferenceNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strPollutant = '" & Replace(temp2, "'", "''") & "', " & _
                    "strEmissionSource = '" & Replace(temp3, "'", "''") & "', " & _
                    "strReportType = '" & Replace(temp4, "'", "''") & "', " & _
                    "strDocumentType = '" & Replace(temp5, "'", "''") & "', " & _
                    "strApplicableRequirement = '" & Replace(temp6, "'", "''") & "', " & _
                    "strTestingFirm = '" & Replace(temp7, "'", "''") & "', " & _
                    "strReviewingEngineer = '" & Replace(temp8, "'", "''") & "', " & _
                    "strWitnessingEngineer = '" & Replace(temp9, "'", "''") & "', " & _
                    "strWitnessingEngineer2 = '" & Replace(temp10, "'", "''") & "', " & _
                    "strReviewingUnit = '" & Replace(temp11, "'", "''") & "', " & _
                    "datReviewedByUnitManager = '" & Replace(temp12, "'", "''") & "', " & _
                    "strComplianceManager = '" & Replace(temp13, "'", "''") & "', " & _
                    "datTestDateStart = '" & Replace(temp14, "'", "''") & "', " & _
                    "datTestDateEnd = '" & Replace(temp15, "'", "''") & "', " & _
                    "datReceivedDate = '" & Replace(temp16, "'", "''") & "', " & _
                    "datCompleteDate = '" & Replace(temp17, "'", "''") & "', " & _
                    "mmoCommentArea = '" & Replace(temp18, "'", "''") & "', " & _
                    "strClosed = '" & Replace(temp19, "'", "''") & "', " & _
                    "strCommissioner = '" & Replace(temp20, "'", "''") & "', " & _
                    "strDirector = '" & Replace(temp21, "'", "''") & "', " & _
                    "strProgramManager = '" & Replace(temp22, "'", "''") & "', " & _
                    "strComplianceStatus = '" & Replace(temp23, "'", "''") & "', " & _
                    "strCC = '" & Replace(temp24, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp25, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp26, "'", "''") & "', " & _
                    "strControlEquipmentData = '" & Replace(temp27, "'", "''") & "', " & _
                    "strDelete = '" & Replace(temp28, "'", "''") & "', " & _
                    "strDeterminationMethod = '" & Replace(temp29, "'", "''") & "', " & _
                    "strOtherWitnessingEng = '" & Replace(temp30, "'", "''") & "', " & _
                    "strConfidentialData = '" & Replace(temp31, "'", "''") & "' " & _
                    "WHERE strReferenceNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferISMPREportMemo()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPReportMemo "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strReferenceNumber")
            End If
            If IsDBNull(drDSRow("strMemorandumField")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strMemorandumField")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacity")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strMaxOperatingCapacity")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacityUnit")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strMaxOperatingCapacityUnit")
            End If
            If IsDBNull(drDSRow("strOperatingCapacity")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strOperatingCapacity")
            End If
            If IsDBNull(drDSRow("strOperatingCapacityUnit")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strOperatingCapacityUnit")
            End If
            If IsDBNull(drDSRow("strAllowableEmissioNRate1A")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strAllowableEmissioNRate1A")
            End If
            If IsDBNull(drDSRow("strAllowableEmissioNRate1B")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strAllowableEmissioNRate1B")
            End If
            If IsDBNull(drDSRow("strAllowableEmissioNRate1C")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strAllowableEmissioNRate1C")
            End If
            If IsDBNull(drDSRow("strAllowableEmisssionRateUnit1A")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strAllowableEmisssionRateUnit1A")
            End If
            If IsDBNull(drDSRow("strAllowableEmisssionRateUnit1B")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strAllowableEmisssionRateUnit1B")
            End If
            If IsDBNull(drDSRow("strAllowableEmisssionRateUnit1C")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strAllowableEmisssionRateUnit1C")
            End If
            If IsDBNull(drDSRow("strMonitorManufactureAndModel")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strMonitorManufactureAndModel")
            End If
            If IsDBNull(drDSRow("strMonitorSerialNumber")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strMonitorSerialNumber")
            End If

            SQL = "SELECT strReferenceNumber " & _
            "FROM " & connNameSpace & ".ISMPReportInformation " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".ISMPReportMemo " & _
                "WHERE strReferenceNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".ISMPReportMemo " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".ISMPReportMemo SET " & _
                    "strReferenceNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strMemorandumField = '" & Replace(temp2, "'", "''") & "', " & _
                    "strMaxOperatingCapacity = '" & Replace(temp3, "'", "''") & "', " & _
                    "strMaxOperatingCapacityUnit = '" & Replace(temp4, "'", "''") & "', " & _
                    "strOperatingCapacity = '" & Replace(temp5, "'", "''") & "', " & _
                    "strOperatingCapacityUnit = '" & Replace(temp6, "'", "''") & "', " & _
                    "strAllowableEmissioNRate1A = '" & Replace(temp7, "'", "''") & "', " & _
                    "strAllowableEmissioNRate1B = '" & Replace(temp8, "'", "''") & "', " & _
                    "strAllowableEmissioNRate1C = '" & Replace(temp9, "'", "''") & "', " & _
                    "strAllowableEmisssionRateUnit1A = '" & Replace(temp10, "'", "''") & "', " & _
                    "strAllowableEmisssionRateUnit1B = '" & Replace(temp11, "'", "''") & "', " & _
                    "strAllowableEmisssionRateUnit1C = '" & Replace(temp12, "'", "''") & "', " & _
                    "strMonitorManufactureAndModel = '" & Replace(temp13, "'", "''") & "', " & _
                    "strMonitorSerialNumber = '" & Replace(temp14, "'", "''") & "' " & _
                    "WHERE strReferenceNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferISMPReportOneStack()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPReportOneStack "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strReferenceNumber")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacity")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strMaxOperatingCapacity")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacityUnit")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strMaxOperatingCapacityUnit")
            End If
            If IsDBNull(drDSRow("strOperatingCapacity")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strOperatingCapacity")
            End If
            If IsDBNull(drDSRow("strOperatingCapacityUnit")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strOperatingCapacityUnit")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate1")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strAllowableEmissionRate1")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate2")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strAllowableEmissionRate2")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate3")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strAllowableEmissionRate3")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRateUnit1")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strAllowableEmissionRateUnit1")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRateUnit2")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strAllowableEmissionRateUnit2")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRateUnit3")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strAllowableEmissionRateUnit3")
            End If
            If IsDBNull(drDSRow("strRunNumber1A")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strRunNumber1A")
            End If
            If IsDBNull(drDSRow("strRunNumber1B")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strRunNumber1B")
            End If
            If IsDBNull(drDSRow("strRunNumber1C")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strRunNumber1C")
            End If
            If IsDBNull(drDSRow("strRunNumber1D")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("strRunNumber1D")
            End If
            If IsDBNull(drDSRow("strGasTemperature1A")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("strGasTemperature1A")
            End If
            If IsDBNull(drDSRow("strGasTemperature1B")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("strGasTemperature1B")
            End If
            If IsDBNull(drDSRow("strGasTemperature1C")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("strGasTemperature1C")
            End If
            If IsDBNull(drDSRow("strGasTemperature1D")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("strGasTemperature1D")
            End If
            If IsDBNull(drDSRow("strGasMoisture1A")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("strGasMoisture1A")
            End If
            If IsDBNull(drDSRow("strGasMoisture1B")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("strGasMoisture1B")
            End If
            If IsDBNull(drDSRow("strGasMoisture1C")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("strGasMoisture1C")
            End If
            If IsDBNull(drDSRow("strGasMoisture1D")) Then
                temp23 = ""
            Else
                temp23 = drDSRow("strGasMoisture1D")
            End If
            If IsDBNull(drDSRow("strGasFlowRateACFM1A")) Then
                temp24 = ""
            Else
                temp24 = drDSRow("strGasFlowRateACFM1A")
            End If
            If IsDBNull(drDSRow("strGasFlowRateACFM1B")) Then
                temp25 = ""
            Else
                temp25 = drDSRow("strGasFlowRateACFM1B")
            End If
            If IsDBNull(drDSRow("strGasFlowRateACFM1C")) Then
                temp26 = ""
            Else
                temp26 = drDSRow("strGasFlowRateACFM1C")
            End If
            If IsDBNull(drDSRow("strGasFlowRateACFM1D")) Then
                temp27 = ""
            Else
                temp27 = drDSRow("strGasFlowRateACFM1D")
            End If
            If IsDBNull(drDSRow("strGasFlowRateDSCFM1A")) Then
                temp28 = ""
            Else
                temp28 = drDSRow("strGasFlowRateDSCFM1A")
            End If
            If IsDBNull(drDSRow("strGasFlowRateDSCFM1B")) Then
                temp29 = ""
            Else
                temp29 = drDSRow("strGasFlowRateDSCFM1B")
            End If
            If IsDBNull(drDSRow("strGasFlowRateDSCFM1C")) Then
                temp30 = ""
            Else
                temp30 = drDSRow("strGasFlowRateDSCFM1C")
            End If
            If IsDBNull(drDSRow("strGasFlowRateDSCFM1D")) Then
                temp31 = ""
            Else
                temp31 = drDSRow("strGasFlowRateDSCFM1D")
            End If
            If IsDBNull(drDSRow("strPollutantConcentration1A")) Then
                temp32 = ""
            Else
                temp32 = drDSRow("strPollutantConcentration1A")
            End If
            If IsDBNull(drDSRow("strPollutantConcentration1B")) Then
                temp33 = ""
            Else
                temp33 = drDSRow("strPollutantConcentration1B")
            End If
            If IsDBNull(drDSRow("strPollutantConcentration1C")) Then
                temp34 = ""
            Else
                temp34 = drDSRow("strPollutantConcentration1C")
            End If
            If IsDBNull(drDSRow("strPollutantConcentration1D")) Then
                temp35 = ""
            Else
                temp35 = drDSRow("strPollutantConcentration1D")
            End If
            If IsDBNull(drDSRow("strPollutantConcentrationUnit")) Then
                temp36 = ""
            Else
                temp36 = drDSRow("strPollutantConcentrationUnit")
            End If
            If IsDBNull(drDSRow("strPollutantConcentrationAvg")) Then
                temp37 = ""
            Else
                temp37 = drDSRow("strPollutantConcentrationAvg")
            End If
            If IsDBNull(drDSRow("strEmissionRate1A")) Then
                temp38 = ""
            Else
                temp38 = drDSRow("strEmissionRate1A")
            End If
            If IsDBNull(drDSRow("strEmissionRate1B")) Then
                temp39 = ""
            Else
                temp39 = drDSRow("strEmissionRate1B")
            End If
            If IsDBNull(drDSRow("strEmissionRate1C")) Then
                temp40 = ""
            Else
                temp40 = drDSRow("strEmissionRate1C")
            End If
            If IsDBNull(drDSRow("strEmissionRate1D")) Then
                temp41 = ""
            Else
                temp41 = drDSRow("strEmissionRate1D")
            End If
            If IsDBNull(drDSRow("strEmissionRateUnit")) Then
                temp42 = ""
            Else
                temp42 = drDSRow("strEmissionRateUnit")
            End If
            If IsDBNull(drDSRow("strEmissionRateAvg")) Then
                temp43 = ""
            Else
                temp43 = drDSRow("strEmissionRateAvg")
            End If
            If IsDBNull(drDSRow("strPercentAllowable")) Then
                temp44 = ""
            Else
                temp44 = drDSRow("strPercentAllowable")
            End If

            SQL = "SELECT strReferenceNumber " & _
            "FROM " & connNameSpace & ".ISMPReportInformation " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".ISMPReportOneStack " & _
                "WHERE strReferenceNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".ISMPReportOneStack " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                    "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                    "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                    "'" & Replace(temp21, "'", "''") & "', '" & Replace(temp22, "'", "''") & "', " & _
                    "'" & Replace(temp23, "'", "''") & "', '" & Replace(temp24, "'", "''") & "', " & _
                    "'" & Replace(temp25, "'", "''") & "', '" & Replace(temp26, "'", "''") & "', " & _
                    "'" & Replace(temp27, "'", "''") & "', '" & Replace(temp28, "'", "''") & "', " & _
                    "'" & Replace(temp29, "'", "''") & "', '" & Replace(temp30, "'", "''") & "', " & _
                    "'" & Replace(temp31, "'", "''") & "', '" & Replace(temp32, "'", "''") & "', " & _
                    "'" & Replace(temp33, "'", "''") & "', '" & Replace(temp34, "'", "''") & "', " & _
                    "'" & Replace(temp35, "'", "''") & "', '" & Replace(temp36, "'", "''") & "', " & _
                    "'" & Replace(temp37, "'", "''") & "', '" & Replace(temp38, "'", "''") & "', " & _
                    "'" & Replace(temp39, "'", "''") & "', '" & Replace(temp40, "'", "''") & "', " & _
                    "'" & Replace(temp41, "'", "''") & "', '" & Replace(temp42, "'", "''") & "', " & _
                    "'" & Replace(temp43, "'", "''") & "', '" & Replace(temp44, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".ISMPReportOneStack SET " & _
                    "strReferenceNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strMaxOperatingCapacity = '" & Replace(temp2, "'", "''") & "', " & _
                    "strMaxOperatingCapacityUnit = '" & Replace(temp3, "'", "''") & "', " & _
                    "strOperatingCapacity = '" & Replace(temp4, "'", "''") & "', " & _
                    "strOperatingCapacityUnit = '" & Replace(temp5, "'", "''") & "', " & _
                    "strAllowableEmissionRate1 = '" & Replace(temp6, "'", "''") & "', " & _
                    "strAllowableEmissionRate2 = '" & Replace(temp7, "'", "''") & "', " & _
                    "strAllowableEmissionRate3 = '" & Replace(temp8, "'", "''") & "', " & _
                    "strAllowableEmissionRateUnit1 = '" & Replace(temp9, "'", "''") & "', " & _
                    "strAllowableEmissionRateUnit2 = '" & Replace(temp10, "'", "''") & "', " & _
                    "strAllowableEmissionRateUnit3 = '" & Replace(temp11, "'", "''") & "', " & _
                    "strRunNumber1A = '" & Replace(temp12, "'", "''") & "', " & _
                    "strRunNumber1B = '" & Replace(temp13, "'", "''") & "', " & _
                    "strRunNumber1C = '" & Replace(temp14, "'", "''") & "', " & _
                    "strRunNumber1D = '" & Replace(temp15, "'", "''") & "', " & _
                    "strGasTemperature1A = '" & Replace(temp16, "'", "''") & "', " & _
                    "strGasTemperature1B = '" & Replace(temp17, "'", "''") & "', " & _
                    "strGasTemperature1C = '" & Replace(temp18, "'", "''") & "', " & _
                    "strGasTemperature1D = '" & Replace(temp19, "'", "''") & "', " & _
                    "strGasMoisture1A = '" & Replace(temp20, "'", "''") & "', " & _
                    "strGasMoisture1B = '" & Replace(temp21, "'", "''") & "', " & _
                    "strGasMoisture1C = '" & Replace(temp22, "'", "''") & "', " & _
                    "strGasMoisture1D = '" & Replace(temp23, "'", "''") & "', " & _
                    "strGasFlowRateACFM1A = '" & Replace(temp24, "'", "''") & "', " & _
                    "strGasFlowRateACFM1B = '" & Replace(temp25, "'", "''") & "', " & _
                    "strGasFlowRateACFM1C = '" & Replace(temp26, "'", "''") & "', " & _
                    "strGasFlowRateACFM1D = '" & Replace(temp27, "'", "''") & "', " & _
                    "strGasFlowRateDSCFM1A = '" & Replace(temp28, "'", "''") & "', " & _
                    "strGasFlowRateDSCFM1B = '" & Replace(temp29, "'", "''") & "', " & _
                    "strGasFlowRateDSCFM1C = '" & Replace(temp30, "'", "''") & "', " & _
                    "strGasFlowRateDSCFM1D = '" & Replace(temp31, "'", "''") & "', " & _
                    "strPollutantConcentration1A = '" & Replace(temp32, "'", "''") & "', " & _
                    "strPollutantConcentration1B = '" & Replace(temp33, "'", "''") & "', " & _
                    "strPollutantConcentration1C = '" & Replace(temp34, "'", "''") & "', " & _
                    "strPollutantConcentration1D = '" & Replace(temp35, "'", "''") & "', " & _
                    "strPollutantConcentrationUnit = '" & Replace(temp36, "'", "''") & "', " & _
                    "strPollutantConcentrationAvg = '" & Replace(temp37, "'", "''") & "', " & _
                    "strEmissionRate1A = '" & Replace(temp38, "'", "''") & "', " & _
                    "strEmissionRate1B = '" & Replace(temp39, "'", "''") & "', " & _
                    "strEmissionRate1C = '" & Replace(temp40, "'", "''") & "', " & _
                    "strEmissionRate1D = '" & Replace(temp41, "'", "''") & "', " & _
                    "strEmissionRateUnit = '" & Replace(temp42, "'", "''") & "', " & _
                    "strEmissionRateAvg = '" & Replace(temp43, "'", "''") & "', " & _
                    "strPercentAllowable = '" & Replace(temp44, "'", "''") & "' " & _
                    "WHERE strReferenceNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferISMPREportOpacity()
        SQL = "Select * " & _
              "from " & connNameSpace & ".ISMPReportOpacity "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strReferenceNumber")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacity1A")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strMaxOperatingCapacity1A")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacity2A")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strMaxOperatingCapacity2A")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacity3A")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strMaxOperatingCapacity3A")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacity4A")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strMaxOperatingCapacity4A")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacity5A")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strMaxOperatingCapacity5A")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacityUnit")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strMaxOperatingCapacityUnit")
            End If
            If IsDBNull(drDSRow("strOperatingCapacity1A")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strOperatingCapacity1A")
            End If
            If IsDBNull(drDSRow("strOperatingCapacity2A")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strOperatingCapacity2A")
            End If
            If IsDBNull(drDSRow("strOperatingCapacity3A")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strOperatingCapacity3A")
            End If
            If IsDBNull(drDSRow("strOperatingCapacity4A")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strOperatingCapacity4A")
            End If
            If IsDBNull(drDSRow("strOperatingCapacity5A")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strOperatingCapacity5A")
            End If
            If IsDBNull(drDSRow("strOperatingCapacityUnit")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strOperatingCapacityUnit")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate1A")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strAllowableEmissionRate1A")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate2A")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("strAllowableEmissionRate2A")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate3A")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("strAllowableEmissionRate3A")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate4A")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("strAllowableEmissionRate4A")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate5A")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("strAllowableEmissionRate5A")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRateUnit")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("strAllowableEmissionRateUnit")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate22")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("strAllowableEmissionRate22")
            End If
            If IsDBNull(drDSRow("strOpacityTestDuration")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("strOpacityTestDuration")
            End If
            If IsDBNull(drDSRow("strAccumulatedEmissionTime")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("strAccumulatedEmissionTime")
            End If
            If IsDBNull(drDSRow("strOpacityPointA")) Then
                temp23 = ""
            Else
                temp23 = drDSRow("strOpacityPointA")
            End If
            If IsDBNull(drDSRow("strOpacityPointB")) Then
                temp24 = ""
            Else
                temp24 = drDSRow("strOpacityPointB")
            End If
            If IsDBNull(drDSRow("strOpacityPointC")) Then
                temp25 = ""
            Else
                temp25 = drDSRow("strOpacityPointC")
            End If
            If IsDBNull(drDSRow("strOpacityPointD")) Then
                temp26 = ""
            Else
                temp26 = drDSRow("strOpacityPointD")
            End If
            If IsDBNull(drDSRow("strOpacityPointE")) Then
                temp27 = ""
            Else
                temp27 = drDSRow("strOpacityPointE")
            End If
            If IsDBNull(drDSRow("strEquipmentItem1")) Then
                temp28 = ""
            Else
                temp28 = drDSRow("strEquipmentItem1")
            End If
            If IsDBNull(drDSRow("strEquipmentItem2")) Then
                temp29 = ""
            Else
                temp29 = drDSRow("strEquipmentItem2")
            End If
            If IsDBNull(drDSRow("strEquipmentItem3")) Then
                temp30 = ""
            Else
                temp30 = drDSRow("strEquipmentItem#")
            End If
            If IsDBNull(drDSRow("strEquipmentItem4")) Then
                temp31 = ""
            Else
                temp31 = drDSRow("strEquipmentItem4")
            End If
            If IsDBNull(drDSRow("strEquipmentItem5")) Then
                temp32 = ""
            Else
                temp32 = drDSRow("strEquipmentItem5")
            End If

            SQL = "SELECT strReferenceNumber " & _
            "FROM " & connNameSpace & ".ISMPReportInformation " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".ISMPReportOpacity " & _
                "WHERE strReferenceNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".ISMPReportOpacity " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                    "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                    "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                    "'" & Replace(temp21, "'", "''") & "', '" & Replace(temp22, "'", "''") & "', " & _
                    "'" & Replace(temp23, "'", "''") & "', '" & Replace(temp24, "'", "''") & "', " & _
                    "'" & Replace(temp25, "'", "''") & "', '" & Replace(temp26, "'", "''") & "', " & _
                    "'" & Replace(temp27, "'", "''") & "', '" & Replace(temp28, "'", "''") & "', " & _
                    "'" & Replace(temp29, "'", "''") & "', '" & Replace(temp30, "'", "''") & "', " & _
                    "'" & Replace(temp31, "'", "''") & "', '" & Replace(temp32, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".ISMPReportOpacity SET " & _
                    "strReferenceNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strMaxOperatingCapacity1A = '" & Replace(temp2, "'", "''") & "', " & _
                    "strMaxOperatingCapacity2A = '" & Replace(temp3, "'", "''") & "', " & _
                    "strMaxOperatingCapacity3A = '" & Replace(temp4, "'", "''") & "', " & _
                    "strMaxOperatingCapacity4A = '" & Replace(temp5, "'", "''") & "', " & _
                    "strMaxOperatingCapacity5A = '" & Replace(temp6, "'", "''") & "', " & _
                    "strMaxOperatingCapacityUnit = '" & Replace(temp7, "'", "''") & "', " & _
                    "strOperatingCapacity1A = '" & Replace(temp8, "'", "''") & "', " & _
                    "strOperatingCapacity2A = '" & Replace(temp9, "'", "''") & "', " & _
                    "strOperatingCapacity3A = '" & Replace(temp10, "'", "''") & "', " & _
                    "strOperatingCapacity4A = '" & Replace(temp11, "'", "''") & "', " & _
                    "strOperatingCapacity5A = '" & Replace(temp12, "'", "''") & "', " & _
                    "strOperatingCapacityUnit = '" & Replace(temp13, "'", "''") & "', " & _
                    "strAllowableEmissionRate1A = '" & Replace(temp14, "'", "''") & "' " & _
                    "strAllowableEmissionRate2A = '" & Replace(temp15, "'", "''") & "', " & _
                    "strAllowableEmissionRate3A = '" & Replace(temp16, "'", "''") & "', " & _
                    "strAllowableEmissionRate4A = '" & Replace(temp17, "'", "''") & "', " & _
                    "strAllowableEmissionRate5A = '" & Replace(temp18, "'", "''") & "', " & _
                    "strAllowableEmissionRateUnit = '" & Replace(temp19, "'", "''") & "', " & _
                    "strAllowableEmissionRate22 = '" & Replace(temp20, "'", "''") & "', " & _
                    "strOpacityTestDuration = '" & Replace(temp21, "'", "''") & "', " & _
                    "strAccumulatedEmissionTime = '" & Replace(temp22, "'", "''") & "', " & _
                    "strOpacityPointA = '" & Replace(temp23, "'", "''") & "', " & _
                    "strOpacityPointB = '" & Replace(temp24, "'", "''") & "', " & _
                    "strOpacityPointC = '" & Replace(temp25, "'", "''") & "', " & _
                    "strOpacityPointD = '" & Replace(temp26, "'", "''") & "', " & _
                    "strOpacityPointE = '" & Replace(temp27, "'", "''") & "', " & _
                    "strEquipmentItem1 = '" & Replace(temp28, "'", "''") & "', " & _
                    "strEquipmentItem2 = '" & Replace(temp29, "'", "''") & "', " & _
                    "strEquipmentItem3 = '" & Replace(temp30, "'", "''") & "', " & _
                    "strEquipmentItem4 = '" & Replace(temp31, "'", "''") & "', " & _
                    "strEquipmentItem5 = '" & Replace(temp32, "'", "''") & "' " & _
                    "WHERE strReferenceNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferISMPREportPondAndGas()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPReportPondAndGas "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strReferenceNumber")
            End If
            If IsDBNull(drDSRow("strMaxOperatingcapacity")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strMaxOperatingcapacity")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacityUnit")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strMaxOperatingCapacityUnit")
            End If
            If IsDBNull(drDSRow("strOperatingCapacity")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strOperatingCapacity")
            End If
            If IsDBNull(drDSRow("strOperatingCapacityUnit")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strOperatingCapacityUnit")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate1")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strAllowableEmissionRate1")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate2")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strAllowableEmissionRate2")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate3")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strAllowableEmissionRate3")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRateUnit1")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strAllowableEmissionRateUnit1")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRateUnit2")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strAllowableEmissionRateUnit2")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRateUnit3")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strAllowableEmissionRateUnit3")
            End If
            If IsDBNull(drDSRow("strRunNumber1A")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strRunNumber1A")
            End If
            If IsDBNull(drDSRow("strRunNumber1B")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strRunNumber1B")
            End If
            If IsDBNull(drDSRow("strRunNumber1C")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strRunNumber1C")
            End If
            If IsDBNull(drDSRow("strPollutantConcentration1A")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("strPollutantConcentration1A")
            End If
            If IsDBNull(drDSRow("strPollutantConcentration1B")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("strPollutantConcentration1B")
            End If
            If IsDBNull(drDSRow("strPollutantConcentration1C")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("strPollutantConcentration1C")
            End If
            If IsDBNull(drDSRow("strPollutantConcentrationUnit")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("strPollutantConcentrationUnit")
            End If
            If IsDBNull(drDSRow("strPollutantConcentractionAvg")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("strPollutantConcentractionAvg")
            End If
            If IsDBNull(drDSRow("strEmissionRate1A")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("strEmissionRate1A")
            End If
            If IsDBNull(drDSRow("strEmissionRate1B")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("strEmissionRate1B")
            End If
            If IsDBNull(drDSRow("strEmissionRate1C")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("strEmissionRate1C")
            End If
            If IsDBNull(drDSRow("strEmissionRateUnit")) Then
                temp23 = ""
            Else
                temp23 = drDSRow("strEmissionRateUnit")
            End If
            If IsDBNull(drDSRow("strEmissionRateAvg")) Then
                temp24 = ""
            Else
                temp24 = drDSRow("strEmissionRateAvg")
            End If
            If IsDBNull(drDSRow("strTreatmentRate1A")) Then
                temp25 = ""
            Else
                temp25 = drDSRow("strTreatmentRate1A")
            End If
            If IsDBNull(drDSRow("strTreatmentRate1B")) Then
                temp26 = ""
            Else
                temp26 = drDSRow("strTreatmentRate1B")
            End If
            If IsDBNull(drDSRow("strTreatmentRate1C")) Then
                temp27 = ""
            Else
                temp27 = drDSRow("strTreatmentRate1C")
            End If
            If IsDBNull(drDSRow("strTreatmentRateUnit")) Then
                temp28 = ""
            Else
                temp28 = drDSRow("strTreatmentRateUnit")
            End If
            If IsDBNull(drDSRow("strTreatmentRateAvg")) Then
                temp29 = ""
            Else
                temp29 = drDSRow("strTreatmentRateAvg")
            End If
            If IsDBNull(drDSRow("strPercentAllowable")) Then
                temp30 = ""
            Else
                temp30 = drDSRow("strPercentAllowable")
            End If

            SQL = "SELECT strReferenceNumber " & _
            "FROM " & connNameSpace & ".ISMPReportInformation " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".ISMPReportPondAndGas " & _
                "WHERE strReferenceNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".ISMPReportPondAndGas " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                    "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                    "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                    "'" & Replace(temp21, "'", "''") & "', '" & Replace(temp22, "'", "''") & "', " & _
                    "'" & Replace(temp23, "'", "''") & "', '" & Replace(temp24, "'", "''") & "', " & _
                    "'" & Replace(temp25, "'", "''") & "', '" & Replace(temp26, "'", "''") & "', " & _
                    "'" & Replace(temp27, "'", "''") & "', '" & Replace(temp28, "'", "''") & "', " & _
                    "'" & Replace(temp29, "'", "''") & "', '" & Replace(temp30, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".ISMPReportPondAndGas SET " & _
                    "strReferenceNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strMaxOperatingCapacity = '" & Replace(temp2, "'", "''") & "', " & _
                    "strMaxOperatingCapacityUnit = '" & Replace(temp3, "'", "''") & "', " & _
                    "strOperatingCapacity = '" & Replace(temp4, "'", "''") & "', " & _
                    "strOperatingCapacityUnit = '" & Replace(temp5, "'", "''") & "', " & _
                    "strAllowableEmissionRate1 = '" & Replace(temp6, "'", "''") & "', " & _
                    "strAllowableEmissionRate2 = '" & Replace(temp7, "'", "''") & "', " & _
                    "strAllowableEmissionRate3 = '" & Replace(temp8, "'", "''") & "', " & _
                    "strAllowableEmissionRateUnit1 = '" & Replace(temp9, "'", "''") & "', " & _
                    "strAllowableEmissionRateUnit2 = '" & Replace(temp10, "'", "''") & "', " & _
                    "strAllowableEmissionRateUnit3 = '" & Replace(temp11, "'", "''") & "', " & _
                    "strRunNumber1A = '" & Replace(temp12, "'", "''") & "', " & _
                    "strRunNumber1B = '" & Replace(temp13, "'", "''") & "', " & _
                    "strRunNumber1C = '" & Replace(temp14, "'", "''") & "', " & _
                    "strPollutantConcentration1A = '" & Replace(temp15, "'", "''") & "', " & _
                    "strPollutantConcentration1B = '" & Replace(temp16, "'", "''") & "', " & _
                    "strPollutantConcentration1C = '" & Replace(temp17, "'", "''") & "', " & _
                    "strPollutantConcentrationUnit = '" & Replace(temp18, "'", "''") & "', " & _
                    "strPollutantConcentractionAvg = '" & Replace(temp19, "'", "''") & "', " & _
                    "strEmissionRate1A = '" & Replace(temp20, "'", "''") & "', " & _
                    "strEmissionRate1B = '" & Replace(temp21, "'", "''") & "', " & _
                    "strEmissionRate1C = '" & Replace(temp22, "'", "''") & "', " & _
                    "strEmissionRateUnit = '" & Replace(temp23, "'", "''") & "', " & _
                    "strEmissionRateAvg = '" & Replace(temp24, "'", "''") & "', " & _
                    "strTreatmentRate1A = '" & Replace(temp25, "'", "''") & "', " & _
                    "strTreatmentRate1B = '" & Replace(temp26, "'", "''") & "', " & _
                    "strTreatmentRate1C = '" & Replace(temp27, "'", "''") & "', " & _
                    "strTreatmentRateUnit = '" & Replace(temp28, "'", "''") & "', " & _
                    "strTreatmentRateAvg = '" & Replace(temp29, "'", "''") & "', " & _
                    "strPercentAllowable = '" & Replace(temp30, "'", "''") & "' " & _
                    "WHERE strReferenceNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferISMPReportRATA()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPReportRata "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strReferenceNumber")
            End If
            If IsDBNull(drDSRow("strDiluent")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strDiluent")
            End If
            If IsDBNull(drDSRow("strApplicableStandard")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strApplicableStandard")
            End If
            If IsDBNull(drDSRow("strRelativeAccuracyPercent")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strRelativeAccuracyPercent")
            End If
            If IsDBNull(drDSRow("strReferenceMethod1")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strReferenceMethod1")
            End If
            If IsDBNull(drDSRow("strReferenceMethod2")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strReferenceMethod2")
            End If
            If IsDBNull(drDSRow("strReferenceMethod3")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strReferenceMethod3")
            End If
            If IsDBNull(drDSRow("strReferenceMethod4")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strReferenceMethod4")
            End If
            If IsDBNull(drDSRow("strReferenceMethod5")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strReferenceMethod5")
            End If
            If IsDBNull(drDSRow("strReferenceMethod6")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strReferenceMethod6")
            End If
            If IsDBNull(drDSRow("strReferenceMethod7")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strReferenceMethod7")
            End If
            If IsDBNull(drDSRow("strReferenceMethod8")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strReferenceMethod8")
            End If
            If IsDBNull(drDSRow("strReferenceMethod9")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strReferenceMethod9")
            End If
            If IsDBNull(drDSRow("strReferenceMethod10")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strReferenceMethod10")
            End If
            If IsDBNull(drDSRow("strReferenceMethod11")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("strReferenceMethod11")
            End If
            If IsDBNull(drDSRow("strReferenceMethod12")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("strReferenceMethod12")
            End If
            If IsDBNull(drDSRow("strRataUnits")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("strRataUnits")
            End If
            If IsDBNull(drDSRow("strCMS1")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("strCMS1")
            End If
            If IsDBNull(drDSRow("strCMS2")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("strCMS2")
            End If
            If IsDBNull(drDSRow("strCMS3")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("strCMS3")
            End If
            If IsDBNull(drDSRow("strCMS4")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("strCMS4")
            End If
            If IsDBNull(drDSRow("strCMS5")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("strCMS5")
            End If
            If IsDBNull(drDSRow("strCMS6")) Then
                temp23 = ""
            Else
                temp23 = drDSRow("strCMS6")
            End If
            If IsDBNull(drDSRow("strCMS7")) Then
                temp24 = ""
            Else
                temp24 = drDSRow("strCMS7")
            End If
            If IsDBNull(drDSRow("strCMS8")) Then
                temp25 = ""
            Else
                temp25 = drDSRow("strCMS8")
            End If
            If IsDBNull(drDSRow("strCMS9")) Then
                temp26 = ""
            Else
                temp26 = drDSRow("strCMS9")
            End If
            If IsDBNull(drDSRow("strCMS10")) Then
                temp27 = ""
            Else
                temp27 = drDSRow("strCMS10")
            End If
            If IsDBNull(drDSRow("strCMS11")) Then
                temp28 = ""
            Else
                temp28 = drDSRow("strCMS11")
            End If
            If IsDBNull(drDSRow("strCMS12")) Then
                temp29 = ""
            Else
                temp29 = drDSRow("strCMS12")
            End If
            If IsDBNull(drDSRow("strAccuracyChoice")) Then
                temp30 = ""
            Else
                temp30 = drDSRow("strAccuracyChoice")
            End If
            If IsDBNull(drDSRow("strAccuracyRequiredPercent")) Then
                temp31 = ""
            Else
                temp31 = drDSRow("strAccuracyRequiredPercent")
            End If
            If IsDBNull(drDSRow("strAccuracyRequiredStatement")) Then
                temp32 = ""
            Else
                temp32 = drDSRow("strAccuracyRequiredStatement")
            End If
            If IsDBNull(drDSRow("strRunsIncludedKey")) Then
                temp33 = ""
            Else
                temp33 = drDSRow("strRunsIncludedKey")
            End If

            SQL = "SELECT strReferenceNumber " & _
            "FROM " & connNameSpace & ".ISMPReportInformation " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".ISMPReportRata " & _
                "WHERE strReferenceNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".ISMPReportRata " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                    "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                    "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                    "'" & Replace(temp21, "'", "''") & "', '" & Replace(temp22, "'", "''") & "', " & _
                    "'" & Replace(temp23, "'", "''") & "', '" & Replace(temp24, "'", "''") & "', " & _
                    "'" & Replace(temp25, "'", "''") & "', '" & Replace(temp26, "'", "''") & "', " & _
                    "'" & Replace(temp27, "'", "''") & "', '" & Replace(temp28, "'", "''") & "', " & _
                    "'" & Replace(temp29, "'", "''") & "', '" & Replace(temp30, "'", "''") & "', " & _
                    "'" & Replace(temp31, "'", "''") & "', '" & Replace(temp32, "'", "''") & "', " & _
                    "'" & Replace(temp33, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".ISMPReportRata SET " & _
                    "strReferenceNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strDiluent = '" & Replace(temp2, "'", "''") & "', " & _
                    "strApplicableStandard = '" & Replace(temp3, "'", "''") & "', " & _
                    "strRelativeAccuracyPercent = '" & Replace(temp4, "'", "''") & "', " & _
                    "strReferenceMethod1 = '" & Replace(temp5, "'", "''") & "', " & _
                    "strReferenceMethod2 = '" & Replace(temp6, "'", "''") & "', " & _
                    "strReferenceMethod3 = '" & Replace(temp7, "'", "''") & "', " & _
                    "strReferenceMethod4 = '" & Replace(temp8, "'", "''") & "', " & _
                    "strReferenceMethod5 = '" & Replace(temp9, "'", "''") & "', " & _
                    "strReferenceMethod6 = '" & Replace(temp10, "'", "''") & "', " & _
                    "strReferenceMethod7 = '" & Replace(temp11, "'", "''") & "', " & _
                    "strReferenceMethod8 = '" & Replace(temp12, "'", "''") & "', " & _
                    "strReferenceMethod9 = '" & Replace(temp13, "'", "''") & "', " & _
                    "strReferenceMethod10 = '" & Replace(temp14, "'", "''") & "', " & _
                    "strReferenceMethod11 = '" & Replace(temp15, "'", "''") & "', " & _
                    "strReferenceMethod12 = '" & Replace(temp16, "'", "''") & "', " & _
                    "strRataUnits = '" & Replace(temp17, "'", "''") & "', " & _
                    "strCMS1 = '" & Replace(temp18, "'", "''") & "', " & _
                    "strCMS2 = '" & Replace(temp19, "'", "''") & "', " & _
                    "strCMS3 = '" & Replace(temp20, "'", "''") & "', " & _
                    "strCMS4 = '" & Replace(temp21, "'", "''") & "', " & _
                    "strCMS5 = '" & Replace(temp22, "'", "''") & "', " & _
                    "strCMS6 = '" & Replace(temp23, "'", "''") & "', " & _
                    "strCMS7 = '" & Replace(temp24, "'", "''") & "', " & _
                    "strCMS8 = '" & Replace(temp25, "'", "''") & "', " & _
                    "strCMS9 = '" & Replace(temp26, "'", "''") & "', " & _
                    "strCMS10 = '" & Replace(temp27, "'", "''") & "', " & _
                    "strCMS11 = '" & Replace(temp28, "'", "''") & "', " & _
                    "strCMS12 = '" & Replace(temp29, "'", "''") & "', " & _
                    "strAccuracyChoice = '" & Replace(temp30, "'", "''") & "', " & _
                    "strAccuracyRequiredPercent = '" & Replace(temp31, "'", "''") & "', " & _
                    "strAccuracyRequiredStatement = '" & Replace(temp32, "'", "''") & "', " & _
                    "strRunsIncludedKey = '" & Replace(temp33, "'", "''") & "' " & _
                    "WHERE strReferenceNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferISMPREportTwoStack()
        SQL = "Select * " & _
      "from " & connNameSpace & ".ISMPReportTwoStack "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strReferenceNumber")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacity")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strMaxOperatingCapacity")
            End If
            If IsDBNull(drDSRow("strMaxOperatingCapacityUnit")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strMaxOperatingCapacityUnit")
            End If
            If IsDBNull(drDSRow("strOperatingCapacity")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strOperatingCapacity")
            End If
            If IsDBNull(drDSRow("strOperatingCapacityUnit")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strOperatingCapacityUnit")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate1")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strAllowableEmissionRate1")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate2")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strAllowableEmissionRate2")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRate3")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strAllowableEmissionRate3")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRateUnit1")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strAllowableEmissionRateUnit1")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRateUnit2")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strAllowableEmissionRateUnit2")
            End If
            If IsDBNull(drDSRow("strAllowableEmissionRateUnit3")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strAllowableEmissionRateUnit3")
            End If
            If IsDBNull(drDSRow("strStackOneName")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strStackOneName")
            End If
            If IsDBNull(drDSRow("strStackTwoName")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strStackTwoName")
            End If
            If IsDBNull(drDSRow("strRunNumber1A")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strRunNumber1A")
            End If
            If IsDBNull(drDSRow("strRunNumber1B")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("strRunNumber1B")
            End If
            If IsDBNull(drDSRow("strRunNumber1C")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("strRunNumber1C")
            End If
            If IsDBNull(drDSRow("strRunNumber2A")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("strRunNumber2A")
            End If
            If IsDBNull(drDSRow("strRunNumber2B")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("strRunNumber2B")
            End If
            If IsDBNull(drDSRow("strRunNumber2c")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("strRunNumber2C")
            End If
            If IsDBNull(drDSRow("strGasTemperature1A")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("strGasTemperature1A")
            End If
            If IsDBNull(drDSRow("strGasTemperature1B")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("strGasTemperature1B")
            End If
            If IsDBNull(drDSRow("strGasTemperature1C")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("strGasTemperature1C")
            End If
            If IsDBNull(drDSRow("strGasTemperature2A")) Then
                temp23 = ""
            Else
                temp23 = drDSRow("strGasTemperature2A")
            End If
            If IsDBNull(drDSRow("strGasTemperature2B")) Then
                temp24 = ""
            Else
                temp24 = drDSRow("strGasTemperature2B")
            End If
            If IsDBNull(drDSRow("strGasTemperature2C")) Then
                temp25 = ""
            Else
                temp25 = drDSRow("strGasTemperature2C")
            End If
            If IsDBNull(drDSRow("strGasMoisture1A")) Then
                temp26 = ""
            Else
                temp26 = drDSRow("strGasMoisture1A")
            End If
            If IsDBNull(drDSRow("strGasMoisture1B")) Then
                temp27 = ""
            Else
                temp27 = drDSRow("strGasMoisture1B")
            End If
            If IsDBNull(drDSRow("strGasMoisture1C")) Then
                temp28 = ""
            Else
                temp28 = drDSRow("strGasMoisture1C")
            End If
            If IsDBNull(drDSRow("strGasMoisture2A")) Then
                temp29 = ""
            Else
                temp29 = drDSRow("strGasMoisture2A")
            End If
            If IsDBNull(drDSRow("strGasMoisture2B")) Then
                temp30 = ""
            Else
                temp30 = drDSRow("strGasMoisture2B")
            End If
            If IsDBNull(drDSRow("strGasMoisture2C")) Then
                temp31 = ""
            Else
                temp31 = drDSRow("strGasMoisture2C")
            End If
            If IsDBNull(drDSRow("strGasFlowRateACFM1A")) Then
                temp32 = ""
            Else
                temp32 = drDSRow("strGasFlowRateACFM1A")
            End If
            If IsDBNull(drDSRow("strGasFlowRateACFM1B")) Then
                temp33 = ""
            Else
                temp33 = drDSRow("strGasFlowRateACFM1B")
            End If
            If IsDBNull(drDSRow("strGasFlowRateACFM1C")) Then
                temp34 = ""
            Else
                temp34 = drDSRow("strGasFlowRateACFM1C")
            End If
            If IsDBNull(drDSRow("strGasFlowRateACFM2A")) Then
                temp35 = ""
            Else
                temp35 = drDSRow("strGasFlowRateACFM2A")
            End If
            If IsDBNull(drDSRow("strGasFlowRateACFM2B")) Then
                temp36 = ""
            Else
                temp36 = drDSRow("strGasFlowRateACFM2B")
            End If

            If IsDBNull(drDSRow("strGasFlowRateACFM2C")) Then
                temp37 = ""
            Else
                temp37 = drDSRow("strGasFlowRateACFM2C")
            End If
            If IsDBNull(drDSRow("strGasFlowRateDSCFM1A")) Then
                temp38 = ""
            Else
                temp38 = drDSRow("strGasFlowRateDSCFM1A")
            End If
            If IsDBNull(drDSRow("strGasFlowRateDSCFM1B")) Then
                temp39 = ""
            Else
                temp39 = drDSRow("strGasFlowRateDSCFM1B")
            End If
            If IsDBNull(drDSRow("strGasFlowRateDSCFM1C")) Then
                temp40 = ""
            Else
                temp40 = drDSRow("strGasFlowRateDSCFM1C")
            End If
            If IsDBNull(drDSRow("strGasFlowRateDSCFM2A")) Then
                temp41 = ""
            Else
                temp41 = drDSRow("strGasFlowRateDSCFM2A")
            End If
            If IsDBNull(drDSRow("strGasFlowRateDSCFM2B")) Then
                temp42 = ""
            Else
                temp42 = drDSRow("strGasFlowRateDSCFM2B")
            End If
            If IsDBNull(drDSRow("strGasFlowRateDSCFM2C")) Then
                temp43 = ""
            Else
                temp43 = drDSRow("strGasFlowRateDSCFM2C")
            End If
            If IsDBNull(drDSRow("strPollutantConcentration1A")) Then
                temp44 = ""
            Else
                temp44 = drDSRow("strPollutantConcentration1A")
            End If
            If IsDBNull(drDSRow("strPollutantConcentration1B")) Then
                temp45 = ""
            Else
                temp45 = drDSRow("strPollutantConcentration1B")
            End If
            If IsDBNull(drDSRow("strPollutantConcentration1C")) Then
                temp46 = ""
            Else
                temp46 = drDSRow("strPollutantConcentration1C")
            End If
            If IsDBNull(drDSRow("strPollutantConcentration2A")) Then
                temp47 = ""
            Else
                temp47 = drDSRow("strPollutantConcentration2A")
            End If

            If IsDBNull(drDSRow("strPollutantConcentration2B")) Then
                temp48 = ""
            Else
                temp48 = drDSRow("strPollutantConcentration2B")
            End If
            If IsDBNull(drDSRow("strPollutantConcentration2C")) Then
                temp49 = ""
            Else
                temp49 = drDSRow("strPollutantConcentration2C")
            End If
            If IsDBNull(drDSRow("strPollutantConcentractionUnit")) Then
                temp50 = ""
            Else
                temp50 = drDSRow("strPollutantConcentractionUnit")
            End If
            If IsDBNull(drDSRow("strPollutantConcentractionAvg1")) Then
                temp51 = ""
            Else
                temp51 = drDSRow("strPollutantConcentractionAvg1")
            End If
            If IsDBNull(drDSRow("strPollutantConcentractionAvg2")) Then
                temp52 = ""
            Else
                temp52 = drDSRow("strPollutantConcentractionAvg2")
            End If
            If IsDBNull(drDSRow("strEmissionRate1A")) Then
                temp53 = ""
            Else
                temp53 = drDSRow("strEmissionRate1A")
            End If
            If IsDBNull(drDSRow("strEmissionRate1B")) Then
                temp54 = ""
            Else
                temp54 = drDSRow("strEmissionRate1B")
            End If
            If IsDBNull(drDSRow("strEmissionRate1C")) Then
                temp55 = ""
            Else
                temp55 = drDSRow("strEmissionRate1C")
            End If
            If IsDBNull(drDSRow("strEmissionRate2A")) Then
                temp56 = ""
            Else
                temp56 = drDSRow("strEmissionRate2A")
            End If
            If IsDBNull(drDSRow("strEmissionRate2B")) Then
                temp57 = ""
            Else
                temp57 = drDSRow("strEmissionRate2B")
            End If
            If IsDBNull(drDSRow("strEmissionRate2C")) Then
                temp58 = ""
            Else
                temp58 = drDSRow("strEmissionRate2C")
            End If
            If IsDBNull(drDSRow("strEmissionRateUnit")) Then
                temp59 = ""
            Else
                temp59 = drDSRow("strEmissionRateUnit")
            End If
            If IsDBNull(drDSRow("strEmissionRateAvg1")) Then
                temp60 = ""
            Else
                temp60 = drDSRow("strEmissionRateAvg1")
            End If
            If IsDBNull(drDSRow("strEmissionRateAvg2")) Then
                temp61 = ""
            Else
                temp61 = drDSRow("strEmissionRateAvg2")
            End If
            If IsDBNull(drDSRow("strEmissionRateTotal1")) Then
                temp62 = ""
            Else
                temp62 = drDSRow("strEmissionRateTotal1")
            End If
            If IsDBNull(drDSRow("strEmissionRateTotal2")) Then
                temp63 = ""
            Else
                temp63 = drDSRow("strEmissionRateTotal2")
            End If
            If IsDBNull(drDSRow("strEmissionRateTotal3")) Then
                temp64 = ""
            Else
                temp64 = drDSRow("strEmissionRateTotal3")
            End If
            If IsDBNull(drDSRow("strEmissionRateTotalAvg")) Then
                temp65 = ""
            Else
                temp65 = drDSRow("strEmissionRateTotalAvg")
            End If
            If IsDBNull(drDSRow("strDestructionPercent")) Then
                temp66 = ""
            Else
                temp66 = drDSRow("strDestructionPercent")
            End If
            If IsDBNull(drDSRow("strPercentAllowable")) Then
                temp67 = ""
            Else
                temp67 = drDSRow("strPercentAllowable")
            End If
           
            SQL = "SELECT strReferenceNumber " & _
            "FROM " & connNameSpace & ".ISMPReportInformation " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".ISMPReportTwoStack " & _
                "WHERE strReferenceNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".ISMPReportTwoStack " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                    "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                    "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                    "'" & Replace(temp21, "'", "''") & "', '" & Replace(temp22, "'", "''") & "', " & _
                    "'" & Replace(temp23, "'", "''") & "', '" & Replace(temp24, "'", "''") & "', " & _
                    "'" & Replace(temp25, "'", "''") & "', '" & Replace(temp26, "'", "''") & "', " & _
                    "'" & Replace(temp27, "'", "''") & "', '" & Replace(temp28, "'", "''") & "', " & _
                    "'" & Replace(temp29, "'", "''") & "', '" & Replace(temp30, "'", "''") & "', " & _
                    "'" & Replace(temp31, "'", "''") & "', '" & Replace(temp32, "'", "''") & "', " & _
                    "'" & Replace(temp33, "'", "''") & "', '" & Replace(temp34, "'", "''") & "', " & _
                    "'" & Replace(temp35, "'", "''") & "', '" & Replace(temp36, "'", "''") & "', " & _
                    "'" & Replace(temp37, "'", "''") & "', '" & Replace(temp38, "'", "''") & "', " & _
                    "'" & Replace(temp39, "'", "''") & "', '" & Replace(temp40, "'", "''") & "', " & _
                    "'" & Replace(temp41, "'", "''") & "', '" & Replace(temp42, "'", "''") & "', " & _
                    "'" & Replace(temp43, "'", "''") & "', '" & Replace(temp44, "'", "''") & "', " & _
                    "'" & Replace(temp45, "'", "''") & "', '" & Replace(temp46, "'", "''") & "', " & _
                    "'" & Replace(temp47, "'", "''") & "', '" & Replace(temp48, "'", "''") & "', " & _
                    "'" & Replace(temp49, "'", "''") & "', '" & Replace(temp50, "'", "''") & "', " & _
                    "'" & Replace(temp51, "'", "''") & "', '" & Replace(temp52, "'", "''") & "', " & _
                    "'" & Replace(temp53, "'", "''") & "', '" & Replace(temp54, "'", "''") & "', " & _
                    "'" & Replace(temp55, "'", "''") & "', '" & Replace(temp56, "'", "''") & "', " & _
                    "'" & Replace(temp57, "'", "''") & "', '" & Replace(temp58, "'", "''") & "', " & _
                    "'" & Replace(temp59, "'", "''") & "', '" & Replace(temp60, "'", "''") & "', " & _
                    "'" & Replace(temp61, "'", "''") & "', '" & Replace(temp62, "'", "''") & "', " & _
                    "'" & Replace(temp63, "'", "''") & "', '" & Replace(temp64, "'", "''") & "', " & _
                    "'" & Replace(temp65, "'", "''") & "', '" & Replace(temp66, "'", "''") & "', " & _
                    "'" & Replace(temp67, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".ISMPReportTwoStack SET " & _
                    "strReferenceNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strMaxOperatingCapacity = '" & Replace(temp2, "'", "''") & "', " & _
                    "strMaxOperatingCapacityUnit = '" & Replace(temp3, "'", "''") & "', " & _
                    "strOperatingCapacity = '" & Replace(temp4, "'", "''") & "', " & _
                    "strOperatingCapacityUnit = '" & Replace(temp5, "'", "''") & "', " & _
                    "strAllowableEmissioNRate1 = '" & Replace(temp6, "'", "''") & "', " & _
                    "strAllowableEmissioNRate2 = '" & Replace(temp7, "'", "''") & "', " & _
                    "strAllowableEmissioNRate3 = '" & Replace(temp8, "'", "''") & "', " & _
                    "strAllowableEmisssionRateUnit1 = '" & Replace(temp9, "'", "''") & "', " & _
                    "strAllowableEmisssionRateUnit2 = '" & Replace(temp10, "'", "''") & "', " & _
                    "strAllowableEmisssionRateUnit3 = '" & Replace(temp11, "'", "''") & "', " & _
                    "strStackOneName = '" & Replace(temp12, "'", "''") & "', " & _
                    "strStackTwoName = '" & Replace(temp13, "'", "''") & "', " & _
                    "strRunNumber1A = '" & Replace(temp14, "'", "''") & "', " & _
                    "strRunNumber1B = '" & Replace(temp15, "'", "''") & "', " & _
                    "strRunNumber1C = '" & Replace(temp16, "'", "''") & "', " & _
                    "strRunNumber2A = '" & Replace(temp17, "'", "''") & "', " & _
                    "strRunNumber2B = '" & Replace(temp18, "'", "''") & "', " & _
                    "strRunNumber2C = '" & Replace(temp19, "'", "''") & "', " & _
                    "strGasTemperature1A = '" & Replace(temp20, "'", "''") & "', " & _
                    "strGasTemperature1B = '" & Replace(temp21, "'", "''") & "', " & _
                    "strGasTemperature1C = '" & Replace(temp22, "'", "''") & "', " & _
                    "strGasTemperature2A = '" & Replace(temp23, "'", "''") & "', " & _
                    "strGasTemperature2B = '" & Replace(temp24, "'", "''") & "', " & _
                    "strGasTemperature2C = '" & Replace(temp25, "'", "''") & "', " & _
                    "strGasMoisture1A = '" & Replace(temp26, "'", "''") & "', " & _
                    "strGasMoisture1B = '" & Replace(temp27, "'", "''") & "', " & _
                    "strGasMoisture1C = '" & Replace(temp28, "'", "''") & "', " & _
                    "strGasMoisture2A = '" & Replace(temp29, "'", "''") & "', " & _
                    "strGasMoisture2B = '" & Replace(temp30, "'", "''") & "', " & _
                    "strGasMoisture2C = '" & Replace(temp31, "'", "''") & "', " & _
                    "strGasFlowRateACFM1A = '" & Replace(temp32, "'", "''") & "', " & _
                    "strGasFlowRateACFM1B = '" & Replace(temp33, "'", "''") & "', " & _
                    "strGasFlowRateACFM1C = '" & Replace(temp34, "'", "''") & "', " & _
                    "strGasFlowRateACFM2A = '" & Replace(temp35, "'", "''") & "', " & _
                    "strGasFlowRateACFM2B = '" & Replace(temp36, "'", "''") & "', " & _
                    "strGasFlowRateACFM2C = '" & Replace(temp37, "'", "''") & "', " & _
                    "strGasFlowRateDSCFM1A = '" & Replace(temp38, "'", "''") & "', " & _
                    "strGasFlowRateDSCFM1B = '" & Replace(temp39, "'", "''") & "', " & _
                    "strGasFlowRateDSCFM1C = '" & Replace(temp40, "'", "''") & "', " & _
                    "strGasFlowRateDSCFM2A = '" & Replace(temp41, "'", "''") & "', " & _
                    "strGasFlowRateDSCFM2B = '" & Replace(temp42, "'", "''") & "', " & _
                    "strGasFlowRateDSCFM2C = '" & Replace(temp43, "'", "''") & "', " & _
                    "strPollutantConcentration1A = '" & Replace(temp44, "'", "''") & "', " & _
                    "strPollutantConcentration1B = '" & Replace(temp45, "'", "''") & "', " & _
                    "strPollutantConcentration1C = '" & Replace(temp46, "'", "''") & "', " & _
                    "strPollutantConcentration2A = '" & Replace(temp47, "'", "''") & "', " & _
                    "strPollutantConcentration2B = '" & Replace(temp48, "'", "''") & "', " & _
                    "strPollutantConcentration2C = '" & Replace(temp49, "'", "''") & "', " & _
                    "strPollutantConcentrationUnit = '" & Replace(temp50, "'", "''") & "', " & _
                    "strPollutantConcentrationAVG1 = '" & Replace(temp51, "'", "''") & "', " & _
                    "strPollutantConcentrationAVG2 = '" & Replace(temp52, "'", "''") & "', " & _
                    "strEmissionRate1A = '" & Replace(temp53, "'", "''") & "', " & _
                    "strEmissionRate1B = '" & Replace(temp54, "'", "''") & "', " & _
                    "strEmissionRate1C = '" & Replace(temp55, "'", "''") & "', " & _
                    "strEmissionRate2A = '" & Replace(temp56, "'", "''") & "', " & _
                    "strEmissionRate2B = '" & Replace(temp57, "'", "''") & "', " & _
                    "strEmissionRate2C = '" & Replace(temp58, "'", "''") & "', " & _
                    "strEmissionRateUnit = '" & Replace(temp59, "'", "''") & "', " & _
                    "strEmissionRateAVG1 = '" & Replace(temp60, "'", "''") & "', " & _
                    "strEmissionRateAVG2 = '" & Replace(temp61, "'", "''") & "', " & _
                    "strEmissionRateTotal1 = '" & Replace(temp62, "'", "''") & "', " & _
                    "strEmissionRateTotal2 = '" & Replace(temp63, "'", "''") & "', " & _
                    "strEmissionRateTotal3 = '" & Replace(temp64, "'", "''") & "', " & _
                    "strEmissionRateTotalAvg = '" & Replace(temp65, "'", "''") & "', " & _
                    "strDestructionPercent = '" & Replace(temp66, "'", "''") & "', " & _
                    "strPercentAllowable = '" & Replace(temp67, "'", "''") & "' " & _
                    "WHERE strReferenceNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferISMPReportType()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPReportType "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strKey")) Then
                temp = ""
            Else
                temp = drDSRow("strKey")
            End If
            If IsDBNull(drDSRow("strReportType")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strReportType")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".ISMPReportType " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".ISMPReportType " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".ISMPReportType SET " & _
                "strKey = '" & Replace(temp, "'", "''") & "', " & _
                "strReportType = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE strKey = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferISMPTestFirmComments()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPTestFirmComments "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strCommentsID")) Then
                temp = ""
            Else
                temp = drDSRow("strCommentsID")
            End If
            If IsDBNull(drDSRow("strTestingFirmKey")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strTestingFirmKey")
            End If
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("strTestLogNumber")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strTestLogNumber")
            End If
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strReferenceNumber")
            End If
            If IsDBNull(drDSRow("strCommentType")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strCommentType")
            End If
            If IsDBNull(drDSRow("strStaffResponsible")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strStaffResponsible")
            End If
            If IsDBNull(drDSRow("datCommentDate")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("datCommentDate")
            End If
            If IsDBNull(drDSRow("strComment")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strComment")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".ISMPTestFirmComments " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".ISMPTestFirmComments " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                "'" & Replace(temp11, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".ISMPTestFirmComments SET " & _
                "strCommentsID = '" & Replace(temp, "'", "''") & "', " & _
                "strTestingFirmKey = '" & Replace(temp2, "'", "''") & "', " & _
                "strAIRSNumber = '" & Replace(temp3, "'", "''") & "', " & _
                "strTestLogNumber = '" & Replace(temp4, "'", "''") & "', " & _
                "strReferenceNumber = '" & Replace(temp5, "'", "''") & "', " & _
                "strCommentType = '" & Replace(temp6, "'", "''") & "', " & _
                "strStaffResponsible = '" & Replace(temp7, "'", "''") & "', " & _
                "datCommentDate = '" & Replace(temp8, "'", "''") & "', " & _
                "strComment = '" & Replace(temp9, "'", "''") & "', " & _
                "strModifingPerson = '" & Replace(temp10, "'", "''") & "', " & _
                "datModifingDate = '" & Replace(temp11, "'", "''") & "' " & _
                "WHERE strCommentsID = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferISMPTestLogLink()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPTestLogLink "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strReferenceNumber")
            End If
            If IsDBNull(drDSRow("strTestLogNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strTestLogNumber")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".ISMPTestLogLink " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".ISMPTestLogLink " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".ISMPTestLogLink SET " & _
                "strReferenceNumber = '" & Replace(temp, "'", "''") & "', " & _
                "strTestLogNumber = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE strReferenceNumber = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferISMPTestLogNumber()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPTestLogNumber "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strTestLogNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strTestLogNumber")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".ISMPTestLogNumber " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".ISMPTestLogNumber " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".ISMPTestLogNumber SET " & _
                "strTestLogNumber = '" & Replace(temp, "'", "''") & "' " & _
                "WHERE strTestLogNumber = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferISMPTestNotification()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPTestNotification "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strTestLogNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strTestLogNumber")
            End If
            If IsDBNull(drDSRow("strEmissinoUnit")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strEmissinoUnit")
            End If

            If IsDBNull(drDSRow("datProposedStartDate")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("datProposedStartDate")
            End If
            If IsDBNull(drDSRow("datProposedEndDate")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("datProposedEndDate")
            End If
            If IsDBNull(drDSRow("strComments")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strComments")
            End If
            If IsDBNull(drDSRow("strUserID")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strUserID")
            End If
            If IsDBNull(drDSRow("strContactEmail")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strContactEmail")
            End If
            If IsDBNull(drDSRow("strConfirmationNumber")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strConfirmationNumber")
            End If
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("strStaffResponsible")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strStaffResponsible")
            End If
            If IsDBNull(drDSRow("strTestPlanAvailable")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strTestPlanAvailable")
            End If
            If IsDBNull(drDSRow("strTimelyNotification")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strTimelyNotification")
            End If
            If IsDBNull(drDSRow("strOnlineFirstName")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strOnlineFirstName")
            End If
            If IsDBNull(drDSRow("strOnlineLastName")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strOnlineLastName")
            End If
            If IsDBNull(drDSRow("strInternalComments")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("strInternalComments")
            End If

            If IsDBNull(drDSRow("strModifingStaff")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("strModifingStaff")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("datModifingDate")
            End If
            If IsDBNull(drDSRow("strTelephone")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("strTelephone")
            End If
            If IsDBNull(drDSRow("strFax")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("strFax")
            End If
            If IsDBNull(drDSRow("DatTestPlanReceived")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("DatTestPlanReceived")
            End If
            If IsDBNull(drDSRow("datTEstNotification")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("datTEstNotification")
            End If
            If IsDBNull(drDSRow("strPollutants")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("strPollutants")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".ISMPTestNotification " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".ISMPTestNotification " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                "'" & Replace(temp21, "'", "''") & "', '" & Replace(temp22, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".ISMPTestNotification SET " & _
                "strTestLogNumber = '" & Replace(temp, "'", "''") & "', " & _
                "strEmissionUnit = '" & Replace(temp2, "'", "''") & "', " & _
                "datProposedStartDate = '" & Replace(temp3, "'", "''") & "', " & _
                "datProposedEndDate = '" & Replace(temp4, "'", "''") & "', " & _
                "strComments = '" & Replace(temp5, "'", "''") & "', " & _
                "sttrUserID = '" & Replace(temp6, "'", "''") & "', " & _
                "strContactEmail = '" & Replace(temp7, "'", "''") & "', " & _
                "strConfirmationNumber = '" & Replace(temp8, "'", "''") & "', " & _
                "strAIRSNumber = '" & Replace(temp9, "'", "''") & "', " & _
                "strStaffResponsible = '" & Replace(temp10, "'", "''") & "', " & _
                "strTestPlanAvailable = '" & Replace(temp11, "'", "''") & "', " & _
                "strTimelyNotification = '" & Replace(temp12, "'", "''") & "', " & _
                "strOnlineFirstName = '" & Replace(temp13, "'", "''") & "', " & _
                "strOnlineLastName = '" & Replace(temp14, "'", "''") & "', " & _
                "strInternalComments = '" & Replace(temp15, "'", "''") & "', " & _
                "strModifingStaff = '" & Replace(temp16, "'", "''") & "', " & _
                "datModifingDate = '" & Replace(temp17, "'", "''") & "', " & _
                "strTelephone = '" & Replace(temp18, "'", "''") & "', " & _
                "strFax = '" & Replace(temp19, "'", "''") & "', " & _
                "datTestPlantReceived = '" & Replace(temp20, "'", "''") & "', " & _
                "datTestNotification = '" & Replace(temp21, "'", "''") & "', " & _
                "strPollutants = '" & Replace(temp22, "'", "''") & "' " & _
                "WHERE strTestLogNumber = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferISMPTestNotificationLog()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPTestNotificationLog "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strTestLogNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strTestLogNumber")
            End If
            If IsDBNull(drDSRow("strEngineer")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strEngineer")
            End If

            If IsDBNull(drDSRow("strUnit")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strUnit")
            End If
            If IsDBNull(drDSRow("strEmissionUnit")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strEmissionUnit")
            End If
            If IsDBNull(drDSRow("strAIRSnumber")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strAIRSnumber")
            End If
            If IsDBNull(drDSRow("strFacilityName")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strFacilityName")
            End If
            If IsDBNull(drDSRow("datNotificationDate")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("datNotificationDate")
            End If
            If IsDBNull(drDSRow("datProposedStartDate")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("datProposedStartDate")
            End If
            If IsDBNull(drDSRow("datProposedEndDate")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("datProposedEndDate")
            End If
            If IsDBNull(drDSRow("mmoComments")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("mmoComments")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".ISMPTestNotificationLog " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".ISMPTestNotificationLog " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".ISMPTestNotificationLog SET " & _
                "strTestLogNumber = '" & Replace(temp, "'", "''") & "', " & _
                "strEngineer = '" & Replace(temp2, "'", "''") & "', " & _
                "strUnit = '" & Replace(temp3, "'", "''") & "', " & _
                "strEmissionUnit = '" & Replace(temp4, "'", "''") & "', " & _
                "strAIRSNumber = '" & Replace(temp5, "'", "''") & "', " & _
                "strFacilityName = '" & Replace(temp6, "'", "''") & "', " & _
                "datNotificationDate = '" & Replace(temp7, "'", "''") & "', " & _
                "datProposedStartDate = '" & Replace(temp8, "'", "''") & "', " & _
                "datProposedEndDate = '" & Replace(temp9, "'", "''") & "', " & _
                "mmoComments = '" & Replace(temp10, "'", "''") & "' " & _
                "WHERE strTestLogNumber = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferISMPTestREportAids()

    End Sub
    Sub TransferISMPTestREportMemo()
        SQL = "Select * " & _
        "from " & connNameSpace & ".ISMPTestReportMemo "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strReferenceNumber")
            End If
            If IsDBNull(drDSRow("strMemorandumField")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strMemorandumField")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".ISMPTestReportMemo " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".ISMPTestReportMemo " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".ISMPTestReportMemo SET " & _
                "strReferenceNumber = '" & Replace(temp, "'", "''") & "', " & _
                "strMemorandumField = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE strReferenceNumber = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferISMPWitnessingEng()
        SQL = "Select * " & _
              "from " & connNameSpace & ".ISMPWitnessingEng "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strReferenceNumber")
            End If
            If IsDBNull(drDSRow("strWitnessingEngineer")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strWitnessingEngineer")
            End If

            SQL = "SELECT COUNT(*) " & _
            "FROM " & connNameSpace & ".ISMPWitnessingEng " & _
            "WHERE strReferenceNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = False Or dr.Item(0) = 0 Then
                SQL = "Insert into " & connNameSpace & ".ISMPWitnessingEng " & _
                "Values " & _
                "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
            Else
                SQL = "UPDATE " & connNameSpace & ".ISMPWitnessingEng SET " & _
                "strReferenceNumber = '" & Replace(temp, "'", "''") & "', " & _
                "strWitnessingEngineer = '" & Replace(temp2, "'", "''") & "' " & _
                "WHERE strReferenceNumber = '" & Replace(temp, "'", "''") & "' "
            End If
            dr.Close()

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Next
    End Sub
    Sub TransferSSCPACCS()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPACCS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strTrackingNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strTrackingNumber")
            End If
            If IsDBNull(drDSRow("strSubmittalNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strSubmittalNumber")
            End If

            If IsDBNull(drDSRow("strPostMarkedOnTime")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strPostMarkedOnTime")
            End If
            If IsDBNull(drDSRow("datPostMarkDate")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("datPostMarkDate")
            End If
            If IsDBNull(drDSRow("strSignedByRO")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strSignedByRO")
            End If
            If IsDBNull(drDSRow("strCorrectACCForms")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strCorrectACCForms")
            End If
            If IsDBNull(drDSRow("strTitleVConditionsListed")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strTitleVConditionsListed")
            End If
            If IsDBNull(drDSRow("strACCCorrectlyFilledOut")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strACCCorrectlyFilledOut")
            End If
            If IsDBNull(drDSRow("strReportedDeviations")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strReportedDeviations")
            End If
            If IsDBNull(drDSRow("strDeviationsUnReported")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strDeviationsUnReported")
            End If
            If IsDBNull(drDSRow("strcomments")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strComments")
            End If
            If IsDBNull(drDSRow("strEnforcementNeeded")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strEnforcementNeeded")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSCPItemMaster " & _
            "WHERE strTrackingNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPACCS " & _
                "WHERE strTrackingNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPACCS " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPACCS SET " & _
                    "strTrackingNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strSubmittalNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strPostMarkedOnTime = '" & Replace(temp3, "'", "''") & "', " & _
                    "datPostMarkDate = '" & Replace(temp4, "'", "''") & "', " & _
                    "strSignedByRO = '" & Replace(temp5, "'", "''") & "', " & _
                    "strCorrectACCForms = '" & Replace(temp6, "'", "''") & "', " & _
                    "strTitleVConditionsListed = '" & Replace(temp7, "'", "''") & "', " & _
                    "strACCCorrectlyFilledOut = '" & Replace(temp8, "'", "''") & "', " & _
                    "strReportedDeviations = '" & Replace(temp9, "'", "''") & "', " & _
                    "strDeviationsUnReported = '" & Replace(temp10, "'", "''") & "', " & _
                    "strComments = '" & Replace(temp11, "'", "''") & "', " & _
                    "strEnforcementNeeded = '" & Replace(temp12, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp13, "'", "''") & "', " & _
                    "datModifingPerson = '" & Replace(temp14, "'", "''") & "' " & _
                    "WHERE strTrackingNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPACCSHistory()
        SQL = "Select * " & _
      "from " & connNameSpace & ".SSCPACCSHistory "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strTrackingNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strTrackingNumber")
            End If
            If IsDBNull(drDSRow("strSubmittalNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strSubmittalNumber")
            End If

            If IsDBNull(drDSRow("strPostMarkedOnTime")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strPostMarkedOnTime")
            End If
            If IsDBNull(drDSRow("datPostMarkDate")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("datPostMarkDate")
            End If
            If IsDBNull(drDSRow("strSignedByRO")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strSignedByRO")
            End If
            If IsDBNull(drDSRow("strCorrectACCForms")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strCorrectACCForms")
            End If
            If IsDBNull(drDSRow("strTitleVConditionsListed")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strTitleVConditionsListed")
            End If
            If IsDBNull(drDSRow("strACCCorrectlyFilledOut")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strACCCorrectlyFilledOut")
            End If
            If IsDBNull(drDSRow("strReportedDeviations")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strReportedDeviations")
            End If
            If IsDBNull(drDSRow("strDeviationsUnReported")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strDeviationsUnReported")
            End If
            If IsDBNull(drDSRow("strcomments")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strComments")
            End If
            If IsDBNull(drDSRow("strEnforcementNeeded")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strEnforcementNeeded")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSCPItemMaster " & _
            "WHERE strTrackingNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPACCSHistory " & _
                "WHERE strTrackingNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPACCSHistory " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPACCSHistory SET " & _
                    "strTrackingNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strSubmittalNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strPostMarkedOnTime = '" & Replace(temp3, "'", "''") & "', " & _
                    "datPostMarkDate = '" & Replace(temp4, "'", "''") & "', " & _
                    "strSignedByRO = '" & Replace(temp5, "'", "''") & "', " & _
                    "strCorrectACCForms = '" & Replace(temp6, "'", "''") & "', " & _
                    "strTitleVConditionsListed = '" & Replace(temp7, "'", "''") & "', " & _
                    "strACCCorrectlyFilledOut = '" & Replace(temp8, "'", "''") & "', " & _
                    "strReportedDeviations = '" & Replace(temp9, "'", "''") & "', " & _
                    "strDeviationsUnReported = '" & Replace(temp10, "'", "''") & "', " & _
                    "strComments = '" & Replace(temp11, "'", "''") & "', " & _
                    "strEnforcementNeeded = '" & Replace(temp12, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp13, "'", "''") & "', " & _
                    "datModifingPerson = '" & Replace(temp14, "'", "''") & "' " & _
                    "WHERE strTrackingNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPDistrictAssignment()
        SQL = "Select * " & _
   "from " & connNameSpace & ".SSCPDistrictAssignment "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("strDistrictEngineer")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strDistrictEngineer")
            End If

            If IsDBNull(drDSRow("strDistrictAssigningManager")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strDistrictAssigningManager")
            End If
            If IsDBNull(drDSRow("datDistrictAssignmentDate")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("datDistrictAssignmentDate")
            End If
         
            SQL = "SELECT strAIRSNumber " & _
            "FROM " & connNameSpace & ".APBMasterAIRS " & _
            "WHERE strAIRSNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPDistrictAssignment " & _
                "WHERE strAIRSNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPDistrictAssignment " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPDistrictAssignment SET " & _
                    "strAIRSNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strDistrictEngineer = '" & Replace(temp2, "'", "''") & "', " & _
                    "strDistrictAssigningManager = '" & Replace(temp3, "'", "''") & "', " & _
                    "datDistrictAssignmentDate = '" & Replace(temp4, "'", "''") & "' " & _
                    "WHERE strAIRSNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPDistrictResponsible()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPDistrictResponsible "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("strDistrictResponsible")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strDistrictResponsible")
            End If

            If IsDBNull(drDSRow("strAssigningManager")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strAssigningManager")
            End If
            If IsDBNull(drDSRow("datAssigningDate")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("datAssigningDate")
            End If
          
            SQL = "SELECT strAIRSNumber " & _
            "FROM " & connNameSpace & ".APBMasterAIRS " & _
            "WHERE strAIRSNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPDistrictResponsible " & _
                "WHERE strAIRSNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPDistrictResponsible " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPDistrictResponsible SET " & _
                    "strAIRSNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strDistrictResponsible = '" & Replace(temp2, "'", "''") & "', " & _
                    "strAssigningManager = '" & Replace(temp3, "'", "''") & "', " & _
                    "datAssigningDate = '" & Replace(temp4, "'", "''") & "' " & _
                    "WHERE strAIRSNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPEnforcement()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPEnforcement "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strEnforcementNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strEnforcementNumber")
            End If
            If IsDBNull(drDSRow("strActionType")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strActionType")
            End If

            If IsDBNull(drDSRow("strGeneralComments")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strGeneralComments")
            End If
            If IsDBNull(drDSRow("datDiscoveryDate")) Then
                temp4 = ""
            Else
                temp4 = Format(CDate(drDSRow("datDiscoveryDate")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("datLONSent")) Then
                temp5 = ""
            Else
                temp5 = Format(CDate(drDSRow("datLONSent")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strLONComments")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strLONComments")
            End If
            If IsDBNull(drDSRow("strLONResolvedEnforcement")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strLONResolvedEnforcement")
            End If
            If IsDBNull(drDSRow("datDayZero")) Then
                temp8 = ""
            Else
                temp8 = Format(CDate(drDSRow("datDayZero")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strHPV")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strHPV")
            End If
            If IsDBNull(drDSRow("strAFSKeyActionNumber")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strAFSKeyActionNumber")
            End If
            If IsDBNull(drDSRow("datNOVSent")) Then
                temp11 = ""
            Else
                temp11 = Format(CDate(drDSRow("datNOVSent")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strAFSNOVSentNumber")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strAFSNOVSentNumber")
            End If
            If IsDBNull(drDSRow("strNOVCommentsEntry")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strNOVCommentsEntry")
            End If
            If IsDBNull(drDSRow("datNOVResponseReceived")) Then
                temp14 = ""
            Else
                temp14 = Format(CDate(drDSRow("datNOVResponseReceived")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strNOVResolvedEnforcement")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("strNOVResolvedEnforcement")
            End If
            If IsDBNull(drDSRow("strAFSNOVResolvedNumber")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("strAFSNOVResolvedNumber")
            End If
            If IsDBNull(drDSRow("datNFALetterSent")) Then
                temp17 = ""
            Else
                temp17 = Format(CDate(drDSRow("datNFALetterSent")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("datCOProposed")) Then
                temp18 = ""
            Else
                temp18 = Format(CDate(drDSRow("datCOProposed")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strAFSCOProposedNumber")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("strAFSCOProposedNumber")
            End If
            If IsDBNull(drDSRow("strCOCommentsEntry")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("strCOCommentsEntry")
            End If
            If IsDBNull(drDSRow("datCOExecuted")) Then
                temp21 = ""
            Else
                temp21 = Format(CDate(drDSRow("datCOExecuted")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strAFSCOExecutedNumber")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("strAFSCOExecutedNumber")
            End If
            If IsDBNull(drDSRow("datCOReceivedFromCompany")) Then
                temp23 = ""
            Else
                temp23 = Format(CDate(drDSRow("datCOReceivedFromCompany")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("datCOReceivedFromDirector")) Then
                temp24 = ""
            Else
                temp24 = Format(CDate(drDSRow("datCOReceivedFromDirector")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strCOResolvedEnforcement")) Then
                temp25 = ""
            Else
                temp25 = drDSRow("strCOResolvedEnforcement")
            End If
            If IsDBNull(drDSRow("datCOResolved")) Then
                temp26 = ""
            Else
                temp26 = Format(CDate(drDSRow("datCOResolved")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strAFSCOResolvedNumber")) Then
                temp27 = ""
            Else
                temp27 = drDSRow("strAFSCOResolvedNumber")
            End If
            If IsDBNull(drDSRow("strCOPenaltyAmount")) Then
                temp28 = ""
            Else
                temp28 = drDSRow("strCOPenaltyAmount")
            End If
            If IsDBNull(drDSRow("strCOPenaltyAmountComments")) Then
                temp29 = ""
            Else
                temp29 = drDSRow("strCOPenaltyAmountComments")
            End If
            If IsDBNull(drDSRow("strStipulatedPenalty")) Then
                temp30 = ""
            Else
                temp30 = drDSRow("strStipulatedPenalty")
            End If
            If IsDBNull(drDSRow("strAOCommentsEntry")) Then
                temp31 = ""
            Else
                temp31 = drDSRow("strAOCommentsEntry")
            End If
            If IsDBNull(drDSRow("datAOExecuted")) Then
                temp32 = ""
            Else
                temp32 = Format(CDate(drDSRow("datAOExecuted")), "dd-MMM-yyyy")
            End If

            If IsDBNull(drDSRow("strAFSAOtoAGNumber")) Then
                temp33 = ""
            Else
                temp33 = drDSRow("strAFSAOtoAGNumber")
            End If
            If IsDBNull(drDSRow("datAOAppealed")) Then
                temp34 = ""
            Else
                temp34 = Format(CDate(drDSRow("datAOAppealed")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strAFSCivilCourtNumber")) Then
                temp35 = ""
            Else
                temp35 = drDSRow("strAFSCivilCourtNumber")
            End If
            If IsDBNull(drDSRow("datAOResolved")) Then
                temp36 = ""
            Else
                temp36 = Format(CDate(drDSRow("datAOResolved")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strAFSAOResolvedNumber")) Then
                temp37 = ""
            Else
                temp37 = drDSRow("strAFSAOResolvedNumber")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp38 = ""
            Else
                temp38 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp39 = ""
            Else
                temp39 = Format(CDate(drDSRow("datModifingDate")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strPollutants")) Then
                temp40 = ""
            Else
                temp40 = drDSRow("strPollutants")
            End If
            If IsDBNull(drDSRow("strPollutantStatus")) Then
                temp41 = ""
            Else
                temp41 = drDSRow("strPollutantStatus")
            End If
            If IsDBNull(drDSRow("datLONtoUC")) Then
                temp42 = ""
            Else
                temp42 = Format(CDate(drDSRow("datLONtoUC")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("datNOVtoUC")) Then
                temp43 = ""
            Else
                temp43 = Format(CDate(drDSRow("datNOVtoUC")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("datNOVtoPM")) Then
                temp44 = ""
            Else
                temp44 = Format(CDate(drDSRow("datNOVtoPM")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("datNFAtoUC")) Then
                temp45 = ""
            Else
                temp45 = Format(CDate(drDSRow("datNFAtoUC")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("datNFAtoPM")) Then
                temp46 = ""
            Else
                temp46 = Format(CDate(drDSRow("datNFAtoPM")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("datCOtoUC")) Then
                temp47 = ""
            Else
                temp47 = Format(CDate(drDSRow("datCOtoUC")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("datCOToPM")) Then
                temp48 = ""
            Else
                temp48 = Format(CDate(drDSRow("datCOToPM")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strCONumber")) Then
                temp49 = ""
            Else
                temp49 = drDSRow("strCONumber")
            End If
            If IsDBNull(drDSRow("datLONResolved")) Then
                temp50 = ""
            Else
                temp50 = Format(CDate(drDSRow("datLONResolved")), "dd-MMM-yyyy")
            End If

            SQL = "SELECT strEnforcementNumber " & _
            "FROM " & connNameSpace & ".SSCPEnforcementItems " & _
            "WHERE strEnforcementNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPEnforcement " & _
                "WHERE strEnforcementNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPEnforcement " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                    "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                    "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                    "'" & Replace(temp21, "'", "''") & "', '" & Replace(temp22, "'", "''") & "', " & _
                    "'" & Replace(temp23, "'", "''") & "', '" & Replace(temp24, "'", "''") & "', " & _
                    "'" & Replace(temp25, "'", "''") & "', '" & Replace(temp26, "'", "''") & "', " & _
                    "'" & Replace(temp27, "'", "''") & "', '" & Replace(temp28, "'", "''") & "', " & _
                    "'" & Replace(temp29, "'", "''") & "', '" & Replace(temp30, "'", "''") & "', " & _
                    "'" & Replace(temp31, "'", "''") & "', '" & Replace(temp32, "'", "''") & "', " & _
                    "'" & Replace(temp33, "'", "''") & "', '" & Replace(temp34, "'", "''") & "', " & _
                    "'" & Replace(temp35, "'", "''") & "', '" & Replace(temp36, "'", "''") & "', " & _
                    "'" & Replace(temp37, "'", "''") & "', '" & Replace(temp38, "'", "''") & "', " & _
                    "'" & Replace(temp39, "'", "''") & "', '" & Replace(temp40, "'", "''") & "', " & _
                    "'" & Replace(temp41, "'", "''") & "', '" & Replace(temp42, "'", "''") & "', " & _
                    "'" & Replace(temp43, "'", "''") & "', '" & Replace(temp44, "'", "''") & "', " & _
                    "'" & Replace(temp45, "'", "''") & "', '" & Replace(temp46, "'", "''") & "', " & _
                    "'" & Replace(temp47, "'", "''") & "', '" & Replace(temp48, "'", "''") & "', " & _
                    "'" & Replace(temp49, "'", "''") & "', '" & Replace(temp50, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPEnforcement SET " & _
                    "strEnforcementNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strActionType = '" & Replace(temp2, "'", "''") & "', " & _
                    "strGeneralComments = '" & Replace(temp3, "'", "''") & "', " & _
                    "datDiscoveryDate = '" & Replace(temp4, "'", "''") & "', " & _
                    "datLONSent = '" & Replace(temp5, "'", "''") & "', " & _
                    "strLONComments = '" & Replace(temp6, "'", "''") & "', " & _
                    "strLONResolvedEnforcement = '" & Replace(temp7, "'", "''") & "', " & _
                    "datDayZero = '" & Replace(temp8, "'", "''") & "', " & _
                    "strHPV = '" & Replace(temp9, "'", "''") & "', " & _
                    "strAFSKeyActionNumber = '" & Replace(temp10, "'", "''") & "', " & _
                    "datNOVSent = '" & Replace(temp11, "'", "''") & "', " & _
                    "strAFSNOVSentNumber = '" & Replace(temp12, "'", "''") & "', " & _
                    "strNOVCommentsEntry = '" & Replace(temp13, "'", "''") & "', " & _
                    "datNOVResponseReceived = '" & Replace(temp14, "'", "''") & "', " & _
                    "strNOVResolvedEnforcement = '" & Replace(temp15, "'", "''") & "', " & _
                    "strAFSNOVResolvedNumber = '" & Replace(temp16, "'", "''") & "', " & _
                    "datNFALetterSent = '" & Replace(temp17, "'", "''") & "', " & _
                    "datCOProposed = '" & Replace(temp18, "'", "''") & "', " & _
                    "strAFSCOProposedNumber = '" & Replace(temp19, "'", "''") & "', " & _
                    "strCOCommentsEntry = '" & Replace(temp20, "'", "''") & "', " & _
                    "datCOExecuted = '" & Replace(temp21, "'", "''") & "', " & _
                    "strAFSCOExecutedNumber = '" & Replace(temp22, "'", "''") & "', " & _
                    "datCOReceivedFromCompany = '" & Replace(temp23, "'", "''") & "', " & _
                    "datCOReceivedFromDirector = '" & Replace(temp24, "'", "''") & "', " & _
                    "strCOResolvedEnforcement = '" & Replace(temp25, "'", "''") & "', " & _
                    "datCOResolved = '" & Replace(temp26, "'", "''") & "', " & _
                    "strAFSCOResolvedNumber = '" & Replace(temp27, "'", "''") & "', " & _
                    "strCOPenaltyAmount = '" & Replace(temp28, "'", "''") & "', " & _
                    "strCOPenaltyAmountComments = '" & Replace(temp29, "'", "''") & "', " & _
                    "strStipulatedPenalty = '" & Replace(temp30, "'", "''") & "', " & _
                    "strAOCommentsEntry = '" & Replace(temp31, "'", "''") & "', " & _
                    "datAOExecuted = '" & Replace(temp32, "'", "''") & "', " & _
                    "strAFSAOtoAGNumber = '" & Replace(temp33, "'", "''") & "', " & _
                    "datAOAppealed = '" & Replace(temp34, "'", "''") & "', " & _
                    "strAFSCivilCourtNumber = '" & Replace(temp35, "'", "''") & "', " & _
                    "datAOResolved = '" & Replace(temp36, "'", "''") & "', " & _
                    "strAFSAOResolvedNumber = '" & Replace(temp37, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp38, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp39, "'", "''") & "', " & _
                    "strPollutants = '" & Replace(temp40, "'", "''") & "', " & _
                    "strPollutantStatus = '" & Replace(temp41, "'", "''") & "', " & _
                    "datLONtoUC = '" & Replace(temp42, "'", "''") & "', " & _
                    "datNOVtoUC = '" & Replace(temp43, "'", "''") & "', " & _
                    "datNOVtoPM = '" & Replace(temp44, "'", "''") & "', " & _
                    "datNFAtoUC = '" & Replace(temp45, "'", "''") & "', " & _
                    "datNFAtoPM = '" & Replace(temp46, "'", "''") & "', " & _
                    "datCOtoUC = '" & Replace(temp47, "'", "''") & "', " & _
                    "datCOtoPM = '" & Replace(temp48, "'", "''") & "', " & _
                    "strCONumber = '" & Replace(temp49, "'", "''") & "', " & _
                    "datLONResolved = '" & Replace(temp50, "'", "''") & "' " & _
                    "WHERE strEnforcementNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPEnforcementAOComments()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPEnforcementAOComments "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strEnforcementNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strEnforcementNumber")
            End If
            If IsDBNull(drDSRow("strAOEntryNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strAOEntryNumber")
            End If

            If IsDBNull(drDSRow("strAOComment")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strAOComment")
            End If

            SQL = "SELECT strEnforcementNumber " & _
            "FROM " & connNameSpace & ".SSCPEnforcementItems " & _
            "WHERE strEnforcementNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPEnforcementAOComments " & _
                "WHERE strEnforcementNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPEnforcementAOComments " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPEnforcementAOComments SET " & _
                    "strEnforcementNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strAOEntryNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strAOComment = '" & Replace(temp3, "'", "''") & "' " & _
                    "WHERE strEnforcementNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPEnforcementCOComments()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPEnforcementCOComments "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strEnforcementNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strEnforcementNumber")
            End If
            If IsDBNull(drDSRow("strCOEntryNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strCOEntryNumber")
            End If

            If IsDBNull(drDSRow("strCOComment")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strCOComment")
            End If

            SQL = "SELECT strEnforcementNumber " & _
            "FROM " & connNameSpace & ".SSCPEnforcementItems " & _
            "WHERE strEnforcementNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPEnforcementCOComments " & _
                "WHERE strEnforcementNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPEnforcementCOComments " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPEnforcementCOComments SET " & _
                    "strEnforcementNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strCOEntryNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strCOComment = '" & Replace(temp3, "'", "''") & "' " & _
                    "WHERE strEnforcementNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPEnforcementItems()
        SQL = "Select * " & _
     "from " & connNameSpace & ".SSCPEnforcementItems "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strEnforcementNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strEnforcementNumber")
            End If
            If IsDBNull(drDSRow("strTrackingNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strTrackingNumber")
            End If

            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("datEnforcementFinalized")) Then
                temp4 = ""
            Else
                temp4 = Format(CDate(drDSRow("datEnforcementFinalized")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strStaffResponsible")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strStaffResponsible")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp7 = ""
            Else
                temp7 = Format(CDate(drDSRow("datModifingDate")), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strStatus")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strStatus")
            End If

            SQL = "SELECT strAIRSnumber " & _
            "FROM " & connNameSpace & ".APBMasterAIRS " & _
            "WHERE strAIRSNumber = '" & temp3 & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPEnforcementItems " & _
                "WHERE strEnforcementNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPEnforcementItems " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "') " 
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPEnforcementItems SET " & _
                    "strEnforcementNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strTrackingNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strAIRSNumber = '" & Replace(temp3, "'", "''") & "', " & _
                    "datEnforcementFinalized = '" & Replace(temp4, "'", "''") & "', " & _
                    "strStaffResponsible = '" & Replace(temp5, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp6, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp7, "'", "''") & "', " & _
                    "strStatus = '" & Replace(temp8, "'", "''") & "' " & _
                    "WHERE strEnforcementNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPEnforcementLetter()

    End Sub
    Sub TransferSSCPEnforceNOVComments()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPEnforcementNOVComments "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strEnforcementNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strEnforcementNumber")
            End If
            If IsDBNull(drDSRow("strNOVEntryNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strNOVEntryNumber")
            End If

            If IsDBNull(drDSRow("strNOVComment")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strNOVComment")
            End If

            SQL = "SELECT strEnforcementNumber " & _
            "FROM " & connNameSpace & ".SSCPEnforcementItems " & _
            "WHERE strEnforcementNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPEnforcementNOVComments " & _
                "WHERE strEnforcementNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPEnforcementNOVComments " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPEnforcementNOVComments SET " & _
                    "strEnforcementNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strNOVEntryNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strNOVComment = '" & Replace(temp3, "'", "''") & "' " & _
                    "WHERE strEnforcementNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPEnforcementStipulated()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPEnforcementStipulated "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strEnforcementNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strEnforcementNumber")
            End If
            If IsDBNull(drDSRow("strEnforcementKey")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strEnforcementKey")
            End If

            If IsDBNull(drDSRow("strStipulatedPenalty")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strStipulatedPenalty")
            End If
            If IsDBNull(drDSRow("strStipulatedPenaltyComments")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strStipulatedPenaltyComments")
            End If
            If IsDBNull(drDSRow("strAFSStipulatedPenaltyNumber")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strAFSStipulatedPenaltyNumber")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strEnforcementNumber " & _
            "FROM " & connNameSpace & ".SSCPEnforcementItems " & _
            "WHERE strEnforcementNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPEnforcementStipulated " & _
                "WHERE strEnforcementNumber = '" & temp & "' " & _
                "and strEnforcementKey = '" & temp2 & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPEnforcementStipulated " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPEnforcementStipulated SET " & _
                    "strEnforcementNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strEnforcementKey = '" & Replace(temp2, "'", "''") & "', " & _
                    "strStipulatedPenalty = '" & Replace(temp3, "'", "''") & "', " & _
                    "strStipulatedPenaltyComments = '" & Replace(temp4, "'", "''") & "', " & _
                    "strAFSStipulatedPenaltyNumber = '" & Replace(temp5, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp6, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp7, "'", "''") & "' " & _
                    "WHERE strEnforcementNumber = '" & Replace(temp, "'", "''") & "' " & _
                    "and strEnforcementKey = '" & temp2 & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPFacilityAssignment()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPFacilityAssignment "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("strSSCPUnit")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strSSCPUnit")
            End If

            If IsDBNull(drDSRow("strSSCPEngineer")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strSSCPEngineer")
            End If
            If IsDBNull(drDSRow("strSSCPAssigningManager")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strSSCPAssigningManager")
            End If
            If IsDBNull(drDSRow("datAssignmentDate")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("datAssignmentDate")
            End If

            SQL = "SELECT strAIRSNumber " & _
            "FROM " & connNameSpace & ".APBMasterAIRS " & _
            "WHERE strAIRSNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPFacilityAssignment " & _
                "WHERE strAIRSNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPFacilityAssignment " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPFacilityAssignment SET " & _
                    "strAIRSNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strSSCPUnit = '" & Replace(temp2, "'", "''") & "', " & _
                    "strSSCPEngineer = '" & Replace(temp3, "'", "''") & "', " & _
                    "strSSCPAssigningManager = '" & Replace(temp4, "'", "''") & "', " & _
                    "datAssignmentDate = '" & Replace(temp5, "'", "''") & "' " & _
                    "WHERE strAIRSNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPFCE()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPFCE "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strFCENumber")) Then
                temp = ""
            Else
                temp = drDSRow("strFCENumber")
            End If
            If IsDBNull(drDSRow("strFCEStatus")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strFCEStatus")
            End If

            If IsDBNull(drDSRow("strReviewer")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strReviewer")
            End If
            If IsDBNull(drDSRow("datFCECompleted")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("datFCECompleted")
            End If
            If IsDBNull(drDSRow("strFCEComments")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strFCEComments")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("datModifingDate")
            End If
            If IsDBNull(drDSRow("strSiteInspection")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strSiteInspection")
            End If
            If IsDBNull(drDSRow("strFCEyear")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strFCEyear")
            End If

            SQL = "SELECT strFCENumber " & _
            "FROM " & connNameSpace & ".SSCPFCEMaster " & _
            "WHERE strFCENumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPFCE " & _
                "WHERE strFCENumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPFCE " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPFCE SET " & _
                    "strFCENumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strFCEStatus = '" & Replace(temp2, "'", "''") & "', " & _
                    "strReviewer = '" & Replace(temp3, "'", "''") & "', " & _
                    "datFCECompleted = '" & Replace(temp4, "'", "''") & "', " & _
                    "strFCEComments = '" & Replace(temp5, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp6, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp7, "'", "''") & "', " & _
                    "strSiteInspection = '" & Replace(temp8, "'", "''") & "', " & _
                    "strFCEYear = '" & Replace(temp9, "'", "''") & "' " & _
                    "WHERE strFCENumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPFCEMaster()
        SQL = "Select * " & _
      "from " & connNameSpace & ".SSCPFCEMaster "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strFCENumber")) Then
                temp = ""
            Else
                temp = drDSRow("strFCENumber")
            End If
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strAIRSNumber")
            End If

            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strAIRSNumber " & _
            "FROM " & connNameSpace & ".APBMasterAIRS " & _
            "WHERE strAIRSNumber = '" & temp2 & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPFCEMaster " & _
                "WHERE strFCENumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPFCEMaster " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPFCEMaster SET " & _
                    "strFCENumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strAIRSNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp3, "'", "''") & "', " & _
                    "datModifingdate = '" & Replace(temp4, "'", "''") & "' " & _
                    "WHERE strFCENumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPInspectionActivity()
        SQL = "Select * " & _
      "from " & connNameSpace & ".SSCPInspectionActivity "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("datInspectionDateStart")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("datInspectionDateStart")
            End If

            If IsDBNull(drDSRow("datInspectionDateEnd")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("datInspectionDateEnd")
            End If
            If IsDBNull(drDSRow("strInspectingEngineer")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strInspectingEngineer")
            End If
            If IsDBNull(drDSRow("datModified")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("datModified")
            End If

            SQL = "SELECT strAIRSNumber " & _
            "FROM " & connNameSpace & ".APBMasterAIRS " & _
            "WHERE strAIRSNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPInspectionActivity " & _
                "WHERE strAIRSNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPInspectionActivity " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPInspectionActivity SET " & _
                    "strAIRSnumber = '" & Replace(temp, "'", "''") & "', " & _
                    "datInspectionDateStart = '" & Replace(temp2, "'", "''") & "', " & _
                    "datInspectionDateEnd = '" & Replace(temp3, "'", "''") & "', " & _
                    "strInspectingEngineer = '" & Replace(temp4, "'", "''") & "', " & _
                    "datModified = '" & Replace(temp5, "'", "''") & "' " & _
                    "WHERE strAIRSNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPInspections()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPInspections "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strTrackingNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strTrackingNumber")
            End If
            If IsDBNull(drDSRow("datInspectionDateStart")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("datInspectionDateStart")
            End If

            If IsDBNull(drDSRow("datInspectionDateEnd")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("datInspectionDateEnd")
            End If
            If IsDBNull(drDSRow("strInspectionReason")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strInspectionReason")
            End If
            If IsDBNull(drDSRow("strWeatherConditions")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strWeatherConditions")
            End If
            If IsDBNull(drDSRow("strInspectionGuide")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strInspectionGuide")
            End If
            If IsDBNull(drDSRow("strFacilityOperating")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strFacilityOperating")
            End If
            If IsDBNull(drDSRow("strInspectionComplianceStatus")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strInspectionComplianceStatus")
            End If
            If IsDBNull(drDSRow("strInspectionComments")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strInspectionComments")
            End If
            If IsDBNull(drDSRow("strInspectionFollowUp")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strInspectionFollowUp")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSCPItemMaster " & _
            "WHERE strTrackingNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPInspections " & _
                "WHERE strTrackingNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPInspections " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPInspections SET " & _
                    "strTrackingNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "datInspectionDateStart = '" & Replace(temp2, "'", "''") & "', " & _
                    "datInspectionDateEnd = '" & Replace(temp3, "'", "''") & "', " & _
                    "strInspectionReason = '" & Replace(temp4, "'", "''") & "', " & _
                    "strWeatherConditions = '" & Replace(temp5, "'", "''") & "', " & _
                    "strInspectionGuide = '" & Replace(temp6, "'", "''") & "', " & _
                    "strFacilityOperating = '" & Replace(temp7, "'", "''") & "', " & _
                    "strInspectionComplianceStatus = '" & Replace(temp8, "'", "''") & "', " & _
                    "strInspectionComments = '" & Replace(temp9, "'", "''") & "', " & _
                    "strInspectionFollowUp = '" & Replace(temp10, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp11, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp12, "'", "''") & "' " & _
                    "WHERE strTrackingNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPInspectionsRequired()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPInspectionsRequired "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("strInspectionRequired")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strInspectionRequired")
            End If

            If IsDBNull(drDSRow("strAssigningManager")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strAssigningManager")
            End If
            If IsDBNull(drDSRow("datAssigningDate")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("datAssigningDate")
            End If

            SQL = "SELECT strAIRSNumber " & _
            "FROM " & connNameSpace & ".APBMasterAIRS " & _
            "WHERE strAIRSNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPInspectionsRequired " & _
                "WHERE strAIRSNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPInspectionsRequired " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPInspectionsRequired SET " & _
                    "strAIRSnumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strInspectionRequired = '" & Replace(temp2, "'", "''") & "', " & _
                    "strAssigningManager = '" & Replace(temp3, "'", "''") & "', " & _
                    "datAssigningDate = '" & Replace(temp4, "'", "''") & "' " & _
                    "WHERE strAIRSNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPInspectionTracking()
        SQL = "Select * " & _
      "from " & connNameSpace & ".SSCPInspectionTracking "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("inspectionKey")) Then
                temp = ""
            Else
                temp = drDSRow("inspectionKey")
            End If
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strAIRSNumber")
            End If

            If IsDBNull(drDSRow("strInspectingEngineer")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strInspectingEngineer")
            End If
            If IsDBNull(drDSRow("strLockSchedule")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strLockSchedule")
            End If

            If IsDBNull(drDSRow("strTrackingNumber")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strTrackingNumber")
            End If
            If IsDBNull(drDSRow("datScheduledateStart")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("datScheduledateStart")
            End If
            If IsDBNull(drDSRow("datScheduledateEnd")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("datScheduledateEnd")
            End If
            If IsDBNull(drDSRow("datCurrentDateStart")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("datCurrentDateStart")
            End If
            If IsDBNull(drDSRow("datCurrentDateEnd")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("datCurrentDateEnd")
            End If
            If IsDBNull(drDSRow("datActualDateStart")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("datActualDateStart")
            End If
            If IsDBNull(drDSRow("datActualDateEnd")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("datActualDateEnd")
            End If

            SQL = "SELECT strAIRSNumber " & _
            "FROM " & connNameSpace & ".APBMasterAIRS " & _
            "WHERE strAIRSNumber = '" & temp2 & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPInspectionTracking " & _
                "WHERE strAIRSNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPInspectionTracking " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPInspectionTracking SET " & _
                    "InspectionKey = '" & Replace(temp, "'", "''") & "', " & _
                    "strAIRSNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strInspectingEngineer = '" & Replace(temp3, "'", "''") & "', " & _
                    "strLockSchedule = '" & Replace(temp4, "'", "''") & "', " & _
                    "SSCPTrackingNumber = '" & Replace(temp5, "'", "''") & "', " & _
                    "datScheduleDateStart = '" & Replace(temp6, "'", "''") & "', " & _
                    "datScheduleDateEnd = '" & Replace(temp7, "'", "''") & "', " & _
                    "datCurrentDateStart = '" & Replace(temp8, "'", "''") & "', " & _
                    "datCurrentDateEnd = '" & Replace(temp9, "'", "''") & "', " & _
                    "datActualDateStart = '" & Replace(temp10, "'", "''") & "', " & _
                    "datActualDateEnd = '" & Replace(temp11, "'", "''") & "' " & _
                    "WHERE InspectionKey = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPItemMaster()
        SQL = "Select * " & _
    "from " & connNameSpace & ".SSCPItemMaster "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strTrackingNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strTrackingNumber")
            End If
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strAIRSNumber")
            End If

            If IsDBNull(drDSRow("datReceivedDate")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("datReceivedDate")
            End If
            If IsDBNull(drDSRow("strEventType")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strEventType")
            End If

            If IsDBNull(drDSRow("strResponsibleStaff")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strResponsibleStaff")
            End If
            If IsDBNull(drDSRow("datCompleteDate")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("datCompleteDate")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("datModifingDate")
            End If
            If IsDBNull(drDSRow("strDelete")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strDelete")
            End If
            If IsDBNull(drDSRow("datAcknoledgmentLetterSent")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("datAcknoledgmentLetterSent")
            End If
            If IsDBNull(drDSRow("datInformationRequestDate")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("datInformationRequestDate")
            End If

            SQL = "SELECT strAIRSNumber " & _
            "FROM " & connNameSpace & ".APBMasterAIRS " & _
            "WHERE strAIRSNumber = '" & temp2 & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPItemMaster " & _
                "WHERE strTrackingNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPItemMaster " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPItemMaster SET " & _
                    "strTrackingNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strAIRSNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "datReceivedDate = '" & Replace(temp3, "'", "''") & "', " & _
                    "strEventType = '" & Replace(temp4, "'", "''") & "', " & _
                    "strResponsibleStaff = '" & Replace(temp5, "'", "''") & "', " & _
                    "datCompleteDate = '" & Replace(temp6, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp7, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp8, "'", "''") & "', " & _
                    "strDelete = '" & Replace(temp9, "'", "''") & "', " & _
                    "datAcknoledgmentLetterSent = '" & Replace(temp10, "'", "''") & "', " & _
                    "datInformationRequestDate = '" & Replace(temp11, "'", "''") & "' " & _
                    "WHERE strTrackingNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPNotifications()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPNotifications "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strTrackingNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strTrackingNumber")
            End If
            If IsDBNull(drDSRow("datNotificationDue")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("datNotificationDue")
            End If

            If IsDBNull(drDSRow("strNotificationDue")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strNotificationDue")
            End If
            If IsDBNull(drDSRow("datNotificationSent")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("datNotificationSent")
            End If

            If IsDBNull(drDSRow("strNotificationSent")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strNotificationSent")
            End If
            If IsDBNull(drDSRow("strNotificationType")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strNotificationType")
            End If
            If IsDBNull(drDSRow("strNotificationTypeOther")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strNotificationTypeOther")
            End If
            If IsDBNull(drDSRow("strNotificationComment")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strNotificationComment")
            End If
            If IsDBNull(drDSRow("strNotificationFollowUp")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strNotificationFollowUp")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSCPItemMaster " & _
            "WHERE strTrackingNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPNotifications " & _
                "WHERE strTrackingNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPNotifications " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPNotifications SET " & _
                    "strTrackingNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "datNotificationDue = '" & Replace(temp2, "'", "''") & "', " & _
                    "strNotificationDue = '" & Replace(temp3, "'", "''") & "', " & _
                    "datNotificationSent = '" & Replace(temp4, "'", "''") & "', " & _
                    "strNotificationSent = '" & Replace(temp5, "'", "''") & "', " & _
                    "strNotificationType = '" & Replace(temp6, "'", "''") & "', " & _
                    "strNotificationTypeOther = '" & Replace(temp7, "'", "''") & "', " & _
                    "strNotificationComment = '" & Replace(temp8, "'", "''") & "', " & _
                    "strNotificationFollowUp = '" & Replace(temp9, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp10, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp11, "'", "''") & "' " & _
                    "WHERE strTrackingNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPReports()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPReports "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strTrackingNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strTrackingNumber")
            End If
            If IsDBNull(drDSRow("strReportPeriod")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strReportPeriod")
            End If

            If IsDBNull(drDSRow("datReportingPeriodStart")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("datReportingPeriodStart")
            End If
            If IsDBNull(drDSRow("datReportingPeriodEnd")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("datReportingPeriodEnd")
            End If

            If IsDBNull(drDSRow("strReportingPeriodComments")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strReportingPeriodComments")
            End If
            If IsDBNull(drDSRow("datReportDueDate")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("datReportDueDate")
            End If
            If IsDBNull(drDSRow("datSentByFacilityDate")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("datSentByFacilityDate")
            End If
            If IsDBNull(drDSRow("strCompleteStatus")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strCompleteStatus")
            End If
            If IsDBNull(drDSRow("strEnforcementNeeded")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strEnforcementNeeded")
            End If
            If IsDBNull(drDSRow("strShowDeviation")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strShowDeviation")
            End If
            If IsDBNull(drDSRow("strGeneralComments")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strGeneralComments")
            End If

            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingdate")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("datModifingdate")
            End If
            If IsDBNull(drDSRow("strSubmittalNumber")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strSubmittalNumber")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSCPItemMaster " & _
            "WHERE strTrackingNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPReports " & _
                "WHERE strTrackingNumber = '" & temp & "' " & _
                "and strSubmittalNumber = '" & temp14 & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPReports " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPReports SET " & _
                    "strTrackingNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strReportPeriod = '" & Replace(temp2, "'", "''") & "', " & _
                    "datReportingPeriodStart = '" & Replace(temp3, "'", "''") & "', " & _
                    "datReportingPeriodEnd = '" & Replace(temp4, "'", "''") & "', " & _
                    "strReportingPeriodComments = '" & Replace(temp5, "'", "''") & "', " & _
                    "datReportDueDate = '" & Replace(temp6, "'", "''") & "', " & _
                    "datSentByFacilityDate = '" & Replace(temp7, "'", "''") & "', " & _
                    "strCompleteStatus = '" & Replace(temp8, "'", "''") & "', " & _
                    "strEnforcementNeeded = '" & Replace(temp9, "'", "''") & "', " & _
                    "strShowDeviation = '" & Replace(temp10, "'", "''") & "', " & _
                    "strGeneralComments = '" & Replace(temp11, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp12, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp13, "'", "''") & "', " & _
                    "strSubmittalNumber = '" & Replace(temp14, "'", "''") & "' " & _
                    "WHERE strTrackingNumber = '" & Replace(temp, "'", "''") & "' " & _
                    "and strSubmittalNumber = '" & Replace(temp14, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPReportsHistory()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPReportsHistory "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strTrackingNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strTrackingNumber")
            End If
            If IsDBNull(drDSRow("strSubmittalNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strSubmittalNumber")
            End If
            If IsDBNull(drDSRow("strReportPeriod")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strReportPeriod")
            End If
            If IsDBNull(drDSRow("datReportingPeriodStart")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("datReportingPeriodStart")
            End If
            If IsDBNull(drDSRow("datReportingPeriodEnd")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("datReportingPeriodEnd")
            End If

            If IsDBNull(drDSRow("strReportingPeriodComments")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strReportingPeriodComments")
            End If
            If IsDBNull(drDSRow("datReportDueDate")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("datReportDueDate")
            End If
            If IsDBNull(drDSRow("datSentByFacilityDate")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("datSentByFacilityDate")
            End If
            If IsDBNull(drDSRow("strCompleteStatus")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strCompleteStatus")
            End If
            If IsDBNull(drDSRow("strEnforcementNeeded")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strEnforcementNeeded")
            End If
            If IsDBNull(drDSRow("strShowDeviation")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strShowDeviation")
            End If
            If IsDBNull(drDSRow("strGeneralComments")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strGeneralComments")
            End If

            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingdate")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("datModifingdate")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSCPItemMaster " & _
            "WHERE strTrackingNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPReportsHistory " & _
                "WHERE strTrackingNumber = '" & temp & "' " & _
                "and strSubmittalNumber = '" & temp14 & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPReportsHistory " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPReportsHistory SET " & _
                    "strTrackingNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strSubmittalNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strReportPeriod = '" & Replace(temp3, "'", "''") & "', " & _
                    "datReportingPeriodStart = '" & Replace(temp4, "'", "''") & "', " & _
                    "datReportingPeriodEnd = '" & Replace(temp5, "'", "''") & "', " & _
                    "strReportingPeriodComments = '" & Replace(temp6, "'", "''") & "', " & _
                    "datReportDueDate = '" & Replace(temp7, "'", "''") & "', " & _
                    "datSentByFacilityDate = '" & Replace(temp8, "'", "''") & "', " & _
                    "strCompleteStatus = '" & Replace(temp9, "'", "''") & "', " & _
                    "strEnforcementNeeded = '" & Replace(temp10, "'", "''") & "', " & _
                    "strShowDeviation = '" & Replace(temp11, "'", "''") & "', " & _
                    "strGeneralComments = '" & Replace(temp12, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp13, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp14, "'", "''") & "' " & _
                    "WHERE strTrackingNumber = '" & Replace(temp, "'", "''") & "' " & _
                    "and strSubmittalNumber = '" & Replace(temp2, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSCPTestReports()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSCPTestReports "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strTrackingNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strTrackingNumber")
            End If
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strReferenceNumber")
            End If

            If IsDBNull(drDSRow("datTestReportDue")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("datTestReportDue")
            End If
            If IsDBNull(drDSRow("strTestReportComments")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strTestReportComments")
            End If

            If IsDBNull(drDSRow("strTestReportFollowup")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strTestReportFollowup")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingdDate")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("datModifingdDate")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSCPItemMaster " & _
            "WHERE strTrackingNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSCPTestReports " & _
                "WHERE strTrackingNumber = '" & temp & "' " 
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSCPTestReports " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSCPTestReports SET " & _
                    "strTrackingNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strReferenceNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "datTestReportDue = '" & Replace(temp3, "'", "''") & "', " & _
                    "strTestReportComments = '" & Replace(temp4, "'", "''") & "', " & _
                    "strTestReportFollowUp = '" & Replace(temp5, "'", "''") & "', " & _
                    "strModifingperson = '" & Replace(temp6, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp7, "'", "''") & "' " & _
                    "WHERE strTrackingNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSPPApplicationContact()
        SQL = "Select * " & _
              "from " & connNameSpace & ".SSPPApplicationContact "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strApplicationNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strApplicationNumber")
            End If
            If IsDBNull(drDSRow("strContactFirstName")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strContactFirstName")
            End If

            If IsDBNull(drDSRow("strContactLastName")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strContactLastName")
            End If
            If IsDBNull(drDSRow("strContactPrefix")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strContactPrefix")
            End If
            If IsDBNull(drDSRow("strContactSuffix")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strContactSuffix")
            End If
            If IsDBNull(drDSRow("strContactTitle")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strContactTitle")
            End If
            If IsDBNull(drDSRow("strContactCompanyName")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strContactCompanyName")
            End If
            If IsDBNull(drDSRow("strContactPhoneNumber1")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strContactPhoneNumber1")
            End If
            If IsDBNull(drDSRow("strContactFaxNumber")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strContactFaxNumber")
            End If
            If IsDBNull(drDSRow("strContactEmail")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strContactEmail")
            End If
            If IsDBNull(drDSRow("strContactAddress1")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strContactAddress1")
            End If
            If IsDBNull(drDSRow("strContactCity")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strContactCity")
            End If
            If IsDBNull(drDSRow("strContactState")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strContactState")
            End If
            If IsDBNull(drDSRow("strContactZipCode")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strContactZipCode")
            End If
            If IsDBNull(drDSRow("strContactDescription")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("strContactDescription")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSPPApplicationMaster " & _
            "WHERE strApplicationNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSPPApplicationContact " & _
                "WHERE strApplicationNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSPPApplicationContact " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSPPApplicationContact SET " & _
                    "strApplicationNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strContactFirstName = '" & Replace(temp2, "'", "''") & "', " & _
                    "strContactLastName = '" & Replace(temp3, "'", "''") & "', " & _
                    "strContactPrefix = '" & Replace(temp4, "'", "''") & "', " & _
                    "strContactSuffix = '" & Replace(temp5, "'", "''") & "', " & _
                    "strContactTitle = '" & Replace(temp6, "'", "''") & "', " & _
                    "strContactCompanyName = '" & Replace(temp7, "'", "''") & "', " & _
                    "strContactPhoneNumber1 = '" & Replace(temp8, "'", "''") & "', " & _
                    "strContactFaxNumber = '" & Replace(temp9, "'", "''") & "', " & _
                    "strContactEmail = '" & Replace(temp10, "'", "''") & "', " & _
                    "strContactAddress1 = '" & Replace(temp11, "'", "''") & "', " & _
                    "strContactCity = '" & Replace(temp12, "'", "''") & "', " & _
                    "strContactState = '" & Replace(temp13, "'", "''") & "', " & _
                    "strContactZipCode = '" & Replace(temp14, "'", "''") & "', " & _
                    "strContactDescription = '" & Replace(temp15, "'", "''") & "' " & _
                    "WHERE strApplicationNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSPPApplicationData()
        SQL = "Select * " & _
            "from " & connNameSpace & ".SSPPApplicationData "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strApplicationNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strApplicationNumber")
            End If
            If IsDBNull(drDSRow("strFacilityName")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strFacilityName")
            End If

            If IsDBNull(drDSRow("strFacilityStreet1")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strFacilityStreet1")
            End If
            If IsDBNull(drDSRow("strFacilityStreet2")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strFacilityStreet2")
            End If
            If IsDBNull(drDSRow("strFacilityCity")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strFacilityCity")
            End If
            If IsDBNull(drDSRow("strFacilityState")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strFacilityState")
            End If
            If IsDBNull(drDSRow("strFacilityZipCode")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strFacilityZipCode")
            End If
            If IsDBNull(drDSRow("strOperationalStatus")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strOperationalStatus")
            End If
            If IsDBNull(drDSRow("strClass")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strClass")
            End If
            If IsDBNull(drDSRow("strAIRProgramCodes")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strAIRProgramCodes")
            End If
            If IsDBNull(drDSRow("strSICCode")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strSICCode")
            End If
            If IsDBNull(drDSRow("strPermitNumber")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strPermitNumber")
            End If
            If IsDBNull(drDSRow("strPlantDescription")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strPlantDescription")
            End If
            If IsDBNull(drDSRow("strComments")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strComments")
            End If
            If IsDBNull(drDSRow("strApplicationNotes")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("strApplicationNotes")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("datModifingDate")
            End If
            If IsDBNull(drDSRow("strTargeted")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("strTargeted")
            End If
            If IsDBNull(drDSRow("strStateProgramCodes")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("strStateProgramCodes")
            End If
            If IsDBNull(drDSRow("strTrackedRules")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("strTrackedRules")
            End If
            If IsDBNull(drDSRow("strPAReady")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("strPAReady")
            End If
            If IsDBNull(drDSRow("strPNReady")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("strPNReady")
            End If
            If IsDBNull(drDSRow("strSignificantComments")) Then
                temp23 = ""
            Else
                temp23 = drDSRow("strSignificantComments")
            End If
            If IsDBNull(drDSRow("strAppReceivedNotification")) Then
                temp24 = ""
            Else
                temp24 = drDSRow("strAppReceivedNotification")
            End If
            If IsDBNull(drDSRow("strDraftOnWebNotification")) Then
                temp25 = ""
            Else
                temp25 = drDSRow("strDraftOnWebNotification")
            End If
            If IsDBNull(drDSRow("strFinalOnWebNotification")) Then
                temp26 = ""
            Else
                temp26 = drDSRow("strFinalOnWebNotification")
            End If
            If IsDBNull(drDSRow("strPublicInvolvement")) Then
                temp27 = ""
            Else
                temp27 = drDSRow("strPublicInvolvement")
            End If
            If IsDBNull(drDSRow("strPAPNComments")) Then
                temp28 = ""
            Else
                temp28 = drDSRow("strPAPNComments")
            End If
            If IsDBNull(drDSRow("strPAPosted")) Then
                temp29 = ""
            Else
                temp29 = drDSRow("strPAPosted")
            End If
            If IsDBNull(drDSRow("strPNPosted")) Then
                temp30 = ""
            Else
                temp30 = drDSRow("strPNPosted")
            End If
            If IsDBNull(drDSRow("strSSCPUnit")) Then
                temp31 = ""
            Else
                temp31 = drDSRow("strSSCPUnit")
            End If
            If IsDBNull(drDSRow("strSSCPReviewer")) Then
                temp32 = ""
            Else
                temp32 = drDSRow("strSSCPReviewer")
            End If
            If IsDBNull(drDSRow("strSSCPComments")) Then
                temp33 = ""
            Else
                temp33 = drDSRow("strSSCPComments")
            End If
            If IsDBNull(drDSRow("strISMPUnit")) Then
                temp34 = ""
            Else
                temp34 = drDSRow("strISMPUnit")
            End If
            If IsDBNull(drDSRow("strISMPReviewer")) Then
                temp35 = ""
            Else
                temp35 = drDSRow("strISMPReviewer")
            End If
            If IsDBNull(drDSRow("strISMPComments")) Then
                temp36 = ""
            Else
                temp36 = drDSRow("strISMPComments")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSPPApplicationMaster " & _
            "WHERE strApplicationNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSPPApplicationData " & _
                "WHERE strApplicationNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSPPApplicationData " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                    "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                    "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                    "'" & Replace(temp21, "'", "''") & "', '" & Replace(temp22, "'", "''") & "', " & _
                    "'" & Replace(temp23, "'", "''") & "', '" & Replace(temp24, "'", "''") & "', " & _
                    "'" & Replace(temp25, "'", "''") & "', '" & Replace(temp26, "'", "''") & "', " & _
                    "'" & Replace(temp27, "'", "''") & "', '" & Replace(temp28, "'", "''") & "', " & _
                    "'" & Replace(temp29, "'", "''") & "', '" & Replace(temp30, "'", "''") & "', " & _
                    "'" & Replace(temp31, "'", "''") & "', '" & Replace(temp32, "'", "''") & "', " & _
                    "'" & Replace(temp33, "'", "''") & "', '" & Replace(temp34, "'", "''") & "', " & _
                    "'" & Replace(temp35, "'", "''") & "', '" & Replace(temp36, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSPPApplicationData SET " & _
                    "strApplicationNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strFacilityName = '" & Replace(temp2, "'", "''") & "', " & _
                    "strFacilityStreet1 = '" & Replace(temp3, "'", "''") & "', " & _
                    "strFacilityStreet2 = '" & Replace(temp4, "'", "''") & "', " & _
                    "strFacilityCity = '" & Replace(temp5, "'", "''") & "', " & _
                    "strFacilityState = '" & Replace(temp6, "'", "''") & "', " & _
                    "strFacilityZipCode = '" & Replace(temp7, "'", "''") & "', " & _
                    "strOperationalStatus = '" & Replace(temp8, "'", "''") & "', " & _
                    "strClass = '" & Replace(temp9, "'", "''") & "', " & _
                    "strAirProgramCodes = '" & Replace(temp10, "'", "''") & "', " & _
                    "strSICCode = '" & Replace(temp11, "'", "''") & "', " & _
                    "strPermitNumber = '" & Replace(temp12, "'", "''") & "', " & _
                    "strPlantDescription = '" & Replace(temp13, "'", "''") & "', " & _
                    "strComments = '" & Replace(temp14, "'", "''") & "', " & _
                    "strApplicationNotes = '" & Replace(temp15, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp16, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp17, "'", "''") & "', " & _
                    "strTargeted = '" & Replace(temp18, "'", "''") & "', " & _
                    "strStateProgramCodes = '" & Replace(temp19, "'", "''") & "', " & _
                    "strTrackedRules = '" & Replace(temp20, "'", "''") & "', " & _
                    "strPAReady = '" & Replace(temp21, "'", "''") & "', " & _
                    "strPNReady = '" & Replace(temp22, "'", "''") & "', " & _
                    "strSignificantComments = '" & Replace(temp23, "'", "''") & "', " & _
                    "strAppReceivedNotification = '" & Replace(temp24, "'", "''") & "', " & _
                    "strDraftOnWebNotification = '" & Replace(temp25, "'", "''") & "', " & _
                    "strFinalOnWebNotification = '" & Replace(temp26, "'", "''") & "', " & _
                    "strPublicInvolvement = '" & Replace(temp27, "'", "''") & "', " & _
                    "strPAPNComments = '" & Replace(temp28, "'", "''") & "', " & _
                    "strPAPosted = '" & Replace(temp29, "'", "''") & "', " & _
                    "strPNPosted = '" & Replace(temp30, "'", "''") & "', " & _
                    "strSSCPUnit = '" & Replace(temp31, "'", "''") & "', " & _
                    "strSSCPReviewer = '" & Replace(temp32, "'", "''") & "', " & _
                    "strSSCPComments = '" & Replace(temp33, "'", "''") & "', " & _
                    "strISMPUnit = '" & Replace(temp34, "'", "''") & "', " & _
                    "strISMPReviewer = '" & Replace(temp35, "'", "''") & "', " & _
                    "strISMPComments = '" & Replace(temp36, "'", "''") & "' " & _
                    "WHERE strApplicationNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSPPApplicationInformation()
        SQL = "Select * " & _
            "from " & connNameSpace & ".SSPPApplicationInformation "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strApplicationNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strApplicationNumber")
            End If
            If IsDBNull(drDSRow("strRequestKey")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strRequestKey")
            End If

            If IsDBNull(drDSRow("datInformationRequested")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("datInformationRequested")
            End If
            If IsDBNull(drDSRow("strInformationRequested")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strInformationRequested")
            End If
            If IsDBNull(drDSRow("datInformationReceived")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("datInformationReceived")
            End If
            If IsDBNull(drDSRow("strInformationRecieved")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strInformationRecieved")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("datModifingDate")
            End If
         
            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSPPApplicationMaster " & _
            "WHERE strApplicationNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSPPApplicationInformation " & _
                "WHERE strApplicationNumber = '" & temp & "' " & _
                "and strRequestKey = '" & temp2 & "' "

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSPPApplicationInformation " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSPPApplicationInformation SET " & _
                    "strApplicationNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strRequestKey = '" & Replace(temp2, "'", "''") & "', " & _
                    "datInformationRequested = '" & Replace(temp3, "'", "''") & "', " & _
                    "strInformationRequested = '" & Replace(temp4, "'", "''") & "', " & _
                    "datInformationReceived = '" & Replace(temp5, "'", "''") & "', " & _
                    "strInformationRecieved = '" & Replace(temp6, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp7, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp8, "'", "''") & "' " & _
                    "WHERE strApplicationNumber = '" & Replace(temp, "'", "''") & "' " & _
                    "and strRequestKey = '" & temp2 & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSPPApplicationLinking()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSPPApplicationLinking "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strApplicationNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strApplicationNumber")
            End If
            If IsDBNull(drDSRow("strMasterApplication")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strMasterApplication")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSPPApplicationMaster " & _
            "WHERE strApplicationNumber = '" & temp2 & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSPPApplicationLinking " & _
                "WHERE strApplicationNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSPPApplicationLinking " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSPPApplicationLinking SET " & _
                    "strApplicationNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strMasterApplication = '" & Replace(temp2, "'", "''") & "' " & _
                    "WHERE strMasterApplication = '" & Replace(temp2, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSPPApplicationMaster()
        SQL = "Select * " & _
            "from " & connNameSpace & ".SSPPApplicationMaster "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strApplicationNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strApplicationNumber")
            End If
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strAIRSNumber")
            End If

            If IsDBNull(drDSRow("strStaffResponsible")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strStaffResponsible")
            End If
            If IsDBNull(drDSRow("strApplicationType")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strApplicationType")
            End If
            If IsDBNull(drDSRow("strPermitType")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strPermitType")
            End If
            If IsDBNull(drDSRow("APBUnit")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("APBUnit")
            End If
            If IsDBNull(drDSRow("DatFinalizedDate")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("DatFinalizedDate")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".APBMasterAIRS " & _
            "WHERE strAIRSNumber = '" & temp2 & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSPPApplicationMaster " & _
                "WHERE strApplicationNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSPPApplicationMaster " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSPPApplicationMaster SET " & _
                    "strApplicationNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strAIRSNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strStaffResponsible = '" & Replace(temp3, "'", "''") & "', " & _
                    "strApplicationType = '" & Replace(temp4, "'", "''") & "', " & _
                    "strPermitType = '" & Replace(temp5, "'", "''") & "', " & _
                    "APBUnit = '" & Replace(temp6, "'", "''") & "', " & _
                    "datFinalizedDate = '" & Replace(temp7, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp8, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp9, "'", "''") & "' " & _
                    "WHERE strApplicationNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSPPApplicationQuality()
        SQL = "Select * " & _
            "from " & connNameSpace & ".SSPPApplicationQuality "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strApplicationNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strApplicationNumber")
            End If
            If IsDBNull(drDSRow("strDifficulty")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strDifficulty")
            End If

            If IsDBNull(drDSRow("strGrammer")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strGrammer")
            End If
            If IsDBNull(drDSRow("strTechnical")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strTechnical")
            End If
            If IsDBNull(drDSRow("strComments")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strComments")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("datModifingDate")
            End If
         
            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSPPApplicationMaster " & _
            "WHERE strApplicationNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSPPApplicationQuality " & _
                "WHERE strApplicationNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSPPApplicationQuality " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSPPApplicationQuality SET " & _
                    "strApplicationNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strDifficulty = '" & Replace(temp2, "'", "''") & "', " & _
                    "strGrammer = '" & Replace(temp3, "'", "''") & "', " & _
                    "strTechnical = '" & Replace(temp4, "'", "''") & "', " & _
                    "strComments = '" & Replace(temp5, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp6, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp7, "'", "''") & "' " & _
                    "WHERE strApplicationNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSPPApplicationTracking()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSPPApplicationTracking "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strApplicationNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strApplicationNumber")
            End If
            If IsDBNull(drDSRow("strSubmittalNumber-")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strSubmittalNumber")
            End If

            If IsDBNull(drDSRow("datApplicationStarted")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("datApplicationStarted")
            End If
            If IsDBNull(drDSRow("datReceivedDate")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("datReceivedDate")
            End If
            If IsDBNull(drDSRow("datSentByFacility")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("datSentByFacility")
            End If
            If IsDBNull(drDSRow("datAssignedToEngineer")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("datAssignedToEngineer")
            End If
            If IsDBNull(drDSRow("datReAssignedToEngineer")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("datReAssignedToEngineer")
            End If
            If IsDBNull(drDSRow("datApplicationPackageComplete")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("datApplicationPackageComplete")
            End If
            If IsDBNull(drDSRow("datAcknowledgementLetterSent")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("datAcknowledgementLetterSent")
            End If
            If IsDBNull(drDSRow("datToPMI")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("datToPMI")
            End If
            If IsDBNull(drDSRow("datToPMII")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("datToPMII")
            End If
            If IsDBNull(drDSRow("datReturnedToEngineer")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("datReturnedToEngineer")
            End If
            If IsDBNull(drDSRow("datPermitIssued")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("datPermitIssued")
            End If
            If IsDBNull(drDSRow("datApplicationDeadline")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("datApplicationDeadline")
            End If
            If IsDBNull(drDSRow("datWithdrawn")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("datWithdrawn")
            End If
            If IsDBNull(drDSRow("datDraftIssued")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("datDraftIssued")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("datModifingDate")
            End If
            If IsDBNull(drDSRow("datDraftOnWeb")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("datDraftOnWeb")
            End If
            If IsDBNull(drDSRow("datEPAStatesNotified")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("datEPAStatesNotified")
            End If
            If IsDBNull(drDSRow("datFinalOnWeb")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("datFinalOnWeb")
            End If
            If IsDBNull(drDSRow("datEPANotified")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("datEPANotified")
            End If
            If IsDBNull(drDSRow("datEffective")) Then
                temp23 = ""
            Else
                temp23 = drDSRow("datEffective")
            End If
            If IsDBNull(drDSRow("datEPAStatesNotifiedAppRec")) Then
                temp24 = ""
            Else
                temp24 = drDSRow("datEPAStatesNotifiedAppRec")
            End If
            If IsDBNull(drDSRow("datExperationDate")) Then
                temp25 = ""
            Else
                temp25 = drDSRow("datExperationDate")
            End If
            If IsDBNull(drDSRow("datEPAWaived")) Then
                temp26 = ""
            Else
                temp26 = drDSRow("datEPAWaived")
            End If
            If IsDBNull(drDSRow("datEPAEnds")) Then
                temp27 = ""
            Else
                temp27 = drDSRow("datEPAEnds")
            End If
            If IsDBNull(drDSRow("datToBranchCheif")) Then
                temp28 = ""
            Else
                temp28 = drDSRow("datToBranchCheif")
            End If
            If IsDBNull(drDSRow("datToDirector")) Then
                temp29 = ""
            Else
                temp29 = drDSRow("datToDirector")
            End If
            If IsDBNull(drDSRow("datPAExpires")) Then
                temp30 = ""
            Else
                temp30 = drDSRow("datPAExpires")
            End If
            If IsDBNull(drDSRow("datPNExpires")) Then
                temp31 = ""
            Else
                temp31 = drDSRow("datPNExpires")
            End If
            If IsDBNull(drDSRow("datReviewSubmitted")) Then
                temp32 = ""
            Else
                temp32 = drDSRow("datReviewSubmitted")
            End If
            If IsDBNull(drDSRow("datSSCPReviewDate")) Then
                temp33 = ""
            Else
                temp33 = drDSRow("datSSCPReviewDate")
            End If
            If IsDBNull(drDSRow("datISMPReviewDate")) Then
                temp34 = ""
            Else
                temp34 = drDSRow("datISMPReviewDate")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSPPApplicationMaster " & _
            "WHERE strApplicationNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSPPApplicationContact " & _
                "WHERE strApplicationNumber = '" & temp & "' " & _
                "and strSubmittalNumber = '" & temp2 & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSPPApplicationContact " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                    "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                    "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                    "'" & Replace(temp21, "'", "''") & "', '" & Replace(temp22, "'", "''") & "', " & _
                    "'" & Replace(temp23, "'", "''") & "', '" & Replace(temp24, "'", "''") & "', " & _
                    "'" & Replace(temp25, "'", "''") & "', '" & Replace(temp26, "'", "''") & "', " & _
                    "'" & Replace(temp27, "'", "''") & "', '" & Replace(temp28, "'", "''") & "', " & _
                    "'" & Replace(temp29, "'", "''") & "', '" & Replace(temp30, "'", "''") & "', " & _
                    "'" & Replace(temp31, "'", "''") & "', '" & Replace(temp32, "'", "''") & "', " & _
                    "'" & Replace(temp33, "'", "''") & "', '" & Replace(temp34, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSPPApplicationContact SET " & _
                    "strApplicationNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strSubmittalNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "datApplicationStarted = '" & Replace(temp3, "'", "''") & "', " & _
                    "datReceivedDate = '" & Replace(temp4, "'", "''") & "', " & _
                    "datSentByFacility = '" & Replace(temp5, "'", "''") & "', " & _
                    "datAssignedToEngineer = '" & Replace(temp6, "'", "''") & "', " & _
                    "datReassignedToEngineer = '" & Replace(temp7, "'", "''") & "', " & _
                    "datApplicationPackageComplete = '" & Replace(temp8, "'", "''") & "', " & _
                    "datAcknowledgementLetterSent = '" & Replace(temp9, "'", "''") & "', " & _
                    "datToPMI = '" & Replace(temp10, "'", "''") & "', " & _
                    "datToPMII = '" & Replace(temp11, "'", "''") & "', " & _
                    "datReturnedToEngineer = '" & Replace(temp12, "'", "''") & "', " & _
                    "datPermitIssued = '" & Replace(temp13, "'", "''") & "', " & _
                    "datApplicationDeadline = '" & Replace(temp14, "'", "''") & "', " & _
                    "datWithdrawn = '" & Replace(temp15, "'", "''") & "', " & _
                    "datDraftIssued = '" & Replace(temp16, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp17, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp18, "'", "''") & "', " & _
                    "datDraftOnWeb = '" & Replace(temp19, "'", "''") & "', " & _
                    "datEPAStatesNotified = '" & Replace(temp20, "'", "''") & "', " & _
                    "datFinalOnWeb = '" & Replace(temp21, "'", "''") & "', " & _
                    "datEPANotified = '" & Replace(temp22, "'", "''") & "', " & _
                    "datEffective = '" & Replace(temp23, "'", "''") & "', " & _
                    "datEPAStatesNotifiedAppRec = '" & Replace(temp24, "'", "''") & "', " & _
                    "datExperationDate = '" & Replace(temp25, "'", "''") & "', " & _
                    "datEPAWaived = '" & Replace(temp26, "'", "''") & "', " & _
                    "datEPAEnds = '" & Replace(temp27, "'", "''") & "', " & _
                    "datToBranchCheif = '" & Replace(temp28, "'", "''") & "', " & _
                    "datToDirector = '" & Replace(temp29, "'", "''") & "', " & _
                    "datPAExpires = '" & Replace(temp30, "'", "''") & "', " & _
                    "datPNExpires = '" & Replace(temp31, "'", "''") & "', " & _
                    "datReviewSubmitted = '" & Replace(temp32, "'", "''") & "', " & _
                    "datSSCPReviewDate = '" & Replace(temp33, "'", "''") & "', " & _
                    "datISMPReviewDate = '" & Replace(temp34, "'", "''") & "' " & _
                    "WHERE strApplicationNumber = '" & Replace(temp, "'", "''") & "' " & _
                    "and strSubmittalNumber = '" & temp2 & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSPPCDS()
        SQL = "Select * " & _
        "from " & connNameSpace & ".SSPPCDS "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strApplicationNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strApplicationNumber")
            End If
            If IsDBNull(drDSRow("strFacilityName")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strFacilityName")
            End If

            If IsDBNull(drDSRow("strFacilityStreet1")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strFacilityStreet1")
            End If
            If IsDBNull(drDSRow("strFacilityStreet2")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strFacilityStreet2")
            End If
            If IsDBNull(drDSRow("strFacilityCity")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strFacilityCity")
            End If
            If IsDBNull(drDSRow("strFacilityState")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("strFacilityState")
            End If
            If IsDBNull(drDSRow("strFacilityZipCode")) Then
                temp7 = ""
            Else
                temp7 = drDSRow("strFacilityZipCode")
            End If
            If IsDBNull(drDSRow("strOperationalStatus")) Then
                temp8 = ""
            Else
                temp8 = drDSRow("strOperationalStatus")
            End If
            If IsDBNull(drDSRow("strClass")) Then
                temp9 = ""
            Else
                temp9 = drDSRow("strClass")
            End If
            If IsDBNull(drDSRow("strAirProgramCodes")) Then
                temp10 = ""
            Else
                temp10 = drDSRow("strAirProgramCodes")
            End If
            If IsDBNull(drDSRow("strSICCode")) Then
                temp11 = ""
            Else
                temp11 = drDSRow("strSICCode")
            End If
            If IsDBNull(drDSRow("strPermitNumber")) Then
                temp12 = ""
            Else
                temp12 = drDSRow("strPermitNumber")
            End If
            If IsDBNull(drDSRow("strPlantDescription")) Then
                temp13 = ""
            Else
                temp13 = drDSRow("strPlantDescription")
            End If
            If IsDBNull(drDSRow("strActionType")) Then
                temp14 = ""
            Else
                temp14 = drDSRow("strActionType")
            End If
            If IsDBNull(drDSRow("strActionComments")) Then
                temp15 = ""
            Else
                temp15 = drDSRow("strActionComments")
            End If
            If IsDBNull(drDSRow("datEngineerComplete")) Then
                temp16 = ""
            Else
                temp16 = drDSRow("datEngineerComplete")
            End If
            If IsDBNull(drDSRow("strEngineerComplete")) Then
                temp17 = ""
            Else
                temp17 = drDSRow("strEngineerComplete")
            End If
            If IsDBNull(drDSRow("datPMIComplete")) Then
                temp18 = ""
            Else
                temp18 = drDSRow("datPMIComplete")
            End If
            If IsDBNull(drDSRow("strPMIComplete")) Then
                temp19 = ""
            Else
                temp19 = drDSRow("strPMIComplete")
            End If
            If IsDBNull(drDSRow("datPMIIComplete")) Then
                temp20 = ""
            Else
                temp20 = drDSRow("datPMIIComplete")
            End If
            If IsDBNull(drDSRow("strPMIIComplete")) Then
                temp21 = ""
            Else
                temp21 = drDSRow("strPMIIComplete")
            End If
            If IsDBNull(drDSRow("datDirectorComplete")) Then
                temp22 = ""
            Else
                temp22 = drDSRow("datDirectorComplete")
            End If
            If IsDBNull(drDSRow("strDirectorComplete")) Then
                temp23 = ""
            Else
                temp23 = drDSRow("strDirectorComplete")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp24 = ""
            Else
                temp24 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp25 = ""
            Else
                temp25 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSPPApplicationMaster " & _
            "WHERE strApplicationNumber = '" & temp & "' "
            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".SSPPCDS " & _
                "WHERE strApplicationNumber = '" & temp & "' "
                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".SSPPCDS " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "', " & _
                    "'" & Replace(temp7, "'", "''") & "', '" & Replace(temp8, "'", "''") & "', " & _
                    "'" & Replace(temp9, "'", "''") & "', '" & Replace(temp10, "'", "''") & "', " & _
                    "'" & Replace(temp11, "'", "''") & "', '" & Replace(temp12, "'", "''") & "', " & _
                    "'" & Replace(temp13, "'", "''") & "', '" & Replace(temp14, "'", "''") & "', " & _
                    "'" & Replace(temp15, "'", "''") & "', '" & Replace(temp16, "'", "''") & "', " & _
                    "'" & Replace(temp17, "'", "''") & "', '" & Replace(temp18, "'", "''") & "', " & _
                    "'" & Replace(temp19, "'", "''") & "', '" & Replace(temp20, "'", "''") & "', " & _
                    "'" & Replace(temp21, "'", "''") & "', '" & Replace(temp22, "'", "''") & "', " & _
                    "'" & Replace(temp23, "'", "''") & "', '" & Replace(temp24, "'", "''") & "', " & _
                    "'" & Replace(temp25, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".SSPPCDS SET " & _
                    "strApplicationNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strFacilityName = '" & Replace(temp2, "'", "''") & "', " & _
                    "strFacilityStreet1 = '" & Replace(temp3, "'", "''") & "', " & _
                    "strFacilityStreet2 = '" & Replace(temp4, "'", "''") & "', " & _
                    "strFacilityCity = '" & Replace(temp5, "'", "''") & "', " & _
                    "strFacilityState = '" & Replace(temp6, "'", "''") & "', " & _
                    "strFacilityZipCode = '" & Replace(temp7, "'", "''") & "', " & _
                    "strOperationalStatus = '" & Replace(temp8, "'", "''") & "', " & _
                    "strClass = '" & Replace(temp9, "'", "''") & "', " & _
                    "strAirProgramCodes = '" & Replace(temp10, "'", "''") & "', " & _
                    "strSICCode = '" & Replace(temp11, "'", "''") & "', " & _
                    "strPermitNumber = '" & Replace(temp12, "'", "''") & "', " & _
                    "strPlantDescription = '" & Replace(temp13, "'", "''") & "', " & _
                    "strActionType = '" & Replace(temp14, "'", "''") & "', " & _
                    "strActionComments = '" & Replace(temp15, "'", "''") & "', " & _
                    "datEngineerComplete = '" & Replace(temp16, "'", "''") & "', " & _
                    "strEngineerComplete = '" & Replace(temp17, "'", "''") & "', " & _
                    "datPMIComplete = '" & Replace(temp18, "'", "''") & "', " & _
                    "strPMIComplete = '" & Replace(temp19, "'", "''") & "', " & _
                    "datPMIIComplete = '" & Replace(temp20, "'", "''") & "', " & _
                    "strPMIIComplete = '" & Replace(temp21, "'", "''") & "', " & _
                    "datDirectorComplete = '" & Replace(temp22, "'", "''") & "', " & _
                    "strDirectorComplete = '" & Replace(temp23, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp24, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp25, "'", "''") & "' " & _
                    "WHERE strApplicationNumber = '" & Replace(temp, "'", "''") & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferSSPPPublicLetters()

    End Sub
    Sub TransferAFSAirPollutantDate()
        SQL = "Select * " & _
        "from " & connNameSpace & ".AFSAirPollutantData "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strAIRPollutantKey")) Then
                temp = ""
            Else
                temp = drDSRow("strAIRPollutantKey")
            End If
            If IsDBNull(drDSRow("strPollutantKey")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strPollutantKey")
            End If

            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("strUpDateStatus")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strUpDateStatus")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("strComments")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp6 = ""
            Else
                temp6 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".APBAirProgramPollutants " & _
            "WHERE strAirPollutantKey = '" & temp & "' " & _
            "and strPollutantKey = '" & temp2 & "' "

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".AFSAirPollutantData " & _
                "WHERE strAirPollutantKey = '" & temp & "' " & _
                "and strPollutantKey = '" & temp2 & "' "

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".AFSAirPollutantData " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "', '" & Replace(temp6, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".AFSAirPollutantData SET " & _
                    "strAIRPollutantKey = '" & Replace(temp, "'", "''") & "', " & _
                    "strPollutantKey = '" & Replace(temp2, "'", "''") & "', " & _
                    "strAIRSNumber = '" & Replace(temp3, "'", "''") & "', " & _
                    "strUpDateStatus = '" & Replace(temp4, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp5, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp6, "'", "''") & "' " & _
                    "WHERE strAirPollutantKey = '" & temp & "' " & _
                    "and strPollutantKey = '" & temp2 & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferAFSBatchFile()

    End Sub
    Sub TransferAFSFacilityData()
        SQL = "Select * " & _
        "from " & connNameSpace & ".AFSFacilityData "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strAIRSNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strAIRSNumber")
            End If
            If IsDBNull(drDSRow("strUpdateStatus")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strUpdateStatus")
            End If

            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strAIRSNumber " & _
            "FROM " & connNameSpace & ".ABPMasterAIRS " & _
            "WHERE strAIRSNumber = '" & temp & "' "

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".AFSFacilityData " & _
                "WHERE strAIRSNumber = '" & temp & "' "

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".AFSFacilityData " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".AFSFacilityData SET " & _
                    "strAIRSNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strUpdateStatus = '" & Replace(temp2, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp3, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp4, "'", "''") & "' " & _
                    "WHERE strAIRSNumber = '" & temp & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferAFSISMPRecords()
        SQL = "Select * " & _
        "from " & connNameSpace & ".AFSISMPRecords "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strReferenceNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strReferenceNumber")
            End If
            If IsDBNull(drDSRow("strAFSActionNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strAFSActionNumber")
            End If

            If IsDBNull(drDSRow("strUpDateStatus")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strUpDateStatus")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strReferenceNumber " & _
            "FROM " & connNameSpace & ".ISMPMaster " & _
            "WHERE strReferenceNumber = '" & temp & "' "

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".AFSISMPRecords " & _
                "WHERE strReferenceNumber = '" & temp & "' "

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".AFSISMPRecords " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".AFSISMPRecords SET " & _
                    "strReferenceNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strAFSActionNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strUpDateStatus = '" & Replace(temp3, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp4, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp5, "'", "''") & "' " & _
                    "WHERE strReferenceNumber = '" & temp & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferAFSSSCPEnforcementRecords()
        SQL = "Select * " & _
        "from " & connNameSpace & ".AFSSSCPEnforcementRecords "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strEnforcementNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strEnforcementNumber")
            End If
            If IsDBNull(drDSRow("strAFSActionNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strAFSActionNumber")
            End If

            If IsDBNull(drDSRow("strUpDateStatus")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strUpDateStatus")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strEnforcementNumber " & _
            "FROM " & connNameSpace & ".SSCPEnforcementItems " & _
            "WHERE strEnforcementNumber = '" & temp & "' "

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".AFSSSCPEnforcementRecords " & _
                "WHERE strEnforcementNumber = '" & temp & "' "

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".AFSSSCPEnforcementRecords " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".AFSSSCPEnforcementRecords SET " & _
                    "strEnforcementNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strAFSActionNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strUpDateStatus = '" & Replace(temp3, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp4, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp5, "'", "''") & "' " & _
                    "WHERE strEnforcementNumber = '" & temp & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferAFSSSCPFCERecords()
        SQL = "Select * " & _
        "from " & connNameSpace & ".AFSSSCPFCERecords "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strFCENumber")) Then
                temp = ""
            Else
                temp = drDSRow("strFCENumber")
            End If
            If IsDBNull(drDSRow("strAFSActionNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strAFSActionNumber")
            End If

            If IsDBNull(drDSRow("strUpDateStatus")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strUpDateStatus")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strFCENumber " & _
            "FROM " & connNameSpace & ".SSCPFCEMaster " & _
            "WHERE strFCENumber = '" & temp & "' "

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".AFSSSCPFCERecords " & _
                "WHERE strFCENumber = '" & temp & "' "

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".AFSSSCPFCERecords " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".AFSSSCPFCERecords SET " & _
                    "strFCENumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strAFSActionNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strUpDateStatus = '" & Replace(temp3, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp4, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp5, "'", "''") & "' " & _
                    "WHERE strFCENumber = '" & temp & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferAFSSSCPRecords()
        SQL = "Select * " & _
        "from " & connNameSpace & ".AFSSSCPRecords "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strTrackingNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strTrackingNumber")
            End If
            If IsDBNull(drDSRow("strAFSActionNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strAFSActionNumber")
            End If

            If IsDBNull(drDSRow("strUpDateStatus")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strUpDateStatus")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strTrackingNumber " & _
            "FROM " & connNameSpace & ".SSCPItemMaster " & _
            "WHERE strTrackingNumber = '" & temp & "' "

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".AFSSSCPRecords " & _
                "WHERE strTrackingNumber = '" & temp & "' "

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".AFSSSCPRecords " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".AFSSSCPRecords SET " & _
                    "strTrackingNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strAFSActionNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strUpDateStatus = '" & Replace(temp3, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp4, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp5, "'", "''") & "' " & _
                    "WHERE strTrackingNumber = '" & temp & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Sub TransferAFSSSPPRecords()
        SQL = "Select * " & _
        "from " & connNameSpace & ".AFSSSPPRecords "

        dsTemp = New DataSet
        daTemp = New OracleDataAdapter(SQL, conn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daTemp.Fill(dsTemp, "Temp")

        For Each drDSRow In dsTemp.Tables("Temp").Select()
            If IsDBNull(drDSRow("strApplicationNumber")) Then
                temp = ""
            Else
                temp = drDSRow("strApplicationNumber")
            End If
            If IsDBNull(drDSRow("strAFSActionNumber")) Then
                temp2 = ""
            Else
                temp2 = drDSRow("strAFSActionNumber")
            End If

            If IsDBNull(drDSRow("strUpDateStatus")) Then
                temp3 = ""
            Else
                temp3 = drDSRow("strUpDateStatus")
            End If
            If IsDBNull(drDSRow("strModifingPerson")) Then
                temp4 = ""
            Else
                temp4 = drDSRow("strModifingPerson")
            End If
            If IsDBNull(drDSRow("datModifingDate")) Then
                temp5 = ""
            Else
                temp5 = drDSRow("datModifingDate")
            End If

            SQL = "SELECT strApplicationNumber " & _
            "FROM " & connNameSpace & ".SSPPApplicationMaster " & _
            "WHERE strApplicationNumber = '" & temp & "' "

            cmd = New OracleCommand(SQL, transferConn)
            If transferConn.State = ConnectionState.Closed Then
                transferConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "SELECT COUNT(*) " & _
                "FROM " & connNameSpace & ".AFSSSPPRecords " & _
                "WHERE strTrackingNumber = '" & temp & "' "

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = False Or dr.Item(0) = 0 Then
                    SQL = "Insert into " & connNameSpace & ".AFSSSPPRecords " & _
                    "Values " & _
                    "('" & Replace(temp, "'", "''") & "', '" & Replace(temp2, "'", "''") & "', " & _
                    "'" & Replace(temp3, "'", "''") & "', '" & Replace(temp4, "'", "''") & "', " & _
                    "'" & Replace(temp5, "'", "''") & "') "
                Else
                    SQL = "UPDATE " & connNameSpace & ".AFSSSPPRecords SET " & _
                    "strApplicationNumber = '" & Replace(temp, "'", "''") & "', " & _
                    "strAFSActionNumber = '" & Replace(temp2, "'", "''") & "', " & _
                    "strUpDateStatus = '" & Replace(temp3, "'", "''") & "', " & _
                    "strModifingPerson = '" & Replace(temp4, "'", "''") & "', " & _
                    "datModifingDate = '" & Replace(temp5, "'", "''") & "' " & _
                    "WHERE strApplicationNumber = '" & temp & "' "
                End If
                dr.Close()

                cmd = New OracleCommand(SQL, transferConn)
                If transferConn.State = ConnectionState.Closed Then
                    transferConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
        Next
    End Sub
    Private Sub bgwTransfer_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwTransfer.RunWorkerCompleted
        btnTransferData.Enabled = True
        btnClearSelection.Enabled = True
        lblTransfer.Text = "Data Transfer Complete."

    End Sub
#End Region
    Private Sub btnPreLoadNewFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreLoadNewFacility.Click
        Try
            If txtApplicationNumber.Text <> "App No." Then
                SQL = "select " & _
                "strFacilityName, strFacilityStreet1, " & _
                "strFacilityCity, strFacilityZipCode, " & _
                "strOperationalStatus, strClass, " & _
                "strAirProgramCodes, strSICCode, " & _
                "strPlantDescription, strContactFirstName, " & _
                "strContactLastName, strContactpreFix, " & _
                "strContactSuffix, strContactTitle, " & _
                "strContactPhoneNumber1 " & _
                "from " & connNameSpace & ".SSPPApplicationdata, " & _
                "" & connNameSpace & ".SSPPApplicationContact " & _
                "where " & connNameSpace & ".SSPPApplicationData.strApplicationNumber = " & connNameSpace & ".SSPPApplicationContact.strApplicationNumber " & _
                "and " & connNameSpace & ".SSPPApplicationData.strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        txtCDSFacilityName.Clear()
                    Else
                        txtCDSFacilityName.Text = dr.Item("strFacilityname")
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        txtCDSStreetAddress.Clear()
                        txtMailingAddress.Clear()
                    Else
                        txtCDSStreetAddress.Text = dr.Item("strFacilityStreet1")
                        txtMailingAddress.Text = dr.Item("strFacilityStreet1")
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        txtCDSCity.Clear()
                        txtMailingCity.Clear()
                    Else
                        txtCDSCity.Text = dr.Item("strFacilityCity")
                        txtMailingCity.Text = dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        mtbCDSZipCode.Clear()
                        mtbMailingZipCode.Clear()
                    Else
                        mtbCDSZipCode.Text = dr.Item("strFacilityZipCode")
                        mtbMailingZipCode.Text = dr.Item("strFacilityZipCode")
                    End If
                    If IsDBNull(dr.Item("strOperationalStatus")) Then
                        cboCDSOperationalStatus.Text = ""
                    Else
                        temp = dr.Item("strOperationalStatus")
                        Select Case temp.ToString
                            Case "O"
                                cboCDSOperationalStatus.Text = "O - Operating"
                            Case "P"
                                cboCDSOperationalStatus.Text = "P - Planned"
                            Case "C"
                                cboCDSOperationalStatus.Text = "C - Under Construction"
                            Case "T"
                                cboCDSOperationalStatus.Text = "T - Temporarily Closed"
                            Case "X"
                                cboCDSOperationalStatus.Text = "X - Permanently Closed"
                            Case "I"
                                cboCDSOperationalStatus.Text = "I - Seasonal Operation"
                            Case Else
                                cboCDSOperationalStatus.Text = " "
                        End Select
                        '  cboCDSOperationalStatus.SelectedText = dr.Item("stroperationalStatus")
                    End If
                    If IsDBNull(dr.Item("strClass")) Then
                        cboCDSClassCode.Text = ""
                    Else
                        cboCDSClassCode.Text = dr.Item("strClass")
                    End If
                    If IsDBNull(dr.Item("strAirProgramCodes")) Then
                        chbCDS_1.Checked = False
                        chbCDS_2.Checked = False
                        chbCDS_3.Checked = False
                        chbCDS_4.Checked = False
                        chbCDS_5.Checked = False
                        chbCDS_6.Checked = False
                        chbCDS_7.Checked = False
                        chbCDS_8.Checked = False
                        chbCDS_9.Checked = False
                        chbCDS_10.Checked = False
                        chbCDS_11.Checked = False
                        chbCDS_12.Checked = False
                        chbCDS_13.Checked = False
                    Else
                        temp = dr.Item("strAirProgramCodes")
                        If Mid(temp, 1, 1) = "0" Then
                            chbCDS_1.Checked = False
                        Else
                            chbCDS_1.Checked = True
                        End If
                        If Mid(temp, 2, 1) = "0" Then
                            chbCDS_2.Checked = False
                        Else
                            chbCDS_2.Checked = True
                        End If
                        If Mid(temp, 3, 1) = "0" Then
                            chbCDS_3.Checked = False
                        Else
                            chbCDS_3.Checked = True
                        End If
                        If Mid(temp, 4, 1) = "0" Then
                            chbCDS_4.Checked = False
                        Else
                            chbCDS_4.Checked = True
                        End If
                        If Mid(temp, 5, 1) = "0" Then
                            chbCDS_5.Checked = False
                        Else
                            chbCDS_5.Checked = True
                        End If
                        If Mid(temp, 6, 1) = "0" Then
                            chbCDS_6.Checked = False
                        Else
                            chbCDS_6.Checked = True
                        End If
                        If Mid(temp, 7, 1) = "0" Then
                            chbCDS_7.Checked = False
                        Else
                            chbCDS_7.Checked = True
                        End If
                        If Mid(temp, 8, 1) = "0" Then
                            chbCDS_8.Checked = False
                        Else
                            chbCDS_8.Checked = True
                        End If
                        If Mid(temp, 9, 1) = "0" Then
                            chbCDS_9.Checked = False
                        Else
                            chbCDS_9.Checked = True
                        End If
                        If Mid(temp, 10, 1) = "0" Then
                            chbCDS_10.Checked = False
                        Else
                            chbCDS_10.Checked = True
                        End If
                        If Mid(temp, 11, 1) = "0" Then
                            chbCDS_11.Checked = False
                        Else
                            chbCDS_11.Checked = True
                        End If
                        If Mid(temp, 12, 1) = "0" Then
                            chbCDS_12.Checked = False
                        Else
                            chbCDS_12.Checked = True
                        End If
                        If Mid(temp, 13, 1) = "0" Then
                            chbCDS_13.Checked = False
                        Else
                            chbCDS_13.Checked = True
                        End If
                    End If
                    If IsDBNull(dr.Item("strSICCode")) Then
                        mtbCDSSICCode.Clear()
                    Else
                        mtbCDSSICCode.Text = dr.Item("strSICCode")
                    End If
                    If IsDBNull(dr.Item("strPlantDescription")) Then
                        txtFacilityDescription.Clear()
                    Else
                        txtFacilityDescription.Text = dr.Item("strPlantDescription")
                    End If
                    If IsDBNull(dr.Item("strContactFirstName")) Then
                        txtContactFirstName.Clear()
                    Else
                        txtContactFirstName.Text = dr.Item("strContactFirstName")
                    End If
                    If IsDBNull(dr.Item("strContactLastName")) Then
                        txtContactLastName.Clear()
                    Else
                        txtContactLastName.Text = dr.Item("strContactLastName")
                    End If
                    If IsDBNull(dr.Item("strContactPreFix")) Then
                        txtContactSocialTitle.Clear()
                    Else
                        txtContactSocialTitle.Text = dr.Item("strContactPrefix")
                    End If
                    If IsDBNull(dr.Item("strContactSuffix")) Then
                        txtContactPedigree.Clear()
                    Else
                        txtContactPedigree.Text = dr.Item("strContactSuffix")
                    End If
                    If IsDBNull(dr.Item("strContactTitle")) Then
                        txtContactTitle.Clear()
                    Else
                        txtContactTitle.Text = dr.Item("strContactTitle")
                    End If
                    If IsDBNull(dr.Item("strCOntactphoneNumber1")) Then
                        mtbContactPhoneNumber.Clear()
                        mtbContactNumberExtension.Clear()
                    Else
                        mtbContactPhoneNumber.Text = Mid(dr.Item("strContactPhoneNumber1"), 1, 10)
                        mtbContactNumberExtension.Text = Mid(dr.Item("strcontactPhoneNumber1"), 11)
                    End If
                End If
                dr.Close()
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub MmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiHelp.Click
        Try
            Help.ShowHelp(Label1, "http://airpermit.dnr.state.ga.us/helpdocs/IAIP_help/")
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnForceBasicRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForceBasicRefresh.Click
        Try
            GenerateRefresh("A")
            GenerateRefresh("C")

            Dim FileName As String = ""
            Dim path As New SaveFileDialog
            Dim DestFilePath As String = "N/A"
            Dim OutPutFile As String = ""
            Dim da As OracleDataAdapter
            Dim cmdCB As OracleCommandBuilder
            Dim ds As DataSet

            If txtAFSBatchFile.Text <> "" Then
                SQL = "select " & connNameSpace & ".afsFileNumber.nextval from Dual"
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    FileName = "GA" & dr.Item(0)
                End While
                dr.Close()

                If FileName <> "" Then
                    path.InitialDirectory = "S:\ISMP\DMU\Production\APB IAIP\Batches"
                    path.FileName = FileName
                    path.Filter = "AFS Data (.txt)|.txt"
                    path.FilterIndex = 1
                    path.DefaultExt = ".txt"

                    If path.ShowDialog = Windows.Forms.DialogResult.OK Then
                        DestFilePath = path.FileName.ToString
                    Else
                        DestFilePath = "N/A"
                    End If

                    If DestFilePath <> "N/A" Then
                        Dim Encoder As New System.Text.ASCIIEncoding
                        Dim bytedata As Byte() = Encoder.GetBytes(txtAFSBatchFile.Text)

                        Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                        fs.Write(bytedata, 0, bytedata.Length)
                        fs.Close()

                        SQL = "Select * " & _
                        "from " & connNameSpace & ".AFSBatchFiles " & _
                        "where AFSFileName = '" & FileName & "' "

                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        da = New OracleDataAdapter(SQL, conn)
                        cmdCB = New OracleCommandBuilder(da)
                        ds = New DataSet("AFSData")
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey

                        da.Fill(ds, "AFSData")
                        Dim row As DataRow = ds.Tables("AFSData").NewRow()
                        row("AFSFileName") = FileName
                        row("AFSBatchFile") = bytedata
                        row("strStaffResponsible") = UserGCode
                        row("DatModifingDate") = OracleDate
                        ds.Tables("AFSData").Rows.Add(row)
                        da.Update(ds, "AFSData")
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub GenerateRefresh(ByVal UpdateCode As String)
        Dim BatchText As String = ""
        Dim len As Integer
        Dim i As Integer

        Dim AIRSNumber As String
        Dim UserAFSCode As String
        Dim FacilityName As String
        Dim FacilityStreet As String
        Dim FacilityCity As String
        Dim FacilityZipCode As String
        Dim SICCode As String
        Dim FacilityContactPerson As String
        Dim ContactPhoneNumber As String
        Dim PlantDesc As String
        Dim AirProgramCode As String
        Dim Pollutant As String
        Dim Classification As String
        Dim ComplianceStatus As String
        Dim AttainmentStatus As String
        Dim AirProgramPollutantLines As String
        Dim CMSMember As String
        Dim Inspector As String
        Dim OperationalStatus As String

        Try
            'G81 was Ender 
            'G62 is for Permitting Manager
            'G36 is for Compliance Manager 
            'GM8 is for Monitoring Manager 

            SQL = "Select " & connNameSpace & ".APBMasterAIRS.strAIRSNumber, " & _
            "strFacilityName, strFacilityStreet1,   " & _
            "strFacilityCity, strFacilityzipCode,   " & _
            "strSICCode, strContactFirstName,   " & _
            "strContactLastName, strContactTitle,   " & _
            "strContactPhoneNumber1, strPlantDescription,   " & _
            "" & connNameSpace & ".AFSFacilityData.strModifingPerson, strUpdateStatus,  " & _
            "strCMSMember  " & _
            "from " & connNameSpace & ".APBMasterAIRS, " & connNameSpace & ".APBFacilityInformation,  " & _
            "" & connNameSpace & ".APBHeaderData, " & connNameSpace & ".APBContactInformation,  " & _
            "" & connNameSpace & ".APBSupplamentalData, " & connNameSpace & ".AFSFacilityData  " & _
            "where " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".AFSFacilityData.strAIRSNumber    " & _
            "and " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".APBFacilityInformation.strAIRSnumber  " & _
            "and " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".APBHeaderData.strAIRSNumber   " & _
            "and " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".APBContactInformation.strAIRSNumber " & _
            "and " & connNameSpace & ".APBMasterAIRS.strAIRSNumber = " & connNameSpace & ".APBSupplamentalData.strAIRSNumber   " & _
            "and " & connNameSpace & ".APBContactInformation.strKEy = '30'  "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = "N/A"
                Else
                    AIRSNumber = Mid(dr.Item("strAIRSNumber"), 3)
                End If
                If AIRSNumber <> "N/A" Then
                    UserAFSCode = "G62"
                    If IsDBNull(dr.Item("StrFacilityname")) Then
                        FacilityName = "N/A"
                    Else
                        FacilityName = dr.Item("strFacilityName")
                    End If
                    If FacilityName.Length > 40 Then
                        FacilityName = Mid(FacilityName, 1, 40)
                    End If
                    len = FacilityName.Length
                    For i = len To 63
                        FacilityName = FacilityName & " "
                    Next
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        FacilityStreet = "N/A"
                    Else
                        FacilityStreet = dr.Item("strFacilityStreet1")
                    End If
                    If FacilityStreet.Length > 30 Then
                        FacilityStreet = Mid(FacilityStreet, 1, 30)
                    End If
                    len = FacilityStreet.Length
                    For i = len To 63
                        FacilityStreet = FacilityStreet & " "
                    Next
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        FacilityCity = "N/A"
                    Else
                        FacilityCity = dr.Item("strFacilityCity")
                    End If
                    If FacilityCity.Length > 30 Then
                        FacilityCity = Mid(FacilityCity, 1, 30)
                    End If
                    len = FacilityCity.Length
                    For i = len To 29
                        FacilityCity = FacilityCity & " "
                    Next
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        FacilityZipCode = "000000000"
                    Else
                        FacilityZipCode = Replace(dr.Item("strFacilityZipCode"), "-", "")
                    End If
                    If FacilityZipCode.Length > 9 Then
                        FacilityZipCode = Mid(FacilityZipCode, 1, 9)
                    End If
                    len = FacilityZipCode.Length
                    If FacilityZipCode.Length <> 9 Then
                        For i = len To 8
                            FacilityZipCode = FacilityZipCode & " "
                        Next
                    End If
                    If IsDBNull(dr.Item("strContactFirstName")) Then
                        FacilityContactPerson = ""
                    Else
                        FacilityContactPerson = dr.Item("strContactFirstName")
                    End If
                    If IsDBNull(dr.Item("strContactLastName")) Then
                        FacilityContactPerson = FacilityContactPerson
                    Else
                        If FacilityContactPerson <> "" Then
                            FacilityContactPerson = FacilityContactPerson & " " & dr.Item("strContactLastName")
                        Else
                            FacilityContactPerson = dr.Item("strContactLastName")
                        End If
                    End If
                    If FacilityContactPerson = "" Then
                        FacilityContactPerson = "N/A"
                    End If
                    If FacilityContactPerson.Length > 20 Then
                        FacilityContactPerson = Mid(FacilityContactPerson, 1, 20)
                    End If
                    len = FacilityContactPerson.Length
                    If FacilityContactPerson.Length <> 20 Then
                        For i = len To 19
                            FacilityContactPerson = FacilityContactPerson & " "
                        Next
                    End If
                    If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                        ContactPhoneNumber = "0000000000"
                    Else
                        ContactPhoneNumber = Replace(Replace(dr.Item("strContactPhoneNumber1"), ")", ""), "(", "")
                    End If
                    len = ContactPhoneNumber.Length
                    If len > 10 Then
                        ContactPhoneNumber = Mid(ContactPhoneNumber, 1, 10)
                    End If
                    If ContactPhoneNumber.Length <> 10 Then
                        For i = len To 9
                            ContactPhoneNumber = ContactPhoneNumber & "0"
                        Next
                    End If
                    If IsDBNull(dr.Item("strPlantDescription")) Then
                        PlantDesc = "N/A"
                    Else
                        PlantDesc = dr.Item("strPlantDescription")
                    End If
                    len = PlantDesc.Length
                    If PlantDesc.Length > 25 Then
                        PlantDesc = Mid(PlantDesc, 1, 25)
                    End If
                    len = PlantDesc.Length
                    If PlantDesc.Length <> 36 Then
                        For i = len To 35
                            PlantDesc = PlantDesc & " "
                        Next
                    End If
                    If IsDBNull(dr.Item("strCMSMember")) Then
                        CMSMember = "**"
                    Else
                        CMSMember = dr.Item("strCMSMember")
                        Select Case CMSMember
                            Case "A"
                                CMSMember = CMSMember & "2"
                            Case "S"
                                CMSMember = CMSMember & "5"
                            Case Else
                                CMSMember = "**"
                        End Select
                    End If
                    len = CMSMember.Length
                    If len <> 66 Then
                        For i = len To 65
                            CMSMember = CMSMember & " "
                        Next
                    End If

                    Inspector = "G36"

                    If IsDBNull(dr.Item("strSICCode")) Then
                        SICCode = "0000"
                    Else
                        SICCode = dr.Item("strSICCode")
                    End If
                    If FacilityZipCode.Length > 4 Then
                        SICCode = Mid(dr.Item("strSICCode"), 1, 4)
                    End If
                    len = FacilityZipCode.Length
                    If SICCode.Length <> 4 Then
                        For i = len To 3
                            SICCode = SICCode & " "
                        Next
                    End If
                    SICCode = SICCode & "            " & Inspector & "      "

                    SQL2 = "Select " & _
                    "" & connNameSpace & ".AFSAirPollutantData.strAIRPollutantKey, " & _
                    "" & connNameSpace & ".AFSAirPollutantData.strPollutantKey, " & _
                    "strComplianceStatus, strClass, " & _
                    "" & connNameSpace & ".APBHeaderData.strAttainmentStatus, " & _
                    "" & connNameSpace & ".APBAirProgramPollutants.strOperationalStatus " & _
                    "from " & connNameSpace & ".APBAirProgramPollutants, " & connNameSpace & ".AFSAirPollutantData, " & _
                    "" & connNameSpace & ".APBHeaderData, " & connNameSpace & ".LookUpCountyInformation " & _
                    "where " & connNameSpace & ".APBAirProgramPollutants.strAIRSNumber = '" & dr.Item("strAIRSNumber") & "' " & _
                    "and " & connNameSpace & ".APBHeaderData.strAIRSNumber = " & connNameSpace & ".APBAirProgramPollutants.strAIRSNumber " & _
                    "and substr(" & connNameSpace & ".APBHeaderData.strAIRSNumber, 5, 3) = " & connNameSpace & ".LookUpCountyInformation.strCountyCode " & _
                    "and " & connNameSpace & ".AFSAirPollutantData.strAirPollutantKey = " & connNameSpace & ".APBAirProgramPollutants.strAirPollutantKey " & _
                    "and " & connNameSpace & ".AFSAirPollutantData.strPollutantKey = " & connNameSpace & ".APBAirProgramPollutants.strPollutantKey " & _
                    "and " & connNameSpace & ".AFSAirPollutantData.strAIRSNumber  = '04" & AIRSNumber & "' "

                    cmd2 = New OracleCommand(SQL2, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader

                    AirProgramPollutantLines = ""

                    While dr2.Read
                        If IsDBNull(dr2.Item("strAIRPollutantKey")) Then
                            AirProgramCode = "0"
                        Else
                            AirProgramCode = Mid(dr2.Item("strAIRPollutantKey"), 13)
                        End If
                        If IsDBNull(dr2.Item("strOperationalStatus")) Then
                            OperationalStatus = "O"
                        Else
                            OperationalStatus = dr2.Item("strOperationalStatus")
                        End If
                        If IsDBNull(dr2.Item("strPollutantKey")) Then
                            Pollutant = "OT"
                        Else
                            Pollutant = dr2.Item("strPollutantKey")
                        End If

                        If Pollutant.Length > 9 Then
                            Pollutant = Mid(Pollutant, 1, 9)
                        End If
                        len = Pollutant.Length
                        If Pollutant.Length <> 9 Then
                            For i = len To 8
                                Pollutant = Pollutant & " "
                            Next
                        End If
                        If IsDBNull(dr2.Item("strClass")) Then
                            Classification = "B "
                        Else
                            Classification = dr2.Item("strClass")
                        End If
                        Select Case Classification
                            Case "A"
                                Classification = "A "
                            Case "SM"
                                Classification = "SM"
                            Case "B"
                                Classification = "B "
                            Case "C"
                                Classification = "B "
                            Case "PR"
                                Classification = "SM"
                            Case "U"
                                Classification = "B "
                            Case Else
                                Classification = "B "
                        End Select
                        If IsDBNull(dr2.Item("strComplianceStatus")) Then
                            ComplianceStatus = "C"
                        Else
                            ComplianceStatus = dr2.Item("strComplianceStatus")
                        End If
                        If IsDBNull(dr2.Item("strAttainmentStatus")) Then
                            AttainmentStatus = "A"
                        Else
                            AttainmentStatus = dr2.Item("strAttainmentStatus")
                            If AttainmentStatus = "True" Then
                                AttainmentStatus = "A"
                            Else
                                AttainmentStatus = "N"
                            End If
                        End If
                        Pollutant = Pollutant & ComplianceStatus & Classification & AttainmentStatus & "                                                    "
                        AirProgramPollutantLines = AirProgramPollutantLines & AIRSNumber & "121" & AirProgramCode & OperationalStatus & "                                                            " & "           " & UpdateCode & vbCrLf & _
                        AIRSNumber & "131" & AirProgramCode & Pollutant & UpdateCode & vbCrLf
                    End While
                    dr2.Close()

                    BatchText = BatchText & _
                    AIRSNumber & "101  " & FacilityName & UpdateCode & vbCrLf & _
                    AIRSNumber & "102  " & FacilityStreet & UpdateCode & vbCrLf & _
                    AIRSNumber & "103  " & FacilityCity & FacilityZipCode & SICCode & UpdateCode & vbCrLf & _
                    AIRSNumber & "1040                                                                 " & UpdateCode & vbCrLf & _
                    AIRSNumber & "105" & FacilityContactPerson & ContactPhoneNumber & PlantDesc & UpdateCode & vbCrLf & _
                    AirProgramPollutantLines & _
                    AIRSNumber & "181" & CMSMember & UpdateCode & vbCrLf & _
                    AIRSNumber & "161900000     00 EPA ACT.>90000       " & Format(Date.Today, "yyMMdd") & "                        NA" & vbCrLf
                End If
            End While
            dr.Close()

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & BatchText
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub

End Class