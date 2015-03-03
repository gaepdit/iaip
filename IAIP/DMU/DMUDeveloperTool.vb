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
        Try

            TCDMUTools.TabPages.Remove(TPAddNewFacility)
            TCDMUTools.TabPages.Remove(TPErrorLog)
            TCDMUTools.TabPages.Remove(TPWebErrorLog)

            LoadOtherComboBoxes()

            If AccountFormAccess(129, 3) = "1" Or AccountFormAccess(129, 4) = "1" Then
                TCDMUTools.TabPages.Add(TPErrorLog)
                rdbViewUnresolvedErrors.Checked = True

                TCDMUTools.TabPages.Add(TPWebErrorLog)
                Me.rdbUnresolvedWebErrors.Checked = True
                FormatWebErrorListGrid()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            If txtAFSBatchFile.Text = "" Then
                txtAFSBatchFile.Text = "NO AFS DATA TO UPDATE."
            Else
                SQL = "select AIRBranch.afsFileNumber.nextval from Dual"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
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
                        "from AIRBranch.AFSBatchFiles " & _
                        "where AFSFileName = '" & FileName & "' "

                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        da = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            SQL = "Select AIRBranch.APBMasterAIRS.strAIRSNumber, " & _
            "strFacilityName, strFacilityStreet1,   " & _
            "strFacilityCity, strFacilityzipCode,   " & _
            "strSICCode, strContactFirstName,   " & _
            "strContactLastName, strContactTitle,   " & _
            "strContactPhoneNumber1, strPlantDescription,   " & _
            "AIRBranch.AFSFacilityData.strModifingPerson, strUpdateStatus,  " & _
            "strCMSMember, strAIrProgramcodes " & _
            "from AIRBranch.APBMasterAIRS, AIRBranch.APBFacilityInformation,  " & _
            "AIRBranch.APBHeaderData, AIRBranch.APBContactInformation,  " & _
            "AIRBranch.APBSupplamentalData, AIRBranch.AFSFacilityData  " & _
            "where AIRBranch.APBMasterAIRS.strAIRSNumber = AIRBranch.AFSFacilityData.strAIRSNumber    " & _
            "and AIRBranch.APBMasterAIRS.strAIRSNumber = AIRBranch.APBFacilityInformation.strAIRSnumber  " & _
            "and AIRBranch.APBMasterAIRS.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber   " & _
            "and AIRBranch.APBMasterAIRS.strAIRSNumber = AIRBranch.APBContactInformation.strAIRSNumber " & _
            "and AIRBranch.APBMasterAIRS.strAIRSNumber = AIRBranch.APBSupplamentalData.strAIRSNumber   " & _
            "and AIRBranch.APBContactInformation.strKEy = '30'  " & _
            "and strAIRProgramCodes not like '0000000000000%' " & _
            "and strUpDateStatus = 'A'  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                        "AIRBranch.AFSAirPollutantData.strAIRPollutantKey, " & _
                        "AIRBranch.AFSAirPollutantData.strPollutantKey, " & _
                        "strComplianceStatus, strClass, " & _
                        "AIRBranch.APBHeaderData.strAttainmentStatus, " & _
                        "AIRBranch.APBAirProgramPollutants.strOperationalStatus " & _
                        "from AIRBranch.APBAirProgramPollutants, AIRBranch.AFSAirPollutantData, " & _
                        "AIRBranch.APBHeaderData, AIRBranch.LookUpCountyInformation " & _
                        "where APBAirProgramPollutants.strAIRSNumber = '" & dr.Item("strAIRSNumber") & "' " & _
                        "and APBHeaderData.strAIRSNumber = APBAirProgramPollutants.strAIRSNumber " & _
                        "and substr(AIRBranch.APBHeaderData.strAIRSNumber, 5, 3) = LookUpCountyInformation.strCountyCode " & _
                        "and AFSAirPollutantData.strAirPollutantKey = APBAirProgramPollutants.strAirPollutantKey " & _
                        "and AFSAirPollutantData.strPollutantKey = APBAirProgramPollutants.strPollutantKey " & _
                        "and AFSAirPollutantData.strAIRSNumber  = '04" & AIRSNumber & "' "

                        cmd2 = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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

                        SQL2 = "Update AIRBranch.AFSAirPollutantData set " & _
                        "strUpDateStatus = 'N' " & _
                        "where strAIRSNumber = '" & dr.Item("strAIRSNumber") & "' "

                        cmd2 = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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

                    SQL = "Update AIRBranch.AFSFacilityData set " & _
                    "strUpDateStatus = 'N' " & _
                    "where strUpDateStatus = 'A' " & _
                    "and strAIRSnumber = '04" & AIRSNumber & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
            End While
            dr.Close()


            txtAFSBatchFile.Text = txtAFSBatchFile.Text & BatchText
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
        "AIRBranch.AFSAirPollutantData.strAIRSNUmber, " & _
        "AIRBranch.AFSAirPollutantData.strAIRPollutantKey, " & _
        "AIRBranch.AFSAirPollutantData.strPollutantKey, " & _
        "AIRBranch.APBAirProgramPollutants.strComplianceStatus, " & _
        "AIRBranch.APBHeaderData.strClass, " & _
        "AIRBranch.APBHeaderData.strAttainmentStatus, " & _
        "AIRBranch.AFSAirPollutantData.strUpdatestatus, " & _
        "AIRBranch.APBAirProgramPollutants.strOperationalStatus " & _
        "from AIRBranch.APBAirProgramPollutants, AIRBranch.AFSAirPollutantData, " & _
        "AIRBranch.APBHeaderData, AIRBranch.LookUpCountyInformation,  " & _
        "AIRBranch.AFSFacilityData " & _
        "where " & _
        "AIRBranch.APBHeaderData.strAIRSNumber = AIRBranch.APBAirProgramPollutants.strAIRSNumber " & _
        "and substr(AIRBranch.APBHeaderData.strAIRSNumber, 5, 3) = AIRBranch.LookUpCountyInformation.strCountyCode " & _
        "and AIRBranch.AFSAirPollutantData.strAirPollutantKey = AIRBranch.APBAirProgramPollutants.strAirPollutantKey " & _
        "and AIRBranch.AFSAirPollutantData.strPollutantKey = AIRBranch.APBAirProgramPollutants.strPollutantKey " & _
        " and AIRBranch.AFSAirPollutantData.strAIRSNUmber = AIRBranch.AFSFacilityData.strAIRSNUmber " & _
        "and AIRBranch.AFSAirPollutantData.strUpdateStatus <> 'N' " & _
        " and (AIRBranch.AFSFacilityData.strUpdateStatus = 'C' or AIRBranch.AFSFacilityData.strUpdateStatus = 'N') "

        Try
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
            "AIRBranch.APBSubpartData.strAIRSnumber, " & _
            "AIRBranch.AFSAirPollutantData.strUpdateStatus    " & _
            "from AIRBranch.APBSubpartData, AIRBranch.AFSAirPollutantData,   " & _
            "AIRBranch.AFSFacilityData " & _
            "where AIRBranch.APBSubpartData.strSubpartKey = " & _
                   "AIRBranch.AFSAirPollutantData.strAIRPollutantKey  " & _
            " and AIRBranch.AFSAirPollutantData.strAIRSNUmber = " & _
                   "AIRBranch.AFSFacilityData.strAIRSNUmber  " & _
            "and AIRBranch.AFSAirPollutantData.strUpdateStatus <> 'N' " & _
            " and  (AIRBranch.AFSFacilityData.strUpdateStatus = 'C' or AIRBranch.AFSFacilityData.strUpdateStatus = 'N') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                        "from AIRBranch.APBSubpartData " & _
                        "where strSubpartKey = '04" & AIRSNumber & "8' "

                        cmd2 = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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
                        "from AIRBranch.APBSubpartData " & _
                        "where strSubpartKey = '04" & AIRSNumber & "9' "

                        cmd2 = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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
                         "from AIRBranch.APBSubpartData " & _
                         "where strSubpartKey = '04" & AIRSNumber & "M' "

                        cmd2 = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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

            'SQL = "Update AIRBranch.AFSAirPollutantData set " & _
            '"strUpDateStatus = 'N' " & _
            '"where strUpDateStatus <> 'N' "

            SQL = "Update AIRBranch.AFSAirPollutantData set " & _
            "AIRBranch.AFSAirPollutantData.strUpDateStatus = 'N' " & _
            "where AIRBranch.AFSAirPollutantData.strUpdateStatus <> 'N' " & _
            "and exists (select * from AIRBranch.AFSFacilityData " & _
            "where AIRBranch.AFSAirPollutantData.STRAIRSNUMBER = AIRBranch.AFSFacilityData.STRAIRSNUMBER " & _
            "and   (AIRBranch.AFSFacilityData.strUpdateStatus = 'C' or AIRBranch.AFSFacilityData.strUpdateStatus = 'N')  ) "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            cmd.ExecuteNonQuery()

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & AirProgramPollutantLines

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

        SQL = "Select AIRBranch.APBMasterAIRS.strAIRSNumber, " & _
        "strFacilityName, strFacilityStreet1,  " & _
        "strFacilityCity, strFacilityzipCode,  " & _
        "strSICCode, strContactFirstName,  " & _
        "strContactLastName, strContactTitle,  " & _
        "strContactPhoneNumber1, strPlantDescription,  " & _
        "AIRBranch.AFSFacilityData.strModifingPerson, strUpdateStatus, " & _
        "strCMSMember " & _
        "from AIRBranch.APBMasterAIRS, AIRBranch.APBFacilityInformation, " & _
        "AIRBranch.APBHeaderData, AIRBranch.APBContactInformation, " & _
        "AIRBranch.APBSupplamentalData, AIRBranch.AFSFacilityData " & _
        "where AIRBranch.APBMasterAIRS.strAIRSNumber = AIRBranch.AFSFacilityData.strAIRSNumber   " & _
        "and AIRBranch.APBMasterAIRS.strAIRSNumber = AIRBranch.APBFacilityInformation.strAIRSnumber " & _
        "and AIRBranch.APBMasterAIRS.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSNumber  " & _
        "and AIRBranch.APBMasterAIRS.strAIRSNumber = AIRBranch.APBContactInformation.strAIRSNumber  " & _
        "and AIRBranch.APBMasterAIRS.strAIRSNumber = AIRBranch.APBSupplamentalData.strAIRSNumber  " & _
        "and AIRBranch.APBContactInformation.strKEy = '30' " & _
        "and strUpDateStatus = 'C' "

        Try
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                        "AIRBranch.AFSAirPollutantData.strAIRPollutantKey, " & _
                        "AIRBranch.AFSAirPollutantData.strPollutantKey, " & _
                        "strComplianceStatus, strUpdateStatus, " & _
                        "strOperationalStatus " & _
                        "from AIRBranch.APBAirProgramPollutants, AIRBranch.AFSAirPollutantData " & _
                        "where AIRBranch.APBAirProgramPollutants.strAIRSNumber = '" & dr.Item("strAIRSNumber") & "' " & _
                        "and AIRBranch.AFSAirPollutantData.strAirPollutantKey = AIRBranch.APBAirProgramPollutants.strAirPollutantKey " & _
                        "and AIRBranch.AFSAirPollutantData.strPollutantKey = AIRBranch.APBAirProgramPollutants.strPollutantKey " & _
                        "and strUpdateStatus <> 'N'  "

                        cmd2 = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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

                        SQL2 = "Update AIRBranch.AFSAirPollutantData set " & _
                        "strUpDateStatus = 'N' " & _
                        "where strAIRSNumber = '" & dr.Item("strAIRSNumber") & "' "

                        cmd2 = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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

            SQL = "Update AIRBranch.AFSFacilityData set " & _
            "strUpDateStatus = 'N' " & _
            "where strUpDateStatus = 'C' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & BatchText

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub PermittingActions()
        Dim BatchText As String = ""
        Dim Comments As String = ""
        Dim PermitFile As String = ""
        Dim len As Integer
        Dim i As Integer

        Dim AppNum As String = ""
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
        "ssppapplicationmaster.strApplicationnumber, " & _
        "AFSSSPPRecords.strAFSActionNumber, AFSSSPPRecords.strUpDateStatus,  " & _
        "SSPPApplicationMaster.strAIRSNumber, " & _
        "SSPPApplicationMaster.strStaffResponsible,  " & _
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
        "AIRBranch.SSPPApplicationData.strComments, " & _
        "to_Char(AIRBranch.SSPPApplicationMaster.DatFinalizedDate, 'YYMMDD') as AchievedDate, " & _
        "AIRBranch.SSPPApplicationData.strAirProgramCodes, " & _
        "AIRBranch.SSPPApplicationData.strPermitNumber " & _
        "from AIRBranch.AFSSSPPRecords, AIRBranch.SSPPApplicationMaster,  " & _
        "AIRBranch.SSPPApplicationData, airbranch.AFSFacilityData " & _
        "where AFSSSPPRecords.strUpDateStatus <> 'N'  " & _
        " and SSPPApplicationMaster.strairsnumber =  AFSFacilityData.strairsnumber " & _
        "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " & _
        "and AFSSSPPRecords.strApplicationNumber = SSPPApplicationMaster.strApplicationNumber " & _
        "and (strPermitType = '1' or strPermitType = '4' or strPermitType = '5' " & _
        "or strPermitType = '7' or strPermitType = '10' or strPermitType = '12' " & _
        "or strPermitType = '13') " & _
        "and SSPPApplicationMaster.strAIRSNumber not like '%APL%' " & _
        " and (AFSFacilityData.strUpdateStatus = 'C' or AFSFacilityData.strUpdateStatus = 'N') "

        Try
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                    If IsDBNull(dr.Item("strApplicationnumber")) Then
                        AppNum = ""
                    Else
                        AppNum = dr.Item("strApplicationnumber")
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

                SQL = "Update AIRBranch.AFSSSPPRecords set " & _
                 "strUpDateStatus = 'N' " & _
                 "where strUPDateStatus <> 'N' " & _
                 "and strApplicationNumber = '" & AppNum & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & BatchText

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub FCEActions()
        Dim BatchText As String = ""
        Dim Comments As String
        Dim len As Integer
        Dim i As Integer

        Dim FCENumber As String = ""
        Dim UpdateCode As String
        Dim AIRSNumber As String
        Dim AIRProgramCodes As String
        Dim UserAFSCode As String
        Dim ActionNumber As String
        Dim ResultsCode As String
        Dim ActionComments As String
        Dim DateAcheived As String
        Dim SiteStatus As String

        SQL = "Select AFSSSCPFCERecords.strUpDateStatus, " & _
        "AFSSSCPFCERecords.strFCENumber, AFSSSCPFCERecords.strAFSActionNumber, " & _
        "SSCPFCEMaster.strAIRSNumber, strAIRProgramCodes, " & _
        "to_char(datFCECompleted, 'YYMMDD') as AchievedDate, strFCEStatus, " & _
        "strFCEComments, strSiteInspection " & _
        "from AIRBranch.AFSSSCPFCERecords, AIRBranch.SSCPFCEMaster, " & _
        "AIRBranch.SSCPFCE, AIRBranch.APBHeaderData, AIRBranch.AFSFacilityData  " & _
        "where AFSSSCPFCERecords.strUpDateStatus <> 'N' " & _
        "and APBHeaderData.strairsnumber = afsfacilitydata.strairsnumber " & _
        "and AFSSSCPFCERecords.strFCENumber = SSCPFCEMaster.strFCENumber " & _
        "and SSCPFCE.strFCENumber = AFSSSCPFCERecords.strFCENumber " & _
        "and APBHeaderData.strAIRSNumber = SSCPFCEMaster.strAIRSNumber " & _
        "and (AFSFacilityData.strUpdateStatus = 'C' or AFSFacilityData.strUpdateStatus = 'N') "

        Try
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                    If IsDBNull(dr.Item("strFCENumber")) Then
                        FCENumber = ""
                    Else
                        FCENumber = dr.Item("strFCENumber")
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

                SQL = "Update AIRBranch.AFSSSCPFCERecords set " & _
                  "strUpDateStatus = 'N' " & _
                  "where strUPDateStatus <> 'N' " & _
                  "and strFCENumber = '" & FCENumber & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            End While

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & BatchText

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub ComplianceActions()
        Dim BatchText As String = ""
        Dim len As Integer
        Dim i As Integer

        Dim TrackingNumber As String = ""
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
        "AFSSSCPRecords.strUpDateStatus, AFSSSCPRecords.strTrackingNumber,  " & _
        "strEventType,  " & _
        "strResponsibleStaff,  " & _
        "case " & _
        "when strEventType = '01' then to_char(datReceivedDate, 'YYMMDD') " & _
        "when strEventType = '02' then to_char(datInspectionDateEnd, 'YYMMDD')   " & _
        "when strEventType = '03' then to_char(datReceivedDate, 'YYMMDD') " & _
        "when strEventType = '05' then to_char(datReceivedDate, 'YYMMDD') " & _
        "else to_char(datCompleteDate, 'YYMMDD') " & _
        "end AchievedDate,   " & _
        "AIRBranch.APBHeaderData.strAIRSNumber,  " & _
        "strAIRProgramCodes, AFSSSCPRecords.strAFSActionNumber,  " & _
        "strEnforcementNumber  " & _
        "from AIRBranch.AFSSSCPRecords, AIRBranch.SSCPItemMaster,  " & _
        "AIRBranch.APBHeaderData, AIRBranch.SSCP_AuditedEnforcement, " & _
        "AIRBranch.SSCPInspections, airbranch.AFSFacilityData " & _
        "where AFSSSCPRecords.strUpdateStatus <> 'N'  " & _
        "and APBHeaderData.strAIRSNumber = AFSFacilityData.strAIRSNumber " & _
        "and AIRBranch.SSCPItemMaster.strTrackingNumber = AIRBranch.AFSSSCPRecords.strTrackingNumber  " & _
        "and AIRBranch.APBHeaderData.strAIRSNumber = AIRBranch.SSCPItemMaster.strAIRSNumber   " & _
        "and AIRBranch.SSCPItemMaster.strTrackingNumber = AIRBranch.SSCP_AuditedEnforcement.strTrackingNumber (+)  " & _
        "and AIRBranch.SSCPItemMaster.strTrackingNumber = AIRBranch.SSCPInspections.strTrackingNumber  (+) " & _
        "and (AFSFacilityData.strUpdateStatus = 'C' or AFSFacilityData.strUpdateStatus = 'N') "

        Try
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                AIRSNumber = Mid(dr.Item("strAIRSNumber"), 3)
                If IsDBNull(dr.Item("strUpDatestatus")) Then
                    UpdateCode = "A"
                Else
                    UpdateCode = dr.Item("strUpDateStatus")
                End If
                If IsDBNull(dr.Item("strTrackingNumber")) Then
                    TrackingNumber = ""
                Else
                    TrackingNumber = dr.Item("strTrackingNumber")
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

                SQL = "Update AIRBranch.AFSSSCPRecords set " & _
                "strUpDateStatus = 'N' " & _
                "where strUPDateStatus <> 'N' " & _
                "and strTrackingNumber = '" & TrackingNumber & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & BatchText

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            'Added Key action number to AFS Table if not already there. 
            SQL = "select " & _
            "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
            "strafskeyactionnumber, strModifingPerson, datModifingDate " & _
            "from AIRBranch.SSCP_AuditedEnforcement " & _
            "where strafskeyactionnumber is not null  " & _
            "and not exists (select * " & _
            "from AIRBranch.afssscpenforcementrecords " & _
            "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
            "AIRBranch.afssscpenforcementrecords.strenforcementnumber )  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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

                SQL = "Insert into AIRBranch.AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            'Updates Key Action Number (Would only happen if the already sent to AFS)  
            SQL = "select " & _
            "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strafskeyactionnumber, strModifingPerson, datModifingDate " & _
            "from AIRBranch.SSCP_AuditedEnforcement " & _
            "where strafskeyactionnumber is not null " & _
            "and exists (select * from AIRBranch.afssscpenforcementrecords " & _
            "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
            "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and to_Date(AIRBranch.sscp_auditedEnforcement.datModifingDate) <> " & _
            "to_Date(AIRBranch.afssscpenforcementrecords.datModifingDate) " & _
            "and strafskeyactionnumber = afssscpenforcementrecords.strAFSActionNumber " & _
            "and strUpdateStatus = 'N' ) "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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

                SQL = "Update AIRBranch.AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            'Adds NOV Sent to AFS 
            SQL = "select " & _
            "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
            "strAFSNOVSentNumber, strModifingPerson, datModifingDate " & _
            "from AIRBranch.SSCP_AuditedEnforcement " & _
            "where strAFSNOVSentNumber is not null  " & _
            "and not exists (select * " & _
            "from AIRBranch.afssscpenforcementrecords " & _
            "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
            "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSNOVSentNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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

                SQL = "Insert into AIRBranch.AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            'Updates NOV Sent to AFS 
            SQL = "select " & _
            "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSNOVSentNumber, strModifingPerson, datModifingDate " & _
            "from AIRBranch.SSCP_AuditedEnforcement " & _
            "where strAFSNOVSentNumber is not null " & _
            "and exists (select * from AIRBranch.afssscpenforcementrecords " & _
            "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
            "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and to_Date(AIRBranch.sscp_auditedEnforcement.datModifingDate) <> " & _
            "to_Date(AIRBranch.afssscpenforcementrecords.datModifingDate) " & _
            "and strUpdateStatus = 'N' " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSNOVSentNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                SQL = "Update AIRBranch.AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            'Adds NOV Resolved 
            SQL = "select " & _
          "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
          "strAFSNOVResolvedNumber, strModifingPerson, datModifingDate " & _
          "from AIRBranch.SSCP_AuditedEnforcement " & _
          "where strAFSNOVResolvedNumber is not null  " & _
          "and not exists (select * " & _
          "from AIRBranch.afssscpenforcementrecords " & _
          "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
          "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSNOVResolvedNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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

                SQL = "Insert into AIRBranch.AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            'Updates NOV Resolved 
            SQL = "select " & _
            "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSNOVResolvedNumber, strModifingPerson, datModifingDate " & _
            "from AIRBranch.SSCP_AuditedEnforcement " & _
            "where strAFSNOVResolvedNumber is not null " & _
            "and exists (select * from AIRBranch.afssscpenforcementrecords " & _
            "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
            "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and to_Date(AIRBranch.sscp_auditedEnforcement.datModifingDate) <> " & _
            "to_Date(AIRBranch.afssscpenforcementrecords.datModifingDate) " & _
            "and strUpdateStatus = 'N' " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSNOVResolvedNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                SQL = "Update AIRBranch.AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            'Adds CO Proposed 
            SQL = "select " & _
          "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
          "strAFSCOProposedNumber, strModifingPerson, datModifingDate " & _
          "from AIRBranch.SSCP_AuditedEnforcement " & _
          "where strAFSCOProposedNumber is not null  " & _
          "and not exists (select * " & _
          "from AIRBranch.afssscpenforcementrecords " & _
          "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
          "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSCOProposedNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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

                SQL = "Insert into AIRBranch.AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            'Updates CO Proposed 
            SQL = "select " & _
            "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSCOProposedNumber, strModifingPerson, datModifingDate " & _
            "from AIRBranch.SSCP_AuditedEnforcement " & _
            "where strAFSCOProposedNumber is not null " & _
            "and exists (select * from AIRBranch.afssscpenforcementrecords " & _
            "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
            "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and to_Date(AIRBranch.sscp_auditedEnforcement.datModifingDate) <> " & _
            "to_Date(AIRBranch.afssscpenforcementrecords.datModifingDate) " & _
            "and strUpdateStatus = 'N' " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSCOProposedNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                SQL = "Update AIRBranch.AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            'Addes CO Executed 
            SQL = "select " & _
          "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
          "strAFSCOExecutedNumber, strModifingPerson, datModifingDate " & _
          "from AIRBranch.SSCP_AuditedEnforcement " & _
          "where strAFSCOExecutedNumber is not null  " & _
          "and not exists (select * " & _
          "from AIRBranch.afssscpenforcementrecords " & _
          "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
          "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSCOExecutedNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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

                SQL = "Insert into AIRBranch.AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            'Updates Co Executed 
            SQL = "select " & _
            "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSCOExecutedNumber, strModifingPerson, datModifingDate " & _
            "from AIRBranch.SSCP_AuditedEnforcement " & _
            "where strAFSCOExecutedNumber is not null " & _
            "and exists (select * from AIRBranch.afssscpenforcementrecords " & _
            "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
            "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and to_Date(AIRBranch.sscp_auditedEnforcement.datModifingDate) <> " & _
            "to_Date(AIRBranch.afssscpenforcementrecords.datModifingDate) " & _
            "and strUpdateStatus = 'N' " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSCOExecutedNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                SQL = "Update AIRBranch.AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            'Adds CO Resolved 
            SQL = "select " & _
          "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
          "strAFSCOResolvedNumber, strModifingPerson, datModifingDate " & _
          "from AIRBranch.SSCP_AuditedEnforcement " & _
          "where strAFSCOResolvedNumber is not null  " & _
          "and not exists (select * " & _
          "from AIRBranch.afssscpenforcementrecords " & _
          "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
          "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSCOResolvedNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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

                SQL = "Insert into AIRBranch.AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            'Updates CO Resolved 
            SQL = "select " & _
            "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSCOResolvedNumber, strModifingPerson, datModifingDate " & _
            "from AIRBranch.SSCP_AuditedEnforcement " & _
            "where strAFSCOResolvedNumber is not null " & _
            "and exists (select * from AIRBranch.afssscpenforcementrecords " & _
            "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
            "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and to_Date(AIRBranch.sscp_auditedEnforcement.datModifingDate) <> " & _
            "to_Date(AIRBranch.afssscpenforcementrecords.datModifingDate) " & _
            "and strUpdateStatus = 'N' " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSCOResolvedNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                SQL = "Update AIRBranch.AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            'Adds AO to AG 
            SQL = "select " & _
          "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
          "strAFSAOtoAGNumber, strModifingPerson, datModifingDate " & _
          "from AIRBranch.SSCP_AuditedEnforcement " & _
          "where strAFSAOtoAGNumber is not null  " & _
          "and not exists (select * " & _
          "from AIRBranch.afssscpenforcementrecords " & _
          "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
          "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSAOtoAGNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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

                SQL = "Insert into AIRBranch.AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            'Updates AO to AG 
            SQL = "select " & _
            "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSAOtoAGNumber, strModifingPerson, datModifingDate " & _
            "from AIRBranch.SSCP_AuditedEnforcement " & _
            "where strAFSAOtoAGNumber is not null " & _
            "and exists (select * from AIRBranch.afssscpenforcementrecords " & _
            "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
            "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and to_Date(AIRBranch.sscp_auditedEnforcement.datModifingDate) <> " & _
            "to_Date(AIRBranch.afssscpenforcementrecords.datModifingDate) " & _
            "and strUpdateStatus = 'N' " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSAOtoAGNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                SQL = "Update AIRBranch.AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            'Adds Civil Order (AO) 
            SQL = "select " & _
          "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
          "strAFSCivilCOurtNumber, strModifingPerson, datModifingDate " & _
          "from AIRBranch.SSCP_AuditedEnforcement " & _
          "where strAFSCivilCOurtNumber is not null  " & _
          "and not exists (select * " & _
          "from AIRBranch.afssscpenforcementrecords " & _
          "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
          "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSCivilCOurtNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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

                SQL = "Insert into AIRBranch.AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            'Updates Civil Order (AO)  
            SQL = "select " & _
            "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSCivilCOurtNumber, strModifingPerson, datModifingDate " & _
            "from AIRBranch.SSCP_AuditedEnforcement " & _
            "where strAFSCivilCOurtNumber is not null " & _
            "and exists (select * from AIRBranch.afssscpenforcementrecords " & _
            "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
            "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and to_Date(AIRBranch.sscp_auditedEnforcement.datModifingDate) <> " & _
            "to_Date(AIRBranch.afssscpenforcementrecords.datModifingDate) " & _
            "and strUpdateStatus = 'N' " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSCivilCOurtNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                SQL = "Update AIRBranch.AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

            'Adds AO Resolved 
            SQL = "select " & _
            "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber, " & _
            "strAFSAOResolvedNumber, strModifingPerson, datModifingDate " & _
            "from AIRBranch.SSCP_AuditedEnforcement " & _
            "where strAFSAOResolvedNumber is not null  " & _
            "and not exists (select * " & _
            "from AIRBranch.afssscpenforcementrecords " & _
            "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
            "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSAOResolvedNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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

                SQL = "Insert into AIRBranch.AFSSSCPEnforcementRecords " & _
                "values " & _
                "('" & EnforcementNumber & "', '" & AFSNumber & "', " & _
                "'A', '" & ModifingPerson & "', " & _
                "'" & ModifingDate & "') "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            'Updates AO Resolved 
            SQL = "select " & _
            "AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber, strAIRSNumber,  " & _
            "strAFSAOResolvedNumber, strModifingPerson, datModifingDate " & _
            "from AIRBranch.SSCP_AuditedEnforcement " & _
            "where strAFSAOResolvedNumber is not null " & _
            "and exists (select * from AIRBranch.afssscpenforcementrecords " & _
            "where AIRBranch.sscp_auditedEnforcement.strenforcementnumber = " & _
            "AIRBranch.afssscpenforcementrecords.strenforcementnumber " & _
            "and to_Date(AIRBranch.sscp_auditedEnforcement.datModifingDate) <> " & _
            "to_Date(AIRBranch.afssscpenforcementrecords.datModifingDate) " & _
            "and strUpdateStatus = 'N' " & _
            "and AIRBranch.sscp_auditedEnforcement.strAFSAOResolvedNumber = " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber)  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                SQL = "Update AIRBranch.AFSSSCPEnforcementRecords set " & _
                "strUpdateStatus = 'C', " & _
                "strModifingPerson = '" & ModifingPerson & "', " & _
                "datModifingDate = '" & ModifingDate & "' " & _
                "where strEnforcementNumber = '" & EnforcementNumber & "' " & _
                "and strAfsActionNumber = '" & AFSNumber & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()

            End While
            dr.Close()

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
            "AIRBranch.AFSSSCPRecords, AIRBranch.AFSFacilityData " & _
            "where AFSSSCPEnforcementRecords.strUpdateStatus <> 'N'     " & _
            "and APBHeaderData.strAIRSNumber = AFSFacilityData.strAIRSNumber   " & _
            "and  SSCP_AuditedEnforcement.strEnforcementNumber = AFSSSCPEnforcementRecords.strEnforcementNumber  " & _
            "and  APBHeaderData.strAIRSNumber = SSCP_AuditedEnforcement.strAIRSNumber         " & _
            "and  SSCP_AuditedEnforcement.strEnforcementNumber = SSCPENforcementStipulated.strEnforcementNumber (+)     " & _
            "and  SSCP_AuditedEnforcement.strTrackingNumber = SSCPItemMaster.strTrackingNumber (+)    " & _
            "and strEventType <> '03'  " & _
            "and SSCP_AuditedEnforcement.strTrackingNumber = AFSSSCPRecords.strTrackingNumber (+)  " & _
            "and  (AFSFacilityData.strUpdateStatus = 'C' or AFSFacilityData.strUpdateStatus = 'N') " & _
            "Order by strAIRSNumber, strAFSActionNumber ASC  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                AIRSNumber = Mid(dr.Item("strAIRSNumber"), 3)
                If IsDBNull(dr.Item("strUpDatestatus")) Then
                    UpdateCode = "A"
                Else
                    UpdateCode = dr.Item("strUpDateStatus")
                End If
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
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
                        Case "60"   'AO to AG  3/4/2013 changed 60 to NO to both address and resolve action
                            BatchText = BatchText & _
                            AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "NO                      " & AOExecuted & "  0000000" & UserAFSCode & "            N" & UpdateCode & vbCrLf & _
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

                SQL = "Update AIRBranch.AFSSSCPEnforcementRecords set " & _
                "strUpDateStatus = 'N' " & _
                "where strUPDateStatus <> 'N' " & _
                "and strEnforcementNumber = '" & EnforcementNumber & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            SQL = "Select   distinct " & _
            "AIRBranch.afssscpenforcementrecords.strUpDateStatus,   " & _
            "AIRBranch.AFSSSCPEnforcementRecords.strEnforcementNumber,   " & _
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
            "AIRBranch.SSCPENforcementStipulated.strStipulatedPenalty,    " & _
            "AIRBranch.APBHeaderData.strAIRSNumber, strAIRProgramCodes,    " & _
            "AIRBranch.afssscpenforcementrecords.strAFSActionNumber,    " & _
            "strAFSKeyActionNumber, AIRBranch.SSCP_AuditedEnforcement.strTrackingNumber,    " & _
            "case    " & _
            "when AIRBranch.afssscpenforcementrecords.strAFSActionNumber = strAFSKeyActionNumber then '04'    " & _
            "when AIRBranch.afssscpenforcementrecords.strAFSActionNumber = strAFSNOVSentNumber then '56'    " & _
            "when AIRBranch.afssscpenforcementrecords.strAFSActionNumber = strAFSNOVResolvedNumber then 'AW'    " & _
            "when AIRBranch.afssscpenforcementrecords.strAFSActionNumber = strAFSCOProposedNumber then '57'    " & _
            "when AIRBranch.afssscpenforcementrecords.strAFSActionNumber = strAFSCOExecutedNumber then 'X1'    " & _
            "when AIRBranch.afssscpenforcementrecords.strAFSActionNumber = strAFSCOResolvedNumber then 'AS'    " & _
            "when AIRBranch.afssscpenforcementrecords.strAFSActionNumber = strAFSStipulatedPenaltyNumber then 'Z4'  " & _
            "when AIRBranch.afssscpenforcementrecords.strAFSActionNumber = strAFSAOtoAGNumber then '60'    " & _
            "when AIRBranch.afssscpenforcementrecords.strAFSActionNumber = strAFSCivilCourtNumber then '64'    " & _
            "when AIRBranch.afssscpenforcementrecords.strAFSActionNumber = strAFSAOResolvedNumber then 'AS'    " & _
            "Else 'ERROR'     " & _
            "End as ActionType,    " & _
            "strPollutants, strHPV,   " & _
            "AIRBranch.afsismprecords.strafsactionnumber as LinkingEvent   " & _
            "from AIRBranch.AFSSSCPEnforcementRecords,    " & _
            "AIRBranch.SSCP_AuditedEnforcement,     " & _
            "AIRBranch.APBHeaderData, AIRBranch.SSCPENforcementStipulated,    " & _
            "AIRBranch.SSCPItemMaster,   " & _
            "AIRBranch.sscptestreports,   " & _
            "AIRBranch.AFSISMPRecords, AIRBranch.AFSFacilityData   " & _
            "where AIRBranch.afssscpenforcementrecords.strUpdateStatus <> 'N'    " & _
            "and apbheaderdata.strAIRSnumber = AFSFacilityData.strAIRSNumber  " & _
            "and AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber = " & _
            "AIRBranch.AFSSSCPEnforcementRecords.strEnforcementNumber    " & _
            "and AIRBranch.APBHeaderData.strAIRSNumber = AIRBranch.SSCP_AuditedEnforcement.strAIRSNumber    " & _
            "and AIRBranch.SSCP_AuditedEnforcement.strEnforcementNumber = " & _
            "AIRBranch.SSCPENforcementStipulated.strEnforcementNumber (+)    " & _
            "and AIRBranch.SSCP_AuditedEnforcement.strTrackingNumber = " & _
            "AIRBranch.SSCPItemMaster.strTrackingNumber (+)   " & _
            "and AIRBranch.SSCP_AuditedEnforcement.strTrackingNumber = " & _
            "AIRBranch.SSCPTestReports.strTrackingNumber (+)  " & _
            "and AIRBranch.sscptestReports.strReferenceNumber  = " & _
            "AIRBranch.AFSISMPRecords.strReferenceNumber  (+)  " & _
            "and (strEventType = '03' or strEventType is null) " & _
            "and  (AFSFacilityData.strUpdateStatus = 'C' or AFSFacilityData.strUpdateStatus = 'N') " & _
            "order by strairsnumber, discoverydate "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                AIRSNumber = Mid(dr.Item("strAIRSNumber"), 3)
                If IsDBNull(dr.Item("strEnforcementNumber")) Then
                    EnforcementNumber = ""
                Else
                    EnforcementNumber = dr.Item("strEnforcementNumber")
                End If

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
                    Case "60"   'AO to AG 3/4/2013 changed 60 to NO to both address and resolve action
                        BatchText = BatchText & _
                        AIRSNumber & "161" & ActionNumber & AIRProgramCodes & "NO                      " & AOExecuted & "  0000000" & UserAFSCode & "            N" & UpdateCode & _
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
                SQL = "Update AIRBranch.AFSSSCPEnforcementRecords set " & _
               "strUpDateStatus = 'N' " & _
               "where strUPDateStatus <> 'N' " & _
               "and strEnforcementNumber = '" & EnforcementNumber & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            End While
            dr.Close()

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & BatchText

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

        SQL = "Select " & _
        "AFSISMPRecords.strReferenceNumber, " & _
        "AFSISMPRecords.strAfsActionNumber, " & _
        "substr(AIRBranch.ISMPMaster.strAIRSNumber, 3) as AIRSNumber,  " & _
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
        "strReviewingEngineer, AFSISMPRecords.strUpdateStatus, strPollutant, " & _
        "mmoCommentArea, strafscode  " & _
        "from AIRBranch.AFSISMPRecords, AIRBranch.ISMPMaster, " & _
        "AIRBranch.ISMPReportInformation, AIRBranch.APBHeaderData, " & _
        "AIRBranch.LookUPPollutants, AIRBranch.AFSFacilityData  " & _
        "where AIRBranch.ISMPMaster.strReferenceNumber = AIRBranch.AFSISMPRecords.strReferenceNumber  " & _
        "and ISMPMaster.strAIRSnumber = AFSFacilityData.strAIRSnumber  " & _
        "and AIRBranch.ISMPMaster.strReferenceNumber = AIRBranch.ISMPReportInformation.strReferenceNumber  " & _
        "and AIRBranch.ISMPMaster.strAIRSNumber = AIRBranch.APBHeaderData.strAIRSnumber " & _
        "and AIRBranch.LookUPPollutants.strPollutantcode = AIRBranch.ISMPReportInformation.strPollutant " & _
        "and strComplianceStatus <> '02' " & _
        "and AFSISMPRecords.strUpdateStatus <> 'N' " & _
        "and (AFSFacilityData.strUpdateStatus = 'C' or AFSFacilityData.strUpdateStatus = 'N')  "

        Try
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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

                SQL = "Update AIRBranch.AFSISMPRecords set " & _
                "strUpDateStatus = 'N' " & _
                "where strUPDateStatus <> 'N' " & _
                "and strReferenceNumber = '" & referenceNumber & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                cmd.ExecuteNonQuery()
            End While

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & BatchText

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub FindRegion(ByVal Region As String, ByVal AIRSNumber As String)
        Try

            If Len(AIRSNumber) = 12 And IsNumeric(AIRSNumber) Then
                SQL = "Select (AIRBranch.LookUPDistricts.strDistrictcode|| '-'||strDistrictName) as District " & _
                "from AIRBranch.LookUPDistricts, AIRBranch.LookUPDistrictInformation " & _
                "where AIRBranch.LookUPDistricts.strDistrictCode = AIRBranch.LookUPDistrictInformation.strDistrictCode " & _
                "and strDistrictCounty = '" & Mid(AIRSNumber, 5, 3) & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                "from AIRBranch.APBMasterAIRS " & _
                "where strAIRSNumber = '" & AIRSNumber & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
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

                        SQL = "Insert into AIRBranch.APBMasterAIRS " & _
                        "(strAIRSNumber, strModifingPerson, " & _
                        "datModifingDate) " & _
                        "values " & _
                        "('" & AIRSNumber & "', '" & UserGCode & "', " & _
                        "'" & OracleDate & "') "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Insert into AIRBranch.APBFacilityInformation " & _
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

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        Dim AttainmentStatus As String = ""

                        SQL = "select " & _
                        "strNonAttainment " & _
                        "from AIRBranch.LookUpCountyInformation " & _
                        "where strCountyCode = '" & Mid(AIRSNumber, 5, 3) & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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

                        SQL = "Insert into AIRBranch.APBHeaderData " & _
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

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If

                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Insert into AIRBranch.APBSupplamentalData " & _
                        "(strAIRSNumber, datSSCPTestReportDue, " & _
                        "strModifingPerson, DatModifingDate, " & _
                        "strDistrictOffice, strCMSMember, " & _
                        "strAFSActionNumber) " & _
                        "values " & _
                         "('" & AIRSNumber & "', '', " & _
                         "'" & UserGCode & "', '" & OracleDate & "', " & _
                         "'" & DistrictOffice & "', '', " & _
                         "'00001' ) "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If

                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "insert into AIRBranch.APBContactInformation " & _
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

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If

                        dr = cmd.ExecuteReader
                        dr.Close()

                        If chbCDS_1.Checked = True Then
                            SQL = "Insert into AIRBranch.APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "0', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_2.Checked = True Then
                            SQL = "Insert into AIRBranch.APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "1', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_3.Checked = True Then
                            SQL = "Insert into AIRBranch.APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "3', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_4.Checked = True Then
                            SQL = "Insert into AIRBranch.APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "4', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_5.Checked = True Then
                            SQL = "Insert into AIRBranch.APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "6', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_6.Checked = True Then
                            SQL = "Insert into AIRBranch.APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "7', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_7.Checked = True Then
                            SQL = "Insert into AIRBranch.APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "8', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_8.Checked = True Then
                            SQL = "Insert into AIRBranch.APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "9', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_9.Checked = True Then
                            SQL = "Insert into AIRBranch.APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "F', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_10.Checked = True Then
                            SQL = "Insert into AIRBranch.APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "A', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_11.Checked = True Then
                            SQL = "Insert into AIRBranch.APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "I', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_12.Checked = True Then
                            SQL = "Insert into AIRBranch.APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "M', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                        If chbCDS_13.Checked = True Then
                            SQL = "Insert into AIRBranch.APBAirProgramPollutants " & _
                            "(strAIRSNumber, strAIRPollutantKey, " & _
                            "strPollutantKey, strComplianceStatus, " & _
                            "strModifingPerson, datModifingDate) " & _
                            "values " & _
                            "('" & AIRSNumber & "', '" & AIRSNumber & "V', " & _
                            "'OT', 'C', " & _
                            "'" & UserGCode & "', '" & OracleDate & "') "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If

                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If

                        SQL = "Insert into AIRBranch.SSCPDistrictResponsible " & _
                        "values " & _
                        "('" & AIRSNumber & "', 'False', " & _
                        "'1', sysdate) "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Insert into AIRBranch.SSCPInspectionsRequired " & _
                        "(numKey, strAIRSnumber, intyear) " & _
                        "values " & _
                        "((Select max(numkey) + 1 from AIRBranch.SSCPInspectionsRequired), " & _
                        "'" & AIRSNumber & "', '" & Now.Year.ToString & "') "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
    Private Sub txtCDSAIRSNumber_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCDSAIRSNumber.Leave
        Try

            FindRegion(txtCDSRegionCode.Text, txtCDSAIRSNumber.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnNewFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewFacility.Click
        Try
            CreateNewFacility()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtCDSStreetAddress_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCDSStreetAddress.TextChanged
        Try

            txtMailingAddress.Text = txtCDSStreetAddress.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtCDSCity_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCDSCity.TextChanged
        Try

            txtMailingCity.Text = txtCDSCity.Text
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnGenerateBatchFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateBatchFile.Click
        Try
            txtAFSBatchFile.Clear()
            GenerateBatchFile()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnClearAFSFileGenerator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAFSFileGenerator.Click
        Try
            txtAFSBatchFile.Clear()

            rdbGenerateStandardFile.Checked = True
            rdbGenerateStandardFile.Checked = False

            pnlStandardFile.Enabled = False
            pnlAIRSSpecific.Enabled = False
            pnlSubParts.Enabled = False
            pnlBasicRefresh.Enabled = False


            mtbAFSAirsNumber.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnClearAddNewFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAddNewFacility.Click
        Try

            ClearCreateFacility()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

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
                SQL = "Update " & DBNameSpace & ".IAIPErrorLog set " & _
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
                    "from " & DBNameSpace & ".LogErrors " & _
                    "where NumError = " & txtWebErrorNumber.Text & " "

                    SQL = "select numError, " & _
                    "strIPAddress, strUserEmail, " & _
                    "strErrorPage, dateTimeStamp, " & _
                    "strErrorMsg, strSolution " & _
                    "From " & DBNameSpace & ".OLAPERRORLog " & _
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
                SQL = "Update " & DBNameSpace & ".OLAPErrorLog set " & _
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
    Private Sub mtbCDSZipCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mtbCDSZipCode.TextChanged
        Try

            mtbMailingZipCode.Text = mtbCDSZipCode.Text

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteAIRSNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAIRSNumber.Click
        Try

            If Me.txtDeleteAIRSNumber.Text <> "" And txtDeleteAIRSNumber.Text.Length = 8 Then
                SQL = "delete " & DBNameSpace & ".afsairpollutantdata where strAIRSNumber = '0413" & txtDeleteAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                "values " & _
                "(" & _
                "(select " & _
                "case when max(numCounter) is null then 1 " & _
                "else max(numCounter) + 1 " & _
                "end numCounter " & _
                "from " & DBNameSpace & ".AFSDeletions), " & _
                "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                "'" & OracleDate & "', '', " & _
                "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & DBNameSpace & ".afsfacilitydata where strAirsnumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & DBNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & DBNameSpace & ".apbairprogrampollutants where strAIRSNumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & DBNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & DBNameSpace & ".hb_apbairprogrampollutants where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & DBNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & DBNameSpace & ".apbcontactinformation where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & DBNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & DBNameSpace & ".apbheaderdata where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & DBNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & DBNameSpace & ".HB_APBHeaderData where strAIRSNumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & DBNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & DBNameSpace & ".HB_APBFacilityInformation where strAIRSNumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & DBNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & DBNameSpace & ".APBFacilityInformation where strAIRSNumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & DBNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & DBNameSpace & ".apbsupplamentaldata where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & DBNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".SSCPInspectionsRequired where strAIRSnumber = '0413" & txtDeleteAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & DBNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Delete " & DBNameSpace & ".SSCPDistrictResponsible where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & DBNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "delete " & DBNameSpace & ".sscpfacilityassignment where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & DBNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()


                SQL = "delete " & DBNameSpace & ".sscpInspectionsRequired where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & DBNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()


                SQL = "delete " & DBNameSpace & ".apbmasterairs where strairsnumber = '0413" & txtDeleteAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL2 = "Insert into " & DBNameSpace & ".AFSDeletions " & _
                         "values " & _
                         "(" & _
                         "(select " & _
                         "case when max(numCounter) is null then 1 " & _
                         "else max(numCounter) + 1 " & _
                         "end numCounter " & _
                         "from " & DBNameSpace & ".AFSDeletions), " & _
                         "'0413" & txtDeleteAIRSNumber.Text & "', " & _
                         "'" & Replace(SQL, "'", "''") & "', 'True', " & _
                         "'" & OracleDate & "', '', " & _
                         "'') "

                cmd = New OracleCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
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
                "from " & DBNameSpace & ".SSPPApplicationdata, " & _
                "" & DBNameSpace & ".SSPPApplicationContact " & _
                "where " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationContact.strApplicationNumber " & _
                "and " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber = '" & txtApplicationNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnForceBasicRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForceBasicRefresh.Click
        Try

            txtAFSBatchFile.Clear()
            GenerateRefresh("A")
            GenerateRefresh("C")

            Dim FileName As String = ""
            Dim path As New SaveFileDialog
            Dim DestFilePath As String = "N/A"
            Dim OutPutFile As String = ""
            Dim da As OracleDataAdapter
            Dim cmdCB As OracleCommandBuilder
            Dim ds As DataSet

            If txtAFSBatchFile.Text = "" Then
                txtAFSBatchFile.Text = "NO AFS DATA TO UPDATE."
            Else
                SQL = "select " & DBNameSpace & ".afsFileNumber.nextval from Dual"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
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
                        "from " & DBNameSpace & ".AFSBatchFiles " & _
                        "where AFSFileName = '" & FileName & "' "

                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        da = New OracleDataAdapter(SQL, CurrentConnection)
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

            SQL = "Select " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber, " & _
            "strFacilityName, strFacilityStreet1,   " & _
            "strFacilityCity, strFacilityzipCode,   " & _
            "strSICCode, strContactFirstName,   " & _
            "strContactLastName, strContactTitle,   " & _
            "strContactPhoneNumber1, strPlantDescription,   " & _
            "" & DBNameSpace & ".AFSFacilityData.strModifingPerson, strUpdateStatus,  " & _
            "strCMSMember  " & _
            "from " & DBNameSpace & ".APBMasterAIRS, " & DBNameSpace & ".APBFacilityInformation,  " & _
            "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBContactInformation,  " & _
            "" & DBNameSpace & ".APBSupplamentalData, " & DBNameSpace & ".AFSFacilityData  " & _
            "where " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".AFSFacilityData.strAIRSNumber    " & _
            "and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber  " & _
            "and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber   " & _
            "and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBContactInformation.strAIRSNumber " & _
            "and " & DBNameSpace & ".APBMasterAIRS.strAIRSNumber = " & DBNameSpace & ".APBSupplamentalData.strAIRSNumber   " & _
            "and " & DBNameSpace & ".APBContactInformation.strKEy = '30'  "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
                    "" & DBNameSpace & ".AFSAirPollutantData.strAIRPollutantKey, " & _
                    "" & DBNameSpace & ".AFSAirPollutantData.strPollutantKey, " & _
                    "strComplianceStatus, strClass, " & _
                    "" & DBNameSpace & ".APBHeaderData.strAttainmentStatus, " & _
                    "" & DBNameSpace & ".APBAirProgramPollutants.strOperationalStatus " & _
                    "from " & DBNameSpace & ".APBAirProgramPollutants, " & DBNameSpace & ".AFSAirPollutantData, " & _
                    "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".LookUpCountyInformation " & _
                    "where " & DBNameSpace & ".APBAirProgramPollutants.strAIRSNumber = '" & dr.Item("strAIRSNumber") & "' " & _
                    "and " & DBNameSpace & ".APBHeaderData.strAIRSNumber = " & DBNameSpace & ".APBAirProgramPollutants.strAIRSNumber " & _
                    "and substr(" & DBNameSpace & ".APBHeaderData.strAIRSNumber, 5, 3) = " & DBNameSpace & ".LookUpCountyInformation.strCountyCode " & _
                    "and " & DBNameSpace & ".AFSAirPollutantData.strAirPollutantKey = " & DBNameSpace & ".APBAirProgramPollutants.strAirPollutantKey " & _
                    "and " & DBNameSpace & ".AFSAirPollutantData.strPollutantKey = " & DBNameSpace & ".APBAirProgramPollutants.strPollutantKey " & _
                    "and " & DBNameSpace & ".AFSAirPollutantData.strAIRSNumber  = '04" & AIRSNumber & "' "

                    cmd2 = New OracleCommand(SQL2, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
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
                    "from " & DBNameSpace & ".IAIPErrorLog, " & DBNameSpace & ".EPDUserProfiles  " & _
                    "where " & DBNameSpace & ".IAIPErrorLog.strUser = " & DBNameSpace & ".EPDUserProfiles.numUserID " & _
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

    Private Sub btnUpdateAllSubParts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateAllSubParts.Click
        Try
            Dim AIRSNumber As String = ""
            Dim AIRProgramCode As String = ""
            Dim AirProgramPollutantLines As String = ""
            Dim Subpart As String = ""
            Dim SubpartData As String = ""
            Dim Len As Integer
            Dim UpdateCode As String = ""
            txtAFSBatchFile.Clear()
            txtAFSBatchFile.Text = ""
            Subpart = ""
            SubpartData = ""

            SQL = "Select " & _
            "distinct(substr(strSubPartkey, 13, 1)) as subpart,  " & _
            "" & DBNameSpace & ".APBSubpartData.strAIRSnumber, " & _
            "" & DBNameSpace & ".AFSAirPollutantData.strUpdateStatus    " & _
            "from " & DBNameSpace & ".APBSubpartData, " & DBNameSpace & ".AFSAirPollutantData,   " & _
            "" & DBNameSpace & ".AFSFacilityData, airbranch.apbHeaderdata " & _
            "where " & DBNameSpace & ".APBSubpartData.strSubpartKey = " & _
            "" & DBNameSpace & ".AFSAirPollutantData.strAIRPollutantKey  " & _
            " and airbranch.afsfacilitydata.strairsnumber = airbranch.apbHeaderdata.strairsnumber " & _
            "and stroperationalstatus  = 'O' " & _
            " and " & DBNameSpace & ".AFSAirPollutantData.strAIRSNUmber = " & _
            "" & DBNameSpace & ".AFSFacilityData.strAIRSNUmber  " & _
            " and " & DBNameSpace & ".AFSFacilityData.strUpdateStatus <>  'A' " & _
            "   and AIRBranch.AFSFacilityData.strUpdateStatus <>  'H' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                AirProgramCode = ""
                AIRSNumber = ""
                UpdateCode = "A"

                If IsDBNull(dr.Item("SubPart")) Then
                    AirProgramCode = ""
                Else
                    AirProgramCode = dr.Item("SubPart")
                    If IsDBNull(dr.Item("strAIRSnumber")) Then
                    Else
                        AIRSNumber = Mid(dr.Item("strAIRSnumber"), 3)
                    End If
                End If

                Select Case AirProgramCode
                    Case "8"
                        SQL = "Select " & _
                        "strSubPart " & _
                        "from " & DBNameSpace & ".APBSubpartData " & _
                        "where strSubpartKey = '04" & AIRSNumber & "8' "

                        cmd2 = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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
                                    Len = Subpart.Length
                                    If Subpart.Length <> 5 Then
                                        For i As Integer = Len To 4
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
                            Len = SubpartData.Length
                            If SubpartData.Length <> 64 Then
                                For i As Integer = Len To 63
                                    SubpartData = SubpartData & " "
                                Next
                            End If
                            SubpartData = AIRSNumber & "1228A" & SubpartData & UpdateCode
                        End If
                    Case "9"
                        SQL = "Select " & _
                        "strSubPart " & _
                        "from " & DBNameSpace & ".APBSubpartData " & _
                        "where strSubpartKey = '04" & AIRSNumber & "9' "

                        cmd2 = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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
                                    Len = Subpart.Length
                                    If Subpart.Length <> 5 Then
                                        For i As Integer = Len To 4
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
                            Len = SubpartData.Length
                            If SubpartData.Length <> 64 Then
                                For i As Integer = Len To 63
                                    SubpartData = SubpartData & " "
                                Next
                            End If
                            SubpartData = AIRSNumber & "1229A" & SubpartData & UpdateCode
                        End If
                    Case "M"
                        SQL = "Select " & _
                         "strSubPart " & _
                         "from " & DBNameSpace & ".APBSubpartData " & _
                         "where strSubpartKey = '04" & AIRSNumber & "M' "

                        cmd2 = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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
                                    Len = Subpart.Length
                                    If Subpart.Length <> 5 Then
                                        For i As Integer = Len To 4
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
                            Len = SubpartData.Length
                            If SubpartData.Length <> 64 Then
                                For i As Integer = Len To 63
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

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & AirProgramPollutantLines


            SQL = "Select " & _
          "distinct(substr(strSubPartkey, 13, 1)) as subpart,  " & _
          "" & DBNameSpace & ".APBSubpartData.strAIRSnumber, " & _
          "" & DBNameSpace & ".AFSAirPollutantData.strUpdateStatus    " & _
          "from " & DBNameSpace & ".APBSubpartData, " & DBNameSpace & ".AFSAirPollutantData,   " & _
          "" & DBNameSpace & ".AFSFacilityData, airbranch.apbHeaderdata " & _
          "where " & DBNameSpace & ".APBSubpartData.strSubpartKey = " & _
          "" & DBNameSpace & ".AFSAirPollutantData.strAIRPollutantKey  " & _
          " and airbranch.afsfacilitydata.strairsnumber = airbranch.apbHeaderdata.strairsnumber " & _
          "and stroperationalstatus  = 'O' " & _
          " and " & DBNameSpace & ".AFSAirPollutantData.strAIRSNUmber = " & _
          "" & DBNameSpace & ".AFSFacilityData.strAIRSNUmber  " & _
          " and " & DBNameSpace & ".AFSFacilityData.strUpdateStatus <>  'A' " & _
          "   and AIRBranch.AFSFacilityData.strUpdateStatus <>  'H' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                AIRProgramCode = ""
                AIRSNumber = ""
                UpdateCode = "C"

                If IsDBNull(dr.Item("SubPart")) Then
                    AIRProgramCode = ""
                Else
                    AIRProgramCode = dr.Item("SubPart")
                    If IsDBNull(dr.Item("strAIRSnumber")) Then
                    Else
                        AIRSNumber = Mid(dr.Item("strAIRSnumber"), 3)
                    End If
                End If

                Select Case AIRProgramCode
                    Case "8"
                        SQL = "Select " & _
                        "strSubPart " & _
                        "from " & DBNameSpace & ".APBSubpartData " & _
                        "where strSubpartKey = '04" & AIRSNumber & "8' "

                        cmd2 = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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
                                    Len = Subpart.Length
                                    If Subpart.Length <> 5 Then
                                        For i As Integer = Len To 4
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
                            Len = SubpartData.Length
                            If SubpartData.Length <> 64 Then
                                For i As Integer = Len To 63
                                    SubpartData = SubpartData & " "
                                Next
                            End If
                            SubpartData = AIRSNumber & "1228A" & SubpartData & UpdateCode
                        End If
                    Case "9"
                        SQL = "Select " & _
                        "strSubPart " & _
                        "from " & DBNameSpace & ".APBSubpartData " & _
                        "where strSubpartKey = '04" & AIRSNumber & "9' "

                        cmd2 = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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
                                    Len = Subpart.Length
                                    If Subpart.Length <> 5 Then
                                        For i As Integer = Len To 4
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
                            Len = SubpartData.Length
                            If SubpartData.Length <> 64 Then
                                For i As Integer = Len To 63
                                    SubpartData = SubpartData & " "
                                Next
                            End If
                            SubpartData = AIRSNumber & "1229A" & SubpartData & UpdateCode
                        End If
                    Case "M"
                        SQL = "Select " & _
                         "strSubPart " & _
                         "from " & DBNameSpace & ".APBSubpartData " & _
                         "where strSubpartKey = '04" & AIRSNumber & "M' "

                        cmd2 = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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
                                    Len = Subpart.Length
                                    If Subpart.Length <> 5 Then
                                        For i As Integer = Len To 4
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
                            Len = SubpartData.Length
                            If SubpartData.Length <> 64 Then
                                For i As Integer = Len To 63
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

            txtAFSBatchFile.Text = txtAFSBatchFile.Text & AirProgramPollutantLines

            Dim FileName As String = ""
            Dim path As New SaveFileDialog
            Dim DestFilePath As String = "N/A"
            Dim OutPutFile As String = ""
            Dim da As OracleDataAdapter
            Dim cmdCB As OracleCommandBuilder
            Dim ds As DataSet

            If txtAFSBatchFile.Text = "" Then
                txtAFSBatchFile.Text = "NO AFS DATA TO UPDATE."
            Else
                SQL = "select " & DBNameSpace & ".afsFileNumber.nextval from Dual"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
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
                        "from " & DBNameSpace & ".AFSBatchFiles " & _
                        "where AFSFileName = '" & FileName & "' "

                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        da = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mtbAFSAirsNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mtbAFSAirsNumber.TextChanged
        Try
            If mtbAFSAirsNumber.Text.Length = 8 Then
                btnAIRSSpecificRefresh.Enabled = True
            Else
                btnAIRSSpecificRefresh.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAIRSSpecificRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAIRSSpecificRefresh.Click
        Try
            txtAFSBatchFile.Clear()
            SQL = "Update AIRBranch.AFSAirPollutantData set " & _
            "strUpdateStatus = 'A' " & _
            "where strAIRSnumber = '0413" & mtbAFSAirsNumber.Text & "' " & _
            "and (strUpdateStatus <> 'D' and strUpdateStatus <> 'H') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            SQL = "Update AIRBranch.AFSFacilityData set " & _
           "strUpdateStatus = 'A' " & _
           "where strAIRSnumber = '0413" & mtbAFSAirsNumber.Text & "' " & _
           "and (strUpdateStatus <> 'D' and strUpdateStatus <> 'H') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            SQL = "Update airbranch.AFSISMPRecords " & _
             "set strUpdateStatus = 'A' " & _
             "where exists (select * from airbranch.ISMPMaster " & _
             "where ISMPMaster.STRREFERENCENUMBER = AFSISMPRecords.STRREFERENCENUMBER " & _
             "and strAIRSnumber = '0413" & mtbAFSAirsNumber.Text & "' " & _
             "and (strUpdateStatus <> 'D' and strUpdateStatus <> 'H'))"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            SQL = "Update airbranch.AFSSSCPEnforcementRecords " & _
           "set strUpdateStatus = 'A' " & _
           "where exists (select * from airbranch.SSCP_AuditedEnforcement " & _
           "where SSCP_AuditedEnforcement.STRENFORCEMENTNUMBER = AFSSSCPEnforcementRecords.STRENFORCEMENTNUMBER " & _
           "and strAIRSnumber = '0413" & mtbAFSAirsNumber.Text & "' " & _
           "and (strUpdateStatus <> 'D' and strUpdateStatus <> 'H'))"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            SQL = "Update airbranch.AFSSSCPFCERecords " & _
            "set strUpdateStatus = 'A' " & _
            "where exists (select * from airbranch.SSCPFCEMaster " & _
            "where SSCPFCEMaster.STRFCENUMBER = AFSSSCPFCERecords.STRFCENUMBER " & _
            "and strAIRSnumber = '0413" & mtbAFSAirsNumber.Text & "' " & _
            "and (strUpdateStatus <> 'D' and strUpdateStatus <> 'H'))"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            SQL = "Update airbranch.AFSSSCPRecords " & _
            "set strUpdateStatus = 'A' " & _
            "where exists (select * from airbranch.SSCPItemMaster " & _
            "where SSCPItemMaster.strTrackingNumber = AFSSSCPRecords.strTrackingNumber " & _
            "and strAIRSnumber = '0413" & mtbAFSAirsNumber.Text & "' " & _
            "and (strUpdateStatus <> 'D' and strUpdateStatus <> 'H'))"


            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            SQL = "Update airbranch.AFSSSPPRecords " & _
            "set strUpdateStatus = 'A' " & _
            "where exists (select * from airbranch.SSPPApplicationMaster " & _
            "where SSPPApplicationMaster.strApplicationNumber = AFSSSPPRecords.strApplicationNumber " & _
            "and strAIRSnumber = '0413" & mtbAFSAirsNumber.Text & "' " & _
            "and (strUpdateStatus <> 'D' and strUpdateStatus <> 'H'))"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            GenerateNewFacilities()
            AddNewAirPollutants()
            GenerateFacilityChanges()
            PermittingActions()
            ISMPActions()
            ComplianceActions()
            FCEActions()
            EnforcementActions()

            SQL = "Update AIRBranch.AFSAirPollutantData set " & _
            "strUpdateStatus = 'C' " & _
            "where strAIRSnumber = '0413" & mtbAFSAirsNumber.Text & "' " & _
            "and (strUpdateStatus <> 'D' and strUpdateStatus <> 'H') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            SQL = "Update AIRBranch.AFSFacilityData set " & _
          "strUpdateStatus = 'C' " & _
          "where strAIRSnumber = '0413" & mtbAFSAirsNumber.Text & "' " & _
          "and (strUpdateStatus <> 'D' and strUpdateStatus <> 'H') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            SQL = "Update airbranch.AFSISMPRecords " & _
                "set strUpdateStatus = 'C' " & _
                "where exists (select * from airbranch.ISMPMaster " & _
                "where ISMPMaster.STRREFERENCENUMBER = AFSISMPRecords.STRREFERENCENUMBER " & _
                "and strAIRSnumber = '0413" & mtbAFSAirsNumber.Text & "' " & _
                "and (strUpdateStatus <> 'D' and strUpdateStatus <> 'H'))"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            SQL = "Update airbranch.AFSSSCPEnforcementRecords " & _
           "set strUpdateStatus = 'C' " & _
           "where exists (select * from airbranch.SSCP_AuditedEnforcement " & _
           "where SSCP_AuditedEnforcement.STRENFORCEMENTNUMBER = AFSSSCPEnforcementRecords.STRENFORCEMENTNUMBER " & _
           "and strAIRSnumber = '0413" & mtbAFSAirsNumber.Text & "' " & _
           "and (strUpdateStatus <> 'D' and strUpdateStatus <> 'H'))"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            SQL = "Update airbranch.AFSSSCPFCERecords " & _
            "set strUpdateStatus = 'C' " & _
            "where exists (select * from airbranch.SSCPFCEMaster " & _
            "where SSCPFCEMaster.STRFCENUMBER = AFSSSCPFCERecords.STRFCENUMBER " & _
            "and strAIRSnumber = '0413" & mtbAFSAirsNumber.Text & "' " & _
            "and (strUpdateStatus <> 'D' and strUpdateStatus <> 'H'))"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            SQL = "Update airbranch.AFSSSCPRecords " & _
            "set strUpdateStatus = 'C' " & _
            "where exists (select * from airbranch.SSCPItemMaster " & _
            "where SSCPItemMaster.strTrackingNumber = AFSSSCPRecords.strTrackingNumber " & _
            "and strAIRSnumber = '0413" & mtbAFSAirsNumber.Text & "' " & _
            "and (strUpdateStatus <> 'D' and strUpdateStatus <> 'H'))"


            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            SQL = "Update airbranch.AFSSSPPRecords " & _
            "set strUpdateStatus = 'C' " & _
            "where exists (select * from airbranch.SSPPApplicationMaster " & _
            "where SSPPApplicationMaster.strApplicationNumber = AFSSSPPRecords.strApplicationNumber " & _
            "and strAIRSnumber = '0413" & mtbAFSAirsNumber.Text & "' " & _
            "and (strUpdateStatus <> 'D' and strUpdateStatus <> 'H'))"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

            GenerateNewFacilities()
            AddNewAirPollutants()
            GenerateFacilityChanges()
            PermittingActions()
            ISMPActions()
            ComplianceActions()
            FCEActions()
            EnforcementActions()

            Dim FileName As String = ""
            Dim path As New SaveFileDialog
            Dim DestFilePath As String = "N/A"
            Dim OutPutFile As String = ""
            Dim da As OracleDataAdapter
            Dim cmdCB As OracleCommandBuilder
            Dim ds As DataSet

            If txtAFSBatchFile.Text = "" Then
                txtAFSBatchFile.Text = "NO AFS DATA TO UPDATE."
            Else
                SQL = "select " & DBNameSpace & ".afsFileNumber.nextval from Dual"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
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
                        "from " & DBNameSpace & ".AFSBatchFiles " & _
                        "where AFSFileName = '" & FileName & "' "

                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        da = New OracleDataAdapter(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbGenerateStandardFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGenerateStandardFile.CheckedChanged
        Try
            pnlStandardFile.Enabled = True
            pnlAIRSSpecific.Enabled = False
            pnlSubParts.Enabled = False
            pnlBasicRefresh.Enabled = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbAIRSSpecific_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbAIRSSpecific.CheckedChanged
        Try
            pnlStandardFile.Enabled = False
            pnlAIRSSpecific.Enabled = True
            pnlSubParts.Enabled = False
            pnlBasicRefresh.Enabled = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbUpdateAllSubparts_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbUpdateAllSubparts.CheckedChanged
        Try
            pnlStandardFile.Enabled = False
            pnlAIRSSpecific.Enabled = False
            pnlSubParts.Enabled = True
            pnlBasicRefresh.Enabled = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbBasicData_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbBasicData.CheckedChanged
        Try
            pnlStandardFile.Enabled = False
            pnlAIRSSpecific.Enabled = False
            pnlSubParts.Enabled = False
            pnlBasicRefresh.Enabled = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class