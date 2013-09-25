'Imports System.DateTime
Imports Oracle.DataAccess.Client

Imports System.Windows.Forms

Public Class ISMPTestReportAdministrative
    Dim SQL, SQL2 As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim dsPollutant As DataSet
    Dim daPollutant As OracleDataAdapter
    Dim dsTestingFirms As DataSet
    Dim daTestingFirms As OracleDataAdapter
    Dim dsManagers As DataSet
    Dim daManagers As OracleDataAdapter
    Dim dsComplianceManagers As DataSet
    Dim daComplianceManagers As OracleDataAdapter
    Dim dsGrid As DataSet
    Dim daGrid As OracleDataAdapter
    Dim dsFacility As DataSet
    Dim daFacility As OracleDataAdapter

    Private Sub DevTestReportAdministrative_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            DTPDateReceived.Text = Date.Today
            DTPTestDateStart.Text = Date.Today
            DTPTestDateEnd.Text = Date.Today
            DTPTestDateStart.Value = Date.Today
            DTPTestDateEnd.Value = Date.Today

            DTPDateClosed.Text = Date.Today
            rdbOpenReport.Checked = False
            rdbCloseReport.Checked = False

            Panel1.Text = "Select a Function..."
            Panel2.Text = UserName
            Panel3.Text = OracleDate

            'These two were commented out to speed up the loading of this form
            'FillFacilityDataSet()
            'FillFacilityAndAIRSCombos()

            FillPollutantandTestingFirms()
            FillPollutantCombo()
            FillTestingFirmsCombo()

            bgw1.WorkerReportsProgress = True
            bgw1.WorkerSupportsCancellation = True
            bgw1.RunWorkerAsync()

            dtpAddTestReportDateReceived.Text = OracleDate
            DTPAddTestReportDateCompleted.Text = OracleDate


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#Region "Page Load"
    Private Sub FillFacilityDataSet()
        Try

            SQL = "select strFacilityName, substr(strAIRSNumber, 5) as strAIRSNumber, " & _
            "strFacilityStreet1, strFacilityCity, strFacilityState, " & _
            "strFacilityZipCode " & _
            "from " & DBNameSpace & ".APBFacilityInformation order by strFacilityName"

            dsFacility = New DataSet

            daFacility = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daFacility.Fill(dsFacility, "APBFacilities")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Private Sub FillFacilityAndAIRSCombos()
        Dim dtFacility As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try

            dtFacility.Columns.Add("strFacilityName", GetType(System.String))
            dtFacility.Columns.Add("strAIRSNumber", GetType(System.String))
            dtFacility.Columns.Add("strFacilityStreet1", GetType(System.String))
            dtFacility.Columns.Add("strFacilityCity", GetType(System.String))
            dtFacility.Columns.Add("strFacilityState", GetType(System.String))
            dtFacility.Columns.Add("strFacilityZipCode", GetType(System.String))

            drNewRow = dtFacility.NewRow()
            drNewRow("strFacilityName") = " "
            drNewRow("strAIRSNumber") = " "
            drNewRow("strFacilityStreet1") = " "
            drNewRow("strFacilityCity") = " "
            drNewRow("strFacilityState") = " "
            drNewRow("strFacilityZipCode") = " "
            dtFacility.Rows.Add(drNewRow)

            For Each drDSRow In dsFacility.Tables("APBFacilities").Rows()
                drNewRow = dtFacility.NewRow()
                drNewRow("strFacilityName") = drDSRow("strFacilityName")
                drNewRow("strAIRSNumber") = drDSRow("strAIRSNumber")
                drNewRow("strFacilityStreet1") = drDSRow("strFacilityStreet1")
                drNewRow("strFacilityCity") = drDSRow("strFacilityCity")
                drNewRow("strFacilityState") = drDSRow("strFacilityState")
                drNewRow("strFacilityZipCode") = drDSRow("strFacilityZipCode")
                dtFacility.Rows.Add(drNewRow)
            Next

            With cboAIRSNumber
                .DataSource = dtFacility
                .DisplayMember = "strAIRSNumber"
                .ValueMember = "strAIRSNumber"
                .SelectedIndex = 0
            End With

            With cboFacilityName
                .DataSource = dtFacility
                .DisplayMember = "strFacilityName"
                .ValueMember = "strAIRSNumber"
                .SelectedIndex = 0
            End With

            With txtFacilityAddress
                .DataBindings.Add(New Binding("Text", dtFacility, "strFacilityStreet1"))
            End With

            With txtFacilityCity
                .DataBindings.Add(New Binding("Text", dtFacility, "strFacilityCity"))
            End With

            With txtFacilityState
                .DataBindings.Add(New Binding("Text", dtFacility, "strFacilityState"))
            End With

            With txtFacilityZipCode
                .DataBindings.Add(New Binding("Text", dtFacility, "strFacilityZipCode"))
            End With
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub FillPollutantandTestingFirms()
        Try

            SQL = "Select strPollutantCode, strPollutantDescription from " & DBNameSpace & ".LookUPPollutants order by strPollutantDescription"
            SQL2 = "Select strTestingFirmKey, strTestingFirm from " & DBNameSpace & ".LookUPTestingFirms order by strTestingFirm"

            dsPollutant = New DataSet
            dsTestingFirms = New DataSet

            daPollutant = New OracleDataAdapter(SQL, Conn)
            daTestingFirms = New OracleDataAdapter(SQL2, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daPollutant.Fill(dsPollutant, "Pollutant")
            daTestingFirms.Fill(dsTestingFirms, "TestingFirms")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub FillPollutantCombo()

        Dim dtPollutant As New DataTable

        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try

            dtPollutant.Columns.Add("strPollutantDescription", GetType(System.String))
            dtPollutant.Columns.Add("strPollutantCode", GetType(System.String))

            drNewRow = dtPollutant.NewRow()
            drNewRow("strPollutantDescription") = " "
            drNewRow("strPollutantCode") = " "
            dtPollutant.Rows.Add(drNewRow)


            For Each drDSRow In dsPollutant.Tables("Pollutant").Rows()
                drNewRow = dtPollutant.NewRow()
                drNewRow("strPollutantDescription") = drDSRow("strPollutantDescription")
                drNewRow("strPollutantCode") = drDSRow("strPollutantCode")
                dtPollutant.Rows.Add(drNewRow)
            Next

            With cboPollutant
                .DataSource = dtPollutant
                .DisplayMember = "strPollutantDescription"
                .ValueMember = "strPollutantCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
        End Try

    End Sub
    Private Sub FillTestingFirmsCombo()
        Dim dtTestingFirm As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try
            dtTestingFirm.Columns.Add("strTestingFirm", GetType(System.String))
            dtTestingFirm.Columns.Add("strTestingFirmKey", GetType(System.String))

            drNewRow = dtTestingFirm.NewRow()
            drNewRow("strTestingFirm") = " "
            drNewRow("strTestingFirmKey") = " "
            dtTestingFirm.Rows.Add(drNewRow)

            For Each drDSRow In dsTestingFirms.Tables("TestingFirms").Rows()
                drNewRow = dtTestingFirm.NewRow()
                drNewRow("strTestingFirm") = drDSRow("strTestingFirm")
                drNewRow("strTestingFirmKey") = drDSRow("strTestingFirmKey")
                dtTestingFirm.Rows.Add(drNewRow)
            Next

            With cboTestingFirms
                .DataSource = dtTestingFirm
                .DisplayMember = "strTestingFirm"
                .ValueMember = "strTestingFirmKey"
                .SelectedIndex = 0
            End With
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub FillDateGrid()
        Dim SQL As String

        SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
        "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
        "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
        " substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as StrAIRSNumber, " & _
        "strFacilityName, " & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
        "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & _
        "" & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
        "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
        "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
        "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
        "and strDelete is NULL " & _
        "and strClosed = 'False' " & _
        "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

        dsGrid = New DataSet
        daGrid = New OracleDataAdapter(SQL, Conn)

        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If

        daGrid.Fill(dsGrid, "Grid")

    End Sub

#End Region

#Region "Subs and Functions"
    Private Sub LoadAddress()
        Dim SQL As String
        Dim temp As String
        Try


            If btnSearchForAIRS.Visible = True And cboAIRSNumber.Text <> "" And cboAIRSNumber.Text.Length = 8 Then
                cboFacilityName.Text = ""
                txtFacilityAddress.Clear()
                txtFacilityCity.Clear()
                txtFacilityState.Clear()
                txtFacilityZipCode.Clear()

                SQL = "select " & _
                "strFacilityName, strFacilityStreet1, " & _
                "strFacilityCity, " & _
                "strFacilityState, strFacilityZipcode " & _
                "from " & DBNameSpace & ".APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & cboAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        cboFacilityName.Text = ""
                    Else
                        cboFacilityName.Text = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        txtFacilityAddress.Clear()
                    Else
                        txtFacilityAddress.Text = dr.Item("strFacilityStreet1")
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        txtFacilityCity.Clear()
                    Else
                        txtFacilityCity.Text = dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strFacilityState")) Then
                        txtFacilityState.Clear()
                    Else
                        txtFacilityState.Text = dr.Item("strFacilityState")
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        txtFacilityZipCode.Clear()
                    Else
                        txtFacilityZipCode.Text = dr.Item("strFacilityZipCode")
                    End If
                End If
                dr.Close()
                Exit Sub
            End If

            If cboAIRSNumber.SelectedIndex <> -1 Then

                SQL = "Select strFacilityName, strFacilityStreet1, " & _
                "strFacilityCity, strFacilityState, strFacilityZipCode " & _
                "from " & DBNameSpace & ".APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & cboAIRSNumber.Text & "'"

                Dim cmd As New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                Dim dr As OracleDataReader = cmd.ExecuteReader
                Dim recExist As Boolean = dr.Read
                If recExist Then
                    temp = dr.Item("strFacilityName")
                    If temp <> cboFacilityName.Text Then
                        txtFacilityAddress.Text = ""
                        txtFacilityCity.Text = ""
                        txtFacilityState.Text = ""
                        txtFacilityZipCode.Text = ""
                    Else
                        txtFacilityAddress.Text = dr.Item("strFacilityStreet1")
                        txtFacilityCity.Text = dr.Item("StrFacilityCity")
                        txtFacilityState.Text = dr.Item("strFacilityState")
                        txtFacilityZipCode.Text = Mid(dr.Item("strFacilityZipCode"), 1, 5)
                    End If
                Else
                    txtFacilityAddress.Text = ""
                    txtFacilityCity.Text = ""
                    txtFacilityState.Text = ""
                    txtFacilityZipCode.Text = ""
                End If
            Else
                txtFacilityAddress.Text = ""
                txtFacilityCity.Text = ""
                txtFacilityState.Text = ""
                txtFacilityZipCode.Text = ""
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub FillTestReportList(ByVal ReferenceNumber As String)
        Try


            Me.clbReferenceNumbers.Items.Clear()

            If DTPDateReceived.Text <> "" And cboAIRSNumber.Text <> "" Then
                SQL = "Select " & _
                "" & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
                "strEmissionSource, strPollutantDescription " & _
                "from " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".ISMPReportInformation, " & _
                "" & DBNameSpace & ".LookUPPollutants " & _
                "where " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                "and " & DBNameSpace & ".ISMPReportInformation.strPollutant = " & DBNameSpace & ".LookUPPollutants.strPollutantCode " & _
                "and strAIRSNumber = '0413" & cboAIRSNumber.Text & "' " & _
                "and datReceivedDate = '" & DTPDateReceived.Text & "' " & _
                "and strClosed <> 'True' " & _
                "and (strDelete <> 'DELETE' " & _
                "or strDelete is NUll) " & _
                "Order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    clbReferenceNumbers.Items.Add(dr.Item("strReferenceNumber") & " - " & dr.Item("strEmissionSource") & " - " & dr.Item("strPollutantDescription"))
                    If clbReferenceNumbers.Items.Contains(txtReferenceNumber.Text _
                             & " - " & dr.Item("strEmissionSource") & " - " & dr.Item("strPollutantDescription")) Then
                        clbReferenceNumbers.SetItemCheckState(clbReferenceNumbers.FindString(txtReferenceNumber.Text), CheckState.Checked)
                    End If
                End While
                dr.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub GetNextReferenceNumber()
        Dim SQL As String = ""
        Dim RefNum As String = ""
        Dim RefYear As String = Now.Year.ToString

        Try


            SQL = "select " & _
            "case when max(strReferenceNumber) is null then to_char(sysdate, 'YYYY')||'00001'  " & _
            "else to_char(max(to_number(strReferenceNumber) + 1 )) " & _
            "end MaxRefNum  " & _
            "from " & DBNameSpace & ".ISMPMaster  " & _
            "where strReferenceNumber like '" & RefYear & "%' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("MaxRefNum")) Then
                    RefNum = RefYear & "00001"
                Else
                    RefNum = dr.Item("MaxRefNum")
                End If
            End While
            dr.Close()

            txtReferenceNumber.Text = RefNum

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub Save()
        Dim RecordStatus As String = "False"
        Dim AIRSNumber As String = ""

        Try

            If rdbCloseReport.Checked = False Then

            Else
                MsgBox("This record is currently marked as being closed." & vbCrLf & "Click Open Record to Save information.", _
                MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
                Exit Sub
            End If

            If btnSearchForAIRS.Visible = True Then
                If cboAIRSNumber.Text <> "" And cboAIRSNumber.Text.Length = 8 Then
                    AIRSNumber = cboAIRSNumber.Text
                Else
                    MsgBox("Invalid AIRS Number", MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
                    Exit Sub
                End If
            Else
                If cboAIRSNumber.SelectedIndex <> -1 And cboAIRSNumber.SelectedIndex <> 0 Then
                    AIRSNumber = cboAIRSNumber.SelectedValue
                Else
                    MsgBox("The Facility Name does not correspond to the AIRS Number provided." _
                     & vbCr & "This must be corrected before moving on.", MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
                    Exit Sub
                End If
            End If

            If cboPollutant.SelectedIndex <> -1 And cboPollutant.SelectedIndex <> 0 Then
            Else
                MsgBox("The Pollutant does not match any of the provided pollutants." _
                  & vbCr & "This must be corrected before moving on.", MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
                Exit Sub
            End If
            If cboTestingFirms.SelectedIndex <> -1 And cboTestingFirms.SelectedIndex <> 0 Then
            Else
                MsgBox("The Testing Firm does not match any of the provided Testing Firms." _
                  & vbCr & "This must be corrected before moving on.", MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
                Exit Sub
            End If

            If chbOverright.Checked = False Then
                If txtReferenceNumber.Text = "" Then
                    GetNextReferenceNumber()
                End If
            End If

            If txtEmissionSource.Text = "" Then
                txtEmissionSource.Text = "N/A"
            End If

            '   If txtReferenceNumber.Text <> "" And txtReferenceNumber.Text.Length = 9 Then
            If txtReferenceNumber.Text <> "" Then
                If rdbOpenReport.Checked = False And rdbCloseReport.Checked = False Then
                    rdbOpenReport.Checked = True
                End If

                SQL = "Select strReferenceNumber from " & DBNameSpace & ".ISMPMaster where strReferenceNumber = '" & txtReferenceNumber.Text & "'"

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".ISMPMaster set " & _
                    "strAIRSNumber = '0413" & AIRSNumber & "' " & _
                    "where strReferenceNumber = '" & txtReferenceNumber.Text & "'"

                    SQL2 = "Update " & DBNameSpace & ".ISMPReportInformation set " & _
                    "strPollutant = '" & cboPollutant.SelectedValue & "', " & _
                    "strEmissionSource = '" & txtEmissionSource.Text & "', " & _
                    "strTestingFirm = '" & cboTestingFirms.SelectedValue & "', " & _
                    "datTestDateStart = '" & DTPTestDateStart.Text & "', " & _
                    "datTestDateEnd = '" & DTPTestDateEnd.Text & "', " & _
                    "datReceivedDate = '" & DTPDateReceived.Text & "', " & _
                    "datCompleteDate = '" & DTPDateClosed.Text & "', " & _
                    "strClosed = '" & RecordStatus & "', " & _
                    "strDelete = '' " & _
                    "where strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                Else
                    SQL = "Insert into " & DBNameSpace & ".ISMPMaster values ('" & txtReferenceNumber.Text & "', " & _
                    "'0413" & AIRSNumber & "', '" & UserGCode & "', " & _
                    "'" & OracleDate & "')"


                    'This SQl statement was changed on 15-Oct-09 when LookUpAPBManagementType was created. MFloyd

                    ' SQL2 = "Insert into " & DBNameSpace & ".ISMPReportInformation " & _
                    '"(strReferenceNumber, strPollutant, strEmissionSource, " & _
                    '"strReportType, strDocumentType, strApplicableRequirement, " & _
                    '"strTestingFirm, strReviewingEngineer, strWitnessingEngineer, " & _
                    '"strWitnessingEngineer2, strReviewingUnit, datReviewedbyUnitManager, " & _
                    '"strComplianceManager, datTestDateStart, datTestDateEnd, " & _
                    '"datReceivedDate, datCompleteDate, mmoCommentArea, strClosed, " & _
                    '"strDirector, strCommissioner, strProgramManager, " & _
                    '"strComplianceStatus, strcc, strModifingPerson, datModifingDate, " & _
                    '"strControlEquipmentData, strDelete, numReviewingManager) " & _
                    '"values " & _
                    '"('" & txtReferenceNumber.Text & "', " & _
                    '"'" & cboPollutant.SelectedValue & "', " & _
                    '"'" & Replace(txtEmissionSource.Text, "'", "''") & "', " & _
                    '"'004', " & _
                    '"'001', " & _
                    '"'Incomplete', " & _
                    '"'" & cboTestingFirms.SelectedValue & "', " & _
                    '"'0', '0', '0', " & _
                    '"'0', " & _
                    '"'04-Jul-1776', " & _
                    '"(SELECT " & _
                    '"CASE  " & _
                    '"WHEN strDistrictResponsible <> 'False' AND strDistrictResponsible IS NOT NULL  " & _
                    '"       THEN strDistrictManager  " & _
                    '"WHEN strSSCPAssigningManager <> '1' AND strSSCPAssigningManager IS NOT NULL  " & _
                    '"       THEN strSSCPAssigningManager  " & _
                    '"ELSE '337' " & _
                    '"END ManagerResponsible  " & _
                    '"from " & DBNameSpace & ".LookUPDistricts, " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION,  " & _
                    '"" & DBNameSpace & ".SSCPDistrictResponsible, " & DBNameSpace & ".SSCPFacilityAssignment    " & _
                    '"WHERE " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION.strDistrictCode = " & DBNameSpace & ".LookUPDistricts.strDistrictCode (+) " & _
                    '"AND " & DBNameSpace & ".SSCPFacilityAssignment.strAIRSNumber = " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber (+) " & _
                    '"AND SUBSTR(" & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber, 5, 3) = strDistrictCounty (+) " & _
                    '"AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = '0413" & cboAIRSNumber.Text & "'), " & _
                    '"'" & DTPTestDateStart.Text & "', '" & DTPTestDateEnd.Text & "', " & _
                    '"'" & DTPDateReceived.Text & "', " & _
                    '"'04-Jul-1776', 'N/A', '" & RecordStatus & "', " & _
                    '"(select strDirector from " & DBNameSpace & ".LookUpAPBManagement), " & _
                    '"(select strCommissioner from " & DBNameSpace & ".LookUpAPBManagement), " & _
                    '"(select strISMPProgramMang from " & DBNameSpace & ".LookUpAPBManagement), " & _
                    '"'01', " & _
                    '"(SELECT " & _
                    '"CASE " & _
                    '"WHEN strDistrictResponsible <> 'False' AND strDistrictResponsible IS NOT NULL " & _
                    '" THEN '9' " & _
                    '"ELSE '0'   " & _
                    '"END ManagerResponsible " & _
                    '"from " & DBNameSpace & ".LookUPDistricts, " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION, " & _
                    '"" & DBNameSpace & ".SSCPDistrictResponsible, " & DBNameSpace & ".SSCPFacilityAssignment   " & _
                    '"WHERE " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION.strDistrictCode = " & DBNameSpace & ".LookUPDistricts.strDistrictCode (+)   " & _
                    '"AND " & DBNameSpace & ".SSCPFacilityAssignment.strAIRSNumber = " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber (+)   " & _
                    '"AND SUBSTR(" & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber, 5, 3) = strDistrictCounty (+)   " & _
                    '"AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = '0413" & cboAIRSNumber.Text & "'), " & _
                    '"'" & UserGCode & "', '" & OracleDate & "', " & _
                    '"'N/A', '', '')"

                    '  SQL2 = "Insert into " & DBNameSpace & ".ISMPReportInformation " & _
                    '"(strReferenceNumber, strPollutant, strEmissionSource, " & _
                    '"strReportType, strDocumentType, strApplicableRequirement, " & _
                    '"strTestingFirm, strReviewingEngineer, strWitnessingEngineer, " & _
                    '"strWitnessingEngineer2, strReviewingUnit, datReviewedbyUnitManager, " & _
                    '"strComplianceManager, datTestDateStart, datTestDateEnd, " & _
                    '"datReceivedDate, datCompleteDate, mmoCommentArea, strClosed, " & _
                    '"strDirector, strCommissioner, strProgramManager, " & _
                    '"strComplianceStatus, strcc, strModifingPerson, datModifingDate, " & _
                    '"strControlEquipmentData, strDelete, numReviewingManager) " & _
                    '"values " & _
                    '"('" & txtReferenceNumber.Text & "', " & _
                    '"'" & cboPollutant.SelectedValue & "', " & _
                    '"'" & Replace(txtEmissionSource.Text, "'", "''") & "', " & _
                    '"'004', " & _
                    '"'001', " & _
                    '"'Incomplete', " & _
                    '"'" & cboTestingFirms.SelectedValue & "', " & _
                    '"'0', '0', '0', " & _
                    '"'0', " & _
                    '"'04-Jul-1776', " & _
                    '"(SELECT " & _
                    '"CASE  " & _
                    '"WHEN strDistrictResponsible <> 'False' AND strDistrictResponsible IS NOT NULL  " & _
                    '"       THEN strDistrictManager  " & _
                    '"WHEN strSSCPAssigningManager <> '1' AND strSSCPAssigningManager IS NOT NULL  " & _
                    '"       THEN strSSCPAssigningManager  " & _
                    '"ELSE '337' " & _
                    '"END ManagerResponsible  " & _
                    '"from " & DBNameSpace & ".LookUPDistricts, " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION,  " & _
                    '"" & DBNameSpace & ".SSCPDistrictResponsible, " & DBNameSpace & ".SSCPFacilityAssignment    " & _
                    '"WHERE " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION.strDistrictCode = " & DBNameSpace & ".LookUPDistricts.strDistrictCode (+) " & _
                    '"AND " & DBNameSpace & ".SSCPFacilityAssignment.strAIRSNumber = " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber (+) " & _
                    '"AND SUBSTR(" & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber, 5, 3) = strDistrictCounty (+) " & _
                    '"AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = '0413" & cboAIRSNumber.Text & "'), " & _
                    '"'" & DTPTestDateStart.Text & "', '" & DTPTestDateEnd.Text & "', " & _
                    '"'" & DTPDateReceived.Text & "', " & _
                    '"'04-Jul-1776', 'N/A', '" & RecordStatus & "', " & _
                    '"(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
                    '"where strKey = '1' and strCurrentContact = '1' ), " & _
                    '"(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
                    '"where strKey = '2' and strCurrentContact = '1' ), " & _
                    '"(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
                    '"where strKey = '5' and strCurrentContact = '1' ), " & _
                    '"'01', " & _
                    '"(SELECT " & _
                    '"CASE " & _
                    '"WHEN strDistrictResponsible <> 'False' AND strDistrictResponsible IS NOT NULL " & _
                    '" THEN '9' " & _
                    '"ELSE '0'   " & _
                    '"END ManagerResponsible " & _
                    '"from " & DBNameSpace & ".LookUPDistricts, " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION, " & _
                    '"" & DBNameSpace & ".SSCPDistrictResponsible, " & DBNameSpace & ".SSCPFacilityAssignment   " & _
                    '"WHERE " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION.strDistrictCode = " & DBNameSpace & ".LookUPDistricts.strDistrictCode (+)   " & _
                    '"AND " & DBNameSpace & ".SSCPFacilityAssignment.strAIRSNumber = " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber (+)   " & _
                    '"AND SUBSTR(" & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber, 5, 3) = strDistrictCounty (+)   " & _
                    '"AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = '0413" & cboAIRSNumber.Text & "'), " & _
                    '"'" & UserGCode & "', '" & OracleDate & "', " & _
                    '"'N/A', '', '')"

                    '        SQL2 = "Insert into " & DBNameSpace & ".ISMPReportInformation " & _
                    '"(strReferenceNumber, strPollutant, strEmissionSource, " & _
                    '"strReportType, strDocumentType, strApplicableRequirement, " & _
                    '"strTestingFirm, strReviewingEngineer, strWitnessingEngineer, " & _
                    '"strWitnessingEngineer2, strReviewingUnit, datReviewedbyUnitManager, " & _
                    '"strComplianceManager, datTestDateStart, datTestDateEnd, " & _
                    '"datReceivedDate, datCompleteDate, mmoCommentArea, strClosed, " & _
                    '"strDirector, strCommissioner, strProgramManager, " & _
                    '"strComplianceStatus, strcc, strModifingPerson, datModifingDate, " & _
                    '"strControlEquipmentData, strDelete, numReviewingManager) " & _
                    '"values " & _
                    '"('" & txtReferenceNumber.Text & "', " & _
                    '"'" & cboPollutant.SelectedValue & "', " & _
                    '"'" & Replace(txtEmissionSource.Text, "'", "''") & "', " & _
                    '"'004', " & _
                    '"'001', " & _
                    '"'Incomplete', " & _
                    '"'" & cboTestingFirms.SelectedValue & "', " & _
                    '"'0', '0', '0', " & _
                    '"'0', " & _
                    '"'04-Jul-1776', " & _
                    '"(SELECT " & _
                    '"CASE  " & _
                    '"WHEN strDistrictResponsible <> 'False' AND strDistrictResponsible IS NOT NULL  " & _
                    '"       THEN strDistrictManager  " & _
                    '"WHEN to_char(TABLE1.STRASSIGNINGMANAGER) <> '1' AND to_char(TABLE1.STRASSIGNINGMANAGER) IS NOT NULL  " & _
                    '"       THEN to_char(TABLE1.STRASSIGNINGMANAGER)  " & _
                    '"ELSE '337' " & _
                    '"END ManagerResponsible  " & _
                    '"from " & DBNameSpace & ".LookUPDistricts, " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION,  " & _
                    '"" & DBNameSpace & ".SSCPDistrictResponsible,     " & _
                    '"(Select " & _
                    '"max(intYear), strAssigningManager, strAIRSNumber " & _
                    '"from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                    '"group by strAssigningManager, strAIRSNumber) Table1 " & _
                    '"WHERE " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION.strDistrictCode = " & DBNameSpace & ".LookUPDistricts.strDistrictCode (+) " & _
                    '"AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = Table1.strAIRSnumber (+) " & _
                    '"AND SUBSTR(" & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber, 5, 3) = strDistrictCounty (+) " & _
                    '"AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = '0413" & cboAIRSNumber.Text & "'), " & _
                    '"'" & DTPTestDateStart.Text & "', '" & DTPTestDateEnd.Text & "', " & _
                    '"'" & DTPDateReceived.Text & "', " & _
                    '"'04-Jul-1776', 'N/A', '" & RecordStatus & "', " & _
                    '"(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
                    '"where strKey = '1' and strCurrentContact = '1' ), " & _
                    '"(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
                    '"where strKey = '2' and strCurrentContact = '1' ), " & _
                    '"(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
                    '"where strKey = '5' and strCurrentContact = '1' ), " & _
                    '"'01', " & _
                    '"(SELECT " & _
                    '"CASE " & _
                    '"WHEN strDistrictResponsible <> 'False' AND strDistrictResponsible IS NOT NULL " & _
                    '" THEN '9' " & _
                    '"ELSE '0'   " & _
                    '"END ManagerResponsible " & _
                    '"from " & DBNameSpace & ".LookUPDistricts, " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION,  " & _
                    '"" & DBNameSpace & ".SSCPDistrictResponsible,     " & _
                    '"(Select " & _
                    '"max(intYear), strAssigningManager, strAIRSNumber " & _
                    '"from " & DBNameSpace & ".SSCPInspectionsRequired " & _
                    '"group by strAssigningManager, strAIRSNumber) Table1 " & _
                    '"WHERE " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION.strDistrictCode = " & DBNameSpace & ".LookUPDistricts.strDistrictCode (+) " & _
                    '"AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = Table1.strAIRSnumber (+) " & _
                    '"AND SUBSTR(" & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber, 5, 3) = strDistrictCounty (+) " & _
                    '"AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = '0413" & cboAIRSNumber.Text & "'), " & _
                    '"'" & UserGCode & "', '" & OracleDate & "', " & _
                    '"'N/A', '', '')"

                    SQL2 = "Insert into " & DBNameSpace & ".ISMPReportInformation " & _
           "(strReferenceNumber, strPollutant, strEmissionSource, " & _
           "strReportType, strDocumentType, strApplicableRequirement, " & _
           "strTestingFirm, strReviewingEngineer, strWitnessingEngineer, " & _
           "strWitnessingEngineer2, strReviewingUnit, datReviewedbyUnitManager, " & _
           "strComplianceManager, datTestDateStart, datTestDateEnd, " & _
           "datReceivedDate, datCompleteDate, mmoCommentArea, strClosed, " & _
           "strDirector, strCommissioner, strProgramManager, " & _
           "strComplianceStatus, strcc, strModifingPerson, datModifingDate, " & _
           "strControlEquipmentData, strDelete, numReviewingManager) " & _
           "values " & _
           "('" & txtReferenceNumber.Text & "', " & _
           "'" & cboPollutant.SelectedValue & "', " & _
           "'" & Replace(txtEmissionSource.Text, "'", "''") & "', " & _
           "'004', " & _
           "'001', " & _
           "'Incomplete', " & _
           "'" & cboTestingFirms.SelectedValue & "', " & _
           "'0', '0', '0', " & _
           "'0', " & _
           "'04-Jul-1776', " & _
           "(SELECT " & _
           "CASE  WHEN strDistrictResponsible <> 'False' AND strDistrictResponsible IS NOT NULL         THEN strDistrictManager  " & _
           "WHEN to_char(TABLE1.STRASSIGNINGMANAGER) <> '1' AND to_char(TABLE1.STRASSIGNINGMANAGER) IS NOT NULL THEN to_char(TABLE1.STRASSIGNINGMANAGER) " & _
           " ELSE '337' " & _
           "END ManagerResponsible  " & _
           "from " & DBNameSpace & ".LookUPDistricts, " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION,  " & _
           "" & DBNameSpace & ".SSCPDISTRICTRESPONSIBLE,     " & _
           "(select " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.STRASSIGNINGMANAGER, " & _
           "" & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.strAIRSNumber " & _
           "from " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED, " & _
           "(select max(INTYEAR) as MAXYEAR, STRAIRSNUMBER " & _
           "from " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED " & _
           "group by STRAIRSNUMBER) MAXRESULTS " & _
           "where " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER = MAXRESULTS.STRAIRSNUMBER " & _
           "and " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.INTYEAR = MAXRESULTS.MAXYEAR) Table1 " & _
           "WHERE " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION.strDistrictCode = " & DBNameSpace & ".LookUPDistricts.strDistrictCode (+) " & _
           "AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = Table1.strAIRSnumber (+) " & _
           "AND SUBSTR(" & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber, 5, 3) = strDistrictCounty (+) " & _
           "and " & DBNameSpace & ".SSCPDISTRICTRESPONSIBLE.STRAIRSNUMBER = '0413" & cboAIRSNumber.Text & "'), " & _
           "'" & DTPTestDateStart.Text & "', '" & DTPTestDateEnd.Text & "', " & _
           "'" & DTPDateReceived.Text & "', " & _
           "'04-Jul-1776', 'N/A', '" & RecordStatus & "', " & _
           "(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
           "where strKey = '1' and strCurrentContact = '1' ), " & _
           "(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
           "where strKey = '2' and strCurrentContact = '1' ), " & _
           "(select strManagementName from " & DBNameSpace & ".LookUpAPBManagementType " & _
           "where strKey = '5' and strCurrentContact = '1' ), " & _
           "'01', " & _
           "(SELECT " & _
           "CASE " & _
           "WHEN strDistrictResponsible <> 'False' AND strDistrictResponsible IS NOT NULL " & _
           "THEN '0' ELSE '0' " & _
           "END ManagerResponsible " & _
           "from " & DBNameSpace & ".LookUPDistricts, " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION,  " & _
           "" & DBNameSpace & ".SSCPDistrictResponsible,     " & _
           "(select " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.STRASSIGNINGMANAGER, " & _
            "" & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.strAIRSNumber " & _
            "from " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED, " & _
            "(select max(INTYEAR) as MAXYEAR, STRAIRSNUMBER " & _
            "from " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED " & _
            "group by STRAIRSNUMBER) MAXRESULTS " & _
            "where " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER = MAXRESULTS.STRAIRSNUMBER " & _
            "and " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.INTYEAR = MAXRESULTS.MAXYEAR) Table1 " & _
           "WHERE " & DBNameSpace & ".LOOKUPDISTRICTINFORMATION.strDistrictCode = " & DBNameSpace & ".LookUPDistricts.strDistrictCode (+) " & _
           "AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = Table1.strAIRSnumber (+) " & _
           "AND SUBSTR(" & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber, 5, 3) = strDistrictCounty (+) " & _
           "AND " & DBNameSpace & ".SSCPDistrictResponsible.strAIRSNumber = '0413" & cboAIRSNumber.Text & "'), " & _
           "'" & UserGCode & "', '" & OracleDate & "', " & _
           "'N/A', '', '')"

                End If

                Try

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Catch ex As Exception
                    ErrorReport(ex.ToString(), "ISMPFacilityAndTestReportInfo.Save1")
                End Try

                Try

                    cmd = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Catch ex As Exception
                    ErrorReport(ex.ToString(), "ISMPFacilityAndTestReportInfo.Save2")
                End Try

                bgw1.WorkerReportsProgress = True
                bgw1.WorkerSupportsCancellation = True
                bgw1.RunWorkerAsync()

                If rdbCloseReport.Checked = True Then
                    TBFacilityInfo.Buttons.Item(0).Enabled = False
                End If
                Find(txtReferenceNumber.Text)
                MsgBox("Done", MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
            Else
                MsgBox("You must Provide a Reference Number with a length of 9 characters or less.", _
                         MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
            End If

        Catch ex As Exception
            ErrorReport(txtReferenceNumber.Text & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub CloseTestReport()
        Try
            Dim AFSActionNumber As String = ""
            Dim UpdateCode As String
            Dim ComplianceStatus As String
            Dim AIRSNumber As String = ""

            If rdbCloseReport.Checked = True Then
                If btnSearchForAIRS.Visible = True Then
                    If cboAIRSNumber.Text <> "" And cboAIRSNumber.Text.Length = 8 Then
                        AIRSNumber = cboAIRSNumber.Text
                    End If
                Else
                    AIRSNumber = cboAIRSNumber.SelectedValue
                End If

                For Each RefNum As String In Me.clbReferenceNumbers.CheckedItems
                    RefNum = Mid(RefNum, 1, (RefNum.IndexOf(" -")))
                    If RefNum.Length > 9 Then
                        MsgBox("There was an issue with the Reference Number being more than 9 characters in length." & vbCrLf & _
                               "Please note the Reference Numbers being closed out and contact the Data Management Unit with those numbers.", _
                                MsgBoxStyle.Exclamation, "Test Report Administration")
                        Exit Sub
                    End If
                    SQL = "Select strComplianceStatus " & _
                    "from " & DBNameSpace & ".ISMPReportInformation " & _
                    "where strReferenceNumber = '" & RefNum & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        ComplianceStatus = dr.Item("strComplianceStatus")
                    Else
                        ComplianceStatus = "00"
                    End If
                    dr.Close()
                    Select Case ComplianceStatus
                        Case "00"
                            SQL = ""
                        Case "01"
                            SQL = ""
                        Case Else
                            SQL = "Select strUpdateStatus " & _
                            "from " & DBNameSpace & ".AFSISMPRecords " & _
                            "where strReferenceNumber = '" & RefNum & "' "
                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            recExist = dr.Read
                            If recExist = True Then
                                UpdateCode = dr.Item("strupdateStatus")
                            Else
                                UpdateCode = ""
                            End If
                            dr.Close()
                            Select Case UpdateCode
                                Case "A"
                                    'Leave it alone
                                Case "C"
                                    'Leave it alone
                                Case "N"
                                    SQL = "Update " & DBNameSpace & ".AFSISMPRecords set " & _
                                    "strUpDateStatus = 'C' " & _
                                    "where strReferenceNumber = '" & RefNum & "' "
                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    dr.Close()
                                Case ""
                                    SQL = "Select strAFSActionNumber " & _
                                    "from " & DBNameSpace & ".APBSupplamentalData " & _
                                    "where strAIRSNumber = '0413" & AIRSNumber & "' "

                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    While dr.Read
                                        AFSActionNumber = dr.Item("strAFSActionNumber")
                                    End While
                                    dr.Close()

                                    'This was removed when AFS went to 5 digits. 

                                    'Select Case AFSActionNumber.Length
                                    '    Case 0
                                    '        AFSActionNumber = "1"
                                    '    Case 1
                                    '        AFSActionNumber = "00" & AFSActionNumber
                                    '    Case 2
                                    '        AFSActionNumber = "0" & AFSActionNumber
                                    '    Case 3
                                    '        AFSActionNumber = AFSActionNumber
                                    '    Case Else
                                    '        AFSActionNumber = AFSActionNumber
                                    'End Select

                                    SQL = "Insert into " & DBNameSpace & ".AFSISMPRecords " & _
                                    "(strReferenceNumber, strAFSActionNumber, " & _
                                    "strUpDateStatus, strModifingPerson, " & _
                                    "datModifingDate) " & _
                                    "values " & _
                                    "('" & RefNum & "', '" & AFSActionNumber & "', " & _
                                    "'A', '" & UserGCode & "', " & _
                                    "'" & OracleDate & "') "

                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    dr.Close()

                                    AFSActionNumber = CInt(AFSActionNumber) + 1

                                    'This was removed when AFS went to 5 digits. 
                                    'Select Case AFSActionNumber.Length
                                    '    Case 0
                                    '        AFSActionNumber = "001"
                                    '    Case 1
                                    '        AFSActionNumber = "00" & AFSActionNumber
                                    '    Case 2
                                    '        AFSActionNumber = "0" & AFSActionNumber
                                    '    Case 3
                                    '        AFSActionNumber = AFSActionNumber
                                    '    Case Else
                                    '        AFSActionNumber = AFSActionNumber
                                    'End Select

                                    SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " & _
                                    "strAFSActionNumber = '" & AFSActionNumber & "' " & _
                                    "where strAIRSNumber = '0413" & AIRSNumber & "' "

                                    cmd = New OracleCommand(SQL, Conn)
                                    If Conn.State = ConnectionState.Closed Then
                                        Conn.Open()
                                    End If
                                    dr = cmd.ExecuteReader
                                    dr.Close()

                                Case Else
                                    'Leave it alone
                            End Select
                    End Select
                    Select Case ComplianceStatus
                        Case "00"
                            MsgBox("Reference Number " & RefNum.ToString & " does not exist in the system.", _
                                   MsgBoxStyle.Exclamation, "ISMP Test Report Information")
                            Exit Sub
                        Case "01"
                            MsgBox("Test Report " & RefNum.ToString & " is marked as 'File Open' in the Compliance Status." & vbCrLf & _
                            "Have the engineer correct this status before trying to close out this Test Report.", _
                              MsgBoxStyle.Exclamation, "ISMP Test Report Information")
                            Exit Sub
                        Case Else
                            SQL = "Update " & DBNameSpace & ".ISMPReportInformation set " & _
                            "strClosed = 'True', " & _
                            "datCompleteDate = '" & DTPDateClosed.Text & "' " & _
                            "where strReferenceNumber = '" & RefNum.ToString & "' "
                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()
                    End Select
                    StartComplianceWork(RefNum)
                Next
            End If


            bgw1.WorkerReportsProgress = True
            bgw1.WorkerSupportsCancellation = True
            bgw1.RunWorkerAsync()

            clbReferenceNumbers.Items.Clear()
            FillTestReportList(txtReferenceNumber.Text)

            MsgBox("Done", MsgBoxStyle.Exclamation, "ISMP Test Report Information")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try



    End Sub
    Private Sub Find(ByVal RefNum As String)
        Dim SQL As String
        Dim AirList As String = ""
        Dim AirProgramCodes As String = ""
        Dim temp As String = ""

        Try


            If txtReferenceNumber.Text <> "" Then
                temp = txtReferenceNumber.Text

                SQL = "Select strAIRSNumber from " & DBNameSpace & ".ISMPMaster where strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                Dim cmd As New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                Dim dr As OracleDataReader = cmd.ExecuteReader

                Dim recExist As Boolean = dr.Read

                If recExist Then
                    cboAIRSNumber.Text = Mid(dr.Item("strAirsnumber"), 5)
                    dr.Close()

                    LoadAddress()


                    SQL = "Select  " & _
              "" & DBNameSpace & ".ISMPReportInformation.strReferenceNumber, " & _
              "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
              "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
              "to_char(datTestDateEnd, 'dd-Mon-yyyy') as forDatTestDateEnd, " & _
              "to_char(datReviewedByUnitmanager, 'dd-Mon-yyyy') as forDatReviewedByUnitManager, " & _
              "to_char(datCompleteDate, 'dd-Mon-yyyy') as forDateComplete, " & _
              "strClosed, " & _
              "" & DBNameSpace & ".ISMpReportType.strReportType, " & _
              "(select (strLastName||', '||strFirstName) as ReviewingEngineer " & _
               "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".ISMPReportInformation " & _
               "where " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".ISMPReportInformation.strReviewingEngineer " & _
               "and " & DBNameSpace & ".ISMPReportInformation.strReferencenumber = '" & txtReferenceNumber.Text & "') as ReviewingENgineer,  " & _
              "(strLastName||', '||strFirstName) as UnitManager, " & _
               "strEmissionSource, " & _
               "" & DBNameSpace & ".LookUpTestingFirms.strTestingFirm, " & _
               "" & DBNameSpace & ".LookUpPollutants.strPollutantDescription,  " & _
               "" & DBNameSpace & ".LookUpEPDUnits.strUnitDesc, " & _
               "" & DBNameSpace & ".ISMPDocumentType.strDocumentType, " & _
               "" & DBNameSpace & ".LookUpISMPComplianceStatus.strComplianceStatus " & _
            "from " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".ISMPReportInformation, " & _
              "" & DBNameSpace & ".ISMPReportType, " & DBNameSpace & ".LookUpTestingFirms, " & _
              "" & DBNameSpace & ".LookUpPollutants, " & DBNameSpace & ".LookUpEPDUnits, " & _
              "" & DBNameSpace & ".EPDUSerPRofiles, " & DBNameSpace & ".ISMPDocumentType,  " & _
              "" & DBNameSpace & ".LookUpISMpComplianceStatus " & _
            "where " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
              "and " & DBNameSpace & ".ISMPReportInformation.strReportType = " & DBNameSpace & ".ISMpReportType.strKey " & _
              "and " & DBNameSpace & ".ISMPREportINformation.strTestingFirm = " & DBNameSpace & ".LookUpTestingFirms.strTestingFirmKey " & _
              "and " & DBNameSpace & ".ISMPReportInformation.strPollutant = " & DBNameSpace & ".LookUpPollutants.strPollutantCode " & _
              "and " & DBNameSpace & ".ISMPREportInformation.numReviewingManager = " & DBNameSpace & ".EPDUserProfiles.numUserID (+) " & _
              "and " & DBNameSpace & ".EPDUserPRofiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
              "and " & DBNameSpace & ".ISMPReportInformation.strDocumentTYpe = " & DBNameSpace & ".ISMPDocumentType.strKEy " & _
              "and " & DBNameSpace & ".ISMPReportINformation.strComplianceStatus = " & DBNameSpace & ".LookUpISMPComplianceStatus.strComplianceKey " & _
              "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader

                    While dr.Read
                        DTPDateReceived.Text = dr.Item("forDatReceivedDate")
                        txtEmissionSource.Text = dr.Item("strEmissionSource")
                        cboTestingFirms.Text = dr.Item("strTestingFirm")
                        cboPollutant.Text = dr.Item("strPollutantDescription")
                        DTPTestDateStart.Text = dr.Item("forDatTestDateStart")
                        DTPTestDateEnd.Text = dr.Item("fordatTestDateEnd")
                        DTPTestDateStart.Text = dr.Item("forDatTestDateStart")
                        DTPTestDateEnd.Text = dr.Item("forDatTestDateEnd")
                        If dr.Item("forDateComplete") = "04-Jul-1776" Then
                            txtDaysInAPB.Text = DateDiff(DateInterval.Day, CDate(dr.Item("forDatReceivedDate")), CDate(OracleDate))
                        Else
                            txtDaysInAPB.Text = DateDiff(DateInterval.Day, CDate(dr.Item("forDatReceivedDate")), CDate(dr.Item("forDateComplete")))
                        End If
                        If dr.Item("strClosed") = "True" Then
                            rdbOpenReport.Checked = False
                            rdbCloseReport.Checked = True
                            TBFacilityInfo.Buttons.Item(0).Enabled = False
                        Else
                            rdbOpenReport.Checked = True
                            rdbCloseReport.Checked = False
                            TBFacilityInfo.Buttons.Item(0).Enabled = True
                        End If

                        If dr.Item("forDateComplete") <> "04-Jul-1776" Then
                            DTPDateClosed.Text = dr.Item("forDateComplete")
                        Else
                            DTPDateClosed.Text = OracleDate
                        End If
                    End While
                    dr.Close()

                    FillTestReportList(temp)
                Else

                End If

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub Print()
        Try

            PrintOut = Nothing
            If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
            PrintOut.txtPrintType.Text = "ISMPAIRSForm"
            PrintOut.txtReferenceNumber.Text = Me.txtReferenceNumber.Text
            PrintOut.Show()
            PrintOut.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub Clear()
        Try

            txtReferenceNumber.Clear()

            If btnSearchForAIRS.Visible = True Then
                cboAIRSNumber.Text = ""
                cboFacilityName.Text = ""
            Else
                If cboFacilityName.SelectedIndex > -1 Then
                    cboFacilityName.SelectedIndex = 0
                End If
                If cboAIRSNumber.SelectedIndex > -1 Then
                    cboAIRSNumber.SelectedIndex = 0
                End If
            End If

            txtFacilityAddress.Clear()
            txtFacilityCity.Clear()
            txtFacilityState.Clear()
            txtFacilityZipCode.Clear()
            DTPDateReceived.Value = OracleDate
            DTPTestDateStart.Value = OracleDate
            DTPTestDateEnd.Value = OracleDate
            txtEmissionSource.Clear()
            cboTestingFirms.Text = ""
            cboPollutant.Text = ""
            txtDaysInAPB.Clear()
            rdbOpenReport.Checked = True
            rdbCloseReport.Checked = False
            DTPDateClosed.Text = OracleDate
            TBFacilityInfo.Buttons.Item(0).Enabled = True
            If cboTestingFirms.SelectedIndex > -1 Then
                cboTestingFirms.SelectedIndex = 0
            End If
            If cboPollutant.SelectedIndex > -1 Then
                cboPollutant.SelectedIndex = 0
            End If
            clbReferenceNumbers.Items.Clear()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub Delete()
        Dim temp As String = ""
        Dim SQL As String = ""

        Try

            If txtReferenceNumber.Text <> "" Then
                temp = InputBox("If you want to Delete this record, type the following word exactly as you see it." + vbCrLf + "Delete", "Facility Information Delete")

                If temp = "Delete" Then

                    SQL = "Select " & DBNameSpace & ".ISMPDocumentType.strTableName " & _
                    "from " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPReportInformation " & _
                    "where " & DBNameSpace & ".ISMPReportInformation.strDocumentType = " & DBNameSpace & ".ISMPDocumentType.strKEy " & _
                    "and strReferenceNumber = '" & txtReferenceNumber.Text & "'"

                    Dim cmd As New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If

                    Dim dr As OracleDataReader = cmd.ExecuteReader

                    Dim recExist As Boolean = dr.Read
                    If recExist Then
                        temp = dr.Item("strTableName")
                    End If
                    If Conn.State = ConnectionState.Open Then
                        'conn.close()
                    End If
                    If temp <> "DeLeTe" Then
                        SQL = "Delete " & temp & " where strREferenceNumber = '" & txtReferenceNumber.Text & "'"
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        If Conn.State = ConnectionState.Open Then
                            'conn.close()
                        End If
                        SQL = "Update " & DBNameSpace & ".ISMPReportInformation set " & _
                        "strDelete = 'DELETE' where strREferenceNumber = '" & txtReferenceNumber.Text & "'"

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        Try

                            dr = cmd.ExecuteReader
                        Catch ex As Exception
                            ErrorReport(ex.ToString(), "ISMPFacilityAndTestReportInfo.Delete2")
                        End Try


                        If Conn.State = ConnectionState.Open Then
                            'conn.close()
                        End If

                        bgw1.WorkerReportsProgress = True
                        bgw1.WorkerSupportsCancellation = True
                        bgw1.RunWorkerAsync()

                        MsgBox("Deleted", MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(txtReferenceNumber.Text & vbCrLf & SQL & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub MoveOn()
        Dim SQL As String

        Try

            If txtReferenceNumber.Text <> "" Then
                SQL = "select " & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                 "from " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPReportInformation " & _
                 "where " & DBNameSpace & ".ISMPReportInformation.strDocumentType = " & DBNameSpace & ".ISMPDocumentType.strKey and " & _
                 "strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                Dim cmd As New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                Dim dr As OracleDataReader = cmd.ExecuteReader
                Dim recExist As Boolean = dr.Read
                If recExist = True Then
                    ISMPTestReportsEntry = Nothing
                    If ISMPTestReportsEntry Is Nothing Then ISMPTestReportsEntry = New ISMPTestReports
                    ISMPTestReportsEntry.txtReferenceNumber.Text = txtReferenceNumber.Text
                    ISMPTestReportsEntry.Show()
                    Me.Hide()
                    ISMPTestReportsEntry.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub OpenMemo()
        Try

            If txtReferenceNumber.Text <> "" Then
                ISMPMemoEdit = Nothing
                If ISMPMemoEdit Is Nothing Then ISMPMemoEdit = New ISMPMemo
                ISMPMemoEdit.txtReferenceNumber.Text = Me.txtReferenceNumber.Text
                ISMPMemoEdit.Show()
                ISMPMemoEdit.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub DeleteTestReport()
        Try



            For Each RefNum As String In Me.clbReferenceNumbers.CheckedItems
                RefNum = Mid(RefNum, 1, (RefNum.IndexOf(" -")))

                SQL = "Select strReferenceNumber " & _
                "from " & DBNameSpace & ".ISMPReportInformation " & _
                "where strReferenceNumber = '" & RefNum & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".ISMPReportInformation set " & _
                    "strDelete = 'DELETE' " & _
                    "where strReferenceNumber = '" & RefNum & "'  "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

            Next

            bgw1.WorkerReportsProgress = True
            bgw1.WorkerSupportsCancellation = True
            bgw1.RunWorkerAsync()

            MsgBox("Delete Done", MsgBoxStyle.Information, "ISMP Test Report Administration")

            Clear()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub StartComplianceWork(ByVal RefNum As String)
        Try

            Dim StaffResponsible As String = ""
            Dim TrackingNumber As String = ""
            Dim TestReportDue As String = ""

            If cboAIRSNumber.Text <> "" Then
                SQL = "select " & _
                "strTrackingNumber " & _
                "from " & DBNameSpace & ".SSCPTestReports " & _
                "where strReferenceNumber = '" & txtReferenceNumber.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    Exit Sub
                End If

                'SQL = "select strSSCPEngineer " & _
                '"from " & DBNameSpace & ".SSCPFacilityAssignment " & _
                '"where strAIRSNumber = '0413" & cboAIRSNumber.Text & "' "

                SQL = "Select " & _
                "numSSCPEngineer " & _
                "from " & DBNameSpace & ".SSCPInspectionsRequired, " & _
                "(select max(intyear) as MaxYear, " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER  " & _
                "from " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED " & _
                "where " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER = '0413" & cboAIRSNumber.Text & "' " & _
                "group by " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER ) MaxResults " & _
                "where " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.strAIRSNumber = '0413" & cboAIRSNumber.Text & "' " & _
                "and " & DBNameSpace & ".SSCPINSPECTIONSREQUIRED.intyear = maxresults.maxyear " & _
                "group by numSSCPEngineer "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    StaffResponsible = dr.Item("numSSCPEngineer")
                Else
                    StaffResponsible = "0"
                End If
                dr.Close()

                SQL = "select datSSCPTestReportDue " & _
                "from " & DBNameSpace & ".APBSupplamentalData " & _
                "where strAIRSNumber = '0413" & cboAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("datSSCPTestReportDue")) Then
                        TestReportDue = DTPDateClosed.Text
                    Else
                        TestReportDue = Format(dr.Item("datSSCPTestReportDue"), "dd-MMM-yyyy")
                    End If
                Else
                    TestReportDue = DTPDateClosed.Text
                End If
                dr.Close()

                SQL = "Insert into " & DBNameSpace & ".SSCPItemMaster " & _
                "(strTrackingNumber, strAIRSNumber, " & _
                "datReceivedDate, strEventType, " & _
                "strResponsibleStaff, datCompleteDate, " & _
                "strModifingPerson, datModifingDate) " & _
                "values " & _
                "(" & DBNameSpace & ".SSCPTrackingNumber.nextval, '0413" & cboAIRSNumber.Text & "', " & _
                "'" & DTPDateClosed.Text & "', '03', " & _
                "'" & StaffResponsible & "', '', " & _
                "'" & UserGCode & "', '" & OracleDate & "')"

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Select " & DBNameSpace & ".SSCPTrackingNumber.currval from dual "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    TrackingNumber = dr.Item(0)
                End While
                dr.Close()

                SQL = "Insert into " & DBNameSpace & ".SSCPTestReports " & _
                "(strTrackingNumber, strReferenceNumber, " & _
                "datTestReportDue, " & _
                "strTestReportComments, strTestReportFollowUp, " & _
                "strModifingPerson, datModifingDate) " & _
                "Values " & _
                "('" & TrackingNumber & "', '" & RefNum & "', " & _
                "'" & TestReportDue & "', " & _
                "' ', 'False', " & _
                "'" & UserGCode & "', '" & OracleDate & "') "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#Region "Main Menu"
    Private Sub MmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiSave.Click
        Try

            Save()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiReferenceNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiReferenceNumber.Click
        Try

            GetNextReferenceNumber()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiExit.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiCut.Click
        Try

            SendKeys.Send("^(x)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiCopy.Click
        Try

            SendKeys.Send("^(c)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiPaste.Click
        Try

            SendKeys.Send("^(v)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiClear.Click
        Try

            Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiDelete.Click
        Try

            Delete()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiShowDeletedRecords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiShowDeletedRecords.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber,  " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                  "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                  "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                  "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                  "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                  "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                  "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                  "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                  "and strDelete = 'DELETE' " & _
                  "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiViewByTestType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiViewByTestType.Click
        Try

            MsgBox("Select a Test Report Type from the drop down list.", MsgBoxStyle.MsgBoxHelp, "View by Test Report Type.")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#Region "View by Test Reports"
    Private Sub MmiUnassigned_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiUnassigned.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber,  " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
            "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
            "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
            "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
            "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
            "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
            "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
            "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
            "and strDelete is NULL " & _
             "and strClosed = 'False' " & _
            "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'Unassigned' " & _
            "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiOneStackTwoRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiOneStackTwoRun.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                 "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                 "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                 "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                 "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                 "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                 "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                 "and strDelete is NULL " & _
                  "and strClosed = 'False' " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'One Stack (Two Runs)' " & _
                 "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiOneStackThreeRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiOneStackThreeRun.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                "and strDelete is NULL " & _
                 "and strClosed = 'False' " & _
                "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'One Stack (Three Runs)' " & _
                "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiOneStackFourRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiOneStackFourRun.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                 "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                 "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                 "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                 "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                 "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                 "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                 "and strDelete is NULL " & _
                  "and strClosed = 'False' " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'One Stack (Four Runs)' " & _
                 "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiTwoStackStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiTwoStackStandard.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                "and strDelete is NULL " & _
                 "and strClosed = 'False' " & _
                "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'Two Stack (Standard)' " & _
                "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiTwoStackDRE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiTwoStackDRE.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                 "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                 "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                 "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                 "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                 "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                 "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                 "and strDelete is NULL " & _
                  "and strClosed = 'False' " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'Two Stack (DRE)' " & _
                 "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiLoadingRack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiLoadingRack.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
               "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
               "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
               "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
               "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
               "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
               "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
               "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
               "and strDelete is NULL " & _
                "and strClosed = 'False' " & _
               "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'Loading Rack' " & _
               "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiFlare_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiFlare.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber,  " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                 "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                 "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                 "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                 "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                 "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                 "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                 "and strDelete is NULL " & _
                  "and strClosed = 'False' " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'Flare' " & _
                 "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiPondTreatment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiPondTreatment.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                 "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                 "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                 "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                 "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                 "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                 "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                 "and strDelete is NULL " & _
                  "and strClosed = 'False' " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'Pond Treatment' " & _
                 "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiGasConcentration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiGasConcentration.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber,  " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                 "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                 "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                 "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                 "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                 "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                 "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                 "and strDelete is NULL " & _
                  "and strClosed = 'False' " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'Gas Concentration' " & _
                 "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiRata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiRata.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                 "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                 "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                 "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                 "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                 "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                 "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                 "and strDelete is NULL " & _
                  "and strClosed = 'False' " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'Rata' " & _
                 "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiPEMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiPEMS.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber,  " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                "and strDelete is NULL " & _
                 "and strClosed = 'False' " & _
                "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'PEMS' " & _
                "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiMemoStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiMemoStandard.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                "and strDelete is NULL " & _
                 "and strClosed = 'False' " & _
                "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'Memorandum (Standard)' " & _
                "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiMemoToFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiMemoToFile.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber,  " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                "and strDelete is NULL " & _
                 "and strClosed = 'False' " & _
                "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'Memorandum (To File)' " & _
                "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiMemoPTE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiMemoPTE.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber,  " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                  "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                  "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                  "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                  "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                  "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                  "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                  "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                  "and strDelete is NULL " & _
                   "and strClosed = 'False' " & _
                  "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'PTE (Perminate Total Enclosure)' " & _
                  "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiMethod9Single_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiMethod9Single.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber,  " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                   "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                   "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                   "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                   "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                   "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                   "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                   "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                   "and strDelete is NULL " & _
                    "and strClosed = 'False' " & _
                   "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'Method9 (Single)' " & _
                   "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiMethod9Multi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiMethod9Multi.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                 "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                 "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                 "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                 "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                 "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                 "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                 "and strDelete is NULL " & _
                  "and strClosed = 'False' " & _
                 "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'Method 9 (Multi.)' " & _
                 "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiMethod22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiMethod22.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber,  " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                "and strDelete is NULL " & _
                 "and strClosed = 'False' " & _
                "and " & DBNameSpace & ".ISMPDocumentType.strDocumentType = 'Method 22' " & _
                "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiAllTestReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiAllTestReports.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
                  "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
                  "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
                  "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                  "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
                  "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
                  "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
                  "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                  "and strDelete is NULL " & _
                   "and strClosed = 'False' " & _
                  "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiOpenRecords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiOpenRecords.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber,  " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
               "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
               "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
               "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
               "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
               "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
               "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
               "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
               "and strDelete is NULL " & _
               "and strClosed = 'False' " & _
               "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiClosedRecords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiClosedRecords.Click
        Dim SQL As String

        Try

            SQL = "select " & DBNameSpace & ".ISMPMaster.strReferenceNumber, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as forDatReceivedDate, " & _
            "to_char(datTestDateStart, 'dd-Mon-yyyy') as forDatTestDateStart, " & _
            "substr(" & DBNameSpace & ".ISMPMaster.strAirsnumber, 5) as strAIRSNumber, strFacilityName, " & _
            "" & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
            "from " & DBNameSpace & ".ISMPReportInformation, " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPMaster, " & DBNameSpace & ".APBFacilityInformation " & _
            "where " & DBNameSpace & ".APBFacilityInformation.strairsnumber = " & DBNameSpace & ".ISMPMaster.strairsnumber " & _
            "and " & DBNameSpace & ".ISMPDocumentType.strKey = " & DBNameSpace & ".ISMPReportInformation.strDocumentType " & _
            "and " & DBNameSpace & ".ISMPMaster.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
            "and strDelete is NULL " & _
            "and strClosed = 'True' " & _
            "order by " & DBNameSpace & ".ISMPMaster.strReferenceNumber"

            dsGrid = New DataSet

            daGrid = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            daGrid.Fill(dsGrid, "Grid")
            dgvFacilityInfo.DataSource = dsGrid
            dgvFacilityInfo.DataMember = "Grid"

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region
    Private Sub MmiViewByFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiViewByFacility.Click
        Try

            If FacilityLookUpTool Is Nothing Then
                If FacilityLookUpTool Is Nothing Then FacilityLookUpTool = New IAIPFacilityLookUpTool
                FacilityLookUpTool.Show()
            Else
                FacilityLookUpTool.Dispose()
                FacilityLookUpTool = New IAIPFacilityLookUpTool
                If FacilityLookUpTool Is Nothing Then FacilityLookUpTool = New IAIPFacilityLookUpTool
                FacilityLookUpTool.Show()
                FacilityLookUpTool.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Public WriteOnly Property ValueFromFacilityLookUp() As String
        Set(ByVal Value As String)
            cboAIRSNumber.Text = Value
        End Set
    End Property
    Private Sub MmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiHelp.Click
        Try

            Help.ShowHelp(Label10, HelpUrl)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub MmiShowToolbar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiShowToolbar.Click
        Try

            If TBFacilityInfo.Visible = True Then
                TBFacilityInfo.Visible = False
                MmiShowToolbar.Checked = True
            Else
                TBFacilityInfo.Visible = True
                MmiShowToolbar.Checked = False
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region


#End Region

#Region "Delcarations"
    Private Sub DevTestReportAdministrative_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            If NavigationScreen Is Nothing Then
                NavigationScreen = New IAIPNavigation
            End If
            NavigationScreen.Show()

            ISMPFacility = Nothing

            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub TBFacilityInfo_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBFacilityInfo.ButtonClick
        Try

            Dim temp As String

            Select Case TBFacilityInfo.Buttons.IndexOf(e.Button)
                Case 0
                    Save()
                Case 1
                    temp = InputBox("Enter the Reference Number you are searching for.", "Reference Number Search")
                    If temp <> "" Then
                        Clear()
                        txtReferenceNumber.Text = temp
                        If cboAIRSNumber.Text = "" Then
                            txtReferenceNumber.Text = ""
                            MsgBox("Reference Number does not exist in Database", MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
                        End If
                    End If
                Case 2
                    MoveOn()
                Case 3
                    OpenMemo()
                    'Case 4
                    'Print()
                Case 4
                    Clear()
                Case 5
                    DeleteTestReport()
                Case 6
                    Me.Close()
                Case 7
                    Me.Close()
                Case Else

            End Select

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Private Sub dgvFacilityInfo_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvFacilityInfo.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvFacilityInfo.HitTest(e.X, e.Y)

        Try


            If dgvFacilityInfo.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvFacilityInfo.Columns(0).HeaderText = "Reference #" Then
                    txtReferenceNumber.Text = dgvFacilityInfo(0, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub txtReferenceNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReferenceNumber.TextChanged
        Try

            If txtReferenceNumber.Text <> "" Then
                Find(txtReferenceNumber.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub cmiPrintAFSForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmiPrintAFSForm.Click
        Try

            PrintOut = Nothing
            If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
            PrintOut.txtPrintType.Text = "ISMPAIRSForm"
            PrintOut.txtReferenceNumber.Text = Me.txtReferenceNumber.Text
            PrintOut.Show()
            PrintOut.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub cmiPrintTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmiPrintTestReport.Click
        Try

            PrintOut = Nothing
            If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
            PrintOut.txtPrintType.Text = "ISMPTestReport"
            PrintOut.txtReferenceNumber.Text = Me.txtReferenceNumber.Text
            PrintOut.Show()
            PrintOut.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub LLFacilityName_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LLFacilityName.LinkClicked
        Try

            If FacilityLookUpTool Is Nothing Then
                If FacilityLookUpTool Is Nothing Then FacilityLookUpTool = New IAIPFacilityLookUpTool
                FacilityLookUpTool.Show()
            Else
                FacilityLookUpTool.Dispose()
                FacilityLookUpTool = New IAIPFacilityLookUpTool
                If FacilityLookUpTool Is Nothing Then FacilityLookUpTool = New IAIPFacilityLookUpTool
                FacilityLookUpTool.Show()
            End If
            FacilityLookUpTool.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub rdbCloseReport_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbCloseReport.CheckedChanged
        Try

            If rdbCloseReport.Checked = False And TBFacilityInfo.Buttons.Item(0).Enabled = False Then
                TBFacilityInfo.Buttons.Item(0).Enabled = True
            End If
            If rdbCloseReport.Checked = True Then
                DTPDateClosed.Enabled = True
                btnCloseTestReport.Enabled = True

                If DTPDateClosed.Value = "09-Sep-9998" Or DTPDateClosed.Value = "04-Jul-1776" Then
                    DTPDateClosed.Value = Date.Today
                End If
            Else
                DTPDateClosed.Enabled = False
                btnCloseTestReport.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            'If conn.State = ConnectionState.Open Then
            '    'conn.close()
            'End If
        End Try

    End Sub
    Private Sub mmiMemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiMemo.Click
        Try

            OpenMemo()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub chbOverright_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbOverright.CheckedChanged
        Try

            If chbOverright.Checked = True Then
                txtReferenceNumber.ReadOnly = False
            Else
                txtReferenceNumber.ReadOnly = True
                txtReferenceNumber.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiAddTestingFirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiAddTestingFirm.Click
        Try

            ISMPAddTestingFirm = Nothing
            If ISMPAddTestingFirm Is Nothing Then ISMPAddTestingFirm = New ISMPAddTestingFirms
            ISMPAddTestingFirm.Show()
            ISMPAddTestingFirm.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiAddPollutant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiAddPollutant.Click
        Try

            ISMPAddPollutant = Nothing
            If ISMPAddPollutant Is Nothing Then ISMPAddPollutant = New ISMPAddPollutants
            ISMPAddPollutant.Show()
            ISMPAddPollutant.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub mmiRefreshLists_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiRefreshLists.Click
        Try

            dsPollutant = New DataSet
            cboPollutant.DataSource = dsPollutant
            dsTestingFirms = New DataSet
            cboTestingFirms.DataSource = dsTestingFirms
            FillPollutantandTestingFirms()
            FillPollutantCombo()
            FillTestingFirmsCombo()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Try

            Save()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnDeleteTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteTestReport.Click
        Try

            DeleteTestReport()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnClearReferenceNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearReferenceNumber.Click
        Try
            txtReferenceNumber.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub btnCloseTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseTestReport.Click
        Try

            CloseTestReport()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region

    Private Sub btnAddTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTestReport.Click
        Try
            Dim RefNum As String
            Dim AIRSNumber As String
            Dim Commissioner As String
            Dim Director As String
            Dim ProgramManager As String
            Dim DateReceived As String
            Dim DateCompleted As String

            If txtAddTestReportRefNum.Text <> "" Then
                txtAddTestReportRefNum.BackColor = Color.White
                RefNum = txtAddTestReportRefNum.Text
                If mtbAddTestReportAIRSNumber.Text <> "" And Len(mtbAddTestReportAIRSNumber.Text) = 8 Then
                    AIRSNumber = mtbAddTestReportAIRSNumber.Text
                    mtbAddTestReportAIRSNumber.BackColor = Color.White
                Else
                    mtbAddTestReportAIRSNumber.BackColor = Color.Tomato
                    MsgBox("Please add a valid AIRS Number.", MsgBoxStyle.Information, "Add Test Report")
                    Exit Sub
                End If
                If txtAddTestReportCommissioner.Text <> "" Then
                    Commissioner = txtAddTestReportCommissioner.Text
                    txtAddTestReportCommissioner.BackColor = Color.White
                Else
                    txtAddTestReportCommissioner.BackColor = Color.Tomato
                    MsgBox("Please add a valid Commissioenr.", MsgBoxStyle.Information, "Add Test Report")
                    Exit Sub
                End If
                If txtAddTestReportDirector.Text <> "" Then
                    Director = txtAddTestReportDirector.Text
                    txtAddTestReportDirector.BackColor = Color.White
                Else
                    txtAddTestReportDirector.BackColor = Color.Tomato
                    MsgBox("Please add a valid Director.", MsgBoxStyle.Information, "Add Test Report")
                    Exit Sub
                End If
                If txtAddTestReportProgramManager.Text <> "" Then
                    ProgramManager = txtAddTestReportProgramManager.Text
                    txtAddTestReportProgramManager.BackColor = Color.White
                Else
                    txtAddTestReportProgramManager.BackColor = Color.Tomato
                    MsgBox("Please add a valid Program Manager.", MsgBoxStyle.Information, "Add Test Report")
                    Exit Sub
                End If
                DateReceived = dtpAddTestReportDateReceived.Text
                DateCompleted = DTPAddTestReportDateCompleted.Text

                SQL = "Select " & _
                "strReferenceNumber " & _
                "from " & DBNameSpace & ".ISMPMaster " & _
                "where strReferenceNumber = '" & RefNum & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    MsgBox("This Refernece Number already exists in the system.", MsgBoxStyle.Information, "Add Test Report")
                    Exit Sub
                End If

                SQL = "Select " & _
                "strAIRSNumber " & _
                "from " & DBNameSpace & ".APBMasterAIRS " & _
                "where strAIRSNumber = '0413" & AIRSNumber & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    MsgBox("This AIRS Number does not exist in the system.", MsgBoxStyle.Information, "Add Test Report")
                    Exit Sub
                End If

                SQL = "Insert into " & DBNameSpace & ".ISMPMaster " & _
                "values " & _
                "('" & RefNum & "', '0413" & AIRSNumber & "', " & _
                "'" & UserGCode & "', '" & OracleDate & "') "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Insert into " & DBNameSpace & ".ISMPReportInformation " & _
                "values " & _
                "('" & RefNum & "', '00001', " & _
                "'N/A', '001', " & _
                "'001', 'N/A', " & _
                "'00001', '329', " & _
                "'0', '0', " & _
                "'12', '" & DateReceived & "', " & _
                "'0', '04-Jul-1776', " & _
                "'04-Jul-1776', '" & DateReceived & "', " & _
                "'" & DateCompleted & "', " & _
                "'Historical Test report added to IAIP, assigned to Richard Taylor - Chemical & VOC Source Monitoring Unit', " & _
                "'False', '" & Replace(Commissioner, "'", "''") & "', " & _
                "'" & Replace(Director, "'", "''") & "', '" & Replace(ProgramManager, "'", "''") & "', " & _
                "'01', '0', " & _
                "'" & UserGCode & "', '" & OracleDate & "', " & _
                "'N/A', '', " & _
                "'', '', " & _
                "'', '', '') "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                bgw1.WorkerReportsProgress = True
                bgw1.WorkerSupportsCancellation = True
                bgw1.RunWorkerAsync()

                MsgBox("Record Added.", MsgBoxStyle.Information, "Add Test Report")

            Else
                txtAddTestReportRefNum.BackColor = Color.Tomato
                MsgBox("Please add a valid Reference Number.", MsgBoxStyle.Information, "Add Test Report")
                Exit Sub
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearAddTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAddTestReport.Click
        Try
            txtAddTestReportRefNum.Clear()
            txtAddTestReportRefNum.BackColor = Color.White
            txtAddTestReportCommissioner.Clear()
            txtAddTestReportCommissioner.BackColor = Color.White
            txtAddTestReportDirector.Clear()
            txtAddTestReportDirector.BackColor = Color.White
            txtAddTestReportProgramManager.Clear()
            txtAddTestReportProgramManager.BackColor = Color.White
            mtbAddTestReportAIRSNumber.Clear()
            mtbAddTestReportAIRSNumber.BackColor = Color.White
            dtpAddTestReportDateReceived.Text = OracleDate
            dtpAddTestReportDateReceived.BackColor = Color.White
            DTPAddTestReportDateCompleted.Text = OracleDate
            DTPAddTestReportDateCompleted.BackColor = Color.White


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub bgw1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgw1.DoWork
        FillDateGrid()
    End Sub

    Private Sub bgw1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgw1.RunWorkerCompleted
        dgvFacilityInfo.DataSource = dsGrid
        dgvFacilityInfo.DataMember = "Grid"

        dgvFacilityInfo.RowHeadersVisible = False
        dgvFacilityInfo.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvFacilityInfo.AllowUserToResizeColumns = True
        dgvFacilityInfo.AllowUserToAddRows = False
        dgvFacilityInfo.AllowUserToDeleteRows = False
        dgvFacilityInfo.AllowUserToOrderColumns = True
        dgvFacilityInfo.AllowUserToResizeRows = True
        dgvFacilityInfo.Columns("strReferenceNumber").HeaderText = "Reference #"
        'dgvFacilityInfo.Columns("strReferenceNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        dgvFacilityInfo.Columns("strReferenceNumber").DisplayIndex = 0
        dgvFacilityInfo.Columns("StrAIRSNumber").HeaderText = "AIRS #"
        'dgvFacilityInfo.Columns("StrAIRSNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        dgvFacilityInfo.Columns("StrAIRSNumber").DisplayIndex = 1
        dgvFacilityInfo.Columns("strFacilityName").HeaderText = "Facility Name"
        'dgvFacilityInfo.Columns("strFacilityName").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgvFacilityInfo.Columns("strFacilityName").DisplayIndex = 2
        dgvFacilityInfo.Columns("forDatReceivedDate").HeaderText = "Received Date"
        'dgvFacilityInfo.Columns("forDatReceivedDate").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgvFacilityInfo.Columns("forDatReceivedDate").DisplayIndex = 3
        dgvFacilityInfo.Columns("forDatTestDateStart").HeaderText = "Date Test Started"
        'dgvFacilityInfo.Columns("forDatTestDateStart").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgvFacilityInfo.Columns("forDatTestDateStart").DisplayIndex = 4
        dgvFacilityInfo.Columns("strDocumentType").HeaderText = "Document Type"
        'dgvFacilityInfo.Columns("strDocumentType").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgvFacilityInfo.Columns("strDocumentType").DisplayIndex = 5
    End Sub

   
    Private Sub btnCloseHistoricTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseHistoricTestReport.Click
        Try
            If txtCloseTestReportRefNum.Text <> "" Then
                SQL = "Select " & _
                "strReferenceNumber " & _
                "from " & DBNameSpace & ".ISMPReportInformation " & _
                "where strReferenceNumber = '" & txtCloseTestReportRefNum.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".ISMPReportInformation set " & _
                    "strClosed = 'True' " & _
                    "where strReferenceNumber = '" & txtCloseTestReportRefNum.Text & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    bgw1.WorkerReportsProgress = True
                    bgw1.WorkerSupportsCancellation = True
                    bgw1.RunWorkerAsync()

                    MsgBox("Test Report Closed", MsgBoxStyle.Information, "Historical Test Report")

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnReOpenHistoricTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReOpenHistoricTestReport.Click
        Try
            If txtCloseTestReportRefNum.Text <> "" Then
                SQL = "Select " & _
                "strReferenceNumber " & _
                "from " & DBNameSpace & ".ISMPReportInformation " & _
                "where strReferenceNumber = '" & txtCloseTestReportRefNum.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".ISMPReportInformation set " & _
                    "strClosed = 'False' " & _
                    "where strReferenceNumber = '" & txtCloseTestReportRefNum.Text & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    bgw1.WorkerReportsProgress = True
                    bgw1.WorkerSupportsCancellation = True
                    bgw1.RunWorkerAsync()

                    MsgBox("Test Report Re-Opened", MsgBoxStyle.Information, "Historical Test Report")

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnOpenTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenTestReport.Click
        Try
            If txtAddTestReportRefNum.Text <> "" Then
                SQL = "select " & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                 "from " & DBNameSpace & ".ISMPDocumentType, " & DBNameSpace & ".ISMPReportInformation " & _
                 "where " & DBNameSpace & ".ISMPReportInformation.strDocumentType = " & DBNameSpace & ".ISMPDocumentType.strKey and " & _
                 "strReferenceNumber = '" & txtAddTestReportRefNum.Text & "'"
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    ISMPTestReportsEntry = Nothing
                    If ISMPTestReportsEntry Is Nothing Then ISMPTestReportsEntry = New ISMPTestReports
                    ISMPTestReportsEntry.txtReferenceNumber.Text = txtAddTestReportRefNum.Text
                    ISMPTestReportsEntry.Show()
                    ISMPTestReportsEntry.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

   
    Private Sub btnSearchForAIRS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchForAIRS.Click
        Try
            If cboAIRSNumber.Text <> "" And cboAIRSNumber.Text.Length = 8 Then
                cboFacilityName.Text = ""
                txtFacilityAddress.Clear()
                txtFacilityCity.Clear()
                txtFacilityState.Clear()
                txtFacilityZipCode.Clear()

                SQL = "select " & _
                "strFacilityName, strFacilityStreet1, " & _
                "strFacilityCity, " & _
                "strFacilityState, strFacilityZipcode " & _
                "from " & DBNameSpace & ".APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & cboAIRSNumber.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        cboFacilityName.Text = ""
                    Else
                        cboFacilityName.Text = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        txtFacilityAddress.Clear()
                    Else
                        txtFacilityAddress.Text = dr.Item("strFacilityStreet1")
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        txtFacilityCity.Clear()
                    Else
                        txtFacilityCity.Text = dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strFacilityState")) Then
                        txtFacilityState.Clear()
                    Else
                        txtFacilityState.Text = dr.Item("strFacilityState")
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        txtFacilityZipCode.Clear()
                    Else
                        txtFacilityZipCode.Text = dr.Item("strFacilityZipCode")
                    End If
                End If
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

  
    Private Sub btnLoadCombos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadCombos.Click
        Try
            btnSearchForAIRS.Visible = False
            cboAIRSNumber.Text = ""
            cboFacilityName.Text = ""
            txtFacilityAddress.Clear()
            txtFacilityCity.Clear()
            txtFacilityState.Clear()
            txtFacilityZipCode.Clear()

            FillFacilityDataSet()
            FillFacilityAndAIRSCombos()
        Catch ex As Exception

        End Try
    End Sub
End Class